namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    partial class MetricMonitorControl
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
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.grouperMetricRules = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.dataPointDataGridView = new System.Windows.Forms.DataGridView();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageMonitor = new System.Windows.Forms.TabPage();
            this.metricMonitorRuleSplitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grouperMonitorRules = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.monitorRuleDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperEvents = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.eventListView = new System.Windows.Forms.ListView();
            this.stateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.entityColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timestampColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboChartType = new System.Windows.Forms.ComboBox();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnMetrics = new System.Windows.Forms.Button();
            this.btnClearRules = new System.Windows.Forms.Button();
            this.btnCloseTabs = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.monitorRuleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblRefresh = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnSet = new System.Windows.Forms.Button();
            this.btnClearData = new System.Windows.Forms.Button();
            this.txtMonitorRefreshTimeout = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.tabPageDescription.SuspendLayout();
            this.grouperMetricRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataPointDataGridView)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.tabPageMonitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metricMonitorRuleSplitContainer)).BeginInit();
            this.metricMonitorRuleSplitContainer.Panel1.SuspendLayout();
            this.metricMonitorRuleSplitContainer.Panel2.SuspendLayout();
            this.metricMonitorRuleSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grouperMonitorRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRuleDataGridView)).BeginInit();
            this.grouperEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRuleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageDescription.Controls.Add(this.grouperMetricRules);
            this.tabPageDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageDescription.Location = new System.Drawing.Point(4, 24);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Size = new System.Drawing.Size(968, 452);
            this.tabPageDescription.TabIndex = 2;
            this.tabPageDescription.Text = "Query";
            // 
            // grouperMetricRules
            // 
            this.grouperMetricRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperMetricRules.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMetricRules.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMetricRules.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMetricRules.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMetricRules.BorderThickness = 1F;
            this.grouperMetricRules.Controls.Add(this.dataPointDataGridView);
            this.grouperMetricRules.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMetricRules.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMetricRules.ForeColor = System.Drawing.Color.White;
            this.grouperMetricRules.GroupImage = null;
            this.grouperMetricRules.GroupTitle = "Metrics Rules";
            this.grouperMetricRules.Location = new System.Drawing.Point(16, 8);
            this.grouperMetricRules.Name = "grouperMetricRules";
            this.grouperMetricRules.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMetricRules.PaintGroupBox = true;
            this.grouperMetricRules.RoundCorners = 4;
            this.grouperMetricRules.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMetricRules.ShadowControl = false;
            this.grouperMetricRules.ShadowThickness = 1;
            this.grouperMetricRules.Size = new System.Drawing.Size(936, 432);
            this.grouperMetricRules.TabIndex = 1;
            this.grouperMetricRules.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperDatapoints_CustomPaint);
            // 
            // dataPointDataGridView
            // 
            this.dataPointDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPointDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.dataPointDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataPointDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataPointDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataPointDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.dataPointDataGridView.Location = new System.Drawing.Point(16, 32);
            this.dataPointDataGridView.Name = "dataPointDataGridView";
            this.dataPointDataGridView.RowHeadersWidth = 24;
            this.dataPointDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataPointDataGridView.Size = new System.Drawing.Size(904, 384);
            this.dataPointDataGridView.TabIndex = 27;
            this.dataPointDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataPointDataGridView_CellClick);
            this.dataPointDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataPointDataGridView_DataError);
            this.dataPointDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataPointDataGridView_RowsAdded);
            this.dataPointDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataPointDataGridView_RowsRemoved);
            this.dataPointDataGridView.Resize += new System.EventHandler(this.dataPointDataGridView_Resize);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.tabPageDescription);
            this.mainTabControl.Controls.Add(this.tabPageMonitor);
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(16, 16);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(976, 480);
            this.mainTabControl.TabIndex = 19;
            this.mainTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mainTabControl_DrawItem);
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            this.mainTabControl.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.mainTabControl_ControlAdded);
            this.mainTabControl.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.mainTabControl_ControlRemoved);
            // 
            // tabPageMonitor
            // 
            this.tabPageMonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageMonitor.Controls.Add(this.metricMonitorRuleSplitContainer);
            this.tabPageMonitor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageMonitor.Location = new System.Drawing.Point(4, 24);
            this.tabPageMonitor.Name = "tabPageMonitor";
            this.tabPageMonitor.Size = new System.Drawing.Size(968, 452);
            this.tabPageMonitor.TabIndex = 3;
            this.tabPageMonitor.Text = "Monitor";
            // 
            // metricMonitorRuleSplitContainer
            // 
            this.metricMonitorRuleSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metricMonitorRuleSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.metricMonitorRuleSplitContainer.Name = "metricMonitorRuleSplitContainer";
            // 
            // metricMonitorRuleSplitContainer.Panel1
            // 
            this.metricMonitorRuleSplitContainer.Panel1.Controls.Add(this.splitContainer1);
            // 
            // metricMonitorRuleSplitContainer.Panel2
            // 
            this.metricMonitorRuleSplitContainer.Panel2.Controls.Add(this.cboChartType);
            this.metricMonitorRuleSplitContainer.Panel2.Controls.Add(this.chart);
            this.metricMonitorRuleSplitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.metricMonitorRuleSplitContainer_Panel2_Paint);
            this.metricMonitorRuleSplitContainer.Size = new System.Drawing.Size(936, 432);
            this.metricMonitorRuleSplitContainer.SplitterDistance = 460;
            this.metricMonitorRuleSplitContainer.SplitterWidth = 16;
            this.metricMonitorRuleSplitContainer.TabIndex = 34;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grouperMonitorRules);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grouperEvents);
            this.splitContainer1.Size = new System.Drawing.Size(460, 432);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 0;
            // 
            // grouperMonitorRules
            // 
            this.grouperMonitorRules.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMonitorRules.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMonitorRules.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMonitorRules.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMonitorRules.BorderThickness = 1F;
            this.grouperMonitorRules.Controls.Add(this.monitorRuleDataGridView);
            this.grouperMonitorRules.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMonitorRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMonitorRules.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMonitorRules.ForeColor = System.Drawing.Color.White;
            this.grouperMonitorRules.GroupImage = null;
            this.grouperMonitorRules.GroupTitle = "Monitor Rules";
            this.grouperMonitorRules.Location = new System.Drawing.Point(0, 0);
            this.grouperMonitorRules.Name = "grouperMonitorRules";
            this.grouperMonitorRules.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMonitorRules.PaintGroupBox = true;
            this.grouperMonitorRules.RoundCorners = 4;
            this.grouperMonitorRules.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMonitorRules.ShadowControl = false;
            this.grouperMonitorRules.ShadowThickness = 1;
            this.grouperMonitorRules.Size = new System.Drawing.Size(460, 212);
            this.grouperMonitorRules.TabIndex = 5;
            this.grouperMonitorRules.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMonitorRules_CustomPaint);
            // 
            // monitorRuleDataGridView
            // 
            this.monitorRuleDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monitorRuleDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.monitorRuleDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.monitorRuleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.monitorRuleDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.monitorRuleDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.monitorRuleDataGridView.Location = new System.Drawing.Point(17, 33);
            this.monitorRuleDataGridView.Name = "monitorRuleDataGridView";
            this.monitorRuleDataGridView.RowHeadersWidth = 24;
            this.monitorRuleDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.monitorRuleDataGridView.Size = new System.Drawing.Size(426, 164);
            this.monitorRuleDataGridView.TabIndex = 28;
            this.monitorRuleDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.monitorRuleDataGridView_CellClick);
            this.monitorRuleDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.monitorRuleDataGridView_CellFormatting);
            this.monitorRuleDataGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.monitorRuleDataGridView_CellValidated);
            this.monitorRuleDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.monitorRuleDataGridView_DataError);
            this.monitorRuleDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.monitorRuleDataGridView_EditingControlShowing);
            this.monitorRuleDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.monitorRuleDataGridView_RowsAdded);
            this.monitorRuleDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.monitorRuleDataGridView_RowsRemoved);
            this.monitorRuleDataGridView.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.monitorRuleDataGridView_RowValidating);
            this.monitorRuleDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.monitorRuleDataGridView_UserDeletingRow);
            this.monitorRuleDataGridView.Resize += new System.EventHandler(this.monitorRuleDataGridView_Resize);
            // 
            // grouperEvents
            // 
            this.grouperEvents.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperEvents.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperEvents.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperEvents.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEvents.BorderThickness = 1F;
            this.grouperEvents.Controls.Add(this.eventListView);
            this.grouperEvents.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperEvents.ForeColor = System.Drawing.Color.White;
            this.grouperEvents.GroupImage = null;
            this.grouperEvents.GroupTitle = "Monitor Events";
            this.grouperEvents.Location = new System.Drawing.Point(0, 0);
            this.grouperEvents.Name = "grouperEvents";
            this.grouperEvents.Padding = new System.Windows.Forms.Padding(20);
            this.grouperEvents.PaintGroupBox = true;
            this.grouperEvents.RoundCorners = 4;
            this.grouperEvents.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEvents.ShadowControl = false;
            this.grouperEvents.ShadowThickness = 1;
            this.grouperEvents.Size = new System.Drawing.Size(460, 212);
            this.grouperEvents.TabIndex = 21;
            this.grouperEvents.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperEvents_CustomPaint);
            // 
            // eventListView
            // 
            this.eventListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.stateColumnHeader,
            this.entityColumnHeader,
            this.valueColumnHeader,
            this.timestampColumnHeader});
            this.eventListView.FullRowSelect = true;
            this.eventListView.Location = new System.Drawing.Point(16, 32);
            this.eventListView.Name = "eventListView";
            this.eventListView.OwnerDraw = true;
            this.eventListView.Size = new System.Drawing.Size(428, 168);
            this.eventListView.TabIndex = 1;
            this.eventListView.UseCompatibleStateImageBehavior = false;
            this.eventListView.View = System.Windows.Forms.View.Details;
            this.eventListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.eventListView_DrawColumnHeader);
            this.eventListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.eventListView_DrawItem);
            this.eventListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.eventListView_DrawSubItem);
            this.eventListView.Resize += new System.EventHandler(this.eventListView_Resize);
            // 
            // stateColumnHeader
            // 
            this.stateColumnHeader.Text = "State";
            this.stateColumnHeader.Width = 50;
            // 
            // entityColumnHeader
            // 
            this.entityColumnHeader.Text = "Entity";
            this.entityColumnHeader.Width = 75;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Value";
            this.valueColumnHeader.Width = 140;
            // 
            // timestampColumnHeader
            // 
            this.timestampColumnHeader.Text = "Timestamp";
            this.timestampColumnHeader.Width = 78;
            // 
            // cboChartType
            // 
            this.cboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChartType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboChartType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChartType.FormattingEnabled = true;
            this.cboChartType.Items.AddRange(new object[] {
            "PeekLock",
            "ReceiveDelete"});
            this.cboChartType.Location = new System.Drawing.Point(20, 392);
            this.cboChartType.Name = "cboChartType";
            this.cboChartType.Size = new System.Drawing.Size(416, 21);
            this.cboChartType.TabIndex = 133;
            this.cboChartType.SelectedIndexChanged += new System.EventHandler(this.cboChartType_SelectedIndexChanged);
            // 
            // chart
            // 
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
            chartArea1.AxisX.Title = "Time";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.Title = "Secondary X";
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisY.ScrollBar.Size = 10D;
            chartArea1.AxisY.Title = "Messages\\KB";
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
            this.chart.Location = new System.Drawing.Point(16, 8);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(424, 376);
            this.chart.TabIndex = 127;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Monitored Values";
            title1.ShadowColor = System.Drawing.Color.Transparent;
            title1.ShadowOffset = 1;
            title1.Text = "Monitored Values";
            this.chart.Titles.Add(title1);
            // 
            // btnMetrics
            // 
            this.btnMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMetrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnMetrics.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMetrics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMetrics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnMetrics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMetrics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMetrics.Location = new System.Drawing.Point(920, 504);
            this.btnMetrics.Name = "btnMetrics";
            this.btnMetrics.Size = new System.Drawing.Size(72, 24);
            this.btnMetrics.TabIndex = 7;
            this.btnMetrics.Text = "Get Metrics";
            this.btnMetrics.UseVisualStyleBackColor = false;
            this.btnMetrics.Click += new System.EventHandler(this.btnGet_Click);
            this.btnMetrics.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnMetrics.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnClearRules
            // 
            this.btnClearRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearRules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClearRules.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearRules.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearRules.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearRules.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearRules.Location = new System.Drawing.Point(840, 504);
            this.btnClearRules.Name = "btnClearRules";
            this.btnClearRules.Size = new System.Drawing.Size(72, 24);
            this.btnClearRules.TabIndex = 6;
            this.btnClearRules.Text = "Clear Rules";
            this.btnClearRules.UseVisualStyleBackColor = false;
            this.btnClearRules.Click += new System.EventHandler(this.btnClear_Click);
            this.btnClearRules.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnClearRules.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnCloseTabs
            // 
            this.btnCloseTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCloseTabs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCloseTabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCloseTabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCloseTabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseTabs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCloseTabs.Location = new System.Drawing.Point(680, 504);
            this.btnCloseTabs.Name = "btnCloseTabs";
            this.btnCloseTabs.Size = new System.Drawing.Size(72, 24);
            this.btnCloseTabs.TabIndex = 4;
            this.btnCloseTabs.Text = "Close Tabs";
            this.btnCloseTabs.UseVisualStyleBackColor = false;
            this.btnCloseTabs.Click += new System.EventHandler(this.btnCloseTabs_Click);
            this.btnCloseTabs.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCloseTabs.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExport.Location = new System.Drawing.Point(600, 504);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(72, 24);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnImport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnImport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImport.Location = new System.Drawing.Point(520, 504);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(72, 24);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // lblRefresh
            // 
            this.lblRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRefresh.AutoSize = true;
            this.lblRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRefresh.Location = new System.Drawing.Point(16, 508);
            this.lblRefresh.Name = "lblRefresh";
            this.lblRefresh.Size = new System.Drawing.Size(172, 13);
            this.lblRefresh.TabIndex = 21;
            this.lblRefresh.Text = "Monitor Refresh Interval (seconds):";
            // 
            // timer
            // 
            this.timer.Interval = 30000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSet.Location = new System.Drawing.Point(232, 504);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(72, 24);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = false;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnClearData
            // 
            this.btnClearData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClearData.Enabled = false;
            this.btnClearData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearData.Location = new System.Drawing.Point(760, 504);
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.Size = new System.Drawing.Size(72, 24);
            this.btnClearData.TabIndex = 5;
            this.btnClearData.Text = "Clear Data";
            this.btnClearData.UseVisualStyleBackColor = false;
            this.btnClearData.Click += new System.EventHandler(this.btnClearEvents_Click);
            // 
            // txtMonitorRefreshTimeout
            // 
            this.txtMonitorRefreshTimeout.AllowSpace = false;
            this.txtMonitorRefreshTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMonitorRefreshTimeout.Location = new System.Drawing.Point(192, 506);
            this.txtMonitorRefreshTimeout.Name = "txtMonitorRefreshTimeout";
            this.txtMonitorRefreshTimeout.Size = new System.Drawing.Size(32, 20);
            this.txtMonitorRefreshTimeout.TabIndex = 0;
            this.txtMonitorRefreshTimeout.Text = "30";
            // 
            // MetricMonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.btnClearData);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.txtMonitorRefreshTimeout);
            this.Controls.Add(this.lblRefresh);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCloseTabs);
            this.Controls.Add(this.btnClearRules);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnMetrics);
            this.Name = "MetricMonitorControl";
            this.Size = new System.Drawing.Size(1008, 544);
            this.tabPageDescription.ResumeLayout(false);
            this.grouperMetricRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataPointDataGridView)).EndInit();
            this.mainTabControl.ResumeLayout(false);
            this.tabPageMonitor.ResumeLayout(false);
            this.metricMonitorRuleSplitContainer.Panel1.ResumeLayout(false);
            this.metricMonitorRuleSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metricMonitorRuleSplitContainer)).EndInit();
            this.metricMonitorRuleSplitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grouperMonitorRules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.monitorRuleDataGridView)).EndInit();
            this.grouperEvents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorRuleBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageDescription;
        internal System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.Button btnMetrics;
        private Grouper grouperMetricRules;
        private System.Windows.Forms.DataGridView dataPointDataGridView;
        private System.Windows.Forms.Button btnClearRules;
        private System.Windows.Forms.Button btnCloseTabs;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabPage tabPageMonitor;
        private System.Windows.Forms.BindingSource monitorRuleBindingSource;
        private System.Windows.Forms.SplitContainer metricMonitorRuleSplitContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        internal Grouper grouperMonitorRules;
        internal Grouper grouperEvents;
        private System.Windows.Forms.ComboBox cboChartType;
        internal System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.DataGridView monitorRuleDataGridView;
        private System.Windows.Forms.Label lblRefresh;
        private System.Windows.Forms.Timer timer;
        private NumericTextBox txtMonitorRefreshTimeout;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnClearData;
        private System.Windows.Forms.ListView eventListView;
        private System.Windows.Forms.ColumnHeader stateColumnHeader;
        private System.Windows.Forms.ColumnHeader entityColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private System.Windows.Forms.ColumnHeader timestampColumnHeader;
    }
}
