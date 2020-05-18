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
			this.lblDeletionScopePromptText = new System.Windows.Forms.Label();
			this.txtDeletionScopePromptCheck = new System.Windows.Forms.TextBox();
			this.lblExplanation = new System.Windows.Forms.Label();
			this.chkDisableAccidentalDeletionPrevention = new System.Windows.Forms.CheckBox();
			this.pnlFlowLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlFlowLayout
			// 
			this.pnlFlowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlFlowLayout.AutoSize = true;
			this.pnlFlowLayout.Controls.Add(this.lblMessage);
			this.pnlFlowLayout.Controls.Add(this.lblDeletionScopePromptText);
			this.pnlFlowLayout.Controls.Add(this.txtDeletionScopePromptCheck);
			this.pnlFlowLayout.Controls.Add(this.lblExplanation);
			this.pnlFlowLayout.Controls.Add(this.chkDisableAccidentalDeletionPrevention);
			this.pnlFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlFlowLayout.Location = new System.Drawing.Point(0, 0);
			this.pnlFlowLayout.Name = "pnlFlowLayout";
			this.pnlFlowLayout.Size = new System.Drawing.Size(484, 141);
			this.pnlFlowLayout.TabIndex = 3;
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(3, 8);
			this.lblMessage.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(478, 13);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "To prevent accidental deletion of multiple items, please type the description of " +
    "what is being deleted:";
			// 
			// lblDeletionScopePromptText
			// 
			this.lblDeletionScopePromptText.AutoSize = true;
			this.lblDeletionScopePromptText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDeletionScopePromptText.ForeColor = System.Drawing.Color.Red;
			this.lblDeletionScopePromptText.Location = new System.Drawing.Point(30, 29);
			this.lblDeletionScopePromptText.Margin = new System.Windows.Forms.Padding(30, 8, 3, 0);
			this.lblDeletionScopePromptText.Name = "lblDeletionScopePromptText";
			this.lblDeletionScopePromptText.Size = new System.Drawing.Size(98, 14);
			this.lblDeletionScopePromptText.TabIndex = 1;
			this.lblDeletionScopePromptText.Text = "Deletion Type";
			// 
			// txtDeletionScopePromptCheck
			// 
			this.txtDeletionScopePromptCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDeletionScopePromptCheck.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDeletionScopePromptCheck.ForeColor = System.Drawing.Color.Red;
			this.txtDeletionScopePromptCheck.Location = new System.Drawing.Point(30, 46);
			this.txtDeletionScopePromptCheck.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
			this.txtDeletionScopePromptCheck.Name = "txtDeletionScopePromptCheck";
			this.txtDeletionScopePromptCheck.Size = new System.Drawing.Size(424, 22);
			this.txtDeletionScopePromptCheck.TabIndex = 2;
			// 
			// lblExplanation
			// 
			this.lblExplanation.AutoSize = true;
			this.lblExplanation.Location = new System.Drawing.Point(3, 79);
			this.lblExplanation.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
			this.lblExplanation.Name = "lblExplanation";
			this.lblExplanation.Size = new System.Drawing.Size(448, 26);
			this.lblExplanation.TabIndex = 3;
			this.lblExplanation.Text = "If you type the full text shown, then the selected item and all of its children w" +
    "ill be permanently deleted.";
			// 
			// chkDisableAccidentalDeletionPrevention
			// 
			this.chkDisableAccidentalDeletionPrevention.AutoSize = true;
			this.chkDisableAccidentalDeletionPrevention.Location = new System.Drawing.Point(8, 116);
			this.chkDisableAccidentalDeletionPrevention.Margin = new System.Windows.Forms.Padding(8, 3, 3, 8);
			this.chkDisableAccidentalDeletionPrevention.Name = "chkDisableAccidentalDeletionPrevention";
			this.chkDisableAccidentalDeletionPrevention.Size = new System.Drawing.Size(256, 17);
			this.chkDisableAccidentalDeletionPrevention.TabIndex = 4;
			this.chkDisableAccidentalDeletionPrevention.Text = "Delete multiple items without this check next time";
			this.chkDisableAccidentalDeletionPrevention.UseVisualStyleBackColor = true;
			// 
			// AccidentalDeletionPreventionCheckControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Cornsilk;
			this.Controls.Add(this.pnlFlowLayout);
			this.Name = "AccidentalDeletionPreventionCheckControl";
			this.Size = new System.Drawing.Size(484, 144);
			this.pnlFlowLayout.ResumeLayout(false);
			this.pnlFlowLayout.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlFlowLayout;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblDeletionScopePromptText;
        private System.Windows.Forms.TextBox txtDeletionScopePromptCheck;
        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.CheckBox chkDisableAccidentalDeletionPrevention;
    }
}
