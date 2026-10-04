using System.Collections.Generic;

namespace ServiceBusExplorer.Core.Models
{
    /// <summary>Type-tag used throughout the entity tree and detail panels.</summary>
    public enum EntityKind
    {
        Queue,
        Topic,
        Subscription,
        EventHub,
        NotificationHub,
        Relay
    }

    /// <summary>Flat description of a Service Bus entity returned by the namespace service.</summary>
    public sealed class EntityInfo
    {
        public EntityKind Kind        { get; set; }
        public string     Name        { get; set; } = string.Empty;
        /// <summary>For subscriptions: the parent topic path.</summary>
        public string?    TopicPath   { get; set; }
        public long       ActiveMessageCount      { get; set; }
        public long       DeadLetterMessageCount  { get; set; }
        public long       ScheduledMessageCount   { get; set; }
        public long       TransferMessageCount    { get; set; }
        public long       TransferDeadLetterMessageCount { get; set; }
        public long       TotalMessageCount       { get; set; }
        public bool       IsCompatibilityMode     { get; set; }

        public string DisplayPath =>
            TopicPath != null ? $"{TopicPath}/Subscriptions/{Name}" : Name;
    }

    /// <summary>A node in the entity tree model.</summary>
    public sealed class EntityTreeNode
    {
        public string                   Label    { get; set; } = string.Empty;
        public EntityKind?              Kind     { get; set; }
        public EntityInfo?              Entity   { get; set; }
        public List<EntityTreeNode>     Children { get; set; } = new List<EntityTreeNode>();
        public bool                     IsGroup  => Entity == null;
    }
}

