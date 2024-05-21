using FastColoredTextBoxNS;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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
        public List<Dictionary<string, string>> filterList = new List<Dictionary<string, string>>();
        public List<string> eventTypesList = new List<string>();
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

                if (!IsValidSubscriptionName(SubscriptionName))
                {
                    throw new Exception("Your Event Subscription name is not in a supported format - it can only contain letters, numbers, and dashes.");
                }
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

                var filter = new Dictionary<string, string>();
                filter["Key"] = Key;
                filter["Operator"] = Operator;
                filter["Value"] = Value;

                if (!filter["Key"].Equals(String.Empty))
                {
                    filterList.Add(filter);
                }

                Key = String.Empty;
                Operator = String.Empty;
                Value = String.Empty;

                if (!EventType.Equals(String.Empty))
                {
                    eventTypesList.Add(EventType);
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            DialogResult = DialogResult.OK;
        }

        private void addFilter_Click(object sender, EventArgs e)
        {
            try
            {

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

                if (!Operator.Equals(String.Empty) && (textBoxFilterValue.Visible || comboBoxFilterValue.Visible))
                {
                    if (Value.Equals(String.Empty))
                    {
                        throw new Exception("Specify a value for the operator");
                    }

                }

                SubscriptionName = txtSubscriptionName.Text;
                Key = textBoxFilterKey.Text;
                Operator = comboBoxFilterOperator.Text;
                EventType = textBoxFilterEventType.Text;
                txtSubscriptionName.Enabled = false;

                var filter = new Dictionary<string, string>();
                filter["Key"] = Key;
                filter["Operator"] = Operator;
                filter["Value"] = Value;

                if (!filter["Key"].Equals(String.Empty))
                {
                    filterList.Add(filter);
                }


                if (!EventType.Equals(String.Empty))
                {
                    eventTypesList.Add(EventType);
                }

                if (filterList.Count >= 25)
                {
                    btnAddNewFilter.Enabled = false;
                }

                Key = String.Empty;
                Operator = String.Empty;
                Value = String.Empty;
                textBoxFilterKey.Text = String.Empty;
                comboBoxFilterOperator.Text = String.Empty;
                comboBoxFilterValue.Text = String.Empty;
                textBoxFilterEventType.Text = String.Empty;
                textBoxFilterValue.Text = String.Empty;

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

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

        private void comboBoxFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = comboBoxFilterValue.SelectedItem as string;
            if (!selectedItem.Equals(String.Empty) && comboBoxFilterValue.Visible && !textBoxFilterValue.Visible)
            {
                btnAddNewFilter.Enabled = true;
                comboBoxFilterValue.SelectedItem = String.Empty;
                comboBoxFilterValue.Text = String.Empty;
            }
        }

        private void comboBoxFilterOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddNewFilter.Enabled = false;
            var selectedItem = comboBoxFilterOperator.SelectedItem as string;

            if (selectedItem.Equals("Boolean equals"))
            {
                textBoxFilterValue.Visible = false;
                comboBoxFilterValue.Visible = true;
                comboBoxFilterValue.SelectedItem = String.Empty;
                comboBoxFilterValue.DataSource = boolList; 
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

            if (!textBoxFilterKey.Text.Equals(String.Empty) && !comboBoxFilterValue.Visible && !textBoxFilterValue.Visible)
            {
                btnAddNewFilter.Enabled = true;
            }
        }

        public static Boolean IsValidSubscriptionName(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9-]+$");
            return rg.IsMatch(strToCheck);
        }

        private void textBoxFilterValue_TextChanged(object sender, EventArgs e)
        {
            btnAddNewFilter.Enabled = false;

            if (!textBoxFilterKey.Text.Equals(String.Empty) && !comboBoxFilterValue.Visible && textBoxFilterValue.Visible)
            {
                btnAddNewFilter.Enabled = true;
            }
        }

        private Boolean IsValueFieldValid()
        {
            if (textBoxFilterValue.Visible && textBoxFilterValue.Text.Equals(String.Empty))
            {
                return false;
            }

            if (comboBoxFilterValue.Visible && comboBoxFilterOperator.Text.Equals(String.Empty))
            {
               return false;
            }

            return true;
        }

        private void textBoxFilterKey_TextChanged(object sender, EventArgs e)
        {
            btnAddNewFilter.Enabled = false;

            if (!textBoxFilterKey.Text.Equals(String.Empty) && !comboBoxFilterOperator.Text .Equals(String.Empty) && IsValueFieldValid())
            {
                btnAddNewFilter.Enabled = true;
            }
        }
    }
}
