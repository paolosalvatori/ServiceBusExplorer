using Azure.Messaging.ServiceBus.Administration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    public class BulkServiceBusPurger
    {
        private readonly ServiceBusHelper2 serviceBusHelper;

        public event EventHandler<PurgeOperationCompletedEventArgs> PurgeCompleted;
        public event EventHandler<PurgeOperationFailedEventArgs> PurgeFailed;

        public BulkServiceBusPurger(ServiceBusHelper2 serviceBusHelper)
        {
            this.serviceBusHelper = serviceBusHelper;
        }

        public async Task PurgeSubscriptions(BulkPurgeStrategy bulkPurgeStrategy, List<SubscriptionProperties> subscriptions)
        {
            foreach (SubscriptionProperties subscription in subscriptions)
            {
                if ((bulkPurgeStrategy & BulkPurgeStrategy.Messages) == BulkPurgeStrategy.Messages)
                {
                    await this.PurgeSubscription(subscription, false);
                }

                if ((bulkPurgeStrategy & BulkPurgeStrategy.DeadletteredMessages) == BulkPurgeStrategy.DeadletteredMessages)
                {
                    await this.PurgeSubscription(subscription, true);
                }
            }
        }

        private async Task PurgeSubscription(SubscriptionProperties subscriptionProperties, bool purgeDeadLetterQueueInstead)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                ServiceBusPurger purger = new ServiceBusPurger(this.serviceBusHelper, subscriptionProperties);
                long totalMessagesPurged = await purger.Purge(purgeDeadLetterQueueInstead);

                stopwatch.Stop();

                this.PurgeCompleted?.Invoke(this, new PurgeOperationCompletedEventArgs($"{subscriptionProperties.TopicName}/subscriptions/{subscriptionProperties.SubscriptionName}", stopwatch.ElapsedMilliseconds, totalMessagesPurged, purgeDeadLetterQueueInstead));
            }
            catch (Exception ex)
            {
                this.PurgeFailed?.Invoke(this, new PurgeOperationFailedEventArgs(ex));
            }
        }

        public async Task PurgeQueues(BulkPurgeStrategy bulkPurgeStrategy, List<QueueProperties> queues)
        {
            foreach (QueueProperties queue in queues)
            {
                if ((bulkPurgeStrategy & BulkPurgeStrategy.Messages) == BulkPurgeStrategy.Messages)
                {
                    await this.PurgeQueue(queue, false);
                }

                if ((bulkPurgeStrategy & BulkPurgeStrategy.DeadletteredMessages) == BulkPurgeStrategy.DeadletteredMessages)
                {
                    await this.PurgeQueue(queue, true);
                }
            }
        }

        private async Task PurgeQueue(QueueProperties queueProperties, bool purgeDeadLetterQueueInstead)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                ServiceBusPurger purger = new ServiceBusPurger(this.serviceBusHelper, queueProperties);
                long totalMessagesPurged = await purger.Purge(purgeDeadLetterQueueInstead);

                stopwatch.Stop();

                this.PurgeCompleted?.Invoke(this, new PurgeOperationCompletedEventArgs(queueProperties.Name, stopwatch.ElapsedMilliseconds, totalMessagesPurged, purgeDeadLetterQueueInstead));
            }
            catch (Exception ex)
            {
                this.PurgeFailed?.Invoke(this, new PurgeOperationFailedEventArgs(ex));
            }
        }
    }

    public class PurgeOperationCompletedEventArgs : EventArgs
    {
        public bool IsDeadLetterQueue { get; set; }
        public string EntityPath { get; set; }
        public long ElapsedMilliseconds { get; set; }
        public long TotalMessagesPurged { get; set; }

        public PurgeOperationCompletedEventArgs(string entityPath, long elapsedMilliseconds, long totalMessagesPurged, bool isDeadLetterQueue)
        {
            this.EntityPath = entityPath;
            this.ElapsedMilliseconds = elapsedMilliseconds;
            this.TotalMessagesPurged = totalMessagesPurged;
            this.IsDeadLetterQueue = isDeadLetterQueue;
        }
    }

    public class PurgeOperationFailedEventArgs : EventArgs
    {
        public Exception Exception { get; set; }

        public PurgeOperationFailedEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }

    [Flags]
    public enum BulkPurgeStrategy
    {
        Messages = 1,
        DeadletteredMessages = 2,
        All = Messages | DeadletteredMessages
    }
}
