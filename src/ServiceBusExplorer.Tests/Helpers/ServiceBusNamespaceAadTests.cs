using FluentAssertions;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusNamespaceAadTests
    {
        #region Parser round-trip tests

        [Fact]
        public void GetServiceBusNamespace_AadMinimalString_ParsesCorrectly()
        {
            var connectionString = "Endpoint=sb://myns.servicebus.windows.net/;AuthMode=AAD;TransportType=Amqp";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", connectionString, (_, __) => { });

            ns.Should().NotBeNull();
            ns.IsAzureActiveDirectory.Should().BeTrue();
            ns.AuthMode.Should().Be(ServiceBusAuthMode.AzureActiveDirectory);
            ns.Uri.Should().Be("sb://myns.servicebus.windows.net/");
            ns.Namespace.Should().Be("myns");
            ns.FullyQualifiedNamespace.Should().Be("myns.servicebus.windows.net");
            ns.TransportType.Should().Be(TransportType.Amqp);
            ns.TenantId.Should().BeNullOrEmpty();
            ns.EntityPath.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GetServiceBusNamespace_AadWithTenantId_ParsesCorrectly()
        {
            var connectionString = "Endpoint=sb://myns.servicebus.windows.net/;AuthMode=AAD;TenantId=12345678-1234-1234-1234-123456789012;TransportType=Amqp";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", connectionString, (_, __) => { });

            ns.Should().NotBeNull();
            ns.IsAzureActiveDirectory.Should().BeTrue();
            ns.TenantId.Should().Be("12345678-1234-1234-1234-123456789012");
        }

        [Fact]
        public void GetServiceBusNamespace_AadWithEntityPath_ParsesCorrectly()
        {
            var connectionString = "Endpoint=sb://myns.servicebus.windows.net/;AuthMode=AAD;TransportType=Amqp;EntityPath=myqueue";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", connectionString, (_, __) => { });

            ns.Should().NotBeNull();
            ns.IsAzureActiveDirectory.Should().BeTrue();
            ns.EntityPath.Should().Be("myqueue");
        }

        [Fact]
        public void GetServiceBusNamespace_AadEntityPathLegacyEmptyTenant_ParsesCorrectly()
        {
            // Legacy format that may exist in saved configs from earlier versions
            var connectionString = "Endpoint=sb://myns.servicebus.windows.net/;AuthMode=AAD;TenantId=;TransportType=Amqp;EntityPath=myqueue";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", connectionString, (_, __) => { });

            ns.Should().NotBeNull();
            ns.IsAzureActiveDirectory.Should().BeTrue();
            ns.EntityPath.Should().Be("myqueue");
            ns.TenantId.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GetServiceBusNamespace_AadWithAllFields_RoundTrips()
        {
            var original = "Endpoint=sb://myns.servicebus.windows.net/;AuthMode=AAD;TenantId=aaaa-bbbb;TransportType=Amqp;EntityPath=myqueue";
            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", original, (_, __) => { });

            var rebuilt = ServiceBusNamespace.BuildAadConnectionString(
                ns.Uri, ns.TenantId, ns.TransportType, ns.EntityPath);

            var ns2 = ServiceBusNamespace.GetServiceBusNamespace("TestKey2", rebuilt, (_, __) => { });

            ns2.Should().NotBeNull();
            ns2.IsAzureActiveDirectory.Should().BeTrue();
            ns2.Uri.Should().Be(ns.Uri);
            ns2.TenantId.Should().Be(ns.TenantId);
            ns2.TransportType.Should().Be(ns.TransportType);
            ns2.EntityPath.Should().Be(ns.EntityPath);
            ns2.FullyQualifiedNamespace.Should().Be(ns.FullyQualifiedNamespace);
        }

        [Fact]
        public void GetServiceBusNamespace_AadCaseInsensitiveAuthMode_ParsesCorrectly()
        {
            var connectionString = "Endpoint=sb://myns.servicebus.windows.net/;authmode=aad;TransportType=Amqp";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", connectionString, (_, __) => { });

            ns.Should().NotBeNull();
            ns.IsAzureActiveDirectory.Should().BeTrue();
        }

        #endregion

        #region SAS entries still work

        [Fact]
        public void GetServiceBusNamespace_SasString_StillWorks()
        {
            var connectionString = "Endpoint=sb://myns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc123=;TransportType=Amqp";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("SasKey", connectionString, (_, __) => { });

            ns.Should().NotBeNull();
            ns.IsAzureActiveDirectory.Should().BeFalse();
            ns.AuthMode.Should().Be(ServiceBusAuthMode.Sas);
            ns.SharedAccessKeyName.Should().Be("RootManageSharedAccessKey");
        }

        #endregion

        #region Auth mode property tests

        [Fact]
        public void ServiceBusAuthMode_DefaultConstructor_IsSas()
        {
            var ns = new ServiceBusNamespace();

            ns.AuthMode.Should().Be(ServiceBusAuthMode.Sas);
            ns.IsAzureActiveDirectory.Should().BeFalse();
        }

        #endregion

        #region BuildAadConnectionString tests

        [Fact]
        public void BuildAadConnectionString_MinimalFields_ProducesValidString()
        {
            var result = ServiceBusNamespace.BuildAadConnectionString(
                "sb://myns.servicebus.windows.net/", null, TransportType.Amqp);

            result.Should().Contain("Endpoint=sb://myns.servicebus.windows.net/");
            result.Should().Contain("AuthMode=AAD");
            result.Should().Contain("TransportType=Amqp");
            result.Should().NotContain("TenantId=");
            result.Should().NotContain("EntityPath=");
        }

        [Fact]
        public void BuildAadConnectionString_WithTenant_IncludesTenantId()
        {
            var result = ServiceBusNamespace.BuildAadConnectionString(
                "sb://myns.servicebus.windows.net/", "my-tenant-id", TransportType.Amqp);

            result.Should().Contain("TenantId=my-tenant-id");
        }

        [Fact]
        public void BuildAadConnectionString_WithEntityPath_IncludesEntityPath()
        {
            var result = ServiceBusNamespace.BuildAadConnectionString(
                "sb://myns.servicebus.windows.net/", "my-tenant", TransportType.Amqp, "myqueue");

            result.Should().Contain("EntityPath=myqueue");
        }

        [Fact]
        public void BuildAadConnectionString_EntityPathNoTenant_OmitsTenantId()
        {
            var result = ServiceBusNamespace.BuildAadConnectionString(
                "sb://myns.servicebus.windows.net/", null, TransportType.Amqp, "myqueue");

            result.Should().Contain("EntityPath=myqueue");
            result.Should().Contain("AuthMode=AAD");
            result.Should().NotContain("TenantId=");
        }

        [Fact]
        public void BuildAadConnectionString_NetMessagingTransport_IncludesCorrectTransportType()
        {
            var result = ServiceBusNamespace.BuildAadConnectionString(
                "sb://myns.servicebus.windows.net/", null, TransportType.NetMessaging);

            result.Should().Contain("TransportType=NetMessaging");
        }

        #endregion

        #region FullyQualifiedNamespace derivation

        [Fact]
        public void FullyQualifiedNamespace_StripsProtocolAndTrailingSlash()
        {
            var connectionString = "Endpoint=sb://myns.servicebus.windows.net/;AuthMode=AAD;TransportType=Amqp";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", connectionString, (_, __) => { });

            ns.FullyQualifiedNamespace.Should().Be("myns.servicebus.windows.net");
        }

        [Fact]
        public void FullyQualifiedNamespace_NoTrailingSlash_Works()
        {
            var connectionString = "Endpoint=sb://myns.servicebus.windows.net;AuthMode=AAD;TransportType=Amqp";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", connectionString, (_, __) => { });

            ns.FullyQualifiedNamespace.Should().Be("myns.servicebus.windows.net");
        }

        #endregion

        #region Namespace derivation from endpoint

        [Fact]
        public void Namespace_DerivedFromEndpoint_FirstSegmentOfHost()
        {
            var connectionString = "Endpoint=sb://mynamespace.servicebus.windows.net/;AuthMode=AAD;TransportType=Amqp";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("TestKey", connectionString, (_, __) => { });

            ns.Namespace.Should().Be("mynamespace");
        }

        [Fact]
        [Trait("Category", "AAD")]
        public void ConnectionStringWithoutTransportType_AadNamespace_ReturnsNull()
        {
            // AAD namespaces have no SAS connection string, so the property should return null
            var ns = new ServiceBusNamespace { ConnectionString = null };

            var result = ns.ConnectionStringWithoutTransportType;

            result.Should().BeNull("AAD namespaces have no SAS connection string");
        }

        [Fact]
        [Trait("Category", "AAD")]
        public void FullyQualifiedNamespace_InvalidUri_ReturnsNull()
        {
            var ns = new ServiceBusNamespace { Uri = "not://a valid endpoint" };

            var result = ns.FullyQualifiedNamespace;

            result.Should().BeNull("an invalid URI should not throw, just return null");
        }

        [Fact]
        [Trait("Category", "AAD")]
        public void GetServiceBusNamespace_AadWithInvalidEndpoint_NamespaceNameIsNull()
        {
            // Spaces in host cause UriFormatException — the narrowed catch handles this gracefully
            var connectionString = "Endpoint=sb://invalid host name:xyz/;AuthMode=AAD;TransportType=Amqp";

            var ns = ServiceBusNamespace.GetServiceBusNamespace("BadKey", connectionString, (_, __) => { });

            ns.Should().NotBeNull("the parser logs a warning and continues with a null namespace name");
            ns.Namespace.Should().BeNull("the endpoint URI was malformed");
        }

        #endregion
    }
}
