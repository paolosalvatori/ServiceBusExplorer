using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

using Azure.Messaging.ServiceBus;

using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public static class ServiceBusReceivedMessageAdapter
    {
        private static readonly ConditionalWeakTable<BrokeredMessage, ServiceBusReceivedMessage> received = new ConditionalWeakTable<BrokeredMessage, ServiceBusReceivedMessage>();

        public static ServiceBusReceivedMessage GetReceivedMetadata(BrokeredMessage message) =>
            received.TryGetValue(message, out var original) ? original : null;

        public static BrokeredMessage ToBrokeredMessage(ServiceBusReceivedMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var brokeredMessage = new BrokeredMessage(new MemoryStream(message.Body.ToArray()), true);
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
            received.Add(brokeredMessage, message);

            return brokeredMessage;
        }

        private static void SetReceivedProperty(BrokeredMessage target, string name, object value) =>
            typeof(BrokeredMessage).GetProperty(name, BindingFlags.Public | BindingFlags.Instance).SetValue(target, value);
    }
}
