using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusTopic : IServiceBusEntity
    {
        /// <summary>
        /// Creates a new topic in the service namespace with the given name.
        /// </summary>
        /// <param name="path">Path of the topic relative to the service namespace base address.</param>
        /// <returns>Returns a newly-created TopicDescription object.</returns>    
        TopicDescription CreateTopic(string path);

        /// <summary>
        /// Creates a new topic in the service namespace with the given name.
        /// </summary>
        /// <param name="topicDescription">A TopicDescription object describing the attributes with which the new topic will be created.</param>
        /// <returns>Returns a newly-created TopicDescription object.</returns>
        TopicDescription CreateTopic(TopicDescription topicDescription);

        /// <summary>
        /// Deletes the topic described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="path">Path of the topic relative to the service namespace base address.</param>
        Task DeleteTopic(string path);

        /// <summary>
        /// Deletes the topic passed as a argument.
        /// </summary>
        /// <param name="topic">The topic to delete.</param>
        Task DeleteTopic(TopicDescription topic);


        /// <summary>
        /// Deletes all the topics in the list.
        /// <param name="topics">A list of topics to delete.</param>
        /// </summary>
        Task DeleteTopics(IEnumerable<string> topics);

        /// <summary>
        /// Retrieves the topic from the service namespace.
        /// </summary>
        /// <param name="path">Path of the topic relative to the service namespace base address.</param>
        /// <returns>A TopicDescription handle to the topic, or null if the topic does not exist in the service namespace.</returns>
        TopicDescription GetTopic(string path);

        /// <summary>
        /// Retrieves an enumerable collection of all topics in the service bus namespace.
        /// </summary>
        /// <param name="filter">OData filter.</param>
        /// <returns>Returns an IEnumerable<TopicDescription/> collection of all topics in the service namespace.
        ///          Returns an empty collection if no topic exists in this service namespace.</returns>
        IEnumerable<TopicDescription> GetTopics(string filter, int timeoutInSeconds);

        /// <summary>
        /// Gets the uri of a topic.
        /// </summary>
        /// <param name="topicPath">The name of a topic.</param>
        /// <returns>The absolute uri of the topic.</returns>
        Uri GetTopicUri(string topicPath);

        /// <summary>
        /// Renames a topic inside a namespace.
        /// </summary>
        /// <param name="path">The path to an existing topic.</param>
        /// <param name="newPath">The new path to the renamed topic.</param>
        /// <returns>Returns a TopicDescription with the new name.</returns>
        TopicDescription RenameTopic(string path, string newPath);

        /// <summary>
        /// Updates a topic in the service namespace with the given name.
        /// </summary>
        /// <param name="topicDescription">A TopicDescription object describing the attributes with which the new topic will be updated.</param>
        /// <returns>Returns an updated TopicDescription object.</returns>
        TopicDescription UpdateTopic(TopicDescription topicDescription);
    }
}
