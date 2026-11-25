using System;
using System.Reflection;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusNamespaceEmulatorTests
    {
        const string EmulatorConnectionString =
            "Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";

        [Theory]
        [InlineData("sb://127.0.0.1:5672", "127.0.0.1")]
        [InlineData("sb://emulator.example.test:5672", "emulator.example.test")]
        public void PaddedKeysPreserveEmulatorHost(string endpoint, string expected)
        {
            var ns = ServiceBusNamespace.GetServiceBusNamespace("test", " Endpoint = " + endpoint +
                "; SharedAccessKeyName = name ; SharedAccessKey = synthetic ; UseDevelopmentEmulator = true ;", (_, __) => { });
            ns.Should().NotBeNull();
            ns.UseDevelopmentEmulator.Should().BeTrue();
            ns.Namespace.Should().Be(expected);
            ns.FullyQualifiedNamespace.Should().Be(expected);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("65536")]
        [InlineData("bad")]
        public void InvalidPortsAreRejected(string port)
        {
            ServiceBusNamespace.GetServiceBusNamespace("test", EmulatorConnectionString.Replace("5672", port),
                (_, __) => { }).Should().BeNull();
        }

        [Fact]
        public void RejectedInvalidConnectionDoesNotReplaceActiveProfileOrClients()
        {
            var helper = new ServiceBusHelper((_, __) => { });
            var profile = typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", BindingFlags.Instance | BindingFlags.NonPublic);
            var active = ServiceBusNamespace.GetServiceBusNamespace("active", EmulatorConnectionString.Replace("UseDevelopmentEmulator=true", "UseDevelopmentEmulator=false"), (_, __) => { });
            profile.SetValue(helper, active);
            var fields = typeof(ServiceBusHelper).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var before = Array.ConvertAll(fields, field => field.GetValue(helper));
            Action connect = () => helper.Connect(null);
            connect.Should().Throw<ArgumentException>();
            profile.GetValue(helper).Should().BeSameAs(active);
            for (var index = 0; index < fields.Length; index++)
                fields[index].GetValue(helper).Should().Be(before[index]);
        }

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
