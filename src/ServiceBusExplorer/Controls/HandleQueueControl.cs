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
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using Microsoft.ServiceBus.Messaging;
using System.Threading.Tasks;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
using ServiceBusExplorer.Utilities.Helpers;
using ServiceBusExplorer.ServiceBus.Helpers;

#endregion

// ReSharper disable once CheckNamespace

namespace ServiceBusExplorer.Controls
{
    public partial class HandleQueueControl : UserControl
    {
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
        private const int EnablePartitioningIndex = 2;
        private const int EnableExpressIndex = 3;
        private const int RequiresDuplicateDetectionIndex = 4;
        private const int RequiresSessionIndex = 5;
        private const int SupportOrderingIndex = 6;
        private const int IsAnonymousAccessibleIndex = 7;

        //***************************
        // Texts
        //***************************
        private const string DeleteText = "Delete";
        private const string CreateText = "Create";
        private const string UpdateText = "Update";
        private const string CancelText = "Cancel";
        private const string EnableText = "Enable";
        private const string DisableText = "Disable";
        private const string StatusText = "Status";
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

        private const string DefaultMessageTimeToLiveDaysMustBeANumber =
            "The Days value of the DefaultMessageTimeToLive field must be a number.";

        private const string DefaultMessageTimeToLiveHoursMustBeANumber =
            "The Hours value of the DefaultMessageTimeToLive field must be a number.";

        private const string DefaultMessageTimeToLiveMinutesMustBeANumber =
            "The Minutes value of the DefaultMessageTimeToLive field must be a number.";

        private const string DefaultMessageTimeToLiveSecondsMustBeANumber =
            "The Seconds value of the DefaultMessageTimeToLive field must be a number.";

        private const string DefaultMessageTimeToLiveMillisecondsMustBeANumber =
            "The Milliseconds value of the DefaultMessageTimeToLive field must be a number.";

        private const string DuplicateDetectionHistoryTimeWindowDaysMustBeANumber =
            "The Days value of the DuplicateDetectionHistoryTimeWindow field must be a number.";

        private const string DuplicateDetectionHistoryTimeWindowHoursMustBeANumber =
            "The Hours value of the DuplicateDetectionHistoryTimeWindow field must be a number.";

        private const string DuplicateDetectionHistoryTimeWindowMinutesMustBeANumber =
            "The Minutes value of the DuplicateDetectionHistoryTimeWindow field must be a number.";

        private const string DuplicateDetectionHistoryTimeWindowSecondsMustBeANumber =
            "The Seconds value of the DuplicateDetectionHistoryTimeWindow field must be a number.";

        private const string DuplicateDetectionHistoryTimeWindowMillisecondsMustBeANumber =
            "The Milliseconds value of the DuplicateDetectionHistoryTimeWindow field must be a number.";

        private const string LockDurationDaysMustBeANumber =
            "The Days value of the LockDuration field must be a number.";

        private const string LockDurationHoursMustBeANumber =
            "The Hours value of the LockDuration field must be a number.";

        private const string LockDurationMinutesMustBeANumber =
            "The Minutes value of the LockDuration field must be a number.";

        private const string LockDurationSecondsMustBeANumber =
            "The Seconds value of the LockDuration field must be a number.";

        private const string LockDurationMillisecondsMustBeANumber =
            "The Milliseconds value of the LockDuration field must be a number.";

        private const string AutoDeleteOnIdleDaysMustBeANumber =
            "The Days value of the AutoDeleteOnIdle field must be a number.";

        private const string AutoDeleteOnIdleHoursMustBeANumber =
            "The Hours value of the AutoDeleteOnIdle field must be a number.";

        private const string AutoDeleteOnIdleMinutesMustBeANumber =
            "The Minutes value of the AutoDeleteOnIdle field must be a number.";

        private const string AutoDeleteOnIdleSecondsMustBeANumber =
            "The Seconds value of the AutoDeleteOnIdle field must be a number.";

        private const string AutoDeleteOnIdleMillisecondsMustBeANumber =
            "The Milliseconds value of the AutoDeleteOnIdle field must be a number.";

        private const string CannotForwardToItself =
            "The value of the ForwardTo property of the current queue cannot be set to itself.";

        private const string CannotForwardDeadLetteredMessagesToItself =
            "The value of the ForwardDeadLetteredMessagesTo property of the current queue cannot be set to itself.";

        private const string MessagesPeekedFromTheQueue = "[{0}] messages peeked from the queue [{1}].";

        private const string MessagesPeekedFromTheDeadletterQueue =
            "[{0}] messages peeked from the deadletter queue of the queue [{1}].";

        private const string MessagesPeekedFromTheTransferDeadletterQueue =
            "[{0}] messages peeked from the transfer deadletter queue of the queue [{1}].";

        private const string MessagesReceivedFromTheQueue = "[{0}] messages received from the queue [{1}].";

        private const string MessagesReceivedFromTheDeadletterQueue =
            "[{0}] messages received from the deadletter queue of the queue [{1}].";

        private const string MessagesReceivedFromTheTransferDeadletterQueue =
            "[{0}] messages received from the transfer deadletter queue of the queue [{1}].";

        private const string SessionsGotFromTheQueue = "[{0}] sessions retrieved for the queue [{1}].";
        private const string RetrieveMessagesFromQueue = "Retrieve messages from queue";
        private const string RetrieveMessagesFromDeadletterQueue = "Retrieve messages from deadletter queue";

        private const string RetrieveMessagesFromTransferDeadletterQueue =
            "Retrieve messages from transfer deadletter queue";

        private const string AuthorizationRuleDeleteMessage = "The Authorization Rule will be permanently deleted";
        private const string SelectEntityDialogTitle = "Select a target Queue or Topic";
        private const string SelectEntityGrouperTitle = "Forward To";
        private const string SelectEntityLabelText = "Target Queue or Topic:";
        private const string KeyNameCannotBeNull = "Authorization Rule [{0}]: the KeyName cannot be null";
        private const string DoubleClickMessage = "Double-click a row to repair and resubmit the corresponding message.";
        private const string FilterExpressionTitle = "Define Filter Expression";
        private const string FilterExpressionLabel = "Filter Expression";
        private const string FilterExpressionNotValidMessage = "The filter expression [{0}] is not valid: {1}";

        private const string FilterExpressionAppliedMessage =
            "The filter expression [{0}] from {1} to {2} has been successfully applied. [{3}] messages retrieved.";

        private const string FilterExpressionRemovedMessage = "The filter expression has been removed.";

        private const string NoMessageReceivedFromTheQueue =
            "The timeout  of [{0}] seconds has expired and no message was retrieved from the queue [{1}].";

        private const string NoMessageReceivedFromTheDeadletterQueue =
            "The timeout  of [{0}] seconds has expired and no message was retrieved from the deadletter queue of the queue [{1}].";

        private const string NoMessageReceivedFromTheTransferDeadletterQueue =
            "The timeout  of [{0}] seconds has expired and no message was retrieved from the transfer deadletter queue of the queue [{1}].";

        //***************************
        // Tooltips
        //***************************
        private const string PathTooltip = "Gets or sets the queue path.";
        private const string UserMetadataTooltip = "Gets or sets the user metadata.";

        private const string ForwardToTooltip =
            "Gets or sets the path to the recipient to which the message is forwarded.";

        private const string ForwardDeadLetteredMessagesToTooltip =
            "Gets or sets the path to the recipient to which the dead lettered message is forwarded.";

        private const string MaxQueueSizeTooltip = "Gets or sets the maximum queue size in gigabytes.";

        private const string DefaultMessageTimeToLiveTooltip =
            "Gets or sets the default message time to live of a queue.";

        private const string DuplicateDetectionHistoryTimeWindowTooltip =
            "Gets or sets the duration of the time window for duplicate detection history.";

        private const string LockDurationTooltip = "Gets or sets the lock duration timespan associated with this queue.";

        private const string AutoDeleteOnIdleTooltip =
            "Gets or sets the maximum period of idleness after which the queue is auto deleted.";

        private const string MaxDeliveryCountTooltip =
            "Gets or sets the maximum delivery count. A message is automatically deadlettered after this number of deliveries.";

        private const string DeleteTooltip = "Delete the row.";

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
        private const string IsReadOnly = "Is ReadOnly";

        //***************************
        // Constants
        //***************************
        private const long SeviceBusForWindowsServerMaxQueueSize = 8796093022207;
        private const int GrouperMessagePropertiesWith = 312;
        private const string SaveAsTitle = "Save File As";
        private const string JsonExtension = "json";
        private const string TxtExtension = "txt";
        private const string JsonFilter = "JSON Files|*.json|Text Documents|*.txt";
        private const string AllFilesFilter = "Text Documents|*.txt|JSON Files|*.json|XML Files|*.xml|All Files (*.*)|*.*";
        private const string MessageFileFormat = "BrokeredMessage_{0}_{1}.json";
        private const string MessageFileFormatAutoRecognize = "BrokeredMessage_{0}_{1}.txt";

        //***************************
        // Pages
        //***************************
        private const string MessagesTabPage = "tabPageMessages";
        private const string SessionsTabPage = "tabPageSessions";
        private const string DeadletterTabPage = "tabPageDeadletter";
        private const string TransferDeadletterTabPage = "tabPageTransferDeadletter";
        private const string QueueEntity = "Queue";

        #endregion

        #region Private Fields

        private QueueDescription queueDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly string path;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private BrokeredMessage brokeredMessage;
        private BrokeredMessage deadletterMessage;
        private BrokeredMessage transferDeadletterMessage;
        private int currentMessageRowIndex;
        private int currentDeadletterMessageRowIndex;
        private int currentTransferDeadletterMessageRowIndex;
        private bool sorting;
        private string messagesFilterExpression;
        private string deadletterFilterExpression;
        private DateTime? messagesFilterFromDate;
        private DateTime? messagesFilterToDate;
        private DateTime? deadletterFilterFromDate;
        private DateTime? deadletterFilterToDate;
        private SortableBindingList<BrokeredMessage> messageBindingList;
        private SortableBindingList<BrokeredMessage> deadletterBindingList;
        private SortableBindingList<BrokeredMessage> transferDeadletterBindingList;
        private SortableBindingList<MessageSession> sessionBindingList;
        private bool buttonsMoved;

