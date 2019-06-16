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
using Microsoft.ServiceBus.Messaging;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    public class MessagingPurger
    {
        // Either queueDescription or subscriptWrapper is used - but never both.
        readonly QueueDescription queueDescription;
        readonly SubscriptionWrapper subscriptionWrapper;
        readonly ServiceBusHelper serviceBusHelper;

        public MessagingPurger(ServiceBusHelper serviceBusHelper, QueueDescription queueDescription)
        {
            this.serviceBusHelper = serviceBusHelper;
            this.queueDescription = queueDescription;
        }

        public MessagingPurger(ServiceBusHelper serviceBusHelper, SubscriptionWrapper subscriptionWrapper)
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
            GetEntityData(deadLetterQueueData: false,
                messageCount: out var messagesToPurgeCount,
                entityPath: out _);

            return await DoPurgeSessionEntity(messagesToPurgeCount)
                .ConfigureAwait(false);
        }

        async Task<long> DoPurgeSessionEntity(long messagesToPurgeCount)
        {
            long totalMessagesPurged = 0;
            var messagingFactory = MessagingFactory.CreateFromConnectionString(serviceBusHelper.ConnectionString);
            ClientEntity entityClient;
            if (queueDescription != null)
            {
                entityClient = messagingFactory.CreateQueueClient(queueDescription.Path, ReceiveMode.ReceiveAndDelete);
            }
            else
            {
                entityClient = messagingFactory.CreateSubscriptionClient(subscriptionWrapper.SubscriptionDescription.TopicPath,
                    subscriptionWrapper.SubscriptionDescription.Name, ReceiveMode.ReceiveAndDelete);
            }

            var consecutiveSessionTimeOuts = 0;
            try
            {
                const int enoughZeroReceives = 3;
                while (consecutiveSessionTimeOuts < enoughZeroReceives && totalMessagesPurged < messagesToPurgeCount)
                {
                    MessageSession session;

                    if (queueDescription != null)
                    {
                        // Currently unable to use the isExclusiveMode parameter because then a MessagingException starting 
                        // with the message "The service was unable to process the request; please retry the operation" is thrown.
                        session = await ((QueueClient)entityClient).AcceptMessageSessionAsync(
                            serverWaitTime: TimeSpan.FromMilliseconds(200 * (consecutiveSessionTimeOuts + 1)))
                                .ConfigureAwait(false);
                    }
                    else
                    {
                        session = await ((SubscriptionClient)entityClient).AcceptMessageSessionAsync(
                            serverWaitTime: TimeSpan.FromMilliseconds(200 * (consecutiveSessionTimeOuts + 1)))
                                .ConfigureAwait(false);
                    }

                    var consecutiveZeroBatchReceives = 0;
                    while (consecutiveZeroBatchReceives < enoughZeroReceives
                        && totalMessagesPurged < messagesToPurgeCount)
                    {
                        var messages = await session.ReceiveBatchAsync(1000, TimeSpan.FromMilliseconds(1000))
                                                    .ConfigureAwait(false);

                        if (messages.Any())
                        {
                            Interlocked.Add(ref totalMessagesPurged, messages.Count());
                            consecutiveZeroBatchReceives = 0;

                            foreach (var message in messages)
                            {
                                message.Dispose();
                            }
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
                await entityClient.CloseAsync()
                    .ConfigureAwait(false);
            }

            return totalMessagesPurged;
        }

        async Task<long> PurgeNonSessionEntity(bool purgeDeadLetterQueueInstead)
        {
            GetEntityData(purgeDeadLetterQueueInstead, out var messagesToPurgeCount, out var entityPath);

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

                GetEntityData(purgeDeadLetterQueueInstead, out messageCount, out _);
                ++retries;
            }

            return purgedMessagesCount;
        }

        void GetEntityData(bool deadLetterQueueData, out long messageCount, out string entityPath)
        {
            if (deadLetterQueueData)
            {
                if (queueDescription != null)
                {
                    var queueDescription2 = serviceBusHelper.GetQueue(queueDescription.Path);
                    messageCount = queueDescription2.MessageCountDetails.DeadLetterMessageCount;
                    entityPath = QueueClient.FormatDeadLetterPath(queueDescription.Path);
                }
                else
                {
                    var subscriptionDescription = serviceBusHelper.GetSubscription(subscriptionWrapper.TopicDescription.Path,
                        subscriptionWrapper.SubscriptionDescription.Name);
                    messageCount = subscriptionDescription.MessageCountDetails.DeadLetterMessageCount;
                    entityPath = SubscriptionClient.FormatDeadLetterPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                        subscriptionWrapper.SubscriptionDescription.Name);
                }
            }
            else
            {
                if (queueDescription != null)
                {
                    var queueDescription2 = serviceBusHelper.GetQueue(queueDescription.Path);
                    messageCount = queueDescription2.MessageCountDetails.ActiveMessageCount;
                    entityPath = queueDescription.Path;
                }
                else
                {
                    var subscriptionDescription = serviceBusHelper.GetSubscription(subscriptionWrapper.TopicDescription.Path,
                        subscriptionWrapper.SubscriptionDescription.Name);
                    messageCount = subscriptionDescription.MessageCountDetails.ActiveMessageCount;
                    entityPath = SubscriptionClient.FormatSubscriptionPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                        subscriptionWrapper.SubscriptionDescription.Name);
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
                    var messagingFactory = MessagingFactory.CreateFromConnectionString(serviceBusHelper.ConnectionString);

                    ClientEntity receiver;
                    if (queue)  // The dead letter queue for a subscription is a queue
                    {
                        receiver = await messagingFactory.CreateMessageReceiverAsync(entityPath, ReceiveMode.ReceiveAndDelete)
                           .ConfigureAwait(false);
                    }
                    else
                    {
                        receiver = messagingFactory.CreateSubscriptionClient(subscriptionWrapper.SubscriptionDescription.TopicPath,
                            subscriptionWrapper.SubscriptionDescription.Name, ReceiveMode.ReceiveAndDelete);
                    }

                    try
                    {
                        var consecutiveZeroBatchReceives = 0;
                        const int enoughZeroBatchReceives = 3;

                        while (!quit && Interlocked.Read(ref totalMessagesPurged) < messagesToPurgeCount)
                        {
                            IEnumerable<BrokeredMessage> messages;

                            if (queue)
                            {
                                messages = await ((MessageReceiver)receiver).ReceiveBatchAsync(1000,
                                   TimeSpan.FromMilliseconds(20000 * (consecutiveZeroBatchReceives + 1)))
                                   .ConfigureAwait(false);
                            }
                            else
                            {
                                messages = await ((SubscriptionClient)receiver).ReceiveBatchAsync(1000,
                                   TimeSpan.FromMilliseconds(500 * (consecutiveZeroBatchReceives + 1)))
                                   .ConfigureAwait(false);
                            }

                            // ReSharper disable once PossibleMultipleEnumeration
                            if (messages.Any())
                            {
                                // ReSharper disable once PossibleMultipleEnumeration
                                consecutiveZeroBatchReceives = 0;
                                long messageCount = messages.Count();
                                Interlocked.Add(ref totalMessagesPurged, messageCount);

                                foreach (var message in messages)
                                {
                                    message.Dispose();
                                }
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
                        await messagingFactory.CloseAsync().ConfigureAwait(false);
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
