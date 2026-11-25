using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ManagementEnumerationTests
    {
        [Theory]
        [InlineData("Name Eq 'topic7' And MessageCount Gt 0", "topic7", true)]
        [InlineData("Name Eq 'topic7' And MessageCount Gt 0", "topic8", false)]
        [InlineData("Name Eq 'topic7' Or MessageCount Gt 0", "topic8", true)]
        [InlineData("startswith(Path, 'topic') Eq true", "topic8", true)]
        [InlineData("Name Eq 'a and b'", "a and b", true)]
        public void EarlyPathConditionPreservesBooleanAndQuotedStringSemantics(string filter, string path, bool expected)
        {
            ServiceBusManagementFilter.ParsePathCondition(filter)(path).Should().Be(expected);
        }

        [Fact]
        public void TopicNameFilterSkipsExcludedCountersAndJoinsAllRuntimePages()
        {
            var sdk = new EnumerationClient();
            var result = Client(sdk).GetTopics("Name Eq 'topic7'", 5).ToList();
            result.Select(item => item.Path).Should().Equal("topic7");
            result[0].MessageCountDetails.ActiveMessageCount.Should().Be(8);
            result[0].MessageCountDetails.ScheduledMessageCount.Should().Be(3);
            sdk.TopicLists.Should().Be(1);
            sdk.TopicRuntimeLists.Should().Be(1);
            sdk.TopicPointReads.Should().Be(0);
            sdk.SubscriptionTopics.Should().Equal("topic7");
        }

        [Fact]
        public void ExcludedTopicsNeedNoRuntimeRequests()
        {
            var sdk = new EnumerationClient();
            Client(sdk).GetTopics("Path Eq 'missing'", 5).Should().BeEmpty();
            sdk.TopicRuntimeLists.Should().Be(0);
            sdk.SubscriptionTopics.Should().BeEmpty();
        }

        [Fact]
        public void RuntimeAlternativeDoesNotIncorrectlyPruneTopicsAndConcurrencyIsBounded()
        {
            var sdk = new EnumerationClient();
            Client(sdk).GetTopics("Name Eq 'missing' Or MessageCount Gt 0", 5).Should().HaveCount(9);
            sdk.MaximumActive.Should().Be(4);
            sdk.Active.Should().Be(0);
            sdk.TopicRuntimeLists.Should().Be(1);
            sdk.TopicPointReads.Should().Be(0);
            sdk.SubscriptionTopics.Should().HaveCount(9).And.OnlyHaveUniqueItems();
        }

        [Fact]
        public void TimeoutCancelsAndDrainsEveryStartedCounterRead()
        {
            var sdk = new EnumerationClient { Delay = 10000 };
            ((Action)(() => Client(sdk).GetTopics(null, 1).ToList())).Should().Throw<TimeoutException>();
            sdk.Active.Should().Be(0);
            sdk.SubscriptionTopics.Should().HaveCount(4);
        }

        [Fact]
        public void CounterFailureDrainsPeersAndDoesNotStartLaterBatch()
        {
            var sdk = new EnumerationClient { FailTopic = "topic0" };
            ((Action)(() => Client(sdk).GetTopics(null, 5).ToList())).Should().Throw<InvalidOperationException>();
            sdk.Active.Should().Be(0);
            sdk.SubscriptionTopics.Should().HaveCount(4);
        }

        [Fact]
        public void SubscriptionPagesAreJoinedByNameWithoutPerSubscriptionReads()
        {
            var sdk = new EnumerationClient();
            var result = Client(sdk).GetSubscriptions("topic0", "MessageCount Gt 0").ToList();
            result.Should().HaveCount(100);
            result.Single(item => item.Name == "sub73").MessageCount.Should().Be(74);
            sdk.SubscriptionTopics.Should().Equal("topic0");
            sdk.SubscriptionPointReads.Should().Be(0);
        }

        [Fact]
        public void MissingRuntimeSnapshotUsesOnlyTheMissingEntityFallback()
        {
            var sdk = new EnumerationClient { OmitRuntime = true };
            Client(sdk).GetTopics("Name Eq 'topic7'", 5).Should().ContainSingle();
            sdk.TopicPointReads.Should().Be(1);
            Client(sdk).GetSubscriptions("topic0").Should().HaveCount(100);
            sdk.SubscriptionPointReads.Should().Be(1);
        }

        private static ServiceBusManagementClient Client(EnumerationClient sdk) =>
            new ServiceBusManagementClient(sdk, new Uri("sb://synthetic.invalid/"));

        private sealed class EnumerationClient : ServiceBusAdministrationClient
        {
            public int TopicLists, TopicRuntimeLists, TopicPointReads, SubscriptionPointReads;
            public int Active, MaximumActive;
            public int Delay = 30;
            public bool OmitRuntime;
            public string FailTopic;
            public readonly List<string> SubscriptionTopics = new List<string>();
            private static AsyncPageable<T> Pages<T>(IEnumerable<T> items) where T : notnull => AsyncPageable<T>.FromPages(
                items.Select((item, index) => Page<T>.FromValues(new[] { item }, "page" + index, null)));
            public override AsyncPageable<TopicProperties> GetTopicsAsync(CancellationToken cancellationToken = default)
            {
                TopicLists++;
                return Pages(Enumerable.Range(0, 9).Select(i => ServiceBusModelFactory.TopicProperties("topic" + i, maxSizeInMegabytes: 1024,
                    defaultMessageTimeToLive: TimeSpan.MaxValue, autoDeleteOnIdle: TimeSpan.MaxValue,
                    duplicateDetectionHistoryTimeWindow: TimeSpan.FromMinutes(10), status: EntityStatus.Active)));
            }
            public override AsyncPageable<TopicRuntimeProperties> GetTopicsRuntimePropertiesAsync(CancellationToken cancellationToken = default)
            {
                TopicRuntimeLists++;
                return Pages(Enumerable.Range(0, 9).Where(i => !OmitRuntime || i != 7).Reverse()
                    .Select(i => ServiceBusModelFactory.TopicRuntimeProperties("TOPIC" + i, scheduledMessageCount: 3)));
            }
            public override Task<Response<TopicRuntimeProperties>> GetTopicRuntimePropertiesAsync(string name, CancellationToken cancellationToken = default)
            {
                TopicPointReads++;
                return Task.FromResult(Response.FromValue(ServiceBusModelFactory.TopicRuntimeProperties(name), (Response)null));
            }
            public override AsyncPageable<SubscriptionProperties> GetSubscriptionsAsync(string topic, CancellationToken cancellationToken = default) =>
                Pages(Enumerable.Range(0, 100).Select(i => ServiceBusModelFactory.SubscriptionProperties(topic, "sub" + i, lockDuration: TimeSpan.FromMinutes(1),
                    defaultMessageTimeToLive: TimeSpan.MaxValue, autoDeleteOnIdle: TimeSpan.MaxValue,
                    maxDeliveryCount: 10, status: EntityStatus.Active, userMetadata: "")));
            public override Task<Response<SubscriptionRuntimeProperties>> GetSubscriptionRuntimePropertiesAsync(string topic, string name, CancellationToken cancellationToken = default)
            {
                SubscriptionPointReads++;
                return Task.FromResult(Response.FromValue(ServiceBusModelFactory.SubscriptionRuntimeProperties(topic, name, totalMessageCount: 74), (Response)null));
            }
            public override AsyncPageable<SubscriptionRuntimeProperties> GetSubscriptionsRuntimePropertiesAsync(string topic, CancellationToken cancellationToken = default)
            {
                lock (SubscriptionTopics) SubscriptionTopics.Add(topic);
                return new RuntimePages(this, topic, cancellationToken);
            }
            private sealed class RuntimePages : AsyncPageable<SubscriptionRuntimeProperties>
            {
                private readonly EnumerationClient owner;
                private readonly string topic;
                public RuntimePages(EnumerationClient owner, string topic, CancellationToken token) : base(token)
                { this.owner = owner; this.topic = topic; }
                public override async IAsyncEnumerable<Page<SubscriptionRuntimeProperties>> AsPages(string continuationToken = null, int? pageSizeHint = null)
                {
                    var active = Interlocked.Increment(ref owner.Active);
                    lock (owner.SubscriptionTopics) owner.MaximumActive = Math.Max(owner.MaximumActive, active);
                    try
                    {
                        await Task.Delay(owner.Delay, CancellationToken).ConfigureAwait(false);
                        if (topic == owner.FailTopic) throw new InvalidOperationException("Synthetic counter failure");
                        for (var i = 99; i >= 0; i--)
                        {
                            if (owner.OmitRuntime && i == 73) continue;
                            yield return Page<SubscriptionRuntimeProperties>.FromValues(new[] {
                                ServiceBusModelFactory.SubscriptionRuntimeProperties(topic, "SUB" + i,
                                    totalMessageCount: i + 1, activeMessageCount: i == 0 ? 8 : 0)
                            }, i == 0 ? null : "next", null);
                        }
                    }
                    finally { Interlocked.Decrement(ref owner.Active); }
                }
            }
        }
    }
}
