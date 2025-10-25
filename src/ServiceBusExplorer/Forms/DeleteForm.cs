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

#region Using Directives

using System;
using System.Drawing;
using System.Windows.Forms;
using ServiceBusExplorer.Helpers;

#endregion

namespace ServiceBusExplorer.Forms
{
    public partial class DeleteForm : Form
    {
        #region Private Constants
        //***************************
        // Message
        //***************************
        private const string MessageFormat = "The {0} {1} will be permanently deleted.";
        private const string Unknown = "Unknown";
        #endregion

        #region Private Fields
        private TwoFilesConfiguration configuration;
        private bool useAccidentalDeletionPreventionCheck = false;
        #endregion

        #region Public Constructor
        public DeleteForm(string message)
        {
            InitializeComponent();
            lblMessage.Text = string.Format(message);
            Width = lblMessage.Width + 88;
        }

        public DeleteForm(string entityName, string entityType)
        {
            InitializeComponent();
            lblMessage.Text = string.Format(MessageFormat,
                                            entityType ?? Unknown,
                                            entityName ?? Unknown);
            Width = lblMessage.Width + 88;
        }
        #endregion

        #region Public Methods
        public void ShowAccidentalDeletionPreventionCheck(TwoFilesConfiguration configuration, string deletionScopePromptText)
        {
            this.configuration = configuration;

            bool disableAccidentalDeletionPrevention = configuration.GetBoolValue(
                ConfigurationParameters.DisableAccidentalDeletionPrevention,
                defaultValue: false);

            if (!disableAccidentalDeletionPrevention)
            {
                useAccidentalDeletionPreventionCheck = true;

                accidentalDeletionPreventionCheckControl.DeletionScopePromptText = deletionScopePromptText;
                accidentalDeletionPreventionCheckControl.Visible = true;

                accidentalDeletionPreventionCheckControl.Top = mainPanel.Bottom;
                buttonsPanel.Top = accidentalDeletionPreventionCheckControl.Bottom;

                accidentalDeletionPreventionCheckControl.Width = ClientSize.Width;

                ClientSize = new Size(ClientSize.Width, buttonsPanel.Bottom);
            }
        }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            bool acceptForm = false;

            if (useAccidentalDeletionPreventionCheck == false)
                acceptForm = true;
            else
            {
                acceptForm = accidentalDeletionPreventionCheckControl.CheckAcceptanceAndNotifyUser();

                if (accidentalDeletionPreventionCheckControl.DisableFurtherChecks)
                {
                    configuration.SetValue(
                        ConfigurationParameters.DisableAccidentalDeletionPrevention,
                        value: true);

                    configuration.Save();
                }
            }

            if (acceptForm)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                control.ForeColor = Color.White;
            }
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                control.ForeColor = SystemColors.ControlText;
            }
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(153, 180, 209), 1), 0, mainPanel.Size.Height - 1, mainPanel.Size.Width, mainPanel.Size.Height - 1);
        }
        #endregion

        public static bool ShowAndWaitUserConfirmation(IWin32Window owner, string message)
        {
            DeleteForm deleteForm = new DeleteForm(message);
            DialogResult dialogResult = deleteForm.ShowDialog(owner);

            return dialogResult == DialogResult.OK;
        }
    }
}
