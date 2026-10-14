using System.IO;
using System.Text;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
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
    }
}
