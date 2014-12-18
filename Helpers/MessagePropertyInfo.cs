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
