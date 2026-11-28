using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using ServiceBusExplorer.UIHelpers;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Tests.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [Collection("Entra credential cache")]
    public class EventHubArmFormTests
    {
        [Fact]
        public void FormLoadsGroupsAndWritesOnlyAfterTheUserConfirms()
        {
            OnSta(() =>
            {
                using var wire = new EventHubArmManagementTests.Wire();
                using var form = new EventHubArmForm(wire.Open());
                form.Show();
                var groups = (ListView)Assert.Single(form.Controls.Find("consumerGroups", true));
                PumpUntil(() => groups.Items.Count == 2);
                Assert.Equal(3, wire.Requests.Count);
                groups.Items["workers"].Selected = true;
                var name = (TextBox)Assert.Single(form.Controls.Find("groupName", true));
                var metadata = (TextBox)Assert.Single(form.Controls.Find("groupMetadata", true));
                PumpUntil(() => name.Text == "workers");
                Assert.Equal("original", metadata.Text);
                metadata.Text = " new value ";
                Complete(form.SaveGroupAsync(selected => { Assert.Equal("workers", selected); return false; }));
                Assert.Equal(3, wire.Requests.Count);
                Complete(form.SaveGroupAsync(selected => { Assert.Equal("workers", selected); return true; }));
                Assert.Equal(4, wire.Requests.Count);
                Assert.Equal("PUT", wire.Requests[3].Method);
                Assert.Equal(" new value ", groups.Items["workers"].SubItems[1].Text);

                name.Text = "bad/name";
                Complete(form.SaveGroupAsync(_ => throw new Exception("Invalid input must not prompt or write")));
                Assert.Equal(4, wire.Requests.Count);
                name.Text = "new-group";
                metadata.Text = "first";
                wire.OmitPutMetadata = true;
                Complete(form.SaveGroupAsync(selected => { Assert.Equal("new-group", selected); return true; }));
                Assert.Equal(5, wire.Requests.Count);
                Assert.Equal(3, groups.Items.Count);
                Assert.Equal("first", groups.Items["new-group"].SubItems[1].Text);

                name.Text = "draft-group";
                metadata.Text = "edited metadata";
                wire.Failure = System.Net.HttpStatusCode.Forbidden;
                Complete(form.SaveGroupAsync(_ => true));
                var failedCount = wire.Requests.Count;
                Complete(form.SaveGroupAsync(_ => throw new Exception("A failed write requires refresh")));
                Assert.Equal(failedCount, wire.Requests.Count);
                wire.Failure = null;
                Complete(form.RefreshGroupsAsync());
                Assert.Equal("draft-group", name.Text);
                Assert.Equal("edited metadata", metadata.Text);
                var writes = wire.Requests.Count(x => x.Method == "PUT");
                Complete(form.SaveGroupAsync(_ => false));
                Assert.Equal(writes, wire.Requests.Count(x => x.Method == "PUT"));
                Complete(form.SaveGroupAsync(group => { Assert.Equal("draft-group", group); return true; }));
                Assert.Contains("edited metadata", wire.Requests.Last().Body);
                Assert.Equal(writes + 1, wire.Requests.Count(x => x.Method == "PUT"));
                form.Close();
            });
        }

        [Fact]
        public void PasteKeepsWholeInputAndRejectsOverlongMetadataOrNameBeforeConfirmation()
        {
            OnSta(() =>
            {
                using var wire = new EventHubArmManagementTests.Wire();
                using var form = new EventHubArmForm(wire.Open());
                form.Show();
                var name = (TextBox)form.Controls.Find("groupName", true).Single();
                var data = (TextBox)form.Controls.Find("groupMetadata", true).Single();
                PumpUntil(() => name.Enabled);
                name.Paste(new string('a', 51));
                Assert.Equal(51, name.TextLength);
                Complete(form.SaveGroupAsync(_ => throw new Exception("Overlong name was accepted")));
                name.Text = "workers";
                data.Paste(new string('m', 1025));
                Assert.Equal(1025, data.TextLength);
                Complete(form.SaveGroupAsync(_ => throw new Exception("Overlong metadata was accepted")));
                Assert.DoesNotContain(wire.Requests, r => r.Method == "PUT");
            });
        }

        [Fact]
        public void UnconfirmedWriteCanBeKeptOpenOrClosedWithoutAnotherPut()
        {
            OnSta(() =>
            {
                using var wire = new EventHubArmManagementTests.Wire { PendingPut = new TaskCompletionSource<bool>() };
                bool allowClose = false;
                using var form = new EventHubArmForm(wire.Open(), () => allowClose);
                form.Show();
                var name = (TextBox)form.Controls.Find("groupName", true).Single();
                PumpUntil(() => name.Enabled);
                name.Text = "workers";
                var request = form.SaveGroupAsync(_ => true);
                PumpUntil(() => wire.Requests.Any(r => r.Method == "PUT"));
                form.Close();
                Assert.False(form.IsDisposed);
                Assert.False(wire.PutToken.IsCancellationRequested);
                allowClose = true;
                form.Close();
                Assert.True(form.IsDisposed);
                Assert.True(wire.PutToken.IsCancellationRequested);
                // Azure may already have accepted the PUT even after the user cancels the wait.
                wire.PendingPut.TrySetResult(true);
                Complete(request);
                Assert.Single(wire.Requests, r => r.Method == "PUT");
            });
        }

        [Fact]
        public void StartingEventGridGatesArmBeforeDiscoveryAndAfterFailedInitialization()
        {
            var main = (MainForm)FormatterServices.GetUninitializedObject(typeof(MainForm));
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var helper = new ServiceBusHelper((_, __) => { }) { NamespaceUri = new Uri("sb://synthetic.servicebus.windows.net") };
            typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", flags).SetValue(helper,
                new ServiceBusExplorer.Helpers.ServiceBusNamespace(helper.NamespaceUri.ToString(), "synthetic", "tenant", Microsoft.ServiceBus.Messaging.TransportType.Amqp));
            typeof(MainForm).GetField("serviceBusHelper", flags).SetValue(main, helper);
            typeof(MainForm).GetField("eventGridSubscriptionLoads", flags).SetValue(main, new EventGridSubscriptionLoads<TreeNode>());
            var reset = typeof(MainForm).GetMethod("ResetEventGridConnection", flags);
            Assert.True(main.CanOpenArmEventHub);
            reset.Invoke(main, new object[] { true });
            Assert.Null(typeof(MainForm).GetField("eventGridLibrary", flags).GetValue(main));
            Assert.False(main.CanOpenArmEventHub);
            reset.Invoke(main, new object[] { false });
            Assert.True(main.CanOpenArmEventHub);
        }

        private static void OnSta(Action action)
        {
            Exception error = null;
            var thread = new Thread(() =>
            {
                try { action(); }
                catch (Exception ex) { error = ex; }
            });
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "ARM form did not complete");
            if (error != null) ExceptionDispatchInfo.Capture(error).Throw();
        }

        private static void Complete(Task task)
        {
            PumpUntil(() => task.IsCompleted);
            task.GetAwaiter().GetResult();
        }

        private static void PumpUntil(Func<bool> ready)
        {
            var until = DateTime.UtcNow.AddSeconds(8);
            while (!ready() && DateTime.UtcNow < until)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }
            Assert.True(ready(), "The ARM form did not reach the expected state");
        }
    }
}
