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
using Microsoft.Azure.NotificationHubs;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.ServiceBus.Helpers;
using ServiceBusExplorer.UIHelpers;
using ServiceBusExplorer.Utilities.Helpers;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace ServiceBusExplorer.Forms
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
        private const string PartitionFormat = "{0,2:00}";

        //***************************
        // Messages
        //***************************
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
        private const string EnableTopic = "Enable Topic";
        private const string DisableTopic = "Disable Topic";
        private const string EnableSubscription = "Enable Subscription";
        private const string DisableSubscription = "Disable Subscription";
        private const string EnableEventHub = "Enable Event Hub";
        private const string DisableEventHub = "Disable Event Hub";
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
        private const string SubscriptionEntities = "Subscriptions";
        private const string PartitionEntities = "Partitions";
        private const string ConsumerGroupEntities = "Consumer Groups";
        private const string FilteredQueueEntities = "Queues (Filtered)";
        private const string FilteredTopicEntities = "Topics (Filtered)";
        private const string FilteredSubscriptionEntities = "Subscriptions (Filtered)";
        private const string RuleEntities = "Rules";
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
        private const string ImportToolStripMenuItemName = "importEntityMenuItem2";
        private const string ImportToolStripMenuItemText = "Import Entities";
        private const string ImportToolStripMenuItemToolTipText = "Import entity definition from file.";
        private const string EventClick = "EventClick";
        private const string EventsProperty = "Events";
        private const string ChangeStatusTopicMenuItem = "changeStatusTopicMenuItem";
        private const string ChangeStatusSubscriptionMenuItem = "changeStatusSubscriptionMenuItem";
        private const string ChangeStatusEventHubMenuItem = "changeStatusEventHubMenuItem";
        private const string DefaultConsumerGroupName = "$Default";

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
        private string messageFile;
        private bool importing;
        private readonly int mainSplitterDistance;
        private readonly int splitterContainerDistance;
        private ConfigFileUse configFileUse;
        private decimal treeViewFontSize;
        private decimal logFontSize;
        private bool showMessageCount = true;
        private bool saveMessageToFile = true;
        private bool savePropertiesToFile = true;
        private bool saveCheckpointsToFile = true;
        private readonly List<Tuple<string, string>> fileNames = new List<Tuple<string, string>>();
        private readonly string argumentName;
        private readonly string argumentValue;
        private string messageBodyType = BodyType.Stream.ToString();
        private BlockingCollection<string> logCollection = new BlockingCollection<string>();
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private Task logTask;
        private List<TreeNode> treeNodesToLazyLoad = new List<TreeNode>();
        #endregion

        #region Private Static Fields
        private static MainForm mainSingletonMainForm;
        private static IWebProxy initialDefaultWebProxy;

        private static IDictionary<string, Func<MessageCountDetails, long>> messageCountRetriever = new Dictionary<string, Func<MessageCountDetails, long>>
        {
            { Constants.ActiveMessages, mcd => mcd.ActiveMessageCount },
            { Constants.DeadLetterMessages, mcd => mcd.DeadLetterMessageCount },
            { Constants.ScheduledMessages, mcd => mcd.ScheduledMessageCount },
            { Constants.TransferMessages, mcd => mcd.TransferMessageCount },
            { Constants.TransferDeadLetterMessages, mcd => mcd.TransferDeadLetterMessageCount },
        };

        #endregion

        #region Public Constructor
        /// <summary>
        /// Initializes a new instance of the MainForm class.
        /// </summary>
        public MainForm(string logMessage)
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
            treeViewFontSize = (decimal)serviceBusTreeView.Font.Size;
            logFontSize = (decimal)lstLog.Font.Size;
            Trace.Listeners.Add(new LogTraceListener(MainForm.StaticWriteToLog));
            mainSingletonMainForm = this;
            serviceBusHelper = new ServiceBusHelper(WriteToLog);
            serviceBusHelper.OnCreate += serviceBusHelper_OnCreate;
            serviceBusHelper.OnDelete += serviceBusHelper_OnDelete;
            serviceBusTreeView.TreeViewNodeSorter = new TreeViewHelper();
            eventClickFieldInfo = typeof(ToolStripItem).GetField(EventClick, BindingFlags.NonPublic | BindingFlags.Static);
            eventsPropertyInfo = typeof(Component).GetProperty(EventsProperty, BindingFlags.NonPublic | BindingFlags.Instance);
            configFileUse = TwoFilesConfiguration.GetCurrentConfigFileUse();

            GetServiceBusNamespacesFromConfiguration();
            GetServiceBusNamespaceFromEnvironmentVariable();
            GetBrokeredMessageInspectorsFromConfiguration();
            GetEventDataInspectorsFromConfiguration();
            GetBrokeredMessageGeneratorsFromConfiguration();
            GetEventDataGeneratorsFromConfiguration();
            GetServiceBusNamespaceSettingsFromConfiguration();
            ReadEventHubPartitionCheckpointFile();
            UpdateSavedConnectionsMenu();
            DisplayNewVersionInformation();

            WriteToLog(logMessage);
        }

        void DisplayNewVersionInformation()
        {
            if (!VersionProvider.IsLatestVersion(out var releaseInfo, WriteToLog))
            {
                linkLabelNewVersionAvailable.Visible = true;
                linkLabelNewVersionAvailable.Text = $"New Version {releaseInfo.Version} is available";
            }
            else
            {
                linkLabelNewVersionAvailable.Visible = false;
            }
        }

        private void UpdateSavedConnectionsMenu()
        {
            savedConnectionsToolStripMenuItem.DropDownItems.Clear();
            savedConnectionsToolStripMenuItem.Enabled = false;

            List<Keys> allowedShortCutKeys = new List<Keys>()
            {
                Keys.Control | Keys.D1,
                Keys.Control | Keys.D2,
                Keys.Control | Keys.D3,
                Keys.Control | Keys.D4,
                Keys.Control | Keys.D5
            };

            foreach (var namespaceKey in serviceBusHelper.ServiceBusNamespaces.Keys.OrderBy(k => k))
            {
                if (serviceBusHelper.ServiceBusNamespaces[namespaceKey].UserCreated)
                {
                    var shortcutKey = allowedShortCutKeys.Count > 0 ? allowedShortCutKeys.First() : Keys.None;
                    if (allowedShortCutKeys.Count > 0) allowedShortCutKeys.RemoveAt(0);

                    var menuItem = new ToolStripMenuItem
                    {
                        Text = namespaceKey,
                        ShortcutKeys = shortcutKey,
                        Tag = namespaceKey
                    };
                    menuItem.Click += SavedConnectionToolStripMenuItem_Click;

                    savedConnectionsToolStripMenuItem.DropDownItems.Add(menuItem);
                }
            }

            if (savedConnectionsToolStripMenuItem.DropDownItems.Count > 0)
            {
                savedConnectionsToolStripMenuItem.Enabled = true;
            }
        }

        private async void SavedConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var serviceBusNamespace = serviceBusHelper.ServiceBusNamespaces[(sender as ToolStripMenuItem).Tag.ToString()];
            serviceBusHelper.Connect(serviceBusNamespace);
            SetTitle(serviceBusNamespace.Namespace);

            foreach (var userControl in panelMain.Controls.OfType<UserControl>())
            {
                userControl.Dispose();
            }
            panelMain.Controls.Clear();
            panelMain.BackColor = SystemColors.Window;
            await ShowEntities(EntityType.All);
        }

        /// <summary>
        /// Initializes a new instance of the MainForm class.
        /// </summary>
        /// <param name="argument">Argument type (n or c).</param>
        /// <param name="value">Argument value</param>
        public MainForm(string argument, string value, string logMessage)
            : this(logMessage)
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
        private async void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainSettings = new MainSettings
            {
                LogFontSize = (decimal)lstLog.Font.Size,
                TreeViewFontSize = (decimal)serviceBusTreeView.Font.Size,
                RetryCount = RetryHelper.RetryCount,
                RetryTimeout = RetryHelper.RetryTimeout,
                ReceiveTimeout = ReceiveTimeout,
                ServerTimeout = ServerTimeout,
                PrefetchCount = PrefetchCount,
                TopCount = TopCount,
                SenderThinkTime = SenderThinkTime,
                ReceiverThinkTime = ReceiverThinkTime,
                MonitorRefreshInterval = MonitorRefreshInterval,

                ShowMessageCount = showMessageCount,
                UseAscii = UseAscii,
                SaveMessageToFile = saveMessageToFile,
                SavePropertiesToFile = savePropertiesToFile,
                SaveCheckpointsToFile = saveCheckpointsToFile,

                Label = Label,
                MessageFile = messageFile,
                MessageText = MessageText,
                MessageContentType = MessageContentType,

                SelectedEntities = SelectedEntities,
                SelectedMessageCounts = SelectedMessageCounts,
                MessageBodyType = messageBodyType,
                ConnectivityMode = ServiceBusHelper.ConnectivityMode,
                UseAmqpWebSockets = ServiceBusHelper.UseAmqpWebSockets,
                EncodingType = ServiceBusHelper.EncodingType,

                ProxyOverrideDefault = ProxyOverrideDefault,
                ProxyAddress = ProxyAddress,
                ProxyBypassList = ProxyBypassList,
                ProxyBypassOnLocal = ProxyBypassOnLocal,
                ProxyUseDefaultCredentials = ProxyUseDefaultCredentials,
                ProxyUserName = ProxyUserName,
                ProxyPassword = ProxyPassword,

                NodesColors = NodesColors
            };

            var configuration = TwoFilesConfiguration.Create(configFileUse, WriteToLog);

            mainSettings.DisableAccidentalDeletionPrevention = configuration.GetBoolValue(
                ConfigurationParameters.DisableAccidentalDeletionPrevention, defaultValue: false, WriteToLog);

            var lastConfigFileUse = configFileUse;

            using (var optionForm = new OptionForm(mainSettings, configFileUse))
            {
                if (optionForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                configFileUse = optionForm.ConfigFileUse;

                if (lastConfigFileUse != configFileUse)
                {
                    // Refresh the ServiceBus namespaces
                    GetServiceBusNamespacesFromConfiguration();
                    GetServiceBusNamespaceFromEnvironmentVariable();

                    // Then update the shortcut menus
                    UpdateSavedConnectionsMenu();
                }

                lstLog.Font = new Font(lstLog.Font.FontFamily, (float)optionForm.MainSettings.LogFontSize);
                serviceBusTreeView.Font = new Font(serviceBusTreeView.Font.FontFamily,
                    (float)optionForm.MainSettings.TreeViewFontSize);
                RetryHelper.RetryCount = optionForm.MainSettings.RetryCount;
                RetryHelper.RetryTimeout = optionForm.MainSettings.RetryTimeout;
                ReceiveTimeout = optionForm.MainSettings.ReceiveTimeout;
                ServerTimeout = optionForm.MainSettings.ServerTimeout;
                PrefetchCount = optionForm.MainSettings.PrefetchCount;
                TopCount = optionForm.MainSettings.TopCount;
                SenderThinkTime = optionForm.MainSettings.SenderThinkTime;
                ReceiverThinkTime = optionForm.MainSettings.ReceiverThinkTime;
                MonitorRefreshInterval = optionForm.MainSettings.MonitorRefreshInterval;

                var reloadEntities = false;
                if (showMessageCount != optionForm.MainSettings.ShowMessageCount)
                {
                    showMessageCount = optionForm.MainSettings.ShowMessageCount;
                    reloadEntities = true;
                }
                if (!SelectedMessageCounts.SequenceEqual(optionForm.MainSettings.SelectedMessageCounts))
                {
                    SelectedMessageCounts = optionForm.MainSettings.SelectedMessageCounts;
                    reloadEntities = true;
                }
                if (reloadEntities)
                {
                    await ShowEntities(EntityType.All);
                }

                UseAscii = optionForm.MainSettings.UseAscii;
                saveMessageToFile = optionForm.MainSettings.SaveMessageToFile;
                savePropertiesToFile = optionForm.MainSettings.SavePropertiesToFile;
                saveCheckpointsToFile = optionForm.MainSettings.SaveCheckpointsToFile;

                Label = optionForm.MainSettings.Label;
                messageFile = optionForm.MainSettings.MessageFile;
                MessageText = optionForm.MainSettings.MessageText;
                MessageContentType = optionForm.MainSettings.MessageContentType;

                SelectedEntities = optionForm.MainSettings.SelectedEntities;

                messageBodyType = optionForm.MainSettings.MessageBodyType;
                ServiceBusHelper.ConnectivityMode = optionForm.MainSettings.ConnectivityMode;
                ServiceBusHelper.UseAmqpWebSockets = optionForm.MainSettings.UseAmqpWebSockets;
                ServiceBusHelper.EncodingType = optionForm.MainSettings.EncodingType;

                SetProxy(optionForm.MainSettings);

                NodesColors = optionForm.MainSettings.NodesColors;
            }

            ReapplyColors(rootNode);
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
        async void MainForm_OnRefresh()
        {
            if (currentNode != null)
            {
                serviceBusTreeView.SelectedNode = currentNode;
                currentNode.EnsureVisible();
                await RefreshSelectedEntity();
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
        async void serviceBusHelper_OnDelete(ServiceBusHelperEventArgs args)
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
                    var queueListNode = FindNode(Constants.QueueEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(queueName))
                    {
                        DeleteNode(queueName, queueListNode);
                    }
                    else
                    {
                        await ShowEntities(EntityType.Queue);
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
                    var topicListNode = FindNode(Constants.TopicEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(topicName))
                    {
                        DeleteNode(topicName, topicListNode);
                    }
                    else
                    {
                        await ShowEntities(EntityType.Topic);
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
                    var relayListNode = FindNode(Constants.RelayEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(relayName))
                    {
                        DeleteNode(relayName, relayListNode);
                    }
                    else
                    {
                        await ShowEntities(EntityType.Relay);
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
                    var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(eventHubName))
                    {
                        DeleteNode(eventHubName, eventHubListNode);
                    }
                    else
                    {
                        await ShowEntities(EntityType.EventHub);
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
                    var notificationHubListNode = FindNode(Constants.NotificationHubEntities, rootNode);
                    if (!string.IsNullOrWhiteSpace(notificationHubName))
                    {
                        DeleteNode(notificationHubName, notificationHubListNode);
                    }
                    else
                    {
                        await ShowEntities(EntityType.NotificationHub);
                    }
                    serviceBusTreeView.SelectedNode = notificationHubListNode;
                    notificationHubListNode.EnsureVisible();
                    HandleNodeMouseClick(notificationHubListNode);
                    return;
                }
                // SubscriptionDescription Entity
                if (args.EntityType == EntityType.Subscription)
                {
                    var topicListNode = FindNode(Constants.TopicEntities, rootNode);
                    var subscription = args.EntityInstance as SubscriptionDescription;
                    if (subscription != null &&
                        !string.IsNullOrWhiteSpace(subscription.TopicPath))
                    {
                        var topicNode = FindNode(subscription.TopicPath, topicListNode);
                        if (topicNode == null)
                        {
                            await ShowEntities(EntityType.Topic);
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
                                await ShowEntities(EntityType.Topic);
                                return;
                            }
                        }
                        else
                        {
                            await ShowEntities(EntityType.Topic);
                            return;
                        }
                    }
                    else
                    {
                        await ShowEntities(EntityType.Topic);
                        return;
                    }
                    serviceBusTreeView.SelectedNode = null;
                    return;
                }
                // RuleDescription Entity
                if (args.EntityType == EntityType.Rule)
                {
                    var topicListNode = FindNode(Constants.TopicEntities, rootNode);
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
                            await ShowEntities(EntityType.Topic);
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
                                    await ShowEntities(EntityType.Topic);
                                    return;
                                }
                            }
                            else
                            {
                                await ShowEntities(EntityType.Topic);
                                return;
                            }
                        }
                        else
                        {
                            await ShowEntities(EntityType.Topic);
                            return;
                        }
                    }
                    else
                    {
                        await ShowEntities(EntityType.Topic);
                        return;
                    }
                    serviceBusTreeView.SelectedNode = null;
                }
                // ConsumerGroupDescription Entity
                if (args.EntityType == EntityType.ConsumerGroup)
                {
                    var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
                    var notificationHub = args.EntityInstance as ConsumerGroupDescription;
                    if (!string.IsNullOrWhiteSpace(notificationHub?.EventHubPath))
                    {
                        var eventHubNode = FindNode(notificationHub.EventHubPath, eventHubListNode);
                        if (eventHubNode == null)
                        {
                            await ShowEntities(EntityType.EventHub);
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
                                await ShowEntities(EntityType.EventHub);
                                return;
                            }
                        }
                        else
                        {
                            await ShowEntities(EntityType.EventHub);
                            return;
                        }
                    }
                    else
                    {
                        await ShowEntities(EntityType.EventHub);
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
                        var queueListNode = FindNode(Constants.QueueEntities, rootNode);
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
                        var topicListNode = FindNode(Constants.TopicEntities, rootNode);
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
                        var relayListNode = FindNode(Constants.RelayEntities, rootNode);
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
                        var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
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
                        var notificationHubListNode = FindNode(Constants.NotificationHubEntities, rootNode);
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
                    var topicListNode = FindNode(Constants.TopicEntities, rootNode);
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
                                                                           GetNameAndMessageCountText(wrapper.SubscriptionDescription.Name, wrapper.SubscriptionDescription.MessageCountDetails),
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
                    if (wrapper?.SubscriptionDescription == null ||
                        wrapper?.RuleDescription == null)
                    {
                        return;
                    }
                    var topicListNode = FindNode(Constants.TopicEntities, rootNode);
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
                    var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
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

        private void displayHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandLineOptions.ProcessCommandLineArguments(new[] { "--help" }, out _, out _, out var helpText);
            WriteToLog(helpText);
        }

        private async void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connectForm = new ConnectForm(serviceBusHelper, configFileUse))
                {
                    if (connectForm.ShowDialog() != DialogResult.OK)
                    {
                        UpdateSavedConnectionsMenu();
                        return;
                    }
                    UpdateSavedConnectionsMenu();
                    SelectedEntities = connectForm.SelectedEntities;
                    ServiceBusHelper.ConnectivityMode = connectForm.ConnectivityMode;
                    ServiceBusHelper.UseAmqpWebSockets = connectForm.UseAmqpWebSockets;
                    var serviceBusNamespace = ServiceBusNamespace.GetServiceBusNamespace(connectForm.Key ?? "Manual",
                        connectForm.ConnectionString, StaticWriteToLog);
                    serviceBusHelper.Connect(serviceBusNamespace);
                    SetTitle(serviceBusNamespace.Namespace);

                    foreach (var userControl in panelMain.Controls.OfType<UserControl>())
                    {
                        userControl.Dispose();
                    }
                    panelMain.Controls.Clear();
                    panelMain.BackColor = SystemColors.Window;
                    await ShowEntities(EntityType.All);
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

        private async void refreshEntityMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            await ShowEntities(EntityType.All);
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
                var queueListNode = FindNode(Constants.QueueEntities, rootNode);
                var topicListNode = FindNode(Constants.TopicEntities, rootNode);
                var relayListNode = FindNode(Constants.RelayEntities, rootNode);
                var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
                var notificationHubListNode = FindNode(Constants.NotificationHubEntities, rootNode);

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
                    ExportEntities(queueList, Constants.QueueEntities, null);
                    return;
                }
                // Topics
                if (serviceBusTreeView.SelectedNode == topicListNode)
                {
                    var topicList = new List<IExtensibleDataObject>();
                    GetTopicList(topicList, topicListNode);
                    ExportEntities(topicList, Constants.TopicEntities, null);
                    return;
                }
                // Relays
                if (serviceBusTreeView.SelectedNode == relayListNode)
                {
                    var relayList = new List<IExtensibleDataObject>();
                    GetRelayList(relayList, relayListNode);
                    ExportEntities(relayList, Constants.RelayEntities, null);
                    return;
                }
                // EventHubs
                if (serviceBusTreeView.SelectedNode == eventHubListNode)
                {
                    var eventHubList = new List<IExtensibleDataObject>();
                    GetEventHubList(eventHubList, eventHubListNode);
                    ExportEntities(eventHubList, Constants.EventHubEntities, null);
                    return;
                }
                // NotificationHubs
                if (serviceBusTreeView.SelectedNode == notificationHubListNode)
                {
                    var notificationHubList = new List<IExtensibleDataObject>();
                    GetNotificationHubList(notificationHubList, notificationHubListNode);
                    ExportEntities(notificationHubList, Constants.NotificationHubEntities, null);
                    return;
                }
                // Check that serviceBusTreeView.SelectedNode.Tag is not null
                if (serviceBusTreeView.SelectedNode.Tag == null)
                {
                    return;
                }
                // Url Segment Node
                if (serviceBusTreeView.SelectedNode.Tag is UrlSegmentWrapper urlSegmentWrapper)
                {
                    if (urlSegmentWrapper.EntityType == EntityType.Queue)
                    {
                        var queueList = new List<IExtensibleDataObject>();
                        GetQueueList(queueList, serviceBusTreeView.SelectedNode);
                        ExportEntities(queueList,
                                       FormatAbsolutePathForExport(urlSegmentWrapper.Uri),
                                       Constants.QueueEntities);
                    }
                    else if (urlSegmentWrapper.EntityType == EntityType.Topic)
                    {
                        var topicList = new List<IExtensibleDataObject>();
                        GetTopicList(topicList, serviceBusTreeView.SelectedNode);
                        ExportEntities(topicList,
                                       FormatAbsolutePathForExport(urlSegmentWrapper.Uri),
                                       Constants.TopicEntities);
                    }
                    else
                    {
                        var relayList = new List<IExtensibleDataObject>();
                        GetRelayList(relayList, serviceBusTreeView.SelectedNode);
                        ExportEntities(relayList,
                                       FormatAbsolutePathForExport(urlSegmentWrapper.Uri),
                                       Constants.RelayEntities);
                    }
                    return;
                }

                // Queue Node
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription queueDescription)
                {
                    ExportEntities(new List<IExtensibleDataObject> { queueDescription },
                                   queueDescription.Path,
                                   QueueEntity);
                    return;
                }

                // Topic Node
                if (serviceBusTreeView.SelectedNode.Tag is TopicDescription topicDescription)
                {
                    ExportEntities(new List<IExtensibleDataObject> { topicDescription },
                                   topicDescription.Path,
                                   TopicEntity);
                }

                // Relay Node
                if (serviceBusTreeView.SelectedNode.Tag is RelayDescription relayDescription)
                {
                    ExportEntities(new List<IExtensibleDataObject> { relayDescription },
                                   relayDescription.Path,
                                   RelayEntity);
                    return;
                }

                // EventHub Node
                if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription eventHubDescription)
                {
                    ExportEntities(new List<IExtensibleDataObject> { eventHubDescription },
                                   eventHubDescription.Path,
                                   EventHubEntity);
                }

                // NotificationHub Node
                if (serviceBusTreeView.SelectedNode.Tag is NotificationHubDescription notificationHubDescription)
                {
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
                if (serviceBusTreeView.SelectedNode.Tag is QueueDescription queueDescription)
                {
                    using (var parameterForm = new ParameterForm($"Enter a new name for the {queueDescription.Path} queue.",
                            new List<string> { "Name" },
                            new List<string> { queueDescription.Path },
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
                if (serviceBusTreeView.SelectedNode.Tag is TopicDescription topicDescription)
                {
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
        private async void refreshEntity_Click(object sender, EventArgs e)
        {
            await RefreshSelectedEntity();
        }

        private void RefreshIndividualSubscription(SubscriptionDescription subscriptionDescription, TreeNode subscriptionNode)
        {
            var rules = serviceBusHelper.GetRules(subscriptionDescription);
            var ruleDescriptions = rules as RuleDescription[] ?? rules.ToArray();
            if (!ruleDescriptions.Any())
            {
                subscriptionNode.Nodes.Clear();
                return;
            }
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
                var subscriptionNode = subscriptionsNode.Nodes.Add(subscriptionDescription.Name,
                                                                   GetNameAndMessageCountText(subscriptionDescription.Name, subscriptionDescription.MessageCountDetails),
                                                                   subscriptionDescription.Status == EntityStatus.Active ? SubscriptionIconIndex : GreySubscriptionIconIndex,
                                                                   subscriptionDescription.Status == EntityStatus.Active ? SubscriptionIconIndex : GreySubscriptionIconIndex);
                subscriptionNode.ContextMenuStrip = subscriptionContextMenuStrip;
                subscriptionNode.Tag = new SubscriptionWrapper(subscriptionDescription, topicDescription);
                if (topicDescription != null)
                {
                    WriteToLog(string.Format(CultureInfo.CurrentCulture, SubscriptionRetrievedFormat, subscriptionDescription.Name, topicDescription.Path), false);
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
                    if (serviceBusTreeView.SelectedNode.Text == Constants.QueueEntities)
                    {
                        panelMain.HeaderText = CreateQueue;
                        ShowQueue(null, null);
                        return;
                    }
                    // Topics Node (Create New TopicDescription)
                    if (serviceBusTreeView.SelectedNode.Text == Constants.TopicEntities)
                    {
                        panelMain.HeaderText = CreateTopic;
                        ShowTopic(null, null);
                        return;
                    }
                    // Relays Node (Create New RelayDescription)
                    if (serviceBusTreeView.SelectedNode.Text == Constants.RelayEntities)
                    {
                        panelMain.HeaderText = CreateRelay;
                        ShowRelay(null, null);
                        return;
                    }
                    // EventHubs Node (Create New EventHubDescription)
                    if (serviceBusTreeView.SelectedNode.Text == Constants.EventHubEntities)
                    {
                        panelMain.HeaderText = CreateEventHub;
                        ShowEventHub(null);
                        return;
                    }
                    // ConsumerGroup Node (Create New ConsumerGroupDescription)
                    if (serviceBusTreeView.SelectedNode.Text == ConsumerGroupEntities)
                    {
                        var parent = serviceBusTreeView.SelectedNode.Parent;
                        if (parent?.Tag is EventHubDescription eventHubDescription)
                        {
                            panelMain.HeaderText = CreateConsumerGroup;
                            ShowConsumerGroup(null, eventHubDescription.Path);
                        }
                        return;
                    }

                    // NotificationHubs Node (Create New NotificationHubDescription)
                    if (serviceBusTreeView.SelectedNode.Text == Constants.NotificationHubEntities)
                    {
                        panelMain.HeaderText = CreateNotificationHub;
                        ShowNotificationHub(null);
                        return;
                    }
                    if (serviceBusTreeView.SelectedNode.Tag != null)
                    {
                        // UrlSegment Node
                        if (serviceBusTreeView.SelectedNode.Tag is UrlSegmentWrapper urlSegmentWrapper)
                        {
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
                            if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper subscriptionNewWrapper)
                            {
                                ShowSubscription(new SubscriptionWrapper(null, subscriptionNewWrapper.TopicDescription));
                            }
                            return;
                        }

                        // SubscriptionDescription Node (Create New RuleDescription)
                        if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper subscriptionRuleWrapper)
                        {
                            panelMain.HeaderText = AddRule;
                            ShowRule(new RuleWrapper(null, subscriptionRuleWrapper.SubscriptionDescription), !serviceBusTreeView.SelectedNode.Nodes.ContainsKey(RuleEntities));
                            return;
                        }

                        // Rules Node (Create New RuleDescription)
                        if (serviceBusTreeView.SelectedNode.Text == RuleEntities)
                        {
                            panelMain.HeaderText = AddRule;
                            if (serviceBusTreeView.SelectedNode.Tag is RuleWrapper ruleWrapper)
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

        private void serviceBusTreeView_KeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Modifiers == (Keys.Alt | Keys.Control) && keyEventArgs.KeyCode == Keys.Delete) // ignore three-finger salute
            {
                return;
            }

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
                    var queueListNode = FindNode(Constants.QueueEntities, rootNode);
                    var topicListNode = FindNode(Constants.TopicEntities, rootNode);
                    var relayServiceListNode = FindNode(Constants.RelayEntities, rootNode);
                    var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
                    var notificationHubListNode = FindNode(Constants.NotificationHubEntities, rootNode);

                    string serviceBusNamespaceLocalName = GetServiceBusNamespaceLocalName(rootNode.Text);

                    var configuration = TwoFilesConfiguration.Create(configFileUse, WriteToLog);

                    // Root Node
                    if (serviceBusTreeView.SelectedNode == rootNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllEntities))
                        {
                            deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "Everything in " + serviceBusNamespaceLocalName);

                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var queueList = new List<string>();
                                var topicList = new List<string>();
                                GetQueueList(queueList, queueListNode);
                                GetTopicList(topicList, topicListNode);
                                await serviceBusHelper.DeleteQueues(queueList);
                                await serviceBusHelper.DeleteTopics(topicList);
                                await ShowEntities(EntityType.All);
                            }
                        }
                        return;
                    }

                    // Queues Node
                    if (serviceBusTreeView.SelectedNode == queueListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllQueues))
                        {
                            deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "All queues in " + serviceBusNamespaceLocalName);

                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var queueList = new List<string>();
                                GetQueueList(queueList, queueListNode);
                                await serviceBusHelper.DeleteQueues(queueList);
                                await ShowEntities(EntityType.Queue);
                            }
                        }
                        return;
                    }

                    // Topics Node
                    if (serviceBusTreeView.SelectedNode == topicListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllTopics))
                        {
                            deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "All topics in " + serviceBusNamespaceLocalName);

                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var topicList = new List<string>();
                                GetTopicList(topicList, topicListNode);
                                await serviceBusHelper.DeleteTopics(topicList);
                                await ShowEntities(EntityType.Topic);
                            }
                        }
                        return;
                    }

                    // Relays Node
                    if (serviceBusTreeView.SelectedNode == relayServiceListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllRelays))
                        {
                            deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "All relays in " + serviceBusNamespaceLocalName);

                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var relayServiceList = new List<string>();
                                GetRelayList(relayServiceList, relayServiceListNode);
                                await serviceBusHelper.DeleteRelays(relayServiceList);
                                await ShowEntities(EntityType.Relay);
                            }
                        }
                        return;
                    }

                    // EventHubs Node
                    if (serviceBusTreeView.SelectedNode == eventHubListNode)
                    {
                        using (var deleteForm = new DeleteForm(DeleteAllEventHubs))
                        {
                            deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "All event hubs in " + serviceBusNamespaceLocalName);

                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var eventHubList = new List<string>();
                                GetEventHubList(eventHubList, eventHubListNode);
                                serviceBusHelper.DeleteEventHubs(eventHubList);
                                await ShowEntities(EntityType.EventHub);
                            }
                        }
                        return;
                    }

                    // ConsumerGroups Node
                    if (serviceBusTreeView.SelectedNode.Text == ConsumerGroupEntities)
                    {
                        var parent = serviceBusTreeView.SelectedNode.Parent;
                        if (parent?.Tag is EventHubDescription eventHubConsumerDescription)
                        {
                            using (var deleteForm = new DeleteForm(string.Format(DeleteAllConsumerGroups, eventHubConsumerDescription.Path)))
                            {
                                deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "All consumer groups in " + serviceBusNamespaceLocalName);

                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    var notificationHubList = new List<string>();
                                    GetConsumerGroupList(notificationHubList, serviceBusTreeView.SelectedNode);
                                    notificationHubList.Remove(DefaultConsumerGroupName);
                                    serviceBusHelper.DeleteConsumerGroups(eventHubConsumerDescription.Path, notificationHubList);
                                    await RefreshSelectedEntity();
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
                            deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "All notification hubs in " + serviceBusNamespaceLocalName);

                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                var notificationHubList = new List<string>();
                                GetNotificationHubList(notificationHubList, notificationHubListNode);
                                await serviceBusHelper.DeleteNotificationHubs(notificationHubList);
                                await ShowEntities(EntityType.NotificationHub);
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
                    if (serviceBusTreeView.SelectedNode.Tag is UrlSegmentWrapper urlSegmentWrapper)
                    {
                        if (urlSegmentWrapper.EntityType == EntityType.Queue)
                        {
                            using (var deleteForm = new DeleteForm(string.Format(DeleteAllQueuesInPath, FormatAbsolutePathForEdit(urlSegmentWrapper.Uri))))
                            {
                                deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "Queues in subpath of " + serviceBusNamespaceLocalName);

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
                                deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "Topics in subpath of " + serviceBusNamespaceLocalName);

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
                                deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "Relays in subpath of " + serviceBusNamespaceLocalName);

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
                    if (serviceBusTreeView.SelectedNode.Tag is QueueDescription queueDescription)
                    {
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
                    if (serviceBusTreeView.SelectedNode.Tag is TopicDescription topicDescription)
                    {
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
                    if (serviceBusTreeView.SelectedNode.Tag is RelayDescription relayDescription)
                    {
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

                        if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription eventHubDeleteDescription)
                        {
                            using (var deleteForm = new DeleteForm(string.Format(DeleteAllConsumerGroups, eventHubDeleteDescription.Path)))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    var consumerGroups = new List<string>();
                                    GetConsumerGroupList(consumerGroups, serviceBusTreeView.SelectedNode);
                                    consumerGroups.Remove(DefaultConsumerGroupName);
                                    serviceBusHelper.DeleteConsumerGroups(eventHubDeleteDescription.Path, consumerGroups);
                                    await RefreshSelectedEntity();
                                }
                            }
                        }
                        return;
                    }

                    // EventHub Node
                    if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription eventHubDescription)
                    {
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
                    if (serviceBusTreeView.SelectedNode.Tag is ConsumerGroupDescription consumerGroupDescription)
                    {
                        using (var deleteForm = new DeleteForm(consumerGroupDescription.Name, ConsumerGroupEntity.ToLower()))
                        {
                            if (deleteForm.ShowDialog() == DialogResult.OK)
                            {
                                serviceBusHelper.DeleteConsumerGroup(consumerGroupDescription);
                            }
                        }
                        return;
                    }

                    // NotificationHub Node
                    if (serviceBusTreeView.SelectedNode.Tag is NotificationHubDescription notificationHubDescription)
                    {
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
                    if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper subscriptionWrapper)
                    {
                        if (subscriptionWrapper.TopicDescription != null &&
                            subscriptionWrapper.SubscriptionDescription != null)
                        {
                            using (var deleteForm = new DeleteForm(subscriptionWrapper.SubscriptionDescription.Name, SubscriptionEntity.ToLower()))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    await serviceBusHelper.DeleteSubscription(subscriptionWrapper.SubscriptionDescription);
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
                                deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, "All rules in " + serviceBusNamespaceLocalName);

                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    serviceBusHelper.RemoveRules(ruleWrappers);
                                }
                            }
                        }
                        return;
                    }

                    // Rule Node
                    if (serviceBusTreeView.SelectedNode.Tag is RuleWrapper ruleWrapper)
                    {
                        if (ruleWrapper.SubscriptionDescription != null &&
                            ruleWrapper.RuleDescription != null)
                        {
                            using (var deleteForm = new DeleteForm(ruleWrapper.RuleDescription.Name, RuleEntity.ToLower()))
                            {
                                if (deleteForm.ShowDialog() == DialogResult.OK)
                                {
                                    serviceBusHelper.RemoveRule(ruleWrapper.SubscriptionDescription, ruleWrapper.RuleDescription);
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

        private void changeStatusQueueMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                var tag = serviceBusTreeView.SelectedNode.Tag as QueueDescription;
                if (tag != null)
                {
                    // Put a check against the item that reflects the current status of the queue
                    var queueDescription = serviceBusHelper.GetQueue(tag.Path);
                    var status = queueDescription.Status;
                    foreach (var dropDownItem in changeStatusQueueMenuItem.DropDownItems)
                    {
                        var dropDownMenuItem = dropDownItem as ToolStripMenuItem;
                        dropDownMenuItem.Checked = (EntityStatus)dropDownMenuItem.Tag == status;
                    }
                }
                else
                {
                    throw new NotSupportedException("Not supported for entity types other than queues.");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void changeStatusQueue_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (serviceBusHelper != null &&
                    serviceBusTreeView.SelectedNode != null)
                {
                    if (serviceBusTreeView.SelectedNode.Tag is QueueDescription queueDescription)
                    {
                        var desiredStatus = (EntityStatus)e.ClickedItem.Tag;
                        using (var changeStatusForm = new ChangeStatusForm(queueDescription.Path, QueueEntity.ToLower(), desiredStatus))
                        {
                            if (changeStatusForm.ShowDialog() == DialogResult.OK)
                            {
                                queueDescription.Status = (EntityStatus)e.ClickedItem.Tag;
                                serviceBusHelper.NamespaceManager.UpdateQueue(queueDescription);
                                await RefreshSelectedEntity();
                            }
                        }
                    }
                    else
                    {
                        throw new NotSupportedException("Not supported for entity types other than queues.");
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
                    if (serviceBusTreeView.SelectedNode.Tag is QueueDescription queueDescription)
                    {
                        using (var changeQueueStatusForm = new ChangeQueueStatusForm(queueDescription.Status))
                        {
                            if (changeQueueStatusForm.ShowDialog() == DialogResult.OK)
                            {
                                queueDescription.Status = changeQueueStatusForm.EntityStatus;
                                await serviceBusHelper.NamespaceManager.UpdateQueueAsync(queueDescription);
                                await RefreshSelectedEntity();
                            }
                        }
                        return;
                    }
                    // Topic Node
                    if (serviceBusTreeView.SelectedNode.Tag is TopicDescription topicDescription)
                    {
                        var desiredStatus = topicDescription.Status == EntityStatus.Active
                                                          ? EntityStatus.Disabled
                                                          : EntityStatus.Active;
                        using (var changeStatusForm = new ChangeStatusForm(topicDescription.Path, TopicEntity.ToLower(), desiredStatus))
                        {
                            if (changeStatusForm.ShowDialog() == DialogResult.OK)
                            {
                                topicDescription.Status = desiredStatus;
                                await serviceBusHelper.NamespaceManager.UpdateTopicAsync(topicDescription);
                                await RefreshSelectedEntity();
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
                    if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper subscriptionWrapper)
                    {
                        if (subscriptionWrapper.TopicDescription != null &&
                            subscriptionWrapper.SubscriptionDescription != null)
                        {
                            var desiredStatus = subscriptionWrapper.SubscriptionDescription.Status = subscriptionWrapper.SubscriptionDescription.Status == EntityStatus.Active
                                                                             ? EntityStatus.Disabled
                                                                             : EntityStatus.Active;
                            using (var changeStatusForm = new ChangeStatusForm(subscriptionWrapper.SubscriptionDescription.Name, SubscriptionEntity.ToLower(), desiredStatus))
                            {
                                if (changeStatusForm.ShowDialog() == DialogResult.OK)
                                {
                                    subscriptionWrapper.SubscriptionDescription.Status = desiredStatus;
                                    await serviceBusHelper.NamespaceManager.UpdateSubscriptionAsync(subscriptionWrapper.SubscriptionDescription);
                                    await RefreshSelectedEntity();
                                    changeStatusSubscriptionMenuItem.Text = subscriptionWrapper.SubscriptionDescription.Status == EntityStatus.Active
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
                    if (serviceBusTreeView.SelectedNode.Tag is EventHubDescription eventHubDescription)
                    {
                        var desiredStatus = eventHubDescription.Status = eventHubDescription.Status == EntityStatus.Active
                                                          ? EntityStatus.Disabled
                                                          : EntityStatus.Active;
                        using (var changeStatusForm = new ChangeStatusForm(eventHubDescription.Path, EventHubEntity.ToLower(), desiredStatus))
                        {
                            if (changeStatusForm.ShowDialog() == DialogResult.OK)
                            {
                                eventHubDescription.Status = desiredStatus;
                                await serviceBusHelper.NamespaceManager.UpdateEventHubAsync(eventHubDescription);
                                await RefreshSelectedEntity();
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

        private void serviceBusTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            EnsureNodeHasBeenLazyLoaded(e.Node);
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

        public async Task RefreshSelectedEntity()
        {
            try
            {
                serviceBusTreeView.SuspendDrawing();
                serviceBusTreeView.SuspendLayout();
                serviceBusTreeView.BeginUpdate();
                var queueListNode = FindNode(Constants.QueueEntities, rootNode);
                var topicListNode = FindNode(Constants.TopicEntities, rootNode);
                var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
                var notificationHubListNode = FindNode(Constants.NotificationHubEntities, rootNode);
                var relayServiceListNode = FindNode(Constants.RelayEntities, rootNode);
                if (serviceBusTreeView.SelectedNode != null)
                {
                    // Queues
                    if (serviceBusTreeView.SelectedNode == queueListNode)
                    {
                        await ShowEntities(EntityType.Queue);
                        return;
                    }

                    // Topics
                    if (serviceBusTreeView.SelectedNode == topicListNode)
                    {
                        await ShowEntities(EntityType.Topic);
                        return;
                    }

                    // Event Hubs
                    if (serviceBusTreeView.SelectedNode == eventHubListNode)
                    {
                        await ShowEntities(EntityType.EventHub);
                        return;
                    }

                    // Notification Hubs
                    if (serviceBusTreeView.SelectedNode == notificationHubListNode)
                    {
                        await ShowEntities(EntityType.NotificationHub);
                        return;
                    }

                    // Relays
                    if (serviceBusTreeView.SelectedNode == relayServiceListNode)
                    {
                        await ShowEntities(EntityType.Relay);
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
                        RefreshQueueNode(serviceBusTreeView.SelectedNode, queueDescription);

                        // Update the right view
                        var control = panelMain.Controls[0] as HandleQueueControl;
                        if (control != null)
                        {
                            control.RefreshData(queueDescription);
                            WriteToLog(string.Format(QueueRetrievedFormat, queueDescription.Path), false);
                        }

                        return;
                    }

                    // Individual Topic Node
                    var selectedNodeTag = serviceBusTreeView.SelectedNode.Tag as TopicDescription;
                    if (selectedNodeTag != null)
                    {
                        var topicDescription = serviceBusHelper.GetTopic(selectedNodeTag.Path);
                        RefreshTopicNode(serviceBusTreeView.SelectedNode, topicDescription);

                        var control = panelMain.Controls[0] as HandleTopicControl;
                        if (control != null)
                        {
                            control.RefreshData(topicDescription);
                            WriteToLog(string.Format(TopicRetrievedFormat, topicDescription.Path), false);
                        }

                        return;
                    }

                    // Relay Node
                    var nodeTag = serviceBusTreeView.SelectedNode.Tag as RelayDescription;
                    if (nodeTag != null)
                    {
                        var description = nodeTag;
                        if (description.IsDynamic)
                        {
                            var relayCollection = serviceBusHelper.GetRelays(MainForm.SingletonMainForm.ServerTimeout);
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
                    if (serviceBusTreeView.SelectedNode.Tag is ConsumerGroupDescription consumerGroupDescription)
                    {
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
                                                                                   GetNameAndMessageCountText(subscription.Name, subscription.MessageCountDetails),
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
                    if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper subWrapper)
                    {
                        var subscriptionDescription = serviceBusHelper.GetSubscription(subWrapper.SubscriptionDescription.TopicPath, subWrapper.SubscriptionDescription.Name);
                        subWrapper = new SubscriptionWrapper(subscriptionDescription, subWrapper.TopicDescription);
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
                        serviceBusTreeView.SelectedNode.Tag = subWrapper;

                        if (panelMain.Controls[0] is HandleSubscriptionControl control)
                        {
                            control.RefreshData(subWrapper);
                            WriteToLog(string.Format(SubscriptionRetrievedFormat,
                                                     subWrapper.SubscriptionDescription.Name,
                                                     subWrapper.SubscriptionDescription.TopicPath),
                                       false);
                        }
                        serviceBusTreeView.SelectedNode.Text = GetNameAndMessageCountText(serviceBusTreeView.SelectedNode.Name, subscriptionDescription.MessageCountDetails);

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

        public async Task RefreshQueues()
        {
            await ShowEntities(EntityType.Queue);
        }

        public async Task RefreshTopics()
        {
            await ShowEntities(EntityType.Topic);
        }

        public async Task RefreshServiceBusEntityNode(string path)
        {
            var serviceBusHelper2 = serviceBusHelper.GetServiceBusHelper2();

            if (await serviceBusHelper2.IsQueue(path))
            {
                var queueDescription = serviceBusHelper.GetQueue(path);
                var queueListNode = FindNode(Constants.QueueEntities, rootNode);
                var queueNode = FindNode(path, queueListNode);

                RefreshQueueNode(queueNode, queueDescription);
                return;
            }

            if (await serviceBusHelper2.IsTopic(path))
            {
                var topicDescription = serviceBusHelper.GetTopic(path);
                var topicListNode = FindNode(Constants.TopicEntities, rootNode);
                var topicNode = FindNode(path, topicListNode);

                RefreshTopicNode(topicNode, topicDescription);
                return;
            }

            WriteToLog($"{path} is neither a queue nor a topic so there was no update in the tree view.");
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
                        Invoke(new Action<string>(InternalWriteToLog), message);
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
                var queueListNode = FindNode(Constants.QueueEntities, rootNode);
                var topicListNode = FindNode(Constants.TopicEntities, rootNode);
                var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
                var notificationHubListNode = FindNode(Constants.NotificationHubEntities, rootNode);
                var relayServiceListNode = FindNode(Constants.RelayEntities, rootNode);
                actionsToolStripMenuItem.DropDownItems.Clear();
                actionsToolStripMenuItem.DropDownItems.Add(createIoTHubListenerMenuItem);
                actionsToolStripMenuItem.DropDownItems.Add(createEventHubListenerMenuItem);

                // Ensure that the node has loaded its children
                EnsureNodeHasBeenLazyLoaded(node);

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
                if (node.Tag is UrlSegmentWrapper urlSegmentWrapper)
                {
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
                if (node.Tag is QueueDescription queueDescription)
                {
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
                if (node.Tag is TopicDescription topicDescription)
                {
                    changeStatusTopicMenuItem.Text = topicDescription.Status == EntityStatus.Active ? DisableTopic : EnableTopic;
                    var list = CloneItems(topicContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewTopicFormat, topicDescription.Path);
                    ShowTopic(topicDescription, null);
                    return;
                }

                // Relay Node
                if (node.Tag is RelayDescription relayDescription)
                {
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
                if (node.Tag is EventHubDescription eventHubDescription)
                {
                    changeStatusEventHubMenuItem.Text = eventHubDescription.Status == EntityStatus.Active ? DisableEventHub : EnableEventHub;
                    var list = CloneItems(eventHubContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewEventHubFormat, eventHubDescription.Path);
                    ShowEventHub(eventHubDescription);
                    return;
                }

                // Partition Node
                if (node.Tag is PartitionDescription partitionDescription)
                {
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
                if (node.Tag is ConsumerGroupDescription consumerGroupDescription)
                {
                    var list = CloneItems(notificationHubContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewConsumerGroupFormat, consumerGroupDescription.Name);
                    ShowConsumerGroup(consumerGroupDescription, consumerGroupDescription.EventHubPath);
                    return;
                }

                // NotificationHub Node
                if (node.Tag is NotificationHubDescription notificationHubDescription)
                {
                    var list = CloneItems(notificationHubContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewNotificationHubFormat, notificationHubDescription.Path);
                    ShowNotificationHub(notificationHubDescription);
                    return;
                }

                // Subscription Node
                if (node.Tag is SubscriptionWrapper subscriptionWrapper)
                {
                    changeStatusSubscriptionMenuItem.Text = subscriptionWrapper.SubscriptionDescription.Status == EntityStatus.Active ? DisableSubscription : EnableSubscription;
                    getSubscriptionMessageSessionsMenuItem.Visible = subscriptionWrapper.SubscriptionDescription.RequiresSession;
                    getSubscriptionMessageSessionsSeparator.Visible = subscriptionWrapper.SubscriptionDescription.RequiresSession;
                    subReceiveMessagesMenuItem.Visible = string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.ForwardTo);
                    subReceiveToolStripSeparator.Visible = string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.ForwardTo);
                    var list = CloneItems(subscriptionContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewSubscriptionFormat, subscriptionWrapper.SubscriptionDescription.Name);
                    ShowSubscription(subscriptionWrapper);
                    return;
                }

                // RuleDescription Node
                if (node.Tag is RuleWrapper ruleWrapper)
                {
                    var list = CloneItems(ruleContextMenuStrip.Items);
                    AddImportAndSeparatorMenuItems(list);
                    actionsToolStripMenuItem.DropDownItems.AddRange(list.ToArray());
                    panelMain.HeaderText = string.Format(ViewRuleFormat, ruleWrapper.RuleDescription.Name);
                    ShowRule(ruleWrapper, null);
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

        private void GetServiceBusNamespacesFromConfiguration()
        {
            try
            {
                var configuration = TwoFilesConfiguration.Create(configFileUse, WriteToLog);
                serviceBusHelper.ServiceBusNamespaces =
                    ServiceBusNamespace.GetMessagingNamespaces(configuration, WriteToLog);
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
                serviceBusHelper.ServiceBusNamespaces.Add(key, ServiceBusNamespace.GetServiceBusNamespace(key, connectionString, StaticWriteToLog));
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
                        var type = Type.GetType((string)e.Value);
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
                        var type = Type.GetType((string)e.Value);
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
                        var type = Type.GetType((string)e.Value);
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
                        var type = Type.GetType((string)e.Value);
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

        void GetServiceBusNamespaceSettingsFromConfiguration()
        {
            if (serviceBusHelper == null)
            {
                return;
            }

            var currentSettings = new MainSettings
            {
                LogFontSize = logFontSize,
                TreeViewFontSize = treeViewFontSize,
                RetryCount = RetryHelper.RetryCount,
                RetryTimeout = RetryHelper.RetryTimeout,
                ReceiveTimeout = ReceiveTimeout,
                ServerTimeout = ServerTimeout,
                PrefetchCount = PrefetchCount,
                TopCount = TopCount,
                SenderThinkTime = SenderThinkTime,
                ReceiverThinkTime = ReceiverThinkTime,
                MonitorRefreshInterval = MonitorRefreshInterval,
                ShowMessageCount = showMessageCount,
                UseAscii = UseAscii,
                SaveMessageToFile = saveMessageToFile,
                SavePropertiesToFile = savePropertiesToFile,
                SaveCheckpointsToFile = saveCheckpointsToFile,
                Label = Label,
                MessageFile = messageFile,
                MessageText = MessageText,
                MessageContentType = MessageContentType,
                SelectedEntities = SelectedEntities,
                SelectedMessageCounts = SelectedMessageCounts,
                MessageBodyType = messageBodyType,
                ConnectivityMode = ServiceBusHelper.ConnectivityMode,
                UseAmqpWebSockets = ServiceBusHelper.UseAmqpWebSockets,
                ProxyOverrideDefault = ProxyOverrideDefault,
                ProxyAddress = ProxyAddress,
                ProxyBypassList = ProxyBypassList,
                ProxyBypassOnLocal = ProxyBypassOnLocal,
                ProxyUseDefaultCredentials = ProxyUseDefaultCredentials,
                ProxyUserName = ProxyUserName,
                ProxyPassword = ProxyPassword,
                NodesColors = NodesColors
            };

            var readSettings = ConfigurationHelper.GetMainProperties(configFileUse, currentSettings, WriteToLog);

            var tempLogFontSize = readSettings.LogFontSize;
            if (tempLogFontSize != logFontSize)
            {
                logFontSize = tempLogFontSize;
                lstLog.Font = new Font(lstLog.Font.FontFamily, (float)logFontSize);
            }

            var tempTreeViewFontSize = readSettings.TreeViewFontSize;
            if (tempTreeViewFontSize != treeViewFontSize)
            {
                treeViewFontSize = tempTreeViewFontSize;
                serviceBusTreeView.Font = new Font(serviceBusTreeView.Font.FontFamily, (float)treeViewFontSize);
            }

            RetryHelper.RetryCount = readSettings.RetryCount;
            RetryHelper.RetryTimeout = readSettings.RetryTimeout;

            var tempReceiveTimeout = readSettings.ReceiveTimeout;
            if (tempReceiveTimeout >= 0)
            {
                ReceiveTimeout = tempReceiveTimeout;
            }

            var tempServerTimeout = readSettings.ServerTimeout;
            if (tempServerTimeout >= 0)
            {
                ServerTimeout = tempServerTimeout;
            }

            var tempPrefetchCount = readSettings.PrefetchCount;
            if (tempPrefetchCount >= 0)
            {
                PrefetchCount = tempPrefetchCount;
            }

            var tempTopValue = readSettings.TopCount;
            if (tempTopValue > 0)
            {
                TopCount = tempTopValue;
            }

            var tempSenderThinkTime = readSettings.SenderThinkTime;
            if (tempSenderThinkTime >= 0)
            {
                SenderThinkTime = tempSenderThinkTime;
            }

            var tempReceiverThinkTime = readSettings.ReceiverThinkTime;
            if (tempReceiverThinkTime >= 0)
            {
                ReceiverThinkTime = tempReceiverThinkTime;
            }

            var tempMonitorRefreshIntervalValue = readSettings.MonitorRefreshInterval;
            if (tempMonitorRefreshIntervalValue >= 0)
            {
                MonitorRefreshInterval = tempMonitorRefreshIntervalValue;
            }

            showMessageCount = readSettings.ShowMessageCount;
            UseAscii = readSettings.UseAscii;
            saveMessageToFile = readSettings.SaveMessageToFile;
            savePropertiesToFile = readSettings.SavePropertiesToFile;
            saveCheckpointsToFile = readSettings.SaveCheckpointsToFile;

            Label = readSettings.Label;

            MessageText = readSettings.MessageText;
            MessageContentType = readSettings.MessageContentType;
            messageFile = readSettings.MessageFile;

            SelectedEntities = readSettings.SelectedEntities;
            SelectedMessageCounts = readSettings.SelectedMessageCounts;
            messageBodyType = readSettings.MessageBodyType;
            ServiceBusHelper.ConnectivityMode = readSettings.ConnectivityMode;
            ServiceBusHelper.UseAmqpWebSockets = readSettings.UseAmqpWebSockets;
            ServiceBusHelper.EncodingType = readSettings.EncodingType;

            // Get values for settings that are not part of MainSettings
            // configFileUse = TwoFilesConfiguration.GetCurrentConfigFileUse();

            var configuration = TwoFilesConfiguration.Create(configFileUse, WriteToLog);

            serviceBusHelper.TraceEnabled =
                configuration.GetBoolValue(ConfigurationParameters.DebugFlagParameter,
                    serviceBusHelper.TraceEnabled);

            serviceBusHelper.Scheme = configuration.GetStringValue(ConfigurationParameters.SchemeParameter,
                serviceBusHelper.Scheme);

            var messageDeferProvider = configuration.GetStringValue(ConfigurationParameters.MessageDeferProviderParameter);

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
                catch (Exception)
                {
                    // Comment to avoid ReSharper warning
                }
            }

            SetProxy(readSettings);

            NodesColors = readSettings.NodesColors;
        }

        private void SetProxy(MainSettings settings)
        {
            if (initialDefaultWebProxy == null)
            {
                initialDefaultWebProxy = WebRequest.DefaultWebProxy;
            }

            ProxyOverrideDefault = settings.ProxyOverrideDefault;
            ProxyAddress = settings.ProxyAddress;
            ProxyBypassList = settings.ProxyBypassList;
            ProxyBypassOnLocal = settings.ProxyBypassOnLocal;
            ProxyUseDefaultCredentials = settings.ProxyUseDefaultCredentials;
            ProxyUserName = settings.ProxyUserName;
            ProxyPassword = settings.ProxyPassword;

            if (settings.ProxyOverrideDefault && !string.IsNullOrWhiteSpace(settings.ProxyAddress))
            {
                var credentials = settings.ProxyUseDefaultCredentials
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(settings.ProxyUserName, settings.ProxyPassword);
                var proxy = new WebProxy(settings.ProxyAddress, settings.ProxyBypassOnLocal, settings.ProxyBypassList.Split(';'), credentials);
                WebRequest.DefaultWebProxy = proxy;
            }
            else
            {
                WebRequest.DefaultWebProxy = initialDefaultWebProxy;
            }
        }

        private void ReadEventHubPartitionCheckpointFile()
        {
            if (saveCheckpointsToFile)
            {
                EventProcessorCheckpointHelper.ReadCheckpoints();
            }
        }

        private void RefreshQueueNode(TreeNode node, QueueDescription queueDescription)
        {
            if (queueDescription.Status == EntityStatus.Active)
            {
                node.ImageIndex = QueueIconIndex;
                node.SelectedImageIndex = QueueIconIndex;
            }
            else
            {
                node.ImageIndex = GreyQueueIconIndex;
                node.SelectedImageIndex = GreyQueueIconIndex;
            }

            node.Tag = queueDescription;
            node.Text = GetNameAndMessageCountText(node.Name, queueDescription.MessageCountDetails);
        }

        private void RefreshTopicNode(TreeNode node, TopicDescription topicDescription)
        {
            if (topicDescription.Status == EntityStatus.Active)
            {
                node.ImageIndex = TopicIconIndex;
                node.SelectedImageIndex = TopicIconIndex;
            }
            else
            {
                node.ImageIndex = GreyTopicIconIndex;
                node.SelectedImageIndex = GreyTopicIconIndex;
            }

            node.Tag = topicDescription;
            RefreshIndividualTopic(node);
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

        private void SetTitle(string prefix)
        {
            this.Text = $"{prefix} - Service Bus Explorer";
        }

        #endregion

        #region Public Static Methods
        public static void StaticWriteToLog(string message, bool async = true)
        {
            mainSingletonMainForm.WriteToLog(message);
        }
        #endregion

        #region Private Static Methods
        private static string GetServiceBusNamespaceLocalName(string text)
        {
            if (Uri.TryCreate(text, UriKind.Absolute, out var serviceBusNamespaceUri))
            {
                string hostNameQualified = serviceBusNamespaceUri.Host;

                int separator = hostNameQualified.IndexOf('.');

                if (separator < 0)
                {
                    return hostNameQualified;
                }

                return hostNameQualified.Substring(0, separator);
            }

            return text;
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

        public string MessageText { get; set; }

        public string MessageContentType { get; set; }

        public string Label { get; set; }

        public int ReceiveTimeout { get; set; } = 1;

        public int ServerTimeout { get; set; } = 5;

        public int PrefetchCount { get; set; }

        public int TopCount { get; set; } = 10;

        public int SenderThinkTime { get; set; } = 100;

        public int ReceiverThinkTime { get; set; } = 100;

        public int MonitorRefreshInterval { get; set; } = 30;

        public bool UseAscii { get; set; } = true;

        public List<string> SelectedEntities { get; private set; } = new List<string>();
        public List<string> SelectedMessageCounts { get; private set; } = new List<string>();

        public bool ProxyOverrideDefault { get; set; }
        public string ProxyAddress { get; set; }
        public string ProxyBypassList { get; set; }
        public bool ProxyBypassOnLocal { get; set; }
        public bool ProxyUseDefaultCredentials { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyPassword { get; set; }

        public List<NodeColorInfo> NodesColors { get; set; } = new List<NodeColorInfo>();

        public BodyType MessageBodyType
        {
            get
            {
                if (!Enum.TryParse<BodyType>(messageBodyType, out var bodyType))
                {
                    bodyType = BodyType.Stream;
                }
                return bodyType;
            }
        }

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

        // ReSharper disable once FunctionComplexityOverflow
        private async Task ShowEntities(EntityType entityType)
        {
            var updating = false;

            try
            {
                if (serviceBusHelper != null && serviceBusHelper.NamespaceUri != null)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    serviceBusTreeView.SuspendDrawing();
                    serviceBusTreeView.SuspendLayout();
                    serviceBusTreeView.BeginUpdate();
                    treeNodesToLazyLoad = new List<TreeNode>();
                    var queueListNode = FindNode(Constants.QueueEntities, rootNode);
                    var topicListNode = FindNode(Constants.TopicEntities, rootNode);
                    var eventHubListNode = FindNode(Constants.EventHubEntities, rootNode);
                    var notificationHubListNode = FindNode(Constants.NotificationHubEntities, rootNode);
                    var relayServiceListNode = FindNode(Constants.RelayEntities, rootNode);
                    if (entityType == EntityType.All)
                    {
                        serviceBusTreeView.Nodes.Clear();
                        rootNode = serviceBusTreeView.Nodes.Add(serviceBusHelper.NamespaceUri.AbsoluteUri, serviceBusHelper.NamespaceUri.AbsoluteUri, AzureIconIndex, AzureIconIndex);
                        rootNode.ContextMenuStrip = rootContextMenuStrip;
                        if (SelectedEntities.Contains(Constants.QueueEntities))
                        {
                            queueListNode = rootNode.Nodes.Add(Constants.QueueEntities, Constants.QueueEntities, QueueListIconIndex, QueueListIconIndex);
                            queueListNode.ContextMenuStrip = queuesContextMenuStrip;
                        }
                        if (SelectedEntities.Contains(Constants.TopicEntities))
                        {
                            topicListNode = rootNode.Nodes.Add(Constants.TopicEntities, Constants.TopicEntities, TopicListIconIndex, TopicListIconIndex);
                            topicListNode.ContextMenuStrip = topicsContextMenuStrip;
                        }

                        // NOTE: Relays are not actually supported by Service Bus for Windows Server
                        if (serviceBusHelper.IsCloudNamespace)
                        {
                            if (SelectedEntities.Contains(Constants.EventHubEntities))
                            {
                                eventHubListNode = rootNode.Nodes.Add(Constants.EventHubEntities, Constants.EventHubEntities, EventHubListIconIndex, EventHubListIconIndex);
                                eventHubListNode.ContextMenuStrip = eventHubsContextMenuStrip;
                            }
                            if (SelectedEntities.Contains(Constants.NotificationHubEntities))
                            {
                                notificationHubListNode = rootNode.Nodes.Add(Constants.NotificationHubEntities, Constants.NotificationHubEntities, NotificationHubListIconIndex, NotificationHubListIconIndex);
                                notificationHubListNode.ContextMenuStrip = notificationHubsContextMenuStrip;
                            }
                            if (SelectedEntities.Contains(Constants.RelayEntities))
                            {
                                relayServiceListNode = rootNode.Nodes.Add(Constants.RelayEntities, Constants.RelayEntities, RelayListIconIndex, RelayListIconIndex);
                                relayServiceListNode.ContextMenuStrip = relayServicesContextMenuStrip;
                            }
                        }
                    }
                    updating = true;
                    if (serviceBusHelper.IsCloudNamespace)
                    {
                        if (SelectedEntities.Contains(Constants.EventHubEntities) &&
                            (entityType == EntityType.All ||
                            entityType == EntityType.EventHub))
                        {
                            try
                            {

                                var eventHubs = await serviceBusHelper.NamespaceManager.GetEventHubsAsync();
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
                                if (ex is AggregateException)
                                {
                                    ex = ((AggregateException)ex).InnerExceptions.First();
                                }
                                WriteToLog($"Failed to retrieve EventHub entities. Exception: {ex}");
                                serviceBusTreeView.Nodes.Remove(eventHubListNode);
                            }
                        }
                        if (SelectedEntities.Contains(Constants.NotificationHubEntities) &&
                            (entityType == EntityType.All ||
                            entityType == EntityType.NotificationHub))
                        {
                            if (serviceBusHelper.NotificationHubNamespaceManager != null)
                            {
                                try
                                {
                                    var notificationHubs = await serviceBusHelper.NotificationHubNamespaceManager.GetNotificationHubsAsync();
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
                                catch (ArgumentException)
                                {
                                    // This is where we end up if there are no Notification Hubs in the namespace
                                    serviceBusTreeView.Nodes.Remove(notificationHubListNode);
                                }
                                catch (Exception ex) when (FilterOutException(ex))
                                {
                                    if (ex is AggregateException)
                                    {
                                        ex = ((AggregateException)ex).InnerExceptions.First();
                                    }
                                    WriteToLog($"Failed to retrieve Notification Hub entities. Exception: {ex}");
                                    serviceBusTreeView.Nodes.Remove(notificationHubListNode);
                                }
                            }
                            else
                            {
                                serviceBusTreeView.Nodes.Remove(notificationHubListNode);
                            }
                        }
                        if (SelectedEntities.Contains(Constants.RelayEntities) &&
                            (entityType == EntityType.All ||
                            entityType == EntityType.Relay))
                        {
                            try
                            {
                                var relayServices = serviceBusHelper.GetRelays(MainForm.SingletonMainForm.ServerTimeout);

                                relayServiceListNode.Text = Constants.RelayEntities;

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
                                if (ex is AggregateException)
                                {
                                    ex = ((AggregateException)ex).InnerExceptions.First();
                                }
                                WriteToLog($"Failed to retrieve Relay entities. Exception: {ex}");
                                serviceBusTreeView.Nodes.Remove(relayServiceListNode);
                            }
                        }
                    }

                    if (SelectedEntities.Contains(Constants.QueueEntities) &&
                        (entityType == EntityType.All ||
                         entityType == EntityType.Queue))
                    {
                        try
                        {
                            var queues = serviceBusHelper.GetQueues(FilterExpressionHelper.QueueFilterExpression,
                                MainForm.SingletonMainForm.ServerTimeout);
                            queueListNode.Text = string.IsNullOrWhiteSpace(FilterExpressionHelper.QueueFilterExpression)
                                ? Constants.QueueEntities
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
                        catch (Exception ex) when (FilterOutException(ex))
                        {
                            if (ex is AggregateException)
                            {
                                ex = ((AggregateException)ex).InnerExceptions.First();
                            }
                            WriteToLog($"Failed to retrieve Service Bus queues. Exception: {ex}");
                            serviceBusTreeView.Nodes.Remove(queueListNode);
                        }
                    }
                    if (SelectedEntities.Contains(Constants.TopicEntities) &&
                        (entityType == EntityType.All ||
                         entityType == EntityType.Topic))
                    {
                        try
                        {
                            var topics = serviceBusHelper.GetTopics(FilterExpressionHelper.TopicFilterExpression,
                                MainForm.SingletonMainForm.ServerTimeout);
                            topicListNode.Text = string.IsNullOrWhiteSpace(FilterExpressionHelper.TopicFilterExpression)
                                ? Constants.TopicEntities
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
                                    LazyLoadNode(entityNode);
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
                            if (ex is AggregateException)
                            {
                                ex = ((AggregateException)ex).InnerExceptions.First();
                            }
                            WriteToLog($"Failed to retrieve Service Bus topics. Exception: {ex}");
                            serviceBusTreeView.Nodes.Remove(topicListNode);
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
                if (ex is AggregateException && ((AggregateException)ex).InnerExceptions.Count == 1)
                {
                    ex = ((AggregateException)ex).InnerExceptions.First();
                }
                return ex is ArgumentException || ex is WebException || ex is UnauthorizedAccessException || ex is MessagingException || ex is TimeoutException;
            }
        }

        /// <summary>
        /// If the node is in the list of nodes that still require LazyLoading then remove the node from the list and lazy load it.
        /// </summary>
        /// <param name="node"></param>
        private void EnsureNodeHasBeenLazyLoaded(TreeNode node)
        {
            if (treeNodesToLazyLoad?.Remove(node) ?? false)
            {
                LazyLoadNode(node);
            }
        }

        /// <summary>
        /// Adds a Topic node to the 
        /// </summary>
        /// <param name="entityNode">If <see cref="entityNode"/>.Tag is a <see cref="TopicDescription"/> then adds the subscriptions node,
        /// a <see cref="SubscriptionWrapper"/> node for each subscription, and an empty rules node. The <see cref="SubscriptionWrapper"/> node
        /// is added to the list of nodes to be fully lazy loaded when the subscription node is expanded.
        /// If <see cref="entityNode"/>.Tag is a <see cref="SubscriptionWrapper"/> then fully loads the rules node and its individual rules.
        /// </param>
        private void LazyLoadNode(TreeNode entityNode)
        {
            try
            {
                if (entityNode.Tag is TopicDescription)
                {
                    var topic = (TopicDescription)entityNode.Tag;
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
                                GetNameAndMessageCountText(subscription.Name, subscription.MessageCountDetails),
                                subscription.Status == EntityStatus.Active
                                    ? SubscriptionIconIndex
                                    : GreySubscriptionIconIndex,
                                subscription.Status == EntityStatus.Active
                                    ? SubscriptionIconIndex
                                    : GreySubscriptionIconIndex);
                            subscriptionNode.ContextMenuStrip = subscriptionContextMenuStrip;
                            subscriptionNode.Tag = new SubscriptionWrapper(subscription, topic);
                            // All subscription nodes have a "Rules" node, so add one so that the item appears to have children.
                            // We will Lazy Load the actual rules node if/when it is needed.
                            subscriptionNode.Nodes.Clear();
                            subscriptionNode.Nodes.Add(RuleEntities, RuleEntities, RuleListIconIndex, RuleListIconIndex);
                            WriteToLog(
                                string.Format(CultureInfo.CurrentCulture,
                                    SubscriptionRetrievedFormat, subscription.Name, topic.Path),
                                false);
                            treeNodesToLazyLoad.Add(subscriptionNode);
                            ApplyColor(subscriptionNode, true);
                        }
                    }
                }
                else if (entityNode.Tag is SubscriptionWrapper)
                {
                    var subscriptionWrapper = (SubscriptionWrapper)entityNode.Tag;
                    TreeNode subscriptionNode = entityNode;
                    subscriptionNode.Nodes.Clear();
                    var subscription = subscriptionWrapper.SubscriptionDescription;
                    var topic = subscriptionWrapper.TopicDescription;
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
                    ApplyColor(subscriptionNode, true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
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
            var savedFile = SaveEntityToFile(xml, path);
            if (savedFile != null)
            {
                WriteToLog(string.Format(EntitiesExported, savedFile));
            }
        }

        private void copyStringToClipboard(string str)
        {
            using (var form = new ClipboardForm(str))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Clipboard.SetText(str);
                }
            }
        }

        private void copyNamespaceUrlMenuItem_Click(object sender, EventArgs e)
        {
            Uri uri = serviceBusHelper.NamespaceUri;
            if (uri == null)
            {
                return;
            }
            var prettyUri = uri.AbsoluteUri[uri.AbsoluteUri.Length - 1] == '/'
                          ? uri.AbsoluteUri.Substring(0, uri.AbsoluteUri.Length - 1)
                          : uri.AbsoluteUri;
            copyStringToClipboard(prettyUri);
        }

        private void copyConnectionStringMenuItem_Click(object sender, EventArgs e)
        {
            var connectionString = serviceBusHelper.ConnectionString;
            if (connectionString == null)
            {
                return;
            }
            copyStringToClipboard(connectionString);
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
                        ApplyColor(entityNode, false);
                    }
                    else
                    {
                        if (tag is QueueDescription)
                        {
                            var queueDescription = tag as QueueDescription;
                            entityNode = entityNode.Nodes.Add(segments[i],
                                                              GetNameAndMessageCountText(segments[i], queueDescription.MessageCountDetails),
                                                              queueDescription.Status == EntityStatus.Active ? QueueIconIndex : GreyQueueIconIndex,
                                                              queueDescription.Status == EntityStatus.Active ? QueueIconIndex : GreyQueueIconIndex);
                            entityNode.ContextMenuStrip = queueContextMenuStrip;
                            entityNode.Tag = tag;
                            ApplyColor(entityNode, true);

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
                            ApplyColor(entityNode, true);

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
                            ApplyColor(entityNode, true);

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
                            ApplyColor(entityNode, true);

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
                            ApplyColor(entityNode, true);

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

        private string GetNameAndMessageCountText(string name, MessageCountDetails details)
        {
            var sb = new StringBuilder();
            sb.Append(name);
            if (showMessageCount && SelectedMessageCounts.Any())
            {
                sb.Append(" (");
                var counts = SelectedMessageCounts.Select(smc => messageCountRetriever[smc](details));
                sb.Append(string.Join(", ", counts));
                sb.Append(")");
            }
            return sb.ToString();
        }

        private void ReapplyColors(TreeNode parentNode)
        {
            if (parentNode == null)
            {
                return;
            }
            foreach (TreeNode node in parentNode.Nodes)
            {
                if (node.Tag is UrlSegmentWrapper)
                {
                    ApplyColor(node, false);
                }
                else if (node.Tag is EntityDescription)
                {
                    ApplyColor(node, true);
                }
                ReapplyColors(node);
            }
        }

        private void ApplyColor(TreeNode node, bool isLeaf)
        {
            foreach (var nodeColorInfo in NodesColors.Where(nc => nc.IsLeaf == isLeaf))
            {
                if (Regex.IsMatch(node.Name, nodeColorInfo.Text ?? string.Empty))
                {
                    node.ForeColor = nodeColorInfo.Color;
                    return;
                }
            }
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
            lstLog.Font = new Font(lstLog.Font.FontFamily, (float)logFontSize);
            serviceBusTreeView.Font = new Font(serviceBusTreeView.Font.FontFamily, (float)treeViewFontSize);
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
                MessageAndPropertiesHelper.WriteMessage(MessageText);
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

        private async void filterEntity_Click(object sender, EventArgs e)
        {
            var queueListNode = FindNode(Constants.QueueEntities, rootNode);
            var topicListNode = FindNode(Constants.TopicEntities, rootNode);

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
                            await ShowEntities(EntityType.Queue);
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
                            await ShowEntities(EntityType.Topic);
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
                            await RefreshSelectedEntity();
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

        private async void MainForm_Shown(object sender, EventArgs e)
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
                        var serviceBusNamespace = ServiceBusNamespace.GetServiceBusNamespace(item.Key, ns.ConnectionString, StaticWriteToLog);
                        serviceBusHelper.Connect(serviceBusNamespace);
                        SetTitle(serviceBusNamespace.Namespace);
                    }
                }
                if (string.Compare(argumentName, "/c", StringComparison.InvariantCultureIgnoreCase) == 0 ||
                    string.Compare(argumentName, "-c", StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    var serviceBusNamespace = ServiceBusNamespace.GetServiceBusNamespace("Manual", argumentValue, StaticWriteToLog);
                    serviceBusHelper.Connect(serviceBusNamespace);
                    SetTitle(serviceBusNamespace.Namespace);
                }
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.Window;
                await ShowEntities(EntityType.All);
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
                                                             new List<string> { "IoT Hub Connection String", "Endpoint", "Consumer Group" },
                                                             new List<string> { null, "messages/events", "$Default" },
                                                             new List<bool> { false, false, false }))
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

        private async void purgeAllMenuItem_Click(object sender, EventArgs e)
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
                        await control.PurgeAllMessagesAsync();
                    }
                }

                // Subscription Node
                if (serviceBusTreeView.SelectedNode.Tag is SubscriptionWrapper)
                {
                    var control = panelMain.Controls[0] as HandleSubscriptionControl;
                    if (control != null)
                    {
                        await control.PurgeAllMessagesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void linkLabelNewVersionAvailable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new NewVersionAvailableForm())
            {
                form.ShowDialog();
            }
        }
        #endregion

        private async void bulkPurgeAllMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await this.BulkPurge(serviceBusTreeView.SelectedNode, PurgeStrategies.All);
        }

        private async void bulkPurgeMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await this.BulkPurge(serviceBusTreeView.SelectedNode, PurgeStrategies.Messages);
        }

        private async void bulkPurgeDeadletterQueueMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await this.BulkPurge(serviceBusTreeView.SelectedNode, PurgeStrategies.DeadletteredMessages);
        }

        private async Task BulkPurge(TreeNode treeNode, PurgeStrategies bulkPurgeStrategy)
        {
            try
            {
                if (treeNode == null)
                {
                    return;
                }

                List<SubscriptionWrapper> subscriptions = new List<SubscriptionWrapper>();
                Func<TreeNode, IEnumerable<SubscriptionWrapper>> subscriptionsExtractor = tn => tn.FirstNode.Nodes.Cast<TreeNode>().Select(n => n.Tag as SubscriptionWrapper);

                List<QueueDescription> queues = new List<QueueDescription>();

                string strategyDescription = ServiceBusExplorerResources.ResourceManager.GetString($"BulkPurgeStrategy_ConfirmationMessage_{bulkPurgeStrategy}");
                string deleteConfirmation = string.Empty;

                if (treeNode == FindNode(Constants.TopicEntities, rootNode)
                    || (treeNode.Tag is UrlSegmentWrapper && (treeNode.Tag as UrlSegmentWrapper).EntityType == EntityType.Topic))
                {
                    deleteConfirmation = $"Are you sure you want to purge {strategyDescription} from all topics{(treeNode.Tag is UrlSegmentWrapper ? " in this folder" : string.Empty)}?";
                    
                    List<TreeNode> topicTreeNodes = new List<TreeNode>();
                    this.FindTopicsNodesRecursive(topicTreeNodes, treeNode);

                    subscriptions.AddRange(topicTreeNodes.SelectMany(subscriptionsExtractor));
                }
                else if (treeNode == FindNode(Constants.QueueEntities, rootNode) 
                    || (treeNode.Tag is UrlSegmentWrapper && (treeNode.Tag as UrlSegmentWrapper).EntityType == EntityType.Queue))
                {
                    deleteConfirmation = $"Are you sure you want to purge {strategyDescription} from all queues{(treeNode.Tag is UrlSegmentWrapper ? " in this folder" : string.Empty)}?";
                    this.FindQueuesRecursive(queues, treeNode);
                }
                else if (treeNode.Tag is TopicDescription)
                {
                    deleteConfirmation = $"Are you sure you want to purge {strategyDescription} from the topic {treeNode.Text}?";
                    subscriptions.AddRange(subscriptionsExtractor(treeNode));
                }

                if (!subscriptions.Any() && !queues.Any())
                {
                    return;
                }

                if (!DeleteForm.ShowAndWaitUserConfirmation(this, deleteConfirmation))
                {
                    return;
                }

                if (subscriptions.Any())
                {
                    TopicSubscriptionServiceBusPurger purger = new TopicSubscriptionServiceBusPurger(this.serviceBusHelper.GetServiceBusHelper2());
                    purger.PurgeFailed += (o, e) => this.HandleException(e.Exception);
                    purger.PurgeCompleted += (o, e) => this.WriteToLog($"[{e.TotalMessagesPurged}] messages have been purged from the{(e.IsDeadLetterQueue ? " dead-letter queue of the" : "")} [{e.EntityPath}] subscription in [{e.ElapsedMilliseconds / 1000}] seconds.");
                    await purger.Purge(bulkPurgeStrategy, await this.serviceBusHelper.GetSubscriptionProperties(subscriptions));
                }
                if (queues.Any())
                {
                    QueueServiceBusPurger purger = new QueueServiceBusPurger(this.serviceBusHelper.GetServiceBusHelper2());
                    purger.PurgeFailed += (o, e) => this.HandleException(e.Exception);
                    purger.PurgeCompleted += (o, e) => this.WriteToLog($"[{e.TotalMessagesPurged}] messages have been purged from the{(e.IsDeadLetterQueue ? " dead-letter queue of the" : "")} [{e.EntityPath}] queue in [{e.ElapsedMilliseconds / 1000}] seconds.");
                    await purger.Purge(bulkPurgeStrategy, await this.serviceBusHelper.GetQueueProperties(queues));
                }

                await RefreshSelectedEntity();
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        private void FindQueuesRecursive(List<QueueDescription> queues, TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                if (child.Tag is QueueDescription)
                    queues.Add(child.Tag as QueueDescription);
                else if (child.Tag is UrlSegmentWrapper)
                    this.FindQueuesRecursive(queues, child);
            }
        }

        private void FindTopicsNodesRecursive(List<TreeNode> topicNodes, TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                if (child.Tag is TopicDescription)
                    topicNodes.Add(child);
                else if (child.Tag is UrlSegmentWrapper)
                    this.FindTopicsNodesRecursive(topicNodes, child);
            }
        }
    }
}
