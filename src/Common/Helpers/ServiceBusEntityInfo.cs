using System;
using System.Runtime.Serialization;
using Microsoft.ServiceBus.Messaging;
using Azure.Messaging.ServiceBus.Administration;
using EntityStatus = Microsoft.ServiceBus.Messaging.EntityStatus;

namespace ServiceBusExplorer.Helpers
{
    // The XML-compatible edit buffer is separate from the administration runtime snapshot.
    public abstract class ServiceBusEntityInfo : IExtensibleDataObject
    {
        public ExtensionDataObject ExtensionData { get; set; }
        public bool IsReadOnly => false;
    }

    public sealed class EntityMessageCounts
    {
        public long ActiveMessageCount { get; set; }
        public long DeadLetterMessageCount { get; set; }
        public long ScheduledMessageCount { get; set; }
        public long TransferMessageCount { get; set; }
        public long TransferDeadLetterMessageCount { get; set; }
    }

    public sealed class QueueInfo : ServiceBusEntityInfo
    {
        public QueueInfo(string path) : this(new QueueDescription(path)) { }
        public QueueInfo(QueueDescription description) { Description = description ?? throw new ArgumentNullException(nameof(description)); }
        public QueueInfo(QueueProperties properties, QueueRuntimeProperties runtime) : this(ServiceBusManagementMapper.From(properties))
        {
            Properties = properties;
            RuntimeProperties = runtime;
            MaxMessageSizeInKilobytes = properties.MaxMessageSizeInKilobytes;
            MessageCountDetails = new EntityMessageCounts
            {
                ActiveMessageCount = runtime.ActiveMessageCount,
                DeadLetterMessageCount = runtime.DeadLetterMessageCount,
                ScheduledMessageCount = runtime.ScheduledMessageCount,
                TransferMessageCount = runtime.TransferMessageCount,
                TransferDeadLetterMessageCount = runtime.TransferDeadLetterMessageCount
            };
        }

        public QueueDescription Description { get; }
        public QueueProperties Properties { get; }
        public QueueRuntimeProperties RuntimeProperties { get; }
        public long? MaxMessageSizeInKilobytes { get; set; }
        public EntityMessageCounts MessageCountDetails { get; } = new EntityMessageCounts();
        public long MessageCount => RuntimeProperties?.TotalMessageCount ?? 0;
        public long SizeInBytes => RuntimeProperties?.SizeInBytes ?? 0;
        public DateTime CreatedAt => RuntimeProperties?.CreatedAt.UtcDateTime ?? DateTime.MinValue;
        public DateTime UpdatedAt => RuntimeProperties?.UpdatedAt.UtcDateTime ?? DateTime.MinValue;
        public DateTime AccessedAt => RuntimeProperties?.AccessedAt.UtcDateTime ?? DateTime.MinValue;
        public string Path { get => Description.Path; set => Description.Path = value; }
        public TimeSpan LockDuration { get => Description.LockDuration; set => Description.LockDuration = value; }
        public long MaxSizeInMegabytes { get => Description.MaxSizeInMegabytes; set => Description.MaxSizeInMegabytes = value; }
        public bool RequiresDuplicateDetection { get => Description.RequiresDuplicateDetection; set => Description.RequiresDuplicateDetection = value; }
        public bool RequiresSession { get => Description.RequiresSession; set => Description.RequiresSession = value; }
        public TimeSpan DefaultMessageTimeToLive { get => Description.DefaultMessageTimeToLive; set => Description.DefaultMessageTimeToLive = value; }
        public TimeSpan AutoDeleteOnIdle { get => Description.AutoDeleteOnIdle; set => Description.AutoDeleteOnIdle = value; }
        public bool EnableDeadLetteringOnMessageExpiration { get => Description.EnableDeadLetteringOnMessageExpiration; set => Description.EnableDeadLetteringOnMessageExpiration = value; }
        public TimeSpan DuplicateDetectionHistoryTimeWindow { get => Description.DuplicateDetectionHistoryTimeWindow; set => Description.DuplicateDetectionHistoryTimeWindow = value; }
        public int MaxDeliveryCount { get => Description.MaxDeliveryCount; set => Description.MaxDeliveryCount = value; }
        public bool EnableBatchedOperations { get => Description.EnableBatchedOperations; set => Description.EnableBatchedOperations = value; }
        public Microsoft.ServiceBus.Messaging.AuthorizationRules Authorization => Description.Authorization;
        public bool IsAnonymousAccessible { get => Description.IsAnonymousAccessible; set => Description.IsAnonymousAccessible = value; }
        public bool SupportOrdering { get => Description.SupportOrdering; set => Description.SupportOrdering = value; }
        public EntityStatus Status { get => Description.Status; set => Description.Status = value; }
        public string ForwardTo { get => Description.ForwardTo; set => Description.ForwardTo = value; }
        public string ForwardDeadLetteredMessagesTo { get => Description.ForwardDeadLetteredMessagesTo; set => Description.ForwardDeadLetteredMessagesTo = value; }
        public bool EnablePartitioning { get => Description.EnablePartitioning; set => Description.EnablePartitioning = value; }
        public string UserMetadata { get => Description.UserMetadata; set => Description.UserMetadata = value; }
        public bool EnableExpress { get => Description.EnableExpress; set => Description.EnableExpress = value; }
        public static implicit operator QueueInfo(QueueDescription description) => description == null ? null : new QueueInfo(description);
        public static implicit operator QueueDescription(QueueInfo info) => info?.Description;
    }

