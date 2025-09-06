﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal class ServiceBusRule : ServiceBusEntity, IServiceBusRule
    {
        private const string SubscriptionDescriptionCannotBeNull = "The subscription description argument cannot be null.";
        private const string RuleDescriptionCannotBeNull = "The rule description argument cannot be null.";
        private const string RuleCannotBeNull = "The rule argument cannot be null.";
        private const string RuleCreated = "The {0} rule for the {1} subscription has been successfully created.";
        private const string RuleDeleted = "The {0} rule for the {1} subscription has been successfully deleted.";

        private MessagingFactory messagingFactory;

        public ServiceBusRule(ServiceBusNamespace serviceBusNamespace, NamespaceManager namespaceManager)
            : base(serviceBusNamespace, namespaceManager)
        {
        }

        protected override EntityType EntityType => EntityType.Rule;

        /// <summary>
        /// Retrieves an enumerated collection of rules attached to the subscription passed as a parameter.
        /// </summary>
        /// <param name="subscription">A subscription belonging to the current service namespace base.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of rules attached to the subscription passed as a parameter.</returns>
        public IEnumerable<RuleDescription> GetRules(SubscriptionDescription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetRules(subscription.TopicPath, subscription.Name), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerated collection of rules attached to the subscription passed as a parameter.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <param name="name">The name of a subscription belonging to the topic passed as a parameter.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of rules attached to the subscription passed as a parameter.</returns>
        public IEnumerable<RuleDescription> GetRules(string topicPath, string name)
        {
            if (string.IsNullOrWhiteSpace(topicPath))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(NameCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetRules(topicPath, name), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Adds a rule to this subscription, with a default pass-through filter added.
        /// </summary>
        /// <param name="subscriptionDescription">The subscription to add the rule to.</param>
        /// <param name="ruleDescription">Metadata of the rule to be created.</param>
        /// <returns>Returns a newly-created RuleDescription object.</returns>
        public RuleDescription AddRule(SubscriptionDescription subscriptionDescription, RuleDescription ruleDescription)
        {
            if (subscriptionDescription == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }
            if (ruleDescription == null)
            {
                throw new ArgumentException(RuleDescriptionCannotBeNull);
            }
            var subscriptionClient = GetMessagingFactory().CreateSubscriptionClient(subscriptionDescription.TopicPath,
                                                                               subscriptionDescription.Name);
            RetryHelper.RetryAction(() => subscriptionClient.AddRule(ruleDescription), WriteToLog);
            var func = (() => NamespaceManager.GetRules(subscriptionDescription.TopicPath, subscriptionDescription.Name));
            var rules = RetryHelper.RetryFunc(func, WriteToLog);
            var rule = rules.FirstOrDefault(r => r.Name == ruleDescription.Name);
            Log(string.Format(CultureInfo.CurrentCulture, RuleCreated, ruleDescription.Name, subscriptionDescription.Name));
            OnCreated(new ServiceBusHelperEventArgs(new RuleWrapper(rule, subscriptionDescription), EntityType));
            return rule;
        }

        /// <summary>
        /// Removes the rules contained in the list passed as a argument.
        /// </summary>
        /// <param name="wrappers">The list containing the ruleWrappers of the rules to remove.</param>
        public void RemoveRules(IEnumerable<RuleWrapper> wrappers)
        {
            if (wrappers == null)
            {
                throw new ArgumentException(RuleDescriptionCannotBeNull);
            }
            foreach (var wrapper in wrappers)
            {
                RemoveRule(wrapper.SubscriptionDescription, wrapper.RuleDescription);
            }
        }

        /// <summary>
        /// Removes the rule passed as a argument.
        /// </summary>
        /// <param name="subscriptionDescription">A subscription belonging to the current service namespace base.</param>
        /// <param name="rule">The rule to remove.</param>
        public void RemoveRule(SubscriptionDescription subscriptionDescription, RuleDescription rule)
        {
            if (subscriptionDescription == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }
            if (rule == null)
            {
                throw new ArgumentException(RuleCannotBeNull);
            }
            var subscriptionClient = GetMessagingFactory().CreateSubscriptionClient(subscriptionDescription.TopicPath,
                                                                               subscriptionDescription.Name);
            RetryHelper.RetryAction(() => subscriptionClient.RemoveRule(rule.Name), WriteToLog);
            Log(string.Format(CultureInfo.CurrentCulture, RuleDeleted, rule.Name, subscriptionClient.Name));
            OnDeleted(new ServiceBusHelperEventArgs(new RuleWrapper(rule, subscriptionDescription), EntityType.Rule));
        }

        private MessagingFactory GetMessagingFactory()
        {
            if (messagingFactory == null || messagingFactory.IsClosed)
            {
                var builder = new ServiceBusConnectionStringBuilder(ServiceBusNamespace.ConnectionString)
                {
                    EntityPath = string.Empty
                };
                messagingFactory = MessagingFactory.CreateFromConnectionString(builder.ToString());
            }

            return messagingFactory;
        }
    }
}
