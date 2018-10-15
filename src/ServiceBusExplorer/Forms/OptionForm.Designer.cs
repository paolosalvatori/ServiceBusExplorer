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

using Microsoft.Azure.ServiceBusExplorer.Controls;

namespace Microsoft.Azure.ServiceBusExplorer.Forms
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
            Microsoft.Azure.ServiceBusExplorer.Controls.CheckBoxProperties checkBoxProperties1 = new Microsoft.Azure.ServiceBusExplorer.Controls.CheckBoxProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblLogFontSize = new System.Windows.Forms.Label();
            this.logNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblTreeViewFontSize = new System.Windows.Forms.Label();
            this.treeViewNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblRetryCount = new System.Windows.Forms.Label();
            this.retryCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblRetryTimeout = new System.Windows.Forms.Label();
            this.retryTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblTop = new System.Windows.Forms.Label();
            this.topNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblReceiveTimeout = new System.Windows.Forms.Label();
            this.receiveTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblServerTimeout = new System.Windows.Forms.Label();
            this.serverTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblPrefetchCount = new System.Windows.Forms.Label();
            this.saveMessageToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.prefetchCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.savePropertiesToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.lblSaveMessageOnExit = new System.Windows.Forms.Label();
            this.lblSavePropertiesOnExit = new System.Windows.Forms.Label();
            this.lblSenderThinkTime = new System.Windows.Forms.Label();
            this.senderThinkTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblReceiverThinkTime = new System.Windows.Forms.Label();
            this.receiverThinkTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblLabel = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.lblMessageText = new System.Windows.Forms.Label();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.lblMessageFile = new System.Windows.Forms.Label();
            this.txtMessageFile = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblMonitorRefreshInterval = new System.Windows.Forms.Label();
            this.monitorRefreshIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblConnectivityMode = new System.Windows.Forms.Label();
            this.cboConnectivityMode = new System.Windows.Forms.ComboBox();
            this.cboEncodingType = new System.Windows.Forms.ComboBox();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.showMessageCountCheckBox = new System.Windows.Forms.CheckBox();
            this.lblShowMessageCount = new System.Windows.Forms.Label();
            this.lblSelectedEntities = new System.Windows.Forms.Label();
            this.saveCheckpointsToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.lblSaveCheckpointsOnExit = new System.Windows.Forms.Label();
            this.lblUseAscii = new System.Windows.Forms.Label();
            this.useAsciiCheckBox = new System.Windows.Forms.CheckBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDefaultMessageBodyType = new System.Windows.Forms.ComboBox();
            this.LabelDefaultMessageBodyType = new System.Windows.Forms.Label();
            this.cboSelectedEntities = new Microsoft.Azure.ServiceBusExplorer.Controls.CheckBoxComboBox();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.cboConfigFile = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiveTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prefetchCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.senderThinkTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiverThinkTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRefreshIntervalNumericUpDown)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(577, 633);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 30);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnOk.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOk.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(683, 633);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnDefault.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDefault.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDefault.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefault.Location = new System.Drawing.Point(470, 633);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(4);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(96, 30);
            this.btnDefault.TabIndex = 3;
            this.btnDefault.Text = "&Default";
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
            this.btnSave.Location = new System.Drawing.Point(671, 12);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblLogFontSize
            // 
            this.lblLogFontSize.AutoSize = true;
            this.lblLogFontSize.Location = new System.Drawing.Point(17, 30);
            this.lblLogFontSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogFontSize.Name = "lblLogFontSize";
            this.lblLogFontSize.Size = new System.Drawing.Size(99, 17);
            this.lblLogFontSize.TabIndex = 3;
            this.lblLogFontSize.Text = "Log Font Size:";
            // 
            // logNumericUpDown
            // 
            this.logNumericUpDown.DecimalPlaces = 2;
            this.logNumericUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.logNumericUpDown.Location = new System.Drawing.Point(251, 25);
            this.logNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.logNumericUpDown.Name = "logNumericUpDown";
            this.logNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.logNumericUpDown.TabIndex = 4;
            this.logNumericUpDown.ValueChanged += new System.EventHandler(this.logNumericUpDown_ValueChanged);
            // 
            // lblTreeViewFontSize
            // 
            this.lblTreeViewFontSize.AutoSize = true;
            this.lblTreeViewFontSize.Location = new System.Drawing.Point(17, 69);
            this.lblTreeViewFontSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTreeViewFontSize.Name = "lblTreeViewFontSize";
            this.lblTreeViewFontSize.Size = new System.Drawing.Size(138, 17);
            this.lblTreeViewFontSize.TabIndex = 7;
            this.lblTreeViewFontSize.Text = "Tree View Font Size:";
            // 
            // treeViewNumericUpDown
            // 
            this.treeViewNumericUpDown.DecimalPlaces = 2;
            this.treeViewNumericUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.treeViewNumericUpDown.Location = new System.Drawing.Point(251, 64);
            this.treeViewNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.treeViewNumericUpDown.Name = "treeViewNumericUpDown";
            this.treeViewNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.treeViewNumericUpDown.TabIndex = 8;
            this.treeViewNumericUpDown.ValueChanged += new System.EventHandler(this.treeViewNumericUpDown_ValueChanged);
            // 
            // lblRetryCount
            // 
            this.lblRetryCount.AutoSize = true;
            this.lblRetryCount.Location = new System.Drawing.Point(17, 109);
            this.lblRetryCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRetryCount.Name = "lblRetryCount";
            this.lblRetryCount.Size = new System.Drawing.Size(87, 17);
            this.lblRetryCount.TabIndex = 11;
            this.lblRetryCount.Text = "Retry Count:";
            // 
            // retryCountNumericUpDown
            // 
            this.retryCountNumericUpDown.Location = new System.Drawing.Point(251, 104);
            this.retryCountNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.retryCountNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.retryCountNumericUpDown.Name = "retryCountNumericUpDown";
            this.retryCountNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.retryCountNumericUpDown.TabIndex = 12;
            this.retryCountNumericUpDown.ValueChanged += new System.EventHandler(this.retryCountNumericUpDown_ValueChanged);
            // 
            // lblRetryTimeout
            // 
            this.lblRetryTimeout.AutoSize = true;
            this.lblRetryTimeout.Location = new System.Drawing.Point(17, 148);
            this.lblRetryTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRetryTimeout.Name = "lblRetryTimeout";
            this.lblRetryTimeout.Size = new System.Drawing.Size(191, 17);
            this.lblRetryTimeout.TabIndex = 15;
            this.lblRetryTimeout.Text = "Retry Timeout (milliseconds):";
            // 
            // retryTimeoutNumericUpDown
            // 
            this.retryTimeoutNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.retryTimeoutNumericUpDown.Location = new System.Drawing.Point(251, 143);
            this.retryTimeoutNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.retryTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.retryTimeoutNumericUpDown.Name = "retryTimeoutNumericUpDown";
            this.retryTimeoutNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.retryTimeoutNumericUpDown.TabIndex = 16;
            this.retryTimeoutNumericUpDown.ValueChanged += new System.EventHandler(this.retryTimeoutNumericUpDown_ValueChanged);
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(379, 148);
            this.lblTop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(78, 17);
            this.lblTop.TabIndex = 17;
            this.lblTop.Text = "Top Count:";
            // 
            // topNumericUpDown
            // 
            this.topNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.topNumericUpDown.Location = new System.Drawing.Point(625, 143);
            this.topNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.topNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.topNumericUpDown.Name = "topNumericUpDown";
            this.topNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.topNumericUpDown.TabIndex = 18;
            this.topNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.topNumericUpDown.ValueChanged += new System.EventHandler(this.topNumericUpDown_ValueChanged);
            // 
            // lblReceiveTimeout
            // 
            this.lblReceiveTimeout.AutoSize = true;
            this.lblReceiveTimeout.Location = new System.Drawing.Point(379, 30);
            this.lblReceiveTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceiveTimeout.Name = "lblReceiveTimeout";
            this.lblReceiveTimeout.Size = new System.Drawing.Size(185, 17);
            this.lblReceiveTimeout.TabIndex = 5;
            this.lblReceiveTimeout.Text = "Receive Timeout (seconds):";
            // 
            // receiveTimeoutNumericUpDown
            // 
            this.receiveTimeoutNumericUpDown.Location = new System.Drawing.Point(625, 25);
            this.receiveTimeoutNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.receiveTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.receiveTimeoutNumericUpDown.Name = "receiveTimeoutNumericUpDown";
            this.receiveTimeoutNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.receiveTimeoutNumericUpDown.TabIndex = 6;
            this.receiveTimeoutNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.receiveTimeoutNumericUpDown.ValueChanged += new System.EventHandler(this.receiveTimeoutNumericUpDown_ValueChanged);
            // 
            // lblServerTimeout
            // 
            this.lblServerTimeout.AutoSize = true;
            this.lblServerTimeout.Location = new System.Drawing.Point(379, 69);
            this.lblServerTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerTimeout.Name = "lblServerTimeout";
            this.lblServerTimeout.Size = new System.Drawing.Size(176, 17);
            this.lblServerTimeout.TabIndex = 9;
            this.lblServerTimeout.Text = "Server Timeout (seconds):";
            // 
            // serverTimeoutNumericUpDown
            // 
            this.serverTimeoutNumericUpDown.Location = new System.Drawing.Point(625, 64);
            this.serverTimeoutNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.serverTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.serverTimeoutNumericUpDown.Name = "serverTimeoutNumericUpDown";
            this.serverTimeoutNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.serverTimeoutNumericUpDown.TabIndex = 10;
            this.serverTimeoutNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.serverTimeoutNumericUpDown.ValueChanged += new System.EventHandler(this.sessionTimeoutNumericUpDown_ValueChanged);
            // 
            // lblPrefetchCount
            // 
            this.lblPrefetchCount.AutoSize = true;
            this.lblPrefetchCount.Location = new System.Drawing.Point(379, 109);
            this.lblPrefetchCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrefetchCount.Name = "lblPrefetchCount";
            this.lblPrefetchCount.Size = new System.Drawing.Size(106, 17);
            this.lblPrefetchCount.TabIndex = 13;
            this.lblPrefetchCount.Text = "Prefetch Count:";
            // 
            // saveMessageToFileCheckBox
            // 
            this.saveMessageToFileCheckBox.AutoSize = true;
            this.saveMessageToFileCheckBox.Checked = true;
            this.saveMessageToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveMessageToFileCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveMessageToFileCheckBox.Location = new System.Drawing.Point(251, 345);
            this.saveMessageToFileCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.saveMessageToFileCheckBox.Name = "saveMessageToFileCheckBox";
            this.saveMessageToFileCheckBox.Size = new System.Drawing.Size(18, 17);
            this.saveMessageToFileCheckBox.TabIndex = 35;
            this.saveMessageToFileCheckBox.UseVisualStyleBackColor = true;
            this.saveMessageToFileCheckBox.CheckedChanged += new System.EventHandler(this.saveMessageToFileCheckBox_CheckedChanged);
            // 
            // prefetchCountNumericUpDown
            // 
            this.prefetchCountNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.prefetchCountNumericUpDown.Location = new System.Drawing.Point(625, 104);
            this.prefetchCountNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.prefetchCountNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.prefetchCountNumericUpDown.Name = "prefetchCountNumericUpDown";
            this.prefetchCountNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.prefetchCountNumericUpDown.TabIndex = 14;
            this.prefetchCountNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.prefetchCountNumericUpDown.ValueChanged += new System.EventHandler(this.prefetchCountNumericUpDown_ValueChanged);
            // 
            // savePropertiesToFileCheckBox
            // 
            this.savePropertiesToFileCheckBox.AutoSize = true;
            this.savePropertiesToFileCheckBox.Checked = true;
            this.savePropertiesToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.savePropertiesToFileCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.savePropertiesToFileCheckBox.Location = new System.Drawing.Point(710, 345);
            this.savePropertiesToFileCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.savePropertiesToFileCheckBox.Name = "savePropertiesToFileCheckBox";
            this.savePropertiesToFileCheckBox.Size = new System.Drawing.Size(18, 17);
            this.savePropertiesToFileCheckBox.TabIndex = 37;
            this.savePropertiesToFileCheckBox.UseVisualStyleBackColor = true;
            this.savePropertiesToFileCheckBox.CheckedChanged += new System.EventHandler(this.savePropertiesToFileCheckBox_CheckedChanged);
            // 
            // lblSaveMessageOnExit
            // 
            this.lblSaveMessageOnExit.AutoSize = true;
            this.lblSaveMessageOnExit.Location = new System.Drawing.Point(17, 345);
            this.lblSaveMessageOnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSaveMessageOnExit.Name = "lblSaveMessageOnExit";
            this.lblSaveMessageOnExit.Size = new System.Drawing.Size(229, 17);
            this.lblSaveMessageOnExit.TabIndex = 34;
            this.lblSaveMessageOnExit.Text = "Save Message Body to File on Exit:";
            // 
            // lblSavePropertiesOnExit
            // 
            this.lblSavePropertiesOnExit.AutoSize = true;
            this.lblSavePropertiesOnExit.Location = new System.Drawing.Point(379, 345);
            this.lblSavePropertiesOnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSavePropertiesOnExit.Name = "lblSavePropertiesOnExit";
            this.lblSavePropertiesOnExit.Size = new System.Drawing.Size(262, 17);
            this.lblSavePropertiesOnExit.TabIndex = 36;
            this.lblSavePropertiesOnExit.Text = "Save Message Properties to File on Exit:";
            // 
            // lblSenderThinkTime
            // 
            this.lblSenderThinkTime.AutoSize = true;
            this.lblSenderThinkTime.Location = new System.Drawing.Point(17, 187);
            this.lblSenderThinkTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderThinkTime.Name = "lblSenderThinkTime";
            this.lblSenderThinkTime.Size = new System.Drawing.Size(222, 17);
            this.lblSenderThinkTime.TabIndex = 19;
            this.lblSenderThinkTime.Text = "Sender Think Time (milliseconds):";
            // 
            // senderThinkTimeNumericUpDown
            // 
            this.senderThinkTimeNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.senderThinkTimeNumericUpDown.Location = new System.Drawing.Point(251, 182);
            this.senderThinkTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.senderThinkTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.senderThinkTimeNumericUpDown.Name = "senderThinkTimeNumericUpDown";
            this.senderThinkTimeNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.senderThinkTimeNumericUpDown.TabIndex = 20;
            this.senderThinkTimeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.senderThinkTimeNumericUpDown.ValueChanged += new System.EventHandler(this.senderThinkTimeNumericUpDown_ValueChanged);
            // 
            // lblReceiverThinkTime
            // 
            this.lblReceiverThinkTime.AutoSize = true;
            this.lblReceiverThinkTime.Location = new System.Drawing.Point(379, 187);
            this.lblReceiverThinkTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceiverThinkTime.Name = "lblReceiverThinkTime";
            this.lblReceiverThinkTime.Size = new System.Drawing.Size(232, 17);
            this.lblReceiverThinkTime.TabIndex = 21;
            this.lblReceiverThinkTime.Text = "Receiver Think Time (milliseconds):";
            // 
            // receiverThinkTimeNumericUpDown
            // 
            this.receiverThinkTimeNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.receiverThinkTimeNumericUpDown.Location = new System.Drawing.Point(625, 182);
            this.receiverThinkTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.receiverThinkTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.receiverThinkTimeNumericUpDown.Name = "receiverThinkTimeNumericUpDown";
            this.receiverThinkTimeNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.receiverThinkTimeNumericUpDown.TabIndex = 22;
            this.receiverThinkTimeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.receiverThinkTimeNumericUpDown.ValueChanged += new System.EventHandler(this.receiverThinkTimeNumericUpDown_ValueChanged);
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(17, 385);
            this.lblLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(47, 17);
            this.lblLabel.TabIndex = 38;
            this.lblLabel.Text = "Label:";
            // 
            // txtLabel
            // 
            this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabel.Location = new System.Drawing.Point(251, 382);
            this.txtLabel.Margin = new System.Windows.Forms.Padding(4);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(477, 22);
            this.txtLabel.TabIndex = 39;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            // 
            // lblMessageText
            // 
            this.lblMessageText.AutoSize = true;
            this.lblMessageText.Location = new System.Drawing.Point(17, 461);
            this.lblMessageText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessageText.Name = "lblMessageText";
            this.lblMessageText.Size = new System.Drawing.Size(100, 17);
            this.lblMessageText.TabIndex = 42;
            this.lblMessageText.Text = "Message Text:";
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.Location = new System.Drawing.Point(251, 456);
            this.txtMessageText.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.Size = new System.Drawing.Size(425, 22);
            this.txtMessageText.TabIndex = 43;
            this.txtMessageText.TextChanged += new System.EventHandler(this.txtMessageText_TextChanged);
            // 
            // lblMessageFile
            // 
            this.lblMessageFile.AutoSize = true;
            this.lblMessageFile.Location = new System.Drawing.Point(17, 423);
            this.lblMessageFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessageFile.Name = "lblMessageFile";
            this.lblMessageFile.Size = new System.Drawing.Size(102, 17);
            this.lblMessageFile.TabIndex = 40;
            this.lblMessageFile.Text = "Message Path:";
            // 
            // txtMessageFile
            // 
            this.txtMessageFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageFile.Location = new System.Drawing.Point(251, 419);
            this.txtMessageFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessageFile.Name = "txtMessageFile";
            this.txtMessageFile.Size = new System.Drawing.Size(477, 22);
            this.txtMessageFile.TabIndex = 41;
            this.txtMessageFile.TextChanged += new System.EventHandler(this.txtMessageFile_TextChanged);
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
            this.btnOpen.Location = new System.Drawing.Point(696, 461);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(32, 26);
            this.btnOpen.TabIndex = 44;
            this.btnOpen.Text = "...";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            this.btnOpen.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOpen.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // lblMonitorRefreshInterval
            // 
            this.lblMonitorRefreshInterval.AutoSize = true;
            this.lblMonitorRefreshInterval.Location = new System.Drawing.Point(17, 227);
            this.lblMonitorRefreshInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonitorRefreshInterval.Name = "lblMonitorRefreshInterval";
            this.lblMonitorRefreshInterval.Size = new System.Drawing.Size(230, 17);
            this.lblMonitorRefreshInterval.TabIndex = 23;
            this.lblMonitorRefreshInterval.Text = "Monitor Refresh Interval (seconds):";
            // 
            // monitorRefreshIntervalNumericUpDown
            // 
            this.monitorRefreshIntervalNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.monitorRefreshIntervalNumericUpDown.Location = new System.Drawing.Point(251, 222);
            this.monitorRefreshIntervalNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.monitorRefreshIntervalNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.monitorRefreshIntervalNumericUpDown.Name = "monitorRefreshIntervalNumericUpDown";
            this.monitorRefreshIntervalNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.monitorRefreshIntervalNumericUpDown.TabIndex = 24;
            this.monitorRefreshIntervalNumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.monitorRefreshIntervalNumericUpDown.ValueChanged += new System.EventHandler(this.monitorRefreshIntervalNumericUpDown_ValueChanged);
            // 
            // lblConnectivityMode
            // 
            this.lblConnectivityMode.AutoSize = true;
            this.lblConnectivityMode.Location = new System.Drawing.Point(379, 227);
            this.lblConnectivityMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnectivityMode.Name = "lblConnectivityMode";
            this.lblConnectivityMode.Size = new System.Drawing.Size(127, 17);
            this.lblConnectivityMode.TabIndex = 25;
            this.lblConnectivityMode.Text = "Connectivity Mode:";
            // 
            // cboConnectivityMode
            // 
            this.cboConnectivityMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnectivityMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboConnectivityMode.FormattingEnabled = true;
            this.cboConnectivityMode.Location = new System.Drawing.Point(625, 222);
            this.cboConnectivityMode.Margin = new System.Windows.Forms.Padding(4);
            this.cboConnectivityMode.Name = "cboConnectivityMode";
            this.cboConnectivityMode.Size = new System.Drawing.Size(105, 24);
            this.cboConnectivityMode.TabIndex = 26;
            this.cboConnectivityMode.SelectedIndexChanged += new System.EventHandler(this.cboConnectivityMode_SelectedIndexChanged);
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
            this.cboEncodingType.Location = new System.Drawing.Point(625, 261);
            this.cboEncodingType.Margin = new System.Windows.Forms.Padding(4);
            this.cboEncodingType.Name = "cboEncodingType";
            this.cboEncodingType.Size = new System.Drawing.Size(105, 24);
            this.cboEncodingType.TabIndex = 30;
            this.cboEncodingType.SelectedIndexChanged += new System.EventHandler(this.cboEncoding_SelectedIndexChanged);
            // 
            // lblEncoding
            // 
            this.lblEncoding.AutoSize = true;
            this.lblEncoding.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEncoding.Location = new System.Drawing.Point(379, 266);
            this.lblEncoding.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size(71, 17);
            this.lblEncoding.TabIndex = 29;
            this.lblEncoding.Text = "Encoding:";
            // 
            // showMessageCountCheckBox
            // 
            this.showMessageCountCheckBox.AutoSize = true;
            this.showMessageCountCheckBox.Checked = true;
            this.showMessageCountCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMessageCountCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.showMessageCountCheckBox.Location = new System.Drawing.Point(251, 306);
            this.showMessageCountCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.showMessageCountCheckBox.Name = "showMessageCountCheckBox";
            this.showMessageCountCheckBox.Size = new System.Drawing.Size(18, 17);
            this.showMessageCountCheckBox.TabIndex = 32;
            this.showMessageCountCheckBox.UseVisualStyleBackColor = true;
            this.showMessageCountCheckBox.CheckedChanged += new System.EventHandler(this.showMessageCountCheckBox_CheckedChanged);
            // 
            // lblShowMessageCount
            // 
            this.lblShowMessageCount.AutoSize = true;
            this.lblShowMessageCount.Location = new System.Drawing.Point(17, 289);
            this.lblShowMessageCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShowMessageCount.Name = "lblShowMessageCount";
            this.lblShowMessageCount.Size = new System.Drawing.Size(148, 17);
            this.lblShowMessageCount.TabIndex = 31;
            this.lblShowMessageCount.Text = "Show Message Count:";
            // 
            // lblSelectedEntities
            // 
            this.lblSelectedEntities.AutoSize = true;
            this.lblSelectedEntities.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSelectedEntities.Location = new System.Drawing.Point(17, 499);
            this.lblSelectedEntities.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedEntities.Name = "lblSelectedEntities";
            this.lblSelectedEntities.Size = new System.Drawing.Size(117, 17);
            this.lblSelectedEntities.TabIndex = 45;
            this.lblSelectedEntities.Text = "Selected Entities:";
            // 
            // saveCheckpointsToFileCheckBox
            // 
            this.saveCheckpointsToFileCheckBox.AutoSize = true;
            this.saveCheckpointsToFileCheckBox.Checked = true;
            this.saveCheckpointsToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveCheckpointsToFileCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveCheckpointsToFileCheckBox.Location = new System.Drawing.Point(710, 306);
            this.saveCheckpointsToFileCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.saveCheckpointsToFileCheckBox.Name = "saveCheckpointsToFileCheckBox";
            this.saveCheckpointsToFileCheckBox.Size = new System.Drawing.Size(18, 17);
            this.saveCheckpointsToFileCheckBox.TabIndex = 33;
            this.saveCheckpointsToFileCheckBox.UseVisualStyleBackColor = true;
            this.saveCheckpointsToFileCheckBox.CheckedChanged += new System.EventHandler(this.saveCheckpointsToFileCheckBox_CheckedChanged);
            // 
            // lblSaveCheckpointsOnExit
            // 
            this.lblSaveCheckpointsOnExit.AutoSize = true;
            this.lblSaveCheckpointsOnExit.Location = new System.Drawing.Point(379, 306);
            this.lblSaveCheckpointsOnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSaveCheckpointsOnExit.Name = "lblSaveCheckpointsOnExit";
            this.lblSaveCheckpointsOnExit.Size = new System.Drawing.Size(297, 17);
            this.lblSaveCheckpointsOnExit.TabIndex = 33;
            this.lblSaveCheckpointsOnExit.Text = "Save Event Hub Partition Checkpoints on Exit:";
            // 
            // lblUseAscii
            // 
            this.lblUseAscii.AutoSize = true;
            this.lblUseAscii.Location = new System.Drawing.Point(17, 266);
            this.lblUseAscii.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUseAscii.Name = "lblUseAscii";
            this.lblUseAscii.Size = new System.Drawing.Size(74, 17);
            this.lblUseAscii.TabIndex = 27;
            this.lblUseAscii.Text = "Use ASCII:";
            // 
            // useAsciiCheckBox
            // 
            this.useAsciiCheckBox.AutoSize = true;
            this.useAsciiCheckBox.Checked = true;
            this.useAsciiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useAsciiCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.useAsciiCheckBox.Location = new System.Drawing.Point(251, 266);
            this.useAsciiCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.useAsciiCheckBox.Name = "useAsciiCheckBox";
            this.useAsciiCheckBox.Size = new System.Drawing.Size(18, 17);
            this.useAsciiCheckBox.TabIndex = 28;
            this.useAsciiCheckBox.UseVisualStyleBackColor = true;
            this.useAsciiCheckBox.CheckedChanged += new System.EventHandler(this.useAscii_CheckedChanged);
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BackColor = System.Drawing.SystemColors.Window;
            this.mainPanel.Controls.Add(this.btnSave);
            this.mainPanel.Controls.Add(this.groupBox1);
            this.mainPanel.Controls.Add(this.btnOpenConfig);
            this.mainPanel.Controls.Add(this.cboConfigFile);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Location = new System.Drawing.Point(9, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(779, 625);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboDefaultMessageBodyType);
            this.groupBox1.Controls.Add(this.LabelDefaultMessageBodyType);
            this.groupBox1.Controls.Add(this.useAsciiCheckBox);
            this.groupBox1.Controls.Add(this.lblUseAscii);
            this.groupBox1.Controls.Add(this.lblSaveCheckpointsOnExit);
            this.groupBox1.Controls.Add(this.saveCheckpointsToFileCheckBox);
            this.groupBox1.Controls.Add(this.cboSelectedEntities);
            this.groupBox1.Controls.Add(this.lblSelectedEntities);
            this.groupBox1.Controls.Add(this.lblShowMessageCount);
            this.groupBox1.Controls.Add(this.showMessageCountCheckBox);
            this.groupBox1.Controls.Add(this.lblEncoding);
            this.groupBox1.Controls.Add(this.cboEncodingType);
            this.groupBox1.Controls.Add(this.cboConnectivityMode);
            this.groupBox1.Controls.Add(this.lblConnectivityMode);
            this.groupBox1.Controls.Add(this.monitorRefreshIntervalNumericUpDown);
            this.groupBox1.Controls.Add(this.lblMonitorRefreshInterval);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.txtMessageFile);
            this.groupBox1.Controls.Add(this.lblMessageFile);
            this.groupBox1.Controls.Add(this.txtMessageText);
            this.groupBox1.Controls.Add(this.lblMessageText);
            this.groupBox1.Controls.Add(this.txtLabel);
            this.groupBox1.Controls.Add(this.lblLabel);
            this.groupBox1.Controls.Add(this.receiverThinkTimeNumericUpDown);
            this.groupBox1.Controls.Add(this.lblReceiverThinkTime);
            this.groupBox1.Controls.Add(this.senderThinkTimeNumericUpDown);
            this.groupBox1.Controls.Add(this.lblSenderThinkTime);
            this.groupBox1.Controls.Add(this.lblSavePropertiesOnExit);
            this.groupBox1.Controls.Add(this.lblSaveMessageOnExit);
            this.groupBox1.Controls.Add(this.savePropertiesToFileCheckBox);
            this.groupBox1.Controls.Add(this.prefetchCountNumericUpDown);
            this.groupBox1.Controls.Add(this.saveMessageToFileCheckBox);
            this.groupBox1.Controls.Add(this.lblPrefetchCount);
            this.groupBox1.Controls.Add(this.serverTimeoutNumericUpDown);
            this.groupBox1.Controls.Add(this.lblServerTimeout);
            this.groupBox1.Controls.Add(this.receiveTimeoutNumericUpDown);
            this.groupBox1.Controls.Add(this.lblReceiveTimeout);
            this.groupBox1.Controls.Add(this.topNumericUpDown);
            this.groupBox1.Controls.Add(this.lblTop);
            this.groupBox1.Controls.Add(this.retryTimeoutNumericUpDown);
            this.groupBox1.Controls.Add(this.lblRetryTimeout);
            this.groupBox1.Controls.Add(this.retryCountNumericUpDown);
            this.groupBox1.Controls.Add(this.lblRetryCount);
            this.groupBox1.Controls.Add(this.treeViewNumericUpDown);
            this.groupBox1.Controls.Add(this.lblTreeViewFontSize);
            this.groupBox1.Controls.Add(this.logNumericUpDown);
            this.groupBox1.Controls.Add(this.lblLogFontSize);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 569);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // cboDefaultMessageBodyType
            // 
            this.cboDefaultMessageBodyType.BackColor = System.Drawing.SystemColors.Window;
            this.cboDefaultMessageBodyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDefaultMessageBodyType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDefaultMessageBodyType.FormattingEnabled = true;
            this.cboDefaultMessageBodyType.Items.AddRange(new object[] {
            "Stream",
            "String",
            "WCF"});
            this.cboDefaultMessageBodyType.Location = new System.Drawing.Point(251, 532);
            this.cboDefaultMessageBodyType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDefaultMessageBodyType.Name = "cboDefaultMessageBodyType";
            this.cboDefaultMessageBodyType.Size = new System.Drawing.Size(479, 24);
            this.cboDefaultMessageBodyType.TabIndex = 48;
            this.cboDefaultMessageBodyType.SelectedIndexChanged += new System.EventHandler(this.cboDefaultMessageBodyType_SelectedIndexChanged);
            // 
            // LabelDefaultMessageBodyType
            // 
            this.LabelDefaultMessageBodyType.AutoSize = true;
            this.LabelDefaultMessageBodyType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelDefaultMessageBodyType.Location = new System.Drawing.Point(15, 537);
            this.LabelDefaultMessageBodyType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelDefaultMessageBodyType.Name = "LabelDefaultMessageBodyType";
            this.LabelDefaultMessageBodyType.Size = new System.Drawing.Size(184, 17);
            this.LabelDefaultMessageBodyType.TabIndex = 47;
            this.LabelDefaultMessageBodyType.Text = "Default message body type:";
            // 
            // cboSelectedEntities
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboSelectedEntities.CheckBoxProperties = checkBoxProperties1;
            this.cboSelectedEntities.DisplayMemberSingleItem = "";
            this.cboSelectedEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectedEntities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSelectedEntities.FormattingEnabled = true;
            this.cboSelectedEntities.Location = new System.Drawing.Point(251, 493);
            this.cboSelectedEntities.Margin = new System.Windows.Forms.Padding(4);
            this.cboSelectedEntities.Name = "cboSelectedEntities";
            this.cboSelectedEntities.Size = new System.Drawing.Size(479, 24);
            this.cboSelectedEntities.TabIndex = 46;
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenConfig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenConfig.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenConfig.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnOpenConfig.Location = new System.Drawing.Point(568, 12);
            this.btnOpenConfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(96, 30);
            this.btnOpenConfig.TabIndex = 2;
            this.btnOpenConfig.Text = "O&pen";
            this.btnOpenConfig.UseVisualStyleBackColor = false;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // cboConfigFile
            // 
            this.cboConfigFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConfigFile.DropDownWidth = 156;
            this.cboConfigFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboConfigFile.FormattingEnabled = true;
            this.cboConfigFile.Location = new System.Drawing.Point(361, 12);
            this.cboConfigFile.Margin = new System.Windows.Forms.Padding(4);
            this.cboConfigFile.Name = "cboConfigFile";
            this.cboConfigFile.Size = new System.Drawing.Size(199, 24);
            this.cboConfigFile.TabIndex = 1;
            this.cboConfigFile.SelectedIndexChanged += new System.EventHandler(this.cboConfigFile_SelectedIndexChanged);
            this.cboConfigFile.SelectionChangeCommitted += new System.EventHandler(this.cboConfigFile_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configuration File for Settings and Connection Strings:";
            // 
            // OptionForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(801, 674);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OptionForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.logNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiveTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prefetchCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.senderThinkTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiverThinkTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRefreshIntervalNumericUpDown)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblLogFontSize;
        private System.Windows.Forms.NumericUpDown logNumericUpDown;
        private System.Windows.Forms.Label lblTreeViewFontSize;
        private System.Windows.Forms.NumericUpDown treeViewNumericUpDown;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.NumericUpDown retryCountNumericUpDown;
        private System.Windows.Forms.Label lblRetryTimeout;
        private System.Windows.Forms.NumericUpDown retryTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.NumericUpDown topNumericUpDown;
        private System.Windows.Forms.Label lblReceiveTimeout;
        private System.Windows.Forms.NumericUpDown receiveTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblServerTimeout;
        private System.Windows.Forms.NumericUpDown serverTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblPrefetchCount;
        private System.Windows.Forms.CheckBox saveMessageToFileCheckBox;
        private System.Windows.Forms.NumericUpDown prefetchCountNumericUpDown;
        private System.Windows.Forms.CheckBox savePropertiesToFileCheckBox;
        private System.Windows.Forms.Label lblSaveMessageOnExit;
        private System.Windows.Forms.Label lblSavePropertiesOnExit;
        private System.Windows.Forms.Label lblSenderThinkTime;
        private System.Windows.Forms.NumericUpDown senderThinkTimeNumericUpDown;
        private System.Windows.Forms.Label lblReceiverThinkTime;
        private System.Windows.Forms.NumericUpDown receiverThinkTimeNumericUpDown;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label lblMessageText;
        private System.Windows.Forms.TextBox txtMessageText;
        private System.Windows.Forms.Label lblMessageFile;
        private System.Windows.Forms.TextBox txtMessageFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblMonitorRefreshInterval;
        private System.Windows.Forms.NumericUpDown monitorRefreshIntervalNumericUpDown;
        private System.Windows.Forms.Label lblConnectivityMode;
        private System.Windows.Forms.ComboBox cboConnectivityMode;
        private System.Windows.Forms.ComboBox cboEncodingType;
        private System.Windows.Forms.Label lblEncoding;
        private System.Windows.Forms.CheckBox showMessageCountCheckBox;
        private System.Windows.Forms.Label lblShowMessageCount;
        private System.Windows.Forms.Label lblSelectedEntities;
        private CheckBoxComboBox cboSelectedEntities;
        private System.Windows.Forms.CheckBox saveCheckpointsToFileCheckBox;
        private System.Windows.Forms.Label lblSaveCheckpointsOnExit;
        private System.Windows.Forms.Label lblUseAscii;
        private System.Windows.Forms.CheckBox useAsciiCheckBox;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label LabelDefaultMessageBodyType;
        private System.Windows.Forms.ComboBox cboDefaultMessageBodyType;
        private System.Windows.Forms.ComboBox cboConfigFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
