namespace ServiceBusExplorer.Controls
{
    partial class HandleRuleControl
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
            this.btnCreateDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grouperFilterType = new ServiceBusExplorer.Controls.Grouper();
            this.checkBoxIsCorrelationFilter = new System.Windows.Forms.CheckBox();
            this.grouperCorrelationFilter = new ServiceBusExplorer.Controls.Grouper();
            this.txtCorrelationFilterTo = new System.Windows.Forms.TextBox();
            this.lblCorrelationFilterTo = new System.Windows.Forms.Label();
            this.txtCorrelationFilterSessionId = new System.Windows.Forms.TextBox();
            this.lblCorrelationFilterSessionId = new System.Windows.Forms.Label();
            this.txtCorrelationFilterReplyToSessionId = new System.Windows.Forms.TextBox();
            this.lblCorrelationFilterReplyToSessionId = new System.Windows.Forms.Label();
            this.txtCorrelationFilterReplyTo = new System.Windows.Forms.TextBox();
            this.lblCorrelationFilterReplyTo = new System.Windows.Forms.Label();
            this.txtCorrelationFilterMessageId = new System.Windows.Forms.TextBox();
            this.lblCorrelationFilterMessageId = new System.Windows.Forms.Label();
            this.txtCorrelationFilterLabel = new System.Windows.Forms.TextBox();
            this.lblCorrelationFilterLabel = new System.Windows.Forms.Label();
            this.txtCorrelationFilterCorrelationId = new System.Windows.Forms.TextBox();
            this.lblCorrelationFilterCorrelationId = new System.Windows.Forms.Label();
            this.txtCorrelationFilterContentType = new System.Windows.Forms.TextBox();
            this.lblCorrelationFilterContentType = new System.Windows.Forms.Label();
            this.authorizationRulesDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperCreatedAt = new ServiceBusExplorer.Controls.Grouper();
            this.txtCreatedAt = new System.Windows.Forms.TextBox();
            this.grouperIsDefault = new ServiceBusExplorer.Controls.Grouper();
            this.checkBoxDefault = new System.Windows.Forms.CheckBox();
            this.grouperAction = new ServiceBusExplorer.Controls.Grouper();
            this.txtSqlFilterAction = new System.Windows.Forms.TextBox();
            this.grouperFilter = new ServiceBusExplorer.Controls.Grouper();
            this.txtFilterExpression = new System.Windows.Forms.TextBox();
            this.grouperName = new ServiceBusExplorer.Controls.Grouper();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grouperFilterType.SuspendLayout();
            this.grouperCorrelationFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).BeginInit();
            this.grouperCreatedAt.SuspendLayout();
            this.grouperIsDefault.SuspendLayout();
            this.grouperAction.SuspendLayout();
            this.grouperFilter.SuspendLayout();
            this.grouperName.SuspendLayout();
            this.SuspendLayout();
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
            this.btnCreateDelete.Location = new System.Drawing.Point(800, 360);
            this.btnCreateDelete.Name = "btnCreateDelete";
            this.btnCreateDelete.Size = new System.Drawing.Size(72, 24);
            this.btnCreateDelete.TabIndex = 6;
            this.btnCreateDelete.Text = "Create";
            this.btnCreateDelete.UseVisualStyleBackColor = false;
            this.btnCreateDelete.Click += new System.EventHandler(this.btnCreateDelete_Click);
            this.btnCreateDelete.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCreateDelete.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(880, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // grouperFilterType
            // 
            this.grouperFilterType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperFilterType.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperFilterType.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperFilterType.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperFilterType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilterType.BorderThickness = 1F;
            this.grouperFilterType.Controls.Add(this.checkBoxIsCorrelationFilter);
            this.grouperFilterType.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilterType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperFilterType.ForeColor = System.Drawing.Color.White;
            this.grouperFilterType.GroupImage = null;
            this.grouperFilterType.GroupTitle = "Filter Type";
            this.grouperFilterType.Location = new System.Drawing.Point(685, 8);
            this.grouperFilterType.Name = "grouperFilterType";
            this.grouperFilterType.Padding = new System.Windows.Forms.Padding(20);
            this.grouperFilterType.PaintGroupBox = true;
            this.grouperFilterType.RoundCorners = 4;
            this.grouperFilterType.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperFilterType.ShadowControl = false;
            this.grouperFilterType.ShadowThickness = 1;
            this.grouperFilterType.Size = new System.Drawing.Size(137, 80);
            this.grouperFilterType.TabIndex = 3;
            // 
            // checkBoxIsCorrelationFilter
            // 
            this.checkBoxIsCorrelationFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxIsCorrelationFilter.AutoSize = true;
            this.checkBoxIsCorrelationFilter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxIsCorrelationFilter.Location = new System.Drawing.Point(16, 40);
            this.checkBoxIsCorrelationFilter.Name = "checkBoxIsCorrelationFilter";
            this.checkBoxIsCorrelationFilter.Size = new System.Drawing.Size(76, 17);
            this.checkBoxIsCorrelationFilter.TabIndex = 0;
            this.checkBoxIsCorrelationFilter.Text = "Correlation";
            this.checkBoxIsCorrelationFilter.UseVisualStyleBackColor = true;
            this.checkBoxIsCorrelationFilter.CheckedChanged += new System.EventHandler(this.checkBoxIsCorrelationFilter_CheckedChanged);
            // 
            // grouperCorrelationFilter
            // 
            this.grouperCorrelationFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperCorrelationFilter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperCorrelationFilter.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperCorrelationFilter.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperCorrelationFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCorrelationFilter.BorderThickness = 1F;
            this.grouperCorrelationFilter.Controls.Add(this.txtCorrelationFilterTo);
            this.grouperCorrelationFilter.Controls.Add(this.lblCorrelationFilterTo);
            this.grouperCorrelationFilter.Controls.Add(this.txtCorrelationFilterSessionId);
            this.grouperCorrelationFilter.Controls.Add(this.lblCorrelationFilterSessionId);
            this.grouperCorrelationFilter.Controls.Add(this.txtCorrelationFilterReplyToSessionId);
            this.grouperCorrelationFilter.Controls.Add(this.lblCorrelationFilterReplyToSessionId);
            this.grouperCorrelationFilter.Controls.Add(this.txtCorrelationFilterReplyTo);
            this.grouperCorrelationFilter.Controls.Add(this.lblCorrelationFilterReplyTo);
            this.grouperCorrelationFilter.Controls.Add(this.txtCorrelationFilterMessageId);
            this.grouperCorrelationFilter.Controls.Add(this.lblCorrelationFilterMessageId);
            this.grouperCorrelationFilter.Controls.Add(this.txtCorrelationFilterLabel);
            this.grouperCorrelationFilter.Controls.Add(this.lblCorrelationFilterLabel);
            this.grouperCorrelationFilter.Controls.Add(this.txtCorrelationFilterCorrelationId);
            this.grouperCorrelationFilter.Controls.Add(this.lblCorrelationFilterCorrelationId);
            this.grouperCorrelationFilter.Controls.Add(this.txtCorrelationFilterContentType);
            this.grouperCorrelationFilter.Controls.Add(this.lblCorrelationFilterContentType);
            this.grouperCorrelationFilter.Controls.Add(this.authorizationRulesDataGridView);
            this.grouperCorrelationFilter.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCorrelationFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperCorrelationFilter.ForeColor = System.Drawing.Color.White;
            this.grouperCorrelationFilter.GroupImage = null;
            this.grouperCorrelationFilter.GroupTitle = "Correlation Filter";
            this.grouperCorrelationFilter.Location = new System.Drawing.Point(16, 96);
            this.grouperCorrelationFilter.Name = "grouperCorrelationFilter";
            this.grouperCorrelationFilter.Padding = new System.Windows.Forms.Padding(40, 38, 40, 38);
            this.grouperCorrelationFilter.PaintGroupBox = true;
            this.grouperCorrelationFilter.RoundCorners = 4;
            this.grouperCorrelationFilter.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperCorrelationFilter.ShadowControl = false;
            this.grouperCorrelationFilter.ShadowThickness = 1;
            this.grouperCorrelationFilter.Size = new System.Drawing.Size(860, 256);
            this.grouperCorrelationFilter.TabIndex = 1;
            this.grouperCorrelationFilter.Visible = false;
            // 
            // txtCorrelationFilterTo
            // 
            this.txtCorrelationFilterTo.Location = new System.Drawing.Point(128, 228);
            this.txtCorrelationFilterTo.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterTo.Name = "txtCorrelationFilterTo";
            this.txtCorrelationFilterTo.Size = new System.Drawing.Size(250, 20);
            this.txtCorrelationFilterTo.TabIndex = 15;
            // 
            // lblCorrelationFilterTo
            // 
            this.lblCorrelationFilterTo.AutoSize = true;
            this.lblCorrelationFilterTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterTo.Location = new System.Drawing.Point(16, 228);
            this.lblCorrelationFilterTo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterTo.Name = "lblCorrelationFilterTo";
            this.lblCorrelationFilterTo.Size = new System.Drawing.Size(20, 13);
            this.lblCorrelationFilterTo.TabIndex = 14;
            this.lblCorrelationFilterTo.Text = "To";
            // 
            // txtCorrelationFilterSessionId
            // 
            this.txtCorrelationFilterSessionId.Location = new System.Drawing.Point(128, 200);
            this.txtCorrelationFilterSessionId.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterSessionId.Name = "txtCorrelationFilterSessionId";
            this.txtCorrelationFilterSessionId.Size = new System.Drawing.Size(250, 20);
            this.txtCorrelationFilterSessionId.TabIndex = 13;
            // 
            // lblCorrelationFilterSessionId
            // 
            this.lblCorrelationFilterSessionId.AutoSize = true;
            this.lblCorrelationFilterSessionId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterSessionId.Location = new System.Drawing.Point(16, 200);
            this.lblCorrelationFilterSessionId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterSessionId.Name = "lblCorrelationFilterSessionId";
            this.lblCorrelationFilterSessionId.Size = new System.Drawing.Size(53, 13);
            this.lblCorrelationFilterSessionId.TabIndex = 12;
            this.lblCorrelationFilterSessionId.Text = "SessionId";
            // 
            // txtCorrelationFilterReplyToSessionId
            // 
            this.txtCorrelationFilterReplyToSessionId.Location = new System.Drawing.Point(128, 172);
            this.txtCorrelationFilterReplyToSessionId.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterReplyToSessionId.Name = "txtCorrelationFilterReplyToSessionId";
            this.txtCorrelationFilterReplyToSessionId.Size = new System.Drawing.Size(250, 20);
            this.txtCorrelationFilterReplyToSessionId.TabIndex = 11;
            // 
            // lblCorrelationFilterReplyToSessionId
            // 
            this.lblCorrelationFilterReplyToSessionId.AutoSize = true;
            this.lblCorrelationFilterReplyToSessionId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterReplyToSessionId.Location = new System.Drawing.Point(16, 172);
            this.lblCorrelationFilterReplyToSessionId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterReplyToSessionId.Name = "lblCorrelationFilterReplyToSessionId";
            this.lblCorrelationFilterReplyToSessionId.Size = new System.Drawing.Size(93, 13);
            this.lblCorrelationFilterReplyToSessionId.TabIndex = 10;
            this.lblCorrelationFilterReplyToSessionId.Text = "ReplyToSessionId";
            // 
            // txtCorrelationFilterReplyTo
            // 
            this.txtCorrelationFilterReplyTo.Location = new System.Drawing.Point(128, 144);
            this.txtCorrelationFilterReplyTo.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterReplyTo.Name = "txtCorrelationFilterReplyTo";
            this.txtCorrelationFilterReplyTo.Size = new System.Drawing.Size(250, 20);
            this.txtCorrelationFilterReplyTo.TabIndex = 9;
            // 
            // lblCorrelationFilterReplyTo
            // 
            this.lblCorrelationFilterReplyTo.AutoSize = true;
            this.lblCorrelationFilterReplyTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterReplyTo.Location = new System.Drawing.Point(16, 144);
            this.lblCorrelationFilterReplyTo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterReplyTo.Name = "lblCorrelationFilterReplyTo";
            this.lblCorrelationFilterReplyTo.Size = new System.Drawing.Size(47, 13);
            this.lblCorrelationFilterReplyTo.TabIndex = 8;
            this.lblCorrelationFilterReplyTo.Text = "ReplyTo";
            // 
            // txtCorrelationFilterMessageId
            // 
            this.txtCorrelationFilterMessageId.Location = new System.Drawing.Point(128, 116);
            this.txtCorrelationFilterMessageId.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterMessageId.Name = "txtCorrelationFilterMessageId";
            this.txtCorrelationFilterMessageId.Size = new System.Drawing.Size(250, 20);
            this.txtCorrelationFilterMessageId.TabIndex = 7;
            // 
            // lblCorrelationFilterMessageId
            // 
            this.lblCorrelationFilterMessageId.AutoSize = true;
            this.lblCorrelationFilterMessageId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterMessageId.Location = new System.Drawing.Point(16, 116);
            this.lblCorrelationFilterMessageId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterMessageId.Name = "lblCorrelationFilterMessageId";
            this.lblCorrelationFilterMessageId.Size = new System.Drawing.Size(59, 13);
            this.lblCorrelationFilterMessageId.TabIndex = 6;
            this.lblCorrelationFilterMessageId.Text = "MessageId";
            // 
            // txtCorrelationFilterLabel
            // 
            this.txtCorrelationFilterLabel.Location = new System.Drawing.Point(128, 88);
            this.txtCorrelationFilterLabel.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterLabel.Name = "txtCorrelationFilterLabel";
            this.txtCorrelationFilterLabel.Size = new System.Drawing.Size(250, 20);
            this.txtCorrelationFilterLabel.TabIndex = 5;
            // 
            // lblCorrelationFilterLabel
            // 
            this.lblCorrelationFilterLabel.AutoSize = true;
            this.lblCorrelationFilterLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterLabel.Location = new System.Drawing.Point(16, 88);
            this.lblCorrelationFilterLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterLabel.Name = "lblCorrelationFilterLabel";
            this.lblCorrelationFilterLabel.Size = new System.Drawing.Size(33, 13);
            this.lblCorrelationFilterLabel.TabIndex = 4;
            this.lblCorrelationFilterLabel.Text = "Label";
            // 
            // txtCorrelationFilterCorrelationId
            // 
            this.txtCorrelationFilterCorrelationId.Location = new System.Drawing.Point(128, 60);
            this.txtCorrelationFilterCorrelationId.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterCorrelationId.Name = "txtCorrelationFilterCorrelationId";
            this.txtCorrelationFilterCorrelationId.Size = new System.Drawing.Size(250, 20);
            this.txtCorrelationFilterCorrelationId.TabIndex = 3;
            // 
            // lblCorrelationFilterCorrelationId
            // 
            this.lblCorrelationFilterCorrelationId.AutoSize = true;
            this.lblCorrelationFilterCorrelationId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterCorrelationId.Location = new System.Drawing.Point(16, 60);
            this.lblCorrelationFilterCorrelationId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterCorrelationId.Name = "lblCorrelationFilterCorrelationId";
            this.lblCorrelationFilterCorrelationId.Size = new System.Drawing.Size(66, 13);
            this.lblCorrelationFilterCorrelationId.TabIndex = 2;
            this.lblCorrelationFilterCorrelationId.Text = "CorrelationId";
            // 
            // txtCorrelationFilterContentType
            // 
            this.txtCorrelationFilterContentType.Location = new System.Drawing.Point(128, 32);
            this.txtCorrelationFilterContentType.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterContentType.Name = "txtCorrelationFilterContentType";
            this.txtCorrelationFilterContentType.Size = new System.Drawing.Size(250, 20);
            this.txtCorrelationFilterContentType.TabIndex = 1;
            // 
            // lblCorrelationFilterContentType
            // 
            this.lblCorrelationFilterContentType.AutoSize = true;
            this.lblCorrelationFilterContentType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterContentType.Location = new System.Drawing.Point(16, 32);
            this.lblCorrelationFilterContentType.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterContentType.Name = "lblCorrelationFilterContentType";
            this.lblCorrelationFilterContentType.Size = new System.Drawing.Size(68, 13);
            this.lblCorrelationFilterContentType.TabIndex = 0;
            this.lblCorrelationFilterContentType.Text = "ContentType";
            // 
            // authorizationRulesDataGridView
            // 
            this.authorizationRulesDataGridView.AllowUserToResizeRows = false;
            this.authorizationRulesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.authorizationRulesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.authorizationRulesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.authorizationRulesDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.authorizationRulesDataGridView.Location = new System.Drawing.Point(16, 256);
            this.authorizationRulesDataGridView.Margin = new System.Windows.Forms.Padding(8);
            this.authorizationRulesDataGridView.MultiSelect = false;
            this.authorizationRulesDataGridView.Name = "authorizationRulesDataGridView";
            this.authorizationRulesDataGridView.RowHeadersWidth = 24;
            this.authorizationRulesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.authorizationRulesDataGridView.ShowCellErrors = false;
            this.authorizationRulesDataGridView.ShowRowErrors = false;
            this.authorizationRulesDataGridView.Size = new System.Drawing.Size(400, 96);
            this.authorizationRulesDataGridView.TabIndex = 0;
            this.authorizationRulesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.authorizationRulesDataGridView_RowsAdded);
            this.authorizationRulesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.authorizationRulesDataGridView_RowsRemoved);
            this.authorizationRulesDataGridView.Resize += new System.EventHandler(this.authorizationRulesDataGridView_Resize);
            // 
            // grouperCreatedAt
            // 
            this.grouperCreatedAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperCreatedAt.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperCreatedAt.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperCreatedAt.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperCreatedAt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCreatedAt.BorderThickness = 1F;
            this.grouperCreatedAt.Controls.Add(this.txtCreatedAt);
            this.grouperCreatedAt.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCreatedAt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperCreatedAt.ForeColor = System.Drawing.Color.White;
            this.grouperCreatedAt.GroupImage = null;
            this.grouperCreatedAt.GroupTitle = "Created At";
            this.grouperCreatedAt.Location = new System.Drawing.Point(492, 8);
            this.grouperCreatedAt.Name = "grouperCreatedAt";
            this.grouperCreatedAt.Padding = new System.Windows.Forms.Padding(20);
            this.grouperCreatedAt.PaintGroupBox = true;
            this.grouperCreatedAt.RoundCorners = 4;
            this.grouperCreatedAt.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperCreatedAt.ShadowControl = false;
            this.grouperCreatedAt.ShadowThickness = 1;
            this.grouperCreatedAt.Size = new System.Drawing.Size(187, 80);
            this.grouperCreatedAt.TabIndex = 2;
            // 
            // txtCreatedAt
            // 
            this.txtCreatedAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCreatedAt.BackColor = System.Drawing.SystemColors.Window;
            this.txtCreatedAt.Location = new System.Drawing.Point(16, 40);
            this.txtCreatedAt.Name = "txtCreatedAt";
            this.txtCreatedAt.ReadOnly = true;
            this.txtCreatedAt.Size = new System.Drawing.Size(155, 20);
            this.txtCreatedAt.TabIndex = 0;
            // 
            // grouperIsDefault
            // 
            this.grouperIsDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperIsDefault.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperIsDefault.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperIsDefault.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperIsDefault.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperIsDefault.BorderThickness = 1F;
            this.grouperIsDefault.Controls.Add(this.checkBoxDefault);
            this.grouperIsDefault.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperIsDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperIsDefault.ForeColor = System.Drawing.Color.White;
            this.grouperIsDefault.GroupImage = null;
            this.grouperIsDefault.GroupTitle = "Is Default?";
            this.grouperIsDefault.Location = new System.Drawing.Point(828, 8);
            this.grouperIsDefault.Name = "grouperIsDefault";
            this.grouperIsDefault.Padding = new System.Windows.Forms.Padding(20);
            this.grouperIsDefault.PaintGroupBox = true;
            this.grouperIsDefault.RoundCorners = 4;
            this.grouperIsDefault.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperIsDefault.ShadowControl = false;
            this.grouperIsDefault.ShadowThickness = 1;
            this.grouperIsDefault.Size = new System.Drawing.Size(124, 80);
            this.grouperIsDefault.TabIndex = 4;
            // 
            // checkBoxDefault
            // 
            this.checkBoxDefault.AutoSize = true;
            this.checkBoxDefault.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxDefault.Location = new System.Drawing.Point(16, 40);
            this.checkBoxDefault.Name = "checkBoxDefault";
            this.checkBoxDefault.Size = new System.Drawing.Size(60, 17);
            this.checkBoxDefault.TabIndex = 0;
            this.checkBoxDefault.Text = "Default";
            this.checkBoxDefault.UseVisualStyleBackColor = true;
            this.checkBoxDefault.CheckedChanged += new System.EventHandler(this.checkBoxDefault_CheckedChanged);
            // 
            // grouperAction
            // 
            this.grouperAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperAction.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperAction.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperAction.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperAction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAction.BorderThickness = 1F;
            this.grouperAction.Controls.Add(this.txtSqlFilterAction);
            this.grouperAction.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperAction.ForeColor = System.Drawing.Color.White;
            this.grouperAction.GroupImage = null;
            this.grouperAction.GroupTitle = "Action";
            this.grouperAction.Location = new System.Drawing.Point(492, 96);
            this.grouperAction.Name = "grouperAction";
            this.grouperAction.Padding = new System.Windows.Forms.Padding(20);
            this.grouperAction.PaintGroupBox = true;
            this.grouperAction.RoundCorners = 4;
            this.grouperAction.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAction.ShadowControl = false;
            this.grouperAction.ShadowThickness = 1;
            this.grouperAction.Size = new System.Drawing.Size(460, 256);
            this.grouperAction.TabIndex = 5;
            // 
            // txtSqlFilterAction
            // 
            this.txtSqlFilterAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlFilterAction.BackColor = System.Drawing.SystemColors.Window;
            this.txtSqlFilterAction.Location = new System.Drawing.Point(16, 32);
            this.txtSqlFilterAction.Multiline = true;
            this.txtSqlFilterAction.Name = "txtSqlFilterAction";
            this.txtSqlFilterAction.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSqlFilterAction.Size = new System.Drawing.Size(428, 208);
            this.txtSqlFilterAction.TabIndex = 0;
            // 
            // grouperFilter
            // 
            this.grouperFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperFilter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperFilter.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperFilter.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilter.BorderThickness = 1F;
            this.grouperFilter.Controls.Add(this.txtFilterExpression);
            this.grouperFilter.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperFilter.ForeColor = System.Drawing.Color.White;
            this.grouperFilter.GroupImage = null;
            this.grouperFilter.GroupTitle = "Filter";
            this.grouperFilter.Location = new System.Drawing.Point(16, 96);
            this.grouperFilter.Name = "grouperFilter";
            this.grouperFilter.Padding = new System.Windows.Forms.Padding(20);
            this.grouperFilter.PaintGroupBox = true;
            this.grouperFilter.RoundCorners = 4;
            this.grouperFilter.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperFilter.ShadowControl = false;
            this.grouperFilter.ShadowThickness = 1;
            this.grouperFilter.Size = new System.Drawing.Size(460, 256);
            this.grouperFilter.TabIndex = 3;
            // 
            // txtFilterExpression
            // 
            this.txtFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterExpression.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilterExpression.Location = new System.Drawing.Point(16, 32);
            this.txtFilterExpression.Multiline = true;
            this.txtFilterExpression.Name = "txtFilterExpression";
            this.txtFilterExpression.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFilterExpression.Size = new System.Drawing.Size(428, 208);
            this.txtFilterExpression.TabIndex = 0;
            // 
            // grouperName
            // 
            this.grouperName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperName.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperName.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperName.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperName.BorderThickness = 1F;
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
            this.grouperName.Size = new System.Drawing.Size(460, 80);
            this.grouperName.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.SystemColors.Window;
            this.txtName.Location = new System.Drawing.Point(16, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(428, 20);
            this.txtName.TabIndex = 0;
            // 
            // HandleRuleControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.grouperFilterType);
            this.Controls.Add(this.grouperCorrelationFilter);
            this.Controls.Add(this.grouperCreatedAt);
            this.Controls.Add(this.grouperIsDefault);
            this.Controls.Add(this.grouperAction);
            this.Controls.Add(this.grouperFilter);
            this.Controls.Add(this.grouperName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateDelete);
            this.Name = "HandleRuleControl";
            this.Size = new System.Drawing.Size(968, 400);
            this.Resize += new System.EventHandler(this.HandleRuleControl_Resize);
            this.grouperFilterType.ResumeLayout(false);
            this.grouperFilterType.PerformLayout();
            this.grouperCorrelationFilter.ResumeLayout(false);
            this.grouperCorrelationFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).EndInit();
            this.grouperCreatedAt.ResumeLayout(false);
            this.grouperCreatedAt.PerformLayout();
            this.grouperIsDefault.ResumeLayout(false);
            this.grouperIsDefault.PerformLayout();
            this.grouperAction.ResumeLayout(false);
            this.grouperAction.PerformLayout();
            this.grouperFilter.ResumeLayout(false);
            this.grouperFilter.PerformLayout();
            this.grouperName.ResumeLayout(false);
            this.grouperName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private Grouper grouperName;
        private System.Windows.Forms.TextBox txtName;
        private Grouper grouperFilter;
        private System.Windows.Forms.TextBox txtFilterExpression;
        private Grouper grouperAction;
        private System.Windows.Forms.TextBox txtSqlFilterAction;
        private Grouper grouperIsDefault;
        private System.Windows.Forms.CheckBox checkBoxDefault;
        private Grouper grouperCreatedAt;
        private System.Windows.Forms.TextBox txtCreatedAt;
        private Grouper grouperFilterType;
        private Grouper grouperCorrelationFilter;
        private System.Windows.Forms.CheckBox checkBoxIsCorrelationFilter;
        private System.Windows.Forms.TextBox txtCorrelationFilterContentType;
        private System.Windows.Forms.Label lblCorrelationFilterContentType;
        private System.Windows.Forms.TextBox txtCorrelationFilterTo;
        private System.Windows.Forms.Label lblCorrelationFilterTo;
        private System.Windows.Forms.TextBox txtCorrelationFilterSessionId;
        private System.Windows.Forms.Label lblCorrelationFilterSessionId;
        private System.Windows.Forms.TextBox txtCorrelationFilterReplyToSessionId;
        private System.Windows.Forms.Label lblCorrelationFilterReplyToSessionId;
        private System.Windows.Forms.TextBox txtCorrelationFilterReplyTo;
        private System.Windows.Forms.Label lblCorrelationFilterReplyTo;
        private System.Windows.Forms.TextBox txtCorrelationFilterMessageId;
        private System.Windows.Forms.Label lblCorrelationFilterMessageId;
        private System.Windows.Forms.TextBox txtCorrelationFilterLabel;
        private System.Windows.Forms.Label lblCorrelationFilterLabel;
        private System.Windows.Forms.TextBox txtCorrelationFilterCorrelationId;
        private System.Windows.Forms.Label lblCorrelationFilterCorrelationId;
        private System.Windows.Forms.DataGridView authorizationRulesDataGridView;
    }
}
