using FluentAssertions;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventHubCheckpointIdentityTests
    {
        [Theory]
        [InlineData("Endpoint=sb://public.servicebus.windows.net/;SharedAccessKeyName=one;SharedAccessKey=synthetic", "public.servicebus.windows.net")]
        [InlineData("Endpoint=sb://public.servicebus.windows.net/;SharedAccessKeyName=two;SharedAccessKey=rotated", "public.servicebus.windows.net")]
        [InlineData("HostName=device.azure-devices.net;SharedAccessKeyName=owner;SharedAccessKey=synthetic", "legacy-iot:device.azure-devices.net")]
        [InlineData("ordinary-namespace", "ordinary-namespace")]
        [InlineData("SharedAccessKey=synthetic", null)]
        public void LegacyIdentityNeverRetainsCredentials(string oldIdentity, string expected)
        {
            var item = new EventProcessorCheckpointInfo { Namespace = oldIdentity, EventHub = "hub", ConsumerGroup = "$Default" };
            item.Namespace = EventHubCheckpointIdentity.NormalizeStoredNamespace(item.Namespace);
            item.Namespace.Should().Be(expected);
            var json = JsonConvert.SerializeObject(item);
            json.Should().NotContain("SharedAccessKey").And.NotContain("synthetic").And.NotContain("rotated");
            item.EventHub.Should().Be("hub");
        }

        [Fact]
        public void SavedCheckpointAlwaysResumesExclusively()
        {
            var position = EventHubCheckpointIdentity.ResumeAfter(new Lease { Offset = "42" });
            position.Should().Be(Azure.Messaging.EventHubs.Consumer.EventPosition.FromOffset("42", false));
            position.Should().NotBe(Azure.Messaging.EventHubs.Consumer.EventPosition.FromOffset("42", true));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("-1")]
        public void MissingCheckpointStartsAtEarliest(string offset)
        {
            EventHubCheckpointIdentity.ResumeAfter(new Lease { Offset = offset }).Should()
                .Be(Azure.Messaging.EventHubs.Consumer.EventPosition.Earliest);
        }
    }
}
