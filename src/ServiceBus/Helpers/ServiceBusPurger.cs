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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    public class ServiceBusPurger
    {
        // Either queueProperties or subscriptWrapper is used - but never both.
        readonly QueueProperties queueProperties;
        readonly SubscriptionWrapper2 subscriptionWrapper;
        readonly ServiceBusHelper2 serviceBusHelper;

        public ServiceBusPurger(ServiceBusHelper2 serviceBusHelper, QueueProperties queueProperties)
        {
            this.serviceBusHelper = serviceBusHelper;
            this.queueProperties = queueProperties;
        }

        public ServiceBusPurger(ServiceBusHelper2 serviceBusHelper, SubscriptionWrapper2 subscriptionWrapper)
        {
            this.serviceBusHelper = serviceBusHelper;
            this.subscriptionWrapper = subscriptionWrapper;
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

        async Task<ServiceBusReceiver> CreateServiceBusReceiver(ServiceBusClient client,
            bool purgeDeadLetterQueueInstead)
        {
            ServiceBusReceiver receiver = null;

            if (!purgeDeadLetterQueueInstead && EntityRequiresSession())
            {
                // Create SessionReceiver

                if (queueProperties != null)
                {
                    receiver = await client.AcceptNextSessionAsync(
                        queueProperties.Name,
                        new ServiceBusSessionReceiverOptions
                        {
                            PrefetchCount = 10,
                            ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
                        });
                }
                else
                {
                    receiver = await client.AcceptNextSessionAsync(
                        subscriptionWrapper.SubscriptionProperties.TopicName,
                        subscriptionWrapper.SubscriptionProperties.SubscriptionName,

                        new ServiceBusSessionReceiverOptions
                        {
                            PrefetchCount = 10,
                            ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
                        });
                }
            }
            else
            {
                // Create normal Receiver
                if (queueProperties != null)
                {
                    receiver = client.CreateReceiver(
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
                    receiver = client.CreateReceiver(
                        subscriptionWrapper.SubscriptionProperties.TopicName,
                        subscriptionWrapper.SubscriptionProperties.SubscriptionName,
                        new ServiceBusReceiverOptions
                        {
                            PrefetchCount = 50,
                            ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete,
                            SubQueue = purgeDeadLetterQueueInstead ? SubQueue.DeadLetter : SubQueue.None
                        });
                }
            }

            return receiver;
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
                    sessionReceiver = (ServiceBusSessionReceiver)await
                        CreateServiceBusReceiver(client, purgeDeadLetterQueueInstead: false)
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

                messageCount = await GetMessageCount(purgeDeadLetterQueueInstead);
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
                        subscriptionWrapper.SubscriptionProperties.TopicName,
                        subscriptionWrapper.SubscriptionProperties.SubscriptionName)
                        .ConfigureAwait(false);

                    return runtimeInfoResponse.Value.DeadLetterMessageCount;
                }
            }
            else
            {
                if (queueProperties != null)
                {
                    var runtimeInfo = await client.GetQueueRuntimePropertiesAsync(queueProperties.Name);

                    return runtimeInfo.Value.ActiveMessageCount;
                }
                else
                {
                    var runtimeInfo = await client.GetSubscriptionRuntimePropertiesAsync(subscriptionWrapper.TopicProperties.Name,
                                subscriptionWrapper.SubscriptionProperties.SubscriptionName);

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
                        ServiceBusReceiver receiver = await CreateServiceBusReceiver(
                            client,
                            purgeDeadLetterSubqueueInstead)
                            .ConfigureAwait(false);

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

            return subscriptionWrapper.SubscriptionProperties.RequiresSession;
        }
    }
}
