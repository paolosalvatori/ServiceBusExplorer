using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;
using Azure.Messaging.ServiceBus.Administration;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public class ManagementRenameTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ContextAndActionsRenameReflectTheActiveManagementPath(bool modern)
        {
            RunSta(() =>
            {
                var main = (MainForm)FormatterServices.GetUninitializedObject(typeof(MainForm));
                var helper = new ServiceBusHelper((_, __) => { });
                var client = new ServiceBusManagementClient(new FakeClient(), new Uri("sb://synthetic.invalid/"));
                Set(helper, "managementClient", modern ? client : null);
                Set(main, "serviceBusHelper", helper);
                Set(main, "eventClickFieldInfo", typeof(ToolStripItem).GetField("EventClick", BindingFlags.NonPublic | BindingFlags.Static));
                Set(main, "eventsPropertyInfo", typeof(System.ComponentModel.Component).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance));
                using (var queue = new ToolStripMenuItem("Rename Queue") { Name = "renameQueueMenuItem" })
                using (var topic = new ToolStripMenuItem("Rename Topic") { Name = "renameTopicMenuItem" })
                using (var context = new ContextMenuStrip())
                {
                    Set(main, "renameQueueMenuItem", queue);
                    Set(main, "renameTopicMenuItem", topic);
                    context.Items.AddRange(new ToolStripItem[] { queue, topic });
                    main.UpdateRenameAvailability();
                    Assert.Equal(!modern, queue.Enabled);
                    Assert.Equal(!modern, topic.Enabled);
                    var clones = (List<ToolStripItem>)typeof(MainForm).GetMethod("CloneItems", BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(main, new object[] { context.Items });
                    try
                    {
                        foreach (var clone in clones)
                            if (clone is ToolStripMenuItem) Assert.Equal(!modern, clone.Enabled);
                    }
                    finally { foreach (var clone in clones) clone.Dispose(); }
                    // Reconnect must restore the supported path rather than permanently disabling it.
                    Set(helper, "managementClient", modern ? null : client);
                    main.UpdateRenameAvailability();
                    Assert.Equal(modern, queue.Enabled);
                    Assert.Equal(modern, topic.Enabled);
                }
            });
        }

        [Fact]
        public void DirectRenameClickOnModernPathReturnsBeforeOpeningDialogOrAccessingSelection()
        {
            RunSta(() =>
            {
                var main = (MainForm)FormatterServices.GetUninitializedObject(typeof(MainForm));
                var helper = new ServiceBusHelper((_, __) => { });
                Set(helper, "managementClient", new ServiceBusManagementClient(new FakeClient(), new Uri("sb://synthetic.invalid/")));
                Set(main, "serviceBusHelper", helper);
                Set(main, "eventClickFieldInfo", typeof(ToolStripItem).GetField("EventClick", BindingFlags.NonPublic | BindingFlags.Static));
                Set(main, "eventsPropertyInfo", typeof(System.ComponentModel.Component).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance));
                typeof(MainForm).GetMethod("renameEntity_Click", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(main, new object[] { null, EventArgs.Empty });
            });
        }

        private static void Set(object instance, string name, object value) => instance.GetType()
            .GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, value);
        private sealed class FakeClient : ServiceBusAdministrationClient { }
        private static void RunSta(Action action)
        {
            Exception failure = null;
            var thread = new Thread(() => { try { action(); } catch (Exception ex) { failure = ex; } });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null) throw new Exception("Rename entry-point regression", failure);
        }
    }
}
