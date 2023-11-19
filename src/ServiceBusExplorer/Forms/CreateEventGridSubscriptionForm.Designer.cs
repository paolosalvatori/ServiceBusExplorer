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
            this.grouperMessages = new ServiceBusExplorer.Controls.Grouper();
            this.lblSubscriptionName = new System.Windows.Forms.Label();
            this.txtSubscriptionName = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grouperMessages.SuspendLayout();
            this.SuspendLayout();
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
            this.grouperMessages.Controls.Add(this.lblSubscriptionName);
            this.grouperMessages.Controls.Add(this.txtSubscriptionName);
            this.grouperMessages.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessages.ForeColor = System.Drawing.Color.White;
            this.grouperMessages.GroupImage = null;
            this.grouperMessages.GroupTitle = "Create Subscription";
            this.grouperMessages.Location = new System.Drawing.Point(18, 15);
            this.grouperMessages.Name = "grouperMessages";
            this.grouperMessages.Padding = new System.Windows.Forms.Padding(20, 21, 20, 21);
            this.grouperMessages.PaintGroupBox = true;
            this.grouperMessages.RoundCorners = 4;
            this.grouperMessages.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessages.ShadowControl = false;
            this.grouperMessages.ShadowThickness = 1;
            this.grouperMessages.Size = new System.Drawing.Size(233, 104);
            this.grouperMessages.TabIndex = 43;
            // 
            // lblSubscriptionName
            // 
            this.lblSubscriptionName.AutoSize = true;
            this.lblSubscriptionName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubscriptionName.Location = new System.Drawing.Point(22, 41);
            this.lblSubscriptionName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubscriptionName.Name = "lblSubscriptionName";
            this.lblSubscriptionName.Size = new System.Drawing.Size(99, 13);
            this.lblSubscriptionName.TabIndex = 43;
            this.lblSubscriptionName.Text = "Subscription Name:";
            // 
            // txtSubscriptionName
            // 
            this.txtSubscriptionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubscriptionName.Location = new System.Drawing.Point(23, 61);
            this.txtSubscriptionName.Name = "txtSubscriptionName";
            this.txtSubscriptionName.Size = new System.Drawing.Size(194, 20);
            this.txtSubscriptionName.TabIndex = 42;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(95, 142);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 25);
            this.btnOk.TabIndex = 44;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(179, 142);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 25);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CreateEventGridSubscriptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(275, 181);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grouperMessages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateEventGridSubscriptionForm";
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
    }
}
