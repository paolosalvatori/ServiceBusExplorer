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
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class MetricGraphControl : UserControl
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
        private readonly IList<IEnumerable<MetricValue>> metricValueList;
        private readonly IList<MetricDataPoint> metricDataPointList;
        #endregion

        #region Public Constructors
        public MetricGraphControl(WriteToLogDelegate writeToLog, 
                                  Action closeAction, 
                                  IList<IEnumerable<MetricValue>> metricValueList, 
                                  IList<MetricDataPoint> metricDataPointList)
        {
            this.writeToLog = writeToLog;
            this.closeAction = closeAction;
            this.metricDataPointList = metricDataPointList;
            this.metricValueList = metricValueList;
            InitializeComponent();
            foreach (var item in Enum.GetValues(typeof (SeriesChartType)))
            {
                cboChartType.Items.Add(item);
                cboChartType.SelectedItem = SeriesChartType.FastLine;
            }
        } 
        #endregion

        #region Private Methods
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

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (closeAction != null)
            {
                closeAction();
            }
        }
        
        private void MetricControl_Load(object sender, EventArgs e)
        {
            CreateChart();
        }

        // ReSharper disable once FunctionComplexityOverflow
        private void CreateChart()
        {
            try
            {
                string unit = null;
                for (var i = 0; i < metricDataPointList.Count; i++)
                {
                    try
                    {
                        if (metricDataPointList[i] == null ||
                            string.IsNullOrWhiteSpace(metricDataPointList[i].Type) ||
                            !MetricInfo.EntityMetricDictionary.ContainsKey(metricDataPointList[i].Type))
                        {
                            continue;
                        }
                        var metricInfo = MetricInfo.EntityMetricDictionary[metricDataPointList[i].Type].FirstOrDefault(m => m.Name == metricDataPointList[i].Metric);
                        if (metricInfo == null)
                        {
                            continue;
                        }
                        var metricName = string.Format(@"{0}\{1}",
                                                       CultureInfo.CurrentCulture.TextInfo.ToTitleCase(metricDataPointList[i].Entity),
                                                       metricInfo.DisplayName);
                        if (chart.Series.Any(s => s.Name == metricName))
                        {
                            continue;
                        }
                        if (i == 0)
                        {
                            unit = metricInfo.Unit;
                        }
                        else
                        {
                            if (unit != null && !unit.Contains(metricInfo.Unit))
                            {
                                unit = string.Format(@"{0}\{1}", unit, metricInfo.Unit);
                            }
                        }
                        var series = chart.Series.Add(metricName);
                        series.ChartType = SeriesChartType.FastLine;
                        series.XAxisType = AxisType.Primary;
                        series.YAxisType = AxisType.Primary;
                        series.Legend = "Default";
                        series.LegendText = metricName;
                        foreach (var value in metricValueList[i])
                        {
                            switch (metricInfo.PrimaryAggregation)
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
                chart.ChartAreas[0].Axes[1].Title = unit;
                chart.Titles[0].Text = "Main Graph";
            }
            catch (Exception ex)
            {
               HandleException(ex);
            }
        }
        
        private void cboChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chart.Series.Count <= 0)
            {
                return;
            }
            foreach (var series in chart.Series)
            {
                series.ChartType = (SeriesChartType) cboChartType.SelectedItem;
            }
        }

        private void MetricGraphControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     cboChartType.Location.X - 1,
                                     cboChartType.Location.Y - 1,
                                     cboChartType.Size.Width + 1,
                                     cboChartType.Size.Height + 1);
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
