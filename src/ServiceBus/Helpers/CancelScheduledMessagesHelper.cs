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
        public static async Task CancelScheduledMessages(ServiceBusHelper2 serviceBusHelper,
            string queueName, List<long> sequenceNumbersToCancel)
        {
            var client = serviceBusHelper.CreateServiceBusClient();

            try
            {
                var sender = client.CreateSender(queueName);

                serviceBusHelper.WriteToLog($"Starting cancellation of scheduled messages on queue {queueName}.");

                var stopwatch = Stopwatch.StartNew();
                var semaphore = new SemaphoreSlim(40); // As recommended by https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-transfers-locks-settlement#settling-send-operations
                var tasks = new List<Task>(sequenceNumbersToCancel.Count);

                foreach (long sequenceNumber in sequenceNumbersToCancel)
                {
                    await semaphore.WaitAsync();
                    tasks.Add(CancelScheduledMessageWithLogAsync(sender, sequenceNumber, serviceBusHelper.WriteToLog)
                        .ContinueWith((t) => semaphore.Release()));
                }

                await Task.WhenAll(tasks);
                stopwatch.Stop();
                Func<string> singleOrPlural = () => sequenceNumbersToCancel.Count() > 1 ? "messages": "message";
                serviceBusHelper.WriteToLog($"Cancelled {sequenceNumbersToCancel.Count} scheduled {singleOrPlural()} in {stopwatch.Elapsed}.");
            }
            finally
            {
               await client.DisposeAsync();
            }
        }

        static async Task CancelScheduledMessageWithLogAsync(ServiceBusSender sender, 
            long sequenceNumber, WriteToLogDelegate writeToLog)
        {
            var task = sender.CancelScheduledMessageAsync(sequenceNumber).ContinueWith(
               (t, state) => writeToLog($"Cancelled scheduled message with sequence number {state}."),
                sequenceNumber);
            return task;
        }
    }
}
