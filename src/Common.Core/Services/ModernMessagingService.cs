using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Core.Abstractions;
using ServiceBusExplorer.Core.Models;

namespace ServiceBusExplorer.Core.Services
{
    /// <summary>
    /// Cross-platform message send/receive/peek using Azure.Messaging.ServiceBus.
    /// Works on Windows, macOS and Linux.
    /// </summary>
    public sealed class ModernMessagingService : IMessagingService, IAsyncDisposable
    {
        private readonly ServiceBusClient _client;

        public ModernMessagingService(string connectionString)
            => _client = new ServiceBusClient(connectionString);

        public ModernMessagingService(string fullyQualifiedNamespace, Azure.Core.TokenCredential credential)
            => _client = new ServiceBusClient(fullyQualifiedNamespace, credential);

        public static ModernMessagingService FromProfile(ConnectionProfile profile)
        {
            if (profile.IsAad)
                return new ModernMessagingService(
                    profile.FullyQualifiedNamespace!,
                    AadTokenCredentialFactory.Create(profile.TenantId));

            return new ModernMessagingService(profile.ConnectionString!);
        }

        // ── Send ───────────────────────────────────────────────────────────────────────────

        public async Task SendMessageAsync(string entityPath, ServiceBusMessageData msg, CancellationToken ct = default)
        {
            await using var sender = _client.CreateSender(entityPath);
            await sender.SendMessageAsync(ToSdkMessage(msg), ct).ConfigureAwait(false);
        }

        public async Task SendMessagesAsync(string entityPath, IEnumerable<ServiceBusMessageData> messages, CancellationToken ct = default)
        {
            await using var sender = _client.CreateSender(entityPath);
            var batch = await sender.CreateMessageBatchAsync(ct).ConfigureAwait(false);
            foreach (var m in messages)
                batch.TryAddMessage(ToSdkMessage(m));
            await sender.SendMessagesAsync(batch, ct).ConfigureAwait(false);
        }

        // ── Receive ────────────────────────────────────────────────────────────────────────

        public async Task<IReadOnlyList<ServiceBusMessageData>> ReceiveMessagesAsync(
            string entityPath, int maxMessages = 10, int waitTimeSeconds = 5, CancellationToken ct = default)
        {
            await using var receiver = _client.CreateReceiver(entityPath);
            var msgs = await receiver.ReceiveMessagesAsync(
                maxMessages, TimeSpan.FromSeconds(waitTimeSeconds), ct).ConfigureAwait(false);
            var result = msgs.Select(FromSdkMessage).ToList();

            // Complete each received message
            foreach (var m in msgs)
                await receiver.CompleteMessageAsync(m, ct).ConfigureAwait(false);

            return result;
        }

        public async Task<IReadOnlyList<ServiceBusMessageData>> PeekMessagesAsync(
            string entityPath, int maxMessages = 10, CancellationToken ct = default)
        {
            await using var receiver = _client.CreateReceiver(entityPath);
            var msgs = await receiver.PeekMessagesAsync(maxMessages, cancellationToken: ct).ConfigureAwait(false);
            return msgs.Select(FromSdkPeekedMessage).ToList();
        }

        // ── Dead-letter ───────────────────────────────────────────────────────────────────

        public async Task<IReadOnlyList<ServiceBusMessageData>> PeekDeadLetterMessagesAsync(
            string entityPath, int maxMessages = 10, CancellationToken ct = default)
        {
            await using var receiver = _client.CreateReceiver(entityPath,
                new ServiceBusReceiverOptions
                {
                    SubQueue = SubQueue.DeadLetter,
                    ReceiveMode = ServiceBusReceiveMode.PeekLock
                });
            var msgs = await receiver.PeekMessagesAsync(maxMessages, cancellationToken: ct).ConfigureAwait(false);
            return msgs.Select(FromSdkPeekedMessage).ToList();
        }

        public async Task<IReadOnlyList<ServiceBusMessageData>> ReceiveDeadLetterMessagesAsync(
            string entityPath, int maxMessages = 10, int waitTimeSeconds = 5, CancellationToken ct = default)
        {
            await using var receiver = _client.CreateReceiver(entityPath,
                new ServiceBusReceiverOptions
                {
                    SubQueue = SubQueue.DeadLetter,
                    ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
                });
            var msgs = await receiver.ReceiveMessagesAsync(
                maxMessages, TimeSpan.FromSeconds(waitTimeSeconds), ct).ConfigureAwait(false);
            return msgs.Select(FromSdkMessage).ToList();
        }

        public async Task PurgeQueueAsync(string entityPath, CancellationToken ct = default)
        {
            await using var receiver = _client.CreateReceiver(entityPath,
                new ServiceBusReceiverOptions { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete });

            while (!ct.IsCancellationRequested)
            {
                var batch = await receiver.ReceiveMessagesAsync(
                    maxMessages: 200,
                    maxWaitTime: TimeSpan.FromSeconds(2),
                    cancellationToken: ct).ConfigureAwait(false);

                if (batch.Count == 0)
                    break;
            }
        }

        public async Task PurgeDeadLetterQueueAsync(string entityPath, CancellationToken ct = default)        {
            await using var receiver = _client.CreateReceiver(entityPath,
                new ServiceBusReceiverOptions { SubQueue = SubQueue.DeadLetter });
            ServiceBusReceivedMessage? msg;
            do
            {
                msg = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(2), ct).ConfigureAwait(false);
                if (msg != null)
                    await receiver.CompleteMessageAsync(msg, ct).ConfigureAwait(false);
            } while (msg != null && !ct.IsCancellationRequested);
        }

