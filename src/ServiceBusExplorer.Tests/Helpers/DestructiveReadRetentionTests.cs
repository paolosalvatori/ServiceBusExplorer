using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class DestructiveReadRetentionTests
    {
        [Theory]
        [InlineData(null, SubQueue.None)]
        [InlineData(null, SubQueue.DeadLetter)]
        [InlineData(null, SubQueue.TransferDeadLetter)]
        [InlineData("subscription", SubQueue.None)]
        [InlineData("subscription", SubQueue.DeadLetter)]
        public void InspectorFailureRetainsPreviousRowsAndEveryOriginalInDestructiveBatch(string subscription, SubQueue queue)
        {
            var receiver = new Reader(new[] { 1 }, new[] { 2, 3, 4 });
            var client = new Client(receiver);
            Exception reported = null;
            var rows = ServiceBusHelper.ReadMessagesFromModernClient(client, "entity", subscription, queue,
                false, int.MaxValue, null, TimeSpan.Zero, new Inspector(3), ex => reported = ex);
            try
            {
                var partial = Assert.IsType<PartialMessageReadException>(reported);
                Assert.Equal(4, partial.Messages.Count);
                Assert.Equal(new long[] { 1, 2, 3, 4 }, rows.Select(row => row.SequenceNumber));
                Assert.Equal(new long[] { 1, 2, 3, 4 }, rows.Select(ServiceBusReceivedMessageAdapter.GetSourceSequenceNumber));
                Assert.Equal(new[] { "edited1", "body2", "body3", "body4" }, rows.Select(Text));
                Assert.Equal(2, receiver.Calls);
                Assert.True(receiver.Disposed);
                Assert.True(client.Disposed);
                Assert.Equal(queue, client.Options.SubQueue);
                Assert.Equal(ServiceBusReceiveMode.ReceiveAndDelete, client.Options.ReceiveMode);
            }
            finally { rows.ForEach(row => row.Dispose()); }
        }

        [Fact]
        public void LaterReceiveAndCleanupFailuresStillReturnConsumedRows()
        {
            var receiver = new Reader(new[] { 1, 2 }) { FailReceive = true, FailDispose = true };
            var client = new Client(receiver) { FailDispose = true };
            Exception reported = null;
            var rows = ServiceBusHelper.ReadMessagesFromModernClient(client, "entity", null, SubQueue.None,
                false, int.MaxValue, null, TimeSpan.Zero, null, ex => reported = ex);
            try
            {
                Assert.Equal(new[] { "body1", "body2" }, rows.Select(Text));
                Assert.IsType<AggregateException>(Assert.IsType<PartialMessageReadException>(reported).InnerException);
                Assert.True(receiver.Disposed);
                Assert.True(client.Disposed);
            }
            finally { rows.ForEach(row => row.Dispose()); }
        }

        [Fact]
        public void DirectReaderReturnsConsumedBatchInExceptionWhenNoReporterIsProvided()
        {
            var receiver = new Reader(new[] { 1, 2, 3 });
            var partial = Assert.Throws<PartialMessageReadException>(() => ServiceBusHelper.ReadMessagesFromModernReceiver(
                receiver, false, int.MaxValue, null, TimeSpan.Zero, new Inspector(2)));
            try { Assert.Equal(new[] { "body1", "body2", "body3" }, partial.Messages.Select(Text)); }
            finally { foreach (var row in partial.Messages) row.Dispose(); }
        }

        [Fact]
        public void PeekFailureStillThrowsWithoutPublishingPartialRows()
        {
            var receiver = new Reader(new[] { 1, 2, 3 });
            var client = new Client(receiver);
            var reports = 0;
            Assert.Throws<InvalidOperationException>(() => ServiceBusHelper.ReadMessagesFromModernClient(
                client, "entity", null, SubQueue.None, true, 3, null, TimeSpan.Zero, new Inspector(2), ex => reports++));
            Assert.Equal(0, reports);
            Assert.True(receiver.Disposed);
            Assert.True(client.Disposed);
            Assert.Equal(ServiceBusReceiveMode.PeekLock, client.Options.ReceiveMode);
        }

        private static string Text(BrokeredMessage row) => Encoding.UTF8.GetString(ServiceBusMessageAdapter.ToServiceBusMessage(row).Body.ToArray());
        private sealed class Inspector : IBrokeredMessageInspector
        {
            private readonly long failure;
            public Inspector(long failure) { this.failure = failure; }
            public BrokeredMessage BeforeSendMessage(BrokeredMessage message) => message;
            public BrokeredMessage AfterReceiveMessage(BrokeredMessage message)
            {
                var sequence = message.SequenceNumber;
                message.SetBodyStream(new MemoryStream(Encoding.UTF8.GetBytes("edited" + sequence)));
                if (sequence == failure) throw new InvalidOperationException("Synthetic inspector failure");
                return message;
            }
        }
        private sealed class Client : ServiceBusClient
        {
            private readonly Reader reader;
            public bool Disposed, FailDispose;
            public ServiceBusReceiverOptions Options;
            public Client(Reader reader) { this.reader = reader; }
            public override ServiceBusReceiver CreateReceiver(string queueName, ServiceBusReceiverOptions options = null)
            { Options = options; reader.Mode = options.ReceiveMode; return reader; }
            public override ServiceBusReceiver CreateReceiver(string topicName, string subscriptionName, ServiceBusReceiverOptions options = null)
                => CreateReceiver(topicName, options);
            public override ValueTask DisposeAsync()
            { Disposed = true; if (FailDispose) throw new InvalidOperationException("Synthetic client cleanup failure"); return default; }
        }
        private sealed class Reader : ServiceBusReceiver
        {
            private readonly Queue<int[]> batches;
            public Reader(params int[][] batches) { this.batches = new Queue<int[]>(batches); }
            public bool Disposed, FailReceive, FailDispose;
            public int Calls;
            public ServiceBusReceiveMode Mode = ServiceBusReceiveMode.ReceiveAndDelete;
            public override ServiceBusReceiveMode ReceiveMode => Mode;
            private Task<IReadOnlyList<ServiceBusReceivedMessage>> Next()
            {
                Calls++;
                if (batches.Count == 0 && FailReceive) throw new InvalidOperationException("Synthetic receive failure");
                IReadOnlyList<ServiceBusReceivedMessage> messages = batches.Count == 0 ? Array.Empty<ServiceBusReceivedMessage>() :
                    batches.Dequeue().Select(i => ServiceBusModelFactory.ServiceBusReceivedMessage(
                        body: BinaryData.FromString("body" + i), sequenceNumber: i, messageId: "id" + i)).ToArray();
                return Task.FromResult(messages);
            }
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default) => Next();
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> PeekMessagesAsync(int maxMessages, long? fromSequenceNumber = null, CancellationToken cancellationToken = default) => Next();
            public override ValueTask DisposeAsync()
            { Disposed = true; if (FailDispose) throw new InvalidOperationException("Synthetic receiver cleanup failure"); return default; }
        }
    }
}
