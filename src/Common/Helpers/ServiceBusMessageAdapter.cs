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

        public static ServiceBusMessage ToServiceBusMessage(BrokeredMessage message) => ToServiceBusMessage(message, false);

        internal static ServiceBusMessage ToServiceBusMessage(BrokeredMessage message, bool sendStringValue)
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

            var serviceBusMessage = ServiceBusReceivedMessageAdapter.CreatePreservedMessage(message, body);
            if (serviceBusMessage == null && sendStringValue)
            {
                // A sender inspector can replace a typed String with an arbitrary stream.
                // Read the current bytes so that an inspector's body edit wins.
                try
                {
                    using (var stream = new MemoryStream(body, false))
                    {
                        var text = (string)new CustomDataContractBinarySerializer(typeof(string)).ReadObject(stream);
                        if (text != null)
                            serviceBusMessage = new ServiceBusMessage(new AmqpAnnotatedMessage(AmqpMessageBody.FromValue(text)));
                    }
                }
                catch (System.Runtime.Serialization.SerializationException) { }
                catch (System.Xml.XmlException) { }
            }
            if (serviceBusMessage == null) serviceBusMessage = new ServiceBusMessage(new BinaryData(body));
            var initial = ServiceBusReceivedMessageAdapter.GetIncompleteHeaders(message);
            if (initial == null) serviceBusMessage.ApplicationProperties.Clear();
            else
                foreach (var key in initial.Properties.Keys)
                    if (!message.Properties.ContainsKey(key)) serviceBusMessage.ApplicationProperties.Remove(key);

            if (initial == null || message.MessageId != initial.MessageId) serviceBusMessage.MessageId = message.MessageId;
            if (initial == null || message.SessionId != initial.SessionId) serviceBusMessage.SessionId = message.SessionId;
            if (initial == null || message.CorrelationId != initial.CorrelationId) serviceBusMessage.CorrelationId = message.CorrelationId;
            if (initial == null || message.ContentType != initial.ContentType) serviceBusMessage.ContentType = message.ContentType;
            if (initial == null || message.Label != initial.Label) serviceBusMessage.Subject = message.Label;
            if (initial == null || message.PartitionKey != initial.PartitionKey) serviceBusMessage.PartitionKey = message.PartitionKey;
            if (initial == null || message.ViaPartitionKey != initial.ViaPartitionKey) serviceBusMessage.TransactionPartitionKey = message.ViaPartitionKey;
            if (initial == null || message.To != initial.To) serviceBusMessage.To = message.To;
            if (initial == null || message.ReplyTo != initial.ReplyTo) serviceBusMessage.ReplyTo = message.ReplyTo;
            if (initial == null || message.ReplyToSessionId != initial.ReplyToSessionId) serviceBusMessage.ReplyToSessionId = message.ReplyToSessionId;
            if (initial == null || message.TimeToLive != initial.TimeToLive) serviceBusMessage.TimeToLive = message.TimeToLive;
            if (initial == null || message.ScheduledEnqueueTimeUtc != initial.ScheduledEnqueueTimeUtc)
                serviceBusMessage.ScheduledEnqueueTime = new DateTimeOffset(DateTime.SpecifyKind(message.ScheduledEnqueueTimeUtc, DateTimeKind.Utc));

            foreach (var property in message.Properties)
                if (initial == null || !initial.Properties.TryGetValue(property.Key, out var old) || !Equals(old, property.Value))
                    serviceBusMessage.ApplicationProperties[property.Key] = property.Value;

            return serviceBusMessage;
        }
    }
}
