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

#nullable enable
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Common.Contracts;
using Common.Models;
using Microsoft.Extensions.Logging;
using ServiceBusExplorer.Common.Helpers;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        // CheckListBox item texts
        //***************************
        private const string EnableBatchedOperationsItemText = "Enable Batched Operations";
        private const string EnableDeadLetteringOnMessageExpirationItemText = "Enable Dead-lettering On Message Expiration";
        private const string EnablePartitioningItemText = "Enable Partitioning";
        private const string EnableExpressItemText = "Enable Express";
        private const string RequiresDuplicateDetectionItemText = "Requires Duplicate Detection";
        private const string RequiresSessionItemText = "Requires Session";
        private const string SupportOrderingItemText = "Enforce Message Ordering";
        private const string IsAnonymousAccessibleItemText = "Is Anonymous Accessible";

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

        private const string DefaultMessageTimeToLive = "DefaultMessageTimeToLive";
        private const string DuplicateDetectionHistoryTimeWindow = "DuplicateDetectionHistoryTimeWindow";
        private const string LockDuration = "LockDuration";
        private const string AutoDeleteOnIdle = "AutoDeleteOnIdle";

        private const string CannotForwardToItself =
            "The value of the ForwardTo property of the current queue cannot be set to itself.";

        private const string CannotForwardDeadLetteredMessagesToItself =
            "The value of the ForwardDeadLetteredMessagesTo property of the current queue cannot be set to itself.";

        private const string MessagesPeekedFromTheQueue = "[{0}] messages peeked from the queue [{1}].";

        private const string MessagesPeekedFromTheDeadletterQueue =
            "[{0}] messages peeked from the dead-letter queue of the queue [{1}].";

        private const string MessagesPeekedFromTheTransferDeadletterQueue =
            "[{0}] messages peeked from the transfer dead-letter queue of the queue [{1}].";

        private const string MessagesReceivedFromTheQueue = "[{0}] messages received from the queue [{1}].";

        private const string MessagesReceivedFromTheDeadletterQueue =
            "[{0}] messages received from the dead-letter queue of the queue [{1}].";

        private const string MessagesReceivedFromTheTransferDeadletterQueue =
            "[{0}] messages received from the transfer dead-letter queue of the queue [{1}].";

        private const string SessionsGotFromTheQueue = "[{0}] sessions retrieved for the queue [{1}].";
        private const string RetrieveMessagesFromQueue = "Retrieve messages from queue";
        private const string RetrieveMessagesFromDeadletterQueue = "Retrieve messages from dead-letter queue";

        private const string RetrieveMessagesFromTransferDeadletterQueue =
            "Retrieve messages from transfer dead-letter queue";

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
            "The timeout  of [{0}] seconds has expired and no message was retrieved from the dead-letter queue of the queue [{1}].";

        private const string NoMessageReceivedFromTheTransferDeadletterQueue =
            "The timeout  of [{0}] seconds has expired and no message was retrieved from the transfer dead-letter queue of the queue [{1}].";

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
            "Gets or sets the maximum delivery count. A message is automatically dead-lettered after this number of deliveries.";

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
        private const string ActiveMessageCount = Constants.ActiveMessages + " " + MessageCount;
        private const string DeadLetterCount = Constants.DeadLetterMessages + " " + MessageCount;
        private const string ScheduledMessageCount = Constants.ScheduledMessages + " " + MessageCount;
        private const string TransferMessageCount = Constants.TransferMessages + " " + MessageCount;
        private const string TransferDeadLetterMessageCount = Constants.TransferDeadLetterMessages + " " + MessageCount;
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
        private const int MaxMessageCount = 20000;

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

        private readonly IServiceBusService _serviceBusHelper;
        private QueueMetadata queueDescription = default!;
        private readonly WriteToLogDelegate writeToLog = default!;
        private readonly string path = default!;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private readonly bool premiumNamespace = false;
        private ServiceBusReceivedMessage brokeredMessage = default!;
        private ServiceBusReceivedMessage deadletterMessage = default!;
        private ServiceBusReceivedMessage transferDeadletterMessage = default!;
        private int currentMessageRowIndex = default!;
        private int currentDeadletterMessageRowIndex = default!;
        private int currentTransferDeadletterMessageRowIndex = default!;
        private bool sorting = default!;
        private string messagesFilterExpression = default!;
        private string deadletterFilterExpression = default!;
        private DateTime? messagesFilterFromDate = default!;
        private DateTime? messagesFilterToDate = default!;
        private DateTime? deadletterFilterFromDate = default!;
        private DateTime? deadletterFilterToDate = default!;
        private SortableBindingList<ServiceBusReceivedMessage> messageBindingList = default!;
        private SortableBindingList<ServiceBusReceivedMessage> deadletterBindingList = default!;
        private SortableBindingList<ServiceBusReceivedMessage> transferDeadletterBindingList = default!;
        private SortableBindingList<ServiceBusReceivedMessage> sessionBindingList = default!;
        private bool buttonsMoved;
        private readonly bool duplicateQueue;

        #endregion

        #region Private Static Fields

        private static readonly List<string> ClaimTypes = new List<string>
         {
             "NameIdentifier",
             "Upn",
             "Role",
             "SharedAccessKey"
         };


        private static List<string> QueueSettingsList = new List<string>()
         {
             EnableBatchedOperationsItemText,
             EnableDeadLetteringOnMessageExpirationItemText,
             EnablePartitioningItemText,
             EnableExpressItemText,
             RequiresDuplicateDetectionItemText,
             RequiresSessionItemText,
             SupportOrderingItemText,
             IsAnonymousAccessibleItemText
         };

        private static readonly List<string> Operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> TimeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };

        #endregion

        #region Public Constructor

        public HandleQueueControl(
            WriteToLogDelegate writeToLog, 
            IServiceBusService serviceBusHelper,
            QueueMetadata queueDescription, 
            string path,
            bool duplicateQueue)
        {
            this.writeToLog = writeToLog;
            this._serviceBusHelper = serviceBusHelper;  
            //this.path = path;
            this.queueDescription = queueDescription;

            if (!_serviceBusHelper.ConnectionStringContainsEntityPath())
            {
                this.premiumNamespace = _serviceBusHelper.IsPremiumNamespace();
            }

            this.duplicateQueue = duplicateQueue;

            InitializeComponent();
            InitializeControls(initialCall: true);
        }
        #endregion

        #region Public Events

        public event Action OnCancel = default!;
        public event Action OnRefresh = default!;
        public event Action OnChangeStatus = default!;

        #endregion

        #region Public Methods

        public async Task GetMessages()
        {
            using (
                var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromQueue, MainForm.SingletonMainForm.TopCount,
                    _serviceBusHelper.BrokeredMessageInspectors.Keys, queueDescription.RequiresSession))
            {
                if (receiveModeForm.ShowDialog() == DialogResult.OK)
                {
                    txtMessageText.Text = string.Empty;
                    messageCustomPropertyGrid.SelectedObject = null;
                    messagePropertyGrid.SelectedObject = null;
                    var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) &&
                                           _serviceBusHelper.BrokeredMessageInspectors.ContainsKey(
                                               receiveModeForm.Inspector)
                        ? Activator.CreateInstance(_serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector])
                            as IBrokeredMessageInspector
                        : null;
                    if (queueDescription.EnablePartitioning)
                    {
                        await ReadMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count,
                            messageInspector, receiveModeForm.FromSequenceNumber, receiveModeForm.FromSession);
                    }
                    else
                    {
                        await GetMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber, receiveModeForm.FromSession);
                    }
                }
            }
        }

        public async Task PurgeMessages(int numberOfMessages)
        {
            if (queueDescription.EnablePartitioning)
            {
                await ReadMessagesOneAtTheTime(false, true, numberOfMessages, null);
            }
            else
            {
                await GetMessages(false, true, numberOfMessages, null);
            }
        }

        public async Task PurgeMessagesAsync()
        {
            await this.DoPurge(PurgeStrategies.Messages, $"Would you like to purge the {queueDescription.Name} queue?");
        }


        public async Task PurgeDeadletterQueueMessagesAsync()
        {
            await this.DoPurge(PurgeStrategies.DeadletteredMessages, $"Would you like to purge the dead-letter queue of the {queueDescription.Name} queue?");
        }

        public async Task PurgeAllMessagesAsync()
        {
            await this.DoPurge(PurgeStrategies.All, $"Would you like to purge all (messages and dead-lettered messages) from the {queueDescription.Name} queue?");
        }

        private async Task DoPurge(PurgeStrategies purgeStrategy, string deleteConfirmation)
        {
            if (!DeleteForm.ShowAndWaitUserConfirmation(this, deleteConfirmation))
            {
                return;
            }

            Application.UseWaitCursor = true;

            QueueServiceBusPurger purger = new QueueServiceBusPurger(this._serviceBusHelper.Clone());
            purger.PurgeFailed += (o, e) => this.HandleException(e.Exception);
            purger.PurgeCompleted += (o, e) => writeToLog($"[{e.TotalMessagesPurged}] messages have been purged from the{(e.IsDeadLetterQueue ? " dead-letter queue of the" : "")} [{e.EntityPath}] queue in [{e.ElapsedMilliseconds / 1000}] seconds.");

            var queueToPurge = await _serviceBusHelper.GetQueueAsync(queueDescription.Name); 
            await purger.Purge(purgeStrategy, queueToPurge);

            await MainForm.SingletonMainForm.RefreshSelectedEntity();
            Application.UseWaitCursor = false;
        }

        public async Task GetDeadletterMessages()
        {
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromDeadletterQueue, MainForm.SingletonMainForm.TopCount, _serviceBusHelper.BrokeredMessageInspectors.Keys))
            {
                if (receiveModeForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtDeadletterText.Text = string.Empty;
                deadletterCustomPropertyGrid.SelectedObject = null;
                deadletterPropertyGrid.SelectedObject = null;
                var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) && _serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector)
                    ? Activator.CreateInstance(_serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector
                    : null;
                if (queueDescription.EnablePartitioning)
                {
                    await ReadDeadletterMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
                else
                {
                    await GetDeadletterMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
            }
        }

        public async Task GetTransferDeadletterMessages()
        {
            using (var receiveModeForm = new ReceiveModeForm(RetrieveMessagesFromTransferDeadletterQueue, MainForm.SingletonMainForm.TopCount, _serviceBusHelper.BrokeredMessageInspectors.Keys))
            {
                if (receiveModeForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtTransferDeadletterText.Text = string.Empty;
                transferDeadletterCustomPropertyGrid.SelectedObject = null;
                transferDeadletterPropertyGrid.SelectedObject = null;
                var messageInspector = !string.IsNullOrEmpty(receiveModeForm.Inspector) && _serviceBusHelper.BrokeredMessageInspectors.ContainsKey(receiveModeForm.Inspector)
                    ? Activator.CreateInstance(_serviceBusHelper.BrokeredMessageInspectors[receiveModeForm.Inspector]) as IBrokeredMessageInspector
                    : null;
                if (queueDescription.EnablePartitioning)
                {
                    await ReadTransferDeadletterMessagesOneAtTheTime(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
                else
                {
                    await GetTransferDeadletterMessages(receiveModeForm.Peek, receiveModeForm.All, receiveModeForm.Count, messageInspector, receiveModeForm.FromSequenceNumber);
                }
            }
        }

        public void RefreshData(QueueMetadata queue)
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

        public async Task GetMessageSessions()
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageSessions.SuspendDrawing();
                tabPageSessions.SuspendLayout();


                var sessionEnumerable = new List<ServiceBusReceivedMessage>(); 

                while (true)
                {
                    ServiceBusSessionReceiver receiver = await _serviceBusHelper.Client.AcceptNextSessionAsync(queueDescription.Name);

                    if (receiver == null)
                        break; // No more sessions available

                    await foreach (ServiceBusReceivedMessage message in receiver.ReceiveMessagesAsync())
                    {
                        sessionEnumerable.Add(message); 
                    }

                    await receiver.CloseAsync();
                }

                if (sessionEnumerable == null)
                {
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                if (mainTabControl.TabPages[SessionsTabPage] == null)
                {
                    EnablePage(SessionsTabPage);
                }

                var messageSessions = sessionEnumerable.ToArray();
                sessionBindingList = new SortableBindingList<ServiceBusReceivedMessage>(messageSessions)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = false
                };
                writeToLog(string.Format(SessionsGotFromTheQueue, sessionBindingList.Count, queueDescription.Name));
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

        #region Private Static Methods

        static bool AreAllSelectedMessageScheduled(DataGridViewSelectedRowCollection selectedRows)
        {
            //return selectedRows.Cast<DataGridViewRow>()
            //    .All(row => (row.DataBoundItem as ServiceBusMessage)?. == MessageState.Scheduled); //TODO: No counterpart redesign 
            return true; 
        }

        #endregion

        #region Private Instance Methods

        private void InitializeControls(bool initialCall)
        {
            trackBarMaxQueueSize.Maximum = _serviceBusHelper.IsCloudNamespace() ? 5 : 11;

            if (this.premiumNamespace)
            {
                trackBarMaxQueueSize.Maximum = 80;
                trackBarMaxQueueSize.TickFrequency = 10;
            }

            // This must only be done once per instance of this class
            if (initialCall)
            {
                checkedListBox.Items.Clear();

                foreach (string item in QueueSettingsList)
                {
                    switch (item)
                    {
                        // Don't add some settings for premium and Service Bus Server namespaces
                        case EnablePartitioningItemText when this.premiumNamespace:
                        case EnableExpressItemText when this.premiumNamespace:
                        case IsAnonymousAccessibleItemText when _serviceBusHelper.IsCloudNamespace():
                            break;

                        default:
                            checkedListBox.Items.Add(item);
                            break;
                    }
                }
            }

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
            authorizationRulesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            authorizationRulesDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

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
                if (_serviceBusHelper.Connection.Namespace.IsCloudNamespace)
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
                if (duplicateQueue)
                {
                    ConfigureDuplicateUserInterface();
                }
                else
                {
                    ConfigureReadUserInterface();
                }
            }
            else
            {
                //ConfigureCreateUserInterface();
            }
        }

        ///// <summary>
        ///// Configures the user interface for the ReadOnly view.
        ///// </summary>
        private void ConfigureReadUserInterface()
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
            sessionsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            sessionsDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

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
            deadletterDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            deadletterDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

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

            //checkedListBox.ItemCheck += checkedListBox_ItemCheck;

            toolTip.SetToolTip(txtPath, PathTooltip);
            toolTip.SetToolTip(txtUserMetadata, UserMetadataTooltip);
            toolTip.SetToolTip(txtForwardTo, ForwardToTooltip);
            toolTip.SetToolTip(txtForwardDeadLetteredMessagesTo, ForwardDeadLetteredMessagesToTooltip);
            toolTip.SetToolTip(trackBarMaxQueueSize, MaxQueueSizeTooltip);
            toolTip.SetToolTip(tsDefaultMessageTimeToLive, DefaultMessageTimeToLiveTooltip);
            toolTip.SetToolTip(tsDuplicateDetectionHistoryTimeWindow, DuplicateDetectionHistoryTimeWindowTooltip);
            toolTip.SetToolTip(tsLockDuration, LockDurationTooltip);
            toolTip.SetToolTip(tsAutoDeleteOnIdle, AutoDeleteOnIdleTooltip);
            toolTip.SetToolTip(txtMaxDeliveryCount, MaxDeliveryCountTooltip);
        }

        ///// <summary>
        ///// Configures the user interface for the duplicate view.
        ///// This view pre-populates the view with the data of an existing queue description.
        ///// </summary>
        private void ConfigureDuplicateUserInterface()
        {
            InitializeData();

            // clear property list view for old queue
            propertyListView.Items.Clear();

            var sizeInGb = (int)queueDescription.MaxSizeInMegaBytes * 1024; 
            // special handling for max size if partitioning is enabled
            trackBarMaxQueueSize.Maximum = _serviceBusHelper.IsCloudNamespace() ? 5 : 11;
            trackBarMaxQueueSize.Value = queueDescription.EnablePartitioning 
                ? sizeInGb / 16
                : sizeInGb;

            ConfigureCreateUserInterface();
        }

        ///// <summary>
        ///// Configures the users interface for the create view.
        ///// </summary>
        private void ConfigureCreateUserInterface()
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
            var bindingList = new BindingList<AuthorizationRuleWrapper2>(new List<AuthorizationRuleWrapper2>())
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

        private void bindingList_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                if (queueDescription != null &&
                    queueDescription.AuthorizationRules.Count > 0 &&
                    queueDescription.AuthorizationRules.Count > e.NewIndex)
                {
                    var rule = queueDescription.AuthorizationRules.ElementAt(e.NewIndex);
                    if (rule != null)
                    {
                        queueDescription.AuthorizationRules.Remove(rule);
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
            BindingList<AuthorizationRuleWrapper2> bindingList;
            if (queueDescription.AuthorizationRules.Count > 0)
            {
                var enumerable = queueDescription.AuthorizationRules.Select(r => new AuthorizationRuleWrapper2(r));
                bindingList = new BindingList<AuthorizationRuleWrapper2>(enumerable.ToList())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };

            }
            else
            {
                bindingList = new BindingList<AuthorizationRuleWrapper2>(new List<AuthorizationRuleWrapper2>())
                {
                    AllowEdit = true,
                    AllowNew = true,
                    AllowRemove = true
                };
            }
            bindingList.ListChanged += bindingList_ListChanged;
            authorizationRulesBindingSource.DataSource = new BindingList<AuthorizationRuleWrapper2>(bindingList);
            authorizationRulesDataGridView.DataSource = authorizationRulesBindingSource;

            // Initialize property grid
            var propertyList = new List<string[]>();

            var dto = queueDescription.CreatedAt; 

            propertyList.AddRange(new[]
            {
                 new[] {Status, queueDescription.Status.ToString()},
                 //new[] {IsReadOnly, queueDescription.IsReadOnly.ToString()}, //TODO: Obsolete 
                 new[] {SizeInBytes, queueDescription.SizeInMegaBytes.ToString("N0")},
                 new[] {CreatedAt, queueDescription.CreatedAt.ToLocalTime().ToString(CultureInfo.CurrentCulture)},
                 new[] {AccessedAt, queueDescription.AccessedAt.ToLocalTime().ToString(CultureInfo.CurrentCulture)},
                 new[] {UpdatedAt, queueDescription.UpdatedAt.ToLocalTime().ToString(CultureInfo.CurrentCulture)},
                 new[]
                 {
                     ActiveMessageCount,
                     queueDescription.ActiveMessageCount.ToString("N0", CultureInfo.CurrentCulture)
                 },
                 new[]
                 {
                     DeadLetterCount,
                     queueDescription.DeadLetterMessageCount.ToString("N0",
                         CultureInfo.CurrentCulture)
                 },
                 new[]
                 {
                     ScheduledMessageCount,
                     queueDescription.ScheduledMessageCount.ToString("N0", CultureInfo.CurrentCulture)
                 },
                 new[]
                 {
                     TransferMessageCount,
                     queueDescription.TransferMessageCount.ToString("N0", CultureInfo.CurrentCulture)
                 },
                 new[]
                 {
                     TransferDeadLetterMessageCount,
                     queueDescription.TransferDeadLetterMessageCount.ToString("N0",
                         CultureInfo.CurrentCulture)
                 },
                 new[] {MessageCount, queueDescription.TotalMessageCount.ToString("N0", CultureInfo.CurrentCulture)}
             });

            propertyListView.Items.Clear();
            foreach (var array in propertyList)
            {
                propertyListView.Items.Add(new ListViewItem(array));
            }

            // Path
            if (!string.IsNullOrWhiteSpace(queueDescription.Name))
            {
                txtPath.Text = queueDescription.Name;
            }

            // UserMetadata
            if (!string.IsNullOrWhiteSpace(queueDescription.UserMetadata))
            {
                txtUserMetadata.Text = queueDescription.UserMetadata;
            }

            // ForwardTo
            if (!string.IsNullOrWhiteSpace(queueDescription.ForwardTo))
            {
                //txtForwardTo.Text = _serviceBusHelper.GetAddressRelativeToNamespace(queueDescription.ForwardTo); //TODO: 
            }

            // ForwardDeadLetteredMessagesTo
            if (!string.IsNullOrWhiteSpace(queueDescription.ForwardDeadLetteredMessagesTo))
            {
                //txtForwardDeadLetteredMessagesTo.Text = serviceBusHelper.GetAddressRelativeToNamespace(queueDescription.ForwardDeadLetteredMessagesTo); //TODO: 
            }

            var maxSizeInGb = (int)queueDescription.MaxSizeInMegabytes * 1024; 

            // MaxQueueSizeInBytes
            trackBarMaxQueueSize.Value = _serviceBusHelper.IsCloudNamespace()
                ? maxSizeInGb
                : queueDescription.MaxSizeInMegabytes == SeviceBusForWindowsServerMaxQueueSize
                    ? 11 
                    : maxSizeInGb;

            // Update maximum and value if Maximum size is more than 5 Gigs (either premium or partitioned)
            if (maxSizeInGb > 5)
            {
                trackBarMaxQueueSize.Maximum = maxSizeInGb;
                trackBarMaxQueueSize.Value = maxSizeInGb;
            }

            // MaxDeliveryCount
            txtMaxDeliveryCount.Text = queueDescription.MaxDeliveryCount.ToString(CultureInfo.InvariantCulture);

            // DefaultMessageTimeToLive
            tsDefaultMessageTimeToLive.TimeSpanValue = queueDescription.DefaultMessageTimeToLive;

            // DuplicateDetectionHistoryTimeWindow
            tsDuplicateDetectionHistoryTimeWindow.TimeSpanValue = queueDescription.DuplicateDetectionHistoryTimeWindow;

            // LockDuration
            tsLockDuration.TimeSpanValue = queueDescription.LockDuration;

            // AutoDeleteOnIdle
            tsAutoDeleteOnIdle.TimeSpanValue = queueDescription.AutoDeleteOnIdle;

            // EnableBatchedOperations
            checkedListBox.SetItemChecked(EnableBatchedOperationsItemText,
                queueDescription.EnableBatchedOperations);

            // EnableDeadLetteringOnMessageExpiration
            //checkedListBox.SetItemChecked(EnableDeadLetteringOnMessageExpirationItemText,
            //    queueDescription.EnableDeadLetteringOnMessageExpiration); //TODO: Queues automatically expire messages


            if (_serviceBusHelper.IsCloudNamespace() && !this.premiumNamespace)
            {
                // EnablePartitioning
                checkedListBox.SetItemChecked(EnablePartitioningItemText, queueDescription.EnablePartitioning);

                // EnableExpress
                //checkedListBox.SetItemChecked(EnableExpressItemText, queueDescription.EnableExpress); //TODO: Express Obsolete 
            }

            // RequiresDuplicateDetection
            checkedListBox.SetItemChecked(RequiresDuplicateDetectionItemText,
                queueDescription.RequiresDuplicateDetection);

            // RequiresSession
            checkedListBox.SetItemChecked(RequiresSessionItemText,
                queueDescription.RequiresSession);

            // SupportOrdering
            //checkedListBox.SetItemChecked(SupportOrderingItemText,
            //    queueDescription.SupportOrdering); //TODO: Always enabled 

            // IsAnonymousAccessible
            //if (!_serviceBusHelper.IsCloudNamespace())
            //{
            //    checkedListBox.SetItemChecked(IsAnonymousAccessibleItemText,
            //        queueDescription.IsAnonymousAccessible); //TODO: Obsolete
            //}
        }

        private async Task<ServiceBusReceiver> BuildMessageReceiverAsync(ServiceBusReceiveMode receiveMode, string? fromSession = null)
        {
            if (!queueDescription.RequiresSession)
            {
                return _serviceBusHelper.Client.CreateReceiver(queueDescription.Name, new ServiceBusReceiverOptions { ReceiveMode = receiveMode });
            }

            return await _serviceBusHelper.Client.AcceptNextSessionAsync(queueDescription.Name, new ServiceBusSessionReceiverOptions { ReceiveMode = receiveMode });

            //return await _serviceBusHelper.Client.AcceptSessionAsync(
            //    queueDescription.Name, 
            //    sessionId: fromSession,
            //    new ServiceBusSessionReceiverOptions { ReceiveMode = receiveMode }); 
        }

        private async Task GetMessages(bool peek, bool all, int count, IBrokeredMessageInspector? messageInspector, long? fromSequenceNumber = null, string? fromSession = null)
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageMessages.SuspendDrawing();
                tabPageMessages.SuspendLayout();

                Cursor.Current = Cursors.WaitCursor;
                var brokeredMessages = new List<ServiceBusReceivedMessage>();
                if (peek)
                {
                    var totalRetrieved = 0;

                    var receiver = await BuildMessageReceiverAsync(ServiceBusReceiveMode.PeekLock, fromSession);
                    while (totalRetrieved < count)
                    {
                        IReadOnlyList<ServiceBusReceivedMessage> messageEnumerable;

                        if (totalRetrieved == 0 && fromSequenceNumber.HasValue)
                        {
                            //messageEnumerable = await receiver.PeekMessagesAsync(count, fromSequenceNumber.Value); //TODO: What is count here? 
                            messageEnumerable = await receiver.PeekMessagesAsync(MaxMessageCount, fromSequenceNumber.Value);
                        }
                        else
                        {
                            messageEnumerable = await receiver.PeekMessagesAsync(count);
                        }

                        if (messageEnumerable == null)
                        {
                            break;
                        }
                        var messageArray = messageEnumerable as ServiceBusReceivedMessage[] ?? messageEnumerable.ToArray();
                        var partialList = messageInspector != null
                            ? messageArray.Select(b => messageInspector.AfterReceiveMessage(b)).ToList()
                            : new List<ServiceBusReceivedMessage>(messageArray);
                        brokeredMessages.AddRange(partialList);
                        totalRetrieved += partialList.Count;
                        if (partialList.Count == 0)
                        {
                            break;
                        }
                    }
                    writeToLog(string.Format(MessagesPeekedFromTheQueue, brokeredMessages.Count, queueDescription.Name));
                }
                else
                {
                    var messageReceiver = await BuildMessageReceiverAsync(ServiceBusReceiveMode.ReceiveAndDelete, fromSession);

                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = await messageReceiver.ReceiveMessagesAsync(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved,
                            TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        var enumerable = messages.ToList(); 
                        retrieved = enumerable.Count;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        brokeredMessages.AddRange(messageInspector != null
                            ? enumerable.Select(b => messageInspector.AfterReceiveMessage(b))
                            : enumerable);
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesReceivedFromTheQueue, brokeredMessages.Count, queueDescription.Name));
                }
                messageBindingList = new SortableBindingList<ServiceBusReceivedMessage>(brokeredMessages)
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
                    queueDescription.Name));
            }
            catch (NotSupportedException)
            {
                await ReadMessagesOneAtTheTime(peek, all, count, messageInspector, fromSequenceNumber);
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

        private async Task ReadMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector? messageInspector, long? fromSequenceNumber = null, string? fromSession = null)
        {
            try
            {
                var brokeredMessages = new List<ServiceBusReceivedMessage>();
                if (peek)
                {   
                    var messageReceiver = await BuildMessageReceiverAsync(ServiceBusReceiveMode.PeekLock, fromSession);

                    for (var i = 0; i < count; i++)
                    {
                        ServiceBusReceivedMessage message;

                        if (i == 0 && fromSequenceNumber.HasValue)
                        {
                            message = await messageReceiver.PeekMessageAsync(fromSequenceNumber.Value);
                        }
                        else
                        {
                            message = await messageReceiver.PeekMessageAsync();
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
                    writeToLog(string.Format(MessagesPeekedFromTheQueue, brokeredMessages.Count, queueDescription.Name));
                }
                else
                {
                    var messageReceiver = await BuildMessageReceiverAsync(ServiceBusReceiveMode.ReceiveAndDelete, fromSession);

                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var message =
                            await messageReceiver.ReceiveMessageAsync(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        retrieved = message != null ? 1 : 0;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        if (message != null)
                        {
                            brokeredMessages.Add(messageInspector != null
                                ? messageInspector.AfterReceiveMessage(message)
                                : message);
                        }
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesReceivedFromTheQueue, brokeredMessages.Count, queueDescription.Name));
                }
                messageBindingList = new SortableBindingList<ServiceBusReceivedMessage>(brokeredMessages)
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
                    queueDescription.Name));
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private async Task GetDeadletterMessages(bool peek, bool all, int count, IBrokeredMessageInspector? messageInspector, long? fromSequenceNumber)
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageDeadletter.SuspendDrawing();
                tabPageDeadletter.SuspendLayout();

                Cursor.Current = Cursors.WaitCursor;
                var brokeredMessages = new List<ServiceBusReceivedMessage>();

                //var queuePath = QueueClient.FormatDeadLetterPath(queueDescription.Name);
                if (peek)
                {
                    var reciever = await BuildMessageReceiverAsync(ServiceBusReceiveMode.PeekLock);

                    //var queueClient = _serviceBusHelper.CreateQueueClient(queuePath, ReceiveMode.PeekLock);
                    var totalRetrieved = 0;
                    var retrieved = 0;
                    do
                    {
                        IEnumerable<ServiceBusReceivedMessage> messages;

                        if (retrieved == 0 && fromSequenceNumber.HasValue)
                        {
                            messages = await reciever.PeekMessagesAsync((int)fromSequenceNumber.Value, all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        }
                        else
                        {
                            messages = await reciever.PeekMessagesAsync(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        }

                        var enumerable = messages as ServiceBusReceivedMessage[] ?? messages.ToArray();
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
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Name));
                }
                else
                {
                    var reciever = await BuildMessageReceiverAsync(ServiceBusReceiveMode.ReceiveAndDelete);

                    //var queueClient = _serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = await reciever.PeekMessagesAsync(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        var enumerable = messages as ServiceBusReceivedMessage[] ?? messages.ToArray();
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
                        foreach (var message in brokeredMessages)
                            await reciever.CompleteMessageAsync(message);
                    //}
                    //else
                    //{
                    //    foreach (var partitionKey in brokeredMessages.Select(bm => bm.PartitionKey).Distinct()) TODO: 
                    //    {
                    //        var key = partitionKey;
                    //        await reciever.CompleteMessageAsync(
                    //            brokeredMessages.Where(bm => bm.PartitionKey == key).Select(bm => bm.LockToken));
                    //    }
                    //}
                    writeToLog(string.Format(MessagesReceivedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Name));
                }

                deadletterBindingList = new SortableBindingList<ServiceBusReceivedMessage>(brokeredMessages.Select(m => m))
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
                writeToLog(string.Format(NoMessageReceivedFromTheDeadletterQueue, MainForm.SingletonMainForm.ReceiveTimeout, queueDescription.Name));
            }
            catch (NotSupportedException)
            {
                await ReadDeadletterMessagesOneAtTheTime(peek, all, count, messageInspector, fromSequenceNumber);
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

        private async Task GetTransferDeadletterMessages(bool peek, bool all, int count, IBrokeredMessageInspector? messageInspector, long? fromSequenceNumber)
        {
            try
            {
                mainTabControl.SuspendDrawing();
                mainTabControl.SuspendLayout();
                tabPageTransferDeadletter.SuspendDrawing();
                tabPageTransferDeadletter.SuspendLayout();

                Cursor.Current = Cursors.WaitCursor;
                var brokeredMessages = new List<ServiceBusReceivedMessage>();

                //var queuePath = QueueClient.FormatTransferDeadLetterPath(queueDescription.Name);
                if (peek)
                {
                    var reciever = await BuildMessageReceiverAsync(ServiceBusReceiveMode.PeekLock);
                    //var queueClient = _serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.PeekLock);
                    var totalRetrieved = 0;
                    var retrieved = 0;
                    do
                    {
                        IEnumerable<ServiceBusReceivedMessage> messages;

                        if (retrieved == 0 && fromSequenceNumber.HasValue)
                        {
                            messages = await reciever.PeekMessagesAsync((int)fromSequenceNumber.Value, all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        }
                        else
                        {
                            messages = await reciever.PeekMessagesAsync(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        }

                        var enumerable = messages as ServiceBusReceivedMessage[] ?? messages.ToArray();
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
                    writeToLog(string.Format(MessagesPeekedFromTheTransferDeadletterQueue, brokeredMessages.Count, queueDescription.Name));
                }
                else
                {
                    var reciever = await BuildMessageReceiverAsync(ServiceBusReceiveMode.ReceiveAndDelete);

                    //var queueClient = _serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var messages = await reciever.PeekMessagesAsync(all
                                ? MainForm.SingletonMainForm.TopCount
                                : count - totalRetrieved);
                        var enumerable = messages as ServiceBusReceivedMessage[] ?? messages.ToArray();
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
                    foreach (var message in brokeredMessages) 
                        await reciever.CompleteMessageAsync(message);
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
                    writeToLog(string.Format(MessagesReceivedFromTheTransferDeadletterQueue, brokeredMessages.Count, queueDescription.Name));
                }

                transferDeadletterBindingList = new SortableBindingList<ServiceBusReceivedMessage>(brokeredMessages)
                {
                    AllowEdit = false,
                    AllowNew = false,
                    AllowRemove = true
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
                writeToLog(string.Format(NoMessageReceivedFromTheTransferDeadletterQueue, MainForm.SingletonMainForm.ReceiveTimeout, queueDescription.Name));
            }
            catch (NotSupportedException)
            {
                await ReadTransferDeadletterMessagesOneAtTheTime(peek, all, count, messageInspector, fromSequenceNumber);
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

        private async Task ReadDeadletterMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector? messageInspector, long? fromSequenceNumber)
        {
            try
            {
                var brokeredMessages = new List<ServiceBusReceivedMessage>();
                //var queuePath = QueueClient.FormatDeadLetterPath(queueDescription.Name);

                if (peek)
                {
                    var reciever = await BuildMessageReceiverAsync(ServiceBusReceiveMode.PeekLock);

                    //var queueClient = _serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.PeekLock);
                    for (var i = 0; i < count; i++)
                    {
                        ServiceBusReceivedMessage message;

                        if (i == 0 && fromSequenceNumber.HasValue)
                        {
                            message = await reciever.PeekMessageAsync((int)fromSequenceNumber.Value);
                        }
                        else
                        {
                            message = await reciever.PeekMessageAsync();
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
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Name));
                }
                else
                {
                    var reciever = await BuildMessageReceiverAsync(ServiceBusReceiveMode.ReceiveAndDelete);

                    //var queueClient = _serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var message = reciever.ReceiveMessageAsync(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout)).GetAwaiter().GetResult();
                        retrieved = message != null ? 1 : 0;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        if (message != null)
                        {
                            brokeredMessages.Add(messageInspector != null
                            ? messageInspector.AfterReceiveMessage(message)
                            : message);
                        }
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesPeekedFromTheDeadletterQueue, brokeredMessages.Count, queueDescription.Name));
                }
                deadletterBindingList = new SortableBindingList<ServiceBusReceivedMessage>(brokeredMessages)
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
                writeToLog(string.Format(NoMessageReceivedFromTheDeadletterQueue, MainForm.SingletonMainForm.ReceiveTimeout, queueDescription.Name));
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private async Task ReadTransferDeadletterMessagesOneAtTheTime(bool peek, bool all, int count, IBrokeredMessageInspector? messageInspector, long? fromSequenceNumber)
        {
            try
            {
                var brokeredMessages = new List<ServiceBusReceivedMessage>();
                //var queuePath = QueueClient.FormatTransferDeadLetterPath(queueDescription.Name);

                if (peek)
                {
                    var reciever = await BuildMessageReceiverAsync(ServiceBusReceiveMode.PeekLock);

                    //var queueClient = _serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.PeekLock);
                    for (var i = 0; i < count; i++)
                    {
                        ServiceBusReceivedMessage message;

                        if (i == 0 && fromSequenceNumber.HasValue)
                        {
                            message = await reciever.PeekMessageAsync(fromSequenceNumber.Value);
                        }
                        else
                        {
                            message = await reciever.PeekMessageAsync();
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
                    writeToLog(string.Format(MessagesPeekedFromTheTransferDeadletterQueue, brokeredMessages.Count, queueDescription.Name));
                }
                else
                {
                    var reciever = await BuildMessageReceiverAsync(ServiceBusReceiveMode.ReceiveAndDelete);

                    //var queueClient = _serviceBusHelper.MessagingFactory.CreateQueueClient(queuePath, ReceiveMode.ReceiveAndDelete);
                    var totalRetrieved = 0;
                    int retrieved;
                    do
                    {
                        var message = await reciever.ReceiveMessageAsync(TimeSpan.FromSeconds(MainForm.SingletonMainForm.ReceiveTimeout));
                        retrieved = message != null ? 1 : 0;
                        if (retrieved == 0)
                        {
                            continue;
                        }
                        totalRetrieved += retrieved;
                        if (message != null)
                        {
                            brokeredMessages.Add(messageInspector != null
                                ? messageInspector.AfterReceiveMessage(message)
                                : message);
                        }
                    } while (retrieved > 0 && (all || count > totalRetrieved));
                    writeToLog(string.Format(MessagesPeekedFromTheTransferDeadletterQueue, brokeredMessages.Count, queueDescription.Name));
                }
                transferDeadletterBindingList = new SortableBindingList<ServiceBusReceivedMessage>(brokeredMessages)
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
                writeToLog(string.Format(NoMessageReceivedFromTheTransferDeadletterQueue, MainForm.SingletonMainForm.ReceiveTimeout, queueDescription.Name));
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void btnCreateDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serviceBusHelper == null)
                {
                    return;
                }
                if (btnCreateDelete.Text == DeleteText)
                {
                    using (var deleteForm = new DeleteForm(queueDescription.Name, QueueEntity.ToLower()))
                    {
                        var configuration = TwoFilesConfiguration.Create(TwoFilesConfiguration.GetCurrentConfigFileUse(), writeToLog);

                        bool disableAccidentalDeletionPrevention = configuration.GetBoolValue(
                                                               ConfigurationParameters.DisableAccidentalDeletionPrevention,
                                                               defaultValue: false);

                        if (!disableAccidentalDeletionPrevention)
                        {
                            deleteForm.ShowAccidentalDeletionPreventionCheck(configuration, $"Delete {queueDescription.Name} {QueueEntity.ToLower()}");
                        }

                        if (deleteForm.ShowDialog() == DialogResult.OK)
                        {
                            _serviceBusHelper.DeleteQueueAsync(queueDescription.Name).GetAwaiter().GetResult();
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

                    var description = new QueueMetadata()
                    {
                        Name = txtPath.Text,
                        UserMetadata = txtUserMetadata.Text,
                        ForwardTo = txtForwardTo.Text,
                        ForwardDeadLetteredMessagesTo = txtForwardDeadLetteredMessagesTo.Text,
                        MaxSizeInMegabytes = _serviceBusHelper.IsCloudNamespace()
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

                    if (tsDefaultMessageTimeToLive.IsFilled)
                    {
                        if (tsDefaultMessageTimeToLive.TimeSpanValue.HasValue)
                        {
                            description.DefaultMessageTimeToLive = tsDefaultMessageTimeToLive.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsDefaultMessageTimeToLive.GetErrorMessage(DefaultMessageTimeToLive));
                            return;
                        }
                    }

                    if (tsDuplicateDetectionHistoryTimeWindow.IsFilled)
                    {
                        if (tsDuplicateDetectionHistoryTimeWindow.TimeSpanValue.HasValue)
                        {
                            description.DuplicateDetectionHistoryTimeWindow = tsDuplicateDetectionHistoryTimeWindow.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsDuplicateDetectionHistoryTimeWindow.GetErrorMessage(DuplicateDetectionHistoryTimeWindow));
                            return;
                        }
                    }

                    if (tsAutoDeleteOnIdle.IsFilled)
                    {
                        if (tsAutoDeleteOnIdle.TimeSpanValue.HasValue)
                        {
                            description.AutoDeleteOnIdle = tsAutoDeleteOnIdle.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsAutoDeleteOnIdle.GetErrorMessage(AutoDeleteOnIdle));
                            return;
                        }
                    }

                    if (tsLockDuration.IsFilled)
                    {
                        if (tsLockDuration.TimeSpanValue.HasValue)
                        {
                            description.LockDuration = tsLockDuration.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsLockDuration.GetErrorMessage(LockDuration));
                            return;
                        }
                    }

                    description.EnableBatchedOperations = checkedListBox.GetItemChecked(EnableBatchedOperationsItemText);
                    //description.EnableDeadLetteringOnMessageExpiration =
                    //    checkedListBox.GetItemChecked(EnableDeadLetteringOnMessageExpirationItemText);

                    // The following two items are not shown on Premium namespaces
                    description.EnablePartitioning = checkedListBox.GetItemChecked(EnablePartitioningItemText, defaultValue: false);
                    //description.EnableExpress = checkedListBox.GetItemChecked(EnableExpressItemText, defaultValue: false);

                    description.RequiresDuplicateDetection =
                        checkedListBox.GetItemChecked(RequiresDuplicateDetectionItemText);
                    description.RequiresSession = checkedListBox.GetItemChecked(RequiresSessionItemText);
                    //description.SupportOrdering = checkedListBox.GetItemChecked(SupportOrderingItemText);

                    // The following item is only shown on Service Bus for Windows Server namespaces
                    //description.IsAnonymousAccessible = checkedListBox.GetItemChecked(IsAnonymousAccessibleItemText, defaultValue: false);

                    var bindingList =
                        authorizationRulesBindingSource.DataSource as BindingList<AuthorizationRuleWrapper2>;
                    if (bindingList != null)
                    {
                        for (var i = 0; i < bindingList.Count; i++)
                        {
                            var rule = bindingList[i];
                            if (_serviceBusHelper.IsCloudNamespace())
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
                            if (_serviceBusHelper.IsCloudNamespace())
                            {
                                if (string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    description.AuthorizationRules.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rule.PrimaryKey ?? GenerateRandomKey(),
                                        rightList));
                                }
                                else
                                {
                                    description.AuthorizationRules.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rule.PrimaryKey ?? GenerateRandomKey(),
                                        rule.SecondaryKey ?? GenerateRandomKey(),
                                        rightList));
                                }
                            }
                            else
                            {
                                //description.Authorization.Add(new AllowRule(rule.IssuerName, TODO: 
                                //    rule.ClaimType,
                                //    rule.ClaimValue,
                                //    rightList));
                            }
                        }
                    }


                    queueDescription = _serviceBusHelper.CreateQueueAsync(description).GetAwaiter().GetResult();
                    InitializeControls(initialCall: false);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public static string GenerateRandomKey()
        {
            var keyBytes = new byte[32]; // 256 bits
            RandomNumberGenerator.Fill(keyBytes);
            return Convert.ToBase64String(keyBytes);
        }

        private void HandleException(Exception? ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }
            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex?.Message));
            if (!string.IsNullOrWhiteSpace(ex?.InnerException?.Message))
            {
                writeToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex?.InnerException?.Message));
            }
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (queueDescription == null)
            {
                return;
            }
            if (e.Index == checkedListBox.Items.IndexOf(EnablePartitioningItemText))
            {
                e.NewValue = queueDescription.EnablePartitioning ? CheckState.Checked : CheckState.Unchecked;
            }
            if (e.Index == checkedListBox.Items.IndexOf(RequiresSessionItemText))
            {
                e.NewValue = queueDescription.RequiresSession ? CheckState.Checked : CheckState.Unchecked;
            }
            if (e.Index == checkedListBox.Items.IndexOf(RequiresDuplicateDetectionItemText))
            {
                e.NewValue = queueDescription.RequiresDuplicateDetection ? CheckState.Checked : CheckState.Unchecked;
            }
            //if (e.Index == checkedListBox.Items.IndexOf(IsAnonymousAccessibleItemText))
            //{
            //    e.NewValue = queueDescription.IsAnonymousAccessible ? CheckState.Checked : CheckState.Unchecked; //TODO: Anon is obsolete
            //}
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

                    if (tsDefaultMessageTimeToLive.IsFilled)
                    {
                        if (tsDefaultMessageTimeToLive.TimeSpanValue.HasValue)
                        {
                            queueDescription.DefaultMessageTimeToLive = tsDefaultMessageTimeToLive.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsDefaultMessageTimeToLive.GetErrorMessage(DefaultMessageTimeToLive));
                            return;
                        }
                    }

                    if (tsDuplicateDetectionHistoryTimeWindow.IsFilled)
                    {
                        if (tsDuplicateDetectionHistoryTimeWindow.TimeSpanValue.HasValue)
                        {
                            queueDescription.DuplicateDetectionHistoryTimeWindow = tsDuplicateDetectionHistoryTimeWindow.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsDuplicateDetectionHistoryTimeWindow.GetErrorMessage(DuplicateDetectionHistoryTimeWindow));
                            return;
                        }
                    }

                    if (tsAutoDeleteOnIdle.IsFilled)
                    {
                        if (tsAutoDeleteOnIdle.TimeSpanValue.HasValue)
                        {
                            queueDescription.AutoDeleteOnIdle = tsAutoDeleteOnIdle.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsAutoDeleteOnIdle.GetErrorMessage(AutoDeleteOnIdle));
                            return;
                        }
                    }

                    if (tsLockDuration.IsFilled)
                    {
                        if (tsLockDuration.TimeSpanValue.HasValue)
                        {
                            queueDescription.LockDuration = tsLockDuration.TimeSpanValue.Value;
                        }
                        else
                        {
                            writeToLog(tsLockDuration.GetErrorMessage(LockDuration));
                            return;
                        }
                    }

                    queueDescription.EnableBatchedOperations =
                        checkedListBox.GetItemChecked(EnableBatchedOperationsItemText);
                    //queueDescription.EnableExpress = checkedListBox.GetItemChecked(EnableExpressItemText, defaultValue: false); TODO: 
                    //queueDescription.EnableDeadLetteringOnMessageExpiration =
                    //    checkedListBox.GetItemChecked(EnableDeadLetteringOnMessageExpirationItemText);
                    //queueDescription.SupportOrdering = checkedListBox.GetItemChecked(SupportOrderingItemText);

                    //queueDescription.IsAnonymousAccessible =
                    //    checkedListBox.GetItemChecked(IsAnonymousAccessibleItemText, defaultValue: false);

                    var bindingList =
                        authorizationRulesBindingSource.DataSource as BindingList<AuthorizationRuleWrapper2>;
                    if (bindingList != null)
                    {
                        for (var i = 0; i < bindingList.Count; i++)
                        {
                            var rule = bindingList[i];

                            if (_serviceBusHelper.IsCloudNamespace())
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
                            if (_serviceBusHelper.IsCloudNamespace())
                            {
                                if (string.IsNullOrWhiteSpace(rule.PrimaryKey) &&
                                    string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    queueDescription.AuthorizationRules.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rightList));
                                }
                                else if (string.IsNullOrWhiteSpace(rule.SecondaryKey))
                                {
                                    queueDescription.AuthorizationRules.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        GenerateRandomKey(),
                                        rightList));
                                }
                                else
                                {
                                    queueDescription.AuthorizationRules.Add(new SharedAccessAuthorizationRule(rule.KeyName,
                                        rule.PrimaryKey ?? GenerateRandomKey(),
                                        rule.SecondaryKey ?? GenerateRandomKey(),
                                        rightList));
                                }
                            }
                            else
                            {
                                //queueDescription.AuthorizationRules.Add(new AllowRule(rule.IssuerName, TODO: 
                                //    rule.ClaimType,
                                //    rule.ClaimValue,
                                //    rightList));
                            }
                        }
                    }

                    queueDescription.Status = EntityStatus.Disabled;
                    _serviceBusHelper.UpdateQueueAsync(queueDescription).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    queueDescription = _serviceBusHelper.GetQueueAsync(queueDescription.Name).GetAwaiter().GetResult();
                }
                finally
                {
                    queueDescription.Status = EntityStatus.Active;
                    queueDescription = _serviceBusHelper.UpdateQueueAsync(queueDescription).GetAwaiter().GetResult();
                    InitializeData();
                }
            }
        }

        private static void textBox_GotFocus(object? sender, EventArgs e)
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
            using (var form = creteSelectEntityFormForPath(txtForwardTo.Text).GetAwaiter().GetResult())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtForwardTo.Text = form.Path;
                }
            }
        }

        private void btnOpenForwardDeadLetteredMessagesToForm_Click(object sender, EventArgs e)
        {
            using (var form = creteSelectEntityFormForPath(txtForwardDeadLetteredMessagesTo.Text).GetAwaiter().GetResult())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtForwardDeadLetteredMessagesTo.Text = form.Path;
                }
            }
        }

        private async Task<SelectEntityForm> creteSelectEntityFormForPath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                QueueMetadata queueDescriptionSource = default!;
                try
                {
                    queueDescriptionSource = await _serviceBusHelper.GetQueueAsync(path);
                }
                catch (Exception)
                {
                    // we might have found a topic, and the sdk will throw with an indistinguishable MessagingException error.
                }
                if (queueDescriptionSource != null)
                {
                    return new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText, queueDescriptionSource);
                }
                //TopicDescription topicDescriptionSource = default!; TODO: 
                //try
                //{
                //    topicDescriptionSource = _serviceBusHelper.GetTopic(path);
                //}
                //catch (Exception)
                //{
                //    // we might have found a queue, and the sdk will throw with an indistinguishable MessagingException error.
                //}
                //if (topicDescriptionSource != null)
                //{
                //    return new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText, topicDescriptionSource);
                //}
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

            if (e != null && e?.Font != null)
            {
                var @event = e; 
                var roundedFontSize = (float)Math.Round(@event.Font.SizeInPoints);
                var bounds = new RectangleF(@event.Bounds.X + 4, (@event.Bounds.Height - 8 - roundedFontSize) / 2, @event.Bounds.Width,
                    roundedFontSize + 6);
                e.Graphics.DrawString(e?.Header?.Text, @event.Font, new SolidBrush(SystemColors.ControlText), bounds);
            }
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
            if (!_serviceBusHelper.IsCloudNamespace() &&
                trackBarMaxQueueSize.Value == trackBarMaxQueueSize.Maximum)
            {
                lblMaxQueueSizeInGB.Text = MaxGigabytes;
            }
        }

        private void mainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mainTabControl, e, null);
        }

        private void DrawTabControlTabs(TabControl tabControl, DrawItemEventArgs e, ImageList? images)
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
            if (tabControl?.Parent?.BackgroundImage != null)
            {
                var src = new RectangleF(tabstripEndRectF.X + tabControl.Left, tabstripEndRectF.Y + tabControl.Top,
                    tabstripEndRectF.Width, tabstripEndRectF.Height);
                e.Graphics.DrawImage(tabControl.Parent.BackgroundImage, tabstripEndRectF, src, GraphicsUnit.Pixel);
            }
            // If we have no image, use the background color.
            else if (tabControl?.Parent?.BackColor != null)
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
            var page = tabControl?.TabPages[e.Index];
            if (page != null)
            {
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
                            Image icon = default!;
                            if (page.ImageIndex > -1)
                                icon = images.Images[page.ImageIndex];
                            else if (page.ImageKey != "")
                            {
                                var fromImages = images?.Images[page.ImageKey];
                                if (fromImages != null)
                                    icon = fromImages;
                            }

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
                            var family = e?.Font?.FontFamily;
                            var style = e?.Font?.Style;
                            if (family != null && style != null)
                                e?.Graphics.DrawString(tabName, new Font(family, 8.25F, (FontStyle)style), foreBrush,
                                    labelRect, sf);
                        }
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

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView ?? throw new ArgumentNullException(nameof(sender));
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Far,

                LineAlignment = StringAlignment.Center
            };

            Size textSize = TextRenderer.MeasureText(rowIdx, this.Font);

            if (grid.RowHeadersWidth < textSize.Width + 20)
            {
                grid.RowHeadersWidth = textSize.Width + 20;
            }

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth - 4, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var textBox = (TextBox)e.Control;
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Both;
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
            GetMessages().GetAwaiter().GetResult();
        }

        private void btnSessions_Click(object sender, EventArgs e)
        {
            GetMessageSessions().GetAwaiter().GetResult();
        }

        private void messagesDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var bindingList = sessionsBindingSource.DataSource as BindingList<ServiceBusSessionReceiver>;
                if (bindingList == null)
                {
                    return;
                }

                var sessionReceiver = bindingList[e.RowIndex];
                sessionPropertyGrid.SelectedObject = sessionReceiver;

                var state = sessionReceiver.GetSessionStateAsync().GetAwaiter().GetResult();
                if (state == null)
                {
                    txtSessionState.Text = string.Empty;
                    return;
                }

                // Assuming the state is UTF-8 encoded
                var sessionStateText = Encoding.UTF8.GetString(state);
                txtSessionState.Text = sessionStateText;
                messageCustomPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter<string, object>(brokeredMessage.ApplicationProperties.ToDictionary());
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
            var bindingList = sessionsBindingSource.DataSource as BindingList<ServiceBusSessionReceiver>;
            if (bindingList == null)
            {
                return;
            }
            var sessionReceiver = bindingList[e.RowIndex];
            sessionPropertyGrid.SelectedObject = sessionReceiver;
            var state = sessionReceiver.GetSessionStateAsync().GetAwaiter().GetResult();
            if (state == null)
            {
                txtSessionState.Text = string.Empty;
                return;
            }
            var stream = state.ToStream();
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

        private async void btnDeadletter_Click(object sender, EventArgs e)
        {
            await GetDeadletterMessages();
        }

        private void btnTransferDlq_Click(object sender, EventArgs e)
        {
            GetTransferDeadletterMessages().GetAwaiter().GetResult();
        }

        private void deadletterDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = deadletterBindingSource.DataSource as BindingList<ServiceBusReceivedMessage>;
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

            LanguageDetector.SetFormattedMessage(_serviceBusHelper, deadletterMessage, txtDeadletterText);

            deadletterCustomPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter<string, object>(deadletterMessage.ApplicationProperties.ToDictionary());
        }

        private void transferDeadletterDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = transferDeadletterBindingSource.DataSource as BindingList<ServiceBusReceivedMessage>;
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

            LanguageDetector.SetFormattedMessage(_serviceBusHelper, transferDeadletterMessage, txtTransferDeadletterText);

            transferDeadletterCustomPropertyGrid.SelectedObject = new DictionaryPropertyGridAdapter<string, object>(deadletterMessage.ApplicationProperties.ToDictionary());
        }

        private void authorizationRulesDataGridView_Resize(object sender, EventArgs? e)
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
                if (_serviceBusHelper.IsCloudNamespace())
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
            if (!_serviceBusHelper.IsCloudNamespace() &&
                e.RowIndex == authorizationRulesDataGridView.Rows.Count - 1 &&
                string.IsNullOrWhiteSpace(
                    authorizationRulesDataGridView.Rows[e.RowIndex].Cells["IssuerName"].Value as string))
            {
                authorizationRulesDataGridView.Rows[e.RowIndex].Cells["IssuerName"].Value = _serviceBusHelper.Connection.Namespace;
            }
        }

        private void messagesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var bindingList = messagesBindingSource.DataSource as BindingList<ServiceBusReceivedMessage>;
            if (bindingList == null)
            {
                return;
            }
            using (var messageForm = new MessageForm(queueDescription,
                MessageForm.QueueType.PrimaryQueue, bindingList[e.RowIndex], _serviceBusHelper, writeToLog))
            {
                messageForm.ShowDialog();
            }
        }

        private void deadletterDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RepairAndResubmitSharedDeadletterMessage(e);
        }

        private void transferDeadletterDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RepairAndResubmitSharedDeadletterMessage(e);
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
            messageCustomPropertyGrid.Size = new Size(grouperMessageCustomProperties.Size.Width - messageCustomPropertyGrid.Location.X * 2,
                                                    grouperMessageCustomProperties.Size.Height - messageCustomPropertyGrid.Location.Y -
                                                    messageCustomPropertyGrid.Location.X);
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
            deadletterCustomPropertyGrid.Size =
                new Size(grouperDeadletterCustomProperties.Size.Width - deadletterCustomPropertyGrid.Location.X * 2,
                    grouperDeadletterCustomProperties.Size.Height - deadletterCustomPropertyGrid.Location.Y -
                    deadletterCustomPropertyGrid.Location.X);
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
            transferDeadletterCustomPropertyGrid.Size =
                new Size(grouperTransferDeadletterCustomProperties.Size.Width - transferDeadletterCustomPropertyGrid.Location.X * 2,
                    grouperTransferDeadletterCustomProperties.Size.Height - transferDeadletterCustomPropertyGrid.Location.Y -
                    transferDeadletterCustomPropertyGrid.Location.X);
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
            resubmitMessageToolStripMenuItem.Visible = !multipleSelectedRows;

            if (AreAllSelectedMessageScheduled(messagesDataGridView.SelectedRows))
            {
                SetCancelScheduledMessageToolStripMenuItemText(multipleSelectedRows);
                cancelScheduledMessageToolStripMenuItem.Visible = true;
            }
            else
            {
                cancelScheduledMessageToolStripMenuItem.Visible = false;
            }

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

        private void resubmitMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResubmitSelectedMessages();
        }

        private void resubmitSelectedMessagesInBatchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResubmitSelectedMessages();
        }

        async void cancelScheduledMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (messagesDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var configuration = TwoFilesConfiguration.Create(TwoFilesConfiguration.GetCurrentConfigFileUse(), writeToLog);
            bool disableAccidentalDeletionPrevention = configuration.GetBoolValue(
                                    ConfigurationParameters.DisableAccidentalDeletionPrevention,
                                    defaultValue: false);

            var thisForm = FindForm();

            if (thisForm == null)
                throw new ArgumentNullException(nameof(sender), $"{nameof(thisForm)} is null");

            if (!disableAccidentalDeletionPrevention)
            {
                if (MessageBox.Show(owner: thisForm,
                    text: "Are you sure you want to cancel the scheduled message(s)\n\n" +
                    "They will be permanently removed.\n\n" +
                    "You can disable this check by changing the Disable Accidental Deletion Prevention setting.",
                    caption: "Cancel Scheduled Message(s)",
                    buttons: MessageBoxButtons.YesNo,
                    icon: MessageBoxIcon.Warning,
                    defaultButton: MessageBoxDefaultButton.Button2)
                    == DialogResult.No)
                {
                    return;
                }
            }


            IEnumerable<ServiceBusReceivedMessage> messages = messagesDataGridView.SelectedRows.Cast<DataGridViewRow>()
                .Select(r => (ServiceBusReceivedMessage)r.DataBoundItem).Where(m => m != null);

            List<long> sequenceNumbersToCancel = messages.Select(s => s.SequenceNumber).ToList();


            try
            {
                thisForm.UseWaitCursor = true;

                var serviceBusHelperClone = _serviceBusHelper.Clone();

                await CancelScheduledMessagesHelper.CancelScheduledMessages(
                    _serviceBusHelper, this.queueDescription.Name, sequenceNumbersToCancel);
            }
            finally
            {
                thisForm.UseWaitCursor = false;
            }
        }

        private void ResubmitSelectedMessages()
        {
            if (messagesDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            using (var form = new MessageForm(
                queueDescription,
                MessageForm.QueueType.PrimaryQueue,
                messagesDataGridView.SelectedRows.Cast<DataGridViewRow>()
                        .Select(r => (ServiceBusReceivedMessage)r.DataBoundItem),
                _serviceBusHelper, writeToLog))
            {
                form.ShowDialog();
            }
        }

        void SetCancelScheduledMessageToolStripMenuItemText(bool multipleSelectedRows)
        {
            cancelScheduledMessageToolStripMenuItem.Text = multipleSelectedRows
             ? "Cancel Selected Scheduled Messages"
             : "Cancel Selected Scheduled Message";
        }

        void deleteSelectedSharedDeadLetterMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteSelectedSharedDeadLetterMessagesToolStripMenuItem_Click(sender, e);
        }

        async void deleteSelectedSharedDeadLetterMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataGridView = GetActiveDeadletterGridView();

            if (dataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var messages = dataGridView.SelectedRows.Cast<DataGridViewRow>()
                .Select(r => r.DataBoundItem as ServiceBusReceivedMessage);

            string confirmationText;
            var transferText = dataGridView == transferDeadletterDataGridView ? "transfer " : string.Empty;

            if (messages.Count() == 1)
            {
                confirmationText = "Are you sure you want to delete the selected message from the " +
                    $"{transferText}dead-letter subqueue for the {queueDescription.Name} queue?";
            }
            else
            {
                confirmationText = $"Are you sure you want to delete {messages.Count()} messages from the " +
                    $"{transferText}dead-letter subqueue for {queueDescription.Name} queue?";
            }

            using (var deleteForm = new DeleteForm(confirmationText))
            {
                if (deleteForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            var sequenceNumbersToDelete = messages.Select(s => s?.SequenceNumber).ToList();
            var deadLetterMessageHandler = new DeadLetterMessageHandler(writeToLog, _serviceBusHelper,
                MainForm.SingletonMainForm.ReceiveTimeout, queueDescription);

            try
            {
                Application.UseWaitCursor = true;
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var messagesDeleteCount = sequenceNumbersToDelete.Count;
                var result = await deadLetterMessageHandler.DeleteMessages(sequenceNumbersToDelete,
                    TransferDLQ: dataGridView == transferDeadletterDataGridView ? true : false);

                DataGridViewHelper.RemoveDataGridRowsUsingSequenceNumbers(dataGridView, result.DeletedSequenceNumbers);

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
            ShowAppropriateSharedDeadletterMenuItems(e);
        }

        private void transferDeadletterDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            ShowAppropriateSharedDeadletterMenuItems(e);
        }

        private void repairAndResubmitSharedDeadletterMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataGridView = GetActiveDeadletterGridView();

            var currentRowIndex = dataGridView == deadletterDataGridView
                ? currentDeadletterMessageRowIndex
                : currentTransferDeadletterMessageRowIndex;

            RepairAndResubmitSharedDeadletterMessage(new DataGridViewCellEventArgs(0, currentRowIndex));
        }

        private async void resubmitSharedDeadletterMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ResubmitSelectedDeadletterMessages(GetActiveDeadletterGridView());
        }

        private async void resubmitSelectedSharedDeadletterMessagesInBatchModeToolStripMenuItem_Click(object sender,
            EventArgs e)
        {
            await ResubmitSelectedDeadletterMessages(GetActiveDeadletterGridView());
        }

        async Task ResubmitSelectedDeadletterMessages(DataGridView dataGridView)
        {
            try
            {
                if (dataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }

                using (var form = new MessageForm(
                    queueDescription,
                    dataGridView == deadletterDataGridView
                        ? MessageForm.QueueType.Deadletter
                        : MessageForm.QueueType.TransferDeadletter,
                    dataGridView.SelectedRows.Cast<DataGridViewRow>()
                           .Select(r => (ServiceBusReceivedMessage)r.DataBoundItem),
                    _serviceBusHelper, writeToLog))
                {
                    form.ShowDialog();
                    if (form.RemovedSequenceNumbers != null && form.RemovedSequenceNumbers.Any())
                    {
                        DataGridViewHelper.RemoveDataGridRowsUsingSequenceNumbers(dataGridView, form.RemovedSequenceNumbers);
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
                    //FilterMessages(); TODO: 
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
                    //FilterMessages(); TODO: 
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

        private static bool IsWithinDateTimeRange(ServiceBusReceivedMessage message, DateTime? fromDateTime, DateTime? toDateTime)
        {
            if (message.EnqueuedTime < (fromDateTime ?? DateTime.MinValue))
            {
                return false;
            }
            if (message.EnqueuedTime > (toDateTime ?? DateTime.MaxValue))
            {
                return false;
            }
            return true;
        }

        private void FilterMessages()
        {
            var bindingList = new SortableBindingList<ServiceBusReceivedMessage>();
            try
            {
                if (messagesFilterFromDate == null && messagesFilterToDate == null && string.IsNullOrWhiteSpace(messagesFilterExpression))
                {
                    bindingList = messageBindingList;
                    messagesBindingSource.DataSource = messageBindingList;
                    messagesDataGridView.DataSource = messagesBindingSource;
                    writeToLog(FilterExpressionRemovedMessage);
                }
                //else
                //{
                //    var filteredList = messageBindingList.ToList(); TODO: 
                //    if (!string.IsNullOrWhiteSpace(messagesFilterExpression))
                //    {
                //        //Filter filter;
                //        try
                //        {
                //            var sqlFilter = new SqlFilter(messagesFilterExpression);
                //            sqlFilter.Validate();
                //            filter = sqlFilter.Preprocess();
                //        }
                //        catch (Exception ex)
                //        {
                //            writeToLog(string.Format(FilterExpressionNotValidMessage, messagesFilterExpression,
                //                ex.Message));
                //            return;
                //        }

                //        filteredList = filteredList.Where(filter.Match).ToList();
                //    }

                //    if (messagesFilterFromDate != null || messagesFilterToDate != null)
                //    {
                //        filteredList = filteredList.Where(msg => IsWithinDateTimeRange(msg, messagesFilterFromDate, messagesFilterToDate)).ToList();
                //    }

                //    bindingList = new SortableBindingList<ServiceBusReceivedMessage>(filteredList)
                //    {
                //        AllowEdit = false,
                //        AllowNew = false,
                //        AllowRemove = false
                //    };
                //    messagesBindingSource.DataSource = bindingList;
                //    messagesDataGridView.DataSource = messagesBindingSource;
                //    writeToLog(string.Format(FilterExpressionAppliedMessage, messagesFilterExpression, messagesFilterFromDate ?? DateTime.MinValue, messagesFilterToDate ?? DateTime.MaxValue, bindingList.Count));
                //}
            }
            finally
            {
                if (!bindingList.Any())
                {
                    txtMessageText.Text = string.Empty;
                    messageCustomPropertyGrid.SelectedObject = null;
                    messagePropertyGrid.SelectedObject = null;
                }
                else
                {
                    if (messagesDataGridView.Rows.Count > 0)
                    {
                        brokeredMessage = default!;
                        messagesDataGridView_RowEnter(this, new DataGridViewCellEventArgs(0, 0));
                    }
                }
            }
        }

        private void FilterDeadletters()
        {
            var bindingList = new SortableBindingList<ServiceBusReceivedMessage>();
            try
            {
                if (deadletterFilterFromDate == null && deadletterFilterToDate == null && string.IsNullOrWhiteSpace(deadletterFilterExpression))
                {
                    bindingList = deadletterBindingList;
                    deadletterBindingSource.DataSource = deadletterBindingList;
                    deadletterDataGridView.DataSource = deadletterBindingSource;
                    writeToLog(FilterExpressionRemovedMessage);
                }
                //else TODO: 
                //{
                //    var filteredList = deadletterBindingList.ToList();
                //    if (!string.IsNullOrWhiteSpace(deadletterFilterExpression))
                //    {
                //        Filter filter;
                //        try
                //        {
                //            var sqlFilter = new SqlFilter(deadletterFilterExpression);
                //            sqlFilter.Validate();
                //            filter = sqlFilter.Preprocess();
                //        }
                //        catch (Exception ex)
                //        {
                //            writeToLog(string.Format(FilterExpressionNotValidMessage, deadletterFilterExpression,
                //                ex.Message));
                //            return;
                //        }

                //        filteredList = filteredList.Where(filter.Match).ToList();
                //    }

                //    if (deadletterFilterFromDate != null || deadletterFilterToDate != null)
                //    {
                //        filteredList = filteredList.Where(msg => IsWithinDateTimeRange(msg, deadletterFilterFromDate, deadletterFilterToDate)).ToList();
                //    }

                //    bindingList = new SortableBindingList<ServiceBusReceivedMessage>(filteredList)
                //    {
                //        AllowEdit = false,
                //        AllowNew = false,
                //        AllowRemove = false
                //    };
                //    deadletterBindingSource.DataSource = bindingList;
                //    deadletterDataGridView.DataSource = deadletterBindingSource;
                //    writeToLog(string.Format(FilterExpressionAppliedMessage, deadletterFilterExpression, deadletterFilterFromDate ?? DateTime.MinValue, deadletterFilterToDate ?? DateTime.MaxValue, bindingList.Count));
                //}
            }
            finally
            {
                if (!bindingList.Any())
                {
                    txtDeadletterText.Text = string.Empty;
                    deadletterCustomPropertyGrid.SelectedObject = null;
                    deadletterPropertyGrid.SelectedObject = null;
                }
                else
                {
                    if (deadletterDataGridView.Rows.Count > 0)
                    {
                        deadletterMessage = default!;
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

        private string CreateFileName()
        {
            return string.Format(MessageFileFormat,
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_serviceBusHelper.Connection.Namespace.Namespace),
                DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }

        private string CreateFileNameAutoRecognize()
        {
            return string.Format(MessageFileFormatAutoRecognize,
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_serviceBusHelper.Connection.Namespace.Namespace),
                DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }

        private async void btnPurgeMessages_Click(object sender, EventArgs e)
        {
            await PurgeMessagesAsync();
        }
        #endregion

        #region Save Messages

        void saveSelectedMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentMessageRowIndex < 0)
                {
                    return;
                }
                var bindingList = messagesBindingSource.DataSource as BindingList<ServiceBusReceivedMessage>;
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

                var bindingList = messagesBindingSource.DataSource as BindingList<ServiceBusReceivedMessage>;
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

        void saveSelectedMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (messagesDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                var messages =
                    messagesDataGridView.SelectedRows.Cast<DataGridViewRow>()
                        .Select(r => r.DataBoundItem as ServiceBusReceivedMessage);
                IEnumerable<ServiceBusReceivedMessage?> brokeredMessages = messages as ServiceBusReceivedMessage?[] ?? messages.ToArray();
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
                    var bodies = brokeredMessages.Select(bm => _serviceBusHelper.GetMessageText(bm?.Body));
                    writer.Write(MessageSerializationHelper.Serialize(brokeredMessages, bodies, doNotSerializeBody: true));
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
                                        .Select(r => r.DataBoundItem as ServiceBusReceivedMessage);
                IEnumerable<ServiceBusReceivedMessage?> brokeredMessages = messages as ServiceBusReceivedMessage[] ?? messages.ToArray();
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

                var bodies = brokeredMessages.Select(bm => _serviceBusHelper.GetMessageText(bm?.Body));
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

        void saveSelectedSharedDeadletteredMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSelectedMessage(SaveInJsonFormat: true);
        }

        void saveSelectedSharedDeadletteredMessageBodyAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSelectedMessage(SaveInJsonFormat: false);
        }

        void saveSelectedSharedDeadletteredMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSelectedMessages(SaveInJsonFormat: true);
        }

        void saveSelectedSharedDeadletteredMessagesBodyAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSelectedMessages(SaveInJsonFormat: false);
        }

        void SaveSelectedMessage(bool SaveInJsonFormat)
        {
            var activeGridView = GetActiveDeadletterGridView();

            var currentRowIndex = activeGridView == deadletterDataGridView
                ? currentDeadletterMessageRowIndex
                : currentTransferDeadletterMessageRowIndex;

            if (currentRowIndex < 0)
            {
                return;
            }

            var bindingSource = activeGridView == deadletterDataGridView
                ? deadletterBindingSource
                : transferDeadletterBindingSource;

            var bindingList = bindingSource.DataSource as BindingList<ServiceBusReceivedMessage>;

            if (bindingList == null)
            {
                return;
            }

            var text = activeGridView == deadletterDataGridView
                ? txtDeadletterText.Text
                : txtTransferDeadletterText.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = SaveAsTitle;

            if (SaveInJsonFormat)
            {
                saveFileDialog.DefaultExt = JsonExtension;
                saveFileDialog.Filter = JsonFilter;
                saveFileDialog.FileName = CreateFileName();
            }
            else
            {
                saveFileDialog.DefaultExt = TxtExtension;
                saveFileDialog.Filter = AllFilesFilter;
                saveFileDialog.FileName = CreateFileNameAutoRecognize();
            }

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
                if (SaveInJsonFormat)
                {
                    writer.Write(MessageSerializationHelper.Serialize(bindingList[currentRowIndex], text));
                }
                else
                {
                    writer.Write(txtDeadletterText.Text);
                }
            }
        }

        void SaveSelectedMessages(bool SaveInJsonFormat)
        {
            var activeGridView = GetActiveDeadletterGridView();

            var currentRowIndex = activeGridView == deadletterDataGridView
                ? currentDeadletterMessageRowIndex
                : currentTransferDeadletterMessageRowIndex;

            if (activeGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            var messages = activeGridView.SelectedRows.Cast<DataGridViewRow>()
                        .Select(r => r.DataBoundItem as ServiceBusReceivedMessage);
            IEnumerable<ServiceBusReceivedMessage?> brokeredMessages = messages as ServiceBusReceivedMessage[] ?? messages.ToArray();

            if (!brokeredMessages.Any())
            {
                return;
            }

            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = SaveAsTitle;

            if (SaveInJsonFormat)
            {
                saveFileDialog.DefaultExt = JsonExtension;
                saveFileDialog.Filter = JsonFilter;
                saveFileDialog.FileName = CreateFileName();
            }
            else
            {
                saveFileDialog.DefaultExt = TxtExtension;
                saveFileDialog.Filter = AllFilesFilter;
                saveFileDialog.FileName = CreateFileNameAutoRecognize();
            }

            if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {
                return;
            }

            if (File.Exists(saveFileDialog.FileName))
            {
                File.Delete(saveFileDialog.FileName);
            }

            if (SaveInJsonFormat)
            {
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    var bodies = brokeredMessages.Select(bm => _serviceBusHelper.GetMessageText(bm?.Body));
                    writer.Write(MessageSerializationHelper.Serialize(brokeredMessages, bodies, doNotSerializeBody: true));
                }
            }
            else
            {
                var bodies = brokeredMessages.Select(bm => _serviceBusHelper.GetMessageText(bm?.Body));
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
        }

        #endregion Save Messages

        private void selectAllMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            messagesDataGridView.SelectAll();
        }

        private void selectAllDeadletterMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activeGridView = GetActiveDeadletterGridView();

            if (activeGridView == deadletterDataGridView)
            {
                deadletterDataGridView.SelectAll();
            }
            else if (activeGridView == transferDeadletterDataGridView)
            {
                transferDeadletterDataGridView.SelectAll();
            }
        }

        private async void btnPurgeDeadletterQueueMessages_Click(object sender, EventArgs e)
        {
            await PurgeDeadletterQueueMessagesAsync();
        }

        // Returns either the plain deadletter grid view or the transfer deadletter grid view,
        // depending on which tab is selected.
        DataGridView GetActiveDeadletterGridView()
        {
            if (mainTabControl.SelectedTab == mainTabControl.TabPages[DeadletterTabPage])
            {
                return deadletterDataGridView;
            }
            else if (mainTabControl.SelectedTab == mainTabControl.TabPages[TransferDeadletterTabPage])
            {
                return transferDeadletterDataGridView;
            }
            else
            {
                throw new InvalidOperationException("Unexpected tab selected.");
            }
        }

        void ShowAppropriateSharedDeadletterMenuItems(DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex == -1)
            {
                return;
            }

            var activeGridView = GetActiveDeadletterGridView();

            activeGridView.Rows[e.RowIndex].Selected = true;
            var multipleSelectedRows = activeGridView.SelectedRows.Count > 1;

            repairAndResubmitSharedDeadletterToolStripMenuItem.Visible = !multipleSelectedRows;
            resubmitSharedDeadletterToolStripMenuItem.Visible = !multipleSelectedRows;
            saveSelectedSharedDeadletteredMessageToolStripMenuItem.Visible = !multipleSelectedRows;
            saveSelectedSharedDeadletteredMessageBodyAsFileToolStripMenuItem.Visible = !multipleSelectedRows;
            deleteSelectedSharedDeadletterMessageToolStripMenuItem.Visible = !multipleSelectedRows;


            resubmitSelectedSharedDeadletterInBatchModeToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedSharedDeadletteredMessagesToolStripMenuItem.Visible = multipleSelectedRows;
            saveSelectedSharedDeadletteredMessagesBodyAsFileToolStripMenuItem.Visible = multipleSelectedRows;
            deleteSelectedSharedDeadletterMessagesToolStripMenuItem.Visible = multipleSelectedRows;

            sharedDeadletterContextMenuStrip.Show(Cursor.Position);
        }

        void RepairAndResubmitSharedDeadletterMessage(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var activeGridView = GetActiveDeadletterGridView();

            var bindingList = activeGridView == deadletterDataGridView
                ? deadletterBindingSource.DataSource as BindingList<ServiceBusReceivedMessage>
                : transferDeadletterBindingSource.DataSource as BindingList<ServiceBusReceivedMessage>;

            if (bindingList == null)
            {
                return;
            }

            using (var messageForm = new MessageForm(queueDescription,
                activeGridView == deadletterDataGridView
                    ? MessageForm.QueueType.Deadletter
                    : MessageForm.QueueType.TransferDeadletter,
                bindingList[e.RowIndex],
                _serviceBusHelper,
                writeToLog))
            {
                messageForm.ShowDialog();

                Application.UseWaitCursor = true;

                try
                {
                    if (messageForm.RemovedSequenceNumbers != null && messageForm.RemovedSequenceNumbers.Any())
                    {
                        DataGridViewHelper.RemoveDataGridRowsUsingSequenceNumbers(activeGridView, messageForm.RemovedSequenceNumbers);
                    }
                }
                finally
                {
                    Application.UseWaitCursor = false;
                }
            }
        }
    }
}
