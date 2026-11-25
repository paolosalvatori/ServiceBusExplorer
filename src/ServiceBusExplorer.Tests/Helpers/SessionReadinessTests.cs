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
    public class SessionReadinessTests
    {
        [Fact]
        public void EntityWidePeekNeverAcceptsOrLocksASession()
        {
            var client = new Client();
            var helper = new Helper(client);
            var messages = helper.GetMessagesFromModernSessionDataPlane("queue", null, null, true, 2, 10, TimeSpan.FromSeconds(1), null);
            try { Assert.Equal(new long[] { 10, 11 }, messages.Select(m => m.SequenceNumber)); Assert.Equal(0, client.Accepts); Assert.Equal(1, client.Peeks); }
            finally { messages.ForEach(m => m.Dispose()); }
            Assert.True(client.Disposed);
        }
        [Fact]
        public void ReceiveAllDrainsMultipleSessionsUntilAcceptTimesOut()
        {
            var client = new Client();
            var helper = new Helper(client);
            var count = ServiceBusHelper.ResolveModernReadCount(false, true, 1, 1);
            var messages = helper.GetMessagesFromModernSessionDataPlane("queue", null, null, false, count, null, TimeSpan.FromSeconds(1), null);
            try { Assert.Equal(new long[] { 1, 2 }, messages.Select(m => m.SequenceNumber)); Assert.Equal(3, client.Accepts); }
            finally { messages.ForEach(m => m.Dispose()); }
            Assert.All(client.Sessions, s => Assert.True(s.Disposed));
            Assert.True(client.Disposed);
        }
        [Fact]
        public void SelectedSessionDoesNotConsumeOtherSessions()
        {
            var client = new Client();
            var messages = new Helper(client).GetMessagesFromModernSessionDataPlane("queue", null, "selected", false, int.MaxValue, null, TimeSpan.FromSeconds(1), null);
            try { Assert.Single(messages); Assert.Equal(1, client.Accepts); }
            finally { messages.ForEach(m => m.Dispose()); }
        }
        [Theory]
        [InlineData(true, true, false, true)]
        [InlineData(false, true, false, false)]
        [InlineData(true, false, false, false)]
        [InlineData(true, true, true, false)]
        public void ManualReceiveTransactionCommitsOnlySuccessfulUncancelledWork(bool success, bool commit, bool cancel, bool expected)
        {
            using var cancellation = new CancellationTokenSource();
            System.Transactions.TransactionStatus? outcome = null;
            var result = ServiceBusHelper.ExecuteReceiveTransaction(() =>
            {
                System.Transactions.Transaction.Current.TransactionCompleted += (_, args) => outcome = args.Transaction.TransactionInformation.Status;
                if (cancel) cancellation.Cancel();
                return success;
            }, commit, cancellation.Token);
            Assert.Equal(expected, result);
            Assert.Equal(expected ? System.Transactions.TransactionStatus.Committed : System.Transactions.TransactionStatus.Aborted, outcome);
        }

        [Fact]
        public void FailedManualSessionInspectorAbortsWithoutCompletingTheMessage()
        {
            using var cancellation = new CancellationTokenSource();
            var receiver = new FailedSession();
            var helper = new ServiceBusHelper((_, __) => { });
            var committed = ServiceBusHelper.ExecuteReceiveTransaction(() => helper.ReceiveMessages(receiver,
                ServiceBusReceiveMode.PeekLock, false, 0, 1, null, false, true, false,
                false, false, false, false, 1, false, 0, new FailedInspector(), null, cancellation, out _),
                true, cancellation.Token);
            Assert.False(committed);
            Assert.False(receiver.Completed);
            Assert.True(receiver.Abandoned);
        }
        private class FailedInspector : IBrokeredMessageInspector
        {
            public Microsoft.ServiceBus.Messaging.BrokeredMessage BeforeSendMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message) => message;
            public Microsoft.ServiceBus.Messaging.BrokeredMessage AfterReceiveMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message) => throw new InvalidOperationException("inspector failed");
        }
        private class FailedSession : ServiceBusSessionReceiver
        {
            public bool Completed, Abandoned;
            public override Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default) => Task.FromResult(Message(1));
            public override Task CompleteMessageAsync(ServiceBusReceivedMessage message, CancellationToken cancellationToken = default) { Completed = true; return Task.CompletedTask; }
            public override Task AbandonMessageAsync(ServiceBusReceivedMessage message, IDictionary<string, object> propertiesToModify = null, CancellationToken cancellationToken = default) { Abandoned = true; return Task.CompletedTask; }
        }

        private class Helper : ServiceBusHelper
        {
            private readonly ServiceBusClient client;
            public Helper(ServiceBusClient client) : base((_, __) => { }) { this.client = client; }
            public override ServiceBusClient CreateServiceBusClient(bool enableCrossEntityTransactions = false) => client;
        }
        private static ServiceBusReceivedMessage Message(long sequence) => ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("body"), messageId: "id-"+sequence, sequenceNumber: sequence);
        private class Session : ServiceBusSessionReceiver
        {
            private readonly long sequence;
            private bool read;
            public bool Disposed;
            public Session(long sequence) { this.sequence = sequence; }
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
            {
                Assert.InRange(maxMessages, 1, 100);
                IReadOnlyList<ServiceBusReceivedMessage> result = read ? Array.Empty<ServiceBusReceivedMessage>() : new[] { Message(sequence) };
                read = true; return Task.FromResult(result);
            }
            public override ValueTask DisposeAsync() { Disposed = true; return default; }
        }
        private class PeekReceiver : ServiceBusReceiver
        {
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> PeekMessagesAsync(int maxMessages, long? fromSequenceNumber = null, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(new[] { Message(10), Message(11) });
            public override ValueTask DisposeAsync() => default;
        }
        private class Client : ServiceBusClient
        {
            public int Accepts, Peeks;
            public bool Disposed;
            public List<Session> Sessions = new List<Session>();
            public override ServiceBusReceiver CreateReceiver(string queueName, ServiceBusReceiverOptions options = null) { Peeks++; return new PeekReceiver(); }
            public override Task<ServiceBusSessionReceiver> AcceptNextSessionAsync(string queueName, ServiceBusSessionReceiverOptions options = null, CancellationToken cancellationToken = default)
            {
                if (++Accepts > 2) throw new ServiceBusException("No sessions", ServiceBusFailureReason.ServiceTimeout);
                var session = new Session(Accepts); Sessions.Add(session); return Task.FromResult<ServiceBusSessionReceiver>(session);
            }
            public override Task<ServiceBusSessionReceiver> AcceptSessionAsync(string queueName, string sessionId, ServiceBusSessionReceiverOptions options = null, CancellationToken cancellationToken = default) => AcceptNextSessionAsync(queueName, options, cancellationToken);
            public override ValueTask DisposeAsync() { Disposed = true; return default; }
        }
    }
}
