﻿#region Copyright
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
using System.Linq;

using Microsoft.ServiceBus;
#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    public class MainSettings
    {
        #region Internal constants
        internal const string DefaultLabel = "Service Bus Explorer";
        #endregion

        #region Public Properties

        public decimal LogFontSize { get; set; }
        public decimal TreeViewFontSize { get; set; }
        public int RetryCount { get; set; }
        public int RetryTimeout { get; set; }
        public int ReceiveTimeout { get; set; }
        public int ServerTimeout { get; set; }
        public int PrefetchCount { get; set; }
        public int TopCount { get; set; }
        public int SenderThinkTime { get; set; }
        public int ReceiverThinkTime { get; set; }
        public int MonitorRefreshInterval { get; set; }
        public bool ShowMessageCount { get; set; }
        public bool UseAscii { get; set; }
        public bool SaveMessageToFile { get; set; }
        public bool SavePropertiesToFile { get; set; }
        public bool SaveCheckpointsToFile { get; set; }
        public string Label { get; set; }
        public string MessageFile { get; set; }
        public string MessageText { get; set; }

        public List<string> SelectedEntities { get; set; }

        public string MessageBodyType { get; set; }
        public ConnectivityMode ConnectivityMode { get; set; }

        public Enums.EncodingType EncodingType { get; set; }

        #endregion

        #region Public Static Methods
        public MainSettings GetDefault()
        {
            var defaultSettings = new MainSettings();

            defaultSettings.SetDefault();

            return defaultSettings;
        }
        #endregion

        #region Public Instance Methods

        public void SetDefault()
        {
            LogFontSize = (decimal)8.25;
            TreeViewFontSize = (decimal)8.25;

            RetryCount = 10;
            RetryTimeout = 100;

            ReceiveTimeout = 1;
            ServerTimeout = 3;

            PrefetchCount = 0;
            TopCount = 10;

            SenderThinkTime = 500;
            ReceiverThinkTime = 500;

            MonitorRefreshInterval = 30;

            ShowMessageCount = true;
            UseAscii = true;
            SaveMessageToFile = true;
            SavePropertiesToFile = true;
            SaveCheckpointsToFile = true;

            Label = DefaultLabel;
            MessageFile = string.Empty;
            MessageText = string.Empty;
            SelectedEntities = ConfigurationHelper.Entities;

            MessageBodyType = BodyType.Stream.ToString();
            ConnectivityMode = ConnectivityMode.AutoDetect;
        }

        public override bool Equals(object other)
        {
            // Check if null
            if (null == other) return false;

            // Check if it's the right type
            if (!(other is MainSettings otherProperties)) return false;

            // Check all properties
            if (LogFontSize != otherProperties.LogFontSize) return false;
            if (TreeViewFontSize != otherProperties.TreeViewFontSize) return false;
            if (RetryCount != otherProperties.RetryCount) return false;
            if (RetryTimeout != otherProperties.RetryTimeout) return false;
            if (ReceiveTimeout != otherProperties.ReceiveTimeout) return false;
            if (ServerTimeout != otherProperties.ServerTimeout) return false;
            if (PrefetchCount != otherProperties.PrefetchCount) return false;
            if (TopCount != otherProperties.TopCount) return false;
            if (SenderThinkTime != otherProperties.SenderThinkTime) return false;
            if (ReceiverThinkTime != otherProperties.ReceiverThinkTime) return false;
            if (MonitorRefreshInterval != otherProperties.MonitorRefreshInterval) return false;
            if (ShowMessageCount != otherProperties.ShowMessageCount) return false;
            if (UseAscii != otherProperties.UseAscii) return false;
            if (SaveMessageToFile != otherProperties.SaveMessageToFile) return false;
            if (SavePropertiesToFile != otherProperties.SavePropertiesToFile) return false;
            if (SaveCheckpointsToFile != otherProperties.SaveCheckpointsToFile) return false;
            if (Label != otherProperties.Label) return false;
            if (MessageFile != otherProperties.MessageFile) return false;
            if (MessageText != otherProperties.MessageText) return false;

            if (!SelectedEntities.SequenceEqual(SelectedEntities)) return false;

            if (MessageBodyType != otherProperties.MessageBodyType) return false;
            if (ConnectivityMode != otherProperties.ConnectivityMode) return false;

            return true;
        }

        //// GetHashCode is based on mutable fields so these fields must not be changed while 
        // it is being in a container.
        public override int GetHashCode()
        {
            return
                RetryCount +
                RetryTimeout +
                ReceiveTimeout +
                ServerTimeout +
                PrefetchCount +
                TopCount +
                SenderThinkTime +
                ReceiverThinkTime +
                MonitorRefreshInterval +
                Label.Length;
        }

        public object GetValue(string setting)
        {
            switch (setting)
            {
                case ConfigurationParameters.LogFontSize:
                    return LogFontSize;

                case ConfigurationParameters.TreeViewFontSize:
                    return TreeViewFontSize;

                case ConfigurationParameters.RetryCountParameter:
                    return RetryCount;

                case ConfigurationParameters.RetryTimeoutParameter:
                    return RetryTimeout;

                case ConfigurationParameters.ReceiveTimeoutParameter:
                    return ReceiveTimeout;

                case ConfigurationParameters.ServerTimeoutParameter:
                    return ServerTimeout;

                case ConfigurationParameters.PrefetchCountParameter:
                    return PrefetchCount;

                case ConfigurationParameters.TopParameter:
                    return TopCount;

                case ConfigurationParameters.SenderThinkTimeParameter:
                    return SenderThinkTime;

                case ConfigurationParameters.ReceiverThinkTimeParameter:
                    return ReceiverThinkTime;

                case ConfigurationParameters.MonitorRefreshIntervalParameter:
                    return MonitorRefreshInterval;

                case ConfigurationParameters.ShowMessageCountParameter:
                    return ShowMessageCount;

                case ConfigurationParameters.UseAsciiParameter:
                    return UseAscii;

                case ConfigurationParameters.SaveMessageToFileParameter:
                    return SaveMessageToFile;

                case ConfigurationParameters.SavePropertiesToFileParameter:
                    return SavePropertiesToFile;

                case ConfigurationParameters.SaveCheckpointsToFileParameter:
                    return SaveCheckpointsToFile;

                case ConfigurationParameters.LabelParameter:
                    return Label;

                case ConfigurationParameters.FileParameter:
                    return MessageFile;

                case ConfigurationParameters.MessageParameter:
                    return MessageText;

                case ConfigurationParameters.SelectedEntitiesParameter:
                    return SelectedEntities;

                case ConfigurationParameters.MessageBodyType:
                    return MessageBodyType;

                case ConfigurationParameters.ConnectivityMode:
                    return ConnectivityMode;

                case ConfigurationParameters.Encoding:
                    return EncodingType;
            }

            throw new InvalidOperationException(String.Format("Unexpected value for setting: {0}", setting));
        }

        #endregion
    }
}
