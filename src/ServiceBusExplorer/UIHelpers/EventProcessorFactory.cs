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
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    public class EventProcessorFactory<T> : IEventProcessorFactory where T: class, IEventProcessor
    {
        #region Private Fields
        private readonly T instance;
        private readonly EventProcessorFactoryConfiguration configuration;
        #endregion

        #region Public Constructors
        public EventProcessorFactory()
        {
            configuration = new EventProcessorFactoryConfiguration
            {
                TrackEvent = e => new object(),
                GetElapsedTime = () => 0,
                UpdateStatistics = (a, b, c) => { },
                MessageInspector = null,
                WriteToLog = EventProcessor.DummyWriteToLog,
                ServiceBusHelper = null,
            };
        }

        public EventProcessorFactory(EventProcessorFactoryConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public EventProcessorFactory(T instance)
        {
            this.instance = instance;
        } 
        #endregion

        #region IEventProcessorFactory Methods
        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return instance ?? Activator.CreateInstance(typeof(T),  configuration) as T;
        }
        #endregion
    }
}
