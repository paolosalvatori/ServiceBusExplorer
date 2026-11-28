using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Controls
{
    // The controls share the application-wide MainForm settings.
    [CollectionDefinition("Session browser UI", DisableParallelization = true)]
    public class SessionBrowserCollection { }

    [Collection("Session browser UI")]
    public class SessionBrowserControlTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SessionsButtonListsOnlySessionIdsWithoutReadingState(bool subscription)
        {
            RunUi(subscription, (control, helper) =>
            {
                Find<Button>(control, "btnSessions").PerformClick();

                Assert.Equal(subscription ? "topic" : "queue", helper.ListEntity);
                Assert.Equal(subscription ? "subscription" : null, helper.ListSubscription);
                var grid = Find<DataGridView>(control, "sessionsDataGridView");
                var column = Assert.Single(grid.Columns.Cast<DataGridViewColumn>());
                Assert.Equal(nameof(SessionInfo.SessionId), column.DataPropertyName);
                Assert.Equal("A", Assert.Single(grid.Rows.Cast<DataGridViewRow>()).Cells[column.Index].Value);
                Assert.Null(grid.CurrentCell);
                Assert.Empty(grid.SelectedRows);
                Assert.Empty(helper.ReadIds);
                return Task.CompletedTask;
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void RowSelectionIsIndependentOfReceiveTimeoutAndSharesPendingReads(bool subscription)
        {
            RunUi(subscription, async (control, helper) =>
            {
                MainForm.SingletonMainForm.ReceiveTimeout = 0;
                helper.Rows.Add(new SessionInfo("B"));
                Find<Button>(control, "btnSessions").PerformClick();
                var grid = Find<DataGridView>(control, "sessionsDataGridView");
                var state = Find<TextBox>(control, "txtSessionState");
                var pending = new TaskCompletionSource<byte[]>();
                helper.Read = id => id == "A" ? pending.Task : Task.FromResult(Encoding.UTF8.GetBytes("state B"));

                grid.CurrentCell = grid.Rows[0].Cells[0];
                Assert.Equal(new[] { "A" }, helper.ReadIds);
                Assert.False(helper.ReadToken.CanBeCanceled);
                grid.CurrentCell = grid.Rows[1].Cells[0];
                Assert.Equal("state B", state.Text);
                grid.CurrentCell = grid.Rows[0].Cells[0];
                Assert.Equal(new[] { "A", "B" }, helper.ReadIds);
                Assert.Equal(helper.ListEntity, helper.ReadEntity);
                Assert.Equal(helper.ListSubscription, helper.ReadSubscription);

                await CompleteReadAndCheckDisplay(state, pending, "state A");
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void LockedSelectionShowsStatusAndCanReadAgainAfterRelease(bool subscription)
        {
            RunUi(subscription, async (control, helper) =>
            {
                await Refresh(control);
                var grid = Find<DataGridView>(control, "sessionsDataGridView");
                var state = Find<TextBox>(control, "txtSessionState");
                helper.Read = _ => Task.FromException<byte[]>(new ServiceBusException("held", ServiceBusFailureReason.SessionCannotBeLocked));
                grid.CurrentCell = grid.Rows[0].Cells[0];
                Assert.Contains("locked by another receiver", state.Text);
                Assert.DoesNotContain(helper.Logs, line => line.Contains("held"));

                helper.Read = id => Task.FromResult(Encoding.UTF8.GetBytes("released " + id));
                await Refresh(control);
                grid.CurrentCell = grid.Rows[0].Cells[0];
                Assert.Equal("released A", state.Text);
                Assert.Equal(new[] { "A", "A" }, helper.ReadIds);
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void FailedRefreshPreservesSingleSelectedRowAndDetails(bool subscription)
        {
            RunUi(subscription, async (control, helper) =>
            {
                await Refresh(control);
                var grid = Find<DataGridView>(control, "sessionsDataGridView");
                var details = Find<PropertyGrid>(control, "sessionPropertyGrid");
                var state = Find<TextBox>(control, "txtSessionState");
                grid.CurrentCell = grid.Rows[0].Cells[0];
                var row = grid.CurrentRow;
                Assert.Same(helper.Rows[0], row.DataBoundItem);
                Assert.Same(row.DataBoundItem, details.SelectedObject);
                Assert.Equal("state A", state.Text);
                var pending = new TaskCompletionSource<List<SessionInfo>>();
                helper.List = () => pending.Task;
                var refresh = Refresh(control);
                Assert.Same(row.DataBoundItem, details.SelectedObject);
                Assert.Equal("state A", state.Text);

                pending.SetException(new InvalidOperationException("discovery failed"));
                await refresh;
                Assert.Same(row, Assert.Single(grid.SelectedRows.Cast<DataGridViewRow>()));
                Assert.Same(row, grid.CurrentRow);
                Assert.Same(row.DataBoundItem, details.SelectedObject);
                Assert.Equal("state A", state.Text);
                Assert.Contains(helper.Logs, line => line.Contains("discovery failed"));

                helper.List = () => Task.FromResult(new List<SessionInfo>());
                await Refresh(control);
                Assert.Empty(grid.Rows);
                Assert.Null(details.SelectedObject);
                Assert.Equal(string.Empty, state.Text);
                Assert.Equal(new[] { "A" }, helper.ReadIds);
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void RowChangedDuringFailedRefreshCanFinishItsPendingRead(bool subscription)
        {
            RunUi(subscription, async (control, helper) =>
            {
                helper.Rows.Add(new SessionInfo("B"));
                await Refresh(control);
                var grid = Find<DataGridView>(control, "sessionsDataGridView");
                var state = Find<TextBox>(control, "txtSessionState");
                grid.CurrentCell = grid.Rows[0].Cells[0];

                var discovery = new TaskCompletionSource<List<SessionInfo>>();
                helper.List = () => discovery.Task;
                var refresh = Refresh(control);
                var pending = new TaskCompletionSource<byte[]>();
                helper.Read = _ => pending.Task;
                grid.CurrentCell = grid.Rows[1].Cells[0];
                Assert.Equal(new[] { "A", "B" }, helper.ReadIds);
                discovery.SetException(new InvalidOperationException("discovery failed"));
                await refresh;

                await CompleteReadAndCheckDisplay(state, pending, "state B");
                Assert.Same(helper.Rows[1], grid.CurrentRow.DataBoundItem);
                Assert.Same(helper.Rows[1], Find<PropertyGrid>(control, "sessionPropertyGrid").SelectedObject);
            });
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public void ClosingControlCancelsDiscoveryAndIgnoresLateCompletion(bool subscription, bool lateFailure)
        {
            RunUi(subscription, async (control, helper) =>
            {
                var pending = new TaskCompletionSource<List<SessionInfo>>();
                helper.List = () => pending.Task;
                var refresh = Refresh(control);
                bool cancelled = false;
                using var registration = helper.ListToken.Register(() => cancelled = true);
                control.Dispose();
                Assert.True(cancelled);

                if (lateFailure) pending.SetException(new OperationCanceledException(helper.ListToken));
                else pending.SetResult(helper.Rows);
                await refresh;
                Assert.Empty(helper.Logs);
                Assert.Empty(helper.ReadIds);
            });
        }

        private static async Task CompleteReadAndCheckDisplay(TextBox state, TaskCompletionSource<byte[]> pending, string expected)
        {
            var displayed = new TaskCompletionSource<string>();
            void OnTextChanged(object sender, EventArgs e) => displayed.TrySetResult(state.Text);
            state.TextChanged += OnTextChanged;
            try
            {
                pending.SetResult(Encoding.UTF8.GetBytes(expected));
                var completed = await Task.WhenAny(displayed.Task, Task.Delay(TimeSpan.FromSeconds(5)));
                Assert.Same(displayed.Task, completed);
                Assert.Equal(expected, await displayed.Task);
            }
            finally
            {
                state.TextChanged -= OnTextChanged;
            }
        }

        private static T Find<T>(Control control, string name) where T : Control =>
            (T)control.Controls.Find(name, true).Single();

        private static Task Refresh(UserControl control) => control is HandleQueueControl queue
            ? queue.GetMessageSessions() : ((HandleSubscriptionControl)control).GetMessageSessions();

        private static void RunUi(bool subscription, Func<UserControl, BrowserHelper, Task> test)
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                var singleton = typeof(MainForm).GetField("mainSingletonMainForm", BindingFlags.NonPublic | BindingFlags.Static);
                var previous = singleton.GetValue(null);
                try
                {
                    var main = (MainForm)FormatterServices.GetUninitializedObject(typeof(MainForm));
                    main.ReceiveTimeout = 30;
                    singleton.SetValue(null, main);
                    var helper = new BrowserHelper();
                    void Log(string message, bool async) => helper.Logs.Add(message);
                    using UserControl control = subscription
                        ? new HandleSubscriptionControl(Log, helper, new SubscriptionWrapper(
                            new SubscriptionInfo("topic", "subscription") { RequiresSession = true }, new TopicInfo("topic")))
                        : new HandleQueueControl(Log, helper, new QueueInfo("queue") { RequiresSession = true }, "queue", false);
                    using var form = new Form { ClientSize = new Size(1100, 700) };
                    control.Dock = DockStyle.Fill;
                    form.Controls.Add(control);
                    form.Shown += async (_, __) =>
                    {
                        try { await test(control, helper); }
                        catch (Exception ex) { failure = ex; }
                        finally { form.Close(); }
                    };
                    Application.Run(form);
                }
                catch (Exception ex) { failure = ex; }
                finally { singleton.SetValue(null, previous); }
            }) { IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "Session browser did not finish.");
            if (failure != null) ExceptionDispatchInfo.Capture(failure).Throw();
        }

        private sealed class BrowserHelper : ServiceBusHelper
        {
            internal readonly List<string> Logs = new List<string>();
            internal readonly List<string> ReadIds = new List<string>();
            internal readonly List<SessionInfo> Rows = new List<SessionInfo> { new SessionInfo("A") };
            internal Func<Task<List<SessionInfo>>> List;
            internal Func<string, Task<byte[]>> Read = id => Task.FromResult(Encoding.UTF8.GetBytes("state " + id));
            internal string ListEntity, ListSubscription, ReadEntity, ReadSubscription;
            internal CancellationToken ListToken, ReadToken;

            internal BrowserHelper() : base((_, __) => { })
            {
                NamespaceUri = new Uri("sb://localhost/");
                // An entity-scoped profile skips the queue editor's namespace SKU lookup.
                typeof(ServiceBusHelper).GetProperty(nameof(ConnectionString)).SetValue(this,
                    "Endpoint=sb://localhost/;SharedAccessKeyName=test;SharedAccessKey=dGVzdA==;EntityPath=queue");
                List = () => Task.FromResult(Rows);
            }

            public override Task<List<SessionInfo>> GetMessageSessionsFromModernDataPlane(
                string entityPath, string subscriptionName, CancellationToken cancellationToken)
            {
                ListEntity = entityPath;
                ListSubscription = subscriptionName;
                ListToken = cancellationToken;
                return List();
            }

            public override Task<byte[]> GetSessionStateFromModernDataPlane(
                string entityPath, string subscriptionName, string sessionId, CancellationToken cancellationToken)
            {
                ReadEntity = entityPath;
                ReadSubscription = subscriptionName;
                ReadIds.Add(sessionId);
                ReadToken = cancellationToken;
                return Read(sessionId);
            }
        }
    }
}
