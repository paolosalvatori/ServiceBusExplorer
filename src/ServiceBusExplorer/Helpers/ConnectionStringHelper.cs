using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
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
