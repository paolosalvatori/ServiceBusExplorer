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
using System.Diagnostics;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
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
        public LogTraceListener()
        {
            writeToLog = MainForm.StaticWriteToLog;
        }

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
