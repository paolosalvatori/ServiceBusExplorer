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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiveEventForm));
            this.grouperReceiveEvents = new ServiceBusExplorer.Controls.Grouper();
            this.txtEventCount = new ServiceBusExplorer.Controls.NumericTextBox();
            this.btnTop = new System.Windows.Forms.RadioButton();
            this.btnMax = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grouperReceiveEvents.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouperReceiveEvents
            // 
            this.grouperReceiveEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperReceiveEvents.BackgroundColor = System.Drawing.Color.White;
            this.grouperReceiveEvents.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperReceiveEvents.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperReceiveEvents.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperReceiveEvents.BorderThickness = 1F;
            this.grouperReceiveEvents.Controls.Add(this.txtEventCount);
            this.grouperReceiveEvents.Controls.Add(this.btnTop);
            this.grouperReceiveEvents.Controls.Add(this.btnMax);
            this.grouperReceiveEvents.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperReceiveEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperReceiveEvents.ForeColor = System.Drawing.Color.White;
            this.grouperReceiveEvents.GroupImage = null;
            this.grouperReceiveEvents.GroupTitle = "Event Count";
            this.grouperReceiveEvents.Location = new System.Drawing.Point(18, 15);
            this.grouperReceiveEvents.Name = "grouperReceiveEvents";
            this.grouperReceiveEvents.Padding = new System.Windows.Forms.Padding(20, 21, 20, 21);
            this.grouperReceiveEvents.PaintGroupBox = true;
            this.grouperReceiveEvents.RoundCorners = 4;
            this.grouperReceiveEvents.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperReceiveEvents.ShadowControl = false;
            this.grouperReceiveEvents.ShadowThickness = 1;
            this.grouperReceiveEvents.Size = new System.Drawing.Size(205, 68);
            this.grouperReceiveEvents.TabIndex = 43;
            // 
            // txtEventCount
            // 
            this.txtEventCount.AllowDecimal = false;
            this.txtEventCount.AllowNegative = false;
            this.txtEventCount.AllowSpace = false;
            this.txtEventCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEventCount.IsZeroWhenEmpty = false;
            this.txtEventCount.Location = new System.Drawing.Point(112, 33);
            this.txtEventCount.Name = "txtEventCount";
            this.txtEventCount.Size = new System.Drawing.Size(77, 20);
            this.txtEventCount.TabIndex = 42;
            // 
            // btnTop
            // 
            this.btnTop.AutoSize = true;
            this.btnTop.Checked = true;
            this.btnTop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTop.Location = new System.Drawing.Point(64, 33);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(44, 17);
            this.btnTop.TabIndex = 41;
            this.btnTop.TabStop = true;
            this.btnTop.Text = "Top";
            this.btnTop.UseVisualStyleBackColor = true;
            this.btnTop.CheckedChanged += new System.EventHandler(this.receiveEvents_CheckedChanged);
            // 
            // btnMax
            // 
            this.btnMax.AutoSize = true;
            this.btnMax.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMax.Location = new System.Drawing.Point(16, 33);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(45, 17);
            this.btnMax.TabIndex = 40;
            this.btnMax.Text = "Max";
            this.btnMax.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(67, 99);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 25);
            this.btnOk.TabIndex = 44;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            this.btnCancel.Location = new System.Drawing.Point(151, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 25);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ReceiveEventForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(247, 145);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grouperReceiveEvents);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(263, 184);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(263, 184);
            this.Name = "ReceiveEventForm";
            this.Text = "Receive Events";
            this.grouperReceiveEvents.ResumeLayout(false);
            this.grouperReceiveEvents.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Grouper grouperReceiveEvents;
        private Controls.NumericTextBox txtEventCount;
        private System.Windows.Forms.RadioButton btnMax;
        private System.Windows.Forms.RadioButton btnTop;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