        #endregion

        #region Private Static Fields

        private static readonly List<string> ClaimTypes = new List<string>
        {
            "NameIdentifier",
            "Upn",
            "Role",
            "SharedAccessKey"
        };

        private static readonly List<string> Operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> TimeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };

        #endregion

        #region Public Constructors

        public HandleQueueControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper,
            QueueDescription queueDescription, string path)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.path = path;
            this.queueDescription = queueDescription;

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

        public void GetMessages()
        {
            using (
                var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromQueue, MainForm.SingletonMainForm.TopCount,
                    serviceBusHelper.BrokeredMessageInspectors.Keys))
            {
                if (receiveModeForm.ShowDialog() == DialogResult.OK)
                {
                    txtMessageText.Text = string.Empty;
                    messagePropertyListView.Items.Clear();
                    messagePropertyGrid.SelectedObject = null;
                    var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                           serviceBusHelper.BrokeredMessageInspectors.ContainsKey(
                                               receiveModeForm.Inspector)
                        ? Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector])
                            as IBrokeredMessageInspector
                        : null;
                    if (queueDescription.EnablePartitioning)
                    {
                        ReadMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count,
                            messageInspector, receiveModeForm.FromSequenceNumber);
                    }
                    else
                    {
                        GetMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                    }
                }
            }
        }

        public void PurgeMessages(int numberOfMessages)
        {
            if (queueDescription.EnablePartitioning)
            {
                ReadMessagesOneAtTheTime(false, true, numberOfMessages, null);
            }
            else
            {
                GetMessages(false, true, numberOfMessages, null);
            }
        }

        public async Task<long> PurgeMessagesAsync()
        {
            using (var deleteForm = new DeleteForm($"Would you like to purge the {queueDescription.Path} queue?"))
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
                var newSdkQueueDescription = await serviceBusHelper.GetNewSdkQueueDescription(queueDescription);
                var purger = new ServiceBusPurger(serviceBusHelper.GetServiceBusHelper2(), newSdkQueueDescription);
                var count = await purger.Purge();
                stopwatch.Stop();
                MainForm.SingletonMainForm.refreshEntity_Click(null, null);
                writeToLog($"[{count}] messages have been purged from the [{queueDescription.Path}] queue in [{stopwatch.ElapsedMilliseconds}] milliseconds.");
                return count;
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }


        public async Task<long> PurgeDeadletterQueueMessagesAsync()
        {
            using (var deleteForm = new DeleteForm($"Would you like to purge the deadletter queue of the {queueDescription.Path} queue?"))
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
                var newSdkQueueDescription = await serviceBusHelper.GetNewSdkQueueDescription(queueDescription);
                var purger = new ServiceBusPurger(serviceBusHelper.GetServiceBusHelper2(), newSdkQueueDescription);
                var count = await purger.Purge(purgeDeadLetterQueueInstead: true);
                stopwatch.Stop();
                MainForm.SingletonMainForm.refreshEntity_Click(null, null);
                writeToLog($"[{count}] messages have been purged from the deadletter queue of the [{queueDescription.Path}] queue in [{stopwatch.ElapsedMilliseconds}] milliseconds.");
                return count;
            }
            finally
            {
                Application.UseWaitCursor = false;
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
                var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) && serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector)
                    ? Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector
                    : null;
                if (queueDescription.EnablePartitioning)
                {
                    ReadDeadletterMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
                else
                {
                    GetDeadletterMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
            }
        }

        public void GetTransferDeadletterMessages()
        {
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromTransferDeadletterQueue, MainForm.SingletonMainForm.TopCount, serviceBusHelper.BrokeredMessageInspectors.Keys))
            {
                if (receiveModeForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtTransferDeadletterText.Text = string.Empty;
                transferDeadletterPropertyListView.Items.Clear();
                transferDeadletterPropertyGrid.SelectedObject = null;
                var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) && serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector)
                    ? Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector
                    : null;
                if (queueDescription.EnablePartitioning)
                {
                    ReadTransferDeadletterMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
                else
                {
                    GetTransferDeadletterMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
            }
        }

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

                sessionListStateSplitContainer.SplitterDistance = sessionListStateSplitContainer.Width -
                                                          GrouperMessagePropertiesWith -
                                                          sessionListStateSplitContainer.SplitterWidth;
                sessionMainSplitContainer.SplitterDistance =
                    sessionMainSplitContainer.Size.Height / 2 - 8;

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
            sessionListStateSplitContainer.SplitterWidth = 16;
            deadletterSplitContainer.SplitterWidth = 16;
            transferDeadletterSplitContainer.SplitterWidth = 16;


            messageMainSplitContainer.SplitterWidth = 8;
            deadletterMainSplitContainer.SplitterDistance = 8;
            transferMainSplitContainer.SplitterWidth = 8;
            sessionMainSplitContainer.SplitterWidth = 8;

            messagePropertiesSplitContainer.SplitterWidth = 8;
            deadletterPropertiesSplitContainer.SplitterWidth = 8;
            transferDeadletterSplitContainer.SplitterDistance = 8;

            // Tab pages
            DisablePage(MessagesTabPage);
            DisablePage(SessionsTabPage);
            DisablePage(DeadletterTabPage);
            DisablePage(TransferDeadletterTabPage);

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
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "IssuerName",
                    DataPropertyName = "IssuerName"
                });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewComboBoxColumn
                {
                    Name = "ClaimType",
                    DataPropertyName = "ClaimType",
                    DataSource = ClaimTypes,
                    FlatStyle = FlatStyle.Flat
                });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ClaimValue",
                    DataPropertyName = "ClaimValue"
                });
                if (serviceBusHelper.IsCloudNamespace)
                {
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "KeyName",
                        DataPropertyName = "KeyName"
                    });
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "PrimaryKey",
                        DataPropertyName = "PrimaryKey"
                    });
                    authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = "SecondaryKey",
                        DataPropertyName = "SecondaryKey"
                    });
                }
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "Manage",
                    DataPropertyName = "Manage",
                    Width = 50
                });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "Send",
                    DataPropertyName = "Send",
                    Width = 50
                });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "Listen",
                    DataPropertyName = "Listen",
                    Width = 50
                });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Revision",
                    DataPropertyName = "Revision",
                    Width = 50,
                    ReadOnly = true
                });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "CreatedTime",
                    DataPropertyName = "CreatedTime",
                    ReadOnly = true
                });
                authorizationRulesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ModifiedTime",
                    DataPropertyName = "ModifiedTime",
                    ReadOnly = true
                });
            }


            if (queueDescription != null)
            {
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

                // Set Grid style
                transferDeadletterDataGridView.EnableHeadersVisualStyles = false;
                transferDeadletterDataGridView.AutoGenerateColumns = false;
                transferDeadletterDataGridView.AutoSize = true;

                // Create the MessageId column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = MessageId,
                    Name = MessageId,
                    Width = 120
                };
                transferDeadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the SequenceNumber column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = SequenceNumberValue,
                    Name = SequenceNumberName,
                    Width = 52
                };
                transferDeadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the Size column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = MessageSize,
                    Name = MessageSize,
                    Width = 52
                };
                transferDeadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the Label column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = Label,
                    Name = Label,
                    Width = 120
                };
                transferDeadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the EnqueuedTimeUtc column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = EnqueuedTimeUtc,
                    Name = EnqueuedTimeUtc,
                    Width = 120
                };
                transferDeadletterDataGridView.Columns.Add(textBoxColumn);

                // Create the ExpiresAtUtc column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = ExpiresAtUtc,
                    Name = ExpiresAtUtc,
                    Width = 120
                };
                transferDeadletterDataGridView.Columns.Add(textBoxColumn);

                // Set the selection background color for all the cells.
                transferDeadletterDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                transferDeadletterDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                transferDeadletterDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                transferDeadletterDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                transferDeadletterDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                transferDeadletterDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                transferDeadletterDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                transferDeadletterDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                transferDeadletterDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                checkedListBox.ItemCheck += checkedListBox_ItemCheck;

                toolTip.SetToolTip(txtPath, PathTooltip);
                toolTip.SetToolTip(txtUserMetadata, UserMetadataTooltip);
                toolTip.SetToolTip(txtForwardTo, ForwardToTooltip);
                toolTip.SetToolTip(txtForwardDeadLetteredMessagesTo, ForwardDeadLetteredMessagesToTooltip);
                toolTip.SetToolTip(trackBarMaxQueueSize, MaxQueueSizeTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveDays, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveHours, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveMinutes, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveSeconds, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDefaultMessageTimeToLiveMilliseconds, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowDays,
                    DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowHours,
                    DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowMinutes,
                    DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowSeconds,
                    DuplicateDetectionHistoryTimeWindowTooltip);
                toolTip.SetToolTip(txtDuplicateDetectionHistoryTimeWindowMilliseconds,
                    DuplicateDetectionHistoryTimeWindowTooltip);
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
                // Initialize buttons
                btnCreateDelete.Text = CreateText;
                btnCancelUpdate.Text = CancelText;
                btnRefresh.Visible = false;
                btnChangeStatus.Visible = false;
                btnMessages.Visible = false;
                btnSessions.Visible = false;
                btnDeadletter.Visible = false;
                btnPurgeMessages.Visible = false;
                btnPurgeDeadletterQueueMessages.Visible = false;
                btnTransferDeadletterQueue.Visible = false;

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
                if (queueDescription != null &&
                    queueDescription.Authorization.Count > 0 &&
                    queueDescription.Authorization.Count > e.NewIndex)
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
            btnChangeStatus.Text = StatusText;
            btnRefresh.Visible = true;
            btnChangeStatus.Visible = true;
            btnMessages.Visible = true;
            btnSessions.Visible = queueDescription.RequiresSession;

            if (btnMessages.Visible && !btnSessions.Visible && !buttonsMoved)
            {
                btnPurgeMessages.Location = btnPurgeDeadletterQueueMessages.Location;
                btnPurgeDeadletterQueueMessages.Location = btnSessions.Location;
                buttonsMoved = true;
            }
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
                new[]
                {
                    ActiveMessageCount,
                    queueDescription.MessageCountDetails.ActiveMessageCount.ToString("N0", CultureInfo.CurrentCulture)
                },
                new[]
                {
                    DeadletterCount,
                    queueDescription.MessageCountDetails.DeadLetterMessageCount.ToString("N0",
                        CultureInfo.CurrentCulture)
                },
                new[]
                {
                    ScheduledMessageCount,
                    queueDescription.MessageCountDetails.ScheduledMessageCount.ToString("N0", CultureInfo.CurrentCulture)
                },
                new[]
                {
                    TransferMessageCount,
                    queueDescription.MessageCountDetails.TransferMessageCount.ToString("N0", CultureInfo.CurrentCulture)
                },
                new[]
                {
                    TransferDeadLetterMessageCount,
                    queueDescription.MessageCountDetails.TransferDeadLetterMessageCount.ToString("N0",
                        CultureInfo.CurrentCulture)
                },
                new[] {MessageCount, queueDescription.MessageCount.ToString("N0", CultureInfo.CurrentCulture)}
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
                txtForwardTo.Text = serviceBusHelper.GetAddressRelativeToNamespace(queueDescription.ForwardTo);
            }

            // ForwardDeadLetteredMessagesTo
            if (!string.IsNullOrWhiteSpace(queueDescription.ForwardDeadLetteredMessagesTo))
            {
                txtForwardDeadLetteredMessagesTo.Text = serviceBusHelper.GetAddressRelativeToNamespace(queueDescription.ForwardDeadLetteredMessagesTo);
            }

            // MaxQueueSizeInBytes
            trackBarMaxQueueSize.Value = serviceBusHelper.IsCloudNamespace
                ? queueDescription.MaxSizeInGigabytes()
                : queueDescription.MaxSizeInMegabytes == SeviceBusForWindowsServerMaxQueueSize
                ? 11 : queueDescription.MaxSizeInGigabytes();

            // Update maximum and value if Maximum size is more than 5 Gigs (either premium or partitioned)
            if (queueDescription.MaxSizeInGigabytes() > 5)
            {
                trackBarMaxQueueSize.Maximum = queueDescription.MaxSizeInGigabytes();
                trackBarMaxQueueSize.Value = queueDescription.MaxSizeInGigabytes();
            }

            // MaxDeliveryCount
            txtMaxDeliveryCount.Text = queueDescription.MaxDeliveryCount.ToString(CultureInfo.InvariantCulture);

            // DefaultMessageTimeToLive
            txtDefaultMessageTimeToLiveDays.Text =
                queueDescription.DefaultMessageTimeToLive.Days.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveHours.Text =
                queueDescription.DefaultMessageTimeToLive.Hours.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveMinutes.Text =
                queueDescription.DefaultMessageTimeToLive.Minutes.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveSeconds.Text =
                queueDescription.DefaultMessageTimeToLive.Seconds.ToString(CultureInfo.InvariantCulture);
            txtDefaultMessageTimeToLiveMilliseconds.Text =
                queueDescription.DefaultMessageTimeToLive.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // DuplicateDetectionHistoryTimeWindow
            txtDuplicateDetectionHistoryTimeWindowDays.Text =
                queueDescription.DuplicateDetectionHistoryTimeWindow.Days.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowHours.Text =
                queueDescription.DuplicateDetectionHistoryTimeWindow.Hours.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowMinutes.Text =
                queueDescription.DuplicateDetectionHistoryTimeWindow.Minutes.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowSeconds.Text =
                queueDescription.DuplicateDetectionHistoryTimeWindow.Seconds.ToString(CultureInfo.InvariantCulture);
            txtDuplicateDetectionHistoryTimeWindowMilliseconds.Text =
                queueDescription.DuplicateDetectionHistoryTimeWindow.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // LockDuration
            txtLockDurationDays.Text = queueDescription.LockDuration.Days.ToString(CultureInfo.InvariantCulture);
            txtLockDurationHours.Text = queueDescription.LockDuration.Hours.ToString(CultureInfo.InvariantCulture);
            txtLockDurationMinutes.Text = queueDescription.LockDuration.Minutes.ToString(CultureInfo.InvariantCulture);
            txtLockDurationSeconds.Text = queueDescription.LockDuration.Seconds.ToString(CultureInfo.InvariantCulture);
            txtLockDurationMilliseconds.Text =
                queueDescription.LockDuration.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // AutoDeleteOnIdle
            txtAutoDeleteOnIdleDays.Text = queueDescription.AutoDeleteOnIdle.Days.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleHours.Text =
                queueDescription.AutoDeleteOnIdle.Hours.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleMinutes.Text =
                queueDescription.AutoDeleteOnIdle.Minutes.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleSeconds.Text =
                queueDescription.AutoDeleteOnIdle.Seconds.ToString(CultureInfo.InvariantCulture);
            txtAutoDeleteOnIdleMilliseconds.Text =
                queueDescription.AutoDeleteOnIdle.Milliseconds.ToString(CultureInfo.InvariantCulture);

            // EnableBatchedOperations
            checkedListBox.SetItemChecked(EnableBatchedOperationsIndex,
                queueDescription.EnableBatchedOperations);

            // EnableDeadLetteringOnMessageExpiration
            checkedListBox.SetItemChecked(EnableDeadLetteringOnMessageExpirationIndex,
                queueDescription.EnableDeadLetteringOnMessageExpiration);


            if (serviceBusHelper.IsCloudNamespace)
            {
                // EnablePartitioning
                checkedListBox.SetItemChecked(EnablePartitioningIndex, queueDescription.EnablePartitioning);

                // EnableExpress
                checkedListBox.SetItemChecked(EnableExpressIndex, queueDescription.EnableExpress);
            }

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

        private void GetMessages(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber = null)
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
                    var totalRetrieved = 0;
                    while (totalRetrieved < count)
                    {
                        IEnumerable<BrokeredMessage> messageEnumerable;

                        if (totalRetrieved == 0 && fromSequenceNumber.HasValue)
                        {
                            messageEnumerable = queueClient.PeekBatch(fromSequenceNumber.Value, count);
                        }
                        else
                        {
                            messageEnumerable = queueClient.PeekBatch(count);
                        }

                        if (messageEnumerable == null)
                        {
                            break;
                        }
                        var messageArray = messageEnumerable as BrokeredMessage[] ?? messageEnumerable.ToArray();
                        var partialList = messageInspector != null
                            ? messageArray.Select(b => messageInspector.AfterReceiveMessage(b)).ToList()
                            : new List<BrokeredMessage>(messageArray);
                        brokeredMessages.AddRange(partialList);
                        totalRetrieved += partialList.Count;
                        if (partialList.Count == 0)
                        {
                            break;
                        }
                    }
                    writeToLog(string.Format(MessagesPeekedFromTheQueue, brokeredMessages.Count, queueDescription.Path));
                }
                else
                {
                    MessageReceiver messageReceiver;
                    if (queueDescription.RequiresSession)
                    {
                        var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queueDescription.Path,
                            ReceiveMode.ReceiveAndDelete);
                        messageReceiver =
                            queueClient.AcceptMessageSession(
                                TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
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
                        var messages = messageReceiver.ReceiveBatch(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved,
                            TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Length;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null
                            ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b))
                            : enumerable);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
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

                messagesSplitContainer.SplitterDistance = messagesSplitContainer.Width -
                                                          GrouperMessagePropertiesWith -
                                                          messagesSplitContainer.SplitterWidth;
                messageMainSplitContainer.SplitterDistance = messageMainSplitContainer.Size.Height / 2 - 8;

                messagePropertiesSplitContainer.SplitterDistance = messageMainSplitContainer.SplitterDistance;

                if (!peek)
                {
                    OnRefresh?.Invoke();
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
                writeToLog(string.Format(NoMessageReceivedFromTheQueue,
                    MainForm.SingletonMainForm.ReceiveTimeout,
                    queueDescription.Path));
            }
            catch (NotSupportedException)
            {
                ReadMessagesOneAtTheTime(peek, all, count, messageInspector, fromSequenceNumber);
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

        private void ReadMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber = null)
        {
            try
            {
                var brokeredMessages = new List<BrokeredMessage>();
                if (peek)
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queueDescription.Path,
                        ReceiveMode.PeekLock);
                    for (var i = 0; i < count; i++)
                    {
                        BrokeredMessage message;

                        if (i == 0 && fromSequenceNumber.HasValue)
                        {
                            message = queueClient.Peek(fromSequenceNumber.Value);
                        }
                        else
                        {
                            message = queueClient.Peek();
                        }

                        if (message != null)
                        {
                            if (messageInspector != null)
                            {
                                message = messageInspector.AfterReceiveMessage(message);
                            }
                            brokeredMessages.Add(message);
                        }
                    }
                    writeToLog(string.Format(MessagesPeekedFromTheQueue, brokeredMessages.Count, queueDescription.Path));
                }
                else
                {
                    MessageReceiver messageReceiver;
                    if (queueDescription.RequiresSession)
                    {
                        var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queueDescription.Path,
                            ReceiveMode.ReceiveAndDelete);
                        messageReceiver =
                            queueClient.AcceptMessageSession(
                                TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
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
                        var message =
                            messageReceiver.Receive(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        retrieved = message != null ? 1 : 0;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.Add(messageInspector != null
                            ? messageInspector.AfterReceiveMessage(message)
                            : message);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
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
                messagesSplitContainer.SplitterDistance = messagesSplitContainer.Width -
                                                          GrouperMessagePropertiesWith -
                                                          messagesSplitContainer.SplitterWidth;
                messageMainSplitContainer.SplitterDistance = messageMainSplitContainer.Size.Height / 2 - 8;
                messagePropertiesSplitContainer.SplitterDistance = messageMainSplitContainer.SplitterDistance;

                if (!peek)
                {
                    OnRefresh?.Invoke();
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
                writeToLog(string.Format(NoMessageReceivedFromTheQueue,
                    MainForm.SingletonMainForm.ReceiveTimeout,
                    queueDescription.Path));
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void GetDeadletterMessages(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber)
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageDeadletter.SuspendDrawing();
                tabPageDeadletter.SuspendLayout();

                Cursor.Current = Cursors.WaitCursor;
                var brokeredMessages = new List<BrokeredMessage>();

                var queuePath = QueueClient.FormatDeadLetterPath(queueDescription.Path);
                if (peek)
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.PeekLock);
                    var totalRetrieved = 0;
                    var retrieved = 0;
                    do
                    {
                        IEnumerable<BrokeredMessage> messages;

                        if (retrieved == 0 && fromSequenceNumber.HasValue)
                        {
                            messages = queueClient.PeekBatch(fromSequenceNumber.Value, all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        }
                        else
                        {
                            messages = queueClient.PeekBatch(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        }

                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Length;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null
                            ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b))
                            : enumerable);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }
                else
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = queueClient.ReceiveBatch(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved,
                            TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Length;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null
                            ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b))
                            : enumerable);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    //if (!queueDescription.EnablePartitioning)
                    //{
                    //    queueClient.CompleteBatch(brokeredMessages.Select(bm => bm.LockToken));
                    //}
                    //else
                    //{
                    //    foreach (var partitionKey in brokeredMessages.Select(bm => bm.PartitionKey).Distinct())
                    //    {
                    //        var key = partitionKey;
                    //        queueClient.CompleteBatch(
                    //            brokeredMessages.Where(bm => bm.PartitionKey == key).Select(bm => bm.LockToken));
                    //    }
                    //}
                    writeToLog(string.Format(MessagesReceivedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }

                deadletterBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = true
                };

                deadletterBindingSource.DataSource = deadletterBindingList;
                deadletterDataGridView.DataSource = deadletterBindingSource;

                deadletterSplitContainer.SplitterDistance = deadletterSplitContainer.Width -
                                                            GrouperMessagePropertiesWith -
                                                            deadletterSplitContainer.SplitterWidth;
                deadletterMainSplitContainer.SplitterDistance =
                    deadletterMainSplitContainer.Size.Height / 2 - 8;

                if (!peek)
                {
                    OnRefresh?.Invoke();
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
                writeToLog(string.Format(NoMessageReceivedFromTheDeadletterQueue, MainForm.SingletonMainForm.ReceiveTimeout, queueDescription.Path));
            }
            catch (NotSupportedException)
            {
                ReadDeadletterMessagesOneAtTheTime(peek, all, count, messageInspector, fromSequenceNumber);
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

        private void GetTransferDeadletterMessages(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber)
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageTransferDeadletter.SuspendDrawing();
                tabPageTransferDeadletter.SuspendLayout();

                Cursor.Current = Cursors.WaitCursor;
                var brokeredMessages = new List<BrokeredMessage>();

                var queuePath = QueueClient.FormatTransferDeadLetterPath(queueDescription.Path);
                if (peek)
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.PeekLock);
                    var totalRetrieved = 0;
                    var retrieved = 0;
                    do
                    {
                        IEnumerable<BrokeredMessage> messages;

                        if (retrieved == 0 && fromSequenceNumber.HasValue)
                        {
                            messages = queueClient.PeekBatch(fromSequenceNumber.Value, all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        }
                        else
                        {
                            messages = queueClient.PeekBatch(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        }

                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Length;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null
                            ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b))
                            : enumerable);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesPeekedFromTheTransferDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }
                else
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = queueClient.ReceiveBatch(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved,
                            TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Length;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null
                            ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b))
                            : enumerable);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    //if (!queueDescription.EnablePartitioning)
                    //{
                    //    queueClient.CompleteBatch(brokeredMessages.Select(bm => bm.LockToken));
                    //}
                    //else
                    //{
                    //    foreach (var partitionKey in brokeredMessages.Select(bm => bm.PartitionKey).Distinct())
                    //    {
                    //        var key = partitionKey;
                    //        queueClient.CompleteBatch(
                    //            brokeredMessages.Where(bm => bm.PartitionKey == key).Select(bm => bm.LockToken));
                    //    }
                    //}
                    writeToLog(string.Format(MessagesReceivedFromTheTransferDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }

                transferDeadletterBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };

                transferDeadletterBindingSource.DataSource = transferDeadletterBindingList;
                transferDeadletterDataGridView.DataSource = transferDeadletterBindingSource;

                transferDeadletterSplitContainer.SplitterDistance = transferDeadletterSplitContainer.Width -
                                                            GrouperMessagePropertiesWith -
                                                            transferDeadletterSplitContainer.SplitterWidth;
                transferMainSplitContainer.SplitterDistance = transferMainSplitContainer.Size.Height / 2 - 8;


                if (!peek)
                {
                    OnRefresh?.Invoke();
                }
                if (mainTabControl.TabPages[TransferDeadletterTabPage] == null)
                {
                    EnablePage(TransferDeadletterTabPage);
                }
                if (mainTabControl.TabPages[TransferDeadletterTabPage] != null)
                {
                    mainTabControl.SelectTab(TransferDeadletterTabPage);
                }
            }
            catch (TimeoutException)
            {
                writeToLog(string.Format(NoMessageReceivedFromTheTransferDeadletterQueue, MainForm.SingletonMainForm.ReceiveTimeout, queueDescription.Path));
            }
            catch (NotSupportedException)
            {
                ReadTransferDeadletterMessagesOneAtTheTime(peek, all, count, messageInspector, fromSequenceNumber);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                mainTabControl.ResumeLayout();
                mainTabControl.ResumeDrawing();
                tabPageTransferDeadletter.ResumeLayout();
                tabPageTransferDeadletter.ResumeDrawing();
                Cursor.Current = Cursors.Default;
            }
        }

        private void ReadDeadletterMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber)
        {
            try
            {
                var brokeredMessages = new List<BrokeredMessage>();
                var queuePath = QueueClient.FormatDeadLetterPath(queueDescription.Path);

                if (peek)
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.PeekLock);
                    for (var i = 0; i < count; i++)
                    {
                        BrokeredMessage message;

                        if (i == 0 && fromSequenceNumber.HasValue)
                        {
                            message = queueClient.Peek(fromSequenceNumber.Value);
                        }
                        else
                        {
                            message = queueClient.Peek();
                        }

                        if (message == null)
                        {
                            continue;
                        }
                        if (messageInspector != null)
                        {
                            message = messageInspector.AfterReceiveMessage(message);
                        }
                        brokeredMessages.Add(message);
                    }
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }
                else
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var message = queueClient.Receive(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        retrieved = message != null ? 1 : 0;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.Add(messageInspector != null
                            ? messageInspector.AfterReceiveMessage(message)
                            : message);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }
                deadletterBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = true
                };
                deadletterBindingSource.DataSource = deadletterBindingList;
                deadletterDataGridView.DataSource = deadletterBindingSource;

                deadletterSplitContainer.SplitterDistance = deadletterSplitContainer.Width -
                                                            GrouperMessagePropertiesWith -
                                                            deadletterSplitContainer.SplitterWidth;
                deadletterMainSplitContainer.SplitterDistance = deadletterMainSplitContainer.Size.Height / 2 - 8;


                if (!peek)
                {
                    OnRefresh?.Invoke();
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
                writeToLog(string.Format(NoMessageReceivedFromTheDeadletterQueue, MainForm.SingletonMainForm.ReceiveTimeout, queueDescription.Path));
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void ReadTransferDeadletterMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber)
        {
            try
            {
                var brokeredMessages = new List<BrokeredMessage>();
                var queuePath = QueueClient.FormatTransferDeadLetterPath(queueDescription.Path);

                if (peek)
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.PeekLock);
                    for (var i = 0; i < count; i++)
                    {
                        BrokeredMessage message;

                        if (i == 0 && fromSequenceNumber.HasValue)
                        {
                            message = queueClient.Peek(fromSequenceNumber.Value);
                        }
                        else
                        {
                            message = queueClient.Peek();
                        }

                        if (message == null)
                        {
                            continue;
                        }
                        if (messageInspector != null)
                        {
                            message = messageInspector.AfterReceiveMessage(message);
                        }
                        brokeredMessages.Add(message);
                    }
                    writeToLog(string.Format(MessagesPeekedFromTheTransferDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }
                else
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var message = queueClient.Receive(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        retrieved = message != null ? 1 : 0;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.Add(messageInspector != null
                            ? messageInspector.AfterReceiveMessage(message)
                            : message);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesPeekedFromTheTransferDeadletterQueue, brokeredMessages.Count, queueDescription.Path));
                }
                transferDeadletterBindingList = new SortableBindingList<BrokeredMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };
                transferDeadletterBindingSource.DataSource = transferDeadletterBindingList;
                transferDeadletterDataGridView.DataSource = transferDeadletterBindingSource;

                transferDeadletterSplitContainer.SplitterDistance = transferDeadletterSplitContainer.Width -
                                                            GrouperMessagePropertiesWith -
                                                            transferDeadletterSplitContainer.SplitterWidth;

                if (!peek)
                {
                    OnRefresh?.Invoke();
                }
                if (mainTabControl.TabPages[TransferDeadletterTabPage] == null)
                {
                    EnablePage(TransferDeadletterTabPage);
                }
                if (mainTabControl.TabPages[TransferDeadletterTabPage] != null)
                {
                    mainTabControl.SelectTab(TransferDeadletterTabPage);
                }
            }
            catch (TimeoutException)
            {
                writeToLog(string.Format(NoMessageReceivedFromTheTransferDeadletterQueue, MainForm.SingletonMainForm.ReceiveTimeout, queueDescription.Path));
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
                            await serviceBusHelper.DeleteQueue(queueDescription);
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

                    if (string.Equals(txtPath.Text.Trim(), txtForwardTo.Text.Trim(),
                        StringComparison.CurrentCultureIgnoreCase))
                    {
                        writeToLog(CannotForwardToItself);
                        txtForwardTo.Focus();
                        return;
                    }

                    if (string.Equals(txtPath.Text.Trim(), txtForwardDeadLetteredMessagesTo.Text.Trim(),
                        StringComparison.CurrentCultureIgnoreCase))
                    {
                        writeToLog(CannotForwardDeadLetteredMessagesToItself);
                        txtForwardTo.Focus();
                        return;
                    }

                    var description = new QueueDescription(txtPath.Text)
                    {
                        UserMetadata = txtUserMetadata.Text,
                        ForwardTo = txtForwardTo.Text,
                        ForwardDeadLetteredMessagesTo = txtForwardDeadLetteredMessagesTo.Text,
                        MaxSizeInMegabytes = serviceBusHelper.IsCloudNamespace
                            ? trackBarMaxQueueSize.Value * 1024
                            : trackBarMaxQueueSize.Value == trackBarMaxQueueSize.Maximum
                                ? SeviceBusForWindowsServerMaxQueueSize
                                : trackBarMaxQueueSize.Value * 1024
                    };

                    if (!string.IsNullOrWhiteSpace(txtMaxDeliveryCount.Text))
                    {
                        if (int.TryParse(txtMaxDeliveryCount.Text, out var value))
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
                        description.DuplicateDetectionHistoryTimeWindow = new TimeSpan(days, hours, minutes, seconds,
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
                    description.EnableDeadLetteringOnMessageExpiration =
                        checkedListBox.GetItemChecked(EnableDeadLetteringOnMessageExpirationIndex);
                    if (serviceBusHelper.IsCloudNamespace)
                    {
                        description.EnablePartitioning = checkedListBox.GetItemChecked(EnablePartitioningIndex);
                        description.EnableExpress = checkedListBox.GetItemChecked(EnableExpressIndex);
                    }
                    description.RequiresDuplicateDetection =
                        checkedListBox.GetItemChecked(RequiresDuplicateDetectionIndex);
                    description.RequiresSession = checkedListBox.GetItemChecked(RequiresSessionIndex);
                    description.SupportOrdering = checkedListBox.GetItemChecked(SupportOrderingIndex);
                    if (!serviceBusHelper.IsCloudNamespace)
                    {
                        description.IsAnonymousAccessible = checkedListBox.GetItemChecked(IsAnonymousAccessibleIndex);
                    }

                    var bindingList =
                        authorizationRulesBindingSource.DataSource as BindingList<AuthorizationRuleWrapper>;
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
                                if (string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    description.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                        rightList));
                                }
                                else
                                {
                                    description.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                        rule.SecondaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
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

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (queueDescription == null)
            {
                return;
            }
            if (e.Index == EnablePartitioningIndex)
            {
                e.NewValue = queueDescription.EnablePartitioning ? CheckState.Checked : CheckState.Unchecked;
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
                OnCancel?.Invoke();
            }
            else
            {
                try
                {
                    if (string.Equals(txtPath.Text.Trim(), txtForwardTo.Text.Trim(),
                        StringComparison.CurrentCultureIgnoreCase))
                    {
                        writeToLog(CannotForwardToItself);
                        txtForwardTo.Focus();
                        return;
                    }

                    if (String.Equals(txtPath.Text.Trim(), txtForwardDeadLetteredMessagesTo.Text.Trim(),
                        StringComparison.CurrentCultureIgnoreCase))
                    {
                        writeToLog(CannotForwardDeadLetteredMessagesToItself);
                        txtForwardTo.Focus();
                        return;
                    }

                    queueDescription.UserMetadata = txtUserMetadata.Text;
                    queueDescription.ForwardTo = string.IsNullOrWhiteSpace(txtForwardTo.Text) ? null : txtForwardTo.Text;
                    queueDescription.ForwardDeadLetteredMessagesTo =
                        string.IsNullOrWhiteSpace(txtForwardDeadLetteredMessagesTo.Text)
                            ? null
                            : txtForwardDeadLetteredMessagesTo.Text;

                    if (!string.IsNullOrWhiteSpace(txtMaxDeliveryCount.Text))
                    {
                        if (int.TryParse(txtMaxDeliveryCount.Text, out var value))
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

                    queueDescription.EnableBatchedOperations =
                        checkedListBox.GetItemChecked(EnableBatchedOperationsIndex);
                    queueDescription.EnableExpress = checkedListBox.GetItemChecked(EnableExpressIndex);
                    queueDescription.EnableDeadLetteringOnMessageExpiration =
                        checkedListBox.GetItemChecked(EnableDeadLetteringOnMessageExpirationIndex);
                    queueDescription.SupportOrdering = checkedListBox.GetItemChecked(SupportOrderingIndex);

                    if (!serviceBusHelper.IsCloudNamespace)
                    {
                        queueDescription.IsAnonymousAccessible =
                            checkedListBox.GetItemChecked(IsAnonymousAccessibleIndex);
                    }

                    var bindingList =
                        authorizationRulesBindingSource.DataSource as BindingList<AuthorizationRuleWrapper>;
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
                                rightList.AddRange(new[]
                                    {AccessRights.Manage, AccessRights.Send, AccessRights.Listen});
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
                                if (string.IsNullOrWhiteSpace(rule.PrimaryKey) &&
                                    string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    queueDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rightList));
                                }
                                else if (string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    queueDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                        rightList));
                                }
                                else
                                {
                                    queueDescription.Authorization.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rule.PrimaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
                                        rule.SecondaryKey ?? SharedAccessAuthorizationRule.GenerateRandomKey(),
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
                NativeMethods.HideCaret(textBox.Handle);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            OnRefresh?.Invoke();
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            OnChangeStatus?.Invoke();
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
            using (var form = creteSelectEntityFormForPath(txtForwardTo.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtForwardTo.Text = form.Path;
                }
            }
        }

        private void btnOpenForwardDeadLetteredMessagesToForm_Click(object sender, EventArgs e)
        {
            using (var form = creteSelectEntityFormForPath(txtForwardDeadLetteredMessagesTo.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtForwardDeadLetteredMessagesTo.Text = form.Path;
                }
            }
        }

        private SelectEntityForm creteSelectEntityFormForPath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                QueueDescription queueDescriptionSource = null;
                try
                {
                    queueDescriptionSource = serviceBusHelper.GetQueue(path);
                }
                catch (Exception)
                {
                    // we might have found a topic, and the sdk will throw with an indistinguishable MessagingException error.
                }
                if (queueDescriptionSource != null)
                {
                    return new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText, queueDescriptionSource);
                }
                TopicDescription topicDescriptionSource = null;
                try
                {
                    topicDescriptionSource = serviceBusHelper.GetTopic(path);
                }
                catch (Exception)
                {
                    // we might have found a queue, and the sdk will throw with an indistinguishable MessagingException error.
                }
                if (topicDescriptionSource != null)
                {
                    return new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText, topicDescriptionSource);
                }
            }
            return new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText);
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            var startX = e.ColumnIndex == 0 ? -1 : e.Bounds.X;
            var endX = e.Bounds.X + e.Bounds.Width - 1;
            // Background
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(215, 228, 242)), startX, -1, e.Bounds.Width + 1,
                e.Bounds.Height + 1);
            // Left vertical line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, startX,
                e.Bounds.Y + e.Bounds.Height + 1);
            // TopCount horizontal line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, endX, -1);
            // Bottom horizontal line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), startX, e.Bounds.Height - 1, endX,
                e.Bounds.Height - 1);
            // Right vertical line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), endX, -1, endX, e.Bounds.Height + 1);
            var roundedFontSize = (float)Math.Round(e.Font.SizeInPoints);
            var bounds = new RectangleF(e.Bounds.X + 4, (e.Bounds.Height - 8 - roundedFontSize) / 2, e.Bounds.Width,
                roundedFontSize + 6);
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
            var leftVerticalLineRect = new RectangleF(2, tabstripEndRect.Y + tabstripEndRect.Height + 2, 2,
                tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var rightVerticalLineRect = new RectangleF(tabControl.TabPages[tabControl.SelectedIndex].Width + 4,
                tabstripEndRect.Y + tabstripEndRect.Height + 2, 2,
                tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var bottomHorizontalLineRect = new RectangleF(2,
                tabstripEndRect.Y + tabstripEndRect.Height + tabControl.TabPages[tabControl.SelectedIndex].Height + 2,
                tabControl.TabPages[tabControl.SelectedIndex].Width + 4, 2);
            RectangleF leftVerticalBarNearFirstTab = new Rectangle(0, 0, 2, tabstripEndRect.Height + 2);

            // First, do the end of the tab strip.
            // If we have an image use it.
            if (tabControl.Parent.BackgroundImage != null)
            {
                var src = new RectangleF(tabstripEndRectF.X + tabControl.Left, tabstripEndRectF.Y + tabControl.Top,
                    tabstripEndRectF.Width, tabstripEndRectF.Height);
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
                        e.Graphics.DrawString(tabName, new Font(e.Font.FontFamily, 8.25F, e.Font.Style), foreBrush,
                            labelRect, sf);
                    }
                }
            }
        }

        private void EnablePage(string pageName)
        {
            var page =
                hiddenPages.FirstOrDefault(
                    p => string.Compare(p.Name, pageName, StringComparison.InvariantCultureIgnoreCase) == 0);
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
            if (dataGridView == null || dataGridView.ColumnCount == 0)
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
                    dataGridView == deadletterDataGridView ||
                    dataGridView == transferDeadletterDataGridView) &&
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
                    dataGridView.Columns[4].Width = columnWidth + (width - (columnWidth * 4)) + 10;
                    dataGridView.Columns[5].Width = columnWidth + 10;
                }
                if (dataGridView != sessionsDataGridView ||
                    dataGridView.ColumnCount != 4)
                {
                    return;
                }
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
            try
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

                LanguageDetector.SetFormattedMessage(serviceBusHelper, brokeredMessage, txtMessageText);

                var listViewItems = brokeredMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, Convert.ToString(p.Value) })).ToArray();
                messagePropertyListView.Items.Clear();
                messagePropertyListView.Items.AddRange(listViewItems);
                messagePropertyGrid.SelectedObject = brokeredMessage;
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
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
                txtSessionState.Text = string.Empty;
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
            messagesDataGridView.Size = new Size(
                grouperMessageList.Size.Width - (messagesDataGridView.Location.X * 2 + 2),
                grouperMessageList.Size.Height - messagesDataGridView.Location.Y - messagesDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                messagesDataGridView.Location.X - 1,
                messagesDataGridView.Location.Y - 1,
                messagesDataGridView.Size.Width + 1,
                messagesDataGridView.Size.Height + 1);
        }

        private void grouperSessionList_CustomPaint(PaintEventArgs e)
        {
            sessionsDataGridView.Size = new Size(
                grouperSessionList.Size.Width - (sessionsDataGridView.Location.X * 2 + 2),
                grouperSessionList.Size.Height - sessionsDataGridView.Location.Y - sessionsDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                sessionsDataGridView.Location.X - 1,
                sessionsDataGridView.Location.Y - 1,
                sessionsDataGridView.Size.Width + 1,
                sessionsDataGridView.Size.Height + 1);
        }

        private void grouperDeadletterList_CustomPaint(PaintEventArgs e)
        {
            deadletterDataGridView.Size =
                new Size(grouperDeadletterList.Size.Width - (deadletterDataGridView.Location.X * 2 + 2),
                    grouperDeadletterList.Size.Height - deadletterDataGridView.Location.Y -
                    deadletterDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                deadletterDataGridView.Location.X - 1,
                deadletterDataGridView.Location.Y - 1,
                deadletterDataGridView.Size.Width + 1,
                deadletterDataGridView.Size.Height + 1);
        }

        private void grouperTransferDeadletterList_CustomPaint(PaintEventArgs e)
        {
            transferDeadletterDataGridView.Size =
                new Size(grouperTransferDeadletterList.Size.Width - (transferDeadletterDataGridView.Location.X * 2 + 2),
                    grouperTransferDeadletterList.Size.Height - transferDeadletterDataGridView.Location.Y -
                    transferDeadletterDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                transferDeadletterDataGridView.Location.X - 1,
                transferDeadletterDataGridView.Location.Y - 1,
                transferDeadletterDataGridView.Size.Width + 1,
                transferDeadletterDataGridView.Size.Height + 1);
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
            sessionPropertyGrid.Size = new Size(grouperSessionProperties.Size.Width - 32,
                sessionPropertyGrid.Size.Height);
        }

        private void deadletterTabPage_Resize(object sender, EventArgs e)
        {

        }

        private void btnDeadletter_Click(object sender, EventArgs e)
        {
            GetDeadletterMessages();
        }

        private void btnTransferDlq_Click(object sender, EventArgs e)
        {
            GetTransferDeadletterMessages();
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

            LanguageDetector.SetFormattedMessage(serviceBusHelper, deadletterMessage, txtDeadletterText);

            var listViewItems = deadletterMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, Convert.ToString(p.Value) })).ToArray();
            deadletterPropertyListView.Items.Clear();
            deadletterPropertyListView.Items.AddRange(listViewItems);
        }

        private void transferDeadletterDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = transferDeadletterBindingSource.DataSource as BindingList<BrokeredMessage>;
            currentTransferDeadletterMessageRowIndex = e.RowIndex;
            if (bindingList == null)
            {
                return;
            }
            if (transferDeadletterMessage == bindingList[e.RowIndex])
            {
                return;
            }
            transferDeadletterMessage = bindingList[e.RowIndex];
            transferDeadletterPropertyGrid.SelectedObject = transferDeadletterMessage;

            LanguageDetector.SetFormattedMessage(serviceBusHelper, transferDeadletterMessage, txtTransferDeadletterText);

            var listViewItems = transferDeadletterMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, Convert.ToString(p.Value) })).ToArray();
            transferDeadletterPropertyListView.Items.Clear();
            transferDeadletterPropertyListView.Items.AddRange(listViewItems);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);

            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            var groupSeparator = numberFormatInfo.NumberGroupSeparator;
            var negativeSign = numberFormatInfo.NegativeSign;

            var keyInput = e.KeyChar.ToString(CultureInfo.InvariantCulture);

            if (char.IsDigit(e.KeyChar))
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
                string.IsNullOrWhiteSpace(
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["IssuerName"].Value as string))
            {
                authorizationRulesDataGridView.Rows[e.RowIndex].Cells["IssuerName"].Value = serviceBusHelper.Namespace;
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
            using (var messageForm = new MessageForm(queueDescription, bindingList[e.RowIndex], serviceBusHelper, writeToLog))
            {
                messageForm.ShowDialog();

                Application.UseWaitCursor = true;
                try
                {
                    if (messageForm.RemovedSequenceNumbers != null && messageForm.RemovedSequenceNumbers.Any())
                    {
                        RemoveDeadletterDataGridRows(messageForm.RemovedSequenceNumbers);
                    }
                }
                finally
                {
                    Application.UseWaitCursor = false;
                }
            }
        }

        private void transferDeadletterDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var bindingList = transferDeadletterBindingSource.DataSource as BindingList<BrokeredMessage>;
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

        private void transferDeadletterDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = transferDeadletterDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ToolTipText = DoubleClickMessage;
        }

        private void grouperMessageText_CustomPaint(PaintEventArgs obj)
        {
            txtMessageText.Size = new Size(grouperMessageText.Size.Width - (txtMessageText.Location.X * 2),
                grouperMessageText.Size.Height - txtMessageText.Location.Y - txtMessageText.Location.X);
        }

        private void grouperMessageCustomProperties_CustomPaint(PaintEventArgs obj)
        {
            messagePropertyListView.Size = new Size(grouperMessageCustomProperties.Size.Width - messagePropertyListView.Location.X * 2,
                                                    grouperMessageCustomProperties.Size.Height - messagePropertyListView.Location.Y -
                                                    messagePropertyListView.Location.X);
        }

        private void grouperMessageSystemProperties_CustomPaint(PaintEventArgs obj)
        {
            messagePropertyGrid.Size = new Size(grouperMessageSystemProperties.Size.Width - messagePropertyGrid.Location.X * 2,
                                                grouperMessageSystemProperties.Size.Height - messagePropertyGrid.Location.Y - messagePropertyGrid.Location.X);
        }

        private void grouperDeadletterText_CustomPaint(PaintEventArgs obj)
        {
            txtDeadletterText.Size = new Size(grouperDeadletterText.Size.Width - txtDeadletterText.Location.X * 2,
                grouperDeadletterText.Size.Height - txtDeadletterText.Location.Y - txtDeadletterText.Location.X);
        }

        private void grouperDeadletterCustomProperties_CustomPaint(PaintEventArgs obj)
        {
            deadletterPropertyListView.Size =
                new Size(grouperDeadletterCustomProperties.Size.Width - deadletterPropertyListView.Location.X * 2,
                    grouperDeadletterCustomProperties.Size.Height - deadletterPropertyListView.Location.Y -
                    deadletterPropertyListView.Location.X);
        }

        private void grouperDeadletterSystemProperties_CustomPaint(PaintEventArgs obj)
        {
            deadletterPropertyGrid.Size =
                new Size(grouperDeadletterSystemProperties.Size.Width - deadletterPropertyGrid.Location.X * 2,
                    grouperDeadletterSystemProperties.Size.Height - deadletterPropertyGrid.Location.Y -
                    deadletterPropertyGrid.Location.X);
        }


        private void grouperTransferDeadletterText_CustomPaint(PaintEventArgs obj)
        {
            txtTransferDeadletterText.Size = new Size(grouperTransferDeadletterText.Size.Width - txtTransferDeadletterText.Location.X * 2,
                grouperTransferDeadletterText.Size.Height - txtTransferDeadletterText.Location.Y - txtTransferDeadletterText.Location.X);
        }

        private void grouperTransferDeadletterCustomProperties_CustomPaint(PaintEventArgs obj)
        {
            transferDeadletterPropertyListView.Size =
                new Size(grouperTransferDeadletterCustomProperties.Size.Width - transferDeadletterPropertyListView.Location.X * 2,
                    grouperTransferDeadletterCustomProperties.Size.Height - transferDeadletterPropertyListView.Location.Y -
                    transferDeadletterPropertyListView.Location.X);
        }

        private void grouperTransferDeadletterSystemProperties_CustomPaint(PaintEventArgs obj)
        {
            transferDeadletterPropertyGrid.Size =
                new Size(grouperTransferDeadletterSystemProperties.Size.Width - transferDeadletterPropertyGrid.Location.X * 2,
                    grouperTransferDeadletterSystemProperties.Size.Height - transferDeadletterPropertyGrid.Location.Y -
                    transferDeadletterPropertyGrid.Location.X);
        }

        private void grouperSessionState_CustomPaint(PaintEventArgs obj)
        {
            txtSessionState.Size = new Size(grouperSessionState.Size.Width - (txtSessionState.Location.X * 2),
                grouperSessionState.Size.Height - txtSessionState.Location.Y - txtSessionState.Location.X);
        }

        private void grouperSessionProperties_CustomPaint(PaintEventArgs obj)
        {
            sessionPropertyGrid.Size = new Size(
                grouperSessionProperties.Size.Width - (sessionPropertyGrid.Location.X * 2),
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
            saveSelectedMessageBodyAsFileToolStripMenuItem.Visible = !multipleSelectedRows;
            resubmitSelectedMessagesInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedMessagesToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedMessagesBodyAsFileToolStripMenuItem.Visible = multipleSelectedRows;
            messagesContextMenuStrip.Show(Cursor.Position);
        }

        private void repairAndResubmitMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            messagesDataGridView_CellDoubleClick(messagesDataGridView,
                new DataGridViewCellEventArgs(0, currentMessageRowIndex));
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

        void deleteSelectedMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteSelectedMessagesToolStripMenuItem_Click(sender, e);
        }

        async void deleteSelectedMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (deadletterDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var messages = deadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
                .Select(r => r.DataBoundItem as BrokeredMessage);

            string confirmationText;

            if (messages.Count() == 1)
            {
                confirmationText = "Are you sure you want to delete the selected message from the " +
                    $"deadletter subqueue for the {queueDescription.Path} queue?";
            }
            else
            {
                confirmationText = $"Are you sure you want to delete {messages.Count()} messages from the " +
                    $"deadletter subqueue for {queueDescription.Path} queue?";
            }

            using (var deleteForm = new DeleteForm(confirmationText))
            {
                if (deleteForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            var sequenceNumbersToDelete = messages.Select(s => s.SequenceNumber).ToList();
            var deadLetterMessageHandler = new DeadLetterMessageHandler(writeToLog, serviceBusHelper,
                MainForm.SingletonMainForm.ReceiveTimeout, queueDescription);

            try
            {
                Application.UseWaitCursor = true;
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var messagesDeleteCount = sequenceNumbersToDelete.Count;
                var result = await deadLetterMessageHandler.DeleteMessages(sequenceNumbersToDelete);
                RemoveDeadletterDataGridRows(result.DeletedSequenceNumbers);

                if (messagesDeleteCount > result.DeletedSequenceNumbers.Count)
                {
                    var messageText = deadLetterMessageHandler.GetFailureExplanation(result, messagesDeleteCount, delete: true);
                    Application.UseWaitCursor = false;
                    writeToLog(messageText);
                    MessageBox.Show(messageText, "Not all selected messages were deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (LockDurationTooLowException ldtle)
            {
                Application.UseWaitCursor = false;
                MessageBox.Show(ldtle.Message, "Delete operation cancelled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Application.UseWaitCursor = false;
            }

            MainForm.SingletonMainForm.refreshEntity_Click(null, null);
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
            saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem.Visible = !multipleSelectedRows;
            deleteSelectedMessageToolStripMenuItem.Visible = !multipleSelectedRows;

            resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedDeadletteredMessagesToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem.Visible = multipleSelectedRows;
            deleteSelectedMessagesToolStripMenuItem.Visible = multipleSelectedRows;

            deadletterContextMenuStrip.Show(Cursor.Position);
        }

        private void transferDeadletterDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex == -1)
            {
                return;
            }
            transferDeadletterDataGridView.Rows[e.RowIndex].Selected = true;
            var multipleSelectedRows = transferDeadletterDataGridView.SelectedRows.Count > 1;
            repairAndResubmitDeadletterToolStripMenuItem.Visible = !multipleSelectedRows;
            saveSelectedDeadletteredMessageToolStripMenuItem.Visible = !multipleSelectedRows;
            saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem.Visible = !multipleSelectedRows;
            resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedDeadletteredMessagesToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem.Visible = multipleSelectedRows;
            transferDeadletterContextMenuStrip.Show(Cursor.Position);
        }

        private void repairAndResubmitDeadletterMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deadletterDataGridView_CellDoubleClick(deadletterDataGridView,
                new DataGridViewCellEventArgs(0, currentDeadletterMessageRowIndex));
        }

        private void resubmitSelectedDeadletterMessagesInBatchModeToolStripMenuItem_Click(object sender,
            EventArgs e)
        {
            try
            {
                if (deadletterDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                using (var form = new MessageForm(queueDescription, deadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
                    .Select(r => (BrokeredMessage)r.DataBoundItem), serviceBusHelper, writeToLog))
                {
                    form.ShowDialog();
                    if (form.RemovedSequenceNumbers != null && form.RemovedSequenceNumbers.Any())
                    {
                        RemoveDeadletterDataGridRows(form.RemovedSequenceNumbers);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            // Rather than getting the updated entities from the forms we refresh all queues and topics to keep things 
            // simple.
            MainForm.SingletonMainForm.RefreshQueues();
            MainForm.SingletonMainForm.RefreshTopics();
        }

        private void repairAndResubmitTransferDeadletterMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transferDeadletterDataGridView_CellDoubleClick(transferDeadletterDataGridView,
                new DataGridViewCellEventArgs(0, currentTransferDeadletterMessageRowIndex));
        }

        private void resubmitSelectedTransferDeadletterMessagesInBatchModeToolStripMenuItem_Click(object sender,
            EventArgs e)
        {
            try
            {
                if (transferDeadletterDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                using (var form = new MessageForm(transferDeadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
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
                    FilterMessages();
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

        private void pictFindMessagesByDate_Click(object sender, EventArgs e)
        {
            try
            {
                messagesDataGridView.SuspendDrawing();
                messagesDataGridView.SuspendLayout();
                if (messageBindingList == null)
                {
                    return;
                }
                using (var form = new DateTimeRangeForm(messagesFilterFromDate, messagesFilterToDate))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    messagesFilterFromDate = form.DateTimeFrom;
                    messagesFilterToDate = form.DateTimeTo;
                    FilterMessages();
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

        private static bool IsWithinDateTimeRange(BrokeredMessage message, DateTime? fromDateTime, DateTime? toDateTime)
        {
            if (message.EnqueuedTimeUtc < (fromDateTime ?? DateTime.MinValue))
            {
                return false;
            }
            if (message.EnqueuedTimeUtc > (toDateTime ?? DateTime.MaxValue))
            {
                return false;
            }
            return true;
        }

        private void FilterMessages()
        {
            var bindingList = new SortableBindingList<BrokeredMessage>();
            try
            {
                if (messagesFilterFromDate == null && messagesFilterToDate == null && string.IsNullOrWhiteSpace(messagesFilterExpression))
                {
                    bindingList = messageBindingList;
                    messagesBindingSource.DataSource = messageBindingList;
                    messagesDataGridView.DataSource = messagesBindingSource;
                    writeToLog(FilterExpressionRemovedMessage);
                }
                else
                {
                    var filteredList = messageBindingList.ToList();
                    if (!string.IsNullOrWhiteSpace(messagesFilterExpression))
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
                            writeToLog(string.Format(FilterExpressionNotValidMessage, messagesFilterExpression,
                                ex.Message));
                            return;
                        }

                        filteredList = filteredList.Where(filter.Match).ToList();
                    }

                    if (messagesFilterFromDate != null || messagesFilterToDate != null)
                    {
                        filteredList = filteredList.Where(msg => IsWithinDateTimeRange(msg, messagesFilterFromDate, messagesFilterToDate)).ToList();
                    }

                    bindingList = new SortableBindingList<BrokeredMessage>(filteredList)
                    {
                        AllowEdit = false,
                        AllowNew = false,
                        AllowRemove = false
                    };
                    messagesBindingSource.DataSource = bindingList;
                    messagesDataGridView.DataSource = messagesBindingSource;
                    writeToLog(string.Format(FilterExpressionAppliedMessage, messagesFilterExpression, messagesFilterFromDate ?? DateTime.MinValue, messagesFilterToDate ?? DateTime.MaxValue, bindingList.Count));
                }
            }
            finally
            {
                if (!bindingList.Any())
                {
                    txtMessageText.Text = string.Empty;
                    messagePropertyListView.Items.Clear();
                    messagePropertyGrid.SelectedObject = null;
                }
                else
                {
                    if (messagesDataGridView.Rows.Count > 0)
                    {
                        brokeredMessage = null;
                        messagesDataGridView_RowEnter(this, new DataGridViewCellEventArgs(0, 0));
                    }
                }
            }
        }

        private void FilterDeadletters()
        {
            var bindingList = new SortableBindingList<BrokeredMessage>();
            try
            {
                if (deadletterFilterFromDate == null && deadletterFilterToDate == null && string.IsNullOrWhiteSpace(deadletterFilterExpression))
                {
                    bindingList = deadletterBindingList;
                    deadletterBindingSource.DataSource = deadletterBindingList;
                    deadletterDataGridView.DataSource = deadletterBindingSource;
                    writeToLog(FilterExpressionRemovedMessage);
                }
                else
                {
                    var filteredList = deadletterBindingList.ToList();
                    if (!string.IsNullOrWhiteSpace(deadletterFilterExpression))
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
                            writeToLog(string.Format(FilterExpressionNotValidMessage, deadletterFilterExpression,
                                ex.Message));
                            return;
                        }

                        filteredList = filteredList.Where(filter.Match).ToList();
                    }

                    if (deadletterFilterFromDate != null || deadletterFilterToDate != null)
                    {
                        filteredList = filteredList.Where(msg => IsWithinDateTimeRange(msg, deadletterFilterFromDate, deadletterFilterToDate)).ToList();
                    }

                    bindingList = new SortableBindingList<BrokeredMessage>(filteredList)
                    {
                        AllowEdit = false,
                        AllowNew = false,
                        AllowRemove = false
                    };
                    deadletterBindingSource.DataSource = bindingList;
                    deadletterDataGridView.DataSource = deadletterBindingSource;
                    writeToLog(string.Format(FilterExpressionAppliedMessage, deadletterFilterExpression, deadletterFilterFromDate ?? DateTime.MinValue, deadletterFilterToDate ?? DateTime.MaxValue, bindingList.Count));
                }
            }
            finally
            {
                if (!bindingList.Any())
                {
                    txtDeadletterText.Text = string.Empty;
                    deadletterPropertyListView.Items.Clear();
                    deadletterPropertyGrid.SelectedObject = null;
                }
                else
                {
                    if (deadletterDataGridView.Rows.Count > 0)
                    {
                        deadletterMessage = null;
                        deadletterDataGridView_RowEnter(this, new DataGridViewCellEventArgs(0, 0));
                    }
                }
            }
        }

        private void pictFindDeadletterByDate_Click(object sender, EventArgs e)
        {
            try
            {
                deadletterDataGridView.SuspendDrawing();
                deadletterDataGridView.SuspendLayout();
                if (deadletterBindingList == null)
                {
                    return;
                }
                using (var form = new DateTimeRangeForm(deadletterFilterFromDate, deadletterFilterToDate))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    deadletterFilterFromDate = form.DateTimeFrom;
                    deadletterFilterToDate = form.DateTimeTo;
                    FilterDeadletters();
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
                using (var form = new TextForm(FilterExpressionTitle, FilterExpressionLabel, deadletterFilterExpression)
                )
                {
                    form.Size = new Size(600, 200);
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    deadletterFilterExpression = form.Content;
                    FilterDeadletters();
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

        private void pictureBoxByDate_MouseEnter(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.Image = Properties.Resources.FindByDateExtensionRaised;
            }
        }

        private void pictureBoxByDate_MouseLeave(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.Image = Properties.Resources.FindByDateExtension;
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

        private void authorizationRulesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void deadletterDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void transferDeadletterDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        #region Save Messages

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
                saveFileDialog.RestoreDirectory = true;
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

        void saveSelectedMessageBodyAsFileToolStripMenuItem_Click(object sender, EventArgs e)
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

                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = TxtExtension;
                saveFileDialog.Filter = AllFilesFilter;
                saveFileDialog.FileName = CreateFileNameAutoRecognize();
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
                    writer.Write(txtMessageText.Text);
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
                var messages =
                    messagesDataGridView.SelectedRows.Cast<DataGridViewRow>()
                        .Select(r => r.DataBoundItem as BrokeredMessage);
                IEnumerable<BrokeredMessage> brokeredMessages = messages as BrokeredMessage[] ?? messages.ToArray();
                if (!brokeredMessages.Any())
                {
                    return;
                }
                saveFileDialog.RestoreDirectory = true;
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
                    var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm,
                         MainForm.SingletonMainForm.UseAscii, out _));
                    writer.Write(MessageSerializationHelper.Serialize(brokeredMessages, bodies));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        void saveSelectedMessagesBodyAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (messagesDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }

                var messages =
                    messagesDataGridView.SelectedRows.Cast<DataGridViewRow>()
                                        .Select(r => r.DataBoundItem as BrokeredMessage);
                IEnumerable<BrokeredMessage> brokeredMessages = messages as BrokeredMessage[] ?? messages.ToArray();
                if (!brokeredMessages.Any())
                {
                    return;
                }

                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = TxtExtension;
                saveFileDialog.Filter = AllFilesFilter;
                saveFileDialog.FileName = CreateFileNameAutoRecognize();
                if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }

                var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm,
                    MainForm.SingletonMainForm.UseAscii, out _));
                var count = 0;
                foreach (var body in bodies)
                {
                    count++;
                    var fileNameParts = saveFileDialog.FileName.Split('.').ToList();
                    var fileExtension = fileNameParts.Last();
                    fileNameParts.RemoveAt(fileNameParts.IndexOf(fileExtension));
                    fileNameParts.Add($"({count}).{fileExtension}");
                    var fileName = string.Join(".", fileNameParts);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    using (var writer = new StreamWriter(fileName))
                    {
                        writer.Write(body);
                    }
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
                saveFileDialog.RestoreDirectory = true;
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

        void saveSelectedDeadletteredMessageBodyAsFileToolStripMenuItem_Click(object sender, EventArgs e)
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

                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = TxtExtension;
                saveFileDialog.Filter = AllFilesFilter;
                saveFileDialog.FileName = CreateFileNameAutoRecognize();
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
                    writer.Write(txtDeadletterText.Text);
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
                var messages = deadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
                        .Select(r => r.DataBoundItem as BrokeredMessage);
                IEnumerable<BrokeredMessage> brokeredMessages = messages as BrokeredMessage[] ?? messages.ToArray();
                if (!brokeredMessages.Any())
                {
                    return;
                }
                saveFileDialog.RestoreDirectory = true;
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
                    var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm,
                         MainForm.SingletonMainForm.UseAscii, out _));
                    writer.Write(MessageSerializationHelper.Serialize(brokeredMessages, bodies));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        void saveSelectedDeadletteredMessagesBodyAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (deadletterDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }

                var messages = deadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
                                                     .Select(r => r.DataBoundItem as BrokeredMessage);
                IEnumerable<BrokeredMessage> brokeredMessages = messages as BrokeredMessage[] ?? messages.ToArray();
                if (!brokeredMessages.Any())
                {
                    return;
                }

                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = TxtExtension;
                saveFileDialog.Filter = AllFilesFilter;
                saveFileDialog.FileName = CreateFileNameAutoRecognize();
                if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }

                var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm,
                    MainForm.SingletonMainForm.UseAscii, out _));
                var count = 0;
                foreach (var body in bodies)
                {
                    count++;
                    var fileNameParts = saveFileDialog.FileName.Split('.').ToList();
                    var fileExtension = fileNameParts.Last();
                    fileNameParts.RemoveAt(fileNameParts.IndexOf(fileExtension));
                    fileNameParts.Add($"({count}).{fileExtension}");
                    var fileName = string.Join(".", fileNameParts);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    using (var writer = new StreamWriter(fileName))
                    {
                        writer.Write(body);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveSelectedTransferDeadletteredMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentTransferDeadletterMessageRowIndex < 0)
                {
                    return;
                }
                var bindingList = transferDeadletterBindingSource.DataSource as BindingList<BrokeredMessage>;
                if (bindingList == null)
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTransferDeadletterText.Text))
                {
                    return;
                }
                saveFileDialog.RestoreDirectory = true;
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
                    writer.Write(MessageSerializationHelper.Serialize(bindingList[currentTransferDeadletterMessageRowIndex],
                        txtTransferDeadletterText.Text));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        void saveSelectedTransferDeadletteredMessageBodyAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentTransferDeadletterMessageRowIndex < 0)
                {
                    return;
                }

                var bindingList = transferDeadletterBindingSource.DataSource as BindingList<BrokeredMessage>;
                if (bindingList == null)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTransferDeadletterText.Text))
                {
                    return;
                }

                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = TxtExtension;
                saveFileDialog.Filter = AllFilesFilter;
                saveFileDialog.FileName = CreateFileNameAutoRecognize();
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
                    writer.Write(txtTransferDeadletterText.Text);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveSelectedTransferDeadletteredMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (transferDeadletterDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                var messages = transferDeadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
                        .Select(r => r.DataBoundItem as BrokeredMessage);
                IEnumerable<BrokeredMessage> brokeredMessages = messages as BrokeredMessage[] ?? messages.ToArray();
                if (!brokeredMessages.Any())
                {
                    return;
                }
                saveFileDialog.RestoreDirectory = true;
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
                    var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm,
                         MainForm.SingletonMainForm.UseAscii, out _));
                    writer.Write(MessageSerializationHelper.Serialize(brokeredMessages, bodies));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        void saveSelectedTransferDeadletteredMessagesBodyAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (transferDeadletterDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }

                var messages = transferDeadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
                                                             .Select(r => r.DataBoundItem as BrokeredMessage);
                IEnumerable<BrokeredMessage> brokeredMessages = messages as BrokeredMessage[] ?? messages.ToArray();
                if (!brokeredMessages.Any())
                {
                    return;
                }

                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = TxtExtension;
                saveFileDialog.Filter = AllFilesFilter;
                saveFileDialog.FileName = CreateFileNameAutoRecognize();
                if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }

                var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm,
                    MainForm.SingletonMainForm.UseAscii, out _));
                var count = 0;
                foreach (var body in bodies)
                {
                    count++;
                    var fileNameParts = saveFileDialog.FileName.Split('.').ToList();
                    var fileExtension = fileNameParts.Last();
                    fileNameParts.RemoveAt(fileNameParts.IndexOf(fileExtension));
                    fileNameParts.Add($"({count}).{fileExtension}");
                    var fileName = string.Join(".", fileNameParts);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                    using (var writer = new StreamWriter(fileName))
                    {
                        writer.Write(body);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        #endregion Save Messages

        private string CreateFileName()
        {
            return string.Format(MessageFileFormat,
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(serviceBusHelper.Namespace),
                DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }

        private string CreateFileNameAutoRecognize()
        {
            return string.Format(MessageFileFormatAutoRecognize,
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(serviceBusHelper.Namespace),
                DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }

        private async void btnPurgeMessages_Click(object sender, EventArgs e)
        {
            await PurgeMessagesAsync();
        }

        private async void btnPurgeDeadletterQueueMessages_Click(object sender, EventArgs e)
        {
            await PurgeDeadletterQueueMessagesAsync();
        }

        void RemoveDeadletterDataGridRows(IEnumerable<long> sequenceNumbersToRemove)
        {
            var rowsToRemove = new List<DataGridViewRow>(sequenceNumbersToRemove.Count());

            foreach (DataGridViewRow row in deadletterDataGridView.Rows)
            {
                var message = (BrokeredMessage)row.DataBoundItem;

                if (sequenceNumbersToRemove.Contains(message.SequenceNumber))
                {
                    rowsToRemove.Add(row);
                    if (rowsToRemove.Count >= sequenceNumbersToRemove.Count())
                    {
                        break;
                    }
                }
            }

            for (var rowIndex = rowsToRemove.Count - 1; rowIndex >= 0; --rowIndex)
            {
                var row = rowsToRemove[rowIndex];
                deadletterDataGridView.Rows.Remove(row);
            }

            deadletterDataGridView.ClearSelection();
        }
        #endregion
    }
}
