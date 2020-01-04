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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Forms
{
    using Enums;
    using ServiceBusExplorer.UIHelpers;
    using ServiceBusExplorer.Utilities.Helpers;

    public sealed partial class ContainerForm : Form
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string DateFormat = "<{0,2:00}:{1,2:00}:{2,2:00}> {3}";
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string SendMessagesFormat = "Send messages to {0}";
        private const string SendEventsToEventHubFormat = "Send events to {0}";
        private const string SendEventsToEventHubPartitionFormat = "Send events to partition {0} of {1}";
        private const string TestQueueFormat = "Test queue {0} in MDI mode";
        private const string TestTopicFormat = "Test topic {0} in MDI mode";
        private const string TestSubscriptionFormat = "Test subscription {0} in MDI mode";
        private const string TestRelayFormat = "Test relay {0} in MDI mode";
        private const string HeaderTextTestQueueFormat = "Test Queue: {0}";
        private const string HeaderTextTestTopicFormat = "Test Topic: {0}";
        private const string HeaderTextTestSubscriptionFormat = "Test Subscription: {0}";
        private const string HeaderTextTestEventHubFormat = "Test Event Hub: {0}";
        private const string HeaderTextTestEventHubPartitionFormat = "Test Partition: {0} of Event Hub: {1}";
        private const string HeaderTextTestRelayFormat = "Test Relay: {0}";
        private const string LogFileNameFormat = "ServiceBusExplorer {0}.txt";
        private const string QueueListenerFormat = "Listener for queue {0}";
        private const string SubscriptionListenerFormat = "Listener for subscription {0}";
        private const string HeaderTextQueueListenerFormat = "Queue Listener: {0}";
        private const string HeaderTextSubscriptionListenerFormat = "Subscription Listener: {0}";
        private const string ConsumerGroupListenerFormat = "Listener for {0}.{1} Consumer Group";
        private const string PartitionListenerFormat = "Listener for Partition {0} of {1}.{2} Consumer Group";
        private const string IoTHubListenerFormat = "Listener for Consumer Group {0} of IoT HUb {1}";
        private const string HeaderTextConsumerGroupListenerFormat = "Consumer Group Listener: {0}.{1}";
        private const string HeaderTextPartitionListenerFormat = "Partition Listener: {0}.{1}[{2}]";
        private const string HeaderTextIoTHubListenerFormat = "IoT Hub Listener: {0}.{1}";

        //***************************
        // Constants
        //***************************
        private const string CloseLabel = "Close";
        private const string SaveAsTitle = "Save Log As";
        private const string SaveAsExtension = "txt";
        private const string SaveAsFilter = "Text Documents|*.txt";

        //***************************
        // Sizes
        //***************************
        private const int ControlMinWidth = 816;
        private const int ControlMinHeight = 345;
        #endregion

        #region Private Fields
        private readonly MainForm mainForm;
        private readonly TestQueueControl testQueueControl;
        private readonly TestTopicControl testTopicControl;
        private readonly TestSubscriptionControl testSubscriptionControl;
        private readonly TestEventHubControl testEventHubControl;
        private readonly TestRelayControl testRelayControl;
        private readonly LogTraceListener logTraceListener;
        private readonly int mainSplitterDistance;
        private BlockingCollection<string> logCollection = new BlockingCollection<string>();
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        // ReSharper disable once NotAccessedField.Local
        private Task logTask;
        #endregion

        #region Public Constructors
        public ContainerForm(ServiceBusHelper serviceBusHelper, MainForm mainForm, FormTypeEnum formType, QueueDescription queueDescription)
        {
            try
            {
                InitializeComponent();
                Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        WriteToLog(t.Exception.Message);
                    }
                });
                this.mainForm = mainForm;
                mainSplitterDistance = mainSplitContainer.SplitterDistance;
                SuspendLayout();
                panelMain.SuspendDrawing();
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;

                if (formType == FormTypeEnum.Listener)
                {
                    var listenerControl = new ListenerControl(WriteToLog, StopLog, StartLog, new ServiceBusHelper(WriteToLog, serviceBusHelper), queueDescription)
                    {
                        Location = new Point(1, panelMain.HeaderHeight + 1),
                        Size = new Size(panelMain.Size.Width - 3, queueDescription.RequiresSession ? 544 : 520),
                        Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    listenerControl.Focus();

                    Text = string.Format(QueueListenerFormat, queueDescription.Path);
                    mainSplitContainer.SplitterDistance = queueDescription.RequiresSession ? 570 : listenerControl.Size.Height + 26;
                    panelMain.HeaderText = string.Format(HeaderTextQueueListenerFormat, queueDescription.Path);
                    panelMain.Controls.Add(listenerControl);
                }
                else
                {
                    testQueueControl = new TestQueueControl(mainForm, WriteToLog, StopLog, StartLog, new ServiceBusHelper(WriteToLog, serviceBusHelper), queueDescription)
                                           {
                                               Location = new Point(1, panelMain.HeaderHeight + 1),
                                               Size = new Size(panelMain.Size.Width - 3, panelMain.Size.Height - 26),
                                               Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                                           };


                    if (formType == FormTypeEnum.Send)
                    {
                        testQueueControl.mainTabControl.TabPages.RemoveAt(2);
                        testQueueControl.receiverEnabledCheckBox.Checked = false;
                        testQueueControl.senderEnabledCheckBox.Checked = true;
                        testQueueControl.senderEnabledCheckBox.Visible = false;
                        testQueueControl.grouperMessage.Location = new Point(testQueueControl.grouperMessage.Location.X, 8);
                        testQueueControl.grouperMessage.Size = new Size(testQueueControl.grouperMessage.Size.Width, 
                                                                        testQueueControl.grouperMessage.Size.Height + 16);
                        testQueueControl.grouperSender.Location = new Point(testQueueControl.grouperSender.Location.X, 8);
                        testQueueControl.grouperSender.Size = new Size(testQueueControl.grouperSender.Size.Width,
                                                                       testQueueControl.grouperSender.Size.Height + 16);
                        Text = string.Format(SendMessagesFormat, queueDescription.Path);
                    }
                    else
                    {
                        Text = string.Format(TestQueueFormat, queueDescription.Path);
                        logTraceListener = new LogTraceListener(WriteToLog);
                        Trace.Listeners.Add(logTraceListener);
                    }

                    testQueueControl.btnCancel.Text = CloseLabel;
                    testQueueControl.btnCancel.Click -= testQueueControl.btnCancel_Click;
                    testQueueControl.btnCancel.Click += BtnCancelOnClick;
                    testQueueControl.Focus();

                    panelMain.HeaderText = string.Format(HeaderTextTestQueueFormat, queueDescription.Path);
                    panelMain.Controls.Add(testQueueControl);
                }
                SetStyle(ControlStyles.ResizeRedraw, true);
            }
            finally
            {
                panelMain.ResumeDrawing();
                ResumeLayout();
            }
        }

        public ContainerForm(ServiceBusHelper serviceBusHelper, MainForm mainForm, FormTypeEnum formType, TopicDescription topicDescription, List<SubscriptionDescription> subscriptionList)
        {
            try
            {
                InitializeComponent();
                Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        WriteToLog(t.Exception.Message);
                    }
                });
                this.mainForm = mainForm;
                mainSplitterDistance = mainSplitContainer.SplitterDistance;
                SuspendLayout();
                panelMain.SuspendDrawing();
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;

                testTopicControl = new TestTopicControl(mainForm, WriteToLog, StopLog, StartLog, new ServiceBusHelper(WriteToLog, serviceBusHelper), topicDescription, subscriptionList)
                                       {
                                           Location = new Point(1, panelMain.HeaderHeight + 1),
                                           Size = new Size(panelMain.Size.Width - 3, panelMain.Size.Height - 26),
                                           Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                                       };


                if (formType == FormTypeEnum.Send)
                {
                    testTopicControl.mainTabControl.TabPages.RemoveAt(2);
                    testTopicControl.receiverEnabledCheckBox.Checked = false;
                    testTopicControl.senderEnabledCheckBox.Checked = true;
                    testTopicControl.senderEnabledCheckBox.Visible = false;
                    testTopicControl.grouperMessage.Location = new Point(testTopicControl.grouperMessage.Location.X, 8);
                    testTopicControl.grouperMessage.Size = new Size(testTopicControl.grouperMessage.Size.Width,
                                                                    testTopicControl.grouperMessage.Size.Height + 16);
                    testTopicControl.grouperSender.Location = new Point(testTopicControl.grouperSender.Location.X, 8);
                    testTopicControl.grouperSender.Size = new Size(testTopicControl.grouperSender.Size.Width,
                                                                   testTopicControl.grouperSender.Size.Height + 16);
                    Text = string.Format(SendMessagesFormat, topicDescription.Path);
                }
                else
                {
                    Text = string.Format(TestTopicFormat, topicDescription.Path);
                    logTraceListener = new LogTraceListener(WriteToLog);
                    Trace.Listeners.Add(logTraceListener);
                }

                testTopicControl.btnCancel.Text = CloseLabel;
                testTopicControl.btnCancel.Click -= testTopicControl.btnCancel_Click;
                testTopicControl.btnCancel.Click += BtnCancelOnClick;
                testTopicControl.Focus();

                panelMain.HeaderText = string.Format(HeaderTextTestTopicFormat, topicDescription.Path);
                panelMain.Controls.Add(testTopicControl);
                SetStyle(ControlStyles.ResizeRedraw, true);
            }
            finally
            {
                panelMain.ResumeDrawing();
                ResumeLayout();
            }
        }

        public ContainerForm(ServiceBusHelper serviceBusHelper, MainForm mainForm, FormTypeEnum formType, SubscriptionWrapper subscriptionWrapper)
        {
            try
            {
                InitializeComponent();
                Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        WriteToLog(t.Exception.Message);
                    }
                });
                this.mainForm = mainForm;
                mainSplitterDistance = mainSplitContainer.SplitterDistance;
                SuspendLayout();
                panelMain.SuspendDrawing();
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;

                if (formType == FormTypeEnum.Listener)
                {
                    var listenerControl = new ListenerControl(WriteToLog, StopLog, StartLog, new ServiceBusHelper(WriteToLog, serviceBusHelper), subscriptionWrapper.SubscriptionDescription)
                    {
                        Location = new Point(1, panelMain.HeaderHeight + 1),
                        Size = new Size(panelMain.Size.Width - 3, subscriptionWrapper.SubscriptionDescription.RequiresSession ? 544 : 520),
                        Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };
                    listenerControl.Focus();

                    Text = string.Format(SubscriptionListenerFormat, subscriptionWrapper.SubscriptionDescription.Name);
                    mainSplitContainer.SplitterDistance = subscriptionWrapper.SubscriptionDescription.RequiresSession ? 570 : listenerControl.Size.Height + 26;
                    panelMain.HeaderText = string.Format(HeaderTextSubscriptionListenerFormat, subscriptionWrapper.SubscriptionDescription.Name);
                    panelMain.Controls.Add(listenerControl);
                }
                else
                {
                    testSubscriptionControl = new TestSubscriptionControl(mainForm, WriteToLog, StopLog, StartLog, new ServiceBusHelper(WriteToLog, serviceBusHelper), subscriptionWrapper)
                    {
                        Location = new Point(1, panelMain.HeaderHeight + 1),
                        Size = new Size(panelMain.Size.Width - 3, panelMain.Size.Height - 26),
                        Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                    };

                    testSubscriptionControl.btnCancel.Click -= testSubscriptionControl.btnCancel_Click;
                    testSubscriptionControl.btnCancel.Click += BtnCancelOnClick;
                    testSubscriptionControl.Focus();

                    Text = string.Format(TestSubscriptionFormat, subscriptionWrapper.SubscriptionDescription.Name);

                    panelMain.HeaderText = string.Format(HeaderTextTestSubscriptionFormat, subscriptionWrapper.SubscriptionDescription.Name);
                    panelMain.Controls.Add(testSubscriptionControl);
                }
                SetStyle(ControlStyles.ResizeRedraw, true);
            }
            finally
            {
                panelMain.ResumeDrawing();
                ResumeLayout();
            }
        }

        public ContainerForm(ServiceBusHelper serviceBusHelper, MainForm mainForm, EventHubDescription eventHubDescription, PartitionDescription partitionDescription = null)
        {
            try
            {
                InitializeComponent();
                Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        WriteToLog(t.Exception.Message);
                    }
                });
                this.mainForm = mainForm;
                mainSplitterDistance = mainSplitContainer.SplitterDistance;
                SuspendLayout();
                panelMain.SuspendDrawing();
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;

                testEventHubControl = new TestEventHubControl(mainForm, WriteToLog, StopLog, StartLog, new ServiceBusHelper(WriteToLog, serviceBusHelper), eventHubDescription, partitionDescription)
                {
                    Location = new Point(1, panelMain.HeaderHeight + 1),
                    Size = new Size(panelMain.Size.Width - 3, panelMain.Size.Height - 26),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Text = partitionDescription == null
                    ? string.Format(SendEventsToEventHubFormat, eventHubDescription.Path)
                    : string.Format(SendEventsToEventHubPartitionFormat,
                        partitionDescription.PartitionId,
                        eventHubDescription.Path);

                testEventHubControl.btnCancel.Text = CloseLabel;
                testEventHubControl.btnCancel.Click -= testEventHubControl.btnCancel_Click;
                testEventHubControl.btnCancel.Click += BtnCancelOnClick;
                testEventHubControl.Focus();

                panelMain.HeaderText = partitionDescription == null ?
                                       string.Format(HeaderTextTestEventHubFormat, eventHubDescription.Path) :
                                       string.Format(HeaderTextTestEventHubPartitionFormat, 
                                                     partitionDescription.PartitionId, 
                                                     eventHubDescription.Path);

                panelMain.Controls.Add(testEventHubControl);
                SetStyle(ControlStyles.ResizeRedraw, true);
            }
            finally
            {
                panelMain.ResumeDrawing();
                ResumeLayout();
            }
        }

        public ContainerForm(ServiceBusHelper serviceBusHelper, MainForm mainForm, ConsumerGroupDescription consumerGroupDescription, IEnumerable<PartitionDescription> partitionDescriptions)
        {
            try
            {
                var descriptions = partitionDescriptions as IList<PartitionDescription> ?? partitionDescriptions.ToList();
                if (!descriptions.Any())
                {
                    return;
                }
                InitializeComponent();
                Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        WriteToLog(t.Exception.Message);
                    }
                });
                this.mainForm = mainForm;
                mainSplitterDistance = mainSplitContainer.SplitterDistance;
                SuspendLayout();
                panelMain.SuspendDrawing();
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;

                var partitionListenerControl = new PartitionListenerControl(WriteToLog, StopLog, StartLog, new ServiceBusHelper(WriteToLog, serviceBusHelper), consumerGroupDescription, descriptions)
                {
                    Location = new Point(1, panelMain.HeaderHeight + 1),
                    Size = new Size(panelMain.Size.Width - 3, panelMain.Size.Height - 26),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                if (descriptions.Count == 1)
                {
                    Text = string.Format(PartitionListenerFormat, descriptions[0].PartitionId, consumerGroupDescription.EventHubPath, consumerGroupDescription.Name);
                    panelMain.HeaderText = string.Format(HeaderTextPartitionListenerFormat, consumerGroupDescription.EventHubPath, consumerGroupDescription.Name, descriptions[0].PartitionId);
                }
                else
                {
                    Text = string.Format(ConsumerGroupListenerFormat, consumerGroupDescription.EventHubPath, consumerGroupDescription.Name);
                    panelMain.HeaderText = string.Format(HeaderTextConsumerGroupListenerFormat, consumerGroupDescription.EventHubPath, consumerGroupDescription.Name);
                }                
                partitionListenerControl.Focus();
                panelMain.Controls.Add(partitionListenerControl);
                SetStyle(ControlStyles.ResizeRedraw, true);
            }
            finally
            {
                panelMain.ResumeDrawing();
                ResumeLayout();
            }
        }

        public ContainerForm(MainForm mainForm, string connectionString, string hubName, string consumerGroup, bool iotHub)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(consumerGroup))
                {
                    return;
                }
                InitializeComponent();
                Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        WriteToLog(t.Exception.Message);
                    }
                });
                this.mainForm = mainForm;
                mainSplitterDistance = mainSplitContainer.SplitterDistance;
                SuspendLayout();
                panelMain.SuspendDrawing();
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;

                var partitionListenerControl = new PartitionListenerControl(WriteToLog, StopLog, StartLog, connectionString, hubName, consumerGroup)
                {
                    Location = new Point(1, panelMain.HeaderHeight + 1),
                    Size = new Size(panelMain.Size.Width - 3, panelMain.Size.Height - 26),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                if (iotHub)
                {
                    var match = Regex.Match(connectionString, "HostName=([A-Za-z0-9_-]+)", RegexOptions.IgnoreCase);
                    var ioTHubName = match.Success ? match.Groups[1].Value : string.Empty;
                    Text = string.Format(IoTHubListenerFormat, consumerGroup, ioTHubName);
                    panelMain.HeaderText = string.Format(HeaderTextIoTHubListenerFormat, ioTHubName, consumerGroup);
                }
                else
                {
                    Text = string.Format(ConsumerGroupListenerFormat, consumerGroup, hubName);
                    panelMain.HeaderText = string.Format(HeaderTextConsumerGroupListenerFormat, hubName, consumerGroup);
                }
                partitionListenerControl.Focus();
                panelMain.Controls.Add(partitionListenerControl);
                SetStyle(ControlStyles.ResizeRedraw, true);
            }
            finally
            {
                panelMain?.ResumeDrawing();
                ResumeLayout();
            }
        }

        public ContainerForm(ServiceBusHelper serviceBusHelper, MainForm mainForm, RelayDescription relayDescription)
        {
            try
            {
                InitializeComponent();
                Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        WriteToLog(t.Exception.Message);
                    }
                });
                this.mainForm = mainForm;
                mainSplitterDistance = mainSplitContainer.SplitterDistance;
                SuspendLayout();
                panelMain.SuspendDrawing();
                panelMain.Controls.Clear();
                panelMain.BackColor = SystemColors.GradientInactiveCaption;

                testRelayControl = new TestRelayControl(mainForm, WriteToLog, StopLog, StartLog, relayDescription, new ServiceBusHelper(WriteToLog, serviceBusHelper))
                {
                    Location = new Point(1, panelMain.HeaderHeight + 1),
                    Size = new Size(panelMain.Size.Width - 3, panelMain.Size.Height - 26),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Text = string.Format(TestRelayFormat, relayDescription.Path);

                testRelayControl.btnCancel.Text = CloseLabel;
                testRelayControl.btnCancel.Click -= testRelayControl.btnCancel_Click;
                testRelayControl.btnCancel.Click += BtnCancelOnClick;
                testRelayControl.Focus();

                panelMain.HeaderText = string.Format(HeaderTextTestRelayFormat, relayDescription.Path);

                panelMain.Controls.Add(testRelayControl);
                SetStyle(ControlStyles.ResizeRedraw, true);
            }
            finally
            {
                panelMain.ResumeDrawing();
                ResumeLayout();
            }
        }
        #endregion

        #region Public Methods
        public void Clear()
        {
            lstLog.Items.Clear();
        }
        #endregion

        #region Private Methods
        private async void BtnCancelOnClick(object sender, EventArgs eventArgs)
        {
            if (testQueueControl != null)
            {
                await testQueueControl.CancelActions();
            }
            if (testTopicControl != null)
            {
                await testTopicControl.CancelActions();
            }
            if (testSubscriptionControl != null)
            {
                await testSubscriptionControl.CancelActions();
            }
            if (testEventHubControl != null)
            {
                await testEventHubControl.CancelActions();
            }
            if (testRelayControl != null)
            {
                await testRelayControl.CancelActions();
            }
            Close();
        }

        private void SetControlSize(Control control)
        {
            var ok = false;
            if (panelMain.Controls.Count > 0)
            {
                try
                {
                    if (control == null)
                    {
                        control = panelMain.Controls[0];
                        control.SuspendDrawing();
                        ok = true;
                    }
                    var width = panelMain.Width - 4;
                    var height = panelMain.Height - 26;
                    control.Width = width < ControlMinWidth ? ControlMinWidth : width;
                    control.Height = height < ControlMinHeight ? ControlMinHeight : height;
                }
                finally
                {
                    if (ok)
                    {
                        control.ResumeDrawing();
                    }
                }
            }
        }

        private void ContainerForm_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendDrawing();
        }

        private void ContainerForm_ResizeEnd(object sender, EventArgs e)
        {
            this.ResumeDrawing();
        }

        private void ContainerForm_SizeChanged(object sender, EventArgs e)
        {
            var changingUI = false;
            try
            {
                changingUI = true;
                panelMain.SuspendLayout();
                panelMain.SuspendDrawing();
                SetControlSize(null);
            }
            catch (Exception ex)
            {
                mainForm?.HandleException(ex);
            }
            finally
            {
                if (changingUI)
                {
                    panelMain.ResumeDrawing();
                    panelMain.ResumeLayout();
                }
            }
        }

        private Task StopLog()
        {
            cancellationTokenSource.Cancel();
            return Task.FromResult(true);
        }

        private void StartLog()
        {
            if (logCollection != null)
            {
                logCollection.Dispose();
            }
            logCollection = new BlockingCollection<string>();
            cancellationTokenSource = new CancellationTokenSource();
            logTask = Task.Factory.StartNew(AsyncWriteToLog).ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    WriteToLog(t.Exception.Message);
                }
            });
        }

        private async void AsyncWriteToLog()
        {
            try
            {
                var count = 1;
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    var ok = logCollection.TryTake(out var message, 100);
                    if (!ok)
                    {
                        continue;
                    }
                    count = (count + 1) % 10;
                    if (count == 0)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(5));
                    }
                    if (InvokeRequired)
                    {
                        Invoke(new Action<string>(InternalWriteToLog), new object[] { message });
                    }
                    else
                    {
                        InternalWriteToLog(message);
                    }
                }
            }
// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
            
// ReSharper disable FunctionNeverReturns
        }
// ReSharper restore FunctionNeverReturns

        private void WriteToLog(string message, bool async = true)
        {
            if (async)
            {
                logCollection.Add(message);
            }
            else
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(InternalWriteToLog), new object[] { message });
                }
                else
                {
                    InternalWriteToLog(message);
                }
            }
        }

        private void InternalWriteToLog(string message)
        {
            lock (this)
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    var lines = message.Split('\n');
                    var objNow = DateTime.Now;
                    var space = new string(' ', 11);

                    for (var i = 0; i < lines.Length; i++)
                    {
                        if (i == 0)
                        {
                            var line = string.Format(DateFormat,
                                                        objNow.Hour,
                                                        objNow.Minute,
                                                        objNow.Second,
                                                        lines[i]);
                            lstLog.Items.Add(line);
                        }
                        else
                        {
                            lstLog.Items.Add(space + lines[i]);
                        }
                    }
                    lstLog.SelectedIndex = lstLog.Items.Count - 1;
                    lstLog.SelectedIndex = -1;
                }
            }
        }

        private void ContainerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationTokenSource.Cancel(false);
            foreach (var userControl in panelMain.Controls.OfType<UserControl>())
            {
                userControl.Dispose();
            }
            if (logTraceListener != null &&
                Trace.Listeners.Contains(logTraceListener))
            {
                Trace.Listeners.Remove(logTraceListener);
            }
        }

        private void HandleException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }
            WriteToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                WriteToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }

        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            var changingUI = false;
            try
            {
                changingUI = true;
                panelMain.SuspendLayout();
                panelMain.SuspendDrawing();
                SetControlSize(null);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                if (changingUI)
                {
                    panelMain.ResumeDrawing();
                    panelMain.ResumeLayout();
                }
            }
        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        /// <summary>
        /// Saves the log to a text file
        /// </summary>
        /// <param name="sender">MainForm object</param>
        /// <param name="e">System.EventArgs parameter</param>
        private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               SaveLog(true);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void lstLog_Leave(object sender, EventArgs e)
        {
            lstLog.SelectedIndex = -1;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new AboutForm())
            {
                form.ShowDialog();
            }
        }

        private void setDefaultLayouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainSplitContainer.SplitterDistance = mainSplitterDistance;
        }

        private void logWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem != null)
            {
                mainSplitContainer.Panel2Collapsed = !toolStripMenuItem.Checked;
                mainSplitContainer_SplitterMoved(this, null);
            }
        }

        private void lstLog_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Control || e.KeyCode != Keys.C)
                {
                    return;
                }
                var builder = new StringBuilder();
                foreach (var item in lstLog.SelectedItems)
                {
                    builder.AppendLine(item.ToString());
                }
                if (builder.Length > 0)
                {
                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var builder = new StringBuilder();
                foreach (var item in lstLog.Items)
                {
                    builder.AppendLine(item.ToString());
                }
                if (builder.Length > 0)
                {
                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void copySelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var builder = new StringBuilder();
                foreach (var item in lstLog.SelectedItems)
                {
                    builder.AppendLine(item.ToString());
                }
                if (builder.Length > 0)
                {
                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private void clearSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var builder = new StringBuilder();
                var list = new List<object>();
                list.AddRange(lstLog.SelectedItems.Cast<object>().ToArray());
                foreach (var item in list)
                {
                    lstLog.Items.Remove(item);
                }
                if (builder.Length > 0)
                {
                    Clipboard.SetText(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLog(true);
        }

        private void saveSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveLog(false);
        }

        private void SaveLog(bool all)
        {
            try
            {
                saveFileDialog.Title = SaveAsTitle;
                saveFileDialog.DefaultExt = SaveAsExtension;
                saveFileDialog.Filter = SaveAsFilter;
                saveFileDialog.FileName = string.Format(LogFileNameFormat, DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
                if (saveFileDialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    return;
                }
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    if (all)
                    {
                        foreach (var t in lstLog.Items)
                        {
                            writer.WriteLine(t as string);
                        }
                    }
                    else
                    {
                        foreach (var t in lstLog.SelectedItems)
                        {
                            writer.WriteLine(t.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        #endregion      
    }
}
