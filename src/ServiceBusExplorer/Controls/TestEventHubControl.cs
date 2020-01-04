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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Cursor = System.Windows.Forms.Cursor;

#endregion

namespace ServiceBusExplorer.Controls
{
    using Enums;
    using ServiceBusExplorer.UIHelpers;
    using ServiceBusExplorer.Utilities.Helpers;
    using static ServiceBusExplorer.ServiceBusHelper;

    public partial class TestEventHubControl : UserControl
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string LabelFormat = "{0:0.000}";

        //***************************
        // Properties & Types
        //***************************
        private const string PropertyKey = "Key";
        private const string PropertyType = "Type";
        private const string PropertyValue = "Value";
        private const string DefaultMessageCount = "1";
        private const string DefaulSendBatchSize = "10";
        private const string DefaultSenderTaskCount = "1";
        private const string StartCaption = "Start";
        private const string StopCaption = "Stop";

        //***************************
        // Messages
        //***************************
        private const string MessageCannotBeNull = "The Message field cannot be null.";
        private const string DefaultMessageText = "Hi mate, how are you?";
        private const string MessageCountMustBeANumber = "The Message Count field must be an integer number greater or equal to zero.";
        private const string SendTaskCountMustBeANumber = "The Sender Task Count field must be an integer number greater than zero.";
        private const string SenderBatchSizeMustBeANumber = "The Sender Batch Size field must be an integer number greater than zero.";
        private const string SenderThinkTimeMustBeANumber = "The Sender Think Time field must be an integer number greater than zero.";
        private const string NoMessageSelected = "No message to send has been selected.";
        private const string SelectEventDataGenerator = "Select an EventData generator...";
        private const string InvalidJsonTemplate = "{0} is an invalid JSON template. The file will be used as text message rather than a template.";
        private const string InvalidXmlTemplate = "{0} is an invalid XML template. The file will be used as text message rather than a template.";
        private const string SelectEventDataInspector = "Select an EventData inspector...";
        private const string ConnectionStringCannotBeNull = "The namespace connection string cannot be null.";

        //***************************
        // Tooltips
        //***************************
        private const string MessageTextTooltip = "Gets or sets the message text.";
        private const string PartitionKeyTooltip = "Gets or sets the partition key.";
        private const string MessagePropertiesTooltip = "Gets or sets the properties of the message.";
        private const string MessageCountTooltip = "Gets or sets the number of messages to send.";
        private const string SendTaskCountTooltip = "Gets or sets the count of message senders.";
        private const string UpdateMessageIdTooltip = "Gets or sets a boolean value indicating whether the id of the message must be updated when sending multiple messages.";
        private const string EnableSenderLoggingTooltip = "Enable logging of message content and properties for message senders.";
        private const string EnableSenderVerboseLoggingTooltip = "Enable verbose logging for message senders.";
        private const string EnablePartitionKeyUpdateTooltip = "Enable automatic PartitionKey update. The tool generates a new GUID at each call. Note: the value of the checkbox is ignored and the PartitionKey is never updated when using JSON or XML templates or an EventData generator.";
        
        //***************************
        // Tab Pages
        //***************************
        private const int MessageTabPage = 0;
        private const int FilesTabPage = 1;

        //***************************
        // ListView Column Indexes
        //***************************
        private const int NameListViewColumnIndex = 0;
        private const int SizeListViewColumnIndex = 1;
        #endregion

        #region Private Instance Fields
        private readonly EventHubDescription eventHubDescription;
        private readonly PartitionDescription partitionDescription;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly MainForm mainForm;
        private readonly WriteToLogDelegate writeToLog;
        private readonly Func<Task> stopLog;
        private readonly Action startLog;
        private readonly BindingSource bindingSource = new BindingSource();
        private List<EventHubClient> eventHubClientCollection = new List<EventHubClient>();
        private CancellationTokenSource senderCancellationTokenSource;
        private CancellationTokenSource managerCancellationTokenSource;
        private CancellationTokenSource graphCancellationTokenSource;
        private ManualResetEventSlim managerResetEvent;
        private long eventDataCount = 1;
        private int senderBatchSize = 1;
        private int senderThinkTime;
        private long currentIndex;
        private long senderMessageNumber;
        private double senderMessagesPerSecond;
        private double senderMinimumTime;
        private double senderMaximumTime;
        private double senderAverageTime;
        private double senderTotalTime;
        private int actionCount;
        private int senderTaskCount = 1;
        private bool isSenderFaulted;
        private BlockingCollection<Tuple<long, long, DirectionType>> blockingCollection;
        private IEventDataGenerator senderEventDataGenerator;
        private IEventDataInspector senderEventDataInspector;
        
