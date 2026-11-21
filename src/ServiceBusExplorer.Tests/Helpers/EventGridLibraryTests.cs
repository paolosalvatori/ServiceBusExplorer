using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using Azure.ResourceManager.EventGrid;
using Azure.ResourceManager.EventGrid.Models;
using EventGridExplorerLibrary;
using FluentAssertions;
using ServiceBusExplorer.Utilities.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventGridLibraryTests
    {
        private static readonly NamespaceTopicEndpoint Endpoint = new NamespaceTopicEndpoint("store.example.test");

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NamespaceTopicEndpoint_MissingHostname_ExplainsHowToRecover(string hostname)
        {
            Action getHostname = () => new NamespaceTopicEndpoint(hostname);

            getHostname.Should().Throw<InvalidOperationException>()
                .WithMessage("*no topics hostname*refresh the namespace*");
        }

        [Fact]
        public void NamespaceTopicEndpoint_ConfiguredHostname_ProducesHttpsAddressAndTreeLabel()
        {
            Endpoint.Address.Should().Be(new Uri("https://store.example.test"));
            Endpoint.ToString().Should().Be("https://store.example.test");
        }

        [Theory]
        [InlineData("https://")]
        [InlineData("http://example.westus-1.eventgrid.azure.net")]
        [InlineData("bad host")]
        [InlineData("store.example.test/topics/orders")]
        public void NamespaceTopicEndpoint_RejectsMalformedArmHostname(string hostname)
        {
            Action getEndpoint = () => new NamespaceTopicEndpoint(hostname);

            getEndpoint.Should().Throw<ArgumentException>()
                .WithMessage("*invalid topics hostname*Refresh*")
                .Which.ParamName.Should().Be("hostname");
        }

        [Fact]
        public async Task GetTopicsAsync_WithoutEndpointStillBrowsesButBlocksDataPlaneBeforeFetchingKeys()
        {
            var arm = new EventGridArmTransport();
            arm.AddRefresh("orders");
            var library = new TestLibrary(arm);

            var topics = await library.GetTopicsAsync("group", "store", null);
            topics.Select(t => t.Data.Name).Should().Equal("orders");
            Func<Task> publish = () => library.PublishEventAsync("orders", "/shop", "Order.Created", "{}");

            await publish.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*namespace endpoint is not loaded*Refresh*");
            arm.Requests.Should().ContainSingle().Which.Method.Should().Be(HttpMethod.Get);
        }

        [Fact]
        public async Task GetTopicsAsync_LoadsAllPagesAndSubscriptionsWithoutReadingKeys()
        {
            var arm = new EventGridArmTransport();
            var nextLink = "https://management.azure.com" + EventGridArmTransport.TopicsPath + "?page=2";
            arm.AddTopicPage(new[] { "orders" }, nextLink);
            arm.AddTopicPage(new[] { "shipments" });
            var library = new TestLibrary(arm);

            var topics = await library.GetTopicsAsync("group", "store", Endpoint);

            topics.Select(t => t.Data.Name).Should().Equal("orders", "shipments");
            topics.Select(t => t.Id.Name).Should().Equal("orders", "shipments");
            arm.AddResponse(new { value = Array.Empty<object>() });
            await foreach (var page in library.GetEventSubscriptionsAsync(topics[0]).AsPages())
            {
                page.Values.Should().BeEmpty();
            }
            var path = EventGridArmTransport.TopicsPath;
            arm.Requests.Select(r => (r.Method, r.Uri.AbsolutePath)).Should().Equal(
                (HttpMethod.Get, path), (HttpMethod.Get, path), (HttpMethod.Get, path + "/orders/eventSubscriptions"));
            arm.Requests[1].Uri.Should().Be(new Uri(nextLink));
            library.Senders.Should().BeEmpty();
            library.ReceiverCreations.Should().BeEmpty();
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public async Task Publish_UncachedTopic_ExplainsHowToRecover(bool batch, bool removed)
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            if (removed)
            {
                arm.AddRefresh("orders");
                await library.GetTopicsAsync("group", "store", Endpoint);
            }
            arm.AddRefresh("another-topic");
            await library.GetTopicsAsync("group", "store", Endpoint);
            Func<Task> publish = () => batch
                ? library.PublishEventsAsync("orders", "/shop", "Order.Created", new List<string> { "one" })
                : library.PublishEventAsync("orders", "/shop", "Order.Created", "{}");

            await publish.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*topic 'orders' is not loaded*Refresh the namespace topics*");
            library.Senders.Should().BeEmpty();
            arm.Requests.Should().OnlyContain(request => request.Method == HttpMethod.Get);
        }

        [Fact]
        public async Task PublishEventAsync_And_PublishEventsAsync_DelegateToSenderClient()
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            arm.AddRefresh("orders");
            await library.GetTopicsAsync("group", "store", Endpoint);
            arm.AddKeys("orders-key");

            await library.PublishEventAsync("orders", "/shop/orders", "Order.Created", "{\"id\":42}");
            await library.PublishEventsAsync("orders", "/shop/orders", "Order.Updated", new List<string> { "one", "two" });

            var fakeSender = library.Senders.Single();
            fakeSender.SingleEvents.Should().ContainSingle();
            fakeSender.SingleEvents[0].Source.Should().Be("/shop/orders");
            fakeSender.SingleEvents[0].Type.Should().Be("Order.Created");

            fakeSender.BatchEvents.Should().ContainSingle();
            fakeSender.BatchEvents[0].Select(e => e.Type).Should().Equal("Order.Updated", "Order.Updated");
            arm.Requests.Count(r => r.Uri.AbsolutePath.EndsWith("/orders/listKeys")).Should().Be(1);
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(45, 45)]
        [InlineData(500, 120)]
        public async Task ReceiveEventsAsync_ClampsWaitTime_AndReturnsDetails(int configuredWaitSeconds, int expectedWaitSeconds)
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm, configuredWaitSeconds);
            arm.AddRefresh("orders");
            await library.GetTopicsAsync("group", "store", Endpoint);
            arm.AddKeys("orders-key");
            var cloudEvent = new CloudEvent("/source", "Sample.Event", "payload");
            var details = EventGridNamespacesModelFactory.ReceiveDetails(
                EventGridNamespacesModelFactory.BrokerProperties("lock-1", 1),
                cloudEvent);
            var fakeReceiver = library.Receiver;
            fakeReceiver.NextReceiveResult = EventGridNamespacesModelFactory.ReceiveResult(new[] { details });

            var result = await library.ReceiveEventsAsync("orders", "sub-a", 5);

            result.Should().NotBeNull();
            result.Details.Should().ContainSingle();
            result.Details[0].BrokerProperties.LockToken.Should().Be("lock-1");
            result.Details[0].Event.Id.Should().Be(cloudEvent.Id);
            fakeReceiver.LastMaxEvents.Should().Be(5);
            fakeReceiver.LastMaxWaitTime.Should().Be(TimeSpan.FromSeconds(expectedWaitSeconds));
        }

        [Fact]
        public async Task ReceiveEventsAsync_ReusesReceiverPerSubscriptionUntilSuccessfulRefresh()
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            arm.AddRefresh("orders");
            await library.GetTopicsAsync("group", "store", Endpoint);
            arm.AddKeys("orders-key");
            library.ReceiverCreations.Should().BeEmpty();

            await library.ReceiveEventsAsync("orders", "sub-a", 1);
            await library.ReceiveEventsAsync("orders", "sub-a", 1);
            await library.ReceiveEventsAsync("orders", "sub-b", 1);

            var newEndpoint = new NamespaceTopicEndpoint("changed.example.test");
            arm.AddTopicPage(new[] { "orders" });
            arm.AddKeys("rotated");
            await library.GetTopicsAsync("group", "store", newEndpoint);
            await library.ReceiveEventsAsync("orders", "sub-a", 1);

            library.ReceiverCreations.Should().Equal(
                (Endpoint.Address, "orders", "sub-a", "orders-key"),
                (Endpoint.Address, "orders", "sub-b", "orders-key"),
                (newEndpoint.Address, "orders", "sub-a", "rotated"));
        }

        [Fact]
        public async Task FailedRefresh_PreservesSendersReceiversEndpointAndKeys()
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            arm.AddRefresh("orders", "shipments");
            await library.GetTopicsAsync("group", "store", Endpoint);
            arm.AddKeys("orders-key");
            await library.PublishEventAsync("orders", "/shop", "BeforeRefresh", "{}");
            await library.ReceiveEventsAsync("orders", "existing", 1);
            var originalSender = library.Senders.Single();

            arm.AddTopicPage(new[] { "new-topic" }, "https://management.azure.com" + EventGridArmTransport.TopicsPath + "?page=2");
            Func<Task> publishPartial = () => library.PublishEventAsync("new-topic", "/shop", "Order.Created", "{}");
            arm.AddResponse(new { error = new { code = "Forbidden", message = "Access to topic listing denied" } },
                HttpStatusCode.Forbidden, async () =>
                {
                    await library.PublishEventAsync("orders", "/shop", "DuringRefresh", "{}");
                    await publishPartial.Should().ThrowAsync<InvalidOperationException>().WithMessage("*topic 'new-topic' is not loaded*");
                });
            Func<Task> refresh = () => library.GetTopicsAsync("group", "store", new NamespaceTopicEndpoint("changed.example.test"));
            (await refresh.Should().ThrowAsync<RequestFailedException>()).Which.Status.Should().Be(403);

            await library.PublishEventAsync("orders", "/shop", "Order.Created", "{}");
            await library.ReceiveEventsAsync("orders", "existing", 1);
            arm.AddKeys("shipments-key");
            await library.ReceiveEventsAsync("shipments", "new-sub", 1);
            originalSender.SingleEvents.Select(e => e.Type).Should().Equal("BeforeRefresh", "DuringRefresh", "Order.Created");
            library.ReceiverCreations.Should().Equal(
                (Endpoint.Address, "orders", "existing", "orders-key"),
                (Endpoint.Address, "shipments", "new-sub", "shipments-key"));
            await publishPartial.Should().ThrowAsync<InvalidOperationException>().WithMessage("*topic 'new-topic' is not loaded*");
        }

        [Fact]
        public async Task GetTopicsAsync_AnOlderRefreshCannotReplaceTheNewerSnapshot()
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            var requested = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var finishOlder = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            arm.AddResponse(new { value = new[] { EventGridArmTransport.Topic("old") } }, beforeResponse: () =>
            {
                requested.TrySetResult(true);
                return finishOlder.Task;
            });
            arm.AddRefresh("new");
            var older = library.GetTopicsAsync("group", "store", Endpoint);
            try
            {
                (await Task.WhenAny(requested.Task, older)).Should().BeSameAs(requested.Task);
                var newer = library.GetTopicsAsync("group", "store", Endpoint);
                (await Task.WhenAny(newer, Task.Delay(TimeSpan.FromSeconds(5)))).Should().BeSameAs(newer);
                (await newer).Select(t => t.Data.Name).Should().Equal("new");
            }
            finally
            {
                finishOlder.TrySetResult(true);
            }
            await older;

            arm.AddKeys("new-key");
            await library.PublishEventAsync("new", "/source", "Test", "{}");
            Func<Task> oldTopic = () => library.PublishEventAsync("old", "/source", "Test", "{}");
            await oldTopic.Should().ThrowAsync<InvalidOperationException>().WithMessage("*topic 'old' is not loaded*");
        }

        [Fact]
        public async Task DeniedTopicKeyLeavesOtherTopicsAvailableAndCanBeRetried()
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            arm.AddRefresh("restricted", "orders");

            var topics = await library.GetTopicsAsync("group", "store", Endpoint);
            topics.Select(t => t.Data.Name).Should().Equal("restricted", "orders");
            arm.AddResponse(new { error = new { code = "Forbidden", message = "No listKeys permission" } }, HttpStatusCode.Forbidden);
            Func<Task> restricted = () => library.PublishEventAsync("restricted", "/source", "Test", "{}");
            await restricted.Should().ThrowAsync<InvalidOperationException>().WithMessage("*HTTP 403*Browsing is still available*can list topic keys*");
            library.Senders.Should().BeEmpty();

            arm.AddKeys("orders-key");
            await library.PublishEventAsync("orders", "/source", "Test", "{}");
            arm.AddKeys("restricted-key");
            await restricted();

            library.Senders.Should().HaveCount(2).And.OnlyContain(sender => sender.SingleEvents.Count == 1);
            arm.Requests.Count(r => r.Uri.AbsolutePath.EndsWith("/restricted/listKeys")).Should().Be(2);
            arm.Requests.Count(r => r.Uri.AbsolutePath.EndsWith("/orders/listKeys")).Should().Be(1);
        }

        [Fact]
        public async Task UnauthorizedPublishDoesNotReplayAndLateFailureCannotInvalidateTheNewKey()
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            var oldSendStarted = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var finishOldSend = new TaskCompletionSource<Response>(TaskCreationOptions.RunContinuationsAsynchronously);
            library.SendSingle = (key, evt) =>
            {
                if (key == "old-key" && evt.Type == "Delayed")
                {
                    oldSendStarted.TrySetResult(true);
                    return finishOldSend.Task;
                }
                if (key == "old-key")
                {
                    return Task.FromException<Response>(new RequestFailedException(401, "Unauthorized"));
                }
                return Task.FromResult<Response>(new FakeResponse());
            };
            arm.AddRefresh("orders");
            await library.GetTopicsAsync("group", "store", Endpoint);
            arm.AddKeys("old-key");

            var delayed = library.PublishEventAsync("orders", "/source", "Delayed", "{}");
            try
            {
                (await Task.WhenAny(oldSendStarted.Task, delayed)).Should().BeSameAs(oldSendStarted.Task);
                Func<Task> publish = () => library.PublishEventAsync("orders", "/source", "Immediate", "{}");
                await publish.Should().ThrowAsync<InvalidOperationException>().WithMessage("*retry the operation*not retried automatically*");
                library.Senders.Single().SingleEvents.Select(evt => evt.Type).Should().Equal("Delayed", "Immediate");

                arm.AddKeys("new-key");
                await publish();
            }
            finally
            {
                finishOldSend.TrySetException(new RequestFailedException(401, "Unauthorized"));
            }
            Func<Task> finish = () => delayed;
            await finish.Should().ThrowAsync<InvalidOperationException>().WithMessage("*not retried automatically*");
            await library.PublishEventAsync("orders", "/source", "Next", "{}");

            library.Senders.Should().HaveCount(2);
            library.Senders[1].SingleEvents.Select(evt => evt.Type).Should().Equal("Immediate", "Next");
            arm.Requests.Count(r => r.Uri.AbsolutePath.EndsWith("/orders/listKeys")).Should().Be(2);
        }

        [Theory]
        [InlineData("Acknowledge")]
        [InlineData("Release")]
        [InlineData("Reject")]
        public async Task ReceiverPaths_ReportMissingEndpointAndRemovalOfPreviouslyCachedTopic(string action)
        {
            var logs = new List<string>();
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm, log: (msg, _) => logs.Add(msg));
            var settlementLogs = new List<string>();
            Func<Task<bool>> settle = () => library.EventActionsAsync(action, new List<string> { "token" }, "orders", "sub",
                (msg, _) => settlementLogs.Add(msg));

            (await library.ReceiveEventsAsync("orders", "sub", 1)).Should().BeNull();
            logs.Should().ContainSingle().Which.Should().Contain("namespace endpoint is not loaded").And.Contain("Refresh");
            (await settle()).Should().BeFalse();
            settlementLogs.Should().ContainSingle().Which.Should().Contain("namespace endpoint is not loaded").And.Contain("Refresh");

            arm.AddRefresh("orders");
            await library.GetTopicsAsync("group", "store", Endpoint);
            arm.AddKeys("orders-key");
            (await library.ReceiveEventsAsync("orders", "sub", 1)).Should().NotBeNull();
            arm.AddRefresh();
            await library.GetTopicsAsync("group", "store", Endpoint);
            logs.Clear();
            settlementLogs.Clear();

            (await library.ReceiveEventsAsync("orders", "sub", 1)).Should().BeNull();
            logs.Should().ContainSingle().Which.Should().Contain("topic 'orders' is not loaded").And.Contain("Refresh");
            (await settle()).Should().BeFalse();
            settlementLogs.Should().ContainSingle().Which.Should().Contain("topic 'orders' is not loaded").And.Contain("Refresh");
        }

        [Fact]
        public async Task EventActionsAsync_HandlesAcknowledgeReleaseAndReject()
        {
            var logs = new List<string>();
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            arm.AddRefresh("orders");
            await library.GetTopicsAsync("group", "store", Endpoint);
            arm.AddKeys("orders-key");
            library.Receiver.NextAcknowledgeResult = EventGridNamespacesModelFactory.AcknowledgeResult(
                Array.Empty<FailedLockToken>(), new[] { "tok-1" });
            library.Receiver.NextReleaseResult = EventGridNamespacesModelFactory.ReleaseResult(
                Array.Empty<FailedLockToken>(), new[] { "tok-2" });
            library.Receiver.NextRejectResult = EventGridNamespacesModelFactory.RejectResult(
                new[] { EventGridNamespacesModelFactory.FailedLockToken("tok-3", new ResponseError("ExpiredLock", "Token expired")) },
                Array.Empty<string>());

            var ackSucceeded = await library.EventActionsAsync("Acknowledge", new List<string> { "tok-1" }, "orders", "sub-a", (msg, _) => logs.Add(msg));
            var releaseSucceeded = await library.EventActionsAsync("Release", new List<string> { "tok-2" }, "orders", "sub-a", (msg, _) => logs.Add(msg));
            var rejectSucceeded = await library.EventActionsAsync("Reject", new List<string> { "tok-3" }, "orders", "sub-a", (msg, _) => logs.Add(msg));

            ackSucceeded.Should().BeTrue();
            releaseSucceeded.Should().BeTrue();
            rejectSucceeded.Should().BeFalse();
            logs.Should().Contain(l => l.Contains("ExpiredLock"));
            logs.Should().Contain(l => l.Contains("Token expired"));
        }

        [Theory]
        [InlineData(401, "retry the operation")]
        [InlineData(403, "network access policy")]
        [InlineData(429, "HTTP 429")]
        public async Task EventActionsAsync_ReportsServiceFailureWithoutReplaying(int status, string expectedMessage)
        {
            var logs = new List<string>();
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            arm.AddRefresh("orders");
            await library.GetTopicsAsync("group", "store", Endpoint);
            arm.AddKeys("orders-key");
            library.Receiver.AcknowledgeError = new RequestFailedException(status, "Service failure");

            var result = await library.EventActionsAsync("Acknowledge", new List<string> { "test-token" }, "orders", "sub",
                (message, _) => logs.Add(message));

            result.Should().BeFalse();
            logs.Should().ContainSingle().Which.Should().Contain(expectedMessage).And.Contain("not retried automatically");
            library.Receiver.AcknowledgeCalls.Should().Be(1);
            arm.Requests.Count(r => r.Uri.AbsolutePath.EndsWith("/orders/listKeys")).Should().Be(1);
        }

        [Fact]
        public async Task EventActionsAsync_UnsupportedActionWithTokens_ReportsArgumentError()
        {
            var library = new TestLibrary(new EventGridArmTransport());
            Func<Task> settle = () => library.EventActionsAsync("Unknown", new List<string> { "token" }, "orders", "sub", (_, __) => { });

            (await settle.Should().ThrowAsync<ArgumentOutOfRangeException>()).Which.ParamName.Should().Be("action");
            library.ReceiverCreations.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateSubscriptionAsync_CreatesQueueSubscriptionUnderTheNamespaceTopic()
        {
            var arm = new EventGridArmTransport();
            var library = new TestLibrary(arm);
            var subscriptionPath = EventGridArmTransport.TopicsPath + "/orders/eventSubscriptions/queue-sub";
            arm.AddResponse(EventGridArmTransport.Topic("orders"));
            arm.AddResponse(new { error = new { code = "ResourceNotFound", message = "No subscription" } }, HttpStatusCode.NotFound);
            arm.AddResponse(new { id = subscriptionPath, name = "queue-sub", type = NamespaceTopicEventSubscriptionResource.ResourceType.ToString(), properties = new { provisioningState = "Succeeded" } });

            await library.CreateSubscriptionAsync("group", "store", "orders", "queue-sub", "Queue",
                new List<Dictionary<string, string>>(), new List<string>());

            var request = arm.Requests.Single(r => r.Method == HttpMethod.Put);
            request.Uri.AbsolutePath.Should().Be(subscriptionPath);
            request.Uri.Query.Should().Be("?api-version=2025-02-15");
            using (var json = JsonDocument.Parse(request.Body))
            {
                json.RootElement.GetProperty("properties").GetProperty("deliveryConfiguration")
                    .GetProperty("deliveryMode").GetString().Should().Be("Queue");
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NamespaceWebhookSubscription_UsesGaCreateOrUpdateRequest(bool recreateArmClient)
        {
            var arm = new EventGridArmTransport();
            var controlPlane = arm.CreateClient();
            if (recreateArmClient)
            {
                controlPlane.armclient = null;
            }

            arm.AddResponse(EventGridArmTransport.Topic("orders"));
            var topic = (await controlPlane.GetNamespaceResource("group", "store").GetNamespaceTopicAsync("orders")).Value;
            var subscriptionPath = EventGridArmTransport.TopicsPath + "/orders/eventSubscriptions/webhook";
            arm.AddResponse(new { id = subscriptionPath, name = "webhook", type = NamespaceTopicEventSubscriptionResource.ResourceType.ToString(), properties = new { } });
            var destination = new Uri("https://example.test/events");
            var data = new NamespaceTopicEventSubscriptionData
            {
                DeliveryConfiguration = new DeliveryConfiguration
                {
                    DeliveryMode = DeliveryMode.Push,
                    Push = new PushInfo { Destination = new WebHookEventSubscriptionDestination { Endpoint = destination } }
                }
            };

            await topic.GetNamespaceTopicEventSubscriptions().CreateOrUpdateAsync(WaitUntil.Started, "webhook", data);

            var request = arm.Requests.Single(r => r.Method == HttpMethod.Put);
            request.Uri.AbsolutePath.Should().Be(subscriptionPath);
            request.Uri.Query.Should().Be("?api-version=2025-02-15");
            using (var json = JsonDocument.Parse(request.Body))
            {
                var push = json.RootElement.GetProperty("properties").GetProperty("deliveryConfiguration").GetProperty("push");
                push.GetProperty("destination").GetProperty("endpointType").GetString().Should().Be("WebHook");
                push.GetProperty("destination").GetProperty("properties").GetProperty("endpointUrl").GetString().Should().Be(destination.AbsoluteUri);
            }
        }

        [Fact]
        public void EventGridFilterFactory_CreatesGaResourceManagerFilters()
        {
            var configuration = new FiltersConfiguration();
            var factory = new EventGridFilterFactory(configuration)
            {
                Key = "data.priority",
                OperatorType = "Number is in range",
                Value = "1-5,10-20"
            };
            factory.FilterSelection();

            factory.Key = "data.enabled";
            factory.OperatorType = "Boolean equals";
            factory.Value = "true";
            factory.FilterSelection();

            configuration.Filters.Should().HaveCount(2);
            configuration.Filters[0].Should().BeOfType<NumberInRangeFilter>()
                .Which.Values.Should().HaveCount(2);
            configuration.Filters[1].Should().BeOfType<BoolEqualsFilter>()
                .Which.Value.Should().BeTrue();
        }

        private sealed class TestLibrary : EventGridLibrary
        {
            public TestLibrary(EventGridArmTransport arm, int waitSeconds = 30, WriteToLogDelegate log = null)
                : base(arm.CreateClient(), waitSeconds, log ?? ((_, __) => { }))
            {
            }

            public Func<string, CloudEvent, Task<Response>> SendSingle { get; set; }
            public List<FakeEventGridSenderClient> Senders { get; } = new List<FakeEventGridSenderClient>();
            public FakeEventGridReceiverClient Receiver { get; } = new FakeEventGridReceiverClient();
            public List<(Uri Endpoint, string Topic, string Subscription, string Key)> ReceiverCreations { get; } =
                new List<(Uri, string, string, string)>();

            internal override EventGridSenderClient CreateSenderClient(Uri endpoint, string topicName, AzureKeyCredential credential)
            {
                var sender = new FakeEventGridSenderClient
                {
                    SendSingle = SendSingle == null ? null : cloudEvent => SendSingle(credential.Key, cloudEvent)
                };
                Senders.Add(sender);
                return sender;
            }

            internal override EventGridReceiverClient CreateReceiverClient(Uri endpoint, string topicName, string subscriptionName, AzureKeyCredential credential)
            {
                ReceiverCreations.Add((endpoint, topicName, subscriptionName, credential.Key));
                return Receiver;
            }
        }

        private sealed class FakeEventGridSenderClient : EventGridSenderClient
        {
            public Func<CloudEvent, Task<Response>> SendSingle { get; set; }
            public List<CloudEvent> SingleEvents { get; } = new List<CloudEvent>();
            public List<IReadOnlyList<CloudEvent>> BatchEvents { get; } = new List<IReadOnlyList<CloudEvent>>();

            public override Task<Response> SendAsync(CloudEvent cloudEvent, CancellationToken cancellationToken = default)
            {
                SingleEvents.Add(cloudEvent);
                return SendSingle == null ? Task.FromResult<Response>(new FakeResponse()) : SendSingle(cloudEvent);
            }

            public override Task<Response> SendAsync(IEnumerable<CloudEvent> cloudEvents, CancellationToken cancellationToken = default)
            {
                BatchEvents.Add(cloudEvents.ToList());
                return Task.FromResult<Response>(new FakeResponse());
            }
        }

        private sealed class FakeEventGridReceiverClient : EventGridReceiverClient
        {
            public ReceiveResult NextReceiveResult { get; set; } = EventGridNamespacesModelFactory.ReceiveResult(Array.Empty<ReceiveDetails>());
            public AcknowledgeResult NextAcknowledgeResult { get; set; }
            public Exception AcknowledgeError { get; set; }
            public int AcknowledgeCalls { get; private set; }
            public ReleaseResult NextReleaseResult { get; set; }
            public RejectResult NextRejectResult { get; set; }
            public int? LastMaxEvents { get; private set; }
            public TimeSpan? LastMaxWaitTime { get; private set; }

            public override Task<Response<ReceiveResult>> ReceiveAsync(int? maxEvents = null, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
            {
                LastMaxEvents = maxEvents;
                LastMaxWaitTime = maxWaitTime;
                return Task.FromResult(Response.FromValue(NextReceiveResult, new FakeResponse()));
            }

            public override Task<Response<AcknowledgeResult>> AcknowledgeAsync(IEnumerable<string> lockTokens, CancellationToken cancellationToken = default)
            {
                AcknowledgeCalls++;
                return AcknowledgeError == null
                    ? Task.FromResult(Response.FromValue(NextAcknowledgeResult, new FakeResponse()))
                    : Task.FromException<Response<AcknowledgeResult>>(AcknowledgeError);
            }

            public override Task<Response<ReleaseResult>> ReleaseAsync(IEnumerable<string> lockTokens, ReleaseDelay? releaseDelayInSeconds = null, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(Response.FromValue(NextReleaseResult, new FakeResponse()));
            }

            public override Task<Response<RejectResult>> RejectAsync(IEnumerable<string> lockTokens, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(Response.FromValue(NextRejectResult, new FakeResponse()));
            }
        }

        private sealed class FakeResponse : Response
        {
            public override int Status => 200;
            public override string ReasonPhrase => "OK";
            public override Stream ContentStream { get; set; }
            public override string ClientRequestId { get; set; }
            public override void Dispose() { }
            protected override bool TryGetHeader(string name, out string value)
            {
                value = null;
                return false;
            }
            protected override bool TryGetHeaderValues(string name, out IEnumerable<string> values)
            {
                values = null;
                return false;
            }
            protected override bool ContainsHeader(string name) => false;
            protected override IEnumerable<HttpHeader> EnumerateHeaders() => Enumerable.Empty<HttpHeader>();
        }
    }
}
