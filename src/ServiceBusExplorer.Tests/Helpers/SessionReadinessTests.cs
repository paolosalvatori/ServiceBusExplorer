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
        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public void FailedManualProcessingEndsSessionWorkerRegardlessOfCommitSetting(bool transaction, bool commit)
        {
            using var cancellation = new CancellationTokenSource();
            var receiver = new FailedSession();
            var helper = new ServiceBusHelper((_, __) => { });
            var succeeded = ServiceBusHelper.ExecuteManualReceive(() => helper.ReceiveMessages(receiver,
                ServiceBusReceiveMode.PeekLock, false, 0, 1, null, false, true, false,
                false, false, false, false, 1, false, 0, new FailedInspector(), null, cancellation, out _),
                transaction, commit, cancellation.Token, out var committed);
            Assert.False(succeeded);
            Assert.False(committed);
            Assert.False(receiver.Completed);
            Assert.True(receiver.Abandoned);
        }

        [Fact]
        public void SuccessfulRequestedRollbackIsDistinctFromProcessingFailure()
        {
            Assert.True(ServiceBusHelper.ExecuteManualReceive(() => true, true, false, CancellationToken.None, out var committed));
            Assert.False(committed);
        }

        [Theory]
        [InlineData(false, false, SubQueue.DeadLetter)]
        [InlineData(false, true, SubQueue.DeadLetter)]
        [InlineData(true, false, SubQueue.DeadLetter)]
        [InlineData(true, true, SubQueue.DeadLetter)]
        [InlineData(false, false, SubQueue.TransferDeadLetter)]
        [InlineData(false, true, SubQueue.TransferDeadLetter)]
        public void DeadLetterBrowseUsesOrdinaryModernReceiverWithoutAcceptingSessions(bool subscription, bool peek, SubQueue subQueue)
        {
            var client = new DeadLetterClient();
            var helper = new Helper(client);
            var messages = helper.GetMessagesFromModernDataPlane("entity", subscription ? "subscription" : null,
                subQueue, peek, 1, 10, TimeSpan.Zero, null);
            try { Assert.Single(messages); }
            finally { messages.ForEach(m => m.Dispose()); }
            Assert.Equal(subscription, client.Subscription);
            Assert.Equal(subQueue, client.Options.SubQueue);
            Assert.Equal(peek ? ServiceBusReceiveMode.PeekLock : ServiceBusReceiveMode.ReceiveAndDelete, client.Options.ReceiveMode);
            Assert.True(client.Disposed);
            Assert.True(client.Receiver.Disposed);
        }

        [Fact]
        public void EmulatorTransferDeadLetterRejectsBeforeCreatingAnyClient()
        {
            var client = new DeadLetterClient();
            var helper = new Helper(client);
            var profile = ServiceBusNamespace.GetServiceBusNamespace("synthetic", "Endpoint=sb://localhost:5672;SharedAccessKeyName=name;SharedAccessKey=synthetic;UseDevelopmentEmulator=true", (_, __) => { });
            typeof(ServiceBusHelper).GetField("serviceBusNamespaceInstance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(helper, profile);
            Assert.Throws<NotSupportedException>(() => helper.GetMessagesFromModernDataPlane("queue", null,
                SubQueue.TransferDeadLetter, true, 1, null, TimeSpan.Zero, null));
            Assert.Null(client.Options);
        }

        [Theory]
        [InlineData("accept")]
        [InlineData("receive")]
        [InlineData("receiver-dispose")]
        [InlineData("client-dispose")]
        public void SessionBrowseRetainsConsumedRowsAndReportsLaterFailure(string failure)
        {
            Exception reported = null;
            var client = new PartialClient(failure);
            var messages = new Helper(client).GetMessagesFromModernSessionDataPlane("queue", null, null,
                false, int.MaxValue, null, TimeSpan.FromSeconds(1), null, error => reported = error);
            try
            {
                Assert.Equal(new long[] { 1 }, messages.Select(m => m.SequenceNumber));
                var partial = Assert.IsType<PartialMessageReadException>(reported);
                Assert.Same(messages[0], Assert.Single(partial.Messages));
                Assert.Equal("body", new System.IO.StreamReader(messages[0].GetBody<System.IO.Stream>()).ReadToEnd());
                Assert.Contains(failure, partial.ToString());
                Assert.True(client.Disposed);
            }
            finally { messages.ForEach(m => m.Dispose()); }
        }

        [Fact]
        public void SessionInspectorFailureOnSecondOfThreeRetainsEveryOriginalConsumedRow()
        {
            Exception reported = null;
            var inspector = new SecondRowInspector();
            var messages = new Helper(new BatchClient()).GetMessagesFromModernSessionDataPlane("queue", null, null,
                false, int.MaxValue, null, TimeSpan.FromSeconds(1), inspector, error => reported = error);
            try
            {
                Assert.IsType<PartialMessageReadException>(reported);
                Assert.Equal(2, inspector.Calls);
                Assert.Equal(new long[] { 1, 2, 3 }, messages.Select(message => message.SequenceNumber));
                Assert.All(messages, message =>
                {
                    AssertReceivedHeaders(message);
                    Assert.Equal("session", message.SessionId);
                    Assert.Equal("session", message.PartitionKey);
                    Assert.Equal("body", ServiceBusMessageAdapter.ToServiceBusMessage(message).Body.ToString());
                });
            }
            finally { messages.ForEach(message => message.Dispose()); }
        }
        [Theory]
        [InlineData("session", null)]
        [InlineData("session", "session")]
        public void SessionInspectionAndSuccessfulCloneRowsKeepPublicReceiverHeaders(string sessionId, string partitionKey)
        {
            var inspector = new HeaderCloneInspector(sessionId, partitionKey);
            var messages = new Helper(new BatchClient(sessionId, partitionKey)).GetMessagesFromModernSessionDataPlane("queue", null, sessionId,
                false, 3, null, TimeSpan.FromSeconds(1), inspector);
            try
            {
                Assert.Equal(new long[] { 1, 2, 3 }, inspector.Sequences);
                Assert.Equal(new long[] { 1, 2, 3 }, messages.Select(message => message.SequenceNumber));
                Assert.All(messages, message =>
                {
                    AssertReceivedHeaders(message);
                    Assert.Equal(sessionId, message.SessionId);
                    Assert.Equal(partitionKey ?? sessionId, message.PartitionKey);
                    Assert.Equal("edited", ServiceBusMessageAdapter.ToServiceBusMessage(message).Body.ToString());
                    Assert.Equal("inspected", message.Label);
                    Assert.Equal("value", message.Properties["inspector"]);
                });
            }
            finally { messages.ForEach(message => message.Dispose()); }
        }
        [Theory]
        [InlineData(null)]
        [InlineData("partition")]
        public void ReceivedRowSupportsNonSessionRouting(string partitionKey)
        {
            var original = Message(1, null, partitionKey);
            using var envelope = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(original);
            using var inspected = new HeaderCloneInspector(null, partitionKey).AfterReceiveMessage(envelope);
            using var row = ServiceBusReceivedMessageAdapter.ToReceivedRow(original, inspected);
            AssertReceivedHeaders(row);
            Assert.Null(row.SessionId);
            Assert.Equal(partitionKey, row.PartitionKey);
            Assert.Equal("edited", ServiceBusMessageAdapter.ToServiceBusMessage(row).Body.ToString());
            Assert.Equal("inspected", row.Label);
        }

        private class HeaderCloneInspector : IBrokeredMessageInspector
        {
            public List<long> Sequences = new List<long>();
            private readonly string sessionId, partitionKey;
            public HeaderCloneInspector(string sessionId, string partitionKey) { this.sessionId = sessionId; this.partitionKey = partitionKey; }
            public Microsoft.ServiceBus.Messaging.BrokeredMessage BeforeSendMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message) => message;
            public Microsoft.ServiceBus.Messaging.BrokeredMessage AfterReceiveMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message)
            {
                Sequences.Add(message.SequenceNumber);
                AssertReceivedHeaders(message);
                Assert.Equal(sessionId, message.SessionId);
                Assert.Equal(partitionKey ?? sessionId, message.PartitionKey);
                var clone = message.Clone();
                clone.SetBodyStream(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("edited")));
                clone.Label = "inspected";
                clone.Properties["inspector"] = "value";
                return clone;
            }
        }

        private class SecondRowInspector : IBrokeredMessageInspector
        {
            public int Calls;
            public Microsoft.ServiceBus.Messaging.BrokeredMessage BeforeSendMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message) => message;
            public Microsoft.ServiceBus.Messaging.BrokeredMessage AfterReceiveMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message)
            {
                Assert.Equal(Calls + 1, message.SequenceNumber);
                Assert.Equal("session", message.SessionId);
                Assert.Equal("session", message.PartitionKey);
                var stream = message.GetBody<System.IO.Stream>();
                stream.Position = 0;
                stream.WriteByte((byte)'!');
                stream.Position = 0;
                if (++Calls == 2) throw new InvalidOperationException("second row failed");
                return message;
            }
        }
        private class BatchClient : ServiceBusClient
        {
            private readonly string sessionId, partitionKey;
            public BatchClient(string sessionId = "session", string partitionKey = "session") { this.sessionId = sessionId; this.partitionKey = partitionKey; }
            public override Task<ServiceBusSessionReceiver> AcceptNextSessionAsync(string queueName, ServiceBusSessionReceiverOptions options = null, CancellationToken cancellationToken = default) => Task.FromResult<ServiceBusSessionReceiver>(new BatchSession(sessionId, partitionKey));
            public override Task<ServiceBusSessionReceiver> AcceptSessionAsync(string queueName, string sessionId, ServiceBusSessionReceiverOptions options = null, CancellationToken cancellationToken = default) => AcceptNextSessionAsync(queueName, options, cancellationToken);
            public override ValueTask DisposeAsync() => default;
        }
        private class BatchSession : ServiceBusSessionReceiver
        {
            private readonly string sessionId, partitionKey;
            public BatchSession(string sessionId, string partitionKey) { this.sessionId = sessionId; this.partitionKey = partitionKey; }
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(new[] { Message(1, sessionId, partitionKey), Message(2, sessionId, partitionKey), Message(3, sessionId, partitionKey) });
            public override ValueTask DisposeAsync() => default;
        }

        private class PartialClient : ServiceBusClient
        {
            private readonly string failure;
            private int accepts;
            public bool Disposed;
            public PartialClient(string failure) { this.failure = failure; }
            public override Task<ServiceBusSessionReceiver> AcceptNextSessionAsync(string queueName, ServiceBusSessionReceiverOptions options = null, CancellationToken cancellationToken = default)
            {
                if (++accepts == 1) return Task.FromResult<ServiceBusSessionReceiver>(new PartialSession(failure));
                if (failure == "accept") throw new InvalidOperationException(failure);
                throw new ServiceBusException("finished", ServiceBusFailureReason.ServiceTimeout);
            }
            public override ValueTask DisposeAsync()
            {
                Disposed = true;
                if (failure == "client-dispose") throw new InvalidOperationException(failure);
                return default;
            }
        }
        private class PartialSession : ServiceBusSessionReceiver
        {
            private readonly string failure;
            private bool read;
            public PartialSession(string failure) { this.failure = failure; }
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default)
            {
                if (read && failure == "receive") throw new InvalidOperationException(failure);
                var result = read ? Array.Empty<ServiceBusReceivedMessage>() : new[] { Message(1) };
                read = true;
                return Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(result);
            }
            public override ValueTask DisposeAsync()
            {
                if (failure == "receiver-dispose") throw new InvalidOperationException(failure);
                return default;
            }
        }

        private class DeadLetterClient : ServiceBusClient
        {
            public ServiceBusReceiverOptions Options;
            public bool Subscription, Disposed;
            public DeadLetterReceiver Receiver = new DeadLetterReceiver();
            public override ServiceBusReceiver CreateReceiver(string queueName, ServiceBusReceiverOptions options = null)
            { Options = options; return Receiver; }
            public override ServiceBusReceiver CreateReceiver(string topicName, string subscriptionName, ServiceBusReceiverOptions options = null)
            { Subscription = true; Options = options; return Receiver; }
            public override ValueTask DisposeAsync() { Disposed = true; return default; }
        }
        private class DeadLetterReceiver : ServiceBusReceiver
        {
            public bool Disposed;
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> PeekMessagesAsync(int maxMessages, long? fromSequenceNumber = null, CancellationToken cancellationToken = default) =>
                Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(new[] { Message(10) });
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default) =>
                Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(new[] { Message(10) });
            public override ValueTask DisposeAsync() { Disposed = true; return default; }
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
        private static void AssertReceivedHeaders(Microsoft.ServiceBus.Messaging.BrokeredMessage message)
        {
            Assert.Equal(message.SequenceNumber + 100, message.EnqueuedSequenceNumber);
            Assert.Equal(4, message.DeliveryCount);
            Assert.Equal(new Guid("11111111-1111-1111-1111-111111111111"), message.LockToken);
            Assert.Equal(new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc), message.EnqueuedTimeUtc);
            Assert.Equal(new DateTime(2020, 1, 1, 0, 5, 0, DateTimeKind.Utc), message.LockedUntilUtc);
            Assert.Equal("original-source", message.DeadLetterSource);
        }
        private static ServiceBusReceivedMessage Message(long sequence, string sessionId = "session", string partitionKey = "session") => ServiceBusModelFactory.ServiceBusReceivedMessage(
            body: new BinaryData("body"), messageId: "id-"+sequence, sequenceNumber: sequence, sessionId: sessionId, partitionKey: partitionKey,
            enqueuedSequenceNumber: sequence + 100, deliveryCount: 4,
            lockTokenGuid: new Guid("11111111-1111-1111-1111-111111111111"),
            enqueuedTime: new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero),
            lockedUntil: new DateTimeOffset(2020, 1, 1, 0, 5, 0, TimeSpan.Zero), deadLetterSource: "original-source");
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
