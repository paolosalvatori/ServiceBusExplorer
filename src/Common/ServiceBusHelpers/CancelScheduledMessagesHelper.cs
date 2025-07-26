using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;
using Common.Contracts;
using ServiceBusExplorer.Utilities.Helpers;

namespace Common.ServiceBusHelpers
{
    public static class CancelScheduledMessagesHelper
    {
        class OperationStatus
        {
            public int Successes;
            public int Failures;
        }

        public static async Task CancelScheduledMessages(IServiceBusService serviceBusHelper,
            string queueName, List<long> sequenceNumbersToCancel)
        {
            ServiceBusSender sender = null; 
            try
            {
                sender = serviceBusHelper.CreateSender(queueName);

                serviceBusHelper.WriteToLog($"Starting cancellation of scheduled messages on queue {queueName}.");

                var operationStatus = new OperationStatus();

                var stopwatch = Stopwatch.StartNew();
                var semaphore = new SemaphoreSlim(40); // As recommended by https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-transfers-locks-settlement#settling-send-operations
                var tasks = new List<Task>(sequenceNumbersToCancel.Count);

                foreach (long sequenceNumber in sequenceNumbersToCancel)
                {
                    await semaphore.WaitAsync();
                    tasks.Add(CancelScheduledMessageWithLog(sender, sequenceNumber, serviceBusHelper.WriteToLog, operationStatus)
                        .ContinueWith((t, state) => ((SemaphoreSlim)state)?.Release(), semaphore));
                }

                await Task.WhenAll(tasks);
                stopwatch.Stop();

                Func<int, string> singleOrPlural = (c) => c > 1 ? "messages" : "message";

                serviceBusHelper.WriteToLog($"Successfully cancelled {operationStatus.Successes} " +
                    $"scheduled {singleOrPlural(operationStatus.Successes)} in {stopwatch.Elapsed}.");

                if (operationStatus.Failures > 0)
                {
                    serviceBusHelper.WriteToLog($"Failed to cancel {operationStatus.Failures} " +
                        $"{singleOrPlural(operationStatus.Failures)}.");
                }
            }
            finally
            {
                if (sender != null) await sender.DisposeAsync();
            }
        }

        static Task CancelScheduledMessageWithLog(ServiceBusSender sender,
            long sequenceNumber, WriteToLogDelegate writeToLog, OperationStatus operationStatus)
        {
            Task task = sender.CancelScheduledMessageAsync(sequenceNumber)
                .ContinueWith(_ =>
                    {
                        writeToLog($"Cancelled scheduled message with sequence number {sequenceNumber}.");
                        Interlocked.Increment(ref operationStatus.Successes);
                    },
                    TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously)
                .ContinueWith(_ =>
                    {
                        writeToLog($"Failed to cancel scheduled message with sequence number {sequenceNumber}.");
                        Interlocked.Increment(ref operationStatus.Failures);
                    },
                    TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

            return task;
        }
    }
}
