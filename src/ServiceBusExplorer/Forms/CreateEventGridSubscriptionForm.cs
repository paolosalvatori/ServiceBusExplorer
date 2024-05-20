using FastColoredTextBoxNS;
using Microsoft.Azure.Amqp.Framing;
using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ServiceBusExplorer.Forms
{
    public partial class CreateEventGridSubscriptionForm : Form
    {
        #region Private Constants
        private const string ExceptionFormat = "Exception: {0}";
        private readonly WriteToLogDelegate writeToLog = default!;

        //Filter operator values
        private readonly List<bool> boolList = new List<bool> { true, false };

        #endregion

        #region Public Fields
        public string SubscriptionName;
        public string Key;
        public string Operator;
        public string Value;
        public string EventType;
        #endregion

        public CreateEventGridSubscriptionForm(WriteToLogDelegate writeToLog)
        {
            this.writeToLog = writeToLog;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SubscriptionName = txtSubscriptionName.Text;
                Key = textBoxFilterKey.Text;
                Operator = comboBoxFilterOperator.Text;
                EventType = textBoxFilterEventType.Text;

                if (comboBoxFilterValue.Visible)
                {
                    Value = comboBoxFilterValue.Text;
                }
                else
                {
                    Value = textBoxFilterValue.Text;
                }

                if (Key.Equals(String.Empty) && !Operator.Equals(String.Empty))
                {
                    throw new Exception("Key cannot be empty");
                }

                if (!Operator.Equals(String.Empty) && (textBoxFilterValue.Visible || comboBoxFilterValue.Visible) )
                {
                    if (Value.Equals(String.Empty))
                    {
                        throw new Exception("Specify a value for the operator");
                    }
                    
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void HandleException(Exception? ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }

            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex?.Message));
        }

        private void CreateEventGridSubscriptionForm_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grouperMessages_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBoxFilterOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = comboBoxFilterOperator.SelectedItem as string;

            if (selectedItem.Equals("Boolean equals"))
            {
                textBoxFilterValue.Visible = false;
                comboBoxFilterValue.Visible = true;
                comboBoxFilterValue.DataSource = boolList;
                comboBoxFilterValue.Text = string.Empty;    
            }
            else if (selectedItem.Equals("Is not null") || selectedItem.Equals("Is null or undefined"))
            {
                textBoxFilterValue.Visible = false;
                comboBoxFilterValue.Visible = false;
                comboBoxFilterValue.DataSource = null;
            }
            else
            {
                comboBoxFilterValue.Visible = false;
                textBoxFilterValue.Visible = true;
                textBoxFilterValue.Text = string.Empty;
            }
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
