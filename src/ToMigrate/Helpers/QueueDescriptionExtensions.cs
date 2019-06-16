namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    using ServiceBus.Messaging;

    public static class QueueDescriptionExtensions
    {
        public static int MaxSizeInGigabytes(this QueueDescription queueDescription)
        {
            return (int) (queueDescription.MaxSizeInMegabytes / 1024);
        }
    }
}
