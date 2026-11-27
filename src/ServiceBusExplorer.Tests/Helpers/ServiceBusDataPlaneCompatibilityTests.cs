using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Azure.Core.Amqp;
using Azure.Messaging.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.UIHelpers;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusDataPlaneCompatibilityTests
    {
        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void AsyncSendAndSettlementHonorCommitChoice(bool settlement, bool commit)
        {
            using var cancel = new CancellationTokenSource();
            var operation = new TransactionalOperation();
            var helper = Helper();
            if (settlement)
            {
                var reader = new Receiver(Message(1), null) { Settlement = operation.ApplyAsync };
                var committed = ServiceBusHelper.ExecuteReceiveTransaction(() =>
                    Receive(helper, reader, null, false, cancel), commit, cancel.Token);
                Assert.Equal(commit, committed);
                Assert.Equal(new long[] { 1 }, reader.Completed);
            }
            else
            {
                using var template = Template("transaction");
                var sender = new Sender { Send = operation.ApplyAsync };
                using (var scope = ServiceBusHelper.CreateDataPlaneTransactionScope())
                {
                    Assert.True(Send(helper, sender, template, 1, BodyType.String, cancel));
                    if (commit) scope.Complete();
                }
            }
            Assert.True(operation.Enlisted);
            Assert.Equal(commit, operation.Applied);
        }

        [Fact]
        public void ConversionFailureRetainsNativeMessageAndBothNeighbors()
        {
            var bad = Message(22);
            bad.GetRawAmqpMessage().ApplicationProperties["native"] = "retained";
            bad.GetRawAmqpMessage().Header.TimeToLive = TimeSpan.Zero;
            var reader = new Receiver { Batch = new[] { Message(21), bad, Message(23) }, Mode = ServiceBusReceiveMode.ReceiveAndDelete };
            var error = Assert.Throws<PartialMessageReadException>(() =>
                ServiceBusHelper.ReadMessagesFromModernReceiver(reader, false, 3, null, TimeSpan.Zero, null));
            try
            {
                Assert.Equal(new long[] { 21, 22, 23 }, error.Messages.Select(ServiceBusReceivedMessageAdapter.GetSourceSequenceNumber));
                Assert.Equal(reader.Batch, error.Messages.Select(ServiceBusReceivedMessageAdapter.GetReceivedMetadata));
                Assert.Equal("body22", ServiceBusReceivedMessageAdapter.GetReceivedMetadata(error.Messages[1]).Body.ToString());
                Assert.Equal(new[] { "body21", "body23" }, new[] { error.Messages[0], error.Messages[2] }.Select(m => ServiceBusMessageAdapter.ToServiceBusMessage(m).Body.ToString()));
                using var repaired = ServiceBusReceivedMessageAdapter.CloneForResubmission(error.Messages[1]);
                repaired.Label = "repair";
                repaired.SetBodyStream(new MemoryStream(new byte[] { 9 }));
                var outbound = ServiceBusMessageAdapter.ToServiceBusMessage(repaired);
                Assert.Equal("repair", outbound.Subject);
                Assert.Equal("retained", outbound.ApplicationProperties["native"]);
                Assert.Equal(TimeSpan.Zero, outbound.TimeToLive);
                Assert.Equal(new byte[] { 9 }, outbound.Body.ToArray());
                Assert.Single(reader.Waits);
            }
            finally { foreach (var row in error.Messages) row.Dispose(); }
        }

        [Fact]
        public void AbsentAmqpDeliveryCountUsesItsDefault()
        {
            var message = Message(7);
            message.GetRawAmqpMessage().Header.DeliveryCount = null;
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(message);
            Assert.Equal(0, row.DeliveryCount);
            Assert.Equal(7, row.SequenceNumber);
        }

        [Theory]
        [InlineData(1, "")]
        [InlineData(1, "héllo 👋")]
        [InlineData(3, "héllo 👋")]
        public void StringTesterSendsExactAmqpValueForSingleAndBatch(int count, string text)
        {
            using var cancel = new CancellationTokenSource();
            using var template = Template(text);
            var sender = new Sender();
            Assert.True(Send(Helper(), sender, template, count, BodyType.String, cancel));
            var sent = Assert.Single(sender.Groups);
            Assert.Equal(count, sent.Count);
            Assert.All(sent, item => ServiceBusAmqpBodyTests.AssertBody(AmqpMessageBody.FromValue(text), item.GetRawAmqpMessage().Body));
        }

        [Fact]
        public void StringSenderHonorsInspectorStreamReplacement()
        {
            using var cancel = new CancellationTokenSource();
            using var template = Template("replace me");
            var bytes = new byte[] { 0, 255, 40 };
            var inspector = new StreamSenderInspector(bytes);
            var sender = new Sender();
            Assert.True(Send(Helper(), sender, template, 1, BodyType.String, cancel, inspector));
            var message = Assert.Single(Assert.Single(sender.Groups));
            ServiceBusAmqpBodyTests.AssertBody(AmqpMessageBody.FromData(new[] { new ReadOnlyMemory<byte>(bytes) }),
                message.GetRawAmqpMessage().Body);
        }

        [Fact]
        public void TypedStringInspectorAndUnchangedResendSeeOriginalValue()
        {
            const string text = "plain string with ü and quotes \"here\"";
            var raw = new AmqpAnnotatedMessage(AmqpMessageBody.FromValue(text));
            var native = ServiceBusReceivedMessage.FromAmqpMessage(raw, new BinaryData(Guid.Empty.ToByteArray()));
            var reader = new Receiver { Batch = new[] { native }, Mode = ServiceBusReceiveMode.ReceiveAndDelete };
            var inspector = new StringInspector();
            using var row = Assert.Single(ServiceBusHelper.ReadMessagesFromModernReceiver(reader, false, 1, null, TimeSpan.Zero, inspector));
            Assert.Equal(text, inspector.Text);
            ServiceBusAmqpBodyTests.AssertBody(raw.Body, ServiceBusMessageAdapter.ToServiceBusMessage(row).GetRawAmqpMessage().Body);
        }

        [Fact]
        public void ScheduledPeekEnablesCancellationOnlyForScheduledSelection()
        {
            var scheduled = ServiceBusModelFactory.ServiceBusReceivedMessage(sequenceNumber: 5, serviceBusMessageState: ServiceBusMessageState.Scheduled);
            var deferred = ServiceBusModelFactory.ServiceBusReceivedMessage(sequenceNumber: 6, serviceBusMessageState: ServiceBusMessageState.Deferred);
            var reader = new Receiver { Batch = new[] { scheduled, deferred } };
            var rows = ServiceBusHelper.ReadMessagesFromModernReceiver(reader, true, 2, null, TimeSpan.Zero, null);
            try
            {
                Assert.Equal(MessageState.Scheduled, rows[0].State);
                Assert.Equal(MessageState.Deferred, rows[1].State);
                Assert.True(HandleQueueControl.CanCancelScheduledMessages(rows.Take(1)));
                Assert.False(HandleQueueControl.CanCancelScheduledMessages(rows));
                using var replacement = ServiceBusReceivedMessageAdapter.Inspect(rows[0], _ => Template("edited"));
                Assert.True(HandleQueueControl.CanCancelScheduledMessages(new[] { replacement }));
            }
            finally { rows.ForEach(row => row.Dispose()); }
        }

        [Theory]
        [InlineData(false, false, ReceiveMode.PeekLock)]
        [InlineData(true, true, ReceiveMode.ReceiveAndDelete)]
        public void NonSessionSubscriptionUsesModernEntityAndSubqueue(bool newFactory, bool deadLetter, ReceiveMode mode)
        {
            var helper = Helper();
            var profile = ServiceBusNamespace.GetServiceBusNamespace("local",
                "Endpoint=sb://localhost:5672;SharedAccessKeyName=test;SharedAccessKey=synthetic;UseDevelopmentEmulator=true;", (_, __) => { });
            typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(helper, profile);
            Assert.True(helper.IsEmulator);
            Assert.Null(helper.GetSessionTestFactory(false, newFactory));
            Assert.Throws<NotSupportedException>(() => helper.GetSessionTestFactory(true, newFactory));
            var client = new Client(new Receiver());
            ServiceBusHelper.CreateSubscriptionTestReceiver(client, "orders", "audit", mode, 17, deadLetter);
            Assert.Equal("orders", client.Topic);
            Assert.Equal("audit", client.Subscription);
            Assert.Equal(17, client.Options.PrefetchCount);
            Assert.Equal(deadLetter ? SubQueue.DeadLetter : SubQueue.None, client.Options.SubQueue);
            Assert.Equal(mode == ReceiveMode.PeekLock ? ServiceBusReceiveMode.PeekLock : ServiceBusReceiveMode.ReceiveAndDelete, client.Options.ReceiveMode);
        }

        [Fact]
        public void DeferredDrainNeverReceivesANewLiveMessage()
        {
            using var cancel = new CancellationTokenSource();
            var reader = new Receiver(Message(1), null, Message(99));
            Assert.True(Receive(Helper(), reader, new SqlFilter("1=1").Preprocess(), true, cancel));
            Assert.Equal(2, reader.Waits.Count);
            Assert.Equal(new long[] { 1 }, reader.Deferred);
            Assert.Equal(new long[] { 1 }, reader.Completed);
            Assert.Equal(new long[] { 1 }, reader.RequestedDeferred);
        }

        [Fact]
        public void DisabledRoutingFilterCannotAbortDestructiveBatch()
        {
            using var cancel = new CancellationTokenSource();
            var reader = new Receiver { Batch = new[] { Message(1), Message(2) }, Mode = ServiceBusReceiveMode.ReceiveAndDelete };
            var filter = new SqlFilter("quantity / divisor > 0").Preprocess();
            foreach (var message in reader.Batch)
            {
                message.GetRawAmqpMessage().ApplicationProperties["quantity"] = 1;
                message.GetRawAmqpMessage().ApplicationProperties["divisor"] = 0;
            }
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(reader.Batch[0]);
            Assert.ThrowsAny<Exception>(() => filter.Match(row));
            long reported = 0;
            Assert.True(Helper().ReceiveMessages(reader, reader.Mode, false, 0, 0, filter, false, false, false,
                false, false, true, true, 2, false, 0, null, (n, t, dir) => reported += n, cancel, out var trace), trace);
            Assert.Equal(2, reported);
            Assert.Equal(2, reader.Waits.Count);
        }

        [Theory]
        [InlineData(nameof(BrokeredMessage.PartitionKey))]
        [InlineData(nameof(BrokeredMessage.SessionId))]
        [InlineData(nameof(BrokeredMessage.ViaPartitionKey))]
        public void RoutingBoundariesPreserveOrderAndAcknowledgedCounts(string routingProperty)
        {
            var sender = new Sender();
            var rows = new[] { "A", "A", "B", "A" }.Select((key, index) =>
            {
                var row = Template(index.ToString());
                typeof(BrokeredMessage).GetProperty(routingProperty).SetValue(row, key);
                row.MessageId = index.ToString();
                return row;
            }).ToList();
            try
            {
                var counts = new List<int>();
                ServiceBusHelper.SendServiceBusBatch(sender, rows, CancellationToken.None, (n, time) => counts.Add(n));
                Assert.Equal(new[] { 2, 1, 1 }, counts);
                Assert.Equal(new[] { "0,1", "2", "3" }, sender.Groups.Select(batch => string.Join(",", batch.Select(m => m.MessageId))));
            }
            finally { rows.ForEach(row => row.Dispose()); }
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(3, 3)]
        public async Task BrowserTesterAndDeadLetterUseSupportedWait(int configured, int expected)
        {
            var reader = new Receiver { Batch = new[] { Message(4) }, Mode = ServiceBusReceiveMode.ReceiveAndDelete };
            using var row = Assert.Single(ServiceBusHelper.ReadMessagesFromModernReceiver(reader, false, 1, null, TimeSpan.FromSeconds(configured), null));
            using var cancel = new CancellationTokenSource();
            var tester = new Receiver();
            Assert.True(Receive(Helper(), tester, null, false, cancel, configured));
            var dlq = new Receiver(Message(4));
            var result = await new Handler(new Client(dlq), configured).DeleteMessages(new List<long?> { 4 }, false);
            Assert.Null(result.Failure);
            Assert.Equal(new long[] { 4 }, result.DeletedSequenceNumbers);
            var moveReader = new Receiver();
            var move = await new Handler(new Client(moveReader), configured).MoveMessages("target", new List<long> { 4 }, false);
            Assert.Null(move.Failure);
            Assert.Empty(move.DeletedSequenceNumbers);
            foreach (var source in new[] { reader, tester, dlq, moveReader })
                Assert.Equal(TimeSpan.FromSeconds(expected), Assert.Single(source.Waits));
        }

        [Fact]
        public async Task StopInvalidatesSenderCacheBeforeClosingClients()
        {
            var first = new PendingClient();
            var second = new PendingClient();
            var owners = new List<ServiceBusClient> { first, second };
            var receiver = new PendingClient();
            var receivers = new List<ServiceBusClient> { receiver };
            var cache = new List<ServiceBusSender> { new Sender() };
            var run = cache;
            var failures = new List<Exception>();
            var stop = TestControlHelper.StopClients(ref cache, owners, receivers, failures.Add);
            Assert.Null(cache);
            Assert.Empty(owners);
            Assert.Empty(receivers);
            Assert.Single(run);
            Assert.False(stop.IsCompleted);
            var restarted = new PendingClient();
            owners.Add(restarted);
            receivers.Add(restarted);
            receiver.Closed.SetResult(true);
            first.Closed.SetException(new InvalidOperationException("synthetic cleanup failure"));
            second.Closed.SetResult(true);
            await stop;
            Assert.Single(failures);
            Assert.Equal(1, first.Closes);
            Assert.Equal(1, second.Closes);
            Assert.Equal(1, receiver.Closes);
            Assert.Equal(0, restarted.Closes);
            Assert.Same(restarted, Assert.Single(owners));
            Assert.Same(restarted, Assert.Single(receivers));
        }

        private sealed class PendingClient : ServiceBusClient
        {
            public int Closes;
            public readonly TaskCompletionSource<bool> Closed = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            public override ValueTask DisposeAsync() { Closes++; return new ValueTask(Closed.Task); }
        }

        private static ServiceBusReceivedMessage Message(long id) =>
            ServiceBusModelFactory.ServiceBusReceivedMessage(body: BinaryData.FromString("body" + id), sequenceNumber: id, messageId: "id" + id);

        private static BrokeredMessage Template(string text) => new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(text)), true);

        private static ServiceBusHelper Helper()
        {
            var helper = new ServiceBusHelper((_, __) => { });
            typeof(ServiceBusHelper).GetField("namespaceUri", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(helper, new Uri("sb://unit.servicebus.windows.net/"));
            return helper;
        }

        private static bool Send(ServiceBusHelper helper, Sender sender, BrokeredMessage row, int count, BodyType type, CancellationTokenSource cancel, IBrokeredMessageInspector inspector = null)
        {
            long next = -1;
            var ok = helper.SendMessages(sender, "queue", new[] { row }, () => ++next, count, 0, false, false, false, false,
                false, false, count > 1, false, count, false, 0, type, inspector, null, cancel, out var trace);
            Assert.True(ok, trace);
            return ok;
        }

        private static bool Receive(ServiceBusHelper helper, Receiver reader, Filter filter, bool defer, CancellationTokenSource cancel, int wait = 0) =>
            helper.ReceiveMessages(reader, ServiceBusReceiveMode.PeekLock, false, 0, wait, filter, false, true, defer,
                false, false, false, false, 1, false, 0, null, null, cancel, out _);

        private sealed class Sender : ServiceBusSender
        {
            public readonly List<List<ServiceBusMessage>> Groups = new List<List<ServiceBusMessage>>();
            private readonly Dictionary<ServiceBusMessageBatch, List<ServiceBusMessage>> batches = new Dictionary<ServiceBusMessageBatch, List<ServiceBusMessage>>();
            public Func<Task> Send;
            public override ValueTask<ServiceBusMessageBatch> CreateMessageBatchAsync(CancellationToken cancellationToken = default)
            {
                var rows = new List<ServiceBusMessage>();
                var batch = ServiceBusModelFactory.ServiceBusMessageBatch(0, rows, tryAddCallback: _ => true);
                batches.Add(batch, rows);
                return new ValueTask<ServiceBusMessageBatch>(batch);
            }
            public override Task SendMessagesAsync(ServiceBusMessageBatch batch, CancellationToken cancellationToken = default)
            {
                Groups.Add(batches[batch].ToList());
                return Send?.Invoke() ?? Task.CompletedTask;
            }
            public override ValueTask DisposeAsync() => default;
            public override Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken = default)
            {
                Groups.Add(new List<ServiceBusMessage> { message });
                return Send?.Invoke() ?? Task.CompletedTask;
            }
        }

        private sealed class Receiver : ServiceBusReceiver
        {
            public ServiceBusReceiveMode Mode;
            public ServiceBusReceivedMessage[] Batch = Array.Empty<ServiceBusReceivedMessage>();
            private readonly Queue<ServiceBusReceivedMessage> live;
            public readonly List<TimeSpan?> Waits = new List<TimeSpan?>();
            public readonly List<long> Deferred = new List<long>();
            public readonly List<long> RequestedDeferred = new List<long>();
            public readonly List<long> Completed = new List<long>();
            public Func<Task> Settlement;
            public Receiver(params ServiceBusReceivedMessage[] messages) { live = new Queue<ServiceBusReceivedMessage>(messages); }
            public override ServiceBusReceiveMode ReceiveMode => Mode;
            public override Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
            {
                Waits.Add(maxWaitTime);
                return Task.FromResult(live.Count == 0 ? null : live.Dequeue());
            }
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
            {
                Waits.Add(maxWaitTime);
                return Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(Waits.Count == 1 ? Batch : Array.Empty<ServiceBusReceivedMessage>());
            }
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> PeekMessagesAsync(int maxMessages, long? fromSequenceNumber = null, CancellationToken cancellationToken = default) =>
                Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(Batch);
            public override Task DeferMessageAsync(ServiceBusReceivedMessage message, IDictionary<string, object> propertiesToModify = null, CancellationToken cancellationToken = default)
            {
                Deferred.Add(message.SequenceNumber);
                return Task.CompletedTask;
            }
            public override Task<ServiceBusReceivedMessage> ReceiveDeferredMessageAsync(long sequenceNumber, CancellationToken cancellationToken = default)
            {
                RequestedDeferred.Add(sequenceNumber);
                return Task.FromResult(Message(sequenceNumber));
            }
            public override Task CompleteMessageAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken = default)
            {
                Completed.Add(message.SequenceNumber);
                return Settlement?.Invoke() ?? Task.CompletedTask;
            }
            public override ValueTask DisposeAsync() => default;
        }

        private sealed class Client : ServiceBusClient
        {
            private readonly Receiver receiver;
            public string Topic, Subscription;
            public ServiceBusReceiverOptions Options;
            public Client(Receiver receiver) { this.receiver = receiver; }
            public override ServiceBusReceiver CreateReceiver(string topicName, string subscriptionName, ServiceBusReceiverOptions options = null)
            {
                Topic = topicName;
                Subscription = subscriptionName;
                Options = options;
                return receiver;
            }
            public override ServiceBusReceiver CreateReceiver(string queueName, ServiceBusReceiverOptions options = null) => receiver;
            public override ServiceBusSender CreateSender(string entityPath) => new Sender();
            public override ValueTask DisposeAsync() => default;
        }

        private sealed class Handler : DeadLetterMessageHandler
        {
            private readonly Client client;
            public Handler(Client client, int wait) : base((text, _) => { }, null, wait, new QueueInfo("test") { LockDuration = TimeSpan.FromMinutes(1) }) { this.client = client; }
            protected override ServiceBusClient CreateClient(bool crossEntityTransactions) => client;
        }

        private sealed class StreamSenderInspector : IBrokeredMessageInspector
        {
            private readonly byte[] bytes;
            public StreamSenderInspector(byte[] bytes) { this.bytes = bytes; }
            public BrokeredMessage BeforeSendMessage(BrokeredMessage message)
            {
                message.SetBodyStream(new MemoryStream(bytes));
                return message;
            }
            public BrokeredMessage AfterReceiveMessage(BrokeredMessage message) => message;
        }

        private sealed class StringInspector : IBrokeredMessageInspector
        {
            public string Text;
            public BrokeredMessage BeforeSendMessage(BrokeredMessage message) => message;
            public BrokeredMessage AfterReceiveMessage(BrokeredMessage message)
            {
                using (var copy = message.Clone()) Text = copy.GetBody<string>();
                return message;
            }
        }

        private sealed class TransactionalOperation : IEnlistmentNotification
        {
            public bool Applied, Enlisted;
            public Task ApplyAsync() => Task.Run(() =>
            {
                if (Transaction.Current == null) { Applied = true; return; }
                Enlisted = true;
                Transaction.Current.EnlistVolatile(this, EnlistmentOptions.None);
            });
            public void Prepare(PreparingEnlistment enlistment) => enlistment.Prepared();
            public void Commit(Enlistment enlistment) { Applied = true; enlistment.Done(); }
            public void Rollback(Enlistment enlistment) => enlistment.Done();
            public void InDoubt(Enlistment enlistment) => enlistment.Done();
        }
    }
}
