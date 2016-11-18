

#region Using Directives
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Transactions;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.ServiceBus.Messaging;
using Cursor = System.Windows.Forms.Cursor;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public delegate void UpdateStatisticsDelegate(long messageNumber, long elapsedMilliseconds, DirectionType direction);

    public partial class TestQueueControl : UserControl
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string LabelFormat = "{0:0.000}";

        //***************************
        // Properties & Types
        //***************************
        private const string PropertyKey = "Key";
        private const string PropertyType = "Type";
        private const string PropertyValue = "Value";
        private const string DefaultSessionId = "1";
        private const string DefaultMessageCount = "1";
        private const string DefaulSendBatchSize = "10";
        private const string DefaulReceiveBatchSize = "10";
        private const string DefaultSenderTaskCount = "1";
        private const string DefaultReceiverTaskCount = "1";
        private const string DefaultReceiveTimeout = "1";
        private const string DefaultSessionTimeout = "3";
        private const string DefaultPrefetchCount = "0";
        private const string PeekLock = "PeekLock";
        private const string StartCaption = "Start";
        private const string StopCaption = "Stop";
        private const string DefaultFilterExpression = "1=1";

        //***************************
        // Messages
        //***************************
        private const string MessageCannotBeNull = "The Message field cannot be null.";
        private const string ReceiveTimeoutCannotBeNull = "The receive timeout field cannot be null and must be a non negative integer number.";
        private const string SessionTimeoutCannotBeNull = "The session timeout field cannot be null and must be a non negative integer number.";
        private const string PrefetchCountCannotBeNull = "The prefetch count field cannot be null and must be an integer number.";
        private const string DefaultMessageText = "Hi mate, how are you?";
        private const string MessageCountMustBeANumber = "The Message Count field must be an integer number greater or equal to zero.";
        private const string SendTaskCountMustBeANumber = "The Sender Task Count field must be an integer number greater than zero.";
        private const string ReceiveTaskCountMustBeANumber = "The Receiver Task Count field must be an integer number greater than zero.";
        private const string SenderBatchSizeMustBeANumber = "The Sender Batch Size field must be an integer number greater than zero.";
        private const string ReceiverBatchSizeMustBeANumber = "The Receiver Batch Size field must be an integer number greater than zero.";
        private const string SenderThinkTimeMustBeANumber = "The Sender Think Time field must be an integer number greater than zero.";
        private const string ReceiverThinkTimeMustBeANumber = "The Receiver Think Time field must be an integer number greater than zero.";
        private const string TransactionCommitted = " - Transaction committed.";
        private const string TransactionAborted = " - Transaction aborted.";
        private const string NoMoreSessionsToAccept = "Receiver[{0}]: No more sessions to accept.";
        private const string FilterExpressionIsNotValid = "The filter expression is not valid.";
        private const string NoMessageSelected = "No message to send has been selected.";

        //***************************
        // Tooltips
        //***************************
        private const string ContentTypeTooltip = "Gets or sets the type of the content.";
        private const string ToTooltip = "Gets or sends the send to address.";
        private const string ScheduledEnqueueTimeUtcTooltip = "Gets or sets the timeout in seconds after which the message will be enqueued.";
        private const string ReceiveModeTooltip = "Gets or sets the receive mode of message receivers.";
        private const string MessageTextTooltip = "Gets or sets the message text.";
        private const string SenderEnabledToolTip = "Enable or disable message senders.";
        private const string ReceiverEnabledToolTip = "Enable or disable message receivers.";
        private const string LabelTooltip = "Gets or sets the message label.";
        private const string MessageIdTooltip = "Gets or sets the message id.";
        private const string SessionIdTooltip = "Gets or sets the session id.";
        private const string CorrelationIdTooltip = "Gets or sets the correlation id.";
        private const string ReplyToTooltip = "Gets or sets the replyTo field.";
        private const string ReplyToSessionIdTooltip = "Gets or sets the replyToSessionId field.";
        private const string MessagePropertiesTooltip = "Gets or sets the properties of the message.";
        private const string MessageCountTooltip = "Gets or sets the number of messages to send.";
        private const string SendTaskCountTooltip = "Gets or sets the count of message senders.";
        private const string ReceiverTaskCountTooltip = "Gets or sets the count of message receivers.";
        private const string UpdateMessageIdTooltip = "Gets or sets a boolean value indicating whether the id of the message must be updated when sending multiple messages.";
        private const string ReceiveTimeoutTooltip = "Gets or sets the receive timeout.";
        private const string SessionTimeoutTooltip = "Gets or sets the session timeout.";
        private const string FilterExpressionTooltip = "Gets or sets the filter expression used to select messages to move to the dead-letter queue or to defer.";
        private const string TryToReceiveTooltip = "Gets or sets a boolean value indicating whether trying to receive test messages.";
        private const string EnableSenderLoggingTooltip = "Enable logging of message content and properties for message senders.";
        private const string EnableReceiverLoggingTooltip = "Enable logging of message content and properties for message receivers.";
        private const string EnableSenderVerboseLoggingTooltip = "Enable verbose logging for message senders.";
        private const string EnableReceiverVerboseLoggingTooltip = "Enable verbose logging for message receivers.";
        private const string EnableSenderTransactionTooltip = "Enable transactional behavior for message senders.";
        private const string EnableReceiverTransactionTooltip = "Enable transactional behavior for message receivers.";
        private const string EnableSenderCommitTooltip = "Enable transaction commit for message senders.";
        private const string EnableReceiverCommitTooltip = "Enable transaction commit for message receivers.";
        private const string EnableMessageIdUpdateTooltip = "Enable automatic message id update.";
        private const string OneSessionPerSenderTaskTooltip = "Use one session per sender task.";
        private const string EnableMoveToDeadLetterTooltip = "When this option is enabled, all received messages are moved to the DeadLetter queue.";
        private const string EnableReadFromDeadLetterTooltip = "When this option is enabled, the receivers attempts to read messages from the DeadLetter queue.";

        //***************************
        // Tab Pages
        //***************************
        private const int MessageTabPage = 0;

        //***************************
        // ListView Column Indexes
        //***************************
        private const int NameListViewColumnIndex = 0;
        private const int SizeListViewColumnIndex = 1;
        #endregion

        #region Private Instance Fields
        private readonly QueueDescription queueDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly MainForm mainForm;
        private readonly WriteToLogDelegate writeToLog;
        private readonly BindingSource bindingSource = new BindingSource();
        private int receiveTimeout = 60;
        private int sessionTimeout = 60;
        private int prefetchCount;
        private List<MessageSender> messageSenderCollection = new List<MessageSender>();
        private CancellationTokenSource senderCancellationTokenSource;
        private CancellationTokenSource receiverCancellationTokenSource;
        private CancellationTokenSource managerCancellationTokenSource;
        private CancellationTokenSource graphCancellationTokenSource;
        private ManualResetEventSlim managerResetEvent;
        private long messageCount = 1;
        private int senderBatchSize = 1;
        private int receiverBatchSize = 1;
        private int senderThinkTime;
        private int receiverThinkTime;
        private long currentIndex;
        private long senderMessageNumber;
        private double senderMessagesPerSecond;
        private double senderMinimumTime;
        private double senderMaximumTime;
        private double senderAverageTime;
        private double senderTotalTime;
        private long receiverMessageNumber;
        private double receiverMessagesPerSecond;
        private double receiverMinimumTime;
        private double receiverMaximumTime;
        private double receiverAverageTime;
        private double receiverTotalTime;
        private int actionCount;
        private int senderTaskCount = 1;
        private int receiverTaskCount = 1;
        private bool isSenderFaulted;
        private Filter filter;
        private BlockingCollection<Tuple<long, long, DirectionType>> blockingCollection;
        #endregion

        #region Private Static Fields
        private static readonly List<string> types = new List<string> { "Boolean", "Byte", "Int16", "Int32", "Int64", "Single", "Double", "Decimal", "Guid", "DateTime", "String" };
        #endregion

        #region Public Constructors
        public TestQueueControl(MainForm mainForm,
                                WriteToLogDelegate writeToLog,
                                ServiceBusHelper serviceBusHelper,
                                QueueDescription queueDescription)
        {
            this.mainForm = mainForm;
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.queueDescription = queueDescription;
            InitializeComponent();
            InitializeControls();
        }
        #endregion

        #region Public Events
        public event Action OnCancel;
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            try
            {
                // Populate filenames listview control
                if (mainForm.FileNames.Any())
                {
                    foreach (var tuple in mainForm.FileNames)
                    {
                        messageFileListView.Items.Add(new ListViewItem(new[]
                                                        {
                                                            tuple.Item1, 
                                                            tuple.Item2
                                                        }));
                    }
                    btnClearFiles.Enabled = messageFileListView.Items.Count > 0;
                }

                // Set Think Time
                txtSenderThinkTime.Text = mainForm.SenderThinkTime.ToString(CultureInfo.InvariantCulture);
                txtReceiverThinkTime.Text = mainForm.ReceiverThinkTime.ToString(CultureInfo.InvariantCulture);
                senderThinkTime = mainForm.SenderThinkTime;
                receiverThinkTime = mainForm.ReceiverThinkTime;

                // Set Binding Source
                bindingSource.DataSource = MessagePropertyInfo.Properties;

                // Initialize body type combo
                cboBodyType.SelectedIndex = 0;

                // Initialize the DataGridView.
                propertiesDataGridView.AutoGenerateColumns = false;
                propertiesDataGridView.AutoSize = true;
                propertiesDataGridView.DataSource = bindingSource;
                propertiesDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                var textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = PropertyKey,
                    Name = PropertyKey,
                    Width = 138
                };
                propertiesDataGridView.Columns.Add(textBoxColumn);

                // Create the Type column
                var comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = types,
                    DataPropertyName = PropertyType,
                    Name = PropertyType,
                    Width = 90,
                    FlatStyle = FlatStyle.Flat
                };
                propertiesDataGridView.Columns.Add(comboBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = PropertyValue,
                    Name = PropertyValue,
                    Width = 138
                };
                propertiesDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                propertiesDataGridView.EnableHeadersVisualStyles = false;

                // Set the selection background color for all the cells.
                propertiesDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                propertiesDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                propertiesDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                propertiesDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                propertiesDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //propertiesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //propertiesDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                propertiesDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                propertiesDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                propertiesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                propertiesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                txtMessageText.Text = mainForm != null &&
                                      !string.IsNullOrWhiteSpace(mainForm.MessageText) ?
                                      XmlHelper.Indent(mainForm.MessageText) :
                                      DefaultMessageText;
                txtLabel.Text = mainForm != null &&
                                !string.IsNullOrWhiteSpace(mainForm.Label) ?
                                mainForm.Label :
                                DefaultMessageText;
                txtMessageId.Text = Guid.NewGuid().ToString();
                if (queueDescription.RequiresSession)
                {
                    txtSessionId.Text = DefaultSessionId;
                    checkBoxOneSessionPerTask.Checked = true;
                }
                else
                {
                    txtSessionId.Text = null;
                    checkBoxOneSessionPerTask.Checked = false;
                }
                txtMessageCount.Text = DefaultMessageCount;
                txtSendBatchSize.Text = DefaulSendBatchSize;
                txtReceiveBatchSize.Text = DefaulReceiveBatchSize;
                txtSendTaskCount.Text = DefaultSenderTaskCount;
                txtReceiveTaskCount.Text = DefaultReceiverTaskCount;
                txtReceiveTimeout.Text = mainForm != null ?
                                         mainForm.ReceiveTimeout.ToString(CultureInfo.InvariantCulture) :
                                         DefaultReceiveTimeout;
                txtSessionTimeout.Text = mainForm != null ?
                                         mainForm.ServerTimeout.ToString(CultureInfo.InvariantCulture) :
                                         DefaultSessionTimeout;
                txtPrefetchCount.Text = mainForm != null ?
                                        mainForm.PrefetchCount.ToString(CultureInfo.InvariantCulture) :
                                        DefaultPrefetchCount;
                cboReceivedMode.SelectedIndex = 1;
                txtSendBatchSize.Enabled = false;
                txtReceiveBatchSize.Enabled = false;

                // Create Tooltips for controls
                toolTip.SetToolTip(senderEnabledCheckBox, SenderEnabledToolTip);
                toolTip.SetToolTip(receiverEnabledCheckBox, ReceiverEnabledToolTip);
                toolTip.SetToolTip(txtMessageText, MessageTextTooltip);
                toolTip.SetToolTip(propertiesDataGridView, MessagePropertiesTooltip);
                toolTip.SetToolTip(txtLabel, LabelTooltip);
                toolTip.SetToolTip(txtMessageId, MessageIdTooltip);
                toolTip.SetToolTip(txtSessionId, SessionIdTooltip);
                toolTip.SetToolTip(txtCorrelationId, CorrelationIdTooltip);
                toolTip.SetToolTip(txtReplyTo, ReplyToTooltip);
                toolTip.SetToolTip(txtReplyToSessionId, ReplyToSessionIdTooltip);
                toolTip.SetToolTip(txtMessageCount, MessageCountTooltip);
                toolTip.SetToolTip(txtSendTaskCount, SendTaskCountTooltip);
                toolTip.SetToolTip(txtReceiveTaskCount, ReceiverTaskCountTooltip);
                toolTip.SetToolTip(checkBoxUpdateMessageId, UpdateMessageIdTooltip);
                toolTip.SetToolTip(txtReceiveTimeout, ReceiveTimeoutTooltip);
                toolTip.SetToolTip(txtSessionTimeout, SessionTimeoutTooltip);
                toolTip.SetToolTip(txtFilterExpression, FilterExpressionTooltip);
                toolTip.SetToolTip(txtTo, ToTooltip);
                toolTip.SetToolTip(txtContentType, ContentTypeTooltip);
                toolTip.SetToolTip(txtScheduledEnqueueTimeUtc, ScheduledEnqueueTimeUtcTooltip);
                toolTip.SetToolTip(receiverEnabledCheckBox, TryToReceiveTooltip);
                toolTip.SetToolTip(checkBoxEnableSenderLogging, EnableSenderLoggingTooltip);
                toolTip.SetToolTip(checkBoxEnableReceiverLogging, EnableReceiverLoggingTooltip);
                toolTip.SetToolTip(checkBoxSenderVerboseLogging, EnableSenderVerboseLoggingTooltip);
                toolTip.SetToolTip(checkBoxReceiverVerboseLogging, EnableReceiverVerboseLoggingTooltip);
                toolTip.SetToolTip(checkBoxOneSessionPerTask, OneSessionPerSenderTaskTooltip);
                toolTip.SetToolTip(checkBoxSenderUseTransaction, EnableSenderTransactionTooltip);
                toolTip.SetToolTip(checkBoxReceiverUseTransaction, EnableReceiverTransactionTooltip);
                toolTip.SetToolTip(checkBoxSenderCommitTransaction, EnableSenderCommitTooltip);
                toolTip.SetToolTip(checkBoxReceiverCommitTransaction, EnableReceiverCommitTooltip);
                toolTip.SetToolTip(checkBoxUpdateMessageId, EnableMessageIdUpdateTooltip);
                toolTip.SetToolTip(checkBoxMoveToDeadLetter, EnableMoveToDeadLetterTooltip);
                toolTip.SetToolTip(checkBoxReadFromDeadLetter, EnableReadFromDeadLetterTooltip);
                toolTip.SetToolTip(cboReceivedMode, ReceiveModeTooltip);

                splitContainer.SplitterWidth = 16;
                splitContainer.SplitterDistance = (splitContainer.Size.Width - splitContainer.SplitterWidth)/2;
                propertiesDataGridView.Size = txtMessageText.Size;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private bool ValidateParameters()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMessageText.Text))
                {
                    writeToLog(MessageCannotBeNull);
                    return false;
                }
                int temp;
                if (string.IsNullOrWhiteSpace(txtReceiveTimeout.Text) ||
                    !int.TryParse(txtReceiveTimeout.Text, out temp) ||
                    temp < 0)
                {
                    writeToLog(ReceiveTimeoutCannotBeNull);
                    return false;
                }
                receiveTimeout = temp;
                if (string.IsNullOrWhiteSpace(txtSessionTimeout.Text) ||
                    !int.TryParse(txtSessionTimeout.Text, out temp) ||
                    temp < 0)
                {
                    writeToLog(SessionTimeoutCannotBeNull);
                    return false;
                }
                sessionTimeout = temp;
                if (string.IsNullOrWhiteSpace(txtPrefetchCount.Text) ||
                    !int.TryParse(txtPrefetchCount.Text, out temp))
                {
                    writeToLog(PrefetchCountCannotBeNull);
                    return false;
                }
                prefetchCount = temp;
                if (!int.TryParse(txtMessageCount.Text, out temp) || temp < 0)
                {
                    writeToLog(MessageCountMustBeANumber);
                    return false;
                }
                messageCount = temp;
                if (!int.TryParse(txtSendBatchSize.Text, out temp) || temp <= 0)
                {
                    writeToLog(SenderBatchSizeMustBeANumber);
                    return false;
                }
                senderBatchSize = temp;
                if (!int.TryParse(txtReceiveBatchSize.Text, out temp) || temp <= 0)
                {
                    writeToLog(ReceiverBatchSizeMustBeANumber);
                    return false;
                }
                receiverBatchSize = temp;
                if (!int.TryParse(txtSenderThinkTime.Text, out temp) || temp <= 0)
                {
                    writeToLog(SenderThinkTimeMustBeANumber);
                    return false;
                }
                senderThinkTime = temp;
                if (!int.TryParse(txtReceiverThinkTime.Text, out temp) || temp <= 0)
                {
                    writeToLog(ReceiverThinkTimeMustBeANumber);
                    return false;
                }
                receiverThinkTime = temp;
                if (!int.TryParse(txtReceiveBatchSize.Text, out temp) || temp <= 0)
                {
                    writeToLog(ReceiverBatchSizeMustBeANumber);
                    return false;
                }
                receiverBatchSize = temp;
                if (!int.TryParse(txtSendTaskCount.Text, out temp) || temp <= 0)
                {
                    writeToLog(SendTaskCountMustBeANumber);
                    return false;
                }
                senderTaskCount = temp;
                if (!int.TryParse(txtReceiveTaskCount.Text, out temp) || temp <= 0)
                {
                    writeToLog(ReceiveTaskCountMustBeANumber);
                    return false;
                }
                receiverTaskCount = temp;
                var sqlFilter = new SqlFilter(!string.IsNullOrWhiteSpace(txtFilterExpression.Text)
                                                                  ? txtFilterExpression.Text
                                                                  : DefaultFilterExpression);
                sqlFilter.Validate();
                filter = sqlFilter.Preprocess();
                if (filter == null)
                {
                    writeToLog(FilterExpressionIsNotValid);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
            return true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStart.Text == StopCaption)
                {
                    CancelActions();
                    btnStart.Text = StartCaption;
                    return;
                }

                if (serviceBusHelper != null &&
                    ValidateParameters())
                {
                    btnStart.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                    //*****************************************************************************************************
                    //                                   Retrieve Messaging Factory
                    //*****************************************************************************************************
                    var messagingFactory = serviceBusHelper.MessagingFactory;

                    //*****************************************************************************************************
                    //                                   Initialize Statistics and Manager Action
                    //*****************************************************************************************************
                    actionCount = 0;
                    senderMessageNumber = 0;
                    senderMessagesPerSecond = 0;
                    senderMinimumTime = long.MaxValue;
                    senderMaximumTime = 0;
                    senderAverageTime = 0;
                    senderTotalTime = 0;
                    receiverMessageNumber = 0;
                    receiverMessagesPerSecond = 0;
                    receiverMinimumTime = long.MaxValue;
                    receiverMaximumTime = 0;
                    receiverAverageTime = 0;
                    receiverTotalTime = 0;

                    lblSenderLastTime.Text = string.Empty;
                    lblSenderAverageTime.Text = string.Empty;
                    lblSenderMaximumTime.Text = string.Empty;
                    lblSenderMinimumTime.Text = string.Empty;
                    lblSenderMessagesPerSecond.Text = string.Empty;
                    lblSenderMessageNumber.Text = string.Empty;

                    lblReceiverLastTime.Text = string.Empty;
                    lblReceiverAverageTime.Text = string.Empty;
                    lblReceiverMaximumTime.Text = string.Empty;
                    lblReceiverMinimumTime.Text = string.Empty;
                    lblReceiverMessagesPerSecond.Text = string.Empty;
                    lblReceiverMessageNumber.Text = string.Empty;

                    if (checkBoxSenderEnableGraph.Checked ||
                        checkBoxReceiverEnableGraph.Checked)
                    {
                        chart.Series.ToList().ForEach(s => s.Points.Clear());
                    }
                    managerResetEvent = new ManualResetEventSlim(false);
                    Action<CancellationTokenSource> managerAction = cts =>
                    {
                        if (cts == null)
                        {
                            return;
                        }
                        try
                        {
                            managerResetEvent.Wait(cts.Token);
                        }
                        catch (OperationCanceledException)
                        {
                        }
                        if (!cts.IsCancellationRequested)
                        {
                            Invoke((MethodInvoker)delegate { btnStart.Text = StartCaption; });
                        }
                    };

                    Action updateGraphAction = () =>
                    {
                        var ok = true;
                        long max = 10;
                        while (!graphCancellationTokenSource.IsCancellationRequested && (actionCount > 1 || ok))
                        {
                            ok = true;
                            long sendMessageNumber = 0;
                            long receiveMessageNumber = 0;
                            long sendTotalTime = 0;
                            long receiveTotalTime = 0;
                            while (ok && sendMessageNumber < max && receiveMessageNumber < max)
                            {
                                Tuple<long, long, DirectionType> tuple;
                                ok = blockingCollection.TryTake(out tuple, 10);
                                if (ok)
                                {
                                    if (tuple.Item3 == DirectionType.Send)
                                    {
                                        sendMessageNumber += tuple.Item1;
                                        sendTotalTime += tuple.Item2;
                                        if (sendMessageNumber > max)
                                        {
                                            max = sendMessageNumber;
                                        }
                                        continue;
                                    }
                                    receiveMessageNumber += tuple.Item1;
                                    receiveTotalTime += tuple.Item2;
                                    if (receiveMessageNumber > max)
                                    {
                                        max = receiveMessageNumber;
                                    }
                                }
                            }
                            if (sendMessageNumber > 0)
                            {
                                var sendTuple = new Tuple<long, long, DirectionType>(sendMessageNumber, sendTotalTime, DirectionType.Send);
                                if (InvokeRequired)
                                {
                                    Invoke(new UpdateStatisticsDelegate(InternalUpdateStatistics),
                                           new object[] { sendTuple.Item1, 
                                                          sendTuple.Item2, 
                                                          sendTuple.Item3 });
                                }
                                else
                                {
                                    InternalUpdateStatistics(sendTuple.Item1,
                                                             sendTuple.Item2,
                                                             sendTuple.Item3);
                                }
                            }
                            if (receiveMessageNumber > 0)
                            {
                                var receiveTuple = new Tuple<long, long, DirectionType>(receiveMessageNumber, receiveTotalTime, DirectionType.Receive);
                                if (InvokeRequired)
                                {
                                    Invoke(new UpdateStatisticsDelegate(InternalUpdateStatistics),
                                           new object[] { receiveTuple.Item1, 
                                                          receiveTuple.Item2, 
                                                          receiveTuple.Item3 });
                                }
                                else
                                {
                                    InternalUpdateStatistics(receiveTuple.Item1,
                                                             receiveTuple.Item2,
                                                             receiveTuple.Item3);
                                }
                            }
                        }
                        if (Interlocked.Decrement(ref actionCount) == 0)
                        {
                            managerResetEvent.Set();
                        }
                    };

                    AsyncCallback updateGraphCallback = a =>
                    {
                        var action = a.AsyncState as Action;
                        if (action != null)
                        {
                            action.EndInvoke(a);
                            if (Interlocked.Decrement(ref actionCount) == 0)
                            {
                                managerResetEvent.Set();
                            }
                        }
                    };

                    blockingCollection = new BlockingCollection<Tuple<long, long, DirectionType>>();

                    //*****************************************************************************************************
                    //                                   Sending messages to a Queue
                    //*****************************************************************************************************

                    if (senderEnabledCheckBox.Checked && messageCount > 0)
                    {
                        // Create message senders. They are cached for later usage to improve performance.
                        if (isSenderFaulted ||
                            messageSenderCollection == null ||
                            messageSenderCollection.Count == 0 ||
                            messageSenderCollection.Count < senderTaskCount)
                        {
                            messageSenderCollection = new List<MessageSender>(senderTaskCount);
                            for (var i = 0; i < senderTaskCount; i++)
                            {
                                messageSenderCollection.Add(messagingFactory.CreateMessageSender(queueDescription.Path));
                            }
                            isSenderFaulted = false;
                        }

                        // Create outbound message template list
                        var messageTemplateList = new List<BrokeredMessage>();
                        var messageTextList = new List<string>();
                        if (messageTabControl.SelectedIndex == MessageTabPage)
                        {
                            messageTemplateList.Add(serviceBusHelper.CreateMessage(txtMessageText.Text,
                                                                                   txtLabel.Text,
                                                                                   txtContentType.Text,
                                                                                   GetMessageId(),
                                                                                   txtSessionId.Text,
                                                                                   txtCorrelationId.Text,
                                                                                   txtTo.Text,
                                                                                   txtReplyTo.Text,
                                                                                   txtReplyToSessionId.Text,
                                                                                   txtTimeToLive.Text,
                                                                                   txtScheduledEnqueueTimeUtc.Text,
                                                                                   bindingSource.Cast<MessagePropertyInfo>()));
                            messageTextList.Add(txtMessageText.Text);
                        }
                        else
                        {
                            var fileList = messageFileListView.Items.Cast<ListViewItem>().Where(i => i.Checked).Select(i => i.Text).ToList();
                            if (fileList.Count == 0)
                            {
                                writeToLog(NoMessageSelected);
                                return;
                            }
                            foreach (var fileName in fileList)
                            {
                                try
                                {
                                    using (var streamReader = new StreamReader(fileName))
                                    {
                                        var messageText = streamReader.ReadToEnd();
                                        messageTemplateList.Add(serviceBusHelper.CreateMessage(messageText,
                                                                                               txtLabel.Text,
                                                                                               txtContentType.Text,
                                                                                               GetMessageId(),
                                                                                               txtSessionId.Text,
                                                                                               txtCorrelationId.Text,
                                                                                               txtTo.Text,
                                                                                               txtReplyTo.Text,
                                                                                               txtReplyToSessionId.Text,
                                                                                               txtTimeToLive.Text,
                                                                                               txtScheduledEnqueueTimeUtc.Text,
                                                                                               bindingSource.Cast<MessagePropertyInfo>()));
                                        messageTextList.Add(messageText);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    HandleException(ex);
                                }
                            }
                        }
                        try
                        {
                            senderCancellationTokenSource = new CancellationTokenSource();
                            currentIndex = 0;

                            BodyType bodyType;
                            if (!Enum.TryParse(cboBodyType.Text, true, out bodyType))
                            {
                                bodyType = BodyType.Stream;
                            }

                            Func<long> getMessageNumber = () =>
                            {
                                lock (this)
                                {
                                    return currentIndex++;
                                }
                            };
                            Action<int, IEnumerable<BrokeredMessage>, IEnumerable<String>> senderAction = (taskId, messageTemplateEnumerable, messageTextEnumerable) =>
                            {
                                try
                                {
                                    string traceMessage;
                                    bool ok;
                                    
                                    if (checkBoxSenderUseTransaction.Checked)
                                    {
                                        using (var scope = new TransactionScope())
                                        {
                                            ok = serviceBusHelper.SendMessages(messageSenderCollection[taskId],
                                                                               messageTemplateEnumerable,
                                                                               getMessageNumber,
                                                                               messageCount,
                                                                               messageTextEnumerable,
                                                                               taskId,
                                                                               checkBoxUpdateMessageId.Checked,
                                                                               checkBoxAddMessageNumber.Checked,
                                                                               checkBoxOneSessionPerTask.Checked,
                                                                               checkBoxEnableSenderLogging.Checked,
                                                                               checkBoxSenderVerboseLogging.Checked,
                                                                               checkBoxSenderEnableStatistics.Checked,
                                                                               checkBoxSendBatch.Checked,
                                                                               senderBatchSize,
                                                                               checkBoxSenderThinkTime.Checked,
                                                                               senderThinkTime,
                                                                               bodyType,
                                                                               UpdateStatistics,
                                                                               senderCancellationTokenSource,
                                                                               out traceMessage);
                                            var builder = new StringBuilder(traceMessage);
                                            if (checkBoxSenderCommitTransaction.Checked)
                                            {
                                                scope.Complete();
                                                builder.AppendLine(TransactionCommitted);
                                            }
                                            else
                                            {
                                                builder.AppendLine(TransactionAborted);
                                            }
                                            traceMessage = builder.ToString();
                                        }
                                    }
                                    else
                                    {
                                        ok = serviceBusHelper.SendMessages(messageSenderCollection[taskId],
                                                                           messageTemplateEnumerable,
                                                                           getMessageNumber,
                                                                           messageCount,
                                                                           messageTextEnumerable,
                                                                           taskId,
                                                                           checkBoxUpdateMessageId.Checked,
                                                                           checkBoxAddMessageNumber.Checked,
                                                                           checkBoxOneSessionPerTask.Checked,
                                                                           checkBoxEnableSenderLogging.Checked,
                                                                           checkBoxSenderVerboseLogging.Checked,
                                                                           checkBoxSenderEnableStatistics.Checked,
                                                                           checkBoxSendBatch.Checked,
                                                                           senderBatchSize,
                                                                           checkBoxSenderThinkTime.Checked,
                                                                           senderThinkTime,
                                                                           bodyType,
                                                                           UpdateStatistics,
                                                                           senderCancellationTokenSource,
                                                                           out traceMessage);
                                    }
                                    if (!string.IsNullOrWhiteSpace(traceMessage))
                                    {
                                        writeToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                    }
                                    isSenderFaulted = !ok;
                                }
                                catch (Exception ex)
                                {
                                    isSenderFaulted = true;
                                    HandleException(ex);
                                }
                            };

                            // Define Sender AsyncCallback
                            AsyncCallback senderCallback = a =>
                            {
                                var action = a.AsyncState as Action<int, IEnumerable<BrokeredMessage>, IEnumerable<String>>;
                                if (action != null)
                                {
                                    action.EndInvoke(a);
                                    if (Interlocked.Decrement(ref actionCount) == 0)
                                    {
                                        managerResetEvent.Set();
                                    }
                                }
                            };

                            // Start Sender Actions
                            for (var i = 0; i < Math.Min(messageCount, senderTaskCount); i++)
                            {
                                senderAction.BeginInvoke(i, messageTemplateList, messageTextList, senderCallback, senderAction);
                                Interlocked.Increment(ref actionCount);
                            }
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }

                    //*****************************************************************************************************
                    //                                   Receiving messages from a Queue
                    //*****************************************************************************************************
                    if (receiverEnabledCheckBox.Checked)
                    {
                        var currentReceiveMode = cboReceivedMode.Text == PeekLock ?
                                                                         ReceiveMode.PeekLock :
                                                                         ReceiveMode.ReceiveAndDelete;
                        var currentMoveToDeadLetterQueue = checkBoxMoveToDeadLetter.Checked;
                        var currentReadFromDeadLetterQueue = checkBoxReadFromDeadLetter.Checked;

                        try
                        {
                            receiverCancellationTokenSource = new CancellationTokenSource();
                            Action<int> receiverAction = taskId =>
                            {
                                var allSessionsAccepted = false;

                                while (!allSessionsAccepted)
                                {
                                    try
                                    {
                                        MessageReceiver messageReceiver;
                                        if (currentReadFromDeadLetterQueue)
                                        {
                                            messageReceiver = messagingFactory.CreateMessageReceiver(QueueClient.FormatDeadLetterPath(queueDescription.Path),
                                                                                                     currentReceiveMode);
                                        }
                                        else
                                        {
                                            if (queueDescription.RequiresSession)
                                            {
                                                var queueClient = messagingFactory.CreateQueueClient(queueDescription.Path,
                                                                                                     currentReceiveMode);
                                                messageReceiver = queueClient.AcceptMessageSession(TimeSpan.FromSeconds(sessionTimeout));
                                            }
                                            else
                                            {
                                                messageReceiver = messagingFactory.CreateMessageReceiver(queueDescription.Path,
                                                                                                         currentReceiveMode);
                                            }
                                        }
                                        messageReceiver.PrefetchCount = prefetchCount;
                                        string traceMessage;
                                        if (checkBoxReceiverUseTransaction.Checked)
                                        {
                                            using (var scope = new TransactionScope())
                                            {
                                                serviceBusHelper.ReceiveMessages(messageReceiver,
                                                                                 taskId,
                                                                                 receiveTimeout,
                                                                                 filter,
                                                                                 currentMoveToDeadLetterQueue,
                                                                                 checkBoxCompleteReceive.Checked,
                                                                                 checkBoxDeferMessage.Checked,
                                                                                 checkBoxEnableReceiverLogging.Checked,
                                                                                 checkBoxReceiverVerboseLogging.Checked,
                                                                                 checkBoxReceiverEnableStatistics.Checked,
                                                                                 checkBoxReceiveBatch.Checked,
                                                                                 receiverBatchSize,
                                                                                 checkBoxReceiverThinkTime.Checked,
                                                                                 receiverThinkTime,
                                                                                 UpdateStatistics,
                                                                                 receiverCancellationTokenSource,
                                                                                 out traceMessage);
                                                var builder = new StringBuilder(traceMessage);
                                                if (checkBoxReceiverCommitTransaction.Checked)
                                                {
                                                    scope.Complete();
                                                    builder.AppendLine(TransactionCommitted);
                                                }
                                                else
                                                {
                                                    builder.AppendLine(TransactionAborted);
                                                }
                                                traceMessage = builder.ToString();
                                            }
                                        }
                                        else
                                        {
                                            serviceBusHelper.ReceiveMessages(messageReceiver,
                                                                             taskId,
                                                                             receiveTimeout,
                                                                             filter,
                                                                             currentMoveToDeadLetterQueue,
                                                                             checkBoxCompleteReceive.Checked,
                                                                             checkBoxDeferMessage.Checked,
                                                                             checkBoxEnableReceiverLogging.Checked,
                                                                             checkBoxReceiverVerboseLogging.Checked,
                                                                             checkBoxReceiverEnableStatistics.Checked,
                                                                             checkBoxReceiveBatch.Checked,
                                                                             receiverBatchSize,
                                                                             checkBoxReceiverThinkTime.Checked,
                                                                             receiverThinkTime,
                                                                             UpdateStatistics,
                                                                             receiverCancellationTokenSource,
                                                                             out traceMessage);
                                        }
                                        if (!string.IsNullOrWhiteSpace(traceMessage))
                                        {
                                            writeToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                        }
                                        allSessionsAccepted = !queueDescription.RequiresSession;
                                    }
                                    catch (TimeoutException ex)
                                    {
                                        if (queueDescription.RequiresSession)
                                        {
                                            writeToLog(string.Format(NoMoreSessionsToAccept, taskId));
                                            allSessionsAccepted = true;
                                        }
                                        else
                                        {
                                            HandleException(ex);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        HandleException(ex);
                                    }
                                }
                            };

                            // Define Receiver AsyncCallback
                            AsyncCallback receiverCallback = a =>
                            {
                                var action = a.AsyncState as Action<int>;
                                if (action != null)
                                {
                                    action.EndInvoke(a);
                                    if (Interlocked.Decrement(ref actionCount) == 0)
                                    {
                                        managerResetEvent.Set();
                                    }
                                }
                            };

                            // Start Receiver Actions
                            for (var i = 0; i < receiverTaskCount; i++)
                            {
                                receiverAction.BeginInvoke(i, receiverCallback, receiverAction);
                                Interlocked.Increment(ref actionCount);
                            }
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }
                    if (actionCount > 0)
                    {
                        managerCancellationTokenSource = new CancellationTokenSource();
                        managerAction.BeginInvoke(managerCancellationTokenSource, null, null);
                        graphCancellationTokenSource = new CancellationTokenSource();
                        updateGraphAction.BeginInvoke(updateGraphCallback, updateGraphAction);
                        Interlocked.Increment(ref actionCount);
                        btnStart.Text = StopCaption;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                btnStart.Enabled = true;
                Cursor.Current = Cursors.Default;
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

        private string GetMessageId()
        {
            if (checkBoxUpdateMessageId.Checked)
            {
                return Guid.NewGuid().ToString();
            }
            if (!string.IsNullOrWhiteSpace(txtMessageId.Text))
            {
                return txtMessageId.Text;
            }
            return Guid.NewGuid().ToString();
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

        internal void CancelActions()
        {
            if (managerCancellationTokenSource != null)
            {
                managerCancellationTokenSource.Cancel();
            }
            if (graphCancellationTokenSource != null)
            {
                graphCancellationTokenSource.Cancel();
            }
            if (senderCancellationTokenSource != null)
            {
                senderCancellationTokenSource.Cancel();
            }
            if (receiverCancellationTokenSource != null)
            {
                receiverCancellationTokenSource.Cancel();
            }
        }

        internal void btnCancel_Click(object sender, EventArgs e)
        {
            CancelActions();
            OnCancel();
        }

        private void mainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mainTabControl, e, null);
        }

        private void senderEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            grouperMessage.Enabled = senderEnabledCheckBox.Checked;
            grouperSender.Enabled = senderEnabledCheckBox.Checked;
            SetGraphLayout();
        }

        private void receiverEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            grouperReceiver.Enabled = receiverEnabledCheckBox.Checked;
            SetGraphLayout();
        }

        private void checkBoxEnableSenderLogging_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSenderVerboseLogging.Enabled = checkBoxEnableSenderLogging.Checked;
        }

        private void checkBoxEnableReceiverLogging_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxReceiverVerboseLogging.Enabled = checkBoxEnableReceiverLogging.Checked;
        }

        private void checkBoxSenderUseTransaction_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSenderCommitTransaction.Enabled = checkBoxSenderUseTransaction.Checked;
        }

        private void checkBoxReceiverUseTransaction_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxReceiverCommitTransaction.Enabled = checkBoxReceiverUseTransaction.Checked;
        }

        private void checkBoxMoveToDeadLetter_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxReadFromDeadLetter.Enabled = !checkBoxMoveToDeadLetter.Checked;
            checkBoxReadFromDeadLetter.Checked = false;
            checkBoxDeferMessage.Enabled = !checkBoxMoveToDeadLetter.Checked;
            checkBoxDeferMessage.Checked = false;
            if (checkBoxMoveToDeadLetter.Enabled)
            {
                cboReceivedMode.Text = PeekLock;
            }
        }

        private void checkBoxReadFromDeadLetter_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxMoveToDeadLetter.Enabled = !checkBoxReadFromDeadLetter.Checked;
            checkBoxMoveToDeadLetter.Checked = false;
            checkBoxDeferMessage.Enabled = !checkBoxReadFromDeadLetter.Checked;
            checkBoxDeferMessage.Checked = false;
        }

        private void checkBoxDeferMessage_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxMoveToDeadLetter.Enabled = !checkBoxDeferMessage.Checked;
            checkBoxMoveToDeadLetter.Checked = false;
            checkBoxReadFromDeadLetter.Enabled = !checkBoxDeferMessage.Checked;
            checkBoxReadFromDeadLetter.Checked = false;
        }

        private void cboReceivedMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxCompleteReceive.Enabled = cboReceivedMode.Text == PeekLock;
        }

        private void checkBoxSenderEnableStatistics_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSenderEnableGraph.Enabled = checkBoxSenderEnableStatistics.Checked;
        }

        private void checkBoxReceiverEnableStatistics_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxReceiverEnableGraph.Enabled = checkBoxReceiverEnableStatistics.Checked;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.FileName = string.Empty;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() != DialogResult.OK || 
                    string.IsNullOrWhiteSpace(openFileDialog.FileName) ||
                    !File.Exists(openFileDialog.FileName))
                {
                    return;
                }
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    var text = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return;
                    }
                    txtMessageText.Text = XmlHelper.Indent(text);
                    if (mainForm != null)
                    {
                        mainForm.MessageText = text;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
            {
                mainForm.Label = txtLabel.Text;
            }
        }

        /// <summary>
        /// Updates the statistics and graph on the control.
        /// </summary>
        /// <param name="messageNumber">Elapsed time.</param>
        /// <param name="elapsedMilliseconds">Elapsed time.</param>
        /// <param name="direction">The direction of the I/O operation: Send or Receive.</param>
        private void UpdateStatistics(long messageNumber, long elapsedMilliseconds, DirectionType direction)
        {
            blockingCollection.Add(new Tuple<long, long, DirectionType>(messageNumber, elapsedMilliseconds, direction));
        }

        /// <summary>
        /// Updates the statistics and graph on the control.
        /// </summary>
        /// <param name="messageNumber">Elapsed time.</param>
        /// <param name="elapsedMilliseconds">Elapsed time.</param>
        /// <param name="direction">The direction of the I/O operation: Send or Receive.</param>
        private void InternalUpdateStatistics(long messageNumber, long elapsedMilliseconds, DirectionType direction)
        {
            lock (this)
            {
                var elapsedSeconds = (double)elapsedMilliseconds / 1000;

                if (direction == DirectionType.Send)
                {
                    if (elapsedSeconds > senderMaximumTime)
                    {
                        senderMaximumTime = elapsedSeconds;
                    }
                    if (elapsedSeconds < senderMinimumTime)
                    {
                        senderMinimumTime = elapsedSeconds;
                    }
                    senderTotalTime += elapsedSeconds;
                    senderMessageNumber += messageNumber;
                    senderAverageTime = senderMessageNumber > 0 ? senderTotalTime / senderMessageNumber : 0;
                    senderMessagesPerSecond = senderTotalTime > 0 ? senderMessageNumber * senderTaskCount / senderTotalTime : 0;

                    lblSenderLastTime.Text = string.Format(LabelFormat, elapsedSeconds);
                    lblSenderLastTime.Refresh();
                    lblSenderAverageTime.Text = string.Format(LabelFormat, senderAverageTime);
                    lblSenderAverageTime.Refresh();
                    lblSenderMaximumTime.Text = string.Format(LabelFormat, senderMaximumTime);
                    lblSenderMaximumTime.Refresh();
                    lblSenderMinimumTime.Text = string.Format(LabelFormat, senderMinimumTime);
                    lblSenderMinimumTime.Refresh();
                    lblSenderMessagesPerSecond.Text = string.Format(LabelFormat, senderMessagesPerSecond);
                    lblSenderMessagesPerSecond.Refresh();
                    lblSenderMessageNumber.Text = senderMessageNumber.ToString(CultureInfo.InvariantCulture);
                    lblSenderMessageNumber.Refresh();

                    if (checkBoxSenderEnableGraph.Checked)
                    {
                        chart.Series["SenderLatency"].Points.AddXY(senderMessageNumber, elapsedSeconds);
                        chart.Series["SenderThroughput"].Points.AddXY(senderMessageNumber, senderMessagesPerSecond);
                    }
                }
                else
                {
                    if (elapsedSeconds > receiverMaximumTime)
                    {
                        receiverMaximumTime = elapsedSeconds;
                    }
                    if (elapsedSeconds < receiverMinimumTime)
                    {
                        receiverMinimumTime = elapsedSeconds;
                    }
                    receiverTotalTime += elapsedSeconds;
                    receiverMessageNumber += messageNumber;
                    receiverAverageTime = receiverMessageNumber > 0 ? receiverTotalTime / receiverMessageNumber : 0;
                    receiverMessagesPerSecond = receiverTotalTime > 0 ? receiverMessageNumber * receiverTaskCount / receiverTotalTime : 0;

                    lblReceiverLastTime.Text = string.Format(LabelFormat, elapsedSeconds);
                    lblReceiverLastTime.Refresh();
                    lblReceiverAverageTime.Text = string.Format(LabelFormat, receiverAverageTime);
                    lblReceiverAverageTime.Refresh();
                    lblReceiverMaximumTime.Text = string.Format(LabelFormat, receiverMaximumTime);
                    lblReceiverMaximumTime.Refresh();
                    lblReceiverMinimumTime.Text = string.Format(LabelFormat, receiverMinimumTime);
                    lblReceiverMinimumTime.Refresh();
                    lblReceiverMessagesPerSecond.Text = string.Format(LabelFormat, receiverMessagesPerSecond);
                    lblReceiverMessagesPerSecond.Refresh();
                    lblReceiverMessageNumber.Text = receiverMessageNumber.ToString(CultureInfo.InvariantCulture);
                    lblReceiverMessageNumber.Refresh();

                    if (checkBoxReceiverEnableGraph.Checked)
                    {
                        chart.Series["ReceiverLatency"].Points.AddXY(receiverMessageNumber, elapsedSeconds);
                        chart.Series["ReceiverThroughput"].Points.AddXY(receiverMessageNumber, receiverMessagesPerSecond);
                    }
                }
            }
        }

        private void txtMessageText_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMessageText.Text))
            {
                mainForm.MessageText = txtMessageText.Text;
            }
        }

        private void SetGraphLayout()
        {
            var text = string.Empty;
            chart.Series.Clear();
            if (!senderEnabledCheckBox.Checked &&
                !receiverEnabledCheckBox.Checked)
            {
                grouperSenderStatistics.Visible = false;
                grouperReceiverStatistics.Visible = false;
                chart.Visible = false;
                return;
            }

            if (senderEnabledCheckBox.Checked)
            {
                var series1 = new Series();
                var series3 = new Series();

                series1.BorderColor = Color.FromArgb(180, 26, 59, 105);
                series1.BorderWidth = 2;
                series1.ChartArea = "Default";
                series1.ChartType = SeriesChartType.FastLine;
                series1.Legend = "Default";
                series1.LegendText = "Sender Latency";
                series1.Name = "SenderLatency";

                series3.BorderWidth = 2;
                series3.ChartArea = "Default";
                series3.ChartType = SeriesChartType.FastLine;
                series3.Legend = "Default";
                series3.LegendText = "Sender Throughput";
                series3.Name = "SenderThroughput";

                chart.Series.Add(series1);
                chart.Series.Add(series3);
                chart.Visible = true;
                text = "Sender Performance Counters";
                grouperSenderStatistics.Visible = true;
            }
            else
            {
                grouperSenderStatistics.Visible = false;
                chart.Visible = true;
                chart.Size = new Size(tabPageGraph.Width - 160,
                                      tabPageGraph.Height - 22);
            }

            if (receiverEnabledCheckBox.Checked)
            {
                var series2 = new Series();
                var series4 = new Series();

                series2.BorderColor = Color.Red;
                series2.BorderWidth = 2;
                series2.ChartArea = "Default";
                series2.ChartType = SeriesChartType.FastLine;
                series2.Legend = "Default";
                series2.LegendText = "Receiver Latency";
                series2.Name = "ReceiverLatency";

                series4.BorderWidth = 2;
                series4.ChartArea = "Default";
                series4.ChartType = SeriesChartType.FastLine;
                series4.Legend = "Default";
                series4.LegendText = "Receiver Throughput";
                series4.Name = "ReceiverThroughput";

                chart.Series.Add(series2);
                chart.Series.Add(series4);
                chart.Visible = true;
                text = "Receiver Performance Counters";
                if (senderEnabledCheckBox.Checked)
                {
                    chart.Size = new Size(tabPageGraph.Width - 304,
                                          tabPageGraph.Height - 22);
                }
                grouperReceiverStatistics.Location = senderEnabledCheckBox.Checked ?
                                                   new Point(grouperSenderStatistics.Width + chart.Width + 32, 8) :
                                                   new Point(16, 8);
                grouperReceiverStatistics.Anchor = senderEnabledCheckBox.Checked
                                                     ? AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right
                                                     : AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
                grouperReceiverStatistics.Visible = true;
            }
            else
            {
                grouperReceiverStatistics.Visible = false;
                chart.Visible = true;
                chart.Size = new Size(tabPageGraph.Width - 160,
                                      tabPageGraph.Height - 22);
            }

            if (senderEnabledCheckBox.Checked && receiverEnabledCheckBox.Checked)
            {
                text = "Sender & Receiver Performance Counters";
            }

            var title = new Title
            {
                Font = new Font("Microsoft Sans Serif", 8.25F,
                                FontStyle.Regular,
                                GraphicsUnit.Point,
                                0),
                Name = "Title",
                ShadowColor = Color.Transparent,
                ShadowOffset = 1,
                Text = text
            };

            chart.Titles.Clear();
            chart.Titles.Add(title);
            tabPageGraph.Refresh();
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

        private void propertiesDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateLastColumnWidth();
        }

        private void propertiesDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateLastColumnWidth();
        }

        private void propertiesDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateLastColumnWidth();
        }

        private void checkBoxSendBatch_CheckedChanged(object sender, EventArgs e)
        {
            txtSendBatchSize.Enabled = checkBoxSendBatch.Checked;
        }

        private void checkBoxReceiveBatch_CheckedChanged(object sender, EventArgs e)
        {
            txtReceiveBatchSize.Enabled = checkBoxReceiveBatch.Checked;
            checkBoxMoveToDeadLetter.Enabled = !checkBoxReceiveBatch.Checked;
            checkBoxMoveToDeadLetter.Checked = false;
            checkBoxReadFromDeadLetter.Enabled = !checkBoxReceiveBatch.Checked;
            checkBoxReadFromDeadLetter.Checked = false;
            checkBoxDeferMessage.Enabled = !checkBoxReceiveBatch.Checked;
            checkBoxDeferMessage.Checked = false;
        }

        private void CalculateLastColumnWidth()
        {
            if (propertiesDataGridView.Columns.Count == 3)
            {
                var width = propertiesDataGridView.Width - propertiesDataGridView.Columns[0].Width -
                            propertiesDataGridView.Columns[1].Width - propertiesDataGridView.RowHeadersWidth;
                var verticalScrollbar = propertiesDataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                propertiesDataGridView.Columns[2].Width = width;
            }
        }

        private void TestQueueControl_Resize(object sender, EventArgs e)
        {
            grouperMessage.SuspendDrawing();
            grouperMessage.SuspendLayout();
            try
            {
                var textBoxWidth = (grouperMessage.Width - 216) / 2;
                lblSessionId.Location = new Point(104 + textBoxWidth, lblSessionId.Location.Y);
                lblCorrelationId.Location = new Point(lblSessionId.Location.X, lblCorrelationId.Location.Y);
                lblContentType.Location = new Point(lblSessionId.Location.X, lblContentType.Location.Y);
                lblReplyToSessionId.Location = new Point(lblSessionId.Location.X, lblReplyToSessionId.Location.Y);
                lblTimeToLive.Location = new Point(lblSessionId.Location.X, lblTimeToLive.Location.Y);
                txtMessageId.Size = new Size(textBoxWidth, txtMessageId.Size.Height);
                txtSessionId.Size = new Size(textBoxWidth, txtSessionId.Size.Height);
                txtTo.Size = new Size(textBoxWidth, txtTo.Size.Height);
                txtCorrelationId.Size = new Size(textBoxWidth, txtCorrelationId.Size.Height);
                txtReplyTo.Size = new Size(textBoxWidth, txtReplyTo.Size.Height);
                txtContentType.Size = new Size(textBoxWidth, txtContentType.Size.Height);
                txtLabel.Size = new Size(textBoxWidth, txtLabel.Size.Height);
                txtReplyToSessionId.Size = new Size(textBoxWidth, txtReplyToSessionId.Size.Height);
                txtScheduledEnqueueTimeUtc.Size = new Size(textBoxWidth, txtScheduledEnqueueTimeUtc.Size.Height);
                txtTimeToLive.Size = new Size(textBoxWidth, txtTimeToLive.Size.Height);
                txtSessionId.Location = new Point(textBoxWidth + 200, txtSessionId.Location.Y);
                txtCorrelationId.Location = new Point(txtSessionId.Location.X, txtCorrelationId.Location.Y);
                txtContentType.Location = new Point(textBoxWidth + 200, txtContentType.Location.Y);
                txtReplyToSessionId.Location = new Point(txtSessionId.Location.X, txtReplyToSessionId.Location.Y);
                txtTimeToLive.Location = new Point(textBoxWidth + 200, txtTimeToLive.Location.Y);
                
            }
            finally
            {
                grouperMessage.ResumeLayout();
                grouperMessage.ResumeDrawing();
            }
        }

        private void grouperSender_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   cboBodyType.Location.X - 1,
                                   cboBodyType.Location.Y - 1,
                                   cboBodyType.Size.Width + 1,
                                   cboBodyType.Size.Height + 1);
        }

        private void grouperReceiver_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboReceivedMode.Location.X - 1,
                                    cboReceivedMode.Location.Y - 1,
                                    cboReceivedMode.Size.Width + 1,
                                    cboReceivedMode.Size.Height + 1);
        }

        private void grouperMessageProperties_CustomPaint(PaintEventArgs e)
        {
            propertiesDataGridView.Size = new Size(grouperMessageProperties.Size.Width - 32,
                                                   grouperMessageProperties.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   propertiesDataGridView.Location.X - 1,
                                   propertiesDataGridView.Location.Y - 1,
                                   propertiesDataGridView.Size.Width + 1,
                                   propertiesDataGridView.Size.Height + 1);
        }
        
        private void checkBoxSenderThinkTime_CheckedChanged(object sender, EventArgs e)
        {
            txtSenderThinkTime.Enabled = checkBoxSenderThinkTime.Checked;
        }

        private void checkBoxReceiverThinkTime_CheckedChanged(object sender, EventArgs e)
        {
            txtReceiverThinkTime.Enabled = checkBoxReceiverThinkTime.Checked;
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
        
        private void messageTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(messageTabControl, e, null);
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = true;
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog() != DialogResult.OK ||
               !openFileDialog.FileNames.Any())
            {
                return;
            }
            foreach (var fileInfo in openFileDialog.FileNames.Select(fileName => new FileInfo(fileName)))
            {
                var size = string.Format("{0} KB", fileInfo.Length%1024 == 0
                                                       ? fileInfo.Length/1024
                                                       : fileInfo.Length/1024 + 1);
                messageFileListView.Items.Add(new ListViewItem(new[]
                    {
                        fileInfo.FullName, 
                        size
                    }));
                mainForm.FileNames.Add(new Tuple<string, string>(fileInfo.FullName, size));
            }
            btnClearFiles.Enabled = messageFileListView.Items.Count > 0;
        }

        private void messageFileListView_Resize(object sender, EventArgs e)
        {
            try
            {
                messageFileListView.SuspendDrawing();
                messageFileListView.SuspendLayout();
                var listView = sender as ListView;
                if (listView == null)
                {
                    return;
                }
                var width = listView.Width - listView.Columns[SizeListViewColumnIndex].Width;
                listView.Columns[NameListViewColumnIndex].Width = width - 4;
            }
            finally
            {
                messageFileListView.ResumeLayout();
                messageFileListView.ResumeDrawing();
            }
        }

        private void messageFileListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

        private void messageFileListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //e.DrawDefault = true;
            //e.DrawBackground();
            //e.DrawText();
        }

        private void messageFileListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            messageFileListView.Items.Clear();
            mainForm.FileNames.Clear();
            btnClearFiles.Enabled = false;
        }

        private void checkBoxFileName_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < messageFileListView.Items.Count; i++)
            {
                messageFileListView.Items[i].Checked = checkBoxFileName.Checked;
            }
        }
        
        private void grouperMessageFiles_CustomPaint(PaintEventArgs obj)
        {
            checkBoxFileName.Location = new Point(messageFileListView.Location.X + 8,
                                                  messageFileListView.Location.Y + 4);
        }

        private void checkBoxOneSessionPerTask_CheckedChanged(object sender, EventArgs e)
        {
            txtSessionId.Enabled = !checkBoxOneSessionPerTask.Checked;
        }
        #endregion
    }
}
