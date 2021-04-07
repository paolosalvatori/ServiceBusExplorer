using Azure.Messaging.ServiceBus.Administration;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.ServiceBus.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceBusExplorer.Common
{
    public class BulkServiceBusPurger
    {
        private readonly ServiceBusHelper serviceBusHelper;

        public event EventHandler<PurgeOperationCompletedEventArgs> PurgeCompleted;

        public BulkServiceBusPurger(ServiceBusHelper serviceBusHelper)
        {
            this.serviceBusHelper = serviceBusHelper;
        }

        public async Task PurgeSubscriptions(BulkPurgeStrategy bulkPurgeStrategy, List<SubscriptionWrapper> subscriptions)
        {
            foreach (SubscriptionWrapper subscription in subscriptions)
            {
                if ((bulkPurgeStrategy & BulkPurgeStrategy.Messages) == BulkPurgeStrategy.Messages)
                {
                    await this.Purge(subscription, false);
                }

                if ((bulkPurgeStrategy & BulkPurgeStrategy.DeadletteredMessages) == BulkPurgeStrategy.DeadletteredMessages)
                {
                    await this.Purge(subscription, true);
                }
            }
        }

        private async Task Purge(SubscriptionWrapper subscription, bool purgeDeadLetterQueueInstead)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SubscriptionProperties subscriptionProperties = await serviceBusHelper.GetSubscriptionProperties(subscription);

            ServiceBusPurger purger = new ServiceBusPurger(this.serviceBusHelper.GetServiceBusHelper2(), subscriptionProperties);
            long totalMessagesPurged = await purger.Purge(purgeDeadLetterQueueInstead);

            stopwatch.Stop();

            string entityPath = SubscriptionClient.FormatSubscriptionPath(subscription.SubscriptionDescription.TopicPath,
                                                           subscription.SubscriptionDescription.Name);

            if (this.PurgeCompleted != null)
                this.PurgeCompleted(this, new PurgeOperationCompletedEventArgs(entityPath, stopwatch.ElapsedMilliseconds, totalMessagesPurged, purgeDeadLetterQueueInstead));
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

    [Flags]
    public enum BulkPurgeStrategy
    {
        Messages = 1,
        DeadletteredMessages = 2,
        All = Messages | DeadletteredMessages
    }
}
