
using Azure.Messaging.ServiceBus.Administration;
using System;

namespace ServiceBusExplorer.Common.Abstractions;

public class QueueMetadata
{
    private QueueMetadata() { }

    // From QueueProperties
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
    public string Status { get; set; }
    public AuthorizationRules AuthorizationRules { get; set; } 
    public long MaxSizeInMegaBytes { get; set; }

    // From QueueruntimePropertiesProperties
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

    public static QueueMetadata Create(QueueProperties properties, QueueRuntimeProperties runtimeProperties)
    {
        return new()
        {
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
            Status = properties.Status.ToString(),
            AuthorizationRules = properties.AuthorizationRules,
            MaxSizeInMegaBytes = properties.MaxSizeInMegabytes,

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
}
