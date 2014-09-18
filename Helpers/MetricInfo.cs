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
using System.ComponentModel;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class MetricInfo
    {
        #region Static Constructor
        static MetricInfo()
        {
            MetricInfos = new BindingList<MetricInfo> 
            {
                new MetricInfo {FriendlyName = "Length", Name="length", Unit="Bytes", PrimaryAggregation="Max"},
                new MetricInfo {FriendlyName = "Size", Name="size", Unit="Messages", PrimaryAggregation="Total"},
                new MetricInfo {FriendlyName = "Incoming Messages", Name="incoming", Unit="Messages", PrimaryAggregation="Total"},
                new MetricInfo {FriendlyName = "Outgoing Messages", Name="outgoing", Unit="Messages", PrimaryAggregation="Total"},
                new MetricInfo {FriendlyName = "Operations Total", Name="requests.total", Unit="Messages", PrimaryAggregation="Max"},
                new MetricInfo {FriendlyName = "Successful Operations", Name="requests.successful", Unit="Messages", PrimaryAggregation="Total"},
                new MetricInfo {FriendlyName = "Failed Operations", Name="requests.failed", Unit="Messages", PrimaryAggregation="Total"},
                new MetricInfo {FriendlyName = "Internal Server Errors", Name="requests.failed.internalservererror", Unit="Messages", PrimaryAggregation="Total"},
                new MetricInfo {FriendlyName = "Server Busy Errors", Name="sts.failed.serverbusy", Unit="Messages", PrimaryAggregation="Total"},
                new MetricInfo {FriendlyName = "Other Errors", Name="requests.failed.other", Unit="Messages", PrimaryAggregation="Total"}
            };
            MetricInfos.AllowEdit = true;
            MetricInfos.AllowNew = true;
            MetricInfos.AllowRemove = true;
        }          
        #endregion

        #region Public Instance Properties
        public string FriendlyName { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string PrimaryAggregation { get; set; }
        #endregion

        #region Public Static Properties
        public static BindingList<MetricInfo> MetricInfos { get; private set; }
        #endregion
    }
}
