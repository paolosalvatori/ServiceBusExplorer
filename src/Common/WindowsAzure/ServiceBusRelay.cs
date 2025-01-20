using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBusExplorer.WindowsAzure
{
    internal class ServiceBusRelay : ServiceBusEntity, IServiceBusRelay
    {
        private const string RelayCreated = "The relay {0} has been successfully created.";
        private const string RelayDeleted = "The relay {0} has been successfully deleted.";
        private const string RelayUpdated = "The relay {0} has been successfully updated.";

        private readonly string servicePath = string.Empty;

        public ServiceBusRelay(ServiceBusNamespace serviceBusNamespace, Microsoft.ServiceBus.NamespaceManager namespaceManager)
            : base(serviceBusNamespace, namespaceManager)
        {
        }

        protected override EntityType EntityType => EntityType.Relay;

        /// <summary>
        /// Retrieves the relay from the service namespace.
        /// </summary>
        /// <param name="path">Path of the relay relative to the service namespace base address.</param>
        /// <returns>A RelayDescription handle to the relay, or null if the relay does not exist in the service namespace. </returns>
        public RelayDescription GetRelay(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => NamespaceManager.GetRelayAsync(path).Result, WriteToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all relays in the service bus namespace.
        /// </summary>
        /// <returns>Returns an IEnumerable<RelayDescription/> collection of all relays in the service namespace.
        ///          Returns an empty collection if no relay exists in this service namespace.</returns>
        public IEnumerable<RelayDescription> GetRelays(int timeoutInSeconds)
        {
            if (NamespaceManager != null)
            {
                var taskList = new List<Task>();
                var task = NamespaceManager.GetRelaysAsync();
                taskList.Add(task);
                taskList.Add(Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds)));
                Task.WaitAny(taskList.ToArray());
                if (task.IsCompleted)
                {
                    try
                    {
                        return task.Result;
                    }
                    catch (AggregateException ex)
                    {
                        throw ex.InnerExceptions.First();
                    }
                }
                throw new TimeoutException();
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Creates a new relay in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A RelayDescription object describing the attributes with which the new event hub will be created.</param>
        /// <returns>Returns a newly-created RelayDescription object.</returns>
        public RelayDescription CreateRelay(RelayDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var relayService = RetryHelper.RetryFunc(() => NamespaceManager.CreateRelayAsync(description).Result, WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, RelayCreated, description.Path));
                OnCreated(relayService);
                return relayService;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes the relay described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="path">Path of the relay relative to the service namespace base address.</param>
        public async Task DeleteRelay(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                await RetryHelper.RetryActionAsync(() => NamespaceManager.DeleteRelayAsync(path), WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, RelayDeleted, path));
                OnDeleted(new ServiceBusHelperEventArgs(path, EntityType));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Deletes all the relays in the list.
        /// <param name="relayServices">A list of relays to delete.</param>
        /// </summary>
        public async Task DeleteRelays(IEnumerable<string> relayServices)
        {
            if (relayServices == null)
            {
                return;
            }
            await Task.WhenAll(relayServices.Select(DeleteRelay));
        }

        /// <summary>
        /// Updates a relay in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A RelayDescription object describing the attributes with which the new relay will be updated.</param>
        /// <returns>Returns an updated RelayDescription object.</returns>
        public RelayDescription UpdateRelay(RelayDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager != null)
            {
                var relayService = RetryHelper.RetryFunc(() => NamespaceManager.UpdateRelayAsync(description).Result, WriteToLog);
                Log(string.Format(CultureInfo.CurrentCulture, RelayUpdated, description.Path));
                OnCreated(relayService);
                return relayService;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Gets the uri of a relay.
        /// </summary>
        /// <param name="description">The description of a relay.</param>
        /// <returns>The absolute uri of the relay.</returns>
        public Uri GetRelayUri(RelayDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (NamespaceManager == null)
            {
                return null;
            }
            var currentScheme = description.RelayType != RelayType.Http
                ? Scheme
                : description.RequiresTransportSecurity ? "https" : "http";
            return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(currentScheme, Namespace, string.Concat(servicePath, description.Path));
        }

    }
}
