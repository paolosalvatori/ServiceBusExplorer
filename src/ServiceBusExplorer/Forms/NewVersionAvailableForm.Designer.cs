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

using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.Forms
{
    partial class NewVersionAvailableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        #region Private Fields
        private System.ComponentModel.IContainer components = null;
        #endregion

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewVersionAvailableForm));
            this.lblExeVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLatestVersion = new System.Windows.Forms.Label();
            this.linkLabelnewVersion = new System.Windows.Forms.LinkLabel();
            this.labelReleaseInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblExeVersion
            // 
            this.lblExeVersion.AutoSize = true;
            this.lblExeVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblExeVersion.Location = new System.Drawing.Point(16, 81);
            this.lblExeVersion.Name = "lblExeVersion";
            this.lblExeVersion.Size = new System.Drawing.Size(81, 13);
            this.lblExeVersion.TabIndex = 1;
            this.lblExeVersion.Text = "Version: 1.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Service Bus Explorer";
            // 
            // labelLatestVersion
            // 
            this.labelLatestVersion.AutoSize = true;
            this.labelLatestVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelLatestVersion.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelLatestVersion.ForeColor = System.Drawing.Color.Red;
            this.labelLatestVersion.Location = new System.Drawing.Point(16, 106);
            this.labelLatestVersion.Name = "labelLatestVersion";
            this.labelLatestVersion.Size = new System.Drawing.Size(185, 16);
            this.labelLatestVersion.TabIndex = 2;
            this.labelLatestVersion.Text = "New Version is Available";
            // 
            // linkLabelnewVersion
            // 
            this.linkLabelnewVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelnewVersion.AutoSize = true;
            this.linkLabelnewVersion.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelnewVersion.ForeColor = System.Drawing.Color.Navy;
            this.linkLabelnewVersion.Location = new System.Drawing.Point(16, 134);
            this.linkLabelnewVersion.Name = "linkLabelnewVersion";
            this.linkLabelnewVersion.Size = new System.Drawing.Size(220, 13);
            this.linkLabelnewVersion.TabIndex = 3;
            this.linkLabelnewVersion.TabStop = true;
            this.linkLabelnewVersion.Text = "https://github.com/xxxxxxxxxxxxxxxx/xxxxxxx";
            this.linkLabelnewVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelnewVersion_LinkClicked);
            // 
            // labelReleaseInfo
            // 
            this.labelReleaseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelReleaseInfo.AutoSize = true;
            this.labelReleaseInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelReleaseInfo.Location = new System.Drawing.Point(16, 159);
            this.labelReleaseInfo.Name = "labelReleaseInfo";
            this.labelReleaseInfo.Size = new System.Drawing.Size(59, 13);
            this.labelReleaseInfo.TabIndex = 4;
            this.labelReleaseInfo.Text = "releaseInfo";
            // 
            // NewVersionAvailableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.labelReleaseInfo);
            this.Controls.Add(this.labelLatestVersion);
            this.Controls.Add(this.linkLabelnewVersion);
            this.Controls.Add(this.lblExeVersion);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewVersionAvailableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Version Checker";
            this.Load += new System.EventHandler(this.NewVersionAvailableForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblExeVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLatestVersion;
        private System.Windows.Forms.LinkLabel linkLabelnewVersion;
        private System.Windows.Forms.Label labelReleaseInfo;
    }
}
