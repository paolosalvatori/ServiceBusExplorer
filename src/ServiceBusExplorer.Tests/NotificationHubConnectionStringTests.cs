using FluentAssertions;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class NotificationHubConnectionStringTests
    {
        [Fact]
        public void Normalize_Removes_TransportType_Suffix()
        {
            var result = ServiceBusHelper.NormalizeNotificationHubConnectionString(
                "Endpoint=sb://ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc+/=;TransportType=NetMessaging");

            result.Should().Be("Endpoint=sb://ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc+/=");
        }

        [Fact]
        public void Normalize_Keeps_SharedAccessSignature()
        {
            ServiceBusHelper.NormalizeNotificationHubConnectionString("Endpoint=sb://ns/;SharedAccessSignature=SharedAccessSignature sr=x&sig=y;TransportType=Amqp")
                .Should().Be("Endpoint=sb://ns/;SharedAccessSignature=SharedAccessSignature sr=x&sig=y");
        }
    }
}
