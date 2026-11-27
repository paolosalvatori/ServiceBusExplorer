using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
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

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CleanupFailuresAreLoggedWithoutChangingTheSendOutcome(bool failSend)
        {
            var client = new Client { DisposeFailure = new IOException("client close") };
            client.Sender.DisposeFailure = new IOException("sender close");
            var log = new List<string>();
            var helper = new Helper(client, log.Add);
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(ServiceBusAmqpBodyTests.Message("Data"));
            if (failSend) client.Sender.Failure = new InvalidOperationException("send failed");

            var send = helper.ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None);
            if (failSend)
                Assert.Same(client.Sender.Failure, await Assert.ThrowsAsync<InvalidOperationException>(() => send));
            else
                Assert.Equal(1, await send);

            Assert.Equal(1, client.Sender.Calls);
            Assert.Equal(new[] { "sender", "client" }, client.Disposals);
            Assert.Collection(log,
                entry => Assert.Contains("sender close", entry),
                entry => Assert.Contains("client close", entry));
        }

        [Fact]
        public async Task SenderCreationFailureSurvivesClientCleanup()
        {
            var client = new Client
            {
                CreateFailure = new InvalidOperationException("sender creation"),
                DisposeFailure = new IOException("client close")
            };
            var log = new List<string>();
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(ServiceBusAmqpBodyTests.Message("Data"));

            var failure = await Assert.ThrowsAsync<InvalidOperationException>(() => new Helper(client, log.Add)
                .ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None));
            Assert.Same(client.CreateFailure, failure);
            Assert.Equal(0, client.Sender.Calls);
            Assert.Equal(new[] { "client" }, client.Disposals);
            Assert.Contains("client close", Assert.Single(log));
        }

        [Fact]
        public async Task ThrowingCleanupLoggerStillReleasesClientAfterSenderFailure()
        {
            var client = new Client();
            client.Sender.DisposeFailure = new IOException("sender close");
            var helper = new Helper(client, _ => throw new ObjectDisposedException("log"));
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(ServiceBusAmqpBodyTests.Message("Data"));

            Assert.Equal(1, await helper.ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None));
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

        [Theory]
        [InlineData("binary", true)]
        [InlineData("binary", false)]
        [InlineData("utf-8", true)]
        [InlineData("utf-16", true)]
        [InlineData("utf-16BE", false)]
        public async Task TrackedEnvelopeReachesPeerWithOriginalActionBodyAndHeaders(string encoding, bool includeContentType)
        {
            var client = new Client();
            var helper = new Helper(client) { NamespaceUri = new Uri("sb://example.servicebus.windows.net/") };
            var source = new Uri(helper.NamespaceUri, "source");
            var peer = new Uri(helper.NamespaceUri, "target");
            const string action = "urn:orders:submit";
            const string payload = "Ordre № 42 – 東京";
            MessageEncodingBindingElement binding = encoding == "binary"
                ? (MessageEncodingBindingElement)new BinaryMessageEncodingBindingElement()
                : new TextMessageEncodingBindingElement(MessageVersion.Default, Encoding.GetEncoding(encoding));
            var encoder = binding.CreateMessageEncoderFactory().Encoder;
            using var input = Message.CreateMessage(MessageVersion.Default, action, payload);
            input.Headers.To = source;
            input.Headers.Add(MessageHeader.CreateHeader("Trace", "urn:tracking", "trace-42"));
            using var buffer = new MemoryStream();
            encoder.WriteMessage(input, buffer);
            var contentType = includeContentType ? encoder.ContentType : null;
            var native = ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData(buffer.ToArray()),
                messageId: "id", sessionId: "session", contentType: contentType, to: "broker-route");
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(native);
            row.Properties["DeadLetterReason"] = "old";
            row.Properties["customer"] = "42";

            Assert.Equal(1, await helper.ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None));

            var sent = Assert.Single(client.Sender.Messages);
            Assert.Equal("target", client.Path);
            Assert.Equal(contentType, sent.ContentType);
            Assert.Equal("broker-route", sent.To);
            Assert.Equal("session", sent.SessionId);
            if (encoding != "binary")
                Assert.Contains(peer.AbsoluteUri, Encoding.GetEncoding(encoding).GetString(sent.Body.ToArray()));
            Assert.Equal("42", sent.ApplicationProperties["customer"]);
            Assert.False(sent.ApplicationProperties.ContainsKey("DeadLetterReason"));
            using var output = sent.Body.ToStream();
            using var envelope = encoder.ReadMessage(output, 65536, encoder.ContentType);
            Assert.Equal(peer, envelope.Headers.To);
            Assert.True(new EndpointAddressMessageFilter(new EndpointAddress(peer)).Match(envelope));
            Assert.False(new EndpointAddressMessageFilter(new EndpointAddress(source)).Match(envelope));
            Assert.Equal(action, envelope.Headers.Action);
            Assert.Equal("trace-42", envelope.Headers.GetHeader<string>("Trace", "urn:tracking"));
            Assert.Equal(payload, envelope.GetBody<string>());
            var original = buffer.ToArray();
            Assert.Equal(original, native.Body.ToArray());
            Assert.Equal(original, ServiceBusMessageAdapter.ToReplayMessage(row).Body.ToArray());
            Assert.Equal("old", row.Properties["DeadLetterReason"]);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task EnvelopeWithoutAPeerChangeKeepsItsExactBytes(bool addressed)
        {
            var namespaceUri = new Uri("sb://example.test/farm/");
            var to = addressed ? $"<a:To>{new Uri(namespaceUri, "target")}</a:To>" : "";
            var original = Encoding.UTF8.GetBytes(
                "<s:Envelope xmlns:s='http://www.w3.org/2003/05/soap-envelope' xmlns:a='http://www.w3.org/2005/08/addressing'>\n" +
                $"  <s:Header><a:Action>urn:order</a:Action>{to}</s:Header>\n  <s:Body><Order/></s:Body>\n</s:Envelope>");
            var native = ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData(original), messageId: "id");
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(native);
            var client = new Client();
            var helper = new Helper(client) { NamespaceUri = namespaceUri };
            await helper.ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None);

            Assert.Equal(original, Assert.Single(client.Sender.Messages).Body.ToArray());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("application/soap+msbin1")]
        public async Task UnrelatedBinaryRetainsItsOriginalDataSections(string contentType)
        {
            var native = ServiceBusAmqpBodyTests.Message("Data");
            native.GetRawAmqpMessage().Properties.ContentType = contentType;
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(native);
            var client = new Client();
            await new Helper(client).ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None);

            var sent = Assert.Single(client.Sender.Messages);
            ServiceBusAmqpBodyTests.AssertBody(native.GetRawAmqpMessage().Body, sent.GetRawAmqpMessage().Body);
            Assert.Equal(contentType, sent.ContentType);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NonSoapXmlWithAToElementIsReplayedVerbatim(bool binary)
        {
            using var buffer = new MemoryStream();
            using (var writer = binary ? XmlDictionaryWriter.CreateBinaryWriter(buffer)
                : XmlDictionaryWriter.CreateTextWriter(buffer, Encoding.UTF8, ownsStream: false))
            {
                writer.WriteStartElement("Order");
                writer.WriteElementString("To", "http://www.w3.org/2005/08/addressing", "urn:customer");
                writer.WriteEndElement();
            }
            var original = buffer.ToArray();
            var native = ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData(original),
                messageId: "order", contentType: binary ? "application/soap+msbin1" : "application/soap+xml");
            using var row = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(native);
            var client = new Client();
            await new Helper(client).ResubmitMessagesAsync("target", new[] { row }, false, CancellationToken.None);

            Assert.Equal(original, Assert.Single(client.Sender.Messages).Body.ToArray());
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
            public Helper(Client client, Action<string> log = null) : base((entry, _) => log?.Invoke(entry)) =>
                this.client = client;
            public override ServiceBusClient CreateServiceBusClient(bool enableCrossEntityTransactions = false)
            { Clients++; return client ?? throw new InvalidOperationException("Unexpected client creation."); }
        }
        private sealed class Client : ServiceBusClient
        {
            public readonly List<string> Disposals = new List<string>();
            public readonly Sender Sender;
            public string Path;
            public Exception CreateFailure;
            public Exception DisposeFailure;
            public Client() => Sender = new Sender(Disposals);
            public override ServiceBusSender CreateSender(string queueOrTopicName, ServiceBusSenderOptions options = default)
            {
                Path = queueOrTopicName;
                if (CreateFailure != null) throw CreateFailure;
                return Sender;
            }
            public override ValueTask DisposeAsync()
            {
                Disposals.Add("client");
                return DisposeFailure == null ? default : new ValueTask(Task.FromException(DisposeFailure));
            }
        }
        private sealed class Sender : ServiceBusSender
        {
            private readonly List<string> disposals;
            public List<ServiceBusMessage> Messages;
            public Exception Failure;
            public Exception DisposeFailure;
            public int Calls;
            public Sender(List<string> disposals) => this.disposals = disposals;
            public override Task SendMessagesAsync(IEnumerable<ServiceBusMessage> messages, CancellationToken cancellationToken = default)
            {
                Calls++;
                Messages = messages.ToList();
                return Failure == null ? Task.CompletedTask : Task.FromException(Failure);
            }
            public override ValueTask DisposeAsync()
            {
                disposals.Add("sender");
                if (DisposeFailure != null) throw DisposeFailure;
                return default;
            }
        }
    }
}
