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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class PartitionListenerControl : UserControl
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
        private const string PartitionKey = "PartitionKey";
        private const string SequenceNumber = "SequenceNumber";
        private const string Offset = "Offset";
        private const string EnqueuedTimeUtc = "EnqueuedTimeUtc";
        private const string Start = "Start";
        private const string Stop = "Stop";

        //***************************
        // Messages
        //***************************
        private const string DoubleClickMessage = "Double-click a row to open the event data.";
        private const string ConnectionStringCannotBeNull = "The namespace connection string cannot be null.";
        private const string SelectEventDataInspector = "Select an EventData inspector...";

        //***************************
        // Property Labels
        //***************************
        private const string PartitionId = "PartitionId";
        private const string EventHubPath = "Event Hub Path";
        private const string BeginSequenceNumber = "Begin Sequence Number";
        private const string LastEnqueuedOffset = "LastEnqueuedOffset";
        private const string LastEnqueuedTimeUtc = "LastEnqueuedTimeUtc";

        //***************************
        // Tooltips
        //***************************
        private const string RefreshTimeoutTooltip = "Gets or sets the refresh timeout for the information shown in the Partition Information panel.";
        private const string ReceiveTimeoutTooltip = "Gets or sets the receive timeout used by the listener to receive events from a partition.";
        private const string LoggingTooltip = "Enable or disable logging. You can change this setting before or after the listener is started.";
        private const string VerboseTooltip = "Enable or disable verbose logging. You can change this setting before or after the listener is started.";
        private const string TrackingTooltip = "Enable or disable message tracking. You can change this setting before or after the listener is started.";
        private const string GraphTooltip = "Enable or disable graph. You can change this setting before or after the listener is started.";
        private const string CheckpointAtReceiveTimeoutTooltip = "Enable or disable checkpoint at receive timeout and after the specified number of events.";
        private const string EventsTotalTooltip = "The total number of events received from the partition(s).";
        private const string EventsPerSecondTooltip = "The average number of events received from the partition(s) per second .";
        private const string AverageDurationTooltip = "The average duration of receive operation in seconds.";
        private const string KbPerSecTooltip = "The average number of KB received per second.";
        private const string ScaleTooltip = "Select a scale to enhance the visibility of counter data in the chart.";
        private const string MaxCountTooltip = "The maximum amount of event data the user is willing to accept in one call.";
        private const string StartDateTimeUtcTooltip = "The starting date and time in UTC format for this receiver. The Receive method starts receiving the next event after this StartingDateTimeUtc value. If null, the receiver starts receiving events from the beginning of the Event Hubs event stream.";
        private const string OffsetInclusiveTooltip = "if set to true, the Starting Offset is treated as an inclusive offset meaning the first event returned is the one that has the starting offset. Normally first event returned is the event after the starting offset.";

        //***************************
        // Constants
        //***************************
        private const string DefaultConsumerGroupName = "$Default";
        private const string SaveAsTitle = "Save File As";
        private const string JsonExtension = "json";
        private const string JsonFilter = "JSON Files|*.json|JSON Files With Unserialized Body|*.json|Text Documents|*.txt";
        private const string MessageFileFormat = "EventData_{0}_{1}.json";
        #endregion

        #region Private Fields
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly Func<Task> stopLog;
        private readonly Action startLog;
        private EventData currentEventData;
        private int grouperEventDataCustomPropertiesWidth;
        private int currentMessageRowIndex;
        private readonly int partitionCount;
        private bool sorting;
        private readonly SortableBindingList<EventData> eventDataBindingList = new SortableBindingList<EventData> { AllowNew = false, AllowEdit = false, AllowRemove = false };
        private readonly IList<PartitionRuntimeInformation> partitionRuntumeInformationList = new List<PartitionRuntimeInformation>();
        private BlockingCollection<EventData> eventDataCollection = new BlockingCollection<EventData>();
        private System.Timers.Timer timer;
        private long receiverMessageNumber;
        private long receiverMessageSizeTotal;
        private double receiverMessageSizePerSecond;
        private double receiverAverageTime;
        private double receiverMessagesPerSecond;
        private double receiverMinimumTime;
        private double receiverMaximumTime;
        private double receiverTotalTime;
        private double averageTimeScale;
        private double eventDataPerSecondScale;
        private double messageSizePerSecondScale;
        private BlockingCollection<Tuple<long, long, long>> blockingCollection;
        private CancellationTokenSource cancellationTokenSource;
        private Control parentForm;
        private ContainerForm containerForm;
        private bool stopping;
        private bool graph;
        private IEventDataInspector receiverEventDataInspector;
        // ReSharper disable once NotAccessedField.Local
        private readonly EventHubDescription eventHubDescription;
        private DateTime dateTime = DateTime.MinValue;
        private readonly EventHubConsumerGroup consumerGroup;
        private readonly EventHubClient eventHubClient;
        private Dictionary<string, bool> registeredDictionary = new Dictionary<string, bool>();
        private bool clearing;
        private bool cleared;
        private string iotHubConnectionString;
        #endregion

        #region Public Constructors
        public PartitionListenerControl()
        {
            eventHubDescription = null;
        }

        public PartitionListenerControl(WriteToLogDelegate writeToLog,
                                        Func<Task> stopLog,
                                        Action startLog,
                                        ServiceBusHelper serviceBusHelper,
                                        ConsumerGroupDescription consumerGroupDescription,
                                        IEnumerable<PartitionDescription> partitionDescriptions)
        {
            Task.Factory.StartNew(AsyncTrackEventData).ContinueWith(t =>
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
            eventHubDescription = serviceBusHelper.NamespaceManager.GetEventHub(consumerGroupDescription.EventHubPath);
            eventHubClient = EventHubClient.CreateFromConnectionString(GetAmqpConnectionString(serviceBusHelper.ConnectionString),
                                                                                               consumerGroupDescription.EventHubPath);
            consumerGroup = string.Compare(consumerGroupDescription.Name,
                                           DefaultConsumerGroupName,
                                           StringComparison.InvariantCultureIgnoreCase) == 0
                                           ? eventHubClient.GetDefaultConsumerGroup()
                                           : eventHubClient.GetConsumerGroup(consumerGroupDescription.Name);
            IList<string> partitionIdList = partitionDescriptions.Select(pd => pd.PartitionId).ToList();
            foreach (var id in partitionIdList)
            {
                partitionRuntumeInformationList.Add(eventHubClient.GetPartitionRuntimeInformation(id));
            }
            partitionCount = partitionRuntumeInformationList.Count;
            InitializeComponent();
            InitializeControls();
            Disposed += ListenerControl_Disposed;
        }

        public PartitionListenerControl(WriteToLogDelegate writeToLog,
                                        Func<Task> stopLog,
                                        Action startLog,
                                        string iotHubConnectionString,
                                        string consumerGroupName)
        {
            Task.Factory.StartNew(AsyncTrackEventData).ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    writeToLog(t.Exception.Message);
                }
            });
            this.iotHubConnectionString = iotHubConnectionString;
            this.writeToLog = writeToLog;
            this.stopLog = stopLog;
            this.startLog = startLog;
            serviceBusHelper = new ServiceBusHelper(writeToLog);
            eventHubClient = EventHubClient.CreateFromConnectionString(iotHubConnectionString, "messages/events");
            consumerGroup = string.Compare(consumerGroupName,
                                           DefaultConsumerGroupName,
                                           StringComparison.InvariantCultureIgnoreCase) == 0
                                           ? eventHubClient.GetDefaultConsumerGroup()
                                           : eventHubClient.GetConsumerGroup(consumerGroupName);
            IList<string> partitionIdList = new List<string>(eventHubClient.GetRuntimeInformation().PartitionIds);
            foreach (var id in partitionIdList)
            {
                partitionRuntumeInformationList.Add(eventHubClient.GetPartitionRuntimeInformation(id));
            }
            partitionCount = partitionRuntumeInformationList.Count;
            InitializeComponent();
            InitializeControls();
            Disposed += ListenerControl_Disposed;
        }
        #endregion

        #region Private Methods
        private async void ListenerControl_Disposed(object sender, EventArgs e)
        {
            await StopListenerAsync();
        }

        private void InitializeControls()
        {
            // Hide Partition Combobox
            if (partitionRuntumeInformationList != null)
            {
                // ReSharper disable once CoVariantArrayConversion
                cboPartition.Items.AddRange(partitionRuntumeInformationList.Select(p => p.PartitionId).ToArray());
                cboPartition.SelectedIndex = 0;
                if (partitionRuntumeInformationList.Count() == 1)
                {
                    lblPartition.Visible = false;
                    cboPartition.Visible = false;
                    lblPartitionInformation.Visible = false;
                    propertyListView.Location = new Point(16, 32);
                    propertyListView.Size = new Size(propertyListView.Width, grouperPartitionInformation.Size.Height - 48);
                }
            }

            // Add data to scale combobox
            object[] scaleValues = { (double)1000, (double)100, (double)10, (double)1, 0.1, 0.01, 0.001 };
            object[] scaleValues2 = { (double)10000, (double)1000, (double)100, (double)10, (double)1, 0.1, 0.01, 0.001 };
            cboAverageDuration.Items.AddRange(scaleValues2);
            cboEventDataPerSecond.Items.AddRange(scaleValues);
            cboMessageSizePerSecond.Items.AddRange(scaleValues);
            cboAverageDuration.SelectedIndex = 4;
            cboEventDataPerSecond.SelectedIndex = 3;
            cboMessageSizePerSecond.SelectedIndex = 3;

            // Hide caret
            txtEventDataPerSecond.GotFocus += textBox_GotFocus;
            txtEventDataTotal.GotFocus += textBox_GotFocus;
            txtAverageDuration.GotFocus += textBox_GotFocus;
            txtMessageSizePerSecond.GotFocus += textBox_GotFocus;

            // Splitter controls
            eventDataSplitContainer.SplitterWidth = 16;
            eventDataCustomPropertiesSplitContainer.SplitterWidth = 16;
            messageListTextPropertiesSplitContainer.SplitterWidth = 8;

            // Set Grid style
            eventDataDataGridView.EnableHeadersVisualStyles = false;
            eventDataDataGridView.AutoGenerateColumns = false;
            eventDataDataGridView.AutoSize = true;

            // Create the PartitionKey column
            var textBoxColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = PartitionKey,
                Name = PartitionKey,
                Width = 120
            };
            eventDataDataGridView.Columns.Add(textBoxColumn);

            // Create the SequenceNumber column
            textBoxColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = SequenceNumber,
                Name = SequenceNumber,
                Width = 52
            };
            eventDataDataGridView.Columns.Add(textBoxColumn);

            // Create the Offset column
            textBoxColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = Offset,
                Name = Offset,
                Width = 52
            };
            eventDataDataGridView.Columns.Add(textBoxColumn);

            // Create the EnqueuedTimeUtc column
            textBoxColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = EnqueuedTimeUtc,
                Name = EnqueuedTimeUtc,
                Width = 120
            };
            eventDataDataGridView.Columns.Add(textBoxColumn);

            // Set the selection background color for all the cells.
            eventDataDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
            eventDataDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            eventDataDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

            // Set the background color for all rows and for alternating rows.  
            // The value for alternating rows overrides the value for all rows. 
            eventDataDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            eventDataDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
            //eventDataDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            //eventDataDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Set the row and column header styles.
            eventDataDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            eventDataDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            eventDataDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            eventDataDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Set DataGridView DataSource
            eventDataBindingSource.DataSource = eventDataBindingList;
            eventDataDataGridView.DataSource = eventDataBindingSource;

            // Set Grouper width
            grouperEventDataCustomPropertiesWidth = grouperEventDataCustomProperties.Size.Width;

            // Refresh data
            timer_Elapsed(null, null);

            // Set Tooltips
            toolTip.SetToolTip(txtRefreshInformation, RefreshTimeoutTooltip);
            toolTip.SetToolTip(txtReceiveTimeout, ReceiveTimeoutTooltip);
            toolTip.SetToolTip(txtMaxBatchSize, MaxCountTooltip);
            toolTip.SetToolTip(pickerStartingDateTimeUtc, StartDateTimeUtcTooltip);

            toolTip.SetToolTip(checkBoxLogging, LoggingTooltip);
            toolTip.SetToolTip(checkBoxVerbose, VerboseTooltip);
            toolTip.SetToolTip(checkBoxTrackMessages, TrackingTooltip);
            toolTip.SetToolTip(checkBoxGraph, GraphTooltip);
            toolTip.SetToolTip(checkBoxCheckpoint, CheckpointAtReceiveTimeoutTooltip);
            toolTip.SetToolTip(checkBoxOffsetInclusive, OffsetInclusiveTooltip);

            toolTip.SetToolTip(txtEventDataTotal, EventsTotalTooltip);
            toolTip.SetToolTip(txtEventDataPerSecond, EventsPerSecondTooltip);
            toolTip.SetToolTip(txtAverageDuration, AverageDurationTooltip);
            toolTip.SetToolTip(txtMessageSizePerSecond, KbPerSecTooltip);

            toolTip.SetToolTip(cboEventDataPerSecond, ScaleTooltip);
            toolTip.SetToolTip(cboAverageDuration, ScaleTooltip);
            toolTip.SetToolTip(cboMessageSizePerSecond, ScaleTooltip);


            propertyListView.ContextMenuStrip = partitionInformationContextMenuStrip;

            cboReceiverInspector.Items.Add(SelectEventDataInspector);
            cboReceiverInspector.SelectedIndex = 0;

            if (serviceBusHelper == null || serviceBusHelper.EventDataInspectors == null)
            {
                return;
            }
            foreach (var key in serviceBusHelper.EventDataInspectors.Keys)
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
                var columnWidth = width / 4;
                dataGridView.Columns[0].Width = columnWidth;
                dataGridView.Columns[1].Width = columnWidth;
                dataGridView.Columns[2].Width = columnWidth;
                dataGridView.Columns[3].Width = columnWidth + (width - (columnWidth * 4));
            }
            finally
            {
                dataGridView.ResumeLayout();
                dataGridView.ResumeDrawing();
            }
        }

        private void eventDataDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var bindingList = eventDataBindingSource.DataSource as BindingList<EventData>;
            currentMessageRowIndex = e.RowIndex;
            if (bindingList == null)
            {
                return;
            }
            if (currentEventData == bindingList[e.RowIndex])
            {
                return;
            }
            currentEventData = bindingList[e.RowIndex];
            eventDataPropertyGrid.SelectedObject = currentEventData;
            BodyType bodyType;
            txtMessageText.Text = XmlHelper.Indent(serviceBusHelper.GetMessageText(currentEventData, out bodyType));
            var listViewItems = currentEventData.Properties.Select(p => new ListViewItem(new[] { p.Key, (p.Value ?? string.Empty).ToString() })).ToArray();
            eventDataPropertyListView.Items.Clear();
            eventDataPropertyListView.Items.AddRange(listViewItems);
        }

        private void tabPageMessages_Resize(object sender, EventArgs e)
        {
            try
            {
                eventDataSplitContainer.SuspendDrawing();
                eventDataSplitContainer.SuspendLayout();
                grouperEventDataCustomProperties.Size = new Size(grouperEventDataCustomProperties.Size.Width, messageListTextPropertiesSplitContainer.Panel2.Size.Height);
                eventDataPropertyGrid.Size = new Size(grouperMessageProperties.Size.Width - 32, eventDataPropertyGrid.Size.Height);
                eventDataPropertyListView.Size = new Size(grouperEventDataCustomProperties.Size.Width - 32, eventDataPropertyListView.Size.Height);
                eventDataCustomPropertiesSplitContainer.SplitterDistance = eventDataCustomPropertiesSplitContainer.Width -
                                                                            grouperEventDataCustomPropertiesWidth -
                                                                            eventDataCustomPropertiesSplitContainer.SplitterWidth;
                grouperEventDataCustomPropertiesWidth = grouperEventDataCustomProperties.Width;
            }
            finally
            {
                eventDataSplitContainer.ResumeDrawing();
                eventDataSplitContainer.ResumeLayout();
            }
        }

        private void grouperMessageList_CustomPaint(PaintEventArgs e)
        {
            eventDataDataGridView.Size = new Size(grouperMessageList.Size.Width - (eventDataDataGridView.Location.X * 2 + 2),
                                                 grouperMessageList.Size.Height - eventDataDataGridView.Location.Y - eventDataDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                            eventDataDataGridView.Location.X - 1,
                                            eventDataDataGridView.Location.Y - 1,
                                            eventDataDataGridView.Size.Width + 1,
                                            eventDataDataGridView.Size.Height + 1);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void eventDataDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var bindingList = eventDataBindingSource.DataSource as BindingList<EventData>;
            if (bindingList == null)
            {
                return;
            }
            using (var messageForm = new EventDataForm(bindingList[e.RowIndex], serviceBusHelper, writeToLog))
            {
                messageForm.ShowDialog();
            }
        }

        private void eventDataDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = eventDataDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ToolTipText = DoubleClickMessage;
        }

        private void grouperEventDataText_CustomPaint(PaintEventArgs obj)
        {
            txtMessageText.Size = new Size(grouperMessageText.Size.Width - (txtMessageText.Location.X * 2),
                                           grouperMessageText.Size.Height - txtMessageText.Location.Y - txtMessageText.Location.X);
        }

        private void grouperEventDataCustomProperties_CustomPaint(PaintEventArgs obj)
        {
            eventDataPropertyListView.Size = new Size(grouperEventDataCustomProperties.Size.Width - (eventDataPropertyListView.Location.X * 2),
                                                    grouperEventDataCustomProperties.Size.Height - eventDataPropertyListView.Location.Y - eventDataPropertyListView.Location.X);
        }

        private void grouperMessageProperties_CustomPaint(PaintEventArgs obj)
        {
            eventDataPropertyGrid.Size = new Size(grouperMessageProperties.Size.Width - (eventDataPropertyGrid.Location.X * 2),
                                                grouperMessageProperties.Size.Height - eventDataPropertyGrid.Location.Y - eventDataPropertyGrid.Location.X);
        }

        private void eventDataDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex == -1)
            {
                return;
            }
            eventDataDataGridView.Rows[e.RowIndex].Selected = true;
            var multipleSelectedRows = eventDataDataGridView.SelectedRows.Count > 1;
            viewAndSaveEventDataToolStripMenuItem.Visible = !multipleSelectedRows;
            saveSelectedEventToolStripMenuItem.Visible = !multipleSelectedRows;
            toolStripSeparator.Visible = !multipleSelectedRows;
            saveSelectedEventsToolStripMenuItem.Visible = multipleSelectedRows;
            eventDataContextMenuStrip.Show(Cursor.Position);
        }

        private void viewAndSaveEventDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventDataDataGridView_CellDoubleClick(eventDataDataGridView, new DataGridViewCellEventArgs(0, currentMessageRowIndex));
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

        // ReSharper disable once FunctionComplexityOverflow
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (string.Compare(btnStart.Text, Start, StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (startLog != null && checkBoxLogging.Checked)
                {
                    startLog();
                }
                receiverEventDataInspector = cboReceiverInspector.SelectedIndex > 0
                                           ? Activator.CreateInstance(serviceBusHelper.EventDataInspectors[cboReceiverInspector.Text]) as IEventDataInspector
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
                timer.Elapsed += timer_Elapsed;
                var ns = serviceBusHelper != null && !string.IsNullOrWhiteSpace(serviceBusHelper.Namespace) ? serviceBusHelper.Namespace : iotHubConnectionString;
                var eventHub = eventHubClient.Path;
                var maxBatchSize = txtMaxBatchSize.IntegerValue > 0 ? txtMaxBatchSize.IntegerValue : 1;
                var receiveTimeout = TimeSpan.FromSeconds(txtReceiveTimeout.IntegerValue);
                try
                {
                    registeredDictionary = new Dictionary<string, bool>(partitionRuntumeInformationList.Count);
                    var startDateTimeEnabled = pickerStartingDateTimeUtc.Checked;
                    var startDateTimeValue = DateTime.SpecifyKind(pickerStartingDateTimeUtc.Value, DateTimeKind.Utc);
                    var eventProcessorOptions = new EventProcessorOptions
                    {
                        InitialOffsetProvider = partitionId =>
                        {
                            if (startDateTimeEnabled)
                            {
                                return startDateTimeValue;
                            }
                            var lease = EventProcessorCheckpointHelper.GetLease(ns,
                                eventHub,
                                consumerGroup.GroupName,
                                partitionId);
                            return lease != null ? lease.Offset : "-1";
                        },
                        MaxBatchSize = maxBatchSize,
                        ReceiveTimeOut = receiveTimeout
                    };
                    eventProcessorOptions.ExceptionReceived += (s, ex) => HandleException(ex.Exception);
                    var checkpointManager = new EventProcessorCheckpointManager
                    {
                        Namespace = ns,
                        EventHub = eventHub,
                        ConsumerGroup = consumerGroup.GroupName
                    };
                    var eventProcessorFactoryConfiguration = new EventProcessorFactoryConfiguration(checkBoxLogging,
                                                                                                    checkBoxTrackMessages,
                                                                                                    checkBoxVerbose,
                                                                                                    checkBoxOffsetInclusive,
                                                                                                    checkBoxCheckpoint,
                                                                                                    cancellationTokenSource.Token)
                    {
                        TrackEvent = ev => Invoke(new Action<EventData>(m => eventDataCollection.Add(m)), ev),
                        GetElapsedTime = GetElapsedTime,
                        UpdateStatistics = UpdateStatistics,
                        WriteToLog = writeToLog,
                        MessageInspector = receiverEventDataInspector,
                        ServiceBusHelper = serviceBusHelper
                    };
                    foreach (var partitionRuntimeInformation in partitionRuntumeInformationList)
                    {
#pragma warning disable 4014
                        consumerGroup.RegisterProcessorFactoryAsync(
#pragma warning restore 4014
EventProcessorCheckpointHelper.GetLease(ns, eventHub, consumerGroup.GroupName, partitionRuntimeInformation.PartitionId),
                                checkpointManager,
                                new EventProcessorFactory<EventProcessor>(eventProcessorFactoryConfiguration),
                                eventProcessorOptions);
                        registeredDictionary.Add(partitionRuntimeInformation.PartitionId, true);
                    }
#pragma warning disable 4014
                    Task.Run(new Action(RefreshGraph));
#pragma warning restore 4014
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    StopListenerAsync().Wait();
                    btnStart.Text = Start;
                }
            }
            else
            {
                try
                {
                    await StopListenerAsync();
                    btnStart.Text = Start;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        public object Bingo(string partitionId)
        {
            return DateTime.UtcNow;
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

        private async void AsyncTrackEventData()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        if (clearing)
                        {
                            Thread.Sleep(50);
                            continue;
                        }
                        if (cleared)
                        {
                            Invoke(new Action(ClearTrackedMessages));

                            cleared = false;
                            continue;
                        }
                        EventData eventData;
                        var ok = eventDataCollection.TryTake(out eventData, 100);
                        if (!ok)
                        {
                            continue;
                        }
                        await Task.Delay(TimeSpan.FromMilliseconds(5));

                        if (InvokeRequired)
                        {
                            Invoke(new Action(() => eventDataBindingList.Add(eventData)));
                        }
                        else
                        {
                            eventDataBindingList.Add(eventData);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clearing = true;
                cleared = true;
                eventDataCollection.Dispose();
                eventDataCollection = new BlockingCollection<EventData>();
                ClearTrackedMessages();
                ClearStatistics();
                ClearCharts();
                if (containerForm != null)
                {
                    containerForm.Clear();
                }
                txtMessageText.Text = null;
                eventDataPropertyListView.Clear();
                eventDataPropertyGrid.SelectedObject = null;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                clearing = false;
            }
        }

        private void ClearTrackedMessages()
        {
            eventDataBindingList.Clear();
            eventDataPropertyGrid.SelectedObject = null;
            txtMessageText.Text = string.Empty;
            eventDataPropertyListView.Items.Clear();
        }

        private void ClearStatistics()
        {
            txtEventDataTotal.Text = @"0";
            txtEventDataPerSecond.Text = @"0";
            txtAverageDuration.Text = @"0";
            txtMessageSizePerSecond.Text = @"0";

            receiverTotalTime = 0;
            receiverMessageSizeTotal = 0;
            receiverMessageNumber = 0;
            receiverAverageTime = 0;
            receiverMessagesPerSecond = 0;
            receiverMessageSizePerSecond = 0;
        }

        private void ClearCharts()
        {
            chart.Series["ReceiverLatency"].Points.Clear();
            chart.Series["ReceiverThroughput"].Points.Clear();
            chart.Series["MessageSizePerSecond"].Points.Clear();
        }

        private async void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                await StopListenerAsync();
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

            txtEventDataTotal.AutoSize = false;
            txtEventDataTotal.Size = new Size(width, cboEventDataPerSecond.Size.Height + 2);

            txtEventDataPerSecond.AutoSize = false;
            txtEventDataPerSecond.Size = new Size(width - cboEventDataPerSecond.Size.Width, cboEventDataPerSecond.Size.Height + 2);

            txtAverageDuration.AutoSize = false;
            txtAverageDuration.Size = new Size(width - cboAverageDuration.Size.Width, cboAverageDuration.Size.Height + 2);

            txtMessageSizePerSecond.AutoSize = false;
            txtMessageSizePerSecond.Size = new Size(width - cboMessageSizePerSecond.Size.Width, cboMessageSizePerSecond.Size.Height + 2);


            txtEventDataPerSecond.Location = new Point(width + 32, txtEventDataPerSecond.Location.Y);
            lblEventDataPerSecond.Location = new Point(width + 32, lblEventDataTotal.Location.Y);
            cboEventDataPerSecond.Location = new Point(2 * width + 32 - cboEventDataPerSecond.Size.Width + 1, cboEventDataPerSecond.Location.Y);

            cboAverageDuration.Location = new Point(width + 16 - cboAverageDuration.Size.Width + 1, cboAverageDuration.Location.Y);

            txtMessageSizePerSecond.Location = new Point(width + 32, txtMessageSizePerSecond.Location.Y);
            lblMessageSizePerSecond.Location = new Point(width + 32, lblMessageSizePerSecond.Location.Y);
            cboMessageSizePerSecond.Location = new Point(2 * width + 32 - cboMessageSizePerSecond.Size.Width + 1, cboMessageSizePerSecond.Location.Y);

            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboEventDataPerSecond.Location.X - 1,
                                    cboEventDataPerSecond.Location.Y - 1,
                                    cboEventDataPerSecond.Size.Width + 1,
                                    cboEventDataPerSecond.Size.Height + 1);
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
            var textboxWidth = (grouperOptions.Size.Width - 80 - pickerStartingDateTimeUtc.Size.Width) / 3;

            txtRefreshInformation.Size = new Size(textboxWidth, txtRefreshInformation.Size.Height);
            txtReceiveTimeout.Size = new Size(textboxWidth, txtReceiveTimeout.Size.Height);
            txtMaxBatchSize.Size = new Size(textboxWidth, txtMaxBatchSize.Size.Height);

            txtReceiveTimeout.Location = new Point(textboxWidth + 32, txtReceiveTimeout.Location.Y);
            lblReceiveTimeout.Location = new Point(textboxWidth + 32, lblReceiveTimeout.Location.Y);
            txtMaxBatchSize.Location = new Point(2 * textboxWidth + 48, txtMaxBatchSize.Location.Y);
            lblMaxBatchSize.Location = new Point(2 * textboxWidth + 48, lblMaxBatchSize.Location.Y);

            var width = (grouperOptions.Size.Width - 32 - checkBoxCheckpoint.Size.Width) / 5;
            checkBoxVerbose.Location = new Point(width, checkBoxVerbose.Location.Y);
            checkBoxTrackMessages.Location = new Point(2 * width, checkBoxTrackMessages.Location.Y);
            checkBoxGraph.Location = new Point(3 * width, checkBoxGraph.Location.Y);
            checkBoxOffsetInclusive.Location = new Point(4 * width, checkBoxGraph.Location.Y);
            //checkBoxCheckpoint.Location = new Point(5 * width + 48, checkBoxCheckpoint.Location.Y);
        }

        private static void textBox_GotFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                HideCaret(textBox.Handle);
            }
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            GetPartitionRuntimeInformation();
        }

        private async Task StopListenerAsync()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
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
            foreach (var partitionDescription in partitionRuntumeInformationList)
            {
                if (registeredDictionary.ContainsKey(partitionDescription.PartitionId) &&
                    registeredDictionary[partitionDescription.PartitionId])
                {
                    await consumerGroup.UnregisterProcessorAsync(new Lease { PartitionId = partitionDescription.PartitionId },
                                                                 ServiceBus.Messaging.CloseReason.Shutdown);
                }
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
                receiverMessagesPerSecond = receiverTotalTime > 0 ? receiverMessageNumber * partitionCount / receiverTotalTime : 0;
                receiverMessageSizePerSecond = receiverTotalTime > 0 ? (receiverMessageSizeTotal * partitionCount) / (receiverTotalTime * 1024) : 0;

                txtEventDataPerSecond.Text = string.Format(LabelFormat, receiverMessagesPerSecond);
                txtEventDataPerSecond.Refresh();
                txtEventDataTotal.Text = receiverMessageNumber.ToString(CultureInfo.InvariantCulture);
                txtEventDataTotal.Refresh();
                txtAverageDuration.Text = string.Format(LabelFormat, receiverAverageTime);
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

        private async void checkBoxLogging_CheckedChanged(object sender, EventArgs e)
        {
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

        private void checkBoxGraph_CheckedChanged(object sender, EventArgs e)
        {
            graph = checkBoxGraph.Checked;
        }

        private static string GetAmqpConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException(ConnectionStringCannotBeNull);
            }
            var builder = new ServiceBusConnectionStringBuilder(connectionString) { TransportType = TransportType.Amqp };
            return builder.ToString();
        }

        private void grouperPartitionInformation_CustomPaint(PaintEventArgs e)
        {
            if (!cboPartition.Visible)
            {
                return;
            }
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboPartition.Location.X - 1,
                                    cboPartition.Location.Y - 1,
                                    cboPartition.Size.Width + 1,
                                    cboPartition.Size.Height + 1);
        }

        private void cboPartition_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPartitionRuntimeInformation();
        }

        private void cboEventDataPerSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventDataPerSecondScale = (double)cboEventDataPerSecond.SelectedItem;
        }

        private void cboAverageTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            averageTimeScale = (double)cboAverageDuration.SelectedItem;
        }

        private void cboMessageSizePerSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            messageSizePerSecondScale = (double)cboMessageSizePerSecond.SelectedItem;
        }

        private void eventDataDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void PartitionListenerControl_Paint(object sender, PaintEventArgs e)
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

                if (eventDataCollection != null)
                {
                    eventDataCollection.Dispose();
                }

                //if (cancellationTokenSource != null)
                //{
                //    cancellationTokenSource.Dispose();
                //}

                if (receiverEventDataInspector != null)
                {
                    var disposable = receiverEventDataInspector as IDisposable;
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

        private void copyPartitionInformationToClipboardMenuItem_Click(object sender, EventArgs e)
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

        private void GetPartitionRuntimeInformation()
        {
            try
            {
                // Initialize property grid
                var propertyList = new List<string[]>();
                var index = partitionRuntumeInformationList.Count == 1 ? 0 : cboPartition.InvokeRequired ? (int)Invoke(new Func<int>(() => cboPartition.SelectedIndex)) : cboPartition.SelectedIndex;
                var partitions = new List<PartitionRuntimeInformation>(new[] { eventHubClient.GetPartitionRuntimeInformation(partitionRuntumeInformationList[index].PartitionId) });
                if (!partitions.Any())
                {
                    return;
                }
                var partitionId = cboPartition.InvokeRequired ? Invoke(new Func<string>(() => cboPartition.Text)) : cboPartition.Text;
                var partition = partitions.FirstOrDefault(p => p.PartitionId == (string)partitionId);
                if (partition == null)
                {
                    return;
                }
                propertyList.AddRange(new[]{new[]{PartitionId, partition.PartitionId},
                                            new[]{EventHubPath, partition.EventHubPath},
                                            new[]{LastEnqueuedOffset, partition.LastEnqueuedOffset ?? "Null"},
                                            new[]{LastEnqueuedTimeUtc, partition.LastEnqueuedTimeUtc.ToString(CultureInfo.InvariantCulture)},
                                            new[]{BeginSequenceNumber, partition.BeginSequenceNumber.ToString("N0")}});

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
            catch (TimeoutException)
            { }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveSelectedEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentMessageRowIndex < 0)
                {
                    return;
                }
                var bindingList = eventDataBindingSource.DataSource as BindingList<EventData>;
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
                bool doNotSerializeBody = saveFileDialog.FilterIndex == 2;
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.Write(MessageSerializationHelper.Serialize(bindingList[currentMessageRowIndex], txtMessageText.Text, doNotSerializeBody));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveSelectedEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (eventDataDataGridView.SelectedRows.Count <= 0)
                {
                    return;
                }
                var messages = eventDataDataGridView.SelectedRows.Cast<DataGridViewRow>().Select(r => r.DataBoundItem as EventData);
                IEnumerable<EventData> brokeredMessages = messages as EventData[] ?? messages.ToArray();
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
                bool doNotSerializeBody = saveFileDialog.FilterIndex == 2;

                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    BodyType bodyType;
                    var bodies = brokeredMessages.Select(bm => serviceBusHelper.GetMessageText(bm, out bodyType, doNotSerializeBody));
                    writer.Write(MessageSerializationHelper.Serialize(brokeredMessages, bodies, doNotSerializeBody));
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