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
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public enum GetRegistrationsMethod
    {
        All,
        Tag,
        PnsHandle
    }

    public partial class RegistrationsForm : Form
    {
        #region Private Constants
        //***************************
        // Message
        //***************************
        private const string PsnHandleCannotBeNull = "The PNS handle cannot be null.";
        private const string TopMustBePositiveNumber = "TopCount paramater must be a positive number.";
        #endregion

        #region Private Fields
        private readonly WriteToLogDelegate writeToLog;
        #endregion

        #region Public Constructor
        public RegistrationsForm(WriteToLogDelegate writeToLog)
        {
            this.writeToLog = writeToLog;
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        public GetRegistrationsMethod GetRegistrationsMethod { get; set; }
        public string PnsHandle { get; private set; }
        public string TagName { get; private set; }
        public int TopCount { get; private set; }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            
            GetRegistrationsMethod = btnTag.Checked ? GetRegistrationsMethod.Tag : 
                                     btnPnsHandle.Checked ? GetRegistrationsMethod.PnsHandle : 
                                     GetRegistrationsMethod.All;
            if (GetRegistrationsMethod == GetRegistrationsMethod.PnsHandle)
            {
                if (string.IsNullOrWhiteSpace(txtPnsHandle.Text))
                {
                    txtPnsHandle.Focus();
                    writeToLog(PsnHandleCannotBeNull, false);
                    return;
                }
                PnsHandle = txtPnsHandle.Text;
            }
            else
            {
                TagName = txtTag.Text;
                int temp;
                if (int.TryParse(txtTop.Text, out temp))
                {
                    TopCount = temp;
                }
                else
                {
                    txtTop.Focus();
                    writeToLog(TopMustBePositiveNumber);
                    return;
                }
            }
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

        private void RegistrationsForm_KeyPress(object sender, KeyPressEventArgs e)
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

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            txtPnsHandle.Enabled = btnPnsHandle.Checked;
            txtTag.Enabled = btnTag.Checked;
        }
        #endregion
    }
}
