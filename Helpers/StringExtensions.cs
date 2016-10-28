#region Using Directives
using System.Text;
using System.IO;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class StringExtensions
    {
        public static MemoryStream ToMemoryStream(this string source)
        {
            return source.ToMemoryStream(Encoding.UTF8);
        }

        public static MemoryStream ToMemoryStream(this string source, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(source));
        }
    }
}
