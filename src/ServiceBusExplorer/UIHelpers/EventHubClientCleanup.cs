using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;

namespace ServiceBusExplorer.UIHelpers
{
    internal static class EventHubClientCleanup
    {
        internal static Task DisposeProducersAsync(IEnumerable<EventHubProducerClient> clients)
        {
            // Async lambdas capture synchronous disposal failures too, so every client is attempted.
            return Task.WhenAll(clients.Select(async client => await client.DisposeAsync().ConfigureAwait(false)));
        }

        internal static async Task DisposeConsumerAfterReadersAsync(EventHubConsumerClient client, IEnumerable<Task> readers)
        {
            try { await Task.WhenAll(readers).ConfigureAwait(false); }
            finally { await client.DisposeAsync().ConfigureAwait(false); }
        }
    }
}
