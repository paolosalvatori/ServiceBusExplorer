#region Using Directives

using System.IO;

#endregion


namespace ServiceBusExplorer.Utilities.Helpers
{
    public static class PathHelper
    {
        public static string GetNumberedFileName(string fileName, int count)
        {
            var directory = Path.GetDirectoryName(fileName);
            var baseName = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            var fileNameWithCount = Path.Combine(directory ?? string.Empty, $"{baseName}({count}){extension}");

            return fileNameWithCount;
        }
    }
}
