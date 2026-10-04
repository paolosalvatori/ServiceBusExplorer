namespace ServiceBusExplorer.Core.Models
{
    /// <summary>
    /// Well-known entity type display names shared across both the WinForms and Avalonia hosts.
    /// Keep in sync with ServiceBusExplorer.Helpers.Constants in the legacy Common project.
    /// </summary>
    public static class EntityTypeConstants
    {
        public const string Queues            = "Queues";
        public const string Topics            = "Topics";
        public const string Subscriptions     = "Subscriptions";
        public const string Relays            = "Relays";
        public const string EventHubs         = "Event Hubs";
        public const string NotificationHubs  = "Notification Hubs";

        // Message sub-types used by the dashboard
        public const string ActiveMessages              = "Active";
        public const string DeadLetterMessages          = "DeadLettered";
        public const string ScheduledMessages           = "Scheduled";
        public const string TransferMessages            = "InTransfer";
        public const string TransferDeadLetterMessages  = "Transfer DL";
    }
}

