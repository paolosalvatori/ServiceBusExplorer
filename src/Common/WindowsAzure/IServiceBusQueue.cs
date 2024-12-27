using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusQueue : IServiceBusEntity
    {
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
    }
}
