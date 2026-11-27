using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Azure.Messaging.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusTransactionProgressTests
    {
        [Theory]
        [InlineData(1, 0, ReceiveMode.ReceiveAndDelete)]
        [InlineData(3, 0, ReceiveMode.ReceiveAndDelete)]
        [InlineData(3, 1, ReceiveMode.ReceiveAndDelete)]
        [InlineData(3, 1, ReceiveMode.PeekLock)]
        public void LegacyReceiveAccountsEntireConsumedBatchBeforeInspectorFailure(int count, int failAt, ReceiveMode mode)
        {
            var messages = new List<BrokeredMessage>();
            for (var i = 0; i < count; i++) messages.Add(new BrokeredMessage(new MemoryStream(new byte[] { 1 })));
            var published = 0;
            var expected = mode == ReceiveMode.ReceiveAndDelete ? count : 0;
            var inspector = new FailingBatchInspector(failAt, () => Assert.Equal(expected, published));
            try
            {
                Assert.Throws<InvalidOperationException>(() => ServiceBusHelper.InspectReceivedMessages(messages, mode, inspector, n => published += n));
                Assert.Equal(expected, published);
                Assert.Equal(failAt + 1, inspector.Calls);
            }
            finally { messages.ForEach(message => message.Dispose()); }
        }

        private sealed class FailingBatchInspector : IBrokeredMessageInspector
        {
            private readonly int failAt;
            private readonly Action check;
            public int Calls;
            public FailingBatchInspector(int failAt, Action check) { this.failAt = failAt; this.check = check; }
            public BrokeredMessage BeforeSendMessage(BrokeredMessage message) => message;
            public BrokeredMessage AfterReceiveMessage(BrokeredMessage message)
            {
                check();
                if (Calls++ == failAt) throw new InvalidOperationException("synthetic inspector failure");
                return message;
            }
        }

        [Theory]
        [InlineData(true, true, false, true)]
        [InlineData(false, true, false, false)]
        [InlineData(true, false, false, false)]
        [InlineData(true, true, true, false)]
        public void OnlySuccessfulRequestedUncancelledTransactionPublishesProgress(bool success, bool commit, bool cancelled, bool expected)
        {
            int published = 0;
            var committed = ServiceBusHelper.ExecuteReceiveTransaction(() =>
            {
                ServiceBusHelper.PublishConfirmedProgress(() => published++);
                Assert.Equal(0, published);
                return success;
            }, commit, new CancellationToken(cancelled));
            Assert.Equal(expected, committed);
            Assert.Equal(expected ? 1 : 0, published);
        }

        [Theory]
        [InlineData(false, 1)]
        [InlineData(true, 0)]
        public void LaterSendFailureKeepsOnlyConfirmedNontransactionalProgress(bool transaction, int expected)
        {
            using var sender = new FailingSender();
            using var template = new BrokeredMessage(new MemoryStream(new byte[] { 1 })) { MessageId = "test" };
            using var cancellation = new CancellationTokenSource();
            var helper = new ServiceBusHelper((_, __) => { });
            typeof(ServiceBusHelper).GetField("namespaceUri", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(helper, new Uri("sb://test.servicebus.windows.net/"));
            long next = -1;
            long published = 0;
            Func<bool> send = () => helper.SendMessages(sender, "queue", new[] { template }, () => ++next,
                3, 0, false, false, false, false, false, true, true, true, 3, false, 0, BodyType.Stream,
                null, (count, elapsed, direction) => published += count, cancellation, out _);
            if (transaction) Assert.False(ServiceBusHelper.ExecuteReceiveTransaction(send, true, CancellationToken.None));
            else Assert.False(send());
            Assert.Equal(expected, published);
            Assert.Equal(2, sender.Sends);
        }

        [Theory]
        [InlineData(false, false, 1)]
        [InlineData(true, false, 0)]
        [InlineData(false, true, 2)]
        public void FailedProcessingReportsConfirmedSettlementsOrDestructiveBatch(bool transaction, bool receiveAndDelete, int expected)
        {
            var receiver = new FailingReceiver();
            var helper = new ServiceBusHelper((_, __) => { });
            using var cancellation = new CancellationTokenSource();
            long published = 0;
            Func<bool> receive = () => helper.ReceiveMessages(receiver,
                receiveAndDelete ? ServiceBusReceiveMode.ReceiveAndDelete : ServiceBusReceiveMode.PeekLock,
                false, 0, 1, null, false, true, false, false, false, true, true, 2, false, 0,
                new FailingInspector(), (count, elapsed, direction) => published += count, cancellation, out _);
            if (transaction) Assert.False(ServiceBusHelper.ExecuteReceiveTransaction(receive, true, CancellationToken.None));
            else Assert.False(receive());
            Assert.Equal(expected, published);
            Assert.Equal(receiveAndDelete ? 0 : 1, receiver.Completed);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TransactionalDestructiveReceiveRejectsBeforeSingleOrBatchRead(bool batch)
        {
            var receiver = new FailingReceiver();
            using var scope = new TransactionScope();
            using var cancellation = new CancellationTokenSource();
            var helper = new ServiceBusHelper((_, __) => { });
            Assert.Throws<NotSupportedException>(() => helper.ReceiveMessages(receiver, ServiceBusReceiveMode.ReceiveAndDelete,
                false, 0, 1, null, false, false, false, false, false, false, batch, 2, false, 0, null, null, cancellation, out _));
            Assert.Equal(0, receiver.Reads);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void LegacySettlementBoundaryPublishesOnlyAfterSuccessfulAcknowledgment(bool fail)
        {
            long count = 0;
            Action settle = () => { Assert.Equal(0, count); if (fail) throw new InvalidOperationException("synthetic settlement failure"); };
            Action run = () => ServiceBusHelper.SettleAndPublishReceive(settle, (number, elapsed, direction) => count += number, 1);
            if (fail) Assert.Throws<InvalidOperationException>(run);
            else run();
            Assert.Equal(fail ? 0 : 1, count);
        }

        [Fact]
        public void RequestedTransactionRejectsDestructiveModeEvenWithoutAmbientScope()
        {
            Assert.Throws<NotSupportedException>(() => ServiceBusHelper.ValidateReceiveTransaction(true, true));
            ServiceBusHelper.ValidateReceiveTransaction(false, true);
            ServiceBusHelper.ValidateReceiveTransaction(true, false);
        }

        private sealed class FailingSender : ServiceBusSender, IDisposable
        {
            public int Sends;
            public override ValueTask<ServiceBusMessageBatch> CreateMessageBatchAsync(CancellationToken cancellationToken = default)
            {
                var store = new List<ServiceBusMessage>();
                return new ValueTask<ServiceBusMessageBatch>(ServiceBusModelFactory.ServiceBusMessageBatch(
                    0, store, tryAddCallback: _ => store.Count == 0));
            }
            public override Task SendMessagesAsync(ServiceBusMessageBatch batch, CancellationToken cancellationToken = default)
            {
                if (++Sends == 2) throw new InvalidOperationException("synthetic send failure");
                return Task.CompletedTask;
            }
            public void Dispose() { }
        }
        private sealed class FailingReceiver : ServiceBusReceiver
        {
            public int Completed, Reads;
            public override Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
            { Reads++; return Task.FromResult(ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: "single")); }
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages,
                TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default) =>
                ReadBatch();
            private Task<IReadOnlyList<ServiceBusReceivedMessage>> ReadBatch()
            {
                Reads++;
                return Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(new[] {
                    ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: "first"),
                    ServiceBusModelFactory.ServiceBusReceivedMessage(messageId: "fail") });
            }
            public override Task CompleteMessageAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken = default)
            { Completed++; return Task.CompletedTask; }
            public override Task AbandonMessageAsync(ServiceBusReceivedMessage message,
                IDictionary<string, object> propertiesToModify = null, CancellationToken cancellationToken = default) => Task.CompletedTask;
        }
        private sealed class FailingInspector : IBrokeredMessageInspector
        {
            public BrokeredMessage BeforeSendMessage(BrokeredMessage message) => message;
            public BrokeredMessage AfterReceiveMessage(BrokeredMessage message) => message.MessageId == "fail"
                ? throw new InvalidOperationException("synthetic processing failure") : message;
        }
    }
}
