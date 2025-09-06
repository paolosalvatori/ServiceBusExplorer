using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal class ServiceBusSubscription : ServiceBusEntity, IServiceBusSubscription
    {
        private const string SubscriptionDescriptionCannotBeNull = "The subscription description argument cannot be null.";
        private const string SubscriptionCreated = "The {0} subscription for the {1} topic has been successfully created.";
        private const string SubscriptionDeleted = "The {0} subscription for the {1} topic has been successfully deleted.";
        private const string SubscriptionUpdated = "The {0} subscription for the {1} topic has been successfully updated.";
        private const string TopicDescriptionCannotBeNull = "The topic decsription argument cannot be null.";
        private const string RuleDescriptionCannotBeNull = "The rule description argument cannot be null.";

        private readonly string servicePath = string.Empty;

        public ServiceBusSubscription(ServiceBusNamespace serviceBusNamespace, NamespaceManager namespaceManager) : base(serviceBusNamespace, namespaceManager)
        {
        }

        protected override EntityType EntityType => EntityType.Subscription;

        /// <summary>
        /// Gets a subscription attached to the topic passed a parameter.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <param name="name">The name of the subscription to get.</param>
        /// <returns>Returns the subscription with the specified name.</returns>
        public SubscriptionDescription GetSubscription(string topicPath, string name)
        {
            if (NamespaceManager == null)
            {
                throw new ArgumentException(NamespaceManagerCannotBeNull);
            }
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
                return RetryHelper.RetryFunc(() => NamespaceManager.GetSubscription(topicPath, name), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerated collection of subscriptions attached to the topic whose name is passed as a parameter.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of subscriptions attached to the topic passed as a parameter.</returns>
        public IEnumerable<SubscriptionDescription> GetSubscriptions(string topicPath)
        {
            if (string.IsNullOrWhiteSpace(topicPath))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetSubscriptions(topicPath), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerated collection of subscriptions attached to the topic passed as a parameter.
        /// </summary>
        /// <param name="topic">A topic belonging to the current service namespace base.</param>
        /// <param name="filter">OData filter.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of subscriptions attached to the topic passed as a parameter.</returns>
        public IEnumerable<SubscriptionDescription> GetSubscriptions(TopicDescription topic, string filter)
        {
            if (topic == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => string.IsNullOrWhiteSpace(filter) ?
                                                   NamespaceManager.GetSubscriptions(topic.Path) :
                                                   NamespaceManager.GetSubscriptions(topic.Path, filter), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Gets the uri of a subscription.
        /// </summary>
        /// <param name="topicPath">The name of the topic.</param>
        /// <param name="name">The name of a subscription.</param>
        /// <returns>The absolute uri of the subscription.</returns>
        public Uri GetSubscriptionUri(string topicPath, string name)
        {
            if (IsCloudNamespace())
            {
                return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, SubscriptionClient.FormatSubscriptionPath(topicPath, name)));
            }
            else
            {
                var uriBuilder = new UriBuilder
                {
                    Host = NamespaceManager.Address.Host,
                    Path = $"{NamespaceManager.Address.AbsolutePath}/{SubscriptionClient.FormatSubscriptionPath(topicPath, name)}",
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        /// <summary>
        /// Gets the uri of the deadletter queue for a given subscription.
        /// </summary>
        /// <param name="topicPath">The name of a topic.</param>
        /// <param name="name">The name of a subscription.</param>
        /// <returns>The absolute uri of the deadletter queue.</returns>
        public Uri GetSubscriptionDeadLetterQueueUri(string topicPath, string name)
        {
            if (IsCloudNamespace())
            {
                return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, SubscriptionClient.FormatDeadLetterPath(topicPath, name));
            }
            else
            {
                var uriBuilder = new UriBuilder
                {
                    Host = NamespaceManager.Address.Host,
                    Path = $"{NamespaceManager.Address.AbsolutePath}/{SubscriptionClient.FormatDeadLetterPath(topicPath, name)}",
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        /// <summary>
        /// Adds a subscription to this topic, with a default pass-through filter added.
        /// </summary>
        /// <param name="topicDescription">A topic belonging to the current service namespace base.</param>
        /// <param name="subscriptionDescription">Metadata of the subscription to be created.</param>
        /// <returns>Returns a newly-created SubscriptionDescription object.</returns>
        public SubscriptionDescription CreateSubscription(TopicDescription topicDescription, SubscriptionDescription subscriptionDescription)
        {
            if (topicDescription == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (subscriptionDescription == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            var subscription = RetryHelper.RetryFunc(() => NamespaceManager.CreateSubscription(subscriptionDescription), WriteToLog);
            Log(string.Format(CultureInfo.CurrentCulture, SubscriptionCreated, subscription.Name, topicDescription.Path));
            OnCreated(new ServiceBusHelperEventArgs(new SubscriptionWrapper(subscription, topicDescription), EntityType.Subscription));
            return subscription;
        }

        /// <summary>
        /// Adds a subscription to this topic, with a default pass-through filter added.
        /// </summary>
        /// <param name="topicDescription">A topic belonging to the current service namespace base.</param>
        /// <param name="subscriptionDescription">Metadata of the subscription to be created.</param>
        /// <param name="ruleDescription">The metadata describing the properties of the rule to be associated with the subscription.</param>
        /// <returns>Returns a newly-created SubscriptionDescription object.</returns>
        public SubscriptionDescription CreateSubscription(TopicDescription topicDescription,
                                                          SubscriptionDescription subscriptionDescription,
                                                          RuleDescription ruleDescription)
        {
            if (topicDescription == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (subscriptionDescription == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (ruleDescription == null)
            {
                throw new ArgumentException(RuleDescriptionCannotBeNull);
            }
            var subscription = RetryHelper.RetryFunc(() => NamespaceManager.CreateSubscription(subscriptionDescription, ruleDescription), WriteToLog);
            Log(string.Format(CultureInfo.CurrentCulture, SubscriptionCreated, subscription.Name, topicDescription.Path));
            OnCreated(new ServiceBusHelperEventArgs(new SubscriptionWrapper(subscription, topicDescription), EntityType));
            return subscription;
        }

        /// <summary>
        /// Updates a subscription to this topic.
        /// </summary>
        /// <param name="topicDescription">A topic belonging to the current service namespace base.</param>
        /// <param name="subscriptionDescription">Metadata of the subscription to be updated.</param>
        /// <returns>Returns an updated SubscriptionDescription object.</returns>
        public SubscriptionDescription UpdateSubscription(TopicDescription topicDescription, SubscriptionDescription subscriptionDescription)
        {
            if (topicDescription == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (subscriptionDescription == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            var subscription = RetryHelper.RetryFunc(() => NamespaceManager.UpdateSubscription(subscriptionDescription), WriteToLog);
            Log(string.Format(CultureInfo.CurrentCulture, SubscriptionUpdated, subscription.Name, topicDescription.Path));
            //OnCreate(new ServiceBusHelperEventArgs(new SubscriptionWrapper(subscription, topicDescription), EntityType.Subscription));
            return subscription;
        }

        /// <summary>
        /// Removes the subscriptions contained in the list passed as a argument.
        /// </summary>
        /// <param name="subscriptionDescriptions">The list containing subscriptions to remove.</param>
        public Task DeleteSubscriptions(IEnumerable<SubscriptionDescription> subscriptionDescriptions)
        {
            if (subscriptionDescriptions == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }

            return Task.WhenAll(subscriptionDescriptions.Select(DeleteSubscription));
        }

        /// <summary>
        /// Removes the subscription passed as a argument.
        /// </summary>
        /// <param name="subscriptionDescription">The subscription to remove.</param>
        public async Task DeleteSubscription(SubscriptionDescription subscriptionDescription)
        {
            if (subscriptionDescription == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }
            await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteSubscriptionAsync(subscriptionDescription.TopicPath, subscriptionDescription.Name), WriteToLog);
            Log(string.Format(CultureInfo.CurrentCulture, SubscriptionDeleted, subscriptionDescription.Name, subscriptionDescription.TopicPath));
            OnDeleted(subscriptionDescription);
        }
    }
}
