#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Properties;
using Cursor = System.Windows.Forms.Cursor;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class MetricMonitorControl : UserControl
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string GraphTabPage = "tabPageGraph";
        private const string GrouperFormat1 = "Entity: [{0}] Metric: [{1}] Unit: [{2}]";
        private const string GrouperFormat2 = "Metric: [{0}] Unit: [{1}]";

        //***************************
        // Constants
        //***************************
        private const string EntityProperty = "Entity";
        private const string TypeProperty = "Type";
        private const string MonitorProperty = "Monitor";
        private const string WarningProperty = "Warning";
        private const string CriticalProperty = "Critical";
        private const string CurrentProperty = "Current";
        private const string StateProperty = "State";
        private const string MetricProperty = "Metric";
        private const string GranularityProperty = "Granularity";
        private const string TimeFilterOperator = "Operator";
        private const string TimeFilterValue = "Value";
        private const string TimeFilterOperator1Name = "FilterOperator1";
        private const string TimeFilterOperator2Name = "FilterOperator2";
        private const string TimeFilterValue1Name = "FilterValue1";
        private const string TimeFilterValue2Name = "FilterValue2";
        private const string GraphName = "Graph";
        private const string FriendlyNameProperty = "FriendlyName";
        private const string NameProperty = "Name";
        private const string SelectEntityDialogTitle = "Select an Entity";
        private const string SelectEntityGrouperTitle = "Entity";
        private const string SelectEntityLabelText = "Name:";
        private const string Unknown = "Unkown";
        private const string SaveAsTitle = "Save File As";
        private const string XmlExtension = "xml";
        private const string XmlFilter = "XML Files|*.xml";
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string SubscriptionEntity = "Subscription";
        private const string ActiveMessageCount = "ActiveMessageCount";
        private const string DeadletterMessageCount = "DeadletterMessageCount";
        private const string SizeInKb = "SizeInKB";

        //***************************
        // Messages
        //***************************
        private const string RefreshIntervalSet = "The monitor refresh interval has been set to {0} seconds.";

        //***************************
        // Regular Expressions
        //***************************
        private const string RegularExpression = @"(?i)(queues|topics|\S+/Subscriptions/\S+)";

        //***************************
        // Metrics DataGridView Column Indexes
        //***************************
        private const int EntityMetricsDataGridViewColumnIndex = 0;
        private const int TypeMetricsDataGridViewColumnIndex = 1;
        
        //***************************
        // Monitor DataGridView Column Indexes
        //***************************
        private const int EntityMonitorDataGridViewColumnIndex = 0;
        private const int TypeMonitorDataGridViewColumnIndex = 1;
        private const int MonitorMonitorDataGridViewColumnIndex = 2;
        private const int WarningMonitorDataGridViewColumnIndex = 3;
        private const int CriticalMonitorDataGridViewColumnIndex = 4;
        private const int CurrentMonitorDataGridViewColumnIndex = 5;
        private const int StateMonitorDataGridViewColumnIndex = 6;
        

        //***************************
        // ListView Column Indexes
        //***************************
        private const int StateListViewColumnIndex = 0;
        private const int EntityListViewColumnIndex = 1;
        private const int MonitorListViewColumnIndex = 2;
        private const int TimestampListViewColumnIndex = 3;

        //***************************
        // Tab Pages Indexes
        //***************************
        private const int QueryTabPageIndex = 0;
        private const int MonitorTabPageIndex = 1;
        #endregion

        #region Private Instance Fields
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly BindingSource dataPointBindingSource = new BindingSource();
        private BindingList<MetricDataPoint> dataPointBindingList;
        private BindingList<MonitorRule> monitorRuleBindingList;
        private readonly string entityName;
        private readonly string entityType;
        private readonly bool existingEntity;
        private VScrollBar monitorRuleDataGridViewVerticalScrollbar;
        private bool importingMonitorRules;
        #endregion

        #region Private Static Fields
        private static readonly List<string> operators = new List<string> { "ge", "gt", "le", "lt", "eq", "ne" };
        private static readonly List<string> timeGranularityList = new List<string> { "PT5M", "PT1H", "P1D", "P7D" };
        #endregion

        #region Public Constructors
        public MetricMonitorControl(WriteToLogDelegate writeToLog, 
                                    ServiceBusHelper serviceBusHelper, 
                                    IList<MetricDataPoint> dataPoints, 
                                    string entityName, 
                                    string entityType)
        {
            monitorRuleBindingList = new BindingList<MonitorRule>();
            dataPointBindingList = dataPoints != null
                              ? new BindingList<MetricDataPoint>(dataPoints)
                              {
                                  AllowNew = true,
                                  AllowEdit = true,
                                  AllowRemove = true
                              }
                              : new BindingList<MetricDataPoint>
                              {
                                  AllowNew = true,
                                  AllowEdit = true,
                                  AllowRemove = true
                              };
            this.writeToLog = writeToLog;
            this.serviceBusHelper = serviceBusHelper;
            this.entityName = entityName;
            this.entityType = entityType;
            var regex = new Regex(RegularExpression);
            existingEntity = !string.IsNullOrWhiteSpace(entityName) && 
                             !string.IsNullOrWhiteSpace(entityType) && 
                             regex.IsMatch(entityType);
            InitializeComponent();
            InitializeControls();
        }
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            try
            {
                // Set Monitor Refresh Interval
                txtMonitorRefreshTimeout.Text = MainForm.SingletonMainForm.MonitorRefreshInterval.ToString(CultureInfo.InvariantCulture);

                // Set splitter width
                metricMonitorRuleSplitContainer.SplitterWidth = 16;

                // Set Grid style
                monitorRuleDataGridView.EnableHeadersVisualStyles = false;

                // Set the selection background color for all the cells.
                monitorRuleDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                monitorRuleDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                monitorRuleDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                monitorRuleDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                monitorRuleDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //filtersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //filtersDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                monitorRuleDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                monitorRuleDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                monitorRuleDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                monitorRuleDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize the DataGridView.
                monitorRuleBindingSource.DataSource = monitorRuleBindingList;
                monitorRuleDataGridView.AutoGenerateColumns = false;
                monitorRuleDataGridView.AutoSize = true;
                monitorRuleDataGridView.DataSource = monitorRuleBindingSource;
                monitorRuleDataGridView.ForeColor = SystemColors.WindowText;

                if (!existingEntity)
                {
                    // Create the Entity column
                    var entityColumn = new DataGridViewTextBoxColumn
                    {
                        Name = EntityProperty,
                        DataPropertyName = EntityProperty,
                        ReadOnly = true,
                        Width = 100
                    };
                    monitorRuleDataGridView.Columns.Add(entityColumn);

                    // Create the type column
                    var typeColumn = new DataGridViewTextBoxColumn
                    {
                        Name = TypeProperty,
                        DataPropertyName = TypeProperty,
                        Width = 72,
                        ReadOnly = true
                    };
                    monitorRuleDataGridView.Columns.Add(typeColumn);
                }

                // Create the Monitor column
                var metricMonitorValueColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = MonitorInfo.MonitorInfos,
                    DataPropertyName = MonitorProperty,
                    DisplayMember = FriendlyNameProperty,
                    ValueMember = NameProperty,
                    Name = MonitorProperty,
                    Width = 132,
                    FlatStyle = FlatStyle.Flat,
                    DropDownWidth = 148,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                };
                monitorRuleDataGridView.Columns.Add(metricMonitorValueColumn);

                // Create the Warning Threshold column
                var warningThresholdValueColumn = new DataGridViewTextBoxColumn
                {
                    Name = WarningProperty,
                    DataPropertyName = WarningProperty,
                    Width = 50
                };
                monitorRuleDataGridView.Columns.Add(warningThresholdValueColumn);

                // Create the type column
                var criticalThresholdValueColumn = new DataGridViewTextBoxColumn
                {
                    Name = CriticalProperty,
                    DataPropertyName = CriticalProperty,
                    Width = 50
                };
                monitorRuleDataGridView.Columns.Add(criticalThresholdValueColumn);

                // Create the Current column
                var currentValueColumn = new DataGridViewTextBoxColumn
                {
                    Name = CurrentProperty,
                    DataPropertyName = CurrentProperty,
                    ReadOnly = true,
                    Width = 50
                };
                monitorRuleDataGridView.Columns.Add(currentValueColumn);

                // Create the State column
                var stateColumn = new DataGridViewImageColumn
                {
                    Name = StateProperty,
                    DataPropertyName = StateProperty,
                    ImageLayout = DataGridViewImageCellLayout.Stretch,
                    Width = 36,
                    ReadOnly = true,
                    DefaultCellStyle = { NullValue = null }
                };
                monitorRuleDataGridView.Columns.Add(stateColumn);

                // Initialize combobox
                foreach (var item in Enum.GetValues(typeof(SeriesChartType)))
                {
                    cboChartType.Items.Add(item);
                    cboChartType.SelectedItem = SeriesChartType.FastLine;
                }
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

                if (!existingEntity)
                {
                    // Create the Entity column
                    var entityColumn = new DataGridViewTextBoxColumn
                        {
                            Name = EntityProperty,
                            DataPropertyName = EntityProperty,
                            ReadOnly = true,
                            Width = 100
                        };
                    dataPointDataGridView.Columns.Add(entityColumn);

                    // Create the type column
                    var typeColumn = new DataGridViewTextBoxColumn
                    {
                        Name = TypeProperty,
                        DataPropertyName = TypeProperty,
                        Width = 72,
                        ReadOnly = true
                    };
                    dataPointDataGridView.Columns.Add(typeColumn);
                }
                
                // Create the Metric column
                var metricColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = MetricInfo.MetricInfos,
                    DataPropertyName = MetricProperty,
                    DisplayMember = FriendlyNameProperty,
                    ValueMember = NameProperty,
                    Name = MetricProperty,
                    Width = 132,
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
                    Width = 132
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
                    Width = 144
                };
                dataPointDataGridView.Columns.Add(value2Column);

                // Create graph column
                var graphColumn = new DataGridViewCheckBoxColumn
                    {
                        DataPropertyName = GraphName,
                        HeaderText = GraphName,
                        Name = GraphName,
                        Width = 50
                    };
                dataPointDataGridView.Columns.Add(graphColumn);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void CalculateColumnWidth()
        {
            try
            {
                dataPointDataGridView.SuspendDrawing();
                dataPointDataGridView.SuspendLayout();
                btnMetrics.Enabled = dataPointDataGridView.Rows.Count > 1;
                if (mainTabControl.SelectedIndex == 0)
                {
                    btnClearRules.Enabled = dataPointDataGridView.Rows.Count > 1;
                    btnExport.Enabled = dataPointDataGridView.Rows.Count > 1;
                }
                if (dataPointDataGridView.Columns.Count < 5)
                {
                    return;
                }
                var otherColumnsWidth = 0;
                for (var i = 0; i < dataPointDataGridView.Columns.Count; i++)
                {
                    if (i == EntityMetricsDataGridViewColumnIndex)
                    {
                        continue;
                    }
                    otherColumnsWidth += dataPointDataGridView.Columns[i].Width;
                }
                var width = dataPointDataGridView.Width - dataPointDataGridView.RowHeadersWidth - otherColumnsWidth;
                var verticalScrollbar = dataPointDataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                dataPointDataGridView.Columns[EntityMetricsDataGridViewColumnIndex].Width = width;
            }
            finally
            {
                dataPointDataGridView.ResumeLayout();
                dataPointDataGridView.ResumeDrawing();
            }
        }

        private void grouperDatapoints_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    dataPointDataGridView.Location.X - 1,
                                    dataPointDataGridView.Location.Y - 1,
                                    dataPointDataGridView.Size.Width + 1,
                                    dataPointDataGridView.Size.Height + 1);
        }

        private void dataPointDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == TypeMetricsDataGridViewColumnIndex)
            {
                return;
            }
            var currentRow = dataPointDataGridView.Rows[e.RowIndex];
            if (e.ColumnIndex != EntityMetricsDataGridViewColumnIndex)
            {
                if (string.IsNullOrWhiteSpace(currentRow.Cells[EntityMetricsDataGridViewColumnIndex].Value as string) ||
                    string.IsNullOrWhiteSpace(currentRow.Cells[TypeMetricsDataGridViewColumnIndex].Value as string))
                {
                    return;
                }
                dataPointDataGridView.NotifyCurrentCellDirty(true);
                return;
            }
            var form = new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText, true);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            dataPointDataGridView.NotifyCurrentCellDirty(true);
            currentRow.Cells[EntityProperty].Value = form.Path;
            currentRow.Cells[TypeProperty].Value = form.Type;
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

        private void dataPointDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateColumnWidth();
        }

        private void dataPointDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateColumnWidth();
        }

        private void dataPointDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateColumnWidth();
        }

        private void dataPointDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            switch (mainTabControl.SelectedIndex)
            {
                case QueryTabPageIndex:
                    dataPointDataGridView.Rows.Clear();
                    break;
                case MonitorTabPageIndex:
                    monitorRuleDataGridView.Rows.Clear();
                    chart.Series.Clear();
                    btnClearData.Enabled = false;
                    eventListView.Items.Clear();
                    break;
            }
        }

        private void btnCloseTabs_Click(object sender, EventArgs e)
        {
            if (mainTabControl.TabPages.Count <= 2)
            {
                return;
            }
            while (mainTabControl.TabPages.Count > 2)
            {
                mainTabControl.TabPages.RemoveAt(mainTabControl.TabPages.Count - 1);
            }
        }

        private void mainTabControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            btnCloseTabs.Enabled = mainTabControl.TabPages.Count > 1;
        }

        private void mainTabControl_ControlAdded(object sender, ControlEventArgs e)
        {
            btnCloseTabs.Enabled = mainTabControl.TabPages.Count > 1;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dataPointBindingList.Count == 0)
                {
                    return;
                }
                if (existingEntity)
                {
                    foreach (var item in dataPointBindingList)
                    {
                        item.Entity = entityName;
                        item.Type = entityType;
                    }
                }
                var uris = MetricHelper.BuildUriListForDataPointMetricQueries(MainForm.SingletonMainForm.SubscriptionId,
                                                                              serviceBusHelper.Namespace,
                                                                              dataPointBindingList);
                var uriList = uris as IList<Uri> ?? uris.ToList();
                if (uris == null || !uriList.Any())
                {
                    return;
                }
                
                var metricData = MetricHelper.ReadMetricDataUsingTasks(uriList, MainForm.SingletonMainForm.CertificateThumbprint);
                var metricList = metricData as IList<IEnumerable<MetricValue>> ?? metricData.ToList();
                if (metricData == null && metricList.Count == 0)
                {
                    return;
                }
                // Common Graph
                var graphDataPoints = dataPointBindingList.Where(d => d.Graph);
                if (graphDataPoints.Any())
                {
                    if (mainTabControl.TabPages.ContainsKey(GraphTabPage))
                    {
                        mainTabControl.TabPages.RemoveByKey(GraphTabPage);
                    }
                    mainTabControl.TabPages.Add(GraphTabPage, "Main Graph");
                    var tabPage = mainTabControl.TabPages[GraphTabPage];
                    tabPage.BackColor = Color.FromArgb(215, 228, 242);
                    tabPage.ForeColor = SystemColors.ControlText;
                    var metricGraphControl = new MetricGraphControl(writeToLog,
                                                                    () => mainTabControl.TabPages.RemoveByKey(GraphTabPage),
                                                                    metricList,
                                                                    dataPointBindingList.ToList())
                    {
                        Location = new Point(0, 0),
                        Dock = DockStyle.Fill
                    };
                    mainTabControl.TabPages[GraphTabPage].Controls.Add(metricGraphControl);
                }

                // Individual Metrics
                for (var i = 0; i < metricList.Count; i++)
                {
                    if (metricList[i] == null)
                    {
                        continue;
                    }
                    var metricInfo = MetricInfo.MetricInfos.FirstOrDefault(m => m.Name == dataPointBindingList[i].Metric);
                    var friendlyName = metricInfo != null ? metricInfo.FriendlyName : dataPointBindingList[i].Metric;
                    var unit = metricInfo != null ? metricInfo.Unit : Unknown;
                    var entity = dataPointBindingList[i].Type == SubscriptionEntity
                                     ? new Uri(string.Format("http://x/{0}", dataPointBindingList[i].Entity)).Segments[3]
                                     : dataPointBindingList[i].Entity;
                    var key = !existingEntity
                                    ? string.Format(@"{0}\{1}", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity),
                                                    friendlyName)
                                    : friendlyName;
                    if (mainTabControl.TabPages.ContainsKey(key))
                    {
                        mainTabControl.TabPages.RemoveByKey(key);
                    }
                    mainTabControl.TabPages.Add(key, key);
                    var tabPage = mainTabControl.TabPages[key];
                    tabPage.BackColor = Color.FromArgb(215, 228, 242);
                    tabPage.ForeColor = SystemColors.ControlText;

                    var metricValueControl = new MetricValueControl(writeToLog,
                                                                    () => mainTabControl.TabPages.RemoveByKey(key),
                                                                    metricList[i],
                                                                    dataPointBindingList[i],
                                                                    metricInfo)
                        {
                            Location = new Point(0, 0),
                            Dock = DockStyle.Fill,
                            Tag = !existingEntity
                                      ? string.Format(GrouperFormat1, entity, friendlyName, unit)
                                      : string.Format(GrouperFormat2, friendlyName, unit)
                        };
                    mainTabControl.TabPages[key].Controls.Add(metricValueControl);
                }
                if (mainTabControl.TabPages.ContainsKey(GraphTabPage))
                {
                    mainTabControl.SelectTab(GraphTabPage);
                    return;
                }
                if (metricList.Count > 0)
                {
                    mainTabControl.SelectTab(1);
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
        
        private void btnImport_Click(object sender, EventArgs e)
        {
            switch (mainTabControl.SelectedIndex)
            {
                case QueryTabPageIndex:
                    if (dataPointBindingList.Count > 0)
                    {
                        dataPointBindingList.Clear();
                    }
                    openFileDialog.FileName = string.Empty;
                    openFileDialog.DefaultExt = XmlExtension;
                    openFileDialog.Filter = XmlFilter;
                    if (openFileDialog.ShowDialog() != DialogResult.OK ||
                        string.IsNullOrWhiteSpace(openFileDialog.FileName) ||
                        !File.Exists(openFileDialog.FileName))
                    {
                        return;
                    }
                    using (var reader = new StreamReader(openFileDialog.FileName))
                    {
                        var xml = reader.ReadToEnd();
                        if (!XmlHelper.IsXml(xml))
                        {
                            return;
                        }
                        var list = XmlSerializerHelper.XmlDeserialize(xml, typeof(MetricDataPointList)) as MetricDataPointList;
                        if (list == null)
                        {
                            return;
                        }
                        try
                        {
                            dataPointDataGridView.SuspendDrawing();
                            dataPointDataGridView.SuspendLayout();
                            dataPointBindingList = new BindingList<MetricDataPoint>(list);
                            dataPointBindingSource.DataSource = dataPointBindingList;
                            dataPointDataGridView.DataSource = dataPointBindingSource;
                        }
                        finally
                        {
                           dataPointDataGridView.ResumeLayout();
                            dataPointDataGridView.ResumeDrawing();
                        }
                    }
                    break;
                case MonitorTabPageIndex:
                    if (monitorRuleBindingList.Count > 0)
                    {
                        monitorRuleBindingList.Clear();
                    }
                    openFileDialog.FileName = string.Empty;
                    openFileDialog.DefaultExt = XmlExtension;
                    openFileDialog.Filter = XmlFilter;
                    if (openFileDialog.ShowDialog() != DialogResult.OK ||
                        string.IsNullOrWhiteSpace(openFileDialog.FileName) ||
                        !File.Exists(openFileDialog.FileName))
                    {
                        return;
                    }
                    using (var reader = new StreamReader(openFileDialog.FileName))
                    {
                        var xml = reader.ReadToEnd();
                        if (!XmlHelper.IsXml(xml))
                        {
                            return;
                        }
                        var list = XmlSerializerHelper.XmlDeserialize(xml, typeof(MonitorRuleList)) as MonitorRuleList;
                        if (list == null)
                        {
                            return;
                        }
                        monitorRuleBindingList = new BindingList<MonitorRule>(list);
                        monitorRuleBindingSource.DataSource = monitorRuleBindingList;
                        try
                        {
                            importingMonitorRules = true;
                            monitorRuleDataGridView.SuspendDrawing();
                            monitorRuleDataGridView.SuspendLayout();
                            monitorRuleDataGridView.DataSource = monitorRuleBindingSource;
                            for (var i = 0; i < monitorRuleDataGridView.Rows.Count; i++)
                            {
                                ValidateRow(i, false);
                            }
                            Task.Factory.StartNew(ReadMonitorData);
                        }
                        finally
                        {
                            monitorRuleDataGridView.ResumeLayout();
                            monitorRuleDataGridView.ResumeDrawing();
                            importingMonitorRules = false;
                        }
                    }
                    break;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            switch (mainTabControl.SelectedIndex)
            {
                case QueryTabPageIndex:
                    {
                        if (dataPointBindingList.Count == 0)
                        {
                            return;
                        }
                        saveFileDialog.Title = SaveAsTitle;
                        saveFileDialog.DefaultExt = XmlExtension;
                        saveFileDialog.Filter = XmlFilter;
                        saveFileDialog.FileName = CreateExportFileNameForMetrics();
                        if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                            string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                        {
                            return;
                        }
                        if (File.Exists(saveFileDialog.FileName))
                        {
                            File.Delete(saveFileDialog.FileName);
                        }
                        var xml = XmlSerializerHelper.XmlSerialize(new MetricDataPointList(dataPointBindingList));
                        if (string.IsNullOrWhiteSpace(xml))
                        {
                            return;
                        }
                        using (var writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            writer.Write(xml);
                        }
                    }
                    break;
                case MonitorTabPageIndex:
                    {
                        if (monitorRuleBindingList.Count == 0)
                        {
                            return;
                        }
                        saveFileDialog.Title = SaveAsTitle;
                        saveFileDialog.DefaultExt = XmlExtension;
                        saveFileDialog.Filter = XmlFilter;
                        saveFileDialog.FileName = CreateExportFileNameForMonitorRules();
                        if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                            string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                        {
                            return;
                        }
                        if (File.Exists(saveFileDialog.FileName))
                        {
                            File.Delete(saveFileDialog.FileName);
                        }
                        var xml = XmlSerializerHelper.XmlSerialize(new MonitorRuleList(monitorRuleBindingList));
                        if (string.IsNullOrWhiteSpace(xml))
                        {
                            return;
                        }
                        using (var writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            writer.Write(xml);
                        }
                    }
                    break;
            }
        }

        private string CreateExportFileNameForMetrics()
        {
            return string.Format("{0}_MetricDataPoints_{1}.xml", 
                                 CultureInfo.CurrentCulture.TextInfo.ToTitleCase(serviceBusHelper.Namespace),
                                 DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }

        private string CreateExportFileNameForMonitorRules()
        {
            return string.Format("{0}_MonitorRules_{1}.xml",
                                 CultureInfo.CurrentCulture.TextInfo.ToTitleCase(serviceBusHelper.Namespace),
                                 DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }

        private void monitorRuleDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateColumnWidthForMonitorRuleDataGridView();
        }

        private void monitorRuleDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateColumnWidthForMonitorRuleDataGridView();
        }

        private void monitorRuleDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateColumnWidthForMonitorRuleDataGridView();
            var ok = monitorRuleBindingList.Any(m => m.Valid);
            if (timer.Enabled && !ok)
            {
                timer.Enabled = false;
            }
        }

        private void MonitorRuleDataGridViewVerticalScrollbarVisibleChanged(object sender, EventArgs e)
        {
            CalculateColumnWidthForMonitorRuleDataGridView();
        }

        private void CalculateColumnWidthForMonitorRuleDataGridView()
        {
            try
            {
                monitorRuleDataGridView.SuspendDrawing();
                monitorRuleDataGridView.SuspendLayout();
                if (mainTabControl.SelectedIndex == 1)
                {
                    btnClearRules.Enabled = monitorRuleDataGridView.Rows.Count > 1;
                    btnExport.Enabled = monitorRuleDataGridView.Rows.Count > 1;
                }
                if (monitorRuleDataGridView.Columns.Count < 7)
                {
                    return;
                }
                var width = monitorRuleDataGridView.Width - monitorRuleDataGridView.RowHeadersWidth;
                if (monitorRuleDataGridViewVerticalScrollbar == null)
                {
                    monitorRuleDataGridViewVerticalScrollbar = monitorRuleDataGridView.Controls.OfType<VScrollBar>().First();
                    monitorRuleDataGridViewVerticalScrollbar.VisibleChanged += MonitorRuleDataGridViewVerticalScrollbarVisibleChanged;
                }
                if (monitorRuleDataGridViewVerticalScrollbar != null && monitorRuleDataGridViewVerticalScrollbar.Visible)
                {
                    width -= monitorRuleDataGridViewVerticalScrollbar.Width;
                }
                var otherColumnWidth = monitorRuleDataGridView.Columns[TypeMonitorDataGridViewColumnIndex].Width +
                                       monitorRuleDataGridView.Columns[WarningMonitorDataGridViewColumnIndex].Width +
                                       monitorRuleDataGridView.Columns[CriticalMonitorDataGridViewColumnIndex].Width +
                                       monitorRuleDataGridView.Columns[CurrentMonitorDataGridViewColumnIndex].Width +
                                       monitorRuleDataGridView.Columns[StateMonitorDataGridViewColumnIndex].Width;
                var columnWidth = (width - otherColumnWidth) / 2;
                monitorRuleDataGridView.Columns[EntityMonitorDataGridViewColumnIndex].Width = columnWidth + width - otherColumnWidth - columnWidth * 2;
                monitorRuleDataGridView.Columns[MonitorMonitorDataGridViewColumnIndex].Width = columnWidth;
            }
            finally
            {
                monitorRuleDataGridView.ResumeLayout();
                monitorRuleDataGridView.ResumeDrawing();
            }
        }

        private void metricMonitorRuleSplitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        {
            chart.Size = new Size(metricMonitorRuleSplitContainer.Panel2.Size.Width + 4,
                                  metricMonitorRuleSplitContainer.Panel2.Size.Height - cboChartType.Size.Height - 10);
            chart.Location = new Point(0, 8);
            cboChartType.Size = new Size(metricMonitorRuleSplitContainer.Panel2.Size.Width - 2, cboChartType.Size.Height);
            cboChartType.Location = new Point(1, metricMonitorRuleSplitContainer.Panel2.Size.Height - cboChartType.Size.Height - 1);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     cboChartType.Location.X - 1,
                                     cboChartType.Location.Y - 1,
                                     cboChartType.Size.Width + 1,
                                     cboChartType.Size.Height + 1);

        }

        private void cboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chart.Series.Count <= 0)
            {
                return;
            }
            foreach (var series in chart.Series)
            {
                series.ChartType = (SeriesChartType)cboChartType.SelectedItem;
            }
        }

        private void monitorRuleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == TypeMonitorDataGridViewColumnIndex)
            {
                return;
            }
            var currentRow = monitorRuleDataGridView.Rows[e.RowIndex];
            if (e.ColumnIndex != EntityMonitorDataGridViewColumnIndex)
            {
                if (string.IsNullOrWhiteSpace(currentRow.Cells[EntityMonitorDataGridViewColumnIndex].Value as string) ||
                    string.IsNullOrWhiteSpace(currentRow.Cells[TypeMonitorDataGridViewColumnIndex].Value as string))
                {
                    return;
                }
                monitorRuleDataGridView.NotifyCurrentCellDirty(true);
                return;
            }
            
            var form = new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText, true);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            monitorRuleDataGridView.NotifyCurrentCellDirty(true);
            currentRow.Cells[EntityProperty].Value = form.Path;
            currentRow.Cells[TypeProperty].Value = form.Type;
        }

        private void monitorRuleDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= Column_KeyPress;
            if (monitorRuleDataGridView.CurrentCell.ColumnIndex == 3 ||
                monitorRuleDataGridView.CurrentCell.ColumnIndex == 4)
            {
                var textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress += Column_KeyPress;
                }
            }
        }

        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        

        private void monitorRuleDataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            ValidateRow(e.RowIndex);
        }

        private void monitorRuleDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            ValidateRow(e.RowIndex, false);
        }

        private Series CreateSeries(MonitorRule rule)
        {
            if (rule == null)
            {
                return null;
            }
            try
            {
                try
                {
                    var monitorInfo = MonitorInfo.MonitorInfos.FirstOrDefault(m => m.Name == rule.Monitor);
                    if (monitorInfo == null)
                    {
                        return null;
                    }
                    var seriesName = string.Format(@"{0}\{1}",
                                                       CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rule.Entity),
                                                       monitorInfo.FriendlyName);
                    var series = chart.Series.Add(seriesName);
                    series.ChartType = SeriesChartType.FastLine;
                    series.XAxisType = AxisType.Primary;
                    series.YAxisType = AxisType.Primary;
                    series.Legend = "Default";
                    series.LegendText = seriesName;
                    return series;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return null;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ReadMonitorData();
        }

        private void ValidateRow(int rowIndex, bool logEnabled = true)
        {
            try
            {
                if (rowIndex == monitorRuleDataGridView.Rows.Count - 1)
                {
                    return;
                }
                var currentRow = monitorRuleDataGridView.Rows[rowIndex];
                var currentRule = monitorRuleBindingList[rowIndex];
                if (string.IsNullOrWhiteSpace(currentRow.Cells[EntityProperty].Value as string))
                {
                    currentRule.Valid = false;
                    return;
                }
                if (string.IsNullOrWhiteSpace(currentRow.Cells[TypeProperty].Value as string))
                {
                    currentRule.Valid = false;
                    return;
                }
                if (string.IsNullOrWhiteSpace(currentRow.Cells[MonitorProperty].Value as string))
                {
                    currentRule.Valid = false;
                    return;
                }
                var warning = (long)currentRow.Cells[WarningProperty].Value;
                var critical = (long)currentRow.Cells[CriticalProperty].Value;
                if (warning < 0)
                {
                    currentRule.Valid = false;
                    if (logEnabled)
                    {
                        writeToLog("The value of the Warning column must be a positive integer number.");
                    }
                    return;
                }
                if (critical < 0)
                {
                    currentRule.Valid = false;
                    if (logEnabled)
                    {
                        writeToLog("The value of the Critical column must be a positive integer number.");
                    }
                    return;
                }
                if (warning > 0 && critical > 0 && warning >= critical)
                {
                    currentRule.Valid = false;
                    if (logEnabled)
                    {
                        writeToLog("The value of the Warning column cannot be greater or equal than the value of the Critical column.");
                    }
                    return;
                }
                if (monitorRuleBindingList.Any(m => string.Compare(m.Entity, currentRule.Entity, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                                                    string.Compare(m.Type, currentRule.Type, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                                                    string.Compare(m.Monitor, currentRule.Monitor, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                                                    m != currentRule))
                {
                    currentRule.Valid = false;
                    if (logEnabled)
                    {
                        writeToLog("You can't define two rules for the same entity and the same value to monitor.");
                    }
                    return;
                }
                currentRule.Valid = true;
                if (currentRule.Series == null)
                {
                    currentRule.Series = CreateSeries(currentRule);
                }
                if (timer.Enabled)
                {
                    return;
                }
                timer.Enabled = true;
                if (!importingMonitorRules)
                {
                    Task.Factory.StartNew(ReadMonitorData);
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        private void monitorRuleDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (monitorRuleBindingList[e.Row.Index].Series != null)
            {
                chart.Series.Remove(monitorRuleBindingList[e.Row.Index].Series);
            }
        }

        private void monitorRuleDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != StateMonitorDataGridViewColumnIndex)
            {
                return;
            }
            if (e.Value is MonitorState)
            {
                // Ensure that the value is a string.
                var stateValue = (MonitorState)e.Value;

                // Set the cell ToolTip to the text value.
                var cell = monitorRuleDataGridView[e.ColumnIndex, e.RowIndex];
                cell.ToolTipText = stateValue.ToString();

                // Replace the string value with the image value. 
                switch (stateValue)
                {
                    case MonitorState.Normal:
                        e.Value = new Bitmap(Resources.Green);
                        break;
                    case MonitorState.Warning:
                        e.Value = new Bitmap(Resources.Yellow);
                        break;
                    case MonitorState.Critical:
                        e.Value = new Bitmap(Resources.Red);
                        break;
                }
            }
            else
            {
                e.Value = null; 
            }
        }

        private void ReadMonitorData()
        {
            try
            {
                var refresh = false;
                var now = DateTime.Now.ToLongTimeString();
                var max = (from monitorRule in monitorRuleBindingList 
                           where monitorRule.Valid && monitorRule.Series != null 
                           select monitorRule.Series.Points.Count).Max();
                foreach (var monitorRule in monitorRuleBindingList)
                {
                    if (!monitorRule.Valid || monitorRule.Series == null)
                    {
                        continue;
                    }
                    long value = 0;
                    switch (monitorRule.Type)
                    {
                        case QueueEntity:
                            var queueDescription = serviceBusHelper.GetQueue(monitorRule.Entity);
                            switch (monitorRule.Monitor)
                            {
                                case ActiveMessageCount :
                                    value = queueDescription.MessageCountDetails.ActiveMessageCount;
                                    break;
                                case DeadletterMessageCount :
                                    value = queueDescription.MessageCountDetails.DeadLetterMessageCount;
                                    break;
                                case SizeInKb :
                                    value = (long)Math.Floor(((decimal) queueDescription.SizeInBytes)/1024) + 1;
                                    break;
                            }
                            break;
                        case TopicEntity:
                            var topicDescription = serviceBusHelper.GetTopic(monitorRule.Entity);
                            switch (monitorRule.Monitor)
                            {
                                case ActiveMessageCount:
                                    value = topicDescription.MessageCountDetails.ActiveMessageCount;
                                    break;
                                case DeadletterMessageCount:
                                    value = topicDescription.MessageCountDetails.DeadLetterMessageCount;
                                    break;
                                case SizeInKb:
                                    value = (long)Math.Floor(((decimal)topicDescription.SizeInBytes) / 1024) + 1;
                                    break;
                            }
                            break;
                        case SubscriptionEntity:
                            var parts = monitorRule.Entity.Split('/');
                            if (parts.Length == 3)
                            {
                                var subscriptionDescription = serviceBusHelper.GetSubscription(parts[0], parts[2]);
                                switch (monitorRule.Monitor)
                                {
                                    case ActiveMessageCount:
                                        value = subscriptionDescription.MessageCountDetails.ActiveMessageCount;
                                        break;
                                    case DeadletterMessageCount:
                                        value = subscriptionDescription.MessageCountDetails.DeadLetterMessageCount;
                                        break;
                                    case SizeInKb:
                                        value = 0;
                                        break;
                                }
                            }
                            break;
                    }
                    var count = max - monitorRule.Series.Points.Count + 1;
                    for (var i = 0; i < count; i++)
                    {
                        if (InvokeRequired)
                        {
                            Invoke(new AddPointDelegate(AddPoint), new object[] { monitorRule, now, value });
                        }
                        else
                        {
                            monitorRule.Series.Points.AddXY(now, value);
                            btnClearData.Enabled = true;
                        }
                    }
                    if (value != monitorRule.Current)
                    {
                        monitorRule.Current = value;
                        refresh = true;
                    }
                    if (value >= monitorRule.Critical && monitorRule.Critical != 0)
                    {
                        if (monitorRule.State != MonitorState.Critical)
                        {
                            if (InvokeRequired)
                            {
                                Invoke(new AddEventDelegate(AddEvent), new object[] { monitorRule, MonitorState.Critical });
                            }
                            else
                            {
                                AddEvent(monitorRule, MonitorState.Critical);
                                btnClearData.Enabled = true;
                            }
                        }
                        monitorRule.State = MonitorState.Critical;
                        refresh = true;
                        continue;
                    }
                    if (value >= monitorRule.Warning && monitorRule.Warning != 0)
                    {
                        if (monitorRule.State != MonitorState.Warning)
                        {
                            if (InvokeRequired)
                            {
                                Invoke(new AddEventDelegate(AddEvent), new object[] { monitorRule, MonitorState.Warning });
                            }
                            else
                            {
                                AddEvent(monitorRule, MonitorState.Warning);
                                btnClearData.Enabled = true;
                            }
                        }
                        monitorRule.State = MonitorState.Warning;
                        refresh = true;
                        continue;
                    }
                    if (monitorRule.State != MonitorState.Normal)
                    {
                        if (InvokeRequired)
                        {
                            Invoke(new AddEventDelegate(AddEvent), new object[] { monitorRule, MonitorState.Normal });
                        }
                        else
                        {
                            AddEvent(monitorRule, MonitorState.Normal);
                            btnClearData.Enabled = true;
                        }
                    }
                    monitorRule.State = MonitorState.Normal; 
                }
                if (!refresh)
                {
                    return;
                }
                if (InvokeRequired)
                {
                    Invoke(new Action(monitorRuleDataGridView.Refresh));
                }
                else
                {
                    monitorRuleDataGridView.Refresh();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private delegate void AddPointDelegate(MonitorRule monitorRule, object xValue, long yValue);

        private void AddPoint(MonitorRule monitorRule, object xValue, long yValue)
        {
            monitorRule.Series.Points.AddXY(xValue, yValue);
            btnClearData.Enabled = true;
        }

        private delegate void AddEventDelegate(MonitorRule monitorRule, MonitorState state);

        private void AddEvent(MonitorRule monitorRule, MonitorState state)
        {
            var monitorInfo = MonitorInfo.MonitorInfos.FirstOrDefault(m => m.Name == monitorRule.Monitor);
            var value = monitorInfo != null ? monitorInfo.FriendlyName : monitorRule.Monitor;
            var listViewItem = new ListViewItem(state.ToString());
            listViewItem.SubItems.AddRange(new []{monitorRule.Entity,
                                                  value, 
                                                  DateTime.Now.ToLongTimeString()});
            eventListView.Items.Add(listViewItem);
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMonitorRefreshTimeout.Text))
            {
                return;
            }
            timer.Interval = txtMonitorRefreshTimeout.IntegerValue * 1000;
            writeToLog(string.Format(RefreshIntervalSet, txtMonitorRefreshTimeout.IntegerValue));
        }
        
        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (mainTabControl.SelectedIndex)
            {
                case QueryTabPageIndex:
                    btnClearRules.Enabled = dataPointDataGridView.Rows.Count > 1;
                    btnExport.Enabled = dataPointDataGridView.Rows.Count > 1;
                    btnClearData.Enabled = false;
                    break;
                case MonitorTabPageIndex:
                    btnClearRules.Enabled = monitorRuleDataGridView.Rows.Count > 1;
                    btnExport.Enabled = monitorRuleDataGridView.Rows.Count > 1;
                    btnClearData.Enabled = eventListView.Items.Count > 0;
                    break;
            }
        }

        private void btnClearEvents_Click(object sender, EventArgs e)
        {
            eventListView.Items.Clear();
            foreach (var serie in chart.Series)
            {
                serie.Points.Clear();
            }
            btnClearData.Enabled = false;
        }

        private void eventListView_Resize(object sender, EventArgs e)
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
                var width = listView.Width -
                        listView.Columns[StateListViewColumnIndex].Width -
                        listView.Columns[MonitorListViewColumnIndex].Width -
                        listView.Columns[TimestampListViewColumnIndex].Width;
                listView.Columns[EntityListViewColumnIndex].Width = width - 4;
            }
            finally
            {
                listView.ResumeLayout();
                listView.ResumeDrawing();
            }
        }

        private void eventListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

        private void eventListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //e.DrawDefault = true;
            //e.DrawBackground();
            //e.DrawText();
        }

        private void eventListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
            //e.DrawBackground();
            //e.DrawText();
        }

        private void grouperMonitorRules_CustomPaint(PaintEventArgs e)
        {
            monitorRuleDataGridView.Size = new Size(grouperMonitorRules.Size.Width - (monitorRuleDataGridView.Location.X * 2 + 2),
                                                    grouperMonitorRules.Size.Height - monitorRuleDataGridView.Location.Y - monitorRuleDataGridView.Location.X - 2);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     monitorRuleDataGridView.Location.X - 1,
                                     monitorRuleDataGridView.Location.Y - 1,
                                     monitorRuleDataGridView.Size.Width + 1,
                                     monitorRuleDataGridView.Size.Height + 1);
        }

        private void grouperEvents_CustomPaint(PaintEventArgs obj)
        {
            eventListView.Size = new Size(grouperEvents.Size.Width - eventListView.Location.X * 2,
                                          grouperEvents.Size.Height - eventListView.Location.Y - eventListView.Location.X);
        }
        #endregion
    }
}
