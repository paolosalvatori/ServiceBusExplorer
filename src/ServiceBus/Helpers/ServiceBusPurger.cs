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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.Management;
using System.Threading.Tasks;
using System.Threading;

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    public class ServiceBusPurger
    {
        // Either queueDescription or subscriptWrapper is used - but never both.
        readonly QueueDescription queueDescription;
        readonly SubscriptionWrapper2 subscriptionWrapper;
        readonly ServiceBusHelper2 serviceBusHelper;

        public ServiceBusPurger(ServiceBusHelper2 serviceBusHelper, QueueDescription queueDescription)
        {
            this.serviceBusHelper = serviceBusHelper;
            this.queueDescription = queueDescription;
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
                totalMessagesPurged = await PurgeNonSessionEntity(purgeDeadLetterQueueInstead: purgeDeadLetterQueueInstead).ConfigureAwait(false);
            }

            return totalMessagesPurged;
        }

        async Task<long> PurgeSessionEntity()
        {
            long messagesToPurgeCount = await GetMessageCount(deadLetterQueueData: false);

            return await DoPurgeSessionEntity(messagesToPurgeCount).ConfigureAwait(false);
        }

        async Task<long> DoPurgeSessionEntity(long messagesToPurgeCount)
        {
            long totalMessagesPurged = 0;

            ISessionClient sessionClient = new SessionClient(
                serviceBusHelper.ConnectionString, 
                GetEntityPath(deadLetterQueue: false),
                null,
                receiveMode: ReceiveMode.ReceiveAndDelete, 
                retryPolicy: RetryPolicy.Default, 
                prefetchCount: 10,
                transportType: serviceBusHelper.TransportType);

            var consecutiveSessionTimeOuts = 0;
            try
            {
                const int enoughZeroReceives = 3;
                while (consecutiveSessionTimeOuts < enoughZeroReceives && totalMessagesPurged < messagesToPurgeCount)
                {
                    IMessageSession session = await sessionClient.AcceptMessageSessionAsync();

                    var consecutiveZeroBatchReceives = 0;
                    while (consecutiveZeroBatchReceives < enoughZeroReceives
                        && totalMessagesPurged < messagesToPurgeCount)
                    {
                        var messages = await session.ReceiveAsync(1000, TimeSpan.FromMilliseconds(1000))
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

                    await session.CloseAsync().ConfigureAwait(false);
                }
            }
            catch (TimeoutException)
            {
                ++consecutiveSessionTimeOuts;
            }
            finally
            {
                await sessionClient.CloseAsync().ConfigureAwait(false);
            }

            return totalMessagesPurged;
        }

        async Task<long> PurgeNonSessionEntity(bool purgeDeadLetterQueueInstead)
        {
            var entityPath = GetEntityPath(purgeDeadLetterQueueInstead);
            long messagesToPurgeCount = await GetMessageCount(purgeDeadLetterQueueInstead);
            long purgedMessagesCount = 0;
            var messageCount = messagesToPurgeCount;
            var retries = 0;

            // Sometimes it does not start polling or quits polling while not done
            while (purgedMessagesCount < messagesToPurgeCount && messageCount >= 1 && retries < 3)
            {
                purgedMessagesCount += await DoPurgeNonSessionEntity(
                    queue: purgeDeadLetterQueueInstead ? true : queueDescription != null,
                    messagesToPurgeCount: messagesToPurgeCount,
                    entityPath: entityPath)
                    .ConfigureAwait(false);

                messageCount = await GetMessageCount(purgeDeadLetterQueueInstead);
                ++retries;
            }

            return purgedMessagesCount;
        }

        string GetEntityPath(bool deadLetterQueue)
        {
            if (deadLetterQueue)
            {
                if (queueDescription != null)
                {
                    return EntityNameHelper.FormatDeadLetterPath(queueDescription.Path);
                }
                else
                {
                    var subscriptionPath = EntityNameHelper.FormatSubscriptionPath(subscriptionWrapper.TopicDescription.Path,
                        subscriptionWrapper.SubscriptionDescription.SubscriptionName);
                    return EntityNameHelper.FormatDeadLetterPath(subscriptionPath);
                }
            }
            else
            {
                if (queueDescription != null)
                {
                    return queueDescription.Path;
                }
                else
                {
                    return EntityNameHelper.FormatSubscriptionPath(subscriptionWrapper.TopicDescription.Path,
                        subscriptionWrapper.SubscriptionDescription.SubscriptionName);
                }
            }
        }

        async Task<long> GetMessageCount(bool deadLetterQueueData)
        {
            var client = new ManagementClient(serviceBusHelper.ConnectionString);

            if (deadLetterQueueData)
            {
                if (queueDescription != null)
                {
                    var runtimeInfo = await client.GetQueueRuntimeInfoAsync(queueDescription.Path);

                    return runtimeInfo.MessageCountDetails.DeadLetterMessageCount;
                }
                else
                {
                    var runtimeInfo = await client.GetSubscriptionRuntimeInfoAsync(subscriptionWrapper.TopicDescription.Path,
                        subscriptionWrapper.SubscriptionDescription.SubscriptionName);

                    return runtimeInfo.MessageCountDetails.DeadLetterMessageCount;
                }
            }
            else
            {
                if (queueDescription != null)
                {
                    var runtimeInfo = await client.GetQueueRuntimeInfoAsync(queueDescription.Path);

                    return runtimeInfo.MessageCountDetails.ActiveMessageCount;
                }
                else
                {
                    var runtimeInfo = await client.GetSubscriptionRuntimeInfoAsync(subscriptionWrapper.TopicDescription.Path,
                                subscriptionWrapper.SubscriptionDescription.SubscriptionName);

                    return runtimeInfo.MessageCountDetails.ActiveMessageCount;
                }
            }
        }

        async Task<long> DoPurgeNonSessionEntity(bool queue, long messagesToPurgeCount, string entityPath)
        {
            long totalMessagesPurged = 0;
            var taskCount = Math.Min((int)messagesToPurgeCount / 1000 + 1, 20);
            var tasks = new Task[taskCount];
            var quit = false;  // This instance controls all the receiving tasks

            for (var taskIndex = 0; taskIndex < tasks.Length; taskIndex++)
            {
                tasks[taskIndex] = Task.Run(async () =>
                {
                    ClientEntity receiver;

                    receiver = new MessageReceiver(serviceBusHelper.ConnectionString, entityPath,
                        ReceiveMode.ReceiveAndDelete, RetryPolicy.Default, prefetchCount: 50);

                    try
                    {
                        var consecutiveZeroBatchReceives = 0;
                        const int enoughZeroBatchReceives = 3;

                        while (!quit && Interlocked.Read(ref totalMessagesPurged) < messagesToPurgeCount)
                        {
                            IEnumerable<Message> messages;

                            messages = await ((MessageReceiver)receiver).ReceiveAsync(1000,
                               TimeSpan.FromMilliseconds(20000 * (consecutiveZeroBatchReceives + 1)))
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
                        await receiver.CloseAsync().ConfigureAwait(false);
                    }
                });  // End of lambda 
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return totalMessagesPurged;
        }

        bool EntityRequiresSession()
        {
            if (queueDescription != null)
            {
                return queueDescription.RequiresSession;
            }

            return subscriptionWrapper.SubscriptionDescription.RequiresSession;
        }
    }
}
