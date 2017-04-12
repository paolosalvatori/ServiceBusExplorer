using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    static class VersionProvider
    {
        public static string GetVersion()
        {
            var asm = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(asm.Location);
            return $"{versionInfo.FileMajorPart}.{versionInfo.FileMinorPart}.{versionInfo.FileBuildPart}";
        }
    }
}