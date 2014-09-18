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
using System.Globalization;
using System.Windows.Forms;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    static class Program
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        #endregion

        #region Static Main Method
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            try
            {
                string argument = null;
                string value = null; 
                if (args != null && args.Length >= 2)
                {
                    for (var i = 0; i < args.Length; i++)
                    {
                        if ((string.Compare(args[i], "/n", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                            string.Compare(args[i], "-n", StringComparison.InvariantCultureIgnoreCase) == 0) &&
                            args.Length > i + 1 &&
                            !string.IsNullOrWhiteSpace(args[i + 1]))
                        {
                            argument = args[i];
                            value = args[i + 1];
                        }
                        if ((string.Compare(args[i], "/c", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                            string.Compare(args[i], "-c", StringComparison.InvariantCultureIgnoreCase) == 0) &&
                            args.Length > i + 1 &&
                            !string.IsNullOrWhiteSpace(args[i + 1]))
                        {
                            argument = args[i];
                            value = args[i + 1];
                        }
                        if ((string.Compare(args[i], "/q", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                            string.Compare(args[i], "-q", StringComparison.InvariantCultureIgnoreCase) == 0) &&
                            args.Length > i + 1 &&
                            !string.IsNullOrWhiteSpace(args[i + 1]))
                        {
                            FilterExpressionHelper.QueueFilterExpression = args[i + 1];
                        }
                        if ((string.Compare(args[i], "/t", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                            string.Compare(args[i], "-t", StringComparison.InvariantCultureIgnoreCase) == 0) &&
                            args.Length > i + 1 &&
                            !string.IsNullOrWhiteSpace(args[i + 1]))
                        {
                            FilterExpressionHelper.TopicFilterExpression = args[i + 1];
                        }
                        if ((string.Compare(args[i], "/s", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                            string.Compare(args[i], "-s", StringComparison.InvariantCultureIgnoreCase) == 0) &&
                            args.Length > i + 1 &&
                            !string.IsNullOrWhiteSpace(args[i + 1]))
                        {
                            FilterExpressionHelper.SubscriptionFilterExpression = args[i + 1];
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(argument) &&
                    !string.IsNullOrWhiteSpace(value))
                {
                    Application.Run(new MainForm(argument, value));
                }
                else
                {
                    Application.Run(new MainForm());
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e != null &&
                e.Exception != null)
            {
                HandleException(e.Exception);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e != null &&
                e.ExceptionObject != null)
            {
                HandleException(e.ExceptionObject as Exception);
            }
        }

        static void HandleException(Exception ex)
        {
            if (ex != null && !string.IsNullOrWhiteSpace(ex.Message))
            {
                MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
                if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
                }
            }
        }
        #endregion
    }
}
