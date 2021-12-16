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
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Utilities.Helpers;
#endregion

namespace ServiceBusExplorer.Controls
{
    public abstract class TestControlBase : UserControl
    {
        #region Protected Constants
        //***************************
        // Formats
        //***************************
        protected const string ExceptionFormat = "Exception: {0}";
        protected const string InnerExceptionFormat = "InnerException: {0}";
        protected const string LabelFormat = "{0:0.000}";
        #endregion

        #region Protected instance fields
        protected bool isReadyToStoreMessageText;
        protected readonly ServiceBusHelper serviceBusHelper;
        protected readonly MainForm mainForm;
        protected readonly WriteToLogDelegate writeToLog;
        protected readonly Func<Task> stopLog;
        protected readonly Action startLog;
        #endregion

        #region Public Constructors
        public TestControlBase(MainForm mainForm,
                                WriteToLogDelegate writeToLog,
                                Func<Task> stopLog,
                                Action startLog,
                                ServiceBusHelper serviceBusHelper)
        {
            this.mainForm = mainForm;
            this.writeToLog = writeToLog;
            this.stopLog = stopLog;
            this.startLog = startLog;
            this.serviceBusHelper = serviceBusHelper;
        }
        #endregion

        #region Protected Methods
        protected void OnMessageTextChanged(string text)
        {
            if (isReadyToStoreMessageText)
            {
                mainForm.MessageText = text;
            }
        }
        #endregion
    }
}
