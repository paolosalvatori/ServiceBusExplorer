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
using System.ServiceModel.Channels;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Helpers
{
    public class CustomMessageSessionAsyncHandlerConfiguration
    {
        #region Public Properties
        public bool Logging { get; set; }
        public bool Tracking { get; set; }
        public bool Verbose { get; set; }
        public bool AutoComplete { get; set; }
        public MessageEncoder MessageEncoder { get; set; }
        public ReceiveMode ReceiveMode { get; set; }
        public Func<BrokeredMessage, object> TrackMessage { get; set; }
        public Func<long> GetElapsedTime { get; set; }
        public Action<long, long, long> UpdateStatistics { get; set; }
        public IBrokeredMessageInspector MessageInspector { get; set; }
        public WriteToLogDelegate WriteToLog { get; set; }
        public ServiceBusHelper ServiceBusHelper { get; set; } 
        #endregion
    }
}
