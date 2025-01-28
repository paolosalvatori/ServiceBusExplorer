using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusEventHub : IServiceBusEntity
    {
        EventHubDescription CreateEventHub(EventHubDescription description);
        
        void DeleteEventHub(EventHubDescription eventHubDescription);
        
        void DeleteEventHub(string path);
        
        void DeleteEventHubs(IEnumerable<string> eventHubs);
        
        EventHubDescription GetEventHub(string path);
        
        Task<IEnumerable<EventHubDescription>> GetEventHubs(int timeoutInSeconds);
        
        Uri GetEventHubUri(string eventHubPath);
        
        EventHubDescription UpdateEventHub(EventHubDescription description);
    }
}