        #endregion

        #region Private Static Fields
        private static readonly List<string> types = new List<string> { "Boolean", "Byte", "Int16", "Int32", "Int64", "Single", "Double", "Decimal", "Guid", "DateTime", "String" };
        #endregion

        #region Public Constructors
        public TestEventHubControl(MainForm mainForm,
                                   WriteToLogDelegate writeToLog,
                                   Func<Task> stopLog,
                                   Action startLog,
                                   ServiceBusHelper serviceBusHelper,
                                   EventHubDescription eventHubDescription,
                                   PartitionDescription partitionDescription)
        {
            this.mainForm = mainForm;
            this.writeToLog = writeToLog;
            this.stopLog = stopLog;
            this.startLog = startLog;
            this.serviceBusHelper = serviceBusHelper;
            this.eventHubDescription = eventHubDescription;
            this.partitionDescription = partitionDescription;
            InitializeComponent();
            InitializeControls();
        }
        #endregion

        #region Public Events
        public event Action OnCancel;
        #endregion

        #region Private Methods
        private void InitializeControls()
        {
            try
            {
                // Get Event Data Generator and Inspector classes
                cboSenderInspector.Items.Add(SelectEventDataInspector);
                cboSenderInspector.SelectedIndex = 0;
                cboEventDataGeneratorType.Items.Add(SelectEventDataGenerator);
                cboEventDataGeneratorType.SelectedIndex = 0;
                cboMessageFormat.Items.AddRange(new[] { "Text", "JSON", "XML" });
                cboMessageFormat.SelectedIndex = 0;

                grouperMessageProperties.Size = new Size(splitContainer.Panel2.Width, grouperMessageProperties.Size.Height);

                if (serviceBusHelper != null)
                {
                    if (serviceBusHelper.EventDataInspectors != null)
                    {
                        foreach (var key in serviceBusHelper.EventDataInspectors.Keys)
                        {
                            cboSenderInspector.Items.Add(key);
                        }
                    }

                    if (serviceBusHelper.EventDataGenerators != null)
                    {
                        foreach (var key in serviceBusHelper.EventDataGenerators.Keys)
                        {
                            cboEventDataGeneratorType.Items.Add(key);
                        }
                    }
                }
                
                // Set chart layout
                SetGraphLayout();

                // Populate filenames listview control
                if (mainForm.FileNames.Any())
                {
                    foreach (var tuple in mainForm.FileNames)
                    {
                        messageFileListView.Items.Add(new ListViewItem(new[]
                                                        {
                                                            tuple.Item1, 
                                                            tuple.Item2
                                                        }));
                    }
                    btnClearFiles.Enabled = messageFileListView.Items.Count > 0;
                }

                // Set Think Time
                txtSenderThinkTime.Text = mainForm.SenderThinkTime.ToString(CultureInfo.InvariantCulture);
                senderThinkTime = mainForm.SenderThinkTime;

                // Set Binding Source
                bindingSource.DataSource = MessagePropertyInfo.Properties;

                // Initialize the DataGridView.
                propertiesDataGridView.AutoGenerateColumns = false;
                propertiesDataGridView.AutoSize = true;
                propertiesDataGridView.DataSource = bindingSource;
                propertiesDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                var textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = PropertyKey,
                    Name = PropertyKey,
                    Width = 138
                };
                propertiesDataGridView.Columns.Add(textBoxColumn);

                // Create the Type column
                var comboBoxColumn = new DataGridViewComboBoxColumn
                {
                    DataSource = types,
                    DataPropertyName = PropertyType,
                    Name = PropertyType,
                    Width = 90,
                    FlatStyle = FlatStyle.Flat
                };
                propertiesDataGridView.Columns.Add(comboBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = PropertyValue,
                    Name = PropertyValue,
                    Width = 138
                };
                propertiesDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                propertiesDataGridView.EnableHeadersVisualStyles = false;

                // Set the selection background color for all the cells.
                propertiesDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                propertiesDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                propertiesDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                propertiesDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                propertiesDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //propertiesDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //propertiesDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                propertiesDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                propertiesDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                propertiesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                propertiesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                LanguageDetector.SetFormattedMessage(serviceBusHelper,
                                                     mainForm != null && 
                                                     !string.IsNullOrWhiteSpace(mainForm.MessageText) ?
                                                     mainForm.MessageText :
                                                     DefaultMessageText, 
                                                     txtMessageText);

