#region Using Directives

using System.Text.RegularExpressions;

#endregion

namespace ServiceBusExplorer.Utilities.Helpers
{
    public static class ConnectionStringHelper
    {
        private readonly static Regex EntityPathRegex = new Regex(@"(?:^|[^\w])(EntityPath=[^;]+)");

        public static bool IsEntitySpecific(string connectionString)
        {
            return EntityPathRegex.IsMatch(connectionString);
        }
    }
}
