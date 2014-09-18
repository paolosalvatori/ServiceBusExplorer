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
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public enum ServiceBusNamespaceType
    {
        Custom,
        Cloud,
        OnPremises
    }

    /// <summary>
    /// This class represents a service bus namespace address and authentication credentials
    /// </summary>
    public class ServiceBusNamespace
    {
        #region Public Constants
        //***************************
        // Formats
        //***************************
        public const string ConnectionStringFormat = "Endpoint={0};SharedSecretIssuer={1};SharedSecretValue={2};TransportType={3}";
        public const string SasConnectionStringFormat = "Endpoint={0};SharedAccessKeyName={1};SharedAccessKey={2};TransportType={3}";
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the ServiceBusHelper class.
        /// </summary>
        public ServiceBusNamespace()
        {
            ConnectionStringType = ServiceBusNamespaceType.Cloud;
            ConnectionString = default(string);
            Uri = default(string);
            Namespace = default(string);
            IssuerName = default(string);
            IssuerSecret = default(string);
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
        public ServiceBusNamespace(ServiceBusNamespaceType connectionStringType,
                                   string connectionString,
                                   string uri,
                                   string ns,
                                   string servicePath,
                                   string name,
                                   string key,
                                   string stsEndpoint,
                                   TransportType transportType,
                                   bool isSas = false)
        {
            ConnectionStringType = connectionStringType;
            Uri = string.IsNullOrWhiteSpace(uri) ? 
                  ServiceBusEnvironment.CreateServiceUri("sb", ns, servicePath).ToString() : 
                  uri;
            ConnectionString = ConnectionStringType == ServiceBusNamespaceType.Custom ? 
                               ConnectionString = string.Format(ConnectionStringFormat,
                                                                Uri,
                                                                name,
                                                                key,
                                                                transportType) : 
                               connectionString;
            Namespace = ns;
            IssuerName = name;
            if (isSas)
            {
                SharedAccessKeyName = name;
                SharedAccessKey = key;
            }
            else
            {
                IssuerSecret = key;
                ServicePath = servicePath;
            }
            TransportType = transportType;
            StsEndpoint = stsEndpoint;
            RuntimePort = default(string);
            ManagementPort = default(string);
            WindowsDomain = default(string);
            WindowsUserName = default(string);
            WindowsPassword = default(string);
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
                                   TransportType transportType)
        {
            ConnectionStringType = ServiceBusNamespaceType.OnPremises;
            ConnectionString = connectionString;
            Uri = endpoint;
            var uri = new Uri(endpoint);
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                Uri = ServiceBusEnvironment.CreateServiceUri(uri.Scheme, ns, null).ToString();
            }
            Namespace = ns;
            IssuerName = default(string);
            IssuerSecret = default(string);
            var settings = new MessagingFactorySettings();
            TransportType = settings.TransportType;
            StsEndpoint = stsEndpoint;
            RuntimePort = runtimePort;
            ManagementPort = managementPort;
            WindowsDomain = windowsDomain;
            WindowsUserName = windowsUsername;
            WindowsPassword = windowsPassword;
            TransportType = transportType;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the service bus namespace type.
        /// </summary>
        public ServiceBusNamespaceType ConnectionStringType { get; set; }

        /// <summary>
        /// Gets or sets the service bus namespace connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the full address of the service bus namespace.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets or sets the service bus namespace.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the issuer name of the shared secret credentials.
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Gets or sets the issuer secret of the shared secret credentials.
        /// </summary>
        public string IssuerSecret { get; set; }

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
        #endregion
    }
}