                txtPartitionKey.Text = Guid.NewGuid().ToString();
                txtMessageCount.Text = DefaultMessageCount;
                txtSendTaskCount.Text = DefaultSenderTaskCount;
                txtSendBatchSize.Text = DefaulSendBatchSize;
                txtSendBatchSize.Enabled = false;

                // Create Tooltips for controls
                toolTip.SetToolTip(txtMessageText, MessageTextTooltip);
                toolTip.SetToolTip(propertiesDataGridView, MessagePropertiesTooltip);
                toolTip.SetToolTip(txtPartitionKey, PartitionKeyTooltip);
                toolTip.SetToolTip(txtMessageCount, MessageCountTooltip);
                toolTip.SetToolTip(txtSendTaskCount, SendTaskCountTooltip);
                toolTip.SetToolTip(checkBoxUpdatePartitionKey, UpdateMessageIdTooltip);
                toolTip.SetToolTip(checkBoxEnableSenderLogging, EnableSenderLoggingTooltip);
                toolTip.SetToolTip(checkBoxSenderVerboseLogging, EnableSenderVerboseLoggingTooltip);
                toolTip.SetToolTip(checkBoxUpdatePartitionKey, EnablePartitionKeyUpdateTooltip);

                splitContainer.SplitterWidth = 16;
                splitContainer.SplitterDistance = (splitContainer.Size.Width - splitContainer.SplitterWidth)/2;
                propertiesDataGridView.Size = txtMessageText.Size;

