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
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;

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

    public Uri NamespaceUri { get; set; }
    public string ConnectionString { get; set; }
    public ServiceBusTransportType TransportType { get; set; }

    public Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }


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

    public bool ConnectionStringContainsEntityPath()
    {
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
        return new ServiceBusClient(
            ConnectionString,
            new ServiceBusClientOptions { TransportType = this.TransportType });
    }

    private ServiceBusNamespace _serviceBusNamespace { get; set; }

    /// <summary>
    /// Connects the ServiceBusHelper object to service bus namespace contained in the ServiceBusNamespaces dictionary.
    /// </summary>
    /// <param name="serviceBusNamespace">The Service Bus namespace.</param>
    /// <returns>True if the operation succeeds, false otherwise.</returns>
    public bool Connect(ServiceBusNamespace serviceBusNamespace)
    {
        //this.serviceBusNamespaceInstance = serviceBusNamespace;

        if (string.IsNullOrWhiteSpace(serviceBusNamespace?.ConnectionString))
        {
            throw new ArgumentException("The connection string argument cannot be null.");
        }

        if (!TestNamespaceHostIsContactable())
        {
            throw new Exception($"Could not contact host in connection string: {serviceBusNamespace.ConnectionString}.");
        }

        _serviceBusNamespace = serviceBusNamespace;
        var func = ConnectInternal;

        return RetryHelper.RetryFunc(func, null);
    }

    private bool ConnectInternal()
    {
        var connectionString = _serviceBusNamespace.ConnectionString;

        ServiceBusClient client = new(connectionString);

        return true;
    }


    private bool TestNamespaceHostIsContactable()
    {
        if (!Uri.TryCreate(_serviceBusNamespace.Uri, UriKind.Absolute, out var namespaceUri))
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
}
