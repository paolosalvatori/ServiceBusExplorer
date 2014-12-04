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

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class ConfigurationParameters
    {
        #region Public Constants
        //***************************
        // Parameters
        //***************************
        public const string DebugFlagParameter = "debug";
        public const string ShowMessageCountParameter = "showMessageCount";
        public const string SaveMessageToFileParameter = "saveMessageToFile";
        public const string SavePropertiesToFileParameter = "savePropertiesToFile";
        public const string SaveCheckpointsToFileParameter = "saveCheckpointsToFile";
        public const string SchemeParameter = "scheme";
        public const string MessageParameter = "message";
        public const string FileParameter = "file";
        public const string LabelParameter = "label";
        public const string RetryCountParameter = "retryCount";
        public const string RetryTimeoutParameter = "retryTimeout";
        public const string TopParameter = "topCount";
        public const string ReceiveTimeoutParameter = "receiveTimeout";
        public const string ServerTimeoutParameter = "serverTimeout";
        public const string SenderThinkTimeParameter = "senderThinkTime";
        public const string ReceiverThinkTimeParameter = "receiverThinkTime";
        public const string MonitorRefreshIntervalParameter = "monitorRefreshInterval";
        public const string PrefetchCountParameter = "prefetchCount";
        public const string MessageDeferProviderParameter = "messageDeferProvider";
        public const string SubscriptionIdParameter = "subscriptionId";
        public const string CertificateThumbprintParameter = "certificateThumbprint";
        public const string LogFontSize = "logFontSize";
        public const string TreeViewFontSize = "treeViewFontSize";
        public const string ConnectivityMode = "connectivityMode";
        public const string Encoding = "encoding";
        public const string SelectedEntitiesParameter = "selectedEntities";
        public const string MicrosoftServiceBusConnectionString = "Microsoft.ServiceBus.ConnectionString";
        #endregion
    }
}
