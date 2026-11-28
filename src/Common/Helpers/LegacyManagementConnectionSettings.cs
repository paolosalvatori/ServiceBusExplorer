using System;
using System.Collections.Generic;

namespace ServiceBusExplorer.Helpers
{
    internal static class LegacyManagementConnectionSettings
    {
        // NamespaceManager's WindowsAzure.ServiceBus 7.0.1 connection parser accepts
        // these keys. Validate before publication, without constructing that manager.
        private static readonly HashSet<string> SupportedKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "OperationTimeout", "Endpoint", "EntityPath", "Publisher", "RuntimePort", "ManagementPort",
            "StsEndpoint", "ViaEndpoint", "WindowsDomain", "WindowsUsername", "WindowsPassword",
            "OAuthDomain", "OAuthUsername", "OAuthPassword", "SharedSecretIssuer", "SharedSecretValue",
            "SharedAccessKeyName", "SharedAccessKey", "SharedAccessSignature", "TransportType",
            "EnableAmqpLinkRedirect", "AmqpSecurityScheme", "Authentication", "Audience", "HostName"
        };

        internal static void Validate(string connectionString)
        {
            foreach (var part in connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (string.IsNullOrWhiteSpace(part)) continue;
                var separator = part.IndexOf('=');
                if (separator < 1 || !SupportedKeys.Contains(part.Substring(0, separator).Trim()))
                    // Do not echo keys or values: an invalid setting can contain credentials.
                    throw new ArgumentException("Unsupported legacy management connection string setting.", nameof(connectionString));
            }
        }
    }
}
