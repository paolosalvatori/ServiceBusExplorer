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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus.Messaging;
using FastColoredTextBoxNS;
using ServiceBusExplorer.Utilities.Helpers;

#endregion

namespace ServiceBusExplorer.Forms
{
    public partial class MessageForm : Form
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        const string ExceptionFormat = "Exception: {0}";
        const string InnerExceptionFormat = "InnerException: {0}";

        //***************************
        // Properties & Types
        //***************************
        const string PropertyKey = "Key";
        const string PropertyType = "Type";
        const string PropertyValue = "Value";
        const string StringType = "String";

        //***************************
        // Messages
        //***************************
        const string SelectEntityDialogTitle = "Select a target Queue or Topic";
        const string SelectEntityGrouperTitle = "Send To";
        const string SelectEntityLabelText = "Target Queue or Topic:";
        const string PropertyConversionError = "{0} property conversion error: {1}";
        const string PropertyValueCannotBeNull = "The value of the {0} property cannot be null.";
        const string WarningHeader = "The following validations failed:";
        const string WarningFormat = "\n\r - {0}";
        const string SelectBrokeredMessageInspector = "Select a BrokeredMessage inspector...";
        const string MessageSentMessage = "[{0}] messages were sent to [{1}] in [{2}] milliseconds.";
        const string MessageMovedMessage = "[{0}] messages were moved to [{1}] in [{2}] milliseconds.";

        //***************************
        // Constants
        //***************************
        const string SaveAsTitle = "Save File As";
        const string JsonExtension = "json";
        const string JsonFilter = "JSON Files|*.json|Text Documents|*.txt";
        const string MessageFileFormat = "BrokeredMessage_{0}_{1}.json";
        #endregion

        #region Private Instance Fields
        readonly IEnumerable<BrokeredMessage> brokeredMessages;
        readonly BrokeredMessage brokeredMessage;
        readonly ServiceBusHelper serviceBusHelper;
        readonly WriteToLogDelegate writeToLog;
        readonly BindingSource bindingSource = new BindingSource();
        readonly QueueDescription queueDescription; // Might be null
        readonly SubscriptionWrapper subscriptionWrapper; // Might be null
        #endregion

        #region Private Static Fields

        static readonly List<string> Types = new List<string>
        {
            "Boolean", 
            "Byte", 
            "Int16", 
            "Int32", 
            "Int64", 
            "Single", 
            "Double", 
            "Decimal", 
            "Guid", 
            "DateTime", 
            "TimeSpan", 
            "String", 
            "Char",
            "UInt64",
            "UInt32",
            "UInt16",
            "SByte"
        };
        #endregion

        #region Public Properties
        public List<long> RemovedSequenceNumbers
        {
            get; private set;
        } = new List<long>();
        #endregion

        #region Public Constructors
        public MessageForm(BrokeredMessage brokeredMessage, ServiceBusHelper serviceBusHelper, WriteToLogDelegate writeToLog)
        {
            this.brokeredMessage = brokeredMessage;
            this.serviceBusHelper = serviceBusHelper;
            this.writeToLog = writeToLog;
            InitializeComponent();

            cboBodyType.SelectedIndex = (int)MainForm.SingletonMainForm.MessageBodyType;
            messagePropertyGrid.SelectedObject = brokeredMessage;

            InitializeMessageTextControl(brokeredMessage);

            // Initialize the DataGridView.
            bindingSource.DataSource = new BindingList<MessagePropertyInfo>(brokeredMessage.Properties.Select(p => new MessagePropertyInfo(p.Key,
                                                                                                      GetShortValueTypeName(p.Value),
                                                                                                      p.Value)).ToList());
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
                DataSource = Types,
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

            // Set the row and column header styles.
            propertiesDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            propertiesDataGridView.RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            propertiesDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(215, 228, 242);
            propertiesDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            // Get Brokered Message Inspector classes
            cboSenderInspector.Items.Add(SelectBrokeredMessageInspector);
            cboSenderInspector.SelectedIndex = 0;

            if (serviceBusHelper.BrokeredMessageInspectors == null)
            {
                return;
            }
            foreach (var key in serviceBusHelper.BrokeredMessageInspectors.Keys)
            {
                cboSenderInspector.Items.Add(key);
            }
        }

