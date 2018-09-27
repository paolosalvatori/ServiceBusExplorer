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

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    public static class ConfigurationHelper
    {
        static readonly string SERVICEBUS_SECTION_NAME = "serviceBusNamespaces";

        #region Public methods

        public static void UpdateServiceBusNamespace(string key, string newKey, string newValue, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(writeToLog);;

            configuration.UpdateEntryInDictionarySection(SERVICEBUS_SECTION_NAME, key, newKey, newValue, writeToLog);
        }

        public static void AddServiceBusNamespace(string key, string value, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(writeToLog);;

            configuration.AddEntryToDictionarySection(SERVICEBUS_SECTION_NAME, key, value);
        }

        public static void RemoveServiceBusNamespace(string key, WriteToLogDelegate writeToLog)
        {
            var configuration = TwoFilesConfiguration.Create(writeToLog);;

            configuration.RemoveEntryFromDictionarySection(SERVICEBUS_SECTION_NAME, key, writeToLog);
        }
        
        #endregion
    }
}
