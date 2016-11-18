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
using System.IO;
using System.Windows.Forms;
using System.Drawing;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
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
