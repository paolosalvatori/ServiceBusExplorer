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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Controls
{
    public partial class HandleRuleControl : UserControl
    {   
        #region Private Constants
        //***************************
        // Formats
        //***************************
        const string ExceptionFormat = "Exception: {0}";
        const string InnerExceptionFormat = "InnerException: {0}";

        //***************************
        // Texts
        //***************************
        const string RemoveText = "Remove";
        const string AddText = "Add";
        const string RuleEntity = "RuleDescription";

        //***************************
        // Messages
        //***************************
        const string NameCannotBeNull = "The Name field cannot be null.";

        //***************************
        // Tooltips
        //***************************
        const string NameTooltip = "Gets or sets the rule name.";
        const string FilterExpressionTooltip = "Gets or sets the filter expression.";
        const string FilterActionTooltip = "Gets or sets the filter action.";
        #endregion

        #region Private Fields
        readonly RuleWrapper ruleWrapper;
        readonly ServiceBusHelper serviceBusHelper;
        readonly WriteToLogDelegate writeToLog;
        bool? isFirstRule;
        BindingList<UserPropertyWrapper> userPropertyBindingList;
        #endregion

        #region Public Constructors
        public HandleRuleControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, RuleWrapper ruleWrapper, bool? isFirstRule)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.ruleWrapper = ruleWrapper;
            this.isFirstRule = isFirstRule;
            InitializeComponent();
            InitializeControls();
            InitializeData();
        } 
        #endregion

        #region Public Events
        public event Action OnCancel;
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            // Set Grid style
            authorizationRulesDataGridView.EnableHeadersVisualStyles = false;

            // Set the selection background color for all the cells.
            authorizationRulesDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
            authorizationRulesDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            authorizationRulesDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

            // Set the background color for all rows and for alternating rows.  
            // The value for alternating rows overrides the value for all rows. 
            authorizationRulesDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            authorizationRulesDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
            //authorizationRulesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            //authorizationRulesDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Set the row and column header styles.
            authorizationRulesDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            authorizationRulesDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            authorizationRulesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            authorizationRulesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            authorizationRulesDataGridView.AutoGenerateColumns = false;
            if (authorizationRulesDataGridView.Columns.Count == 0)
            {
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Property Name", DataPropertyName = "Name" });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Value", DataPropertyName = "Value" });
            }
            authorizationRulesDataGridView_Resize(authorizationRulesDataGridView, EventArgs.Empty);
        }

        private void InitializeData()
        {
            if (ruleWrapper != null &&
                ruleWrapper.SubscriptionDescription != null &&
                ruleWrapper.RuleDescription != null)
            {
                btnCreateDelete.Text = RemoveText;
                btnCancel.Enabled = false;
                txtCreatedAt.Text = ruleWrapper.RuleDescription.CreatedAt.ToString(CultureInfo.CurrentCulture);
                txtCreatedAt.BackColor = SystemColors.Window;
                checkBoxDefault.Checked = ruleWrapper.RuleDescription.Name == RuleDescription.DefaultRuleName;
                checkBoxDefault.Enabled = false;
                checkBoxIsCorrelationFilter.Enabled = false;

                SetReadOnly(this);

                if (!string.IsNullOrWhiteSpace(ruleWrapper.RuleDescription.Name))
                {
                    txtName.Text = ruleWrapper.RuleDescription.Name;
                }

                if (ruleWrapper.RuleDescription.Filter is SqlFilter sqlFilter)
                {
                    txtFilterExpression.Text = sqlFilter.SqlExpression ?? string.Empty;
                    checkBoxIsCorrelationFilter.Checked = false;
                }

                if (ruleWrapper.RuleDescription.Filter is CorrelationFilter correlationFilter)
                {
                    var value = correlationFilter.ToString();
                    txtFilterExpression.Text = value.Replace("CorrelationFilter: ", string.Empty);
                    checkBoxIsCorrelationFilter.Checked = true;
                    txtCorrelationFilterContentType.Text = correlationFilter.ContentType;
                    txtCorrelationFilterCorrelationId.Text = correlationFilter.CorrelationId;
                    txtCorrelationFilterLabel.Text = correlationFilter.Label;
                    txtCorrelationFilterMessageId.Text = correlationFilter.MessageId;
                    txtCorrelationFilterReplyTo.Text = correlationFilter.ReplyTo;
                    txtCorrelationFilterReplyToSessionId.Text = correlationFilter.ReplyToSessionId;
                    txtCorrelationFilterSessionId.Text = correlationFilter.SessionId;
                    txtCorrelationFilterTo.Text = correlationFilter.To;

                    var wrappedUserProperties = correlationFilter.Properties.Select((prop) => new UserPropertyWrapper(prop)).ToList();
                    userPropertyBindingList = new BindingList<UserPropertyWrapper>(wrappedUserProperties)
                    {
                        AllowNew = false,
                        AllowEdit = false,
                        AllowRemove = false
                    };
                }

                if (ruleWrapper.RuleDescription.Action is SqlRuleAction action)
                {
                    txtSqlFilterAction.Text = action.SqlExpression ?? string.Empty;
                }

                toolTip.SetToolTip(txtName, NameTooltip);
                toolTip.SetToolTip(txtFilterExpression, FilterExpressionTooltip);
                toolTip.SetToolTip(txtSqlFilterAction, FilterActionTooltip);
            }
            else
            {
                btnCreateDelete.Text = AddText;
                txtCreatedAt.GotFocus += textBox_GotFocus;
                if (isFirstRule.HasValue)
                {
                    checkBoxDefault.Checked = isFirstRule.Value;
                }

                userPropertyBindingList = new BindingList<UserPropertyWrapper>()
                {
                    AllowNew = true,
                    AllowEdit = true,
                    AllowRemove = true
                };
            }
            authorizationRulesDataGridView.DataSource = userPropertyBindingList;
            txtName.Focus();
        }

        static void textBox_GotFocus(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                NativeMethods.HideCaret(textBox.Handle);
            }
        }

        void SetReadOnly(Control control)
        {
            if (control != null &&
                control.Controls.Count > 0)
            {
                for (var i = 0; i < control.Controls.Count; i++)
                {
                    if (control.Controls[i] is TextBox textBox)
                    {
                        textBox.ReadOnly = true;
                        textBox.BackColor = SystemColors.Window;
                        textBox.GotFocus += textBox_GotFocus;
                        continue;
                    }
                    SetReadOnly(control.Controls[i]);
                }
            }
        }

        void btnCreateDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusHelper == null || ruleWrapper?.SubscriptionDescription == null)
                {
                    return;
                }

                if (btnCreateDelete.Text == RemoveText 
                    && !string.IsNullOrWhiteSpace(ruleWrapper.SubscriptionDescription?.Name) 
                    && !string.IsNullOrWhiteSpace(ruleWrapper.RuleDescription?.Name))
                {
                    using (var deleteForm = new DeleteForm(ruleWrapper.RuleDescription.Name, RuleEntity.ToLower()))
                    {
                        if (deleteForm.ShowDialog() == DialogResult.OK)
                        {
                            serviceBusHelper.RemoveRule(ruleWrapper.SubscriptionDescription, ruleWrapper.RuleDescription);
                        }
                    }
                    return;
                }

                if (btnCreateDelete.Text != AddText)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    writeToLog(NameCannotBeNull);
                    return;
                }

                var ruleDescription = new RuleDescription(txtName.Text);

                if (checkBoxIsCorrelationFilter.Checked)
                {
                    var filter = new CorrelationFilter()
                    {
                        ContentType = (!string.IsNullOrWhiteSpace(txtCorrelationFilterContentType.Text) ? txtCorrelationFilterContentType.Text : null),
                        CorrelationId = (!string.IsNullOrWhiteSpace(txtCorrelationFilterCorrelationId.Text) ? txtCorrelationFilterCorrelationId.Text : null),
                        Label = (!string.IsNullOrWhiteSpace(txtCorrelationFilterLabel.Text) ? txtCorrelationFilterLabel.Text : null),
                        MessageId = (!string.IsNullOrWhiteSpace(txtCorrelationFilterMessageId.Text) ? txtCorrelationFilterMessageId.Text : null),
                        ReplyTo = (!string.IsNullOrWhiteSpace(txtCorrelationFilterReplyTo.Text) ? txtCorrelationFilterReplyTo.Text : null),
                        ReplyToSessionId = (!string.IsNullOrWhiteSpace(txtCorrelationFilterReplyToSessionId.Text) ? txtCorrelationFilterReplyToSessionId.Text : null),
                        SessionId = (!string.IsNullOrWhiteSpace(txtCorrelationFilterSessionId.Text) ? txtCorrelationFilterSessionId.Text : null),
                        To = (!string.IsNullOrWhiteSpace(txtCorrelationFilterTo.Text) ? txtCorrelationFilterTo.Text : null)
                    };
                    if (userPropertyBindingList != null)
                    {
                        foreach (var prop in userPropertyBindingList)
                        {
                            filter.Properties.Add(prop.Name, prop.Value);
                        }
                    }
                    ruleDescription.Filter = filter;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(txtFilterExpression.Text))
                    {
                        ruleDescription.Filter = new SqlFilter(txtFilterExpression.Text);
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtSqlFilterAction.Text))
                {
                    ruleDescription.Action = new SqlRuleAction(txtSqlFilterAction.Text);
                }

                ruleWrapper.RuleDescription = serviceBusHelper.AddRule(ruleWrapper.SubscriptionDescription, ruleDescription);
                InitializeData();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        void HandleException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }

            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));

            if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message))
            {
                writeToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel?.Invoke();
        }

        void checkBoxDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDefault.Checked)
            {
                txtName.Text = RuleDescription.DefaultRuleName;
            }
        }

        void checkBoxIsCorrelationFilter_CheckedChanged(object sender, EventArgs e)
        {
            grouperFilter.Visible = !checkBoxIsCorrelationFilter.Checked;
            grouperCorrelationFilter.Visible = checkBoxIsCorrelationFilter.Checked;
        }

        private void authorizationRulesDataGridView_Resize(object sender, EventArgs e)
        {
            try
            {
                authorizationRulesDataGridView.SuspendDrawing();
                authorizationRulesDataGridView.SuspendLayout();
                if (authorizationRulesDataGridView.Columns["Property Name"] == null ||
                    authorizationRulesDataGridView.Columns["Value"] == null)
                {
                    return;
                }

                var width = authorizationRulesDataGridView.Width - 24;
                var verticalScrollbar = authorizationRulesDataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                int columnWidth = width / 2;
                authorizationRulesDataGridView.Columns["Property Name"].Width = columnWidth;
                authorizationRulesDataGridView.Columns["Value"].Width = columnWidth;
            }
            finally
            {
                authorizationRulesDataGridView.ResumeLayout();
                authorizationRulesDataGridView.ResumeDrawing();
            }
        }

        private void authorizationRulesDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            authorizationRulesDataGridView_Resize(sender, null);
        }

        private void authorizationRulesDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            authorizationRulesDataGridView_Resize(sender, null);
        }

        void HandleRuleControl_Resize(object sender, EventArgs e)
        {
            var width = (Size.Width - 48) / 2;
            var height = Size.Height - 152;
            grouperFilter.Size = new Size(width, height);
            grouperAction.Size = new Size(width, height);
            grouperAction.Location = new Point(grouperFilter.Location.X + width + 16, 
                                                         grouperAction.Location.Y);
            grouperName.Size = new Size(width, grouperName.Size.Height);
            grouperCreatedAt.Size = new Size(Size.Width - grouperName.Size.Width - grouperFilterType.Size.Width - grouperIsDefault.Size.Width - 80, 
                                             grouperCreatedAt.Size.Height);
            grouperCreatedAt.Location = new Point(grouperFilter.Location.X + width + 16,
                                                  grouperCreatedAt.Location.Y);
            grouperCorrelationFilter.Size = new Size(width, height);
        }

        void button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                control.ForeColor = Color.White;
            }
        }

        void button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                control.ForeColor = SystemColors.ControlText;
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    components?.Dispose();
                }


                for (var i = 0; i < Controls.Count; i++)
                {
                    Controls[i].Dispose();
                }

                base.Dispose(disposing);
            }
            catch
            {
                // ignored
            }
        }
        #endregion
    }
}
