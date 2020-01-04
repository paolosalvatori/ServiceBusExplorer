﻿namespace ServiceBusExplorer.Controls
{
    partial class HandleRelayControl
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
            this.grouperRelaySettings = new ServiceBusExplorer.Controls.Grouper();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.grouperPath = new ServiceBusExplorer.Controls.Grouper();
            this.lblRelativeURI = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.grouperRelayProperties = new ServiceBusExplorer.Controls.Grouper();
            this.cboRelayType = new System.Windows.Forms.ComboBox();
            this.lblRelayType = new System.Windows.Forms.Label();
            this.txtUserMetadata = new System.Windows.Forms.TextBox();
            this.lblUserMetadata = new System.Windows.Forms.Label();
            this.btnOpenDescriptionForm = new System.Windows.Forms.Button();
            this.grouperRelayInformation = new ServiceBusExplorer.Controls.Grouper();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageAuthorization = new System.Windows.Forms.TabPage();
            this.grouperAuthorizationRuleList = new ServiceBusExplorer.Controls.Grouper();
            this.authorizationRulesDataGridView = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCancelUpdate = new System.Windows.Forms.Button();
            this.btnCreateDelete = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.authorizationRulesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).BeginInit();
            this.tabPageDescription.SuspendLayout();
            this.grouperRelaySettings.SuspendLayout();
            this.grouperPath.SuspendLayout();
            this.grouperRelayProperties.SuspendLayout();
            this.grouperRelayInformation.SuspendLayout();
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
            this.tabPageDescription.Controls.Add(this.grouperRelaySettings);
            this.tabPageDescription.Controls.Add(this.grouperPath);
            this.tabPageDescription.Controls.Add(this.grouperRelayProperties);
            this.tabPageDescription.Controls.Add(this.grouperRelayInformation);
            this.tabPageDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDescription.Location = new System.Drawing.Point(4, 27);
            this.tabPageDescription.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(1293, 560);
            this.tabPageDescription.TabIndex = 2;
            this.tabPageDescription.Text = "Description";
            // 
            // grouperRelaySettings
            // 
            this.grouperRelaySettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperRelaySettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperRelaySettings.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperRelaySettings.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperRelaySettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperRelaySettings.BorderThickness = 1F;
            this.grouperRelaySettings.Controls.Add(this.checkedListBox);
            this.grouperRelaySettings.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperRelaySettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperRelaySettings.ForeColor = System.Drawing.Color.White;
            this.grouperRelaySettings.GroupImage = null;
            this.grouperRelaySettings.GroupTitle = "Relay Settings";
            this.grouperRelaySettings.Location = new System.Drawing.Point(437, 118);
            this.grouperRelaySettings.Margin = new System.Windows.Forms.Padding(4);
            this.grouperRelaySettings.Name = "grouperRelaySettings";
            this.grouperRelaySettings.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperRelaySettings.PaintGroupBox = true;
            this.grouperRelaySettings.RoundCorners = 4;
            this.grouperRelaySettings.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperRelaySettings.ShadowControl = false;
            this.grouperRelaySettings.ShadowThickness = 1;
            this.grouperRelaySettings.Size = new System.Drawing.Size(395, 423);
            this.grouperRelaySettings.TabIndex = 5;
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
            "Requires Client Authorization",
            "Requires Transport Security"});
            this.checkedListBox.Location = new System.Drawing.Point(21, 53);
            this.checkedListBox.Margin = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(351, 328);
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
            // grouperRelayProperties
            // 
            this.grouperRelayProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperRelayProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperRelayProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperRelayProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperRelayProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperRelayProperties.BorderThickness = 1F;
            this.grouperRelayProperties.Controls.Add(this.cboRelayType);
            this.grouperRelayProperties.Controls.Add(this.lblRelayType);
            this.grouperRelayProperties.Controls.Add(this.txtUserMetadata);
            this.grouperRelayProperties.Controls.Add(this.lblUserMetadata);
            this.grouperRelayProperties.Controls.Add(this.btnOpenDescriptionForm);
            this.grouperRelayProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperRelayProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperRelayProperties.ForeColor = System.Drawing.Color.White;
            this.grouperRelayProperties.GroupImage = null;
            this.grouperRelayProperties.GroupTitle = "Relay Properties";
            this.grouperRelayProperties.Location = new System.Drawing.Point(21, 118);
            this.grouperRelayProperties.Margin = new System.Windows.Forms.Padding(4);
            this.grouperRelayProperties.Name = "grouperRelayProperties";
            this.grouperRelayProperties.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperRelayProperties.PaintGroupBox = true;
            this.grouperRelayProperties.RoundCorners = 4;
            this.grouperRelayProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperRelayProperties.ShadowControl = false;
            this.grouperRelayProperties.ShadowThickness = 1;
            this.grouperRelayProperties.Size = new System.Drawing.Size(395, 423);
            this.grouperRelayProperties.TabIndex = 3;
            this.grouperRelayProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperRelayProperties_CustomPaint);
            // 
            // cboRelayType
            // 
            this.cboRelayType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRelayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRelayType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRelayType.FormattingEnabled = true;
            this.cboRelayType.Location = new System.Drawing.Point(21, 54);
            this.cboRelayType.Margin = new System.Windows.Forms.Padding(4);
            this.cboRelayType.Name = "cboRelayType";
            this.cboRelayType.Size = new System.Drawing.Size(308, 25);
            this.cboRelayType.TabIndex = 29;
            // 
            // lblRelayType
            // 
            this.lblRelayType.AutoSize = true;
            this.lblRelayType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRelayType.Location = new System.Drawing.Point(21, 34);
            this.lblRelayType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelayType.Name = "lblRelayType";
            this.lblRelayType.Size = new System.Drawing.Size(84, 17);
            this.lblRelayType.TabIndex = 28;
            this.lblRelayType.Text = "Relay Type:";
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
            this.txtUserMetadata.Size = new System.Drawing.Size(308, 294);
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
            // grouperRelayInformation
            // 
            this.grouperRelayInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperRelayInformation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperRelayInformation.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperRelayInformation.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperRelayInformation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperRelayInformation.BorderThickness = 1F;
            this.grouperRelayInformation.Controls.Add(this.propertyListView);
            this.grouperRelayInformation.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperRelayInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperRelayInformation.ForeColor = System.Drawing.Color.White;
            this.grouperRelayInformation.GroupImage = null;
            this.grouperRelayInformation.GroupTitle = "Relay Information";
            this.grouperRelayInformation.Location = new System.Drawing.Point(853, 10);
            this.grouperRelayInformation.Margin = new System.Windows.Forms.Padding(4);
            this.grouperRelayInformation.Name = "grouperRelayInformation";
            this.grouperRelayInformation.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperRelayInformation.PaintGroupBox = true;
            this.grouperRelayInformation.RoundCorners = 4;
            this.grouperRelayInformation.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperRelayInformation.ShadowControl = false;
            this.grouperRelayInformation.ShadowThickness = 1;
            this.grouperRelayInformation.Size = new System.Drawing.Size(416, 532);
            this.grouperRelayInformation.TabIndex = 6;
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
            this.btnRefresh.Location = new System.Drawing.Point(1013, 620);
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
            this.btnCancelUpdate.TabIndex = 4;
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
            this.btnCreateDelete.TabIndex = 3;
            this.btnCreateDelete.Text = "Create";
            this.btnCreateDelete.UseVisualStyleBackColor = false;
            this.btnCreateDelete.Click += new System.EventHandler(this.btnCreateDelete_Click);
            this.btnCreateDelete.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCreateDelete.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // HandleRelayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCancelUpdate);
            this.Controls.Add(this.btnCreateDelete);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HandleRelayControl";
            this.Size = new System.Drawing.Size(1344, 670);
            ((System.ComponentModel.ISupportInitialize)(this.deadletterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).EndInit();
            this.tabPageDescription.ResumeLayout(false);
            this.grouperRelaySettings.ResumeLayout(false);
            this.grouperPath.ResumeLayout(false);
            this.grouperPath.PerformLayout();
            this.grouperRelayProperties.ResumeLayout(false);
            this.grouperRelayProperties.PerformLayout();
            this.grouperRelayInformation.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnCancelUpdate;
        private System.Windows.Forms.Button btnCreateDelete;
        private System.Windows.Forms.ToolTip toolTip1;
        private Grouper grouperRelaySettings;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private Grouper grouperPath;
        private System.Windows.Forms.Label lblRelativeURI;
        private System.Windows.Forms.TextBox txtPath;
        private Grouper grouperRelayProperties;
        private System.Windows.Forms.TextBox txtUserMetadata;
        private System.Windows.Forms.Label lblUserMetadata;
        private System.Windows.Forms.Button btnOpenDescriptionForm;
        private Grouper grouperRelayInformation;
        private System.Windows.Forms.ListView propertyListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private System.Windows.Forms.TabPage tabPageAuthorization;
        private Grouper grouperAuthorizationRuleList;
        private System.Windows.Forms.DataGridView authorizationRulesDataGridView;
        private System.Windows.Forms.BindingSource authorizationRulesBindingSource;
        private System.Windows.Forms.Label lblRelayType;
        private System.Windows.Forms.ComboBox cboRelayType;
    }
}
