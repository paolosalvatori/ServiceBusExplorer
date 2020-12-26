#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Helpers
{
    public static class Constants
    {
        public const string QueueEntities = "Queues";
        public const string TopicEntities = "Topics";
        public const string RelayEntities = "Relays";
        public const string EventHubEntities = "Event Hubs";
        public const string NotificationHubEntities = "Notification Hubs";

        public const string ActiveMessages = "Active";
        public const string DeadLetterMessages = "DeadLettered";
        public const string ScheduledMessages = "Scheduled";
        public const string TransferMessages = "InTransfer";
        public const string TransferDeadLetterMessages = "Transfer DL";
        
        public static readonly TimeSpan DefaultOperationTimeout = TimeSpan.FromMinutes(1.0);
        public static readonly TimeSpan TokenRequestOperationTimeout = TimeSpan.FromMinutes(3.0);
        public static readonly long ServicePointMaxIdleTimeMilliSeconds = 50000;
        public static readonly TimeSpan DefaultBatchFlushInterval = TimeSpan.FromMilliseconds(20.0);
        public static readonly double DefaultUsedSpaceAlertPercentage = 70.0;
        public static readonly TimeSpan DefaultLockDuration = TimeSpan.FromSeconds(60.0);
        public static readonly TimeSpan  DefaultDuplicateDetectionHistoryExpiryDuration = TimeSpan.FromMinutes(10.0);
        public static readonly TimeSpan DefaultAllowedTimeToLive = TimeSpan.MaxValue;
        public static readonly TimeSpan AutoDeleteOnIdleDefaultValue = TimeSpan.MaxValue;
        public static readonly TimeSpan MaximumAllowedTimeToLive = TimeSpan.MaxValue;
        public static readonly TimeSpan MinimumAllowedTimeToLive = TimeSpan.FromSeconds(1.0);
        public static readonly TimeSpan MaximumLockDuration = TimeSpan.FromMinutes(5.0);
        public static readonly TimeSpan DebuggingLockDuration = TimeSpan.FromDays(1.0);
        public static readonly TimeSpan MinimumAllowedIdleTimeoutForAutoDelete = TimeSpan.FromMinutes(5.0);
        public static readonly TimeSpan MaximumAllowedIdleTimeoutForAutoDelete = TimeSpan.MaxValue;
        public static readonly TimeSpan MaximumDuplicateDetectionHistoryTimeWindow = TimeSpan.FromDays(7.0);
        public static readonly TimeSpan MinimumDuplicateDetectionHistoryTimeWindow = TimeSpan.FromSeconds(20.0);
        public static readonly TimeSpan DefaultRetryDelay = TimeSpan.FromSeconds(10.0);
        public static readonly int DefaultRetryLimit = 3;
        public static readonly int DefaultSqlFlushThreshold = 0x1194;
        public static readonly int FlushBatchThreshold = 100;
        public static readonly int MaximumRequestSchedulerQueueDepth = 0x3a98;
        public static readonly int MaximumBatchSchedulerQueueDepth = 0x186a0;
        public static readonly int MaximumEntityNameLength = 400;
        public static readonly int MaximumMessageHeaderPropertySize = 0xffff;
        public static readonly string ContainerShortName = ".";
        public static readonly int DefaultPrefetchCount = 0;
        public static readonly int DefaultMessageSessionPrefetchCount = 0;
        public static readonly int DefaultMaxDeliveryCount = 10;
        public static readonly int MinAllowedMaxDeliveryCount = 1;
        public static readonly int MaxAllowedMaxDeliveryCount = 0x7fffffff;
        public static readonly long DefaultLastPeekedSequenceNumber = 0L;
        public static readonly TimeSpan DefaultRegistrationTtl = TimeSpan.FromDays(90.0);
        public static readonly TimeSpan MaximumRegistrationTtl = TimeSpan.FromDays(90.0);
        public static readonly TimeSpan MinimumRegistrationTtl = TimeSpan.FromDays(1.0);
        public static readonly string Windows = "windows";
        public static readonly string IssuedToken = "issuedToken";
        public static readonly string Anonymous = "anonymous";
        public static readonly List<string> SupportedClaims = new List<string> { "Manage", "Send", "Listen" };
        public static readonly List<string> SupportedSubQueueNames = new List<string> { "$DeadLetterQueue" };
        public static readonly Type ConstantsType = typeof(Constants);
        public static readonly Type MessageType = typeof(BrokeredMessage);
        public static readonly Type GuidType = typeof(Guid);
        public static readonly Type ObjectType = typeof(object);
        public static readonly List<string> AlwaysOmittedProperties = new List<string> {"DeadLetterReason", "DeadLetterErrorDescription", "NServiceBus.Transport.Recovery"};
    }
}
