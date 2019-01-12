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

        static string GetFormattedFileVersion(Assembly assembly)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return "Version: " + 
                $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}";
        }
    }
}
