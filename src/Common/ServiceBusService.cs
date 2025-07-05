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
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

// ReSharper disable CheckNamespace
namespace Common;
// ReSharper restore CheckNamespace

public class ServiceBusService : IServiceBusService
{
    readonly WriteToLogDelegate writeToLog;

    public string ConnectionString { get; set; }
    public ServiceBusTransportType TransportType { get; set; }

    public Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }


    public WriteToLogDelegate WriteToLog
    {
        get
        {
            return writeToLog;
        }
    }

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

}
