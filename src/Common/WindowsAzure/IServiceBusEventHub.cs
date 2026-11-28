using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusEventHub : IServiceBusEntity
    {
        Task<IEnumerable<EventHubDescription>> GetEventHubsAsync();
        Task<EventHubDescription> GetEventHubAsync(string path);
        Task<EventHubDescription> UpdateEventHubAsync(EventHubDescription description);

        EventHubDescription CreateEventHub(EventHubDescription description);
        
        void DeleteEventHub(EventHubDescription eventHubDescription);
                
        void DeleteEventHubs(IEnumerable<string> eventHubs);
        
        EventHubDescription GetEventHub(string path);
                
        Uri GetEventHubUri(string eventHubPath);
        
        EventHubDescription UpdateEventHub(EventHubDescription description);
    }
}
