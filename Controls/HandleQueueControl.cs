#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
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
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class HandleQueueControl : UserControl
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
        private const int EnableDeadLetteringOnMessageExpirationIndex = 1;
        private const int RequiresDuplicateDetectionIndex = 2;
        private const int RequiresSessionIndex = 3;
        private const int SupportOrderingIndex = 4;
        private const int IsAnonymousAccessibleIndex = 5;

        //***************************
        // Texts
        //***************************
        private const string DeleteText = "Delete";
        private const string CreateText = "Create";
        private const string UpdateText = "Update";
        private const string CancelText = "Cancel";
        private const string EnableText = "Enable";
        private const string DisableText = "Disable";
        private const string QueueEntity = "QueueDescription";
        private const string UserMetadata = "User Metadata";
        private const string MaxGigabytes = "MAX";
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
        private const string PathCannotBeNull = "The Path field cannot be null.";
        private const string MaxDeliveryCountMustBeANumber = "The MaxDeliveryCount field must be a number.";
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

        private const string CannotForwardToItself = "The value of the ForwardTo property of the current queue cannot be set to itself.";
        private const string MessagesPeekedFromTheQueue = "[{0}] messages peeked from the queue [{1}].";
        private const string MessagesPeekedFromTheDeadletterQueue = "[{0}] messages peeked from the deadletter queue of the queue [{1}].";
        private const string MessagesReceivedFromTheQueue = "[{0}] messages received from the queue [{1}].";
        private const string MessagesReceivedFromTheDeadletterQueue = "[{0}] messages received from the deadletter queue of the queue [{1}].";
        private const string SessionsGotFromTheQueue = "[{0}] sessions retrieved for the queue [{1}].";

        private const string RetrieveMessagesFromQueue = "Retrieve messages from queue";
        private const string RetrieveMessagesFromDeadletterQueue = "Retrieve messages from deadletter queue";
        private const string AuthorizationRuleDeleteMessage = "The Authorization Rule will be permanently deleted";
        private const string SelectEntityDialogTitle = "Select a target Queue or Topic";
        private const string SelectEntityGrouperTitle = "Forward To";
        private const string SelectEntityLabelText = "Target Queue or Topic:";

        private const string KeyNameCannotBeNull = "Authorization Rule [{0}]: the KeyName cannot be null";
        private const string PrimaryKeyCannotBeNull = "Authorization Rule [{0}]: the PrimaryKey cannot be null";
        private const string DoubleClickMessage = "Double-click a row to repair and resubmit the corresponding message.";
        private const string MessageSentMessage = "[{0}] messages where sent to [{1}]";
        private const string FilterExpressionTitle = "Define Filter Expression";
        private const string FilterExpressionLabel = "Filter Expression";
        private const string FilterExpressionNotValidMessage = "The filter expression [{0}] is not valid: {1}";
        private const string FilterExpressionAppliedMessage = "The filter expression [{0}] has been successfully applied. [{1}] messages retrieved.";
        private const string FilterExpressionRemovedMessage = "The filter expression has been removed.";

        //***************************
        // Tooltips
        //***************************
        private const string PathTooltip = "Gets or sets the queue path.";
        private const string UserMetadataTooltip = "Gets or sets the user metadata.";
        private const string ForwardToTooltip = "Gets or sets the path to the recipient to which the message is forwarded.";
        private const string MaxQueueSizeTooltip = "Gets or sets the maximum queue size in gigabytes.";
        private const string DefaultMessageTimeToLiveTooltip = "Gets or sets the default message time to live of a queue.";
        private const string DuplicateDetectionHistoryTimeWindowTooltip = "Gets or sets the duration of the time window for duplicate detection history.";
        private const string LockDurationTooltip = "Gets or sets the lock duration timespan associated with this queue.";
        private const string AutoDeleteOnIdleTooltip = "Gets or sets the maximum period of idleness after which the queue is auto deleted.";
        private const string MaxDeliveryCountTooltip = "Gets or sets the maximum delivery count. A message is automatically deadlettered after this number of deliveries.";

        //***************************
        // Property Labels
        //***************************
        private const string Status = "Status";
        private const string SizeInBytes = "Size In Bytes";
        private const string CreatedAt = "Created At";
        private const string AccessedAt = "Accessed At";
        private const string UpdatedAt = "Updated At";
        private const string MessageCount = "Message Count";
        private const string ActiveMessageCount = "Active Message Count";
        private const string DeadletterCount = "DeadLetter Message Count";
        private const string ScheduledMessageCount = "Scheduled Message Count";
        private const string TransferMessageCount = "Transfer Message Count";
        private const string TransferDeadLetterMessageCount = "Transfer DL Message Count";
        private const string IsReadOnly = "IsReadOnly";

        //***************************
        // Constants
        //***************************
        private const long SeviceBusForWindowsServerMaxQueueSize = 8796093022207;

        //***************************
        // Pages
        //***************************
        private const string MessagesTabPage = "tabPageMessages";
        private const string SessionsTabPage = "tabPageSessions";
        private const string DeadletterTabPage = "tabPageDeadletter";
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
        private const string MetricsQueueEntity = "Queue";
        private const string Unknown = "Unkown";
        #endregion

        #region Private Fields
        private QueueDescription queueDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly string path;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private BrokeredMessage brokeredMessage;
        private BrokeredMessage deadletterMessage;
        private int messagesSplitContainerSplitterDistance;
        private int sessionsSplitContainerSplitterDistance;
        private int deadletterSplitContainerSplitterDistance;
        private int grouperMessageCustomPropertiesWidth;
        private int grouperDeadletterCustomPropertiesWidth;
        private readonly BindingSource dataPointBindingSource = new BindingSource();
        private readonly BindingList<MetricDataPoint> dataPointBindingList;
        private int tabIndex;
        private int currentMessageRowIndex;
        private int currentDeadletterMessageRowIndex;
        private bool sorting;
        private string messagesFilterExpression;
        private string deadletterFilterExpression;
        private SortableBindingList<BrokeredMessage> messageBindingList;
        private SortableBindingList<BrokeredMessage> deadletterBindingList;
        private SortableBindingList<MessageSession> sessionBindingList;
        #endregion

        #region Private Static Fields
        private static readonly List<string> claimTypes = new List<string> { "NameIdentifier", "Upn", "Role", "SharedAccessKey" };
        private static readonly List<string> operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> timeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };
        #endregion

        #region Public Constructors
        public HandleQueueControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, QueueDescription queueDescription, string path)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.path = path;
            this.queueDescription = queueDescription;
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
        public void RefreshData(QueueDescription queue)
        {
            try
            {
                queueDescription = queue;
                InitializeData();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void GetMessages(bool peek, bool all, int count)
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
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queueDescription.Path,
                                                                                          ReceiveMode.PeekLock);

                    var messageEnumerable = queueClient.PeekBatch(count);
                    if (messageEnumerable == null)
                    {
                        return;
                    }
                    var messageArray = messageEnumerable as BrokeredMessage[] ?? messageEnumerable.ToArray();
                    brokeredMessages = new List<BrokeredMessage>(messageArray);
                    writeToLog(string.Format(MessagesPeekedFromTheQueue, brokeredMessages.Count, queueDescription.Path));
                }
                else
                {
                    MessageReceiver messageReceiver;
                    if (queueDescription.RequiresSession)
                    {
                        var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queueDescription.Path,
                                                                                              ReceiveMode.ReceiveAndDelete);
                        messageReceiver = queueClient.AcceptMessageSession(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ServerTimeout));
                    }
                    else
                    {
                        messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(queueDescription.Path,
                                                                                                  ReceiveMode.ReceiveAndDelete);
                    }

                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = messageReceiver.ReceiveBatch(all ?
                                                                MainForm.SingletonMainForm.TopCount :
                                                                count - totalRetrieved,
                                                                TimeSpan.FromSeconds(1));
                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Count();
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(enumerable);
                    }
                    while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesReceivedFromTheQueue, brokeredMessages.Count, queueDescription.Path));
                }
                messageBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };
                messagesBindingSource.DataSource = messageBindingList;
                messagesDataGridView.DataSource = messagesBindingSource;
                messagesSplitContainer.SplitterDistance = messagesSplitContainerSplitterDistance;
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

        public void GetDeadletterMessages(bool peek, bool all, int count)
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageDeadletter.SuspendDrawing();
                tabPageDeadletter.SuspendLayout();

                Cursor.Current = Cursors.WaitCursor;
                var brokeredMessages = new List<BrokeredMessage>();
                var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(QueueClient.FormatDeadLetterPath(queueDescription.Path),
                                                                                      ReceiveMode.PeekLock);

                if (peek)
                {

                    var messageEnumerable = queueClient.PeekBatch(count);
                    if (messageEnumerable == null)
                    {
                        return;
                    }
                    var messageArray = messageEnumerable as BrokeredMessage[] ?? messageEnumerable.ToArray();
                    brokeredMessages = new List<BrokeredMessage>(messageArray);
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }
                else
                {
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = queueClient.ReceiveBatch(all ?
                                                                MainForm.SingletonMainForm.TopCount :
                                                                count - totalRetrieved,
                                                                TimeSpan.FromSeconds(1));
                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Count();
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(enumerable);
                    }
                    while (retrieved > 0 && (all || count > totalRetrieved));
                    queueClient.CompleteBatch(brokeredMessages.Select(bm => bm.LockToken));
                    writeToLog(string.Format(MessagesReceivedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }

                deadletterBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };

                deadletterBindingSource.DataSource = deadletterBindingList;
                deadletterDataGridView.DataSource = deadletterBindingSource;
                deadletterSplitContainer.SplitterDistance = deadletterSplitContainerSplitterDistance;
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

        public void GetMessageSessions()
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageSessions.SuspendDrawing();
                tabPageSessions.SuspendLayout();

                var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queueDescription.Path,
                                                                                      ReceiveMode.PeekLock);
                var sessionEnumerable = queueClient.GetMessageSessions();
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
                writeToLog(string.Format(SessionsGotFromTheQueue, sessionBindingList.Count, queueDescription.Path));
                sessionsBindingSource.DataSource = sessionBindingList;
                sessionsDataGridView.DataSource = sessionsBindingSource;
                sessionsSplitContainer.SplitterDistance = sessionsSplitContainerSplitterDistance;
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
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            trackBarMaxQueueSize.Maximum = serviceBusHelper.IsCloudNamespace ? 5 : 11;

            // Splitter controls
            messagesSplitContainer.SplitterWidth = 16;
            sessionsSplitContainer.SplitterWidth = 16;
            deadletterSplitContainer.SplitterWidth = 16;
            messagesCustomPropertiesSplitContainer.SplitterWidth = 16;
            deadletterCustomPropertiesSplitContainer.SplitterWidth = 16;
            messageListTextPropertiesSplitContainer.SplitterWidth = 8;
            sessionListTextPropertiesSplitContainer.SplitterWidth = 8;
            deadletterListTextPropertiesSplitContainer.SplitterWidth = 8;

            // Tab pages
            DisablePage(MessagesTabPage);
            DisablePage(SessionsTabPage);
            DisablePage(DeadletterTabPage);

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

            if (queueDescription != null)
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
                txtPath.ReadOnly = true;
                txtPath.BackColor = SystemColors.Window;
                txtPath.GotFocus += textBox_GotFocus;

                txtMessageText.ReadOnly = true;
                txtMessageText.BackColor = SystemColors.Window;
                txtMessageText.GotFocus += textBox_GotFocus;

                txtDeadletterText.ReadOnly = true;
                txtDeadletterText.BackColor = SystemColors.Window;
                txtDeadletterText.GotFocus += textBox_GotFocus;

                txtSessionState.ReadOnly = true;
                txtSessionState.BackColor = SystemColors.Window;
                txtSessionState.GotFocus += textBox_GotFocus;

                trackBarMaxQueueSize.Enabled = false;

                // Initialize Controls with Data
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

                messagesSplitContainerSplitterDistance = messagesSplitContainer.SplitterDistance;
                sessionsSplitContainerSplitterDistance = sessionsSplitContainer.SplitterDistance;
                deadletterSplitContainerSplitterDistance = deadletterSplitContainer.SplitterDistance;
                grouperMessageCustomPropertiesWidth = grouperMessageCustomProperties.Size.Width;
                grouperDeadletterCustomPropertiesWidth = grouperDeadletterCustomProperties.Size.Width;

                checkedListBox.ItemCheck += checkedListBox_ItemCheck;

                toolTip.SetToolTip(txtPath, PathTooltip);
                toolTip.SetToolTip(txtUserMetadata, UserMetadataTooltip);
                toolTip.SetToolTip(txtForwardTo, ForwardToTooltip);
                toolTip.SetToolTip(trackBarMaxQueueSize, MaxQueueSizeTooltip);
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
                toolTip.SetToolTip(txtLockDurationDays, LockDurationTooltip);
                toolTip.SetToolTip(txtLockDurationHours, LockDurationTooltip);
                toolTip.SetToolTip(txtLockDurationMinutes, LockDurationTooltip);
                toolTip.SetToolTip(txtLockDurationSeconds, LockDurationTooltip);
                toolTip.SetToolTip(txtLockDurationMilliseconds, LockDurationTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleDays, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleHours, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleMinutes, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleSeconds, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtAutoDeleteOnIdleMilliseconds, AutoDeleteOnIdleTooltip);
                toolTip.SetToolTip(txtMaxDeliveryCount, MaxDeliveryCountTooltip);
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
                if (queueDescription.Authorization.Count > 0 && queueDescription.Authorization.Count > e.NewIndex)
                {
                    var rule = queueDescription.Authorization.ElementAt(e.NewIndex);
                    if (rule != null)
                    {
                        queueDescription.Authorization.Remove(rule);
                    }
                }
            }
        }

        private void InitializeData()
        {
            // Initialize buttons
            btnCreateDelete.Text = DeleteText;
            btnCancelUpdate.Text = UpdateText;
            btnChangeStatus.Text = queueDescription.Status == EntityStatus.Active ? DisableText : EnableText;
            btnRefresh.Visible = true;
            btnChangeStatus.Visible = true;
            btnMessages.Visible = true;
            btnSessions.Visible = queueDescription.RequiresSession;
            btnMessages.Visible = string.IsNullOrWhiteSpace(queueDescription.ForwardTo);

            if (!btnMessages.Visible && !btnSessions.Visible)
            {
                btnMetrics.Location = btnMessages.Location;
            }
            if (!btnMessages.Visible && btnSessions.Visible)
            {
                btnMetrics.Location = btnSessions.Location;
                btnSessions.Location = btnMessages.Location;
            }
            if (btnMessages.Visible && !btnSessions.Visible)
            {
                btnMetrics.Location = btnSessions.Location;
            }
            btnMetrics.Visible = serviceBusHelper.IsCloudNamespace;
            btnDeadletter.Visible = true;

            // Authorization Rules
            BindingList<AuthorizationRuleWrapper> bindingList;
            if (queueDescription.Authorization.Count > 0)
            {
                var enumerable = queueDescription.Authorization.Select(r => new AuthorizationRuleWrapper(r));
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

            propertyList.AddRange(new[]
                    {
                        new[] {Status, queueDescription.Status.ToString()},
                        new[] {IsReadOnly, queueDescription.IsReadOnly.ToString()},
                        new[] {SizeInBytes, queueDescription.SizeInBytes.ToString("N0")},
                        new[] {CreatedAt, queueDescription.CreatedAt.ToString(CultureInfo.CurrentCulture)},
                        new[] {AccessedAt, queueDescription.AccessedAt.ToString(CultureInfo.CurrentCulture)},
                        new[] {UpdatedAt, queueDescription.UpdatedAt.ToString(CultureInfo.CurrentCulture)},
                        new[] {ActiveMessageCount, queueDescription.MessageCountDetails.ActiveMessageCount.ToString("N0")},
                        new[] {DeadletterCount, queueDescription.MessageCountDetails.DeadLetterMessageCount.ToString("N0")},
                        new[] {ScheduledMessageCount, queueDescription.MessageCountDetails.ScheduledMessageCount.ToString("N0")},
                        new[] {TransferMessageCount, queueDescription.MessageCountDetails.TransferMessageCount.ToString("N0")},
                        new[] {TransferDeadLetterMessageCount, queueDescription.MessageCountDetails.TransferDeadLetterMessageCount.ToString("N0")},
                        new[] {MessageCount, queueDescription.MessageCount.ToString("N0")}
                    });

            propertyListView.Items.Clear();
            foreach (var array in propertyList)
            {
                propertyListView.Items.Add(new ListViewItem(array));
            }

            // Path
            if (!string.IsNullOrWhiteSpace(queueDescription.Path))
            {
                txtPath.Text = queueDescription.Path;
            }

            // UserMetadata
            if (!string.IsNullOrWhiteSpace(queueDescription.UserMetadata))
            {
                txtUserMetadata.Text = queueDescription.UserMetadata;
            }

            // ForwardTo
            if (!string.IsNullOrWhiteSpace(queueDescription.ForwardTo))
            {
                int i;
                txtForwardTo.Text = !string.IsNullOrWhiteSpace(queueDescription.ForwardTo) &&
                                    (i = queueDescription.ForwardTo.IndexOf('/')) > 0 &&
                                    i < queueDescription.ForwardTo.Length - 1 ?
                                    queueDescription.ForwardTo.Substring(queueDescription.ForwardTo.LastIndexOf('/') + 1) :
                                    queueDescription.ForwardTo;

            }

            // MaxQueueSizeInBytes
            trackBarMaxQueueSize.Value = serviceBusHelper.IsCloudNamespace
                                             ? (int)queueDescription.MaxSizeInMegabytes / 1024
                                             : queueDescription.MaxSizeInMegabytes == SeviceBusForWindowsServerMaxQueueSize
                                                   ? 11
                                                   : (int)queueDescription.MaxSizeInMegabytes / 1024;

            // MaxDeliveryCount
            txtMaxDeliveryCount.Text = queueDescription.MaxDeliveryCount.ToString(CultureInfo.InvariantCulture);

            // DefaultMessageTimeToLive
            txtDefaultMessageTimeToLiveDays.Text = queueDescription.DefaultMessageTimeToLive.Days.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveHours.Text = queueDescription.DefaultMessageTimeToLive.Hours.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveMinutes.Text = queueDescription.DefaultMessageTimeToLive.Minutes.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveSeconds.Text = queueDescription.DefaultMessageTimeToLive.Seconds.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveMilliseconds.Text = queueDescription.DefaultMessageTimeToLive.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // DuplicateDetectionHistoryTimeWindow
            txtDuplicateDetectionHistoryTimeWindowDays.Text = queueDescription.DuplicateDetectionHistoryTimeWindow.Days.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowHours.Text = queueDescription.DuplicateDetectionHistoryTimeWindow.Hours.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowMinutes.Text = queueDescription.DuplicateDetectionHistoryTimeWindow.Minutes.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowSeconds.Text = queueDescription.DuplicateDetectionHistoryTimeWindow.Seconds.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text = queueDescription.DuplicateDetectionHistoryTimeWindow.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // LockDuration
            txtLockDurationDays.Text = queueDescription.LockDuration.Days.ToString(CultureInfo.InvariantCulture);
            txtLockDurationHours.Text = queueDescription.LockDuration.Hours.ToString(CultureInfo.InvariantCulture);
            txtLockDurationMinutes.Text = queueDescription.LockDuration.Minutes.ToString(CultureInfo.InvariantCulture);
            txtLockDurationSeconds.Text = queueDescription.LockDuration.Seconds.ToString(CultureInfo.InvariantCulture);
            txtLockDurationMilliseconds.Text = queueDescription.LockDuration.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // AutoDeleteOnIdle
            txtAutoDeleteOnIdleDays.Text = queueDescription.AutoDeleteOnIdle.Days.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleHours.Text = queueDescription.AutoDeleteOnIdle.Hours.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleMinutes.Text = queueDescription.AutoDeleteOnIdle.Minutes.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleSeconds.Text = queueDescription.AutoDeleteOnIdle.Seconds.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleMilliseconds.Text = queueDescription.AutoDeleteOnIdle.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // EnableBatchedOperations
            checkedListBox.SetItemChecked(EnableBatchedOperationsIndex,
                                          queueDescription.EnableBatchedOperations);

            // EnableDeadLetteringOnMessageExpiration
            checkedListBox.SetItemChecked(EnableDeadLetteringOnMessageExpirationIndex,
                                          queueDescription.EnableDeadLetteringOnMessageExpiration);

            // RequiresDuplicateDetection
            checkedListBox.SetItemChecked(RequiresDuplicateDetectionIndex,
                                          queueDescription.RequiresDuplicateDetection);

            // RequiresSession
            checkedListBox.SetItemChecked(RequiresSessionIndex,
                                          queueDescription.RequiresSession);

            // SupportOrdering
            checkedListBox.SetItemChecked(SupportOrderingIndex,
                                          queueDescription.SupportOrdering);

            // IsAnonymousAccessible
            if (!serviceBusHelper.IsCloudNamespace)
            {
                checkedListBox.SetItemChecked(IsAnonymousAccessibleIndex,
                                              queueDescription.IsAnonymousAccessible);
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
                    using (var deleteForm = new DeleteForm(queueDescription.Path, QueueEntity.ToLower()))
                    {
                        if (deleteForm.ShowDialog() == DialogResult.OK)
                        {
                            serviceBusHelper.DeleteQueue(queueDescription);
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

                    if (txtPath.Text.Trim().ToLower() == txtForwardTo.Text.Trim().ToLower())
                    {
                        writeToLog(CannotForwardToItself);
                        txtForwardTo.Focus();
                        return;
                    }

                    var description = new QueueDescription(txtPath.Text)
                    {
                        UserMetadata = txtUserMetadata.Text,
                        ForwardTo = txtForwardTo.Text,
                        MaxSizeInMegabytes = serviceBusHelper.IsCloudNamespace
                                             ? trackBarMaxQueueSize.Value * 1024
                                             : trackBarMaxQueueSize.Value == trackBarMaxQueueSize.Maximum
                                                   ? SeviceBusForWindowsServerMaxQueueSize
                                                   : trackBarMaxQueueSize.Value * 1024
                    };

                    if (!string.IsNullOrWhiteSpace(txtMaxDeliveryCount.Text))
                    {
                        int value;
                        if (int.TryParse(txtMaxDeliveryCount.Text, out value))
                        {
                            description.MaxDeliveryCount = value;
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
                        description.LockDuration = new TimeSpan(days, hours, minutes, seconds, milliseconds);
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
                    description.EnableDeadLetteringOnMessageExpiration = checkedListBox.GetItemChecked(EnableDeadLetteringOnMessageExpirationIndex);
                    description.RequiresDuplicateDetection = checkedListBox.GetItemChecked(RequiresDuplicateDetectionIndex);
                    description.RequiresSession = checkedListBox.GetItemChecked(RequiresSessionIndex);
                    description.SupportOrdering = checkedListBox.GetItemChecked(SupportOrderingIndex);
                    if (!serviceBusHelper.IsCloudNamespace)
                    {
                        description.IsAnonymousAccessible = checkedListBox.GetItemChecked(IsAnonymousAccessibleIndex);
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


                    queueDescription = serviceBusHelper.CreateQueue(description);
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

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (queueDescription == null)
            {
                return;
            }
            if (e.Index == RequiresSessionIndex)
            {
                e.NewValue = queueDescription.RequiresSession ? CheckState.Checked : CheckState.Unchecked;
            }
            if (e.Index == RequiresDuplicateDetectionIndex)
            {
                e.NewValue = queueDescription.RequiresDuplicateDetection ? CheckState.Checked : CheckState.Unchecked;
            }
            if (e.Index == IsAnonymousAccessibleIndex)
            {
                e.NewValue = queueDescription.IsAnonymousAccessible ? CheckState.Checked : CheckState.Unchecked;
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
                    if (txtPath.Text.Trim().ToLower() == txtForwardTo.Text.Trim().ToLower())
                    {
                        writeToLog(CannotForwardToItself);
                        txtForwardTo.Focus();
                        return;
                    }

                    queueDescription.UserMetadata = txtUserMetadata.Text;
                    queueDescription.ForwardTo = txtForwardTo.Text;

                    if (!string.IsNullOrWhiteSpace(txtMaxDeliveryCount.Text))
                    {
                        int value;
                        if (int.TryParse(txtMaxDeliveryCount.Text, out value))
                        {
                            queueDescription.MaxDeliveryCount = value;
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
                        var timeSpan = new TimeSpan(days, hours, minutes, seconds, milliseconds);
                        if (!timeSpan.IsMaxValue())
                        {
                            queueDescription.DefaultMessageTimeToLive = timeSpan;
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
                        queueDescription.DuplicateDetectionHistoryTimeWindow = new TimeSpan(days, hours, minutes,
                                                                                            seconds, milliseconds);
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
                        queueDescription.LockDuration = new TimeSpan(days, hours, minutes, seconds, milliseconds);
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
                            queueDescription.AutoDeleteOnIdle = timeSpan;
                        }
                    }

                    queueDescription.EnableBatchedOperations = checkedListBox.GetItemChecked(EnableBatchedOperationsIndex);
                    queueDescription.EnableDeadLetteringOnMessageExpiration = checkedListBox.GetItemChecked(EnableDeadLetteringOnMessageExpirationIndex);
                    queueDescription.SupportOrdering = checkedListBox.GetItemChecked(SupportOrderingIndex);

                    if (!serviceBusHelper.IsCloudNamespace)
                    {
                        queueDescription.IsAnonymousAccessible = checkedListBox.GetItemChecked(IsAnonymousAccessibleIndex);
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
                                    queueDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rightList));
                                }
                                else if (string.IsNullOrWhiteSpace(rule.SecondaryKey))

                                {
                                    queueDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rule.PrimaryKey,
                                                                                                         rightList));
                                }
                                else
                                {
                                    queueDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                                                                                         rule.PrimaryKey,
                                                                                                         rule.SecondaryKey,
                                                                                                         rightList));
                                }
                            }
                            else
                            {
                                queueDescription.Authorization.Add(new AllowRule(rule.IssuerName,
                                                                                 rule.ClaimType,
                                                                                 rule.ClaimValue,
                                                                                 rightList));
                            }
                        }
                    }

                    queueDescription.Status = EntityStatus.Disabled;
                    serviceBusHelper.UpdateQueue(queueDescription);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    queueDescription = serviceBusHelper.GetQueue(queueDescription.Path);
                }
                finally
                {
                    queueDescription.Status = EntityStatus.Active;
                    queueDescription = serviceBusHelper.NamespaceManager.UpdateQueue(queueDescription);
                    InitializeData();
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

        private void trackBarMaxQueueSize_ValueChanged(object sender, decimal value)
        {
            lblMaxQueueSizeInGB.Text = string.Format(SizeInGigabytes, trackBarMaxQueueSize.Value);
            if (!serviceBusHelper.IsCloudNamespace &&
                trackBarMaxQueueSize.Value == trackBarMaxQueueSize.Maximum)
            {
                lblMaxQueueSizeInGB.Text = MaxGigabytes;
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
                using (var backBrush = new SolidBrush(tabControl.Parent.BackColor))
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
                        tabBackgroundRect = new Rectangle(e.Bounds.X, e.Bounds.Y - 2, e.Bounds.Width, e.Bounds.Height + 4);
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
            if (dataGridView == null)
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
                    if (verticalScrollbar.Visible)
                    {
                        width -= verticalScrollbar.Width;
                    }
                    dataGridView.Columns[1].Width = width;
                }
                if (dataGridView == messagesDataGridView ||
                    dataGridView == deadletterDataGridView)
                {
                    var width = dataGridView.Width -
                                dataGridView.RowHeadersWidth -
                                dataGridView.Columns[1].Width -
                                dataGridView.Columns[2].Width;
                    var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                    if (verticalScrollbar.Visible)
                    {
                        width -= verticalScrollbar.Width;
                    }
                    var columnWidth = width / 4;
                    dataGridView.Columns[0].Width = columnWidth - 20;
                    dataGridView.Columns[3].Width = columnWidth;
                    dataGridView.Columns[4].Width = columnWidth + (width - (columnWidth * 4)) + 10;
                    dataGridView.Columns[5].Width = columnWidth + 10;
                }
                if (dataGridView == sessionsDataGridView)
                {
                    var width = dataGridView.Width - dataGridView.RowHeadersWidth;
                    var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                    if (verticalScrollbar.Visible)
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
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromQueue, MainForm.SingletonMainForm.TopCount))
            {
                if (receiveModeForm.ShowDialog() == DialogResult.OK)
                {
                    txtMessageText.Text = string.Empty;
                    messagePropertyListView.Items.Clear();
                    messagePropertyGrid.SelectedObject = null;
                    GetMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count);
                }
            }
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
            if (brokeredMessage == bindingList[e.RowIndex])
            {
                return;
            }
            brokeredMessage = bindingList[e.RowIndex];
            messagePropertyGrid.SelectedObject = brokeredMessage;
            BodyType bodyType;
            txtMessageText.Text = XmlHelper.Indent(serviceBusHelper.GetMessageText(brokeredMessage, out bodyType));
            var listViewItems = brokeredMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, Convert.ToString(p.Value) })).ToArray();
            messagePropertyListView.Items.Clear();
            messagePropertyListView.Items.AddRange(listViewItems);
        }

        private void tabPageMessages_Resize(object sender, EventArgs e)
        {
            try
            {
                messagesSplitContainer.SuspendDrawing();
                messagesSplitContainer.SuspendLayout();
                grouperMessageCustomProperties.Size = new Size(grouperMessageCustomProperties.Size.Width, messageListTextPropertiesSplitContainer.Panel2.Size.Height);
                messagePropertyGrid.Size = new Size(grouperMessageProperties.Size.Width - 32, messagePropertyGrid.Size.Height);
                messagePropertyListView.Size = new Size(grouperMessageCustomProperties.Size.Width - 32, messagePropertyListView.Size.Height);
                messagesCustomPropertiesSplitContainer.SplitterDistance = messagesCustomPropertiesSplitContainer.Width -
                                                                            grouperMessageCustomPropertiesWidth -
                                                                            messagesCustomPropertiesSplitContainer.SplitterWidth;
                grouperMessageCustomPropertiesWidth = grouperMessageCustomProperties.Width;
            }
            finally
            {
                messagesSplitContainer.ResumeDrawing();
                messagesSplitContainer.ResumeLayout();
            }
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

        private void grouperAuthorizationRuleList_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    authorizationRulesDataGridView.Location.X - 1,
                                    authorizationRulesDataGridView.Location.Y - 1,
                                    authorizationRulesDataGridView.Size.Width + 1,
                                    authorizationRulesDataGridView.Size.Height + 1);
        }

        private void tabPageSessions_Resize(object sender, EventArgs e)
        {
            sessionPropertyGrid.Size = new Size(grouperSessionProperties.Size.Width - 32, sessionPropertyGrid.Size.Height);
        }

        private void deadletterTabPage_Resize(object sender, EventArgs e)
        {
            try
            {
                deadletterSplitContainer.SuspendDrawing();
                deadletterSplitContainer.SuspendLayout();
                grouperDeadletterCustomProperties.Size = new Size(grouperDeadletterCustomProperties.Size.Width, deadletterListTextPropertiesSplitContainer.Panel2.Size.Height);
                deadletterPropertyGrid.Size = new Size(grouperDeadletterProperties.Size.Width - 32, deadletterPropertyGrid.Size.Height);
                deadletterPropertyListView.Size = new Size(grouperDeadletterCustomProperties.Size.Width - 32, deadletterPropertyListView.Size.Height);
                deadletterCustomPropertiesSplitContainer.SplitterDistance = deadletterCustomPropertiesSplitContainer.Width -
                                                                            grouperDeadletterCustomPropertiesWidth -
                                                                            deadletterCustomPropertiesSplitContainer.SplitterWidth;
                grouperDeadletterCustomPropertiesWidth = grouperDeadletterCustomProperties.Width;
            }
            finally
            {
                deadletterSplitContainer.ResumeDrawing();
                deadletterSplitContainer.ResumeLayout();
            }
        }

        private void btnDeadletter_Click(object sender, EventArgs e)
        {
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromDeadletterQueue, MainForm.SingletonMainForm.TopCount))
            {
                if (receiveModeForm.ShowDialog() == DialogResult.OK)
                {
                    txtDeadletterText.Text = string.Empty;
                    deadletterPropertyListView.Items.Clear();
                    deadletterPropertyGrid.SelectedObject = null;
                    GetDeadletterMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count);
                }
            }
        }

        private void deadletterDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = deadletterBindingSource.DataSource as BindingList<BrokeredMessage>;
            currentDeadletterMessageRowIndex = e.RowIndex;
            if (bindingList == null)
            {
                return;
            }
            if (deadletterMessage == bindingList[e.RowIndex])
            {
                return;
            }
            deadletterMessage = bindingList[e.RowIndex];
            deadletterPropertyGrid.SelectedObject = deadletterMessage;
            BodyType bodyType;
            txtDeadletterText.Text = XmlHelper.Indent(serviceBusHelper.GetMessageText(deadletterMessage, out bodyType));
            var listViewItems = deadletterMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, Convert.ToString(p.Value) })).ToArray();
            deadletterPropertyListView.Items.Clear();
            deadletterPropertyListView.Items.AddRange(listViewItems);
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
                    item.Entity = queueDescription.Path;
                    item.Type = MetricsQueueEntity;
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
            resubmitSelectedMessagesInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            messagesContextMenuStrip.Show(Cursor.Position);
        }

        private void repairAndResubmitMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            messagesDataGridView_CellDoubleClick(messagesDataGridView, new DataGridViewCellEventArgs(0, currentMessageRowIndex));
        }

        private async void resubmitSelectedMessagesInBatchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (messagesDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                string entityPath;
                using (var form = new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(form.Path))
                    {
                        return;
                    }
                    entityPath = form.Path;
                }
                var sent = 0;
                var messageSender = await serviceBusHelper.MessagingFactory.CreateMessageSenderAsync(entityPath);
                var messages = messagesDataGridView.SelectedRows.Cast<DataGridViewRow>().Select(r =>
                {
                    BodyType bodyType;
                    var message = r.DataBoundItem as BrokeredMessage;
                    var messageText = serviceBusHelper.GetMessageText(message, out bodyType);
                    if (bodyType == BodyType.Wcf)
                    {
                        var wcfUri = serviceBusHelper.IsCloudNamespace ?
                                         new Uri(serviceBusHelper.NamespaceUri, messageSender.Path) :
                                         new UriBuilder
                                         {
                                             Host = serviceBusHelper.NamespaceUri.Host,
                                             Path = string.Format("{0}/{1}", serviceBusHelper.NamespaceUri.AbsolutePath, messageSender.Path),
                                             Scheme = "sb"
                                         }.Uri;
                        return serviceBusHelper.CreateMessageForWcfReceiver(message,
                                                                            0,
                                                                            false,
                                                                            false,
                                                                            messageText,
                                                                            wcfUri);
                    }
                    return serviceBusHelper.CreateMessageForApiReceiver(message,
                                                                        0,
                                                                        false,
                                                                        false,
                                                                        messageText,
                                                                        bodyType);
                });
                IEnumerable<BrokeredMessage> brokeredMessages = messages as IList<BrokeredMessage> ?? messages.ToList();
                if (brokeredMessages.Any())
                {
                    sent = brokeredMessages.Count();
                    await messageSender.SendBatchAsync(brokeredMessages);
                }
                writeToLog(string.Format(MessageSentMessage, sent, entityPath));
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
            resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            deadletterContextMenuStrip.Show(Cursor.Position);
        }

        private void repairAndResubmitDeadletterMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deadletterDataGridView_CellDoubleClick(deadletterDataGridView, new DataGridViewCellEventArgs(0, currentDeadletterMessageRowIndex));
        }

        private async void resubmitSelectedDeadletterMessagesInBatchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (deadletterDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                string entityPath;
                using (var form = new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(form.Path))
                    {
                        return;
                    }
                    entityPath = form.Path;
                }
                var sent = 0;
                var messageSender = await serviceBusHelper.MessagingFactory.CreateMessageSenderAsync(entityPath);
                var messages = deadletterDataGridView.SelectedRows.Cast<DataGridViewRow>().Select(r =>
                {
                    BodyType bodyType;
                    var message = r.DataBoundItem as BrokeredMessage;
                    var messageText = serviceBusHelper.GetMessageText(message, out bodyType);
                    if (bodyType == BodyType.Wcf)
                    {
                        var wcfUri = serviceBusHelper.IsCloudNamespace ?
                                         new Uri(serviceBusHelper.NamespaceUri, messageSender.Path) :
                                         new UriBuilder
                                         {
                                             Host = serviceBusHelper.NamespaceUri.Host,
                                             Path = string.Format("{0}/{1}", serviceBusHelper.NamespaceUri.AbsolutePath, messageSender.Path),
                                             Scheme = "sb"
                                         }.Uri;
                        return serviceBusHelper.CreateMessageForWcfReceiver(message,
                                                                            0,
                                                                            false,
                                                                            false,
                                                                            messageText,
                                                                            wcfUri);
                    }
                    return serviceBusHelper.CreateMessageForApiReceiver(message,
                                                                        0,
                                                                        false,
                                                                        false,
                                                                        messageText,
                                                                        bodyType);
                });
                IEnumerable<BrokeredMessage> brokeredMessages = messages as IList<BrokeredMessage> ?? messages.ToList();
                if (brokeredMessages.Any())
                {
                    sent = brokeredMessages.Count();
                    await messageSender.SendBatchAsync(brokeredMessages);
                }
                writeToLog(string.Format(MessageSentMessage, sent, entityPath));
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
        #endregion
    }
}