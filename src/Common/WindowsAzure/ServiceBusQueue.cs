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

        protected override EntityType EntityType => EntityType.Queue;

        /// <summary>
        /// Retrieves an enumerable collection of all queues in the service bus namespace.
        /// </summary>
        /// <param name="filter">OData filter.</param>
        /// <returns>Returns an IEnumerable<QueueDescription/> collection of all queues in the service namespace, or
        /// an empty collection if no queue exists in this service namespace.</returns>
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

        /// <summary>
        /// Retrieves the queue from the service namespace.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        /// <returns>A QueueDescription handle to the queue, or null if the queue does not exist in the service namespace. </returns>
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

        /// <summary>
        /// Gets the uri of a queue.
        /// </summary>
        /// <param name="queuePath">The name of a queue.</param>
        /// <returns>The absolute uri of the queue.</returns>
        public Uri GetQueueUri(string queuePath)
        {
            if (IsCloudNamespace())
            {
                return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, queuePath));
            }
            else
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

        /// <summary>
        /// Gets the uri of the deadletter queue for a given queue.
        /// </summary>
        /// <param name="queuePath">The name of a queue.</param>
        /// <returns>the absolute uri of the deadletter queue.</returns>
        public Uri GetQueueDeadLetterQueueUri(string queuePath)
        {
            if (IsCloudNamespace())
            {
                return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, QueueClient.FormatDeadLetterPath(queuePath)));
            }
            else
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

        /// <summary>.
        /// Creates a new queue in the service namespace with the given name.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        /// <returns>Returns a newly-created QueueDescription object.</returns>
        public QueueDescription CreateQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => NamespaceManager.CreateQueue(path), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, QueueCreated, path));
                OnCreated(queue);
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Creates a new queue in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A QueueDescription object describing the attributes with which the new queue will be created.</param>
        /// <returns>Returns a newly-created QueueDescription object.</returns>
        public QueueDescription CreateQueue(QueueDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => NamespaceManager.CreateQueue(description), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, QueueCreated, description.Path));
                OnCreated(queue);
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Updates a queue in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A QueueDescription object describing the attributes with which the new queue will be updated.</param>
        /// <returns>Returns an updated QueueDescription object.</returns>
        public QueueDescription UpdateQueue(QueueDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => NamespaceManager.UpdateQueue(description), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, QueueUpdated, description.Path));
                OnCreated(queue);
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes all the queues in the list.
        /// <param name="queues">A list of queues to delete.</param>
        /// </summary>
        public async Task DeleteQueues(IEnumerable<string> queues)
        {
            if (queues == null)
            {
                return;
            }

            await Task.WhenAll(queues.Select(DeleteQueue));
        }

        /// <summary>
        /// Deletes the queue described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        public async Task DeleteQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteQueueAsync(path), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, QueueDeleted, path));
                OnDeleted(new QueueDescription(path));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Deletes the queue passed as a argument.
        /// </summary>
        /// <param name="queueDescription">The queue to delete.</param>
        public async Task DeleteQueue(QueueDescription queueDescription)
        {
            if (queueDescription == null)
            {
                throw new ArgumentException(QueueDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteQueueAsync(queueDescription.Path), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, QueueDeleted, queueDescription.Path));
                OnDeleted(queueDescription);
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Renames a queue inside a namespace.
        /// </summary>
        /// <param name="path">The path to an existing queue.</param>
        /// <param name="newPath">The new path to the renamed queue.</param>
        /// <returns>Returns a QueueDescription with the new name.</returns>
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
                Log(string.Format(CultureInfo.CurrentCulture, QueueRenamed, path, newPath));
                OnDeleted(new QueueDescription(path));
                OnCreated(queueDescription);
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
