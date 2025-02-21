using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusEventHub : IServiceBusEntity
    {
        EventHubDescription CreateEventHub(EventHubDescription description);
        
        void DeleteEventHub(EventHubDescription eventHubDescription);
                
        void DeleteEventHubs(IEnumerable<string> eventHubs);
        
        EventHubDescription GetEventHub(string path);
                
        Uri GetEventHubUri(string eventHubPath);
        
        EventHubDescription UpdateEventHub(EventHubDescription description);
    }
}
