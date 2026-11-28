using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    [Collection("Entra credential cache")]
    public class LegacyManagementIsolationTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConnectAndQueueTopicUrisDoNotInitializeLegacyManagement(bool entra)
        {
            var helper = Connect(entra);
            Assert.EndsWith("/orders", helper.GetQueueUri("orders").AbsoluteUri);
            Assert.EndsWith("/events", helper.GetTopicUri("events").AbsoluteUri);
            Assert.Null(Field(helper, "namespaceManager"));
            Assert.False(((Lazy<NamespaceManager>)Field(helper, "legacyNamespaceManager")).IsValueCreated);
            foreach (var field in new[] { "serviceBusEventHub", "serviceBusConsumerGroup", "serviceBusPartition", "serviceBusRelay", "serviceBusNotificationHub", "messagingFactory" })
                Assert.Null(Field(helper, field));
            foreach (var field in new[] { "serviceBusQueue", "serviceBusTopic", "serviceBusSubscription", "serviceBusRule" })
                Assert.NotNull(Field(helper, field));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void EventHubGroupAndPartitionEntryPointsRetainNamesAndShareManager(bool entra)
        {
            var helper = Connect(entra);
            helper.Scheme = "https";
            Assert.EndsWith("/hub", helper.GetEventHubUri("hub").AbsoluteUri);
            var manager = helper.NamespaceManager;
            Assert.NotNull(manager);
            Assert.Equal("https", helper.GetEventHubUri("hub").Scheme);
            Assert.Contains("$Default", helper.GetConsumerGroupUri("hub", "$Default").AbsoluteUri);
            Assert.EndsWith("/0", helper.GetPartitionUri("hub", "$Default", "0").AbsoluteUri);
            Assert.Same(manager, helper.NamespaceManager);
            Assert.NotNull(Field(helper, "serviceBusEventHub"));
            Assert.NotNull(Field(helper, "serviceBusConsumerGroup"));
            Assert.NotNull(Field(helper, "serviceBusPartition"));
            Assert.Null(Field(helper, "serviceBusRelay"));
            Assert.Null(Field(helper, "messagingFactory"));
            if (entra)
                Assert.Same(EntraCredentialFactory.CreateOldSdkTokenProvider("isolation-test"), manager.Settings.TokenProvider);
            else
                Assert.NotNull(manager.Settings.TokenProvider);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void QueueTopicReadsUseModernManagementWithoutLegacyInitialization(bool entra)
        {
            var helper = Connect(entra);
            var client = new SyntheticAdministrationClient();
            var management = (ServiceBusManagementClient)Field(helper, "managementClient");
            // Keep the connected production adapters; substitute only their SDK network boundary.
            typeof(ServiceBusManagementClient).GetField("<Client>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(management, client);
            Assert.Equal("orders", helper.GetQueue("orders").Path);
            Assert.Equal("events", helper.GetTopic("events").Path);
            Assert.Equal(5, client.Reads);
            Assert.Null(Field(helper, "namespaceManager"));
            Assert.False(((Lazy<NamespaceManager>)Field(helper, "legacyNamespaceManager")).IsValueCreated);
        }

        [Fact]
        public void CopyDoesNotInitializeManagerAndSharesItOnDemand()
        {
            var helper = Connect(false);
            var copy = new ServiceBusHelper((_, __) => { }, helper);
            Assert.Null(Field(helper, "namespaceManager"));
            Assert.Null(Field(copy, "namespaceManager"));
            Assert.False(((Lazy<NamespaceManager>)Field(helper, "legacyNamespaceManager")).IsValueCreated);
            Assert.Same(helper.NamespaceManager, copy.NamespaceManager);
        }

        [Fact]
        public void ReconnectDoesNotRetargetAnUninitializedCopy()
        {
            var helper = Connect(false);
            var copy = new ServiceBusHelper((_, __) => { }, helper);
            Assert.True(helper.Connect(SasProfile(5673)));
            Assert.Null(Field(helper, "namespaceManager"));
            Assert.Equal(5672, copy.NamespaceManager.Address.Port);
            Assert.Equal(5673, helper.NamespaceManager.Address.Port);
            Assert.NotSame(copy.NamespaceManager, helper.NamespaceManager);
        }

        [Fact]
        public void EmulatorDoesNotExposeLegacyManagement()
        {
            var helper = new ServiceBusHelper((_, __) => { });
            var profile = ServiceBusNamespace.GetServiceBusNamespace("local",
                "Endpoint=sb://localhost:5672;SharedAccessKeyName=name;SharedAccessKey=synthetic;UseDevelopmentEmulator=true", (_, __) => { });
            Assert.True(helper.Connect(profile));
            Assert.Null(helper.NamespaceManager);
            Assert.Throws<InvalidOperationException>(() => helper.GetEventHubUri("hub"));
            Assert.EndsWith("/orders", helper.GetQueueUri("orders").AbsoluteUri);
        }

        private sealed class SyntheticAdministrationClient : ServiceBusAdministrationClient
        {
            public int Reads;
            private Task<Response<T>> Result<T>(T value)
            {
                Reads++;
                return Task.FromResult(Response.FromValue(value, null));
            }
            public override Task<Response<QueueProperties>> GetQueueAsync(string name, CancellationToken token = default) =>
                Result(ServiceBusModelFactory.QueueProperties(name));
            public override Task<Response<QueueRuntimeProperties>> GetQueueRuntimePropertiesAsync(string name, CancellationToken token = default) =>
                Result(ServiceBusModelFactory.QueueRuntimeProperties(name));
            public override Task<Response<TopicProperties>> GetTopicAsync(string name, CancellationToken token = default) =>
                Result(ServiceBusModelFactory.TopicProperties(name));
            public override Task<Response<TopicRuntimeProperties>> GetTopicRuntimePropertiesAsync(string name, CancellationToken token = default) =>
                Result(ServiceBusModelFactory.TopicRuntimeProperties(name));
            public override AsyncPageable<SubscriptionRuntimeProperties> GetSubscriptionsRuntimePropertiesAsync(string topic, CancellationToken token = default)
            {
                Reads++;
                return AsyncPageable<SubscriptionRuntimeProperties>.FromPages(new[] {
                    Page<SubscriptionRuntimeProperties>.FromValues(Array.Empty<SubscriptionRuntimeProperties>(), null, null) });
            }
        }

        private static ServiceBusHelper Connect(bool entra)
        {
            var helper = new ServiceBusHelper((_, __) => { });
            // Replace only the network probe; use the app's actual connection preparation/publication.
            typeof(ServiceBusHelper).GetField("serviceBusAudienceProbe", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(helper, new Func<ServiceBusAdministrationClient, Task>(_ => Task.CompletedTask));
            var profile = entra
                ? new ServiceBusNamespace("sb://localhost:5672/", "local", "isolation-test", TransportType.Amqp)
                : SasProfile(5672);
            Assert.True(helper.Connect(profile));
            return helper;
        }

        private static ServiceBusNamespace SasProfile(int port) => ServiceBusNamespace.GetServiceBusNamespace("local",
            "Endpoint=sb://localhost:" + port + ";SharedAccessKeyName=name;SharedAccessKey=synthetic;EntityPath=hub;TransportType=Amqp", (_, __) => { });

        private static object Field(ServiceBusHelper helper, string name) => typeof(ServiceBusHelper)
            .GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(helper);
    }
}
