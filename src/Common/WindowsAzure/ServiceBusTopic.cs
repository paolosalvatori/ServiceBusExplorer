// // Auto-added comment

// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.ServiceBus.Messaging;
// using ServiceBusExplorer.Enums;
// using ServiceBusExplorer.Helpers;

// namespace ServiceBusExplorer.WindowsAzure
// {
//     internal class ServiceBusTopic : ServiceBusEntity, IServiceBusTopic
//     {
//         private const string TopicDescriptionCannotBeNull = "The topic decsription argument cannot be null.";
//         private const string TopicCreated = "The topic {0} has been successfully created.";
//         private const string TopicDeleted = "The topic {0} has been successfully deleted.";
//         private const string TopicRenamed = "The topic {0} has been successfully renamed to {1}.";
//         private const string TopicUpdated = "The topic {0} has been successfully updated.";

//         private readonly string servicePath = string.Empty;

//         public ServiceBusTopic(ServiceBusNamespace serviceBusNamespace, Microsoft.ServiceBus.NamespaceManager namespaceManager)
//             : base(serviceBusNamespace, namespaceManager)
//         {
//         }

//         protected override EntityType EntityType => EntityType.Topic;

//         /// <summary>
//         /// Retrieves the topic from the service namespace.
//         /// </summary>
//         /// <param name="path">Path of the topic relative to the service namespace base address.</param>
//         /// <returns>A TopicDescription handle to the topic, or null if the topic does not exist in the service namespace.</returns>
//         public TopicDescription GetTopic(string path)
//         {
//             if (string.IsNullOrWhiteSpace(path))
//             {
//                 throw new ArgumentException(PathCannotBeNull);
//             }

//             if (NamespaceManager != null)
//             {
//                 return RetryHelper.RetryFunc(() => NamespaceManager.GetTopic(path), WriteToLog);
//             }

//             throw new ApplicationException(ServiceBusIsDisconnected);
//         }

//         /// <summary>
//         /// Retrieves an enumerable collection of all topics in the service bus namespace.
//         /// </summary>
//         /// <param name="filter">OData filter.</param>
//         /// <returns>Returns an IEnumerable<TopicDescription/> collection of all topics in the service namespace.
//         ///          Returns an empty collection if no topic exists in this service namespace.</returns>
//         public IEnumerable<TopicDescription> GetTopics(string filter, int timeoutInSeconds)
//         {
//             if (NamespaceManager != null)
//             {
//                 //Documentation states AND is the only logical clause allowed in the filter
//                 //https://docs.microsoft.com/en-us/dotnet/api/microsoft.servicebus.namespacemanager.gettopicsasync?view=azure-dotnet
//                 //Split on ' OR ' and combine queues returned
//                 var taskList = new List<Task>();
//                 if (string.IsNullOrEmpty(ServiceBusNamespace.EntityPath))
//                 {
//                     IEnumerable<TopicDescription> topics = new List<TopicDescription>();
//                     var filters = new List<string>();
//                     if (string.IsNullOrWhiteSpace(filter))
//                     {
//                         filters.Add(filter);
//                     }
//                     else
//                     {
//                         filters = filter.ToLowerInvariant().Split(new[] { " or " }, StringSplitOptions.None).ToList();
//                     }

//                     foreach (var splitFilter in filters)
//                     {
//                         var task = string.IsNullOrWhiteSpace(filter) ? NamespaceManager.GetTopicsAsync() : NamespaceManager.GetTopicsAsync(splitFilter);
//                         taskList.Add(task);
//                         taskList.Add(Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds)));
//                         Task.WaitAny(taskList.ToArray());
//                         if (task.IsCompleted)
//                         {
//                             topics = topics.Union(task.Result);
//                             taskList.Clear();
//                         }
//                         else
//                         {
//                             throw new TimeoutException();
//                         }
//                     }
//                     return topics;
//                 }

//                 return new List<TopicDescription> {
//                     GetTopicUsingEntityPath(timeoutInSeconds)
//                 };
//             }
//             throw new ApplicationException(ServiceBusIsDisconnected);
//         }

//         /// <summary>
//         /// Gets the uri of a topic.
//         /// </summary>
//         /// <param name="topicPath">The name of a topic.</param>
//         /// <returns>The absolute uri of the topic.</returns>
//         public Uri GetTopicUri(string topicPath)
//         {
//             if (IsCloudNamespace())
//             {
//                 return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, topicPath));
//             }
//             else
//             {
//                 var uriBuilder = new UriBuilder
//                 {
//                     Host = NamespaceManager.Address.Host,
//                     Path = $"{NamespaceManager.Address.AbsolutePath}/{topicPath}",
//                     Scheme = "sb",
//                 };
//                 return uriBuilder.Uri;
//             }
//         }

