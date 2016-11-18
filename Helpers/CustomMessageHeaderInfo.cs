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
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class CustomMessageHeaderInfo
    {
        #region Public Constructors
        public CustomMessageHeaderInfo()
        {

            Name = null;
            Namespace = null;
            Value = null;
        }

        public CustomMessageHeaderInfo(string key, string type, string value)
        {
            Name = key;
            Namespace = type;
            Value = value;
        }
        #endregion

        #region Static Constructor
        static CustomMessageHeaderInfo()
        {
            Headers = new List<CustomMessageHeaderInfo>();
        }
        #endregion

        #region Public Instance Properties
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Value { get; set; }
        #endregion

        #region Public Static Properties
        public static List<CustomMessageHeaderInfo> Headers { get; set; }
        #endregion
    }
}