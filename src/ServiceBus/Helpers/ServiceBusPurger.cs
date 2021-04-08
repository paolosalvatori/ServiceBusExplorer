#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    internal class OldServiceBusPurger
    {
        // Either queueProperties or subscriptProperties is used - but never both.
        readonly QueueProperties queueProperties;
        readonly SubscriptionProperties subscriptionProperties;
        readonly ServiceBusHelper2 serviceBusHelper;

        public OldServiceBusPurger(ServiceBusHelper2 serviceBusHelper, QueueProperties queueProperties)
        {
            this.serviceBusHelper = serviceBusHelper;
            this.queueProperties = queueProperties;
        }

        public OldServiceBusPurger(ServiceBusHelper2 serviceBusHelper, SubscriptionProperties subscriptionProperties)
        {
            this.serviceBusHelper = serviceBusHelper;
            this.subscriptionProperties = subscriptionProperties;
        }

        /// <summary>
        /// Purges the messages from a queue, subscription or a dead letter queue. Handles all kinds of queues.
        /// </summary>
        /// <param name="purgeDeadLetterQueueInstead">If false it will purge the queue, if true it will purge the 
        /// dead letter queue instead.</param>
        /// <returns>The number of messages purged</returns>
        public async Task<long> Purge(bool purgeDeadLetterQueueInstead = false)
        {
            long totalMessagesPurged;

            if (!purgeDeadLetterQueueInstead && EntityRequiresSession())
            {
                totalMessagesPurged = await PurgeSessionEntity().ConfigureAwait(false);
            }
            else
            {
                totalMessagesPurged = await PurgeNonSessionEntity(
                    purgeDeadLetterQueueInstead: purgeDeadLetterQueueInstead).ConfigureAwait(false);
            }

            return totalMessagesPurged;
        }

        ServiceBusReceiver CreateServiceBusReceiver(ServiceBusClient client,
            bool purgeDeadLetterQueueInstead)
        {
            if (queueProperties != null)
            {
                return client.CreateReceiver(
                    queueProperties.Name,
                    new ServiceBusReceiverOptions
                    {
                        PrefetchCount = 50,
                        ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete,
                        SubQueue = purgeDeadLetterQueueInstead ? SubQueue.DeadLetter : SubQueue.None
                    });
            }
            else
            {
                return client.CreateReceiver(
                    subscriptionProperties.TopicName,
                    subscriptionProperties.SubscriptionName,
                    new ServiceBusReceiverOptions
                    {
                        PrefetchCount = 50,
                        ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete,
                        SubQueue = purgeDeadLetterQueueInstead ? SubQueue.DeadLetter : SubQueue.None
                    });
            }
        }

        async Task<ServiceBusSessionReceiver> CreateServiceBusSessionReceiver(ServiceBusClient client,
            bool purgeDeadLetterQueueInstead)
        {
            if (queueProperties != null)
            {
                return await client.AcceptNextSessionAsync(
                    queueProperties.Name,
                    new ServiceBusSessionReceiverOptions
                    {
                        PrefetchCount = 10,
                        ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
                    })
                    .ConfigureAwait(false);
            }
            else
            {
                return await client.AcceptNextSessionAsync(
                    subscriptionProperties.TopicName,
                    subscriptionProperties.SubscriptionName,
                    new ServiceBusSessionReceiverOptions
                    {
                        PrefetchCount = 10,
                        ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
                    })
                    .ConfigureAwait(false);
            }
        }

        async Task<long> PurgeSessionEntity()
        {
            long totalMessagesPurged = 0;
            var consecutiveSessionTimeOuts = 0;
            ServiceBusSessionReceiver sessionReceiver = null;
            long messagesToPurgeCount = await GetMessageCount(deadLetterQueueData: false)
                .ConfigureAwait(false);

            var client = new ServiceBusClient(
              serviceBusHelper.ConnectionString,
              new ServiceBusClientOptions
              {
                  TransportType = serviceBusHelper.TransportType
              });

            try
            {
                const int enoughZeroReceives = 3;

                while (consecutiveSessionTimeOuts < enoughZeroReceives && totalMessagesPurged < messagesToPurgeCount)
                {
                    sessionReceiver = await CreateServiceBusSessionReceiver(
                        client,
                        purgeDeadLetterQueueInstead: false)
                        .ConfigureAwait(false);

                    var consecutiveZeroBatchReceives = 0;

                    while (consecutiveZeroBatchReceives < enoughZeroReceives
                        && totalMessagesPurged < messagesToPurgeCount)
                    {
                        var messages = await sessionReceiver.ReceiveMessagesAsync(
                            maxMessages: 1000,
                            maxWaitTime: TimeSpan.FromMilliseconds(1000))
                            .ConfigureAwait(false);

                        if (messages != null && messages.Any())
                        {
                            Interlocked.Add(ref totalMessagesPurged, messages.Count());
                            consecutiveZeroBatchReceives = 0;
                        }
                        else
                        {
                            ++consecutiveZeroBatchReceives;
                        }
                    }

                    await sessionReceiver.CloseAsync().ConfigureAwait(false);
                }
            }
            catch (TimeoutException)
            {
                ++consecutiveSessionTimeOuts;
            }
            finally
            {
                await client.DisposeAsync().ConfigureAwait(false);
            }

            return totalMessagesPurged;
        }

        async Task<long> PurgeNonSessionEntity(bool purgeDeadLetterQueueInstead)
        {
            long messagesToPurgeCount = await GetMessageCount(purgeDeadLetterQueueInstead).ConfigureAwait(false);
            long purgedMessagesCount = 0;
            var messageCount = messagesToPurgeCount;
            var retries = 0;

            // Sometimes it does not start polling or quits polling while not done
            while (purgedMessagesCount < messagesToPurgeCount && messageCount >= 1 && retries < 3)
            {
                purgedMessagesCount += await DoPurgeNonSessionEntity(
                    messagesToPurgeCount: messagesToPurgeCount,
                    purgeDeadLetterSubqueueInstead: purgeDeadLetterQueueInstead
                    )
                    .ConfigureAwait(false);

                messageCount = await GetMessageCount(purgeDeadLetterQueueInstead).ConfigureAwait(false);
                ++retries;
            }

            return purgedMessagesCount;
        }

        async Task<long> GetMessageCount(bool deadLetterQueueData)
        {
            var client = new ServiceBusAdministrationClient(serviceBusHelper.ConnectionString);

            if (deadLetterQueueData)
            {
                if (queueProperties != null)
                {
                    var runtimeInfoResponse = await client.GetQueueRuntimePropertiesAsync(queueProperties.Name)
                        .ConfigureAwait(false);

                    return runtimeInfoResponse.Value.DeadLetterMessageCount;
                }
                else
                {
                    var runtimeInfoResponse = await client.GetSubscriptionRuntimePropertiesAsync(
                        subscriptionProperties.TopicName,
                        subscriptionProperties.SubscriptionName)
                        .ConfigureAwait(false);

                    return runtimeInfoResponse.Value.DeadLetterMessageCount;
                }
            }
            else
            {
                if (queueProperties != null)
                {
                    var runtimeInfo = await client.GetQueueRuntimePropertiesAsync(queueProperties.Name)
                        .ConfigureAwait(false);

                    return runtimeInfo.Value.ActiveMessageCount;
                }
                else
                {
                    var runtimeInfo = await client.GetSubscriptionRuntimePropertiesAsync(
                        subscriptionProperties.TopicName,
                        subscriptionProperties.SubscriptionName)
                        .ConfigureAwait(false);

                    return runtimeInfo.Value.ActiveMessageCount;
                }
            }
        }

        async Task<long> DoPurgeNonSessionEntity(long messagesToPurgeCount, bool purgeDeadLetterSubqueueInstead)
        {
            long totalMessagesPurged = 0;
            var taskCount = Math.Min((int)messagesToPurgeCount / 1000 + 1, 20);
            var tasks = new Task[taskCount];
            var quit = false;  // This instance controls all the receiving tasks

            var client = new ServiceBusClient(
                serviceBusHelper.ConnectionString,
                new ServiceBusClientOptions { TransportType = serviceBusHelper.TransportType });

            try
            {
                for (var taskIndex = 0; taskIndex < tasks.Length; taskIndex++)
                {
                    tasks[taskIndex] = Task.Run(async () =>
                    {
                        ServiceBusReceiver receiver = CreateServiceBusReceiver(
                            client,
                            purgeDeadLetterSubqueueInstead);

                        try
                        {
                            var consecutiveZeroBatchReceives = 0;
                            const int enoughZeroBatchReceives = 3;

                            while (!quit && Interlocked.Read(ref totalMessagesPurged) < messagesToPurgeCount)
                            {
                                IEnumerable<ServiceBusReceivedMessage> messages;

                                messages = await receiver.ReceiveMessagesAsync(
                                    maxMessages: 1000,
                                    maxWaitTime: TimeSpan.FromMilliseconds(20000 * (consecutiveZeroBatchReceives + 1)))
                                   .ConfigureAwait(false);

                                // ReSharper disable once PossibleMultipleEnumeration
                                if (messages != null && messages.Any())
                                {
                                    // ReSharper disable once PossibleMultipleEnumeration
                                    long messageCount = messages.Count();
                                    Interlocked.Add(ref totalMessagesPurged, messageCount);
                                }
                                else
                                {
                                    ++consecutiveZeroBatchReceives;
                                    if (consecutiveZeroBatchReceives >= enoughZeroBatchReceives)
                                        quit = true;
                                }
                            }
                        }
                        finally
                        {
                            if (null != receiver)
                            {
                                await receiver.CloseAsync().ConfigureAwait(false);
                            }
                        }
                    });  // End of lambda 
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            finally
            {
                await client.DisposeAsync().ConfigureAwait(false);
            }

            return totalMessagesPurged;
        }

        bool EntityRequiresSession()
        {
            if (queueProperties != null)
            {
                return queueProperties.RequiresSession;
            }

            return subscriptionProperties.RequiresSession;
        }
    }

    public class ServiceBusPurger
    {
        private readonly ServiceBusHelper2 serviceBusHelper;

        public event EventHandler<PurgeOperationCompletedEventArgs> PurgeCompleted;
        public event EventHandler<PurgeOperationFailedEventArgs> PurgeFailed;

        public ServiceBusPurger(ServiceBusHelper2 serviceBusHelper)
        {
            this.serviceBusHelper = serviceBusHelper;
        }

        public async Task PurgeSubscription(PurgeStrategy purgeStrategy, SubscriptionProperties subscription)
        {
            await this.PurgeSubscriptions(purgeStrategy, new List<SubscriptionProperties>() { subscription });
        }

        public async Task PurgeSubscriptions(PurgeStrategy purgeStrategy, List<SubscriptionProperties> subscriptions)
        {
            foreach (SubscriptionProperties subscription in subscriptions)
            {
                if ((purgeStrategy & PurgeStrategy.Messages) == PurgeStrategy.Messages)
                {
                    await this.InternalPurgeSubscription(subscription, false);
                }

                if ((purgeStrategy & PurgeStrategy.DeadletteredMessages) == PurgeStrategy.DeadletteredMessages)
                {
                    await this.InternalPurgeSubscription(subscription, true);
                }
            }
        }

        private async Task InternalPurgeSubscription(SubscriptionProperties subscriptionProperties, bool purgeDeadLetterQueueInstead)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                OldServiceBusPurger purger = new OldServiceBusPurger(this.serviceBusHelper, subscriptionProperties);
                long totalMessagesPurged = await purger.Purge(purgeDeadLetterQueueInstead);

                stopwatch.Stop();

                this.PurgeCompleted?.Invoke(this, new PurgeOperationCompletedEventArgs($"{subscriptionProperties.TopicName}/subscriptions/{subscriptionProperties.SubscriptionName}", stopwatch.ElapsedMilliseconds, totalMessagesPurged, purgeDeadLetterQueueInstead));
            }
            catch (Exception ex)
            {
                this.PurgeFailed?.Invoke(this, new PurgeOperationFailedEventArgs(ex));
            }
        }

        public async Task PurgeQueue(PurgeStrategy purgeStrategy, QueueProperties queue)
        {
            await this.PurgeQueues(purgeStrategy, new List<QueueProperties>() { queue });
        }

        public async Task PurgeQueues(PurgeStrategy purgeStrategy, List<QueueProperties> queues)
        {
            foreach (QueueProperties queue in queues)
            {
                if ((purgeStrategy & PurgeStrategy.Messages) == PurgeStrategy.Messages)
                {
                    await this.InternalPurgeQueue(queue, false);
                }

                if ((purgeStrategy & PurgeStrategy.DeadletteredMessages) == PurgeStrategy.DeadletteredMessages)
                {
                    await this.InternalPurgeQueue(queue, true);
                }
            }
        }

        private async Task InternalPurgeQueue(QueueProperties queueProperties, bool purgeDeadLetterQueueInstead)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                OldServiceBusPurger purger = new OldServiceBusPurger(this.serviceBusHelper, queueProperties);
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
    public enum PurgeStrategy
    {
        Messages = 1,
        DeadletteredMessages = 2,
        All = Messages | DeadletteredMessages
    }
}
