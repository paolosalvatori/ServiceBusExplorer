#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
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
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.cboEncodingType = new System.Windows.Forms.ComboBox();
            this.cboConnectivityMode = new System.Windows.Forms.ComboBox();
            this.lblConnectivityMode = new System.Windows.Forms.Label();
            this.monitorRefreshIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblMonitorRefreshInterval = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtMessageFile = new System.Windows.Forms.TextBox();
            this.lblMessageFile = new System.Windows.Forms.Label();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.lblMessageText = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtSubscriptionId = new System.Windows.Forms.TextBox();
            this.lblSubscriptionId = new System.Windows.Forms.Label();
            this.txtManagementCertificateThumbprint = new System.Windows.Forms.TextBox();
            this.lblManagementCertificateThumbprint = new System.Windows.Forms.Label();
            this.receiverThinkTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblReceiverThinkTime = new System.Windows.Forms.Label();
            this.senderThinkTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblSenderThinkTime = new System.Windows.Forms.Label();
            this.lblSavePropertiesOnExit = new System.Windows.Forms.Label();
            this.lblSaveMessageOnExit = new System.Windows.Forms.Label();
            this.savePropertiesToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.prefetchCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveMessageToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.lblPrefetchCount = new System.Windows.Forms.Label();
            this.serverTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblServerTimeout = new System.Windows.Forms.Label();
            this.receiveTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblReceiveTimeout = new System.Windows.Forms.Label();
            this.topNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblTop = new System.Windows.Forms.Label();
            this.retryTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblRetryTimeout = new System.Windows.Forms.Label();
            this.retryCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblRetryCount = new System.Windows.Forms.Label();
            this.treeViewNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblTreeViewFontSize = new System.Windows.Forms.Label();
            this.logNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblLogFontSize = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblShowMessageCount = new System.Windows.Forms.Label();
            this.showMessageCountCheckBox = new System.Windows.Forms.CheckBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRefreshIntervalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiverThinkTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.senderThinkTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prefetchCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiveTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logNumericUpDown)).BeginInit();
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
            this.btnOk.Location = new System.Drawing.Point(392, 488);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 24);
            this.btnOk.TabIndex = 2;
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
            this.btnCancel.Location = new System.Drawing.Point(472, 488);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BackColor = System.Drawing.SystemColors.Window;
            this.mainPanel.Controls.Add(this.lblEncoding);
            this.mainPanel.Controls.Add(this.cboEncodingType);
            this.mainPanel.Controls.Add(this.cboConnectivityMode);
            this.mainPanel.Controls.Add(this.lblConnectivityMode);
            this.mainPanel.Controls.Add(this.monitorRefreshIntervalNumericUpDown);
            this.mainPanel.Controls.Add(this.lblMonitorRefreshInterval);
            this.mainPanel.Controls.Add(this.btnOpen);
            this.mainPanel.Controls.Add(this.txtMessageFile);
            this.mainPanel.Controls.Add(this.lblMessageFile);
            this.mainPanel.Controls.Add(this.txtMessageText);
            this.mainPanel.Controls.Add(this.lblMessageText);
            this.mainPanel.Controls.Add(this.txtLabel);
            this.mainPanel.Controls.Add(this.lblLabel);
            this.mainPanel.Controls.Add(this.txtSubscriptionId);
            this.mainPanel.Controls.Add(this.lblSubscriptionId);
            this.mainPanel.Controls.Add(this.txtManagementCertificateThumbprint);
            this.mainPanel.Controls.Add(this.lblManagementCertificateThumbprint);
            this.mainPanel.Controls.Add(this.receiverThinkTimeNumericUpDown);
            this.mainPanel.Controls.Add(this.lblReceiverThinkTime);
            this.mainPanel.Controls.Add(this.senderThinkTimeNumericUpDown);
            this.mainPanel.Controls.Add(this.lblSenderThinkTime);
            this.mainPanel.Controls.Add(this.lblSavePropertiesOnExit);
            this.mainPanel.Controls.Add(this.lblSaveMessageOnExit);
            this.mainPanel.Controls.Add(this.savePropertiesToFileCheckBox);
            this.mainPanel.Controls.Add(this.prefetchCountNumericUpDown);
            this.mainPanel.Controls.Add(this.saveMessageToFileCheckBox);
            this.mainPanel.Controls.Add(this.lblPrefetchCount);
            this.mainPanel.Controls.Add(this.serverTimeoutNumericUpDown);
            this.mainPanel.Controls.Add(this.lblServerTimeout);
            this.mainPanel.Controls.Add(this.receiveTimeoutNumericUpDown);
            this.mainPanel.Controls.Add(this.lblReceiveTimeout);
            this.mainPanel.Controls.Add(this.topNumericUpDown);
            this.mainPanel.Controls.Add(this.lblTop);
            this.mainPanel.Controls.Add(this.retryTimeoutNumericUpDown);
            this.mainPanel.Controls.Add(this.lblRetryTimeout);
            this.mainPanel.Controls.Add(this.retryCountNumericUpDown);
            this.mainPanel.Controls.Add(this.lblRetryCount);
            this.mainPanel.Controls.Add(this.treeViewNumericUpDown);
            this.mainPanel.Controls.Add(this.lblTreeViewFontSize);
            this.mainPanel.Controls.Add(this.logNumericUpDown);
            this.mainPanel.Controls.Add(this.lblLogFontSize);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(560, 473);
            this.mainPanel.TabIndex = 33;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // lblEncoding
            // 
            this.lblEncoding.AutoSize = true;
            this.lblEncoding.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEncoding.Location = new System.Drawing.Point(8, 212);
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size(55, 13);
            this.lblEncoding.TabIndex = 81;
            this.lblEncoding.Text = "Encoding:";
            // 
            // cboEncodingType
            // 
            this.cboEncodingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEncodingType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEncodingType.FormattingEnabled = true;
            this.cboEncodingType.Items.AddRange(new object[] {
            "ASCII",
            "UTF7",
            "UTF8",
            "UTF32",
            "Unicode"});
            this.cboEncodingType.Location = new System.Drawing.Point(184, 208);
            this.cboEncodingType.Name = "cboEncodingType";
            this.cboEncodingType.Size = new System.Drawing.Size(360, 21);
            this.cboEncodingType.TabIndex = 80;
            this.cboEncodingType.SelectedIndexChanged += new System.EventHandler(this.cboEncoding_SelectedIndexChanged);
            // 
            // cboConnectivityMode
            // 
            this.cboConnectivityMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnectivityMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboConnectivityMode.FormattingEnabled = true;
            this.cboConnectivityMode.Location = new System.Drawing.Point(464, 176);
            this.cboConnectivityMode.Name = "cboConnectivityMode";
            this.cboConnectivityMode.Size = new System.Drawing.Size(80, 21);
            this.cboConnectivityMode.TabIndex = 68;
            this.cboConnectivityMode.SelectedIndexChanged += new System.EventHandler(this.cboConnectivityMode_SelectedIndexChanged);
            // 
            // lblConnectivityMode
            // 
            this.lblConnectivityMode.AutoSize = true;
            this.lblConnectivityMode.Location = new System.Drawing.Point(280, 180);
            this.lblConnectivityMode.Name = "lblConnectivityMode";
            this.lblConnectivityMode.Size = new System.Drawing.Size(98, 13);
            this.lblConnectivityMode.TabIndex = 66;
            this.lblConnectivityMode.Text = "Connectivity Mode:";
            // 
            // monitorRefreshIntervalNumericUpDown
            // 
            this.monitorRefreshIntervalNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.monitorRefreshIntervalNumericUpDown.Location = new System.Drawing.Point(184, 176);
            this.monitorRefreshIntervalNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.monitorRefreshIntervalNumericUpDown.Name = "monitorRefreshIntervalNumericUpDown";
            this.monitorRefreshIntervalNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.monitorRefreshIntervalNumericUpDown.TabIndex = 64;
            this.monitorRefreshIntervalNumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.monitorRefreshIntervalNumericUpDown.ValueChanged += new System.EventHandler(this.monitorRefreshIntervalNumericUpDown_ValueChanged);
            // 
            // lblMonitorRefreshInterval
            // 
            this.lblMonitorRefreshInterval.AutoSize = true;
            this.lblMonitorRefreshInterval.Location = new System.Drawing.Point(8, 180);
            this.lblMonitorRefreshInterval.Name = "lblMonitorRefreshInterval";
            this.lblMonitorRefreshInterval.Size = new System.Drawing.Size(172, 13);
            this.lblMonitorRefreshInterval.TabIndex = 65;
            this.lblMonitorRefreshInterval.Text = "Monitor Refresh Interval (seconds):";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpen.Location = new System.Drawing.Point(520, 432);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(24, 21);
            this.btnOpen.TabIndex = 17;
            this.btnOpen.Text = "...";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            this.btnOpen.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOpen.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // txtMessageFile
            // 
            this.txtMessageFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageFile.Location = new System.Drawing.Point(184, 400);
            this.txtMessageFile.Name = "txtMessageFile";
            this.txtMessageFile.Size = new System.Drawing.Size(360, 20);
            this.txtMessageFile.TabIndex = 15;
            this.txtMessageFile.TextChanged += new System.EventHandler(this.txtMessageFile_TextChanged);
            // 
            // lblMessageFile
            // 
            this.lblMessageFile.AutoSize = true;
            this.lblMessageFile.Location = new System.Drawing.Point(8, 404);
            this.lblMessageFile.Name = "lblMessageFile";
            this.lblMessageFile.Size = new System.Drawing.Size(78, 13);
            this.lblMessageFile.TabIndex = 63;
            this.lblMessageFile.Text = "Message Path:";
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.Location = new System.Drawing.Point(184, 432);
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.Size = new System.Drawing.Size(328, 20);
            this.txtMessageText.TabIndex = 16;
            this.txtMessageText.TextChanged += new System.EventHandler(this.txtMessageText_TextChanged);
            // 
            // lblMessageText
            // 
            this.lblMessageText.AutoSize = true;
            this.lblMessageText.Location = new System.Drawing.Point(8, 436);
            this.lblMessageText.Name = "lblMessageText";
            this.lblMessageText.Size = new System.Drawing.Size(77, 13);
            this.lblMessageText.TabIndex = 61;
            this.lblMessageText.Text = "Message Text:";
            // 
            // txtLabel
            // 
            this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabel.Location = new System.Drawing.Point(184, 368);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(360, 20);
            this.txtLabel.TabIndex = 14;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(8, 372);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(36, 13);
            this.lblLabel.TabIndex = 59;
            this.lblLabel.Text = "Label:";
            // 
            // txtSubscriptionId
            // 
            this.txtSubscriptionId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubscriptionId.Location = new System.Drawing.Point(184, 304);
            this.txtSubscriptionId.Name = "txtSubscriptionId";
            this.txtSubscriptionId.Size = new System.Drawing.Size(360, 20);
            this.txtSubscriptionId.TabIndex = 12;
            this.txtSubscriptionId.TextChanged += new System.EventHandler(this.txtSubscriptionId_TextChanged);
            // 
            // lblSubscriptionId
            // 
            this.lblSubscriptionId.AutoSize = true;
            this.lblSubscriptionId.Location = new System.Drawing.Point(8, 308);
            this.lblSubscriptionId.Name = "lblSubscriptionId";
            this.lblSubscriptionId.Size = new System.Drawing.Size(80, 13);
            this.lblSubscriptionId.TabIndex = 57;
            this.lblSubscriptionId.Text = "Subscription Id:";
            // 
            // txtManagementCertificateThumbprint
            // 
            this.txtManagementCertificateThumbprint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtManagementCertificateThumbprint.Location = new System.Drawing.Point(184, 336);
            this.txtManagementCertificateThumbprint.Name = "txtManagementCertificateThumbprint";
            this.txtManagementCertificateThumbprint.Size = new System.Drawing.Size(360, 20);
            this.txtManagementCertificateThumbprint.TabIndex = 13;
            this.txtManagementCertificateThumbprint.TextChanged += new System.EventHandler(this.txtManagementCertificateThumbprint_TextChanged);
            // 
            // lblManagementCertificateThumbprint
            // 
            this.lblManagementCertificateThumbprint.AutoSize = true;
            this.lblManagementCertificateThumbprint.Location = new System.Drawing.Point(8, 340);
            this.lblManagementCertificateThumbprint.Name = "lblManagementCertificateThumbprint";
            this.lblManagementCertificateThumbprint.Size = new System.Drawing.Size(113, 13);
            this.lblManagementCertificateThumbprint.TabIndex = 55;
            this.lblManagementCertificateThumbprint.Text = "Certificate Thumbprint:";
            // 
            // receiverThinkTimeNumericUpDown
            // 
            this.receiverThinkTimeNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.receiverThinkTimeNumericUpDown.Location = new System.Drawing.Point(464, 144);
            this.receiverThinkTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.receiverThinkTimeNumericUpDown.Name = "receiverThinkTimeNumericUpDown";
            this.receiverThinkTimeNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.receiverThinkTimeNumericUpDown.TabIndex = 9;
            this.receiverThinkTimeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.receiverThinkTimeNumericUpDown.ValueChanged += new System.EventHandler(this.receiverThinkTimeNumericUpDown_ValueChanged);
            // 
            // lblReceiverThinkTime
            // 
            this.lblReceiverThinkTime.AutoSize = true;
            this.lblReceiverThinkTime.Location = new System.Drawing.Point(280, 148);
            this.lblReceiverThinkTime.Name = "lblReceiverThinkTime";
            this.lblReceiverThinkTime.Size = new System.Drawing.Size(174, 13);
            this.lblReceiverThinkTime.TabIndex = 54;
            this.lblReceiverThinkTime.Text = "Receiver Think Time (milliseconds):";
            // 
            // senderThinkTimeNumericUpDown
            // 
            this.senderThinkTimeNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.senderThinkTimeNumericUpDown.Location = new System.Drawing.Point(184, 144);
            this.senderThinkTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.senderThinkTimeNumericUpDown.Name = "senderThinkTimeNumericUpDown";
            this.senderThinkTimeNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.senderThinkTimeNumericUpDown.TabIndex = 8;
            this.senderThinkTimeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.senderThinkTimeNumericUpDown.ValueChanged += new System.EventHandler(this.senderThinkTimeNumericUpDown_ValueChanged);
            // 
            // lblSenderThinkTime
            // 
            this.lblSenderThinkTime.AutoSize = true;
            this.lblSenderThinkTime.Location = new System.Drawing.Point(8, 148);
            this.lblSenderThinkTime.Name = "lblSenderThinkTime";
            this.lblSenderThinkTime.Size = new System.Drawing.Size(165, 13);
            this.lblSenderThinkTime.TabIndex = 52;
            this.lblSenderThinkTime.Text = "Sender Think Time (milliseconds):";
            // 
            // lblSavePropertiesOnExit
            // 
            this.lblSavePropertiesOnExit.AutoSize = true;
            this.lblSavePropertiesOnExit.Location = new System.Drawing.Point(352, 276);
            this.lblSavePropertiesOnExit.Name = "lblSavePropertiesOnExit";
            this.lblSavePropertiesOnExit.Size = new System.Drawing.Size(151, 13);
            this.lblSavePropertiesOnExit.TabIndex = 11;
            this.lblSavePropertiesOnExit.Text = "Save Properties to File on Exit:";
            // 
            // lblSaveMessageOnExit
            // 
            this.lblSaveMessageOnExit.AutoSize = true;
            this.lblSaveMessageOnExit.Location = new System.Drawing.Point(8, 276);
            this.lblSaveMessageOnExit.Name = "lblSaveMessageOnExit";
            this.lblSaveMessageOnExit.Size = new System.Drawing.Size(147, 13);
            this.lblSaveMessageOnExit.TabIndex = 10;
            this.lblSaveMessageOnExit.Text = "Save Message to File on Exit:";
            // 
            // savePropertiesToFileCheckBox
            // 
            this.savePropertiesToFileCheckBox.AutoSize = true;
            this.savePropertiesToFileCheckBox.Checked = true;
            this.savePropertiesToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.savePropertiesToFileCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.savePropertiesToFileCheckBox.Location = new System.Drawing.Point(528, 276);
            this.savePropertiesToFileCheckBox.Name = "savePropertiesToFileCheckBox";
            this.savePropertiesToFileCheckBox.Size = new System.Drawing.Size(15, 14);
            this.savePropertiesToFileCheckBox.TabIndex = 36;
            this.savePropertiesToFileCheckBox.UseVisualStyleBackColor = true;
            this.savePropertiesToFileCheckBox.CheckedChanged += new System.EventHandler(this.savePropertiesToFileCheckBox_CheckedChanged);
            // 
            // prefetchCountNumericUpDown
            // 
            this.prefetchCountNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.prefetchCountNumericUpDown.Location = new System.Drawing.Point(464, 80);
            this.prefetchCountNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.prefetchCountNumericUpDown.Name = "prefetchCountNumericUpDown";
            this.prefetchCountNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.prefetchCountNumericUpDown.TabIndex = 5;
            this.prefetchCountNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.prefetchCountNumericUpDown.ValueChanged += new System.EventHandler(this.prefetchCountNumericUpDown_ValueChanged);
            // 
            // saveMessageToFileCheckBox
            // 
            this.saveMessageToFileCheckBox.AutoSize = true;
            this.saveMessageToFileCheckBox.Checked = true;
            this.saveMessageToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveMessageToFileCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveMessageToFileCheckBox.Location = new System.Drawing.Point(184, 276);
            this.saveMessageToFileCheckBox.Name = "saveMessageToFileCheckBox";
            this.saveMessageToFileCheckBox.Size = new System.Drawing.Size(15, 14);
            this.saveMessageToFileCheckBox.TabIndex = 35;
            this.saveMessageToFileCheckBox.UseVisualStyleBackColor = true;
            this.saveMessageToFileCheckBox.CheckedChanged += new System.EventHandler(this.saveMessageToFileCheckBox_CheckedChanged);
            // 
            // lblPrefetchCount
            // 
            this.lblPrefetchCount.AutoSize = true;
            this.lblPrefetchCount.Location = new System.Drawing.Point(280, 84);
            this.lblPrefetchCount.Name = "lblPrefetchCount";
            this.lblPrefetchCount.Size = new System.Drawing.Size(81, 13);
            this.lblPrefetchCount.TabIndex = 48;
            this.lblPrefetchCount.Text = "Prefetch Count:";
            // 
            // serverTimeoutNumericUpDown
            // 
            this.serverTimeoutNumericUpDown.Location = new System.Drawing.Point(464, 48);
            this.serverTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.serverTimeoutNumericUpDown.Name = "serverTimeoutNumericUpDown";
            this.serverTimeoutNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.serverTimeoutNumericUpDown.TabIndex = 3;
            this.serverTimeoutNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.serverTimeoutNumericUpDown.ValueChanged += new System.EventHandler(this.sessionTimeoutNumericUpDown_ValueChanged);
            // 
            // lblServerTimeout
            // 
            this.lblServerTimeout.AutoSize = true;
            this.lblServerTimeout.Location = new System.Drawing.Point(280, 52);
            this.lblServerTimeout.Name = "lblServerTimeout";
            this.lblServerTimeout.Size = new System.Drawing.Size(131, 13);
            this.lblServerTimeout.TabIndex = 46;
            this.lblServerTimeout.Text = "Server Timeout (seconds):";
            // 
            // receiveTimeoutNumericUpDown
            // 
            this.receiveTimeoutNumericUpDown.Location = new System.Drawing.Point(464, 16);
            this.receiveTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.receiveTimeoutNumericUpDown.Name = "receiveTimeoutNumericUpDown";
            this.receiveTimeoutNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.receiveTimeoutNumericUpDown.TabIndex = 1;
            this.receiveTimeoutNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.receiveTimeoutNumericUpDown.ValueChanged += new System.EventHandler(this.receiveTimeoutNumericUpDown_ValueChanged);
            // 
            // lblReceiveTimeout
            // 
            this.lblReceiveTimeout.AutoSize = true;
            this.lblReceiveTimeout.Location = new System.Drawing.Point(280, 20);
            this.lblReceiveTimeout.Name = "lblReceiveTimeout";
            this.lblReceiveTimeout.Size = new System.Drawing.Size(140, 13);
            this.lblReceiveTimeout.TabIndex = 44;
            this.lblReceiveTimeout.Text = "Receive Timeout (seconds):";
            // 
            // topNumericUpDown
            // 
            this.topNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.topNumericUpDown.Location = new System.Drawing.Point(464, 112);
            this.topNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.topNumericUpDown.Name = "topNumericUpDown";
            this.topNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.topNumericUpDown.TabIndex = 7;
            this.topNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.topNumericUpDown.ValueChanged += new System.EventHandler(this.topNumericUpDown_ValueChanged);
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(280, 116);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(60, 13);
            this.lblTop.TabIndex = 42;
            this.lblTop.Text = "Top Count:";
            // 
            // retryTimeoutNumericUpDown
            // 
            this.retryTimeoutNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.retryTimeoutNumericUpDown.Location = new System.Drawing.Point(184, 112);
            this.retryTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.retryTimeoutNumericUpDown.Name = "retryTimeoutNumericUpDown";
            this.retryTimeoutNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.retryTimeoutNumericUpDown.TabIndex = 6;
            this.retryTimeoutNumericUpDown.ValueChanged += new System.EventHandler(this.retryTimeoutNumericUpDown_ValueChanged);
            // 
            // lblRetryTimeout
            // 
            this.lblRetryTimeout.AutoSize = true;
            this.lblRetryTimeout.Location = new System.Drawing.Point(8, 116);
            this.lblRetryTimeout.Name = "lblRetryTimeout";
            this.lblRetryTimeout.Size = new System.Drawing.Size(141, 13);
            this.lblRetryTimeout.TabIndex = 40;
            this.lblRetryTimeout.Text = "Retry Timeout (milliseconds):";
            // 
            // retryCountNumericUpDown
            // 
            this.retryCountNumericUpDown.Location = new System.Drawing.Point(184, 80);
            this.retryCountNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.retryCountNumericUpDown.Name = "retryCountNumericUpDown";
            this.retryCountNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.retryCountNumericUpDown.TabIndex = 4;
            this.retryCountNumericUpDown.ValueChanged += new System.EventHandler(this.retryCountNumericUpDown_ValueChanged);
            // 
            // lblRetryCount
            // 
            this.lblRetryCount.AutoSize = true;
            this.lblRetryCount.Location = new System.Drawing.Point(8, 84);
            this.lblRetryCount.Name = "lblRetryCount";
            this.lblRetryCount.Size = new System.Drawing.Size(66, 13);
            this.lblRetryCount.TabIndex = 38;
            this.lblRetryCount.Text = "Retry Count:";
            // 
            // treeViewNumericUpDown
            // 
            this.treeViewNumericUpDown.DecimalPlaces = 2;
            this.treeViewNumericUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.treeViewNumericUpDown.Location = new System.Drawing.Point(184, 48);
            this.treeViewNumericUpDown.Name = "treeViewNumericUpDown";
            this.treeViewNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.treeViewNumericUpDown.TabIndex = 2;
            this.treeViewNumericUpDown.ValueChanged += new System.EventHandler(this.treeViewNumericUpDown_ValueChanged);
            // 
            // lblTreeViewFontSize
            // 
            this.lblTreeViewFontSize.AutoSize = true;
            this.lblTreeViewFontSize.Location = new System.Drawing.Point(8, 52);
            this.lblTreeViewFontSize.Name = "lblTreeViewFontSize";
            this.lblTreeViewFontSize.Size = new System.Drawing.Size(105, 13);
            this.lblTreeViewFontSize.TabIndex = 36;
            this.lblTreeViewFontSize.Text = "Tree View Font Size:";
            // 
            // logNumericUpDown
            // 
            this.logNumericUpDown.DecimalPlaces = 2;
            this.logNumericUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.logNumericUpDown.Location = new System.Drawing.Point(184, 16);
            this.logNumericUpDown.Name = "logNumericUpDown";
            this.logNumericUpDown.Size = new System.Drawing.Size(80, 20);
            this.logNumericUpDown.TabIndex = 0;
            this.logNumericUpDown.ValueChanged += new System.EventHandler(this.logNumericUpDown_ValueChanged);
            // 
            // lblLogFontSize
            // 
            this.lblLogFontSize.AutoSize = true;
            this.lblLogFontSize.Location = new System.Drawing.Point(8, 20);
            this.lblLogFontSize.Name = "lblLogFontSize";
            this.lblLogFontSize.Size = new System.Drawing.Size(75, 13);
            this.lblLogFontSize.TabIndex = 34;
            this.lblLogFontSize.Text = "Log Font Size:";
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnDefault.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDefault.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDefault.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefault.Location = new System.Drawing.Point(312, 488);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(72, 24);
            this.btnDefault.TabIndex = 1;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = false;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            this.btnDefault.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnDefault.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(232, 488);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 24);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblShowMessageCount
            // 
            this.lblShowMessageCount.AutoSize = true;
            this.lblShowMessageCount.Location = new System.Drawing.Point(8, 244);
            this.lblShowMessageCount.Name = "lblShowMessageCount";
            this.lblShowMessageCount.Size = new System.Drawing.Size(114, 13);
            this.lblShowMessageCount.TabIndex = 84;
            this.lblShowMessageCount.Text = "Show Message Count:";
            // 
            // showMessageCountCheckBox
            // 
            this.showMessageCountCheckBox.AutoSize = true;
            this.showMessageCountCheckBox.Checked = true;
            this.showMessageCountCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMessageCountCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.showMessageCountCheckBox.Location = new System.Drawing.Point(184, 244);
            this.showMessageCountCheckBox.Name = "showMessageCountCheckBox";
            this.showMessageCountCheckBox.Size = new System.Drawing.Size(15, 14);
            this.showMessageCountCheckBox.TabIndex = 85;
            this.showMessageCountCheckBox.UseVisualStyleBackColor = true;
            this.showMessageCountCheckBox.CheckedChanged += new System.EventHandler(this.showMessageCountCheckBox_CheckedChanged);
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(560, 521);
            this.Controls.Add(this.lblShowMessageCount);
            this.Controls.Add(this.showMessageCountCheckBox);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OptionForm_KeyPress);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRefreshIntervalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiverThinkTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.senderThinkTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prefetchCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiveTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label lblLogFontSize;
        private System.Windows.Forms.NumericUpDown logNumericUpDown;
        private System.Windows.Forms.NumericUpDown treeViewNumericUpDown;
        private System.Windows.Forms.Label lblTreeViewFontSize;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.NumericUpDown retryTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblRetryTimeout;
        private System.Windows.Forms.NumericUpDown retryCountNumericUpDown;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.NumericUpDown topNumericUpDown;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.NumericUpDown prefetchCountNumericUpDown;
        private System.Windows.Forms.Label lblPrefetchCount;
        private System.Windows.Forms.NumericUpDown serverTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblServerTimeout;
        private System.Windows.Forms.NumericUpDown receiveTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblReceiveTimeout;
        private System.Windows.Forms.Label lblSavePropertiesOnExit;
        private System.Windows.Forms.Label lblSaveMessageOnExit;
        private System.Windows.Forms.CheckBox savePropertiesToFileCheckBox;
        private System.Windows.Forms.CheckBox saveMessageToFileCheckBox;
        private System.Windows.Forms.NumericUpDown receiverThinkTimeNumericUpDown;
        private System.Windows.Forms.Label lblReceiverThinkTime;
        private System.Windows.Forms.NumericUpDown senderThinkTimeNumericUpDown;
        private System.Windows.Forms.Label lblSenderThinkTime;
        private System.Windows.Forms.TextBox txtManagementCertificateThumbprint;
        private System.Windows.Forms.Label lblManagementCertificateThumbprint;
        private System.Windows.Forms.TextBox txtSubscriptionId;
        private System.Windows.Forms.Label lblSubscriptionId;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtMessageText;
        private System.Windows.Forms.Label lblMessageText;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.TextBox txtMessageFile;
        private System.Windows.Forms.Label lblMessageFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.NumericUpDown monitorRefreshIntervalNumericUpDown;
        private System.Windows.Forms.Label lblMonitorRefreshInterval;
        private System.Windows.Forms.Label lblConnectivityMode;
        private System.Windows.Forms.ComboBox cboConnectivityMode;
        private System.Windows.Forms.Label lblEncoding;
        private System.Windows.Forms.ComboBox cboEncodingType;
        private System.Windows.Forms.Label lblShowMessageCount;
        private System.Windows.Forms.CheckBox showMessageCountCheckBox;
    }
}