﻿#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/.
//
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using Azure.Messaging.ServiceBus.Administration;

using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.ServiceBus.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using ServiceBusExplorer.WindowsAzure;

using AzureNotificationHubs = Microsoft.Azure.NotificationHubs;
#endregion

namespace ServiceBusExplorer
{
    using System.IO.Compression;
    using System.Web.UI.WebControls;
    using Abstractions;
    using ServiceBusConnectionStringBuilder = Microsoft.ServiceBus.ServiceBusConnectionStringBuilder;

    public enum BodyType
    {
        Stream,
        String,
        Wcf,
        ByteArray
    }

    public class ServiceBusHelper
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string DefaultScheme = "sb";
        private const string MessageNumber = "MessageNumber";
        private const string StringType = "String";
        private const string DeadLetterQueue = "$DeadLetterQueue";
        private const string NullValue = "NULL";
        private const string CloudServiceBusPostfix = ".servicebus.windows.net";
        private const string GermanyServiceBusPostfix = ".servicebus.cloudapi.de";
        private const string ChinaServiceBusPostfix = ".servicebus.chinacloudapi.cn";
        private const string TestServiceBusPostFix = ".servicebus.int7.windows-int.net";
        private const int MaxBufferSize = 262144; // 256 KB


        //***************************
        // Messages
        //***************************
        private const string ServiceBusConnectionStringCannotBeNull = "The connection string argument cannot be null.";
        private const string ServiceBusIsConnected = "The application is now connected to the {0} service bus namespace.";
        private const string WarningHeader = "The following validations failed:";
        private const string WarningFormat = "\n\r - {0}";
        private const string PropertyConversionError = "{0} property conversion error: {1}";
        private const string PropertyValueCannotBeNull = "The value of the {0} property cannot be null.";
        private const string MessageSuccessfullySent = "Sender[{0}]:   Message sent. MessageId=[{1}] SessionId=[{2}] Label=[{3}] Size=[{4}]";
        private const string MessageSuccessfullyReceived = "Receiver[{0}]: Message received. MessageId=[{1}] SessionId=[{2}] Label=[{3}] Size=[{4}] DeliveryCount[{5}]";
        private const string MessagePeekedButNotConsumed = "Receiver[{0}]: Message peeked, but not consumed. MessageId=[{1}] SessionId=[{2}] Label=[{3}] Size=[{4}]";
        private const string MessageSuccessfullyReceivedNoTask = "Message {0}: MessageId=[{1}] SessionId=[{2}] Label=[{3}] Size=[{4}] DeliveryCount[{5}]";
        private const string EventDataSuccessfullySent = "Sender[{0}]: EventData sent. MessageNumber=[{1}] PartitionKey=[{2}]";
        private const string ReceiverStatisticsLineNoTask = "Messages {0}: Count=[{1}]";
        private const string SentMessagePropertiesHeader = "Properties:";
        private const string ReceivedMessagePropertiesHeader = "Properties:";
        private const string SentMessagePayloadHeader = "Payload:";
        private const string ReceivedMessagePayloadHeader = "Payload:";
        private const string MessageTextFormat = "{0}";
        private const string MessagePropertyFormat = " - Key=[{0}] Value=[{1}]";
        private const string MessageDeferred = " - The message was deferred.";
        private const string ReadMessageDeferred = " - Read deferred message.";
        private const string MessageMovedToDeadLetterQueue = " - The message was moved to the Dead-letter queue.";
        private const string MessageReadFromDeadLetterQueue = " - The message was read from the Dead-letter queue.";
        private const string NoMessageWasReceived = "Receiver[{0}]: no message was received.";
        private const string SenderStatisticsHeader = "Sender[{0}]:";
        private const string SenderStatisticsLine1 = " - Message Count=[{0}] Messages Sent/Sec=[{1:F1}] Total Elapsed Time (ms)=[{2}]";
        private const string SenderStatisticsLine2 = " - Average Send Time (ms)=[{0}] Minimum Send Time (ms)=[{1}] Maximum Send Time (ms)=[{2}] ";
        private const string ReceiverStatisticsHeader = "Receiver[{0}]:";
        private const string ReceiverStatisticsLine1 = " - Message Count=[{0}] Messages Read/Sec=[{1:F1}] Total Elapsed Time (ms)=[{2}]";
        private const string ReceiverStatisticsWithCompleteLine1 = " - Message Count=[{0}] Messages Read/Sec=[{1:F1}] Total Receive Elapsed Time (ms)=[{2}] Total Complete Elapsed Time (ms)=[{3}]";
        private const string ReceiverStatisticsLine2 = " - Average Receive Time (ms)=[{0}] Minimum Receive Time (ms)=[{1}] Maximum Receive Time (ms)=[{2}] ";
        private const string ReceiverStatisticsLine3 = " - Average Complete Time (ms)=[{0}] Minimum Complete Time (ms)=[{1}] Maximum Complete Time (ms)=[{2}] ";
        private const string ExceptionOccurred = " - Exception occurred: {0}";
        private const string UnableToReadMessageBody = "Unable to read the message body.";
        private const string EventHubClientCannotBeNull = "The EventHubClient parameter cannot be null.";
        private const string EventHubSenderCannotBeNull = "The EventHubSender parameter cannot be null.";
        private const string MessageSenderCannotBeNull = "The MessageSender parameter cannot be null.";
        private const string MessageReceiverCannotBeNull = "The MessageReceiver parameter cannot be null.";
        private const string BrokeredMessageCannotBeNull = "The BrokeredMessage parameter cannot be null.";
        private const string EventDataCannotBeNull = "The EventData parameter cannot be null.";
        private const string EventDataTemplateEnumerableCannotBeNull = "The eventDataTemplateEnumerable parameter cannot be null.";
        private const string CancellationTokenSourceCannotBeNull = "The CancellationTokenSource parameter cannot be null.";
        private const string MessageIsNotXmlOrJson = "The message is not in XML or JSON format.";
        private const string MessageFactorySuccessfullyCreated = "MessagingFactory successfully created.";
        private const string SleepingFor = "Sleeping for [{0}] milliseconds...";
        private const string Read = "read";
        private const string Peeked = "peeked";
        #endregion

