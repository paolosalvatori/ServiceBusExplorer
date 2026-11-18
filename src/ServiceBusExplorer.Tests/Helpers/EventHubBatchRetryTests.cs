using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventHubBatchRetryTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task LaterGroupFailureNeverReplaysAcknowledgedGroups(bool permanentFailure)
        {
            var producer = new FailingProducer(permanentFailure);
            var logs = new List<string>();
            var counts = new List<long>();
            var elapsed = new List<long>();
            var helper = new ServiceBusHelper((message, async) => logs.Add(message));
            var wallTime = Stopwatch.StartNew();
            using (var first = new EventDataEnvelope(new MemoryStream(new byte[] { 1 })) { PartitionKey = "first" })
            using (var second = new EventDataEnvelope(new MemoryStream(new byte[] { 2 })) { PartitionKey = "second" })
            using (var cancellation = new CancellationTokenSource())
            {
                long next = 0;
                Func<Task> send = () => helper.SendEventData(producer, new[] { first, second }, () => next++, 2, 0,
                    updatePartitionKey: false, noPartitionKey: false, addMessageNumber: true,
                    logging: true, verbose: true, statistics: true, sendBatch: true, batchSize: 2,
                    senderThinkTime: false, thinkTime: 0, eventDataInspector: null, updateStatistics: (count, time, direction) => { counts.Add(count); elapsed.Add(time); },
                    cancellationTokenSource: cancellation);
                if (permanentFailure)
                {
                    var failure = await send.Should().ThrowAsync<Exception>().WithMessage("*Simulated later group failure*");
                    failure.Which.Message.Should().Contain("Message Count=[1]");
                }
                else
                {
                    await send();
                }
            }
            wallTime.Stop();
            counts.Sum().Should().Be(permanentFailure ? 1 : 2);
            // The failed attempt includes 150 ms of work, as well as RetryHelper's backoff.
            (wallTime.ElapsedMilliseconds - elapsed.Sum()).Should().BeGreaterThanOrEqualTo(150);
            logs.Should().Contain(message => message.Contains("EventData sent. MessageNumber=[0] PartitionKey=[first]"));
            logs.Count(message => message.Contains("EventData sent. MessageNumber=[1]")).Should().Be(permanentFailure ? 0 : 1);
            producer.Attempts.Count(key => key == "first").Should().Be(1);
            producer.Attempts.Count(key => key == "second").Should().BeGreaterThan(1);
            producer.Acknowledged.Should().Equal(permanentFailure ? new[] { "first" } : new[] { "first", "second" });
        }

        private sealed class FailingProducer : EventHubProducerClient
        {
            private readonly bool permanentFailure;
            public List<string> Attempts { get; } = new List<string>();
            public List<string> Acknowledged { get; } = new List<string>();

            public FailingProducer(bool permanentFailure) { this.permanentFailure = permanentFailure; }

            public override Task SendAsync(IEnumerable<EventData> events, SendEventOptions options,
                CancellationToken cancellationToken = default)
            {
                // Materialize to ensure the same, nonempty payload survives a retry.
                events.Single().EventBody.ToArray().Should().HaveCount(1);
                Attempts.Add(options.PartitionKey);
                if (options.PartitionKey == "second" &&
                    (permanentFailure || Attempts.Count(key => key == "second") == 1))
                {
                    Thread.Sleep(150);
                    throw new TimeoutException("Simulated later group failure");
                }
                Acknowledged.Add(options.PartitionKey);
                return Task.CompletedTask;
            }
        }
    }
}
