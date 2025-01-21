using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusNotificationHub : IServiceBusEntity
    {
        NotificationHubDescription CreateNotificationHub(NotificationHubDescription description);
        
        Task DeleteNotificationHub(NotificationHubDescription notificationHubDescription);
        
        Task DeleteNotificationHub(string path);
        
        Task DeleteNotificationHubs(IEnumerable<string> notificationHubs);
        
        NotificationHubDescription GetNotificationHub(string path);
        
        IEnumerable<NotificationHubDescription> GetNotificationHubs(int timeoutInSeconds);
        
        Uri GetNotificationHubUri(string notificationHubPath);
        
        NotificationHubDescription UpdateNotificationHub(NotificationHubDescription description);
    }
}
