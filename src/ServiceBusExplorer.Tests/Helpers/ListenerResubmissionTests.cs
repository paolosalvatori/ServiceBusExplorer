using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Xunit;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ListenerResubmissionTests
    {
        [Fact]
        public async Task ResubmissionPreservesRowsAndDisposesSenderBeforeClient()
        {
            var client = new Client();
            using var row = new BrokeredMessage(new MemoryStream(new byte[] { 0, 1, 255 }))
            { MessageId = "id", SessionId = "session", CorrelationId = "correlation" };
            row.Properties["customer"] = "42";
            var sent = await new Helper(client).ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None);
            Assert.Equal(1, sent);
            Assert.Equal("target", client.Path);
            var message = Assert.Single(client.Sender.Messages);
            Assert.Equal("id", message.MessageId);
            Assert.Equal("session", message.SessionId);
            Assert.Equal("correlation", message.CorrelationId);
            Assert.Equal("42", message.ApplicationProperties["customer"]);
            Assert.Equal(new byte[] { 0, 1, 255 }, message.Body.ToArray());
            using var copy = row.Clone();
            using var body = copy.GetBody<Stream>();
            Assert.Equal(0, body.ReadByte());
            Assert.Equal(new[] { "sender", "client" }, client.Disposals);
        }

        [Fact]
        public async Task SendFailurePropagatesWithoutRetryOrSuccessAndDisposesResources()
        {
            var client = new Client();
            client.Sender.Failure = new InvalidOperationException("send failed");
            using var row = new BrokeredMessage(new MemoryStream(new byte[] { 1 }));
            var failure = await Assert.ThrowsAsync<InvalidOperationException>(() => new Helper(client)
                .ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None));
            Assert.Same(client.Sender.Failure, failure);
            Assert.Equal(1, client.Sender.Calls);
            Assert.Equal(new[] { "sender", "client" }, client.Disposals);
        }

        [Fact]
        public async Task EmptyAndCancelledWorkNeverCreatesAClient()
        {
            var helper = new Helper(null);
            Assert.Equal(0, await helper.ResubmitMessagesAsync("target", Array.Empty<BrokeredMessage>(), false, CancellationToken.None));
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => helper.ResubmitMessagesAsync(
                "target", Array.Empty<BrokeredMessage>(), false, cancellation.Token));
            Assert.Equal(0, helper.Clients);
        }

        [Theory]
        [InlineData("String")]
        [InlineData("Value")]
        [InlineData("Sequence")]
        public async Task ListenerReplayPreservesNativeBodyAndRemovesRecoveryProperties(string kind)
        {
            var client = new Client();
            var native = ServiceBusAmqpBodyTests.Message(kind);
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(native);
            row.Properties["DeadLetterReason"] = "old";
            await new Helper(client).ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None);
            var sent = Assert.Single(client.Sender.Messages);
            ServiceBusAmqpBodyTests.AssertBody(native.GetRawAmqpMessage().Body, sent.GetRawAmqpMessage().Body);
            Assert.False(sent.ApplicationProperties.ContainsKey("DeadLetterReason"));
            Assert.Equal("old", row.Properties["DeadLetterReason"]);
        }

        [Fact]
        public async Task ListenerReplayKeepsSerializedStringBytes()
        {
            var client = new Client();
            using var row = new BrokeredMessage("legacy text");
            byte[] original;
            using (var copy = row.Clone())
            using (var stream = copy.GetBody<Stream>())
            using (var buffer = new MemoryStream()) { stream.CopyTo(buffer); original = buffer.ToArray(); }
            await new Helper(client).ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None);
            Assert.Equal(original, Assert.Single(client.Sender.Messages).Body.ToArray());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("oversized")]
        public async Task InvalidStateSelectionNeverCreatesAClient(string sessionId)
        {
            var helper = new Helper(null);
            if (sessionId == "oversized") sessionId = new string('s', 129);
            await Assert.ThrowsAsync<ArgumentException>(() => helper.GetSessionStateFromModernDataPlane(
                "entity", null, sessionId, CancellationToken.None));
            Assert.Equal(0, helper.Clients);
        }

        [Fact]
        public async Task CancelledStateSelectionNeverCreatesAClient()
        {
            var helper = new Helper(null);
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => helper.GetSessionStateFromModernDataPlane(
                "entity", null, "selected", new CancellationToken(true)));
            Assert.Equal(0, helper.Clients);
        }

        private sealed class Helper : ServiceBusHelper
        {
            private readonly Client client;
            public int Clients;
            public Helper(Client client) : base((_, __) => { }) => this.client = client;
            public override ServiceBusClient CreateServiceBusClient(bool enableCrossEntityTransactions = false)
            { Clients++; return client ?? throw new InvalidOperationException("Unexpected client creation."); }
        }
        private sealed class Client : ServiceBusClient
        {
            public readonly List<string> Disposals = new List<string>();
            public readonly Sender Sender;
            public string Path;
            public Client() => Sender = new Sender(Disposals);
            public override ServiceBusSender CreateSender(string queueOrTopicName, ServiceBusSenderOptions options = default)
            { Path = queueOrTopicName; return Sender; }
            public override ValueTask DisposeAsync() { Disposals.Add("client"); return default; }
        }
        private sealed class Sender : ServiceBusSender
        {
            private readonly List<string> disposals;
            public List<ServiceBusMessage> Messages;
            public Exception Failure;
            public int Calls;
            public Sender(List<string> disposals) => this.disposals = disposals;
            public override Task SendMessagesAsync(IEnumerable<ServiceBusMessage> messages, CancellationToken cancellationToken = default)
            {
                Calls++;
                Messages = messages.ToList();
                return Failure == null ? Task.CompletedTask : Task.FromException(Failure);
            }
            public override ValueTask DisposeAsync() { disposals.Add("sender"); return default; }
        }
    }
}
