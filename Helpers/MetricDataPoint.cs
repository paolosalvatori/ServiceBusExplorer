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

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class MetricDataPoint
    {
        #region Public Properties
        public string Entity { get; set; }
        public string Type { get; set; }
        public string Metric { get; set; }
        public string Granularity { get; set; }
        public string FilterOperator1 { get; set; }
        public string FilterValue1 { get; set; }
        public string FilterOperator2 { get; set; }
        public string FilterValue2 { get; set; }
        private bool graph = true;
        public bool Graph
        {
            get { return graph; }
            set { graph = value; }
        }
        #endregion
    }
}
