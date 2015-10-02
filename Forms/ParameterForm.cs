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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class ParameterForm : Form
    {
        #region Public Constructor
        public ParameterForm(string title, IList<string> parameterNameList, IList<string> parameterValueList)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(title))
            {
                Text = title;
            }
            if (parameterNameList == null)
            {
                return;
            }
            var labelY = 16;
            var textBoxY = 32;
            var textBoxWidth = mainPanel.Size.Width - 32;
            for (var i = 0; i < parameterNameList.Count; i++)
            {
                var parameter = parameterNameList[i];
                var label = new Label
                {
                    Name = string.Format("lbl{0}:", parameter),
                    Text = parameter,
                    Location = new Point(16, labelY)
                };
                mainPanel.Controls.Add(label);
                var textBox = new TextBox
                {
                    Name = string.Format("txt{0}", parameter),
                    AutoSize = false,
                    Size = new Size(textBoxWidth, 24),
                    Location = new Point(16, textBoxY),
                    Tag = i
                };
                if (parameterNameList.Count == parameterValueList.Count)
                {
                    textBox.Text = parameterValueList[i];
                }
                mainPanel.Controls.Add(textBox);
                textBox.BringToFront();
                labelY += 48;
                textBoxY += 48;
            }
            var delta = parameterNameList.Count*48;
            Size = new Size(Size.Width, Size.Height + delta);
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        #endregion

        #region Public Properties
        public IList<string> ParameterValues { get; set; }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            ParameterValues = mainPanel.Controls.Cast<Control>().Where(c => c is TextBox).OrderBy(c => c.Tag).Select(t => t.Text).ToList();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            ParameterValues = null;
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
        #endregion
    }
}
