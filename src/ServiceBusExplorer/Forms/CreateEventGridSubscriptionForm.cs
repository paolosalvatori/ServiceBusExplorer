using Azure.ResourceManager.EventGrid.Models;
using FastColoredTextBoxNS;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Markup;

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
        public List<string> OperatorsThatTakeInString = new List<string>
        {
            "String is in",
            "String is not in",
            "String contains",
            "String does not contain",
            "String begins with",
            "String does not begin with",
            "String ends with",
            "String does not end with",
            "Number is in",
            "Number is not in",
        };

        public List<string> OperatorsThatTakeInIntValue = new List<string>
        {
            "Number is less than",
            "Number is greater than",
            "Number is less than or equal to",
            "Number is greater than or equal to"
        };

        public List<string> OperatorsThatTakeIntRange = new List<string>
        {
            "Number is in range",
            "Number is not in range",
        };

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
                if (comboBoxFilterValue.Visible)
                {
                    Value = comboBoxFilterValue.Text;
                }
                else
                {
                    Value = textBoxFilterValue.Text;
                }

                if (IsValidSubscriptionName(SubscriptionName))
                {
                    if (string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Operator))
                    {
                        throw new Exception("Key cannot be empty");
                    }

                    if (!string.IsNullOrEmpty(Operator) && (textBoxFilterValue.Visible || comboBoxFilterValue.Visible) && string.IsNullOrEmpty(Value))
                    {
                        throw new Exception("Specify a value for the operator");
                    }

                    if (IsValueFieldInputValid())
                    {
                        var filter = new Dictionary<string, string>();
                        filter["Key"] = Key;
                        filter["Operator"] = Operator;
                        filter["Value"] = Value;

                        if (!string.IsNullOrEmpty(filter["Key"]))
                        {
                            filterList.Add(filter);
                            writeToLog(string.Format(CultureInfo.CurrentCulture, $"Successfully added filter with key: {Key}, Operator: {Operator}, and Value(s): {Value}"));
                        }

                        Key = String.Empty;
                        Operator = String.Empty;
                        Value = String.Empty;

                        if (!string.IsNullOrEmpty(EventType))
                        {
                            eventTypesList.Add(EventType);
                            writeToLog(string.Format(CultureInfo.CurrentCulture, $"Successfully added EventType {EventType}"));
                        }
                    }
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void addFilter_Click(object sender, EventArgs e)
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

                if (IsValidSubscriptionName(SubscriptionName))
                {
                    txtSubscriptionName.Enabled = false;

                    if (string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Operator))
                    {
                        throw new Exception("Key cannot be empty");
                    }

                    if (!string.IsNullOrEmpty(Operator) && (textBoxFilterValue.Visible || comboBoxFilterValue.Visible) && string.IsNullOrEmpty(Value))
                    {
                        throw new Exception("Specify a value for the operator");
                    }

                    if (IsValueFieldInputValid())
                    {
                        var filter = new Dictionary<string, string>();
                        filter["Key"] = Key;
                        filter["Operator"] = Operator;
                        filter["Value"] = Value;

                        if (!string.IsNullOrEmpty(filter["Key"]))
                        {
                            filterList.Add(filter);
                            writeToLog(string.Format(CultureInfo.CurrentCulture, $"Successfully added filter with key: {Key}, Operator: {Operator}, and Value(s): {Value}"));
                        }


                        if (!string.IsNullOrEmpty(EventType))
                        {
                            eventTypesList.Add(EventType);
                            writeToLog(string.Format(CultureInfo.CurrentCulture, $"Successfully added EventType {EventType}"));
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
                }

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

        private void HandleException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }

            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex?.Message));
        }

        private void comboBoxFilterValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddNewFilter.Enabled = true;
        }

        private void comboBoxFilterOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddNewFilter.Enabled = false;
            var selectedItem = comboBoxFilterOperator.SelectedItem as string;
            Operator = selectedItem;

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

            if (!string.IsNullOrEmpty(textBoxFilterKey.Text) && !comboBoxFilterValue.Visible && !textBoxFilterValue.Visible)
            {
                btnAddNewFilter.Enabled = true;
            }
        }

        public static Boolean IsValidSubscriptionName(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9-]+$");
            if (!rg.IsMatch(strToCheck))
            {
                MessageBox.Show("Your Event Subscription name is not in a supported format - it can only contain letters, numbers, and dashes.");
                return false;
            }
            return true;
        }

        private void textBoxFilterValue_TextChanged(object sender, EventArgs e)
        {
            btnAddNewFilter.Enabled = false;
            if (!string.IsNullOrEmpty(textBoxFilterKey.Text) && !comboBoxFilterValue.Visible && textBoxFilterValue.Visible)
            {
                btnAddNewFilter.Enabled = true;
            }
            
        }
        private Boolean IsValueFieldInputValid()
        {
            if (OperatorsThatTakeInIntValue.Contains(Operator))
            {
                if (Regex.IsMatch(textBoxFilterValue.Text, "[^0-9]"))
                {
                    MessageBox.Show("With selected operator, value can only be numbers. Please enter only numbers.");
                    textBoxFilterValue.Clear();
                    return false;
                }
            }
            else if (OperatorsThatTakeIntRange.Contains(Operator))
            {
                string[] rangeList = textBoxFilterValue.Text.Split(',');

                if (rangeList.Length > 25)
                {
                    MessageBox.Show("You have inputted more than the maximum value count. No more then 25 values are allowed.");
                    return false;
                }
                foreach (string i in rangeList)
                {
                    if (!Regex.IsMatch(i, "\\d+-\\d+"))
                    {
                        MessageBox.Show("With selected operator, value should be in the format integer-integer, separated by commas to add more values.");
                        return false;
                    }
                }
                
            }
            else if (OperatorsThatTakeInString.Contains(Operator))
            {
                string[] valuesList = textBoxFilterValue.Text.Split(',');

                if (valuesList.Length > 25)
                {
                    MessageBox.Show("You  have inputted more than the maximum value count. No more then 25 values are allowed.");
                    return false;
                }

            }
            Regex.Replace(Value, @"\s", "");
            return true;
        }

        private Boolean IsValueFieldValid()
        {
            if (textBoxFilterValue.Visible && string.IsNullOrEmpty(textBoxFilterValue.Text))
            {
                return false;
            }

            if (comboBoxFilterValue.Visible && string.IsNullOrEmpty(comboBoxFilterOperator.Text))
            {
               return false;
            }

            return true;
        }

        private void textBoxFilterKey_TextChanged(object sender, EventArgs e)
        {
            btnAddNewFilter.Enabled = false;

            if (!string.IsNullOrEmpty(textBoxFilterKey.Text) && !string.IsNullOrEmpty(comboBoxFilterOperator.Text) && IsValueFieldValid())
            {
                btnAddNewFilter.Enabled = true;
            }
        }
    }
}
