﻿namespace ServiceBusExplorer.Controls
{
    partial class HandleTopicControl
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
            this.deadletterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sessionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.messagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.grouperAutoDeleteOnIdle = new ServiceBusExplorer.Controls.Grouper();
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
            this.groupergrouperDefaultMessageTimeToLive = new ServiceBusExplorer.Controls.Grouper();
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
            this.grouperTopicSettings = new ServiceBusExplorer.Controls.Grouper();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.grouperPath = new ServiceBusExplorer.Controls.Grouper();
            this.lblRelativeURI = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.grouperDuplicateDetectionHistoryTimeWindow = new ServiceBusExplorer.Controls.Grouper();
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
            this.grouperTopicProperties = new ServiceBusExplorer.Controls.Grouper();
            this.lblMaxTopicSizeInGB = new System.Windows.Forms.Label();
            this.trackBarMaxTopicSize = new ServiceBusExplorer.Controls.CustomTrackBar();
            this.txtUserMetadata = new System.Windows.Forms.TextBox();
            this.lblUserMetadata = new System.Windows.Forms.Label();
            this.btnOpenDescriptionForm = new System.Windows.Forms.Button();
            this.lblMaxTopicSize = new System.Windows.Forms.Label();
            this.grouperTopicInformation = new ServiceBusExplorer.Controls.Grouper();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageAuthorization = new System.Windows.Forms.TabPage();
            this.grouperAuthorizationRuleList = new ServiceBusExplorer.Controls.Grouper();
            this.authorizationRulesDataGridView = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.btnCancelUpdate = new System.Windows.Forms.Button();
            this.btnCreateDelete = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.authorizationRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).BeginInit();
            this.tabPageDescription.SuspendLayout();
            this.grouperAutoDeleteOnIdle.SuspendLayout();
            this.groupergrouperDefaultMessageTimeToLive.SuspendLayout();
            this.grouperTopicSettings.SuspendLayout();
            this.grouperPath.SuspendLayout();
            this.grouperDuplicateDetectionHistoryTimeWindow.SuspendLayout();
            this.grouperTopicProperties.SuspendLayout();
            this.grouperTopicInformation.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.tabPageAuthorization.SuspendLayout();
            this.grouperAuthorizationRuleList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageDescription.Controls.Add(this.grouperAutoDeleteOnIdle);
            this.tabPageDescription.Controls.Add(this.groupergrouperDefaultMessageTimeToLive);
            this.tabPageDescription.Controls.Add(this.grouperTopicSettings);
            this.tabPageDescription.Controls.Add(this.grouperPath);
            this.tabPageDescription.Controls.Add(this.grouperDuplicateDetectionHistoryTimeWindow);
            this.tabPageDescription.Controls.Add(this.grouperTopicProperties);
            this.tabPageDescription.Controls.Add(this.grouperTopicInformation);
            this.tabPageDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDescription.Location = new System.Drawing.Point(4, 27);
            this.tabPageDescription.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(1293, 560);
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
            this.grouperAutoDeleteOnIdle.Location = new System.Drawing.Point(21, 118);
            this.grouperAutoDeleteOnIdle.Margin = new System.Windows.Forms.Padding(4);
            this.grouperAutoDeleteOnIdle.Name = "grouperAutoDeleteOnIdle";
            this.grouperAutoDeleteOnIdle.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperAutoDeleteOnIdle.PaintGroupBox = true;
            this.grouperAutoDeleteOnIdle.RoundCorners = 4;
            this.grouperAutoDeleteOnIdle.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAutoDeleteOnIdle.ShadowControl = false;
            this.grouperAutoDeleteOnIdle.ShadowThickness = 1;
            this.grouperAutoDeleteOnIdle.Size = new System.Drawing.Size(395, 98);
            this.grouperAutoDeleteOnIdle.TabIndex = 1;
            // 
            // lblAutoDeleteOnIdleMilliseconds
            // 
            this.lblAutoDeleteOnIdleMilliseconds.AutoSize = true;
            this.lblAutoDeleteOnIdleMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleMilliseconds.Location = new System.Drawing.Point(320, 34);
            this.lblAutoDeleteOnIdleMilliseconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleMilliseconds.Name = "lblAutoDeleteOnIdleMilliseconds";
            this.lblAutoDeleteOnIdleMilliseconds.Size = new System.Drawing.Size(64, 17);
            this.lblAutoDeleteOnIdleMilliseconds.TabIndex = 25;
            this.lblAutoDeleteOnIdleMilliseconds.Text = "Millisecs:";
            // 
            // txtAutoDeleteOnIdleMilliseconds
            // 
            this.txtAutoDeleteOnIdleMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleMilliseconds.Location = new System.Drawing.Point(320, 54);
            this.txtAutoDeleteOnIdleMilliseconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutoDeleteOnIdleMilliseconds.Name = "txtAutoDeleteOnIdleMilliseconds";
            this.txtAutoDeleteOnIdleMilliseconds.Size = new System.Drawing.Size(52, 23);
            this.txtAutoDeleteOnIdleMilliseconds.TabIndex = 4;
            this.txtAutoDeleteOnIdleMilliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblAutoDeleteOnIdleSeconds
            // 
            this.lblAutoDeleteOnIdleSeconds.AutoSize = true;
            this.lblAutoDeleteOnIdleSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleSeconds.Location = new System.Drawing.Point(245, 34);
            this.lblAutoDeleteOnIdleSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleSeconds.Name = "lblAutoDeleteOnIdleSeconds";
            this.lblAutoDeleteOnIdleSeconds.Size = new System.Drawing.Size(67, 17);
            this.lblAutoDeleteOnIdleSeconds.TabIndex = 24;
            this.lblAutoDeleteOnIdleSeconds.Text = "Seconds:";
            // 
            // txtAutoDeleteOnIdleSeconds
            // 
            this.txtAutoDeleteOnIdleSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleSeconds.Location = new System.Drawing.Point(245, 54);
            this.txtAutoDeleteOnIdleSeconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutoDeleteOnIdleSeconds.Name = "txtAutoDeleteOnIdleSeconds";
            this.txtAutoDeleteOnIdleSeconds.Size = new System.Drawing.Size(52, 23);
            this.txtAutoDeleteOnIdleSeconds.TabIndex = 3;
            this.txtAutoDeleteOnIdleSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblAutoDeleteOnIdleMinutes
            // 
            this.lblAutoDeleteOnIdleMinutes.AutoSize = true;
            this.lblAutoDeleteOnIdleMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleMinutes.Location = new System.Drawing.Point(171, 34);
            this.lblAutoDeleteOnIdleMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleMinutes.Name = "lblAutoDeleteOnIdleMinutes";
            this.lblAutoDeleteOnIdleMinutes.Size = new System.Drawing.Size(61, 17);
            this.lblAutoDeleteOnIdleMinutes.TabIndex = 23;
            this.lblAutoDeleteOnIdleMinutes.Text = "Minutes:";
            // 
            // txtAutoDeleteOnIdleMinutes
            // 
            this.txtAutoDeleteOnIdleMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleMinutes.Location = new System.Drawing.Point(171, 54);
            this.txtAutoDeleteOnIdleMinutes.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutoDeleteOnIdleMinutes.Name = "txtAutoDeleteOnIdleMinutes";
            this.txtAutoDeleteOnIdleMinutes.Size = new System.Drawing.Size(52, 23);
            this.txtAutoDeleteOnIdleMinutes.TabIndex = 2;
            this.txtAutoDeleteOnIdleMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblAutoDeleteOnIdleHours
            // 
            this.lblAutoDeleteOnIdleHours.AutoSize = true;
            this.lblAutoDeleteOnIdleHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleHours.Location = new System.Drawing.Point(96, 34);
            this.lblAutoDeleteOnIdleHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleHours.Name = "lblAutoDeleteOnIdleHours";
            this.lblAutoDeleteOnIdleHours.Size = new System.Drawing.Size(50, 17);
            this.lblAutoDeleteOnIdleHours.TabIndex = 22;
            this.lblAutoDeleteOnIdleHours.Text = "Hours:";
            // 
            // lblAutoDeleteOnIdleDays
            // 
            this.lblAutoDeleteOnIdleDays.AutoSize = true;
            this.lblAutoDeleteOnIdleDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAutoDeleteOnIdleDays.Location = new System.Drawing.Point(21, 34);
            this.lblAutoDeleteOnIdleDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAutoDeleteOnIdleDays.Name = "lblAutoDeleteOnIdleDays";
            this.lblAutoDeleteOnIdleDays.Size = new System.Drawing.Size(44, 17);
            this.lblAutoDeleteOnIdleDays.TabIndex = 21;
            this.lblAutoDeleteOnIdleDays.Text = "Days:";
            // 
            // txtAutoDeleteOnIdleHours
            // 
            this.txtAutoDeleteOnIdleHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleHours.Location = new System.Drawing.Point(96, 54);
            this.txtAutoDeleteOnIdleHours.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutoDeleteOnIdleHours.Name = "txtAutoDeleteOnIdleHours";
            this.txtAutoDeleteOnIdleHours.Size = new System.Drawing.Size(52, 23);
            this.txtAutoDeleteOnIdleHours.TabIndex = 1;
            this.txtAutoDeleteOnIdleHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txtAutoDeleteOnIdleDays
            // 
            this.txtAutoDeleteOnIdleDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtAutoDeleteOnIdleDays.Location = new System.Drawing.Point(21, 54);
            this.txtAutoDeleteOnIdleDays.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutoDeleteOnIdleDays.Name = "txtAutoDeleteOnIdleDays";
            this.txtAutoDeleteOnIdleDays.Size = new System.Drawing.Size(52, 23);
            this.txtAutoDeleteOnIdleDays.TabIndex = 0;
            this.txtAutoDeleteOnIdleDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // groupergrouperDefaultMessageTimeToLive
            // 
            this.groupergrouperDefaultMessageTimeToLive.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupergrouperDefaultMessageTimeToLive.BackgroundGradientColor = System.Drawing.Color.White;
            this.groupergrouperDefaultMessageTimeToLive.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
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
            this.groupergrouperDefaultMessageTimeToLive.Location = new System.Drawing.Point(437, 118);
            this.groupergrouperDefaultMessageTimeToLive.Margin = new System.Windows.Forms.Padding(4);
            this.groupergrouperDefaultMessageTimeToLive.Name = "groupergrouperDefaultMessageTimeToLive";
            this.groupergrouperDefaultMessageTimeToLive.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.groupergrouperDefaultMessageTimeToLive.PaintGroupBox = true;
            this.groupergrouperDefaultMessageTimeToLive.RoundCorners = 4;
            this.groupergrouperDefaultMessageTimeToLive.ShadowColor = System.Drawing.Color.DarkGray;
            this.groupergrouperDefaultMessageTimeToLive.ShadowControl = false;
            this.groupergrouperDefaultMessageTimeToLive.ShadowThickness = 1;
            this.groupergrouperDefaultMessageTimeToLive.Size = new System.Drawing.Size(395, 98);
            this.groupergrouperDefaultMessageTimeToLive.TabIndex = 2;
            // 
            // lblDefaultMessageTimeToLiveMilliseconds
            // 
            this.lblDefaultMessageTimeToLiveMilliseconds.AutoSize = true;
            this.lblDefaultMessageTimeToLiveMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDefaultMessageTimeToLiveMilliseconds.Location = new System.Drawing.Point(320, 34);
            this.lblDefaultMessageTimeToLiveMilliseconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultMessageTimeToLiveMilliseconds.Name = "lblDefaultMessageTimeToLiveMilliseconds";
            this.lblDefaultMessageTimeToLiveMilliseconds.Size = new System.Drawing.Size(64, 17);
            this.lblDefaultMessageTimeToLiveMilliseconds.TabIndex = 25;
            this.lblDefaultMessageTimeToLiveMilliseconds.Text = "Millisecs:";
            // 
            // txtDefaultMessageTimeToLiveMilliseconds
            // 
            this.txtDefaultMessageTimeToLiveMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveMilliseconds.Location = new System.Drawing.Point(320, 54);
            this.txtDefaultMessageTimeToLiveMilliseconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefaultMessageTimeToLiveMilliseconds.Name = "txtDefaultMessageTimeToLiveMilliseconds";
            this.txtDefaultMessageTimeToLiveMilliseconds.Size = new System.Drawing.Size(52, 23);
            this.txtDefaultMessageTimeToLiveMilliseconds.TabIndex = 4;
            this.txtDefaultMessageTimeToLiveMilliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDefaultMessageTimeToLiveSeconds
            // 
            this.lblDefaultMessageTimeToLiveSeconds.AutoSize = true;
            this.lblDefaultMessageTimeToLiveSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDefaultMessageTimeToLiveSeconds.Location = new System.Drawing.Point(245, 34);
            this.lblDefaultMessageTimeToLiveSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultMessageTimeToLiveSeconds.Name = "lblDefaultMessageTimeToLiveSeconds";
            this.lblDefaultMessageTimeToLiveSeconds.Size = new System.Drawing.Size(67, 17);
            this.lblDefaultMessageTimeToLiveSeconds.TabIndex = 24;
            this.lblDefaultMessageTimeToLiveSeconds.Text = "Seconds:";
            // 
            // txtDefaultMessageTimeToLiveSeconds
            // 
            this.txtDefaultMessageTimeToLiveSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveSeconds.Location = new System.Drawing.Point(245, 54);
            this.txtDefaultMessageTimeToLiveSeconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefaultMessageTimeToLiveSeconds.Name = "txtDefaultMessageTimeToLiveSeconds";
            this.txtDefaultMessageTimeToLiveSeconds.Size = new System.Drawing.Size(52, 23);
            this.txtDefaultMessageTimeToLiveSeconds.TabIndex = 3;
            this.txtDefaultMessageTimeToLiveSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDefaultMessageTimeToLiveMinutes
            // 
            this.lblDefaultMessageTimeToLiveMinutes.AutoSize = true;
            this.lblDefaultMessageTimeToLiveMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDefaultMessageTimeToLiveMinutes.Location = new System.Drawing.Point(171, 34);
            this.lblDefaultMessageTimeToLiveMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultMessageTimeToLiveMinutes.Name = "lblDefaultMessageTimeToLiveMinutes";
            this.lblDefaultMessageTimeToLiveMinutes.Size = new System.Drawing.Size(61, 17);
            this.lblDefaultMessageTimeToLiveMinutes.TabIndex = 23;
            this.lblDefaultMessageTimeToLiveMinutes.Text = "Minutes:";
            // 
            // txtDefaultMessageTimeToLiveMinutes
            // 
            this.txtDefaultMessageTimeToLiveMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveMinutes.Location = new System.Drawing.Point(171, 54);
            this.txtDefaultMessageTimeToLiveMinutes.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefaultMessageTimeToLiveMinutes.Name = "txtDefaultMessageTimeToLiveMinutes";
            this.txtDefaultMessageTimeToLiveMinutes.Size = new System.Drawing.Size(52, 23);
            this.txtDefaultMessageTimeToLiveMinutes.TabIndex = 2;
            this.txtDefaultMessageTimeToLiveMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lbllblDefaultMessageTimeToLiveHours
            // 
            this.lbllblDefaultMessageTimeToLiveHours.AutoSize = true;
            this.lbllblDefaultMessageTimeToLiveHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbllblDefaultMessageTimeToLiveHours.Location = new System.Drawing.Point(96, 34);
            this.lbllblDefaultMessageTimeToLiveHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbllblDefaultMessageTimeToLiveHours.Name = "lbllblDefaultMessageTimeToLiveHours";
            this.lbllblDefaultMessageTimeToLiveHours.Size = new System.Drawing.Size(50, 17);
            this.lbllblDefaultMessageTimeToLiveHours.TabIndex = 22;
            this.lbllblDefaultMessageTimeToLiveHours.Text = "Hours:";
            // 
            // lblDefaultMessageTimeToLiveDays
            // 
            this.lblDefaultMessageTimeToLiveDays.AutoSize = true;
            this.lblDefaultMessageTimeToLiveDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDefaultMessageTimeToLiveDays.Location = new System.Drawing.Point(21, 34);
            this.lblDefaultMessageTimeToLiveDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDefaultMessageTimeToLiveDays.Name = "lblDefaultMessageTimeToLiveDays";
            this.lblDefaultMessageTimeToLiveDays.Size = new System.Drawing.Size(44, 17);
            this.lblDefaultMessageTimeToLiveDays.TabIndex = 21;
            this.lblDefaultMessageTimeToLiveDays.Text = "Days:";
            // 
            // txtDefaultMessageTimeToLiveHours
            // 
            this.txtDefaultMessageTimeToLiveHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveHours.Location = new System.Drawing.Point(96, 54);
            this.txtDefaultMessageTimeToLiveHours.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefaultMessageTimeToLiveHours.Name = "txtDefaultMessageTimeToLiveHours";
            this.txtDefaultMessageTimeToLiveHours.Size = new System.Drawing.Size(52, 23);
            this.txtDefaultMessageTimeToLiveHours.TabIndex = 1;
            this.txtDefaultMessageTimeToLiveHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txtDefaultMessageTimeToLiveDays
            // 
            this.txtDefaultMessageTimeToLiveDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultMessageTimeToLiveDays.Location = new System.Drawing.Point(21, 54);
            this.txtDefaultMessageTimeToLiveDays.Margin = new System.Windows.Forms.Padding(4);
            this.txtDefaultMessageTimeToLiveDays.Name = "txtDefaultMessageTimeToLiveDays";
            this.txtDefaultMessageTimeToLiveDays.Size = new System.Drawing.Size(52, 23);
            this.txtDefaultMessageTimeToLiveDays.TabIndex = 0;
            this.txtDefaultMessageTimeToLiveDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // grouperTopicSettings
            // 
            this.grouperTopicSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperTopicSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperTopicSettings.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperTopicSettings.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperTopicSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTopicSettings.BorderThickness = 1F;
            this.grouperTopicSettings.Controls.Add(this.checkedListBox);
            this.grouperTopicSettings.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTopicSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperTopicSettings.ForeColor = System.Drawing.Color.White;
            this.grouperTopicSettings.GroupImage = null;
            this.grouperTopicSettings.GroupTitle = "Topic Settings";
            this.grouperTopicSettings.Location = new System.Drawing.Point(437, 335);
            this.grouperTopicSettings.Margin = new System.Windows.Forms.Padding(4);
            this.grouperTopicSettings.Name = "grouperTopicSettings";
            this.grouperTopicSettings.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperTopicSettings.PaintGroupBox = true;
            this.grouperTopicSettings.RoundCorners = 4;
            this.grouperTopicSettings.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperTopicSettings.ShadowControl = false;
            this.grouperTopicSettings.ShadowThickness = 1;
            this.grouperTopicSettings.Size = new System.Drawing.Size(395, 207);
            this.grouperTopicSettings.TabIndex = 5;
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
            "Enable Filtering Messages Before Publishing",
            "Enable Partitioning",
            "Enable Express",
            "Requires Duplicate Detection",
            "Enforce Message Ordering",
            "Is Anonymous Accessible"});
            this.checkedListBox.Location = new System.Drawing.Point(21, 53);
            this.checkedListBox.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(351, 130);
            this.checkedListBox.TabIndex = 0;
            this.checkedListBox.ThreeDCheckBoxes = true;
            this.checkedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_ItemCheck);
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
            this.grouperPath.Location = new System.Drawing.Point(21, 10);
            this.grouperPath.Margin = new System.Windows.Forms.Padding(4);
            this.grouperPath.Name = "grouperPath";
            this.grouperPath.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperPath.PaintGroupBox = true;
            this.grouperPath.RoundCorners = 4;
            this.grouperPath.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperPath.ShadowControl = false;
            this.grouperPath.ShadowThickness = 1;
            this.grouperPath.Size = new System.Drawing.Size(811, 98);
            this.grouperPath.TabIndex = 0;
            // 
            // lblRelativeURI
            // 
            this.lblRelativeURI.AutoSize = true;
            this.lblRelativeURI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRelativeURI.Location = new System.Drawing.Point(21, 34);
            this.lblRelativeURI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelativeURI.Name = "lblRelativeURI";
            this.lblRelativeURI.Size = new System.Drawing.Size(90, 17);
            this.lblRelativeURI.TabIndex = 22;
            this.lblRelativeURI.Text = "Relative URI:";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPath.Location = new System.Drawing.Point(21, 54);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(767, 23);
            this.txtPath.TabIndex = 0;
            // 
            // grouperDuplicateDetectionHistoryTimeWindow
            // 
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperDuplicateDetectionHistoryTimeWindow.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
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
            this.grouperDuplicateDetectionHistoryTimeWindow.Location = new System.Drawing.Point(437, 226);
            this.grouperDuplicateDetectionHistoryTimeWindow.Margin = new System.Windows.Forms.Padding(4);
            this.grouperDuplicateDetectionHistoryTimeWindow.Name = "grouperDuplicateDetectionHistoryTimeWindow";
            this.grouperDuplicateDetectionHistoryTimeWindow.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperDuplicateDetectionHistoryTimeWindow.PaintGroupBox = true;
            this.grouperDuplicateDetectionHistoryTimeWindow.RoundCorners = 4;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowControl = false;
            this.grouperDuplicateDetectionHistoryTimeWindow.ShadowThickness = 1;
            this.grouperDuplicateDetectionHistoryTimeWindow.Size = new System.Drawing.Size(395, 98);
            this.grouperDuplicateDetectionHistoryTimeWindow.TabIndex = 4;
            // 
            // lblDuplicateDetectionHistoryTimeWindowMilliseconds
            // 
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Location = new System.Drawing.Point(320, 34);
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Name = "lblDuplicateDetectionHistoryTimeWindowMilliseconds";
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Size = new System.Drawing.Size(64, 17);
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.TabIndex = 25;
            this.lblDuplicateDetectionHistoryTimeWindowMilliseconds.Text = "Millisecs:";
            // 
            // txtDuplicateDetectionHistoryTimeWindowMilliseconds
            // 
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.Location = new System.Drawing.Point(320, 54);
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.Name = "txtDuplicateDetectionHistoryTimeWindowMilliseconds";
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.Size = new System.Drawing.Size(52, 23);
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.TabIndex = 4;
            this.txtDuplicateDetectionHistoryTimeWindowMilliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDuplicateDetectionHistoryTimeWindowSeconds
            // 
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Location = new System.Drawing.Point(245, 34);
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Name = "lblDuplicateDetectionHistoryTimeWindowSeconds";
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Size = new System.Drawing.Size(67, 17);
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.TabIndex = 24;
            this.lblDuplicateDetectionHistoryTimeWindowSeconds.Text = "Seconds:";
            // 
            // txtDuplicateDetectionHistoryTimeWindowSeconds
            // 
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.Location = new System.Drawing.Point(245, 54);
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.Name = "txtDuplicateDetectionHistoryTimeWindowSeconds";
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.Size = new System.Drawing.Size(52, 23);
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.TabIndex = 3;
            this.txtDuplicateDetectionHistoryTimeWindowSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDuplicateDetectionHistoryTimeWindowMinutes
            // 
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Location = new System.Drawing.Point(171, 34);
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Name = "lblDuplicateDetectionHistoryTimeWindowMinutes";
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Size = new System.Drawing.Size(61, 17);
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.TabIndex = 23;
            this.lblDuplicateDetectionHistoryTimeWindowMinutes.Text = "Minutes:";
            // 
            // txtDuplicateDetectionHistoryTimeWindowMinutes
            // 
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.Location = new System.Drawing.Point(171, 54);
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.Margin = new System.Windows.Forms.Padding(4);
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.Name = "txtDuplicateDetectionHistoryTimeWindowMinutes";
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.Size = new System.Drawing.Size(52, 23);
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.TabIndex = 2;
            this.txtDuplicateDetectionHistoryTimeWindowMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblDuplicateDetectionHistoryTimeWindowHours
            // 
            this.lblDuplicateDetectionHistoryTimeWindowHours.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowHours.Location = new System.Drawing.Point(96, 34);
            this.lblDuplicateDetectionHistoryTimeWindowHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowHours.Name = "lblDuplicateDetectionHistoryTimeWindowHours";
            this.lblDuplicateDetectionHistoryTimeWindowHours.Size = new System.Drawing.Size(50, 17);
            this.lblDuplicateDetectionHistoryTimeWindowHours.TabIndex = 22;
            this.lblDuplicateDetectionHistoryTimeWindowHours.Text = "Hours:";
            // 
            // lblDuplicateDetectionHistoryTimeWindowDays
            // 
            this.lblDuplicateDetectionHistoryTimeWindowDays.AutoSize = true;
            this.lblDuplicateDetectionHistoryTimeWindowDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuplicateDetectionHistoryTimeWindowDays.Location = new System.Drawing.Point(21, 34);
            this.lblDuplicateDetectionHistoryTimeWindowDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuplicateDetectionHistoryTimeWindowDays.Name = "lblDuplicateDetectionHistoryTimeWindowDays";
            this.lblDuplicateDetectionHistoryTimeWindowDays.Size = new System.Drawing.Size(44, 17);
            this.lblDuplicateDetectionHistoryTimeWindowDays.TabIndex = 21;
            this.lblDuplicateDetectionHistoryTimeWindowDays.Text = "Days:";
            // 
            // txtDuplicateDetectionHistoryTimeWindowHours
            // 
            this.txtDuplicateDetectionHistoryTimeWindowHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowHours.Location = new System.Drawing.Point(96, 54);
            this.txtDuplicateDetectionHistoryTimeWindowHours.Margin = new System.Windows.Forms.Padding(4);
            this.txtDuplicateDetectionHistoryTimeWindowHours.Name = "txtDuplicateDetectionHistoryTimeWindowHours";
            this.txtDuplicateDetectionHistoryTimeWindowHours.Size = new System.Drawing.Size(52, 23);
            this.txtDuplicateDetectionHistoryTimeWindowHours.TabIndex = 1;
            this.txtDuplicateDetectionHistoryTimeWindowHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // txtDuplicateDetectionHistoryTimeWindowDays
            // 
            this.txtDuplicateDetectionHistoryTimeWindowDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtDuplicateDetectionHistoryTimeWindowDays.Location = new System.Drawing.Point(21, 54);
            this.txtDuplicateDetectionHistoryTimeWindowDays.Margin = new System.Windows.Forms.Padding(4);
            this.txtDuplicateDetectionHistoryTimeWindowDays.Name = "txtDuplicateDetectionHistoryTimeWindowDays";
            this.txtDuplicateDetectionHistoryTimeWindowDays.Size = new System.Drawing.Size(52, 23);
            this.txtDuplicateDetectionHistoryTimeWindowDays.TabIndex = 0;
            this.txtDuplicateDetectionHistoryTimeWindowDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // grouperTopicProperties
            // 
            this.grouperTopicProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperTopicProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperTopicProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperTopicProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperTopicProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTopicProperties.BorderThickness = 1F;
            this.grouperTopicProperties.Controls.Add(this.lblMaxTopicSizeInGB);
            this.grouperTopicProperties.Controls.Add(this.trackBarMaxTopicSize);
            this.grouperTopicProperties.Controls.Add(this.txtUserMetadata);
            this.grouperTopicProperties.Controls.Add(this.lblUserMetadata);
            this.grouperTopicProperties.Controls.Add(this.btnOpenDescriptionForm);
            this.grouperTopicProperties.Controls.Add(this.lblMaxTopicSize);
            this.grouperTopicProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTopicProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperTopicProperties.ForeColor = System.Drawing.Color.White;
            this.grouperTopicProperties.GroupImage = null;
            this.grouperTopicProperties.GroupTitle = "Topic Properties";
            this.grouperTopicProperties.Location = new System.Drawing.Point(21, 226);
            this.grouperTopicProperties.Margin = new System.Windows.Forms.Padding(4);
            this.grouperTopicProperties.Name = "grouperTopicProperties";
            this.grouperTopicProperties.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperTopicProperties.PaintGroupBox = true;
            this.grouperTopicProperties.RoundCorners = 4;
            this.grouperTopicProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperTopicProperties.ShadowControl = false;
            this.grouperTopicProperties.ShadowThickness = 1;
            this.grouperTopicProperties.Size = new System.Drawing.Size(395, 315);
            this.grouperTopicProperties.TabIndex = 3;
            // 
            // lblMaxTopicSizeInGB
            // 
            this.lblMaxTopicSizeInGB.AutoSize = true;
            this.lblMaxTopicSizeInGB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxTopicSizeInGB.Location = new System.Drawing.Point(336, 59);
            this.lblMaxTopicSizeInGB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxTopicSizeInGB.Name = "lblMaxTopicSizeInGB";
            this.lblMaxTopicSizeInGB.Size = new System.Drawing.Size(40, 17);
            this.lblMaxTopicSizeInGB.TabIndex = 33;
            this.lblMaxTopicSizeInGB.Text = "1 GB";
            // 
            // trackBarMaxTopicSize
            // 
            this.trackBarMaxTopicSize.BackColor = System.Drawing.Color.Transparent;
            this.trackBarMaxTopicSize.BorderColor = System.Drawing.Color.Black;
            this.trackBarMaxTopicSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarMaxTopicSize.ForeColor = System.Drawing.Color.Black;
            this.trackBarMaxTopicSize.IndentHeight = 6;
            this.trackBarMaxTopicSize.LargeChange = 1;
            this.trackBarMaxTopicSize.Location = new System.Drawing.Point(11, 49);
            this.trackBarMaxTopicSize.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarMaxTopicSize.Maximum = 10;
            this.trackBarMaxTopicSize.Minimum = 1;
            this.trackBarMaxTopicSize.Name = "trackBarMaxTopicSize";
            this.trackBarMaxTopicSize.Size = new System.Drawing.Size(331, 29);
            this.trackBarMaxTopicSize.TabIndex = 35;
            this.trackBarMaxTopicSize.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMaxTopicSize.TickColor = System.Drawing.Color.Black;
            this.trackBarMaxTopicSize.TickHeight = 4;
            this.trackBarMaxTopicSize.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarMaxTopicSize.TrackerSize = new System.Drawing.Size(12, 12);
            this.trackBarMaxTopicSize.TrackLineBrushStyle = ServiceBusExplorer.Controls.BrushStyle.Solid;
            this.trackBarMaxTopicSize.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarMaxTopicSize.TrackLineHeight = 1;
            this.trackBarMaxTopicSize.Value = 1;
            this.trackBarMaxTopicSize.ValueChanged += new ServiceBusExplorer.Controls.ValueChangedHandler(this.trackBarMaxTopicSize_ValueChanged);
            // 
            // txtUserMetadata
            // 
            this.txtUserMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserMetadata.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserMetadata.Location = new System.Drawing.Point(21, 108);
            this.txtUserMetadata.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserMetadata.MaxLength = 0;
            this.txtUserMetadata.Multiline = true;
            this.txtUserMetadata.Name = "txtUserMetadata";
            this.txtUserMetadata.Size = new System.Drawing.Size(308, 186);
            this.txtUserMetadata.TabIndex = 2;
            // 
            // lblUserMetadata
            // 
            this.lblUserMetadata.AutoSize = true;
            this.lblUserMetadata.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserMetadata.Location = new System.Drawing.Point(21, 89);
            this.lblUserMetadata.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserMetadata.Name = "lblUserMetadata";
            this.lblUserMetadata.Size = new System.Drawing.Size(117, 17);
            this.lblUserMetadata.TabIndex = 27;
            this.lblUserMetadata.Text = "User Description:";
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
            this.btnOpenDescriptionForm.Location = new System.Drawing.Point(341, 108);
            this.btnOpenDescriptionForm.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenDescriptionForm.Name = "btnOpenDescriptionForm";
            this.btnOpenDescriptionForm.Size = new System.Drawing.Size(32, 26);
            this.btnOpenDescriptionForm.TabIndex = 3;
            this.btnOpenDescriptionForm.Text = "...";
            this.btnOpenDescriptionForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenDescriptionForm.UseVisualStyleBackColor = false;
            this.btnOpenDescriptionForm.Click += new System.EventHandler(this.btnOpenUserMetadataForm_Click);
            this.btnOpenDescriptionForm.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOpenDescriptionForm.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // lblMaxTopicSize
            // 
            this.lblMaxTopicSize.AutoSize = true;
            this.lblMaxTopicSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxTopicSize.Location = new System.Drawing.Point(21, 34);
            this.lblMaxTopicSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxTopicSize.Name = "lblMaxTopicSize";
            this.lblMaxTopicSize.Size = new System.Drawing.Size(154, 17);
            this.lblMaxTopicSize.TabIndex = 24;
            this.lblMaxTopicSize.Text = "Max Queue Size In GB:";
            // 
            // grouperTopicInformation
            // 
            this.grouperTopicInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperTopicInformation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperTopicInformation.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperTopicInformation.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperTopicInformation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTopicInformation.BorderThickness = 1F;
            this.grouperTopicInformation.Controls.Add(this.propertyListView);
            this.grouperTopicInformation.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTopicInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperTopicInformation.ForeColor = System.Drawing.Color.White;
            this.grouperTopicInformation.GroupImage = null;
            this.grouperTopicInformation.GroupTitle = "Topic Information";
            this.grouperTopicInformation.Location = new System.Drawing.Point(853, 10);
            this.grouperTopicInformation.Margin = new System.Windows.Forms.Padding(4);
            this.grouperTopicInformation.Name = "grouperTopicInformation";
            this.grouperTopicInformation.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperTopicInformation.PaintGroupBox = true;
            this.grouperTopicInformation.RoundCorners = 4;
            this.grouperTopicInformation.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperTopicInformation.ShadowControl = false;
            this.grouperTopicInformation.ShadowThickness = 1;
            this.grouperTopicInformation.Size = new System.Drawing.Size(416, 532);
            this.grouperTopicInformation.TabIndex = 6;
            // 
            // propertyListView
            // 
            this.propertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.valueColumnHeader});
            this.propertyListView.Location = new System.Drawing.Point(21, 39);
            this.propertyListView.Margin = new System.Windows.Forms.Padding(4);
            this.propertyListView.Name = "propertyListView";
            this.propertyListView.OwnerDraw = true;
            this.propertyListView.Size = new System.Drawing.Size(371, 472);
            this.propertyListView.TabIndex = 0;
            this.propertyListView.UseCompatibleStateImageBehavior = false;
            this.propertyListView.View = System.Windows.Forms.View.Details;
            this.propertyListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.propertyListView_DrawColumnHeader);
            this.propertyListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.propertyListView_DrawItem);
            this.propertyListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.propertyListView_DrawSubItem);
            this.propertyListView.Resize += new System.EventHandler(this.propertyListView_Resize);
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
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.tabPageDescription);
            this.mainTabControl.Controls.Add(this.tabPageAuthorization);
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(21, 20);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1301, 591);
            this.mainTabControl.TabIndex = 19;
            this.mainTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mainTabControl_DrawItem);
            // 
            // tabPageAuthorization
            // 
            this.tabPageAuthorization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageAuthorization.Controls.Add(this.grouperAuthorizationRuleList);
            this.tabPageAuthorization.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageAuthorization.Location = new System.Drawing.Point(4, 27);
            this.tabPageAuthorization.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageAuthorization.Name = "tabPageAuthorization";
            this.tabPageAuthorization.Size = new System.Drawing.Size(1293, 560);
            this.tabPageAuthorization.TabIndex = 3;
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
            this.grouperAuthorizationRuleList.Location = new System.Drawing.Point(21, 10);
            this.grouperAuthorizationRuleList.Margin = new System.Windows.Forms.Padding(4);
            this.grouperAuthorizationRuleList.Name = "grouperAuthorizationRuleList";
            this.grouperAuthorizationRuleList.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperAuthorizationRuleList.PaintGroupBox = true;
            this.grouperAuthorizationRuleList.RoundCorners = 4;
            this.grouperAuthorizationRuleList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAuthorizationRuleList.ShadowControl = false;
            this.grouperAuthorizationRuleList.ShadowThickness = 1;
            this.grouperAuthorizationRuleList.Size = new System.Drawing.Size(1248, 532);
            this.grouperAuthorizationRuleList.TabIndex = 21;
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
            this.authorizationRulesDataGridView.Location = new System.Drawing.Point(21, 39);
            this.authorizationRulesDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.authorizationRulesDataGridView.MultiSelect = false;
            this.authorizationRulesDataGridView.Name = "authorizationRulesDataGridView";
            this.authorizationRulesDataGridView.RowHeadersWidth = 24;
            this.authorizationRulesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.authorizationRulesDataGridView.ShowCellErrors = false;
            this.authorizationRulesDataGridView.ShowRowErrors = false;
            this.authorizationRulesDataGridView.Size = new System.Drawing.Size(1205, 474);
            this.authorizationRulesDataGridView.TabIndex = 0;
            this.authorizationRulesDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.authorizationRulesDataGridView_CellContentClick);
            this.authorizationRulesDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.authorizationRulesDataGridView_DataError);
            this.authorizationRulesDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.authorizationRulesDataGridView_RowEnter);
            this.authorizationRulesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.authorizationRulesDataGridView_RowsAdded);
            this.authorizationRulesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.authorizationRulesDataGridView_RowsRemoved);
            this.authorizationRulesDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.authorizationRulesDataGridView_UserDeletingRow);
            this.authorizationRulesDataGridView.Resize += new System.EventHandler(this.authorizationRulesDataGridView_Resize);
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
            this.btnRefresh.Location = new System.Drawing.Point(907, 620);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(96, 30);
            this.btnRefresh.TabIndex = 2;
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
            this.btnChangeStatus.Location = new System.Drawing.Point(1013, 620);
            this.btnChangeStatus.Margin = new System.Windows.Forms.Padding(4);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(96, 30);
            this.btnChangeStatus.TabIndex = 3;
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
            this.btnCancelUpdate.Location = new System.Drawing.Point(1227, 620);
            this.btnCancelUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelUpdate.Name = "btnCancelUpdate";
            this.btnCancelUpdate.Size = new System.Drawing.Size(96, 30);
            this.btnCancelUpdate.TabIndex = 5;
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
            this.btnCreateDelete.Location = new System.Drawing.Point(1120, 620);
            this.btnCreateDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateDelete.Name = "btnCreateDelete";
            this.btnCreateDelete.Size = new System.Drawing.Size(96, 30);
            this.btnCreateDelete.TabIndex = 4;
            this.btnCreateDelete.Text = "Create";
            this.btnCreateDelete.UseVisualStyleBackColor = false;
            this.btnCreateDelete.Click += new System.EventHandler(this.btnCreateDelete_Click);
            this.btnCreateDelete.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCreateDelete.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // HandleTopicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnChangeStatus);
            this.Controls.Add(this.btnCancelUpdate);
            this.Controls.Add(this.btnCreateDelete);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HandleTopicControl";
            this.Size = new System.Drawing.Size(1344, 670);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).EndInit();
            this.tabPageDescription.ResumeLayout(false);
            this.grouperAutoDeleteOnIdle.ResumeLayout(false);
            this.grouperAutoDeleteOnIdle.PerformLayout();
            this.groupergrouperDefaultMessageTimeToLive.ResumeLayout(false);
            this.groupergrouperDefaultMessageTimeToLive.PerformLayout();
            this.grouperTopicSettings.ResumeLayout(false);
            this.grouperPath.ResumeLayout(false);
            this.grouperPath.PerformLayout();
            this.grouperDuplicateDetectionHistoryTimeWindow.ResumeLayout(false);
            this.grouperDuplicateDetectionHistoryTimeWindow.PerformLayout();
            this.grouperTopicProperties.ResumeLayout(false);
            this.grouperTopicProperties.PerformLayout();
            this.grouperTopicInformation.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.tabPageAuthorization.ResumeLayout(false);
            this.grouperAuthorizationRuleList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.BindingSource deadletterBindingSource;
        private System.Windows.Forms.BindingSource sessionsBindingSource;
        private System.Windows.Forms.BindingSource messagesBindingSource;
        private System.Windows.Forms.TabPage tabPageDescription;
        internal System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Button btnCancelUpdate;
        private System.Windows.Forms.Button btnCreateDelete;
        private System.Windows.Forms.ToolTip toolTip1;
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
        private Grouper grouperTopicSettings;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private Grouper grouperPath;
        private System.Windows.Forms.Label lblRelativeURI;
        private System.Windows.Forms.TextBox txtPath;
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
        private Grouper grouperTopicProperties;
        private System.Windows.Forms.Label lblMaxTopicSizeInGB;
        private CustomTrackBar trackBarMaxTopicSize;
        private System.Windows.Forms.TextBox txtUserMetadata;
        private System.Windows.Forms.Label lblUserMetadata;
        private System.Windows.Forms.Button btnOpenDescriptionForm;
        private System.Windows.Forms.Label lblMaxTopicSize;
        private Grouper grouperTopicInformation;
        private System.Windows.Forms.ListView propertyListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
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
        private System.Windows.Forms.TabPage tabPageAuthorization;
        private Grouper grouperAuthorizationRuleList;
        private System.Windows.Forms.DataGridView authorizationRulesDataGridView;
        private System.Windows.Forms.BindingSource authorizationRulesBindingSource;
    }
}
