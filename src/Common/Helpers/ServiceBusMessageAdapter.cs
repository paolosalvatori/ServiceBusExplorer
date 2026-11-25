using System;
using System.IO;

using Azure;
using Azure.Core.Amqp;
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

            // The SDK received-message constructor preserves Data, Value and Sequence,
            // stripping delivery-only annotations. Detach nested AMQP values as well.
            var copy = new ServiceBusMessage(message);
            var serviceBusMessage = new ServiceBusMessage(
                AmqpAnnotatedMessage.FromBytes(copy.GetRawAmqpMessage().ToBytes()));
            serviceBusMessage.ApplicationProperties.Clear();
            foreach (var property in message.ApplicationProperties)
                serviceBusMessage.ApplicationProperties.Add(property.Key, property.Value);

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

            var serviceBusMessage = ServiceBusReceivedMessageAdapter.CreatePreservedMessage(message, body) ??
                new ServiceBusMessage(new BinaryData(body));
            serviceBusMessage.ApplicationProperties.Clear();
            serviceBusMessage.MessageId = message.MessageId;
            serviceBusMessage.SessionId = message.SessionId;
            serviceBusMessage.CorrelationId = message.CorrelationId;
            serviceBusMessage.ContentType = message.ContentType;
            serviceBusMessage.Subject = message.Label;
            serviceBusMessage.PartitionKey = message.PartitionKey;
            serviceBusMessage.TransactionPartitionKey = message.ViaPartitionKey;
            serviceBusMessage.To = message.To;
            serviceBusMessage.ReplyTo = message.ReplyTo;
            serviceBusMessage.ReplyToSessionId = message.ReplyToSessionId;
            serviceBusMessage.TimeToLive = message.TimeToLive;
            serviceBusMessage.ScheduledEnqueueTime = new DateTimeOffset(
                    DateTime.SpecifyKind(message.ScheduledEnqueueTimeUtc, DateTimeKind.Utc));

            foreach (var property in message.Properties)
            {
                serviceBusMessage.ApplicationProperties.Add(property.Key, property.Value);
            }

            return serviceBusMessage;
        }
    }
}
