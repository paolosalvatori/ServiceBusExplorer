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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    public class EventProcessor : IEventProcessor
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string EventProcessorOpenAsyncFormat = "[EventProcessor] Open: PartitionId=[{0}] Offset=[{1}]";
        private const string EventProcessorCloseAsyncFormat = "[EventProcessor] Close: PartitionId=[{0}] Reason=[{1}]";
        private const string EventDataSuccessfullyReceived = "[EventProcessor] Event: PartitionId=[{0}] PartitionKey=[{1}] SequenceNumber=[{2}] Offset=[{3}] EnqueuedTimeUtc=[{4}]";
        private const string EventDataSuccessfullyNoPartitionKeyReceived = "[EventProcessor] Event: PartitionId=[{0}] SequenceNumber=[{1}] Offset=[{2}] EnqueuedTimeUtc=[{3}]";
        #endregion

        #region Private Fields
        private PartitionContext partitionContext;
        private readonly EventProcessorFactoryConfiguration configuration;
        #endregion

        #region Public Constructors
        public EventProcessor()
        {
            configuration = new EventProcessorFactoryConfiguration
            {
                TrackEvent = e => new object(),
                GetElapsedTime = () => 0,
                UpdateStatistics = (a, b, c) =>{},
                MessageInspector = null,
                WriteToLog = DummyWriteToLog,
                ServiceBusHelper = null
            };
        }

        public EventProcessor(EventProcessorFactoryConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion

        #region IEventProcessor Methods
        public Task OpenAsync(PartitionContext context)
        {
            try
            {
                if (configuration.Logging)
                {
                    configuration.WriteToLog(string.Format(EventProcessorOpenAsyncFormat,
                                                       context.Lease.PartitionId,
                                                       context.Lease.Offset));
                }
                partitionContext = context;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return Task.FromResult<object>(null);
        }

        // ReSharper disable once FunctionComplexityOverflow
        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            try
            {
                if (configuration.CancellationToken.IsCancellationRequested)
                {
                    return;
                }
                var events = messages as IList<EventData> ?? messages.ToList();
                var bodySize = (long)0;
                for (var i = 0; i < events.Count; i++)
                {
                    if (configuration.CancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    if (configuration.MessageInspector != null)
                    {
                        events[i] = configuration.MessageInspector.AfterReceiveMessage(events[i]);
                    }
                    if (configuration.Logging)
                    {
                        var builder = new StringBuilder(string.IsNullOrWhiteSpace(events[i].PartitionKey)?
                                                        string.Format(EventDataSuccessfullyNoPartitionKeyReceived,
                                                        context.Lease.PartitionId,
                                                        events[i].SequenceNumber,
                                                        events[i].Offset,
                                                        events[i].EnqueuedTimeUtc):
                                                        string.Format(EventDataSuccessfullyReceived,
                                                        context.Lease.PartitionId,
                                                        events[i].PartitionKey,
                                                        events[i].SequenceNumber,
                                                        events[i].Offset,
                                                        events[i].EnqueuedTimeUtc));
                        if (configuration.Verbose)
                        {
                            configuration.ServiceBusHelper.GetMessageAndProperties(builder, events[i]);
                        }
                        configuration.WriteToLog(builder.ToString());
                    }
                    if (configuration.Tracking && !configuration.CancellationToken.IsCancellationRequested)
                    {
                        configuration.TrackEvent(events[i]);
                    }
                    bodySize += events[i].SerializedSizeInBytes;
                    if (!configuration.Checkpoint)
                    {
                        continue;
                    }
                    await partitionContext.CheckpointAsync(events[events.Count - 1]);
                }
                configuration.UpdateStatistics(events.Count, configuration.GetElapsedTime(), bodySize);
            }
            catch (LeaseLostException)
            {
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            try
            {
                if (configuration.Logging)
                {
                    configuration.WriteToLog(string.Format(EventProcessorCloseAsyncFormat,
                                                           partitionContext.Lease.PartitionId,
                                                           reason), false);
                }
                if (configuration.Checkpoint && reason == CloseReason.Shutdown)
                {
                    await context.CheckpointAsync();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion

        #region Public Static Methods
        public static void DummyWriteToLog(string message, bool async = true)
        {
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
