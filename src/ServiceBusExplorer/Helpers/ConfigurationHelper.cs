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
        static readonly string SERVICEBUS_SECTION_NAME = "serviceBusNamespaces";

        static readonly List<string> entities = new List<string> { Constants.QueueEntities, Constants.TopicEntities,
            Constants.EventHubEntities, Constants.NotificationHubEntities, Constants.RelayEntities };

        #region Public methods

        public static void UpdateServiceBusNamespace(string key, string newKey, string newValue, WriteToLogDelegate writeToLog)
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

        public static List<string> GetEntitiesAsList(string parameter)
        {
            return parameter.Split(',').Select(item => item.Trim()).ToList();
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
        static void GetDefaultSelectedEntities(List<string> selectedEntities, List<string> entities)
        {
            selectedEntities.AddRange(entities);
        }
        #endregion
    }
}
