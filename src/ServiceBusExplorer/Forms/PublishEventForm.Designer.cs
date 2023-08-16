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

using ServiceBusExplorer.Controls;

namespace ServiceBusExplorer.Forms
{
    partial class PublishEventForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PublishEventForm));
            this.messageListTextPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageText = new ServiceBusExplorer.Controls.Grouper();
            this.grouperMessageProperties = new ServiceBusExplorer.Controls.Grouper();
            this.grouperCaption = new ServiceBusExplorer.Controls.Grouper();
            this.lblEventSource = new System.Windows.Forms.Label();
            this.lblEventType = new System.Windows.Forms.Label();
            this.lblEventInfo = new System.Windows.Forms.Label();
            this.txtEventSource = new System.Windows.Forms.TextBox();
            this.txtEventType = new System.Windows.Forms.TextBox();
            this.txtEventInfo = new FastColoredTextBoxNS.FastColoredTextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).BeginInit();
            this.messageListTextPropertiesSplitContainer.SuspendLayout();
            this.grouperCaption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEventInfo)).BeginInit();
            this.grouperMessageText.SuspendLayout();
            this.grouperMessageProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageListTextPropertiesSplitContainer
            // 
            this.messageListTextPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messageListTextPropertiesSplitContainer.Name = "messageListTextPropertiesSplitContainer";
            this.messageListTextPropertiesSplitContainer.Size = new System.Drawing.Size(150, 100);
            this.messageListTextPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperCaption
            // 
            this.grouperCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperCaption.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperCaption.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperCaption.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperCaption.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCaption.BorderThickness = 1F;
            this.grouperCaption.Controls.Add(this.txtEventInfo);
            this.grouperCaption.Controls.Add(this.txtEventType);
            this.grouperCaption.Controls.Add(this.lblEventType);
            this.grouperCaption.Controls.Add(this.txtEventSource);
            this.grouperCaption.Controls.Add(this.lblEventInfo);
            this.grouperCaption.Controls.Add(this.lblEventSource);
            this.grouperCaption.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperCaption.ForeColor = System.Drawing.Color.White;
            this.grouperCaption.GroupImage = null;
            this.grouperCaption.GroupTitle = "Publish Event";
            this.grouperCaption.Location = new System.Drawing.Point(15, 16);
            this.grouperCaption.Name = "grouperCaption";
            this.grouperCaption.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.grouperCaption.PaintGroupBox = true;
            this.grouperCaption.RoundCorners = 4;
            this.grouperCaption.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperCaption.ShadowControl = false;
            this.grouperCaption.ShadowThickness = 1;
            this.grouperCaption.Size = new System.Drawing.Size(501, 316);
            this.grouperCaption.TabIndex = 34;
            // 
            // grouperMessageText
            // 
            this.grouperMessageText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageText.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.BorderThickness = 1F;
            this.grouperMessageText.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageText.ForeColor = System.Drawing.Color.White;
            this.grouperMessageText.GroupImage = null;
            this.grouperMessageText.GroupTitle = "Message Text";
            this.grouperMessageText.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperMessageText.Name = "grouperMessageText";
            this.grouperMessageText.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperMessageText.PaintGroupBox = true;
            this.grouperMessageText.RoundCorners = 4;
            this.grouperMessageText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageText.ShadowControl = false;
            this.grouperMessageText.ShadowThickness = 1;
            this.grouperMessageText.Size = new System.Drawing.Size(999, 340);
            this.grouperMessageText.TabIndex = 0;
            // 
            // grouperMessageProperties
            // 
            this.grouperMessageProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.BorderThickness = 1F;
            this.grouperMessageProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageProperties.GroupImage = null;
            this.grouperMessageProperties.GroupTitle = "Message Properties";
            this.grouperMessageProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperMessageProperties.Name = "grouperMessageProperties";
            this.grouperMessageProperties.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperMessageProperties.PaintGroupBox = true;
            this.grouperMessageProperties.RoundCorners = 4;
            this.grouperMessageProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageProperties.ShadowControl = false;
            this.grouperMessageProperties.ShadowThickness = 1;
            this.grouperMessageProperties.Size = new System.Drawing.Size(381, 729);
            this.grouperMessageProperties.TabIndex = 0;
            // 
            // lblEventSource
            // 
            this.lblEventSource.AutoSize = true;
            this.lblEventSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEventSource.Location = new System.Drawing.Point(16, 32);
            this.lblEventSource.Name = "lblEventSource";
            this.lblEventSource.Size = new System.Drawing.Size(75, 13);
            this.lblEventSource.TabIndex = 32;
            this.lblEventSource.Text = "Event Source:";
            // 
            // lblEventType
            // 
            this.lblEventType.AutoSize = true;
            this.lblEventType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEventType.Location = new System.Drawing.Point(16, 79);
            this.lblEventType.Name = "lblEventType";
            this.lblEventType.Size = new System.Drawing.Size(65, 13);
            this.lblEventType.TabIndex = 37;
            this.lblEventType.Text = "Event Type:";
            // 
            // lblEventInfo
            // 
            this.lblEventInfo.AutoSize = true;
            this.lblEventInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEventInfo.Location = new System.Drawing.Point(16, 126);
            this.lblEventInfo.Name = "lblEventInfo";
            this.lblEventInfo.Size = new System.Drawing.Size(110, 13);
            this.lblEventInfo.TabIndex = 35;
            this.lblEventInfo.Text = "Event JSON Payload:";
            // 
            // txtEventSource
            // 
            this.txtEventSource.Location = new System.Drawing.Point(16, 47);
            this.txtEventSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEventSource.Name = "txtEventSource";
            this.txtEventSource.Size = new System.Drawing.Size(474, 20);
            this.txtEventSource.TabIndex = 36;
            // 
            // txtEventType
            // 
            this.txtEventType.Location = new System.Drawing.Point(16, 94);
            this.txtEventType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEventType.Name = "txtEventType";
            this.txtEventType.Size = new System.Drawing.Size(474, 20);
            this.txtEventType.TabIndex = 38;
            // 
            // txtEventInfo
            // 
            this.txtEventInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEventInfo.AutoCompleteBracketsList = new char[] {
                '(',
                ')',
                '{',
                '}',
                '[',
                ']',
                '\"',
                '\"',
                '\'',
                '\''};
            this.txtEventInfo.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtEventInfo.BackBrush = null;
            this.txtEventInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEventInfo.CharHeight = 14;
            this.txtEventInfo.CharWidth = 8;
            this.txtEventInfo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEventInfo.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtEventInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtEventInfo.IsReplaceMode = false;
            this.txtEventInfo.Location = new System.Drawing.Point(16, 146);
            this.txtEventInfo.Name = "txtEventInfo";
            this.txtEventInfo.Paddings = new System.Windows.Forms.Padding(0);
            this.txtEventInfo.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtEventInfo.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtEventInfo.ServiceColors")));
            this.txtEventInfo.Size = new System.Drawing.Size(473, 153);
            this.txtEventInfo.TabIndex = 39;
            this.txtEventInfo.Zoom = 100;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Location = new System.Drawing.Point(361, 348);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(72, 24);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Publish";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(444, 348);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PublishEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(525, 385);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.grouperCaption);
            this.Name = "PublishEventForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publish Event";
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).EndInit();
            this.messageListTextPropertiesSplitContainer.ResumeLayout(false);
            this.grouperCaption.ResumeLayout(false);
            this.grouperCaption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEventInfo)).EndInit();
            this.grouperMessageText.ResumeLayout(false);
            this.grouperMessageText.PerformLayout();
            this.grouperMessageProperties.ResumeLayout(false);
            this.grouperMessageProperties.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer messageListTextPropertiesSplitContainer;
        private Grouper grouperMessageProperties;
        private Grouper grouperMessageText;
        private Grouper grouperCaption;
        private System.Windows.Forms.Label lblEventSource;
        private System.Windows.Forms.Label lblEventType;
        private System.Windows.Forms.Label lblEventInfo;
        private System.Windows.Forms.TextBox txtEventSource;
        private System.Windows.Forms.TextBox txtEventType;
        private FastColoredTextBoxNS.FastColoredTextBox txtEventInfo;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
    }
}
