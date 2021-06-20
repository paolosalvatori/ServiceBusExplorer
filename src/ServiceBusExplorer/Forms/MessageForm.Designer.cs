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
    partial class MessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.messagesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.messageListTextPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageText = new ServiceBusExplorer.Controls.Grouper();
            this.chkAutoindent = new System.Windows.Forms.CheckBox();
            this.txtMessageText = new FastColoredTextBoxNS.FastColoredTextBox();
            this.grouperMessageCustomProperties = new ServiceBusExplorer.Controls.Grouper();
            this.legend = new System.Windows.Forms.Label();
            this.propertiesDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperMessageProperties = new ServiceBusExplorer.Controls.Grouper();
            this.messagePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.lblBody = new System.Windows.Forms.Label();
            this.cboBodyType = new System.Windows.Forms.ComboBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.cboSenderInspector = new System.Windows.Forms.ComboBox();
            this.lblReceiverInspector = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.chkNewMessageId = new System.Windows.Forms.CheckBox();
            this.chkRemove = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.messagesSplitContainer)).BeginInit();
            this.messagesSplitContainer.Panel1.SuspendLayout();
            this.messagesSplitContainer.Panel2.SuspendLayout();
            this.messagesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).BeginInit();
            this.messageListTextPropertiesSplitContainer.Panel1.SuspendLayout();
            this.messageListTextPropertiesSplitContainer.Panel2.SuspendLayout();
            this.messageListTextPropertiesSplitContainer.SuspendLayout();
            this.grouperMessageText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessageText)).BeginInit();
            this.grouperMessageCustomProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).BeginInit();
            this.grouperMessageProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(880, 508);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 24);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(800, 508);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 24);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // messagesSplitContainer
            // 
            this.messagesSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesSplitContainer.Location = new System.Drawing.Point(16, 16);
            this.messagesSplitContainer.Name = "messagesSplitContainer";
            // 
            // messagesSplitContainer.Panel1
            // 
            this.messagesSplitContainer.Panel1.Controls.Add(this.messageListTextPropertiesSplitContainer);
            // 
            // messagesSplitContainer.Panel2
            // 
            this.messagesSplitContainer.Panel2.Controls.Add(this.grouperMessageProperties);
            this.messagesSplitContainer.Size = new System.Drawing.Size(936, 474);
            this.messagesSplitContainer.SplitterDistance = 666;
            this.messagesSplitContainer.SplitterWidth = 16;
            this.messagesSplitContainer.TabIndex = 4;
            // 
            // messageListTextPropertiesSplitContainer
            // 
            this.messageListTextPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageListTextPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messageListTextPropertiesSplitContainer.Name = "messageListTextPropertiesSplitContainer";
            this.messageListTextPropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // messageListTextPropertiesSplitContainer.Panel1
            // 
            this.messageListTextPropertiesSplitContainer.Panel1.Controls.Add(this.grouperMessageText);
            // 
            // messageListTextPropertiesSplitContainer.Panel2
            // 
            this.messageListTextPropertiesSplitContainer.Panel2.Controls.Add(this.grouperMessageCustomProperties);
            this.messageListTextPropertiesSplitContainer.Size = new System.Drawing.Size(666, 474);
            this.messageListTextPropertiesSplitContainer.SplitterDistance = 225;
            this.messageListTextPropertiesSplitContainer.SplitterWidth = 8;
            this.messageListTextPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperMessageText
            // 
            this.grouperMessageText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageText.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.BorderThickness = 1F;
            this.grouperMessageText.Controls.Add(this.chkAutoindent);
            this.grouperMessageText.Controls.Add(this.txtMessageText);
            this.grouperMessageText.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageText.ForeColor = System.Drawing.Color.White;
            this.grouperMessageText.GroupImage = null;
            this.grouperMessageText.GroupTitle = "Message Text";
            this.grouperMessageText.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageText.Name = "grouperMessageText";
            this.grouperMessageText.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageText.PaintGroupBox = true;
            this.grouperMessageText.RoundCorners = 4;
            this.grouperMessageText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageText.ShadowControl = false;
            this.grouperMessageText.ShadowThickness = 1;
            this.grouperMessageText.Size = new System.Drawing.Size(666, 225);
            this.grouperMessageText.TabIndex = 0;
            // 
            // chkAutoindent
            // 
            this.chkAutoindent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoindent.AutoSize = true;
            this.chkAutoindent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.chkAutoindent.Checked = true;
            this.chkAutoindent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoindent.ForeColor = System.Drawing.Color.Black;
            this.chkAutoindent.Location = new System.Drawing.Point(530, 3);
            this.chkAutoindent.Name = "chkAutoindent";
            this.chkAutoindent.Padding = new System.Windows.Forms.Padding(18, 0, 9, 0);
            this.chkAutoindent.Size = new System.Drawing.Size(104, 17);
            this.chkAutoindent.TabIndex = 0;
            this.chkAutoindent.Text = "Autoindent";
            this.chkAutoindent.UseVisualStyleBackColor = false;
            this.chkAutoindent.CheckedChanged += new System.EventHandler(this.ChkAutoindent_CheckedChanged);
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.AutoCompleteBracketsList = new char[] {
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
            this.txtMessageText.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtMessageText.BackBrush = null;
            this.txtMessageText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessageText.CharHeight = 14;
            this.txtMessageText.CharWidth = 8;
            this.txtMessageText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMessageText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtMessageText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtMessageText.IsReplaceMode = false;
            this.txtMessageText.Location = new System.Drawing.Point(16, 32);
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.Paddings = new System.Windows.Forms.Padding(0);
            this.txtMessageText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtMessageText.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtMessageText.ServiceColors")));
            this.txtMessageText.Size = new System.Drawing.Size(632, 176);
            this.txtMessageText.TabIndex = 1;
            this.txtMessageText.Zoom = 100;
            // 
            // grouperMessageCustomProperties
            // 
            this.grouperMessageCustomProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageCustomProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageCustomProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageCustomProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageCustomProperties.BorderThickness = 1F;
            this.grouperMessageCustomProperties.Controls.Add(this.legend);
            this.grouperMessageCustomProperties.Controls.Add(this.propertiesDataGridView);
            this.grouperMessageCustomProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageCustomProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageCustomProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageCustomProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageCustomProperties.GroupImage = null;
            this.grouperMessageCustomProperties.GroupTitle = "Message Custom Properties";
            this.grouperMessageCustomProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageCustomProperties.Name = "grouperMessageCustomProperties";
            this.grouperMessageCustomProperties.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.grouperMessageCustomProperties.PaintGroupBox = true;
            this.grouperMessageCustomProperties.RoundCorners = 4;
            this.grouperMessageCustomProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageCustomProperties.ShadowControl = false;
            this.grouperMessageCustomProperties.ShadowThickness = 1;
            this.grouperMessageCustomProperties.Size = new System.Drawing.Size(666, 241);
            this.grouperMessageCustomProperties.TabIndex = 0;
            this.grouperMessageCustomProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageCustomProperties_CustomPaint);
            // 
            // legend
            // 
            this.legend.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.legend.AutoSize = true;
            this.legend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.legend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.legend.ForeColor = System.Drawing.Color.Black;
            this.legend.Location = new System.Drawing.Point(20, 226);
            this.legend.Name = "legend";
            this.legend.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.legend.Size = new System.Drawing.Size(313, 15);
            this.legend.TabIndex = 1;
            this.legend.Text = "* To delete a property, select the row and press the DELETE key";
            // 
            // propertiesDataGridView
            // 
            this.propertiesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.propertiesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.propertiesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.propertiesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertiesDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.propertiesDataGridView.Location = new System.Drawing.Point(16, 32);
            this.propertiesDataGridView.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.propertiesDataGridView.Name = "propertiesDataGridView";
            this.propertiesDataGridView.Size = new System.Drawing.Size(636, 194);
            this.propertiesDataGridView.TabIndex = 0;
            this.propertiesDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.propertiesDataGridView_DataError);
            this.propertiesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.propertiesDataGridView_RowsAdded);
            this.propertiesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.propertiesDataGridView_RowsRemoved);
            this.propertiesDataGridView.Resize += new System.EventHandler(this.propertiesDataGridView_Resize);
            // 
            // grouperMessageProperties
            // 
            this.grouperMessageProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.BorderThickness = 1F;
            this.grouperMessageProperties.Controls.Add(this.messagePropertyGrid);
            this.grouperMessageProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageProperties.GroupImage = null;
            this.grouperMessageProperties.GroupTitle = "Message Properties";
            this.grouperMessageProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageProperties.Name = "grouperMessageProperties";
            this.grouperMessageProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageProperties.PaintGroupBox = true;
            this.grouperMessageProperties.RoundCorners = 4;
            this.grouperMessageProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageProperties.ShadowControl = false;
            this.grouperMessageProperties.ShadowThickness = 1;
            this.grouperMessageProperties.Size = new System.Drawing.Size(254, 474);
            this.grouperMessageProperties.TabIndex = 0;
            // 
            // messagePropertyGrid
            // 
            this.messagePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagePropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.messagePropertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.messagePropertyGrid.HelpVisible = false;
            this.messagePropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.messagePropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.messagePropertyGrid.Name = "messagePropertyGrid";
            this.messagePropertyGrid.Size = new System.Drawing.Size(221, 428);
            this.messagePropertyGrid.TabIndex = 0;
            this.messagePropertyGrid.ToolbarVisible = false;
            // 
            // lblBody
            // 
            this.lblBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBody.AutoSize = true;
            this.lblBody.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBody.Location = new System.Drawing.Point(322, 512);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(61, 13);
            this.lblBody.TabIndex = 2;
            this.lblBody.Text = "Body Type:";
            // 
            // cboBodyType
            // 
            this.cboBodyType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBodyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBodyType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboBodyType.FormattingEnabled = true;
            this.cboBodyType.Items.AddRange(new object[] {
            "Stream",
            "String",
            "WCF",
            "ByteArray"});
            this.cboBodyType.Location = new System.Drawing.Point(386, 509);
            this.cboBodyType.Name = "cboBodyType";
            this.cboBodyType.Size = new System.Drawing.Size(100, 21);
            this.cboBodyType.TabIndex = 3;
            // 
            // cboSenderInspector
            // 
            this.cboSenderInspector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSenderInspector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSenderInspector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSenderInspector.FormattingEnabled = true;
            this.cboSenderInspector.Location = new System.Drawing.Point(120, 509);
            this.cboSenderInspector.MaximumSize = new System.Drawing.Size(198, 0);
            this.cboSenderInspector.Name = "cboSenderInspector";
            this.cboSenderInspector.Size = new System.Drawing.Size(198, 21);
            this.cboSenderInspector.TabIndex = 1;
            // 
            // lblReceiverInspector
            // 
            this.lblReceiverInspector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReceiverInspector.AutoSize = true;
            this.lblReceiverInspector.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReceiverInspector.Location = new System.Drawing.Point(16, 512);
            this.lblReceiverInspector.Name = "lblReceiverInspector";
            this.lblReceiverInspector.Size = new System.Drawing.Size(100, 13);
            this.lblReceiverInspector.TabIndex = 0;
            this.lblReceiverInspector.Text = "Message Inspector:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Location = new System.Drawing.Point(720, 508);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(72, 24);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // chkNewMessageId
            // 
            this.chkNewMessageId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkNewMessageId.Location = new System.Drawing.Point(621, 502);
            this.chkNewMessageId.Name = "chkNewMessageId";
            this.chkNewMessageId.Size = new System.Drawing.Size(93, 39);
            this.chkNewMessageId.TabIndex = 5;
            this.chkNewMessageId.Text = "Generate new MessageId";
            this.chkNewMessageId.UseVisualStyleBackColor = true;
            // 
            // chkRemove
            // 
            this.chkRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRemove.Location = new System.Drawing.Point(491, 502);
            this.chkRemove.Name = "chkRemove";
            this.chkRemove.Size = new System.Drawing.Size(124, 39);
            this.chkRemove.TabIndex = 4;
            this.chkRemove.Text = "Remove message from DLQ";
            this.chkRemove.UseVisualStyleBackColor = true;
            this.chkRemove.Visible = false;
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(968, 541);
            this.Controls.Add(this.chkRemove);
            this.Controls.Add(this.chkNewMessageId);
            this.Controls.Add(this.cboSenderInspector);
            this.Controls.Add(this.lblReceiverInspector);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.cboBodyType);
            this.Controls.Add(this.messagesSplitContainer);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repair and Resubmit Message";
            this.Load += new System.EventHandler(this.MessageForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MessageForm_Paint);
            this.messagesSplitContainer.Panel1.ResumeLayout(false);
            this.messagesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesSplitContainer)).EndInit();
            this.messagesSplitContainer.ResumeLayout(false);
            this.messageListTextPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.messageListTextPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).EndInit();
            this.messageListTextPropertiesSplitContainer.ResumeLayout(false);
            this.grouperMessageText.ResumeLayout(false);
            this.grouperMessageText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessageText)).EndInit();
            this.grouperMessageCustomProperties.ResumeLayout(false);
            this.grouperMessageCustomProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).EndInit();
            this.grouperMessageProperties.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SplitContainer messagesSplitContainer;
        private System.Windows.Forms.SplitContainer messageListTextPropertiesSplitContainer;
        private Grouper grouperMessageProperties;
        private System.Windows.Forms.PropertyGrid messagePropertyGrid;
        private Grouper grouperMessageText;
        private Grouper grouperMessageCustomProperties;
        private System.Windows.Forms.DataGridView propertiesDataGridView;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.ComboBox cboBodyType;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ComboBox cboSenderInspector;
        private System.Windows.Forms.Label lblReceiverInspector;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.CheckBox chkNewMessageId;
        private System.Windows.Forms.CheckBox chkRemove;
        private FastColoredTextBoxNS.FastColoredTextBox txtMessageText;
        private System.Windows.Forms.CheckBox chkAutoindent;
        private System.Windows.Forms.Label legend;
    }
}
