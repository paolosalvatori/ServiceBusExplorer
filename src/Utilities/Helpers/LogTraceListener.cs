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

using System.Diagnostics;

#endregion

namespace ServiceBusExplorer.Utilities.Helpers
{
    #region Delegates
    public delegate void WriteToLogDelegate(string message, bool async = true);
    #endregion

    public class LogTraceListener : TraceListener
    {
        #region Private Fields
        private readonly WriteToLogDelegate writeToLog;
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the LogTraceListener class.
        /// </summary>
        public LogTraceListener(WriteToLogDelegate writeToLog)
        {
            this.writeToLog = writeToLog;
        }

        /// <summary>
        /// Initializes a new instance of the LogTraceListener class.
        /// </summary>
        /// <param name="writeToLog"></param>
        /// <param name="name">The name of the LogTraceListener.</param>
        public LogTraceListener(WriteToLogDelegate writeToLog, string name)
            : base(name)
        {
            this.writeToLog = writeToLog;
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// Writes the specified message to the log ListBox on the UI of the MainForm.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public override void Write(string message)
        {
            writeToLog(message);
        }

        /// <summary>
        /// Writes the specified message to the log ListBox on the UI of the MainForm.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public override void WriteLine(string message)
        {
            writeToLog(message);
        }
        
        /// <summary>
        /// Emits an error message to the log ListBox on the UI of the MainForm.
        /// </summary>
        /// <param name="message">The error message to write.</param>
        public override void Fail(string message)
        {
            writeToLog(message);
        }
        #endregion
    }
}
