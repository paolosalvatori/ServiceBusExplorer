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
    public partial class ReceiveModeForm : Form
    {
        #region Public Constructor
        public ReceiveModeForm(string message, int count)
        {
            InitializeComponent();
            Text = message;
            txtMessageCount.Text = count.ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        #region Public Properties
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public int Count { get; private set; }
        public bool Peek { get; private set; }
        public bool All { get; private set; }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            int count;
            if (int.TryParse(txtMessageCount.Text, out count))
            {
                Count = count;
            }
            Peek = btnPeek.Checked;
            All = btnAll.Checked;
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

        private void txtMessageCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            var groupSeparator = numberFormatInfo.NumberGroupSeparator;
            var negativeSign = numberFormatInfo.NegativeSign;

            var keyInput = e.KeyChar.ToString(CultureInfo.InvariantCulture);

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
                     keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
            }
        }

        private void ReceiveModeForm_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnTop_CheckedChanged(object sender, EventArgs e)
        {
            txtMessageCount.Enabled = btnTop.Checked;
        }

        private void receiveMode_CheckedChanged(object sender, EventArgs e)
        {
            btnAll.Enabled = btnReceive.Checked;
            if (btnPeek.Checked)
            {
                btnTop.Checked = true;
            }
        }
        #endregion
    }
}
