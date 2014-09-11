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
using System.Collections.Generic;
using System.Xml.Serialization;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [XmlRoot(ElementName = "MonitorRules", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    [XmlType(TypeName = "MonitorRules", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    public class MonitorRuleList : List<MonitorRule>
    {
        #region Public Constructors
        public MonitorRuleList()
        {}

        public MonitorRuleList(int capacity): base(capacity)
        {}

        public MonitorRuleList(IEnumerable<MonitorRule> collection)
            : base(collection)
        {}
        #endregion
    }
}
