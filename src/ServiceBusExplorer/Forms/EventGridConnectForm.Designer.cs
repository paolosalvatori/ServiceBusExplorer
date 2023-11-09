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
        /// <summary>
        /// Required designer variable.
        /// </summary>
        #region Private Fields
        private System.ComponentModel.IContainer components = null;
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblResourceGroupName = new System.Windows.Forms.Label();
            this.lblNamespaceName = new System.Windows.Forms.Label();
            this.lblSubscriptionId = new System.Windows.Forms.Label();
            this.lblApiVersion = new System.Windows.Forms.Label();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.lblCloud = new System.Windows.Forms.Label();
            this.txtResourceGroupName = new System.Windows.Forms.TextBox();
            this.txtNamespaceName = new System.Windows.Forms.TextBox();
            this.txtSubscriptionId = new System.Windows.Forms.TextBox();
            this.txtApiVersion = new System.Windows.Forms.TextBox();
            this.txtRetryTimeout = new System.Windows.Forms.TextBox();
            this.cloudGroupBox = new System.Windows.Forms.GroupBox();
            this.btnPublicCloud = new System.Windows.Forms.RadioButton();
            this.btnFairfaxCloud = new System.Windows.Forms.RadioButton();
            this.btnMooncakeCloud = new System.Windows.Forms.RadioButton();
            this.btnCustomCloud = new System.Windows.Forms.RadioButton();
            this.txtCustomId = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbAzure = new System.Windows.Forms.PictureBox();
            this.grouperEventGridNamespaceSettings = new ServiceBusExplorer.Controls.Grouper();
            this.cloudGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAzure)).BeginInit();
            this.grouperEventGridNamespaceSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblResourceGroupName
            // 
            this.lblResourceGroupName.AutoSize = true;
            this.lblResourceGroupName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblResourceGroupName.Location = new System.Drawing.Point(24, 49);
            this.lblResourceGroupName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResourceGroupName.Name = "lblResourceGroupName";
            this.lblResourceGroupName.Size = new System.Drawing.Size(119, 13);
            this.lblResourceGroupName.TabIndex = 0;
            this.lblResourceGroupName.Text = "Resource Group Name:";
            // 
            // lblNamespaceName
            // 
            this.lblNamespaceName.AutoSize = true;
            this.lblNamespaceName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNamespaceName.Location = new System.Drawing.Point(24, 123);
            this.lblNamespaceName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNamespaceName.Name = "lblNamespaceName";
            this.lblNamespaceName.Size = new System.Drawing.Size(98, 13);
            this.lblNamespaceName.TabIndex = 2;
            this.lblNamespaceName.Text = "Namespace Name:";
            // 
            // lblSubscriptionId
            // 
            this.lblSubscriptionId.AutoSize = true;
            this.lblSubscriptionId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubscriptionId.Location = new System.Drawing.Point(24, 197);
            this.lblSubscriptionId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubscriptionId.Name = "lblSubscriptionId";
            this.lblSubscriptionId.Size = new System.Drawing.Size(82, 13);
            this.lblSubscriptionId.TabIndex = 6;
            this.lblSubscriptionId.Text = "Subscription ID:";
            // 
            // lblApiVersion
            // 
            this.lblApiVersion.AutoSize = true;
            this.lblApiVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblApiVersion.Location = new System.Drawing.Point(24, 270);
            this.lblApiVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApiVersion.Name = "lblApiVersion";
            this.lblApiVersion.Size = new System.Drawing.Size(65, 13);
            this.lblApiVersion.TabIndex = 10;
            this.lblApiVersion.Text = "API Version:";
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTimeout.Location = new System.Drawing.Point(24, 349);
            this.lblTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(76, 13);
            this.lblTimeout.TabIndex = 18;
            this.lblTimeout.Text = "Retry Timeout (ms):";
            // 
            // lblCloud
            // 
            this.lblCloud.AutoSize = true;
            this.lblCloud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCloud.Location = new System.Drawing.Point(24, 425);
            this.lblCloud.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCloud.Name = "lblCloud";
            this.lblCloud.Size = new System.Drawing.Size(74, 13);
            this.lblCloud.TabIndex = 51;
            this.lblCloud.Text = "Cloud Tenant:";
            // 
            // txtResourceGroupName
            // 
            this.txtResourceGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResourceGroupName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtResourceGroupName.Location = new System.Drawing.Point(24, 74);
            this.txtResourceGroupName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtResourceGroupName.Multiline = true;
            this.txtResourceGroupName.Name = "txtResourceGroupName";
            this.txtResourceGroupName.Size = new System.Drawing.Size(502, 29);
            this.txtResourceGroupName.TabIndex = 1;
            // 
            // txtNamespaceName
            // 
            this.txtNamespaceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNamespaceName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNamespaceName.Location = new System.Drawing.Point(24, 148);
            this.txtNamespaceName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNamespaceName.Name = "txtNamespaceName";
            this.txtNamespaceName.Size = new System.Drawing.Size(502, 20);
            this.txtNamespaceName.TabIndex = 3;
            // 
            // txtSubscriptionId
            // 
            this.txtSubscriptionId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubscriptionId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSubscriptionId.Location = new System.Drawing.Point(24, 220);
            this.txtSubscriptionId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSubscriptionId.Name = "txtSubscriptionId";
            this.txtSubscriptionId.Size = new System.Drawing.Size(502, 20);
            this.txtSubscriptionId.TabIndex = 16;
            // 
            // txtApiVersion
            // 
            this.txtApiVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApiVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtApiVersion.Location = new System.Drawing.Point(24, 297);
            this.txtApiVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtApiVersion.Name = "txtApiVersion";
            this.txtApiVersion.Size = new System.Drawing.Size(502, 20);
            this.txtApiVersion.TabIndex = 17;
            // 
            // txtRetryTimeout
            // 
            this.txtRetryTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRetryTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRetryTimeout.Location = new System.Drawing.Point(24, 373);
            this.txtRetryTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRetryTimeout.Name = "txtRetryTimeout";
            this.txtRetryTimeout.Size = new System.Drawing.Size(502, 20);
            this.txtRetryTimeout.TabIndex = 19;
            // 
            // cloudGroupBox
            // 
            this.cloudGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cloudGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.cloudGroupBox.Controls.Add(this.btnPublicCloud);
            this.cloudGroupBox.Controls.Add(this.btnFairfaxCloud);
            this.cloudGroupBox.Controls.Add(this.btnMooncakeCloud);
            this.cloudGroupBox.Controls.Add(this.btnCustomCloud);
            this.cloudGroupBox.Controls.Add(this.txtCustomId);
            this.cloudGroupBox.Location = new System.Drawing.Point(24, 445);
            this.cloudGroupBox.Name = "cloudGroupBox";
            this.cloudGroupBox.Size = new System.Drawing.Size(502, 130);
            this.cloudGroupBox.TabIndex = 53;
            this.cloudGroupBox.TabStop = false;
            // 
            // btnPublicCloud
            // 
            this.btnPublicCloud.AutoSize = true;
            this.btnPublicCloud.Checked = true;
            this.btnPublicCloud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPublicCloud.Location = new System.Drawing.Point(6, 11);
            this.btnPublicCloud.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPublicCloud.Name = "btnPublicCloud";
            this.btnPublicCloud.Size = new System.Drawing.Size(54, 17);
            this.btnPublicCloud.TabIndex = 40;
            this.btnPublicCloud.TabStop = true;
            this.btnPublicCloud.Text = "Public";
            this.btnPublicCloud.UseVisualStyleBackColor = true;
            // 
            // btnFairfaxCloud
            // 
            this.btnFairfaxCloud.AutoSize = true;
            this.btnFairfaxCloud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFairfaxCloud.Location = new System.Drawing.Point(6, 40);
            this.btnFairfaxCloud.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnFairfaxCloud.Name = "btnFairfaxCloud";
            this.btnFairfaxCloud.Size = new System.Drawing.Size(56, 17);
            this.btnFairfaxCloud.TabIndex = 40;
            this.btnFairfaxCloud.Text = "Fairfax";
            this.btnFairfaxCloud.UseVisualStyleBackColor = true;
            // 
            // btnMooncakeCloud
            // 
            this.btnMooncakeCloud.AutoSize = true;
            this.btnMooncakeCloud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMooncakeCloud.Location = new System.Drawing.Point(6, 68);
            this.btnMooncakeCloud.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMooncakeCloud.Name = "btnMooncakeCloud";
            this.btnMooncakeCloud.Size = new System.Drawing.Size(76, 17);
            this.btnMooncakeCloud.TabIndex = 40;
            this.btnMooncakeCloud.Text = "Mooncake";
            this.btnMooncakeCloud.UseVisualStyleBackColor = true;
            // 
            // btnCustomCloud
            // 
            this.btnCustomCloud.AutoSize = true;
            this.btnCustomCloud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCustomCloud.Location = new System.Drawing.Point(6, 96);
            this.btnCustomCloud.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.txtCustomId.Location = new System.Drawing.Point(178, 93);
            this.txtCustomId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCustomId.Name = "txtCustomId";
            this.txtCustomId.Size = new System.Drawing.Size(307, 20);
            this.txtCustomId.TabIndex = 41;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(353, 673);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(108, 37);
            this.btnOk.TabIndex = 46;
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
            this.btnCancel.Location = new System.Drawing.Point(473, 673);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 37);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbAzure
            // 
            this.pbAzure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAzure.BackgroundImage = global::ServiceBusExplorer.Properties.Resources.MicrosoftAzureWhiteLogo;
            this.pbAzure.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbAzure.ErrorImage = null;
            this.pbAzure.Location = new System.Drawing.Point(421, 14);
            this.pbAzure.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbAzure.Name = "pbAzure";
            this.pbAzure.Size = new System.Drawing.Size(165, 22);
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
            this.grouperEventGridNamespaceSettings.Controls.Add(this.cloudGroupBox);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.lblCloud);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.txtRetryTimeout);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.lblTimeout);
            this.grouperEventGridNamespaceSettings.Controls.Add(this.txtApiVersion);
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
            this.grouperEventGridNamespaceSettings.Location = new System.Drawing.Point(33, 39);
            this.grouperEventGridNamespaceSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperEventGridNamespaceSettings.Name = "grouperEventGridNamespaceSettings";
            this.grouperEventGridNamespaceSettings.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperEventGridNamespaceSettings.PaintGroupBox = true;
            this.grouperEventGridNamespaceSettings.RoundCorners = 4;
            this.grouperEventGridNamespaceSettings.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEventGridNamespaceSettings.ShadowControl = false;
            this.grouperEventGridNamespaceSettings.ShadowThickness = 1;
            this.grouperEventGridNamespaceSettings.Size = new System.Drawing.Size(552, 602);
            this.grouperEventGridNamespaceSettings.TabIndex = 37;
            // 
            // EventGridConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(621, 732);
            this.Controls.Add(this.pbAzure);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grouperEventGridNamespaceSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventGridConnectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to an Event Grid Namespace";
            this.cloudGroupBox.ResumeLayout(false);
            this.cloudGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAzure)).EndInit();
            this.grouperEventGridNamespaceSettings.ResumeLayout(false);
            this.grouperEventGridNamespaceSettings.PerformLayout();
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
        private System.Windows.Forms.TextBox txtApiVersion;
        private System.Windows.Forms.TextBox txtRetryTimeout;
        private System.Windows.Forms.GroupBox cloudGroupBox;
        private System.Windows.Forms.RadioButton btnPublicCloud;
        private System.Windows.Forms.RadioButton btnFairfaxCloud;
        private System.Windows.Forms.RadioButton btnMooncakeCloud;
        private System.Windows.Forms.RadioButton btnCustomCloud;
        private System.Windows.Forms.TextBox txtCustomId;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbAzure;
    }
}