    public sealed class TopicInfo : ServiceBusEntityInfo
    {
        public TopicInfo(string path) : this(new TopicDescription(path)) { }
        public TopicInfo(TopicDescription description) { Description = description ?? throw new ArgumentNullException(nameof(description)); }
        public TopicInfo(TopicProperties properties, TopicRuntimeProperties runtime, EntityMessageCounts counts) : this(ServiceBusManagementMapper.From(properties))
        {
            Properties = properties;
            RuntimeProperties = runtime;
            MaxMessageSizeInKilobytes = properties.MaxMessageSizeInKilobytes;
            MessageCountDetails = counts ?? throw new ArgumentNullException(nameof(counts));
        }

        public TopicDescription Description { get; }
        public TopicProperties Properties { get; }
        public TopicRuntimeProperties RuntimeProperties { get; }
        public long? MaxMessageSizeInKilobytes { get; set; }
        public EntityMessageCounts MessageCountDetails { get; } = new EntityMessageCounts();
        public long SizeInBytes => RuntimeProperties?.SizeInBytes ?? 0;
        public int SubscriptionCount => RuntimeProperties?.SubscriptionCount ?? 0;
        public DateTime CreatedAt => RuntimeProperties?.CreatedAt.UtcDateTime ?? DateTime.MinValue;
        public DateTime UpdatedAt => RuntimeProperties?.UpdatedAt.UtcDateTime ?? DateTime.MinValue;
        public DateTime AccessedAt => RuntimeProperties?.AccessedAt.UtcDateTime ?? DateTime.MinValue;
        public string Path { get => Description.Path; set => Description.Path = value; }
        public long MaxSizeInMegabytes { get => Description.MaxSizeInMegabytes; set => Description.MaxSizeInMegabytes = value; }
        public bool RequiresDuplicateDetection { get => Description.RequiresDuplicateDetection; set => Description.RequiresDuplicateDetection = value; }
        public TimeSpan DefaultMessageTimeToLive { get => Description.DefaultMessageTimeToLive; set => Description.DefaultMessageTimeToLive = value; }
        public TimeSpan AutoDeleteOnIdle { get => Description.AutoDeleteOnIdle; set => Description.AutoDeleteOnIdle = value; }
        public TimeSpan DuplicateDetectionHistoryTimeWindow { get => Description.DuplicateDetectionHistoryTimeWindow; set => Description.DuplicateDetectionHistoryTimeWindow = value; }
        public bool EnableBatchedOperations { get => Description.EnableBatchedOperations; set => Description.EnableBatchedOperations = value; }
        public Microsoft.ServiceBus.Messaging.AuthorizationRules Authorization => Description.Authorization;
        public bool IsAnonymousAccessible { get => Description.IsAnonymousAccessible; set => Description.IsAnonymousAccessible = value; }
        public bool SupportOrdering { get => Description.SupportOrdering; set => Description.SupportOrdering = value; }
        public EntityStatus Status { get => Description.Status; set => Description.Status = value; }
        public bool EnablePartitioning { get => Description.EnablePartitioning; set => Description.EnablePartitioning = value; }
        public string UserMetadata { get => Description.UserMetadata; set => Description.UserMetadata = value; }
        public bool EnableExpress { get => Description.EnableExpress; set => Description.EnableExpress = value; }
        public bool EnableFilteringMessagesBeforePublishing { get => Description.EnableFilteringMessagesBeforePublishing; set => Description.EnableFilteringMessagesBeforePublishing = value; }
        public static implicit operator TopicInfo(TopicDescription description) => description == null ? null : new TopicInfo(description);
        public static implicit operator TopicDescription(TopicInfo info) => info?.Description;
    }

