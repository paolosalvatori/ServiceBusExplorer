﻿#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    partial class ReceiveModeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiveModeForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.grouperMessages = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtMessageCount = new System.Windows.Forms.TextBox();
            this.btnTop = new System.Windows.Forms.RadioButton();
            this.btnAll = new System.Windows.Forms.RadioButton();
            this.grouperReadMode = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.btnReceive = new System.Windows.Forms.RadioButton();
            this.btnPeek = new System.Windows.Forms.RadioButton();
            this.mainPanel.SuspendLayout();
            this.grouperMessages.SuspendLayout();
            this.grouperReadMode.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(296, 97);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnOk.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOk.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(376, 97);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BackColor = System.Drawing.SystemColors.Window;
            this.mainPanel.Controls.Add(this.grouperMessages);
            this.mainPanel.Controls.Add(this.grouperReadMode);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(464, 88);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // grouperMessages
            // 
            this.grouperMessages.BackgroundColor = System.Drawing.Color.White;
            this.grouperMessages.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessages.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMessages.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessages.BorderThickness = 1F;
            this.grouperMessages.Controls.Add(this.txtMessageCount);
            this.grouperMessages.Controls.Add(this.btnTop);
            this.grouperMessages.Controls.Add(this.btnAll);
            this.grouperMessages.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessages.ForeColor = System.Drawing.Color.White;
            this.grouperMessages.GroupImage = null;
            this.grouperMessages.GroupTitle = "Message Count";
            this.grouperMessages.Location = new System.Drawing.Point(240, 8);
            this.grouperMessages.Name = "grouperMessages";
            this.grouperMessages.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessages.PaintGroupBox = true;
            this.grouperMessages.RoundCorners = 4;
            this.grouperMessages.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessages.ShadowControl = false;
            this.grouperMessages.ShadowThickness = 1;
            this.grouperMessages.Size = new System.Drawing.Size(208, 64);
            this.grouperMessages.TabIndex = 42;
            // 
            // txtMessageCount
            // 
            this.txtMessageCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageCount.Location = new System.Drawing.Point(112, 32);
            this.txtMessageCount.Name = "txtMessageCount";
            this.txtMessageCount.Size = new System.Drawing.Size(80, 20);
            this.txtMessageCount.TabIndex = 42;
            this.txtMessageCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessageCount_KeyPress);
            // 
            // btnTop
            // 
            this.btnTop.AutoSize = true;
            this.btnTop.Checked = true;
            this.btnTop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTop.Location = new System.Drawing.Point(64, 32);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(44, 17);
            this.btnTop.TabIndex = 41;
            this.btnTop.TabStop = true;
            this.btnTop.Text = "Top";
            this.btnTop.UseVisualStyleBackColor = true;
            this.btnTop.CheckedChanged += new System.EventHandler(this.btnTop_CheckedChanged);
            // 
            // btnAll
            // 
            this.btnAll.AutoSize = true;
            this.btnAll.Enabled = false;
            this.btnAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAll.Location = new System.Drawing.Point(16, 32);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(36, 17);
            this.btnAll.TabIndex = 40;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = true;
            // 
            // grouperReadMode
            // 
            this.grouperReadMode.BackgroundColor = System.Drawing.Color.White;
            this.grouperReadMode.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperReadMode.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperReadMode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperReadMode.BorderThickness = 1F;
            this.grouperReadMode.Controls.Add(this.btnReceive);
            this.grouperReadMode.Controls.Add(this.btnPeek);
            this.grouperReadMode.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperReadMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperReadMode.ForeColor = System.Drawing.Color.White;
            this.grouperReadMode.GroupImage = null;
            this.grouperReadMode.GroupTitle = "Receive Mode";
            this.grouperReadMode.Location = new System.Drawing.Point(16, 8);
            this.grouperReadMode.Name = "grouperReadMode";
            this.grouperReadMode.Padding = new System.Windows.Forms.Padding(20);
            this.grouperReadMode.PaintGroupBox = true;
            this.grouperReadMode.RoundCorners = 4;
            this.grouperReadMode.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperReadMode.ShadowControl = false;
            this.grouperReadMode.ShadowThickness = 1;
            this.grouperReadMode.Size = new System.Drawing.Size(208, 64);
            this.grouperReadMode.TabIndex = 41;
            // 
            // btnReceive
            // 
            this.btnReceive.AutoSize = true;
            this.btnReceive.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReceive.Location = new System.Drawing.Point(80, 32);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(120, 17);
            this.btnReceive.TabIndex = 39;
            this.btnReceive.Text = "Receive and Delete";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.CheckedChanged += new System.EventHandler(this.receiveMode_CheckedChanged);
            // 
            // btnPeek
            // 
            this.btnPeek.AutoSize = true;
            this.btnPeek.Checked = true;
            this.btnPeek.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPeek.Location = new System.Drawing.Point(16, 32);
            this.btnPeek.Name = "btnPeek";
            this.btnPeek.Size = new System.Drawing.Size(50, 17);
            this.btnPeek.TabIndex = 38;
            this.btnPeek.TabStop = true;
            this.btnPeek.Text = "Peek";
            this.btnPeek.UseVisualStyleBackColor = true;
            this.btnPeek.CheckedChanged += new System.EventHandler(this.receiveMode_CheckedChanged);
            // 
            // ReceiveModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(464, 133);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReceiveModeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Value";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ReceiveModeForm_KeyPress);
            this.mainPanel.ResumeLayout(false);
            this.grouperMessages.ResumeLayout(false);
            this.grouperMessages.PerformLayout();
            this.grouperReadMode.ResumeLayout(false);
            this.grouperReadMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel mainPanel;
        private Grouper grouperMessages;
        private System.Windows.Forms.TextBox txtMessageCount;
        private System.Windows.Forms.RadioButton btnTop;
        private System.Windows.Forms.RadioButton btnAll;
        private Grouper grouperReadMode;
        private System.Windows.Forms.RadioButton btnReceive;
        private System.Windows.Forms.RadioButton btnPeek;
    }
}