        // ── IAsyncDisposable ───────────────────────────────────────────────────────────────

        public async ValueTask DisposeAsync() => await _client.DisposeAsync().ConfigureAwait(false);

        // ── Mapping helpers ────────────────────────────────────────────────────────────────

        private static ServiceBusMessage ToSdkMessage(ServiceBusMessageData d)
        {
            ServiceBusMessage m;
            if (d.BodyBytes != null)
                m = new ServiceBusMessage(d.BodyBytes);
            else
                m = new ServiceBusMessage(Encoding.UTF8.GetBytes(d.Body ?? string.Empty));

            if (d.MessageId  != null) m.MessageId     = d.MessageId;
            if (d.SessionId  != null) m.SessionId      = d.SessionId;
            if (d.CorrelationId != null) m.CorrelationId = d.CorrelationId;
            if (d.Label      != null) m.Subject        = d.Label;
            if (d.ContentType != null) m.ContentType   = d.ContentType;
            if (d.ScheduledEnqueueTime.HasValue)
                m.ScheduledEnqueueTime = d.ScheduledEnqueueTime.Value;

            foreach (var kv in d.Properties)
                m.ApplicationProperties[kv.Key] = kv.Value;

            return m;
        }

        private static ServiceBusMessageData FromSdkMessage(ServiceBusReceivedMessage m)
        {
            var props = new Dictionary<string, object>();
            foreach (var kv in m.ApplicationProperties)
                props[kv.Key] = kv.Value;

            var bodyBytes = m.Body.ToArray();
            var decodedBody = DecodeBody(bodyBytes, m.ContentType, m.Body.ToString());

            return new ServiceBusMessageData
            {
                MessageId        = m.MessageId,
                SessionId        = m.SessionId,
                CorrelationId    = m.CorrelationId,
                Label            = m.Subject,
                ContentType      = m.ContentType,
                Body             = decodedBody,
                BodyBytes        = bodyBytes,
                Size             = bodyBytes.LongLength,
                SequenceNumber   = m.SequenceNumber,
                DeliveryCount    = m.DeliveryCount,
                EnqueuedTime     = m.EnqueuedTime,
                ExpiresAtUtc     = m.ExpiresAt,
                DeadLetterReason = m.DeadLetterReason,
                Properties       = props
            };
        }

        private static ServiceBusMessageData FromSdkPeekedMessage(ServiceBusReceivedMessage m) =>
            FromSdkMessage(m);

        private static string DecodeBody(byte[] bodyBytes, string? contentType, string fallback)
        {
            if (bodyBytes.Length == 0)
                return string.Empty;

            string decoded;
            try
            {
                decoded = Encoding.UTF8.GetString(bodyBytes);
            }
            catch
            {
                return fallback;
            }

            var cleaned = StripControlCharacters(decoded).Trim();
            if (cleaned.Length == 0)
                return fallback;

            // Legacy brokered messages may carry envelope prefixes around JSON payloads.
            if (TryExtractJsonPayload(cleaned, out var jsonPayload))
            {
                if (LooksLikeEnvelope(cleaned) || LooksLikeJsonContentType(contentType) || !LooksLikeJson(cleaned))
                    return jsonPayload;
            }

            return cleaned;
        }

        private static bool LooksLikeJsonContentType(string? contentType)
            => !string.IsNullOrWhiteSpace(contentType)
               && contentType.IndexOf("json", StringComparison.OrdinalIgnoreCase) >= 0;

        private static bool LooksLikeEnvelope(string text)
            => text.IndexOf("schemas.microsoft.com/2003/10/Serialization", StringComparison.OrdinalIgnoreCase) >= 0;

        private static bool LooksLikeJson(string text)
        {
            var t = text.TrimStart();
            return t.StartsWith("{", StringComparison.Ordinal) || t.StartsWith("[", StringComparison.Ordinal);
        }

        private static string StripControlCharacters(string value)
        {
            var sb = new StringBuilder(value.Length);
            foreach (var ch in value)
            {
                if (!char.IsControl(ch) || ch == '\r' || ch == '\n' || ch == '\t')
                    sb.Append(ch);
            }
            return sb.ToString();
        }

        private static bool TryExtractJsonPayload(string input, out string payload)
        {
            payload = string.Empty;

            if (!TryExtractBalanced(input, '{', '}', out var obj)
                && !TryExtractBalanced(input, '[', ']', out obj))
                return false;

            payload = obj.Trim();
            return payload.Length > 0;
        }

        private static bool TryExtractBalanced(string input, char open, char close, out string value)
        {
            value = string.Empty;

            var start = input.IndexOf(open);
            if (start < 0)
                return false;

            int depth = 0;
            bool inString = false;
            bool escaped = false;

            for (int i = start; i < input.Length; i++)
            {
                var ch = input[i];

                if (inString)
                {
                    if (escaped)
                    {
                        escaped = false;
                        continue;
                    }

                    if (ch == '\\')
                    {
                        escaped = true;
                        continue;
                    }

                    if (ch == '"')
                        inString = false;

                    continue;
                }

                if (ch == '"')
                {
                    inString = true;
                    continue;
                }

                if (ch == open)
                {
                    depth++;
                    continue;
                }

                if (ch == close)
                {
                    depth--;
                    if (depth == 0)
                    {
                        value = input.Substring(start, i - start + 1);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}



