using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using ServiceBusExplorer.Enums;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ServiceBusExplorer.Common.Helpers;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public partial class ConnectFormEntraTests
    {
        [Theory]
        [InlineData(NamespaceService.ServiceBus, "Queues,Topics")]
        [InlineData(NamespaceService.EventHubs, "Event Hubs")]
        [InlineData(NamespaceService.NotificationHubs, "Notification Hubs")]
        [InlineData(NamespaceService.Relay, "Relays")]
        public void ManualSasRequiresExactlyOneService(NamespaceService service, string expected)
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                var helper = new ServiceBusHelper((_, __) => { });
                using var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                form.Show();
                GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                var editor = GetTextBox(form, ControlNameUri);
                // The hostname, SAS policy and EntityPath are identical in all four cases.
                editor.Text = "Endpoint=sb://sample.servicebus.windows.net/;SharedAccessKeyName=policy;SharedAccessKey=c3ludGhldGlj;EntityPath=events";
                var connect = FindControl<Button>(form, "btnOk");
                var radios = FindControl<FlowLayoutPanel>(form, "serviceChoices").Controls.OfType<RadioButton>().ToArray();
                Assert.Equal(5, radios.Length);
                Assert.DoesNotContain(radios, option => option.Checked);
                GetComboBox(form, ControlNameTransportType).SelectedItem = Microsoft.ServiceBus.Messaging.TransportType.Amqp;
                Assert.False(connect.Enabled);
                typeof(ConnectForm).GetMethod("ConnectForm_KeyPress", BindingFlags.NonPublic | BindingFlags.Instance)
                    .Invoke(form, new object[] { form, new KeyPressEventArgs('\r') });
                Assert.Equal(DialogResult.None, form.DialogResult);
                FindControl<RadioButton>(form, "radioCustom").PerformClick();
                FindControl<RadioButton>(form, "radio" + service).PerformClick();
                Assert.Single(radios, option => option.Checked);
                Assert.Equal(expected, string.Join(",", form.SelectedEntities));
                Assert.False(GetCheckBoxComboBox(form, ControlNameSelectedEntities).Visible);
                Assert.True(connect.Enabled);
                connect.PerformClick();
                Assert.Equal(DialogResult.OK, form.DialogResult);
                Assert.Null(helper.NamespaceUri);
                ResetManualConnectionState();
            });
        }

        [Fact]
        public void RemovingEmulatorFlagRequiresFreshManualChoice()
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                using var form = new ConnectForm(new ServiceBusHelper((_, __) => { }), ConfigFileUse.ApplicationConfig, null);
                GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                var editor = GetTextBox(form, ControlNameUri);
                editor.Text = LocalSas + ";UseDevelopmentEmulator=false";
                Assert.False(FindControl<Button>(form, "btnOk").Enabled);
                editor.Text = SavedEmulator;
                Assert.True(FindControl<RadioButton>(form, "radioServiceBus").Checked);
                Assert.False(FindControl<RadioButton>(form, "radioEventHubs").Enabled);
                Assert.True(FindControl<RadioButton>(form, "radioCustom").Enabled);
                editor.Text = LocalSas;
                Assert.False(FindControl<RadioButton>(form, "radioServiceBus").Checked);
                Assert.False(FindControl<Button>(form, "btnOk").Enabled);
            });
        }

        [Theory]
        [InlineData("Custom|Relays", NamespaceService.Custom, "Relays")]
        [InlineData("EventHubs", NamespaceService.EventHubs, "Event Hubs")]
        [InlineData("NotificationHubs", NamespaceService.NotificationHubs, "Notification Hubs")]
        public void OpeningProfileRestoresOnlyExplicitSelection(string setting, NamespaceService service, string group)
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                var helper = new ServiceBusHelper((_, __) => { });
                var profile = ServiceBusNamespace.GetServiceBusNamespace(SavedProfileKey, LocalSas, (_, __) => { });
                profile.Selection = NamespaceSelection.FromSetting(setting);
                helper.ServiceBusNamespaces[SavedProfileKey] = profile;
                using var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                form.Show();
                form.SelectProfile(SavedProfileKey);
                Assert.True(FindControl<RadioButton>(form, "radio" + service).Checked);
                Assert.Equal(group, Assert.Single(form.SelectedEntities));
                Assert.Equal(setting, profile.Selection.ToSetting());
                GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                GetTextBox(form, ControlNameUri).Text = LocalSas;
                Assert.False(FindControl<Button>(form, "btnOk").Enabled);
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void AmbiguousSavedMenuAndShortcutOpenPrefilledDialog(bool useShortcut)
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                var helper = new ServiceBusHelper((_, __) => { });
                helper.ServiceBusNamespaces[SavedProfileKey] = ServiceBusNamespace.GetServiceBusNamespace(SavedProfileKey, LocalSas, (_, __) => { });
                using var log = new BlockingCollection<string>();
                using var menu = new ToolStripMenuItem();
                using var strip = new MenuStrip();
                strip.Items.Add(menu);
                var main = CreateProfileTestMainForm(log);
                try
                {
                    SetMainField(main, "serviceBusHelper", helper);
                    SetMainField(main, "configFileUse", ConfigFileUse.ApplicationConfig);
                    SetMainField(main, "savedConnectionsToolStripMenuItem", menu);
                    InvokePrivateMethod(main, "UpdateSavedConnectionsMenu");
                    var item = Assert.Single(menu.DropDownItems.OfType<ToolStripMenuItem>());
                    Assert.Equal(Keys.Control | Keys.D1, item.ShortcutKeys);
                    string selectedKey = null;
                    bool? enabled = null;
                    using var timer = new System.Windows.Forms.Timer { Interval = 20 };
                    timer.Tick += (_, __) =>
                    {
                        var form = Application.OpenForms.OfType<ConnectForm>().FirstOrDefault();
                        if (form == null) return;
                        timer.Stop();
                        selectedKey = GetComboBox(form, ControlNameServiceBusNamespace).Text;
                        enabled = FindControl<Button>(form, "btnOk").Enabled;
                        form.Close();
                    };
                    timer.Start();
                    if (useShortcut)
                    {
                        var handled = typeof(ToolStripMenuItem).GetMethod("ProcessCmdKey", BindingFlags.Instance | BindingFlags.NonPublic)
                            .Invoke(item, new object[] { new Message(), Keys.Control | Keys.D1 });
                        Assert.Equal(true, handled);
                    }
                    else item.PerformClick();
                    Assert.Equal(SavedProfileKey, selectedKey);
                    Assert.Equal(false, enabled);
                    Assert.Null(helper.NamespaceUri);
                }
                finally { SetPrivateStaticField(typeof(MainForm), "mainSingletonMainForm", null); }
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void EntraUsesExistingAudienceResultAndEmulatorUsesServiceBus(bool eventHubAudience)
        {
            var entra = ServiceBusNamespace.GetServiceBusNamespace("Entra", "Endpoint=sb://localhost/;AuthMode=AAD", (_, __) => { });
            Assert.Equal(eventHubAudience ? new[] { Constants.EventHubEntities } : new[] { Constants.QueueEntities, Constants.TopicEntities },
                entra.GetDiscoveryEntities(eventHubAudience, true));
            var emulator = ServiceBusNamespace.GetServiceBusNamespace("Emulator", SavedEmulator, (_, __) => { });
            Assert.Equal(new[] { Constants.QueueEntities, Constants.TopicEntities }, emulator.GetDiscoveryEntities(eventHubAudience, true));
            emulator.Selection = new NamespaceSelection(NamespaceService.Custom, new[] { Constants.QueueEntities, Constants.EventHubEntities });
            Assert.Equal(new[] { Constants.QueueEntities }, emulator.GetDiscoveryEntities(eventHubAudience, true));
        }

        [Fact]
        public void SavedServiceAndCustomGroupsReopenWithTheirProfile()
        {
            WithProfileConfiguration(CheckSavedSelection, storedProfile: LocalSas);
        }

        private static void CheckSavedSelection()
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                using var log = new BlockingCollection<string>();
                var main = CreateProfileTestMainForm(log);
                var helper = new ServiceBusHelper((_, __) => { });
                using var menu = new ToolStripMenuItem();
                SetMainField(main, "serviceBusHelper", helper);
                SetMainField(main, "configFileUse", ConfigFileUse.ApplicationConfig);
                SetMainField(main, "savedConnectionsToolStripMenuItem", menu);
                main.LoadSavedConnections(null);
                if (!helper.ServiceBusNamespaces.ContainsKey(SavedProfileKey))
                    throw new InvalidOperationException("The configuration reader did not load the saved SAS profile.");
                foreach (var service in new[] { NamespaceService.NotificationHubs, NamespaceService.Custom })
                {
                    using (var edit = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null))
                    {
                        edit.Show();
                        edit.SelectProfile(SavedProfileKey);
                        if (service == NamespaceService.NotificationHubs)
                            Assert.False(FindControl<Button>(edit, "btnOk").Enabled);
                        FindControl<RadioButton>(edit, "radio" + service).PerformClick();
                        if (service == NamespaceService.Custom)
                        {
                            foreach (var item in GetCheckBoxComboBox(edit, ControlNameSelectedEntities).CheckBoxItems)
                                item.Checked = item.Text == Constants.RelayEntities;
                        }
                        FindControl<Button>(edit, "btnSave").PerformClick();
                    }
                    main.LoadSavedConnections(null);
                    var profile = helper.ServiceBusNamespaces[SavedProfileKey];
                    Assert.Equal(service, profile.Selection.Service);
                    Assert.DoesNotContain("NamespaceService", profile.ConnectionString);
                    var xml = XDocument.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    var setting = xml.Root.Element("appSettings").Elements("add")
                        .Single(entry => (string)entry.Attribute("key") == "NamespaceService." + SavedProfileKey);
                    Assert.Equal(service == NamespaceService.Custom ? "Custom|Relays" : "NotificationHubs", (string)setting.Attribute("value"));

                    using var reopened = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                    reopened.SelectProfile(SavedProfileKey);
                    Assert.True(FindControl<RadioButton>(reopened, "radio" + service).Checked);
                    Assert.Equal(profile.Selection.Entities, reopened.SelectedEntities);
                }
                ConfigurationHelper.UpdateServiceBusNamespace(ConfigFileUse.ApplicationConfig, SavedProfileKey, "Renamed", null, (_, __) => { });
                main.LoadSavedConnections(null);
                Assert.Equal(new[] { Constants.RelayEntities }, helper.ServiceBusNamespaces["Renamed"].Selection.Entities);
                ConfigurationHelper.RemoveServiceBusNamespace(ConfigFileUse.ApplicationConfig, "Renamed", (_, __) => { });
                main.LoadSavedConnections(null);
                Assert.Empty(helper.ServiceBusNamespaces);
                Assert.DoesNotContain("NamespaceService.", System.IO.File.ReadAllText(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            });
        }

        [Theory]
        [InlineData(NamespaceService.EventHubs)]
        [InlineData(NamespaceService.NotificationHubs)]
        [InlineData(NamespaceService.Relay)]
        public void NonServiceBusDashboardSkipsManualTimerAndPathRefresh(NamespaceService service)
        {
            RunOnStaThread(() =>
            {
                using var dashboard = new DashboardControl();
                using var log = new BlockingCollection<string>();
                var main = CreateProfileTestMainForm(log);
                try
                {
                    SetMainField(main, "dashboardControl", dashboard);
                    SetMainField(main, "treeViewFilterHelper", new TreeViewFilterHelper());
                    // An unconnected helper has no clients. A forbidden call would throw or produce an error log.
                    var connection = new ServiceBusHelper((message, _) => log.Add(message));
                    typeof(MainForm).GetMethod("SetDiscoveryConnection", BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(main, new object[] { connection, new NamespaceSelection(service).Entities });
                    var requests = 0;
                    dashboard.OnRefreshRequested = () => { requests++; return Task.CompletedTask; };
                    var timer = DashboardField<System.Windows.Forms.Timer>(dashboard, "autoRefreshTimer");
                    typeof(System.Windows.Forms.Timer).GetMethod("OnTick", BindingFlags.Instance | BindingFlags.NonPublic)
                        .Invoke(timer, new object[] { EventArgs.Empty });
                    DashboardField<Button>(dashboard, "refreshButton").PerformClick();
                    dashboard.LoadDataAsync().GetAwaiter().GetResult();
                    main.RefreshServiceBusEntityNode("events").GetAwaiter().GetResult();
                    Assert.Equal(0, requests);
                    Assert.Empty(log);
                    Assert.False(timer.Enabled);
                }
                finally { SetPrivateStaticField(typeof(MainForm), "mainSingletonMainForm", null); }
            });
        }

        private sealed class PendingEventHubDiscovery : ServiceBusHelper
        {
            internal readonly TaskCompletionSource<System.Collections.Generic.IEnumerable<Microsoft.ServiceBus.Messaging.EventHubDescription>> Pending =
                new TaskCompletionSource<System.Collections.Generic.IEnumerable<Microsoft.ServiceBus.Messaging.EventHubDescription>>();
            internal int Calls;
            internal PendingEventHubDiscovery() : base((_, __) => { }) { }
            public override Task<System.Collections.Generic.IEnumerable<Microsoft.ServiceBus.Messaging.EventHubDescription>> ListEventHubsForDiscoveryAsync()
            {
                Calls++;
                return Pending.Task;
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TreeAndDashboardIgnoreLateDiscoveryAfterNamespaceSwitch(bool lateFailure)
        {
            RunOnStaThread(() =>
            {
                using var tree = new TreeView();
                using var dashboard = new DashboardControl();
                using var context = new DashboardContext();
                using var filter = new TextBox();
                using var log = new BlockingCollection<string>();
                using var rootMenu = new ContextMenuStrip();
                using var actions = new ToolStripMenuItem();
                using var iot = new ToolStripMenuItem();
                using var listener = new ToolStripMenuItem();
                using var renameQueue = new ToolStripMenuItem();
                using var renameTopic = new ToolStripMenuItem();
                var main = CreateProfileTestMainForm(log);
                try
                {
                    var helper = new ServiceBusHelper((message, _) => log.Add(message)) { NamespaceUri = new Uri("sb://sample.servicebus.windows.net") };
                    var calls = new ConcurrentQueue<string>();
                    foreach (var name in new[] { "serviceBusQueue", "serviceBusTopic", "serviceBusSubscription", "serviceBusRelay" })
                    {
                        var field = typeof(ServiceBusHelper).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
                        field.SetValue(helper, new EmptyEntityLists(field.FieldType, calls).GetTransparentProxy());
                    }
                    SetMainField(main, "serviceBusTreeView", tree);
                    SetMainField(main, "serviceBusHelper", helper);
                    SetMainField(main, "treeViewFilterHelper", new TreeViewFilterHelper());
                    SetMainField(main, "filterTreeViewTextBox", filter);
                    SetMainField(main, "dashboardControl", dashboard);
                    SetMainField(main, "rootContextMenuStrip", rootMenu);
                    SetMainField(main, "actionsToolStripMenuItem", actions);
                    SetMainField(main, "createIoTHubListenerMenuItem", iot);
                    SetMainField(main, "createEventHubListenerMenuItem", listener);
                    SetMainField(main, "renameQueueMenuItem", renameQueue);
                    SetMainField(main, "renameTopicMenuItem", renameTopic);
                    var bind = typeof(MainForm).GetMethod("SetDiscoveryConnection", BindingFlags.NonPublic | BindingFlags.Instance);
                    var refresh = typeof(MainForm).GetMethod("ShowEntities", BindingFlags.NonPublic | BindingFlags.Instance);
                    void Select(NamespaceService service) => bind.Invoke(main, new object[] { helper, new NamespaceSelection(service).Entities });
                    void Load(EntityType type) => CompleteOnUi((Task)refresh.Invoke(main, new object[] { type }));

                    var previous = new PendingEventHubDiscovery { NamespaceUri = new Uri("sb://previous.servicebus.windows.net/") };
                    bind.Invoke(main, new object[] { previous, new NamespaceSelection(NamespaceService.EventHubs).Entities });
                    var outstanding = (Task)refresh.Invoke(main, new object[] { EntityType.All });
                    Assert.Equal(1, previous.Calls);
                    Assert.False(outstanding.IsCompleted);
                    Select(NamespaceService.ServiceBus);
                    Load(EntityType.All);
                    Assert.Equal(new[] { "Queues", "Topics" }, tree.Nodes[0].Nodes.Cast<TreeNode>().Select(node => node.Name));
                    var activeRoot = tree.Nodes[0];
                    if (lateFailure) previous.Pending.SetException(new InvalidOperationException("late failure"));
                    else previous.Pending.SetResult(new[] { new Microsoft.ServiceBus.Messaging.EventHubDescription("old-hub") });
                    CompleteOnUi(outstanding);
                    Assert.Same(activeRoot, tree.Nodes[0]);
                    Assert.Same(activeRoot, tree.SelectedNode);
                    Assert.Empty(tree.Nodes.Find("old-hub", true));
                    Assert.Empty(log);
                    Assert.Equal(new[] { "GetQueues", "GetTopics", "GetQueues", "GetTopics" }, calls.ToArray());
                    Select(NamespaceService.Relay);
                    Load(EntityType.All);
                    Assert.Equal("Relays", Assert.Single(tree.Nodes[0].Nodes.Cast<TreeNode>()).Name);
                    Assert.Equal(new[] { "GetQueues", "GetTopics", "GetQueues", "GetTopics", "GetRelays" }, calls.ToArray());
                    var before = calls.Count;
                    foreach (var service in new[] { NamespaceService.EventHubs, NamespaceService.NotificationHubs, NamespaceService.Relay })
                    {
                        Select(service);
                        Load(EntityType.Queue);
                        Load(EntityType.Topic);
                        CompleteOnUi(dashboard.LoadDataAsync());
                    }
                    Assert.Equal(before, calls.Count);
                    Assert.Empty(log);
                }
                finally { SetPrivateStaticField(typeof(MainForm), "mainSingletonMainForm", null); }
            });
        }

        // Record the actual adapter methods used by the tree and background worker, without contacting Azure.
        private sealed class EmptyEntityLists : RealProxy
        {
            private readonly ConcurrentQueue<string> calls;
            public EmptyEntityLists(Type type, ConcurrentQueue<string> calls) : base(type) => this.calls = calls;

            public override IMessage Invoke(IMessage message)
            {
                var call = (IMethodCallMessage)message;
                calls.Enqueue(call.MethodName);
                var element = ((MethodInfo)call.MethodBase).ReturnType.GetGenericArguments().Single();
                return new ReturnMessage(Array.CreateInstance(element, 0), null, 0, call.LogicalCallContext, call);
            }
        }

        [Fact]
        public void DashboardSwitchClearsRowsAndDiscardsInFlightLoader()
        {
            RunOnStaThread(() =>
            {
                using var dashboard = new DashboardControl();
                using var context = new DashboardContext();
                using var entered = new ManualResetEventSlim();
                using var unblock = new ManualResetEventSlim();
                var topicCalls = 0;
                var subscriptionCalls = 0;
                var oldQueueCalls = 0;
                dashboard.Initialize(() =>
                {
                    if (++oldQueueCalls > 1)
                    {
                        entered.Set();
                        if (!unblock.Wait(TimeSpan.FromSeconds(5))) throw new TimeoutException();
                    }
                    return new[] { new QueueInfo("old") };
                }, () => { ++topicCalls; return new[] { new TopicInfo("topic") }; }, path =>
                {
                    ++subscriptionCalls;
                    return new[] { new SubscriptionInfo(path, "sub") };
                }, null);
                var grid = DashboardField<DataGridView>(dashboard, "dataGridView");
                DashboardField<CheckBox>(dashboard, "autoRefreshCheckBox").Checked = true;
                var timer = DashboardField<System.Windows.Forms.Timer>(dashboard, "autoRefreshTimer");
                Assert.True(timer.Enabled);
                CompleteOnUi(dashboard.LoadDataAsync());
                Assert.Equal("old", grid.Rows[0].Cells["Name"].Value);
                var stale = dashboard.LoadDataAsync();
                try
                {
                    Assert.True(entered.Wait(TimeSpan.FromSeconds(5)));
                    dashboard.Initialize(null, null, null, null);
                    Assert.False(timer.Enabled);
                    dashboard.ApplyFilter(null);
                    Assert.Empty(grid.Rows);
                    dashboard.Initialize(() => new[] { new QueueInfo("new") }, null, null, null);
                    Assert.True(timer.Enabled);
                    CompleteOnUi(dashboard.LoadDataAsync());
                }
                finally { unblock.Set(); }
                CompleteOnUi(stale);
                dashboard.ApplyFilter(null);
                Assert.Equal(1, topicCalls);
                Assert.Equal(1, subscriptionCalls);
                Assert.Single(grid.Rows.Cast<DataGridViewRow>());
                Assert.Equal("new", grid.Rows[0].Cells["Name"].Value);
            });
        }

        private static T DashboardField<T>(DashboardControl dashboard, string name) =>
            (T)typeof(DashboardControl).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dashboard);

        private sealed class DashboardContext : SynchronizationContext, IDisposable
        {
            private readonly ConcurrentQueue<Action> pending = new ConcurrentQueue<Action>();
            private readonly SynchronizationContext previous = Current;
            private readonly bool autoInstall = WindowsFormsSynchronizationContext.AutoInstall;
            public DashboardContext()
            {
                WindowsFormsSynchronizationContext.AutoInstall = false;
                SetSynchronizationContext(this);
            }
            public void Dispose()
            {
                SetSynchronizationContext(previous);
                WindowsFormsSynchronizationContext.AutoInstall = autoInstall;
            }

            public override void Post(SendOrPostCallback callback, object state) =>
                pending.Enqueue(() => callback(state));

            public void RunPending()
            {
                while (pending.TryDequeue(out var callback)) callback();
            }
        }

        private static void CompleteOnUi(Task task)
        {
            var context = (DashboardContext)SynchronizationContext.Current;
            var elapsed = Stopwatch.StartNew();
            while (!task.IsCompleted && elapsed.Elapsed < TimeSpan.FromSeconds(10))
            {
                context.RunPending();
                Thread.Sleep(1);
            }
            Assert.True(task.IsCompleted);
            task.GetAwaiter().GetResult();
        }
    }
}