    public sealed class SubscriptionInfo : ServiceBusEntityInfo
    {
        public SubscriptionInfo(string topicPath, string name) : this(new SubscriptionDescription(topicPath, name)) { }
        public SubscriptionInfo(SubscriptionDescription description) { Description = description ?? throw new ArgumentNullException(nameof(description)); }
        public SubscriptionInfo(SubscriptionProperties properties, SubscriptionRuntimeProperties runtime) : this(ServiceBusManagementMapper.From(properties))
        {
            Properties = properties;
            RuntimeProperties = runtime;
            MessageCountDetails = new EntityMessageCounts
            {
                ActiveMessageCount = runtime.ActiveMessageCount,
                DeadLetterMessageCount = runtime.DeadLetterMessageCount,
                TransferMessageCount = runtime.TransferMessageCount,
                TransferDeadLetterMessageCount = runtime.TransferDeadLetterMessageCount
            };
        }

        public SubscriptionDescription Description { get; }
        public SubscriptionProperties Properties { get; }
        public SubscriptionRuntimeProperties RuntimeProperties { get; }
        public EntityMessageCounts MessageCountDetails { get; } = new EntityMessageCounts();
        public long MessageCount => RuntimeProperties?.TotalMessageCount ?? 0;
        public DateTime CreatedAt => RuntimeProperties?.CreatedAt.UtcDateTime ?? DateTime.MinValue;
        public DateTime UpdatedAt => RuntimeProperties?.UpdatedAt.UtcDateTime ?? DateTime.MinValue;
        public DateTime AccessedAt => RuntimeProperties?.AccessedAt.UtcDateTime ?? DateTime.MinValue;
        public string TopicPath { get => Description.TopicPath; set => Description.TopicPath = value; }
        public string Name { get => Description.Name; set => Description.Name = value; }
        public TimeSpan LockDuration { get => Description.LockDuration; set => Description.LockDuration = value; }
        public bool RequiresSession { get => Description.RequiresSession; set => Description.RequiresSession = value; }
        public TimeSpan DefaultMessageTimeToLive { get => Description.DefaultMessageTimeToLive; set => Description.DefaultMessageTimeToLive = value; }
        public TimeSpan AutoDeleteOnIdle { get => Description.AutoDeleteOnIdle; set => Description.AutoDeleteOnIdle = value; }
        public bool EnableDeadLetteringOnMessageExpiration { get => Description.EnableDeadLetteringOnMessageExpiration; set => Description.EnableDeadLetteringOnMessageExpiration = value; }
        public int MaxDeliveryCount { get => Description.MaxDeliveryCount; set => Description.MaxDeliveryCount = value; }
        public bool EnableBatchedOperations { get => Description.EnableBatchedOperations; set => Description.EnableBatchedOperations = value; }
        public bool EnableDeadLetteringOnFilterEvaluationExceptions { get => Description.EnableDeadLetteringOnFilterEvaluationExceptions; set => Description.EnableDeadLetteringOnFilterEvaluationExceptions = value; }
        public EntityStatus Status { get => Description.Status; set => Description.Status = value; }
        public string ForwardTo { get => Description.ForwardTo; set => Description.ForwardTo = value; }
        public string ForwardDeadLetteredMessagesTo { get => Description.ForwardDeadLetteredMessagesTo; set => Description.ForwardDeadLetteredMessagesTo = value; }
        public string UserMetadata { get => Description.UserMetadata; set => Description.UserMetadata = value; }
        public static implicit operator SubscriptionInfo(SubscriptionDescription description) => description == null ? null : new SubscriptionInfo(description);
        public static implicit operator SubscriptionDescription(SubscriptionInfo info) => info?.Description;
    }

    public sealed class RuleInfo : ServiceBusEntityInfo
    {
        public const string DefaultRuleName = RuleDescription.DefaultRuleName;
        public RuleInfo() : this(new RuleDescription()) { }
        public RuleInfo(string name) : this(new RuleDescription(name)) { }
        public RuleInfo(RuleDescription description) { Description = description ?? throw new ArgumentNullException(nameof(description)); }
        public RuleInfo(RuleProperties properties) : this(ServiceBusManagementMapper.From(properties)) { Properties = properties; }
        public RuleDescription Description { get; }
        public RuleProperties Properties { get; }
        public string Name { get => Description.Name; set => Description.Name = value; }
        public Filter Filter { get => Description.Filter; set => Description.Filter = value; }
        public Microsoft.ServiceBus.Messaging.RuleAction Action { get => Description.Action; set => Description.Action = value; }
        public static implicit operator RuleInfo(RuleDescription description) => description == null ? null : new RuleInfo(description);
        public static implicit operator RuleDescription(RuleInfo info) => info?.Description;
    }
}
