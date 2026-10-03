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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public static Version GetCurrentVersion()
        {
            var v = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return new Version(v.FileMajorPart, v.FileMinorPart, v.FileBuildPart);
        }

        public static Version GetKnownReleaseVersion(WriteToLogDelegate writeToLog = null)
        {
            return GetKnownReleaseVersion(Assembly.GetExecutingAssembly()
                .GetCustomAttributes<AssemblyMetadataAttribute>(), writeToLog);
        }

        static Version GetKnownReleaseVersion(IEnumerable<AssemblyMetadataAttribute> metadata,
            WriteToLogDelegate writeToLog)
        {
            var value = metadata.FirstOrDefault(attribute => attribute.Key == "UpstreamReleaseVersion")?.Value;
            if (Version.TryParse(value, out var version) && version.Build >= 0 && version.Revision < 0)
            {
                return version;
            }

            const string message = "VersionProvider::Upstream release baseline is unavailable or invalid; newer upstream releases cannot be compared.";
            if (writeToLog != null)
            {
                writeToLog(message);
            }
            else
            {
                Console.WriteLine(message);
            }

            return null;
        }

        // Unstamped local builds keep the default 1.0.x file version
        public static bool IsUnstampedBuild()
        {
            return GetCurrentVersion() < new Version(2, 0, 0);
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
