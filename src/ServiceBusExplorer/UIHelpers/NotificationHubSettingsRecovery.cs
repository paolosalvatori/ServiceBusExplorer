using System;

namespace ServiceBusExplorer.UIHelpers
{
    internal sealed class NotificationHubSettingsRecovery
    {
        internal bool NeedsRefresh { get; private set; }

        internal void RequireRefresh() => NeedsRefresh = true;

        internal void Initialize(Action initialize)
        {
            // A failed editor reload can leave controls partly updated. Keep saving blocked until one completes.
            RequireRefresh();
            initialize();
            NeedsRefresh = false;
        }
    }
}
