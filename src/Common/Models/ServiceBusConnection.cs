
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using ServiceBusExplorer.Enums;

namespace Common.Models;

public class ServiceBusConnection
{
    public ServiceBusConnection(
        ServiceBusNamespace currentNamespace, 
        ServiceBusConnectionStringProperties connectionStringProperties,
        NamespaceProperties namespaceProperties,
        EncodingType encodingType)
    {
        Namespace = currentNamespace;
        ConnectionStringProperties = connectionStringProperties;
        NamespaceProperties = namespaceProperties;
        EncodingType = encodingType;
    }

    public ServiceBusNamespace Namespace { get; }
    public ServiceBusConnectionStringProperties ConnectionStringProperties { get; }
    public NamespaceProperties NamespaceProperties { get; }
    public EncodingType EncodingType { get; }
}
