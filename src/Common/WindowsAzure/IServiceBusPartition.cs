using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusPartition : IServiceBusEntity
    {
        PartitionDescription GetPartition(PartitionDescription partitionDescription);
        
        PartitionDescription GetPartition(string path, string consumerGroupName, string name);
        
        IEnumerable<PartitionDescription> GetPartitions(EventHubDescription description);
        
        IEnumerable<PartitionDescription> GetPartitions(EventHubDescription description, string consumerGroupName);
        
        IEnumerable<PartitionDescription> GetPartitions(string path);
        
        IEnumerable<PartitionDescription> GetPartitions(string path, string consumerGroupName);
        
        Uri GetPartitionUri(string eventHubName, string consumerGroupName, string partitionId);
    }
}
