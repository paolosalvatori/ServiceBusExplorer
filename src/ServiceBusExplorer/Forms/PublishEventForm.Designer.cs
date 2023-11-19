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
            this.grouperPublishEvent = new ServiceBusExplorer.Controls.Grouper();
            this.txtEventInfo = new FastColoredTextBoxNS.FastColoredTextBox();
            this.txtEventType = new System.Windows.Forms.TextBox();
            this.lblEventType = new System.Windows.Forms.Label();
            this.txtEventSource = new System.Windows.Forms.TextBox();
            this.lblEventInfo = new System.Windows.Forms.Label();
            this.lblEventSource = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).BeginInit();
            this.messageListTextPropertiesSplitContainer.SuspendLayout();
            this.grouperPublishEvent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEventInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // messageListTextPropertiesSplitContainer
            // 
            this.messageListTextPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messageListTextPropertiesSplitContainer.Name = "messageListTextPropertiesSplitContainer";
            this.messageListTextPropertiesSplitContainer.Size = new System.Drawing.Size(150, 100);
            this.messageListTextPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperPublishEvent
            // 
            this.grouperPublishEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperPublishEvent.BackgroundColor = System.Drawing.Color.White;
            this.grouperPublishEvent.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperPublishEvent.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperPublishEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPublishEvent.BorderThickness = 1F;
            this.grouperPublishEvent.Controls.Add(this.txtEventInfo);
            this.grouperPublishEvent.Controls.Add(this.txtEventType);
            this.grouperPublishEvent.Controls.Add(this.lblEventType);
            this.grouperPublishEvent.Controls.Add(this.txtEventSource);
            this.grouperPublishEvent.Controls.Add(this.lblEventInfo);
            this.grouperPublishEvent.Controls.Add(this.lblEventSource);
            this.grouperPublishEvent.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPublishEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperPublishEvent.ForeColor = System.Drawing.Color.White;
            this.grouperPublishEvent.GroupImage = null;
            this.grouperPublishEvent.GroupTitle = "Publish Event";
            this.grouperPublishEvent.Location = new System.Drawing.Point(15, 16);
            this.grouperPublishEvent.Name = "grouperPublishEvent";
            this.grouperPublishEvent.Padding = new System.Windows.Forms.Padding(20);
            this.grouperPublishEvent.PaintGroupBox = true;
            this.grouperPublishEvent.RoundCorners = 4;
            this.grouperPublishEvent.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperPublishEvent.ShadowControl = false;
            this.grouperPublishEvent.ShadowThickness = 1;
            this.grouperPublishEvent.Size = new System.Drawing.Size(501, 316);
            this.grouperPublishEvent.TabIndex = 34;
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
            this.txtEventInfo.Font = new System.Drawing.Font("Courier New", 9.75F);
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
            // txtEventType
            // 
            this.txtEventType.Location = new System.Drawing.Point(16, 94);
            this.txtEventType.Margin = new System.Windows.Forms.Padding(2);
            this.txtEventType.Name = "txtEventType";
            this.txtEventType.Size = new System.Drawing.Size(474, 20);
            this.txtEventType.TabIndex = 38;
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
            // txtEventSource
            // 
            this.txtEventSource.Location = new System.Drawing.Point(16, 47);
            this.txtEventSource.Margin = new System.Windows.Forms.Padding(2);
            this.txtEventSource.Name = "txtEventSource";
            this.txtEventSource.Size = new System.Drawing.Size(474, 20);
            this.txtEventSource.TabIndex = 36;
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
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Location = new System.Drawing.Point(361, 348);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
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
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
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
            this.Controls.Add(this.grouperPublishEvent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PublishEventForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publish Event";
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).EndInit();
            this.messageListTextPropertiesSplitContainer.ResumeLayout(false);
            this.grouperPublishEvent.ResumeLayout(false);
            this.grouperPublishEvent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEventInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer messageListTextPropertiesSplitContainer;
        private Grouper grouperPublishEvent;
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
