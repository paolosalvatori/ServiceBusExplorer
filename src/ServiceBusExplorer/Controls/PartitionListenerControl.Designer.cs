namespace ServiceBusExplorer.Controls
{
    partial class PartitionListenerControl
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartitionListenerControl));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageListener = new System.Windows.Forms.TabPage();
            this.grouperStatistics = new ServiceBusExplorer.Controls.Grouper();
            this.cboMessageSizePerSecond = new System.Windows.Forms.ComboBox();
            this.cboAverageDuration = new System.Windows.Forms.ComboBox();
            this.cboEventDataPerSecond = new System.Windows.Forms.ComboBox();
            this.lblMessageSizePerSecond = new System.Windows.Forms.Label();
            this.txtMessageSizePerSecond = new ServiceBusExplorer.Controls.NumericTextBox();
            this.lblAverageTime = new System.Windows.Forms.Label();
            this.txtAverageDuration = new ServiceBusExplorer.Controls.NumericTextBox();
            this.txtEventDataTotal = new ServiceBusExplorer.Controls.NumericTextBox();
            this.lblEventDataTotal = new System.Windows.Forms.Label();
            this.txtEventDataPerSecond = new ServiceBusExplorer.Controls.NumericTextBox();
            this.lblEventDataPerSecond = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grouperPartitionInformation = new ServiceBusExplorer.Controls.Grouper();
            this.lblPartitionInformation = new System.Windows.Forms.Label();
            this.lblPartition = new System.Windows.Forms.Label();
            this.cboPartition = new System.Windows.Forms.ComboBox();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grouperOptions = new ServiceBusExplorer.Controls.Grouper();
            this.lblStartingDateTimeUtc = new System.Windows.Forms.Label();
            this.pickerStartingDateTimeUtc = new System.Windows.Forms.DateTimePicker();
            this.checkBoxOffsetInclusive = new System.Windows.Forms.CheckBox();
            this.txtMaxBatchSize = new ServiceBusExplorer.Controls.NumericTextBox();
            this.lblMaxBatchSize = new System.Windows.Forms.Label();
            this.checkBoxCheckpoint = new System.Windows.Forms.CheckBox();
            this.txtReceiveTimeout = new ServiceBusExplorer.Controls.NumericTextBox();
            this.lblReceiveTimeout = new System.Windows.Forms.Label();
            this.checkBoxGraph = new System.Windows.Forms.CheckBox();
            this.checkBoxLogging = new System.Windows.Forms.CheckBox();
            this.checkBoxTrackMessages = new System.Windows.Forms.CheckBox();
            this.checkBoxVerbose = new System.Windows.Forms.CheckBox();
            this.txtRefreshInformation = new ServiceBusExplorer.Controls.NumericTextBox();
            this.lblRefreshInformation = new System.Windows.Forms.Label();
            this.tabPageEventData = new System.Windows.Forms.TabPage();
            this.eventDataSplitContainer = new System.Windows.Forms.SplitContainer();
            this.eventDataMainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageList = new ServiceBusExplorer.Controls.Grouper();
            this.eventDataDataGridView = new System.Windows.Forms.DataGridView();
            this.grouperMessageText = new ServiceBusExplorer.Controls.Grouper();
            this.txtMessageText = new FastColoredTextBoxNS.FastColoredTextBox();
            this.eventDataPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageProperties = new ServiceBusExplorer.Controls.Grouper();
            this.eventDataPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.grouperEventDataCustomProperties = new ServiceBusExplorer.Controls.Grouper();
            this.eventDataPropertyListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventDataContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewAndSaveEventDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.cboReceiverInspector = new System.Windows.Forms.ComboBox();
            this.lblReceiverInspector = new System.Windows.Forms.Label();
            this.partitionInformationContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPartitionInformationToClipboardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainTabControl.SuspendLayout();
            this.tabPageListener.SuspendLayout();
            this.grouperStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.grouperPartitionInformation.SuspendLayout();
            this.grouperOptions.SuspendLayout();
            this.tabPageEventData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventDataSplitContainer)).BeginInit();
            this.eventDataSplitContainer.Panel1.SuspendLayout();
            this.eventDataSplitContainer.Panel2.SuspendLayout();
            this.eventDataSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventDataMainSplitContainer)).BeginInit();
            this.eventDataMainSplitContainer.Panel1.SuspendLayout();
            this.eventDataMainSplitContainer.Panel2.SuspendLayout();
            this.eventDataMainSplitContainer.SuspendLayout();
            this.grouperMessageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventDataDataGridView)).BeginInit();
            this.grouperMessageText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessageText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventDataPropertiesSplitContainer)).BeginInit();
            this.eventDataPropertiesSplitContainer.Panel1.SuspendLayout();
            this.eventDataPropertiesSplitContainer.Panel2.SuspendLayout();
            this.eventDataPropertiesSplitContainer.SuspendLayout();
            this.grouperMessageProperties.SuspendLayout();
            this.grouperEventDataCustomProperties.SuspendLayout();
            this.eventDataContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventDataBindingSource)).BeginInit();
            this.partitionInformationContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.Location = new System.Drawing.Point(920, 504);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 24);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStart.Location = new System.Drawing.Point(840, 504);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(72, 24);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnStart.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.tabPageListener);
            this.mainTabControl.Controls.Add(this.tabPageEventData);
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.ItemSize = new System.Drawing.Size(76, 18);
            this.mainTabControl.Location = new System.Drawing.Point(16, 16);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(976, 480);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mainTabControl_DrawItem);
            // 
            // tabPageListener
            // 
            this.tabPageListener.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageListener.Controls.Add(this.grouperStatistics);
            this.tabPageListener.Controls.Add(this.chart);
            this.tabPageListener.Controls.Add(this.grouperPartitionInformation);
            this.tabPageListener.Controls.Add(this.grouperOptions);
            this.tabPageListener.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageListener.Location = new System.Drawing.Point(4, 22);
            this.tabPageListener.Name = "tabPageListener";
            this.tabPageListener.Size = new System.Drawing.Size(968, 454);
            this.tabPageListener.TabIndex = 2;
            this.tabPageListener.Text = "Listener";
            // 
            // grouperStatistics
            // 
            this.grouperStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperStatistics.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperStatistics.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperStatistics.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperStatistics.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperStatistics.BorderThickness = 1F;
            this.grouperStatistics.Controls.Add(this.cboMessageSizePerSecond);
            this.grouperStatistics.Controls.Add(this.cboAverageDuration);
            this.grouperStatistics.Controls.Add(this.cboEventDataPerSecond);
            this.grouperStatistics.Controls.Add(this.lblMessageSizePerSecond);
            this.grouperStatistics.Controls.Add(this.txtMessageSizePerSecond);
            this.grouperStatistics.Controls.Add(this.lblAverageTime);
            this.grouperStatistics.Controls.Add(this.txtAverageDuration);
            this.grouperStatistics.Controls.Add(this.txtEventDataTotal);
            this.grouperStatistics.Controls.Add(this.lblEventDataTotal);
            this.grouperStatistics.Controls.Add(this.txtEventDataPerSecond);
            this.grouperStatistics.Controls.Add(this.lblEventDataPerSecond);
            this.grouperStatistics.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperStatistics.ForeColor = System.Drawing.Color.White;
            this.grouperStatistics.GroupImage = null;
            this.grouperStatistics.GroupTitle = "Statistics";
            this.grouperStatistics.Location = new System.Drawing.Point(640, 8);
            this.grouperStatistics.Name = "grouperStatistics";
            this.grouperStatistics.Padding = new System.Windows.Forms.Padding(20);
            this.grouperStatistics.PaintGroupBox = true;
            this.grouperStatistics.RoundCorners = 4;
            this.grouperStatistics.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperStatistics.ShadowControl = false;
            this.grouperStatistics.ShadowThickness = 1;
            this.grouperStatistics.Size = new System.Drawing.Size(312, 120);
            this.grouperStatistics.TabIndex = 2;
            this.grouperStatistics.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperStatistics_CustomPaint);
            // 
            // cboMessageSizePerSecond
            // 
            this.cboMessageSizePerSecond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMessageSizePerSecond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMessageSizePerSecond.FormattingEnabled = true;
            this.cboMessageSizePerSecond.Location = new System.Drawing.Point(248, 89);
            this.cboMessageSizePerSecond.Name = "cboMessageSizePerSecond";
            this.cboMessageSizePerSecond.Size = new System.Drawing.Size(48, 21);
            this.cboMessageSizePerSecond.TabIndex = 10;
            this.cboMessageSizePerSecond.SelectedIndexChanged += new System.EventHandler(this.cboMessageSizePerSecond_SelectedIndexChanged);
            // 
            // cboAverageDuration
            // 
            this.cboAverageDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAverageDuration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAverageDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAverageDuration.FormattingEnabled = true;
            this.cboAverageDuration.Location = new System.Drawing.Point(96, 89);
            this.cboAverageDuration.Name = "cboAverageDuration";
            this.cboAverageDuration.Size = new System.Drawing.Size(48, 21);
            this.cboAverageDuration.TabIndex = 7;
            this.cboAverageDuration.SelectedIndexChanged += new System.EventHandler(this.cboAverageTime_SelectedIndexChanged);
            // 
            // cboEventDataPerSecond
            // 
            this.cboEventDataPerSecond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEventDataPerSecond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEventDataPerSecond.FormattingEnabled = true;
            this.cboEventDataPerSecond.Location = new System.Drawing.Point(248, 49);
            this.cboEventDataPerSecond.Name = "cboEventDataPerSecond";
            this.cboEventDataPerSecond.Size = new System.Drawing.Size(48, 21);
            this.cboEventDataPerSecond.TabIndex = 4;
            this.cboEventDataPerSecond.SelectedIndexChanged += new System.EventHandler(this.cboEventDataPerSecond_SelectedIndexChanged);
            // 
            // lblMessageSizePerSecond
            // 
            this.lblMessageSizePerSecond.AutoSize = true;
            this.lblMessageSizePerSecond.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessageSizePerSecond.Location = new System.Drawing.Point(160, 72);
            this.lblMessageSizePerSecond.Name = "lblMessageSizePerSecond";
            this.lblMessageSizePerSecond.Size = new System.Drawing.Size(45, 13);
            this.lblMessageSizePerSecond.TabIndex = 8;
            this.lblMessageSizePerSecond.Text = "KB/Sec";
            // 
            // txtMessageSizePerSecond
            // 
            this.txtMessageSizePerSecond.AllowDecimal = false;
            this.txtMessageSizePerSecond.AllowNegative = false;
            this.txtMessageSizePerSecond.AllowSpace = false;
            this.txtMessageSizePerSecond.Location = new System.Drawing.Point(160, 88);
            this.txtMessageSizePerSecond.Name = "txtMessageSizePerSecond";
            this.txtMessageSizePerSecond.Size = new System.Drawing.Size(88, 20);
            this.txtMessageSizePerSecond.TabIndex = 9;
            // 
            // lblAverageTime
            // 
            this.lblAverageTime.AutoSize = true;
            this.lblAverageTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAverageTime.Location = new System.Drawing.Point(16, 72);
            this.lblAverageTime.Name = "lblAverageTime";
            this.lblAverageTime.Size = new System.Drawing.Size(121, 13);
            this.lblAverageTime.TabIndex = 5;
            this.lblAverageTime.Text = "Average Duration (Sec):";
            // 
            // txtAverageDuration
            // 
            this.txtAverageDuration.AllowDecimal = false;
            this.txtAverageDuration.AllowNegative = false;
            this.txtAverageDuration.AllowSpace = false;
            this.txtAverageDuration.Location = new System.Drawing.Point(16, 88);
            this.txtAverageDuration.Name = "txtAverageDuration";
            this.txtAverageDuration.Size = new System.Drawing.Size(80, 20);
            this.txtAverageDuration.TabIndex = 6;
            // 
            // txtEventDataTotal
            // 
            this.txtEventDataTotal.AllowDecimal = false;
            this.txtEventDataTotal.AllowNegative = false;
            this.txtEventDataTotal.AllowSpace = false;
            this.txtEventDataTotal.Location = new System.Drawing.Point(16, 48);
            this.txtEventDataTotal.Name = "txtEventDataTotal";
            this.txtEventDataTotal.Size = new System.Drawing.Size(128, 20);
            this.txtEventDataTotal.TabIndex = 1;
            // 
            // lblEventDataTotal
            // 
            this.lblEventDataTotal.AutoSize = true;
            this.lblEventDataTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEventDataTotal.Location = new System.Drawing.Point(16, 32);
            this.lblEventDataTotal.Name = "lblEventDataTotal";
            this.lblEventDataTotal.Size = new System.Drawing.Size(70, 13);
            this.lblEventDataTotal.TabIndex = 0;
            this.lblEventDataTotal.Text = "Events Total:";
            // 
            // txtEventDataPerSecond
            // 
            this.txtEventDataPerSecond.AllowDecimal = false;
            this.txtEventDataPerSecond.AllowNegative = false;
            this.txtEventDataPerSecond.AllowSpace = false;
            this.txtEventDataPerSecond.Location = new System.Drawing.Point(160, 48);
            this.txtEventDataPerSecond.Name = "txtEventDataPerSecond";
            this.txtEventDataPerSecond.Size = new System.Drawing.Size(88, 20);
            this.txtEventDataPerSecond.TabIndex = 3;
            // 
            // lblEventDataPerSecond
            // 
            this.lblEventDataPerSecond.AutoSize = true;
            this.lblEventDataPerSecond.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEventDataPerSecond.Location = new System.Drawing.Point(160, 32);
            this.lblEventDataPerSecond.Name = "lblEventDataPerSecond";
            this.lblEventDataPerSecond.Size = new System.Drawing.Size(67, 13);
            this.lblEventDataPerSecond.TabIndex = 2;
            this.lblEventDataPerSecond.Text = "Events/Sec:";
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
            chartArea1.AxisX.Title = "Events";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisY.ScrollBar.Size = 10D;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.Title = "Messages/Sec";
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
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F);
            legend1.IsTextAutoFit = false;
            legend1.MaximumAutoSize = 18F;
            legend1.Name = "Default";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(16, 16);
            this.chart.Name = "chart";
            series1.BorderColor = System.Drawing.Color.Red;
            series1.BorderWidth = 2;
            series1.ChartArea = "Default";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Default";
            series1.LegendText = "Latency";
            series1.Name = "ReceiverLatency";
            series2.BorderWidth = 2;
            series2.ChartArea = "Default";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Default";
            series2.LegendText = "Throughput";
            series2.Name = "ReceiverThroughput";
            series3.ChartArea = "Default";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Legend = "Default";
            series3.LegendText = "KB/Sec";
            series3.Name = "MessageSizePerSecond";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(616, 312);
            this.chart.TabIndex = 0;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title";
            title1.ShadowColor = System.Drawing.Color.Transparent;
            title1.ShadowOffset = 1;
            title1.Text = "Listener Performance Counters";
            this.chart.Titles.Add(title1);
            // 
            // grouperPartitionInformation
            // 
            this.grouperPartitionInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperPartitionInformation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperPartitionInformation.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperPartitionInformation.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperPartitionInformation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPartitionInformation.BorderThickness = 1F;
            this.grouperPartitionInformation.Controls.Add(this.lblPartitionInformation);
            this.grouperPartitionInformation.Controls.Add(this.lblPartition);
            this.grouperPartitionInformation.Controls.Add(this.cboPartition);
            this.grouperPartitionInformation.Controls.Add(this.propertyListView);
            this.grouperPartitionInformation.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPartitionInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperPartitionInformation.ForeColor = System.Drawing.Color.White;
            this.grouperPartitionInformation.GroupImage = null;
            this.grouperPartitionInformation.GroupTitle = "Partition Information";
            this.grouperPartitionInformation.Location = new System.Drawing.Point(640, 136);
            this.grouperPartitionInformation.Name = "grouperPartitionInformation";
            this.grouperPartitionInformation.Padding = new System.Windows.Forms.Padding(20);
            this.grouperPartitionInformation.PaintGroupBox = true;
            this.grouperPartitionInformation.RoundCorners = 4;
            this.grouperPartitionInformation.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperPartitionInformation.ShadowControl = false;
            this.grouperPartitionInformation.ShadowThickness = 1;
            this.grouperPartitionInformation.Size = new System.Drawing.Size(312, 304);
            this.grouperPartitionInformation.TabIndex = 3;
            this.grouperPartitionInformation.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperPartitionInformation_CustomPaint);
            // 
            // lblPartitionInformation
            // 
            this.lblPartitionInformation.AutoSize = true;
            this.lblPartitionInformation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPartitionInformation.Location = new System.Drawing.Point(16, 80);
            this.lblPartitionInformation.Name = "lblPartitionInformation";
            this.lblPartitionInformation.Size = new System.Drawing.Size(62, 13);
            this.lblPartitionInformation.TabIndex = 2;
            this.lblPartitionInformation.Text = "Information:";
            // 
            // lblPartition
            // 
            this.lblPartition.AutoSize = true;
            this.lblPartition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPartition.Location = new System.Drawing.Point(16, 32);
            this.lblPartition.Name = "lblPartition";
            this.lblPartition.Size = new System.Drawing.Size(48, 13);
            this.lblPartition.TabIndex = 0;
            this.lblPartition.Text = "Partition:";
            // 
            // cboPartition
            // 
            this.cboPartition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPartition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPartition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPartition.FormattingEnabled = true;
            this.cboPartition.Location = new System.Drawing.Point(16, 48);
            this.cboPartition.Name = "cboPartition";
            this.cboPartition.Size = new System.Drawing.Size(280, 21);
            this.cboPartition.TabIndex = 1;
            this.cboPartition.SelectedIndexChanged += new System.EventHandler(this.cboPartition_SelectedIndexChanged);
            // 
            // propertyListView
            // 
            this.propertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.valueColumnHeader});
            this.propertyListView.HideSelection = false;
            this.propertyListView.Location = new System.Drawing.Point(16, 96);
            this.propertyListView.Name = "propertyListView";
            this.propertyListView.OwnerDraw = true;
            this.propertyListView.Size = new System.Drawing.Size(280, 192);
            this.propertyListView.TabIndex = 3;
            this.propertyListView.UseCompatibleStateImageBehavior = false;
            this.propertyListView.View = System.Windows.Forms.View.Details;
            this.propertyListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.propertyListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.propertyListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.propertyListView.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 160;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Value";
            this.valueColumnHeader.Width = 115;
            // 
            // grouperOptions
            // 
            this.grouperOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperOptions.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperOptions.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperOptions.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperOptions.BorderThickness = 1F;
            this.grouperOptions.Controls.Add(this.lblStartingDateTimeUtc);
            this.grouperOptions.Controls.Add(this.pickerStartingDateTimeUtc);
            this.grouperOptions.Controls.Add(this.checkBoxOffsetInclusive);
            this.grouperOptions.Controls.Add(this.txtMaxBatchSize);
            this.grouperOptions.Controls.Add(this.lblMaxBatchSize);
            this.grouperOptions.Controls.Add(this.checkBoxCheckpoint);
            this.grouperOptions.Controls.Add(this.txtReceiveTimeout);
            this.grouperOptions.Controls.Add(this.lblReceiveTimeout);
            this.grouperOptions.Controls.Add(this.checkBoxGraph);
            this.grouperOptions.Controls.Add(this.checkBoxLogging);
            this.grouperOptions.Controls.Add(this.checkBoxTrackMessages);
            this.grouperOptions.Controls.Add(this.checkBoxVerbose);
            this.grouperOptions.Controls.Add(this.txtRefreshInformation);
            this.grouperOptions.Controls.Add(this.lblRefreshInformation);
            this.grouperOptions.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperOptions.ForeColor = System.Drawing.Color.White;
            this.grouperOptions.GroupImage = null;
            this.grouperOptions.GroupTitle = "Options";
            this.grouperOptions.Location = new System.Drawing.Point(16, 328);
            this.grouperOptions.Name = "grouperOptions";
            this.grouperOptions.Padding = new System.Windows.Forms.Padding(20);
            this.grouperOptions.PaintGroupBox = true;
            this.grouperOptions.RoundCorners = 4;
            this.grouperOptions.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperOptions.ShadowControl = false;
            this.grouperOptions.ShadowThickness = 1;
            this.grouperOptions.Size = new System.Drawing.Size(608, 112);
            this.grouperOptions.TabIndex = 1;
            this.grouperOptions.Resize += new System.EventHandler(this.grouperOptions_Resize);
            // 
            // lblStartingDateTimeUtc
            // 
            this.lblStartingDateTimeUtc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStartingDateTimeUtc.AutoSize = true;
            this.lblStartingDateTimeUtc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStartingDateTimeUtc.Location = new System.Drawing.Point(424, 32);
            this.lblStartingDateTimeUtc.Name = "lblStartingDateTimeUtc";
            this.lblStartingDateTimeUtc.Size = new System.Drawing.Size(123, 13);
            this.lblStartingDateTimeUtc.TabIndex = 6;
            this.lblStartingDateTimeUtc.Text = "Starting Date Time UTC:";
            // 
            // pickerStartingDateTimeUtc
            // 
            this.pickerStartingDateTimeUtc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pickerStartingDateTimeUtc.Checked = false;
            this.pickerStartingDateTimeUtc.CustomFormat = "dd MMM yyyy HH:mm:ss";
            this.pickerStartingDateTimeUtc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerStartingDateTimeUtc.Location = new System.Drawing.Point(424, 48);
            this.pickerStartingDateTimeUtc.MinDate = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.pickerStartingDateTimeUtc.Name = "pickerStartingDateTimeUtc";
            this.pickerStartingDateTimeUtc.ShowCheckBox = true;
            this.pickerStartingDateTimeUtc.Size = new System.Drawing.Size(168, 20);
            this.pickerStartingDateTimeUtc.TabIndex = 7;
            // 
            // checkBoxOffsetInclusive
            // 
            this.checkBoxOffsetInclusive.AutoSize = true;
            this.checkBoxOffsetInclusive.Checked = true;
            this.checkBoxOffsetInclusive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOffsetInclusive.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxOffsetInclusive.Location = new System.Drawing.Point(400, 80);
            this.checkBoxOffsetInclusive.Name = "checkBoxOffsetInclusive";
            this.checkBoxOffsetInclusive.Size = new System.Drawing.Size(75, 17);
            this.checkBoxOffsetInclusive.TabIndex = 12;
            this.checkBoxOffsetInclusive.Text = "Offset Inc.";
            this.checkBoxOffsetInclusive.UseVisualStyleBackColor = true;
            // 
            // txtMaxBatchSize
            // 
            this.txtMaxBatchSize.AllowDecimal = false;
            this.txtMaxBatchSize.AllowNegative = false;
            this.txtMaxBatchSize.AllowSpace = false;
            this.txtMaxBatchSize.Location = new System.Drawing.Point(288, 48);
            this.txtMaxBatchSize.Name = "txtMaxBatchSize";
            this.txtMaxBatchSize.Size = new System.Drawing.Size(120, 20);
            this.txtMaxBatchSize.TabIndex = 5;
            this.txtMaxBatchSize.Text = "100";
            // 
            // lblMaxBatchSize
            // 
            this.lblMaxBatchSize.AutoSize = true;
            this.lblMaxBatchSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxBatchSize.Location = new System.Drawing.Point(288, 32);
            this.lblMaxBatchSize.Name = "lblMaxBatchSize";
            this.lblMaxBatchSize.Size = new System.Drawing.Size(84, 13);
            this.lblMaxBatchSize.TabIndex = 4;
            this.lblMaxBatchSize.Text = "Max Batch Size:";
            // 
            // checkBoxCheckpoint
            // 
            this.checkBoxCheckpoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCheckpoint.AutoSize = true;
            this.checkBoxCheckpoint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxCheckpoint.Location = new System.Drawing.Point(512, 80);
            this.checkBoxCheckpoint.Name = "checkBoxCheckpoint";
            this.checkBoxCheckpoint.Size = new System.Drawing.Size(80, 17);
            this.checkBoxCheckpoint.TabIndex = 13;
            this.checkBoxCheckpoint.Text = "Checkpoint";
            this.checkBoxCheckpoint.UseVisualStyleBackColor = true;
            // 
            // txtReceiveTimeout
            // 
            this.txtReceiveTimeout.AllowDecimal = false;
            this.txtReceiveTimeout.AllowNegative = false;
            this.txtReceiveTimeout.AllowSpace = false;
            this.txtReceiveTimeout.Location = new System.Drawing.Point(152, 48);
            this.txtReceiveTimeout.Name = "txtReceiveTimeout";
            this.txtReceiveTimeout.Size = new System.Drawing.Size(120, 20);
            this.txtReceiveTimeout.TabIndex = 3;
            this.txtReceiveTimeout.Text = "30";
            // 
            // lblReceiveTimeout
            // 
            this.lblReceiveTimeout.AutoSize = true;
            this.lblReceiveTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReceiveTimeout.Location = new System.Drawing.Point(152, 32);
            this.lblReceiveTimeout.Name = "lblReceiveTimeout";
            this.lblReceiveTimeout.Size = new System.Drawing.Size(117, 13);
            this.lblReceiveTimeout.TabIndex = 2;
            this.lblReceiveTimeout.Text = "Receive Timeout (sec):";
            // 
            // checkBoxGraph
            // 
            this.checkBoxGraph.AutoSize = true;
            this.checkBoxGraph.Checked = true;
            this.checkBoxGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGraph.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxGraph.Location = new System.Drawing.Point(304, 80);
            this.checkBoxGraph.Name = "checkBoxGraph";
            this.checkBoxGraph.Size = new System.Drawing.Size(55, 17);
            this.checkBoxGraph.TabIndex = 11;
            this.checkBoxGraph.Text = "Graph";
            this.checkBoxGraph.UseVisualStyleBackColor = true;
            this.checkBoxGraph.CheckedChanged += new System.EventHandler(this.checkBoxGraph_CheckedChanged);
            // 
            // checkBoxLogging
            // 
            this.checkBoxLogging.AutoSize = true;
            this.checkBoxLogging.Checked = true;
            this.checkBoxLogging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLogging.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxLogging.Location = new System.Drawing.Point(16, 80);
            this.checkBoxLogging.Name = "checkBoxLogging";
            this.checkBoxLogging.Size = new System.Drawing.Size(64, 17);
            this.checkBoxLogging.TabIndex = 8;
            this.checkBoxLogging.Text = "Logging";
            this.checkBoxLogging.UseVisualStyleBackColor = true;
            this.checkBoxLogging.CheckedChanged += new System.EventHandler(this.checkBoxLogging_CheckedChanged);
            // 
            // checkBoxTrackMessages
            // 
            this.checkBoxTrackMessages.AutoSize = true;
            this.checkBoxTrackMessages.Checked = true;
            this.checkBoxTrackMessages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTrackMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxTrackMessages.Location = new System.Drawing.Point(208, 80);
            this.checkBoxTrackMessages.Name = "checkBoxTrackMessages";
            this.checkBoxTrackMessages.Size = new System.Drawing.Size(68, 17);
            this.checkBoxTrackMessages.TabIndex = 10;
            this.checkBoxTrackMessages.Text = "Tracking";
            this.checkBoxTrackMessages.UseVisualStyleBackColor = true;
            // 
            // checkBoxVerbose
            // 
            this.checkBoxVerbose.AutoSize = true;
            this.checkBoxVerbose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxVerbose.Location = new System.Drawing.Point(112, 80);
            this.checkBoxVerbose.Name = "checkBoxVerbose";
            this.checkBoxVerbose.Size = new System.Drawing.Size(65, 17);
            this.checkBoxVerbose.TabIndex = 9;
            this.checkBoxVerbose.Text = "Verbose";
            this.checkBoxVerbose.UseVisualStyleBackColor = true;
            // 
            // txtRefreshInformation
            // 
            this.txtRefreshInformation.AllowDecimal = false;
            this.txtRefreshInformation.AllowNegative = false;
            this.txtRefreshInformation.AllowSpace = false;
            this.txtRefreshInformation.Location = new System.Drawing.Point(16, 48);
            this.txtRefreshInformation.Name = "txtRefreshInformation";
            this.txtRefreshInformation.Size = new System.Drawing.Size(120, 20);
            this.txtRefreshInformation.TabIndex = 1;
            this.txtRefreshInformation.Text = "30";
            this.txtRefreshInformation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblRefreshInformation
            // 
            this.lblRefreshInformation.AutoSize = true;
            this.lblRefreshInformation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRefreshInformation.Location = new System.Drawing.Point(16, 32);
            this.lblRefreshInformation.Name = "lblRefreshInformation";
            this.lblRefreshInformation.Size = new System.Drawing.Size(111, 13);
            this.lblRefreshInformation.TabIndex = 0;
            this.lblRefreshInformation.Text = "Refresh Interval (sec):";
            // 
            // tabPageEventData
            // 
            this.tabPageEventData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageEventData.Controls.Add(this.eventDataSplitContainer);
            this.tabPageEventData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageEventData.Location = new System.Drawing.Point(4, 22);
            this.tabPageEventData.Name = "tabPageEventData";
            this.tabPageEventData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEventData.Size = new System.Drawing.Size(968, 454);
            this.tabPageEventData.TabIndex = 5;
            this.tabPageEventData.Text = "Events";
            this.tabPageEventData.Resize += new System.EventHandler(this.tabPageMessages_Resize);
            // 
            // eventDataSplitContainer
            // 
            this.eventDataSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventDataSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.eventDataSplitContainer.Name = "eventDataSplitContainer";
            // 
            // eventDataSplitContainer.Panel1
            // 
            this.eventDataSplitContainer.Panel1.Controls.Add(this.eventDataMainSplitContainer);
            // 
            // eventDataSplitContainer.Panel2
            // 
            this.eventDataSplitContainer.Panel2.Controls.Add(this.eventDataPropertiesSplitContainer);
            this.eventDataSplitContainer.Size = new System.Drawing.Size(936, 432);
            this.eventDataSplitContainer.SplitterDistance = 608;
            this.eventDataSplitContainer.SplitterWidth = 16;
            this.eventDataSplitContainer.TabIndex = 3;
            // 
            // eventDataMainSplitContainer
            // 
            this.eventDataMainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventDataMainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.eventDataMainSplitContainer.Name = "eventDataMainSplitContainer";
            this.eventDataMainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // eventDataMainSplitContainer.Panel1
            // 
            this.eventDataMainSplitContainer.Panel1.Controls.Add(this.grouperMessageList);
            // 
            // eventDataMainSplitContainer.Panel2
            // 
            this.eventDataMainSplitContainer.Panel2.Controls.Add(this.grouperMessageText);
            this.eventDataMainSplitContainer.Size = new System.Drawing.Size(608, 432);
            this.eventDataMainSplitContainer.SplitterDistance = 212;
            this.eventDataMainSplitContainer.SplitterWidth = 8;
            this.eventDataMainSplitContainer.TabIndex = 0;
            // 
            // grouperMessageList
            // 
            this.grouperMessageList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageList.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageList.BorderThickness = 1F;
            this.grouperMessageList.Controls.Add(this.eventDataDataGridView);
            this.grouperMessageList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageList.ForeColor = System.Drawing.Color.White;
            this.grouperMessageList.GroupImage = null;
            this.grouperMessageList.GroupTitle = "Events List";
            this.grouperMessageList.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageList.Name = "grouperMessageList";
            this.grouperMessageList.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageList.PaintGroupBox = true;
            this.grouperMessageList.RoundCorners = 4;
            this.grouperMessageList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageList.ShadowControl = false;
            this.grouperMessageList.ShadowThickness = 1;
            this.grouperMessageList.Size = new System.Drawing.Size(608, 212);
            this.grouperMessageList.TabIndex = 0;
            this.grouperMessageList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageList_CustomPaint);
            // 
            // eventDataDataGridView
            // 
            this.eventDataDataGridView.AllowUserToAddRows = false;
            this.eventDataDataGridView.AllowUserToDeleteRows = false;
            this.eventDataDataGridView.AllowUserToResizeRows = false;
            this.eventDataDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventDataDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.eventDataDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.eventDataDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventDataDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.eventDataDataGridView.Location = new System.Drawing.Point(17, 33);
            this.eventDataDataGridView.Name = "eventDataDataGridView";
            this.eventDataDataGridView.ReadOnly = true;
            this.eventDataDataGridView.RowHeadersWidth = 24;
            this.eventDataDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.eventDataDataGridView.ShowCellErrors = false;
            this.eventDataDataGridView.ShowRowErrors = false;
            this.eventDataDataGridView.Size = new System.Drawing.Size(574, 162);
            this.eventDataDataGridView.TabIndex = 0;
            this.eventDataDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.eventDataDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.eventDataDataGridView_CellDoubleClick);
            this.eventDataDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.eventDataDataGridView_CellFormatting);
            this.eventDataDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.eventDataDataGridView_CellMouseDown);
            this.eventDataDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.eventDataDataGridView_DataError);
            this.eventDataDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.eventDataDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.eventDataDataGridView_RowEnter);
            this.eventDataDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
            this.eventDataDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            this.eventDataDataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
            this.eventDataDataGridView.Resize += new System.EventHandler(this.dataGridView_Resize);
            // 
            // grouperMessageText
            // 
            this.grouperMessageText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageText.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.BorderThickness = 1F;
            this.grouperMessageText.Controls.Add(this.txtMessageText);
            this.grouperMessageText.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageText.ForeColor = System.Drawing.Color.White;
            this.grouperMessageText.GroupImage = null;
            this.grouperMessageText.GroupTitle = "Event Text";
            this.grouperMessageText.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageText.Name = "grouperMessageText";
            this.grouperMessageText.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageText.PaintGroupBox = true;
            this.grouperMessageText.RoundCorners = 4;
            this.grouperMessageText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageText.ShadowControl = false;
            this.grouperMessageText.ShadowThickness = 1;
            this.grouperMessageText.Size = new System.Drawing.Size(608, 212);
            this.grouperMessageText.TabIndex = 0;
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtMessageText.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtMessageText.BackBrush = null;
            this.txtMessageText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessageText.CharHeight = 14;
            this.txtMessageText.CharWidth = 8;
            this.txtMessageText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMessageText.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtMessageText.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtMessageText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtMessageText.IsReplaceMode = false;
            this.txtMessageText.Location = new System.Drawing.Point(16, 32);
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.Paddings = new System.Windows.Forms.Padding(0);
            this.txtMessageText.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtMessageText.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtMessageText.ServiceColors")));
            this.txtMessageText.Size = new System.Drawing.Size(576, 168);
            this.txtMessageText.TabIndex = 0;
            this.txtMessageText.Zoom = 100;
            // 
            // eventDataPropertiesSplitContainer
            // 
            this.eventDataPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventDataPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.eventDataPropertiesSplitContainer.Name = "eventDataPropertiesSplitContainer";
            this.eventDataPropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // eventDataPropertiesSplitContainer.Panel1
            // 
            this.eventDataPropertiesSplitContainer.Panel1.Controls.Add(this.grouperMessageProperties);
            // 
            // eventDataPropertiesSplitContainer.Panel2
            // 
            this.eventDataPropertiesSplitContainer.Panel2.Controls.Add(this.grouperEventDataCustomProperties);
            this.eventDataPropertiesSplitContainer.Size = new System.Drawing.Size(312, 432);
            this.eventDataPropertiesSplitContainer.SplitterDistance = 212;
            this.eventDataPropertiesSplitContainer.SplitterWidth = 8;
            this.eventDataPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperMessageProperties
            // 
            this.grouperMessageProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.BorderThickness = 1F;
            this.grouperMessageProperties.Controls.Add(this.eventDataPropertyGrid);
            this.grouperMessageProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageProperties.GroupImage = null;
            this.grouperMessageProperties.GroupTitle = "Event System Properties";
            this.grouperMessageProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageProperties.Name = "grouperMessageProperties";
            this.grouperMessageProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageProperties.PaintGroupBox = true;
            this.grouperMessageProperties.RoundCorners = 4;
            this.grouperMessageProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageProperties.ShadowControl = false;
            this.grouperMessageProperties.ShadowThickness = 1;
            this.grouperMessageProperties.Size = new System.Drawing.Size(312, 212);
            this.grouperMessageProperties.TabIndex = 0;
            // 
            // eventDataPropertyGrid
            // 
            this.eventDataPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventDataPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.eventDataPropertyGrid.HelpVisible = false;
            this.eventDataPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.eventDataPropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.eventDataPropertyGrid.Name = "eventDataPropertyGrid";
            this.eventDataPropertyGrid.Size = new System.Drawing.Size(280, 164);
            this.eventDataPropertyGrid.TabIndex = 0;
            this.eventDataPropertyGrid.ToolbarVisible = false;
            // 
            // grouperEventDataCustomProperties
            // 
            this.grouperEventDataCustomProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperEventDataCustomProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperEventDataCustomProperties.BackgroundGradientMode = ServiceBusExplorer.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouperEventDataCustomProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventDataCustomProperties.BorderThickness = 1F;
            this.grouperEventDataCustomProperties.Controls.Add(this.eventDataPropertyListView);
            this.grouperEventDataCustomProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventDataCustomProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperEventDataCustomProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperEventDataCustomProperties.ForeColor = System.Drawing.Color.White;
            this.grouperEventDataCustomProperties.GroupImage = null;
            this.grouperEventDataCustomProperties.GroupTitle = "Event Custom Properties";
            this.grouperEventDataCustomProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperEventDataCustomProperties.Name = "grouperEventDataCustomProperties";
            this.grouperEventDataCustomProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperEventDataCustomProperties.PaintGroupBox = true;
            this.grouperEventDataCustomProperties.RoundCorners = 4;
            this.grouperEventDataCustomProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEventDataCustomProperties.ShadowControl = false;
            this.grouperEventDataCustomProperties.ShadowThickness = 1;
            this.grouperEventDataCustomProperties.Size = new System.Drawing.Size(312, 212);
            this.grouperEventDataCustomProperties.TabIndex = 0;
            // 
            // eventDataPropertyListView
            // 
            this.eventDataPropertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventDataPropertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.eventDataPropertyListView.HideSelection = false;
            this.eventDataPropertyListView.Location = new System.Drawing.Point(16, 32);
            this.eventDataPropertyListView.Name = "eventDataPropertyListView";
            this.eventDataPropertyListView.OwnerDraw = true;
            this.eventDataPropertyListView.Size = new System.Drawing.Size(280, 164);
            this.eventDataPropertyListView.TabIndex = 0;
            this.eventDataPropertyListView.UseCompatibleStateImageBehavior = false;
            this.eventDataPropertyListView.View = System.Windows.Forms.View.Details;
            this.eventDataPropertyListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.eventDataPropertyListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.eventDataPropertyListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.eventDataPropertyListView.Resize += new System.EventHandler(this.listView_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 113;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 115;
            // 
            // eventDataContextMenuStrip
            // 
            this.eventDataContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewAndSaveEventDataToolStripMenuItem,
            this.toolStripSeparator,
            this.saveSelectedEventToolStripMenuItem,
            this.saveSelectedEventsToolStripMenuItem});
            this.eventDataContextMenuStrip.Name = "registrationContextMenuStrip";
            this.eventDataContextMenuStrip.Size = new System.Drawing.Size(209, 76);
            // 
            // viewAndSaveEventDataToolStripMenuItem
            // 
            this.viewAndSaveEventDataToolStripMenuItem.Name = "viewAndSaveEventDataToolStripMenuItem";
            this.viewAndSaveEventDataToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.viewAndSaveEventDataToolStripMenuItem.Text = "View and Save Event Data";
            this.viewAndSaveEventDataToolStripMenuItem.Click += new System.EventHandler(this.viewAndSaveEventDataToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(205, 6);
            // 
            // saveSelectedEventToolStripMenuItem
            // 
            this.saveSelectedEventToolStripMenuItem.Name = "saveSelectedEventToolStripMenuItem";
            this.saveSelectedEventToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.saveSelectedEventToolStripMenuItem.Text = "Save Selected Event";
            this.saveSelectedEventToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedEventToolStripMenuItem_Click);
            // 
            // saveSelectedEventsToolStripMenuItem
            // 
            this.saveSelectedEventsToolStripMenuItem.Name = "saveSelectedEventsToolStripMenuItem";
            this.saveSelectedEventsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.saveSelectedEventsToolStripMenuItem.Text = "Save Selected Events";
            this.saveSelectedEventsToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedEventsToolStripMenuItem_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClear.Location = new System.Drawing.Point(760, 504);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 24);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            this.btnClear.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnClear.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // cboReceiverInspector
            // 
            this.cboReceiverInspector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReceiverInspector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReceiverInspector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboReceiverInspector.FormattingEnabled = true;
            this.cboReceiverInspector.Location = new System.Drawing.Point(128, 506);
            this.cboReceiverInspector.Name = "cboReceiverInspector";
            this.cboReceiverInspector.Size = new System.Drawing.Size(512, 21);
            this.cboReceiverInspector.TabIndex = 2;
            // 
            // lblReceiverInspector
            // 
            this.lblReceiverInspector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReceiverInspector.AutoSize = true;
            this.lblReceiverInspector.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReceiverInspector.Location = new System.Drawing.Point(16, 510);
            this.lblReceiverInspector.Name = "lblReceiverInspector";
            this.lblReceiverInspector.Size = new System.Drawing.Size(111, 13);
            this.lblReceiverInspector.TabIndex = 1;
            this.lblReceiverInspector.Text = "Event Data Inspector:";
            // 
            // partitionInformationContextMenuStrip
            // 
            this.partitionInformationContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPartitionInformationToClipboardMenuItem});
            this.partitionInformationContextMenuStrip.Name = "registrationContextMenuStrip";
            this.partitionInformationContextMenuStrip.Size = new System.Drawing.Size(289, 26);
            // 
            // copyPartitionInformationToClipboardMenuItem
            // 
            this.copyPartitionInformationToClipboardMenuItem.Name = "copyPartitionInformationToClipboardMenuItem";
            this.copyPartitionInformationToClipboardMenuItem.Size = new System.Drawing.Size(288, 22);
            this.copyPartitionInformationToClipboardMenuItem.Text = "Copy Partition Information to Clipboard.";
            this.copyPartitionInformationToClipboardMenuItem.ToolTipText = "Copy partition information to clipboard.";
            this.copyPartitionInformationToClipboardMenuItem.Click += new System.EventHandler(this.copyPartitionInformationToClipboardMenuItem_Click);
            // 
            // PartitionListenerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.cboReceiverInspector);
            this.Controls.Add(this.lblReceiverInspector);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.Name = "PartitionListenerControl";
            this.Size = new System.Drawing.Size(1008, 544);
            this.Load += new System.EventHandler(this.ListenerControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PartitionListenerControl_Paint);
            this.mainTabControl.ResumeLayout(false);
            this.tabPageListener.ResumeLayout(false);
            this.grouperStatistics.ResumeLayout(false);
            this.grouperStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.grouperPartitionInformation.ResumeLayout(false);
            this.grouperPartitionInformation.PerformLayout();
            this.grouperOptions.ResumeLayout(false);
            this.grouperOptions.PerformLayout();
            this.tabPageEventData.ResumeLayout(false);
            this.eventDataSplitContainer.Panel1.ResumeLayout(false);
            this.eventDataSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventDataSplitContainer)).EndInit();
            this.eventDataSplitContainer.ResumeLayout(false);
            this.eventDataMainSplitContainer.Panel1.ResumeLayout(false);
            this.eventDataMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventDataMainSplitContainer)).EndInit();
            this.eventDataMainSplitContainer.ResumeLayout(false);
            this.grouperMessageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventDataDataGridView)).EndInit();
            this.grouperMessageText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMessageText)).EndInit();
            this.eventDataPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.eventDataPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventDataPropertiesSplitContainer)).EndInit();
            this.eventDataPropertiesSplitContainer.ResumeLayout(false);
            this.grouperMessageProperties.ResumeLayout(false);
            this.grouperEventDataCustomProperties.ResumeLayout(false);
            this.eventDataContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventDataBindingSource)).EndInit();
            this.partitionInformationContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStart;
        internal System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPageListener;
        private System.Windows.Forms.TabPage tabPageEventData;
        private Grouper grouperPartitionInformation;
        private System.Windows.Forms.ListView propertyListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private Grouper grouperOptions;
        private System.Windows.Forms.SplitContainer eventDataSplitContainer;
        private System.Windows.Forms.BindingSource eventDataBindingSource;
        private System.Windows.Forms.SplitContainer eventDataMainSplitContainer;
        private Grouper grouperMessageList;
        private System.Windows.Forms.DataGridView eventDataDataGridView;
        private System.Windows.Forms.ContextMenuStrip eventDataContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewAndSaveEventDataToolStripMenuItem;
        private NumericTextBox txtRefreshInformation;
        private System.Windows.Forms.Label lblRefreshInformation;
        private Grouper grouperStatistics;
        private NumericTextBox txtEventDataTotal;
        private System.Windows.Forms.Label lblEventDataTotal;
        private NumericTextBox txtEventDataPerSecond;
        private System.Windows.Forms.Label lblEventDataPerSecond;
        internal System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.CheckBox checkBoxLogging;
        private System.Windows.Forms.CheckBox checkBoxVerbose;
        private System.Windows.Forms.CheckBox checkBoxTrackMessages;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox checkBoxGraph;
        private NumericTextBox txtReceiveTimeout;
        private System.Windows.Forms.Label lblReceiveTimeout;
        private System.Windows.Forms.Label lblPartitionInformation;
        private System.Windows.Forms.Label lblPartition;
        private System.Windows.Forms.ComboBox cboPartition;
        private System.Windows.Forms.CheckBox checkBoxCheckpoint;
        private NumericTextBox txtAverageDuration;
        private System.Windows.Forms.Label lblAverageTime;
        private System.Windows.Forms.Label lblMessageSizePerSecond;
        private NumericTextBox txtMessageSizePerSecond;
        private System.Windows.Forms.ComboBox cboMessageSizePerSecond;
        private System.Windows.Forms.ComboBox cboAverageDuration;
        private System.Windows.Forms.ComboBox cboEventDataPerSecond;
        private System.Windows.Forms.ComboBox cboReceiverInspector;
        private System.Windows.Forms.Label lblReceiverInspector;
        private System.Windows.Forms.ContextMenuStrip partitionInformationContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyPartitionInformationToClipboardMenuItem;
        private NumericTextBox txtMaxBatchSize;
        private System.Windows.Forms.Label lblMaxBatchSize;
        private System.Windows.Forms.CheckBox checkBoxOffsetInclusive;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedEventsToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker pickerStartingDateTimeUtc;
        private System.Windows.Forms.Label lblStartingDateTimeUtc;
        private Grouper grouperMessageText;
        private FastColoredTextBoxNS.FastColoredTextBox txtMessageText;
        private System.Windows.Forms.SplitContainer eventDataPropertiesSplitContainer;
        private Grouper grouperMessageProperties;
        private System.Windows.Forms.PropertyGrid eventDataPropertyGrid;
        private Grouper grouperEventDataCustomProperties;
        private System.Windows.Forms.ListView eventDataPropertyListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
