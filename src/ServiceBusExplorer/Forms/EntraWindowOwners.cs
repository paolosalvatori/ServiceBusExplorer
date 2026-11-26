using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.Forms
{
    // Only Explorer's main and tool windows register. The list belongs to their UI thread.
    internal sealed class EntraWindowOwners
    {
        private static readonly List<EntraWindowOwners> activationOrder = new List<EntraWindowOwners>();
        private IntPtr handle;

        internal static void Register(Form form, bool mainWindow = false)
        {
            var owner = new EntraWindowOwners();
            void Publish() => EntraCredentialFactory.SetOwnerWindowHandle(CurrentHandle());

            void HandleCreated(object sender, EventArgs args)
            {
                owner.handle = form.Handle;
                if (mainWindow && !activationOrder.Contains(owner)) activationOrder.Insert(0, owner);
                Publish();
            }

            form.HandleCreated += HandleCreated;
            form.Activated += (sender, args) =>
            {
                activationOrder.Remove(owner);
                activationOrder.Add(owner);
                Publish();
            };
            form.VisibleChanged += (sender, args) =>
            {
                if (!mainWindow && owner.handle != IntPtr.Zero && !form.Visible && !form.RecreatingHandle)
                {
                    activationOrder.Remove(owner);
                    EntraCredentialFactory.ReleaseOwnerWindowHandle(owner.handle, CurrentHandle());
                }
            };
            form.HandleDestroyed += (sender, args) =>
            {
                var destroyed = owner.handle;
                owner.handle = IntPtr.Zero;
                if (!form.RecreatingHandle) activationOrder.Remove(owner);
                EntraCredentialFactory.ReleaseOwnerWindowHandle(destroyed, CurrentHandle());
            };
            if (form.IsHandleCreated) HandleCreated(form, EventArgs.Empty);
        }

        private static IntPtr CurrentHandle() =>
            activationOrder.LastOrDefault(owner => owner.handle != IntPtr.Zero)?.handle ?? IntPtr.Zero;
    }
}
