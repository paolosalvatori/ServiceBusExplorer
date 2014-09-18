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
