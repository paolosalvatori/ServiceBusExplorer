namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    using ServiceBus.Messaging;

    static class TopicDescriptionExtensions
    {
        public static int MaxSizeInGigabytes(this TopicDescription topicDescription)
        {
            return (int) (topicDescription.MaxSizeInMegabytes / 1024);
        }
    }
}
