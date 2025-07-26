
using Azure.Messaging.ServiceBus.Administration;
using System;

namespace Common.Models;

public class QueueMetadata
{
    // From QueueProperties
    private QueueProperties QueueProperties { get; set; }
    public string Name { get; set; }
    public TimeSpan LockDuration { get; set; }
    public long MaxSizeInMegabytes { get; set; }
    public bool RequiresDuplicateDetection { get; set; }
    public TimeSpan DuplicateDetectionHistoryTimeWindow { get; set; }
    public bool RequiresSession { get; set; }
    public bool DeadLetteringOnMessageExpiration { get; set; }
    public TimeSpan DefaultMessageTimeToLive { get; set; }
    public string ForwardTo { get; set; }
    public string ForwardDeadLetteredMessagesTo { get; set; }
    public string UserMetadata { get; set; }
    public int MaxDeliveryCount { get; set; }
    public bool EnableBatchedOperations { get; set; }
    public bool EnablePartitioning { get; set; }
    public TimeSpan AutoDeleteOnIdle { get; set; }
    public EntityStatus Status { get; set; }
    public AuthorizationRules AuthorizationRules { get; set; }
    public long? MaxMessageSizeInKilobytes { get; set; }
    public long MaxSizeInMegaBytes { get; set; }

    // From QueueRuntimeProperties
    private QueueRuntimeProperties QueueRuntimeProperties { get; set; }
    public long ActiveMessageCount { get; set; }
    public long DeadLetterMessageCount { get; set; }
    public long ScheduledMessageCount { get; set; }
    public long TransferMessageCount { get; set; }
    public long TransferDeadLetterMessageCount { get; set; }
    public long TotalMessageCount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset AccessedAt { get; set; }
    public long SizeInMegaBytes { get; set; }

    public QueueMetadata() { }

    public static QueueMetadata Create(QueueProperties properties, QueueRuntimeProperties runtimeProperties)
    {
        return new()
        {
            QueueProperties = properties,
            Name = properties.Name,
            LockDuration = properties.LockDuration,
            MaxSizeInMegabytes = properties.MaxSizeInMegabytes,
            RequiresDuplicateDetection = properties.RequiresDuplicateDetection,
            DuplicateDetectionHistoryTimeWindow = properties.DuplicateDetectionHistoryTimeWindow,
            RequiresSession = properties.RequiresSession,
            DeadLetteringOnMessageExpiration = properties.DeadLetteringOnMessageExpiration,
            DefaultMessageTimeToLive = properties.DefaultMessageTimeToLive,
            ForwardTo = properties.ForwardTo,
            ForwardDeadLetteredMessagesTo = properties.ForwardDeadLetteredMessagesTo,
            UserMetadata = properties.UserMetadata,
            MaxDeliveryCount = properties.MaxDeliveryCount,
            EnableBatchedOperations = properties.EnableBatchedOperations,
            EnablePartitioning = properties.EnablePartitioning,
            AutoDeleteOnIdle = properties.AutoDeleteOnIdle,
            Status = properties.Status,
            AuthorizationRules = properties.AuthorizationRules,
            MaxMessageSizeInKilobytes = properties.MaxMessageSizeInKilobytes,
            MaxSizeInMegaBytes = properties.MaxSizeInMegabytes,

            QueueRuntimeProperties = runtimeProperties,
            ActiveMessageCount = runtimeProperties.ActiveMessageCount,
            DeadLetterMessageCount = runtimeProperties.DeadLetterMessageCount,
            ScheduledMessageCount = runtimeProperties.ScheduledMessageCount,
            TransferMessageCount = runtimeProperties.TransferMessageCount,
            TransferDeadLetterMessageCount = runtimeProperties.TransferDeadLetterMessageCount,
            TotalMessageCount = runtimeProperties.TotalMessageCount,
            CreatedAt = runtimeProperties.CreatedAt,
            AccessedAt = runtimeProperties.AccessedAt,
            UpdatedAt = runtimeProperties.UpdatedAt,
            SizeInMegaBytes = runtimeProperties.SizeInBytes
        };
    }

    public CreateQueueOptions AsCreateQueueOptions()
    {
        var opts = new CreateQueueOptions(Name)
        {
            MaxDeliveryCount = MaxDeliveryCount,
            EnableBatchedOperations = EnableBatchedOperations,
            EnablePartitioning = EnablePartitioning,
            AutoDeleteOnIdle  = AutoDeleteOnIdle,
            Status = Status,
            DeadLetteringOnMessageExpiration = DeadLetteringOnMessageExpiration,
            DefaultMessageTimeToLive = DefaultMessageTimeToLive, 
            DuplicateDetectionHistoryTimeWindow = DuplicateDetectionHistoryTimeWindow,
            ForwardDeadLetteredMessagesTo = ForwardDeadLetteredMessagesTo,
            ForwardTo = ForwardTo,
            LockDuration = LockDuration,
            MaxMessageSizeInKilobytes = MaxMessageSizeInKilobytes,
            MaxSizeInMegabytes = MaxSizeInMegabytes,
            RequiresDuplicateDetection = RequiresDuplicateDetection,
            RequiresSession = RequiresSession,
            UserMetadata = UserMetadata
        };

        foreach (var rule in AuthorizationRules) opts.AuthorizationRules.Add(rule);

        return opts; 
    }

    public QueueProperties AsQueueProperties => QueueProperties;
    public QueueRuntimeProperties AsQueueRuntimeProperties => QueueRuntimeProperties;

}
