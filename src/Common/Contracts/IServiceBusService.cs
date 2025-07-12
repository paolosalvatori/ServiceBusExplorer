
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Common.Abstractions;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Contracts;

public interface IServiceBusService
{
    ServiceBusNamespace CurrentNamespace { get; }
    public EncodingType EncodingType { get; set; }

    Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }
    ServiceBusTransportType TransportType { get; set; }

    bool IsPremiumNamespace();
    bool ConnectionStringContainsEntityPath();

    Task<bool> ConnectAsync(ServiceBusNamespace busNamespace);
    Task<IEnumerable<QueueMetadata>> GetQueuesAsync(string filter, int timeoutInSeconds);
}
