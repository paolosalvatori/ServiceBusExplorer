namespace ServiceBusExplorer.Controls
{
    partial class HandleQueueControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleQueueControl));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPurgeDeadletterQueueMessages = new System.Windows.Forms.Button();
            this.btnTransferDeadletterQueue = new System.Windows.Forms.Button();
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.btnCancelUpdate = new System.Windows.Forms.Button();
            this.btnCreateDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.grouperAutoDeleteOnIdle = new ServiceBusExplorer.Controls.Grouper();
            this.tsAutoDeleteOnIdle = new ServiceBusExplorer.Controls.TimeSpanControl();
            this.grouperQueueInformation = new ServiceBusExplorer.Controls.Grouper();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupergrouperDefaultMessageTimeToLive = new ServiceBusExplorer.Controls.Grouper();
            this.tsDefaultMessageTimeToLive = new ServiceBusExplorer.Controls.TimeSpanControl();
            this.grouperQueueSettings = new ServiceBusExplorer.Controls.Grouper();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.grouperQueueProperties = new ServiceBusExplorer.Controls.Grouper();
            this.btnOpenForwardDeadLetteredMessagesToForm = new System.Windows.Forms.Button();
            this.lblForwardDeadLetteredMessagesTo = new System.Windows.Forms.Label();
            this.txtForwardDeadLetteredMessagesTo = new System.Windows.Forms.TextBox();
            this.lblMaxQueueSize = new System.Windows.Forms.Label();
            this.trackBarMaxQueueSize = new ServiceBusExplorer.Controls.CustomTrackBar();
            this.txtUserMetadata = new System.Windows.Forms.TextBox();
            this.lblUserMetadata = new System.Windows.Forms.Label();
            this.lblMaxQueueSizeInGB = new System.Windows.Forms.Label();
            this.btnOpenDescriptionForm = new System.Windows.Forms.Button();
            this.btnOpenForwardToForm = new System.Windows.Forms.Button();
            this.lblForwardTo = new System.Windows.Forms.Label();
            this.txtForwardTo = new System.Windows.Forms.TextBox();
            this.lblMaxDeliveryCount = new System.Windows.Forms.Label();
            this.txtMaxDeliveryCount = new ServiceBusExplorer.Controls.NumericTextBox();
            this.grouperLockDuration = new ServiceBusExplorer.Controls.Grouper();
            this.tsLockDuration = new ServiceBusExplorer.Controls.TimeSpanControl();
            this.grouperDuplicateDetectionHistoryTimeWindow = new ServiceBusExplorer.Controls.Grouper();
            this.tsDuplicateDetectionHistoryTimeWindow = new ServiceBusExplorer.Controls.TimeSpanControl();
            this.grouperPath = new ServiceBusExplorer.Controls.Grouper();
            this.lblRelativeURI = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.tabPageAuthorization = new System.Windows.Forms.TabPage();
            this.grouperAuthorizationRuleList = new ServiceBusExplorer.Controls.Grouper();
            this.authorizationRulesDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.messagesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.messageMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageList = new ServiceBusExplorer.Controls.Grouper();
            this.pictFindMessages = new System.Windows.Forms.PictureBox();
            this.pictFindMessagesByDate = new System.Windows.Forms.PictureBox();
            this.messagesDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperMessageText = new ServiceBusExplorer.Controls.Grouper();
            this.txtMessageText = new FastColoredTextBoxNS.FastColoredTextBox();
            this.messagePropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageSystemProperties = new ServiceBusExplorer.Controls.Grouper();
            this.messagePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.grouperMessageCustomProperties = new ServiceBusExplorer.Controls.Grouper();
            this.messageCustomPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPageDeadletter = new System.Windows.Forms.TabPage();
            this.deadletterSplitContainer = new System.Windows.Forms.SplitContainer();
            this.deadletterMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperDeadletterList = new ServiceBusExplorer.Controls.Grouper();
            this.pictFindDeadletter = new System.Windows.Forms.PictureBox();
            this.pictFindDeadletterByDate = new System.Windows.Forms.PictureBox();
            this.deadletterDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperDeadletterText = new ServiceBusExplorer.Controls.Grouper();
            this.txtDeadletterText = new FastColoredTextBoxNS.FastColoredTextBox();
            this.deadletterPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperDeadletterSystemProperties = new ServiceBusExplorer.Controls.Grouper();
            this.deadletterPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.grouperDeadletterCustomProperties = new ServiceBusExplorer.Controls.Grouper();
            this.deadletterCustomPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPageTransferDeadletter = new System.Windows.Forms.TabPage();
            this.transferDeadletterSplitContainer = new System.Windows.Forms.SplitContainer();
            this.transferMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperTransferDeadletterList = new ServiceBusExplorer.Controls.Grouper();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.transferDeadletterDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperTransferDeadletterText = new ServiceBusExplorer.Controls.Grouper();
            this.txtTransferDeadletterText = new FastColoredTextBoxNS.FastColoredTextBox();
            this.transferDeadletterPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperTransferDeadletterSystemProperties = new ServiceBusExplorer.Controls.Grouper();
            this.transferDeadletterPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.grouperTransferDeadletterCustomProperties = new ServiceBusExplorer.Controls.Grouper();
            this.transferDeadletterCustomPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPageSessions = new System.Windows.Forms.TabPage();
            this.sessionListStateSplitContainer = new System.Windows.Forms.SplitContainer();
            this.sessionMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperSessionList = new ServiceBusExplorer.Controls.Grouper();
            this.sessionsDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperSessionState = new ServiceBusExplorer.Controls.Grouper();
            this.txtSessionState = new System.Windows.Forms.TextBox();
            this.grouperSessionProperties = new ServiceBusExplorer.Controls.Grouper();
            this.sessionPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.btnSessions = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.btnDeadletter = new System.Windows.Forms.Button();
            this.messagesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.repairAndResubmitMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedMessageBodyAsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedMessagesBodyAsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deadletterContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.repairAndResubmitDeadletterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedDeadletteredMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedDeadletteredMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sessionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.deadletterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.authorizationRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnPurgeMessages = new System.Windows.Forms.Button();
            this.transferDeadletterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.transferDeadletterContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.repairAndResubmitTransferDeadletterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitSelectedTransferDeadletterInBatchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedTransferDeadletteredMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedTransferDeadletteredMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl.SuspendLayout();
            this.tabPageDescription.SuspendLayout();
            this.grouperAutoDeleteOnIdle.SuspendLayout();
            this.grouperQueueInformation.SuspendLayout();
            this.groupergrouperDefaultMessageTimeToLive.SuspendLayout();
            this.grouperQueueSettings.SuspendLayout();
            this.grouperQueueProperties.SuspendLayout();
            this.grouperLockDuration.SuspendLayout();
            this.grouperDuplicateDetectionHistoryTimeWindow.SuspendLayout();
            this.grouperPath.SuspendLayout();
            this.tabPageAuthorization.SuspendLayout();
            this.grouperAuthorizationRuleList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).BeginInit();
            this.tabPageMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesSplitContainer)).BeginInit();
            this.messagesSplitContainer.Panel1.SuspendLayout();
            this.messagesSplitContainer.Panel2.SuspendLayout();
            this.messagesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageMainSplitContainer)).BeginInit();
            this.messageMainSplitContainer.Panel1.SuspendLayout();
            this.messageMainSplitContainer.Panel2.SuspendLayout();
            this.messageMainSplitContainer.SuspendLayout();
            this.grouperMessageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindMessagesByDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesDataGridView)).BeginInit();
            this.grouperMessageText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessageText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagePropertiesSplitContainer)).BeginInit();
            this.messagePropertiesSplitContainer.Panel1.SuspendLayout();
            this.messagePropertiesSplitContainer.Panel2.SuspendLayout();
            this.messagePropertiesSplitContainer.SuspendLayout();
            this.grouperMessageSystemProperties.SuspendLayout();
            this.grouperMessageCustomProperties.SuspendLayout();
            this.tabPageDeadletter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterSplitContainer)).BeginInit();
            this.deadletterSplitContainer.Panel1.SuspendLayout();
            this.deadletterSplitContainer.Panel2.SuspendLayout();
            this.deadletterSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterMainSplitContainer)).BeginInit();
            this.deadletterMainSplitContainer.Panel1.SuspendLayout();
            this.deadletterMainSplitContainer.Panel2.SuspendLayout();
            this.deadletterMainSplitContainer.SuspendLayout();
            this.grouperDeadletterList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindDeadletter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindDeadletterByDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterDataGridView)).BeginInit();
            this.grouperDeadletterText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeadletterText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterPropertiesSplitContainer)).BeginInit();
            this.deadletterPropertiesSplitContainer.Panel1.SuspendLayout();
            this.deadletterPropertiesSplitContainer.Panel2.SuspendLayout();
            this.deadletterPropertiesSplitContainer.SuspendLayout();
            this.grouperDeadletterSystemProperties.SuspendLayout();
            this.grouperDeadletterCustomProperties.SuspendLayout();
            this.tabPageTransferDeadletter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transferDeadletterSplitContainer)).BeginInit();
            this.transferDeadletterSplitContainer.Panel1.SuspendLayout();
            this.transferDeadletterSplitContainer.Panel2.SuspendLayout();
            this.transferDeadletterSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transferMainSplitContainer)).BeginInit();
            this.transferMainSplitContainer.Panel1.SuspendLayout();
            this.transferMainSplitContainer.Panel2.SuspendLayout();
            this.transferMainSplitContainer.SuspendLayout();
            this.grouperTransferDeadletterList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferDeadletterDataGridView)).BeginInit();
            this.grouperTransferDeadletterText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTransferDeadletterText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferDeadletterPropertiesSplitContainer)).BeginInit();
            this.transferDeadletterPropertiesSplitContainer.Panel1.SuspendLayout();
            this.transferDeadletterPropertiesSplitContainer.Panel2.SuspendLayout();
            this.transferDeadletterPropertiesSplitContainer.SuspendLayout();
            this.grouperTransferDeadletterSystemProperties.SuspendLayout();
            this.grouperTransferDeadletterCustomProperties.SuspendLayout();
            this.tabPageSessions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionListStateSplitContainer)).BeginInit();
            this.sessionListStateSplitContainer.Panel1.SuspendLayout();
            this.sessionListStateSplitContainer.Panel2.SuspendLayout();
            this.sessionListStateSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionMainSplitContainer)).BeginInit();
            this.sessionMainSplitContainer.Panel1.SuspendLayout();
            this.sessionMainSplitContainer.Panel2.SuspendLayout();
            this.sessionMainSplitContainer.SuspendLayout();
            this.grouperSessionList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsDataGridView)).BeginInit();
            this.grouperSessionState.SuspendLayout();
            this.grouperSessionProperties.SuspendLayout();
            this.messagesContextMenuStrip.SuspendLayout();
            this.deadletterContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferDeadletterBindingSource)).BeginInit();
            this.transferDeadletterContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPurgeDeadletterQueueMessages
            // 
            this.btnPurgeDeadletterQueueMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPurgeDeadletterQueueMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnPurgeDeadletterQueueMessages.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnPurgeDeadletterQueueMessages.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnPurgeDeadletterQueueMessages.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnPurgeDeadletterQueueMessages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurgeDeadletterQueueMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPurgeDeadletterQueueMessages.Location = new System.Drawing.Point(279, 504);
            this.btnPurgeDeadletterQueueMessages.Name = "btnPurgeDeadletterQueueMessages";
            this.btnPurgeDeadletterQueueMessages.Size = new System.Drawing.Size(72, 24);
            this.btnPurgeDeadletterQueueMessages.TabIndex = 2;
            this.btnPurgeDeadletterQueueMessages.Text = "Purge DLQ";
            this.toolTip.SetToolTip(this.btnPurgeDeadletterQueueMessages, "Purge Dead-letter Queue");
            this.btnPurgeDeadletterQueueMessages.UseVisualStyleBackColor = false;
            this.btnPurgeDeadletterQueueMessages.Click += new System.EventHandler(this.btnPurgeDeadletterQueueMessages_Click);
            this.btnPurgeDeadletterQueueMessages.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnPurgeDeadletterQueueMessages.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnTransferDeadletterQueue
            // 
            this.btnTransferDeadletterQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransferDeadletterQueue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnTransferDeadletterQueue.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnTransferDeadletterQueue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnTransferDeadletterQueue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnTransferDeadletterQueue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferDeadletterQueue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTransferDeadletterQueue.Location = new System.Drawing.Point(597, 504);
            this.btnTransferDeadletterQueue.Name = "btnTransferDeadletterQueue";
            this.btnTransferDeadletterQueue.Size = new System.Drawing.Size(72, 24);
            this.btnTransferDeadletterQueue.TabIndex = 6;
            this.btnTransferDeadletterQueue.Text = "Transf DLQ";
            this.toolTip.SetToolTip(this.btnTransferDeadletterQueue, "Transfer Dead-letter Queue");
            this.btnTransferDeadletterQueue.UseVisualStyleBackColor = false;
            this.btnTransferDeadletterQueue.Click += new System.EventHandler(this.btnTransferDlq_Click);
            // 
            // btnChangeStatus
            // 
            this.btnChangeStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangeStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnChangeStatus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnChangeStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnChangeStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnChangeStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnChangeStatus.Location = new System.Drawing.Point(756, 504);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(72, 24);
            this.btnChangeStatus.TabIndex = 8;
            this.btnChangeStatus.Text = "Disable";
            this.btnChangeStatus.UseVisualStyleBackColor = false;
            this.btnChangeStatus.Click += new System.EventHandler(this.btnChangeStatus_Click);
            this.btnChangeStatus.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnChangeStatus.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnCancelUpdate
            // 
            this.btnCancelUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancelUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancelUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancelUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancelUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancelUpdate.Location = new System.Drawing.Point(915, 504);
            this.btnCancelUpdate.Name = "btnCancelUpdate";
            this.btnCancelUpdate.Size = new System.Drawing.Size(72, 24);
            this.btnCancelUpdate.TabIndex = 10;
            this.btnCancelUpdate.Text = "Update";
            this.btnCancelUpdate.UseVisualStyleBackColor = false;
            this.btnCancelUpdate.Click += new System.EventHandler(this.btnCancelUpdate_Click);
            this.btnCancelUpdate.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancelUpdate.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnCreateDelete
            // 
            this.btnCreateDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCreateDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCreateDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCreateDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCreateDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateDelete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCreateDelete.Location = new System.Drawing.Point(836, 504);
            this.btnCreateDelete.Name = "btnCreateDelete";
            this.btnCreateDelete.Size = new System.Drawing.Size(72, 24);
            this.btnCreateDelete.TabIndex = 9;
            this.btnCreateDelete.Text = "Create";
            this.btnCreateDelete.UseVisualStyleBackColor = false;
            this.btnCreateDelete.Click += new System.EventHandler(this.btnCreateDelete_Click);
            this.btnCreateDelete.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCreateDelete.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefresh.Location = new System.Drawing.Point(676, 504);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 24);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnRefresh.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.tabPageDescription);
            this.mainTabControl.Controls.Add(this.tabPageAuthorization);
            this.mainTabControl.Controls.Add(this.tabPageMessages);
            this.mainTabControl.Controls.Add(this.tabPageDeadletter);
            this.mainTabControl.Controls.Add(this.tabPageTransferDeadletter);
            this.mainTabControl.Controls.Add(this.tabPageSessions);
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.ItemSize = new System.Drawing.Size(76, 18);
            this.mainTabControl.Location = new System.Drawing.Point(16, 16);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(976, 480);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mainTabControl_DrawItem);
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageDescription.Controls.Add(this.grouperAutoDeleteOnIdle);
            this.tabPageDescription.Controls.Add(this.grouperQueueInformation);
            this.tabPageDescription.Controls.Add(this.groupergrouperDefaultMessageTimeToLive);
            this.tabPageDescription.Controls.Add(this.grouperQueueSettings);
            this.tabPageDescription.Controls.Add(this.grouperQueueProperties);
            this.tabPageDescription.Controls.Add(this.grouperLockDuration);
            this.tabPageDescription.Controls.Add(this.grouperDuplicateDetectionHistoryTimeWindow);
            this.tabPageDescription.Controls.Add(this.grouperPath);
            this.tabPageDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDescription.Location = new System.Drawing.Point(4, 22);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(968, 454);
            this.tabPageDescription.TabIndex = 2;
            this.tabPageDescription.Text = "Description";
            // 
            // grouperAutoDeleteOnIdle
            // 
            this.grouperAutoDeleteOnIdle.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperAutoDeleteOnIdle.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperAutoDeleteOnIdle.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperAutoDeleteOnIdle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAutoDeleteOnIdle.BorderThickness = 1F;
            this.grouperAutoDeleteOnIdle.Controls.Add(this.tsAutoDeleteOnIdle);
            this.grouperAutoDeleteOnIdle.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAutoDeleteOnIdle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperAutoDeleteOnIdle.ForeColor = System.Drawing.Color.White;
            this.grouperAutoDeleteOnIdle.GroupImage = null;
            this.grouperAutoDeleteOnIdle.GroupTitle = "Auto Delete On Idle";
            this.grouperAutoDeleteOnIdle.Location = new System.Drawing.Point(328, 8);
            this.grouperAutoDeleteOnIdle.Name = "grouperAutoDeleteOnIdle";
            this.grouperAutoDeleteOnIdle.Padding = new System.Windows.Forms.Padding(20);
            this.grouperAutoDeleteOnIdle.PaintGroupBox = true;
            this.grouperAutoDeleteOnIdle.RoundCorners = 4;
            this.grouperAutoDeleteOnIdle.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAutoDeleteOnIdle.ShadowControl = false;
            this.grouperAutoDeleteOnIdle.ShadowThickness = 1;
            this.grouperAutoDeleteOnIdle.Size = new System.Drawing.Size(296, 80);
            this.grouperAutoDeleteOnIdle.TabIndex = 3;
            // 
            // tsAutoDeleteOnIdle
            // 
            this.tsAutoDeleteOnIdle.Location = new System.Drawing.Point(13, 25);
            this.tsAutoDeleteOnIdle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tsAutoDeleteOnIdle.Name = "tsAutoDeleteOnIdle";
            this.tsAutoDeleteOnIdle.Size = new System.Drawing.Size(273, 42);
            this.tsAutoDeleteOnIdle.TabIndex = 0;
            this.tsAutoDeleteOnIdle.TimeSpanValue = null;
            // 
            // grouperQueueInformation
            // 
            this.grouperQueueInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperQueueInformation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperQueueInformation.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperQueueInformation.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperQueueInformation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueInformation.BorderThickness = 1F;
            this.grouperQueueInformation.Controls.Add(this.propertyListView);
            this.grouperQueueInformation.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperQueueInformation.ForeColor = System.Drawing.Color.White;
            this.grouperQueueInformation.GroupImage = null;
            this.grouperQueueInformation.GroupTitle = "Queue Information";
            this.grouperQueueInformation.Location = new System.Drawing.Point(640, 8);
            this.grouperQueueInformation.Name = "grouperQueueInformation";
            this.grouperQueueInformation.Padding = new System.Windows.Forms.Padding(20);
            this.grouperQueueInformation.PaintGroupBox = true;
            this.grouperQueueInformation.RoundCorners = 4;
            this.grouperQueueInformation.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperQueueInformation.ShadowControl = false;
            this.grouperQueueInformation.ShadowThickness = 1;
            this.grouperQueueInformation.Size = new System.Drawing.Size(312, 432);
            this.grouperQueueInformation.TabIndex = 7;
            // 
            // propertyListView
            // 
            this.propertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.valueColumnHeader});
            this.propertyListView.HideSelection = false;
            this.propertyListView.Location = new System.Drawing.Point(16, 32);
            this.propertyListView.Name = "propertyListView";
            this.propertyListView.OwnerDraw = true;
            this.propertyListView.Size = new System.Drawing.Size(280, 384);
            this.propertyListView.TabIndex = 0;
            this.propertyListView.UseCompatibleStateImageBehavior = false;
            this.propertyListView.View = System.Windows.Forms.View.Details;
            this.propertyListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.propertyListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.propertyListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.propertyListView.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 160;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Value";
            this.valueColumnHeader.Width = 115;
            // 
            // groupergrouperDefaultMessageTimeToLive
            // 
            this.groupergrouperDefaultMessageTimeToLive.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupergrouperDefaultMessageTimeToLive.BackgroundGradientColor = System.Drawing.Color.White;
            this.groupergrouperDefaultMessageTimeToLive.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.groupergrouperDefaultMessageTimeToLive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.groupergrouperDefaultMessageTimeToLive.BorderThickness = 1F;
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.tsDefaultMessageTimeToLive);
            this.groupergrouperDefaultMessageTimeToLive.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.groupergrouperDefaultMessageTimeToLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupergrouperDefaultMessageTimeToLive.ForeColor = System.Drawing.Color.White;
            this.groupergrouperDefaultMessageTimeToLive.GroupImage = null;
            this.groupergrouperDefaultMessageTimeToLive.GroupTitle = "Default Message Time To Live";
            this.groupergrouperDefaultMessageTimeToLive.Location = new System.Drawing.Point(328, 96);
            this.groupergrouperDefaultMessageTimeToLive.Name = "groupergrouperDefaultMessageTimeToLive";
            this.groupergrouperDefaultMessageTimeToLive.Padding = new System.Windows.Forms.Padding(20);
            this.groupergrouperDefaultMessageTimeToLive.PaintGroupBox = true;
            this.groupergrouperDefaultMessageTimeToLive.RoundCorners = 4;
            this.groupergrouperDefaultMessageTimeToLive.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupergrouperDefaultMessageTimeToLive.ShadowControl = false;
            this.groupergrouperDefaultMessageTimeToLive.ShadowThickness = 1;
            this.groupergrouperDefaultMessageTimeToLive.Size = new System.Drawing.Size(296, 80);
            this.groupergrouperDefaultMessageTimeToLive.TabIndex = 4;
            // 
            // tsDefaultMessageTimeToLive
            // 
            this.tsDefaultMessageTimeToLive.Location = new System.Drawing.Point(13, 25);
            this.tsDefaultMessageTimeToLive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tsDefaultMessageTimeToLive.Name = "tsDefaultMessageTimeToLive";
            this.tsDefaultMessageTimeToLive.Size = new System.Drawing.Size(273, 42);
            this.tsDefaultMessageTimeToLive.TabIndex = 0;
            this.tsDefaultMessageTimeToLive.TimeSpanValue = null;
            // 
            // grouperQueueSettings
            // 
            this.grouperQueueSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperQueueSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperQueueSettings.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperQueueSettings.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperQueueSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueSettings.BorderThickness = 1F;
            this.grouperQueueSettings.Controls.Add(this.checkedListBox);
            this.grouperQueueSettings.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperQueueSettings.ForeColor = System.Drawing.Color.White;
            this.grouperQueueSettings.GroupImage = null;
            this.grouperQueueSettings.GroupTitle = "Queue Settings";
            this.grouperQueueSettings.Location = new System.Drawing.Point(328, 272);
            this.grouperQueueSettings.Name = "grouperQueueSettings";
            this.grouperQueueSettings.Padding = new System.Windows.Forms.Padding(20);
            this.grouperQueueSettings.PaintGroupBox = true;
            this.grouperQueueSettings.RoundCorners = 4;
            this.grouperQueueSettings.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperQueueSettings.ShadowControl = false;
            this.grouperQueueSettings.ShadowThickness = 1;
            this.grouperQueueSettings.Size = new System.Drawing.Size(296, 168);
            this.grouperQueueSettings.TabIndex = 6;
            // 
            // checkedListBox
            // 
            this.checkedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(16, 32);
            this.checkedListBox.Margin = new System.Windows.Forms.Padding(8);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(264, 109);
            this.checkedListBox.TabIndex = 0;
            this.checkedListBox.ThreeDCheckBoxes = true;
            this.checkedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_ItemCheck);
            // 
            // grouperQueueProperties
            // 
            this.grouperQueueProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperQueueProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperQueueProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperQueueProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperQueueProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueProperties.BorderThickness = 1F;
            this.grouperQueueProperties.Controls.Add(this.btnOpenForwardDeadLetteredMessagesToForm);
            this.grouperQueueProperties.Controls.Add(this.lblForwardDeadLetteredMessagesTo);
            this.grouperQueueProperties.Controls.Add(this.txtForwardDeadLetteredMessagesTo);
            this.grouperQueueProperties.Controls.Add(this.lblMaxQueueSize);
            this.grouperQueueProperties.Controls.Add(this.trackBarMaxQueueSize);
            this.grouperQueueProperties.Controls.Add(this.txtUserMetadata);
            this.grouperQueueProperties.Controls.Add(this.lblUserMetadata);
            this.grouperQueueProperties.Controls.Add(this.lblMaxQueueSizeInGB);
            this.grouperQueueProperties.Controls.Add(this.btnOpenDescriptionForm);
            this.grouperQueueProperties.Controls.Add(this.btnOpenForwardToForm);
            this.grouperQueueProperties.Controls.Add(this.lblForwardTo);
            this.grouperQueueProperties.Controls.Add(this.txtForwardTo);
            this.grouperQueueProperties.Controls.Add(this.lblMaxDeliveryCount);
            this.grouperQueueProperties.Controls.Add(this.txtMaxDeliveryCount);
            this.grouperQueueProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperQueueProperties.ForeColor = System.Drawing.Color.White;
            this.grouperQueueProperties.GroupImage = null;
            this.grouperQueueProperties.GroupTitle = "Queue Properties";
            this.grouperQueueProperties.Location = new System.Drawing.Point(16, 184);
            this.grouperQueueProperties.Name = "grouperQueueProperties";
            this.grouperQueueProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperQueueProperties.PaintGroupBox = true;
            this.grouperQueueProperties.RoundCorners = 4;
            this.grouperQueueProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperQueueProperties.ShadowControl = false;
            this.grouperQueueProperties.ShadowThickness = 1;
            this.grouperQueueProperties.Size = new System.Drawing.Size(296, 256);
            this.grouperQueueProperties.TabIndex = 2;
            // 
            // btnOpenForwardDeadLetteredMessagesToForm
            // 
            this.btnOpenForwardDeadLetteredMessagesToForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenForwardDeadLetteredMessagesToForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenForwardDeadLetteredMessagesToForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenForwardDeadLetteredMessagesToForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenForwardDeadLetteredMessagesToForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenForwardDeadLetteredMessagesToForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenForwardDeadLetteredMessagesToForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenForwardDeadLetteredMessagesToForm.Location = new System.Drawing.Point(256, 224);
            this.btnOpenForwardDeadLetteredMessagesToForm.Name = "btnOpenForwardDeadLetteredMessagesToForm";
            this.btnOpenForwardDeadLetteredMessagesToForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenForwardDeadLetteredMessagesToForm.TabIndex = 13;
            this.btnOpenForwardDeadLetteredMessagesToForm.Text = "...";
            this.btnOpenForwardDeadLetteredMessagesToForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenForwardDeadLetteredMessagesToForm.UseVisualStyleBackColor = false;
            this.btnOpenForwardDeadLetteredMessagesToForm.Click += new System.EventHandler(this.btnOpenForwardDeadLetteredMessagesToForm_Click);
            // 
            // lblForwardDeadLetteredMessagesTo
            // 
            this.lblForwardDeadLetteredMessagesTo.AutoSize = true;
            this.lblForwardDeadLetteredMessagesTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblForwardDeadLetteredMessagesTo.Location = new System.Drawing.Point(16, 208);
            this.lblForwardDeadLetteredMessagesTo.Name = "lblForwardDeadLetteredMessagesTo";
            this.lblForwardDeadLetteredMessagesTo.Size = new System.Drawing.Size(182, 13);
            this.lblForwardDeadLetteredMessagesTo.TabIndex = 11;
            this.lblForwardDeadLetteredMessagesTo.Text = "Forward Dead-lettered Messages To:";
            // 
            // txtForwardDeadLetteredMessagesTo
            // 
            this.txtForwardDeadLetteredMessagesTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtForwardDeadLetteredMessagesTo.BackColor = System.Drawing.SystemColors.Window;
            this.txtForwardDeadLetteredMessagesTo.Location = new System.Drawing.Point(16, 224);
            this.txtForwardDeadLetteredMessagesTo.Name = "txtForwardDeadLetteredMessagesTo";
            this.txtForwardDeadLetteredMessagesTo.Size = new System.Drawing.Size(232, 20);
            this.txtForwardDeadLetteredMessagesTo.TabIndex = 12;
            // 
            // lblMaxQueueSize
            // 
            this.lblMaxQueueSize.AutoSize = true;
            this.lblMaxQueueSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxQueueSize.Location = new System.Drawing.Point(16, 28);
            this.lblMaxQueueSize.Name = "lblMaxQueueSize";
            this.lblMaxQueueSize.Size = new System.Drawing.Size(118, 13);
            this.lblMaxQueueSize.TabIndex = 0;
            this.lblMaxQueueSize.Text = "Max Queue Size In GB:";
            // 
            // trackBarMaxQueueSize
            // 
            this.trackBarMaxQueueSize.BackColor = System.Drawing.Color.Transparent;
            this.trackBarMaxQueueSize.BorderColor = System.Drawing.Color.Black;
            this.trackBarMaxQueueSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarMaxQueueSize.ForeColor = System.Drawing.Color.Black;
            this.trackBarMaxQueueSize.IndentHeight = 6;
            this.trackBarMaxQueueSize.LargeChange = 1;
            this.trackBarMaxQueueSize.Location = new System.Drawing.Point(16, 40);
            this.trackBarMaxQueueSize.Maximum = 10;
            this.trackBarMaxQueueSize.Minimum = 1;
            this.trackBarMaxQueueSize.Name = "trackBarMaxQueueSize";
            this.trackBarMaxQueueSize.Size = new System.Drawing.Size(232, 29);
            this.trackBarMaxQueueSize.TabIndex = 1;
            this.trackBarMaxQueueSize.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMaxQueueSize.TickColor = System.Drawing.Color.Black;
            this.trackBarMaxQueueSize.TickHeight = 4;
            this.trackBarMaxQueueSize.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarMaxQueueSize.TrackerSize = new System.Drawing.Size(12, 12);
            this.trackBarMaxQueueSize.TrackLineBrushStyle = ServiceBusExplorer.Controls.BrushStyle.Solid;
            this.trackBarMaxQueueSize.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarMaxQueueSize.TrackLineHeight = 1;
            this.trackBarMaxQueueSize.Value = 1;
            this.trackBarMaxQueueSize.ValueChanged += new ServiceBusExplorer.Controls.ValueChangedHandler(this.trackBarMaxQueueSize_ValueChanged);
            // 
            // txtUserMetadata
            // 
            this.txtUserMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserMetadata.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserMetadata.Location = new System.Drawing.Point(16, 136);
            this.txtUserMetadata.Name = "txtUserMetadata";
            this.txtUserMetadata.Size = new System.Drawing.Size(232, 20);
            this.txtUserMetadata.TabIndex = 6;
            // 
            // lblUserMetadata
            // 
            this.lblUserMetadata.AutoSize = true;
            this.lblUserMetadata.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserMetadata.Location = new System.Drawing.Point(16, 120);
            this.lblUserMetadata.Name = "lblUserMetadata";
            this.lblUserMetadata.Size = new System.Drawing.Size(88, 13);
            this.lblUserMetadata.TabIndex = 5;
            this.lblUserMetadata.Text = "User Description:";
            // 
            // lblMaxQueueSizeInGB
            // 
            this.lblMaxQueueSizeInGB.AutoSize = true;
            this.lblMaxQueueSizeInGB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxQueueSizeInGB.Location = new System.Drawing.Point(252, 48);
            this.lblMaxQueueSizeInGB.Name = "lblMaxQueueSizeInGB";
            this.lblMaxQueueSizeInGB.Size = new System.Drawing.Size(31, 13);
            this.lblMaxQueueSizeInGB.TabIndex = 2;
            this.lblMaxQueueSizeInGB.Text = "1 GB";
            // 
            // btnOpenDescriptionForm
            // 
            this.btnOpenDescriptionForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDescriptionForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenDescriptionForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenDescriptionForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenDescriptionForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenDescriptionForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenDescriptionForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenDescriptionForm.Location = new System.Drawing.Point(256, 136);
            this.btnOpenDescriptionForm.Name = "btnOpenDescriptionForm";
            this.btnOpenDescriptionForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenDescriptionForm.TabIndex = 7;
            this.btnOpenDescriptionForm.Text = "...";
            this.btnOpenDescriptionForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenDescriptionForm.UseVisualStyleBackColor = false;
            this.btnOpenDescriptionForm.Click += new System.EventHandler(this.btnOpenUserMetadataForm_Click);
            // 
            // btnOpenForwardToForm
            // 
            this.btnOpenForwardToForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenForwardToForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenForwardToForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenForwardToForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenForwardToForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenForwardToForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenForwardToForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenForwardToForm.Location = new System.Drawing.Point(256, 180);
            this.btnOpenForwardToForm.Name = "btnOpenForwardToForm";
            this.btnOpenForwardToForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenForwardToForm.TabIndex = 10;
            this.btnOpenForwardToForm.Text = "...";
            this.btnOpenForwardToForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenForwardToForm.UseVisualStyleBackColor = false;
            this.btnOpenForwardToForm.Click += new System.EventHandler(this.btnOpenForwardToForm_Click);
            // 
            // lblForwardTo
            // 
            this.lblForwardTo.AutoSize = true;
            this.lblForwardTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblForwardTo.Location = new System.Drawing.Point(16, 164);
            this.lblForwardTo.Name = "lblForwardTo";
            this.lblForwardTo.Size = new System.Drawing.Size(64, 13);
            this.lblForwardTo.TabIndex = 8;
            this.lblForwardTo.Text = "Forward To:";
            // 
            // txtForwardTo
            // 
            this.txtForwardTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtForwardTo.BackColor = System.Drawing.SystemColors.Window;
            this.txtForwardTo.Location = new System.Drawing.Point(16, 180);
            this.txtForwardTo.Name = "txtForwardTo";
            this.txtForwardTo.Size = new System.Drawing.Size(232, 20);
            this.txtForwardTo.TabIndex = 9;
            // 
            // lblMaxDeliveryCount
            // 
            this.lblMaxDeliveryCount.AutoSize = true;
            this.lblMaxDeliveryCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxDeliveryCount.Location = new System.Drawing.Point(16, 72);
            this.lblMaxDeliveryCount.Name = "lblMaxDeliveryCount";
            this.lblMaxDeliveryCount.Size = new System.Drawing.Size(102, 13);
            this.lblMaxDeliveryCount.TabIndex = 3;
            this.lblMaxDeliveryCount.Text = "Max Delivery Count:";
            // 
            // txtMaxDeliveryCount
            // 
            this.txtMaxDeliveryCount.AllowDecimal = false;
            this.txtMaxDeliveryCount.AllowNegative = false;
            this.txtMaxDeliveryCount.AllowSpace = false;
            this.txtMaxDeliveryCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxDeliveryCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaxDeliveryCount.Location = new System.Drawing.Point(16, 88);
            this.txtMaxDeliveryCount.Name = "txtMaxDeliveryCount";
            this.txtMaxDeliveryCount.Size = new System.Drawing.Size(232, 20);
            this.txtMaxDeliveryCount.TabIndex = 4;
            // 
            // grouperLockDuration
            // 
            this.grouperLockDuration.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperLockDuration.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperLockDuration.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperLockDuration.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperLockDuration.BorderThickness = 1F;
            this.grouperLockDuration.Controls.Add(this.tsLockDuration);
            this.grouperLockDuration.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperLockDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperLockDuration.ForeColor = System.Drawing.Color.White;
            this.grouperLockDuration.GroupImage = null;
            this.grouperLockDuration.GroupTitle = "Lock Duration";
            this.grouperLockDuration.Location = new System.Drawing.Point(328, 184);
            this.grouperLockDuration.Name = "grouperLockDuration";
            this.grouperLockDuration.Padding = new System.Windows.Forms.Padding(20);
            this.grouperLockDuration.PaintGroupBox = true;
            this.grouperLockDuration.RoundCorners = 4;
            this.grouperLockDuration.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperLockDuration.ShadowControl = false;
            this.grouperLockDuration.ShadowThickness = 1;
            this.grouperLockDuration.Size = new System.Drawing.Size(296, 80);
            this.grouperLockDuration.TabIndex = 5;
            // 
            // tsLockDuration
            // 
            this.tsLockDuration.Location = new System.Drawing.Point(13, 25);
            this.tsLockDuration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tsLockDuration.Name = "tsLockDuration";
            this.tsLockDuration.Size = new System.Drawing.Size(273, 42);
            this.tsLockDuration.TabIndex = 0;
            this.tsLockDuration.TimeSpanValue = null;
            // 
            // grouperDuplicateDetectionHistoryTimeWindow
            // 
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDuplicateDetectionHistoryTimeWindow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDuplicateDetectionHistoryTimeWindow.BorderThickness = 1F;
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.tsDuplicateDetectionHistoryTimeWindow);
            this.grouperDuplicateDetectionHistoryTimeWindow.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDuplicateDetectionHistoryTimeWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDuplicateDetectionHistoryTimeWindow.ForeColor = System.Drawing.Color.White;
            this.grouperDuplicateDetectionHistoryTimeWindow.GroupImage = null;
            this.grouperDuplicateDetectionHistoryTimeWindow.GroupTitle = "Duplicate Detection History Time Window";
            this.grouperDuplicateDetectionHistoryTimeWindow.Location = new System.Drawing.Point(16, 96);
            this.grouperDuplicateDetectionHistoryTimeWindow.Name = "grouperDuplicateDetectionHistoryTimeWindow";
            this.grouperDuplicateDetectionHistoryTimeWindow.Padding = new System.Windows.Forms.Padding(20);
            this.grouperDuplicateDetectionHistoryTimeWindow.PaintGroupBox = true;
            this.grouperDuplicateDetectionHistoryTimeWindow.RoundCorners = 4;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowControl = false;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowThickness = 1;
            this.grouperDuplicateDetectionHistoryTimeWindow.Size = new System.Drawing.Size(296, 80);
            this.grouperDuplicateDetectionHistoryTimeWindow.TabIndex = 1;
            // 
            // tsDuplicateDetectionHistoryTimeWindow
            // 
            this.tsDuplicateDetectionHistoryTimeWindow.Location = new System.Drawing.Point(13, 25);
            this.tsDuplicateDetectionHistoryTimeWindow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tsDuplicateDetectionHistoryTimeWindow.Name = "tsDuplicateDetectionHistoryTimeWindow";
            this.tsDuplicateDetectionHistoryTimeWindow.Size = new System.Drawing.Size(273, 42);
            this.tsDuplicateDetectionHistoryTimeWindow.TabIndex = 0;
            this.tsDuplicateDetectionHistoryTimeWindow.TimeSpanValue = null;
            // 
            // grouperPath
            // 
            this.grouperPath.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperPath.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperPath.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperPath.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPath.BorderThickness = 1F;
            this.grouperPath.Controls.Add(this.lblRelativeURI);
            this.grouperPath.Controls.Add(this.txtPath);
            this.grouperPath.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperPath.ForeColor = System.Drawing.Color.White;
            this.grouperPath.GroupImage = null;
            this.grouperPath.GroupTitle = "Path";
            this.grouperPath.Location = new System.Drawing.Point(16, 8);
            this.grouperPath.Name = "grouperPath";
            this.grouperPath.Padding = new System.Windows.Forms.Padding(20);
            this.grouperPath.PaintGroupBox = true;
            this.grouperPath.RoundCorners = 4;
            this.grouperPath.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperPath.ShadowControl = false;
            this.grouperPath.ShadowThickness = 1;
            this.grouperPath.Size = new System.Drawing.Size(296, 80);
            this.grouperPath.TabIndex = 0;
            // 
            // lblRelativeURI
            // 
            this.lblRelativeURI.AutoSize = true;
            this.lblRelativeURI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRelativeURI.Location = new System.Drawing.Point(16, 28);
            this.lblRelativeURI.Name = "lblRelativeURI";
            this.lblRelativeURI.Size = new System.Drawing.Size(71, 13);
            this.lblRelativeURI.TabIndex = 0;
            this.lblRelativeURI.Text = "Relative URI:";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPath.Location = new System.Drawing.Point(16, 44);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(264, 20);
            this.txtPath.TabIndex = 1;
            // 
            // tabPageAuthorization
            // 
            this.tabPageAuthorization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageAuthorization.Controls.Add(this.grouperAuthorizationRuleList);
            this.tabPageAuthorization.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageAuthorization.Location = new System.Drawing.Point(4, 22);
            this.tabPageAuthorization.Name = "tabPageAuthorization";
            this.tabPageAuthorization.Size = new System.Drawing.Size(968, 454);
            this.tabPageAuthorization.TabIndex = 8;
            this.tabPageAuthorization.Text = "Authorization Rules";
            // 
            // grouperAuthorizationRuleList
            // 
            this.grouperAuthorizationRuleList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperAuthorizationRuleList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperAuthorizationRuleList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperAuthorizationRuleList.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperAuthorizationRuleList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAuthorizationRuleList.BorderThickness = 1F;
            this.grouperAuthorizationRuleList.Controls.Add(this.authorizationRulesDataGridView);
            this.grouperAuthorizationRuleList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAuthorizationRuleList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperAuthorizationRuleList.ForeColor = System.Drawing.Color.White;
            this.grouperAuthorizationRuleList.GroupImage = null;
            this.grouperAuthorizationRuleList.GroupTitle = "Authorization Rule List";
            this.grouperAuthorizationRuleList.Location = new System.Drawing.Point(16, 8);
            this.grouperAuthorizationRuleList.Name = "grouperAuthorizationRuleList";
            this.grouperAuthorizationRuleList.Padding = new System.Windows.Forms.Padding(20);
            this.grouperAuthorizationRuleList.PaintGroupBox = true;
            this.grouperAuthorizationRuleList.RoundCorners = 4;
            this.grouperAuthorizationRuleList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAuthorizationRuleList.ShadowControl = false;
            this.grouperAuthorizationRuleList.ShadowThickness = 1;
            this.grouperAuthorizationRuleList.Size = new System.Drawing.Size(936, 432);
            this.grouperAuthorizationRuleList.TabIndex = 20;
            this.grouperAuthorizationRuleList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperAuthorizationRuleList_CustomPaint);
            // 
            // authorizationRulesDataGridView
            // 
            this.authorizationRulesDataGridView.AllowUserToOrderColumns = true;
            this.authorizationRulesDataGridView.AllowUserToResizeRows = false;
            this.authorizationRulesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorizationRulesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.authorizationRulesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.authorizationRulesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.authorizationRulesDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.authorizationRulesDataGridView.Location = new System.Drawing.Point(16, 32);
            this.authorizationRulesDataGridView.MultiSelect = false;
            this.authorizationRulesDataGridView.Name = "authorizationRulesDataGridView";
            this.authorizationRulesDataGridView.RowHeadersWidth = 24;
            this.authorizationRulesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.authorizationRulesDataGridView.ShowCellErrors = false;
            this.authorizationRulesDataGridView.ShowRowErrors = false;
            this.authorizationRulesDataGridView.Size = new System.Drawing.Size(904, 385);
            this.authorizationRulesDataGridView.TabIndex = 0;
            this.authorizationRulesDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.authorizationRulesDataGridView_CellContentClick);
            this.authorizationRulesDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.authorizationRulesDataGridView_DataError);
            this.authorizationRulesDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.authorizationRulesDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.authorizationRulesDataGridView_RowEnter);
            this.authorizationRulesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.authorizationRulesDataGridView_RowsAdded);
            this.authorizationRulesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.authorizationRulesDataGridView_RowsRemoved);
            this.authorizationRulesDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.authorizationRulesDataGridView_UserDeletingRow);
            this.authorizationRulesDataGridView.Resize += new System.EventHandler(this.authorizationRulesDataGridView_Resize);
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageMessages.Controls.Add(this.messagesSplitContainer);
            this.tabPageMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessages.Size = new System.Drawing.Size(968, 454);
            this.tabPageMessages.TabIndex = 5;
            this.tabPageMessages.Text = "Messages";
            this.tabPageMessages.Resize += new System.EventHandler(this.tabPageMessages_Resize);
            // 
            // messagesSplitContainer
            // 
            this.messagesSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.messagesSplitContainer.Name = "messagesSplitContainer";
            // 
            // messagesSplitContainer.Panel1
            // 
            this.messagesSplitContainer.Panel1.Controls.Add(this.messageMainSplitContainer);
            // 
            // messagesSplitContainer.Panel2
            // 
            this.messagesSplitContainer.Panel2.Controls.Add(this.messagePropertiesSplitContainer);
            this.messagesSplitContainer.Size = new System.Drawing.Size(936, 432);
            this.messagesSplitContainer.SplitterDistance = 606;
            this.messagesSplitContainer.SplitterWidth = 16;
            this.messagesSplitContainer.TabIndex = 3;
            // 
            // messageMainSplitContainer
            // 
            this.messageMainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageMainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messageMainSplitContainer.Name = "messageMainSplitContainer";
            this.messageMainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // messageMainSplitContainer.Panel1
            // 
            this.messageMainSplitContainer.Panel1.Controls.Add(this.grouperMessageList);
            // 
            // messageMainSplitContainer.Panel2
            // 
            this.messageMainSplitContainer.Panel2.Controls.Add(this.grouperMessageText);
            this.messageMainSplitContainer.Size = new System.Drawing.Size(606, 432);
            this.messageMainSplitContainer.SplitterDistance = 211;
            this.messageMainSplitContainer.SplitterWidth = 8;
            this.messageMainSplitContainer.TabIndex = 0;
            // 
            // grouperMessageList
            // 
            this.grouperMessageList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageList.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageList.BorderThickness = 1F;
            this.grouperMessageList.Controls.Add(this.pictFindMessages);
            this.grouperMessageList.Controls.Add(this.pictFindMessagesByDate);
            this.grouperMessageList.Controls.Add(this.messagesDataGridView);
            this.grouperMessageList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageList.ForeColor = System.Drawing.Color.White;
            this.grouperMessageList.GroupImage = null;
            this.grouperMessageList.GroupTitle = "Message List";
            this.grouperMessageList.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageList.Name = "grouperMessageList";
            this.grouperMessageList.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageList.PaintGroupBox = true;
            this.grouperMessageList.RoundCorners = 4;
            this.grouperMessageList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageList.ShadowControl = false;
            this.grouperMessageList.ShadowThickness = 1;
            this.grouperMessageList.Size = new System.Drawing.Size(606, 211);
            this.grouperMessageList.TabIndex = 17;
            this.grouperMessageList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageList_CustomPaint);
            // 
            // pictFindMessages
            // 
            this.pictFindMessages.Image = ((System.Drawing.Image)(resources.GetObject("pictFindMessages.Image")));
            this.pictFindMessages.Location = new System.Drawing.Point(100, 0);
            this.pictFindMessages.Name = "pictFindMessages";
            this.pictFindMessages.Size = new System.Drawing.Size(24, 24);
            this.pictFindMessages.TabIndex = 1;
            this.pictFindMessages.TabStop = false;
            this.pictFindMessages.Click += new System.EventHandler(this.pictFindMessages_Click);
            this.pictFindMessages.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictFindMessages.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            // 
            // pictFindMessagesByDate
            // 
            this.pictFindMessagesByDate.Image = global::ServiceBusExplorer.Properties.Resources.FindByDateExtension;
            this.pictFindMessagesByDate.Location = new System.Drawing.Point(123, 0);
            this.pictFindMessagesByDate.Name = "pictFindMessagesByDate";
            this.pictFindMessagesByDate.Size = new System.Drawing.Size(24, 24);
            this.pictFindMessagesByDate.TabIndex = 1;
            this.pictFindMessagesByDate.TabStop = false;
            this.pictFindMessagesByDate.Click += new System.EventHandler(this.pictFindMessagesByDate_Click);
            this.pictFindMessagesByDate.MouseEnter += new System.EventHandler(this.pictureBoxByDate_MouseEnter);
            this.pictFindMessagesByDate.MouseLeave += new System.EventHandler(this.pictureBoxByDate_MouseLeave);
            // 
            // messagesDataGridView
            // 
            this.messagesDataGridView.AllowUserToAddRows = false;
            this.messagesDataGridView.AllowUserToDeleteRows = false;
            this.messagesDataGridView.AllowUserToResizeRows = false;
            this.messagesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.messagesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.messagesDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.messagesDataGridView.Location = new System.Drawing.Point(17, 33);
            this.messagesDataGridView.Name = "messagesDataGridView";
            this.messagesDataGridView.ReadOnly = true;
            this.messagesDataGridView.RowHeadersWidth = 24;
            this.messagesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.messagesDataGridView.ShowCellErrors = false;
            this.messagesDataGridView.ShowRowErrors = false;
            this.messagesDataGridView.Size = new System.Drawing.Size(574, 161);
            this.messagesDataGridView.TabIndex = 0;
            this.messagesDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.messagesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.messagesDataGridView_CellDoubleClick);
            this.messagesDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.messagesDataGridView_CellFormatting);
            this.messagesDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.messagesDataGridView_CellMouseDown);
            this.messagesDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.messagesDataGridView_DataError);
            this.messagesDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.messagesDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.messagesDataGridView_RowEnter);
            this.messagesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
            this.messagesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            this.messagesDataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
            this.messagesDataGridView.Resize += new System.EventHandler(this.dataGridView_Resize);
            // 
            // grouperMessageText
            // 
            this.grouperMessageText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageText.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.BorderThickness = 1F;
            this.grouperMessageText.Controls.Add(this.txtMessageText);
            this.grouperMessageText.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageText.ForeColor = System.Drawing.Color.White;
            this.grouperMessageText.GroupImage = null;
            this.grouperMessageText.GroupTitle = "Message Text";
            this.grouperMessageText.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageText.Name = "grouperMessageText";
            this.grouperMessageText.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageText.PaintGroupBox = true;
            this.grouperMessageText.RoundCorners = 4;
            this.grouperMessageText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageText.ShadowControl = false;
            this.grouperMessageText.ShadowThickness = 1;
            this.grouperMessageText.Size = new System.Drawing.Size(606, 213);
            this.grouperMessageText.TabIndex = 26;
            this.grouperMessageText.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageText_CustomPaint);
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtMessageText.AutoScrollMinSize = new System.Drawing.Size(2, 14);
            this.txtMessageText.BackBrush = null;
            this.txtMessageText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessageText.CharHeight = 14;
            this.txtMessageText.CharWidth = 8;
            this.txtMessageText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMessageText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtMessageText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtMessageText.IsReplaceMode = false;
            this.txtMessageText.Location = new System.Drawing.Point(16, 32);
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.Paddings = new System.Windows.Forms.Padding(0);
            this.txtMessageText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtMessageText.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtMessageText.ServiceColors")));
            this.txtMessageText.Size = new System.Drawing.Size(576, 164);
            this.txtMessageText.TabIndex = 0;
            this.txtMessageText.Zoom = 100;
            // 
            // messagePropertiesSplitContainer
            // 
            this.messagePropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagePropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messagePropertiesSplitContainer.Name = "messagePropertiesSplitContainer";
            this.messagePropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // messagePropertiesSplitContainer.Panel1
            // 
            this.messagePropertiesSplitContainer.Panel1.Controls.Add(this.grouperMessageSystemProperties);
            // 
            // messagePropertiesSplitContainer.Panel2
            // 
            this.messagePropertiesSplitContainer.Panel2.Controls.Add(this.grouperMessageCustomProperties);
            this.messagePropertiesSplitContainer.Size = new System.Drawing.Size(314, 432);
            this.messagePropertiesSplitContainer.SplitterDistance = 211;
            this.messagePropertiesSplitContainer.SplitterWidth = 8;
            this.messagePropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperMessageSystemProperties
            // 
            this.grouperMessageSystemProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageSystemProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageSystemProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageSystemProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageSystemProperties.BorderThickness = 1F;
            this.grouperMessageSystemProperties.Controls.Add(this.messagePropertyGrid);
            this.grouperMessageSystemProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageSystemProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageSystemProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageSystemProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageSystemProperties.GroupImage = null;
            this.grouperMessageSystemProperties.GroupTitle = "Message System Properties";
            this.grouperMessageSystemProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageSystemProperties.Name = "grouperMessageSystemProperties";
            this.grouperMessageSystemProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageSystemProperties.PaintGroupBox = true;
            this.grouperMessageSystemProperties.RoundCorners = 4;
            this.grouperMessageSystemProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageSystemProperties.ShadowControl = false;
            this.grouperMessageSystemProperties.ShadowThickness = 1;
            this.grouperMessageSystemProperties.Size = new System.Drawing.Size(314, 211);
            this.grouperMessageSystemProperties.TabIndex = 20;
            this.grouperMessageSystemProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageSystemProperties_CustomPaint);
            // 
            // messagePropertyGrid
            // 
            this.messagePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagePropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.messagePropertyGrid.HelpVisible = false;
            this.messagePropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.messagePropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.messagePropertyGrid.Name = "messagePropertyGrid";
            this.messagePropertyGrid.Size = new System.Drawing.Size(281, 163);
            this.messagePropertyGrid.TabIndex = 2;
            this.messagePropertyGrid.ToolbarVisible = false;
            // 
            // grouperMessageCustomProperties
            // 
            this.grouperMessageCustomProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageCustomProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageCustomProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageCustomProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageCustomProperties.BorderThickness = 1F;
            this.grouperMessageCustomProperties.Controls.Add(this.messageCustomPropertyGrid);
            this.grouperMessageCustomProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageCustomProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageCustomProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageCustomProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageCustomProperties.GroupImage = null;
            this.grouperMessageCustomProperties.GroupTitle = "Message Custom Properties";
            this.grouperMessageCustomProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageCustomProperties.Name = "grouperMessageCustomProperties";
            this.grouperMessageCustomProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageCustomProperties.PaintGroupBox = true;
            this.grouperMessageCustomProperties.RoundCorners = 4;
            this.grouperMessageCustomProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageCustomProperties.ShadowControl = false;
            this.grouperMessageCustomProperties.ShadowThickness = 1;
            this.grouperMessageCustomProperties.Size = new System.Drawing.Size(314, 213);
            this.grouperMessageCustomProperties.TabIndex = 27;
            this.grouperMessageCustomProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageCustomProperties_CustomPaint);
            // 
            // messageCustomPropertyGrid
            // 
            this.messageCustomPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageCustomPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.messageCustomPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.messageCustomPropertyGrid.HelpVisible = false;
            this.messageCustomPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.messageCustomPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.messageCustomPropertyGrid.Name = "messageCustomPropertyGrid";
            this.messageCustomPropertyGrid.Size = new System.Drawing.Size(281, 164);
            this.messageCustomPropertyGrid.TabIndex = 1;
            this.messageCustomPropertyGrid.ToolbarVisible = false;
            // 
            // tabPageDeadletter
            // 
            this.tabPageDeadletter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageDeadletter.Controls.Add(this.deadletterSplitContainer);
            this.tabPageDeadletter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDeadletter.Location = new System.Drawing.Point(4, 22);
            this.tabPageDeadletter.Name = "tabPageDeadletter";
            this.tabPageDeadletter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDeadletter.Size = new System.Drawing.Size(968, 454);
            this.tabPageDeadletter.TabIndex = 7;
            this.tabPageDeadletter.Text = "Deadletter";
            this.tabPageDeadletter.Resize += new System.EventHandler(this.deadletterTabPage_Resize);
            // 
            // deadletterSplitContainer
            // 
            this.deadletterSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deadletterSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.deadletterSplitContainer.Name = "deadletterSplitContainer";
            // 
            // deadletterSplitContainer.Panel1
            // 
            this.deadletterSplitContainer.Panel1.Controls.Add(this.deadletterMainSplitContainer);
            // 
            // deadletterSplitContainer.Panel2
            // 
            this.deadletterSplitContainer.Panel2.Controls.Add(this.deadletterPropertiesSplitContainer);
            this.deadletterSplitContainer.Size = new System.Drawing.Size(936, 432);
            this.deadletterSplitContainer.SplitterDistance = 606;
            this.deadletterSplitContainer.SplitterWidth = 16;
            this.deadletterSplitContainer.TabIndex = 4;
            // 
            // deadletterMainSplitContainer
            // 
            this.deadletterMainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deadletterMainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.deadletterMainSplitContainer.Name = "deadletterMainSplitContainer";
            this.deadletterMainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // deadletterMainSplitContainer.Panel1
            // 
            this.deadletterMainSplitContainer.Panel1.Controls.Add(this.grouperDeadletterList);
            // 
            // deadletterMainSplitContainer.Panel2
            // 
            this.deadletterMainSplitContainer.Panel2.Controls.Add(this.grouperDeadletterText);
            this.deadletterMainSplitContainer.Size = new System.Drawing.Size(606, 432);
            this.deadletterMainSplitContainer.SplitterDistance = 211;
            this.deadletterMainSplitContainer.SplitterWidth = 8;
            this.deadletterMainSplitContainer.TabIndex = 0;
            // 
            // grouperDeadletterList
            // 
            this.grouperDeadletterList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDeadletterList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDeadletterList.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDeadletterList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterList.BorderThickness = 1F;
            this.grouperDeadletterList.Controls.Add(this.pictFindDeadletter);
            this.grouperDeadletterList.Controls.Add(this.pictFindDeadletterByDate);
            this.grouperDeadletterList.Controls.Add(this.deadletterDataGridView);
            this.grouperDeadletterList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperDeadletterList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDeadletterList.ForeColor = System.Drawing.Color.White;
            this.grouperDeadletterList.GroupImage = null;
            this.grouperDeadletterList.GroupTitle = "Message List";
            this.grouperDeadletterList.Location = new System.Drawing.Point(0, 0);
            this.grouperDeadletterList.Name = "grouperDeadletterList";
            this.grouperDeadletterList.Padding = new System.Windows.Forms.Padding(20);
            this.grouperDeadletterList.PaintGroupBox = true;
            this.grouperDeadletterList.RoundCorners = 4;
            this.grouperDeadletterList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDeadletterList.ShadowControl = false;
            this.grouperDeadletterList.ShadowThickness = 1;
            this.grouperDeadletterList.Size = new System.Drawing.Size(606, 211);
            this.grouperDeadletterList.TabIndex = 17;
            this.grouperDeadletterList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterList_CustomPaint);
            // 
            // pictFindDeadletter
            // 
            this.pictFindDeadletter.Image = ((System.Drawing.Image)(resources.GetObject("pictFindDeadletter.Image")));
            this.pictFindDeadletter.Location = new System.Drawing.Point(100, 0);
            this.pictFindDeadletter.Name = "pictFindDeadletter";
            this.pictFindDeadletter.Size = new System.Drawing.Size(24, 24);
            this.pictFindDeadletter.TabIndex = 2;
            this.pictFindDeadletter.TabStop = false;
            this.pictFindDeadletter.Click += new System.EventHandler(this.pictFindDeadletter_Click);
            this.pictFindDeadletter.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictFindDeadletter.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            // 
            // pictFindDeadletterByDate
            // 
            this.pictFindDeadletterByDate.Image = global::ServiceBusExplorer.Properties.Resources.FindByDateExtension;
            this.pictFindDeadletterByDate.Location = new System.Drawing.Point(123, 0);
            this.pictFindDeadletterByDate.Name = "pictFindDeadletterByDate";
            this.pictFindDeadletterByDate.Size = new System.Drawing.Size(24, 24);
            this.pictFindDeadletterByDate.TabIndex = 2;
            this.pictFindDeadletterByDate.TabStop = false;
            this.pictFindDeadletterByDate.Click += new System.EventHandler(this.pictFindDeadletterByDate_Click);
            this.pictFindDeadletterByDate.MouseEnter += new System.EventHandler(this.pictureBoxByDate_MouseEnter);
            this.pictFindDeadletterByDate.MouseLeave += new System.EventHandler(this.pictureBoxByDate_MouseLeave);
            // 
            // deadletterDataGridView
            // 
            this.deadletterDataGridView.AllowUserToAddRows = false;
            this.deadletterDataGridView.AllowUserToDeleteRows = false;
            this.deadletterDataGridView.AllowUserToResizeRows = false;
            this.deadletterDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deadletterDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.deadletterDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.deadletterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.deadletterDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.deadletterDataGridView.Location = new System.Drawing.Point(17, 33);
            this.deadletterDataGridView.Name = "deadletterDataGridView";
            this.deadletterDataGridView.ReadOnly = true;
            this.deadletterDataGridView.RowHeadersWidth = 24;
            this.deadletterDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.deadletterDataGridView.ShowCellErrors = false;
            this.deadletterDataGridView.ShowRowErrors = false;
            this.deadletterDataGridView.Size = new System.Drawing.Size(574, 161);
            this.deadletterDataGridView.TabIndex = 0;
            this.deadletterDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.deadletterDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.deadletterDataGridView_CellDoubleClick);
            this.deadletterDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.deadletterDataGridView_CellFormatting);
            this.deadletterDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.deadletterDataGridView_CellMouseDown);
            this.deadletterDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.deadletterDataGridView_DataError);
            this.deadletterDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.deadletterDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.deadletterDataGridView_RowEnter);
            this.deadletterDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
            this.deadletterDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            this.deadletterDataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
            this.deadletterDataGridView.Resize += new System.EventHandler(this.dataGridView_Resize);
            // 
            // grouperDeadletterText
            // 
            this.grouperDeadletterText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDeadletterText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDeadletterText.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDeadletterText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterText.BorderThickness = 1F;
            this.grouperDeadletterText.Controls.Add(this.txtDeadletterText);
            this.grouperDeadletterText.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperDeadletterText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDeadletterText.ForeColor = System.Drawing.Color.White;
            this.grouperDeadletterText.GroupImage = null;
            this.grouperDeadletterText.GroupTitle = "Message Text";
            this.grouperDeadletterText.Location = new System.Drawing.Point(0, 0);
            this.grouperDeadletterText.Name = "grouperDeadletterText";
            this.grouperDeadletterText.Padding = new System.Windows.Forms.Padding(20);
            this.grouperDeadletterText.PaintGroupBox = true;
            this.grouperDeadletterText.RoundCorners = 4;
            this.grouperDeadletterText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDeadletterText.ShadowControl = false;
            this.grouperDeadletterText.ShadowThickness = 1;
            this.grouperDeadletterText.Size = new System.Drawing.Size(606, 213);
            this.grouperDeadletterText.TabIndex = 26;
            this.grouperDeadletterText.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterText_CustomPaint);
            // 
            // txtDeadletterText
            // 
            this.txtDeadletterText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeadletterText.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtDeadletterText.AutoScrollMinSize = new System.Drawing.Size(2, 14);
            this.txtDeadletterText.BackBrush = null;
            this.txtDeadletterText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeadletterText.CharHeight = 14;
            this.txtDeadletterText.CharWidth = 8;
            this.txtDeadletterText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDeadletterText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtDeadletterText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDeadletterText.IsReplaceMode = false;
            this.txtDeadletterText.Location = new System.Drawing.Point(16, 32);
            this.txtDeadletterText.Name = "txtDeadletterText";
            this.txtDeadletterText.Paddings = new System.Windows.Forms.Padding(0);
            this.txtDeadletterText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtDeadletterText.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtDeadletterText.ServiceColors")));
            this.txtDeadletterText.Size = new System.Drawing.Size(576, 164);
            this.txtDeadletterText.TabIndex = 0;
            this.txtDeadletterText.Zoom = 100;
            // 
            // deadletterPropertiesSplitContainer
            // 
            this.deadletterPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deadletterPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.deadletterPropertiesSplitContainer.Name = "deadletterPropertiesSplitContainer";
            this.deadletterPropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // deadletterPropertiesSplitContainer.Panel1
            // 
            this.deadletterPropertiesSplitContainer.Panel1.Controls.Add(this.grouperDeadletterSystemProperties);
            // 
            // deadletterPropertiesSplitContainer.Panel2
            // 
            this.deadletterPropertiesSplitContainer.Panel2.Controls.Add(this.grouperDeadletterCustomProperties);
            this.deadletterPropertiesSplitContainer.Size = new System.Drawing.Size(314, 432);
            this.deadletterPropertiesSplitContainer.SplitterDistance = 211;
            this.deadletterPropertiesSplitContainer.SplitterWidth = 8;
            this.deadletterPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperDeadletterSystemProperties
            // 
            this.grouperDeadletterSystemProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDeadletterSystemProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDeadletterSystemProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDeadletterSystemProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterSystemProperties.BorderThickness = 1F;
            this.grouperDeadletterSystemProperties.Controls.Add(this.deadletterPropertyGrid);
            this.grouperDeadletterSystemProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterSystemProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperDeadletterSystemProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDeadletterSystemProperties.ForeColor = System.Drawing.Color.White;
            this.grouperDeadletterSystemProperties.GroupImage = null;
            this.grouperDeadletterSystemProperties.GroupTitle = "Message System Properties";
            this.grouperDeadletterSystemProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperDeadletterSystemProperties.Name = "grouperDeadletterSystemProperties";
            this.grouperDeadletterSystemProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperDeadletterSystemProperties.PaintGroupBox = true;
            this.grouperDeadletterSystemProperties.RoundCorners = 4;
            this.grouperDeadletterSystemProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDeadletterSystemProperties.ShadowControl = false;
            this.grouperDeadletterSystemProperties.ShadowThickness = 1;
            this.grouperDeadletterSystemProperties.Size = new System.Drawing.Size(314, 211);
            this.grouperDeadletterSystemProperties.TabIndex = 20;
            this.grouperDeadletterSystemProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterSystemProperties_CustomPaint);
            // 
            // deadletterPropertyGrid
            // 
            this.deadletterPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deadletterPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.deadletterPropertyGrid.HelpVisible = false;
            this.deadletterPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.deadletterPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.deadletterPropertyGrid.Name = "deadletterPropertyGrid";
            this.deadletterPropertyGrid.Size = new System.Drawing.Size(281, 162);
            this.deadletterPropertyGrid.TabIndex = 1;
            this.deadletterPropertyGrid.ToolbarVisible = false;
            // 
            // grouperDeadletterCustomProperties
            // 
            this.grouperDeadletterCustomProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDeadletterCustomProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDeadletterCustomProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDeadletterCustomProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterCustomProperties.BorderThickness = 1F;
            this.grouperDeadletterCustomProperties.Controls.Add(this.deadletterCustomPropertyGrid);
            this.grouperDeadletterCustomProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterCustomProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperDeadletterCustomProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDeadletterCustomProperties.ForeColor = System.Drawing.Color.White;
            this.grouperDeadletterCustomProperties.GroupImage = null;
            this.grouperDeadletterCustomProperties.GroupTitle = "Message Custom Properties";
            this.grouperDeadletterCustomProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperDeadletterCustomProperties.Name = "grouperDeadletterCustomProperties";
            this.grouperDeadletterCustomProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperDeadletterCustomProperties.PaintGroupBox = true;
            this.grouperDeadletterCustomProperties.RoundCorners = 4;
            this.grouperDeadletterCustomProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDeadletterCustomProperties.ShadowControl = false;
            this.grouperDeadletterCustomProperties.ShadowThickness = 1;
            this.grouperDeadletterCustomProperties.Size = new System.Drawing.Size(314, 213);
            this.grouperDeadletterCustomProperties.TabIndex = 27;
            this.grouperDeadletterCustomProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterCustomProperties_CustomPaint);
            // 
            // deadletterCustomPropertyGrid
            // 
            this.deadletterCustomPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deadletterCustomPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.deadletterCustomPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.deadletterCustomPropertyGrid.HelpVisible = false;
            this.deadletterCustomPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.deadletterCustomPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.deadletterCustomPropertyGrid.Name = "deadletterCustomPropertyGrid";
            this.deadletterCustomPropertyGrid.Size = new System.Drawing.Size(281, 164);
            this.deadletterCustomPropertyGrid.TabIndex = 2;
            this.deadletterCustomPropertyGrid.ToolbarVisible = false;
            // 
            // tabPageTransferDeadletter
            // 
            this.tabPageTransferDeadletter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageTransferDeadletter.Controls.Add(this.transferDeadletterSplitContainer);
            this.tabPageTransferDeadletter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageTransferDeadletter.Location = new System.Drawing.Point(4, 22);
            this.tabPageTransferDeadletter.Name = "tabPageTransferDeadletter";
            this.tabPageTransferDeadletter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTransferDeadletter.Size = new System.Drawing.Size(968, 454);
            this.tabPageTransferDeadletter.TabIndex = 10;
            this.tabPageTransferDeadletter.Text = "Transfer Deadletter";
            // 
            // transferDeadletterSplitContainer
            // 
            this.transferDeadletterSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transferDeadletterSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.transferDeadletterSplitContainer.Name = "transferDeadletterSplitContainer";
            // 
            // transferDeadletterSplitContainer.Panel1
            // 
            this.transferDeadletterSplitContainer.Panel1.Controls.Add(this.transferMainSplitContainer);
            // 
            // transferDeadletterSplitContainer.Panel2
            // 
            this.transferDeadletterSplitContainer.Panel2.Controls.Add(this.transferDeadletterPropertiesSplitContainer);
            this.transferDeadletterSplitContainer.Size = new System.Drawing.Size(936, 432);
            this.transferDeadletterSplitContainer.SplitterDistance = 606;
            this.transferDeadletterSplitContainer.SplitterWidth = 16;
            this.transferDeadletterSplitContainer.TabIndex = 5;
            // 
            // transferMainSplitContainer
            // 
            this.transferMainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transferMainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.transferMainSplitContainer.Name = "transferMainSplitContainer";
            this.transferMainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // transferMainSplitContainer.Panel1
            // 
            this.transferMainSplitContainer.Panel1.Controls.Add(this.grouperTransferDeadletterList);
            // 
            // transferMainSplitContainer.Panel2
            // 
            this.transferMainSplitContainer.Panel2.Controls.Add(this.grouperTransferDeadletterText);
            this.transferMainSplitContainer.Size = new System.Drawing.Size(606, 432);
            this.transferMainSplitContainer.SplitterDistance = 211;
            this.transferMainSplitContainer.SplitterWidth = 8;
            this.transferMainSplitContainer.TabIndex = 0;
            // 
            // grouperTransferDeadletterList
            // 
            this.grouperTransferDeadletterList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperTransferDeadletterList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperTransferDeadletterList.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperTransferDeadletterList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTransferDeadletterList.BorderThickness = 1F;
            this.grouperTransferDeadletterList.Controls.Add(this.pictureBox1);
            this.grouperTransferDeadletterList.Controls.Add(this.transferDeadletterDataGridView);
            this.grouperTransferDeadletterList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTransferDeadletterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperTransferDeadletterList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperTransferDeadletterList.ForeColor = System.Drawing.Color.White;
            this.grouperTransferDeadletterList.GroupImage = null;
            this.grouperTransferDeadletterList.GroupTitle = "Message List";
            this.grouperTransferDeadletterList.Location = new System.Drawing.Point(0, 0);
            this.grouperTransferDeadletterList.Name = "grouperTransferDeadletterList";
            this.grouperTransferDeadletterList.Padding = new System.Windows.Forms.Padding(20);
            this.grouperTransferDeadletterList.PaintGroupBox = true;
            this.grouperTransferDeadletterList.RoundCorners = 4;
            this.grouperTransferDeadletterList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperTransferDeadletterList.ShadowControl = false;
            this.grouperTransferDeadletterList.ShadowThickness = 1;
            this.grouperTransferDeadletterList.Size = new System.Drawing.Size(606, 211);
            this.grouperTransferDeadletterList.TabIndex = 17;
            this.grouperTransferDeadletterList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperTransferDeadletterList_CustomPaint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(100, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // transferDeadletterDataGridView
            // 
            this.transferDeadletterDataGridView.AllowUserToAddRows = false;
            this.transferDeadletterDataGridView.AllowUserToDeleteRows = false;
            this.transferDeadletterDataGridView.AllowUserToResizeRows = false;
            this.transferDeadletterDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transferDeadletterDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.transferDeadletterDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.transferDeadletterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transferDeadletterDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.transferDeadletterDataGridView.Location = new System.Drawing.Point(17, 33);
            this.transferDeadletterDataGridView.Name = "transferDeadletterDataGridView";
            this.transferDeadletterDataGridView.ReadOnly = true;
            this.transferDeadletterDataGridView.RowHeadersWidth = 24;
            this.transferDeadletterDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.transferDeadletterDataGridView.ShowCellErrors = false;
            this.transferDeadletterDataGridView.ShowRowErrors = false;
            this.transferDeadletterDataGridView.Size = new System.Drawing.Size(574, 161);
            this.transferDeadletterDataGridView.TabIndex = 0;
            this.transferDeadletterDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.transferDeadletterDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.transferDeadletterDataGridView_CellDoubleClick);
            this.transferDeadletterDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.transferDeadletterDataGridView_CellFormatting);
            this.transferDeadletterDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.transferDeadletterDataGridView_CellMouseDown);
            this.transferDeadletterDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.transferDeadletterDataGridView_DataError);
            this.transferDeadletterDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.transferDeadletterDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.transferDeadletterDataGridView_RowEnter);
            this.transferDeadletterDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
            this.transferDeadletterDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            this.transferDeadletterDataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
            this.transferDeadletterDataGridView.Resize += new System.EventHandler(this.dataGridView_Resize);
            // 
            // grouperTransferDeadletterText
            // 
            this.grouperTransferDeadletterText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperTransferDeadletterText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperTransferDeadletterText.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperTransferDeadletterText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTransferDeadletterText.BorderThickness = 1F;
            this.grouperTransferDeadletterText.Controls.Add(this.txtTransferDeadletterText);
            this.grouperTransferDeadletterText.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTransferDeadletterText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperTransferDeadletterText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperTransferDeadletterText.ForeColor = System.Drawing.Color.White;
            this.grouperTransferDeadletterText.GroupImage = null;
            this.grouperTransferDeadletterText.GroupTitle = "Message Text";
            this.grouperTransferDeadletterText.Location = new System.Drawing.Point(0, 0);
            this.grouperTransferDeadletterText.Name = "grouperTransferDeadletterText";
            this.grouperTransferDeadletterText.Padding = new System.Windows.Forms.Padding(20);
            this.grouperTransferDeadletterText.PaintGroupBox = true;
            this.grouperTransferDeadletterText.RoundCorners = 4;
            this.grouperTransferDeadletterText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperTransferDeadletterText.ShadowControl = false;
            this.grouperTransferDeadletterText.ShadowThickness = 1;
            this.grouperTransferDeadletterText.Size = new System.Drawing.Size(606, 213);
            this.grouperTransferDeadletterText.TabIndex = 26;
            this.grouperTransferDeadletterText.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperTransferDeadletterText_CustomPaint);
            // 
            // txtTransferDeadletterText
            // 
            this.txtTransferDeadletterText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTransferDeadletterText.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtTransferDeadletterText.AutoScrollMinSize = new System.Drawing.Size(2, 14);
            this.txtTransferDeadletterText.BackBrush = null;
            this.txtTransferDeadletterText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransferDeadletterText.CharHeight = 14;
            this.txtTransferDeadletterText.CharWidth = 8;
            this.txtTransferDeadletterText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTransferDeadletterText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtTransferDeadletterText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTransferDeadletterText.IsReplaceMode = false;
            this.txtTransferDeadletterText.Location = new System.Drawing.Point(16, 32);
            this.txtTransferDeadletterText.Name = "txtTransferDeadletterText";
            this.txtTransferDeadletterText.Paddings = new System.Windows.Forms.Padding(0);
            this.txtTransferDeadletterText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtTransferDeadletterText.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtTransferDeadletterText.ServiceColors")));
            this.txtTransferDeadletterText.Size = new System.Drawing.Size(576, 164);
            this.txtTransferDeadletterText.TabIndex = 0;
            this.txtTransferDeadletterText.Zoom = 100;
            // 
            // transferDeadletterPropertiesSplitContainer
            // 
            this.transferDeadletterPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transferDeadletterPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.transferDeadletterPropertiesSplitContainer.Name = "transferDeadletterPropertiesSplitContainer";
            this.transferDeadletterPropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // transferDeadletterPropertiesSplitContainer.Panel1
            // 
            this.transferDeadletterPropertiesSplitContainer.Panel1.Controls.Add(this.grouperTransferDeadletterSystemProperties);
            // 
            // transferDeadletterPropertiesSplitContainer.Panel2
            // 
            this.transferDeadletterPropertiesSplitContainer.Panel2.Controls.Add(this.grouperTransferDeadletterCustomProperties);
            this.transferDeadletterPropertiesSplitContainer.Size = new System.Drawing.Size(314, 432);
            this.transferDeadletterPropertiesSplitContainer.SplitterDistance = 211;
            this.transferDeadletterPropertiesSplitContainer.SplitterWidth = 8;
            this.transferDeadletterPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperTransferDeadletterSystemProperties
            // 
            this.grouperTransferDeadletterSystemProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperTransferDeadletterSystemProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperTransferDeadletterSystemProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperTransferDeadletterSystemProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTransferDeadletterSystemProperties.BorderThickness = 1F;
            this.grouperTransferDeadletterSystemProperties.Controls.Add(this.transferDeadletterPropertyGrid);
            this.grouperTransferDeadletterSystemProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTransferDeadletterSystemProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperTransferDeadletterSystemProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperTransferDeadletterSystemProperties.ForeColor = System.Drawing.Color.White;
            this.grouperTransferDeadletterSystemProperties.GroupImage = null;
            this.grouperTransferDeadletterSystemProperties.GroupTitle = "Message System Properties";
            this.grouperTransferDeadletterSystemProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperTransferDeadletterSystemProperties.Name = "grouperTransferDeadletterSystemProperties";
            this.grouperTransferDeadletterSystemProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperTransferDeadletterSystemProperties.PaintGroupBox = true;
            this.grouperTransferDeadletterSystemProperties.RoundCorners = 4;
            this.grouperTransferDeadletterSystemProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperTransferDeadletterSystemProperties.ShadowControl = false;
            this.grouperTransferDeadletterSystemProperties.ShadowThickness = 1;
            this.grouperTransferDeadletterSystemProperties.Size = new System.Drawing.Size(314, 211);
            this.grouperTransferDeadletterSystemProperties.TabIndex = 20;
            this.grouperTransferDeadletterSystemProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperTransferDeadletterSystemProperties_CustomPaint);
            // 
            // transferDeadletterPropertyGrid
            // 
            this.transferDeadletterPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transferDeadletterPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.transferDeadletterPropertyGrid.HelpVisible = false;
            this.transferDeadletterPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.transferDeadletterPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.transferDeadletterPropertyGrid.Name = "transferDeadletterPropertyGrid";
            this.transferDeadletterPropertyGrid.Size = new System.Drawing.Size(281, 163);
            this.transferDeadletterPropertyGrid.TabIndex = 1;
            this.transferDeadletterPropertyGrid.ToolbarVisible = false;
            // 
            // grouperTransferDeadletterCustomProperties
            // 
            this.grouperTransferDeadletterCustomProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperTransferDeadletterCustomProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperTransferDeadletterCustomProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperTransferDeadletterCustomProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTransferDeadletterCustomProperties.BorderThickness = 1F;
            this.grouperTransferDeadletterCustomProperties.Controls.Add(this.transferDeadletterCustomPropertyGrid);
            this.grouperTransferDeadletterCustomProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTransferDeadletterCustomProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperTransferDeadletterCustomProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperTransferDeadletterCustomProperties.ForeColor = System.Drawing.Color.White;
            this.grouperTransferDeadletterCustomProperties.GroupImage = null;
            this.grouperTransferDeadletterCustomProperties.GroupTitle = "Message Custom Properties";
            this.grouperTransferDeadletterCustomProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperTransferDeadletterCustomProperties.Name = "grouperTransferDeadletterCustomProperties";
            this.grouperTransferDeadletterCustomProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperTransferDeadletterCustomProperties.PaintGroupBox = true;
            this.grouperTransferDeadletterCustomProperties.RoundCorners = 4;
            this.grouperTransferDeadletterCustomProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperTransferDeadletterCustomProperties.ShadowControl = false;
            this.grouperTransferDeadletterCustomProperties.ShadowThickness = 1;
            this.grouperTransferDeadletterCustomProperties.Size = new System.Drawing.Size(314, 213);
            this.grouperTransferDeadletterCustomProperties.TabIndex = 27;
            this.grouperTransferDeadletterCustomProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterCustomProperties_CustomPaint);
            // 
            // transferDeadletterCustomPropertyGrid
            // 
            this.transferDeadletterCustomPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transferDeadletterCustomPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.transferDeadletterCustomPropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.transferDeadletterCustomPropertyGrid.HelpVisible = false;
            this.transferDeadletterCustomPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.transferDeadletterCustomPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.transferDeadletterCustomPropertyGrid.Name = "transferDeadletterCustomPropertyGrid";
            this.transferDeadletterCustomPropertyGrid.Size = new System.Drawing.Size(281, 164);
            this.transferDeadletterCustomPropertyGrid.TabIndex = 3;
            this.transferDeadletterCustomPropertyGrid.ToolbarVisible = false;
            // 
            // tabPageSessions
            // 
            this.tabPageSessions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageSessions.Controls.Add(this.sessionListStateSplitContainer);
            this.tabPageSessions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageSessions.Location = new System.Drawing.Point(4, 22);
            this.tabPageSessions.Name = "tabPageSessions";
            this.tabPageSessions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSessions.Size = new System.Drawing.Size(968, 454);
            this.tabPageSessions.TabIndex = 6;
            this.tabPageSessions.Text = "Sessions";
            this.tabPageSessions.Resize += new System.EventHandler(this.tabPageSessions_Resize);
            // 
            // sessionListStateSplitContainer
            // 
            this.sessionListStateSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionListStateSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.sessionListStateSplitContainer.Name = "sessionListStateSplitContainer";
            // 
            // sessionListStateSplitContainer.Panel1
            // 
            this.sessionListStateSplitContainer.Panel1.Controls.Add(this.sessionMainSplitContainer);
            // 
            // sessionListStateSplitContainer.Panel2
            // 
            this.sessionListStateSplitContainer.Panel2.Controls.Add(this.grouperSessionProperties);
            this.sessionListStateSplitContainer.Size = new System.Drawing.Size(936, 432);
            this.sessionListStateSplitContainer.SplitterDistance = 606;
            this.sessionListStateSplitContainer.SplitterWidth = 16;
            this.sessionListStateSplitContainer.TabIndex = 4;
            // 
            // sessionMainSplitContainer
            // 
            this.sessionMainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sessionMainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.sessionMainSplitContainer.Name = "sessionMainSplitContainer";
            this.sessionMainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sessionMainSplitContainer.Panel1
            // 
            this.sessionMainSplitContainer.Panel1.Controls.Add(this.grouperSessionList);
            // 
            // sessionMainSplitContainer.Panel2
            // 
            this.sessionMainSplitContainer.Panel2.Controls.Add(this.grouperSessionState);
            this.sessionMainSplitContainer.Size = new System.Drawing.Size(606, 432);
            this.sessionMainSplitContainer.SplitterDistance = 211;
            this.sessionMainSplitContainer.SplitterWidth = 8;
            this.sessionMainSplitContainer.TabIndex = 1;
            // 
            // grouperSessionList
            // 
            this.grouperSessionList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSessionList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSessionList.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperSessionList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionList.BorderThickness = 1F;
            this.grouperSessionList.Controls.Add(this.sessionsDataGridView);
            this.grouperSessionList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperSessionList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSessionList.ForeColor = System.Drawing.Color.White;
            this.grouperSessionList.GroupImage = null;
            this.grouperSessionList.GroupTitle = "Session List";
            this.grouperSessionList.Location = new System.Drawing.Point(0, 0);
            this.grouperSessionList.Name = "grouperSessionList";
            this.grouperSessionList.Padding = new System.Windows.Forms.Padding(20);
            this.grouperSessionList.PaintGroupBox = true;
            this.grouperSessionList.RoundCorners = 4;
            this.grouperSessionList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSessionList.ShadowControl = false;
            this.grouperSessionList.ShadowThickness = 1;
            this.grouperSessionList.Size = new System.Drawing.Size(606, 211);
            this.grouperSessionList.TabIndex = 20;
            this.grouperSessionList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperSessionList_CustomPaint);
            // 
            // sessionsDataGridView
            // 
            this.sessionsDataGridView.AllowUserToAddRows = false;
            this.sessionsDataGridView.AllowUserToDeleteRows = false;
            this.sessionsDataGridView.AllowUserToResizeRows = false;
            this.sessionsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionsDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.sessionsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sessionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sessionsDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.sessionsDataGridView.Location = new System.Drawing.Point(17, 33);
            this.sessionsDataGridView.MultiSelect = false;
            this.sessionsDataGridView.Name = "sessionsDataGridView";
            this.sessionsDataGridView.ReadOnly = true;
            this.sessionsDataGridView.RowHeadersWidth = 24;
            this.sessionsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sessionsDataGridView.ShowCellErrors = false;
            this.sessionsDataGridView.ShowRowErrors = false;
            this.sessionsDataGridView.Size = new System.Drawing.Size(571, 161);
            this.sessionsDataGridView.TabIndex = 0;
            this.sessionsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.sessionsDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.sessionsDataGridView_DataError);
            this.sessionsDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.sessionsDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.sessionsDataGridView_RowEnter);
            this.sessionsDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
            this.sessionsDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            this.sessionsDataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
            this.sessionsDataGridView.Resize += new System.EventHandler(this.dataGridView_Resize);
            // 
            // grouperSessionState
            // 
            this.grouperSessionState.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSessionState.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSessionState.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperSessionState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionState.BorderThickness = 1F;
            this.grouperSessionState.Controls.Add(this.txtSessionState);
            this.grouperSessionState.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperSessionState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSessionState.ForeColor = System.Drawing.Color.White;
            this.grouperSessionState.GroupImage = null;
            this.grouperSessionState.GroupTitle = "Session State";
            this.grouperSessionState.Location = new System.Drawing.Point(0, 0);
            this.grouperSessionState.Name = "grouperSessionState";
            this.grouperSessionState.Padding = new System.Windows.Forms.Padding(20);
            this.grouperSessionState.PaintGroupBox = true;
            this.grouperSessionState.RoundCorners = 4;
            this.grouperSessionState.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSessionState.ShadowControl = false;
            this.grouperSessionState.ShadowThickness = 1;
            this.grouperSessionState.Size = new System.Drawing.Size(606, 213);
            this.grouperSessionState.TabIndex = 26;
            this.grouperSessionState.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperSessionState_CustomPaint);
            // 
            // txtSessionState
            // 
            this.txtSessionState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSessionState.Location = new System.Drawing.Point(16, 32);
            this.txtSessionState.MaxLength = 0;
            this.txtSessionState.Multiline = true;
            this.txtSessionState.Name = "txtSessionState";
            this.txtSessionState.Size = new System.Drawing.Size(574, 165);
            this.txtSessionState.TabIndex = 13;
            // 
            // grouperSessionProperties
            // 
            this.grouperSessionProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSessionProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSessionProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperSessionProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionProperties.BorderThickness = 1F;
            this.grouperSessionProperties.Controls.Add(this.sessionPropertyGrid);
            this.grouperSessionProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperSessionProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSessionProperties.ForeColor = System.Drawing.Color.White;
            this.grouperSessionProperties.GroupImage = null;
            this.grouperSessionProperties.GroupTitle = "Session System Properties";
            this.grouperSessionProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperSessionProperties.Name = "grouperSessionProperties";
            this.grouperSessionProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperSessionProperties.PaintGroupBox = true;
            this.grouperSessionProperties.RoundCorners = 4;
            this.grouperSessionProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSessionProperties.ShadowControl = false;
            this.grouperSessionProperties.ShadowThickness = 1;
            this.grouperSessionProperties.Size = new System.Drawing.Size(314, 432);
            this.grouperSessionProperties.TabIndex = 19;
            this.grouperSessionProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperSessionProperties_CustomPaint);
            // 
            // sessionPropertyGrid
            // 
            this.sessionPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.sessionPropertyGrid.HelpVisible = false;
            this.sessionPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.sessionPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.sessionPropertyGrid.Name = "sessionPropertyGrid";
            this.sessionPropertyGrid.Size = new System.Drawing.Size(281, 384);
            this.sessionPropertyGrid.TabIndex = 1;
            this.sessionPropertyGrid.ToolbarVisible = false;
            // 
            // btnSessions
            // 
            this.btnSessions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSessions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSessions.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSessions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSessions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSessions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSessions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSessions.Location = new System.Drawing.Point(358, 504);
            this.btnSessions.Name = "btnSessions";
            this.btnSessions.Size = new System.Drawing.Size(72, 24);
            this.btnSessions.TabIndex = 3;
            this.btnSessions.Text = "Sessions";
            this.btnSessions.UseVisualStyleBackColor = false;
            this.btnSessions.Click += new System.EventHandler(this.btnSessions_Click);
            // 
            // btnMessages
            // 
            this.btnMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnMessages.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMessages.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMessages.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMessages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMessages.Location = new System.Drawing.Point(438, 504);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(72, 24);
            this.btnMessages.TabIndex = 4;
            this.btnMessages.Text = "Messages";
            this.btnMessages.UseVisualStyleBackColor = false;
            this.btnMessages.Click += new System.EventHandler(this.btnMessages_Click);
            // 
            // btnDeadletter
            // 
            this.btnDeadletter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeadletter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnDeadletter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDeadletter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDeadletter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDeadletter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeadletter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeadletter.Location = new System.Drawing.Point(518, 504);
            this.btnDeadletter.Name = "btnDeadletter";
            this.btnDeadletter.Size = new System.Drawing.Size(72, 24);
            this.btnDeadletter.TabIndex = 5;
            this.btnDeadletter.Text = "Dead-letter";
            this.btnDeadletter.UseVisualStyleBackColor = false;
            this.btnDeadletter.Click += new System.EventHandler(this.btnDeadletter_Click);
            // 
            // messagesContextMenuStrip
            // 
            this.messagesContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.messagesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repairAndResubmitMessageToolStripMenuItem,
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveSelectedMessageToolStripMenuItem,
            this.saveSelectedMessageBodyAsFileToolStripMenuItem,
            this.saveSelectedMessagesToolStripMenuItem,
            this.saveSelectedMessagesBodyAsFileToolStripMenuItem});
            this.messagesContextMenuStrip.Name = "registrationContextMenuStrip";
            this.messagesContextMenuStrip.Size = new System.Drawing.Size(306, 142);
            // 
            // repairAndResubmitMessageToolStripMenuItem
            // 
            this.repairAndResubmitMessageToolStripMenuItem.Name = "repairAndResubmitMessageToolStripMenuItem";
            this.repairAndResubmitMessageToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.repairAndResubmitMessageToolStripMenuItem.Text = "Repair and Resubmit Selected Message";
            this.repairAndResubmitMessageToolStripMenuItem.Click += new System.EventHandler(this.repairAndResubmitMessageToolStripMenuItem_Click);
            // 
            // resubmitSelectedMessagesInBatchModeToolStripMenuItem
            // 
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Name = "resubmitSelectedMessagesInBatchModeToolStripMenuItem";
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Text = "Resubmit Selected Messages In Batch Mode";
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Click += new System.EventHandler(this.resubmitSelectedMessagesInBatchModeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(302, 6);
            // 
            // saveSelectedMessageToolStripMenuItem
            // 
            this.saveSelectedMessageToolStripMenuItem.Name = "saveSelectedMessageToolStripMenuItem";
            this.saveSelectedMessageToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedMessageToolStripMenuItem.Text = "Save Selected Message";
            this.saveSelectedMessageToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedMessageToolStripMenuItem_Click);
            // 
            // saveSelectedMessageBodyAsFileToolStripMenuItem
            // 
            this.saveSelectedMessageBodyAsFileToolStripMenuItem.Name = "saveSelectedMessageBodyAsFileToolStripMenuItem";
            this.saveSelectedMessageBodyAsFileToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedMessageBodyAsFileToolStripMenuItem.Text = "Save Selected Message Text as File";
            this.saveSelectedMessageBodyAsFileToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedMessageBodyAsFileToolStripMenuItem_Click);
            // 
            // saveSelectedMessagesToolStripMenuItem
            // 
            this.saveSelectedMessagesToolStripMenuItem.Name = "saveSelectedMessagesToolStripMenuItem";
            this.saveSelectedMessagesToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedMessagesToolStripMenuItem.Text = "Save Selected Messages";
            this.saveSelectedMessagesToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedMessagesToolStripMenuItem_Click);
            // 
            // saveSelectedMessagesBodyAsFileToolStripMenuItem
            // 
            this.saveSelectedMessagesBodyAsFileToolStripMenuItem.Name = "saveSelectedMessagesBodyAsFileToolStripMenuItem";
            this.saveSelectedMessagesBodyAsFileToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedMessagesBodyAsFileToolStripMenuItem.Text = "Save Selected Messages Text as File";
            this.saveSelectedMessagesBodyAsFileToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedMessagesBodyAsFileToolStripMenuItem_Click);
            // 
            // deadletterContextMenuStrip
            // 
            this.deadletterContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.deadletterContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repairAndResubmitDeadletterToolStripMenuItem,
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveSelectedDeadletteredMessageToolStripMenuItem,
            this.saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem,
            this.saveSelectedDeadletteredMessagesToolStripMenuItem,
            this.saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem,
            this.deleteSelectedMessageToolStripMenuItem,
            this.deleteSelectedMessagesToolStripMenuItem});
            this.deadletterContextMenuStrip.Name = "registrationContextMenuStrip";
            this.deadletterContextMenuStrip.Size = new System.Drawing.Size(306, 186);
            // 
            // repairAndResubmitDeadletterToolStripMenuItem
            // 
            this.repairAndResubmitDeadletterToolStripMenuItem.Name = "repairAndResubmitDeadletterToolStripMenuItem";
            this.repairAndResubmitDeadletterToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.repairAndResubmitDeadletterToolStripMenuItem.Text = "Repair And Resubmit Selected Message";
            this.repairAndResubmitDeadletterToolStripMenuItem.Click += new System.EventHandler(this.repairAndResubmitDeadletterMessageToolStripMenuItem_Click);
            // 
            // resubmitSelectedDeadletterInBatchModeToolStripMenuItem
            // 
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Name = "resubmitSelectedDeadletterInBatchModeToolStripMenuItem";
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Text = "Resubmit Selected Messages In Batch Mode";
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Click += new System.EventHandler(this.resubmitSelectedDeadletterMessagesInBatchModeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(302, 6);
            // 
            // saveSelectedDeadletteredMessageToolStripMenuItem
            // 
            this.saveSelectedDeadletteredMessageToolStripMenuItem.Name = "saveSelectedDeadletteredMessageToolStripMenuItem";
            this.saveSelectedDeadletteredMessageToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedDeadletteredMessageToolStripMenuItem.Text = "Save Selected Message";
            this.saveSelectedDeadletteredMessageToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedDeadletteredMessageToolStripMenuItem_Click);
            // 
            // saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem
            // 
            this.saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem.Name = "saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem";
            this.saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem.Text = "Save Selected Message Text as File";
            this.saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem_Click);
            // 
            // saveSelectedDeadletteredMessagesToolStripMenuItem
            // 
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Name = "saveSelectedDeadletteredMessagesToolStripMenuItem";
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Text = "Save Selected Messages";
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedDeadletteredMessagesToolStripMenuItem_Click);
            // 
            // saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem
            // 
            this.saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem.Name = "saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem";
            this.saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem.Text = "Save Selected Messages Text as File";
            this.saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem_Click);
            // 
            // deleteSelectedMessageToolStripMenuItem
            // 
            this.deleteSelectedMessageToolStripMenuItem.Name = "deleteSelectedMessageToolStripMenuItem";
            this.deleteSelectedMessageToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.deleteSelectedMessageToolStripMenuItem.Text = "Delete Selected Message";
            this.deleteSelectedMessageToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedMessageToolStripMenuItem_Click);
            // 
            // deleteSelectedMessagesToolStripMenuItem
            // 
            this.deleteSelectedMessagesToolStripMenuItem.Name = "deleteSelectedMessagesToolStripMenuItem";
            this.deleteSelectedMessagesToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.deleteSelectedMessagesToolStripMenuItem.Text = "Delete Selected Messages";
            this.deleteSelectedMessagesToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedMessagesToolStripMenuItem_Click);
            // 
            // btnPurgeMessages
            // 
            this.btnPurgeMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPurgeMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnPurgeMessages.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnPurgeMessages.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnPurgeMessages.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnPurgeMessages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurgeMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPurgeMessages.Location = new System.Drawing.Point(200, 504);
            this.btnPurgeMessages.Name = "btnPurgeMessages";
            this.btnPurgeMessages.Size = new System.Drawing.Size(72, 24);
            this.btnPurgeMessages.TabIndex = 1;
            this.btnPurgeMessages.Text = "Purge";
            this.btnPurgeMessages.UseVisualStyleBackColor = false;
            this.btnPurgeMessages.Click += new System.EventHandler(this.btnPurgeMessages_Click);
            this.btnPurgeMessages.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnPurgeMessages.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // transferDeadletterContextMenuStrip
            // 
            this.transferDeadletterContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.transferDeadletterContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repairAndResubmitTransferDeadletterToolStripMenuItem,
            this.resubmitSelectedTransferDeadletterInBatchModeToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveSelectedTransferDeadletteredMessageToolStripMenuItem,
            this.saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem,
            this.saveSelectedTransferDeadletteredMessagesToolStripMenuItem,
            this.saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem});
            this.transferDeadletterContextMenuStrip.Name = "registrationContextMenuStrip";
            this.transferDeadletterContextMenuStrip.Size = new System.Drawing.Size(306, 142);
            this.transferDeadletterContextMenuStrip.Click += new System.EventHandler(this.resubmitSelectedTransferDeadletterMessagesInBatchModeToolStripMenuItem_Click);
            // 
            // repairAndResubmitTransferDeadletterToolStripMenuItem
            // 
            this.repairAndResubmitTransferDeadletterToolStripMenuItem.Name = "repairAndResubmitTransferDeadletterToolStripMenuItem";
            this.repairAndResubmitTransferDeadletterToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.repairAndResubmitTransferDeadletterToolStripMenuItem.Text = "Repair and Resubmit Selected Message";
            this.repairAndResubmitTransferDeadletterToolStripMenuItem.Click += new System.EventHandler(this.repairAndResubmitTransferDeadletterMessageToolStripMenuItem_Click);
            // 
            // resubmitSelectedTransferDeadletterInBatchModeToolStripMenuItem
            // 
            this.resubmitSelectedTransferDeadletterInBatchModeToolStripMenuItem.Name = "resubmitSelectedTransferDeadletterInBatchModeToolStripMenuItem";
            this.resubmitSelectedTransferDeadletterInBatchModeToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.resubmitSelectedTransferDeadletterInBatchModeToolStripMenuItem.Text = "Resubmit Selected Messages In Batch Mode";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(302, 6);
            // 
            // saveSelectedTransferDeadletteredMessageToolStripMenuItem
            // 
            this.saveSelectedTransferDeadletteredMessageToolStripMenuItem.Name = "saveSelectedTransferDeadletteredMessageToolStripMenuItem";
            this.saveSelectedTransferDeadletteredMessageToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedTransferDeadletteredMessageToolStripMenuItem.Text = "Save Selected Message";
            this.saveSelectedTransferDeadletteredMessageToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedTransferDeadletteredMessageToolStripMenuItem_Click);
            // 
            // saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem
            // 
            this.saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem.Name = "saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem";
            this.saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem.Text = "Save Selected Message Text as File";
            this.saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem_Click);
            // 
            // saveSelectedTransferDeadletteredMessagesToolStripMenuItem
            // 
            this.saveSelectedTransferDeadletteredMessagesToolStripMenuItem.Name = "saveSelectedTransferDeadletteredMessagesToolStripMenuItem";
            this.saveSelectedTransferDeadletteredMessagesToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedTransferDeadletteredMessagesToolStripMenuItem.Text = "Save Selected Messages";
            this.saveSelectedTransferDeadletteredMessagesToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedTransferDeadletteredMessagesToolStripMenuItem_Click);
            // 
            // saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem
            // 
            this.saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem.Name = "saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem";
            this.saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem.Text = "Save Selected Messages Text as File";
            this.saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem_Click);
            // 
            // HandleQueueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.btnTransferDeadletterQueue);
            this.Controls.Add(this.btnPurgeMessages);
            this.Controls.Add(this.btnPurgeDeadletterQueueMessages);
            this.Controls.Add(this.btnDeadletter);
            this.Controls.Add(this.btnMessages);
            this.Controls.Add(this.btnSessions);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnChangeStatus);
            this.Controls.Add(this.btnCancelUpdate);
            this.Controls.Add(this.btnCreateDelete);
            this.Name = "HandleQueueControl";
            this.Size = new System.Drawing.Size(1008, 544);
            this.mainTabControl.ResumeLayout(false);
            this.tabPageDescription.ResumeLayout(false);
            this.grouperAutoDeleteOnIdle.ResumeLayout(false);
            this.grouperQueueInformation.ResumeLayout(false);
            this.groupergrouperDefaultMessageTimeToLive.ResumeLayout(false);
            this.grouperQueueSettings.ResumeLayout(false);
            this.grouperQueueProperties.ResumeLayout(false);
            this.grouperQueueProperties.PerformLayout();
            this.grouperLockDuration.ResumeLayout(false);
            this.grouperDuplicateDetectionHistoryTimeWindow.ResumeLayout(false);
            this.grouperPath.ResumeLayout(false);
            this.grouperPath.PerformLayout();
            this.tabPageAuthorization.ResumeLayout(false);
            this.grouperAuthorizationRuleList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).EndInit();
            this.tabPageMessages.ResumeLayout(false);
            this.messagesSplitContainer.Panel1.ResumeLayout(false);
            this.messagesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesSplitContainer)).EndInit();
            this.messagesSplitContainer.ResumeLayout(false);
            this.messageMainSplitContainer.Panel1.ResumeLayout(false);
            this.messageMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageMainSplitContainer)).EndInit();
            this.messageMainSplitContainer.ResumeLayout(false);
            this.grouperMessageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictFindMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindMessagesByDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesDataGridView)).EndInit();
            this.grouperMessageText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMessageText)).EndInit();
            this.messagePropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.messagePropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagePropertiesSplitContainer)).EndInit();
            this.messagePropertiesSplitContainer.ResumeLayout(false);
            this.grouperMessageSystemProperties.ResumeLayout(false);
            this.grouperMessageCustomProperties.ResumeLayout(false);
            this.tabPageDeadletter.ResumeLayout(false);
            this.deadletterSplitContainer.Panel1.ResumeLayout(false);
            this.deadletterSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterSplitContainer)).EndInit();
            this.deadletterSplitContainer.ResumeLayout(false);
            this.deadletterMainSplitContainer.Panel1.ResumeLayout(false);
            this.deadletterMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterMainSplitContainer)).EndInit();
            this.deadletterMainSplitContainer.ResumeLayout(false);
            this.grouperDeadletterList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictFindDeadletter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindDeadletterByDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterDataGridView)).EndInit();
            this.grouperDeadletterText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDeadletterText)).EndInit();
            this.deadletterPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.deadletterPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterPropertiesSplitContainer)).EndInit();
            this.deadletterPropertiesSplitContainer.ResumeLayout(false);
            this.grouperDeadletterSystemProperties.ResumeLayout(false);
            this.grouperDeadletterCustomProperties.ResumeLayout(false);
            this.tabPageTransferDeadletter.ResumeLayout(false);
            this.transferDeadletterSplitContainer.Panel1.ResumeLayout(false);
            this.transferDeadletterSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.transferDeadletterSplitContainer)).EndInit();
            this.transferDeadletterSplitContainer.ResumeLayout(false);
            this.transferMainSplitContainer.Panel1.ResumeLayout(false);
            this.transferMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.transferMainSplitContainer)).EndInit();
            this.transferMainSplitContainer.ResumeLayout(false);
            this.grouperTransferDeadletterList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferDeadletterDataGridView)).EndInit();
            this.grouperTransferDeadletterText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTransferDeadletterText)).EndInit();
            this.transferDeadletterPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.transferDeadletterPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.transferDeadletterPropertiesSplitContainer)).EndInit();
            this.transferDeadletterPropertiesSplitContainer.ResumeLayout(false);
            this.grouperTransferDeadletterSystemProperties.ResumeLayout(false);
            this.grouperTransferDeadletterCustomProperties.ResumeLayout(false);
            this.tabPageSessions.ResumeLayout(false);
            this.sessionListStateSplitContainer.Panel1.ResumeLayout(false);
            this.sessionListStateSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionListStateSplitContainer)).EndInit();
            this.sessionListStateSplitContainer.ResumeLayout(false);
            this.sessionMainSplitContainer.Panel1.ResumeLayout(false);
            this.sessionMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionMainSplitContainer)).EndInit();
            this.sessionMainSplitContainer.ResumeLayout(false);
            this.grouperSessionList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionsDataGridView)).EndInit();
            this.grouperSessionState.ResumeLayout(false);
            this.grouperSessionState.PerformLayout();
            this.grouperSessionProperties.ResumeLayout(false);
            this.messagesContextMenuStrip.ResumeLayout(false);
            this.deadletterContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferDeadletterBindingSource)).EndInit();
            this.transferDeadletterContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Button btnCancelUpdate;
        private System.Windows.Forms.Button btnCreateDelete;
        private System.Windows.Forms.Button btnRefresh;
        internal System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPageDescription;
        private System.Windows.Forms.TabPage tabPageMessages;
        private Grouper grouperAutoDeleteOnIdle;
        private Grouper grouperQueueInformation;
        private System.Windows.Forms.ListView propertyListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private Grouper grouperQueueSettings;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private Grouper grouperQueueProperties;
        private System.Windows.Forms.Label lblMaxQueueSize;
        private CustomTrackBar trackBarMaxQueueSize;
        private System.Windows.Forms.TextBox txtUserMetadata;
        private System.Windows.Forms.Label lblUserMetadata;
        private System.Windows.Forms.Label lblMaxQueueSizeInGB;
        private System.Windows.Forms.Button btnOpenDescriptionForm;
        private System.Windows.Forms.Button btnOpenForwardToForm;
        private System.Windows.Forms.Label lblForwardTo;
        private System.Windows.Forms.TextBox txtForwardTo;
        private System.Windows.Forms.Label lblMaxDeliveryCount;
        private ServiceBusExplorer.Controls.NumericTextBox txtMaxDeliveryCount;
        private Grouper grouperLockDuration;
        private Grouper groupergrouperDefaultMessageTimeToLive;
        private Grouper grouperDuplicateDetectionHistoryTimeWindow;
        private Grouper grouperPath;
        private System.Windows.Forms.Label lblRelativeURI;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TabPage tabPageSessions;
        private System.Windows.Forms.SplitContainer messagesSplitContainer;
        private System.Windows.Forms.Button btnSessions;
        private System.Windows.Forms.Button btnMessages;
        private System.Windows.Forms.BindingSource messagesBindingSource;
        private System.Windows.Forms.SplitContainer messageMainSplitContainer;
        private Grouper grouperMessageList;
        private System.Windows.Forms.SplitContainer sessionListStateSplitContainer;
        private Grouper grouperSessionProperties;
        private System.Windows.Forms.PropertyGrid sessionPropertyGrid;
        private System.Windows.Forms.BindingSource sessionsBindingSource;
        private System.Windows.Forms.DataGridView messagesDataGridView;
        private System.Windows.Forms.Button btnDeadletter;
        private System.Windows.Forms.TabPage tabPageDeadletter;
        private System.Windows.Forms.SplitContainer deadletterSplitContainer;
        private System.Windows.Forms.SplitContainer deadletterMainSplitContainer;
        private Grouper grouperDeadletterList;
        private System.Windows.Forms.DataGridView deadletterDataGridView;
        private System.Windows.Forms.BindingSource deadletterBindingSource;
        private System.Windows.Forms.SplitContainer sessionMainSplitContainer;
        private Grouper grouperSessionList;
        private System.Windows.Forms.DataGridView sessionsDataGridView;
        private Grouper grouperSessionState;
        private System.Windows.Forms.TextBox txtSessionState;
        private System.Windows.Forms.TabPage tabPageAuthorization;
        private System.Windows.Forms.BindingSource authorizationRulesBindingSource;
        private Grouper grouperAuthorizationRuleList;
        private System.Windows.Forms.DataGridView authorizationRulesDataGridView;
        private System.Windows.Forms.ContextMenuStrip messagesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem repairAndResubmitMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitSelectedMessagesInBatchModeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip deadletterContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem repairAndResubmitDeadletterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitSelectedDeadletterInBatchModeToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictFindMessages;
        private System.Windows.Forms.PictureBox pictFindDeadletter;
        private System.Windows.Forms.PictureBox pictFindMessagesByDate;
        private System.Windows.Forms.PictureBox pictFindDeadletterByDate;
        private System.Windows.Forms.Button btnOpenForwardDeadLetteredMessagesToForm;
        private System.Windows.Forms.Label lblForwardDeadLetteredMessagesTo;
        private System.Windows.Forms.TextBox txtForwardDeadLetteredMessagesTo;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedMessagesToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedDeadletteredMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedDeadletteredMessagesToolStripMenuItem;
        private System.Windows.Forms.Button btnPurgeDeadletterQueueMessages;
        private System.Windows.Forms.Button btnPurgeMessages;
        private System.Windows.Forms.TabPage tabPageTransferDeadletter;
        private System.Windows.Forms.Button btnTransferDeadletterQueue;
        private System.Windows.Forms.SplitContainer transferDeadletterSplitContainer;
        private System.Windows.Forms.SplitContainer transferMainSplitContainer;
        private Grouper grouperTransferDeadletterList;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView transferDeadletterDataGridView;
        private System.Windows.Forms.BindingSource transferDeadletterBindingSource;
        private System.Windows.Forms.ContextMenuStrip transferDeadletterContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem repairAndResubmitTransferDeadletterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitSelectedTransferDeadletterInBatchModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedTransferDeadletteredMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedTransferDeadletteredMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedMessagesToolStripMenuItem;
        private Grouper grouperMessageText;
        private FastColoredTextBoxNS.FastColoredTextBox txtMessageText;
        private System.Windows.Forms.SplitContainer messagePropertiesSplitContainer;
        private Grouper grouperMessageSystemProperties;
        private System.Windows.Forms.PropertyGrid messagePropertyGrid;
        private Grouper grouperMessageCustomProperties;
        private System.Windows.Forms.PropertyGrid deadletterPropertyGrid;
        private System.Windows.Forms.SplitContainer deadletterPropertiesSplitContainer;
        private Grouper grouperDeadletterSystemProperties;
        private Grouper grouperDeadletterText;
        private FastColoredTextBoxNS.FastColoredTextBox txtDeadletterText;
        private Grouper grouperDeadletterCustomProperties;
        private System.Windows.Forms.SplitContainer transferDeadletterPropertiesSplitContainer;
        private Grouper grouperTransferDeadletterSystemProperties;
        private System.Windows.Forms.PropertyGrid transferDeadletterPropertyGrid;
        private Grouper grouperTransferDeadletterText;
        private FastColoredTextBoxNS.FastColoredTextBox txtTransferDeadletterText;
        private Grouper grouperTransferDeadletterCustomProperties;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedMessageBodyAsFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedMessagesBodyAsFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem;
        private TimeSpanControl tsAutoDeleteOnIdle;
        private TimeSpanControl tsDefaultMessageTimeToLive;
        private TimeSpanControl tsLockDuration;
        private TimeSpanControl tsDuplicateDetectionHistoryTimeWindow;
        private System.Windows.Forms.PropertyGrid messageCustomPropertyGrid;
        private System.Windows.Forms.PropertyGrid deadletterCustomPropertyGrid;
        private System.Windows.Forms.PropertyGrid transferDeadletterCustomPropertyGrid;
    }
}