//         /// <summary>
//         /// Creates a new topic in the service namespace with the given name.
//         /// </summary>
//         /// <param name="topicDescription">A TopicDescription object describing the attributes with which the new topic will be created.</param>
//         /// <returns>Returns a newly-created TopicDescription object.</returns>
//         public TopicDescription CreateTopic(TopicDescription topicDescription)
//         {
//             if (topicDescription == null)
//             {
//                 throw new ArgumentException(TopicDescriptionCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 var topic = RetryHelper.RetryFunc(() => NamespaceManager.CreateTopic(topicDescription), WriteToLog);
//                 Log(string.Format(CultureInfo.CurrentCulture, TopicCreated, topicDescription.Path));
//                 OnCreated(topic);
//                 return topic;
//             }
//             throw new ApplicationException(ServiceBusIsDisconnected);
//         }

//         /// <summary>
//         /// Updates a topic in the service namespace with the given name.
//         /// </summary>
//         /// <param name="topicDescription">A TopicDescription object describing the attributes with which the new topic will be updated.</param>
//         /// <returns>Returns an updated TopicDescription object.</returns>
//         public TopicDescription UpdateTopic(TopicDescription topicDescription)
//         {
//             if (topicDescription == null)
//             {
//                 throw new ArgumentException(TopicDescriptionCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 var topic = RetryHelper.RetryFunc(() => NamespaceManager.UpdateTopic(topicDescription), WriteToLog);
//                 Log(string.Format(CultureInfo.CurrentCulture, TopicUpdated, topicDescription.Path));
//                 OnCreated(topic);
//                 return topic;
//             }
//             throw new ApplicationException(ServiceBusIsDisconnected);
//         }

//         /// <summary>
//         /// Deletes all the topics in the list.
//         /// <param name="topics">A list of topics to delete.</param>
//         /// </summary>
//         public async Task DeleteTopics(IEnumerable<string> topics)
//         {
//             if (topics == null)
//             {
//                 return;
//             }

//             await Task.WhenAll(topics.Select(DeleteTopic));
//         }

//         /// <summary>
//         /// Deletes the topic passed as a argument.
//         /// </summary>
//         /// <param name="topic">The topic to delete.</param>
//         public async Task DeleteTopic(TopicDescription topic)
//         {
//             if (topic == null)
//             {
//                 throw new ArgumentException(TopicDescriptionCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteTopicAsync(topic.Path), WriteToLog);
//                 Log(string.Format(CultureInfo.CurrentCulture, TopicDeleted, topic.Path));
//                 OnDeleted(topic);
//             }
//             else
//             {
//                 throw new ApplicationException(ServiceBusIsDisconnected);
//             }
//         }

//         /// <summary>
//         /// Renames a topic inside a namespace.
//         /// </summary>
//         /// <param name="path">The path to an existing topic.</param>
//         /// <param name="newPath">The new path to the renamed topic.</param>
//         /// <returns>Returns a TopicDescription with the new name.</returns>
//         public TopicDescription RenameTopic(string path, string newPath)
//         {
//             if (string.IsNullOrWhiteSpace(path))
//             {
//                 throw new ArgumentException(PathCannotBeNull);
//             }
//             if (string.IsNullOrWhiteSpace(newPath))
//             {
//                 throw new ArgumentException(NewPathCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 var topicDescription = RetryHelper.RetryFunc(() => NamespaceManager.RenameTopic(path, newPath), WriteToLog);
//                 Log(string.Format(CultureInfo.CurrentCulture, TopicRenamed, path, newPath));
//                 OnDeleted(new TopicDescription(path));
//                 OnCreated(topicDescription);
//                 return topicDescription;
//             }
//             throw new ApplicationException(ServiceBusIsDisconnected);
//         }

//         private TopicDescription GetTopicUsingEntityPath(int timeoutInSeconds)
//         {
//             var taskList = new List<Task>();
//             var getTopicTask = NamespaceManager.GetTopicAsync(ServiceBusNamespace.EntityPath);
//             taskList.Add(getTopicTask);
//             taskList.Add(Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds)));
//             Task.WaitAny(taskList.ToArray());
//             if (getTopicTask.IsCompleted)
//             {
//                 try
//                 {
//                     return getTopicTask.Result;
//                 }
//                 catch (AggregateException ex)
//                 {
//                     throw ex.InnerExceptions.First();
//                 }
//             }
//             throw new TimeoutException();
//         }

//         private async Task DeleteTopic(string path)
//         {
//             if (string.IsNullOrWhiteSpace(path))
//             {
//                 throw new ArgumentException(PathCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteTopicAsync(path), WriteToLog);
//                 Log(string.Format(CultureInfo.CurrentCulture, TopicDeleted, path));
//                 OnDeleted(new TopicDescription(path));
//             }
//             else
//             {
//                 throw new ApplicationException(ServiceBusIsDisconnected);
//             }
//         }
//     }
// }
