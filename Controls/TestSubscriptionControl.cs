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
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class TestSubscriptionControl : UserControl
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
        private const string DefaultReceiverTaskCount = "1";
        private const string PeekLock = "PeekLock";
        private const string StartCaption = "Start";
        private const string StopCaption = "Stop";
        private const string DefaultFilterExpression = "1=1";
        private const string DefaultReceiveTimeout = "1";
        private const string DefaultSessionTimeout = "3";
        private const string DefaultPrefetchCount = "0";
        private const string DefaulReceiveBatchSize = "10";

        //***************************
        // Messages
        //***************************
        private const string ReceiveTaskCountMustBeANumber = "The Receiver Task Count field must be an integer number greater than zero.";
        private const string ReceiveTimeoutCannotBeNull = "The receive timeout field cannot be null and must be a non negative number.";
        private const string SessionTimeoutCannotBeNull = "The session timeout field cannot be null and must be a non negative number.";
        private const string PrefetchCountCannotBeNull = "The prefetch count field cannot be null and must be an integer number.";
        private const string ReceiverBatchSizeMustBeANumber = "The Receiver Batch Size field must be an integer number greater than zero.";
        private const string ReceiverThinkTimeMustBeANumber = "The Receiver Think Time field must be an integer number greater than zero.";
        private const string TransactionCommitted = " - Transaction committed.";
        private const string TransactionAborted = " - Transaction aborted.";
        private const string NoMoreSessionsToAccept = "Receiver[{0}]: No more sessions to accept.";
        private const string FilterExpressionIsNotValid = "The filter expression is not valid.";
        private const string NoSubscriptionSelected = "No subscription has been selected.";
        private const string SelectBrokeredMessageInspector = "Select a BrokeredMessage inspector...";

        //***************************
        // Tooltips
        //***************************
        private const string ReceiveModeTooltip = "Gets or sets the receive mode of message receivers.";
        private const string ReceiverTaskCountTooltip = "Gets or sets the count of message receivers.";
        private const string ReceiveTimeoutTooltip = "Gets or sets the receive timeout.";
        private const string SessionTimeoutTooltip = "Gets or sets the session timeout.";
        private const string FilterExpressionTooltip = "Gets or sets the filter expression used to select messages to move to the dead-letter queue or to defer.";
        private const string EnableReceiverLoggingTooltip = "Enable logging of message content and properties for message receivers.";
        private const string EnableReceiverVerboseLoggingTooltip = "Enable verbose logging for message receivers.";
        private const string EnableReceiverTransactionTooltip = "Enable transactional behavior for message receivers.";
        private const string EnableReceiverCommitTooltip = "Enable transaction commit for message receivers.";
        private const string EnableMoveToDeadLetterTooltip = "When this option is enabled, all received messages are moved to the DeadLetter queue.";
        private const string EnableReadFromDeadLetterTooltip = "When this option is enabled, the receivers attempts to read messages from the DeadLetter queue.";
        #endregion

        #region Private Instance Fields
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly MainForm mainForm;
        private readonly WriteToLogDelegate writeToLog;
        private readonly Func<Task> stopLog;
        private readonly Action startLog;
        private readonly SubscriptionWrapper subscriptionWrapper;
        private int receiveTimeout = 60;
        private int sessionTimeout = 60;
        private int prefetchCount;
        private CancellationTokenSource receiverCancellationTokenSource;
        private CancellationTokenSource managerCancellationTokenSource;
        private CancellationTokenSource graphCancellationTokenSource;
        private ManualResetEventSlim managerResetEvent;
        private long receiverMessageNumber;
        private double receiverMessagesPerSecond;
        private double receiverMinimumTime;
        private double receiverMaximumTime;
        private double receiverAverageTime;
        private double receiverTotalTime;
        private int actionCount;
        private int receiverTaskCount = 1;
        private int receiverBatchSize = 1;
        private int receiverThinkTime;
        private Filter filter;
        private BlockingCollection<Tuple<long, long, DirectionType>> blockingCollection;
        private IBrokeredMessageInspector receiverBrokeredMessageInspector;
        #endregion

        #region Public Constructors
        public TestSubscriptionControl(MainForm mainForm,
                                       WriteToLogDelegate writeToLog,
                                       Func<Task> stopLog,
                                       Action startLog,
                                       ServiceBusHelper serviceBusHelper, 
                                       SubscriptionWrapper subscriptionWrapper)
        {
            this.mainForm = mainForm;
            this.writeToLog = writeToLog;
            this.stopLog = stopLog;
            this.startLog = startLog;
            this.serviceBusHelper = serviceBusHelper;
            this.subscriptionWrapper = subscriptionWrapper;
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
                // Get Brokered Message Generator and Inspector classes
                cboReceiverInspector.Items.Add(SelectBrokeredMessageInspector);
                cboReceiverInspector.SelectedIndex = 0;

                if (serviceBusHelper != null)
                {
                    if (serviceBusHelper.BrokeredMessageInspectors != null)
                    {
                        foreach (var key in serviceBusHelper.BrokeredMessageInspectors.Keys)
                        {
                            cboReceiverInspector.Items.Add(key);
                        }
                    }
                }

                // Set Think Time
                txtReceiverThinkTime.Text = mainForm.ReceiverThinkTime.ToString(CultureInfo.InvariantCulture);
                receiverThinkTime = mainForm.ReceiverThinkTime;

                // Set Task Count
                txtReceiveTaskCount.Text = DefaultReceiverTaskCount;
                cboReceivedMode.SelectedIndex = 1;
                txtReceiveTimeout.Text = mainForm != null ?
                                         mainForm.ReceiveTimeout.ToString(CultureInfo.InvariantCulture) :
                                         DefaultReceiveTimeout;
                txtServerTimeout.Text = mainForm != null ?
                                         mainForm.ServerTimeout.ToString(CultureInfo.InvariantCulture) :
                                         DefaultSessionTimeout;
                txtPrefetchCount.Text = mainForm != null ?
                                        mainForm.PrefetchCount.ToString(CultureInfo.InvariantCulture) :
                                        DefaultPrefetchCount;
                txtReceiveBatchSize.Text = DefaulReceiveBatchSize;
                txtReceiveBatchSize.Enabled = false;

                // Create Tooltips for controls
                toolTip.SetToolTip(txtReceiveTaskCount, ReceiverTaskCountTooltip);
                toolTip.SetToolTip(txtReceiveTimeout, ReceiveTimeoutTooltip);
                toolTip.SetToolTip(txtServerTimeout, SessionTimeoutTooltip);
                toolTip.SetToolTip(txtFilterExpression, FilterExpressionTooltip);
                toolTip.SetToolTip(checkBoxEnableReceiverLogging, EnableReceiverLoggingTooltip);
                toolTip.SetToolTip(checkBoxReceiverVerboseLogging, EnableReceiverVerboseLoggingTooltip);
                toolTip.SetToolTip(checkBoxReceiverUseTransaction, EnableReceiverTransactionTooltip);
                toolTip.SetToolTip(checkBoxReceiverCommitTransaction, EnableReceiverCommitTooltip);
                toolTip.SetToolTip(checkBoxMoveToDeadLetter, EnableMoveToDeadLetterTooltip);
                toolTip.SetToolTip(checkBoxReadFromDeadLetter, EnableReadFromDeadLetterTooltip);
                toolTip.SetToolTip(cboReceivedMode, ReceiveModeTooltip);
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
                int temp;
                if (string.IsNullOrWhiteSpace(txtReceiveTimeout.Text) ||
                    !int.TryParse(txtReceiveTimeout.Text, out temp) ||
                    temp < 0)
                {
                    writeToLog(ReceiveTimeoutCannotBeNull);
                    return false;
                }
                receiveTimeout = temp;
                if (string.IsNullOrWhiteSpace(txtServerTimeout.Text) ||
                    !int.TryParse(txtServerTimeout.Text, out temp) ||
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
                if (!int.TryParse(txtReceiveBatchSize.Text, out temp) || temp <= 0)
                {
                    writeToLog(ReceiverBatchSizeMustBeANumber);
                    return false;
                }
                receiverBatchSize = temp;
                if (!int.TryParse(txtReceiverThinkTime.Text, out temp) || temp <= 0)
                {
                    writeToLog(ReceiverThinkTimeMustBeANumber);
                    return false;
                }
                receiverThinkTime = temp;
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

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStart.Text == StopCaption)
                {
                    await CancelActions();
                    btnStart.Text = StartCaption;
                    return;
                }

                if (serviceBusHelper != null &&
                    ValidateParameters())
                {
                    if (startLog != null)
                    {
                        startLog();
                    }
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
                    receiverMessageNumber = 0;
                    receiverMessagesPerSecond = 0;
                    receiverMinimumTime = long.MaxValue;
                    receiverMaximumTime = 0;
                    receiverAverageTime = 0;
                    receiverTotalTime = 0;
                    if (checkBoxReceiverEnableGraph.Checked)
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
                            long receiveMessageNumber = 0;
                            long receiveTotalTime = 0;
                            while (ok && receiveMessageNumber < max)
                            {
                                Tuple<long, long, DirectionType> tuple;
                                ok = blockingCollection.TryTake(out tuple, 10);
                                if (ok)
                                {
                                    receiveMessageNumber += tuple.Item1;
                                    receiveTotalTime += tuple.Item2;
                                    if (receiveMessageNumber > max)
                                    {
                                        max = receiveMessageNumber;
                                    }
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
                    //                                   Receiving messages from a Subscription
                    //*****************************************************************************************************
                    var currentSubscription = subscriptionWrapper.SubscriptionDescription;
                    if (currentSubscription == null)
                    {
                        throw new ArgumentException(NoSubscriptionSelected);
                    }
                    var currentReceiveMode = cboReceivedMode.Text == PeekLock ?
                                                                     ReceiveMode.PeekLock :
                                                                     ReceiveMode.ReceiveAndDelete;
                    var currentMoveToDeadLetterQueue = checkBoxMoveToDeadLetter.Checked;
                    var currentReadFromDeadLetterQueue = checkBoxReadFromDeadLetter.Checked;

                    try
                    {
                        receiverCancellationTokenSource = new CancellationTokenSource();
                        receiverCancellationTokenSource = new CancellationTokenSource();
                        receiverBrokeredMessageInspector = cboReceiverInspector.SelectedIndex > 0
                                                      ? Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[cboReceiverInspector.Text]) as IBrokeredMessageInspector
                                                      : null;

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
                                        messageReceiver =
                                            messagingFactory.CreateMessageReceiver(SubscriptionClient.FormatDeadLetterPath(currentSubscription.TopicPath,
                                                                                                                           currentSubscription.Name),
                                                                                   currentReceiveMode);
                                    }
                                    else
                                    {
                                        if (currentSubscription.RequiresSession)
                                        {
                                            var subscriptionClient = messagingFactory.CreateSubscriptionClient(currentSubscription.TopicPath,
                                                                                                               currentSubscription.Name,
                                                                                                               currentReceiveMode);
                                            messageReceiver = subscriptionClient.AcceptMessageSession(TimeSpan.FromSeconds(sessionTimeout));
                                        }
                                        else
                                        {
                                            messageReceiver = messagingFactory.CreateMessageReceiver(SubscriptionClient.FormatSubscriptionPath(currentSubscription.TopicPath,
                                                                                                                                               currentSubscription.Name),
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
                                                                             receiverBrokeredMessageInspector,
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
                                                                         receiverBrokeredMessageInspector,
                                                                         UpdateStatistics,
                                                                         receiverCancellationTokenSource,
                                                                         out traceMessage);
                                    }
                                    if (!string.IsNullOrWhiteSpace(traceMessage))
                                    {
                                        writeToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                    }
                                    allSessionsAccepted = !currentSubscription.RequiresSession;
                                }
                                catch (TimeoutException ex)
                                {
                                    if (currentSubscription.RequiresSession)
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

        internal async Task CancelActions()
        {
            if (stopLog != null)
            {
                await stopLog();
            }
            if (managerCancellationTokenSource != null)
            {
                managerCancellationTokenSource.Cancel();
            }
            if (graphCancellationTokenSource != null)
            {
                graphCancellationTokenSource.Cancel();
            }
            if (receiverCancellationTokenSource != null)
            {
                receiverCancellationTokenSource.Cancel();
            }
        }

        internal async void btnCancel_Click(object sender, EventArgs e)
        {
            await CancelActions();
            OnCancel();
        }

        private void mainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mainTabControl, e, null);
        }

        private void checkBoxEnableReceiverLogging_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxReceiverVerboseLogging.Enabled = checkBoxEnableReceiverLogging.Checked;
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

        private void checkBoxReceiverEnableStatistics_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxReceiverEnableGraph.Enabled = checkBoxReceiverEnableStatistics.Checked;
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
                var elapsedSeconds = (double) elapsedMilliseconds/1000;

                if (direction == DirectionType.Receive)
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
                    receiverAverageTime = receiverMessageNumber > 0 ? receiverTotalTime/receiverMessageNumber : 0;
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
        
        private void grouperReceiver_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboReceivedMode.Location.X - 1,
                                    cboReceivedMode.Location.Y - 1,
                                    cboReceivedMode.Size.Width + 1,
                                    cboReceivedMode.Size.Height + 1);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboReceiverInspector.Location.X - 1,
                                    cboReceiverInspector.Location.Y - 1,
                                    cboReceiverInspector.Size.Width + 1,
                                    cboReceiverInspector.Size.Height + 1);
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

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }

                if (receiverCancellationTokenSource != null)
                {
                    receiverCancellationTokenSource.Dispose();
                }

                if (managerCancellationTokenSource != null)
                {
                    managerCancellationTokenSource.Dispose();
                }

                if (graphCancellationTokenSource != null)
                {
                    graphCancellationTokenSource.Dispose();
                }

                if (managerResetEvent != null)
                {
                    managerResetEvent.Dispose();
                }

                if (blockingCollection != null)
                {
                    blockingCollection.Dispose();
                }

                if (receiverBrokeredMessageInspector != null)
                {
                    var disposable = receiverBrokeredMessageInspector as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
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
        #endregion
    }
}
