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
using System;
using System.Diagnostics;
using System.Reflection;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus;

namespace ServiceBusExplorer.Helpers
{
    public static class VersionProvider
    {
        public static string GetExeVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();

            return GetFormattedFileVersion(assembly);
        }

        public static string GetServiceBusClientVersion()
        {
            var assembly = Assembly.GetAssembly(typeof(NamespaceManager));

            return GetFormattedFileVersion(assembly);
        }

        public static bool IsLatestVersion(out ReleaseInfo nextReleaseInfo, WriteToLogDelegate writeToLog = null)
        {
            nextReleaseInfo = GitHubReleaseProvider.GetServiceBusClientLatestVersion(writeToLog).GetAwaiter().GetResult();

            var currentVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            var currentVersion = new Version(currentVersionInfo.FileMajorPart, currentVersionInfo.FileMinorPart, currentVersionInfo.FileBuildPart);

            return currentVersion.CompareTo(nextReleaseInfo.Version) >= 0;
        }

        static string GetFormattedFileVersion(Assembly assembly)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return "Version: " + 
                $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}";
        }
    }
}
