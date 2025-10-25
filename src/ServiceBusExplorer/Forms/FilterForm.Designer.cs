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
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.grouperFilter = new ServiceBusExplorer.Controls.Grouper();
            this.cboMessageCountOperator = new System.Windows.Forms.ComboBox();
            this.txtMessageCount = new ServiceBusExplorer.Controls.NumericTextBox();
            this.lblMessageCount = new System.Windows.Forms.Label();
            this.lblTimeFilters = new System.Windows.Forms.Label();
            this.txtStartsWith = new System.Windows.Forms.TextBox();
            this.timeFilterDataGridView = new System.Windows.Forms.DataGridView();
            this.lblStartsWith = new System.Windows.Forms.Label();
            this.lblFilterExpression = new System.Windows.Forms.Label();
            this.txtFilterExpression = new System.Windows.Forms.TextBox();
            this.grouperFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeFilterDataGridView)).BeginInit();
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
            this.btnOk.Location = new System.Drawing.Point(248, 432);
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
            this.btnCancel.Location = new System.Drawing.Point(328, 432);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Queue.ico");
            this.imageList.Images.SetKeyName(1, "Topic.ico");
            this.imageList.Images.SetKeyName(2, "Chart.ico");
            this.imageList.Images.SetKeyName(3, "Class.ico");
            this.imageList.Images.SetKeyName(4, "Add.ico");
            this.imageList.Images.SetKeyName(5, "UserInfo.ico");
            this.imageList.Images.SetKeyName(6, "exec.ico");
            this.imageList.Images.SetKeyName(7, "AzureLogo.ico");
            this.imageList.Images.SetKeyName(8, "World.png");
            this.imageList.Images.SetKeyName(9, "Relay.png");
            this.imageList.Images.SetKeyName(10, "folder_web.ico");
            this.imageList.Images.SetKeyName(11, "Web.ico");
            this.imageList.Images.SetKeyName(12, "GreyChart.ico");
            this.imageList.Images.SetKeyName(13, "GreyClass.ico");
            this.imageList.Images.SetKeyName(14, "GreyUserInfo.ico");
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(168, 432);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 24);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // grouperFilter
            // 
            this.grouperFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperFilter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperFilter.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperFilter.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilter.BorderThickness = 1F;
            this.grouperFilter.Controls.Add(this.cboMessageCountOperator);
            this.grouperFilter.Controls.Add(this.txtMessageCount);
            this.grouperFilter.Controls.Add(this.lblMessageCount);
            this.grouperFilter.Controls.Add(this.lblTimeFilters);
            this.grouperFilter.Controls.Add(this.txtStartsWith);
            this.grouperFilter.Controls.Add(this.timeFilterDataGridView);
            this.grouperFilter.Controls.Add(this.lblStartsWith);
            this.grouperFilter.Controls.Add(this.lblFilterExpression);
            this.grouperFilter.Controls.Add(this.txtFilterExpression);
            this.grouperFilter.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperFilter.ForeColor = System.Drawing.Color.White;
            this.grouperFilter.GroupImage = null;
            this.grouperFilter.GroupTitle = "FilterExpression";
            this.grouperFilter.Location = new System.Drawing.Point(16, 16);
            this.grouperFilter.Name = "grouperFilter";
            this.grouperFilter.Padding = new System.Windows.Forms.Padding(20);
            this.grouperFilter.PaintGroupBox = true;
            this.grouperFilter.RoundCorners = 4;
            this.grouperFilter.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperFilter.ShadowControl = false;
            this.grouperFilter.ShadowThickness = 1;
            this.grouperFilter.Size = new System.Drawing.Size(384, 400);
            this.grouperFilter.TabIndex = 0;
            this.grouperFilter.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperFilter_CustomPaint);
            // 
            // cboMessageCountOperator
            // 
            this.cboMessageCountOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMessageCountOperator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMessageCountOperator.FormattingEnabled = true;
            this.cboMessageCountOperator.Items.AddRange(new object[] {
            "None",
            "Eq",
            "Ne",
            "Gt",
            "Ge",
            "Le",
            "Lt"});
            this.cboMessageCountOperator.Location = new System.Drawing.Point(16, 96);
            this.cboMessageCountOperator.Name = "cboMessageCountOperator";
            this.cboMessageCountOperator.Size = new System.Drawing.Size(72, 21);
            this.cboMessageCountOperator.TabIndex = 3;
            this.cboMessageCountOperator.TextChanged += new System.EventHandler(this.cboMessageCountOperator_TextChanged);
            // 
            // txtMessageCount
            // 
            this.txtMessageCount.AllowDecimal = false;
            this.txtMessageCount.AllowNegative = false;
            this.txtMessageCount.AllowSpace = false;
            this.txtMessageCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageCount.Location = new System.Drawing.Point(96, 96);
            this.txtMessageCount.Name = "txtMessageCount";
            this.txtMessageCount.Size = new System.Drawing.Size(272, 20);
            this.txtMessageCount.TabIndex = 4;
            this.txtMessageCount.TextChanged += new System.EventHandler(this.txtMessageCount_TextChanged);
            // 
            // lblMessageCount
            // 
            this.lblMessageCount.AutoSize = true;
            this.lblMessageCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageCount.Location = new System.Drawing.Point(16, 80);
            this.lblMessageCount.Name = "lblMessageCount";
            this.lblMessageCount.Size = new System.Drawing.Size(109, 13);
            this.lblMessageCount.TabIndex = 2;
            this.lblMessageCount.Text = "Message Count Filter:";
            // 
            // lblTimeFilters
            // 
            this.lblTimeFilters.AutoSize = true;
            this.lblTimeFilters.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTimeFilters.Location = new System.Drawing.Point(16, 128);
            this.lblTimeFilters.Name = "lblTimeFilters";
            this.lblTimeFilters.Size = new System.Drawing.Size(63, 13);
            this.lblTimeFilters.TabIndex = 5;
            this.lblTimeFilters.Text = "Time Filters:";
            // 
            // txtStartsWith
            // 
            this.txtStartsWith.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartsWith.Location = new System.Drawing.Point(16, 48);
            this.txtStartsWith.Name = "txtStartsWith";
            this.txtStartsWith.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStartsWith.Size = new System.Drawing.Size(352, 20);
            this.txtStartsWith.TabIndex = 1;
            this.txtStartsWith.TextChanged += new System.EventHandler(this.txtStartsWith_TextChanged);
            // 
            // timeFilterDataGridView
            // 
            this.timeFilterDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeFilterDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.timeFilterDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.timeFilterDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timeFilterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.timeFilterDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.timeFilterDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.timeFilterDataGridView.Location = new System.Drawing.Point(17, 145);
            this.timeFilterDataGridView.Name = "timeFilterDataGridView";
            this.timeFilterDataGridView.Size = new System.Drawing.Size(350, 166);
            this.timeFilterDataGridView.TabIndex = 6;
            this.timeFilterDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.timeFilterDataGridView_CellClick);
            this.timeFilterDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.filtersDataGridView_DataError);
            this.timeFilterDataGridView.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.filtersDataGridView_RowLeave);
            this.timeFilterDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.filtersDataGridView_RowsAdded);
            this.timeFilterDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.filtersDataGridView_RowsRemoved);
            this.timeFilterDataGridView.Leave += new System.EventHandler(this.filtersDataGridView_Leave);
            this.timeFilterDataGridView.Resize += new System.EventHandler(this.filtersDataGridView_Resize);
            // 
            // lblStartsWith
            // 
            this.lblStartsWith.AutoSize = true;
            this.lblStartsWith.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStartsWith.Location = new System.Drawing.Point(16, 32);
            this.lblStartsWith.Name = "lblStartsWith";
            this.lblStartsWith.Size = new System.Drawing.Size(112, 13);
            this.lblStartsWith.TabIndex = 0;
            this.lblStartsWith.Text = "Path Starts With Filter:";
            // 
            // lblFilterExpression
            // 
            this.lblFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFilterExpression.AutoSize = true;
            this.lblFilterExpression.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFilterExpression.Location = new System.Drawing.Point(16, 320);
            this.lblFilterExpression.Name = "lblFilterExpression";
            this.lblFilterExpression.Size = new System.Drawing.Size(86, 13);
            this.lblFilterExpression.TabIndex = 7;
            this.lblFilterExpression.Text = "Filter Expression:";
            // 
            // txtFilterExpression
            // 
            this.txtFilterExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterExpression.Location = new System.Drawing.Point(16, 336);
            this.txtFilterExpression.Multiline = true;
            this.txtFilterExpression.Name = "txtFilterExpression";
            this.txtFilterExpression.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFilterExpression.Size = new System.Drawing.Size(352, 50);
            this.txtFilterExpression.TabIndex = 8;
            this.txtFilterExpression.TextChanged += new System.EventHandler(this.txtFilterExpression_TextChanged);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(416, 473);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.grouperFilter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FilterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Filter Expression";
            this.Activated += new System.EventHandler(this.TextForm_Activated);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterForm_KeyPress);
            this.grouperFilter.ResumeLayout(false);
            this.grouperFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeFilterDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private Grouper grouperFilter;
        private System.Windows.Forms.TextBox txtFilterExpression;
        private System.Windows.Forms.Label lblFilterExpression;
        private System.Windows.Forms.Label lblStartsWith;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView timeFilterDataGridView;
        private System.Windows.Forms.TextBox txtStartsWith;
        private System.Windows.Forms.Label lblTimeFilters;
        private System.Windows.Forms.Label lblMessageCount;
        private NumericTextBox txtMessageCount;
        private System.Windows.Forms.ComboBox cboMessageCountOperator;
    }
}
