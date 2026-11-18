using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventHubCheckpointBatchTests
    {
        [Fact]
        public async Task FlushesLastSuccessfulEventAtCountAndOnStop()
        {
            var saved = new List<long>();
            var batch = new EventHubCheckpointBatch(3, TimeSpan.FromHours(1), lease =>
            { saved.Add(lease.SequenceNumber); return Task.CompletedTask; });
            await batch.ProcessedAsync(LeaseAt(1), true);
            await batch.ProcessedAsync(LeaseAt(2), true);
            saved.Should().BeEmpty();
            await batch.ProcessedAsync(LeaseAt(3), true);
            saved.Should().Equal(3);
            await batch.ProcessedAsync(LeaseAt(4), true);
            // Event 5 fails inspection, so it is never passed to ProcessedAsync.
            await batch.FlushAsync(true);
            await batch.FlushAsync(true);
            saved.Should().Equal(3, 4);
        }

        [Fact]
        public async Task IdleTimeoutFlushesPartialBatchAndDisablingDiscardsPending()
        {
            var now = DateTimeOffset.UtcNow;
            var saved = new List<long>();
            var batch = new EventHubCheckpointBatch(100, TimeSpan.FromSeconds(5), lease =>
            { saved.Add(lease.SequenceNumber); return Task.CompletedTask; }, () => now);
            await batch.ProcessedAsync(LeaseAt(1), true);
            now += TimeSpan.FromSeconds(4);
            await batch.FlushIfDueAsync(true);
            saved.Should().BeEmpty();
            now += TimeSpan.FromSeconds(1);
            await batch.FlushIfDueAsync(true);
            saved.Should().Equal(1);
            await batch.ProcessedAsync(LeaseAt(2), true);
            await batch.FlushIfDueAsync(false);
            await batch.FlushAsync(true);
            saved.Should().Equal(1);
            await batch.ProcessedAsync(LeaseAt(3), false);
            await batch.FlushAsync(true);
            saved.Should().Equal(1);
        }

        [Fact]
        public async Task FailedFlushRetainsSuccessfulPositionForStopRetry()
        {
            var attempts = 0;
            var batch = new EventHubCheckpointBatch(1, TimeSpan.FromSeconds(5), lease =>
            {
                lease.SequenceNumber.Should().Be(42);
                if (++attempts == 1) throw new InvalidOperationException("checkpoint failure");
                return Task.CompletedTask;
            });
            Func<Task> flush = () => batch.ProcessedAsync(LeaseAt(42), true);
            await flush.Should().ThrowAsync<InvalidOperationException>();
            await batch.FlushAsync(true);
            attempts.Should().Be(2);
        }

        private static Lease LeaseAt(long sequence) => new Lease
        { PartitionId = "0", SequenceNumber = sequence, Offset = sequence.ToString() };
    }
}
