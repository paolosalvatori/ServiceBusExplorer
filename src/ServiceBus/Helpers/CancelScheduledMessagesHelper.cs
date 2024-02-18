using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;

using ServiceBusExplorer.Utilities.Helpers;

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    public static class CancelScheduledMessagesHelper
    {
        class PassIntToTask
        {
            public int Count; 
        }

        public static async Task CancelScheduledMessages(ServiceBusHelper2 serviceBusHelper,
            string queueName, List<long> sequenceNumbersToCancel)
        {
            var client = serviceBusHelper.CreateServiceBusClient();

            try
            {
                var sender = client.CreateSender(queueName);

                serviceBusHelper.WriteToLog($"Starting cancellation of scheduled messages on queue {queueName}.");

                var successes = new PassIntToTask();
                var failures = new PassIntToTask();

                var stopwatch = Stopwatch.StartNew();
                var semaphore = new SemaphoreSlim(40); // As recommended by https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-transfers-locks-settlement#settling-send-operations
                var tasks = new List<Task>(sequenceNumbersToCancel.Count);

                foreach (long sequenceNumber in sequenceNumbersToCancel)
                {
                    await semaphore.WaitAsync();
                    tasks.Add(CancelScheduledMessageWithLog(sender, sequenceNumber, serviceBusHelper.WriteToLog, successes, failures)
                        .ContinueWith((t, state) => ((SemaphoreSlim)state)?.Release(), semaphore));
                }

                await Task.WhenAll(tasks);
                stopwatch.Stop();

                Func<int, string> singleOrPlural = (c) => c > 1 ? "messages" : "message";


                serviceBusHelper.WriteToLog($"Successfully cancelled {successes.Count} " +
                    $"scheduled {singleOrPlural(successes.Count)} in {stopwatch.Elapsed}.");

                if (failures.Count > 0)
                {
                    serviceBusHelper.WriteToLog($"Failed to cancel {failures.Count} " +
                        $"{singleOrPlural(failures.Count)}.");
                }
            }
            finally
            {
                await client.DisposeAsync();
            }
        }

        static Task CancelScheduledMessageWithLog(ServiceBusSender sender,
            long sequenceNumber, WriteToLogDelegate writeToLog, PassIntToTask successes, PassIntToTask failures)
        {
            Task task = sender.CancelScheduledMessageAsync(sequenceNumber)
                .ContinueWith(_ =>
                    {
                        writeToLog($"Cancelled scheduled message with sequence number {sequenceNumber}.");
                        Interlocked.Increment(ref successes.Count);
                    },
                    TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously)
                .ContinueWith(_ =>
                    {
                        writeToLog($"Failed to cancel scheduled message with sequence number {sequenceNumber}.");
                        Interlocked.Increment(ref failures.Count);
                    },
                    TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

            return task;
        }
    }
}
