namespace ServiceBusExplorer.Forms
{
    partial class ReceiveEventForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.grouperMessages = new ServiceBusExplorer.Controls.Grouper();
            this.txtEventCount = new ServiceBusExplorer.Controls.NumericTextBox();
            this.btnMax = new System.Windows.Forms.RadioButton();
            this.btnTop = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grouperMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouperMessages
            // 
            this.grouperMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperMessages.BackgroundColor = System.Drawing.Color.White;
            this.grouperMessages.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessages.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessages.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessages.BorderThickness = 1F;
            this.grouperMessages.Controls.Add(this.txtEventCount);
            this.grouperMessages.Controls.Add(this.btnTop);
            this.grouperMessages.Controls.Add(this.btnMax);
            this.grouperMessages.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessages.ForeColor = System.Drawing.Color.White;
            this.grouperMessages.GroupImage = null;
            this.grouperMessages.GroupTitle = "Event Count";
            this.grouperMessages.Location = new System.Drawing.Point(27, 23);
            this.grouperMessages.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grouperMessages.Name = "grouperMessages";
            this.grouperMessages.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.grouperMessages.PaintGroupBox = true;
            this.grouperMessages.RoundCorners = 4;
            this.grouperMessages.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessages.ShadowControl = false;
            this.grouperMessages.ShadowThickness = 1;
            this.grouperMessages.Size = new System.Drawing.Size(308, 102);
            this.grouperMessages.TabIndex = 43;
            // 
            // txtEventCount
            // 
            this.txtEventCount.AllowDecimal = false;
            this.txtEventCount.AllowNegative = false;
            this.txtEventCount.AllowSpace = false;
            this.txtEventCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEventCount.IsZeroWhenEmpty = false;
            this.txtEventCount.Location = new System.Drawing.Point(168, 49);
            this.txtEventCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEventCount.Name = "txtEventCount";
            this.txtEventCount.Size = new System.Drawing.Size(114, 26);
            this.txtEventCount.TabIndex = 42;
            // 
            // btnMax
            // 
            this.btnMax.AutoSize = true;
            this.btnMax.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMax.Location = new System.Drawing.Point(24, 50);
            this.btnMax.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(65, 24);
            this.btnMax.TabIndex = 40;
            this.btnMax.Text = "Max";
            this.btnMax.UseVisualStyleBackColor = true;
            // 
            // btnTop
            // 
            this.btnTop.AutoSize = true;
            this.btnTop.Checked = true;
            this.btnTop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTop.Location = new System.Drawing.Point(96, 49);
            this.btnTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(62, 24);
            this.btnTop.TabIndex = 41;
            this.btnTop.TabStop = true;
            this.btnTop.Text = "Top";
            this.btnTop.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(100, 149);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(108, 37);
            this.btnOk.TabIndex = 44;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(227, 149);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 37);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ReceiveEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(365, 200);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grouperMessages);
            this.MaximumSize = new System.Drawing.Size(387, 256);
            this.MinimumSize = new System.Drawing.Size(387, 256);
            this.Name = "ReceiveEventForm";
            this.Text = "Receive Events";
            this.grouperMessages.ResumeLayout(false);
            this.grouperMessages.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Controls.Grouper grouperMessages;
        private Controls.NumericTextBox txtEventCount;
        private System.Windows.Forms.RadioButton btnMax;
        private System.Windows.Forms.RadioButton btnTop;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
