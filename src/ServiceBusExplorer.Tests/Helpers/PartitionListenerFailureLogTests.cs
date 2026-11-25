using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Messaging.EventHubs.Consumer;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class PartitionListenerFailureLogTests
    {
        [Fact]
        public void ReaderFailureIsVisibleBeforeLogStopsWhileUiContinuesPumping()
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try
                {
                    using var form = new Form();
                    using var listener = new PartitionListenerControl();
                    Method("InitializeComponent").Invoke(listener, null);
                    using var log = new TextBox { Multiline = true };
                    form.Controls.Add(listener);
                    form.Controls.Add(log);
                    var stopped = new TaskCompletionSource<bool>();
                    var queued = new List<string>();
                    Set(listener, "writeToLog", new WriteToLogDelegate((message, async) =>
                    {
                        if (async) { queued.Add(message); return; }
                        if (log.InvokeRequired) log.Invoke(new Action(() => log.AppendText(message)));
                        else log.AppendText(message);
                    }));
                    Set(listener, "stopLog", new Func<Task>(() =>
                    {
                        try
                        {
                            Assert.Contains("synthetic reader failure", log.Text);
                            Assert.Empty(queued);
                            stopped.TrySetResult(true);
                        }
                        catch (Exception ex) { stopped.TrySetException(ex); }
                        return Task.CompletedTask;
                    }));
                    using var cancellation = new CancellationTokenSource();
                    Set(listener, "cancellationTokenSource", cancellation);
                    using var timeout = new System.Windows.Forms.Timer { Interval = 10000 };
                    timeout.Tick += (sender, args) =>
                    {
                        failure = new TimeoutException("Listener failure cleanup blocked the UI.");
                        form.Close();
                    };
                    form.Shown += async (sender, args) =>
                    {
                        timeout.Start();
                        try
                        {
                            var reader = Task.Run(async () => await (Task)Method("ReadPartitionAsync").Invoke(listener,
                                new object[] { new FailedConsumer(), "0", EventPosition.Earliest, "synthetic", "hub", "$Default", 10, TimeSpan.FromSeconds(1), cancellation.Token }));
                            ((List<Task>)Field("partitionReadTasks").GetValue(listener)).Add(reader);
                            await stopped.Task;
                            await listener.ShutdownAsync();
                            await reader;
                            Assert.True(cancellation.IsCancellationRequested);
                            Assert.Contains("synthetic reader failure", log.Text);
                        }
                        catch (Exception ex) { failure = ex; }
                        finally { timeout.Stop(); form.Close(); }
                    };
                    Application.Run(form);
                }
                catch (Exception ex) { failure = ex; }
            }) { IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "UI thread did not finish.");
            if (failure != null) ExceptionDispatchInfo.Capture(failure).Throw();
        }

        private sealed class FailedConsumer : EventHubConsumerClient
        {
            public override IAsyncEnumerable<PartitionEvent> ReadEventsFromPartitionAsync(string partitionId,
                EventPosition startingPosition, ReadEventOptions readOptions, CancellationToken cancellationToken = default) =>
                throw new InvalidOperationException("synthetic reader failure");
        }

        private static FieldInfo Field(string name) => typeof(PartitionListenerControl).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
        private static MethodInfo Method(string name) => typeof(PartitionListenerControl).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
        private static void Set(object instance, string name, object value) => Field(name).SetValue(instance, value);
    }
}
