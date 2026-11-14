namespace ServiceBusExplorer.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using ServiceBusExplorer.Helpers;
    using EventData = ServiceBusExplorer.Helpers.EventDataEnvelope;

    public class EventDataMessage : IDisposable
    {
        readonly EventData eventData;
        Stream stream;

        public EventDataMessage(EventData eventData)
        {
            this.eventData = eventData;
            stream = eventData.GetBodyStream();
            Properties = eventData.Properties;
            PartitionKey = eventData.PartitionKey;
            SequenceNumber = eventData.SequenceNumber;
            Offset = eventData.Offset;
            SerializedSizeInBytes = eventData.SerializedSizeInBytes;
            EnqueuedTimeUtc = eventData.EnqueuedTimeUtc;
            SystemProperties = eventData.SystemProperties;
        }

        public EventDataMessage(EventData eventData,
                                string partitionKey,
                                long sequenceNumber,
                                string offset,
                                long serializedSizeInBytes,
                                DateTime enqueuedTimeUtc)
            : this(eventData)
        {
            PartitionKey = partitionKey;
            SequenceNumber = sequenceNumber;
            Offset = offset;
            SerializedSizeInBytes = serializedSizeInBytes;
            EnqueuedTimeUtc = enqueuedTimeUtc;
            var systemProperties = new Dictionary<string, object>(eventData.SystemProperties)
            {
                ["PartitionKey"] = partitionKey,
                ["SequenceNumber"] = sequenceNumber,
                ["Offset"] = offset,
                ["EnqueuedTimeUtc"] = enqueuedTimeUtc
            };
            SystemProperties = systemProperties;
        }

        public string PartitionKey { get; private set; }
        public long SequenceNumber { get; private set; }
        public long SerializedSizeInBytes { get; private set; }
        public string Offset { get; private set; }
        public DateTime EnqueuedTimeUtc { get; private set; }
        public IDictionary<string, object> Properties { get; private set; }
        public IDictionary<string, object> SystemProperties { get; private set; }

        public Stream GetBodyStream()
        {
            var memoryStream = new MemoryStream();

            stream.CopyTo(memoryStream);
            stream.Seek(0L, SeekOrigin.Begin);

            return memoryStream;
        }

        public void Dispose()
        {
            eventData.Dispose();
        }
    }
}
