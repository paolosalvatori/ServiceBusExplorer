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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    using Utilities.Helpers;

    public abstract class ServiceBusPurger<TEntity>
        where TEntity : class
    {
        protected readonly ServiceBusHelper2 serviceBusHelper;

        public event EventHandler<PurgeOperationCompletedEventArgs> PurgeCompleted;
        public event EventHandler<PurgeOperationFailedEventArgs> PurgeFailed;

        protected ServiceBusPurger(ServiceBusHelper2 serviceBusHelper)
        {
            this.serviceBusHelper = serviceBusHelper;
        }

        public async Task Purge(PurgeStrategies purgeStrategy, TEntity entity, WriteToLogDelegate writeToLog)
        {
            await this.Purge(purgeStrategy, new List<TEntity>() { entity }, writeToLog)
                .ConfigureAwait(false);
        }

        public async Task Purge(PurgeStrategies purgeStrategy, List<TEntity> entities, WriteToLogDelegate writeToLog)
        {
            foreach (TEntity subscription in entities)
            {
                if ((purgeStrategy & PurgeStrategies.Messages) == PurgeStrategies.Messages)
                {
                    await this.InternalPurge(subscription, purgeDeadLetterQueueInstead: false, writeToLog: writeToLog)
                        .ConfigureAwait(false);
                }

                if ((purgeStrategy & PurgeStrategies.DeadletteredMessages) == PurgeStrategies.DeadletteredMessages)
                {
                    await this.InternalPurge(subscription, purgeDeadLetterQueueInstead: true, writeToLog: writeToLog)
                        .ConfigureAwait(false);
                }
            }
        }

        private async Task InternalPurge(TEntity entity, bool purgeDeadLetterQueueInstead, WriteToLogDelegate writeToLog)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                long totalMessagesPurged;

                if (!purgeDeadLetterQueueInstead && this.EntityRequiresSession(entity))
                {
                    totalMessagesPurged = await this.PurgeSessionEntity(entity, writeToLog).ConfigureAwait(false);
                }
                else
                {
                    totalMessagesPurged = await this.PurgeNonSessionEntity(entity, purgeDeadLetterQueueInstead).ConfigureAwait(false);
                }

                stopwatch.Stop();

                string entityPath = this.GetEntityPath(entity);
                this.PurgeCompleted?.Invoke(this, new PurgeOperationCompletedEventArgs(entityPath, stopwatch.ElapsedMilliseconds, totalMessagesPurged, purgeDeadLetterQueueInstead));
            }
            catch (Exception ex)
            {
                this.PurgeFailed?.Invoke(this, new PurgeOperationFailedEventArgs(ex));
            }
        }

        private async Task<long> PurgeSessionEntity(TEntity entity, WriteToLogDelegate writeToLog)
        {
            const int maxSessionTimeOuts = 3;
            const int maxNoMessagesReceived = 5;
            long totalMessagesPurged = 0;
            long lastLoggedMessageCount = 0;

            var tasks = new Task[4];

            long messagesToPurgeCount = await GetMessageCount(entity, deadLetterQueueData: false)
                .ConfigureAwait(false);

            var client = new ServiceBusClient(
                serviceBusHelper.ConnectionString,
                new ServiceBusClientOptions
                {
                    RetryOptions = new ServiceBusRetryOptions
                    {
                        TryTimeout = TimeSpan.FromMilliseconds(1000),
                        MaxRetries = 0
                    },
                    TransportType = serviceBusHelper.TransportType
                });

            try
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = Task.Run(async () =>
                    {
                        int consecutiveSessionTimeOuts = 0;
                        int consecutiveNoMessagesReceived = 0;

                        while (consecutiveSessionTimeOuts < maxSessionTimeOuts
                               && consecutiveNoMessagesReceived < maxNoMessagesReceived
                               && Interlocked.Read(ref totalMessagesPurged) < messagesToPurgeCount)
                        {
                            ServiceBusSessionReceiver sessionReceiver = null;

                            try
                            {
                                sessionReceiver = await CreateServiceBusSessionReceiver(entity,
                                        client,
                                        purgeDeadLetterQueueInstead: false)
                                    .ConfigureAwait(false);

                                while (true)
                                {
                                    var messages = await sessionReceiver.ReceiveMessagesAsync(
                                            maxMessages: 1000,
                                            maxWaitTime: TimeSpan.FromMilliseconds(250 * (consecutiveNoMessagesReceived + 1)))
                                        .ConfigureAwait(false);

                                    if (messages != null && messages.Any())
                                    {
                                        consecutiveSessionTimeOuts = 0;
                                        consecutiveNoMessagesReceived = 0;
                                        Interlocked.Add(ref totalMessagesPurged, messages.Count);

                                        if (Interlocked.Read(ref totalMessagesPurged) - Interlocked.Read(ref lastLoggedMessageCount) > 100)
                                        {
                                            Interlocked.Exchange(ref lastLoggedMessageCount, Interlocked.Read(ref totalMessagesPurged));
                                            writeToLog($"[{Interlocked.Read(ref totalMessagesPurged)}] messages have been purged out of [{messagesToPurgeCount}].");
                                        }
                                    }
                                    else
                                    {
                                        ++consecutiveNoMessagesReceived;
                                        break;
                                    }
                                }
                            }
                            catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.ServiceTimeout)
                            {
                                ++consecutiveSessionTimeOuts;
                            }
                            catch
                            {
                                break;
                            }
                            finally
                            {
                                if (sessionReceiver != null)
                                    await sessionReceiver.CloseAsync().ConfigureAwait(false);
                            }
                        }
                    });
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            finally
            {
                await client.DisposeAsync().ConfigureAwait(false);
            }

            return totalMessagesPurged;
        }

        private async Task<long> PurgeNonSessionEntity(TEntity entity, bool purgeDeadLetterQueueInstead)
        {
            long messagesToPurgeCount = await GetMessageCount(entity, purgeDeadLetterQueueInstead).ConfigureAwait(false);
            long purgedMessagesCount = 0;
            long messageCount = messagesToPurgeCount;
            int retries = 0;

            // Sometimes it does not start polling or quits polling while not done
            while (purgedMessagesCount < messagesToPurgeCount && messageCount >= 1 && retries < 3)
            {
                purgedMessagesCount += await DoPurgeNonSessionEntity(entity,
                        messagesToPurgeCount: messagesToPurgeCount,
                        purgeDeadLetterSubqueueInstead: purgeDeadLetterQueueInstead)
                    .ConfigureAwait(false);

                messageCount = await GetMessageCount(entity, purgeDeadLetterQueueInstead).ConfigureAwait(false);
                ++retries;
            }

            return purgedMessagesCount;
        }

        private async Task<long> DoPurgeNonSessionEntity(TEntity entity, long messagesToPurgeCount, bool purgeDeadLetterSubqueueInstead)
        {
            long totalMessagesPurged = 0;
            int taskCount = Math.Min((int)messagesToPurgeCount / 1000 + 1, 20);
            var tasks = new Task[taskCount];

            var client = new ServiceBusClient(
                serviceBusHelper.ConnectionString,
                new ServiceBusClientOptions
                {
                    RetryOptions = new ServiceBusRetryOptions
                    {
                        TryTimeout = TimeSpan.FromMilliseconds(1000),
                        MaxRetries = 0
                    },
                    TransportType = serviceBusHelper.TransportType
                });

            try
            {
                for (int taskIndex = 0; taskIndex < tasks.Length; taskIndex++)
                {
                    tasks[taskIndex] = Task.Run(async () =>
                    {
                        var receiver = CreateServiceBusReceiver(entity, client, purgeDeadLetterSubqueueInstead);

                        try
                        {
                            int consecutiveZeroBatchReceives = 0;
                            const int enoughZeroBatchReceives = 3;

                            while (consecutiveZeroBatchReceives < enoughZeroBatchReceives
                                   && Interlocked.Read(ref totalMessagesPurged) < messagesToPurgeCount)
                            {
                                var messages = await receiver.ReceiveMessagesAsync(
                                        maxMessages: 1000,
                                        maxWaitTime: TimeSpan.FromMilliseconds(1500 * (consecutiveZeroBatchReceives + 1)))
                                    .ConfigureAwait(false);
                                
                                if (messages != null && messages.Any())
                                {
                                    consecutiveZeroBatchReceives = 0;
                                    long messageCount = messages.Count;
                                    Interlocked.Add(ref totalMessagesPurged, messageCount);
                                }
                                else
                                {
                                    ++consecutiveZeroBatchReceives;
                                }
                            }
                        }
                        finally
                        {
                            if (receiver != null)
                                await receiver.CloseAsync().ConfigureAwait(false);
                        }
                    });
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            finally
            {
                await client.DisposeAsync().ConfigureAwait(false);
            }

            return totalMessagesPurged;
        }

        protected abstract Task<long> GetMessageCount(TEntity entity, bool deadLetterQueueData);

        protected abstract bool EntityRequiresSession(TEntity entity);

        protected abstract string GetEntityPath(TEntity entity);

        protected abstract ServiceBusReceiver CreateServiceBusReceiver(TEntity entity, ServiceBusClient client, bool purgeDeadLetterQueueInstead);

        protected abstract Task<ServiceBusSessionReceiver> CreateServiceBusSessionReceiver(TEntity entity, ServiceBusClient client, bool purgeDeadLetterQueueInstead);
    }
}
