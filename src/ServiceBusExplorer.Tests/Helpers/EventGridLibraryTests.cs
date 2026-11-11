using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using Azure.ResourceManager.EventGrid.Models;
using EventGridExplorerLibrary;
using FluentAssertions;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventGridLibraryTests
    {
        [Fact]
        public async Task PublishEventAsync_And_PublishEventsAsync_DelegateToSenderClient()
        {
            var logs = new List<string>();
            var library = new EventGridLibrary("00000000-0000-0000-0000-000000000000", null, 30, string.Empty, (msg, _) => logs.Add(msg));
            var fakeSender = new FakeEventGridSenderClient();
            GetSenderClients(library)["orders"] = fakeSender;

            await library.PublishEventAsync("orders", "/shop/orders", "Order.Created", "{\"id\":42}");
            await library.PublishEventsAsync("orders", "/shop/orders", "Order.Updated", new List<string> { "one", "two" });

            fakeSender.SingleEvents.Should().ContainSingle();
            fakeSender.SingleEvents[0].Source.Should().Be("/shop/orders");
            fakeSender.SingleEvents[0].Type.Should().Be("Order.Created");

            fakeSender.BatchEvents.Should().ContainSingle();
            fakeSender.BatchEvents[0].Select(e => e.Type).Should().Equal("Order.Updated", "Order.Updated");
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(45, 45)]
        [InlineData(500, 120)]
        public async Task ReceiveEventsAsync_ClampsWaitTime_AndReturnsDetails(int configuredWaitSeconds, int expectedWaitSeconds)
        {
            var library = new EventGridLibrary("00000000-0000-0000-0000-000000000000", null, configuredWaitSeconds, string.Empty, (_, __) => { });
            var cloudEvent = new CloudEvent("/source", "Sample.Event", "payload");
            var details = EventGridNamespacesModelFactory.ReceiveDetails(
                EventGridNamespacesModelFactory.BrokerProperties("lock-1", 1),
                cloudEvent);
            var fakeReceiver = new FakeEventGridReceiverClient
            {
                NextReceiveResult = EventGridNamespacesModelFactory.ReceiveResult(new[] { details })
            };
            GetReceiverClients(library)[("orders", "sub-a")] = fakeReceiver;

            var result = await library.ReceiveEventsAsync("orders", "sub-a", 5);

            result.Should().NotBeNull();
            result.Details.Should().ContainSingle();
            result.Details[0].BrokerProperties.LockToken.Should().Be("lock-1");
            result.Details[0].Event.Id.Should().Be(cloudEvent.Id);
            fakeReceiver.LastMaxEvents.Should().Be(5);
            fakeReceiver.LastMaxWaitTime.Should().Be(TimeSpan.FromSeconds(expectedWaitSeconds));
        }

        [Fact]
        public void ReceiveEventsAsync_LazilyCreatesAndCachesReceiverClientPerSubscription()
        {
            var library = new EventGridLibrary("00000000-0000-0000-0000-000000000000", null, 20, string.Empty, (_, __) => { });
            GetTopicCredentials(library)["orders"] = (new Uri("https://example.westus-1.eventgrid.azure.net"), new AzureKeyCredential("test-key"));

            var getReceiverMethod = typeof(EventGridLibrary).GetMethod("GetReceiverClient", BindingFlags.Instance | BindingFlags.NonPublic);
            getReceiverMethod.Should().NotBeNull();

            var subAClient1 = (EventGridReceiverClient)getReceiverMethod.Invoke(library, new object[] { "orders", "sub-a" });
            var subAClient2 = (EventGridReceiverClient)getReceiverMethod.Invoke(library, new object[] { "orders", "sub-a" });
            var subBClient = (EventGridReceiverClient)getReceiverMethod.Invoke(library, new object[] { "orders", "sub-b" });

            subAClient1.Should().NotBeNull();
            subAClient2.Should().BeSameAs(subAClient1);
            subBClient.Should().NotBeSameAs(subAClient1);
        }

        [Fact]
        public async Task EventActionsAsync_HandlesAcknowledgeReleaseAndReject()
        {
            var logs = new List<string>();
            var library = new EventGridLibrary("00000000-0000-0000-0000-000000000000", null, 30, string.Empty, (msg, _) => logs.Add(msg));
            var fakeReceiver = new FakeEventGridReceiverClient
            {
                NextAcknowledgeResult = EventGridNamespacesModelFactory.AcknowledgeResult(
                    Array.Empty<FailedLockToken>(),
                    new[] { "tok-1" }),
                NextReleaseResult = EventGridNamespacesModelFactory.ReleaseResult(
                    Array.Empty<FailedLockToken>(),
                    new[] { "tok-2" }),
                NextRejectResult = EventGridNamespacesModelFactory.RejectResult(
                    new[] { EventGridNamespacesModelFactory.FailedLockToken("tok-3", new ResponseError("ExpiredLock", "Token expired")) },
                    Array.Empty<string>())
            };
            GetReceiverClients(library)[("orders", "sub-a")] = fakeReceiver;

            var ackSucceeded = await library.EventActionsAsync("Acknowledge", new List<string> { "tok-1" }, "orders", "sub-a", (msg, _) => logs.Add(msg));
            var releaseSucceeded = await library.EventActionsAsync("Release", new List<string> { "tok-2" }, "orders", "sub-a", (msg, _) => logs.Add(msg));
            var rejectSucceeded = await library.EventActionsAsync("Reject", new List<string> { "tok-3" }, "orders", "sub-a", (msg, _) => logs.Add(msg));

            ackSucceeded.Should().BeTrue();
            releaseSucceeded.Should().BeTrue();
            rejectSucceeded.Should().BeFalse();
            logs.Should().Contain(l => l.Contains("ExpiredLock"));
            logs.Should().Contain(l => l.Contains("Token expired"));
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

        private static Dictionary<string, EventGridSenderClient> GetSenderClients(EventGridLibrary library)
        {
            var field = typeof(EventGridLibrary).GetField("senderClients", BindingFlags.Instance | BindingFlags.NonPublic);
            field.Should().NotBeNull();
            return (Dictionary<string, EventGridSenderClient>)field.GetValue(library);
        }

        private static Dictionary<(string TopicName, string SubscriptionName), EventGridReceiverClient> GetReceiverClients(EventGridLibrary library)
        {
            var field = typeof(EventGridLibrary).GetField("receiverClients", BindingFlags.Instance | BindingFlags.NonPublic);
            field.Should().NotBeNull();
            return (Dictionary<(string TopicName, string SubscriptionName), EventGridReceiverClient>)field.GetValue(library);
        }

        private static Dictionary<string, (Uri Endpoint, AzureKeyCredential Credential)> GetTopicCredentials(EventGridLibrary library)
        {
            var field = typeof(EventGridLibrary).GetField("topicCredentials", BindingFlags.Instance | BindingFlags.NonPublic);
            field.Should().NotBeNull();
            return (Dictionary<string, (Uri Endpoint, AzureKeyCredential Credential)>)field.GetValue(library);
        }

        private sealed class FakeEventGridSenderClient : EventGridSenderClient
        {
            public List<CloudEvent> SingleEvents { get; } = new List<CloudEvent>();
            public List<IReadOnlyList<CloudEvent>> BatchEvents { get; } = new List<IReadOnlyList<CloudEvent>>();

            public override Task<Response> SendAsync(CloudEvent cloudEvent, CancellationToken cancellationToken = default)
            {
                SingleEvents.Add(cloudEvent);
                return Task.FromResult<Response>(new FakeResponse());
            }

            public override Task<Response> SendAsync(IEnumerable<CloudEvent> cloudEvents, CancellationToken cancellationToken = default)
            {
                BatchEvents.Add(cloudEvents.ToList());
                return Task.FromResult<Response>(new FakeResponse());
            }
        }

        private sealed class FakeEventGridReceiverClient : EventGridReceiverClient
        {
            public ReceiveResult NextReceiveResult { get; set; }
            public AcknowledgeResult NextAcknowledgeResult { get; set; }
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
                return Task.FromResult(Response.FromValue(NextAcknowledgeResult, new FakeResponse()));
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
