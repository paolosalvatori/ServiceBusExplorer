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

#nullable enable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer.Forms
{
    public partial class ReceiveModeForm : Form
    {
        #region Private Constants
        //***************************
        // Messages
        //***************************
        const string SelectBrokeredMessageInspector = "Select a BrokeredMessage inspector...";
        #endregion

        #region Public Constructor
        public ReceiveModeForm(string message, int count, IEnumerable<string> brokeredMessageInspectors, bool fromSessionSelectionActive = false)
        {
            InitializeComponent();
            Text = message;
            txtMessageCount.Text = count.ToString(CultureInfo.InvariantCulture);
            cboReceiverInspector.Items.Add(SelectBrokeredMessageInspector);
            cboReceiverInspector.SelectedIndex = 0;
            var messageInspectors = brokeredMessageInspectors as string[] ?? brokeredMessageInspectors.ToArray();
            if (!messageInspectors.Any())
            {
                return;
            }
            for (var i = 0; i < messageInspectors.Length; i++)
            {
                cboReceiverInspector.Items.Add(messageInspectors[i]);
            }

            txtFromSession.Enabled = fromSessionSelectionActive;
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
        public string? Inspector { get; private set; }
        public long? FromSequenceNumber { get; private set; }
        public string? FromSession { get; private set; }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object? sender, EventArgs? e)
        {
            DialogResult = DialogResult.OK;
            if (int.TryParse(txtMessageCount.Text, out var count))
            {
                Count = count;
            }
            Peek = btnPeek.Checked;
            All = btnAll.Checked;
            if (cboReceiverInspector.SelectedIndex > 0)
            {
                Inspector = cboReceiverInspector.Text;
            }

            if (txtFromSequenceNumber.Enabled && !string.IsNullOrEmpty(txtFromSequenceNumber.Text))
            {
                if (long.TryParse(txtFromSequenceNumber.Text, out var fromSequenceNumber))
                {
                    FromSequenceNumber = fromSequenceNumber;
                }
            }

            if (!string.IsNullOrEmpty(txtFromSession.Text))
            {
                FromSession = txtFromSession.Text;
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

            txtFromSequenceNumber.Enabled = btnPeek.Checked;
        }
        
        private void grouperInspector_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboReceiverInspector.Location.X - 1,
                                    cboReceiverInspector.Location.Y - 1,
                                    cboReceiverInspector.Size.Width + 1,
                                    cboReceiverInspector.Size.Height + 1);
        }
        #endregion
    }
}
