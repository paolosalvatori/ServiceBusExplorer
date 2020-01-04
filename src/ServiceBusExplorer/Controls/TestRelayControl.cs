﻿#region Copyright
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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.UIHelpers;
using static ServiceBusExplorer.ServiceBusHelper;
using ServiceBusExplorer.Utilities.Helpers;

#endregion

namespace ServiceBusExplorer.Controls
{
    public partial class TestRelayControl : UserControl
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
        private const string HeaderName = "Name";
        private const string HeaderNamespace = "Namespace";
        private const string HeaderValue = "Value";
        private const string StartCaption = "Start";
        private const string StopCaption = "Stop";
        private const string DefaultMessageCount = "1";
        private const string DefaultSendTaskCount = "1";
        private const string DefaultAction = "*";

        //***************************
        // Messages
        //***************************
        private const string DefaultMessageText = "Hi mate, how are you?";
        private const string MessageCountMustBeANumber = "The Message Count field must be an integer number greater or equal to zero.";
        private const string SendTaskCountMustBeANumber = "The Sender Task Count field must be an integer number greater than zero.";
        private const string MessageCannotBeNull = "The Message field cannot be null.";
        private const string SenderStatisticsHeader = "Sender[{0}]:";
        private const string SenderStatisticsLine1 = " - Message Count=[{0}] Messages Sent/Sec=[{1:F1}] Total Elapsed Time (ms)=[{2}]";
        private const string SenderStatisticsLine2 = " - Average Send Time (ms)=[{0}] Minimum Send Time (ms)=[{1}] Maximum Send Time (ms)=[{2}] ";
        private const string MessageSuccessfullySent = "Sender[{0}]: Request message sent. MessageNumber=[{1}]";
        private const string MessageSuccessfullyReceived = "Sender[{0}]: Response message received. MessageNumber=[{1}]";
        private const string PayloadHeader = "Payload:";
        private const string WcfMessageTextFormat = "{0}";
        private const string HeadersHeader = "Headers:";
        private const string MessageHeaderFormat = " - Name=[{0}] Namespace=[{1}] Value=[{2}]";
        private const string ExceptionOccurred = " - Exception occurred: {0}";

        //***************************
        // Tooltips
        //***************************
        private const string MessageCountTooltip = "Gets or sets the number of messages to send.";
        private const string SendTaskCountTooltip = "Gets or sets the count of message senders.";
        #endregion

        #region Private Instance Fields
        private readonly RelayDescription relayDescription;
        private readonly MainForm mainForm;
        private readonly WriteToLogDelegate writeToLog;
        private readonly Func<Task> stopLog;
        private readonly Action startLog;
        private readonly BindingSource bindingSource = new BindingSource();
        private System.ServiceModel.Channels.Binding binding;
        private CancellationTokenSource managerCancellationTokenSource;
        private CancellationTokenSource senderCancellationTokenSource;
        private ManualResetEventSlim managerResetEvent;
        private int actionCount;
        private int messageCount;
        private int senderTaskCount;
        private long currentIndex;
        private long senderMessageNumber;
        private double senderMessagesPerSecond;
        private double senderMinimumTime;
        private double senderMaximumTime;
        private double senderAverageTime;
        private double senderTotalTime;
        private readonly ServiceBusHelper serviceBusHelper;
        #endregion

