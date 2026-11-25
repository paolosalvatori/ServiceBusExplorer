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
                : kind == "Value" ? AmqpMessageBody.FromValue(new Dictionary<string, object> { ["number"] = 42, ["bytes"] = new byte[] { 0, 255 } })
                : AmqpMessageBody.FromSequence(new[] { (IList<object>)new List<object> { "text", 42, new byte[] { 0, 255 } }, new List<object> { true } });
            var raw = new AmqpAnnotatedMessage(body);
            raw.Properties.MessageId = new AmqpMessageId(kind);
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

        private sealed class MixedReceiver : ServiceBusReceiver
        {
            public ServiceBusReceivedMessage[] Messages = { Message("Data"), Message("Value"), Message("Sequence") };
            public override ServiceBusReceiveMode ReceiveMode => ServiceBusReceiveMode.ReceiveAndDelete;
            public override Task<IReadOnlyList<ServiceBusReceivedMessage>> ReceiveMessagesAsync(int maxMessages, TimeSpan? maxWaitTime = null, CancellationToken cancellationToken = default) =>
                Task.FromResult<IReadOnlyList<ServiceBusReceivedMessage>>(Messages);
        }
    }
}
