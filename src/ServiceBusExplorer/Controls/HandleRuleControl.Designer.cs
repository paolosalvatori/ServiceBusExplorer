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
            this.grouperName = new ServiceBusExplorer.Controls.Grouper();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grouperAction = new ServiceBusExplorer.Controls.Grouper();
            this.txtSqlFilterAction = new System.Windows.Forms.TextBox();
            this.grouperFilter = new ServiceBusExplorer.Controls.Grouper();
            this.txtFilterExpression = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grouperFilterType.SuspendLayout();
            this.grouperCorrelationFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).BeginInit();
            this.grouperCreatedAt.SuspendLayout();
            this.grouperIsDefault.SuspendLayout();
            this.grouperName.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grouperAction.SuspendLayout();
            this.grouperFilter.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.grouperFilterType.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperFilterType.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperFilterType.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperFilterType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilterType.BorderThickness = 1F;
            this.grouperFilterType.Controls.Add(this.checkBoxIsCorrelationFilter);
            this.grouperFilterType.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilterType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperFilterType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperFilterType.ForeColor = System.Drawing.Color.White;
            this.grouperFilterType.GroupImage = null;
            this.grouperFilterType.GroupTitle = "Filter Type";
            this.grouperFilterType.Location = new System.Drawing.Point(695, 3);
            this.grouperFilterType.Name = "grouperFilterType";
            this.grouperFilterType.Padding = new System.Windows.Forms.Padding(20);
            this.grouperFilterType.PaintGroupBox = true;
            this.grouperFilterType.RoundCorners = 4;
            this.grouperFilterType.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperFilterType.ShadowControl = false;
            this.grouperFilterType.ShadowThickness = 1;
            this.grouperFilterType.Size = new System.Drawing.Size(115, 74);
            this.grouperFilterType.TabIndex = 3;
            // 
            // checkBoxIsCorrelationFilter
            // 
            this.checkBoxIsCorrelationFilter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxIsCorrelationFilter.AutoSize = true;
            this.checkBoxIsCorrelationFilter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxIsCorrelationFilter.Location = new System.Drawing.Point(20, 42);
            this.checkBoxIsCorrelationFilter.Name = "checkBoxIsCorrelationFilter";
            this.checkBoxIsCorrelationFilter.Size = new System.Drawing.Size(76, 17);
            this.checkBoxIsCorrelationFilter.TabIndex = 0;
            this.checkBoxIsCorrelationFilter.Text = "Correlation";
            this.checkBoxIsCorrelationFilter.UseVisualStyleBackColor = true;
            this.checkBoxIsCorrelationFilter.CheckedChanged += new System.EventHandler(this.checkBoxIsCorrelationFilter_CheckedChanged);
            // 
            // grouperCorrelationFilter
            // 
            this.grouperCorrelationFilter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperCorrelationFilter.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperCorrelationFilter.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperCorrelationFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCorrelationFilter.BorderThickness = 1F;
            this.grouperCorrelationFilter.Controls.Add(this.tableLayoutPanel2);
            this.grouperCorrelationFilter.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCorrelationFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperCorrelationFilter.ForeColor = System.Drawing.Color.White;
            this.grouperCorrelationFilter.GroupImage = null;
            this.grouperCorrelationFilter.GroupTitle = "Correlation Filter";
            this.grouperCorrelationFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grouperCorrelationFilter.Location = new System.Drawing.Point(16, 400);
            this.grouperCorrelationFilter.Name = "grouperCorrelationFilter";
            this.grouperCorrelationFilter.Padding = new System.Windows.Forms.Padding(40, 38, 40, 38);
            this.grouperCorrelationFilter.PaintGroupBox = true;
            this.grouperCorrelationFilter.RoundCorners = 4;
            this.grouperCorrelationFilter.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperCorrelationFilter.ShadowControl = false;
            this.grouperCorrelationFilter.ShadowThickness = 1;
            this.grouperCorrelationFilter.Size = new System.Drawing.Size(458, 386);
            this.grouperCorrelationFilter.TabIndex = 1;
            this.grouperCorrelationFilter.Visible = false;
            // 
            // txtCorrelationFilterTo
            // 
            this.txtCorrelationFilterTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorrelationFilterTo.Location = new System.Drawing.Point(111, 216);
            this.txtCorrelationFilterTo.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterTo.Name = "txtCorrelationFilterTo";
            this.txtCorrelationFilterTo.Size = new System.Drawing.Size(306, 20);
            this.txtCorrelationFilterTo.TabIndex = 15;
            // 
            // lblCorrelationFilterTo
            // 
            this.lblCorrelationFilterTo.AutoSize = true;
            this.lblCorrelationFilterTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterTo.Location = new System.Drawing.Point(6, 210);
            this.lblCorrelationFilterTo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterTo.Name = "lblCorrelationFilterTo";
            this.lblCorrelationFilterTo.Size = new System.Drawing.Size(20, 13);
            this.lblCorrelationFilterTo.TabIndex = 14;
            this.lblCorrelationFilterTo.Text = "To";
            // 
            // txtCorrelationFilterSessionId
            // 
            this.txtCorrelationFilterSessionId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorrelationFilterSessionId.Location = new System.Drawing.Point(111, 186);
            this.txtCorrelationFilterSessionId.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterSessionId.Name = "txtCorrelationFilterSessionId";
            this.txtCorrelationFilterSessionId.Size = new System.Drawing.Size(306, 20);
            this.txtCorrelationFilterSessionId.TabIndex = 13;
            // 
            // lblCorrelationFilterSessionId
            // 
            this.lblCorrelationFilterSessionId.AutoSize = true;
            this.lblCorrelationFilterSessionId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterSessionId.Location = new System.Drawing.Point(6, 180);
            this.lblCorrelationFilterSessionId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterSessionId.Name = "lblCorrelationFilterSessionId";
            this.lblCorrelationFilterSessionId.Size = new System.Drawing.Size(53, 13);
            this.lblCorrelationFilterSessionId.TabIndex = 12;
            this.lblCorrelationFilterSessionId.Text = "SessionId";
            // 
            // txtCorrelationFilterReplyToSessionId
            // 
            this.txtCorrelationFilterReplyToSessionId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorrelationFilterReplyToSessionId.Location = new System.Drawing.Point(111, 156);
            this.txtCorrelationFilterReplyToSessionId.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterReplyToSessionId.Name = "txtCorrelationFilterReplyToSessionId";
            this.txtCorrelationFilterReplyToSessionId.Size = new System.Drawing.Size(306, 20);
            this.txtCorrelationFilterReplyToSessionId.TabIndex = 11;
            // 
            // lblCorrelationFilterReplyToSessionId
            // 
            this.lblCorrelationFilterReplyToSessionId.AutoSize = true;
            this.lblCorrelationFilterReplyToSessionId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterReplyToSessionId.Location = new System.Drawing.Point(6, 150);
            this.lblCorrelationFilterReplyToSessionId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterReplyToSessionId.Name = "lblCorrelationFilterReplyToSessionId";
            this.lblCorrelationFilterReplyToSessionId.Size = new System.Drawing.Size(93, 13);
            this.lblCorrelationFilterReplyToSessionId.TabIndex = 10;
            this.lblCorrelationFilterReplyToSessionId.Text = "ReplyToSessionId";
            // 
            // txtCorrelationFilterReplyTo
            // 
            this.txtCorrelationFilterReplyTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorrelationFilterReplyTo.Location = new System.Drawing.Point(111, 126);
            this.txtCorrelationFilterReplyTo.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterReplyTo.Name = "txtCorrelationFilterReplyTo";
            this.txtCorrelationFilterReplyTo.Size = new System.Drawing.Size(306, 20);
            this.txtCorrelationFilterReplyTo.TabIndex = 9;
            // 
            // lblCorrelationFilterReplyTo
            // 
            this.lblCorrelationFilterReplyTo.AutoSize = true;
            this.lblCorrelationFilterReplyTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterReplyTo.Location = new System.Drawing.Point(6, 120);
            this.lblCorrelationFilterReplyTo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterReplyTo.Name = "lblCorrelationFilterReplyTo";
            this.lblCorrelationFilterReplyTo.Size = new System.Drawing.Size(47, 13);
            this.lblCorrelationFilterReplyTo.TabIndex = 8;
            this.lblCorrelationFilterReplyTo.Text = "ReplyTo";
            // 
            // txtCorrelationFilterMessageId
            // 
            this.txtCorrelationFilterMessageId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorrelationFilterMessageId.Location = new System.Drawing.Point(111, 96);
            this.txtCorrelationFilterMessageId.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterMessageId.Name = "txtCorrelationFilterMessageId";
            this.txtCorrelationFilterMessageId.Size = new System.Drawing.Size(306, 20);
            this.txtCorrelationFilterMessageId.TabIndex = 7;
            // 
            // lblCorrelationFilterMessageId
            // 
            this.lblCorrelationFilterMessageId.AutoSize = true;
            this.lblCorrelationFilterMessageId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterMessageId.Location = new System.Drawing.Point(6, 90);
            this.lblCorrelationFilterMessageId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterMessageId.Name = "lblCorrelationFilterMessageId";
            this.lblCorrelationFilterMessageId.Size = new System.Drawing.Size(59, 13);
            this.lblCorrelationFilterMessageId.TabIndex = 6;
            this.lblCorrelationFilterMessageId.Text = "MessageId";
            // 
            // txtCorrelationFilterLabel
            // 
            this.txtCorrelationFilterLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorrelationFilterLabel.Location = new System.Drawing.Point(111, 66);
            this.txtCorrelationFilterLabel.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterLabel.Name = "txtCorrelationFilterLabel";
            this.txtCorrelationFilterLabel.Size = new System.Drawing.Size(306, 20);
            this.txtCorrelationFilterLabel.TabIndex = 5;
            // 
            // lblCorrelationFilterLabel
            // 
            this.lblCorrelationFilterLabel.AutoSize = true;
            this.lblCorrelationFilterLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterLabel.Location = new System.Drawing.Point(6, 60);
            this.lblCorrelationFilterLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterLabel.Name = "lblCorrelationFilterLabel";
            this.lblCorrelationFilterLabel.Size = new System.Drawing.Size(33, 13);
            this.lblCorrelationFilterLabel.TabIndex = 4;
            this.lblCorrelationFilterLabel.Text = "Label";
            // 
            // txtCorrelationFilterCorrelationId
            // 
            this.txtCorrelationFilterCorrelationId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorrelationFilterCorrelationId.Location = new System.Drawing.Point(111, 36);
            this.txtCorrelationFilterCorrelationId.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterCorrelationId.Name = "txtCorrelationFilterCorrelationId";
            this.txtCorrelationFilterCorrelationId.Size = new System.Drawing.Size(306, 20);
            this.txtCorrelationFilterCorrelationId.TabIndex = 3;
            // 
            // lblCorrelationFilterCorrelationId
            // 
            this.lblCorrelationFilterCorrelationId.AutoSize = true;
            this.lblCorrelationFilterCorrelationId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterCorrelationId.Location = new System.Drawing.Point(6, 30);
            this.lblCorrelationFilterCorrelationId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCorrelationFilterCorrelationId.Name = "lblCorrelationFilterCorrelationId";
            this.lblCorrelationFilterCorrelationId.Size = new System.Drawing.Size(66, 13);
            this.lblCorrelationFilterCorrelationId.TabIndex = 2;
            this.lblCorrelationFilterCorrelationId.Text = "CorrelationId";
            // 
            // txtCorrelationFilterContentType
            // 
            this.txtCorrelationFilterContentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCorrelationFilterContentType.Location = new System.Drawing.Point(111, 6);
            this.txtCorrelationFilterContentType.Margin = new System.Windows.Forms.Padding(6);
            this.txtCorrelationFilterContentType.Name = "txtCorrelationFilterContentType";
            this.txtCorrelationFilterContentType.Size = new System.Drawing.Size(306, 20);
            this.txtCorrelationFilterContentType.TabIndex = 1;
            // 
            // lblCorrelationFilterContentType
            // 
            this.lblCorrelationFilterContentType.AutoSize = true;
            this.lblCorrelationFilterContentType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCorrelationFilterContentType.Location = new System.Drawing.Point(6, 0);
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
            this.tableLayoutPanel2.SetColumnSpan(this.authorizationRulesDataGridView, 2);
            this.authorizationRulesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.authorizationRulesDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.authorizationRulesDataGridView.Location = new System.Drawing.Point(8, 248);
            this.authorizationRulesDataGridView.Margin = new System.Windows.Forms.Padding(8);
            this.authorizationRulesDataGridView.MultiSelect = false;
            this.authorizationRulesDataGridView.Name = "authorizationRulesDataGridView";
            this.authorizationRulesDataGridView.RowHeadersWidth = 24;
            this.authorizationRulesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.authorizationRulesDataGridView.ShowCellErrors = false;
            this.authorizationRulesDataGridView.ShowRowErrors = false;
            this.authorizationRulesDataGridView.Size = new System.Drawing.Size(407, 77);
            this.authorizationRulesDataGridView.TabIndex = 0;
            this.authorizationRulesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.authorizationRulesDataGridView_RowsAdded);
            this.authorizationRulesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.authorizationRulesDataGridView_RowsRemoved);
            this.authorizationRulesDataGridView.Resize += new System.EventHandler(this.authorizationRulesDataGridView_Resize);
            // 
            // grouperCreatedAt
            // 
            this.grouperCreatedAt.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperCreatedAt.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperCreatedAt.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperCreatedAt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCreatedAt.BorderThickness = 1F;
            this.grouperCreatedAt.Controls.Add(this.txtCreatedAt);
            this.grouperCreatedAt.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCreatedAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperCreatedAt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperCreatedAt.ForeColor = System.Drawing.Color.White;
            this.grouperCreatedAt.GroupImage = null;
            this.grouperCreatedAt.GroupTitle = "Created At";
            this.grouperCreatedAt.Location = new System.Drawing.Point(478, 3);
            this.grouperCreatedAt.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.grouperCreatedAt.Name = "grouperCreatedAt";
            this.grouperCreatedAt.Padding = new System.Windows.Forms.Padding(20);
            this.grouperCreatedAt.PaintGroupBox = true;
            this.grouperCreatedAt.RoundCorners = 4;
            this.grouperCreatedAt.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperCreatedAt.ShadowControl = false;
            this.grouperCreatedAt.ShadowThickness = 1;
            this.grouperCreatedAt.Size = new System.Drawing.Size(211, 74);
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
            this.txtCreatedAt.Size = new System.Drawing.Size(179, 20);
            this.txtCreatedAt.TabIndex = 0;
            // 
            // grouperIsDefault
            // 
            this.grouperIsDefault.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperIsDefault.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperIsDefault.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperIsDefault.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperIsDefault.BorderThickness = 1F;
            this.grouperIsDefault.Controls.Add(this.checkBoxDefault);
            this.grouperIsDefault.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperIsDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperIsDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperIsDefault.ForeColor = System.Drawing.Color.White;
            this.grouperIsDefault.GroupImage = null;
            this.grouperIsDefault.GroupTitle = "Is Default?";
            this.grouperIsDefault.Location = new System.Drawing.Point(816, 3);
            this.grouperIsDefault.Name = "grouperIsDefault";
            this.grouperIsDefault.Padding = new System.Windows.Forms.Padding(20);
            this.grouperIsDefault.PaintGroupBox = true;
            this.grouperIsDefault.RoundCorners = 4;
            this.grouperIsDefault.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperIsDefault.ShadowControl = false;
            this.grouperIsDefault.ShadowThickness = 1;
            this.grouperIsDefault.Size = new System.Drawing.Size(117, 74);
            this.grouperIsDefault.TabIndex = 4;
            // 
            // checkBoxDefault
            // 
            this.checkBoxDefault.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxDefault.AutoSize = true;
            this.checkBoxDefault.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxDefault.Location = new System.Drawing.Point(29, 43);
            this.checkBoxDefault.Name = "checkBoxDefault";
            this.checkBoxDefault.Size = new System.Drawing.Size(60, 17);
            this.checkBoxDefault.TabIndex = 0;
            this.checkBoxDefault.Text = "Default";
            this.checkBoxDefault.UseVisualStyleBackColor = true;
            this.checkBoxDefault.CheckedChanged += new System.EventHandler(this.checkBoxDefault_CheckedChanged);
            // 
            // grouperName
            // 
            this.grouperName.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperName.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperName.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperName.BorderThickness = 1F;
            this.grouperName.Controls.Add(this.txtName);
            this.grouperName.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperName.ForeColor = System.Drawing.Color.White;
            this.grouperName.GroupImage = null;
            this.grouperName.GroupTitle = "Name";
            this.grouperName.Location = new System.Drawing.Point(3, 3);
            this.grouperName.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.grouperName.Name = "grouperName";
            this.grouperName.Padding = new System.Windows.Forms.Padding(20);
            this.grouperName.PaintGroupBox = true;
            this.grouperName.RoundCorners = 4;
            this.grouperName.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperName.ShadowControl = false;
            this.grouperName.ShadowThickness = 1;
            this.grouperName.Size = new System.Drawing.Size(455, 74);
            this.grouperName.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.SystemColors.Window;
            this.txtName.Location = new System.Drawing.Point(16, 40);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(423, 20);
            this.txtName.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel1.Controls.Add(this.grouperFilter, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grouperFilterType, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.grouperIsDefault, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.grouperName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grouperCreatedAt, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grouperAction, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(936, 344);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // grouperAction
            // 
            this.grouperAction.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperAction.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperAction.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperAction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAction.BorderThickness = 1F;
            this.tableLayoutPanel1.SetColumnSpan(this.grouperAction, 3);
            this.grouperAction.Controls.Add(this.txtSqlFilterAction);
            this.grouperAction.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperAction.ForeColor = System.Drawing.Color.White;
            this.grouperAction.GroupImage = null;
            this.grouperAction.GroupTitle = "Action";
            this.grouperAction.Location = new System.Drawing.Point(478, 83);
            this.grouperAction.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.grouperAction.Name = "grouperAction";
            this.grouperAction.Padding = new System.Windows.Forms.Padding(20);
            this.grouperAction.PaintGroupBox = true;
            this.grouperAction.RoundCorners = 4;
            this.grouperAction.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperAction.ShadowControl = false;
            this.grouperAction.ShadowThickness = 1;
            this.grouperAction.Size = new System.Drawing.Size(455, 258);
            this.grouperAction.TabIndex = 6;
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
            this.txtSqlFilterAction.Size = new System.Drawing.Size(423, 210);
            this.txtSqlFilterAction.TabIndex = 0;
            // 
            // grouperFilter
            // 
            this.grouperFilter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperFilter.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperFilter.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilter.BorderThickness = 1F;
            this.grouperFilter.Controls.Add(this.txtFilterExpression);
            this.grouperFilter.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperFilter.ForeColor = System.Drawing.Color.White;
            this.grouperFilter.GroupImage = null;
            this.grouperFilter.GroupTitle = "Filter";
            this.grouperFilter.Location = new System.Drawing.Point(3, 83);
            this.grouperFilter.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.grouperFilter.Name = "grouperFilter";
            this.grouperFilter.Padding = new System.Windows.Forms.Padding(20);
            this.grouperFilter.PaintGroupBox = true;
            this.grouperFilter.RoundCorners = 4;
            this.grouperFilter.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperFilter.ShadowControl = false;
            this.grouperFilter.ShadowThickness = 1;
            this.grouperFilter.Size = new System.Drawing.Size(455, 258);
            this.grouperFilter.TabIndex = 7;
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
            this.txtFilterExpression.Size = new System.Drawing.Size(423, 210);
            this.txtFilterExpression.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.Controls.Add(this.authorizationRulesDataGridView, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.txtCorrelationFilterTo, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblCorrelationFilterContentType, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtCorrelationFilterSessionId, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.lblCorrelationFilterTo, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.txtCorrelationFilterReplyToSessionId, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblCorrelationFilterCorrelationId, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtCorrelationFilterReplyTo, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblCorrelationFilterLabel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtCorrelationFilterMessageId, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblCorrelationFilterSessionId, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.txtCorrelationFilterLabel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblCorrelationFilterMessageId, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtCorrelationFilterCorrelationId, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCorrelationFilterReplyTo, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtCorrelationFilterContentType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblCorrelationFilterReplyToSessionId, 0, 5);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(19, 36);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(423, 333);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // HandleRuleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateDelete);
            this.Controls.Add(this.grouperCorrelationFilter);
            this.Name = "HandleRuleControl";
            this.Size = new System.Drawing.Size(968, 400);
            this.Resize += new System.EventHandler(this.HandleRuleControl_Resize);
            this.grouperFilterType.ResumeLayout(false);
            this.grouperFilterType.PerformLayout();
            this.grouperCorrelationFilter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.authorizationRulesDataGridView)).EndInit();
            this.grouperCreatedAt.ResumeLayout(false);
            this.grouperCreatedAt.PerformLayout();
            this.grouperIsDefault.ResumeLayout(false);
            this.grouperIsDefault.PerformLayout();
            this.grouperName.ResumeLayout(false);
            this.grouperName.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grouperAction.ResumeLayout(false);
            this.grouperAction.PerformLayout();
            this.grouperFilter.ResumeLayout(false);
            this.grouperFilter.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private Grouper grouperName;
        private System.Windows.Forms.TextBox txtName;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Grouper grouperFilter;
        private System.Windows.Forms.TextBox txtFilterExpression;
        private Grouper grouperAction;
        private System.Windows.Forms.TextBox txtSqlFilterAction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
