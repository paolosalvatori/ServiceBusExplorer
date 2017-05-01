using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    static class VersionProvider
    {
        public static string GetVersion()
        {
            var versionInfo = GetFileVersionInfo();

            return $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}";
        }

        public static string GetVersionWithSha()
        {
            return GetFileVersionInfo().ProductVersion;
        }

        private static FileVersionInfo GetFileVersionInfo()
        {
            var asm = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(asm.Location);
            return versionInfo;
        }
    }
}