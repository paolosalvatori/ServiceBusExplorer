using ServiceBusExplorer.Controls;
using Azure.Messaging.ServiceBus;

namespace ServiceBusExplorer.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            logCollection.Dispose();
            cancellationTokenSource.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            imageList = new System.Windows.Forms.ImageList(components);
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            connectUsingSASToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            connectUsingEntraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            savedConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparatorMain = new System.Windows.Forms.ToolStripSeparator();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            createIoTHubListenerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            createEventHubListenerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            setDefaultLayouToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            logWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showCommandLineOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            splitContainer = new System.Windows.Forms.SplitContainer();
            panelTreeView = new HeaderPanel();
            serviceBusTreeView = new System.Windows.Forms.TreeView();
            panelMain = new HeaderPanel();
            mainSplitContainer = new System.Windows.Forms.SplitContainer();
            panelLog = new HeaderPanel();
            lstLog = new System.Windows.Forms.ListBox();
            logContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            clearSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            rootContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            deleteEntityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshRootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            copyNamespaceUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyConnectionStringMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator68 = new System.Windows.Forms.ToolStripSeparator();
            exportEntityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            importEntityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            queuesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            createQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator37 = new System.Windows.Forms.ToolStripSeparator();
            refreshQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator36 = new System.Windows.Forms.ToolStripSeparator();
            filterQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            exportQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator75 = new System.Windows.Forms.ToolStripSeparator();
            purgeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            messagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            deadletterQueueMessagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator76 = new System.Windows.Forms.ToolStripSeparator();
            allMessagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ruleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            removeRuleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            rulesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            addRuleMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            deleteRulesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            refreshRulesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            subscriptionsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            addSubscriptionMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            deleteSubscriptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator40 = new System.Windows.Forms.ToolStripSeparator();
            refreshSubscriptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator41 = new System.Windows.Forms.ToolStripSeparator();
            filterSubscriptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            eventGridSubscriptionsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            createEventGridSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            subscriptionContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            removeSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            duplicateSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            changeStatusSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            addRuleMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            copySubscriptionUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copySubscriptionDeadletterSubscriptionUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            testSubscriptionSDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            testSubscriptionMDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            createSubscriptionListenerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator46 = new System.Windows.Forms.ToolStripSeparator();
            subReceiveMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            subscriptionReceiveDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            subscriptionReceiveTransferDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            subReceiveToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            subscriptionPurgeMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            subscriptionPurgeDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            subscriptionPurgeAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            getSubscriptionMessageSessionsSeparator = new System.Windows.Forms.ToolStripSeparator();
            getSubscriptionMessageSessionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            topicContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            changeStatusTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            renameTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            exportTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            addSubscriptionMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            deleteTopicSubscriptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            copyTopicUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            testTopicSDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            testTopicMDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
            sendMessagesTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator70 = new System.Windows.Forms.ToolStripSeparator();
            purgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            topicPurgeMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            topicPurgeDeadletterQueueMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator73 = new System.Windows.Forms.ToolStripSeparator();
            topicPurgeAllMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            eventGridTopicContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            publishEventsTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteEventGridTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            eventGridSubscriptionContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            receiveEventsSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteEventGridSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            queueContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            changeStatusQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            renameQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            duplicateQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            exportQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            copyQueueUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyQueueDeadletterQueueUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            testQueueSDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            testQueueMDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            queueSendMessageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            createQueueListenerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            queueReceiveMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            queueReceiveDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            queueReceiveTransferDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            queueReceiveToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            queuePurgeMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            queuePurgeDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            queuePurgeAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            getQueueMessageSessionsSeparator = new System.Windows.Forms.ToolStripSeparator();
            getQueueMessageSessionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            topicsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            createTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            refreshTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            filterTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator39 = new System.Windows.Forms.ToolStripSeparator();
            exportTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator71 = new System.Windows.Forms.ToolStripSeparator();
            purgeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            messagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deadletterQueueMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator72 = new System.Windows.Forms.ToolStripSeparator();
            allMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            eventGridTopicsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            createEventGridTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            relayServicesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator65 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator67 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            openFileDialog = new System.Windows.Forms.OpenFileDialog();
            mainMenuStrip = new System.Windows.Forms.MenuStrip();
            queueFolderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            folderCreateQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            folderDeleteQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            folderExportQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator74 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
            messagesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            deadletterMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator77 = new System.Windows.Forms.ToolStripSeparator();
            allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            topicFolderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            folderCreateTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            folderDeleteTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            folderExportTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            expandSubTreeMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator78 = new System.Windows.Forms.ToolStripSeparator();
            purgeToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            messagesToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            deadletterQueueMessagesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator79 = new System.Windows.Forms.ToolStripSeparator();
            allMessagesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            relayFolderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            expandSubTreeMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            collapseSubTreeMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            notificationHubContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            deleteNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator43 = new System.Windows.Forms.ToolStripSeparator();
            exportNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator44 = new System.Windows.Forms.ToolStripSeparator();
            copyUrlNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            getRegistrationsNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            notificationHubsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            createNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteNotificationHubsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            refreshNotificationHubsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator42 = new System.Windows.Forms.ToolStripSeparator();
            exportNotificationHubsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator45 = new System.Windows.Forms.ToolStripSeparator();
            expandNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            collapseNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            eventHubContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            changeStatusEventHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteEventHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshEventHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator49 = new System.Windows.Forms.ToolStripSeparator();
            exportEventHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator50 = new System.Windows.Forms.ToolStripSeparator();
            createConsumerGroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteConsumerGroupsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator51 = new System.Windows.Forms.ToolStripSeparator();
            copyEventHubUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator52 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator53 = new System.Windows.Forms.ToolStripSeparator();
            sendMessagesEventHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            eventHubsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            createEventHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteEventHubsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            refreshEventHubsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator47 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator48 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            consumerGroupsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            createConsumerGroupMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            deleteConsumerGroupsMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator55 = new System.Windows.Forms.ToolStripSeparator();
            refreshConsumerGroupsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator56 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            partitionContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            refreshPartitionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator58 = new System.Windows.Forms.ToolStripSeparator();
            copyPartitionUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator54 = new System.Windows.Forms.ToolStripSeparator();
            createPartitionListenerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator60 = new System.Windows.Forms.ToolStripSeparator();
            sendMessagesEventHubPartitionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            partitionsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            refreshPartitionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator57 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            consumerGroupContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            deleteConsumerGroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshConsumerGroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator59 = new System.Windows.Forms.ToolStripSeparator();
            getPartitionDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator66 = new System.Windows.Forms.ToolStripSeparator();
            copyConsumerGroupUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator61 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator62 = new System.Windows.Forms.ToolStripSeparator();
            createConsumerGroupListenerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            logoPictureBox = new System.Windows.Forms.PictureBox();
            relayServiceFolderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator63 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator64 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            relayContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            deleteRelayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshRelayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            relayToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            exportRelayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            relayToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            copyRelayUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator69 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem27 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem28 = new System.Windows.Forms.ToolStripMenuItem();
            linkLabelNewVersionAvailable = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            panelTreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            panelLog.SuspendLayout();
            logContextMenuStrip.SuspendLayout();
            rootContextMenuStrip.SuspendLayout();
            queuesContextMenuStrip.SuspendLayout();
            ruleContextMenuStrip.SuspendLayout();
            rulesContextMenuStrip.SuspendLayout();
            subscriptionsContextMenuStrip.SuspendLayout();
            eventGridSubscriptionsContextMenuStrip.SuspendLayout();
            subscriptionContextMenuStrip.SuspendLayout();
            topicContextMenuStrip.SuspendLayout();
            eventGridTopicContextMenuStrip.SuspendLayout();
            eventGridSubscriptionContextMenuStrip.SuspendLayout();
            queueContextMenuStrip.SuspendLayout();
            topicsContextMenuStrip.SuspendLayout();
            eventGridTopicsContextMenuStrip.SuspendLayout();
            relayServicesContextMenuStrip.SuspendLayout();
            mainMenuStrip.SuspendLayout();
            queueFolderContextMenuStrip.SuspendLayout();
            topicFolderContextMenuStrip.SuspendLayout();
            relayFolderContextMenuStrip.SuspendLayout();
            notificationHubContextMenuStrip.SuspendLayout();
            notificationHubsContextMenuStrip.SuspendLayout();
            eventHubContextMenuStrip.SuspendLayout();
            eventHubsContextMenuStrip.SuspendLayout();
            consumerGroupsContextMenuStrip.SuspendLayout();
            partitionContextMenuStrip.SuspendLayout();
            partitionsContextMenuStrip.SuspendLayout();
            consumerGroupContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            relayServiceFolderContextMenuStrip.SuspendLayout();
            relayContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // imageList
            // 
            imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = System.Drawing.Color.Transparent;
            imageList.Images.SetKeyName(0, "Queue.ico");
            imageList.Images.SetKeyName(1, "Topic.ico");
            imageList.Images.SetKeyName(2, "Chart.ico");
            imageList.Images.SetKeyName(3, "Class.ico");
            imageList.Images.SetKeyName(4, "Add.ico");
            imageList.Images.SetKeyName(5, "UserInfo.ico");
            imageList.Images.SetKeyName(6, "exec.ico");
            imageList.Images.SetKeyName(7, "AzureLogo.ico");
            imageList.Images.SetKeyName(8, "World.png");
            imageList.Images.SetKeyName(9, "Relay.png");
            imageList.Images.SetKeyName(10, "folder_web.ico");
            imageList.Images.SetKeyName(11, "Web.ico");
            imageList.Images.SetKeyName(12, "GreyChart.ico");
            imageList.Images.SetKeyName(13, "GreyClass.ico");
            imageList.Images.SetKeyName(14, "GreyUserInfo.ico");
            imageList.Images.SetKeyName(15, "hub.png");
            imageList.Images.SetKeyName(16, "app.ico");
            imageList.Images.SetKeyName(17, "Funnel.ico");
            imageList.Images.SetKeyName(18, "EventHub.ico");
            imageList.Images.SetKeyName(19, "GreyEventHub.ico");
            imageList.Images.SetKeyName(20, "kdf.png");
            imageList.Images.SetKeyName(21, "groupofusers.ico");
            imageList.Images.SetKeyName(22, "groupofusers_grey.ico");
            imageList.Images.SetKeyName(23, "PieDiagram.ico");
            imageList.Images.SetKeyName(24, "EventGridNamespace.ico");
            imageList.Images.SetKeyName(25, "EventGridTopic.ico");
            imageList.Images.SetKeyName(26, "EventGridSubscription.ico");
            imageList.Images.SetKeyName(27, "EventGrid.ico");
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { connectUsingSASToolStripMenuItem, connectUsingEntraToolStripMenuItem, savedConnectionsToolStripMenuItem, toolStripSeparatorMain, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // connectUsingSASToolStripMenuItem
            // 
            connectUsingSASToolStripMenuItem.Name = "connectUsingSASToolStripMenuItem";
            connectUsingSASToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            connectUsingSASToolStripMenuItem.Text = "&Connect using SAS";
            connectUsingSASToolStripMenuItem.Click += connectUsingSASToolStripMenuItem_Click;
            // 
            // connectUsingEntraToolStripMenuItem
            // 
            connectUsingEntraToolStripMenuItem.Name = "connectUsingEntraToolStripMenuItem";
            connectUsingEntraToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            connectUsingEntraToolStripMenuItem.Text = "Connect using &Entra (Event grid)";
            // 
            // savedConnectionsToolStripMenuItem
            // 
            savedConnectionsToolStripMenuItem.Enabled = false;
            savedConnectionsToolStripMenuItem.Name = "savedConnectionsToolStripMenuItem";
            savedConnectionsToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            savedConnectionsToolStripMenuItem.Text = "&Saved connections";
            // 
            // toolStripSeparatorMain
            // 
            toolStripSeparatorMain.Name = "toolStripSeparatorMain";
            toolStripSeparatorMain.Size = new System.Drawing.Size(242, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4;
            exitToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { clearLogToolStripMenuItem, saveLogToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // clearLogToolStripMenuItem
            // 
            clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            clearLogToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E;
            clearLogToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            clearLogToolStripMenuItem.Text = "Clear Log";
            clearLogToolStripMenuItem.Click += clearLog_Click;
            // 
            // saveLogToolStripMenuItem
            // 
            saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
            saveLogToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
            saveLogToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            saveLogToolStripMenuItem.Text = "Save Log As...";
            saveLogToolStripMenuItem.Click += saveLogToolStripMenuItem_Click;
            // 
            // actionsToolStripMenuItem
            // 
            actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { createIoTHubListenerMenuItem, createEventHubListenerMenuItem });
            actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            actionsToolStripMenuItem.Text = "&Actions";
            // 
            // createIoTHubListenerMenuItem
            // 
            createIoTHubListenerMenuItem.Name = "createIoTHubListenerMenuItem";
            createIoTHubListenerMenuItem.Size = new System.Drawing.Size(210, 22);
            createIoTHubListenerMenuItem.Text = "Create IoT Hub Listener";
            createIoTHubListenerMenuItem.ToolTipText = "Create IoT Hub listener.";
            // 
            // createEventHubListenerMenuItem
            // 
            createEventHubListenerMenuItem.Name = "createEventHubListenerMenuItem";
            createEventHubListenerMenuItem.Size = new System.Drawing.Size(210, 22);
            createEventHubListenerMenuItem.Text = "Create Event Hub Listener";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { setDefaultLayouToolStripMenuItem, logWindowToolStripMenuItem, toolStripSeparator21, optionsToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L;
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "&View";
            // 
            // setDefaultLayouToolStripMenuItem
            // 
            setDefaultLayouToolStripMenuItem.Name = "setDefaultLayouToolStripMenuItem";
            setDefaultLayouToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D;
            setDefaultLayouToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            setDefaultLayouToolStripMenuItem.Text = "Set Default Layout";
            // 
            // logWindowToolStripMenuItem
            // 
            logWindowToolStripMenuItem.Checked = true;
            logWindowToolStripMenuItem.CheckOnClick = true;
            logWindowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            logWindowToolStripMenuItem.Name = "logWindowToolStripMenuItem";
            logWindowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L;
            logWindowToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            logWindowToolStripMenuItem.Text = "&Log Window";
            // 
            // toolStripSeparator21
            // 
            toolStripSeparator21.Name = "toolStripSeparator21";
            toolStripSeparator21.Size = new System.Drawing.Size(209, 6);
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            optionsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            optionsToolStripMenuItem.Text = "Options...";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { showCommandLineOptionsToolStripMenuItem, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // showCommandLineOptionsToolStripMenuItem
            // 
            showCommandLineOptionsToolStripMenuItem.Name = "showCommandLineOptionsToolStripMenuItem";
            showCommandLineOptionsToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            showCommandLineOptionsToolStripMenuItem.Text = "Show Command Line Options";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A;
            aboutToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            aboutToolStripMenuItem.Text = "&About Service Bus Explorer";
            // 
            // statusStrip
            // 
            statusStrip.BackColor = System.Drawing.Color.FromArgb(215, 228, 242);
            statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            statusStrip.Location = new System.Drawing.Point(0, 947);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip.Size = new System.Drawing.Size(1652, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Location = new System.Drawing.Point(0, 0);
            splitContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(panelTreeView);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(panelMain);
            splitContainer.Size = new System.Drawing.Size(1615, 652);
            splitContainer.SplitterDistance = 434;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 1;
            splitContainer.SplitterMoved += mainSplitContainer_SplitterMoved;
            // 
            // panelTreeView
            // 
            panelTreeView.AutoScroll = true;
            panelTreeView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            panelTreeView.Controls.Add(serviceBusTreeView);
            panelTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            panelTreeView.ForeColor = System.Drawing.SystemColors.Window;
            panelTreeView.HeaderColor1 = System.Drawing.Color.FromArgb(191, 205, 219);
            panelTreeView.HeaderColor2 = System.Drawing.Color.FromArgb(153, 180, 209);
            panelTreeView.HeaderFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            panelTreeView.HeaderHeight = 24;
            panelTreeView.HeaderText = "Service Bus Namespace";
            panelTreeView.Icon = (System.Drawing.Image)resources.GetObject("panelTreeView.Icon");
            panelTreeView.IconTransparentColor = System.Drawing.Color.White;
            panelTreeView.Location = new System.Drawing.Point(0, 0);
            panelTreeView.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            panelTreeView.Name = "panelTreeView";
            panelTreeView.Padding = new System.Windows.Forms.Padding(6, 33, 6, 5);
            panelTreeView.Size = new System.Drawing.Size(434, 652);
            panelTreeView.TabIndex = 0;
            // 
            // serviceBusTreeView
            // 
            serviceBusTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            serviceBusTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            serviceBusTreeView.HideSelection = false;
            serviceBusTreeView.ImageIndex = 0;
            serviceBusTreeView.ImageList = imageList;
            serviceBusTreeView.Indent = 20;
            serviceBusTreeView.ItemHeight = 20;
            serviceBusTreeView.Location = new System.Drawing.Point(6, 33);
            serviceBusTreeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            serviceBusTreeView.Name = "serviceBusTreeView";
            serviceBusTreeView.SelectedImageIndex = 0;
            serviceBusTreeView.Size = new System.Drawing.Size(422, 614);
            serviceBusTreeView.TabIndex = 0;
            serviceBusTreeView.AfterSelect += serviceBusTreeView_AfterSelect;
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
            panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMain.ForeColor = System.Drawing.SystemColors.Window;
            panelMain.HeaderColor1 = System.Drawing.Color.FromArgb(191, 205, 219);
            panelMain.HeaderColor2 = System.Drawing.Color.FromArgb(153, 180, 209);
            panelMain.HeaderFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            panelMain.HeaderHeight = 24;
            panelMain.HeaderText = "";
            panelMain.Icon = Properties.Resources.SmallWorld;
            panelMain.IconTransparentColor = System.Drawing.Color.White;
            panelMain.Location = new System.Drawing.Point(0, 0);
            panelMain.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            panelMain.Name = "panelMain";
            panelMain.Padding = new System.Windows.Forms.Padding(6, 33, 6, 5);
            panelMain.Size = new System.Drawing.Size(1176, 652);
            panelMain.TabIndex = 0;
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            mainSplitContainer.Location = new System.Drawing.Point(19, 46);
            mainSplitContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mainSplitContainer.Name = "mainSplitContainer";
            mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(splitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(panelLog);
            mainSplitContainer.Size = new System.Drawing.Size(1615, 903);
            mainSplitContainer.SplitterDistance = 652;
            mainSplitContainer.SplitterWidth = 5;
            mainSplitContainer.TabIndex = 21;
            mainSplitContainer.SplitterMoved += mainSplitContainer_SplitterMoved;
            // 
            // panelLog
            // 
            panelLog.AutoScroll = true;
            panelLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            panelLog.Controls.Add(lstLog);
            panelLog.Dock = System.Windows.Forms.DockStyle.Fill;
            panelLog.ForeColor = System.Drawing.SystemColors.Window;
            panelLog.HeaderColor1 = System.Drawing.Color.FromArgb(191, 205, 219);
            panelLog.HeaderColor2 = System.Drawing.Color.FromArgb(153, 180, 209);
            panelLog.HeaderFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            panelLog.HeaderHeight = 24;
            panelLog.HeaderText = "Log";
            panelLog.Icon = (System.Drawing.Image)resources.GetObject("panelLog.Icon");
            panelLog.IconTransparentColor = System.Drawing.Color.White;
            panelLog.Location = new System.Drawing.Point(0, 0);
            panelLog.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            panelLog.Name = "panelLog";
            panelLog.Padding = new System.Windows.Forms.Padding(6, 33, 6, 5);
            panelLog.Size = new System.Drawing.Size(1615, 246);
            panelLog.TabIndex = 0;
            // 
            // lstLog
            // 
            lstLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lstLog.ContextMenuStrip = logContextMenuStrip;
            lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            lstLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.ItemHeight = 14;
            lstLog.Location = new System.Drawing.Point(6, 33);
            lstLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lstLog.Name = "lstLog";
            lstLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            lstLog.Size = new System.Drawing.Size(1603, 208);
            lstLog.TabIndex = 0;
            // 
            // logContextMenuStrip
            // 
            logContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            logContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { copyAllToolStripMenuItem, copySelectedToolStripMenuItem, toolStripSeparator27, clearAllToolStripMenuItem, clearSelectedToolStripMenuItem, toolStripSeparator29, saveAllToolStripMenuItem, saveSelectedToolStripMenuItem });
            logContextMenuStrip.Name = "logContextMenuStrip";
            logContextMenuStrip.Size = new System.Drawing.Size(150, 148);
            // 
            // copyAllToolStripMenuItem
            // 
            copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            copyAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            copyAllToolStripMenuItem.Text = "Copy All";
            // 
            // copySelectedToolStripMenuItem
            // 
            copySelectedToolStripMenuItem.Name = "copySelectedToolStripMenuItem";
            copySelectedToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            copySelectedToolStripMenuItem.Text = "Copy Selected";
            // 
            // toolStripSeparator27
            // 
            toolStripSeparator27.Name = "toolStripSeparator27";
            toolStripSeparator27.Size = new System.Drawing.Size(146, 6);
            // 
            // clearAllToolStripMenuItem
            // 
            clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            clearAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            clearAllToolStripMenuItem.Text = "Clear All";
            // 
            // clearSelectedToolStripMenuItem
            // 
            clearSelectedToolStripMenuItem.Name = "clearSelectedToolStripMenuItem";
            clearSelectedToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            clearSelectedToolStripMenuItem.Text = "Clear Selected";
            // 
            // toolStripSeparator29
            // 
            toolStripSeparator29.Name = "toolStripSeparator29";
            toolStripSeparator29.Size = new System.Drawing.Size(146, 6);
            // 
            // saveAllToolStripMenuItem
            // 
            saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            saveAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            saveAllToolStripMenuItem.Text = "Save All";
            // 
            // saveSelectedToolStripMenuItem
            // 
            saveSelectedToolStripMenuItem.Name = "saveSelectedToolStripMenuItem";
            saveSelectedToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            saveSelectedToolStripMenuItem.Text = "Save Selected";
            // 
            // rootContextMenuStrip
            // 
            rootContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            rootContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { deleteEntityMenuItem, refreshRootMenuItem, toolStripSeparator9, copyNamespaceUrlMenuItem, copyConnectionStringMenuItem, toolStripSeparator68, exportEntityMenuItem, importEntityMenuItem, toolStripSeparator15, expandSubTreeMenuItem1, collapseSubTreeMenuItem1 });
            rootContextMenuStrip.Name = "rootContextMenuStrip";
            rootContextMenuStrip.Size = new System.Drawing.Size(202, 198);
            // 
            // deleteEntityMenuItem
            // 
            deleteEntityMenuItem.Name = "deleteEntityMenuItem";
            deleteEntityMenuItem.Size = new System.Drawing.Size(201, 22);
            deleteEntityMenuItem.Text = "Delete Entities";
            deleteEntityMenuItem.ToolTipText = "Delete the entities contained in the current namespace.";
            // 
            // refreshRootMenuItem
            // 
            refreshRootMenuItem.Name = "refreshRootMenuItem";
            refreshRootMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshRootMenuItem.Size = new System.Drawing.Size(201, 22);
            refreshRootMenuItem.Text = "Refresh Entities";
            refreshRootMenuItem.ToolTipText = "Refresh the entities contained in the current namespace.";
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new System.Drawing.Size(198, 6);
            // 
            // copyNamespaceUrlMenuItem
            // 
            copyNamespaceUrlMenuItem.Name = "copyNamespaceUrlMenuItem";
            copyNamespaceUrlMenuItem.Size = new System.Drawing.Size(201, 22);
            copyNamespaceUrlMenuItem.Text = "Copy Namespace URL";
            copyNamespaceUrlMenuItem.ToolTipText = "Copy Namespace URL to clipboard";
            // 
            // copyConnectionStringMenuItem
            // 
            copyConnectionStringMenuItem.Name = "copyConnectionStringMenuItem";
            copyConnectionStringMenuItem.Size = new System.Drawing.Size(201, 22);
            copyConnectionStringMenuItem.Text = "Copy Connection String";
            copyConnectionStringMenuItem.ToolTipText = "Copy Namespace connection string to clipboard";
            // 
            // toolStripSeparator68
            // 
            toolStripSeparator68.Name = "toolStripSeparator68";
            toolStripSeparator68.Size = new System.Drawing.Size(198, 6);
            // 
            // exportEntityMenuItem
            // 
            exportEntityMenuItem.Name = "exportEntityMenuItem";
            exportEntityMenuItem.Size = new System.Drawing.Size(201, 22);
            exportEntityMenuItem.Text = "Export Entities";
            exportEntityMenuItem.ToolTipText = "Export entity definition to file.";
            // 
            // importEntityMenuItem
            // 
            importEntityMenuItem.Name = "importEntityMenuItem";
            importEntityMenuItem.Size = new System.Drawing.Size(201, 22);
            importEntityMenuItem.Text = "Import Entities";
            importEntityMenuItem.ToolTipText = "Import entity definition from file.";
            importEntityMenuItem.Click += importEntity_Click;
            // 
            // toolStripSeparator15
            // 
            toolStripSeparator15.Name = "toolStripSeparator15";
            toolStripSeparator15.Size = new System.Drawing.Size(198, 6);
            // 
            // expandSubTreeMenuItem1
            // 
            expandSubTreeMenuItem1.Name = "expandSubTreeMenuItem1";
            expandSubTreeMenuItem1.Size = new System.Drawing.Size(201, 22);
            expandSubTreeMenuItem1.Text = "Expand Subtree";
            expandSubTreeMenuItem1.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem1
            // 
            collapseSubTreeMenuItem1.Name = "collapseSubTreeMenuItem1";
            collapseSubTreeMenuItem1.Size = new System.Drawing.Size(201, 22);
            collapseSubTreeMenuItem1.Text = "Collapse Subtree";
            collapseSubTreeMenuItem1.ToolTipText = "Collapse the subtree.";
            // 
            // queuesContextMenuStrip
            // 
            queuesContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            queuesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createQueueMenuItem, deleteQueuesMenuItem, toolStripSeparator37, refreshQueuesMenuItem, toolStripSeparator36, filterQueueMenuItem, toolStripSeparator3, exportQueuesMenuItem, toolStripSeparator14, expandSubTreeMenuItem2, collapseSubTreeMenuItem2, toolStripSeparator75, purgeToolStripMenuItem2 });
            queuesContextMenuStrip.Name = "createContextMenuStrip";
            queuesContextMenuStrip.Size = new System.Drawing.Size(176, 210);
            // 
            // createQueueMenuItem
            // 
            createQueueMenuItem.Name = "createQueueMenuItem";
            createQueueMenuItem.Size = new System.Drawing.Size(175, 22);
            createQueueMenuItem.Text = "Create Queue";
            createQueueMenuItem.ToolTipText = "Create a new queue.";
            // 
            // deleteQueuesMenuItem
            // 
            deleteQueuesMenuItem.Name = "deleteQueuesMenuItem";
            deleteQueuesMenuItem.Size = new System.Drawing.Size(175, 22);
            deleteQueuesMenuItem.Text = "Delete Queues";
            deleteQueuesMenuItem.ToolTipText = "Deletes all the queues in the current namespace.";
            // 
            // toolStripSeparator37
            // 
            toolStripSeparator37.Name = "toolStripSeparator37";
            toolStripSeparator37.Size = new System.Drawing.Size(172, 6);
            // 
            // refreshQueuesMenuItem
            // 
            refreshQueuesMenuItem.Name = "refreshQueuesMenuItem";
            refreshQueuesMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshQueuesMenuItem.Size = new System.Drawing.Size(175, 22);
            refreshQueuesMenuItem.Text = "Refresh Queues";
            refreshQueuesMenuItem.ToolTipText = "Refresh all the queues in the current namespace.";
            // 
            // toolStripSeparator36
            // 
            toolStripSeparator36.Name = "toolStripSeparator36";
            toolStripSeparator36.Size = new System.Drawing.Size(172, 6);
            // 
            // filterQueueMenuItem
            // 
            filterQueueMenuItem.Name = "filterQueueMenuItem";
            filterQueueMenuItem.Size = new System.Drawing.Size(175, 22);
            filterQueueMenuItem.Text = "Filter Queues";
            filterQueueMenuItem.ToolTipText = "Define a filter expression for queues.";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
            // 
            // exportQueuesMenuItem
            // 
            exportQueuesMenuItem.Name = "exportQueuesMenuItem";
            exportQueuesMenuItem.Size = new System.Drawing.Size(175, 22);
            exportQueuesMenuItem.Text = "Export Queues";
            exportQueuesMenuItem.ToolTipText = "Export queues definition to file.";
            // 
            // toolStripSeparator14
            // 
            toolStripSeparator14.Name = "toolStripSeparator14";
            toolStripSeparator14.Size = new System.Drawing.Size(172, 6);
            // 
            // expandSubTreeMenuItem2
            // 
            expandSubTreeMenuItem2.Name = "expandSubTreeMenuItem2";
            expandSubTreeMenuItem2.Size = new System.Drawing.Size(175, 22);
            expandSubTreeMenuItem2.Text = "Expand Subtree";
            expandSubTreeMenuItem2.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem2
            // 
            collapseSubTreeMenuItem2.Name = "collapseSubTreeMenuItem2";
            collapseSubTreeMenuItem2.Size = new System.Drawing.Size(175, 22);
            collapseSubTreeMenuItem2.Text = "Collapse Subtree";
            collapseSubTreeMenuItem2.ToolTipText = "Collapse the subtree.";
            // 
            // toolStripSeparator75
            // 
            toolStripSeparator75.Name = "toolStripSeparator75";
            toolStripSeparator75.Size = new System.Drawing.Size(172, 6);
            // 
            // purgeToolStripMenuItem2
            // 
            purgeToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { messagesToolStripMenuItem1, deadletterQueueMessagesToolStripMenuItem1, toolStripSeparator76, allMessagesToolStripMenuItem1 });
            purgeToolStripMenuItem2.Name = "purgeToolStripMenuItem2";
            purgeToolStripMenuItem2.Size = new System.Drawing.Size(175, 22);
            purgeToolStripMenuItem2.Text = "Purge";
            // 
            // messagesToolStripMenuItem1
            // 
            messagesToolStripMenuItem1.Name = "messagesToolStripMenuItem1";
            messagesToolStripMenuItem1.Size = new System.Drawing.Size(225, 22);
            messagesToolStripMenuItem1.Text = "Messages";
            // 
            // deadletterQueueMessagesToolStripMenuItem1
            // 
            deadletterQueueMessagesToolStripMenuItem1.Name = "deadletterQueueMessagesToolStripMenuItem1";
            deadletterQueueMessagesToolStripMenuItem1.Size = new System.Drawing.Size(225, 22);
            deadletterQueueMessagesToolStripMenuItem1.Text = "Dead-letter Queue Messages";
            // 
            // toolStripSeparator76
            // 
            toolStripSeparator76.Name = "toolStripSeparator76";
            toolStripSeparator76.Size = new System.Drawing.Size(222, 6);
            // 
            // allMessagesToolStripMenuItem1
            // 
            allMessagesToolStripMenuItem1.Name = "allMessagesToolStripMenuItem1";
            allMessagesToolStripMenuItem1.Size = new System.Drawing.Size(225, 22);
            allMessagesToolStripMenuItem1.Text = "All Messages";
            // 
            // ruleContextMenuStrip
            // 
            ruleContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            ruleContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { removeRuleMenuItem });
            ruleContextMenuStrip.Name = "ruleContextMenuStrip";
            ruleContextMenuStrip.Size = new System.Drawing.Size(144, 26);
            // 
            // removeRuleMenuItem
            // 
            removeRuleMenuItem.Name = "removeRuleMenuItem";
            removeRuleMenuItem.Size = new System.Drawing.Size(143, 22);
            removeRuleMenuItem.Text = "Remove Rule";
            removeRuleMenuItem.ToolTipText = "Remove the current rule.";
            // 
            // rulesContextMenuStrip
            // 
            rulesContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            rulesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { addRuleMenuItem2, deleteRulesMenuItem, toolStripSeparator10, refreshRulesMenuItem });
            rulesContextMenuStrip.Name = "rulesContextMenuStrip";
            rulesContextMenuStrip.Size = new System.Drawing.Size(164, 76);
            // 
            // addRuleMenuItem2
            // 
            addRuleMenuItem2.Name = "addRuleMenuItem2";
            addRuleMenuItem2.Size = new System.Drawing.Size(163, 22);
            addRuleMenuItem2.Text = "Add Rule";
            addRuleMenuItem2.ToolTipText = "Add a new rule.";
            // 
            // deleteRulesMenuItem
            // 
            deleteRulesMenuItem.Name = "deleteRulesMenuItem";
            deleteRulesMenuItem.Size = new System.Drawing.Size(163, 22);
            deleteRulesMenuItem.Text = "Delete Rules";
            deleteRulesMenuItem.ToolTipText = "Delete rules for the current subscription.";
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new System.Drawing.Size(160, 6);
            // 
            // refreshRulesMenuItem
            // 
            refreshRulesMenuItem.Name = "refreshRulesMenuItem";
            refreshRulesMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshRulesMenuItem.Size = new System.Drawing.Size(163, 22);
            refreshRulesMenuItem.Text = "Refresh Rules";
            refreshRulesMenuItem.ToolTipText = "Refresh rules for the current subscription.";
            // 
            // subscriptionsContextMenuStrip
            // 
            subscriptionsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            subscriptionsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { addSubscriptionMenuItem2, deleteSubscriptionsMenuItem, toolStripSeparator40, refreshSubscriptionsMenuItem, toolStripSeparator41, filterSubscriptionsMenuItem, toolStripSeparator19, expandSubTreeMenuItem6, collapseSubTreeMenuItem6 });
            subscriptionsContextMenuStrip.Name = "subscriptionsContextMenuStrip";
            subscriptionsContextMenuStrip.Size = new System.Drawing.Size(207, 154);
            // 
            // addSubscriptionMenuItem2
            // 
            addSubscriptionMenuItem2.Name = "addSubscriptionMenuItem2";
            addSubscriptionMenuItem2.Size = new System.Drawing.Size(206, 22);
            addSubscriptionMenuItem2.Text = "Create Subscription";
            addSubscriptionMenuItem2.ToolTipText = "Add a new subscription.";
            // 
            // deleteSubscriptionsMenuItem
            // 
            deleteSubscriptionsMenuItem.Name = "deleteSubscriptionsMenuItem";
            deleteSubscriptionsMenuItem.Size = new System.Drawing.Size(206, 22);
            deleteSubscriptionsMenuItem.Text = "Delete Subscriptions";
            deleteSubscriptionsMenuItem.ToolTipText = "Delete all subscription for the current topic.";
            // 
            // toolStripSeparator40
            // 
            toolStripSeparator40.Name = "toolStripSeparator40";
            toolStripSeparator40.Size = new System.Drawing.Size(203, 6);
            // 
            // refreshSubscriptionsMenuItem
            // 
            refreshSubscriptionsMenuItem.Name = "refreshSubscriptionsMenuItem";
            refreshSubscriptionsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshSubscriptionsMenuItem.Size = new System.Drawing.Size(206, 22);
            refreshSubscriptionsMenuItem.Text = "Refresh Subscriptions";
            refreshSubscriptionsMenuItem.ToolTipText = "Refresh subscriptions for the current topic.";
            // 
            // toolStripSeparator41
            // 
            toolStripSeparator41.Name = "toolStripSeparator41";
            toolStripSeparator41.Size = new System.Drawing.Size(203, 6);
            // 
            // filterSubscriptionsMenuItem
            // 
            filterSubscriptionsMenuItem.Name = "filterSubscriptionsMenuItem";
            filterSubscriptionsMenuItem.Size = new System.Drawing.Size(206, 22);
            filterSubscriptionsMenuItem.Text = "Filter Subscriptions";
            filterSubscriptionsMenuItem.ToolTipText = "Define a filter expression for  the subscriptions of the current topic.";
            // 
            // toolStripSeparator19
            // 
            toolStripSeparator19.Name = "toolStripSeparator19";
            toolStripSeparator19.Size = new System.Drawing.Size(203, 6);
            // 
            // expandSubTreeMenuItem6
            // 
            expandSubTreeMenuItem6.Name = "expandSubTreeMenuItem6";
            expandSubTreeMenuItem6.Size = new System.Drawing.Size(206, 22);
            expandSubTreeMenuItem6.Text = "Expand Subtree";
            expandSubTreeMenuItem6.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem6
            // 
            collapseSubTreeMenuItem6.Name = "collapseSubTreeMenuItem6";
            collapseSubTreeMenuItem6.Size = new System.Drawing.Size(206, 22);
            collapseSubTreeMenuItem6.Text = "Collapse Subtree";
            collapseSubTreeMenuItem6.ToolTipText = "Collapse the subtree.";
            // 
            // eventGridSubscriptionsContextMenuStrip
            // 
            eventGridSubscriptionsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            eventGridSubscriptionsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createEventGridSubscriptionMenuItem });
            eventGridSubscriptionsContextMenuStrip.Name = "createEventGridSubscriptionsContextMenuStrip";
            eventGridSubscriptionsContextMenuStrip.Size = new System.Drawing.Size(178, 26);
            // 
            // createEventGridSubscriptionMenuItem
            // 
            createEventGridSubscriptionMenuItem.Name = "createEventGridSubscriptionMenuItem";
            createEventGridSubscriptionMenuItem.Size = new System.Drawing.Size(177, 22);
            createEventGridSubscriptionMenuItem.Text = "Create Subscription";
            createEventGridSubscriptionMenuItem.ToolTipText = "Create a new subscription.";
            // 
            // subscriptionContextMenuStrip
            // 
            subscriptionContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            subscriptionContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { removeSubscriptionMenuItem, duplicateSubscriptionMenuItem, changeStatusSubscriptionMenuItem, refreshSubscriptionMenuItem, toolStripSeparator7, addRuleMenuItem1, toolStripSeparator8, copySubscriptionUrlMenuItem, copySubscriptionDeadletterSubscriptionUrlMenuItem, toolStripSeparator20, expandSubTreeMenuItem7, collapseSubTreeMenuItem7, toolStripSeparator33, testSubscriptionSDIMenuItem, testSubscriptionMDIMenuItem, toolStripSeparator28, createSubscriptionListenerMenuItem, toolStripSeparator46, subReceiveMessagesMenuItem, subscriptionReceiveDeadletterQueueMessagesMenuItem, subscriptionReceiveTransferDeadletterQueueMessagesMenuItem, subReceiveToolStripSeparator, subscriptionPurgeMessagesMenuItem, subscriptionPurgeDeadletterQueueMessagesMenuItem, subscriptionPurgeAllMenuItem, getSubscriptionMessageSessionsSeparator, getSubscriptionMessageSessionsMenuItem });
            subscriptionContextMenuStrip.Name = "subscriptionContextMenuStrip";
            subscriptionContextMenuStrip.Size = new System.Drawing.Size(314, 470);
            // 
            // removeSubscriptionMenuItem
            // 
            removeSubscriptionMenuItem.Name = "removeSubscriptionMenuItem";
            removeSubscriptionMenuItem.Size = new System.Drawing.Size(313, 22);
            removeSubscriptionMenuItem.Text = "Delete Subscription";
            removeSubscriptionMenuItem.ToolTipText = "Delete the current subscription.";
            // 
            // duplicateSubscriptionMenuItem
            // 
            duplicateSubscriptionMenuItem.Name = "duplicateSubscriptionMenuItem";
            duplicateSubscriptionMenuItem.Size = new System.Drawing.Size(313, 22);
            duplicateSubscriptionMenuItem.Text = "Duplicate Subscription";
            duplicateSubscriptionMenuItem.ToolTipText = "Duplicate current subscription including rules and actions.";
            // 
            // changeStatusSubscriptionMenuItem
            // 
            changeStatusSubscriptionMenuItem.Name = "changeStatusSubscriptionMenuItem";
            changeStatusSubscriptionMenuItem.Size = new System.Drawing.Size(313, 22);
            changeStatusSubscriptionMenuItem.Text = "Change Status Subscription";
            changeStatusSubscriptionMenuItem.Click += changeStatusEntity_Click;
            // 
            // refreshSubscriptionMenuItem
            // 
            refreshSubscriptionMenuItem.Name = "refreshSubscriptionMenuItem";
            refreshSubscriptionMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshSubscriptionMenuItem.Size = new System.Drawing.Size(313, 22);
            refreshSubscriptionMenuItem.Text = "Refresh Subscription";
            refreshSubscriptionMenuItem.ToolTipText = "Refresh the current subscription.";
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new System.Drawing.Size(310, 6);
            // 
            // addRuleMenuItem1
            // 
            addRuleMenuItem1.Name = "addRuleMenuItem1";
            addRuleMenuItem1.Size = new System.Drawing.Size(313, 22);
            addRuleMenuItem1.Text = "Add Rule";
            addRuleMenuItem1.ToolTipText = "Add a new rule.";
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new System.Drawing.Size(310, 6);
            // 
            // copySubscriptionUrlMenuItem
            // 
            copySubscriptionUrlMenuItem.Name = "copySubscriptionUrlMenuItem";
            copySubscriptionUrlMenuItem.Size = new System.Drawing.Size(313, 22);
            copySubscriptionUrlMenuItem.Text = "Copy Subscription URL";
            copySubscriptionUrlMenuItem.ToolTipText = "Copy the subscription URL to the clipboard.";
            // 
            // copySubscriptionDeadletterSubscriptionUrlMenuItem
            // 
            copySubscriptionDeadletterSubscriptionUrlMenuItem.Name = "copySubscriptionDeadletterSubscriptionUrlMenuItem";
            copySubscriptionDeadletterSubscriptionUrlMenuItem.Size = new System.Drawing.Size(313, 22);
            copySubscriptionDeadletterSubscriptionUrlMenuItem.Text = "Copy Deadletter Queue URL";
            copySubscriptionDeadletterSubscriptionUrlMenuItem.ToolTipText = "Copy the deadletter queue URL to the clipboard.";
            // 
            // toolStripSeparator20
            // 
            toolStripSeparator20.Name = "toolStripSeparator20";
            toolStripSeparator20.Size = new System.Drawing.Size(310, 6);
            // 
            // expandSubTreeMenuItem7
            // 
            expandSubTreeMenuItem7.Name = "expandSubTreeMenuItem7";
            expandSubTreeMenuItem7.Size = new System.Drawing.Size(313, 22);
            expandSubTreeMenuItem7.Text = "Expand Subtree";
            expandSubTreeMenuItem7.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem7
            // 
            collapseSubTreeMenuItem7.Name = "collapseSubTreeMenuItem7";
            collapseSubTreeMenuItem7.Size = new System.Drawing.Size(313, 22);
            collapseSubTreeMenuItem7.Text = "Collapse Subtree";
            collapseSubTreeMenuItem7.ToolTipText = "Collapse the subtree.";
            // 
            // toolStripSeparator33
            // 
            toolStripSeparator33.Name = "toolStripSeparator33";
            toolStripSeparator33.Size = new System.Drawing.Size(310, 6);
            // 
            // testSubscriptionSDIMenuItem
            // 
            testSubscriptionSDIMenuItem.Name = "testSubscriptionSDIMenuItem";
            testSubscriptionSDIMenuItem.Size = new System.Drawing.Size(313, 22);
            testSubscriptionSDIMenuItem.Text = "Test Subscription In SDI Mode";
            testSubscriptionSDIMenuItem.ToolTipText = "Test the current subscription in SDI mode.";
            // 
            // testSubscriptionMDIMenuItem
            // 
            testSubscriptionMDIMenuItem.Name = "testSubscriptionMDIMenuItem";
            testSubscriptionMDIMenuItem.Size = new System.Drawing.Size(313, 22);
            testSubscriptionMDIMenuItem.Text = "Test Subscription In MDI Mode";
            testSubscriptionMDIMenuItem.ToolTipText = "Test the current subscription in MDI mode.";
            // 
            // toolStripSeparator28
            // 
            toolStripSeparator28.Name = "toolStripSeparator28";
            toolStripSeparator28.Size = new System.Drawing.Size(310, 6);
            // 
            // createSubscriptionListenerMenuItem
            // 
            createSubscriptionListenerMenuItem.Name = "createSubscriptionListenerMenuItem";
            createSubscriptionListenerMenuItem.Size = new System.Drawing.Size(313, 22);
            createSubscriptionListenerMenuItem.Text = "Create Subscription Listener";
            createSubscriptionListenerMenuItem.ToolTipText = "Create a subscription listener.";
            // 
            // toolStripSeparator46
            // 
            toolStripSeparator46.Name = "toolStripSeparator46";
            toolStripSeparator46.Size = new System.Drawing.Size(310, 6);
            // 
            // subReceiveMessagesMenuItem
            // 
            subReceiveMessagesMenuItem.Name = "subReceiveMessagesMenuItem";
            subReceiveMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            subReceiveMessagesMenuItem.Text = "Receive Messages";
            subReceiveMessagesMenuItem.ToolTipText = "Receive messages from the current queue.";
            // 
            // subscriptionReceiveDeadletterQueueMessagesMenuItem
            // 
            subscriptionReceiveDeadletterQueueMessagesMenuItem.Name = "subscriptionReceiveDeadletterQueueMessagesMenuItem";
            subscriptionReceiveDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            subscriptionReceiveDeadletterQueueMessagesMenuItem.Text = "Receive Dead-letter Queue Messages";
            subscriptionReceiveDeadletterQueueMessagesMenuItem.ToolTipText = "Receive messages from the deadletter queue.";
            // 
            // subscriptionReceiveTransferDeadletterQueueMessagesMenuItem
            // 
            subscriptionReceiveTransferDeadletterQueueMessagesMenuItem.Name = "subscriptionReceiveTransferDeadletterQueueMessagesMenuItem";
            subscriptionReceiveTransferDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            subscriptionReceiveTransferDeadletterQueueMessagesMenuItem.Text = "Receive Transfer Dead-letter Queue Messages";
            // 
            // subReceiveToolStripSeparator
            // 
            subReceiveToolStripSeparator.Name = "subReceiveToolStripSeparator";
            subReceiveToolStripSeparator.Size = new System.Drawing.Size(310, 6);
            // 
            // subscriptionPurgeMessagesMenuItem
            // 
            subscriptionPurgeMessagesMenuItem.Name = "subscriptionPurgeMessagesMenuItem";
            subscriptionPurgeMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            subscriptionPurgeMessagesMenuItem.Text = "Purge Messages";
            // 
            // subscriptionPurgeDeadletterQueueMessagesMenuItem
            // 
            subscriptionPurgeDeadletterQueueMessagesMenuItem.Name = "subscriptionPurgeDeadletterQueueMessagesMenuItem";
            subscriptionPurgeDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            subscriptionPurgeDeadletterQueueMessagesMenuItem.Text = "Purge Dead-letter Queue Messages";
            // 
            // subscriptionPurgeAllMenuItem
            // 
            subscriptionPurgeAllMenuItem.Name = "subscriptionPurgeAllMenuItem";
            subscriptionPurgeAllMenuItem.Size = new System.Drawing.Size(313, 22);
            subscriptionPurgeAllMenuItem.Text = "Purge All Messages";
            // 
            // getSubscriptionMessageSessionsSeparator
            // 
            getSubscriptionMessageSessionsSeparator.Name = "getSubscriptionMessageSessionsSeparator";
            getSubscriptionMessageSessionsSeparator.Size = new System.Drawing.Size(310, 6);
            // 
            // getSubscriptionMessageSessionsMenuItem
            // 
            getSubscriptionMessageSessionsMenuItem.Name = "getSubscriptionMessageSessionsMenuItem";
            getSubscriptionMessageSessionsMenuItem.Size = new System.Drawing.Size(313, 22);
            getSubscriptionMessageSessionsMenuItem.Text = "Get Message Sessions";
            getSubscriptionMessageSessionsMenuItem.ToolTipText = "Retrieves all message sessions whose session state was updated since lastUpdatedTime.";
            // 
            // topicContextMenuStrip
            // 
            topicContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            topicContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { changeStatusTopicMenuItem, deleteTopicMenuItem, refreshTopicMenuItem, renameTopicMenuItem, toolStripSeparator6, exportTopicMenuItem, toolStripSeparator2, addSubscriptionMenuItem1, deleteTopicSubscriptionsMenuItem, toolStripSeparator12, copyTopicUrlMenuItem, toolStripSeparator18, expandSubTreeMenuItem5, collapseSubTreeMenuItem5, toolStripSeparator32, testTopicSDIMenuItem, testTopicMDIMenuItem, toolStripSeparator35, sendMessagesTopicMenuItem, toolStripSeparator70, purgeToolStripMenuItem });
            topicContextMenuStrip.Name = "topicContextMenuStrip";
            topicContextMenuStrip.Size = new System.Drawing.Size(200, 354);
            // 
            // changeStatusTopicMenuItem
            // 
            changeStatusTopicMenuItem.Name = "changeStatusTopicMenuItem";
            changeStatusTopicMenuItem.Size = new System.Drawing.Size(199, 22);
            changeStatusTopicMenuItem.Text = "Change Status Topic";
            changeStatusTopicMenuItem.Click += changeStatusEntity_Click;
            // 
            // deleteTopicMenuItem
            // 
            deleteTopicMenuItem.Name = "deleteTopicMenuItem";
            deleteTopicMenuItem.Size = new System.Drawing.Size(199, 22);
            deleteTopicMenuItem.Text = "Delete Topic";
            deleteTopicMenuItem.ToolTipText = "Delete the current topic.";
            // 
            // refreshTopicMenuItem
            // 
            refreshTopicMenuItem.Name = "refreshTopicMenuItem";
            refreshTopicMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshTopicMenuItem.Size = new System.Drawing.Size(199, 22);
            refreshTopicMenuItem.Text = "Refresh Topic";
            refreshTopicMenuItem.ToolTipText = "Refresh the current topic.";
            // 
            // renameTopicMenuItem
            // 
            renameTopicMenuItem.Name = "renameTopicMenuItem";
            renameTopicMenuItem.Size = new System.Drawing.Size(199, 22);
            renameTopicMenuItem.Text = "Rename Topic";
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new System.Drawing.Size(196, 6);
            // 
            // exportTopicMenuItem
            // 
            exportTopicMenuItem.Name = "exportTopicMenuItem";
            exportTopicMenuItem.Size = new System.Drawing.Size(199, 22);
            exportTopicMenuItem.Text = "Export Topic";
            exportTopicMenuItem.ToolTipText = "Export topic definition to file.";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
            // 
            // addSubscriptionMenuItem1
            // 
            addSubscriptionMenuItem1.Name = "addSubscriptionMenuItem1";
            addSubscriptionMenuItem1.Size = new System.Drawing.Size(199, 22);
            addSubscriptionMenuItem1.Text = "Create Subscription";
            addSubscriptionMenuItem1.ToolTipText = "Create a new subscription to the current topic.";
            // 
            // deleteTopicSubscriptionsMenuItem
            // 
            deleteTopicSubscriptionsMenuItem.Name = "deleteTopicSubscriptionsMenuItem";
            deleteTopicSubscriptionsMenuItem.Size = new System.Drawing.Size(199, 22);
            deleteTopicSubscriptionsMenuItem.Text = "Delete Subscriptions";
            deleteTopicSubscriptionsMenuItem.ToolTipText = "Delete all subscription for the current topic.";
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new System.Drawing.Size(196, 6);
            // 
            // copyTopicUrlMenuItem
            // 
            copyTopicUrlMenuItem.Name = "copyTopicUrlMenuItem";
            copyTopicUrlMenuItem.Size = new System.Drawing.Size(199, 22);
            copyTopicUrlMenuItem.Text = "Copy Topic URL";
            copyTopicUrlMenuItem.ToolTipText = "Copy the topic URL to the clipboard.";
            // 
            // toolStripSeparator18
            // 
            toolStripSeparator18.Name = "toolStripSeparator18";
            toolStripSeparator18.Size = new System.Drawing.Size(196, 6);
            // 
            // expandSubTreeMenuItem5
            // 
            expandSubTreeMenuItem5.Name = "expandSubTreeMenuItem5";
            expandSubTreeMenuItem5.Size = new System.Drawing.Size(199, 22);
            expandSubTreeMenuItem5.Text = "Expand Subtree";
            expandSubTreeMenuItem5.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem5
            // 
            collapseSubTreeMenuItem5.Name = "collapseSubTreeMenuItem5";
            collapseSubTreeMenuItem5.Size = new System.Drawing.Size(199, 22);
            collapseSubTreeMenuItem5.Text = "Collapse Subtree";
            collapseSubTreeMenuItem5.ToolTipText = "Collapse the subtree.";
            // 
            // toolStripSeparator32
            // 
            toolStripSeparator32.Name = "toolStripSeparator32";
            toolStripSeparator32.Size = new System.Drawing.Size(196, 6);
            // 
            // testTopicSDIMenuItem
            // 
            testTopicSDIMenuItem.Name = "testTopicSDIMenuItem";
            testTopicSDIMenuItem.Size = new System.Drawing.Size(199, 22);
            testTopicSDIMenuItem.Text = "Test Topic In SDI Mode";
            testTopicSDIMenuItem.ToolTipText = "Test the current topic in SDI mode.";
            // 
            // testTopicMDIMenuItem
            // 
            testTopicMDIMenuItem.Name = "testTopicMDIMenuItem";
            testTopicMDIMenuItem.Size = new System.Drawing.Size(199, 22);
            testTopicMDIMenuItem.Text = "Test Topic In MDI Mode";
            testTopicMDIMenuItem.ToolTipText = "Test the current topic in MDI mode.";
            // 
            // toolStripSeparator35
            // 
            toolStripSeparator35.Name = "toolStripSeparator35";
            toolStripSeparator35.Size = new System.Drawing.Size(196, 6);
            // 
            // sendMessagesTopicMenuItem
            // 
            sendMessagesTopicMenuItem.Name = "sendMessagesTopicMenuItem";
            sendMessagesTopicMenuItem.Size = new System.Drawing.Size(199, 22);
            sendMessagesTopicMenuItem.Text = "Send Messages";
            sendMessagesTopicMenuItem.ToolTipText = "Send test messages to the current topic.";
            // 
            // toolStripSeparator70
            // 
            toolStripSeparator70.Name = "toolStripSeparator70";
            toolStripSeparator70.Size = new System.Drawing.Size(196, 6);
            // 
            // purgeToolStripMenuItem
            // 
            purgeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { topicPurgeMessagesToolStripMenuItem, topicPurgeDeadletterQueueMessagesToolStripMenuItem, toolStripSeparator73, topicPurgeAllMessagesToolStripMenuItem });
            purgeToolStripMenuItem.Name = "purgeToolStripMenuItem";
            purgeToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            purgeToolStripMenuItem.Text = "Purge";
            // 
            // topicPurgeMessagesToolStripMenuItem
            // 
            topicPurgeMessagesToolStripMenuItem.Name = "topicPurgeMessagesToolStripMenuItem";
            topicPurgeMessagesToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            topicPurgeMessagesToolStripMenuItem.Text = "Messages";
            // 
            // topicPurgeDeadletterQueueMessagesToolStripMenuItem
            // 
            topicPurgeDeadletterQueueMessagesToolStripMenuItem.Name = "topicPurgeDeadletterQueueMessagesToolStripMenuItem";
            topicPurgeDeadletterQueueMessagesToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            topicPurgeDeadletterQueueMessagesToolStripMenuItem.Text = "Dead-letter Queue Messages";
            // 
            // toolStripSeparator73
            // 
            toolStripSeparator73.Name = "toolStripSeparator73";
            toolStripSeparator73.Size = new System.Drawing.Size(222, 6);
            // 
            // topicPurgeAllMessagesToolStripMenuItem
            // 
            topicPurgeAllMessagesToolStripMenuItem.Name = "topicPurgeAllMessagesToolStripMenuItem";
            topicPurgeAllMessagesToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            topicPurgeAllMessagesToolStripMenuItem.Text = "All Messages";
            // 
            // eventGridTopicContextMenuStrip
            // 
            eventGridTopicContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            eventGridTopicContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { publishEventsTopicMenuItem, deleteEventGridTopicMenuItem });
            eventGridTopicContextMenuStrip.Name = "eventGridTopicContextMenuStrip";
            eventGridTopicContextMenuStrip.Size = new System.Drawing.Size(146, 48);
            // 
            // publishEventsTopicMenuItem
            // 
            publishEventsTopicMenuItem.Name = "publishEventsTopicMenuItem";
            publishEventsTopicMenuItem.Size = new System.Drawing.Size(145, 22);
            publishEventsTopicMenuItem.Text = "Publish Event";
            publishEventsTopicMenuItem.ToolTipText = "Publish event to the current topic.";
            // 
            // deleteEventGridTopicMenuItem
            // 
            deleteEventGridTopicMenuItem.Name = "deleteEventGridTopicMenuItem";
            deleteEventGridTopicMenuItem.Size = new System.Drawing.Size(145, 22);
            deleteEventGridTopicMenuItem.Text = "Delete Topic";
            deleteEventGridTopicMenuItem.ToolTipText = "Delete current topic.";
            // 
            // eventGridSubscriptionContextMenuStrip
            // 
            eventGridSubscriptionContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            eventGridSubscriptionContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { receiveEventsSubscriptionMenuItem, deleteEventGridSubscriptionMenuItem });
            eventGridSubscriptionContextMenuStrip.Name = "eventGridSubscriptionContextMenuStrip";
            eventGridSubscriptionContextMenuStrip.Size = new System.Drawing.Size(177, 48);
            // 
            // receiveEventsSubscriptionMenuItem
            // 
            receiveEventsSubscriptionMenuItem.Name = "receiveEventsSubscriptionMenuItem";
            receiveEventsSubscriptionMenuItem.Size = new System.Drawing.Size(176, 22);
            receiveEventsSubscriptionMenuItem.Text = "Receive Events";
            receiveEventsSubscriptionMenuItem.ToolTipText = "Receive events from the current topic.";
            // 
            // deleteEventGridSubscriptionMenuItem
            // 
            deleteEventGridSubscriptionMenuItem.Name = "deleteEventGridSubscriptionMenuItem";
            deleteEventGridSubscriptionMenuItem.Size = new System.Drawing.Size(176, 22);
            deleteEventGridSubscriptionMenuItem.Text = "Delete Subscription";
            deleteEventGridSubscriptionMenuItem.ToolTipText = "Delete current subscription.";
            // 
            // queueContextMenuStrip
            // 
            queueContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            queueContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { changeStatusQueueMenuItem, deleteQueueMenuItem, refreshQueueMenuItem, renameQueueMenuItem, duplicateQueueMenuItem, toolStripSeparator5, exportQueueMenuItem, toolStripSeparator11, copyQueueUrlMenuItem, copyQueueDeadletterQueueUrlMenuItem, toolStripSeparator25, testQueueSDIMenuItem, testQueueMDIMenuItem, toolStripSeparator34, queueSendMessageMenuItem, createQueueListenerMenuItem, toolStripSeparator31, queueReceiveMessagesMenuItem, queueReceiveDeadletterQueueMessagesMenuItem, queueReceiveTransferDeadletterQueueMessagesMenuItem, queueReceiveToolStripSeparator, queuePurgeMessagesMenuItem, queuePurgeDeadletterQueueMessagesMenuItem, queuePurgeAllMenuItem, getQueueMessageSessionsSeparator, getQueueMessageSessionsMenuItem });
            queueContextMenuStrip.Name = "nodeContextMenuStrip";
            queueContextMenuStrip.Size = new System.Drawing.Size(314, 464);
            // 
            // changeStatusQueueMenuItem
            // 
            changeStatusQueueMenuItem.Name = "changeStatusQueueMenuItem";
            changeStatusQueueMenuItem.Size = new System.Drawing.Size(313, 22);
            changeStatusQueueMenuItem.Text = "Set Status";
            // 
            // deleteQueueMenuItem
            // 
            deleteQueueMenuItem.Name = "deleteQueueMenuItem";
            deleteQueueMenuItem.Size = new System.Drawing.Size(313, 22);
            deleteQueueMenuItem.Text = "Delete Queue";
            deleteQueueMenuItem.ToolTipText = "Delete the current queue.";
            // 
            // refreshQueueMenuItem
            // 
            refreshQueueMenuItem.Name = "refreshQueueMenuItem";
            refreshQueueMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshQueueMenuItem.Size = new System.Drawing.Size(313, 22);
            refreshQueueMenuItem.Text = "Refresh Queue";
            refreshQueueMenuItem.ToolTipText = "Refresh the current queue.";
            // 
            // renameQueueMenuItem
            // 
            renameQueueMenuItem.Name = "renameQueueMenuItem";
            renameQueueMenuItem.Size = new System.Drawing.Size(313, 22);
            renameQueueMenuItem.Text = "Rename Queue";
            // 
            // duplicateQueueMenuItem
            // 
            duplicateQueueMenuItem.Name = "duplicateQueueMenuItem";
            duplicateQueueMenuItem.Size = new System.Drawing.Size(313, 22);
            duplicateQueueMenuItem.Text = "Duplicate Queue";
            duplicateQueueMenuItem.ToolTipText = "Duplicate the selected Queue not including Authorization Rules.";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(310, 6);
            // 
            // exportQueueMenuItem
            // 
            exportQueueMenuItem.Name = "exportQueueMenuItem";
            exportQueueMenuItem.Size = new System.Drawing.Size(313, 22);
            exportQueueMenuItem.Text = "Export Queue";
            exportQueueMenuItem.ToolTipText = "Export queue definition to file.";
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new System.Drawing.Size(310, 6);
            // 
            // copyQueueUrlMenuItem
            // 
            copyQueueUrlMenuItem.Name = "copyQueueUrlMenuItem";
            copyQueueUrlMenuItem.Size = new System.Drawing.Size(313, 22);
            copyQueueUrlMenuItem.Text = "Copy Queue URL";
            copyQueueUrlMenuItem.ToolTipText = "Copy the queue URL to the clipboard.";
            // 
            // copyQueueDeadletterQueueUrlMenuItem
            // 
            copyQueueDeadletterQueueUrlMenuItem.Name = "copyQueueDeadletterQueueUrlMenuItem";
            copyQueueDeadletterQueueUrlMenuItem.Size = new System.Drawing.Size(313, 22);
            copyQueueDeadletterQueueUrlMenuItem.Text = "Copy Deadletter Queue URL";
            copyQueueDeadletterQueueUrlMenuItem.ToolTipText = "Copy the deadletter queue URL to the clipboard.";
            // 
            // toolStripSeparator25
            // 
            toolStripSeparator25.Name = "toolStripSeparator25";
            toolStripSeparator25.Size = new System.Drawing.Size(310, 6);
            // 
            // testQueueSDIMenuItem
            // 
            testQueueSDIMenuItem.Name = "testQueueSDIMenuItem";
            testQueueSDIMenuItem.Size = new System.Drawing.Size(313, 22);
            testQueueSDIMenuItem.Text = "Test Queue In SDI Mode";
            testQueueSDIMenuItem.ToolTipText = "Test the current queue in SDI mode.";
            // 
            // testQueueMDIMenuItem
            // 
            testQueueMDIMenuItem.Name = "testQueueMDIMenuItem";
            testQueueMDIMenuItem.Size = new System.Drawing.Size(313, 22);
            testQueueMDIMenuItem.Text = "Test Queue In MDI Mode";
            testQueueMDIMenuItem.ToolTipText = "Test the current queue in MDI mode.";
            // 
            // toolStripSeparator34
            // 
            toolStripSeparator34.Name = "toolStripSeparator34";
            toolStripSeparator34.Size = new System.Drawing.Size(310, 6);
            // 
            // queueSendMessageMenuItem
            // 
            queueSendMessageMenuItem.Name = "queueSendMessageMenuItem";
            queueSendMessageMenuItem.Size = new System.Drawing.Size(313, 22);
            queueSendMessageMenuItem.Text = "Send Messages";
            queueSendMessageMenuItem.ToolTipText = "Send test messages to the current queue.";
            // 
            // createQueueListenerMenuItem
            // 
            createQueueListenerMenuItem.Name = "createQueueListenerMenuItem";
            createQueueListenerMenuItem.Size = new System.Drawing.Size(313, 22);
            createQueueListenerMenuItem.Text = "Create Queue Listener";
            createQueueListenerMenuItem.ToolTipText = "Create a queue listener.";
            // 
            // toolStripSeparator31
            // 
            toolStripSeparator31.Name = "toolStripSeparator31";
            toolStripSeparator31.Size = new System.Drawing.Size(310, 6);
            // 
            // queueReceiveMessagesMenuItem
            // 
            queueReceiveMessagesMenuItem.Name = "queueReceiveMessagesMenuItem";
            queueReceiveMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            queueReceiveMessagesMenuItem.Text = "Receive Messages";
            queueReceiveMessagesMenuItem.ToolTipText = "Receive messages from the current queue.";
            // 
            // queueReceiveDeadletterQueueMessagesMenuItem
            // 
            queueReceiveDeadletterQueueMessagesMenuItem.Name = "queueReceiveDeadletterQueueMessagesMenuItem";
            queueReceiveDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            queueReceiveDeadletterQueueMessagesMenuItem.Text = "Receive Dead-letter Queue Messages";
            queueReceiveDeadletterQueueMessagesMenuItem.ToolTipText = "Receive messages from the deadletter queue.";
            // 
            // queueReceiveTransferDeadletterQueueMessagesMenuItem
            // 
            queueReceiveTransferDeadletterQueueMessagesMenuItem.Name = "queueReceiveTransferDeadletterQueueMessagesMenuItem";
            queueReceiveTransferDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            queueReceiveTransferDeadletterQueueMessagesMenuItem.Text = "Receive Transfer Dead-letter Queue Messages";
            // 
            // queueReceiveToolStripSeparator
            // 
            queueReceiveToolStripSeparator.Name = "queueReceiveToolStripSeparator";
            queueReceiveToolStripSeparator.Size = new System.Drawing.Size(310, 6);
            // 
            // queuePurgeMessagesMenuItem
            // 
            queuePurgeMessagesMenuItem.Name = "queuePurgeMessagesMenuItem";
            queuePurgeMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            queuePurgeMessagesMenuItem.Text = "Purge Messages";
            // 
            // queuePurgeDeadletterQueueMessagesMenuItem
            // 
            queuePurgeDeadletterQueueMessagesMenuItem.Name = "queuePurgeDeadletterQueueMessagesMenuItem";
            queuePurgeDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(313, 22);
            queuePurgeDeadletterQueueMessagesMenuItem.Text = "Purge Dead-letter Queue Messages";
            // 
            // queuePurgeAllMenuItem
            // 
            queuePurgeAllMenuItem.Name = "queuePurgeAllMenuItem";
            queuePurgeAllMenuItem.Size = new System.Drawing.Size(313, 22);
            queuePurgeAllMenuItem.Text = "Purge All Messages";
            // 
            // getQueueMessageSessionsSeparator
            // 
            getQueueMessageSessionsSeparator.Name = "getQueueMessageSessionsSeparator";
            getQueueMessageSessionsSeparator.Size = new System.Drawing.Size(310, 6);
            // 
            // getQueueMessageSessionsMenuItem
            // 
            getQueueMessageSessionsMenuItem.Name = "getQueueMessageSessionsMenuItem";
            getQueueMessageSessionsMenuItem.Size = new System.Drawing.Size(313, 22);
            getQueueMessageSessionsMenuItem.Text = "Get Message Sessions";
            getQueueMessageSessionsMenuItem.ToolTipText = "Retrieves all message sessions whose session state was updated since lastUpdatedTime.";
            // 
            // topicsContextMenuStrip
            // 
            topicsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            topicsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createTopicMenuItem, deleteTopicsMenuItem, toolStripSeparator38, refreshTopicsMenuItem, toolStripSeparator4, filterTopicsMenuItem, toolStripSeparator39, exportTopicsMenuItem, toolStripSeparator16, expandSubTreeMenuItem3, collapseSubTreeMenuItem3, toolStripSeparator71, purgeToolStripMenuItem1 });
            topicsContextMenuStrip.Name = "createContextMenuStrip";
            topicsContextMenuStrip.Size = new System.Drawing.Size(170, 210);
            // 
            // createTopicMenuItem
            // 
            createTopicMenuItem.Name = "createTopicMenuItem";
            createTopicMenuItem.Size = new System.Drawing.Size(169, 22);
            createTopicMenuItem.Text = "Create Topic";
            createTopicMenuItem.ToolTipText = "Create a new topic.";
            // 
            // deleteTopicsMenuItem
            // 
            deleteTopicsMenuItem.Name = "deleteTopicsMenuItem";
            deleteTopicsMenuItem.Size = new System.Drawing.Size(169, 22);
            deleteTopicsMenuItem.Text = "Delete Topics";
            deleteTopicsMenuItem.ToolTipText = "Delete all the topics in the current namespace.";
            // 
            // toolStripSeparator38
            // 
            toolStripSeparator38.Name = "toolStripSeparator38";
            toolStripSeparator38.Size = new System.Drawing.Size(166, 6);
            // 
            // refreshTopicsMenuItem
            // 
            refreshTopicsMenuItem.Name = "refreshTopicsMenuItem";
            refreshTopicsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshTopicsMenuItem.Size = new System.Drawing.Size(169, 22);
            refreshTopicsMenuItem.Text = "Refresh Topics";
            refreshTopicsMenuItem.ToolTipText = "Refresh all the topics in the current namespace.";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
            // 
            // filterTopicsMenuItem
            // 
            filterTopicsMenuItem.Name = "filterTopicsMenuItem";
            filterTopicsMenuItem.Size = new System.Drawing.Size(169, 22);
            filterTopicsMenuItem.Text = "Filter Topics";
            filterTopicsMenuItem.ToolTipText = "Define a filter expression for topics.";
            // 
            // toolStripSeparator39
            // 
            toolStripSeparator39.Name = "toolStripSeparator39";
            toolStripSeparator39.Size = new System.Drawing.Size(166, 6);
            // 
            // exportTopicsMenuItem
            // 
            exportTopicsMenuItem.Name = "exportTopicsMenuItem";
            exportTopicsMenuItem.Size = new System.Drawing.Size(169, 22);
            exportTopicsMenuItem.Text = "Export Topics";
            exportTopicsMenuItem.ToolTipText = "Export topics definition to file.";
            // 
            // toolStripSeparator16
            // 
            toolStripSeparator16.Name = "toolStripSeparator16";
            toolStripSeparator16.Size = new System.Drawing.Size(166, 6);
            // 
            // expandSubTreeMenuItem3
            // 
            expandSubTreeMenuItem3.Name = "expandSubTreeMenuItem3";
            expandSubTreeMenuItem3.Size = new System.Drawing.Size(169, 22);
            expandSubTreeMenuItem3.Text = "Expand Subtree";
            expandSubTreeMenuItem3.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem3
            // 
            collapseSubTreeMenuItem3.Name = "collapseSubTreeMenuItem3";
            collapseSubTreeMenuItem3.Size = new System.Drawing.Size(169, 22);
            collapseSubTreeMenuItem3.Text = "Collapse Subtree";
            collapseSubTreeMenuItem3.ToolTipText = "Collapse the subtree.";
            // 
            // toolStripSeparator71
            // 
            toolStripSeparator71.Name = "toolStripSeparator71";
            toolStripSeparator71.Size = new System.Drawing.Size(166, 6);
            // 
            // purgeToolStripMenuItem1
            // 
            purgeToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { messagesToolStripMenuItem, deadletterQueueMessagesToolStripMenuItem, toolStripSeparator72, allMessagesToolStripMenuItem });
            purgeToolStripMenuItem1.Name = "purgeToolStripMenuItem1";
            purgeToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            purgeToolStripMenuItem1.Text = "Purge";
            // 
            // messagesToolStripMenuItem
            // 
            messagesToolStripMenuItem.Name = "messagesToolStripMenuItem";
            messagesToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            messagesToolStripMenuItem.Text = "Messages";
            // 
            // deadletterQueueMessagesToolStripMenuItem
            // 
            deadletterQueueMessagesToolStripMenuItem.Name = "deadletterQueueMessagesToolStripMenuItem";
            deadletterQueueMessagesToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            deadletterQueueMessagesToolStripMenuItem.Text = "Dead-letter Queue Messages";
            // 
            // toolStripSeparator72
            // 
            toolStripSeparator72.Name = "toolStripSeparator72";
            toolStripSeparator72.Size = new System.Drawing.Size(222, 6);
            // 
            // allMessagesToolStripMenuItem
            // 
            allMessagesToolStripMenuItem.Name = "allMessagesToolStripMenuItem";
            allMessagesToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            allMessagesToolStripMenuItem.Text = "All Messages";
            // 
            // eventGridTopicsContextMenuStrip
            // 
            eventGridTopicsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            eventGridTopicsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createEventGridTopicMenuItem });
            eventGridTopicsContextMenuStrip.Name = "createEventGridTopicsContextMenuStrip";
            eventGridTopicsContextMenuStrip.Size = new System.Drawing.Size(141, 26);
            // 
            // createEventGridTopicMenuItem
            // 
            createEventGridTopicMenuItem.Name = "createEventGridTopicMenuItem";
            createEventGridTopicMenuItem.Size = new System.Drawing.Size(140, 22);
            createEventGridTopicMenuItem.Text = "Create Topic";
            createEventGridTopicMenuItem.ToolTipText = "Create a new topic.";
            // 
            // relayServicesContextMenuStrip
            // 
            relayServicesContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            relayServicesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem15, toolStripMenuItem16, toolStripSeparator65, toolStripMenuItem19, toolStripSeparator67, toolStripMenuItem21, toolStripSeparator24, expandSubTreeMenuItem11, collapseSubTreeMenuItem11 });
            relayServicesContextMenuStrip.Name = "relayServicesContextMenuStrip";
            relayServicesContextMenuStrip.Size = new System.Drawing.Size(169, 154);
            // 
            // toolStripMenuItem15
            // 
            toolStripMenuItem15.Name = "toolStripMenuItem15";
            toolStripMenuItem15.Size = new System.Drawing.Size(168, 22);
            toolStripMenuItem15.Text = "Create Relay";
            toolStripMenuItem15.ToolTipText = "Create a new relay.";
            // 
            // toolStripMenuItem16
            // 
            toolStripMenuItem16.Name = "toolStripMenuItem16";
            toolStripMenuItem16.Size = new System.Drawing.Size(168, 22);
            toolStripMenuItem16.Text = "Delete Relays";
            toolStripMenuItem16.ToolTipText = "Deletes all the services in the current namespace.";
            // 
            // toolStripSeparator65
            // 
            toolStripSeparator65.Name = "toolStripSeparator65";
            toolStripSeparator65.Size = new System.Drawing.Size(165, 6);
            // 
            // toolStripMenuItem19
            // 
            toolStripMenuItem19.Name = "toolStripMenuItem19";
            toolStripMenuItem19.ShortcutKeys = System.Windows.Forms.Keys.F5;
            toolStripMenuItem19.Size = new System.Drawing.Size(168, 22);
            toolStripMenuItem19.Text = "Refresh Relays";
            toolStripMenuItem19.ToolTipText = "Refresh all the relays in the current namespace.";
            // 
            // toolStripSeparator67
            // 
            toolStripSeparator67.Name = "toolStripSeparator67";
            toolStripSeparator67.Size = new System.Drawing.Size(165, 6);
            // 
            // toolStripMenuItem21
            // 
            toolStripMenuItem21.Name = "toolStripMenuItem21";
            toolStripMenuItem21.Size = new System.Drawing.Size(168, 22);
            toolStripMenuItem21.Text = "Export Relays";
            toolStripMenuItem21.ToolTipText = "Export relays definition to file.";
            // 
            // toolStripSeparator24
            // 
            toolStripSeparator24.Name = "toolStripSeparator24";
            toolStripSeparator24.Size = new System.Drawing.Size(165, 6);
            // 
            // expandSubTreeMenuItem11
            // 
            expandSubTreeMenuItem11.Name = "expandSubTreeMenuItem11";
            expandSubTreeMenuItem11.Size = new System.Drawing.Size(168, 22);
            expandSubTreeMenuItem11.Text = "Expand Subtree";
            expandSubTreeMenuItem11.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem11
            // 
            collapseSubTreeMenuItem11.Name = "collapseSubTreeMenuItem11";
            collapseSubTreeMenuItem11.Size = new System.Drawing.Size(168, 22);
            collapseSubTreeMenuItem11.Text = "Collapse Subtree";
            collapseSubTreeMenuItem11.ToolTipText = "Collapse the subtree.";
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.BackColor = System.Drawing.Color.Transparent;
            mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, actionsToolStripMenuItem, viewToolStripMenuItem, helpToolStripMenuItem });
            mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            mainMenuStrip.Size = new System.Drawing.Size(1652, 24);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // queueFolderContextMenuStrip
            // 
            queueFolderContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            queueFolderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { folderCreateQueueMenuItem, folderDeleteQueuesMenuItem, toolStripSeparator1, folderExportQueuesMenuItem, toolStripSeparator22, expandSubTreeMenuItem9, collapseSubTreeMenuItem9, toolStripSeparator74, toolStripMenuItem20 });
            queueFolderContextMenuStrip.Name = "createContextMenuStrip";
            queueFolderContextMenuStrip.Size = new System.Drawing.Size(163, 154);
            // 
            // folderCreateQueueMenuItem
            // 
            folderCreateQueueMenuItem.Name = "folderCreateQueueMenuItem";
            folderCreateQueueMenuItem.Size = new System.Drawing.Size(162, 22);
            folderCreateQueueMenuItem.Text = "Create Queue";
            folderCreateQueueMenuItem.ToolTipText = "Create a new queue in the current path.";
            // 
            // folderDeleteQueuesMenuItem
            // 
            folderDeleteQueuesMenuItem.Name = "folderDeleteQueuesMenuItem";
            folderDeleteQueuesMenuItem.Size = new System.Drawing.Size(162, 22);
            folderDeleteQueuesMenuItem.Text = "Delete Queues";
            folderDeleteQueuesMenuItem.ToolTipText = "Deletes all queues in the current path.";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
            // 
            // folderExportQueuesMenuItem
            // 
            folderExportQueuesMenuItem.Name = "folderExportQueuesMenuItem";
            folderExportQueuesMenuItem.Size = new System.Drawing.Size(162, 22);
            folderExportQueuesMenuItem.Text = "Export Queues";
            folderExportQueuesMenuItem.ToolTipText = "Export the definition of the queues in the current path to file.";
            // 
            // toolStripSeparator22
            // 
            toolStripSeparator22.Name = "toolStripSeparator22";
            toolStripSeparator22.Size = new System.Drawing.Size(159, 6);
            // 
            // expandSubTreeMenuItem9
            // 
            expandSubTreeMenuItem9.Name = "expandSubTreeMenuItem9";
            expandSubTreeMenuItem9.Size = new System.Drawing.Size(162, 22);
            expandSubTreeMenuItem9.Text = "Expand Subtree";
            expandSubTreeMenuItem9.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem9
            // 
            collapseSubTreeMenuItem9.Name = "collapseSubTreeMenuItem9";
            collapseSubTreeMenuItem9.Size = new System.Drawing.Size(162, 22);
            collapseSubTreeMenuItem9.Text = "Collapse Subtree";
            collapseSubTreeMenuItem9.ToolTipText = "Collapse the subtree.";
            // 
            // toolStripSeparator74
            // 
            toolStripSeparator74.Name = "toolStripSeparator74";
            toolStripSeparator74.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripMenuItem20
            // 
            toolStripMenuItem20.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { messagesToolStripMenuItem2, deadletterMessagesToolStripMenuItem, toolStripSeparator77, allToolStripMenuItem });
            toolStripMenuItem20.Name = "toolStripMenuItem20";
            toolStripMenuItem20.Size = new System.Drawing.Size(162, 22);
            toolStripMenuItem20.Text = "Purge";
            // 
            // messagesToolStripMenuItem2
            // 
            messagesToolStripMenuItem2.Name = "messagesToolStripMenuItem2";
            messagesToolStripMenuItem2.Size = new System.Drawing.Size(225, 22);
            messagesToolStripMenuItem2.Text = "Messages";
            // 
            // deadletterMessagesToolStripMenuItem
            // 
            deadletterMessagesToolStripMenuItem.Name = "deadletterMessagesToolStripMenuItem";
            deadletterMessagesToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            deadletterMessagesToolStripMenuItem.Text = "Dead-letter Queue Messages";
            // 
            // toolStripSeparator77
            // 
            toolStripSeparator77.Name = "toolStripSeparator77";
            toolStripSeparator77.Size = new System.Drawing.Size(222, 6);
            // 
            // allToolStripMenuItem
            // 
            allToolStripMenuItem.Name = "allToolStripMenuItem";
            allToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            allToolStripMenuItem.Text = "All Messages";
            // 
            // topicFolderContextMenuStrip
            // 
            topicFolderContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            topicFolderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { folderCreateTopicMenuItem, folderDeleteTopicsMenuItem, toolStripSeparator13, folderExportTopicsMenuItem, toolStripSeparator23, expandSubTreeMenuItem10, collapseSubTreeMenuItem10, toolStripSeparator78, purgeToolStripMenuItem3 });
            topicFolderContextMenuStrip.Name = "createContextMenuStrip";
            topicFolderContextMenuStrip.Size = new System.Drawing.Size(163, 154);
            // 
            // folderCreateTopicMenuItem
            // 
            folderCreateTopicMenuItem.Name = "folderCreateTopicMenuItem";
            folderCreateTopicMenuItem.Size = new System.Drawing.Size(162, 22);
            folderCreateTopicMenuItem.Text = "Create Topic";
            folderCreateTopicMenuItem.ToolTipText = "Create a new topic in the specified path.";
            // 
            // folderDeleteTopicsMenuItem
            // 
            folderDeleteTopicsMenuItem.Name = "folderDeleteTopicsMenuItem";
            folderDeleteTopicsMenuItem.Size = new System.Drawing.Size(162, 22);
            folderDeleteTopicsMenuItem.Text = "Delete Topics";
            folderDeleteTopicsMenuItem.ToolTipText = "Delete all topics in the current path.";
            // 
            // toolStripSeparator13
            // 
            toolStripSeparator13.Name = "toolStripSeparator13";
            toolStripSeparator13.Size = new System.Drawing.Size(159, 6);
            // 
            // folderExportTopicsMenuItem
            // 
            folderExportTopicsMenuItem.Name = "folderExportTopicsMenuItem";
            folderExportTopicsMenuItem.Size = new System.Drawing.Size(162, 22);
            folderExportTopicsMenuItem.Text = "Export Topics";
            folderExportTopicsMenuItem.ToolTipText = "Export the definition of the topics in the current path to file.";
            // 
            // toolStripSeparator23
            // 
            toolStripSeparator23.Name = "toolStripSeparator23";
            toolStripSeparator23.Size = new System.Drawing.Size(159, 6);
            // 
            // expandSubTreeMenuItem10
            // 
            expandSubTreeMenuItem10.Name = "expandSubTreeMenuItem10";
            expandSubTreeMenuItem10.Size = new System.Drawing.Size(162, 22);
            expandSubTreeMenuItem10.Text = "Expand Subtree";
            expandSubTreeMenuItem10.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem10
            // 
            collapseSubTreeMenuItem10.Name = "collapseSubTreeMenuItem10";
            collapseSubTreeMenuItem10.Size = new System.Drawing.Size(162, 22);
            collapseSubTreeMenuItem10.Text = "Collapse Subtree";
            collapseSubTreeMenuItem10.ToolTipText = "Collapse the subtree.";
            // 
            // toolStripSeparator78
            // 
            toolStripSeparator78.Name = "toolStripSeparator78";
            toolStripSeparator78.Size = new System.Drawing.Size(159, 6);
            // 
            // purgeToolStripMenuItem3
            // 
            purgeToolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { messagesToolStripMenuItem3, deadletterQueueMessagesToolStripMenuItem2, toolStripSeparator79, allMessagesToolStripMenuItem2 });
            purgeToolStripMenuItem3.Name = "purgeToolStripMenuItem3";
            purgeToolStripMenuItem3.Size = new System.Drawing.Size(162, 22);
            purgeToolStripMenuItem3.Text = "Purge";
            // 
            // messagesToolStripMenuItem3
            // 
            messagesToolStripMenuItem3.Name = "messagesToolStripMenuItem3";
            messagesToolStripMenuItem3.Size = new System.Drawing.Size(225, 22);
            messagesToolStripMenuItem3.Text = "Messages";
            // 
            // deadletterQueueMessagesToolStripMenuItem2
            // 
            deadletterQueueMessagesToolStripMenuItem2.Name = "deadletterQueueMessagesToolStripMenuItem2";
            deadletterQueueMessagesToolStripMenuItem2.Size = new System.Drawing.Size(225, 22);
            deadletterQueueMessagesToolStripMenuItem2.Text = "Dead-letter Queue Messages";
            // 
            // toolStripSeparator79
            // 
            toolStripSeparator79.Name = "toolStripSeparator79";
            toolStripSeparator79.Size = new System.Drawing.Size(222, 6);
            // 
            // allMessagesToolStripMenuItem2
            // 
            allMessagesToolStripMenuItem2.Name = "allMessagesToolStripMenuItem2";
            allMessagesToolStripMenuItem2.Size = new System.Drawing.Size(225, 22);
            allMessagesToolStripMenuItem2.Text = "All Messages";
            // 
            // relayFolderContextMenuStrip
            // 
            relayFolderContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            relayFolderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { expandSubTreeMenuItem12, collapseSubTreeMenuItem12 });
            relayFolderContextMenuStrip.Name = "createContextMenuStrip";
            relayFolderContextMenuStrip.Size = new System.Drawing.Size(163, 48);
            // 
            // expandSubTreeMenuItem12
            // 
            expandSubTreeMenuItem12.Name = "expandSubTreeMenuItem12";
            expandSubTreeMenuItem12.Size = new System.Drawing.Size(162, 22);
            expandSubTreeMenuItem12.Text = "Expand Subtree";
            expandSubTreeMenuItem12.ToolTipText = "Expand the subtree.";
            // 
            // collapseSubTreeMenuItem12
            // 
            collapseSubTreeMenuItem12.Name = "collapseSubTreeMenuItem12";
            collapseSubTreeMenuItem12.Size = new System.Drawing.Size(162, 22);
            collapseSubTreeMenuItem12.Text = "Collapse Subtree";
            collapseSubTreeMenuItem12.ToolTipText = "Collapse the subtree.";
            // 
            // notificationHubContextMenuStrip
            // 
            notificationHubContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            notificationHubContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { deleteNotificationHubMenuItem, refreshNotificationHubMenuItem, toolStripSeparator43, exportNotificationHubMenuItem, toolStripSeparator44, copyUrlNotificationHubMenuItem, toolStripSeparator26, getRegistrationsNotificationHubMenuItem });
            notificationHubContextMenuStrip.Name = "nodeContextMenuStrip";
            notificationHubContextMenuStrip.Size = new System.Drawing.Size(225, 132);
            // 
            // deleteNotificationHubMenuItem
            // 
            deleteNotificationHubMenuItem.Name = "deleteNotificationHubMenuItem";
            deleteNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            deleteNotificationHubMenuItem.Text = "Delete Notification Hub";
            deleteNotificationHubMenuItem.ToolTipText = "Delete the current notification hub.";
            // 
            // refreshNotificationHubMenuItem
            // 
            refreshNotificationHubMenuItem.Name = "refreshNotificationHubMenuItem";
            refreshNotificationHubMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            refreshNotificationHubMenuItem.Text = "Refresh Notification Hub";
            refreshNotificationHubMenuItem.ToolTipText = "Refresh the current notification hub.";
            // 
            // toolStripSeparator43
            // 
            toolStripSeparator43.Name = "toolStripSeparator43";
            toolStripSeparator43.Size = new System.Drawing.Size(221, 6);
            // 
            // exportNotificationHubMenuItem
            // 
            exportNotificationHubMenuItem.Name = "exportNotificationHubMenuItem";
            exportNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            exportNotificationHubMenuItem.Text = "Export Notification Hub";
            exportNotificationHubMenuItem.ToolTipText = "Export notification hub definition to file.";
            // 
            // toolStripSeparator44
            // 
            toolStripSeparator44.Name = "toolStripSeparator44";
            toolStripSeparator44.Size = new System.Drawing.Size(221, 6);
            // 
            // copyUrlNotificationHubMenuItem
            // 
            copyUrlNotificationHubMenuItem.Name = "copyUrlNotificationHubMenuItem";
            copyUrlNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            copyUrlNotificationHubMenuItem.Text = "Copy Notification Hub URL";
            copyUrlNotificationHubMenuItem.ToolTipText = "Copy the notification hub URL to the clipboard.";
            // 
            // toolStripSeparator26
            // 
            toolStripSeparator26.Name = "toolStripSeparator26";
            toolStripSeparator26.Size = new System.Drawing.Size(221, 6);
            // 
            // getRegistrationsNotificationHubMenuItem
            // 
            getRegistrationsNotificationHubMenuItem.Name = "getRegistrationsNotificationHubMenuItem";
            getRegistrationsNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            getRegistrationsNotificationHubMenuItem.Text = "Get Registrations";
            getRegistrationsNotificationHubMenuItem.ToolTipText = "Gets the registrations of the current notification hub.";
            // 
            // notificationHubsContextMenuStrip
            // 
            notificationHubsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            notificationHubsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createNotificationHubMenuItem, deleteNotificationHubsMenuItem, toolStripSeparator17, refreshNotificationHubsMenuItem, toolStripSeparator42, exportNotificationHubsMenuItem, toolStripSeparator45, expandNotificationHubMenuItem, collapseNotificationHubMenuItem });
            notificationHubsContextMenuStrip.Name = "relayServicesContextMenuStrip";
            notificationHubsContextMenuStrip.Size = new System.Drawing.Size(230, 154);
            // 
            // createNotificationHubMenuItem
            // 
            createNotificationHubMenuItem.Name = "createNotificationHubMenuItem";
            createNotificationHubMenuItem.Size = new System.Drawing.Size(229, 22);
            createNotificationHubMenuItem.Text = "Create Notification Hub";
            createNotificationHubMenuItem.ToolTipText = "Create a new notification hub.";
            // 
            // deleteNotificationHubsMenuItem
            // 
            deleteNotificationHubsMenuItem.Name = "deleteNotificationHubsMenuItem";
            deleteNotificationHubsMenuItem.Size = new System.Drawing.Size(229, 22);
            deleteNotificationHubsMenuItem.Text = "Delete Notification Hubs";
            deleteNotificationHubsMenuItem.ToolTipText = "Deletes all notification hubs in the current namespace.";
            // 
            // toolStripSeparator17
            // 
            toolStripSeparator17.Name = "toolStripSeparator17";
            toolStripSeparator17.Size = new System.Drawing.Size(226, 6);
            // 
            // refreshNotificationHubsMenuItem
            // 
            refreshNotificationHubsMenuItem.Name = "refreshNotificationHubsMenuItem";
            refreshNotificationHubsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshNotificationHubsMenuItem.Size = new System.Drawing.Size(229, 22);
            refreshNotificationHubsMenuItem.Text = "Refresh Notification Hubs";
            refreshNotificationHubsMenuItem.ToolTipText = "Refresh all relays in the current namespace.";
            // 
            // toolStripSeparator42
            // 
            toolStripSeparator42.Name = "toolStripSeparator42";
            toolStripSeparator42.Size = new System.Drawing.Size(226, 6);
            // 
            // exportNotificationHubsMenuItem
            // 
            exportNotificationHubsMenuItem.Name = "exportNotificationHubsMenuItem";
            exportNotificationHubsMenuItem.Size = new System.Drawing.Size(229, 22);
            exportNotificationHubsMenuItem.Text = "Export Notification Hubs";
            exportNotificationHubsMenuItem.ToolTipText = "Export notification hubs definition to file.";
            // 
            // toolStripSeparator45
            // 
            toolStripSeparator45.Name = "toolStripSeparator45";
            toolStripSeparator45.Size = new System.Drawing.Size(226, 6);
            // 
            // expandNotificationHubMenuItem
            // 
            expandNotificationHubMenuItem.Name = "expandNotificationHubMenuItem";
            expandNotificationHubMenuItem.Size = new System.Drawing.Size(229, 22);
            expandNotificationHubMenuItem.Text = "Expand Subtree";
            expandNotificationHubMenuItem.ToolTipText = "Expand the subtree.";
            // 
            // collapseNotificationHubMenuItem
            // 
            collapseNotificationHubMenuItem.Name = "collapseNotificationHubMenuItem";
            collapseNotificationHubMenuItem.Size = new System.Drawing.Size(229, 22);
            collapseNotificationHubMenuItem.Text = "Collapse Subtree";
            collapseNotificationHubMenuItem.ToolTipText = "Collapse the subtree.";
            // 
            // eventHubContextMenuStrip
            // 
            eventHubContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            eventHubContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { changeStatusEventHubMenuItem, deleteEventHubMenuItem, refreshEventHubMenuItem, toolStripSeparator49, exportEventHubMenuItem, toolStripSeparator50, createConsumerGroupMenuItem, deleteConsumerGroupsMenuItem, toolStripSeparator51, copyEventHubUrlMenuItem, toolStripSeparator52, toolStripMenuItem11, toolStripMenuItem12, toolStripSeparator53, sendMessagesEventHubMenuItem });
            eventHubContextMenuStrip.Name = "topicContextMenuStrip";
            eventHubContextMenuStrip.Size = new System.Drawing.Size(209, 254);
            // 
            // changeStatusEventHubMenuItem
            // 
            changeStatusEventHubMenuItem.Name = "changeStatusEventHubMenuItem";
            changeStatusEventHubMenuItem.Size = new System.Drawing.Size(208, 22);
            changeStatusEventHubMenuItem.Text = "Change Status Event Hub";
            changeStatusEventHubMenuItem.ToolTipText = "Change the status of the current event hub.";
            changeStatusEventHubMenuItem.Click += changeStatusEntity_Click;
            // 
            // deleteEventHubMenuItem
            // 
            deleteEventHubMenuItem.Name = "deleteEventHubMenuItem";
            deleteEventHubMenuItem.Size = new System.Drawing.Size(208, 22);
            deleteEventHubMenuItem.Text = "Delete Event Hub";
            deleteEventHubMenuItem.ToolTipText = "Delete the current event hub.";
            // 
            // refreshEventHubMenuItem
            // 
            refreshEventHubMenuItem.Name = "refreshEventHubMenuItem";
            refreshEventHubMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshEventHubMenuItem.Size = new System.Drawing.Size(208, 22);
            refreshEventHubMenuItem.Text = "Refresh Event Hub";
            refreshEventHubMenuItem.ToolTipText = "Refresh the current event hub.";
            // 
            // toolStripSeparator49
            // 
            toolStripSeparator49.Name = "toolStripSeparator49";
            toolStripSeparator49.Size = new System.Drawing.Size(205, 6);
            // 
            // exportEventHubMenuItem
            // 
            exportEventHubMenuItem.Name = "exportEventHubMenuItem";
            exportEventHubMenuItem.Size = new System.Drawing.Size(208, 22);
            exportEventHubMenuItem.Text = "Export Event Hub";
            exportEventHubMenuItem.ToolTipText = "Export event hub definition to file.";
            // 
            // toolStripSeparator50
            // 
            toolStripSeparator50.Name = "toolStripSeparator50";
            toolStripSeparator50.Size = new System.Drawing.Size(205, 6);
            // 
            // createConsumerGroupMenuItem
            // 
            createConsumerGroupMenuItem.Name = "createConsumerGroupMenuItem";
            createConsumerGroupMenuItem.Size = new System.Drawing.Size(208, 22);
            createConsumerGroupMenuItem.Text = "Create Consumer Group";
            createConsumerGroupMenuItem.ToolTipText = "Create a new  consumer group for the current event hub.";
            // 
            // deleteConsumerGroupsMenuItem
            // 
            deleteConsumerGroupsMenuItem.Name = "deleteConsumerGroupsMenuItem";
            deleteConsumerGroupsMenuItem.Size = new System.Drawing.Size(208, 22);
            deleteConsumerGroupsMenuItem.Text = "Delete Consumer Groups";
            deleteConsumerGroupsMenuItem.ToolTipText = "Delete all consumer groups for the current event hub.";
            // 
            // toolStripSeparator51
            // 
            toolStripSeparator51.Name = "toolStripSeparator51";
            toolStripSeparator51.Size = new System.Drawing.Size(205, 6);
            // 
            // copyEventHubUrlMenuItem
            // 
            copyEventHubUrlMenuItem.Name = "copyEventHubUrlMenuItem";
            copyEventHubUrlMenuItem.Size = new System.Drawing.Size(208, 22);
            copyEventHubUrlMenuItem.Text = "Copy Event Hub URL";
            copyEventHubUrlMenuItem.ToolTipText = "Copy the topic URL to the clipboard.";
            // 
            // toolStripSeparator52
            // 
            toolStripSeparator52.Name = "toolStripSeparator52";
            toolStripSeparator52.Size = new System.Drawing.Size(205, 6);
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new System.Drawing.Size(208, 22);
            toolStripMenuItem11.Text = "Expand Subtree";
            toolStripMenuItem11.ToolTipText = "Expand the subtree.";
            // 
            // toolStripMenuItem12
            // 
            toolStripMenuItem12.Name = "toolStripMenuItem12";
            toolStripMenuItem12.Size = new System.Drawing.Size(208, 22);
            toolStripMenuItem12.Text = "Collapse Subtree";
            toolStripMenuItem12.ToolTipText = "Collapse the subtree.";
            // 
            // toolStripSeparator53
            // 
            toolStripSeparator53.Name = "toolStripSeparator53";
            toolStripSeparator53.Size = new System.Drawing.Size(205, 6);
            // 
            // sendMessagesEventHubMenuItem
            // 
            sendMessagesEventHubMenuItem.Name = "sendMessagesEventHubMenuItem";
            sendMessagesEventHubMenuItem.Size = new System.Drawing.Size(208, 22);
            sendMessagesEventHubMenuItem.Text = "Send Events";
            sendMessagesEventHubMenuItem.ToolTipText = "Send test events to the current event hub.";
            // 
            // eventHubsContextMenuStrip
            // 
            eventHubsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            eventHubsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createEventHubMenuItem, deleteEventHubsMenuItem, toolStripSeparator30, refreshEventHubsMenuItem, toolStripSeparator47, toolStripMenuItem4, toolStripSeparator48, toolStripMenuItem5, toolStripMenuItem6 });
            eventHubsContextMenuStrip.Name = "relayServicesContextMenuStrip";
            eventHubsContextMenuStrip.Size = new System.Drawing.Size(196, 154);
            // 
            // createEventHubMenuItem
            // 
            createEventHubMenuItem.Name = "createEventHubMenuItem";
            createEventHubMenuItem.Size = new System.Drawing.Size(195, 22);
            createEventHubMenuItem.Text = "Create Event Hub";
            createEventHubMenuItem.ToolTipText = "Create a new event hub.";
            // 
            // deleteEventHubsMenuItem
            // 
            deleteEventHubsMenuItem.Name = "deleteEventHubsMenuItem";
            deleteEventHubsMenuItem.Size = new System.Drawing.Size(195, 22);
            deleteEventHubsMenuItem.Text = "Delete Event Hubs";
            deleteEventHubsMenuItem.ToolTipText = "Deletes all event hubs in the current namespace.";
            // 
            // toolStripSeparator30
            // 
            toolStripSeparator30.Name = "toolStripSeparator30";
            toolStripSeparator30.Size = new System.Drawing.Size(192, 6);
            // 
            // refreshEventHubsMenuItem
            // 
            refreshEventHubsMenuItem.Name = "refreshEventHubsMenuItem";
            refreshEventHubsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshEventHubsMenuItem.Size = new System.Drawing.Size(195, 22);
            refreshEventHubsMenuItem.Text = "Refresh Event Hubs";
            refreshEventHubsMenuItem.ToolTipText = "Refresh all event hubs in the current namespace.";
            // 
            // toolStripSeparator47
            // 
            toolStripSeparator47.Name = "toolStripSeparator47";
            toolStripSeparator47.Size = new System.Drawing.Size(192, 6);
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(195, 22);
            toolStripMenuItem4.Text = "Export Event Hubs";
            toolStripMenuItem4.ToolTipText = "Export event hubs definition to file.";
            // 
            // toolStripSeparator48
            // 
            toolStripSeparator48.Name = "toolStripSeparator48";
            toolStripSeparator48.Size = new System.Drawing.Size(192, 6);
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new System.Drawing.Size(195, 22);
            toolStripMenuItem5.Text = "Expand Subtree";
            toolStripMenuItem5.ToolTipText = "Expand the subtree.";
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new System.Drawing.Size(195, 22);
            toolStripMenuItem6.Text = "Collapse Subtree";
            toolStripMenuItem6.ToolTipText = "Collapse the subtree.";
            // 
            // consumerGroupsContextMenuStrip
            // 
            consumerGroupsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            consumerGroupsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { createConsumerGroupMenuItem1, deleteConsumerGroupsMenuItem1, toolStripSeparator55, refreshConsumerGroupsMenuItem, toolStripSeparator56, toolStripMenuItem10, toolStripMenuItem13 });
            consumerGroupsContextMenuStrip.Name = "subscriptionsContextMenuStrip";
            consumerGroupsContextMenuStrip.Size = new System.Drawing.Size(232, 126);
            // 
            // createConsumerGroupMenuItem1
            // 
            createConsumerGroupMenuItem1.Name = "createConsumerGroupMenuItem1";
            createConsumerGroupMenuItem1.Size = new System.Drawing.Size(231, 22);
            createConsumerGroupMenuItem1.Text = "Create Consumer Group";
            createConsumerGroupMenuItem1.ToolTipText = "Create a new  consumer group for the current event hub.";
            // 
            // deleteConsumerGroupsMenuItem1
            // 
            deleteConsumerGroupsMenuItem1.Name = "deleteConsumerGroupsMenuItem1";
            deleteConsumerGroupsMenuItem1.Size = new System.Drawing.Size(231, 22);
            deleteConsumerGroupsMenuItem1.Text = "Delete Consumer Groups";
            deleteConsumerGroupsMenuItem1.ToolTipText = "Delete all consumer groups for the current event hub.";
            // 
            // toolStripSeparator55
            // 
            toolStripSeparator55.Name = "toolStripSeparator55";
            toolStripSeparator55.Size = new System.Drawing.Size(228, 6);
            // 
            // refreshConsumerGroupsMenuItem
            // 
            refreshConsumerGroupsMenuItem.Name = "refreshConsumerGroupsMenuItem";
            refreshConsumerGroupsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshConsumerGroupsMenuItem.Size = new System.Drawing.Size(231, 22);
            refreshConsumerGroupsMenuItem.Text = "Refresh Consumer Groups";
            refreshConsumerGroupsMenuItem.ToolTipText = "Refresh consumer groups for the current event hub.";
            // 
            // toolStripSeparator56
            // 
            toolStripSeparator56.Name = "toolStripSeparator56";
            toolStripSeparator56.Size = new System.Drawing.Size(228, 6);
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new System.Drawing.Size(231, 22);
            toolStripMenuItem10.Text = "Expand Subtree";
            toolStripMenuItem10.ToolTipText = "Expand the subtree.";
            // 
            // toolStripMenuItem13
            // 
            toolStripMenuItem13.Name = "toolStripMenuItem13";
            toolStripMenuItem13.Size = new System.Drawing.Size(231, 22);
            toolStripMenuItem13.Text = "Collapse Subtree";
            toolStripMenuItem13.ToolTipText = "Collapse the subtree.";
            // 
            // partitionContextMenuStrip
            // 
            partitionContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            partitionContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { refreshPartitionMenuItem, toolStripSeparator58, copyPartitionUrlMenuItem, toolStripSeparator54, createPartitionListenerMenuItem, toolStripSeparator60, sendMessagesEventHubPartitionMenuItem });
            partitionContextMenuStrip.Name = "ruleContextMenuStrip";
            partitionContextMenuStrip.Size = new System.Drawing.Size(201, 110);
            // 
            // refreshPartitionMenuItem
            // 
            refreshPartitionMenuItem.Name = "refreshPartitionMenuItem";
            refreshPartitionMenuItem.Size = new System.Drawing.Size(200, 22);
            refreshPartitionMenuItem.Text = "Refresh Partition";
            refreshPartitionMenuItem.ToolTipText = "Refresh the current partition.";
            // 
            // toolStripSeparator58
            // 
            toolStripSeparator58.Name = "toolStripSeparator58";
            toolStripSeparator58.Size = new System.Drawing.Size(197, 6);
            // 
            // copyPartitionUrlMenuItem
            // 
            copyPartitionUrlMenuItem.Name = "copyPartitionUrlMenuItem";
            copyPartitionUrlMenuItem.Size = new System.Drawing.Size(200, 22);
            copyPartitionUrlMenuItem.Text = "Copy Partition URL";
            copyPartitionUrlMenuItem.ToolTipText = "Copy the partition URL to the clipboard.";
            // 
            // toolStripSeparator54
            // 
            toolStripSeparator54.Name = "toolStripSeparator54";
            toolStripSeparator54.Size = new System.Drawing.Size(197, 6);
            // 
            // createPartitionListenerMenuItem
            // 
            createPartitionListenerMenuItem.Name = "createPartitionListenerMenuItem";
            createPartitionListenerMenuItem.Size = new System.Drawing.Size(200, 22);
            createPartitionListenerMenuItem.Text = "Create Partition Listener";
            createPartitionListenerMenuItem.ToolTipText = "Create a partition listener.";
            // 
            // toolStripSeparator60
            // 
            toolStripSeparator60.Name = "toolStripSeparator60";
            toolStripSeparator60.Size = new System.Drawing.Size(197, 6);
            // 
            // sendMessagesEventHubPartitionMenuItem
            // 
            sendMessagesEventHubPartitionMenuItem.Name = "sendMessagesEventHubPartitionMenuItem";
            sendMessagesEventHubPartitionMenuItem.Size = new System.Drawing.Size(200, 22);
            sendMessagesEventHubPartitionMenuItem.Text = "Send Events";
            sendMessagesEventHubPartitionMenuItem.ToolTipText = "Send test events to the current event hub partition.";
            // 
            // partitionsContextMenuStrip
            // 
            partitionsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            partitionsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { refreshPartitionsMenuItem, toolStripSeparator57, toolStripMenuItem8, toolStripMenuItem9 });
            partitionsContextMenuStrip.Name = "subscriptionsContextMenuStrip";
            partitionsContextMenuStrip.Size = new System.Drawing.Size(186, 76);
            // 
            // refreshPartitionsMenuItem
            // 
            refreshPartitionsMenuItem.Name = "refreshPartitionsMenuItem";
            refreshPartitionsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshPartitionsMenuItem.Size = new System.Drawing.Size(185, 22);
            refreshPartitionsMenuItem.Text = "Refresh Partitions";
            refreshPartitionsMenuItem.ToolTipText = "Refresh partitions for the current event hub.";
            // 
            // toolStripSeparator57
            // 
            toolStripSeparator57.Name = "toolStripSeparator57";
            toolStripSeparator57.Size = new System.Drawing.Size(182, 6);
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new System.Drawing.Size(185, 22);
            toolStripMenuItem8.Text = "Expand Subtree";
            toolStripMenuItem8.ToolTipText = "Expand the subtree.";
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new System.Drawing.Size(185, 22);
            toolStripMenuItem9.Text = "Collapse Subtree";
            toolStripMenuItem9.ToolTipText = "Collapse the subtree.";
            // 
            // consumerGroupContextMenuStrip
            // 
            consumerGroupContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            consumerGroupContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { deleteConsumerGroupMenuItem, refreshConsumerGroupMenuItem, toolStripSeparator59, getPartitionDataMenuItem, toolStripSeparator66, copyConsumerGroupUrlMenuItem, toolStripSeparator61, toolStripMenuItem17, toolStripMenuItem18, toolStripSeparator62, createConsumerGroupListenerMenuItem });
            consumerGroupContextMenuStrip.Name = "topicContextMenuStrip";
            consumerGroupContextMenuStrip.Size = new System.Drawing.Size(247, 182);
            // 
            // deleteConsumerGroupMenuItem
            // 
            deleteConsumerGroupMenuItem.Name = "deleteConsumerGroupMenuItem";
            deleteConsumerGroupMenuItem.Size = new System.Drawing.Size(246, 22);
            deleteConsumerGroupMenuItem.Text = "Delete Consumer Group";
            deleteConsumerGroupMenuItem.ToolTipText = "Delete the current consumer group.";
            // 
            // refreshConsumerGroupMenuItem
            // 
            refreshConsumerGroupMenuItem.Name = "refreshConsumerGroupMenuItem";
            refreshConsumerGroupMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshConsumerGroupMenuItem.Size = new System.Drawing.Size(246, 22);
            refreshConsumerGroupMenuItem.Text = "Refresh Consumer Group";
            refreshConsumerGroupMenuItem.ToolTipText = "Refresh the current consumer group.";
            // 
            // toolStripSeparator59
            // 
            toolStripSeparator59.Name = "toolStripSeparator59";
            toolStripSeparator59.Size = new System.Drawing.Size(243, 6);
            // 
            // getPartitionDataMenuItem
            // 
            getPartitionDataMenuItem.Name = "getPartitionDataMenuItem";
            getPartitionDataMenuItem.Size = new System.Drawing.Size(246, 22);
            getPartitionDataMenuItem.Text = "Get Partition Data";
            getPartitionDataMenuItem.ToolTipText = "Get partition data for the current event hub .";
            // 
            // toolStripSeparator66
            // 
            toolStripSeparator66.Name = "toolStripSeparator66";
            toolStripSeparator66.Size = new System.Drawing.Size(243, 6);
            // 
            // copyConsumerGroupUrlMenuItem
            // 
            copyConsumerGroupUrlMenuItem.Name = "copyConsumerGroupUrlMenuItem";
            copyConsumerGroupUrlMenuItem.Size = new System.Drawing.Size(246, 22);
            copyConsumerGroupUrlMenuItem.Text = "Copy Consumer Group URL";
            copyConsumerGroupUrlMenuItem.ToolTipText = "Copy the consumer group URL to the clipboard.";
            // 
            // toolStripSeparator61
            // 
            toolStripSeparator61.Name = "toolStripSeparator61";
            toolStripSeparator61.Size = new System.Drawing.Size(243, 6);
            // 
            // toolStripMenuItem17
            // 
            toolStripMenuItem17.Name = "toolStripMenuItem17";
            toolStripMenuItem17.Size = new System.Drawing.Size(246, 22);
            toolStripMenuItem17.Text = "Expand Subtree";
            toolStripMenuItem17.ToolTipText = "Expand the subtree.";
            // 
            // toolStripMenuItem18
            // 
            toolStripMenuItem18.Name = "toolStripMenuItem18";
            toolStripMenuItem18.Size = new System.Drawing.Size(246, 22);
            toolStripMenuItem18.Text = "Collapse Subtree";
            toolStripMenuItem18.ToolTipText = "Collapse the subtree.";
            // 
            // toolStripSeparator62
            // 
            toolStripSeparator62.Name = "toolStripSeparator62";
            toolStripSeparator62.Size = new System.Drawing.Size(243, 6);
            // 
            // createConsumerGroupListenerMenuItem
            // 
            createConsumerGroupListenerMenuItem.Name = "createConsumerGroupListenerMenuItem";
            createConsumerGroupListenerMenuItem.Size = new System.Drawing.Size(246, 22);
            createConsumerGroupListenerMenuItem.Text = "Create Consumer Group Listener";
            createConsumerGroupListenerMenuItem.ToolTipText = "Create a consumer group listener. This operation creates a listener on each partition.";
            // 
            // logoPictureBox
            // 
            logoPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            logoPictureBox.BackgroundImage = Properties.Resources.MicrosoftAzureWhiteLogo;
            logoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            logoPictureBox.Location = new System.Drawing.Point(1521, 18);
            logoPictureBox.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new System.Drawing.Size(112, 14);
            logoPictureBox.TabIndex = 23;
            logoPictureBox.TabStop = false;
            // 
            // relayServiceFolderContextMenuStrip
            // 
            relayServiceFolderContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            relayServiceFolderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripSeparator63, toolStripMenuItem3, toolStripSeparator64, toolStripMenuItem7, toolStripMenuItem14 });
            relayServiceFolderContextMenuStrip.Name = "createContextMenuStrip";
            relayServiceFolderContextMenuStrip.Size = new System.Drawing.Size(163, 126);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            toolStripMenuItem1.Text = "Create Relay";
            toolStripMenuItem1.ToolTipText = "Create a new relay in the current path.";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(162, 22);
            toolStripMenuItem2.Text = "Delete Relays";
            toolStripMenuItem2.ToolTipText = "Deletes all relays in the current path.";
            // 
            // toolStripSeparator63
            // 
            toolStripSeparator63.Name = "toolStripSeparator63";
            toolStripSeparator63.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(162, 22);
            toolStripMenuItem3.Text = "Export Relays";
            toolStripMenuItem3.ToolTipText = "Export the definition of the relays in the current path to file.";
            // 
            // toolStripSeparator64
            // 
            toolStripSeparator64.Name = "toolStripSeparator64";
            toolStripSeparator64.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new System.Drawing.Size(162, 22);
            toolStripMenuItem7.Text = "Expand Subtree";
            toolStripMenuItem7.ToolTipText = "Expand the subtree.";
            // 
            // toolStripMenuItem14
            // 
            toolStripMenuItem14.Name = "toolStripMenuItem14";
            toolStripMenuItem14.Size = new System.Drawing.Size(162, 22);
            toolStripMenuItem14.Text = "Collapse Subtree";
            toolStripMenuItem14.ToolTipText = "Collapse the subtree.";
            // 
            // relayContextMenuStrip
            // 
            relayContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            relayContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { deleteRelayMenuItem, refreshRelayMenuItem, relayToolStripSeparator1, exportRelayMenuItem, relayToolStripSeparator2, copyRelayUrlMenuItem, toolStripSeparator69, toolStripMenuItem27, toolStripMenuItem28 });
            relayContextMenuStrip.Name = "nodeContextMenuStrip";
            relayContextMenuStrip.Size = new System.Drawing.Size(199, 154);
            // 
            // deleteRelayMenuItem
            // 
            deleteRelayMenuItem.Name = "deleteRelayMenuItem";
            deleteRelayMenuItem.Size = new System.Drawing.Size(198, 22);
            deleteRelayMenuItem.Text = "Delete Relay";
            deleteRelayMenuItem.ToolTipText = "Delete the current relay.";
            // 
            // refreshRelayMenuItem
            // 
            refreshRelayMenuItem.Name = "refreshRelayMenuItem";
            refreshRelayMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            refreshRelayMenuItem.Size = new System.Drawing.Size(198, 22);
            refreshRelayMenuItem.Text = "Refresh Relay";
            refreshRelayMenuItem.ToolTipText = "Refresh the current relay.";
            // 
            // relayToolStripSeparator1
            // 
            relayToolStripSeparator1.Name = "relayToolStripSeparator1";
            relayToolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // exportRelayMenuItem
            // 
            exportRelayMenuItem.Name = "exportRelayMenuItem";
            exportRelayMenuItem.Size = new System.Drawing.Size(198, 22);
            exportRelayMenuItem.Text = "Export Relay";
            exportRelayMenuItem.ToolTipText = "Export relay definition to file.";
            // 
            // relayToolStripSeparator2
            // 
            relayToolStripSeparator2.Name = "relayToolStripSeparator2";
            relayToolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // copyRelayUrlMenuItem
            // 
            copyRelayUrlMenuItem.Name = "copyRelayUrlMenuItem";
            copyRelayUrlMenuItem.Size = new System.Drawing.Size(198, 22);
            copyRelayUrlMenuItem.Text = "Copy Relay URL";
            copyRelayUrlMenuItem.ToolTipText = "Copy the relay URL to the clipboard.";
            // 
            // toolStripSeparator69
            // 
            toolStripSeparator69.Name = "toolStripSeparator69";
            toolStripSeparator69.Size = new System.Drawing.Size(195, 6);
            // 
            // toolStripMenuItem27
            // 
            toolStripMenuItem27.Name = "toolStripMenuItem27";
            toolStripMenuItem27.Size = new System.Drawing.Size(198, 22);
            toolStripMenuItem27.Text = "Test Relay In SDI Mode";
            toolStripMenuItem27.ToolTipText = "Test the current relay in SDI mode.";
            // 
            // toolStripMenuItem28
            // 
            toolStripMenuItem28.Name = "toolStripMenuItem28";
            toolStripMenuItem28.Size = new System.Drawing.Size(198, 22);
            toolStripMenuItem28.Text = "Test Relay In MDI Mode";
            toolStripMenuItem28.ToolTipText = "Test the current relay in MDI mode.";
            // 
            // linkLabelNewVersionAvailable
            // 
            linkLabelNewVersionAvailable.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            linkLabelNewVersionAvailable.AutoSize = true;
            linkLabelNewVersionAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            linkLabelNewVersionAvailable.ForeColor = System.Drawing.Color.Navy;
            linkLabelNewVersionAvailable.LinkColor = System.Drawing.Color.Navy;
            linkLabelNewVersionAvailable.Location = new System.Drawing.Point(1225, 17);
            linkLabelNewVersionAvailable.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            linkLabelNewVersionAvailable.Name = "linkLabelNewVersionAvailable";
            linkLabelNewVersionAvailable.Size = new System.Drawing.Size(149, 15);
            linkLabelNewVersionAvailable.TabIndex = 1;
            linkLabelNewVersionAvailable.TabStop = true;
            linkLabelNewVersionAvailable.Text = "New Version Available";
            linkLabelNewVersionAvailable.LinkClicked += linkLabelNewVersionAvailable_LinkClicked;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(215, 228, 242);
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1652, 969);
            Controls.Add(linkLabelNewVersionAvailable);
            Controls.Add(logoPictureBox);
            Controls.Add(mainSplitContainer);
            Controls.Add(statusStrip);
            Controls.Add(mainMenuStrip);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Service Bus Explorer";
            FormClosing += MainForm_FormClosing;
            Shown += MainForm_Shown;
            ResizeBegin += MainForm_ResizeBegin;
            ResizeEnd += MainForm_ResizeEnd;
            SizeChanged += MainForm_SizeChanged;
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            panelTreeView.ResumeLayout(false);
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            panelLog.ResumeLayout(false);
            logContextMenuStrip.ResumeLayout(false);
            rootContextMenuStrip.ResumeLayout(false);
            queuesContextMenuStrip.ResumeLayout(false);
            ruleContextMenuStrip.ResumeLayout(false);
            rulesContextMenuStrip.ResumeLayout(false);
            subscriptionsContextMenuStrip.ResumeLayout(false);
            eventGridSubscriptionsContextMenuStrip.ResumeLayout(false);
            subscriptionContextMenuStrip.ResumeLayout(false);
            topicContextMenuStrip.ResumeLayout(false);
            eventGridTopicContextMenuStrip.ResumeLayout(false);
            eventGridSubscriptionContextMenuStrip.ResumeLayout(false);
            queueContextMenuStrip.ResumeLayout(false);
            topicsContextMenuStrip.ResumeLayout(false);
            eventGridTopicsContextMenuStrip.ResumeLayout(false);
            relayServicesContextMenuStrip.ResumeLayout(false);
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            queueFolderContextMenuStrip.ResumeLayout(false);
            topicFolderContextMenuStrip.ResumeLayout(false);
            relayFolderContextMenuStrip.ResumeLayout(false);
            notificationHubContextMenuStrip.ResumeLayout(false);
            notificationHubsContextMenuStrip.ResumeLayout(false);
            eventHubContextMenuStrip.ResumeLayout(false);
            eventHubsContextMenuStrip.ResumeLayout(false);
            consumerGroupsContextMenuStrip.ResumeLayout(false);
            partitionContextMenuStrip.ResumeLayout(false);
            partitionsContextMenuStrip.ResumeLayout(false);
            consumerGroupContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            relayServiceFolderContextMenuStrip.ResumeLayout(false);
            relayContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logWindowToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorMain;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private HeaderPanel panelTreeView;
        private HeaderPanel panelMain;
        private HeaderPanel panelLog;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.ToolStripMenuItem saveLogToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip rootContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshRootMenuItem;
        private System.Windows.Forms.ContextMenuStrip queuesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createQueueMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteQueuesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshQueuesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exportQueuesMenuItem;
        private System.Windows.Forms.ContextMenuStrip ruleContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeRuleMenuItem;
        private System.Windows.Forms.ContextMenuStrip rulesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addRuleMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteRulesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem refreshRulesMenuItem;
        private System.Windows.Forms.ContextMenuStrip subscriptionsContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip eventGridSubscriptionsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addSubscriptionMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteSubscriptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshSubscriptionsMenuItem;
        private System.Windows.Forms.ContextMenuStrip subscriptionContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeSubscriptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testSubscriptionSDIMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem addRuleMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem refreshSubscriptionMenuItem;
        private System.Windows.Forms.ContextMenuStrip topicContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip eventGridTopicContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip eventGridSubscriptionContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testTopicSDIMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem exportTopicMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addSubscriptionMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteTopicSubscriptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshTopicMenuItem;
        private System.Windows.Forms.ContextMenuStrip queueContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteQueueMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testQueueSDIMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshQueueMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exportQueueMenuItem;
        private System.Windows.Forms.ContextMenuStrip topicsContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip eventGridTopicsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTopicsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createEventGridTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createEventGridSubscriptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshTopicsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exportTopicsMenuItem;
        private System.Windows.Forms.ContextMenuStrip relayServicesContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem copyQueueUrlMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyQueueDeadletterQueueUrlMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem copyTopicUrlMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySubscriptionUrlMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySubscriptionDeadletterSubscriptionUrlMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem importEntityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportEntityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEntityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.ContextMenuStrip queueFolderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem folderCreateQueueMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderDeleteQueuesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem folderExportQueuesMenuItem;
        private System.Windows.Forms.ContextMenuStrip topicFolderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem folderCreateTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderDeleteTopicsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem folderExportTopicsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem11;
        private System.Windows.Forms.ContextMenuStrip relayFolderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem expandSubTreeMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem collapseSubTreeMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem setDefaultLayouToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripMenuItem queueReceiveMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queueReceiveDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripMenuItem subReceiveMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator subReceiveToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem subscriptionReceiveDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
        private System.Windows.Forms.ToolStripMenuItem queueSendMessageMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
        private System.Windows.Forms.ToolStripMenuItem sendMessagesTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem publishEventsTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiveEventsSubscriptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEventGridTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEventGridSubscriptionMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator33;
        private System.Windows.Forms.ToolStripMenuItem testQueueMDIMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testTopicMDIMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testSubscriptionMDIMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator35;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator34;
        private System.Windows.Forms.ToolStripMenuItem changeStatusQueueMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeStatusTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeStatusSubscriptionMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator37;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator36;
        private System.Windows.Forms.ToolStripMenuItem filterQueueMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator38;
        private System.Windows.Forms.ToolStripMenuItem filterTopicsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator39;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator40;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator41;
        private System.Windows.Forms.ToolStripMenuItem filterSubscriptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getQueueMessageSessionsMenuItem;
        private System.Windows.Forms.ToolStripSeparator getQueueMessageSessionsSeparator;
        private System.Windows.Forms.ToolStripMenuItem getSubscriptionMessageSessionsMenuItem;
        private System.Windows.Forms.ToolStripSeparator getSubscriptionMessageSessionsSeparator;
        private System.Windows.Forms.ContextMenuStrip notificationHubContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteNotificationHubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshNotificationHubMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator43;
        private System.Windows.Forms.ToolStripMenuItem exportNotificationHubMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator44;
        private System.Windows.Forms.ToolStripMenuItem copyUrlNotificationHubMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
        private System.Windows.Forms.ToolStripMenuItem getRegistrationsNotificationHubMenuItem;
        private System.Windows.Forms.ContextMenuStrip notificationHubsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createNotificationHubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteNotificationHubsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem refreshNotificationHubsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator42;
        private System.Windows.Forms.ToolStripMenuItem exportNotificationHubsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator45;
        private System.Windows.Forms.ToolStripMenuItem expandNotificationHubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseNotificationHubMenuItem;
        private System.Windows.Forms.ContextMenuStrip logContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSubscriptionListenerMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator46;
        private System.Windows.Forms.ToolStripMenuItem createQueueListenerMenuItem;
        private System.Windows.Forms.ContextMenuStrip eventHubContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem changeStatusEventHubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEventHubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshEventHubMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator49;
        private System.Windows.Forms.ToolStripMenuItem exportEventHubMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator50;
        private System.Windows.Forms.ToolStripMenuItem createConsumerGroupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteConsumerGroupsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator51;
        private System.Windows.Forms.ToolStripMenuItem copyEventHubUrlMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator52;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator53;
        private System.Windows.Forms.ToolStripMenuItem sendMessagesEventHubMenuItem;
        private System.Windows.Forms.ContextMenuStrip eventHubsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createEventHubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEventHubsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
        private System.Windows.Forms.ToolStripMenuItem refreshEventHubsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator47;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator48;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ContextMenuStrip consumerGroupsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem createConsumerGroupMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteConsumerGroupsMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator55;
        private System.Windows.Forms.ToolStripMenuItem refreshConsumerGroupsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator56;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ContextMenuStrip partitionContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshPartitionMenuItem;
        private System.Windows.Forms.ContextMenuStrip partitionsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshPartitionsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator57;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ContextMenuStrip consumerGroupContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteConsumerGroupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshConsumerGroupMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator59;
        private System.Windows.Forms.ToolStripMenuItem copyConsumerGroupUrlMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator61;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator62;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator58;
        private System.Windows.Forms.ToolStripMenuItem copyPartitionUrlMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator54;
        private System.Windows.Forms.ToolStripMenuItem createPartitionListenerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createConsumerGroupListenerMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator60;
        private System.Windows.Forms.ToolStripMenuItem sendMessagesEventHubPartitionMenuItem;
        private System.Windows.Forms.ContextMenuStrip relayServiceFolderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator63;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator64;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator65;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator67;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem21;
        private System.Windows.Forms.ContextMenuStrip relayContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteRelayMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshRelayMenuItem;
        private System.Windows.Forms.ToolStripSeparator relayToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportRelayMenuItem;
        private System.Windows.Forms.ToolStripSeparator relayToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyRelayUrlMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator69;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem27;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem28;
        private System.Windows.Forms.ToolStripMenuItem getPartitionDataMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator66;
        private System.Windows.Forms.ToolStripMenuItem createIoTHubListenerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createEventHubListenerMenuItem;
        private System.Windows.Forms.TreeView serviceBusTreeView;
        private System.Windows.Forms.ToolStripMenuItem subscriptionPurgeMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subscriptionPurgeDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator queueReceiveToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem queuePurgeMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queuePurgeDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queueReceiveTransferDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subscriptionReceiveTransferDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameQueueMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savedConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyNamespaceUrlMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyConnectionStringMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator68;
        private System.Windows.Forms.LinkLabel linkLabelNewVersionAvailable;
        private System.Windows.Forms.ToolStripMenuItem showCommandLineOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator70;
        private System.Windows.Forms.ToolStripMenuItem purgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topicPurgeAllMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topicPurgeMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topicPurgeDeadletterQueueMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator71;
        private System.Windows.Forms.ToolStripMenuItem purgeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem allMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem messagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deadletterQueueMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator72;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator73;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator74;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator75;
        private System.Windows.Forms.ToolStripMenuItem purgeToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem messagesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deadletterQueueMessagesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator76;
        private System.Windows.Forms.ToolStripMenuItem allMessagesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem queuePurgeAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subscriptionPurgeAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem20;
        private System.Windows.Forms.ToolStripMenuItem messagesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deadletterMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator77;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator78;
        private System.Windows.Forms.ToolStripMenuItem purgeToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem messagesToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem deadletterQueueMessagesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator79;
        private System.Windows.Forms.ToolStripMenuItem allMessagesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem duplicateSubscriptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateQueueMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectUsingSASToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectUsingEntraToolStripMenuItem;
    }
}

