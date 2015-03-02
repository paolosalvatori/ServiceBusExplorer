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
using System.IO;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [Serializable]
    public class EventProcessorCheckpointInfo
    {
        #region Public Instance Properties
        [JsonProperty(PropertyName = "namespace", Order = 1)]
        public string Namespace { get; set; }

        [JsonProperty(PropertyName = "eventHub", Order = 2)]
        public string EventHub { get; set; }

        [JsonProperty(PropertyName = "consumerGroup", Order = 3)]
        public string ConsumerGroup { get; set; }

        [JsonProperty(PropertyName = "leases", Order = 4)]
        public Dictionary<string, Lease> Leases { get; set; } 
        #endregion

        #region Public Overriden Methods
        public override bool Equals(object obj)
        {
            var e = obj as EventProcessorCheckpointInfo;

            return e != null && 
                   string.Compare(Namespace, e.Namespace, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                   string.Compare(EventHub, e.EventHub, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        } 
        #endregion
    }
}
