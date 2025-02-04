using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal class ServiceBusConsumerGroup : ServiceBusEntity, IServiceBusConsumerGroup
    {
        private const string EventHubDescriptionCannotBeNull = "The event hub description argument cannot be null.";
        private const string ConsumerGroupDescriptionCannotBeNull = "The consumer group description argument cannot be null.";
        private const string ConsumerGroupCreated = "The consumer group {0} has been successfully created.";
        private const string ConsumerGroupDeleted = "The consumer group {0} has been successfully deleted.";
        private const string ConsumerGroupUpdated = "The consumer group {0} has been successfully updated.";

        private readonly string servicePath = string.Empty;

        public ServiceBusConsumerGroup(ServiceBusNamespace serviceBusNamespace, NamespaceManager namespaceManager) : base(serviceBusNamespace, namespaceManager)
        {
        }

        protected override EntityType EntityType => EntityType.ConsumerGroup;

        /// <summary>
        /// Retrieves the collection of consumer groups of the event hub passed as a parameter.
        /// </summary>
        /// <param name="eventHubPath">The path of a event hub.</param>
        /// <param name="name">The name of a consumer group.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of consumer groups attached to the event hub passed as a parameter.</returns>
        public ConsumerGroupDescription GetConsumerGroup(string eventHubPath, string name)
        {
            if (string.IsNullOrWhiteSpace(eventHubPath))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(NameCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetConsumerGroup(eventHubPath, name), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves the collection of consumer groups of the event hub passed as a parameter.
        /// </summary>
        /// <param name="path">Path of the event hub relative to the service namespace base address.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of consumer groups attached to the event hub passed as a parameter.</returns>
        public IEnumerable<ConsumerGroupDescription> GetConsumerGroups(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetConsumerGroups(path), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves the collection of consumer groups of the event hub passed as a parameter.
        /// </summary>
        /// <param name="description">A event hub belonging to the current service namespace base.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of consumer groups attached to the event hub passed as a parameter.</returns>
        public IEnumerable<ConsumerGroupDescription> GetConsumerGroups(EventHubDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(EventHubDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetConsumerGroups(description.Path), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Creates a new consumer group in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A ConsumerGroupDescription object describing the attributes with which the new consumer group will be created.</param>
        /// <returns>Returns a newly-created ConsumerGroupDescription object.</returns>
        public ConsumerGroupDescription CreateConsumerGroup(ConsumerGroupDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var consumerGroup = RetryHelper.RetryFunc(() => NamespaceManager.CreateConsumerGroup(description), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, ConsumerGroupCreated, description.Name));
                OnCreated(consumerGroup);
                return consumerGroup;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes the consumer group described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="eventHubName">Name of the event hub.</param>
        /// <param name="name">Name of the consumer group.</param>
        public void DeleteConsumerGroup(string eventHubName, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                RetryHelper.RetryAction(() => NamespaceManager.DeleteConsumerGroup(eventHubName, name), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, ConsumerGroupDeleted, name));
                OnDeleted(new ConsumerGroupDescription(eventHubName, name));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Deletes the consumer group described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="consumerGroupDescription">The consumer group to delete.</param>
        public void DeleteConsumerGroup(ConsumerGroupDescription consumerGroupDescription)
        {
            if (string.IsNullOrWhiteSpace(consumerGroupDescription?.Name))
            {
                throw new ArgumentException(ConsumerGroupDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                RetryHelper.RetryAction(() => NamespaceManager.DeleteConsumerGroup(consumerGroupDescription.EventHubPath, consumerGroupDescription.Name), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, ConsumerGroupDeleted, consumerGroupDescription.Name));
                OnDeleted(consumerGroupDescription);
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Deletes all the consumer groups in the list.
        /// <param name="eventHubName">Name of the event hub.</param>
        /// <param name="consumerGroups">A list of consumer groups to delete.</param>
        /// </summary>
        public void DeleteConsumerGroups(string eventHubName, IEnumerable<string> consumerGroups)
        {
            if (consumerGroups == null)
            {
                return;
            }
            foreach (var consumerGroup in consumerGroups)
            {
                DeleteConsumerGroup(eventHubName, consumerGroup);
            }
        }

        /// <summary>
        /// Updates a consumer group in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A ConsumerGroupDescription object describing the attributes with which the new consumer group will be updated.</param>
        /// <returns>Returns an updated ConsumerGroupDescription object.</returns>
        public ConsumerGroupDescription UpdateConsumerGroup(ConsumerGroupDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var consumerGroup = RetryHelper.RetryFunc(() => NamespaceManager.UpdateConsumerGroup(description), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, ConsumerGroupUpdated, description.Name));
                OnCreated(consumerGroup);
                return consumerGroup;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Gets the uri of a consumer group.
        /// </summary>
        /// <param name="eventHubName">Name of the event hub.</param>
        /// <param name="consumerGroupPath">The name of a consumer group.</param>
        /// <returns>The absolute uri of the consumer group.</returns>
        public Uri GetConsumerGroupUri(string eventHubName, string consumerGroupPath)
        {
            return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, eventHubName, "/", consumerGroupPath));
        }
    }
}
