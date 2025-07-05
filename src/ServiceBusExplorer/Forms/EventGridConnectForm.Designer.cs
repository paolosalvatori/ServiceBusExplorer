// Auto-added comment

#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

namespace ServiceBusExplorer.Forms
{
    partial class EventGridConnectForm
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventGridConnectForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbAzure = new System.Windows.Forms.PictureBox();
            this.grouperEventGridNamespaceSettings = new ServiceBusExplorer.Controls.Grouper();
            this.cboApiVersion = new System.Windows.Forms.ComboBox();
            this.cloudGroupBox = new System.Windows.Forms.GroupBox();
            this.btnPublicCloud = new System.Windows.Forms.RadioButton();
            this.btnCustomCloud = new System.Windows.Forms.RadioButton();
            this.txtCustomId = new System.Windows.Forms.TextBox();
            this.lblCloud = new System.Windows.Forms.Label();
            this.txtRetryTimeout = new System.Windows.Forms.TextBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.txtSubscriptionId = new System.Windows.Forms.TextBox();
            this.lblApiVersion = new System.Windows.Forms.Label();
            this.txtResourceGroupName = new System.Windows.Forms.TextBox();
            this.lblResourceGroupName = new System.Windows.Forms.Label();
            this.lblSubscriptionId = new System.Windows.Forms.Label();
            this.txtNamespaceName = new System.Windows.Forms.TextBox();
            this.lblNamespaceName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbAzure)).BeginInit();
            this.grouperEventGridNamespaceSettings.SuspendLayout();
            this.cloudGroupBox.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(235, 403);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 24);
            this.btnOk.TabIndex = 46;
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
            this.btnCancel.Location = new System.Drawing.Point(315, 403);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbAzure
            // 
            this.pbAzure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.pbAzure.BackgroundImage = global::ServiceBusExplorer.Properties.Resources.MicrosoftAzureWhiteLogo;
            this.pbAzure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbAzure.ErrorImage = null;
            this.pbAzure.Location = new System.Drawing.Point(281, 9);
            this.pbAzure.Name = "pbAzure";
            this.pbAzure.Size = new System.Drawing.Size(110, 14);
            this.pbAzure.TabIndex = 48;
            this.pbAzure.TabStop = false;
            // 
            // grouperEventGridNamespaceSettings
            // 
            this.grouperEventGridNamespaceSettings.BackgroundColor = System.Drawing.Color.White;
            this.grouperEventGridNamespaceSettings.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperEventGridNamespaceSettings.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperEventGridNamespaceSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventGridNamespaceSettings.BorderThickness = 1F;
            this.grouperEventGridNamespaceSettings.Controls.Add(this.cboApiVersion);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.cloudGroupBox);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.lblCloud);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.txtRetryTimeout);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.lblTimeout);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.txtSubscriptionId);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.lblApiVersion);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.txtResourceGroupName);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.lblResourceGroupName);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.lblSubscriptionId);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.txtNamespaceName);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.lblNamespaceName);
            this.grouperEventGridNamespaceSettings.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventGridNamespaceSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperEventGridNamespaceSettings.ForeColor = System.Drawing.Color.White;
            this.grouperEventGridNamespaceSettings.GroupImage = null;
            this.grouperEventGridNamespaceSettings.GroupTitle = "Context Settings";
            this.grouperEventGridNamespaceSettings.Location = new System.Drawing.Point(22, 25);
            this.grouperEventGridNamespaceSettings.Name = "grouperEventGridNamespaceSettings";
            this.grouperEventGridNamespaceSettings.Padding = new System.Windows.Forms.Padding(20);
            this.grouperEventGridNamespaceSettings.PaintGroupBox = true;
            this.grouperEventGridNamespaceSettings.RoundCorners = 4;
            this.grouperEventGridNamespaceSettings.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEventGridNamespaceSettings.ShadowControl = false;
            this.grouperEventGridNamespaceSettings.ShadowThickness = 1;
            this.grouperEventGridNamespaceSettings.Size = new System.Drawing.Size(368, 367);
            this.grouperEventGridNamespaceSettings.TabIndex = 37;
            // 
            // cboApiVersion
            // 
            this.cboApiVersion.FormattingEnabled = true;
            this.cboApiVersion.Items.AddRange(new object[] {
             "2023-12-15-preview",
             "2023-06-01-preview"});
            this.cboApiVersion.Location = new System.Drawing.Point(19, 200);
            this.cboApiVersion.Margin = new System.Windows.Forms.Padding(2);
            this.cboApiVersion.Name = "cboApiVersion";
            this.cboApiVersion.Size = new System.Drawing.Size(193, 21);
            this.cboApiVersion.TabIndex = 54;
            // 
            // cloudGroupBox
            // 
            this.cloudGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cloudGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.cloudGroupBox.Controls.Add(this.btnPublicCloud);
            this.cloudGroupBox.Controls.Add(this.btnCustomCloud);
            this.cloudGroupBox.Controls.Add(this.txtCustomId);
            this.cloudGroupBox.Location = new System.Drawing.Point(16, 289);
            this.cloudGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.cloudGroupBox.Name = "cloudGroupBox";
            this.cloudGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.cloudGroupBox.Size = new System.Drawing.Size(335, 45);
            this.cloudGroupBox.TabIndex = 53;
            this.cloudGroupBox.TabStop = false;
            // 
            // btnPublicCloud
            // 
            this.btnPublicCloud.AutoSize = true;
            this.btnPublicCloud.Checked = true;
            this.btnPublicCloud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPublicCloud.Location = new System.Drawing.Point(4, 7);
            this.btnPublicCloud.Name = "btnPublicCloud";
            this.btnPublicCloud.Size = new System.Drawing.Size(54, 17);
            this.btnPublicCloud.TabIndex = 40;
            this.btnPublicCloud.TabStop = true;
            this.btnPublicCloud.Text = "Public";
            this.btnPublicCloud.UseVisualStyleBackColor = true;
            // 
            // btnCustomCloud
            // 
            this.btnCustomCloud.AutoSize = true;
            this.btnCustomCloud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCustomCloud.Location = new System.Drawing.Point(4, 24);
            this.btnCustomCloud.Name = "btnCustomCloud";
            this.btnCustomCloud.Size = new System.Drawing.Size(111, 17);
            this.btnCustomCloud.TabIndex = 40;
            this.btnCustomCloud.Text = "Custom Tenant ID";
            this.btnCustomCloud.UseVisualStyleBackColor = true;
            this.btnCustomCloud.CheckedChanged += new System.EventHandler(this.customCloud_CheckedChanged);
            // 
            // txtCustomId
            // 
            this.txtCustomId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomId.Enabled = false;
            this.txtCustomId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCustomId.Location = new System.Drawing.Point(121, 19);
            this.txtCustomId.Name = "txtCustomId";
            this.txtCustomId.Size = new System.Drawing.Size(188, 20);
            this.txtCustomId.TabIndex = 41;
            // 
            // lblCloud
            // 
            this.lblCloud.AutoSize = true;
            this.lblCloud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCloud.Location = new System.Drawing.Point(16, 276);
            this.lblCloud.Name = "lblCloud";
            this.lblCloud.Size = new System.Drawing.Size(74, 13);
            this.lblCloud.TabIndex = 51;
            this.lblCloud.Text = "Cloud Tenant:";
            // 
            // txtRetryTimeout
            // 
            this.txtRetryTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRetryTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRetryTimeout.Location = new System.Drawing.Point(16, 242);
            this.txtRetryTimeout.Name = "txtRetryTimeout";
            this.txtRetryTimeout.Size = new System.Drawing.Size(103, 20);
            this.txtRetryTimeout.TabIndex = 19;
            this.txtRetryTimeout.Text = "60";
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTimeout.Location = new System.Drawing.Point(16, 227);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(90, 13);
            this.lblTimeout.TabIndex = 18;
            this.lblTimeout.Text = "Retry Timeout (s):";
            // 
            // txtSubscriptionId
            // 
            this.txtSubscriptionId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubscriptionId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSubscriptionId.Location = new System.Drawing.Point(16, 49);
            this.txtSubscriptionId.Name = "txtSubscriptionId";
            this.txtSubscriptionId.Size = new System.Drawing.Size(336, 20);
            this.txtSubscriptionId.TabIndex = 0;
            // 
            // lblApiVersion
            // 
            this.lblApiVersion.AutoSize = true;
            this.lblApiVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblApiVersion.Location = new System.Drawing.Point(16, 176);
            this.lblApiVersion.Name = "lblApiVersion";
            this.lblApiVersion.Size = new System.Drawing.Size(65, 13);
            this.lblApiVersion.TabIndex = 10;
            this.lblApiVersion.Text = "API Version:";
            // 
            // txtResourceGroupName
            // 
            this.txtResourceGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResourceGroupName.BackColor = System.Drawing.SystemColors.Window;
            this.txtResourceGroupName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtResourceGroupName.Location = new System.Drawing.Point(16, 97);
            this.txtResourceGroupName.Multiline = true;
            this.txtResourceGroupName.Name = "txtResourceGroupName";
            this.txtResourceGroupName.Size = new System.Drawing.Size(336, 20);
            this.txtResourceGroupName.TabIndex = 1;
            // 
            // lblResourceGroupName
            // 
            this.lblResourceGroupName.AutoSize = true;
            this.lblResourceGroupName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblResourceGroupName.Location = new System.Drawing.Point(16, 83);
            this.lblResourceGroupName.Name = "lblResourceGroupName";
            this.lblResourceGroupName.Size = new System.Drawing.Size(119, 13);
            this.lblResourceGroupName.TabIndex = 0;
            this.lblResourceGroupName.Text = "Resource Group Name:";
            // 
            // lblSubscriptionId
            // 
            this.lblSubscriptionId.AutoSize = true;
            this.lblSubscriptionId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubscriptionId.Location = new System.Drawing.Point(16, 34);
            this.lblSubscriptionId.Name = "lblSubscriptionId";
            this.lblSubscriptionId.Size = new System.Drawing.Size(82, 13);
            this.lblSubscriptionId.TabIndex = 6;
            this.lblSubscriptionId.Text = "Subscription ID:";
            // 
            // txtNamespaceName
            // 
            this.txtNamespaceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNamespaceName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNamespaceName.Location = new System.Drawing.Point(16, 145);
            this.txtNamespaceName.Name = "txtNamespaceName";
            this.txtNamespaceName.Size = new System.Drawing.Size(336, 20);
            this.txtNamespaceName.TabIndex = 3;
            // 
            // lblNamespaceName
            // 
            this.lblNamespaceName.AutoSize = true;
            this.lblNamespaceName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNamespaceName.Location = new System.Drawing.Point(16, 133);
            this.lblNamespaceName.Name = "lblNamespaceName";
            this.lblNamespaceName.Size = new System.Drawing.Size(98, 13);
            this.lblNamespaceName.TabIndex = 2;
            this.lblNamespaceName.Text = "Namespace Name:";
            // 
            // EventGridConnectForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(414, 442);
            this.Controls.Add(this.pbAzure);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grouperEventGridNamespaceSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventGridConnectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect to an Event Grid Namespace";
            this.Load += new System.EventHandler(this.EventGridConnectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbAzure)).EndInit();
            this.grouperEventGridNamespaceSettings.ResumeLayout(false);
            this.grouperEventGridNamespaceSettings.PerformLayout();
            this.cloudGroupBox.ResumeLayout(false);
            this.cloudGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Grouper grouperEventGridNamespaceSettings;
        private System.Windows.Forms.Label lblNamespaceName;
        private System.Windows.Forms.Label lblResourceGroupName;
        private System.Windows.Forms.Label lblSubscriptionId;
        private System.Windows.Forms.Label lblApiVersion;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Label lblCloud;
        private System.Windows.Forms.TextBox txtNamespaceName;
        private System.Windows.Forms.TextBox txtResourceGroupName;
        private System.Windows.Forms.TextBox txtSubscriptionId;
        private System.Windows.Forms.TextBox txtRetryTimeout;
        private System.Windows.Forms.GroupBox cloudGroupBox;
        private System.Windows.Forms.RadioButton btnPublicCloud;
        private System.Windows.Forms.RadioButton btnCustomCloud;
        private System.Windows.Forms.TextBox txtCustomId;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbAzure;
        private System.Windows.Forms.ComboBox cboApiVersion;
    }
}
