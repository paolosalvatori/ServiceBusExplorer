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
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

#endregion

namespace ServiceBusExplorer.Helpers
{
    [Serializable]
    [XmlType(TypeName = "event", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    [XmlRoot(ElementName = "event", Namespace = "http://schemas.microsoft.com/servicebusexplorer", IsNullable = false)]
    [DataContract(Name = "event", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
    public class ThresholdDeviceMessage
    {
        /// <summary>
        /// Gets or sets the device id.
        /// </summary>
        [XmlElement(ElementName = "eventId", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "eventId", Order = 1)]
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the device id.
        /// </summary>
        [XmlElement(ElementName = "deviceId", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "deviceId", Order = 2)]
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the device value.
        /// </summary>
        [XmlElement(ElementName = "value", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "value", Order = 3)]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the event timestamp.
        /// </summary>
        [XmlElement(ElementName = "timestamp", Namespace = "http://schemas.microsoft.com/servicebusexplorer")]
        [JsonProperty(PropertyName = "timestamp", Order = 4)]
        public DateTime Timestamp { get; set; }
    }

    public class ThresholdDeviceBrokeredMessageGenerator : IBrokeredMessageGenerator, IDisposable
    {
        #region Public Constants
        //***************************
        // Constants
        //***************************
        private const string GeneratorProperties = "Generator Properties";
        private const string CustomProperties = "Custom Properties";
        private const string SystemProperties = "System Properties";
        private const string MinDeviceIdDescription = "Gets or sets the minimum device id.";
        private const string MaxDeviceIdDescription = "Gets or sets the maximum device id.";
        private const string MinValueDescription = "Gets or sets the minimum value.";
        private const string MaxValueDescription = "Gets or sets the maximum value.";
        private const string MessageFormatDescription = "Gets or sets the message format: JSON or XML.";
        private const string CityDescription = "Gets or sets the city.";
        private const string CountryDescription = "Gets or sets the country.";
        private const string LabelDescription = "Gets or sets the Label property of the BrokeredMessage.";

        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string BrokeredMessageCreatedFormat = "[ThresholdDeviceBrokeredMessageGenerator] {0} objects have been successfully created.";
        #endregion

        #region Public Static Fields
        public static int EventId;
        #endregion

        #region Public Constructor
        public ThresholdDeviceBrokeredMessageGenerator()
        {
            MinDeviceId = 1;
            MaxDeviceId = 100;
            MinValue = 1;
            MaxValue = 100;
            City = "Milan";
            Country = "Italy";
            Label = "Service Bus Explorer";
        } 
        #endregion

        #region IBrokeredMessageGenerator Methods
        public IEnumerable<BrokeredMessage> GenerateBrokeredMessageCollection(int brokeredMessageCount, WriteToLogDelegate writeToLog)
        {
            if (brokeredMessageCount < 0)
            {
                return null;
            }
            var random = new Random();
            var messageList = new List<BrokeredMessage>();
            for (var i = 0; i < brokeredMessageCount; i++)
            {
                try
                {
                    var payload = new ThresholdDeviceEvent
                    {
                        EventId = EventId++,
                        DeviceId = random.Next(MinDeviceId, MaxDeviceId + 1),
                        Value = random.Next(MinDeviceId, MaxDeviceId + 1),
                        Timestamp = DateTime.UtcNow
                    };
                    var text = MessageFormat == MessageFormat.Json
                        ? JsonSerializerHelper.Serialize(payload)
                        : XmlSerializerHelper.Serialize(payload);
                    var brokeredMessage = new BrokeredMessage(text.ToMemoryStream())
                    {
                        MessageId = payload.DeviceId.ToString(CultureInfo.InvariantCulture),
                    };
                    brokeredMessage.Properties.Add("eventId", payload.EventId);
                    brokeredMessage.Properties.Add("deviceId", payload.DeviceId);
                    brokeredMessage.Properties.Add("value", payload.Value);
                    brokeredMessage.Properties.Add("time", DateTime.UtcNow.Ticks);
                    brokeredMessage.Properties.Add("city", City);
                    brokeredMessage.Properties.Add("country", Country);
                    brokeredMessage.Label = Label;
                    messageList.Add(brokeredMessage);
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrWhiteSpace(ex.Message))
                    {
                        writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
                    }
                    if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                    {
                        writeToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
                    }
                }
            }
            if (writeToLog != null)
            {
                writeToLog(string.Format(BrokeredMessageCreatedFormat, messageList.Count));
            }
            return messageList;
        } 
        #endregion

        #region IDisposable Methods
        public void Dispose()
        {
        }
        #endregion

        #region Public Properties
        [Category(GeneratorProperties)]
        [Description(MinDeviceIdDescription)]
        [DefaultValue(1)]
        public int MinDeviceId { get; set; }

        [Category(GeneratorProperties)]
        [Description(MaxDeviceIdDescription)]
        [DefaultValue(100)]
        public int MaxDeviceId { get; set; }

        [Category(GeneratorProperties)]
        [Description(MinValueDescription)]
        public int MinValue { get; set; }

        [Category(GeneratorProperties)]
        [Description(MaxValueDescription)]
        public int MaxValue { get; set; }

        [Category(GeneratorProperties)]
        [Description(MessageFormatDescription)]
        public MessageFormat MessageFormat { get; set; }

        [Category(CustomProperties)]
        [Description(CityDescription)]
        public string City { get; set; }

        [Category(CustomProperties)]
        [Description(CountryDescription)]
        public string Country { get; set; }

        [Category(SystemProperties)]
        [Description(LabelDescription)]
        public string Label { get; set; }
        #endregion
    }
}
