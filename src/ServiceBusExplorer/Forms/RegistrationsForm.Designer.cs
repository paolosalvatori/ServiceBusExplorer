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
    partial class RegistrationsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationsForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.txtTop = new ServiceBusExplorer.Controls.NumericTextBox();
            this.lblTop = new System.Windows.Forms.Label();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.txtPnsHandle = new System.Windows.Forms.TextBox();
            this.btnTag = new System.Windows.Forms.RadioButton();
            this.btnPnsHandle = new System.Windows.Forms.RadioButton();
            this.mainPanel.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(232, 120);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
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
            this.btnCancel.Location = new System.Drawing.Point(312, 120);
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
            this.mainPanel.Controls.Add(this.radioButton1);
            this.mainPanel.Controls.Add(this.txtTop);
            this.mainPanel.Controls.Add(this.lblTop);
            this.mainPanel.Controls.Add(this.txtTag);
            this.mainPanel.Controls.Add(this.txtPnsHandle);
            this.mainPanel.Controls.Add(this.btnTag);
            this.mainPanel.Controls.Add(this.btnPnsHandle);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(400, 112);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(24, 80);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(39, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "All:";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // txtTop
            // 
            this.txtTop.AllowDecimal = false;
            this.txtTop.AllowNegative = false;
            this.txtTop.AllowSpace = false;
            this.txtTop.Location = new System.Drawing.Point(232, 80);
            this.txtTop.Name = "txtTop";
            this.txtTop.Size = new System.Drawing.Size(152, 20);
            this.txtTop.TabIndex = 6;
            this.txtTop.Text = "100";
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(112, 84);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(113, 13);
            this.lblTop.TabIndex = 5;
            this.lblTop.Text = "Top Count/Page Size:";
            // 
            // txtTag
            // 
            this.txtTag.Enabled = false;
            this.txtTag.Location = new System.Drawing.Point(112, 48);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(272, 20);
            this.txtTag.TabIndex = 3;
            // 
            // txtPnsHandle
            // 
            this.txtPnsHandle.Enabled = false;
            this.txtPnsHandle.Location = new System.Drawing.Point(112, 16);
            this.txtPnsHandle.Name = "txtPnsHandle";
            this.txtPnsHandle.Size = new System.Drawing.Size(272, 20);
            this.txtPnsHandle.TabIndex = 1;
            // 
            // btnTag
            // 
            this.btnTag.AutoSize = true;
            this.btnTag.Location = new System.Drawing.Point(24, 48);
            this.btnTag.Name = "btnTag";
            this.btnTag.Size = new System.Drawing.Size(47, 17);
            this.btnTag.TabIndex = 2;
            this.btnTag.Text = "Tag:";
            this.btnTag.UseVisualStyleBackColor = true;
            this.btnTag.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // btnPnsHandle
            // 
            this.btnPnsHandle.AutoSize = true;
            this.btnPnsHandle.Location = new System.Drawing.Point(24, 16);
            this.btnPnsHandle.Name = "btnPnsHandle";
            this.btnPnsHandle.Size = new System.Drawing.Size(87, 17);
            this.btnPnsHandle.TabIndex = 0;
            this.btnPnsHandle.Text = "PNS Handle:";
            this.btnPnsHandle.UseVisualStyleBackColor = true;
            this.btnPnsHandle.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // RegistrationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(400, 153);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegistrationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Get Registrations";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RegistrationsForm_KeyPress);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.RadioButton btnPnsHandle;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.TextBox txtPnsHandle;
        private System.Windows.Forms.RadioButton btnTag;
        private System.Windows.Forms.Label lblTop;
        private ServiceBusExplorer.Controls.NumericTextBox txtTop;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}
