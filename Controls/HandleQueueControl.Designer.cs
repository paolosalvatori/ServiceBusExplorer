namespace Microsoft.Azure.ServiceBusExplorer.Controls
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.btnCancelUpdate = new System.Windows.Forms.Button();
            this.btnCreateDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.grouperAutoDeleteOnIdle = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.lblAutoDeleteOnIdleMilliseconds = new System.Windows.Forms.Label();
            this.txtAutoDeleteOnIdleMilliseconds = new System.Windows.Forms.TextBox();
            this.lblAutoDeleteOnIdleSeconds = new System.Windows.Forms.Label();
            this.txtAutoDeleteOnIdleSeconds = new System.Windows.Forms.TextBox();
            this.lblAutoDeleteOnIdleMinutes = new System.Windows.Forms.Label();
            this.txtAutoDeleteOnIdleMinutes = new System.Windows.Forms.TextBox();
            this.lblAutoDeleteOnIdleHours = new System.Windows.Forms.Label();
            this.lblAutoDeleteOnIdleDays = new System.Windows.Forms.Label();
            this.txtAutoDeleteOnIdleHours = new System.Windows.Forms.TextBox();
            this.txtAutoDeleteOnIdleDays = new System.Windows.Forms.TextBox();
            this.grouperQueueInformation = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupergrouperDefaultMessageTimeToLive = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.lblDefaultMessageTimeToLiveMilliseconds = new System.Windows.Forms.Label();
            this.txtDefaultMessageTimeToLiveMilliseconds = new System.Windows.Forms.TextBox();
            this.lblDefaultMessageTimeToLiveSeconds = new System.Windows.Forms.Label();
            this.txtDefaultMessageTimeToLiveSeconds = new System.Windows.Forms.TextBox();
            this.lblDefaultMessageTimeToLiveMinutes = new System.Windows.Forms.Label();
            this.txtDefaultMessageTimeToLiveMinutes = new System.Windows.Forms.TextBox();
            this.lbllblDefaultMessageTimeToLiveHours = new System.Windows.Forms.Label();
            this.lblDefaultMessageTimeToLiveDays = new System.Windows.Forms.Label();
            this.txtDefaultMessageTimeToLiveHours = new System.Windows.Forms.TextBox();
            this.txtDefaultMessageTimeToLiveDays = new System.Windows.Forms.TextBox();
            this.grouperQueueSettings = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.grouperQueueProperties = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.btnOpenForwardDeadLetteredMessagesToForm = new System.Windows.Forms.Button();
            this.lblForwardDeadLetteredMessagesTo = new System.Windows.Forms.Label();
            this.txtForwardDeadLetteredMessagesTo = new System.Windows.Forms.TextBox();
            this.lblMaxQueueSize = new System.Windows.Forms.Label();
            this.trackBarMaxQueueSize = new Microsoft.Azure.ServiceBusExplorer.Controls.CustomTrackBar();
            this.txtUserMetadata = new System.Windows.Forms.TextBox();
            this.lblUserMetadata = new System.Windows.Forms.Label();
            this.lblMaxQueueSizeInGB = new System.Windows.Forms.Label();
            this.btnOpenDescriptionForm = new System.Windows.Forms.Button();
            this.btnOpenForwardToForm = new System.Windows.Forms.Button();
            this.lblForwardTo = new System.Windows.Forms.Label();
            this.txtForwardTo = new System.Windows.Forms.TextBox();
            this.lblMaxDeliveryCount = new System.Windows.Forms.Label();
            this.txtMaxDeliveryCount = new System.Windows.Forms.TextBox();
            this.grouperLockDuration = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.lblLockDurationMilliseconds = new System.Windows.Forms.Label();
            this.txtLockDurationMilliseconds = new System.Windows.Forms.TextBox();
            this.lblLockDurationSeconds = new System.Windows.Forms.Label();
            this.txtLockDurationSeconds = new System.Windows.Forms.TextBox();
            this.lblLockDurationMinutes = new System.Windows.Forms.Label();
            this.txtLockDurationMinutes = new System.Windows.Forms.TextBox();
            this.lblLockDurationHours = new System.Windows.Forms.Label();
            this.lblLockDurationDays = new System.Windows.Forms.Label();
            this.txtLockDurationHours = new System.Windows.Forms.TextBox();
            this.txtLockDurationDays = new System.Windows.Forms.TextBox();
            this.grouperDuplicateDetectionHistoryTimeWindow = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds = new System.Windows.Forms.Label();
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds = new System.Windows.Forms.TextBox();
            this.lblDuplicateDetectionHistoryTimeWindowSeconds = new System.Windows.Forms.Label();
            this.txtDuplicateDetectionHistoryTimeWindowSeconds = new System.Windows.Forms.TextBox();
            this.lblDuplicateDetectionHistoryTimeWindowMinutes = new System.Windows.Forms.Label();
            this.txtDuplicateDetectionHistoryTimeWindowMinutes = new System.Windows.Forms.TextBox();
            this.lblDuplicateDetectionHistoryTimeWindowHours = new System.Windows.Forms.Label();
            this.lblDuplicateDetectionHistoryTimeWindowDays = new System.Windows.Forms.Label();
            this.txtDuplicateDetectionHistoryTimeWindowHours = new System.Windows.Forms.TextBox();
            this.txtDuplicateDetectionHistoryTimeWindowDays = new System.Windows.Forms.TextBox();
            this.grouperPath = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.lblRelativeURI = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.tabPageAuthorization = new System.Windows.Forms.TabPage();
            this.grouperAuthorizationRuleList = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.authorizationRulesDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPageMetrics = new System.Windows.Forms.TabPage();
            this.grouperDatapoints = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.dataPointDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.messagesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.messageListTextPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageList = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.pictFindMessages = new System.Windows.Forms.PictureBox();
            this.messagesDataGridView = new System.Windows.Forms.DataGridView();
            this.messagesCustomPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageText = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.grouperMessageCustomProperties = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.messagePropertyListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grouperMessageProperties = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.messagePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPageDeadletter = new System.Windows.Forms.TabPage();
            this.deadletterSplitContainer = new System.Windows.Forms.SplitContainer();
            this.deadletterListTextPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperDeadletterList = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.pictFindDeadletter = new System.Windows.Forms.PictureBox();
            this.deadletterDataGridView = new System.Windows.Forms.DataGridView();
            this.deadletterCustomPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperDeadletterText = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.txtDeadletterText = new System.Windows.Forms.TextBox();
            this.grouperDeadletterCustomProperties = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.deadletterPropertyListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grouperDeadletterProperties = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.deadletterPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPageSessions = new System.Windows.Forms.TabPage();
            this.sessionsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.sessionListTextPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperSessionList = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.sessionsDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperSessionState = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.txtSessionState = new System.Windows.Forms.TextBox();
            this.grouperSessionProperties = new Microsoft.Azure.ServiceBusExplorer.Controls.Grouper();
            this.sessionPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.btnSessions = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.btnDeadletter = new System.Windows.Forms.Button();
            this.messagesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.repairAndResubmitMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deadletterContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.repairAndResubmitDeadletterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedDeadletteredMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedDeadletteredMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sessionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.deadletterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.authorizationRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnMetrics = new System.Windows.Forms.Button();
            this.btnCloseTabs = new System.Windows.Forms.Button();
            this.btnPurgeDeadletterQueueMessages = new System.Windows.Forms.Button();
            this.btnPurgeMessages = new System.Windows.Forms.Button();
            this.tabPageTransferDeadletter = new System.Windows.Forms.TabPage();
            this.btnTransferDlq = new System.Windows.Forms.Button();
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
            this.tabPageMetrics.SuspendLayout();
            this.grouperDatapoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointDataGridView)).BeginInit();
            this.tabPageMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesSplitContainer)).BeginInit();
            this.messagesSplitContainer.Panel1.SuspendLayout();
            this.messagesSplitContainer.Panel2.SuspendLayout();
            this.messagesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).BeginInit();
            this.messageListTextPropertiesSplitContainer.Panel1.SuspendLayout();
            this.messageListTextPropertiesSplitContainer.Panel2.SuspendLayout();
            this.messageListTextPropertiesSplitContainer.SuspendLayout();
            this.grouperMessageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesCustomPropertiesSplitContainer)).BeginInit();
            this.messagesCustomPropertiesSplitContainer.Panel1.SuspendLayout();
            this.messagesCustomPropertiesSplitContainer.Panel2.SuspendLayout();
            this.messagesCustomPropertiesSplitContainer.SuspendLayout();
            this.grouperMessageText.SuspendLayout();
            this.grouperMessageCustomProperties.SuspendLayout();
            this.grouperMessageProperties.SuspendLayout();
            this.tabPageDeadletter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterSplitContainer)).BeginInit();
            this.deadletterSplitContainer.Panel1.SuspendLayout();
            this.deadletterSplitContainer.Panel2.SuspendLayout();
            this.deadletterSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterListTextPropertiesSplitContainer)).BeginInit();
            this.deadletterListTextPropertiesSplitContainer.Panel1.SuspendLayout();
            this.deadletterListTextPropertiesSplitContainer.Panel2.SuspendLayout();
            this.deadletterListTextPropertiesSplitContainer.SuspendLayout();
            this.grouperDeadletterList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindDeadletter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterCustomPropertiesSplitContainer)).BeginInit();
            this.deadletterCustomPropertiesSplitContainer.Panel1.SuspendLayout();
            this.deadletterCustomPropertiesSplitContainer.Panel2.SuspendLayout();
            this.deadletterCustomPropertiesSplitContainer.SuspendLayout();
            this.grouperDeadletterText.SuspendLayout();
            this.grouperDeadletterCustomProperties.SuspendLayout();
            this.grouperDeadletterProperties.SuspendLayout();
            this.tabPageSessions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsSplitContainer)).BeginInit();
            this.sessionsSplitContainer.Panel1.SuspendLayout();
            this.sessionsSplitContainer.Panel2.SuspendLayout();
            this.sessionsSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionListTextPropertiesSplitContainer)).BeginInit();
            this.sessionListTextPropertiesSplitContainer.Panel1.SuspendLayout();
            this.sessionListTextPropertiesSplitContainer.Panel2.SuspendLayout();
            this.sessionListTextPropertiesSplitContainer.SuspendLayout();
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
            this.SuspendLayout();
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
            this.btnChangeStatus.Location = new System.Drawing.Point(1140, 775);
            this.btnChangeStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(108, 37);
            this.btnChangeStatus.TabIndex = 6;
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
            this.btnCancelUpdate.Location = new System.Drawing.Point(1380, 775);
            this.btnCancelUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelUpdate.Name = "btnCancelUpdate";
            this.btnCancelUpdate.Size = new System.Drawing.Size(108, 37);
            this.btnCancelUpdate.TabIndex = 8;
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
            this.btnCreateDelete.Location = new System.Drawing.Point(1260, 775);
            this.btnCreateDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCreateDelete.Name = "btnCreateDelete";
            this.btnCreateDelete.Size = new System.Drawing.Size(108, 37);
            this.btnCreateDelete.TabIndex = 7;
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
            this.btnRefresh.Location = new System.Drawing.Point(1020, 775);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(108, 37);
            this.btnRefresh.TabIndex = 5;
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
            this.mainTabControl.Controls.Add(this.tabPageMetrics);
            this.mainTabControl.Controls.Add(this.tabPageMessages);
            this.mainTabControl.Controls.Add(this.tabPageDeadletter);
            this.mainTabControl.Controls.Add(this.tabPageTransferDeadletter);
            this.mainTabControl.Controls.Add(this.tabPageSessions);
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.ItemSize = new System.Drawing.Size(76, 18);
            this.mainTabControl.Location = new System.Drawing.Point(24, 25);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1464, 738);
            this.mainTabControl.TabIndex = 11;
            this.mainTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mainTabControl_DrawItem);
            this.mainTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.mainTabControl_Selected);
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
            this.tabPageDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(1456, 712);
            this.tabPageDescription.TabIndex = 2;
            this.tabPageDescription.Text = "Description";
            // 
            // grouperAutoDeleteOnIdle
            // 
            this.grouperAutoDeleteOnIdle.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperAutoDeleteOnIdle.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperAutoDeleteOnIdle.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperAutoDeleteOnIdle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAutoDeleteOnIdle.BorderThickness = 1F;
            this.grouperAutoDeleteOnIdle.Controls.Add(this.lblAutoDeleteOnIdleMilliseconds);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.txtAutoDeleteOnIdleMilliseconds);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.lblAutoDeleteOnIdleSeconds);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.txtAutoDeleteOnIdleSeconds);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.lblAutoDeleteOnIdleMinutes);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.txtAutoDeleteOnIdleMinutes);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.lblAutoDeleteOnIdleHours);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.lblAutoDeleteOnIdleDays);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.txtAutoDeleteOnIdleHours);
            this.grouperAutoDeleteOnIdle.Controls.Add(this.txtAutoDeleteOnIdleDays);
            this.grouperAutoDeleteOnIdle.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAutoDeleteOnIdle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperAutoDeleteOnIdle.ForeColor = System.Drawing.Color.White;
            this.grouperAutoDeleteOnIdle.GroupImage = null;
            this.grouperAutoDeleteOnIdle.GroupTitle = "Auto Delete On Idle";
            this.grouperAutoDeleteOnIdle.Location = new System.Drawing.Point(492, 12);
            this.grouperAutoDeleteOnIdle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperAutoDeleteOnIdle.Name = "grouperAutoDeleteOnIdle";
            this.grouperAutoDeleteOnIdle.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperAutoDeleteOnIdle.PaintGroupBox = true;
            this.grouperAutoDeleteOnIdle.RoundCorners = 4;
            this.grouperAutoDeleteOnIdle.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAutoDeleteOnIdle.ShadowControl = false;
            this.grouperAutoDeleteOnIdle.ShadowThickness = 1;
            this.grouperAutoDeleteOnIdle.Size = new System.Drawing.Size(444, 123);
            this.grouperAutoDeleteOnIdle.TabIndex = 1;
            // 
            // lblAutoDeleteOnIdleMilliseconds
            // 
            this.lblAutoDeleteOnIdleMilliseconds.AutoSize = true;
            this.lblAutoDeleteOnIdleMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleMilliseconds.Location = new System.Drawing.Point(360, 43);
            this.lblAutoDeleteOnIdleMilliseconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleMilliseconds.Name = "lblAutoDeleteOnIdleMilliseconds";
            this.lblAutoDeleteOnIdleMilliseconds.Size = new System.Drawing.Size(80, 20);
            this.lblAutoDeleteOnIdleMilliseconds.TabIndex = 25;
            this.lblAutoDeleteOnIdleMilliseconds.Text = "Millisecs:";
            // 
            // txtAutoDeleteOnIdleMilliseconds
            // 
            this.txtAutoDeleteOnIdleMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleMilliseconds.Location = new System.Drawing.Point(360, 68);
            this.txtAutoDeleteOnIdleMilliseconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAutoDeleteOnIdleMilliseconds.Name = "txtAutoDeleteOnIdleMilliseconds";
            this.txtAutoDeleteOnIdleMilliseconds.Size = new System.Drawing.Size(58, 26);
            this.txtAutoDeleteOnIdleMilliseconds.TabIndex = 4;
            this.txtAutoDeleteOnIdleMilliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblAutoDeleteOnIdleSeconds
            // 
            this.lblAutoDeleteOnIdleSeconds.AutoSize = true;
            this.lblAutoDeleteOnIdleSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleSeconds.Location = new System.Drawing.Point(276, 43);
            this.lblAutoDeleteOnIdleSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleSeconds.Name = "lblAutoDeleteOnIdleSeconds";
            this.lblAutoDeleteOnIdleSeconds.Size = new System.Drawing.Size(79, 20);
            this.lblAutoDeleteOnIdleSeconds.TabIndex = 24;
            this.lblAutoDeleteOnIdleSeconds.Text = "Seconds:";
            // 
            // txtAutoDeleteOnIdleSeconds
            // 
            this.txtAutoDeleteOnIdleSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleSeconds.Location = new System.Drawing.Point(276, 68);
            this.txtAutoDeleteOnIdleSeconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAutoDeleteOnIdleSeconds.Name = "txtAutoDeleteOnIdleSeconds";
            this.txtAutoDeleteOnIdleSeconds.Size = new System.Drawing.Size(58, 26);
            this.txtAutoDeleteOnIdleSeconds.TabIndex = 3;
            this.txtAutoDeleteOnIdleSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblAutoDeleteOnIdleMinutes
            // 
            this.lblAutoDeleteOnIdleMinutes.AutoSize = true;
            this.lblAutoDeleteOnIdleMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleMinutes.Location = new System.Drawing.Point(192, 43);
            this.lblAutoDeleteOnIdleMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleMinutes.Name = "lblAutoDeleteOnIdleMinutes";
            this.lblAutoDeleteOnIdleMinutes.Size = new System.Drawing.Size(73, 20);
            this.lblAutoDeleteOnIdleMinutes.TabIndex = 23;
            this.lblAutoDeleteOnIdleMinutes.Text = "Minutes:";
            // 
            // txtAutoDeleteOnIdleMinutes
            // 
            this.txtAutoDeleteOnIdleMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleMinutes.Location = new System.Drawing.Point(192, 68);
            this.txtAutoDeleteOnIdleMinutes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAutoDeleteOnIdleMinutes.Name = "txtAutoDeleteOnIdleMinutes";
            this.txtAutoDeleteOnIdleMinutes.Size = new System.Drawing.Size(58, 26);
            this.txtAutoDeleteOnIdleMinutes.TabIndex = 2;
            this.txtAutoDeleteOnIdleMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblAutoDeleteOnIdleHours
            // 
            this.lblAutoDeleteOnIdleHours.AutoSize = true;
            this.lblAutoDeleteOnIdleHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleHours.Location = new System.Drawing.Point(108, 43);
            this.lblAutoDeleteOnIdleHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleHours.Name = "lblAutoDeleteOnIdleHours";
            this.lblAutoDeleteOnIdleHours.Size = new System.Drawing.Size(60, 20);
            this.lblAutoDeleteOnIdleHours.TabIndex = 22;
            this.lblAutoDeleteOnIdleHours.Text = "Hours:";
            // 
            // lblAutoDeleteOnIdleDays
            // 
            this.lblAutoDeleteOnIdleDays.AutoSize = true;
            this.lblAutoDeleteOnIdleDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleDays.Location = new System.Drawing.Point(24, 43);
            this.lblAutoDeleteOnIdleDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleDays.Name = "lblAutoDeleteOnIdleDays";
            this.lblAutoDeleteOnIdleDays.Size = new System.Drawing.Size(53, 20);
            this.lblAutoDeleteOnIdleDays.TabIndex = 21;
            this.lblAutoDeleteOnIdleDays.Text = "Days:";
            // 
            // txtAutoDeleteOnIdleHours
            // 
            this.txtAutoDeleteOnIdleHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleHours.Location = new System.Drawing.Point(108, 68);
            this.txtAutoDeleteOnIdleHours.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAutoDeleteOnIdleHours.Name = "txtAutoDeleteOnIdleHours";
            this.txtAutoDeleteOnIdleHours.Size = new System.Drawing.Size(58, 26);
            this.txtAutoDeleteOnIdleHours.TabIndex = 1;
            this.txtAutoDeleteOnIdleHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txtAutoDeleteOnIdleDays
            // 
            this.txtAutoDeleteOnIdleDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleDays.Location = new System.Drawing.Point(24, 68);
            this.txtAutoDeleteOnIdleDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAutoDeleteOnIdleDays.Name = "txtAutoDeleteOnIdleDays";
            this.txtAutoDeleteOnIdleDays.Size = new System.Drawing.Size(58, 26);
            this.txtAutoDeleteOnIdleDays.TabIndex = 0;
            this.txtAutoDeleteOnIdleDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // grouperQueueInformation
            // 
            this.grouperQueueInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperQueueInformation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperQueueInformation.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperQueueInformation.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperQueueInformation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueInformation.BorderThickness = 1F;
            this.grouperQueueInformation.Controls.Add(this.propertyListView);
            this.grouperQueueInformation.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperQueueInformation.ForeColor = System.Drawing.Color.White;
            this.grouperQueueInformation.GroupImage = null;
            this.grouperQueueInformation.GroupTitle = "Queue Information";
            this.grouperQueueInformation.Location = new System.Drawing.Point(960, 12);
            this.grouperQueueInformation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperQueueInformation.Name = "grouperQueueInformation";
            this.grouperQueueInformation.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperQueueInformation.PaintGroupBox = true;
            this.grouperQueueInformation.RoundCorners = 4;
            this.grouperQueueInformation.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperQueueInformation.ShadowControl = false;
            this.grouperQueueInformation.ShadowThickness = 1;
            this.grouperQueueInformation.Size = new System.Drawing.Size(468, 665);
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
            this.propertyListView.Location = new System.Drawing.Point(24, 49);
            this.propertyListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.propertyListView.Name = "propertyListView";
            this.propertyListView.OwnerDraw = true;
            this.propertyListView.Size = new System.Drawing.Size(418, 589);
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
            this.groupergrouperDefaultMessageTimeToLive.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.groupergrouperDefaultMessageTimeToLive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.groupergrouperDefaultMessageTimeToLive.BorderThickness = 1F;
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.lblDefaultMessageTimeToLiveMilliseconds);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.txtDefaultMessageTimeToLiveMilliseconds);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.lblDefaultMessageTimeToLiveSeconds);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.txtDefaultMessageTimeToLiveSeconds);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.lblDefaultMessageTimeToLiveMinutes);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.txtDefaultMessageTimeToLiveMinutes);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.lbllblDefaultMessageTimeToLiveHours);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.lblDefaultMessageTimeToLiveDays);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.txtDefaultMessageTimeToLiveHours);
            this.groupergrouperDefaultMessageTimeToLive.Controls.Add(this.txtDefaultMessageTimeToLiveDays);
            this.groupergrouperDefaultMessageTimeToLive.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.groupergrouperDefaultMessageTimeToLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupergrouperDefaultMessageTimeToLive.ForeColor = System.Drawing.Color.White;
            this.groupergrouperDefaultMessageTimeToLive.GroupImage = null;
            this.groupergrouperDefaultMessageTimeToLive.GroupTitle = "Default Message Time To Live";
            this.groupergrouperDefaultMessageTimeToLive.Location = new System.Drawing.Point(492, 148);
            this.groupergrouperDefaultMessageTimeToLive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupergrouperDefaultMessageTimeToLive.Name = "groupergrouperDefaultMessageTimeToLive";
            this.groupergrouperDefaultMessageTimeToLive.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.groupergrouperDefaultMessageTimeToLive.PaintGroupBox = true;
            this.groupergrouperDefaultMessageTimeToLive.RoundCorners = 4;
            this.groupergrouperDefaultMessageTimeToLive.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupergrouperDefaultMessageTimeToLive.ShadowControl = false;
            this.groupergrouperDefaultMessageTimeToLive.ShadowThickness = 1;
            this.groupergrouperDefaultMessageTimeToLive.Size = new System.Drawing.Size(444, 123);
            this.groupergrouperDefaultMessageTimeToLive.TabIndex = 2;
            // 
            // lblDefaultMessageTimeToLiveMilliseconds
            // 
            this.lblDefaultMessageTimeToLiveMilliseconds.AutoSize = true;
            this.lblDefaultMessageTimeToLiveMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDefaultMessageTimeToLiveMilliseconds.Location = new System.Drawing.Point(360, 43);
            this.lblDefaultMessageTimeToLiveMilliseconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultMessageTimeToLiveMilliseconds.Name = "lblDefaultMessageTimeToLiveMilliseconds";
            this.lblDefaultMessageTimeToLiveMilliseconds.Size = new System.Drawing.Size(80, 20);
            this.lblDefaultMessageTimeToLiveMilliseconds.TabIndex = 25;
            this.lblDefaultMessageTimeToLiveMilliseconds.Text = "Millisecs:";
            // 
            // txtDefaultMessageTimeToLiveMilliseconds
            // 
            this.txtDefaultMessageTimeToLiveMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveMilliseconds.Location = new System.Drawing.Point(360, 68);
            this.txtDefaultMessageTimeToLiveMilliseconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDefaultMessageTimeToLiveMilliseconds.Name = "txtDefaultMessageTimeToLiveMilliseconds";
            this.txtDefaultMessageTimeToLiveMilliseconds.Size = new System.Drawing.Size(58, 26);
            this.txtDefaultMessageTimeToLiveMilliseconds.TabIndex = 4;
            this.txtDefaultMessageTimeToLiveMilliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDefaultMessageTimeToLiveSeconds
            // 
            this.lblDefaultMessageTimeToLiveSeconds.AutoSize = true;
            this.lblDefaultMessageTimeToLiveSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDefaultMessageTimeToLiveSeconds.Location = new System.Drawing.Point(276, 43);
            this.lblDefaultMessageTimeToLiveSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultMessageTimeToLiveSeconds.Name = "lblDefaultMessageTimeToLiveSeconds";
            this.lblDefaultMessageTimeToLiveSeconds.Size = new System.Drawing.Size(79, 20);
            this.lblDefaultMessageTimeToLiveSeconds.TabIndex = 24;
            this.lblDefaultMessageTimeToLiveSeconds.Text = "Seconds:";
            // 
            // txtDefaultMessageTimeToLiveSeconds
            // 
            this.txtDefaultMessageTimeToLiveSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveSeconds.Location = new System.Drawing.Point(276, 68);
            this.txtDefaultMessageTimeToLiveSeconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDefaultMessageTimeToLiveSeconds.Name = "txtDefaultMessageTimeToLiveSeconds";
            this.txtDefaultMessageTimeToLiveSeconds.Size = new System.Drawing.Size(58, 26);
            this.txtDefaultMessageTimeToLiveSeconds.TabIndex = 3;
            this.txtDefaultMessageTimeToLiveSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDefaultMessageTimeToLiveMinutes
            // 
            this.lblDefaultMessageTimeToLiveMinutes.AutoSize = true;
            this.lblDefaultMessageTimeToLiveMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDefaultMessageTimeToLiveMinutes.Location = new System.Drawing.Point(192, 43);
            this.lblDefaultMessageTimeToLiveMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultMessageTimeToLiveMinutes.Name = "lblDefaultMessageTimeToLiveMinutes";
            this.lblDefaultMessageTimeToLiveMinutes.Size = new System.Drawing.Size(73, 20);
            this.lblDefaultMessageTimeToLiveMinutes.TabIndex = 23;
            this.lblDefaultMessageTimeToLiveMinutes.Text = "Minutes:";
            // 
            // txtDefaultMessageTimeToLiveMinutes
            // 
            this.txtDefaultMessageTimeToLiveMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveMinutes.Location = new System.Drawing.Point(192, 68);
            this.txtDefaultMessageTimeToLiveMinutes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDefaultMessageTimeToLiveMinutes.Name = "txtDefaultMessageTimeToLiveMinutes";
            this.txtDefaultMessageTimeToLiveMinutes.Size = new System.Drawing.Size(58, 26);
            this.txtDefaultMessageTimeToLiveMinutes.TabIndex = 2;
            this.txtDefaultMessageTimeToLiveMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lbllblDefaultMessageTimeToLiveHours
            // 
            this.lbllblDefaultMessageTimeToLiveHours.AutoSize = true;
            this.lbllblDefaultMessageTimeToLiveHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbllblDefaultMessageTimeToLiveHours.Location = new System.Drawing.Point(108, 43);
            this.lbllblDefaultMessageTimeToLiveHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbllblDefaultMessageTimeToLiveHours.Name = "lbllblDefaultMessageTimeToLiveHours";
            this.lbllblDefaultMessageTimeToLiveHours.Size = new System.Drawing.Size(60, 20);
            this.lbllblDefaultMessageTimeToLiveHours.TabIndex = 22;
            this.lbllblDefaultMessageTimeToLiveHours.Text = "Hours:";
            // 
            // lblDefaultMessageTimeToLiveDays
            // 
            this.lblDefaultMessageTimeToLiveDays.AutoSize = true;
            this.lblDefaultMessageTimeToLiveDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDefaultMessageTimeToLiveDays.Location = new System.Drawing.Point(24, 43);
            this.lblDefaultMessageTimeToLiveDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultMessageTimeToLiveDays.Name = "lblDefaultMessageTimeToLiveDays";
            this.lblDefaultMessageTimeToLiveDays.Size = new System.Drawing.Size(53, 20);
            this.lblDefaultMessageTimeToLiveDays.TabIndex = 21;
            this.lblDefaultMessageTimeToLiveDays.Text = "Days:";
            // 
            // txtDefaultMessageTimeToLiveHours
            // 
            this.txtDefaultMessageTimeToLiveHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveHours.Location = new System.Drawing.Point(108, 68);
            this.txtDefaultMessageTimeToLiveHours.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDefaultMessageTimeToLiveHours.Name = "txtDefaultMessageTimeToLiveHours";
            this.txtDefaultMessageTimeToLiveHours.Size = new System.Drawing.Size(58, 26);
            this.txtDefaultMessageTimeToLiveHours.TabIndex = 1;
            this.txtDefaultMessageTimeToLiveHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txtDefaultMessageTimeToLiveDays
            // 
            this.txtDefaultMessageTimeToLiveDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveDays.Location = new System.Drawing.Point(24, 68);
            this.txtDefaultMessageTimeToLiveDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDefaultMessageTimeToLiveDays.Name = "txtDefaultMessageTimeToLiveDays";
            this.txtDefaultMessageTimeToLiveDays.Size = new System.Drawing.Size(58, 26);
            this.txtDefaultMessageTimeToLiveDays.TabIndex = 0;
            this.txtDefaultMessageTimeToLiveDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // grouperQueueSettings
            // 
            this.grouperQueueSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperQueueSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperQueueSettings.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperQueueSettings.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperQueueSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueSettings.BorderThickness = 1F;
            this.grouperQueueSettings.Controls.Add(this.checkedListBox);
            this.grouperQueueSettings.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperQueueSettings.ForeColor = System.Drawing.Color.White;
            this.grouperQueueSettings.GroupImage = null;
            this.grouperQueueSettings.GroupTitle = "Queue Settings";
            this.grouperQueueSettings.Location = new System.Drawing.Point(492, 418);
            this.grouperQueueSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperQueueSettings.Name = "grouperQueueSettings";
            this.grouperQueueSettings.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperQueueSettings.PaintGroupBox = true;
            this.grouperQueueSettings.RoundCorners = 4;
            this.grouperQueueSettings.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperQueueSettings.ShadowControl = false;
            this.grouperQueueSettings.ShadowThickness = 1;
            this.grouperQueueSettings.Size = new System.Drawing.Size(444, 258);
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
            this.checkedListBox.Items.AddRange(new object[] {
            "Enable Batched Operations",
            "Enable Dead Lettering On Message Expiration",
            "Enable Partitioning",
            "Enable Express",
            "Requires Duplicate Detection",
            "Requires Session",
            "Enforce Message Ordering",
            "Is Anonymous Accessible"});
            this.checkedListBox.Location = new System.Drawing.Point(24, 68);
            this.checkedListBox.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(394, 130);
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
            this.grouperQueueProperties.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
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
            this.grouperQueueProperties.Location = new System.Drawing.Point(24, 283);
            this.grouperQueueProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperQueueProperties.Name = "grouperQueueProperties";
            this.grouperQueueProperties.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperQueueProperties.PaintGroupBox = true;
            this.grouperQueueProperties.RoundCorners = 4;
            this.grouperQueueProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperQueueProperties.ShadowControl = false;
            this.grouperQueueProperties.ShadowThickness = 1;
            this.grouperQueueProperties.Size = new System.Drawing.Size(444, 394);
            this.grouperQueueProperties.TabIndex = 5;
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
            this.btnOpenForwardDeadLetteredMessagesToForm.Location = new System.Drawing.Point(384, 345);
            this.btnOpenForwardDeadLetteredMessagesToForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpenForwardDeadLetteredMessagesToForm.Name = "btnOpenForwardDeadLetteredMessagesToForm";
            this.btnOpenForwardDeadLetteredMessagesToForm.Size = new System.Drawing.Size(36, 32);
            this.btnOpenForwardDeadLetteredMessagesToForm.TabIndex = 36;
            this.btnOpenForwardDeadLetteredMessagesToForm.Text = "...";
            this.btnOpenForwardDeadLetteredMessagesToForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenForwardDeadLetteredMessagesToForm.UseVisualStyleBackColor = false;
            this.btnOpenForwardDeadLetteredMessagesToForm.Click += new System.EventHandler(this.btnOpenForwardDeadLetteredMessagesToForm_Click);
            // 
            // lblForwardDeadLetteredMessagesTo
            // 
            this.lblForwardDeadLetteredMessagesTo.AutoSize = true;
            this.lblForwardDeadLetteredMessagesTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblForwardDeadLetteredMessagesTo.Location = new System.Drawing.Point(24, 320);
            this.lblForwardDeadLetteredMessagesTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblForwardDeadLetteredMessagesTo.Name = "lblForwardDeadLetteredMessagesTo";
            this.lblForwardDeadLetteredMessagesTo.Size = new System.Drawing.Size(293, 20);
            this.lblForwardDeadLetteredMessagesTo.TabIndex = 37;
            this.lblForwardDeadLetteredMessagesTo.Text = "Forward Dead Lettered Messages To:";
            // 
            // txtForwardDeadLetteredMessagesTo
            // 
            this.txtForwardDeadLetteredMessagesTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtForwardDeadLetteredMessagesTo.BackColor = System.Drawing.SystemColors.Window;
            this.txtForwardDeadLetteredMessagesTo.Location = new System.Drawing.Point(24, 345);
            this.txtForwardDeadLetteredMessagesTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtForwardDeadLetteredMessagesTo.Name = "txtForwardDeadLetteredMessagesTo";
            this.txtForwardDeadLetteredMessagesTo.Size = new System.Drawing.Size(346, 26);
            this.txtForwardDeadLetteredMessagesTo.TabIndex = 35;
            // 
            // lblMaxQueueSize
            // 
            this.lblMaxQueueSize.AutoSize = true;
            this.lblMaxQueueSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxQueueSize.Location = new System.Drawing.Point(24, 43);
            this.lblMaxQueueSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxQueueSize.Name = "lblMaxQueueSize";
            this.lblMaxQueueSize.Size = new System.Drawing.Size(185, 20);
            this.lblMaxQueueSize.TabIndex = 24;
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
            this.trackBarMaxQueueSize.Location = new System.Drawing.Point(24, 62);
            this.trackBarMaxQueueSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trackBarMaxQueueSize.Maximum = 10;
            this.trackBarMaxQueueSize.Minimum = 1;
            this.trackBarMaxQueueSize.Name = "trackBarMaxQueueSize";
            this.trackBarMaxQueueSize.Size = new System.Drawing.Size(348, 29);
            this.trackBarMaxQueueSize.TabIndex = 34;
            this.trackBarMaxQueueSize.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMaxQueueSize.TickColor = System.Drawing.Color.Black;
            this.trackBarMaxQueueSize.TickHeight = 4;
            this.trackBarMaxQueueSize.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarMaxQueueSize.TrackerSize = new System.Drawing.Size(12, 12);
            this.trackBarMaxQueueSize.TrackLineBrushStyle = Microsoft.Azure.ServiceBusExplorer.Controls.BrushStyle.Solid;
            this.trackBarMaxQueueSize.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarMaxQueueSize.TrackLineHeight = 1;
            this.trackBarMaxQueueSize.Value = 1;
            this.trackBarMaxQueueSize.ValueChanged += new Microsoft.Azure.ServiceBusExplorer.Controls.ValueChangedHandler(this.trackBarMaxQueueSize_ValueChanged);
            // 
            // txtUserMetadata
            // 
            this.txtUserMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserMetadata.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserMetadata.Location = new System.Drawing.Point(24, 209);
            this.txtUserMetadata.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUserMetadata.Name = "txtUserMetadata";
            this.txtUserMetadata.Size = new System.Drawing.Size(346, 26);
            this.txtUserMetadata.TabIndex = 2;
            // 
            // lblUserMetadata
            // 
            this.lblUserMetadata.AutoSize = true;
            this.lblUserMetadata.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserMetadata.Location = new System.Drawing.Point(24, 185);
            this.lblUserMetadata.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserMetadata.Name = "lblUserMetadata";
            this.lblUserMetadata.Size = new System.Drawing.Size(141, 20);
            this.lblUserMetadata.TabIndex = 27;
            this.lblUserMetadata.Text = "User Description:";
            // 
            // lblMaxQueueSizeInGB
            // 
            this.lblMaxQueueSizeInGB.AutoSize = true;
            this.lblMaxQueueSizeInGB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxQueueSizeInGB.Location = new System.Drawing.Point(378, 74);
            this.lblMaxQueueSizeInGB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxQueueSizeInGB.Name = "lblMaxQueueSizeInGB";
            this.lblMaxQueueSizeInGB.Size = new System.Drawing.Size(48, 20);
            this.lblMaxQueueSizeInGB.TabIndex = 33;
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
            this.btnOpenDescriptionForm.Location = new System.Drawing.Point(384, 209);
            this.btnOpenDescriptionForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpenDescriptionForm.Name = "btnOpenDescriptionForm";
            this.btnOpenDescriptionForm.Size = new System.Drawing.Size(36, 32);
            this.btnOpenDescriptionForm.TabIndex = 3;
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
            this.btnOpenForwardToForm.Location = new System.Drawing.Point(384, 277);
            this.btnOpenForwardToForm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpenForwardToForm.Name = "btnOpenForwardToForm";
            this.btnOpenForwardToForm.Size = new System.Drawing.Size(36, 32);
            this.btnOpenForwardToForm.TabIndex = 5;
            this.btnOpenForwardToForm.Text = "...";
            this.btnOpenForwardToForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenForwardToForm.UseVisualStyleBackColor = false;
            this.btnOpenForwardToForm.Click += new System.EventHandler(this.btnOpenForwardToForm_Click);
            // 
            // lblForwardTo
            // 
            this.lblForwardTo.AutoSize = true;
            this.lblForwardTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblForwardTo.Location = new System.Drawing.Point(24, 252);
            this.lblForwardTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblForwardTo.Name = "lblForwardTo";
            this.lblForwardTo.Size = new System.Drawing.Size(99, 20);
            this.lblForwardTo.TabIndex = 31;
            this.lblForwardTo.Text = "Forward To:";
            // 
            // txtForwardTo
            // 
            this.txtForwardTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtForwardTo.BackColor = System.Drawing.SystemColors.Window;
            this.txtForwardTo.Location = new System.Drawing.Point(24, 277);
            this.txtForwardTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtForwardTo.Name = "txtForwardTo";
            this.txtForwardTo.Size = new System.Drawing.Size(346, 26);
            this.txtForwardTo.TabIndex = 4;
            // 
            // lblMaxDeliveryCount
            // 
            this.lblMaxDeliveryCount.AutoSize = true;
            this.lblMaxDeliveryCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxDeliveryCount.Location = new System.Drawing.Point(24, 111);
            this.lblMaxDeliveryCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxDeliveryCount.Name = "lblMaxDeliveryCount";
            this.lblMaxDeliveryCount.Size = new System.Drawing.Size(160, 20);
            this.lblMaxDeliveryCount.TabIndex = 26;
            this.lblMaxDeliveryCount.Text = "Max Delivery Count:";
            // 
            // txtMaxDeliveryCount
            // 
            this.txtMaxDeliveryCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxDeliveryCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaxDeliveryCount.Location = new System.Drawing.Point(24, 135);
            this.txtMaxDeliveryCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaxDeliveryCount.Name = "txtMaxDeliveryCount";
            this.txtMaxDeliveryCount.Size = new System.Drawing.Size(346, 26);
            this.txtMaxDeliveryCount.TabIndex = 0;
            this.txtMaxDeliveryCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // grouperLockDuration
            // 
            this.grouperLockDuration.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperLockDuration.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperLockDuration.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperLockDuration.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperLockDuration.BorderThickness = 1F;
            this.grouperLockDuration.Controls.Add(this.lblLockDurationMilliseconds);
            this.grouperLockDuration.Controls.Add(this.txtLockDurationMilliseconds);
            this.grouperLockDuration.Controls.Add(this.lblLockDurationSeconds);
            this.grouperLockDuration.Controls.Add(this.txtLockDurationSeconds);
            this.grouperLockDuration.Controls.Add(this.lblLockDurationMinutes);
            this.grouperLockDuration.Controls.Add(this.txtLockDurationMinutes);
            this.grouperLockDuration.Controls.Add(this.lblLockDurationHours);
            this.grouperLockDuration.Controls.Add(this.lblLockDurationDays);
            this.grouperLockDuration.Controls.Add(this.txtLockDurationHours);
            this.grouperLockDuration.Controls.Add(this.txtLockDurationDays);
            this.grouperLockDuration.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperLockDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperLockDuration.ForeColor = System.Drawing.Color.White;
            this.grouperLockDuration.GroupImage = null;
            this.grouperLockDuration.GroupTitle = "Lock Duration";
            this.grouperLockDuration.Location = new System.Drawing.Point(492, 283);
            this.grouperLockDuration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperLockDuration.Name = "grouperLockDuration";
            this.grouperLockDuration.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperLockDuration.PaintGroupBox = true;
            this.grouperLockDuration.RoundCorners = 4;
            this.grouperLockDuration.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperLockDuration.ShadowControl = false;
            this.grouperLockDuration.ShadowThickness = 1;
            this.grouperLockDuration.Size = new System.Drawing.Size(444, 123);
            this.grouperLockDuration.TabIndex = 4;
            // 
            // lblLockDurationMilliseconds
            // 
            this.lblLockDurationMilliseconds.AutoSize = true;
            this.lblLockDurationMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLockDurationMilliseconds.Location = new System.Drawing.Point(360, 43);
            this.lblLockDurationMilliseconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLockDurationMilliseconds.Name = "lblLockDurationMilliseconds";
            this.lblLockDurationMilliseconds.Size = new System.Drawing.Size(80, 20);
            this.lblLockDurationMilliseconds.TabIndex = 25;
            this.lblLockDurationMilliseconds.Text = "Millisecs:";
            // 
            // txtLockDurationMilliseconds
            // 
            this.txtLockDurationMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtLockDurationMilliseconds.Location = new System.Drawing.Point(360, 68);
            this.txtLockDurationMilliseconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLockDurationMilliseconds.Name = "txtLockDurationMilliseconds";
            this.txtLockDurationMilliseconds.Size = new System.Drawing.Size(58, 26);
            this.txtLockDurationMilliseconds.TabIndex = 4;
            this.txtLockDurationMilliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblLockDurationSeconds
            // 
            this.lblLockDurationSeconds.AutoSize = true;
            this.lblLockDurationSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLockDurationSeconds.Location = new System.Drawing.Point(276, 43);
            this.lblLockDurationSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLockDurationSeconds.Name = "lblLockDurationSeconds";
            this.lblLockDurationSeconds.Size = new System.Drawing.Size(79, 20);
            this.lblLockDurationSeconds.TabIndex = 24;
            this.lblLockDurationSeconds.Text = "Seconds:";
            // 
            // txtLockDurationSeconds
            // 
            this.txtLockDurationSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtLockDurationSeconds.Location = new System.Drawing.Point(276, 68);
            this.txtLockDurationSeconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLockDurationSeconds.Name = "txtLockDurationSeconds";
            this.txtLockDurationSeconds.Size = new System.Drawing.Size(58, 26);
            this.txtLockDurationSeconds.TabIndex = 3;
            this.txtLockDurationSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblLockDurationMinutes
            // 
            this.lblLockDurationMinutes.AutoSize = true;
            this.lblLockDurationMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLockDurationMinutes.Location = new System.Drawing.Point(192, 43);
            this.lblLockDurationMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLockDurationMinutes.Name = "lblLockDurationMinutes";
            this.lblLockDurationMinutes.Size = new System.Drawing.Size(73, 20);
            this.lblLockDurationMinutes.TabIndex = 23;
            this.lblLockDurationMinutes.Text = "Minutes:";
            // 
            // txtLockDurationMinutes
            // 
            this.txtLockDurationMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtLockDurationMinutes.Location = new System.Drawing.Point(192, 68);
            this.txtLockDurationMinutes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLockDurationMinutes.Name = "txtLockDurationMinutes";
            this.txtLockDurationMinutes.Size = new System.Drawing.Size(58, 26);
            this.txtLockDurationMinutes.TabIndex = 2;
            this.txtLockDurationMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblLockDurationHours
            // 
            this.lblLockDurationHours.AutoSize = true;
            this.lblLockDurationHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLockDurationHours.Location = new System.Drawing.Point(108, 43);
            this.lblLockDurationHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLockDurationHours.Name = "lblLockDurationHours";
            this.lblLockDurationHours.Size = new System.Drawing.Size(60, 20);
            this.lblLockDurationHours.TabIndex = 22;
            this.lblLockDurationHours.Text = "Hours:";
            // 
            // lblLockDurationDays
            // 
            this.lblLockDurationDays.AutoSize = true;
            this.lblLockDurationDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLockDurationDays.Location = new System.Drawing.Point(24, 43);
            this.lblLockDurationDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLockDurationDays.Name = "lblLockDurationDays";
            this.lblLockDurationDays.Size = new System.Drawing.Size(53, 20);
            this.lblLockDurationDays.TabIndex = 21;
            this.lblLockDurationDays.Text = "Days:";
            // 
            // txtLockDurationHours
            // 
            this.txtLockDurationHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtLockDurationHours.Location = new System.Drawing.Point(108, 68);
            this.txtLockDurationHours.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLockDurationHours.Name = "txtLockDurationHours";
            this.txtLockDurationHours.Size = new System.Drawing.Size(58, 26);
            this.txtLockDurationHours.TabIndex = 1;
            this.txtLockDurationHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txtLockDurationDays
            // 
            this.txtLockDurationDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtLockDurationDays.Location = new System.Drawing.Point(24, 68);
            this.txtLockDurationDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLockDurationDays.Name = "txtLockDurationDays";
            this.txtLockDurationDays.Size = new System.Drawing.Size(58, 26);
            this.txtLockDurationDays.TabIndex = 0;
            this.txtLockDurationDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // grouperDuplicateDetectionHistoryTimeWindow
            // 
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDuplicateDetectionHistoryTimeWindow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDuplicateDetectionHistoryTimeWindow.BorderThickness = 1F;
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.lblDuplicateDetectionHistoryTimeWindowMilliseconds);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.txtDuplicateDetectionHistoryTimeWindowMilliseconds);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.lblDuplicateDetectionHistoryTimeWindowSeconds);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.txtDuplicateDetectionHistoryTimeWindowSeconds);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.lblDuplicateDetectionHistoryTimeWindowMinutes);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.txtDuplicateDetectionHistoryTimeWindowMinutes);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.lblDuplicateDetectionHistoryTimeWindowHours);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.lblDuplicateDetectionHistoryTimeWindowDays);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.txtDuplicateDetectionHistoryTimeWindowHours);
            this.grouperDuplicateDetectionHistoryTimeWindow.Controls.Add(this.txtDuplicateDetectionHistoryTimeWindowDays);
            this.grouperDuplicateDetectionHistoryTimeWindow.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDuplicateDetectionHistoryTimeWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDuplicateDetectionHistoryTimeWindow.ForeColor = System.Drawing.Color.White;
            this.grouperDuplicateDetectionHistoryTimeWindow.GroupImage = null;
            this.grouperDuplicateDetectionHistoryTimeWindow.GroupTitle = "Duplicate Detection History Time Window";
            this.grouperDuplicateDetectionHistoryTimeWindow.Location = new System.Drawing.Point(24, 148);
            this.grouperDuplicateDetectionHistoryTimeWindow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperDuplicateDetectionHistoryTimeWindow.Name = "grouperDuplicateDetectionHistoryTimeWindow";
            this.grouperDuplicateDetectionHistoryTimeWindow.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperDuplicateDetectionHistoryTimeWindow.PaintGroupBox = true;
            this.grouperDuplicateDetectionHistoryTimeWindow.RoundCorners = 4;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowControl = false;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowThickness = 1;
            this.grouperDuplicateDetectionHistoryTimeWindow.Size = new System.Drawing.Size(444, 123);
            this.grouperDuplicateDetectionHistoryTimeWindow.TabIndex = 3;
            // 
            // lblDuplicateDetectionHistoryTimeWindowMilliseconds
            // 
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Location = new System.Drawing.Point(360, 43);
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Name = "lblDuplicateDetectionHistoryTimeWindowMilliseconds";
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Size = new System.Drawing.Size(80, 20);
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.TabIndex = 25;
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Text = "Millisecs:";
            // 
            // txtDuplicateDetectionHistoryTimeWindowMilliseconds
            // 
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.Location = new System.Drawing.Point(360, 68);
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.Name = "txtDuplicateDetectionHistoryTimeWindowMilliseconds";
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.Size = new System.Drawing.Size(58, 26);
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.TabIndex = 4;
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDuplicateDetectionHistoryTimeWindowSeconds
            // 
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Location = new System.Drawing.Point(276, 43);
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Name = "lblDuplicateDetectionHistoryTimeWindowSeconds";
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Size = new System.Drawing.Size(79, 20);
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.TabIndex = 24;
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Text = "Seconds:";
            // 
            // txtDuplicateDetectionHistoryTimeWindowSeconds
            // 
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.Location = new System.Drawing.Point(276, 68);
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.Name = "txtDuplicateDetectionHistoryTimeWindowSeconds";
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.Size = new System.Drawing.Size(58, 26);
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.TabIndex = 3;
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDuplicateDetectionHistoryTimeWindowMinutes
            // 
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Location = new System.Drawing.Point(192, 43);
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Name = "lblDuplicateDetectionHistoryTimeWindowMinutes";
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Size = new System.Drawing.Size(73, 20);
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.TabIndex = 23;
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Text = "Minutes:";
            // 
            // txtDuplicateDetectionHistoryTimeWindowMinutes
            // 
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.Location = new System.Drawing.Point(192, 68);
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.Name = "txtDuplicateDetectionHistoryTimeWindowMinutes";
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.Size = new System.Drawing.Size(58, 26);
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.TabIndex = 2;
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDuplicateDetectionHistoryTimeWindowHours
            // 
            this.lblDuplicateDetectionHistoryTimeWindowHours.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowHours.Location = new System.Drawing.Point(108, 43);
            this.lblDuplicateDetectionHistoryTimeWindowHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowHours.Name = "lblDuplicateDetectionHistoryTimeWindowHours";
            this.lblDuplicateDetectionHistoryTimeWindowHours.Size = new System.Drawing.Size(60, 20);
            this.lblDuplicateDetectionHistoryTimeWindowHours.TabIndex = 22;
            this.lblDuplicateDetectionHistoryTimeWindowHours.Text = "Hours:";
            // 
            // lblDuplicateDetectionHistoryTimeWindowDays
            // 
            this.lblDuplicateDetectionHistoryTimeWindowDays.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowDays.Location = new System.Drawing.Point(24, 43);
            this.lblDuplicateDetectionHistoryTimeWindowDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowDays.Name = "lblDuplicateDetectionHistoryTimeWindowDays";
            this.lblDuplicateDetectionHistoryTimeWindowDays.Size = new System.Drawing.Size(53, 20);
            this.lblDuplicateDetectionHistoryTimeWindowDays.TabIndex = 21;
            this.lblDuplicateDetectionHistoryTimeWindowDays.Text = "Days:";
            // 
            // txtDuplicateDetectionHistoryTimeWindowHours
            // 
            this.txtDuplicateDetectionHistoryTimeWindowHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowHours.Location = new System.Drawing.Point(108, 68);
            this.txtDuplicateDetectionHistoryTimeWindowHours.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDuplicateDetectionHistoryTimeWindowHours.Name = "txtDuplicateDetectionHistoryTimeWindowHours";
            this.txtDuplicateDetectionHistoryTimeWindowHours.Size = new System.Drawing.Size(58, 26);
            this.txtDuplicateDetectionHistoryTimeWindowHours.TabIndex = 1;
            this.txtDuplicateDetectionHistoryTimeWindowHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txtDuplicateDetectionHistoryTimeWindowDays
            // 
            this.txtDuplicateDetectionHistoryTimeWindowDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowDays.Location = new System.Drawing.Point(24, 68);
            this.txtDuplicateDetectionHistoryTimeWindowDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDuplicateDetectionHistoryTimeWindowDays.Name = "txtDuplicateDetectionHistoryTimeWindowDays";
            this.txtDuplicateDetectionHistoryTimeWindowDays.Size = new System.Drawing.Size(58, 26);
            this.txtDuplicateDetectionHistoryTimeWindowDays.TabIndex = 0;
            this.txtDuplicateDetectionHistoryTimeWindowDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // grouperPath
            // 
            this.grouperPath.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperPath.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperPath.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperPath.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPath.BorderThickness = 1F;
            this.grouperPath.Controls.Add(this.lblRelativeURI);
            this.grouperPath.Controls.Add(this.txtPath);
            this.grouperPath.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperPath.ForeColor = System.Drawing.Color.White;
            this.grouperPath.GroupImage = null;
            this.grouperPath.GroupTitle = "Path";
            this.grouperPath.Location = new System.Drawing.Point(24, 12);
            this.grouperPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperPath.Name = "grouperPath";
            this.grouperPath.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperPath.PaintGroupBox = true;
            this.grouperPath.RoundCorners = 4;
            this.grouperPath.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperPath.ShadowControl = false;
            this.grouperPath.ShadowThickness = 1;
            this.grouperPath.Size = new System.Drawing.Size(444, 123);
            this.grouperPath.TabIndex = 0;
            // 
            // lblRelativeURI
            // 
            this.lblRelativeURI.AutoSize = true;
            this.lblRelativeURI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRelativeURI.Location = new System.Drawing.Point(24, 43);
            this.lblRelativeURI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelativeURI.Name = "lblRelativeURI";
            this.lblRelativeURI.Size = new System.Drawing.Size(107, 20);
            this.lblRelativeURI.TabIndex = 22;
            this.lblRelativeURI.Text = "Relative URI:";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPath.Location = new System.Drawing.Point(24, 68);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(394, 26);
            this.txtPath.TabIndex = 0;
            // 
            // tabPageAuthorization
            // 
            this.tabPageAuthorization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageAuthorization.Controls.Add(this.grouperAuthorizationRuleList);
            this.tabPageAuthorization.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageAuthorization.Location = new System.Drawing.Point(4, 22);
            this.tabPageAuthorization.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageAuthorization.Name = "tabPageAuthorization";
            this.tabPageAuthorization.Size = new System.Drawing.Size(1456, 712);
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
            this.grouperAuthorizationRuleList.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperAuthorizationRuleList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAuthorizationRuleList.BorderThickness = 1F;
            this.grouperAuthorizationRuleList.Controls.Add(this.authorizationRulesDataGridView);
            this.grouperAuthorizationRuleList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAuthorizationRuleList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperAuthorizationRuleList.ForeColor = System.Drawing.Color.White;
            this.grouperAuthorizationRuleList.GroupImage = null;
            this.grouperAuthorizationRuleList.GroupTitle = "Authorization Rule List";
            this.grouperAuthorizationRuleList.Location = new System.Drawing.Point(24, 12);
            this.grouperAuthorizationRuleList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperAuthorizationRuleList.Name = "grouperAuthorizationRuleList";
            this.grouperAuthorizationRuleList.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperAuthorizationRuleList.PaintGroupBox = true;
            this.grouperAuthorizationRuleList.RoundCorners = 4;
            this.grouperAuthorizationRuleList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAuthorizationRuleList.ShadowControl = false;
            this.grouperAuthorizationRuleList.ShadowThickness = 1;
            this.grouperAuthorizationRuleList.Size = new System.Drawing.Size(1404, 665);
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
            this.authorizationRulesDataGridView.Location = new System.Drawing.Point(24, 49);
            this.authorizationRulesDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.authorizationRulesDataGridView.MultiSelect = false;
            this.authorizationRulesDataGridView.Name = "authorizationRulesDataGridView";
            this.authorizationRulesDataGridView.RowHeadersWidth = 24;
            this.authorizationRulesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.authorizationRulesDataGridView.ShowCellErrors = false;
            this.authorizationRulesDataGridView.ShowRowErrors = false;
            this.authorizationRulesDataGridView.Size = new System.Drawing.Size(1356, 592);
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
            // tabPageMetrics
            // 
            this.tabPageMetrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageMetrics.Controls.Add(this.grouperDatapoints);
            this.tabPageMetrics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageMetrics.Location = new System.Drawing.Point(4, 22);
            this.tabPageMetrics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMetrics.Name = "tabPageMetrics";
            this.tabPageMetrics.Size = new System.Drawing.Size(1456, 712);
            this.tabPageMetrics.TabIndex = 9;
            this.tabPageMetrics.Text = "Metrics";
            // 
            // grouperDatapoints
            // 
            this.grouperDatapoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperDatapoints.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDatapoints.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDatapoints.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDatapoints.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDatapoints.BorderThickness = 1F;
            this.grouperDatapoints.Controls.Add(this.dataPointDataGridView);
            this.grouperDatapoints.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDatapoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDatapoints.ForeColor = System.Drawing.Color.White;
            this.grouperDatapoints.GroupImage = null;
            this.grouperDatapoints.GroupTitle = "Metrics Rules";
            this.grouperDatapoints.Location = new System.Drawing.Point(24, 12);
            this.grouperDatapoints.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperDatapoints.Name = "grouperDatapoints";
            this.grouperDatapoints.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperDatapoints.PaintGroupBox = true;
            this.grouperDatapoints.RoundCorners = 4;
            this.grouperDatapoints.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDatapoints.ShadowControl = false;
            this.grouperDatapoints.ShadowThickness = 1;
            this.grouperDatapoints.Size = new System.Drawing.Size(1404, 665);
            this.grouperDatapoints.TabIndex = 2;
            this.grouperDatapoints.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDatapoints_CustomPaint);
            // 
            // dataPointDataGridView
            // 
            this.dataPointDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPointDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.dataPointDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataPointDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPointDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataPointDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.dataPointDataGridView.Location = new System.Drawing.Point(24, 49);
            this.dataPointDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataPointDataGridView.Name = "dataPointDataGridView";
            this.dataPointDataGridView.RowHeadersWidth = 24;
            this.dataPointDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataPointDataGridView.Size = new System.Drawing.Size(1356, 591);
            this.dataPointDataGridView.TabIndex = 27;
            this.dataPointDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataPointDataGridView_CellClick);
            this.dataPointDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataPointDataGridView_DataError);
            this.dataPointDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataPointDataGridView_RowsAdded);
            this.dataPointDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataPointDataGridView_RowsRemoved);
            this.dataPointDataGridView.Resize += new System.EventHandler(this.dataPointDataGridView_Resize);
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageMessages.Controls.Add(this.messagesSplitContainer);
            this.tabPageMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMessages.Size = new System.Drawing.Size(1456, 712);
            this.tabPageMessages.TabIndex = 5;
            this.tabPageMessages.Text = "Messages";
            this.tabPageMessages.Resize += new System.EventHandler(this.tabPageMessages_Resize);
            // 
            // messagesSplitContainer
            // 
            this.messagesSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesSplitContainer.Location = new System.Drawing.Point(24, 12);
            this.messagesSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messagesSplitContainer.Name = "messagesSplitContainer";
            // 
            // messagesSplitContainer.Panel1
            // 
            this.messagesSplitContainer.Panel1.Controls.Add(this.messageListTextPropertiesSplitContainer);
            // 
            // messagesSplitContainer.Panel2
            // 
            this.messagesSplitContainer.Panel2.Controls.Add(this.grouperMessageProperties);
            this.messagesSplitContainer.Size = new System.Drawing.Size(1404, 665);
            this.messagesSplitContainer.SplitterDistance = 910;
            this.messagesSplitContainer.SplitterWidth = 24;
            this.messagesSplitContainer.TabIndex = 3;
            // 
            // messageListTextPropertiesSplitContainer
            // 
            this.messageListTextPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageListTextPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messageListTextPropertiesSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messageListTextPropertiesSplitContainer.Name = "messageListTextPropertiesSplitContainer";
            this.messageListTextPropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // messageListTextPropertiesSplitContainer.Panel1
            // 
            this.messageListTextPropertiesSplitContainer.Panel1.Controls.Add(this.grouperMessageList);
            // 
            // messageListTextPropertiesSplitContainer.Panel2
            // 
            this.messageListTextPropertiesSplitContainer.Panel2.Controls.Add(this.messagesCustomPropertiesSplitContainer);
            this.messageListTextPropertiesSplitContainer.Size = new System.Drawing.Size(910, 665);
            this.messageListTextPropertiesSplitContainer.SplitterDistance = 324;
            this.messageListTextPropertiesSplitContainer.SplitterWidth = 12;
            this.messageListTextPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperMessageList
            // 
            this.grouperMessageList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageList.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageList.BorderThickness = 1F;
            this.grouperMessageList.Controls.Add(this.pictFindMessages);
            this.grouperMessageList.Controls.Add(this.messagesDataGridView);
            this.grouperMessageList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageList.ForeColor = System.Drawing.Color.White;
            this.grouperMessageList.GroupImage = null;
            this.grouperMessageList.GroupTitle = "Message List";
            this.grouperMessageList.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperMessageList.Name = "grouperMessageList";
            this.grouperMessageList.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperMessageList.PaintGroupBox = true;
            this.grouperMessageList.RoundCorners = 4;
            this.grouperMessageList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageList.ShadowControl = false;
            this.grouperMessageList.ShadowThickness = 1;
            this.grouperMessageList.Size = new System.Drawing.Size(910, 324);
            this.grouperMessageList.TabIndex = 17;
            this.grouperMessageList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageList_CustomPaint);
            // 
            // pictFindMessages
            // 
            this.pictFindMessages.Image = global::Microsoft.Azure.ServiceBusExplorer.Properties.Resources.FindExtension;
            this.pictFindMessages.Location = new System.Drawing.Point(150, 0);
            this.pictFindMessages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictFindMessages.Name = "pictFindMessages";
            this.pictFindMessages.Size = new System.Drawing.Size(36, 37);
            this.pictFindMessages.TabIndex = 1;
            this.pictFindMessages.TabStop = false;
            this.pictFindMessages.Click += new System.EventHandler(this.pictFindMessages_Click);
            this.pictFindMessages.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictFindMessages.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
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
            this.messagesDataGridView.Location = new System.Drawing.Point(26, 51);
            this.messagesDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messagesDataGridView.Name = "messagesDataGridView";
            this.messagesDataGridView.ReadOnly = true;
            this.messagesDataGridView.RowHeadersWidth = 24;
            this.messagesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.messagesDataGridView.ShowCellErrors = false;
            this.messagesDataGridView.ShowRowErrors = false;
            this.messagesDataGridView.Size = new System.Drawing.Size(858, 247);
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
            // messagesCustomPropertiesSplitContainer
            // 
            this.messagesCustomPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesCustomPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messagesCustomPropertiesSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messagesCustomPropertiesSplitContainer.Name = "messagesCustomPropertiesSplitContainer";
            // 
            // messagesCustomPropertiesSplitContainer.Panel1
            // 
            this.messagesCustomPropertiesSplitContainer.Panel1.Controls.Add(this.grouperMessageText);
            // 
            // messagesCustomPropertiesSplitContainer.Panel2
            // 
            this.messagesCustomPropertiesSplitContainer.Panel2.Controls.Add(this.grouperMessageCustomProperties);
            this.messagesCustomPropertiesSplitContainer.Size = new System.Drawing.Size(910, 329);
            this.messagesCustomPropertiesSplitContainer.SplitterDistance = 488;
            this.messagesCustomPropertiesSplitContainer.SplitterWidth = 24;
            this.messagesCustomPropertiesSplitContainer.TabIndex = 26;
            // 
            // grouperMessageText
            // 
            this.grouperMessageText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageText.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
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
            this.grouperMessageText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperMessageText.Name = "grouperMessageText";
            this.grouperMessageText.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperMessageText.PaintGroupBox = true;
            this.grouperMessageText.RoundCorners = 4;
            this.grouperMessageText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageText.ShadowControl = false;
            this.grouperMessageText.ShadowThickness = 1;
            this.grouperMessageText.Size = new System.Drawing.Size(488, 329);
            this.grouperMessageText.TabIndex = 25;
            this.grouperMessageText.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageText_CustomPaint);
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessageText.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageText.Location = new System.Drawing.Point(24, 49);
            this.txtMessageText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMessageText.Multiline = true;
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageText.Size = new System.Drawing.Size(438, 253);
            this.txtMessageText.TabIndex = 0;
            // 
            // grouperMessageCustomProperties
            // 
            this.grouperMessageCustomProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageCustomProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageCustomProperties.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageCustomProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageCustomProperties.BorderThickness = 1F;
            this.grouperMessageCustomProperties.Controls.Add(this.messagePropertyListView);
            this.grouperMessageCustomProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageCustomProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageCustomProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageCustomProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageCustomProperties.GroupImage = null;
            this.grouperMessageCustomProperties.GroupTitle = "Message Custom Properties";
            this.grouperMessageCustomProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageCustomProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperMessageCustomProperties.Name = "grouperMessageCustomProperties";
            this.grouperMessageCustomProperties.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperMessageCustomProperties.PaintGroupBox = true;
            this.grouperMessageCustomProperties.RoundCorners = 4;
            this.grouperMessageCustomProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageCustomProperties.ShadowControl = false;
            this.grouperMessageCustomProperties.ShadowThickness = 1;
            this.grouperMessageCustomProperties.Size = new System.Drawing.Size(398, 329);
            this.grouperMessageCustomProperties.TabIndex = 26;
            this.grouperMessageCustomProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageCustomProperties_CustomPaint);
            // 
            // messagePropertyListView
            // 
            this.messagePropertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagePropertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.messagePropertyListView.Location = new System.Drawing.Point(24, 49);
            this.messagePropertyListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messagePropertyListView.Name = "messagePropertyListView";
            this.messagePropertyListView.OwnerDraw = true;
            this.messagePropertyListView.Size = new System.Drawing.Size(348, 253);
            this.messagePropertyListView.TabIndex = 0;
            this.messagePropertyListView.UseCompatibleStateImageBehavior = false;
            this.messagePropertyListView.View = System.Windows.Forms.View.Details;
            this.messagePropertyListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.messagePropertyListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.messagePropertyListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.messagePropertyListView.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 113;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 115;
            // 
            // grouperMessageProperties
            // 
            this.grouperMessageProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageProperties.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.BorderThickness = 1F;
            this.grouperMessageProperties.Controls.Add(this.messagePropertyGrid);
            this.grouperMessageProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageProperties.GroupImage = null;
            this.grouperMessageProperties.GroupTitle = "Message Properties";
            this.grouperMessageProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperMessageProperties.Name = "grouperMessageProperties";
            this.grouperMessageProperties.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperMessageProperties.PaintGroupBox = true;
            this.grouperMessageProperties.RoundCorners = 4;
            this.grouperMessageProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageProperties.ShadowControl = false;
            this.grouperMessageProperties.ShadowThickness = 1;
            this.grouperMessageProperties.Size = new System.Drawing.Size(470, 665);
            this.grouperMessageProperties.TabIndex = 19;
            this.grouperMessageProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageProperties_CustomPaint);
            // 
            // messagePropertyGrid
            // 
            this.messagePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagePropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.messagePropertyGrid.HelpVisible = false;
            this.messagePropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.messagePropertyGrid.Location = new System.Drawing.Point(24, 49);
            this.messagePropertyGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messagePropertyGrid.Name = "messagePropertyGrid";
            this.messagePropertyGrid.Size = new System.Drawing.Size(420, 591);
            this.messagePropertyGrid.TabIndex = 2;
            this.messagePropertyGrid.ToolbarVisible = false;
            // 
            // tabPageDeadletter
            // 
            this.tabPageDeadletter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageDeadletter.Controls.Add(this.deadletterSplitContainer);
            this.tabPageDeadletter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDeadletter.Location = new System.Drawing.Point(4, 22);
            this.tabPageDeadletter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageDeadletter.Name = "tabPageDeadletter";
            this.tabPageDeadletter.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageDeadletter.Size = new System.Drawing.Size(1456, 712);
            this.tabPageDeadletter.TabIndex = 7;
            this.tabPageDeadletter.Text = "Deadletter";
            this.tabPageDeadletter.Resize += new System.EventHandler(this.deadletterTabPage_Resize);
            // 
            // deadletterSplitContainer
            // 
            this.deadletterSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deadletterSplitContainer.Location = new System.Drawing.Point(24, 12);
            this.deadletterSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deadletterSplitContainer.Name = "deadletterSplitContainer";
            // 
            // deadletterSplitContainer.Panel1
            // 
            this.deadletterSplitContainer.Panel1.Controls.Add(this.deadletterListTextPropertiesSplitContainer);
            // 
            // deadletterSplitContainer.Panel2
            // 
            this.deadletterSplitContainer.Panel2.Controls.Add(this.grouperDeadletterProperties);
            this.deadletterSplitContainer.Size = new System.Drawing.Size(1404, 665);
            this.deadletterSplitContainer.SplitterDistance = 910;
            this.deadletterSplitContainer.SplitterWidth = 24;
            this.deadletterSplitContainer.TabIndex = 4;
            // 
            // deadletterListTextPropertiesSplitContainer
            // 
            this.deadletterListTextPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deadletterListTextPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.deadletterListTextPropertiesSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deadletterListTextPropertiesSplitContainer.Name = "deadletterListTextPropertiesSplitContainer";
            this.deadletterListTextPropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // deadletterListTextPropertiesSplitContainer.Panel1
            // 
            this.deadletterListTextPropertiesSplitContainer.Panel1.Controls.Add(this.grouperDeadletterList);
            // 
            // deadletterListTextPropertiesSplitContainer.Panel2
            // 
            this.deadletterListTextPropertiesSplitContainer.Panel2.Controls.Add(this.deadletterCustomPropertiesSplitContainer);
            this.deadletterListTextPropertiesSplitContainer.Size = new System.Drawing.Size(910, 665);
            this.deadletterListTextPropertiesSplitContainer.SplitterDistance = 324;
            this.deadletterListTextPropertiesSplitContainer.SplitterWidth = 12;
            this.deadletterListTextPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperDeadletterList
            // 
            this.grouperDeadletterList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDeadletterList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDeadletterList.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDeadletterList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterList.BorderThickness = 1F;
            this.grouperDeadletterList.Controls.Add(this.pictFindDeadletter);
            this.grouperDeadletterList.Controls.Add(this.deadletterDataGridView);
            this.grouperDeadletterList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperDeadletterList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDeadletterList.ForeColor = System.Drawing.Color.White;
            this.grouperDeadletterList.GroupImage = null;
            this.grouperDeadletterList.GroupTitle = "Message List";
            this.grouperDeadletterList.Location = new System.Drawing.Point(0, 0);
            this.grouperDeadletterList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperDeadletterList.Name = "grouperDeadletterList";
            this.grouperDeadletterList.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperDeadletterList.PaintGroupBox = true;
            this.grouperDeadletterList.RoundCorners = 4;
            this.grouperDeadletterList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDeadletterList.ShadowControl = false;
            this.grouperDeadletterList.ShadowThickness = 1;
            this.grouperDeadletterList.Size = new System.Drawing.Size(910, 324);
            this.grouperDeadletterList.TabIndex = 17;
            this.grouperDeadletterList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterList_CustomPaint);
            // 
            // pictFindDeadletter
            // 
            this.pictFindDeadletter.Image = global::Microsoft.Azure.ServiceBusExplorer.Properties.Resources.FindExtension;
            this.pictFindDeadletter.Location = new System.Drawing.Point(150, 0);
            this.pictFindDeadletter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictFindDeadletter.Name = "pictFindDeadletter";
            this.pictFindDeadletter.Size = new System.Drawing.Size(36, 37);
            this.pictFindDeadletter.TabIndex = 2;
            this.pictFindDeadletter.TabStop = false;
            this.pictFindDeadletter.Click += new System.EventHandler(this.pictFindDeadletter_Click);
            this.pictFindDeadletter.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictFindDeadletter.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
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
            this.deadletterDataGridView.Location = new System.Drawing.Point(26, 51);
            this.deadletterDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deadletterDataGridView.Name = "deadletterDataGridView";
            this.deadletterDataGridView.ReadOnly = true;
            this.deadletterDataGridView.RowHeadersWidth = 24;
            this.deadletterDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.deadletterDataGridView.ShowCellErrors = false;
            this.deadletterDataGridView.ShowRowErrors = false;
            this.deadletterDataGridView.Size = new System.Drawing.Size(858, 247);
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
            // deadletterCustomPropertiesSplitContainer
            // 
            this.deadletterCustomPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deadletterCustomPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.deadletterCustomPropertiesSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deadletterCustomPropertiesSplitContainer.Name = "deadletterCustomPropertiesSplitContainer";
            // 
            // deadletterCustomPropertiesSplitContainer.Panel1
            // 
            this.deadletterCustomPropertiesSplitContainer.Panel1.Controls.Add(this.grouperDeadletterText);
            // 
            // deadletterCustomPropertiesSplitContainer.Panel2
            // 
            this.deadletterCustomPropertiesSplitContainer.Panel2.Controls.Add(this.grouperDeadletterCustomProperties);
            this.deadletterCustomPropertiesSplitContainer.Size = new System.Drawing.Size(910, 329);
            this.deadletterCustomPropertiesSplitContainer.SplitterDistance = 488;
            this.deadletterCustomPropertiesSplitContainer.SplitterWidth = 24;
            this.deadletterCustomPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperDeadletterText
            // 
            this.grouperDeadletterText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDeadletterText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDeadletterText.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
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
            this.grouperDeadletterText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperDeadletterText.Name = "grouperDeadletterText";
            this.grouperDeadletterText.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperDeadletterText.PaintGroupBox = true;
            this.grouperDeadletterText.RoundCorners = 4;
            this.grouperDeadletterText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDeadletterText.ShadowControl = false;
            this.grouperDeadletterText.ShadowThickness = 1;
            this.grouperDeadletterText.Size = new System.Drawing.Size(488, 329);
            this.grouperDeadletterText.TabIndex = 25;
            this.grouperDeadletterText.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterText_CustomPaint);
            // 
            // txtDeadletterText
            // 
            this.txtDeadletterText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeadletterText.BackColor = System.Drawing.SystemColors.Window;
            this.txtDeadletterText.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeadletterText.Location = new System.Drawing.Point(24, 49);
            this.txtDeadletterText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDeadletterText.Multiline = true;
            this.txtDeadletterText.Name = "txtDeadletterText";
            this.txtDeadletterText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDeadletterText.Size = new System.Drawing.Size(438, 253);
            this.txtDeadletterText.TabIndex = 0;
            // 
            // grouperDeadletterCustomProperties
            // 
            this.grouperDeadletterCustomProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDeadletterCustomProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDeadletterCustomProperties.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDeadletterCustomProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterCustomProperties.BorderThickness = 1F;
            this.grouperDeadletterCustomProperties.Controls.Add(this.deadletterPropertyListView);
            this.grouperDeadletterCustomProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterCustomProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperDeadletterCustomProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDeadletterCustomProperties.ForeColor = System.Drawing.Color.White;
            this.grouperDeadletterCustomProperties.GroupImage = null;
            this.grouperDeadletterCustomProperties.GroupTitle = "Message Custom Properties";
            this.grouperDeadletterCustomProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperDeadletterCustomProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperDeadletterCustomProperties.Name = "grouperDeadletterCustomProperties";
            this.grouperDeadletterCustomProperties.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperDeadletterCustomProperties.PaintGroupBox = true;
            this.grouperDeadletterCustomProperties.RoundCorners = 4;
            this.grouperDeadletterCustomProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDeadletterCustomProperties.ShadowControl = false;
            this.grouperDeadletterCustomProperties.ShadowThickness = 1;
            this.grouperDeadletterCustomProperties.Size = new System.Drawing.Size(398, 329);
            this.grouperDeadletterCustomProperties.TabIndex = 26;
            this.grouperDeadletterCustomProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterCustomProperties_CustomPaint);
            // 
            // deadletterPropertyListView
            // 
            this.deadletterPropertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deadletterPropertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.deadletterPropertyListView.Location = new System.Drawing.Point(24, 49);
            this.deadletterPropertyListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deadletterPropertyListView.Name = "deadletterPropertyListView";
            this.deadletterPropertyListView.OwnerDraw = true;
            this.deadletterPropertyListView.Size = new System.Drawing.Size(348, 253);
            this.deadletterPropertyListView.TabIndex = 0;
            this.deadletterPropertyListView.UseCompatibleStateImageBehavior = false;
            this.deadletterPropertyListView.View = System.Windows.Forms.View.Details;
            this.deadletterPropertyListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.deadletterPropertyListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.deadletterPropertyListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.deadletterPropertyListView.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 113;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Value";
            this.columnHeader4.Width = 115;
            // 
            // grouperDeadletterProperties
            // 
            this.grouperDeadletterProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDeadletterProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDeadletterProperties.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDeadletterProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterProperties.BorderThickness = 1F;
            this.grouperDeadletterProperties.Controls.Add(this.deadletterPropertyGrid);
            this.grouperDeadletterProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDeadletterProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperDeadletterProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDeadletterProperties.ForeColor = System.Drawing.Color.White;
            this.grouperDeadletterProperties.GroupImage = null;
            this.grouperDeadletterProperties.GroupTitle = "Message Properties";
            this.grouperDeadletterProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperDeadletterProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperDeadletterProperties.Name = "grouperDeadletterProperties";
            this.grouperDeadletterProperties.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperDeadletterProperties.PaintGroupBox = true;
            this.grouperDeadletterProperties.RoundCorners = 4;
            this.grouperDeadletterProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDeadletterProperties.ShadowControl = false;
            this.grouperDeadletterProperties.ShadowThickness = 1;
            this.grouperDeadletterProperties.Size = new System.Drawing.Size(470, 665);
            this.grouperDeadletterProperties.TabIndex = 19;
            this.grouperDeadletterProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDeadletterProperties_CustomPaint);
            // 
            // deadletterPropertyGrid
            // 
            this.deadletterPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deadletterPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.deadletterPropertyGrid.HelpVisible = false;
            this.deadletterPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.deadletterPropertyGrid.Location = new System.Drawing.Point(24, 49);
            this.deadletterPropertyGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deadletterPropertyGrid.Name = "deadletterPropertyGrid";
            this.deadletterPropertyGrid.Size = new System.Drawing.Size(420, 591);
            this.deadletterPropertyGrid.TabIndex = 1;
            this.deadletterPropertyGrid.ToolbarVisible = false;
            // 
            // tabPageSessions
            // 
            this.tabPageSessions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageSessions.Controls.Add(this.sessionsSplitContainer);
            this.tabPageSessions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageSessions.Location = new System.Drawing.Point(4, 22);
            this.tabPageSessions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSessions.Name = "tabPageSessions";
            this.tabPageSessions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSessions.Size = new System.Drawing.Size(1456, 712);
            this.tabPageSessions.TabIndex = 6;
            this.tabPageSessions.Text = "Sessions";
            this.tabPageSessions.Resize += new System.EventHandler(this.tabPageSessions_Resize);
            // 
            // sessionsSplitContainer
            // 
            this.sessionsSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionsSplitContainer.Location = new System.Drawing.Point(24, 12);
            this.sessionsSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sessionsSplitContainer.Name = "sessionsSplitContainer";
            // 
            // sessionsSplitContainer.Panel1
            // 
            this.sessionsSplitContainer.Panel1.Controls.Add(this.sessionListTextPropertiesSplitContainer);
            // 
            // sessionsSplitContainer.Panel2
            // 
            this.sessionsSplitContainer.Panel2.Controls.Add(this.grouperSessionProperties);
            this.sessionsSplitContainer.Size = new System.Drawing.Size(1404, 665);
            this.sessionsSplitContainer.SplitterDistance = 910;
            this.sessionsSplitContainer.SplitterWidth = 24;
            this.sessionsSplitContainer.TabIndex = 4;
            // 
            // sessionListTextPropertiesSplitContainer
            // 
            this.sessionListTextPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sessionListTextPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.sessionListTextPropertiesSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sessionListTextPropertiesSplitContainer.Name = "sessionListTextPropertiesSplitContainer";
            this.sessionListTextPropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sessionListTextPropertiesSplitContainer.Panel1
            // 
            this.sessionListTextPropertiesSplitContainer.Panel1.Controls.Add(this.grouperSessionList);
            // 
            // sessionListTextPropertiesSplitContainer.Panel2
            // 
            this.sessionListTextPropertiesSplitContainer.Panel2.Controls.Add(this.grouperSessionState);
            this.sessionListTextPropertiesSplitContainer.Size = new System.Drawing.Size(910, 665);
            this.sessionListTextPropertiesSplitContainer.SplitterDistance = 324;
            this.sessionListTextPropertiesSplitContainer.SplitterWidth = 12;
            this.sessionListTextPropertiesSplitContainer.TabIndex = 1;
            // 
            // grouperSessionList
            // 
            this.grouperSessionList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSessionList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSessionList.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
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
            this.grouperSessionList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperSessionList.Name = "grouperSessionList";
            this.grouperSessionList.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperSessionList.PaintGroupBox = true;
            this.grouperSessionList.RoundCorners = 4;
            this.grouperSessionList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSessionList.ShadowControl = false;
            this.grouperSessionList.ShadowThickness = 1;
            this.grouperSessionList.Size = new System.Drawing.Size(910, 324);
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
            this.sessionsDataGridView.Location = new System.Drawing.Point(26, 51);
            this.sessionsDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sessionsDataGridView.MultiSelect = false;
            this.sessionsDataGridView.Name = "sessionsDataGridView";
            this.sessionsDataGridView.ReadOnly = true;
            this.sessionsDataGridView.RowHeadersWidth = 24;
            this.sessionsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sessionsDataGridView.ShowCellErrors = false;
            this.sessionsDataGridView.ShowRowErrors = false;
            this.sessionsDataGridView.Size = new System.Drawing.Size(858, 248);
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
            this.grouperSessionState.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperSessionState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionState.BorderThickness = 1F;
            this.grouperSessionState.Controls.Add(this.txtSessionState);
            this.grouperSessionState.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperSessionState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSessionState.ForeColor = System.Drawing.Color.White;
            this.grouperSessionState.GroupImage = null;
            this.grouperSessionState.GroupTitle = "SessionState";
            this.grouperSessionState.Location = new System.Drawing.Point(0, 0);
            this.grouperSessionState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperSessionState.Name = "grouperSessionState";
            this.grouperSessionState.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperSessionState.PaintGroupBox = true;
            this.grouperSessionState.RoundCorners = 4;
            this.grouperSessionState.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSessionState.ShadowControl = false;
            this.grouperSessionState.ShadowThickness = 1;
            this.grouperSessionState.Size = new System.Drawing.Size(910, 329);
            this.grouperSessionState.TabIndex = 26;
            this.grouperSessionState.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperSessionState_CustomPaint);
            // 
            // txtSessionState
            // 
            this.txtSessionState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSessionState.Location = new System.Drawing.Point(24, 49);
            this.txtSessionState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSessionState.Multiline = true;
            this.txtSessionState.Name = "txtSessionState";
            this.txtSessionState.Size = new System.Drawing.Size(860, 250);
            this.txtSessionState.TabIndex = 13;
            // 
            // grouperSessionProperties
            // 
            this.grouperSessionProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSessionProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSessionProperties.BackgroundGradientMode = Microsoft.Azure.ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperSessionProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionProperties.BorderThickness = 1F;
            this.grouperSessionProperties.Controls.Add(this.sessionPropertyGrid);
            this.grouperSessionProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSessionProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperSessionProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSessionProperties.ForeColor = System.Drawing.Color.White;
            this.grouperSessionProperties.GroupImage = null;
            this.grouperSessionProperties.GroupTitle = "Session Properties";
            this.grouperSessionProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperSessionProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperSessionProperties.Name = "grouperSessionProperties";
            this.grouperSessionProperties.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperSessionProperties.PaintGroupBox = true;
            this.grouperSessionProperties.RoundCorners = 4;
            this.grouperSessionProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSessionProperties.ShadowControl = false;
            this.grouperSessionProperties.ShadowThickness = 1;
            this.grouperSessionProperties.Size = new System.Drawing.Size(470, 665);
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
            this.sessionPropertyGrid.Location = new System.Drawing.Point(24, 49);
            this.sessionPropertyGrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sessionPropertyGrid.Name = "sessionPropertyGrid";
            this.sessionPropertyGrid.Size = new System.Drawing.Size(420, 591);
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
            this.btnSessions.Location = new System.Drawing.Point(542, 775);
            this.btnSessions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSessions.Name = "btnSessions";
            this.btnSessions.Size = new System.Drawing.Size(108, 37);
            this.btnSessions.TabIndex = 2;
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
            this.btnMessages.Location = new System.Drawing.Point(662, 775);
            this.btnMessages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(108, 37);
            this.btnMessages.TabIndex = 3;
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
            this.btnDeadletter.Location = new System.Drawing.Point(778, 775);
            this.btnDeadletter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDeadletter.Name = "btnDeadletter";
            this.btnDeadletter.Size = new System.Drawing.Size(108, 37);
            this.btnDeadletter.TabIndex = 4;
            this.btnDeadletter.Text = "Deadletter";
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
            this.saveSelectedMessagesToolStripMenuItem});
            this.messagesContextMenuStrip.Name = "registrationContextMenuStrip";
            this.messagesContextMenuStrip.Size = new System.Drawing.Size(433, 130);
            // 
            // repairAndResubmitMessageToolStripMenuItem
            // 
            this.repairAndResubmitMessageToolStripMenuItem.Name = "repairAndResubmitMessageToolStripMenuItem";
            this.repairAndResubmitMessageToolStripMenuItem.Size = new System.Drawing.Size(432, 30);
            this.repairAndResubmitMessageToolStripMenuItem.Text = "Repair and Resubmit Selected Message";
            this.repairAndResubmitMessageToolStripMenuItem.Click += new System.EventHandler(this.repairAndResubmitMessageToolStripMenuItem_Click);
            // 
            // resubmitSelectedMessagesInBatchModeToolStripMenuItem
            // 
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Name = "resubmitSelectedMessagesInBatchModeToolStripMenuItem";
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Size = new System.Drawing.Size(432, 30);
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Text = "Resubmit Selected Messages In Batch Mode";
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Click += new System.EventHandler(this.resubmitSelectedMessagesInBatchModeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(429, 6);
            // 
            // saveSelectedMessageToolStripMenuItem
            // 
            this.saveSelectedMessageToolStripMenuItem.Name = "saveSelectedMessageToolStripMenuItem";
            this.saveSelectedMessageToolStripMenuItem.Size = new System.Drawing.Size(432, 30);
            this.saveSelectedMessageToolStripMenuItem.Text = "Save Selected Message";
            this.saveSelectedMessageToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedMessageToolStripMenuItem_Click);
            // 
            // saveSelectedMessagesToolStripMenuItem
            // 
            this.saveSelectedMessagesToolStripMenuItem.Name = "saveSelectedMessagesToolStripMenuItem";
            this.saveSelectedMessagesToolStripMenuItem.Size = new System.Drawing.Size(432, 30);
            this.saveSelectedMessagesToolStripMenuItem.Text = "Save Selected Messages";
            this.saveSelectedMessagesToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedMessagesToolStripMenuItem_Click);
            // 
            // deadletterContextMenuStrip
            // 
            this.deadletterContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.deadletterContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repairAndResubmitDeadletterToolStripMenuItem,
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveSelectedDeadletteredMessageToolStripMenuItem,
            this.saveSelectedDeadletteredMessagesToolStripMenuItem});
            this.deadletterContextMenuStrip.Name = "registrationContextMenuStrip";
            this.deadletterContextMenuStrip.Size = new System.Drawing.Size(433, 130);
            // 
            // repairAndResubmitDeadletterToolStripMenuItem
            // 
            this.repairAndResubmitDeadletterToolStripMenuItem.Name = "repairAndResubmitDeadletterToolStripMenuItem";
            this.repairAndResubmitDeadletterToolStripMenuItem.Size = new System.Drawing.Size(432, 30);
            this.repairAndResubmitDeadletterToolStripMenuItem.Text = "Repair and Resubmit Selected Message";
            this.repairAndResubmitDeadletterToolStripMenuItem.Click += new System.EventHandler(this.repairAndResubmitDeadletterMessageToolStripMenuItem_Click);
            // 
            // resubmitSelectedDeadletterInBatchModeToolStripMenuItem
            // 
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Name = "resubmitSelectedDeadletterInBatchModeToolStripMenuItem";
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Size = new System.Drawing.Size(432, 30);
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Text = "Resubmit Selected Messages In Batch Mode";
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Click += new System.EventHandler(this.resubmitSelectedDeadletterMessagesInBatchModeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(429, 6);
            // 
            // saveSelectedDeadletteredMessageToolStripMenuItem
            // 
            this.saveSelectedDeadletteredMessageToolStripMenuItem.Name = "saveSelectedDeadletteredMessageToolStripMenuItem";
            this.saveSelectedDeadletteredMessageToolStripMenuItem.Size = new System.Drawing.Size(432, 30);
            this.saveSelectedDeadletteredMessageToolStripMenuItem.Text = "Save Selected Message";
            this.saveSelectedDeadletteredMessageToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedDeadletteredMessageToolStripMenuItem_Click);
            // 
            // saveSelectedDeadletteredMessagesToolStripMenuItem
            // 
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Name = "saveSelectedDeadletteredMessagesToolStripMenuItem";
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Size = new System.Drawing.Size(432, 30);
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Text = "Save Selected Messages";
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedDeadletteredMessagesToolStripMenuItem_Click);
            // 
            // btnMetrics
            // 
            this.btnMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMetrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnMetrics.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMetrics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMetrics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMetrics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMetrics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMetrics.Location = new System.Drawing.Point(302, 775);
            this.btnMetrics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMetrics.Name = "btnMetrics";
            this.btnMetrics.Size = new System.Drawing.Size(108, 37);
            this.btnMetrics.TabIndex = 12;
            this.btnMetrics.Text = "Get Metrics";
            this.btnMetrics.UseVisualStyleBackColor = false;
            this.btnMetrics.Click += new System.EventHandler(this.btnMetrics_Click);
            // 
            // btnCloseTabs
            // 
            this.btnCloseTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCloseTabs.Enabled = false;
            this.btnCloseTabs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCloseTabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCloseTabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCloseTabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseTabs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCloseTabs.Location = new System.Drawing.Point(422, 775);
            this.btnCloseTabs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCloseTabs.Name = "btnCloseTabs";
            this.btnCloseTabs.Size = new System.Drawing.Size(108, 37);
            this.btnCloseTabs.TabIndex = 13;
            this.btnCloseTabs.Text = "Close Tabs";
            this.btnCloseTabs.UseVisualStyleBackColor = false;
            this.btnCloseTabs.Click += new System.EventHandler(this.btnCloseTabs_Click);
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
            this.btnPurgeDeadletterQueueMessages.Location = new System.Drawing.Point(182, 775);
            this.btnPurgeDeadletterQueueMessages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPurgeDeadletterQueueMessages.Name = "btnPurgeDeadletterQueueMessages";
            this.btnPurgeDeadletterQueueMessages.Size = new System.Drawing.Size(108, 37);
            this.btnPurgeDeadletterQueueMessages.TabIndex = 14;
            this.btnPurgeDeadletterQueueMessages.Text = "Purge DLQ";
            this.btnPurgeDeadletterQueueMessages.UseVisualStyleBackColor = false;
            this.btnPurgeDeadletterQueueMessages.Click += new System.EventHandler(this.btnPurgeDeadletterQueueMessages_Click);
            this.btnPurgeDeadletterQueueMessages.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnPurgeDeadletterQueueMessages.MouseLeave += new System.EventHandler(this.button_MouseLeave);
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
            this.btnPurgeMessages.Location = new System.Drawing.Point(62, 775);
            this.btnPurgeMessages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPurgeMessages.Name = "btnPurgeMessages";
            this.btnPurgeMessages.Size = new System.Drawing.Size(108, 37);
            this.btnPurgeMessages.TabIndex = 15;
            this.btnPurgeMessages.Text = "Purge";
            this.btnPurgeMessages.UseVisualStyleBackColor = false;
            this.btnPurgeMessages.Click += new System.EventHandler(this.btnPurgeMessages_Click);
            this.btnPurgeMessages.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnPurgeMessages.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // tabPageTransferDeadletter
            // 
            this.tabPageTransferDeadletter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageTransferDeadletter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageTransferDeadletter.Location = new System.Drawing.Point(4, 22);
            this.tabPageTransferDeadletter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTransferDeadletter.Name = "tabPageTransferDeadletter";
            this.tabPageTransferDeadletter.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTransferDeadletter.Size = new System.Drawing.Size(1456, 712);
            this.tabPageTransferDeadletter.TabIndex = 10;
            this.tabPageTransferDeadletter.Text = "Transfer Deadletter";
            // 
            // btnTransferDlq
            // 
            this.btnTransferDlq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransferDlq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnTransferDlq.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnTransferDlq.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnTransferDlq.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnTransferDlq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferDlq.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTransferDlq.Location = new System.Drawing.Point(900, 775);
            this.btnTransferDlq.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTransferDlq.Name = "btnTransferDlq";
            this.btnTransferDlq.Size = new System.Drawing.Size(108, 37);
            this.btnTransferDlq.TabIndex = 16;
            this.btnTransferDlq.Text = "TDLQ";
            this.btnTransferDlq.UseVisualStyleBackColor = false;
            this.btnTransferDlq.Click += new System.EventHandler(this.btnTransferDlq_Click);
            // 
            // HandleQueueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.btnTransferDlq);
            this.Controls.Add(this.btnPurgeMessages);
            this.Controls.Add(this.btnPurgeDeadletterQueueMessages);
            this.Controls.Add(this.btnCloseTabs);
            this.Controls.Add(this.btnMetrics);
            this.Controls.Add(this.btnDeadletter);
            this.Controls.Add(this.btnMessages);
            this.Controls.Add(this.btnSessions);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnChangeStatus);
            this.Controls.Add(this.btnCancelUpdate);
            this.Controls.Add(this.btnCreateDelete);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "HandleQueueControl";
            this.Size = new System.Drawing.Size(1512, 837);
            this.mainTabControl.ResumeLayout(false);
            this.tabPageDescription.ResumeLayout(false);
            this.grouperAutoDeleteOnIdle.ResumeLayout(false);
            this.grouperAutoDeleteOnIdle.PerformLayout();
            this.grouperQueueInformation.ResumeLayout(false);
            this.groupergrouperDefaultMessageTimeToLive.ResumeLayout(false);
            this.groupergrouperDefaultMessageTimeToLive.PerformLayout();
            this.grouperQueueSettings.ResumeLayout(false);
            this.grouperQueueProperties.ResumeLayout(false);
            this.grouperQueueProperties.PerformLayout();
            this.grouperLockDuration.ResumeLayout(false);
            this.grouperLockDuration.PerformLayout();
            this.grouperDuplicateDetectionHistoryTimeWindow.ResumeLayout(false);
            this.grouperDuplicateDetectionHistoryTimeWindow.PerformLayout();
            this.grouperPath.ResumeLayout(false);
            this.grouperPath.PerformLayout();
            this.tabPageAuthorization.ResumeLayout(false);
            this.grouperAuthorizationRuleList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).EndInit();
            this.tabPageMetrics.ResumeLayout(false);
            this.grouperDatapoints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataPointDataGridView)).EndInit();
            this.tabPageMessages.ResumeLayout(false);
            this.messagesSplitContainer.Panel1.ResumeLayout(false);
            this.messagesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesSplitContainer)).EndInit();
            this.messagesSplitContainer.ResumeLayout(false);
            this.messageListTextPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.messageListTextPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).EndInit();
            this.messageListTextPropertiesSplitContainer.ResumeLayout(false);
            this.grouperMessageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictFindMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesDataGridView)).EndInit();
            this.messagesCustomPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.messagesCustomPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesCustomPropertiesSplitContainer)).EndInit();
            this.messagesCustomPropertiesSplitContainer.ResumeLayout(false);
            this.grouperMessageText.ResumeLayout(false);
            this.grouperMessageText.PerformLayout();
            this.grouperMessageCustomProperties.ResumeLayout(false);
            this.grouperMessageProperties.ResumeLayout(false);
            this.tabPageDeadletter.ResumeLayout(false);
            this.deadletterSplitContainer.Panel1.ResumeLayout(false);
            this.deadletterSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterSplitContainer)).EndInit();
            this.deadletterSplitContainer.ResumeLayout(false);
            this.deadletterListTextPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.deadletterListTextPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterListTextPropertiesSplitContainer)).EndInit();
            this.deadletterListTextPropertiesSplitContainer.ResumeLayout(false);
            this.grouperDeadletterList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictFindDeadletter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterDataGridView)).EndInit();
            this.deadletterCustomPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.deadletterCustomPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterCustomPropertiesSplitContainer)).EndInit();
            this.deadletterCustomPropertiesSplitContainer.ResumeLayout(false);
            this.grouperDeadletterText.ResumeLayout(false);
            this.grouperDeadletterText.PerformLayout();
            this.grouperDeadletterCustomProperties.ResumeLayout(false);
            this.grouperDeadletterProperties.ResumeLayout(false);
            this.tabPageSessions.ResumeLayout(false);
            this.sessionsSplitContainer.Panel1.ResumeLayout(false);
            this.sessionsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionsSplitContainer)).EndInit();
            this.sessionsSplitContainer.ResumeLayout(false);
            this.sessionListTextPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.sessionListTextPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionListTextPropertiesSplitContainer)).EndInit();
            this.sessionListTextPropertiesSplitContainer.ResumeLayout(false);
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
        private System.Windows.Forms.Label lblAutoDeleteOnIdleMilliseconds;
        private System.Windows.Forms.TextBox txtAutoDeleteOnIdleMilliseconds;
        private System.Windows.Forms.Label lblAutoDeleteOnIdleSeconds;
        private System.Windows.Forms.TextBox txtAutoDeleteOnIdleSeconds;
        private System.Windows.Forms.Label lblAutoDeleteOnIdleMinutes;
        private System.Windows.Forms.TextBox txtAutoDeleteOnIdleMinutes;
        private System.Windows.Forms.Label lblAutoDeleteOnIdleHours;
        private System.Windows.Forms.Label lblAutoDeleteOnIdleDays;
        private System.Windows.Forms.TextBox txtAutoDeleteOnIdleHours;
        private System.Windows.Forms.TextBox txtAutoDeleteOnIdleDays;
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
        private System.Windows.Forms.TextBox txtMaxDeliveryCount;
        private Grouper grouperLockDuration;
        private System.Windows.Forms.Label lblLockDurationMilliseconds;
        private System.Windows.Forms.TextBox txtLockDurationMilliseconds;
        private System.Windows.Forms.Label lblLockDurationSeconds;
        private System.Windows.Forms.TextBox txtLockDurationSeconds;
        private System.Windows.Forms.Label lblLockDurationMinutes;
        private System.Windows.Forms.TextBox txtLockDurationMinutes;
        private System.Windows.Forms.Label lblLockDurationHours;
        private System.Windows.Forms.Label lblLockDurationDays;
        private System.Windows.Forms.TextBox txtLockDurationHours;
        private System.Windows.Forms.TextBox txtLockDurationDays;
        private Grouper groupergrouperDefaultMessageTimeToLive;
        private System.Windows.Forms.Label lblDefaultMessageTimeToLiveMilliseconds;
        private System.Windows.Forms.TextBox txtDefaultMessageTimeToLiveMilliseconds;
        private System.Windows.Forms.Label lblDefaultMessageTimeToLiveSeconds;
        private System.Windows.Forms.TextBox txtDefaultMessageTimeToLiveSeconds;
        private System.Windows.Forms.Label lblDefaultMessageTimeToLiveMinutes;
        private System.Windows.Forms.TextBox txtDefaultMessageTimeToLiveMinutes;
        private System.Windows.Forms.Label lbllblDefaultMessageTimeToLiveHours;
        private System.Windows.Forms.Label lblDefaultMessageTimeToLiveDays;
        private System.Windows.Forms.TextBox txtDefaultMessageTimeToLiveHours;
        private System.Windows.Forms.TextBox txtDefaultMessageTimeToLiveDays;
        private Grouper grouperDuplicateDetectionHistoryTimeWindow;
        private System.Windows.Forms.Label lblDuplicateDetectionHistoryTimeWindowMilliseconds;
        private System.Windows.Forms.TextBox txtDuplicateDetectionHistoryTimeWindowMilliseconds;
        private System.Windows.Forms.Label lblDuplicateDetectionHistoryTimeWindowSeconds;
        private System.Windows.Forms.TextBox txtDuplicateDetectionHistoryTimeWindowSeconds;
        private System.Windows.Forms.Label lblDuplicateDetectionHistoryTimeWindowMinutes;
        private System.Windows.Forms.TextBox txtDuplicateDetectionHistoryTimeWindowMinutes;
        private System.Windows.Forms.Label lblDuplicateDetectionHistoryTimeWindowHours;
        private System.Windows.Forms.Label lblDuplicateDetectionHistoryTimeWindowDays;
        private System.Windows.Forms.TextBox txtDuplicateDetectionHistoryTimeWindowHours;
        private System.Windows.Forms.TextBox txtDuplicateDetectionHistoryTimeWindowDays;
        private Grouper grouperPath;
        private System.Windows.Forms.Label lblRelativeURI;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TabPage tabPageSessions;
        private System.Windows.Forms.SplitContainer messagesSplitContainer;
        private Grouper grouperMessageProperties;
        private System.Windows.Forms.Button btnSessions;
        private System.Windows.Forms.Button btnMessages;
        private System.Windows.Forms.BindingSource messagesBindingSource;
        private System.Windows.Forms.SplitContainer messageListTextPropertiesSplitContainer;
        private Grouper grouperMessageList;
        private System.Windows.Forms.SplitContainer sessionsSplitContainer;
        private Grouper grouperSessionProperties;
        private System.Windows.Forms.PropertyGrid sessionPropertyGrid;
        private System.Windows.Forms.BindingSource sessionsBindingSource;
        private System.Windows.Forms.DataGridView messagesDataGridView;
        private System.Windows.Forms.Button btnDeadletter;
        private System.Windows.Forms.TabPage tabPageDeadletter;
        private System.Windows.Forms.SplitContainer deadletterSplitContainer;
        private System.Windows.Forms.SplitContainer deadletterListTextPropertiesSplitContainer;
        private Grouper grouperDeadletterList;
        private System.Windows.Forms.DataGridView deadletterDataGridView;
        private Grouper grouperDeadletterProperties;
        private System.Windows.Forms.PropertyGrid deadletterPropertyGrid;
        private System.Windows.Forms.BindingSource deadletterBindingSource;
        private System.Windows.Forms.SplitContainer deadletterCustomPropertiesSplitContainer;
        private Grouper grouperDeadletterText;
        private System.Windows.Forms.TextBox txtDeadletterText;
        private Grouper grouperDeadletterCustomProperties;
        private System.Windows.Forms.ListView deadletterPropertyListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.SplitContainer messagesCustomPropertiesSplitContainer;
        private Grouper grouperMessageText;
        private System.Windows.Forms.TextBox txtMessageText;
        private Grouper grouperMessageCustomProperties;
        private System.Windows.Forms.ListView messagePropertyListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.SplitContainer sessionListTextPropertiesSplitContainer;
        private Grouper grouperSessionList;
        private System.Windows.Forms.DataGridView sessionsDataGridView;
        private Grouper grouperSessionState;
        private System.Windows.Forms.TextBox txtSessionState;
        private System.Windows.Forms.TabPage tabPageAuthorization;
        private System.Windows.Forms.BindingSource authorizationRulesBindingSource;
        private Grouper grouperAuthorizationRuleList;
        private System.Windows.Forms.DataGridView authorizationRulesDataGridView;
        private System.Windows.Forms.PropertyGrid messagePropertyGrid;
        private System.Windows.Forms.TabPage tabPageMetrics;
        private Grouper grouperDatapoints;
        private System.Windows.Forms.DataGridView dataPointDataGridView;
        private System.Windows.Forms.ContextMenuStrip messagesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem repairAndResubmitMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitSelectedMessagesInBatchModeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip deadletterContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem repairAndResubmitDeadletterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitSelectedDeadletterInBatchModeToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictFindMessages;
        private System.Windows.Forms.PictureBox pictFindDeadletter;
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
        private System.Windows.Forms.Button btnMetrics;
        private System.Windows.Forms.Button btnCloseTabs;
        private System.Windows.Forms.Button btnPurgeDeadletterQueueMessages;
        private System.Windows.Forms.Button btnPurgeMessages;
        private System.Windows.Forms.TabPage tabPageTransferDeadletter;
        private System.Windows.Forms.Button btnTransferDlq;
    }
}