        #region Public Constructors
        public TestRelayControl(MainForm mainForm, 
                                WriteToLogDelegate writeToLog,
                                Func<Task> stopLog,
                                Action startLog,
                                RelayDescription relayDescription, 
                                ServiceBusHelper serviceBusHelper)
        {
            this.mainForm = mainForm;
            this.writeToLog = writeToLog;
            this.stopLog = stopLog;
            this.startLog = startLog;
            this.relayDescription = relayDescription;
            this.serviceBusHelper = serviceBusHelper;
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
                txtMessageCount.Text = DefaultMessageCount;
                txtSendTaskCount.Text = DefaultSendTaskCount;
                txtAction.Text = DefaultAction;

                // ReSharper disable CoVariantArrayConversion
                cboBinding.Items.AddRange(ServiceBusBindingHelper.Bindings.Keys.ToArray());
                // ReSharper restore CoVariantArrayConversion
                if (cboBinding.Items.Count > 0)
                {
                    cboBinding.SelectedIndex = 0;
                }

                cboMessageFormat.Items.AddRange(new[] { "Text", "JSON", "XML" });
                cboMessageFormat.SelectedIndex = 0;

                grouperMessageText.Size = new Size(splitContainer.Panel1.Width, grouperMessageText.Size.Height);
                grouperMessageFormat.Size = new Size(splitContainer.Panel1.Width, grouperMessageFormat.Size.Height);
                grouperMessageHeaders.Size = new Size(splitContainer.Panel2.Width, grouperMessageHeaders.Size.Height);

                bindingSplitContainer.SplitterWidth = 16;

                switch (relayDescription.RelayType)
                {
                    case RelayType.Http:
                        cboBinding.Text = typeof(BasicHttpRelayBinding).Name;
                        break;
                    case RelayType.NetEvent:
                        cboBinding.Text = typeof(NetEventRelayBinding).Name;
                        break;
                    case RelayType.NetOneway:
                        cboBinding.Text = typeof(NetOnewayRelayBinding).Name;
                        break;
                    case RelayType.NetTcp:
                        cboBinding.Text = typeof(NetTcpRelayBinding).Name;
                        break;
                    case RelayType.None:
                        cboBinding.Text = typeof(BasicHttpRelayBinding).Name;
                        break;
                }

                bindingSource.DataSource = CustomMessageHeaderInfo.Headers;
                

                // Initialize the DataGridView.
                headersDataGridView.AutoGenerateColumns = false;
                headersDataGridView.AutoSize = true;
                headersDataGridView.DataSource = bindingSource;
                headersDataGridView.ForeColor = SystemColors.WindowText;

                // Create the Name column
                var textBoxColumn = new DataGridViewTextBoxColumn
                                        {
                                            DataPropertyName = HeaderName, 
                                            Name = HeaderName, 
                                            Width = 90
                                        };
                headersDataGridView.Columns.Add(textBoxColumn);

                // Create the Type column
                textBoxColumn = new DataGridViewTextBoxColumn
                                        {
                                            DataPropertyName = HeaderNamespace,
                                            Name = HeaderNamespace,
                                            Width = 84
                                        };
                headersDataGridView.Columns.Add(textBoxColumn);

                // Create the Value column
                textBoxColumn = new DataGridViewTextBoxColumn
                                    {
                                        DataPropertyName = HeaderValue, 
                                        Name = HeaderValue, 
                                        Width = 102
                                    };
                headersDataGridView.Columns.Add(textBoxColumn);

                // Set Grid style
                headersDataGridView.EnableHeadersVisualStyles = false;

                // Set the selection background color for all the cells.
                headersDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(92, 125, 150);
                headersDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.Window;

                // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default 
                // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
                headersDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 180, 209);

                // Set the background color for all rows and for alternating rows.  
                // The value for alternating rows overrides the value for all rows. 
                headersDataGridView.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                headersDataGridView.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                //headersDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                //headersDataGridView.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                // Set the row and column header styles.
                headersDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                headersDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                headersDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
                headersDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

                LanguageDetector.SetFormattedMessage(serviceBusHelper,
                                                     mainForm != null &&
                                                     !string.IsNullOrWhiteSpace(mainForm.MessageText) ?
                                                     mainForm.MessageText :
                                                     DefaultMessageText,
                                                     txtMessageText);

                // Set Tooltips
                toolTip.SetToolTip(txtMessageCount, MessageCountTooltip);
                toolTip.SetToolTip(txtSendTaskCount, SendTaskCountTooltip);

                splitContainer.SplitterWidth = 16;

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
                messageCount = temp;
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

