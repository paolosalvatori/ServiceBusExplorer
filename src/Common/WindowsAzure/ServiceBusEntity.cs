// // Auto-added comment

// using Microsoft.ServiceBus;
// using Microsoft.ServiceBus.Messaging;
// using ServiceBusExplorer.Enums;
// using ServiceBusExplorer.Helpers;
// using ServiceBusExplorer.Utilities.Helpers;

// namespace ServiceBusExplorer.WindowsAzure
// {
//     internal abstract class ServiceBusEntity : IServiceBusEntity
//     {
//         protected const string PathCannotBeNull = "The path argument cannot be null or empty.";
//         protected const string NewPathCannotBeNull = "The new path argument cannot be null or empty.";
//         protected const string NameCannotBeNull = "The name argument cannot be null or empty.";
//         protected const string DescriptionCannotBeNull = "The description argument cannot be null.";
//         protected const string NamespaceManagerCannotBeNull = "The namespace manager argument cannot be null.";
//         protected const string ServiceBusIsDisconnected = "The application is now disconnected from any service bus namespace.";

//         private const string DefaultScheme = "sb";
//         private const string CloudServiceBusPostfix = ".servicebus.windows.net";
//         private const string GermanyServiceBusPostfix = ".servicebus.cloudapi.de";
//         private const string ChinaServiceBusPostfix = ".servicebus.chinacloudapi.cn";
//         private const string TestServiceBusPostFix = ".servicebus.int7.windows-int.net";

//         private readonly string ns;

//         protected ServiceBusEntity(ServiceBusNamespace serviceBusNamespace, NamespaceManager namespaceManager)
//         {
//             this.ServiceBusNamespace = serviceBusNamespace;
//             this.NamespaceManager = namespaceManager;
//             ns = GetNamespace();
//         }

//         public string Scheme { get; set; } = DefaultScheme;

//         public WriteToLogDelegate WriteToLog { get; set; } = (message, async) => { };

//         public event IServiceBusEntity.EventHandler OnDelete;

//         public event IServiceBusEntity.EventHandler OnCreate;

//         protected ServiceBusNamespace ServiceBusNamespace { get; }

//         protected NamespaceManager NamespaceManager { get; }

//         protected string Namespace { get { return ns; } }

//         protected abstract EntityType EntityType { get; }

//         protected void OnCreated(ServiceBusHelperEventArgs args)
//         {
//             args.EntityType = EntityType;
//             OnCreate?.Invoke(args);
//         }

//         protected void OnCreated<T>(T entityInstance) where T : EntityDescription
//         {
//             OnCreate?.Invoke(new ServiceBusHelperEventArgs(entityInstance, EntityType));
//         }

//         protected void OnDeleted(ServiceBusHelperEventArgs args)
//         {
//             args.EntityType = EntityType;
//             OnDelete?.Invoke(args);
//         }

//         protected void OnDeleted<T>(T entityInstance) where T : EntityDescription
//         {
//             OnDelete?.Invoke(new ServiceBusHelperEventArgs(entityInstance, EntityType));
//         }

//         protected void Log(string message)
//         {
//             WriteToLog?.Invoke(message);
//         }

//         protected bool IsCloudNamespace()
//         {
//             string uri;
//             var connectionStringType = ServiceBusNamespace.ConnectionStringType;
//             var namespaceUri = NamespaceManager.Address;
//             return connectionStringType == ServiceBusNamespaceType.Cloud ||
//                   (namespaceUri != null &&
//                    !string.IsNullOrWhiteSpace(uri = namespaceUri.ToString()) &&
//                    (uri.Contains(CloudServiceBusPostfix) ||
//                     uri.Contains(TestServiceBusPostFix) ||
//                     uri.Contains(GermanyServiceBusPostfix) ||
//                     uri.Contains(ChinaServiceBusPostfix)));
//         }

//         private string GetNamespace()
//         {
//             var namespaceUri = NamespaceManager.Address;
//             return IsCloudNamespace() ? namespaceUri.Host.Split('.')[0] : namespaceUri.Segments[namespaceUri.Segments.Length - 1];
//         }

//     }
// }
