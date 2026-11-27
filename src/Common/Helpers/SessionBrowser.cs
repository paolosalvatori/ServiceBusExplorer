using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace ServiceBusExplorer.Helpers
{
    public static class SessionBrowser
    {
        // Takes ownership of this operation's client; no session receiver is opened.
        public static async Task<List<SessionInfo>> ListAsync(ServiceBusClient client,
            string entityPath, string subscriptionName, CancellationToken cancellationToken)
        {
            try
            {
                var sessions = subscriptionName == null
                    ? client.GetMessageSessionsAsync(entityPath, cancellationToken)
                    : client.GetMessageSessionsAsync(entityPath, subscriptionName, cancellationToken);
                var result = new List<SessionInfo>();
                await foreach (var sessionId in sessions.ConfigureAwait(false))
                    result.Add(new SessionInfo(sessionId));
                return result;
            }
            finally
            {
                await client.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}