        public MessageForm(QueueDescription queueDescription, BrokeredMessage brokeredMessage,
            ServiceBusHelper serviceBusHelper, WriteToLogDelegate writeToLog) :
            this(brokeredMessage, serviceBusHelper, writeToLog)
        {
            this.queueDescription = queueDescription;
        }

        public MessageForm(SubscriptionWrapper subscriptionWrapper, BrokeredMessage brokeredMessage,
            ServiceBusHelper serviceBusHelper, WriteToLogDelegate writeToLog) :
            this(brokeredMessage, serviceBusHelper, writeToLog)
        {
            this.subscriptionWrapper = subscriptionWrapper;
        }

        public MessageForm(IEnumerable<BrokeredMessage> brokeredMessages, ServiceBusHelper serviceBusHelper, WriteToLogDelegate writeToLog)
        {
            this.brokeredMessages = brokeredMessages;
            this.serviceBusHelper = serviceBusHelper;
            this.writeToLog = writeToLog;
            InitializeComponent();

            // Make it just a small dialog with the controls on one row
            messagesSplitContainer.Visible = false;
            btnSave.Visible = false;
            btnSubmit.Location = btnSave.Location;
            cboSenderInspector.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            var moveRightInPixels = btnClose.Left - btnSubmit.Left;
            Size = new Size(Size.Width - moveRightInPixels, 80);
            lblBody.Left += moveRightInPixels;
            cboBodyType.Left += moveRightInPixels;
            chkNewMessageId.Left += moveRightInPixels;
            chkRemove.Left += moveRightInPixels;
            cboSenderInspector.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            cboBodyType.SelectedIndex = (int)MainForm.SingletonMainForm.MessageBodyType;

            // Get Brokered Message Inspector classes
            cboSenderInspector.Items.Add(SelectBrokeredMessageInspector);
            cboSenderInspector.SelectedIndex = 0;

            if (serviceBusHelper.BrokeredMessageInspectors == null)
            {
                return;
            }
            foreach (var key in serviceBusHelper.BrokeredMessageInspectors.Keys)
            {
                cboSenderInspector.Items.Add(key);
            }
        }

        public MessageForm(QueueDescription queueDescription, IEnumerable<BrokeredMessage> brokeredMessages,
            ServiceBusHelper serviceBusHelper, WriteToLogDelegate writeToLog) :
            this(brokeredMessages, serviceBusHelper, writeToLog)
        {
            this.queueDescription = queueDescription;
        }

        public MessageForm(SubscriptionWrapper subscriptionWrapper, IEnumerable<BrokeredMessage> brokeredMessages,
            ServiceBusHelper serviceBusHelper, WriteToLogDelegate writeToLog) :
            this(brokeredMessages, serviceBusHelper, writeToLog)
        {
            this.subscriptionWrapper = subscriptionWrapper;
        }
        #endregion

        #region Event Handlers
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
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

        private void MessageForm_Load(object sender, EventArgs e)
        {
            txtMessageText.Focus();
            txtMessageText.SelectionLength = 0;

            if (queueDescription != null || subscriptionWrapper != null)
            {
                chkRemove.Visible = true;
            }
        }

