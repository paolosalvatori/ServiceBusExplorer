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
    partial class DateTimeRangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeRangeForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpFromDateTime = new ServiceBusExplorer.Controls.Grouper();
            this.dateFromTimePicker = new System.Windows.Forms.DateTimePicker();
            this.grpToDateTime = new ServiceBusExplorer.Controls.Grouper();
            this.dateToTimePicker = new System.Windows.Forms.DateTimePicker();
            this.grpFromDateTime.SuspendLayout();
            this.grpToDateTime.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(96, 204);
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
            this.btnCancel.Location = new System.Drawing.Point(176, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // grpFromDateTime
            // 
            this.grpFromDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFromDateTime.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grpFromDateTime.BackgroundGradientColor = System.Drawing.Color.White;
            this.grpFromDateTime.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grpFromDateTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grpFromDateTime.BorderThickness = 1F;
            this.grpFromDateTime.Controls.Add(this.dateFromTimePicker);
            this.grpFromDateTime.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grpFromDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grpFromDateTime.ForeColor = System.Drawing.Color.White;
            this.grpFromDateTime.GroupImage = null;
            this.grpFromDateTime.GroupTitle = "From Date Time";
            this.grpFromDateTime.Location = new System.Drawing.Point(16, 16);
            this.grpFromDateTime.Name = "grpFromDateTime";
            this.grpFromDateTime.Padding = new System.Windows.Forms.Padding(20);
            this.grpFromDateTime.PaintGroupBox = true;
            this.grpFromDateTime.RoundCorners = 4;
            this.grpFromDateTime.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpFromDateTime.ShadowControl = false;
            this.grpFromDateTime.ShadowThickness = 1;
            this.grpFromDateTime.Size = new System.Drawing.Size(232, 80);
            this.grpFromDateTime.TabIndex = 0;
            // 
            // dateFromTimePicker
            // 
            this.dateFromTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateFromTimePicker.Checked = false;
            this.dateFromTimePicker.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dateFromTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFromTimePicker.Location = new System.Drawing.Point(16, 32);
            this.dateFromTimePicker.Name = "dateFromTimePicker";
            this.dateFromTimePicker.ShowCheckBox = true;
            this.dateFromTimePicker.Size = new System.Drawing.Size(203, 20);
            this.dateFromTimePicker.TabIndex = 0;
            // 
            // grpToDateTime
            // 
            this.grpToDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpToDateTime.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grpToDateTime.BackgroundGradientColor = System.Drawing.Color.White;
            this.grpToDateTime.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grpToDateTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grpToDateTime.BorderThickness = 1F;
            this.grpToDateTime.Controls.Add(this.dateToTimePicker);
            this.grpToDateTime.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grpToDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grpToDateTime.ForeColor = System.Drawing.Color.White;
            this.grpToDateTime.GroupImage = null;
            this.grpToDateTime.GroupTitle = "To Date Time";
            this.grpToDateTime.Location = new System.Drawing.Point(16, 102);
            this.grpToDateTime.Name = "grpToDateTime";
            this.grpToDateTime.Padding = new System.Windows.Forms.Padding(20);
            this.grpToDateTime.PaintGroupBox = true;
            this.grpToDateTime.RoundCorners = 4;
            this.grpToDateTime.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpToDateTime.ShadowControl = false;
            this.grpToDateTime.ShadowThickness = 1;
            this.grpToDateTime.Size = new System.Drawing.Size(232, 80);
            this.grpToDateTime.TabIndex = 1;
            // 
            // dateToTimePicker
            // 
            this.dateToTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateToTimePicker.Checked = false;
            this.dateToTimePicker.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.dateToTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateToTimePicker.Location = new System.Drawing.Point(16, 32);
            this.dateToTimePicker.Name = "dateToTimePicker";
            this.dateToTimePicker.ShowCheckBox = true;
            this.dateToTimePicker.Size = new System.Drawing.Size(203, 20);
            this.dateToTimePicker.TabIndex = 0;
            // 
            // DateTimeRangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(264, 237);
            this.Controls.Add(this.grpToDateTime);
            this.Controls.Add(this.grpFromDateTime);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateTimeRangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Enqueued DateTime Range";
            this.grpFromDateTime.ResumeLayout(false);
            this.grpToDateTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Grouper grpFromDateTime;
        private System.Windows.Forms.DateTimePicker dateFromTimePicker;
        private Grouper grpToDateTime;
        private System.Windows.Forms.DateTimePicker dateToTimePicker;
    }
}
