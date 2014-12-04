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
using Microsoft.ServiceBus.Messaging; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class CustomMessageSessionAsyncHandlerFactory : IMessageSessionAsyncHandlerFactory
    {
        #region Public Properties
        public CustomMessageSessionAsyncHandlerConfiguration Configuration { get; set; } 
        #endregion

        #region Public Constructor
        public CustomMessageSessionAsyncHandlerFactory(CustomMessageSessionAsyncHandlerConfiguration configuration)
        {
            Configuration = configuration;
        } 
        #endregion

        #region IMessageSessionAsyncHandlerFactory Methods
        public IMessageSessionAsyncHandler CreateInstance(MessageSession session, BrokeredMessage message)
        {
            return new CustomMessageSessionAsyncHandler(Configuration);
        }

        public void DisposeInstance(IMessageSessionAsyncHandler handler)
        {
        } 
        #endregion
    }
}
