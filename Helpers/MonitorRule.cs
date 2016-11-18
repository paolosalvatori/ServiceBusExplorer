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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Serialization;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public enum MonitorState
    {
        Normal,
        Warning,
        Critical
    }

    public class MonitorRule
    {
        #region Private Fields
        private MonitorState monitorState = MonitorState.Normal;
        #endregion

        #region Public Properties
        public string Entity { get; set; }
        public string Type { get; set; }
        public string Monitor { get; set; }
        public long Warning { get; set; }
        public long Critical { get; set; }
        public long Current { get; set; }
        public bool Valid { get; set; }
        [XmlIgnore]
        public Series Series { get; set; }
        [XmlIgnore]
        public MonitorState State
        {
            get { return monitorState; }
            set { monitorState = value; }
        }
        #endregion
    }
}
