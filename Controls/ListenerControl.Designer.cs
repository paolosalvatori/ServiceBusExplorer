namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    partial class ListenerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPageListener = new System.Windows.Forms.TabPage();
            this.grouperStatistics = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtMessagesTotal = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.lblMessagesTotal = new System.Windows.Forms.Label();
            this.txtMessagePerSecond = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.lblMessagePerSecond = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grouperQueueInformation = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.propertyListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grouperOptions = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtPrefetchCount = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.lblPrefetchCount = new System.Windows.Forms.Label();
            this.lblReceiveMode = new System.Windows.Forms.Label();
            this.cboReceivedMode = new System.Windows.Forms.ComboBox();
            this.checkBoxGraph = new System.Windows.Forms.CheckBox();
            this.checkBoxLogging = new System.Windows.Forms.CheckBox();
            this.checkBoxTrackMessages = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoComplete = new System.Windows.Forms.CheckBox();
            this.checkBoxVerbose = new System.Windows.Forms.CheckBox();
            this.txtRefreshInformation = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.lblRefreshInformation = new System.Windows.Forms.Label();
            this.txtMaxConcurrentCalls = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.lblMaxConcurrentCalls = new System.Windows.Forms.Label();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.messagesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.messageListTextPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageList = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.pictFindMessages = new System.Windows.Forms.PictureBox();
            this.messagesDataGridView = new System.Windows.Forms.DataGridView();
            this.messagesCustomPropertiesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.grouperMessageText = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.grouperMessageCustomProperties = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.messagePropertyListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grouperMessageProperties = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.messagePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.messagesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.repairAndResubmitMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.tabPageListener.SuspendLayout();
            this.grouperStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.grouperQueueInformation.SuspendLayout();
            this.grouperOptions.SuspendLayout();
            this.tabPageMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesSplitContainer)).BeginInit();
            this.messagesSplitContainer.Panel1.SuspendLayout();
            this.messagesSplitContainer.Panel2.SuspendLayout();
            this.messagesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).BeginInit();
            this.messageListTextPropertiesSplitContainer.Panel1.SuspendLayout();
            this.messageListTextPropertiesSplitContainer.Panel2.SuspendLayout();
            this.messageListTextPropertiesSplitContainer.SuspendLayout();
            this.grouperMessageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictFindMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesCustomPropertiesSplitContainer)).BeginInit();
            this.messagesCustomPropertiesSplitContainer.Panel1.SuspendLayout();
            this.messagesCustomPropertiesSplitContainer.Panel2.SuspendLayout();
            this.messagesCustomPropertiesSplitContainer.SuspendLayout();
            this.grouperMessageText.SuspendLayout();
            this.grouperMessageCustomProperties.SuspendLayout();
            this.grouperMessageProperties.SuspendLayout();
            this.messagesContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).BeginInit();
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
            this.btnStart.TabIndex = 3;
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
            this.mainTabControl.Controls.Add(this.tabPageMessages);
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.ItemSize = new System.Drawing.Size(76, 18);
            this.mainTabControl.Location = new System.Drawing.Point(16, 16);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(976, 480);
            this.mainTabControl.TabIndex = 11;
            this.mainTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mainTabControl_DrawItem);
            // 
            // tabPageListener
            // 
            this.tabPageListener.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageListener.Controls.Add(this.grouperStatistics);
            this.tabPageListener.Controls.Add(this.chart);
            this.tabPageListener.Controls.Add(this.grouperQueueInformation);
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
            this.grouperStatistics.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperStatistics.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperStatistics.BorderThickness = 1F;
            this.grouperStatistics.Controls.Add(this.txtMessagesTotal);
            this.grouperStatistics.Controls.Add(this.lblMessagesTotal);
            this.grouperStatistics.Controls.Add(this.txtMessagePerSecond);
            this.grouperStatistics.Controls.Add(this.lblMessagePerSecond);
            this.grouperStatistics.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperStatistics.ForeColor = System.Drawing.Color.White;
            this.grouperStatistics.GroupImage = null;
            this.grouperStatistics.GroupTitle = "Statistics";
            this.grouperStatistics.Location = new System.Drawing.Point(640, 16);
            this.grouperStatistics.Name = "grouperStatistics";
            this.grouperStatistics.Padding = new System.Windows.Forms.Padding(20);
            this.grouperStatistics.PaintGroupBox = true;
            this.grouperStatistics.RoundCorners = 4;
            this.grouperStatistics.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperStatistics.ShadowControl = false;
            this.grouperStatistics.ShadowThickness = 1;
            this.grouperStatistics.Size = new System.Drawing.Size(312, 80);
            this.grouperStatistics.TabIndex = 127;
            this.grouperStatistics.Resize += new System.EventHandler(this.grouperStatistics_Resize);
            // 
            // txtMessagesTotal
            // 
            this.txtMessagesTotal.AllowSpace = false;
            this.txtMessagesTotal.Location = new System.Drawing.Point(160, 48);
            this.txtMessagesTotal.Name = "txtMessagesTotal";
            this.txtMessagesTotal.Size = new System.Drawing.Size(136, 20);
            this.txtMessagesTotal.TabIndex = 26;
            // 
            // lblMessagesTotal
            // 
            this.lblMessagesTotal.AutoSize = true;
            this.lblMessagesTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessagesTotal.Location = new System.Drawing.Point(160, 32);
            this.lblMessagesTotal.Name = "lblMessagesTotal";
            this.lblMessagesTotal.Size = new System.Drawing.Size(85, 13);
            this.lblMessagesTotal.TabIndex = 25;
            this.lblMessagesTotal.Text = "Messages Total:";
            // 
            // txtMessagePerSecond
            // 
            this.txtMessagePerSecond.AllowSpace = false;
            this.txtMessagePerSecond.Location = new System.Drawing.Point(16, 48);
            this.txtMessagePerSecond.Name = "txtMessagePerSecond";
            this.txtMessagePerSecond.Size = new System.Drawing.Size(128, 20);
            this.txtMessagePerSecond.TabIndex = 24;
            // 
            // lblMessagePerSecond
            // 
            this.lblMessagePerSecond.AutoSize = true;
            this.lblMessagePerSecond.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMessagePerSecond.Location = new System.Drawing.Point(16, 32);
            this.lblMessagePerSecond.Name = "lblMessagePerSecond";
            this.lblMessagePerSecond.Size = new System.Drawing.Size(82, 13);
            this.lblMessagePerSecond.TabIndex = 23;
            this.lblMessagePerSecond.Text = "Messages/Sec:";
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
            chartArea1.AxisX.Title = "Messages";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisY.ScrollBar.Size = 10D;
            chartArea1.AxisY.Title = "Time - Msg/Sec";
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
            this.chart.Location = new System.Drawing.Point(16, 24);
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
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(608, 304);
            this.chart.TabIndex = 128;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title";
            title1.ShadowColor = System.Drawing.Color.Transparent;
            title1.ShadowOffset = 1;
            title1.Text = "Listener Performance Counters";
            this.chart.Titles.Add(title1);
            // 
            // grouperQueueInformation
            // 
            this.grouperQueueInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperQueueInformation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperQueueInformation.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperQueueInformation.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperQueueInformation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueInformation.BorderThickness = 1F;
            this.grouperQueueInformation.Controls.Add(this.propertyListView);
            this.grouperQueueInformation.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperQueueInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperQueueInformation.ForeColor = System.Drawing.Color.White;
            this.grouperQueueInformation.GroupImage = null;
            this.grouperQueueInformation.GroupTitle = "Queue Information";
            this.grouperQueueInformation.Location = new System.Drawing.Point(640, 104);
            this.grouperQueueInformation.Name = "grouperQueueInformation";
            this.grouperQueueInformation.Padding = new System.Windows.Forms.Padding(20);
            this.grouperQueueInformation.PaintGroupBox = true;
            this.grouperQueueInformation.RoundCorners = 4;
            this.grouperQueueInformation.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperQueueInformation.ShadowControl = false;
            this.grouperQueueInformation.ShadowThickness = 1;
            this.grouperQueueInformation.Size = new System.Drawing.Size(312, 336);
            this.grouperQueueInformation.TabIndex = 7;
            // 
            // propertyListView
            // 
            this.propertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.valueColumnHeader});
            this.propertyListView.Location = new System.Drawing.Point(16, 32);
            this.propertyListView.Name = "propertyListView";
            this.propertyListView.OwnerDraw = true;
            this.propertyListView.Size = new System.Drawing.Size(280, 288);
            this.propertyListView.TabIndex = 0;
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
            this.grouperOptions.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperOptions.BorderThickness = 1F;
            this.grouperOptions.Controls.Add(this.txtPrefetchCount);
            this.grouperOptions.Controls.Add(this.lblPrefetchCount);
            this.grouperOptions.Controls.Add(this.lblReceiveMode);
            this.grouperOptions.Controls.Add(this.cboReceivedMode);
            this.grouperOptions.Controls.Add(this.checkBoxGraph);
            this.grouperOptions.Controls.Add(this.checkBoxLogging);
            this.grouperOptions.Controls.Add(this.checkBoxTrackMessages);
            this.grouperOptions.Controls.Add(this.checkBoxAutoComplete);
            this.grouperOptions.Controls.Add(this.checkBoxVerbose);
            this.grouperOptions.Controls.Add(this.txtRefreshInformation);
            this.grouperOptions.Controls.Add(this.lblRefreshInformation);
            this.grouperOptions.Controls.Add(this.txtMaxConcurrentCalls);
            this.grouperOptions.Controls.Add(this.lblMaxConcurrentCalls);
            this.grouperOptions.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperOptions.ForeColor = System.Drawing.Color.White;
            this.grouperOptions.GroupImage = null;
            this.grouperOptions.GroupTitle = "Options";
            this.grouperOptions.Location = new System.Drawing.Point(16, 336);
            this.grouperOptions.Name = "grouperOptions";
            this.grouperOptions.Padding = new System.Windows.Forms.Padding(20);
            this.grouperOptions.PaintGroupBox = true;
            this.grouperOptions.RoundCorners = 4;
            this.grouperOptions.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperOptions.ShadowControl = false;
            this.grouperOptions.ShadowThickness = 1;
            this.grouperOptions.Size = new System.Drawing.Size(608, 104);
            this.grouperOptions.TabIndex = 0;
            this.grouperOptions.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperOptions_CustomPaint);
            this.grouperOptions.Resize += new System.EventHandler(this.grouperOptions_Resize);
            // 
            // txtPrefetchCount
            // 
            this.txtPrefetchCount.AllowSpace = false;
            this.txtPrefetchCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrefetchCount.Location = new System.Drawing.Point(256, 48);
            this.txtPrefetchCount.Name = "txtPrefetchCount";
            this.txtPrefetchCount.Size = new System.Drawing.Size(104, 20);
            this.txtPrefetchCount.TabIndex = 139;
            this.txtPrefetchCount.Text = "100";
            // 
            // lblPrefetchCount
            // 
            this.lblPrefetchCount.AutoSize = true;
            this.lblPrefetchCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrefetchCount.Location = new System.Drawing.Point(256, 32);
            this.lblPrefetchCount.Name = "lblPrefetchCount";
            this.lblPrefetchCount.Size = new System.Drawing.Size(81, 13);
            this.lblPrefetchCount.TabIndex = 138;
            this.lblPrefetchCount.Text = "Prefetch Count:";
            // 
            // lblReceiveMode
            // 
            this.lblReceiveMode.AutoSize = true;
            this.lblReceiveMode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReceiveMode.Location = new System.Drawing.Point(376, 32);
            this.lblReceiveMode.Name = "lblReceiveMode";
            this.lblReceiveMode.Size = new System.Drawing.Size(80, 13);
            this.lblReceiveMode.TabIndex = 137;
            this.lblReceiveMode.Text = "Receive Mode:";
            // 
            // cboReceivedMode
            // 
            this.cboReceivedMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReceivedMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReceivedMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboReceivedMode.FormattingEnabled = true;
            this.cboReceivedMode.Items.AddRange(new object[] {
            "ReceiveDelete",
            "PeekLock"});
            this.cboReceivedMode.Location = new System.Drawing.Point(376, 48);
            this.cboReceivedMode.Name = "cboReceivedMode";
            this.cboReceivedMode.Size = new System.Drawing.Size(104, 21);
            this.cboReceivedMode.TabIndex = 131;
            this.cboReceivedMode.SelectedIndexChanged += new System.EventHandler(this.cboReceivedMode_SelectedIndexChanged);
            // 
            // checkBoxGraph
            // 
            this.checkBoxGraph.AutoSize = true;
            this.checkBoxGraph.Checked = true;
            this.checkBoxGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGraph.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxGraph.Location = new System.Drawing.Point(520, 80);
            this.checkBoxGraph.Name = "checkBoxGraph";
            this.checkBoxGraph.Size = new System.Drawing.Size(55, 17);
            this.checkBoxGraph.TabIndex = 30;
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
            this.checkBoxLogging.Location = new System.Drawing.Point(136, 80);
            this.checkBoxLogging.Name = "checkBoxLogging";
            this.checkBoxLogging.Size = new System.Drawing.Size(64, 17);
            this.checkBoxLogging.TabIndex = 27;
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
            this.checkBoxTrackMessages.Location = new System.Drawing.Point(376, 80);
            this.checkBoxTrackMessages.Name = "checkBoxTrackMessages";
            this.checkBoxTrackMessages.Size = new System.Drawing.Size(68, 17);
            this.checkBoxTrackMessages.TabIndex = 29;
            this.checkBoxTrackMessages.Text = "Tracking";
            this.checkBoxTrackMessages.UseVisualStyleBackColor = true;
            this.checkBoxTrackMessages.CheckedChanged += new System.EventHandler(this.checkBoxTrackMessages_CheckedChanged);
            // 
            // checkBoxAutoComplete
            // 
            this.checkBoxAutoComplete.AutoSize = true;
            this.checkBoxAutoComplete.Checked = true;
            this.checkBoxAutoComplete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoComplete.Enabled = false;
            this.checkBoxAutoComplete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAutoComplete.Location = new System.Drawing.Point(16, 80);
            this.checkBoxAutoComplete.Name = "checkBoxAutoComplete";
            this.checkBoxAutoComplete.Size = new System.Drawing.Size(95, 17);
            this.checkBoxAutoComplete.TabIndex = 24;
            this.checkBoxAutoComplete.Text = "Auto Complete";
            this.checkBoxAutoComplete.UseVisualStyleBackColor = true;
            this.checkBoxAutoComplete.CheckedChanged += new System.EventHandler(this.checkBoxAutoComplete_CheckedChanged);
            // 
            // checkBoxVerbose
            // 
            this.checkBoxVerbose.AutoSize = true;
            this.checkBoxVerbose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxVerbose.Location = new System.Drawing.Point(256, 80);
            this.checkBoxVerbose.Name = "checkBoxVerbose";
            this.checkBoxVerbose.Size = new System.Drawing.Size(65, 17);
            this.checkBoxVerbose.TabIndex = 28;
            this.checkBoxVerbose.Text = "Verbose";
            this.checkBoxVerbose.UseVisualStyleBackColor = true;
            this.checkBoxVerbose.CheckedChanged += new System.EventHandler(this.checkBoxVerbose_CheckedChanged);
            // 
            // txtRefreshInformation
            // 
            this.txtRefreshInformation.AllowSpace = false;
            this.txtRefreshInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRefreshInformation.Location = new System.Drawing.Point(136, 48);
            this.txtRefreshInformation.Name = "txtRefreshInformation";
            this.txtRefreshInformation.Size = new System.Drawing.Size(104, 20);
            this.txtRefreshInformation.TabIndex = 26;
            this.txtRefreshInformation.Text = "1";
            this.txtRefreshInformation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblRefreshInformation
            // 
            this.lblRefreshInformation.AutoSize = true;
            this.lblRefreshInformation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRefreshInformation.Location = new System.Drawing.Point(136, 32);
            this.lblRefreshInformation.Name = "lblRefreshInformation";
            this.lblRefreshInformation.Size = new System.Drawing.Size(111, 13);
            this.lblRefreshInformation.TabIndex = 25;
            this.lblRefreshInformation.Text = "Refresh Interval (sec):";
            // 
            // txtMaxConcurrentCalls
            // 
            this.txtMaxConcurrentCalls.AllowSpace = false;
            this.txtMaxConcurrentCalls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxConcurrentCalls.Location = new System.Drawing.Point(16, 48);
            this.txtMaxConcurrentCalls.Name = "txtMaxConcurrentCalls";
            this.txtMaxConcurrentCalls.Size = new System.Drawing.Size(104, 20);
            this.txtMaxConcurrentCalls.TabIndex = 23;
            this.txtMaxConcurrentCalls.Text = "1";
            this.txtMaxConcurrentCalls.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // lblMaxConcurrentCalls
            // 
            this.lblMaxConcurrentCalls.AutoSize = true;
            this.lblMaxConcurrentCalls.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMaxConcurrentCalls.Location = new System.Drawing.Point(16, 32);
            this.lblMaxConcurrentCalls.Name = "lblMaxConcurrentCalls";
            this.lblMaxConcurrentCalls.Size = new System.Drawing.Size(110, 13);
            this.lblMaxConcurrentCalls.TabIndex = 22;
            this.lblMaxConcurrentCalls.Text = "Max Concurrent Calls:";
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageMessages.Controls.Add(this.messagesSplitContainer);
            this.tabPageMessages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessages.Size = new System.Drawing.Size(968, 454);
            this.tabPageMessages.TabIndex = 5;
            this.tabPageMessages.Text = "Messages";
            this.tabPageMessages.Resize += new System.EventHandler(this.tabPageMessages_Resize);
            // 
            // messagesSplitContainer
            // 
            this.messagesSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesSplitContainer.Location = new System.Drawing.Point(16, 8);
            this.messagesSplitContainer.Name = "messagesSplitContainer";
            // 
            // messagesSplitContainer.Panel1
            // 
            this.messagesSplitContainer.Panel1.Controls.Add(this.messageListTextPropertiesSplitContainer);
            // 
            // messagesSplitContainer.Panel2
            // 
            this.messagesSplitContainer.Panel2.Controls.Add(this.grouperMessageProperties);
            this.messagesSplitContainer.Size = new System.Drawing.Size(936, 432);
            this.messagesSplitContainer.SplitterDistance = 608;
            this.messagesSplitContainer.SplitterWidth = 16;
            this.messagesSplitContainer.TabIndex = 3;
            // 
            // messageListTextPropertiesSplitContainer
            // 
            this.messageListTextPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageListTextPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messageListTextPropertiesSplitContainer.Name = "messageListTextPropertiesSplitContainer";
            this.messageListTextPropertiesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // messageListTextPropertiesSplitContainer.Panel1
            // 
            this.messageListTextPropertiesSplitContainer.Panel1.Controls.Add(this.grouperMessageList);
            // 
            // messageListTextPropertiesSplitContainer.Panel2
            // 
            this.messageListTextPropertiesSplitContainer.Panel2.Controls.Add(this.messagesCustomPropertiesSplitContainer);
            this.messageListTextPropertiesSplitContainer.Size = new System.Drawing.Size(608, 432);
            this.messageListTextPropertiesSplitContainer.SplitterDistance = 212;
            this.messageListTextPropertiesSplitContainer.SplitterWidth = 8;
            this.messageListTextPropertiesSplitContainer.TabIndex = 0;
            // 
            // grouperMessageList
            // 
            this.grouperMessageList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageList.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageList.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageList.BorderThickness = 1F;
            this.grouperMessageList.Controls.Add(this.pictFindMessages);
            this.grouperMessageList.Controls.Add(this.messagesDataGridView);
            this.grouperMessageList.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageList.ForeColor = System.Drawing.Color.White;
            this.grouperMessageList.GroupImage = null;
            this.grouperMessageList.GroupTitle = "Message List";
            this.grouperMessageList.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageList.Name = "grouperMessageList";
            this.grouperMessageList.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageList.PaintGroupBox = true;
            this.grouperMessageList.RoundCorners = 4;
            this.grouperMessageList.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageList.ShadowControl = false;
            this.grouperMessageList.ShadowThickness = 1;
            this.grouperMessageList.Size = new System.Drawing.Size(608, 212);
            this.grouperMessageList.TabIndex = 17;
            this.grouperMessageList.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageList_CustomPaint);
            // 
            // pictFindMessages
            // 
            this.pictFindMessages.Image = global::Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Properties.Resources.FindExtension;
            this.pictFindMessages.Location = new System.Drawing.Point(100, 0);
            this.pictFindMessages.Name = "pictFindMessages";
            this.pictFindMessages.Size = new System.Drawing.Size(24, 24);
            this.pictFindMessages.TabIndex = 1;
            this.pictFindMessages.TabStop = false;
            this.pictFindMessages.Click += new System.EventHandler(this.pictFindMessages_Click);
            this.pictFindMessages.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictFindMessages.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            // 
            // messagesDataGridView
            // 
            this.messagesDataGridView.AllowUserToAddRows = false;
            this.messagesDataGridView.AllowUserToDeleteRows = false;
            this.messagesDataGridView.AllowUserToResizeRows = false;
            this.messagesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.messagesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messagesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.messagesDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.messagesDataGridView.Location = new System.Drawing.Point(17, 33);
            this.messagesDataGridView.Name = "messagesDataGridView";
            this.messagesDataGridView.ReadOnly = true;
            this.messagesDataGridView.RowHeadersWidth = 24;
            this.messagesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.messagesDataGridView.ShowCellErrors = false;
            this.messagesDataGridView.ShowRowErrors = false;
            this.messagesDataGridView.Size = new System.Drawing.Size(574, 162);
            this.messagesDataGridView.TabIndex = 0;
            this.messagesDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.messagesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.messagesDataGridView_CellDoubleClick);
            this.messagesDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.messagesDataGridView_CellFormatting);
            this.messagesDataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.messagesDataGridView_CellMouseDown);
            this.messagesDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            this.messagesDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.messagesDataGridView_RowEnter);
            this.messagesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
            this.messagesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
            this.messagesDataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
            this.messagesDataGridView.Resize += new System.EventHandler(this.dataGridView_Resize);
            // 
            // messagesCustomPropertiesSplitContainer
            // 
            this.messagesCustomPropertiesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesCustomPropertiesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.messagesCustomPropertiesSplitContainer.Name = "messagesCustomPropertiesSplitContainer";
            // 
            // messagesCustomPropertiesSplitContainer.Panel1
            // 
            this.messagesCustomPropertiesSplitContainer.Panel1.Controls.Add(this.grouperMessageText);
            // 
            // messagesCustomPropertiesSplitContainer.Panel2
            // 
            this.messagesCustomPropertiesSplitContainer.Panel2.Controls.Add(this.grouperMessageCustomProperties);
            this.messagesCustomPropertiesSplitContainer.Size = new System.Drawing.Size(608, 212);
            this.messagesCustomPropertiesSplitContainer.SplitterDistance = 328;
            this.messagesCustomPropertiesSplitContainer.SplitterWidth = 16;
            this.messagesCustomPropertiesSplitContainer.TabIndex = 26;
            // 
            // grouperMessageText
            // 
            this.grouperMessageText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageText.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.BorderThickness = 1F;
            this.grouperMessageText.Controls.Add(this.txtMessageText);
            this.grouperMessageText.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageText.ForeColor = System.Drawing.Color.White;
            this.grouperMessageText.GroupImage = null;
            this.grouperMessageText.GroupTitle = "Message Text";
            this.grouperMessageText.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageText.Name = "grouperMessageText";
            this.grouperMessageText.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageText.PaintGroupBox = true;
            this.grouperMessageText.RoundCorners = 4;
            this.grouperMessageText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageText.ShadowControl = false;
            this.grouperMessageText.ShadowThickness = 1;
            this.grouperMessageText.Size = new System.Drawing.Size(328, 212);
            this.grouperMessageText.TabIndex = 25;
            this.grouperMessageText.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageText_CustomPaint);
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessageText.Location = new System.Drawing.Point(16, 32);
            this.txtMessageText.Multiline = true;
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageText.Size = new System.Drawing.Size(296, 164);
            this.txtMessageText.TabIndex = 0;
            // 
            // grouperMessageCustomProperties
            // 
            this.grouperMessageCustomProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageCustomProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageCustomProperties.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageCustomProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageCustomProperties.BorderThickness = 1F;
            this.grouperMessageCustomProperties.Controls.Add(this.messagePropertyListView);
            this.grouperMessageCustomProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageCustomProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageCustomProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageCustomProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageCustomProperties.GroupImage = null;
            this.grouperMessageCustomProperties.GroupTitle = "Message Custom Properties";
            this.grouperMessageCustomProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageCustomProperties.Name = "grouperMessageCustomProperties";
            this.grouperMessageCustomProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageCustomProperties.PaintGroupBox = true;
            this.grouperMessageCustomProperties.RoundCorners = 4;
            this.grouperMessageCustomProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageCustomProperties.ShadowControl = false;
            this.grouperMessageCustomProperties.ShadowThickness = 1;
            this.grouperMessageCustomProperties.Size = new System.Drawing.Size(264, 212);
            this.grouperMessageCustomProperties.TabIndex = 26;
            this.grouperMessageCustomProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageCustomProperties_CustomPaint);
            // 
            // messagePropertyListView
            // 
            this.messagePropertyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagePropertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.messagePropertyListView.Location = new System.Drawing.Point(16, 32);
            this.messagePropertyListView.Name = "messagePropertyListView";
            this.messagePropertyListView.OwnerDraw = true;
            this.messagePropertyListView.Size = new System.Drawing.Size(232, 164);
            this.messagePropertyListView.TabIndex = 0;
            this.messagePropertyListView.UseCompatibleStateImageBehavior = false;
            this.messagePropertyListView.View = System.Windows.Forms.View.Details;
            this.messagePropertyListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.messagePropertyListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.messagePropertyListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.messagePropertyListView.Resize += new System.EventHandler(this.listView_Resize);
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
            // grouperMessageProperties
            // 
            this.grouperMessageProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageProperties.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.BorderThickness = 1F;
            this.grouperMessageProperties.Controls.Add(this.messagePropertyGrid);
            this.grouperMessageProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grouperMessageProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageProperties.GroupImage = null;
            this.grouperMessageProperties.GroupTitle = "Message Properties";
            this.grouperMessageProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageProperties.Name = "grouperMessageProperties";
            this.grouperMessageProperties.Padding = new System.Windows.Forms.Padding(20);
            this.grouperMessageProperties.PaintGroupBox = true;
            this.grouperMessageProperties.RoundCorners = 4;
            this.grouperMessageProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageProperties.ShadowControl = false;
            this.grouperMessageProperties.ShadowThickness = 1;
            this.grouperMessageProperties.Size = new System.Drawing.Size(312, 432);
            this.grouperMessageProperties.TabIndex = 19;
            this.grouperMessageProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageProperties_CustomPaint);
            // 
            // messagePropertyGrid
            // 
            this.messagePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messagePropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.messagePropertyGrid.HelpVisible = false;
            this.messagePropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.messagePropertyGrid.Location = new System.Drawing.Point(16, 32);
            this.messagePropertyGrid.Name = "messagePropertyGrid";
            this.messagePropertyGrid.Size = new System.Drawing.Size(280, 384);
            this.messagePropertyGrid.TabIndex = 2;
            this.messagePropertyGrid.ToolbarVisible = false;
            // 
            // messagesContextMenuStrip
            // 
            this.messagesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repairAndResubmitMessageToolStripMenuItem,
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem});
            this.messagesContextMenuStrip.Name = "registrationContextMenuStrip";
            this.messagesContextMenuStrip.Size = new System.Drawing.Size(306, 48);
            // 
            // repairAndResubmitMessageToolStripMenuItem
            // 
            this.repairAndResubmitMessageToolStripMenuItem.Name = "repairAndResubmitMessageToolStripMenuItem";
            this.repairAndResubmitMessageToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.repairAndResubmitMessageToolStripMenuItem.Text = "Repair and Resubmit Selected Message";
            this.repairAndResubmitMessageToolStripMenuItem.Click += new System.EventHandler(this.repairAndResubmitMessageToolStripMenuItem_Click);
            // 
            // resubmitSelectedMessagesInBatchModeToolStripMenuItem
            // 
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Name = "resubmitSelectedMessagesInBatchModeToolStripMenuItem";
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Size = new System.Drawing.Size(305, 22);
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Text = "Resubmit Selected Messages In Batch Mode";
            this.resubmitSelectedMessagesInBatchModeToolStripMenuItem.Click += new System.EventHandler(this.resubmitSelectedMessagesInBatchModeToolStripMenuItem_Click);
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
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            this.btnClear.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnClear.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // ListenerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.Name = "ListenerControl";
            this.Size = new System.Drawing.Size(1008, 544);
            this.Load += new System.EventHandler(this.ListenerControl_Load);
            this.mainTabControl.ResumeLayout(false);
            this.tabPageListener.ResumeLayout(false);
            this.grouperStatistics.ResumeLayout(false);
            this.grouperStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.grouperQueueInformation.ResumeLayout(false);
            this.grouperOptions.ResumeLayout(false);
            this.grouperOptions.PerformLayout();
            this.tabPageMessages.ResumeLayout(false);
            this.messagesSplitContainer.Panel1.ResumeLayout(false);
            this.messagesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesSplitContainer)).EndInit();
            this.messagesSplitContainer.ResumeLayout(false);
            this.messageListTextPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.messageListTextPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageListTextPropertiesSplitContainer)).EndInit();
            this.messageListTextPropertiesSplitContainer.ResumeLayout(false);
            this.grouperMessageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictFindMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesDataGridView)).EndInit();
            this.messagesCustomPropertiesSplitContainer.Panel1.ResumeLayout(false);
            this.messagesCustomPropertiesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesCustomPropertiesSplitContainer)).EndInit();
            this.messagesCustomPropertiesSplitContainer.ResumeLayout(false);
            this.grouperMessageText.ResumeLayout(false);
            this.grouperMessageText.PerformLayout();
            this.grouperMessageCustomProperties.ResumeLayout(false);
            this.grouperMessageProperties.ResumeLayout(false);
            this.messagesContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStart;
        internal System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPageListener;
        private System.Windows.Forms.TabPage tabPageMessages;
        private Grouper grouperQueueInformation;
        private System.Windows.Forms.ListView propertyListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private Grouper grouperOptions;
        private System.Windows.Forms.Label lblMaxConcurrentCalls;
        private System.Windows.Forms.SplitContainer messagesSplitContainer;
        private Grouper grouperMessageProperties;
        private System.Windows.Forms.BindingSource messagesBindingSource;
        private System.Windows.Forms.SplitContainer messageListTextPropertiesSplitContainer;
        private Grouper grouperMessageList;
        private System.Windows.Forms.DataGridView messagesDataGridView;
        private System.Windows.Forms.SplitContainer messagesCustomPropertiesSplitContainer;
        private Grouper grouperMessageText;
        private System.Windows.Forms.TextBox txtMessageText;
        private Grouper grouperMessageCustomProperties;
        private System.Windows.Forms.ListView messagePropertyListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.PropertyGrid messagePropertyGrid;
        private System.Windows.Forms.ContextMenuStrip messagesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem repairAndResubmitMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resubmitSelectedMessagesInBatchModeToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictFindMessages;
        private NumericTextBox txtMaxConcurrentCalls;
        private System.Windows.Forms.CheckBox checkBoxAutoComplete;
        private NumericTextBox txtRefreshInformation;
        private System.Windows.Forms.Label lblRefreshInformation;
        private Grouper grouperStatistics;
        private NumericTextBox txtMessagesTotal;
        private System.Windows.Forms.Label lblMessagesTotal;
        private NumericTextBox txtMessagePerSecond;
        private System.Windows.Forms.Label lblMessagePerSecond;
        internal System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.CheckBox checkBoxLogging;
        private System.Windows.Forms.CheckBox checkBoxVerbose;
        private System.Windows.Forms.CheckBox checkBoxTrackMessages;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox checkBoxGraph;
        private System.Windows.Forms.ComboBox cboReceivedMode;
        private System.Windows.Forms.Label lblReceiveMode;
        private NumericTextBox txtPrefetchCount;
        private System.Windows.Forms.Label lblPrefetchCount;
    }
}
