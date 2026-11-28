using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusConsumerGroup : IServiceBusEntity
    {
        Task<ConsumerGroupDescription> GetConsumerGroupAsync(string eventHubPath, string name);

        ConsumerGroupDescription CreateConsumerGroup(ConsumerGroupDescription description);
        
        void DeleteConsumerGroup(ConsumerGroupDescription consumerGroupDescription);
                
        void DeleteConsumerGroups(string eventHubName, IEnumerable<string> consumerGroups);
        
        ConsumerGroupDescription GetConsumerGroup(string eventHubPath, string name);
                
        IEnumerable<ConsumerGroupDescription> GetConsumerGroups(string path);
        
        Uri GetConsumerGroupUri(string eventHubName, string consumerGroupPath);
        
        ConsumerGroupDescription UpdateConsumerGroup(ConsumerGroupDescription description);
    }
}
