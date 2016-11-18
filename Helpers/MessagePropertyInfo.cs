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
using System.Linq;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class MessagePropertyInfo
    {
        #region Public Constructors
        public MessagePropertyInfo()
        {

            Key = null;
            Type = "String";
            Value = null;
        }

        public MessagePropertyInfo(string key, string type, object value)
        {
            Key = key;
            Type = type;
            Value = value;
        }
        #endregion

        #region Static Constructor
        static MessagePropertyInfo()
        {
            Properties = MessageAndPropertiesHelper.ReadProperties() ?? new List<MessagePropertyInfo>
                {
                    new MessagePropertyInfo("MachineName", "String", Environment.MachineName),
                    new MessagePropertyInfo("UserName", "String", Environment.UserName)
                };
        }
        #endregion

        #region Public Instance Properties
        public string Key { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        #endregion

        #region Public Static Properties
        public static List<MessagePropertyInfo> Properties { get; private set; }
        #endregion
    }
}
