using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBusExplorer.WindowsAzure
{
    internal sealed class ServiceBusQueue : ServiceBusEntity, IServiceBusQueue
    {
        private const string QueueDescriptionCannotBeNull = "The queue description argument cannot be null.";
        private const string QueueCreated = "The queue {0} has been successfully created.";
        private const string QueueDeleted = "The queue {0} has been successfully deleted.";
        private const string QueueRenamed = "The queue {0} has been successfully renamed to {1}.";
        private const string QueueUpdated = "The queue {0} has been successfully updated.";
     
        private readonly string servicePath = string.Empty;      

        public ServiceBusQueue(ServiceBusNamespace serviceBusNamespace, Microsoft.ServiceBus.NamespaceManager namespaceManager)
            : base(serviceBusNamespace, namespaceManager)
        {
        }

        public IEnumerable<QueueDescription> GetQueues(string filter, int timeoutInSeconds)
        {
            if (NamespaceManager != null)
            {
                if (string.IsNullOrEmpty(ServiceBusNamespace.EntityPath))
                {
                    var taskList = new List<Task>();
                    //Documentation states AND is the only logical clause allowed in the filter (And FYI a maximum of only 3 filter expressions allowed)
                    //https://docs.microsoft.com/en-us/dotnet/api/microsoft.servicebus.namespacemanager.getqueuesasync?view=azure-dotnet#Microsoft_ServiceBus_NamespaceManager_GetQueuesAsync_System_String_
                    //Split on ' OR ' and combine queues returned
                    IEnumerable<QueueDescription> queues = new List<QueueDescription>();
                    var filters = new List<string>();
                    if (string.IsNullOrWhiteSpace(filter))
                    {
                        filters.Add(filter);
                    }
                    else
                    {
                        filters = filter.ToLowerInvariant().Split(new[] { " or " }, StringSplitOptions.None).ToList();
                    }
                    foreach (var splitFilter in filters)
                    {
                        var task = string.IsNullOrWhiteSpace(filter) ? NamespaceManager.GetQueuesAsync() : NamespaceManager.GetQueuesAsync(splitFilter);
                        taskList.Add(task);
                        taskList.Add(Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds)));
                        Task.WaitAny(taskList.ToArray());
                        if (task.IsCompleted)
                        {
                            queues = queues.Union(task.Result);
                            taskList.Clear();
                        }
                        else
                        {
                            throw new TimeoutException();
                        }
                    }
                    return queues;
                }

                return new List<QueueDescription> {
                    GetQueueUsingEntityPath(timeoutInSeconds)
                };
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public QueueDescription GetQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetQueue(path), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public Uri GetQueueUri(string queuePath)
        {
            if (IsCloudNamespace())
            {
                return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, queuePath));
            }
            // ReSharper disable RedundantIfElseBlock
            else
            // ReSharper restore RedundantIfElseBlock
            {
                var uriBuilder = new UriBuilder
                {
                    Host = NamespaceManager.Address.Host,
                    Path = $"{NamespaceManager.Address.AbsolutePath}/{queuePath}",
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        public Uri GetQueueDeadLetterQueueUri(string queuePath)
        {
            if (IsCloudNamespace())
            {
                return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, QueueClient.FormatDeadLetterPath(queuePath)));
            }
            // ReSharper disable RedundantIfElseBlock
            else
            // ReSharper restore RedundantIfElseBlock
            {
                var uriBuilder = new UriBuilder
                {
                    Host = NamespaceManager.Address.Host,
                    Path = $"{NamespaceManager.Address.AbsolutePath}/{QueueClient.FormatDeadLetterPath(queuePath)}",
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        public QueueDescription CreateQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => NamespaceManager.CreateQueue(path), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, QueueCreated, path));
                OnCreated(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public QueueDescription CreateQueue(QueueDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => NamespaceManager.CreateQueue(description), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, QueueCreated, description.Path));
                OnCreated(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public QueueDescription UpdateQueue(QueueDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => NamespaceManager.UpdateQueue(description), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, QueueUpdated, description.Path));
                OnCreated(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public async Task DeleteQueues(IEnumerable<string> queues)
        {
            if (queues == null)
            {
                return;
            }

            await Task.WhenAll(queues.Select(DeleteQueue));
        }

        public async Task DeleteQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteQueueAsync(path), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, QueueDeleted, path));
                OnDeleted(new ServiceBusHelperEventArgs(path, EntityType.Queue));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        public async Task DeleteQueue(QueueDescription queueDescription)
        {
            if (queueDescription == null)
            {
                throw new ArgumentException(QueueDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteQueueAsync(queueDescription.Path), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, QueueDeleted, queueDescription.Path));
                OnDeleted(new ServiceBusHelperEventArgs(queueDescription, EntityType.Queue));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        public QueueDescription RenameQueue(string path, string newPath)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (string.IsNullOrWhiteSpace(newPath))
            {
                throw new ArgumentException(NewPathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var queueDescription = RetryHelper.RetryFunc(() => NamespaceManager.RenameQueue(path, newPath), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, QueueRenamed, path, newPath));
                OnDeleted(new ServiceBusHelperEventArgs(new QueueDescription(path), EntityType.Queue));
                OnCreated(new ServiceBusHelperEventArgs(queueDescription, EntityType.Queue));
                return queueDescription;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        private QueueDescription GetQueueUsingEntityPath(int timeoutInSeconds)
        {
            var taskList = new List<Task>();
            var getQueueTask = NamespaceManager.GetQueueAsync(ServiceBusNamespace.EntityPath);
            taskList.Add(getQueueTask);
            taskList.Add(Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds)));
            Task.WaitAny(taskList.ToArray());
            if (getQueueTask.IsCompleted)
            {
                try
                {
                    return getQueueTask.Result;
                }
                catch (AggregateException ex)
                {
                    throw ex.InnerExceptions.First();
                }
            }
            throw new TimeoutException();
        }
    }
}
