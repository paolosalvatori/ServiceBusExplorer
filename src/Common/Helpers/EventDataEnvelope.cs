using System;
using System.Collections.Generic;
using System.IO;
using Azure;
using Azure.Messaging.EventHubs;

namespace ServiceBusExplorer.Helpers
{
    public sealed class EventDataEnvelope : IDisposable
    {
        private Stream body;

        public EventDataEnvelope(Stream body)
        {
            SetBodyStream(body);
            Properties = new Dictionary<string, object>();
            SystemProperties = new Dictionary<string, object>();
        }

        public string PartitionKey { get; set; }
        public long SequenceNumber { get; set; }
        public long SerializedSizeInBytes => body.CanSeek ? body.Length : 0;
        public string Offset { get; set; }
        public DateTime EnqueuedTimeUtc { get; set; }
        public IDictionary<string, object> Properties { get; }
        public IDictionary<string, object> SystemProperties { get; }

        public Stream GetBodyStream()
        {
            if (body.CanSeek)
            {
                body.Position = 0;
            }
            return body;
        }

        public void SetBodyStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var copy = new MemoryStream();
            var originalPosition = stream.CanSeek ? stream.Position : 0;
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }
            stream.CopyTo(copy);
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
            copy.Position = 0;
            body?.Dispose();
            body = copy;
        }

        public EventDataEnvelope Clone()
        {
            var clone = new EventDataEnvelope(GetBodyStream())
            {
                PartitionKey = PartitionKey,
                SequenceNumber = SequenceNumber,
                Offset = Offset,
                EnqueuedTimeUtc = EnqueuedTimeUtc
            };
            foreach (var property in Properties)
            {
                clone.Properties.Add(property);
            }
            foreach (var property in SystemProperties)
            {
                clone.SystemProperties.Add(property);
            }
            return clone;
        }

        public EventData ToAzureEventData()
        {
            using (var copy = new MemoryStream())
            {
                GetBodyStream().CopyTo(copy);
                var eventData = new EventData(new BinaryData(copy.ToArray()));
                foreach (var property in Properties)
                {
                    eventData.Properties[property.Key] = property.Value;
                }
                return eventData;
            }
        }

        public void Dispose()
        {
            body?.Dispose();
        }
    }
}
