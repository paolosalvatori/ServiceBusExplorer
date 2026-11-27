using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusReceiverReadinessTests
    {
        [Fact]
        public void ReceiveAllDrainsBeyondTopCountAndShortBatches()
        {
            var receiver = new Reader();
            var count = ServiceBusHelper.ResolveModernReadCount(false, true, 1, 2);
            var messages = ServiceBusHelper.ReadMessagesFromModernReceiver(receiver, false, count, null, TimeSpan.Zero, null);
            try
            {
                Assert.Equal(new long[] { 11, 12, 13 }, messages.Select(m => m.SequenceNumber));
                Assert.Equal(3, receiver.Calls);
                Assert.All(receiver.Sizes, size => Assert.InRange(size, 1, 100));
            }
            finally { messages.ForEach(m => m.Dispose()); }
        }

        [Fact]
        public void PeekAllRetainsTopLimitAndAdvancesBrokerSequence()
        {
            var receiver = new Reader();
            var count = ServiceBusHelper.ResolveModernReadCount(true, true, 1, 3);
            var messages = ServiceBusHelper.ReadMessagesFromModernReceiver(receiver, true, count, 11, TimeSpan.Zero, null);
            try
            {
                Assert.Equal(3, messages.Count);
                Assert.Equal(new long?[] { 11, 13 }, receiver.Starts);
                Assert.Equal(new[] { 3, 1 }, receiver.Sizes);
            }
            finally { messages.ForEach(m => m.Dispose()); }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task FailedOperationRetainsConfirmedIdsAndAttemptsEveryCleanup(bool move)
        {
            var receiver = new FailingReceiver();
            var client = new FakeClient(receiver);
            var logs = new List<string>();
            var handler = new Handler(client, logs);
            var result = move
                ? await handler.MoveMessages("target", new List<long> { 1, 2 }, false)
                : await handler.DeleteMessages(new List<long?> { 1, 2 }, false);
            Assert.Equal("receive failed", result.Failure);
            Assert.Equal(new long[] { 1 }, result.DeletedSequenceNumbers);
            Assert.True(receiver.Abandoned);
            Assert.True(receiver.Disposed);
            Assert.True(client.Disposed);
            if (move) Assert.True(client.Sender.Disposed);
            Assert.Contains(logs, log => log.Contains("abandon failed"));
            Assert.Contains(logs, log => log.Contains("dispose failed"));
            Assert.Contains("Only 1 of 2", handler.GetFailureExplanation(result, 2, !move));
            Assert.Contains("Check broker state", handler.GetFailureExplanation(result, 2, !move));
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task UnsupportedMoveRejectsBeforeClientCreation(bool transfer, bool partitioned)
        {
            var handler = new Handler(null, new List<string>(), partitioned);
            await Assert.ThrowsAsync<NotSupportedException>(() => handler.MoveMessages("target", new List<long> { 1 }, transfer));
            Assert.False(handler.ClientCreated);
        }

        private static ServiceBusReceivedMessage Message(long sequence) => ServiceBusModelFactory.ServiceBusReceivedMessage(
            body: new BinaryData(new byte[] { 0, 255 }), messageId: "message-" + sequence, sequenceNumber: sequence);

        private class Reader : ServiceBusReceiver
        {
            public int Calls;
            public List<int> Sizes = new List<int>();
            public List<long?> Starts = new List<long?>();
            private IReadOnlyList<ServiceBusReceivedMessage> Next(int size)
            {
                Sizes.Add(size);
                Calls++;
                return Calls == 1 ? new[] { Message(11), Message(12) } : Calls == 2 ? new[] { Message(13) } : Array.Empty<ServiceBusReceivedMessage>();
            }
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default) => Task.FromResult(Next(maxMessages));
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> PeekMessagesAsync(int maxMessages, long? fromSequenceNumber = null, CancellationToken cancellationToken = default)
            { Starts.Add(fromSequenceNumber); return Task.FromResult(Next(maxMessages)); }
        }

        private class Handler : DeadLetterMessageHandler
        {
            private readonly ServiceBusClient client;
            public bool ClientCreated;
            public Handler(ServiceBusClient client, List<string> logs, bool partitioned = false)
                : base((message, _) => logs.Add(message), null, 1, new QueueInfo("source") { EnablePartitioning = partitioned, LockDuration = TimeSpan.FromMinutes(1) }) { this.client = client; }
            protected override ServiceBusClient CreateClient(bool crossEntityTransactions) { ClientCreated = true; return client; }
        }
        private class FailingReceiver : ServiceBusReceiver
        {
            private int calls;
            public bool Abandoned, Disposed;
            public override Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
            {
                calls++;
                if (calls == 3) throw new InvalidOperationException("receive failed");
                return Task.FromResult(Message(calls == 1 ? 99 : 1));
            }
            public override Task CompleteMessageAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken = default) => Task.CompletedTask;
            public override Task AbandonMessageAsync(ServiceBusReceivedMessage message, IDictionary<string, object> propertiesToModify = null, CancellationToken cancellationToken = default)
            { Abandoned = true; throw new InvalidOperationException("abandon failed"); }
            public override ValueTask DisposeAsync() { Disposed = true; throw new InvalidOperationException("dispose failed"); }
        }
        private class FakeSender : ServiceBusSender
        {
            public bool Disposed;
            public override Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken = default) => Task.CompletedTask;
            public override ValueTask DisposeAsync() { Disposed = true; return default; }
        }
        private class FakeClient : ServiceBusClient
        {
            private readonly ServiceBusReceiver receiver;
            public FakeSender Sender = new FakeSender();
            public bool Disposed;
            public FakeClient(ServiceBusReceiver receiver) { this.receiver = receiver; }
            public override ServiceBusReceiver CreateReceiver(string queueName, ServiceBusReceiverOptions options = null) => receiver;
            public override ServiceBusSender CreateSender(string queueOrTopicName, ServiceBusSenderOptions options = null) => Sender;
            public override ValueTask DisposeAsync() { Disposed = true; return default; }
        }
    }
}
