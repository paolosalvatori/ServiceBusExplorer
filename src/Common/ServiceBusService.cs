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

using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Common.Contracts;
using Microsoft.Azure.NotificationHubs;
using ServiceBusExplorer.Common.Abstractions;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

// ReSharper disable CheckNamespace
namespace Common;
// ReSharper restore CheckNamespace

public class ServiceBusService : IServiceBusService
{
    //readonly WriteToLogDelegate writeToLog;

    public enum BodyType
    {
        Stream,
        String,
        Wcf,
        ByteArray
    }


    public ServiceBusAdministrationClient Client { get; set; }
    public string ConnectionString { get; set; }
    public ServiceBusTransportType TransportType { get; set; }
    public EncodingType EncodingType { get; set; }


    public Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; } = [];
    public ServiceBusNamespace CurrentNamespace { get; private set; }

    private bool _isCurrentNamespacePremium { get; set; }


    //public WriteToLogDelegate WriteToLog
    //{
    //    get
    //    {
    //        return writeToLog;
    //    }
    //}

    //public ServiceBusService(WriteToLogDelegate writeToLog)
    //{
    //    this.writeToLog = writeToLog;
    //}

    public ServiceBusService()
    {
    }

    /// <summary>
    ///  Dispose of the returned ServiceBusClient object by calling DisposeAsync().
    /// </summary>
    /// <returns>An Azure.Messaging.ServiceBus.ServiceBusClient</returns>
    public ServiceBusClient CreateServiceBusClient()
    {
        return new ServiceBusClient(
            ConnectionString,
            new ServiceBusClientOptions { TransportType = this.TransportType });
    }

    /// <summary>
    /// Connects the ServiceBusHelper object to service bus namespace contained in the ServiceBusNamespaces dictionary.
    /// </summary>
    /// <param name="busNamespace">The Service Bus namespace.</param>
    /// <returns>True if the operation succeeds, false otherwise.</returns>
    public async Task<bool> ConnectAsync(ServiceBusNamespace busNamespace)
    {
        if (string.IsNullOrWhiteSpace(busNamespace?.ConnectionString))
        {
            throw new ArgumentException("The connection string argument cannot be null.");
        }

        if (!TestNamespaceHostIsContactable(busNamespace))
        {
            throw new Exception($"Could not contact host in connection string: {busNamespace.ConnectionString}.");
        }

        await Task.CompletedTask;

        return await ConnectAsyncInternal(busNamespace);
    }

    private async Task<bool> ConnectAsyncInternal(ServiceBusNamespace busNamespace)
    {
        var connectionString = busNamespace.ConnectionString;

        Client = new ServiceBusAdministrationClient(connectionString);

        CurrentNamespace = busNamespace;

        NamespaceProperties namespaceProperties = await Client.GetNamespacePropertiesAsync().ConfigureAwait(false);

        _isCurrentNamespacePremium = namespaceProperties.MessagingSku == MessagingSku.Premium;

        return true;
    }

    private bool TestNamespaceHostIsContactable(ServiceBusNamespace busNamespace)
    {
        if (!Uri.TryCreate(busNamespace.Uri, UriKind.Absolute, out var namespaceUri))
        {
            return false;
        }

        try
        {
            System.Net.Dns.GetHostEntry(namespaceUri.Host);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<QueueMetadata>> GetQueuesAsync(string filter, int timeoutInSeconds)
    {
        var queues = new List<QueueMetadata>();
        try
        {
            await foreach (var properties in Client.GetQueuesAsync())
            {
                var runtime = await Client.GetQueueRuntimePropertiesAsync(properties.Name);
                queues.Add(QueueMetadata.Create(properties, runtime)); 
            }
            return queues; 
        }
        catch
        {
            //TODO: 
            return []; 
        }
    }

    public bool IsPremiumNamespace()
    {
        return _isCurrentNamespacePremium; 
    }

    public bool ConnectionStringContainsEntityPath()
    {
        var connectionStringProperties = ServiceBusConnectionStringProperties.Parse(ConnectionString);
        return connectionStringProperties?.EntityPath != null;
    }
}
