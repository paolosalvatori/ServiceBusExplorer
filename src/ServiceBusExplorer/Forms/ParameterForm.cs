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

namespace ServiceBusExplorer.Forms
{
    public partial class ParameterForm : Form
    {
        #region Private Fields
        private IList<bool> canBeNullList;
        private readonly IList<Control> textBoxList; 
        #endregion

        #region Public Constructor
        public ParameterForm(string title, IList<string> parameterNameList, IList<string> parameterValueList, IList<bool> canBeNullList = null)
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
            this.canBeNullList = canBeNullList;
            var labelY = 16;
            var textBoxY = 32;
            var textBoxWidth = mainPanel.Size.Width - 32;
            for (var i = 0; i < parameterNameList.Count; i++)
            {
                var parameter = parameterNameList[i];
                var label = new Label
                {
                    Name = string.Format("lbl{0}", parameter),
                    Text = string.Format("{0}:", parameter),
                    Location = new Point(16, labelY),
                    Size = new Size(400, 20)
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
                if (canBeNullList != null && 
                    parameterValueList.Count == canBeNullList.Count)
                {
                    textBox.TextChanged += textBox_TextChanged;
                }
                mainPanel.Controls.Add(textBox);
                textBox.BringToFront();
                labelY += 48;
                textBoxY += 48;
            }
            textBoxList = mainPanel.Controls.Cast<Control>().Where(c => c is TextBox).ToList();
            var delta = parameterNameList.Count*48;
            Size = new Size(Size.Width, Size.Height + delta);
            textBox_TextChanged(null, null);
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            var ok = true;
            for (var i = 0; i < textBoxList.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(textBoxList[i].Text) && !canBeNullList[i])
                {
                    ok = false;
                    break;
                }
            }
            btnOk.Enabled = ok;
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
