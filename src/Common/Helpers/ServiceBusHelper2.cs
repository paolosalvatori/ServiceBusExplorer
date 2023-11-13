#region Copyright
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
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Azure.Identity;
using ServiceBusExplorer.Helpers;
#endregion

// ReSharper disable CheckNamespace
namespace ServiceBusExplorer.ServiceBus.Helpers
// ReSharper restore CheckNamespace
{

    using Utilities.Helpers;
    using System.IO.Compression;
    using System.Text;
    using Abstractions;
    using Enums;


    public enum BodyType
    {
        Stream,
        String,
        Wcf,
        ByteArray
    }

    public class ServiceBusHelper2
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
        private const string ServiceBusUriArgumentCannotBeNull = "The uri argument cannot be null.";
        private const string ServiceBusNamespaceArgumentCannotBeNull = "The nameSpace argument cannot be null.";
        private const string ServiceBusIssuerNameArgumentCannotBeNull = "The issuerName argument cannot be null.";
        private const string ServiceBusIssuerSecretArgumentCannotBeNull = "The issuerSecret argument cannot be null.";
        private const string serviceBusAdministrationClientCannotBeNull = "The namespace manager argument cannot be null.";
        private const string QueueDescriptionCannotBeNull = "The queue description argument cannot be null.";
        private const string TopicDescriptionCannotBeNull = "The topic decsription argument cannot be null.";
        private const string SubscriptionDescriptionCannotBeNull = "The subscription description argument cannot be null.";
        private const string RuleDescriptionCannotBeNull = "The rule description argument cannot be null.";
        private const string EventHubDescriptionCannotBeNull = "The event hub description argument cannot be null.";
        private const string ConsumerGroupCannotBeNull = "The consumerGroup argument cannot be null or empty.";
        private const string PartitionDescriptionCannotBeNull = "The partition description argument cannot be null.";
        private const string ConsumerGroupDescriptionCannotBeNull = "The consumer group description argument cannot be null.";
        private const string NotificationHubDescriptionCannotBeNull = "The notification hub description argument cannot be null.";
        private const string RuleCannotBeNull = "The rule argument cannot be null.";
        private const string PathCannotBeNull = "The path argument cannot be null or empty.";
        private const string NewPathCannotBeNull = "The new path argument cannot be null or empty.";
        private const string NameCannotBeNull = "The name argument cannot be null or empty.";
        private const string DescriptionCannotBeNull = "The description argument cannot be null.";
        private const string ServiceBusIsDisconnected = "The application is now disconnected from any service bus namespace.";
        private const string ServiceBusIsConnected = "The application is now connected to the {0} service bus namespace.";
        private const string QueueCreated = "The queue {0} has been successfully created.";
        private const string QueueDeleted = "The queue {0} has been successfully deleted.";
        private const string QueueRenamed = "The queue {0} has been successfully renamed to {1}.";
        private const string QueueUpdated = "The queue {0} has been successfully updated.";
        private const string TopicCreated = "The topic {0} has been successfully created.";
        private const string TopicDeleted = "The topic {0} has been successfully deleted.";
        private const string TopicRenamed = "The topic {0} has been successfully renamed to {1}.";
        private const string TopicUpdated = "The topic {0} has been successfully updated.";
        private const string SubscriptionCreated = "The {0} subscription for the {1} topic has been successfully created.";
        private const string SubscriptionDeleted = "The {0} subscription for the {1} topic has been successfully deleted.";
        private const string SubscriptionUpdated = "The {0} subscription for the {1} topic has been successfully updated.";
        private const string RuleCreated = "The {0} rule for the {1} subscription has been successfully created.";
        private const string RuleDeleted = "The {0} rule for the {1} subscription has been successfully deleted.";
        private const string RelayCreated = "The relay {0} has been successfully created.";
        private const string RelayDeleted = "The relay {0} has been successfully deleted.";
        private const string RelayUpdated = "The relay {0} has been successfully updated.";
        private const string EventHubCreated = "The event hub {0} has been successfully created.";
        private const string EventHubDeleted = "The event hub {0} has been successfully deleted.";
        private const string EventHubUpdated = "The event hub {0} has been successfully updated.";
        private const string ConsumerGroupCreated = "The consumer group {0} has been successfully created.";
        private const string ConsumerGroupDeleted = "The consumer group {0} has been successfully deleted.";
        private const string ConsumerGroupUpdated = "The consumer group {0} has been successfully updated.";
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
        private ServiceBusAdministrationClient serviceBusAdministrationClient;
        private ServiceBusClient serviceBusClient;
        //private AzureNotificationHubs.serviceBusAdministrationClient notificationHubserviceBusAdministrationClient;
        private bool traceEnabled;
        private string scheme = DefaultScheme;
        //private Microsoft.ServiceBus.TokenProvider tokenProvider;
        //private AzureNotificationHubs.TokenProvider notificationHubTokenProvider;
        private Uri namespaceUri;
        private ServiceBusNamespaceType connectionStringType;
        private Uri atomFeedUri;
        private string ns;
        private string servicePath;
        private string connectionString;
        //private List<BrokeredMessage> brokeredMessageList;
        private readonly WriteToLogDelegate writeToLog;
        private string currentSharedAccessKeyName;
        private string currentSharedAccessKey;
        private ServiceBusTransportType currentTransportType;
        private ServiceBusNamespace2 serviceBusNamespaceInstance;
        #endregion
        
        #region Private Static Fields
        private static Encoding encodingType = Encoding.ASCII;
        #endregion
        
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the ServiceBusHelper class.
        /// </summary>
        /// <param name="writeToLog">WriteToLog method.</param>
        public ServiceBusHelper2(WriteToLogDelegate writeToLog)
        {
            this.writeToLog = writeToLog;
            traceEnabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceBusHelper class.
        /// </summary>
        /// <param name="writeToLog">WriteToLog method.</param>
        /// <param name="traceEnabled">A boolean value indicating whether tracing is enabled.</param>
        public ServiceBusHelper2(WriteToLogDelegate writeToLog, bool traceEnabled)
        {
            this.writeToLog = writeToLog;
            this.traceEnabled = traceEnabled;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceBusHelper class.
        /// </summary>
        /// <param name="writeToLog">WriteToLog method.</param>
        /// <param name="serviceBusHelper">Base ServiceBusHelper.</param>
        public ServiceBusHelper2(WriteToLogDelegate writeToLog, ServiceBusHelper2 serviceBusHelper)
        {
            this.writeToLog = writeToLog;
            AtomFeedUri = serviceBusHelper.AtomFeedUri;
            MessageDeferProviderType = serviceBusHelper.MessageDeferProviderType;
            ConnectionString = serviceBusHelper.ConnectionString;
            //namespaceManager = serviceBusHelper.NamespaceManager;
            //notificationHubNamespaceManager = serviceBusHelper.NotificationHubNamespaceManager;
            //MessagingFactory = serviceBusHelper.MessagingFactory;
            Namespace = serviceBusHelper.Namespace;
            NamespaceUri = serviceBusHelper.NamespaceUri;
            MessageDeferProviderType = serviceBusHelper.MessageDeferProviderType;
            Scheme = serviceBusHelper.Scheme;
            ServiceBusNamespaces = serviceBusHelper.ServiceBusNamespaces;
            BrokeredMessageInspectors = serviceBusHelper.BrokeredMessageInspectors;
            EventDataInspectors = serviceBusHelper.EventDataInspectors;
            BrokeredMessageGenerators = serviceBusHelper.BrokeredMessageGenerators;
            EventDataGenerators = serviceBusHelper.EventDataGenerators;
            ServicePath = serviceBusHelper.ServicePath;
            //TokenProvider = serviceBusHelper.TokenProvider;
            //notificationHubTokenProvider = serviceBusHelper.notificationHubTokenProvider;
            TraceEnabled = serviceBusHelper.TraceEnabled;
            SharedAccessKey = serviceBusHelper.SharedAccessKey;
            SharedAccessKeyName = serviceBusHelper.SharedAccessKeyName;
            TransportType = serviceBusHelper.TransportType;
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
        /// Gets or sets the current service name.
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

        public string ConnectionStringWithoutEntityPath
        {
            get
            {
                var builder = ServiceBusConnectionStringProperties .Parse(connectionString);

                // Todo find a way to remove entity path
                //builder.EntityPath = string.Empty;

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
            set
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
        /// Gets or sets the transport type.
        /// </summary>
        public ServiceBusTransportType TransportType
        {
            get
            {
                lock (this)
                {
                    return currentTransportType;
                }
            }
            set
            {
                lock (this)
                {
                    currentTransportType = value;
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
        /// Gets or sets the dictionary containing serviceBus accounts.
        /// </summary>
        public Dictionary<string, ServiceBusNamespace2> ServiceBusNamespaces { get; set; }

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
        
        public bool ConnectionStringContainsEntityPath()
        {
            var connectionStringProperties = ServiceBusConnectionStringProperties.Parse(ConnectionString);
            
            if (connectionStringProperties?.EntityPath != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsPremiumNamespace()
        {
            var administrationClient = new ServiceBusAdministrationClient(ConnectionString);
            NamespaceProperties namespaceProperties = await administrationClient.GetNamespacePropertiesAsync().ConfigureAwait(false);

            return namespaceProperties.MessagingSku == MessagingSku.Premium;
        }

        public async Task<bool> IsQueue(string name)
        {
            var administrationClient = new ServiceBusAdministrationClient(ConnectionString);
            return await administrationClient.QueueExistsAsync(name).ConfigureAwait(false);
        }

        public async Task<bool> IsTopic(string name)
        {
            var administrationClient = new ServiceBusAdministrationClient(ConnectionString);
            return await administrationClient.TopicExistsAsync(name).ConfigureAwait(false);
        }
        
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
        public static Encoding EncodingType
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
#pragma warning disable CS0067 // Event is never used
        public event EventHandler OnDelete;
        public event EventHandler OnCreate;
#pragma warning restore CS0067 // Event is never used
        #endregion

        
        #region Public Methods

        
        /// <summary>
        /// Connects the ServiceBusHelper object to service bus namespace contained in the ServiceBusNamespaces dictionary.
        /// </summary>
        /// <param name="serviceBusNamespace">The Service Bus namespace.</param>
        /// <returns>True if the operation succeeds, false otherwise.</returns>
        public bool Connect(ServiceBusNamespace2 serviceBusNamespace)
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
                currentTransportType = serviceBusNamespace.TransportType;

                // The serviceBusAdministrationClient class can be used for managing entities,
                // such as queues, topics, subscriptions, and rules, in your service namespace.
                // You must provide service namespace address and access credentials in order
                // to manage your service namespace.
                if (!string.IsNullOrEmpty(serviceBusNamespace.SharedAccessKey) && !string.IsNullOrEmpty(serviceBusNamespace.SharedAccessKey)){
                    serviceBusAdministrationClient = new ServiceBusAdministrationClient(serviceBusNamespace.ConnectionString);
                    serviceBusClient = new ServiceBusClient(serviceBusNamespace.ConnectionString);
                } else
                {
                    serviceBusAdministrationClient = new ServiceBusAdministrationClient(serviceBusNamespace.Namespace + "servicebus.windows.net", new DefaultAzureCredential());
                    serviceBusClient = new ServiceBusClient(serviceBusNamespace.Namespace + "servicebus.windows.net", new DefaultAzureCredential());
                }

                //todo moet retry policy geset worden?


                try
                {
                    // todo notificationhub nodig hier?
                }
                catch (Exception)
                {
                    // ignored
                }
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, ServiceBusIsConnected, connectionString));
                //var namespaceProperties = serviceBusAdministrationClient.GetNamespacePropertiesAsync().Result;
                //namespaceUri = new Uri(namespaceProperties.Value.Name);
                connectionStringType = serviceBusNamespace.ConnectionStringType;
                //ns = IsCloudNamespace ? namespaceUri.Host.Split('.')[0] : namespaceUri.Segments[namespaceUri.Segments.Length - 1];
                //atomFeedUri = new Uri($"{Uri.UriSchemeHttp}://{namespaceUri.Host}");
                
                return true;
            });
            return RetryHelper.RetryFunc(func, writeToLog);
        }
        
        
        // TODO rest van public methods

        #region queuecontrol
        
        /// <summary>
        /// Retrieves an enumerable collection of all queues in the service bus namespace.
        /// </summary>
        /// <param name="filter">OData filter.</param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns>Returns an IEnumerable<QueueDescription/> collection of all queues in the service namespace.
        ///          Returns an empty collection if no queue exists in this service namespace.</returns>
        public async Task<IEnumerable<QueueRuntimeProperties>> GetQueuesAsync(string filter, int timeoutInSeconds)
        {
            if (serviceBusAdministrationClient != null)
            {
                if (string.IsNullOrEmpty(serviceBusNamespaceInstance.EntityPath))
                {
                    /*
                    var taskList = new List<Task>();
                    */
                    //Documentation states AND is the only logical clause allowed in the filter (And FYI a maximum of only 3 filter expressions allowed)
                    //https://docs.microsoft.com/en-us/dotnet/api/microsoft.servicebus.namespacemanager.getqueuesasync?view=azure-dotnet#Microsoft_ServiceBus_NamespaceManager_GetQueuesAsync_System_String_
                    //Split on ' OR ' and combine queues returned
                    
                    var queuesListingResult = /*string.IsNullOrWhiteSpace(filter) ?*/ serviceBusAdministrationClient.GetQueuesRuntimePropertiesAsync() /*: serviceBusAdministrationClient.GetQueuesAsync(/*splitFilter#1#)*/;
                    

                    // Todo client side filtering
                    IList<QueueRuntimeProperties> queues = new List<QueueRuntimeProperties>();

                    await foreach (var item in queuesListingResult)
                    {
                        queues.Add(item);
                    }

                    return queues;
                }

                return new List<QueueRuntimeProperties> {
                    GetQueueUsingEntityPath(timeoutInSeconds)
                };
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }
        
        /// <summary>
        /// Retrieves a queue in the service bus namespace that matches the entity path.
        /// </summary>
        private QueueRuntimeProperties GetQueueUsingEntityPath(int timeoutInSeconds)
        {
            var taskList = new List<Task>();
            var getQueueTask = serviceBusAdministrationClient.GetQueueRuntimePropertiesAsync(serviceBusNamespaceInstance.EntityPath);
            taskList.Add(getQueueTask);
            taskList.Add(Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds)));
            Task.WaitAny(taskList.ToArray());
            if (getQueueTask.IsCompleted)
            {
                try
                {
                    return getQueueTask.Result;
                }
                catch (AggregateException ex)
                {
                    throw ex.InnerExceptions.First();
                }
            }
            throw new TimeoutException();
        }
        
        /// <summary>
        /// Retrieves the queue from the service namespace.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        /// <returns>A QueueDescription handle to the queue, or null if the queue does not exist in the service namespace. </returns>
        public QueueProperties GetQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (serviceBusAdministrationClient != null)
            {
                return RetryHelper.RetryFunc(() => serviceBusAdministrationClient.GetQueueAsync(path).Result.Value, writeToLog);
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }
        
        /// <summary>.
        /// Creates a new queue in the service namespace with the given name.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        /// <returns>Returns a newly-created QueueDescription object.</returns>
        public QueueProperties CreateQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (serviceBusAdministrationClient != null)
            {
                var queue = RetryHelper.RetryFunc(() => serviceBusAdministrationClient.CreateQueueAsync(path).Result.Value, writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueCreated, path));
                OnCreate?.Invoke(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Creates a new queue in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A QueueDescription object describing the attributes with which the new queue will be created.</param>
        /// <returns>Returns a newly-created QueueDescription object.</returns>
        public QueueProperties CreateQueue(QueueProperties description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (serviceBusAdministrationClient != null)
            {
                var queue = RetryHelper.RetryFunc(() => serviceBusAdministrationClient.CreateQueueAsync(description.Name).Result.Value, writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueCreated, description.Name));
                OnCreate?.Invoke(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Updates a queue in the service namespace with the given name.
        /// </summary>
        /// <param name="description">A QueueDescription object describing the attributes with which the new queue will be updated.</param>
        /// <returns>Returns an updated QueueDescription object.</returns>
        public QueueProperties UpdateQueue(QueueProperties description)
        {
            if (description == null)
            {
                throw new ArgumentException(DescriptionCannotBeNull);
            }
            if (serviceBusAdministrationClient != null)
            {
                var queue = RetryHelper.RetryFunc(() => serviceBusAdministrationClient.UpdateQueueAsync(description).Result.Value, writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueUpdated, description.Name));
                OnCreate?.Invoke(new ServiceBusHelperEventArgs(queue, EntityType.Queue));
                return queue;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }

        /// <summary>
        /// Deletes all the queues in the list.
        /// <param name="queues">A list of queues to delete.</param>
        /// </summary>
        public async Task DeleteQueues(IEnumerable<string> queues)
        {
            if (queues == null)
            {
                return;
            }

            await Task.WhenAll(queues.Select(DeleteQueue));
        }

        /// <summary>
        /// Deletes the queue described by the relative name of the service namespace base address.
        /// </summary>
        /// <param name="path">Path of the queue relative to the service namespace base address.</param>
        public async Task DeleteQueue(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (serviceBusAdministrationClient != null)
            {
                await RetryHelper.RetryActionAsync(() => serviceBusAdministrationClient.DeleteQueueAsync(path), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueDeleted, path));
                OnDelete?.Invoke(new ServiceBusHelperEventArgs(path, EntityType.Queue));
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
        public async Task DeleteQueue(QueueProperties queueDescription)
        {
            if (queueDescription == null)
            {
                throw new ArgumentException(QueueDescriptionCannotBeNull);
            }
            if (serviceBusAdministrationClient != null)
            {
                await RetryHelper.RetryActionAsync(() => serviceBusAdministrationClient.DeleteQueueAsync(queueDescription.Name), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueDeleted, queueDescription.Name));
                OnDelete?.Invoke(new ServiceBusHelperEventArgs(queueDescription, EntityType.Queue));
            }
            else
            {
                throw new ApplicationException(ServiceBusIsDisconnected);
            }
        }

        /*
        /// <summary>
        /// Renames a queue inside a namespace.
        /// </summary>
        /// <param name="path">The path to an existing queue.</param>
        /// <param name="newPath">The new path to the renamed queue.</param>
        /// <returns>Returns a QueueDescription with the new name.</returns>
        public QueueProperties RenameQueue(string path, string newPath)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(PathCannotBeNull);
            }
            if (string.IsNullOrWhiteSpace(newPath))
            {
                throw new ArgumentException(NewPathCannotBeNull);
            }
            if (serviceBusAdministrationClient != null)
            {
                var queueDescription = RetryHelper.RetryFunc(() => serviceBusAdministrationClient.RenameQueue(path, newPath), writeToLog);
                WriteToLogIf(traceEnabled, string.Format(CultureInfo.CurrentCulture, QueueRenamed, path, newPath));
                OnDelete?.Invoke(new ServiceBusHelperEventArgs(new QueueProperties(path), EntityType.Queue));
                OnCreate?.Invoke(new ServiceBusHelperEventArgs(queueDescription, EntityType.Queue));
                return queueDescription;
            }
            throw new ApplicationException(ServiceBusIsDisconnected);
        }
        */
        
        #endregion
        
        /*/// <summary>
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
            if (serviceBusAdministrationClient == null)
            {
                throw new ApplicationException(ServiceBusIsDisconnected);

            }
            var queueClient = serviceBusClient.CreateSessionProcessor(path);
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
            if (messagingFactory == null)
            {
                throw new ApplicationException(ServiceBusIsDisconnected);

            }
            var queueClient = messagingFactory.CreateQueueClient(queue.Path);
            return RetryHelper.RetryFunc(() => dateTime != null ? queueClient.GetMessageSessions(dateTime.Value) : queueClient.GetMessageSessions(), writeToLog);
        }
        */


        
        
        #endregion
        
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
            // Todo can possibly go
            return encodingType;
        }
        
        private static bool TestNamespaceHostIsContactable(ServiceBusNamespace2 serviceBusNamespace)
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
    }
}
