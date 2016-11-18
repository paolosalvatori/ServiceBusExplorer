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
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
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
        private const string LabelFormat = "{0:0.000}";

        //***************************
        // Texts
        //***************************
        private const string MessageId = "MessageId";
        private const string MessageSize = "Size";
        private const string Label = "Label";
        private const string Start = "Start";
        private const string Stop = "Stop";
        private const string NullValue = "NULL";

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
        private const string NoSessionAvailable = "No session available.";

        //***************************
        // Property Labels
        //***************************
        private const string MessageCount = "Message Count";
        private const string ActiveMessageCount = "Active Message Count";
        private const string DeadletterCount = "DeadLetter Message Count";
        private const string ScheduledMessageCount = "Scheduled Message Count";
        private const string TransferMessageCount = "Transfer Message Count";
        private const string TransferDeadLetterMessageCount = "Transfer DL Message Count";
        #endregion

        #region Private Fields
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private BrokeredMessage brokeredMessage;
        private int grouperMessageCustomPropertiesWidth;
        private int currentMessageRowIndex;
        private bool sorting;
        private string messagesFilterExpression;
        private readonly SortableBindingList<BrokeredMessage> messageBindingList = new SortableBindingList<BrokeredMessage> { AllowNew = false, AllowEdit = false, AllowRemove = false };
        private EntityDescription entityDescription;
        private MessageReceiver messageReceiver;
        private readonly MessageEncoder encoder;
        private System.Timers.Timer timer;
        private long receiverMessageNumber;
        private double receiverMessagesPerSecond;
        private double receiverMinimumTime;
        private double receiverMaximumTime;
        private double receiverTotalTime;
        private BlockingCollection<Tuple<long, long, DirectionType>> blockingCollection;
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
        #endregion

        #region Public Constructors
        public ListenerControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, EntityDescription entityDescription)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.entityDescription = entityDescription;
            var element = new BinaryMessageEncodingBindingElement();
            var encoderFactory = element.CreateMessageEncoderFactory();
            encoder = encoderFactory.Encoder;
            InitializeComponent();
            InitializeControls();
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
            // Set default Receive Mode
            cboReceivedMode.SelectedIndex = 0;

            // Hide caret
            txtMessagePerSecond.GotFocus += textBox_GotFocus;
            txtMessagesTotal.GotFocus += textBox_GotFocus;

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
            var listViewItems = brokeredMessage.Properties.Select(p => new ListViewItem(new[] { p.Key, p.Value.ToString() })).ToArray();
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
                cancellationTokenSource = new CancellationTokenSource();
                btnStart.Text = Stop;
                try
                {
                    receiveMode = cboReceivedMode.SelectedIndex == 1 ? ReceiveMode.PeekLock : ReceiveMode.ReceiveAndDelete;
                    messageReceiver = await CreateMessageReceiverAsync();
                    if (messageReceiver == null)
                    {
                        return;
                    }
                    messageReceiver.PrefetchCount = txtPrefetchCount.IntegerValue;

                    // Initialize message pump options
                    var options = new OnMessageOptions
                    {
                        // Indicates if the message-pump should call complete on messages after the callback has completed processing.
                        AutoComplete = checkBoxAutoComplete.Checked,
                        // Indicates the maximum number of concurrent calls to the callback the pump should initiate 
                        MaxConcurrentCalls = txtMaxConcurrentCalls.IntegerValue
                    };
                    // Allows to get notified of any errors encountered by the message pump
                    options.ExceptionReceived += LogErrors;

                    blockingCollection = new BlockingCollection<Tuple<long, long, DirectionType>>();

                    timer = new System.Timers.Timer
                    {
                        AutoReset = true,
                        Enabled = true,
                        Interval = 1000 * txtRefreshInformation.IntegerValue
                    };
                    timer.Elapsed += timer_Elapsed;
                    autoComplete = checkBoxAutoComplete.Checked;
                    logging = checkBoxLogging.Checked;
                    verbose = checkBoxVerbose.Checked;
                    tracking = checkBoxTrackMessages.Checked;
                    graph = checkBoxGraph.Checked;
                    
                    
#pragma warning disable 4014
                    Task.Run(new Action(RefreshGraph));
                    Task.Run(() => messageReceiver.OnMessageAsync(ProcessMessage, options));    
#pragma warning restore 4014
                    GetElapsedTime();
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

        private void grouperStatistics_Resize(object sender, EventArgs e)
        {
            var width = (grouperStatistics.Size.Width - 48) / 2;
            txtMessagePerSecond.Size = new Size(width, txtMessagePerSecond.Size.Height);
            txtMessagesTotal.Size = new Size(width, txtMessagesTotal.Size.Height);
            txtMessagesTotal.Location = new Point(width + 32, txtMessagesTotal.Location.Y);
            lblMessagesTotal.Location = new Point(width + 32, lblMessagesTotal.Location.Y);
        }

        private void grouperOptions_Resize(object sender, EventArgs e)
        {
            var width = (grouperOptions.Size.Width - 80) / 4;
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
            checkBoxGraph.Location = new Point(grouperOptions.Size.Width - checkBoxGraph.Size.Width - 12, checkBoxGraph.Location.Y);
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

        private async Task ProcessMessage(BrokeredMessage message)
        {
            try
            {
                
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
                    writeToLog(builder.ToString(), false);
                }
                if (tracking)
                {
                    Invoke(new Action(() => messageBindingList.Add(message.Clone())));
                }
                if (graph)
                {
                    UpdateStatistics(1, GetElapsedTime(), DirectionType.Receive);
                }
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

        private async Task<MessageReceiver> CreateMessageReceiverAsync()
        {
            if (entityDescription is QueueDescription)
            {
                var currentQueue = entityDescription as QueueDescription;
                if (currentQueue.RequiresSession)
                {
                    var queueClient = serviceBusHelper.MessagingFactory.CreateQueueClient(currentQueue.Path, receiveMode);
                    while (messageReceiver == null && !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            return await queueClient.AcceptMessageSessionAsync(TimeSpan.FromSeconds(10));
                        }
                        catch (TimeoutException)
                        {
                            writeToLog(NoSessionAvailable);
                        }
                    }
                }
                else
                {
                    return await serviceBusHelper.MessagingFactory.CreateMessageReceiverAsync(currentQueue.Path, receiveMode);
                }
            }
            if (entityDescription is SubscriptionDescription)
            {
                var currentSubscription = entityDescription as SubscriptionDescription;
                if (currentSubscription.RequiresSession)
                {
                    var subscriptionClient = serviceBusHelper.MessagingFactory.CreateSubscriptionClient(currentSubscription.TopicPath,
                                                                                                        currentSubscription.Name,
                                                                                                        receiveMode);
                    while (messageReceiver == null && !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            return await subscriptionClient.AcceptMessageSessionAsync(TimeSpan.FromSeconds(10));
                        }
                        catch (TimeoutException)
                        {
                            writeToLog(NoSessionAvailable);
                        }
                    }
                }
                else
                {
                    return await serviceBusHelper.MessagingFactory.CreateMessageReceiverAsync(SubscriptionClient.FormatSubscriptionPath(currentSubscription.TopicPath,
                                                                                                                                        currentSubscription.Name),
                                                                                                         receiveMode);
                }
            }
            return null;
        }

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

        private void StopListener()
        {
            lock (this)
            {
                stopping = true;
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
            if (messageReceiver != null)
            {
                messageReceiver.Close();
                messageReceiver = null;
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
                    long cycles = 0;
                    while (receiveMessageNumber < max && cycles < max)
                    {
                        cycles++;
                        Tuple<long, long, DirectionType> tuple;
                        var ok = blockingCollection.TryTake(out tuple, 10);
                        if (!ok)
                        {
                            continue;
                        }
                        receiveMessageNumber += tuple.Item1;
                        receiveTotalTime += tuple.Item2;
                        if (receiveMessageNumber > max)
                        {
                            max = receiveMessageNumber;
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
                    receiverMessagesPerSecond = receiverTotalTime > 0 ? receiverMessageNumber / receiverTotalTime : 0;

                    txtMessagePerSecond.Text = string.Format(LabelFormat, receiverMessagesPerSecond);
                    txtMessagePerSecond.Refresh();
                    txtMessagesTotal.Text = receiverMessageNumber.ToString(CultureInfo.InvariantCulture);
                    txtMessagesTotal.Refresh();

                    chart.Series["ReceiverLatency"].Points.AddXY(receiverMessageNumber, elapsedSeconds);
                    chart.Series["ReceiverThroughput"].Points.AddXY(receiverMessageNumber, receiverMessagesPerSecond);
                }
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
        #endregion
    }
}