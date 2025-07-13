using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using ServiceBusExplorer.Common.Abstractions;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Contracts;

public interface IServiceBusService
{
    ServiceBusConnection Connection { get; }
    ServiceBusClient Client { get; }
    ServiceBusAdministrationClient AdminClient { get; }

    Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }
    Dictionary<string, Type> BrokeredMessageInspectors { get; set; }
    Dictionary<string, Type> BrokeredMessageGenerators { get; set; }


    bool IsPremiumNamespace();
    bool ConnectionStringContainsEntityPath();
    bool IsCloudNamespace(); 

    Task<bool> ConnectAsync(ServiceBusNamespace busNamespace, EncodingType encodingType);
    Task<IEnumerable<QueueMetadata>> GetQueuesAsync(string filter, int timeoutInSeconds);
}