        private void grouperMessageCustomProperties_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     propertiesDataGridView.Location.X - 1,
                                     propertiesDataGridView.Location.Y - 1,
                                     propertiesDataGridView.Size.Width + 1,
                                     propertiesDataGridView.Size.Height + 1);
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

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = CreateSelectEntityForm())
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(form.Path))
                    {
                        return;
                    }

                    Application.UseWaitCursor = true;
                    try
                    {

                        if (!Enum.TryParse<BodyType>(cboBodyType.Text, true, out var bodyType))
                        {
                            bodyType = BodyType.Stream;
                        }
                        var messageSender = serviceBusHelper.MessagingFactory.CreateMessageSender(form.Path);
                        var messages = brokeredMessages != null ?
                                        new List<BrokeredMessage>(brokeredMessages) :
                                        new List<BrokeredMessage>(new[] { brokeredMessage });
                        var outboundMessages = new List<BrokeredMessage>();
                        var sequenceNumbers = new List<long>(); // Only used when removing messages
                        foreach (var message in messages)
                        {
                            BrokeredMessage outboundMessage;
                            if (bodyType == BodyType.Wcf)
                            {
                                var wcfUri = serviceBusHelper.IsCloudNamespace ?
                                                 new Uri(serviceBusHelper.NamespaceUri, messageSender.Path) :
                                                 new UriBuilder
                                                 {
                                                     Host = serviceBusHelper.NamespaceUri.Host,
                                                     Path = $"{serviceBusHelper.NamespaceUri.AbsolutePath}/{messageSender.Path}",
                                                     Scheme = "sb"
                                                 }.Uri;
                                outboundMessage = serviceBusHelper.CreateMessageForWcfReceiver(message.Clone(txtMessageText.Text, messagesSplitContainer.Visible),
                                                                                               0,
                                                                                               false,
                                                                                               false,
                                                                                               wcfUri);
                            }
                            else
                            {
                                if (brokeredMessage != null)
                                {
                                    // For body type ByteArray cloning is not an option. When cloned, supplied body can be only of a string or stream types, but not byte array :(
                                    outboundMessage = bodyType == BodyType.ByteArray ?
                                                      brokeredMessage.CloneWithByteArrayBodyType(txtMessageText.Text, messagesSplitContainer.Visible) :
                                                      brokeredMessage.Clone(txtMessageText.Text, messagesSplitContainer.Visible);
                                }
                                else
                                {
                                    var messageText = serviceBusHelper.GetMessageText(message,
                                        MainForm.SingletonMainForm.UseAscii, out bodyType);

                                    // For body type ByteArray cloning is not an option. When cloned, supplied body can be only of a string or stream types, but not byte array :(
                                    outboundMessage = bodyType == BodyType.ByteArray ?
                                                      message.CloneWithByteArrayBodyType(messageText, messagesSplitContainer.Visible) :
                                                      message.Clone(message.GetBody<Stream>(), messagesSplitContainer.Visible);
                                }

                                outboundMessage = serviceBusHelper.CreateMessageForApiReceiver(outboundMessage,
                                                                                               0,
                                                                                               chkNewMessageId.Checked,
                                                                                               false,
                                                                                               bodyType,
                                                                                               cboSenderInspector.SelectedIndex > 0 ?
                                                                                               Activator.CreateInstance(serviceBusHelper.BrokeredMessageInspectors[cboSenderInspector.Text]) as IBrokeredMessageInspector :
                                                                                               null);
                            }

                            sequenceNumbers.Add(message.SequenceNumber);

                            var warningCollection = new ConcurrentBag<string>();
                            foreach (var messagePropertyInfo in bindingSource.Cast<MessagePropertyInfo>())
                            {
                                try
                                {
                                    if (Constants.AlwaysOmittedProperties.Exists(
                                        omitProp => messagePropertyInfo.Key.Equals(omitProp, StringComparison.InvariantCultureIgnoreCase)))
                                    {
                                        continue;
                                    }
                                    messagePropertyInfo.Key = messagePropertyInfo.Key.Trim();
                                    if (messagePropertyInfo.Type != StringType && messagePropertyInfo.Value == null)
                                    {
                                        warningCollection.Add(string.Format(CultureInfo.CurrentUICulture, PropertyValueCannotBeNull, messagePropertyInfo.Key));
                                    }
                                    else
                                    {
                                        if (outboundMessage.Properties.ContainsKey(messagePropertyInfo.Key))
                                        {
                                            outboundMessage.Properties[messagePropertyInfo.Key] = ConversionHelper.MapStringTypeToCLRType(messagePropertyInfo.Type, messagePropertyInfo.Value);
                                        }
                                        else
                                        {
                                            outboundMessage.Properties.Add(messagePropertyInfo.Key, ConversionHelper.MapStringTypeToCLRType(messagePropertyInfo.Type, messagePropertyInfo.Value));
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    warningCollection.Add(string.Format(CultureInfo.CurrentUICulture, PropertyConversionError, messagePropertyInfo.Key, ex.Message));
                                }
                            }
                            if (warningCollection.Count <= 0)
                            {
                                outboundMessages.Add(outboundMessage);
                                continue;
                            }
                            var builder = new StringBuilder(WarningHeader);
                            var warnings = warningCollection.ToArray<string>();
                            for (var i = 0; i < warningCollection.Count; i++)
                            {
                                builder.AppendFormat(WarningFormat, warnings[i]);
                            }
                            writeToLog(builder.ToString());
                        }
                        if (!outboundMessages.Any())
                        {
                            return;
                        }

                        var sent = outboundMessages.Count;
                        var stopwatch = new Stopwatch();
                        stopwatch.Start();

                        if (chkRemove.Checked)
                        {
                            var messageHandler = CreateDeadLetterMessageHandler();

                            var result = await messageHandler.MoveMessages(messageSender,
                                sequenceNumbers, outboundMessages);
                            RemovedSequenceNumbers = result.DeletedSequenceNumbers;
                            stopwatch.Stop();

                            if (result.TimedOut)
                            {
                                var messageText = messageHandler.GetFailureExplanation(result, outboundMessages.Count, delete: false);
                                writeToLog(messageText);
                                Application.UseWaitCursor = false;
                                MessageBox.Show(messageText, "Not all selected messages were moved",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                writeToLog(string.Format(MessageMovedMessage, result.DeletedSequenceNumbers.Count,
                                    messageSender.Path, stopwatch.ElapsedMilliseconds));
                            }

                            if (null != queueDescription)
                            {
                                if (!messageSender.Path.Equals(queueDescription.Path, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    await MainForm.SingletonMainForm.RefreshServiceBusEntityNode(queueDescription.Path);
                                }
                            }
                            else if (null != subscriptionWrapper?.SubscriptionDescription?.TopicPath)
                            {
                                await MainForm.SingletonMainForm.RefreshServiceBusEntityNode(subscriptionWrapper.SubscriptionDescription.TopicPath);
                            }
                        }
                        else
                        {
                            var messageIndex = 0;
                            try
                            {
                                while (messageIndex < outboundMessages.Count)
                                {
                                    await messageSender.SendAsync(outboundMessages[messageIndex++]);
                                }
                            }
                            catch (Exception exception)
                            {
                                Application.UseWaitCursor = false;
                                var messageText = $"{outboundMessages.Count} were selected but only" +
                                    $" {messageIndex} messages were sent. The error message is: {exception.Message}";
                                MessageBox.Show(messageText, "Not all selected messages were sent",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            stopwatch.Stop();
                            writeToLog(string.Format(MessageSentMessage, sent, messageSender.Path, stopwatch.ElapsedMilliseconds));
                        }

                        await MainForm.SingletonMainForm.RefreshServiceBusEntityNode(messageSender.Path);

                        if (brokeredMessages != null)
                        {
                            Close();
                        }
                    }
                    finally
                    {
                        Application.UseWaitCursor = false;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        DeadLetterMessageHandler CreateDeadLetterMessageHandler()
        {
            if (queueDescription != null)
            {
                if (subscriptionWrapper != null)
                {
                    throw new ArgumentException(
                        "At least one of the arguments queueDescription and subscriptionWrapper must be null.");
                }

                return new DeadLetterMessageHandler(writeToLog, serviceBusHelper,
                                MainForm.SingletonMainForm.ReceiveTimeout, queueDescription);
            }

            if (subscriptionWrapper != null)
            {
                return new DeadLetterMessageHandler(writeToLog, serviceBusHelper,
                                MainForm.SingletonMainForm.ReceiveTimeout, subscriptionWrapper);
            }

            throw new ArgumentException("queueDescription or subscriptionWrapper must be set.");
        }

        SelectEntityForm CreateSelectEntityForm()
        {
            if (queueDescription != null)
            {
                if (subscriptionWrapper != null)
                {
                    throw new ArgumentException(
                        "At least one of the arguments queueDescription and subscriptionWrapper must be null.");
                }

                return new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle,
                    SelectEntityLabelText, queueDescription);
            }

            if (subscriptionWrapper != null)
            {
                return new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle,
                   SelectEntityLabelText, subscriptionWrapper.TopicDescription);
            }

            return new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle,
                 SelectEntityLabelText);
        }

        private void HandleException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }
            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message))
            {
                writeToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }

        private void MessageForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     cboBodyType.Location.X - 1,
                                     cboBodyType.Location.Y - 1,
                                     cboBodyType.Size.Width + 1,
                                     cboBodyType.Size.Height + 1);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     cboSenderInspector.Location.X - 1,
                                     cboSenderInspector.Location.Y - 1,
                                     cboSenderInspector.Size.Width + 1,
                                     cboSenderInspector.Size.Height + 1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessageText.Text))
            {
                return;
            }
            saveFileDialog.Title = SaveAsTitle;
            saveFileDialog.DefaultExt = JsonExtension;
            saveFileDialog.Filter = JsonFilter;
            saveFileDialog.FileName = CreateFileName();
            if (saveFileDialog.ShowDialog() != DialogResult.OK ||
                string.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {
                return;
            }
            if (File.Exists(saveFileDialog.FileName))
            {
                File.Delete(saveFileDialog.FileName);
            }
            using (var writer = new StreamWriter(saveFileDialog.FileName))
            {
                writer.Write(MessageSerializationHelper.Serialize(brokeredMessage, txtMessageText.Text));
            }
        }

        private string CreateFileName()
        {
            return string.Format(MessageFileFormat,
                                 CultureInfo.CurrentCulture.TextInfo.ToTitleCase(serviceBusHelper.Namespace),
                                 DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }

        private void propertiesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        void ChkAutoindent_CheckedChanged(object sender, EventArgs e)
        {
            InitializeMessageTextControl(messagePropertyGrid.SelectedObject as BrokeredMessage);
        }
        #endregion

        #region Private Methods

        string GetShortValueTypeName(object o)
        {
            if (o == null) o = new object();
            var typeName = o.GetType().ToString();
            return typeName.Length > 7 ? typeName.Substring(7) : typeName;
        }

        void InitializeMessageTextControl(BrokeredMessage message)
        {
            var messageText = this.serviceBusHelper.GetMessageText(message,
                 MainForm.SingletonMainForm.UseAscii, out _);

            if (chkAutoindent.Checked && JsonSerializerHelper.IsJson(messageText))
            {
                txtMessageText.Language = Language.JSON;
                txtMessageText.Text = JsonSerializerHelper.Indent(messageText);
            }
            else if (chkAutoindent.Checked && XmlHelper.IsXml(messageText))
            {
                txtMessageText.Language = Language.HTML;
                txtMessageText.Text = XmlHelper.Indent(messageText);
            }
            else
            {
                txtMessageText.Language = Language.Custom;
                txtMessageText.Text = messageText;
            }
        }

        #endregion
    }
}
