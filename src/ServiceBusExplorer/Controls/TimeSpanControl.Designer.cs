
namespace ServiceBusExplorer.Controls
{
    partial class TimeSpanControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMilliseconds = new System.Windows.Forms.Label();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.lblDays = new System.Windows.Forms.Label();
            this.txtMilliseconds = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtSeconds = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtMinutes = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtHours = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtDays = new ServiceBusExplorer.Controls.NumericTextBox();
            this.SuspendLayout();
            // 
            // lblMilliseconds
            // 
            this.lblMilliseconds.AutoSize = true;
            this.lblMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMilliseconds.Location = new System.Drawing.Point(221, 3);
            this.lblMilliseconds.Name = "lblMilliseconds";
            this.lblMilliseconds.Size = new System.Drawing.Size(49, 13);
            this.lblMilliseconds.TabIndex = 8;
            this.lblMilliseconds.Text = "Millisecs:";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSeconds.Location = new System.Drawing.Point(166, 3);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(52, 13);
            this.lblSeconds.TabIndex = 6;
            this.lblSeconds.Text = "Seconds:";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMinutes.Location = new System.Drawing.Point(111, 3);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(47, 13);
            this.lblMinutes.TabIndex = 4;
            this.lblMinutes.Text = "Minutes:";
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHours.Location = new System.Drawing.Point(56, 3);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(38, 13);
            this.lblHours.TabIndex = 2;
            this.lblHours.Text = "Hours:";
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDays.Location = new System.Drawing.Point(1, 3);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(34, 13);
            this.lblDays.TabIndex = 0;
            this.lblDays.Text = "Days:";
            // 
            // txtMilliseconds
            // 
            this.txtMilliseconds.AllowDecimal = false;
            this.txtMilliseconds.AllowNegative = false;
            this.txtMilliseconds.AllowSpace = false;
            this.txtMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtMilliseconds.IsZeroWhenEmpty = true;
            this.txtMilliseconds.Location = new System.Drawing.Point(223, 19);
            this.txtMilliseconds.Name = "txtMilliseconds";
            this.txtMilliseconds.Size = new System.Drawing.Size(40, 20);
            this.txtMilliseconds.TabIndex = 9;
            // 
            // txtSeconds
            // 
            this.txtSeconds.AllowDecimal = false;
            this.txtSeconds.AllowNegative = false;
            this.txtSeconds.AllowSpace = false;
            this.txtSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtSeconds.IsZeroWhenEmpty = true;
            this.txtSeconds.Location = new System.Drawing.Point(168, 19);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(40, 20);
            this.txtSeconds.TabIndex = 7;
            // 
            // txtMinutes
            // 
            this.txtMinutes.AllowDecimal = false;
            this.txtMinutes.AllowNegative = false;
            this.txtMinutes.AllowSpace = false;
            this.txtMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtMinutes.IsZeroWhenEmpty = true;
            this.txtMinutes.Location = new System.Drawing.Point(113, 19);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(40, 20);
            this.txtMinutes.TabIndex = 5;
            // 
            // txtHours
            // 
            this.txtHours.AllowDecimal = false;
            this.txtHours.AllowNegative = false;
            this.txtHours.AllowSpace = false;
            this.txtHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtHours.IsZeroWhenEmpty = true;
            this.txtHours.Location = new System.Drawing.Point(58, 19);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(40, 20);
            this.txtHours.TabIndex = 3;
            // 
            // txtDays
            // 
            this.txtDays.AllowDecimal = false;
            this.txtDays.AllowNegative = false;
            this.txtDays.AllowSpace = false;
            this.txtDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtDays.IsZeroWhenEmpty = true;
            this.txtDays.Location = new System.Drawing.Point(3, 19);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(40, 20);
            this.txtDays.TabIndex = 1;
            // 
            // TimeSpanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMilliseconds);
            this.Controls.Add(this.txtMilliseconds);
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.txtSeconds);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.txtMinutes);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.lblDays);
            this.Controls.Add(this.txtHours);
            this.Controls.Add(this.txtDays);
            this.Name = "TimeSpanControl";
            this.Size = new System.Drawing.Size(273, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMilliseconds;
        private NumericTextBox txtMilliseconds;
        private System.Windows.Forms.Label lblSeconds;
        private NumericTextBox txtSeconds;
        private System.Windows.Forms.Label lblMinutes;
        private NumericTextBox txtMinutes;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblDays;
        private NumericTextBox txtHours;
        private NumericTextBox txtDays;
    }
}
