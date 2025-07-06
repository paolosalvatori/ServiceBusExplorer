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
using Common;
using Common.Contracts;
using Microsoft.Azure.Amqp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ServiceBusExplorer.Forms;
using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer
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
            ServicePointManager.DefaultConnectionLimit = 200;

            var builder = Host.CreateApplicationBuilder(args);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var appSettings = config.GetSection("AppSettings").Get<AppSettings>();
            var cliSettings = CommandLineOptions.Parse(args);

            builder.Services.AddSingleton(appSettings);
            builder.Services.AddSingleton(cliSettings);
            builder.Services.AddSingleton<MainForm>();
            builder.Services.AddSingleton<Application>();
            builder.Services.AddSingleton<IServiceBusService, ServiceBusService>();

            using IHost host = builder.Build();

            try
            {
                var mainForm = host.Services.GetRequiredService<MainForm>();
                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                MessageBox.Show(text: $"A fatal exception occurred. ServiceBusExplorer will now shut down. \n\n{e.Message}.",
                    caption: "ServiceBusExplorer",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Stop);
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
                var message = ex.Message;

                if (ex.GetType() == typeof(TypeInitializationException))
                {
                    message += Environment.NewLine + Environment.NewLine +
                        "This may be due to an invalid configuration file.";
                }

                MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, message));

                if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                {
                    MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
                }
            }
        }
        #endregion
    }
}
