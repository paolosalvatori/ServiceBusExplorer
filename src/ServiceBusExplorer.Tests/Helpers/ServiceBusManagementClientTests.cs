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
    [Collection("Service Bus connection settings")]
    public class ServiceBusManagementClientTests
    {
        private const string EmulatorConnection = "Endpoint=sb://localhost:5672;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=test;UseDevelopmentEmulator=true;TransportType=Amqp;EntityPath=orders";

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task EmulatorSecondaryHelperSeparatesManagementAndDataTransport(bool websockets)
        {
            var previous = ServiceBusHelper.UseAmqpWebSockets;
            try
            {
                ServiceBusHelper.UseAmqpWebSockets = websockets;
                var helper = new ServiceBusHelper((_, __) => { });
                var profile = ServiceBusNamespace.GetServiceBusNamespace("synthetic", EmulatorConnection.Replace(":5672", ":5300") + ";RuntimePort=5672", (_, __) => { });
                typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(helper, profile);
                var secondary = helper.GetServiceBusHelper2();
                secondary.AdministrationConnectionString.Should().Contain(":5300");
                secondary.ConnectionString.Should().Contain(":5672").And.NotContain(":5300");
                secondary.TransportType.Should().Be(ServiceBusTransportType.AmqpTcp);
                secondary.CreateAdministrationClient().Should().NotBeNull();
                await secondary.CreateServiceBusClient().DisposeAsync();
            }
            finally { ServiceBusHelper.UseAmqpWebSockets = previous; }
        }

        [Theory]
        [InlineData("priority: high")]
        [InlineData("<![CDATA[priority: high]]>")]
        [InlineData("<Property>valid: value</Property>ignored")]
        public void DirectCorrelationPropertyTextRejectsBeforeAnyMutation(string properties)
        {
            var sdk = new FakeAdministrationClient();
            var xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Queues><Queue><Path>must-not-create</Path></Queue></Queues><Rule><Name>test</Name><Filter><CorrelationFilter><CorrelationId>known</CorrelationId><Properties>" + properties + "</Properties></CorrelationFilter></Filter></Rule></Entities>";
            ((Action)(() => NewHelper(sdk).ImportEntities(xml))).Should().Throw<NotSupportedException>().WithMessage("*No entities were changed*");
            sdk.CreatedQueue.Should().BeNull();
            sdk.CreatedTopic.Should().BeNull();
            sdk.CreatedSubscription.Should().BeNull();
            sdk.CreatedRule.Should().BeNull();
        }

        [Fact]
        public void AdjacentCorrelationPropertiesAllSurviveCompactXml()
        {
            var sdk = new FakeAdministrationClient();
            var xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Topics><Topic><Path>events</Path><Subscriptions><Subscription><TopicPath>events</TopicPath><Name>sub</Name><Rules><Rule><Name>test</Name><Filter><CorrelationFilter><Properties><Property>one: first</Property><Property>two: second:colon</Property><Property>three: third</Property></Properties></CorrelationFilter></Filter></Rule></Rules></Subscription></Subscriptions></Topic></Topics></Entities>";
            NewHelper(sdk).ImportEntities(xml);
            var filter = sdk.CreatedRule.Filter.Should().BeOfType<CorrelationRuleFilter>().Subject;
            filter.ApplicationProperties.Should().HaveCount(3);
            filter.ApplicationProperties["one"].Should().Be("first");
            filter.ApplicationProperties["two"].Should().Be("second:colon");
            filter.ApplicationProperties["three"].Should().Be("third");
        }

        [Theory]
        [InlineData("<Filter unsupported='value'><TrueFilter /></Filter>")]
        [InlineData("<Filter unsupported='value' />")]
        [InlineData("<Filter>ignored<TrueFilter /></Filter>")]
        [InlineData("<Action><EmptyRuleAction unsupported='value'/></Action>")]
        public void UnsupportedWrapperMetadataRejectsBeforeAnyWrite(string wrapper)
        {
            var sdk = new FakeAdministrationClient();
            var xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Queues><Queue><Path>must-not-create</Path></Queue></Queues><Rule><Name>test</Name>" + wrapper + "</Rule></Entities>";
            ((Action)(() => NewHelper(sdk).ImportEntities(xml))).Should().Throw<NotSupportedException>().WithMessage("*No entities were changed*");
            sdk.CreatedQueue.Should().BeNull();
            sdk.CreatedRule.Should().BeNull();
        }

        [Fact]
        public async Task EmulatorReconnectClearsCloudAdaptersAndEnablesModernDataPlane()
        {
            var helper = new ServiceBusHelper((_, __) => { });
            var names = new[] { "Relay", "NotificationHub", "EventHub", "ConsumerGroup", "Partition" };
            foreach (var name in names)
            {
                var type = typeof(ServiceBusHelper).Assembly.GetType("ServiceBusExplorer.WindowsAzure.ServiceBus" + name, true);
                typeof(ServiceBusHelper).GetField("serviceBus" + name, BindingFlags.Instance | BindingFlags.NonPublic)
                    .SetValue(helper, FormatterServices.GetUninitializedObject(type));
            }
            helper.Connect(ServiceBusNamespace.GetServiceBusNamespace("test", EmulatorConnection, (_, __) => { })).Should().BeTrue();
            foreach (var name in names)
                typeof(ServiceBusHelper).GetField("serviceBus" + name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(helper).Should().BeNull();
            var secondary = helper.GetServiceBusHelper2();
            secondary.AdministrationConnectionString.Should().Contain(":5300").And.NotContain("EntityPath");
            secondary.CreateAdministrationClient().Should().NotBeNull();
            await secondary.CreateServiceBusClient().DisposeAsync();
        }

        [Theory]
        [InlineData("Relay")]
        [InlineData("EventHub")]
        [InlineData("NotificationHub")]
        [InlineData("Rule")]
        public void MixedImportRejectsUnsupportedContentBeforeFirstQueueMutation(string unsupported)
        {
            var sdk = new FakeAdministrationClient();
            var helper = NewHelper(sdk);
            typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(helper, ServiceBusNamespace.GetServiceBusNamespace("test", EmulatorConnection, (_, __) => { }));
            var xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Queues><Queue><Path>must-not-create</Path></Queue></Queues><" + unsupported + "/></Entities>";
            ((Action)(() => helper.ImportEntities(xml))).Should().Throw<NotSupportedException>().WithMessage("*No entities were changed*");
            sdk.CreatedQueue.Should().BeNull();
        }

        [Theory]
        [InlineData("FalseFilter")]
        [InlineData("CorrelationFilter")]
        [InlineData("SqlFilter")]
        public void NestedRuleImportIsExplicitlyRejectedWithoutLossyMutation(string filter)
        {
            var sdk = new FakeAdministrationClient();
            var xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Topics><Topic><Path>events</Path><Subscriptions><Subscription><Name>sub</Name><Rules><Rule><Name>filter</Name><Filter><" + filter + "><Properties><Property>key:typed-value</Property></Properties></" + filter + "></Filter><Action><SqlRuleAction/></Action></Rule></Rules></Subscription></Subscriptions></Topic></Topics></Entities>";
            ((Action)(() => NewHelper(sdk).ImportEntities(xml))).Should().Throw<NotSupportedException>().WithMessage("*XML rule shape*No entities were changed*");
            sdk.CreatedRule.Should().BeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData("<TrueFilter />")]
        [InlineData("<FalseFilter />")]
        [InlineData("<SqlFilter><SqlExpression>priority &gt; 4</SqlExpression></SqlFilter>")]
        [InlineData("<CorrelationFilter><CorrelationId>correlation</CorrelationId><Properties><Property>key: value:with:colons</Property></Properties></CorrelationFilter>")]
        public void LegacyNestedRuleFixturesImportWithoutChangingMeaning(string filter)
        {
            var sdk = new FakeAdministrationClient();
            var logs = new List<string>();
            var xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Topics><Topic><Path>events</Path><Subscriptions><Subscription><TopicPath>events</TopicPath><Name>sub</Name><Rules><Rule><Name>$Default</Name><Filter>" + filter + "</Filter><Action><SqlRuleAction><SqlExpression>SET selected = true</SqlExpression></SqlRuleAction></Action></Rule></Rules></Subscription></Subscriptions></Topic></Topics></Entities>";
            new ImportExportHelper((message, _) => logs.Add(message)).DeserializeAndCreate(NewHelper(sdk), xml);
            logs.Should().BeEmpty();
            sdk.CreatedRule.Name.Should().Be("$Default");
            sdk.CreatedRule.Action.Should().BeOfType<SqlRuleAction>().Which.SqlExpression.Should().Be("SET selected = true");
            if (filter.Contains("FalseFilter")) sdk.CreatedRule.Filter.Should().BeOfType<FalseRuleFilter>();
            else if (filter.Contains("SqlFilter")) sdk.CreatedRule.Filter.Should().BeOfType<SqlRuleFilter>().Which.SqlExpression.Should().Be("priority > 4");
            else if (filter.Contains("CorrelationFilter"))
            {
                var correlation = sdk.CreatedRule.Filter.Should().BeOfType<CorrelationRuleFilter>().Subject;
                correlation.CorrelationId.Should().Be("correlation");
                correlation.ApplicationProperties["key"].Should().Be("value:with:colons");
            }
            else sdk.CreatedRule.Filter.Should().BeOfType<TrueRuleFilter>();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task ExportedBooleanRulesRoundTripThroughNestedImport(bool accepts)
        {
            var sdk = new FakeAdministrationClient { ExportedFilter = accepts ? (RuleFilter)new TrueRuleFilter() : new FalseRuleFilter() };
            var helper = NewHelper(sdk);
            var logs = new List<string>();
            var exporter = new ImportExportHelper((message, _) => logs.Add(message));
            var xml = await exporter.ReadAndSerialize(helper, new List<IExtensibleDataObject> { NewClient(sdk).GetTopic("events") });
            exporter.DeserializeAndCreate(helper, xml);
            logs.Should().BeEmpty();
            if (accepts) sdk.CreatedRule.Filter.Should().BeOfType<TrueRuleFilter>();
            else sdk.CreatedRule.Filter.Should().BeOfType<FalseRuleFilter>();
        }

        [Fact]
        public async Task BrokerStringCorrelationExportRoundTripsUnicodeWhitespaceAndAdjacentProperties()
        {
            var filter = new CorrelationRuleFilter();
            filter.ApplicationProperties["first"] = "  blå:雪\r\nline";
            filter.ApplicationProperties["second"] = "value";
            filter.ApplicationProperties["empty"] = "";
            var sdk = new FakeAdministrationClient { ExportedFilter = filter };
            var helper = NewHelper(sdk);
            var logs = new List<string>();
            var exporter = new ImportExportHelper((message, _) => logs.Add(message));
            var xml = await exporter.ReadAndSerialize(helper, new List<IExtensibleDataObject> { NewClient(sdk).GetTopic("events") });
            exporter.DeserializeAndCreate(helper, xml);
            logs.Should().BeEmpty();
            var imported = sdk.CreatedRule.Filter.Should().BeOfType<CorrelationRuleFilter>().Subject;
            imported.ApplicationProperties.Should().BeEquivalentTo(filter.ApplicationProperties);
        }

        [Theory]
        [InlineData("int")]
        [InlineData("bool")]
        [InlineData("double")]
        [InlineData("guid")]
        [InlineData("dateTime")]
        [InlineData("name")]
        [InlineData("sql")]
        public async Task UnrepresentableBrokerRuleAbortsExportAndPreservesExistingDestination(string kind)
        {
            var filter = new CorrelationRuleFilter();
            object value = kind == "int" ? (object)42 : kind == "bool" ? true : kind == "double" ? 1.5 :
                kind == "guid" ? (object)Guid.Empty : kind == "dateTime" ? (object)new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc) : (object)"text";
            filter.ApplicationProperties[kind == "name" ? "key:part" : "key"] = value;
            RuleFilter exported = filter;
            if (kind == "sql")
            {
                var sql = new SqlRuleFilter("value = @p"); sql.Parameters["p"] = 42; exported = sql;
            }
            var sdk = new FakeAdministrationClient { ExportedFilter = exported };
            var helper = NewHelper(sdk);
            var exporter = new ImportExportHelper((_, __) => { });
            var directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "sbe-export-" + Guid.NewGuid().ToString("N"));
            System.IO.Directory.CreateDirectory(directory);
            var path = System.IO.Path.Combine(directory, "backup.xml");
            const string previous = "<previous-backup />";
            System.IO.File.WriteAllText(path, previous);
            try
            {
                var invoked = false;
                var exception = await Assert.ThrowsAnyAsync<Exception>(() => ServiceBusExplorer.Forms.MainForm.PrepareAndSaveExportAsync(
                    () => exporter.ReadAndSerialize(helper, new List<IExtensibleDataObject> { NewClient(sdk).GetTopic("events") }),
                    xml => { invoked = true; ServiceBusExplorer.Forms.MainForm.WriteExportAtomically(path, xml); return path; }));
                exception.GetBaseException().Should().BeOfType<NotSupportedException>();
                invoked.Should().BeFalse();
                System.IO.File.ReadAllText(path).Should().Be(previous);
                System.IO.Directory.GetFiles(directory).Should().ContainSingle();
            }
            finally { System.IO.Directory.Delete(directory, true); }
        }

        [Fact]
        public async Task SuccessfulExportAtomicallyReplacesDestinationAfterPreparation()
        {
            var directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "sbe-export-" + Guid.NewGuid().ToString("N"));
            System.IO.Directory.CreateDirectory(directory);
            var path = System.IO.Path.Combine(directory, "backup.xml");
            System.IO.File.WriteAllText(path, "previous");
            try
            {
                await ServiceBusExplorer.Forms.MainForm.PrepareAndSaveExportAsync(() => Task.FromResult("<new>雪</new>"),
                    xml => { System.IO.File.ReadAllText(path).Should().Be("previous"); ServiceBusExplorer.Forms.MainForm.WriteExportAtomically(path, xml); return path; });
                System.IO.File.ReadAllText(path).Should().Be("<new>雪</new>");
                System.IO.Directory.GetFiles(directory).Should().ContainSingle();
            }
            finally { System.IO.Directory.Delete(directory, true); }
        }

        [Theory]
        [InlineData("<SqlFilter><SqlExpression>value = @p</SqlExpression><Parameters><Parameter name='p' type='int'>1</Parameter></Parameters></SqlFilter>")]
        [InlineData("<CorrelationFilter><Properties><Property type='int'>key: 1</Property></Properties></CorrelationFilter>")]
        public void TypedRuleShapesFailBeforeEarlierQueueMutation(string filter)
        {
            var sdk = new FakeAdministrationClient();
            var xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Queues><Queue><Path>must-not-create</Path></Queue></Queues><Rule><Name>typed</Name><Filter>" + filter + "</Filter></Rule></Entities>";
            ((Action)(() => NewHelper(sdk).ImportEntities(xml))).Should().Throw<NotSupportedException>().WithMessage("*No entities were changed*");
            sdk.CreatedQueue.Should().BeNull();
        }

        [Fact]
        public void NestedTopicAndSubscriptionImportPreservesFalseSettings()
        {
            var sdk = new FakeAdministrationClient();
            var logs = new List<string>();
            const string xml = "<Entities xmlns='http://schemas.microsoft.com/servicebusexplorer'><Topics><Topic><Path>events</Path><EnableBatchedOperations>false</EnableBatchedOperations><Subscriptions><Subscription><TopicPath>events</TopicPath><Name>sub</Name><EnableBatchedOperations>false</EnableBatchedOperations><EnableDeadLetteringOnMessageExpiration>false</EnableDeadLetteringOnMessageExpiration></Subscription></Subscriptions></Topic></Topics></Entities>";
            new ImportExportHelper((message, _) => logs.Add(message)).DeserializeAndCreate(NewHelper(sdk), xml);
            logs.Should().BeEmpty();
            sdk.CreatedTopic.Name.Should().Be("events");
            sdk.CreatedTopic.EnableBatchedOperations.Should().BeFalse();
            sdk.CreatedSubscription.TopicName.Should().Be("events");
            sdk.CreatedSubscription.SubscriptionName.Should().Be("sub");
            sdk.CreatedSubscription.EnableBatchedOperations.Should().BeFalse();
            sdk.CreatedSubscription.DeadLetteringOnMessageExpiration.Should().BeFalse();
        }

        [Fact]
        public async Task SuccessfulMutationsDoNotDependOnOptionalRuntimeRefresh()
        {
            var sdk = new FakeAdministrationClient { RuntimeUnavailable = true };
            var client = NewClient(sdk);
            var created = client.CreateQueue(new QueueInfo("created"));
            created.Path.Should().Be("created");
            created.RuntimeProperties.Should().BeNull();
            var updated = await client.UpdateQueueAsync(new QueueInfo("created") { UserMetadata = "changed" });
            updated.UserMetadata.Should().Be("changed-server");
            updated.RuntimeProperties.Should().BeNull();
            client.UpdateQueue(updated).Should().NotBeNull();
            client.CreateTopic(new TopicInfo("events")).RuntimeProperties.Should().BeNull();
            client.CreateSubscription(new SubscriptionInfo("events", "sub")).RuntimeProperties.Should().BeNull();
            client.UpdateTopic(new TopicInfo("events")).RuntimeProperties.Should().BeNull();
            client.UpdateSubscription(new SubscriptionInfo("events", "sub")).RuntimeProperties.Should().BeNull();
            (await client.UpdateTopicAsync(new TopicInfo("events"))).RuntimeProperties.Should().BeNull();
            (await client.UpdateSubscriptionAsync(new SubscriptionInfo("events", "sub"))).RuntimeProperties.Should().BeNull();
        }

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

        [Theory]
        [InlineData("localhost:5672", "", 5672)]
        [InlineData("localhost", "", 5672)]
        [InlineData("localhost:5300", ";RuntimePort=5672", 5672)]
        [InlineData("localhost:15300", ";RuntimePort=15672", 15672)]
        [InlineData("localhost:15672", ";ManagementPort=15300", 15672)]
        [InlineData("localhost:15300", ";ManagementPort=15300;RuntimePort=15672", 15672)]
        public async Task EmulatorDataPlaneConnection_UsesConfiguredAmqpPortAndEntityScope(string endpoint, string extra, int port)
        {
            var profile = ServiceBusNamespace.GetServiceBusNamespace("test", EmulatorConnection.Replace("localhost:5672", endpoint) + extra, (_, __) => { });
            var helper = new ServiceBusHelper((_, __) => { });
            typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(helper, profile);
            var secondary = helper.GetServiceBusHelper2();
            secondary.ConnectionString.Should().Contain(":" + port + "/").And.Contain("EntityPath=orders")
                .And.NotContain("TransportType").And.NotContain("ManagementPort").And.NotContain("RuntimePort");
            await using var client = secondary.CreateServiceBusClient();
            PhysicalEndpoint(client).Port.Should().Be(port);
            client.CreateSender("orders").Should().NotBeNull();
            ((Action)(() => client.CreateSender("other"))).Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("localhost:5300", "")]
        [InlineData("localhost:5300", ";ManagementPort=15300")]
        [InlineData("localhost:15300", "")]
        [InlineData("localhost", ";ManagementPort=15300")]
        [InlineData("localhost:15300", ";ManagementPort=15300")]
        public void RemappedManagementOnlyEmulatorRefusesDataOperations(string endpoint, string extra)
        {
            var profile = ServiceBusNamespace.GetServiceBusNamespace("test", EmulatorConnection.Replace("localhost:5672", endpoint) + extra, (_, __) => { });
            var helper = new ServiceBusHelper((_, __) => { });
            typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(helper, profile);
            var secondary = helper.GetServiceBusHelper2();
            secondary.CreateAdministrationClient().Should().NotBeNull();
            ((Action)(() => secondary.CreateServiceBusClient())).Should().Throw<NotSupportedException>().WithMessage("*RuntimePort*");
        }

        [Fact]
        public async Task ManagementPortAloneDoesNotSetTheAzureDataEndpoint()
        {
            const string profile = "Endpoint=sb://namespace.invalid/;ManagementPort=14443;SharedAccessKeyName=name;SharedAccessKey=test";
            var data = ServiceBusHelper.GetDataPlaneConnectionString(profile, false);
            var helper = new ServiceBusExplorer.ServiceBus.Helpers.ServiceBusHelper2((_, __) => { }) { ConnectionString = data };
            await using var client = helper.CreateServiceBusClient();
            PhysicalEndpoint(client).IsDefaultPort.Should().BeTrue();
            data.Should().NotContain("14443");
        }

        [Fact]
        public async Task AzureDataPlaneConnection_RetainsScopeAndUsesExplicitWireEndpoint()
        {
            const string connection = "Endpoint=sb://example.servicebus.windows.net:15671/;ManagementPort=14443;SharedAccessKeyName=send;SharedAccessKey=test;TransportType=Amqp;EntityPath=orders";
            var data = ServiceBusHelper.GetDataPlaneConnectionString(connection, false);
            data.Should().Contain("EntityPath=orders").And.NotContain("ManagementPort").And.NotContain("TransportType");
            var helper = new ServiceBusExplorer.ServiceBus.Helpers.ServiceBusHelper2((_, __) => { }) { ConnectionString = data };
            await using var client = helper.CreateServiceBusClient();
            PhysicalEndpoint(client).Port.Should().Be(15671);
            var transport = AmqpTransport(client);
            var credential = (TokenCredential)transport.GetType().GetProperty("Credential", Instance).GetValue(transport);
            var token = credential.GetToken(new TokenRequestContext(Array.Empty<string>()), CancellationToken.None).Token;
            var resource = token.Split('&')[0].Substring("SharedAccessSignature sr=".Length);
            Uri.UnescapeDataString(resource).Should().Be("amqps://example.servicebus.windows.net/orders");
            client.CreateSender("orders").Should().NotBeNull();
            ((Action)(() => client.CreateSender("other"))).Should().Throw<ArgumentException>();
        }

        private const BindingFlags Instance = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private static object AmqpTransport(ServiceBusClient client)
        {
            object connection = typeof(ServiceBusClient).GetProperty("Connection", Instance).GetValue(client);
            return connection.GetType().GetProperty("InnerClient", Instance).GetValue(connection);
        }

        private static Uri PhysicalEndpoint(ServiceBusClient client)
        {
            var transport = AmqpTransport(client);
            return (Uri)transport.GetType().GetProperty("ConnectionEndpoint", Instance).GetValue(transport);
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
            copy.GetServiceBusHelper2().ConnectionString.Should().Contain("Endpoint=sb://localhost:5672/");
            copy.GetServiceBusHelper2().ConnectionString.Should().Contain("EntityPath=orders");
            ns.ConnectionString.Should().Be(EmulatorConnection);
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
            public CreateTopicOptions CreatedTopic { get; private set; }
            public CreateSubscriptionOptions CreatedSubscription { get; private set; }
            public QueueProperties UpdatedQueue { get; private set; }
            public string DeletedQueue { get; private set; }
            public CreateRuleOptions CreatedRule { get; private set; }
            public string DeletedRule { get; private set; }
            public Exception QueueFailure { get; set; }
            public bool RuntimeUnavailable { get; set; }
            public RuleFilter ExportedFilter { get; set; } = new TrueRuleFilter();
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
                RuntimeUnavailable ? throw new InvalidOperationException("Synthetic runtime refresh unavailable") :
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
            public override Task<Response<TopicProperties>> CreateTopicAsync(CreateTopicOptions options, CancellationToken cancellationToken = default)
            {
                CreatedTopic = options;
                return GetTopicAsync(options.Name, cancellationToken);
            }
            public override Task<Response<SubscriptionProperties>> CreateSubscriptionAsync(CreateSubscriptionOptions options, CancellationToken cancellationToken = default)
            {
                CreatedSubscription = options;
                return GetSubscriptionAsync(options.TopicName, options.SubscriptionName, cancellationToken);
            }
            public override Task<Response<SubscriptionProperties>> CreateSubscriptionAsync(CreateSubscriptionOptions options, CreateRuleOptions rule, CancellationToken cancellationToken = default)
            {
                CreatedRule = rule;
                return CreateSubscriptionAsync(options, cancellationToken);
            }
            public override Task<Response<TopicProperties>> UpdateTopicAsync(TopicProperties properties, CancellationToken cancellationToken = default) => Result(properties);
            public override Task<Response<SubscriptionProperties>> UpdateSubscriptionAsync(SubscriptionProperties properties, CancellationToken cancellationToken = default) => Result(properties);
            public override Task<Response<TopicProperties>> GetTopicAsync(string name, CancellationToken cancellationToken = default) =>
                Result(ServiceBusModelFactory.TopicProperties(name, maxSizeInMegabytes: 1024, defaultMessageTimeToLive: TimeSpan.MaxValue,
                    autoDeleteOnIdle: TimeSpan.MaxValue, duplicateDetectionHistoryTimeWindow: TimeSpan.FromMinutes(10), status: EntityStatus.Active));
            public override Task<Response<TopicRuntimeProperties>> GetTopicRuntimePropertiesAsync(string name, CancellationToken cancellationToken = default) =>
                RuntimeUnavailable ? throw new InvalidOperationException("Synthetic runtime refresh unavailable") :
                Result(ServiceBusModelFactory.TopicRuntimeProperties(name, scheduledMessageCount: 3, subscriptionCount: 1));
            public override AsyncPageable<SubscriptionRuntimeProperties> GetSubscriptionsRuntimePropertiesAsync(string topic, CancellationToken cancellationToken = default) =>
                Pages(GetSubscriptionRuntimePropertiesAsync(topic, "sub").Result.Value);
            public override Task<Response<SubscriptionProperties>> GetSubscriptionAsync(string topic, string name, CancellationToken cancellationToken = default) =>
                Result(ServiceBusModelFactory.SubscriptionProperties(topic, name, lockDuration: TimeSpan.FromMinutes(1),
                    defaultMessageTimeToLive: TimeSpan.MaxValue, autoDeleteOnIdle: TimeSpan.MaxValue, maxDeliveryCount: 10, status: EntityStatus.Active, userMetadata: ""));
            public override Task<Response<SubscriptionRuntimeProperties>> GetSubscriptionRuntimePropertiesAsync(string topic, string name, CancellationToken cancellationToken = default) =>
                RuntimeUnavailable ? throw new InvalidOperationException("Synthetic runtime refresh unavailable") :
                Result(ServiceBusModelFactory.SubscriptionRuntimeProperties(topic, name, totalMessageCount: 10, activeMessageCount: 8, deadLetterMessageCount: 2));
            public override AsyncPageable<SubscriptionProperties> GetSubscriptionsAsync(string topic, CancellationToken cancellationToken = default) =>
                Pages(GetSubscriptionAsync(topic, "sub").Result.Value);
            public override Task<Response<RuleProperties>> CreateRuleAsync(string topic, string subscription, CreateRuleOptions options, CancellationToken cancellationToken = default)
            {
                CreatedRule = options;
                return Result(ServiceBusModelFactory.RuleProperties(options.Name, options.Filter, options.Action));
            }
            public override AsyncPageable<RuleProperties> GetRulesAsync(string topic, string subscription, CancellationToken cancellationToken = default) =>
                Pages(ServiceBusModelFactory.RuleProperties("filtered", ExportedFilter, null));
            public override Task<Response> DeleteRuleAsync(string topic, string subscription, string rule, CancellationToken cancellationToken = default)
            {
                DeletedRule = topic + "/" + subscription + "/" + rule;
                return Task.FromResult<Response>(null);
            }
        }
    }
}
