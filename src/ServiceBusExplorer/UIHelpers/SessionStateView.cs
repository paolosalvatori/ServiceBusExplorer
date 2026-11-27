using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.UIHelpers
{
    internal sealed class SessionStateView
    {
        private readonly Action<string> display;
        private readonly Action<Exception> reportFailure;
        private readonly Func<bool> canDisplay;
        private int selection;

        internal SessionStateView(Action<string> display, Action<Exception> reportFailure, Func<bool> canDisplay)
        {
            this.display = display;
            this.reportFailure = reportFailure;
            this.canDisplay = canDisplay;
        }

        // GetMessageSessions returns a browsable handle with its own SDK operation timeout.
        internal Task ShowAsync(MessageSession selected) =>
            ReadAsync(selected == null ? null : new Func<Task<Stream>>(selected.GetStateAsync));

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