                if (ValidateParameters() &&
                    serviceBusHelper != null)
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
                        chart.Series[0].Points.Clear();
                        chart.Series[1].Points.Clear();
                    }
                    managerResetEvent = new ManualResetEventSlim(false);
                    Action<CancellationTokenSource> managerAction = cts =>
                    {
                        if (cts != null)
                        {
                            managerResetEvent.Wait(cts.Token);
                            if (!cts.IsCancellationRequested)
                            {
                                Invoke((MethodInvoker)delegate { btnStart.Text = StartCaption; });
                            }
                        }
                    };

                    //*****************************************************************************************************
                    //                                   Sending messages to the Relay
                    //*****************************************************************************************************
                    if (binding != null &&
                        messageCount > 0)
                    {
                        var oneWay = ServiceBusBindingHelper.IsOneWay(binding);

                        try
                        {
                            senderCancellationTokenSource = new CancellationTokenSource();
                            currentIndex = 0;
                            Func<long> getMessageNumber = () =>
                            {
                                lock (this)
                                {
                                    return currentIndex++;
                                }
                            };
                            Action<int, bool, string> senderAction = (taskId, oneway, messageText) =>
                            {
                                var messagesSent = 0;
                                var totalElapsedTime = 0L;
                                var minimumSendTime = long.MaxValue;
                                var maximumSendTime = 0L;
                                string exceptionMessage = null;
                                string traceMessage;
                                ChannelFactory<IOutputChannel> outputChannelFactory = null;
                                ChannelFactory<IRequestChannel> requestChannelFactory = null;
                                IOutputChannel outputChannel = null;
                                IRequestChannel requestChannel = null;
                                StringBuilder builder;

                                // local method to remove duplication
                                void closeFactoriesAndChannelsAndHandleException(Exception exception)
                                {
                                    outputChannelFactory?.Abort();
                                    outputChannel?.Abort();
                                    requestChannelFactory?.Abort();
                                    requestChannel?.Abort();
                                    exceptionMessage = string.Format(ExceptionOccurred, exception.Message);
                                    HandleException(exception);
                                }

                                try
                                {
                                    MessageBuffer messageBuffer;
                                    using (var stringReader = new StringReader(txtMessageText.Text))
                                    {
                                        using (var xmlTextReader = new XmlTextReader(stringReader))
                                        {
                                            var message = System.ServiceModel.Channels.Message.CreateMessage(binding.MessageVersion, txtAction.Text, xmlTextReader);
                                            var headers = bindingSource.Cast<CustomMessageHeaderInfo>();
                                            foreach (var header in headers)
                                            {
                                                if (header != null &&
                                                    !string.IsNullOrWhiteSpace(header.Name) &&
                                                    !string.IsNullOrWhiteSpace(header.Namespace) &&
                                                    !string.IsNullOrWhiteSpace(header.Value))
                                                {
                                                    message.Headers.Add(MessageHeader.CreateHeader(header.Name, header.Namespace, header.Value));
                                                }
                                            }
                                            //message.Headers.Add(MessageHeader.CreateHeader(
                                            messageBuffer = message.CreateBufferedCopy(1048576); // 1 MB
                                        }
                                    }

                                    var tokenProvider = CreateTokenProvider();

                                    if (oneWay)
                                    {
                                        outputChannelFactory = new ChannelFactory<IOutputChannel>(binding, serviceBusHelper.GetRelayUri(relayDescription).AbsoluteUri);
                                        outputChannelFactory.Endpoint.Contract.SessionMode = SessionMode.Allowed;
                                        if (ServiceBusBindingHelper.GetRelayClientAuthenticationType(binding) == RelayClientAuthenticationType.RelayAccessToken)
                                        {
                                            outputChannelFactory.Endpoint.Behaviors.Add(new TransportClientEndpointBehavior(tokenProvider));
                                        }
                                        outputChannel = outputChannelFactory.CreateChannel();
                                    }
                                    else
                                    {
                                        if (binding is WebHttpRelayBinding)
                                        {
                                            requestChannelFactory = new WebChannelFactory<IRequestChannel>(binding, serviceBusHelper.GetRelayUri(relayDescription));
                                        }
                                        else
                                        {
                                            requestChannelFactory = new ChannelFactory<IRequestChannel>(binding, serviceBusHelper.GetRelayUri(relayDescription).AbsoluteUri);
                                        }
                                        
                                        requestChannelFactory.Endpoint.Contract.SessionMode = SessionMode.Allowed;
                                        if (ServiceBusBindingHelper.GetRelayClientAuthenticationType(binding) == RelayClientAuthenticationType.RelayAccessToken)
                                        {
                                            requestChannelFactory.Endpoint.Behaviors.Add(new TransportClientEndpointBehavior(tokenProvider));
                                        }
                                        requestChannel = requestChannelFactory.CreateChannel();
                                    }

                                    long messageNumber;
                                    while ((messageNumber = getMessageNumber()) < messageCount &&
                                            !senderCancellationTokenSource.Token.IsCancellationRequested)
                                    {
                                        var stopwatch = new Stopwatch();
                                        var requestMessage = messageBuffer.CreateMessage();
                                        long elapsedMilliseconds;
                                        if (oneway)
                                        {
                                            try
                                            {
                                                stopwatch.Start();
                                                if (outputChannel != null)
                                                {
                                                    RetryHelper.RetryAction(() => outputChannel.Send(requestMessage), writeToLog);
                                                }
                                            }
                                            finally
                                            {
                                                stopwatch.Stop();
                                                elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                                            }
                                            if (checkBoxEnableSenderLogging.Checked)
                                            {
                                                builder = new StringBuilder();
                                                builder.AppendLine(string.Format(MessageSuccessfullySent, taskId, messageNumber));
                                                if (checkBoxSenderVerboseLogging.Checked)
                                                {
                                                    var stringBuilder = new StringBuilder();
                                                    using (var stringReader = new StringReader(messageText))
                                                    {
                                                        using (var reader = XmlReader.Create(stringReader))
                                                        {
                                                            // The XmlWriter is used just to indent the XML message
                                                            var settings = new XmlWriterSettings { Indent = true };
                                                            using (var writer = XmlWriter.Create(stringBuilder, settings))
                                                            {
                                                                writer.WriteNode(reader, true);
                                                            }
                                                        }
                                                    }
                                                    var bodyContents = stringBuilder.ToString();
                                                    builder.AppendLine(PayloadHeader);
                                                    builder.AppendLine(string.Format(WcfMessageTextFormat, bodyContents));
                                                    if (requestMessage.Headers.Count > 0)
                                                    {
                                                        builder.AppendLine(HeadersHeader);
                                                        for (var i = 0; i < requestMessage.Headers.Count; i++)
                                                        {
                                                            var reader = requestMessage.Headers.GetReaderAtHeader(i);
                                                            var value = reader.ReadOuterXml();
                                                            builder.AppendLine(string.Format(MessageHeaderFormat,
                                                                                            requestMessage.Headers[i].Name,
                                                                                            requestMessage.Headers[i].Namespace,
                                                                                            value));
                                                        }
                                                    }
                                                }
                                                traceMessage = builder.ToString();
                                                if (!string.IsNullOrWhiteSpace(traceMessage))
                                                {
                                                    writeToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            System.ServiceModel.Channels.Message responseMessage = null;
                                            try
                                            {
                                                stopwatch.Start();
                                                if (requestChannel != null)
                                                {
                                                    responseMessage =
                                                        RetryHelper.RetryFunc(
                                                            () => requestChannel.Request(requestMessage), writeToLog);
                                                }
                                            }
                                            finally
                                            {
                                                stopwatch.Stop();
                                                elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                                            }
                                            if (checkBoxEnableSenderLogging.Checked)
                                            {
                                                builder = new StringBuilder();
                                                builder.AppendLine(string.Format(MessageSuccessfullySent, taskId, messageNumber));
                                                if (checkBoxSenderVerboseLogging.Checked)
                                                {
                                                    var stringBuilder = new StringBuilder();
                                                    using (var stringReader = new StringReader(messageText))
                                                    {
                                                        using (var reader = XmlReader.Create(stringReader))
                                                        {
                                                            // The XmlWriter is used just to indent the XML message
                                                            var settings = new XmlWriterSettings { Indent = true };
                                                            using (var writer = XmlWriter.Create(stringBuilder, settings))
                                                            {
                                                                writer.WriteNode(reader, true);
                                                            }
                                                        }
                                                    }
                                                    var bodyContents = stringBuilder.ToString();
                                                    builder.AppendLine(PayloadHeader);
                                                    builder.AppendLine(string.Format(WcfMessageTextFormat, bodyContents));
                                                    if (requestMessage.Headers.Count > 0)
                                                    {
                                                        builder.AppendLine(HeadersHeader);
                                                        for (var i = 0; i < requestMessage.Headers.Count; i++)
                                                        {
                                                            var reader = requestMessage.Headers.GetReaderAtHeader(i);
                                                            var value = reader.ReadOuterXml();
                                                            builder.AppendLine(string.Format(MessageHeaderFormat,
                                                                                            requestMessage.Headers[i].Name,
                                                                                            requestMessage.Headers[i].Namespace,
                                                                                            value));
                                                        }
                                                    }
                                                }
                                                builder.AppendLine(string.Format(MessageSuccessfullyReceived, taskId, messageNumber));
                                                if (responseMessage != null &&
                                                    checkBoxSenderVerboseLogging.Checked)
                                                {
                                                    var stringBuilder = new StringBuilder();
                                                    using (var reader = responseMessage.GetReaderAtBodyContents())
                                                    {
                                                        // The XmlWriter is used just to indent the XML message
                                                        var settings = new XmlWriterSettings { Indent = true };
                                                        using (var writer = XmlWriter.Create(stringBuilder, settings))
                                                        {
                                                            writer.WriteNode(reader, true);
                                                        }
                                                    }
                                                    var bodyContents = stringBuilder.ToString();
                                                    builder.AppendLine(PayloadHeader);
                                                    builder.AppendLine(string.Format(WcfMessageTextFormat, bodyContents));
                                                    if (responseMessage.Headers.Count > 0)
                                                    {
                                                        builder.AppendLine(HeadersHeader);
                                                        for (var i = 0; i < responseMessage.Headers.Count; i++)
                                                        {
                                                            var reader = responseMessage.Headers.GetReaderAtHeader(i);
                                                            var value = reader.ReadOuterXml();
                                                            builder.AppendLine(string.Format(MessageHeaderFormat,
                                                                                                responseMessage.Headers[i].Name,
                                                                                                responseMessage.Headers[i].Namespace,
                                                                                                value));
                                                        }
                                                    }
                                                }
                                                traceMessage = builder.ToString();
                                                if (!string.IsNullOrWhiteSpace(traceMessage))
                                                {
                                                    writeToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                                }
                                            }
                                        }
                                        messagesSent++;
                                        if (elapsedMilliseconds > maximumSendTime)
                                        {
                                            maximumSendTime = elapsedMilliseconds;
                                        }
                                        if (elapsedMilliseconds < minimumSendTime)
                                        {
                                            minimumSendTime = elapsedMilliseconds;
                                        }
                                        totalElapsedTime += elapsedMilliseconds;
                                        if (checkBoxSenderEnableStatistics.Checked)
                                        {
                                            UpdateStatistics(elapsedMilliseconds, DirectionType.Send);
                                        }
                                    }
                                    outputChannelFactory?.Close();
                                    outputChannel?.Close();
                                    requestChannelFactory?.Close();
                                    requestChannel?.Close();
                                }
                                catch (FaultException ex)
                                {
                                    closeFactoriesAndChannelsAndHandleException(ex);
                                }
                                catch (CommunicationObjectAbortedException ex)
                                {
                                    closeFactoriesAndChannelsAndHandleException(ex);
                                }
                                catch (CommunicationObjectFaultedException ex)
                                {
                                    closeFactoriesAndChannelsAndHandleException(ex);
                                }
                                catch (CommunicationException ex)
                                {
                                    closeFactoriesAndChannelsAndHandleException(ex);
                                }
                                catch (TimeoutException ex)
                                {
                                    closeFactoriesAndChannelsAndHandleException(ex);
                                }
                                catch (Exception ex)
                                {
                                    closeFactoriesAndChannelsAndHandleException(ex);
                                }
                                var averageSendTime = messagesSent > 0 ? totalElapsedTime / messagesSent : maximumSendTime;
                                var messagesPerSecond = totalElapsedTime > 0 ? messagesSent * 1000 / (double)totalElapsedTime : 0;
                                builder = new StringBuilder();
                                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                                 SenderStatisticsHeader,
                                                                 taskId));
                                if (!string.IsNullOrWhiteSpace(exceptionMessage))
                                {
                                    builder.AppendLine(exceptionMessage);
                                }
                                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                                 SenderStatisticsLine1,
                                                                 messagesSent,
                                                                 messagesPerSecond,
                                                                 totalElapsedTime));
                                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,
                                                                 SenderStatisticsLine2,
                                                                 averageSendTime,
                                                                 minimumSendTime == long.MaxValue ? 0 : minimumSendTime,
                                                                 maximumSendTime));
                                traceMessage = builder.ToString();
                                if (!string.IsNullOrWhiteSpace(traceMessage))
                                {
                                    writeToLog(traceMessage.Substring(0, traceMessage.Length - 1));
                                }
                            };

                            // Define Sender AsyncCallback
                            AsyncCallback senderCallback = a =>
                            {
                                var action = a.AsyncState as Action<int, bool, string>;
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
                            for (var i = 0; i < Math.Min(messageCount, senderTaskCount); i++)
                            {
                                senderAction.BeginInvoke(i, oneWay, txtMessageText.Text, senderCallback, senderAction);
                                Interlocked.Increment(ref actionCount);
                            }
                        }
                        catch (Exception ex)
                        {
                            HandleException(ex);
                        }
                        if (actionCount > 0)
                        {
                            managerCancellationTokenSource = new CancellationTokenSource();
                            managerAction.BeginInvoke(managerCancellationTokenSource, null, null);
                            btnStart.Text = StopCaption;
                        }
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

        private TokenProvider CreateTokenProvider()
        {
            if (!string.IsNullOrWhiteSpace(serviceBusHelper.SharedAccessKeyName) &&
                !string.IsNullOrWhiteSpace(serviceBusHelper.SharedAccessKey))
            {
                return TokenProvider.CreateSharedAccessSignatureTokenProvider(serviceBusHelper.SharedAccessKeyName, serviceBusHelper.SharedAccessKey);
            }
            if (!string.IsNullOrWhiteSpace(serviceBusHelper.IssuerName) &&
                !string.IsNullOrWhiteSpace(serviceBusHelper.IssuerSecret))
            {
                return TokenProvider.CreateSharedSecretTokenProvider(serviceBusHelper.IssuerName, serviceBusHelper.IssuerSecret);
            }
            return null;
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

        public async Task CancelActions()
        {
            if (stopLog != null)
            {
                await stopLog();
            }
            if (managerCancellationTokenSource != null)
            {
                managerCancellationTokenSource.Cancel();
            }
            if (senderCancellationTokenSource != null)
            {
                senderCancellationTokenSource.Cancel();
            }
        }

        public async void btnCancel_Click(object sender, EventArgs e)
        {
            await CancelActions();
            if (OnCancel != null) OnCancel();
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
                    LanguageDetector.SetFormattedMessage(serviceBusHelper, text, txtMessageText);
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

        private void BuildBindingTreeView(object item, string name = null, TreeNode parent = null, int level = 0)
        {
            if (item == null)
            {
                return;
            }
            var type = item.GetType();
            if (!IsComplex(type))
            {
                return;
            }
            var node = level == 0
                           ? bindingTreeView.Nodes.Add(type.Name, type.Name, 0, 0)
                           : parent != null ? parent.Nodes.Add(name, name, 1, 1) : null;
            if (node == null)
            {
                return;
            }
            node.Tag = item;
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var dynamicCustomTypeDescriptor = ProviderInstaller.Install(item);
            foreach (var property in properties)
            {
                if (IsComplex(property.PropertyType))
                {
                    var customPropertyDescriptor = dynamicCustomTypeDescriptor.GetProperty(property.Name);
                    customPropertyDescriptor.SetIsBrowsable(false);
                    if (property.CanRead)
                    {
                        BuildBindingTreeView(property.GetValue(item, null), property.Name, node, ++level);
                    }
                }
            }
            bindingTreeView.ExpandAll();
        }

        private bool IsComplex(Type type)
        {
            if (type == null)
            {
                return false;
            }
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var ok = properties.Any(p => p.CanWrite);
            return ok &&
                   !type.IsPrimitive &&
                   !type.IsEnum &&
                   type != typeof (string) &&
                   type != typeof (TimeSpan) &&
                   type != typeof (DateTime) &&
                   type != typeof (DateTimeOffset);
        }

        private void bindingTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            bindingPropertyGrid.SelectedObject = e.Node.Tag;
        }

        private void cboBinding_TextChanged(object sender, EventArgs e)
        {
            bindingTreeView.Nodes.Clear();
            var type = ServiceBusBindingHelper.Bindings[cboBinding.Text];
            if (type != null)
            {
                binding = Activator.CreateInstance(type) as System.ServiceModel.Channels.Binding;
                ServiceBusBindingHelper.SetSecurityProperties(relayDescription.RequiresClientAuthorization,
                                                              relayDescription.RequiresTransportSecurity,
                                                              ref binding);
                BuildBindingTreeView(binding);
                bindingPropertyGrid.SelectedObject = binding;
            }
        }

        /// <summary>
        /// Updates the statistics and graph on the control.
        /// </summary>
        /// <param name="elapsedMilliseconds">Elapsed time.</param>
        /// <param name="direction">The direction of the I/O operation: Send or Receive.</param>
        private void UpdateStatistics(long elapsedMilliseconds, DirectionType direction)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateStatisticsDelegate(InternalUpdateStatistics), 1, elapsedMilliseconds, direction);
            }
            else
            {
                InternalUpdateStatistics(1, elapsedMilliseconds, direction);
            }
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
                var elapsedSeconds = (double) elapsedMilliseconds/1000;

                if (direction == DirectionType.Send)
                {
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
                    senderAverageTime = senderMessageNumber > 0 ? senderTotalTime/senderMessageNumber : 0;
                    senderMessagesPerSecond = senderTotalTime > 0 ? senderMessageNumber * senderTaskCount/senderTotalTime : 0;

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
                        chart.Series[0].Points.AddY(elapsedSeconds);
                        chart.Update();
                        chart.Series[1].Points.AddY(senderMessagesPerSecond);
                        chart.Update();
                    }
                }
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

        private void headersDataGridView_Resize(object sender, EventArgs e)
        {
            CalculateLastColumnWidth();
        }

        private void headersDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CalculateLastColumnWidth();
        }

        private void headersDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalculateLastColumnWidth();
        }

        private void CalculateLastColumnWidth()
        {
            try
            {
                headersDataGridView.SuspendDrawing();
                headersDataGridView.SuspendLayout();
                if (headersDataGridView.Columns.Count != 3)
                {
                    return;
                }
                var width = headersDataGridView.Width - headersDataGridView.Columns[0].Width -
                            headersDataGridView.Columns[1].Width - headersDataGridView.RowHeadersWidth;
                var verticalScrollbar = headersDataGridView.Controls.OfType<VScrollBar>().First();
                if (verticalScrollbar != null && verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                headersDataGridView.Columns[2].Width = width;
            }
            finally
            {
                headersDataGridView.ResumeLayout();
                headersDataGridView.ResumeDrawing();
            }
        }

        private void grouperBinding_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboBinding.Location.X - 1,
                                    cboBinding.Location.Y - 1,
                                    cboBinding.Size.Width + 1,
                                    cboBinding.Size.Height + 1);
        }

        private void grouperMessageHeaders_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                  headersDataGridView.Location.X - 1,
                                  headersDataGridView.Location.Y - 1,
                                  headersDataGridView.Size.Width + 1,
                                  headersDataGridView.Size.Height + 1);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);

            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            var groupSeparator = numberFormatInfo.NumberGroupSeparator;
            var negativeSign = numberFormatInfo.NegativeSign;

            var keyInput = e.KeyChar.ToString(CultureInfo.InvariantCulture);

            if (char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
                     keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
            }
        }

        private void headersDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

                if (senderCancellationTokenSource != null)
                {
                    senderCancellationTokenSource.Dispose();
                }

                if (managerCancellationTokenSource != null)
                {
                    managerCancellationTokenSource.Dispose();
                }

                if (managerResetEvent != null)
                {
                    managerResetEvent.Dispose();
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

        private void txtMessageText_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMessageText.Text))
            {
                mainForm.MessageText = txtMessageText.Text;
            }
        }

        private void grouperMessageFormat_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   cboMessageFormat.Location.X - 1,
                                   cboMessageFormat.Location.Y - 1,
                                   cboMessageFormat.Size.Width + 1,
                                   cboMessageFormat.Size.Height + 1);
        }

        private void cboMessageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LanguageDetector.SetFormattedMessage(cboMessageFormat.Text, txtMessageText);
        }

        #endregion
    }
}
