using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusSubscription : IServiceBusEntity
    {
        SubscriptionDescription CreateSubscription(TopicDescription topicDescription, SubscriptionDescription subscriptionDescription);

        SubscriptionDescription CreateSubscription(TopicDescription topicDescription, SubscriptionDescription subscriptionDescription, RuleDescription ruleDescription);

        void DeleteSubscription(string topicPath, string name);

        Task DeleteSubscription(SubscriptionDescription subscriptionDescription);

        Task DeleteSubscriptions(IEnumerable<SubscriptionDescription> subscriptionDescriptions);

        SubscriptionDescription GetSubscription(string topicPath, string name);

        Uri GetSubscriptionDeadLetterQueueUri(string topicPath, string name);

        IEnumerable<SubscriptionDescription> GetSubscriptions(string topicPath);

        IEnumerable<SubscriptionDescription> GetSubscriptions(string topicPath, string filter);

        IEnumerable<SubscriptionDescription> GetSubscriptions(TopicDescription topic);

        IEnumerable<SubscriptionDescription> GetSubscriptions(TopicDescription topic, string filter);

        Uri GetSubscriptionUri(string topicPath, string name);

        SubscriptionDescription UpdateSubscription(TopicDescription topicDescription, SubscriptionDescription subscriptionDescription);
    }
}
