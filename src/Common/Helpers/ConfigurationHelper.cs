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

using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceBusExplorer.Helpers
{
    public static class ConfigurationHelper
    {
        static readonly string SERVICEBUS_SECTION_NAME = "serviceBusNamespaces";

        static readonly List<string> entities = new List<string> { Constants.QueueEntities, Constants.TopicEntities,
            Constants.EventHubEntities, Constants.NotificationHubEntities, Constants.RelayEntities };

        static readonly List<string> messageCounts = new List<string> { Constants.ActiveMessages, Constants.DeadLetterMessages,
            Constants.ScheduledMessages, Constants.TransferMessages, Constants.TransferDeadLetterMessages };

        #region Public methods

        public static void UpdateServiceBusNamespace(ConfigFileUse configFileUse, string key, string newKey, string newValue,
            WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(configFileUse, writeToLog);

            configuration.UpdateEntryInDictionarySection(SERVICEBUS_SECTION_NAME, key, newKey, newValue, writeToLog);
        }

        public static void AddServiceBusNamespace(ConfigFileUse configFileUse, string key, string value, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(configFileUse, writeToLog);

            configuration.AddEntryToDictionarySection(SERVICEBUS_SECTION_NAME, key, value);
        }

        public static void RemoveServiceBusNamespace(ConfigFileUse configFileUse, string key, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(configFileUse, writeToLog);

            configuration.RemoveEntryFromDictionarySection(SERVICEBUS_SECTION_NAME, key, writeToLog);
        }

        public static MainSettings GetMainProperties(ConfigFileUse configFileUse,
            MainSettings currentSettings, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(configFileUse, writeToLog);

            return GetMainSettingsUsingConfiguration(configuration, currentSettings, writeToLog);
        }

        #endregion

        #region Public static properties
        public static List<string> Entities
        {
            get
            {
                return entities;
            }
        }

        public static List<string> MessageCounts
        {
            get
            {
                return messageCounts;
            }
        }
        #endregion

        #region Private static methods
        static List<string> GetSelectedEntities(TwoFilesConfiguration configuration)
        {
            return GetListFromSettings(configuration, ConfigurationParameters.SelectedEntitiesParameter, entities, entities);
        }

        static List<string> GetSelectedMessageCounts(TwoFilesConfiguration configuration)
        {
            return GetListFromSettings(configuration, ConfigurationParameters.SelectedMessageCountsParameter, messageCounts,
                new List<string> { Constants.ActiveMessages, Constants.DeadLetterMessages, Constants.TransferDeadLetterMessages }
                );
        }

        static List<string> GetParameterValueAsList(string parameter)
        {
            return parameter.Split(',').Select(item => item.Trim()).ToList();
        }

        static List<string> GetListFromSettings(TwoFilesConfiguration configuration, string configurationKey, List<string> allowedItems, List<string> defaultItems)
        {
            var selectedItems = new List<string>();
            var parameter = configuration.GetStringValue(configurationKey);

            if (!string.IsNullOrEmpty(parameter))
            {
                var items = GetParameterValueAsList(parameter);
                if (items.Count == 0)
                {
                    selectedItems.AddRange(defaultItems);
                }
                else
                {
                    foreach (var item in items)
                    {
                        if (allowedItems.Contains(item, StringComparer.OrdinalIgnoreCase))
                        {
                            selectedItems.Add(item);
                        }
                    }
                }
            }
            else
            {
                selectedItems.AddRange(defaultItems);
            }

            return selectedItems;
        }

        static MainSettings GetMainSettingsUsingConfiguration(TwoFilesConfiguration configuration,
            MainSettings currentSettings, WriteToLogDelegate writeToLog)
        {
            var resultProperties = new MainSettings();

            resultProperties.LogFontSize = configuration.GetDecimalValue(ConfigurationParameters.LogFontSize,
                currentSettings.LogFontSize, writeToLog);

            resultProperties.TreeViewFontSize = configuration.GetDecimalValue(ConfigurationParameters.TreeViewFontSize,
                currentSettings.TreeViewFontSize, writeToLog);

            resultProperties.RetryCount = configuration.GetIntValue(ConfigurationParameters.RetryCountParameter,
                currentSettings.RetryCount, writeToLog);

            resultProperties.RetryTimeout = configuration.GetIntValue(ConfigurationParameters.RetryTimeoutParameter,
                currentSettings.RetryTimeout, writeToLog);

            resultProperties.ReceiveTimeout = configuration.GetIntValue(ConfigurationParameters.ReceiveTimeoutParameter,
                currentSettings.ReceiveTimeout, writeToLog);

            resultProperties.ServerTimeout = configuration.GetIntValue(ConfigurationParameters.ServerTimeoutParameter,
                currentSettings.ServerTimeout, writeToLog);

            resultProperties.PrefetchCount = configuration.GetIntValue(ConfigurationParameters.PrefetchCountParameter,
                currentSettings.PrefetchCount, writeToLog);

            resultProperties.TopCount = configuration.GetIntValue(ConfigurationParameters.TopParameter, 
                currentSettings.TopCount, writeToLog);

            resultProperties.SenderThinkTime = configuration.GetIntValue
                (ConfigurationParameters.SenderThinkTimeParameter, currentSettings.SenderThinkTime, writeToLog);

            resultProperties.ReceiverThinkTime = configuration.GetIntValue
                (ConfigurationParameters.ReceiverThinkTimeParameter, currentSettings.ReceiverThinkTime, writeToLog);

            resultProperties.MonitorRefreshInterval = configuration.GetIntValue
                (ConfigurationParameters.MonitorRefreshIntervalParameter, 
                currentSettings.MonitorRefreshInterval, writeToLog);

            resultProperties.ShowMessageCount = configuration.GetBoolValue
                (ConfigurationParameters.ShowMessageCountParameter,
                currentSettings.ShowMessageCount, writeToLog);

            resultProperties.UseAscii = configuration.GetBoolValue(ConfigurationParameters.UseAsciiParameter,
                currentSettings.UseAscii, writeToLog);

            resultProperties.SaveMessageToFile = configuration.GetBoolValue
                (ConfigurationParameters.SaveMessageToFileParameter, currentSettings.SaveMessageToFile, writeToLog);

            resultProperties.SavePropertiesToFile = configuration.GetBoolValue
                (ConfigurationParameters.SavePropertiesToFileParameter,
                currentSettings.SavePropertiesToFile, writeToLog);

            resultProperties.SaveCheckpointsToFile = configuration.GetBoolValue
                (ConfigurationParameters.SaveCheckpointsToFileParameter,
                currentSettings.SaveCheckpointsToFile, writeToLog);

            resultProperties.Label = configuration.GetStringValue(ConfigurationParameters.LabelParameter, 
                MainSettings.DefaultLabel);

            MessageAndPropertiesHelper.GetMessageTextAndFile(configuration,
                out string messageText, out string messageFile);
            resultProperties.MessageText = messageText;
            resultProperties.MessageFile = messageFile;
            
            resultProperties.MessageContentType = configuration.GetStringValue(ConfigurationParameters.MessageContentTypeParameter,
                string.Empty);

            resultProperties.SelectedEntities = ConfigurationHelper.GetSelectedEntities(configuration);
            resultProperties.SelectedMessageCounts = ConfigurationHelper.GetSelectedMessageCounts(configuration);

            resultProperties.MessageBodyType = configuration.GetStringValue(ConfigurationParameters.MessageBodyType,
                BodyType.Stream.ToString());

            resultProperties.ConnectivityMode = configuration.GetEnumValue
                (ConfigurationParameters.ConnectivityMode, currentSettings.ConnectivityMode, writeToLog);
            resultProperties.UseAmqpWebSockets = configuration.GetBoolValue
                (ConfigurationParameters.UseAmqpWebSockets, currentSettings.UseAmqpWebSockets, writeToLog);
            resultProperties.EncodingType = configuration.GetEnumValue
                (ConfigurationParameters.Encoding, currentSettings.EncodingType, writeToLog);

            resultProperties.DisableAccidentalDeletionPrevention = configuration.GetBoolValue
                (ConfigurationParameters.DisableAccidentalDeletionPrevention, currentSettings.DisableAccidentalDeletionPrevention, writeToLog);

            resultProperties.ProxyOverrideDefault = configuration.GetBoolValue(ConfigurationParameters.ProxyOverrideDefault, currentSettings.ProxyOverrideDefault, writeToLog);
            resultProperties.ProxyUseDefaultCredentials = configuration.GetBoolValue(ConfigurationParameters.ProxyUseDefaultCredentials, currentSettings.ProxyUseDefaultCredentials, writeToLog);
            resultProperties.ProxyBypassOnLocal = configuration.GetBoolValue(ConfigurationParameters.ProxyBypassOnLocal, currentSettings.ProxyBypassOnLocal, writeToLog);
            resultProperties.ProxyAddress = configuration.GetStringValue(ConfigurationParameters.ProxyAddress, string.Empty);
            resultProperties.ProxyBypassList = configuration.GetStringValue(ConfigurationParameters.ProxyBypassList, string.Empty);
            resultProperties.ProxyUserName = configuration.GetStringValue(ConfigurationParameters.ProxyUserName, string.Empty);
            resultProperties.ProxyPassword = configuration.GetStringValue(ConfigurationParameters.ProxyPassword, string.Empty);
            resultProperties.NodesColors = NodeColorInfo.ParseAll(configuration.GetStringValue(ConfigurationParameters.NodesColors, string.Empty));

            return resultProperties;
        }
        #endregion
    }
}
