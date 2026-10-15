using System;
using System.Collections.Generic;
using System.IO;

using FluentAssertions;

using Azure;
using Azure.Messaging.ServiceBus;

using Microsoft.ServiceBus.Messaging;

using ServiceBusExplorer.Helpers;

using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusMessageAdapterTests
    {
        [Fact]
        public void ToServiceBusMessage_CopiesBodyAndSendableProperties()
        {
            var body = new byte[] { 0, 1, 2, 127, 255 };
            var scheduledEnqueueTime = DateTime.SpecifyKind(DateTime.UtcNow.AddMinutes(5), DateTimeKind.Unspecified);
            using (var brokeredMessage = new BrokeredMessage(new MemoryStream(body))
            {
                MessageId = "message-id",
                SessionId = "partition",
                CorrelationId = "correlation-id",
                ContentType = "application/octet-stream",
                Label = "subject",
                PartitionKey = "partition",
                ViaPartitionKey = "via-partition",
                To = "destination",
                ReplyTo = "reply",
                ReplyToSessionId = "reply-session",
                TimeToLive = TimeSpan.FromMinutes(10),
                ScheduledEnqueueTimeUtc = scheduledEnqueueTime
            })
            {
                brokeredMessage.Properties.Add("custom", 42);

                var message = ServiceBusMessageAdapter.ToServiceBusMessage(brokeredMessage);
                message.Body.ToArray().Should().Equal(body);
                message.MessageId.Should().Be("message-id");
                message.SessionId.Should().Be("partition");
                message.CorrelationId.Should().Be("correlation-id");
                message.ContentType.Should().Be("application/octet-stream");
                message.Subject.Should().Be("subject");
                message.PartitionKey.Should().Be("partition");
                message.TransactionPartitionKey.Should().Be("via-partition");
                message.To.Should().Be("destination");
                message.ReplyTo.Should().Be("reply");
                message.ReplyToSessionId.Should().Be("reply-session");
                message.TimeToLive.Should().Be(TimeSpan.FromMinutes(10));
                message.ScheduledEnqueueTime.Should().Be(
                    new DateTimeOffset(DateTime.SpecifyKind(scheduledEnqueueTime, DateTimeKind.Utc)));
                message.ApplicationProperties["custom"].Should().Be(42);
            }
        }

        [Fact]
        public void ToServiceBusMessage_NullMessage_ThrowsArgumentNullException()
        {
            Action act = () => ServiceBusMessageAdapter.ToServiceBusMessage(null);

            act.Should().Throw<ArgumentNullException>().WithParameterName("message");
        }

        [Fact]
        public void ToServiceBusMessage_DefaultMessage_CopiesBodyWithoutOptionalMetadata()
        {
            using (var brokeredMessage = new BrokeredMessage("plain text"))
            {
                var message = ServiceBusMessageAdapter.ToServiceBusMessage(brokeredMessage);

                message.Body.Should().NotBeNull();
                message.Subject.Should().BeNull();
                message.ScheduledEnqueueTime.Should().Be(default);
            }
        }

        [Fact]
        public void ToBrokeredMessage_CopiesApplicationAndSystemMetadata()
        {
            var enqueuedTime = DateTimeOffset.UtcNow;
            var lockedUntil = enqueuedTime.AddMinutes(1);
            var lockToken = Guid.NewGuid();
            var message = ServiceBusModelFactory.ServiceBusReceivedMessage(
                body: new BinaryData(new byte[] { 1, 2, 3 }),
                messageId: "message-id",
                partitionKey: "session",
                viaPartitionKey: "via-partition",
                sessionId: "session",
                replyToSessionId: "reply-session",
                timeToLive: TimeSpan.FromMinutes(3),
                correlationId: "correlation",
                subject: "subject",
                to: "destination",
                contentType: "application/octet-stream",
                replyTo: "reply",
                scheduledEnqueueTime: enqueuedTime.AddMinutes(2),
                properties: new Dictionary<string, object> { ["application"] = 42 },
                lockTokenGuid: lockToken,
                deliveryCount: 3,
                lockedUntil: lockedUntil,
                sequenceNumber: 101,
                deadLetterSource: "source-queue",
                enqueuedSequenceNumber: 99,
                enqueuedTime: enqueuedTime);

            using (var brokeredMessage = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(message))
            {
                using (var body = brokeredMessage.GetBody<Stream>())
                using (var buffer = new MemoryStream())
                {
                    body.CopyTo(buffer);
                    buffer.ToArray().Should().Equal(new byte[] { 1, 2, 3 });
                }

                brokeredMessage.MessageId.Should().Be("message-id");
                brokeredMessage.PartitionKey.Should().Be("session");
                brokeredMessage.SessionId.Should().Be("session");
                brokeredMessage.Label.Should().Be("subject");
                brokeredMessage.Properties["application"].Should().Be(42);
                brokeredMessage.Properties["ServiceBusExplorer.SystemProperties.SequenceNumber"].Should().Be(101L);
                brokeredMessage.Properties["ServiceBusExplorer.SystemProperties.EnqueuedSequenceNumber"].Should().Be(99L);
                brokeredMessage.Properties["ServiceBusExplorer.SystemProperties.DeliveryCount"].Should().Be(3);
                brokeredMessage.Properties["ServiceBusExplorer.SystemProperties.EnqueuedTime"].Should().Be(enqueuedTime);
                brokeredMessage.Properties["ServiceBusExplorer.SystemProperties.LockedUntil"].Should().Be(lockedUntil);
                brokeredMessage.Properties["ServiceBusExplorer.SystemProperties.LockToken"].Should().Be(lockToken.ToString());
                brokeredMessage.Properties["ServiceBusExplorer.SystemProperties.DeadLetterSource"].Should().Be("source-queue");
                brokeredMessage.Properties["ServiceBusExplorer.SystemProperties.TransactionPartitionKey"].Should().Be("via-partition");
            }
        }

        [Fact]
        public void ToBrokeredMessage_ConflictingApplicationProperty_ThrowsInsteadOfDroppingMetadata()
        {
            var message = ServiceBusModelFactory.ServiceBusReceivedMessage(
                body: new BinaryData(Array.Empty<byte>()),
                messageId: "message-id",
                properties: new Dictionary<string, object>
                {
                    ["ServiceBusExplorer.SystemProperties.SequenceNumber"] = "application-value"
                },
                sequenceNumber: 1);

            Action act = () => ServiceBusReceivedMessageAdapter.ToBrokeredMessage(message);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("*conflicts with received Service Bus system metadata*");
        }

        [Fact]
        public void ToServiceBusMessage_ReceivedMessage_CopiesOnlySendableMetadata()
        {
            var receivedMessage = ServiceBusModelFactory.ServiceBusReceivedMessage(
                body: new BinaryData(new byte[] { 4, 5, 6 }),
                messageId: "message-id",
                partitionKey: "session",
                sessionId: "session",
                subject: "subject",
                properties: new Dictionary<string, object> { ["application"] = "value" },
                sequenceNumber: 55,
                deliveryCount: 2);

            var outbound = ServiceBusMessageAdapter.ToServiceBusMessageFromReceivedMessage(receivedMessage);

            outbound.Body.ToArray().Should().Equal(new byte[] { 4, 5, 6 });
            outbound.MessageId.Should().Be("message-id");
            outbound.SessionId.Should().Be("session");
            outbound.Subject.Should().Be("subject");
            outbound.ApplicationProperties.Should().ContainKey("application").WhoseValue.Should().Be("value");
            outbound.ApplicationProperties.Should().NotContainKey("ServiceBusExplorer.SystemProperties.SequenceNumber");
        }
    }
}
