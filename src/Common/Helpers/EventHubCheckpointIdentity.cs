using System;
using EventPosition = Azure.Messaging.EventHubs.Consumer.EventPosition;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public static class EventHubCheckpointIdentity
    {
        public static EventPosition ResumeAfter(Lease lease) =>
            string.IsNullOrWhiteSpace(lease?.Offset) || lease.Offset == "-1"
                ? EventPosition.Earliest : EventPosition.FromOffset(lease.Offset, false);

        // Only extract public identity fields. Never retain/log credentials from old namespace keys.
        public static string NormalizeStoredNamespace(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            if (!value.Contains("=")) return value;
            string endpoint = null, hostName = null;
            foreach (var field in value.Split(';'))
            {
                var separator = field.IndexOf('=');
                if (separator < 0) continue;
                var name = field.Substring(0, separator).Trim();
                if (name.Equals("Endpoint", StringComparison.OrdinalIgnoreCase)) endpoint = field.Substring(separator + 1).Trim();
                if (name.Equals("HostName", StringComparison.OrdinalIgnoreCase)) hostName = field.Substring(separator + 1).Trim();
            }
            if (Uri.TryCreate(endpoint, UriKind.Absolute, out var uri) &&
                (uri.Scheme == "sb" || uri.Scheme == "amqps") && !string.IsNullOrEmpty(uri.Host))
                return uri.DnsSafeHost.ToLowerInvariant();
            // IoT HostName does not identify its Event Hubs-compatible endpoint. Keep a sanitized
            // legacy identity for manual recovery, never guess a mapping to a different stream.
            if (Uri.CheckHostName(hostName ?? "") == UriHostNameType.Dns)
                return "legacy-iot:" + hostName.ToLowerInvariant();
            return null;
        }
    }
}
