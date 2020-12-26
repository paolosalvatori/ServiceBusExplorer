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

namespace ServiceBusExplorer.Helpers
{
    public static class ConfigurationParameters
    {
        #region Public Constants
        //***************************
        // Parameters
        //***************************
        public const string ConfigurationConfigFileParameter = "ConfigFile";
        public const string DebugFlagParameter = "debug";
        public const string ShowMessageCountParameter = "showMessageCount";
        public const string SaveMessageToFileParameter = "saveMessageToFile";
        public const string SavePropertiesToFileParameter = "savePropertiesToFile";
        public const string SaveCheckpointsToFileParameter = "saveCheckpointsToFile";
        public const string SchemeParameter = "scheme";
        public const string MessageParameter = "message";
        public const string MessageContentTypeParameter = "contentType";
        public const string FileParameter = "file";
        public const string LabelParameter = "label";
        public const string RetryCountParameter = "retryCount";
        public const string RetryTimeoutParameter = "retryTimeout";
        public const string TopParameter = "topCount";
        public const string ReceiveTimeoutParameter = "receiveTimeout";
        public const string ServerTimeoutParameter = "serverTimeout";
        public const string SenderThinkTimeParameter = "senderThinkTime";
        public const string UseAsciiParameter = "useAscii";
        public const string ReceiverThinkTimeParameter = "receiverThinkTime";
        public const string MonitorRefreshIntervalParameter = "monitorRefreshInterval";
        public const string PrefetchCountParameter = "prefetchCount";
        public const string MessageDeferProviderParameter = "messageDeferProvider";
        public const string LogFontSize = "logFontSize";
        public const string TreeViewFontSize = "treeViewFontSize";
        public const string MessageBodyType = "messageBodyType";
        public const string ConnectivityMode = "connectivityMode";
        public const string UseAmqpWebSockets = "useAmqpWebSockets";
        public const string Encoding = "encoding";
        public const string SelectedEntitiesParameter = "selectedEntities";
        public const string SelectedMessageCountsParameter = "selectedMessageCounts";
        public const string MicrosoftServiceBusConnectionString = "Microsoft.ServiceBus.ConnectionString";
        public const string DisableAccidentalDeletionPrevention = "disableAccidentalDeletionPrevention";

        public const string ProxyOverrideDefault = "Proxy.OverrideDefault";
        public const string ProxyAddress = "Proxy.Address";
        public const string ProxyBypassList = "Proxy.BypassList";
        public const string ProxyBypassOnLocal = "Proxy.BypassOnLocal";
        public const string ProxyUseDefaultCredentials = "Proxy.UseDefaultCredentials";
        public const string ProxyUserName = "Proxy.UserName";
        public const string ProxyPassword = "Proxy.Password";
        public const string NodesColors = "Colors.Nodes";

        #endregion


    }
}
