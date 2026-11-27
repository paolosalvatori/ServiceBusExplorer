using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Amqp;
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusAmqpBodyTests
    {
        internal static ServiceBusReceivedMessage Message(string kind)
        {
            var body = kind == "Data" ? AmqpMessageBody.FromData(new[] { new ReadOnlyMemory<byte>(new byte[] { 0, 255 }), new ReadOnlyMemory<byte>(new byte[] { 42 }) })
                : kind == "String" ? AmqpMessageBody.FromValue("Unicode ü string")
                : kind == "Value" ? AmqpMessageBody.FromValue(new Dictionary<string, object> { ["number"] = 42, ["bytes"] = new byte[] { 0, 255 } })
                : AmqpMessageBody.FromSequence(new[] { (IList<object>)new List<object> { "text", 42, new byte[] { 0, 255 } }, new List<object> { true } });
            var raw = new AmqpAnnotatedMessage(body);
            raw.Properties.MessageId = new AmqpMessageId(kind);
            raw.Header.DeliveryCount = 0;
            return ServiceBusReceivedMessage.FromAmqpMessage(raw, new BinaryData(Guid.Empty.ToByteArray()));
        }

        [Theory]
        [InlineData("Data")]
        [InlineData("Value")]
        [InlineData("Sequence")]
        public void ReceivedBodySurvivesAdaptCloneAndResend(string kind)
        {
            var source = Message(kind);
            using var adapted = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(source);
            using var clone = ServiceBusReceivedMessageAdapter.CloneForResubmission(adapted);
            clone.MessageId = "new-id";
            clone.Properties["user"] = "edited";
            var outbound = ServiceBusMessageAdapter.ToServiceBusMessage(clone);
            AssertBody(source.GetRawAmqpMessage().Body, outbound.GetRawAmqpMessage().Body);
            Assert.Equal("new-id", outbound.MessageId);
            Assert.Equal(kind, source.MessageId);
            Assert.Equal("edited", outbound.ApplicationProperties["user"]);
            AssertBody(source.GetRawAmqpMessage().Body,
                ServiceBusMessageAdapter.ToServiceBusMessageFromReceivedMessage(source).GetRawAmqpMessage().Body);
            if (outbound.GetRawAmqpMessage().Body.TryGetValue(out var value))
            {
                ((System.Collections.IDictionary)value)["number"] = -1;
                source.GetRawAmqpMessage().Body.TryGetValue(out var original);
                Assert.Equal(42, ((IDictionary<string, object>)original)["number"]);
            }
        }

        internal static void AssertBody(AmqpMessageBody expected, AmqpMessageBody actual)
        {
            Assert.Equal(expected.BodyType, actual.BodyType);
            // AMQP serialization compares types, bytes, sequence sections and nested values.
            Assert.Equal(new AmqpAnnotatedMessage(expected).ToBytes().ToArray(),
                new AmqpAnnotatedMessage(actual).ToBytes().ToArray());
        }

        [Fact]
        public void MixedReceiveAndDeleteBatchRetainsEveryBody()
        {
            var receiver = new MixedReceiver();
            var messages = ServiceBusHelper.ReadMessagesFromModernReceiver(receiver, false, 3, null, TimeSpan.Zero, null);
            try
            {
                Assert.Equal(3, messages.Count);
                for (int i = 0; i < messages.Count; i++)
                    AssertBody(receiver.Messages[i].GetRawAmqpMessage().Body,
                        ServiceBusMessageAdapter.ToServiceBusMessage(messages[i]).GetRawAmqpMessage().Body);
            }
            finally { messages.ForEach(m => m.Dispose()); }
        }

        [Theory]
        [InlineData("Value", false, false)]
        [InlineData("Value", false, true)]
        [InlineData("Value", true, false)]
        [InlineData("Value", true, true)]
        [InlineData("Sequence", false, false)]
        [InlineData("Sequence", false, true)]
        [InlineData("Sequence", true, false)]
        [InlineData("Sequence", true, true)]
        public void InspectorCloneBoundariesPreserveUnchangedBodiesAndKeepEdits(string kind, bool send, bool edit)
        {
            var receiver = new MixedReceiver { Messages = new[] { Message(kind) } };
            var inspector = new CloningInspector(edit);
            Microsoft.ServiceBus.Messaging.BrokeredMessage outbound;
            if (send)
            {
                using var source = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(receiver.Messages[0]);
                outbound = new ServiceBusHelper((_, __) => { }).CreateMessageForApiReceiver(source, 0, false, false, BodyType.Stream, inspector);
            }
            else
            {
                outbound = Assert.Single(ServiceBusHelper.ReadMessagesFromModernReceiver(receiver, false, 1, null, TimeSpan.Zero, inspector));
            }
            using (outbound)
            {
                var converted = ServiceBusMessageAdapter.ToServiceBusMessage(outbound);
                if (edit)
                {
                    Assert.Equal(AmqpMessageBodyType.Data, converted.GetRawAmqpMessage().Body.BodyType);
                    Assert.Equal("intentional edit", converted.Body.ToString());
                }
                else AssertBody(receiver.Messages[0].GetRawAmqpMessage().Body, converted.GetRawAmqpMessage().Body);
            }
        }

        [Theory]
        [InlineData("Value")]
        [InlineData("Sequence")]
        public void InPlaceInspectorEditsDoNotChangeThePreservedPreviewBaseline(string kind)
        {
            var receiver = new MixedReceiver { Messages = new[] { Message(kind) } };
            var inspector = new InPlaceInspector();
            using var received = Assert.Single(ServiceBusHelper.ReadMessagesFromModernReceiver(receiver, false, 1, null, TimeSpan.Zero, inspector));
            using var outgoing = new ServiceBusHelper((_, __) => { }).CreateMessageForApiReceiver(received, 0, false, false, BodyType.Stream, null);
            var modern = ServiceBusMessageAdapter.ToServiceBusMessage(outgoing);
            Assert.Equal(AmqpMessageBodyType.Data, modern.GetRawAmqpMessage().Body.BodyType);
            Assert.Equal((byte)'!', modern.Body.ToArray()[0]);
            Assert.Same(receiver.Messages[0], ServiceBusReceivedMessageAdapter.GetReceivedMetadata(received));
        }

        private sealed class InPlaceInspector : IBrokeredMessageInspector
        {
            public Microsoft.ServiceBus.Messaging.BrokeredMessage BeforeSendMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message) => message;
            public Microsoft.ServiceBus.Messaging.BrokeredMessage AfterReceiveMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message)
            {
                var stream = message.GetBody<Stream>();
                stream.Position = 0;
                stream.WriteByte((byte)'!');
                stream.Position = 0;
                return message;
            }
        }

        private sealed class CloningInspector : IBrokeredMessageInspector
        {
            private readonly bool edit;
            public CloningInspector(bool edit) { this.edit = edit; }
            public Microsoft.ServiceBus.Messaging.BrokeredMessage BeforeSendMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message) => Clone(message);
            public Microsoft.ServiceBus.Messaging.BrokeredMessage AfterReceiveMessage(Microsoft.ServiceBus.Messaging.BrokeredMessage message) => Clone(message);
            private Microsoft.ServiceBus.Messaging.BrokeredMessage Clone(Microsoft.ServiceBus.Messaging.BrokeredMessage message)
            {
                var clone = message.Clone();
                if (edit) clone.SetBodyStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("intentional edit")));
                return clone;
            }
        }

        private sealed class MixedReceiver : ServiceBusReceiver
        {
            public ServiceBusReceivedMessage[] Messages = { Message("Data"), Message("Value"), Message("Sequence") };
            public override ServiceBusReceiveMode ReceiveMode => ServiceBusReceiveMode.ReceiveAndDelete;
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default) =>
                Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(Messages);
        }
    }
}
