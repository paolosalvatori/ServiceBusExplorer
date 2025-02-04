using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusConsumerGroup : IServiceBusEntity
    {
        ConsumerGroupDescription CreateConsumerGroup(ConsumerGroupDescription description);
        
        void DeleteConsumerGroup(ConsumerGroupDescription consumerGroupDescription);
        
        void DeleteConsumerGroup(string eventHubName, string name);
        
        void DeleteConsumerGroups(string eventHubName, IEnumerable<string> consumerGroups);
        
        ConsumerGroupDescription GetConsumerGroup(string eventHubPath, string name);
        
        IEnumerable<ConsumerGroupDescription> GetConsumerGroups(EventHubDescription description);
        
        IEnumerable<ConsumerGroupDescription> GetConsumerGroups(string path);
        
        Uri GetConsumerGroupUri(string eventHubName, string consumerGroupPath);
        
        ConsumerGroupDescription UpdateConsumerGroup(ConsumerGroupDescription description);
    }
}