        #region Private Fields
        private Type messageDeferProviderType = typeof(InMemoryMessageDeferProvider);
        private Microsoft.ServiceBus.NamespaceManager namespaceManager;
        private AzureNotificationHubs.NamespaceManager notificationHubNamespaceManager;
        private MessagingFactory messagingFactory;
        private bool traceEnabled;
        private string scheme = DefaultScheme;
        private AzureNotificationHubs.TokenProvider notificationHubTokenProvider;
        private Uri namespaceUri;
        private ServiceBusNamespaceType connectionStringType;
        private string ns;
        private string connectionString;
        private List<BrokeredMessage> brokeredMessageList;
        private readonly WriteToLogDelegate writeToLog;
        private string currentSharedAccessKeyName;
        private string currentSharedAccessKey;
        private ServiceBusNamespace serviceBusNamespaceInstance;
        private IServiceBusQueue serviceBusQueue;
        private IServiceBusTopic serviceBusTopic;
        private IServiceBusSubscription serviceBusSubscription;
        private IServiceBusRule serviceBusRule;
        private IServiceBusRelay serviceBusRelay;
        private IServiceBusNotificationHub serviceBusNotificationHub;
        private IServiceBusEventHub serviceBusEventHub;
        private IServiceBusConsumerGroup serviceBusConsumerGroup;
        private IServiceBusPartition serviceBusPartition;
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
        /// <param name="serviceBusHelper">Base ServiceBusHelper.</param>
        public ServiceBusHelper(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper)
        {
            this.writeToLog = writeToLog;
            MessageDeferProviderType = serviceBusHelper.MessageDeferProviderType;
            connectionString = serviceBusHelper.ConnectionString;
            namespaceManager = serviceBusHelper.NamespaceManager;
            notificationHubNamespaceManager = serviceBusHelper.NotificationHubNamespaceManager;
            MessagingFactory = serviceBusHelper.MessagingFactory;
            Namespace = serviceBusHelper.Namespace;
            NamespaceUri = serviceBusHelper.NamespaceUri;
            MessageDeferProviderType = serviceBusHelper.MessageDeferProviderType;
            Scheme = serviceBusHelper.Scheme;
            ServiceBusNamespaces = serviceBusHelper.ServiceBusNamespaces;
            BrokeredMessageInspectors = serviceBusHelper.BrokeredMessageInspectors;
            EventDataInspectors = serviceBusHelper.EventDataInspectors;
            BrokeredMessageGenerators = serviceBusHelper.BrokeredMessageGenerators;
            EventDataGenerators = serviceBusHelper.EventDataGenerators;
            notificationHubTokenProvider = serviceBusHelper.notificationHubTokenProvider;
            TraceEnabled = serviceBusHelper.TraceEnabled;
            SharedAccessKey = serviceBusHelper.SharedAccessKey;
            SharedAccessKeyName = serviceBusHelper.SharedAccessKeyName;
            serviceBusQueue = serviceBusHelper.serviceBusQueue;
            serviceBusTopic = serviceBusHelper.serviceBusTopic;
            serviceBusSubscription = serviceBusHelper.serviceBusSubscription;
            serviceBusRule = serviceBusHelper.serviceBusRule;
            serviceBusRelay = serviceBusHelper.serviceBusRelay;
            serviceBusNotificationHub = serviceBusHelper.serviceBusNotificationHub;
            serviceBusEventHub = serviceBusHelper.serviceBusEventHub;
            serviceBusConsumerGroup = serviceBusHelper.serviceBusConsumerGroup;
            serviceBusPartition = serviceBusHelper.serviceBusPartition;
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
                return connectionStringType == ServiceBusNamespaceType.Cloud ||
                      (namespaceUri != null &&
                       !string.IsNullOrWhiteSpace(uri = namespaceUri.ToString()) &&
                       (uri.Contains(CloudServiceBusPostfix) ||
                        uri.Contains(TestServiceBusPostFix) ||
                        uri.Contains(GermanyServiceBusPostfix) ||
                        uri.Contains(ChinaServiceBusPostfix)));
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
                    if (serviceBusQueue != null)
                    {
                        serviceBusQueue.Scheme = scheme;
                    }

                    if (serviceBusTopic != null)
                    {
                        serviceBusTopic.Scheme = scheme;
                    }

                    if (serviceBusSubscription != null)
                    {
                        serviceBusSubscription.Scheme = scheme;
                    }

                    if (serviceBusRule != null)
                    {
                        serviceBusRule.Scheme = scheme;
                    }

                    if (serviceBusRelay != null)
                    {
                        serviceBusRelay.Scheme = scheme;
                    }

                    if (serviceBusNotificationHub != null)
                    {
                        serviceBusNotificationHub.Scheme = scheme;
                    }

                    if (serviceBusEventHub != null)
                    {
                        serviceBusEventHub.Scheme = scheme;
                    }

                    if (serviceBusConsumerGroup != null)
                    {
                        serviceBusConsumerGroup.Scheme = scheme;
                    }

                    if (serviceBusPartition != null)
                    {
                        serviceBusPartition.Scheme = scheme;
                    }
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
            private set
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
        public Microsoft.ServiceBus.NamespaceManager NamespaceManager
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
        /// Gets the current namespace manager.
        /// </summary>
        public AzureNotificationHubs.NamespaceManager NotificationHubNamespaceManager
        {
            get
            {
                lock (this)
                {
                    return notificationHubNamespaceManager;
                }
            }
        }

        private string ConnectionStringWithoutEntityPath
        {
            get
            {
                var builder = new ServiceBusConnectionStringBuilder(connectionString)
                {
                    EntityPath = string.Empty
                };

                return builder.ToString();
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
            private set
            {
                lock (this)
                {
                    connectionString = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets the shared access key name.
        /// </summary>
        public string SharedAccessKeyName
        {
            get
            {
                lock (this)
                {
                    return currentSharedAccessKeyName;
                }
            }
            set
            {
                lock (this)
                {
                    currentSharedAccessKeyName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the shared access key.
        /// </summary>
        public string SharedAccessKey
        {
            get
            {
                lock (this)
                {
                    return currentSharedAccessKey;
                }
            }
            set
            {
                lock (this)
                {
                    currentSharedAccessKey = value;
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
                        messagingFactory = CreateMessagingFactory();
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

        /// <summary>
        /// Gets or sets the dictionary containing BrokeredMessage inspectors.
        /// </summary>
        public Dictionary<string, Type> BrokeredMessageInspectors { get; set; }

        /// <summary>
        /// Gets or sets the dictionary containing EventData inspectors.
        /// </summary>
        public Dictionary<string, Type> EventDataInspectors { get; set; }

        /// <summary>
        /// Gets or sets the dictionary containing BrokeredMessage generators.
        /// </summary>
        public Dictionary<string, Type> BrokeredMessageGenerators { get; set; }

        /// <summary>
        /// Gets or sets the dictionary containing EventData generators.
        /// </summary>
        public Dictionary<string, Type> EventDataGenerators { get; set; }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Gets or sets the connectivity mode when connecting to namespaces
        /// </summary>
        public static Microsoft.ServiceBus.ConnectivityMode ConnectivityMode
        {
            get
            {
                return Microsoft.ServiceBus.ServiceBusEnvironment.SystemConnectivity.Mode;
            }
            set
            {
                Microsoft.ServiceBus.ServiceBusEnvironment.SystemConnectivity.Mode = value;
            }
        }

        public static bool UseAmqpWebSockets { get; set; }

        /// <summary>
        /// Gets or sets the encodingType of sent messages
        /// </summary>
        public static ServiceBusExplorer.Enums.EncodingType EncodingType
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

        public delegate void UpdateStatisticsDelegate(long messageNumber, long elapsedMilliseconds, DirectionType direction);

        #region Public Events
        public delegate void EventHandler(ServiceBusHelperEventArgs args);
        public event EventHandler OnDelete;
        public event EventHandler OnCreate;
        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new messaging factory object.
        /// </summary>
        /// <returns>A messaging factory object.</returns>
        public MessagingFactory CreateMessagingFactory()
        {
            MessagingFactory factory;
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                factory = MessagingFactory.CreateFromConnectionString(ConnectionStringWithoutEntityPath);
            }
            else
            {
                // The MessagingFactorySettings specifies the service bus messaging factory settings.
                var messagingFactorySettings = new MessagingFactorySettings
                {
                    OperationTimeout = TimeSpan.FromMinutes(5)
                };
                // In the first release of the service bus, the only available transport protocol is sb
                if (scheme == DefaultScheme)
                {
                    messagingFactorySettings.NetMessagingTransportSettings = new NetMessagingTransportSettings();
                }

                // As the name suggests, the MessagingFactory class is a Factory class that allows to create
                // instances of the QueueClient, TopicClient and SubscriptionClient classes.
                factory = MessagingFactory.Create(namespaceUri, messagingFactorySettings);
            }

            WriteToLogIf(traceEnabled, MessageFactorySuccessfullyCreated);
            return factory;
        }

        /// <summary>
        /// Connects the ServiceBusHelper object to service bus namespace contained in the ServiceBusNamespaces dictionary.
        /// </summary>
        /// <param name="serviceBusNamespace">The Service Bus namespace.</param>
        /// <returns>True if the operation succeeds, false otherwise.</returns>
        public bool Connect(ServiceBusNamespace serviceBusNamespace)
        {
            this.serviceBusNamespaceInstance = serviceBusNamespace;

            if (string.IsNullOrWhiteSpace(serviceBusNamespace?.ConnectionString))
            {
                throw new ArgumentException(ServiceBusConnectionStringCannotBeNull);
            }

            if (!TestNamespaceHostIsContactable(serviceBusNamespace))
            {
                throw new Exception($"Could not contact host in connection string: { serviceBusNamespace.ConnectionString }.");
            }

            var func = (() =>
            {
                connectionString = serviceBusNamespace.ConnectionString;
                currentSharedAccessKey = serviceBusNamespace.SharedAccessKey;
                currentSharedAccessKeyName = serviceBusNamespace.SharedAccessKeyName;

                // The NamespaceManager class can be used for managing entities,
                // such as queues, topics, subscriptions, and rules, in your service namespace.
                // You must provide service namespace address and access credentials in order
                // to manage your service namespace.
                namespaceManager = Microsoft.ServiceBus.NamespaceManager.CreateFromConnectionString(ConnectionStringWithoutEntityPath);

                // Set retry count
                if (namespaceManager.Settings.RetryPolicy is Microsoft.ServiceBus.RetryExponential defaultServiceBusRetryExponential)
                {
                    namespaceManager.Settings.RetryPolicy = new Microsoft.ServiceBus.RetryExponential(defaultServiceBusRetryExponential.MinimalBackoff,
                                                                                            defaultServiceBusRetryExponential.MaximumBackoff,
                                                                                            RetryHelper.RetryCount);
                }

                try
                {
                    notificationHubNamespaceManager = AzureNotificationHubs.NamespaceManager.CreateFromConnectionString(serviceBusNamespace.ConnectionStringWithoutTransportType);

                    // Set retry count
                    if (notificationHubNamespaceManager.Settings.RetryPolicy is AzureNotificationHubs.RetryExponential defaultNotificationHubsRetryExponential)
                    {
                        notificationHubNamespaceManager.Settings.RetryPolicy = new AzureNotificationHubs.RetryExponential(defaultNotificationHubsRetryExponential.MinimalBackoff,
                                                                                                                        defaultNotificationHubsRetryExponential.MaximumBackoff,
                                                                                                                        defaultNotificationHubsRetryExponential.DeltaBackoff,
                                                                                                                        defaultNotificationHubsRetryExponential.TerminationTimeBuffer,
                                                                                                                        RetryHelper.RetryCount);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                serviceBusQueue = CreateServiceBusEntity(static (sbn, nsmgr) => new ServiceBusQueue(sbn, nsmgr));
                serviceBusTopic = CreateServiceBusEntity(static (sbn, nsmgr) => new ServiceBusTopic(sbn, nsmgr));
                serviceBusSubscription = CreateServiceBusEntity(static (sbn, nsmgr) => new ServiceBusSubscription(sbn, nsmgr));
                serviceBusRule = CreateServiceBusEntity(static (sbn, nsmgr) => new ServiceBusRule(sbn, nsmgr));
                serviceBusRelay = CreateServiceBusEntity(static (sbn, nsmgr) => new ServiceBusRelay(sbn, nsmgr));
                serviceBusNotificationHub = CreateServiceBusEntity((sbn, nsmgr) => new ServiceBusNotificationHub(sbn, nsmgr, notificationHubNamespaceManager));
                serviceBusEventHub = CreateServiceBusEntity(static (sbn, nsmgr) => new ServiceBusEventHub(sbn, nsmgr));
                serviceBusConsumerGroup = CreateServiceBusEntity(static (sbn, nsmgr) => new ServiceBusConsumerGroup(sbn, nsmgr));
                serviceBusPartition = CreateServiceBusEntity(static (sbn, nsmgr) => new ServiceBusPartition(sbn, nsmgr));

                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, ServiceBusIsConnected, namespaceManager.Address.AbsoluteUri));
                namespaceUri = namespaceManager.Address;
                connectionStringType = serviceBusNamespace.ConnectionStringType;
                ns = IsCloudNamespace ? namespaceUri.Host.Split('.')[0] : namespaceUri.Segments[namespaceUri.Segments.Length - 1];

                // As the name suggests, the MessagingFactory class is a Factory class that allows to create
                // instances of the QueueClient, TopicClient and SubscriptionClient classes.
                MessagingFactory = MessagingFactory.CreateFromConnectionString(ConnectionStringWithoutEntityPath);
                WriteToLogIf(traceEnabled, MessageFactorySuccessfullyCreated);
                return true;
            });
            return RetryHelper.RetryFunc(func, writeToLog);
        }

        /// <summary>
        /// Retrieves the relay from the service namespace.
        /// </summary>
        /// <param name="path">Path of the relay relative to the service namespace base address.</param>
        /// <returns>A RelayDescription handle to the relay, or null if the relay does not exist in the service namespace. </returns>
        public RelayDescription GetRelay(string path)
        {
            return serviceBusRelay.GetRelay(path);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all relays in the service bus namespace.
        /// </summary>
        /// <returns>Returns an IEnumerable<RelayDescription/> collection of all relays in the service namespace.
        ///          Returns an empty collection if no relay exists in this service namespace.</returns>
        public IEnumerable<RelayDescription> GetRelays(int timeoutInSeconds)
        {
            return serviceBusRelay.GetRelays(timeoutInSeconds);
        }

        /// <summary>
        /// Creates a new relay in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A RelayDescription object describing the attributes with which the new event hub will be created.</param>
        /// <returns>Returns a newly-created RelayDescription object.</returns>
        public RelayDescription CreateRelay(RelayDescription description)
        {
            return serviceBusRelay.CreateRelay(description);
        }

        /// <summary>
        /// Deletes the relay described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="path">Path of the relay relative to the service namespace base address.</param>
        public async Task DeleteRelay(string path)
        {
            await serviceBusRelay.DeleteRelay(path);
        }

        /// <summary>
        /// Deletes all the relays in the list.
        /// <param name="relayServices">A list of relays to delete.</param>
        /// </summary>
        public async Task DeleteRelays(IEnumerable<string> relayServices)
        {
            await serviceBusRelay.DeleteRelays(relayServices);
        }

        /// <summary>
        /// Updates a relay in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A RelayDescription object describing the attributes with which the new relay will be updated.</param>
        /// <returns>Returns an updated RelayDescription object.</returns>
        public RelayDescription UpdateRelay(RelayDescription description)
        {
            return serviceBusRelay.UpdateRelay(description);
        }

        /// <summary>
        /// Gets the uri of a relay.
        /// </summary>
        /// <param name="description">The description of a relay.</param>
        /// <returns>The absolute uri of the relay.</returns>
        public Uri GetRelayUri(RelayDescription description)
        {
            return serviceBusRelay.GetRelayUri(description);
        }

        /// <summary>
        /// Retrieves the event hub from the service namespace.
        /// </summary>
        /// <param name="path">Path of the event hub relative to the service namespace base address.</param>
        /// <returns>A EventHubDescription handle to the event hub, or null if the event hub does not exist in the service namespace. </returns>
        public EventHubDescription GetEventHub(string path)
        {
            return serviceBusEventHub.GetEventHub(path);
        }

        /// <summary>
        /// Creates a new event hub in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A EventHubDescription object describing the attributes with which the new event hub will be created.</param>
        /// <returns>Returns a newly-created EventHubDescription object.</returns>
        public EventHubDescription CreateEventHub(EventHubDescription description)
        {
            return serviceBusEventHub.CreateEventHub(description);
        }

        /// <summary>
        /// Deletes the event hub described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="eventHubDescription">The event hub to delete.</param>
        public void DeleteEventHub(EventHubDescription eventHubDescription)
        {
            serviceBusEventHub.DeleteEventHub(eventHubDescription);
        }

        /// <summary>
        /// Deletes all the event hubs in the list.
        /// <param name="eventHubs">A list of event hubs to delete.</param>
        /// </summary>
        public void DeleteEventHubs(IEnumerable<string> eventHubs)
        {
            serviceBusEventHub.DeleteEventHubs(eventHubs);
        }

        /// <summary>
        /// Updates a event hub in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A EventHubDescription object describing the attributes with which the new event hub will be updated.</param>
        /// <returns>Returns an updated EventHubDescription object.</returns>
        public EventHubDescription UpdateEventHub(EventHubDescription description)
        {
            return serviceBusEventHub.UpdateEventHub(description);
        }

        /// <summary>
        /// Gets the uri of a event hub.
        /// </summary>
        /// <param name="eventHubPath">The path of a event hub.</param>
        /// <returns>The absolute uri of the event hub.</returns>
        public Uri GetEventHubUri(string eventHubPath)
        {
            return serviceBusEventHub.GetEventHubUri(eventHubPath);
        }

        /// <summary>
        /// Retrieves a partition.
        /// </summary>
        /// <param name="path">Path of the event hub relative to the service namespace base address.</param>
        /// <param name="consumerGroupName">The consumer group name.</param>
        /// <param name="name">Partition name.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of partitions attached to the event hub passed as a parameter.</returns>
        public PartitionDescription GetPartition(string path, string consumerGroupName, string name)
        {
            return serviceBusPartition.GetPartition(path, consumerGroupName, name);
        }

        /// <summary>
        /// Retrieves the collection of partitions of the event hub passed as a parameter.
        /// </summary>
        /// <param name="path">Path of the event hub relative to the service namespace base address.</param>
        /// <param name="consumerGroupName">The consumer group name.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of partitions attached to the event hub passed as a parameter.</returns>
        public IEnumerable<PartitionDescription> GetPartitions(string path, string consumerGroupName)
        {
            return serviceBusPartition.GetPartitions(path, consumerGroupName);
        }

        /// <summary>
        /// Gets the uri of a partition.
        /// </summary>
        /// <param name="eventHubName">Name of the event hub.</param>
        /// <param name="consumerGroupName">Name of the partition.</param>
        /// <param name="partitionId">The partition id.</param>
        /// <returns>The absolute uri of the partition.</returns>
        public Uri GetPartitionUri(string eventHubName, string consumerGroupName, string partitionId)
        {
            return serviceBusPartition.GetPartitionUri(eventHubName, consumerGroupName, partitionId);
        }

        /// <summary>
        /// Retrieves the collection of consumer groups of the event hub passed as a parameter.
        /// </summary>
        /// <param name="eventHubPath">The path of a event hub.</param>
        /// <param name="name">The name of a consumer group.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of consumer groups attached to the event hub passed as a parameter.</returns>
        public ConsumerGroupDescription GetConsumerGroup(string eventHubPath, string name)
        {
            return serviceBusConsumerGroup.GetConsumerGroup(eventHubPath, name);
        }

        /// <summary>
        /// Retrieves the collection of consumer groups of the event hub passed as a parameter.
        /// </summary>
        /// <param name="path">Path of the event hub relative to the service namespace base address.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of consumer groups attached to the event hub passed as a parameter.</returns>
        public IEnumerable<ConsumerGroupDescription> GetConsumerGroups(string path)
        {
            return serviceBusConsumerGroup.GetConsumerGroups(path);
        }

        /// <summary>
        /// Creates a new consumer group in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A ConsumerGroupDescription object describing the attributes with which the new consumer group will be created.</param>
        /// <returns>Returns a newly-created ConsumerGroupDescription object.</returns>
        public ConsumerGroupDescription CreateConsumerGroup(ConsumerGroupDescription description)
        {
            return serviceBusConsumerGroup.CreateConsumerGroup(description);
        }

        /// <summary>
        /// Deletes the consumer group described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="consumerGroupDescription">The consumer group to delete.</param>
        public void DeleteConsumerGroup(ConsumerGroupDescription consumerGroupDescription)
        {
            serviceBusConsumerGroup.DeleteConsumerGroup(consumerGroupDescription);
        }

        /// <summary>
        /// Deletes all the consumer groups in the list.
        /// <param name="eventHubName">Name of the event hub.</param>
        /// <param name="consumerGroups">A list of consumer groups to delete.</param>
        /// </summary>
        public void DeleteConsumerGroups(string eventHubName, IEnumerable<string> consumerGroups)
        {
            serviceBusConsumerGroup.DeleteConsumerGroups(eventHubName, consumerGroups);
        }

        /// <summary>
        /// Updates a consumer group in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A ConsumerGroupDescription object describing the attributes with which the new consumer group will be updated.</param>
        /// <returns>Returns an updated ConsumerGroupDescription object.</returns>
        public ConsumerGroupDescription UpdateConsumerGroup(ConsumerGroupDescription description)
        {
            return serviceBusConsumerGroup.UpdateConsumerGroup(description);
        }

        /// <summary>
        /// Gets the uri of a consumer group.
        /// </summary>
        /// <param name="eventHubName">Name of the event hub.</param>
        /// <param name="consumerGroupPath">The name of a consumer group.</param>
        /// <returns>The absolute uri of the consumer group.</returns>
        public Uri GetConsumerGroupUri(string eventHubName, string consumerGroupPath)
        {
            return serviceBusConsumerGroup.GetConsumerGroupUri(eventHubName, consumerGroupPath);
        }

        /// <summary>
        /// Retrieves the notification hub from the service namespace.
        /// </summary>
        /// <param name="path">Path of the notification hub relative to the service namespace base address.</param>
        /// <returns>A NotificationHubDescription handle to the notification hub, or null if the notification hub does not exist in the service namespace. </returns>
        public AzureNotificationHubs.NotificationHubDescription GetNotificationHub(string path)
        {
            return serviceBusNotificationHub.GetNotificationHub(path);
        }

        /// <summary>
        /// Creates a new notification hub in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A NotificationHubDescription object describing the attributes with which the new notification hub will be created.</param>
        /// <returns>Returns a newly-created NotificationHubDescription object.</returns>
        public AzureNotificationHubs.NotificationHubDescription CreateNotificationHub(AzureNotificationHubs.NotificationHubDescription description)
        {
            return serviceBusNotificationHub.CreateNotificationHub(description);
        }

        /// <summary>
        /// Deletes the notification hub described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="notificationHubDescription">The notification hub to delete.</param>
        public async Task DeleteNotificationHub(AzureNotificationHubs.NotificationHubDescription notificationHubDescription)
        {
            await serviceBusNotificationHub.DeleteNotificationHub(notificationHubDescription);
        }

        /// <summary>
        /// Deletes all the notification hubs in the list.
        /// <param name="notificationHubs">A list of notification hubs to delete.</param>
        /// </summary>
        public async Task DeleteNotificationHubs(IEnumerable<string> notificationHubs)
        {
            await serviceBusNotificationHub.DeleteNotificationHubs(notificationHubs);
        }

        /// <summary>
        /// Updates a notification hub in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A NotificationHubDescription object describing the attributes with which the new notification hub will be updated.</param>
        /// <returns>Returns an updated NotificationHubDescription object.</returns>
        public AzureNotificationHubs.NotificationHubDescription UpdateNotificationHub(AzureNotificationHubs.NotificationHubDescription description)
        {
            return serviceBusNotificationHub.UpdateNotificationHub(description);
        }

        /// <summary>
        /// Gets the uri of a notification hub.
        /// </summary>
        /// <param name="notificationHubPath">The name of a notification hub.</param>
        /// <returns>The absolute uri of the notification hub.</returns>
        public Uri GetNotificationHubUri(string notificationHubPath)
        {
            return serviceBusNotificationHub.GetNotificationHubUri(notificationHubPath);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all queues in the service bus namespace.
        /// </summary>
        /// <param name="filter">OData filter.</param>
        /// <returns>Returns an IEnumerable<QueueDescription/> collection of all queues in the service namespace.
        ///          Returns an empty collection if no queue exists in this service namespace.</returns>
        public IEnumerable<QueueDescription> GetQueues(string filter, int timeoutInSeconds)
        {
            return serviceBusQueue.GetQueues(filter, timeoutInSeconds);
        }

        /// <summary>
        /// Retrieves the queue from the service namespace.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        /// <returns>A QueueDescription handle to the queue, or null if the queue does not exist in the service namespace. </returns>
        public QueueDescription GetQueue(string path)
        {
            return serviceBusQueue.GetQueue(path);
        }

        /// <summary>
        /// Retrieves the topic from the service namespace.
        /// </summary>
        /// <param name="path">Path of the topic relative to the service namespace base address.</param>
        /// <returns>A TopicDescription handle to the topic, or null if the topic does not exist in the service namespace. </returns>
        public TopicDescription GetTopic(string path)
        {
            return serviceBusTopic.GetTopic(path);
        }

        /// <summary>
        /// Retrieves an enumerable collection of all topics in the service bus namespace.
        /// </summary>
        /// <param name="filter">OData filter.</param>
        /// <returns>Returns an IEnumerable<TopicDescription/> collection of all topics in the service namespace.
        ///          Returns an empty collection if no topic exists in this service namespace.</returns>
        public IEnumerable<TopicDescription> GetTopics(string filter, int timeoutInSeconds)
        {
            return serviceBusTopic.GetTopics(filter, timeoutInSeconds);
        }

        /// <summary>
        /// Gets a subscription attached to the topic passed a parameter.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <param name="name">The name of the subscription to get.</param>
        /// <returns>Returns the subscription with the specified name.</returns>
        public SubscriptionDescription GetSubscription(string topicPath, string name)
        {
            return serviceBusSubscription.GetSubscription(topicPath, name);
        }

        /// <summary>
        /// Retrieves an enumerated collection of subscriptions attached to the topic whose name is passed as a parameter.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of subscriptions attached to the topic passed as a parameter.</returns>
        public IEnumerable<SubscriptionDescription> GetSubscriptions(string topicPath)
        {
            return serviceBusSubscription.GetSubscriptions(topicPath);
        }

        /// <summary>
        /// Retrieves an enumerated collection of subscriptions attached to the topic passed as a parameter.
        /// </summary>
        /// <param name="topic">A topic belonging to the current service namespace base.</param>
        /// <param name="filter">OData filter.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of subscriptions attached to the topic passed as a parameter.</returns>
        public IEnumerable<SubscriptionDescription> GetSubscriptions(TopicDescription topic, string filter)
        {
            return serviceBusSubscription.GetSubscriptions(topic, filter);
        }

        /// <summary>
        /// Retrieves an enumerated collection of rules attached to the subscription passed as a parameter.
        /// </summary>
        /// <param name="subscription">A subscription belonging to the current service namespace base.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of rules attached to the subscription passed as a parameter.</returns>
        public IEnumerable<RuleDescription> GetRules(SubscriptionDescription subscription)
        {
            return serviceBusRule.GetRules(subscription);
        }

        /// <summary>
        /// Retrieves an enumerated collection of rules attached to the subscription passed as a parameter.
        /// </summary>
        /// <param name="topicPath">The name of a topic belonging to the current service namespace base.</param>
        /// <param name="name">The name of a subscription belonging to the topic passed as a parameter.</param>
        /// <returns>Returns an IEnumerable<SubscriptionDescription/> collection of rules attached to the subscription passed as a parameter.</returns>
        public IEnumerable<RuleDescription> GetRules(string topicPath, string name)
        {
            return serviceBusRule.GetRules(topicPath, name);
        }

        /// <summary>
        /// Gets the uri of a queue.
        /// </summary>
        /// <param name="queuePath">The name of a queue.</param>
        /// <returns>The absolute uri of the queue.</returns>
        public Uri GetQueueUri(string queuePath)
        {
           return serviceBusQueue.GetQueueUri(queuePath);
        }

        /// <summary>
        /// Gets the uri of the deadletter queue for a given queue.
        /// </summary>
        /// <param name="queuePath">The name of a queue.</param>
        /// <returns>the absolute uri of the deadletter queue.</returns>
        public Uri GetQueueDeadLetterQueueUri(string queuePath)
        {
            return serviceBusQueue.GetQueueDeadLetterQueueUri(queuePath);
        }

        /// <summary>
        /// Gets the uri of a topic.
        /// </summary>
        /// <param name="topicPath">The name of a topic.</param>
        /// <returns>The absolute uri of the topic.</returns>
        public Uri GetTopicUri(string topicPath)
        {
            return serviceBusTopic.GetTopicUri(topicPath);
        }

        /// <summary>
        /// Gets the uri of a subscription.
        /// </summary>
        /// <param name="topicPath">The name of the topic.</param>
        /// <param name="name">The name of a subscription.</param>
        /// <returns>The absolute uri of the subscription.</returns>
        public Uri GetSubscriptionUri(string topicPath, string name)
        {
            return serviceBusSubscription.GetSubscriptionUri(topicPath, name);
        }

        /// <summary>
        /// Gets the uri of the deadletter queue for a given subscription.
        /// </summary>
        /// <param name="topicPath">The name of a topic.</param>
        /// <param name="name">The name of a subscription.</param>
        /// <returns>The absolute uri of the deadletter queue.</returns>
        public Uri GetSubscriptionDeadLetterQueueUri(string topicPath, string name)
        {
            return serviceBusSubscription.GetSubscriptionDeadLetterQueueUri(topicPath, name);
        }

        /// <summary>
        /// Creates a new queue in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A QueueDescription object describing the attributes with which the new queue will be created.</param>
        /// <returns>Returns a newly-created QueueDescription object.</returns>
        public QueueDescription CreateQueue(QueueDescription description)
        {
            return serviceBusQueue.CreateQueue(description);
        }

        /// <summary>
        /// Updates a queue in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A QueueDescription object describing the attributes with which the new queue will be updated.</param>
        /// <returns>Returns an updated QueueDescription object.</returns>
        public QueueDescription UpdateQueue(QueueDescription description)
        {
            return serviceBusQueue.UpdateQueue(description);
        }

        /// <summary>
        /// Deletes all the queues in the list.
        /// <param name="queues">A list of queues to delete.</param>
        /// </summary>
        public async Task DeleteQueues(IEnumerable<string> queues)
        {
            await serviceBusQueue.DeleteQueues(queues);
        }

        /// <summary>
        /// Deletes the queue passed as a argument.
        /// </summary>
        /// <param name="queueDescription">The queue to delete.</param>
        public async Task DeleteQueue(QueueDescription queueDescription)
        {
            await serviceBusQueue.DeleteQueue(queueDescription);
        }

        /// <summary>
        /// Renames a queue inside a namespace.
        /// </summary>
        /// <param name="path">The path to an existing queue.</param>
        /// <param name="newPath">The new path to the renamed queue.</param>
        /// <returns>Returns a QueueDescription with the new name.</returns>
        public QueueDescription RenameQueue(string path, string newPath)
        {
            return serviceBusQueue.RenameQueue(path, newPath);
        }

        /// <summary>
        /// Creates a new topic in the service namespace with the given name.
        /// </summary>
        /// <param name="topicDescription">A TopicDescription object describing the attributes with which the new topic will be created.</param>
        /// <returns>Returns a newly-created TopicDescription object.</returns>
        public TopicDescription CreateTopic(TopicDescription topicDescription)
        {
            return serviceBusTopic.CreateTopic(topicDescription);
        }

        /// <summary>
        /// Updates a topic in the service namespace with the given name.
        /// </summary>
        /// <param name="topicDescription">A TopicDescription object describing the attributes with which the new topic will be updated.</param>
        /// <returns>Returns an updated TopicDescription object.</returns>
        public TopicDescription UpdateTopic(TopicDescription topicDescription)
        {
            return serviceBusTopic.UpdateTopic(topicDescription);
        }

        /// <summary>
        /// Deletes all the topics in the list.
        /// <param name="topics">A list of topics to delete.</param>
        /// </summary>
        public async Task DeleteTopics(IEnumerable<string> topics)
        {
            await serviceBusTopic.DeleteTopics(topics);
        }

        /// <summary>
        /// Deletes the topic passed as a argument.
        /// </summary>
        /// <param name="topic">The topic to delete.</param>
        public async Task DeleteTopic(TopicDescription topic)
        {
            await serviceBusTopic.DeleteTopic(topic);
        }

        /// <summary>
        /// Renames a topic inside a namespace.
        /// </summary>
        /// <param name="path">The path to an existing topic.</param>
        /// <param name="newPath">The new path to the renamed topic.</param>
        /// <returns>Returns a TopicDescription with the new name.</returns>
        public TopicDescription RenameTopic(string path, string newPath)
        {
            return serviceBusTopic.RenameTopic(path, newPath);
        }

        /// <summary>
        /// Adds a subscription to this topic, with a default pass-through filter added.
        /// </summary>
        /// <param name="topicDescription">A topic belonging to the current service namespace base.</param>
        /// <param name="subscriptionDescription">Metadata of the subscription to be created.</param>
        /// <returns>Returns a newly-created SubscriptionDescription object.</returns>
        public SubscriptionDescription CreateSubscription(TopicDescription topicDescription, SubscriptionDescription subscriptionDescription)
        {
            return serviceBusSubscription.CreateSubscription(topicDescription, subscriptionDescription);
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
            return serviceBusSubscription.CreateSubscription(topicDescription, subscriptionDescription, ruleDescription);
        }

        /// <summary>
        /// Updates a subscription to this topic.
        /// </summary>
        /// <param name="topicDescription">A topic belonging to the current service namespace base.</param>
        /// <param name="subscriptionDescription">Metadata of the subscription to be updated.</param>
        /// <returns>Returns an updated SubscriptionDescription object.</returns>
        public SubscriptionDescription UpdateSubscription(TopicDescription topicDescription, SubscriptionDescription subscriptionDescription)
        {
            return serviceBusSubscription.UpdateSubscription(topicDescription, subscriptionDescription);
        }

        /// <summary>
        /// Removes the subscriptions contained in the list passed as a argument.
        /// </summary>
        /// <param name="subscriptionDescriptions">The list containing subscriptions to remove.</param>
        public Task DeleteSubscriptions(IEnumerable<SubscriptionDescription> subscriptionDescriptions)
        {
            return serviceBusSubscription.DeleteSubscriptions(subscriptionDescriptions);
        }

        /// <summary>
        /// Removes the subscription passed as a argument.
        /// </summary>
        /// <param name="subscriptionDescription">The subscription to remove.</param>
        public async Task DeleteSubscription(SubscriptionDescription subscriptionDescription)
        {
            await serviceBusSubscription.DeleteSubscription(subscriptionDescription);
        }

        /// <summary>
        /// Adds a rule to this subscription, with a default pass-through filter added.
        /// </summary>
        /// <param name="subscriptionDescription">The subscription to add the rule to.</param>
        /// <param name="ruleDescription">Metadata of the rule to be created.</param>
        /// <returns>Returns a newly-created RuleDescription object.</returns>
        public RuleDescription AddRule(SubscriptionDescription subscriptionDescription, RuleDescription ruleDescription)
        {
            return serviceBusRule.AddRule(subscriptionDescription, ruleDescription);
        }

        /// <summary>
        /// Removes the rules contained in the list passed as a argument.
        /// </summary>
        /// <param name="wrappers">The list containing the ruleWrappers of the rules to remove.</param>
        public void RemoveRules(IEnumerable<RuleWrapper> wrappers)
        {
            serviceBusRule.RemoveRules(wrappers);
        }

        /// <summary>
        /// Removes the rule passed as a argument.
        /// </summary>
        /// <param name="subscriptionDescription">A subscription belonging to the current service namespace base.</param>
        /// <param name="rule">The rule to remove.</param>
        public void RemoveRule(SubscriptionDescription subscriptionDescription, RuleDescription rule)
        {
            serviceBusRule.RemoveRule(subscriptionDescription, rule);
        }

        /// <summary>
        /// Create a BrokeredMessage object
        /// </summary>
        /// <param name="template">A BrokeredMessageTemplate object.</param>
        /// <returns>The newly created BrokeredMessage object.</returns>
        public BrokeredMessage CreateBrokeredMessageTemplate(BrokeredMessageTemplate template)
        {
            return CreateBrokeredMessageTemplate(template.Message,
                                                 template.Label,
                                                 template.ContentType,
                                                 template.MessageId,
                                                 template.SessionId,
                                                 template.CorrelationId,
                                                 template.PartitionKey,
                                                 template.To,
                                                 template.ReplyTo,
                                                 template.ReplyToSessionId,
                                                 template.TimeToLive,
                                                 template.ScheduledEnqueueTimeUtc,
                                                 template.ForcePersistence,
                                                 template.Properties);
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
        /// <param name="partitionKey">The value of the PartitionKey property of the message.</param>
        /// <param name="to">The send to address.</param>
        /// <param name="replyTo">The value of the ReplyTo property of the message.</param>
        /// <param name="replyToSessionId">The value of the ReplyToSessionId property of the message.</param>
        /// <param name="timeToLive">The value of the TimeToLive property of the message.</param>
        /// <param name="scheduledEnqueueTimeUtc">The receiveTimeout in seconds after which the message will be enqueued.</param>
        /// <param name="forcePersistence">The value of the ForcePersistence property of the message.</param>
        /// <param name="properties">The user-defined properties of the message.</param>
        /// <returns>The newly created BrokeredMessage object.</returns>
        public BrokeredMessage CreateBrokeredMessageTemplate(string text,
                                                             string label,
                                                             string contentType,
                                                             string messageId,
                                                             string sessionId,
                                                             string correlationId,
                                                             string partitionKey,
                                                             string to,
                                                             string replyTo,
                                                             string replyToSessionId,
                                                             string timeToLive,
                                                             string scheduledEnqueueTimeUtc,
                                                             bool forcePersistence,
                                                             IEnumerable<MessagePropertyInfo> properties)
        {
            return CreateBrokeredMessageTemplate(text.ToMemoryStream(GetEncoding()),
                                                label,
                                                contentType,
                                                messageId,
                                                sessionId,
                                                correlationId,
                                                partitionKey,
                                                to,
                                                replyTo,
                                                replyToSessionId,
                                                timeToLive,
                                                scheduledEnqueueTimeUtc,
                                                properties);
        }

        /// <summary>
        /// Create a BrokeredMessage object
        /// </summary>
        /// <param name="stream">The message stream.</param>
        /// <param name="label">The value of the LabelId property of the message.</param>
        /// <param name="contentType">The type of the content.</param>
        /// <param name="messageId">The value of the MessageId property of the message.</param>
        /// <param name="sessionId">The value of the SessionId property of the message.</param>
        /// <param name="correlationId">The value of the CorrelationId property of the message.</param>
        /// <param name="partitionKey">The value of the PartitionKey property of the message.</param>
        /// <param name="to">The send to address.</param>
        /// <param name="replyTo">The value of the ReplyTo property of the message.</param>
        /// <param name="replyToSessionId">The value of the ReplyToSessionId property of the message.</param>
        /// <param name="timeToLive">The value of the TimeToLive property of the message.</param>
        /// <param name="scheduledEnqueueTimeUtc">The receiveTimeout in seconds after which the message will be enqueued.</param>
        /// <param name="properties">The user-defined properties of the message.</param>
        /// <returns>The newly created BrokeredMessage object.</returns>
        public BrokeredMessage CreateBrokeredMessageTemplate(Stream stream,
                                                             string label,
                                                             string contentType,
                                                             string messageId,
                                                             string sessionId,
                                                             string correlationId,
                                                             string partitionKey,
                                                             string to,
                                                             string replyTo,
                                                             string replyToSessionId,
                                                             string timeToLive,
                                                             string scheduledEnqueueTimeUtc,
                                                             IEnumerable<MessagePropertyInfo> properties)
        {
            var warningCollection = new ConcurrentBag<string>();
            var outboundMessage = new BrokeredMessage(stream, true);
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
            if (!string.IsNullOrWhiteSpace(partitionKey))
            {
                outboundMessage.PartitionKey = !string.IsNullOrWhiteSpace(sessionId) ? sessionId : partitionKey;
            }
            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                outboundMessage.ReplyTo = replyTo;
            }
            if (!string.IsNullOrWhiteSpace(replyToSessionId))
            {
                outboundMessage.ReplyToSessionId = replyToSessionId;
            }
            if (!string.IsNullOrWhiteSpace(timeToLive) && int.TryParse(timeToLive, out var ttl))
            {
                outboundMessage.TimeToLive = TimeSpan.FromSeconds(ttl);
            }
            if (!string.IsNullOrWhiteSpace(scheduledEnqueueTimeUtc) && int.TryParse(scheduledEnqueueTimeUtc, out var ss))
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
        /// Create an EventData object
        /// </summary>
        /// <param name="template">An EventDataTemplate.</param>
        /// <returns>The newly created EventData object.</returns>
        public EventData CreateEventDataTemplate(EventDataTemplate template)

        {
            return CreateEventDataTemplate(template.Message, template.PartitionKey, template.Properties);
        }

        /// <summary>
        /// Create an EventData object
        /// </summary>
        /// <param name="text">The event data text.</param>
        /// <param name="partitionKey">The value of the PartitionKey property of the event data.</param>
        /// <param name="properties">The user-defined properties of the event data.</param>
        /// <returns>The newly created EventData object used as a template.</returns>
        public EventData CreateEventDataTemplate(string text,
                                                 string partitionKey,
                                                 IEnumerable<MessagePropertyInfo> properties)
        {
            return CreateEventDataTemplate(text.ToMemoryStream(GetEncoding()), partitionKey, properties);
        }

        /// <summary>
        /// Create an EventData object
        /// </summary>
        /// <param name="stream">The event data stream.</param>
        /// <param name="partitionKey">The value of the PartitionKey property of the event data.</param>
        /// <param name="properties">The user-defined properties of the event data.</param>
        /// <returns>The newly created EventData object used as a template.</returns>
        public EventData CreateEventDataTemplate(Stream stream,
                                                 string partitionKey,
                                                 IEnumerable<MessagePropertyInfo> properties)
        {
            var warningCollection = new ConcurrentBag<string>();
            var outboundEventData = new EventData(stream)
            {
                PartitionKey = !string.IsNullOrWhiteSpace(partitionKey) ? partitionKey : Guid.NewGuid().ToString()
            };
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
                        if (outboundEventData.Properties.ContainsKey(e.Key))
                        {
                            outboundEventData.Properties[e.Key] = ConversionHelper.MapStringTypeToCLRType(e.Type, e.Value);
                        }
                        else
                        {
                            outboundEventData.Properties.Add(e.Key, ConversionHelper.MapStringTypeToCLRType(e.Type, e.Value));
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
            return outboundEventData;
        }

        /// <summary>
        /// This method can be used to send multiple messages to a queue or a topic.
        /// </summary>
        /// <param name="eventHubClient">An EventHubClient object used to send messages.</param>
        /// <param name="eventDataTemplateEnumerable">A collection of message templates to use to clone messages from.</param>
        /// <param name="getMessageNumber">This function returns the message number.</param>
        /// <param name="messageCount">The total number of messages to send.</param>
        /// <param name="taskId">The sender task id.</param>
        /// <param name="updatePartitionKey">Indicates whether to use a unique template key for each message.</param>
        /// <param name="noPartitionKey">Indiactes to specify a null value for the PartitionKey property. Messages will be written in a round robin fashion to event hub partitions.</param>
        /// <param name="addMessageNumber">Indicates whether to add a message number property.</param>
        /// <param name="logging">Indicates whether to enable logging of message content and properties.</param>
        /// <param name="verbose">Indicates whether to enable verbose logging.</param>
        /// <param name="statistics">Indicates whether to enable sender statistics.</param>
        /// <param name="eventDataInspector">Event Data inspector.</param>
        /// <param name="updateStatistics">When statistics = true, this delegate is invoked to update statistics.</param>
        /// <param name="sendBatch">Indicates whether to use SendBatch.</param>
        /// <param name="senderThinkTime">Indicates whether to use think time.</param>
        /// <param name="thinkTime">Indicates the value of the sender think time.</param>
        /// <param name="batchSize">Indicates the batch size.</param>
        /// <param name="cancellationTokenSource">The cancellation token.</param>
        /// <param name="partitionId">PartitionId (optional: used only when sending messages to a specific partition.</param>
        /// <returns>Trace message.</returns>
        public async Task<string> SendEventData(EventHubClient eventHubClient,
                                                IEnumerable<EventData> eventDataTemplateEnumerable,
                                                Func<long> getMessageNumber,
                                                long messageCount,
                                                int taskId,
                                                bool updatePartitionKey,
                                                bool noPartitionKey,
                                                bool addMessageNumber,
                                                bool logging,
                                                bool verbose,
                                                bool statistics,
                                                bool sendBatch,
                                                int batchSize,
                                                bool senderThinkTime,
                                                int thinkTime,
                                                IEventDataInspector eventDataInspector,
                                                UpdateStatisticsDelegate updateStatistics,
                                                CancellationTokenSource cancellationTokenSource,
                                                string partitionId = null)
        {
            if (eventHubClient == null)
            {
                throw new ArgumentNullException(EventHubClientCannotBeNull);
            }

            if (eventDataTemplateEnumerable == null)
            {
                throw new ArgumentNullException(EventDataTemplateEnumerableCannotBeNull);
            }

            if (cancellationTokenSource == null)
            {
                throw new ArgumentNullException(CancellationTokenSourceCannotBeNull);
            }

            var eventDataCircularList = new CircularList<EventData>(eventDataTemplateEnumerable);

            long messagesSent = 0;
            long totalElapsedTime = 0;
            var minimumSendTime = long.MaxValue;
            long maximumSendTime = 0;
            string exceptionMessage = null;
            try
            {
                long messageNumber;
                string partitionKey = null;
                EventHubSender eventHubSender = null;
                var partitionIdIsNull = string.IsNullOrWhiteSpace(partitionId);
                if (!partitionIdIsNull)
                {
                    eventHubSender = await eventHubClient.CreatePartitionedSenderAsync(partitionId);
                }
                if (sendBatch && batchSize > 1)
                {
                    var more = true;
                    while (!cancellationTokenSource.Token.IsCancellationRequested && more)
                    {
                        var eventDataList = new List<EventData>();
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
                            // ReSharper disable once ImplicitlyCapturedClosure
                            await RetryHelper.RetryActionAsync(async () =>
                            {
                                for (var i = 0; i < messageNumberList.Count; i++)
                                {
                                    eventDataList.Add(eventDataInspector != null ?
                                                      eventDataInspector.BeforeSendMessage(eventDataCircularList.Next.Clone()) :
                                                      eventDataCircularList.Next.Clone());
                                    if ((i % batchSize) == 0)
                                    {
                                        partitionKey = Guid.NewGuid().ToString();
                                    }
                                    if (addMessageNumber)
                                    {
                                        eventDataList[i].Properties[MessageNumber] = messageNumberList[i];
                                    }
                                    if (updatePartitionKey)
                                    {
                                        eventDataList[i].PartitionKey = partitionKey;
                                    }
                                    if (noPartitionKey || !partitionIdIsNull)
                                    {
                                        eventDataList[i].PartitionKey = null;
                                    }
                                }
                                if (messageNumberList.Count > 0)
                                {
                                    if (partitionIdIsNull)
                                    {
                                        elapsedMilliseconds = await SendEventDataBatch(eventHubClient,
                                                                                       eventDataList,
                                                                                       messageNumberList,
                                                                                       taskId,
                                                                                       logging,
                                                                                       verbose);
                                    }
                                    else
                                    {
                                        elapsedMilliseconds = await SendEventDataBatch(eventHubSender,
                                                                                       eventDataList,
                                                                                       messageNumberList,
                                                                                       taskId,
                                                                                       logging,
                                                                                       verbose);
                                    }
                                }
                            },
                            writeToLog);
                            messagesSent += eventDataList.Count;
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
                                updateStatistics(eventDataList.Count, elapsedMilliseconds, DirectionType.Send);
                            }
                        }
                        if (senderThinkTime)
                        {
                            WriteToLog(string.Format(SleepingFor, thinkTime));

                            await Task.Delay(thinkTime);
                        }
                    }
                }
                else
                {
                    while ((messageNumber = getMessageNumber()) < messageCount &&
                       !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        long elapsedMilliseconds = 0;
                        var number = messageNumber;
                        await RetryHelper.RetryActionAsync(async () =>
                        {
                            var eventData = eventDataInspector != null ?
                                            eventDataInspector.BeforeSendMessage(eventDataCircularList.Next.Clone()) :
                                            eventDataCircularList.Next.Clone();
                            if (addMessageNumber)
                            {
                                // ReSharper disable AccessToModifiedClosure
                                eventData.Properties[MessageNumber] = number;
                                // ReSharper restore AccessToModifiedClosure
                            }
                            if (updatePartitionKey)
                            {
                                eventData.PartitionKey = Guid.NewGuid().ToString();
                            }
                            if (noPartitionKey || !partitionIdIsNull)
                            {
                                eventData.PartitionKey = null;
                            }
                            if (partitionIdIsNull)
                            {
                                elapsedMilliseconds = await SendEventData(eventHubClient,
                                                                          eventData,
                                                                          number,
                                                                          taskId,
                                                                          logging,
                                                                          verbose);
                            }
                            else
                            {
                                elapsedMilliseconds = await SendEventData(eventHubSender,
                                                                          eventData,
                                                                          number,
                                                                          taskId,
                                                                          logging,
                                                                          verbose);
                            }
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

                            await Task.Delay(thinkTime);
                        }
                    }
                }
            }
            catch (ServerBusyException ex)
            {
                eventHubClient.Abort();
                exceptionMessage = ex.Message;
            }
            catch (MessageLockLostException ex)
            {
                eventHubClient.Abort();
                exceptionMessage = ex.Message;
            }
            catch (CommunicationObjectAbortedException ex)
            {
                eventHubClient.Abort();
                exceptionMessage = ex.Message;
            }
            catch (CommunicationObjectFaultedException ex)
            {
                eventHubClient.Abort();
                exceptionMessage = ex.Message;
            }
            catch (CommunicationException ex)
            {
                eventHubClient.Abort();
                exceptionMessage = ex.Message;
            }
            catch (TimeoutException ex)
            {
                eventHubClient.Abort();
                exceptionMessage = ex.Message;
            }
            catch (Exception ex)
            {
                eventHubClient.Abort();
                exceptionMessage = ex.Message;
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
                throw new Exception(builder.ToString());
            }
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             SenderStatisticsLine1,
                                             messagesSent,
                                             messagesPerSecond,
                                             totalElapsedTime));
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             SenderStatisticsLine2,
                                             averageSendTime,
                                             minimumSendTime == long.MaxValue ? 0 : minimumSendTime,
                                             maximumSendTime));
            return builder.ToString();
        }

        ///// <summary>
        ///// This method can be used to send an event data to an event hub.
        ///// </summary>
        ///// <param name="eventHubClient">A EventHubClient object used to send messages.</param>
        ///// <param name="eventDataList">The list of messages to send.</param>
        ///// <param name="messageNumberList">The list of message numbers.</param>
        ///// <param name="taskId">The sender task id.</param>
        ///// <param name="logging">Indicates whether logging of message content and properties is enabled.</param>
        ///// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        ///// <returns>Elapsed milliseconds.</returns>
        private async Task<long> SendEventDataBatch(EventHubClient eventHubClient,
                                                   List<EventData> eventDataList,
                                                   List<long> messageNumberList,
                                                   int taskId,
                                                   bool logging,
                                                   bool verbose)
        {
            long elapsedMilliseconds = 0;

            if (eventHubClient == null)
            {
                throw new ArgumentNullException(EventHubClientCannotBeNull);
            }

            if (eventDataList == null || eventDataList.Count == 0)
            {
                return elapsedMilliseconds;
            }
            List<Stream> eventDataPayloadList = null;
            var builder = new StringBuilder();
            var stopwatch = Stopwatch.StartNew();
            try
            {
                if (logging && verbose)
                {
                    eventDataPayloadList = eventDataList.Select(e => e.Clone().GetBodyStream()).ToList();
                }
                await eventHubClient.SendBatchAsync(eventDataList);
            }
            finally
            {
                stopwatch.Stop();
            }
            elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            if (logging)
            {
                for (var i = 0; i < eventDataList.Count; i++)
                {
                    try
                    {
                        builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                         EventDataSuccessfullySent,
                                                         taskId,
                                                         messageNumberList[i],
                                                         string.IsNullOrWhiteSpace(eventDataList[i].PartitionKey)
                                                                ? NullValue
                                                                : eventDataList[i].PartitionKey));
                        if (verbose)
                        {
                            builder.AppendLine(SentMessagePayloadHeader);
                            builder.AppendLine(string.Format(MessageTextFormat, GetMessageText(eventDataPayloadList[i])));
                            builder.AppendLine(SentMessagePropertiesHeader);
                            foreach (var p in eventDataList[i].Properties)
                            {
                                builder.AppendLine(string.Format(MessagePropertyFormat,
                                                                 p.Key,
                                                                 p.Value));
                            }
                        }
                    }
                    finally
                    {
                        eventDataList[i].Dispose();
                    }
                }
                var traceMessage = builder.ToString();
                WriteToLog(traceMessage.Substring(0, traceMessage.Length - 1));
            }
            return elapsedMilliseconds;
        }

        ///// <summary>
        ///// This method can be used to send an event data to an event hub.
        ///// </summary>
        ///// <param name="eventHubClient">A EventHubSender object used to send messages.</param>
        ///// <param name="eventDataList">The list of messages to send.</param>
        ///// <param name="messageNumberList">The list of message numbers.</param>
        ///// <param name="taskId">The sender task id.</param>
        ///// <param name="logging">Indicates whether logging of message content and properties is enabled.</param>
        ///// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        ///// <returns>Elapsed milliseconds.</returns>
        private async Task<long> SendEventDataBatch(EventHubSender eventHubSender,
                                                   List<EventData> eventDataList,
                                                   List<long> messageNumberList,
                                                   int taskId,
                                                   bool logging,
                                                   bool verbose)
        {
            long elapsedMilliseconds = 0;

            if (eventHubSender == null)
            {
                throw new ArgumentNullException(EventHubSenderCannotBeNull);
            }

            if (eventDataList == null || eventDataList.Count == 0)
            {
                return elapsedMilliseconds;
            }
            List<Stream> eventDataPayloadList = null;
            var builder = new StringBuilder();
            var stopwatch = Stopwatch.StartNew();
            try
            {
                if (logging && verbose)
                {
                    eventDataPayloadList = eventDataList.Select(e => e.Clone().GetBodyStream()).ToList();
                }
                await eventHubSender.SendBatchAsync(eventDataList);
            }
            finally
            {
                stopwatch.Stop();
            }
            elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            if (logging)
            {
                for (var i = 0; i < eventDataList.Count; i++)
                {
                    try
                    {
                        builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                         EventDataSuccessfullySent,
                                                         taskId,
                                                         messageNumberList[i],
                                                         string.IsNullOrWhiteSpace(eventDataList[i].PartitionKey)
                                                                ? NullValue
                                                                : eventDataList[i].PartitionKey));
                        if (verbose)
                        {
                            builder.AppendLine(SentMessagePayloadHeader);
                            builder.AppendLine(string.Format(MessageTextFormat, GetMessageText(eventDataPayloadList[i])));
                            builder.AppendLine(SentMessagePropertiesHeader);
                            foreach (var p in eventDataList[i].Properties)
                            {
                                builder.AppendLine(string.Format(MessagePropertyFormat,
                                                                 p.Key,
                                                                 p.Value));
                            }
                        }
                    }
                    finally
                    {
                        eventDataList[i].Dispose();
                    }
                }
                var traceMessage = builder.ToString();
                WriteToLog(traceMessage.Substring(0, traceMessage.Length - 1));
            }
            return elapsedMilliseconds;
        }

        /// <summary>
        /// This method can be used to send an event data to an event hub.
        /// </summary>
        /// <param name="eventHubClient">An EventHubClient object used to send event data.</param>
        /// <param name="eventData">The event data to send.</param>
        /// <param name="messageNumber">The message number.</param>
        /// <param name="taskId">The sender task id.</param>
        /// <param name="logging">Indicates whether logging of event data content and properties is enabled.</param>
        /// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        /// <returns>Elapsed milliseconds.</returns>
        private async Task<long> SendEventData(EventHubClient eventHubClient,
                                              EventData eventData,
                                              long messageNumber,
                                              int taskId,
                                              bool logging,
                                              bool verbose)
        {
            long elapsedMilliseconds;
            if (eventHubClient == null)
            {
                throw new ArgumentNullException(EventHubClientCannotBeNull);
            }

            if (eventData == null)
            {
                throw new ArgumentNullException(EventDataCannotBeNull);
            }

            var stopwatch = new Stopwatch();
            Stream bodyStream = null;
            try
            {
                var builder = new StringBuilder();
                try
                {
                    if (logging && verbose)
                    {
                        bodyStream = eventData.Clone().GetBodyStream();
                    }
                    stopwatch.Start();
                    await eventHubClient.SendAsync(eventData);
                }
                finally
                {
                    stopwatch.Stop();
                }
                elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                if (logging)
                {
                    builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                     EventDataSuccessfullySent,
                                                     taskId,
                                                     messageNumber,
                                                     string.IsNullOrWhiteSpace(eventData.PartitionKey) ? NullValue : eventData.PartitionKey));
                    if (verbose)
                    {
                        builder.AppendLine(SentMessagePayloadHeader);
                        builder.AppendLine(string.Format(MessageTextFormat, GetMessageText(bodyStream)));
                        if (eventData.Properties.Any())
                        {
                            builder.AppendLine(SentMessagePropertiesHeader);
                            foreach (var p in eventData.Properties)
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
                eventData.Dispose();
            }
            return elapsedMilliseconds;
        }

        /// <summary>
        /// This method can be used to send an event data to an event hub.
        /// </summary>
        /// <param name="eventHubSender">A EventHubSender object used to send event data.</param>
        /// <param name="eventData">The event data to send.</param>
        /// <param name="messageNumber">The message number.</param>
        /// <param name="taskId">The sender task id.</param>
        /// <param name="logging">Indicates whether logging of event data content and properties is enabled.</param>
        /// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        /// <returns>Elapsed milliseconds.</returns>
        private async Task<long> SendEventData(EventHubSender eventHubSender,
                                              EventData eventData,
                                              long messageNumber,
                                              int taskId,
                                              bool logging,
                                              bool verbose)
        {
            long elapsedMilliseconds;
            if (eventHubSender == null)
            {
                throw new ArgumentNullException(EventHubSenderCannotBeNull);
            }

            if (eventData == null)
            {
                throw new ArgumentNullException(EventDataCannotBeNull);
            }

            var stopwatch = new Stopwatch();
            Stream bodyStream = null;
            try
            {
                var builder = new StringBuilder();
                try
                {
                    if (logging && verbose)
                    {
                        bodyStream = eventData.Clone().GetBodyStream();
                    }
                    stopwatch.Start();
                    await eventHubSender.SendAsync(eventData);
                }
                finally
                {
                    stopwatch.Stop();
                }
                elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                if (logging)
                {
                    builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                     EventDataSuccessfullySent,
                                                     taskId,
                                                     messageNumber,
                                                     string.IsNullOrWhiteSpace(eventData.PartitionKey) ? NullValue : eventData.PartitionKey));
                    if (verbose)
                    {
                        builder.AppendLine(SentMessagePayloadHeader);
                        builder.AppendLine(string.Format(MessageTextFormat, GetMessageText(bodyStream)));
                        if (eventData.Properties.Any())
                        {
                            builder.AppendLine(SentMessagePropertiesHeader);
                            foreach (var p in eventData.Properties)
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
                eventData.Dispose();
            }
            return elapsedMilliseconds;
        }

        /// <summary>
        /// Create a BrokeredMessage object
        /// </summary>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="taskId">The task Id.</param>
        /// <param name="updateMessageId">Indicates whether to use a unique id for each message.</param>
        /// <param name="oneSessionPerTask">Indicates whether to use a different session for each sender task.</param>
        /// <param name="isBinary">Indicates if the body is binary or not.</param>
        /// <param name="bodyType">Contains the body type.</param>
        /// <param name="messageInspector">A BrokeredMessage inspector object.</param>
        /// <returns>The cloned BrokeredMessage object.</returns>
        public BrokeredMessage CreateMessageForApiReceiver(BrokeredMessage messageTemplate,
                                                           int taskId,
                                                           bool updateMessageId,
                                                           bool oneSessionPerTask,
                                                           BodyType bodyType,
                                                           IBrokeredMessageInspector messageInspector)
        {
            if (messageTemplate == null)
            {
                throw new ArgumentNullException(BrokeredMessageCannotBeNull);
            }
            var outboundMessage = messageTemplate.Clone();
            if (bodyType == BodyType.String)
            {
                using (var reader = new StreamReader(outboundMessage.GetBody<Stream>()))
                {
                    outboundMessage = new BrokeredMessage(reader.ReadToEnd());
                }
            }

            outboundMessage.MessageId = updateMessageId ? Guid.NewGuid().ToString() : messageTemplate.MessageId;
            outboundMessage.SessionId = oneSessionPerTask ? taskId.ToString(CultureInfo.InvariantCulture) : messageTemplate.SessionId;

            if (bodyType == BodyType.String || bodyType == BodyType.ByteArray || bodyType == BodyType.Stream)
            {
                if (!string.IsNullOrWhiteSpace(messageTemplate.Label))
                {
                    outboundMessage.Label = messageTemplate.Label;
                }
                if (!string.IsNullOrWhiteSpace(messageTemplate.ContentType))
                {
                    outboundMessage.ContentType = messageTemplate.ContentType;
                }
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
                foreach (var property in messageTemplate.Properties)
                {
                    outboundMessage.Properties[property.Key] = property.Value;
                }
                outboundMessage.TimeToLive = messageTemplate.TimeToLive;
                outboundMessage.ScheduledEnqueueTimeUtc = messageTemplate.ScheduledEnqueueTimeUtc;
                outboundMessage.ForcePersistence = messageTemplate.ForcePersistence;
            }
            if (messageInspector != null)
            {
                outboundMessage = messageInspector.BeforeSendMessage(outboundMessage);
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
        /// <param name="to">The uri of the target topic or queue.</param>
        /// <returns>The cloned BrokeredMessage object.</returns>
        public BrokeredMessage CreateMessageForWcfReceiver(BrokeredMessage messageTemplate,
                                                           int taskId,
                                                           bool updateMessageId,
                                                           bool oneSessionPerTask,
                                                           Uri to)
        {
            string messageText;
            using (var reader = new StreamReader(messageTemplate.Clone().GetBody<Stream>()))
            {
                messageText = reader.ReadToEnd();
            }
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
                            message.Headers.To = to;
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
                    message.Headers.To = to;
                    encoder.WriteMessage(message, outputStream);
                    outputStream.Seek(0, SeekOrigin.Begin);
                }
            }

            if (message != null && outputStream.Length > 0)
            {
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
                outboundMessage.ForcePersistence = messageTemplate.ForcePersistence;
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
        /// <param name="taskId">The sender task id.</param>
        /// <param name="updateMessageId">Indicates whether to use a unique id for each message.</param>
        /// <param name="addMessageNumber">Indicates whether to add a message number property.</param>
        /// <param name="oneSessionPerTask">Indicates whether to use a different session for each sender task.</param>
        /// <param name="logging">Indicates whether to enable logging of message content and properties.</param>
        /// <param name="verbose">Indicates whether to enable verbose logging.</param>
        /// <param name="statistics">Indicates whether to enable sender statistics.</param>
        /// <param name="messageInspector">A BrokeredMessage inspector object.</param>
        /// <param name="updateStatistics">When statistics = true, this delegate is invoked to update statistics.</param>
        /// <param name="sendBatch">Indicates whether to use SendBatch.</param>
        /// <param name="isBinary">Indicates if the body is binary or not.</param>
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
                                 int taskId,
                                 bool updateMessageId,
                                 bool addMessageNumber,
                                 bool oneSessionPerTask,
                                 bool logging,
                                 bool verbose,
                                 bool statistics,
                                 bool sendBatch,
                                 bool isBinary,
                                 int batchSize,
                                 bool senderThinkTime,
                                 int thinkTime,
                                 BodyType bodyType,
                                 IBrokeredMessageInspector messageInspector,
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

            if (cancellationTokenSource == null)
            {
                throw new ArgumentNullException(CancellationTokenSourceCannotBeNull);
            }

            var messageTemplateCircularList = new CircularList<BrokeredMessage>(messageTemplateEnumerable);

            long messagesSent = 0;
            long totalElapsedTime = 0;
            var minimumSendTime = long.MaxValue;
            long maximumSendTime = 0;
            var ok = true;
            string exceptionMessage = null;
            var wcfUri = IsCloudNamespace ?
                         new Uri(namespaceUri, messageSender.Path) :
                         new UriBuilder
                         {
                             Host = namespaceUri.Host,
                             Path = $"{namespaceUri.AbsolutePath}/{messageSender.Path}",
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
                                    messageList.Add(useWcf ?
                                                    CreateMessageForWcfReceiver(
                                                        messageTemplateCircularList.Next,
                                                        taskId,
                                                        updateMessageId,
                                                        oneSessionPerTask,
                                                        wcfUri) :
                                                    CreateMessageForApiReceiver(
                                                        messageTemplateCircularList.Next,
                                                        taskId,
                                                        updateMessageId,
                                                        oneSessionPerTask,
                                                        bodyType,
                                                        messageInspector));
                                    if (addMessageNumber)
                                    {
                                        messageList[i].Properties[MessageNumber] = messageNumberList[i];
                                    }
                                }
                                if (messageNumberList.Count > 0)
                                {
                                    SendBatch(messageSender,
                                              messageList,
                                              taskId,
                                              isBinary,
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
                            var useWcf = bodyType == BodyType.Wcf;
                            var outboundMessage = useWcf
                                                      ? CreateMessageForWcfReceiver(
                                                          messageTemplateCircularList.Next,
                                                          taskId,
                                                          updateMessageId,
                                                          oneSessionPerTask,
                                                          wcfUri)
                                                      : CreateMessageForApiReceiver(
                                                          messageTemplateCircularList.Next,
                                                          taskId,
                                                          updateMessageId,
                                                          oneSessionPerTask,
                                                          bodyType,
                                                          messageInspector);
                            if (addMessageNumber)
                            {
                                // ReSharper disable AccessToModifiedClosure
                                outboundMessage.Properties[MessageNumber] = messageNumber;
                                // ReSharper restore AccessToModifiedClosure
                            }


                            SendMessage(messageSender,
                                        outboundMessage,
                                        taskId,
                                        isBinary,
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
                                             SenderStatisticsLine1,
                                             messagesSent,
                                             messagesPerSecond,
                                             totalElapsedTime));
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             SenderStatisticsLine2,
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
        /// <param name="taskId">The sender task id.</param>
        /// <param name="isBinary">Indicates if the body is binary or not.</param>
        /// <param name="useWcf">Indicates whether to send messages to a WCF receiver.</param>
        /// <param name="logging">Indicates whether logging of message content and properties is enabled.</param>
        /// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        /// <param name="elapsedMilliseconds">The time spent to send the message.</param>
        private void SendBatch(MessageSender messageSender,
                              List<BrokeredMessage> messageList,
                              int taskId,
                              bool isBinary,
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
            var bodyStreams = new List<Stream>();
            if (logging && verbose)
            {
                bodyStreams.AddRange(messageList.Select(message => message.Clone().GetBodyStream()));
            }
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

            if (!logging)
            {
                return;
            }

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
                        var messageText = GetMessageText(bodyStreams[i], isBinary);
                        if (useWcf)
                        {
                            var stringBuilder = new StringBuilder();
                            using (var reader = XmlReader.Create(new StringReader(messageText)))
                            {
                                // The XmlWriter is used just to indent the XML message
                                var settings = new XmlWriterSettings { Indent = true };
                                using (var writer = XmlWriter.Create(stringBuilder, settings))
                                {
                                    writer.WriteNode(reader, true);
                                }
                            }
                            messageText = stringBuilder.ToString();
                        }
                        builder.AppendLine(string.Format(MessageTextFormat, messageText.Contains('\n') ? messageText :
                            messageText.Substring(0, Math.Min(messageText.Length, 128)) +
                            (messageText.Length >= 128 ? "..." : "")));
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
        
        /// <summary>
        /// This method can be used to send a message to a queue or a topic.
        /// </summary>
        /// <param name="messageSender">A MessageSender object used to send messages.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="taskId">The sender task id.</param>
        /// <param name="isBinary">Indicates if the body is binary or not.</param>
        /// <param name="useWcf">Indicates whether to send messages to a WCF receiver.</param>
        /// <param name="logging">Indicates whether logging of message content and properties is enabled.</param>
        /// <param name="verbose">Indicates whether verbose logging is enabled.</param>
        /// <param name="elapsedMilliseconds">The time spent to send the message.</param>
        private void SendMessage(MessageSender messageSender,
                                BrokeredMessage message,
                                int taskId,
                                bool isBinary,
                                bool useWcf,
                                bool logging,
                                bool verbose,
                                out long elapsedMilliseconds)
        {
            if (messageSender == null)
            {
                throw new ArgumentNullException(MessageSenderCannotBeNull);
            }

            if (message == null)
            {
                throw new ArgumentNullException(BrokeredMessageCannotBeNull);
            }

            var stopwatch = new Stopwatch();

            try
            {
                Stream bodyStream = null;
                if (logging && verbose)
                {
                    bodyStream = message.Clone().GetBodyStream();
                }
                var builder = new StringBuilder();
                try
                {
                    stopwatch.Start();
                    messageSender.Send(message);
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
                                                     string.IsNullOrWhiteSpace(message.MessageId) ? NullValue : message.MessageId,
                                                     string.IsNullOrWhiteSpace(message.SessionId) ? NullValue : message.SessionId,
                                                     string.IsNullOrWhiteSpace(message.Label) ? NullValue : message.Label,
                                                     message.Size));
                    if (verbose)
                    {
                        builder.AppendLine(SentMessagePayloadHeader);
                        var messageText = GetMessageText(bodyStream, isBinary);
                        if (useWcf)
                        {
                            messageText = XmlHelper.Indent(messageText);
                        }
                        builder.AppendLine(string.Format(MessageTextFormat, messageText.Contains('\n') ? messageText : messageText.Substring(0, Math.Min(messageText.Length, 128)) + (messageText.Length >= 128 ? "..." : "")));
                        if (message.Properties.Any())
                        {
                            builder.AppendLine(SentMessagePropertiesHeader);
                            foreach (var p in message.Properties)
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
                message.Dispose();
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
        /// <param name="messageInspector">A BrokeredMessage inspector object.</param>
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
                                    IBrokeredMessageInspector messageInspector,
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
            var minimumReceiveTime = long.MaxValue;
            long maximumReceiveTime = 0;
            var minimumCompleteTime = long.MaxValue;
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
                            var stopwatch = Stopwatch.StartNew();
                            var messageEnumerable = messageReceiver.ReceiveBatch(batchSize, TimeSpan.FromSeconds(timeout));
                            stopwatch.Stop();

                            messageList = messageEnumerable as IList<BrokeredMessage> ?? messageEnumerable.ToList();
                            if (messageInspector != null)
                            {
                                messageList = messageList.Select(b => messageInspector.AfterReceiveMessage(b)).ToList();
                            }
                            isCompleted = messageEnumerable == null || !messageList.Any();
                            if (isCompleted)
                            {
                                continue;
                            }
                            if (messageReceiver.Mode == ReceiveMode.PeekLock)
                            {
                                if (completeReceive)
                                {
                                    stopwatch = Stopwatch.StartNew();
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
                                    var stopwatch = Stopwatch.StartNew();
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
                            var movedToDeadLetterQueue = false;
                            var deferredMessage = false;
                            var readDeferredMessage = false;
                            var stopwatch = new Stopwatch();

                            if (!readingDeferredMessages)
                            {
                                stopwatch.Start();
                                inboundMessage = messageReceiver.Receive(TimeSpan.FromSeconds(timeout));
                                stopwatch.Stop();
                                if (inboundMessage != null && messageInspector != null)
                                {
                                    inboundMessage = messageInspector.AfterReceiveMessage(inboundMessage);
                                }
                                isCompleted = inboundMessage == null &&
                                              messageDeferProvider.Count == 0;
                            }
                            else
                            {
                                isCompleted = messageDeferProvider.Count == 0;
                                if (!isCompleted)
                                {
                                    if (messageDeferProvider.Dequeue(out var sequenceNumber))
                                    {
                                        stopwatch.Start();
                                        inboundMessage = messageReceiver.Receive(sequenceNumber);
                                        stopwatch.Stop();
                                        if (inboundMessage != null && messageInspector != null)
                                        {
                                            inboundMessage = messageInspector.AfterReceiveMessage(inboundMessage);
                                        }
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
                            inboundMessage?.Dispose();
                        }
                        if (!receiverThinkTime)
                        {
                            continue;
                        }
                        WriteToLog(string.Format(SleepingFor, thinkTime));
                        Thread.Sleep(thinkTime);
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
                                                 ReceiverStatisticsLine1,
                                                 messagesReceived,
                                                 messagesPerSecond,
                                                 totalReceiveElapsedTime));
            }
            else
            {
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                 ReceiverStatisticsWithCompleteLine1,
                                                 messagesReceived,
                                                 messagesPerSecond,
                                                 totalReceiveElapsedTime,
                                                 totalCompleteElapsedTime));
            }
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             ReceiverStatisticsLine2,
                                             averageReceiveTime,
                                             minimumReceiveTime == long.MaxValue ? 0 : minimumReceiveTime,
                                             maximumReceiveTime));
            if (messageReceiver.Mode == ReceiveMode.PeekLock)
            {
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                             ReceiverStatisticsLine3,
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
        public async Task<string> ExportEntities(List<IExtensibleDataObject> entityDescriptionList)
        {
            var importExportHelper = new ImportExportHelper(writeToLog);
            return await importExportHelper.ReadAndSerialize(this, entityDescriptionList);
        }

        /// <summary>
        /// Imports entities from a xml string.
        /// </summary>
        /// <param name="xml">The xml containing entities.</param>
        /// <returns>The description of the newly created queue.</returns>
        public void ImportEntities(string xml)
        {
            var importExportHelper = new ImportExportHelper(writeToLog);
            importExportHelper.DeserializeAndCreate(this, xml);
        }

        /// <summary>
        /// Reads the content of the BrokeredMessage passed as argument.
        /// </summary>
        /// <param name="messageToRead">The BrokeredMessage to read.</param>
        /// <param name="bodyType">BodyType</param>
        /// <returns>The content of the BrokeredMessage.</returns>
        public string GetMessageText(BrokeredMessage messageToRead, bool useAscii, out BodyType bodyType)
        {
            string messageText = null;
            Stream stream = null;
            bodyType = BodyType.Stream;
            if (messageToRead == null)
            {
                return null;
            }
            var inboundMessage = messageToRead.Clone();
            bool gzipDecompress = false;
            try
            {
                stream = inboundMessage.GetBody<Stream>();
                if (messageToRead.Properties.ContainsKey("Content-Encoding"))
                {
                    var encoding = inboundMessage.Properties["Content-Encoding"].ToString();
                    gzipDecompress = encoding == "gzip";
                }
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
                    if (gzipDecompress)
                    {
                        var mso = new MemoryStream();
                        using (var gs = new GZipStream(stream, CompressionMode.Decompress))
                        {
                            gs.CopyTo(mso);
                        }
                        stream = mso;
                    }
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
                    if (gzipDecompress)
                    {
                        var mso = new MemoryStream();
                        using (var gs = new GZipStream(stream, CompressionMode.Decompress))
                        {
                            gs.CopyTo(mso);
                        }
                        stream = mso;
                    }
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
                        messageText = AttemptToReadByteArrayBody(messageToRead.Clone(), gzipDecompress);
                        bodyType = BodyType.ByteArray;
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
                                            if (!useAscii && messageText.ToCharArray().GroupBy(c => c).
                                                Where(g => char.IsControl(g.Key) && g.Key != '\t' && g.Key != '\n' && g.Key != '\r').
                                                Select(g => g.First()).Any())
                                            {
                                                stream.Seek(0, SeekOrigin.Begin);
                                                using (var binaryReader = new BinaryReader(stream))
                                                {
                                                    var bytes = binaryReader.ReadBytes((int)stream.Length);
                                                    messageText = BitConverter.ToString(bytes).Replace('-', ' ');
                                                }
                                            }
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
            }
            return messageText;
        }

        private string DecompressAsString(byte[] bytes)
        {
            string result;
            using (var msi = new MemoryStream(bytes))
            {
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        gs.CopyTo(mso);
                    }
                    result = Encoding.UTF8.GetString(mso.ToArray());
                }
            }

            return result;
        }

        private string AttemptToReadByteArrayBody(BrokeredMessage brokeredMessage, bool compress)
        {
            var body = brokeredMessage.GetBody<byte[]>();
            if (compress)
               return DecompressAsString(body);
            return Encoding.UTF8.GetString(body);
        }

        /// <summary>
        /// Reads the message contained in a stream.
        /// </summary>
        /// <param name="stream">The stream containing the message.</param>
        /// <param name="isBinary">Indicates if the body is binary or not.</param>
        /// <returns>The message.</returns>
        private static string GetMessageText(Stream stream, bool isBinary = false)
        {
            string messageText;
            if (stream == null)
            {
                return null;
            }
            try
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
            }
            catch (Exception)
            {
                try
                {
                    stream.Seek(0, SeekOrigin.Begin);
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
                }
                catch (Exception)
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
                            if (isBinary)
                            {
                                using (var binaryReader = new BinaryReader(stream))
                                {
                                    var bytes = binaryReader.ReadBytes((int)stream.Length);
                                    messageText = BitConverter.ToString(bytes).Replace('-', ' ');
                                }
                            }
                            else
                            {
                                using (var reader = new StreamReader(stream))
                                {
                                    messageText = reader.ReadToEnd();
                                    if (messageText.ToCharArray().GroupBy(c => c).
                                        Where(g => char.IsControl(g.Key) && g.Key != '\t' && g.Key != '\n' && g.Key != '\r').
                                        Select(g => g.First()).Any())
                                    {
                                        stream.Seek(0, SeekOrigin.Begin);
                                        using (var binaryReader = new BinaryReader(stream))
                                        {
                                            var bytes = binaryReader.ReadBytes((int)stream.Length);
                                            messageText = BitConverter.ToString(bytes).Replace('-', ' ');
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            messageText = UnableToReadMessageBody;
                        }
                    }
                }
            }
            return messageText;
        }

        /// <summary>
        /// Reads the content of the EventData passed as argument.
        /// </summary>
        /// <param name="eventDataToRead">The EventData to read.</param>
        /// <param name="bodyType">BodyType</param>
        /// <param name="doNotSerializeBody"></param>
        /// <returns>The content of the EventData.</returns>
        public string GetMessageText(EventDataMessage eventDataToRead, out BodyType bodyType, bool doNotSerializeBody = false)
        {
            string eventDataText = null;
            bodyType = BodyType.Stream;
            if (eventDataToRead == null)
            {
                return null;
            }

            using var stream = eventDataToRead.GetBodyStream();

            var bBodyParsed = false;
            if (!doNotSerializeBody)
            {
                try
                {
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
                        var eventData = encoder.ReadMessage(stream, MaxBufferSize);
                        using (var reader = eventData.GetReaderAtBodyContents())
                        {
                            // The XmlWriter is used just to indent the XML eventData
                            var settings = new XmlWriterSettings { Indent = true };
                            using (var writer = XmlWriter.Create(stringBuilder, settings))
                            {
                                writer.WriteNode(reader, true);
                            }
                        }
                        eventDataText = stringBuilder.ToString();
                        bodyType = BodyType.Wcf;
                    }
                    bBodyParsed = true;
                }
                catch (Exception)
                {
                    try
                    {
                        if (stream != null)
                        {
                            stream.Seek(0, SeekOrigin.Begin);

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
                            var eventData = encoder.ReadMessage(stream, MaxBufferSize);
                            using (var reader = eventData.GetReaderAtBodyContents())
                            {
                                eventDataText = reader.ReadString();
                            }
                            bodyType = BodyType.Wcf;
                        }
                        bBodyParsed = true;
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
                                    eventDataText = serializer.ReadObject(stream) as string;
                                    bodyType = BodyType.String;
                                    bBodyParsed = true;
                                }
                                catch (Exception)
                                {
                                    bBodyParsed = false;
                                }
                            }
                            else
                            {
                                bBodyParsed = false;
                            }
                        }
                        catch (Exception)
                        {
                            bBodyParsed = false;
                        }
                    }
                }
            }
            if (!bBodyParsed)
            {
                try
                {
                    if (stream != null)
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        using (var reader = new StreamReader(stream))
                        {
                            eventDataText = reader.ReadToEnd();
                            if (eventDataText.ToCharArray().GroupBy(c => c).
                                Where(g => char.IsControl(g.Key) && g.Key != '\t' && g.Key != '\n' && g.Key != '\r').
                                Select(g => g.First()).Any())
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                using (var binaryReader = new BinaryReader(stream))
                                {
                                    var bytes = binaryReader.ReadBytes((int)stream.Length);
                                    eventDataText = BitConverter.ToString(bytes).Replace('-', ' ');
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    eventDataText = UnableToReadMessageBody;
                }
            }
            return eventDataText;
        }

        public string GetAddressRelativeToNamespace(string address)
        {
            if (Uri.IsWellFormedUriString(address, UriKind.Absolute))
            {
                var uri = new UriBuilder(new Uri(address, UriKind.Absolute))
                {
                    Scheme = NamespaceUri.Scheme,
                    Port = NamespaceUri.Port
                }.Uri;
                if (NamespaceUri.IsBaseOf(uri))
                {
                    var uriRelativeToNamespace = NamespaceUri.MakeRelativeUri(uri);
                    return uriRelativeToNamespace.ToString();
                }
            }
            int i;
            return !string.IsNullOrWhiteSpace(address) &&
                   (i = address.LastIndexOf('/', Math.Max(0, address.Length - 2))) > 0 &&
                   i < address.Length - 1 ?
                address.Substring(i + 1) :
                address;
        }

        public ServiceBusHelper2 GetServiceBusHelper2()
        {
            var serviceBusHelper2 = new ServiceBusHelper2(writeToLog);
            serviceBusHelper2.ConnectionString = ConnectionString;
            serviceBusHelper2.TransportType = UseAmqpWebSockets
                ? Azure.Messaging.ServiceBus.ServiceBusTransportType.AmqpWebSockets
                : Azure.Messaging.ServiceBus.ServiceBusTransportType.AmqpTcp;
            return serviceBusHelper2;
        }

        public async Task<QueueProperties> GetQueueProperties(QueueDescription oldQueueDescription)
        {
            return (await this.GetQueueProperties(new List<QueueDescription>() { oldQueueDescription }).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task<List<QueueProperties>> GetQueueProperties(List<QueueDescription> oldQueueDescriptions)
        {
            var administrationClient = new ServiceBusAdministrationClient(connectionString);
            var result = new List<QueueProperties>();

            foreach (QueueDescription oldQueueDescription in oldQueueDescriptions)
            {
                result.Add(await administrationClient.GetQueueAsync(oldQueueDescription.Path).ConfigureAwait(false));
            }

            return result;
        }

        public async Task<SubscriptionProperties> GetSubscriptionProperties(SubscriptionWrapper oldSubscriptionWrapper)
        {
            return (await this.GetSubscriptionProperties(new List<SubscriptionWrapper>() { oldSubscriptionWrapper }).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task<List<SubscriptionProperties>> GetSubscriptionProperties(List<SubscriptionWrapper> oldSubscriptionWrappers)
        {
            var managementClient = new ServiceBusAdministrationClient(connectionString);
            var result = new List<SubscriptionProperties>();

            foreach (SubscriptionWrapper oldSubscriptionWrapper in oldSubscriptionWrappers)
            {
                var topicResponse = await managementClient.GetTopicAsync(
                    oldSubscriptionWrapper.TopicDescription.Path)
                    .ConfigureAwait(false);

                var subscriptionResponse = await managementClient.GetSubscriptionAsync(
                    topicResponse.Value.Name,
                    oldSubscriptionWrapper.SubscriptionDescription.Name)
                    .ConfigureAwait(false);

                result.Add(subscriptionResponse.Value);
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Receives a message from a queue or a subscription.
        /// </summary>
        /// <param name="messageCount">The message count.</param>
        /// <param name="messageTotal">The total number of messages read.</param>
        /// <param name="messageReceiver">The message receiver used to receive messages.</param>
        /// <param name="complete">Call Complete method to delete the message.</param>
        /// <param name="encoder">MessageEncoder used to decode a WCF message.</param>
        /// <param name="timeout">The receive receiveTimeout.</param>
        private async void ReceiveNextMessage(int? messageCount, int messageTotal, MessageReceiver messageReceiver, MessageEncoder encoder, bool complete, TimeSpan timeout)
        {
            var inboundMessage = await messageReceiver.ReceiveAsync(timeout);
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
                builder.AppendLine(string.Format(ReceiverStatisticsLineNoTask,
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
                                                complete ? Read : Peeked,
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
                ReceiveNextMessage(messageCount, messageTotal, messageReceiver, encoder, complete, timeout);
            }
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

            if (inboundMessage == null)
            {
                return;
            }
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
                        var settings = new XmlWriterSettings { Indent = true };
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

        /// <summary>
        /// Gets the eventData body and properties.
        /// </summary>
        /// <param name="builder">The string builder object used to accumulate the trace event data.</param>
        /// <param name="inboundMessage">The inbound event data.</param>
        public void GetMessageAndProperties(StringBuilder builder, EventData inboundMessage)
        {
            string eventDataText = null;
            Stream stream = null;

            if (inboundMessage == null)
            {
                return;
            }

            try
            {
                var eventDataClone = inboundMessage.Clone();
                stream = eventDataClone.GetBodyStream();
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        eventDataText = reader.ReadToEnd();
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
                            eventDataText = serializer.ReadObject(stream) as string;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                using (var reader = new StreamReader(stream))
                                {
                                    eventDataText = reader.ReadToEnd();
                                }
                            }
                            catch (Exception)
                            {
                                eventDataText = UnableToReadMessageBody;
                            }
                        }
                    }
                    else
                    {
                        eventDataText = UnableToReadMessageBody;
                    }
                }
                catch (Exception)
                {
                    eventDataText = UnableToReadMessageBody;
                }
            }
            builder.AppendLine();
            builder.AppendLine(ReceivedMessagePayloadHeader);
            builder.AppendLine(string.Format(MessageTextFormat, eventDataText));
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

        private T CreateServiceBusEntity<T>(Func<ServiceBusNamespace, NamespaceManager, T> initialization) where T : IServiceBusEntity
        {
            T entity = initialization(serviceBusNamespaceInstance, namespaceManager);
            entity.Scheme = scheme;
            entity.OnCreate += args => OnCreate?.Invoke(args);
            entity.OnDelete += args => OnDelete?.Invoke(args);
            entity.WriteToLog = (message, async) => WriteToLogIf(traceEnabled, message, async);
            return entity;
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

        private static bool TestNamespaceHostIsContactable(ServiceBusNamespace serviceBusNamespace)
        {
            if (!Uri.TryCreate(serviceBusNamespace.Uri, UriKind.Absolute, out var namespaceUri))
            {
                return false;
            }

            try
            {
                System.Net.Dns.GetHostEntry(namespaceUri.Host);
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
