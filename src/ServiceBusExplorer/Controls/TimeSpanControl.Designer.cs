
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDays = new System.Windows.Forms.Label();
            this.txtDays = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtMilliseconds = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtSeconds = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtHours = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtMinutes = new ServiceBusExplorer.Controls.NumericTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMilliseconds
            // 
            this.lblMilliseconds.AutoSize = true;
            this.lblMilliseconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMilliseconds.Location = new System.Drawing.Point(219, 0);
            this.lblMilliseconds.Name = "lblMilliseconds";
            this.lblMilliseconds.Size = new System.Drawing.Size(46, 13);
            this.lblMilliseconds.TabIndex = 8;
            this.lblMilliseconds.Text = "Millisecs";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSeconds.Location = new System.Drawing.Point(165, 0);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(44, 26);
            this.lblSeconds.TabIndex = 6;
            this.lblSeconds.Text = "Seconds";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMinutes.Location = new System.Drawing.Point(111, 0);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(44, 13);
            this.lblMinutes.TabIndex = 4;
            this.lblMinutes.Text = "Minutes";
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHours.Location = new System.Drawing.Point(57, 0);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(35, 13);
            this.lblHours.TabIndex = 2;
            this.lblHours.Text = "Hours";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.txtDays, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtMilliseconds, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMilliseconds, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDays, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblHours, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSeconds, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSeconds, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtHours, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMinutes, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMinutes, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(273, 42);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDays.Location = new System.Drawing.Point(3, 0);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(31, 13);
            this.lblDays.TabIndex = 1;
            this.lblDays.Text = "Days";
            // 
            // txtDays
            // 
            this.txtDays.AllowDecimal = false;
            this.txtDays.AllowNegative = false;
            this.txtDays.AllowSpace = false;
            this.txtDays.BackColor = System.Drawing.SystemColors.Window;
            this.txtDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDays.IsZeroWhenEmpty = true;
            this.txtDays.Location = new System.Drawing.Point(3, 29);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(48, 20);
            this.txtDays.TabIndex = 2;
            // 
            // txtMilliseconds
            // 
            this.txtMilliseconds.AllowDecimal = false;
            this.txtMilliseconds.AllowNegative = false;
            this.txtMilliseconds.AllowSpace = false;
            this.txtMilliseconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtMilliseconds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMilliseconds.IsZeroWhenEmpty = true;
            this.txtMilliseconds.Location = new System.Drawing.Point(219, 29);
            this.txtMilliseconds.Name = "txtMilliseconds";
            this.txtMilliseconds.Size = new System.Drawing.Size(51, 20);
            this.txtMilliseconds.TabIndex = 9;
            // 
            // txtSeconds
            // 
            this.txtSeconds.AllowDecimal = false;
            this.txtSeconds.AllowNegative = false;
            this.txtSeconds.AllowSpace = false;
            this.txtSeconds.BackColor = System.Drawing.SystemColors.Window;
            this.txtSeconds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSeconds.IsZeroWhenEmpty = true;
            this.txtSeconds.Location = new System.Drawing.Point(165, 29);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(48, 20);
            this.txtSeconds.TabIndex = 7;
            // 
            // txtHours
            // 
            this.txtHours.AllowDecimal = false;
            this.txtHours.AllowNegative = false;
            this.txtHours.AllowSpace = false;
            this.txtHours.BackColor = System.Drawing.SystemColors.Window;
            this.txtHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHours.IsZeroWhenEmpty = true;
            this.txtHours.Location = new System.Drawing.Point(57, 29);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(48, 20);
            this.txtHours.TabIndex = 3;
            // 
            // txtMinutes
            // 
            this.txtMinutes.AllowDecimal = false;
            this.txtMinutes.AllowNegative = false;
            this.txtMinutes.AllowSpace = false;
            this.txtMinutes.BackColor = System.Drawing.SystemColors.Window;
            this.txtMinutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMinutes.IsZeroWhenEmpty = true;
            this.txtMinutes.Location = new System.Drawing.Point(111, 29);
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(48, 20);
            this.txtMinutes.TabIndex = 5;
            // 
            // TimeSpanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TimeSpanControl";
            this.Size = new System.Drawing.Size(273, 42);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMilliseconds;
        private NumericTextBox txtMilliseconds;
        private System.Windows.Forms.Label lblSeconds;
        private NumericTextBox txtSeconds;
        private System.Windows.Forms.Label lblMinutes;
        private NumericTextBox txtMinutes;
        private System.Windows.Forms.Label lblHours;
        private NumericTextBox txtHours;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NumericTextBox txtDays;
        private System.Windows.Forms.Label lblDays;
    }
}
