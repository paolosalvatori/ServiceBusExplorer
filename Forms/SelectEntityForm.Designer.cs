#region Copyright
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
    partial class SelectEntityForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectEntityForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.grouperTreeView = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.lblSelectTargetEntity = new System.Windows.Forms.Label();
            this.lblTargetQueueTopic = new System.Windows.Forms.Label();
            this.txtEntity = new System.Windows.Forms.TextBox();
            this.serviceBusTreeView = new System.Windows.Forms.TreeView();
            this.btnClear = new System.Windows.Forms.Button();
            this.grouperTreeView.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(328, 432);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 24);
            this.btnOk.TabIndex = 2;
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
            this.btnCancel.Location = new System.Drawing.Point(408, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Queue.ico");
            this.imageList.Images.SetKeyName(1, "Topic.ico");
            this.imageList.Images.SetKeyName(2, "Chart.ico");
            this.imageList.Images.SetKeyName(3, "Class.ico");
            this.imageList.Images.SetKeyName(4, "Add.ico");
            this.imageList.Images.SetKeyName(5, "UserInfo.ico");
            this.imageList.Images.SetKeyName(6, "exec.ico");
            this.imageList.Images.SetKeyName(7, "AzureLogo.ico");
            this.imageList.Images.SetKeyName(8, "World.png");
            this.imageList.Images.SetKeyName(9, "RelayService.png");
            this.imageList.Images.SetKeyName(10, "folder_web.ico");
            this.imageList.Images.SetKeyName(11, "Web.ico");
            this.imageList.Images.SetKeyName(12, "GreyChart.ico");
            this.imageList.Images.SetKeyName(13, "GreyClass.ico");
            this.imageList.Images.SetKeyName(14, "GreyUserInfo.ico");
            // 
            // grouperTreeView
            // 
            this.grouperTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperTreeView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperTreeView.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperTreeView.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperTreeView.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTreeView.BorderThickness = 1F;
            this.grouperTreeView.Controls.Add(this.lblSelectTargetEntity);
            this.grouperTreeView.Controls.Add(this.lblTargetQueueTopic);
            this.grouperTreeView.Controls.Add(this.txtEntity);
            this.grouperTreeView.Controls.Add(this.serviceBusTreeView);
            this.grouperTreeView.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperTreeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperTreeView.ForeColor = System.Drawing.Color.White;
            this.grouperTreeView.GroupImage = null;
            this.grouperTreeView.GroupTitle = "Title";
            this.grouperTreeView.Location = new System.Drawing.Point(16, 16);
            this.grouperTreeView.Name = "grouperTreeView";
            this.grouperTreeView.Padding = new System.Windows.Forms.Padding(20);
            this.grouperTreeView.PaintGroupBox = true;
            this.grouperTreeView.RoundCorners = 4;
            this.grouperTreeView.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperTreeView.ShadowControl = false;
            this.grouperTreeView.ShadowThickness = 1;
            this.grouperTreeView.Size = new System.Drawing.Size(464, 400);
            this.grouperTreeView.TabIndex = 0;
            // 
            // lblSelectTargetEntity
            // 
            this.lblSelectTargetEntity.AutoSize = true;
            this.lblSelectTargetEntity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSelectTargetEntity.Location = new System.Drawing.Point(16, 32);
            this.lblSelectTargetEntity.Name = "lblSelectTargetEntity";
            this.lblSelectTargetEntity.Size = new System.Drawing.Size(104, 13);
            this.lblSelectTargetEntity.TabIndex = 24;
            this.lblSelectTargetEntity.Text = "Current Namespace:";
            // 
            // lblTargetQueueTopic
            // 
            this.lblTargetQueueTopic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTargetQueueTopic.AutoSize = true;
            this.lblTargetQueueTopic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTargetQueueTopic.Location = new System.Drawing.Point(16, 350);
            this.lblTargetQueueTopic.Name = "lblTargetQueueTopic";
            this.lblTargetQueueTopic.Size = new System.Drawing.Size(118, 13);
            this.lblTargetQueueTopic.TabIndex = 23;
            this.lblTargetQueueTopic.Text = "Target Queue or Topic:";
            // 
            // txtForwardTo
            // 
            this.txtEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntity.Location = new System.Drawing.Point(16, 366);
            this.txtEntity.Name = "txtEntity";
            this.txtEntity.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEntity.Size = new System.Drawing.Size(429, 20);
            this.txtEntity.TabIndex = 1;
            this.txtEntity.TextChanged += new System.EventHandler(this.txtForwardTo_TextChanged);
            // 
            // serviceBusTreeView
            // 
            this.serviceBusTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serviceBusTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serviceBusTreeView.ImageIndex = 0;
            this.serviceBusTreeView.ImageList = this.imageList;
            this.serviceBusTreeView.Indent = 20;
            this.serviceBusTreeView.ItemHeight = 20;
            this.serviceBusTreeView.Location = new System.Drawing.Point(16, 48);
            this.serviceBusTreeView.Name = "serviceBusTreeView";
            this.serviceBusTreeView.SelectedImageIndex = 0;
            this.serviceBusTreeView.Size = new System.Drawing.Size(429, 294);
            this.serviceBusTreeView.TabIndex = 0;
            this.serviceBusTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.serviceBusTreeView_NodeMouseClick);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(248, 432);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 24);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // SelectEntityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(496, 473);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.grouperTreeView);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectEntityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title";
            this.Activated += new System.EventHandler(this.TextForm_Activated);
            this.grouperTreeView.ResumeLayout(false);
            this.grouperTreeView.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Grouper grouperTreeView;
        private System.Windows.Forms.TreeView serviceBusTreeView;
        private System.Windows.Forms.TextBox txtEntity;
        private System.Windows.Forms.Label lblTargetQueueTopic;
        private System.Windows.Forms.Label lblSelectTargetEntity;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnClear;
    }
}