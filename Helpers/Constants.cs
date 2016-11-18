#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class Constants
    {
        public static readonly TimeSpan DefaultOperationTimeout = TimeSpan.FromMinutes(1.0);
        public static readonly TimeSpan TokenRequestOperationTimeout = TimeSpan.FromMinutes(3.0);
        public static readonly long ServicePointMaxIdleTimeMilliSeconds = 0xc350;
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
    }
}
