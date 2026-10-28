using System;
using System.IO;

using Azure;
using Azure.Messaging.ServiceBus;

using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public static class ServiceBusMessageAdapter
    {
        public static ServiceBusMessage ToServiceBusMessageFromReceivedMessage(ServiceBusReceivedMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var serviceBusMessage = new ServiceBusMessage(message.Body)
            {
                MessageId = message.MessageId,
                SessionId = message.SessionId,
                CorrelationId = message.CorrelationId,
                ContentType = message.ContentType,
                Subject = message.Subject,
                PartitionKey = message.PartitionKey,
                To = message.To,
                ReplyTo = message.ReplyTo,
                ReplyToSessionId = message.ReplyToSessionId,
                TransactionPartitionKey = message.TransactionPartitionKey,
                TimeToLive = message.TimeToLive,
                ScheduledEnqueueTime = message.ScheduledEnqueueTime
            };

            foreach (var property in message.ApplicationProperties)
            {
                serviceBusMessage.ApplicationProperties.Add(property.Key, property.Value);
            }

            return serviceBusMessage;
        }

        public static ServiceBusMessage ToServiceBusMessage(BrokeredMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            byte[] body;
            using (var clone = message.Clone())
            using (var bodyStream = clone.GetBody<Stream>())
            using (var buffer = new MemoryStream())
            {
                if (bodyStream != null)
                {
                    bodyStream.CopyTo(buffer);
                }
                body = buffer.ToArray();
            }

            var serviceBusMessage = new ServiceBusMessage(new BinaryData(body))
            {
                MessageId = message.MessageId,
                SessionId = message.SessionId,
                CorrelationId = message.CorrelationId,
                ContentType = message.ContentType,
                Subject = message.Label,
                PartitionKey = message.PartitionKey,
                To = message.To,
                ReplyTo = message.ReplyTo,
                ReplyToSessionId = message.ReplyToSessionId,
                TransactionPartitionKey = message.ViaPartitionKey,
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
