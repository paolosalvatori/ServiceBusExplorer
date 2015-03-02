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
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class MetricValueControl : UserControl
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        #endregion

        #region Private Fields
        private readonly WriteToLogDelegate writeToLog;
        private readonly Action closeAction;
        private readonly BindingList<MetricValue> bindingList;
        private readonly IList<MetricValue> metricList;
        private readonly MetricDataPoint metricDataPoint;
        private readonly MetricInfo metricInfo;
        private VScrollBar verticalScrollbar;
        #endregion

        #region Public Constructors
        public MetricValueControl(WriteToLogDelegate writeToLog, 
                             Action closeAction, 
                             IEnumerable<MetricValue> metricValues, 
                             MetricDataPoint metricDataPoint,  
                             MetricInfo metricInfo)
        {
            this.writeToLog = writeToLog;
            this.closeAction = closeAction;
            this.metricDataPoint = metricDataPoint;
            this.metricInfo = metricInfo;
            InitializeComponent();
            metricList = metricValues as IList<MetricValue> ?? metricValues.ToList();
            bindingList = metricValues != null ?
                          new BindingList<MetricValue>(metricList)
                            {
                                AllowNew = true,
                                AllowEdit = true,
                                AllowRemove = true
                            } : 
                          new BindingList<MetricValue>
                            {
                                AllowNew = true,
                                AllowEdit = true,
                                AllowRemove = true
                            };
            InitializeControls();
        } 
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            try
            {
                // Set Grid style
                metricDataGridView.EnableHeadersVisualStyles = false;

                // Set the selection background color for all the cells.
                metricDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                metricDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                metricDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                metricDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                metricDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //filtersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //filtersDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                metricDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                metricDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                metricDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                metricDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Initialize the DataGridView.
                bindingSource.DataSource = bindingList;
                metricDataGridView.AutoSize = true;
                metricDataGridView.DataSource = bindingSource;
                metricDataGridView.ForeColor = SystemColors.WindowText;


                // Initialize combobox
                foreach (var item in Enum.GetValues(typeof(SeriesChartType)))
                {
                    cboChartType.Items.Add(item);
                    cboChartType.SelectedItem = SeriesChartType.FastLine;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
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

        private void grouperMetric_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     metricDataGridView.Location.X - 1,
                                     metricDataGridView.Location.Y - 1,
                                     metricDataGridView.Size.Width + 1,
                                     metricDataGridView.Size.Height + 1);
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (closeAction != null)
            {
                closeAction();
            }
        }

        private void metricDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateColumnWidth();
        }

        private void metricDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateColumnWidth();
        }

        private void metricDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateColumnWidth();
        }

        private void verticalScrollbar_VisibleChanged(object sender, EventArgs e)
        {
            CalculateColumnWidth();
        }

        private void CalculateColumnWidth()
        {
            try
            {
                metricDataGridView.SuspendDrawing();
                metricDataGridView.SuspendLayout();
                if (metricDataGridView.Columns.Count == 0)
                {
                    return;
                }
                var width = metricDataGridView.Width - metricDataGridView.RowHeadersWidth;
                if (verticalScrollbar == null)
                {
                    verticalScrollbar = metricDataGridView.Controls.OfType<VScrollBar>().First();
                    verticalScrollbar.VisibleChanged += verticalScrollbar_VisibleChanged;
                }
                if (verticalScrollbar != null && verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                var columnWidth = width / metricDataGridView.Columns.Count;
                if (columnWidth > 112)
                {
                    metricDataGridView.Columns[metricDataGridView.Columns.Count - 1].Width = columnWidth + (width - metricDataGridView.Columns.Count * columnWidth);
                    for (var i = 0; i < metricDataGridView.Columns.Count - 1; i++)
                    {
                        metricDataGridView.Columns[i].Width = columnWidth;
                    }
                }
                else
                {
                    columnWidth = (width - 112) / (metricDataGridView.Columns.Count - 1);
                    metricDataGridView.Columns[metricDataGridView.Columns.Count - 1].Width = width - (metricDataGridView.Columns.Count - 1) * columnWidth;
                    for (var i = 0; i < metricDataGridView.Columns.Count - 1; i++)
                    {
                        metricDataGridView.Columns[i].Width = columnWidth;
                    }
                }
            }
            finally
            {
                metricDataGridView.ResumeLayout();
                metricDataGridView.ResumeDrawing();
            }
        }
        
        private void MetricControl_Load(object sender, EventArgs e)
        {
            grouperMetric.GroupTitle = Tag as string;
            CreateChart();
        }

        private void CreateChart()
        {
            try
            {
                var metrics = new List<string>{"Min", "Max", "Total", "Average"};
                foreach (var metric in metrics)
                {
                    try
                    {
                        var series = chart.Series.Add(metric);
                        series.ChartType = SeriesChartType.FastLine;
                        series.XAxisType = AxisType.Primary;
                        series.YAxisType = AxisType.Primary;
                        series.Legend = "Default";
                        series.LegendText = metric;
                        foreach (var value in metricList)
                        {
                            switch (metric)
                            {
                                case "Min":
                                    series.Points.AddXY(value.Timestamp.ToString(CultureInfo.InvariantCulture), value.Min);
                                    break;
                                case "Max":
                                    series.Points.AddXY(value.Timestamp.ToString(CultureInfo.InvariantCulture), value.Max);
                                    break;
                                case "Total":
                                    series.Points.AddXY(value.Timestamp.ToString(CultureInfo.InvariantCulture), value.Total);
                                    break;
                                case "Average":
                                    series.Points.AddXY(value.Timestamp.ToString(CultureInfo.InvariantCulture), value.Average);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
                chart.ChartAreas[0].Axes[0].Title = "Time";
                chart.ChartAreas[0].Axes[1].Title = metricInfo.Unit;
                chart.Titles[0].Text = !string.IsNullOrWhiteSpace(metricInfo.DisplayName)
                                           ? metricInfo.DisplayName
                                           : metricDataPoint.Metric;
            }
            catch (Exception ex)
            {
               HandleException(ex);
            }
        }

        private void sessionsSplitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        {
            chart.Size = new Size(sessionsSplitContainer.Panel2.Size.Width + 4,
                                  sessionsSplitContainer.Panel2.Size.Height - cboChartType.Size.Height - 10);
            chart.Location = new Point(0, 8);
            cboChartType.Size = new Size(sessionsSplitContainer.Panel2.Size.Width - 2, cboChartType.Size.Height);
            cboChartType.Location = new Point(1, sessionsSplitContainer.Panel2.Size.Height - cboChartType.Size.Height - 1);
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

        private void metricDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
        #endregion
    }
}
