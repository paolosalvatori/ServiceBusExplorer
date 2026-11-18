using System;
using System.Collections.Generic;
using System.IO;
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
            var helper = new ServiceBusHelper((message, async) => { });
            using (var first = new EventDataEnvelope(new MemoryStream(new byte[] { 1 })) { PartitionKey = "first" })
            using (var second = new EventDataEnvelope(new MemoryStream(new byte[] { 2 })) { PartitionKey = "second" })
            using (var cancellation = new CancellationTokenSource())
            {
                long next = 0;
                Func<Task> send = () => helper.SendEventData(producer, new[] { first, second }, () => next++, 2, 0,
                    updatePartitionKey: false, noPartitionKey: false, addMessageNumber: true,
                    logging: false, verbose: false, statistics: false, sendBatch: true, batchSize: 2,
                    senderThinkTime: false, thinkTime: 0, eventDataInspector: null, updateStatistics: null,
                    cancellationTokenSource: cancellation);
                if (permanentFailure)
                {
                    await send.Should().ThrowAsync<Exception>().WithMessage("*Simulated later group failure*");
                }
                else
                {
                    await send();
                }
            }
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
                    throw new TimeoutException("Simulated later group failure");
                }
                Acknowledged.Add(options.PartitionKey);
                return Task.CompletedTask;
            }
        }
    }
}
