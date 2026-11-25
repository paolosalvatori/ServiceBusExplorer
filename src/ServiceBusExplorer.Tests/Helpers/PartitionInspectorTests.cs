using System;
using System.IO;
using FluentAssertions;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class PartitionInspectorTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void InspectorFailureStopsProcessingWithReplayPolicyAndOriginalPosition(bool throws)
        {
            using (var data = new EventDataEnvelope(new MemoryStream(new byte[] { 1 }))
                { SequenceNumber = 42, Offset = "123" })
            {
                var inspector = new Inspector(value =>
                {
                    value.SequenceNumber = 999;
                    value.Offset = "changed";
                    if (throws) throw new Exception("inspector error");
                    return null;
                });
                Action inspect = () => PartitionListenerControl.InspectReceivedEvent(inspector, data, "2");
                inspect.Should().Throw<InvalidOperationException>()
                    .WithMessage("*partition 2, sequence 42, offset 123*not checkpointed*restart to replay*");
            }
        }

        [Fact]
        public void SuccessfulInspectionPreservesReturnedEnvelope()
        {
            using (var data = new EventDataEnvelope(new MemoryStream(new byte[] { 1 })))
            {
                PartitionListenerControl.InspectReceivedEvent(new Inspector(value => value), data, "0")
                    .Should().BeSameAs(data);
            }
        }

        private sealed class Inspector : IEventDataInspector
        {
            private readonly Func<EventDataEnvelope, EventDataEnvelope> inspect;
            public Inspector(Func<EventDataEnvelope, EventDataEnvelope> inspect) { this.inspect = inspect; }
            public EventDataEnvelope AfterReceiveMessage(EventDataEnvelope eventData) => inspect(eventData);
            public EventDataEnvelope BeforeSendMessage(EventDataEnvelope eventData) => eventData;
        }
    }
}
