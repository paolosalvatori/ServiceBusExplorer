using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.Amqp;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ReceivedMessageReplayTests
    {
        // Feed encoded AMQP to the pinned legacy receiver converter, including its native value cache.
        // Constructing a BrokeredMessage from a CLR string exercises a different (DataContract) representation.
        internal static BrokeredMessage Receive(AmqpAnnotatedMessage raw) => Receive(raw.ToBytes().ToArray());

        private static BrokeredMessage Receive(byte[] bytes)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var assembly = typeof(BrokeredMessage).Assembly;
            var bufferType = assembly.GetType("Microsoft.ServiceBus.Messaging.Amqp.ByteBuffer", true);
            var wireType = assembly.GetType("Microsoft.ServiceBus.Messaging.Amqp.AmqpMessage", true);
            var buffer = Activator.CreateInstance(bufferType, flags, null, new object[] { bytes, 0, bytes.Length }, null);
            var wire = wireType.GetMethod("CreateBufferMessage", flags, null, new[] { bufferType }, null).Invoke(null, new[] { buffer });
            return (BrokeredMessage)assembly.GetType("Microsoft.ServiceBus.Messaging.Amqp.AmqpMessageConverter", true)
                .GetMethod("ClientGetMessage", flags, null, new[] { wireType }, null).Invoke(null, new[] { wire });
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public void NativeTextAndBinaryValueRetainTheirTypeThroughTheWire(bool binary, bool edited)
        {
            const string original = "original å";
            const string correction = "corrected ö";
            object value = binary ? (object)Encoding.UTF8.GetBytes(original) : original;
            using var source = Receive(new AmqpAnnotatedMessage(AmqpMessageBody.FromValue(value)));
            Assert.True(ServiceBusMessageAdapter.TryGetReplayText(source, out var display));
            Assert.Equal(original, display);
            using var prepared = ServiceBusMessageAdapter.PrepareForReplay(source, true, edited ? correction : null, BodyType.String, false, null);
            var replay = ServiceBusMessageAdapter.ToReplayMessage(prepared).GetRawAmqpMessage();
            var wire = AmqpAnnotatedMessage.FromBytes(replay.ToBytes());
            Assert.True(wire.Body.TryGetValue(out var sent));
            if (binary) Assert.Equal(Encoding.UTF8.GetBytes(edited ? correction : original), Assert.IsType<byte[]>(sent));
            else Assert.Equal(edited ? correction : original, sent);
            Assert.True(ServiceBusMessageAdapter.TryGetReplayText(source, out display));
            Assert.Equal(original, display);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void BatchPreparationFiltersRecoveryMetadataAndHonorsTheNewIdOption(bool newId)
        {
            var wire = new AmqpAnnotatedMessage(AmqpMessageBody.FromValue(new byte[] { 1, 2 }));
            wire.Properties.MessageId = new AmqpMessageId("original");
            foreach (var name in Constants.AlwaysOmittedProperties) wire.ApplicationProperties[name.ToUpperInvariant()] = "old";
            wire.ApplicationProperties["retain"] = 42;
            using var source = Receive(wire);
            using var outbound = ServiceBusMessageAdapter.PrepareForReplay(source, false, null, BodyType.ByteArray, newId, null);
            var sent = ServiceBusMessageAdapter.ToReplayMessage(outbound);
            Assert.Single(sent.ApplicationProperties);
            Assert.Equal(42, sent.ApplicationProperties["retain"]);
            Assert.Equal(!newId, sent.MessageId == source.MessageId);
            Assert.Equal(4, source.Properties.Count);
        }

        [Fact]
        public void AValueThatTheNewCodecReencodesDifferentlyCanBeViewedButCannotSilentlyChange()
        {
            // A valid str32 encoding of one character. The modern encoder would choose str8.
            using var source = Receive(new byte[] { 0, 0x53, 0x77, 0xb1, 0, 0, 0, 1, 0x61 });
            Assert.True(ServiceBusMessageAdapter.TryGetReplayText(source, out var display));
            Assert.Equal("a", display);
            Assert.Throws<NotSupportedException>(() => ServiceBusMessageAdapter.ToReplayMessage(source));
        }

        [Fact]
        public void DataStartingWithAValueDescriptorRemainsByteForByteData()
        {
            var payload = new byte[] { 0, 0x53, 0x77, 0xa1, 1, 0x41 };
            using var source = Receive(new AmqpAnnotatedMessage(AmqpMessageBody.FromData(new[] { (ReadOnlyMemory<byte>)payload })));
            var replay = ServiceBusMessageAdapter.ToReplayMessage(source);
            Assert.Equal(AmqpMessageBodyType.Data, replay.GetRawAmqpMessage().Body.BodyType);
            Assert.Equal(payload, replay.Body.ToArray());
        }

        [Fact]
        public async Task MoveRejectsTheEntireSelectionBeforeAcquiringAnyLockOrSending()
        {
            var handler = new DeadLetterMessageHandler((_, __) => { }, null, 1, new QueueInfo("orders"));
            using var good = Receive(new AmqpAnnotatedMessage(AmqpMessageBody.FromValue("ok")));
            using var unavailable = Receive(new AmqpAnnotatedMessage(AmqpMessageBody.FromSequence(
                new[] { (IList<object>)new List<object> { 1 } })));
            var numbers = new List<long> { 1, 2 };
            int sends = 0;
            Task Send(Azure.Messaging.ServiceBus.ServiceBusMessage _) { ++sends; return Task.CompletedTask; }
            await Assert.ThrowsAsync<NotSupportedException>(() => handler.MovePreparedMessages(numbers, false, new[] { good, unavailable }, Send));
            await Assert.ThrowsAsync<ArgumentException>(() => handler.MovePreparedMessages(numbers, false, new[] { good }, Send));
            await Assert.ThrowsAsync<ArgumentException>(() => handler.MovePreparedMessages(
                new List<long> { 1, 1 }, false, new[] { good, good }, Send));
            await Assert.ThrowsAsync<NotSupportedException>(() => handler.MoveMessages(
                "target", numbers, false, new List<BrokeredMessage> { good, unavailable }));
            Assert.Equal(0, sends);
        }

        [Fact]
        public void UnavailableSequenceAndUnsupportedTextEditStopBeforeDelivery()
        {
            var sequence = AmqpMessageBody.FromSequence(new[] { (IList<object>)new List<object> { "a", 2 } });
            using var lost = Receive(new AmqpAnnotatedMessage(sequence));
            Assert.Throws<NotSupportedException>(() => ServiceBusMessageAdapter.ToReplayMessage(lost));
            Assert.Throws<NotSupportedException>(() => ServiceBusMessageAdapter.PrepareForReplay(lost, false, "edit", BodyType.Stream, false, null));

            using var scalar = Receive(new AmqpAnnotatedMessage(AmqpMessageBody.FromValue(12)));
            Assert.Throws<NotSupportedException>(() => ServiceBusMessageAdapter.PrepareForReplay(scalar, false, "12", BodyType.Stream, false, null));
            Assert.True(ServiceBusMessageAdapter.ToReplayMessage(scalar).GetRawAmqpMessage().Body.TryGetValue(out var value));
            Assert.Equal(12, value);
        }
    }
}
