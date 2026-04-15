using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using FluentAssertions;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.ServiceBus.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusHelperAadTests
    {
        [Fact]
        public void CopyConstructor_AadState_PreservesAadFieldsAndNewSdkBridge()
        {
            var source = new ServiceBusHelper((message, asynchronous) => { })
            {
                NamespaceUri = new Uri("sb://myns.servicebus.windows.net/")
            };
            var aadNamespace = new ServiceBusNamespace(
                "sb://myns.servicebus.windows.net/",
                "myns",
                "tenant-id",
                TransportType.Amqp);

            SetPrivateField(source, "serviceBusNamespaceInstance", aadNamespace);
            SetPrivateField(source, "aadTokenProvider", AadCredentialFactory.CreateOldSdkTokenProvider("tenant-id"));

            var copy = new ServiceBusHelper((message, asynchronous) => { }, source);

            GetPrivateField<TokenProvider>(copy, "aadTokenProvider").Should().NotBeNull();
            GetPrivateField<ServiceBusNamespace>(copy, "serviceBusNamespaceInstance").Should().BeSameAs(aadNamespace);

            var helper2 = copy.GetServiceBusHelper2();
            helper2.IsAad.Should().BeTrue();
            helper2.FullyQualifiedNamespace.Should().Be("myns.servicebus.windows.net");
            helper2.AadTokenCredential.Should().NotBeNull();
        }

        [Fact]
        public void CreateEventHubClient_AadNamespace_ReturnsClient()
        {
            var helper = new ServiceBusHelper((message, asynchronous) => { });
            var aadNamespace = new ServiceBusNamespace(
                "sb://myns.servicebus.windows.net/",
                "myns",
                "tenant-id",
                TransportType.Amqp);

            SetPrivateField(helper, "serviceBusNamespaceInstance", aadNamespace);

            var client = helper.CreateEventHubClient("hub1");

            client.Should().NotBeNull();
        }

        [Fact]
        public void ServiceBusHelper2_CreateClients_AadConfiguration_ReturnsClients()
        {
            var helper2 = new ServiceBusHelper2((message, asynchronous) => { })
            {
                FullyQualifiedNamespace = "myns.servicebus.windows.net",
                AadTokenCredential = new FakeTokenCredential(),
                TransportType = Azure.Messaging.ServiceBus.ServiceBusTransportType.AmqpTcp
            };

            helper2.IsAad.Should().BeTrue();
            helper2.CreateServiceBusClient().Should().NotBeNull();
            helper2.CreateAdministrationClient().Should().NotBeNull();
        }

        [Fact]
        public void AadCredentialFactory_SameTenant_ReusesCredentialCallbackAndProvider()
        {
            var credential1 = AadCredentialFactory.CreateInteractiveBrowserCredential("tenant-id");
            var credential2 = AadCredentialFactory.CreateInteractiveBrowserCredential("tenant-id");
            var callback1 = AadCredentialFactory.CreateOldSdkAuthenticationCallback("tenant-id");
            var callback2 = AadCredentialFactory.CreateOldSdkAuthenticationCallback("tenant-id");
            var provider1 = AadCredentialFactory.CreateOldSdkTokenProvider("tenant-id");
            var provider2 = AadCredentialFactory.CreateOldSdkTokenProvider("tenant-id");
            var tokenCredential1 = AadCredentialFactory.CreateNewSdkTokenCredential("tenant-id");
            var tokenCredential2 = AadCredentialFactory.CreateNewSdkTokenCredential("tenant-id");

            credential2.Should().BeSameAs(credential1);
            callback2.Should().BeSameAs(callback1);
            provider2.Should().BeSameAs(provider1);
            tokenCredential2.Should().BeSameAs(tokenCredential1);
            tokenCredential1.Should().NotBeNull();
        }

        [Fact]
        public void AadCredentialFactory_BlankTenant_ReusesSameCredentialAsOrganizations()
        {
            var fromNull = AadCredentialFactory.CreateInteractiveBrowserCredential(null);
            var fromOrganizations = AadCredentialFactory.CreateInteractiveBrowserCredential("organizations");

            fromOrganizations.Should().BeSameAs(fromNull);
        }

        [Fact]
        public void AadCredentialFactory_GetScopes_UsesResourceValue()
        {
            var scopes = InvokePrivateStatic<string[]>(typeof(AadCredentialFactory), "GetScopes", "https://servicebus.azure.net");

            scopes.Should().ContainSingle().Which.Should().Be("https://servicebus.azure.net/.default");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void AadCredentialFactory_GetAuthority_BlankTenant_UsesOrganizationsEndpoint(string tenantId)
        {
            var authority = AadCredentialFactory.GetAuthority(tenantId);

            authority.Should().Be("https://login.microsoftonline.com/organizations");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateEventHubClient_NullOrEmptyPath_ThrowsArgumentExceptionWithParamName(string path)
        {
            var helper = new ServiceBusHelper((message, asynchronous) => { });

            var act = () => helper.CreateEventHubClient(path);

            act.Should().Throw<ArgumentException>()
                .And.ParamName.Should().Be("path");
        }

        static T GetPrivateField<T>(object instance, string fieldName)
        {
            var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            field.Should().NotBeNull();
            return (T)field.GetValue(instance);
        }

        static T InvokePrivateStatic<T>(Type type, string methodName, params object[] args)
        {
            var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            method.Should().NotBeNull();
            return (T)method.Invoke(null, args);
        }

        static void SetPrivateField(object instance, string fieldName, object value)
        {
            var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            field.Should().NotBeNull();
            field.SetValue(instance, value);
        }

        sealed class FakeTokenCredential : TokenCredential
        {
            public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return new AccessToken("fake-token", DateTimeOffset.UtcNow.AddMinutes(5));
            }

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return new ValueTask<AccessToken>(new AccessToken("fake-token", DateTimeOffset.UtcNow.AddMinutes(5)));
            }
        }
    }
}
