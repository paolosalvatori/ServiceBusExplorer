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

        public static ServiceBusReceivedMessage GetReceivedMetadata(BrokeredMessage message) =>
            received.TryGetValue(message, out var original) ? original.Message : null;

        private sealed class ReceivedBody
        {
            public ServiceBusReceivedMessage Message;
            public byte[] Preview;
        }

        public static bool HasStructuredBody(BrokeredMessage message) =>
            received.TryGetValue(message, out var original) &&
            original.Message.GetRawAmqpMessage().Body.BodyType != AmqpMessageBodyType.Data;

        public static BrokeredMessage CloneForResubmission(BrokeredMessage message)
        {
            var clone = message.Clone();
            if (received.TryGetValue(message, out var original)) received.Add(clone, original);
            return clone;
        }

        public static BrokeredMessage Inspect(BrokeredMessage source, Func<BrokeredMessage, BrokeredMessage> inspect)
        {
            var result = inspect(source) ?? throw new InvalidOperationException("The message inspector returned no message.");
            // Source identity survives an inspector replacement independently of payload edits.
            // CreatePreservedMessage separately checks bytes against the immutable baseline.
            if (!ReferenceEquals(source, result) && received.TryGetValue(source, out var original))
                received.GetValue(result, _ => original);
            return result;
        }

        internal static ServiceBusMessage CreatePreservedMessage(BrokeredMessage message, byte[] body)
        {
            if (!received.TryGetValue(message, out var original) || !body.SequenceEqual(original.Preview)) return null;
            return ServiceBusMessageAdapter.ToServiceBusMessageFromReceivedMessage(original.Message);
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

            var preview = Preview(message);
            var brokeredMessage = new BrokeredMessage(new MemoryStream(preview), true);
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
            SetReceivedProperty(brokeredMessage, "SequenceNumber", message.SequenceNumber);
            SetReceivedProperty(brokeredMessage, "EnqueuedSequenceNumber", message.EnqueuedSequenceNumber);
            SetReceivedProperty(brokeredMessage, "DeliveryCount", message.DeliveryCount);
            SetReceivedProperty(brokeredMessage, "EnqueuedTimeUtc", message.EnqueuedTime.UtcDateTime);
            SetReceivedProperty(brokeredMessage, "LockedUntilUtc", message.LockedUntil.UtcDateTime);
            SetReceivedProperty(brokeredMessage, "LockToken", Guid.Parse(message.LockToken));
            SetReceivedProperty(brokeredMessage, "DeadLetterSource", message.DeadLetterSource);
            brokeredMessage.ViaPartitionKey = message.TransactionPartitionKey;
            received.Add(brokeredMessage, new ReceivedBody { Message = message, Preview = (byte[])preview.Clone() });

            return brokeredMessage;
        }

        private static void SetReceivedProperty(BrokeredMessage target, string name, object value) =>
            typeof(BrokeredMessage).GetProperty(name, BindingFlags.Public | BindingFlags.Instance).SetValue(target, value);
    }
}
