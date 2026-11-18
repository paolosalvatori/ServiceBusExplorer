using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    // One instance per reader. Only successfully processed events may advance its checkpoint.
    public sealed class EventHubCheckpointBatch
    {
        private readonly int batchSize;
        private readonly TimeSpan interval;
        private readonly Func<Lease, Task> checkpoint;
        private readonly Func<DateTimeOffset> utcNow;
        private DateTimeOffset lastFlush;
        private Lease pending;
        private int count;

        public EventHubCheckpointBatch(int batchSize, TimeSpan interval, Func<Lease, Task> checkpoint,
            Func<DateTimeOffset> utcNow = null)
        {
            this.batchSize = Math.Max(1, batchSize);
            this.interval = interval;
            this.checkpoint = checkpoint;
            this.utcNow = utcNow ?? (() => DateTimeOffset.UtcNow);
            lastFlush = this.utcNow();
        }

        public Task ProcessedAsync(Lease lease, bool enabled)
        {
            if (!enabled) return FlushAsync(false);
            pending = lease;
            count++;
            return FlushIfDueAsync(true);
        }

        public Task FlushIfDueAsync(bool enabled)
        {
            return !enabled || count >= batchSize || utcNow() - lastFlush >= interval
                ? FlushAsync(enabled) : Task.CompletedTask;
        }

        public async Task FlushAsync(bool enabled)
        {
            if (enabled && pending != null)
            {
                await checkpoint(pending).ConfigureAwait(false);
            }
            pending = null;
            count = 0;
            lastFlush = utcNow();
        }
    }
}
