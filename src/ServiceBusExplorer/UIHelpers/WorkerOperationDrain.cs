using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusExplorer.UIHelpers
{
    internal static class WorkerOperationDrain
    {
        internal static Task Start(Func<AsyncCallback, IAsyncResult> start, AsyncCallback callback)
        {
            var completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            try
            {
                start(result =>
                {
                    try { callback(result); completion.TrySetResult(true); }
                    catch (Exception ex) { completion.TrySetException(ex); }
                });
            }
            catch (Exception ex) { completion.TrySetException(ex); }
            return completion.Task;
        }

        // Include EndInvoke and callback cleanup, not just the delegate's wait handle.
        // Awaiting leaves the UI available to worker Invoke calls until all workers finish.
        internal static Task DrainAsync(IEnumerable<Task> operations) => Task.WhenAll(operations);
    }
}
