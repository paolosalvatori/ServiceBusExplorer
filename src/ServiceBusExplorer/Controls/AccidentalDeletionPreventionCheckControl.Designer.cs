namespace ServiceBusExplorer.Controls
{
    partial class AccidentalDeletionPreventionCheckControl
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
			this.pnlFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.lblEntityName = new System.Windows.Forms.Label();
			this.txtEntityNameCheck = new System.Windows.Forms.TextBox();
			this.lblExplanation = new System.Windows.Forms.Label();
			this.pnlFlowLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlFlowLayout
			// 
			this.pnlFlowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlFlowLayout.AutoSize = true;
			this.pnlFlowLayout.Controls.Add(this.lblMessage);
			this.pnlFlowLayout.Controls.Add(this.lblEntityName);
			this.pnlFlowLayout.Controls.Add(this.txtEntityNameCheck);
			this.pnlFlowLayout.Controls.Add(this.lblExplanation);
			this.pnlFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlFlowLayout.Location = new System.Drawing.Point(0, 0);
			this.pnlFlowLayout.Name = "pnlFlowLayout";
			this.pnlFlowLayout.Size = new System.Drawing.Size(484, 100);
			this.pnlFlowLayout.TabIndex = 3;
			this.pnlFlowLayout.SizeChanged += new System.EventHandler(this.pnlFlowLayout_SizeChanged);
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(3, 8);
			this.lblMessage.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(362, 13);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "To prevent accidental deletion, please type the name of item being deleted:";
			// 
			// lblEntityName
			// 
			this.lblEntityName.AutoSize = true;
			this.lblEntityName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblEntityName.ForeColor = System.Drawing.Color.Red;
			this.lblEntityName.Location = new System.Drawing.Point(30, 29);
			this.lblEntityName.Margin = new System.Windows.Forms.Padding(30, 8, 3, 0);
			this.lblEntityName.Name = "lblEntityName";
			this.lblEntityName.Size = new System.Drawing.Size(84, 14);
			this.lblEntityName.TabIndex = 1;
			this.lblEntityName.Text = "Entity Name";
			// 
			// txtEntityNameCheck
			// 
			this.txtEntityNameCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtEntityNameCheck.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEntityNameCheck.ForeColor = System.Drawing.Color.Red;
			this.txtEntityNameCheck.Location = new System.Drawing.Point(30, 46);
			this.txtEntityNameCheck.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
			this.txtEntityNameCheck.Name = "txtEntityNameCheck";
			this.txtEntityNameCheck.Size = new System.Drawing.Size(419, 22);
			this.txtEntityNameCheck.TabIndex = 2;
			// 
			// lblExplanation
			// 
			this.lblExplanation.AutoSize = true;
			this.lblExplanation.Location = new System.Drawing.Point(3, 79);
			this.lblExplanation.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
			this.lblExplanation.Name = "lblExplanation";
			this.lblExplanation.Size = new System.Drawing.Size(473, 13);
			this.lblExplanation.TabIndex = 3;
			this.lblExplanation.Text = "If you type the full name of the item, then that item and all of its children wil" +
    "l be permanently deleted.";
			// 
			// AccidentalDeletionPreventionCheckControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Cornsilk;
			this.Controls.Add(this.pnlFlowLayout);
			this.Name = "AccidentalDeletionPreventionCheckControl";
			this.Size = new System.Drawing.Size(484, 102);
			this.pnlFlowLayout.ResumeLayout(false);
			this.pnlFlowLayout.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlFlowLayout;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.TextBox txtEntityNameCheck;
        private System.Windows.Forms.Label lblExplanation;
    }
}
