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
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
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
        private const string LabelFormat = "{0:0.000}";

        //***************************
        // Texts
        //***************************
        private const string PartitionKey = "PartitionKey";
        private const string Offset = "Offset";
        private const string EnqueuedTimeUtc = "EnqueuedTimeUtc";
        private const string Start = "Start";
        private const string Stop = "Stop";
        private const string NullValue = "NULL";

        //***************************
        // Messages
        //***************************
        private const string DoubleClickMessage = "Double-click a row to repair and resubmit the corresponding message.";
        private const string EventDataSuccessfullyReceived = "Event received. PartitionKey=[{0}] Offset=[{1}] EnqueuedTimeUtc=[{2}]";
        private const string CheckPointExecuted = "Checkpoint. PartitionId=[{0}]";
        private const string ConnectionStringCannotBeNull = "The namespace connection string cannot be null.";
        private const string SelectEventDataInspector = "Select an EventData inspector...";

        //***************************
        // Property Labels
        //***************************
        private const string PartitionId = "PartitionId";
        private const string EventHubPath = "Event Hub Path";
        private const string SizeInBytes = "Size in Bytes";
        private const string BeginSequenceNumber = "Begin Sequence Number";
        private const string EndSequenceNumber = "End Sequence Number";

        //***************************
        // Tooltips
        //***************************
        private const string RefreshTimeoutTooltip = "Gets or sets the refresh timeout for the information shown in the Partition Information panel.";
        private const string ReceiveTimeoutTooltip = "Gets or sets the receive timeout used by the listener to receive events from a partition.";
        private const string PrefetchCountTooltip = "Gets or sets the prefetch count for used by the listener to receive events from a partition.";
        private const string OffsetTooltip = "Gets or sets the offset from which the listener starts to receive events from a partition.";
        private const string CheckpointCountTooltip = "Gets or sets the number of events after which the listener performs a checkpoint on the partition.";
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
        private const string EpochTooltip = "The epoch value. The service uses this value to enforce partition/lease ownership.";
        private const string OffsetInclusiveTooltip = "if set to true, the Starting Offset is treated as an inclusive offset meaning the first event returned is the one that has the starting offset. Normally first event returned is the event after the starting offset.";

        //***************************
        // Constants
        //***************************
        private const string DefaultConsumerGroupName = "$Default";
        #endregion

        #region Private Fields
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly Action stopAndRestartLog;
        private EventData currentEventData;
        private int grouperEventDataCustomPropertiesWidth;
        private int currentMessageRowIndex;
        private int checkpointCount;
        private readonly int partitionCount;
        private bool sorting;
        private readonly SortableBindingList<EventData> eventDataBindingList = new SortableBindingList<EventData> { AllowNew = false, AllowEdit = false, AllowRemove = false };
        private readonly ConsumerGroupDescription consumerGroupDescription;
        private readonly IList<PartitionDescription> partitionDescriptions;
        private readonly BlockingCollection<EventData> eventDataCollection = new BlockingCollection<EventData>();
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
        private bool logging;
        private bool verbose;
        private bool tracking;
        private bool graph;
        private bool checkpoint;
        private IEventDataInspector receiverEventDataInspector;
        private EventHubDescription eventHubDescription ;
        #endregion

        #region Private Static Fields
        private static readonly MethodInfo checkpointMethodInfo;
        #endregion

        #region Static Constructor
        static PartitionListenerControl()
        {
            var type = typeof (EventHubReceiver);
            checkpointMethodInfo = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(p => string.Compare("Checkpoint", p.Name, StringComparison.OrdinalIgnoreCase) == 0);
        }
        #endregion

        #region Public Constructors
        public PartitionListenerControl()
        {
            eventHubDescription = null;
        }

        public PartitionListenerControl(WriteToLogDelegate writeToLog,
                                        Action stopAndRestartLog,
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
            this.stopAndRestartLog = stopAndRestartLog;
            this.serviceBusHelper = serviceBusHelper;
            this.consumerGroupDescription = consumerGroupDescription;
            var descriptions = partitionDescriptions as IList<PartitionDescription> ?? partitionDescriptions.ToList();
            this.partitionDescriptions = descriptions;
            partitionCount = partitionDescriptions == null || descriptions.Count == 0? 0 : descriptions.Count;
            eventHubDescription = serviceBusHelper.NamespaceManager.GetEventHub(consumerGroupDescription.EventHubPath);    
            InitializeComponent();
            InitializeControls();
            checkBoxCheckpoint_CheckedChanged(null, null);
            Disposed += ListenerControl_Disposed;
        }
        #endregion

        #region Private Methods
        void ListenerControl_Disposed(object sender, EventArgs e)
        {
            StopListener();
        }

        private void InitializeControls()
        {
            // Hide Partition Combobox
            if (partitionDescriptions != null)
            {
                // ReSharper disable once CoVariantArrayConversion
                cboPartition.Items.AddRange(partitionDescriptions.Select(p => p.PartitionId).ToArray());
                cboPartition.SelectedIndex = 0;
                if (partitionDescriptions.Count() == 1)
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
            

            // Set checkpointCount value
            checkpointCount = txtCheckpointCount.IntegerValue;

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

            // Create the Size column
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
            toolTip.SetToolTip(txtPrefetchCount, PrefetchCountTooltip);
            toolTip.SetToolTip(txtOffset, OffsetTooltip);
            toolTip.SetToolTip(txtEpoch, EpochTooltip);
            toolTip.SetToolTip(txtCheckpointCount, CheckpointCountTooltip);
            
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
            var listViewItems = currentEventData.Properties.Select(p => new ListViewItem(new[] { p.Key, p.Value.ToString() })).ToArray();
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (string.Compare(btnStart.Text, Start, StringComparison.OrdinalIgnoreCase) == 0)
            {
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
                logging = checkBoxLogging.Checked;
                verbose = checkBoxVerbose.Checked;
                tracking = checkBoxTrackMessages.Checked;
                graph = checkBoxGraph.Checked;
                try
                {
                    foreach (var partitionDescription in partitionDescriptions)
                    {
                        var description = partitionDescription;
                        Task.Run(async () =>
                        {
                            try
                            {
                                var eventHubClient = EventHubClient.CreateFromConnectionString(GetAmqpConnectionString(serviceBusHelper.ConnectionString),
                                                                                               consumerGroupDescription.EventHubPath);
                                var consumerGroup = string.Compare(consumerGroupDescription.Name,
                                                                   DefaultConsumerGroupName,
                                                                   StringComparison.InvariantCultureIgnoreCase) == 0
                                                                   ? eventHubClient.GetDefaultConsumerGroup()
                                                                   : eventHubClient.GetConsumerGroup(consumerGroupDescription.Name);
                                var epoch = txtEpoch.IntegerValue;
                                var eventHubReceiver = epoch == 0 ? 
                                                        await consumerGroup.CreateReceiverAsync(description.PartitionId, txtOffset.Text, checkBoxOffsetInclusive.Checked) :
                                                        await consumerGroup.CreateReceiverAsync(description.PartitionId, txtOffset.Text, checkBoxOffsetInclusive.Checked, epoch);
                                eventHubReceiver.PrefetchCount = txtPrefetchCount.IntegerValue;
                                var received = 0;
                                EventData previousEventData = null;
                                while (!cancellationTokenSource.Token.IsCancellationRequested)
                                {
                                    var stopwatch = Stopwatch.StartNew();
                                    var eventData = await eventHubReceiver.ReceiveAsync(TimeSpan.FromSeconds(txtReceiveTimeout.IntegerValue));
                                    stopwatch.Stop();
                                    if (eventData == null)
                                    {
                                        if (previousEventData != null && checkpoint)
                                        {
                                            checkpointMethodInfo.Invoke(eventHubReceiver, new object[] { previousEventData });
                                            previousEventData = null;
                                            if (logging)
                                            {
                                                writeToLog(string.Format(CheckPointExecuted, description.PartitionId));
                                            }
                                        }
                                        continue;
                                    }
                                    previousEventData = eventData;
                                    if (receiverEventDataInspector != null)
                                    {
                                        eventData = receiverEventDataInspector.AfterReceiveMessage(eventData);
                                    }
                                    received++;
                                    if (logging)
                                    {
                                        var builder = new StringBuilder(string.Format(EventDataSuccessfullyReceived,
                                                                        string.IsNullOrWhiteSpace(eventData.PartitionKey)
                                                                            ? NullValue
                                                                            : eventData.PartitionKey,
                                                                        eventData.Offset,
                                                                        eventData.EnqueuedTimeUtc));
                                        if (verbose)
                                        {
                                            serviceBusHelper.GetMessageAndProperties(builder, eventData);
                                        }
                                        writeToLog(builder.ToString());
                                    }
                                    if (tracking)
                                    {
                                        eventDataCollection.Add(eventData);
                                    }
                                    if (graph)
                                    {
                                        var bodySize = (long)0;
                                        var bodyStream = eventData.GetBodyStream();
                                        if (bodyStream != null && bodyStream.CanSeek)
                                        {
                                            bodySize = bodyStream.Length;
                                        }
                                        UpdateStatistics(1, stopwatch.ElapsedMilliseconds, bodySize);
                                    }
                                    if (received == checkpointCount && checkpoint)
                                    {
                                        checkpointMethodInfo.Invoke(eventHubReceiver, new object[] { eventData });
                                        received = 0;
                                        if (logging)
                                        {
                                            writeToLog(string.Format(CheckPointExecuted, description.PartitionId));
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
                        });
                    }
                    #pragma warning disable 4014
                    Task.Run(new Action(RefreshGraph));
                    #pragma warning restore 4014
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    StopListener();
                    btnStart.Text = Start;
                }
            }
            else
            {
                try
                {
                    StopListener();
                    btnStart.Text = Start;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        private async void AsyncTrackEventData()
        {
            try
            {
                while (true)
                {
                    EventData eventData;
                    var ok = eventDataCollection.TryTake(out eventData, 100);
                    if (!ok)
                    {
                        continue;
                    }
                    await Task.Delay(TimeSpan.FromMilliseconds(5));
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => eventDataBindingList.Add(eventData.Clone())));
                    }
                    else
                    {
                        eventDataBindingList.Add(eventData.Clone());
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
                eventDataBindingList.Clear();
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                StopListener();
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
            var width = (grouperOptions.Size.Width - 72) / 6; 

            txtRefreshInformation.Size = new Size(width, txtRefreshInformation.Size.Height);
            txtReceiveTimeout.Size = new Size(width, txtReceiveTimeout.Size.Height);
            txtPrefetchCount.Size = new Size(width, txtPrefetchCount.Size.Height);
            txtOffset.Size = new Size(width, txtOffset.Size.Height);
            txtEpoch.Size = new Size(width, txtOffset.Size.Height);
            txtCheckpointCount.Size = new Size(width, txtPrefetchCount.Size.Height);

            txtReceiveTimeout.Location = new Point(width + 24, txtReceiveTimeout.Location.Y);
            lblReceiveTimeout.Location = new Point(width + 24, lblReceiveTimeout.Location.Y);
            txtPrefetchCount.Location = new Point(2 * width + 32, txtPrefetchCount.Location.Y);
            lblPrefetchCount.Location = new Point(2 * width + 32, lblPrefetchCount.Location.Y);
            txtOffset.Location = new Point(3 * width + 40, txtOffset.Location.Y);
            lblOffset.Location = new Point(3 * width + 40, lblOffset.Location.Y);
            txtEpoch.Location = new Point(4 * width + 48, txtEpoch.Location.Y);
            lblEpoch.Location = new Point(4 * width + 48, lblEpoch.Location.Y);
            txtCheckpointCount.Location = new Point(5 * width + 56, txtCheckpointCount.Location.Y);
            lblCheckpointCount.Location = new Point(5 * width + 56, lblCheckpointCount.Location.Y);
           
            checkBoxVerbose.Location = new Point(width + 24, checkBoxVerbose.Location.Y);
            checkBoxTrackMessages.Location = new Point(2 * width + 32, checkBoxTrackMessages.Location.Y);
            checkBoxGraph.Location = new Point(3 * width + 40, checkBoxGraph.Location.Y);
            checkBoxOffsetInclusive.Location = new Point(4 * width + 48, checkBoxGraph.Location.Y);
            checkBoxCheckpoint.Location = new Point(5 * width + 56, checkBoxCheckpoint.Location.Y);
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
            GetPartitionDescription();
        }

        private void StopListener()
        {
            lock (this)
            {
                stopping = true;
            }
            if (stopAndRestartLog != null)
            {
                stopAndRestartLog();
            }
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
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
                            Invoke(new Action<long, long, long>(InternalUpdateStatistics),
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
        private void InternalUpdateStatistics(long messageNumber, long elapsedMilliseconds, long size)
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
                receiverMessageSizePerSecond = receiverTotalTime > 0 ? (receiverMessageSizeTotal * partitionCount) / (receiverTotalTime * 1024): 0;

                txtEventDataPerSecond.Text = string.Format(LabelFormat, receiverMessagesPerSecond);
                txtEventDataPerSecond.Refresh();
                txtEventDataTotal.Text = receiverMessageNumber.ToString(CultureInfo.InvariantCulture);
                txtEventDataTotal.Refresh();
                txtAverageDuration.Text = string.Format(LabelFormat, receiverAverageTime);
                txtMessageSizePerSecond.Text = string.Format(LabelFormat, receiverMessageSizePerSecond);
                txtAverageDuration.Refresh();

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

        private void grouperOptions_CustomPaint(PaintEventArgs e)
        {
        }

        private void checkBoxLogging_CheckedChanged(object sender, EventArgs e)
        {
            logging = checkBoxLogging.Checked;
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

        private void checkBoxCheckpoint_CheckedChanged(object sender, EventArgs e)
        {
            checkpoint = checkBoxCheckpoint.Checked && checkBoxCheckpoint.Enabled;
            txtCheckpointCount.Enabled = checkpoint;
        }

        private void txtCheckpointCount_TextChanged(object sender, EventArgs e)
        {
            checkpointCount = txtCheckpointCount.IntegerValue;
        }

        private static string GetAmqpConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException(ConnectionStringCannotBeNull);
            }
            var builder = new ServiceBusConnectionStringBuilder(connectionString) {TransportType = TransportType.Amqp};
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

        private  void cboPartition_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPartitionDescription();
            //try
            //{
            //    var propertyList = new List<string[]>();
            //    var partitionId = cboPartition.InvokeRequired ? Invoke(new Func<string>(() => cboPartition.Text)) : cboPartition.Text;
            //    var partition = partitionDescriptions.FirstOrDefault(p => p.PartitionId == (string)partitionId);
            //    if (partition == null)
            //    {
            //        return;
            //    }
            //    propertyList.AddRange(new[]{new[]{PartitionId, partition.PartitionId},
            //                          new[]{EventHubPath, partition.EventHubPath},
            //                          new[]{SizeInBytes, partition.SizeInBytes.ToString("N0")},
            //                          new[]{BeginSequenceNumber, partition.BeginSequenceNumber.ToString("N0")},
            //                          new[]{EndSequenceNumber, partition.EndSequenceNumber.ToString("N0")}});

            //    propertyListView.Items.Clear();
            //    foreach (var array in propertyList)
            //    {
            //        propertyListView.Items.Add(new ListViewItem(array));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    HandleException(ex);
            //}
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

        private void GetPartitionDescription()
        {
            try
            {
                // Initialize property grid
                var propertyList = new List<string[]>();
                var index = partitionDescriptions.Count == 1 ? 0 : cboPartition.InvokeRequired ? (int)Invoke(new Func<int>(() => cboPartition.SelectedIndex)) : cboPartition.SelectedIndex;
                var partitions = new List<PartitionDescription>(new[] { serviceBusHelper.GetPartition(partitionDescriptions[index].EventHubPath,
                                                                                                            partitionDescriptions[index].PartitionId) });
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
                                            new[]{SizeInBytes, partition.SizeInBytes.ToString("N0")},
                                            new[]{BeginSequenceNumber, partition.BeginSequenceNumber.ToString("N0")},
                                            new[]{EndSequenceNumber, partition.EndSequenceNumber.ToString("N0")}});

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
        #endregion
    }
}