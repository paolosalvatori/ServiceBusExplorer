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
using System.Threading;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class EventProcessorFactoryConfiguration
    {
        #region Private Fields
        private readonly CheckBox logging;
        private readonly CheckBox tracking;
        private readonly CheckBox verbose;
        private readonly CheckBox offsetInclusive;
        private readonly CheckBox checkpoint;
        private readonly CancellationToken cancellationToken;
        #endregion

        #region Public Conctructor
        public EventProcessorFactoryConfiguration()
        {
            
        }

        public EventProcessorFactoryConfiguration(CheckBox logging,
                                                  CheckBox tracking,
                                                  CheckBox verbose,
                                                  CheckBox offsetInclusive,
                                                  CheckBox checkpoint,
                                                  CancellationToken cancellationToken)
        {
            this.logging = logging;
            this.tracking = tracking;
            this.verbose = verbose;
            this.offsetInclusive = offsetInclusive;
            this.checkpoint = checkpoint;
            this.cancellationToken = cancellationToken;
        }
        #endregion

        #region Public Properties
        public bool Logging 
        {
            get
            {
                return logging != null && (bool)logging.Invoke(new Func<bool>(()=> logging.Checked));
            }
        }

        public bool Tracking
        {
            get
            {
                return tracking != null && (bool)tracking.Invoke(new Func<bool>(() => tracking.Checked));
            }
        }

        public bool Verbose
        {
            get
            {
                return verbose != null && (bool)verbose.Invoke(new Func<bool>(() => verbose.Checked));
            }
        }

        public bool OffsetInclusive
        {
            get
            {
                return offsetInclusive != null && (bool)offsetInclusive.Invoke(new Func<bool>(() => offsetInclusive.Checked));
            }
        }

        public bool Checkpoint
        {
            get
            {
                return checkpoint != null && (bool)checkpoint.Invoke(new Func<bool>(() => checkpoint.Checked));
            }
        }

        public CancellationToken CancellationToken
        {
            get
            {
                return cancellationToken;
                
            }
        }

        public Func<EventData, object> TrackEvent { get; set; }
        public Func<long> GetElapsedTime { get; set; }
        public Action<long, long, long> UpdateStatistics { get; set; }
        public IEventDataInspector MessageInspector { get; set; }
        public WriteToLogDelegate WriteToLog { get; set; }
        public ServiceBusHelper ServiceBusHelper { get; set; }
        #endregion
    }
}
