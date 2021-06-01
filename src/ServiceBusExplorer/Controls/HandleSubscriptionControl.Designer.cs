namespace ServiceBusExplorer.Controls
{
    partial class HandleSubscriptionControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HandleSubscriptionControl));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPurgeDeadletterQueueMessages = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.btnCancelUpdate = new System.Windows.Forms.Button();
            this.btnCreateDelete = new System.Windows.Forms.Button();
            this.btnDeadletter = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.btnSessions = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.grouperAutoDeleteOnIdle = new ServiceBusExplorer.Controls.Grouper();
            this.tsAutoDeleteOnIdle = new ServiceBusExplorer.Controls.TimeSpanControl();
            this.groupergrouperDefaultMessageTimeToLive = new ServiceBusExplorer.Controls.Grouper();
            this.tsDefaultMessageTimeToLive = new ServiceBusExplorer.Controls.TimeSpanControl();
            this.grouperName = new ServiceBusExplorer.Controls.Grouper();
            this.lblRelativeURI = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grouperDefaultRule = new ServiceBusExplorer.Controls.Grouper();
            this.btnOpenActionForm = new System.Windows.Forms.Button();
            this.btnOpenFilterForm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.txtAction = new System.Windows.Forms.TextBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.grouperSubscriptionInformation = new ServiceBusExplorer.Controls.Grouper();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grouperSubscriptionProperties = new ServiceBusExplorer.Controls.Grouper();
            this.btnOpenForwardDeadLetteredMessagesToForm = new System.Windows.Forms.Button();
            this.lblForwardDeadLetteredMessagesTo = new System.Windows.Forms.Label();
            this.txtForwardDeadLetteredMessagesTo = new System.Windows.Forms.TextBox();
            this.btnOpenDescriptionForm = new System.Windows.Forms.Button();
            this.txtUserMetadata = new System.Windows.Forms.TextBox();
            this.btnOpenForwardToForm = new System.Windows.Forms.Button();
            this.lblForwardTo = new System.Windows.Forms.Label();
            this.txtForwardTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelUserDescription = new System.Windows.Forms.Label();
            this.txtMaxDeliveryCount = new ServiceBusExplorer.Controls.NumericTextBox();
            this.grouperLockDuration = new ServiceBusExplorer.Controls.Grouper();
            this.tsLockDuration = new ServiceBusExplorer.Controls.TimeSpanControl();
            this.grouperSubscriptionSettings = new ServiceBusExplorer.Controls.Grouper();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
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
            this.grouperMessageProperties = new ServiceBusExplorer.Controls.Grouper();
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
            this.tabPageSessions = new System.Windows.Forms.TabPage();
            this.sessionsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.sessionMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperSessionList = new ServiceBusExplorer.Controls.Grouper();
            this.sessionsDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperSessionState = new ServiceBusExplorer.Controls.Grouper();
            this.txtSessionState = new System.Windows.Forms.TextBox();
            this.grouperSessionProperties = new ServiceBusExplorer.Controls.Grouper();
            this.sessionPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.sessionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.messagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.deadletterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.authorizationRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.deadletterContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.repairAndResubmitDeadletterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedDeadletteredMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedDeadletteredMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.repairAndResubmitMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPurgeMessages = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.tabPageDescription.SuspendLayout();
            this.grouperAutoDeleteOnIdle.SuspendLayout();
            this.groupergrouperDefaultMessageTimeToLive.SuspendLayout();
            this.grouperName.SuspendLayout();
            this.grouperDefaultRule.SuspendLayout();
            this.grouperSubscriptionInformation.SuspendLayout();
            this.grouperSubscriptionProperties.SuspendLayout();
            this.grouperLockDuration.SuspendLayout();
            this.grouperSubscriptionSettings.SuspendLayout();
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
            this.grouperMessageProperties.SuspendLayout();
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
            this.tabPageSessions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsSplitContainer)).BeginInit();
            this.sessionsSplitContainer.Panel1.SuspendLayout();
            this.sessionsSplitContainer.Panel2.SuspendLayout();
            this.sessionsSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionMainSplitContainer)).BeginInit();
            this.sessionMainSplitContainer.Panel1.SuspendLayout();
            this.sessionMainSplitContainer.Panel2.SuspendLayout();
            this.sessionMainSplitContainer.SuspendLayout();
            this.grouperSessionList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsDataGridView)).BeginInit();
            this.grouperSessionState.SuspendLayout();
            this.grouperSessionProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesBindingSource)).BeginInit();
            this.deadletterContextMenuStrip.SuspendLayout();
            this.messagesContextMenuStrip.SuspendLayout();
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
            this.btnPurgeDeadletterQueueMessages.Location = new System.Drawing.Point(362, 504);
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
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefresh.Location = new System.Drawing.Point(680, 504);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 24);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnRefresh.MouseLeave += new System.EventHandler(this.button_MouseLeave);
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
            this.btnChangeStatus.Location = new System.Drawing.Point(759, 504);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(72, 24);
            this.btnChangeStatus.TabIndex = 7;
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
            this.btnCancelUpdate.Location = new System.Drawing.Point(918, 504);
            this.btnCancelUpdate.Name = "btnCancelUpdate";
            this.btnCancelUpdate.Size = new System.Drawing.Size(72, 24);
            this.btnCancelUpdate.TabIndex = 9;
            this.btnCancelUpdate.Text = "Update";
            this.btnCancelUpdate.UseVisualStyleBackColor = false;
            this.btnCancelUpdate.Click += new System.EventHandler(this.btnCancelUpdate_Click);
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
            this.btnCreateDelete.Location = new System.Drawing.Point(838, 504);
            this.btnCreateDelete.Name = "btnCreateDelete";
            this.btnCreateDelete.Size = new System.Drawing.Size(72, 24);
            this.btnCreateDelete.TabIndex = 8;
            this.btnCreateDelete.Text = "Create";
            this.btnCreateDelete.UseVisualStyleBackColor = false;
            this.btnCreateDelete.Click += new System.EventHandler(this.btnCreateDelete_Click);
            this.btnCreateDelete.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCreateDelete.MouseLeave += new System.EventHandler(this.button_MouseLeave);
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
            this.btnDeadletter.Location = new System.Drawing.Point(600, 504);
            this.btnDeadletter.Name = "btnDeadletter";
            this.btnDeadletter.Size = new System.Drawing.Size(72, 24);
            this.btnDeadletter.TabIndex = 5;
            this.btnDeadletter.Text = "Dead-letter";
            this.btnDeadletter.UseVisualStyleBackColor = false;
            this.btnDeadletter.Click += new System.EventHandler(this.btnDeadletter_Click);
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
            this.btnMessages.Location = new System.Drawing.Point(520, 504);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(72, 24);
            this.btnMessages.TabIndex = 4;
            this.btnMessages.Text = "Messages";
            this.btnMessages.UseVisualStyleBackColor = false;
            this.btnMessages.Click += new System.EventHandler(this.btnMessages_Click);
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
            this.btnSessions.Location = new System.Drawing.Point(441, 504);
            this.btnSessions.Name = "btnSessions";
            this.btnSessions.Size = new System.Drawing.Size(72, 24);
            this.btnSessions.TabIndex = 3;
            this.btnSessions.Text = "Sessions";
            this.btnSessions.UseVisualStyleBackColor = false;
            this.btnSessions.Click += new System.EventHandler(this.btnSessions_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.tabPageDescription);
            this.mainTabControl.Controls.Add(this.tabPageMessages);
            this.mainTabControl.Controls.Add(this.tabPageDeadletter);
            this.mainTabControl.Controls.Add(this.tabPageSessions);
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tabPageDescription.Controls.Add(this.groupergrouperDefaultMessageTimeToLive);
            this.tabPageDescription.Controls.Add(this.grouperName);
            this.tabPageDescription.Controls.Add(this.grouperDefaultRule);
            this.tabPageDescription.Controls.Add(this.grouperSubscriptionInformation);
            this.tabPageDescription.Controls.Add(this.grouperSubscriptionProperties);
            this.tabPageDescription.Controls.Add(this.grouperLockDuration);
            this.tabPageDescription.Controls.Add(this.grouperSubscriptionSettings);
            this.tabPageDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDescription.Location = new System.Drawing.Point(4, 24);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(968, 452);
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
            this.grouperAutoDeleteOnIdle.TabIndex = 4;
            // 
            // tsAutoDeleteOnIdle
            // 
            this.tsAutoDeleteOnIdle.Location = new System.Drawing.Point(14, 25);
            this.tsAutoDeleteOnIdle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tsAutoDeleteOnIdle.Name = "tsAutoDeleteOnIdle";
            this.tsAutoDeleteOnIdle.Size = new System.Drawing.Size(273, 42);
            this.tsAutoDeleteOnIdle.TabIndex = 0;
            this.tsAutoDeleteOnIdle.TimeSpanValue = null;
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
            this.groupergrouperDefaultMessageTimeToLive.TabIndex = 5;
            // 
            // tsDefaultMessageTimeToLive
            // 
            this.tsDefaultMessageTimeToLive.Location = new System.Drawing.Point(14, 25);
            this.tsDefaultMessageTimeToLive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tsDefaultMessageTimeToLive.Name = "tsDefaultMessageTimeToLive";
            this.tsDefaultMessageTimeToLive.Size = new System.Drawing.Size(273, 42);
            this.tsDefaultMessageTimeToLive.TabIndex = 0;
            this.tsDefaultMessageTimeToLive.TimeSpanValue = null;
            // 
            // grouperName
            // 
            this.grouperName.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperName.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperName.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperName.BorderThickness = 1F;
            this.grouperName.Controls.Add(this.lblRelativeURI);
            this.grouperName.Controls.Add(this.txtName);
            this.grouperName.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperName.ForeColor = System.Drawing.Color.White;
            this.grouperName.GroupImage = null;
            this.grouperName.GroupTitle = "Name";
            this.grouperName.Location = new System.Drawing.Point(16, 8);
            this.grouperName.Name = "grouperName";
            this.grouperName.Padding = new System.Windows.Forms.Padding(20);
            this.grouperName.PaintGroupBox = true;
            this.grouperName.RoundCorners = 4;
            this.grouperName.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperName.ShadowControl = false;
            this.grouperName.ShadowThickness = 1;
            this.grouperName.Size = new System.Drawing.Size(296, 80);
            this.grouperName.TabIndex = 0;
            // 
            // lblRelativeURI
            // 
            this.lblRelativeURI.AutoSize = true;
            this.lblRelativeURI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRelativeURI.Location = new System.Drawing.Point(16, 28);
            this.lblRelativeURI.Name = "lblRelativeURI";
            this.lblRelativeURI.Size = new System.Drawing.Size(99, 13);
            this.lblRelativeURI.TabIndex = 0;
            this.lblRelativeURI.Text = "Subscription Name:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.SystemColors.Window;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtName.Location = new System.Drawing.Point(16, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(264, 20);
            this.txtName.TabIndex = 1;
            // 
            // grouperDefaultRule
            // 
            this.grouperDefaultRule.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDefaultRule.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDefaultRule.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperDefaultRule.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDefaultRule.BorderThickness = 1F;
            this.grouperDefaultRule.Controls.Add(this.btnOpenActionForm);
            this.grouperDefaultRule.Controls.Add(this.btnOpenFilterForm);
            this.grouperDefaultRule.Controls.Add(this.label1);
            this.grouperDefaultRule.Controls.Add(this.lblFilter);
            this.grouperDefaultRule.Controls.Add(this.txtAction);
            this.grouperDefaultRule.Controls.Add(this.txtFilter);
            this.grouperDefaultRule.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperDefaultRule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperDefaultRule.ForeColor = System.Drawing.Color.White;
            this.grouperDefaultRule.GroupImage = null;
            this.grouperDefaultRule.GroupTitle = "Default Rule";
            this.grouperDefaultRule.Location = new System.Drawing.Point(328, 184);
            this.grouperDefaultRule.Name = "grouperDefaultRule";
            this.grouperDefaultRule.Padding = new System.Windows.Forms.Padding(20);
            this.grouperDefaultRule.PaintGroupBox = true;
            this.grouperDefaultRule.RoundCorners = 4;
            this.grouperDefaultRule.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDefaultRule.ShadowControl = false;
            this.grouperDefaultRule.ShadowThickness = 1;
            this.grouperDefaultRule.Size = new System.Drawing.Size(296, 120);
            this.grouperDefaultRule.TabIndex = 6;
            // 
            // btnOpenActionForm
            // 
            this.btnOpenActionForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenActionForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenActionForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenActionForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenActionForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenActionForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenActionForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenActionForm.Location = new System.Drawing.Point(256, 88);
            this.btnOpenActionForm.Name = "btnOpenActionForm";
            this.btnOpenActionForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenActionForm.TabIndex = 5;
            this.btnOpenActionForm.Text = "...";
            this.btnOpenActionForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenActionForm.UseVisualStyleBackColor = false;
            this.btnOpenActionForm.Click += new System.EventHandler(this.btnOpenActionForm_Click);
            // 
            // btnOpenFilterForm
            // 
            this.btnOpenFilterForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFilterForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenFilterForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenFilterForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenFilterForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenFilterForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFilterForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenFilterForm.Location = new System.Drawing.Point(256, 44);
            this.btnOpenFilterForm.Name = "btnOpenFilterForm";
            this.btnOpenFilterForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenFilterForm.TabIndex = 2;
            this.btnOpenFilterForm.Text = "...";
            this.btnOpenFilterForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenFilterForm.UseVisualStyleBackColor = false;
            this.btnOpenFilterForm.Click += new System.EventHandler(this.openOpenFilterForm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(16, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Action:";
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFilter.Location = new System.Drawing.Point(16, 28);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(32, 13);
            this.lblFilter.TabIndex = 0;
            this.lblFilter.Text = "Filter:";
            // 
            // txtAction
            // 
            this.txtAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAction.BackColor = System.Drawing.SystemColors.Window;
            this.txtAction.Location = new System.Drawing.Point(16, 88);
            this.txtAction.Name = "txtAction";
            this.txtAction.Size = new System.Drawing.Size(232, 20);
            this.txtAction.TabIndex = 4;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilter.Location = new System.Drawing.Point(16, 44);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(232, 20);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.Text = "1=1";
            // 
            // grouperSubscriptionInformation
            // 
            this.grouperSubscriptionInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperSubscriptionInformation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSubscriptionInformation.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSubscriptionInformation.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperSubscriptionInformation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSubscriptionInformation.BorderThickness = 1F;
            this.grouperSubscriptionInformation.Controls.Add(this.propertyListView);
            this.grouperSubscriptionInformation.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSubscriptionInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSubscriptionInformation.ForeColor = System.Drawing.Color.White;
            this.grouperSubscriptionInformation.GroupImage = null;
            this.grouperSubscriptionInformation.GroupTitle = "Subscription Information";
            this.grouperSubscriptionInformation.Location = new System.Drawing.Point(640, 8);
            this.grouperSubscriptionInformation.Name = "grouperSubscriptionInformation";
            this.grouperSubscriptionInformation.Padding = new System.Windows.Forms.Padding(20);
            this.grouperSubscriptionInformation.PaintGroupBox = true;
            this.grouperSubscriptionInformation.RoundCorners = 4;
            this.grouperSubscriptionInformation.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSubscriptionInformation.ShadowControl = false;
            this.grouperSubscriptionInformation.ShadowThickness = 1;
            this.grouperSubscriptionInformation.Size = new System.Drawing.Size(312, 432);
            this.grouperSubscriptionInformation.TabIndex = 0;
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
            this.propertyListView.Size = new System.Drawing.Size(279, 384);
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
            // grouperSubscriptionProperties
            // 
            this.grouperSubscriptionProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperSubscriptionProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSubscriptionProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSubscriptionProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperSubscriptionProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSubscriptionProperties.BorderThickness = 1F;
            this.grouperSubscriptionProperties.Controls.Add(this.btnOpenForwardDeadLetteredMessagesToForm);
            this.grouperSubscriptionProperties.Controls.Add(this.lblForwardDeadLetteredMessagesTo);
            this.grouperSubscriptionProperties.Controls.Add(this.txtForwardDeadLetteredMessagesTo);
            this.grouperSubscriptionProperties.Controls.Add(this.btnOpenDescriptionForm);
            this.grouperSubscriptionProperties.Controls.Add(this.txtUserMetadata);
            this.grouperSubscriptionProperties.Controls.Add(this.btnOpenForwardToForm);
            this.grouperSubscriptionProperties.Controls.Add(this.lblForwardTo);
            this.grouperSubscriptionProperties.Controls.Add(this.txtForwardTo);
            this.grouperSubscriptionProperties.Controls.Add(this.label2);
            this.grouperSubscriptionProperties.Controls.Add(this.labelUserDescription);
            this.grouperSubscriptionProperties.Controls.Add(this.txtMaxDeliveryCount);
            this.grouperSubscriptionProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSubscriptionProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSubscriptionProperties.ForeColor = System.Drawing.Color.White;
            this.grouperSubscriptionProperties.GroupImage = null;
            this.grouperSubscriptionProperties.GroupTitle = "Subscription Properties";
            this.grouperSubscriptionProperties.Location = new System.Drawing.Point(16, 184);
            this.grouperSubscriptionProperties.Name = "grouperSubscriptionProperties";
            this.grouperSubscriptionProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperSubscriptionProperties.PaintGroupBox = true;
            this.grouperSubscriptionProperties.RoundCorners = 4;
            this.grouperSubscriptionProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSubscriptionProperties.ShadowControl = false;
            this.grouperSubscriptionProperties.ShadowThickness = 1;
            this.grouperSubscriptionProperties.Size = new System.Drawing.Size(296, 256);
            this.grouperSubscriptionProperties.TabIndex = 2;
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
            this.btnOpenForwardDeadLetteredMessagesToForm.Location = new System.Drawing.Point(256, 176);
            this.btnOpenForwardDeadLetteredMessagesToForm.Name = "btnOpenForwardDeadLetteredMessagesToForm";
            this.btnOpenForwardDeadLetteredMessagesToForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenForwardDeadLetteredMessagesToForm.TabIndex = 10;
            this.btnOpenForwardDeadLetteredMessagesToForm.Text = "...";
            this.btnOpenForwardDeadLetteredMessagesToForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenForwardDeadLetteredMessagesToForm.UseVisualStyleBackColor = false;
            this.btnOpenForwardDeadLetteredMessagesToForm.Click += new System.EventHandler(this.btnOpenForwardDeadLetteredMessagesToForm_Click);
            // 
            // lblForwardDeadLetteredMessagesTo
            // 
            this.lblForwardDeadLetteredMessagesTo.AutoSize = true;
            this.lblForwardDeadLetteredMessagesTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblForwardDeadLetteredMessagesTo.Location = new System.Drawing.Point(16, 160);
            this.lblForwardDeadLetteredMessagesTo.Name = "lblForwardDeadLetteredMessagesTo";
            this.lblForwardDeadLetteredMessagesTo.Size = new System.Drawing.Size(182, 13);
            this.lblForwardDeadLetteredMessagesTo.TabIndex = 8;
            this.lblForwardDeadLetteredMessagesTo.Text = "Forward Dead-lettered Messages To:";
            // 
            // txtForwardDeadLetteredMessagesTo
            // 
            this.txtForwardDeadLetteredMessagesTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtForwardDeadLetteredMessagesTo.BackColor = System.Drawing.SystemColors.Window;
            this.txtForwardDeadLetteredMessagesTo.Location = new System.Drawing.Point(16, 176);
            this.txtForwardDeadLetteredMessagesTo.Name = "txtForwardDeadLetteredMessagesTo";
            this.txtForwardDeadLetteredMessagesTo.Size = new System.Drawing.Size(232, 20);
            this.txtForwardDeadLetteredMessagesTo.TabIndex = 9;
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
            this.btnOpenDescriptionForm.Location = new System.Drawing.Point(256, 88);
            this.btnOpenDescriptionForm.Name = "btnOpenDescriptionForm";
            this.btnOpenDescriptionForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenDescriptionForm.TabIndex = 4;
            this.btnOpenDescriptionForm.Text = "...";
            this.btnOpenDescriptionForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenDescriptionForm.UseVisualStyleBackColor = false;
            this.btnOpenDescriptionForm.Click += new System.EventHandler(this.btnOpenUserMetadataForm_Click);
            // 
            // txtUserMetadata
            // 
            this.txtUserMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserMetadata.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserMetadata.Location = new System.Drawing.Point(16, 88);
            this.txtUserMetadata.Name = "txtUserMetadata";
            this.txtUserMetadata.Size = new System.Drawing.Size(232, 20);
            this.txtUserMetadata.TabIndex = 3;
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
            this.btnOpenForwardToForm.Location = new System.Drawing.Point(256, 132);
            this.btnOpenForwardToForm.Name = "btnOpenForwardToForm";
            this.btnOpenForwardToForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenForwardToForm.TabIndex = 7;
            this.btnOpenForwardToForm.Text = "...";
            this.btnOpenForwardToForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenForwardToForm.UseVisualStyleBackColor = false;
            this.btnOpenForwardToForm.Click += new System.EventHandler(this.btnOpenForwardToForm_Click);
            // 
            // lblForwardTo
            // 
            this.lblForwardTo.AutoSize = true;
            this.lblForwardTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblForwardTo.Location = new System.Drawing.Point(16, 116);
            this.lblForwardTo.Name = "lblForwardTo";
            this.lblForwardTo.Size = new System.Drawing.Size(64, 13);
            this.lblForwardTo.TabIndex = 5;
            this.lblForwardTo.Text = "Forward To:";
            // 
            // txtForwardTo
            // 
            this.txtForwardTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtForwardTo.BackColor = System.Drawing.SystemColors.Window;
            this.txtForwardTo.Location = new System.Drawing.Point(16, 132);
            this.txtForwardTo.Name = "txtForwardTo";
            this.txtForwardTo.Size = new System.Drawing.Size(232, 20);
            this.txtForwardTo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(16, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Max Delivery Count:";
            // 
            // labelUserDescription
            // 
            this.labelUserDescription.AutoSize = true;
            this.labelUserDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelUserDescription.Location = new System.Drawing.Point(16, 72);
            this.labelUserDescription.Name = "labelUserDescription";
            this.labelUserDescription.Size = new System.Drawing.Size(88, 13);
            this.labelUserDescription.TabIndex = 2;
            this.labelUserDescription.Text = "User Description:";
            // 
            // txtMaxDeliveryCount
            // 
            this.txtMaxDeliveryCount.AllowDecimal = false;
            this.txtMaxDeliveryCount.AllowNegative = false;
            this.txtMaxDeliveryCount.AllowSpace = false;
            this.txtMaxDeliveryCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxDeliveryCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaxDeliveryCount.Location = new System.Drawing.Point(16, 44);
            this.txtMaxDeliveryCount.Name = "txtMaxDeliveryCount";
            this.txtMaxDeliveryCount.Size = new System.Drawing.Size(232, 20);
            this.txtMaxDeliveryCount.TabIndex = 1;
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
            this.grouperLockDuration.Location = new System.Drawing.Point(16, 96);
            this.grouperLockDuration.Name = "grouperLockDuration";
            this.grouperLockDuration.Padding = new System.Windows.Forms.Padding(20);
            this.grouperLockDuration.PaintGroupBox = true;
            this.grouperLockDuration.RoundCorners = 4;
            this.grouperLockDuration.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperLockDuration.ShadowControl = false;
            this.grouperLockDuration.ShadowThickness = 1;
            this.grouperLockDuration.Size = new System.Drawing.Size(296, 80);
            this.grouperLockDuration.TabIndex = 1;
            // 
            // tsLockDuration
            // 
            this.tsLockDuration.Location = new System.Drawing.Point(16, 25);
            this.tsLockDuration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tsLockDuration.Name = "tsLockDuration";
            this.tsLockDuration.Size = new System.Drawing.Size(273, 42);
            this.tsLockDuration.TabIndex = 0;
            this.tsLockDuration.TimeSpanValue = null;
            // 
            // grouperSubscriptionSettings
            // 
            this.grouperSubscriptionSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperSubscriptionSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSubscriptionSettings.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSubscriptionSettings.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperSubscriptionSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSubscriptionSettings.BorderThickness = 1F;
            this.grouperSubscriptionSettings.Controls.Add(this.checkedListBox);
            this.grouperSubscriptionSettings.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSubscriptionSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSubscriptionSettings.ForeColor = System.Drawing.Color.White;
            this.grouperSubscriptionSettings.GroupImage = null;
            this.grouperSubscriptionSettings.GroupTitle = "Subscription Settings";
            this.grouperSubscriptionSettings.Location = new System.Drawing.Point(328, 312);
            this.grouperSubscriptionSettings.Name = "grouperSubscriptionSettings";
            this.grouperSubscriptionSettings.Padding = new System.Windows.Forms.Padding(20);
            this.grouperSubscriptionSettings.PaintGroupBox = true;
            this.grouperSubscriptionSettings.RoundCorners = 4;
            this.grouperSubscriptionSettings.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSubscriptionSettings.ShadowControl = false;
            this.grouperSubscriptionSettings.ShadowThickness = 1;
            this.grouperSubscriptionSettings.Size = new System.Drawing.Size(296, 128);
            this.grouperSubscriptionSettings.TabIndex = 7;
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
            "Enable Batched Operations ",
            "Enable Dead-lettering On Filter Evaluation Error",
            "Enable Dead-lettering On Message Expiration",
            "Requires Session"});
            this.checkedListBox.Location = new System.Drawing.Point(16, 32);
            this.checkedListBox.Margin = new System.Windows.Forms.Padding(8);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(264, 64);
            this.checkedListBox.TabIndex = 0;
            this.checkedListBox.ThreeDCheckBoxes = true;
            this.checkedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_ItemCheck);
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageMessages.Controls.Add(this.messagesSplitContainer);
            this.tabPageMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageMessages.Location = new System.Drawing.Point(4, 24);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessages.Size = new System.Drawing.Size(968, 452);
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
            this.pictFindMessages.TabIndex = 2;
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
            this.messagesDataGridView.Size = new System.Drawing.Size(571, 161);
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
            this.txtMessageText.AutoScrollMinSize = new System.Drawing.Size(27, 14);
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
            this.txtMessageText.Size = new System.Drawing.Size(575, 164);
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
            this.messagePropertiesSplitContainer.Panel1.Controls.Add(this.grouperMessageProperties);
            // 
            // messagePropertiesSplitContainer.Panel2
            // 
            this.messagePropertiesSplitContainer.Panel2.Controls.Add(this.grouperMessageCustomProperties);
            this.messagePropertiesSplitContainer.Size = new System.Drawing.Size(314, 432);
            this.messagePropertiesSplitContainer.SplitterDistance = 211;
            this.messagePropertiesSplitContainer.SplitterWidth = 8;
            this.messagePropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperMessageProperties
            // 
            this.grouperMessageProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.BorderThickness = 1F;
            this.grouperMessageProperties.Controls.Add(this.messagePropertyGrid);
            this.grouperMessageProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageProperties.GroupImage = null;
            this.grouperMessageProperties.GroupTitle = "Message System Properties";
            this.grouperMessageProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageProperties.Name = "grouperMessageProperties";
            this.grouperMessageProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageProperties.PaintGroupBox = true;
            this.grouperMessageProperties.RoundCorners = 4;
            this.grouperMessageProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageProperties.ShadowControl = false;
            this.grouperMessageProperties.ShadowThickness = 1;
            this.grouperMessageProperties.Size = new System.Drawing.Size(314, 211);
            this.grouperMessageProperties.TabIndex = 20;
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
            this.messagePropertyGrid.TabIndex = 1;
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
            // 
            // messageCustomPropertyGrid
            // 
            this.messageCustomPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageCustomPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.messageCustomPropertyGrid.HelpVisible = false;
            this.messageCustomPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.messageCustomPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.messageCustomPropertyGrid.Name = "messageCustomPropertyGrid";
            this.messageCustomPropertyGrid.Size = new System.Drawing.Size(281, 164);
            this.messageCustomPropertyGrid.TabIndex = 2;
            this.messageCustomPropertyGrid.ToolbarVisible = false;
            // 
            // tabPageDeadletter
            // 
            this.tabPageDeadletter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageDeadletter.Controls.Add(this.deadletterSplitContainer);
            this.tabPageDeadletter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDeadletter.Location = new System.Drawing.Point(4, 24);
            this.tabPageDeadletter.Name = "tabPageDeadletter";
            this.tabPageDeadletter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDeadletter.Size = new System.Drawing.Size(968, 452);
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
            this.pictFindDeadletter.TabIndex = 3;
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
            this.deadletterDataGridView.Size = new System.Drawing.Size(571, 161);
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
            this.txtDeadletterText.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtDeadletterText.BackBrush = null;
            this.txtDeadletterText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeadletterText.CharHeight = 14;
            this.txtDeadletterText.CharWidth = 8;
            this.txtDeadletterText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDeadletterText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtDeadletterText.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtDeadletterText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDeadletterText.IsReplaceMode = false;
            this.txtDeadletterText.Location = new System.Drawing.Point(16, 32);
            this.txtDeadletterText.Name = "txtDeadletterText";
            this.txtDeadletterText.Paddings = new System.Windows.Forms.Padding(0);
            this.txtDeadletterText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtDeadletterText.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtDeadletterText.ServiceColors")));
            this.txtDeadletterText.Size = new System.Drawing.Size(575, 164);
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
            this.deadletterPropertyGrid.Size = new System.Drawing.Size(281, 163);
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
            this.deadletterCustomPropertyGrid.HelpVisible = false;
            this.deadletterCustomPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.deadletterCustomPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.deadletterCustomPropertyGrid.Name = "deadletterCustomPropertyGrid";
            this.deadletterCustomPropertyGrid.Size = new System.Drawing.Size(281, 164);
            this.deadletterCustomPropertyGrid.TabIndex = 3;
            this.deadletterCustomPropertyGrid.ToolbarVisible = false;
            // 
            // tabPageSessions
            // 
            this.tabPageSessions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageSessions.Controls.Add(this.sessionsSplitContainer);
            this.tabPageSessions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageSessions.Location = new System.Drawing.Point(4, 24);
            this.tabPageSessions.Name = "tabPageSessions";
            this.tabPageSessions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSessions.Size = new System.Drawing.Size(968, 452);
            this.tabPageSessions.TabIndex = 6;
            this.tabPageSessions.Text = "Sessions";
            this.tabPageSessions.Resize += new System.EventHandler(this.tabPageSessions_Resize);
            // 
            // sessionsSplitContainer
            // 
            this.sessionsSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionsSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.sessionsSplitContainer.Name = "sessionsSplitContainer";
            // 
            // sessionsSplitContainer.Panel1
            // 
            this.sessionsSplitContainer.Panel1.Controls.Add(this.sessionMainSplitContainer);
            // 
            // sessionsSplitContainer.Panel2
            // 
            this.sessionsSplitContainer.Panel2.Controls.Add(this.grouperSessionProperties);
            this.sessionsSplitContainer.Size = new System.Drawing.Size(936, 432);
            this.sessionsSplitContainer.SplitterDistance = 606;
            this.sessionsSplitContainer.SplitterWidth = 16;
            this.sessionsSplitContainer.TabIndex = 4;
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
            this.sessionMainSplitContainer.TabIndex = 2;
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
            this.grouperSessionList.TabIndex = 0;
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
            this.grouperSessionState.TabIndex = 0;
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
            this.txtSessionState.Size = new System.Drawing.Size(574, 163);
            this.txtSessionState.TabIndex = 0;
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
            this.grouperSessionProperties.TabIndex = 0;
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
            this.sessionPropertyGrid.TabIndex = 0;
            this.sessionPropertyGrid.ToolbarVisible = false;
            // 
            // deadletterContextMenuStrip
            // 
            this.deadletterContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.deadletterContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repairAndResubmitDeadletterToolStripMenuItem,
            this.resubmitSelectedDeadletterInBatchModeToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveSelectedDeadletteredMessageToolStripMenuItem,
            this.saveSelectedDeadletteredMessagesToolStripMenuItem,
            this.deleteSelectedMessageToolStripMenuItem,
            this.deleteSelectedMessagesToolStripMenuItem});
            this.deadletterContextMenuStrip.Name = "registrationContextMenuStrip";
            this.deadletterContextMenuStrip.Size = new System.Drawing.Size(306, 142);
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
            // saveSelectedDeadletteredMessagesToolStripMenuItem
            // 
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Name = "saveSelectedDeadletteredMessagesToolStripMenuItem";
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Text = "Save Selected Messages";
            this.saveSelectedDeadletteredMessagesToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedDeadletteredMessagesToolStripMenuItem_Click);
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
            this.messagesContextMenuStrip.Size = new System.Drawing.Size(306, 98);
            // 
            // repairAndResubmitMessageToolStripMenuItem
            // 
            this.repairAndResubmitMessageToolStripMenuItem.Name = "repairAndResubmitMessageToolStripMenuItem";
            this.repairAndResubmitMessageToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.repairAndResubmitMessageToolStripMenuItem.Text = "Repair And Resubmit Selected Message";
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
            // saveSelectedMessagesToolStripMenuItem
            // 
            this.saveSelectedMessagesToolStripMenuItem.Name = "saveSelectedMessagesToolStripMenuItem";
            this.saveSelectedMessagesToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.saveSelectedMessagesToolStripMenuItem.Text = "Save Selected Messages";
            this.saveSelectedMessagesToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedMessagesToolStripMenuItem_Click);
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
            this.btnPurgeMessages.Location = new System.Drawing.Point(282, 504);
            this.btnPurgeMessages.Name = "btnPurgeMessages";
            this.btnPurgeMessages.Size = new System.Drawing.Size(72, 24);
            this.btnPurgeMessages.TabIndex = 1;
            this.btnPurgeMessages.Text = "Purge";
            this.btnPurgeMessages.UseVisualStyleBackColor = false;
            this.btnPurgeMessages.Click += new System.EventHandler(this.btnPurgeMessages_Click);
            this.btnPurgeMessages.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnPurgeMessages.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // HandleSubscriptionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.btnPurgeMessages);
            this.Controls.Add(this.btnPurgeDeadletterQueueMessages);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnDeadletter);
            this.Controls.Add(this.btnMessages);
            this.Controls.Add(this.btnSessions);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnChangeStatus);
            this.Controls.Add(this.btnCancelUpdate);
            this.Controls.Add(this.btnCreateDelete);
            this.Name = "HandleSubscriptionControl";
            this.Size = new System.Drawing.Size(1008, 544);
            this.mainTabControl.ResumeLayout(false);
            this.tabPageDescription.ResumeLayout(false);
            this.grouperAutoDeleteOnIdle.ResumeLayout(false);
            this.groupergrouperDefaultMessageTimeToLive.ResumeLayout(false);
            this.grouperName.ResumeLayout(false);
            this.grouperName.PerformLayout();
            this.grouperDefaultRule.ResumeLayout(false);
            this.grouperDefaultRule.PerformLayout();
            this.grouperSubscriptionInformation.ResumeLayout(false);
            this.grouperSubscriptionProperties.ResumeLayout(false);
            this.grouperSubscriptionProperties.PerformLayout();
            this.grouperLockDuration.ResumeLayout(false);
            this.grouperSubscriptionSettings.ResumeLayout(false);
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
            this.grouperMessageProperties.ResumeLayout(false);
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
            this.tabPageSessions.ResumeLayout(false);
            this.sessionsSplitContainer.Panel1.ResumeLayout(false);
            this.sessionsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionsSplitContainer)).EndInit();
            this.sessionsSplitContainer.ResumeLayout(false);
            this.sessionMainSplitContainer.Panel1.ResumeLayout(false);
            this.sessionMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionMainSplitContainer)).EndInit();
            this.sessionMainSplitContainer.ResumeLayout(false);
            this.grouperSessionList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionsDataGridView)).EndInit();
            this.grouperSessionState.ResumeLayout(false);
            this.grouperSessionState.PerformLayout();
            this.grouperSessionProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deadletterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesBindingSource)).EndInit();
            this.deadletterContextMenuStrip.ResumeLayout(false);
            this.messagesContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Button btnCancelUpdate;
        private System.Windows.Forms.Button btnCreateDelete;
        private System.Windows.Forms.Button btnDeadletter;
        private System.Windows.Forms.Button btnMessages;
        private System.Windows.Forms.Button btnSessions;
        internal System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPageDescription;
        private System.Windows.Forms.TabPage tabPageMessages;
        private System.Windows.Forms.SplitContainer messagesSplitContainer;
        private System.Windows.Forms.SplitContainer messageMainSplitContainer;
        private Grouper grouperMessageList;
        private System.Windows.Forms.DataGridView messagesDataGridView;
        private System.Windows.Forms.TabPage tabPageSessions;
        private System.Windows.Forms.SplitContainer sessionsSplitContainer;
        private Grouper grouperSessionProperties;
        private System.Windows.Forms.PropertyGrid sessionPropertyGrid;
        private System.Windows.Forms.TabPage tabPageDeadletter;
        private System.Windows.Forms.SplitContainer deadletterSplitContainer;
        private System.Windows.Forms.SplitContainer deadletterMainSplitContainer;
        private Grouper grouperDeadletterList;
        private System.Windows.Forms.DataGridView deadletterDataGridView;
        private Grouper groupergrouperDefaultMessageTimeToLive;
        private Grouper grouperName;
        private System.Windows.Forms.Label lblRelativeURI;
        private System.Windows.Forms.TextBox txtName;
        private Grouper grouperDefaultRule;
        private System.Windows.Forms.Button btnOpenActionForm;
        private System.Windows.Forms.Button btnOpenFilterForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox txtAction;
        private System.Windows.Forms.TextBox txtFilter;
        private Grouper grouperSubscriptionInformation;
        private System.Windows.Forms.ListView propertyListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private Grouper grouperSubscriptionProperties;
        private System.Windows.Forms.Button btnOpenDescriptionForm;
        private System.Windows.Forms.TextBox txtUserMetadata;
        private System.Windows.Forms.Button btnOpenForwardToForm;
        private System.Windows.Forms.Label lblForwardTo;
        private System.Windows.Forms.TextBox txtForwardTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelUserDescription;
        private ServiceBusExplorer.Controls.NumericTextBox txtMaxDeliveryCount;
        private Grouper grouperLockDuration;
        private Grouper grouperSubscriptionSettings;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private Grouper grouperAutoDeleteOnIdle;
        private System.Windows.Forms.BindingSource sessionsBindingSource;
        private System.Windows.Forms.BindingSource messagesBindingSource;
        private System.Windows.Forms.BindingSource deadletterBindingSource;
        private System.Windows.Forms.SplitContainer sessionMainSplitContainer;
        private Grouper grouperSessionList;
        private System.Windows.Forms.DataGridView sessionsDataGridView;
        private Grouper grouperSessionState;
        private System.Windows.Forms.TextBox txtSessionState;
        private System.Windows.Forms.BindingSource authorizationRulesBindingSource;
        private System.Windows.Forms.PictureBox pictFindMessages;
        private System.Windows.Forms.PictureBox pictFindDeadletter;
        private System.Windows.Forms.PictureBox pictFindMessagesByDate;
        private System.Windows.Forms.PictureBox pictFindDeadletterByDate;
        private System.Windows.Forms.Button btnOpenForwardDeadLetteredMessagesToForm;
        private System.Windows.Forms.Label lblForwardDeadLetteredMessagesTo;
        private System.Windows.Forms.TextBox txtForwardDeadLetteredMessagesTo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip deadletterContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem repairAndResubmitDeadletterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitSelectedDeadletterInBatchModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedDeadletteredMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedDeadletteredMessagesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip messagesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem repairAndResubmitMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitSelectedMessagesInBatchModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedMessagesToolStripMenuItem;
        private System.Windows.Forms.Button btnPurgeMessages;
        private System.Windows.Forms.Button btnPurgeDeadletterQueueMessages;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedMessagesToolStripMenuItem;
        private System.Windows.Forms.SplitContainer messagePropertiesSplitContainer;
        private Grouper grouperMessageProperties;
        private System.Windows.Forms.PropertyGrid messagePropertyGrid;
        private Grouper grouperMessageCustomProperties;
        private Grouper grouperMessageText;
        private FastColoredTextBoxNS.FastColoredTextBox txtMessageText;
        private System.Windows.Forms.SplitContainer deadletterPropertiesSplitContainer;
        private Grouper grouperDeadletterSystemProperties;
        private System.Windows.Forms.PropertyGrid deadletterPropertyGrid;
        private Grouper grouperDeadletterText;
        private FastColoredTextBoxNS.FastColoredTextBox txtDeadletterText;
        private Grouper grouperDeadletterCustomProperties;
        private TimeSpanControl tsAutoDeleteOnIdle;
        private TimeSpanControl tsDefaultMessageTimeToLive;
        private TimeSpanControl tsLockDuration;
        private System.Windows.Forms.PropertyGrid messageCustomPropertyGrid;
        private System.Windows.Forms.PropertyGrid deadletterCustomPropertyGrid;
    }
}
