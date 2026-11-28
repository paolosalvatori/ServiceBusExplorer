using System;
using System.Runtime.ExceptionServices;
using System.Threading;
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
            Exception error = null;
            var thread = new Thread(() =>
            {
                try
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

                    wire.Failure = System.Net.HttpStatusCode.Forbidden;
                    Complete(form.SaveGroupAsync(_ => true));
                    var failedCount = wire.Requests.Count;
                    Complete(form.SaveGroupAsync(_ => throw new Exception("A failed write requires refresh")));
                    Assert.Equal(failedCount, wire.Requests.Count);
                    form.Close();
                }
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
