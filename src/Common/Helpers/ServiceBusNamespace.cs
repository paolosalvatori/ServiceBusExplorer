#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Text.RegularExpressions;

using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using ServiceBusExplorer.Utilities.Helpers;

#endregion

namespace ServiceBusExplorer.Helpers
{
    public enum ServiceBusNamespaceType
    {
        Custom,
        Cloud,
        OnPremises
    }

    /// <summary>
    /// Identifies the authentication mechanism used to connect to a Service Bus namespace.
    /// </summary>
    public enum ServiceBusAuthMode
    {
        /// <summary>Shared Access Signature (connection-string based)</summary>
        Sas,
        /// <summary>On-premises Windows credentials</summary>
        Windows,
        /// <summary>Microsoft Entra ID interactive browser sign-in</summary>
        Entra
    }

    /// <summary>
    /// This class represents a service bus namespace address and authentication credentials
    /// </summary>
    public class ServiceBusNamespace
    {
        #region Private Constants
        //***************************
        // Constants for accessing configuration files
        //***************************
        const string ServiceBusNamespaces = "serviceBusNamespaces";

        //***************************
        // Messages
        //***************************
        const string ServiceBusNamespacesNotConfigured = "Service bus accounts have not been properly configured in the configuration file.";
        const string ServiceBusNamespaceIsNullOrEmpty = "The connection string for service bus entry {0} is null or empty.";
        const string ServiceBusNamespaceIsWrong = "The connection string for service bus namespace {0} is in the wrong format.";
        const string ServiceBusNamespaceEndpointIsNullOrEmpty = "The endpoint for the service bus namespace {0} is null or empty.";
        const string ServiceBusNamespaceEndpointPrefixedWithSb = "The endpoint for the service bus namespace {0} is being automatically prefixed with \"sb://\".";
        const string ServiceBusNamespaceStsEndpointIsNullOrEmpty = "The sts endpoint for the service bus namespace {0} is null or empty.";
        const string ServiceBusNamespaceRuntimePortIsNullOrEmpty = "The runtime port for the service bus namespace {0} is null or empty.";
        const string ServiceBusNamespaceManagementPortIsNullOrEmpty = "The management port for the service bus namespace {0} is null or empty.";
        const string ServiceBusNamespaceEndpointUriIsInvalid = "The endpoint URI for the service bus namespace {0} is invalid.";
        const string ServiceBusNamespaceSharedAccessKeyNameIsInvalid = "The SharedAccessKeyName for the service bus namespace {0} is invalid.";
        const string ServiceBusNamespaceSharedAccessKeyIsInvalid = "The SharedAccessKey for the service bus namespace {0} is invalid.";

        //***************************
        // Parameters
        //***************************
        const string ConnectionStringEndpoint = "endpoint";
        const string ConnectionStringSharedAccessKeyName = "sharedaccesskeyname";
        const string ConnectionStringSharedAccessKey = "sharedaccesskey";
        const string ConnectionStringStsEndpoint = "stsendpoint";
        const string ConnectionStringRuntimePort = "runtimeport";
        const string ConnectionStringManagementPort = "managementport";
        const string ConnectionStringWindowsUsername = "windowsusername";
        const string ConnectionStringWindowsDomain = "windowsdomain";
        const string ConnectionStringWindowsPassword = "windowspassword";
        const string ConnectionStringTransportType = "transporttype";
        const string ConnectionStringEntityPath = "entitypath";
        const string ConnectionStringAuthMode = "authmode";
        const string ConnectionStringTenantId = "tenantid";
        const string ConnectionStringUseDevelopmentEmulator = "usedevelopmentemulator";
        // Wire value, not display text. Saved namespaces persist "AuthMode=AAD", so this string and
        // the AuthMode=AAD in the EntraConnectionString*Format constants must not be renamed to
        // "entra" or every existing saved Entra connection stops being recognised.
        const string AuthModeEntra = "aad";

        #endregion

