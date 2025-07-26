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
using Common.Models;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable CheckNamespace
namespace Common;
// ReSharper restore CheckNamespace

public class ServiceBusService : IServiceBusService
{
    #region Private Constants 

    private const string CloudServiceBusPostfix = ".servicebus.windows.net";
    private const string GermanyServiceBusPostfix = ".servicebus.cloudapi.de";
    private const string ChinaServiceBusPostfix = ".servicebus.chinacloudapi.cn";
    private const string TestServiceBusPostFix = ".servicebus.int7.windows-int.net";

    #endregion

    public enum BodyType
    {
        Stream,
        String,
        Wcf,
        ByteArray
    }

    public ServiceBusConnection Connection { get; private set; }
    public ServiceBusClient Client { get; private set; }
    public ServiceBusAdministrationClient AdminClient { get; private set; }

    public Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; } = [];
    public Dictionary<string, Type> BrokeredMessageInspectors { get; set; } = [];
    public Dictionary<string, Type> BrokeredMessageGenerators { get; set; } = [];

    public WriteToLogDelegate WriteToLog { get; private set; }

    public ServiceBusService(WriteToLogDelegate writeToLog)
    {
        WriteToLog = writeToLog;
    }

    public void AddLogging(WriteToLogDelegate deletgate)
    {
        WriteToLog = deletgate;
    }

    /// <summary>
    /// Connects the ServiceBusHelper object to service bus namespace contained in the ServiceBusNamespaces dictionary.
    /// </summary>
    /// <param name="busNamespace">The Service Bus namespace.</param>
    /// <returns>True if the operation succeeds, false otherwise.</returns>
    public async Task<bool> ConnectAsync(ServiceBusNamespace busNamespace, EncodingType encodingType)
    {
        //TODO: What if we can't connect? 
        if (string.IsNullOrWhiteSpace(busNamespace?.ConnectionString))
        {
            throw new ArgumentException("The connection string argument cannot be null.");
        }

        if (!TestNamespaceHostIsContactable(busNamespace))
        {
            throw new Exception($"Could not contact host in connection string: {busNamespace.ConnectionString}.");
        }

        await Task.CompletedTask;

        return await ConnectAsyncInternal(busNamespace, encodingType);
    }

    private async Task<bool> ConnectAsyncInternal(ServiceBusNamespace busNamespace, EncodingType encodingType)
    {
        var connectionString = busNamespace.ConnectionString;

        AdminClient = new ServiceBusAdministrationClient(connectionString);
        Client = new ServiceBusClient(connectionString);

        var namespaceProperties = await AdminClient.GetNamespacePropertiesAsync().ConfigureAwait(false);
        var connectionStringProperties = ServiceBusConnectionStringProperties.Parse(connectionString);

        Connection = new(busNamespace, connectionStringProperties, namespaceProperties, encodingType);

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
            await foreach (var properties in AdminClient.GetQueuesAsync())
            {
                var runtime = await AdminClient.GetQueueRuntimePropertiesAsync(properties.Name);
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

    public async Task<QueueMetadata> GetQueueAsync(string name)
    {
        try
        {
            var queues = await GetQueuesAsync("", 1000); 
            return queues.FirstOrDefault(q => q.Name == name);
        }
        catch
        {
            //TODO: 
            return null;
        }
    }

    public async Task<QueueMetadata> CreateQueueAsync(QueueMetadata metadata)
    {
        try
        {
            await AdminClient.CreateQueueAsync(metadata.AsCreateQueueOptions());
            return await GetQueueAsync(metadata.Name);
        }
        catch
        {
            //TODO: 
            return null; 
        }
    }

    public async Task<QueueMetadata> UpdateQueueAsync(QueueMetadata metadata)
    {
        try
        {
            await AdminClient.UpdateQueueAsync(metadata.AsQueueProperties);
            return await GetQueueAsync(metadata.Name);
        }
        catch
        {
            //TODO: 
            return null;
        }
    }

    public ServiceBusSender CreateSender(string name)
    {
        try
        {
            return Client.CreateSender(name); 
        }
        catch
        {
            //TODO:
            return null;
        }
    }

    public ServiceBusReceiver CreateReceiver(string name, ServiceBusReceiveMode mode)
    {
        var opts = new ServiceBusReceiverOptions
        {
            ReceiveMode = mode
        };

        try
        {
            return Client.CreateReceiver(name, opts);
        }
        catch
        {
            //TODO:
            return null;
        }
    }

    public async Task DeleteQueueAsync(string name)
    {
        try
        {
            await AdminClient.DeleteQueueAsync(name);
        }
        catch
        {
            //TODO: 
        }
    }

    public bool IsPremiumNamespace()
    {
        return Connection.NamespaceProperties.MessagingSku == MessagingSku.Premium; 
    }

    public bool ConnectionStringContainsEntityPath()
    {
        return Connection.ConnectionStringProperties.EntityPath != null;
    }

    public bool IsCloudNamespace()
    {
        var connNamespace = Connection.Namespace;

        return connNamespace.ConnectionStringType == ServiceBusNamespaceType.Cloud ||
              (connNamespace != null &&
               !string.IsNullOrWhiteSpace(connNamespace.Uri) &&
               (connNamespace.Uri.Contains(CloudServiceBusPostfix) ||
                connNamespace.Uri.Contains(TestServiceBusPostFix) ||
                connNamespace.Uri.Contains(GermanyServiceBusPostfix) ||
                connNamespace.Uri.Contains(ChinaServiceBusPostfix)));
    }

    public ServiceBusMessage ReceivedToMessage(ServiceBusReceivedMessage receivedMessage)
    {
        var message = new ServiceBusMessage(receivedMessage.Body);

        foreach (var prop in receivedMessage.ApplicationProperties)
        {
            message.ApplicationProperties[prop.Key] = prop.Value;
        }

        message.ContentType = receivedMessage.ContentType;
        message.CorrelationId = receivedMessage.CorrelationId;
        message.Subject = receivedMessage.Subject;
        message.MessageId = receivedMessage.MessageId;
        message.PartitionKey = receivedMessage.PartitionKey;
        message.SessionId = receivedMessage.SessionId;
        message.TimeToLive = receivedMessage.TimeToLive;

        return message;
    }

    public string GetMessageText(BinaryData data)
    {
        return Encoding.UTF8.GetString(data.ToArray());
    }

    public IServiceBusService Clone()
    {
        return new ServiceBusService(WriteToLog)
        {
            AdminClient = AdminClient,
            BrokeredMessageGenerators = BrokeredMessageGenerators,
            BrokeredMessageInspectors = BrokeredMessageInspectors,
            Client = Client,
            Connection = Connection,
            ServiceBusNamespaces = ServiceBusNamespaces,
        };
    }
}
