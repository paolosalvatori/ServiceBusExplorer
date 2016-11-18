#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class UploadCertificateForm : Form
    {
        #region Public Constructor
        public UploadCertificateForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        public string CertificatePath { get; set; }
        public string CertificateKey { get; set; }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            CertificatePath = txtCertificatePath.Text;
            CertificateKey = txtCertificatePassword.Text;
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

        private void UploadCertificateForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                btnOk_Click(null, null);
            }
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(153, 180, 209), 1), 0, mainPanel.Size.Height - 1, mainPanel.Size.Width, mainPanel.Size.Height - 1);
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                return;
            }
            txtCertificatePath.Text = openFileDialog.FileName;
        }
        #endregion
    }
}
