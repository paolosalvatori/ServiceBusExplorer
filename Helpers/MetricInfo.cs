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
