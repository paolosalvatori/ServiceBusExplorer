using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal class ServiceBusPartition : ServiceBusEntity, IServiceBusPartition
    {
        private const string ConsumerGroupCannotBeNull = "The consumerGroup argument cannot be null or empty.";

        private readonly string servicePath = string.Empty;

        public ServiceBusPartition(ServiceBusNamespace serviceBusNamespace, NamespaceManager namespaceManager) : base(serviceBusNamespace, namespaceManager)
        {
        }

        protected override EntityType EntityType => EntityType.All; // Partitions are not included in the EntityType enum

        /// <summary>
        /// Retrieves a partition.
        /// </summary>
        /// <param name="path">Path of the event hub relative to the service namespace base address.</param>
        /// <param name="consumerGroupName">The consumer group name.</param>
        /// <param name="name">Partition name.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of partitions attached to the event hub passed as a parameter.</returns>
        public PartitionDescription GetPartition(string path, string consumerGroupName, string name)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetEventHubPartition(path, consumerGroupName, name), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves the collection of partitions of the event hub passed as a parameter.
        /// </summary>
        /// <param name="path">Path of the event hub relative to the service namespace base address.</param>
        /// <param name="consumerGroupName">The consumer group name.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of partitions attached to the event hub passed as a parameter.</returns>
        public IEnumerable<PartitionDescription> GetPartitions(string path, string consumerGroupName)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (string.IsNullOrWhiteSpace(consumerGroupName))
            {
                throw new ArgumentException(ConsumerGroupCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var description = NamespaceManager.GetEventHub(path);
                return description.PartitionIds.Select((t, i) => i).Select(index => RetryHelper.RetryFunc(() => NamespaceManager.GetEventHubPartition(description.Path, consumerGroupName, description.PartitionIds[index]), WriteToLog)).ToList();
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Gets the uri of a partition.
        /// </summary>
        /// <param name="eventHubName">Name of the event hub.</param>
        /// <param name="consumerGroupName">Name of the partition.</param>
        /// <param name="partitionId">The partition id.</param>
        /// <returns>The absolute uri of the partition.</returns>
        public Uri GetPartitionUri(string eventHubName, string consumerGroupName, string partitionId)
        {
            return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, eventHubName, "/", consumerGroupName, "/", partitionId));
        }
    }
}
