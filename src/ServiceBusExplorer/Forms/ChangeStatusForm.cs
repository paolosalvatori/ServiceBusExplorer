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
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Forms
{
    public partial class ChangeStatusForm : Form
    {
        #region Private Constants
        //***************************
        // Message
        //***************************
        private const string EnableFormat = "This operation will enable the {0} {1}. Do you want to continue?";
        private const string DisableFormat = "This operation will disable the {0} {1}. Do you want to continue?";
        private const string SetStatusFormat = "This operation will set the status of the {0} {1} to {2}. Do you want to continue?";
        private const string Unknown = "Unknown";
        #endregion

        #region Public Constructor
        public ChangeStatusForm(string entityName, string entityType, EntityStatus desiredStatus)
        {
            InitializeComponent();
            
            if (desiredStatus == EntityStatus.Active)
            {
                lblMessage.Text = string.Format(EnableFormat, entityType ?? Unknown, entityName ?? Unknown);
            }
            else if (desiredStatus == EntityStatus.Disabled)
            {
                lblMessage.Text = string.Format(DisableFormat, entityType ?? Unknown, entityName ?? Unknown);
            }
            else
            {
                lblMessage.Text = string.Format(SetStatusFormat, entityType ?? Unknown, entityName ?? Unknown, desiredStatus.ToString());
            }
            
            Width = lblMessage.Width + 72;
        }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
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
        
        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(153, 180, 209), 1), 0, mainPanel.Size.Height - 1, mainPanel.Size.Width, mainPanel.Size.Height - 1);
        }
        #endregion 
    }
}
