using System.IO;

namespace ServiceBusExplorer.Helpers
{
    public static class EventDataExtensions
    {
        public static EventDataEnvelope Clone(this EventDataEnvelope eventData, Stream stream)
        {
            if (eventData == null || stream == null)
            {
                return null;
            }

            var clone = eventData.Clone();
            clone.SetBodyStream(stream);
            return clone;
        }

        public static EventDataEnvelope Clone(this EventDataEnvelope eventData, string text)
        {
            if (eventData == null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(text);
                writer.Flush();
                stream.Position = 0;
                return eventData.Clone(stream);
            }
        }

        public static Stream GetBodyStream(this EventDataEnvelope eventData)
        {
            return eventData?.GetBodyStream();
        }

        public static void SetBodyStream(this EventDataEnvelope eventData, Stream stream)
        {
            eventData?.SetBodyStream(stream);
        }
    }
}
