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
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    [Serializable]
    [XmlType(TypeName = "eventData", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    [XmlRoot(ElementName = "eventData", Namespace = "http://schemas.microsoft.com/servicebusexplorer", IsNullable = false)]
    [DataContract(Name = "eventData", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    [JsonObject(MemberSerialization.OptIn)]
    public class EventDataTemplate
    {
        #region Private Fields
        private string content;
        #endregion

        #region Public Constructors
        public EventDataTemplate()
        {
            Properties = new List<MessagePropertyInfo>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the value of the PartitionKey property of the EventData object.
        /// </summary>
        [XmlElement(ElementName = "partitionKey", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "partitionKey", Order = 1)]
        public string PartitionKey { get; set; }

        /// <summary>
        /// Gets or sets the message of the EventData object.
        /// </summary>
        [XmlIgnore]
        [JsonProperty(PropertyName = "message", Order = 2)]
        public string Message 
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            } 
        }

        /// <summary>
        /// Gets or sets the message of the EventData object.
        /// </summary>
        [XmlElement(ElementName = "message", Type = typeof(XmlCDataSection), Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        public XmlCDataSection CData 
        {
            get
            {
                return new XmlDocument().CreateCDataSection(content);
            }
            set
            {
                if (value == null)
                {
                    content = null;
                    return;

                }
                var node0 = value;
                if (node0 == null)
                {
                    throw new InvalidOperationException(string.Format("Invalid node type"));
                }
                content = node0.Value;
            }
        }

        /// <summary>
        /// Gets or sets the value of the Properties collection of the EventData object.
        /// </summary>
        [XmlArray(ElementName = "properties", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [XmlArrayItem(ElementName = "property", Type = typeof(MessagePropertyInfo))]
        [JsonProperty(PropertyName = "properties", Order = 3)]
        public List<MessagePropertyInfo> Properties { get; set; } 
        #endregion
    }
}
