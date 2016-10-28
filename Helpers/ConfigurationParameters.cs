#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
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
        public const string MicrosoftServiceBusConnectionString = "Microsoft.ServiceBus.ConnectionString";
        #endregion
    }
}
