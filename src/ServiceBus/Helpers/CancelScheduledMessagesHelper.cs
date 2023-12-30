using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    public static class CancelScheduledMessagesHelper
    {
        public static async Task CancelScheduledMessages(ServiceBusHelper2 serviceBusHelper,
            string queueName, List<long> sequenceNumbersToCancel)
        {
            var client = serviceBusHelper.CreateServiceBusClient();
            var sender = client.CreateSender(queueName);

            serviceBusHelper.WriteToLog($"Starting cancellation of scheduled messages on queue {queueName}.");

            var stopwatch = Stopwatch.StartNew();

            // Got lots of Azure.Messaging.ServiceBus.ServiceBusExceptions and the UI stopped working when testing 
            // with 1000 messages when calling using Task.WhenAll. So, doing it one by one instead.
            foreach (var sequenceNumber in sequenceNumbersToCancel)
            {
                await sender.CancelScheduledMessageAsync(sequenceNumber);
                serviceBusHelper.WriteToLog($"Cancelled scheduled message with sequence number {sequenceNumber}.");
            }

            stopwatch.Stop();

            serviceBusHelper.WriteToLog($"Cancelled {sequenceNumbersToCancel.Count} scheduled message(s) in {stopwatch.ElapsedMilliseconds / 1000} seconds.");
        }
    }
}
