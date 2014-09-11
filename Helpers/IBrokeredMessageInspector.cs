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
using Microsoft.ServiceBus.Messaging; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public interface IBrokeredMessageInspector
    {
        BrokeredMessage BeforeSendMessage(BrokeredMessage message, WriteToLogDelegate writeToLog = null);
        BrokeredMessage AfterReceiveMessage(BrokeredMessage message, WriteToLogDelegate writeToLog = null);
    }
}
