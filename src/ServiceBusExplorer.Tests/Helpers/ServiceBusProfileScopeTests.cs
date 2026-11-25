using System;
using System.Reflection;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus.Administration;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.ServiceBus.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusProfileScopeTests
    {
        [Theory]
        [InlineData("queue-one")]
        [InlineData("topic-one")]
        public void SecondaryHelperRetainsOriginalScopeWhileSdkConnectionsAreNormalized(string entity)
        {
            var profile = ServiceBusNamespace.GetServiceBusNamespace("synthetic", "Endpoint=sb://scope.invalid;SharedAccessKeyName=name;SharedAccessKey=synthetic;TransportType=Amqp;EntityPath=" + entity, (_, __) => { });
            var helper = new ServiceBusHelper((_, __) => { });
            typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(helper, profile);
            var secondary = helper.GetServiceBusHelper2();
            Assert.Equal(entity, secondary.ProfileEntityPath);
            Assert.True(secondary.ConnectionStringContainsEntityPath());
            Assert.DoesNotContain("EntityPath", secondary.ConnectionString);
            Assert.DoesNotContain("EntityPath", secondary.AdministrationConnectionString);
            Assert.Equal(entity, new ServiceBusHelper((_, __) => { }, helper).GetServiceBusHelper2().ProfileEntityPath);
        }

        [Fact]
        public async Task EntityScopedSkuCheckDoesNotConstructAnAdministrationClient()
        {
            var helper = new ProbeGuard { ProfileEntityPath = "queue-one" };
            Assert.False(await helper.IsPremiumNamespace());
            Assert.False(helper.Probed);
        }

        [Fact]
        public void DirectHelperStillRecognizesUnnormalizedEntityScopedConnection()
        {
            var helper = new ServiceBusHelper2((_, __) => { }) { ConnectionString = "Endpoint=sb://scope.invalid;SharedAccessKeyName=name;SharedAccessKey=synthetic;EntityPath=queue-one" };
            Assert.True(helper.ConnectionStringContainsEntityPath());
        }

        private class ProbeGuard : ServiceBusHelper2
        {
            public bool Probed;
            public ProbeGuard() : base((_, __) => { }) { }
            public override ServiceBusAdministrationClient CreateAdministrationClient() { Probed = true; throw new InvalidOperationException("Namespace probe must not run."); }
        }
    }
}
