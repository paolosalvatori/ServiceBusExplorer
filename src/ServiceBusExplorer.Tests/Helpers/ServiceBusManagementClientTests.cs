using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;
using Legacy = Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusManagementClientTests
    {
        private const string EmulatorConnection = "Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=test;UseDevelopmentEmulator=true;TransportType=Amqp;EntityPath=orders";

        [Fact]
        public void QueueListing_MaterializesAllPagesAndUsesNativeRuntimeCounts()
        {
            var client = NewClient(new FakeAdministrationClient());

            var queues = client.GetQueues("Startswith(Path, 'ORD') Eq true Or Path Eq 'invoices'", 10).ToArray();

            queues.Select(queue => queue.Path).Should().Equal("orders", "invoices");
            queues[0].MessageCount.Should().Be(3000000000L);
            queues[0].MessageCountDetails.DeadLetterMessageCount.Should().Be(42);
            queues[0].RuntimeProperties.Should().NotBeNull();
            queues[0].Properties.Name.Should().Be("orders");
            queues[0].CreatedAt.Should().Be(new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc));
        }

        [Fact]
        public async Task QueueCrud_UsesAdministrationOptionsAndPreservesNativeOnlyProperties()
        {
            var sdk = new FakeAdministrationClient();
            var client = NewClient(sdk);
            var created = client.CreateQueue(new QueueInfo("new-queue")
            {
                UserMetadata = "metadata",
                ForwardTo = "destination",
                MaxMessageSizeInKilobytes = 4096
            });
            sdk.CreatedQueue.Name.Should().Be("new-queue");
            sdk.CreatedQueue.ForwardTo.Should().Be("destination");
            sdk.CreatedQueue.MaxMessageSizeInKilobytes.Should().Be(4096);
            created.UserMetadata.Should().Be("metadata");

            var queue = client.GetQueue("orders");
            queue.UserMetadata = "updated";
            queue.Status = Legacy.EntityStatus.Disabled;
            var updated = await client.UpdateQueueAsync(queue);
            sdk.UpdatedQueue.UserMetadata.Should().Be("updated");
            sdk.UpdatedQueue.Status.Should().Be(EntityStatus.Disabled);
            sdk.UpdatedQueue.MaxMessageSizeInKilobytes.Should().Be(4096);
            updated.UserMetadata.Should().Be("updated-server", "the update response, not the pre-update request, is the new snapshot");

            await client.DeleteQueueAsync("orders");
            sdk.DeletedQueue.Should().Be("orders");
        }

        [Fact]
        public void TopicAndSubscriptionRuntime_UseSeparateAdministrationSnapshots()
        {
            var client = NewClient(new FakeAdministrationClient());
            var topic = client.GetTopic("events");
            topic.SubscriptionCount.Should().Be(1);
            topic.MessageCountDetails.ActiveMessageCount.Should().Be(8);
            topic.MessageCountDetails.DeadLetterMessageCount.Should().Be(2);
            topic.MessageCountDetails.ScheduledMessageCount.Should().Be(3);
            var subscription = client.GetSubscriptions("events", "MessageCount Gt 9").Should().ContainSingle().Subject;
            subscription.MessageCount.Should().Be(10);
            subscription.TopicPath.Should().Be("events");
            subscription.MessageCountDetails.DeadLetterMessageCount.Should().Be(2);
        }

        [Fact]
        public void RuleCrud_UsesAdministrationNotAMessagingFactory()
        {
            var sdk = new FakeAdministrationClient();
            var client = NewClient(sdk);
            var rule = new RuleInfo("filtered")
            {
                Filter = new Legacy.SqlFilter("priority > 4"),
                Action = new Legacy.SqlRuleAction("SET selected = true")
            };

            client.CreateRule("events", "sub", rule).Name.Should().Be("filtered");
            sdk.CreatedRule.Filter.Should().BeOfType<SqlRuleFilter>().Which.SqlExpression.Should().Be("priority > 4");
            client.GetRules("events", "sub").Should().ContainSingle().Which.Properties.Should().NotBeNull();
            client.DeleteRule("events", "sub", "filtered");
            sdk.DeletedRule.Should().Be("events/sub/filtered");
        }

        [Fact]
        public void Rename_IsExplicitlyUnsupportedWithoutCreatingOrDeletingEntities()
        {
            var sdk = new FakeAdministrationClient();
            var client = NewClient(sdk);
            ((Action)(() => client.RenameQueue("orders", "new"))).Should().Throw<NotSupportedException>();
            ((Action)(() => client.RenameTopic("events", "new"))).Should().Throw<NotSupportedException>();
            sdk.CreatedQueue.Should().BeNull();
            sdk.DeletedQueue.Should().BeNull();
        }

        [Fact]
        public void RequestFailures_AreNotRetriedByLegacyRetryHelper()
        {
            var attempts = 0;
            ((Action)(() => RetryHelper.RetryFunc<int>(() =>
            {
                attempts++;
                throw new RequestFailedException(403, "Manage claim is required");
            }, (_, __) => { }))).Should().Throw<RequestFailedException>();
            attempts.Should().Be(1);
        }

        [Fact]
        public void EntityScopedQueueProbe_OnlySuppressesNotFound()
        {
            var sdk = new FakeAdministrationClient
            {
                QueueFailure = new ServiceBusException("Not a queue", ServiceBusFailureReason.MessagingEntityNotFound)
            };
            var helper = NewHelper(sdk);
            helper.GetQueues(null, 10).Should().BeEmpty();
            sdk.QueueFailure = new RequestFailedException(403, "Manage claim is required");
            ((Action)(() => helper.GetQueues(null, 10))).Should().Throw<RequestFailedException>();
        }

        [Theory]
        [InlineData("localhost:5672", "localhost:5300")]
        [InlineData("localhost", "localhost:5300")]
        [InlineData("127.0.0.1:55300", "127.0.0.1:55300")]
        public void EmulatorAdministrationConnection_UsesHttpPortAndStripsLegacySettings(string endpoint, string expected)
        {
            var connection = EmulatorConnection.Replace("localhost:5672", endpoint);
            var ns = ServiceBusNamespace.GetServiceBusNamespace("test", connection, (_, __) => { });
            var administration = ServiceBusHelper.GetAdministrationConnectionString(ns);
            administration.Should().Contain("Endpoint=sb://" + expected);
            administration.Should().Contain("UseDevelopmentEmulator=true");
            administration.Should().NotContain("TransportType");
            administration.Should().NotContain("EntityPath");
            new ServiceBusAdministrationClient(administration).Should().NotBeNull();
        }

        [Fact]
        public void EmulatorConnect_DoesNotCreateLegacyMessagingFactory()
        {
            var ns = ServiceBusNamespace.GetServiceBusNamespace("test", EmulatorConnection, (_, __) => { });
            var helper = new ServiceBusHelper((_, __) => { });
            helper.Connect(ns).Should().BeTrue();
            helper.ManagementClient.Should().NotBeNull();
            helper.NamespaceManager.Should().BeNull();
            var copy = new ServiceBusHelper((_, __) => { }, helper);
            copy.IsEmulator.Should().BeTrue();
            ((Action)(() => copy.CreateMessagingFactory())).Should().Throw<NotSupportedException>().WithMessage("*data-plane*");
        }

        [Fact]
        public async Task EmulatorAdministrationRequest_UsesHttpWithoutContactingANetwork()
        {
            var ns = ServiceBusNamespace.GetServiceBusNamespace("test", EmulatorConnection, (_, __) => { });
            var capture = new CaptureRequestPolicy();
            var options = new ServiceBusAdministrationClientOptions();
            options.Retry.MaxRetries = 0;
            options.AddPolicy(capture, HttpPipelinePosition.PerCall);
            var client = new ServiceBusAdministrationClient(ServiceBusHelper.GetAdministrationConnectionString(ns), options);

            await Assert.ThrowsAsync<InvalidOperationException>(() => client.QueueExistsAsync("orders"));

            capture.Address.Scheme.Should().Be("http");
            capture.Address.Host.Should().Be("localhost");
            capture.Address.Port.Should().Be(5300);
        }

        [Fact]
        public async Task QueueXml_RetainsExistingContractAndImportsThroughAdministration()
        {
            var sdk = new FakeAdministrationClient();
            var helper = NewHelper(sdk);
            var queue = NewClient(sdk).GetQueue("orders");
            queue.MaxSizeInMegabytes = 81920;
            queue.ForwardTo = "target";
            queue.MaxMessageSizeInKilobytes = 4096;
            var primaryKey = Convert.ToBase64String(new byte[32]);
            var secondaryKey = Convert.ToBase64String(Enumerable.Repeat((byte)1, 32).ToArray());
            queue.Authorization.Add(new Legacy.SharedAccessAuthorizationRule("send", primaryKey, secondaryKey, new[] { Legacy.AccessRights.Send }));
            var log = new List<string>();
            var exporter = new ImportExportHelper((message, _) => log.Add(message));

            var xml = await exporter.ReadAndSerialize(helper, new List<IExtensibleDataObject> { queue });
            var root = XDocument.Parse(xml).Root;
            XNamespace ns = "http://schemas.microsoft.com/servicebusexplorer";
            root.Name.Should().Be(ns + "Entities");
            var saved = root.Element(ns + "Queues").Element(ns + "Queue");
            saved.Element(ns + "Path").Value.Should().Be("orders");
            saved.Element(ns + "MaxSizeInMegabytes").Value.Should().Be("81920");
            saved.Element(ns + "ForwardTo").Value.Should().Be("target");
            saved.Element(ns + "AuthorizationRules").Descendants(ns + "PrimaryKey").Single().Value.Should().Be(primaryKey);
            log.Should().BeEmpty();

            exporter.DeserializeAndCreate(helper, xml);
            log.Should().BeEmpty();
            sdk.CreatedQueue.Name.Should().Be("orders");
            sdk.CreatedQueue.MaxSizeInMegabytes.Should().Be(81920);
            sdk.CreatedQueue.MaxMessageSizeInKilobytes.Should().Be(4096);
            sdk.CreatedQueue.ForwardTo.Should().Be("target");
            sdk.CreatedQueue.AuthorizationRules.OfType<SharedAccessAuthorizationRule>().Single().SecondaryKey.Should().Be(secondaryKey);
        }

        [Fact]
        public async Task LegacyQueueXml_StillImportsWithoutNativeOnlyFields()
        {
            var sdk = new FakeAdministrationClient();
            var helper = NewHelper(sdk);
            var xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Queues><Queue><Path>legacy</Path><MaxDeliveryCount>7</MaxDeliveryCount><UserMetadata>existing export</UserMetadata></Queue></Queues></Entities>";
            var log = new List<string>();
            new ImportExportHelper((message, _) => log.Add(message)).DeserializeAndCreate(helper, xml);
            log.Should().BeEmpty();
            sdk.CreatedQueue.Name.Should().Be("legacy");
            sdk.CreatedQueue.MaxDeliveryCount.Should().Be(7);
            sdk.CreatedQueue.UserMetadata.Should().Be("existing export");
            await Task.CompletedTask;
        }

        private static ServiceBusManagementClient NewClient(FakeAdministrationClient client) => new ServiceBusManagementClient(client, new Uri("sb://localhost/"));

        private sealed class CaptureRequestPolicy : HttpPipelinePolicy
        {
            public Uri Address { get; private set; }
            public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
            {
                Address = message.Request.Uri.ToUri();
                throw new InvalidOperationException("Request captured; no network access.");
            }
            public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
            {
                Process(message, pipeline);
                return default;
            }
        }

        private static ServiceBusHelper NewHelper(FakeAdministrationClient client)
        {
            var helper = new ServiceBusHelper((_, __) => { });
            var ns = ServiceBusNamespace.GetServiceBusNamespace("test", EmulatorConnection, (_, __) => { });
            var management = NewClient(client);
            foreach (var entity in new[] { "Queue", "Topic", "Subscription", "Rule" })
            {
                var type = typeof(ServiceBusHelper).Assembly.GetType("ServiceBusExplorer.WindowsAzure.ServiceBus" + entity, true);
                var handler = Activator.CreateInstance(type, ns, management);
                typeof(ServiceBusHelper).GetField("serviceBus" + entity, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(helper, handler);
            }
            return helper;
        }

        private sealed class FakeAdministrationClient : ServiceBusAdministrationClient
        {
            public CreateQueueOptions CreatedQueue { get; private set; }
            public QueueProperties UpdatedQueue { get; private set; }
            public string DeletedQueue { get; private set; }
            public CreateRuleOptions CreatedRule { get; private set; }
            public string DeletedRule { get; private set; }
            public Exception QueueFailure { get; set; }
            private readonly Dictionary<string, QueueProperties> queues = new Dictionary<string, QueueProperties>();

            private static QueueProperties Queue(string name) => ServiceBusModelFactory.QueueProperties(name,
                lockDuration: TimeSpan.FromMinutes(1), maxSizeInMegabytes: 1024,
                defaultMessageTimeToLive: TimeSpan.MaxValue, autoDeleteOnIdle: TimeSpan.MaxValue,
                duplicateDetectionHistoryTimeWindow: TimeSpan.FromMinutes(10), maxDeliveryCount: 10,
                status: EntityStatus.Active, userMetadata: "", maxMessageSizeInKilobytes: 4096);
            private static Task<Response<T>> Result<T>(T value) => Task.FromResult(Response.FromValue(value, null));
            private static AsyncPageable<T> Pages<T>(params T[] values) where T : notnull =>
                AsyncPageable<T>.FromPages(values.Select((value, index) => Page<T>.FromValues(new[] { value }, index == values.Length - 1 ? null : "next", null)));
            public override Task<Response<QueueProperties>> GetQueueAsync(string name, CancellationToken cancellationToken = default)
            {
                if (QueueFailure != null) throw QueueFailure;
                return Result(queues.TryGetValue(name, out var queue) ? queue : Queue(name));
            }
            public override Task<Response<QueueRuntimeProperties>> GetQueueRuntimePropertiesAsync(string name, CancellationToken cancellationToken = default) =>
                Result(ServiceBusModelFactory.QueueRuntimeProperties(name, totalMessageCount: 3000000000L, activeMessageCount: 2999999958L,
                    deadLetterMessageCount: 42, createdAt: new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero)));
            public override AsyncPageable<QueueProperties> GetQueuesAsync(CancellationToken cancellationToken = default) => Pages(Queue("orders"), Queue("invoices"));
            public override AsyncPageable<QueueRuntimeProperties> GetQueuesRuntimePropertiesAsync(CancellationToken cancellationToken = default) =>
                Pages(GetQueueRuntimePropertiesAsync("orders").Result.Value, GetQueueRuntimePropertiesAsync("invoices").Result.Value);
            public override Task<Response<QueueProperties>> CreateQueueAsync(CreateQueueOptions options, CancellationToken cancellationToken = default)
            {
                CreatedQueue = options;
                var properties = Queue(options.Name);
                properties.UserMetadata = options.UserMetadata;
                properties.MaxMessageSizeInKilobytes = options.MaxMessageSizeInKilobytes;
                queues[options.Name] = properties;
                return Result(properties);
            }
            public override Task<Response<QueueProperties>> UpdateQueueAsync(QueueProperties properties, CancellationToken cancellationToken = default)
            {
                UpdatedQueue = properties;
                var updated = Queue(properties.Name);
                updated.UserMetadata = properties.UserMetadata + "-server";
                updated.MaxMessageSizeInKilobytes = properties.MaxMessageSizeInKilobytes;
                queues[properties.Name] = updated;
                return Result(updated);
            }
            public override Task<Response> DeleteQueueAsync(string name, CancellationToken cancellationToken = default)
            {
                DeletedQueue = name;
                return Task.FromResult<Response>(null);
            }
            public override Task<Response<TopicProperties>> GetTopicAsync(string name, CancellationToken cancellationToken = default) =>
                Result(ServiceBusModelFactory.TopicProperties(name, maxSizeInMegabytes: 1024, defaultMessageTimeToLive: TimeSpan.MaxValue,
                    autoDeleteOnIdle: TimeSpan.MaxValue, duplicateDetectionHistoryTimeWindow: TimeSpan.FromMinutes(10), status: EntityStatus.Active));
            public override Task<Response<TopicRuntimeProperties>> GetTopicRuntimePropertiesAsync(string name, CancellationToken cancellationToken = default) =>
                Result(ServiceBusModelFactory.TopicRuntimeProperties(name, scheduledMessageCount: 3, subscriptionCount: 1));
            public override AsyncPageable<SubscriptionRuntimeProperties> GetSubscriptionsRuntimePropertiesAsync(string topic, CancellationToken cancellationToken = default) =>
                Pages(GetSubscriptionRuntimePropertiesAsync(topic, "sub").Result.Value);
            public override Task<Response<SubscriptionProperties>> GetSubscriptionAsync(string topic, string name, CancellationToken cancellationToken = default) =>
                Result(ServiceBusModelFactory.SubscriptionProperties(topic, name, lockDuration: TimeSpan.FromMinutes(1),
                    defaultMessageTimeToLive: TimeSpan.MaxValue, autoDeleteOnIdle: TimeSpan.MaxValue, maxDeliveryCount: 10, status: EntityStatus.Active, userMetadata: ""));
            public override Task<Response<SubscriptionRuntimeProperties>> GetSubscriptionRuntimePropertiesAsync(string topic, string name, CancellationToken cancellationToken = default) =>
                Result(ServiceBusModelFactory.SubscriptionRuntimeProperties(topic, name, totalMessageCount: 10, activeMessageCount: 8, deadLetterMessageCount: 2));
            public override AsyncPageable<SubscriptionProperties> GetSubscriptionsAsync(string topic, CancellationToken cancellationToken = default) =>
                Pages(GetSubscriptionAsync(topic, "sub").Result.Value);
            public override Task<Response<RuleProperties>> CreateRuleAsync(string topic, string subscription, CreateRuleOptions options, CancellationToken cancellationToken = default)
            {
                CreatedRule = options;
                return Result(ServiceBusModelFactory.RuleProperties(options.Name, options.Filter, options.Action));
            }
            public override AsyncPageable<RuleProperties> GetRulesAsync(string topic, string subscription, CancellationToken cancellationToken = default) =>
                Pages(ServiceBusModelFactory.RuleProperties("filtered", new TrueRuleFilter(), null));
            public override Task<Response> DeleteRuleAsync(string topic, string subscription, string rule, CancellationToken cancellationToken = default)
            {
                DeletedRule = topic + "/" + subscription + "/" + rule;
                return Task.FromResult<Response>(null);
            }
        }
    }
}
