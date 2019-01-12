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

using Microsoft.Azure.ServiceBusExplorer.Helpers;

namespace Microsoft.Azure.ServiceBusExplorer.Forms
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.lblCorporation = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblExeVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mailLinkLabel = new System.Windows.Forms.LinkLabel();
            this.blogLinkLabel = new System.Windows.Forms.LinkLabel();
            this.twitterLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.siteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.lblClientVersion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCorporation
            // 
            this.lblCorporation.AutoSize = true;
            this.lblCorporation.BackColor = System.Drawing.Color.Transparent;
            this.lblCorporation.Location = new System.Drawing.Point(204, 327);
            this.lblCorporation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCorporation.Name = "lblCorporation";
            this.lblCorporation.Size = new System.Drawing.Size(135, 16);
            this.lblCorporation.TabIndex = 39;
            this.lblCorporation.Text = "Microsoft Corporation";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(204, 302);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(161, 22);
            this.lblName.TabIndex = 37;
            this.lblName.Text = "Paolo Salvatori";
            // 
            // lblExeVersion
            // 
            this.lblExeVersion.AutoSize = true;
            this.lblExeVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblExeVersion.Location = new System.Drawing.Point(204, 185);
            this.lblExeVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExeVersion.Name = "lblExeVersion";
            this.lblExeVersion.Size = new System.Drawing.Size(97, 16);
            this.lblExeVersion.TabIndex = 42;
            this.lblExeVersion.Text = "Version: 1.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(204, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 22);
            this.label2.TabIndex = 41;
            this.label2.Text = "Service Bus Explorer";
            // 
            // mailLinkLabel
            // 
            this.mailLinkLabel.AutoSize = true;
            this.mailLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.mailLinkLabel.Location = new System.Drawing.Point(330, 348);
            this.mailLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mailLinkLabel.Name = "mailLinkLabel";
            this.mailLinkLabel.Size = new System.Drawing.Size(147, 16);
            this.mailLinkLabel.TabIndex = 43;
            this.mailLinkLabel.TabStop = true;
            this.mailLinkLabel.Text = "paolos@microsoft.com";
            this.mailLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mailLinkLabel_LinkClicked);
            // 
            // blogLinkLabel
            // 
            this.blogLinkLabel.AutoSize = true;
            this.blogLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.blogLinkLabel.Location = new System.Drawing.Point(330, 368);
            this.blogLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.blogLinkLabel.Name = "blogLinkLabel";
            this.blogLinkLabel.Size = new System.Drawing.Size(185, 16);
            this.blogLinkLabel.TabIndex = 44;
            this.blogLinkLabel.TabStop = true;
            this.blogLinkLabel.Text = "http://blogs.msdn.com/paolos";
            this.blogLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.blogLinkLabel_LinkClicked);
            // 
            // twitterLinkLabel
            // 
            this.twitterLinkLabel.AutoSize = true;
            this.twitterLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.twitterLinkLabel.Location = new System.Drawing.Point(330, 387);
            this.twitterLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.twitterLinkLabel.Name = "twitterLinkLabel";
            this.twitterLinkLabel.Size = new System.Drawing.Size(175, 16);
            this.twitterLinkLabel.TabIndex = 45;
            this.twitterLinkLabel.TabStop = true;
            this.twitterLinkLabel.Text = "https://twitter.com/babosbird";
            this.twitterLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.twitterLinkLabel_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(205, 347);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 46;
            this.label1.Text = "Email:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(205, 366);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 18);
            this.label4.TabIndex = 47;
            this.label4.Text = "Blog:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(205, 386);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 18);
            this.label5.TabIndex = 48;
            this.label5.Text = "Twitter:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(205, 246);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 18);
            this.label3.TabIndex = 50;
            this.label3.Text = "Project site:";
            // 
            // siteLinkLabel
            // 
            this.siteLinkLabel.AutoSize = true;
            this.siteLinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.siteLinkLabel.Location = new System.Drawing.Point(330, 248);
            this.siteLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.siteLinkLabel.Name = "siteLinkLabel";
            this.siteLinkLabel.Size = new System.Drawing.Size(325, 16);
            this.siteLinkLabel.TabIndex = 49;
            this.siteLinkLabel.TabStop = true;
            this.siteLinkLabel.Text = "https://github.com/paolosalvatori/ServiceBusExplorer";
            this.siteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.siteLinkLabel_LinkClicked);
            // 
            // lblClientVersion
            // 
            this.lblClientVersion.AutoSize = true;
            this.lblClientVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblClientVersion.Location = new System.Drawing.Point(204, 226);
            this.lblClientVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClientVersion.Name = "lblClientVersion";
            this.lblClientVersion.Size = new System.Drawing.Size(191, 16);
            this.lblClientVersion.TabIndex = 52;
            this.lblClientVersion.Text = "Version: Could not be retrieved";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(204, 201);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 22);
            this.label7.TabIndex = 51;
            this.label7.Text = "Service Bus Client";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(779, 543);
            this.Controls.Add(this.lblClientVersion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.siteLinkLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.twitterLinkLabel);
            this.Controls.Add(this.blogLinkLabel);
            this.Controls.Add(this.mailLinkLabel);
            this.Controls.Add(this.lblExeVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCorporation);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label5);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " About Service Bus Explorer 1.0.0";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AboutForm_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCorporation;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblExeVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel mailLinkLabel;
        private System.Windows.Forms.LinkLabel blogLinkLabel;
        private System.Windows.Forms.LinkLabel twitterLinkLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel siteLinkLabel;
        private System.Windows.Forms.Label lblClientVersion;
        private System.Windows.Forms.Label label7;
    }
}
