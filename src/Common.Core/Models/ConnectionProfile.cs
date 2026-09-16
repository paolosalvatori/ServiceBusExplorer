using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ServiceBusExplorer.Core.Models
{
    /// <summary>
    /// Portable, SDK-agnostic representation of a Service Bus connection.
    /// This is the cross-platform counterpart to the legacy ServiceBusNamespace class
    /// in Common — with no references to Microsoft.ServiceBus or Windows-only APIs.
    ///
    /// Used by both the Avalonia host and (going forward) the legacy WinForms host via adapters.
    /// </summary>
    public sealed class ConnectionProfile
    {
        // ── Identity ──────────────────────────────────────────────────────────────────────

        /// <summary>
        /// User-assigned display name used in the saved connections dropdown.
        /// When null the <see cref="Namespace"/> is used as the display name.
        /// </summary>
        public string? Name { get; set; }

        // ── Authentication ─────────────────────────────────────────────────────────────────

        public ConnectionAuthMode AuthMode { get; set; } = ConnectionAuthMode.Sas;

        // ── Endpoint ───────────────────────────────────────────────────────────────────────

        /// <summary>Full endpoint URI, e.g. sb://mynamespace.servicebus.windows.net/</summary>
        public string? Endpoint { get; set; }

        // ── SAS fields ─────────────────────────────────────────────────────────────────────

        /// <summary>Full SAS connection string. Takes precedence over individual fields.</summary>
        public string? ConnectionString { get; set; }

        public string? SharedAccessKeyName { get; set; }
        public string? SharedAccessKey     { get; set; }

        // ── AAD fields ─────────────────────────────────────────────────────────────────────

        /// <summary>Optional Azure AD tenant ID. Null means the organizations endpoint.</summary>
        public string? TenantId { get; set; }

        // ── On-premises Windows fields (compatibility mode) ─────────────────────────────────

        public string? StsEndpoint      { get; set; }
        public string? RuntimePort      { get; set; }
        public string? ManagementPort   { get; set; }
        public string? WindowsDomain    { get; set; }
        public string? WindowsUserName  { get; set; }
        public string? WindowsPassword  { get; set; }

        // ── Common ────────────────────────────────────────────────────────────────────────

        public string?                 EntityPath     { get; set; }
        public ConnectionTransportType TransportType  { get; set; } = ConnectionTransportType.AmqpTcp;

        /// <summary>Entity types to load on connect (e.g. Queues, Topics, Event Hubs).</summary>
        public List<string> SelectedEntities { get; set; } = new List<string>
        {
            EntityTypeConstants.Queues,
            EntityTypeConstants.Topics
        };

        // ── Filter expressions ────────────────────────────────────────────────────────────

        public string? QueueFilter        { get; set; }
        public string? TopicFilter        { get; set; }
        public string? SubscriptionFilter { get; set; }

        // ── Metadata ─────────────────────────────────────────────────────────────────────

        /// <summary>True when the user explicitly created / saved this profile.</summary>
        public bool UserCreated { get; set; }

        // ── Computed ─────────────────────────────────────────────────────────────────────

        /// <summary>Short namespace name, e.g. "mynamespace" derived from the endpoint host.</summary>
        [JsonIgnore]
        public string? Namespace
        {
            get
            {
                var ep = EffectiveEndpoint;
                if (string.IsNullOrWhiteSpace(ep)) return null;
                try
                {
                    var host = new Uri(ep).Host;
                    var dot  = host.IndexOf('.');
                    return dot > 0 ? host.Substring(0, dot) : host;
                }
                catch (UriFormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>Fully-qualified namespace host, e.g. "mynamespace.servicebus.windows.net".</summary>
        [JsonIgnore]
        public string? FullyQualifiedNamespace
        {
            get
            {
                var ep = EffectiveEndpoint;
                if (string.IsNullOrWhiteSpace(ep)) return null;
                try { return new Uri(ep).Host; }
                catch (UriFormatException) { return null; }
            }
        }

        [JsonIgnore]
        public bool IsAad => AuthMode == ConnectionAuthMode.AzureActiveDirectory;

        [JsonIgnore]
        public string DisplayName => !string.IsNullOrWhiteSpace(Name) ? Name! : Namespace ?? "(unknown)";

        // ── Helpers ───────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Returns the endpoint URI. For SAS profiles without an explicit Endpoint,
        /// the endpoint is extracted from the ConnectionString.
        /// </summary>
        [JsonIgnore]
        public string? EffectiveEndpoint
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Endpoint)) return Endpoint;

                // Try to extract from ConnectionString
                if (string.IsNullOrWhiteSpace(ConnectionString)) return null;
                foreach (var part in ConnectionString!.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var eq = part.IndexOf('=');
                    if (eq < 0) continue;
                    var key = part.Substring(0, eq).Trim();
                    if (string.Equals(key, "Endpoint", StringComparison.OrdinalIgnoreCase))
                        return part.Substring(eq + 1).Trim();
                }
                return null;
            }
        }

        /// <summary>
        /// Builds the canonical AAD metadata-only connection string for persistence.
        /// No secrets are stored.
        /// </summary>
        public string BuildAadConnectionString()
        {
            var hasTenant = !string.IsNullOrWhiteSpace(TenantId);
            var hasEntity = !string.IsNullOrWhiteSpace(EntityPath);
            var transport = TransportType == ConnectionTransportType.AmqpWebSockets
                ? "AmqpWebSockets" : "Amqp";

            if (hasEntity && hasTenant)
                return $"Endpoint={Endpoint};AuthMode=AAD;TenantId={TenantId};TransportType={transport};EntityPath={EntityPath}";
            if (hasEntity)
                return $"Endpoint={Endpoint};AuthMode=AAD;TransportType={transport};EntityPath={EntityPath}";
            if (hasTenant)
                return $"Endpoint={Endpoint};AuthMode=AAD;TenantId={TenantId};TransportType={transport}";

            return $"Endpoint={Endpoint};AuthMode=AAD;TransportType={transport}";
        }
    }
}

