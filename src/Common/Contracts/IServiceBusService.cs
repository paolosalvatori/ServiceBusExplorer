
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Contracts;

public interface IServiceBusService
{
    Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }
    ServiceBusTransportType TransportType { get; set; }

    Task<bool> ConnectAsync(ServiceBusNamespace busNamespace); 
}
