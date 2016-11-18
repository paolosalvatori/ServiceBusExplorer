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
    partial class ConnectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnClearSubscriptionFilterExpression = new System.Windows.Forms.Button();
            this.btnClearTopicFilterExpression = new System.Windows.Forms.Button();
            this.btnClearQueueFilterExpression = new System.Windows.Forms.Button();
            this.btnOpenSubscriptionFilterForm = new System.Windows.Forms.Button();
            this.txtSubscriptionFilterExpression = new System.Windows.Forms.TextBox();
            this.btnOpenTopicFilterForm = new System.Windows.Forms.Button();
            this.btnOpenQueueFilterForm = new System.Windows.Forms.Button();
            this.txtQueueFilterExpression = new System.Windows.Forms.TextBox();
            this.txtTopicFilterExpression = new System.Windows.Forms.TextBox();
            this.txtIssuerSecret = new System.Windows.Forms.TextBox();
            this.txtIssuerName = new System.Windows.Forms.TextBox();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.grouperFilters = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.lblSubscriptionFilterExpression = new System.Windows.Forms.Label();
            this.lblQueueFilterExpression = new System.Windows.Forms.Label();
            this.lblTopicFilterExpression = new System.Windows.Forms.Label();
            this.grouperServiceBusNamespaceSettings = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.cboTransportType = new System.Windows.Forms.ComboBox();
            this.lblTransportType = new System.Windows.Forms.Label();
            this.cboConnectivityMode = new System.Windows.Forms.ComboBox();
            this.lblConnectivityMode = new System.Windows.Forms.Label();
            this.txtUri = new System.Windows.Forms.TextBox();
            this.lblUri = new System.Windows.Forms.Label();
            this.lblIssuerSecret = new System.Windows.Forms.Label();
            this.lblIssuerName = new System.Windows.Forms.Label();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.grouperServiceBusNamespaces = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.cboServiceBusNamespace = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.grouperFilters.SuspendLayout();
            this.grouperServiceBusNamespaceSettings.SuspendLayout();
            this.grouperServiceBusNamespaces.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(232, 640);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 24);
            this.btnOk.TabIndex = 3;
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
            this.btnCancel.Location = new System.Drawing.Point(312, 640);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnClearSubscriptionFilterExpression
            // 
            this.btnClearSubscriptionFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSubscriptionFilterExpression.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClearSubscriptionFilterExpression.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClearSubscriptionFilterExpression.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearSubscriptionFilterExpression.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearSubscriptionFilterExpression.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearSubscriptionFilterExpression.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSubscriptionFilterExpression.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearSubscriptionFilterExpression.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.btnClearSubscriptionFilterExpression.Location = new System.Drawing.Point(328, 144);
            this.btnClearSubscriptionFilterExpression.Name = "btnClearSubscriptionFilterExpression";
            this.btnClearSubscriptionFilterExpression.Size = new System.Drawing.Size(24, 21);
            this.btnClearSubscriptionFilterExpression.TabIndex = 8;
            this.btnClearSubscriptionFilterExpression.Text = "X";
            this.btnClearSubscriptionFilterExpression.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.btnClearSubscriptionFilterExpression, "Click to cancel the filter expression for subscriptions.");
            this.btnClearSubscriptionFilterExpression.UseVisualStyleBackColor = false;
            this.btnClearSubscriptionFilterExpression.Click += new System.EventHandler(this.btnClearSubscriptionFilterExpression_Click);
            this.btnClearSubscriptionFilterExpression.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnClearSubscriptionFilterExpression.MouseLeave += new System.EventHandler(this.clearButton_MouseLeave);
            // 
            // btnClearTopicFilterExpression
            // 
            this.btnClearTopicFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearTopicFilterExpression.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClearTopicFilterExpression.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClearTopicFilterExpression.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearTopicFilterExpression.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearTopicFilterExpression.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearTopicFilterExpression.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearTopicFilterExpression.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearTopicFilterExpression.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.btnClearTopicFilterExpression.Location = new System.Drawing.Point(328, 96);
            this.btnClearTopicFilterExpression.Name = "btnClearTopicFilterExpression";
            this.btnClearTopicFilterExpression.Size = new System.Drawing.Size(24, 21);
            this.btnClearTopicFilterExpression.TabIndex = 5;
            this.btnClearTopicFilterExpression.Text = "X";
            this.btnClearTopicFilterExpression.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.btnClearTopicFilterExpression, "Click to cancel the filter expression for topics.");
            this.btnClearTopicFilterExpression.UseVisualStyleBackColor = false;
            this.btnClearTopicFilterExpression.Click += new System.EventHandler(this.btnClearTopicFilterExpression_Click);
            this.btnClearTopicFilterExpression.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnClearTopicFilterExpression.MouseLeave += new System.EventHandler(this.clearButton_MouseLeave);
            // 
            // btnClearQueueFilterExpression
            // 
            this.btnClearQueueFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearQueueFilterExpression.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClearQueueFilterExpression.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClearQueueFilterExpression.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearQueueFilterExpression.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearQueueFilterExpression.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearQueueFilterExpression.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearQueueFilterExpression.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClearQueueFilterExpression.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.btnClearQueueFilterExpression.Location = new System.Drawing.Point(328, 48);
            this.btnClearQueueFilterExpression.Name = "btnClearQueueFilterExpression";
            this.btnClearQueueFilterExpression.Size = new System.Drawing.Size(24, 21);
            this.btnClearQueueFilterExpression.TabIndex = 2;
            this.btnClearQueueFilterExpression.Text = "X";
            this.btnClearQueueFilterExpression.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.btnClearQueueFilterExpression, "Click to cancel the filter expression for queues.");
            this.btnClearQueueFilterExpression.UseVisualStyleBackColor = false;
            this.btnClearQueueFilterExpression.Click += new System.EventHandler(this.btnClearQueueFilterExpression_Click);
            this.btnClearQueueFilterExpression.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnClearQueueFilterExpression.MouseLeave += new System.EventHandler(this.clearButton_MouseLeave);
            // 
            // btnOpenSubscriptionFilterForm
            // 
            this.btnOpenSubscriptionFilterForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSubscriptionFilterForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenSubscriptionFilterForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenSubscriptionFilterForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenSubscriptionFilterForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenSubscriptionFilterForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSubscriptionFilterForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenSubscriptionFilterForm.Location = new System.Drawing.Point(296, 144);
            this.btnOpenSubscriptionFilterForm.Name = "btnOpenSubscriptionFilterForm";
            this.btnOpenSubscriptionFilterForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenSubscriptionFilterForm.TabIndex = 7;
            this.btnOpenSubscriptionFilterForm.Text = "...";
            this.btnOpenSubscriptionFilterForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.btnOpenSubscriptionFilterForm, "Click to open the filter expression dialog for subscriptions.");
            this.btnOpenSubscriptionFilterForm.UseVisualStyleBackColor = false;
            this.btnOpenSubscriptionFilterForm.Click += new System.EventHandler(this.btnOpenSubscriptionFilterForm_Click);
            this.btnOpenSubscriptionFilterForm.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOpenSubscriptionFilterForm.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // txtSubscriptionFilterExpression
            // 
            this.txtSubscriptionFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubscriptionFilterExpression.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSubscriptionFilterExpression.Location = new System.Drawing.Point(16, 144);
            this.txtSubscriptionFilterExpression.Name = "txtSubscriptionFilterExpression";
            this.txtSubscriptionFilterExpression.Size = new System.Drawing.Size(272, 20);
            this.txtSubscriptionFilterExpression.TabIndex = 6;
            this.toolTip.SetToolTip(this.txtSubscriptionFilterExpression, "Gets or sets the OData filter for topics.");
            // 
            // btnOpenTopicFilterForm
            // 
            this.btnOpenTopicFilterForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenTopicFilterForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenTopicFilterForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenTopicFilterForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenTopicFilterForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenTopicFilterForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTopicFilterForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenTopicFilterForm.Location = new System.Drawing.Point(296, 96);
            this.btnOpenTopicFilterForm.Name = "btnOpenTopicFilterForm";
            this.btnOpenTopicFilterForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenTopicFilterForm.TabIndex = 4;
            this.btnOpenTopicFilterForm.Text = "...";
            this.btnOpenTopicFilterForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.btnOpenTopicFilterForm, "Click to open the filter expression dialog for topics.");
            this.btnOpenTopicFilterForm.UseVisualStyleBackColor = false;
            this.btnOpenTopicFilterForm.Click += new System.EventHandler(this.btnOpenTopicFilterForm_Click);
            this.btnOpenTopicFilterForm.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOpenTopicFilterForm.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnOpenQueueFilterForm
            // 
            this.btnOpenQueueFilterForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenQueueFilterForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenQueueFilterForm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenQueueFilterForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenQueueFilterForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenQueueFilterForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenQueueFilterForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenQueueFilterForm.Location = new System.Drawing.Point(296, 48);
            this.btnOpenQueueFilterForm.Name = "btnOpenQueueFilterForm";
            this.btnOpenQueueFilterForm.Size = new System.Drawing.Size(24, 21);
            this.btnOpenQueueFilterForm.TabIndex = 1;
            this.btnOpenQueueFilterForm.Text = "...";
            this.btnOpenQueueFilterForm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.btnOpenQueueFilterForm, "Click to open the filter expression dialog for queues.");
            this.btnOpenQueueFilterForm.UseVisualStyleBackColor = false;
            this.btnOpenQueueFilterForm.Click += new System.EventHandler(this.btnOpenQueueFilterForm_Click);
            this.btnOpenQueueFilterForm.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOpenQueueFilterForm.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // txtQueueFilterExpression
            // 
            this.txtQueueFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueueFilterExpression.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtQueueFilterExpression.Location = new System.Drawing.Point(16, 48);
            this.txtQueueFilterExpression.Multiline = true;
            this.txtQueueFilterExpression.Name = "txtQueueFilterExpression";
            this.txtQueueFilterExpression.Size = new System.Drawing.Size(272, 20);
            this.txtQueueFilterExpression.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtQueueFilterExpression, "Gets or sets the OData filter for queues.");
            // 
            // txtTopicFilterExpression
            // 
            this.txtTopicFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTopicFilterExpression.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTopicFilterExpression.Location = new System.Drawing.Point(16, 96);
            this.txtTopicFilterExpression.Name = "txtTopicFilterExpression";
            this.txtTopicFilterExpression.Size = new System.Drawing.Size(272, 20);
            this.txtTopicFilterExpression.TabIndex = 3;
            this.toolTip.SetToolTip(this.txtTopicFilterExpression, "Gets or sets the OData filter for topics.");
            // 
            // txtIssuerSecret
            // 
            this.txtIssuerSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIssuerSecret.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtIssuerSecret.Location = new System.Drawing.Point(16, 192);
            this.txtIssuerSecret.Name = "txtIssuerSecret";
            this.txtIssuerSecret.PasswordChar = '*';
            this.txtIssuerSecret.Size = new System.Drawing.Size(336, 20);
            this.txtIssuerSecret.TabIndex = 3;
            this.toolTip.SetToolTip(this.txtIssuerSecret, "Gets or sets the shared secret issuer secret.");
            this.txtIssuerSecret.TextChanged += new System.EventHandler(this.validation_TextChanged);
            // 
            // txtIssuerName
            // 
            this.txtIssuerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIssuerName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtIssuerName.Location = new System.Drawing.Point(16, 144);
            this.txtIssuerName.Name = "txtIssuerName";
            this.txtIssuerName.Size = new System.Drawing.Size(336, 20);
            this.txtIssuerName.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtIssuerName, "Gets or sets the shared secret issuer name.");
            this.txtIssuerName.TextChanged += new System.EventHandler(this.validation_TextChanged);
            // 
            // txtNamespace
            // 
            this.txtNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNamespace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNamespace.Location = new System.Drawing.Point(16, 96);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(336, 20);
            this.txtNamespace.TabIndex = 1;
            this.toolTip.SetToolTip(this.txtNamespace, "Gets or sets the name of the Service Bus namespace.");
            this.txtNamespace.TextChanged += new System.EventHandler(this.validation_TextChanged);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logoPictureBox.BackgroundImage = global::Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Properties.Resources.WindowsAzureLogo;
            this.logoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoPictureBox.ErrorImage = null;
            this.logoPictureBox.Location = new System.Drawing.Point(275, 8);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(110, 16);
            this.logoPictureBox.TabIndex = 34;
            this.logoPictureBox.TabStop = false;
            // 
            // grouperFilters
            // 
            this.grouperFilters.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperFilters.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperFilters.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperFilters.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilters.BorderThickness = 1F;
            this.grouperFilters.Controls.Add(this.btnClearSubscriptionFilterExpression);
            this.grouperFilters.Controls.Add(this.btnClearTopicFilterExpression);
            this.grouperFilters.Controls.Add(this.btnClearQueueFilterExpression);
            this.grouperFilters.Controls.Add(this.btnOpenSubscriptionFilterForm);
            this.grouperFilters.Controls.Add(this.txtSubscriptionFilterExpression);
            this.grouperFilters.Controls.Add(this.lblSubscriptionFilterExpression);
            this.grouperFilters.Controls.Add(this.btnOpenTopicFilterForm);
            this.grouperFilters.Controls.Add(this.btnOpenQueueFilterForm);
            this.grouperFilters.Controls.Add(this.txtQueueFilterExpression);
            this.grouperFilters.Controls.Add(this.lblQueueFilterExpression);
            this.grouperFilters.Controls.Add(this.txtTopicFilterExpression);
            this.grouperFilters.Controls.Add(this.lblTopicFilterExpression);
            this.grouperFilters.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperFilters.ForeColor = System.Drawing.Color.White;
            this.grouperFilters.GroupImage = null;
            this.grouperFilters.GroupTitle = "Filter Expressions";
            this.grouperFilters.Location = new System.Drawing.Point(16, 440);
            this.grouperFilters.Name = "grouperFilters";
            this.grouperFilters.Padding = new System.Windows.Forms.Padding(20);
            this.grouperFilters.PaintGroupBox = true;
            this.grouperFilters.RoundCorners = 4;
            this.grouperFilters.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperFilters.ShadowControl = false;
            this.grouperFilters.ShadowThickness = 1;
            this.grouperFilters.Size = new System.Drawing.Size(368, 184);
            this.grouperFilters.TabIndex = 2;
            // 
            // lblSubscriptionFilterExpression
            // 
            this.lblSubscriptionFilterExpression.AutoSize = true;
            this.lblSubscriptionFilterExpression.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSubscriptionFilterExpression.Location = new System.Drawing.Point(16, 128);
            this.lblSubscriptionFilterExpression.Name = "lblSubscriptionFilterExpression";
            this.lblSubscriptionFilterExpression.Size = new System.Drawing.Size(147, 13);
            this.lblSubscriptionFilterExpression.TabIndex = 51;
            this.lblSubscriptionFilterExpression.Text = "Subscription Filter Expression:";
            // 
            // lblQueueFilterExpression
            // 
            this.lblQueueFilterExpression.AutoSize = true;
            this.lblQueueFilterExpression.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblQueueFilterExpression.Location = new System.Drawing.Point(16, 32);
            this.lblQueueFilterExpression.Name = "lblQueueFilterExpression";
            this.lblQueueFilterExpression.Size = new System.Drawing.Size(121, 13);
            this.lblQueueFilterExpression.TabIndex = 47;
            this.lblQueueFilterExpression.Text = "Queue Filter Expression:";
            // 
            // lblTopicFilterExpression
            // 
            this.lblTopicFilterExpression.AutoSize = true;
            this.lblTopicFilterExpression.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTopicFilterExpression.Location = new System.Drawing.Point(16, 80);
            this.lblTopicFilterExpression.Name = "lblTopicFilterExpression";
            this.lblTopicFilterExpression.Size = new System.Drawing.Size(116, 13);
            this.lblTopicFilterExpression.TabIndex = 46;
            this.lblTopicFilterExpression.Text = "Topic Filter Expression:";
            // 
            // grouperServiceBusNamespaceSettings
            // 
            this.grouperServiceBusNamespaceSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperServiceBusNamespaceSettings.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperServiceBusNamespaceSettings.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperServiceBusNamespaceSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperServiceBusNamespaceSettings.BorderThickness = 1F;
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.cboTransportType);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.lblTransportType);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.cboConnectivityMode);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.lblConnectivityMode);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.txtUri);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.lblUri);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.txtIssuerSecret);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.lblIssuerSecret);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.txtIssuerName);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.lblIssuerName);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.txtNamespace);
            this.grouperServiceBusNamespaceSettings.Controls.Add(this.lblNamespace);
            this.grouperServiceBusNamespaceSettings.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperServiceBusNamespaceSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperServiceBusNamespaceSettings.ForeColor = System.Drawing.Color.White;
            this.grouperServiceBusNamespaceSettings.GroupImage = null;
            this.grouperServiceBusNamespaceSettings.GroupTitle = "Connection Settings";
            this.grouperServiceBusNamespaceSettings.Location = new System.Drawing.Point(16, 104);
            this.grouperServiceBusNamespaceSettings.Name = "grouperServiceBusNamespaceSettings";
            this.grouperServiceBusNamespaceSettings.Padding = new System.Windows.Forms.Padding(20);
            this.grouperServiceBusNamespaceSettings.PaintGroupBox = true;
            this.grouperServiceBusNamespaceSettings.RoundCorners = 4;
            this.grouperServiceBusNamespaceSettings.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperServiceBusNamespaceSettings.ShadowControl = false;
            this.grouperServiceBusNamespaceSettings.ShadowThickness = 1;
            this.grouperServiceBusNamespaceSettings.Size = new System.Drawing.Size(368, 328);
            this.grouperServiceBusNamespaceSettings.TabIndex = 1;
            this.grouperServiceBusNamespaceSettings.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperServiceBusNamespaceSettings_CustomPaint);
            // 
            // cboTransportType
            // 
            this.cboTransportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTransportType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTransportType.FormattingEnabled = true;
            this.cboTransportType.Location = new System.Drawing.Point(16, 288);
            this.cboTransportType.Name = "cboTransportType";
            this.cboTransportType.Size = new System.Drawing.Size(336, 21);
            this.cboTransportType.TabIndex = 71;
            this.cboTransportType.SelectedIndexChanged += new System.EventHandler(this.cboTransportType_SelectedIndexChanged);
            // 
            // lblTransportType
            // 
            this.lblTransportType.AutoSize = true;
            this.lblTransportType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTransportType.Location = new System.Drawing.Point(16, 272);
            this.lblTransportType.Name = "lblTransportType";
            this.lblTransportType.Size = new System.Drawing.Size(82, 13);
            this.lblTransportType.TabIndex = 70;
            this.lblTransportType.Text = "Transport Type:";
            // 
            // cboConnectivityMode
            // 
            this.cboConnectivityMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnectivityMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboConnectivityMode.FormattingEnabled = true;
            this.cboConnectivityMode.Location = new System.Drawing.Point(16, 240);
            this.cboConnectivityMode.Name = "cboConnectivityMode";
            this.cboConnectivityMode.Size = new System.Drawing.Size(336, 21);
            this.cboConnectivityMode.TabIndex = 69;
            // 
            // lblConnectivityMode
            // 
            this.lblConnectivityMode.AutoSize = true;
            this.lblConnectivityMode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblConnectivityMode.Location = new System.Drawing.Point(16, 224);
            this.lblConnectivityMode.Name = "lblConnectivityMode";
            this.lblConnectivityMode.Size = new System.Drawing.Size(98, 13);
            this.lblConnectivityMode.TabIndex = 45;
            this.lblConnectivityMode.Text = "Connectivity Mode:";
            // 
            // txtUri
            // 
            this.txtUri.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUri.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUri.Location = new System.Drawing.Point(16, 48);
            this.txtUri.Multiline = true;
            this.txtUri.Name = "txtUri";
            this.txtUri.Size = new System.Drawing.Size(336, 20);
            this.txtUri.TabIndex = 0;
            this.txtUri.TextChanged += new System.EventHandler(this.validation_TextChanged);
            // 
            // lblUri
            // 
            this.lblUri.AutoSize = true;
            this.lblUri.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUri.Location = new System.Drawing.Point(16, 32);
            this.lblUri.Name = "lblUri";
            this.lblUri.Size = new System.Drawing.Size(74, 13);
            this.lblUri.TabIndex = 43;
            this.lblUri.Text = "Endpoint URI:";
            // 
            // lblIssuerSecret
            // 
            this.lblIssuerSecret.AutoSize = true;
            this.lblIssuerSecret.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIssuerSecret.Location = new System.Drawing.Point(16, 176);
            this.lblIssuerSecret.Name = "lblIssuerSecret";
            this.lblIssuerSecret.Size = new System.Drawing.Size(143, 13);
            this.lblIssuerSecret.TabIndex = 40;
            this.lblIssuerSecret.Text = "Shared Secret Issuer Secret:";
            // 
            // lblIssuerName
            // 
            this.lblIssuerName.AutoSize = true;
            this.lblIssuerName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIssuerName.Location = new System.Drawing.Point(16, 128);
            this.lblIssuerName.Name = "lblIssuerName";
            this.lblIssuerName.Size = new System.Drawing.Size(140, 13);
            this.lblIssuerName.TabIndex = 39;
            this.lblIssuerName.Text = "Shared Secret Issuer Name:";
            // 
            // lblNamespace
            // 
            this.lblNamespace.AutoSize = true;
            this.lblNamespace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNamespace.Location = new System.Drawing.Point(16, 80);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(67, 13);
            this.lblNamespace.TabIndex = 38;
            this.lblNamespace.Text = "Namespace:";
            // 
            // grouperServiceBusNamespaces
            // 
            this.grouperServiceBusNamespaces.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperServiceBusNamespaces.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperServiceBusNamespaces.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperServiceBusNamespaces.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperServiceBusNamespaces.BorderThickness = 1F;
            this.grouperServiceBusNamespaces.Controls.Add(this.cboServiceBusNamespace);
            this.grouperServiceBusNamespaces.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperServiceBusNamespaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperServiceBusNamespaces.ForeColor = System.Drawing.Color.White;
            this.grouperServiceBusNamespaces.GroupImage = null;
            this.grouperServiceBusNamespaces.GroupTitle = "Service Bus Namespaces";
            this.grouperServiceBusNamespaces.Location = new System.Drawing.Point(16, 24);
            this.grouperServiceBusNamespaces.Name = "grouperServiceBusNamespaces";
            this.grouperServiceBusNamespaces.Padding = new System.Windows.Forms.Padding(20);
            this.grouperServiceBusNamespaces.PaintGroupBox = true;
            this.grouperServiceBusNamespaces.RoundCorners = 4;
            this.grouperServiceBusNamespaces.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperServiceBusNamespaces.ShadowControl = false;
            this.grouperServiceBusNamespaces.ShadowThickness = 1;
            this.grouperServiceBusNamespaces.Size = new System.Drawing.Size(368, 72);
            this.grouperServiceBusNamespaces.TabIndex = 0;
            this.grouperServiceBusNamespaces.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperServiceBusNamespaces_CustomPaint);
            // 
            // cboServiceBusNamespace
            // 
            this.cboServiceBusNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboServiceBusNamespace.BackColor = System.Drawing.SystemColors.Window;
            this.cboServiceBusNamespace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServiceBusNamespace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboServiceBusNamespace.FormattingEnabled = true;
            this.cboServiceBusNamespace.Location = new System.Drawing.Point(16, 32);
            this.cboServiceBusNamespace.Name = "cboServiceBusNamespace";
            this.cboServiceBusNamespace.Size = new System.Drawing.Size(336, 21);
            this.cboServiceBusNamespace.TabIndex = 0;
            this.cboServiceBusNamespace.SelectedIndexChanged += new System.EventHandler(this.cboServiceBusNamespace_SelectedIndexChanged);
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(402, 673);
            this.Controls.Add(this.grouperFilters);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.grouperServiceBusNamespaceSettings);
            this.Controls.Add(this.grouperServiceBusNamespaces);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect to a Service Bus Namespace";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ConnectForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.grouperFilters.ResumeLayout(false);
            this.grouperFilters.PerformLayout();
            this.grouperServiceBusNamespaceSettings.ResumeLayout(false);
            this.grouperServiceBusNamespaceSettings.PerformLayout();
            this.grouperServiceBusNamespaces.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Grouper grouperServiceBusNamespaces;
        private System.Windows.Forms.ComboBox cboServiceBusNamespace;
        private Grouper grouperServiceBusNamespaceSettings;
        private System.Windows.Forms.TextBox txtUri;
        private System.Windows.Forms.Label lblUri;
        private System.Windows.Forms.TextBox txtIssuerSecret;
        private System.Windows.Forms.Label lblIssuerSecret;
        private System.Windows.Forms.TextBox txtIssuerName;
        private System.Windows.Forms.Label lblIssuerName;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.ToolTip toolTip;
        private Grouper grouperFilters;
        private System.Windows.Forms.TextBox txtQueueFilterExpression;
        private System.Windows.Forms.Label lblQueueFilterExpression;
        private System.Windows.Forms.TextBox txtTopicFilterExpression;
        private System.Windows.Forms.Label lblTopicFilterExpression;
        private System.Windows.Forms.Button btnOpenTopicFilterForm;
        private System.Windows.Forms.Button btnOpenQueueFilterForm;
        private System.Windows.Forms.Button btnOpenSubscriptionFilterForm;
        private System.Windows.Forms.TextBox txtSubscriptionFilterExpression;
        private System.Windows.Forms.Label lblSubscriptionFilterExpression;
        private System.Windows.Forms.Button btnClearQueueFilterExpression;
        private System.Windows.Forms.Button btnClearTopicFilterExpression;
        private System.Windows.Forms.Button btnClearSubscriptionFilterExpression;
        private System.Windows.Forms.Label lblConnectivityMode;
        private System.Windows.Forms.ComboBox cboConnectivityMode;
        private System.Windows.Forms.ComboBox cboTransportType;
        private System.Windows.Forms.Label lblTransportType;
    }
}