        #region Public Constants
        //***************************
        // Formats
        //***************************
        public const string SasConnectionStringFormat = "Endpoint={0};SharedAccessKeyName={1};SharedAccessKey={2};TransportType={3}";
        public const string SasConnectionStringEntityPathFormat = "Endpoint={0};SharedAccessKeyName={1};SharedAccessKey={2};TransportType={3};EntityPath={4}";
        public const string EntraConnectionStringFormat = "Endpoint={0};AuthMode=AAD;TransportType={1}";
        public const string EntraConnectionStringTenantFormat = "Endpoint={0};AuthMode=AAD;TenantId={1};TransportType={2}";
        public const string EntraConnectionStringEntityPathFormat = "Endpoint={0};AuthMode=AAD;TenantId={1};TransportType={2};EntityPath={3}";
        public const string EntraConnectionStringEntityPathNoTenantFormat = "Endpoint={0};AuthMode=AAD;TransportType={1};EntityPath={2}";
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the ServiceBusHelper class.
        /// </summary>
        public ServiceBusNamespace()
        {
            ConnectionStringType = ServiceBusNamespaceType.Cloud;
            AuthMode = ServiceBusAuthMode.Sas;
            ConnectionString = default(string);
            Uri = default(string);
            Namespace = default(string);
            ServicePath = default(string);
            StsEndpoint = default(string);
            RuntimePort = default(string);
            ManagementPort = default(string);
            WindowsDomain = default(string);
            WindowsUserName = default(string);
            WindowsPassword = default(string);
        }

