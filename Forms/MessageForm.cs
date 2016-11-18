#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class MessageForm : Form
    {
        #region Private Constants
        //***************************
        // Formats
        //***************************
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";

        //***************************
        // Properties & Types
        //***************************
        private const string PropertyKey = "Key";
        private const string PropertyType = "Type";
        private const string PropertyValue = "Value";
        private const string StringType = "String";

        //***************************
        // Messages
        //***************************
        private const string SelectEntityDialogTitle = "Select a target Queue or Topic";
        private const string SelectEntityGrouperTitle = "Send To";
        private const string SelectEntityLabelText = "Target Queue or Topic:";
        private const string PropertyConversionError = "{0} property conversion error: {1}";
        private const string PropertyValueCannotBeNull = "The value of the {0} property cannot be null.";
        private const string WarningHeader = "The following validations failed:";
        private const string WarningFormat = "\n\r - {0}";

        //***************************
        // Constants
        //***************************
        private const string SaveAsTitle = "Save File As";
        private const string XmlExtension = "xml";
        private const string XmlFilter = "XML Files|*.xml|Text Documents|*.txt";
        #endregion

        #region Private Instance Fields
        private readonly BrokeredMessage brokeredMessage;
        private readonly ServiceBusHelper serviceBusHelper;
        private readonly WriteToLogDelegate writeToLog;
        private readonly BindingSource bindingSource = new BindingSource();
        #endregion

        #region Private Static Fields
        private static readonly List<string> types = new List<string> { "Boolean", "Byte", "Int16", "Int32", "Int64", "Single", "Double", "Decimal", "Guid", "DateTime", "String" };
        #endregion

        #region Public Constructor
        public MessageForm(BrokeredMessage brokeredMessage, ServiceBusHelper serviceBusHelper, WriteToLogDelegate writeToLog)
        {
            this.brokeredMessage = brokeredMessage;
            this.serviceBusHelper = serviceBusHelper;
            this.writeToLog = writeToLog;
            InitializeComponent();

            cboBodyType.SelectedIndex = 0;

            messagePropertyGrid.SelectedObject = brokeredMessage;
            BodyType bodyType;
            txtMessageText.Text = XmlHelper.Indent(serviceBusHelper.GetMessageText(brokeredMessage, out bodyType));

            // Initialize the DataGridView.
            bindingSource.DataSource = new BindingList<MessagePropertyInfo>(brokeredMessage.Properties.Select(p => new MessagePropertyInfo(p.Key, 
                                                                                                      p.Value.GetType().ToString().Substring(7),
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
                if (verticalScrollbar.Visible)
                {
                    width -= verticalScrollbar.Width;
                }
                propertiesDataGridView.Columns[2].Width = width;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new SelectEntityForm(SelectEntityDialogTitle, SelectEntityGrouperTitle, SelectEntityLabelText))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(form.Path))
                    {
                        return;
                    }
                    BodyType bodyType;
                    if (!Enum.TryParse(cboBodyType.Text, true, out bodyType))
                    {
                        bodyType = BodyType.Stream;
                    }
                    var messageSender = serviceBusHelper.MessagingFactory.CreateMessageSender(form.Path);
                    BrokeredMessage outboundMessage;
                    if (bodyType == BodyType.Wcf)
                    {
                        var wcfUri = serviceBusHelper.IsCloudNamespace ?
                                         new Uri(serviceBusHelper.NamespaceUri, messageSender.Path) :
                                         new UriBuilder
                                         {
                                             Host = serviceBusHelper.NamespaceUri.Host,
                                             Path = string.Format("{0}/{1}", serviceBusHelper.NamespaceUri.AbsolutePath, messageSender.Path),
                                             Scheme = "sb"
                                         }.Uri;
                        outboundMessage = serviceBusHelper.CreateMessageForWcfReceiver(brokeredMessage,
                                                                                       0,
                                                                                       false,
                                                                                       false,
                                                                                       txtMessageText.Text,
                                                                                       wcfUri);
                    }
                    else
                    {
                        outboundMessage = serviceBusHelper.CreateMessageForApiReceiver(brokeredMessage,
                                                                                       0,
                                                                                       false,
                                                                                       false,
                                                                                       txtMessageText.Text,
                                                                                       bodyType);
                    }
                    outboundMessage.Properties.Clear();
                    var warningCollection = new ConcurrentBag<string>();
                    foreach (var messagePropertyInfo in bindingSource.Cast<MessagePropertyInfo>())
                    {
                        try
                        {
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
                    if (warningCollection.Count > 0)
                    {
                        var builder = new StringBuilder(WarningHeader);
                        var warnings = warningCollection.ToArray<string>();
                        for (var i = 0; i < warningCollection.Count; i++)
                        {
                            builder.AppendFormat(WarningFormat, warnings[i]);
                        }
                        writeToLog(builder.ToString());
                    }

                    long elapsedMilliseconds;
                    serviceBusHelper.SendMessage(messageSender,
                                                 outboundMessage,
                                                 txtMessageText.Text,
                                                 0,
                                                 bodyType == BodyType.Wcf,
                                                 true,
                                                 true,
                                                 out elapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
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
        
        private void MessageForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     cboBodyType.Location.X - 1,
                                     cboBodyType.Location.Y - 1,
                                     cboBodyType.Size.Width + 1,
                                     cboBodyType.Size.Height + 1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessageText.Text))
            {
                return;
            }
            saveFileDialog.Title = SaveAsTitle;
            saveFileDialog.DefaultExt = XmlExtension;
            saveFileDialog.Filter = XmlFilter;
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
                writer.Write(txtMessageText.Text);
            }
        }

        private string CreateFileName()
        {
            return string.Format("BrokeredMessage_{0}_{1}.xml",
                                 CultureInfo.CurrentCulture.TextInfo.ToTitleCase(serviceBusHelper.Namespace),
                                 DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace('/', '-').Replace(':', '-'));
        }
        #endregion
    }
}