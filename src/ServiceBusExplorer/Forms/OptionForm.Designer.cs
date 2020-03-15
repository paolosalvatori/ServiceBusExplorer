﻿#region Copyright
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
            ServiceBusExplorer.Controls.CheckBoxProperties checkBoxProperties1 = new ServiceBusExplorer.Controls.CheckBoxProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboConfigFile = new System.Windows.Forms.ComboBox();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.tabOptionsControl = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.lblSaveCheckpointsOnExit = new System.Windows.Forms.Label();
            this.saveCheckpointsToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.cboSelectedEntities = new ServiceBusExplorer.Controls.CheckBoxComboBox();
            this.lblSelectedEntities = new System.Windows.Forms.Label();
            this.monitorRefreshIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblMonitorRefreshInterval = new System.Windows.Forms.Label();
            this.useAsciiCheckBox = new System.Windows.Forms.CheckBox();
            this.lblUseAscii = new System.Windows.Forms.Label();
            this.lblShowMessageCount = new System.Windows.Forms.Label();
            this.showMessageCountCheckBox = new System.Windows.Forms.CheckBox();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.cboEncodingType = new System.Windows.Forms.ComboBox();
            this.serverTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblServerTimeout = new System.Windows.Forms.Label();
            this.treeViewNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblTreeViewFontSize = new System.Windows.Forms.Label();
            this.logNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblLogFontSize = new System.Windows.Forms.Label();
            this.tabPageReceiving = new System.Windows.Forms.TabPage();
            this.receiverThinkTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblReceiverThinkTime = new System.Windows.Forms.Label();
            this.prefetchCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblPrefetchCount = new System.Windows.Forms.Label();
            this.receiveTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblReceiveTimeout = new System.Windows.Forms.Label();
            this.topNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblTop = new System.Windows.Forms.Label();
            this.retryTimeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblRetryTimeout = new System.Windows.Forms.Label();
            this.retryCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblRetryCount = new System.Windows.Forms.Label();
            this.tabPageSending = new System.Windows.Forms.TabPage();
            this.saveMessageToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.lblMessageContentType = new System.Windows.Forms.Label();
            this.txtMessageContentType = new System.Windows.Forms.TextBox();
            this.cboDefaultMessageBodyType = new System.Windows.Forms.ComboBox();
            this.LabelDefaultMessageBodyType = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtMessageFile = new System.Windows.Forms.TextBox();
            this.lblMessageFile = new System.Windows.Forms.Label();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.lblMessageText = new System.Windows.Forms.Label();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.senderThinkTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblSenderThinkTime = new System.Windows.Forms.Label();
            this.lblSavePropertiesOnExit = new System.Windows.Forms.Label();
            this.lblSaveMessageOnExit = new System.Windows.Forms.Label();
            this.savePropertiesToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.tabPageConnectivity = new System.Windows.Forms.TabPage();
            this.useAmqpWebSocketsCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboConnectivityMode = new System.Windows.Forms.ComboBox();
            this.lblConnectivityMode = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.tabOptionsControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRefreshIntervalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logNumericUpDown)).BeginInit();
            this.tabPageReceiving.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.receiverThinkTimeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prefetchCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiveTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryTimeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryCountNumericUpDown)).BeginInit();
            this.tabPageSending.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.senderThinkTimeNumericUpDown)).BeginInit();
            this.tabPageConnectivity.SuspendLayout();
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
            this.btnOk.Location = new System.Drawing.Point(388, 670);
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
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(495, 670);
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
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Location = new System.Drawing.Point(708, 670);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(96, 30);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnReset.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(602, 670);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(9, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(755, 569);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(441, 273);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(107, 22);
            this.numericUpDown1.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 278);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 50;
            this.label2.Text = "Log Font Size:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown2.Location = new System.Drawing.Point(278, 31);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(107, 22);
            this.numericUpDown2.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 52;
            this.label3.Text = "Log Font Size:";
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
            // cboConfigFile
            // 
            this.cboConfigFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConfigFile.DropDownWidth = 156;
            this.cboConfigFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboConfigFile.FormattingEnabled = true;
            this.cboConfigFile.Location = new System.Drawing.Point(361, 12);
            this.cboConfigFile.Margin = new System.Windows.Forms.Padding(4);
            this.cboConfigFile.Name = "cboConfigFile";
            this.cboConfigFile.Size = new System.Drawing.Size(263, 24);
            this.cboConfigFile.TabIndex = 1;
            this.cboConfigFile.SelectionChangeCommitted += new System.EventHandler(this.cboConfigFile_SelectionChangeCommitted);
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenConfig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenConfig.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenConfig.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnOpenConfig.Location = new System.Drawing.Point(648, 11);
            this.btnOpenConfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(113, 30);
            this.btnOpenConfig.TabIndex = 2;
            this.btnOpenConfig.Text = "O&pen";
            this.btnOpenConfig.UseVisualStyleBackColor = false;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // tabOptionsControl
            // 
            this.tabOptionsControl.Controls.Add(this.tabPageGeneral);
            this.tabOptionsControl.Controls.Add(this.tabPageReceiving);
            this.tabOptionsControl.Controls.Add(this.tabPageSending);
            this.tabOptionsControl.Controls.Add(this.tabPageConnectivity);
            this.tabOptionsControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabOptionsControl.Location = new System.Drawing.Point(13, 48);
            this.tabOptionsControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabOptionsControl.Name = "tabOptionsControl";
            this.tabOptionsControl.SelectedIndex = 0;
            this.tabOptionsControl.Size = new System.Drawing.Size(767, 610);
            this.tabOptionsControl.TabIndex = 51;
            this.tabOptionsControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlOptions_DrawItem);
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageGeneral.Controls.Add(this.lblSaveCheckpointsOnExit);
            this.tabPageGeneral.Controls.Add(this.saveCheckpointsToFileCheckBox);
            this.tabPageGeneral.Controls.Add(this.cboSelectedEntities);
            this.tabPageGeneral.Controls.Add(this.lblSelectedEntities);
            this.tabPageGeneral.Controls.Add(this.monitorRefreshIntervalNumericUpDown);
            this.tabPageGeneral.Controls.Add(this.lblMonitorRefreshInterval);
            this.tabPageGeneral.Controls.Add(this.useAsciiCheckBox);
            this.tabPageGeneral.Controls.Add(this.lblUseAscii);
            this.tabPageGeneral.Controls.Add(this.lblShowMessageCount);
            this.tabPageGeneral.Controls.Add(this.showMessageCountCheckBox);
            this.tabPageGeneral.Controls.Add(this.lblEncoding);
            this.tabPageGeneral.Controls.Add(this.cboEncodingType);
            this.tabPageGeneral.Controls.Add(this.serverTimeoutNumericUpDown);
            this.tabPageGeneral.Controls.Add(this.lblServerTimeout);
            this.tabPageGeneral.Controls.Add(this.treeViewNumericUpDown);
            this.tabPageGeneral.Controls.Add(this.lblTreeViewFontSize);
            this.tabPageGeneral.Controls.Add(this.logNumericUpDown);
            this.tabPageGeneral.Controls.Add(this.lblLogFontSize);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabPageGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageGeneral.Size = new System.Drawing.Size(759, 581);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            // 
            // lblSaveCheckpointsOnExit
            // 
            this.lblSaveCheckpointsOnExit.AutoSize = true;
            this.lblSaveCheckpointsOnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSaveCheckpointsOnExit.Location = new System.Drawing.Point(18, 382);
            this.lblSaveCheckpointsOnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSaveCheckpointsOnExit.Name = "lblSaveCheckpointsOnExit";
            this.lblSaveCheckpointsOnExit.Size = new System.Drawing.Size(297, 17);
            this.lblSaveCheckpointsOnExit.TabIndex = 51;
            this.lblSaveCheckpointsOnExit.Text = "Save Event Hub Partition Checkpoints on Exit:";
            // 
            // saveCheckpointsToFileCheckBox
            // 
            this.saveCheckpointsToFileCheckBox.AutoSize = true;
            this.saveCheckpointsToFileCheckBox.Checked = true;
            this.saveCheckpointsToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveCheckpointsToFileCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveCheckpointsToFileCheckBox.Location = new System.Drawing.Point(344, 382);
            this.saveCheckpointsToFileCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.saveCheckpointsToFileCheckBox.Name = "saveCheckpointsToFileCheckBox";
            this.saveCheckpointsToFileCheckBox.Size = new System.Drawing.Size(18, 17);
            this.saveCheckpointsToFileCheckBox.TabIndex = 52;
            this.saveCheckpointsToFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // cboSelectedEntities
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboSelectedEntities.CheckBoxProperties = checkBoxProperties1;
            this.cboSelectedEntities.DisplayMemberSingleItem = "";
            this.cboSelectedEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSelectedEntities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSelectedEntities.FormattingEnabled = true;
            this.cboSelectedEntities.Location = new System.Drawing.Point(180, 428);
            this.cboSelectedEntities.Margin = new System.Windows.Forms.Padding(4);
            this.cboSelectedEntities.Name = "cboSelectedEntities";
            this.cboSelectedEntities.Size = new System.Drawing.Size(479, 24);
            this.cboSelectedEntities.TabIndex = 50;
            // 
            // lblSelectedEntities
            // 
            this.lblSelectedEntities.AutoSize = true;
            this.lblSelectedEntities.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSelectedEntities.Location = new System.Drawing.Point(18, 431);
            this.lblSelectedEntities.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedEntities.Name = "lblSelectedEntities";
            this.lblSelectedEntities.Size = new System.Drawing.Size(117, 17);
            this.lblSelectedEntities.TabIndex = 49;
            this.lblSelectedEntities.Text = "Selected Entities:";
            // 
            // monitorRefreshIntervalNumericUpDown
            // 
            this.monitorRefreshIntervalNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.monitorRefreshIntervalNumericUpDown.Location = new System.Drawing.Point(255, 150);
            this.monitorRefreshIntervalNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.monitorRefreshIntervalNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.monitorRefreshIntervalNumericUpDown.Name = "monitorRefreshIntervalNumericUpDown";
            this.monitorRefreshIntervalNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.monitorRefreshIntervalNumericUpDown.TabIndex = 46;
            this.monitorRefreshIntervalNumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblMonitorRefreshInterval
            // 
            this.lblMonitorRefreshInterval.AutoSize = true;
            this.lblMonitorRefreshInterval.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMonitorRefreshInterval.Location = new System.Drawing.Point(18, 155);
            this.lblMonitorRefreshInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonitorRefreshInterval.Name = "lblMonitorRefreshInterval";
            this.lblMonitorRefreshInterval.Size = new System.Drawing.Size(230, 17);
            this.lblMonitorRefreshInterval.TabIndex = 45;
            this.lblMonitorRefreshInterval.Text = "Monitor Refresh Interval (seconds):";
            // 
            // useAsciiCheckBox
            // 
            this.useAsciiCheckBox.AutoSize = true;
            this.useAsciiCheckBox.Checked = true;
            this.useAsciiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useAsciiCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.useAsciiCheckBox.Location = new System.Drawing.Point(344, 208);
            this.useAsciiCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.useAsciiCheckBox.Name = "useAsciiCheckBox";
            this.useAsciiCheckBox.Size = new System.Drawing.Size(18, 17);
            this.useAsciiCheckBox.TabIndex = 40;
            this.useAsciiCheckBox.UseVisualStyleBackColor = true;
            // 
            // lblUseAscii
            // 
            this.lblUseAscii.AutoSize = true;
            this.lblUseAscii.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUseAscii.Location = new System.Drawing.Point(18, 207);
            this.lblUseAscii.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUseAscii.Name = "lblUseAscii";
            this.lblUseAscii.Size = new System.Drawing.Size(74, 17);
            this.lblUseAscii.TabIndex = 39;
            this.lblUseAscii.Text = "Use ASCII:";
            // 
            // lblShowMessageCount
            // 
            this.lblShowMessageCount.AutoSize = true;
            this.lblShowMessageCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblShowMessageCount.Location = new System.Drawing.Point(18, 246);
            this.lblShowMessageCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShowMessageCount.Name = "lblShowMessageCount";
            this.lblShowMessageCount.Size = new System.Drawing.Size(148, 17);
            this.lblShowMessageCount.TabIndex = 43;
            this.lblShowMessageCount.Text = "Show Message Count:";
            // 
            // showMessageCountCheckBox
            // 
            this.showMessageCountCheckBox.AutoSize = true;
            this.showMessageCountCheckBox.Checked = true;
            this.showMessageCountCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showMessageCountCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.showMessageCountCheckBox.Location = new System.Drawing.Point(344, 248);
            this.showMessageCountCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.showMessageCountCheckBox.Name = "showMessageCountCheckBox";
            this.showMessageCountCheckBox.Size = new System.Drawing.Size(18, 17);
            this.showMessageCountCheckBox.TabIndex = 44;
            this.showMessageCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // lblEncoding
            // 
            this.lblEncoding.AutoSize = true;
            this.lblEncoding.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEncoding.Location = new System.Drawing.Point(18, 296);
            this.lblEncoding.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size(71, 17);
            this.lblEncoding.TabIndex = 41;
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
            this.cboEncodingType.Location = new System.Drawing.Point(257, 291);
            this.cboEncodingType.Margin = new System.Windows.Forms.Padding(4);
            this.cboEncodingType.Name = "cboEncodingType";
            this.cboEncodingType.Size = new System.Drawing.Size(105, 24);
            this.cboEncodingType.TabIndex = 42;
            // 
            // serverTimeoutNumericUpDown
            // 
            this.serverTimeoutNumericUpDown.Location = new System.Drawing.Point(255, 108);
            this.serverTimeoutNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.serverTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.serverTimeoutNumericUpDown.Name = "serverTimeoutNumericUpDown";
            this.serverTimeoutNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.serverTimeoutNumericUpDown.TabIndex = 38;
            this.serverTimeoutNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblServerTimeout
            // 
            this.lblServerTimeout.AutoSize = true;
            this.lblServerTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblServerTimeout.Location = new System.Drawing.Point(18, 110);
            this.lblServerTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerTimeout.Name = "lblServerTimeout";
            this.lblServerTimeout.Size = new System.Drawing.Size(176, 17);
            this.lblServerTimeout.TabIndex = 37;
            this.lblServerTimeout.Text = "Server Timeout (seconds):";
            // 
            // treeViewNumericUpDown
            // 
            this.treeViewNumericUpDown.DecimalPlaces = 2;
            this.treeViewNumericUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.treeViewNumericUpDown.Location = new System.Drawing.Point(255, 71);
            this.treeViewNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.treeViewNumericUpDown.Name = "treeViewNumericUpDown";
            this.treeViewNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.treeViewNumericUpDown.TabIndex = 36;
            // 
            // lblTreeViewFontSize
            // 
            this.lblTreeViewFontSize.AutoSize = true;
            this.lblTreeViewFontSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTreeViewFontSize.Location = new System.Drawing.Point(18, 76);
            this.lblTreeViewFontSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTreeViewFontSize.Name = "lblTreeViewFontSize";
            this.lblTreeViewFontSize.Size = new System.Drawing.Size(138, 17);
            this.lblTreeViewFontSize.TabIndex = 35;
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
            this.logNumericUpDown.Location = new System.Drawing.Point(255, 31);
            this.logNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.logNumericUpDown.Name = "logNumericUpDown";
            this.logNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.logNumericUpDown.TabIndex = 34;
            // 
            // lblLogFontSize
            // 
            this.lblLogFontSize.AutoSize = true;
            this.lblLogFontSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLogFontSize.Location = new System.Drawing.Point(18, 36);
            this.lblLogFontSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogFontSize.Name = "lblLogFontSize";
            this.lblLogFontSize.Size = new System.Drawing.Size(99, 17);
            this.lblLogFontSize.TabIndex = 33;
            this.lblLogFontSize.Text = "Log Font Size:";
            // 
            // tabPageReceiving
            // 
            this.tabPageReceiving.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageReceiving.Controls.Add(this.receiverThinkTimeNumericUpDown);
            this.tabPageReceiving.Controls.Add(this.lblReceiverThinkTime);
            this.tabPageReceiving.Controls.Add(this.prefetchCountNumericUpDown);
            this.tabPageReceiving.Controls.Add(this.lblPrefetchCount);
            this.tabPageReceiving.Controls.Add(this.receiveTimeoutNumericUpDown);
            this.tabPageReceiving.Controls.Add(this.lblReceiveTimeout);
            this.tabPageReceiving.Controls.Add(this.topNumericUpDown);
            this.tabPageReceiving.Controls.Add(this.lblTop);
            this.tabPageReceiving.Controls.Add(this.retryTimeoutNumericUpDown);
            this.tabPageReceiving.Controls.Add(this.lblRetryTimeout);
            this.tabPageReceiving.Controls.Add(this.retryCountNumericUpDown);
            this.tabPageReceiving.Controls.Add(this.lblRetryCount);
            this.tabPageReceiving.Location = new System.Drawing.Point(4, 25);
            this.tabPageReceiving.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageReceiving.Name = "tabPageReceiving";
            this.tabPageReceiving.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageReceiving.Size = new System.Drawing.Size(759, 581);
            this.tabPageReceiving.TabIndex = 1;
            this.tabPageReceiving.Text = "Receiving";
            // 
            // receiverThinkTimeNumericUpDown
            // 
            this.receiverThinkTimeNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.receiverThinkTimeNumericUpDown.Location = new System.Drawing.Point(268, 254);
            this.receiverThinkTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.receiverThinkTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.receiverThinkTimeNumericUpDown.Name = "receiverThinkTimeNumericUpDown";
            this.receiverThinkTimeNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.receiverThinkTimeNumericUpDown.TabIndex = 34;
            this.receiverThinkTimeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblReceiverThinkTime
            // 
            this.lblReceiverThinkTime.AutoSize = true;
            this.lblReceiverThinkTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReceiverThinkTime.Location = new System.Drawing.Point(22, 259);
            this.lblReceiverThinkTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceiverThinkTime.Name = "lblReceiverThinkTime";
            this.lblReceiverThinkTime.Size = new System.Drawing.Size(232, 17);
            this.lblReceiverThinkTime.TabIndex = 33;
            this.lblReceiverThinkTime.Text = "Receiver Think Time (milliseconds):";
            // 
            // prefetchCountNumericUpDown
            // 
            this.prefetchCountNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.prefetchCountNumericUpDown.Location = new System.Drawing.Point(268, 168);
            this.prefetchCountNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.prefetchCountNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.prefetchCountNumericUpDown.Name = "prefetchCountNumericUpDown";
            this.prefetchCountNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.prefetchCountNumericUpDown.TabIndex = 28;
            this.prefetchCountNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblPrefetchCount
            // 
            this.lblPrefetchCount.AutoSize = true;
            this.lblPrefetchCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrefetchCount.Location = new System.Drawing.Point(22, 173);
            this.lblPrefetchCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrefetchCount.Name = "lblPrefetchCount";
            this.lblPrefetchCount.Size = new System.Drawing.Size(106, 17);
            this.lblPrefetchCount.TabIndex = 27;
            this.lblPrefetchCount.Text = "Prefetch Count:";
            // 
            // receiveTimeoutNumericUpDown
            // 
            this.receiveTimeoutNumericUpDown.Location = new System.Drawing.Point(268, 39);
            this.receiveTimeoutNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.receiveTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.receiveTimeoutNumericUpDown.Name = "receiveTimeoutNumericUpDown";
            this.receiveTimeoutNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.receiveTimeoutNumericUpDown.TabIndex = 24;
            this.receiveTimeoutNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblReceiveTimeout
            // 
            this.lblReceiveTimeout.AutoSize = true;
            this.lblReceiveTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReceiveTimeout.Location = new System.Drawing.Point(22, 44);
            this.lblReceiveTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceiveTimeout.Name = "lblReceiveTimeout";
            this.lblReceiveTimeout.Size = new System.Drawing.Size(185, 17);
            this.lblReceiveTimeout.TabIndex = 23;
            this.lblReceiveTimeout.Text = "Receive Timeout (seconds):";
            // 
            // topNumericUpDown
            // 
            this.topNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.topNumericUpDown.Location = new System.Drawing.Point(268, 211);
            this.topNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.topNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.topNumericUpDown.Name = "topNumericUpDown";
            this.topNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.topNumericUpDown.TabIndex = 32;
            this.topNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTop.Location = new System.Drawing.Point(22, 216);
            this.lblTop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(78, 17);
            this.lblTop.TabIndex = 31;
            this.lblTop.Text = "Top Count:";
            // 
            // retryTimeoutNumericUpDown
            // 
            this.retryTimeoutNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.retryTimeoutNumericUpDown.Location = new System.Drawing.Point(268, 125);
            this.retryTimeoutNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.retryTimeoutNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.retryTimeoutNumericUpDown.Name = "retryTimeoutNumericUpDown";
            this.retryTimeoutNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.retryTimeoutNumericUpDown.TabIndex = 30;
            // 
            // lblRetryTimeout
            // 
            this.lblRetryTimeout.AutoSize = true;
            this.lblRetryTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRetryTimeout.Location = new System.Drawing.Point(22, 130);
            this.lblRetryTimeout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRetryTimeout.Name = "lblRetryTimeout";
            this.lblRetryTimeout.Size = new System.Drawing.Size(191, 17);
            this.lblRetryTimeout.TabIndex = 29;
            this.lblRetryTimeout.Text = "Retry Timeout (milliseconds):";
            // 
            // retryCountNumericUpDown
            // 
            this.retryCountNumericUpDown.Location = new System.Drawing.Point(268, 82);
            this.retryCountNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.retryCountNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.retryCountNumericUpDown.Name = "retryCountNumericUpDown";
            this.retryCountNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.retryCountNumericUpDown.TabIndex = 26;
            // 
            // lblRetryCount
            // 
            this.lblRetryCount.AutoSize = true;
            this.lblRetryCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRetryCount.Location = new System.Drawing.Point(22, 87);
            this.lblRetryCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRetryCount.Name = "lblRetryCount";
            this.lblRetryCount.Size = new System.Drawing.Size(87, 17);
            this.lblRetryCount.TabIndex = 25;
            this.lblRetryCount.Text = "Retry Count:";
            // 
            // tabPageSending
            // 
            this.tabPageSending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageSending.Controls.Add(this.saveMessageToFileCheckBox);
            this.tabPageSending.Controls.Add(this.lblMessageContentType);
            this.tabPageSending.Controls.Add(this.txtMessageContentType);
            this.tabPageSending.Controls.Add(this.cboDefaultMessageBodyType);
            this.tabPageSending.Controls.Add(this.LabelDefaultMessageBodyType);
            this.tabPageSending.Controls.Add(this.btnOpen);
            this.tabPageSending.Controls.Add(this.txtMessageFile);
            this.tabPageSending.Controls.Add(this.lblMessageFile);
            this.tabPageSending.Controls.Add(this.txtMessageText);
            this.tabPageSending.Controls.Add(this.lblMessageText);
            this.tabPageSending.Controls.Add(this.txtLabel);
            this.tabPageSending.Controls.Add(this.lblLabel);
            this.tabPageSending.Controls.Add(this.senderThinkTimeNumericUpDown);
            this.tabPageSending.Controls.Add(this.lblSenderThinkTime);
            this.tabPageSending.Controls.Add(this.lblSavePropertiesOnExit);
            this.tabPageSending.Controls.Add(this.lblSaveMessageOnExit);
            this.tabPageSending.Controls.Add(this.savePropertiesToFileCheckBox);
            this.tabPageSending.Location = new System.Drawing.Point(4, 25);
            this.tabPageSending.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageSending.Name = "tabPageSending";
            this.tabPageSending.Size = new System.Drawing.Size(759, 581);
            this.tabPageSending.TabIndex = 2;
            this.tabPageSending.Text = "Sending";
            // 
            // saveMessageToFileCheckBox
            // 
            this.saveMessageToFileCheckBox.AutoSize = true;
            this.saveMessageToFileCheckBox.Checked = true;
            this.saveMessageToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveMessageToFileCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveMessageToFileCheckBox.Location = new System.Drawing.Point(352, 89);
            this.saveMessageToFileCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.saveMessageToFileCheckBox.Name = "saveMessageToFileCheckBox";
            this.saveMessageToFileCheckBox.Size = new System.Drawing.Size(18, 17);
            this.saveMessageToFileCheckBox.TabIndex = 67;
            this.saveMessageToFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // lblMessageContentType
            // 
            this.lblMessageContentType.AutoSize = true;
            this.lblMessageContentType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageContentType.Location = new System.Drawing.Point(18, 297);
            this.lblMessageContentType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessageContentType.Name = "lblMessageContentType";
            this.lblMessageContentType.Size = new System.Drawing.Size(158, 17);
            this.lblMessageContentType.TabIndex = 63;
            this.lblMessageContentType.Text = "Message Content Type:";
            // 
            // txtMessageContentType
            // 
            this.txtMessageContentType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageContentType.Location = new System.Drawing.Point(213, 292);
            this.txtMessageContentType.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessageContentType.Name = "txtMessageContentType";
            this.txtMessageContentType.Size = new System.Drawing.Size(523, 22);
            this.txtMessageContentType.TabIndex = 64;
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
            this.cboDefaultMessageBodyType.Location = new System.Drawing.Point(213, 335);
            this.cboDefaultMessageBodyType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDefaultMessageBodyType.Name = "cboDefaultMessageBodyType";
            this.cboDefaultMessageBodyType.Size = new System.Drawing.Size(523, 24);
            this.cboDefaultMessageBodyType.TabIndex = 66;
            // 
            // LabelDefaultMessageBodyType
            // 
            this.LabelDefaultMessageBodyType.AutoSize = true;
            this.LabelDefaultMessageBodyType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelDefaultMessageBodyType.Location = new System.Drawing.Point(18, 340);
            this.LabelDefaultMessageBodyType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelDefaultMessageBodyType.Name = "LabelDefaultMessageBodyType";
            this.LabelDefaultMessageBodyType.Size = new System.Drawing.Size(184, 17);
            this.LabelDefaultMessageBodyType.TabIndex = 65;
            this.LabelDefaultMessageBodyType.Text = "Default message body type:";
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
            this.btnOpen.Location = new System.Drawing.Point(704, 253);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(32, 26);
            this.btnOpen.TabIndex = 62;
            this.btnOpen.Text = "...";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpen.UseVisualStyleBackColor = false;
            // 
            // txtMessageFile
            // 
            this.txtMessageFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageFile.Location = new System.Drawing.Point(213, 218);
            this.txtMessageFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessageFile.Name = "txtMessageFile";
            this.txtMessageFile.Size = new System.Drawing.Size(523, 22);
            this.txtMessageFile.TabIndex = 59;
            // 
            // lblMessageFile
            // 
            this.lblMessageFile.AutoSize = true;
            this.lblMessageFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageFile.Location = new System.Drawing.Point(18, 223);
            this.lblMessageFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessageFile.Name = "lblMessageFile";
            this.lblMessageFile.Size = new System.Drawing.Size(102, 17);
            this.lblMessageFile.TabIndex = 58;
            this.lblMessageFile.Text = "Message Path:";
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.Location = new System.Drawing.Point(213, 255);
            this.txtMessageText.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.Size = new System.Drawing.Size(474, 22);
            this.txtMessageText.TabIndex = 61;
            // 
            // lblMessageText
            // 
            this.lblMessageText.AutoSize = true;
            this.lblMessageText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageText.Location = new System.Drawing.Point(18, 260);
            this.lblMessageText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessageText.Name = "lblMessageText";
            this.lblMessageText.Size = new System.Drawing.Size(100, 17);
            this.lblMessageText.TabIndex = 60;
            this.lblMessageText.Text = "Message Text:";
            // 
            // txtLabel
            // 
            this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabel.Location = new System.Drawing.Point(213, 181);
            this.txtLabel.Margin = new System.Windows.Forms.Padding(4);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(523, 22);
            this.txtLabel.TabIndex = 57;
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLabel.Location = new System.Drawing.Point(18, 186);
            this.lblLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(47, 17);
            this.lblLabel.TabIndex = 56;
            this.lblLabel.Text = "Label:";
            // 
            // senderThinkTimeNumericUpDown
            // 
            this.senderThinkTimeNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.senderThinkTimeNumericUpDown.Location = new System.Drawing.Point(263, 43);
            this.senderThinkTimeNumericUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.senderThinkTimeNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.senderThinkTimeNumericUpDown.Name = "senderThinkTimeNumericUpDown";
            this.senderThinkTimeNumericUpDown.Size = new System.Drawing.Size(107, 22);
            this.senderThinkTimeNumericUpDown.TabIndex = 52;
            this.senderThinkTimeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblSenderThinkTime
            // 
            this.lblSenderThinkTime.AutoSize = true;
            this.lblSenderThinkTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSenderThinkTime.Location = new System.Drawing.Point(18, 48);
            this.lblSenderThinkTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderThinkTime.Name = "lblSenderThinkTime";
            this.lblSenderThinkTime.Size = new System.Drawing.Size(222, 17);
            this.lblSenderThinkTime.TabIndex = 51;
            this.lblSenderThinkTime.Text = "Sender Think Time (milliseconds):";
            // 
            // lblSavePropertiesOnExit
            // 
            this.lblSavePropertiesOnExit.AutoSize = true;
            this.lblSavePropertiesOnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSavePropertiesOnExit.Location = new System.Drawing.Point(18, 130);
            this.lblSavePropertiesOnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSavePropertiesOnExit.Name = "lblSavePropertiesOnExit";
            this.lblSavePropertiesOnExit.Size = new System.Drawing.Size(262, 17);
            this.lblSavePropertiesOnExit.TabIndex = 54;
            this.lblSavePropertiesOnExit.Text = "Save Message Properties to File on Exit:";
            // 
            // lblSaveMessageOnExit
            // 
            this.lblSaveMessageOnExit.AutoSize = true;
            this.lblSaveMessageOnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSaveMessageOnExit.Location = new System.Drawing.Point(18, 89);
            this.lblSaveMessageOnExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSaveMessageOnExit.Name = "lblSaveMessageOnExit";
            this.lblSaveMessageOnExit.Size = new System.Drawing.Size(229, 17);
            this.lblSaveMessageOnExit.TabIndex = 53;
            this.lblSaveMessageOnExit.Text = "Save Message Body to File on Exit:";
            // 
            // savePropertiesToFileCheckBox
            // 
            this.savePropertiesToFileCheckBox.AutoSize = true;
            this.savePropertiesToFileCheckBox.Checked = true;
            this.savePropertiesToFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.savePropertiesToFileCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.savePropertiesToFileCheckBox.Location = new System.Drawing.Point(352, 130);
            this.savePropertiesToFileCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.savePropertiesToFileCheckBox.Name = "savePropertiesToFileCheckBox";
            this.savePropertiesToFileCheckBox.Size = new System.Drawing.Size(18, 17);
            this.savePropertiesToFileCheckBox.TabIndex = 55;
            this.savePropertiesToFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // tabPageConnectivity
            // 
            this.tabPageConnectivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageConnectivity.Controls.Add(this.useAmqpWebSocketsCheckBox);
            this.tabPageConnectivity.Controls.Add(this.label4);
            this.tabPageConnectivity.Controls.Add(this.cboConnectivityMode);
            this.tabPageConnectivity.Controls.Add(this.lblConnectivityMode);
            this.tabPageConnectivity.Location = new System.Drawing.Point(4, 25);
            this.tabPageConnectivity.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageConnectivity.Name = "tabPageConnectivity";
            this.tabPageConnectivity.Size = new System.Drawing.Size(759, 581);
            this.tabPageConnectivity.TabIndex = 3;
            this.tabPageConnectivity.Text = "Connectivity";
            // 
            // useAmqpWebSocketsCheckBox
            // 
            this.useAmqpWebSocketsCheckBox.AutoSize = true;
            this.useAmqpWebSocketsCheckBox.Enabled = false;
            this.useAmqpWebSocketsCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.useAmqpWebSocketsCheckBox.Location = new System.Drawing.Point(556, 118);
            this.useAmqpWebSocketsCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.useAmqpWebSocketsCheckBox.Name = "useAmqpWebSocketsCheckBox";
            this.useAmqpWebSocketsCheckBox.Size = new System.Drawing.Size(18, 17);
            this.useAmqpWebSocketsCheckBox.TabIndex = 68;
            this.useAmqpWebSocketsCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(29, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(456, 17);
            this.label4.TabIndex = 29;
            this.label4.Text = "Use AMQP Web Sockets for Microsoft.Azure.ServiceBus.dll (new client)";
            // 
            // cboConnectivityMode
            // 
            this.cboConnectivityMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnectivityMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboConnectivityMode.FormattingEnabled = true;
            this.cboConnectivityMode.Location = new System.Drawing.Point(469, 57);
            this.cboConnectivityMode.Margin = new System.Windows.Forms.Padding(4);
            this.cboConnectivityMode.Name = "cboConnectivityMode";
            this.cboConnectivityMode.Size = new System.Drawing.Size(105, 24);
            this.cboConnectivityMode.TabIndex = 28;
            // 
            // lblConnectivityMode
            // 
            this.lblConnectivityMode.AutoSize = true;
            this.lblConnectivityMode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblConnectivityMode.Location = new System.Drawing.Point(29, 64);
            this.lblConnectivityMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnectivityMode.Name = "lblConnectivityMode";
            this.lblConnectivityMode.Size = new System.Drawing.Size(404, 17);
            this.lblConnectivityMode.TabIndex = 27;
            this.lblConnectivityMode.Text = "Connectivity Mode for WindowsAzure.ServiceBus.dll (old client)";
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.mainPanel.Controls.Add(this.tabOptionsControl);
            this.mainPanel.Controls.Add(this.btnOpenConfig);
            this.mainPanel.Controls.Add(this.cboConfigFile);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Location = new System.Drawing.Point(9, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(795, 662);
            this.mainPanel.TabIndex = 1;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // OptionForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(823, 711);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.tabOptionsControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRefreshIntervalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serverTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logNumericUpDown)).EndInit();
            this.tabPageReceiving.ResumeLayout(false);
            this.tabPageReceiving.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.receiverThinkTimeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prefetchCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.receiveTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryTimeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retryCountNumericUpDown)).EndInit();
            this.tabPageSending.ResumeLayout(false);
            this.tabPageSending.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.senderThinkTimeNumericUpDown)).EndInit();
            this.tabPageConnectivity.ResumeLayout(false);
            this.tabPageConnectivity.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboConfigFile;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.TabControl tabOptionsControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Label lblSaveCheckpointsOnExit;
        private System.Windows.Forms.CheckBox saveCheckpointsToFileCheckBox;
        private CheckBoxComboBox cboSelectedEntities;
        private System.Windows.Forms.Label lblSelectedEntities;
        private System.Windows.Forms.NumericUpDown monitorRefreshIntervalNumericUpDown;
        private System.Windows.Forms.Label lblMonitorRefreshInterval;
        private System.Windows.Forms.CheckBox useAsciiCheckBox;
        private System.Windows.Forms.Label lblUseAscii;
        private System.Windows.Forms.Label lblShowMessageCount;
        private System.Windows.Forms.CheckBox showMessageCountCheckBox;
        private System.Windows.Forms.Label lblEncoding;
        private System.Windows.Forms.ComboBox cboEncodingType;
        private System.Windows.Forms.NumericUpDown serverTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblServerTimeout;
        private System.Windows.Forms.NumericUpDown treeViewNumericUpDown;
        private System.Windows.Forms.Label lblTreeViewFontSize;
        private System.Windows.Forms.NumericUpDown logNumericUpDown;
        private System.Windows.Forms.Label lblLogFontSize;
        private System.Windows.Forms.TabPage tabPageReceiving;
        private System.Windows.Forms.NumericUpDown receiverThinkTimeNumericUpDown;
        private System.Windows.Forms.Label lblReceiverThinkTime;
        private System.Windows.Forms.NumericUpDown prefetchCountNumericUpDown;
        private System.Windows.Forms.Label lblPrefetchCount;
        private System.Windows.Forms.NumericUpDown receiveTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblReceiveTimeout;
        private System.Windows.Forms.NumericUpDown topNumericUpDown;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.NumericUpDown retryTimeoutNumericUpDown;
        private System.Windows.Forms.Label lblRetryTimeout;
        private System.Windows.Forms.NumericUpDown retryCountNumericUpDown;
        private System.Windows.Forms.Label lblRetryCount;
        private System.Windows.Forms.TabPage tabPageSending;
        private System.Windows.Forms.CheckBox saveMessageToFileCheckBox;
        private System.Windows.Forms.Label lblMessageContentType;
        private System.Windows.Forms.TextBox txtMessageContentType;
        private System.Windows.Forms.ComboBox cboDefaultMessageBodyType;
        private System.Windows.Forms.Label LabelDefaultMessageBodyType;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtMessageFile;
        private System.Windows.Forms.Label lblMessageFile;
        private System.Windows.Forms.TextBox txtMessageText;
        private System.Windows.Forms.Label lblMessageText;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.NumericUpDown senderThinkTimeNumericUpDown;
        private System.Windows.Forms.Label lblSenderThinkTime;
        private System.Windows.Forms.Label lblSavePropertiesOnExit;
        private System.Windows.Forms.Label lblSaveMessageOnExit;
        private System.Windows.Forms.CheckBox savePropertiesToFileCheckBox;
        private System.Windows.Forms.TabPage tabPageConnectivity;
        private System.Windows.Forms.ComboBox cboConnectivityMode;
        private System.Windows.Forms.Label lblConnectivityMode;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.CheckBox useAmqpWebSocketsCheckBox;
        private System.Windows.Forms.Label label4;
    }
}
