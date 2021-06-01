namespace ServiceBusExplorer.Controls
{
    partial class HandleEventHubControl
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
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.grouperPath = new ServiceBusExplorer.Controls.Grouper();
            this.lblRelativeURI = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.grouperEventHubProperties = new ServiceBusExplorer.Controls.Grouper();
            this.lblMessageRetentionInDaysValue = new System.Windows.Forms.Label();
            this.lblMessageRetentionInDays = new System.Windows.Forms.Label();
            this.trackBarMessageRetentionInDays = new ServiceBusExplorer.Controls.CustomTrackBar();
            this.lblPartitionCount = new System.Windows.Forms.Label();
            this.trackBarPartitionCount = new ServiceBusExplorer.Controls.CustomTrackBar();
            this.lblPartitionCountValue = new System.Windows.Forms.Label();
            this.txtUserMetadata = new System.Windows.Forms.TextBox();
            this.lblUserMetadata = new System.Windows.Forms.Label();
            this.grouperEventHubInformation = new ServiceBusExplorer.Controls.Grouper();
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
            this.authorizationRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entityInformationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPartitionInformationToClipboardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataPointDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPageDescription.SuspendLayout();
            this.grouperPath.SuspendLayout();
            this.grouperEventHubProperties.SuspendLayout();
            this.grouperEventHubInformation.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.tabPageAuthorization.SuspendLayout();
            this.grouperAuthorizationRuleList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesBindingSource)).BeginInit();
            this.entityInformationContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageDescription.Controls.Add(this.grouperPath);
            this.tabPageDescription.Controls.Add(this.grouperEventHubProperties);
            this.tabPageDescription.Controls.Add(this.grouperEventHubInformation);
            this.tabPageDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDescription.Location = new System.Drawing.Point(4, 24);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(968, 452);
            this.tabPageDescription.TabIndex = 2;
            this.tabPageDescription.Text = "Description";
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
            this.grouperPath.Size = new System.Drawing.Size(608, 80);
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
            this.txtPath.Size = new System.Drawing.Size(576, 20);
            this.txtPath.TabIndex = 1;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // grouperEventHubProperties
            // 
            this.grouperEventHubProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperEventHubProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperEventHubProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperEventHubProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperEventHubProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventHubProperties.BorderThickness = 1F;
            this.grouperEventHubProperties.Controls.Add(this.lblMessageRetentionInDaysValue);
            this.grouperEventHubProperties.Controls.Add(this.lblMessageRetentionInDays);
            this.grouperEventHubProperties.Controls.Add(this.trackBarMessageRetentionInDays);
            this.grouperEventHubProperties.Controls.Add(this.lblPartitionCount);
            this.grouperEventHubProperties.Controls.Add(this.trackBarPartitionCount);
            this.grouperEventHubProperties.Controls.Add(this.lblPartitionCountValue);
            this.grouperEventHubProperties.Controls.Add(this.txtUserMetadata);
            this.grouperEventHubProperties.Controls.Add(this.lblUserMetadata);
            this.grouperEventHubProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventHubProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperEventHubProperties.ForeColor = System.Drawing.Color.White;
            this.grouperEventHubProperties.GroupImage = null;
            this.grouperEventHubProperties.GroupTitle = "Event Hub Properties";
            this.grouperEventHubProperties.Location = new System.Drawing.Point(16, 96);
            this.grouperEventHubProperties.Name = "grouperEventHubProperties";
            this.grouperEventHubProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperEventHubProperties.PaintGroupBox = true;
            this.grouperEventHubProperties.RoundCorners = 4;
            this.grouperEventHubProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEventHubProperties.ShadowControl = false;
            this.grouperEventHubProperties.ShadowThickness = 1;
            this.grouperEventHubProperties.Size = new System.Drawing.Size(608, 344);
            this.grouperEventHubProperties.TabIndex = 1;
            // 
            // lblMessageRetentionInDaysValue
            // 
            this.lblMessageRetentionInDaysValue.AutoSize = true;
            this.lblMessageRetentionInDaysValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageRetentionInDaysValue.Location = new System.Drawing.Point(272, 316);
            this.lblMessageRetentionInDaysValue.Name = "lblMessageRetentionInDaysValue";
            this.lblMessageRetentionInDaysValue.Size = new System.Drawing.Size(19, 13);
            this.lblMessageRetentionInDaysValue.TabIndex = 4;
            this.lblMessageRetentionInDaysValue.Text = "16";
            // 
            // lblMessageRetentionInDays
            // 
            this.lblMessageRetentionInDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMessageRetentionInDays.AutoSize = true;
            this.lblMessageRetentionInDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageRetentionInDays.Location = new System.Drawing.Point(16, 296);
            this.lblMessageRetentionInDays.Name = "lblMessageRetentionInDays";
            this.lblMessageRetentionInDays.Size = new System.Drawing.Size(141, 13);
            this.lblMessageRetentionInDays.TabIndex = 2;
            this.lblMessageRetentionInDays.Text = "Message Retention In Days:";
            // 
            // trackBarMessageRetentionInDays
            // 
            this.trackBarMessageRetentionInDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarMessageRetentionInDays.BackColor = System.Drawing.Color.Transparent;
            this.trackBarMessageRetentionInDays.BorderColor = System.Drawing.Color.Black;
            this.trackBarMessageRetentionInDays.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarMessageRetentionInDays.ForeColor = System.Drawing.Color.Black;
            this.trackBarMessageRetentionInDays.IndentHeight = 6;
            this.trackBarMessageRetentionInDays.LargeChange = 1;
            this.trackBarMessageRetentionInDays.Location = new System.Drawing.Point(16, 308);
            this.trackBarMessageRetentionInDays.Maximum = 7;
            this.trackBarMessageRetentionInDays.Minimum = 1;
            this.trackBarMessageRetentionInDays.Name = "trackBarMessageRetentionInDays";
            this.trackBarMessageRetentionInDays.Size = new System.Drawing.Size(256, 29);
            this.trackBarMessageRetentionInDays.TabIndex = 3;
            this.trackBarMessageRetentionInDays.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMessageRetentionInDays.TickColor = System.Drawing.Color.Black;
            this.trackBarMessageRetentionInDays.TickHeight = 4;
            this.trackBarMessageRetentionInDays.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarMessageRetentionInDays.TrackerSize = new System.Drawing.Size(12, 12);
            this.trackBarMessageRetentionInDays.TrackLineBrushStyle = ServiceBusExplorer.Controls.BrushStyle.Solid;
            this.trackBarMessageRetentionInDays.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarMessageRetentionInDays.TrackLineHeight = 1;
            this.trackBarMessageRetentionInDays.Value = 1;
            this.trackBarMessageRetentionInDays.ValueChanged += new ServiceBusExplorer.Controls.ValueChangedHandler(this.trackBarMessageRetentionInDays_ValueChanged);
            // 
            // lblPartitionCount
            // 
            this.lblPartitionCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPartitionCount.AutoSize = true;
            this.lblPartitionCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPartitionCount.Location = new System.Drawing.Point(312, 296);
            this.lblPartitionCount.Name = "lblPartitionCount";
            this.lblPartitionCount.Size = new System.Drawing.Size(79, 13);
            this.lblPartitionCount.TabIndex = 5;
            this.lblPartitionCount.Text = "Partition Count:";
            // 
            // trackBarPartitionCount
            // 
            this.trackBarPartitionCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarPartitionCount.BackColor = System.Drawing.Color.Transparent;
            this.trackBarPartitionCount.BorderColor = System.Drawing.Color.Black;
            this.trackBarPartitionCount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackBarPartitionCount.ForeColor = System.Drawing.Color.Black;
            this.trackBarPartitionCount.IndentHeight = 6;
            this.trackBarPartitionCount.LargeChange = 1;
            this.trackBarPartitionCount.Location = new System.Drawing.Point(312, 308);
            this.trackBarPartitionCount.Maximum = 32;
            this.trackBarPartitionCount.Minimum = 2;
            this.trackBarPartitionCount.Name = "trackBarPartitionCount";
            this.trackBarPartitionCount.Size = new System.Drawing.Size(256, 29);
            this.trackBarPartitionCount.TabIndex = 6;
            this.trackBarPartitionCount.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarPartitionCount.TickColor = System.Drawing.Color.Black;
            this.trackBarPartitionCount.TickFrequency = 5;
            this.trackBarPartitionCount.TickHeight = 4;
            this.trackBarPartitionCount.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarPartitionCount.TrackerSize = new System.Drawing.Size(12, 12);
            this.trackBarPartitionCount.TrackLineBrushStyle = ServiceBusExplorer.Controls.BrushStyle.Solid;
            this.trackBarPartitionCount.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.trackBarPartitionCount.TrackLineHeight = 1;
            this.trackBarPartitionCount.Value = 2;
            this.trackBarPartitionCount.ValueChanged += new ServiceBusExplorer.Controls.ValueChangedHandler(this.trackBarPartitionCount_ValueChanged);
            // 
            // lblPartitionCountValue
            // 
            this.lblPartitionCountValue.AutoSize = true;
            this.lblPartitionCountValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPartitionCountValue.Location = new System.Drawing.Point(568, 316);
            this.lblPartitionCountValue.Name = "lblPartitionCountValue";
            this.lblPartitionCountValue.Size = new System.Drawing.Size(19, 13);
            this.lblPartitionCountValue.TabIndex = 7;
            this.lblPartitionCountValue.Text = "16";
            // 
            // txtUserMetadata
            // 
            this.txtUserMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserMetadata.BackColor = System.Drawing.SystemColors.Window;
            this.txtUserMetadata.Location = new System.Drawing.Point(16, 48);
            this.txtUserMetadata.MaxLength = 0;
            this.txtUserMetadata.Multiline = true;
            this.txtUserMetadata.Name = "txtUserMetadata";
            this.txtUserMetadata.Size = new System.Drawing.Size(576, 240);
            this.txtUserMetadata.TabIndex = 1;
            // 
            // lblUserMetadata
            // 
            this.lblUserMetadata.AutoSize = true;
            this.lblUserMetadata.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserMetadata.Location = new System.Drawing.Point(16, 28);
            this.lblUserMetadata.Name = "lblUserMetadata";
            this.lblUserMetadata.Size = new System.Drawing.Size(88, 13);
            this.lblUserMetadata.TabIndex = 0;
            this.lblUserMetadata.Text = "User Description:";
            // 
            // grouperEventHubInformation
            // 
            this.grouperEventHubInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperEventHubInformation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperEventHubInformation.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperEventHubInformation.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperEventHubInformation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventHubInformation.BorderThickness = 1F;
            this.grouperEventHubInformation.Controls.Add(this.propertyListView);
            this.grouperEventHubInformation.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventHubInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperEventHubInformation.ForeColor = System.Drawing.Color.White;
            this.grouperEventHubInformation.GroupImage = null;
            this.grouperEventHubInformation.GroupTitle = "Event Hub Information";
            this.grouperEventHubInformation.Location = new System.Drawing.Point(640, 8);
            this.grouperEventHubInformation.Name = "grouperEventHubInformation";
            this.grouperEventHubInformation.Padding = new System.Windows.Forms.Padding(20);
            this.grouperEventHubInformation.PaintGroupBox = true;
            this.grouperEventHubInformation.RoundCorners = 4;
            this.grouperEventHubInformation.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEventHubInformation.ShadowControl = false;
            this.grouperEventHubInformation.ShadowThickness = 1;
            this.grouperEventHubInformation.Size = new System.Drawing.Size(312, 432);
            this.grouperEventHubInformation.TabIndex = 2;
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
            this.propertyListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.propertyListView_DrawColumnHeader);
            this.propertyListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.propertyListView_DrawItem);
            this.propertyListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.propertyListView_DrawSubItem);
            this.propertyListView.Resize += new System.EventHandler(this.propertyListView_Resize);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 105;
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
            this.mainTabControl.Location = new System.Drawing.Point(16, 16);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(976, 480);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mainTabControl_DrawItem);
            // 
            // tabPageAuthorization
            // 
            this.tabPageAuthorization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageAuthorization.Controls.Add(this.grouperAuthorizationRuleList);
            this.tabPageAuthorization.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageAuthorization.Location = new System.Drawing.Point(4, 24);
            this.tabPageAuthorization.Name = "tabPageAuthorization";
            this.tabPageAuthorization.Size = new System.Drawing.Size(968, 452);
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
            this.grouperAuthorizationRuleList.Location = new System.Drawing.Point(16, 8);
            this.grouperAuthorizationRuleList.Name = "grouperAuthorizationRuleList";
            this.grouperAuthorizationRuleList.Padding = new System.Windows.Forms.Padding(20);
            this.grouperAuthorizationRuleList.PaintGroupBox = true;
            this.grouperAuthorizationRuleList.RoundCorners = 4;
            this.grouperAuthorizationRuleList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAuthorizationRuleList.ShadowControl = false;
            this.grouperAuthorizationRuleList.ShadowThickness = 1;
            this.grouperAuthorizationRuleList.Size = new System.Drawing.Size(936, 432);
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
            this.btnRefresh.Location = new System.Drawing.Point(680, 504);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 24);
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
            this.btnChangeStatus.Location = new System.Drawing.Point(760, 504);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(72, 24);
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
            this.btnCancelUpdate.Location = new System.Drawing.Point(920, 504);
            this.btnCancelUpdate.Name = "btnCancelUpdate";
            this.btnCancelUpdate.Size = new System.Drawing.Size(72, 24);
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
            this.btnCreateDelete.Location = new System.Drawing.Point(840, 504);
            this.btnCreateDelete.Name = "btnCreateDelete";
            this.btnCreateDelete.Size = new System.Drawing.Size(72, 24);
            this.btnCreateDelete.TabIndex = 4;
            this.btnCreateDelete.Text = "Create";
            this.btnCreateDelete.UseVisualStyleBackColor = false;
            this.btnCreateDelete.Click += new System.EventHandler(this.btnCreateDelete_Click);
            this.btnCreateDelete.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCreateDelete.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // entityInformationContextMenuStrip
            // 
            this.entityInformationContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.entityInformationContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPartitionInformationToClipboardMenuItem});
            this.entityInformationContextMenuStrip.Name = "registrationContextMenuStrip";
            this.entityInformationContextMenuStrip.Size = new System.Drawing.Size(299, 26);
            // 
            // copyPartitionInformationToClipboardMenuItem
            // 
            this.copyPartitionInformationToClipboardMenuItem.Name = "copyPartitionInformationToClipboardMenuItem";
            this.copyPartitionInformationToClipboardMenuItem.Size = new System.Drawing.Size(298, 22);
            this.copyPartitionInformationToClipboardMenuItem.Text = "Copy Event Hub Information to Clipboard.";
            this.copyPartitionInformationToClipboardMenuItem.ToolTipText = "Copy event hub information to clipboard.";
            this.copyPartitionInformationToClipboardMenuItem.Click += new System.EventHandler(this.copyPartitionInformationToClipboardMenuItem_Click);
            // 
            // dataPointDataGridView
            // 
            this.dataPointDataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataPointDataGridView.Name = "dataPointDataGridView";
            this.dataPointDataGridView.Size = new System.Drawing.Size(240, 150);
            this.dataPointDataGridView.TabIndex = 0;
            // 
            // HandleEventHubControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnChangeStatus);
            this.Controls.Add(this.btnCancelUpdate);
            this.Controls.Add(this.btnCreateDelete);
            this.Name = "HandleEventHubControl";
            this.Size = new System.Drawing.Size(1008, 544);
            this.tabPageDescription.ResumeLayout(false);
            this.grouperPath.ResumeLayout(false);
            this.grouperPath.PerformLayout();
            this.grouperEventHubProperties.ResumeLayout(false);
            this.grouperEventHubProperties.PerformLayout();
            this.grouperEventHubInformation.ResumeLayout(false);
            this.mainTabControl.ResumeLayout(false);
            this.tabPageAuthorization.ResumeLayout(false);
            this.grouperAuthorizationRuleList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesBindingSource)).EndInit();
            this.entityInformationContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataPointDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabPageDescription;
        internal System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Button btnCancelUpdate;
        private System.Windows.Forms.Button btnCreateDelete;
        private Grouper grouperPath;
        private System.Windows.Forms.Label lblRelativeURI;
        private System.Windows.Forms.TextBox txtPath;
        private Grouper grouperEventHubProperties;
        private System.Windows.Forms.TextBox txtUserMetadata;
        private System.Windows.Forms.Label lblUserMetadata;
        private Grouper grouperEventHubInformation;
        private System.Windows.Forms.ListView propertyListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private System.Windows.Forms.TabPage tabPageAuthorization;
        private Grouper grouperAuthorizationRuleList;
        private System.Windows.Forms.DataGridView authorizationRulesDataGridView;
        private System.Windows.Forms.Label lblPartitionCount;
        private System.Windows.Forms.Label lblMessageRetentionInDays;
        private CustomTrackBar trackBarPartitionCount;
        private System.Windows.Forms.Label lblPartitionCountValue;
        private System.Windows.Forms.BindingSource authorizationRulesBindingSource;
        private System.Windows.Forms.ContextMenuStrip entityInformationContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyPartitionInformationToClipboardMenuItem;
        private System.Windows.Forms.DataGridView dataPointDataGridView;
        private CustomTrackBar trackBarMessageRetentionInDays;
        private System.Windows.Forms.Label lblMessageRetentionInDaysValue;
    }
}
