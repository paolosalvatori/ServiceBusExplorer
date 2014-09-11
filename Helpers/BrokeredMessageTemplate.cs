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
    [XmlType(TypeName = "brokeredMessage", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    [XmlRoot(ElementName = "brokeredMessage", Namespace = "http://schemas.microsoft.com/servicebusexplorer", IsNullable = false)]
    [DataContract(Name = "brokeredMessage", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    [JsonObject(MemberSerialization.OptIn)]
    public class BrokeredMessageTemplate
    {
        #region Private Fields
        private string content;
        #endregion

        #region Public Constructors
        public BrokeredMessageTemplate()
        {
            Properties = new List<MessagePropertyInfo>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the value of the MessageId property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "messageId", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "messageId", Order = 1)]
        public string MessageId { get; set; }

        /// <summary>
        /// Gets or sets the value of the SessionId property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "sessionId", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "sessionId", Order = 2)]
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the value of the CorrelationId property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "correlationId", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "correlationId", Order = 3)]
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the value of the ContentType property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "contentType", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "contentType", Order = 4)]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the value of the Label property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "label", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "label", Order = 5)]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value of the PartitionKey property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "partitionKey", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "partitionKey", Order = 6)]
        public string PartitionKey { get; set; }

        /// <summary>
        /// Gets or sets the value of the To property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "to", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "to", Order = 7)]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the value of the ReplyTo property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "replyTo", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "replyTo", Order = 8)]
        public string ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the value of the ReplyToSessionId property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "replyToSessionId", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "replyToSessionId", Order = 9)]
        public string ReplyToSessionId { get; set; }

        /// <summary>
        /// Gets or sets the value of the TimeToLive property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "timeToLive", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "timeToLive", Order = 10)]
        public string TimeToLive { get; set; }

        /// <summary>
        /// Gets or sets the value of the ScheduledEnqueueTimeUtc property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "scheduledEnqueueTimeUtc", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "scheduledEnqueueTimeUtc", Order = 11)]
        public string ScheduledEnqueueTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the value of the ForcePersistence property of the BrokeredMessage object.
        /// </summary>
        [XmlElement(ElementName = "forcePersistence", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "forcePersistence", Order = 12)]
        public bool ForcePersistence { get; set; }

        /// <summary>
        /// Gets or sets the message of the BrokeredMessage object.
        /// </summary>
        [XmlIgnore]
        [JsonProperty(PropertyName = "message", Order = 13)]
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
        /// Gets or sets the message of the BrokeredMessage object.
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
        /// Gets or sets the value of the Properties collection of the BrokeredMessage object.
        /// </summary>
        [XmlArray(ElementName = "properties", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [XmlArrayItem(ElementName = "property", Type = typeof(MessagePropertyInfo))]
        [JsonProperty(PropertyName = "properties", Order = 14)]
        public List<MessagePropertyInfo> Properties { get; set; } 
        #endregion
    }
}
