namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    partial class MetricValueControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblClose = new System.Windows.Forms.Label();
            this.sessionsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMetric = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.metricDataGridView = new System.Windows.Forms.DataGridView();
            this.cboChartType = new System.Windows.Forms.ComboBox();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sessionsSplitContainer)).BeginInit();
            this.sessionsSplitContainer.Panel1.SuspendLayout();
            this.sessionsSplitContainer.Panel2.SuspendLayout();
            this.sessionsSplitContainer.SuspendLayout();
            this.grouperMetric.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metricDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(125)))), ((int)(((byte)(150)))));
            this.lblClose.Location = new System.Drawing.Point(949, 2);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(15, 13);
            this.lblClose.TabIndex = 30;
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // sessionsSplitContainer
            // 
            this.sessionsSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionsSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.sessionsSplitContainer.Name = "sessionsSplitContainer";
            // 
            // sessionsSplitContainer.Panel1
            // 
            this.sessionsSplitContainer.Panel1.Controls.Add(this.grouperMetric);
            // 
            // sessionsSplitContainer.Panel2
            // 
            this.sessionsSplitContainer.Panel2.Controls.Add(this.cboChartType);
            this.sessionsSplitContainer.Panel2.Controls.Add(this.chart);
            this.sessionsSplitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.sessionsSplitContainer_Panel2_Paint);
            this.sessionsSplitContainer.Size = new System.Drawing.Size(936, 376);
            this.sessionsSplitContainer.SplitterDistance = 460;
            this.sessionsSplitContainer.SplitterWidth = 16;
            this.sessionsSplitContainer.TabIndex = 31;
            // 
            // grouperMetric
            // 
            this.grouperMetric.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMetric.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMetric.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMetric.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMetric.BorderThickness = 1F;
            this.grouperMetric.Controls.Add(this.metricDataGridView);
            this.grouperMetric.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMetric.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMetric.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMetric.ForeColor = System.Drawing.Color.White;
            this.grouperMetric.GroupImage = null;
            this.grouperMetric.GroupTitle = "Metric";
            this.grouperMetric.Location = new System.Drawing.Point(0, 0);
            this.grouperMetric.Name = "grouperMetric";
            this.grouperMetric.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMetric.PaintGroupBox = true;
            this.grouperMetric.RoundCorners = 4;
            this.grouperMetric.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMetric.ShadowControl = false;
            this.grouperMetric.ShadowThickness = 1;
            this.grouperMetric.Size = new System.Drawing.Size(460, 376);
            this.grouperMetric.TabIndex = 4;
            this.grouperMetric.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMetric_CustomPaint);
            // 
            // metricDataGridView
            // 
            this.metricDataGridView.AllowUserToAddRows = false;
            this.metricDataGridView.AllowUserToDeleteRows = false;
            this.metricDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metricDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.metricDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metricDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.metricDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.metricDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.metricDataGridView.Location = new System.Drawing.Point(16, 32);
            this.metricDataGridView.Name = "metricDataGridView";
            this.metricDataGridView.ReadOnly = true;
            this.metricDataGridView.RowHeadersWidth = 24;
            this.metricDataGridView.Size = new System.Drawing.Size(428, 328);
            this.metricDataGridView.TabIndex = 28;
            this.metricDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.metricDataGridView_DataError);
            this.metricDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.metricDataGridView_RowsAdded);
            this.metricDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.metricDataGridView_RowsRemoved);
            this.metricDataGridView.Resize += new System.EventHandler(this.metricDataGridView_Resize);
            // 
            // cboChartType
            // 
            this.cboChartType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChartType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboChartType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChartType.FormattingEnabled = true;
            this.cboChartType.Items.AddRange(new object[] {
            "PeekLock",
            "ReceiveDelete"});
            this.cboChartType.Location = new System.Drawing.Point(8, 336);
            this.cboChartType.Name = "cboChartType";
            this.cboChartType.Size = new System.Drawing.Size(408, 21);
            this.cboChartType.TabIndex = 133;
            this.cboChartType.SelectedIndexChanged += new System.EventHandler(this.cboChartType_SelectedIndexChanged);
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.BackImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.chart.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.chart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart.BorderSkin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.chart.BorderSkin.BorderWidth = 0;
            this.chart.BorderSkin.PageColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.chart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.FrameTitle1;
            chartArea1.Area3DStyle.Inclination = 15;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.Perspective = 10;
            chartArea1.Area3DStyle.Rotation = 10;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)(((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisX.ScrollBar.Size = 10D;
            chartArea1.AxisX.Title = "Primary X";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.Title = "Secondary X";
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisY.ScrollBar.Size = 10D;
            chartArea1.AxisY.Title = "Primary Y";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.Title = "Secondary Y";
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "Default";
            chartArea1.ShadowColor = System.Drawing.Color.White;
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F);
            legend1.IsTextAutoFit = false;
            legend1.MaximumAutoSize = 5F;
            legend1.Name = "Default";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 8);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(396, 320);
            this.chart.TabIndex = 127;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title";
            title1.ShadowColor = System.Drawing.Color.Transparent;
            title1.ShadowOffset = 1;
            title1.Text = "Title";
            this.chart.Titles.Add(title1);
            // 
            // MetricValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.sessionsSplitContainer);
            this.Controls.Add(this.lblClose);
            this.Name = "MetricValueControl";
            this.Size = new System.Drawing.Size(968, 400);
            this.Load += new System.EventHandler(this.MetricControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.sessionsSplitContainer.Panel1.ResumeLayout(false);
            this.sessionsSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionsSplitContainer)).EndInit();
            this.sessionsSplitContainer.ResumeLayout(false);
            this.grouperMetric.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metricDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.SplitContainer sessionsSplitContainer;
        internal Grouper grouperMetric;
        private System.Windows.Forms.DataGridView metricDataGridView;
        internal System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.ComboBox cboChartType;
    }
}
