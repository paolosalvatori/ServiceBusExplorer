// // Auto-added comment

// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.ServiceBus;
// using ServiceBusExplorer.Enums;
// using ServiceBusExplorer.Helpers;

// using AzureNotificationHubs = Microsoft.Azure.NotificationHubs;

// namespace ServiceBusExplorer.WindowsAzure
// {
//     internal class ServiceBusNotificationHub : ServiceBusEntity, IServiceBusNotificationHub
//     {
//         private const string NotificationHubDescriptionCannotBeNull = "The notification hub description argument cannot be null.";
//         private const string NotificationHubCreated = "The notification hub {0} has been successfully created.";
//         private const string NotificationHubDeleted = "The notification hub {0} has been successfully deleted.";
//         private const string NotificationHubUpdated = "The notification hub {0} has been successfully updated.";

//         private readonly string servicePath = string.Empty;
//         private readonly AzureNotificationHubs.NamespaceManager notificationHubNamespaceManager;

//         public ServiceBusNotificationHub(ServiceBusNamespace serviceBusNamespace, NamespaceManager namespaceManager, AzureNotificationHubs.NamespaceManager notificationHubNamespaceManager) : base(serviceBusNamespace, namespaceManager)
//         {
//             this.notificationHubNamespaceManager = notificationHubNamespaceManager;
//         }

//         protected override EntityType EntityType => EntityType.NotificationHub;

//         /// <summary>
//         /// Retrieves the notification hub from the service namespace.
//         /// </summary>
//         /// <param name="path">Path of the notification hub relative to the service namespace base address.</param>
//         /// <returns>A NotificationHubDescription handle to the notification hub, or null if the notification hub does not exist in the service namespace. </returns>
//         public AzureNotificationHubs.NotificationHubDescription GetNotificationHub(string path)
//         {
//             if (string.IsNullOrWhiteSpace(path))
//             {
//                 throw new ArgumentException(PathCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 return RetryHelper.RetryFunc(() => notificationHubNamespaceManager.GetNotificationHub(path), WriteToLog);
//             }
//             throw new ApplicationException(ServiceBusIsDisconnected);
//         }

//         /// <summary>
//         /// Creates a new notification hub in the service namespace with the given name.
//         /// </summary>
//         /// <param name="description">A NotificationHubDescription object describing the attributes with which the new notification hub will be created.</param>
//         /// <returns>Returns a newly-created NotificationHubDescription object.</returns>
//         public AzureNotificationHubs.NotificationHubDescription CreateNotificationHub(AzureNotificationHubs.NotificationHubDescription description)
//         {
//             if (description == null)
//             {
//                 throw new ArgumentException(DescriptionCannotBeNull);
//             }
//             if (NamespaceManager == null)
//             {
//                 throw new ApplicationException(ServiceBusIsDisconnected);
//             }
//             var notificationHub = RetryHelper.RetryFunc(() => notificationHubNamespaceManager.CreateNotificationHub(description), WriteToLog);
//             Log(string.Format(CultureInfo.CurrentCulture, NotificationHubCreated, description.Path));
//             OnCreated(new ServiceBusHelperEventArgs(notificationHub, EntityType));
//             return notificationHub;
//         }

//         /// <summary>
//         /// Deletes the notification hub described by the relative name of the service namespace base address.
//         /// </summary>
//         /// <param name="notificationHubDescription">The notification hub to delete.</param>
//         public async Task DeleteNotificationHub(AzureNotificationHubs.NotificationHubDescription notificationHubDescription)
//         {
//             if (string.IsNullOrWhiteSpace(notificationHubDescription?.Path))
//             {
//                 throw new ArgumentException(NotificationHubDescriptionCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 await RetryHelper.RetryActionAsync(() => notificationHubNamespaceManager.DeleteNotificationHubAsync(notificationHubDescription.Path), WriteToLog);
//                 Log(string.Format(CultureInfo.CurrentCulture, NotificationHubDeleted, notificationHubDescription.Path));
//                 OnDeleted(new ServiceBusHelperEventArgs(notificationHubDescription, EntityType));
//             }
//             else
//             {
//                 throw new ApplicationException(ServiceBusIsDisconnected);
//             }
//         }

//         /// <summary>
//         /// Deletes all the notification hubs in the list.
//         /// <param name="notificationHubs">A list of notification hubs to delete.</param>
//         /// </summary>
//         public async Task DeleteNotificationHubs(IEnumerable<string> notificationHubs)
//         {
//             if (notificationHubs == null)
//             {
//                 return;
//             }
//             await Task.WhenAll(notificationHubs.Select(DeleteNotificationHub).ToArray());
//         }

//         /// <summary>
//         /// Updates a notification hub in the service namespace with the given name.
//         /// </summary>
//         /// <param name="description">A NotificationHubDescription object describing the attributes with which the new notification hub will be updated.</param>
//         /// <returns>Returns an updated NotificationHubDescription object.</returns>
//         public AzureNotificationHubs.NotificationHubDescription UpdateNotificationHub(AzureNotificationHubs.NotificationHubDescription description)
//         {
//             if (description == null)
//             {
//                 throw new ArgumentException(DescriptionCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 var notificationHub = RetryHelper.RetryFunc(() => notificationHubNamespaceManager.UpdateNotificationHub(description), WriteToLog);
//                 Log(string.Format(CultureInfo.CurrentCulture, NotificationHubUpdated, description.Path));
//                 OnCreated(new ServiceBusHelperEventArgs(notificationHub, EntityType));
//                 return notificationHub;
//             }
//             throw new ApplicationException(ServiceBusIsDisconnected);
//         }

//         /// <summary>
//         /// Gets the uri of a notification hub.
//         /// </summary>
//         /// <param name="notificationHubPath">The name of a notification hub.</param>
//         /// <returns>The absolute uri of the notification hub.</returns>
//         public Uri GetNotificationHubUri(string notificationHubPath)
//         {
//             return Microsoft.ServiceBus.ServiceBusEnvironment.CreateServiceUri(Scheme, Namespace, string.Concat(servicePath, notificationHubPath));
//         }

//         private async Task DeleteNotificationHub(string path)
//         {
//             if (string.IsNullOrWhiteSpace(path))
//             {
//                 throw new ArgumentException(PathCannotBeNull);
//             }
//             if (NamespaceManager != null)
//             {
//                 await RetryHelper.RetryActionAsync(() => notificationHubNamespaceManager.DeleteNotificationHubAsync(path), WriteToLog);
//                 Log(string.Format(CultureInfo.CurrentCulture, NotificationHubDeleted, path));
//                 OnDeleted(new ServiceBusHelperEventArgs(path, EntityType));
//             }
//             else
//             {
//                 throw new ApplicationException(ServiceBusIsDisconnected);
//             }
//         }
//     }
// }
