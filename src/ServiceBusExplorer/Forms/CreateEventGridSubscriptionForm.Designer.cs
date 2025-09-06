namespace ServiceBusExplorer.Forms
{
    partial class CreateEventGridSubscriptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateEventGridSubscriptionForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grouperMessages = new ServiceBusExplorer.Controls.Grouper();
            this.btnAddNewFilter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFilterEventType = new System.Windows.Forms.TextBox();
            this.textBoxFilterValue = new System.Windows.Forms.TextBox();
            this.comboBoxFilterValue = new System.Windows.Forms.ComboBox();
            this.labelFilterValue = new System.Windows.Forms.Label();
            this.comboBoxFilterOperator = new System.Windows.Forms.ComboBox();
            this.labelFilterOperator = new System.Windows.Forms.Label();
            this.labelFilterKey = new System.Windows.Forms.Label();
            this.textBoxFilterKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSubscriptionName = new System.Windows.Forms.Label();
            this.txtSubscriptionName = new System.Windows.Forms.TextBox();
            this.grouperMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(815, 645);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(108, 38);
            this.btnOk.TabIndex = 44;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(941, 645);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 38);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grouperMessages
            // 
            this.grouperMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperMessages.BackgroundColor = System.Drawing.Color.White;
            this.grouperMessages.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessages.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessages.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessages.BorderThickness = 1F;
            this.grouperMessages.Controls.Add(this.btnAddNewFilter);
            this.grouperMessages.Controls.Add(this.label2);
            this.grouperMessages.Controls.Add(this.textBoxFilterEventType);
            this.grouperMessages.Controls.Add(this.textBoxFilterValue);
            this.grouperMessages.Controls.Add(this.comboBoxFilterValue);
            this.grouperMessages.Controls.Add(this.labelFilterValue);
            this.grouperMessages.Controls.Add(this.comboBoxFilterOperator);
            this.grouperMessages.Controls.Add(this.labelFilterOperator);
            this.grouperMessages.Controls.Add(this.labelFilterKey);
            this.grouperMessages.Controls.Add(this.textBoxFilterKey);
            this.grouperMessages.Controls.Add(this.label1);
            this.grouperMessages.Controls.Add(this.lblSubscriptionName);
            this.grouperMessages.Controls.Add(this.txtSubscriptionName);
            this.grouperMessages.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessages.ForeColor = System.Drawing.Color.White;
            this.grouperMessages.GroupImage = null;
            this.grouperMessages.GroupTitle = "Create Subscription";
            this.grouperMessages.Location = new System.Drawing.Point(13, 22);
            this.grouperMessages.Margin = new System.Windows.Forms.Padding(4);
            this.grouperMessages.Name = "grouperMessages";
            this.grouperMessages.Padding = new System.Windows.Forms.Padding(30, 32, 30, 32);
            this.grouperMessages.PaintGroupBox = true;
            this.grouperMessages.RoundCorners = 4;
            this.grouperMessages.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessages.ShadowControl = false;
            this.grouperMessages.ShadowThickness = 1;
            this.grouperMessages.Size = new System.Drawing.Size(1035, 589);
            this.grouperMessages.TabIndex = 43;
            // 
            // btnAddNewFilter
            // 
            this.btnAddNewFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewFilter.BackColor = System.Drawing.Color.White;
            this.btnAddNewFilter.Enabled = false;
            this.btnAddNewFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnAddNewFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnAddNewFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnAddNewFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewFilter.ForeColor = System.Drawing.Color.Black;
            this.btnAddNewFilter.Location = new System.Drawing.Point(33, 490);
            this.btnAddNewFilter.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNewFilter.Name = "btnAddNewFilter";
            this.btnAddNewFilter.Size = new System.Drawing.Size(976, 41);
            this.btnAddNewFilter.TabIndex = 57;
            this.btnAddNewFilter.Text = "Add New Filter";
            this.btnAddNewFilter.UseVisualStyleBackColor = false;
            this.btnAddNewFilter.Click += new System.EventHandler(this.addFilter_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(549, 185);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 56;
            this.label2.Text = "Event Type:";
            // 
            // textBoxFilterEventType
            // 
            this.textBoxFilterEventType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilterEventType.Location = new System.Drawing.Point(553, 208);
            this.textBoxFilterEventType.Name = "textBoxFilterEventType";
            this.textBoxFilterEventType.Size = new System.Drawing.Size(456, 26);
            this.textBoxFilterEventType.TabIndex = 55;
            // 
            // textBoxFilterValue
            // 
            this.textBoxFilterValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilterValue.Location = new System.Drawing.Point(33, 345);
            this.textBoxFilterValue.Name = "textBoxFilterValue";
            this.textBoxFilterValue.Size = new System.Drawing.Size(472, 26);
            this.textBoxFilterValue.TabIndex = 54;
            this.textBoxFilterValue.Visible = false;
            this.textBoxFilterValue.TextChanged += new System.EventHandler(this.textBoxFilterValue_TextChanged);
            // 
            // comboBoxFilterValue
            // 
            this.comboBoxFilterValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterValue.FormattingEnabled = true;
            this.comboBoxFilterValue.Location = new System.Drawing.Point(33, 343);
            this.comboBoxFilterValue.Name = "comboBoxFilterValue";
            this.comboBoxFilterValue.Size = new System.Drawing.Size(472, 28);
            this.comboBoxFilterValue.TabIndex = 53;
            this.comboBoxFilterValue.Visible = false;
            this.comboBoxFilterValue.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterValue_SelectedIndexChanged);
            // 
            // labelFilterValue
            // 
            this.labelFilterValue.AutoSize = true;
            this.labelFilterValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelFilterValue.Location = new System.Drawing.Point(30, 320);
            this.labelFilterValue.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelFilterValue.Name = "labelFilterValue";
            this.labelFilterValue.Size = new System.Drawing.Size(56, 20);
            this.labelFilterValue.TabIndex = 52;
            this.labelFilterValue.Text = "Value:";
            // 
            // comboBoxFilterOperator
            // 
            this.comboBoxFilterOperator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterOperator.FormattingEnabled = true;
            this.comboBoxFilterOperator.Items.AddRange(new object[] {
             "Boolean equals",
             "String is in",
             "String is not in",
             "String contains",
             "String does not contain",
             "String begins with",
             "String does not begin with",
             "String ends with",
             "String does not end with",
             "Number is less than",
             "Number is greater than",
             "Number is less than or equal to",
             "Number is greater than or equal to",
             "Number is in",
             "Number is not in",
             "Number is in range",
             "Number is not in range",
             "Is null or undefined",
             "Is not null"});
            this.comboBoxFilterOperator.Location = new System.Drawing.Point(33, 273);
            this.comboBoxFilterOperator.Name = "comboBoxFilterOperator";
            this.comboBoxFilterOperator.Size = new System.Drawing.Size(472, 28);
            this.comboBoxFilterOperator.TabIndex = 51;
            this.comboBoxFilterOperator.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterOperator_SelectedIndexChanged);
            // 
            // labelFilterOperator
            // 
            this.labelFilterOperator.AutoSize = true;
            this.labelFilterOperator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelFilterOperator.Location = new System.Drawing.Point(30, 250);
            this.labelFilterOperator.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelFilterOperator.Name = "labelFilterOperator";
            this.labelFilterOperator.Size = new System.Drawing.Size(80, 20);
            this.labelFilterOperator.TabIndex = 50;
            this.labelFilterOperator.Text = "Operator:";
            // 
            // labelFilterKey
            // 
            this.labelFilterKey.AutoSize = true;
            this.labelFilterKey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelFilterKey.Location = new System.Drawing.Point(30, 185);
            this.labelFilterKey.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelFilterKey.Name = "labelFilterKey";
            this.labelFilterKey.Size = new System.Drawing.Size(42, 20);
            this.labelFilterKey.TabIndex = 49;
            this.labelFilterKey.Text = "Key:";
            // 
            // textBoxFilterKey
            // 
            this.textBoxFilterKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilterKey.Location = new System.Drawing.Point(33, 208);
            this.textBoxFilterKey.Name = "textBoxFilterKey";
            this.textBoxFilterKey.Size = new System.Drawing.Size(472, 26);
            this.textBoxFilterKey.TabIndex = 48;
            this.textBoxFilterKey.TextChanged += new System.EventHandler(this.textBoxFilterKey_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(33, 146);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Filters";
            // 
            // lblSubscriptionName
            // 
            this.lblSubscriptionName.AutoSize = true;
            this.lblSubscriptionName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubscriptionName.Location = new System.Drawing.Point(33, 62);
            this.lblSubscriptionName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSubscriptionName.Name = "lblSubscriptionName";
            this.lblSubscriptionName.Size = new System.Drawing.Size(156, 20);
            this.lblSubscriptionName.TabIndex = 43;
            this.lblSubscriptionName.Text = "Subscription Name:";
            // 
            // txtSubscriptionName
            // 
            this.txtSubscriptionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubscriptionName.Location = new System.Drawing.Point(34, 92);
            this.txtSubscriptionName.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubscriptionName.Name = "txtSubscriptionName";
            this.txtSubscriptionName.Size = new System.Drawing.Size(975, 26);
            this.txtSubscriptionName.TabIndex = 42;
            // 
            // CreateEventGridSubscriptionForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1085, 704);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grouperMessages);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateEventGridSubscriptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Event Grid Subscription";
            this.grouperMessages.ResumeLayout(false);
            this.grouperMessages.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Grouper grouperMessages;
        private System.Windows.Forms.Label lblSubscriptionName;
        private System.Windows.Forms.TextBox txtSubscriptionName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFilterKey;
        private System.Windows.Forms.TextBox textBoxFilterKey;
        private System.Windows.Forms.Label labelFilterOperator;
        private System.Windows.Forms.Label labelFilterValue;
        private System.Windows.Forms.ComboBox comboBoxFilterValue;
        private System.Windows.Forms.ComboBox comboBoxFilterOperator;
        private System.Windows.Forms.TextBox textBoxFilterValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFilterEventType;
        private System.Windows.Forms.Button btnAddNewFilter;
    }
}
