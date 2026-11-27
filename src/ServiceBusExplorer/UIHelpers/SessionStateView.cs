using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.UIHelpers
{
    internal sealed class SessionStateView
    {
        private readonly Action<string> display;
        private readonly Action<Exception> reportFailure;
        private readonly Func<bool> canDisplay;
        private readonly Dictionary<string, Task<byte[]>> pendingReads = new Dictionary<string, Task<byte[]>>(StringComparer.Ordinal);
        private int selection;

        internal SessionStateView(Action<string> display, Action<Exception> reportFailure, Func<bool> canDisplay)
        {
            this.display = display;
            this.reportFailure = reportFailure;
            this.canDisplay = canDisplay;
        }

        // Listing is lock-free; only explicit selection reads state with a short-lived receiver.
        internal Task ReadModernAsync(SessionInfo selected, Func<string, Task<byte[]>> readState) =>
            ReadAsync(selected == null ? null : new Func<Task<Stream>>(async () =>
            {
                var state = await ReadStateAsync(selected.SessionId, readState);
                return state == null ? null : new MemoryStream(state, false);
            }));

        private async Task<byte[]> ReadStateAsync(string sessionId, Func<string, Task<byte[]>> readState)
        {
            // Selection and its continuations run on the UI thread. Share only the
            // outstanding SDK operation; a later selection must see fresh state.
            if (pendingReads.TryGetValue(sessionId, out var read) && !read.IsCompleted)
                return await read;

            read = readState(sessionId);
            pendingReads[sessionId] = read;
            try
            {
                return await read;
            }
            finally
            {
                if (pendingReads.TryGetValue(sessionId, out var current) && current == read)
                    pendingReads.Remove(sessionId);
            }
        }

        internal async Task ReadAsync(Func<Task<Stream>> readSelectedState)
        {
            var request = ++selection;
            if (!canDisplay()) return;
            display(string.Empty);
            if (readSelectedState == null) return;

            try
            {
                using (var stream = await readSelectedState())
                {
                    if (stream == null || !IsCurrent()) return;
                    using (var reader = new StreamReader(stream))
                    {
                        var text = await reader.ReadToEndAsync();
                        if (IsCurrent()) display(text);
                    }
                }
            }
            catch (Exception ex)
            {
                if (IsCurrent()) reportFailure(ex);
            }

            bool IsCurrent() => request == selection && canDisplay();
        }
    }
}
