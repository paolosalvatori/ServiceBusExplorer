#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
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
    public class MonitorInfo
    {
        #region Static Constructor
        static MonitorInfo()
        {
            MonitorInfos = new BindingList<MonitorInfo> 
            {
                new MonitorInfo {FriendlyName = "Active Message Count", Name="ActiveMessageCount", Unit="Messages"},
                new MonitorInfo {FriendlyName = "Deadletter Message Count", Name="DeadletterMessageCount", Unit="Messages"},
                new MonitorInfo {FriendlyName = "Size in KB", Name="SizeInKB", Unit = "KB"}
            };
            MonitorInfos.AllowEdit = true;
            MonitorInfos.AllowNew = true;
            MonitorInfos.AllowRemove = true;
        }          
        #endregion

        #region Public Instance Properties
        public string FriendlyName { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        #endregion

        #region Public Static Properties
        public static BindingList<MonitorInfo> MonitorInfos { get; private set; }
        #endregion
    }
}
