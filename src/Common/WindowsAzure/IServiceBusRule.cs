using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal interface IServiceBusRule : IServiceBusEntity
    {
        RuleDescription AddRule(SubscriptionDescription subscriptionDescription, RuleDescription ruleDescription);

        IEnumerable<RuleDescription> GetRules(string topicPath, string name);

        IEnumerable<RuleDescription> GetRules(SubscriptionDescription subscription);

        void RemoveRule(SubscriptionDescription subscriptionDescription, RuleDescription rule);

        void RemoveRules(IEnumerable<RuleWrapper> wrappers);
    }
}
