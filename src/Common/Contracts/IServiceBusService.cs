
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Helpers;
using System.Collections.Generic;

namespace Common.Contracts;

public interface IServiceBusService
{
    Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }
    ServiceBusTransportType TransportType { get; set; }

    bool Connect(ServiceBusNamespace busNamespace); 
}
