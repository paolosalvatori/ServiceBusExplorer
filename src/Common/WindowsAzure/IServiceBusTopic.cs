// // Auto-added comment

// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Microsoft.ServiceBus.Messaging;

// namespace ServiceBusExplorer.WindowsAzure
// {
//     internal interface IServiceBusTopic : IServiceBusEntity
//     {
//         TopicDescription CreateTopic(TopicDescription topicDescription);

//         Task DeleteTopic(TopicDescription topic);

//         Task DeleteTopics(IEnumerable<string> topics);

//         TopicDescription GetTopic(string path);

//         IEnumerable<TopicDescription> GetTopics(string filter, int timeoutInSeconds);

//         Uri GetTopicUri(string topicPath);

//         TopicDescription RenameTopic(string path, string newPath);

//         TopicDescription UpdateTopic(TopicDescription topicDescription);
//     }
// }
