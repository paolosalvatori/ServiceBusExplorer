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
using System.IO;
using System.Windows.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;

#endregion

namespace ServiceBusExplorer.Forms
{
    public partial class TextForm : Form
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string DefaultLabel = "Text";
        #endregion

        #region Public Constructor
        public TextForm(string label, string content)
        {
            InitializeComponent();
            grouperCaption.GroupTitle = string.IsNullOrWhiteSpace(label) ? DefaultLabel : label;
            Content = XmlHelper.Indent(content);
            txtContent.Text = string.IsNullOrWhiteSpace(Content) ? string.Empty : Content;
        }

        public TextForm(string text, string label, string content)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(text))
            {
                Text = text;
            }
            grouperCaption.GroupTitle = string.IsNullOrWhiteSpace(label) ? DefaultLabel : label;
            Content = XmlHelper.Indent(content);
            txtContent.Text = string.IsNullOrWhiteSpace(Content) ? string.Empty : Content;
        }

        public TextForm(string text, string label, string content, bool hideOpen = false)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(text))
            {
                Text = text;
            }
            btnOpen.Visible = !hideOpen;
            grouperCaption.GroupTitle = string.IsNullOrWhiteSpace(label) ? DefaultLabel : label;
            Content = XmlHelper.Indent(content);
            txtContent.Text = string.IsNullOrWhiteSpace(Content) ? string.Empty : Content;
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
            Content = null;
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
        #endregion

        #region Public Properties
        public string Content { get; set; }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        #endregion

        #region Private Methods
        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            Content = txtContent.Text;
        }

        private void TextForm_Activated(object sender, EventArgs e)
        {
            txtContent.Focus();
            txtContent.SelectionLength = 0;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.FileName = string.Empty;
                if (openFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(openFileDialog.FileName) ||
                    !File.Exists(openFileDialog.FileName))
                {
                    return;
                }
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    var text = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return;
                    }
                    txtContent.Text = text;
                }
            }
            catch (Exception ex)
            {
                MainForm.SingletonMainForm.HandleException(ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContent.Text = string.Empty;
        }
        #endregion
    }
}
