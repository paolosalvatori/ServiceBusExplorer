#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus.Notifications;

#endregion

// ReSharper disable CheckNamespace
namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
// ReSharper restore CheckNamespace
{
    public enum BodyType
    {
        Stream,
        String,
        Wcf
    }

    public class ServiceBusHelper
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string StackTraceFormat = "StackTrace: {0}";

        //***************************
        // Constants
        //***************************
        private const string DefaultScheme = "sb";
        private const string MessageNumber = "MessageNumber";
        private const string StringType = "String";
        private const string DeadLetterQueue = "$DeadLetterQueue";
        private const string NullValue = "NULL";
        private const string CloudServiceBusPostfix = "servicebus.windows.net";
        private const int MaxBufferSize = 262144; // 256 KB


        //***************************
        // Messages
        //***************************
        private const string ServiceBusConnectionStringCannotBeNull = "The connection string argument cannot be null.";
        private const string ServiceBusUriArgumentCannotBeNull = "The uri argument cannot be null.";
        private const string ServiceBusNamespaceArgumentCannotBeNull = "The nameSpace argument cannot be null.";
        private const string ServiceBusIssuerNameArgumentCannotBeNull = "The issuerName argument cannot be null.";
        private const string ServiceBusIssuerSecretArgumentCannotBeNull = "The issuerSecret argument cannot be null.";
        private const string NamespaceManagerCannotBeNull = "The namespace manager argument cannot be null.";
        private const string QueueDescriptionCannotBeNull = "The queue description argument cannot be null.";
        private const string TopicDescriptionCannotBeNull = "The topic decsription argument cannot be null.";
        private const string SubscriptionDescriptionCannotBeNull = "The subscription description argument cannot be null.";
        private const string RuleDescriptionCannotBeNull = "The rule description argument cannot be null.";
        private const string NotificationHubDescriptionCannotBeNull = "The notification hub description argument cannot be null.";
        private const string RuleCannotBeNull = "The rule argument cannot be null.";
        private const string PathCannotBeNull = "The path argument cannot be null or empty.";
        private const string NameCannotBeNull = "The name argument cannot be null or empty.";
        private const string DescriptionCannotBeNull = "The description argument cannot be null.";
        private const string ServiceBusIsDisconnected = "The application is now disconnected from any service bus namespace.";
        private const string ServiceBusIsConnected = "The application is now connected to the {0} service bus namespace.";
        private const string QueueCreated = "The queue {0} has been successfully created.";
        private const string QueueDeleted = "The queue {0} has been successfully deleted.";
        private const string QueueUpdated = "The queue {0} has been successfully updated.";
        private const string TopicCreated = "The topic {0} has been successfully created.";
        private const string TopicDeleted = "The topic {0} has been successfully deleted.";
        private const string TopicUpdated = "The topic {0} has been successfully updated.";
        private const string SubscriptionCreated = "The {0} subscription for the {1} topic has been successfully created.";
        private const string SubscriptionDeleted = "The {0} subscription for the {1} topic has been successfully deleted.";
        private const string SubscriptionUpdated = "The {0} subscription for the {1} topic has been successfully updated.";
        private const string RuleCreated = "The {0} rule for the {1} subscription has been successfully created.";
        private const string RuleDeleted = "The {0} rule for the {1} subscription has been successfully deleted.";
        private const string NotificationHubCreated = "The notification hub {0} has been successfully created.";
        private const string NotificationHubDeleted = "The notification hub {0} has been successfully deleted.";
        private const string NotificationHubUpdated = "The notification hub {0} has been successfully updated.";
        private const string WarningHeader = "The following validations failed:";
        private const string WarningFormat = "\n\r - {0}";
        private const string PropertyConversionError = "{0} property conversion error: {1}";
        private const string PropertyValueCannotBeNull = "The value of the {0} property cannot be null.";
        private const string MessageSuccessfullySent = "Sender[{0}]:   Message sent. MessageId=[{1}] SessionId=[{2}] Label=[{3}] Size=[{4}]";
        private const string MessageSuccessfullyReceived = "Receiver[{0}]: Message received. MessageId=[{1}] SessionId=[{2}] Label=[{3}] Size=[{4}] DeliveryCount[{5}]";
        private const string MessagePeekedButNotConsumed = "Receiver[{0}]: Message peeked, but not consumed. MessageId=[{1}] SessionId=[{2}] Label=[{3}] Size=[{4}]";
        private const string MessageSuccessfullyReceivedNoTask = "Message {0}: MessageId=[{1}] SessionId=[{2}] Label=[{3}] Size=[{4}] DeliveryCount[{5}]";
        private const string ReceiverStatitiscsLineNoTask = "Messages {0}: Count=[{1}]";
        private const string SentMessagePropertiesHeader = "Properties:";
        private const string ReceivedMessagePropertiesHeader = "Properties:";
        private const string SentMessagePayloadHeader = "Payload:";
        private const string ReceivedMessagePayloadHeader = "Payload:";
        private const string MessageTextFormat = "{0}";
        private const string MessagePropertyFormat = " - Key=[{0}] Value=[{1}]";
        private const string MessageDeferred = " - The message was deferred.";
        private const string ReadMessageDeferred = " - Read deferred message.";
        private const string MessageMovedToDeadLetterQueue = " - The message was moved to the DeadLetter queue.";
        private const string MessageReadFromDeadLetterQueue = " - The message was read from the DeadLetter queue.";
        private const string NoMessageWasReceived = "Receiver[{0}]: no message was received";
        private const string SenderStatisticsHeader = "Sender[{0}]:";
        private const string SenderStatitiscsLine1 = " - Message Count=[{0}] Messages Sent/Sec=[{1}] Total Elapsed Time (ms)=[{2}]";
        private const string SenderStatitiscsLine2 = " - Average Send Time (ms)=[{0}] Minimum Send Time (ms)=[{1}] Maximum Send Time (ms)=[{2}] ";
        private const string ReceiverStatisticsHeader = "Receiver[{0}]:";
        private const string ReceiverStatitiscsLine1 = " - Message Count=[{0}] Messages Read/Sec=[{1}] Total Elapsed Time (ms)=[{2}]";
        private const string ReceiverStatitiscsWithCompleteLine1 = " - Message Count=[{0}] Messages Read/Sec=[{1}] Total Receive Elapsed Time (ms)=[{2}] Total Complete Elapsed Time (ms)=[{3}]";
        private const string ReceiverStatitiscsLine2 = " - Average Receive Time (ms)=[{0}] Minimum Receive Time (ms)=[{1}] Maximum Receive Time (ms)=[{2}] ";
        private const string ReceiverStatitiscsLine3 = " - Average Complete Time (ms)=[{0}] Minimum Complete Time (ms)=[{1}] Maximum Complete Time (ms)=[{2}] ";
        private const string ExceptionOccurred = " - Exception occurred: {0}";
        private const string UnableToReadMessageBody = "Unable to read the message body.";
        private const string MessageSenderCannotBeNull = "The MessageSender parameter cannot be null.";
        private const string MessageReceiverCannotBeNull = "The MessageReceiver parameter cannot be null.";
        private const string BrokeredMessageCannotBeNull = "The BrokeredMessage parameter cannot be null.";
        private const string CancellationTokenSourceCannotBeNull = "The CancellationTokenSource parameter cannot be null.";
        private const string MessageIsNotXmlOrJson = "The message is not in XML or JSON format.";
        private const string MessageFactorySuccessfullyCreated = "MessagingFactory successfully created";
        private const string SleepingFor = "Sleeping for [{0}] milliseconds...";
        private const string Read = "read";
        private const string Peeked = "peeked";
        #endregion

        #region Private Fields
        private Type messageDeferProviderType = typeof(InMemoryMessageDeferProvider);
        private NamespaceManager namespaceManager;
        private MessagingFactory messagingFactory;
        private bool traceEnabled = true;
        private string scheme = DefaultScheme;
        private TokenProvider tokenProvider;
        private Uri namespaceUri;
        private Uri atomFeedUri;
        private string ns;
        private string servicePath;
        private string currentIssuerName;
        private string currentIssuerSecret;
        private string connectionString;
        private List<BrokeredMessage> brokeredMessageList;
        private readonly WriteToLogDelegate writeToLog;
        #endregion

        #region Private Static Fields
        private static EncodingType encodingType = EncodingType.ASCII;
        #endregion

        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the ServiceBusHelper class.
        /// </summary>
        /// <param name="writeToLog">WriteToLog method.</param>
        public ServiceBusHelper(WriteToLogDelegate writeToLog)
        {
            this.writeToLog = writeToLog;
            traceEnabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceBusHelper class.
        /// </summary>
        /// <param name="writeToLog">WriteToLog method.</param>
        /// <param name="traceEnabled">A boolean value indicating whether tracing is enabled.</param>
        public ServiceBusHelper(WriteToLogDelegate writeToLog, bool traceEnabled)
        {
            this.writeToLog = writeToLog;
            this.traceEnabled = traceEnabled;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceBusHelper class.
        /// </summary>
        /// <param name="writeToLog">WriteToLog method.</param>
        /// <param name="serviceBusHelper">Base ServiceBusHelper.</param>
        public ServiceBusHelper(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper)
        {
            this.writeToLog = writeToLog;
            traceEnabled = serviceBusHelper.TraceEnabled;
            AtomFeedUri = serviceBusHelper.AtomFeedUri;
            IssuerName = serviceBusHelper.IssuerName;
            IssuerSecret = serviceBusHelper.IssuerSecret;
            MessageDeferProviderType = serviceBusHelper.MessageDeferProviderType;
            connectionString = serviceBusHelper.ConnectionString;
            namespaceManager = serviceBusHelper.NamespaceManager;
            MessagingFactory = serviceBusHelper.MessagingFactory;
            Namespace = serviceBusHelper.Namespace;
            NamespaceUri = serviceBusHelper.NamespaceUri;
            IssuerSecret = serviceBusHelper.IssuerSecret;
            MessageDeferProviderType = serviceBusHelper.MessageDeferProviderType;
            MessagingFactory = serviceBusHelper.MessagingFactory;
            Namespace = serviceBusHelper.Namespace;
            Scheme = serviceBusHelper.Scheme;
            ServiceBusNamespaces = serviceBusHelper.ServiceBusNamespaces;
            ServicePath = serviceBusHelper.ServicePath;
            TokenProvider = serviceBusHelper.TokenProvider;
            TraceEnabled = serviceBusHelper.TraceEnabled;
        }
        #endregion

        #region Public Instance Properties
        /// <summary>
        /// Gets a boolean that indicates if the current namespace is a cloud namespace.
        /// </summary>
        public bool IsCloudNamespace
        {
            get
            {
                string uri;
                return namespaceUri != null &&
                       !string.IsNullOrWhiteSpace(uri = namespaceUri.ToString()) &&
                       uri.Contains(CloudServiceBusPostfix);
            }
        }

        /// <summary>
        /// Gets or sets the type of the message defer provider
        /// </summary>
        public Type MessageDeferProviderType
        {
            get
            {
                lock (this)
                {
                    return messageDeferProviderType;
                }
            }
            set
            {
                lock (this)
                {
                    if (value.GetInterfaces().Contains(typeof(IMessageDeferProvider)))
                    {
                        messageDeferProviderType = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a boolean value indicating whether tracing is enabled.
        /// </summary>
        public bool TraceEnabled
        {
            get
            {
                lock (this)
                {
                    return traceEnabled;
                }
            }
            set
            {
                lock (this)
                {
                    traceEnabled = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the scheme of the URI.
        /// </summary>
        public string Scheme
        {
            get
            {
                lock (this)
                {
                    return scheme;
                }
            }
            set
            {
                lock (this)
                {
                    scheme = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current namespace.
        /// </summary>
        public string Namespace
        {
            get
            {
                lock (this)
                {
                    return ns;
                }
            }
            set
            {
                lock (this)
                {
                    ns = value;
                }
            }
        }

        /// <summary>
        /// Gets the current namespace manager.
        /// </summary>
        public NamespaceManager NamespaceManager
        {
            get
            {
                lock (this)
                {
                    return namespaceManager;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current service path.
        /// </summary>
        public string ServicePath
        {
            get
            {
                lock (this)
                {
                    return servicePath ?? string.Empty;
                }
            }
            set
            {
                lock (this)
                {
                    servicePath = value;
                    if (!string.IsNullOrWhiteSpace(servicePath) &&
                        servicePath[servicePath.Length - 1] != '/')
                    {
                        servicePath = servicePath + '/';
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                lock (this)
                {
                    return connectionString;
                }
            }
            set
            {
                lock (this)
                {
                    connectionString = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the issuer name.
        /// </summary>
        public string IssuerName
        {
            get
            {
                lock (this)
                {
                    return currentIssuerName;
                }
            }
            set
            {
                lock (this)
                {
                    currentIssuerName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the issuer secret.
        /// </summary>
        public string IssuerSecret
        {
            get
            {
                lock (this)
                {
                    return currentIssuerSecret;
                }
            }
            set
            {
                lock (this)
                {
                    currentIssuerSecret = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the URI of the current service bus namespace.
        /// </summary>
        public Uri NamespaceUri
        {
            get
            {
                lock (this)
                {
                    return namespaceUri;
                }
            }
            set
            {
                lock (this)
                {
                    namespaceUri = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the URI of the atom feed for the current namespace.
        /// </summary>
        public Uri AtomFeedUri
        {
            get
            {
                lock (this)
                {
                    return atomFeedUri;
                }
            }
            set
            {
                lock (this)
                {
                    atomFeedUri = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the credentials of the current service bus account.
        /// </summary>
        public TokenProvider TokenProvider
        {
            get
            {
                lock (this)
                {
                    return tokenProvider;
                }
            }
            set
            {
                lock (this)
                {
                    tokenProvider = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the MessagingFactory
        /// </summary>
        public MessagingFactory MessagingFactory
        {
            get
            {
                lock (this)
                {
                    if (messagingFactory == null ||
                        messagingFactory.IsClosed)
                    {
                        messagingFactory = GetMessagingFactory();
                    }
                    return messagingFactory;
                }
            }
            set
            {
                lock (this)
                {
                    messagingFactory = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the dictionary containing serviceBus accounts.
        /// </summary>
        public Dictionary<string, ServiceBusNamespace> ServiceBusNamespaces { get; set; }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets or sets the connectivity mode when connecting to namespaces
        /// </summary>
        public static ConnectivityMode ConnectivityMode
        {
            get
            {
                return ServiceBusEnvironment.SystemConnectivity.Mode;
            }
            set
            {
                ServiceBusEnvironment.SystemConnectivity.Mode = value;
            }
        }

        /// <summary>
        /// Gets or sets the encodingType of sent messages
        /// </summary>
        public static EncodingType EncodingTypeType
        {
            get
            {
                return encodingType;
            }
            set
            {
                encodingType = value;
            }
        }

        #endregion

        #region Public Events
        public delegate void EventHandler(ServiceBusHelperEventArgs args);
        public event EventHandler OnDelete;
        public event EventHandler OnCreate;
        #endregion

        #region Public Methods
        /// <summary>
        /// Connects the ServiceBusHelper object to service bus namespace contained in the ServiceBusNamespaces dictionary.
        /// </summary>
        /// <param name="nameSpace">The namespace of the Service Bus.</param>
        /// <param name="path">The service path that follows the host name section of the URI.</param>
        /// <param name="issuerName">The issuer name of the shared secret credentials.</param>
        /// <param name="issuerSecret">The issuer secret of the shared secret credentials.</param>
        /// <returns>True if the operation succeeds, false otherwise.</returns>
        public bool Connect(string nameSpace, string path, string issuerName, string issuerSecret)
        {
            Func<bool> func = (() =>
            {
                if (string.IsNullOrWhiteSpace(nameSpace))
                {
                    throw new ArgumentException(ServiceBusNamespaceArgumentCannotBeNull);
                }
                if (string.IsNullOrWhiteSpace(issuerName))
                {
                    throw new ArgumentException(ServiceBusIssuerNameArgumentCannotBeNull);
                }
                if (string.IsNullOrWhiteSpace(issuerSecret))
                {
                    throw new ArgumentException(ServiceBusIssuerSecretArgumentCannotBeNull);
                }

                // Create the service URI using the scheme, namespace and service path (optional)
                namespaceUri = ServiceBusEnvironment.CreateServiceUri(scheme,
                                                                      nameSpace,
                                                                      path);
                // Create the atom feed URI using the scheme, namespace and service path (optional)
                atomFeedUri = ServiceBusEnvironment.CreateServiceUri(Uri.UriSchemeHttp,
                                                                     nameSpace,
                                                                     path);
                Namespace = nameSpace;
                ServicePath = path;

                // Create shared secret credentials to to authenticate with the Access Control service, 
                // and acquire an access token that proves to the Service Bus insfrastructure that the 
                // the Service Bus Explorer is authorized to access the entities in the specified namespace.
                tokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName,
                                                                              issuerSecret);

                currentIssuerName = issuerName;
                currentIssuerSecret = issuerSecret;

                // Create and instance of the NamespaceManagerSettings which 
                // specifies service namespace client settings and metadata.
                var namespaceManagerSettings = new NamespaceManagerSettings
                {
                    TokenProvider = tokenProvider,
                    OperationTimeout = TimeSpan.FromMinutes(5)
                };

                // The NamespaceManager class can be used for managing entities, 
                // such as queues, topics, subscriptions, and rules, in your service namespace. 
                // You must provide service namespace address and access credentials in order 
                // to manage your service namespace.
                namespaceManager = new NamespaceManager(namespaceUri, namespaceManagerSettings);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, ServiceBusIsConnected, namespaceUri.AbsoluteUri));

                // The MessagingFactorySettings specifies the service bus messaging factory settings.
                var messagingFactorySettings = new MessagingFactorySettings
                {
                    TokenProvider = tokenProvider,
                    OperationTimeout = TimeSpan.FromMinutes(5)
                };
                // In the first release of the service bus, the only available transport protocol is sb 
                if (scheme == DefaultScheme)
                {
                    messagingFactorySettings.NetMessagingTransportSettings = new NetMessagingTransportSettings();
                }

                // As the name suggests, the MessagingFactory class is a Factory class that allows to create
                // instances of the QueueClient, TopicClient and SubscriptionClient classes.
                MessagingFactory = MessagingFactory.Create(namespaceUri, messagingFactorySettings);
                WriteToLogIf(traceEnabled, MessageFactorySuccessfullyCreated);
                return true;
            });
            return RetryHelper.RetryFunc(func, writeToLog);
        }

        /// <summary>
        /// Connects the ServiceBusHelper object to service bus namespace contained in the ServiceBusNamespaces dictionary.
        /// </summary>
        /// <param name="uri">The full uri of the service namespace.</param>
        /// <param name="issuerName">The issuer name of the shared secret credentials.</param>
        /// <param name="issuerSecret">The issuer secret of the shared secret credentials.</param>
        /// <returns>True if the operation succeeds, false otherwise.</returns>
        public bool Connect(string uri, string issuerName, string issuerSecret)
        {
            Func<bool> func = (() =>
            {
                if (string.IsNullOrWhiteSpace(uri))
                {
                    throw new ArgumentException(ServiceBusUriArgumentCannotBeNull);
                }
                if (string.IsNullOrWhiteSpace(issuerName))
                {
                    throw new ArgumentException(ServiceBusIssuerNameArgumentCannotBeNull);
                }
                if (string.IsNullOrWhiteSpace(issuerSecret))
                {
                    throw new ArgumentException(ServiceBusIssuerSecretArgumentCannotBeNull);
                }

                // Create the service URI using the uri specified in the Connect form
                namespaceUri = new Uri(uri);
                if (!string.IsNullOrWhiteSpace(namespaceUri.Host) &&
                    namespaceUri.Host.Contains('.'))
                {
                    Namespace = namespaceUri.Host.Substring(0, namespaceUri.Host.IndexOf('.'));
                }

                // Create the atom feed URI using the scheme, namespace and service path (optional)
                if (uri.Substring(0, 4) != Uri.UriSchemeHttp)
                {
                    var index = uri.IndexOf("://", StringComparison.Ordinal);
                    if (index > 0)
                    {
                        uri = Uri.UriSchemeHttp + uri.Substring(index);
                    }
                }
                atomFeedUri = new Uri(uri);

                ServicePath = string.Empty;

                // Create shared secret credentials to to authenticate with the Access Control service, 
                // and acquire an access token that proves to the Service Bus insfrastructure that the 
                // the Service Bus Explorer is authorized to access the entities in the specified namespace.
                tokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName,
                                                                              issuerSecret);

                currentIssuerName = issuerName;
                currentIssuerSecret = issuerSecret;

                // Create and instance of the NamespaceManagerSettings which 
                // specifies service namespace client settings and metadata.
                var namespaceManagerSettings = new NamespaceManagerSettings
                {
                    TokenProvider = tokenProvider,
                    OperationTimeout = TimeSpan.FromMinutes(5)
                };

                // The NamespaceManager class can be used for managing entities, 
                // such as queues, topics, subscriptions, and rules, in your service namespace. 
                // You must provide service namespace address and access credentials in order 
                // to manage your service namespace.
                namespaceManager = new NamespaceManager(namespaceUri, namespaceManagerSettings);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, ServiceBusIsConnected, namespaceUri.AbsoluteUri));

                // The MessagingFactorySettings specifies the service bus messaging factory settings.
                var messagingFactorySettings = new MessagingFactorySettings
                {
                    TokenProvider = tokenProvider,
                    OperationTimeout = TimeSpan.FromMinutes(5)
                };
                // In the first release of the service bus, the only available transport protocol is sb 
                if (scheme == DefaultScheme)
                {
                    messagingFactorySettings.NetMessagingTransportSettings = new NetMessagingTransportSettings();
                }

                // As the name suggests, the MessagingFactory class is a Factory class that allows to create
                // instances of the QueueClient, TopicClient and SubscriptionClient classes.
                MessagingFactory = MessagingFactory.Create(namespaceUri, messagingFactorySettings);
                WriteToLogIf(traceEnabled, MessageFactorySuccessfullyCreated);
                return true;
            });
            return RetryHelper.RetryFunc(func, writeToLog);
        }

        /// <summary>
        /// Connects the ServiceBusHelper object to service bus namespace contained in the ServiceBusNamespaces dictionary.
        /// </summary>
        /// <param name="serviceBusNamespace">The Service Bus namespace.</param>
        /// <returns>True if the operation succeeds, false otherwise.</returns>
        public bool Connect(ServiceBusNamespace serviceBusNamespace)
        {
            Func<bool> func = (() =>
            {
                if (serviceBusNamespace == null ||
                    string.IsNullOrWhiteSpace(serviceBusNamespace.ConnectionString))
                {
                    throw new ArgumentException(ServiceBusConnectionStringCannotBeNull);
                }

                connectionString = serviceBusNamespace.ConnectionString;
                IssuerName = serviceBusNamespace.IssuerName;
                IssuerSecret = serviceBusNamespace.IssuerSecret;
                
                // The NamespaceManager class can be used for managing entities, 
                // such as queues, topics, subscriptions, and rules, in your service namespace. 
                // You must provide service namespace address and access credentials in order 
                // to manage your service namespace.
                namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, ServiceBusIsConnected, namespaceManager.Address.AbsoluteUri));
                namespaceUri = namespaceManager.Address;
                ns = IsCloudNamespace ? namespaceUri.Host.Split('.')[0] : namespaceUri.Segments[namespaceUri.Segments.Length - 1];
                atomFeedUri = new Uri(string.Format("{0}://{1}", Uri.UriSchemeHttp, namespaceUri.Host));

                // As the name suggests, the MessagingFactory class is a Factory class that allows to create
                // instances of the QueueClient, TopicClient and SubscriptionClient classes.
                MessagingFactory = MessagingFactory.CreateFromConnectionString(connectionString);
                WriteToLogIf(traceEnabled, MessageFactorySuccessfullyCreated);
                return true;
            });
            return RetryHelper.RetryFunc(func, writeToLog);
        }

        /// <summary>
        /// Retrieves the notification hub from the service namespace.
        /// </summary>
        /// <param name="path">Path of the notification hub relative to the service namespace base address.</param>
        /// <returns>A NotificationHubDescription handle to the notification hub, or null if the notification hub does not exist in the service namespace. </returns>
        public NotificationHubDescription GetNotificationHub(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetNotificationHub(path), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all notification hubs in the service bus namespace.
        /// </summary>
        /// <returns>Returns an IEnumerable<NotificationHubDescription/> collection of all notification hubs in the service namespace. 
        ///          Returns an empty collection if no notification hub exists in this service namespace.</returns>
        public IEnumerable<NotificationHubDescription> GetNotificationHubs()
        {
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetNotificationHubs(), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes the notification hub described by the relative path of the service namespace base address.
        /// </summary>
        /// <param name="path">Path of the notification hub relative to the service namespace base address.</param>
        public void DeleteNotificationHub(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                RetryHelper.RetryAction(() => namespaceManager.DeleteNotificationHub(path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, NotificationHubDeleted, path));
                OnDelete(new ServiceBusHelperEventArgs(path, EntityType.NotificationHub));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Creates a new notification hub in the service namespace with the given path.
        /// </summary>
        /// <param name="description">A NotificationHubDescription object describing the attributes with which the new notification hub will be created.</param>
        /// <returns>Returns a newly-created NotificationHubDescription object.</returns>
        public NotificationHubDescription CreateNotificationHub(NotificationHubDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                var notificationHub = RetryHelper.RetryFunc(() => namespaceManager.CreateNotificationHub(description), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, NotificationHubCreated, description.Path));
                OnCreate(new ServiceBusHelperEventArgs(notificationHub, EntityType.NotificationHub));
                return notificationHub;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes the notification hub described by the relative path of the service namespace base address.
        /// </summary>
        /// <param name="notificationHubDescription">The notification hub to delete.</param>
        public void DeleteNotificationHub(NotificationHubDescription notificationHubDescription)
        {
            if (notificationHubDescription == null ||
                string.IsNullOrWhiteSpace(notificationHubDescription.Path))
            {
                throw new ArgumentException(NotificationHubDescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                RetryHelper.RetryAction(() => namespaceManager.DeleteNotificationHub(notificationHubDescription.Path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, NotificationHubDeleted, notificationHubDescription.Path));
                OnDelete(new ServiceBusHelperEventArgs(notificationHubDescription, EntityType.NotificationHub));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Deletes all the notification hubs in the list.
        /// <param name="notificationHubs">A list of notification hubs to delete.</param>
        /// </summary>
        public void DeleteNotificationHubs(IEnumerable<string> notificationHubs)
        {
            if (notificationHubs == null)
            {
                return;
            }
            foreach (var notificationHub in notificationHubs)
            {
                DeleteNotificationHub(notificationHub);
            }
        }

        /// <summary>
        /// Updates a notification hub in the service namespace with the given path.
        /// </summary>
        /// <param name="description">A NotificationHubDescription object describing the attributes with which the new notification hub will be updated.</param>
        /// <returns>Returns an updated NotificationHubDescription object.</returns>
        public NotificationHubDescription UpdateNotificationHub(NotificationHubDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                var notificationHub = RetryHelper.RetryFunc(() => namespaceManager.UpdateNotificationHub(description), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, NotificationHubUpdated, description.Path));
                OnCreate(new ServiceBusHelperEventArgs(notificationHub, EntityType.NotificationHub));
                return notificationHub;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Gets the uri of a notification hub.
        /// </summary>
        /// <param name="notificationHubPath">The path of a notification hub.</param>
        /// <returns>The absolute uri of the notification hub.</returns>
        public Uri GetNotificationHubUri(string notificationHubPath)
        {
            return ServiceBusEnvironment.CreateServiceUri(scheme, Namespace, string.Concat(ServicePath, notificationHubPath));
        }

        /// <summary>
        /// Retrieves an enumerable collection of all queues in the service bus namespace.
        /// </summary>
        /// <param name="filter">OData filter.</param> 
        /// <returns>Returns an IEnumerable<QueueDescription/> collection of all queues in the service namespace. 
        ///          Returns an empty collection if no queue exists in this service namespace.</returns>
        public IEnumerable<QueueDescription> GetQueues(string filter)
        {
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => string.IsNullOrWhiteSpace(filter)? namespaceManager.GetQueues() : namespaceManager.GetQueues(filter), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves the queue from the service namespace.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        /// <returns>A QueueDescription handle to the queue, or null if the queue does not exist in the service namespace. </returns>
        public QueueDescription GetQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetQueue(path), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all message sessions for the queue passed as argument.
        /// </summary>
        /// <param name="path">The queue for which to search message sessions.</param> 
        /// <param name="dateTime">The time the session was last updated.</param> 
        /// <returns>Returns an IEnumerable<QueueDescription/> collection of message sessions.</returns>
        public IEnumerable<MessageSession> GetMessageSessions(string path, DateTime? dateTime)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (messagingFactory == null)
            {
                throw new ApplicationException(ServiceBusIsDisconnected);

            }
            var queueClient = messagingFactory.CreateQueueClient(path);
            return RetryHelper.RetryFunc(() => dateTime != null ? queueClient.GetMessageSessions(dateTime.Value) : queueClient.GetMessageSessions(), writeToLog);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all message sessions for the queue passed as argument.
        /// </summary>
        /// <param name="queue">The queue for which to search message sessions.</param> 
        /// <param name="dateTime">The time the session was last updated.</param> 
        /// <returns>Returns an IEnumerable<QueueDescription/> collection of message sessions.</returns>
        public IEnumerable<MessageSession> GetMessageSessions(QueueDescription queue, DateTime? dateTime)
        {
            if (queue == null)
            {
                throw new ArgumentException(QueueDescriptionCannotBeNull);
            }
            if (messagingFactory == null )
            {
                throw new ApplicationException(ServiceBusIsDisconnected);

            }
            var queueClient = messagingFactory.CreateQueueClient(queue.Path);
            return RetryHelper.RetryFunc(() => dateTime != null? queueClient.GetMessageSessions(dateTime.Value) : queueClient.GetMessageSessions(), writeToLog);
        }

        /// <summary>
        /// Retrieves the topic from the service namespace.
        /// </summary>
        /// <param name="path">Path of the topic relative to the service namespace base address.</param>
        /// <returns>A TopicDescription handle to the topic, or null if the topic does not exist in the service namespace. </returns>
        public TopicDescription GetTopic(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetTopic(path), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all topics in the service bus namespace.
        /// </summary>
        /// <returns>Returns an IEnumerable<TopicDescription/> collection of all topics in the service namespace. 
        ///          Returns an empty collection if no topic exists in this service namespace.</returns>
        public IEnumerable<TopicDescription> GetTopics()
        {
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetTopics(), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all topics in the service bus namespace.
        /// </summary>
        /// <param name="filter">OData filter.</param> 
        /// <returns>Returns an IEnumerable<TopicDescription/> collection of all topics in the service namespace. 
        ///          Returns an empty collection if no topic exists in this service namespace.</returns>
        public IEnumerable<TopicDescription> GetTopics(string filter)
        {
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => string.IsNullOrWhiteSpace(filter) ? namespaceManager.GetTopics() : namespaceManager.GetTopics(filter), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Gets a subscription attached to the topic passed a parameter.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <param name="name">The name of the subscription to get.</param>
        /// <returns>Returns the subscription with the specified name.</returns>
        public SubscriptionDescription GetSubscription(string topicPath, string name)
        {
            if (namespaceManager == null)
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
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetSubscription(topicPath, name), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerated collection of subscriptions attached to the topic passed as a parameter.
        /// </summary>
        /// <param name="topic">A topic belonging to the current service namespace base.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of subscriptions attached to the topic passed as a parameter.</returns>
        public IEnumerable<SubscriptionDescription> GetSubscriptions(TopicDescription topic)
        {
            if (topic == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetSubscriptions(topic.Path), writeToLog);
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
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetSubscriptions(topicPath), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all message sessions for the subscription passed as argument.
        /// </summary>
        /// <param name="subscription">The subscription for which to search message sessions.</param> 
        /// <param name="dateTime">The time the session was last updated.</param> 
        /// <returns>Returns an IEnumerable<QueueDescription/> collection of message sessions.</returns>
        public IEnumerable<MessageSession> GetMessageSessions(SubscriptionDescription subscription, DateTime? dateTime)
        {
            if (subscription == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }
            if (messagingFactory == null)
            {
                throw new ApplicationException(ServiceBusIsDisconnected);

            }
            var subscriptionClient = messagingFactory.CreateSubscriptionClient(subscription.TopicPath, subscription.Name);
            return RetryHelper.RetryFunc(() => dateTime != null ? subscriptionClient.GetMessageSessions(dateTime.Value) : subscriptionClient.GetMessageSessions(), writeToLog);
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
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => string.IsNullOrWhiteSpace(filter) ?
                                                   namespaceManager.GetSubscriptions(topic.Path) :
                                                   namespaceManager.GetSubscriptions(topic.Path, filter), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Retrieves an enumerated collection of subscriptions attached to the topic whose name is passed as a parameter.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <param name="filter">OData filter.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of subscriptions attached to the topic passed as a parameter.</returns>
        public IEnumerable<SubscriptionDescription> GetSubscriptions(string topicPath, string filter)
        {
            if (string.IsNullOrWhiteSpace(topicPath))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => string.IsNullOrWhiteSpace(filter) ?
                                                   namespaceManager.GetSubscriptions(topicPath) :
                                                   namespaceManager.GetSubscriptions(topicPath, filter), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

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
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetRules(subscription.TopicPath, subscription.Name), writeToLog);
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
            if (namespaceManager != null)
            {
                return RetryHelper.RetryFunc(() => namespaceManager.GetRules(topicPath, name), writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Gets the uri of a queue.
        /// </summary>
        /// <param name="queuePath">The path of a queue.</param>
        /// <returns>The absolute uri of the queue.</returns>
        public Uri GetQueueUri(string queuePath)
        {
            if (IsCloudNamespace)
            {
                return ServiceBusEnvironment.CreateServiceUri(scheme, Namespace, string.Concat(ServicePath, queuePath));
            }
            // ReSharper disable RedundantIfElseBlock
            else
            // ReSharper restore RedundantIfElseBlock
            {
                var uriBuilder = new UriBuilder
                {
                    Host = namespaceUri.Host,
                    Path = string.Format("{0}/{1}", namespaceUri.AbsolutePath, queuePath),
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        /// <summary>
        /// Gets the uri of the deadletter queue for a given queue.
        /// </summary>
        /// <param name="queuePath">The path of a queue.</param>
        /// <returns>he absolute uri of the deadletter queue.</returns>
        public Uri GetQueueDeadLetterQueueUri(string queuePath)
        {
            if (IsCloudNamespace)
            {
                return ServiceBusEnvironment.CreateServiceUri(scheme, Namespace, string.Concat(ServicePath, QueueClient.FormatDeadLetterPath(queuePath)));
            }
            // ReSharper disable RedundantIfElseBlock
            else
            // ReSharper restore RedundantIfElseBlock
            {
                var uriBuilder = new UriBuilder
                {
                    Host = namespaceUri.Host,
                    Path = string.Format("{0}/{1}", namespaceUri.AbsolutePath, QueueClient.FormatDeadLetterPath(queuePath)),
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        /// <summary>
        /// Gets the uri of a topic.
        /// </summary>
        /// <param name="topicPath">The path of a topic.</param>
        /// <returns>The absolute uri of the topic.</returns>
        public Uri GetTopicUri(string topicPath)
        {
            if (IsCloudNamespace)
            {
                return ServiceBusEnvironment.CreateServiceUri(scheme, Namespace, string.Concat(ServicePath, topicPath));
            }
            // ReSharper disable RedundantIfElseBlock
            else
            // ReSharper restore RedundantIfElseBlock
            {
                var uriBuilder = new UriBuilder
                {
                    Host = namespaceUri.Host,
                    Path = string.Format("{0}/{1}", namespaceUri.AbsolutePath, topicPath),
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        /// <summary>
        /// Gets the uri of a subscription.
        /// </summary>
        /// <param name="topicPath">The path of the topic.</param>
        /// <param name="name">The name of a subscription.</param>
        /// <returns>The absolute uri of the subscription.</returns>
        public Uri GetSubscriptionUri(string topicPath, string name)
        {
            if (IsCloudNamespace)
            {
                return ServiceBusEnvironment.CreateServiceUri(scheme, Namespace, string.Concat(ServicePath, SubscriptionClient.FormatSubscriptionPath(topicPath, name)));
            }
            // ReSharper disable RedundantIfElseBlock
            else
            // ReSharper restore RedundantIfElseBlock
            {
                var uriBuilder = new UriBuilder
                {
                    Host = namespaceUri.Host,
                    Path = string.Format("{0}/{1}", namespaceUri.AbsolutePath, SubscriptionClient.FormatSubscriptionPath(topicPath, name)),
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        /// <summary>
        /// Gets the uri of the deadletter queue for a given subscription.
        /// </summary>
        /// <param name="topicPath">The path of a topic.</param>
        /// <param name="name">The name of a subscription.</param>
        /// <returns>The absolute uri of the deadletter queue.</returns>
        public Uri GetSubscriptionDeadLetterQueueUri(string topicPath, string name)
        {
            if (IsCloudNamespace)
            {
                return ServiceBusEnvironment.CreateServiceUri(scheme, Namespace, SubscriptionClient.FormatDeadLetterPath(topicPath, name));
            }
            // ReSharper disable RedundantIfElseBlock
            else
            // ReSharper restore RedundantIfElseBlock
            {
                var uriBuilder = new UriBuilder
                {
                    Host = namespaceUri.Host,
                    Path = string.Format("{0}/{1}", namespaceUri.AbsolutePath, SubscriptionClient.FormatDeadLetterPath(topicPath, name)),
                    Scheme = "sb",
                };
                return uriBuilder.Uri;
            }
        }

        /// <summary>.
        /// Creates a new queue in the service namespace with the given path.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        /// <returns>Returns a newly-created QueueDescription object.</returns>
        public QueueDescription CreateQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => namespaceManager.CreateQueue(path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueCreated, path));
                OnCreate(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Creates a new queue in the service namespace with the given path.
        /// </summary>
        /// <param name="description">A QueueDescription object describing the attributes with which the new queue will be created.</param>
        /// <returns>Returns a newly-created QueueDescription object.</returns>
        public QueueDescription CreateQueue(QueueDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => namespaceManager.CreateQueue(description), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueCreated, description.Path));
                OnCreate(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Updates a queue in the service namespace with the given path.
        /// </summary>
        /// <param name="description">A QueueDescription object describing the attributes with which the new queue will be updated.</param>
        /// <returns>Returns an updated QueueDescription object.</returns>
        public QueueDescription UpdateQueue(QueueDescription description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                var queue = RetryHelper.RetryFunc(() => namespaceManager.UpdateQueue(description), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueUpdated, description.Path));
                OnCreate(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes all the queues in the list.
        /// <param name="queues">A list of queues to delete.</param>
        /// </summary>
        public void DeleteQueues(IEnumerable<string> queues)
        {
            if (queues == null)
            {
                return;
            }
            foreach (var queue in queues)
            {
                DeleteQueue(queue);
            }
        }

        /// <summary>
        /// Deletes the queue described by the relative path of the service namespace base address.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        public void DeleteQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                RetryHelper.RetryAction(() => namespaceManager.DeleteQueue(path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueDeleted, path));
                OnDelete(new ServiceBusHelperEventArgs(path, EntityType.Queue));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Deletes the queue passed as a argument.
        /// </summary>
        /// <param name="queueDescription">The queue to delete.</param>
        public void DeleteQueue(QueueDescription queueDescription)
        {
            if (queueDescription == null)
            {
                throw new ArgumentException(QueueDescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                RetryHelper.RetryAction(() => namespaceManager.DeleteQueue(queueDescription.Path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueDeleted, queueDescription.Path));
                OnDelete(new ServiceBusHelperEventArgs(queueDescription, EntityType.Queue));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Creates a new topic in the service namespace with the given path.
        /// </summary>
        /// <param name="path">Path of the topic relative to the service namespace base address.</param>
        /// <returns>Returns a newly-created TopicDescription object.</returns>
        public TopicDescription CreateTopic(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                var topic = RetryHelper.RetryFunc(() => namespaceManager.CreateTopic(path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, TopicCreated, path));
                OnCreate(new ServiceBusHelperEventArgs(topic, EntityType.Topic));
                return topic;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Creates a new topic in the service namespace with the given path.
        /// </summary>
        /// <param name="topicDescription">A TopicDescription object describing the attributes with which the new topic will be created.</param>
        /// <returns>Returns a newly-created TopicDescription object.</returns>
        public TopicDescription CreateTopic(TopicDescription topicDescription)
        {
            if (topicDescription == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                var topic = RetryHelper.RetryFunc(() => namespaceManager.CreateTopic(topicDescription), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, TopicCreated, topicDescription.Path));
                OnCreate(new ServiceBusHelperEventArgs(topic, EntityType.Topic));
                return topic;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Updates a topic in the service namespace with the given path.
        /// </summary>
        /// <param name="topicDescription">A TopicDescription object describing the attributes with which the new topic will be updated.</param>
        /// <returns>Returns an updated TopicDescription object.</returns>
        public TopicDescription UpdateTopic(TopicDescription topicDescription)
        {
            if (topicDescription == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                var topic = RetryHelper.RetryFunc(() => namespaceManager.UpdateTopic(topicDescription), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, TopicUpdated, topicDescription.Path));
                OnCreate(new ServiceBusHelperEventArgs(topic, EntityType.Topic));
                return topic;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes all the topics in the list.
        /// <param name="topics">A list of topics to delete.</param>
        /// </summary>
        public void DeleteTopics(IEnumerable<string> topics)
        {
            if (topics == null)
            {
                return;
            }
            foreach (var topic in topics)
            {
                DeleteTopic(topic);
            }
        }

        /// <summary>
        /// Deletes the topic described by the relative path of the service namespace base address.
        /// </summary>
        /// <param name="path">Path of the topic relative to the service namespace base address.</param>
        public void DeleteTopic(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (namespaceManager != null)
            {
                RetryHelper.RetryAction(() => namespaceManager.DeleteTopic(path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, TopicDeleted, path));
                OnDelete(new ServiceBusHelperEventArgs(path, EntityType.Topic));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /// <summary>
        /// Deletes the topic passed as a argument.
        /// </summary>
        /// <param name="topic">The topic to delete.</param>
        public void DeleteTopic(TopicDescription topic)
        {
            if (topic == null)
            {
                throw new ArgumentException(TopicDescriptionCannotBeNull);
            }
            if (namespaceManager != null)
            {
                RetryHelper.RetryAction(() => namespaceManager.DeleteTopic(topic.Path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, TopicDeleted, topic.Path));
                OnDelete(new ServiceBusHelperEventArgs(topic, EntityType.Topic));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
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
            var subscription = RetryHelper.RetryFunc(() => namespaceManager.CreateSubscription(subscriptionDescription), writeToLog);
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, SubscriptionCreated, subscription.Name, topicDescription.Path));
            OnCreate(new ServiceBusHelperEventArgs(new SubscriptionWrapper(subscription, topicDescription), EntityType.Subscription));
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
            var subscription = RetryHelper.RetryFunc(() => namespaceManager.CreateSubscription(subscriptionDescription, ruleDescription), writeToLog);
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, SubscriptionCreated, subscription.Name, topicDescription.Path));
            OnCreate(new ServiceBusHelperEventArgs(new SubscriptionWrapper(subscription, topicDescription), EntityType.Subscription));
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
            var subscription = RetryHelper.RetryFunc(() => namespaceManager.UpdateSubscription(subscriptionDescription), writeToLog);
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, SubscriptionUpdated, subscription.Name, topicDescription.Path));
            //OnCreate(new ServiceBusHelperEventArgs(new SubscriptionWrapper(subscription, topicDescription), EntityType.Subscription));
            return subscription;
        }

        /// <summary>
        /// Removes the subscriptions contained in the list passed as a argument.
        /// </summary>
        /// <param name="subscriptionDescriptions">The list containing subscriptions to remove.</param>
        public void DeleteSubscriptions(IEnumerable<SubscriptionDescription> subscriptionDescriptions)
        {
            if (subscriptionDescriptions == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }
            foreach (var subscriptionDescription in subscriptionDescriptions)
            {
                DeleteSubscription(subscriptionDescription);
            }
        }

        /// <summary>
        /// Removes the subscription described by name.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <param name="name">Name of the subscription.</param>
        public void DeleteSubscription(string topicPath, string name)
        {
            if (string.IsNullOrWhiteSpace(topicPath))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(NameCannotBeNull);
            }
            RetryHelper.RetryAction(() => namespaceManager.DeleteSubscription(topicPath, name), writeToLog);
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, SubscriptionDeleted, name, topicPath));
            OnDelete(new ServiceBusHelperEventArgs(name, EntityType.Subscription));
        }

        /// <summary>
        /// Removes the subscription passed as a argument.
        /// </summary>
        /// <param name="subscriptionDescription">The subscription to remove.</param>
        public void DeleteSubscription(SubscriptionDescription subscriptionDescription)
        {
            if (subscriptionDescription == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }
            RetryHelper.RetryAction(() => namespaceManager.DeleteSubscription(subscriptionDescription.TopicPath, subscriptionDescription.Name), writeToLog);
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, SubscriptionDeleted, subscriptionDescription.Name, subscriptionDescription.TopicPath));
            OnDelete(new ServiceBusHelperEventArgs(subscriptionDescription, EntityType.Subscription));
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
            var subscriptionClient = RetryHelper.RetryFunc(() => MessagingFactory.CreateSubscriptionClient(subscriptionDescription.TopicPath,
                                                                                                           subscriptionDescription.Name),
                                                                                                           writeToLog);
            RetryHelper.RetryAction(() => subscriptionClient.AddRule(ruleDescription), writeToLog);
            Func<IEnumerable<RuleDescription>> func = (() => namespaceManager.GetRules(subscriptionDescription.TopicPath, subscriptionDescription.Name));
            var rules = RetryHelper.RetryFunc(func, writeToLog);
            var rule = rules.FirstOrDefault(r => r.Name == ruleDescription.Name);
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, RuleCreated, ruleDescription.Name, subscriptionDescription.Name));
            OnCreate(new ServiceBusHelperEventArgs(new RuleWrapper(rule, subscriptionDescription), EntityType.Rule));
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
        /// Removes the rule described by name.
        /// </summary>
        /// <param name="subscriptionDescription">The subscription to add the rule to.</param>
        /// <param name="name">Name of the rule.</param>
        public void RemoveRule(SubscriptionDescription subscriptionDescription, string name)
        {
            if (subscriptionDescription == null)
            {
                throw new ArgumentException(SubscriptionDescriptionCannotBeNull);
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(NameCannotBeNull);
            }
            var subscriptionClient = MessagingFactory.CreateSubscriptionClient(subscriptionDescription.TopicPath,
                                                                               subscriptionDescription.Name);
            RetryHelper.RetryAction(() => subscriptionClient.RemoveRule(name), writeToLog);
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, RuleDeleted, name, subscriptionClient.Name));
            OnDelete(new ServiceBusHelperEventArgs(name, EntityType.Rule));
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
            var subscriptionClient = MessagingFactory.CreateSubscriptionClient(subscriptionDescription.TopicPath,
                                                                               subscriptionDescription.Name);
            RetryHelper.RetryAction(() => subscriptionClient.RemoveRule(rule.Name), writeToLog);
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, RuleDeleted, rule.Name, subscriptionClient.Name));
            OnDelete(new ServiceBusHelperEventArgs(new RuleWrapper(rule, subscriptionDescription), EntityType.Rule));
        }

        /// <summary>
        /// Create a BrokeredMessage object
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <param name="label">The value of the LabelId property of the message.</param>
        /// <param name="contentType">The type of the content.</param>
        /// <param name="messageId">The value of the MessageId property of the message.</param>
        /// <param name="sessionId">The value of the SessionId property of the message.</param>
        /// <param name="correlationId">The value of the CorrelationId property of the message.</param>
        /// <param name="to">The send to address.</param>
        /// <param name="replyTo">The value of the ReplyTo property of the message.</param>
        /// <param name="replyToSessionId">The value of the ReplyToSessionId property of the message.</param>
        /// <param name="timeToLive">The value of the TimeToLive property of the message.</param>
        /// <param name="scheduledEnqueueTimeUtc">The receiveTimeout in seconds after which the message will be enqueued.</param>
        /// <param name="properties">The user-defined properties of the message.</param>
        /// <returns>The newly created BrokeredMessage object.</returns>
        public BrokeredMessage CreateMessage(string text,
                                             string label,
                                             string contentType,
                                             string messageId,
                                             string sessionId,
                                             string correlationId,
                                             string to,
                                             string replyTo,
                                             string replyToSessionId,
                                             string timeToLive,
                                             string scheduledEnqueueTimeUtc,
                                             IEnumerable<MessagePropertyInfo> properties)
        {
            var warningCollection = new ConcurrentBag<string>();
            var outboundMessage = new BrokeredMessage(text.ToMemoryStream(GetEncoding()), true);
            if (!string.IsNullOrWhiteSpace(label))
            {
                outboundMessage.Label = label;
            }
            if (!string.IsNullOrWhiteSpace(contentType))
            {
                outboundMessage.ContentType = contentType;
            }
            if (!string.IsNullOrWhiteSpace(to))
            {
                outboundMessage.To = to;
            }
            outboundMessage.MessageId = !string.IsNullOrWhiteSpace(messageId) ? messageId : Guid.NewGuid().ToString();
            if (!string.IsNullOrWhiteSpace(sessionId))
            {
                outboundMessage.SessionId = sessionId;
            }
            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                outboundMessage.CorrelationId = correlationId;
            }
            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                outboundMessage.ReplyTo = replyTo;
            }
            if (!string.IsNullOrWhiteSpace(replyToSessionId))
            {
                outboundMessage.ReplyToSessionId = replyToSessionId;
            }
            int ttl;
            if (!string.IsNullOrWhiteSpace(timeToLive) && int.TryParse(timeToLive, out ttl))
            {
                outboundMessage.TimeToLive = TimeSpan.FromSeconds(ttl);
            }
            int ss;
            if (!string.IsNullOrWhiteSpace(scheduledEnqueueTimeUtc) && int.TryParse(scheduledEnqueueTimeUtc, out ss))
            {
                outboundMessage.ScheduledEnqueueTimeUtc = DateTime.UtcNow.AddSeconds(ss);
            }
            foreach (var e in properties)
            {
                try
                {
                    e.Key = e.Key.Trim();
                    if (e.Type != StringType && e.Value == null)
                    {
                        warningCollection.Add(string.Format(CultureInfo.CurrentUICulture, PropertyValueCannotBeNull, e.Key));
                    }
                    else
                    {
                        if (outboundMessage.Properties.ContainsKey(e.Key))
                        {
                            outboundMessage.Properties[e.Key] = ConversionHelper.MapStringTypeToCLRType(e.Type, e.Value);
                        }
                        else
                        {
                            outboundMessage.Properties.Add(e.Key, ConversionHelper.MapStringTypeToCLRType(e.Type, e.Value));
                        }
                    }
                }
                catch (Exception ex)
                {
                    warningCollection.Add(string.Format(CultureInfo.CurrentUICulture, PropertyConversionError, e.Key, ex.Message));
                }
            }
            if (warningCollection.Count > 0)
            {
                var builder = new StringBuilder(WarningHeader);
                var warnings = warningCollection.ToArray<string>();
                for (var i = 0; i < warningCollection.Count; i++)
                {
                    builder.AppendFormat(WarningFormat, warnings[i]);
                }
                WriteToLogIf(traceEnabled, builder.ToString());
                return null;
            }
            return outboundMessage;
        }

        /// <summary>
        /// Create a BrokeredMessage object
        /// </summary>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="taskId">The task Id.</param>
        /// <param name="updateMessageId">Indicates whether to use a unique id for each message.</param>
        /// <param name="oneSessionPerTask">Indicates whether to use a different session for each sender task.</param>
        /// <param name="messageText">The message text.</param>
        /// <param name="bodyType">Contains the body type.</param>
        /// <returns>The cloned BrokeredMessage object.</returns>
        public BrokeredMessage CreateMessageForApiReceiver(BrokeredMessage messageTemplate,
                                                           int taskId,
                                                           bool updateMessageId,
                                                           bool oneSessionPerTask,
                                                           string messageText,
                                                           BodyType bodyType)
        {
            if (messageTemplate == null)
            {
                throw new ArgumentNullException(BrokeredMessageCannotBeNull);
            }
            var outboundMessage = bodyType == BodyType.Stream
                                      ? new BrokeredMessage(messageText.ToMemoryStream(GetEncoding()), true)
                                      : new BrokeredMessage(messageText);
            if (!string.IsNullOrWhiteSpace(messageTemplate.Label))
            {
                outboundMessage.Label = messageTemplate.Label;
            }
            if (!string.IsNullOrWhiteSpace(messageTemplate.ContentType))
            {
                outboundMessage.ContentType = messageTemplate.ContentType;
            }
            outboundMessage.MessageId = updateMessageId ? Guid.NewGuid().ToString() : messageTemplate.MessageId;
            outboundMessage.SessionId = oneSessionPerTask ? taskId.ToString(CultureInfo.InvariantCulture) : messageTemplate.SessionId;
            if (!string.IsNullOrWhiteSpace(messageTemplate.CorrelationId))
            {
                outboundMessage.CorrelationId = messageTemplate.CorrelationId;
            }
            if (!string.IsNullOrWhiteSpace(messageTemplate.To))
            {
                outboundMessage.To = messageTemplate.To;
            }
            if (!string.IsNullOrWhiteSpace(messageTemplate.ReplyTo))
            {
                outboundMessage.ReplyTo = messageTemplate.ReplyTo;
            }
            if (!string.IsNullOrWhiteSpace(messageTemplate.ReplyToSessionId))
            {
                outboundMessage.ReplyToSessionId = messageTemplate.ReplyToSessionId;
            }
            outboundMessage.TimeToLive = messageTemplate.TimeToLive;
            outboundMessage.ScheduledEnqueueTimeUtc = messageTemplate.ScheduledEnqueueTimeUtc;
            foreach (var property in messageTemplate.Properties)
            {
                outboundMessage.Properties.Add(property.Key, property.Value);
            }
            return outboundMessage;
        }


        /// <summary>
        /// Create a BrokeredMessage for a WCF receiver.
        /// </summary>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="taskId">The task Id.</param>
        /// <param name="updateMessageId">Indicates whether to use a unique id for each message.</param>
        /// <param name="oneSessionPerTask">Indicates whether to use a different session for each sender task.</param>
        /// <param name="messageText">The message text.</param>
        /// <param name="to">The uri of the target topic or queue.</param>
        /// <returns>The cloned BrokeredMessage object.</returns>
        public BrokeredMessage CreateMessageForWcfReceiver(BrokeredMessage messageTemplate,
                                                           int taskId,
                                                           bool updateMessageId,
                                                           bool oneSessionPerTask,
                                                           string messageText,
                                                           Uri to)
        {
            var isXml = XmlHelper.IsXml(messageText);
            var isJson = !isXml && JsonSerializerHelper.IsJson(messageText);
            Message message = null;
            MessageEncodingBindingElement element;
            var outputStream = new MemoryStream();
            if (scheme == DefaultScheme)
            {
                element = new BinaryMessageEncodingBindingElement();
            }
            else
            {
                element = new TextMessageEncodingBindingElement();
            }
            var encoderFactory = element.CreateMessageEncoderFactory();
            var encoder = encoderFactory.Encoder;
            if (isXml)
            {
               using (var stringReader = new StringReader(messageText))
                {
                    using (var xmlReader = XmlReader.Create(stringReader))
                    {
                        using (var dictionaryReader = XmlDictionaryReader.CreateDictionaryReader(xmlReader))
                        {
                            message = Message.CreateMessage(MessageVersion.Default, "*", dictionaryReader);
                            encoder.WriteMessage(message, outputStream);
                            outputStream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                }
            }
            else
            {
                if (isJson)
                {
                    message = Message.CreateMessage(MessageVersion.Default, "*", new StringBodyWriter(messageText));
                    encoder.WriteMessage(message, outputStream);
                    outputStream.Seek(0, SeekOrigin.Begin);
                }
            }

            if (message != null && outputStream.Length > 0)
            {
                message.Headers.To = to;
                
                var outboundMessage = new BrokeredMessage(outputStream, true)
                {
                    ContentType = encoder.ContentType
                };
                if (!string.IsNullOrWhiteSpace(messageTemplate.Label))
                {
                    outboundMessage.Label = messageTemplate.Label;
                }
                if (!string.IsNullOrWhiteSpace(messageTemplate.ContentType))
                {
                    outboundMessage.ContentType = messageTemplate.ContentType;
                }
                outboundMessage.MessageId = updateMessageId ? Guid.NewGuid().ToString() : messageTemplate.MessageId;
                outboundMessage.SessionId = oneSessionPerTask ? taskId.ToString(CultureInfo.InvariantCulture) : messageTemplate.SessionId;
                if (!string.IsNullOrWhiteSpace(messageTemplate.CorrelationId))
                {
                    outboundMessage.CorrelationId = messageTemplate.CorrelationId;
                }
                if (!string.IsNullOrWhiteSpace(messageTemplate.To))
                {
                    outboundMessage.To = messageTemplate.To;
                }
                if (!string.IsNullOrWhiteSpace(messageTemplate.ReplyTo))
                {
                    outboundMessage.ReplyTo = messageTemplate.ReplyTo;
                }
                if (!string.IsNullOrWhiteSpace(messageTemplate.ReplyToSessionId))
                {
                    outboundMessage.ReplyToSessionId = messageTemplate.ReplyToSessionId;
                }
                outboundMessage.TimeToLive = messageTemplate.TimeToLive;
                outboundMessage.ScheduledEnqueueTimeUtc = messageTemplate.ScheduledEnqueueTimeUtc;
                foreach (var property in messageTemplate.Properties)
                {
                    outboundMessage.Properties.Add(property.Key, property.Value);
                }
                return outboundMessage;
            }
            throw new ApplicationException(MessageIsNotXmlOrJson);
        }

        /// <summary>
        /// This method can be used to send multiple messages to a queue or a topic.
        /// </summary>
        /// <param name="messageSender">A MessageSender object used to send messages.</param>
        /// <param name="messageTemplateEnumerable">A collection of message templates to use to clone messages from.</param>
        /// <param name="getMessageNumber">This function returns the message number.</param>
        /// <param name="messageCount">The total number of messages to send.</param>
        /// <param name="messageTextEnumerable">A collection containing the message text of templates.</param>
        /// <param name="taskId">The sender task id.</param>
        /// <param name="updateMessageId">Indicates whether to use a unique id for each message.</param>
        /// <param name="addMessageNumber">Indicates whether to add a message number property.</param>
        /// <param name="oneSessionPerTask">Indicates whether to use a different session for each sender task.</param>
        /// <param name="logging">Indicates whether to enable logging of message content and properties.</param>
        /// <param name="verbose">Indicates whether to enable verbose logging.</param>
        /// <param name="statistics">Indicates whether to enable sender statistics.</param>
        /// <param name="updateStatistics">When statistics = true, this delegate is invoked to update statistics.</param>
        /// <param name="sendBatch">Indicates whether to use SendBatch.</param>
        /// <param name="senderThinkTime">Indicates whether to use think time.</param>
        /// <param name="thinkTime">Indicates the value of the sender think time.</param>
        /// <param name="batchSize">Indicates the batch size.</param>
        /// <param name="bodyType">Contains the body type.</param>
        /// <param name="cancellationTokenSource">The cancellation token.</param>
        /// <param name="traceMessage">A trace message.</param>
        /// <returns>True if the method completed without exceptions, false otherwise.</returns>
        public bool SendMessages(MessageSender messageSender,
                                 IEnumerable<BrokeredMessage> messageTemplateEnumerable,
                                 Func<long> getMessageNumber,
                                 long messageCount,
                                 IEnumerable<string> messageTextEnumerable,
                                 int taskId,
                                 bool updateMessageId,
                                 bool addMessageNumber,
                                 bool oneSessionPerTask,
                                 bool logging,
                                 bool verbose,
                                 bool statistics,
                                 bool sendBatch,
                                 int batchSize,
                                 bool senderThinkTime,
                                 int thinkTime,
                                 BodyType bodyType,
                                 UpdateStatisticsDelegate updateStatistics,
                                 CancellationTokenSource cancellationTokenSource,
                                 out string traceMessage)
        {
            if (messageSender == null)
            {
                throw new ArgumentNullException(MessageSenderCannotBeNull);
            }

            if (messageTemplateEnumerable == null)
            {
                throw new ArgumentNullException(BrokeredMessageCannotBeNull);
            }

            if (messageTextEnumerable == null)
            {
                throw new ArgumentNullException(BrokeredMessageCannotBeNull);
            }

            if (cancellationTokenSource == null)
            {
                throw new ArgumentNullException(CancellationTokenSourceCannotBeNull);
            }

            var messageTemplateCircularList = new CircularList<BrokeredMessage>(messageTemplateEnumerable);
            var messageTextCircularList = new CircularList<string>(messageTextEnumerable);

            long messagesSent = 0;
            long totalElapsedTime = 0;
            long minimumSendTime = long.MaxValue;
            long maximumSendTime = 0;
            bool ok = true;
            string exceptionMessage = null;
            var wcfUri = IsCloudNamespace ?
                         new Uri(namespaceUri, messageSender.Path) :
                         new UriBuilder
                         {
                             Host = namespaceUri.Host,
                             Path = string.Format("{0}/{1}", namespaceUri.AbsolutePath, messageSender.Path),
                             Scheme = "sb"
                         }.Uri;
            try
            {
                long messageNumber;
                if (sendBatch && batchSize > 1)
                {
                    var more = true;
                    while (!cancellationTokenSource.Token.IsCancellationRequested && more)
                    {
                        var messageList = new List<BrokeredMessage>();
                        var messageNumberList = new List<long>();
                        while (!cancellationTokenSource.Token.IsCancellationRequested &&
                               messageNumberList.Count < batchSize && more)
                        {
                            messageNumber = getMessageNumber();
                            if (messageNumber < messageCount)
                            {
                                messageNumberList.Add(messageNumber);
                            }
                            else
                            {
                                more = false;
                            }
                        }
                        if (messageNumberList.Count > 0)
                        {
                            long elapsedMilliseconds = 0;
                            RetryHelper.RetryAction(() =>
                            {
                                var useWcf = bodyType == BodyType.Wcf;
                                for (var i = 0; i < messageNumberList.Count; i++)
                                {
                                    messageList.Add(useWcf?
                                                    CreateMessageForWcfReceiver(
                                                        messageTemplateCircularList.Next,
                                                        taskId,
                                                        updateMessageId,
                                                        oneSessionPerTask,
                                                        messageTextCircularList.Next,
                                                        wcfUri) :
                                                    CreateMessageForApiReceiver(
                                                        messageTemplateCircularList.Next,
                                                        taskId,
                                                        updateMessageId,
                                                        oneSessionPerTask,
                                                        messageTextCircularList.Next,
                                                        bodyType));
                                    if (addMessageNumber)
                                    {
                                        messageList[i].Properties[MessageNumber] = messageNumberList[i];
                                    }
                                }
                                if (messageNumberList.Count > 0)
                                {
                                    SendBatch(messageSender,
                                              messageList,
                                              messageTextCircularList,
                                              taskId,
                                              useWcf,
                                              logging,
                                              verbose,
                                              out elapsedMilliseconds);
                                }
                            },
                            writeToLog);
                            messagesSent += messageList.Count;
                            if (elapsedMilliseconds > maximumSendTime)
                            {
                                maximumSendTime = elapsedMilliseconds;
                            }
                            if (elapsedMilliseconds < minimumSendTime)
                            {
                                minimumSendTime = elapsedMilliseconds;
                            }
                            totalElapsedTime += elapsedMilliseconds;
                            if (statistics)
                            {
                                updateStatistics(messageList.Count, elapsedMilliseconds, DirectionType.Send);
                            }
                        }
                        if (senderThinkTime)
                        {
                            WriteToLog(string.Format(SleepingFor, thinkTime));
                            Thread.Sleep(thinkTime);
                        }
                    }
                }
                else
                {
                    while ((messageNumber = getMessageNumber()) < messageCount &&
                       !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        long elapsedMilliseconds = 0;
                        RetryHelper.RetryAction(() =>
                        {
                            var messageText = messageTextCircularList.Next;
                            var useWcf = bodyType == BodyType.Wcf;
                            var outboundMessage = useWcf
                                                      ? CreateMessageForWcfReceiver(
                                                          messageTemplateCircularList.Next,
                                                          taskId,
                                                          updateMessageId,
                                                          oneSessionPerTask,
                                                          messageText,
                                                          wcfUri)
                                                      : CreateMessageForApiReceiver(
                                                          messageTemplateCircularList.Next,
                                                          taskId,
                                                          updateMessageId,
                                                          oneSessionPerTask,
                                                          messageText,
                                                          bodyType);
                            if (addMessageNumber)
                            {
                                // ReSharper disable AccessToModifiedClosure
                                outboundMessage.Properties[MessageNumber] = messageNumber;
                                // ReSharper restore AccessToModifiedClosure
                            }


                            SendMessage(messageSender,
                                        outboundMessage,
                                        messageText,
                                        taskId,
                                        useWcf,
                                        logging,
                                        verbose,
                                        out elapsedMilliseconds);
                        },
                        writeToLog);
                        messagesSent++;
                        if (elapsedMilliseconds > maximumSendTime)
                        {
                            maximumSendTime = elapsedMilliseconds;
                        }
                        if (elapsedMilliseconds < minimumSendTime)
                        {
                            minimumSendTime = elapsedMilliseconds;
                        }
                        totalElapsedTime += elapsedMilliseconds;
                        if (statistics)
                        {
                            updateStatistics(1, elapsedMilliseconds, DirectionType.Send);
                        }
                        if (senderThinkTime)
                        {
                            WriteToLog(string.Format(SleepingFor, thinkTime));
                            Thread.Sleep(thinkTime);
                        }
                    }
                }
            }
            catch (ServerBusyException ex)
            {
                messageSender.Abort();
                exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                ok = false;
            }
            catch (MessageLockLostException ex)
            {
                messageSender.Abort();
                exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                ok = false;
            }
            catch (CommunicationObjectAbortedException ex)
            {
                messageSender.Abort();
                exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                ok = false;
            }
            catch (CommunicationObjectFaultedException ex)
            {
                messageSender.Abort();
                exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                ok = false;
            }
            catch (CommunicationException ex)
            {
                messageSender.Abort();
                exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                ok = false;
            }
            catch (TimeoutException ex)
            {
                messageSender.Abort();
                exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                ok = false;
            }
            catch (Exception ex)
            {
                messageSender.Abort();
                exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                ok = false;
            }
            var averageSendTime = messagesSent > 0 ? totalElapsedTime / messagesSent : maximumSendTime;
            // ReSharper disable RedundantCast
            var messagesPerSecond = totalElapsedTime > 0 ? (double)(messagesSent * 1000) / (double)totalElapsedTime : 0;
            // ReSharper restore RedundantCast
            var builder = new StringBuilder();
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             SenderStatisticsHeader,
                                             taskId));
            if (!string.IsNullOrWhiteSpace(exceptionMessage))
            {
                builder.AppendLine(exceptionMessage);
            }
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             SenderStatitiscsLine1,
                                             messagesSent,
                                             messagesPerSecond,
                                             totalElapsedTime));
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             SenderStatitiscsLine2,
                                             averageSendTime,
                                             minimumSendTime == long.MaxValue ? 0 : minimumSendTime,
                                             maximumSendTime));
            traceMessage = builder.ToString();
            return ok;
        }

        /// <summary>
        /// This method can be used to send a message to a queue or a topic.
        /// </summary>
        /// <param name="messageSender">A MessageSender object used to send messages.</param>
        /// <param name="messageList">The list of messages to send.</param>
        /// <param name="messageTextList">The list of message texts.</param>
        /// <param name="taskId">The sender task id.</param>
        /// <param name="useWcf">Indicates whether to send messages to a WCF receiver.</param>
        /// <param name="logging">Indicates whether logging of message content and properties is enabled.</param>
        /// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        /// <param name="elapsedMilliseconds">The time spent to send the message.</param>
        public void SendBatch(MessageSender messageSender,
                              List<BrokeredMessage> messageList,
                              List<string> messageTextList,
                              int taskId,
                              bool useWcf,
                              bool logging,
                              bool verbose,
                              out long elapsedMilliseconds)
        {
            if (messageSender == null)
            {
                throw new ArgumentNullException(MessageSenderCannotBeNull);
            }

            if (messageList == null || messageList.Count == 0)
            {
                elapsedMilliseconds = 0;
                return;
            }

            var stopwatch = new Stopwatch();


            var builder = new StringBuilder();
            try
            {
                stopwatch.Start();
                messageSender.SendBatch(messageList);
            }
            finally
            {
                stopwatch.Stop();
            }
            elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            if (logging)
            {
                for (var i = 0; i < messageList.Count; i++)
                {
                    try
                    {

                        builder.AppendLine(string.Format(CultureInfo.CurrentCulture, MessageSuccessfullySent,
                                                            taskId,
                                                            string.IsNullOrWhiteSpace(messageList[i].MessageId)
                                                                ? NullValue
                                                                : messageList[i].MessageId,
                                                            string.IsNullOrWhiteSpace(messageList[i].SessionId)
                                                                ? NullValue
                                                                : messageList[i].SessionId,
                                                            string.IsNullOrWhiteSpace(messageList[i].Label)
                                                                ? NullValue
                                                                : messageList[i].Label,
                                                            messageList[i].Size));
                        if (verbose)
                        {
                            builder.AppendLine(SentMessagePayloadHeader);
                            if (useWcf)
                            {
                                var stringBuilder = new StringBuilder();
                                using (var reader = XmlReader.Create(new StringReader(messageTextList[i])))
                                {
                                    // The XmlWriter is used just to indent the XML message
                                    var settings = new XmlWriterSettings {Indent = true};
                                    using (var writer = XmlWriter.Create(stringBuilder, settings))
                                    {
                                        writer.WriteNode(reader, true);
                                    }
                                }
                                messageTextList[i] = stringBuilder.ToString();
                            }
                            builder.AppendLine(string.Format(MessageTextFormat, messageTextList[i]));
                            builder.AppendLine(SentMessagePropertiesHeader);
                            foreach (var p in messageList[i].Properties)
                            {
                                builder.AppendLine(string.Format(MessagePropertyFormat,
                                                                 p.Key,
                                                                 p.Value));
                            }
                        }
                    }
                    finally
                    {
                        messageList[i].Dispose();
                    }
                }
                var traceMessage = builder.ToString();
                WriteToLog(traceMessage.Substring(0, traceMessage.Length - 1));
            }
        }

        /// <summary>
        /// This method can be used to send a message to a queue or a topic.
        /// </summary>
        /// <param name="messageSender">A MessageSender object used to send messages.</param>
        /// <param name="outboundMessage">The message to send.</param>
        /// <param name="messageText">The message text.</param>
        /// <param name="taskId">The sender task id.</param>
        /// <param name="useWcf">Indicates whether to send messages to a WCF receiver.</param>
        /// <param name="logging">Indicates whether logging of message content and properties is enabled.</param>
        /// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        /// <param name="elapsedMilliseconds">The time spent to send the message.</param>
        public void SendMessage(MessageSender messageSender,
                                BrokeredMessage outboundMessage,
                                string messageText,
                                int taskId,
                                bool useWcf,
                                bool logging,
                                bool verbose,
                                out long elapsedMilliseconds)
        {
            if (messageSender == null)
            {
                throw new ArgumentNullException(MessageSenderCannotBeNull);
            }

            if (outboundMessage == null)
            {
                throw new ArgumentNullException(BrokeredMessageCannotBeNull);
            }

            var stopwatch = new Stopwatch();

            try
            {
                var builder = new StringBuilder();
                try
                {
                    stopwatch.Start();
                    messageSender.Send(outboundMessage);
                }
                finally
                {
                    stopwatch.Stop();
                }
                elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                if (logging)
                {
                    builder.AppendLine(string.Format(CultureInfo.CurrentCulture, MessageSuccessfullySent,
                                                     taskId,
                                                     string.IsNullOrWhiteSpace(outboundMessage.MessageId) ? NullValue : outboundMessage.MessageId,
                                                     string.IsNullOrWhiteSpace(outboundMessage.SessionId) ? NullValue : outboundMessage.SessionId,
                                                     string.IsNullOrWhiteSpace(outboundMessage.Label) ? NullValue : outboundMessage.Label,
                                                     outboundMessage.Size));
                    if (verbose)
                    {
                        builder.AppendLine(SentMessagePayloadHeader);
                        if (useWcf)
                        {
                            messageText = XmlHelper.Indent(messageText);
                        }
                        builder.AppendLine(string.Format(MessageTextFormat, messageText));
                        if (outboundMessage.Properties.Any())
                        {
                            builder.AppendLine(SentMessagePropertiesHeader);
                            foreach (var p in outboundMessage.Properties)
                            {
                                builder.AppendLine(string.Format(MessagePropertyFormat,
                                                                    p.Key,
                                                                    p.Value));
                            }
                        }
                    }
                    var traceMessage = builder.ToString();
                    WriteToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                }
            }
            finally
            {
                outboundMessage.Dispose();
            }
        }

        /// <summary>
        /// This method is used to receive message from a queue or a subscription.
        /// </summary>
        /// <param name="messageReceiver">The message receiver used to receive messages.</param>
        /// <param name="taskId">The receiver task id.</param>
        /// <param name="timeout">The receive receiveTimeout.</param>
        /// <param name="filter">The filter expression is used to determine messages to move the dead-letter queue or to defer.</param>
        /// <param name="moveToDeadLetterQueue">Indicates whether to move messages to the dead-letter queue.</param>
        /// <param name="completeReceive">Indicates whether to complete a receive operation when ReceiveMode is equal to PeekLock.</param>
        /// <param name="defer">Indicates whether to defer messages.</param>
        /// <param name="logging">Indicates whether logging of message content and properties is enabled.</param>
        /// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        /// <param name="statistics">Indicates whether to enable receiver statistics.</param>
        /// <param name="receiveBatch">Indicates whether to use ReceiveBatch.</param>
        /// <param name="batchSize">Indicates the batch size.</param>
        /// <param name="receiverThinkTime">Indicates whether to use think time.</param>
        /// <param name="thinkTime">Indicates the value of the think time in milliseconds.</param>
        /// <param name="updateStatistics">When statistics = true, this delegate is invoked to update statistics.</param>
        /// <param name="cancellationTokenSource">The cancellation token.</param>
        /// <param name="traceMessage">A trace message.</param>
        /// <returns>True if the method completed without exceptions, false otherwise.</returns>
        public bool ReceiveMessages(MessageReceiver messageReceiver,
                                    int taskId,
                                    int timeout,
                                    Filter filter,
                                    bool moveToDeadLetterQueue,
                                    bool completeReceive,
                                    bool defer,
                                    bool logging,
                                    bool verbose,
                                    bool statistics,
                                    bool receiveBatch,
                                    int batchSize,
                                    bool receiverThinkTime,
                                    int thinkTime,
                                    UpdateStatisticsDelegate updateStatistics,
                                    CancellationTokenSource cancellationTokenSource,
                                    out string traceMessage)
        {
            if (messageReceiver == null)
            {
                throw new ArgumentNullException(MessageReceiverCannotBeNull);
            }

            if (cancellationTokenSource == null)
            {
                throw new ArgumentNullException(CancellationTokenSourceCannotBeNull);
            }

            BrokeredMessage inboundMessage = null;
            StringBuilder builder;
            var isCompleted = false;
            var ok = true;
            var receivedFromDeadLetterQueue = messageReceiver.Path.EndsWith(DeadLetterQueue);
            var readingDeferredMessages = false;
            long messagesReceived = 0;
            long totalReceiveElapsedTime = 0;
            long totalCompleteElapsedTime = 0;
            long minimumReceiveTime = long.MaxValue;
            long maximumReceiveTime = 0;
            long minimumCompleteTime = long.MaxValue;
            long maximumCompleteTime = 0;
            long fetchedMessages = 0;
            long prefetchElapsedTime = 0;
            string exceptionMessage = null;
            var messageDeferProvider = Activator.CreateInstance(messageDeferProviderType) as IMessageDeferProvider;

            try
            {
                MessageEncodingBindingElement element;
                if (scheme == DefaultScheme)
                {
                    element = new BinaryMessageEncodingBindingElement();
                }
                else
                {
                    element = new TextMessageEncodingBindingElement();
                }
                var encoderFactory = element.CreateMessageEncoderFactory();
                var encoder = encoderFactory.Encoder;
                if (receiveBatch && batchSize > 0)
                {
                    while (!isCompleted &&
                           !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        IList<BrokeredMessage> messageList = null;
                        try
                        {
                            var stopwatch = new Stopwatch();
                            stopwatch.Start();
                            var messageEnumerable = messageReceiver.ReceiveBatch(batchSize, TimeSpan.FromSeconds(timeout));
                            stopwatch.Stop();
                            messageList = messageEnumerable as IList<BrokeredMessage> ?? messageEnumerable.ToList();
                            isCompleted = messageEnumerable == null || !messageList.Any();
                            if (isCompleted)
                            {
                                continue;
                            }
                            if (messageReceiver.Mode == ReceiveMode.PeekLock)
                            {
                                if (completeReceive)
                                {
                                    stopwatch = new Stopwatch();
                                    stopwatch.Start();
                                    messageReceiver.CompleteBatch(messageList.Select(b => b.LockToken));
                                    stopwatch.Stop();
                                    if (stopwatch.ElapsedMilliseconds > maximumCompleteTime)
                                    {
                                        maximumCompleteTime = stopwatch.ElapsedMilliseconds;
                                    }
                                    if (stopwatch.ElapsedMilliseconds < minimumCompleteTime)
                                    {
                                        minimumCompleteTime = stopwatch.ElapsedMilliseconds;
                                    }
                                    totalCompleteElapsedTime += stopwatch.ElapsedMilliseconds;
                                    messagesReceived += messageList.Count;
                                }
                            }
                            else
                            {
                                messagesReceived += messageList.Count;
                            }
                            if (stopwatch.ElapsedMilliseconds > maximumReceiveTime)
                            {
                                maximumReceiveTime = stopwatch.ElapsedMilliseconds;
                            }
                            if (stopwatch.ElapsedMilliseconds < minimumReceiveTime)
                            {
                                minimumReceiveTime = stopwatch.ElapsedMilliseconds;
                            }
                            totalReceiveElapsedTime += stopwatch.ElapsedMilliseconds;
                            if (statistics)
                            {
                                if (messageReceiver.PrefetchCount > 0)
                                {
                                    if (stopwatch.ElapsedMilliseconds == 0)
                                    {
                                        fetchedMessages += messageList.Count;
                                    }
                                    else
                                    {
                                        if (fetchedMessages > 0)
                                        {
                                            updateStatistics(fetchedMessages,
                                                             prefetchElapsedTime,
                                                             DirectionType.Receive);
                                            fetchedMessages = messageList.Count;
                                        }
                                        else
                                        {
                                            fetchedMessages += messageList.Count;
                                        }
                                        prefetchElapsedTime = stopwatch.ElapsedMilliseconds;
                                    }
                                }
                                else
                                {
                                    updateStatistics(messageList.Count, stopwatch.ElapsedMilliseconds, DirectionType.Receive);
                                }
                            }
                            builder = new StringBuilder();

                            if (logging)
                            {
                                // ReSharper disable ForCanBeConvertedToForeach
                                for (var i = 0; i < messageList.Count; i++)
                                // ReSharper restore ForCanBeConvertedToForeach
                                {
                                    if (messageReceiver.Mode == ReceiveMode.PeekLock &&
                                        !completeReceive)
                                    {

                                        builder.AppendLine(string.Format(MessagePeekedButNotConsumed,
                                                                         taskId,
                                                                         string.IsNullOrWhiteSpace(messageList[i].MessageId)
                                                                             ? NullValue
                                                                             : messageList[i].MessageId,
                                                                         string.IsNullOrWhiteSpace(messageList[i].SessionId)
                                                                             ? NullValue
                                                                             : messageList[i].SessionId,
                                                                         string.IsNullOrWhiteSpace(messageList[i].Label)
                                                                             ? NullValue
                                                                             : messageList[i].Label,
                                                                         messageList[i].Size));
                                    }
                                    else
                                    {
                                        builder.AppendLine(string.Format(MessageSuccessfullyReceived,
                                                                         taskId,
                                                                         string.IsNullOrWhiteSpace(messageList[i].MessageId)
                                                                             ? NullValue
                                                                             : messageList[i].MessageId,
                                                                         string.IsNullOrWhiteSpace(messageList[i].SessionId)
                                                                             ? NullValue
                                                                             : messageList[i].SessionId,
                                                                         string.IsNullOrWhiteSpace(messageList[i].Label)
                                                                             ? NullValue
                                                                             : messageList[i].Label,
                                                                         messageList[i].Size,
                                                                         messageList[i].DeliveryCount));
                                    }
                                    if (verbose)
                                    {
                                        GetMessageAndProperties(builder, messageList[i], encoder);
                                    }
                                }
                                traceMessage = builder.ToString();
                                WriteToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                            }
                        }
                        catch (Exception ex)
                        {
                            if (messageList != null &&
                                messageList.Count > 0 &&
                                messageReceiver.Mode == ReceiveMode.PeekLock)
                            {
                                try
                                {
                                    var stopwatch = new Stopwatch();
                                    stopwatch.Start();
                                    messageReceiver.CompleteBatch(messageList.Select(b => b.LockToken));
                                    stopwatch.Stop();
                                    if (stopwatch.ElapsedMilliseconds > maximumCompleteTime)
                                    {
                                        maximumCompleteTime = stopwatch.ElapsedMilliseconds;
                                    }
                                    if (stopwatch.ElapsedMilliseconds < minimumCompleteTime)
                                    {
                                        minimumCompleteTime = stopwatch.ElapsedMilliseconds;
                                    }
                                    totalCompleteElapsedTime += stopwatch.ElapsedMilliseconds;
                                }
                                // ReSharper disable EmptyGeneralCatchClause
                                catch (Exception)
                                // ReSharper restore EmptyGeneralCatchClause
                                {
                                }
                            }
                            exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                            isCompleted = true;
                            ok = false;
                        }
                        finally
                        {
                            if (messageList != null &&
                                messageList.Count > 0)
                            {
                                foreach (var message in messageList)
                                {
                                    message.Dispose();
                                }
                            }
                        }
                        if (receiverThinkTime)
                        {
                            WriteToLog(string.Format(SleepingFor, thinkTime));
                            Thread.Sleep(thinkTime);
                        }
                    }
                }
                else
                {
                    while (!isCompleted &&
                           !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            var stopwatch = new Stopwatch();
                            var movedToDeadLetterQueue = false;
                            var deferredMessage = false;
                            var readDeferredMessage = false;

                            if (!readingDeferredMessages)
                            {
                                stopwatch.Start();
                                inboundMessage = messageReceiver.Receive(TimeSpan.FromSeconds(timeout));
                                stopwatch.Stop();
                                isCompleted = inboundMessage == null &&
                                              messageDeferProvider.Count == 0;
                            }
                            else
                            {
                                isCompleted = messageDeferProvider.Count == 0;
                                if (!isCompleted)
                                {
                                    long sequenceNumber;
                                    if (messageDeferProvider.Dequeue(out sequenceNumber))
                                    {
                                        stopwatch.Start();
                                        inboundMessage = messageReceiver.Receive(sequenceNumber);
                                        stopwatch.Stop();
                                        readDeferredMessage = true;
                                    }
                                }
                            }
                            if (!readingDeferredMessages)
                            {
                                readingDeferredMessages = inboundMessage == null && messageDeferProvider.Count > 0;
                            }

                            if (isCompleted ||
                                inboundMessage == null)
                            {
                                continue;
                            }
                            if (stopwatch.ElapsedMilliseconds > maximumReceiveTime)
                            {
                                maximumReceiveTime = stopwatch.ElapsedMilliseconds;
                            }
                            if (stopwatch.ElapsedMilliseconds < minimumReceiveTime)
                            {
                                minimumReceiveTime = stopwatch.ElapsedMilliseconds;
                            }
                            totalReceiveElapsedTime += stopwatch.ElapsedMilliseconds;
                            if (statistics)
                            {
                                if (messageReceiver.PrefetchCount > 0)
                                {
                                    if (stopwatch.ElapsedMilliseconds == 0)
                                    {
                                        fetchedMessages++;
                                    }
                                    else
                                    {
                                        if (fetchedMessages > 0)
                                        {
                                            updateStatistics(fetchedMessages,
                                                             prefetchElapsedTime,
                                                             DirectionType.Receive);
                                            fetchedMessages = 1;
                                        }
                                        else
                                        {
                                            fetchedMessages++;
                                        }
                                        prefetchElapsedTime = stopwatch.ElapsedMilliseconds;
                                    }
                                }
                                else
                                {
                                    updateStatistics(1, stopwatch.ElapsedMilliseconds, DirectionType.Receive);
                                }
                            }
                            builder = new StringBuilder();

                            if (defer &&
                                !readingDeferredMessages &&
                                filter != null &&
                                filter.Match(inboundMessage))
                            {
                                inboundMessage.Defer();
                                messageDeferProvider.Enqueue(inboundMessage.SequenceNumber);
                                deferredMessage = true;
                            }

                            if (!deferredMessage &&
                                moveToDeadLetterQueue &&
                                filter != null &&
                                filter.Match(inboundMessage))
                            {
                                inboundMessage.DeadLetter();
                                movedToDeadLetterQueue = true;
                                messagesReceived++;
                            }


                            if (!deferredMessage &&
                                !movedToDeadLetterQueue)
                            {
                                if (messageReceiver.Mode == ReceiveMode.PeekLock)
                                {
                                    if (completeReceive)
                                    {
                                        stopwatch = new Stopwatch();
                                        stopwatch.Start();
                                        inboundMessage.Complete();
                                        stopwatch.Stop();
                                        if (stopwatch.ElapsedMilliseconds > maximumCompleteTime)
                                        {
                                            maximumCompleteTime = stopwatch.ElapsedMilliseconds;
                                        }
                                        if (stopwatch.ElapsedMilliseconds < minimumCompleteTime)
                                        {
                                            minimumCompleteTime = stopwatch.ElapsedMilliseconds;
                                        }
                                        totalCompleteElapsedTime += stopwatch.ElapsedMilliseconds;
                                        messagesReceived++;
                                    }
                                    else
                                    {
                                        stopwatch = new Stopwatch();
                                        inboundMessage.Abandon();
                                        stopwatch.Stop();
                                        if (stopwatch.ElapsedMilliseconds > maximumCompleteTime)
                                        {
                                            maximumCompleteTime = stopwatch.ElapsedMilliseconds;
                                        }
                                        if (stopwatch.ElapsedMilliseconds < minimumCompleteTime)
                                        {
                                            minimumCompleteTime = stopwatch.ElapsedMilliseconds;
                                        }
                                        totalCompleteElapsedTime += stopwatch.ElapsedMilliseconds;
                                    }
                                }
                                else
                                {
                                    messagesReceived++;
                                }
                            }

                            if (logging)
                            {
                                if (messageReceiver.Mode == ReceiveMode.PeekLock &&
                                    !completeReceive &&
                                    !deferredMessage &&
                                    !movedToDeadLetterQueue)
                                {
                                    builder.AppendLine(string.Format(MessagePeekedButNotConsumed,
                                                                     taskId,
                                                                     string.IsNullOrWhiteSpace(inboundMessage.MessageId)
                                                                         ? NullValue
                                                                         : inboundMessage.MessageId,
                                                                     string.IsNullOrWhiteSpace(inboundMessage.SessionId)
                                                                         ? NullValue
                                                                         : inboundMessage.SessionId,
                                                                     string.IsNullOrWhiteSpace(inboundMessage.Label)
                                                                         ? NullValue
                                                                         : inboundMessage.Label,
                                                                     inboundMessage.Size));
                                }
                                else
                                {
                                    builder.AppendLine(string.Format(MessageSuccessfullyReceived,
                                                                     taskId,
                                                                     string.IsNullOrWhiteSpace(inboundMessage.MessageId)
                                                                         ? NullValue
                                                                         : inboundMessage.MessageId,
                                                                     string.IsNullOrWhiteSpace(inboundMessage.SessionId)
                                                                         ? NullValue
                                                                         : inboundMessage.SessionId,
                                                                     string.IsNullOrWhiteSpace(inboundMessage.Label)
                                                                         ? NullValue
                                                                         : inboundMessage.Label,
                                                                     inboundMessage.Size,
                                                                     inboundMessage.DeliveryCount));
                                }
                                if (deferredMessage)
                                {
                                    builder.AppendLine(MessageDeferred);
                                }
                                if (readDeferredMessage)
                                {
                                    builder.AppendLine(ReadMessageDeferred);
                                }
                                if (movedToDeadLetterQueue)
                                {
                                    builder.AppendLine(MessageMovedToDeadLetterQueue);
                                }
                                if (receivedFromDeadLetterQueue)
                                {
                                    builder.AppendLine(MessageReadFromDeadLetterQueue);
                                }
                                if (verbose)
                                {
                                    GetMessageAndProperties(builder, inboundMessage, encoder);
                                }
                            }

                            if (logging)
                            {
                                traceMessage = builder.ToString();
                                WriteToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                            }
                        }
                        catch (Exception ex)
                        {
                            if (inboundMessage != null &&
                                messageReceiver.Mode == ReceiveMode.PeekLock)
                            {
                                try
                                {
                                    inboundMessage.Abandon();
                                }
                                // ReSharper disable EmptyGeneralCatchClause
                                catch (Exception)
                                // ReSharper restore EmptyGeneralCatchClause
                                {
                                }
                            }
                            exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                            isCompleted = true;
                            ok = false;
                        }
                        finally
                        {
                            if (inboundMessage != null)
                            {
                                inboundMessage.Dispose();
                            }
                        }
                        if (receiverThinkTime)
                        {
                            WriteToLog(string.Format(SleepingFor, thinkTime));
                            Thread.Sleep(thinkTime);
                        }
                    }
                }

                if (messageReceiver.PrefetchCount > 0 && fetchedMessages > 0 && prefetchElapsedTime > 0)
                {
                    updateStatistics(fetchedMessages, prefetchElapsedTime, DirectionType.Receive);
                }
            }
            catch (Exception ex)
            {
                exceptionMessage = string.Format(ExceptionOccurred, ex.Message);
                ok = false;
            }
            if (messagesReceived == 0)
            {
                WriteToLog(string.Format(NoMessageWasReceived, taskId));
            }
            var averageReceiveTime = messagesReceived > 0 ? totalReceiveElapsedTime / messagesReceived : maximumReceiveTime;
            var averageCompleteTime = messagesReceived > 0 ? totalCompleteElapsedTime / messagesReceived : maximumCompleteTime;
            // ReSharper disable RedundantCast
            var messagesPerSecond = totalReceiveElapsedTime > 0 ? (double)(messagesReceived * 1000) / (double)totalReceiveElapsedTime : 0;
            // ReSharper restore RedundantCast
            builder = new StringBuilder();
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             ReceiverStatisticsHeader,
                                             taskId));
            if (!string.IsNullOrWhiteSpace(exceptionMessage))
            {
                builder.AppendLine(exceptionMessage);
            }
            if (messageReceiver.Mode == ReceiveMode.ReceiveAndDelete)
            {
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                 ReceiverStatitiscsLine1,
                                                 messagesReceived,
                                                 messagesPerSecond,
                                                 totalReceiveElapsedTime));
            }
            else
            {
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                 ReceiverStatitiscsWithCompleteLine1,
                                                 messagesReceived,
                                                 messagesPerSecond,
                                                 totalReceiveElapsedTime,
                                                 totalCompleteElapsedTime));
            }
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             ReceiverStatitiscsLine2,
                                             averageReceiveTime,
                                             minimumReceiveTime == long.MaxValue ? 0 : minimumReceiveTime,
                                             maximumReceiveTime));
            if (messageReceiver.Mode == ReceiveMode.PeekLock)
            {
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             ReceiverStatitiscsLine3,
                                             averageCompleteTime,
                                             minimumCompleteTime == long.MaxValue ? 0 : minimumCompleteTime,
                                             maximumCompleteTime));
            }
            traceMessage = builder.ToString();
            return ok;
        }

        /// <summary>
        /// Exports the entities contained in the list passed a parameter.
        /// </summary>
        /// <param name="entityDescriptionList">The list of the entities to export.</param>
        /// <returns>The xml string representing the entity list.</returns>
        public string ExportEntities(List<EntityDescription> entityDescriptionList)
        {
            return ImportExportHelper.ReadAndSerialize(this, entityDescriptionList);
        }

        /// <summary>
        /// Imports entities from a xml string.
        /// </summary>
        /// <param name="xml">The xml containing entities.</param>
        /// <returns>The description of the newly created queue.</returns>
        public void ImportEntities(string xml)
        {
            ImportExportHelper.DeserializeAndCreate(this, xml);
        }

        /// <summary>
        /// Reads the content of the message passed as argument.
        /// </summary>
        /// <param name="messageToRead">The brokered message to read.</param>
        /// <param name="bodyType">BodyType</param>
        /// <returns>The content of the message.</returns>
        public string GetMessageText(BrokeredMessage messageToRead, out BodyType bodyType)
        {
            string messageText = null;
            Stream stream = null;
            bodyType = BodyType.Stream;
            if (messageToRead == null)
            {
                return null;
            }
            var inboundMessage = messageToRead.Clone();
            try
            {
                stream = inboundMessage.GetBody<Stream>();
                if (stream != null)
                {
                    var element = new BinaryMessageEncodingBindingElement
                    {
                        ReaderQuotas = new XmlDictionaryReaderQuotas
                        {
                            MaxArrayLength = int.MaxValue,
                            MaxBytesPerRead = int.MaxValue,
                            MaxDepth = int.MaxValue,
                            MaxNameTableCharCount = int.MaxValue,
                            MaxStringContentLength = int.MaxValue
                        }
                    };
                    var encoderFactory = element.CreateMessageEncoderFactory();
                    var encoder = encoderFactory.Encoder;
                    var stringBuilder = new StringBuilder();
                    var message = encoder.ReadMessage(stream, MaxBufferSize);
                    using (var reader = message.GetReaderAtBodyContents())
                    {
                        // The XmlWriter is used just to indent the XML message
                        var settings = new XmlWriterSettings { Indent = true };
                        using (var writer = XmlWriter.Create(stringBuilder, settings))
                        {
                            writer.WriteNode(reader, true);
                        }
                    }
                    messageText = stringBuilder.ToString();
                    bodyType = BodyType.Wcf;
                }
            }
            catch (Exception)
            {
                inboundMessage = messageToRead.Clone();
                try
                {
                    stream = inboundMessage.GetBody<Stream>();
                    if (stream != null)
                    {
                        var element = new BinaryMessageEncodingBindingElement
                        {
                            ReaderQuotas = new XmlDictionaryReaderQuotas
                            {
                                MaxArrayLength = int.MaxValue,
                                MaxBytesPerRead = int.MaxValue,
                                MaxDepth = int.MaxValue,
                                MaxNameTableCharCount = int.MaxValue,
                                MaxStringContentLength = int.MaxValue
                            }
                        };
                        var encoderFactory = element.CreateMessageEncoderFactory();
                        var encoder = encoderFactory.Encoder;
                        var message = encoder.ReadMessage(stream, MaxBufferSize);
                        using (var reader = message.GetReaderAtBodyContents())
                        {
                            messageText = reader.ReadString();
                        }
                        bodyType = BodyType.Wcf;
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        if (stream != null)
                        {
                            try
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                var serializer = new CustomDataContractBinarySerializer(typeof(string));
                                messageText = serializer.ReadObject(stream) as string;
                                bodyType = BodyType.String;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    stream.Seek(0, SeekOrigin.Begin);
                                    using (var reader = new StreamReader(stream))
                                    {
                                        messageText = reader.ReadToEnd();
                                    }
                                }
                                catch (Exception)
                                {
                                    messageText = UnableToReadMessageBody;
                                }
                            }
                        }
                        else
                        {
                            messageText = UnableToReadMessageBody;
                        }
                    }
                    catch (Exception)
                    {
                        messageText = UnableToReadMessageBody;
                    }
                }
            }
            return messageText;
        }

        /// <summary>
        /// This method is used to receive message from a queue or a subscription.
        /// </summary>
        /// <param name="entityDescription">The description of the entity from which to read messages.</param>
        /// <param name="messageCount">The number of messages to read.</param>
        /// <param name="complete">This parameter indicates whether to complete the receive operation.</param>
        /// <param name="deadletterQueue">This parameter indicates whether to read messages from the deadletter queue.</param>
        /// <param name="receiveTimeout">Receive receiveTimeout.</param>
        /// <param name="sessionTimeout">Session timeout</param>
        /// <param name="cancellationTokenSource">Cancellation token source.</param>
        public void ReceiveMessages(EntityDescription entityDescription, int? messageCount, bool complete, bool deadletterQueue, TimeSpan receiveTimeout, TimeSpan sessionTimeout, CancellationTokenSource cancellationTokenSource)
        {
            var receiverList = new List<MessageReceiver>();
            if (brokeredMessageList != null &&
                brokeredMessageList.Count > 0)
            {
                brokeredMessageList.ForEach(b => b.Dispose());
            }
            brokeredMessageList = new List<BrokeredMessage>();
            MessageEncodingBindingElement element;
            if (scheme == DefaultScheme)
            {
                element = new BinaryMessageEncodingBindingElement
                {
                    ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxArrayLength = int.MaxValue,
                        MaxBytesPerRead = int.MaxValue,
                        MaxDepth = int.MaxValue,
                        MaxNameTableCharCount = int.MaxValue,
                        MaxStringContentLength = int.MaxValue
                    }
                };
            }
            else
            {
                element = new TextMessageEncodingBindingElement
                {
                    ReaderQuotas = new XmlDictionaryReaderQuotas
                    {
                        MaxArrayLength = int.MaxValue,
                        MaxBytesPerRead = int.MaxValue,
                        MaxDepth = int.MaxValue,
                        MaxNameTableCharCount = int.MaxValue,
                        MaxStringContentLength = int.MaxValue
                    }
                };
            }
            var encoderFactory = element.CreateMessageEncoderFactory();
            var encoder = encoderFactory.Encoder;

            MessageReceiver messageReceiver = null;
            if (entityDescription is QueueDescription)
            {
                var queueDescription = entityDescription as QueueDescription;
                if (deadletterQueue)
                {
                    messageReceiver = messagingFactory.CreateMessageReceiver(QueueClient.FormatDeadLetterPath(queueDescription.Path),
                                                                             ReceiveMode.PeekLock);
                }
                else
                {
                    if (queueDescription.RequiresSession)
                    {
                        var queueClient = messagingFactory.CreateQueueClient(queueDescription.Path,
                                                                             ReceiveMode.PeekLock);
                        messageReceiver = queueClient.AcceptMessageSession(sessionTimeout);
                    }
                    else
                    {
                        messageReceiver = messagingFactory.CreateMessageReceiver(queueDescription.Path,
                                                                                 ReceiveMode.PeekLock);
                    }
                }
            }
            else
            {
                if (entityDescription is SubscriptionDescription)
                {
                    var subscriptionDescription = entityDescription as SubscriptionDescription;
                    if (deadletterQueue)
                    {
                        messageReceiver = messagingFactory.CreateMessageReceiver(SubscriptionClient.FormatDeadLetterPath(subscriptionDescription.TopicPath,
                                                                                                                            subscriptionDescription.Name),
                                                                                    ReceiveMode.PeekLock);
                    }
                    else
                    {
                        if (subscriptionDescription.RequiresSession)
                        {
                            var subscriptionClient = messagingFactory.CreateSubscriptionClient(subscriptionDescription.TopicPath,
                                                                                                subscriptionDescription.Name,
                                                                                                ReceiveMode.PeekLock);
                            messageReceiver = subscriptionClient.AcceptMessageSession(sessionTimeout);
                        }
                        else
                        {
                            messageReceiver = messagingFactory.CreateMessageReceiver(SubscriptionClient.FormatSubscriptionPath(subscriptionDescription.TopicPath,
                                                                                                                                subscriptionDescription.Name),
                                                                                        ReceiveMode.PeekLock);
                        }
                    }
                }
            }
            if (messageReceiver != null)
            {
                messageReceiver.PrefetchCount = 0;
                receiverList.Add(messageReceiver);
                ReceiveNextMessage(messageCount, 0, messageReceiver, ReceiveCallback, encoder, complete, receiveTimeout, cancellationTokenSource.Token);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Writes the specified message to the trace listener.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        private void HandleException(Exception ex)
        {
            if (ex == null || string.IsNullOrWhiteSpace(ex.Message))
            {
                return;
            }
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
            WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, StackTraceFormat, ex.StackTrace));
        }



        /// <summary>
        /// Gets a new messaging factory object.
        /// </summary>
        /// <returns>A messaging factory object.</returns>
        private MessagingFactory GetMessagingFactory()
        {
            // The MessagingFactorySettings specifies the service bus messaging factory settings.
            var messagingFactorySettings = new MessagingFactorySettings
            {
                TokenProvider = tokenProvider,
                OperationTimeout = TimeSpan.FromMinutes(5)
            };
            // In the first release of the service bus, the only available transport protocol is sb 
            if (scheme == DefaultScheme)
            {
                messagingFactorySettings.NetMessagingTransportSettings = new NetMessagingTransportSettings();
            }

            // As the name suggests, the MessagingFactory class is a Factory class that allows to create
            // instances of the QueueClient, TopicClient and SubscriptionClient classes.
            var factory = MessagingFactory.Create(namespaceUri, messagingFactorySettings);
            WriteToLogIf(traceEnabled, MessageFactorySuccessfullyCreated);
            return factory;
        }

        /// <summary>
        /// Receives a message from a queue or a subscription.
        /// </summary>
        /// <param name="messageCount">The message count.</param>
        /// <param name="messageTotal">The total number of messages read.</param>
        /// <param name="messageReceiver">The message receiver used to receive messages.</param>
        /// <param name="complete">Call Complete method to delete the message.</param>
        /// <param name="callback">The callback function invoked when a message is received.</param>
        /// <param name="encoder">MessageEncoder used to decode a WCF message.</param>
        /// <param name="timeout">The receive receiveTimeout.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        private void ReceiveNextMessage(int? messageCount, int messageTotal, MessageReceiver messageReceiver, Func<IAsyncResult, BrokeredMessage> callback, MessageEncoder encoder, bool complete, TimeSpan timeout, CancellationToken cancellationToken)
        {
            Task.Factory.FromAsync(messageReceiver.BeginReceive,
                                   callback,
                                   timeout,
                                   messageReceiver,
                                   TaskCreationOptions.None).
                                   ContinueWith(taskResult =>
                                   {
                                       // Start receiving the next message as soon as we 
                                       // received the previous one. 
                                       // This will not cause a stack overflow because the 
                                       // call will be made from a new Task. 
                                       if (taskResult.Exception != null)
                                       {
                                           Console.WriteLine(taskResult.Exception.ToString());
                                       }
                                       var inboundMessage = taskResult.Result;
                                       if (inboundMessage == null ||
                                           messageCount.HasValue && messageCount == 0)
                                       {
                                           if (brokeredMessageList != null &&
                                               brokeredMessageList.Count > 0)
                                           {
                                               brokeredMessageList.ForEach(b =>
                                                                               {
                                                                                   try
                                                                                   {
                                                                                       if (complete)
                                                                                       {
                                                                                           b.Complete();
                                                                                       }
                                                                                       else
                                                                                       {
                                                                                           b.Abandon();
                                                                                       }
                                                                                   }
                                                                                   catch (MessageLockLostException)
                                                                                   {
                                                                                   }
                                                                                    // ReSharper disable EmptyGeneralCatchClause
                                                                                   catch (Exception)
                                                                                    // ReSharper restore EmptyGeneralCatchClause
                                                                                   {
                                                                                   }

                                                                               });
                                               brokeredMessageList = null;
                                           }
                                           var builder = new StringBuilder();
                                           builder.AppendLine(string.Format(ReceiverStatitiscsLineNoTask,
                                                                            complete ? Read : Peeked,
                                                                            messageTotal));
                                           var traceMessage = builder.ToString();
                                           WriteToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                       }
                                       else
                                       {
                                           messageCount--;
                                           messageTotal++;
                                           var builder = new StringBuilder();
                                           builder.AppendLine(string.Format(MessageSuccessfullyReceivedNoTask,
                                                                            complete ? Read :Peeked,
                                                                            string.IsNullOrWhiteSpace(
                                                                                inboundMessage.MessageId)
                                                                                ? NullValue
                                                                                : inboundMessage.MessageId,
                                                                            string.IsNullOrWhiteSpace(
                                                                                inboundMessage.SessionId)
                                                                                ? NullValue
                                                                                : inboundMessage.SessionId,
                                                                            string.IsNullOrWhiteSpace(inboundMessage.Label)
                                                                                ? NullValue
                                                                                : inboundMessage.Label,
                                                                            inboundMessage.Size,
                                                                            inboundMessage.DeliveryCount));

                                           GetMessageAndProperties(builder, inboundMessage, encoder);
                                           var traceMessage = builder.ToString();
                                           WriteToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                           brokeredMessageList.Add(inboundMessage);
                                           ReceiveNextMessage(messageCount, messageTotal, messageReceiver, callback, encoder, complete, timeout, cancellationToken);
                                       }
                                   }, cancellationToken);
        }

        /// <summary>
        /// Receive callback
        /// </summary>
        /// <param name="asyncResult">AsyncResult object used to complete the asynchronous call.</param>
        /// <returns></returns>
        private BrokeredMessage ReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                var messageReceiver = asyncResult.AsyncState as MessageReceiver;
                if (messageReceiver != null)
                {
                    var bm = messageReceiver.EndReceive(asyncResult);
                    return bm;
                }
                return null;
            }
            catch (TimeoutException)
            {
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets the message body and properties.
        /// </summary>
        /// <param name="builder">The string builder object used to accumulate the trace message.</param>
        /// <param name="inboundMessage">The inbound message.</param>
        /// <param name="encoder">The message encoder used to decode a WCF message.</param>
        public void GetMessageAndProperties(StringBuilder builder, BrokeredMessage inboundMessage, MessageEncoder encoder)
        {
            string messageText = null;
            Stream stream = null;

            try
            {
                var messageClone = inboundMessage.Clone();
                stream = messageClone.GetBody<Stream>();
                if (stream != null)
                {
                    var stringBuilder = new StringBuilder();
                    var message = encoder.ReadMessage(stream, MaxBufferSize);
                    using (var reader = message.GetReaderAtBodyContents())
                    {
                        // The XmlWriter is used just to indent the XML message
                        var settings = new XmlWriterSettings {Indent = true};
                        using (var writer = XmlWriter.Create(stringBuilder, settings))
                        {
                            writer.WriteNode(reader, true);
                        }
                    }
                    messageText = stringBuilder.ToString();
                }
            }
            catch (Exception)
            {
                try
                {
                    var messageClone = inboundMessage.Clone();
                    stream = messageClone.GetBody<Stream>();
                    if (stream != null)
                    {
                        var message = encoder.ReadMessage(stream, MaxBufferSize);
                        using (var reader = message.GetReaderAtBodyContents())
                        {
                            messageText = reader.ReadString();
                        }
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        if (stream != null)
                        {
                            try
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                var serializer = new CustomDataContractBinarySerializer(typeof(string));
                                messageText = serializer.ReadObject(stream) as string;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    stream.Seek(0, SeekOrigin.Begin);
                                    using (var reader = new StreamReader(stream))
                                    {
                                        messageText = reader.ReadToEnd();
                                    }
                                }
                                catch (Exception)
                                {
                                    messageText = UnableToReadMessageBody;
                                }
                            }
                        }
                        else
                        {
                            messageText = UnableToReadMessageBody;
                        }
                    }
                    catch (Exception)
                    {
                        messageText = UnableToReadMessageBody;
                    }
                }
            }
            builder.AppendLine(ReceivedMessagePayloadHeader);
            builder.AppendLine(string.Format(MessageTextFormat, messageText));
            if (inboundMessage.Properties.Any())
            {
                builder.AppendLine(ReceivedMessagePropertiesHeader);
                foreach (var p in inboundMessage.Properties)
                {
                    builder.AppendLine(string.Format(MessagePropertyFormat,
                                                     p.Key,
                                                     p.Value));
                }
            }
        }

        private void WriteToLog(string message)
        {
            if (writeToLog != null &&
                !string.IsNullOrWhiteSpace(message))
            {
                writeToLog(message);
            }
        }

        private void WriteToLogIf(bool condition, string message, bool async = false)
        {
            if (condition &&
                writeToLog != null &&
                !string.IsNullOrWhiteSpace(message))
            {
                writeToLog(message, async);
            }
        }

        private static Encoding GetEncoding()
        {
            switch (encodingType)
            {
                case EncodingType.ASCII:
                    return Encoding.ASCII;
                case EncodingType.UTF7:
                    return Encoding.UTF7;
                case EncodingType.UTF8:
                    return Encoding.UTF8;
                case EncodingType.UTF32:
                    return Encoding.UTF32;
                case EncodingType.Unicode:
                    return Encoding.Unicode;
                default:
                    return Encoding.UTF8;
            }
        }
        #endregion
    }
}