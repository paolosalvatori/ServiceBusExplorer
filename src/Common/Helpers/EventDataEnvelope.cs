using System;
using System.Collections.Generic;
using System.IO;
using Azure;
using Azure.Messaging.EventHubs;

namespace ServiceBusExplorer.Helpers
{
    public sealed class EventDataEnvelope : IDisposable
    {
        private byte[] body;

        public EventDataEnvelope(Stream body)
        {
            SetBodyStream(body);
            Properties = new Dictionary<string, object>();
            SystemProperties = new Dictionary<string, object>();
        }

        public string PartitionKey { get; set; }
        public long SequenceNumber { get; set; }
        public long SerializedSizeInBytes => body.Length;
        public string Offset { get; set; }
        public DateTime EnqueuedTimeUtc { get; set; }
        public IDictionary<string, object> Properties { get; }
        public IDictionary<string, object> SystemProperties { get; }

        public Stream GetBodyStream()
        {
            return new MemoryStream(body, false);
        }

        public void SetBodyStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var originalPosition = stream.CanSeek ? stream.Position : 0;
            try
            {
                if (stream.CanSeek)
                {
                    stream.Position = 0;
                }
                using (var copy = new MemoryStream())
                {
                    stream.CopyTo(copy);
                    body = copy.ToArray();
                }
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public EventDataEnvelope Clone()
        {
            EventDataEnvelope clone;
            using (var bodyStream = GetBodyStream())
            {
                clone = new EventDataEnvelope(bodyStream)
                {
                    PartitionKey = PartitionKey,
                    SequenceNumber = SequenceNumber,
                    Offset = Offset,
                    EnqueuedTimeUtc = EnqueuedTimeUtc
                };
            }
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
            var eventData = new EventData(new BinaryData(body));
            foreach (var property in Properties)
            {
                eventData.Properties[property.Key] = property.Value;
            }
            return eventData;
        }

        public void Dispose()
        {
        }
    }
}
