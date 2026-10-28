using System.IO;
using System.Linq;
using System.Text;
using System;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventDataEnvelopeTests
    {
        [Fact]
        public void SetBodyStream_CopiesBodyAndRestoresSourcePosition()
        {
            var source = new MemoryStream(Encoding.UTF8.GetBytes("event body"));
            source.Position = 4;

            using (var eventData = new EventDataEnvelope(source))
            using (var body = eventData.GetBodyStream())
            using (var reader = new StreamReader(body))
            {
                reader.ReadToEnd().Should().Be("event body");
                source.Position.Should().Be(4);
            }
        }

        [Fact]
        public void ToAzureEventData_CopiesBodyAndApplicationProperties()
        {
            using (var eventData = new EventDataEnvelope(new MemoryStream(Encoding.UTF8.GetBytes("event body"))))
            {
                eventData.Properties["message-number"] = 7;

                var azureEventData = eventData.ToAzureEventData();

                Encoding.UTF8.GetString(azureEventData.EventBody.ToArray()).Should().Be("event body");
                azureEventData.Properties["message-number"].Should().Be(7);
            }
        }

        [Fact]
        public void Clone_PreservesMetadataAndOwnsAnIndependentBody()
        {
            var enqueuedTime = new DateTime(2026, 10, 15, 0, 0, 0, DateTimeKind.Utc);
            var original = new EventDataEnvelope(new MemoryStream(Encoding.UTF8.GetBytes("event body")))
            {
                PartitionKey = "device",
                SequenceNumber = 12,
                Offset = "345",
                EnqueuedTimeUtc = enqueuedTime
            };
            original.Properties["application"] = "value";
            original.SystemProperties["service"] = "metadata";

            using (var clone = original.Clone())
            {
                original.Dispose();

                clone.PartitionKey.Should().Be("device");
                clone.SequenceNumber.Should().Be(12);
                clone.Offset.Should().Be("345");
                clone.EnqueuedTimeUtc.Should().Be(enqueuedTime);
                clone.Properties["application"].Should().Be("value");
                clone.SystemProperties["service"].Should().Be("metadata");
                Encoding.UTF8.GetString(clone.ToAzureEventData().EventBody.ToArray()).Should().Be("event body");
            }
        }

        [Fact]
        public async Task Clone_CanCopySharedTemplateConcurrently()
        {
            const string body = "shared event template body";
            using (var template = new EventDataEnvelope(new MemoryStream(Encoding.UTF8.GetBytes(body))))
            {
                var clones = await Task.WhenAll(Enumerable.Range(0, 64).Select(_ => Task.Run(() =>
                {
                    using (var clone = template.Clone())
                    using (var stream = clone.GetBodyStream())
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                })));

                clones.Should().OnlyContain(clone => clone == body);
            }
        }
    }
}
