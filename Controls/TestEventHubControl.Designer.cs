﻿namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    partial class TestEventHubControl
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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.mainTabMessagePage = new System.Windows.Forms.TabPage();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.messageTabControl = new System.Windows.Forms.TabControl();
            this.tabMessagePage = new System.Windows.Forms.TabPage();
            this.grouperPartitionKey = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtPartitionKey = new System.Windows.Forms.TextBox();
            this.grouperMessageText = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.txtMessageText = new System.Windows.Forms.TextBox();
            this.tabFilesPage = new System.Windows.Forms.TabPage();
            this.grouperMessageFiles = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.radioButtonBinaryFile = new System.Windows.Forms.RadioButton();
            this.radioButtonJsonTemplate = new System.Windows.Forms.RadioButton();
            this.radioButtonXmlTemplate = new System.Windows.Forms.RadioButton();
            this.radioButtonTextFile = new System.Windows.Forms.RadioButton();
            this.checkBoxFileName = new System.Windows.Forms.CheckBox();
            this.messageFileListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabGeneratorPage = new System.Windows.Forms.TabPage();
            this.grouperEventDataGenerator = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.lblRegistration = new System.Windows.Forms.Label();
            this.eventDataGeneratorPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.cboEventDataGeneratorType = new System.Windows.Forms.ComboBox();
            this.lblRegistrationType = new System.Windows.Forms.Label();
            this.grouperMessageProperties = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.propertiesDataGridView = new System.Windows.Forms.DataGridView();
            this.mainTabSenderPage = new System.Windows.Forms.TabPage();
            this.grouperSender = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.checkBoxSocketPerTask = new System.Windows.Forms.CheckBox();
            this.checkBoxNoPartitionKey = new System.Windows.Forms.CheckBox();
            this.txtSendBatchSize = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.checkBoxSendBatch = new System.Windows.Forms.CheckBox();
            this.lblSendBatchSize = new System.Windows.Forms.Label();
            this.cboSenderInspector = new System.Windows.Forms.ComboBox();
            this.lblSenderInspector = new System.Windows.Forms.Label();
            this.txtMessageCount = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.txtSendTaskCount = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.txtSenderThinkTime = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.NumericTextBox();
            this.lblSenderThinkTime = new System.Windows.Forms.Label();
            this.checkBoxSenderThinkTime = new System.Windows.Forms.CheckBox();
            this.checkBoxAddMessageNumber = new System.Windows.Forms.CheckBox();
            this.checkBoxSenderEnableGraph = new System.Windows.Forms.CheckBox();
            this.checkBoxSenderEnableStatistics = new System.Windows.Forms.CheckBox();
            this.checkBoxUpdatePartitionKey = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableSenderLogging = new System.Windows.Forms.CheckBox();
            this.checkBoxSenderVerboseLogging = new System.Windows.Forms.CheckBox();
            this.lblSendTaskCount = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.tabPageGraph = new System.Windows.Forms.TabPage();
            this.grouperSenderStatistics = new Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper();
            this.senderLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSenderAverageTime = new System.Windows.Forms.Label();
            this.lblSenderAverageCaption = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSenderLastTime = new System.Windows.Forms.Label();
            this.lblSenderLastCaption = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSenderMinimumTime = new System.Windows.Forms.Label();
            this.lblSenderMinimumCaption = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSenderMaximumTime = new System.Windows.Forms.Label();
            this.lblSenderMaximumCaption = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSenderMessagesPerSecond = new System.Windows.Forms.Label();
            this.lblSenderMessagesPerSecondCaption = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSenderMessageNumber = new System.Windows.Forms.Label();
            this.lblSenderCallsSuccessedCaption = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectFiles = new System.Windows.Forms.Button();
            this.btnClearFiles = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.mainTabMessagePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.messageTabControl.SuspendLayout();
            this.tabMessagePage.SuspendLayout();
            this.grouperPartitionKey.SuspendLayout();
            this.grouperMessageText.SuspendLayout();
            this.tabFilesPage.SuspendLayout();
            this.grouperMessageFiles.SuspendLayout();
            this.tabGeneratorPage.SuspendLayout();
            this.grouperEventDataGenerator.SuspendLayout();
            this.grouperMessageProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).BeginInit();
            this.mainTabSenderPage.SuspendLayout();
            this.grouperSender.SuspendLayout();
            this.tabPageGraph.SuspendLayout();
            this.grouperSenderStatistics.SuspendLayout();
            this.senderLayoutPanel.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
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
            this.btnStart.Location = new System.Drawing.Point(1120, 482);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(96, 30);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnStart.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(1227, 482);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.mainTabMessagePage);
            this.mainTabControl.Controls.Add(this.mainTabSenderPage);
            this.mainTabControl.Controls.Add(this.tabPageGraph);
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(21, 20);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1301, 453);
            this.mainTabControl.TabIndex = 2;
            this.mainTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mainTabControl_DrawItem);
            // 
            // mainTabMessagePage
            // 
            this.mainTabMessagePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.mainTabMessagePage.Controls.Add(this.splitContainer);
            this.mainTabMessagePage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mainTabMessagePage.Location = new System.Drawing.Point(4, 27);
            this.mainTabMessagePage.Margin = new System.Windows.Forms.Padding(4);
            this.mainTabMessagePage.Name = "mainTabMessagePage";
            this.mainTabMessagePage.Padding = new System.Windows.Forms.Padding(4);
            this.mainTabMessagePage.Size = new System.Drawing.Size(1293, 422);
            this.mainTabMessagePage.TabIndex = 0;
            this.mainTabMessagePage.Text = "Message";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(21, 10);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.messageTabControl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grouperMessageProperties);
            this.splitContainer.Size = new System.Drawing.Size(1248, 394);
            this.splitContainer.SplitterDistance = 612;
            this.splitContainer.SplitterWidth = 21;
            this.splitContainer.TabIndex = 1;
            // 
            // messageTabControl
            // 
            this.messageTabControl.Controls.Add(this.tabMessagePage);
            this.messageTabControl.Controls.Add(this.tabFilesPage);
            this.messageTabControl.Controls.Add(this.tabGeneratorPage);
            this.messageTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.messageTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageTabControl.Location = new System.Drawing.Point(0, 0);
            this.messageTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.messageTabControl.Name = "messageTabControl";
            this.messageTabControl.SelectedIndex = 0;
            this.messageTabControl.Size = new System.Drawing.Size(612, 394);
            this.messageTabControl.TabIndex = 10;
            this.messageTabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.messageTabControl_DrawItem);
            // 
            // tabMessagePage
            // 
            this.tabMessagePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabMessagePage.Controls.Add(this.grouperPartitionKey);
            this.tabMessagePage.Controls.Add(this.grouperMessageText);
            this.tabMessagePage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabMessagePage.Location = new System.Drawing.Point(4, 27);
            this.tabMessagePage.Margin = new System.Windows.Forms.Padding(4);
            this.tabMessagePage.Name = "tabMessagePage";
            this.tabMessagePage.Size = new System.Drawing.Size(604, 363);
            this.tabMessagePage.TabIndex = 2;
            this.tabMessagePage.Text = "Message";
            // 
            // grouperPartitionKey
            // 
            this.grouperPartitionKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperPartitionKey.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperPartitionKey.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperPartitionKey.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperPartitionKey.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPartitionKey.BorderThickness = 1F;
            this.grouperPartitionKey.Controls.Add(this.txtPartitionKey);
            this.grouperPartitionKey.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperPartitionKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperPartitionKey.ForeColor = System.Drawing.Color.White;
            this.grouperPartitionKey.GroupImage = null;
            this.grouperPartitionKey.GroupTitle = "Partition Key";
            this.grouperPartitionKey.Location = new System.Drawing.Point(21, 252);
            this.grouperPartitionKey.Margin = new System.Windows.Forms.Padding(4);
            this.grouperPartitionKey.Name = "grouperPartitionKey";
            this.grouperPartitionKey.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperPartitionKey.PaintGroupBox = true;
            this.grouperPartitionKey.RoundCorners = 4;
            this.grouperPartitionKey.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperPartitionKey.ShadowControl = false;
            this.grouperPartitionKey.ShadowThickness = 1;
            this.grouperPartitionKey.Size = new System.Drawing.Size(555, 86);
            this.grouperPartitionKey.TabIndex = 15;
            // 
            // txtPartitionKey
            // 
            this.txtPartitionKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPartitionKey.BackColor = System.Drawing.SystemColors.Window;
            this.txtPartitionKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPartitionKey.Location = new System.Drawing.Point(21, 39);
            this.txtPartitionKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtPartitionKey.Name = "txtPartitionKey";
            this.txtPartitionKey.Size = new System.Drawing.Size(511, 23);
            this.txtPartitionKey.TabIndex = 0;
            // 
            // grouperMessageText
            // 
            this.grouperMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperMessageText.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageText.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageText.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.BorderThickness = 1F;
            this.grouperMessageText.Controls.Add(this.txtMessageText);
            this.grouperMessageText.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageText.ForeColor = System.Drawing.Color.White;
            this.grouperMessageText.GroupImage = null;
            this.grouperMessageText.GroupTitle = "Message Text";
            this.grouperMessageText.Location = new System.Drawing.Point(21, 10);
            this.grouperMessageText.Margin = new System.Windows.Forms.Padding(4);
            this.grouperMessageText.Name = "grouperMessageText";
            this.grouperMessageText.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperMessageText.PaintGroupBox = true;
            this.grouperMessageText.RoundCorners = 4;
            this.grouperMessageText.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageText.ShadowControl = false;
            this.grouperMessageText.ShadowThickness = 1;
            this.grouperMessageText.Size = new System.Drawing.Size(555, 233);
            this.grouperMessageText.TabIndex = 14;
            // 
            // txtMessageText
            // 
            this.txtMessageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessageText.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessageText.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageText.Location = new System.Drawing.Point(21, 39);
            this.txtMessageText.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessageText.Multiline = true;
            this.txtMessageText.Name = "txtMessageText";
            this.txtMessageText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessageText.Size = new System.Drawing.Size(511, 173);
            this.txtMessageText.TabIndex = 0;
            this.txtMessageText.TextChanged += new System.EventHandler(this.txtMessageText_TextChanged);
            // 
            // tabFilesPage
            // 
            this.tabFilesPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabFilesPage.Controls.Add(this.grouperMessageFiles);
            this.tabFilesPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabFilesPage.Location = new System.Drawing.Point(4, 27);
            this.tabFilesPage.Margin = new System.Windows.Forms.Padding(4);
            this.tabFilesPage.Name = "tabFilesPage";
            this.tabFilesPage.Padding = new System.Windows.Forms.Padding(4);
            this.tabFilesPage.Size = new System.Drawing.Size(604, 363);
            this.tabFilesPage.TabIndex = 5;
            this.tabFilesPage.Text = "Files";
            // 
            // grouperMessageFiles
            // 
            this.grouperMessageFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperMessageFiles.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageFiles.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageFiles.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageFiles.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageFiles.BorderThickness = 1F;
            this.grouperMessageFiles.Controls.Add(this.radioButtonBinaryFile);
            this.grouperMessageFiles.Controls.Add(this.radioButtonJsonTemplate);
            this.grouperMessageFiles.Controls.Add(this.radioButtonXmlTemplate);
            this.grouperMessageFiles.Controls.Add(this.radioButtonTextFile);
            this.grouperMessageFiles.Controls.Add(this.checkBoxFileName);
            this.grouperMessageFiles.Controls.Add(this.messageFileListView);
            this.grouperMessageFiles.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageFiles.ForeColor = System.Drawing.Color.White;
            this.grouperMessageFiles.GroupImage = null;
            this.grouperMessageFiles.GroupTitle = "Message Files";
            this.grouperMessageFiles.Location = new System.Drawing.Point(21, 10);
            this.grouperMessageFiles.Margin = new System.Windows.Forms.Padding(4);
            this.grouperMessageFiles.Name = "grouperMessageFiles";
            this.grouperMessageFiles.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperMessageFiles.PaintGroupBox = true;
            this.grouperMessageFiles.RoundCorners = 4;
            this.grouperMessageFiles.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageFiles.ShadowControl = false;
            this.grouperMessageFiles.ShadowThickness = 1;
            this.grouperMessageFiles.Size = new System.Drawing.Size(555, 330);
            this.grouperMessageFiles.TabIndex = 16;
            this.grouperMessageFiles.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageFiles_CustomPaint);
            // 
            // radioButtonBinaryFile
            // 
            this.radioButtonBinaryFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonBinaryFile.AutoSize = true;
            this.radioButtonBinaryFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonBinaryFile.Location = new System.Drawing.Point(139, 292);
            this.radioButtonBinaryFile.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonBinaryFile.Name = "radioButtonBinaryFile";
            this.radioButtonBinaryFile.Size = new System.Drawing.Size(95, 21);
            this.radioButtonBinaryFile.TabIndex = 19;
            this.radioButtonBinaryFile.Text = "Binary File";
            this.radioButtonBinaryFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonJsonTemplate
            // 
            this.radioButtonJsonTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonJsonTemplate.AutoSize = true;
            this.radioButtonJsonTemplate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonJsonTemplate.Location = new System.Drawing.Point(256, 292);
            this.radioButtonJsonTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonJsonTemplate.Name = "radioButtonJsonTemplate";
            this.radioButtonJsonTemplate.Size = new System.Drawing.Size(122, 21);
            this.radioButtonJsonTemplate.TabIndex = 18;
            this.radioButtonJsonTemplate.TabStop = true;
            this.radioButtonJsonTemplate.Text = "Json Template";
            this.radioButtonJsonTemplate.UseVisualStyleBackColor = true;
            // 
            // radioButtonXmlTemplate
            // 
            this.radioButtonXmlTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonXmlTemplate.AutoSize = true;
            this.radioButtonXmlTemplate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonXmlTemplate.Location = new System.Drawing.Point(417, 292);
            this.radioButtonXmlTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonXmlTemplate.Name = "radioButtonXmlTemplate";
            this.radioButtonXmlTemplate.Size = new System.Drawing.Size(115, 21);
            this.radioButtonXmlTemplate.TabIndex = 17;
            this.radioButtonXmlTemplate.TabStop = true;
            this.radioButtonXmlTemplate.Text = "Xml Template";
            this.radioButtonXmlTemplate.UseVisualStyleBackColor = true;
            // 
            // radioButtonTextFile
            // 
            this.radioButtonTextFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonTextFile.AutoSize = true;
            this.radioButtonTextFile.Checked = true;
            this.radioButtonTextFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioButtonTextFile.Location = new System.Drawing.Point(23, 291);
            this.radioButtonTextFile.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonTextFile.Name = "radioButtonTextFile";
            this.radioButtonTextFile.Size = new System.Drawing.Size(82, 21);
            this.radioButtonTextFile.TabIndex = 16;
            this.radioButtonTextFile.TabStop = true;
            this.radioButtonTextFile.Text = "Text File";
            this.radioButtonTextFile.UseVisualStyleBackColor = true;
            // 
            // checkBoxFileName
            // 
            this.checkBoxFileName.AutoSize = true;
            this.checkBoxFileName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxFileName.Location = new System.Drawing.Point(29, 43);
            this.checkBoxFileName.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxFileName.Name = "checkBoxFileName";
            this.checkBoxFileName.Size = new System.Drawing.Size(67, 21);
            this.checkBoxFileName.TabIndex = 3;
            this.checkBoxFileName.Text = "Name";
            this.checkBoxFileName.UseVisualStyleBackColor = true;
            this.checkBoxFileName.CheckedChanged += new System.EventHandler(this.checkBoxFileName_CheckedChanged);
            // 
            // messageFileListView
            // 
            this.messageFileListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageFileListView.CheckBoxes = true;
            this.messageFileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.sizeColumnHeader});
            this.messageFileListView.FullRowSelect = true;
            this.messageFileListView.Location = new System.Drawing.Point(21, 39);
            this.messageFileListView.Margin = new System.Windows.Forms.Padding(4);
            this.messageFileListView.Name = "messageFileListView";
            this.messageFileListView.OwnerDraw = true;
            this.messageFileListView.Size = new System.Drawing.Size(511, 242);
            this.messageFileListView.TabIndex = 2;
            this.messageFileListView.UseCompatibleStateImageBehavior = false;
            this.messageFileListView.View = System.Windows.Forms.View.Details;
            this.messageFileListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.messageFileListView_DrawColumnHeader);
            this.messageFileListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.messageFileListView_DrawItem);
            this.messageFileListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.messageFileListView_DrawSubItem);
            this.messageFileListView.Resize += new System.EventHandler(this.messageFileListView_Resize);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 200;
            // 
            // sizeColumnHeader
            // 
            this.sizeColumnHeader.Text = "Size";
            this.sizeColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.sizeColumnHeader.Width = 70;
            // 
            // tabGeneratorPage
            // 
            this.tabGeneratorPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabGeneratorPage.Controls.Add(this.grouperEventDataGenerator);
            this.tabGeneratorPage.Location = new System.Drawing.Point(4, 27);
            this.tabGeneratorPage.Margin = new System.Windows.Forms.Padding(4);
            this.tabGeneratorPage.Name = "tabGeneratorPage";
            this.tabGeneratorPage.Size = new System.Drawing.Size(604, 363);
            this.tabGeneratorPage.TabIndex = 6;
            this.tabGeneratorPage.Text = "Generator";
            // 
            // grouperEventDataGenerator
            // 
            this.grouperEventDataGenerator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperEventDataGenerator.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperEventDataGenerator.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperEventDataGenerator.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperEventDataGenerator.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventDataGenerator.BorderThickness = 1F;
            this.grouperEventDataGenerator.Controls.Add(this.lblRegistration);
            this.grouperEventDataGenerator.Controls.Add(this.eventDataGeneratorPropertyGrid);
            this.grouperEventDataGenerator.Controls.Add(this.cboEventDataGeneratorType);
            this.grouperEventDataGenerator.Controls.Add(this.lblRegistrationType);
            this.grouperEventDataGenerator.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperEventDataGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperEventDataGenerator.ForeColor = System.Drawing.Color.White;
            this.grouperEventDataGenerator.GroupImage = null;
            this.grouperEventDataGenerator.GroupTitle = "Event Data Generator";
            this.grouperEventDataGenerator.Location = new System.Drawing.Point(21, 10);
            this.grouperEventDataGenerator.Margin = new System.Windows.Forms.Padding(4);
            this.grouperEventDataGenerator.Name = "grouperEventDataGenerator";
            this.grouperEventDataGenerator.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperEventDataGenerator.PaintGroupBox = true;
            this.grouperEventDataGenerator.RoundCorners = 4;
            this.grouperEventDataGenerator.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperEventDataGenerator.ShadowControl = false;
            this.grouperEventDataGenerator.ShadowThickness = 1;
            this.grouperEventDataGenerator.Size = new System.Drawing.Size(555, 321);
            this.grouperEventDataGenerator.TabIndex = 34;
            this.grouperEventDataGenerator.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperEventDataGenerator_CustomPaint);
            // 
            // lblRegistration
            // 
            this.lblRegistration.AutoSize = true;
            this.lblRegistration.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRegistration.Location = new System.Drawing.Point(21, 98);
            this.lblRegistration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegistration.Name = "lblRegistration";
            this.lblRegistration.Size = new System.Drawing.Size(77, 17);
            this.lblRegistration.TabIndex = 35;
            this.lblRegistration.Text = "Properties:";
            // 
            // eventDataGeneratorPropertyGrid
            // 
            this.eventDataGeneratorPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventDataGeneratorPropertyGrid.BackColor = System.Drawing.SystemColors.Window;
            this.eventDataGeneratorPropertyGrid.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.eventDataGeneratorPropertyGrid.HelpVisible = false;
            this.eventDataGeneratorPropertyGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.eventDataGeneratorPropertyGrid.Location = new System.Drawing.Point(21, 118);
            this.eventDataGeneratorPropertyGrid.Margin = new System.Windows.Forms.Padding(4);
            this.eventDataGeneratorPropertyGrid.Name = "eventDataGeneratorPropertyGrid";
            this.eventDataGeneratorPropertyGrid.Size = new System.Drawing.Size(512, 183);
            this.eventDataGeneratorPropertyGrid.TabIndex = 34;
            this.eventDataGeneratorPropertyGrid.ToolbarVisible = false;
            // 
            // cboEventDataGeneratorType
            // 
            this.cboEventDataGeneratorType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboEventDataGeneratorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEventDataGeneratorType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEventDataGeneratorType.FormattingEnabled = true;
            this.cboEventDataGeneratorType.Location = new System.Drawing.Point(21, 59);
            this.cboEventDataGeneratorType.Margin = new System.Windows.Forms.Padding(4);
            this.cboEventDataGeneratorType.Name = "cboEventDataGeneratorType";
            this.cboEventDataGeneratorType.Size = new System.Drawing.Size(511, 25);
            this.cboEventDataGeneratorType.TabIndex = 33;
            this.cboEventDataGeneratorType.SelectedIndexChanged += new System.EventHandler(this.cboEventDataGeneratorType_SelectedIndexChanged);
            // 
            // lblRegistrationType
            // 
            this.lblRegistrationType.AutoSize = true;
            this.lblRegistrationType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRegistrationType.Location = new System.Drawing.Point(21, 39);
            this.lblRegistrationType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegistrationType.Name = "lblRegistrationType";
            this.lblRegistrationType.Size = new System.Drawing.Size(187, 17);
            this.lblRegistrationType.TabIndex = 32;
            this.lblRegistrationType.Text = "Event Data Generator Type:";
            // 
            // grouperMessageProperties
            // 
            this.grouperMessageProperties.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperMessageProperties.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperMessageProperties.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperMessageProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.BorderThickness = 1F;
            this.grouperMessageProperties.Controls.Add(this.propertiesDataGridView);
            this.grouperMessageProperties.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperMessageProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperMessageProperties.ForeColor = System.Drawing.Color.White;
            this.grouperMessageProperties.GroupImage = null;
            this.grouperMessageProperties.GroupTitle = "Message Properties";
            this.grouperMessageProperties.Location = new System.Drawing.Point(0, 0);
            this.grouperMessageProperties.Margin = new System.Windows.Forms.Padding(4);
            this.grouperMessageProperties.Name = "grouperMessageProperties";
            this.grouperMessageProperties.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperMessageProperties.PaintGroupBox = true;
            this.grouperMessageProperties.RoundCorners = 4;
            this.grouperMessageProperties.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperMessageProperties.ShadowControl = false;
            this.grouperMessageProperties.ShadowThickness = 1;
            this.grouperMessageProperties.Size = new System.Drawing.Size(615, 394);
            this.grouperMessageProperties.TabIndex = 0;
            this.grouperMessageProperties.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperMessageProperties_CustomPaint);
            // 
            // propertiesDataGridView
            // 
            this.propertiesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.propertiesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.propertiesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertiesDataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.propertiesDataGridView.Location = new System.Drawing.Point(21, 39);
            this.propertiesDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.propertiesDataGridView.Name = "propertiesDataGridView";
            this.propertiesDataGridView.RowHeadersWidth = 24;
            this.propertiesDataGridView.Size = new System.Drawing.Size(572, 335);
            this.propertiesDataGridView.TabIndex = 0;
            this.propertiesDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.propertiesDataGridView_DataError);
            this.propertiesDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.propertiesDataGridView_RowsAdded);
            this.propertiesDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.propertiesDataGridView_RowsRemoved);
            this.propertiesDataGridView.Resize += new System.EventHandler(this.propertiesDataGridView_Resize);
            // 
            // mainTabSenderPage
            // 
            this.mainTabSenderPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.mainTabSenderPage.Controls.Add(this.grouperSender);
            this.mainTabSenderPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mainTabSenderPage.Location = new System.Drawing.Point(4, 27);
            this.mainTabSenderPage.Margin = new System.Windows.Forms.Padding(4);
            this.mainTabSenderPage.Name = "mainTabSenderPage";
            this.mainTabSenderPage.Padding = new System.Windows.Forms.Padding(4);
            this.mainTabSenderPage.Size = new System.Drawing.Size(1293, 422);
            this.mainTabSenderPage.TabIndex = 1;
            this.mainTabSenderPage.Text = "Sender";
            // 
            // grouperSender
            // 
            this.grouperSender.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouperSender.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSender.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSender.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperSender.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSender.BorderThickness = 1F;
            this.grouperSender.Controls.Add(this.checkBoxSocketPerTask);
            this.grouperSender.Controls.Add(this.checkBoxNoPartitionKey);
            this.grouperSender.Controls.Add(this.txtSendBatchSize);
            this.grouperSender.Controls.Add(this.checkBoxSendBatch);
            this.grouperSender.Controls.Add(this.lblSendBatchSize);
            this.grouperSender.Controls.Add(this.cboSenderInspector);
            this.grouperSender.Controls.Add(this.lblSenderInspector);
            this.grouperSender.Controls.Add(this.txtMessageCount);
            this.grouperSender.Controls.Add(this.txtSendTaskCount);
            this.grouperSender.Controls.Add(this.txtSenderThinkTime);
            this.grouperSender.Controls.Add(this.lblSenderThinkTime);
            this.grouperSender.Controls.Add(this.checkBoxSenderThinkTime);
            this.grouperSender.Controls.Add(this.checkBoxAddMessageNumber);
            this.grouperSender.Controls.Add(this.checkBoxSenderEnableGraph);
            this.grouperSender.Controls.Add(this.checkBoxSenderEnableStatistics);
            this.grouperSender.Controls.Add(this.checkBoxUpdatePartitionKey);
            this.grouperSender.Controls.Add(this.checkBoxEnableSenderLogging);
            this.grouperSender.Controls.Add(this.checkBoxSenderVerboseLogging);
            this.grouperSender.Controls.Add(this.lblSendTaskCount);
            this.grouperSender.Controls.Add(this.lblCount);
            this.grouperSender.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSender.ForeColor = System.Drawing.Color.White;
            this.grouperSender.GroupImage = null;
            this.grouperSender.GroupTitle = "Configuration";
            this.grouperSender.Location = new System.Drawing.Point(21, 10);
            this.grouperSender.Margin = new System.Windows.Forms.Padding(4);
            this.grouperSender.Name = "grouperSender";
            this.grouperSender.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperSender.PaintGroupBox = true;
            this.grouperSender.RoundCorners = 4;
            this.grouperSender.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSender.ShadowControl = false;
            this.grouperSender.ShadowThickness = 1;
            this.grouperSender.Size = new System.Drawing.Size(1248, 394);
            this.grouperSender.TabIndex = 1;
            this.grouperSender.CustomPaint += new System.Action<System.Windows.Forms.PaintEventArgs>(this.grouperSender_CustomPaint);
            // 
            // checkBoxSocketPerTask
            // 
            this.checkBoxSocketPerTask.AutoSize = true;
            this.checkBoxSocketPerTask.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSocketPerTask.Location = new System.Drawing.Point(524, 209);
            this.checkBoxSocketPerTask.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSocketPerTask.Name = "checkBoxSocketPerTask";
            this.checkBoxSocketPerTask.Size = new System.Drawing.Size(135, 21);
            this.checkBoxSocketPerTask.TabIndex = 19;
            this.checkBoxSocketPerTask.Text = "Socker Per Task";
            this.checkBoxSocketPerTask.UseVisualStyleBackColor = true;
            // 
            // checkBoxNoPartitionKey
            // 
            this.checkBoxNoPartitionKey.AutoSize = true;
            this.checkBoxNoPartitionKey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxNoPartitionKey.Location = new System.Drawing.Point(21, 286);
            this.checkBoxNoPartitionKey.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxNoPartitionKey.Name = "checkBoxNoPartitionKey";
            this.checkBoxNoPartitionKey.Size = new System.Drawing.Size(128, 21);
            this.checkBoxNoPartitionKey.TabIndex = 18;
            this.checkBoxNoPartitionKey.Text = "No PartitionKey";
            this.checkBoxNoPartitionKey.UseVisualStyleBackColor = true;
            this.checkBoxNoPartitionKey.CheckedChanged += new System.EventHandler(this.partitionCheckBox_CheckedChanged);
            // 
            // txtSendBatchSize
            // 
            this.txtSendBatchSize.AllowSpace = false;
            this.txtSendBatchSize.Location = new System.Drawing.Point(363, 128);
            this.txtSendBatchSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtSendBatchSize.Name = "txtSendBatchSize";
            this.txtSendBatchSize.Size = new System.Drawing.Size(137, 23);
            this.txtSendBatchSize.TabIndex = 6;
            // 
            // checkBoxSendBatch
            // 
            this.checkBoxSendBatch.AutoSize = true;
            this.checkBoxSendBatch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSendBatch.Location = new System.Drawing.Point(21, 128);
            this.checkBoxSendBatch.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSendBatch.Name = "checkBoxSendBatch";
            this.checkBoxSendBatch.Size = new System.Drawing.Size(103, 21);
            this.checkBoxSendBatch.TabIndex = 4;
            this.checkBoxSendBatch.Text = "Send Batch";
            this.checkBoxSendBatch.UseVisualStyleBackColor = true;
            this.checkBoxSendBatch.CheckedChanged += new System.EventHandler(this.checkBoxSendBatch_CheckedChanged);
            // 
            // lblSendBatchSize
            // 
            this.lblSendBatchSize.AutoSize = true;
            this.lblSendBatchSize.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSendBatchSize.Location = new System.Drawing.Point(224, 130);
            this.lblSendBatchSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSendBatchSize.Name = "lblSendBatchSize";
            this.lblSendBatchSize.Size = new System.Drawing.Size(79, 17);
            this.lblSendBatchSize.TabIndex = 5;
            this.lblSendBatchSize.Text = "Batch Size:";
            // 
            // cboSenderInspector
            // 
            this.cboSenderInspector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSenderInspector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSenderInspector.FormattingEnabled = true;
            this.cboSenderInspector.Location = new System.Drawing.Point(171, 325);
            this.cboSenderInspector.Margin = new System.Windows.Forms.Padding(4);
            this.cboSenderInspector.Name = "cboSenderInspector";
            this.cboSenderInspector.Size = new System.Drawing.Size(329, 25);
            this.cboSenderInspector.TabIndex = 17;
            // 
            // lblSenderInspector
            // 
            this.lblSenderInspector.AutoSize = true;
            this.lblSenderInspector.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSenderInspector.Location = new System.Drawing.Point(21, 325);
            this.lblSenderInspector.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderInspector.Name = "lblSenderInspector";
            this.lblSenderInspector.Size = new System.Drawing.Size(144, 17);
            this.lblSenderInspector.TabIndex = 16;
            this.lblSenderInspector.Text = "Event Data Inspector:";
            // 
            // txtMessageCount
            // 
            this.txtMessageCount.AllowSpace = false;
            this.txtMessageCount.Location = new System.Drawing.Point(363, 246);
            this.txtMessageCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessageCount.Name = "txtMessageCount";
            this.txtMessageCount.Size = new System.Drawing.Size(137, 23);
            this.txtMessageCount.TabIndex = 15;
            // 
            // txtSendTaskCount
            // 
            this.txtSendTaskCount.AllowSpace = false;
            this.txtSendTaskCount.Location = new System.Drawing.Point(363, 207);
            this.txtSendTaskCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtSendTaskCount.Name = "txtSendTaskCount";
            this.txtSendTaskCount.Size = new System.Drawing.Size(137, 23);
            this.txtSendTaskCount.TabIndex = 12;
            // 
            // txtSenderThinkTime
            // 
            this.txtSenderThinkTime.AllowSpace = false;
            this.txtSenderThinkTime.Enabled = false;
            this.txtSenderThinkTime.Location = new System.Drawing.Point(363, 167);
            this.txtSenderThinkTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtSenderThinkTime.Name = "txtSenderThinkTime";
            this.txtSenderThinkTime.Size = new System.Drawing.Size(137, 23);
            this.txtSenderThinkTime.TabIndex = 9;
            // 
            // lblSenderThinkTime
            // 
            this.lblSenderThinkTime.AutoSize = true;
            this.lblSenderThinkTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSenderThinkTime.Location = new System.Drawing.Point(224, 170);
            this.lblSenderThinkTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderThinkTime.Name = "lblSenderThinkTime";
            this.lblSenderThinkTime.Size = new System.Drawing.Size(90, 17);
            this.lblSenderThinkTime.TabIndex = 8;
            this.lblSenderThinkTime.Text = "Interval (ms):";
            // 
            // checkBoxSenderThinkTime
            // 
            this.checkBoxSenderThinkTime.AutoSize = true;
            this.checkBoxSenderThinkTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSenderThinkTime.Location = new System.Drawing.Point(21, 167);
            this.checkBoxSenderThinkTime.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSenderThinkTime.Name = "checkBoxSenderThinkTime";
            this.checkBoxSenderThinkTime.Size = new System.Drawing.Size(129, 21);
            this.checkBoxSenderThinkTime.TabIndex = 7;
            this.checkBoxSenderThinkTime.Text = "Use Think Time";
            this.checkBoxSenderThinkTime.UseVisualStyleBackColor = true;
            this.checkBoxSenderThinkTime.CheckedChanged += new System.EventHandler(this.checkBoxSenderThinkTime_CheckedChanged);
            // 
            // checkBoxAddMessageNumber
            // 
            this.checkBoxAddMessageNumber.AutoSize = true;
            this.checkBoxAddMessageNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxAddMessageNumber.Location = new System.Drawing.Point(20, 207);
            this.checkBoxAddMessageNumber.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAddMessageNumber.Name = "checkBoxAddMessageNumber";
            this.checkBoxAddMessageNumber.Size = new System.Drawing.Size(109, 21);
            this.checkBoxAddMessageNumber.TabIndex = 10;
            this.checkBoxAddMessageNumber.Text = "Add Number";
            this.checkBoxAddMessageNumber.UseVisualStyleBackColor = true;
            // 
            // checkBoxSenderEnableGraph
            // 
            this.checkBoxSenderEnableGraph.AutoSize = true;
            this.checkBoxSenderEnableGraph.Enabled = false;
            this.checkBoxSenderEnableGraph.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSenderEnableGraph.Location = new System.Drawing.Point(224, 89);
            this.checkBoxSenderEnableGraph.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSenderEnableGraph.Name = "checkBoxSenderEnableGraph";
            this.checkBoxSenderEnableGraph.Size = new System.Drawing.Size(118, 21);
            this.checkBoxSenderEnableGraph.TabIndex = 3;
            this.checkBoxSenderEnableGraph.Text = "Enable Graph";
            this.checkBoxSenderEnableGraph.UseVisualStyleBackColor = true;
            // 
            // checkBoxSenderEnableStatistics
            // 
            this.checkBoxSenderEnableStatistics.AutoSize = true;
            this.checkBoxSenderEnableStatistics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSenderEnableStatistics.Location = new System.Drawing.Point(21, 89);
            this.checkBoxSenderEnableStatistics.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSenderEnableStatistics.Name = "checkBoxSenderEnableStatistics";
            this.checkBoxSenderEnableStatistics.Size = new System.Drawing.Size(134, 21);
            this.checkBoxSenderEnableStatistics.TabIndex = 2;
            this.checkBoxSenderEnableStatistics.Text = "Enable Statistics";
            this.checkBoxSenderEnableStatistics.UseVisualStyleBackColor = true;
            this.checkBoxSenderEnableStatistics.CheckedChanged += new System.EventHandler(this.checkBoxSenderEnableStatistics_CheckedChanged);
            // 
            // checkBoxUpdatePartitionKey
            // 
            this.checkBoxUpdatePartitionKey.AutoSize = true;
            this.checkBoxUpdatePartitionKey.Checked = true;
            this.checkBoxUpdatePartitionKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpdatePartitionKey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxUpdatePartitionKey.Location = new System.Drawing.Point(21, 246);
            this.checkBoxUpdatePartitionKey.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxUpdatePartitionKey.Name = "checkBoxUpdatePartitionKey";
            this.checkBoxUpdatePartitionKey.Size = new System.Drawing.Size(156, 21);
            this.checkBoxUpdatePartitionKey.TabIndex = 13;
            this.checkBoxUpdatePartitionKey.Text = "Update PartitionKey";
            this.checkBoxUpdatePartitionKey.UseVisualStyleBackColor = true;
            this.checkBoxUpdatePartitionKey.CheckedChanged += new System.EventHandler(this.partitionCheckBox_CheckedChanged);
            // 
            // checkBoxEnableSenderLogging
            // 
            this.checkBoxEnableSenderLogging.AutoSize = true;
            this.checkBoxEnableSenderLogging.Checked = true;
            this.checkBoxEnableSenderLogging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableSenderLogging.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxEnableSenderLogging.Location = new System.Drawing.Point(21, 49);
            this.checkBoxEnableSenderLogging.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxEnableSenderLogging.Name = "checkBoxEnableSenderLogging";
            this.checkBoxEnableSenderLogging.Size = new System.Drawing.Size(129, 21);
            this.checkBoxEnableSenderLogging.TabIndex = 0;
            this.checkBoxEnableSenderLogging.Text = "Enable Logging";
            this.checkBoxEnableSenderLogging.UseVisualStyleBackColor = true;
            this.checkBoxEnableSenderLogging.CheckedChanged += new System.EventHandler(this.checkBoxEnableSenderLogging_CheckedChanged);
            // 
            // checkBoxSenderVerboseLogging
            // 
            this.checkBoxSenderVerboseLogging.AutoSize = true;
            this.checkBoxSenderVerboseLogging.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSenderVerboseLogging.Location = new System.Drawing.Point(224, 49);
            this.checkBoxSenderVerboseLogging.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSenderVerboseLogging.Name = "checkBoxSenderVerboseLogging";
            this.checkBoxSenderVerboseLogging.Size = new System.Drawing.Size(131, 21);
            this.checkBoxSenderVerboseLogging.TabIndex = 1;
            this.checkBoxSenderVerboseLogging.Text = "Enable Verbose";
            this.checkBoxSenderVerboseLogging.UseVisualStyleBackColor = true;
            // 
            // lblSendTaskCount
            // 
            this.lblSendTaskCount.AutoSize = true;
            this.lblSendTaskCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSendTaskCount.Location = new System.Drawing.Point(224, 209);
            this.lblSendTaskCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSendTaskCount.Name = "lblSendTaskCount";
            this.lblSendTaskCount.Size = new System.Drawing.Size(84, 17);
            this.lblSendTaskCount.TabIndex = 11;
            this.lblSendTaskCount.Text = "Task Count:";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCount.Location = new System.Drawing.Point(224, 249);
            this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(123, 17);
            this.lblCount.TabIndex = 14;
            this.lblCount.Text = "Event Data Count:";
            // 
            // tabPageGraph
            // 
            this.tabPageGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabPageGraph.Controls.Add(this.grouperSenderStatistics);
            this.tabPageGraph.Controls.Add(this.chart);
            this.tabPageGraph.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageGraph.Location = new System.Drawing.Point(4, 27);
            this.tabPageGraph.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageGraph.Name = "tabPageGraph";
            this.tabPageGraph.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageGraph.Size = new System.Drawing.Size(1293, 422);
            this.tabPageGraph.TabIndex = 3;
            this.tabPageGraph.Text = "Graph";
            // 
            // grouperSenderStatistics
            // 
            this.grouperSenderStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouperSenderStatistics.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.grouperSenderStatistics.BackgroundGradientColor = System.Drawing.Color.White;
            this.grouperSenderStatistics.BackgroundGradientMode = Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Grouper.GroupBoxGradientMode.None;
            this.grouperSenderStatistics.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSenderStatistics.BorderThickness = 1F;
            this.grouperSenderStatistics.Controls.Add(this.senderLayoutPanel);
            this.grouperSenderStatistics.CustomGroupBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.grouperSenderStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grouperSenderStatistics.ForeColor = System.Drawing.Color.White;
            this.grouperSenderStatistics.GroupImage = null;
            this.grouperSenderStatistics.GroupTitle = "Sender";
            this.grouperSenderStatistics.Location = new System.Drawing.Point(21, 10);
            this.grouperSenderStatistics.Margin = new System.Windows.Forms.Padding(4);
            this.grouperSenderStatistics.Name = "grouperSenderStatistics";
            this.grouperSenderStatistics.Padding = new System.Windows.Forms.Padding(27, 25, 27, 25);
            this.grouperSenderStatistics.PaintGroupBox = true;
            this.grouperSenderStatistics.RoundCorners = 4;
            this.grouperSenderStatistics.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouperSenderStatistics.ShadowControl = false;
            this.grouperSenderStatistics.ShadowThickness = 1;
            this.grouperSenderStatistics.Size = new System.Drawing.Size(171, 394);
            this.grouperSenderStatistics.TabIndex = 125;
            // 
            // senderLayoutPanel
            // 
            this.senderLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.senderLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.senderLayoutPanel.ColumnCount = 1;
            this.senderLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.senderLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.senderLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.senderLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.senderLayoutPanel.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.senderLayoutPanel.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.senderLayoutPanel.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.senderLayoutPanel.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.senderLayoutPanel.Controls.Add(this.tableLayoutPanel8, 0, 4);
            this.senderLayoutPanel.Controls.Add(this.tableLayoutPanel10, 0, 5);
            this.senderLayoutPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.senderLayoutPanel.Location = new System.Drawing.Point(21, 39);
            this.senderLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.senderLayoutPanel.Name = "senderLayoutPanel";
            this.senderLayoutPanel.RowCount = 6;
            this.senderLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.senderLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.senderLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.senderLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.senderLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.senderLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.senderLayoutPanel.Size = new System.Drawing.Size(128, 335);
            this.senderLayoutPanel.TabIndex = 121;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel7.Controls.Add(this.lblSenderAverageTime, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.lblSenderAverageCaption, 0, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(4, 59);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(120, 47);
            this.tableLayoutPanel7.TabIndex = 5;
            // 
            // lblSenderAverageTime
            // 
            this.lblSenderAverageTime.BackColor = System.Drawing.Color.White;
            this.lblSenderAverageTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSenderAverageTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSenderAverageTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderAverageTime.Location = new System.Drawing.Point(4, 23);
            this.lblSenderAverageTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderAverageTime.Name = "lblSenderAverageTime";
            this.lblSenderAverageTime.Size = new System.Drawing.Size(112, 16);
            this.lblSenderAverageTime.TabIndex = 117;
            this.lblSenderAverageTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSenderAverageCaption
            // 
            this.lblSenderAverageCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSenderAverageCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderAverageCaption.Location = new System.Drawing.Point(4, 0);
            this.lblSenderAverageCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderAverageCaption.Name = "lblSenderAverageCaption";
            this.lblSenderAverageCaption.Size = new System.Drawing.Size(112, 23);
            this.lblSenderAverageCaption.TabIndex = 116;
            this.lblSenderAverageCaption.Text = "Average Time";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Controls.Add(this.lblSenderLastTime, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblSenderLastCaption, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(120, 47);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblSenderLastTime
            // 
            this.lblSenderLastTime.BackColor = System.Drawing.Color.White;
            this.lblSenderLastTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSenderLastTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSenderLastTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderLastTime.Location = new System.Drawing.Point(4, 23);
            this.lblSenderLastTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderLastTime.Name = "lblSenderLastTime";
            this.lblSenderLastTime.Size = new System.Drawing.Size(112, 16);
            this.lblSenderLastTime.TabIndex = 113;
            this.lblSenderLastTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSenderLastCaption
            // 
            this.lblSenderLastCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSenderLastCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderLastCaption.Location = new System.Drawing.Point(4, 0);
            this.lblSenderLastCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderLastCaption.Name = "lblSenderLastCaption";
            this.lblSenderLastCaption.Size = new System.Drawing.Size(112, 23);
            this.lblSenderLastCaption.TabIndex = 112;
            this.lblSenderLastCaption.Text = "Last Time";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel4.Controls.Add(this.lblSenderMinimumTime, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblSenderMinimumCaption, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 114);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(120, 47);
            this.tableLayoutPanel4.TabIndex = 9;
            // 
            // lblSenderMinimumTime
            // 
            this.lblSenderMinimumTime.BackColor = System.Drawing.Color.White;
            this.lblSenderMinimumTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSenderMinimumTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSenderMinimumTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderMinimumTime.Location = new System.Drawing.Point(4, 23);
            this.lblSenderMinimumTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderMinimumTime.Name = "lblSenderMinimumTime";
            this.lblSenderMinimumTime.Size = new System.Drawing.Size(112, 16);
            this.lblSenderMinimumTime.TabIndex = 118;
            this.lblSenderMinimumTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSenderMinimumCaption
            // 
            this.lblSenderMinimumCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSenderMinimumCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderMinimumCaption.Location = new System.Drawing.Point(4, 0);
            this.lblSenderMinimumCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderMinimumCaption.Name = "lblSenderMinimumCaption";
            this.lblSenderMinimumCaption.Size = new System.Drawing.Size(112, 23);
            this.lblSenderMinimumCaption.TabIndex = 117;
            this.lblSenderMinimumCaption.Text = "Minimum Time";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel5.Controls.Add(this.lblSenderMaximumTime, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblSenderMaximumCaption, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(4, 169);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(120, 47);
            this.tableLayoutPanel5.TabIndex = 10;
            // 
            // lblSenderMaximumTime
            // 
            this.lblSenderMaximumTime.BackColor = System.Drawing.Color.White;
            this.lblSenderMaximumTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSenderMaximumTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSenderMaximumTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderMaximumTime.Location = new System.Drawing.Point(4, 23);
            this.lblSenderMaximumTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderMaximumTime.Name = "lblSenderMaximumTime";
            this.lblSenderMaximumTime.Size = new System.Drawing.Size(112, 16);
            this.lblSenderMaximumTime.TabIndex = 126;
            this.lblSenderMaximumTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSenderMaximumCaption
            // 
            this.lblSenderMaximumCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSenderMaximumCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderMaximumCaption.Location = new System.Drawing.Point(4, 0);
            this.lblSenderMaximumCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderMaximumCaption.Name = "lblSenderMaximumCaption";
            this.lblSenderMaximumCaption.Size = new System.Drawing.Size(112, 23);
            this.lblSenderMaximumCaption.TabIndex = 125;
            this.lblSenderMaximumCaption.Text = "Maximum Time";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel8.Controls.Add(this.lblSenderMessagesPerSecond, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.lblSenderMessagesPerSecondCaption, 0, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(4, 224);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(120, 47);
            this.tableLayoutPanel8.TabIndex = 11;
            // 
            // lblSenderMessagesPerSecond
            // 
            this.lblSenderMessagesPerSecond.BackColor = System.Drawing.Color.White;
            this.lblSenderMessagesPerSecond.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSenderMessagesPerSecond.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSenderMessagesPerSecond.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderMessagesPerSecond.Location = new System.Drawing.Point(4, 23);
            this.lblSenderMessagesPerSecond.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderMessagesPerSecond.Name = "lblSenderMessagesPerSecond";
            this.lblSenderMessagesPerSecond.Size = new System.Drawing.Size(112, 16);
            this.lblSenderMessagesPerSecond.TabIndex = 141;
            this.lblSenderMessagesPerSecond.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSenderMessagesPerSecondCaption
            // 
            this.lblSenderMessagesPerSecondCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSenderMessagesPerSecondCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderMessagesPerSecondCaption.Location = new System.Drawing.Point(4, 0);
            this.lblSenderMessagesPerSecondCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderMessagesPerSecondCaption.Name = "lblSenderMessagesPerSecondCaption";
            this.lblSenderMessagesPerSecondCaption.Size = new System.Drawing.Size(112, 23);
            this.lblSenderMessagesPerSecondCaption.TabIndex = 140;
            this.lblSenderMessagesPerSecondCaption.Text = "Messages/Sec";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel10.Controls.Add(this.lblSenderMessageNumber, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.lblSenderCallsSuccessedCaption, 0, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(4, 279);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(120, 52);
            this.tableLayoutPanel10.TabIndex = 8;
            // 
            // lblSenderMessageNumber
            // 
            this.lblSenderMessageNumber.BackColor = System.Drawing.Color.White;
            this.lblSenderMessageNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSenderMessageNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSenderMessageNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderMessageNumber.Location = new System.Drawing.Point(4, 26);
            this.lblSenderMessageNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderMessageNumber.Name = "lblSenderMessageNumber";
            this.lblSenderMessageNumber.Size = new System.Drawing.Size(112, 16);
            this.lblSenderMessageNumber.TabIndex = 125;
            this.lblSenderMessageNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSenderCallsSuccessedCaption
            // 
            this.lblSenderCallsSuccessedCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSenderCallsSuccessedCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderCallsSuccessedCaption.Location = new System.Drawing.Point(4, 0);
            this.lblSenderCallsSuccessedCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSenderCallsSuccessedCaption.Name = "lblSenderCallsSuccessedCaption";
            this.lblSenderCallsSuccessedCaption.Size = new System.Drawing.Size(112, 26);
            this.lblSenderCallsSuccessedCaption.TabIndex = 124;
            this.lblSenderCallsSuccessedCaption.Text = "Messages Total";
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
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F);
            legend1.IsTextAutoFit = false;
            legend1.MaximumAutoSize = 5F;
            legend1.Name = "Default";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(203, 20);
            this.chart.Margin = new System.Windows.Forms.Padding(4);
            this.chart.Name = "chart";
            series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series1.BorderWidth = 2;
            series1.ChartArea = "Default";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Default";
            series1.LegendText = "Sender Latency";
            series1.Name = "SenderLatency";
            series2.BorderWidth = 2;
            series2.ChartArea = "Default";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Default";
            series2.LegendText = "Sender Throughput";
            series2.Name = "SenderThroughput";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(1067, 384);
            this.chart.TabIndex = 126;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title";
            title1.ShadowColor = System.Drawing.Color.Transparent;
            title1.ShadowOffset = 1;
            title1.Text = "Sender Performance Counters";
            this.chart.Titles.Add(title1);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenFile.Location = new System.Drawing.Point(1013, 482);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(96, 30);
            this.btnOpenFile.TabIndex = 2;
            this.btnOpenFile.Text = "Open";
            this.btnOpenFile.UseVisualStyleBackColor = false;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            this.btnOpenFile.MouseEnter += new System.EventHandler(this.button_MouseEnter);
            this.btnOpenFile.MouseLeave += new System.EventHandler(this.button_MouseLeave);
            // 
            // btnSelectFiles
            // 
            this.btnSelectFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnSelectFiles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSelectFiles.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSelectFiles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnSelectFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFiles.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSelectFiles.Location = new System.Drawing.Point(800, 482);
            this.btnSelectFiles.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectFiles.Name = "btnSelectFiles";
            this.btnSelectFiles.Size = new System.Drawing.Size(96, 30);
            this.btnSelectFiles.TabIndex = 0;
            this.btnSelectFiles.Text = "Select Files";
            this.btnSelectFiles.UseVisualStyleBackColor = false;
            this.btnSelectFiles.Click += new System.EventHandler(this.btnSelectFiles_Click);
            // 
            // btnClearFiles
            // 
            this.btnClearFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.btnClearFiles.Enabled = false;
            this.btnClearFiles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearFiles.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearFiles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this.btnClearFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFiles.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearFiles.Location = new System.Drawing.Point(905, 482);
            this.btnClearFiles.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearFiles.Name = "btnClearFiles";
            this.btnClearFiles.Size = new System.Drawing.Size(96, 30);
            this.btnClearFiles.TabIndex = 1;
            this.btnClearFiles.Text = "Clear Files";
            this.btnClearFiles.UseVisualStyleBackColor = false;
            this.btnClearFiles.Click += new System.EventHandler(this.btnClearFiles_Click);
            // 
            // TestEventHubControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.btnClearFiles);
            this.Controls.Add(this.btnSelectFiles);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TestEventHubControl";
            this.Size = new System.Drawing.Size(1344, 532);
            this.mainTabControl.ResumeLayout(false);
            this.mainTabMessagePage.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.messageTabControl.ResumeLayout(false);
            this.tabMessagePage.ResumeLayout(false);
            this.grouperPartitionKey.ResumeLayout(false);
            this.grouperPartitionKey.PerformLayout();
            this.grouperMessageText.ResumeLayout(false);
            this.grouperMessageText.PerformLayout();
            this.tabFilesPage.ResumeLayout(false);
            this.grouperMessageFiles.ResumeLayout(false);
            this.grouperMessageFiles.PerformLayout();
            this.tabGeneratorPage.ResumeLayout(false);
            this.grouperEventDataGenerator.ResumeLayout(false);
            this.grouperEventDataGenerator.PerformLayout();
            this.grouperMessageProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertiesDataGridView)).EndInit();
            this.mainTabSenderPage.ResumeLayout(false);
            this.grouperSender.ResumeLayout(false);
            this.grouperSender.PerformLayout();
            this.tabPageGraph.ResumeLayout(false);
            this.grouperSenderStatistics.ResumeLayout(false);
            this.senderLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage mainTabMessagePage;
        private System.Windows.Forms.TabPage mainTabSenderPage;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        internal System.Windows.Forms.TabControl mainTabControl;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.DataVisualization.Charting.Chart chart;
        internal System.Windows.Forms.TabPage tabPageGraph;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Grouper grouperMessageProperties;
        private System.Windows.Forms.DataGridView propertiesDataGridView;
        private Grouper grouperSenderStatistics;
        private System.Windows.Forms.TableLayoutPanel senderLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label lblSenderAverageTime;
        private System.Windows.Forms.Label lblSenderAverageCaption;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblSenderLastTime;
        private System.Windows.Forms.Label lblSenderLastCaption;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblSenderMinimumTime;
        private System.Windows.Forms.Label lblSenderMinimumCaption;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lblSenderMaximumTime;
        private System.Windows.Forms.Label lblSenderMaximumCaption;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label lblSenderMessagesPerSecond;
        private System.Windows.Forms.Label lblSenderMessagesPerSecondCaption;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label lblSenderMessageNumber;
        private System.Windows.Forms.Label lblSenderCallsSuccessedCaption;
        private Grouper grouperSender;
        private System.Windows.Forms.Label lblSendTaskCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.CheckBox checkBoxEnableSenderLogging;
        private System.Windows.Forms.CheckBox checkBoxSenderVerboseLogging;
        private System.Windows.Forms.CheckBox checkBoxSenderEnableGraph;
        private System.Windows.Forms.CheckBox checkBoxSenderEnableStatistics;
        private System.Windows.Forms.CheckBox checkBoxUpdatePartitionKey;
        private System.Windows.Forms.CheckBox checkBoxAddMessageNumber;
        private System.Windows.Forms.Label lblSenderThinkTime;
        private System.Windows.Forms.CheckBox checkBoxSenderThinkTime;
        internal System.Windows.Forms.TabControl messageTabControl;
        private System.Windows.Forms.TabPage tabMessagePage;
        private System.Windows.Forms.TabPage tabFilesPage;
        private Grouper grouperMessageText;
        private System.Windows.Forms.TextBox txtMessageText;
        private System.Windows.Forms.Button btnSelectFiles;
        private Grouper grouperMessageFiles;
        private System.Windows.Forms.ListView messageFileListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader sizeColumnHeader;
        private System.Windows.Forms.Button btnClearFiles;
        private System.Windows.Forms.CheckBox checkBoxFileName;
        private Grouper grouperPartitionKey;
        private System.Windows.Forms.TextBox txtPartitionKey;
        private System.Windows.Forms.TabPage tabGeneratorPage;
        private Grouper grouperEventDataGenerator;
        private System.Windows.Forms.Label lblRegistration;
        private System.Windows.Forms.PropertyGrid eventDataGeneratorPropertyGrid;
        private System.Windows.Forms.ComboBox cboEventDataGeneratorType;
        private System.Windows.Forms.Label lblRegistrationType;
        private NumericTextBox txtMessageCount;
        private NumericTextBox txtSendTaskCount;
        private NumericTextBox txtSenderThinkTime;
        private System.Windows.Forms.RadioButton radioButtonBinaryFile;
        private System.Windows.Forms.RadioButton radioButtonJsonTemplate;
        private System.Windows.Forms.RadioButton radioButtonXmlTemplate;
        private System.Windows.Forms.RadioButton radioButtonTextFile;
        private System.Windows.Forms.ComboBox cboSenderInspector;
        private System.Windows.Forms.Label lblSenderInspector;
        private NumericTextBox txtSendBatchSize;
        private System.Windows.Forms.CheckBox checkBoxSendBatch;
        private System.Windows.Forms.Label lblSendBatchSize;
        private System.Windows.Forms.CheckBox checkBoxNoPartitionKey;
        private System.Windows.Forms.CheckBox checkBoxSocketPerTask;
    }
}
