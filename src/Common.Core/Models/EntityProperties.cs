using System;

namespace ServiceBusExplorer.Core.Models
{
    /// <summary>Full configuration properties for a Service Bus Queue.</summary>
    public sealed class QueueProperties
    {
        public string    Name                                   { get; set; } = string.Empty;
        public EntityStatus Status                             { get; set; } = EntityStatus.Active;
        public long      MaxSizeInMegabytes                    { get; set; } = 1024;
        public int       MaxDeliveryCount                      { get; set; } = 10;
        public TimeSpan  DefaultMessageTimeToLive              { get; set; } = TimeSpan.FromDays(14);
        public TimeSpan  LockDuration                          { get; set; } = TimeSpan.FromSeconds(60);
        public TimeSpan  AutoDeleteOnIdle                      { get; set; } = TimeSpan.MaxValue;
        public bool      RequiresDuplicateDetection            { get; set; }
        public TimeSpan  DuplicateDetectionHistoryTimeWindow   { get; set; } = TimeSpan.FromMinutes(10);
        public bool      RequiresSession                       { get; set; }
        public bool      EnableDeadLetteringOnMessageExpiration { get; set; }
        public bool      EnableBatchedOperations               { get; set; } = true;
        public string?   ForwardTo                             { get; set; }
        public string?   ForwardDeadLetteredMessagesTo         { get; set; }
    }

    /// <summary>Full configuration properties for a Service Bus Topic.</summary>
    public sealed class TopicProperties
    {
        public string    Name                                   { get; set; } = string.Empty;
        public EntityStatus Status                             { get; set; } = EntityStatus.Active;
        public long      MaxSizeInMegabytes                    { get; set; } = 1024;
        public TimeSpan  DefaultMessageTimeToLive              { get; set; } = TimeSpan.FromDays(14);
        public TimeSpan  AutoDeleteOnIdle                      { get; set; } = TimeSpan.MaxValue;
        public bool      RequiresDuplicateDetection            { get; set; }
        public TimeSpan  DuplicateDetectionHistoryTimeWindow   { get; set; } = TimeSpan.FromMinutes(10);
        public bool      EnableBatchedOperations               { get; set; } = true;
        public bool      SupportOrdering                       { get; set; }
    }

    /// <summary>Full configuration properties for a Service Bus Subscription.</summary>
    public sealed class SubscriptionProperties
    {
        public string    Name                                             { get; set; } = string.Empty;
        public string    TopicPath                                        { get; set; } = string.Empty;
        public EntityStatus Status                                       { get; set; } = EntityStatus.Active;
        public int       MaxDeliveryCount                                { get; set; } = 10;
        public TimeSpan  DefaultMessageTimeToLive                        { get; set; } = TimeSpan.FromDays(14);
        public TimeSpan  LockDuration                                    { get; set; } = TimeSpan.FromSeconds(60);
        public TimeSpan  AutoDeleteOnIdle                                { get; set; } = TimeSpan.MaxValue;
        public bool      RequiresSession                                 { get; set; }
        public bool      EnableDeadLetteringOnMessageExpiration          { get; set; }
        public bool      EnableDeadLetteringOnFilterEvaluationException  { get; set; } = true;
        public bool      EnableBatchedOperations                         { get; set; } = true;
        public string?   ForwardTo                                       { get; set; }
        public string?   ForwardDeadLetteredMessagesTo                   { get; set; }
    }

    /// <summary>Portable entity status enum (maps to ServiceBusEntityStatus).</summary>
    public enum EntityStatus
    {
        Active,
        Disabled,
        SendDisabled,
        ReceiveDisabled
    }
}

