﻿#region Copyright
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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.ServiceBusExplorer.Forms;
using Microsoft.Azure.ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Controls
{
    public partial class HandleSubscriptionControl : UserControl
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

        //***************************
        // Indexes
        //***************************
        private const int EnableBatchedOperationsIndex = 0;
        private const int EnableDeadLetteringOnFilterEvaluationExceptionsIndex = 1;
        private const int EnableDeadLetteringOnMessageExpirationIndex = 2;
        private const int RequiresSessionIndex = 3;

        //***************************
        // Texts
        //***************************
        private const string DeleteText = "Delete";
        private const string CreateText = "Create";
        private const string UpdateText = "Update";
        private const string CancelText = "Cancel";
        private const string EnableText = "Enable";
        private const string DisableText = "Disable";
        private const string FilterExpression = "Filter Expression";
        private const string ActionExpression = "Action Expression";
        private const string UserMetadata = "User Metadata";
        private const string MessageId = "MessageId";
        private const string SequenceNumberName = "Seq";
        private const string SequenceNumberValue = "SequenceNumber";
        private const string MessageSize = "Size";
        private const string Label = "Label";
        private const string EnqueuedTimeUtc = "EnqueuedTimeUtc";
        private const string ExpiresAtUtc = "ExpiresAtUtc";
        private const string SessionId = "SessionId";
        private const string Path = "Path";
        private const string Mode = "Mode";
        private const string BatchFlushInterval = "BatchFlushInterval";

        //***************************
        // Messages
        //***************************
        private const string NameCannotBeNull = "The Name field cannot be null.";
        private const string MaxDeliveryCountMustBeANumber = "The MaxDeliveryCount field must be a number.";

        private const string DefaultMessageTimeToLiveDaysMustBeANumber = "The Days value of the DefaultMessageTimeToLive field must be a number.";
        private const string DefaultMessageTimeToLiveHoursMustBeANumber = "The Hours value of the DefaultMessageTimeToLive field must be a number.";
        private const string DefaultMessageTimeToLiveMinutesMustBeANumber = "The Minutes value of the DefaultMessageTimeToLive field must be a number.";
        private const string DefaultMessageTimeToLiveSecondsMustBeANumber = "The Seconds value of the DefaultMessageTimeToLive field must be a number.";
        private const string DefaultMessageTimeToLiveMillisecondsMustBeANumber = "The Milliseconds value of the DefaultMessageTimeToLive field must be a number.";

        private const string LockDurationDaysMustBeANumber = "The Days value of the LockDuration field must be a number.";
        private const string LockDurationHoursMustBeANumber = "The Hours value of the LockDuration field must be a number.";
        private const string LockDurationMinutesMustBeANumber = "The Minutes value of the LockDuration field must be a number.";
        private const string LockDurationSecondsMustBeANumber = "The Seconds value of the LockDuration field must be a number.";
        private const string LockDurationMillisecondsMustBeANumber = "The Milliseconds value of the LockDuration field must be a number.";

        private const string AutoDeleteOnIdleDaysMustBeANumber = "The Days value of the AutoDeleteOnIdle field must be a number.";
        private const string AutoDeleteOnIdleHoursMustBeANumber = "The Hours value of the AutoDeleteOnIdle field must be a number.";
        private const string AutoDeleteOnIdleMinutesMustBeANumber = "The Minutes value of the AutoDeleteOnIdle field must be a number.";
        private const string AutoDeleteOnIdleSecondsMustBeANumber = "The Seconds value of the AutoDeleteOnIdle field must be a number.";
        private const string AutoDeleteOnIdleMillisecondsMustBeANumber = "The Milliseconds value of the AutoDeleteOnIdle field must be a number.";

        private const string MessagesPeekedFromTheSubscription = "[{0}] messages peeked from the subscription [{1}].";
        private const string MessagesPeekedFromTheDeadletterQueue = "[{0}] messages peeked from the deadletter queue of the subscription [{1}].";
        private const string MessagesReceivedFromTheSubscription = "[{0}] messages received from the subscription [{1}].";
        private const string MessagesReceivedFromTheDeadletterQueue = "[{0}] messages received from the deadletter queue of the subscription [{1}].";
        private const string SessionsGotFromTheSubscription = "[{0}] sessions retrieved for the subscription [{1}].";
        private const string NoMessageReceivedFromTheSubscription = "The timeout  of [{0}] seconds has expired and no message was retrieved from the subscription [{1}].";
        private const string NoMessageReceivedFromTheDeadletterQueue = "The timeout  of [{0}] seconds has expired and no message was retrieved from the deadletter queue of the subscription [{1}].";

        private const string RetrieveMessagesFromSubscription = "Retrieve messages from subscription";
        private const string RetrieveMessagesFromDeadletterQueue = "Retrieve messages from deadletter queue";
        private const string SelectEntityDialogTitle = "Select a target Queue or Topic";
        private const string SelectEntityGrouperTitle = "Forward To";
        private const string SelectEntityLabelText = "Target Queue or Topic:";

        private const string DoubleClickMessage = "Double-click a row to repair and resubmit the corresponding message.";

        private const string FilterExpressionTitle = "Define Filter Expression";
        private const string FilterExpressionLabel = "Filter Expression";
        private const string FilterExpressionNotValidMessage = "The filter expression [{0}] is not valid: {1}";
        private const string FilterExpressionAppliedMessage = "The filter expression [{0}] has been successfully applied. [{1}] messages retrieved.";
        private const string FilterExpressionRemovedMessage = "The filter expression has been removed.";

        //***************************
        // Tooltips
        //***************************
        private const string NameTooltip = "Gets or sets the subscription name.";
        private const string DefaultMessageTimeToLiveTooltip = "Gets or sets the default message time to live of a queue.";
        private const string FilterExpressionTooltip = "Gets or sets the filter expression of the default rule.";
        private const string FilterActionTooltip = "Gets or sets the filter action of the default rule.";
        private const string LockDurationTooltip = "Gets or sets the lock duration timespan associated with this queue.";
        private const string MaxDeliveryCountTooltip = "Gets or sets the maximum delivery count. A message is automatically deadlettered after this number of deliveries.";
        private const string UserMetadataTooltip = "Gets or sets the user metadata.";
        private const string AutoDeleteOnIdleTooltip = "Gets or sets the maximum period of idleness after which the queue is auto deleted.";
        private const string ForwardToTooltip = "Gets or sets the path to the recipient to which the message is forwarded.";
        private const string ForwardDeadLetteredMessagesToTooltip = "Gets or sets the path to the recipient to which the dead lettered message is forwarded.";
        private const string DeleteTooltip = "Delete the row.";

        //***************************
        // Property Labels
        //***************************
        private const string Status = "Status";
        private const string CreatedAt = "Created At";
        private const string AccessedAt = "Accessed At";
        private const string UpdatedAt = "Updated At";
        private const string MessageCount = "Message Count";
        private const string ActiveMessageCount = "Active Message Count";
        private const string DeadletterMessageCount = "DeadLetter Message Count";
        private const string ScheduledMessageCount = "Scheduled Message Count";
        private const string TransferMessageCount = "Transfer Message Count";
        private const string TransferDeadLetterMessageCount = "Transfer DL Message Count";
        private const string IsReadOnly = "Is ReadOnly";

        //***************************
        // Constants
        //***************************
        private const int GrouperMessagePropertiesWith = 312;

        //***************************
        // Pages
        //***************************
        private const string MessagesTabPage = "tabPageMessages";
        private const string SessionsTabPage = "tabPageSessions";
        private const string DeadletterTabPage = "tabPageDeadletter";
        private const string MetricsTabPage = "tabPageMetrics";
        private const string SaveAsTitle = "Save File As";
        private const string JsonExtension = "json";
        private const string JsonFilter = "JSON Files|*.json|Text Documents|*.txt";
        private const string MessageFileFormat = "BrokeredMessage_{0}_{1}.json";

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
        private const string FriendlyNameProperty = "DisplayName";
        private const string NameProperty = "Name";
        private const string SubscriptionEntity = "Subscription";
        private const string SubscriptionPathFormat = "{0}/Subscriptions/{1}";
        private const string Unknown = "Unkown";
        private const string DeleteName = "Delete";
        #endregion

        #region Private Fields
        private SubscriptionWrapper subscriptionWrapper;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private BrokeredMessage brokeredMessage;
        private BrokeredMessage deadletterMessage;
        private readonly BindingSource dataPointBindingSource = new BindingSource();
        private readonly BindingList<MetricDataPoint> dataPointBindingList;
        private int currentMessageRowIndex;
        private int currentDeadletterMessageRowIndex;
        private bool sorting;
        private string messagesFilterExpression;
        private string deadletterFilterExpression;
        private SortableBindingList<BrokeredMessage> messageBindingList;
        private SortableBindingList<BrokeredMessage> deadletterBindingList;
        private SortableBindingList<MessageSession> sessionBindingList;
        private readonly List<string> metricTabPageIndexList = new List<string>();
        private readonly ManualResetEvent metricsManualResetEvent = new ManualResetEvent(false);
        private bool buttonsMoved;
        #endregion

        #region Private Static Fields
        private static readonly List<string> Operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> TimeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };
        #endregion

        #region Public Constructors
        public HandleSubscriptionControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, SubscriptionWrapper subscriptionWrapper)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.subscriptionWrapper = subscriptionWrapper;
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
        public void RefreshData(SubscriptionWrapper wrapper)
        {
            try
            {
                subscriptionWrapper = wrapper;
                InitializeData();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void GetMessages()
        {
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromSubscription, MainForm.SingletonMainForm.TopCount, serviceBusHelper.BrokeredMessageInspectors.Keys))
            {
                if (receiveModeForm.ShowDialog() == DialogResult.OK)
                {
                    txtMessageText.Text = string.Empty;
                    messagePropertyListView.Items.Clear();
                    messagePropertyGrid.SelectedObject = null;
                    var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                           serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector) ?
                                           Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector :
                                           null;
                    if (subscriptionWrapper.TopicDescription.EnablePartitioning)
                    {
                        ReadMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector);
                    }
                    else
                    {
                        GetMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector);
                    }
                }
            }
        }

        public void GetDeadletterMessages()
        {
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromDeadletterQueue, MainForm.SingletonMainForm.TopCount, serviceBusHelper.BrokeredMessageInspectors.Keys))
            {
                if (receiveModeForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtDeadletterText.Text = string.Empty;
                deadletterPropertyListView.Items.Clear();
                deadletterPropertyGrid.SelectedObject = null;
                var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                       serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector) ?
                    Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector :
                    null;
                if (subscriptionWrapper.TopicDescription.EnablePartitioning)
                {
                    ReadDeadletterMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector);
                }
                else
                {
                    GetDeadletterMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector);
                }
            }
        }

        public void GetTransferDeadletterMessages()
        {
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromDeadletterQueue, MainForm.SingletonMainForm.TopCount, serviceBusHelper.BrokeredMessageInspectors.Keys))
            {
                if (receiveModeForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtDeadletterText.Text = string.Empty;
                deadletterPropertyListView.Items.Clear();
                deadletterPropertyGrid.SelectedObject = null;
                var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                       serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector) ?
                    Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector :
                    null;
                if (subscriptionWrapper.TopicDescription.EnablePartitioning)
                {
                    ReadDeadletterMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector);
                }
                else
                {
                    GetDeadletterMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector);
                }
            }
        }

        public void GetMessageSessions()
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageSessions.SuspendDrawing();
                tabPageSessions.SuspendLayout();

                var subscriptionClient =
                    serviceBusHelper.MessagingFactory.CreateSubscriptionClient(
                        subscriptionWrapper.SubscriptionDescription.TopicPath,
                        subscriptionWrapper.SubscriptionDescription.Name,
                        ReceiveMode.PeekLock);
                var sessionEnumerable = subscriptionClient.GetMessageSessions();
                if (sessionEnumerable == null)
                {
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                if (mainTabControl.TabPages[SessionsTabPage] == null)
                {
                    EnablePage(SessionsTabPage);
                }
                var messageSessions = sessionEnumerable as MessageSession[] ?? sessionEnumerable.ToArray();
                sessionBindingList = new SortableBindingList<MessageSession>(messageSessions)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };
                writeToLog(string.Format(SessionsGotFromTheSubscription, sessionBindingList.Count,
                    subscriptionWrapper.SubscriptionDescription.Name));
                sessionsBindingSource.DataSource = sessionBindingList;
                sessionsDataGridView.DataSource = sessionsBindingSource;

                sessionsSplitContainer.SplitterDistance = sessionsSplitContainer.Width -
                                                          GrouperMessagePropertiesWith -
                                                          sessionsSplitContainer.SplitterWidth;
                sessionListTextPropertiesSplitContainer.SplitterDistance =
                    sessionListTextPropertiesSplitContainer.Size.Height/2 - 8;

                if (mainTabControl.TabPages[SessionsTabPage] != null)
                {
                    mainTabControl.SelectTab(SessionsTabPage);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                mainTabControl.ResumeLayout();
                mainTabControl.ResumeDrawing();
                tabPageSessions.ResumeLayout();
                tabPageSessions.ResumeDrawing();
                Cursor.Current = Cursors.Default;
            }
        }

        public async Task<long> PurgeMessagesAsync()
        {
            using (var deleteForm = new DeleteForm($"Would you like to purge the {subscriptionWrapper.SubscriptionDescription.Name} subscription?"))
            {
                if (deleteForm.ShowDialog() != DialogResult.OK)
                {
                    return 0;
                }
            }

            try
            {
                Application.UseWaitCursor = true;
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var entityPath = SubscriptionClient.FormatSubscriptionPath(subscriptionWrapper.SubscriptionDescription.TopicPath, subscriptionWrapper.SubscriptionDescription.Name);
                var messagingFactory = MessagingFactory.CreateFromConnectionString(serviceBusHelper.ConnectionString);
                var receiver = await messagingFactory.CreateMessageReceiverAsync(entityPath, ReceiveMode.ReceiveAndDelete);
                var count = 0;
                while (true)
                {
                    var messages = await receiver.ReceiveBatchAsync(1000, TimeSpan.FromMilliseconds(100));
                    // ReSharper disable once PossibleMultipleEnumeration
                    if (messages.Any())
                    {
                        // ReSharper disable once PossibleMultipleEnumeration
                        count += messages.Count();
                    }
                    else
                    {
                        if (subscriptionWrapper.TopicDescription.EnablePartitioning)
                        {
                            while (true)
                            {
                                var message = await receiver.ReceiveAsync(TimeSpan.FromMilliseconds(100));
                                if (message != null)
                                {
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
                stopwatch.Stop();
                MainForm.SingletonMainForm.refreshEntity_Click(null, null);
                writeToLog($"[{count}] messages have been purged from the [{entityPath}] subscription in [{stopwatch.ElapsedMilliseconds}] milliseconds.");
                return count;
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }

        public async Task<long> PurgeDeadletterQueueMessagesAsync()
        {
            using (var deleteForm = new DeleteForm($"Would you like to purge the deadletter queue of the {subscriptionWrapper.SubscriptionDescription.Name} subscription?"))
            {
                if (deleteForm.ShowDialog() != DialogResult.OK)
                {
                    return 0;
                }
            }

            try
            {
                Application.UseWaitCursor = true;
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var entityPath = SubscriptionClient.FormatDeadLetterPath(subscriptionWrapper.SubscriptionDescription.TopicPath, subscriptionWrapper.SubscriptionDescription.Name);
                var messagingFactory = MessagingFactory.CreateFromConnectionString(serviceBusHelper.ConnectionString);
                var receiver = await messagingFactory.CreateMessageReceiverAsync(entityPath, ReceiveMode.ReceiveAndDelete);
                var count = 0;
                while (true)
                {
                    var messages = await receiver.ReceiveBatchAsync(1000, TimeSpan.FromMilliseconds(100));
                    // ReSharper disable once PossibleMultipleEnumeration
                    if (messages.Any())
                    {
                        // ReSharper disable once PossibleMultipleEnumeration
                        count += messages.Count();
                    }
                    else
                    {
                        if (subscriptionWrapper.TopicDescription.EnablePartitioning)
                        {
                            while (true)
                            {
                                var message = await receiver.ReceiveAsync(TimeSpan.FromMilliseconds(100));
                                if (message != null)
                                {
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
                stopwatch.Stop();
                entityPath = SubscriptionClient.FormatSubscriptionPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                           subscriptionWrapper.SubscriptionDescription.Name);
                MainForm.SingletonMainForm.refreshEntity_Click(null, null);
                writeToLog($"[{count}] messages have been purged from the deadletter queue of the [{entityPath}] subscription in [{stopwatch.ElapsedMilliseconds}] milliseconds.");
                return count;
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            // Splitter controls
            messagesSplitContainer.SplitterWidth = 16;
            sessionsSplitContainer.SplitterWidth = 16;
            deadletterSplitContainer.SplitterWidth = 16;
            messagesCustomPropertiesSplitContainer.SplitterWidth = 16;
            deadletterCustomPropertiesSplitContainer.SplitterWidth = 16;
            messageListTextPropertiesSplitContainer.SplitterWidth = 8;
            deadletterListTextPropertiesSplitContainer.SplitterWidth = 8;

            // Tabe pages
            DisablePage(MessagesTabPage);
            DisablePage(SessionsTabPage);
            DisablePage(DeadletterTabPage);

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

            if (subscriptionWrapper != null && subscriptionWrapper.SubscriptionDescription != null)
            {
                MetricInfo.GetMetricInfoListAsync(serviceBusHelper.Namespace,
                                             SubscriptionEntity,
                                             string.Format(SubscriptionPathFormat,
                                                           subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                           subscriptionWrapper.SubscriptionDescription.Name)).ContinueWith(t => metricsManualResetEvent.Set());
            }

            if (dataPointDataGridView.Columns.Count == 0)
            {
                // Create the Metric column
                var metricColumn = new DataGridViewComboBoxColumn
                    {
                        DataSource = MetricInfo.EntityMetricDictionary.ContainsKey(SubscriptionEntity) ?
                                 MetricInfo.EntityMetricDictionary[SubscriptionEntity] :
                                 null,
                        DataPropertyName = MetricProperty,
                        DisplayMember = FriendlyNameProperty,
                        ValueMember = NameProperty,
                        Name = MetricProperty,
                        Width = 144,
                        DropDownWidth = 250,
                        FlatStyle = FlatStyle.Flat,
                        DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                    };
                dataPointDataGridView.Columns.Add(metricColumn);

                // Create the Time Granularity column
                var timeGranularityColumn = new DataGridViewComboBoxColumn
                    {
                        DataSource = TimeGranularityList,
                        DataPropertyName = GranularityProperty,
                        Name = GranularityProperty,
                        Width = 72,
                        FlatStyle = FlatStyle.Flat
                    };
                dataPointDataGridView.Columns.Add(timeGranularityColumn);

                // Create the Time Operator 1 column
                var operator1Column = new DataGridViewComboBoxColumn
                    {
                        DataSource = Operators,
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
                        DataSource = Operators,
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

                // Create delete column
                var deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = DeleteName,
                    CellTemplate = new DataGridViewDeleteButtonCell(),
                    HeaderText = string.Empty,
                    Width = 22
                };
                deleteButtonColumn.CellTemplate.ToolTipText = DeleteTooltip;
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                dataPointDataGridView.Columns.Add(deleteButtonColumn);
            }

            if (subscriptionWrapper != null &&
                subscriptionWrapper.TopicDescription != null &&
                subscriptionWrapper.SubscriptionDescription != null)
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

                // Initialize textboxes
                txtName.ReadOnly = true;
                txtName.BackColor = SystemColors.Window;
                txtName.GotFocus += textBox_GotFocus;

                txtFilter.ReadOnly = true;
                txtFilter.BackColor = SystemColors.Window;
                txtFilter.GotFocus += textBox_GotFocus;

                txtAction.ReadOnly = true;
                txtAction.BackColor = SystemColors.Window;
                txtAction.GotFocus += textBox_GotFocus;

                txtMessageText.ReadOnly = true;
                txtMessageText.BackColor = SystemColors.Window;
                txtMessageText.GotFocus += textBox_GotFocus;

                txtDeadletterText.ReadOnly = true;
                txtDeadletterText.BackColor = SystemColors.Window;
                txtDeadletterText.GotFocus += textBox_GotFocus;

                txtSessionState.ReadOnly = true;
                txtSessionState.BackColor = SystemColors.Window;
                txtSessionState.GotFocus += textBox_GotFocus;


                // Initialize groupers
                grouperDefaultRule.Visible = false;
                grouperSubscriptionSettings.Location = grouperDefaultRule.Location;
                grouperSubscriptionSettings.Size = new Size(grouperSubscriptionSettings.Size.Width, grouperSubscriptionSettings.Size.Height + grouperDefaultRule.Size.Height + 8);

                // Initialize Data
                InitializeData();

                // Set Grid style
                messagesDataGridView.EnableHeadersVisualStyles = false;
                messagesDataGridView.AutoGenerateColumns = false;
                messagesDataGridView.AutoSize = true;

                // Create the MessageId column
                var textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = MessageId,
                    Name = MessageId,
                    Width = 120
                };
                messagesDataGridView.Columns.Add(textBoxColumn);

                // Create the SequenceNumber column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = SequenceNumberValue,
                    Name = SequenceNumberName,
                    Width = 52
                };
                messagesDataGridView.Columns.Add(textBoxColumn);

                // Create the Size column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = MessageSize,
                    Name = MessageSize,
                    Width = 52
                };
                messagesDataGridView.Columns.Add(textBoxColumn);

                // Create the Label column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = Label,
                    Name = Label,
                    Width = 120
                };
                messagesDataGridView.Columns.Add(textBoxColumn);

                // Create the EnqueuedTimeUtc column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = EnqueuedTimeUtc,
                    Name = EnqueuedTimeUtc,
                    Width = 120
                };
                messagesDataGridView.Columns.Add(textBoxColumn);

                // Create the ExpiresAtUtc column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = ExpiresAtUtc,
                    Name = ExpiresAtUtc,
                    Width = 120
                };
                messagesDataGridView.Columns.Add(textBoxColumn);

                // Set the selection background color for all the cells.
                messagesDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                messagesDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                messagesDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                messagesDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                messagesDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //messagesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //messagesDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                messagesDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                messagesDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                messagesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                messagesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set Grid style
                sessionsDataGridView.EnableHeadersVisualStyles = false;
                sessionsDataGridView.AutoGenerateColumns = false;
                sessionsDataGridView.AutoSize = true;

                // Create the SessionId column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = SessionId,
                    Name = SessionId,
                    Width = 120
                };
                sessionsDataGridView.Columns.Add(textBoxColumn);

                // Create the Path column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = Path,
                    Name = Path,
                    Width = 120
                };
                sessionsDataGridView.Columns.Add(textBoxColumn);

                // Create the Mode column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = Mode,
                    Name = Mode,
                    Width = 120
                };
                sessionsDataGridView.Columns.Add(textBoxColumn);

                // Create the BatchFlushInterval column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = BatchFlushInterval,
                    Name = BatchFlushInterval,
                    Width = 120
                };
                sessionsDataGridView.Columns.Add(textBoxColumn);

                // Set the selection background color for all the cells.
                sessionsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                sessionsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                sessionsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                sessionsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                sessionsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //sessionsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //sessionsDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                sessionsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                sessionsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                sessionsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                sessionsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set Grid style
                deadletterDataGridView.EnableHeadersVisualStyles = false;
                deadletterDataGridView.AutoGenerateColumns = false;
                deadletterDataGridView.AutoSize = true;

                // Create the MessageId column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = MessageId,
                    Name = MessageId,
                    Width = 120
                };
                deadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the SequenceNumber column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = SequenceNumberValue,
                    Name = SequenceNumberName,
                    Width = 52
                };
                deadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the Size column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = MessageSize,
                    Name = MessageSize,
                    Width = 52
                };
                deadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the Label column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = Label,
                    Name = Label,
                    Width = 120
                };
                deadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the EnqueuedTimeUtc column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = EnqueuedTimeUtc,
                    Name = EnqueuedTimeUtc,
                    Width = 120
                };
                deadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the ExpiresAtUtc column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = ExpiresAtUtc,
                    Name = ExpiresAtUtc,
                    Width = 120
                };
                deadletterDataGridView.Columns.Add(textBoxColumn);

                // Set the selection background color for all the cells.
                deadletterDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                deadletterDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                deadletterDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                deadletterDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                deadletterDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //deadletterDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //deadletterDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                deadletterDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                deadletterDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                deadletterDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                deadletterDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                checkedListBox.ItemCheck += checkedListBox_ItemCheck;

                toolTip.SetToolTip(txtName, NameTooltip);
                toolTip.SetToolTip(txtUserMetadata, UserMetadataTooltip);
                toolTip.SetToolTip(txtForwardTo, ForwardToTooltip);
                toolTip.SetToolTip(txtForwardDeadLetteredMessagesTo, ForwardDeadLetteredMessagesToTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveDays, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveHours, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveMinutes, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveSeconds, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveMilliseconds, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtFilter, FilterExpressionTooltip);
                toolTip.SetToolTip(txtAction, FilterActionTooltip);
                toolTip.SetToolTip(txtLockDurationDays, LockDurationTooltip);
                toolTip.SetToolTip(txtLockDurationHours, LockDurationTooltip);
                toolTip.SetToolTip(txtLockDurationMinutes, LockDurationTooltip);
                toolTip.SetToolTip(txtLockDurationSeconds, LockDurationTooltip);
                toolTip.SetToolTip(txtLockDurationMilliseconds, LockDurationTooltip);
                toolTip.SetToolTip(txtMaxDeliveryCount, MaxDeliveryCountTooltip);
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
                btnMessages.Visible = false;
                btnSessions.Visible = false;
                btnDeadletter.Visible = false;
                btnMetrics.Visible = false;
                btnCloseTabs.Visible = false;
                btnPurgeMessages.Visible = false;
                btnPurgeDeadletterQueueMessages.Visible = false;
                txtName.Focus();
            }
        }

        private void InitializeData()
        {
            // Initialize buttons
            btnCreateDelete.Text = DeleteText;
            btnCancelUpdate.Text = UpdateText;
            btnChangeStatus.Text = subscriptionWrapper.SubscriptionDescription.Status == EntityStatus.Active ? DisableText : EnableText;
            btnRefresh.Visible = true;
            btnChangeStatus.Visible = true;
            btnMessages.Visible = true;
            btnSessions.Visible = subscriptionWrapper.SubscriptionDescription.RequiresSession;
            btnMessages.Visible = string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.ForwardTo);
            btnDeadletter.Visible = string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo);

            if (btnMessages.Visible && !btnSessions.Visible && !buttonsMoved)
            {
                btnPurgeMessages.Location = btnPurgeDeadletterQueueMessages.Location;
                btnPurgeDeadletterQueueMessages.Location = btnMetrics.Location;
                btnMetrics.Location = btnCloseTabs.Location;
                btnCloseTabs.Location = btnSessions.Location;
                buttonsMoved = true;
            }

            btnMetrics.Visible = serviceBusHelper.IsCloudNamespace;
            btnDeadletter.Visible = true;
            btnOpenFilterForm.Enabled = false;
            btnOpenActionForm.Enabled = false;

            // Initialize property grid
            var propertyList = new List<string[]>();

            propertyList.AddRange(new[]{new[]{Status, subscriptionWrapper.SubscriptionDescription.Status.ToString()},
                                            new[]{IsReadOnly, subscriptionWrapper.SubscriptionDescription.IsReadOnly.ToString()},
                                            new[]{CreatedAt, subscriptionWrapper.SubscriptionDescription.CreatedAt.ToString(CultureInfo.CurrentCulture)},
                                            new[]{AccessedAt, subscriptionWrapper.SubscriptionDescription.AccessedAt.ToString(CultureInfo.CurrentCulture)},
                                            new[]{UpdatedAt, subscriptionWrapper.SubscriptionDescription.UpdatedAt.ToString(CultureInfo.CurrentCulture)},
                                            new[]{ActiveMessageCount, subscriptionWrapper.SubscriptionDescription.MessageCountDetails.ActiveMessageCount.ToString("N0")},
                                            new[]{DeadletterMessageCount, subscriptionWrapper.SubscriptionDescription.MessageCountDetails.DeadLetterMessageCount.ToString("N0")},
                                            new[]{ScheduledMessageCount, subscriptionWrapper.SubscriptionDescription.MessageCountDetails.ScheduledMessageCount.ToString("N0")},
                                            new[]{TransferMessageCount, subscriptionWrapper.SubscriptionDescription.MessageCountDetails.TransferMessageCount.ToString("N0")},
                                            new[]{TransferDeadLetterMessageCount, subscriptionWrapper.SubscriptionDescription.MessageCountDetails.TransferDeadLetterMessageCount.ToString("N0")},
                                            new[]{MessageCount, subscriptionWrapper.SubscriptionDescription.MessageCount.ToString("N0")}});

            propertyListView.Items.Clear();
            foreach (var array in propertyList)
            {
                propertyListView.Items.Add(new ListViewItem(array));
            }

            // Name
            if (!string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.Name))
            {
                txtName.Text = subscriptionWrapper.SubscriptionDescription.Name;
            }

            // UserMetadata
            if (!string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.UserMetadata))
            {
                txtUserMetadata.Text = subscriptionWrapper.SubscriptionDescription.UserMetadata;
            }

            // ForwardTo
            if (!string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.ForwardTo))
            {
                int i;
                txtForwardTo.Text = !string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.ForwardTo) &&
                                    (i = subscriptionWrapper.SubscriptionDescription.ForwardTo.IndexOf('/')) > 0 &&
                                    i < subscriptionWrapper.SubscriptionDescription.ForwardTo.Length - 1 ?
                                    subscriptionWrapper.SubscriptionDescription.ForwardTo.Substring(subscriptionWrapper.SubscriptionDescription.ForwardTo.LastIndexOf('/') + 1) :
                                    subscriptionWrapper.SubscriptionDescription.ForwardTo;
            }

            // ForwardDeadLetteredMessagesTo
            if (!string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo))
            {
                int i;
                txtForwardDeadLetteredMessagesTo.Text = !string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo) &&
                                    (i = subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo.IndexOf('/')) > 0 &&
                                    i < subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo.Length - 1 ?
                                    subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo.Substring(subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo.LastIndexOf('/') + 1) :
                                    subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo;
            }

            // MaxDeliveryCount
            txtMaxDeliveryCount.Text = subscriptionWrapper.SubscriptionDescription.MaxDeliveryCount.ToString(CultureInfo.InvariantCulture);

            // DefaultMessageTimeToLive
            txtDefaultMessageTimeToLiveDays.Text = subscriptionWrapper.SubscriptionDescription.DefaultMessageTimeToLive.Days.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveHours.Text = subscriptionWrapper.SubscriptionDescription.DefaultMessageTimeToLive.Hours.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveMinutes.Text = subscriptionWrapper.SubscriptionDescription.DefaultMessageTimeToLive.Minutes.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveSeconds.Text = subscriptionWrapper.SubscriptionDescription.DefaultMessageTimeToLive.Seconds.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveMilliseconds.Text = subscriptionWrapper.SubscriptionDescription.DefaultMessageTimeToLive.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // LockDuration
            txtLockDurationDays.Text = subscriptionWrapper.SubscriptionDescription.LockDuration.Days.ToString(CultureInfo.InvariantCulture);
            txtLockDurationHours.Text = subscriptionWrapper.SubscriptionDescription.LockDuration.Hours.ToString(CultureInfo.InvariantCulture);
            txtLockDurationMinutes.Text = subscriptionWrapper.SubscriptionDescription.LockDuration.Minutes.ToString(CultureInfo.InvariantCulture);
            txtLockDurationSeconds.Text = subscriptionWrapper.SubscriptionDescription.LockDuration.Seconds.ToString(CultureInfo.InvariantCulture);
            txtLockDurationMilliseconds.Text = subscriptionWrapper.SubscriptionDescription.LockDuration.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // AutoDeleteOnIdle
            txtAutoDeleteOnIdleDays.Text = subscriptionWrapper.SubscriptionDescription.AutoDeleteOnIdle.Days.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleHours.Text = subscriptionWrapper.SubscriptionDescription.AutoDeleteOnIdle.Hours.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleMinutes.Text = subscriptionWrapper.SubscriptionDescription.AutoDeleteOnIdle.Minutes.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleSeconds.Text = subscriptionWrapper.SubscriptionDescription.AutoDeleteOnIdle.Seconds.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleMilliseconds.Text = subscriptionWrapper.SubscriptionDescription.AutoDeleteOnIdle.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // EnableDeadLetteringOnFilterEvaluationExceptions
            checkedListBox.SetItemChecked(EnableBatchedOperationsIndex,
                                          subscriptionWrapper.SubscriptionDescription.EnableBatchedOperations);

            // EnableDeadLetteringOnFilterEvaluationExceptions
            checkedListBox.SetItemChecked(EnableDeadLetteringOnFilterEvaluationExceptionsIndex,
                                          subscriptionWrapper.SubscriptionDescription.EnableDeadLetteringOnFilterEvaluationExceptions);

            // EnableDeadLetteringOnMessageExpiration
            checkedListBox.SetItemChecked(EnableDeadLetteringOnMessageExpirationIndex,
                                          subscriptionWrapper.SubscriptionDescription.EnableDeadLetteringOnMessageExpiration);

            // RequiresSession
            checkedListBox.SetItemChecked(RequiresSessionIndex,
                                          subscriptionWrapper.SubscriptionDescription.RequiresSession);
        }

        private void GetMessages(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector)
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageMessages.SuspendDrawing();
                tabPageMessages.SuspendLayout();

                Cursor.Current = Cursors.WaitCursor;
                var brokeredMessages = new List<BrokeredMessage>();
                if (peek)
                {
                    var subscriptionClient = serviceBusHelper.MessagingFactory.CreateSubscriptionClient(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                        subscriptionWrapper.SubscriptionDescription.Name,
                                                                                                        ReceiveMode.PeekLock);
                    var totalRetrieved = 0;
                    while (totalRetrieved < count)
                    {
                        var messageEnumerable = subscriptionClient.PeekBatch(count);
                        if (messageEnumerable == null)
                        {
                            break;
                        }
                        var messageArray = messageEnumerable as BrokeredMessage[] ?? messageEnumerable.ToArray();
                        var partialList = messageInspector != null ?
                                       messageArray.Select(b => messageInspector.AfterReceiveMessage(b, writeToLog)).ToList() :
                                       new List<BrokeredMessage>(messageArray);
                        brokeredMessages.AddRange(partialList);
                        totalRetrieved += partialList.Count;
                        if (partialList.Count == 0)
                        {
                            break;
                        }
                    }
                    writeToLog(string.Format(MessagesPeekedFromTheSubscription, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
                }
                else
                {
                    MessageReceiver messageReceiver;
                    if (subscriptionWrapper.SubscriptionDescription.RequiresSession)
                    {
                        var subscriptionClient = serviceBusHelper.MessagingFactory.CreateSubscriptionClient(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                            subscriptionWrapper.SubscriptionDescription.Name,
                                                                                                            ReceiveMode.ReceiveAndDelete);
                        messageReceiver = subscriptionClient.AcceptMessageSession(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                    }
                    else
                    {
                        messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(SubscriptionClient.FormatSubscriptionPath(
                                                                                                  subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                  subscriptionWrapper.SubscriptionDescription.Name),
                                                                                                  ReceiveMode.ReceiveAndDelete);
                    }
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = messageReceiver.ReceiveBatch(all
                            ? MainForm.SingletonMainForm.TopCount
                            : count - totalRetrieved,
                            TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Count();
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b, writeToLog)) : enumerable);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesReceivedFromTheSubscription, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
                }
                messageBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };
                messagesBindingSource.DataSource = messageBindingList;
                messagesDataGridView.DataSource = messagesBindingSource;

                messagesSplitContainer.SplitterDistance = messagesSplitContainer.Width -
                                                          GrouperMessagePropertiesWith -
                                                          messagesSplitContainer.SplitterWidth;
                messageListTextPropertiesSplitContainer.SplitterDistance = messageListTextPropertiesSplitContainer.Size.Height / 2 - 8;
                messagesCustomPropertiesSplitContainer.SplitterDistance = messagesCustomPropertiesSplitContainer.Size.Width / 2 - 8;

                if (!peek)
                {
                    if (OnRefresh != null)
                    {
                        OnRefresh();
                    }
                }
                if (mainTabControl.TabPages[MessagesTabPage] == null)
                {
                    EnablePage(MessagesTabPage);
                }
                if (mainTabControl.TabPages[MessagesTabPage] != null)
                {
                    mainTabControl.SelectTab(MessagesTabPage);
                }
            }
            catch (TimeoutException)
            {
                writeToLog(string.Format(NoMessageReceivedFromTheSubscription, 
                                         MainForm.SingletonMainForm.ReceiveTimeout,  
                                         subscriptionWrapper.SubscriptionDescription.Name));
            }
            catch (NotSupportedException)
            {
                ReadMessagesOneAtTheTime(peek, all, count, messageInspector);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                mainTabControl.ResumeLayout();
                mainTabControl.ResumeDrawing();
                tabPageMessages.ResumeLayout();
                tabPageMessages.ResumeDrawing();
                Cursor.Current = Cursors.Default;
            }
        }

        private void ReadMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector)
        {
            try
            {
                var brokeredMessages = new List<BrokeredMessage>();
                if (peek)
                {
                    var subscriptionClient = serviceBusHelper.MessagingFactory.CreateSubscriptionClient(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                 subscriptionWrapper.SubscriptionDescription.Name,
                                                                                                 ReceiveMode.PeekLock);
                    for (var i = 0; i < count; i++)
                    {
                        var message = subscriptionClient.Peek();
                        if (message != null)
                        {
                            if (messageInspector != null)
                            {
                                message = messageInspector.AfterReceiveMessage(message);
                            }
                            brokeredMessages.Add(message);
                        }
                    }
                    writeToLog(string.Format(MessagesPeekedFromTheSubscription, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
                }
                else
                {
                    MessageReceiver messageReceiver;
                    if (subscriptionWrapper.SubscriptionDescription.RequiresSession)
                    {
                        var subscriptionClient = serviceBusHelper.MessagingFactory.CreateSubscriptionClient(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                           subscriptionWrapper.SubscriptionDescription.Name,
                                                                                           ReceiveMode.ReceiveAndDelete);
                        messageReceiver = subscriptionClient.AcceptMessageSession(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                    }
                    else
                    {
                        messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(SubscriptionClient.FormatSubscriptionPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                                                            subscriptionWrapper.SubscriptionDescription.Name),
                                                                                                  ReceiveMode.ReceiveAndDelete);
                    }

                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var message = messageReceiver.Receive(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        retrieved = message != null ? 1 : 0;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.Add(messageInspector != null ? messageInspector.AfterReceiveMessage(message) : message);
                    }
                    while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesReceivedFromTheSubscription, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
                }
                messageBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };
                messagesBindingSource.DataSource = messageBindingList;
                messagesDataGridView.DataSource = messagesBindingSource;

                messagesSplitContainer.SplitterDistance = messagesSplitContainer.Width -
                                                          GrouperMessagePropertiesWith -
                                                          messagesSplitContainer.SplitterWidth;
                messageListTextPropertiesSplitContainer.SplitterDistance = messageListTextPropertiesSplitContainer.Size.Height / 2 - 8;
                messagesCustomPropertiesSplitContainer.SplitterDistance = messagesCustomPropertiesSplitContainer.Size.Width / 2 - 8;

                if (!peek)
                {
                    if (OnRefresh != null)
                    {
                        OnRefresh();
                    }
                }
                if (mainTabControl.TabPages[MessagesTabPage] == null)
                {
                    EnablePage(MessagesTabPage);
                }
                if (mainTabControl.TabPages[MessagesTabPage] != null)
                {
                    mainTabControl.SelectTab(MessagesTabPage);
                }
            }
            catch (TimeoutException)
            {
                writeToLog(string.Format(NoMessageReceivedFromTheSubscription,
                                         MainForm.SingletonMainForm.ReceiveTimeout,
                                         subscriptionWrapper.SubscriptionDescription.Name));
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void GetDeadletterMessages(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector)
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageDeadletter.SuspendDrawing();
                tabPageDeadletter.SuspendLayout();

                Cursor.Current = Cursors.WaitCursor;
                var brokeredMessages = new List<BrokeredMessage>();

                if (peek)
                {
                    var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(SubscriptionClient.FormatDeadLetterPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                                              subscriptionWrapper.SubscriptionDescription.Name),
                                                                                              ReceiveMode.PeekLock);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = messageReceiver.PeekBatch(all ?
                                                                    MainForm.SingletonMainForm.TopCount :
                                                                    count - totalRetrieved);
                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Count();
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b, writeToLog)) : enumerable);
                    }
                    while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
                }
                else
                {
                    var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(SubscriptionClient.FormatDeadLetterPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                                              subscriptionWrapper.SubscriptionDescription.Name),
                                                                                              ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = messageReceiver.ReceiveBatch(all ?
                                                                    MainForm.SingletonMainForm.TopCount :
                                                                    count - totalRetrieved,
                                                                    TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Count();
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b, writeToLog)) : enumerable);
                    }
                    while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesReceivedFromTheDeadletterQueue, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
                }

                deadletterBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };

                deadletterBindingSource.DataSource = deadletterBindingList;
                deadletterDataGridView.DataSource = deadletterBindingSource;

                deadletterSplitContainer.SplitterDistance = deadletterSplitContainer.Width -
                                                          GrouperMessagePropertiesWith -
                                                          deadletterSplitContainer.SplitterWidth;
                deadletterListTextPropertiesSplitContainer.SplitterDistance = deadletterListTextPropertiesSplitContainer.Size.Height / 2 - 8;
                deadletterCustomPropertiesSplitContainer.SplitterDistance = deadletterCustomPropertiesSplitContainer.Size.Width / 2 - 8;

                if (!peek)
                {
                    if (OnRefresh != null)
                    {
                        OnRefresh();
                    }
                }
                if (mainTabControl.TabPages[DeadletterTabPage] == null)
                {
                    EnablePage(DeadletterTabPage);
                }
                if (mainTabControl.TabPages[DeadletterTabPage] != null)
                {
                    mainTabControl.SelectTab(DeadletterTabPage);
                }
            }
            catch (TimeoutException)
            {
                writeToLog(string.Format(NoMessageReceivedFromTheDeadletterQueue,
                                         MainForm.SingletonMainForm.ReceiveTimeout,
                                         subscriptionWrapper.SubscriptionDescription.Name));
            }
            catch (NotSupportedException)
            {
                ReadDeadletterMessagesOneAtTheTime(peek, all, count, messageInspector);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                mainTabControl.ResumeLayout();
                mainTabControl.ResumeDrawing();
                tabPageDeadletter.ResumeLayout();
                tabPageDeadletter.ResumeDrawing();
                Cursor.Current = Cursors.Default;
            }
        }

        private void ReadDeadletterMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector)
        {
            try
            {
                var brokeredMessages = new List<BrokeredMessage>();

                if (peek)
                {
                    var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(SubscriptionClient.FormatDeadLetterPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                                              subscriptionWrapper.SubscriptionDescription.Name),
                                                                                                  ReceiveMode.PeekLock);
                    for (var i = 0; i < count; i++)
                    {
                        var message = messageReceiver.Peek();
                        if (message != null)
                        {
                            if (messageInspector != null)
                            {
                                message = messageInspector.AfterReceiveMessage(message);
                            }
                            brokeredMessages.Add(message);
                        }
                    }
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
                }
                else
                {
                    var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(SubscriptionClient.FormatDeadLetterPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                                              subscriptionWrapper.SubscriptionDescription.Name),
                                                                                                  ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var message = messageReceiver.Receive(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        retrieved = message != null ? 1 : 0;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.Add(messageInspector != null ? messageInspector.AfterReceiveMessage(message) : message);
                    }
                    while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesReceivedFromTheDeadletterQueue, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
                }
                deadletterBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };
                deadletterBindingSource.DataSource = deadletterBindingList;
                deadletterDataGridView.DataSource = deadletterBindingSource;

                deadletterSplitContainer.SplitterDistance = deadletterSplitContainer.Width -
                                                          GrouperMessagePropertiesWith -
                                                          deadletterSplitContainer.SplitterWidth;
                deadletterListTextPropertiesSplitContainer.SplitterDistance = deadletterListTextPropertiesSplitContainer.Size.Height / 2 - 8;
                deadletterCustomPropertiesSplitContainer.SplitterDistance = deadletterCustomPropertiesSplitContainer.Size.Width / 2 - 8;

                if (!peek)
                {
                    if (OnRefresh != null)
                    {
                        OnRefresh();
                    }
                }
                if (mainTabControl.TabPages[DeadletterTabPage] == null)
                {
                    EnablePage(DeadletterTabPage);
                }
                if (mainTabControl.TabPages[DeadletterTabPage] != null)
                {
                    mainTabControl.SelectTab(DeadletterTabPage);
                }
            }
            catch (TimeoutException)
            {
                writeToLog(string.Format(NoMessageReceivedFromTheDeadletterQueue,
                                         MainForm.SingletonMainForm.ReceiveTimeout,
                                         subscriptionWrapper.SubscriptionDescription.Name));
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private async void btnCreateDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (serviceBusHelper == null ||
                    subscriptionWrapper == null ||
                    subscriptionWrapper.TopicDescription == null)
                {
                    return;
                }
                if (btnCreateDelete.Text == DeleteText &&
                    subscriptionWrapper.SubscriptionDescription != null &&
                    !string.IsNullOrWhiteSpace(subscriptionWrapper.SubscriptionDescription.Name))
                {
                    using (var deleteForm = new DeleteForm(subscriptionWrapper.SubscriptionDescription.Name, SubscriptionEntity.ToLower()))
                    {
                        if (deleteForm.ShowDialog() == DialogResult.OK)
                        {
                            await serviceBusHelper.DeleteSubscription(subscriptionWrapper.SubscriptionDescription);
                        }
                    }
                    return;
                }
                if (btnCreateDelete.Text == CreateText)
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        writeToLog(NameCannotBeNull);
                        return;
                    }
                    var subscriptionDescription = new SubscriptionDescription(subscriptionWrapper.TopicDescription.Path, txtName.Text);
                    if (!string.IsNullOrWhiteSpace(txtUserMetadata.Text))
                    {
                        subscriptionDescription.UserMetadata = txtUserMetadata.Text;
                    }
                    if (!string.IsNullOrWhiteSpace(txtForwardTo.Text))
                    {
                        subscriptionDescription.ForwardTo = txtForwardTo.Text;
                    }
                    if (!string.IsNullOrWhiteSpace(txtForwardDeadLetteredMessagesTo.Text))
                    {
                        subscriptionDescription.ForwardDeadLetteredMessagesTo = txtForwardDeadLetteredMessagesTo.Text;
                    }
                    if (!string.IsNullOrWhiteSpace(txtMaxDeliveryCount.Text))
                    {
                        if (int.TryParse(txtMaxDeliveryCount.Text, out var value))
                        {
                            subscriptionDescription.MaxDeliveryCount = value;
                        }
                        else
                        {
                            writeToLog(MaxDeliveryCountMustBeANumber);
                            return;
                        }
                    }

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
                        subscriptionDescription.DefaultMessageTimeToLive = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                    }

                    days = 0;
                    hours = 0;
                    minutes = 0;
                    seconds = 0;
                    milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtLockDurationDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtLockDurationHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtLockDurationMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtLockDurationSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtLockDurationMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtLockDurationDays.Text))
                        {
                            if (!int.TryParse(txtLockDurationDays.Text, out days))
                            {
                                writeToLog(LockDurationDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtLockDurationHours.Text))
                        {
                            if (!int.TryParse(txtLockDurationHours.Text, out hours))
                            {
                                writeToLog(LockDurationHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtLockDurationMinutes.Text))
                        {
                            if (!int.TryParse(txtLockDurationMinutes.Text, out minutes))
                            {
                                writeToLog(LockDurationMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtLockDurationSeconds.Text))
                        {
                            if (!int.TryParse(txtLockDurationSeconds.Text, out seconds))
                            {
                                writeToLog(LockDurationSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtLockDurationMilliseconds.Text))
                        {
                            if (!int.TryParse(txtLockDurationMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(LockDurationMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        subscriptionDescription.LockDuration = new TimeSpan(days, hours, minutes, seconds, milliseconds);
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
                        subscriptionDescription.AutoDeleteOnIdle = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                    }

                    subscriptionDescription.EnableBatchedOperations = checkedListBox.GetItemChecked(EnableBatchedOperationsIndex);
                    subscriptionDescription.EnableDeadLetteringOnFilterEvaluationExceptions = checkedListBox.GetItemChecked(EnableDeadLetteringOnFilterEvaluationExceptionsIndex);
                    subscriptionDescription.EnableDeadLetteringOnMessageExpiration = checkedListBox.GetItemChecked(EnableDeadLetteringOnMessageExpirationIndex);
                    subscriptionDescription.RequiresSession = checkedListBox.GetItemChecked(RequiresSessionIndex);

                    var ruleDescription = new RuleDescription();

                    if (!string.IsNullOrWhiteSpace(txtFilter.Text))
                    {
                        ruleDescription.Filter = new SqlFilter(txtFilter.Text);
                    }
                    if (!string.IsNullOrWhiteSpace(txtAction.Text))
                    {
                        ruleDescription.Action = new SqlRuleAction(txtAction.Text);
                    }

                    subscriptionWrapper.SubscriptionDescription = serviceBusHelper.CreateSubscription(subscriptionWrapper.TopicDescription, 
                                                                                          subscriptionDescription, 
                                                                                          ruleDescription);
                    InitializeData();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
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

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (subscriptionWrapper == null || subscriptionWrapper.SubscriptionDescription == null)
            {
                return;
            }
            if (e.Index == RequiresSessionIndex)
            {
                e.NewValue = subscriptionWrapper.SubscriptionDescription.RequiresSession ? CheckState.Checked : CheckState.Unchecked;
            }
        }

        private void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            if (btnCancelUpdate.Text == CancelText)
            {
                OnCancel?.Invoke();
            }
            else
            {
                try
                {
                    subscriptionWrapper.SubscriptionDescription.UserMetadata = txtUserMetadata.Text;
                    subscriptionWrapper.SubscriptionDescription.ForwardTo = string.IsNullOrWhiteSpace(txtForwardTo.Text) ? null : txtForwardTo.Text;
                    subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo = string.IsNullOrWhiteSpace(txtForwardDeadLetteredMessagesTo.Text) ? null : txtForwardDeadLetteredMessagesTo.Text;

                    if (!string.IsNullOrWhiteSpace(txtMaxDeliveryCount.Text))
                    {
                        if (int.TryParse(txtMaxDeliveryCount.Text, out var value))
                        {
                            subscriptionWrapper.SubscriptionDescription.MaxDeliveryCount = value;
                        }
                        else
                        {
                            writeToLog(MaxDeliveryCountMustBeANumber);
                            return;
                        }
                    }

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
                        subscriptionWrapper.SubscriptionDescription.DefaultMessageTimeToLive = new TimeSpan(days, hours, minutes, seconds,
                                                                                 milliseconds);
                    }

                    days = 0;
                    hours = 0;
                    minutes = 0;
                    seconds = 0;
                    milliseconds = 0;

                    if (!string.IsNullOrWhiteSpace(txtLockDurationDays.Text) ||
                        !string.IsNullOrWhiteSpace(txtLockDurationHours.Text) ||
                        !string.IsNullOrWhiteSpace(txtLockDurationMinutes.Text) ||
                        !string.IsNullOrWhiteSpace(txtLockDurationSeconds.Text) ||
                        !string.IsNullOrWhiteSpace(txtLockDurationMilliseconds.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtLockDurationDays.Text))
                        {
                            if (!int.TryParse(txtLockDurationDays.Text, out days))
                            {
                                writeToLog(LockDurationDaysMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtLockDurationHours.Text))
                        {
                            if (!int.TryParse(txtLockDurationHours.Text, out hours))
                            {
                                writeToLog(LockDurationHoursMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtLockDurationMinutes.Text))
                        {
                            if (!int.TryParse(txtLockDurationMinutes.Text, out minutes))
                            {
                                writeToLog(LockDurationMinutesMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtLockDurationSeconds.Text))
                        {
                            if (!int.TryParse(txtLockDurationSeconds.Text, out seconds))
                            {
                                writeToLog(LockDurationSecondsMustBeANumber);
                                return;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(txtLockDurationMilliseconds.Text))
                        {
                            if (!int.TryParse(txtLockDurationMilliseconds.Text, out milliseconds))
                            {
                                writeToLog(LockDurationMillisecondsMustBeANumber);
                                return;
                            }
                        }
                        var timeSpan = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                        if (!timeSpan.IsMaxValue())
                        {
                            subscriptionWrapper.SubscriptionDescription.LockDuration = timeSpan;
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
                            subscriptionWrapper.SubscriptionDescription.AutoDeleteOnIdle = timeSpan;   
                        }
                    }

                    subscriptionWrapper.SubscriptionDescription.EnableBatchedOperations = checkedListBox.GetItemChecked(EnableBatchedOperationsIndex);
                    subscriptionWrapper.SubscriptionDescription.EnableDeadLetteringOnFilterEvaluationExceptions = checkedListBox.GetItemChecked(EnableDeadLetteringOnFilterEvaluationExceptionsIndex);
                    subscriptionWrapper.SubscriptionDescription.EnableDeadLetteringOnMessageExpiration = checkedListBox.GetItemChecked(EnableDeadLetteringOnMessageExpirationIndex);
                    subscriptionWrapper.SubscriptionDescription.Status = EntityStatus.Disabled;
                    serviceBusHelper.UpdateSubscription(subscriptionWrapper.TopicDescription, subscriptionWrapper.SubscriptionDescription);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    subscriptionWrapper.SubscriptionDescription =
                        serviceBusHelper.GetSubscription(subscriptionWrapper.TopicDescription.Path,
                                                         subscriptionWrapper.SubscriptionDescription.Name);
                }
                finally
                {
                    subscriptionWrapper.SubscriptionDescription.Status = EntityStatus.Active;
                    subscriptionWrapper.SubscriptionDescription = serviceBusHelper.NamespaceManager.UpdateSubscription(subscriptionWrapper.SubscriptionDescription);
                    InitializeData();
                }
            }
        }

        private void openOpenFilterForm_Click(object sender, EventArgs e)
        {
            using (var form = new TextForm(FilterExpression, txtFilter.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtFilter.Text = form.Content;
                }
            }
        }

        private void btnOpenActionForm_Click(object sender, EventArgs e)
        {
            using (var form = new TextForm(ActionExpression, txtAction.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtAction.Text = form.Content;
                }
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

        private void btnOpenForwardToForm_Click(object sender, EventArgs e)
        {
            using (var form = new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtForwardTo.Text = form.Path;
                }
            }
        }

        private void btnOpenForwardDeadLetteredMessagesToForm_Click(object sender, EventArgs e)
        {
            using (var form = new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtForwardDeadLetteredMessagesTo.Text = form.Path;
                }
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

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null)
            {
                return;
            }
            try
            {
                listView.SuspendDrawing();
                listView.SuspendLayout();
                var width = listView.Width - listView.Columns[0].Width - 5;
                var scrollbars = ScrollBarHelper.GetVisibleScrollbars(listView);
                if (scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both)
                {
                    width -= 17;
                }
                listView.Columns[1].Width = width;
            }
            finally
            {
                listView.ResumeLayout();
                listView.ResumeDrawing();
            }
        }

        private void dataGridView_Resize(object sender, EventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //var textBox = (TextBox)e.Control;
            //textBox.Multiline = true;
            //textBox.ScrollBars = ScrollBars.Both;
        }

        private void CalculateLastColumnWidth(object sender)
        {
            if (sorting)
            {
                return;
            }
            var dataGridView = sender as DataGridView;
            if (dataGridView == null || dataGridView.Columns.Count == 0)
            {
                return;
            }
            try
            {
                dataGridView.SuspendDrawing();
                dataGridView.SuspendLayout();
                if (dataGridView.Columns.Count == 2)
                {
                    var width = dataGridView.Width - dataGridView.Columns[0].Width - dataGridView.RowHeadersWidth;
                    var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                    if (verticalScrollbar != null && verticalScrollbar.Visible)
                    {
                        width -= verticalScrollbar.Width;
                    }
                    dataGridView.Columns[1].Width = width;
                }
                if ((dataGridView == messagesDataGridView ||
                    dataGridView == deadletterDataGridView) &&
                    dataGridView.ColumnCount == 6)
                {
                    var width = dataGridView.Width -
                                dataGridView.RowHeadersWidth -
                                dataGridView.Columns[1].Width -
                                dataGridView.Columns[2].Width;
                    var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                    if (verticalScrollbar != null && verticalScrollbar.Visible)
                    {
                        width -= verticalScrollbar.Width;
                    }
                    var columnWidth = width / 4;
                    dataGridView.Columns[0].Width = columnWidth - 20;
                    dataGridView.Columns[3].Width = columnWidth;
                    dataGridView.Columns[4].Width = columnWidth +(width - (columnWidth * 4)) + 10;
                    dataGridView.Columns[5].Width = columnWidth + 10;
                }
                if (dataGridView == sessionsDataGridView && 
                    dataGridView.ColumnCount == 4)
                {
                    var width = dataGridView.Width - dataGridView.RowHeadersWidth;
                    var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                    if (verticalScrollbar != null && verticalScrollbar.Visible)
                    {
                        width -= verticalScrollbar.Width;
                    }
                    const int columnNumber = 4;
                    var columnWidth = width / columnNumber;
                    for (var i = 0; i < 3; i++)
                    {
                        dataGridView.Columns[i].Width = columnWidth;
                    }
                    dataGridView.Columns[3].Width = columnWidth + (width - (columnWidth * columnNumber));
                }
            }
            finally
            {
                dataGridView.ResumeLayout();
                dataGridView.ResumeDrawing();
            }
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            GetMessages();
        }

        private void btnSessions_Click(object sender, EventArgs e)
        {
            GetMessageSessions();
        }

        private void messagesDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = messagesBindingSource.DataSource as BindingList<BrokeredMessage>;
            currentMessageRowIndex = e.RowIndex;
            if (bindingList == null)
            {
                return;
            }
            if (brokeredMessage != bindingList[e.RowIndex])
            {
                brokeredMessage = bindingList[e.RowIndex];
                messagePropertyGrid.SelectedObject = brokeredMessage;
                txtMessageText.Text = XmlHelper.Indent(serviceBusHelper.GetMessageText(brokeredMessage, out _));
                var listViewItems = brokeredMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, Convert.ToString(p.Value) })).ToArray();
                messagePropertyListView.Items.Clear();
                messagePropertyListView.Items.AddRange(listViewItems);
            }
        }

        private void tabPageMessages_Resize(object sender, EventArgs e)
        {
        }

        private void sessionsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = sessionsBindingSource.DataSource as BindingList<MessageSession>;
            if (bindingList == null)
            {
                return;
            }
            var messageSession = bindingList[e.RowIndex];
            sessionPropertyGrid.SelectedObject = messageSession;
            var stream = messageSession.GetState();
            if (stream == null)
            {
                return;
            }
            using (stream)
            {
                using (var reader = new StreamReader(stream))
                {
                    txtSessionState.Text = reader.ReadToEnd();
                }
            }
        }

        private void grouperMessageList_CustomPaint(PaintEventArgs e)
        {
            messagesDataGridView.Size = new Size(grouperMessageList.Size.Width - (messagesDataGridView.Location.X * 2 + 2),
                                                 grouperMessageList.Size.Height - messagesDataGridView.Location.Y - messagesDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    messagesDataGridView.Location.X - 1,
                                    messagesDataGridView.Location.Y - 1,
                                    messagesDataGridView.Size.Width + 1,
                                    messagesDataGridView.Size.Height + 1);
        }

        private void grouperSessionList_CustomPaint(PaintEventArgs e)
        {
            sessionsDataGridView.Size = new Size(grouperSessionList.Size.Width - (sessionsDataGridView.Location.X * 2 + 2),
                                                 grouperSessionList.Size.Height - sessionsDataGridView.Location.Y - sessionsDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    sessionsDataGridView.Location.X - 1,
                                    sessionsDataGridView.Location.Y - 1,
                                    sessionsDataGridView.Size.Width + 1,
                                    sessionsDataGridView.Size.Height + 1);
        }

        private void grouperDeadletterList_CustomPaint(PaintEventArgs e)
        {
            deadletterDataGridView.Size = new Size(grouperDeadletterList.Size.Width - (messagesDataGridView.Location.X * 2 + 2),
                                                   grouperDeadletterList.Size.Height - messagesDataGridView.Location.Y - messagesDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    deadletterDataGridView.Location.X - 1,
                                    deadletterDataGridView.Location.Y - 1,
                                    deadletterDataGridView.Size.Width + 1,
                                    deadletterDataGridView.Size.Height + 1);
        }

        private void tabPageSessions_Resize(object sender, EventArgs e)
        {
            sessionPropertyGrid.Size = new Size(grouperSessionProperties.Size.Width - 32, sessionPropertyGrid.Size.Height);
        }

        private void deadletterTabPage_Resize(object sender, EventArgs e)
        {
        }

        private void btnDeadletter_Click(object sender, EventArgs e)
        {
            GetDeadletterMessages();
        }

        private void deadletterDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = deadletterBindingSource.DataSource as BindingList<BrokeredMessage>;
            currentDeadletterMessageRowIndex = e.RowIndex;
            if (bindingList == null)
            {
                return;
            }
            if (deadletterMessage != bindingList[e.RowIndex])
            {
                deadletterMessage = bindingList[e.RowIndex];
                deadletterPropertyGrid.SelectedObject = deadletterMessage;
                txtDeadletterText.Text = XmlHelper.Indent(serviceBusHelper.GetMessageText(deadletterMessage, out _));
                var listViewItems = deadletterMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, Convert.ToString(p.Value) })).ToArray();
                deadletterPropertyListView.Items.Clear();
                deadletterPropertyListView.Items.AddRange(listViewItems);
            }
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

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);

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

        private void CalculateLastColumnWidth()
        {
            if (dataPointDataGridView.Columns.Count < 5)
            {
                return;
            }
            var otherColumnsWidth = 0;
            for (var i = 1; i < dataPointDataGridView.Columns.Count; i++)
            {
                otherColumnsWidth += dataPointDataGridView.Columns[i].Width;
            }
            var width = dataPointDataGridView.Width - dataPointDataGridView.RowHeadersWidth - otherColumnsWidth;
            var verticalScrollbar = dataPointDataGridView.Controls.OfType<VScrollBar>().First();
            if (verticalScrollbar != null && verticalScrollbar.Visible)
            {
                width -= verticalScrollbar.Width;
            }
            dataPointDataGridView.Columns[0].Width = width;
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
            var dataGridViewColumn = dataPointDataGridView.Columns[DeleteName];
            if (dataGridViewColumn != null &&
                e.ColumnIndex == dataGridViewColumn.Index &&
                e.RowIndex > -1 &&
               !dataPointDataGridView.Rows[e.RowIndex].IsNewRow)
            {
                dataPointDataGridView.Rows.RemoveAt(e.RowIndex);
                return;
            }
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
                if (!MetricInfo.EntityMetricDictionary.ContainsKey(SubscriptionEntity))
                {
                    return;
                }
                if (metricTabPageIndexList.Count > 0)
                {
                    for (var i = 0; i < metricTabPageIndexList.Count; i++)
                    {
                        mainTabControl.TabPages.RemoveByKey(metricTabPageIndexList[i]);
                    }
                    metricTabPageIndexList.Clear();
                }
                Cursor.Current = Cursors.WaitCursor;
                if (dataPointBindingList.Count == 0)
                {
                    return;
                }
                foreach (var item in dataPointBindingList)
                {
                    item.Entity = string.Format(SubscriptionPathFormat,
                                                subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                subscriptionWrapper.SubscriptionDescription.Name);
                    item.Type = SubscriptionEntity;
                }
                BindingList<MetricDataPoint> pointBindingList;
                var allDataPoint = dataPointBindingList.FirstOrDefault(m => string.Compare(m.Metric, "all", StringComparison.OrdinalIgnoreCase) == 0);
                if (allDataPoint != null)
                {
                    pointBindingList = new BindingList<MetricDataPoint>();
                    foreach (var item in MetricInfo.EntityMetricDictionary[SubscriptionEntity])
                    {
                        if (string.Compare(item.Name, "all", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            continue;
                        }
                        pointBindingList.Add(new MetricDataPoint
                        {
                            Entity = allDataPoint.Entity,
                            FilterOperator1 = allDataPoint.FilterOperator1,
                            FilterOperator2 = allDataPoint.FilterOperator2,
                            FilterValue1 = allDataPoint.FilterValue1,
                            FilterValue2 = allDataPoint.FilterValue2,
                            Granularity = allDataPoint.Granularity,
                            Graph = allDataPoint.Graph,
                            Metric = item.Name,
                            Type = allDataPoint.Type
                        });
                    }
                }
                else
                {
                    pointBindingList = dataPointBindingList;
                }
                var uris = MetricHelper.BuildUriListForDataPointMetricQueries(MainForm.SingletonMainForm.SubscriptionId,
                    serviceBusHelper.Namespace,
                    pointBindingList);
                var uriList = uris as IList<Uri> ?? uris.ToList();
                if (!uriList.Any())
                {
                    return;
                }
                var metricData = MetricHelper.ReadMetricDataUsingTasks(uriList,
                    MainForm.SingletonMainForm.CertificateThumbprint);
                var metricList = metricData as IList<IEnumerable<MetricValue>> ?? metricData.ToList();
                for (var i = 0; i < metricList.Count; i++)
                {
                    if (metricList[i] == null || !metricList[i].Any())
                    {
                        continue;
                    }
                    var key = string.Format(MetricTabPageKeyFormat, i);
                    var metricInfo = MetricInfo.EntityMetricDictionary[SubscriptionEntity].FirstOrDefault(m => m.Name == pointBindingList[i].Metric);
                    var friendlyName = metricInfo != null ? metricInfo.DisplayName : pointBindingList[i].Metric;
                    var unit = metricInfo != null ? metricInfo.Unit : Unknown;
                    mainTabControl.TabPages.Add(key, friendlyName);
                    metricTabPageIndexList.Add(key);
                    var tabPage = mainTabControl.TabPages[key];
                    tabPage.BackColor = Color.FromArgb(215, 228, 242);
                    tabPage.ForeColor = SystemColors.ControlText;
                    var control = new MetricValueControl(writeToLog,
                        () => mainTabControl.TabPages.RemoveByKey(key),
                        metricList[i],
                        pointBindingList[i],
                        metricInfo)
                    {
                        Location = new Point(0, 0),
                        Dock = DockStyle.Fill,
                        Tag = string.Format(GrouperFormat, friendlyName, unit)
                    };
                    mainTabControl.TabPages[key].Controls.Add(control);
                    btnCloseTabs.Enabled = true;
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

        private void messagesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var bindingList = messagesBindingSource.DataSource as BindingList<BrokeredMessage>;
            if (bindingList == null)
            {
                return;
            }
            using (var messageForm = new MessageForm(bindingList[e.RowIndex], serviceBusHelper, writeToLog))
            {
                messageForm.ShowDialog();
            }
        }

        private void deadletterDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var bindingList = deadletterBindingSource.DataSource as BindingList<BrokeredMessage>;
            if (bindingList == null)
            {
                return;
            }
            using (var messageForm = new MessageForm(bindingList[e.RowIndex], serviceBusHelper, writeToLog))
            {
                messageForm.ShowDialog();
            }
        }

        private void messagesDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = messagesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ToolTipText = DoubleClickMessage;
        }

        private void deadletterDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = deadletterDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ToolTipText = DoubleClickMessage;
        }

        private void grouperMessageText_CustomPaint(PaintEventArgs obj)
        {
            txtMessageText.Size = new Size(grouperMessageText.Size.Width - (txtMessageText.Location.X * 2),
                                           grouperMessageText.Size.Height - txtMessageText.Location.Y - txtMessageText.Location.X);
        }

        private void grouperMessageCustomProperties_CustomPaint(PaintEventArgs obj)
        {
            messagePropertyListView.Size = new Size(grouperMessageCustomProperties.Size.Width - (messagePropertyListView.Location.X * 2),
                                                    grouperMessageCustomProperties.Size.Height - messagePropertyListView.Location.Y - messagePropertyListView.Location.X);
        }

        private void grouperMessageProperties_CustomPaint(PaintEventArgs obj)
        {
            messagePropertyGrid.Size = new Size(grouperMessageProperties.Size.Width - (messagePropertyGrid.Location.X * 2),
                                                grouperMessageProperties.Size.Height - messagePropertyGrid.Location.Y - messagePropertyGrid.Location.X);
        }

        private void grouperDeadletterText_CustomPaint(PaintEventArgs obj)
        {
            txtDeadletterText.Size = new Size(grouperDeadletterText.Size.Width - (txtDeadletterText.Location.X * 2),
                                              grouperDeadletterText.Size.Height - txtDeadletterText.Location.Y - txtDeadletterText.Location.X);
        }

        private void grouperDeadletterCustomProperties_CustomPaint(PaintEventArgs obj)
        {
            deadletterPropertyListView.Size = new Size(grouperDeadletterCustomProperties.Size.Width - (deadletterPropertyListView.Location.X * 2),
                                                       grouperDeadletterCustomProperties.Size.Height - deadletterPropertyListView.Location.Y - deadletterPropertyListView.Location.X);
        }

        private void grouperDeadletterProperties_CustomPaint(PaintEventArgs obj)
        {
            deadletterPropertyGrid.Size = new Size(grouperDeadletterProperties.Size.Width - (deadletterPropertyGrid.Location.X * 2),
                                                   grouperDeadletterProperties.Size.Height - deadletterPropertyGrid.Location.Y - deadletterPropertyGrid.Location.X);
        }

        private void grouperSessionState_CustomPaint(PaintEventArgs obj)
        {
            txtSessionState.Size = new Size(grouperSessionState.Size.Width - (txtSessionState.Location.X * 2),
                                            grouperSessionState.Size.Height - txtSessionState.Location.Y - txtSessionState.Location.X);
        }

        private void grouperSessionProperties_CustomPaint(PaintEventArgs obj)
        {
            sessionPropertyGrid.Size = new Size(grouperSessionProperties.Size.Width - (sessionPropertyGrid.Location.X * 2),
                                                grouperSessionProperties.Size.Height - sessionPropertyGrid.Location.Y - sessionPropertyGrid.Location.X);
        }

        private void messagesDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex == -1)
            {
                return;
            }
            messagesDataGridView.Rows[e.RowIndex].Selected = true;
            var multipleSelectedRows = messagesDataGridView.SelectedRows.Count > 1;
            repairAndResubmitMessageToolStripMenuItem.Visible = !multipleSelectedRows;
            saveSelectedMessageToolStripMenuItem.Visible = !multipleSelectedRows;
            resubmitSelectedMessagesInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedMessagesToolStripMenuItem.Visible = multipleSelectedRows;
            messagesContextMenuStrip.Show(Cursor.Position);
        }

        private void repairAndResubmitMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            messagesDataGridView_CellDoubleClick(messagesDataGridView, new DataGridViewCellEventArgs(0, currentMessageRowIndex));
        }

        private void resubmitSelectedMessagesInBatchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (messagesDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                using (var form = new MessageForm(messagesDataGridView.SelectedRows.Cast<DataGridViewRow>()
                                .Select(r => (BrokeredMessage)r.DataBoundItem), serviceBusHelper, writeToLog))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void deadletterDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex == -1)
            {
                return;
            }
            deadletterDataGridView.Rows[e.RowIndex].Selected = true;
            var multipleSelectedRows = deadletterDataGridView.SelectedRows.Count > 1;
            repairAndResubmitDeadletterToolStripMenuItem.Visible = !multipleSelectedRows;
            saveSelectedDeadletteredMessageToolStripMenuItem.Visible = !multipleSelectedRows;
            resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedDeadletteredMessagesToolStripMenuItem.Visible = multipleSelectedRows;
            deadletterContextMenuStrip.Show(Cursor.Position);
        }

        private void repairAndResubmitDeadletterMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deadletterDataGridView_CellDoubleClick(deadletterDataGridView, new DataGridViewCellEventArgs(0, currentDeadletterMessageRowIndex));
        }

        private void resubmitSelectedDeadletterMessagesInBatchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (deadletterDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                using (var form = new MessageForm(deadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
                                .Select(r => (BrokeredMessage)r.DataBoundItem), serviceBusHelper, writeToLog))
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void pictFindMessages_Click(object sender, EventArgs e)
        {
            try
            {
                messagesDataGridView.SuspendDrawing();
                messagesDataGridView.SuspendLayout();
                if (messageBindingList == null)
                {
                    return;
                }
                using (var form = new TextForm(FilterExpressionTitle, FilterExpressionLabel, messagesFilterExpression))
                {
                    form.Size = new Size(600, 200);
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    messagesFilterExpression = form.Content;
                    if (string.IsNullOrWhiteSpace(messagesFilterExpression))
                    {
                        messagesBindingSource.DataSource = messageBindingList;
                        messagesDataGridView.DataSource = messagesBindingSource;
                        writeToLog(FilterExpressionRemovedMessage);
                    }
                    else
                    {
                        Filter filter;
                        try
                        {
                            var sqlFilter = new SqlFilter(messagesFilterExpression);
                            sqlFilter.Validate();
                            filter = sqlFilter.Preprocess();
                        }
                        catch (Exception ex)
                        {
                            writeToLog(string.Format(FilterExpressionNotValidMessage, messagesFilterExpression, ex.Message));
                            return;
                        }
                        var filteredList = messageBindingList.Where(filter.Match).ToList();
                        var bindingList = new SortableBindingList<BrokeredMessage>(filteredList)
                        {
                            AllowEdit = false,
                            AllowNew = false,
                            AllowRemove = false
                        };
                        messagesBindingSource.DataSource = bindingList;
                        messagesDataGridView.DataSource = messagesBindingSource;
                        writeToLog(string.Format(FilterExpressionAppliedMessage, messagesFilterExpression, bindingList.Count));
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                messagesDataGridView.ResumeLayout();
                messagesDataGridView.ResumeDrawing();
            }
        }

        private void pictFindDeadletter_Click(object sender, EventArgs e)
        {
            try
            {
                deadletterDataGridView.SuspendDrawing();
                deadletterDataGridView.SuspendLayout();
                if (deadletterBindingList == null)
                {
                    return;
                }
                using (var form = new TextForm(FilterExpressionTitle, FilterExpressionLabel, deadletterFilterExpression))
                {
                    form.Size = new Size(600, 200);
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    deadletterFilterExpression = form.Content;
                    if (string.IsNullOrWhiteSpace(deadletterFilterExpression))
                    {
                        deadletterBindingSource.DataSource = deadletterBindingList;
                        deadletterDataGridView.DataSource = deadletterBindingSource;
                        writeToLog(FilterExpressionRemovedMessage);
                    }
                    else
                    {
                        Filter filter;
                        try
                        {
                            var sqlFilter = new SqlFilter(deadletterFilterExpression);
                            sqlFilter.Validate();
                            filter = sqlFilter.Preprocess();
                        }
                        catch (Exception ex)
                        {
                            writeToLog(string.Format(FilterExpressionNotValidMessage, deadletterFilterExpression, ex.Message));
                            return;
                        }
                        var filteredList = deadletterBindingList.Where(filter.Match).ToList();
                        var bindingList = new SortableBindingList<BrokeredMessage>(filteredList)
                        {
                            AllowEdit = false,
                            AllowNew = false,
                            AllowRemove = false
                        };
                        deadletterBindingSource.DataSource = bindingList;
                        deadletterDataGridView.DataSource = deadletterBindingSource;
                        writeToLog(string.Format(FilterExpressionAppliedMessage, deadletterFilterExpression, bindingList.Count));
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                deadletterDataGridView.ResumeLayout();
                deadletterDataGridView.ResumeDrawing();
            }
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.Image = Properties.Resources.FindExtensionRaised;
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.Image = Properties.Resources.FindExtension;
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                sorting = true;
            }
        }

        private void dataGridView_Sorted(object sender, EventArgs e)
        {
            sorting = false;
        }

        private void messagesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void deadletterDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void sessionsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
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
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }

        private void saveSelectedMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentMessageRowIndex < 0)
                {
                    return;
                }
                var bindingList = messagesBindingSource.DataSource as BindingList<BrokeredMessage>;
                if (bindingList == null)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMessageText.Text))
                {
                    return;
                }
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = JsonExtension;
                saveFileDialog.Filter = JsonFilter;
                saveFileDialog.FileName = CreateFileName();
                if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.Write(MessageSerializationHelper.Serialize(bindingList[currentMessageRowIndex], txtMessageText.Text));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveSelectedMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (messagesDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                var messages = messagesDataGridView.SelectedRows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem as BrokeredMessage);
                IEnumerable<BrokeredMessage> brokeredMessages = messages as BrokeredMessage[] ?? messages.ToArray();
                if (!brokeredMessages.Any())
                {
                    return;
                }
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = JsonExtension;
                saveFileDialog.Filter = JsonFilter;
                saveFileDialog.FileName = CreateFileName();
                if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm, out _));
                    writer.Write(MessageSerializationHelper.Serialize(brokeredMessages, bodies));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveSelectedDeadletteredMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentDeadletterMessageRowIndex < 0)
                {
                    return;
                }
                var bindingList = deadletterBindingSource.DataSource as BindingList<BrokeredMessage>;
                if (bindingList == null)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDeadletterText.Text))
                {
                    return;
                }
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = JsonExtension;
                saveFileDialog.Filter = JsonFilter;
                saveFileDialog.FileName = CreateFileName();
                if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.Write(MessageSerializationHelper.Serialize(bindingList[currentDeadletterMessageRowIndex], txtDeadletterText.Text));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveSelectedDeadletteredMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (deadletterDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                var messages = deadletterDataGridView.SelectedRows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem as BrokeredMessage);
                IEnumerable<BrokeredMessage> brokeredMessages = messages as BrokeredMessage[] ?? messages.ToArray();
                if (!brokeredMessages.Any())
                {
                    return;
                }
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = JsonExtension;
                saveFileDialog.Filter = JsonFilter;
                saveFileDialog.FileName = CreateFileName();
                if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm, out _));
                    writer.Write(MessageSerializationHelper.Serialize(brokeredMessages, bodies));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private string CreateFileName()
        {
            return string.Format(MessageFileFormat,
                                 CultureInfo.CurrentCulture.TextInfo.ToTitleCase(serviceBusHelper.Namespace),
                                 DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }

        private void btnCloseTabs_Click(object sender, EventArgs e)
        {
            if (metricTabPageIndexList.Count <= 0)
            {
                return;
            }
            for (var i = 0; i < metricTabPageIndexList.Count; i++)
            {
                mainTabControl.TabPages.RemoveByKey(metricTabPageIndexList[i]);
            }
            metricTabPageIndexList.Clear();
            btnCloseTabs.Enabled = false;
        }

        private void mainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (string.Compare(e.TabPage.Name, MetricsTabPage, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                return;
            }
            Task.Run(() =>
            {
                metricsManualResetEvent.WaitOne();
                var dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)dataPointDataGridView.Columns[MetricProperty];
                if (dataGridViewComboBoxColumn != null)
                {
                    dataGridViewComboBoxColumn.DataSource = MetricInfo.EntityMetricDictionary.ContainsKey(SubscriptionEntity)
                        ? MetricInfo.EntityMetricDictionary[SubscriptionEntity]
                        : null;
                }
            });
        }

        private async void btnPurgeMessages_Click(object sender, EventArgs e)
        {
            try
            {
                Application.UseWaitCursor = true;
                await PurgeMessagesAsync();
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }

        private async void btnPurgeDeadletterQueueMessages_Click(object sender, EventArgs e)
        {
            try
            {
                Application.UseWaitCursor = true;
                await PurgeDeadletterQueueMessagesAsync();
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }
        #endregion
    }
}
