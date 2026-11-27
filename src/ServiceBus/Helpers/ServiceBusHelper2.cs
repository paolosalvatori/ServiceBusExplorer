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

using System;
using System.Threading.Tasks;

using Azure.Core;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

using ServiceBusExplorer.Utilities.Helpers;

// ReSharper disable CheckNamespace
namespace ServiceBusExplorer.ServiceBus.Helpers
// ReSharper restore CheckNamespace
{
    public class ServiceBusHelper2
    {
        readonly WriteToLogDelegate writeToLog;

        public string ConnectionString { get; set; }
        public string AdministrationConnectionString { get; set; }
        // Keep the original scope available to management-only UI probes.
        public string ProfileEntityPath { get; set; }
        public Uri DataPlaneEndpoint { get; set; }
        public string DataPlaneUnavailableReason { get; set; }
        public ServiceBusTransportType TransportType { get; set; }

        /// <summary>
        /// Fully qualified namespace (e.g. "mynamespace.servicebus.windows.net") used for Entra auth.
        /// When set together with EntraTokenCredential, SDK clients use token-based auth instead of connection strings.
        /// </summary>
        public string FullyQualifiedNamespace { get; set; }

        /// <summary>
        /// Token credential for Entra auth with the new Azure.Messaging.ServiceBus SDK.
        /// </summary>
        public TokenCredential EntraTokenCredential { get; set; }

        /// <summary>
        /// Gets a value indicating whether the current connection uses Entra
        /// token-based authentication instead of a connection string.
        /// </summary>
        public bool IsEntra => EntraTokenCredential != null && !string.IsNullOrWhiteSpace(FullyQualifiedNamespace);

        public WriteToLogDelegate WriteToLog
        {
            get
            {
                return writeToLog;
            }
        }

        public ServiceBusHelper2(WriteToLogDelegate writeToLog)
        {
            this.writeToLog = writeToLog;
        }

        /// <summary>
        /// Returns true when the connection string contains an EntityPath segment.
        /// Always returns false for Entra connections.
        /// </summary>
        public bool ConnectionStringContainsEntityPath()
        {
            if (IsEntra) return false;
            if (ProfileEntityPath != null) return !string.IsNullOrWhiteSpace(ProfileEntityPath);

            var connectionStringProperties = ServiceBusConnectionStringProperties.Parse(ConnectionString);

            if (connectionStringProperties?.EntityPath != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Dispose of the returned ServiceBusClient object by calling DisposeAsync().
        /// </summary>
        /// <returns>An Azure.Messaging.ServiceBus.ServiceBusClient</returns>
        public ServiceBusClient CreateServiceBusClient()
        {
            if (DataPlaneUnavailableReason != null) throw new NotSupportedException(DataPlaneUnavailableReason);
            var endpoint = IsEntra ? DataPlaneEndpoint : ServiceBusConnectionStringProperties.Parse(ConnectionString).Endpoint;
            var options = new ServiceBusClientOptions { TransportType = TransportType };
            if (endpoint != null && !endpoint.IsDefaultPort)
                options.CustomEndpointAddress = endpoint;

            return IsEntra
                ? new ServiceBusClient(FullyQualifiedNamespace, EntraTokenCredential, options)
                : new ServiceBusClient(ConnectionString, options);
        }

        /// <summary>
        /// Creates a <see cref="ServiceBusAdministrationClient"/> using Entra credentials or
        /// a connection string depending on the current authentication mode.
        /// </summary>
        public virtual ServiceBusAdministrationClient CreateAdministrationClient()
        {
            return IsEntra
                ? new ServiceBusAdministrationClient(FullyQualifiedNamespace, EntraTokenCredential)
                : new ServiceBusAdministrationClient(AdministrationConnectionString ?? ConnectionString);
        }

        public async Task<bool> IsPremiumNamespace()
        {
            // Entity-only SAS access cannot query namespace SKU. Match the editor's conservative default.
            if (ConnectionStringContainsEntityPath()) return false;
            var administrationClient = CreateAdministrationClient();
            NamespaceProperties namespaceProperties = await administrationClient.GetNamespacePropertiesAsync().ConfigureAwait(false);

            return namespaceProperties.MessagingSku == MessagingSku.Premium;
        }

        public async Task<bool> IsQueue(string name)
        {
            var administrationClient = CreateAdministrationClient();
            return await administrationClient.QueueExistsAsync(name).ConfigureAwait(false);
        }

        public async Task<bool> IsTopic(string name)
        {
            var administrationClient = CreateAdministrationClient();
            return await administrationClient.TopicExistsAsync(name).ConfigureAwait(false);
        }
    }
}
