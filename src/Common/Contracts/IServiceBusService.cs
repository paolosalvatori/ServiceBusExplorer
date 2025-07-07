
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Common.Abstractions;
using ServiceBusExplorer.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Contracts;

public interface IServiceBusService
{
    ServiceBusNamespace CurrentNamespace { get; }
    Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }
    ServiceBusTransportType TransportType { get; set; }

    Task<bool> ConnectAsync(ServiceBusNamespace busNamespace);
    Task<IEnumerable<QueueMetadata>> GetQueuesAsync(string filter, int timeoutInSeconds); 
}
