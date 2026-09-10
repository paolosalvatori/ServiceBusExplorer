using System.Collections.Concurrent;
using Azure.Core;
using Azure.Identity;

namespace ServiceBusExplorer.Core.Services
{
    /// <summary>
    /// Creates and caches Azure.Identity TokenCredential instances for the modern SDK path.
    /// Independent of Microsoft.ServiceBus — works on macOS/Linux.
    /// </summary>
    internal static class AadTokenCredentialFactory
    {
        private static readonly ConcurrentDictionary<string, TokenCredential> _cache =
            new ConcurrentDictionary<string, TokenCredential>(System.StringComparer.OrdinalIgnoreCase);

        public static TokenCredential Create(string? tenantId)
        {
            var key = string.IsNullOrWhiteSpace(tenantId) ? "organizations" : tenantId!.Trim();
            return _cache.GetOrAdd(key, t =>
                new InteractiveBrowserCredential(
                    new InteractiveBrowserCredentialOptions { TenantId = t }));
        }
    }
}


