using System;
using System.Linq;

using Azure.Core.Amqp;
using Azure.Messaging.ServiceBus;

using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public static class ServiceBusMessageAdapter
    {
        public static bool TryGetReplayText(BrokeredMessage message, out string text) => MessageReplayBody.TryGetText(message, out text);

        public static BrokeredMessage PrepareForReplay(BrokeredMessage source, bool replaceProperties, string editedText,
            BodyType originalType, bool newId, IBrokeredMessageInspector inspector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var outbound = source.Clone();
            try
            {
                if (editedText != null) MessageReplayBody.Edit(outbound, editedText, originalType);
                if (replaceProperties) outbound.Properties.Clear();
                if (inspector != null)
                {
                    // An inspector can return a replacement that still owns or wraps this clone's stream.
                    outbound = inspector.BeforeSendMessage(outbound) ?? throw new InvalidOperationException("The message inspector returned null.");
                }
                foreach (var key in outbound.Properties.Keys.Where(key => Constants.AlwaysOmittedProperties.Contains(key, StringComparer.OrdinalIgnoreCase)).ToArray())
                    outbound.Properties.Remove(key);
                if (newId) outbound.MessageId = Guid.NewGuid().ToString();
                return outbound;
            }
            catch
            {
                outbound.Dispose();
                throw;
            }
        }

        public static ServiceBusMessage ToServiceBusMessage(BrokeredMessage message) => ToReplayMessage(message);

        public static ServiceBusMessage ToReplayMessage(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var serviceBusMessage = new ServiceBusMessage(new AmqpAnnotatedMessage(MessageReplayBody.Read(message)))
            {
                MessageId = message.MessageId,
                SessionId = message.SessionId,
                CorrelationId = message.CorrelationId,
                ContentType = message.ContentType,
                Subject = message.Label,
                PartitionKey = message.PartitionKey,
                TransactionPartitionKey = message.ViaPartitionKey,
                To = message.To,
                ReplyTo = message.ReplyTo,
                ReplyToSessionId = message.ReplyToSessionId,
                TimeToLive = message.TimeToLive,
                ScheduledEnqueueTime = new DateTimeOffset(
                    DateTime.SpecifyKind(message.ScheduledEnqueueTimeUtc, DateTimeKind.Utc))
            };

            foreach (var property in message.Properties)
            {
                serviceBusMessage.ApplicationProperties.Add(property.Key, property.Value);
            }

            return serviceBusMessage;
        }
    }
}
