using System;
using System.IO;

using Azure.Messaging.ServiceBus;

using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public static class ServiceBusReceivedMessageAdapter
    {
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

            AddSystemProperty(brokeredMessage, "SequenceNumber", message.SequenceNumber);
            AddSystemProperty(brokeredMessage, "EnqueuedSequenceNumber", message.EnqueuedSequenceNumber);
            AddSystemProperty(brokeredMessage, "PartitionKey", message.PartitionKey);
            AddSystemProperty(brokeredMessage, "DeliveryCount", message.DeliveryCount);
            AddSystemProperty(brokeredMessage, "EnqueuedTime", message.EnqueuedTime);
            AddSystemProperty(brokeredMessage, "ExpiresAt", message.ExpiresAt);
            AddSystemProperty(brokeredMessage, "LockedUntil", message.LockedUntil);
            AddSystemProperty(brokeredMessage, "LockToken", message.LockToken);
            AddSystemProperty(brokeredMessage, "DeadLetterSource", message.DeadLetterSource);
            AddSystemProperty(brokeredMessage, "DeadLetterReason", message.DeadLetterReason);
            AddSystemProperty(brokeredMessage, "DeadLetterErrorDescription", message.DeadLetterErrorDescription);
            AddSystemProperty(brokeredMessage, "TransactionPartitionKey", message.TransactionPartitionKey);
            AddSystemProperty(brokeredMessage, "State", message.State);

            return brokeredMessage;
        }

        private static void AddSystemProperty(BrokeredMessage target, string name, object value)
        {
            var key = $"ServiceBusExplorer.SystemProperties.{name}";
            if (target.Properties.ContainsKey(key))
            {
                throw new InvalidOperationException(
                    $"The application property '{key}' conflicts with received Service Bus system metadata.");
            }
            target.Properties.Add(key, value);
        }
    }
}
