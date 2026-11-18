using System;
using System.IO;

using FluentAssertions;

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
    }
}
