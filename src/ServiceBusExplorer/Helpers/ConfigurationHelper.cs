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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    public static class ConfigurationHelper
    {
        const string DefaultLabel = "Service Bus Explorer";

        static readonly string SERVICEBUS_SECTION_NAME = "serviceBusNamespaces";

        static readonly List<string> entities = new List<string> { Constants.QueueEntities, Constants.TopicEntities,
            Constants.EventHubEntities, Constants.NotificationHubEntities, Constants.RelayEntities };

        #region Public methods

        public static void UpdateServiceBusNamespace(string key, string newKey, string newValue,
            WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(writeToLog);

            configuration.UpdateEntryInDictionarySection(SERVICEBUS_SECTION_NAME, key, newKey, newValue, writeToLog);
        }

        public static void AddServiceBusNamespace(string key, string value, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(writeToLog);

            configuration.AddEntryToDictionarySection(SERVICEBUS_SECTION_NAME, key, value);
        }

        public static void RemoveServiceBusNamespace(string key, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(writeToLog);

            configuration.RemoveEntryFromDictionarySection(SERVICEBUS_SECTION_NAME, key, writeToLog);
        }

        public static MainProperties GetMainProperties(ConfigFileUse configFileUse,
            MainProperties currentProperties, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(configFileUse, writeToLog);

            return GetMainPropertiesUsingConfiguration(configuration, currentProperties, writeToLog);
        }

        public static MainProperties GetMainProperties(MainProperties currentProperties, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(writeToLog);

            return GetMainPropertiesUsingConfiguration(configuration, currentProperties, writeToLog);
        }

        public static List<string> GetSelectedEntities(TwoFilesConfiguration configuration)
        {
            var selectedEntities = new List<string>();
            var parameter = configuration.GetStringValue(ConfigurationParameters.SelectedEntitiesParameter);

            if (!string.IsNullOrEmpty(parameter))
            {
                List<string> items = GetEntitiesAsList(parameter);
                if (items.Count == 0)
                {
                    GetDefaultSelectedEntities(selectedEntities, entities);
                }
                else
                {
                    foreach (var item in items)
                    {
                        if (entities.Contains(item, StringComparer.OrdinalIgnoreCase))
                        {
                            selectedEntities.Add(item);
                        }
                    }
                }
            }
            else
            {
                GetDefaultSelectedEntities(selectedEntities, entities);
            }

            return selectedEntities;
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
        #endregion

        #region Private static methods
        static List<string> GetEntitiesAsList(string parameter)
        {
            return parameter.Split(',').Select(item => item.Trim()).ToList();
        }

        static void GetDefaultSelectedEntities(List<string> selectedEntities, List<string> entities)
        {
            selectedEntities.AddRange(entities);
        }

        static MainProperties GetMainPropertiesUsingConfiguration(TwoFilesConfiguration configuration,
      MainProperties currentProperties, WriteToLogDelegate writeToLog)
        {
            var resultProperties = new MainProperties();

            resultProperties.TraceEnabled =
                configuration.GetBoolValue(ConfigurationParameters.DebugFlagParameter,
                currentProperties.TraceEnabled, writeToLog);

            resultProperties.MessageBodyType = configuration.GetStringValue(ConfigurationParameters.MessageBodyType,
                BodyType.Stream.ToString());

            resultProperties.ConnectivityMode = configuration.GetEnumValue
                (ConfigurationParameters.ConnectivityMode, currentProperties.ConnectivityMode, writeToLog);

            resultProperties.ShowMessageCount = configuration.GetBoolValue
                (ConfigurationParameters.ShowMessageCountParameter,
                currentProperties.ShowMessageCount, writeToLog);

            resultProperties.SaveMessageToFile = configuration.GetBoolValue
                (ConfigurationParameters.SaveMessageToFileParameter, currentProperties.SaveMessageToFile, writeToLog);

            resultProperties.SavePropertiesToFile = configuration.GetBoolValue
                (ConfigurationParameters.SavePropertiesToFileParameter,
                currentProperties.SavePropertiesToFile, writeToLog);

            resultProperties.SaveCheckpointsToFile = configuration.GetBoolValue
                (ConfigurationParameters.SaveCheckpointsToFileParameter,
                currentProperties.SaveCheckpointsToFile, writeToLog);

            resultProperties.UseAscii = configuration.GetBoolValue(ConfigurationParameters.UseAsciiParameter,
                currentProperties.UseAscii, writeToLog);

            MessageAndPropertiesHelper.GetMessageTextAndFile(configuration,
                out string messageText, out string messageFile);
            resultProperties.MessageText = messageText;
            resultProperties.MessageFile = messageFile;

            resultProperties.Label = configuration.GetStringValue(ConfigurationParameters.LabelParameter, DefaultLabel);

            resultProperties.LogFontSize = configuration.GetDecimalValue(ConfigurationParameters.LogFontSize,
                currentProperties.LogFontSize, writeToLog);

            resultProperties.TreeViewFontSize = configuration.GetDecimalValue(ConfigurationParameters.TreeViewFontSize,
                currentProperties.TreeViewFontSize, writeToLog);

            RetryHelper.RetryCount = configuration.GetIntValue(ConfigurationParameters.RetryCountParameter,
                currentProperties.RetryCount, writeToLog);

            RetryHelper.RetryTimeout = configuration.GetIntValue(ConfigurationParameters.RetryTimeoutParameter,
                currentProperties.RetryTimeout, writeToLog);

            resultProperties.ReceiveTimeout = configuration.GetIntValue(ConfigurationParameters.ReceiveTimeoutParameter,
                -1, writeToLog);

            resultProperties.ServerTimeout = configuration.GetIntValue(ConfigurationParameters.ServerTimeoutParameter,
                -1, writeToLog);

            resultProperties.SenderThinkTime = configuration.GetIntValue
                (ConfigurationParameters.SenderThinkTimeParameter, -1, writeToLog);

            resultProperties.ReceiverThinkTime = configuration.GetIntValue
                (ConfigurationParameters.ReceiverThinkTimeParameter, -1, writeToLog);

            resultProperties.MonitorRefreshInterval = configuration.GetIntValue
                (ConfigurationParameters.MonitorRefreshIntervalParameter, -1, writeToLog);

            resultProperties.PrefetchCount = configuration.GetIntValue(ConfigurationParameters.PrefetchCountParameter,
                -1, writeToLog);

            resultProperties.TopCount = configuration.GetIntValue(ConfigurationParameters.TopParameter, -1, writeToLog);

            resultProperties.SelectedEntities = ConfigurationHelper.GetSelectedEntities(configuration);

            return resultProperties;
        }

        #endregion
    }
}
