using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.ServiceBus;

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    static class VersionProvider
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

        public static bool IsLatestVersion()
        {
            var nextReleaseInfo = GitHubReleaseProvider.GetServiceBusClientLatestVersion().GetAwaiter().GetResult();

            var currentVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            var currentVersion = new Version(currentVersionInfo.FileMajorPart, currentVersionInfo.FileMinorPart, currentVersionInfo.FileBuildPart);

            return nextReleaseInfo.Version.CompareTo(currentVersion) > 0;
        }

        static string GetFormattedFileVersion(Assembly assembly)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return "Version: " + 
                $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}";
        }
    }
}
