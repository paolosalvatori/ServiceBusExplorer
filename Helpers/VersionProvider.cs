using System.Diagnostics;
using System.Reflection;

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Helpers
{
    static class VersionProvider
    {
        public static string GetVersion()
        {
            var asm = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(asm.Location);
            return $"{versionInfo.ProductMajorPart}.{versionInfo.ProductMinorPart}.{versionInfo.ProductBuildPart}";
        }
    }
}