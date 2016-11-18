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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class HandleTopicControl : UserControl
    {
        #region DllImports
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        #endregion

        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string SizeInGigabytes = "{0} GB";

        //***************************
        // Indexes
        //***************************
        private const int EnableBatchedOperationsIndex = 0;
        private const int EnableFilteringMessagesBeforePublishingIndex = 1;
        private const int RequiresDuplicateDetectionIndex = 2;
        private const int SupportOrderingIndex = 3;
        private const int IsAnonymousAccessibleIndex = 4;

        //***************************
        // Texts
        //***************************
        private const string DeleteText = "Delete";
        private const string CreateText = "Create";
        private const string UpdateText = "Update";
        private const string CancelText = "Cancel";
        private const string EnableText = "Enable";
        private const string DisableText = "Disable";
        private const string TopicEntity = "TopicDescription";
        private const string UserMetadata = "User Metadata";
        private const string MaxGigabytes = "MAX";

        //***************************
        // Messages
        //***************************
        private const string PathCannotBeNull = "The Path field cannot be null.";
        private const string DefaultMessageTimeToLiveDaysMustBeANumber = "The Days value of the DefaultMessageTimeToLive field must be a number.";
        private const string DefaultMessageTimeToLiveHoursMustBeANumber = "The Hours value of the DefaultMessageTimeToLive field must be a number.";
        private const string DefaultMessageTimeToLiveMinutesMustBeANumber = "The Minutes value of the DefaultMessageTimeToLive field must be a number.";
        private const string DefaultMessageTimeToLiveSecondsMustBeANumber = "The Seconds value of the DefaultMessageTimeToLive field must be a number.";
        private const string DefaultMessageTimeToLiveMillisecondsMustBeANumber = "The Milliseconds value of the DefaultMessageTimeToLive field must be a number.";

        private const string DuplicateDetectionHistoryTimeWindowDaysMustBeANumber = "The Days value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string DuplicateDetectionHistoryTimeWindowHoursMustBeANumber = "The Hours value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string DuplicateDetectionHistoryTimeWindowMinutesMustBeANumber = "The Minutes value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string DuplicateDetectionHistoryTimeWindowSecondsMustBeANumber = "The Seconds value of the DuplicateDetectionHistoryTimeWindow field must be a number.";
        private const string DuplicateDetectionHistoryTimeWindowMillisecondsMustBeANumber = "The Milliseconds value of the DuplicateDetectionHistoryTimeWindow field must be a number.";

        private const string AutoDeleteOnIdleDaysMustBeANumber = "The Days value of the AutoDeleteOnIdle field must be a number.";
        private const string AutoDeleteOnIdleHoursMustBeANumber = "The Hours value of the AutoDeleteOnIdle field must be a number.";
        private const string AutoDeleteOnIdleMinutesMustBeANumber = "The Minutes value of the AutoDeleteOnIdle field must be a number.";
        private const string AutoDeleteOnIdleSecondsMustBeANumber = "The Seconds value of the AutoDeleteOnIdle field must be a number.";
        private const string AutoDeleteOnIdleMillisecondsMustBeANumber = "The Milliseconds value of the AutoDeleteOnIdle field must be a number.";
        private const string AuthorizationRuleDeleteMessage = "The Authorization Rule will be permanently deleted";

        private const string KeyNameCannotBeNull = "Authorization Rule [{0}]: the KeyName cannot be null";
        private const string PrimaryKeyCannotBeNull = "Authorization Rule [{0}]: the PrimaryKey cannot be null";

        //***************************
        // Tooltips
        //***************************
        private const string PathTooltip = "Gets or sets the topic path.";
        private const string MaxTopicSizeTooltip = "Gets or sets the maximum topic size in gigabytes.";
        private const string DefaultMessageTimeToLiveTooltip = "Gets or sets the default message time to live of a topic.";
        private const string DuplicateDetectionHistoryTimeWindowTooltip = "Gets or sets the duration of the time window for duplicate detection history.";
        private const string AutoDeleteOnIdleTooltip = "Gets or sets the maximum period of idleness after which the queue is auto deleted.";
        private const string UserMetadataTooltip = "Gets or sets the user metadata.";

        //***************************
        // Property Labels
        //***************************
        private const string Status = "Status";
        private const string SizeInBytes = "Size In Bytes";
        private const string CreatedAt = "Created At";
        private const string AccessedAt = "Accessed At";
        private const string UpdatedAt = "Updated At";
        private const string ActiveMessageCount = "Active Message Count";
        private const string DeadletterMessageCount = "DeadLetter Message Count";
        private const string ScheduledMessageCount = "Scheduled Message Count";
        private const string TransferMessageCount = "Transfer Message Count";
        private const string TransferDeadLetterMessageCount = "Transfer DL Message Count";
        private const string IsReadOnly = "IsReadOnly";

        //***************************
        // Constants
        //***************************
        private const long SeviceBusForWindowsServerMaxTopicSize = 8796093022207;

        //***************************
        // Pages
        //***************************
        private const string MetricsTabPage = "tabPageMetrics";

        //***************************
        // Metrics Formats
        //***************************
        private const string MetricTabPageKeyFormat = "MetricTabPage{0}";
        private const string GrouperFormat = "Metric: [{0}] Unit: [{1}]";

        //***************************
        // Metrics Constants
        //***************************
        private const string MetricProperty = "Metric";
        private const string GranularityProperty = "Granularity";
        private const string TimeFilterOperator = "Operator";
        private const string TimeFilterValue = "Value";
        private const string TimeFilterOperator1Name = "FilterOperator1";
        private const string TimeFilterOperator2Name = "FilterOperator2";
        private const string TimeFilterValue1Name = "FilterValue1";
        private const string TimeFilterValue2Name = "FilterValue2";
        private const string FriendlyNameProperty = "FriendlyName";
        private const string NameProperty = "Name";
        private const string MetricsTopicEntity = "Topic";
        private const string Unknown = "Unkown";
        #endregion

        #region Private Fields
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private TopicDescription topicDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly string path;
        private readonly BindingSource dataPointBindingSource = new BindingSource();
        private readonly BindingList<MetricDataPoint> dataPointBindingList;
        private int tabIndex;
        #endregion

        #region Private Static Fields
        private static readonly List<string> claimTypes = new List<string> { "NameIdentifier", "Upn", "Role", "SharedAccessKey" };
        private static readonly List<string> operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> timeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };
        #endregion

        #region Public Constructors
        public HandleTopicControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, TopicDescription topicDescription, string path)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.topicDescription = topicDescription;
            this.path = path;
            dataPointBindingList = new BindingList<MetricDataPoint>
            {
                AllowNew = true,
                AllowEdit = true,
                AllowRemove = true
            };
            InitializeComponent();
            InitializeControls();
        } 
        #endregion

        #region Public Events
        public event Action OnCancel;
        public event Action OnRefresh;
        public event Action OnChangeStatus;
        #endregion

        #region Public Methods
        public void RefreshData(TopicDescription topic)
        {
            try
            {
                topicDescription = topic;
                InitializeData();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            trackBarMaxTopicSize.Maximum = serviceBusHelper.IsCloudNamespace ? 5 : 11;

            // IsAnonymousAccessible
            if (serviceBusHelper.IsCloudNamespace)
            {
                if (checkedListBox.Items.Count > IsAnonymousAccessibleIndex)
                {
                    checkedListBox.Items.RemoveAt(IsAnonymousAccessibleIndex);
                }
            }

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
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "IssuerName", DataPropertyName = "IssuerName" });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewComboBoxColumn { Name = "ClaimType", DataPropertyName = "ClaimType", DataSource = claimTypes, FlatStyle = FlatStyle.Flat });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ClaimValue", DataPropertyName = "ClaimValue" });
                if (serviceBusHelper.IsCloudNamespace)
                {
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "KeyName", DataPropertyName = "KeyName" });
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrimaryKey", DataPropertyName = "PrimaryKey" });
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "SecondaryKey", DataPropertyName = "SecondaryKey" });
                }
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Manage", DataPropertyName = "Manage", Width = 50 });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Send", DataPropertyName = "Send", Width = 50 });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Listen", DataPropertyName = "Listen", Width = 50 });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Revision", DataPropertyName = "Revision", Width = 50, ReadOnly = true });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "CreatedTime", DataPropertyName = "CreatedTime", ReadOnly = true });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ModifiedTime", DataPropertyName = "ModifiedTime", ReadOnly = true });
            }

            // Set Grid style
            dataPointDataGridView.EnableHeadersVisualStyles = false;

            // Set the selection background color for all the cells.
            dataPointDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
            dataPointDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            dataPointDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

            // Set the background color for all rows and for alternating rows.  
            // The value for alternating rows overrides the value for all rows. 
            dataPointDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            dataPointDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
            //filtersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            //filtersDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Set the row and column header styles.
            dataPointDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            dataPointDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            dataPointDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            dataPointDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Initialize the DataGridView.
            dataPointBindingSource.DataSource = dataPointBindingList;
            dataPointDataGridView.AutoGenerateColumns = false;
            dataPointDataGridView.AutoSize = true;
            dataPointDataGridView.DataSource = dataPointBindingSource;
            dataPointDataGridView.ForeColor = SystemColors.WindowText;

            if (dataPointDataGridView.Columns.Count == 0)
            {
                // Create the Metric column
                var metricColumn = new DataGridViewComboBoxColumn
                    {
                        DataSource = MetricInfo.MetricInfos,
                        DataPropertyName = MetricProperty,
                        DisplayMember = FriendlyNameProperty,
                        ValueMember = NameProperty,
                        Name = MetricProperty,
                        Width = 144,
                        FlatStyle = FlatStyle.Flat,
                        DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                    };
                dataPointDataGridView.Columns.Add(metricColumn);

                // Create the Time Granularity column
                var timeGranularityColumn = new DataGridViewComboBoxColumn
                    {
                        DataSource = timeGranularityList,
                        DataPropertyName = GranularityProperty,
                        Name = GranularityProperty,
                        Width = 72,
                        FlatStyle = FlatStyle.Flat
                    };
                dataPointDataGridView.Columns.Add(timeGranularityColumn);

                // Create the Time Operator 1 column
                var operator1Column = new DataGridViewComboBoxColumn
                    {
                        DataSource = operators,
                        DataPropertyName = TimeFilterOperator1Name,
                        HeaderText = TimeFilterOperator,
                        Name = TimeFilterOperator1Name,
                        Width = 72,
                        FlatStyle = FlatStyle.Flat
                    };
                dataPointDataGridView.Columns.Add(operator1Column);

                // Create the Time Value 1 column
                var value1Column = new DataGridViewDateTimePickerColumn
                    {
                        DataPropertyName = TimeFilterValue1Name,
                        HeaderText = TimeFilterValue,
                        Name = TimeFilterValue1Name,
                        Width = 136
                    };
                dataPointDataGridView.Columns.Add(value1Column);

                // Create the Time Operator 1 column
                var operator2Column = new DataGridViewComboBoxColumn
                    {
                        DataSource = operators,
                        DataPropertyName = TimeFilterOperator2Name,
                        HeaderText = TimeFilterOperator,
                        Name = TimeFilterOperator2Name,
                        Width = 72,
                        FlatStyle = FlatStyle.Flat
                    };
                dataPointDataGridView.Columns.Add(operator2Column);

                // Create the Time Value 1 column
                var value2Column = new DataGridViewDateTimePickerColumn
                    {
                        DataPropertyName = TimeFilterValue2Name,
                        HeaderText = TimeFilterValue,
                        Name = TimeFilterValue2Name,
                        Width = 136
                    };
                dataPointDataGridView.Columns.Add(value2Column);
            }

            if (topicDescription != null)
            {
                // Tab pages
                if (serviceBusHelper.IsCloudNamespace)
                {
                    EnablePage(MetricsTabPage);
                }
                else
                {
                    DisablePage(MetricsTabPage);
                }

                // Initialize buttons
                btnCreateDelete.Text = DeleteText;
                btnCancelUpdate.Text = UpdateText;
                btnChangeStatus.Text = topicDescription.Status == EntityStatus.Active ? DisableText : EnableText;
                btnRefresh.Visible = true;
                btnChangeStatus.Visible = true;
                btnMetrics.Visible = serviceBusHelper.IsCloudNamespace;

                // Initialize textboxes
                txtPath.ReadOnly = true;
                txtPath.BackColor = SystemColors.Window;
                txtPath.GotFocus += textBox_GotFocus;
                trackBarMaxTopicSize.Enabled = false;

                // Initialize Data
                InitializeData();

                checkedListBox.ItemCheck += checkedListBox_ItemCheck;

                toolTip.SetToolTip(txtPath, PathTooltip);
                toolTip.SetToolTip(txtUserMetadata, UserMetadataTooltip);
                toolTip.SetToolTip(trackBarMaxTopicSize, MaxTopicSizeTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveDays, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveHours, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveMinutes, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveSeconds, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveMilliseconds, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowDays, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowHours, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowMinutes, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowSeconds, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowMilliseconds, DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleDays, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleHours, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleMinutes, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleSeconds, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleMilliseconds, AutoDeleteOnIdleTooltip);

            }
            else
            {
                // Tab pages
                DisablePage(MetricsTabPage);

                // Initialize buttons
                btnCreateDelete.Text = CreateText;
                btnCancelUpdate.Text = CancelText;
                btnRefresh.Visible = false;
                btnChangeStatus.Visible = false;
                btnMetrics.Visible = false;

                // Create BindingList for Authorization Rules
                var bindingList = new BindingList<AuthorizationRuleWrapper>(new List<AuthorizationRuleWrapper>())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };
                bindingList.ListChanged += bindingList_ListChanged;
                authorizationRulesBindingSource.DataSource = bindingList;
                authorizationRulesDataGridView.DataSource = authorizationRulesBindingSource;

                if (!string.IsNullOrWhiteSpace(path))
                {
                    txtPath.Text = path;
                }
                txtPath.Focus();
            }
        }

        private void bindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (topicDescription.Authorization.Count > 0 && topicDescription.Authorization.Count > e.NewIndex)
                {
                    var rule = topicDescription.Authorization.ElementAt(e.NewIndex);
                    if (rule != null)
                    {
                        topicDescription.Authorization.Remove(rule);
                    }
                }
            }
        }

        private void InitializeData()
        {
            // Authorization Rules
            BindingList<AuthorizationRuleWrapper> bindingList;
            if (topicDescription.Authorization.Count > 0)
            {
                var enumerable = topicDescription.Authorization.Select(r => new AuthorizationRuleWrapper(r));
                bindingList = new BindingList<AuthorizationRuleWrapper>(enumerable.ToList())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };

            }
            else
            {
                bindingList = new BindingList<AuthorizationRuleWrapper>(new List<AuthorizationRuleWrapper>())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };
            }
            bindingList.ListChanged += bindingList_ListChanged;
            authorizationRulesBindingSource.DataSource = new BindingList<AuthorizationRuleWrapper>(bindingList);
            authorizationRulesDataGridView.DataSource = authorizationRulesBindingSource;

            // Initialize property grid
            var propertyList = new List<string[]>();

            propertyList.AddRange(new[]{new[]{Status, topicDescription.Status.ToString()},
                                            new[]{IsReadOnly, topicDescription.IsReadOnly.ToString()},
                                            new[]{SizeInBytes, topicDescription.SizeInBytes.ToString("N0")},
                                            new[]{CreatedAt, topicDescription.CreatedAt.ToString(CultureInfo.CurrentCulture)},
                                            new[]{AccessedAt, topicDescription.AccessedAt.ToString(CultureInfo.CurrentCulture)},
                                            new[]{UpdatedAt, topicDescription.UpdatedAt.ToString(CultureInfo.CurrentCulture)},
                                            new[]{ActiveMessageCount, topicDescription.MessageCountDetails.ActiveMessageCount.ToString("N0")},
                                            new[]{DeadletterMessageCount, topicDescription.MessageCountDetails.DeadLetterMessageCount.ToString("N0")},
                                            new[]{ScheduledMessageCount, topicDescription.MessageCountDetails.ScheduledMessageCount.ToString("N0")},
                                            new[]{TransferMessageCount, topicDescription.MessageCountDetails.TransferMessageCount.ToString("N0")},
                                            new[]{TransferDeadLetterMessageCount, topicDescription.MessageCountDetails.TransferDeadLetterMessageCount.ToString("N0")}});

            propertyListView.Items.Clear();
            foreach (var array in propertyList)
            {
                propertyListView.Items.Add(new ListViewItem(array));
            }

            // Path
            if (!string.IsNullOrWhiteSpace(topicDescription.Path))
            {
                txtPath.Text = topicDescription.Path;
            }

            // UserMetadata
            if (!string.IsNullOrWhiteSpace(topicDescription.UserMetadata))
            {
                txtUserMetadata.Text = topicDescription.UserMetadata;
            }

            // MaxQueueSizeInBytes
            trackBarMaxTopicSize.Value = serviceBusHelper.IsCloudNamespace
                                             ? (int)topicDescription.MaxSizeInMegabytes / 1024
                                             : topicDescription.MaxSizeInMegabytes == SeviceBusForWindowsServerMaxTopicSize
                                                   ? 11
                                                   : (int)topicDescription.MaxSizeInMegabytes / 1024;

            // DefaultMessageTimeToLive
            txtDefaultMessageTimeToLiveDays.Text = topicDescription.DefaultMessageTimeToLive.Days.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveHours.Text = topicDescription.DefaultMessageTimeToLive.Hours.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveMinutes.Text = topicDescription.DefaultMessageTimeToLive.Minutes.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveSeconds.Text = topicDescription.DefaultMessageTimeToLive.Seconds.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveMilliseconds.Text = topicDescription.DefaultMessageTimeToLive.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // DuplicateDetectionHistoryTimeWindow
            txtDuplicateDetectionHistoryTimeWindowDays.Text = topicDescription.DuplicateDetectionHistoryTimeWindow.Days.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowHours.Text = topicDescription.DuplicateDetectionHistoryTimeWindow.Hours.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowMinutes.Text = topicDescription.DuplicateDetectionHistoryTimeWindow.Minutes.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowSeconds.Text = topicDescription.DuplicateDetectionHistoryTimeWindow.Seconds.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text = topicDescription.DuplicateDetectionHistoryTimeWindow.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // AutoDeleteOnIdle
            txtAutoDeleteOnIdleDays.Text = topicDescription.AutoDeleteOnIdle.Days.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleHours.Text = topicDescription.AutoDeleteOnIdle.Hours.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleMinutes.Text = topicDescription.AutoDeleteOnIdle.Minutes.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleSeconds.Text = topicDescription.AutoDeleteOnIdle.Seconds.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleMilliseconds.Text = topicDescription.AutoDeleteOnIdle.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // EnableBatchedOperations
            checkedListBox.SetItemChecked(EnableBatchedOperationsIndex,
                                          topicDescription.EnableBatchedOperations);
            // EnableFilteringMessagesBeforePublishing
            checkedListBox.SetItemChecked(EnableFilteringMessagesBeforePublishingIndex,
                                          topicDescription.EnableFilteringMessagesBeforePublishing);
            // RequiresDuplicateDetection
            checkedListBox.SetItemChecked(RequiresDuplicateDetectionIndex,
                                          topicDescription.RequiresDuplicateDetection);

            // SupportOrdering
            checkedListBox.SetItemChecked(SupportOrderingIndex,
                                          topicDescription.SupportOrdering);

            // IsAnonymousAccessible
            if (!serviceBusHelper.IsCloudNamespace &&
                topicDescription != null)
            {
                checkedListBox.SetItemChecked(IsAnonymousAccessibleIndex,
                                              topicDescription.IsAnonymousAccessible);
            }
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (topicDescription == null)
            {
                return;
            }
            if (e.Index == RequiresDuplicateDetectionIndex)
            {
                e.NewValue = topicDescription.RequiresDuplicateDetection ? CheckState.Checked : CheckState.Unchecked;
            }
            if (e.Index == IsAnonymousAccessibleIndex)
            {
                e.NewValue = topicDescription.IsAnonymousAccessible ? CheckState.Checked : CheckState.Unchecked;
            }
        }

        private void btnCreateDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusHelper == null)
                {
                    return;
                }
                if (btnCreateDelete.Text == DeleteText)
                {
                    using (var deleteForm = new DeleteForm(topicDescription.Path, TopicEntity.ToLower()))
                    {
                        if (deleteForm.ShowDialog() == DialogResult.OK)
                        {
                            serviceBusHelper.DeleteTopic(topicDescription);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtPath.Text))
                    {
                        writeToLog(PathCannotBeNull);
                        return;
                    }
                    var description = new TopicDescription(txtPath.Text)
                        {
                            MaxSizeInMegabytes = serviceBusHelper.IsCloudNamespace
                                                 ? trackBarMaxTopicSize.Value*1024
                                                 : trackBarMaxTopicSize.Value == trackBarMaxTopicSize.Maximum
                                                       ? SeviceBusForWindowsServerMaxTopicSize
                                                       : trackBarMaxTopicSize.Value*1024,
                            UserMetadata = txtUserMetadata.Text
                        };

                    var days = 0;
                    var hours = 0;
                    var minutes = 0;
                    var seconds = 0;
                    var milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveDays.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveDays.Text, out days))
                            {
                                writeToLog(DefaultMessageTimeToLiveDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveHours.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveHours.Text, out hours))
                            {
                                writeToLog(DefaultMessageTimeToLiveHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveMinutes.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveMinutes.Text, out minutes))
                            {
                                writeToLog(DefaultMessageTimeToLiveMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveSeconds.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveSeconds.Text, out seconds))
                            {
                                writeToLog(DefaultMessageTimeToLiveSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveMilliseconds.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(DefaultMessageTimeToLiveMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        description.DefaultMessageTimeToLive = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                    }

                    days = 0;
                    hours = 0;
                    minutes = 0;
                    seconds = 0;
                    milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowDays.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowDays.Text, out days))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowHours.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowHours.Text, out hours))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowMinutes.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowMinutes.Text, out minutes))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowSeconds.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowSeconds.Text, out seconds))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        description.DuplicateDetectionHistoryTimeWindow = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                    }

                    days = 0;
                    hours = 0;
                    minutes = 0;
                    seconds = 0;
                    milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleDays.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleDays.Text, out days))
                            {
                                writeToLog(AutoDeleteOnIdleDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleHours.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleHours.Text, out hours))
                            {
                                writeToLog(AutoDeleteOnIdleHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleMinutes.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleMinutes.Text, out minutes))
                            {
                                writeToLog(AutoDeleteOnIdleMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleSeconds.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleSeconds.Text, out seconds))
                            {
                                writeToLog(AutoDeleteOnIdleSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleMilliseconds.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(AutoDeleteOnIdleMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        description.AutoDeleteOnIdle = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                    }

                    description.EnableBatchedOperations = checkedListBox.GetItemChecked(EnableBatchedOperationsIndex);
                    description.EnableFilteringMessagesBeforePublishing = checkedListBox.GetItemChecked(EnableFilteringMessagesBeforePublishingIndex);
                    description.RequiresDuplicateDetection = checkedListBox.GetItemChecked(RequiresDuplicateDetectionIndex);
                    description.SupportOrdering = checkedListBox.GetItemChecked(SupportOrderingIndex);

                    var bindingList = authorizationRulesBindingSource.DataSource as BindingList<AuthorizationRuleWrapper>;
                    if (bindingList != null)
                    {
                        for (var i = 0; i < bindingList.Count; i++)
                        {
                            var rule = bindingList[i];
                            if (serviceBusHelper.IsCloudNamespace)
                            {
                                if (string.IsNullOrWhiteSpace(rule.KeyName))
                                {
                                    writeToLog(string.Format(KeyNameCannotBeNull, i));
                                    continue;
                                }
                                if (string.IsNullOrWhiteSpace(rule.PrimaryKey))
                                {
                                    writeToLog(string.Format(PrimaryKeyCannotBeNull, i));
                                    continue;
                                }
                            }
                            var rightList = new List<AccessRights>();
                            if (rule.Manage)
                            {
                                rightList.AddRange(new[] { AccessRights.Manage, AccessRights.Send, AccessRights.Listen });
                            }
                            else
                            {
                                if (rule.Send)
                                {
                                    rightList.Add(AccessRights.Send);
                                }
                                if (rule.Listen)
                                {
                                    rightList.Add(AccessRights.Listen);
                                }
                            }
                            if (serviceBusHelper.IsCloudNamespace)
                            {
                                if (string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    description.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                    rule.PrimaryKey,
                                                                                                    rightList));
                                }
                                else
                                {
                                    description.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                    rule.PrimaryKey,
                                                                                                    rule.SecondaryKey,
                                                                                                    rightList));
                                }
                            }
                            else
                            {
                                description.Authorization.Add(new AllowRule(rule.IssuerName,
                                                                            rule.ClaimType,
                                                                            rule.ClaimValue,
                                                                            rightList));
                            }
                        }
                    }

                    if (!serviceBusHelper.IsCloudNamespace)
                    {
                        description.IsAnonymousAccessible = checkedListBox.GetItemChecked(IsAnonymousAccessibleIndex);
                    }

                    topicDescription = serviceBusHelper.CreateTopic(description);
                    InitializeControls();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            if (ex == null || string.IsNullOrWhiteSpace(ex.Message))
            {
                return;
            }
            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                writeToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }

        private void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            if (btnCancelUpdate.Text == CancelText)
            {
                if (OnCancel != null)
                {
                    OnCancel();
                }
            }
            else
            {
                try
                {
                    topicDescription.UserMetadata = txtUserMetadata.Text;

                    var days = 0;
                    var hours = 0;
                    var minutes = 0;
                    var seconds = 0;
                    var milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveDays.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveDays.Text, out days))
                            {
                                writeToLog(DefaultMessageTimeToLiveDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveHours.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveHours.Text, out hours))
                            {
                                writeToLog(DefaultMessageTimeToLiveHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveMinutes.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveMinutes.Text, out minutes))
                            {
                                writeToLog(DefaultMessageTimeToLiveMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveSeconds.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveSeconds.Text, out seconds))
                            {
                                writeToLog(DefaultMessageTimeToLiveSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDefaultMessageTimeToLiveMilliseconds.Text))
                        {
                            if (!int.TryParse(txtDefaultMessageTimeToLiveMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(DefaultMessageTimeToLiveMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        var timeSpan = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                        if (!timeSpan.IsMaxValue())
                        {
                            topicDescription.DefaultMessageTimeToLive = timeSpan;
                        }
                    }

                    days = 0;
                    hours = 0;
                    minutes = 0;
                    seconds = 0;
                    milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowDays.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowDays.Text, out days))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowHours.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowHours.Text, out hours))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowMinutes.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowMinutes.Text, out minutes))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowSeconds.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowSeconds.Text, out seconds))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text))
                        {
                            if (!int.TryParse(txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(DuplicateDetectionHistoryTimeWindowMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        var timeSpan = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                        if (!timeSpan.IsMaxValue())
                        {
                            topicDescription.DuplicateDetectionHistoryTimeWindow = timeSpan;
                        }
                    }

                    days = 0;
                    hours = 0;
                    minutes = 0;
                    seconds = 0;
                    milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleDays.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleDays.Text, out days))
                            {
                                writeToLog(AutoDeleteOnIdleDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleHours.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleHours.Text, out hours))
                            {
                                writeToLog(AutoDeleteOnIdleHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleMinutes.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleMinutes.Text, out minutes))
                            {
                                writeToLog(AutoDeleteOnIdleMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleSeconds.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleSeconds.Text, out seconds))
                            {
                                writeToLog(AutoDeleteOnIdleSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtAutoDeleteOnIdleMilliseconds.Text))
                        {
                            if (!int.TryParse(txtAutoDeleteOnIdleMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(AutoDeleteOnIdleMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        var timeSpan = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                        if (!timeSpan.IsMaxValue())
                        {
                            topicDescription.AutoDeleteOnIdle = timeSpan;
                        }
                    }

                    topicDescription.EnableBatchedOperations = checkedListBox.GetItemChecked(EnableBatchedOperationsIndex);
                    topicDescription.EnableFilteringMessagesBeforePublishing = checkedListBox.GetItemChecked(EnableFilteringMessagesBeforePublishingIndex);
                    topicDescription.SupportOrdering = checkedListBox.GetItemChecked(SupportOrderingIndex);
                    
                    if (!serviceBusHelper.IsCloudNamespace)
                    {
                        topicDescription.IsAnonymousAccessible = checkedListBox.GetItemChecked(IsAnonymousAccessibleIndex);
                    }

                    var bindingList = authorizationRulesBindingSource.DataSource as BindingList<AuthorizationRuleWrapper>;
                    if (bindingList != null)
                    {
                        for (var i = 0; i < bindingList.Count; i++)
                        {
                            var rule = bindingList[i];

                            if (serviceBusHelper.IsCloudNamespace)
                            {
                                if (string.IsNullOrWhiteSpace(rule.KeyName))
                                {
                                    writeToLog(string.Format(KeyNameCannotBeNull, i));
                                    continue;
                                }
                            }
                            var rightList = new List<AccessRights>();
                            if (rule.Manage)
                            {
                                rightList.AddRange(new[] { AccessRights.Manage, AccessRights.Send, AccessRights.Listen });
                            }
                            else
                            {
                                if (rule.Send)
                                {
                                    rightList.Add(AccessRights.Send);
                                }
                                if (rule.Listen)
                                {
                                    rightList.Add(AccessRights.Listen);
                                }
                            }
                            if (serviceBusHelper.IsCloudNamespace)
                            {
                                if (string.IsNullOrWhiteSpace(rule.PrimaryKey) && string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    topicDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rightList));
                                }
                                else if (string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    topicDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rule.PrimaryKey,
                                                                                                         rightList));
                                }
                                else
                                {
                                    topicDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rule.PrimaryKey,
                                                                                                         rule.SecondaryKey,
                                                                                                         rightList));
                                }
                            }
                            else
                            {
                                topicDescription.Authorization.Add(new AllowRule(rule.IssuerName,
                                                                                 rule.ClaimType,
                                                                                 rule.ClaimValue,
                                                                                 rightList));
                            }
                        }
                    }

                    topicDescription.Status = EntityStatus.Disabled;
                    serviceBusHelper.UpdateTopic(topicDescription);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    topicDescription = serviceBusHelper.GetTopic(topicDescription.Path);
                }
                finally
                {
                    topicDescription.Status = EntityStatus.Active;
                    topicDescription = serviceBusHelper.NamespaceManager.UpdateTopic(topicDescription);
                    InitializeControls();
                }
            }
        }

        private static void textBox_GotFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                HideCaret(textBox.Handle);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (OnRefresh != null)
            {
                OnRefresh();
            }
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (OnChangeStatus != null)
            {
                OnChangeStatus();
            }
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

        private void btnOpenUserMetadataForm_Click(object sender, EventArgs e)
        {
            using (var form = new TextForm(UserMetadata, txtUserMetadata.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtUserMetadata.Text = form.Content;
                }
            }
        }

        private void propertyListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            var startX = e.ColumnIndex == 0 ? -1 : e.Bounds.X;
            var endX = e.Bounds.X + e.Bounds.Width - 1;
            // Background
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(215, 228, 242)), startX, -1, e.Bounds.Width + 1, e.Bounds.Height + 1);
            // Left vertical line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, startX, e.Bounds.Y + e.Bounds.Height + 1);
            // TopCount horizontal line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, endX, -1);
            // Bottom horizontal line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), startX, e.Bounds.Height - 1, endX, e.Bounds.Height - 1);
            // Right vertical line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), endX, -1, endX, e.Bounds.Height + 1);
            var roundedFontSize = (float)Math.Round(e.Font.SizeInPoints);
            var bounds = new RectangleF(e.Bounds.X + 4, (e.Bounds.Height - 8 - roundedFontSize) / 2, e.Bounds.Width, roundedFontSize + 6);
            e.Graphics.DrawString(e.Header.Text, e.Font, new SolidBrush(SystemColors.ControlText), bounds);
        }

        private void propertyListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void propertyListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void propertyListView_Resize(object sender, EventArgs e)
        {
            try
            {
                propertyListView.SuspendDrawing();
                propertyListView.SuspendLayout();
                var width = propertyListView.Width - propertyListView.Columns[0].Width - 4;
                var scrollbars = ScrollBarHelper.GetVisibleScrollbars(propertyListView);
                if (scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both)
                {
                    width -= 17;
                }
                propertyListView.Columns[1].Width = width;
            }
            finally
            {
                propertyListView.ResumeLayout();
                propertyListView.ResumeDrawing();
            }
        }

        private void trackBarMaxTopicSize_ValueChanged(object sender, decimal value)
        {
            lblMaxTopicSizeInGB.Text = string.Format(SizeInGigabytes, trackBarMaxTopicSize.Value);
            if (!serviceBusHelper.IsCloudNamespace &&
                trackBarMaxTopicSize.Value == trackBarMaxTopicSize.Maximum)
            {
                lblMaxTopicSizeInGB.Text = MaxGigabytes;
            }
        }

        private void mainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mainTabControl, e, null);
        }

        private void DrawTabControlTabs(TabControl tabControl, DrawItemEventArgs e, ImageList images)
        {
            // Get the bounding end of tab strip rectangles.
            var tabstripEndRect = tabControl.GetTabRect(tabControl.TabPages.Count - 1);
            var tabstripEndRectF = new RectangleF(tabstripEndRect.X + tabstripEndRect.Width, tabstripEndRect.Y - 5,
            tabControl.Width - (tabstripEndRect.X + tabstripEndRect.Width), tabstripEndRect.Height + 5);
            var leftVerticalLineRect = new RectangleF(2, tabstripEndRect.Y + tabstripEndRect.Height + 2, 2, tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var rightVerticalLineRect = new RectangleF(tabControl.TabPages[tabControl.SelectedIndex].Width + 4, tabstripEndRect.Y + tabstripEndRect.Height + 2, 2, tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var bottomHorizontalLineRect = new RectangleF(2, tabstripEndRect.Y + tabstripEndRect.Height + tabControl.TabPages[tabControl.SelectedIndex].Height + 2, tabControl.TabPages[tabControl.SelectedIndex].Width + 4, 2);
            RectangleF leftVerticalBarNearFirstTab = new Rectangle(0, 0, 2, tabstripEndRect.Height + 2);

            // First, do the end of the tab strip.
            // If we have an image use it.
            if (tabControl.Parent.BackgroundImage != null)
            {
                var src = new RectangleF(tabstripEndRectF.X + tabControl.Left, tabstripEndRectF.Y + tabControl.Top, tabstripEndRectF.Width, tabstripEndRectF.Height);
                e.Graphics.DrawImage(tabControl.Parent.BackgroundImage, tabstripEndRectF, src, GraphicsUnit.Pixel);
            }
            // If we have no image, use the background color.
            else
            {
                using (Brush backBrush = new SolidBrush(tabControl.Parent.BackColor))
                {
                    e.Graphics.FillRectangle(backBrush, tabstripEndRectF);
                    e.Graphics.FillRectangle(backBrush, leftVerticalLineRect);
                    e.Graphics.FillRectangle(backBrush, rightVerticalLineRect);
                    e.Graphics.FillRectangle(backBrush, bottomHorizontalLineRect);
                    if (mainTabControl.SelectedIndex != 0)
                    {
                        e.Graphics.FillRectangle(backBrush, leftVerticalBarNearFirstTab);
                    }
                }
            }

            // Set up the page and the various pieces.
            var page = tabControl.TabPages[e.Index];
            using (var backBrush = new SolidBrush(page.BackColor))
            {
                using (var foreBrush = new SolidBrush(page.ForeColor))
                {
                    var tabName = page.Text;

                    // Set up the offset for an icon, the bounding rectangle and image size and then fill the background.
                    var iconOffset = 0;
                    Rectangle tabBackgroundRect;

                    if (e.Index == mainTabControl.SelectedIndex)
                    {
                        tabBackgroundRect = e.Bounds;
                        e.Graphics.FillRectangle(backBrush, tabBackgroundRect);
                    }
                    else
                    {
                        tabBackgroundRect = new Rectangle(e.Bounds.X, e.Bounds.Y - 2, e.Bounds.Width,
                                                          e.Bounds.Height + 4);
                        e.Graphics.FillRectangle(backBrush, tabBackgroundRect);
                        var rect = new Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X - 1, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X + e.Bounds.Width, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X + e.Bounds.Width + 1, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                    }

                    // If we have images, process them.
                    if (images != null)
                    {
                        // Get sice and image.
                        var size = images.ImageSize;
                        Image icon = null;
                        if (page.ImageIndex > -1)
                            icon = images.Images[page.ImageIndex];
                        else if (page.ImageKey != "")
                            icon = images.Images[page.ImageKey];

                        // If there is an image, use it.
                        if (icon != null)
                        {
                            var startPoint =
                                new Point(tabBackgroundRect.X + 2 + ((tabBackgroundRect.Height - size.Height) / 2),
                                          tabBackgroundRect.Y + 2 + ((tabBackgroundRect.Height - size.Height) / 2));
                            e.Graphics.DrawImage(icon, new Rectangle(startPoint, size));
                            iconOffset = size.Width + 4;
                        }
                    }

                    // Draw out the label.
                    var labelRect = new Rectangle(tabBackgroundRect.X + iconOffset, tabBackgroundRect.Y + 5,
                                                  tabBackgroundRect.Width - iconOffset, tabBackgroundRect.Height - 3);
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                    {
                        e.Graphics.DrawString(tabName, new Font(e.Font.FontFamily, 8.25F, e.Font.Style), foreBrush, labelRect, sf);
                    }
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            var groupSeparator = numberFormatInfo.NumberGroupSeparator;
            var negativeSign = numberFormatInfo.NegativeSign;

            var keyInput = e.KeyChar.ToString(CultureInfo.InvariantCulture);

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
                     keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
            }
        }

        private void grouperAuthorizationRuleList_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    authorizationRulesDataGridView.Location.X - 1,
                                    authorizationRulesDataGridView.Location.Y - 1,
                                    authorizationRulesDataGridView.Size.Width + 1,
                                    authorizationRulesDataGridView.Size.Height + 1);
        }

        private void EnablePage(string pageName)
        {
            var page = hiddenPages.FirstOrDefault(p => string.Compare(p.Name, pageName, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (page == null)
            {
                return;
            }
            mainTabControl.TabPages.Add(page);
            hiddenPages.Remove(page);
        }

        private void DisablePage(string pageName)
        {
            var page = mainTabControl.TabPages[pageName];
            if (page == null)
            {
                return;
            }
            mainTabControl.TabPages.Remove(page);
            hiddenPages.Add(page);
        }

        private void authorizationRulesDataGridView_Resize(object sender, EventArgs e)
        {
            try
            {
                authorizationRulesDataGridView.SuspendDrawing();
                authorizationRulesDataGridView.SuspendLayout();
                if (authorizationRulesDataGridView.Columns["IssuerName"] == null ||
                authorizationRulesDataGridView.Columns["ClaimType"] == null ||
                authorizationRulesDataGridView.Columns["ClaimValue"] == null ||
                authorizationRulesDataGridView.Columns["Manage"] == null ||
                authorizationRulesDataGridView.Columns["Send"] == null ||
                authorizationRulesDataGridView.Columns["Listen"] == null ||
                authorizationRulesDataGridView.Columns["Revision"] == null ||
                authorizationRulesDataGridView.Columns["CreatedTime"] == null ||
                authorizationRulesDataGridView.Columns["ModifiedTime"] == null)
                {
                    return;
                }

                var width = authorizationRulesDataGridView.Width -
                            authorizationRulesDataGridView.Columns["Manage"].Width -
                            authorizationRulesDataGridView.Columns["Send"].Width -
                            authorizationRulesDataGridView.Columns["Listen"].Width -
                            authorizationRulesDataGridView.Columns["Revision"].Width -
                            authorizationRulesDataGridView.RowHeadersWidth;
                var verticalScrollbar = authorizationRulesDataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                int columnWidth;
                if (serviceBusHelper.IsCloudNamespace)
                {
                    columnWidth = width / 8;
                    authorizationRulesDataGridView.Columns["IssuerName"].Width = width - (7 * columnWidth);
                    if (authorizationRulesDataGridView.Columns["KeyName"] != null &&
                        authorizationRulesDataGridView.Columns["PrimaryKey"] != null &&
                        authorizationRulesDataGridView.Columns["SecondaryKey"] != null)
                    {
                        authorizationRulesDataGridView.Columns["KeyName"].Width = columnWidth;
                        authorizationRulesDataGridView.Columns["PrimaryKey"].Width = columnWidth;
                        authorizationRulesDataGridView.Columns["SecondaryKey"].Width = columnWidth;
                    }
                }
                else
                {
                    columnWidth = width / 5;
                    authorizationRulesDataGridView.Columns["IssuerName"].Width = width - (4 * columnWidth);
                }
                authorizationRulesDataGridView.Columns["ClaimType"].Width = columnWidth;
                authorizationRulesDataGridView.Columns["ClaimValue"].Width = columnWidth;
                authorizationRulesDataGridView.Columns["CreatedTime"].Width = columnWidth;
                authorizationRulesDataGridView.Columns["ModifiedTime"].Width = columnWidth;
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

        private void authorizationRulesDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            using (var deleteForm = new DeleteForm(AuthorizationRuleDeleteMessage))
            {
                e.Cancel = deleteForm.ShowDialog() == DialogResult.Cancel;
            }
        }

        private void authorizationRulesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (authorizationRulesDataGridView.Columns[e.ColumnIndex].Name == "Manage")
            {
                if (!(bool)authorizationRulesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Manage"].Value = true;
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Send"].Value = true;
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Listen"].Value = true;
                }
                return;
            }
            if ((authorizationRulesDataGridView.Columns[e.ColumnIndex].Name == "Send" ||
                 authorizationRulesDataGridView.Columns[e.ColumnIndex].Name == "Listen"))
            {
                if ((bool)authorizationRulesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value &&
                    (bool)authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Manage"].Value)
                {
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["Manage"].Value = false;
                }
            }
        }

        private void authorizationRulesDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!serviceBusHelper.IsCloudNamespace &&
                e.RowIndex == authorizationRulesDataGridView.Rows.Count - 1 &&
                string.IsNullOrWhiteSpace(authorizationRulesDataGridView.Rows[e.RowIndex].Cells["IssuerName"].Value as string))
            {
                authorizationRulesDataGridView.Rows[e.RowIndex].Cells["IssuerName"].Value = serviceBusHelper.Namespace;
            }
        }

        private void CalculateLastColumnWidth()
        {
            if (dataPointDataGridView.Columns.Count < 5)
            {
                return;
            }
            try
            {
                dataPointDataGridView.SuspendDrawing();
                dataPointDataGridView.SuspendLayout();
                var otherColumnsWidth = 0;
                for (var i = 1; i < dataPointDataGridView.Columns.Count; i++)
                {
                    otherColumnsWidth += dataPointDataGridView.Columns[i].Width;
                }
                var width = dataPointDataGridView.Width - dataPointDataGridView.RowHeadersWidth - otherColumnsWidth;
                var verticalScrollbar = dataPointDataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                dataPointDataGridView.Columns[0].Width = width;
            }
            finally
            {
                dataPointDataGridView.ResumeLayout();
                dataPointDataGridView.ResumeDrawing();
            }
        }

        private void grouperDatapoints_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    dataPointDataGridView.Location.X - 1,
                                    dataPointDataGridView.Location.Y - 1,
                                    dataPointDataGridView.Size.Width + 1,
                                    dataPointDataGridView.Size.Height + 1);
        }

        private void dataPointDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataPointDataGridView.NotifyCurrentCellDirty(true);
        }

        private void dataPointDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateLastColumnWidth();
            btnMetrics.Enabled = dataPointDataGridView.Rows.Count > 1;
        }

        private void dataPointDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateLastColumnWidth();
            btnMetrics.Enabled = dataPointDataGridView.Rows.Count > 1;
        }

        private void dataPointDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateLastColumnWidth();
            btnMetrics.Enabled = dataPointDataGridView.Rows.Count > 1;
        }

        private void dataPointDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnMetrics_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dataPointBindingList.Count == 0)
                {
                    return;
                }
                foreach (var item in dataPointBindingList)
                {
                    item.Entity = topicDescription.Path;
                    item.Type = MetricsTopicEntity;
                }
                var uris = MetricHelper.BuildUriListForDataPointMetricQueries(MainForm.SingletonMainForm.SubscriptionId,
                                                                              serviceBusHelper.Namespace,
                                                                              dataPointBindingList);
                var uriList = uris as IList<Uri> ?? uris.ToList();
                if (uris == null || !uriList.Any())
                {
                    return;
                }
                var metricData = MetricHelper.ReadMetricDataUsingTasks(uriList, MainForm.SingletonMainForm.CertificateThumbprint);
                var metricList = metricData as IList<IEnumerable<MetricValue>> ?? metricData.ToList();
                if (metricData == null && metricList.Count == 0)
                {
                    return;
                }
                for (var i = 0; i < metricList.Count; i++)
                {
                    if (metricList[i] == null)
                    {
                        continue;
                    }
                    var key = string.Format(MetricTabPageKeyFormat, tabIndex++);
                    var metricInfo = MetricInfo.MetricInfos.FirstOrDefault(m => m.Name == dataPointBindingList[i].Metric);
                    var friendlyName = metricInfo != null ? metricInfo.FriendlyName : dataPointBindingList[i].Metric;
                    var unit = metricInfo != null ? metricInfo.Unit : Unknown;
                    mainTabControl.TabPages.Add(key, friendlyName);
                    var tabPage = mainTabControl.TabPages[key];
                    tabPage.BackColor = Color.FromArgb(215, 228, 242);
                    tabPage.ForeColor = SystemColors.ControlText;
                    var control = new MetricValueControl(writeToLog,
                                                    () => mainTabControl.TabPages.RemoveByKey(key),
                                                    metricList[i],
                                                    dataPointBindingList[i],
                                                    metricInfo)
                        {
                            Location = new Point(0, 0),
                            Dock = DockStyle.Fill,
                            Tag = string.Format(GrouperFormat, friendlyName, unit)
                        };
                    mainTabControl.TabPages[key].Controls.Add(control);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion
    }
}
