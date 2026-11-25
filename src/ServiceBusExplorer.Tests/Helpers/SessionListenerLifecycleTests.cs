using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Messaging.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    [Collection("Service Bus connection settings")]
    public class SessionListenerLifecycleTests
    {
        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public void FailedOrCancelledCallbackDoesNotCompleteOrReturnSuccess(bool autoComplete, bool cancel)
        {
            RunUi(async (form, listener) =>
            {
                Set(listener, "autoComplete", autoComplete);
                Set(listener, "receiveMode", ReceiveMode.PeekLock);
                Set(listener, "receiverBrokeredMessageInspector", new FailedInspector());
                listener.CaptureSettlementPolicy();
                var completed = false;
                using var cancellation = new CancellationTokenSource();
                if (cancel) cancellation.Cancel();
                var task = listener.ProcessReceivedMessageAsync(Message(), () => { completed = true; return Task.CompletedTask; }, cancellation.Token, true);
                if (cancel) await Assert.ThrowsAnyAsync<OperationCanceledException>(() => task);
                else await Assert.ThrowsAsync<InvalidOperationException>(() => task);
                Assert.False(completed);
            });
        }

        [Fact]
        public void StopSharesDrainTaskKeepsUiResponsiveAndDisposesClientAfterCallback()
        {
            RunUi(async (form, listener) =>
            {
                var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                var events = new List<string>();
                var processor = new Processor(release.Task, entered, events);
                Set(listener, "sessionProcessor", processor);
                Set(listener, "serviceBusClient", new Client(events));
                Set(listener, "stopLog", new Func<Task>(() => { events.Add("log"); return Task.CompletedTask; }));
                Set(listener, "tracking", true);
                Set(listener, "receiveMode", ReceiveMode.PeekLock);
                Set(listener, "autoComplete", false);
                var first = listener.ShutdownAsync();
                await entered.Task;
                Assert.Same(first, listener.ShutdownAsync());
                Assert.Empty(events);
                // Real callback posts tracking to the pumping UI while shutdown waits for it.
                await Task.Run(() => listener.ProcessReceivedMessageAsync(Message(), () => { events.Add("complete"); return Task.CompletedTask; }, CancellationToken.None, true));
                release.SetResult(true);
                await first;
                Assert.Equal(new[] { "complete", "processor", "client", "log" }, events);
                Assert.Same(first, listener.ShutdownAsync());
                var counts = (ConcurrentDictionary<string, int>)Field("sessionMessageCounts").GetValue(listener);
                Assert.Empty(counts);
            });
        }

        [Fact]
        public void ClosingDuringStartupWaitsBeforeDisposingTheClient()
        {
            RunUi(async (form, listener) =>
            {
                var startup = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                var events = new List<string>();
                Set(listener, "startProcessorTask", startup.Task);
                Set(listener, "serviceBusClient", new Client(events));
                var shutdown = listener.ShutdownAsync();
                Assert.False(shutdown.IsCompleted);
                Assert.Empty(events);
                startup.SetCanceled();
                await shutdown;
                Assert.Equal(new[] { "client" }, events);
            });
        }

        [Fact]
        public void ManualWorkerDrainIncludesCallbackAndAllowsUiInvoke()
        {
            RunUi(async (form, listener) =>
            {
                var callbackFinished = false;
                var uiUpdated = false;
                Action work = () => form.Invoke(new Action(() => uiUpdated = true));
                var operation = ServiceBusExplorer.UIHelpers.WorkerOperationDrain.Start(
                    callback => work.BeginInvoke(callback, work), result =>
                    {
                        work.EndInvoke(result);
                        callbackFinished = true;
                    });
                await ServiceBusExplorer.UIHelpers.WorkerOperationDrain.DrainAsync(new[] { operation });
                Assert.True(uiUpdated);
                Assert.True(callbackFinished);
            });
        }

        [Fact]
        public void QueueCancelEntryPointWaitsForWorkersBeforeClosingSharedClient()
        {
            RunUi(async (form, listener) =>
            {
                // Supply identity only; do not run MainForm's configuration/environment-loading constructor.
                var main = (ServiceBusExplorer.Forms.MainForm)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(ServiceBusExplorer.Forms.MainForm));
                using var control = new TestQueueControl(main, (_, __) => { }, () => Task.CompletedTask,
                    () => { }, new ServiceBusHelper((_, __) => { }), new QueueInfo("synthetic"));
                var events = new List<string>();
                var worker = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                ((List<Task>)typeof(TestQueueControl).GetField("pendingOperations", flags).GetValue(control)).Add(worker.Task);
                ((List<ServiceBusClient>)typeof(TestQueueControl).GetField("receiverClients", flags).GetValue(control)).Add(new Client(events));
                var cancel = control.CancelActions();
                Assert.False(cancel.IsCompleted);
                Assert.Same(cancel, control.CancelActions());
                Assert.Empty(events);
                worker.SetResult(true);
                await cancel;
                Assert.Equal(new[] { "client" }, events);
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void RunningProcessorKeepsOriginalSettlementPolicyWhenCheckboxChanges(bool automatic)
        {
            RunUi(async (form, listener) =>
            {
                Set(listener, "autoComplete", automatic);
                Set(listener, "receiveMode", ReceiveMode.PeekLock);
                var checkbox = (CheckBox)typeof(ListenerControl).GetField("checkBoxAutoComplete", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(listener);
                checkbox.Checked = automatic;
                listener.CaptureSettlementPolicy();
                checkbox.Checked = !automatic;
                Set(listener, "receiveMode", ReceiveMode.ReceiveAndDelete);
                var completed = false;
                await listener.ProcessReceivedMessageAsync(Message(), () => { completed = true; return Task.CompletedTask; }, CancellationToken.None, true);
                Assert.Equal(!automatic, completed);
                var options = (ServiceBusSessionProcessorOptions)typeof(ListenerControl)
                    .GetMethod("GetSessionProcessorOptions", BindingFlags.NonPublic | BindingFlags.Instance)
                    .Invoke(listener, new object[] { ServiceBusReceiveMode.PeekLock });
                Assert.Equal(automatic, options.AutoCompleteMessages);
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void QueueCloseWaitsForManagersFinalUiWorkEvenAfterCancellation(bool fail)
        {
            RunUi(async (form, listener) =>
            {
                var main = (ServiceBusExplorer.Forms.MainForm)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(ServiceBusExplorer.Forms.MainForm));
                using var control = new TestQueueControl(main, (_, __) => { }, () => Task.CompletedTask,
                    () => { }, new ServiceBusHelper((_, __) => { }), new QueueInfo("synthetic"));
                form.Controls.Add(control);
                var handle = control.Handle;
                using var completed = new ManualResetEventSlim(true);
                using var cancellation = new CancellationTokenSource();
                var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                var events = new List<string>();
                var uiThread = Thread.CurrentThread.ManagedThreadId;
                var manager = ServiceBusExplorer.UIHelpers.WorkerOperationDrain.RunManagerAsync(control, completed, cancellation.Token, async () =>
                {
                    Assert.Equal(uiThread, Thread.CurrentThread.ManagedThreadId);
                    entered.SetResult(true);
                    await release.Task;
                    events.Add("ui");
                    if (fail) throw new InvalidOperationException("synthetic refresh failure");
                });
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                typeof(TestQueueControl).GetField("managerCancellationTokenSource", flags).SetValue(control, cancellation);
                ((List<Task>)typeof(TestQueueControl).GetField("pendingOperations", flags).GetValue(control)).Add(manager);
                ((List<ServiceBusClient>)typeof(TestQueueControl).GetField("receiverClients", flags).GetValue(control)).Add(new Client(events));
                await entered.Task;
                var close = control.CancelActions();
                Assert.False(close.IsCompleted);
                Assert.Same(close, control.CancelActions());
                Assert.Empty(events);
                release.SetResult(true);
                await close;
                Assert.Equal(new[] { "ui", "client" }, events);
                Assert.Equal(fail, manager.IsFaulted);
            });
        }

        [Fact]
        public void CancelledManagerDoesNotQueueFinalRefresh()
        {
            RunUi(async (form, listener) =>
            {
                using var completed = new ManualResetEventSlim(false);
                using var cancellation = new CancellationTokenSource();
                var invoked = false;
                var manager = ServiceBusExplorer.UIHelpers.WorkerOperationDrain.RunManagerAsync(form, completed, cancellation.Token,
                    () => { invoked = true; return Task.CompletedTask; });
                cancellation.Cancel();
                await manager;
                Assert.False(invoked);
            });
        }

        [Theory]
        [InlineData("queue", false)]
        [InlineData("queue", true)]
        [InlineData("topic", false)]
        [InlineData("topic", true)]
        [InlineData("subscription", false)]
        [InlineData("subscription", true)]
        public void PendingManualStartCannotResumeAfterPermanentClose(string kind, bool close)
        {
            RunUi(async (form, listener) =>
            {
                using var control = CreateManualControl(kind);
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                var type = control.GetType();
                var release = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                ((List<Task>)type.GetField("pendingOperations", flags).GetValue(control)).Add(release.Task);
                var start = (Task<bool>)type.GetMethod("PrepareStartAsync", flags).Invoke(control, null);
                Assert.False(start.IsCompleted);
                var stop = (Task)type.GetMethod(close ? "ShutdownAsync" : "CancelActions", flags).Invoke(control, null);
                Assert.False(stop.IsCompleted);
                release.SetResult(true);
                Assert.Equal(!close, await start);
                await stop;
                Assert.Equal(!close, await (Task<bool>)type.GetMethod("PrepareStartAsync", flags).Invoke(control, null));
            });
        }

        [Theory]
        [InlineData("queue", false)]
        [InlineData("topic", false)]
        [InlineData("subscription", false)]
        [InlineData("subscription", true)]
        public void ManualWorkerStartupFaultAlwaysReleasesAccounting(string kind, bool synchronous)
        {
            RunUi(async (form, listener) =>
            {
                using var control = CreateManualControl(kind);
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                var type = control.GetType();
                using var completed = new ManualResetEventSlim(false);
                type.GetField("managerResetEvent", flags).SetValue(control, completed);
                type.GetField("actionCount", flags).SetValue(control, 1);
                Exception reported = null;
                Action finished = () => type.GetMethod("WorkerFinished", flags).Invoke(control, null);
                Action<Exception> report = error => { reported = error; throw new InvalidOperationException("log failed"); };
                Action work = () => throw new InvalidOperationException("synthetic startup fault");
                var operation = ServiceBusExplorer.UIHelpers.WorkerOperationDrain.Start(
                    callback => synchronous ? throw new InvalidOperationException("synthetic startup fault") : work.BeginInvoke(callback, work),
                    result => ServiceBusExplorer.UIHelpers.WorkerOperationDrain.Complete(() => work.EndInvoke(result), report, finished),
                    error => ServiceBusExplorer.UIHelpers.WorkerOperationDrain.Complete(() => { throw error; }, report, finished));
                if (synchronous) await Assert.ThrowsAsync<InvalidOperationException>(() => operation);
                else await operation;
                Assert.Equal("synthetic startup fault", reported.Message);
                Assert.True(completed.IsSet);
                Assert.Equal(0, type.GetField("actionCount", flags).GetValue(control));
            });
        }

        private static Control CreateManualControl(string kind)
        {
            var main = (ServiceBusExplorer.Forms.MainForm)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(ServiceBusExplorer.Forms.MainForm));
            var helper = new ServiceBusHelper((_, __) => { });
            if (kind == "queue") return new TestQueueControl(main, (_, __) => { }, () => Task.CompletedTask, () => { }, helper, new QueueInfo("synthetic"));
            var topic = new TopicDescription("synthetic");
            var subscription = new SubscriptionDescription("synthetic", "subscription");
            if (kind == "topic") return new TestTopicControl(main, (_, __) => { }, () => Task.CompletedTask, () => { }, helper, topic, new List<SubscriptionInfo> { subscription });
            return new TestSubscriptionControl(main, (_, __) => { }, () => Task.CompletedTask, () => { }, helper, new SubscriptionWrapper(subscription, topic));
        }

        private static ServiceBusReceivedMessage Message() => ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("body"), messageId: "id", sessionId: "session", sequenceNumber: 7);
        private class FailedInspector : IBrokeredMessageInspector
        {
            public BrokeredMessage BeforeSendMessage(BrokeredMessage message) => message;
            public BrokeredMessage AfterReceiveMessage(BrokeredMessage message) => throw new InvalidOperationException("inspector failed");
        }
        private class Processor : ServiceBusSessionProcessor
        {
            private readonly Task drain;
            private readonly TaskCompletionSource<bool> entered;
            private readonly List<string> events;
            public Processor(Task drain, TaskCompletionSource<bool> entered, List<string> events) { this.drain = drain; this.entered = entered; this.events = events; }
            public override Task StopProcessingAsync(CancellationToken cancellationToken = default) { entered.SetResult(true); return drain; }
            public override Task CloseAsync(CancellationToken cancellationToken = default) { events.Add("processor"); return Task.CompletedTask; }
        }
        private class Client : ServiceBusClient
        {
            private readonly List<string> events;
            public Client(List<string> events) { this.events = events; }
            public override ValueTask DisposeAsync() { events.Add("client"); return default; }
        }
        private static FieldInfo Field(string name) => typeof(ListenerControl).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
        private static void Set(object target, string name, object value) => Field(name).SetValue(target, value);
        private static void RunUi(Func<Form, ListenerControl, Task> action)
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try
                {
                    using var form = new Form();
                    using var listener = new ListenerControl();
                    typeof(ListenerControl).GetMethod("InitializeComponent", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(listener, null);
                    Set(listener, "writeToLog", new WriteToLogDelegate((_, __) => { }));
                    Set(listener, "cancellationTokenSource", new CancellationTokenSource());
                    Set(listener, "blockingCollection", new BlockingCollection<Tuple<long, long, long>>());
                    form.Controls.Add(listener);
                    using var timeout = new System.Windows.Forms.Timer { Interval = 10000 };
                    timeout.Tick += (_, __) => { failure = new TimeoutException("Listener UI failed to drain."); form.Close(); };
                    form.Shown += async (_, __) =>
                    {
                        timeout.Start();
                        try { await action(form, listener); await listener.ShutdownAsync(); }
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
    }
}
