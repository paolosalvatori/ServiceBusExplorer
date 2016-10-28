namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
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
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorMain = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDefaultLayouToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelTreeView = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.HeaderPanel();
            this.serviceBusTreeView = new System.Windows.Forms.TreeView();
            this.panelMain = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.HeaderPanel();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.panelLog = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.HeaderPanel();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.logContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.rootContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEntityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshRootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.exportEntityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importEntityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.metricsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.metricsSDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metricsMDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queuesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator37 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator36 = new System.Windows.Forms.ToolStripSeparator();
            this.filterQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ruleContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeRuleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rulesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRuleMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRulesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshRulesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subscriptionsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSubscriptionMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSubscriptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator40 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshSubscriptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator41 = new System.Windows.Forms.ToolStripSeparator();
            this.filterSubscriptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.subscriptionContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeStatusSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshSubscriptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.addRuleMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.copySubscriptionUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySubscriptionDeadletterSubscriptionUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.testSubscriptionSDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSubscriptionMDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.createSubscriptionListenerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.subReceiveAllMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subReceiveTopMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subPeekTopMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subReceiveToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.subReceiveAllDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subReceiveTopDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subPeekTopDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getSubscriptionMessageSessionsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.getSubscriptionMessageSessionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topicContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeStatusTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addSubscriptionMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTopicSubscriptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.copyTopicUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.testTopicSDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testTopicMDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
            this.sendMessagesTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeStatusQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exportQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.copyQueueUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyQueueDeadletterQueueUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.testQueueSDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testQueueMDIMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.queueSendMessageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createQueueListenerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.queueReceiveAllMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueReceiveTopMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queuePeekTopMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueReceiveToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.queueReceiveAllDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueReceiveTopDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queuePeekTopDeadletterQueueMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getQueueMessageSessionsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.getQueueMessageSessionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topicsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.filterTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator39 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.relayServicesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshRelayServicesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.relayServiceContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyRelayServiceUrlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.queueFolderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.folderCreateQueueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderDeleteQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.folderExportQueuesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.topicFolderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.folderCreateTopicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderDeleteTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.folderExportTopicsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.expandSubTreeMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.relayFolderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expandSubTreeMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseSubTreeMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationHubContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator43 = new System.Windows.Forms.ToolStripSeparator();
            this.exportNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator44 = new System.Windows.Forms.ToolStripSeparator();
            this.copyUrlNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.getRegistrationsNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationHubsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteNotificationHubsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshNotificationHubsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator42 = new System.Windows.Forms.ToolStripSeparator();
            this.exportNotificationHubsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator45 = new System.Windows.Forms.ToolStripSeparator();
            this.expandNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseNotificationHubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelTreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.panelLog.SuspendLayout();
            this.logContextMenuStrip.SuspendLayout();
            this.rootContextMenuStrip.SuspendLayout();
            this.queuesContextMenuStrip.SuspendLayout();
            this.ruleContextMenuStrip.SuspendLayout();
            this.rulesContextMenuStrip.SuspendLayout();
            this.subscriptionsContextMenuStrip.SuspendLayout();
            this.subscriptionContextMenuStrip.SuspendLayout();
            this.topicContextMenuStrip.SuspendLayout();
            this.queueContextMenuStrip.SuspendLayout();
            this.topicsContextMenuStrip.SuspendLayout();
            this.relayServicesContextMenuStrip.SuspendLayout();
            this.relayServiceContextMenuStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.queueFolderContextMenuStrip.SuspendLayout();
            this.topicFolderContextMenuStrip.SuspendLayout();
            this.relayFolderContextMenuStrip.SuspendLayout();
            this.notificationHubContextMenuStrip.SuspendLayout();
            this.notificationHubsContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Queue.ico");
            this.imageList.Images.SetKeyName(1, "Topic.ico");
            this.imageList.Images.SetKeyName(2, "Chart.ico");
            this.imageList.Images.SetKeyName(3, "Class.ico");
            this.imageList.Images.SetKeyName(4, "Add.ico");
            this.imageList.Images.SetKeyName(5, "UserInfo.ico");
            this.imageList.Images.SetKeyName(6, "exec.ico");
            this.imageList.Images.SetKeyName(7, "AzureLogo.ico");
            this.imageList.Images.SetKeyName(8, "World.png");
            this.imageList.Images.SetKeyName(9, "RelayService.png");
            this.imageList.Images.SetKeyName(10, "folder_web.ico");
            this.imageList.Images.SetKeyName(11, "Web.ico");
            this.imageList.Images.SetKeyName(12, "GreyChart.ico");
            this.imageList.Images.SetKeyName(13, "GreyClass.ico");
            this.imageList.Images.SetKeyName(14, "GreyUserInfo.ico");
            this.imageList.Images.SetKeyName(15, "hub.png");
            this.imageList.Images.SetKeyName(16, "PieDiagram.ico");
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.toolStripSeparatorMain,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.connectToolStripMenuItem.Text = "&Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // toolStripSeparatorMain
            // 
            this.toolStripSeparatorMain.Name = "toolStripSeparatorMain";
            this.toolStripSeparatorMain.Size = new System.Drawing.Size(159, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.close_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogToolStripMenuItem,
            this.saveLogToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.clearLogToolStripMenuItem.Text = "Clear Log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLog_Click);
            // 
            // saveLogToolStripMenuItem
            // 
            this.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
            this.saveLogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveLogToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveLogToolStripMenuItem.Text = "Save Log As...";
            this.saveLogToolStripMenuItem.Click += new System.EventHandler(this.saveLogToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "&Actions";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDefaultLayouToolStripMenuItem,
            this.logWindowToolStripMenuItem,
            this.toolStripSeparator21,
            this.optionsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // setDefaultLayouToolStripMenuItem
            // 
            this.setDefaultLayouToolStripMenuItem.Name = "setDefaultLayouToolStripMenuItem";
            this.setDefaultLayouToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.setDefaultLayouToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.setDefaultLayouToolStripMenuItem.Text = "Set Default Layout";
            this.setDefaultLayouToolStripMenuItem.Click += new System.EventHandler(this.setDefaultLayouToolStripMenuItem_Click);
            // 
            // logWindowToolStripMenuItem
            // 
            this.logWindowToolStripMenuItem.Checked = true;
            this.logWindowToolStripMenuItem.CheckOnClick = true;
            this.logWindowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logWindowToolStripMenuItem.Name = "logWindowToolStripMenuItem";
            this.logWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.logWindowToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.logWindowToolStripMenuItem.Text = "&Log Window";
            this.logWindowToolStripMenuItem.Click += new System.EventHandler(this.logWindowToolStripMenuItem_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(209, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.aboutToolStripMenuItem.Text = "&About Service Bus Explorer";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.statusStrip.Location = new System.Drawing.Point(0, 819);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1416, 22);
            this.statusStrip.TabIndex = 19;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelTreeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelMain);
            this.splitContainer.Size = new System.Drawing.Size(1384, 570);
            this.splitContainer.SplitterDistance = 372;
            this.splitContainer.TabIndex = 1;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.mainSplitContainer_SplitterMoved);
            // 
            // panelTreeView
            // 
            this.panelTreeView.AutoScroll = true;
            this.panelTreeView.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelTreeView.Controls.Add(this.serviceBusTreeView);
            this.panelTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTreeView.ForeColor = System.Drawing.SystemColors.Window;
            this.panelTreeView.HeaderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(205)))), ((int)(((byte)(219)))));
            this.panelTreeView.HeaderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.panelTreeView.HeaderFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.panelTreeView.HeaderHeight = 24;
            this.panelTreeView.HeaderText = "Service Bus Namespace";
            this.panelTreeView.Icon = ((System.Drawing.Image)(resources.GetObject("panelTreeView.Icon")));
            this.panelTreeView.IconTransparentColor = System.Drawing.Color.White;
            this.panelTreeView.Location = new System.Drawing.Point(0, 0);
            this.panelTreeView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelTreeView.Name = "panelTreeView";
            this.panelTreeView.Padding = new System.Windows.Forms.Padding(5, 29, 5, 4);
            this.panelTreeView.Size = new System.Drawing.Size(372, 570);
            this.panelTreeView.TabIndex = 0;
            // 
            // serviceBusTreeView
            // 
            this.serviceBusTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serviceBusTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceBusTreeView.ImageIndex = 0;
            this.serviceBusTreeView.ImageList = this.imageList;
            this.serviceBusTreeView.Indent = 20;
            this.serviceBusTreeView.ItemHeight = 20;
            this.serviceBusTreeView.Location = new System.Drawing.Point(5, 29);
            this.serviceBusTreeView.Name = "serviceBusTreeView";
            this.serviceBusTreeView.SelectedImageIndex = 0;
            this.serviceBusTreeView.Size = new System.Drawing.Size(362, 537);
            this.serviceBusTreeView.TabIndex = 13;
            this.serviceBusTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.serviceBusTreeView_NodeMouseClick);
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.ForeColor = System.Drawing.SystemColors.Window;
            this.panelMain.HeaderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(205)))), ((int)(((byte)(219)))));
            this.panelMain.HeaderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.panelMain.HeaderFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.panelMain.HeaderHeight = 24;
            this.panelMain.HeaderText = "";
            this.panelMain.Icon = ((System.Drawing.Image)(resources.GetObject("panelMain.Icon")));
            this.panelMain.IconTransparentColor = System.Drawing.Color.White;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(5, 29, 5, 4);
            this.panelMain.Size = new System.Drawing.Size(1008, 570);
            this.panelMain.TabIndex = 0;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSplitContainer.Location = new System.Drawing.Point(16, 40);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.splitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.panelLog);
            this.mainSplitContainer.Size = new System.Drawing.Size(1384, 784);
            this.mainSplitContainer.SplitterDistance = 570;
            this.mainSplitContainer.TabIndex = 21;
            this.mainSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.mainSplitContainer_SplitterMoved);
            // 
            // panelLog
            // 
            this.panelLog.AutoScroll = true;
            this.panelLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelLog.Controls.Add(this.lstLog);
            this.panelLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLog.ForeColor = System.Drawing.SystemColors.Window;
            this.panelLog.HeaderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(205)))), ((int)(((byte)(219)))));
            this.panelLog.HeaderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.panelLog.HeaderFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.panelLog.HeaderHeight = 24;
            this.panelLog.HeaderText = "Log";
            this.panelLog.Icon = ((System.Drawing.Image)(resources.GetObject("panelLog.Icon")));
            this.panelLog.IconTransparentColor = System.Drawing.Color.White;
            this.panelLog.Location = new System.Drawing.Point(0, 0);
            this.panelLog.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelLog.Name = "panelLog";
            this.panelLog.Padding = new System.Windows.Forms.Padding(5, 29, 5, 4);
            this.panelLog.Size = new System.Drawing.Size(1384, 210);
            this.panelLog.TabIndex = 0;
            // 
            // lstLog
            // 
            this.lstLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstLog.ContextMenuStrip = this.logContextMenuStrip;
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.ItemHeight = 14;
            this.lstLog.Location = new System.Drawing.Point(5, 29);
            this.lstLog.Name = "lstLog";
            this.lstLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstLog.Size = new System.Drawing.Size(1374, 177);
            this.lstLog.TabIndex = 4;
            this.lstLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLog_KeyDown);
            this.lstLog.Leave += new System.EventHandler(this.lstLog_Leave);
            // 
            // logContextMenuStrip
            // 
            this.logContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAllToolStripMenuItem,
            this.copySelectedToolStripMenuItem,
            this.toolStripSeparator27,
            this.clearAllToolStripMenuItem,
            this.clearSelectedToolStripMenuItem,
            this.toolStripSeparator29,
            this.saveAllToolStripMenuItem,
            this.saveSelectedToolStripMenuItem});
            this.logContextMenuStrip.Name = "logContextMenuStrip";
            this.logContextMenuStrip.Size = new System.Drawing.Size(150, 148);
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.copyAllToolStripMenuItem.Text = "Copy All";
            this.copyAllToolStripMenuItem.Click += new System.EventHandler(this.copyAllToolStripMenuItem_Click);
            // 
            // copySelectedToolStripMenuItem
            // 
            this.copySelectedToolStripMenuItem.Name = "copySelectedToolStripMenuItem";
            this.copySelectedToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.copySelectedToolStripMenuItem.Text = "Copy Selected";
            this.copySelectedToolStripMenuItem.Click += new System.EventHandler(this.copySelectedToolStripMenuItem_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(146, 6);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // clearSelectedToolStripMenuItem
            // 
            this.clearSelectedToolStripMenuItem.Name = "clearSelectedToolStripMenuItem";
            this.clearSelectedToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.clearSelectedToolStripMenuItem.Text = "Clear Selected";
            this.clearSelectedToolStripMenuItem.Click += new System.EventHandler(this.clearSelectedToolStripMenuItem_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(146, 6);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.saveAllToolStripMenuItem.Text = "Save All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // saveSelectedToolStripMenuItem
            // 
            this.saveSelectedToolStripMenuItem.Name = "saveSelectedToolStripMenuItem";
            this.saveSelectedToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.saveSelectedToolStripMenuItem.Text = "Save Selected";
            this.saveSelectedToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedToolStripMenuItem_Click);
            // 
            // rootContextMenuStrip
            // 
            this.rootContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEntityMenuItem,
            this.refreshRootMenuItem,
            this.toolStripSeparator9,
            this.exportEntityMenuItem,
            this.importEntityMenuItem,
            this.toolStripSeparator15,
            this.expandSubTreeMenuItem1,
            this.collapseSubTreeMenuItem1,
            this.metricsToolStripSeparator,
            this.metricsSDIMenuItem,
            this.metricsMDIMenuItem});
            this.rootContextMenuStrip.Name = "rootContextMenuStrip";
            this.rootContextMenuStrip.Size = new System.Drawing.Size(237, 198);
            // 
            // deleteEntityMenuItem
            // 
            this.deleteEntityMenuItem.Name = "deleteEntityMenuItem";
            this.deleteEntityMenuItem.Size = new System.Drawing.Size(236, 22);
            this.deleteEntityMenuItem.Text = "Delete Entities";
            this.deleteEntityMenuItem.ToolTipText = "Delete the entities contained in the current namespace.";
            this.deleteEntityMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // refreshRootMenuItem
            // 
            this.refreshRootMenuItem.Name = "refreshRootMenuItem";
            this.refreshRootMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshRootMenuItem.Size = new System.Drawing.Size(236, 22);
            this.refreshRootMenuItem.Text = "Refresh Entities";
            this.refreshRootMenuItem.ToolTipText = "Refresh the entities contained in the current namespace.";
            this.refreshRootMenuItem.Click += new System.EventHandler(this.refreshEntityMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(233, 6);
            // 
            // exportEntityMenuItem
            // 
            this.exportEntityMenuItem.Name = "exportEntityMenuItem";
            this.exportEntityMenuItem.Size = new System.Drawing.Size(236, 22);
            this.exportEntityMenuItem.Text = "Export Entities";
            this.exportEntityMenuItem.ToolTipText = "Export entity definition to file.";
            this.exportEntityMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // importEntityMenuItem
            // 
            this.importEntityMenuItem.Name = "importEntityMenuItem";
            this.importEntityMenuItem.Size = new System.Drawing.Size(236, 22);
            this.importEntityMenuItem.Text = "Import Entities";
            this.importEntityMenuItem.ToolTipText = "Import entity definition from file.";
            this.importEntityMenuItem.Click += new System.EventHandler(this.importEntity_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(233, 6);
            // 
            // expandSubTreeMenuItem1
            // 
            this.expandSubTreeMenuItem1.Name = "expandSubTreeMenuItem1";
            this.expandSubTreeMenuItem1.Size = new System.Drawing.Size(236, 22);
            this.expandSubTreeMenuItem1.Text = "Expand Subtree";
            this.expandSubTreeMenuItem1.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem1.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem1
            // 
            this.collapseSubTreeMenuItem1.Name = "collapseSubTreeMenuItem1";
            this.collapseSubTreeMenuItem1.Size = new System.Drawing.Size(236, 22);
            this.collapseSubTreeMenuItem1.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem1.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem1.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // metricsToolStripSeparator
            // 
            this.metricsToolStripSeparator.Name = "metricsToolStripSeparator";
            this.metricsToolStripSeparator.Size = new System.Drawing.Size(233, 6);
            // 
            // metricsSDIMenuItem
            // 
            this.metricsSDIMenuItem.Name = "metricsSDIMenuItem";
            this.metricsSDIMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.metricsSDIMenuItem.Size = new System.Drawing.Size(236, 22);
            this.metricsSDIMenuItem.Text = "Open Metrics in SDI Mode";
            this.metricsSDIMenuItem.ToolTipText = "Access metrics data for the current namespace.";
            this.metricsSDIMenuItem.Click += new System.EventHandler(this.openMetrics_Click);
            // 
            // metricsMDIMenuItem
            // 
            this.metricsMDIMenuItem.Name = "metricsMDIMenuItem";
            this.metricsMDIMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.metricsMDIMenuItem.Size = new System.Drawing.Size(236, 22);
            this.metricsMDIMenuItem.Text = "Open Metrics in MDI Mode";
            this.metricsMDIMenuItem.ToolTipText = "Access metrics data for the current namespace.";
            this.metricsMDIMenuItem.Click += new System.EventHandler(this.openMetrics_Click);
            // 
            // queuesContextMenuStrip
            // 
            this.queuesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createQueueMenuItem,
            this.deleteQueuesMenuItem,
            this.toolStripSeparator37,
            this.refreshQueuesMenuItem,
            this.toolStripSeparator36,
            this.filterQueueMenuItem,
            this.toolStripSeparator3,
            this.exportQueuesMenuItem,
            this.toolStripSeparator14,
            this.expandSubTreeMenuItem2,
            this.collapseSubTreeMenuItem2});
            this.queuesContextMenuStrip.Name = "createContextMenuStrip";
            this.queuesContextMenuStrip.Size = new System.Drawing.Size(176, 182);
            // 
            // createQueueMenuItem
            // 
            this.createQueueMenuItem.Name = "createQueueMenuItem";
            this.createQueueMenuItem.Size = new System.Drawing.Size(175, 22);
            this.createQueueMenuItem.Text = "Create Queue";
            this.createQueueMenuItem.ToolTipText = "Create a new queue.";
            this.createQueueMenuItem.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // deleteQueuesMenuItem
            // 
            this.deleteQueuesMenuItem.Name = "deleteQueuesMenuItem";
            this.deleteQueuesMenuItem.Size = new System.Drawing.Size(175, 22);
            this.deleteQueuesMenuItem.Text = "Delete Queues";
            this.deleteQueuesMenuItem.ToolTipText = "Deletes all queues in the current namespace.";
            this.deleteQueuesMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // toolStripSeparator37
            // 
            this.toolStripSeparator37.Name = "toolStripSeparator37";
            this.toolStripSeparator37.Size = new System.Drawing.Size(172, 6);
            // 
            // refreshQueuesMenuItem
            // 
            this.refreshQueuesMenuItem.Name = "refreshQueuesMenuItem";
            this.refreshQueuesMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshQueuesMenuItem.Size = new System.Drawing.Size(175, 22);
            this.refreshQueuesMenuItem.Text = "Refresh Queues";
            this.refreshQueuesMenuItem.ToolTipText = "Refresh all queues in the current namespace.";
            this.refreshQueuesMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator36
            // 
            this.toolStripSeparator36.Name = "toolStripSeparator36";
            this.toolStripSeparator36.Size = new System.Drawing.Size(172, 6);
            // 
            // filterQueueMenuItem
            // 
            this.filterQueueMenuItem.Name = "filterQueueMenuItem";
            this.filterQueueMenuItem.Size = new System.Drawing.Size(175, 22);
            this.filterQueueMenuItem.Text = "Filter Queues";
            this.filterQueueMenuItem.ToolTipText = "Define a filter expression for queues.";
            this.filterQueueMenuItem.Click += new System.EventHandler(this.filterEntity_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
            // 
            // exportQueuesMenuItem
            // 
            this.exportQueuesMenuItem.Name = "exportQueuesMenuItem";
            this.exportQueuesMenuItem.Size = new System.Drawing.Size(175, 22);
            this.exportQueuesMenuItem.Text = "Export Queues";
            this.exportQueuesMenuItem.ToolTipText = "Export queues definition to file.";
            this.exportQueuesMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(172, 6);
            // 
            // expandSubTreeMenuItem2
            // 
            this.expandSubTreeMenuItem2.Name = "expandSubTreeMenuItem2";
            this.expandSubTreeMenuItem2.Size = new System.Drawing.Size(175, 22);
            this.expandSubTreeMenuItem2.Text = "Expand Subtree";
            this.expandSubTreeMenuItem2.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem2.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem2
            // 
            this.collapseSubTreeMenuItem2.Name = "collapseSubTreeMenuItem2";
            this.collapseSubTreeMenuItem2.Size = new System.Drawing.Size(175, 22);
            this.collapseSubTreeMenuItem2.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem2.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem2.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // ruleContextMenuStrip
            // 
            this.ruleContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeRuleMenuItem});
            this.ruleContextMenuStrip.Name = "ruleContextMenuStrip";
            this.ruleContextMenuStrip.Size = new System.Drawing.Size(144, 26);
            // 
            // removeRuleMenuItem
            // 
            this.removeRuleMenuItem.Name = "removeRuleMenuItem";
            this.removeRuleMenuItem.Size = new System.Drawing.Size(143, 22);
            this.removeRuleMenuItem.Text = "Remove Rule";
            this.removeRuleMenuItem.ToolTipText = "Remove the current rule.";
            this.removeRuleMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // rulesContextMenuStrip
            // 
            this.rulesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRuleMenuItem2,
            this.deleteRulesMenuItem,
            this.toolStripSeparator10,
            this.refreshRulesMenuItem});
            this.rulesContextMenuStrip.Name = "rulesContextMenuStrip";
            this.rulesContextMenuStrip.Size = new System.Drawing.Size(164, 76);
            // 
            // addRuleMenuItem2
            // 
            this.addRuleMenuItem2.Name = "addRuleMenuItem2";
            this.addRuleMenuItem2.Size = new System.Drawing.Size(163, 22);
            this.addRuleMenuItem2.Text = "Add Rule";
            this.addRuleMenuItem2.ToolTipText = "Add a new rule.";
            this.addRuleMenuItem2.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // deleteRulesMenuItem
            // 
            this.deleteRulesMenuItem.Name = "deleteRulesMenuItem";
            this.deleteRulesMenuItem.Size = new System.Drawing.Size(163, 22);
            this.deleteRulesMenuItem.Text = "Delete Rules";
            this.deleteRulesMenuItem.ToolTipText = "Delete rules for the current subscription.";
            this.deleteRulesMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(160, 6);
            // 
            // refreshRulesMenuItem
            // 
            this.refreshRulesMenuItem.Name = "refreshRulesMenuItem";
            this.refreshRulesMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshRulesMenuItem.Size = new System.Drawing.Size(163, 22);
            this.refreshRulesMenuItem.Text = "Refresh Rules";
            this.refreshRulesMenuItem.ToolTipText = "Refresh rules for the current subscription.";
            this.refreshRulesMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // subscriptionsContextMenuStrip
            // 
            this.subscriptionsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSubscriptionMenuItem2,
            this.deleteSubscriptionsMenuItem,
            this.toolStripSeparator40,
            this.refreshSubscriptionsMenuItem,
            this.toolStripSeparator41,
            this.filterSubscriptionsMenuItem,
            this.toolStripSeparator19,
            this.expandSubTreeMenuItem6,
            this.collapseSubTreeMenuItem6});
            this.subscriptionsContextMenuStrip.Name = "subscriptionsContextMenuStrip";
            this.subscriptionsContextMenuStrip.Size = new System.Drawing.Size(207, 154);
            // 
            // addSubscriptionMenuItem2
            // 
            this.addSubscriptionMenuItem2.Name = "addSubscriptionMenuItem2";
            this.addSubscriptionMenuItem2.Size = new System.Drawing.Size(206, 22);
            this.addSubscriptionMenuItem2.Text = "Create Subscription";
            this.addSubscriptionMenuItem2.ToolTipText = "Add a new subscription.";
            this.addSubscriptionMenuItem2.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // deleteSubscriptionsMenuItem
            // 
            this.deleteSubscriptionsMenuItem.Name = "deleteSubscriptionsMenuItem";
            this.deleteSubscriptionsMenuItem.Size = new System.Drawing.Size(206, 22);
            this.deleteSubscriptionsMenuItem.Text = "Delete Subscriptions";
            this.deleteSubscriptionsMenuItem.ToolTipText = "Delete all subscription for the current topic.";
            this.deleteSubscriptionsMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // toolStripSeparator40
            // 
            this.toolStripSeparator40.Name = "toolStripSeparator40";
            this.toolStripSeparator40.Size = new System.Drawing.Size(203, 6);
            // 
            // refreshSubscriptionsMenuItem
            // 
            this.refreshSubscriptionsMenuItem.Name = "refreshSubscriptionsMenuItem";
            this.refreshSubscriptionsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshSubscriptionsMenuItem.Size = new System.Drawing.Size(206, 22);
            this.refreshSubscriptionsMenuItem.Text = "Refresh Subscriptions";
            this.refreshSubscriptionsMenuItem.ToolTipText = "Refresh subscriptions for the current topic.";
            this.refreshSubscriptionsMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator41
            // 
            this.toolStripSeparator41.Name = "toolStripSeparator41";
            this.toolStripSeparator41.Size = new System.Drawing.Size(203, 6);
            // 
            // filterSubscriptionsMenuItem
            // 
            this.filterSubscriptionsMenuItem.Name = "filterSubscriptionsMenuItem";
            this.filterSubscriptionsMenuItem.Size = new System.Drawing.Size(206, 22);
            this.filterSubscriptionsMenuItem.Text = "Filter Subscriptions";
            this.filterSubscriptionsMenuItem.ToolTipText = "Define a filter expression for  the subscriptions of the current topic.";
            this.filterSubscriptionsMenuItem.Click += new System.EventHandler(this.filterEntity_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(203, 6);
            // 
            // expandSubTreeMenuItem6
            // 
            this.expandSubTreeMenuItem6.Name = "expandSubTreeMenuItem6";
            this.expandSubTreeMenuItem6.Size = new System.Drawing.Size(206, 22);
            this.expandSubTreeMenuItem6.Text = "Expand Subtree";
            this.expandSubTreeMenuItem6.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem6.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem6
            // 
            this.collapseSubTreeMenuItem6.Name = "collapseSubTreeMenuItem6";
            this.collapseSubTreeMenuItem6.Size = new System.Drawing.Size(206, 22);
            this.collapseSubTreeMenuItem6.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem6.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem6.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // subscriptionContextMenuStrip
            // 
            this.subscriptionContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeSubscriptionMenuItem,
            this.changeStatusSubscriptionMenuItem,
            this.refreshSubscriptionMenuItem,
            this.toolStripSeparator7,
            this.addRuleMenuItem1,
            this.toolStripSeparator8,
            this.copySubscriptionUrlMenuItem,
            this.copySubscriptionDeadletterSubscriptionUrlMenuItem,
            this.toolStripSeparator20,
            this.expandSubTreeMenuItem7,
            this.collapseSubTreeMenuItem7,
            this.toolStripSeparator33,
            this.testSubscriptionSDIMenuItem,
            this.testSubscriptionMDIMenuItem,
            this.toolStripSeparator30,
            this.createSubscriptionListenerMenuItem,
            this.toolStripSeparator28,
            this.subReceiveAllMessagesMenuItem,
            this.subReceiveTopMessagesMenuItem,
            this.subPeekTopMessagesMenuItem,
            this.subReceiveToolStripSeparator,
            this.subReceiveAllDeadletterQueueMessagesMenuItem,
            this.subReceiveTopDeadletterQueueMessagesMenuItem,
            this.subPeekTopDeadletterQueueMessagesMenuItem,
            this.getSubscriptionMessageSessionsSeparator,
            this.getSubscriptionMessageSessionsMenuItem});
            this.subscriptionContextMenuStrip.Name = "subscriptionContextMenuStrip";
            this.subscriptionContextMenuStrip.Size = new System.Drawing.Size(336, 448);
            // 
            // removeSubscriptionMenuItem
            // 
            this.removeSubscriptionMenuItem.Name = "removeSubscriptionMenuItem";
            this.removeSubscriptionMenuItem.Size = new System.Drawing.Size(335, 22);
            this.removeSubscriptionMenuItem.Text = "Delete Subscription";
            this.removeSubscriptionMenuItem.ToolTipText = "Delete the current subscription.";
            this.removeSubscriptionMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // changeStatusSubscriptionMenuItem
            // 
            this.changeStatusSubscriptionMenuItem.Name = "changeStatusSubscriptionMenuItem";
            this.changeStatusSubscriptionMenuItem.Size = new System.Drawing.Size(335, 22);
            this.changeStatusSubscriptionMenuItem.Text = "Change Status Subscription";
            this.changeStatusSubscriptionMenuItem.Click += new System.EventHandler(this.changeStatusEntity_Click);
            // 
            // refreshSubscriptionMenuItem
            // 
            this.refreshSubscriptionMenuItem.Name = "refreshSubscriptionMenuItem";
            this.refreshSubscriptionMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshSubscriptionMenuItem.Size = new System.Drawing.Size(335, 22);
            this.refreshSubscriptionMenuItem.Text = "Refresh Subscription";
            this.refreshSubscriptionMenuItem.ToolTipText = "Refresh the current subscription.";
            this.refreshSubscriptionMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(332, 6);
            // 
            // addRuleMenuItem1
            // 
            this.addRuleMenuItem1.Name = "addRuleMenuItem1";
            this.addRuleMenuItem1.Size = new System.Drawing.Size(335, 22);
            this.addRuleMenuItem1.Text = "Add Rule";
            this.addRuleMenuItem1.ToolTipText = "Add a new rule.";
            this.addRuleMenuItem1.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(332, 6);
            // 
            // copySubscriptionUrlMenuItem
            // 
            this.copySubscriptionUrlMenuItem.Name = "copySubscriptionUrlMenuItem";
            this.copySubscriptionUrlMenuItem.Size = new System.Drawing.Size(335, 22);
            this.copySubscriptionUrlMenuItem.Text = "Copy Subscription Url";
            this.copySubscriptionUrlMenuItem.ToolTipText = "Copy the subscription url to the clipboard.";
            this.copySubscriptionUrlMenuItem.Click += new System.EventHandler(this.copyEntityUrl_Click);
            // 
            // copySubscriptionDeadletterSubscriptionUrlMenuItem
            // 
            this.copySubscriptionDeadletterSubscriptionUrlMenuItem.Name = "copySubscriptionDeadletterSubscriptionUrlMenuItem";
            this.copySubscriptionDeadletterSubscriptionUrlMenuItem.Size = new System.Drawing.Size(335, 22);
            this.copySubscriptionDeadletterSubscriptionUrlMenuItem.Text = "Copy Deadletter Queue Url";
            this.copySubscriptionDeadletterSubscriptionUrlMenuItem.ToolTipText = "Copy the deadletter queue url to the clipboard.";
            this.copySubscriptionDeadletterSubscriptionUrlMenuItem.Click += new System.EventHandler(this.copyEntityUrl_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(332, 6);
            // 
            // expandSubTreeMenuItem7
            // 
            this.expandSubTreeMenuItem7.Name = "expandSubTreeMenuItem7";
            this.expandSubTreeMenuItem7.Size = new System.Drawing.Size(335, 22);
            this.expandSubTreeMenuItem7.Text = "Expand Subtree";
            this.expandSubTreeMenuItem7.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem7.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem7
            // 
            this.collapseSubTreeMenuItem7.Name = "collapseSubTreeMenuItem7";
            this.collapseSubTreeMenuItem7.Size = new System.Drawing.Size(335, 22);
            this.collapseSubTreeMenuItem7.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem7.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem7.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // toolStripSeparator33
            // 
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            this.toolStripSeparator33.Size = new System.Drawing.Size(332, 6);
            // 
            // testSubscriptionSDIMenuItem
            // 
            this.testSubscriptionSDIMenuItem.Name = "testSubscriptionSDIMenuItem";
            this.testSubscriptionSDIMenuItem.Size = new System.Drawing.Size(335, 22);
            this.testSubscriptionSDIMenuItem.Text = "Test Subscription In SDI Mode";
            this.testSubscriptionSDIMenuItem.ToolTipText = "Test the current subscription in SDI mode.";
            this.testSubscriptionSDIMenuItem.Click += new System.EventHandler(this.testEntityInSDIMode_Click);
            // 
            // testSubscriptionMDIMenuItem
            // 
            this.testSubscriptionMDIMenuItem.Name = "testSubscriptionMDIMenuItem";
            this.testSubscriptionMDIMenuItem.Size = new System.Drawing.Size(335, 22);
            this.testSubscriptionMDIMenuItem.Text = "Test Subscription In MDI Mode";
            this.testSubscriptionMDIMenuItem.ToolTipText = "Test the current subscription in MDI mode.";
            this.testSubscriptionMDIMenuItem.Click += new System.EventHandler(this.testEntityInMDIMode_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(332, 6);
            // 
            // createSubscriptionListenerMenuItem
            // 
            this.createSubscriptionListenerMenuItem.Name = "createSubscriptionListenerMenuItem";
            this.createSubscriptionListenerMenuItem.Size = new System.Drawing.Size(335, 22);
            this.createSubscriptionListenerMenuItem.Text = "Create Subscription Listener";
            this.createSubscriptionListenerMenuItem.ToolTipText = "Create a subscription listener.";
            this.createSubscriptionListenerMenuItem.Click += new System.EventHandler(this.createEntityListenerMenuItem_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(332, 6);
            // 
            // subReceiveAllMessagesMenuItem
            // 
            this.subReceiveAllMessagesMenuItem.Name = "subReceiveAllMessagesMenuItem";
            this.subReceiveAllMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.subReceiveAllMessagesMenuItem.Text = "Receive All Messages";
            this.subReceiveAllMessagesMenuItem.ToolTipText = "Receive all messages from the current queue.";
            this.subReceiveAllMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // subReceiveTopMessagesMenuItem
            // 
            this.subReceiveTopMessagesMenuItem.Name = "subReceiveTopMessagesMenuItem";
            this.subReceiveTopMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.subReceiveTopMessagesMenuItem.Text = "Receive TopCount 10 Messages";
            this.subReceiveTopMessagesMenuItem.ToolTipText = "Receive topCount 10 messages from the current queue.";
            this.subReceiveTopMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // subPeekTopMessagesMenuItem
            // 
            this.subPeekTopMessagesMenuItem.Name = "subPeekTopMessagesMenuItem";
            this.subPeekTopMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.subPeekTopMessagesMenuItem.Text = "Peek TopCount 10 Messages";
            this.subPeekTopMessagesMenuItem.ToolTipText = "Peek topCount 10 messages from the current queue.";
            this.subPeekTopMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // subReceiveToolStripSeparator
            // 
            this.subReceiveToolStripSeparator.Name = "subReceiveToolStripSeparator";
            this.subReceiveToolStripSeparator.Size = new System.Drawing.Size(332, 6);
            // 
            // subReceiveAllDeadletterQueueMessagesMenuItem
            // 
            this.subReceiveAllDeadletterQueueMessagesMenuItem.Name = "subReceiveAllDeadletterQueueMessagesMenuItem";
            this.subReceiveAllDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.subReceiveAllDeadletterQueueMessagesMenuItem.Text = "Receive All Deadletter Queue Messages";
            this.subReceiveAllDeadletterQueueMessagesMenuItem.ToolTipText = "Receive all messages from the deadletter queue.";
            this.subReceiveAllDeadletterQueueMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // subReceiveTopDeadletterQueueMessagesMenuItem
            // 
            this.subReceiveTopDeadletterQueueMessagesMenuItem.Name = "subReceiveTopDeadletterQueueMessagesMenuItem";
            this.subReceiveTopDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.subReceiveTopDeadletterQueueMessagesMenuItem.Text = "Receive TopCount 10 Deadletter Queue Messages";
            this.subReceiveTopDeadletterQueueMessagesMenuItem.ToolTipText = "Receive top messages from the deadletter queue.";
            this.subReceiveTopDeadletterQueueMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // subPeekTopDeadletterQueueMessagesMenuItem
            // 
            this.subPeekTopDeadletterQueueMessagesMenuItem.Name = "subPeekTopDeadletterQueueMessagesMenuItem";
            this.subPeekTopDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.subPeekTopDeadletterQueueMessagesMenuItem.Text = "Peek TopCount 10 Deadletter Queue Messages";
            this.subPeekTopDeadletterQueueMessagesMenuItem.ToolTipText = "Peek top messages from the deadletter queue.";
            this.subPeekTopDeadletterQueueMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // getSubscriptionMessageSessionsSeparator
            // 
            this.getSubscriptionMessageSessionsSeparator.Name = "getSubscriptionMessageSessionsSeparator";
            this.getSubscriptionMessageSessionsSeparator.Size = new System.Drawing.Size(332, 6);
            // 
            // getSubscriptionMessageSessionsMenuItem
            // 
            this.getSubscriptionMessageSessionsMenuItem.Name = "getSubscriptionMessageSessionsMenuItem";
            this.getSubscriptionMessageSessionsMenuItem.Size = new System.Drawing.Size(335, 22);
            this.getSubscriptionMessageSessionsMenuItem.Text = "Get Message Sessions";
            this.getSubscriptionMessageSessionsMenuItem.ToolTipText = "Retrieves all message sessions whose session state was updated since lastUpdatedT" +
    "ime.";
            this.getSubscriptionMessageSessionsMenuItem.Click += new System.EventHandler(this.getMessageSessions_Click);
            // 
            // topicContextMenuStrip
            // 
            this.topicContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteTopicMenuItem,
            this.changeStatusTopicMenuItem,
            this.refreshTopicMenuItem,
            this.toolStripSeparator6,
            this.exportTopicMenuItem,
            this.toolStripSeparator2,
            this.addSubscriptionMenuItem1,
            this.deleteTopicSubscriptionsMenuItem,
            this.toolStripSeparator12,
            this.copyTopicUrlMenuItem,
            this.toolStripSeparator18,
            this.expandSubTreeMenuItem5,
            this.collapseSubTreeMenuItem5,
            this.toolStripSeparator32,
            this.testTopicSDIMenuItem,
            this.testTopicMDIMenuItem,
            this.toolStripSeparator35,
            this.sendMessagesTopicMenuItem});
            this.topicContextMenuStrip.Name = "topicContextMenuStrip";
            this.topicContextMenuStrip.Size = new System.Drawing.Size(202, 304);
            // 
            // deleteTopicMenuItem
            // 
            this.deleteTopicMenuItem.Name = "deleteTopicMenuItem";
            this.deleteTopicMenuItem.Size = new System.Drawing.Size(201, 22);
            this.deleteTopicMenuItem.Text = "Delete Topic";
            this.deleteTopicMenuItem.ToolTipText = "Delete the current topic.";
            this.deleteTopicMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // changeStatusTopicMenuItem
            // 
            this.changeStatusTopicMenuItem.Name = "changeStatusTopicMenuItem";
            this.changeStatusTopicMenuItem.Size = new System.Drawing.Size(201, 22);
            this.changeStatusTopicMenuItem.Text = "Change Status Topic";
            this.changeStatusTopicMenuItem.Click += new System.EventHandler(this.changeStatusEntity_Click);
            // 
            // refreshTopicMenuItem
            // 
            this.refreshTopicMenuItem.Name = "refreshTopicMenuItem";
            this.refreshTopicMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshTopicMenuItem.Size = new System.Drawing.Size(201, 22);
            this.refreshTopicMenuItem.Text = "Refresh Topic";
            this.refreshTopicMenuItem.ToolTipText = "Refresh the current topic.";
            this.refreshTopicMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(198, 6);
            // 
            // exportTopicMenuItem
            // 
            this.exportTopicMenuItem.Name = "exportTopicMenuItem";
            this.exportTopicMenuItem.Size = new System.Drawing.Size(201, 22);
            this.exportTopicMenuItem.Text = "Export Topic";
            this.exportTopicMenuItem.ToolTipText = "Export topic definition to file.";
            this.exportTopicMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // addSubscriptionMenuItem1
            // 
            this.addSubscriptionMenuItem1.Name = "addSubscriptionMenuItem1";
            this.addSubscriptionMenuItem1.Size = new System.Drawing.Size(201, 22);
            this.addSubscriptionMenuItem1.Text = "Create Subscription";
            this.addSubscriptionMenuItem1.ToolTipText = "Create a new subscription to the current topic.";
            this.addSubscriptionMenuItem1.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // deleteTopicSubscriptionsMenuItem
            // 
            this.deleteTopicSubscriptionsMenuItem.Name = "deleteTopicSubscriptionsMenuItem";
            this.deleteTopicSubscriptionsMenuItem.Size = new System.Drawing.Size(201, 22);
            this.deleteTopicSubscriptionsMenuItem.Text = "Delete Subscriptions";
            this.deleteTopicSubscriptionsMenuItem.ToolTipText = "Delete all subscription for the current topic.";
            this.deleteTopicSubscriptionsMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(198, 6);
            // 
            // copyTopicUrlMenuItem
            // 
            this.copyTopicUrlMenuItem.Name = "copyTopicUrlMenuItem";
            this.copyTopicUrlMenuItem.Size = new System.Drawing.Size(201, 22);
            this.copyTopicUrlMenuItem.Text = "Copy Topic Url";
            this.copyTopicUrlMenuItem.ToolTipText = "Copy the topic url to the clipboard.";
            this.copyTopicUrlMenuItem.Click += new System.EventHandler(this.copyEntityUrl_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(198, 6);
            // 
            // expandSubTreeMenuItem5
            // 
            this.expandSubTreeMenuItem5.Name = "expandSubTreeMenuItem5";
            this.expandSubTreeMenuItem5.Size = new System.Drawing.Size(201, 22);
            this.expandSubTreeMenuItem5.Text = "Expand Subtree";
            this.expandSubTreeMenuItem5.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem5.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem5
            // 
            this.collapseSubTreeMenuItem5.Name = "collapseSubTreeMenuItem5";
            this.collapseSubTreeMenuItem5.Size = new System.Drawing.Size(201, 22);
            this.collapseSubTreeMenuItem5.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem5.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem5.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            this.toolStripSeparator32.Size = new System.Drawing.Size(198, 6);
            // 
            // testTopicSDIMenuItem
            // 
            this.testTopicSDIMenuItem.Name = "testTopicSDIMenuItem";
            this.testTopicSDIMenuItem.Size = new System.Drawing.Size(201, 22);
            this.testTopicSDIMenuItem.Text = "Test Topic In SDI Mode";
            this.testTopicSDIMenuItem.ToolTipText = "Test the current topic in SDI mode.";
            this.testTopicSDIMenuItem.Click += new System.EventHandler(this.testEntityInSDIMode_Click);
            // 
            // testTopicMDIMenuItem
            // 
            this.testTopicMDIMenuItem.Name = "testTopicMDIMenuItem";
            this.testTopicMDIMenuItem.Size = new System.Drawing.Size(201, 22);
            this.testTopicMDIMenuItem.Text = "Test Topic In MDI Mode";
            this.testTopicMDIMenuItem.ToolTipText = "Test the current topic in MDI mode.";
            this.testTopicMDIMenuItem.Click += new System.EventHandler(this.testEntityInMDIMode_Click);
            // 
            // toolStripSeparator35
            // 
            this.toolStripSeparator35.Name = "toolStripSeparator35";
            this.toolStripSeparator35.Size = new System.Drawing.Size(198, 6);
            // 
            // sendMessagesTopicMenuItem
            // 
            this.sendMessagesTopicMenuItem.Name = "sendMessagesTopicMenuItem";
            this.sendMessagesTopicMenuItem.Size = new System.Drawing.Size(201, 22);
            this.sendMessagesTopicMenuItem.Text = "Send Messages";
            this.sendMessagesTopicMenuItem.ToolTipText = "Send test messages to the current queue.";
            this.sendMessagesTopicMenuItem.Click += new System.EventHandler(this.sendMessage_Click);
            // 
            // queueContextMenuStrip
            // 
            this.queueContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeStatusQueueMenuItem,
            this.deleteQueueMenuItem,
            this.refreshQueueMenuItem,
            this.toolStripSeparator5,
            this.exportQueueMenuItem,
            this.toolStripSeparator11,
            this.copyQueueUrlMenuItem,
            this.copyQueueDeadletterQueueUrlMenuItem,
            this.toolStripSeparator25,
            this.testQueueSDIMenuItem,
            this.testQueueMDIMenuItem,
            this.toolStripSeparator34,
            this.queueSendMessageMenuItem,
            this.createQueueListenerMenuItem,
            this.toolStripSeparator31,
            this.queueReceiveAllMessagesMenuItem,
            this.queueReceiveTopMessagesMenuItem,
            this.queuePeekTopMessagesMenuItem,
            this.queueReceiveToolStripSeparator,
            this.queueReceiveAllDeadletterQueueMessagesMenuItem,
            this.queueReceiveTopDeadletterQueueMessagesMenuItem,
            this.queuePeekTopDeadletterQueueMessagesMenuItem,
            this.getQueueMessageSessionsSeparator,
            this.getQueueMessageSessionsMenuItem});
            this.queueContextMenuStrip.Name = "nodeContextMenuStrip";
            this.queueContextMenuStrip.Size = new System.Drawing.Size(336, 420);
            // 
            // changeStatusQueueMenuItem
            // 
            this.changeStatusQueueMenuItem.Name = "changeStatusQueueMenuItem";
            this.changeStatusQueueMenuItem.Size = new System.Drawing.Size(335, 22);
            this.changeStatusQueueMenuItem.Text = "Change Status Queue";
            this.changeStatusQueueMenuItem.Click += new System.EventHandler(this.changeStatusEntity_Click);
            // 
            // deleteQueueMenuItem
            // 
            this.deleteQueueMenuItem.Name = "deleteQueueMenuItem";
            this.deleteQueueMenuItem.Size = new System.Drawing.Size(335, 22);
            this.deleteQueueMenuItem.Text = "Delete Queue";
            this.deleteQueueMenuItem.ToolTipText = "Delete the current queue.";
            this.deleteQueueMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // refreshQueueMenuItem
            // 
            this.refreshQueueMenuItem.Name = "refreshQueueMenuItem";
            this.refreshQueueMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshQueueMenuItem.Size = new System.Drawing.Size(335, 22);
            this.refreshQueueMenuItem.Text = "Refresh Queue";
            this.refreshQueueMenuItem.ToolTipText = "Refresh the current queue.";
            this.refreshQueueMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(332, 6);
            // 
            // exportQueueMenuItem
            // 
            this.exportQueueMenuItem.Name = "exportQueueMenuItem";
            this.exportQueueMenuItem.Size = new System.Drawing.Size(335, 22);
            this.exportQueueMenuItem.Text = "Export Queue";
            this.exportQueueMenuItem.ToolTipText = "Export queue definition to file.";
            this.exportQueueMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(332, 6);
            // 
            // copyQueueUrlMenuItem
            // 
            this.copyQueueUrlMenuItem.Name = "copyQueueUrlMenuItem";
            this.copyQueueUrlMenuItem.Size = new System.Drawing.Size(335, 22);
            this.copyQueueUrlMenuItem.Text = "Copy Queue Url";
            this.copyQueueUrlMenuItem.ToolTipText = "Copy the queue url to the clipboard.";
            this.copyQueueUrlMenuItem.Click += new System.EventHandler(this.copyEntityUrl_Click);
            // 
            // copyQueueDeadletterQueueUrlMenuItem
            // 
            this.copyQueueDeadletterQueueUrlMenuItem.Name = "copyQueueDeadletterQueueUrlMenuItem";
            this.copyQueueDeadletterQueueUrlMenuItem.Size = new System.Drawing.Size(335, 22);
            this.copyQueueDeadletterQueueUrlMenuItem.Text = "Copy Deadletter Queue Url";
            this.copyQueueDeadletterQueueUrlMenuItem.ToolTipText = "Copy the deadletter queue url to the clipboard.";
            this.copyQueueDeadletterQueueUrlMenuItem.Click += new System.EventHandler(this.copyEntityUrl_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(332, 6);
            // 
            // testQueueSDIMenuItem
            // 
            this.testQueueSDIMenuItem.Name = "testQueueSDIMenuItem";
            this.testQueueSDIMenuItem.Size = new System.Drawing.Size(335, 22);
            this.testQueueSDIMenuItem.Text = "Test Queue In SDI Mode";
            this.testQueueSDIMenuItem.ToolTipText = "Test the current queue in SDI mode.";
            this.testQueueSDIMenuItem.Click += new System.EventHandler(this.testEntityInSDIMode_Click);
            // 
            // testQueueMDIMenuItem
            // 
            this.testQueueMDIMenuItem.Name = "testQueueMDIMenuItem";
            this.testQueueMDIMenuItem.Size = new System.Drawing.Size(335, 22);
            this.testQueueMDIMenuItem.Text = "Test Queue In MDI Mode";
            this.testQueueMDIMenuItem.ToolTipText = "Test the current queue in MDI mode.";
            this.testQueueMDIMenuItem.Click += new System.EventHandler(this.testEntityInMDIMode_Click);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new System.Drawing.Size(332, 6);
            // 
            // queueSendMessageMenuItem
            // 
            this.queueSendMessageMenuItem.Name = "queueSendMessageMenuItem";
            this.queueSendMessageMenuItem.Size = new System.Drawing.Size(335, 22);
            this.queueSendMessageMenuItem.Text = "Send Messages";
            this.queueSendMessageMenuItem.ToolTipText = "Send test messages to the current queue.";
            this.queueSendMessageMenuItem.Click += new System.EventHandler(this.sendMessage_Click);
            // 
            // createQueueListenerMenuItem
            // 
            this.createQueueListenerMenuItem.Name = "createQueueListenerMenuItem";
            this.createQueueListenerMenuItem.Size = new System.Drawing.Size(335, 22);
            this.createQueueListenerMenuItem.Text = "Create Queue Listener";
            this.createQueueListenerMenuItem.ToolTipText = "Create a queue listener.";
            this.createQueueListenerMenuItem.Click += new System.EventHandler(this.createEntityListenerMenuItem_Click);
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            this.toolStripSeparator31.Size = new System.Drawing.Size(332, 6);
            // 
            // queueReceiveAllMessagesMenuItem
            // 
            this.queueReceiveAllMessagesMenuItem.Name = "queueReceiveAllMessagesMenuItem";
            this.queueReceiveAllMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.queueReceiveAllMessagesMenuItem.Text = "Receive All Messages";
            this.queueReceiveAllMessagesMenuItem.ToolTipText = "Receive all messages from the current queue.";
            this.queueReceiveAllMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // queueReceiveTopMessagesMenuItem
            // 
            this.queueReceiveTopMessagesMenuItem.Name = "queueReceiveTopMessagesMenuItem";
            this.queueReceiveTopMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.queueReceiveTopMessagesMenuItem.Text = "Receive TopCount 10 Messages";
            this.queueReceiveTopMessagesMenuItem.ToolTipText = "Receive topCount 10 messages from the current queue.";
            this.queueReceiveTopMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // queuePeekTopMessagesMenuItem
            // 
            this.queuePeekTopMessagesMenuItem.Name = "queuePeekTopMessagesMenuItem";
            this.queuePeekTopMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.queuePeekTopMessagesMenuItem.Text = "Peek TopCount 10 Messages";
            this.queuePeekTopMessagesMenuItem.ToolTipText = "Peek topCount 10 messages from the current queue.";
            this.queuePeekTopMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // queueReceiveToolStripSeparator
            // 
            this.queueReceiveToolStripSeparator.Name = "queueReceiveToolStripSeparator";
            this.queueReceiveToolStripSeparator.Size = new System.Drawing.Size(332, 6);
            // 
            // queueReceiveAllDeadletterQueueMessagesMenuItem
            // 
            this.queueReceiveAllDeadletterQueueMessagesMenuItem.Name = "queueReceiveAllDeadletterQueueMessagesMenuItem";
            this.queueReceiveAllDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.queueReceiveAllDeadletterQueueMessagesMenuItem.Text = "Receive All Deadletter Queue Messages";
            this.queueReceiveAllDeadletterQueueMessagesMenuItem.ToolTipText = "Receive all messages from the deadletter queue.";
            this.queueReceiveAllDeadletterQueueMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // queueReceiveTopDeadletterQueueMessagesMenuItem
            // 
            this.queueReceiveTopDeadletterQueueMessagesMenuItem.Name = "queueReceiveTopDeadletterQueueMessagesMenuItem";
            this.queueReceiveTopDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.queueReceiveTopDeadletterQueueMessagesMenuItem.Text = "Receive TopCount 10 Deadletter Queue Messages";
            this.queueReceiveTopDeadletterQueueMessagesMenuItem.ToolTipText = "Receive top messages from the deadletter queue.";
            this.queueReceiveTopDeadletterQueueMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // queuePeekTopDeadletterQueueMessagesMenuItem
            // 
            this.queuePeekTopDeadletterQueueMessagesMenuItem.Name = "queuePeekTopDeadletterQueueMessagesMenuItem";
            this.queuePeekTopDeadletterQueueMessagesMenuItem.Size = new System.Drawing.Size(335, 22);
            this.queuePeekTopDeadletterQueueMessagesMenuItem.Text = "Peek TopCount 10 Deadletter Queue Messages";
            this.queuePeekTopDeadletterQueueMessagesMenuItem.ToolTipText = "Peek top messages from the deadletter queue.";
            this.queuePeekTopDeadletterQueueMessagesMenuItem.Click += new System.EventHandler(this.receiveMessages_Click);
            // 
            // getQueueMessageSessionsSeparator
            // 
            this.getQueueMessageSessionsSeparator.Name = "getQueueMessageSessionsSeparator";
            this.getQueueMessageSessionsSeparator.Size = new System.Drawing.Size(332, 6);
            // 
            // getQueueMessageSessionsMenuItem
            // 
            this.getQueueMessageSessionsMenuItem.Name = "getQueueMessageSessionsMenuItem";
            this.getQueueMessageSessionsMenuItem.Size = new System.Drawing.Size(335, 22);
            this.getQueueMessageSessionsMenuItem.Text = "Get Message Sessions";
            this.getQueueMessageSessionsMenuItem.ToolTipText = "Retrieves all message sessions whose session state was updated since lastUpdatedT" +
    "ime.";
            this.getQueueMessageSessionsMenuItem.Click += new System.EventHandler(this.getMessageSessions_Click);
            // 
            // topicsContextMenuStrip
            // 
            this.topicsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createTopicMenuItem,
            this.deleteTopicsMenuItem,
            this.toolStripSeparator38,
            this.refreshTopicsMenuItem,
            this.toolStripSeparator4,
            this.filterTopicsMenuItem,
            this.toolStripSeparator39,
            this.exportTopicsMenuItem,
            this.toolStripSeparator16,
            this.expandSubTreeMenuItem3,
            this.collapseSubTreeMenuItem3});
            this.topicsContextMenuStrip.Name = "createContextMenuStrip";
            this.topicsContextMenuStrip.Size = new System.Drawing.Size(171, 182);
            // 
            // createTopicMenuItem
            // 
            this.createTopicMenuItem.Name = "createTopicMenuItem";
            this.createTopicMenuItem.Size = new System.Drawing.Size(170, 22);
            this.createTopicMenuItem.Text = "Create Topic";
            this.createTopicMenuItem.ToolTipText = "Create a new topic.";
            this.createTopicMenuItem.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // deleteTopicsMenuItem
            // 
            this.deleteTopicsMenuItem.Name = "deleteTopicsMenuItem";
            this.deleteTopicsMenuItem.Size = new System.Drawing.Size(170, 22);
            this.deleteTopicsMenuItem.Text = "Delete Topics";
            this.deleteTopicsMenuItem.ToolTipText = "Delete all topics in the current namespace.";
            this.deleteTopicsMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            this.toolStripSeparator38.Size = new System.Drawing.Size(167, 6);
            // 
            // refreshTopicsMenuItem
            // 
            this.refreshTopicsMenuItem.Name = "refreshTopicsMenuItem";
            this.refreshTopicsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshTopicsMenuItem.Size = new System.Drawing.Size(170, 22);
            this.refreshTopicsMenuItem.Text = "Refresh Topics";
            this.refreshTopicsMenuItem.ToolTipText = "Refresh all topics in the current namespace.";
            this.refreshTopicsMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
            // 
            // filterTopicsMenuItem
            // 
            this.filterTopicsMenuItem.Name = "filterTopicsMenuItem";
            this.filterTopicsMenuItem.Size = new System.Drawing.Size(170, 22);
            this.filterTopicsMenuItem.Text = "Filter Topics";
            this.filterTopicsMenuItem.ToolTipText = "Define a filter expression for topics.";
            this.filterTopicsMenuItem.Click += new System.EventHandler(this.filterEntity_Click);
            // 
            // toolStripSeparator39
            // 
            this.toolStripSeparator39.Name = "toolStripSeparator39";
            this.toolStripSeparator39.Size = new System.Drawing.Size(167, 6);
            // 
            // exportTopicsMenuItem
            // 
            this.exportTopicsMenuItem.Name = "exportTopicsMenuItem";
            this.exportTopicsMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exportTopicsMenuItem.Text = "Export Topics";
            this.exportTopicsMenuItem.ToolTipText = "Export topics definition to file.";
            this.exportTopicsMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(167, 6);
            // 
            // expandSubTreeMenuItem3
            // 
            this.expandSubTreeMenuItem3.Name = "expandSubTreeMenuItem3";
            this.expandSubTreeMenuItem3.Size = new System.Drawing.Size(170, 22);
            this.expandSubTreeMenuItem3.Text = "Expand Subtree";
            this.expandSubTreeMenuItem3.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem3.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem3
            // 
            this.collapseSubTreeMenuItem3.Name = "collapseSubTreeMenuItem3";
            this.collapseSubTreeMenuItem3.Size = new System.Drawing.Size(170, 22);
            this.collapseSubTreeMenuItem3.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem3.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem3.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // relayServicesContextMenuStrip
            // 
            this.relayServicesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshRelayServicesMenuItem,
            this.toolStripSeparator24,
            this.expandSubTreeMenuItem11,
            this.collapseSubTreeMenuItem11});
            this.relayServicesContextMenuStrip.Name = "relayServicesContextMenuStrip";
            this.relayServicesContextMenuStrip.Size = new System.Drawing.Size(209, 76);
            // 
            // refreshRelayServicesMenuItem
            // 
            this.refreshRelayServicesMenuItem.Name = "refreshRelayServicesMenuItem";
            this.refreshRelayServicesMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshRelayServicesMenuItem.Size = new System.Drawing.Size(208, 22);
            this.refreshRelayServicesMenuItem.Text = "Refresh Relay Services";
            this.refreshRelayServicesMenuItem.ToolTipText = "Refresh all relay services in the current namespace.";
            this.refreshRelayServicesMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(205, 6);
            // 
            // expandSubTreeMenuItem11
            // 
            this.expandSubTreeMenuItem11.Name = "expandSubTreeMenuItem11";
            this.expandSubTreeMenuItem11.Size = new System.Drawing.Size(208, 22);
            this.expandSubTreeMenuItem11.Text = "Expand Subtree";
            this.expandSubTreeMenuItem11.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem11.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem11
            // 
            this.collapseSubTreeMenuItem11.Name = "collapseSubTreeMenuItem11";
            this.collapseSubTreeMenuItem11.Size = new System.Drawing.Size(208, 22);
            this.collapseSubTreeMenuItem11.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem11.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem11.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // relayServiceContextMenuStrip
            // 
            this.relayServiceContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyRelayServiceUrlMenuItem});
            this.relayServiceContextMenuStrip.Name = "subscriptionContextMenuStrip";
            this.relayServiceContextMenuStrip.Size = new System.Drawing.Size(192, 26);
            // 
            // copyRelayServiceUrlMenuItem
            // 
            this.copyRelayServiceUrlMenuItem.Name = "copyRelayServiceUrlMenuItem";
            this.copyRelayServiceUrlMenuItem.Size = new System.Drawing.Size(191, 22);
            this.copyRelayServiceUrlMenuItem.Text = "Copy Relay Service Url";
            this.copyRelayServiceUrlMenuItem.ToolTipText = "Copy the relay service url to the clipboard.";
            this.copyRelayServiceUrlMenuItem.Click += new System.EventHandler(this.copyEntityUrl_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1416, 24);
            this.mainMenuStrip.TabIndex = 22;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // queueFolderContextMenuStrip
            // 
            this.queueFolderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderCreateQueueMenuItem,
            this.folderDeleteQueuesMenuItem,
            this.toolStripSeparator1,
            this.folderExportQueuesMenuItem,
            this.toolStripSeparator22,
            this.expandSubTreeMenuItem9,
            this.collapseSubTreeMenuItem9});
            this.queueFolderContextMenuStrip.Name = "createContextMenuStrip";
            this.queueFolderContextMenuStrip.Size = new System.Drawing.Size(163, 126);
            // 
            // folderCreateQueueMenuItem
            // 
            this.folderCreateQueueMenuItem.Name = "folderCreateQueueMenuItem";
            this.folderCreateQueueMenuItem.Size = new System.Drawing.Size(162, 22);
            this.folderCreateQueueMenuItem.Text = "Create Queue";
            this.folderCreateQueueMenuItem.ToolTipText = "Create a new queue in the current path.";
            this.folderCreateQueueMenuItem.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // folderDeleteQueuesMenuItem
            // 
            this.folderDeleteQueuesMenuItem.Name = "folderDeleteQueuesMenuItem";
            this.folderDeleteQueuesMenuItem.Size = new System.Drawing.Size(162, 22);
            this.folderDeleteQueuesMenuItem.Text = "Delete Queues";
            this.folderDeleteQueuesMenuItem.ToolTipText = "Deletes all queues in the current path.";
            this.folderDeleteQueuesMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
            // 
            // folderExportQueuesMenuItem
            // 
            this.folderExportQueuesMenuItem.Name = "folderExportQueuesMenuItem";
            this.folderExportQueuesMenuItem.Size = new System.Drawing.Size(162, 22);
            this.folderExportQueuesMenuItem.Text = "Export Queues";
            this.folderExportQueuesMenuItem.ToolTipText = "Export the definition of the queues in the current path to file.";
            this.folderExportQueuesMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(159, 6);
            // 
            // expandSubTreeMenuItem9
            // 
            this.expandSubTreeMenuItem9.Name = "expandSubTreeMenuItem9";
            this.expandSubTreeMenuItem9.Size = new System.Drawing.Size(162, 22);
            this.expandSubTreeMenuItem9.Text = "Expand Subtree";
            this.expandSubTreeMenuItem9.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem9.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem9
            // 
            this.collapseSubTreeMenuItem9.Name = "collapseSubTreeMenuItem9";
            this.collapseSubTreeMenuItem9.Size = new System.Drawing.Size(162, 22);
            this.collapseSubTreeMenuItem9.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem9.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem9.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // topicFolderContextMenuStrip
            // 
            this.topicFolderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderCreateTopicMenuItem,
            this.folderDeleteTopicsMenuItem,
            this.toolStripSeparator13,
            this.folderExportTopicsMenuItem,
            this.toolStripSeparator23,
            this.expandSubTreeMenuItem10,
            this.collapseSubTreeMenuItem10});
            this.topicFolderContextMenuStrip.Name = "createContextMenuStrip";
            this.topicFolderContextMenuStrip.Size = new System.Drawing.Size(163, 126);
            // 
            // folderCreateTopicMenuItem
            // 
            this.folderCreateTopicMenuItem.Name = "folderCreateTopicMenuItem";
            this.folderCreateTopicMenuItem.Size = new System.Drawing.Size(162, 22);
            this.folderCreateTopicMenuItem.Text = "Create Topic";
            this.folderCreateTopicMenuItem.ToolTipText = "Create a new topic in the specified path.";
            this.folderCreateTopicMenuItem.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // folderDeleteTopicsMenuItem
            // 
            this.folderDeleteTopicsMenuItem.Name = "folderDeleteTopicsMenuItem";
            this.folderDeleteTopicsMenuItem.Size = new System.Drawing.Size(162, 22);
            this.folderDeleteTopicsMenuItem.Text = "Delete Topics";
            this.folderDeleteTopicsMenuItem.ToolTipText = "Delete all topics in the current path.";
            this.folderDeleteTopicsMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(159, 6);
            // 
            // folderExportTopicsMenuItem
            // 
            this.folderExportTopicsMenuItem.Name = "folderExportTopicsMenuItem";
            this.folderExportTopicsMenuItem.Size = new System.Drawing.Size(162, 22);
            this.folderExportTopicsMenuItem.Text = "Export Topics";
            this.folderExportTopicsMenuItem.ToolTipText = "Export the definition of the topics in the current path to file.";
            this.folderExportTopicsMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(159, 6);
            // 
            // expandSubTreeMenuItem10
            // 
            this.expandSubTreeMenuItem10.Name = "expandSubTreeMenuItem10";
            this.expandSubTreeMenuItem10.Size = new System.Drawing.Size(162, 22);
            this.expandSubTreeMenuItem10.Text = "Expand Subtree";
            this.expandSubTreeMenuItem10.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem10.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem10
            // 
            this.collapseSubTreeMenuItem10.Name = "collapseSubTreeMenuItem10";
            this.collapseSubTreeMenuItem10.Size = new System.Drawing.Size(162, 22);
            this.collapseSubTreeMenuItem10.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem10.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem10.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // relayFolderContextMenuStrip
            // 
            this.relayFolderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandSubTreeMenuItem12,
            this.collapseSubTreeMenuItem12});
            this.relayFolderContextMenuStrip.Name = "createContextMenuStrip";
            this.relayFolderContextMenuStrip.Size = new System.Drawing.Size(163, 48);
            // 
            // expandSubTreeMenuItem12
            // 
            this.expandSubTreeMenuItem12.Name = "expandSubTreeMenuItem12";
            this.expandSubTreeMenuItem12.Size = new System.Drawing.Size(162, 22);
            this.expandSubTreeMenuItem12.Text = "Expand Subtree";
            this.expandSubTreeMenuItem12.ToolTipText = "Expand the subtree.";
            this.expandSubTreeMenuItem12.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseSubTreeMenuItem12
            // 
            this.collapseSubTreeMenuItem12.Name = "collapseSubTreeMenuItem12";
            this.collapseSubTreeMenuItem12.Size = new System.Drawing.Size(162, 22);
            this.collapseSubTreeMenuItem12.Text = "Collapse Subtree";
            this.collapseSubTreeMenuItem12.ToolTipText = "Collapse the subtree.";
            this.collapseSubTreeMenuItem12.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // notificationHubContextMenuStrip
            // 
            this.notificationHubContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteNotificationHubMenuItem,
            this.refreshNotificationHubMenuItem,
            this.toolStripSeparator43,
            this.exportNotificationHubMenuItem,
            this.toolStripSeparator44,
            this.copyUrlNotificationHubMenuItem,
            this.toolStripSeparator26,
            this.getRegistrationsNotificationHubMenuItem});
            this.notificationHubContextMenuStrip.Name = "nodeContextMenuStrip";
            this.notificationHubContextMenuStrip.Size = new System.Drawing.Size(225, 132);
            // 
            // deleteNotificationHubMenuItem
            // 
            this.deleteNotificationHubMenuItem.Name = "deleteNotificationHubMenuItem";
            this.deleteNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            this.deleteNotificationHubMenuItem.Text = "Delete Notification Hub";
            this.deleteNotificationHubMenuItem.ToolTipText = "Delete the current notification hub.";
            this.deleteNotificationHubMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // refreshNotificationHubMenuItem
            // 
            this.refreshNotificationHubMenuItem.Name = "refreshNotificationHubMenuItem";
            this.refreshNotificationHubMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            this.refreshNotificationHubMenuItem.Text = "Refresh Notification Hub";
            this.refreshNotificationHubMenuItem.ToolTipText = "Refresh the current notification hub.";
            this.refreshNotificationHubMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator43
            // 
            this.toolStripSeparator43.Name = "toolStripSeparator43";
            this.toolStripSeparator43.Size = new System.Drawing.Size(221, 6);
            // 
            // exportNotificationHubMenuItem
            // 
            this.exportNotificationHubMenuItem.Name = "exportNotificationHubMenuItem";
            this.exportNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            this.exportNotificationHubMenuItem.Text = "Export Notification Hub";
            this.exportNotificationHubMenuItem.ToolTipText = "Export notification hub definition to file.";
            this.exportNotificationHubMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // toolStripSeparator44
            // 
            this.toolStripSeparator44.Name = "toolStripSeparator44";
            this.toolStripSeparator44.Size = new System.Drawing.Size(221, 6);
            // 
            // copyUrlNotificationHubMenuItem
            // 
            this.copyUrlNotificationHubMenuItem.Name = "copyUrlNotificationHubMenuItem";
            this.copyUrlNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            this.copyUrlNotificationHubMenuItem.Text = "Copy Notification Hub Url";
            this.copyUrlNotificationHubMenuItem.ToolTipText = "Copy the notification hub url to the clipboard.";
            this.copyUrlNotificationHubMenuItem.Click += new System.EventHandler(this.copyEntityUrl_Click);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(221, 6);
            // 
            // getRegistrationsNotificationHubMenuItem
            // 
            this.getRegistrationsNotificationHubMenuItem.Name = "getRegistrationsNotificationHubMenuItem";
            this.getRegistrationsNotificationHubMenuItem.Size = new System.Drawing.Size(224, 22);
            this.getRegistrationsNotificationHubMenuItem.Text = "Get Registrations";
            this.getRegistrationsNotificationHubMenuItem.ToolTipText = "Gets the registrations of the current notification hub.";
            this.getRegistrationsNotificationHubMenuItem.Click += new System.EventHandler(this.getRegistrationsNotificationHubMenuItem_Click);
            // 
            // notificationHubsContextMenuStrip
            // 
            this.notificationHubsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNotificationHubMenuItem,
            this.deleteNotificationHubsMenuItem,
            this.toolStripSeparator17,
            this.refreshNotificationHubsMenuItem,
            this.toolStripSeparator42,
            this.exportNotificationHubsMenuItem,
            this.toolStripSeparator45,
            this.expandNotificationHubMenuItem,
            this.collapseNotificationHubMenuItem});
            this.notificationHubsContextMenuStrip.Name = "relayServicesContextMenuStrip";
            this.notificationHubsContextMenuStrip.Size = new System.Drawing.Size(230, 154);
            // 
            // createNotificationHubMenuItem
            // 
            this.createNotificationHubMenuItem.Name = "createNotificationHubMenuItem";
            this.createNotificationHubMenuItem.Size = new System.Drawing.Size(229, 22);
            this.createNotificationHubMenuItem.Text = "Create Notification Hub";
            this.createNotificationHubMenuItem.ToolTipText = "Create a new notification hub.";
            this.createNotificationHubMenuItem.Click += new System.EventHandler(this.createEntity_Click);
            // 
            // deleteNotificationHubsMenuItem
            // 
            this.deleteNotificationHubsMenuItem.Name = "deleteNotificationHubsMenuItem";
            this.deleteNotificationHubsMenuItem.Size = new System.Drawing.Size(229, 22);
            this.deleteNotificationHubsMenuItem.Text = "Delete Notification Hubs";
            this.deleteNotificationHubsMenuItem.ToolTipText = "Deletes all notification hubs in the current namespace.";
            this.deleteNotificationHubsMenuItem.Click += new System.EventHandler(this.deleteEntity_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(226, 6);
            // 
            // refreshNotificationHubsMenuItem
            // 
            this.refreshNotificationHubsMenuItem.Name = "refreshNotificationHubsMenuItem";
            this.refreshNotificationHubsMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshNotificationHubsMenuItem.Size = new System.Drawing.Size(229, 22);
            this.refreshNotificationHubsMenuItem.Text = "Refresh Notification Hubs";
            this.refreshNotificationHubsMenuItem.ToolTipText = "Refresh all relay services in the current namespace.";
            this.refreshNotificationHubsMenuItem.Click += new System.EventHandler(this.refreshEntity_Click);
            // 
            // toolStripSeparator42
            // 
            this.toolStripSeparator42.Name = "toolStripSeparator42";
            this.toolStripSeparator42.Size = new System.Drawing.Size(226, 6);
            // 
            // exportNotificationHubsMenuItem
            // 
            this.exportNotificationHubsMenuItem.Name = "exportNotificationHubsMenuItem";
            this.exportNotificationHubsMenuItem.Size = new System.Drawing.Size(229, 22);
            this.exportNotificationHubsMenuItem.Text = "Export Notification Hubs";
            this.exportNotificationHubsMenuItem.ToolTipText = "Export notification hubs definition to file.";
            this.exportNotificationHubsMenuItem.Click += new System.EventHandler(this.exportEntity_Click);
            // 
            // toolStripSeparator45
            // 
            this.toolStripSeparator45.Name = "toolStripSeparator45";
            this.toolStripSeparator45.Size = new System.Drawing.Size(226, 6);
            // 
            // expandNotificationHubMenuItem
            // 
            this.expandNotificationHubMenuItem.Name = "expandNotificationHubMenuItem";
            this.expandNotificationHubMenuItem.Size = new System.Drawing.Size(229, 22);
            this.expandNotificationHubMenuItem.Text = "Expand Subtree";
            this.expandNotificationHubMenuItem.ToolTipText = "Expand the subtree.";
            this.expandNotificationHubMenuItem.Click += new System.EventHandler(this.expandEntity_Click);
            // 
            // collapseNotificationHubMenuItem
            // 
            this.collapseNotificationHubMenuItem.Name = "collapseNotificationHubMenuItem";
            this.collapseNotificationHubMenuItem.Size = new System.Drawing.Size(229, 22);
            this.collapseNotificationHubMenuItem.Text = "Collapse Subtree";
            this.collapseNotificationHubMenuItem.ToolTipText = "Collapse the subtree.";
            this.collapseNotificationHubMenuItem.Click += new System.EventHandler(this.collapseEntity_Click);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logoPictureBox.BackgroundImage = global::Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Properties.Resources.MicrosoftAzureWhiteLogo;
            this.logoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoPictureBox.Location = new System.Drawing.Point(1290, 13);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(110, 14);
            this.logoPictureBox.TabIndex = 23;
            this.logoPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1416, 841);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Bus Explorer 2.1.3.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelTreeView.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.panelLog.ResumeLayout(false);
            this.logContextMenuStrip.ResumeLayout(false);
            this.rootContextMenuStrip.ResumeLayout(false);
            this.queuesContextMenuStrip.ResumeLayout(false);
            this.ruleContextMenuStrip.ResumeLayout(false);
            this.rulesContextMenuStrip.ResumeLayout(false);
            this.subscriptionsContextMenuStrip.ResumeLayout(false);
            this.subscriptionContextMenuStrip.ResumeLayout(false);
            this.topicContextMenuStrip.ResumeLayout(false);
            this.queueContextMenuStrip.ResumeLayout(false);
            this.topicsContextMenuStrip.ResumeLayout(false);
            this.relayServicesContextMenuStrip.ResumeLayout(false);
            this.relayServiceContextMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.queueFolderContextMenuStrip.ResumeLayout(false);
            this.topicFolderContextMenuStrip.ResumeLayout(false);
            this.relayFolderContextMenuStrip.ResumeLayout(false);
            this.notificationHubContextMenuStrip.ResumeLayout(false);
            this.notificationHubsContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
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
        private System.Windows.Forms.TreeView serviceBusTreeView;
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
        private System.Windows.Forms.ToolStripMenuItem createTopicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTopicsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshTopicsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exportTopicsMenuItem;
        private System.Windows.Forms.ContextMenuStrip relayServicesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshRelayServicesMenuItem;
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
        private System.Windows.Forms.ContextMenuStrip relayServiceContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyRelayServiceUrlMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem queueReceiveAllMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queueReceiveTopMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queuePeekTopMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator queueReceiveToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem queueReceiveAllDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queuePeekTopDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripMenuItem subReceiveAllMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subReceiveTopMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subPeekTopMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator subReceiveToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem subReceiveAllDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subPeekTopDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
        private System.Windows.Forms.ToolStripMenuItem queueSendMessageMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
        private System.Windows.Forms.ToolStripMenuItem sendMessagesTopicMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem subReceiveTopDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queueReceiveTopDeadletterQueueMessagesMenuItem;
        private System.Windows.Forms.ToolStripSeparator metricsToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem metricsSDIMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metricsMDIMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem createQueueListenerMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
    }
}

