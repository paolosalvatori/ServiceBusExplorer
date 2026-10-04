using System;
using System.Collections.Generic;
using ServiceBusExplorer.Core.Models;

namespace ServiceBusExplorer.Core.Services
{
    /// <summary>
    /// Parses Service Bus connection strings into portable <see cref="ConnectionProfile"/> objects.
    /// No dependency on Microsoft.ServiceBus or any Windows-specific APIs.
    /// Supports SAS, AAD, and on-premises Windows connection string formats.
    /// </summary>
    public static class ConnectionStringParser
    {
        // ── Known connection-string keys (lowercase) ───────────────────────────────────────

        private const string KeyEndpoint          = "endpoint";
        private const string KeySasKeyName        = "sharedaccesskeyname";
        private const string KeySasKey            = "sharedaccesskey";
        private const string KeyStsEndpoint       = "stsendpoint";
        private const string KeyRuntimePort       = "runtimeport";
        private const string KeyManagementPort    = "managementport";
        private const string KeyWindowsUser       = "windowsusername";
        private const string KeyWindowsDomain     = "windowsdomain";
        private const string KeyWindowsPassword   = "windowspassword";
        private const string KeyTransportType     = "transporttype";
        private const string KeyEntityPath        = "entitypath";
        private const string KeyAuthMode          = "authmode";
        private const string KeyTenantId          = "tenantid";

        // ── Public API ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Attempts to parse a raw connection string.
        /// Returns null if the format is not recognised.
        /// </summary>
        public static ConnectionProfile? TryParse(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;

            var parts = ParseParts(raw!);
            if (parts.Count == 0) return null;

            // ── AAD ───────────────────────────────────────────────────────────────────────
            if (parts.TryGetValue(KeyAuthMode, out var authModeValue) &&
                string.Equals(authModeValue, "aad", StringComparison.OrdinalIgnoreCase))
            {
                return ParseAad(parts);
            }

            // ── SAS ───────────────────────────────────────────────────────────────────────
            if (parts.ContainsKey(KeyEndpoint) &&
                parts.ContainsKey(KeySasKeyName) &&
                parts.ContainsKey(KeySasKey))
            {
                return ParseSas(raw!, parts);
            }

            // ── On-premises Windows ───────────────────────────────────────────────────────
            if (parts.ContainsKey(KeyRuntimePort) ||
                parts.ContainsKey(KeyManagementPort) ||
                parts.ContainsKey(KeyWindowsUser))
            {
                return ParseWindows(raw!, parts);
            }

            return null;
        }

        /// <summary>
        /// Parses a connection string. Throws <see cref="FormatException"/> if not recognised.
        /// </summary>
        public static ConnectionProfile Parse(string raw)
        {
            var result = TryParse(raw);
            if (result == null)
                throw new FormatException("The connection string is not in a recognised format.");
            return result;
        }

        // ── Parsers ────────────────────────────────────────────────────────────────────────

        private static ConnectionProfile ParseSas(string raw, Dictionary<string, string> p)
        {
            var endpoint = NormaliseEndpoint(p.GetValueOrDefault(KeyEndpoint));

            return new ConnectionProfile
            {
                AuthMode         = ConnectionAuthMode.Sas,
                ConnectionString = raw,
                Endpoint         = endpoint,
                SharedAccessKeyName = p.GetValueOrDefault(KeySasKeyName),
                SharedAccessKey     = p.GetValueOrDefault(KeySasKey),
                EntityPath       = p.GetValueOrDefault(KeyEntityPath),
                TransportType    = ParseTransportType(p.GetValueOrDefault(KeyTransportType)),
                UserCreated      = true
            };
        }

        private static ConnectionProfile ParseAad(Dictionary<string, string> p)
        {
            var endpoint = NormaliseEndpoint(p.GetValueOrDefault(KeyEndpoint));

            return new ConnectionProfile
            {
                AuthMode      = ConnectionAuthMode.AzureActiveDirectory,
                Endpoint      = endpoint,
                TenantId      = p.GetValueOrDefault(KeyTenantId),
                EntityPath    = p.GetValueOrDefault(KeyEntityPath),
                TransportType = ParseTransportType(p.GetValueOrDefault(KeyTransportType)),
                UserCreated   = true
            };
        }

        private static ConnectionProfile ParseWindows(string raw, Dictionary<string, string> p)
        {
            var endpoint = NormaliseEndpoint(p.GetValueOrDefault(KeyEndpoint));

            return new ConnectionProfile
            {
                AuthMode       = ConnectionAuthMode.Windows,
                ConnectionString = raw,
                Endpoint       = endpoint,
                StsEndpoint    = p.GetValueOrDefault(KeyStsEndpoint),
                RuntimePort    = p.GetValueOrDefault(KeyRuntimePort),
                ManagementPort = p.GetValueOrDefault(KeyManagementPort),
                WindowsUserName= p.GetValueOrDefault(KeyWindowsUser),
                WindowsDomain  = p.GetValueOrDefault(KeyWindowsDomain),
                WindowsPassword= p.GetValueOrDefault(KeyWindowsPassword),
                TransportType  = ParseTransportType(p.GetValueOrDefault(KeyTransportType)),
                UserCreated    = true
            };
        }

        // ── Helpers ────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Splits "Key=Value;Key=Value" into a case-insensitive dictionary.
        /// Values that themselves contain '=' (e.g. base-64 SAS keys) are handled correctly.
        /// </summary>
        private static Dictionary<string, string> ParseParts(string raw)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var segment in raw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var eq = segment.IndexOf('=');
                if (eq <= 0) continue;

                var key   = segment.Substring(0, eq).Trim().ToLowerInvariant();
                var value = segment.Substring(eq + 1).Trim();
                result[key] = value;
            }
            return result;
        }

        private static string? NormaliseEndpoint(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return raw;
            // Ensure sb:// scheme for consistency
            if (!raw!.Contains("://")) return "sb://" + raw;
            return raw;
        }

        private static ConnectionTransportType ParseTransportType(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return ConnectionTransportType.AmqpTcp;

            if (raw!.Equals("amqpwebsockets", StringComparison.OrdinalIgnoreCase) ||
                raw.Equals("amqp_websockets", StringComparison.OrdinalIgnoreCase))
                return ConnectionTransportType.AmqpWebSockets;

            return ConnectionTransportType.AmqpTcp;
        }
    }

    // ── Dictionary extension for netstandard2.0 ────────────────────────────────────────────

    internal static class DictionaryExtensions
    {
        internal static TValue? GetValueOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dict, TKey key)
            where TKey : notnull
        {
            dict.TryGetValue(key, out var val);
            return val;
        }
    }
}

