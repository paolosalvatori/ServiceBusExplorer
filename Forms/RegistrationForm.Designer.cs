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
    partial class RegistrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grouperCaption = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.lblRegistration = new System.Windows.Forms.Label();
            this.registrationPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.cboRegistrationType = new System.Windows.Forms.ComboBox();
            this.lblRegistrationType = new System.Windows.Forms.Label();
            this.grouperCaption.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(264, 248);
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
            this.btnCancel.Location = new System.Drawing.Point(344, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // grouperCaption
            // 
            this.grouperCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperCaption.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperCaption.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperCaption.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperCaption.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCaption.BorderThickness = 1F;
            this.grouperCaption.Controls.Add(this.lblRegistration);
            this.grouperCaption.Controls.Add(this.registrationPropertyGrid);
            this.grouperCaption.Controls.Add(this.cboRegistrationType);
            this.grouperCaption.Controls.Add(this.lblRegistrationType);
            this.grouperCaption.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperCaption.ForeColor = System.Drawing.Color.White;
            this.grouperCaption.GroupImage = null;
            this.grouperCaption.GroupTitle = "New Registration";
            this.grouperCaption.Location = new System.Drawing.Point(16, 16);
            this.grouperCaption.Name = "grouperCaption";
            this.grouperCaption.Padding = new System.Windows.Forms.Padding(20);
            this.grouperCaption.PaintGroupBox = true;
            this.grouperCaption.RoundCorners = 4;
            this.grouperCaption.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperCaption.ShadowControl = false;
            this.grouperCaption.ShadowThickness = 1;
            this.grouperCaption.Size = new System.Drawing.Size(400, 216);
            this.grouperCaption.TabIndex = 33;
            this.grouperCaption.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperCaption_CustomPaint);
            // 
            // lblRegistration
            // 
            this.lblRegistration.AutoSize = true;
            this.lblRegistration.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRegistration.Location = new System.Drawing.Point(16, 80);
            this.lblRegistration.Name = "lblRegistration";
            this.lblRegistration.Size = new System.Drawing.Size(66, 13);
            this.lblRegistration.TabIndex = 35;
            this.lblRegistration.Text = "Registration:";
            // 
            // registrationPropertyGrid
            // 
            this.registrationPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.registrationPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.registrationPropertyGrid.HelpVisible = false;
            this.registrationPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.registrationPropertyGrid.Location = new System.Drawing.Point(16, 96);
            this.registrationPropertyGrid.Name = "registrationPropertyGrid";
            this.registrationPropertyGrid.Size = new System.Drawing.Size(368, 104);
            this.registrationPropertyGrid.TabIndex = 34;
            this.registrationPropertyGrid.ToolbarVisible = false;
            // 
            // cboRegistrationType
            // 
            this.cboRegistrationType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRegistrationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRegistrationType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRegistrationType.FormattingEnabled = true;
            this.cboRegistrationType.Location = new System.Drawing.Point(16, 48);
            this.cboRegistrationType.Name = "cboRegistrationType";
            this.cboRegistrationType.Size = new System.Drawing.Size(368, 21);
            this.cboRegistrationType.TabIndex = 33;
            this.cboRegistrationType.SelectedIndexChanged += new System.EventHandler(this.cboRegistrationType_SelectedIndexChanged);
            // 
            // lblRegistrationType
            // 
            this.lblRegistrationType.AutoSize = true;
            this.lblRegistrationType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRegistrationType.Location = new System.Drawing.Point(16, 32);
            this.lblRegistrationType.Name = "lblRegistrationType";
            this.lblRegistrationType.Size = new System.Drawing.Size(93, 13);
            this.lblRegistrationType.TabIndex = 32;
            this.lblRegistrationType.Text = "Registration Type:";
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(432, 281);
            this.Controls.Add(this.grouperCaption);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New Registration";
            this.grouperCaption.ResumeLayout(false);
            this.grouperCaption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Grouper grouperCaption;
        private System.Windows.Forms.ComboBox cboRegistrationType;
        private System.Windows.Forms.Label lblRegistrationType;
        private System.Windows.Forms.Label lblRegistration;
        private System.Windows.Forms.PropertyGrid registrationPropertyGrid;
    }
}