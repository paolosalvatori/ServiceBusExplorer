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
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.ServiceBusExplorer.Enums;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.ServiceBusExplorer.Controls;
using Microsoft.Azure.ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus.Messaging;
using ConnectivityMode = Microsoft.ServiceBus.ConnectivityMode;
#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Forms
{
    public partial class MainForm : Form
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string DateFormat = "<{0,2:00}:{1,2:00}:{2,2:00}> {3}";
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string LogFileNameFormat = "ServiceBusExplorer {0}.txt";
        private const string EntityFileNameFormat = "{0} {1} {2}.xml";
        private const string EntitiesFileNameFormat = "{0} {1}.xml";
        private const string UrlSegmentFormat = "{0}/{1}";
        private const string NameMessageCountFormat = "{0} ({1}, {2}, {3})";
        private const string PartitionFormat = "{0,2:00}";

        //***************************
        // Messages
        //***************************
        private const string ServiceBusNamespacesNotConfigured = "Service bus accounts have not been properly configured in the configuration file.";
        private const string ServiceBusNamespaceIsNullOrEmpty = "The connection string for service bus entry {0} is null or empty.";
        private const string ServiceBusNamespaceIsWrong = "The connection string for service bus namespace {0} is in the wrong format.";
        private const string ServiceBusNamespaceNamespaceAndUriAreNullOrEmpty = "Both the uri and namespace for the service bus entry {0} is null or empty.";
        private const string ServiceBusNamespaceIssuerNameIsNullOrEmpty = "The issuer name for the service bus namespace {0} is null or empty.";
        private const string ServiceBusNamespaceIssuerSecretIsNullOrEmpty = "The issuer secret for the service bus namespace {0} is null or empty.";
        private const string ServiceBusNamespaceEndpointIsNullOrEmpty = "The endpoint for the service bus namespace {0} is null or empty.";
        private const string ServiceBusNamespaceStsEndpointIsNullOrEmpty = "The sts endpoint for the service bus namespace {0} is null or empty.";
        private const string ServiceBusNamespaceRuntimePortIsNullOrEmpty = "The runtime port for the service bus namespace {0} is null or empty.";
        private const string ServiceBusNamespaceManagementPortIsNullOrEmpty = "The management port for the service bus namespace {0} is null or empty.";
        private const string ServiceBusNamespaceEndpointUriIsInvalid = "The endpoint uri for the service bus namespace {0} is invalid.";
        private const string ServiceBusNamespaceSharedAccessKeyNameIsInvalid = "The SharedAccessKeyName for the service bus namespace {0} is invalid.";
        private const string ServiceBusNamespaceSharedAccessKeyIsInvalid = "The SharedAccessKey for the service bus namespace {0} is invalid.";
        private const string QueueRetrievedFormat = "The queue {0} has been successfully retrieved.";
        private const string TopicRetrievedFormat = "The topic {0} has been successfully retrieved.";
        private const string RelayRetrievedFormat = "The relay {0} has been successfully retrieved.";
        private const string SubscriptionRetrievedFormat = "The subscription {0} for the {1} topic has been successfully retrieved.";
        private const string RuleRetrievedFormat = "The rule {0} for the {1} subscription of the {2} topic has been successfully retrieved.";
        private const string EventHubRetrievedFormat = "The event hub {0} has been successfully retrieved.";
        private const string PartitionsRetrievedFormat = "{0} partitions for the event hub {1} have been successfully retrieved.";
        private const string ConsumerGroupsRetrievedFormat = "{0} consumer groups for the event hub {1} have been successfully retrieved.";
        private const string PartitionRetrievedFormat = "The partition {0} of the event hub {1} has been successfully retrieved.";
        private const string ConsumerGroupRetrievedFormat = "The consumer group {0} of the event hub {1} has been successfully retrieved.";
        private const string NotificationHubRetrievedFormat = "The notification hub {0} has been successfully retrieved.";
        private const string TestQueueFormat = "Test Queue: {0}";
        private const string TestTopicFormat = "Test Topic: {0}";
        private const string TestSubscriptionFormat = "Test Subscription: {0}";
        private const string CreateQueue = "Create Queue";
        private const string CreateTopic = "Create Topic";
        private const string CreateRelay = "Create Relay";
        private const string CreateSubscription = "Create Subscription";
        private const string CreateEventHub = "Create Event Hub";
        private const string CreateConsumerGroup = "Create Consumer Group";
        private const string CreateNotificationHub = "Create Notification Hub";
        private const string AddRule = "Add Rule";
        private const string ViewQueueFormat = "View Queue: {0}";
        private const string ViewTopicFormat = "View Topic: {0}";
        private const string ViewSubscriptionFormat = "View Subscription: {0}";
        private const string ViewRelayFormat = "View Relay: {0}";
        private const string ViewRuleFormat = "View Rule: {0}";
        private const string ViewEventHubFormat = "View Event Hub: {0}";
        private const string ViewPartitionFormat = "View Partition: {0}";
        private const string ViewConsumerGroupFormat = "View Consumer Group: {0}";
        private const string ViewNotificationHubFormat = "View Notification Hub: {0}";
        private const string TestRelayFormat = "Test Relay: {0}";
        private const string DeleteAllEntities = "All the entities will be permanently deleted.";
        private const string DeleteAllQueues = "All the queues will be permanently deleted.";
        private const string DeleteAllQueuesInPath = "All the queues in [{0}] will be permanently deleted.";
        private const string DeleteAllTopics = "All the topics will be permanently deleted.";
        private const string DeleteAllTopicsInPath = "All the topics in [{0}] will be permanently deleted.";
        private const string DeleteAllRelays = "All the relays will be permanently deleted.";
        private const string DeleteAllRelaysInPath = "All the relays in [{0}] will be permanently deleted.";
        private const string DeleteAllSubscriptions = "All the subscriptions will be permanently deleted.";
        private const string DeleteAllRules = "All the rules will be permanently deleted.";
        private const string DeleteAllEventHubs = "All the event hubs will be permanently deleted.";
        private const string DeleteAllConsumerGroups = "All the consumer groups of the event hub {0} will be permanently deleted with the exception of the $Default.";
        private const string DeleteAllNotificationHubs = "All the notification hubs will be permanently deleted.";
        private const string EntitiesExported = "Selected entities have been exported to {0}.";
        private const string EntitiesImported = "Entities have been imported from {0}.";
        private const string EnableQueue = "Enable Queue";
        private const string DisableQueue = "Disable Queue";
        private const string EnableTopic = "Enable Topic";
        private const string DisableTopic = "Disable Topic";
        private const string EnableSubscription = "Enable Subscription";
        private const string DisableSubscription = "Disable Subscription";
        private const string EnableEventHub = "Enable Event Hub";
        private const string DisableEventHub = "Disable Event Hub";
        private const string SubscriptionIdCannotBeNull = "In order to use metrics, you need to define the Microsoft Azure Subscription Id in the configuration file or Options form.";
        private const string ManagementCertificateThumbprintCannotBeNull = "In order to use metrics, you need to define in the configuration file or Options form the thumbprint of a valid management certificate for your Microsoft Azure subscription.";
        private const string NoNamespaceWithKeyMessageFormat = "No namespace with key equal to [{0}] exists in the serviceBusNamespaces section of the configuration file.";

        //***************************
        // Constants
        //***************************
        private const string ServiceBusNamespaces = "serviceBusNamespaces";
        private const string BrokeredMessageInspectors = "brokeredMessageInspectors";
        private const string EventDataInspectors = "eventDataInspectors";
        private const string BrokeredMessageGenerators = "brokeredMessageGenerators";
        private const string EventDataGenerators = "eventDataGenerators";
        private const string AllEntities = "Entities";
        private const string QueueEntities = "Queues";
        private const string TopicEntities = "Topics";
        private const string SubscriptionEntities = "Subscriptions";
        private const string PartitionEntities = "Partitions";
        private const string ConsumerGroupEntities = "Consumer Groups";
        private const string FilteredQueueEntities = "Queues (Filtered)";
        private const string FilteredTopicEntities = "Topics (Filtered)";
        private const string FilteredSubscriptionEntities = "Subscriptions (Filtered)";
        private const string RuleEntities = "Rules";
        private const string RelayEntities = "Relays";
        private const string EventHubEntities = "Event Hubs";
        private const string NotificationHubEntities = "Notification Hubs";
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string SubscriptionEntity = "Subscription";
        private const string RuleEntity = "Rule";
        private const string RelayEntity = "Relay";
        private const string EventHubEntity = "Event Hub";
        private const string ConsumerGroupEntity = "Consumer Group";
        private const string NotificationHubEntity = "Notification Hub";
        private const string Entity = "Entity";
        private const string SaveAsTitle = "Save Log As";
        private const string SaveEntityAsTitle = "Save File As";
        private const string OpenEntityAsTitle = "Open File";
        private const string SaveAsExtension = "txt";
        private const string XmlExtension = "xml";
        private const string SaveAsFilter = "Text Documents|*.txt";
        private const string XmlFilter = "XML Files|*.xml";
        private const string DefaultMessageText = "Hi mate, how are you?";
        private const string DefaultLabel = "Service Bus Explorer";
        private const string ImportToolStripMenuItemName = "importEntityMenuItem2";
        private const string ImportToolStripMenuItemText = "Import Entities";
        private const string ImportToolStripMenuItemToolTipText = "Import entity definition from file.";
        private const string EventClick = "EventClick";
        private const string EventsProperty = "Events";
        private const string ChangeStatusQueueMenuItem = "changeStatusQueueMenuItem";
        private const string ChangeStatusTopicMenuItem = "changeStatusTopicMenuItem";
        private const string ChangeStatusSubscriptionMenuItem = "changeStatusSubscriptionMenuItem";
        private const string ChangeStatusEventHubMenuItem = "changeStatusEventHubMenuItem";
        private const string MetricsHeader = "Namespace Metrics";
        private const string DefaultConsumerGroupName = "$Default";

        //***************************
        // Parameters
        //***************************
        private const string ConnectionStringUri = "uri";
        private const string ConnectionStringNameSpace = "namespace";
        private const string ConnectionStringServicePath = "servicepath";
        private const string ConnectionStringIssuerName = "issuername";
        private const string ConnectionStringIssuerSecret = "issuersecret";
        private const string ConnectionStringOwner = "owner";
        private const string ConnectionStringEndpoint = "endpoint";
        private const string ConnectionStringSharedAccessKeyName = "sharedaccesskeyname";
        private const string ConnectionStringSharedAccessKey = "sharedaccesskey";
        private const string ConnectionStringStsEndpoint = "stsendpoint";
        private const string ConnectionStringRuntimePort = "runtimeport";
        private const string ConnectionStringManagementPort = "managementport";
        private const string ConnectionStringWindowsUsername = "windowsusername";
        private const string ConnectionStringWindowsDomain = "windowsdomain";
        private const string ConnectionStringWindowsPassword = "windowspassword";
        private const string ConnectionStringSharedSecretIssuer = "sharedsecretissuer";
        private const string ConnectionStringSharedSecretValue = "sharedsecretvalue";
        private const string ConnectionStringTransportType = "transporttype";
        private const string ConnectionStringEntityPath = "entitypath";

        //***************************
        // Icons
        //***************************
        private const int QueueListIconIndex = 0;
        private const int TopicListIconIndex = 1;
        private const int QueueIconIndex = 2;
        private const int TopicIconIndex = 3;
        private const int SubscriptionListIconIndex = 4;
        private const int SubscriptionIconIndex = 5;
        private const int RuleListIconIndex = 4;
        private const int RuleIconIndex = 6;
        private const int AzureIconIndex = 7;
        private const int RelayListIconIndex = 8;
        private const int RelayLeafIconIndex = 9;
        internal const int UrlSegmentIconIndex = 10;
        private const int GreyQueueIconIndex = 12;
        private const int GreyTopicIconIndex = 13;
        private const int GreySubscriptionIconIndex = 14;
        private const int NotificationHubListIconIndex = 15;
        private const int NotificationHubIconIndex = 16;
        private const int EventHubListIconIndex = 17;
        private const int EventHubIconIndex = 18;
        private const int GreyEventHubIconIndex = 19;
        private const int PartitionListIconIndex = 4;
        private const int PartitionIconIndex = 20;
        private const int ConsumerGroupListIconIndex = 4;
        private const int ConsumerGroupIconIndex = 21;

        //***************************
        // Sizes
        //***************************
        private const int ControlMinWidth = 816;
        private const int ControlMinHeight = 345;
        #endregion

        #region Private Instance Fields
        private readonly ServiceBusHelper serviceBusHelper;
        private TreeNode rootNode;
        private TreeNode currentNode;
        private readonly FieldInfo eventClickFieldInfo;
        private readonly PropertyInfo eventsPropertyInfo;
        private string messageText;
        private string relayMessageText;
        private string messageFile;
        private string label;
        private string subscriptionId;
        private string certificateThumbprint;
        private string microsoftServiceBusConnectionString;
        private bool importing;
        private readonly int mainSplitterDistance;
        private readonly int splitterContainerDistance;
        private float treeViewFontSize;
        private float logFontSize;
        private int topCount = 10;
        private int receiveTimeout = 1;
        private int serverTimeout = 5;
        private int prefetchCount;
        private int senderThinkTime = 100;
        private int receiverThinkTime = 100;
        private int monitorRefreshInterval = 30;
        private bool showMessageCount = true;
        private bool saveMessageToFile = true;
        private bool savePropertiesToFile = true;
        private bool saveCheckpointsToFile = true;
        private bool useAscii = true;
        private readonly List<Tuple<string, string>> fileNames = new List<Tuple<string, string>>();
        private readonly string argumentName;
        private readonly string argumentValue;
        private List<string> selectedEntites = new List<string>();
        private readonly List<string> entities = new List<string> { QueueEntities, TopicEntities, EventHubEntities, NotificationHubEntities, RelayEntities };
        private BlockingCollection<string> logCollection = new BlockingCollection<string>();
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Task logTask;
        private string version;
        #endregion

        #region Private Static Fields
        private static MainForm mainSingletonMainForm;
        #endregion

        #region Public Constructor
        /// <summary>
        /// Initializes a new instance of the MainForm class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            logTask = Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    WriteToLog(t.Exception.Message);
                }
            });
            mainSplitterDistance = mainSplitContainer.SplitterDistance;
            splitterContainerDistance = splitContainer.SplitterDistance;
            treeViewFontSize = serviceBusTreeView.Font.Size;
            logFontSize = lstLog.Font.Size;
            Trace.Listeners.Add(new LogTraceListener());
            mainSingletonMainForm = this;
            serviceBusHelper = new ServiceBusHelper(WriteToLog);
            serviceBusHelper.OnCreate += serviceBusHelper_OnCreate;
            serviceBusHelper.OnDelete += serviceBusHelper_OnDelete;
            serviceBusTreeView.TreeViewNodeSorter = new TreeViewHelper();
            eventClickFieldInfo = typeof(ToolStripItem).GetField(EventClick, BindingFlags.NonPublic | BindingFlags.Static);
            eventsPropertyInfo = typeof(Component).GetProperty(EventsProperty, BindingFlags.NonPublic | BindingFlags.Instance);
            GetServiceBusNamespacesFromConfiguration();
            GetServiceBusNamespaceFromEnvironmentVariable();
            GetBrokeredMessageInspectorsFromConfiguration();
            GetEventDataInspectorsFromConfiguration();
            GetBrokeredMessageGeneratorsFromConfiguration();
            GetEventDataGeneratorsFromConfiguration();
            GetServiceBusNamespaceSettingsFromConfiguration();
            ReadEventHubPartitionCheckpointFile();
        }

        /// <summary>
        /// Initializes a new instance of the MainForm class.
        /// </summary>
        /// <param name="argument">Argument type (n or c).</param>
        /// <param name="value">Argument value</param>
        public MainForm(string argument, string value)
            : this()
        {
            argumentName = argument;
            argumentValue = value;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Opens the options dialog.
        /// </summary>
        /// <param name="sender">MainForm object</param>
        /// <param name="e">System.EventArgs parameter</param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var optionForm = new OptionForm(subscriptionId,
                                                   certificateThumbprint,
                                                   label,
                                                   messageFile,
                                                   messageText,
                                                   (decimal)lstLog.Font.Size,
                                                   (decimal)serviceBusTreeView.Font.Size,
                                                   RetryHelper.RetryCount,
                                                   RetryHelper.RetryTimeout,
                                                   receiveTimeout,
                                                   serverTimeout,
                                                   senderThinkTime,
                                                   receiverThinkTime,
                                                   monitorRefreshInterval,
                                                   prefetchCount,
                                                   topCount,
                                                   showMessageCount,
                                                   saveMessageToFile,
                                                   savePropertiesToFile,
                                                   saveCheckpointsToFile,
                                                   useAscii,
                                                   entities,
                                                   selectedEntites))
            {
                if (optionForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                subscriptionId = optionForm.SubscriptionId;
                certificateThumbprint = optionForm.CertificateThumbprint;
                label = optionForm.Label;
                messageFile = optionForm.MessageFile;
                messageText = optionForm.MessageText;
                lstLog.Font = new Font(lstLog.Font.FontFamily, (float)optionForm.LogFontSize);
                serviceBusTreeView.Font = new Font(serviceBusTreeView.Font.FontFamily,
                                                   (float)optionForm.TreeViewFontSize);
                RetryHelper.RetryCount = optionForm.RetryCount;
                RetryHelper.RetryTimeout = optionForm.RetryTimeout;
                receiveTimeout = optionForm.ReceiveTimeout;
                serverTimeout = optionForm.ServerTimeout;
                senderThinkTime = optionForm.SenderThinkTime;
                receiverThinkTime = optionForm.ReceiverThinkTime;
                monitorRefreshInterval = optionForm.MonitorRefreshInterval;
                prefetchCount = optionForm.PrefetchCount;
                topCount = optionForm.TopCount;
                if (showMessageCount != optionForm.ShowMessageCount)
                {
                    showMessageCount = optionForm.ShowMessageCount;
                    GetEntities(ServiceBusExplorer.Enums.EntityType.All);
                }
                saveMessageToFile = optionForm.SaveMessageToFile;
                savePropertiesToFile = optionForm.SavePropertiesToFile;
                saveCheckpointsToFile = optionForm.SaveCheckpointsToFile;
                useAscii = optionForm.UseAscii;
                selectedEntites = optionForm.SelectedEntities;
            }
        }

        /// <summary>
        /// Saves the log to a text file
        /// </summary>
        /// <param name="sender">MainForm object</param>
        /// <param name="e">System.EventArgs parameter</param>
        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstLog == null || lstLog.Items.Count <= 0)
            {
                return;
            }
            SaveLog(true);
        }

        /// <summary>
        /// Handles cancel events raised by user defined controls.
        /// </summary>
        void MainForm_OnCancel()
        {
            foreach (var userControl in panelMain.Controls.OfType<UserControl>())
            {
                userControl.Dispose();
            }
            panelMain.Controls.Clear();
            panelMain.BackColor = SystemColors.Window;
            panelMain.HeaderText = Entity;
            if (currentNode != null)
            {
                serviceBusTreeView.SelectedNode = currentNode;
                currentNode.EnsureVisible();
                HandleNodeMouseClick(currentNode);
            }
            else
            {
                serviceBusTreeView.SelectedNode = rootNode;
                rootNode.EnsureVisible();
                HandleNodeMouseClick(rootNode);
            }
        }

        /// <summary>
        /// Handles refresh events raised by user defined controls.
        /// </summary>
        void MainForm_OnRefresh()
        {
            if (currentNode != null)
            {
                serviceBusTreeView.SelectedNode = currentNode;
                currentNode.EnsureVisible();
                refreshEntity_Click(null, null);
            }
        }

        /// <summary>
        /// Handles refresh events raised by user defined controls.
        /// </summary>
        void MainForm_OnChangeStatus()
        {
            if (currentNode != null)
            {
                serviceBusTreeView.SelectedNode = currentNode;
                currentNode.EnsureVisible();
                changeStatusEntity_Click(null, null);
            }
        }

        /// <summary>
        /// Individuates and remove the node corresponding to the deleted entity.
        /// </summary>
        /// <param name="args">The ServiceBusHelperEventArgs object containing the reference to the deleted entity.</param>
        void serviceBusHelper_OnDelete(ServiceBusHelperEventArgs args)
        {
            try
            {
                serviceBusTreeView.SuspendDrawing();
                serviceBusTreeView.SuspendLayout();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.Window;
                panelMain.HeaderText = Entity;
                serviceBusTreeView.SelectedNode = rootNode;
                rootNode.EnsureVisible();
                // QueueDescription Entity
                if (args.EntityType == EntityType.Queue)
                {
                    string queueName = null;
                    if (args.EntityInstance is string)
                    {
                        queueName = args.EntityInstance as string;
                    }
                    else
                    {
                        var queueDescription = args.EntityInstance as QueueDescription;
                        if (queueDescription != null)
                        {
                            queueName = queueDescription.Path;
                        }
                    }
                    var queueListNode = FindNode(QueueEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(queueName))
                    {
                        DeleteNode(queueName, queueListNode);
                    }
                    else
                    {
                        GetEntities(EntityType.Queue);
                    }
                    serviceBusTreeView.SelectedNode = queueListNode;
                    queueListNode.EnsureVisible();
                    HandleNodeMouseClick(queueListNode);
                    return;
                }
                // TopicDescription Entity
                if (args.EntityType == EntityType.Topic)
                {
                    string topicName = null;
                    if (args.EntityInstance is string)
                    {
                        topicName = args.EntityInstance as string;
                    }
                    else
                    {
                        var topicDescription = args.EntityInstance as TopicDescription;
                        if (topicDescription != null)
                        {
                            topicName = topicDescription.Path;
                        }
                    }
                    var topicListNode = FindNode(TopicEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(topicName))
                    {
                        DeleteNode(topicName, topicListNode);
                    }
                    else
                    {
                        GetEntities(EntityType.Topic);
                    }
                    serviceBusTreeView.SelectedNode = topicListNode;
                    topicListNode.EnsureVisible();
                    HandleNodeMouseClick(topicListNode);
                    return;
                }
                // RelayDescription Entity
                if (args.EntityType == EntityType.Relay)
                {
                    string relayName = null;
                    if (args.EntityInstance is string)
                    {
                        relayName = args.EntityInstance as string;
                    }
                    else
                    {
                        var relayDescription = args.EntityInstance as RelayDescription;
                        if (relayDescription != null)
                        {
                            relayName = relayDescription.Path;
                        }
                    }
                    var relayListNode = FindNode(RelayEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(relayName))
                    {
                        DeleteNode(relayName, relayListNode);
                    }
                    else
                    {
                        GetEntities(EntityType.Relay);
                    }
                    serviceBusTreeView.SelectedNode = relayListNode;
                    relayListNode.EnsureVisible();
                    HandleNodeMouseClick(relayListNode);
                    return;
                }
                // EventHubDescription Entity
                if (args.EntityType == EntityType.EventHub)
                {
                    string eventHubName = null;
                    if (args.EntityInstance is string)
                    {
                        eventHubName = args.EntityInstance as string;
                    }
                    else
                    {
                        var eventHubDescription = args.EntityInstance as EventHubDescription;
                        if (eventHubDescription != null)
                        {
                            eventHubName = eventHubDescription.Path;
                        }
                    }
                    var eventHubListNode = FindNode(EventHubEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(eventHubName))
                    {
                        DeleteNode(eventHubName, eventHubListNode);
                    }
                    else
                    {
                        GetEntities(EntityType.EventHub);
                    }
                    serviceBusTreeView.SelectedNode = eventHubListNode;
                    eventHubListNode.EnsureVisible();
                    HandleNodeMouseClick(eventHubListNode);
                    return;
                }
                // NotificationHubDescription Entity
                if (args.EntityType == EntityType.NotificationHub)
                {
                    string notificationHubName = null;
                    if (args.EntityInstance is string)
                    {
                        notificationHubName = args.EntityInstance as string;
                    }
                    else
                    {
                        var notificationHubDescription = args.EntityInstance as NotificationHubDescription;
                        if (notificationHubDescription != null)
                        {
                            notificationHubName = notificationHubDescription.Path;
                        }
                    }
                    var notificationHubListNode = FindNode(NotificationHubEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(notificationHubName))
                    {
                        DeleteNode(notificationHubName, notificationHubListNode);
                    }
                    else
                    {
                        GetEntities(EntityType.NotificationHub);
                    }
                    serviceBusTreeView.SelectedNode = notificationHubListNode;
                    notificationHubListNode.EnsureVisible();
                    HandleNodeMouseClick(notificationHubListNode);
                    return;
                }
                // SubscriptionDescription Entity
                if (args.EntityType == EntityType.Subscription)
                {
                    var topicListNode = FindNode(TopicEntities, rootNode);
                    var subscription = args.EntityInstance as SubscriptionDescription;
                    if (subscription != null &&
                        !string.IsNullOrWhiteSpace(subscription.TopicPath))
                    {
                        var topicNode = FindNode(subscription.TopicPath, topicListNode);
                        if (topicNode == null)
                        {
                            GetEntities(EntityType.Topic);
                            return;
                        }
                        if (topicNode.Nodes.ContainsKey(SubscriptionEntities))
                        {
                            var subscriptionsNode = topicNode.Nodes[SubscriptionEntities];
                            if (subscriptionsNode.Nodes.ContainsKey(subscription.Name))
                            {
                                subscriptionsNode.Nodes.RemoveByKey(subscription.Name);
                                if (subscriptionsNode.Nodes.Count == 0)
                                {
                                    topicNode.Nodes.Clear();
                                    serviceBusTreeView.SelectedNode = topicNode;
                                    topicNode.EnsureVisible();
                                    HandleNodeMouseClick(topicNode);
                                }
                                else
                                {
                                    subscriptionsNode.Expand();
                                    serviceBusTreeView.SelectedNode = subscriptionsNode;
                                    subscriptionsNode.EnsureVisible();
                                    HandleNodeMouseClick(subscriptionsNode);
                                }
                            }
                            else
                            {
                                GetEntities(EntityType.Topic);
                                return;
                            }
                        }
                        else
                        {
                            GetEntities(EntityType.Topic);
                            return;
                        }
                    }
                    else
                    {
                        GetEntities(EntityType.Topic);
                        return;
                    }
                    serviceBusTreeView.SelectedNode = null;
                    return;
                }
                // RuleDescription Entity
                if (args.EntityType == EntityType.Rule)
                {
                    var topicListNode = FindNode(TopicEntities, rootNode);
                    var wrapper = args.EntityInstance as RuleWrapper;
                    if (wrapper != null &&
                        wrapper.RuleDescription != null &&
                        wrapper.SubscriptionDescription != null &&
                        !string.IsNullOrWhiteSpace(wrapper.RuleDescription.Name) &&
                        !string.IsNullOrWhiteSpace(wrapper.SubscriptionDescription.TopicPath))
                    {
                        var topicNode = FindNode(wrapper.SubscriptionDescription.TopicPath, topicListNode);
                        if (topicNode == null)
                        {
                            GetEntities(EntityType.Topic);
                            return;
                        }
                        if (topicNode.Nodes.ContainsKey(SubscriptionEntities))
                        {
                            var subscriptionsNode = topicNode.Nodes[SubscriptionEntities];
                            if (subscriptionsNode.Nodes.ContainsKey(wrapper.SubscriptionDescription.Name))
                            {
                                var subscriptionNode = subscriptionsNode.Nodes[wrapper.SubscriptionDescription.Name];
                                if (subscriptionNode.Nodes.ContainsKey(RuleEntities))
                                {
                                    var rulesNode = subscriptionNode.Nodes[RuleEntities];
                                    if (rulesNode.Nodes.ContainsKey(wrapper.RuleDescription.Name))
                                    {
                                        rulesNode.Nodes.RemoveByKey(wrapper.RuleDescription.Name);
                                        if (rulesNode.Nodes.Count == 0)
                                        {
                                            subscriptionNode.Nodes.Clear();
                                            serviceBusTreeView.SelectedNode = subscriptionNode;
                                            subscriptionNode.EnsureVisible();
                                            HandleNodeMouseClick(subscriptionsNode);
                                        }
                                        else
                                        {
                                            rulesNode.Expand();
                                            serviceBusTreeView.SelectedNode = rulesNode;
                                            rulesNode.EnsureVisible();
                                            HandleNodeMouseClick(rulesNode);
                                        }
                                    }
                                }
                                else
                                {
                                    GetEntities(EntityType.Topic);
                                    return;
                                }
                            }
                            else
                            {
                                GetEntities(EntityType.Topic);
                                return;
                            }
                        }
                        else
                        {
                            GetEntities(EntityType.Topic);
                            return;
                        }
                    }
                    else
                    {
                        GetEntities(EntityType.Topic);
                        return;
                    }
                    serviceBusTreeView.SelectedNode = null;
                }
                // ConsumerGroupDescription Entity
                if (args.EntityType == EntityType.ConsumerGroup)
                {
                    var eventHubListNode = FindNode(EventHubEntities, rootNode);
                    var notificationHub = args.EntityInstance as ConsumerGroupDescription;
                    if (notificationHub != null &&
                        !string.IsNullOrWhiteSpace(notificationHub.EventHubPath))
                    {
                        var eventHubNode = FindNode(notificationHub.EventHubPath, eventHubListNode);
                        if (eventHubNode == null)
                        {
                            GetEntities(EntityType.EventHub);
                            return;
                        }
                        if (eventHubNode.Nodes.ContainsKey(ConsumerGroupEntities))
                        {
                            var notificationHubsNode = eventHubNode.Nodes[ConsumerGroupEntities];
                            if (notificationHubsNode.Nodes.ContainsKey(notificationHub.Name))
                            {
                                notificationHubsNode.Nodes.RemoveByKey(notificationHub.Name);
                                if (notificationHubsNode.Nodes.Count == 0)
                                {
                                    eventHubNode.Nodes.Clear();
                                    serviceBusTreeView.SelectedNode = eventHubNode;
                                    eventHubNode.EnsureVisible();
                                    HandleNodeMouseClick(eventHubNode);
                                }
                                else
                                {
                                    notificationHubsNode.Expand();
                                    serviceBusTreeView.SelectedNode = notificationHubsNode;
                                    notificationHubsNode.EnsureVisible();
                                    HandleNodeMouseClick(notificationHubsNode);
                                }
                            }
                            else
                            {
                                GetEntities(EntityType.EventHub);
                                return;
                            }
                        }
                        else
                        {
                            GetEntities(EntityType.EventHub);
                            return;
                        }
                    }
                    else
                    {
                        GetEntities(EntityType.EventHub);
                        return;
                    }
                    serviceBusTreeView.SelectedNode = null;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                serviceBusTreeView.ResumeDrawing();
                serviceBusTreeView.ResumeLayout();
            }
        }

        /// <summary>
        /// Adds a node to the treeview for the newly created entity.
        /// </summary>
        /// <param name="args">The ServiceBusHelperEventArgs object containing the reference to the newly created entity.</param>
        // ReSharper disable once FunctionComplexityOverflow
        void serviceBusHelper_OnCreate(ServiceBusHelperEventArgs args)
        {
            try
            {
                serviceBusTreeView.SuspendDrawing();
                serviceBusTreeView.SuspendLayout();
                // QueueDescription Entity
                if (args.EntityType == EntityType.Queue)
                {
                    var queue = args.EntityInstance as QueueDescription;
                    if (queue != null)
                    {
                        var queueListNode = FindNode(QueueEntities, rootNode);
                        var node = CreateNode(queue.Path, queue, queueListNode, false);
                        if (node == null)
                        {
                            return;
                        }
                        serviceBusTreeView.Sort();
                        panelMain.HeaderText = string.Format(ViewQueueFormat, queue.Path);
                        if (!importing)
                        {
                            serviceBusTreeView.SelectedNode = node;
                            node.EnsureVisible();
                            HandleNodeMouseClick(serviceBusTreeView.SelectedNode);
                        }
                        else
                        {
                            queueListNode.Expand();
                        }
                    }
                    return;
                }
                // TopicDescription Entity
                if (args.EntityType == EntityType.Topic)
                {
                    var topic = args.EntityInstance as TopicDescription;
                    if (topic != null)
                    {
                        var topicListNode = FindNode(TopicEntities, rootNode);
                        var node = CreateNode(topic.Path, topic, topicListNode, false);
                        if (node == null)
                        {
                            return;
                        }
                        serviceBusTreeView.Sort();
                        panelMain.HeaderText = string.Format(ViewTopicFormat, topic.Path);
                        if (!importing)
                        {
                            serviceBusTreeView.SelectedNode = node;
                            node.EnsureVisible();
                            HandleNodeMouseClick(serviceBusTreeView.SelectedNode);
                        }
                        else
                        {
                            topicListNode.Expand();
                        }
                    }
                    return;
                }
                // RelayDescription Entity
                if (args.EntityType == EntityType.Relay)
                {
                    var relay = args.EntityInstance as RelayDescription;
                    if (relay != null)
                    {
                        var relayListNode = FindNode(RelayEntities, rootNode);
                        var node = CreateNode(relay.Path, relay, relayListNode, false);
                        if (node == null)
                        {
                            return;
                        }
                        serviceBusTreeView.Sort();
                        panelMain.HeaderText = string.Format(ViewRelayFormat, relay.Path);
                        if (!importing)
                        {
                            serviceBusTreeView.SelectedNode = node;
                            node.EnsureVisible();
                            HandleNodeMouseClick(serviceBusTreeView.SelectedNode);
                        }
                        else
                        {
                            relayListNode.Expand();
                        }
                    }
                    return;
                }
                // EventHubDescription Entity
                if (args.EntityType == EntityType.EventHub)
                {
                    var eventHub = args.EntityInstance as EventHubDescription;
                    if (eventHub != null)
                    {
                        var eventHubListNode = FindNode(EventHubEntities, rootNode);
                        var node = CreateNode(eventHub.Path, eventHub, eventHubListNode, false);
                        if (node == null)
                        {
                            return;
                        }
                        serviceBusTreeView.Sort();
                        panelMain.HeaderText = string.Format(ViewEventHubFormat, eventHub.Path);
                        if (!importing)
                        {
                            serviceBusTreeView.SelectedNode = node;
                            node.EnsureVisible();
                            HandleNodeMouseClick(serviceBusTreeView.SelectedNode);
                        }
                        else
                        {
                            eventHubListNode.Expand();
                        }
                    }
                    return;
                }
                // NotificationHubDescription Entity
                if (args.EntityType == EntityType.NotificationHub)
                {
                    var notificationHub = args.EntityInstance as NotificationHubDescription;
                    if (notificationHub != null)
                    {
                        var notificationHubListNode = FindNode(NotificationHubEntities, rootNode);
                        var node = CreateNode(notificationHub.Path, notificationHub, notificationHubListNode, false);
                        if (node == null)
                        {
                            return;
                        }
                        serviceBusTreeView.Sort();
                        panelMain.HeaderText = string.Format(ViewNotificationHubFormat, notificationHub.Path);
                        if (!importing)
                        {
                            serviceBusTreeView.SelectedNode = node;
                            node.EnsureVisible();
                            HandleNodeMouseClick(serviceBusTreeView.SelectedNode);
                        }
                        else
                        {
                            notificationHubListNode.Expand();
                        }
                    }
                    return;
                }
                // SubscriptionDescription Entity
                if (args.EntityType == EntityType.Subscription)
                {
                    var wrapper = args.EntityInstance as SubscriptionWrapper;
                    if (wrapper == null ||
                        wrapper.TopicDescription == null ||
                        wrapper.SubscriptionDescription == null)
                    {
                        return;
                    }
                    var topicListNode = FindNode(TopicEntities, rootNode);
                    var topicNode = FindNode(wrapper.TopicDescription.Path, topicListNode);
                    if (topicNode != null)
                    {
                        TreeNode subscriptionsNode;

                        if (topicNode.Nodes.ContainsKey(SubscriptionEntities))
                        {
                            subscriptionsNode = topicNode.Nodes[SubscriptionEntities];
                        }
                        else
                        {
                            subscriptionsNode = topicNode.Nodes.Add(SubscriptionEntities,
                                                                    SubscriptionEntities,
                                                                    wrapper.SubscriptionDescription.Status == EntityStatus.Active ? SubscriptionListIconIndex : GreySubscriptionIconIndex,
                                                                    wrapper.SubscriptionDescription.Status == EntityStatus.Active ? SubscriptionListIconIndex : GreySubscriptionIconIndex);
                            subscriptionsNode.ContextMenuStrip = subscriptionsContextMenuStrip;
                            subscriptionsNode.Tag = new SubscriptionWrapper(null, wrapper.TopicDescription, FilterExpressionHelper.SubscriptionFilterExpression);
                        }
                        var subscriptionNode = subscriptionsNode.Nodes.Add(wrapper.SubscriptionDescription.Name,
                                                                           showMessageCount ?
                                                                           string.Format(NameMessageCountFormat,
                                                                                         wrapper.SubscriptionDescription.Name,
                                                                                         wrapper.SubscriptionDescription.MessageCountDetails.ActiveMessageCount,
                                                                                         wrapper.SubscriptionDescription.MessageCountDetails.DeadLetterMessageCount,
                                                                                         wrapper.SubscriptionDescription.MessageCountDetails.TransferDeadLetterMessageCount) :
                                                                           wrapper.SubscriptionDescription.Name,
                                                                           wrapper.SubscriptionDescription.Status == EntityStatus.Active ? SubscriptionIconIndex : GreySubscriptionIconIndex,
                                                                           wrapper.SubscriptionDescription.Status == EntityStatus.Active ? SubscriptionIconIndex : GreySubscriptionIconIndex);
                        subscriptionNode.ContextMenuStrip = subscriptionContextMenuStrip;
                        subscriptionNode.Tag = new SubscriptionWrapper(wrapper.SubscriptionDescription, wrapper.TopicDescription);
                        subscriptionsNode.Expand();
                        panelMain.HeaderText = string.Format(ViewSubscriptionFormat, wrapper.SubscriptionDescription.Name);
                        if (!importing)
                        {
                            serviceBusTreeView.SelectedNode = subscriptionsNode.Nodes[wrapper.SubscriptionDescription.Name];
                            serviceBusTreeView.SelectedNode.EnsureVisible();
                            HandleNodeMouseClick(serviceBusTreeView.SelectedNode);
                        }
                        var rules = serviceBusHelper.GetRules(wrapper.SubscriptionDescription);
                        var ruleDescriptions = rules as RuleDescription[] ?? rules.ToArray();
                        if (ruleDescriptions.Any())
                        {
                            subscriptionNode.Nodes.Clear();
                            var rulesNode = subscriptionNode.Nodes.Add(RuleEntities, RuleEntities, RuleListIconIndex, RuleListIconIndex);
                            rulesNode.ContextMenuStrip = rulesContextMenuStrip;
                            rulesNode.Tag = new RuleWrapper(null, wrapper.SubscriptionDescription);
                            foreach (var rule in ruleDescriptions)
                            {
                                var ruleNode = rulesNode.Nodes.Add(rule.Name, rule.Name, RuleIconIndex, RuleIconIndex);
                                ruleNode.ContextMenuStrip = ruleContextMenuStrip;
                                ruleNode.Tag = new RuleWrapper(rule, wrapper.SubscriptionDescription);
                            }
                        }
                    }
                    return;
                }
                // RuleDescription Entity
                if (args.EntityType == EntityType.Rule)
                {
                    var wrapper = args.EntityInstance as RuleWrapper;
                    if (wrapper == null ||
                        wrapper.SubscriptionDescription == null ||
                        wrapper.RuleDescription == null)
                    {
                        return;
                    }
                    var topicListNode = FindNode(TopicEntities, rootNode);
                    var topicNode = FindNode(wrapper.SubscriptionDescription.TopicPath, topicListNode);
                    if (topicNode != null)
                    {
                        if (topicNode.Nodes.ContainsKey(SubscriptionEntities))
                        {
                            var subscriptionsNode = topicNode.Nodes[SubscriptionEntities];
                            if (subscriptionsNode.Nodes.ContainsKey(wrapper.SubscriptionDescription.Name))
                            {
                                var subscriptionNode = subscriptionsNode.Nodes[wrapper.SubscriptionDescription.Name];
                                TreeNode rulesNode;
                                if (subscriptionNode.Nodes.ContainsKey(RuleEntities))
                                {
                                    rulesNode = subscriptionNode.Nodes[RuleEntities];
                                }
                                else
                                {
                                    rulesNode = subscriptionNode.Nodes.Add(RuleEntities, RuleEntities, RuleListIconIndex, RuleListIconIndex);
                                    rulesNode.ContextMenuStrip = rulesContextMenuStrip;
                                    rulesNode.Tag = new RuleWrapper(null, wrapper.SubscriptionDescription);
                                }
                                var ruleNode = rulesNode.Nodes.Add(wrapper.RuleDescription.Name, wrapper.RuleDescription.Name, RuleIconIndex, RuleIconIndex);
                                ruleNode.ContextMenuStrip = ruleContextMenuStrip;
                                ruleNode.Tag = wrapper;
                                rulesNode.Expand();
                                panelMain.HeaderText = string.Format(ViewRuleFormat, wrapper.RuleDescription.Name);
                                if (!importing)
                                {
                                    serviceBusTreeView.SelectedNode = rulesNode.Nodes[wrapper.RuleDescription.Name];
                                    serviceBusTreeView.SelectedNode.EnsureVisible();
                                }
                            }
                        }
                    }
                }
                // ConsumerGroupDescription Entity
                if (args.EntityType == EntityType.ConsumerGroup)
                {
                    var consumerGroupDescription = args.EntityInstance as ConsumerGroupDescription;
                    if (consumerGroupDescription == null)
                    {
                        return;
                    }
                    var eventHubListNode = FindNode(EventHubEntities, rootNode);
                    var eventHubNode = FindNode(consumerGroupDescription.EventHubPath, eventHubListNode);
                    if (eventHubNode != null)
                    {
                        TreeNode consumerGroupsNode;

                        if (eventHubNode.Nodes.ContainsKey(ConsumerGroupEntities))
                        {
                            consumerGroupsNode = eventHubNode.Nodes[ConsumerGroupEntities];
                        }
                        else
                        {
                            consumerGroupsNode = eventHubNode.Nodes.Add(ConsumerGroupEntities, ConsumerGroupEntities, ConsumerGroupListIconIndex, ConsumerGroupListIconIndex);
                            consumerGroupsNode.ContextMenuStrip = consumerGroupsContextMenuStrip;
                            consumerGroupsNode.Tag = eventHubNode.Tag;
                        }
                        var eventHubDescription = eventHubNode.Tag as EventHubDescription;
                        if (eventHubDescription == null)
                        {
                            return;
                        }

                        var partitions = GetPartitionsFromDefaultConsumerGroup(eventHubNode) ??
                                         GetPartitionsFromPartitionIds(eventHubDescription);
                        var partitionDescriptions = partitions as IList<PartitionDescription> ?? partitions.ToList();
                        var consumerGroupNode = CreateEventHubConsumerGroupNode(eventHubDescription, consumerGroupDescription, partitionDescriptions, consumerGroupsNode);
                        if (consumerGroupNode == null)
                        {
                            return;
                        }
                        consumerGroupNode.ContextMenuStrip = consumerGroupContextMenuStrip;
                        consumerGroupNode.Tag = consumerGroupDescription;
                        consumerGroupsNode.Expand();
                        panelMain.HeaderText = string.Format(ViewConsumerGroupFormat, consumerGroupDescription.Name);
                        if (!importing)
                        {
                            serviceBusTreeView.SelectedNode = consumerGroupsNode.Nodes[consumerGroupDescription.Name];
                            serviceBusTreeView.SelectedNode.EnsureVisible();
                            HandleNodeMouseClick(serviceBusTreeView.SelectedNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                serviceBusTreeView.ResumeDrawing();
                serviceBusTreeView.ResumeLayout();
            }
        }

        private IEnumerable<PartitionDescription> GetPartitionsFromDefaultConsumerGroup(TreeNode eventHubNode)
        {
            var consumerGroupsNode = FindNode(ConsumerGroupEntities, eventHubNode);
            if (consumerGroupsNode == null)
            {
                return null;
            }
            var defaultConsumerGroupNode = consumerGroupsNode.Nodes[DefaultConsumerGroupName];
            if (defaultConsumerGroupNode == null)
            {
                return null;
            }
            var partitionsNode = defaultConsumerGroupNode.Nodes[PartitionEntities];
            if (partitionsNode == null)
            {
                return null;
            }
            return partitionsNode.Nodes.Cast<TreeNode>().Select(n => n.Tag as PartitionDescription);
        }

        private IEnumerable<PartitionDescription> GetPartitionsFromConsumerGroup(TreeNode consumerGroupNode)
        {
            if (consumerGroupNode == null)
            {
                return null;
            }
            var partitionsNode = consumerGroupNode.Nodes[PartitionEntities];
            if (partitionsNode == null)
            {
                return null;
            }
            return partitionsNode.Nodes.Cast<TreeNode>().Select(n => n.Tag as PartitionDescription);
        }

        private IEnumerable<PartitionDescription> GetPartitionsFromPartitionIds(EventHubDescription eventHubDescription)
        {
            if (eventHubDescription == null)
            {
                yield break;
            }
            foreach (var partitionId in eventHubDescription.PartitionIds)
            {
                yield return new PartitionDescription(eventHubDescription.Path, partitionId);
            }
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendDrawing();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            this.ResumeDrawing();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            var changingUi = false;
            try
            {
                changingUi = true;
                panelMain.SuspendLayout();
                panelMain.SuspendDrawing();
                SetControlSize(null);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                if (changingUi)
                {
                    panelMain.ResumeDrawing();
                    panelMain.ResumeLayout();
                }
            }
        }

        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            var changingUi = false;
            try
            {
                changingUi = true;
                panelMain.SuspendLayout();
                panelMain.SuspendDrawing();
                SetControlSize(null);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                if (changingUi)
                {
                    panelMain.ResumeDrawing();
                    panelMain.ResumeLayout();
                }
            }
        }

        private void logWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem != null)
            {
                mainSplitContainer.Panel2Collapsed = !toolStripMenuItem.Checked;
                mainSplitContainer_SplitterMoved(this, null);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new AboutForm())
            {
                form.ShowDialog();
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connectForm = new ConnectForm(serviceBusHelper))
                {
                    if (connectForm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    selectedEntites = connectForm.SelectedEntities;
                    ServiceBusHelper.ConnectivityMode = connectForm.ConnectivityMode;
                    if (!string.IsNullOrWhiteSpace(connectForm.ConnectionString))
                    {
                        var serviceBusNamespace = GetServiceBusNamespace(connectForm.Key ?? "Manual", connectForm.ConnectionString);
                        serviceBusHelper.Connect(serviceBusNamespace);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(connectForm.Uri))
                        {
                            serviceBusHelper.Connect(connectForm.Uri,
                                                     connectForm.IssuerName,
                                                     connectForm.IssuerSecret,
                                                     connectForm.SharedAccessKeyName,
                                                     connectForm.SharedAccessKey,
                                                     connectForm.TransportType);
                        }
                        else
                        {
                            serviceBusHelper.Connect(connectForm.Namespace,
                                                     connectForm.ServicePath,
                                                     connectForm.IssuerName,
                                                     connectForm.IssuerSecret,
                                                     connectForm.SharedAccessKeyName,
                                                     connectForm.SharedAccessKey,
                                                     connectForm.TransportType);
                        }
                    }
                    // Set Relay Host Name
                    //var assembly = Assembly.GetAssembly(typeof(ServiceBus.ServiceBusEnvironment));
                    //var type = assembly.GetType("Microsoft.ServiceBus.RelayEnvironment");
                    //if (type != null)
                    //{
                    //    var property = type.GetProperty("RelayHostRootName",
                    //                                    BindingFlags.Static |
                    //                                    BindingFlags.Public);
                    //    if (property != null && serviceBusHelper.NamespaceUri != null)
                    //    {
                    //        property.SetValue(null, serviceBusHelper.GetHostWithoutNamespace());
                    //    }
                    //}
                    foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                    {
                        userControl.Dispose();
                    }
                    panelMain.Controls.Clear();
                    panelMain.BackColor = SystemColors.Window;
                    GetEntities(EntityType.All);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private void lstLog_Leave(object sender, EventArgs e)
        {
            lstLog.SelectedIndex = -1;
        }

        private void serviceBusTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (serviceBusTreeView.SelectedNode != e.Node)
            {
                serviceBusTreeView.SelectedNode = e.Node;
                serviceBusTreeView.SelectedNode.EnsureVisible();
                HandleNodeMouseClick(e.Node);
            }
        }

        private void refreshEntityMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            GetEntities(EntityType.All);
            Cursor.Current = Cursors.Default;
        }

        private void expandEntity_Click(object sender, EventArgs e)
        {
            if (serviceBusTreeView.SelectedNode != null)
            {
                serviceBusTreeView.SelectedNode.ExpandAll();
                serviceBusTreeView.SelectedNode.EnsureVisible();
            }
        }

        private void collapseEntity_Click(object sender, EventArgs e)
        {
            if (serviceBusTreeView.SelectedNode != null)
            {
                serviceBusTreeView.SelectedNode.Collapse(false);
                serviceBusTreeView.SelectedNode.EnsureVisible();
            }
        }

        private void exportEntity_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                var queueListNode = FindNode(QueueEntities, rootNode);
                var topicListNode = FindNode(TopicEntities, rootNode);
                var relayListNode = FindNode(RelayEntities, rootNode);
                var eventHubListNode = FindNode(EventHubEntities, rootNode);
                var notificationHubListNode = FindNode(NotificationHubEntities, rootNode);

                // Root
                if (serviceBusTreeView.SelectedNode == rootNode)
                {
                    var queueList = new List<IExtensibleDataObject>();
                    var topicList = new List<IExtensibleDataObject>();
                    var relayList = new List<IExtensibleDataObject>();
                    var eventHubList = new List<IExtensibleDataObject>();
                    var notificationHubList = new List<IExtensibleDataObject>();

                    GetQueueList(queueList, queueListNode);
                    GetTopicList(topicList, topicListNode);
                    GetRelayList(relayList, relayListNode);
                    GetEventHubList(eventHubList, eventHubListNode);
                    GetNotificationHubList(notificationHubList, notificationHubListNode);
                    queueList.AddRange(topicList);
                    queueList.AddRange(relayList);
                    queueList.AddRange(eventHubList);
                    queueList.AddRange(notificationHubList);
                    ExportEntities(queueList, AllEntities, null);
                    return;
                }
                // Queues
                if (serviceBusTreeView.SelectedNode == queueListNode)
                {
                    var queueList = new List<IExtensibleDataObject>();
                    GetQueueList(queueList, queueListNode);
                    ExportEntities(queueList, QueueEntities, null);
                    return;
                }
                // Topics
                if (serviceBusTreeView.SelectedNode == topicListNode)
                {
                    var topicList = new List<IExtensibleDataObject>();
                    GetTopicList(topicList, topicListNode);
                    ExportEntities(topicList, TopicEntities, null);
                    return;
                }
                // Relays
                if (serviceBusTreeView.SelectedNode == relayListNode)
                {
                    var relayList = new List<IExtensibleDataObject>();
                    GetRelayList(relayList, relayListNode);
                    ExportEntities(relayList, RelayEntities, null);
                    return;
                }
                // EventHubs
                if (serviceBusTreeView.SelectedNode == eventHubListNode)
                {
                    var eventHubList = new List<IExtensibleDataObject>();
                    GetEventHubList(eventHubList, eventHubListNode);
                    ExportEntities(eventHubList, EventHubEntities, null);
                    return;
                }
                // NotificationHubs
                if (serviceBusTreeView.SelectedNode == notificationHubListNode)
                {
                    var notificationHubList = new List<IExtensibleDataObject>();
                    GetNotificationHubList(notificationHubList, notificationHubListNode);
                    ExportEntities(notificationHubList, NotificationHubEntities, null);
                    return;
                }
                // Check that serviceBusTreeView.SelectedNode.Tag is not null
                if (serviceBusTreeView.SelectedNode.Tag == null)
                {
                    return;
                }
                // Url Segment Node
                if (serviceBusTreeView.SelectedNode.Tag is UrlSegmentWrapper)
                {
                    var urlSegmentWrapper = serviceBusTreeView.SelectedNode.Tag as UrlSegmentWrapper;
                    if (urlSegmentWrapper.EntityType == EntityType.Queue)
                    {
                        var queueList = new List<IExtensibleDataObject>();
                        GetQueueList(queueList, serviceBusTreeView.SelectedNode);
                        ExportEntities(queueList,
                                       FormatAbsolutePathForExport(urlSegmentWrapper.Uri),
                                       QueueEntities);
                    }
                    else if (urlSegmentWrapper.EntityType == EntityType.Topic)
                    {
                        var topicList = new List<IExtensibleDataObject>();
                        GetTopicList(topicList, serviceBusTreeView.SelectedNode);
                        ExportEntities(topicList,
                                       FormatAbsolutePathForExport(urlSegmentWrapper.Uri),
                                       TopicEntities);
                    }
                    else
                    {
                        var relayList = new List<IExtensibleDataObject>();
                        GetRelayList(relayList, serviceBusTreeView.SelectedNode);
                        ExportEntities(relayList,
                                       FormatAbsolutePathForExport(urlSegmentWrapper.Uri),
                                       RelayEntities);
                    }
                    return;
                }
                // Queue Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                {
                    var queueDescription = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                    ExportEntities(new List<IExtensibleDataObject> { queueDescription },
                                   queueDescription.Path,
                                   QueueEntity);
                    return;
                }
                // Topic Node
                if (serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                {
                    var topicDescription = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                    ExportEntities(new List<IExtensibleDataObject> { topicDescription },
                                   topicDescription.Path,
                                   TopicEntity);
                }
                // Relay Node
                if (serviceBusTreeView.SelectedNode.Tag is RelayDescription)
                {
                    var relayDescription = serviceBusTreeView.SelectedNode.Tag as RelayDescription;
                    ExportEntities(new List<IExtensibleDataObject> { relayDescription },
                                   relayDescription.Path,
                                   RelayEntity);
                    return;
                }
                // EventHub Node
                if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription)
                {
                    var eventHubDescription = serviceBusTreeView.SelectedNode.Tag as EventHubDescription;
                    ExportEntities(new List<IExtensibleDataObject> { eventHubDescription },
                                   eventHubDescription.Path,
                                   EventHubEntity);
                }
                // NotificationHub Node
                if (serviceBusTreeView.SelectedNode.Tag is NotificationHubDescription)
                {
                    var notificationHubDescription = serviceBusTreeView.SelectedNode.Tag as NotificationHubDescription;
                    ExportEntities(new List<IExtensibleDataObject> { notificationHubDescription },
                                   notificationHubDescription.Path,
                                   NotificationHubEntity);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void importEntity_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                var xml = LoadEntityFromFile(out string fileName);
                if (xml == null)
                {
                    return;
                }
                importing = true;
                serviceBusHelper.ImportEntities(xml);
                WriteToLog(string.Format(EntitiesImported, fileName));
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                importing = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void renameEntity_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                // Queue Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                {
                    var queueDescription = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                    using (var parameterForm = new ParameterForm($"Enter a new name for the {queueDescription.Path} queue.",
                            new List<string> { "Name" },
                            new List<string> { queueDescription.Path},
                            new List<bool> { false }))
                    {
                        if (parameterForm.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        serviceBusHelper.RenameQueue(queueDescription.Path, parameterForm.ParameterValues[0]);
                        return;
                    }
                }

                // Topic Node
                if (serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                {
                    var topicDescription = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                    using (var parameterForm = new ParameterForm($"Enter a new name for the {topicDescription.Path} topic.",
                            new List<string> { "Name" },
                            new List<string> { topicDescription.Path },
                            new List<bool> { false }))
                    {
                        if (parameterForm.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        serviceBusHelper.RenameTopic(topicDescription.Path, parameterForm.ParameterValues[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // ReSharper disable once FunctionComplexityOverflow
        public void refreshEntity_Click(object sender, EventArgs e)
        {
            try
            {
                serviceBusTreeView.SuspendDrawing();
                serviceBusTreeView.SuspendLayout();
                serviceBusTreeView.BeginUpdate();
                var queueListNode = FindNode(QueueEntities, rootNode);
                var topicListNode = FindNode(TopicEntities, rootNode);
                var eventHubListNode = FindNode(EventHubEntities, rootNode);
                var notificationHubListNode = FindNode(NotificationHubEntities, rootNode);
                var relayServiceListNode = FindNode(RelayEntities, rootNode);
                if (serviceBusTreeView.SelectedNode != null)
                {
                    // Queues
                    if (serviceBusTreeView.SelectedNode == queueListNode)
                    {
                        GetEntities(EntityType.Queue);
                        return;
                    }
                    // Topics
                    if (serviceBusTreeView.SelectedNode == topicListNode)
                    {
                        GetEntities(EntityType.Topic);
                        return;
                    }
                    // Event Hubs
                    if (serviceBusTreeView.SelectedNode == eventHubListNode)
                    {
                        GetEntities(EntityType.EventHub);
                        return;
                    }
                    // Notification Hubs
                    if (serviceBusTreeView.SelectedNode == notificationHubListNode)
                    {
                        GetEntities(EntityType.NotificationHub);
                        return;
                    }
                    // Relays
                    if (serviceBusTreeView.SelectedNode == relayServiceListNode)
                    {
                        GetEntities(EntityType.Relay);
                        return;
                    }
                    if (serviceBusTreeView.SelectedNode.Tag == null)
                    {
                        return;
                    }
                    // Queue Node
                    var tag = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                    if (tag != null)
                    {
                        var queueDescription = serviceBusHelper.GetQueue(tag.Path);
                        if (queueDescription.Status == EntityStatus.Active)
                        {
                            serviceBusTreeView.SelectedNode.ImageIndex = QueueIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = QueueIconIndex;
                        }
                        else
                        {
                            serviceBusTreeView.SelectedNode.ImageIndex = GreyQueueIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = GreyQueueIconIndex;
                        }
                        serviceBusTreeView.SelectedNode.Tag = queueDescription;
                        var control = panelMain.Controls[0] as HandleQueueControl;
                        if (control != null)
                        {
                            control.RefreshData(queueDescription);
                            WriteToLog(string.Format(QueueRetrievedFormat, queueDescription.Path), false);
                        }
                        serviceBusTreeView.SelectedNode.Text = showMessageCount ?
                                                               string.Format(NameMessageCountFormat,
                                                                             serviceBusTreeView.SelectedNode.Name,
                                                                             queueDescription.MessageCountDetails.ActiveMessageCount,
                                                                             queueDescription.MessageCountDetails.DeadLetterMessageCount,
                                                                             queueDescription.MessageCountDetails.TransferDeadLetterMessageCount) :
                                                               serviceBusTreeView.SelectedNode.Name;
                        return;
                    }
                    // Individual Topic Node
                    var selectedNodeTag = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                    if (selectedNodeTag != null)
                    {
                        var topicDescription = serviceBusHelper.GetTopic(selectedNodeTag.Path);
                        if (topicDescription.Status == EntityStatus.Active)
                        {
                            serviceBusTreeView.SelectedNode.ImageIndex = TopicIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = TopicIconIndex;
                        }
                        else
                        {
                            serviceBusTreeView.SelectedNode.ImageIndex = GreyTopicIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = GreyTopicIconIndex;
                        }
                        serviceBusTreeView.SelectedNode.Tag = topicDescription;
                        var control = panelMain.Controls[0] as HandleTopicControl;
                        if (control != null)
                        {
                            control.RefreshData(topicDescription);
                            WriteToLog(string.Format(TopicRetrievedFormat, topicDescription.Path), false);
                        }

                        RefreshIndividualTopic(serviceBusTreeView.SelectedNode);
                        return;
                    }
                    // Relay Node
                    var nodeTag = serviceBusTreeView.SelectedNode.Tag as RelayDescription;
                    if (nodeTag != null)
                    {
                        var description = nodeTag;
                        if (description.IsDynamic)
                        {
                            var relayCollection = serviceBusHelper.GetRelays();
                            var relayDescriptions = relayCollection as IList<RelayDescription> ?? relayCollection.ToList();
                            if (relayDescriptions.Any())
                            {
                                var relayDescription = relayDescriptions.First(r => string.Compare(r.Path,
                                                                                                   description.Path,
                                                                                                   StringComparison.InvariantCultureIgnoreCase) == 0);
                                if (relayDescription != null)
                                {
                                    var control = panelMain.Controls[0] as HandleRelayControl;
                                    if (control != null)
                                    {
                                        control.RefreshData(relayDescription);
                                        WriteToLog(string.Format(RelayRetrievedFormat, relayDescription.Path), false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var relayDescription = serviceBusHelper.GetRelay(description.Path);
                            serviceBusTreeView.SelectedNode.ImageIndex = RelayLeafIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = RelayLeafIconIndex;
                            serviceBusTreeView.SelectedNode.Tag = relayDescription;
                            var control = panelMain.Controls[0] as HandleRelayControl;
                            if (control != null)
                            {
                                control.RefreshData(relayDescription);
                                WriteToLog(string.Format(RelayRetrievedFormat, relayDescription.Path), false);
                            }
                        }
                        return;
                    }
                    // Partitions Node
                    if (serviceBusTreeView.SelectedNode.Text == PartitionEntities)
                    {
                        var isExpanded = serviceBusTreeView.SelectedNode.IsExpanded;
                        var eventHubDescription = serviceBusTreeView.SelectedNode.Tag as EventHubDescription;
                        if (eventHubDescription == null)
                        {
                            return;
                        }
                        serviceBusTreeView.SelectedNode.Nodes.Clear();
                        var parent = serviceBusTreeView.SelectedNode.Parent;
                        CreateEventHubPartitions(eventHubDescription, parent);
                        var partitionsNode = parent.Nodes.Cast<TreeNode>().First(n => n.Name == PartitionEntities);
                        if (partitionsNode == null)
                        {
                            return;
                        }
                        serviceBusTreeView.SelectedNode = partitionsNode;
                        if (isExpanded)
                        {
                            partitionsNode.Expand();
                        }
                        return;
                    }
                    // Consumer Groups Node
                    if (serviceBusTreeView.SelectedNode.Text == ConsumerGroupEntities)
                    {
                        var isExpanded = serviceBusTreeView.SelectedNode.IsExpanded;
                        var eventHubDescription = serviceBusTreeView.SelectedNode.Tag as EventHubDescription;
                        if (eventHubDescription == null)
                        {
                            return;
                        }
                        serviceBusTreeView.SelectedNode.Nodes.Clear();
                        var parent = serviceBusTreeView.SelectedNode.Parent;
                        var partitions = GetPartitionsFromPartitionIds(eventHubDescription);
                        var partitionDescriptions = partitions as IList<PartitionDescription> ?? partitions.ToList();
                        CreateEventHubConsumerGroups(eventHubDescription, parent, partitionDescriptions);
                        var consumerGroupsNode = parent.Nodes.Cast<TreeNode>().First(n => n.Name == ConsumerGroupEntities);
                        if (consumerGroupsNode == null)
                        {
                            return;
                        }
                        serviceBusTreeView.SelectedNode = consumerGroupsNode;
                        if (isExpanded)
                        {
                            consumerGroupsNode.Expand();
                        }
                        return;
                    }
                    // Event Hub Node
                    if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription)
                    {
                        var eventHubDescription = serviceBusHelper.GetEventHub(((EventHubDescription)serviceBusTreeView.SelectedNode.Tag).Path);
                        if (eventHubDescription.Status == EntityStatus.Active)
                        {
                            serviceBusTreeView.SelectedNode.ImageIndex = EventHubIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = EventHubIconIndex;
                        }
                        else
                        {
                            serviceBusTreeView.SelectedNode.ImageIndex = GreyEventHubIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = GreyEventHubIconIndex;
                        }
                        serviceBusTreeView.SelectedNode.Tag = eventHubDescription;
                        var control = panelMain.Controls[0] as HandleEventHubControl;
                        if (control != null)
                        {
                            control.RefreshData(eventHubDescription);
                            WriteToLog(string.Format(EventHubRetrievedFormat, eventHubDescription.Path), false);
                        }
                        return;
                    }
                    // Partition Node
                    if (serviceBusTreeView.SelectedNode.Tag is PartitionDescription)
                    {
                        var partitionDescription = (PartitionDescription)serviceBusTreeView.SelectedNode.Tag;
                        var consumerGroup = serviceBusTreeView.SelectedNode.Parent.Parent.Tag as ConsumerGroupDescription;
                        var consumerGroupName = consumerGroup != null ? consumerGroup.Name : null;
                        var partition = serviceBusHelper.GetPartition(partitionDescription.EventHubPath, consumerGroupName, partitionDescription.PartitionId);
                        if (partition != null)
                        {
                            serviceBusTreeView.SelectedNode.Tag = partitionDescription;
                            var control = panelMain.Controls[0] as HandlePartitionControl;
                            if (control != null)
                            {
                                control.RefreshData(partition);
                                WriteToLog(string.Format(PartitionRetrievedFormat, partitionDescription.PartitionId, partitionDescription.EventHubPath), false);
                            }
                        }
                        return;
                    }
                    // Consumer Group Node
                    if (serviceBusTreeView.SelectedNode.Tag is ConsumerGroupDescription)
                    {
                        var consumerGroupDescription = serviceBusTreeView.SelectedNode.Tag as ConsumerGroupDescription;
                        consumerGroupDescription = serviceBusHelper.GetConsumerGroup(consumerGroupDescription.EventHubPath, consumerGroupDescription.Name);
                        serviceBusTreeView.SelectedNode.Tag = consumerGroupDescription;
                        var control = panelMain.Controls[0] as HandleConsumerGroupControl;
                        if (control != null)
                        {
                            control.RefreshData(consumerGroupDescription);
                            WriteToLog(string.Format(ConsumerGroupRetrievedFormat, consumerGroupDescription.Name, consumerGroupDescription.EventHubPath), false);
                        }
                        return;
                    }
                    // Notification Hub Node
                    if (serviceBusTreeView.SelectedNode.Tag is NotificationHubDescription)
                    {
                        var notificationHubDescription = serviceBusHelper.GetNotificationHub(((NotificationHubDescription)serviceBusTreeView.SelectedNode.Tag).Path);
                        serviceBusTreeView.SelectedNode.ImageIndex = NotificationHubIconIndex;
                        serviceBusTreeView.SelectedNode.SelectedImageIndex = NotificationHubIconIndex;
                        serviceBusTreeView.SelectedNode.Tag = notificationHubDescription;
                        var control = panelMain.Controls[0] as HandleNotificationHubControl;
                        if (control != null)
                        {
                            control.RefreshData(notificationHubDescription);
                            WriteToLog(string.Format(NotificationHubRetrievedFormat, notificationHubDescription.Path), false);
                        }
                        return;
                    }
                    // Subscriptions Node
                    if (serviceBusTreeView.SelectedNode.Text == SubscriptionEntities ||
                        serviceBusTreeView.SelectedNode.Text == FilteredSubscriptionEntities)
                    {
                        var wrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                        if (wrapper == null)
                        {
                            return;
                        }
                        var subscriptions = serviceBusHelper.GetSubscriptions(wrapper.TopicDescription, wrapper.Filter);
                        var subscriptionDescriptions = subscriptions as IList<SubscriptionDescription> ?? subscriptions.ToList();
                        if ((subscriptionDescriptions.Any()) ||
                            !string.IsNullOrWhiteSpace(wrapper.Filter))
                        {
                            var subscriptionsNode = serviceBusTreeView.SelectedNode;
                            subscriptionsNode.Text = string.IsNullOrWhiteSpace(wrapper.Filter) ?
                                                                 SubscriptionEntities :
                                                                 FilteredSubscriptionEntities;
                            subscriptionsNode.Nodes.Clear();
                            foreach (var subscription in subscriptionDescriptions)
                            {
                                var subscriptionNode = subscriptionsNode.Nodes.Add(subscription.Name,
                                                                                   showMessageCount ?
                                                                                   string.Format(NameMessageCountFormat,
                                                                                                 subscription.Name,
                                                                                                 subscription.MessageCountDetails.ActiveMessageCount,
                                                                                                 subscription.MessageCountDetails.DeadLetterMessageCount,
                                                                                                 subscription.MessageCountDetails.TransferDeadLetterMessageCount) :
                                                                                   subscription.Name,
                                                                                   subscription.Status == EntityStatus.Active ? SubscriptionIconIndex : GreySubscriptionIconIndex,
                                                                                   subscription.Status == EntityStatus.Active ? SubscriptionIconIndex : GreySubscriptionIconIndex);
                                subscriptionNode.ContextMenuStrip = subscriptionContextMenuStrip;
                                subscriptionNode.Tag = new SubscriptionWrapper(subscription, wrapper.TopicDescription);
                                WriteToLog(string.Format(CultureInfo.CurrentCulture, SubscriptionRetrievedFormat, subscription.Name, wrapper.TopicDescription.Path), false);
                                var rules = serviceBusHelper.GetRules(subscription);
                                var ruleDescriptions = rules as RuleDescription[] ?? rules.ToArray();
                                if (ruleDescriptions.Any())
                                {
                                    subscriptionNode.Nodes.Clear();
                                    var rulesNode = subscriptionNode.Nodes.Add(RuleEntities, RuleEntities, RuleListIconIndex, RuleListIconIndex);
                                    rulesNode.ContextMenuStrip = rulesContextMenuStrip;
                                    rulesNode.Tag = new RuleWrapper(null, subscription);
                                    foreach (var rule in ruleDescriptions)
                                    {
                                        var ruleNode = rulesNode.Nodes.Add(rule.Name, rule.Name, RuleIconIndex, RuleIconIndex);
                                        ruleNode.ContextMenuStrip = ruleContextMenuStrip;
                                        ruleNode.Tag = new RuleWrapper(rule, subscription);
                                        WriteToLog(string.Format(CultureInfo.CurrentCulture, RuleRetrievedFormat, rule.Name, subscription.Name, wrapper.TopicDescription.Path), false);
                                    }
                                }
                            }
                        }
                        return;
                    }
                    // Rules Node
                    if (serviceBusTreeView.SelectedNode.Text == RuleEntities)
                    {
                        var wrapper = serviceBusTreeView.SelectedNode.Tag as RuleWrapper;
                        if (wrapper == null)
                        {
                            return;
                        }
                        var rules = serviceBusHelper.GetRules(wrapper.SubscriptionDescription);
                        var ruleDescriptions = rules as RuleDescription[] ?? rules.ToArray();
                        if (ruleDescriptions.Any())
                        {
                            var rulesNode = serviceBusTreeView.SelectedNode;
                            rulesNode.Nodes.Clear();
                            foreach (var rule in ruleDescriptions)
                            {
                                var ruleNode = rulesNode.Nodes.Add(rule.Name, rule.Name, RuleIconIndex, RuleIconIndex);
                                ruleNode.ContextMenuStrip = ruleContextMenuStrip;
                                ruleNode.Tag = new RuleWrapper(rule, wrapper.SubscriptionDescription);
                                WriteToLog(string.Format(CultureInfo.CurrentCulture, RuleRetrievedFormat, rule.Name, wrapper.SubscriptionDescription.Name, wrapper.SubscriptionDescription.TopicPath), false);
                            }
                        }
                        return;
                    }
                    // Individual Subscription Node
                    var subscriptionWrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                    if (subscriptionWrapper != null)
                    {
                        var wrapper = subscriptionWrapper;
                        var subscriptionDescription = serviceBusHelper.GetSubscription(wrapper.SubscriptionDescription.TopicPath,wrapper.SubscriptionDescription.Name);
                        wrapper = new SubscriptionWrapper(subscriptionDescription, wrapper.TopicDescription);
                        if (subscriptionDescription.Status == EntityStatus.Active)
                        {
                            serviceBusTreeView.SelectedNode.ImageIndex = SubscriptionIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = SubscriptionIconIndex;
                        }
                        else
                        {
                            serviceBusTreeView.SelectedNode.ImageIndex = GreySubscriptionIconIndex;
                            serviceBusTreeView.SelectedNode.SelectedImageIndex = GreySubscriptionIconIndex;
                        }
                        serviceBusTreeView.SelectedNode.Tag = wrapper;
                        var control = panelMain.Controls[0] as HandleSubscriptionControl;
                        if (control != null)
                        {
                            control.RefreshData(wrapper);
                            WriteToLog(string.Format(SubscriptionRetrievedFormat,
                                                     wrapper.SubscriptionDescription.Name,
                                                     wrapper.SubscriptionDescription.TopicPath),
                                       false);
                        }
                        serviceBusTreeView.SelectedNode.Text = showMessageCount ?
                                                               string.Format(NameMessageCountFormat,
                                                                             serviceBusTreeView.SelectedNode.Name,
                                                                             subscriptionDescription.MessageCountDetails.ActiveMessageCount,
                                                                             subscriptionDescription.MessageCountDetails.DeadLetterMessageCount,
                                                                             subscriptionDescription.MessageCountDetails.TransferDeadLetterMessageCount) :
                                                               serviceBusTreeView.SelectedNode.Name;

                        RefreshIndividualSubscription(subscriptionDescription, serviceBusTreeView.SelectedNode);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                serviceBusTreeView.ResumeDrawing();
                serviceBusTreeView.ResumeLayout();
                serviceBusTreeView.EndUpdate();
            }
        }

        private void RefreshIndividualSubscription(SubscriptionDescription subscriptionDescription, TreeNode subscriptionNode)
        {
            var rules = serviceBusHelper.GetRules(subscriptionDescription);
            var ruleDescriptions = rules as RuleDescription[] ?? rules.ToArray();
            if (!ruleDescriptions.Any())
                return;
            var subscriptionNodeWasExpanded = subscriptionNode.IsExpanded;
            var rulesNodeWasExpanded = subscriptionNode.Nodes.Count > 0 && subscriptionNode.Nodes[0].IsExpanded;
            subscriptionNode.Nodes.Clear();
            var rulesNode = subscriptionNode.Nodes.Add(RuleEntities, RuleEntities, RuleListIconIndex, RuleListIconIndex);
            rulesNode.ContextMenuStrip = rulesContextMenuStrip;
            rulesNode.Tag = new RuleWrapper(null, subscriptionDescription);
            foreach (var rule in ruleDescriptions)
            {
                var ruleNode = rulesNode.Nodes.Add(rule.Name, rule.Name, RuleIconIndex, RuleIconIndex);
                ruleNode.ContextMenuStrip = ruleContextMenuStrip;
                ruleNode.Tag = new RuleWrapper(rule, subscriptionDescription);
                WriteToLog(string.Format(CultureInfo.CurrentCulture, RuleRetrievedFormat, rule.Name, subscriptionDescription.Name, subscriptionDescription.TopicPath), false);
            }
            if (rulesNodeWasExpanded)
                rulesNode.Expand();
            if (subscriptionNodeWasExpanded)
                subscriptionNode.Expand();
        }

        private void RefreshIndividualTopic(TreeNode selectedNode)
        {
            var wasTopicNodeExpanded = selectedNode.IsExpanded;

            var topicDescription = selectedNode.Tag as TopicDescription;

            var subscriptions = serviceBusHelper.GetSubscriptions(topicDescription, null);
            var subscriptionDescriptions = subscriptions as IList<SubscriptionDescription> ?? subscriptions.ToList();

            if (!subscriptionDescriptions.Any())
            {
                selectedNode.Nodes.Clear();
                return;
            }

            selectedNode.Nodes.Clear();
            var subscriptionsNode = selectedNode.Nodes.Add(SubscriptionEntities, SubscriptionEntities, SubscriptionListIconIndex, SubscriptionListIconIndex);
            subscriptionsNode.Text = string.IsNullOrWhiteSpace(FilterExpressionHelper.SubscriptionFilterExpression) ? SubscriptionEntities : FilteredSubscriptionEntities;
            subscriptionsNode.ContextMenuStrip = subscriptionsContextMenuStrip;
            subscriptionsNode.Tag = new SubscriptionWrapper(null, topicDescription, FilterExpressionHelper.SubscriptionFilterExpression);
            foreach (var subscriptionDescription in subscriptionDescriptions)
            {
                var subscriptionNode = subscriptionsNode.Nodes.Add(subscriptionDescription.Name, showMessageCount
                        ? string.Format(NameMessageCountFormat, 
                                        subscriptionDescription.Name, 
                                        subscriptionDescription.MessageCountDetails.ActiveMessageCount, 
                                        subscriptionDescription.MessageCountDetails.DeadLetterMessageCount,
                                        subscriptionDescription.MessageCountDetails.TransferDeadLetterMessageCount)
                        : subscriptionDescription.Name, subscriptionDescription.Status == EntityStatus.Active ? SubscriptionIconIndex : GreySubscriptionIconIndex, subscriptionDescription.Status == EntityStatus.Active
                        ? SubscriptionIconIndex : GreySubscriptionIconIndex);
                subscriptionNode.ContextMenuStrip = subscriptionContextMenuStrip;
                subscriptionNode.Tag = new SubscriptionWrapper(subscriptionDescription, topicDescription);
                if (topicDescription != null)
                {
                    WriteToLog(string.Format(CultureInfo.CurrentCulture, SubscriptionRetrievedFormat, subscriptionDescription.Name, topicDescription.Path),false);
                }

                RefreshIndividualSubscription(subscriptionDescription, subscriptionNode);
            }

            if (wasTopicNodeExpanded)
                selectedNode.Expand();
        }

        private void createEntity_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode != null)
                {
                    // Queues Node (Create New QueueDescription)
                    if (serviceBusTreeView.SelectedNode.Text == QueueEntities)
                    {
                        panelMain.HeaderText = CreateQueue;
                        ShowQueue(null, null);
                        return;
                    }
                    // Topics Node (Create New TopicDescription)
                    if (serviceBusTreeView.SelectedNode.Text == TopicEntities)
                    {
                        panelMain.HeaderText = CreateTopic;
                        ShowTopic(null, null);
                        return;
                    }
                    // Relays Node (Create New RelayDescription)
                    if (serviceBusTreeView.SelectedNode.Text == RelayEntities)
                    {
                        panelMain.HeaderText = CreateRelay;
                        ShowRelay(null, null);
                        return;
                    }
                    // EventHubs Node (Create New EventHubDescription)
                    if (serviceBusTreeView.SelectedNode.Text == EventHubEntities)
                    {
                        panelMain.HeaderText = CreateEventHub;
                        ShowEventHub(null);
                        return;
                    }
                    // ConsumerGroup Node (Create New ConsumerGroupDescription)
                    if (serviceBusTreeView.SelectedNode.Text == ConsumerGroupEntities)
                    {
                        var parent = serviceBusTreeView.SelectedNode.Parent;
                        if (parent?.Tag is EventHubDescription)
                        {
                            panelMain.HeaderText = CreateConsumerGroup;
                            ShowConsumerGroup(null, ((EventHubDescription)parent.Tag).Path);
                        }
                        return;
                    }
                    // NotificationHubs Node (Create New NotificationHubDescription)
                    if (serviceBusTreeView.SelectedNode.Text == NotificationHubEntities)
                    {
                        panelMain.HeaderText = CreateNotificationHub;
                        ShowNotificationHub(null);
                        return;
                    }
                    if (serviceBusTreeView.SelectedNode.Tag != null)
                    {
                        // UrlSegment Node
                        if (serviceBusTreeView.SelectedNode.Tag is UrlSegmentWrapper)
                        {
                            var urlSegmentWrapper = serviceBusTreeView.SelectedNode.Tag as UrlSegmentWrapper;
                            if (urlSegmentWrapper.EntityType == EntityType.Queue)
                            {
                                panelMain.HeaderText = CreateQueue;
                                ShowQueue(null, FormatAbsolutePathForEdit(urlSegmentWrapper.Uri));
                            }
                            else if (urlSegmentWrapper.EntityType == EntityType.Topic)
                            {
                                panelMain.HeaderText = CreateTopic;
                                ShowTopic(null, FormatAbsolutePathForEdit(urlSegmentWrapper.Uri));
                            }
                            else
                            {
                                panelMain.HeaderText = CreateRelay;
                                ShowRelay(null, FormatAbsolutePathForEdit(urlSegmentWrapper.Uri));
                            }
                            return;
                        }

                        // TopicDescription Node (Create New SubscriptionDescription)
                        if (serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                        {
                            panelMain.HeaderText = CreateSubscription;
                            ShowSubscription(new SubscriptionWrapper(null, serviceBusTreeView.SelectedNode.Tag as TopicDescription));
                            return;
                        }

                        // EventHub Node (Create New ConsumerGroupDescription)
                        if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription)
                        {
                            panelMain.HeaderText = CreateConsumerGroup;
                            ShowConsumerGroup(null, (serviceBusTreeView.SelectedNode.Tag as EventHubDescription).Path);
                            return;
                        }
                        // Subscriptions Node (Create New SubscriptionDescription)
                        if (serviceBusTreeView.SelectedNode.Text == SubscriptionEntities ||
                            serviceBusTreeView.SelectedNode.Text == FilteredSubscriptionEntities)
                        {
                            panelMain.HeaderText = CreateSubscription;
                            var subscriptionWrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                            if (subscriptionWrapper != null)
                            {
                                ShowSubscription(new SubscriptionWrapper(null, subscriptionWrapper.TopicDescription));
                            }
                            return;
                        }
                        // SubscriptionDescription Node (Create New RuleDescription)
                        if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                        {
                            var wrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                            panelMain.HeaderText = AddRule;
                            ShowRule(new RuleWrapper(null, wrapper.SubscriptionDescription), !serviceBusTreeView.SelectedNode.Nodes.ContainsKey(RuleEntities));
                            return;
                        }
                        // Rules Node (Create New RuleDescription)
                        if (serviceBusTreeView.SelectedNode.Text == RuleEntities)
                        {
                            panelMain.HeaderText = AddRule;
                            var ruleWrapper = serviceBusTreeView.SelectedNode.Tag as RuleWrapper;
                            if (ruleWrapper != null)
                            {
                                ShowRule(new RuleWrapper(null, ruleWrapper.SubscriptionDescription), false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void serviceBusTreeView_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.KeyCode)
            {
                case Keys.Delete: // purge entity
                    if (keyEventArgs.Modifiers == Keys.Shift)
                    {
                        receiveMessages_Click(sender, keyEventArgs);
                    }
                    else // delete entity
                    {
                        deleteEntity_Click(sender, keyEventArgs);
                    }
                    break;

                case Keys.Enter: // select entity
                    HandleNodeMouseClick(serviceBusTreeView.SelectedNode);
                    break;
            }
        }

        private async void deleteEntity_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (serviceBusTreeView.SelectedNode != null)
                {
                    var queueListNode = FindNode(QueueEntities, rootNode);
                    var topicListNode = FindNode(TopicEntities, rootNode);
                    var relayServiceListNode = FindNode(RelayEntities, rootNode);
                    var eventHubListNode = FindNode(EventHubEntities, rootNode);
                    var notificationHubListNode = FindNode(NotificationHubEntities, rootNode);

                    // Root Node
                    if (serviceBusTreeView.SelectedNode == rootNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllEntities))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var queueList = new List<string>();
                                var topicList = new List<string>();
                                GetQueueList(queueList, queueListNode);
                                GetTopicList(topicList, topicListNode);
                                await serviceBusHelper.DeleteQueues(queueList);
                                await serviceBusHelper.DeleteTopics(topicList);
                                GetEntities(EntityType.All);
                            }
                        }
                        return;
                    }
                    // Queues Node
                    if (serviceBusTreeView.SelectedNode == queueListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllQueues))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var queueList = new List<string>();
                                GetQueueList(queueList, queueListNode);
                                await serviceBusHelper.DeleteQueues(queueList);
                                GetEntities(EntityType.Queue);
                            }
                        }
                        return;
                    }
                    // Topics Node
                    if (serviceBusTreeView.SelectedNode == topicListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllTopics))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var topicList = new List<string>();
                                GetTopicList(topicList, topicListNode);
                                await serviceBusHelper.DeleteTopics(topicList);
                                GetEntities(EntityType.Topic);
                            }
                        }
                        return;
                    }
                    // Relays Node
                    if (serviceBusTreeView.SelectedNode == relayServiceListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllRelays))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var relayServiceList = new List<string>();
                                GetRelayList(relayServiceList, relayServiceListNode);
                                await serviceBusHelper.DeleteRelays(relayServiceList);
                                GetEntities(EntityType.Relay);
                            }
                        }
                        return;
                    }
                    // EventHubs Node
                    if (serviceBusTreeView.SelectedNode == eventHubListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllEventHubs))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var eventHubList = new List<string>();
                                GetEventHubList(eventHubList, eventHubListNode);
                                serviceBusHelper.DeleteEventHubs(eventHubList);
                                GetEntities(EntityType.EventHub);
                            }
                        }
                        return;
                    }
                    // ConsumerGroups Node
                    if (serviceBusTreeView.SelectedNode.Text == ConsumerGroupEntities)
                    {
                        var parent = serviceBusTreeView.SelectedNode.Parent;
                        if (parent != null && parent.Tag is EventHubDescription)
                        {
                            var eventHubDescription = parent.Tag as EventHubDescription;
                            using (var deleteForm = new DeleteForm(string.Format(DeleteAllConsumerGroups, eventHubDescription.Path)))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    var notificationHubList = new List<string>();
                                    GetConsumerGroupList(notificationHubList, serviceBusTreeView.SelectedNode);
                                    notificationHubList.Remove(DefaultConsumerGroupName);
                                    serviceBusHelper.DeleteConsumerGroups(eventHubDescription.Path, notificationHubList);
                                    refreshEntity_Click(null, null);
                                }
                            }
                        }
                        return;
                    }
                    // NotificationHubs Node
                    if (serviceBusTreeView.SelectedNode == notificationHubListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllNotificationHubs))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var notificationHubList = new List<string>();
                                GetNotificationHubList(notificationHubList, notificationHubListNode);
                                await serviceBusHelper.DeleteNotificationHubs(notificationHubList);
                                GetEntities(EntityType.NotificationHub);
                            }
                        }
                        return;
                    }
                    // Check that serviceBusTreeView.SelectedNode.Tag is not null
                    if (serviceBusTreeView.SelectedNode.Tag == null)
                    {
                        return;
                    }
                    // Url Segment Node
                    if (serviceBusTreeView.SelectedNode.Tag is UrlSegmentWrapper)
                    {
                        var urlSegmentWrapper = serviceBusTreeView.SelectedNode.Tag as UrlSegmentWrapper;
                        if (urlSegmentWrapper.EntityType == EntityType.Queue)
                        {
                            using (var deleteForm = new DeleteForm(string.Format(DeleteAllQueuesInPath, FormatAbsolutePathForEdit(urlSegmentWrapper.Uri))))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    var queueList = new List<string>();
                                    GetQueueList(queueList, serviceBusTreeView.SelectedNode);
                                    await serviceBusHelper.DeleteQueues(queueList);
                                }
                            }
                        }
                        else if (urlSegmentWrapper.EntityType == EntityType.Topic)
                        {
                            using (var deleteForm = new DeleteForm(string.Format(DeleteAllTopicsInPath, FormatAbsolutePathForEdit(urlSegmentWrapper.Uri))))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    var topicList = new List<string>();
                                    GetTopicList(topicList, serviceBusTreeView.SelectedNode);
                                    await serviceBusHelper.DeleteTopics(topicList);
                                }
                            }
                        }
                        else
                        {
                            using (var deleteForm = new DeleteForm(string.Format(DeleteAllRelaysInPath, FormatAbsolutePathForEdit(urlSegmentWrapper.Uri))))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    var relayServiceList = new List<string>();
                                    GetRelayList(relayServiceList, serviceBusTreeView.SelectedNode);
                                    await serviceBusHelper.DeleteRelays(relayServiceList);
                                }
                            }
                        }
                        return;
                    }
                    // Queue Node
                    if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                    {
                        var queueDescription = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                        using (var deleteForm = new DeleteForm(queueDescription.Path, QueueEntity.ToLower()))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                await serviceBusHelper.DeleteQueue(queueDescription);
                            }
                        }
                        return;
                    }
                    // Subscriptions Node
                    if (sender == deleteTopicSubscriptionsMenuItem)
                    {
                        if (serviceBusTreeView.SelectedNode.Nodes.Count == 0)
                        {
                            return;
                        }
                        var subscriptionsNode = serviceBusTreeView.SelectedNode.Nodes[0];
                        var subscriptionDescriptions = subscriptionsNode.
                                                            Nodes.
                                                            Cast<TreeNode>().
                                                            Select(n => ((SubscriptionWrapper)n.Tag).SubscriptionDescription).
                                                            ToList();
                        if (subscriptionDescriptions.Count > 0)
                        {
                            using (var deleteForm = new DeleteForm(DeleteAllSubscriptions))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    await serviceBusHelper.DeleteSubscriptions(subscriptionDescriptions);
                                }
                            }
                        }
                        return;
                    }
                    // Topic Node
                    if (serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                    {
                        var topicDescription = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                        using (var deleteForm = new DeleteForm(topicDescription.Path, TopicEntity.ToLower()))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                await serviceBusHelper.DeleteTopic(topicDescription);
                            }
                        }
                        return;
                    }
                    // Relay Node
                    if (serviceBusTreeView.SelectedNode.Tag is RelayDescription)
                    {
                        var relayDescription = serviceBusTreeView.SelectedNode.Tag as RelayDescription;
                        using (var deleteForm = new DeleteForm(relayDescription.Path, RelayEntity.ToLower()))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                await serviceBusHelper.DeleteRelay(relayDescription.Path);
                            }
                        }
                        return;
                    }
                    // EventHub Node: Delete Consumer Groups
                    if (sender == deleteConsumerGroupsMenuItem)
                    {
                        if (serviceBusTreeView.SelectedNode.Nodes.Count == 0)
                        {
                            return;
                        }
                        var eventHubDescription = serviceBusTreeView.SelectedNode.Tag as EventHubDescription;
                        if (eventHubDescription == null)
                        {
                            return;
                        }
                        using (var deleteForm = new DeleteForm(string.Format(DeleteAllConsumerGroups, eventHubDescription.Path)))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var notificationHubList = new List<string>();
                                GetConsumerGroupList(notificationHubList, serviceBusTreeView.SelectedNode);
                                notificationHubList.Remove(DefaultConsumerGroupName);
                                serviceBusHelper.DeleteConsumerGroups(eventHubDescription.Path, notificationHubList);
                                refreshEntity_Click(null, null);
                            }
                        }
                        return;
                    }
                    // EventHub Node
                    if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription)
                    {
                        var eventHubDescription = serviceBusTreeView.SelectedNode.Tag as EventHubDescription;
                        using (var deleteForm = new DeleteForm(eventHubDescription.Path, EventHubEntity.ToLower()))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                serviceBusHelper.DeleteEventHub(eventHubDescription);
                            }
                        }
                        return;
                    }
                    // ConsumerGroup Node
                    if (serviceBusTreeView.SelectedNode.Tag is ConsumerGroupDescription)
                    {
                        var notificationHubDescription = serviceBusTreeView.SelectedNode.Tag as ConsumerGroupDescription;
                        using (var deleteForm = new DeleteForm(notificationHubDescription.Name, ConsumerGroupEntity.ToLower()))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                serviceBusHelper.DeleteConsumerGroup(notificationHubDescription);
                            }
                        }
                        return;
                    }
                    // NotificationHub Node
                    if (serviceBusTreeView.SelectedNode.Tag is NotificationHubDescription)
                    {
                        var notificationHubDescription = serviceBusTreeView.SelectedNode.Tag as NotificationHubDescription;
                        using (var deleteForm = new DeleteForm(notificationHubDescription.Path, NotificationHubEntity.ToLower()))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                await serviceBusHelper.DeleteNotificationHub(notificationHubDescription);
                            }
                        }
                        return;
                    }
                    // Subscriptions Node
                    if (serviceBusTreeView.SelectedNode.Text == SubscriptionEntities ||
                        serviceBusTreeView.SelectedNode.Text == FilteredSubscriptionEntities)
                    {
                        var subscriptionDescriptions = serviceBusTreeView.SelectedNode.
                                                            Nodes.
                                                            Cast<TreeNode>().
                                                            Select(n => ((SubscriptionWrapper)n.Tag).SubscriptionDescription).
                                                            ToList();
                        if (subscriptionDescriptions.Count > 0)
                        {
                            using (var deleteForm = new DeleteForm(DeleteAllSubscriptions))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    await serviceBusHelper.DeleteSubscriptions(subscriptionDescriptions);
                                }
                            }
                        }
                        return;
                    }
                    // Subscription Node
                    if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                    {
                        var wrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                        if (wrapper.TopicDescription != null &&
                            wrapper.SubscriptionDescription != null)
                        {
                            using (var deleteForm = new DeleteForm(wrapper.SubscriptionDescription.Name, SubscriptionEntity.ToLower()))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    await serviceBusHelper.DeleteSubscription(wrapper.SubscriptionDescription);
                                }
                            }
                        }
                        return;
                    }
                    // Rules Node
                    if (serviceBusTreeView.SelectedNode.Text == RuleEntities)
                    {
                        var ruleWrappers = serviceBusTreeView.SelectedNode.
                                                            Nodes.
                                                            Cast<TreeNode>().
                                                            Select(n => (RuleWrapper)n.Tag).
                                                            ToList();
                        if (ruleWrappers.Count > 0)
                        {
                            using (var deleteForm = new DeleteForm(DeleteAllRules))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    serviceBusHelper.RemoveRules(ruleWrappers);
                                }
                            }
                        }
                        return;
                    }
                    // Rule Node
                    if (serviceBusTreeView.SelectedNode.Tag is RuleWrapper)
                    {
                        var wrapper = serviceBusTreeView.SelectedNode.Tag as RuleWrapper;
                        if (wrapper.SubscriptionDescription != null &&
                            wrapper.RuleDescription != null)
                        {
                            using (var deleteForm = new DeleteForm(wrapper.RuleDescription.Name, RuleEntity.ToLower()))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    serviceBusHelper.RemoveRule(wrapper.SubscriptionDescription, wrapper.RuleDescription);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private async void changeStatusEntity_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (serviceBusHelper != null &&
                    serviceBusTreeView.SelectedNode != null)
                {
                    // Queue Node
                    if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                    {
                        var queueDescription = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                        using (var changeStatusForm = new ChangeStatusForm(queueDescription.Path, QueueEntity.ToLower(), queueDescription.Status))
                        {
                            if (changeStatusForm.ShowDialog() == DialogResult.OK)
                            {
                                queueDescription.Status = queueDescription.Status == EntityStatus.Active
                                                          ? EntityStatus.Disabled
                                                          : EntityStatus.Active;
                                serviceBusHelper.NamespaceManager.UpdateQueue(queueDescription);
                                refreshEntity_Click(null, null);
                                changeStatusQueueMenuItem.Text = queueDescription.Status == EntityStatus.Active
                                                                     ? DisableQueue
                                                                     : EnableQueue;
                                var item = actionsToolStripMenuItem.DropDownItems[ChangeStatusQueueMenuItem];
                                if (item != null)
                                {
                                    item.Text = changeStatusQueueMenuItem.Text;
                                }
                            }
                        }
                        return;
                    }
                    // Topic Node
                    if (serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                    {
                        var topicDescription = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                        using (var changeStatusForm = new ChangeStatusForm(topicDescription.Path, TopicEntity.ToLower(), topicDescription.Status))
                        {
                            if (changeStatusForm.ShowDialog() == DialogResult.OK)
                            {
                                topicDescription.Status = topicDescription.Status == EntityStatus.Active
                                                          ? EntityStatus.Disabled
                                                          : EntityStatus.Active;
                                serviceBusHelper.NamespaceManager.UpdateTopic(topicDescription);
                                refreshEntity_Click(null, null);
                                changeStatusTopicMenuItem.Text = topicDescription.Status == EntityStatus.Active
                                                                     ? DisableTopic
                                                                     : EnableTopic;
                                var item = actionsToolStripMenuItem.DropDownItems[ChangeStatusTopicMenuItem];
                                if (item != null)
                                {
                                    item.Text = changeStatusTopicMenuItem.Text;
                                }
                            }
                        }
                        return;
                    }

                    // Subscription Node
                    if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                    {
                        var wrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                        if (wrapper.TopicDescription != null &&
                            wrapper.SubscriptionDescription != null)
                        {
                            using (var changeStatusForm = new ChangeStatusForm(wrapper.SubscriptionDescription.Name, SubscriptionEntity.ToLower(), wrapper.SubscriptionDescription.Status))
                            {
                                if (changeStatusForm.ShowDialog() == DialogResult.OK)
                                {
                                    wrapper.SubscriptionDescription.Status = wrapper.SubscriptionDescription.Status == EntityStatus.Active
                                                                             ? EntityStatus.Disabled
                                                                             : EntityStatus.Active;
                                    serviceBusHelper.NamespaceManager.UpdateSubscription(wrapper.SubscriptionDescription);
                                    refreshEntity_Click(null, null);
                                    changeStatusSubscriptionMenuItem.Text = wrapper.SubscriptionDescription.Status == EntityStatus.Active
                                                                         ? DisableSubscription
                                                                         : EnableSubscription;
                                    var item = actionsToolStripMenuItem.DropDownItems[ChangeStatusSubscriptionMenuItem];
                                    if (item != null)
                                    {
                                        item.Text = changeStatusSubscriptionMenuItem.Text;
                                    }
                                }
                            }
                        }
                    }
                    // Event Hub
                    if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription)
                    {
                        var eventHubDescription = serviceBusTreeView.SelectedNode.Tag as EventHubDescription;
                        using (var changeStatusForm = new ChangeStatusForm(eventHubDescription.Path, EventHubEntity.ToLower(), eventHubDescription.Status))
                        {
                            if (changeStatusForm.ShowDialog() == DialogResult.OK)
                            {
                                eventHubDescription.Status = eventHubDescription.Status == EntityStatus.Active
                                                          ? EntityStatus.Disabled
                                                          : EntityStatus.Active;
                                await serviceBusHelper.NamespaceManager.UpdateEventHubAsync(eventHubDescription);
                                refreshEntity_Click(null, null);
                                changeStatusEventHubMenuItem.Text = eventHubDescription.Status == EntityStatus.Active
                                                                     ? DisableEventHub
                                                                     : EnableEventHub;
                                var item = actionsToolStripMenuItem.DropDownItems[ChangeStatusEventHubMenuItem];
                                if (item != null)
                                {
                                    item.Text = changeStatusEventHubMenuItem.Text;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void testEntityInSDIMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode == null)
                {
                    return;
                }
                if (serviceBusTreeView.SelectedNode.Tag == null)
                {
                    return;
                }
                // QueueDescription Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                {
                    var queue = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                    panelMain.HeaderText = string.Format(TestQueueFormat, queue.Path);
                    TestQueue(queue, true);
                    return;
                }
                // TopicDescription Node
                if (serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                {
                    var topic = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                    var subscriptionList = new List<SubscriptionDescription>();
                    if (serviceBusTreeView.SelectedNode.Nodes.ContainsKey(SubscriptionEntities))
                    {
                        var subscriptionsNode = serviceBusTreeView.SelectedNode.Nodes[SubscriptionEntities];
                        if (subscriptionsNode != null &&
                            subscriptionsNode.Nodes.Count > 0)
                        {
                            for (var i = 0; i < subscriptionsNode.Nodes.Count; i++)
                            {
                                var wrapper = subscriptionsNode.Nodes[i].Tag as SubscriptionWrapper;
                                if (wrapper != null &&
                                    wrapper.SubscriptionDescription != null)
                                {
                                    subscriptionList.Add(wrapper.SubscriptionDescription);
                                }
                            }
                        }
                    }

                    panelMain.HeaderText = string.Format(TestTopicFormat, topic.Path);
                    TestTopic(topic, subscriptionList, true);
                    return;
                }

                // SubscriptionDescription Node
                if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                {
                    var subscriptionWrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                    panelMain.HeaderText = string.Format(TestSubscriptionFormat, subscriptionWrapper.SubscriptionDescription.Name);
                    TestSubscription(subscriptionWrapper, true);
                }

                // RelayDescription Node
                if (serviceBusTreeView.SelectedNode.Tag is RelayDescription)
                {
                    var relay = serviceBusTreeView.SelectedNode.Tag as RelayDescription;
                    panelMain.HeaderText = string.Format(TestRelayFormat, relay.Path);
                    TestRelay(relay, true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void testEntityInMDIMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode != null)
                {
                    if (serviceBusTreeView.SelectedNode.Tag == null)
                    {
                        return;
                    }
                    // QueueDescription Node
                    if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                    {
                        var queue = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                        TestQueue(queue, false);
                        return;
                    }
                    // TopicDescription Node
                    if (serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                    {
                        var topic = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                        var subscriptionList = new List<SubscriptionDescription>();
                        if (serviceBusTreeView.SelectedNode.Nodes.ContainsKey(SubscriptionEntities))
                        {
                            var subscriptionsNode = serviceBusTreeView.SelectedNode.Nodes[SubscriptionEntities];
                            if (subscriptionsNode != null &&
                                subscriptionsNode.Nodes.Count > 0)
                            {
                                for (var i = 0; i < subscriptionsNode.Nodes.Count; i++)
                                {
                                    var wrapper = subscriptionsNode.Nodes[i].Tag as SubscriptionWrapper;
                                    if (wrapper != null &&
                                        wrapper.SubscriptionDescription != null)
                                    {
                                        subscriptionList.Add(wrapper.SubscriptionDescription);
                                    }
                                }
                            }
                        }
                        TestTopic(topic, subscriptionList, false);
                        return;
                    }

                    // SubscriptionDescription Node
                    if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                    {
                        var subscriptionWrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                        TestSubscription(subscriptionWrapper, false);
                    }

                    // RelayDescription Node
                    if (serviceBusTreeView.SelectedNode.Tag is RelayDescription)
                    {
                        var queue = serviceBusTreeView.SelectedNode.Tag as RelayDescription;
                        TestRelay(queue, false);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void createEntityListenerMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode == null || ServiceBusTreeView.SelectedNode.Tag == null)
                {
                    return;
                }

                // QueueDescription Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                {
                    var queueDescription = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                    var form = new ContainerForm(serviceBusHelper, this, FormTypeEnum.Listener, queueDescription);
                    form.Show();
                    return;
                }

                // SubscriptionDescription Node
                if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                {
                    var subscriptionWrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                    var form = new ContainerForm(serviceBusHelper, this, FormTypeEnum.Listener, subscriptionWrapper);
                    form.Show();
                }

                // ConsumerGroup Node
                if (serviceBusTreeView.SelectedNode.Tag is ConsumerGroupDescription)
                {
                    var consumerGroupPartition = serviceBusTreeView.SelectedNode.Tag as ConsumerGroupDescription;
                    var form = new ContainerForm(serviceBusHelper, this, consumerGroupPartition, GetPartitionsFromConsumerGroup(serviceBusTreeView.SelectedNode));
                    form.Show();
                }

                // PartitionDescription Node
                if (serviceBusTreeView.SelectedNode.Tag is PartitionDescription)
                {
                    var consumerGroupPartition = serviceBusTreeView.SelectedNode.Parent.Parent.Tag as ConsumerGroupDescription;
                    var partitionDescription = serviceBusTreeView.SelectedNode.Tag as PartitionDescription;
                    var form = new ContainerForm(serviceBusHelper, this, consumerGroupPartition, new[] { partitionDescription });
                    form.Show();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion

        #region Public Methods
        public void HandleException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }
            WriteToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message))
            {
                WriteToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }

        public void RefreshQueues()
        {
            GetEntities(EntityType.Queue);
        }

        public void RefreshTopics()
        {
            GetEntities(EntityType.Topic);
        }
        #endregion

        #region Private Methods
        private Task StopLog()
        {
            cancellationTokenSource.Cancel();
            return logTask;
        }

        private void StartLog()
        {
            if (logCollection != null)
            {
                logCollection.Dispose();
            }
            logCollection = new BlockingCollection<string>();
            cancellationTokenSource = new CancellationTokenSource();
            logTask = Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    WriteToLog(t.Exception.Message);
                }
            });
        }

        private async void AsyncWriteToLog()
        {
            try
            {
                var count = 1;
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    string message;
                    var ok = logCollection.TryTake(out message, 100);
                    if (!ok)
                    {
                        continue;
                    }
                    count = (count + 1) % 10;
                    if (count == 0)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(5));
                    }
                    if (InvokeRequired)
                    {
                        Invoke(new Action<string>(InternalWriteToLog), new object[] { message });
                    }
                    else
                    {
                        InternalWriteToLog(message);
                    }
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns


        private void WriteToLog(string message, bool async = true)
        {
            if (async)
            {
                logCollection.Add(message);
            }
            else
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(InternalWriteToLog), new object[] { message });
                }
                else
                {
                    InternalWriteToLog(message);
                }
            }
        }

        private void InternalWriteToLog(string message)
        {
            lock (this)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    return;
                }
                var lines = message.Split('\n');
                var objNow = DateTime.Now;
                var space = new string(' ', 11);

                for (var i = 0; i < lines.Length; i++)
                {
                    if (i == 0)
                    {
                        var line = string.Format(DateFormat,
                                                    objNow.Hour,
                                                    objNow.Minute,
                                                    objNow.Second,
                                                    lines[i]);
                        lstLog.Items.Add(line);
                    }
                    else
                    {
                        lstLog.Items.Add(space + lines[i]);
                    }
                }
                lstLog.SelectedIndex = lstLog.Items.Count - 1;
                lstLog.SelectedIndex = -1;
            }
        }

        private void HandleNodeMouseClick(TreeNode node)
        {
            try
            {
                if (node == null)
                {
                    return;
                }
                currentNode = node;
                serviceBusTreeView.SuspendDrawing();
                serviceBusTreeView.SuspendLayout();
                serviceBusTreeView.BeginUpdate();
                var queueListNode = FindNode(QueueEntities, rootNode);
                var topicListNode = FindNode(TopicEntities, rootNode);
                var eventHubListNode = FindNode(EventHubEntities, rootNode);
                var notificationHubListNode = FindNode(NotificationHubEntities, rootNode);
                var relayServiceListNode = FindNode(RelayEntities, rootNode);
                actionsToolStripMenuItem.DropDownItems.Clear();
                actionsToolStripMenuItem.DropDownItems.Add(createIoTHubListenerMenuItem);
                actionsToolStripMenuItem.DropDownItems.Add(createEventHubListenerMenuItem);
                // Root Node
                if (node == rootNode)
                {
                    var list = CloneItems(rootContextMenuStrip.Items);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // Queues Node
                if (node == queueListNode)
                {
                    var list = CloneItems(queuesContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // Topics Node
                if (node == topicListNode)
                {
                    var list = CloneItems(topicsContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // EventHubs Node
                if (node == eventHubListNode)
                {
                    var list = CloneItems(eventHubsContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // NotificationHubs Node
                if (node == notificationHubListNode)
                {
                    var list = CloneItems(notificationHubsContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // Relays Node
                if (node == relayServiceListNode)
                {
                    var list = CloneItems(relayServicesContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // Subscriptions Node
                if (node.Text == SubscriptionEntities ||
                    node.Text == FilteredSubscriptionEntities)
                {
                    var list = CloneItems(subscriptionsContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // Rules Node
                if (node.Text == RuleEntities)
                {
                    var list = CloneItems(rulesContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // Partitions Node
                if (node.Text == PartitionEntities)
                {
                    var list = CloneItems(partitionsContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                // Consumer Groups Node
                if (node.Text == ConsumerGroupEntities)
                {
                    var list = CloneItems(notificationHubsContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    return;
                }
                if (node.Tag == null)
                {
                    return;
                }
                // Url Segment Node
                if (node.Tag is UrlSegmentWrapper)
                {
                    var urlSegmentWrapper = node.Tag as UrlSegmentWrapper;
                    if (urlSegmentWrapper.EntityType == EntityType.Queue)
                    {
                        var list = CloneItems(queueFolderContextMenuStrip.Items);
                        AddImportAndSeparatorMenuItems(list);
                        actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    }
                    else
                    {
                        var list = CloneItems(topicFolderContextMenuStrip.Items);
                        AddImportAndSeparatorMenuItems(list);
                        actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    }
                    return;
                }
                // Queue Node
                if (node.Tag is QueueDescription)
                {
                    var queueDescription = node.Tag as QueueDescription;
                    changeStatusQueueMenuItem.Text = queueDescription.Status == EntityStatus.Active ? DisableQueue : EnableQueue;
                    getQueueMessageSessionsMenuItem.Visible = queueDescription.RequiresSession;
                    getQueueMessageSessionsSeparator.Visible = queueDescription.RequiresSession;
                    queueReceiveMessagesMenuItem.Visible = string.IsNullOrWhiteSpace(queueDescription.ForwardTo);
                    queueReceiveToolStripSeparator.Visible = string.IsNullOrWhiteSpace(queueDescription.ForwardTo);
                    var list = CloneItems(queueContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewQueueFormat, queueDescription.Path);
                    ShowQueue(queueDescription, null);
                    return;
                }
                // Topic Node
                if (node.Tag is TopicDescription)
                {
                    var topicDescription = node.Tag as TopicDescription;
                    changeStatusTopicMenuItem.Text = topicDescription.Status == EntityStatus.Active ? DisableTopic : EnableTopic;
                    var list = CloneItems(topicContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewTopicFormat, topicDescription.Path);
                    ShowTopic(topicDescription, null);
                    return;
                }
                // Relay Node
                if (node.Tag is RelayDescription)
                {
                    var relayDescription = node.Tag as RelayDescription;
                    deleteRelayMenuItem.Visible = !relayDescription.IsDynamic;
                    exportRelayMenuItem.Visible = !relayDescription.IsDynamic;
                    relayToolStripSeparator1.Visible = !relayDescription.IsDynamic;
                    relayToolStripSeparator2.Visible = !relayDescription.IsDynamic;
                    var list = CloneItems(relayContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewRelayFormat, relayDescription.Path);
                    ShowRelay(relayDescription, null);
                    return;
                }
                // EventHub Node
                if (node.Tag is EventHubDescription)
                {
                    var eventHubDescription = node.Tag as EventHubDescription;
                    changeStatusEventHubMenuItem.Text = eventHubDescription.Status == EntityStatus.Active ? DisableEventHub : EnableEventHub;
                    var list = CloneItems(eventHubContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewEventHubFormat, eventHubDescription.Path);
                    ShowEventHub(eventHubDescription);
                    return;
                }
                // Partition Node
                if (node.Tag is PartitionDescription)
                {
                    var partitionDescription = node.Tag as PartitionDescription;
                    var list = CloneItems(partitionContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewPartitionFormat, partitionDescription.PartitionId);
                    var consumerGroup = node.Parent.Parent.Tag as ConsumerGroupDescription;
                    var consumerGroupName = consumerGroup != null ? consumerGroup.Name : null;
                    partitionDescription = serviceBusHelper.GetPartition(partitionDescription.EventHubPath,
                                                                         consumerGroupName,
                                                                         partitionDescription.PartitionId);
                    ShowPartition(partitionDescription);
                    return;
                }
                // Consumer Group
                if (node.Tag is ConsumerGroupDescription)
                {
                    var notificationHubDescription = node.Tag as ConsumerGroupDescription;
                    var list = CloneItems(notificationHubContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewConsumerGroupFormat, notificationHubDescription.Name);
                    ShowConsumerGroup(notificationHubDescription, notificationHubDescription.EventHubPath);
                    return;
                }
                // NotificationHub Node
                if (node.Tag is NotificationHubDescription)
                {
                    var notificationHubDescription = node.Tag as NotificationHubDescription;
                    var list = CloneItems(notificationHubContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewNotificationHubFormat, notificationHubDescription.Path);
                    ShowNotificationHub(notificationHubDescription);
                    return;
                }
                // Subscription Node
                if (node.Tag is SubscriptionWrapper)
                {
                    var wrapper = node.Tag as SubscriptionWrapper;
                    changeStatusSubscriptionMenuItem.Text = wrapper.SubscriptionDescription.Status == EntityStatus.Active ? DisableSubscription : EnableSubscription;
                    getSubscriptionMessageSessionsMenuItem.Visible = wrapper.SubscriptionDescription.RequiresSession;
                    getSubscriptionMessageSessionsSeparator.Visible = wrapper.SubscriptionDescription.RequiresSession;
                    subReceiveMessagesMenuItem.Visible = string.IsNullOrWhiteSpace(wrapper.SubscriptionDescription.ForwardTo);
                    subReceiveToolStripSeparator.Visible = string.IsNullOrWhiteSpace(wrapper.SubscriptionDescription.ForwardTo);
                    var list = CloneItems(subscriptionContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewSubscriptionFormat, wrapper.SubscriptionDescription.Name);
                    ShowSubscription(wrapper);
                    return;
                }

                // RuleDescription Node
                if (node.Tag is RuleWrapper)
                {
                    var wrapper = node.Tag as RuleWrapper;
                    var list = CloneItems(ruleContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewRuleFormat, wrapper.RuleDescription.Name);
                    ShowRule(wrapper, null);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                serviceBusTreeView.ResumeDrawing();
                serviceBusTreeView.ResumeLayout();
                serviceBusTreeView.EndUpdate();
            }
        }

        public static ServiceBusNamespace GetServiceBusNamespace(string key, string connectionString)
        {

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIsNullOrEmpty, key));
                return null;
            }

            var toLower = connectionString.ToLower();
            var parameters = connectionString.Split(';').ToDictionary(s => s.Substring(0, s.IndexOf('=')).ToLower(), s => s.Substring(s.IndexOf('=') + 1));

            if (toLower.Contains(ConnectionStringEndpoint) &&
                toLower.Contains(ConnectionStringSharedAccessKeyName) &&
                toLower.Contains(ConnectionStringSharedAccessKey))
            {
                if (parameters.Count < 3)
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIsWrong, key));
                    return null;
                }
                var endpoint = parameters.ContainsKey(ConnectionStringEndpoint) ?
                               parameters[ConnectionStringEndpoint] :
                               null;

                if (string.IsNullOrWhiteSpace(endpoint))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceEndpointIsNullOrEmpty, key));
                    return null;
                }

                var stsEndpoint = parameters.ContainsKey(ConnectionStringStsEndpoint) ?
                                  parameters[ConnectionStringStsEndpoint] :
                                  null;

                Uri uri;
                try
                {
                    uri = new Uri(endpoint);
                }
                catch (Exception)
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceEndpointUriIsInvalid, key));
                    return null;
                }
                var ns = uri.Host.Split('.')[0];

                if (!parameters.ContainsKey(ConnectionStringSharedAccessKeyName) || string.IsNullOrWhiteSpace(parameters[ConnectionStringSharedAccessKeyName]))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceSharedAccessKeyNameIsInvalid, key));
                }
                var sharedAccessKeyName = parameters[ConnectionStringSharedAccessKeyName];

                if (!parameters.ContainsKey(ConnectionStringSharedAccessKey) || string.IsNullOrWhiteSpace(parameters[ConnectionStringSharedAccessKey]))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceSharedAccessKeyIsInvalid, key));
                }
                var sharedAccessKey = parameters[ConnectionStringSharedAccessKey];


                var settings = new MessagingFactorySettings();
                var transportType = settings.TransportType;

                if (parameters.ContainsKey(ConnectionStringTransportType))
                {
                    Enum.TryParse(parameters[ConnectionStringTransportType], true, out transportType);
                }

                string entityPath = string.Empty;
                if (parameters.ContainsKey(ConnectionStringEntityPath))
                {
                    entityPath = parameters[ConnectionStringEntityPath];
                }

                return new ServiceBusNamespace(ServiceBusNamespaceType.Cloud, connectionString, endpoint, ns, null, sharedAccessKeyName, sharedAccessKey, stsEndpoint, transportType, true, entityPath);
            }

            if (toLower.Contains(ConnectionStringRuntimePort) ||
                toLower.Contains(ConnectionStringManagementPort) ||
                toLower.Contains(ConnectionStringWindowsUsername) ||
                toLower.Contains(ConnectionStringWindowsDomain) ||
                toLower.Contains(ConnectionStringWindowsPassword))
            {
                if (!toLower.Contains(ConnectionStringEndpoint) ||
                    !toLower.Contains(ConnectionStringStsEndpoint) ||
                    !toLower.Contains(ConnectionStringRuntimePort) ||
                    !toLower.Contains(ConnectionStringManagementPort))
                {
                    return null;
                }

                var endpoint = parameters.ContainsKey(ConnectionStringEndpoint) ?
                               parameters[ConnectionStringEndpoint] :
                               null;

                if (string.IsNullOrWhiteSpace(endpoint))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceEndpointIsNullOrEmpty, key));
                    return null;
                }

                Uri uri;
                try
                {
                    uri = new Uri(endpoint);
                }
                catch (Exception)
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceEndpointUriIsInvalid, key));
                    return null;
                }
                var ns = uri.Host.Split('.')[0];

                var stsEndpoint = parameters.ContainsKey(ConnectionStringStsEndpoint) ?
                                  parameters[ConnectionStringStsEndpoint] :
                                  null;

                if (string.IsNullOrWhiteSpace(stsEndpoint))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceStsEndpointIsNullOrEmpty, key));
                    return null;
                }

                var runtimePort = parameters.ContainsKey(ConnectionStringRuntimePort) ?
                                  parameters[ConnectionStringRuntimePort] :
                                  null;

                if (string.IsNullOrWhiteSpace(runtimePort))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceRuntimePortIsNullOrEmpty, key));
                    return null;
                }

                var managementPort = parameters.ContainsKey(ConnectionStringManagementPort) ?
                                     parameters[ConnectionStringManagementPort] :
                                     null;

                if (string.IsNullOrWhiteSpace(managementPort))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceManagementPortIsNullOrEmpty, key));
                    return null;
                }

                var windowsDomain = parameters.ContainsKey(ConnectionStringWindowsDomain) ?
                                    parameters[ConnectionStringWindowsDomain] :
                                    null;

                var windowsUsername = parameters.ContainsKey(ConnectionStringWindowsUsername) ?
                                      parameters[ConnectionStringWindowsUsername] :
                                      null;

                var windowsPassword = parameters.ContainsKey(ConnectionStringWindowsPassword) ?
                                      parameters[ConnectionStringWindowsPassword] :
                                      null;
                var settings = new MessagingFactorySettings();
                var transportType = settings.TransportType;
                if (parameters.ContainsKey(ConnectionStringTransportType))
                {
                    Enum.TryParse(parameters[ConnectionStringTransportType], true, out transportType);
                }
                return new ServiceBusNamespace(connectionString, endpoint, stsEndpoint, runtimePort, managementPort, windowsDomain, windowsUsername, windowsPassword, ns, transportType);
            }

            if (toLower.Contains(ConnectionStringEndpoint) &&
                toLower.Contains(ConnectionStringSharedSecretIssuer) &&
                toLower.Contains(ConnectionStringSharedSecretValue))
            {
                if (parameters.Count < 3)
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIsWrong, key));
                    return null;
                }

                var endpoint = parameters.ContainsKey(ConnectionStringEndpoint) ?
                               parameters[ConnectionStringEndpoint] :
                               null;

                if (string.IsNullOrWhiteSpace(endpoint))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceEndpointIsNullOrEmpty, key));
                    return null;
                }

                var stsEndpoint = parameters.ContainsKey(ConnectionStringStsEndpoint) ?
                                  parameters[ConnectionStringStsEndpoint] :
                                  null;

                Uri uri;
                try
                {
                    uri = new Uri(endpoint);
                }
                catch (Exception)
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceEndpointUriIsInvalid, key));
                    return null;
                }
                var ns = uri.Host.Split('.')[0];
                var issuerName = parameters.ContainsKey(ConnectionStringSharedSecretIssuer) ?
                                     parameters[ConnectionStringSharedSecretIssuer] :
                                     ConnectionStringOwner;

                if (!parameters.ContainsKey(ConnectionStringSharedSecretValue) ||
                    string.IsNullOrWhiteSpace(parameters[ConnectionStringSharedSecretValue]))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIssuerSecretIsNullOrEmpty, key));
                    return null;

                }
                var issuerSecret = parameters[ConnectionStringSharedSecretValue];

                var settings = new MessagingFactorySettings();
                var transportType = settings.TransportType;
                if (parameters.ContainsKey(ConnectionStringTransportType))
                {
                    Enum.TryParse(parameters[ConnectionStringTransportType], true, out transportType);
                }

                return new ServiceBusNamespace(ServiceBusNamespaceType.Cloud, connectionString, endpoint, ns, null, issuerName, issuerSecret, stsEndpoint, transportType);
            }
            else
            {
                if (parameters.Count < 4)
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIsWrong, key));
                    return null;
                }

                var uriString = parameters.ContainsKey(ConnectionStringUri) ?
                                    parameters[ConnectionStringUri] :
                                    null;

                if (string.IsNullOrWhiteSpace(uriString) && !parameters.ContainsKey(ConnectionStringNameSpace))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceNamespaceAndUriAreNullOrEmpty, key));
                    return null;
                }

                var ns = parameters[ConnectionStringNameSpace];

                var servicePath = parameters.ContainsKey(ConnectionStringServicePath) ?
                                      parameters[ConnectionStringServicePath] :
                                      null;

                if (!parameters.ContainsKey(ConnectionStringIssuerName))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIssuerNameIsNullOrEmpty, key));
                    return null;
                }
                var issuerName = parameters.ContainsKey(ConnectionStringIssuerName) ?
                                     parameters[ConnectionStringIssuerName] :
                                     ConnectionStringOwner;

                if (!parameters.ContainsKey(ConnectionStringIssuerSecret) ||
                    string.IsNullOrWhiteSpace(parameters[ConnectionStringIssuerSecret]))
                {
                    StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ServiceBusNamespaceIssuerSecretIsNullOrEmpty, key));
                    return null;

                }
                var issuerSecret = parameters[ConnectionStringIssuerSecret];

                var settings = new MessagingFactorySettings();
                var transportType = settings.TransportType;
                if (parameters.ContainsKey(ConnectionStringTransportType))
                {
                    Enum.TryParse(parameters[ConnectionStringTransportType], true, out transportType);
                }

                return new ServiceBusNamespace(ServiceBusNamespaceType.Custom, connectionString, uriString, ns, servicePath, issuerName, issuerSecret, null, transportType);
            }
        }

        private void GetServiceBusNamespacesFromConfiguration()
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                var hashtable = ConfigurationManager.GetSection(ServiceBusNamespaces) as Hashtable;

                if (hashtable == null || hashtable.Count == 0)
                {
                    WriteToLog(ServiceBusNamespacesNotConfigured);
                }
                serviceBusHelper.ServiceBusNamespaces = new Dictionary<string, ServiceBusNamespace>();
                if (hashtable == null)
                {
                    return;
                }
                var e = hashtable.GetEnumerator();

                while (e.MoveNext())
                {
                    if (!(e.Key is string) || !(e.Value is string))
                    {
                        continue;
                    }
                    var serviceBusNamespace = GetServiceBusNamespace((string) e.Key, (string) e.Value);
                    if (serviceBusNamespace != null)
                    {
                        serviceBusHelper.ServiceBusNamespaces.Add((string) e.Key, serviceBusNamespace);
                    }
                }
                microsoftServiceBusConnectionString = ConfigurationManager.AppSettings[ConfigurationParameters.MicrosoftServiceBusConnectionString];
                if (!string.IsNullOrWhiteSpace(microsoftServiceBusConnectionString))
                {
                    var serviceBusNamespace = GetServiceBusNamespace(ConfigurationParameters.MicrosoftServiceBusConnectionString, microsoftServiceBusConnectionString);
                    if (serviceBusNamespace != null)
                    {
                        serviceBusHelper.ServiceBusNamespaces.Add(ConfigurationParameters.MicrosoftServiceBusConnectionString, serviceBusNamespace);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void GetServiceBusNamespaceFromEnvironmentVariable()
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureServiceBus.ConnectionString", EnvironmentVariableTarget.User);
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                const string key = @"HKEY_CURRENT_USER\Environment connection string";
                serviceBusHelper.ServiceBusNamespaces.Add(key, GetServiceBusNamespace(key, connectionString));
            }
        }

        private void GetBrokeredMessageInspectorsFromConfiguration()
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                var hashtable = ConfigurationManager.GetSection(BrokeredMessageInspectors) as Hashtable;

                if (hashtable == null || hashtable.Count == 0)
                {
                    return;
                }
                serviceBusHelper.BrokeredMessageInspectors = new Dictionary<string, Type>();
                var e = hashtable.GetEnumerator();

                while (e.MoveNext())
                {
                    if (!(e.Key is string) || !(e.Value is string))
                    {
                        continue;
                    }
                    try
                    {
                        var type = Type.GetType((string) e.Value);
                        if (type != null && type.GetInterfaces().Contains(typeof(IBrokeredMessageInspector)))
                        {
                            if (type.GetConstructor(BindingFlags.Instance |
                                                    BindingFlags.Public |
                                                    BindingFlags.NonPublic,
                                                    null,
                                                    Type.EmptyTypes,
                                                    null) != null)
                            {
                                serviceBusHelper.BrokeredMessageInspectors.Add(e.Key as string, type);
                            }
                        }
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void GetEventDataInspectorsFromConfiguration()
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                var hashtable = ConfigurationManager.GetSection(EventDataInspectors) as Hashtable;

                if (hashtable == null || hashtable.Count == 0)
                {
                    return;
                }
                serviceBusHelper.EventDataInspectors = new Dictionary<string, Type>();
                var e = hashtable.GetEnumerator();

                while (e.MoveNext())
                {
                    if (!(e.Key is string) || !(e.Value is string))
                    {
                        continue;
                    }
                    try
                    {
                        var type = Type.GetType((string) e.Value);
                        if (type != null && type.GetInterfaces().Contains(typeof(IEventDataInspector)))
                        {
                            if (type.GetConstructor(BindingFlags.Instance |
                                                    BindingFlags.Public |
                                                    BindingFlags.NonPublic,
                                                    null,
                                                    Type.EmptyTypes,
                                                    null) != null)
                            {
                                serviceBusHelper.EventDataInspectors.Add(e.Key as string, type);
                            }
                        }
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void GetBrokeredMessageGeneratorsFromConfiguration()
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                var hashtable = ConfigurationManager.GetSection(BrokeredMessageGenerators) as Hashtable;

                if (hashtable == null || hashtable.Count == 0)
                {
                    return;
                }
                serviceBusHelper.BrokeredMessageGenerators = new Dictionary<string, Type>();
                var e = hashtable.GetEnumerator();

                while (e.MoveNext())
                {
                    if (!(e.Key is string) || !(e.Value is string))
                    {
                        continue;
                    }
                    try
                    {
                        var type = Type.GetType((string) e.Value);
                        if (type != null && type.GetInterfaces().Contains(typeof(IBrokeredMessageGenerator)))
                        {
                            if (type.GetConstructor(BindingFlags.Instance |
                                                    BindingFlags.Public |
                                                    BindingFlags.NonPublic,
                                                    null,
                                                    Type.EmptyTypes,
                                                    null) != null)
                            {
                                serviceBusHelper.BrokeredMessageGenerators.Add(e.Key as string, type);
                            }
                        }
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void GetEventDataGeneratorsFromConfiguration()
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                var hashtable = ConfigurationManager.GetSection(EventDataGenerators) as Hashtable;

                if (hashtable == null || hashtable.Count == 0)
                {
                    return;
                }
                serviceBusHelper.EventDataGenerators = new Dictionary<string, Type>();
                var e = hashtable.GetEnumerator();

                while (e.MoveNext())
                {
                    if (!(e.Key is string) || !(e.Value is string))
                    {
                        continue;
                    }
                    try
                    {
                        var type = Type.GetType((string) e.Value);
                        if (type != null && type.GetInterfaces().Contains(typeof(IEventDataGenerator)))
                        {
                            if (type.GetConstructor(BindingFlags.Instance |
                                                    BindingFlags.Public |
                                                    BindingFlags.NonPublic,
                                                    null,
                                                    Type.EmptyTypes,
                                                    null) != null)
                            {
                                serviceBusHelper.EventDataGenerators.Add(e.Key as string, type);
                            }
                        }
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void GetServiceBusNamespaceSettingsFromConfiguration()
        {
            if (serviceBusHelper == null)
            {
                return;
            }
            var parameter = ConfigurationManager.AppSettings[ConfigurationParameters.DebugFlagParameter];
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                bool debug;
                if (bool.TryParse(parameter, out debug))
                {
                    serviceBusHelper.TraceEnabled = debug;
                    RetryHelper.TraceEnabled = debug;
                }
            }
            parameter = ConfigurationManager.AppSettings[ConfigurationParameters.ConnectivityMode];
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                ConnectivityMode connectivityMode;
                if (Enum.TryParse(parameter, true, out connectivityMode))
                {
                    ServiceBusHelper.ConnectivityMode = connectivityMode;
                }
            }
            parameter = ConfigurationManager.AppSettings[ConfigurationParameters.Encoding];
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                EncodingType encodingType;
                if (Enum.TryParse(parameter, true, out encodingType))
                {
                    ServiceBusHelper.EncodingType = encodingType;
                }
            }
            parameter = ConfigurationManager.AppSettings[ConfigurationParameters.ShowMessageCountParameter];
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                bool.TryParse(parameter, out showMessageCount);
            }
            parameter = ConfigurationManager.AppSettings[ConfigurationParameters.SaveMessageToFileParameter];
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                bool.TryParse(parameter, out saveMessageToFile);
            }
            parameter = ConfigurationManager.AppSettings[ConfigurationParameters.SavePropertiesToFileParameter];
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                bool.TryParse(parameter, out savePropertiesToFile);
            }
            parameter = ConfigurationManager.AppSettings[ConfigurationParameters.SaveCheckpointsToFileParameter];
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                bool.TryParse(parameter, out saveCheckpointsToFile);
            }
            var scheme = ConfigurationManager.AppSettings[ConfigurationParameters.SchemeParameter];
            if (!string.IsNullOrWhiteSpace(scheme))
            {
                serviceBusHelper.Scheme = scheme;
            }
            messageText = MessageAndPropertiesHelper.ReadMessage();
            if (string.IsNullOrWhiteSpace(messageText))
            {
                messageText = ConfigurationManager.AppSettings[ConfigurationParameters.MessageParameter];
                if (string.IsNullOrWhiteSpace(messageText))
                {
                    messageText = DefaultMessageText;
                }
            }
            relayMessageText = MessageAndPropertiesHelper.ReadRelayMessage();
            messageFile = ConfigurationManager.AppSettings[ConfigurationParameters.FileParameter];
            if (!string.IsNullOrWhiteSpace(messageFile) &&
                File.Exists(messageFile))
            {
                using (var streamReader = new StreamReader(messageFile))
                {
                    var text = streamReader.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        messageText = text;
                    }
                }
            }
            label = ConfigurationManager.AppSettings[ConfigurationParameters.LabelParameter];
            if (string.IsNullOrWhiteSpace(label))
            {
                label = DefaultLabel;
            }

            subscriptionId = ConfigurationManager.AppSettings[ConfigurationParameters.SubscriptionIdParameter];
            certificateThumbprint = ConfigurationManager.AppSettings[ConfigurationParameters.CertificateThumbprintParameter];


            var logFontSizeValue = ConfigurationManager.AppSettings[ConfigurationParameters.LogFontSize];
            float tempFloat;

            if (Single.TryParse(logFontSizeValue, NumberStyles.Any, CultureInfo.InvariantCulture, out tempFloat))
            {
                logFontSize = tempFloat;
                lstLog.Font = new Font(lstLog.Font.FontFamily, logFontSize);
            }

            var treeViewFontSizeValue = ConfigurationManager.AppSettings[ConfigurationParameters.TreeViewFontSize];
            if (Single.TryParse(treeViewFontSizeValue, NumberStyles.Any, CultureInfo.InvariantCulture, out  tempFloat))
            {
                treeViewFontSize = tempFloat;
                serviceBusTreeView.Font = new Font(serviceBusTreeView.Font.FontFamily, treeViewFontSize);
            }

            var retryCountValue = ConfigurationManager.AppSettings[ConfigurationParameters.RetryCountParameter];
            int retryCount;
            if (int.TryParse(retryCountValue, out  retryCount))
            {
                RetryHelper.RetryCount = retryCount;
            }
            var retryTimeoutValue = ConfigurationManager.AppSettings[ConfigurationParameters.RetryTimeoutParameter];
            int retryTimeout;
            if (int.TryParse(retryTimeoutValue, out  retryTimeout))
            {
                RetryHelper.RetryTimeout = retryTimeout;
            }
            var receiveTimeoutValue = ConfigurationManager.AppSettings[ConfigurationParameters.ReceiveTimeoutParameter];
            int receiveTimeoutTemp;
            if (int.TryParse(receiveTimeoutValue, out  receiveTimeoutTemp) && receiveTimeoutTemp >= 0)
            {
                receiveTimeout = receiveTimeoutTemp;
            }
            var serverTimeoutValue = ConfigurationManager.AppSettings[ConfigurationParameters.ServerTimeoutParameter];
            int serverTimeoutTemp;
            if (int.TryParse(serverTimeoutValue, out  serverTimeoutTemp) && serverTimeoutTemp >= 0)
            {
                serverTimeout = serverTimeoutTemp;
            }
            var senderThinkTimeValue = ConfigurationManager.AppSettings[ConfigurationParameters.SenderThinkTimeParameter];
            int senderThinkTimeTemp;
            if (int.TryParse(senderThinkTimeValue, out  senderThinkTimeTemp) && senderThinkTimeTemp >= 0)
            {
                senderThinkTime = senderThinkTimeTemp;
            }
            var receiverThinkTimeValue = ConfigurationManager.AppSettings[ConfigurationParameters.ReceiverThinkTimeParameter];
            int receiverThinkTimeTemp;
            if (int.TryParse(receiverThinkTimeValue, out  receiverThinkTimeTemp) && receiverThinkTimeTemp >= 0)
            {
                receiverThinkTime = receiverThinkTimeTemp;
            }
            var monitorRefreshIntervalValue = ConfigurationManager.AppSettings[ConfigurationParameters.MonitorRefreshIntervalParameter];
            int monitorRefreshIntervalTemp;
            if (int.TryParse(monitorRefreshIntervalValue, out  monitorRefreshIntervalTemp) && monitorRefreshIntervalTemp >= 0)
            {
                monitorRefreshInterval = monitorRefreshIntervalTemp;
            }
            var prefetchCountValue = ConfigurationManager.AppSettings[ConfigurationParameters.PrefetchCountParameter];
            int prefetchCountTemp;
            if (int.TryParse(prefetchCountValue, out  prefetchCountTemp) && prefetchCountTemp >= 0)
            {
                prefetchCount = prefetchCountTemp;
            }
            var topValue = ConfigurationManager.AppSettings[ConfigurationParameters.TopParameter];
            int topTemp;
            if (int.TryParse(topValue, out  topTemp) && topTemp > 0)
            {
                topCount = topTemp;
            }
            var messageDeferProvider = ConfigurationManager.AppSettings[ConfigurationParameters.MessageDeferProviderParameter];
            if (!string.IsNullOrWhiteSpace(messageDeferProvider))
            {
                try
                {
                    var type = Type.GetType(messageDeferProvider);
                    if (type != null &&
                        type.GetInterfaces().Contains(typeof(IMessageDeferProvider)))
                    {
                        serviceBusHelper.MessageDeferProviderType = type;
                    }
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch (Exception)
                // ReSharper restore EmptyGeneralCatchClause
                {
                }
            }
            parameter = ConfigurationManager.AppSettings[ConfigurationParameters.SelectedEntitiesParameter];
            if (!string.IsNullOrEmpty(parameter))
            {
                var items = parameter.Split(',').Select(item => item.Trim()).ToList();
                if (items.Count == 0)
                {
                    GetDefaultSelectedEntities();
                }
                else
                {
                    foreach (var item in items)
                    {
                        if (entities.Contains(item, StringComparer.OrdinalIgnoreCase))
                        {
                            selectedEntites.Add(item);
                        }
                    }
                }
            }
            else
            {
                GetDefaultSelectedEntities();
            }
        }

        private void ReadEventHubPartitionCheckpointFile()
        {
            if (saveCheckpointsToFile)
            {
                EventProcessorCheckpointHelper.ReadCheckpoints();
            }
        }

        private void GetDefaultSelectedEntities()
        {
            selectedEntites.AddRange(entities);
        }

        private void SetControlSize(Control control)
        {
            var ok = false;
            if (panelMain.Controls.Count > 0)
            {
                try
                {
                    if (control == null)
                    {
                        control = panelMain.Controls[0];
                        control.SuspendDrawing();
                        ok = true;
                    }
                    var width = panelMain.Width - 4;
                    var height = panelMain.Height - 26;
                    control.Width = width < ControlMinWidth ? ControlMinWidth : width;
                    control.Height = height < ControlMinHeight ? ControlMinHeight : height;
                }
                finally
                {
                    if (ok)
                    {
                        control.ResumeDrawing();
                    }
                }
            }
        }
        #endregion

        #region Public Static Methods
        public static void StaticWriteToLog(string message, bool async = true)
        {
            mainSingletonMainForm.WriteToLog(message);
        }
        #endregion

        #region Public Properties
        public List<Tuple<string, string>> FileNames
        {
            get { return fileNames; }
        }

        public TreeView ServiceBusTreeView
        {
            get
            {
                return serviceBusTreeView;
            }
        }

        public string MessageText
        {
            get
            {
                return messageText;
            }
            set
            {
                messageText = value;
            }
        }

        public string RelayMessageText
        {
            get
            {
                return messageText;
            }
            set
            {
                messageText = value;
            }
        }

        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }

        public int ReceiveTimeout
        {
            get
            {
                return receiveTimeout;
            }
            set
            {
                receiveTimeout = value;
            }
        }

        public int ServerTimeout
        {
            get
            {
                return serverTimeout;
            }
            set
            {
                serverTimeout = value;
            }
        }

        public int PrefetchCount
        {
            get
            {
                return prefetchCount;
            }
            set
            {
                prefetchCount = value;
            }
        }

        public int TopCount
        {
            get
            {
                return topCount;
            }
            set
            {
                topCount = value;
            }
        }

        public int SenderThinkTime
        {
            get
            {
                return senderThinkTime;
            }
            set
            {
                senderThinkTime = value;
            }
        }

        public int ReceiverThinkTime
        {
            get
            {
                return receiverThinkTime;
            }
            set
            {
                receiverThinkTime = value;
            }
        }

        public int MonitorRefreshInterval
        {
            get
            {
                return monitorRefreshInterval;
            }
            set
            {
                monitorRefreshInterval = value;
            }
        }

        public string SubscriptionId
        {
            get
            {
                return subscriptionId;
            }
            set
            {
                subscriptionId = value;
            }
        }

        public string CertificateThumbprint
        {
            get
            {
                return certificateThumbprint;
            }
            set
            {
                certificateThumbprint = value;
            }
        }

        public bool UseAscii
        {
            get
            {
                return useAscii;
            }
            set
            {
                useAscii = value;
            }
        }

        public List<string> Entities
        {
            get
            {
                return entities;
            }
        }

        public List<string> SelectedEntities
        {
            get
            {
                return selectedEntites;
            }
        }

        public string Version => version;
        #endregion

        #region Public Static Properties
        public static MainForm SingletonMainForm => mainSingletonMainForm;

        #endregion

        #region Private Methods
        /// <summary>
        /// Saves an entity to a file.
        /// </summary>
        /// <param name="text">The text to save.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns>The file path.</returns>
        private string SaveEntityToFile(string text, string fileName)
        {
            if (string.IsNullOrWhiteSpace(text) ||
                string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }
            saveFileDialog.Title = SaveEntityAsTitle;
            saveFileDialog.DefaultExt = XmlExtension;
            saveFileDialog.Filter = XmlFilter;
            saveFileDialog.FileName = fileName;
            if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                string.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {
                return null;
            }
            if (File.Exists(saveFileDialog.FileName))
            {
                File.Delete(saveFileDialog.FileName);
            }
            using (var writer = new StreamWriter(saveFileDialog.FileName))
            {
                writer.Write(text);
            }
            return saveFileDialog.FileName;
        }

        /// <summary>
        /// Loads an entity from a file.
        /// </summary>
        /// <param name="fileName">The input file containing entities.</param>
        /// <returns>The entity xml.</returns>
        private string LoadEntityFromFile(out string fileName)
        {
            fileName = null;
            openFileDialog.Title = OpenEntityAsTitle;
            openFileDialog.DefaultExt = XmlExtension;
            openFileDialog.Filter = XmlFilter;
            if (openFileDialog.ShowDialog() != DialogResult.OK ||
                string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                return null;
            }
            if (File.Exists(openFileDialog.FileName))
            {
                fileName = openFileDialog.FileName;
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    return reader.ReadToEnd();
                }
            }
            return null;
        }

        /*
        private void DeleteVoidRelaySubTree(TreeNode node)
        {
            var list = new List<TreeNode>();
            InternalDeleteVoidRelaySubTree(node, list);
            foreach(var item in list)
            {
                if (item != node)
                {
                    serviceBusTreeView.Nodes.Remove(item);
                }
            }
        }

        private static bool InternalDeleteVoidRelaySubTree(TreeNode node, ICollection<TreeNode> list)
        {
            if (node == null ||
                node.Tag is RelayWrapper)
            {
                return false;
            }
            if (node.Nodes.Count == 0)
            {
                list.Add(node);
                return true;
            }
            var ok = node.Nodes.Cast<TreeNode>().Aggregate(true, (current, child) => InternalDeleteVoidRelaySubTree(child, list) && current);
            if (ok)
            {
                list.Add(node);
                return true;
            }
            return false;
        }

        private void DeleteVoidNotificationHubSubTree(TreeNode node)
        {
            var list = new List<TreeNode>();
            InternalDeleteVoidNotificationHubSubTree(node, list);
            foreach (var item in list)
            {
                if (item != node)
                {
                    serviceBusTreeView.Nodes.Remove(item);
                }
            }
        }

        private static bool InternalDeleteVoidNotificationHubSubTree(TreeNode node, ICollection<TreeNode> list)
        {
            if (node == null ||
                node.Tag is NotificationHubDescription)
            {
                return false;
            }
            if (node.Nodes.Count == 0)
            {
                list.Add(node);
                return true;
            }
            var ok = node.Nodes.Cast<TreeNode>().Aggregate(true, (current, child) => InternalDeleteVoidNotificationHubSubTree(child, list) && current);
            if (ok)
            {
                list.Add(node);
                return true;
            }
            return false;
        }
        

        private void CreateLeafNode(Uri uri, TreeNode parentNode, string parentTitle)
        {
            var newNode = parentNode.Nodes.Add(uri.AbsoluteUri, uri.AbsoluteUri);
            WriteToLog(string.Format(LinkUriFormat, uri.AbsoluteUri), false);
            newNode.ImageIndex = RelayUriIconIndex;
            newNode.SelectedImageIndex = RelayUriIconIndex;
            newNode.Tag = new RelayWrapper(parentTitle, uri);
            newNode.ContextMenuStrip = relayServiceContextMenuStrip;
        }

        private bool BuildRelaySubTree(Uri uri, TreeNode parentNode, string parentTitle)
        {
            XmlReader reader = null;
            try
            {
                if (string.Compare(uri.Scheme, "http", StringComparison.OrdinalIgnoreCase) != 0 &&
                    string.Compare(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase) != 0)
                {
                    CreateLeafNode(uri, parentNode, parentTitle);
                    return true;
                }
                try
                {
                    reader = XmlReader.Create(uri.AbsoluteUri);
                }
                catch (WebException)
                {
                    CreateLeafNode(uri, parentNode, parentTitle);
                    return true;
                }
                SyndicationFeed feed;
                try
                {
                    feed = SyndicationFeed.Load(reader);
                }
                catch (Exception)
                {
                    if (reader.LocalName == FaultNode)
                    {
                        CreateLeafNode(uri, parentNode, parentTitle);
                    }
                    return true;
                }

                if (feed == null ||
                    !feed.Items.Any())
                {
                    return false;
                }

                var ok = true;
                foreach (var item in feed.Items)
                {
                    if (item.Title == null ||
                        string.IsNullOrWhiteSpace(item.Title.Text))
                    {
                        continue;
                    }
                    var newNode = parentNode.Nodes.ContainsKey(item.Title.Text) ?
                                  parentNode.Nodes[item.Title.Text] :
                                  parentNode.Nodes.Add(item.Title.Text, item.Title.Text);
                    WriteToLog(string.Format(SyndicateItemFormat, item.Title.Text), false);
                    ok = item.Links.Aggregate(ok, (current, link) => BuildRelaySubTree(link.Uri, newNode, item.Title.Text) && current);
                    newNode.ImageIndex = ok ? RelayLeafIconIndex : RelayNonLeafIconIndex;
                    newNode.SelectedImageIndex = newNode.ImageIndex;
                    newNode.Tag = UrlEntity;
                    newNode.ContextMenuStrip = relayFolderContextMenuStrip;
                }
                return false;
            }
            catch (WebException)
            {
            }
            catch (NotSupportedException)
            {
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return true;
        } */

        // ReSharper disable once FunctionComplexityOverflow
        private void GetEntities(EntityType entityType)
        {
            var updating = false;

            try
            {
                if (serviceBusHelper != null)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    serviceBusTreeView.SuspendDrawing();
                    serviceBusTreeView.SuspendLayout();
                    serviceBusTreeView.BeginUpdate();
                    var queueListNode = FindNode(QueueEntities, rootNode);
                    var topicListNode = FindNode(TopicEntities, rootNode);
                    var eventHubListNode = FindNode(EventHubEntities, rootNode);
                    var notificationHubListNode = FindNode(NotificationHubEntities, rootNode);
                    var relayServiceListNode = FindNode(RelayEntities, rootNode);
                    if (entityType == EntityType.All)
                    {
                        serviceBusTreeView.Nodes.Clear();
                        rootNode = serviceBusTreeView.Nodes.Add(serviceBusHelper.NamespaceUri.AbsoluteUri, serviceBusHelper.NamespaceUri.AbsoluteUri, AzureIconIndex, AzureIconIndex);
                        rootNode.ContextMenuStrip = rootContextMenuStrip;
                        if (selectedEntites.Contains(QueueEntities))
                        {
                            queueListNode = rootNode.Nodes.Add(QueueEntities, QueueEntities, QueueListIconIndex, QueueListIconIndex);
                            queueListNode.ContextMenuStrip = queuesContextMenuStrip;
                        }
                        if (selectedEntites.Contains(TopicEntities))
                        {
                            topicListNode = rootNode.Nodes.Add(TopicEntities, TopicEntities, TopicListIconIndex, TopicListIconIndex);
                            topicListNode.ContextMenuStrip = topicsContextMenuStrip;
                        }

                        // NOTE: Relays are not actually supported by Service Bus for Windows Server
                        if (serviceBusHelper.IsCloudNamespace)
                        {
                            if (selectedEntites.Contains(EventHubEntities))
                            {
                                eventHubListNode = rootNode.Nodes.Add(EventHubEntities, EventHubEntities, EventHubListIconIndex, EventHubListIconIndex);
                                eventHubListNode.ContextMenuStrip = eventHubsContextMenuStrip;
                            }
                            if (selectedEntites.Contains(NotificationHubEntities))
                            {
                                notificationHubListNode = rootNode.Nodes.Add(NotificationHubEntities, NotificationHubEntities, NotificationHubListIconIndex, NotificationHubListIconIndex);
                                notificationHubListNode.ContextMenuStrip = notificationHubsContextMenuStrip;
                            }
                            if (selectedEntites.Contains(RelayEntities))
                            {
                                relayServiceListNode = rootNode.Nodes.Add(RelayEntities, RelayEntities, RelayListIconIndex, RelayListIconIndex);
                                relayServiceListNode.ContextMenuStrip = relayServicesContextMenuStrip;
                            }
                        }
                    }
                    updating = true;
                    if (serviceBusHelper.IsCloudNamespace)
                    {
                        if (selectedEntites.Contains(EventHubEntities) &&
                            (entityType == EntityType.All ||
                            entityType == EntityType.EventHub))
                        {
                            try
                            {

                                var eventHubs = serviceBusHelper.NamespaceManager.GetEventHubs();
                                Cursor.Current = Cursors.WaitCursor;
                                eventHubListNode.Nodes.Clear();
                                if (eventHubs != null)
                                {
                                    foreach (var eventHub in eventHubs)
                                    {
                                        if (string.IsNullOrWhiteSpace(eventHub.Path))
                                        {
                                            continue;
                                        }
                                        CreateNode(eventHub.Path, eventHub, eventHubListNode, true);
                                        Cursor.Current = Cursors.WaitCursor;
                                    }
                                }
                                if (entityType == EntityType.EventHub)
                                {
                                    serviceBusTreeView.SelectedNode = eventHubListNode;
                                    serviceBusTreeView.SelectedNode.EnsureVisible();
                                    HandleNodeMouseClick(eventHubListNode);
                                }
                            }
                            catch (Exception ex) when (FilterOutException(ex))
                            {
                                WriteToLog($"Failed to retrieve EventHub entities. Exception: {ex}");
                                serviceBusTreeView.Nodes.Remove(eventHubListNode);
                            }
                        }
                        if (selectedEntites.Contains(NotificationHubEntities) &&
                            (entityType == EntityType.All ||
                            entityType == EntityType.NotificationHub))
                        {
                            if (serviceBusHelper.NotificationHubNamespaceManager != null)
                            {
                                try
                                {
                                    var notificationHubs = serviceBusHelper.NotificationHubNamespaceManager.GetNotificationHubs();
                                    notificationHubListNode.Nodes.Clear();
                                    if (notificationHubs != null)
                                    {
                                        foreach (var notificationHub in notificationHubs)
                                        {
                                            if (string.IsNullOrWhiteSpace(notificationHub.Path))
                                            {
                                                continue;
                                            }
                                            CreateNode(notificationHub.Path, notificationHub, notificationHubListNode, true);
                                        }
                                    }
                                    if (entityType == EntityType.NotificationHub)
                                    {
                                        serviceBusTreeView.SelectedNode = notificationHubListNode;
                                        serviceBusTreeView.SelectedNode.EnsureVisible();
                                        HandleNodeMouseClick(notificationHubListNode);
                                    }
                                }
                                catch (Exception ex) when (FilterOutException(ex))
                                {
                                    WriteToLog($"Failed to retrieve Notification Hub entities. Exception: {ex}");
                                    serviceBusTreeView.Nodes.Remove(notificationHubListNode);
                                }
                            }
                            else
                            {
                                serviceBusTreeView.Nodes.Remove(notificationHubListNode);
                            } 
                        }
                        if (selectedEntites.Contains(RelayEntities) &&
                            (entityType == EntityType.All ||
                            entityType == EntityType.Relay))
                        {
                            try
                            {
                                var relayServices = serviceBusHelper.GetRelays();
                                relayServiceListNode.Text = RelayEntities;

                                relayServiceListNode.Nodes.Clear();
                                if (relayServices != null)
                                {
                                    foreach (var relayService in relayServices)
                                    {
                                        if (string.IsNullOrWhiteSpace(relayService.Path))
                                        {
                                            continue;
                                        }
                                        CreateNode(relayService.Path, relayService, relayServiceListNode, true);
                                    }
                                }
                                if (entityType == EntityType.Relay)
                                {
                                    serviceBusTreeView.SelectedNode = relayServiceListNode;
                                    serviceBusTreeView.SelectedNode.EnsureVisible();
                                    HandleNodeMouseClick(relayServiceListNode);
                                }
                            }
                            catch (Exception ex) when (FilterOutException(ex))
                            {
                                WriteToLog($"Failed to retrieve Relay entities. Exception: {ex}");
                                serviceBusTreeView.Nodes.Remove(relayServiceListNode);
                            }
                        }
                    }

                    if (selectedEntites.Contains(QueueEntities) &&
                        (entityType == EntityType.All ||
                         entityType == EntityType.Queue))
                    {
                        try
                        {
                            var queues = serviceBusHelper.GetQueues(FilterExpressionHelper.QueueFilterExpression);
                            queueListNode.Text = string.IsNullOrWhiteSpace(FilterExpressionHelper.QueueFilterExpression)
                                ? QueueEntities
                                : FilteredQueueEntities;

                            queueListNode.Nodes.Clear();
                            if (queues != null)
                            {
                                foreach (var queue in queues)
                                {
                                    if (string.IsNullOrWhiteSpace(queue.Path))
                                    {
                                        continue;
                                    }
                                    CreateNode(queue.Path, queue, queueListNode, true);
                                }
                            }
                            if (entityType == EntityType.Queue)
                            {
                                serviceBusTreeView.SelectedNode = queueListNode;
                                serviceBusTreeView.SelectedNode.EnsureVisible();
                                HandleNodeMouseClick(queueListNode);
                            }
                        }
                        catch (Exception ex) when(FilterOutException(ex))
                        {
                            WriteToLog($"Failed to retrieve Service Bus queues. Exception: {ex}");
                            serviceBusTreeView.Nodes.Remove(queueListNode);
                        }
                    }
                    if (selectedEntites.Contains(TopicEntities) &&
                        (entityType == EntityType.All ||
                         entityType == EntityType.Topic))
                    {
                        try
                        {
                            var topics = serviceBusHelper.GetTopics(FilterExpressionHelper.TopicFilterExpression);
                            topicListNode.Text = string.IsNullOrWhiteSpace(FilterExpressionHelper.TopicFilterExpression)
                                ? TopicEntities
                                : FilteredTopicEntities;
                            topicListNode.Nodes.Clear();
                            if (topics != null)
                            {
                                foreach (var topic in topics)
                                {
                                    if (string.IsNullOrWhiteSpace(topic.Path))
                                    {
                                        continue;
                                    }
                                    var entityNode = CreateNode(topic.Path, topic, topicListNode, true);
                                    try
                                    {
                                        var subscriptions = serviceBusHelper.GetSubscriptions(topic,
                                            FilterExpressionHelper.SubscriptionFilterExpression);
                                        var subscriptionDescriptions =
                                            subscriptions as IList<SubscriptionDescription> ?? subscriptions.ToList();
                                        if ((subscriptions != null &&
                                             subscriptionDescriptions.Any()) ||
                                            !string.IsNullOrWhiteSpace(
                                                FilterExpressionHelper.SubscriptionFilterExpression))
                                        {
                                            entityNode.Nodes.Clear();
                                            var subscriptionsNode = entityNode.Nodes.Add(SubscriptionEntities,
                                                SubscriptionEntities, SubscriptionListIconIndex,
                                                SubscriptionListIconIndex);
                                            subscriptionsNode.Text =
                                                string.IsNullOrWhiteSpace(
                                                    FilterExpressionHelper.SubscriptionFilterExpression)
                                                    ? SubscriptionEntities
                                                    : FilteredSubscriptionEntities;
                                            subscriptionsNode.ContextMenuStrip = subscriptionsContextMenuStrip;
                                            subscriptionsNode.Tag = new SubscriptionWrapper(null, topic,
                                                FilterExpressionHelper.SubscriptionFilterExpression);
                                            foreach (var subscription in subscriptionDescriptions)
                                            {
                                                var subscriptionNode = subscriptionsNode.Nodes.Add(subscription.Name,
                                                    showMessageCount
                                                        ? string.Format(NameMessageCountFormat,
                                                            subscription.Name,
                                                            subscription.MessageCountDetails.ActiveMessageCount,
                                                            subscription.MessageCountDetails.DeadLetterMessageCount,
                                                            subscription.MessageCountDetails.TransferDeadLetterMessageCount)
                                                        : subscription.Name,
                                                    subscription.Status == EntityStatus.Active
                                                        ? SubscriptionIconIndex
                                                        : GreySubscriptionIconIndex,
                                                    subscription.Status == EntityStatus.Active
                                                        ? SubscriptionIconIndex
                                                        : GreySubscriptionIconIndex);
                                                subscriptionNode.ContextMenuStrip = subscriptionContextMenuStrip;
                                                subscriptionNode.Tag = new SubscriptionWrapper(subscription, topic);
                                                WriteToLog(
                                                    string.Format(CultureInfo.CurrentCulture,
                                                        SubscriptionRetrievedFormat, subscription.Name, topic.Path),
                                                    false);
                                                var rules = serviceBusHelper.GetRules(subscription);
                                                var ruleDescriptions = rules as RuleDescription[] ?? rules.ToArray();
                                                if (rules != null &&
                                                    ruleDescriptions.Any())
                                                {
                                                    subscriptionNode.Nodes.Clear();
                                                    var rulesNode = subscriptionNode.Nodes.Add(RuleEntities,
                                                        RuleEntities, RuleListIconIndex, RuleListIconIndex);
                                                    rulesNode.ContextMenuStrip = rulesContextMenuStrip;
                                                    rulesNode.Tag = new RuleWrapper(null, subscription);
                                                    foreach (var rule in ruleDescriptions)
                                                    {
                                                        var ruleNode = rulesNode.Nodes.Add(rule.Name, rule.Name,
                                                            RuleIconIndex, RuleIconIndex);
                                                        ruleNode.ContextMenuStrip = ruleContextMenuStrip;
                                                        ruleNode.Tag = new RuleWrapper(rule, subscription);
                                                        WriteToLog(
                                                            string.Format(CultureInfo.CurrentCulture,
                                                                RuleRetrievedFormat, rule.Name, subscription.Name,
                                                                topic.Path), false);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        HandleException(ex);
                                    }
                                }

                            }
                            if (entityType == EntityType.Topic)
                            {
                                serviceBusTreeView.SelectedNode = topicListNode;
                                serviceBusTreeView.SelectedNode.EnsureVisible();
                                HandleNodeMouseClick(topicListNode);
                            }
                        }
                        catch (Exception ex) when (FilterOutException(ex))
                        {
                            WriteToLog($"Failed to retrieve Service Bus topics. Exception: {ex}");
                            serviceBusTreeView.Nodes.Remove(queueListNode);
                        }
                    }
                    queueListNode?.Expand();
                    topicListNode?.Expand();
                    eventHubListNode?.Expand();
                    notificationHubListNode?.Expand();
                    relayServiceListNode?.Expand();

                    rootNode.Expand();
                    if (entityType != EntityType.All)
                        return;

                    serviceBusTreeView.SelectedNode = rootNode;
                    serviceBusTreeView.SelectedNode.EnsureVisible();
                    HandleNodeMouseClick(rootNode);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                if (updating)
                {
                    serviceBusTreeView.ResumeDrawing();
                    serviceBusTreeView.ResumeLayout();
                    serviceBusTreeView.EndUpdate();
                    serviceBusTreeView.Refresh();
                }
                Cursor.Current = Cursors.Default;
            }

            bool FilterOutException(Exception ex)
            {
                return ex is ArgumentException || ex is WebException || ex is UnauthorizedAccessException || ex is MessagingException || ex is TimeoutException;
            }
        }

        private void CreateEventHubSubTree(EventHubDescription eventHub, TreeNode entityNode)
        {
            try
            {
                var partitions = GetPartitionsFromPartitionIds(eventHub);
                var partitionDescriptions = partitions as IList<PartitionDescription> ?? partitions.ToList();
                WriteToLog(string.Format(PartitionsRetrievedFormat, eventHub.PartitionCount, eventHub.Path));
                CreateEventHubConsumerGroups(eventHub, entityNode, partitionDescriptions);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void CreateEventHubPartitions(EventHubDescription eventHub, TreeNode entityNode)
        {
            var partitions = GetPartitionsFromPartitionIds(eventHub);
            var partitionDescriptions = partitions as IList<PartitionDescription> ?? partitions.ToList();
            if (partitionDescriptions.Any())
            {
                var node = FindNode(PartitionEntities, entityNode);
                if (node != null)
                {
                    entityNode.Nodes.Remove(node);
                }
                var partitionsNode = entityNode.Nodes.Add(PartitionEntities, PartitionEntities, PartitionListIconIndex, PartitionListIconIndex);
                partitionsNode.ContextMenuStrip = partitionsContextMenuStrip;
                partitionsNode.Tag = eventHub;
                foreach (var partition in partitionDescriptions)
                {
                    CreateEventHubPartitionNode(partition, partitionsNode);
                }
                WriteToLog(string.Format(PartitionsRetrievedFormat, eventHub.PartitionCount, eventHub.Path));
            }
        }

        private void CreateEventHubConsumerGroups(EventHubDescription eventHub, TreeNode entityNode, IList<PartitionDescription> partitionDescriptions)
        {
            var consumerGroups = serviceBusHelper.GetConsumerGroups(eventHub.Path);
            var consumerGroupDescriptions = consumerGroups as IList<ConsumerGroupDescription> ?? consumerGroups.ToList();
            if (consumerGroupDescriptions.Any())
            {
                var node = FindNode(ConsumerGroupEntities, entityNode);
                if (node != null)
                {
                    entityNode.Nodes.Remove(node);
                }
                var consumerGroupsNode = entityNode.Nodes.Add(ConsumerGroupEntities, ConsumerGroupEntities, ConsumerGroupListIconIndex, ConsumerGroupListIconIndex);
                consumerGroupsNode.ContextMenuStrip = consumerGroupsContextMenuStrip;
                consumerGroupsNode.Tag = eventHub;
                foreach (var consumerGroupDescription in consumerGroupDescriptions)
                {
                    CreateEventHubConsumerGroupNode(eventHub, consumerGroupDescription, partitionDescriptions, consumerGroupsNode);
                }
                WriteToLog(string.Format(ConsumerGroupsRetrievedFormat, consumerGroupDescriptions.Count, eventHub.Path));
            }
        }

        private void CreateEventHubPartitionNode(PartitionDescription partition, TreeNode partitionsNode)
        {
            int value;
            var partitionId = int.TryParse(partition.PartitionId, out value)
                              ? string.Format(PartitionFormat, value)
                              : partition.PartitionId;
            var partitionNode = partitionsNode.Nodes.Add(partition.PartitionId,
                                                         partitionId,
                                                         PartitionIconIndex,
                                                         PartitionIconIndex);
            partitionNode.ContextMenuStrip = partitionContextMenuStrip;
            partitionNode.Tag = partition;
        }

        private TreeNode CreateEventHubConsumerGroupNode(EventHubDescription eventHub,
                                                         ConsumerGroupDescription consumerGroupDescription,
                                                         IList<PartitionDescription> partitionDescriptions,
                                                         TreeNode consumerGroupsNode)
        {
            if (consumerGroupsNode.Nodes.ContainsKey(consumerGroupDescription.Name))
            {
                return null;
            }
            var consumerGroupNode = consumerGroupsNode.Nodes.Add(consumerGroupDescription.Name,
                                                                 consumerGroupDescription.Name,
                                                                 ConsumerGroupIconIndex,
                                                                 ConsumerGroupIconIndex);
            consumerGroupNode.ContextMenuStrip = consumerGroupContextMenuStrip;
            consumerGroupNode.Tag = consumerGroupDescription;
            if (partitionDescriptions == null || !partitionDescriptions.Any())
            {
                return consumerGroupNode;
            }
            consumerGroupNode.Nodes.Clear();
            var partitionsNode = consumerGroupNode.Nodes.Add(PartitionEntities, PartitionEntities, PartitionListIconIndex, PartitionListIconIndex);
            partitionsNode.ContextMenuStrip = partitionsContextMenuStrip;
            partitionsNode.Tag = eventHub;
            foreach (var partition in partitionDescriptions)
            {
                CreateEventHubPartitionNode(partition, partitionsNode);
            }
            return consumerGroupNode;
        }

        private void ShowQueue(QueueDescription queue, string path)
        {
            HandleQueueControl queueControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;
                queueControl = new HandleQueueControl(WriteToLog, serviceBusHelper, queue, path);
                queueControl.SuspendDrawing();
                queueControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(queueControl);
                SetControlSize(queueControl);
                queueControl.OnCancel += MainForm_OnCancel;
                queueControl.OnRefresh += MainForm_OnRefresh;
                queueControl.OnChangeStatus += MainForm_OnChangeStatus;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (queueControl != null)
                {
                    ControlHelper.ResumeDrawing(queueControl);
                }
            }
        }

        private void ShowTopic(TopicDescription topic, string path)
        {
            HandleTopicControl topicControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;
                topicControl = new HandleTopicControl(WriteToLog, serviceBusHelper, topic, path);
                topicControl.SuspendDrawing();
                topicControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(topicControl);
                SetControlSize(topicControl);
                topicControl.OnCancel += MainForm_OnCancel;
                topicControl.OnRefresh += MainForm_OnRefresh;
                topicControl.OnChangeStatus += MainForm_OnChangeStatus;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (topicControl != null)
                {
                    topicControl.ResumeDrawing();
                }
            }
        }

        private void ShowSubscription(SubscriptionWrapper wrapper)
        {
            HandleSubscriptionControl subscriptionControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;
                subscriptionControl = new HandleSubscriptionControl(WriteToLog, serviceBusHelper, wrapper);
                subscriptionControl.SuspendDrawing();
                subscriptionControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(subscriptionControl);
                SetControlSize(subscriptionControl);
                subscriptionControl.OnCancel += MainForm_OnCancel;
                subscriptionControl.OnRefresh += MainForm_OnRefresh;
                subscriptionControl.OnChangeStatus += MainForm_OnChangeStatus;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (subscriptionControl != null)
                {
                    subscriptionControl.ResumeDrawing();
                }
            }
        }

        private void ShowRelay(RelayDescription relayService, string path)
        {
            HandleRelayControl relayServiceControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;
                relayServiceControl = new HandleRelayControl(WriteToLog, serviceBusHelper, relayService, path);
                relayServiceControl.SuspendDrawing();
                relayServiceControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(relayServiceControl);
                SetControlSize(relayServiceControl);
                relayServiceControl.OnCancel += MainForm_OnCancel;
                relayServiceControl.OnRefresh += MainForm_OnRefresh;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (relayServiceControl != null)
                {
                    relayServiceControl.ResumeDrawing();
                }
            }
        }

        private void ShowRule(RuleWrapper wrapper, bool? isFirstRule)
        {
            HandleRuleControl ruleControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;
                ruleControl = new HandleRuleControl(WriteToLog, serviceBusHelper, wrapper, isFirstRule);
                ruleControl.SuspendDrawing();
                ruleControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(ruleControl);
                SetControlSize(ruleControl);
                ruleControl.OnCancel += MainForm_OnCancel;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (ruleControl != null)
                {
                    ruleControl.ResumeDrawing();
                }
            }
        }

        private void ShowEventHub(EventHubDescription eventHub)
        {
            HandleEventHubControl eventHubControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;
                eventHubControl = new HandleEventHubControl(WriteToLog, serviceBusHelper, eventHub);
                eventHubControl.SuspendDrawing();
                eventHubControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(eventHubControl);
                SetControlSize(eventHubControl);
                eventHubControl.OnCancel += MainForm_OnCancel;
                eventHubControl.OnRefresh += MainForm_OnRefresh;
                eventHubControl.OnChangeStatus += MainForm_OnChangeStatus;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (eventHubControl != null)
                {
                    eventHubControl.ResumeDrawing();
                }
            }
        }

        private void ShowPartition(PartitionDescription partition)
        {
            HandlePartitionControl partitionControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;

                if (string.IsNullOrWhiteSpace(partition.LastEnqueuedOffset))
                {
                    var consumerGroup = serviceBusTreeView.SelectedNode.Parent.Parent.Tag as ConsumerGroupDescription;
                    var consumerGroupName = consumerGroup != null ? consumerGroup.Name : null;
                    partition = serviceBusHelper.GetPartition(partition.EventHubPath,
                                                              consumerGroupName,
                                                              partition.PartitionId);
                }
                partitionControl = new HandlePartitionControl(WriteToLog, serviceBusHelper, partition);
                partitionControl.SuspendDrawing();
                partitionControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(partitionControl);
                partitionControl.OnRefresh += MainForm_OnRefresh;
                SetControlSize(partitionControl);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (partitionControl != null)
                {
                    partitionControl.ResumeDrawing();
                }
            }
        }

        private void ShowConsumerGroup(ConsumerGroupDescription notificationHub, string eventHubname)
        {
            HandleConsumerGroupControl notificationHubControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;
                notificationHubControl = new HandleConsumerGroupControl(WriteToLog, serviceBusHelper, notificationHub, eventHubname);
                notificationHubControl.SuspendDrawing();
                notificationHubControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(notificationHubControl);
                SetControlSize(notificationHubControl);
                notificationHubControl.OnCancel += MainForm_OnCancel;
                notificationHubControl.OnRefresh += MainForm_OnRefresh;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (notificationHubControl != null)
                {
                    notificationHubControl.ResumeDrawing();
                }
            }
        }

        private void ShowNotificationHub(NotificationHubDescription notificationHub)
        {
            HandleNotificationHubControl notificationHubControl = null;

            try
            {
                panelMain.SuspendDrawing();
                foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                {
                    userControl.Dispose();
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;
                notificationHubControl = new HandleNotificationHubControl(WriteToLog, serviceBusHelper, notificationHub);
                notificationHubControl.SuspendDrawing();
                notificationHubControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                panelMain.Controls.Add(notificationHubControl);
                SetControlSize(notificationHubControl);
                notificationHubControl.OnCancel += MainForm_OnCancel;
                notificationHubControl.OnRefresh += MainForm_OnRefresh;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                panelMain.ResumeDrawing();
                if (notificationHubControl != null)
                {
                    notificationHubControl.ResumeDrawing();
                }
            }
        }

        private void TestQueue(QueueDescription queueDescription, bool sdi)
        {
            if (sdi)
            {
                TestQueueControl queueControl = null;

                try
                {
                    panelMain.SuspendDrawing();
                    foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                    {
                        userControl.Dispose();
                    }
                    panelMain.Controls.Clear();
                    panelMain.BackColor = SystemColors.GradientInactiveCaption;
                    queueControl = new TestQueueControl(this,
                                                        WriteToLog,
                                                        StopLog,
                                                        StartLog,
                                                        serviceBusHelper,
                                                        queueDescription);
                    queueControl.SuspendDrawing();
                    queueControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                    panelMain.Controls.Add(queueControl);
                    SetControlSize(queueControl);
                    queueControl.OnCancel += MainForm_OnCancel;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                finally
                {
                    panelMain.ResumeDrawing();
                    if (queueControl != null)
                    {
                        queueControl.ResumeDrawing();
                    }
                }
            }
            else
            {
                var form = new ContainerForm(serviceBusHelper, this, FormTypeEnum.Test, queueDescription);
                form.Show();
            }
        }

        private void TestTopic(TopicDescription topicDescription, List<SubscriptionDescription> subscriptionList, bool sdi)
        {
            if (sdi)
            {
                TestTopicControl topicControl = null;

                try
                {
                    panelMain.SuspendDrawing();
                    foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                    {
                        userControl.Dispose();
                    }
                    panelMain.Controls.Clear();
                    panelMain.BackColor = SystemColors.GradientInactiveCaption;
                    topicControl = new TestTopicControl(this,
                                                        WriteToLog,
                                                        StopLog,
                                                        StartLog,
                                                        serviceBusHelper,
                                                        topicDescription,
                                                        subscriptionList);
                    topicControl.SuspendDrawing();
                    topicControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                    panelMain.Controls.Add(topicControl);
                    SetControlSize(topicControl);
                    topicControl.OnCancel += MainForm_OnCancel;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                finally
                {
                    panelMain.ResumeDrawing();
                    if (topicControl != null)
                    {
                        topicControl.ResumeDrawing();
                    }
                }
            }
            else
            {
                var form = new ContainerForm(serviceBusHelper, this, FormTypeEnum.Test, topicDescription, subscriptionList);
                form.Show();
            }
        }

        private void TestSubscription(SubscriptionWrapper subscriptionWrapper, bool sdi)
        {
            if (sdi)
            {
                TestSubscriptionControl subscriptionControl = null;

                try
                {
                    panelMain.SuspendDrawing();
                    foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                    {
                        userControl.Dispose();
                    }
                    panelMain.Controls.Clear();
                    panelMain.BackColor = SystemColors.GradientInactiveCaption;
                    subscriptionControl = new TestSubscriptionControl(this,
                                                                      WriteToLog,
                                                                      StopLog,
                                                                      StartLog,
                                                                      serviceBusHelper,
                                                                      subscriptionWrapper);
                    subscriptionControl.SuspendDrawing();
                    subscriptionControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                    panelMain.Controls.Add(subscriptionControl);
                    SetControlSize(subscriptionControl);
                    subscriptionControl.OnCancel += MainForm_OnCancel;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                finally
                {
                    panelMain.ResumeDrawing();
                    if (subscriptionControl != null)
                    {
                        subscriptionControl.ResumeDrawing();
                    }
                }
            }
            else
            {
                var form = new ContainerForm(serviceBusHelper, this, FormTypeEnum.Test, subscriptionWrapper);
                form.Show();
            }
        }

        private void TestRelay(RelayDescription relayDescription, bool sdi)
        {
            if (sdi)
            {
                TestRelayControl relayServiceControl = null;

                try
                {
                    panelMain.SuspendDrawing();
                    foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                    {
                        userControl.Dispose();
                    }
                    panelMain.Controls.Clear();
                    panelMain.BackColor = SystemColors.GradientInactiveCaption;
                    relayServiceControl = new TestRelayControl(this,
                                                               WriteToLog,
                                                               StopLog,
                                                               StartLog,
                                                               relayDescription,
                                                               serviceBusHelper);
                    relayServiceControl.SuspendDrawing();
                    relayServiceControl.Location = new Point(1, panelLog.HeaderHeight + 1);
                    panelMain.Controls.Add(relayServiceControl);
                    SetControlSize(relayServiceControl);
                    relayServiceControl.OnCancel += MainForm_OnCancel;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                finally
                {
                    panelMain.ResumeDrawing();
                    if (relayServiceControl != null)
                    {
                        relayServiceControl.ResumeDrawing();
                    }
                }
            }
            else
            {
                var form = new ContainerForm(serviceBusHelper, this, relayDescription);
                form.Show();
            }
        }

        //private void TestRelay(RelayWrapper relayDescription, bool sdi)
        //{
        //    if (sdi)
        //    {
        //        TestRelayControl relayServiceControl = null;

        //        try
        //        {
        //            panelMain.SuspendDrawing();
        //            foreach (var userControl in panelMain.Controls.OfType<UserControl>())
        //            {
        //                userControl.Dispose();
        //            }
        //            panelMain.Controls.Clear();
        //            panelMain.BackColor = SystemColors.GradientInactiveCaption;
        //            relayServiceControl = new TestRelayControl(this,
        //                                                              WriteToLog,
        //                                                              StopLog,
        //                                                              StartLog,
        //                                                              relayDescription,
        //                                                              serviceBusHelper);
        //            relayServiceControl.SuspendDrawing();
        //            relayServiceControl.Location = new Point(1, panelLog.HeaderHeight + 1);
        //            panelMain.Controls.Add(relayServiceControl);
        //            SetControlSize(relayServiceControl);
        //            relayServiceControl.OnCancel += MainForm_OnCancel;
        //        }
        //        catch (Exception ex)
        //        {
        //            HandleException(ex);
        //        }
        //        finally
        //        {
        //            panelMain.ResumeDrawing();
        //            if (relayServiceControl != null)
        //            {
        //                relayServiceControl.ResumeDrawing();
        //            }
        //        }
        //    }
        //}

        private async void ExportEntities(List<IExtensibleDataObject> list, string entityName, string entityType)
        {
            var xml = await serviceBusHelper.ExportEntities(list);
            var path = entityType == null ?
                       CreateFileName(string.Format(EntitiesFileNameFormat, serviceBusHelper.Namespace, entityName)) :
                       CreateFileName(string.Format(EntityFileNameFormat, serviceBusHelper.Namespace, entityName, entityType));
            WriteToLog(string.Format(EntitiesExported, SaveEntityToFile(xml, path)));
        }

        private void copyEntityUrl_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem &&
                serviceBusHelper != null)
            {
                var toolStripMenuItem = sender as ToolStripMenuItem;
                Uri uri = null;
                switch (toolStripMenuItem.Name)
                {
                    case "copyQueueUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                        {
                            uri = serviceBusHelper.GetQueueUri(((QueueDescription)serviceBusTreeView.SelectedNode.Tag).Path);
                        }
                        break;
                    case "copyQueueDeadletterQueueUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                        {
                            uri = serviceBusHelper.GetQueueDeadLetterQueueUri(((QueueDescription)serviceBusTreeView.SelectedNode.Tag).Path);
                        }
                        break;
                    case "copyTopicUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                        {
                            uri = serviceBusHelper.GetTopicUri(((TopicDescription)serviceBusTreeView.SelectedNode.Tag).Path);
                        }
                        break;
                    case "copyRelayUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is RelayDescription)
                        {
                            uri = serviceBusHelper.GetRelayUri((RelayDescription)serviceBusTreeView.SelectedNode.Tag);
                        }
                        //if (serviceBusTreeView.SelectedNode != null &&
                        //    serviceBusTreeView.SelectedNode.Tag is RelayWrapper)
                        //{
                        //    var wrapper = serviceBusTreeView.SelectedNode.Tag as RelayWrapper;
                        //    if (wrapper.Uri != null)
                        //    {
                        //        uri = wrapper.Uri;
                        //    }
                        //}
                        break;
                    case "copySubscriptionUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                        {
                            var wrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                            if (wrapper.SubscriptionDescription != null)
                            {
                                uri =
                                    serviceBusHelper.GetSubscriptionUri(wrapper.SubscriptionDescription.TopicPath,
                                                                        wrapper.SubscriptionDescription.Name);
                            }
                        }
                        break;
                    case "copySubscriptionDeadletterSubscriptionUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                        {
                            var wrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                            if (wrapper.SubscriptionDescription != null)
                            {
                                uri =
                                    serviceBusHelper.GetSubscriptionDeadLetterQueueUri(wrapper.SubscriptionDescription.TopicPath,
                                                                                       wrapper.SubscriptionDescription.Name);
                            }
                        }
                        break;
                    case "copyUrlNotificationHubMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is NotificationHubDescription)
                        {
                            uri = serviceBusHelper.GetNotificationHubUri(((NotificationHubDescription)serviceBusTreeView.SelectedNode.Tag).Path);
                        }
                        break;
                    case "copyEventHubUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is EventHubDescription)
                        {
                            uri = serviceBusHelper.GetEventHubUri(((EventHubDescription)serviceBusTreeView.SelectedNode.Tag).Path);
                        }
                        break;
                    case "copyConsumerGroupUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is ConsumerGroupDescription)
                        {
                            var notificationHubDescription = serviceBusTreeView.SelectedNode.Tag as ConsumerGroupDescription;
                            uri = serviceBusHelper.GetConsumerGroupUri(notificationHubDescription.EventHubPath, notificationHubDescription.Name);
                        }
                        break;
                    case "copyPartitionUrlMenuItem":
                        if (serviceBusTreeView.SelectedNode != null &&
                            serviceBusTreeView.SelectedNode.Tag is PartitionDescription)
                        {
                            var partitionDescription = serviceBusTreeView.SelectedNode.Tag as PartitionDescription;
                            var node = serviceBusTreeView.SelectedNode.Parent.Parent;
                            if (node != null && node.Tag is ConsumerGroupDescription)
                            {
                                var notificationHubDescription = node.Tag as ConsumerGroupDescription;
                                uri = serviceBusHelper.GetPartitionUri(partitionDescription.EventHubPath, notificationHubDescription.Name, partitionDescription.PartitionId);
                            }
                        }
                        break;
                }
                if (uri != null &&
                    !string.IsNullOrWhiteSpace(uri.AbsoluteUri))
                {
                    var url = uri.AbsoluteUri[uri.AbsoluteUri.Length - 1] == '/'
                                  ? uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.Length - 1)
                                  : uri.AbsoluteUri;
                    using (var form = new ClipboardForm(url))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            Clipboard.SetText(url);
                        }
                    }
                }
            }
        }

        private List<ToolStripItem> CloneItems(ToolStripItemCollection collection)
        {
            var list = new List<ToolStripItem>();
            if (collection == null)
            {
                return list;
            }
            list.Add(new ToolStripSeparator());
            var enumerable = collection.Cast<ToolStripItem>();
            foreach (var toolStripItem in enumerable)
            {
                if (toolStripItem is ToolStripSeparator)
                {
                    list.Add(new ToolStripSeparator());
                }
                else
                {
                    var toolStripMenuItem = toolStripItem as ToolStripMenuItem;
                    if (toolStripMenuItem == null)
                    {
                        continue;
                    }
                    var item = new ToolStripMenuItem
                    {
                        Name = toolStripMenuItem.Name,
                        Size = toolStripMenuItem.Size,
                        Text = toolStripMenuItem.Text,
                        ToolTipText = toolStripMenuItem.ToolTipText,
                        ShortcutKeys = toolStripMenuItem.ShortcutKeys,
                        ShortcutKeyDisplayString = toolStripMenuItem.ShortcutKeyDisplayString,
                        ShowShortcutKeys = toolStripMenuItem.ShowShortcutKeys
                    };
                    var events = (EventHandlerList)eventsPropertyInfo.GetValue(toolStripMenuItem, null);
                    var secret = eventClickFieldInfo.GetValue(null);
                    var handlers = events[secret];
                    events = (EventHandlerList)eventsPropertyInfo.GetValue(item, null);
                    events.AddHandler(secret, handlers);
                    list.Add(item);
                }
            }
            return list;
        }

        private void AddImportAndSeparatorMenuItems(ICollection<ToolStripItem> list)
        {
            if (list == null)
            {
                return;
            }
            if (list.Count > 0)
            {
                list.Add(new ToolStripSeparator());
            }
            var item = new ToolStripMenuItem
            {
                Name = ImportToolStripMenuItemName,
                Size = new Size(154, 22),
                Text = ImportToolStripMenuItemText,
                ToolTipText = ImportToolStripMenuItemToolTipText
            };

            item.Click += importEntity_Click;
            list.Add(item);
        }

        private static string CreateFileName(string text)
        {
            // Check for empty string.
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            var parts = text.Split('.');

            return $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parts[0].Replace(' ', '_').Replace('/', '_'))}.{parts[1]}";
        }

        private void DeleteNode(string path, TreeNode node)
        {
            if (string.IsNullOrWhiteSpace(path) || node == null)
            {
                return;
            }
            var segments = path.Split('/');
            if (segments.Length > 1)
            {
                var index = path.IndexOf('/');
                if (index >= 0 &&
                    !string.IsNullOrWhiteSpace(segments[0]) &&
                    node.Nodes.ContainsKey(segments[0]))
                {
                    var entityNode = node.Nodes[segments[0]];
                    DeleteNode(path.Substring(index + 1), entityNode);
                    if (entityNode.Nodes.Count == 0)
                    {
                        node.Nodes.RemoveByKey(segments[0]);
                    }
                }
            }
            else
            {
                if (node.Nodes.ContainsKey(path))
                {
                    node.Nodes.RemoveByKey(path);
                }
            }
        }

        private TreeNode FindNode(string path, TreeNode node)
        {
            if (string.IsNullOrWhiteSpace(path) || node == null)
            {
                return null;
            }
            var segments = path.Split('/');
            if (segments.Length > 1)
            {
                var index = path.IndexOf('/');
                if (index >= 0 &&
                    !string.IsNullOrWhiteSpace(segments[0]) &&
                    node.Nodes.ContainsKey(segments[0]))
                {
                    var entityNode = node.Nodes[segments[0]];
                    return FindNode(path.Substring(index + 1), entityNode);
                }
            }
            else
            {
                if (node.Nodes.ContainsKey(path))
                {
                    return node.Nodes[path];
                }
            }
            return null;
        }

        private TreeNode CreateNode(string path,
                                    object tag,
                                    TreeNode node,
                                    bool log)
        {
            if (string.IsNullOrWhiteSpace(path) || node == null)
            {
                return null;
            }
            var segments = path.Split('/');
            var entityNode = node;
            var currentUrl = serviceBusHelper.NamespaceUri.AbsoluteUri;
            for (var i = 0; i < segments.Length; i++)
            {
                if (i < segments.Length - 1)
                {
                    currentUrl = currentUrl[currentUrl.Length - 1] == '/' ?
                                 currentUrl + segments[i] :
                                 string.Format(UrlSegmentFormat, currentUrl, segments[i]);
                }
                if (entityNode.Nodes.ContainsKey(segments[i]))
                {
                    entityNode = entityNode.Nodes[segments[i]];
                }
                else
                {
                    if (i < segments.Length - 1)
                    {
                        entityNode = entityNode.Nodes.Add(segments[i],
                                                          segments[i],
                                                          UrlSegmentIconIndex,
                                                          UrlSegmentIconIndex);
                        var entityType = EntityType.Queue;
                        if (tag is QueueDescription)
                        {
                            entityNode.ContextMenuStrip = queueFolderContextMenuStrip;
                        }
                        if (tag is TopicDescription)
                        {
                            entityNode.ContextMenuStrip = topicFolderContextMenuStrip;
                            entityType = EntityType.Topic;
                        }
                        if (tag is RelayDescription)
                        {
                            entityNode.ContextMenuStrip = relayServiceFolderContextMenuStrip;
                            entityType = EntityType.Relay;
                        }
                        entityNode.Tag = new UrlSegmentWrapper(entityType, new Uri(currentUrl));
                    }
                    else
                    {
                        if (tag is QueueDescription)
                        {
                            var queueDescription = tag as QueueDescription;
                            entityNode = entityNode.Nodes.Add(segments[i],
                                                              showMessageCount ?
                                                              string.Format(NameMessageCountFormat,
                                                                            segments[i],
                                                                            queueDescription.MessageCountDetails.ActiveMessageCount,
                                                                            queueDescription.MessageCountDetails.DeadLetterMessageCount,
                                                                            queueDescription.MessageCountDetails.TransferDeadLetterMessageCount) :
                                                              segments[i],
                                                              queueDescription.Status == EntityStatus.Active ? QueueIconIndex : GreyQueueIconIndex,
                                                              queueDescription.Status == EntityStatus.Active ? QueueIconIndex : GreyQueueIconIndex);
                            entityNode.ContextMenuStrip = queueContextMenuStrip;
                            entityNode.Tag = tag;

                            if (log)
                            {
                                WriteToLog(string.Format(CultureInfo.CurrentCulture, QueueRetrievedFormat, queueDescription.Path), false);
                            }
                            return entityNode;
                        }
                        if (tag is TopicDescription)
                        {
                            var topicDescription = tag as TopicDescription;
                            entityNode = entityNode.Nodes.Add(segments[i],
                                                              segments[i],
                                                              topicDescription.Status == EntityStatus.Active ? TopicIconIndex : GreyTopicIconIndex,
                                                              topicDescription.Status == EntityStatus.Active ? TopicIconIndex : GreyTopicIconIndex);
                            entityNode.ContextMenuStrip = topicContextMenuStrip;
                            entityNode.Tag = tag;

                            if (log)
                            {
                                WriteToLog(string.Format(CultureInfo.CurrentCulture, TopicRetrievedFormat, topicDescription.Path), false);
                            }
                            return entityNode;
                        }
                        if (tag is RelayDescription)
                        {
                            var relayDescription = tag as RelayDescription;
                            entityNode = entityNode.Nodes.Add(segments[i],
                                                              segments[i],
                                                              RelayLeafIconIndex,
                                                              RelayLeafIconIndex);
                            entityNode.ContextMenuStrip = relayContextMenuStrip;
                            entityNode.Tag = tag;

                            if (log)
                            {
                                WriteToLog(string.Format(CultureInfo.CurrentCulture, RelayRetrievedFormat, relayDescription.Path), false);
                            }
                            return entityNode;
                        }
                        if (tag is EventHubDescription)
                        {
                            var eventHubDescription = tag as EventHubDescription;
                            entityNode = entityNode.Nodes.Add(segments[i],
                                                              segments[i],
                                                              EventHubIconIndex,
                                                              EventHubIconIndex);
                            entityNode.ContextMenuStrip = eventHubContextMenuStrip;
                            entityNode.Tag = tag;

                            if (log)
                            {
                                WriteToLog(string.Format(CultureInfo.CurrentCulture, EventHubRetrievedFormat, eventHubDescription.Path), false);
                            }
                            CreateEventHubSubTree(eventHubDescription, entityNode);
                            return entityNode;
                        }
                        if (tag is NotificationHubDescription)
                        {
                            var notificationHubDescription = tag as NotificationHubDescription;
                            entityNode = entityNode.Nodes.Add(segments[i],
                                                              segments[i],
                                                              NotificationHubIconIndex,
                                                              NotificationHubIconIndex);
                            entityNode.ContextMenuStrip = notificationHubContextMenuStrip;
                            entityNode.Tag = tag;

                            if (log)
                            {
                                WriteToLog(string.Format(CultureInfo.CurrentCulture, NotificationHubRetrievedFormat, notificationHubDescription.Path), false);
                            }
                            return entityNode;
                        }
                    }
                }
            }
            return null;
        }

        private static void GetQueueList(ICollection<string> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as QueueDescription;
            if (tag != null)
            {
                list.Add(tag.Path);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetQueueList(list, node.Nodes[i]);
            }
        }

        private static void GetQueueList(ICollection<IExtensibleDataObject> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as QueueDescription;
            if (tag != null)
            {
                list.Add(tag);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetQueueList(list, node.Nodes[i]);
            }
        }

        private static void GetTopicList(ICollection<string> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as TopicDescription;
            if (tag != null)
            {
                list.Add(tag.Path);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetTopicList(list, node.Nodes[i]);
            }
        }

        private static void GetTopicList(ICollection<IExtensibleDataObject> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as TopicDescription;
            if (tag != null)
            {
                list.Add(tag);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetTopicList(list, node.Nodes[i]);
            }
        }

        private static void GetRelayList(ICollection<string> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as RelayDescription;
            if (tag != null)
            {
                list.Add(tag.Path);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetRelayList(list, node.Nodes[i]);
            }
        }

        private static void GetRelayList(ICollection<IExtensibleDataObject> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as RelayDescription;
            if (tag != null)
            {
                list.Add(tag);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetRelayList(list, node.Nodes[i]);
            }
        }

        private static void GetEventHubList(ICollection<string> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as EventHubDescription;
            if (tag != null)
            {
                list.Add(tag.Path);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetEventHubList(list, node.Nodes[i]);
            }
        }

        private static void GetEventHubList(ICollection<IExtensibleDataObject> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as EventHubDescription;
            if (tag != null)
            {
                list.Add(tag);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetEventHubList(list, node.Nodes[i]);
            }
        }

        private static void GetConsumerGroupList(ICollection<string> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as ConsumerGroupDescription;
            if (tag != null)
            {
                list.Add(tag.Name);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetConsumerGroupList(list, node.Nodes[i]);
            }
        }

        private static void GetNotificationHubList(ICollection<string> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as NotificationHubDescription;
            if (tag != null)
            {
                list.Add(tag.Path);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetNotificationHubList(list, node.Nodes[i]);
            }
        }

        private static void GetNotificationHubList(ICollection<IExtensibleDataObject> list, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            var tag = node.Tag as NotificationHubDescription;
            if (tag != null)
            {
                list.Add(tag);
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                GetNotificationHubList(list, node.Nodes[i]);
            }
        }

        private static string FormatAbsolutePathForEdit(Uri uri)
        {
            if (uri == null)
            {
                return string.Empty;
            }
            var url = uri.AbsolutePath;
            if (url[0] == '/')
            {
                if (url.Length == 1)
                {
                    return string.Empty;
                }
                // ReSharper disable RedundantIfElseBlock
                else
                // ReSharper restore RedundantIfElseBlock
                {
                    url = url.Substring(1);
                }
            }
            if (url[url.Length - 1] != '/')
            {
                url += '/';
            }
            return url;
        }

        private static string FormatAbsolutePathForExport(Uri uri)
        {
            if (uri == null ||
                string.IsNullOrWhiteSpace(uri.AbsolutePath))
            {
                return string.Empty;
            }
            var url = uri.AbsolutePath;
            if (url[0] == '/')
            {
                if (url.Length == 1)
                {
                    return string.Empty;
                }
                // ReSharper disable RedundantIfElseBlock
                else
                // ReSharper restore RedundantIfElseBlock
                {
                    url = url.Substring(1);
                }
            }
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(url.Split('/').Aggregate((left, right) => left + '_' + right));
        }

        private void setDefaultLayouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainSplitContainer.SplitterDistance = mainSplitterDistance;
            splitContainer.SplitterDistance = splitterContainerDistance;
            lstLog.Font = new Font(lstLog.Font.FontFamily, logFontSize);
            serviceBusTreeView.Font = new Font(serviceBusTreeView.Font.FontFamily, treeViewFontSize);
        }

        private void receiveMessages_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (serviceBusTreeView.SelectedNode == null)
                {
                    return;
                }
                // queue purge was requested
                var view = sender as TreeView;
                if (view != null)
                {
                    var queueControl = panelMain.Controls[0] as HandleQueueControl;
                    if (queueControl != null)
                    {
                        var queueDescription = view.SelectedNode.Tag as QueueDescription;
                        if (queueDescription != null)
                        {
                            queueControl.PurgeMessages(Convert.ToInt32(queueDescription.MessageCount));
                        }
                        return;
                    }
                }

                var text = ((ToolStripMenuItem)sender).Text;
                var deadletter = string.Compare(text, queueReceiveDeadletterQueueMessagesMenuItem.Text, StringComparison.OrdinalIgnoreCase) == 0 ||
                                 string.Compare(text, subscriptionReceiveDeadletterQueueMessagesMenuItem.Text, StringComparison.OrdinalIgnoreCase) == 0;

                var transferDeadletter = string.Compare(text, queueReceiveTransferDeadletterQueueMessagesMenuItem.Text, StringComparison.OrdinalIgnoreCase) == 0 ||
                                         string.Compare(text, subscriptionReceiveTransferDeadletterQueueMessagesMenuItem.Text, StringComparison.OrdinalIgnoreCase) == 0;

                // Queue Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                {
                    var control = panelMain.Controls[0] as HandleQueueControl;
                    if (control != null)
                    {
                        if (deadletter)
                        {
                            control.GetDeadletterMessages();
                        }
                        else if (transferDeadletter)
                        {
                            control.GetTransferDeadletterMessages();
                        }
                        else
                        {
                            control.GetMessages();
                        }
                    }
                }

                // Subscription Node
                if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                {
                    var control = panelMain.Controls[0] as HandleSubscriptionControl;
                    if (control != null)
                    {
                        if (deadletter)
                        {
                            control.GetDeadletterMessages();
                        }
                        else if (transferDeadletter)
                        {
                            control.GetTransferDeadletterMessages();
                        }
                        else
                        {
                            control.GetMessages();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode != null)
                {
                    ContainerForm form = null;
                    if (serviceBusTreeView.SelectedNode.Tag == null)
                    {
                        return;
                    }
                    // Queue Node
                    if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                    {
                        var queueDescription = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                        form = new ContainerForm(serviceBusHelper, this, FormTypeEnum.Send, queueDescription);
                    }

                    // Topic Node
                    if (serviceBusTreeView.SelectedNode.Tag is TopicDescription)
                    {
                        var topicDescription = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                        var subscriptionList = new List<SubscriptionDescription>();
                        if (serviceBusTreeView.SelectedNode.Nodes.ContainsKey(SubscriptionEntities))
                        {
                            var subscriptionsNode = serviceBusTreeView.SelectedNode.Nodes[SubscriptionEntities];
                            if (subscriptionsNode != null &&
                                subscriptionsNode.Nodes.Count > 0)
                            {
                                for (var i = 0; i < subscriptionsNode.Nodes.Count; i++)
                                {
                                    var wrapper = subscriptionsNode.Nodes[i].Tag as SubscriptionWrapper;
                                    if (wrapper != null &&
                                        wrapper.SubscriptionDescription != null)
                                    {
                                        subscriptionList.Add(wrapper.SubscriptionDescription);
                                    }
                                }
                            }
                        }

                        form = new ContainerForm(serviceBusHelper, this, FormTypeEnum.Send, topicDescription, subscriptionList);
                    }

                    // EventHub Node
                    if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription)
                    {
                        var eventHubDescription = serviceBusTreeView.SelectedNode.Tag as EventHubDescription;
                        form = new ContainerForm(serviceBusHelper, this, eventHubDescription);
                    }

                    // Event Hub Partition
                    if (serviceBusTreeView.SelectedNode.Tag is PartitionDescription)
                    {
                        var partitionDescription = serviceBusTreeView.SelectedNode.Tag as PartitionDescription;
                        try
                        {
                            var eventHubNode = serviceBusTreeView.SelectedNode.Parent.Parent.Parent.Parent;
                            if (eventHubNode != null && eventHubNode.Tag is EventHubDescription)
                            {
                                var eventHubDescription = eventHubNode.Tag as EventHubDescription;
                                form = new ContainerForm(serviceBusHelper, this, eventHubDescription, partitionDescription);
                            }
                        }
                        // ReSharper disable once EmptyGeneralCatchClause
                        catch (Exception)
                        {
                        }
                    }

                    if (form != null)
                    {
                        form.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationTokenSource.Cancel(false);
            if (saveMessageToFile)
            {
                MessageAndPropertiesHelper.WriteMessage(messageText);
                MessageAndPropertiesHelper.WriteMessage(relayMessageText);
            }
            if (savePropertiesToFile)
            {
                MessageAndPropertiesHelper.WriteProperties();
            }
            if (saveCheckpointsToFile)
            {
                EventProcessorCheckpointHelper.WriteCheckpoints();
            }
        }

        private void filterEntity_Click(object sender, EventArgs e)
        {
            var queueListNode = FindNode(QueueEntities, rootNode);
            var topicListNode = FindNode(TopicEntities, rootNode);

            // Queues
            if (serviceBusTreeView.SelectedNode == queueListNode)
            {
                var previousFilter = FilterExpressionHelper.QueueFilterExpression;
                using (var form = new FilterForm(QueueEntity, FilterExpressionHelper.QueueFilterExpression))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (previousFilter != form.FilterExpression)
                        {
                            FilterExpressionHelper.QueueFilterExpression = form.FilterExpression;
                            GetEntities(EntityType.Queue);
                        }
                    }
                }
                return;
            }
            // Topics
            if (serviceBusTreeView.SelectedNode == topicListNode)
            {
                var previousFilter = FilterExpressionHelper.TopicFilterExpression;
                using (var form = new FilterForm(TopicEntity, FilterExpressionHelper.TopicFilterExpression))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (previousFilter != form.FilterExpression)
                        {
                            FilterExpressionHelper.TopicFilterExpression = form.FilterExpression;
                            GetEntities(EntityType.Topic);
                        }
                    }
                }
                return;
            }
            // Subscriptions
            if (serviceBusTreeView.SelectedNode.Text == SubscriptionEntities ||
                serviceBusTreeView.SelectedNode.Text == FilteredSubscriptionEntities)
            {
                var wrapper = serviceBusTreeView.SelectedNode.Tag as SubscriptionWrapper;
                var previousFilter = wrapper == null ? null : wrapper.Filter;
                using (var form = new FilterForm(SubscriptionEntity, previousFilter))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (previousFilter != form.FilterExpression)
                        {
                            if (wrapper != null)
                            {
                                wrapper.Filter = form.FilterExpression;
                            }
                            refreshEntity_Click(null, null);
                        }
                    }
                }
            }
        }

        private void getMessageSessions_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode == null ||
                    serviceBusTreeView.SelectedNode.Tag == null)
                {
                    return;
                }
                // Queue Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                {
                    var control = panelMain.Controls[0] as HandleQueueControl;
                    control?.GetMessageSessions();
                    return;
                }

                // Subscription Node
                if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                {
                    var control = panelMain.Controls[0] as HandleSubscriptionControl;
                    control?.GetMessageSessions();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void getRegistrationsNotificationHubMenuItem_Click(object sender, EventArgs e)
        {
            if (!(serviceBusTreeView.SelectedNode.Tag is NotificationHubDescription))
            {
                return;
            }
            var control = panelMain.Controls[0] as HandleNotificationHubControl;
            if (control != null)
            {
                control.GetRegistrations(true, null);
            }
        }

        private void lstLog_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Control || e.KeyCode != Keys.C)
                {
                    return;
                }
                var builder = new StringBuilder();
                foreach (var item in lstLog.SelectedItems)
                {
                    builder.AppendLine(item.ToString());
                }
                if (builder.Length > 0)
                {
                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var builder = new StringBuilder();
                foreach (var item in lstLog.Items)
                {
                    builder.AppendLine(item.ToString());
                }
                if (builder.Length > 0)
                {
                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void copySelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var builder = new StringBuilder();
                foreach (var item in lstLog.SelectedItems)
                {
                    builder.AppendLine(item.ToString());
                }
                if (builder.Length > 0)
                {
                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private void clearSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var builder = new StringBuilder();
                var list = new List<object>();
                list.AddRange(lstLog.SelectedItems.Cast<object>().ToArray());
                foreach (var item in list)
                {
                    lstLog.Items.Remove(item);
                }
                if (builder.Length > 0)
                {
                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLog(true);
        }

        private void saveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLog(false);
        }

        private void SaveLog(bool all)
        {
            try
            {
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = SaveAsExtension;
                saveFileDialog.Filter = SaveAsFilter;
                saveFileDialog.FileName = string.Format(LogFileNameFormat, DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
                if (saveFileDialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    if (all)
                    {
                        foreach (var t in lstLog.Items)
                        {
                            writer.WriteLine(t as string);
                        }
                    }
                    else
                    {
                        foreach (var t in lstLog.SelectedItems)
                        {
                            writer.WriteLine(t.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Refresh();
                if (string.IsNullOrWhiteSpace(argumentName) || string.IsNullOrWhiteSpace(argumentValue))
                {
                    return;
                }
                if (string.Compare(argumentName, "/n", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                    string.Compare(argumentName, "-n", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    var item = serviceBusHelper.ServiceBusNamespaces.FirstOrDefault(s => string.Compare(s.Key, argumentValue, StringComparison.InvariantCultureIgnoreCase) == 0);
                    if (item.Key == null && item.Value == null)
                    {
                        WriteToLog(string.Format(NoNamespaceWithKeyMessageFormat, argumentValue));
                        return;
                    }
                    var ns = item.Value;
                    if (ns != null)
                    {
                        var serviceBusNamespace = GetServiceBusNamespace(item.Key, ns.ConnectionString);
                        serviceBusHelper.Connect(serviceBusNamespace);
                    }
                }
                if (string.Compare(argumentName, "/c", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                    string.Compare(argumentName, "-c", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    var serviceBusNamespace = GetServiceBusNamespace("Manual", argumentValue);
                    serviceBusHelper.Connect(serviceBusNamespace);
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.Window;
                GetEntities(EntityType.All);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void getPartitionDataMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode == null ||
                    serviceBusTreeView.SelectedNode.Tag == null)
                {
                    return;
                }
                
                if (serviceBusTreeView.SelectedNode.Tag is ConsumerGroupDescription)
                {
                    var control = panelMain.Controls[0] as HandleConsumerGroupControl;
                    control?.GetPartitions();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void createIoTHubListenerMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var parameterForm = new ParameterForm("Enter IoT Hub Connection String and Consumer Group", 
                                                             new List<string> {"IoT Hub Connection String", "Endpoint", "Consumer Group"}, 
                                                             new List<string>{null, "messages/events", "$Default" },
                                                             new List<bool>{false, false, false}))
                {
                    if (parameterForm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(parameterForm.ParameterValues[0]))
                    {
                        WriteToLog("The IoT Hub Connection string parameter cannot be null.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(parameterForm.ParameterValues[1]))
                    {
                        WriteToLog("The Endpoint parameter cannot be null.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(parameterForm.ParameterValues[2]))
                    {
                        WriteToLog("The Consumer Group parameter cannot be null.");
                        return;
                    }
                    var form = new ContainerForm(this, 
                                                 parameterForm.ParameterValues[0], 
                                                 parameterForm.ParameterValues[1], 
                                                 parameterForm.ParameterValues[2], 
                                                 true);
                    form.Show();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void createEventHubListenerMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var parameterForm = new ParameterForm("Enter Event Hub Connection String, Event Hub name and Consumer Group",
                                                             new List<string> { "Event Hub Connection String", "Event Hub Path", "Consumer Group" },
                                                             new List<string> { null, null, "$Default" },
                                                             new List<bool> { false, false, false }))
                {
                    if (parameterForm.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(parameterForm.ParameterValues[0]))
                    {
                        WriteToLog("The Event Hub Connection string parameter cannot be null.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(parameterForm.ParameterValues[1]))
                    {
                        WriteToLog("The Event Hub Path string parameter cannot be null.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(parameterForm.ParameterValues[2]))
                    {
                        WriteToLog("The Consumer Group parameter cannot be null.");
                        return;
                    }
                    var form = new ContainerForm(this, parameterForm.ParameterValues[0],
                        parameterForm.ParameterValues[1], parameterForm.ParameterValues[2], false);
                    form.Show();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        

        private async void purgeMessages_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode == null)
                {
                    return;
                }

                // Queue Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                {
                    var control = panelMain.Controls[0] as HandleQueueControl;
                    if (control != null)
                    {
                        await control.PurgeMessagesAsync();
                    }
                }

                // Subscription Node
                if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                {
                    var control = panelMain.Controls[0] as HandleSubscriptionControl;
                    if (control != null)
                    {
                        await control.PurgeMessagesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void purgeDeadletterQueueMessages_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusTreeView.SelectedNode == null)
                {
                    return;
                }

                // Queue Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription)
                {
                    var control = panelMain.Controls[0] as HandleQueueControl;
                    if (control != null)
                    {
                        await control.PurgeDeadletterQueueMessagesAsync();
                    }
                }

                // Subscription Node
                if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                {
                    var control = panelMain.Controls[0] as HandleSubscriptionControl;
                    if (control != null)
                    {
                        await control.PurgeDeadletterQueueMessagesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Text.EndsWith("1.0.0"))
            {
                return;
            }
            version = VersionHelper.RetrieveLatestReleaseFromGitHubAsync().Result;
            if (!string.IsNullOrWhiteSpace(version))
            {
                Text = $"Service Bus Explorer {version}";
            }
        }

        #endregion
    }
}
