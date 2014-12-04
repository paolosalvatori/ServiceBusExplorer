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

#region Using Directives
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class CustomMessageSessionAsyncHandler : IMessageSessionAsyncHandler
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";

        //***************************
        // Texts
        //***************************
        private const string NullValue = "NULL";

        //***************************
        // Messages
        //***************************
        private const string MessageSuccessfullyReceived = "Message received. MessageId=[{0}] SessionId=[{1}] Label=[{2}] Size=[{3}]";
        private const string SessionClosed = "Session[{0}] closed. Message Count=[{1}]";
        #endregion

        #region Private Fields
        private readonly CustomMessageSessionAsyncHandlerConfiguration configuration;
        #endregion

        #region Public Static Fields
        public static readonly Dictionary<string, int> SessionDictionary = new Dictionary<string, int>();
        #endregion

        #region Public Constructors
        public CustomMessageSessionAsyncHandler(CustomMessageSessionAsyncHandlerConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        #region IMessageSessionAsyncHandler Methods
        public async Task OnMessageAsync(MessageSession session, BrokeredMessage message)
        {
            try
            {
                if (message == null)
                {
                    return;
                }
                if (session == null)
                {
                    return;
                }
                if (configuration == null)
                {
                    return;
                }
                if (configuration.MessageInspector != null)
                {
                    message = configuration.MessageInspector.AfterReceiveMessage(message);
                }
                if (configuration.Logging)
                {
                    var builder = new StringBuilder(string.Format(MessageSuccessfullyReceived,
                                                    string.IsNullOrWhiteSpace(message.MessageId)
                                                        ? NullValue
                                                        : message.MessageId,
                                                    string.IsNullOrWhiteSpace(message.SessionId)
                                                        ? NullValue
                                                        : message.SessionId,
                                                    string.IsNullOrWhiteSpace(message.Label)
                                                        ? NullValue
                                                        : message.Label,
                                                    message.Size));
                    if (configuration.Verbose)
                    {
                        configuration.ServiceBusHelper.GetMessageAndProperties(builder, message, configuration.MessageEncoder);
                    }
                    configuration.WriteToLog(builder.ToString(), false);
                }
                if (configuration.Tracking)
                {
                    configuration.TrackMessage(message.Clone());
                }
                configuration.UpdateStatistics(1, configuration.GetElapsedTime(), message.Size);
                if (configuration.ReceiveMode == ReceiveMode.PeekLock && !configuration.AutoComplete)
                {
                    await message.CompleteAsync();
                }
                if (!SessionDictionary.ContainsKey(message.SessionId))
                {
                    SessionDictionary.Add(message.SessionId, 1);
                }
                else
                {
                    SessionDictionary[message.SessionId]++;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public Task OnCloseSessionAsync(MessageSession session)
        {
            if (session == null)
            {
                return Task.FromResult(0);
            }
            if (configuration == null)
            {
                return Task.FromResult(0);
            }
            configuration.WriteToLog(string.Format(SessionClosed, session.SessionId, SessionDictionary[session.SessionId]));
            return Task.FromResult(0);
        }

        public Task OnSessionLostAsync(Exception exception)
        {
            if (exception == null)
            {
                return Task.FromResult(0);
            }
            HandleException(exception);
            return Task.FromResult(0);
        }
        #endregion

        #region Private Methods
        private void HandleException(Exception ex)
        {
            if (ex == null || string.IsNullOrWhiteSpace(ex.Message))
            {
                return;
            }
            if (configuration == null)
            {
                return;
            }
            configuration.WriteToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                configuration.WriteToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }
        #endregion
    }
}
