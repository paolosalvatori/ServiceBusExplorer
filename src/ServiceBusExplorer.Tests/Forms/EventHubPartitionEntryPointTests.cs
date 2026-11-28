using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using ServiceBusExplorer.WindowsAzure;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public partial class ConnectFormEntraTests
    {
        [Fact]
        public void SelectingAndRefreshingEmptyPartitionReadsOncePerActionAndUpdatesPane()
        {
            RunOnStaThread(() =>
            {
                using var host = new Form();
                using var log = new BlockingCollection<string>();
                using var tree = new TreeView();
                using var pane = new HeaderPanel();
                using var logPane = new HeaderPanel();
                using var menu = new ContextMenuStrip();
                using var tabs = new TabControl();
                using var explorer = new TabPage();
                tabs.TabPages.Add(explorer);
                using var actions = new ToolStripMenuItem();
                using var iot = new ToolStripMenuItem();
                using var listener = new ToolStripMenuItem();
                var main = CreateProfileTestMainForm(log);
                try
                {
                    var helper = new ServiceBusHelper((_, __) => { });
                    var reads = new CountingPartition();
                    typeof(ServiceBusHelper).GetField("serviceBusPartition", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(helper, reads);
                    SetMainField(main, "serviceBusHelper", helper);
                    SetMainField(main, "mainTabControl", tabs);
                    SetMainField(main, "tabPageExplorer", explorer);
                    SetMainField(main, "panelMain", pane);
                    SetMainField(main, "panelLog", logPane);
                    SetMainField(main, "serviceBusTreeView", tree);
                    SetMainField(main, "actionsToolStripMenuItem", actions);
                    SetMainField(main, "createIoTHubListenerMenuItem", iot);
                    SetMainField(main, "createEventHubListenerMenuItem", listener);
                    SetMainField(main, "partitionContextMenuStrip", menu);
                    host.Controls.Add(pane);
                    host.Controls.Add(tree);
                    host.Show();
                    var hub = tree.Nodes.Add("orders");
                    SetMainField(main, "rootNode", hub);
                    var group = hub.Nodes.Add("analytics");
                    group.Tag = new ConsumerGroupDescription("orders", "analytics");
                    var partition = group.Nodes.Add("Partitions").Nodes.Add("2");
                    partition.Tag = new PartitionDescription("orders", "2");
                    tree.SelectedNode = hub;
                    typeof(MainForm).GetMethod("serviceBusTreeView_NodeMouseClick", BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(main, new object[] { tree, new TreeNodeMouseClickEventArgs(partition, MouseButtons.Left, 1, 0, 0) });
                    Assert.Equal(1, reads.Calls);
                    var control = Assert.Single(pane.Controls.OfType<HandlePartitionControl>());
                    var values = (ListView)Assert.Single(control.Controls.Find("propertyListView", true));
                    string EndSequence() => values.Items.Cast<ListViewItem>().Single(x => x.Text == "End Sequence Number").SubItems[1].Text;
                    Assert.Equal("1", EndSequence());
                    ((Button)Assert.Single(control.Controls.Find("btnRefresh", true))).PerformClick();
                    Assert.Equal(2, reads.Calls);
                    Assert.Same(control, Assert.Single(pane.Controls.OfType<HandlePartitionControl>()));
                    Assert.Equal("2", EndSequence());
                }
                finally { SetPrivateStaticField(typeof(MainForm), "mainSingletonMainForm", null); }
            });
        }

        private sealed class CountingPartition : IServiceBusPartition
        {
            public int Calls;
            public string Scheme { get; set; }
            public WriteToLogDelegate WriteToLog { get; set; }
            public event IServiceBusEntity.EventHandler OnCreate { add { } remove { } }
            public event IServiceBusEntity.EventHandler OnDelete { add { } remove { } }
            public PartitionDescription GetPartition(string path, string group, string id)
            {
                Assert.Equal(("orders", "analytics", "2"), (path, group, id));
                var description = new PartitionDescription(path, id);
                typeof(PartitionDescription).GetProperty("EndSequenceNumber").SetValue(description, (long)++Calls);
                Assert.True(string.IsNullOrEmpty(description.LastEnqueuedOffset));
                return description;
            }
            public IEnumerable<PartitionDescription> GetPartitions(string path, string group) => throw new NotSupportedException();
            public Uri GetPartitionUri(string hub, string group, string id) => throw new NotSupportedException();
        }
    }
}
