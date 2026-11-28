using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace ServiceBusExplorer.Helpers
{
    public static class SessionBrowser
    {
        internal static async Task<byte[]> ReadStateAsync(ServiceBusClient client, string entityPath,
            string subscriptionName, string sessionId, TimeSpan timeout, CancellationToken cancellationToken)
        {
            using var deadline = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var expired = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            using var registration = deadline.Token.Register(() => expired.TrySetResult(true));
            deadline.CancelAfter(timeout);
            var read = ReadAndCloseAsync(client, entityPath, subscriptionName, sessionId, deadline.Token);
            if (await Task.WhenAny(read, expired.Task).ConfigureAwait(false) == read)
            {
                try { return await read.ConfigureAwait(false); }
                catch (OperationCanceledException) when (deadline.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException("Session state inspection timed out.");
                }
            }

            // SDK disposal has no cancellation token. Let it finish releasing the lock
            // even if the browser has already reported the deadline.
            _ = read.ContinueWith(t => { _ = t.Exception; }, CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
            cancellationToken.ThrowIfCancellationRequested();
            throw new TimeoutException("Session state inspection timed out.");
        }

        private static async Task<byte[]> ReadAndCloseAsync(ServiceBusClient client, string entityPath,
            string subscriptionName, string sessionId, CancellationToken cancellationToken)
        {
            ServiceBusSessionReceiver receiver = null;
            try
            {
                var options = new ServiceBusSessionReceiverOptions
                {
                    ReceiveMode = ServiceBusReceiveMode.PeekLock
                };
                receiver = string.IsNullOrWhiteSpace(subscriptionName)
                    ? await client.AcceptSessionAsync(entityPath, sessionId, options, cancellationToken)
                        .ConfigureAwait(false)
                    : await client.AcceptSessionAsync(entityPath, subscriptionName, sessionId, options, cancellationToken)
                        .ConfigureAwait(false);

                cancellationToken.ThrowIfCancellationRequested();
                var state = await receiver.GetSessionStateAsync(cancellationToken).ConfigureAwait(false);
                return state?.ToArray();
            }
            finally
            {
                try
                {
                    if (receiver != null)
                    {
                        await receiver.DisposeAsync().ConfigureAwait(false);
                    }
                }
                finally
                {
                    await client.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

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
