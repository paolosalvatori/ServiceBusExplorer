using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal sealed class ServiceBusTopic : ServiceBusEntity, IServiceBusTopic
    {
        private const string TopicDescriptionCannotBeNull = "The topic decsription argument cannot be null.";
        private const string TopicCreated = "The topic {0} has been successfully created.";
        private const string TopicDeleted = "The topic {0} has been successfully deleted.";
        private const string TopicRenamed = "The topic {0} has been successfully renamed to {1}.";
        private const string TopicUpdated = "The topic {0} has been successfully updated.";

        private readonly string servicePath = string.Empty;

        public ServiceBusTopic(ServiceBusNamespace serviceBusNamespace, Microsoft.ServiceBus.NamespaceManager namespaceManager)
            : base(serviceBusNamespace, namespaceManager)
        {
        }

        public TopicDescription GetTopic(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }

            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetTopic(path), WriteToLog);
            }

            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public IEnumerable<TopicDescription> GetTopics(string filter, int timeoutInSeconds)
        {
            if (NamespaceManager != null)
            {
                //Documentation states AND is the only logical clause allowed in the filter
                //https://docs.microsoft.com/en-us/dotnet/api/microsoft.servicebus.namespacemanager.gettopicsasync?view=azure-dotnet
                //Split on ' OR ' and combine queues returned
                var taskList = new List<Task>();
                if (string.IsNullOrEmpty(ServiceBusNamespace.EntityPath))
                {
                    IEnumerable<TopicDescription> topics = new List<TopicDescription>();
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
                        var task = string.IsNullOrWhiteSpace(filter) ? NamespaceManager.GetTopicsAsync() : NamespaceManager.GetTopicsAsync(splitFilter);
                        taskList.Add(task);
                        taskList.Add(Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds)));
                        Task.WaitAny(taskList.ToArray());
                        if (task.IsCompleted)
                        {
                            topics = topics.Union(task.Result);
                            taskList.Clear();
                        }
                        else
                        {
                            throw new TimeoutException();
                        }
                    }
                    return topics;
                }

                return new List<TopicDescription> {
                    GetTopicUsingEntityPath(timeoutInSeconds)
                };
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public Uri GetTopicUri(string topicPath)
        {
            if (IsCloudNamespace())
            {
                return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, topicPath));
            }
            // ReSharper disable RedundantIfElseBlock
            else
            // ReSharper restore RedundantIfElseBlock
            {
                var uriBuilder = new UriBuilder
                {
                    Host = NamespaceManager.Address.Host,
                    Path = $"{NamespaceManager.Address.AbsolutePath}/{topicPath}",
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        public TopicDescription CreateTopic(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var topic = RetryHelper.RetryFunc(() => NamespaceManager.CreateTopic(path), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, TopicCreated, path));
                OnCreated(new ServiceBusHelperEventArgs(topic, EntityType.Topic));
                return topic;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public TopicDescription CreateTopic(TopicDescription topicDescription)
        {
            if (topicDescription == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var topic = RetryHelper.RetryFunc(() => NamespaceManager.CreateTopic(topicDescription), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, TopicCreated, topicDescription.Path));
                OnCreated(new ServiceBusHelperEventArgs(topic, EntityType.Topic));
                return topic;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public TopicDescription UpdateTopic(TopicDescription topicDescription)
        {
            if (topicDescription == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var topic = RetryHelper.RetryFunc(() => NamespaceManager.UpdateTopic(topicDescription), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, TopicUpdated, topicDescription.Path));
                OnCreated(new ServiceBusHelperEventArgs(topic, EntityType.Topic));
                return topic;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        public async Task DeleteTopics(IEnumerable<string> topics)
        {
            if (topics == null)
            {
                return;
            }

            await Task.WhenAll(topics.Select(DeleteTopic));
        }

        public async Task DeleteTopic(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteTopicAsync(path), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, TopicDeleted, path));
                OnDeleted(new ServiceBusHelperEventArgs(path, EntityType.Topic));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        public async Task DeleteTopic(TopicDescription topic)
        {
            if (topic == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteTopicAsync(topic.Path), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, TopicDeleted, topic.Path));
                OnDeleted(new ServiceBusHelperEventArgs(topic, EntityType.Topic));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        public TopicDescription RenameTopic(string path, string newPath)
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
                var topicDescription = RetryHelper.RetryFunc(() => NamespaceManager.RenameTopic(path, newPath), WriteToLog);
                WriteToLog?.Invoke(string.Format(CultureInfo.CurrentCulture, TopicRenamed, path, newPath));
                OnDeleted(new ServiceBusHelperEventArgs(new TopicDescription(path), EntityType.Topic));
                OnCreated(new ServiceBusHelperEventArgs(topicDescription, EntityType.Topic));
                return topicDescription;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        private TopicDescription GetTopicUsingEntityPath(int timeoutInSeconds)
        {
            var taskList = new List<Task>();
            var getTopicTask = NamespaceManager.GetTopicAsync(ServiceBusNamespace.EntityPath);
            taskList.Add(getTopicTask);
            taskList.Add(Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds)));
            Task.WaitAny(taskList.ToArray());
            if (getTopicTask.IsCompleted)
            {
                try
                {
                    return getTopicTask.Result;
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
