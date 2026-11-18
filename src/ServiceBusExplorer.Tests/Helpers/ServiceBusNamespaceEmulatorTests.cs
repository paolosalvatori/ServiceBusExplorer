using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusNamespaceEmulatorTests
    {
        const string EmulatorConnectionString =
            "Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";

        [Fact]
        public void EmulatorConnectionString_IsParsedWithoutFullyQualifiedDomainName()
        {
            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", EmulatorConnectionString, (_, __) => { });

            ns.Should().NotBeNull();
            ns.UseDevelopmentEmulator.Should().BeTrue();
            ns.FullyQualifiedNamespace.Should().Be("localhost");
            ns.Namespace.Should().Be("localhost");
        }

        [Fact]
        public void IpAddressEndpoint_IsParsed()
        {
            var cs = EmulatorConnectionString.Replace("localhost", "127.0.0.1");

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", cs, (_, __) => { });

            ns.Should().NotBeNull();
            ns.UseDevelopmentEmulator.Should().BeTrue();
            ns.FullyQualifiedNamespace.Should().Be("127.0.0.1");
        }

        [Fact]
        public void CloudConnectionString_IsNotEmulator()
        {
            var cs = "Endpoint=sb://contoso.servicebus.windows.net/;SharedAccessKeyName=k;SharedAccessKey=v";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", cs, (_, __) => { });

            ns.UseDevelopmentEmulator.Should().BeFalse();
        }
    }
}