        /// <summary>
        /// Initializes a new instance of the ServiceBusNamespace class.
        /// </summary>
        /// <param name="connectionStringType">The service bus namespace connection string type.</param>
        /// <param name="connectionString">The service bus namespace connection string.</param>
        /// <param name="uri">The full address of the service bus namespace.</param>
        /// <param name="ns">The service bus namespace.</param>
        /// <param name="name">The issuer name of the shared secret credentials.</param>
        /// <param name="key">The issuer secret of the shared secret credentials.</param>
        /// <param name="servicePath">The service path that follows the host name section of the URI.</param>
        /// <param name="stsEndpoint">The sts endpoint of the service bus namespace.</param>
        /// <param name="transportType">The transport type to use to access the namespace.</param>
        /// <param name="isSas">True is is SAS connection string, false otherwise.</param>
        /// <param name="entityPath">Entity path connection string scoped to. Otherwise a default.</param>
        public ServiceBusNamespace(ServiceBusNamespaceType connectionStringType,
                                   string connectionString,
                                   string uri,
                                   string ns,
                                   string servicePath,
                                   string name,
                                   string key,
                                   string stsEndpoint,
                                   TransportType transportType,
                                   bool isSas = false,
                                   string entityPath = "",
                                   bool isUserCreated = false)
        {
            ConnectionStringType = connectionStringType;
            AuthMode = ServiceBusAuthMode.Sas;
            Uri = string.IsNullOrWhiteSpace(uri) ?
                  ServiceBusEnvironment.CreateServiceUri("sb", ns, servicePath).ToString() :
                  uri;

            ConnectionString = connectionString;
            Namespace = ns;

            if (isSas)
            {
                SharedAccessKeyName = name;
                SharedAccessKey = key;
            }
            else
            {
                ServicePath = servicePath;
            }

            TransportType = transportType;
            StsEndpoint = stsEndpoint;
            RuntimePort = default(string);
            ManagementPort = default(string);
            WindowsDomain = default(string);
            WindowsUserName = default(string);
            WindowsPassword = default(string);
            EntityPath = entityPath;
            UserCreated = isUserCreated;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceBusNamespace class for an on-premises namespace.
        /// </summary>
        /// <param name="connectionString">The service bus namespace connection string.</param>
        /// <param name="endpoint">The endpoint of the service bus namespace.</param>
        /// <param name="stsEndpoint">The sts endpoint of the service bus namespace.</param>
        /// <param name="runtimePort">The runtime port.</param>
        /// <param name="managementPort">The management port.</param>
        /// <param name="windowsDomain">The Windows domain or machine name.</param>
        /// <param name="windowsUsername">The Windows user name.</param>
        /// <param name="windowsPassword">The Windows user password.</param>
        /// <param name="ns">The service bus namespace.</param>
        /// <param name="transportType">The transport type to use to access the namespace.</param>
        public ServiceBusNamespace(string connectionString,
                                   string endpoint,
                                   string stsEndpoint,
                                   string runtimePort,
                                   string managementPort,
                                   string windowsDomain,
                                   string windowsUsername,
                                   string windowsPassword,
                                   string ns,
                                   TransportType transportType,
                                   bool isUserCreated = false)
        {
            ConnectionStringType = ServiceBusNamespaceType.OnPremises;
            AuthMode = ServiceBusAuthMode.Windows;
            ConnectionString = connectionString;
            Uri = endpoint;
            var uri = new Uri(endpoint);


            if (string.IsNullOrWhiteSpace(endpoint))
            {
                Uri = ServiceBusEnvironment.CreateServiceUri(uri.Scheme, ns, null).ToString();
            }

            Namespace = ns;
            var settings = new MessagingFactorySettings();
            TransportType = settings.TransportType;
            StsEndpoint = stsEndpoint;
            RuntimePort = runtimePort;
            ManagementPort = managementPort;
            WindowsDomain = windowsDomain;
            WindowsUserName = windowsUsername;
            WindowsPassword = windowsPassword;
            TransportType = transportType;
            UserCreated = isUserCreated;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceBusNamespace class for Entra authentication.
        /// </summary>
        public ServiceBusNamespace(string endpoint,
                                   string ns,
                                   string tenantId,
                                   TransportType transportType,
                                   string entityPath = "",
                                   bool isUserCreated = false)
        {
            ConnectionStringType = ServiceBusNamespaceType.Cloud;
            AuthMode = ServiceBusAuthMode.Entra;
            Uri = endpoint;
            Namespace = ns;
            TenantId = tenantId;
            TransportType = transportType;
            EntityPath = entityPath;
            UserCreated = isUserCreated;

            // No SAS keys or connection string for Entra
            ConnectionString = null;
            SharedAccessKeyName = null;
            SharedAccessKey = null;
            StsEndpoint = null;
            RuntimePort = null;
            ManagementPort = null;
            WindowsDomain = null;
            WindowsUserName = null;
            WindowsPassword = null;
        }
        #endregion

        #region Public methods
        public static ServiceBusNamespace GetServiceBusNamespace(string key, string connectionString,
            WriteToLogDelegate staticWriteToLog)
        {

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIsNullOrEmpty, key));
                return null;
            }

            var isUserCreated = IsUserCreatedKey(key);
            var toLower = connectionString.ToLower();

            var parameters = connectionString.Split([';'], StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s.IndexOf('=') > 0)
                .GroupBy(s => s.Substring(0, s.IndexOf('=')).Trim().ToLowerInvariant())
                .ToDictionary(
                    g => g.Key, 
                    g => g.Last().Substring(g.Last().IndexOf('=') + 1).Trim());


            if (toLower.Contains(ConnectionStringEndpoint) &&
                toLower.Contains(ConnectionStringSharedAccessKeyName) &&
                toLower.Contains(ConnectionStringSharedAccessKey))
            {
                return GetServiceBusNamespaceUsingSAS(key, connectionString, staticWriteToLog, 
                    isUserCreated, parameters);
            }

            if (toLower.Contains(ConnectionStringEndpoint) &&
                toLower.Contains(ConnectionStringAuthMode) &&
                parameters.ContainsKey(ConnectionStringAuthMode) &&
                string.Equals(parameters[ConnectionStringAuthMode], AuthModeEntra, StringComparison.OrdinalIgnoreCase))
            {
                return GetServiceBusNamespaceUsingEntra(key, connectionString, staticWriteToLog,
                    isUserCreated, parameters);
            }

