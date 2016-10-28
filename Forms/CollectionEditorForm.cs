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
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class CollectionEditorForm : Form
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string DefaultLabel = "Value";
        #endregion

        #region Public Constructor
        public CollectionEditorForm(string text, string groupTitle, object value)
        {
            InitializeComponent();
            Text = text;
            Value = value;
            grouperCaption.GroupTitle = string.IsNullOrWhiteSpace(groupTitle) ? DefaultLabel : groupTitle;

            // Initialize bindingSource
            bindingSource.DataSource = Value;

            // Initialize the DataGridView.
            dataGridView.AutoGenerateColumns = true;
            dataGridView.AutoSize = true;
            dataGridView.DataSource = bindingSource;
            dataGridView.ForeColor = SystemColors.WindowText;

            //// Create the Name column
            //textBoxColumn = new DataGridViewTextBoxColumn
            //{
            //    DataPropertyName = HeaderName,
            //    Name = HeaderName,
            //    Width = 80
            //};
            //dataGridView.Columns.Add(textBoxColumn);

            //// Create the Value column
            //textBoxColumn = new DataGridViewTextBoxColumn
            //{
            //    DataPropertyName = HeaderValue,
            //    Name = HeaderValue,
            //    Width = 150
            //};
            //dataGridView.Columns.Add(textBoxColumn);

            // Set Grid style
            dataGridView.EnableHeadersVisualStyles = false;
            //dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Set the selection background color for all the cells.
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
            dataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

            // Set the background color for all rows and for alternating rows.  
            // The value for alternating rows overrides the value for all rows. 
            dataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            dataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Set the row and column header styles.
            dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            dataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
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
            Value = null;
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

        private void grouperCaption_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     dataGridView.Location.X - 1,
                                     dataGridView.Location.Y - 1,
                                     dataGridView.Size.Width + 1,
                                     dataGridView.Size.Height + 1);
        }

        private void dataGridView_Resize(object sender, EventArgs e)
        {
            CalculateColumnWidth();
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateColumnWidth();
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateColumnWidth();
        }
        #endregion

        #region Public Properties
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public object Value { get; set; }
        #endregion  

        #region Private Methods
        private void CalculateColumnWidth()
        {
            if (dataGridView.Columns.Count == 0)
            {
                return;
            }
            var width = dataGridView.Width - dataGridView.RowHeadersWidth;
            var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
            if (verticalScrollbar.Visible)
            {
                width -= verticalScrollbar.Width;
            }
            var columnWith = width/dataGridView.Columns.Count;
            for (var i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].Width = i == 0 ? columnWith + (width - (columnWith * dataGridView.Columns.Count)) : columnWith;
            }
        }
        #endregion
    }
}
