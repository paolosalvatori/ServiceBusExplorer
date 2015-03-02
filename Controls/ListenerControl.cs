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
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class ListenerControl : UserControl
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
        private const string LabelFormat = "{0:0.0000}";

        //***************************
        // Texts
        //***************************
        private const string MessageId = "MessageId";
        private const string MessageSize = "Size";
        private const string Label = "Label";
        private const string Start = "Start";
        private const string Stop = "Stop";
        private const string NullValue = "NULL";
        private const string MaxConcurrentSessions = "Max Concurrent Sessions:";

        //***************************
        // Messages
        //***************************
        private const string SelectEntityDialogTitle = "Select a target Queue or Topic";
        private const string SelectEntityGrouperTitle = "Forward To";
        private const string SelectEntityLabelText = "Target Queue or Topic:";
        private const string DoubleClickMessage = "Double-click a row to repair and resubmit the corresponding message.";
        private const string MessageSentMessage = "[{0}] messages where sent to [{1}]";
        private const string FilterExpressionTitle = "Define Filter Expression";
        private const string FilterExpressionLabel = "Filter Expression";
        private const string FilterExpressionNotValidMessage = "The filter expression [{0}] is not valid: {1}";
        private const string FilterExpressionAppliedMessage = "The filter expression [{0}] has been successfully applied. [{1}] messages retrieved.";
        private const string FilterExpressionRemovedMessage = "The filter expression has been removed.";
        private const string MessageSuccessfullyReceived = "Message received. MessageId=[{0}] SessionId=[{1}] Label=[{2}] Size=[{3}]";
        private const string SelectBrokeredMessageInspector = "Select a BrokeredMessage inspector...";

        //***************************
        // Property Labels
        //***************************
        private const string MessageCount = "Message Count";
        private const string ActiveMessageCount = "Active Message Count";
        private const string DeadletterCount = "DeadLetter Message Count";
        private const string ScheduledMessageCount = "Scheduled Message Count";
        private const string TransferMessageCount = "Transfer Message Count";
        private const string TransferDeadLetterMessageCount = "Transfer DL Message Count";

        //***************************
        // Tooltips
        //***************************
        private const string MaxConcurrentCallsTooltip = "Gets or sets the maximum number of concurrent calls to the callback the message pump should initiate.";
        private const string MaxConcurrentSessionsTooltip = "Gets or sets the maximum number of existing sessions.";
        private const string RefreshTimeoutTooltip = "Gets or sets the refresh timeout for the information shown in the Partition Information panel.";
        private const string PrefetchCountTooltip = "Gets or sets the prefetch count for used by the listener to receive events from a partition.";
        private const string ReceiveModeTooltip = "Gets or sets the message receive mode.";
        private const string AutoRenewTimeoutTooltip = "Gets or sets the time needed before the session renew its state.";
        private const string MessageWaitTimeoutTooltip = "Gets or sets the time needed before the message waiting expires.";
        private const string AutoCompleteTooltip = "Gets or sets a value that indicates whether the message-pump should call Complete or Complete on messages after the callback has completed processing";
        private const string LoggingTooltip = "Enable or disable logging. You can change this setting before or after the listener is started.";
        private const string VerboseTooltip = "Enable or disable verbose logging. You can change this setting before or after the listener is started.";
        private const string TrackingTooltip = "Enable or disable message tracking. You can change this setting before or after the listener is started.";
        private const string GraphTooltip = "Enable or disable graph. You can change this setting before or after the listener is started.";
        private const string MessagesTotalTooltip = "The total number of messages received from the partition(s).";
        private const string MessagesPerSecondTooltip = "The average number of messages received from the partition(s) per second .";
        private const string AverageDurationTooltip = "The average duration of receive operation in seconds.";
        private const string KbPerSecTooltip = "The average number of KB received per second.";
        private const string ScaleTooltip = "Select a scale to enhance the visibility of counter data in the chart.";

        //***************************
        // Constants
        //***************************
        private const string SaveAsTitle = "Save File As";
        private const string JsonExtension = "json";
        private const string JsonFilter = "JSON Files|*.json|Text Documents|*.txt";
        private const string MessageFileFormat = "BrokeredMessage_{0}_{1}.json";
        #endregion

        #region Private Fields
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly Func<Task> stopLog;
        private readonly Action startLog;
        private BrokeredMessage brokeredMessage;
        private int grouperMessageCustomPropertiesWidth;
        private int currentMessageRowIndex;
        private bool sorting;
        private string messagesFilterExpression;
        private readonly SortableBindingList<BrokeredMessage> messageBindingList = new SortableBindingList<BrokeredMessage> { AllowNew = false, AllowEdit = false, AllowRemove = false };
        private EntityDescription entityDescription;
        private QueueClient queueClient;
        private SubscriptionClient subscriptionClient;
        private readonly MessageEncoder encoder;
        private System.Timers.Timer timer;
        private long receiverMessageNumber;
        private long receiverMessageSizeTotal;
        private double receiverMessageSizePerSecond;
        private double receiverMessagesPerSecond;
        private double receiverMinimumTime;
        private double receiverMaximumTime;
        private double receiverTotalTime;
        private double receiverAverageTime;
        private double averageTimeScale;
        private double eventDataPerSecondScale;
        private double messageSizePerSecondScale;
        private BlockingCollection<Tuple<long, long, long>> blockingCollection;
        private readonly BlockingCollection<BrokeredMessage> messageCollection = new BlockingCollection<BrokeredMessage>();
        private CancellationTokenSource cancellationTokenSource;
        private DateTime dateTime = DateTime.MinValue;
        private Control parentForm;
        private ContainerForm containerForm;
        private bool autoComplete;
        private bool stopping;
        private bool logging;
        private bool verbose;
        private bool tracking;
        private bool graph;
        private ReceiveMode receiveMode;
        private IBrokeredMessageInspector receiverBrokeredMessageInspector;
        #endregion

        #region Public Constructors
        public ListenerControl()
        {
        }

        public ListenerControl(WriteToLogDelegate writeToLog,
                               Func<Task> stopLog,
                               Action startLog,
                               ServiceBusHelper serviceBusHelper, 
                               EntityDescription entityDescription)
        {
            Task.Factory.StartNew(AsyncTrackMessage).ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    writeToLog(t.Exception.Message);
                }
            });
            this.writeToLog = writeToLog;
            this.stopLog = stopLog;
            this.startLog = startLog;
            this.serviceBusHelper = serviceBusHelper;
            this.entityDescription = entityDescription;
            var element = new BinaryMessageEncodingBindingElement();
            var encoderFactory = element.CreateMessageEncoderFactory();
            encoder = encoderFactory.Encoder;
            InitializeComponent();
            InitializeControls();
            Disposed += ListenerControl_Disposed;
            if (entityDescription is SubscriptionDescription)
            {
                grouperEntityInformation.GroupTitle = "Subscription Information";
            }
        }
        #endregion

        #region Private Methods
        void ListenerControl_Disposed(object sender, EventArgs e)
        {
            StopListener().Wait();
        }

        private bool RequiresSession()
        {
            var description = entityDescription as QueueDescription;
            if (description != null )
            {
                return description.RequiresSession;
            }
            var subscriptionDescription = entityDescription as SubscriptionDescription;
            if (subscriptionDescription != null)
            {
                return subscriptionDescription.RequiresSession;
            }
            return false;
        }

        private void InitializeControls()
        {
            var requiresSession = RequiresSession();
            // Set lblMaxConcurrentCalls value
            if (requiresSession)
            {
                lblMaxConcurrentCalls.Text = MaxConcurrentSessions;
                toolTip.SetToolTip(txtMaxConcurrentCalls, MaxConcurrentSessionsTooltip);
            }

            // Add data to scale combobox
            object[] scaleValues = { (double)1000, (double)100, (double)10, (double)1, 0.1, 0.01, 0.001 };
            cboAverageDuration.Items.AddRange(scaleValues);
            cboMessagesPerSecond.Items.AddRange(scaleValues);
            cboMessageSizePerSecond.Items.AddRange(scaleValues);
            cboAverageDuration.SelectedIndex = 3;
            cboMessagesPerSecond.SelectedIndex = 3;
            cboMessageSizePerSecond.SelectedIndex = 3;

            // Set default Receive Mode
            cboReceivedMode.SelectedIndex = requiresSession? 1 : 0;

            // Hide caret
            txtMessagesPerSecond.GotFocus += textBox_GotFocus;
            txtMessagesTotal.GotFocus += textBox_GotFocus;
            txtAverageDuration.GotFocus += textBox_GotFocus;
            txtMessageSizePerSecond.GotFocus += textBox_GotFocus;

            // Splitter controls
            messagesSplitContainer.SplitterWidth = 16;
            messagesCustomPropertiesSplitContainer.SplitterWidth = 16;
            messageListTextPropertiesSplitContainer.SplitterWidth = 8;

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

            // Set DataGridView DataSource
            messagesBindingSource.DataSource = messageBindingList;
            messagesDataGridView.DataSource = messagesBindingSource;

            // Set Grouper width
            grouperMessageCustomPropertiesWidth = grouperMessageCustomProperties.Size.Width;

            // Refresh data
            timer_Elapsed(null, null);

            // Set Tooltips
            toolTip.SetToolTip(txtMaxConcurrentCalls, MaxConcurrentCallsTooltip);
            toolTip.SetToolTip(txtRefreshInformation, RefreshTimeoutTooltip);
            toolTip.SetToolTip(txtPrefetchCount, PrefetchCountTooltip);
            toolTip.SetToolTip(cboReceivedMode, ReceiveModeTooltip);
            toolTip.SetToolTip(txtAutoRenewTimeout, AutoRenewTimeoutTooltip);
            toolTip.SetToolTip(txtMessageWaitTimeout, MessageWaitTimeoutTooltip);
            
            toolTip.SetToolTip(checkBoxAutoComplete, AutoCompleteTooltip);
            toolTip.SetToolTip(checkBoxLogging, LoggingTooltip);
            toolTip.SetToolTip(checkBoxVerbose, VerboseTooltip);
            toolTip.SetToolTip(checkBoxTrackMessages, TrackingTooltip);
            toolTip.SetToolTip(checkBoxGraph, GraphTooltip);

            toolTip.SetToolTip(txtMessagesTotal, MessagesTotalTooltip);
            toolTip.SetToolTip(txtMessagesPerSecond, MessagesPerSecondTooltip);
            toolTip.SetToolTip(txtAverageDuration, AverageDurationTooltip);
            toolTip.SetToolTip(txtMessageSizePerSecond, KbPerSecTooltip);

            toolTip.SetToolTip(cboMessagesPerSecond, ScaleTooltip);
            toolTip.SetToolTip(cboAverageDuration, ScaleTooltip);
            toolTip.SetToolTip(cboMessageSizePerSecond, ScaleTooltip);

            propertyListView.ContextMenuStrip = entityInformationContextMenuStrip;

            cboReceiverInspector.Items.Add(SelectBrokeredMessageInspector);
            cboReceiverInspector.SelectedIndex = 0;

            if (serviceBusHelper == null || serviceBusHelper.BrokeredMessageInspectors == null)
            {
                return;
            }

            foreach (var key in serviceBusHelper.BrokeredMessageInspectors.Keys)
            {
                cboReceiverInspector.Items.Add(key);
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
                if (listView.Columns.Count == 0)
                {
                    return;
                }
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
                var width = dataGridView.Width - dataGridView.RowHeadersWidth;
                var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                var columnWidth = width / 3;
                dataGridView.Columns[0].Width = columnWidth;
                dataGridView.Columns[1].Width = columnWidth;
                dataGridView.Columns[2].Width = columnWidth + (width - (columnWidth * 3));
            }
            finally
            {
                dataGridView.ResumeLayout();
                dataGridView.ResumeDrawing();
            }
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
            var listViewItems = brokeredMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, (p.Value ?? string.Empty).ToString() })).ToArray();
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

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void messagesDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = messagesDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
                    serviceBusHelper.GetMessageText(message, out bodyType);
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
                                                                            wcfUri);
                    }
                    return serviceBusHelper.CreateMessageForApiReceiver(message,
                                                                        0,
                                                                        false,
                                                                        false,
                                                                        false,
                                                                        bodyType,
                                                                        null);
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

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (string.Compare(btnStart.Text, Start, StringComparison.OrdinalIgnoreCase) == 0)
            {
                receiverBrokeredMessageInspector = cboReceiverInspector.SelectedIndex > 0
                                           ? Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[cboReceiverInspector.Text]) as IBrokeredMessageInspector
                                           : null;

                cancellationTokenSource = new CancellationTokenSource();
                btnStart.Text = Stop;
                blockingCollection = new BlockingCollection<Tuple<long, long, long>>();
                timer = new System.Timers.Timer
                {
                    AutoReset = true,
                    Enabled = true,
                    Interval = 1000 * txtRefreshInformation.IntegerValue
                };
                if (startLog != null && checkBoxLogging.Checked)
                {
                    startLog();
                }
                timer.Elapsed += timer_Elapsed;
                autoComplete = checkBoxAutoComplete.Checked;
                logging = checkBoxLogging.Checked;
                verbose = checkBoxVerbose.Checked;
                tracking = checkBoxTrackMessages.Checked;
                try
                {
                    receiveMode = cboReceivedMode.SelectedIndex == 1 ? ReceiveMode.PeekLock : ReceiveMode.ReceiveAndDelete;

                    if (entityDescription is QueueDescription)
                    {
                        var currentQueue = entityDescription as QueueDescription;
                        queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(currentQueue.Path, receiveMode);
                        queueClient.PrefetchCount = txtPrefetchCount.IntegerValue;
                        if (currentQueue.RequiresSession)
                        {
                           await queueClient.RegisterSessionHandlerFactoryAsync(
                                    new CustomMessageSessionAsyncHandlerFactory(new CustomMessageSessionAsyncHandlerConfiguration
                                    {
                                        Logging = logging,
                                        Tracking = tracking,
                                        AutoComplete = autoComplete,
                                        MessageEncoder = encoder,
                                        ReceiveMode = receiveMode,
                                        GetElapsedTime = GetElapsedTime,
                                        UpdateStatistics = UpdateStatistics,
                                        WriteToLog = writeToLog,
                                        MessageInspector = receiverBrokeredMessageInspector,
                                        ServiceBusHelper = serviceBusHelper,
                                        TrackMessage = bm => Invoke(new Action<BrokeredMessage>(m => messageCollection.Add(m)), bm)
                                    }), GetSessionHandlerOptions());
                        }
                        else
                        {
                            #pragma warning disable 4014
                            Task.Run(() => queueClient.OnMessageAsync(OnMessageAsync, GetOnMessageOptions())); 
                            #pragma warning restore 4014
                        }
                    }

                    if (entityDescription is SubscriptionDescription)
                    {
                        var currentSubscription = entityDescription as SubscriptionDescription;
                        subscriptionClient = serviceBusHelper.MessagingFactory.CreateSubscriptionClient(currentSubscription.TopicPath,
                                                                                                        currentSubscription.Name,
                                                                                                        receiveMode);
                        subscriptionClient.PrefetchCount = txtPrefetchCount.IntegerValue;
                        if (currentSubscription.RequiresSession)
                        {
                            await subscriptionClient.RegisterSessionHandlerFactoryAsync(
                                    new CustomMessageSessionAsyncHandlerFactory(new CustomMessageSessionAsyncHandlerConfiguration
                                    {
                                        Logging = logging,
                                        Tracking = tracking,
                                        AutoComplete = autoComplete,
                                        MessageEncoder = encoder,
                                        ReceiveMode = receiveMode,
                                        GetElapsedTime = GetElapsedTime,
                                        UpdateStatistics = UpdateStatistics,
                                        WriteToLog = writeToLog,
                                        ServiceBusHelper = serviceBusHelper,
                                        TrackMessage = bm => Invoke(new Action<BrokeredMessage>(m => messageCollection.Add(m)), bm)
                                    }), GetSessionHandlerOptions());
                        }
                        else
                        {
                            #pragma warning disable 4014
                            Task.Run(() => subscriptionClient.OnMessageAsync(OnMessageAsync, GetOnMessageOptions())); 
                            #pragma warning restore 4014
                        }
                    }
                    #pragma warning disable 4014
                    Task.Run(new Action(RefreshGraph));          
                    #pragma warning restore 4014
                    GetElapsedTime();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    StopListener().Wait();
                    btnStart.Text = Start;
                }
            }
            else
            {
                try
                {
                    await StopListener();
                    btnStart.Text = Start;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        private OnMessageOptions GetOnMessageOptions()
        {
            // Initialize message pump options
            var options = new OnMessageOptions
            {
                // Indicates if the message-pump should call complete on messages after the callback has completed processing.
                AutoComplete = checkBoxAutoComplete.Checked,
                // Indicates the maximum number of concurrent calls to the callback the pump should initiate 
                MaxConcurrentCalls = txtMaxConcurrentCalls.IntegerValue,
                AutoRenewTimeout = TimeSpan.FromMinutes(1)
            };
            // Allows to get notified of any errors encountered by the message pump
            options.ExceptionReceived += LogErrors;
            return options;
        }

        private SessionHandlerOptions GetSessionHandlerOptions()
        {
            // Initialize message pump options
            var options = new SessionHandlerOptions
            {
                // Indicates if the message-pump should call complete on messages after the callback has completed processing.
                AutoComplete = checkBoxAutoComplete.Checked,
                // Gets or sets the maximum number of existing sessions.
                MaxConcurrentSessions = txtMaxConcurrentCalls.IntegerValue,
                // Gets or sets the time needed before the session renew its state.
                AutoRenewTimeout = TimeSpan.FromSeconds(30),
                // Gets or sets the time needed before the message waiting expires.
                MessageWaitTimeout  = TimeSpan.FromSeconds(30)
            };
            // Allows to get notified of any errors encountered by the message pump
            options.ExceptionReceived += LogErrors;
            return options;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                messageBindingList.Clear();
                if (containerForm != null)
                {
                    containerForm.Clear();
                }
                txtMessageText.Text = null;
                messagePropertyListView.Clear();
                messagePropertyGrid.SelectedObject = null;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                await StopListener();
                if (parentForm != null)
                {
                    ((Form)parentForm).Close();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void grouperStatistics_CustomPaint(PaintEventArgs e)
        {
            var width = (grouperStatistics.Size.Width - 48) / 2;

            txtMessagesTotal.AutoSize = false;
            txtMessagesTotal.Size = new Size(width, cboMessagesPerSecond.Size.Height + 2);

            txtMessagesPerSecond.AutoSize = false;
            txtMessagesPerSecond.Size = new Size(width - cboMessagesPerSecond.Size.Width, cboMessagesPerSecond.Size.Height + 2);

            txtAverageDuration.AutoSize = false;
            txtAverageDuration.Size = new Size(width - cboAverageDuration.Size.Width, cboAverageDuration.Size.Height + 2);

            txtMessageSizePerSecond.AutoSize = false;
            txtMessageSizePerSecond.Size = new Size(width - cboMessageSizePerSecond.Size.Width, cboMessageSizePerSecond.Size.Height + 2);


            txtMessagesPerSecond.Location = new Point(width + 32, txtMessagesPerSecond.Location.Y);
            lblMessagesPerSecond.Location = new Point(width + 32, lblMessagesTotal.Location.Y);
            cboMessagesPerSecond.Location = new Point(2 * width + 32 - cboMessagesPerSecond.Size.Width + 1, cboMessagesPerSecond.Location.Y);

            cboAverageDuration.Location = new Point(width + 16 - cboAverageDuration.Size.Width + 1, cboAverageDuration.Location.Y);

            txtMessageSizePerSecond.Location = new Point(width + 32, txtMessageSizePerSecond.Location.Y);
            lblMessageSizePerSecond.Location = new Point(width + 32, lblMessageSizePerSecond.Location.Y);
            cboMessageSizePerSecond.Location = new Point(2 * width + 32 - cboMessageSizePerSecond.Size.Width + 1, cboMessageSizePerSecond.Location.Y);

            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboMessagesPerSecond.Location.X - 1,
                                    cboMessagesPerSecond.Location.Y - 1,
                                    cboMessagesPerSecond.Size.Width + 1,
                                    cboMessagesPerSecond.Size.Height + 1);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboAverageDuration.Location.X - 1,
                                    cboAverageDuration.Location.Y - 1,
                                    cboAverageDuration.Size.Width + 1,
                                    cboAverageDuration.Size.Height + 1);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboMessageSizePerSecond.Location.X - 1,
                                    cboMessageSizePerSecond.Location.Y - 1,
                                    cboMessageSizePerSecond.Size.Width + 1,
                                    cboMessageSizePerSecond.Size.Height + 1);
        }

        private void grouperOptions_Resize(object sender, EventArgs e)
        {
            var requiresSession = RequiresSession();
            if (requiresSession)
            {
                grouperOptions.Size = new Size(tabPageListener.Size.Width - 32, grouperOptions.Size.Height);
                grouperEntityInformation.Size = new Size(grouperEntityInformation.Size.Width, tabPageListener.Size.Height - grouperStatistics.Size.Height - grouperOptions.Size.Height - 40);
                grouperEntityInformation.Location = new Point(640, 136);
            }
            var width = requiresSession ? (grouperOptions.Size.Width - 112) / 6 : (grouperOptions.Size.Width - 80) / 4; 
            txtMaxConcurrentCalls.Size = new Size(width, txtMaxConcurrentCalls.Size.Height);
            txtRefreshInformation.Size = new Size(width, txtRefreshInformation.Size.Height);
            txtPrefetchCount.Size = new Size(width, txtPrefetchCount.Size.Height);
            cboReceivedMode.Size = new Size(width, cboReceivedMode.Size.Height);
            txtRefreshInformation.Location = new Point(width + 32, txtRefreshInformation.Location.Y);
            lblRefreshInformation.Location = new Point(width + 32, lblRefreshInformation.Location.Y);
            txtPrefetchCount.Location = new Point(2 * width + 48, cboReceivedMode.Location.Y);
            lblPrefetchCount.Location = new Point(2 * width + 48, lblReceiveMode.Location.Y);
            cboReceivedMode.Location = new Point(3 * width + 64, cboReceivedMode.Location.Y);
            lblReceiveMode.Location = new Point(3 * width + 64, lblReceiveMode.Location.Y);
            checkBoxLogging.Location = new Point(width + 32, checkBoxLogging.Location.Y);
            checkBoxVerbose.Location = new Point(2 * width + 48, checkBoxVerbose.Location.Y);
            checkBoxTrackMessages.Location = new Point(3 * width + 64, checkBoxTrackMessages.Location.Y);
            
            if (requiresSession)
            {
                txtAutoRenewTimeout.Size = new Size(width, txtAutoRenewTimeout.Size.Height);
                txtMessageWaitTimeout.Size = new Size(width, txtMessageWaitTimeout.Size.Height);
                txtAutoRenewTimeout.Location = new Point(4 * width + 80, cboReceivedMode.Location.Y);
                lblAutoRenewTimeout.Location = new Point(4 * width + 80, lblReceiveMode.Location.Y);
                txtMessageWaitTimeout.Location = new Point(5 * width + 96, cboReceivedMode.Location.Y);
                lblMessageWaitTimeout.Location = new Point(5 * width + 96, lblReceiveMode.Location.Y);
                checkBoxGraph.Location = new Point(4 * width + 80, checkBoxGraph.Location.Y);
            }
            else
            {
                txtAutoRenewTimeout.Visible = false;
                lblAutoRenewTimeout.Visible = false;
                txtMessageWaitTimeout.Visible = false;
                lblMessageWaitTimeout.Visible = false;
                checkBoxGraph.Location = new Point(grouperOptions.Size.Width - checkBoxGraph.Size.Width - 12, checkBoxGraph.Location.Y);
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

        private void LogErrors(object sender, ExceptionReceivedEventArgs e)
        {
            if (e.Exception == null || e.Exception is OperationCanceledException)
            {
                return;
            }
            HandleException(e.Exception);
        }

        private async Task OnMessageAsync(BrokeredMessage message)
        {
            try
            {
                if (message == null)
                {
                    return;
                }
                if (receiverBrokeredMessageInspector != null)
                {
                    message = receiverBrokeredMessageInspector.AfterReceiveMessage(message);
                }
                if (logging)
                {
                    var builder = new StringBuilder(string.Format(MessageSuccessfullyReceived,
                                                    string.IsNullOrWhiteSpace(message.MessageId)
                                                        ? NullValue
                                                        : message.MessageId,
                                                    string.IsNullOrWhiteSpace(message.SessionId)
                                                        ? NullValue
                                                        : message.SessionId,
                                                    string.IsNullOrWhiteSpace(message.Label)
                                                        ? NullValue
                                                        : message.Label,
                                                    message.Size));
                    if (verbose)
                    {
                        serviceBusHelper.GetMessageAndProperties(builder, message, encoder);
                    }
                    writeToLog(builder.ToString());
                }
                if (tracking)
                {
                    Invoke(new Action(() => messageBindingList.Add(message.Clone())));
                    //Invoke(new Action(() => messageCollection.Add(message)));
                }
                UpdateStatistics(1, GetElapsedTime(), message.Size);
                if (receiveMode == ReceiveMode.PeekLock && !autoComplete)
                {
                    await message.CompleteAsync();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void AsyncTrackMessage()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        BrokeredMessage message;
                        var ok = messageCollection.TryTake(out message, 100);
                        if (!ok)
                        {
                            continue;
                        }
                        await Task.Delay(TimeSpan.FromMilliseconds(5));
                        if (InvokeRequired)
                        {
                            Invoke(new Action(() => messageBindingList.Add(message.Clone())));
                        }
                        else
                        {
                            messageBindingList.Add(message.Clone());
                        }
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch 
                    {
                    }
                    
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch 
            {
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Initialize property grid
            var propertyList = new List<string[]>();
            if (entityDescription is QueueDescription)
            {
                var queueDescription = entityDescription as QueueDescription;
                queueDescription = serviceBusHelper.GetQueue(queueDescription.Path);
                entityDescription = queueDescription;
                propertyList.AddRange(new[]
                    {
                        new[] {ActiveMessageCount, queueDescription.MessageCountDetails.ActiveMessageCount.ToString(CultureInfo.CurrentCulture)},
                        new[] {DeadletterCount, queueDescription.MessageCountDetails.DeadLetterMessageCount.ToString(CultureInfo.CurrentCulture)},
                        new[] {ScheduledMessageCount, queueDescription.MessageCountDetails.ScheduledMessageCount.ToString(CultureInfo.CurrentCulture)},
                        new[] {TransferMessageCount, queueDescription.MessageCountDetails.TransferMessageCount.ToString(CultureInfo.CurrentCulture)},
                        new[] {TransferDeadLetterMessageCount, queueDescription.MessageCountDetails.TransferDeadLetterMessageCount.ToString(CultureInfo.CurrentCulture)},
                        new[] {MessageCount, queueDescription.MessageCount.ToString(CultureInfo.CurrentCulture)}
                    });
            }
            if (entityDescription is SubscriptionDescription)
            {
                var subscriptionDescription = entityDescription as SubscriptionDescription;
                subscriptionDescription = serviceBusHelper.GetSubscription(subscriptionDescription.TopicPath, subscriptionDescription.Name);
                entityDescription = subscriptionDescription;
                propertyList.AddRange(new[]{new[]{ActiveMessageCount, subscriptionDescription.MessageCountDetails.ActiveMessageCount.ToString(CultureInfo.CurrentCulture)},
                                            new[]{ScheduledMessageCount, subscriptionDescription.MessageCountDetails.ScheduledMessageCount.ToString(CultureInfo.CurrentCulture)},
                                            new[]{TransferMessageCount, subscriptionDescription.MessageCountDetails.TransferMessageCount.ToString(CultureInfo.CurrentCulture)},
                                            new[]{TransferDeadLetterMessageCount, subscriptionDescription.MessageCountDetails.TransferDeadLetterMessageCount.ToString(CultureInfo.CurrentCulture)},
                                            new[]{MessageCount, subscriptionDescription.MessageCount.ToString(CultureInfo.CurrentCulture)}});
            }

            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    lock (this)
                    {
                        if (stopping)
                        {
                            return;
                        }
                        propertyListView.Items.Clear();
                        foreach (var array in propertyList)
                        {
                            propertyListView.Items.Add(new ListViewItem(array));
                        }
                    }
                }));
            }
            else
            {
                lock (this)
                {
                    if (stopping)
                    {
                        return;
                    }
                    propertyListView.Items.Clear();
                    foreach (var array in propertyList)
                    {
                        propertyListView.Items.Add(new ListViewItem(array));
                    }
                }
            }
        }

        private async Task StopListener()
        {
            lock (this)
            {
                stopping = true;
            }
            if (stopLog != null)
            {
                await stopLog();
            }
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
            dateTime = DateTime.MinValue;
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
            if (queueClient != null)
            {
                queueClient.Close();
                queueClient = null;
            }
            if (subscriptionClient != null)
            {
                subscriptionClient.Close();
                subscriptionClient = null;
            }
            lock (this)
            {
                stopping = false;
            }
        }

        private void RefreshGraph()
        {
            try
            {
                long max = 10;
                graph = checkBoxGraph.Checked;
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    long receiveMessageNumber = 0;
                    long receiveTotalTime = 0;
                    long sizeTotal = 0;
                    long cycles = 0;
                    while (receiveMessageNumber < max && cycles < max)
                    {
                        cycles++;
                        Tuple<long, long, long> tuple;
                        var ok = blockingCollection.TryTake(out tuple, 10);
                        if (!ok)
                        {
                            continue;
                        }
                        receiveMessageNumber += tuple.Item1;
                        receiveTotalTime += tuple.Item2;
                        sizeTotal += tuple.Item3;
                        if (receiveMessageNumber > max)
                        {
                            max = receiveMessageNumber;
                        }
                    }
                    if (receiveMessageNumber > 0)
                    {
                        var receiveTuple = new Tuple<long, long, long>(receiveMessageNumber, receiveTotalTime, sizeTotal);
                        if (InvokeRequired)
                        {
                            Invoke(new Action<long, long, long, bool>(InternalUpdateStatistics),
                                   new object[] { receiveTuple.Item1, 
                                                  receiveTuple.Item2, 
                                                  receiveTuple.Item3,
                                                  graph});
                        }
                        else
                        {
                            InternalUpdateStatistics(receiveTuple.Item1,
                                                     receiveTuple.Item2,
                                                     receiveTuple.Item3,
                                                     graph);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Updates the statistics and graph on the control.
        /// </summary>
        /// <param name="messageNumber">Elapsed time.</param>
        /// <param name="elapsedMilliseconds">Elapsed time.</param>
        /// <param name="size">Message size.</param>
        private void UpdateStatistics(long messageNumber, long elapsedMilliseconds, long size)
        {
            blockingCollection.Add(new Tuple<long, long, long>(messageNumber, elapsedMilliseconds, size));
        }

        /// <summary>
        /// Updates the statistics and graph on the control.
        /// </summary>
        /// <param name="messageNumber">Elapsed time.</param>
        /// <param name="elapsedMilliseconds">Elapsed time.</param>
        /// <param name="size">Message size.</param>
        /// <param name="chartEnabled">True if the chart is enabled, false otherwise.</param>
        private void InternalUpdateStatistics(long messageNumber, long elapsedMilliseconds, long size, bool chartEnabled)
        {
            lock (this)
            {
                var elapsedSeconds = (double)elapsedMilliseconds / 1000;

                if (elapsedSeconds > receiverMaximumTime)
                {
                    receiverMaximumTime = elapsedSeconds;
                }
                if (elapsedSeconds < receiverMinimumTime)
                {
                    receiverMinimumTime = elapsedSeconds;
                }
                receiverTotalTime += elapsedSeconds;
                receiverMessageSizeTotal += size;
                receiverMessageNumber += messageNumber;
                receiverAverageTime = receiverMessageNumber > 0 ? receiverTotalTime / receiverMessageNumber : 0;
                receiverMessagesPerSecond = receiverTotalTime > 0 ? receiverMessageNumber / receiverTotalTime : 0;
                receiverMessageSizePerSecond = receiverTotalTime > 0 ? receiverMessageSizeTotal / (receiverTotalTime * 1024) : 0;

                txtMessagesPerSecond.Text = string.Format(LabelFormat, receiverMessagesPerSecond);
                txtMessagesPerSecond.Refresh();
                txtMessagesTotal.Text = receiverMessageNumber.ToString(CultureInfo.InvariantCulture);
                txtMessagesTotal.Refresh();
                txtAverageDuration.Text = string.Format(LabelFormat, receiverAverageTime);
                txtAverageDuration.Refresh();
                txtMessageSizePerSecond.Text = string.Format(LabelFormat, receiverMessageSizePerSecond);
                txtAverageDuration.Refresh();

                if (!chartEnabled)
                {
                    return;
                }
                chart.Series["ReceiverLatency"].Points.AddXY(receiverMessageNumber, elapsedSeconds * averageTimeScale);
                chart.Series["ReceiverThroughput"].Points.AddXY(receiverMessageNumber, receiverMessagesPerSecond * eventDataPerSecondScale);
                chart.Series["MessageSizePerSecond"].Points.AddXY(receiverMessageNumber, receiverMessageSizePerSecond * messageSizePerSecondScale);
            }
        }

        private long GetElapsedTime()
        {
            lock (this)
            {
                if (dateTime == DateTime.MinValue)
                {
                    dateTime = DateTime.Now;
                    return 0;
                }
                var now = DateTime.Now;
                var elapsed = now - dateTime;
                dateTime = now;
                return elapsed.Milliseconds;
            }
        }

        private void ListenerControl_Load(object sender, EventArgs e)
        {
            Control control = this;
            parentForm = control.Parent;
            while (parentForm != null && !(parentForm is Form))
            {
                parentForm = parentForm.Parent;
            }
            if (parentForm is ContainerForm)
            {
                containerForm = parentForm as ContainerForm;
            }
        }

        private void cboReceivedMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxAutoComplete.Enabled = cboReceivedMode.SelectedIndex == 1;
        }

        private void grouperOptions_CustomPaint(PaintEventArgs e)
        {
            var pen = new Pen(SystemColors.ActiveBorder, 1);
            e.Graphics.DrawRectangle(pen,
                                     cboReceivedMode.Location.X - 1,
                                     cboReceivedMode.Location.Y - 1,
                                     cboReceivedMode.Size.Width + 1,
                                     cboReceivedMode.Size.Height + 1);
        }

        private void checkBoxAutoComplete_CheckedChanged(object sender, EventArgs e)
        {
            autoComplete = checkBoxAutoComplete.Checked;
        }

        private async void checkBoxLogging_CheckedChanged(object sender, EventArgs e)
        {
            logging = checkBoxLogging.Checked;
            if (checkBoxLogging.Checked)
            {
                if (startLog != null)
                {
                    startLog();
                }
            }
            else
            {
                if (stopLog != null)
                {
                    await stopLog();
                }
            }
        }

        private void checkBoxVerbose_CheckedChanged(object sender, EventArgs e)
        {
            verbose = checkBoxVerbose.Checked;
        }

        private void checkBoxTrackMessages_CheckedChanged(object sender, EventArgs e)
        {
            tracking = checkBoxTrackMessages.Checked;
        }

        private void checkBoxGraph_CheckedChanged(object sender, EventArgs e)
        {
            graph = checkBoxGraph.Checked;
        }

        private void cboMessagesPerSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventDataPerSecondScale = (double)cboMessagesPerSecond.SelectedItem;
        }

        private void cboAverageTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            averageTimeScale = (double)cboAverageDuration.SelectedItem;
        }

        private void cboMessageSizePerSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            messageSizePerSecondScale = (double)cboMessageSizePerSecond.SelectedItem;
        }

        private void messagesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void ListenerControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   cboReceiverInspector.Location.X - 1,
                                   cboReceiverInspector.Location.Y - 1,
                                   cboReceiverInspector.Size.Width + 1,
                                   cboReceiverInspector.Size.Height + 1);
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

                if (blockingCollection != null)
                {
                    blockingCollection.Dispose();
                }

                if (messageCollection != null)
                {
                    messageCollection.Dispose();
                }

                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Dispose();
                }

                if (receiverBrokeredMessageInspector != null)
                {
                    var disposable = receiverBrokeredMessageInspector as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                if (queueClient != null)
                {
                    queueClient.Close();
                }

                if (subscriptionClient != null)
                {
                    subscriptionClient.Close();
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

        private void copyEntityInformationToClipboardMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();
                var builder = new StringBuilder();
                const string delimiter = ",";

                // Setup the columns
                for (var i = 0; i < propertyListView.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(delimiter);
                    }
                    builder.Append(propertyListView.Columns[i].Text);
                }
                builder.AppendLine();

                // Build the data row by row
                for (var i = 0; i < propertyListView.Items.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.AppendLine();
                    }
                    for (var j = 0; j < propertyListView.Columns.Count; j++)
                    {
                        if (j > 0)
                        {
                            builder.Append(delimiter);
                        }
                        builder.Append(propertyListView.Items[i].SubItems[j].Text);
                    }
                }

                Clipboard.SetText(builder.ToString());
            }
            catch (Exception ex)
            {
                HandleException(ex);
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
                    BodyType bodyType;
                    var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm, out bodyType));
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
        #endregion
    }
}