#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team 
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
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [XmlType(TypeName = "property", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    [XmlRoot(ElementName = "property", Namespace = "http://schemas.microsoft.com/servicebusexplorer", IsNullable = false)]
    [DataContract(Name = "property", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    [JsonObject(MemberSerialization.OptIn)]
    public class MessagePropertyInfo
    {
        #region Private Fields
        private object internalValue;
        #endregion

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
        [XmlElement(ElementName = "key", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "key", Order = 1, Required = Required.Always)]
        public string Key { get; set; }

        [XmlElement(ElementName = "type", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "type", Order = 2, Required = Required.Always)]
        public string Type { get; set; }

        [XmlIgnore]
        public object Value
        {
            get
            {
                return internalValue;
            }
            set
            {
                internalValue = value;
            }
        }

        [XmlElement(ElementName = "value", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "value", Order = 3)]
        public string ValueAsString 
        {
            get
            {
                return Convert.ToString(internalValue);
            }
            set
            {
                internalValue = value;
            }
        }
        #endregion

        #region Public Static Properties
        public static List<MessagePropertyInfo> Properties { get; private set; }
        #endregion
    }
}
