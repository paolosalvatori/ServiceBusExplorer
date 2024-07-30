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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelReleaseInfo = new System.Windows.Forms.Label();
            this.linkLabelnewVersion = new System.Windows.Forms.LinkLabel();
            this.labelLatestVersion = new System.Windows.Forms.Label();
            this.lblExeVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelReleaseInfo, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.linkLabelnewVersion, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelLatestVersion, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblExeVersion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 44);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 343);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // labelReleaseInfo
            // 
            this.labelReleaseInfo.AutoSize = true;
            this.labelReleaseInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelReleaseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelReleaseInfo.Location = new System.Drawing.Point(3, 100);
            this.labelReleaseInfo.Name = "labelReleaseInfo";
            this.labelReleaseInfo.Size = new System.Drawing.Size(554, 243);
            this.labelReleaseInfo.TabIndex = 6;
            this.labelReleaseInfo.Text = "releaseInfo";
            // 
            // linkLabelnewVersion
            // 
            this.linkLabelnewVersion.AutoSize = true;
            this.linkLabelnewVersion.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelnewVersion.ForeColor = System.Drawing.Color.Navy;
            this.linkLabelnewVersion.Location = new System.Drawing.Point(3, 75);
            this.linkLabelnewVersion.Name = "linkLabelnewVersion";
            this.linkLabelnewVersion.Size = new System.Drawing.Size(220, 13);
            this.linkLabelnewVersion.TabIndex = 5;
            this.linkLabelnewVersion.TabStop = true;
            this.linkLabelnewVersion.Text = "https://github.com/xxxxxxxxxxxxxxxx/xxxxxxx";
            this.linkLabelnewVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelnewVersion_LinkClicked);
            // 
            // labelLatestVersion
            // 
            this.labelLatestVersion.AutoSize = true;
            this.labelLatestVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelLatestVersion.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelLatestVersion.ForeColor = System.Drawing.Color.Red;
            this.labelLatestVersion.Location = new System.Drawing.Point(3, 50);
            this.labelLatestVersion.Name = "labelLatestVersion";
            this.labelLatestVersion.Size = new System.Drawing.Size(184, 16);
            this.labelLatestVersion.TabIndex = 4;
            this.labelLatestVersion.Text = "New Version is Available";
            // 
            // lblExeVersion
            // 
            this.lblExeVersion.AutoSize = true;
            this.lblExeVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblExeVersion.Location = new System.Drawing.Point(3, 25);
            this.lblExeVersion.Name = "lblExeVersion";
            this.lblExeVersion.Size = new System.Drawing.Size(81, 13);
            this.lblExeVersion.TabIndex = 3;
            this.lblExeVersion.Text = "Version: 1.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Service Bus Explorer";
            // 
            // NewVersionAvailableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.tableLayoutPanel1);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelReleaseInfo;
        private System.Windows.Forms.LinkLabel linkLabelnewVersion;
        private System.Windows.Forms.Label labelLatestVersion;
        private System.Windows.Forms.Label lblExeVersion;
        private System.Windows.Forms.Label label2;
    }
}
