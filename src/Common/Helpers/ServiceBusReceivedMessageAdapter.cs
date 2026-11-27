using System;
using System.IO;
using System.Linq;
using System.Text;
using Azure.Core.Amqp;
using System.Reflection;
using System.Runtime.CompilerServices;

using Azure.Messaging.ServiceBus;

using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public static class ServiceBusReceivedMessageAdapter
    {
        private static readonly ConditionalWeakTable<BrokeredMessage, ReceivedBody> received = new ConditionalWeakTable<BrokeredMessage, ReceivedBody>();

        private sealed class BrokerIdentity
        {
            public long? SequenceNumber;
            public Guid? LockToken;
        }
        private static readonly ConditionalWeakTable<BrokeredMessage, BrokerIdentity> identities = new ConditionalWeakTable<BrokeredMessage, BrokerIdentity>();

        public static long GetSourceSequenceNumber(BrokeredMessage message) =>
            identities.TryGetValue(message, out var identity) && identity.SequenceNumber.HasValue
                ? identity.SequenceNumber.Value : GetReceivedMetadata(message)?.SequenceNumber ?? message.SequenceNumber;

        public static Guid GetSourceLockToken(BrokeredMessage message) =>
            identities.TryGetValue(message, out var identity) && identity.LockToken.HasValue
                ? identity.LockToken.Value : GetReceivedMetadata(message) is ServiceBusReceivedMessage original
                    ? Guid.Parse(original.LockToken) : message.LockToken;

        private static BrokerIdentity CaptureIdentity(BrokeredMessage message)
        {
            if (identities.TryGetValue(message, out var existing)) return existing;
            var identity = new BrokerIdentity();
            // Outbound-only messages have no received headers. Inspectors also run on sends.
            try { identity.SequenceNumber = GetSourceSequenceNumber(message); } catch (InvalidOperationException) { }
            try { identity.LockToken = GetSourceLockToken(message); } catch (InvalidOperationException) { }
            return identity;
        }

        public static ServiceBusReceivedMessage GetReceivedMetadata(BrokeredMessage message) =>
            received.TryGetValue(message, out var original) ? original.Message : null;

        private sealed class ReceivedBody
        {
            public ServiceBusReceivedMessage Message;
            public byte[] Preview;
            public BrokeredMessage IncompleteHeaders;
        }

        public static bool HasStructuredBody(BrokeredMessage message) =>
            received.TryGetValue(message, out var original) &&
            original.Message.GetRawAmqpMessage().Body.BodyType != AmqpMessageBodyType.Data;

        public static BrokeredMessage CloneForResubmission(BrokeredMessage message)
        {
            var identity = CaptureIdentity(message);
            var clone = message.Clone();
            identities.Add(clone, identity);
            if (received.TryGetValue(message, out var original)) received.Add(clone, original);
            return clone;
        }

        public static BrokeredMessage InspectReceivedRow(BrokeredMessage source, IBrokeredMessageInspector inspector) =>
            inspector == null ? source : Inspect(source, inspector.AfterReceiveMessage);

        public static BrokeredMessage Inspect(BrokeredMessage source, Func<BrokeredMessage, BrokeredMessage> inspect)
        {
            var identity = CaptureIdentity(source);
            var result = inspect(source) ?? throw new InvalidOperationException("The message inspector returned no message.");
            if (!ReferenceEquals(source, result)) identities.GetValue(result, _ => identity);
            // Source identity survives an inspector replacement independently of payload edits.
            // CreatePreservedMessage separately checks bytes against the immutable baseline.
            if (!ReferenceEquals(source, result) && received.TryGetValue(source, out var original))
                received.GetValue(result, _ => original);
            return result;
        }

        internal static ServiceBusMessage CreatePreservedMessage(BrokeredMessage message, byte[] body)
        {
            if (!received.TryGetValue(message, out var original)) return null;
            bool unchanged = body.SequenceEqual(original.Preview);
            if (!unchanged && original.IncompleteHeaders == null) return null;
            var copy = ServiceBusMessageAdapter.ToServiceBusMessageFromReceivedMessage(original.Message);
            if (!unchanged)
                copy.GetRawAmqpMessage().Body = AmqpMessageBody.FromData(new[] { new ReadOnlyMemory<byte>(body) });
            return copy;
        }

        internal static BrokeredMessage GetIncompleteHeaders(BrokeredMessage row) =>
            received.TryGetValue(row, out var original) ? original.IncompleteHeaders : null;

        // Preserve the initial editable projection. A repaired fallback row should only
        // overwrite fields the operator actually changed on the original native message.
        private static void RememberIncompleteHeaders(BrokeredMessage row)
        {
            var snapshot = row.Clone();
            snapshot.SetBodyStream(null);
            if (received.TryGetValue(row, out var original)) original.IncompleteHeaders = snapshot;
        }

        private static byte[] Preview(ServiceBusReceivedMessage message)
        {
            var body = message.GetRawAmqpMessage().Body;
            if (body.BodyType == AmqpMessageBodyType.Data) return message.Body.ToArray();
            // The preview is deliberately distinct from the retained AMQP payload. Display
            // serialization must never prevent delivery of another message in a received batch.
            try
            {
                object value = null;
                if (body.TryGetValue(out var scalar)) value = scalar;
                else if (body.TryGetSequence(out var sequence)) value = sequence;
                return Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(value));
            }
            catch (Exception)
            {
                return Encoding.UTF8.GetBytes("[AMQP " + body.BodyType + " body; original payload retained]");
            }
        }

        public static BrokeredMessage ToBrokeredMessage(ServiceBusReceivedMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var brokeredMessage = CreateBodyRow(message);
            try
            {
                CopyReceivedProperties(message, brokeredMessage);
                return brokeredMessage;
            }
            catch
            {
                brokeredMessage.Dispose();
                throw;
            }
        }

        private static BrokeredMessage CreateBodyRow(ServiceBusReceivedMessage message)
        {
            var body = message.GetRawAmqpMessage().Body;
            BrokeredMessage row;
            byte[] baseline;
            if (body.TryGetValue(out var value) && value is string text)
            {
                row = new BrokeredMessage(text);
                using (var copy = row.Clone())
                using (var stream = copy.GetBody<Stream>())
                using (var buffer = new MemoryStream())
                {
                    stream.CopyTo(buffer);
                    baseline = buffer.ToArray();
                }
            }
            else
            {
                var preview = Preview(message);
                row = new BrokeredMessage(new MemoryStream(preview), true);
                baseline = (byte[])preview.Clone();
            }
            received.Add(row, new ReceivedBody { Message = message, Preview = baseline });
            return row;
        }

        // The native batch has already been consumed. Preserve its raw body and identity
        // even when an individual legacy display property cannot represent the delivery.
        internal static BrokeredMessage RetainForDestructiveRead(ServiceBusReceivedMessage message, out Exception failure)
        {
            BrokeredMessage row;
            try
            {
                row = CreateBodyRow(message);
            }
            catch (Exception ex)
            {
                var text = Encoding.UTF8.GetBytes("[Unable to display AMQP body; original message retained]");
                row = new BrokeredMessage(new MemoryStream(text), true);
                received.Add(row, new ReceivedBody { Message = message, Preview = (byte[])text.Clone() });
                if (message.GetRawAmqpMessage().MessageAnnotations.TryGetValue("x-opt-sequence-number", out var seq) && seq is long number)
                    SetReceivedProperty(row, "SequenceNumber", number);
                RememberIncompleteHeaders(row);
                failure = ex;
                return row;
            }
            try
            {
                CopyReceivedProperties(message, row);
                failure = null;
            }
            catch (Exception ex)
            {
                RememberIncompleteHeaders(row);
                failure = ex;
            }
            return row;
        }

        private static void CopyReceivedProperties(ServiceBusReceivedMessage message, BrokeredMessage brokeredMessage)
        {
            SetReceivedProperty(brokeredMessage, "SequenceNumber", message.SequenceNumber);
            if (!string.IsNullOrEmpty(message.MessageId))
            {
                brokeredMessage.MessageId = message.MessageId;
            }
            brokeredMessage.SessionId = message.SessionId;
            brokeredMessage.CorrelationId = message.CorrelationId;
            brokeredMessage.ContentType = message.ContentType;
            brokeredMessage.Label = message.Subject;
            brokeredMessage.To = message.To;
            brokeredMessage.ReplyTo = message.ReplyTo;
            brokeredMessage.ReplyToSessionId = message.ReplyToSessionId;
            brokeredMessage.TimeToLive = message.TimeToLive;
            brokeredMessage.ScheduledEnqueueTimeUtc = message.ScheduledEnqueueTime.UtcDateTime;
            if (string.IsNullOrEmpty(message.SessionId) ||
                string.Equals(message.SessionId, message.PartitionKey, StringComparison.Ordinal))
            {
                brokeredMessage.PartitionKey = message.PartitionKey;
            }

            foreach (var property in message.ApplicationProperties)
            {
                brokeredMessage.Properties.Add(property.Key, property.Value);
            }

            // Keep broker identity out of the editable application-property bag. The pinned
            // legacy SDK exposes internal setters for its received-only display properties.
            SetReceivedProperty(brokeredMessage, "State", (MessageState)message.State);
            SetReceivedProperty(brokeredMessage, "EnqueuedSequenceNumber", message.EnqueuedSequenceNumber);
            var deliveryCount = message.GetRawAmqpMessage().Header.DeliveryCount;
            SetReceivedProperty(brokeredMessage, "DeliveryCount", (int)deliveryCount.GetValueOrDefault());
            SetReceivedProperty(brokeredMessage, "EnqueuedTimeUtc", message.EnqueuedTime.UtcDateTime);
            SetReceivedProperty(brokeredMessage, "LockedUntilUtc", message.LockedUntil.UtcDateTime);
            SetReceivedProperty(brokeredMessage, "LockToken", Guid.Parse(message.LockToken));
            SetReceivedProperty(brokeredMessage, "DeadLetterSource", message.DeadLetterSource);
            brokeredMessage.ViaPartitionKey = message.TransactionPartitionKey;
        }

        private static void SetReceivedProperty(BrokeredMessage target, string name, object value) =>
            typeof(BrokeredMessage).GetProperty(name, BindingFlags.Public | BindingFlags.Instance).SetValue(target, value);
    }
}
