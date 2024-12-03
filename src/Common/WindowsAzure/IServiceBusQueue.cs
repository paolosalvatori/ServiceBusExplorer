using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusExplorer.WindowsAzure
{
    public interface IServiceBusQueue
    {
        #region Management functions
        QueueDescription CreateQueue(QueueDescription description);
        
        QueueDescription CreateQueue(string path);
        
        Task DeleteQueue(QueueDescription queueDescription);
        
        Task DeleteQueue(string path);
        
        Task DeleteQueues(IEnumerable<string> queues);
        
        QueueDescription GetQueue(string path);
        
        Uri GetQueueDeadLetterQueueUri(string queuePath);
        
        IEnumerable<QueueDescription> GetQueues(string filter, int timeoutInSeconds);
        
        Uri GetQueueUri(string queuePath);
        
        QueueDescription RenameQueue(string path, string newPath);
        
        QueueDescription UpdateQueue(QueueDescription description);
        #endregion

        #region Event handlers
        delegate void EventHandler(ServiceBusHelperEventArgs args);

        event EventHandler OnDelete;
        
        event EventHandler OnCreate;
        #endregion

        #region Properties
        WriteToLogDelegate WriteToLog { get; set; }
        
        string Scheme { get; set; }
        #endregion
    }
}
