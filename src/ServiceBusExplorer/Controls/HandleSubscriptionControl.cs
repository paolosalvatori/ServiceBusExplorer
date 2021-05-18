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

using Microsoft.ServiceBus.Messaging;

using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.ServiceBus.Helpers;
using ServiceBusExplorer.UIHelpers;
using ServiceBusExplorer.Utilities.Helpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer.Controls
{
    public partial class HandleSubscriptionControl : UserControl
    {
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

        const string DefaultMessageTimeToLive = "DefaultMessageTimeToLive";
        const string LockDuration = "LockDuration";
        const string AutoDeleteOnIdle = "AutoDeleteOnIdle";

        private const string MessagesPeekedFromTheSubscription = "[{0}] messages peeked from the subscription [{1}].";
        private const string MessagesPeekedFromTheDeadletterQueue = "[{0}] messages peeked from the dead-letter queue of the subscription [{1}].";
        private const string MessagesReceivedFromTheSubscription = "[{0}] messages received from the subscription [{1}].";
        private const string MessagesReceivedFromTheDeadletterQueue = "[{0}] messages received from the dead-letter queue of the subscription [{1}].";
        private const string SessionsGotFromTheSubscription = "[{0}] sessions retrieved for the subscription [{1}].";
        private const string NoMessageReceivedFromTheSubscription = "The timeout  of [{0}] seconds has expired and no message was retrieved from the subscription [{1}].";
        private const string NoMessageReceivedFromTheDeadletterQueue = "The timeout  of [{0}] seconds has expired and no message was retrieved from the dead-letter queue of the subscription [{1}].";

        private const string RetrieveMessagesFromSubscription = "Retrieve messages from subscription";
        private const string RetrieveMessagesFromDeadletterQueue = "Retrieve messages from dead-letter queue";
        private const string SelectEntityDialogTitle = "Select a target Queue or Topic";
        private const string SelectEntityGrouperTitle = "Forward To";
        private const string SelectEntityLabelText = "Target Queue or Topic:";

        private const string DoubleClickMessage = "Double-click a row to repair and resubmit the corresponding message.";

        private const string FilterExpressionTitle = "Define Filter Expression";
        private const string FilterExpressionLabel = "Filter Expression";
        private const string FilterExpressionNotValidMessage = "The filter expression [{0}] is not valid: {1}";
        private const string FilterExpressionAppliedMessage =
            "The filter expression [{0}] from {1} to {2} has been successfully applied. [{3}] messages retrieved.";
        private const string FilterExpressionRemovedMessage = "The filter expression has been removed.";

        //***************************
        // Tooltips
        //***************************
        private const string NameTooltip = "Gets or sets the subscription name.";
        private const string DefaultMessageTimeToLiveTooltip = "Gets or sets the default message time to live of a queue.";
        private const string FilterExpressionTooltip = "Gets or sets the filter expression of the default rule.";
        private const string FilterActionTooltip = "Gets or sets the filter action of the default rule.";
        private const string LockDurationTooltip = "Gets or sets the lock duration timespan associated with this queue.";
        private const string MaxDeliveryCountTooltip = "Gets or sets the maximum delivery count. A message is automatically dead-lettered after this number of deliveries.";
        private const string UserMetadataTooltip = "Gets or sets the user metadata.";
        private const string AutoDeleteOnIdleTooltip = "Gets or sets the maximum period of idleness after which the queue is auto deleted.";
        private const string ForwardToTooltip = "Gets or sets the path to the recipient to which the message is forwarded.";
        private const string ForwardDeadLetteredMessagesToTooltip = "Gets or sets the path to the recipient to which the dead-lettered message is forwarded.";

        //***************************
        // Property Labels
        //***************************
        private const string Status = "Status";
        private const string CreatedAt = "Created At";
        private const string AccessedAt = "Accessed At";
        private const string UpdatedAt = "Updated At";
        private const string MessageCount = "Message Count";
        private const string ActiveMessageCount = "Active Message Count";
        private const string DeadletterMessageCount = "Dead-letter Message Count";
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
        private const string SaveAsTitle = "Save File As";
        private const string JsonExtension = "json";
        private const string JsonFilter = "JSON Files|*.json|Text Documents|*.txt";
        private const string MessageFileFormat = "BrokeredMessage_{0}_{1}.json";


        //***************************
        // Sunscription Constants
        //***************************
        private const string SubscriptionEntity = "Subscription";
        private const string SubscriptionPathFormat = "{0}/Subscriptions/{1}";
        #endregion

        #region Private Fields
        private SubscriptionWrapper subscriptionWrapper;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private BrokeredMessage brokeredMessage;
        private BrokeredMessage deadletterMessage;
        private int currentMessageRowIndex;
        private int currentDeadletterMessageRowIndex;
        private bool sorting;
        private string messagesFilterExpression;
        private string deadletterFilterExpression;
        private DateTime? messagesFilterFromDate;
        private DateTime? messagesFilterToDate;
        private DateTime? deadletterFilterFromDate;
        private DateTime? deadletterFilterToDate;
        private SortableBindingList<BrokeredMessage> messageBindingList;
        private SortableBindingList<BrokeredMessage> deadletterBindingList;
        private SortableBindingList<MessageSession> sessionBindingList;
        private bool buttonsMoved;
        #endregion

        #region Public Constructors
        public HandleSubscriptionControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, SubscriptionWrapper subscriptionWrapper)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.subscriptionWrapper = subscriptionWrapper;

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
                    messageCustomPropertyGrid.SelectedObject = null;
                    messagePropertyGrid.SelectedObject = null;
                    var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                           serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector) ?
                                           Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector :
                                           null;
                    if (subscriptionWrapper.TopicDescription.EnablePartitioning)
                    {
                        ReadMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                    }
                    else
                    {
                        GetMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
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
                deadletterCustomPropertyGrid.SelectedObject = null;
                deadletterPropertyGrid.SelectedObject = null;
                var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                       serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector) ?
                    Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector :
                    null;
                if (subscriptionWrapper.TopicDescription.EnablePartitioning)
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
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromDeadletterQueue, MainForm.SingletonMainForm.TopCount, serviceBusHelper.BrokeredMessageInspectors.Keys))
            {
                if (receiveModeForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtDeadletterText.Text = string.Empty;
                deadletterCustomPropertyGrid.SelectedObject = null;
                deadletterPropertyGrid.SelectedObject = null;
                var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                       serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector) ?
                    Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector :
                    null;
                if (subscriptionWrapper.TopicDescription.EnablePartitioning)
                {
                    ReadDeadletterMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
                else
                {
                    GetDeadletterMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
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

        public async Task PurgeMessagesAsync()
        {
            await this.DoPurge(PurgeStrategies.Messages, $"Would you like to purge the {subscriptionWrapper.SubscriptionDescription.Name} subscription?");
        }

        public async Task PurgeDeadletterQueueMessagesAsync()
        {
            await this.DoPurge(PurgeStrategies.DeadletteredMessages, $"Would you like to purge the dead-letter queue of the {subscriptionWrapper.SubscriptionDescription.Name} subscription?");
        }

        public async Task PurgeAllMessagesAsync()
        {
            await this.DoPurge(PurgeStrategies.All, $"Would you like to purge all (messages and dead-lettered messages) from the {subscriptionWrapper.SubscriptionDescription.Name} subscription?");
        }

        private async Task DoPurge(PurgeStrategies purgeStrategy, string deleteConfirmation)
        {
            if (!DeleteForm.ShowAndWaitUserConfirmation(this, deleteConfirmation))
            {
                return;
            }

            Application.UseWaitCursor = true;

            TopicSubscriptionServiceBusPurger purger = new TopicSubscriptionServiceBusPurger(this.serviceBusHelper.GetServiceBusHelper2());
            purger.PurgeFailed += (o, e) => this.HandleException(e.Exception);
            purger.PurgeCompleted += (o, e) => writeToLog($"[{e.TotalMessagesPurged}] messages have been purged from the{(e.IsDeadLetterQueue ? " dead-letter queue of the" : "")} [{e.EntityPath}] subscription in [{e.ElapsedMilliseconds / 1000}] seconds.");
            await purger.Purge(purgeStrategy, await this.serviceBusHelper.GetSubscriptionProperties(subscriptionWrapper));

            await MainForm.SingletonMainForm.RefreshSelectedEntity();
            Application.UseWaitCursor = false;
        }
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            // Splitter controls
            messagesSplitContainer.SplitterWidth = 16;
            sessionsSplitContainer.SplitterWidth = 16;
            deadletterSplitContainer.SplitterWidth = 16;

            messageMainSplitContainer.SplitterWidth = 8;
            deadletterMainSplitContainer.SplitterWidth = 8;
            sessionMainSplitContainer.SplitterDistance = 8;

            messagePropertiesSplitContainer.SplitterWidth = 8;

            // Tabe pages
            DisablePage(MessagesTabPage);
            DisablePage(SessionsTabPage);
            DisablePage(DeadletterTabPage);

            if (subscriptionWrapper != null &&
                subscriptionWrapper.TopicDescription != null &&
                subscriptionWrapper.SubscriptionDescription != null)
            {
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
                sessionsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                sessionsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                sessionsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

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
                toolTip.SetToolTip(tsDefaultMessageTimeToLive, DefaultMessageTimeToLiveTooltip);
                toolTip.SetToolTip(txtFilter, FilterExpressionTooltip);
                toolTip.SetToolTip(txtAction, FilterActionTooltip);
                toolTip.SetToolTip(tsLockDuration, LockDurationTooltip);
                toolTip.SetToolTip(txtMaxDeliveryCount, MaxDeliveryCountTooltip);
                toolTip.SetToolTip(tsAutoDeleteOnIdle, AutoDeleteOnIdleTooltip);
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
                txtName.Focus();
            }
        }

        private void InitializeData()
        {
            // Initialize buttons
            var forwardTo = subscriptionWrapper.SubscriptionDescription.ForwardTo;
            var forwardDeadLetteredMessagesTo = subscriptionWrapper.SubscriptionDescription.ForwardDeadLetteredMessagesTo;
            btnCreateDelete.Text = DeleteText;
            btnCancelUpdate.Text = UpdateText;
            btnChangeStatus.Text = subscriptionWrapper.SubscriptionDescription.Status == EntityStatus.Active ? DisableText : EnableText;
            btnRefresh.Visible = true;
            btnChangeStatus.Visible = true;
            btnMessages.Visible = true;
            btnSessions.Visible = subscriptionWrapper.SubscriptionDescription.RequiresSession;
            btnMessages.Visible = string.IsNullOrWhiteSpace(forwardTo);
            btnDeadletter.Visible = string.IsNullOrWhiteSpace(forwardDeadLetteredMessagesTo);

            if (btnMessages.Visible && !btnSessions.Visible && !buttonsMoved)
            {
                btnPurgeMessages.Location = btnPurgeDeadletterQueueMessages.Location;
                btnPurgeDeadletterQueueMessages.Location = btnSessions.Location;
                buttonsMoved = true;
            }

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
            if (!string.IsNullOrWhiteSpace(forwardTo))
            {
                txtForwardTo.Text = serviceBusHelper.GetAddressRelativeToNamespace(forwardTo);
            }

            // ForwardDeadLetteredMessagesTo
            if (!string.IsNullOrWhiteSpace(forwardDeadLetteredMessagesTo))
            {
                txtForwardDeadLetteredMessagesTo.Text = serviceBusHelper.GetAddressRelativeToNamespace(forwardDeadLetteredMessagesTo);
            }

            // MaxDeliveryCount
            txtMaxDeliveryCount.Text = subscriptionWrapper.SubscriptionDescription.MaxDeliveryCount.ToString(CultureInfo.InvariantCulture);

            // DefaultMessageTimeToLive
            tsDefaultMessageTimeToLive.TimeSpanValue = subscriptionWrapper.SubscriptionDescription.DefaultMessageTimeToLive;

            // LockDuration
            tsLockDuration.TimeSpanValue = subscriptionWrapper.SubscriptionDescription.LockDuration;

            // AutoDeleteOnIdle
            tsAutoDeleteOnIdle.TimeSpanValue = subscriptionWrapper.SubscriptionDescription.AutoDeleteOnIdle;

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

        private void GetMessages(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber)
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
                        IEnumerable<BrokeredMessage> messageEnumerable;

                        if (totalRetrieved == 0 && fromSequenceNumber.HasValue)
                        {
                            messageEnumerable = subscriptionClient.PeekBatch(fromSequenceNumber.Value, count);
                        }
                        else
                        {
                            messageEnumerable = subscriptionClient.PeekBatch(count);
                        }

                        if (messageEnumerable == null)
                        {
                            break;
                        }
                        var messageArray = messageEnumerable as BrokeredMessage[] ?? messageEnumerable.ToArray();
                        var partialList = messageInspector != null ?
                                       messageArray.Select(b => messageInspector.AfterReceiveMessage(b)).ToList() :
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
                        brokeredMessages.AddRange(messageInspector != null ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b)) : enumerable);
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
                messageMainSplitContainer.SplitterDistance = messageMainSplitContainer.Size.Height / 2 - 8;

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

        private void ReadMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber)
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
                        BrokeredMessage message;

                        if (i == 0 && fromSequenceNumber.HasValue)
                        {
                            message = subscriptionClient.Peek(fromSequenceNumber.Value);
                        }
                        else
                        {
                            message = subscriptionClient.Peek();
                        }

                        if (message != null)
                        {
                            if (messageInspector != null)
                            {
                                message = messageInspector.AfterReceiveMessage(message);
                            }
                            brokeredMessages.Add(message);
                        }
                        else
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
                messageMainSplitContainer.SplitterDistance = messageMainSplitContainer.Size.Height / 2 - 8;

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

                if (peek)
                {
                    var messageReceiver = serviceBusHelper.MessagingFactory.CreateMessageReceiver(SubscriptionClient.FormatDeadLetterPath(subscriptionWrapper.SubscriptionDescription.TopicPath,
                                                                                                                              subscriptionWrapper.SubscriptionDescription.Name),
                                                                                              ReceiveMode.PeekLock);
                    var totalRetrieved = 0;
                    var retrieved = 0;

                    do
                    {
                        IEnumerable<BrokeredMessage> messages;

                        if (retrieved == 0 && fromSequenceNumber.HasValue)
                        {
                            messages = messageReceiver.PeekBatch(fromSequenceNumber.Value, all ?
                                MainForm.SingletonMainForm.TopCount :
                                count - totalRetrieved);
                        }
                        else
                        {
                            messages = messageReceiver.PeekBatch(all ?
                                MainForm.SingletonMainForm.TopCount :
                                count - totalRetrieved);
                        }

                        var enumerable = messages as BrokeredMessage[] ?? messages.ToArray();
                        retrieved = enumerable.Count();
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b)) : enumerable);
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
                        brokeredMessages.AddRange(messageInspector != null ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b)) : enumerable);
                    }
                    while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesReceivedFromTheDeadletterQueue, brokeredMessages.Count, subscriptionWrapper.SubscriptionDescription.Name));
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

        private void ReadDeadletterMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector messageInspector, long? fromSequenceNumber)
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
                        BrokeredMessage message;

                        if (i == 0 && fromSequenceNumber.HasValue)
                        {
                            message = messageReceiver.Peek(fromSequenceNumber.Value);
                        }
                        else
                        {
                            message = messageReceiver.Peek();
                        }

                        if (message != null)
                        {
                            if (messageInspector != null)
                            {
                                message = messageInspector.AfterReceiveMessage(message);
                            }
                            brokeredMessages.Add(message);
                        }
                        else
                        {
                            break;
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

                    if (tsDefaultMessageTimeToLive.IsFilled)
                    {
                        if (tsDefaultMessageTimeToLive.TimeSpanValue.HasValue)
                        {
                            subscriptionDescription.DefaultMessageTimeToLive = tsDefaultMessageTimeToLive.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsDefaultMessageTimeToLive.GetErrorMessage(DefaultMessageTimeToLive));
                            return;
                        }
                    }

                    if (tsLockDuration.IsFilled)
                    {
                        if (tsLockDuration.TimeSpanValue.HasValue)
                        {
                            subscriptionDescription.LockDuration = tsLockDuration.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsLockDuration.GetErrorMessage(LockDuration));
                            return;
                        }
                    }

                    if (tsAutoDeleteOnIdle.IsFilled)
                    {
                        if (tsAutoDeleteOnIdle.TimeSpanValue.HasValue)
                        {
                            subscriptionDescription.AutoDeleteOnIdle = tsAutoDeleteOnIdle.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsAutoDeleteOnIdle.GetErrorMessage(AutoDeleteOnIdle));
                            return;
                        }
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
                NativeMethods.HideCaret(textBox.Handle);
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

                    if (tsDefaultMessageTimeToLive.IsFilled)
                    {
                        if (tsDefaultMessageTimeToLive.TimeSpanValue.HasValue)
                        {
                            subscriptionWrapper.SubscriptionDescription.DefaultMessageTimeToLive = tsDefaultMessageTimeToLive.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsDefaultMessageTimeToLive.GetErrorMessage(DefaultMessageTimeToLive));
                            return;
                        }
                    }

                    if (tsLockDuration.IsFilled)
                    {
                        if (tsLockDuration.TimeSpanValue.HasValue)
                        {
                            subscriptionWrapper.SubscriptionDescription.LockDuration = tsLockDuration.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsLockDuration.GetErrorMessage(LockDuration));
                            return;
                        }
                    }

                    if (tsAutoDeleteOnIdle.IsFilled)
                    {
                        if (tsAutoDeleteOnIdle.TimeSpanValue.HasValue)
                        {
                            subscriptionWrapper.SubscriptionDescription.AutoDeleteOnIdle = tsAutoDeleteOnIdle.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsAutoDeleteOnIdle.GetErrorMessage(AutoDeleteOnIdle));
                            return;
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
                    dataGridView.Columns[4].Width = columnWidth + (width - (columnWidth * 4)) + 10;
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

                LanguageDetector.SetFormattedMessage(serviceBusHelper, brokeredMessage, txtMessageText);

                messageCustomPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter<string, object>(brokeredMessage.Properties);
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

                LanguageDetector.SetFormattedMessage(serviceBusHelper, deadletterMessage, txtDeadletterText);

                deadletterCustomPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter<string, object>(deadletterMessage.Properties);
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
            using (var messageForm = new MessageForm(subscriptionWrapper, bindingList[e.RowIndex],
                serviceBusHelper, writeToLog))
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
            messageCustomPropertyGrid.Size = new Size(grouperMessageCustomProperties.Size.Width - (messageCustomPropertyGrid.Location.X * 2),
                                                    grouperMessageCustomProperties.Size.Height - messageCustomPropertyGrid.Location.Y - messageCustomPropertyGrid.Location.X);
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
            deadletterCustomPropertyGrid.Size = new Size(grouperDeadletterCustomProperties.Size.Width - (deadletterCustomPropertyGrid.Location.X * 2),
                                                       grouperDeadletterCustomProperties.Size.Height - deadletterCustomPropertyGrid.Location.Y - deadletterCustomPropertyGrid.Location.X);
        }

        private void grouperDeadletterSystemProperties_CustomPaint(PaintEventArgs obj)
        {
            deadletterPropertyGrid.Size = new Size(grouperDeadletterSystemProperties.Size.Width - (deadletterPropertyGrid.Location.X * 2),
                                                   grouperDeadletterSystemProperties.Size.Height - deadletterPropertyGrid.Location.Y - deadletterPropertyGrid.Location.X);
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
                    $"dead-letter subqueue for the {subscriptionWrapper.SubscriptionDescription.Name} subscription?";
            }
            else
            {
                confirmationText = $"Are you sure you want to delete {messages.Count()} messages from the " +
                    $"dead-letter subqueue for {subscriptionWrapper.SubscriptionDescription.Name} subscription?";
            }

            using (var deleteForm = new DeleteForm(confirmationText))
            {
                if (deleteForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            var sequenceNumbersToDelete = messages.Select(s => s?.SequenceNumber).ToList();
            var deadLetterMessageHandler = new DeadLetterMessageHandler(writeToLog, serviceBusHelper,
                MainForm.SingletonMainForm.ReceiveTimeout, subscriptionWrapper);

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

            await MainForm.SingletonMainForm.RefreshSelectedEntity();
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
            deleteSelectedMessageToolStripMenuItem.Visible = !multipleSelectedRows;

            resubmitSelectedDeadletterInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedDeadletteredMessagesToolStripMenuItem.Visible = multipleSelectedRows;
            deleteSelectedMessagesToolStripMenuItem.Visible = multipleSelectedRows;

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
                using (var form = new MessageForm(subscriptionWrapper, deadletterDataGridView.SelectedRows.Cast<DataGridViewRow>()
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
            await MainForm.SingletonMainForm.RefreshQueues();
            await MainForm.SingletonMainForm.RefreshTopics();
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

        private void FilterMessages()
        {
            if (messagesFilterFromDate == null && messagesFilterToDate == null && string.IsNullOrWhiteSpace(messagesFilterExpression))
            {
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

                var bindingList = new SortableBindingList<BrokeredMessage>(filteredList)
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

        private void FilterDeadletters()
        {
            if (deadletterFilterFromDate == null && deadletterFilterToDate == null && string.IsNullOrWhiteSpace(deadletterFilterExpression))
            {
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

                var bindingList = new SortableBindingList<BrokeredMessage>(filteredList)
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

        private string CreateFileName()
        {
            return string.Format(MessageFileFormat,
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