            if (toLower.Contains(ConnectionStringRuntimePort) ||
                toLower.Contains(ConnectionStringManagementPort) ||
                toLower.Contains(ConnectionStringWindowsUsername) ||
                toLower.Contains(ConnectionStringWindowsDomain) ||
                toLower.Contains(ConnectionStringWindowsPassword))
            {
                return GetServiceBusNamespaceUsingWindows(key, connectionString, staticWriteToLog, 
                    isUserCreated, toLower, parameters);
            }

            return null;
        }

        public static bool IsUserCreatedKey(string key) =>
            key != "CustomConnectionString" && key != "SASConnectionString";

        public static ServiceBusConnectionStringBuilder CreateLegacyConnectionStringBuilder(string connectionString)
        {
            // The legacy SDK reads the transport enum case-sensitively and includes a terminal
            // delimiter in the preceding value. Adapt its input without changing the saved profile.
            var parts = connectionString.Split(';');
            var last = Array.FindLastIndex(parts, part => !string.IsNullOrWhiteSpace(part));
            var count = parts.Length;
            for (var i = 0; i <= last; i++)
            {
                var separator = parts[i].IndexOf('=');
                if (separator <= 0 || !string.Equals(parts[i].Substring(0, separator).Trim(),
                        ConnectionStringTransportType, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                var value = parts[i].Substring(separator + 1).Trim();
                if (!Enum.TryParse<TransportType>(value, true, out var transport) ||
                    !Enum.IsDefined(typeof(TransportType), transport))
                {
                    throw new ArgumentException("The Service Bus transport type is invalid.");
                }
                parts[i] = "TransportType=" + transport;
                if (i == last)
                {
                    count = last + 1;
                }
            }
            return new ServiceBusConnectionStringBuilder(string.Join(";", parts, 0, count));
        }

        public static Dictionary<string, ServiceBusNamespace> GetMessagingNamespaces
            (TwoFilesConfiguration configuration, WriteToLogDelegate writeToLog) =>
            GetMessagingNamespaces(configuration, writeToLog, null);

        public static Dictionary<string, ServiceBusNamespace> GetMessagingNamespaces(
            TwoFilesConfiguration configuration, WriteToLogDelegate writeToLog, string environmentConnectionString)
        {
            // Read metadata from the same file as each credential. A user entry may shadow an application entry.
            var settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var hashtable = configuration.GetHashtableFromSection(ServiceBusNamespaces, (file, key) =>
                settings[key] = file.AppSettings.Settings[Common.Helpers.ConfigurationHelper.NamespaceServiceSettingPrefix + key]?.Value);

            var serviceBusNamespaces = new Dictionary<string, ServiceBusNamespace>();

            foreach (string key in hashtable?.Keys.Cast<string>() ?? Enumerable.Empty<string>())
            {
                var profile = GetServiceBusNamespace(key, (string)hashtable[key], writeToLog);
                if (profile == null) continue;
                if (!profile.IsEntra) profile.Selection = NamespaceSelection.FromSetting(settings[key]);
                serviceBusNamespaces.Add(key, profile);
            }

            var microsoftServiceBusConnectionString =
                configuration.GetStringValue(ConfigurationParameters.MicrosoftServiceBusConnectionString, out var settingSource);

            AddExternal(ConfigurationParameters.MicrosoftServiceBusConnectionString, microsoftServiceBusConnectionString, settingSource.ToString());
            AddExternal(@"HKEY_CURRENT_USER\Environment connection string", environmentConnectionString, "Environment");
            if (serviceBusNamespaces.Count == 0) writeToLog(ServiceBusNamespacesNotConfigured);
            return serviceBusNamespaces;

            void AddExternal(string key, string value, string source)
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                var profile = GetServiceBusNamespace(key, value, writeToLog);
                if (profile == null) return;
                // A replacement credential can refer to another namespace. Keep the choice with its
                // source and endpoint, without persisting the connection string or its credentials.
                if (!System.Uri.TryCreate(profile.Uri, UriKind.Absolute, out var endpoint) ||
                    string.IsNullOrEmpty(endpoint.Host))
                {
                    writeToLog($"Skipping {key}: the namespace endpoint is invalid.");
                    return;
                }
                profile.externalSelectionKey = $"ExternalNamespaceService.{source}.{endpoint.IdnHost}:{endpoint.Port}{endpoint.AbsolutePath}";
                if (!profile.IsEntra)
                    profile.Selection = NamespaceSelection.FromSetting(configuration.GetStringValue(profile.externalSelectionKey));
                serviceBusNamespaces[key] = profile;
            }
        }

        public static void SaveConnectionString(TwoFilesConfiguration configuration,
            string key, string value, WriteToLogDelegate staticWriteToLog)
        {
            configuration.AddEntryToDictionarySection(ServiceBusNamespaces, key, value);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the service bus namespace type.
        /// </summary>
        public ServiceBusNamespaceType ConnectionStringType { get; set; }

        /// <summary>
        /// Get or set if this is a connection string added by the user
        /// </summary>
        public bool UserCreated { get; set; }

        /// <summary>
        /// Gets or sets the service bus namespace connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the service bus namespace connection string without transport type.
        /// </summary>
        public string ConnectionStringWithoutTransportType
        {
            get
            {
                if (ConnectionString == null)
                    return null;
                var regex = new Regex(@";TransportType=\w*;?");
                var connectionString = regex.Replace(ConnectionString, string.Empty);
                return connectionString;
            }
        }

        /// <summary>
        /// Gets or sets the full address of the service bus namespace.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the service bus namespace.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the service path that follows the host name section of the URI.
        /// </summary>
        public string ServicePath { get; set; }

        /// <summary>
        /// Gets or sets the transport type to use to access the namespace.
        /// </summary>
        public TransportType TransportType { get; set; }

        /// <summary>
        /// Gets or sets the URL of the sts endpoint.
        /// </summary>
        public string StsEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the runtime port.
        /// </summary>
        public string RuntimePort { get; set; }

        /// <summary>
        /// Gets or sets the management port.
        /// </summary>
        public string ManagementPort { get; set; }

        /// <summary>
        /// Gets or sets the windows domain.
        /// </summary>
        public string WindowsDomain { get; set; }

        /// <summary>
        /// Gets or sets the Windows user name.
        /// </summary>
        public string WindowsUserName { get; set; }

        /// <summary>
        /// Gets or sets the Windows user password.
        /// </summary>
        public string WindowsPassword { get; set; }

        /// <summary>
        /// Gets or sets the SharedAccessKeyName.
        /// </summary>
        public string SharedAccessKeyName { get; set; }

        /// <summary>
        /// Gets or sets the SharedAccessKey.
        /// </summary>
        public string SharedAccessKey { get; set; }

        /// <summary>
        /// Gets or sets the EntityPath
        /// </summary>
        public string EntityPath { get; set; }

        /// <summary>
        /// Gets or sets the authentication mode for this namespace entry.
        /// </summary>
        public ServiceBusAuthMode AuthMode { get; set; }

        /// <summary>
        /// Gets or sets the Entra tenant ID (optional; null means the organizations endpoint).
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets the fully qualified namespace host name (e.g. mynamespace.servicebus.windows.net).
        /// Derived from the endpoint URI.
        /// </summary>
        public string FullyQualifiedNamespace
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Uri))
                    return null;
                try
                {
                    return new Uri(Uri).Host;
                }
                catch (UriFormatException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns true when this namespace entry uses Entra authentication
        /// and has no SAS connection string.
        /// </summary>
        public bool IsEntra => AuthMode == ServiceBusAuthMode.Entra;

        /// <summary>
        /// Gets or sets a value indicating whether the connection string targets the
        /// Service Bus emulator (UseDevelopmentEmulator=true). Emulator endpoints are
        /// plain host names or IP addresses, not fully qualified Azure namespaces.
        /// </summary>
        public bool UseDevelopmentEmulator { get; set; }

        private string externalSelectionKey;
        public bool HasExternalCredentials => externalSelectionKey != null;
        public NamespaceSelection Selection { get; set; }

        public void SaveExternalSelection(TwoFilesConfiguration configuration, NamespaceSelection selection)
        {
            if (!HasExternalCredentials) throw new InvalidOperationException("This profile has no external credential source.");
            if (selection == null) throw new ArgumentNullException(nameof(selection));
            configuration.SaveStringSetting(externalSelectionKey, selection.ToSetting());
            Selection = selection;
        }

        public NamespaceSelection KnownSelection => UseDevelopmentEmulator && Selection?.Service != NamespaceService.Custom
            ? new NamespaceSelection(NamespaceService.ServiceBus)
            : Selection;

        public NamespaceService GetDiscoveryService(bool isEventHubNamespace) => IsEntra
            ? (isEventHubNamespace ? NamespaceService.EventHubs : NamespaceService.ServiceBus)
            : (KnownSelection ?? throw new InvalidOperationException("Choose a namespace service before connecting.")).Service;

        public IReadOnlyList<string> GetDiscoveryEntities(bool isEventHubNamespace, bool isCloudNamespace)
        {
            var selection = IsEntra
                ? new NamespaceSelection(GetDiscoveryService(isEventHubNamespace))
                : KnownSelection ?? throw new InvalidOperationException("Choose a namespace service before connecting.");
            // The emulator and Service Bus for Windows Server support queues and topics.
            return isCloudNamespace && !UseDevelopmentEmulator ? selection.Entities : Array.AsReadOnly(selection.Entities
                .Where(group => group == Constants.QueueEntities || group == Constants.TopicEntities).ToArray());
        }
        #endregion

        #region Private Methods

        static ServiceBusNamespace GetServiceBusNamespaceUsingWindows(string key, string connectionString,
            WriteToLogDelegate staticWriteToLog, bool isUserCreated, string toLower, 
            Dictionary<string, string> parameters)
        {
            if (!toLower.Contains(ConnectionStringEndpoint) ||
                !toLower.Contains(ConnectionStringStsEndpoint) ||
                !toLower.Contains(ConnectionStringRuntimePort) ||
                !toLower.Contains(ConnectionStringManagementPort))
            {
                return null;
            }

            var endpoint = parameters.ContainsKey(ConnectionStringEndpoint) ?
                           parameters[ConnectionStringEndpoint] :
                           null;

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, 
                    ServiceBusNamespaceEndpointIsNullOrEmpty, key));
                return null;
            }

            string ns = GetNamespaceNameFromEndpoint(endpoint, staticWriteToLog, key);  

            var stsEndpoint = parameters.ContainsKey(ConnectionStringStsEndpoint) ?
                              parameters[ConnectionStringStsEndpoint] :
                              null;

            if (string.IsNullOrWhiteSpace(stsEndpoint))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, 
                    ServiceBusNamespaceStsEndpointIsNullOrEmpty, key));
                return null;
            }

            var runtimePort = parameters.ContainsKey(ConnectionStringRuntimePort) ?
                              parameters[ConnectionStringRuntimePort] :
                              null;

            if (string.IsNullOrWhiteSpace(runtimePort))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, 
                    ServiceBusNamespaceRuntimePortIsNullOrEmpty, key));
                return null;
            }

            var managementPort = parameters.ContainsKey(ConnectionStringManagementPort) ?
                                 parameters[ConnectionStringManagementPort] :
                                 null;

            if (string.IsNullOrWhiteSpace(managementPort))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceManagementPortIsNullOrEmpty, key));
                return null;
            }

            var windowsDomain = parameters.ContainsKey(ConnectionStringWindowsDomain) ?
                                parameters[ConnectionStringWindowsDomain] :
                                null;

            var windowsUsername = parameters.ContainsKey(ConnectionStringWindowsUsername) ?
                                  parameters[ConnectionStringWindowsUsername] :
                                  null;

            var windowsPassword = parameters.ContainsKey(ConnectionStringWindowsPassword) ?
                                  parameters[ConnectionStringWindowsPassword] :
                                  null;

            var settings = new MessagingFactorySettings();
            var transportType = settings.TransportType;

            if (parameters.ContainsKey(ConnectionStringTransportType))
            {
                Enum.TryParse(parameters[ConnectionStringTransportType], true, out transportType);
            }
            
            return new ServiceBusNamespace(connectionString, endpoint, stsEndpoint, runtimePort, managementPort, windowsDomain, windowsUsername, windowsPassword, ns, transportType, isUserCreated);
        }

        static ServiceBusNamespace GetServiceBusNamespaceUsingSAS(string key, string connectionString, 
            WriteToLogDelegate staticWriteToLog, bool isUserCreated, Dictionary<string, string> parameters)
        {
            if (parameters.Count < 3)
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIsWrong, key));
                return null;
            }

            var endpoint = parameters.ContainsKey(ConnectionStringEndpoint) ?
                           parameters[ConnectionStringEndpoint] :
                           null;

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, 
                    ServiceBusNamespaceEndpointIsNullOrEmpty, key));
                return null;
            }

            if (!endpoint.Contains("://"))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, 
                    ServiceBusNamespaceEndpointPrefixedWithSb, endpoint));
                endpoint = "sb://" + endpoint;
            }

            var stsEndpoint = parameters.ContainsKey(ConnectionStringStsEndpoint) ?
                              parameters[ConnectionStringStsEndpoint] :
                              null;

            if (!System.Uri.TryCreate(endpoint, UriKind.Absolute, out var endpointUri) ||
                string.IsNullOrEmpty(endpointUri.Host) ||
                // The legacy SDK registers an sb URI parser that can hide explicit port zero.
                // Validate the same authority with the standard HTTP parser as well.
                !System.Uri.TryCreate("http" + endpoint.Substring(endpoint.IndexOf(':')), UriKind.Absolute, out var portUri) ||
                portUri.Port == 0)
            {
                staticWriteToLog("The Service Bus endpoint or port is invalid.");
                return null;
            }

            string ns = GetNamespaceNameFromEndpoint(endpoint, staticWriteToLog, key);           

            if (!parameters.TryGetValue(ConnectionStringSharedAccessKeyName, out var sharedAccessKeyName) ||
                string.IsNullOrWhiteSpace(sharedAccessKeyName))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceSharedAccessKeyNameIsInvalid, key));
                return null;
            }

            if (!parameters.TryGetValue(ConnectionStringSharedAccessKey, out var sharedAccessKey) ||
                string.IsNullOrWhiteSpace(sharedAccessKey))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceSharedAccessKeyIsInvalid, key));
                return null;
            }

            var settings = new MessagingFactorySettings();
            var transportType = settings.TransportType;

            if (parameters.ContainsKey(ConnectionStringTransportType))
            {
                Enum.TryParse(parameters[ConnectionStringTransportType], true, out transportType);
            }

            string entityPath = string.Empty;

            if (parameters.ContainsKey(ConnectionStringEntityPath))
            {
                entityPath = parameters[ConnectionStringEntityPath];
            }

            var useDevelopmentEmulator = parameters.ContainsKey(ConnectionStringUseDevelopmentEmulator) &&
                bool.TryParse(parameters[ConnectionStringUseDevelopmentEmulator], out var emulator) && emulator;

            return new ServiceBusNamespace(ServiceBusNamespaceType.Cloud, connectionString, endpoint, ns, null,
                sharedAccessKeyName, sharedAccessKey, stsEndpoint, transportType, true,
                entityPath, isUserCreated)
            {
                UseDevelopmentEmulator = useDevelopmentEmulator,
                Namespace = useDevelopmentEmulator ? endpointUri.Host : ns
            };
        }

        static string GetNamespaceNameFromEndpoint(string endpoint, WriteToLogDelegate staticWriteToLog, 
            string key)
        {
             Uri uri;

            try
            {
                uri = new Uri(endpoint);
            }
            catch (UriFormatException)
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture, 
                    ServiceBusNamespaceEndpointUriIsInvalid, key));
                return null;
            }

            return uri.Host.Split('.')[0];
        }

        static ServiceBusNamespace GetServiceBusNamespaceUsingEntra(string key, string connectionString,
            WriteToLogDelegate staticWriteToLog, bool isUserCreated, Dictionary<string, string> parameters)
        {
            var endpoint = parameters.ContainsKey(ConnectionStringEndpoint) ?
                           parameters[ConnectionStringEndpoint] :
                           null;

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                staticWriteToLog(string.Format(CultureInfo.CurrentCulture,
                    ServiceBusNamespaceEndpointIsNullOrEmpty, key));
                return null;
            }

            if (!endpoint.Contains("://"))
                endpoint = "sb://" + endpoint;
            if (!System.Uri.TryCreate(endpoint, UriKind.Absolute, out var address) || string.IsNullOrEmpty(address.Host))
            {
                staticWriteToLog($"Skipping {key}: the Entra namespace endpoint is invalid.");
                return null;
            }

            string ns = GetNamespaceNameFromEndpoint(endpoint, staticWriteToLog, key);

            var tenantId = parameters.ContainsKey(ConnectionStringTenantId)
                ? parameters[ConnectionStringTenantId]
                : null;

            var settings = new MessagingFactorySettings();
            var transportType = settings.TransportType;

            if (parameters.ContainsKey(ConnectionStringTransportType))
            {
                Enum.TryParse(parameters[ConnectionStringTransportType], true, out transportType);
            }

            string entityPath = string.Empty;

            if (parameters.ContainsKey(ConnectionStringEntityPath))
            {
                entityPath = parameters[ConnectionStringEntityPath];
            }

            return new ServiceBusNamespace(endpoint, ns, tenantId, transportType, entityPath, isUserCreated);
        }

        /// <summary>
        /// Builds a metadata-only connection string for persisting an Entra connection.
        /// No secrets are stored — only the endpoint, auth mode, transport type,
        /// and optionally the tenant ID and entity path.
        /// </summary>
        public static string BuildEntraConnectionString(string endpoint, string tenantId,
            TransportType transportType, string entityPath = null)
        {
            // tenantId is interpolated directly into a semicolon-delimited connection string.
            // A semicolon in a manually-typed tenant ID (bypassing the Options-dialog validation)
            // could otherwise inject an extra field, e.g. "tenant;EntityPath=other".
            var semicolonIndex = tenantId?.IndexOf(';') ?? -1;
            if (semicolonIndex >= 0)
            {
                tenantId = tenantId.Substring(0, semicolonIndex);
            }

            var hasTenant = !string.IsNullOrWhiteSpace(tenantId);
            var hasEntity = !string.IsNullOrWhiteSpace(entityPath);

            if (hasEntity && hasTenant)
            {
                return string.Format(EntraConnectionStringEntityPathFormat,
                    endpoint, tenantId, transportType, entityPath);
            }

            if (hasEntity)
            {
                return string.Format(EntraConnectionStringEntityPathNoTenantFormat,
                    endpoint, transportType, entityPath);
            }

            if (hasTenant)
            {
                return string.Format(EntraConnectionStringTenantFormat, endpoint, tenantId, transportType);
            }

            return string.Format(EntraConnectionStringFormat, endpoint, transportType);
        }

        #endregion
    }
}
