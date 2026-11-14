using TopicDescription = ServiceBusExplorer.Helpers.TopicInfo;
namespace ServiceBusExplorer.Helpers
{
    public static class TopicDescriptionExtensions
    {
        public static int MaxSizeInGigabytes(this TopicDescription topicDescription)
        {
            return (int) (topicDescription.MaxSizeInMegabytes / 1024);
        }
    }
}
