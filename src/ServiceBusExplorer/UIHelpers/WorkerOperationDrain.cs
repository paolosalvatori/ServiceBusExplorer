using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace ServiceBusExplorer.UIHelpers
{
    internal static class WorkerOperationDrain
    {
        internal static Task Start(Func<AsyncCallback, IAsyncResult> start, AsyncCallback callback, Action<Exception> startFailure = null)
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
            catch (Exception ex) { try { startFailure?.Invoke(ex); } finally { completion.TrySetException(ex); } }
            return completion.Task;
        }

        internal static void Complete(Action endInvoke, Action<Exception> reportFailure, Action finished)
        {
            try { endInvoke(); }
            catch (Exception ex) { try { reportFailure(ex); } catch { } }
            finally { finished(); }
        }

        internal static async Task RunManagerAsync(Control owner, ManualResetEventSlim completed,
            CancellationToken cancellationToken, Func<Task> finalUiWork)
        {
            try { await Task.Run(() => completed.Wait(cancellationToken)).ConfigureAwait(false); }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { return; }
            if (cancellationToken.IsCancellationRequested) return;
            var completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            try
            {
                owner.BeginInvoke(new Action(async () =>
                {
                    try
                    {
                        if (!cancellationToken.IsCancellationRequested && !owner.IsDisposed && !owner.Disposing)
                            await finalUiWork();
                        completion.TrySetResult(true);
                    }
                    catch (Exception ex) { completion.TrySetException(ex); }
                }));
            }
            catch (InvalidOperationException) when (owner.IsDisposed || owner.Disposing) { return; }
            // Once UI work starts, cancellation must still drain it before client disposal.
            await completion.Task.ConfigureAwait(false);
        }

        // Include EndInvoke and callback cleanup, not just the delegate's wait handle.
        // Awaiting leaves the UI available to worker Invoke calls until all workers finish.
        internal static Task DrainAsync(IEnumerable<Task> operations) => Task.WhenAll(operations);
    }
}
