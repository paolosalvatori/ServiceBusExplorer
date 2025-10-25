using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Common.Models;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Contracts;

public interface IServiceBusService
{
    ServiceBusConnection Connection { get; }
    ServiceBusClient Client { get; }
    ServiceBusAdministrationClient AdminClient { get; }
    WriteToLogDelegate WriteToLog { get; }

    Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }
    Dictionary<string, Type> BrokeredMessageInspectors { get; set; }
    Dictionary<string, Type> BrokeredMessageGenerators { get; set; }

    bool IsPremiumNamespace();
    bool ConnectionStringContainsEntityPath();
    bool IsCloudNamespace(); 

    Task<bool> ConnectAsync(ServiceBusNamespace busNamespace, EncodingType encodingType);
    Task<IEnumerable<QueueMetadata>> GetQueuesAsync(string filter, int timeoutInSeconds);
    Task<QueueMetadata> GetQueueAsync(string name);

    Task<QueueMetadata> CreateQueueAsync(QueueMetadata metadata);
    Task<QueueMetadata> UpdateQueueAsync(QueueMetadata metadata);
    ServiceBusSender CreateSender(string name);
    Task<ServiceBusReceiver> CreateReceiverAsync(
        string name,
        ServiceBusReceiveMode receiveMode,
        SubQueue queueType = SubQueue.None,
        string sessionId = null);

    Task DeleteQueueAsync(string name);
    IServiceBusService Clone();
    void AddLogging(WriteToLogDelegate deletgate);
    string GetMessageText(BinaryData data);
    ServiceBusMessage ReceivedToMessage(ServiceBusReceivedMessage receivedMessage);
}
