namespace ServiceBusExplorer.Helpers
{
    using Microsoft.ServiceBus.Messaging;

    public static class TopicDescriptionExtensions
    {
        public static int MaxSizeInGigabytes(this TopicDescription topicDescription)
        {
            return (int) (topicDescription.MaxSizeInMegabytes / 1024);
        }
    }
}
