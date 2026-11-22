using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBusExplorer.UIHelpers
{
    // Registered listeners include controls whose asynchronous disposal is still flushing checkpoints.
    // Registration and close requests run on the UI thread; awaiting them never blocks that thread.
    internal sealed class ListenerShutdownCoordinator
    {
        internal static ListenerShutdownCoordinator Shared { get; } = new ListenerShutdownCoordinator();
        private readonly HashSet<Func<Task>> listeners = new HashSet<Func<Task>>();
        private Task closeTask;

        internal bool CanClose => closeTask?.Status == TaskStatus.RanToCompletion;
        internal void Register(Func<Task> shutdown) => listeners.Add(shutdown);
        internal void Unregister(Func<Task> shutdown) => listeners.Remove(shutdown);

        internal Task StopAndPersistAsync(Action persist, Action<Exception> report = null)
        {
            if (closeTask == null || closeTask.IsFaulted || closeTask.IsCanceled)
            {
                closeTask = StopAndPersistCoreAsync(listeners.ToArray(), persist, report);
            }
            return closeTask;
        }

        internal void StopForSystemShutdown(Action persist, Action<Exception> report)
        {
            // WM_QUERYENDSESSION must not wait on UI continuations or cancel Windows shutdown.
            // Request cancellation, observe errors, then save the last completed checkpoint snapshot.
            foreach (var shutdown in listeners.ToArray())
            {
                try
                {
                    var task = shutdown();
                    _ = task.ContinueWith(t => { var observed = t.Exception; },
                        TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                }
                catch (Exception ex) { report(ex); }
            }
            try { persist(); }
            catch (Exception ex) { report(ex); }
        }

        private static async Task StopAndPersistCoreAsync(IEnumerable<Func<Task>> shutdowns, Action persist, Action<Exception> report)
        {
            // Capture synchronous failures too, ensuring all listeners are asked to stop.
            await Task.WhenAll(shutdowns.Select(async shutdown => await shutdown()));
            try { persist(); }
            catch (Exception ex) { report?.Invoke(ex); }
        }
    }
}
