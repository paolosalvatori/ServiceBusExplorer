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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;
using System.Threading.Tasks;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class HandleConsumerGroupControl : UserControl
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

        //***************************
        // Texts
        //***************************
        private const string DeleteText = "Delete";
        private const string CreateText = "Create";
        private const string UpdateText = "Update";
        private const string CancelText = "Cancel";

        //***************************
        // Messages
        //***************************
        private const string PathCannotBeNull = "The Path field cannot be null.";

        //***************************
        // Tooltips
        //***************************
        private const string NameTooltip = "Gets or sets the consumerGroup name.";
        private const string UserMetadataTooltip = "Gets or sets the user metadata.";

        //***************************
        // Property Labels
        //***************************
        private const string EventHubPath = "Event Hub Path";
        private const string CreatedAt = "Created At";
        private const string UpdatedAt = "Updated At";
        private const string IsReadOnly = "Is ReadOnly";

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
        private const string DisplayNameProperty = "DisplayName";
        private const string NameProperty = "Name";
        private const string ConsumerGroupEntity = "Consumer Group";
        private const string Unknown = "Unkown";
        private const string DeleteName = "Delete";

        //***************************
        // Metrics Formats
        //***************************
        private const string MetricTabPageKeyFormat = "MetricTabPage{0}";
        private const string GrouperFormat = "Metric: [{0}] Unit: [{1}]";
        private const string ConsumerGroupPathFormat = "{0}|{1}";

        //***************************
        // Pages
        //***************************
        private const string MetricsTabPage = "tabPageMetrics";
        private const string PartitionsTabPage = "tabPagePartitions";

        //***************************
        // Tooltips
        //***************************
        private const string DeleteTooltip = "Delete the row.";
        #endregion

        #region Private Fields
        private ConsumerGroupDescription consumerGroupDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly string eventHubName;
        private readonly BindingSource dataPointBindingSource = new BindingSource();
        private readonly BindingSource partitionsBindingSource = new BindingSource();
        private readonly BindingList<MetricDataPoint> dataPointBindingList;
        private SortableBindingList<PartitionDescription> partitionsBindingList;
        private readonly List<TabPage> hiddenPages = new List<TabPage>();
        private readonly List<string> metricTabPageIndexList = new List<string>();
        private readonly ManualResetEvent metricsManualResetEvent = new ManualResetEvent(false);
        #endregion

        #region Private Static Fields
        private static readonly List<string> operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> timeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };
        #endregion

        #region Public Constructors
        public HandleConsumerGroupControl(WriteToLogDelegate writeToLog, ServiceBusHelper serviceBusHelper, ConsumerGroupDescription consumerGroupDescription, string eventHubName)
        {
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.consumerGroupDescription = consumerGroupDescription;
            this.eventHubName = eventHubName;
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
        #endregion

        #region Public Methods
        public void RefreshData(ConsumerGroupDescription consumerGroup)
        {
            try
            {
                consumerGroupDescription = consumerGroup;
                InitializeData();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void GetPartitions()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if (serviceBusHelper == null)
                    {
                        return;
                    }

                    // ConsumerGroup Node
                    if (consumerGroupDescription == null)
                    {
                        return;
                    }

                    var partitionEnumerable = serviceBusHelper.GetPartitions(consumerGroupDescription.EventHubPath, consumerGroupDescription.Name);
                    var partitionDescriptionList = partitionEnumerable as IList<PartitionDescription> ??
                                                   partitionEnumerable.ToList();
                    if (partitionEnumerable == null || !partitionDescriptionList.Any())
                    {
                        return;
                    }
                    partitionsBindingList = new SortableBindingList<PartitionDescription>(partitionDescriptionList)
                    {
                        AllowEdit = false,
                        AllowNew = false,
                        AllowRemove = false
                    };
                    partitionsBindingSource.DataSource = partitionsBindingList;
                    partitionsDataGridView.DataSource = partitionsBindingSource;

                    var dataGridViewColumn = partitionsDataGridView.Columns["IsReadOnly"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.Visible = false;
                    }

                    dataGridViewColumn = partitionsDataGridView.Columns["ExtensionData"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.Visible = false;
                    }

                    dataGridViewColumn = partitionsDataGridView.Columns["EventHubPath"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.Visible = false;
                    }

                    dataGridViewColumn = partitionsDataGridView.Columns["ConsumerGroupName"];
                    if (dataGridViewColumn != null)
                    {
                        dataGridViewColumn.Visible = false;
                    }

                    partitionsDataGridView.Refresh();

                    if (mainTabControl.TabPages[PartitionsTabPage] == null)
                    {
                        EnablePage(PartitionsTabPage);
                    }
                    if (mainTabControl.TabPages[PartitionsTabPage] != null)
                    {
                        mainTabControl.SelectTab(PartitionsTabPage);
                    }
                    CalculateLastColumnWidth();
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
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            // Set Grid style
            partitionsDataGridView.EnableHeadersVisualStyles = false;

            // Set the selection background color for all the cells.
            partitionsDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
            partitionsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            partitionsDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

            // Set the background color for all rows and for alternating rows.  
            // The value for alternating rows overrides the value for all rows. 
            partitionsDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
            partitionsDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
            //filtersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            //filtersDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Set the row and column header styles.
            partitionsDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            partitionsDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            partitionsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            partitionsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Initialize the DataGridView.
            partitionsDataGridView.AutoGenerateColumns = true;
            partitionsDataGridView.AutoSize = true;
            partitionsDataGridView.ForeColor = SystemColors.WindowText;

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

            if (consumerGroupDescription != null)
            {
                MetricInfo.GetMetricInfoListAsync(serviceBusHelper.Namespace,
                                             ConsumerGroupEntity,
                                             string.Format(ConsumerGroupPathFormat,
                                                           consumerGroupDescription.EventHubPath,
                                                           consumerGroupDescription.Name)).ContinueWith(t => metricsManualResetEvent.Set());
            }

            if (dataPointDataGridView.Columns.Count == 0)
            {
                // Create the Metric column
                var metricColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = MetricInfo.EntityMetricDictionary.ContainsKey(ConsumerGroupEntity) ?
                                 MetricInfo.EntityMetricDictionary[ConsumerGroupEntity] :
                                 null,
                    DataPropertyName = MetricProperty,
                    DisplayMember = DisplayNameProperty,
                    ValueMember = NameProperty,
                    Name = MetricProperty,
                    Width = 144,
                    DropDownWidth = 250,
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

                // Create delete column
                var deleteButtonColumn = new DataGridViewButtonColumn
                {
                    Name = DeleteName,
                    CellTemplate = new DataGridViewDeleteButtonCell(),
                    HeaderText = string.Empty,
                    Width = 22
                };
                deleteButtonColumn.CellTemplate.ToolTipText = DeleteTooltip;
                deleteButtonColumn.UseColumnTextForButtonValue = true;
                dataPointDataGridView.Columns.Add(deleteButtonColumn);
            }

            DisablePage(PartitionsTabPage);

            if (consumerGroupDescription != null)
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

                // Initialize buttons
                btnCreateDelete.Text = DeleteText;
                btnCancelUpdate.Text = UpdateText;
                btnRefresh.Visible = true;
                btnMetrics.Visible = true;
                btnCloseTabs.Visible = true;
                btnGetPartitions.Visible = true;

                // Initialize textboxes
                txtName.ReadOnly = true;
                txtName.BackColor = SystemColors.Window;
                txtName.GotFocus += textBox_GotFocus;

                // Initialize Data
                InitializeData();

                toolTip.SetToolTip(txtName, NameTooltip);
                toolTip.SetToolTip(txtUserMetadata, UserMetadataTooltip);

                propertyListView.ContextMenuStrip = entityInformationContextMenuStrip;
            }
            else
            {
                // Tab pages
                DisablePage(MetricsTabPage);

                // Initialize buttons
                btnCreateDelete.Text = CreateText;
                btnCancelUpdate.Text = CancelText;
                btnRefresh.Visible = false;
                btnMetrics.Visible = false;
                btnCloseTabs.Visible = false;
                btnGetPartitions.Visible = false;
            }
        }

        private void InitializeData()
        {
            // CheckpointEnabled
            //checkBoxEnableCheckpoint.Checked = (bool)enableCheckpointPropertyInfo.GetValue(consumerGroupDescription);

            // Initialize property grid
            var propertyList = new List<string[]>();

            propertyList.AddRange(new[]{new[]{EventHubPath, consumerGroupDescription.EventHubPath},
                                        new[]{IsReadOnly, consumerGroupDescription.IsReadOnly.ToString()},
                                        new[]{CreatedAt, consumerGroupDescription.CreatedAt.ToString(CultureInfo.CurrentCulture)},
                                        new[]{UpdatedAt, consumerGroupDescription.UpdatedAt.ToString(CultureInfo.CurrentCulture)}});

            propertyListView.Items.Clear();
            foreach (var array in propertyList)
            {
                propertyListView.Items.Add(new ListViewItem(array));
            }

            // Path
            if (!string.IsNullOrWhiteSpace(consumerGroupDescription.Name))
            {
                txtName.Text = consumerGroupDescription.Name;
            }

            // UserMetadata
            if (!string.IsNullOrWhiteSpace(consumerGroupDescription.UserMetadata))
            {
                txtUserMetadata.Text = consumerGroupDescription.UserMetadata;
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
                    using (var deleteForm = new DeleteForm(consumerGroupDescription.Name, ConsumerGroupEntity.ToLower()))
                    {
                        if (deleteForm.ShowDialog() == DialogResult.OK)
                        {
                            serviceBusHelper.DeleteConsumerGroup(consumerGroupDescription);
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        writeToLog(PathCannotBeNull);
                        return;
                    }
                    //var description = new ConsumerGroupDescription(eventHubName, txtName.Text)
                    //    {
                    //        UserMetadata = txtUserMetadata.Text,
                    //        EnableCheckpoint = checkBoxEnableCheckpoint.Enabled
                    //    };

                    var description = new ConsumerGroupDescription(eventHubName, txtName.Text)
                    {
                        UserMetadata = txtUserMetadata.Text
                    };
                    
                    consumerGroupDescription = serviceBusHelper.CreateConsumerGroup(description);
                    InitializeControls();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
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

        private async void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            bool ok = true;

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
                    consumerGroupDescription.UserMetadata = txtUserMetadata.Text;
                    //consumerGroupDescription.EnableCheckpoint = checkBoxEnableCheckpoint.Enabled;
                    serviceBusHelper.UpdateConsumerGroup(consumerGroupDescription);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    ok = false;
                }
                finally
                {
                    InitializeControls();
                }
                if (ok)
                {
                    return;
                }
                try
                {
                    consumerGroupDescription = await serviceBusHelper.NamespaceManager.GetConsumerGroupAsync(eventHubName, consumerGroupDescription.Name);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
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

        private void propertyListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

        private void propertyListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void propertyListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void propertyListView_Resize(object sender, EventArgs e)
        {
            try
            {
                propertyListView.SuspendDrawing();
                propertyListView.SuspendLayout();
                var width = propertyListView.Width - propertyListView.Columns[0].Width - 4;
                var scrollbars = ScrollBarHelper.GetVisibleScrollbars(propertyListView);
                if (scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both)
                {
                    width -= 17;
                }
                propertyListView.Columns[1].Width = width;
            }
            finally
            {
                propertyListView.ResumeLayout();
                propertyListView.ResumeDrawing();
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

                builder.Append(string.Format("Name, {0}", consumerGroupDescription.Name));
                builder.AppendLine();
                builder.Append(string.Format("EventHubPath, {0}", consumerGroupDescription.EventHubPath));
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

        private void dataPointDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridViewColumn = dataPointDataGridView.Columns[DeleteName];
            if (dataGridViewColumn != null &&
                e.ColumnIndex == dataGridViewColumn.Index &&
                e.RowIndex > -1 &&
               !dataPointDataGridView.Rows[e.RowIndex].IsNewRow)
            {
                dataPointDataGridView.Rows.RemoveAt(e.RowIndex);
                return;
            }
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

        // ReSharper disable once FunctionComplexityOverflow
        private void btnMetrics_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MetricInfo.EntityMetricDictionary.ContainsKey(ConsumerGroupEntity))
                {
                    return;
                }
                if (metricTabPageIndexList.Count > 0)
                {
                    for (var i = 0; i < metricTabPageIndexList.Count; i++)
                    {
                        mainTabControl.TabPages.RemoveByKey(metricTabPageIndexList[i]);
                    }
                    metricTabPageIndexList.Clear();
                }
                Cursor.Current = Cursors.WaitCursor;
                if (dataPointBindingList.Count == 0)
                {
                    return;
                }
                foreach (var item in dataPointBindingList)
                {
                    item.Entity = string.Format(ConsumerGroupPathFormat, consumerGroupDescription.EventHubPath, consumerGroupDescription.Name);
                    item.Type = ConsumerGroupEntity;
                }
                BindingList<MetricDataPoint> pointBindingList;
                var allDataPoint = dataPointBindingList.FirstOrDefault(m => string.Compare(m.Metric, "all", StringComparison.OrdinalIgnoreCase) == 0);
                if (allDataPoint != null)
                {
                    pointBindingList = new BindingList<MetricDataPoint>();
                    foreach (var item in MetricInfo.EntityMetricDictionary[ConsumerGroupEntity])
                    {
                        if (string.Compare(item.Name, "all", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            continue;
                        }
                        pointBindingList.Add(new MetricDataPoint
                        {
                            Entity = allDataPoint.Entity,
                            FilterOperator1 = allDataPoint.FilterOperator1,
                            FilterOperator2 = allDataPoint.FilterOperator2,
                            FilterValue1 = allDataPoint.FilterValue1,
                            FilterValue2 = allDataPoint.FilterValue2,
                            Granularity = allDataPoint.Granularity,
                            Graph = allDataPoint.Graph,
                            Metric = item.Name,
                            Type = allDataPoint.Type
                        });
                    }
                }
                else
                {
                    pointBindingList = dataPointBindingList;
                }
                var uris = MetricHelper.BuildUriListForDataPointMetricQueries(MainForm.SingletonMainForm.SubscriptionId,
                    serviceBusHelper.Namespace,
                    pointBindingList);
                var uriList = uris as IList<Uri> ?? uris.ToList();
                if (uris == null || !uriList.Any())
                {
                    return;
                }
                var metricData = MetricHelper.ReadMetricDataUsingTasks(uriList,
                    MainForm.SingletonMainForm.CertificateThumbprint);
                var metricList = metricData as IList<IEnumerable<MetricValue>> ?? metricData.ToList();
                if (metricData == null && metricList.Count == 0)
                {
                    return;
                }
                for (var i = 0; i < metricList.Count; i++)
                {
                    if (metricList[i] == null || !metricList[i].Any())
                    {
                        continue;
                    }
                    var key = string.Format(MetricTabPageKeyFormat, i);
                    var metricInfo = MetricInfo.EntityMetricDictionary[ConsumerGroupEntity].FirstOrDefault(m => m.Name == pointBindingList[i].Metric);
                    var friendlyName = metricInfo != null ? metricInfo.DisplayName : pointBindingList[i].Metric;
                    var unit = metricInfo != null ? metricInfo.Unit : Unknown;
                    mainTabControl.TabPages.Add(key, friendlyName);
                    metricTabPageIndexList.Add(key);
                    var tabPage = mainTabControl.TabPages[key];
                    tabPage.BackColor = Color.FromArgb(215, 228, 242);
                    tabPage.ForeColor = SystemColors.ControlText;
                    var control = new MetricValueControl(writeToLog,
                        () => mainTabControl.TabPages.RemoveByKey(key),
                        metricList[i],
                        pointBindingList[i],
                        metricInfo)
                    {
                        Location = new Point(0, 0),
                        Dock = DockStyle.Fill,
                        Tag = string.Format(GrouperFormat, friendlyName, unit)
                    };
                    mainTabControl.TabPages[key].Controls.Add(control);
                    btnCloseTabs.Enabled = true;
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

        private void btnCloseTabs_Click(object sender, EventArgs e)
        {
            if (metricTabPageIndexList.Count <= 0)
            {
                return;
            }
            for (var i = 0; i < metricTabPageIndexList.Count; i++)
            {
                mainTabControl.TabPages.RemoveByKey(metricTabPageIndexList[i]);
            }
            metricTabPageIndexList.Clear();
            btnCloseTabs.Enabled = false;
        }

        private void mainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (string.Compare(e.TabPage.Name, MetricsTabPage, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                return;
            }
            Task.Run(() =>
            {
                metricsManualResetEvent.WaitOne();
                var dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)dataPointDataGridView.Columns[MetricProperty];
                if (dataGridViewComboBoxColumn != null)
                {
                    dataGridViewComboBoxColumn.DataSource = MetricInfo.EntityMetricDictionary.ContainsKey(ConsumerGroupEntity)
                        ? MetricInfo.EntityMetricDictionary[ConsumerGroupEntity]
                        : null;
                }
            });
        }

        private void partitionsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void partitionsDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void partitionsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void partitionsDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateLastColumnWidth(sender);
        }

        private void CalculateLastColumnWidth(object sender)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView == null)
            {
                return;
            }
            try
            {
                if (dataGridView.ColumnCount == 0)
                {
                    return;
                }
                dataGridView.SuspendDrawing();
                dataGridView.SuspendLayout();
                var width = dataGridView.Width - dataGridView.RowHeadersWidth;
                var verticalScrollbar = dataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                var columnCount = partitionsDataGridView.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible);
                var columnWidth = width/columnCount;
                for (var i = 0; i < partitionsDataGridView.Columns.Count; i++)
                {
                    partitionsDataGridView.Columns[i].Width = columnWidth;
                }
            }
            finally
            {
                dataGridView.ResumeLayout();
                dataGridView.ResumeDrawing();
            }
        }

        private void grouperPartitionsList_CustomPaint(PaintEventArgs e)
        {
            partitionsDataGridView.Size = new Size(
                grouperPartitionsList.Size.Width - (partitionsDataGridView.Location.X * 2 + 2),
                grouperPartitionsList.Size.Height - partitionsDataGridView.Location.Y - partitionsDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                partitionsDataGridView.Location.X - 1,
                partitionsDataGridView.Location.Y - 1,
                partitionsDataGridView.Size.Width + 1,
                partitionsDataGridView.Size.Height + 1);
        }

        private void btnGetPartitions_Click(object sender, EventArgs e)
        {
            GetPartitions();
        }
        #endregion
    }
}
