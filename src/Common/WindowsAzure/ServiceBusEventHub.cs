using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.WindowsAzure
{
    internal class ServiceBusEventHub : ServiceBusEntity, IServiceBusEventHub
    {
        private const string EventHubDescriptionCannotBeNull = "The event hub description argument cannot be null.";
        private const string EventHubCreated = "The event hub {0} has been successfully created.";
        private const string EventHubDeleted = "The event hub {0} has been successfully deleted.";
        private const string EventHubUpdated = "The event hub {0} has been successfully updated.";

        private readonly string servicePath = string.Empty;

        public ServiceBusEventHub(ServiceBusNamespace serviceBusNamespace, NamespaceManager namespaceManager)
            : base(serviceBusNamespace, namespaceManager)
        {
        }

        protected override EntityType EntityType => EntityType.EventHub;

        /// <summary>
        /// Retrieves the event hub from the service namespace.
        /// </summary>
        /// <param name="path">Path of the event hub relative to the service namespace base address.</param>
        /// <returns>A EventHubDescription handle to the event hub, or null if the event hub does not exist in the service namespace. </returns>
        public EventHubDescription GetEventHub(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetEventHub(path), WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Creates a new event hub in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A EventHubDescription object describing the attributes with which the new event hub will be created.</param>
        /// <returns>Returns a newly-created EventHubDescription object.</returns>
        public EventHubDescription CreateEventHub(EventHubDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var eventHub = RetryHelper.RetryFunc(() => NamespaceManager.CreateEventHub(description), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, EventHubCreated, description.Path));
                OnCreated(eventHub);
                return eventHub;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes the event hub described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="eventHubDescription">The event hub to delete.</param>
        public void DeleteEventHub(EventHubDescription eventHubDescription)
        {
            if (string.IsNullOrWhiteSpace(eventHubDescription?.Path))
            {
                throw new ArgumentException(EventHubDescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                RetryHelper.RetryAction(() => NamespaceManager.DeleteEventHubAsync(eventHubDescription.Path), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, EventHubDeleted, eventHubDescription.Path));
                OnDeleted(eventHubDescription);
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Deletes all the event hubs in the list.
        /// <param name="eventHubs">A list of event hubs to delete.</param>
        /// </summary>
        public void DeleteEventHubs(IEnumerable<string> eventHubs)
        {
            if (eventHubs == null)
            {
                return;
            }
            foreach (var eventHub in eventHubs)
            {
                DeleteEventHub(eventHub);
            }
        }

        /// <summary>
        /// Updates a event hub in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A EventHubDescription object describing the attributes with which the new event hub will be updated.</param>
        /// <returns>Returns an updated EventHubDescription object.</returns>
        public EventHubDescription UpdateEventHub(EventHubDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager == null)
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
            var eventHub = RetryHelper.RetryFunc(() => NamespaceManager.UpdateEventHub(description), WriteToLog);
            Log(string.Format(CultureInfo.CurrentCulture, EventHubUpdated, description.Path));
            OnCreated(eventHub);
            return eventHub;
        }

        /// <summary>
        /// Gets the uri of a event hub.
        /// </summary>
        /// <param name="eventHubPath">The path of a event hub.</param>
        /// <returns>The absolute uri of the event hub.</returns>
        public Uri GetEventHubUri(string eventHubPath)
        {
            return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, eventHubPath));
        }

        private void DeleteEventHub(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                RetryHelper.RetryAction(() => NamespaceManager.DeleteEventHub(path), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, EventHubDeleted, path));
                OnDeleted(new ServiceBusHelperEventArgs(path, EntityType.EventHub));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

    }
}
