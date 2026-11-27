using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Messaging.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.UIHelpers;
using ServiceBusExplorer.UIHelpers.Theming;
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

        [Theory]
        [InlineData("queue")]
        [InlineData("topic")]
        [InlineData("subscription")]
        public void EarlyWorkerCompletionCannotFinishRunWhileRegistrationIsOpen(string kind)
        {
            RunUi(async (form, listener) =>
            {
                using var control = CreateManualControl(kind);
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                var type = control.GetType();
                using var completed = new ManualResetEventSlim(false);
                type.GetField("managerResetEvent", flags).SetValue(control, completed);
                Action<string> invoke = name => type.GetMethod(name, flags).Invoke(control, null);
                invoke("BeginWorkerRegistration");
                invoke("RegisterWorker");
                invoke("WorkerFinished"); // first worker completes before later workers start
                Assert.False(completed.IsSet);
                invoke("RegisterWorker");
                invoke("EndWorkerRegistration");
                Assert.False(completed.IsSet);
                invoke("WorkerFinished");
                Assert.True(completed.IsSet);
                Assert.Equal(0, type.GetField("actionCount", flags).GetValue(control));
                await Task.CompletedTask;
            });
        }

        [Theory]
        [InlineData("queue")]
        [InlineData("topic")]
        public async Task NaturalSendRestartDiscardsPreviousClientsAndSendersAfterWorkersDrain(string kind)
        {
            var type = kind == "queue" ? typeof(TestQueueControl) : typeof(TestTopicControl);
            // Supply the run state directly so the real restart/drain can be tested without a window or connection.
            var panel = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(type);
            var main = (MainForm)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(MainForm));
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            void SetRunField(string name, object value) => type.GetField(name, flags).SetValue(panel, value);
            var senders = new List<ServiceBusSender> { new Sender() };
            var disposed = new List<string>();
            var clients = new List<ServiceBusClient> { new Client(disposed) };
            var worker = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            SetRunField("messageSenderCollection", senders);
            SetRunField("senderClients", clients);
            SetRunField("receiverClients", new List<ServiceBusClient>());
            SetRunField("pendingOperations", new List<Task> { worker.Task });
            SetRunField("controlHelper", new TestControlHelper(main, (_, __) => { }, null, null, null));

            var restart = (Task<bool>)type.GetMethod("PrepareStartAsync", flags).Invoke(panel, null);
            Assert.False(restart.IsCompleted);
            Assert.Single(senders);
            Assert.Empty(disposed);
            worker.SetResult(true);
            Assert.True(await restart);
            // Cleanup invalidates the cache so the next start constructs fresh senders.
            Assert.Null(type.GetField("messageSenderCollection", flags).GetValue(panel));
            Assert.Empty(senders);
            Assert.Empty(clients);
            Assert.Equal(new[] { "client" }, disposed);
        }

        [Theory]
        [InlineData("Queue")]
        [InlineData("Topic")]
        [InlineData("Subscription")]
        public void ReplacingAnUnusedMainFormTestPanelKeepsTheSharedLogRunning(string kind)
        {
            RunUi(async (form, listener) =>
            {
                // Only supply the hosting and log fields; the configuration-loading constructor is unnecessary.
                var main = (MainForm)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(MainForm));
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                void SetMain(string name, object value) => typeof(MainForm).GetField(name, flags).SetValue(main, value);
                using var host = new HeaderPanel();
                form.Controls.Add(host);
                ThemeManager.SetHostedBackground(host);
                using var logCancellation = new CancellationTokenSource();
                using var log = new BlockingCollection<string>();
                SetMain("panelMain", host);
                SetMain("panelLog", host);
                SetMain("serviceBusHelper", new ServiceBusHelper((_, __) => { }));
                SetMain("cancellationTokenSource", logCancellation);
                SetMain("logCollection", log);
                SetMain("logTask", Task.CompletedTask);
                var topic = new TopicInfo("synthetic");
                var sub = new SubscriptionInfo(topic.Path, "sub");
                var args = kind == "Queue" ? new object[] { new QueueInfo("synthetic"), true }
                    : kind == "Topic" ? new object[] { topic, new List<SubscriptionInfo> { sub }, true }
                    : new object[] { new SubscriptionWrapper(sub, topic), true };
                typeof(MainForm).GetMethod("Test" + kind, flags).Invoke(main, args);
                var panel = Assert.IsAssignableFrom<UserControl>(Assert.Single(host.Controls));
                Assert.Equal("Test" + kind + "Control", panel.GetType().Name);
                var helper = (TestControlHelper)panel.GetType().GetField("controlHelper", flags).GetValue(panel);

                ThemeManager.ReplaceHostedContent(host, () => new Panel(), null);
                await (Task)panel.GetType().GetMethod("ShutdownAsync", flags).Invoke(panel, null);
                Assert.True(panel.IsDisposed);
                Assert.False(logCancellation.IsCancellationRequested);
                Assert.Null(helper.StartLog);
                helper.WriteToLog("still running");
                Assert.Contains("still running", log);
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task BrowsedStateUsesTheSelectedReadAndIgnoresStaleResults(bool oldReadFails)
        {
            var text = "previous row";
            var errors = new List<Exception>();
            var view = new SessionStateView(value => text = value, errors.Add, () => true);
            var first = new TaskCompletionSource<Stream>(TaskCreationOptions.RunContinuationsAsynchronously);
            var next = new TaskCompletionSource<Stream>(TaskCreationOptions.RunContinuationsAsynchronously);
            var old = view.ReadAsync(() => first.Task);
            Assert.Equal(string.Empty, text);
            var current = view.ReadAsync(() => next.Task);
            await Task.Yield();
            Assert.False(current.IsCompleted);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes("selected row"));
            next.SetResult(stream);
            await current;
            Assert.Equal("selected row", text);
            Assert.False(stream.CanRead);
            if (oldReadFails) first.SetException(new InvalidOperationException("stale failure"));
            else first.SetResult(new MemoryStream(Encoding.UTF8.GetBytes("stale state")));
            await old;
            Assert.Equal("selected row", text);
            Assert.Empty(errors);

            var failure = new InvalidOperationException("current failure");
            await view.ReadAsync(() => Task.FromException<Stream>(failure));
            Assert.Same(failure, Assert.Single(errors));
            Assert.Equal(string.Empty, text);
            text = "clear on deselect";
            await view.ShowAsync(null);
            Assert.Equal(string.Empty, text);
        }

        [Fact]
        public async Task ClosingStateViewDiscardsPendingReadAndDisposesItsStream()
        {
            var open = true;
            var displayed = new List<string>();
            var errors = new List<Exception>();
            var view = new SessionStateView(displayed.Add, errors.Add, () => open);
            var pending = new TaskCompletionSource<Stream>(TaskCreationOptions.RunContinuationsAsynchronously);
            var read = view.ReadAsync(() => pending.Task);
            open = false;
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("closed row"));
            pending.SetResult(stream);
            await read;
            Assert.False(stream.CanRead);
            Assert.Equal(new[] { string.Empty }, displayed);
            Assert.Empty(errors);
        }

        [Fact]
        public async Task InspectorFailureReachesTheProcessorAndIsLoggedOnce()
        {
            var log = new List<string>();
            var listener = CallbackListener(log);
            Set(listener, "receiverBrokeredMessageInspector", new FailedInspector());
            var completed = false;
            var failure = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                listener.ProcessReceivedMessageAsync(Message(), () => { completed = true; return Task.CompletedTask; },
                    CancellationToken.None, true));
            Assert.False(completed);
            Assert.Empty(log);
            await listener.OnProcessorErrorAsync(ProcessorError(failure, CancellationToken.None));
            Assert.Equal("Exception: inspector failed", Assert.Single(log));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task ProcessorCancellationIsSilentOnlyDuringStop(bool stopping)
        {
            var log = new List<string>();
            var listener = CallbackListener(log);
            await listener.OnProcessorErrorAsync(ProcessorError(
                new OperationCanceledException("operation cancelled"), new CancellationToken(stopping)));
            if (stopping) Assert.Empty(log);
            else Assert.Equal("Exception: operation cancelled", Assert.Single(log));
        }

        private static ListenerControl CallbackListener(List<string> log)
        {
            // These callback paths use only the inspector and logger, so they need no native window.
            var listener = (ListenerControl)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(ListenerControl));
            Set(listener, "writeToLog", new WriteToLogDelegate((entry, _) => log.Add(entry)));
            return listener;
        }

        private static ProcessErrorEventArgs ProcessorError(Exception failure, CancellationToken token) =>
            new ProcessErrorEventArgs(failure, ServiceBusErrorSource.ProcessMessageCallback,
                "namespace.example", "queue", "processor", token);

        private sealed class Sender : ServiceBusSender { }

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
