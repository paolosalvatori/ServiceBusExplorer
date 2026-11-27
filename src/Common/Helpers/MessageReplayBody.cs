using System;
using System.IO;
using System.Reflection;
using System.Text;
using Azure.Core.Amqp;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    // WindowsAzure.ServiceBus 7.0.1 retains an encoded section for AMQP Value, payload
    // bytes for Data, and no stream for Sequence. The public Stream getter hides this distinction.
    internal static class MessageReplayBody
    {
        private const BindingFlags Instance = BindingFlags.Instance | BindingFlags.NonPublic;
        private static readonly PropertyInfo Format = typeof(BrokeredMessage).GetProperty("MessageFormat", Instance);
        private static readonly FieldInfo ValueDecoded = typeof(BrokeredMessage).GetField("bodyObjectDecoded", Instance);
        private static readonly FieldInfo CachedValue = typeof(BrokeredMessage).GetField("bodyObject", Instance);

        private static bool IsReceivedAmqp(BrokeredMessage message)
        {
            if (Format == null || ValueDecoded == null || CachedValue == null)
                throw new NotSupportedException("The installed legacy SDK does not expose the information needed to replay this body safely.");

            switch (Format.GetValue(message).ToString())
            {
                case "Sbmp": return false;
                case "Amqp": return true;
                default: throw new NotSupportedException("This legacy message representation cannot be replayed safely.");
            }
        }

        internal static AmqpMessageBody Read(BrokeredMessage message)
        {
            bool received = IsReceivedAmqp(message);
            using var clone = message.Clone();
            var stream = clone.GetBodyStream();
            if (received && stream == null)
                throw new NotSupportedException("The legacy receiver did not retain this AMQP body. The message cannot be replayed safely.");

            var bytes = stream == null ? BinaryData.FromBytes(Array.Empty<byte>()) : BinaryData.FromStream(stream);
            if (!received || !(bool)ValueDecoded.GetValue(message))
                return AmqpMessageBody.FromData(new[] { bytes.ToMemory() });

            var body = AmqpAnnotatedMessage.FromBytes(bytes).Body;
            if (body.BodyType != AmqpMessageBodyType.Value)
                throw new NotSupportedException("The legacy message no longer contains its original AMQP Value section.");
            // The newer codec may normalize an unsupported AMQP primitive. Refuse a lossy replay.
            if (!new AmqpAnnotatedMessage(body).ToBytes().ToMemory().Span.SequenceEqual(bytes.ToMemory().Span))
                throw new NotSupportedException("The installed AMQP codec cannot reproduce this Value section exactly.");
            return body;
        }

        internal static bool TryGetText(BrokeredMessage message, out string text)
        {
            text = null;
            if (Format?.GetValue(message)?.ToString() != "Amqp" || !Equals(ValueDecoded?.GetValue(message), true))
                return false;

            // Display remains available even when the original encoding cannot be replayed.
            var value = CachedValue?.GetValue(message);
            if (value is string str) text = str;
            else if (value is byte[] bytes) text = Encoding.UTF8.GetString(bytes);
            else if (value is ArraySegment<byte> segment) text = Encoding.UTF8.GetString(segment.Array, segment.Offset, segment.Count);
            return text != null;
        }

        internal static void Edit(BrokeredMessage message, string text, BodyType originalType)
        {
            if (IsReceivedAmqp(message))
            {
                var body = Read(message);
                if (body.TryGetValue(out var value))
                {
                    object replacement;
                    if (value is string) replacement = text;
                    else if (value is byte[]) replacement = Encoding.UTF8.GetBytes(text);
                    else throw new NotSupportedException("Only text and binary AMQP Value bodies can be edited as text.");

                    var encoded = new AmqpAnnotatedMessage(AmqpMessageBody.FromValue(replacement)).ToBytes();
                    message.SetBodyStream(new MemoryStream(encoded.ToArray(), writable: false));
                    CachedValue.SetValue(message, replacement);
                    return;
                }
            }

            var stream = new MemoryStream();
            object editedValue = null;
            if (originalType == BodyType.String)
                editedValue = text;
            else if (originalType == BodyType.ByteArray)
                editedValue = Encoding.UTF8.GetBytes(text);

            if (editedValue != null)
                new CustomDataContractBinarySerializer(editedValue.GetType()).WriteObject(stream, editedValue);
            else
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                stream.Write(bytes, 0, bytes.Length);
            }
            stream.Position = 0;
            message.SetBodyStream(stream);
            if (CachedValue.GetValue(message) != null)
                CachedValue.SetValue(message, editedValue);
        }
    }
}