                // Send to Partition
                if (partitionDescription == null)
                {
                    return;
                }
                checkBoxUpdatePartitionKey.Visible = false;
                checkBoxNoPartitionKey.Visible = false;
                lblSenderInspector.Location = new Point(lblSenderInspector.Location.X, lblSenderInspector.Location.Y - 32);
                cboSenderInspector.Location = new Point(cboSenderInspector.Location.X, cboSenderInspector.Location.Y - 32);
                grouperPartitionKey.Visible = false;
                grouperMessageText.Size = new Size(tabMessagePage.Size.Width - 32, tabMessagePage.Size.Height - 32);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private bool ValidateParameters()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMessageText.Text))
                {
                    writeToLog(MessageCannotBeNull);
                    return false;
                }
                if (!int.TryParse(txtMessageCount.Text, out var temp) || temp < 0)
                {
                    writeToLog(MessageCountMustBeANumber);
                    return false;
                }
                eventDataCount = temp;
                if (!int.TryParse(txtSendBatchSize.Text, out temp) || temp <= 0)
                {
                    writeToLog(SenderBatchSizeMustBeANumber);
                    return false;
                }
                senderBatchSize = temp;
                if (!int.TryParse(txtSenderThinkTime.Text, out temp) || temp <= 0)
                {
                    writeToLog(SenderThinkTimeMustBeANumber);
                    return false;
                }
                senderThinkTime = temp;
                if (!int.TryParse(txtSendTaskCount.Text, out temp) || temp <= 0)
                {
                    writeToLog(SendTaskCountMustBeANumber);
                    return false;
                }
                senderTaskCount = temp;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return false;
            }
            return true;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStart.Text == StopCaption)
                {
                    await CancelActions();
                    btnStart.Text = StartCaption;
                    return;
                }

                if (serviceBusHelper != null &&
                    ValidateParameters())
                {
                    if (startLog != null)
                    {
                        startLog();
                    }
                    btnStart.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;

                    //*****************************************************************************************************
                    //                                   Initialize Statistics and Manager Action
                    //*****************************************************************************************************
                    actionCount = 0;
                    senderMessageNumber = 0;
                    senderMessagesPerSecond = 0;
                    senderMinimumTime = long.MaxValue;
                    senderMaximumTime = 0;
                    senderAverageTime = 0;
                    senderTotalTime = 0;

                    lblSenderLastTime.Text = string.Empty;
                    lblSenderAverageTime.Text = string.Empty;
                    lblSenderMaximumTime.Text = string.Empty;
                    lblSenderMinimumTime.Text = string.Empty;
                    lblSenderMessagesPerSecond.Text = string.Empty;
                    lblSenderMessageNumber.Text = string.Empty;

                    if (checkBoxSenderEnableGraph.Checked)
                    {
                        chart.Series.ToList().ForEach(s => s.Points.Clear());
                    }
                    managerResetEvent = new ManualResetEventSlim(false);
                    Action<CancellationTokenSource> managerAction = cts =>
                    {
                        if (cts == null)
                        {
                            return;
                        }
                        try
                        {
                            managerResetEvent.Wait(cts.Token);
                        }
                        catch (OperationCanceledException)
                        {
                        }
                        if (!cts.IsCancellationRequested)
                        {
                            Invoke((MethodInvoker) delegate { btnStart.Text = StartCaption; });
                        }
                    };

                    Action updateGraphAction = () =>
                    {
                        var ok = true;
                        long max = 10;
                        while (!graphCancellationTokenSource.IsCancellationRequested && (actionCount > 1 || ok))
                        {
                            ok = true;
                            long sendMessageNumber = 0;
                            long sendTotalTime = 0;
                            while (ok && sendMessageNumber < max)
                            {
                                ok = blockingCollection.TryTake(out var tuple, 10);
                                if (ok)
                                {
                                    sendMessageNumber += tuple.Item1;
                                    sendTotalTime += tuple.Item2;
                                    if (sendMessageNumber > max)
                                    {
                                        max = sendMessageNumber;
                                    }
                                }
                            }
                            if (sendMessageNumber > 0)
                            {
                                var sendTuple = new Tuple<long, long, DirectionType>(sendMessageNumber, sendTotalTime, DirectionType.Send);
                                if (InvokeRequired)
                                {
                                    Invoke(new UpdateStatisticsDelegate(InternalUpdateStatistics),
                                        new object[]
                                        {
                                            sendTuple.Item1,
                                            sendTuple.Item2,
                                            sendTuple.Item3
                                        });
                                }
                                else
                                {
                                    InternalUpdateStatistics(sendTuple.Item1,
                                        sendTuple.Item2,
                                        sendTuple.Item3);
                                }
                            }
                        }
                        if (Interlocked.Decrement(ref actionCount) == 0)
                        {
                            managerResetEvent.Set();
                        }
                    };

                    AsyncCallback updateGraphCallback = a =>
                    {
                        var action = a.AsyncState as Action;
                        if (action != null)
                        {
                            action.EndInvoke(a);
                            if (Interlocked.Decrement(ref actionCount) == 0)
                            {
                                managerResetEvent.Set();
                            }
                        }
                    };

                    blockingCollection = new BlockingCollection<Tuple<long, long, DirectionType>>();

                    //*****************************************************************************************************
                    //                                   Sending messages to a EventHub
                    //*****************************************************************************************************

                    if (eventDataCount > 0)
                    {
                        // Create event hub clients. They are cached for later usage to improve performance.
                        if (isSenderFaulted ||
                            eventHubClientCollection == null ||
                            eventHubClientCollection.Count == 0 ||
                            eventHubClientCollection.Count < senderTaskCount)
                        {
                            eventHubClientCollection = new List<EventHubClient>(senderTaskCount);
                            var amqpConnectionString = GetAmqpConnectionString(serviceBusHelper.ConnectionString);
                            for (var i = 0; i < senderTaskCount; i++)
                            {
                                eventHubClientCollection.Add(EventHubClient.CreateFromConnectionString(amqpConnectionString, eventHubDescription.Path));
                            }
                            isSenderFaulted = false;
                        }

                        // Create outbound message template list
                        var eventDataTemplateList = new List<EventData>();
                        var updatePartitionKey = checkBoxUpdatePartitionKey.Checked;
                        var noPartitionKey = checkBoxNoPartitionKey.Checked;
                        if (messageTabControl.SelectedIndex == MessageTabPage)
                        {
                            eventDataTemplateList.Add(serviceBusHelper.CreateEventDataTemplate(txtMessageText.Text,
                                                                                               GetPartitionKey(),
                                                                                               bindingSource.Cast<MessagePropertyInfo>()));

                        }
                        else if (messageTabControl.SelectedIndex == FilesTabPage)
                        {
                            updatePartitionKey = !radioButtonJsonTemplate.Checked && !radioButtonXmlTemplate.Checked &&
                                                 checkBoxUpdatePartitionKey.Checked;
                            var fileList = messageFileListView.Items.Cast<ListViewItem>()
                                .Where(i => i.Checked)
                                .Select(i => i.Text)
                                .ToList();
                            if (fileList.Count == 0)
                            {
                                writeToLog(NoMessageSelected);
                                return;
                            }
                            foreach (var fileName in fileList)
                            {
                                try
                                {
                                    using (var streamReader = new StreamReader(fileName))
                                    {
                                        var text = streamReader.ReadToEnd();
                                        EventData template;
                                        if (radioButtonBinaryFile.Checked)
                                        {
                                            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                                            {
                                                using (var binaryReader = new BinaryReader(fileStream))
                                                {
                                                    var bytes = binaryReader.ReadBytes((int) fileStream.Length);
                                                    template = serviceBusHelper.CreateEventDataTemplate(new MemoryStream(bytes),
                                                                                                        GetPartitionKey(), 
                                                                                                        bindingSource.Cast<MessagePropertyInfo>());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (radioButtonTextFile.Checked)
                                            {
                                                template = serviceBusHelper.CreateEventDataTemplate(text, GetPartitionKey(), bindingSource.Cast<MessagePropertyInfo>());
                                            }
                                            else if (radioButtonJsonTemplate.Checked)
                                            {
                                                try
                                                {
                                                    var eventDataTemplate = JsonSerializerHelper.Deserialize<EventDataTemplate>(text);
                                                    template = serviceBusHelper.CreateEventDataTemplate(eventDataTemplate);
                                                }
                                                catch (Exception)
                                                {
                                                    writeToLog(string.Format(InvalidJsonTemplate, fileName));
                                                    template = serviceBusHelper.CreateEventDataTemplate(text, GetPartitionKey(), bindingSource.Cast<MessagePropertyInfo>());
                                                }
                                            }
                                            else // XML Template
                                            {
                                                try
                                                {
                                                    var eventDataTemplate = XmlSerializerHelper.Deserialize<EventDataTemplate>(text);
                                                    template = serviceBusHelper.CreateEventDataTemplate(eventDataTemplate);
                                                }
                                                catch (Exception)
                                                {
                                                    writeToLog(string.Format(InvalidXmlTemplate, fileName));
                                                    template = serviceBusHelper.CreateEventDataTemplate(text, GetPartitionKey(), bindingSource.Cast<MessagePropertyInfo>());
                                                }
                                            }
                                        }
                                        if (template != null)
                                        {
                                            eventDataTemplateList.Add(template);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    HandleException(ex);
                                }
                            }
                        }
                        else // Event Data Generator Tab
                        {
                            try
                            {
                                senderEventDataGenerator = eventDataGeneratorPropertyGrid.SelectedObject as IEventDataGenerator;
                                if (senderEventDataGenerator != null)
                                {
                                    eventDataTemplateList = new List<EventData>(senderEventDataGenerator.GenerateEventDataCollection(txtMessageCount.IntegerValue, writeToLog));
                                }
                            }
                            catch (Exception ex)
                            {
                                HandleException(ex);
                            }
                        }
                        try
                        {
                            senderCancellationTokenSource = new CancellationTokenSource();
                            currentIndex = 0;
                            senderEventDataInspector = cboSenderInspector.SelectedIndex > 0
                                                         ? Activator.CreateInstance(serviceBusHelper.EventDataInspectors[cboSenderInspector.Text]) as IEventDataInspector
                                                         : null;

                            Func<long> getMessageNumber = () =>
                            {
                                lock (this)
                                {
                                    return currentIndex++;
                                }
                            };
                            Action<int, IEnumerable<EventData>> senderAction =
                                (taskId, messageTemplateEnumerable) =>
                                {
                                    try
                                    {
                                        var traceMessage = serviceBusHelper.SendEventData(eventHubClientCollection[taskId],
                                                                                            messageTemplateEnumerable,
                                                                                            getMessageNumber,
                                                                                            eventDataCount,
                                                                                            taskId,
                                                                                            updatePartitionKey,
                                                                                            noPartitionKey,
                                                                                            checkBoxAddMessageNumber.Checked,
                                                                                            checkBoxEnableSenderLogging.Checked,
                                                                                            checkBoxSenderVerboseLogging.Checked,
                                                                                            checkBoxSenderEnableStatistics.Checked,
                                                                                            checkBoxSendBatch.Checked,
                                                                                            senderBatchSize,
                                                                                            checkBoxSenderThinkTime.Checked,
                                                                                            senderThinkTime,
                                                                                            senderEventDataInspector,
                                                                                            UpdateStatistics,
                                                                                            senderCancellationTokenSource,
                                                                                            partitionDescription != null ?
                                                                                            partitionDescription.PartitionId :
                                                                                            null).Result;
                                        if (!string.IsNullOrWhiteSpace(traceMessage))
                                        {
                                            writeToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        isSenderFaulted = true;
                                        HandleException(ex);
                                    }
                                };

                            // Define Sender AsyncCallback
                            AsyncCallback senderCallback = a =>
                            {
                                var action = a.AsyncState as Action<int, IEnumerable<EventData>>;
                                if (action != null)
                                {
                                    action.EndInvoke(a);
                                    if (Interlocked.Decrement(ref actionCount) == 0)
                                    {
                                        managerResetEvent.Set();
                                    }
                                }
                            };

                            // Start Sender Actions
                            for (var i = 0; i < Math.Min(eventDataCount, senderTaskCount); i++)
                            {
                                senderAction.BeginInvoke(i, eventDataTemplateList, senderCallback, senderAction);
                                Interlocked.Increment(ref actionCount);
                            }
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                    }
                    if (actionCount > 0)
                    {
                        managerCancellationTokenSource = new CancellationTokenSource();
                        managerAction.BeginInvoke(managerCancellationTokenSource, null, null);
                        graphCancellationTokenSource = new CancellationTokenSource();
                        updateGraphAction.BeginInvoke(updateGraphCallback, updateGraphAction);
                        Interlocked.Increment(ref actionCount);
                        btnStart.Text = StopCaption;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                btnStart.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private static string GetAmqpConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException(ConnectionStringCannotBeNull);
            }
            var builder = new ServiceBusConnectionStringBuilder(connectionString) { TransportType = TransportType.Amqp };
            return builder.ToString();
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

        private string GetPartitionKey()
        {
            if (checkBoxUpdatePartitionKey.Checked)
            {
                return Guid.NewGuid().ToString();
            }
            if (!string.IsNullOrWhiteSpace(txtPartitionKey.Text))
            {
                return txtPartitionKey.Text;
            }
            return Guid.NewGuid().ToString();
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

        internal async Task CancelActions()
        {
            if (stopLog != null)
            {
                await stopLog();
            }
            if (managerCancellationTokenSource != null)
            {
                managerCancellationTokenSource.Cancel();
            }
            if (graphCancellationTokenSource != null)
            {
                graphCancellationTokenSource.Cancel();
            }
            if (senderCancellationTokenSource != null)
            {
                senderCancellationTokenSource.Cancel();
            }
        }

        internal async void btnCancel_Click(object sender, EventArgs e)
        {
            await CancelActions();
            OnCancel();
        }

        private void mainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(mainTabControl, e, null);
        }

       private void checkBoxEnableSenderLogging_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSenderVerboseLogging.Enabled = checkBoxEnableSenderLogging.Checked;
        }

        private void checkBoxSenderEnableStatistics_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSenderEnableGraph.Enabled = checkBoxSenderEnableStatistics.Checked;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.FileName = string.Empty;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() != DialogResult.OK || 
                    string.IsNullOrWhiteSpace(openFileDialog.FileName) ||
                    !File.Exists(openFileDialog.FileName))
                {
                    return;
                }
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    var text = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        return;
                    }
                    txtMessageText.Text = XmlHelper.Indent(text);
                    if (mainForm != null)
                    {
                        mainForm.MessageText = text;
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

                if (elapsedSeconds > senderMaximumTime)
                {
                    senderMaximumTime = elapsedSeconds;
                }
                if (elapsedSeconds < senderMinimumTime)
                {
                    senderMinimumTime = elapsedSeconds;
                }
                senderTotalTime += elapsedSeconds;
                senderMessageNumber += messageNumber;
                senderAverageTime = senderMessageNumber > 0 ? senderTotalTime / senderMessageNumber : 0;
                senderMessagesPerSecond = senderTotalTime > 0 ? senderMessageNumber * senderTaskCount / senderTotalTime : 0;

                lblSenderLastTime.Text = string.Format(LabelFormat, elapsedSeconds);
                lblSenderLastTime.Refresh();
                lblSenderAverageTime.Text = string.Format(LabelFormat, senderAverageTime);
                lblSenderAverageTime.Refresh();
                lblSenderMaximumTime.Text = string.Format(LabelFormat, senderMaximumTime);
                lblSenderMaximumTime.Refresh();
                lblSenderMinimumTime.Text = string.Format(LabelFormat, senderMinimumTime);
                lblSenderMinimumTime.Refresh();
                lblSenderMessagesPerSecond.Text = string.Format(LabelFormat, senderMessagesPerSecond);
                lblSenderMessagesPerSecond.Refresh();
                lblSenderMessageNumber.Text = senderMessageNumber.ToString(CultureInfo.InvariantCulture);
                lblSenderMessageNumber.Refresh();

                if (checkBoxSenderEnableGraph.Checked)
                {
                    chart.Series["SenderLatency"].Points.AddXY(senderMessageNumber, elapsedSeconds);
                    chart.Series["SenderThroughput"].Points.AddXY(senderMessageNumber, senderMessagesPerSecond);
                }
            }
        }

        private void SetGraphLayout()
        {
            chart.Series.Clear();

            var series1 = new Series();
            var series3 = new Series();

            series1.BorderColor = Color.FromArgb(180, 26, 59, 105);
            series1.BorderWidth = 2;
            series1.ChartArea = "Default";
            series1.ChartType = SeriesChartType.FastLine;
            series1.Legend = "Default";
            series1.LegendText = "Sender Latency";
            series1.Name = "SenderLatency";

            series3.BorderWidth = 2;
            series3.ChartArea = "Default";
            series3.ChartType = SeriesChartType.FastLine;
            series3.Legend = "Default";
            series3.LegendText = "Sender Throughput";
            series3.Name = "SenderThroughput";

            chart.Series.Add(series1);
            chart.Series.Add(series3);
            chart.Visible = true;

            grouperSenderStatistics.Visible = true;
            
            var title = new Title
            {
                Font = new Font("Microsoft Sans Serif", 8.25F,
                                FontStyle.Regular,
                                GraphicsUnit.Point,
                                0),
                Name = "Title",
                ShadowColor = Color.Transparent,
                ShadowOffset = 1,
                Text = "Sender Performance Counters"
            };

            chart.Titles.Clear();
            chart.Titles.Add(title);
            tabPageGraph.Refresh();
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

        private void propertiesDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateLastColumnWidth();
        }

        private void propertiesDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateLastColumnWidth();
        }

        private void propertiesDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateLastColumnWidth();
        }

        //private void checkBoxSendBatch_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtSendBatchSize.Enabled = checkBoxSendBatch.Checked;
        //}

        private void CalculateLastColumnWidth()
        {
            if (propertiesDataGridView.Columns.Count == 3)
            {
                var width = propertiesDataGridView.Width - propertiesDataGridView.Columns[0].Width -
                            propertiesDataGridView.Columns[1].Width - propertiesDataGridView.RowHeadersWidth;
                var verticalScrollbar = propertiesDataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar != null && verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                propertiesDataGridView.Columns[2].Width = width;
            }
        }

        private void grouperMessageProperties_CustomPaint(PaintEventArgs e)
        {
            propertiesDataGridView.Size = new Size(grouperMessageProperties.Size.Width - 32,
                                                   grouperMessageProperties.Size.Height - 48);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   propertiesDataGridView.Location.X - 1,
                                   propertiesDataGridView.Location.Y - 1,
                                   propertiesDataGridView.Size.Width + 1,
                                   propertiesDataGridView.Size.Height + 1);
        }
        
        private void checkBoxSenderThinkTime_CheckedChanged(object sender, EventArgs e)
        {
            txtSenderThinkTime.Enabled = checkBoxSenderThinkTime.Checked;
        }

        private void messageTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawTabControlTabs(messageTabControl, e, null);
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = true;
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog() != DialogResult.OK ||
               !openFileDialog.FileNames.Any())
            {
                return;
            }
            foreach (var fileInfo in openFileDialog.FileNames.Select(fileName => new FileInfo(fileName)))
            {
                var size = string.Format("{0} KB", fileInfo.Length%1024 == 0
                                                       ? fileInfo.Length/1024
                                                       : fileInfo.Length/1024 + 1);
                messageFileListView.Items.Add(new ListViewItem(new[]
                {
                    fileInfo.FullName,
                    size
                }) { Checked = true });
                mainForm.FileNames.Add(new Tuple<string, string>(fileInfo.FullName, size));
            }
            checkBoxFileName.Checked = messageFileListView.Items.Cast<ListViewItem>().All(i => i.Checked);
            var fileList = messageFileListView.Items.Cast<ListViewItem>()
                                    .Select(i => i.Text)
                                    .ToList();
            if (fileList.All(f => Path.GetExtension(f) == ".txt"))
            {
                radioButtonTextFile.Checked = true;
            }
            else if (fileList.All(f => Path.GetExtension(f) == ".json"))
            {
                radioButtonJsonTemplate.Checked = true;
            }
            else if (fileList.All(f => Path.GetExtension(f) == ".xml"))
            {
                radioButtonXmlTemplate.Checked = true;
            }
            btnClearFiles.Enabled = messageFileListView.Items.Count > 0;
        }

        private void messageFileListView_Resize(object sender, EventArgs e)
        {
            try
            {
                messageFileListView.SuspendDrawing();
                messageFileListView.SuspendLayout();
                var listView = sender as ListView;
                if (listView == null)
                {
                    return;
                }
                var width = listView.Width - listView.Columns[SizeListViewColumnIndex].Width;
                listView.Columns[NameListViewColumnIndex].Width = width - 4;
            }
            finally
            {
                messageFileListView.ResumeLayout();
                messageFileListView.ResumeDrawing();
            }
        }

        private void messageFileListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

        private void messageFileListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //e.DrawDefault = true;
            //e.DrawBackground();
            //e.DrawText();
        }

        private void messageFileListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            messageFileListView.Items.Clear();
            mainForm.FileNames.Clear();
            btnClearFiles.Enabled = false;
        }

        private void checkBoxFileName_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < messageFileListView.Items.Count; i++)
            {
                messageFileListView.Items[i].Checked = checkBoxFileName.Checked;
            }
        }

        private void grouperMessageFiles_CustomPaint(PaintEventArgs obj)
        {
            checkBoxFileName.Location = new Point(messageFileListView.Location.X + 8,
                                                  messageFileListView.Location.Y + 4);

            var width = (grouperMessageFiles.Size.Width - 32) / 4;
            radioButtonBinaryFile.Location = new Point(width + 16, radioButtonJsonTemplate.Location.Y);
            radioButtonJsonTemplate.Location = new Point(2 * width + 16, radioButtonJsonTemplate.Location.Y);
            radioButtonXmlTemplate.Location = new Point(grouperMessageFiles.Size.Width - 16 - radioButtonXmlTemplate.Size.Width, radioButtonXmlTemplate.Location.Y);
        }

        private void grouperEventDataGenerator_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   cboEventDataGeneratorType.Location.X - 1,
                                   cboEventDataGeneratorType.Location.Y - 1,
                                   cboEventDataGeneratorType.Size.Width + 1,
                                   cboEventDataGeneratorType.Size.Height + 1);
            eventDataGeneratorPropertyGrid.HelpVisible = eventDataGeneratorPropertyGrid.Height > 250;
        }

        private void cboEventDataGeneratorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboEventDataGeneratorType.SelectedIndex == 0)
                {
                    return;
                }
                if (!serviceBusHelper.EventDataGenerators.ContainsKey(cboEventDataGeneratorType.Text))
                {
                    return;
                }
                var type = serviceBusHelper.EventDataGenerators[cboEventDataGeneratorType.Text];
                if (type == null)
                {
                    return;
                }
                eventDataGeneratorPropertyGrid.SelectedObject = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void propertiesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void grouperSender_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboSenderInspector.Location.X - 1,
                                    cboSenderInspector.Location.Y - 1,
                                    cboSenderInspector.Size.Width + 1,
                                    cboSenderInspector.Size.Height + 1);
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

                if (senderCancellationTokenSource != null)
                {
                    senderCancellationTokenSource.Dispose();
                }

                if (managerCancellationTokenSource != null)
                {
                    managerCancellationTokenSource.Dispose();
                }

                if (graphCancellationTokenSource != null)
                {
                    graphCancellationTokenSource.Dispose();
                }

                if (managerResetEvent != null)
                {
                    managerResetEvent.Dispose();
                }

                if (blockingCollection != null)
                {
                    blockingCollection.Dispose();
                }

                if (senderEventDataGenerator != null)
                {
                    var disposable = senderEventDataGenerator as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                if (senderEventDataInspector != null)
                {
                    var disposable = senderEventDataInspector as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
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

        private void partitionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == checkBoxUpdatePartitionKey && checkBoxUpdatePartitionKey.Checked)
            {
                checkBoxNoPartitionKey.Checked = false;
            }
            if (sender == checkBoxNoPartitionKey && checkBoxNoPartitionKey.Checked)
            {
                checkBoxUpdatePartitionKey.Checked = false;
            }
        }

        private void checkBoxSendBatch_CheckedChanged(object sender, EventArgs e)
        {
            txtSendBatchSize.Enabled = checkBoxSendBatch.Checked;
        }

        private void grouperMessageFormat_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   cboMessageFormat.Location.X - 1,
                                   cboMessageFormat.Location.Y - 1,
                                   cboMessageFormat.Size.Width + 1,
                                   cboMessageFormat.Size.Height + 1);
        }

        private void txtMessageText_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMessageText.Text))
            {
                mainForm.MessageText = txtMessageText.Text;
            }
        }

        private void cboMessageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LanguageDetector.SetFormattedMessage(cboMessageFormat.Text, txtMessageText);
        }

        private void grouperPartitionKey_CustomPaint(PaintEventArgs e)
        {
            grouperPartitionKey.Location = new Point(0, splitContainer.Panel2.Height - grouperPartitionKey.Size.Height - 16);
            grouperPartitionKey.Size = new Size(splitContainer.Panel2.Width, grouperPartitionKey.Size.Height);
        }
        #endregion
    }
}
