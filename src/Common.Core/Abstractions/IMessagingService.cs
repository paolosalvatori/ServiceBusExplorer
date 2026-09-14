using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServiceBusExplorer.Core.Models;

namespace ServiceBusExplorer.Core.Abstractions
{
    /// <summary>
    /// Message-level operations on queues, topics, and subscriptions.
    /// All backed by Azure.Messaging.ServiceBus — runs on macOS/Linux.
    /// </summary>
    public interface IMessagingService
    {
        // ── Send ───────────────────────────────────────────────────────────────────────────

        Task SendMessageAsync(string entityPath, ServiceBusMessageData message, CancellationToken ct = default);
        Task SendMessagesAsync(string entityPath, IEnumerable<ServiceBusMessageData> messages, CancellationToken ct = default);

        // ── Receive ────────────────────────────────────────────────────────────────────────

        Task<IReadOnlyList<ServiceBusMessageData>> ReceiveMessagesAsync(
            string entityPath, int maxMessages = 10, int waitTimeSeconds = 5,
            CancellationToken ct = default);

        Task<IReadOnlyList<ServiceBusMessageData>> PeekMessagesAsync(
            string entityPath, int maxMessages = 10,
            CancellationToken ct = default);

        // ── Dead-letter ───────────────────────────────────────────────────────────────────

        Task<IReadOnlyList<ServiceBusMessageData>> PeekDeadLetterMessagesAsync(
            string entityPath, int maxMessages = 10,
            CancellationToken ct = default);

        /// <summary>Destructively receives (and completes) messages from the dead-letter sub-queue.</summary>
        Task<IReadOnlyList<ServiceBusMessageData>> ReceiveDeadLetterMessagesAsync(
            string entityPath, int maxMessages = 10, int waitTimeSeconds = 5,
            CancellationToken ct = default);

        Task PurgeQueueAsync(string entityPath, CancellationToken ct = default);
        Task PurgeDeadLetterQueueAsync(string entityPath, CancellationToken ct = default);
    }

    /// <summary>Platform-agnostic message representation.</summary>
    public sealed class ServiceBusMessageData
    {
        public string?                        MessageId            { get; set; }
        public string?                        SessionId            { get; set; }
        public string?                        CorrelationId        { get; set; }
        public string?                        Label                { get; set; }
        public string?                        ContentType          { get; set; }
        public string?                        Body                 { get; set; }
        public byte[]?                        BodyBytes            { get; set; }
        public Dictionary<string, object>     Properties           { get; set; } = new Dictionary<string, object>();
        public long                           SequenceNumber       { get; set; }
        public int                            DeliveryCount        { get; set; }
        public string?                        DeadLetterReason     { get; set; }
        public System.DateTimeOffset?         EnqueuedTime         { get; set; }
        public System.DateTimeOffset?         ExpiresAtUtc         { get; set; }
        public System.DateTimeOffset?         ScheduledEnqueueTime { get; set; }

        /// <summary>Body size in bytes (0 when not available).</summary>
        public long                           Size                 { get; set; }

        /// <summary>Human-readable size string, e.g. "1.2 KB".</summary>
        public string SizeDisplay =>
            Size < 1024              ? $"{Size} B"
            : Size < 1024 * 1024     ? $"{Size / 1024.0:F1} KB"
                                     : $"{Size / (1024.0 * 1024):F1} MB";
    }
}

