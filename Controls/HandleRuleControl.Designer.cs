namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    partial class HandleRuleControl
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
            this.grouperName = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grouperFilter = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtSqlFilterExpression = new System.Windows.Forms.TextBox();
            this.grouperAction = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtSqlFilterAction = new System.Windows.Forms.TextBox();
            this.grouperIsDefault = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.checkBoxDefault = new System.Windows.Forms.CheckBox();
            this.grouperCreatedAt = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtCreatedAt = new System.Windows.Forms.TextBox();
            this.grouperName.SuspendLayout();
            this.grouperFilter.SuspendLayout();
            this.grouperAction.SuspendLayout();
            this.grouperIsDefault.SuspendLayout();
            this.grouperCreatedAt.SuspendLayout();
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
            // grouperName
            // 
            this.grouperName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperName.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperName.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperName.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
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
            // grouperFilter
            // 
            this.grouperFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperFilter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperFilter.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperFilter.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilter.BorderThickness = 1F;
            this.grouperFilter.Controls.Add(this.txtSqlFilterExpression);
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
            // txtSqlFilterExpression
            // 
            this.txtSqlFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlFilterExpression.BackColor = System.Drawing.SystemColors.Window;
            this.txtSqlFilterExpression.Location = new System.Drawing.Point(16, 32);
            this.txtSqlFilterExpression.Multiline = true;
            this.txtSqlFilterExpression.Name = "txtSqlFilterExpression";
            this.txtSqlFilterExpression.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSqlFilterExpression.Size = new System.Drawing.Size(428, 208);
            this.txtSqlFilterExpression.TabIndex = 0;
            // 
            // grouperAction
            // 
            this.grouperAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperAction.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperAction.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperAction.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
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
            this.grouperAction.TabIndex = 4;
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
            // grouperIsDefault
            // 
            this.grouperIsDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperIsDefault.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperIsDefault.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperIsDefault.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperIsDefault.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperIsDefault.BorderThickness = 1F;
            this.grouperIsDefault.Controls.Add(this.checkBoxDefault);
            this.grouperIsDefault.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperIsDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperIsDefault.ForeColor = System.Drawing.Color.White;
            this.grouperIsDefault.GroupImage = null;
            this.grouperIsDefault.GroupTitle = "Is Default?";
            this.grouperIsDefault.Location = new System.Drawing.Point(848, 8);
            this.grouperIsDefault.Name = "grouperIsDefault";
            this.grouperIsDefault.Padding = new System.Windows.Forms.Padding(20);
            this.grouperIsDefault.PaintGroupBox = true;
            this.grouperIsDefault.RoundCorners = 4;
            this.grouperIsDefault.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperIsDefault.ShadowControl = false;
            this.grouperIsDefault.ShadowThickness = 1;
            this.grouperIsDefault.Size = new System.Drawing.Size(104, 80);
            this.grouperIsDefault.TabIndex = 2;
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
            // grouperCreatedAt
            // 
            this.grouperCreatedAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperCreatedAt.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperCreatedAt.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperCreatedAt.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
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
            this.grouperCreatedAt.Size = new System.Drawing.Size(340, 80);
            this.grouperCreatedAt.TabIndex = 1;
            // 
            // txtCreatedAt
            // 
            this.txtCreatedAt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCreatedAt.BackColor = System.Drawing.SystemColors.Window;
            this.txtCreatedAt.Location = new System.Drawing.Point(16, 40);
            this.txtCreatedAt.Name = "txtCreatedAt";
            this.txtCreatedAt.ReadOnly = true;
            this.txtCreatedAt.Size = new System.Drawing.Size(308, 20);
            this.txtCreatedAt.TabIndex = 0;
            // 
            // HandleRuleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
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
            this.grouperName.ResumeLayout(false);
            this.grouperName.PerformLayout();
            this.grouperFilter.ResumeLayout(false);
            this.grouperFilter.PerformLayout();
            this.grouperAction.ResumeLayout(false);
            this.grouperAction.PerformLayout();
            this.grouperIsDefault.ResumeLayout(false);
            this.grouperIsDefault.PerformLayout();
            this.grouperCreatedAt.ResumeLayout(false);
            this.grouperCreatedAt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private Grouper grouperName;
        private System.Windows.Forms.TextBox txtName;
        private Grouper grouperFilter;
        private System.Windows.Forms.TextBox txtSqlFilterExpression;
        private Grouper grouperAction;
        private System.Windows.Forms.TextBox txtSqlFilterAction;
        private Grouper grouperIsDefault;
        private System.Windows.Forms.CheckBox checkBoxDefault;
        private Grouper grouperCreatedAt;
        private System.Windows.Forms.TextBox txtCreatedAt;
    }
}
