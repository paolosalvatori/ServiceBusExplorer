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
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.ServiceBus;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class OptionForm : Form
    {
        #region Private Constants
        //***************************
        // Messages
        //***************************
        private const string MessageTextTitle = "Message Text";
        #endregion 

        #region Public Constructor
        public OptionForm(string subscriptionId,
                          string certificateThumbprint,
                          string label,
                          string messageFile,
                          string messageText,
                          decimal logFontSize, 
                          decimal treeViewFontSize, 
                          int retryCount,
                          int retryTimeout,
                          int receiveTimeout,
                          int serverTimeout,
                          int senderThinkTime,
                          int receiverThinkTime,
                          int monitorRefreshInterval,
                          int prefetchCount,
                          int top,
                          bool showMessageCount,
                          bool saveMessageToFile,
                          bool savePropertiesToFile,
                          bool saveCheckpointsToFile,
                          IEnumerable<string> entities,
                          IEnumerable<string> selectedEntities)
        {
            InitializeComponent();

            SubscriptionId = subscriptionId;
            CertificateThumbprint = certificateThumbprint;
            Label = label;
            MessageFile = messageFile;
            MessageText = messageText;
            LogFontSize = logFontSize;
            TreeViewFontSize = treeViewFontSize;
            RetryCount = retryCount;
            RetryTimeout = retryTimeout;
            ReceiveTimeout = receiveTimeout;
            ServerTimeout = serverTimeout;
            SenderThinkTime = senderThinkTime;
            ReceiverThinkTime = receiverThinkTime;
            MonitorRefreshInterval = monitorRefreshInterval;            
            PrefetchCount = prefetchCount;
            TopCount = top;

            txtSubscriptionId.Text = subscriptionId;
            txtManagementCertificateThumbprint.Text = certificateThumbprint;
            txtLabel.Text = label;
            txtMessageFile.Text = messageFile;
            txtMessageText.Text = messageText;
            logNumericUpDown.Value = logFontSize;
            treeViewNumericUpDown.Value = treeViewFontSize;
            retryCountNumericUpDown.Value = retryCount;
            retryTimeoutNumericUpDown.Value = retryTimeout;
            receiveTimeoutNumericUpDown.Value = receiveTimeout;
            serverTimeoutNumericUpDown.Value = serverTimeout;
            senderThinkTimeNumericUpDown.Value = senderThinkTime;
            receiverThinkTimeNumericUpDown.Value = receiverThinkTime;
            monitorRefreshIntervalNumericUpDown.Value = monitorRefreshInterval;
            prefetchCountNumericUpDown.Value = prefetchCount;
            topNumericUpDown.Value = top;
            saveMessageToFileCheckBox.Checked = saveMessageToFile;
            savePropertiesToFileCheckBox.Checked = savePropertiesToFile;
            saveMessageToFileCheckBox.Checked = saveMessageToFile;
            saveCheckpointsToFileCheckBox.Checked = saveCheckpointsToFile;

            var connectivityMode = ServiceBusHelper.ConnectivityMode;
            cboConnectivityMode.DataSource = Enum.GetValues(typeof(ConnectivityMode));
            cboConnectivityMode.SelectedItem = connectivityMode;

            var encodingType = ServiceBusHelper.EncodingType;
            cboEncodingType.DataSource = Enum.GetValues(typeof(EncodingType));
            cboEncodingType.SelectedItem = encodingType;

            ShowMessageCount = showMessageCount;
            SaveMessageToFile = saveMessageToFile;
            SavePropertiesToFile = savePropertiesToFile;
            SaveCheckpointsToFile = saveCheckpointsToFile;

            foreach (var item in entities)
            {
                cboSelectedEntities.Items.Add(item);
            }
            foreach (var item in selectedEntities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }
        }
        #endregion

        #region Public Properties
        public decimal LogFontSize { get; private set; }
        public decimal TreeViewFontSize { get; private set; }
        public int RetryCount { get; private set; }
        public int RetryTimeout { get; private set; }
        public int ReceiveTimeout { get; private set; }
        public int ServerTimeout { get; private set; }
        public int PrefetchCount { get; private set; }
        public int TopCount { get; private set; }
        public int SenderThinkTime { get; private set; }
        public int ReceiverThinkTime { get; private set; }
        public int MonitorRefreshInterval { get; private set; }
        public bool ShowMessageCount { get; private set; }
        public bool SaveMessageToFile { get; private set; }
        public bool SavePropertiesToFile { get; private set; }
        public bool SaveCheckpointsToFile { get; private set; }
        public string CertificateThumbprint { get; private set; }
        public string SubscriptionId { get; private set; }
        public string Label { get; private set; }
        public string MessageFile { get; private set; }
        public string MessageText { get; private set; }
        public List<string> SelectedEntities
        {
            get
            {
                return cboSelectedEntities.CheckBoxItems.Where(i => i.Checked).Select(i => i.Text).ToList();
            }
        }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void logNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            LogFontSize = logNumericUpDown.Value;
        }

        private void treeViewNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            TreeViewFontSize = treeViewNumericUpDown.Value;
        }

        private void OptionForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '\r';
            if (e.Handled)
            {
                btnOk_Click(sender, null);
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            LogFontSize = (decimal)8.25;
            TreeViewFontSize = (decimal)8.25;
            logNumericUpDown.Value = LogFontSize;
            treeViewNumericUpDown.Value = TreeViewFontSize;

            RetryCount = 10;
            RetryTimeout = 100;
            retryCountNumericUpDown.Value = RetryCount;
            retryTimeoutNumericUpDown.Value = RetryTimeout;

            ReceiveTimeout = 1;
            ServerTimeout = 3;
            receiveTimeoutNumericUpDown.Value = ReceiveTimeout;
            serverTimeoutNumericUpDown.Value = ServerTimeout;
            
            PrefetchCount = 0;
            TopCount = 10;
            prefetchCountNumericUpDown.Value = PrefetchCount;
            topNumericUpDown.Value = TopCount;

            SenderThinkTime = 500;
            ReceiverThinkTime = 500;
            senderThinkTimeNumericUpDown.Value = SenderThinkTime;
            receiveTimeoutNumericUpDown.Value = ReceiverThinkTime;

            MonitorRefreshInterval = 30;
            monitorRefreshIntervalNumericUpDown.Value = MonitorRefreshInterval;
            cboConnectivityMode.SelectedItem = ConnectivityMode.AutoDetect;
            cboEncodingType.SelectedItem = EncodingType.ASCII;
            
            SaveMessageToFile = true;
            SavePropertiesToFile = true;
            SaveCheckpointsToFile = true;

            saveMessageToFileCheckBox.Checked = true;
            savePropertiesToFileCheckBox.Checked = true;
            saveCheckpointsToFileCheckBox.Checked = true;

            foreach (var item in MainForm.SingletonMainForm.Entities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }
        }

        private void retryCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RetryCount = (int)retryCountNumericUpDown.Value;
        }

        private void retryTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RetryTimeout = (int)retryTimeoutNumericUpDown.Value;
        }

        private void receiveTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ReceiveTimeout = (int)receiveTimeoutNumericUpDown.Value;
        }
        private void sessionTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ServerTimeout = (int)serverTimeoutNumericUpDown.Value;
        }

        private void prefetchCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            PrefetchCount = (int)prefetchCountNumericUpDown.Value;
        }

        private void topNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            TopCount = (int)topNumericUpDown.Value;
        }

        private void showMessageCountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowMessageCount = showMessageCountCheckBox.Checked;
        }

        private void saveMessageToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SaveMessageToFile = saveMessageToFileCheckBox.Checked;
        }

        private void savePropertiesToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SavePropertiesToFile = savePropertiesToFileCheckBox.Checked;
        }

        private void saveCheckpointsToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SaveCheckpointsToFile = saveCheckpointsToFileCheckBox.Checked;
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
        
        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                     cboConnectivityMode.Location.X - 1,
                                     cboConnectivityMode.Location.Y - 1,
                                     cboConnectivityMode.Size.Width + 1,
                                     cboConnectivityMode.Size.Height + 1);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                   cboEncodingType.Location.X - 1,
                                   cboEncodingType.Location.Y - 1,
                                   cboEncodingType.Size.Width + 1,
                                   cboEncodingType.Size.Height + 1);
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboSelectedEntities.Location.X - 1,
                                    cboSelectedEntities.Location.Y - 1,
                                    cboSelectedEntities.Size.Width + 1,
                                    cboSelectedEntities.Size.Height + 1);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(153, 180, 209), 1), 0, mainPanel.Size.Height - 1, mainPanel.Size.Width, mainPanel.Size.Height - 1);
        }

        private void senderThinkTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SenderThinkTime = (int)senderThinkTimeNumericUpDown.Value;
        }

        private void receiverThinkTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ReceiverThinkTime = (int)receiverThinkTimeNumericUpDown.Value;
        }

        private void monitorRefreshIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MonitorRefreshInterval = (int)monitorRefreshIntervalNumericUpDown.Value;
        }

        private void txtSubscriptionId_TextChanged(object sender, EventArgs e)
        {
            SubscriptionId = txtSubscriptionId.Text;
        }

        private void txtManagementCertificateThumbprint_TextChanged(object sender, EventArgs e)
        {
            CertificateThumbprint = txtManagementCertificateThumbprint.Text;
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            Label = txtLabel.Text;
        }

        private void txtMessageFile_TextChanged(object sender, EventArgs e)
        {
            MessageFile = txtMessageFile.Text;
        }

        private void txtMessageText_TextChanged(object sender, EventArgs e)
        {
            MessageText = txtMessageText.Text;
        }

        private void cboConnectivityMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectivityMode connectivityMode;
            if (Enum.TryParse(cboConnectivityMode.Text, true, out connectivityMode))
            {
                ServiceBusHelper.ConnectivityMode = connectivityMode;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (configuration.AppSettings.Settings[ConfigurationParameters.LogFontSize] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.LogFontSize, LogFontSize.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.LogFontSize].Value = LogFontSize.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.TreeViewFontSize] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.TreeViewFontSize, TreeViewFontSize.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.TreeViewFontSize].Value = TreeViewFontSize.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.ShowMessageCountParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.ShowMessageCountParameter, ShowMessageCount.ToString());
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.ShowMessageCountParameter].Value = ShowMessageCount.ToString();
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.SaveMessageToFileParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.SaveMessageToFileParameter, SaveMessageToFile.ToString());
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.SaveMessageToFileParameter].Value = SaveMessageToFile.ToString();
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.SavePropertiesToFileParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.SavePropertiesToFileParameter, SavePropertiesToFile.ToString());
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.SavePropertiesToFileParameter].Value = SavePropertiesToFile.ToString();
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.SaveCheckpointsToFileParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.SaveCheckpointsToFileParameter, SaveCheckpointsToFile.ToString());
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.SaveCheckpointsToFileParameter].Value = SaveCheckpointsToFile.ToString();
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.RetryCountParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.RetryCountParameter, RetryCount.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.RetryCountParameter].Value = RetryCount.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.RetryTimeoutParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.RetryTimeoutParameter, RetryTimeout.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.RetryTimeoutParameter].Value = RetryTimeout.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.TopParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.TopParameter, TopCount.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.TopParameter].Value = TopCount.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.ReceiveTimeoutParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.ReceiveTimeoutParameter, ReceiveTimeout.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.ReceiveTimeoutParameter].Value = ReceiveTimeout.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.ServerTimeoutParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.ServerTimeoutParameter, ServerTimeout.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.ServerTimeoutParameter].Value = ServerTimeout.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.SenderThinkTimeParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.SenderThinkTimeParameter, SenderThinkTime.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.SenderThinkTimeParameter].Value = SenderThinkTime.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.SenderThinkTimeParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.SenderThinkTimeParameter, SenderThinkTime.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.SenderThinkTimeParameter].Value = SenderThinkTime.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.ReceiverThinkTimeParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.ReceiverThinkTimeParameter, ReceiverThinkTime.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.ReceiverThinkTimeParameter].Value = ReceiverThinkTime.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.MonitorRefreshIntervalParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.MonitorRefreshIntervalParameter, MonitorRefreshInterval.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.MonitorRefreshIntervalParameter].Value = MonitorRefreshInterval.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.PrefetchCountParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.PrefetchCountParameter, PrefetchCount.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.PrefetchCountParameter].Value = PrefetchCount.ToString(CultureInfo.InvariantCulture);
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.SubscriptionIdParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.SubscriptionIdParameter, SubscriptionId);
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.SubscriptionIdParameter].Value = SubscriptionId;
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.CertificateThumbprintParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.CertificateThumbprintParameter, CertificateThumbprint);
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.CertificateThumbprintParameter].Value = CertificateThumbprint;
            }


            if (configuration.AppSettings.Settings[ConfigurationParameters.LabelParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.LabelParameter, Label);
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.LabelParameter].Value = Label;
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.FileParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.FileParameter, MessageFile);
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.FileParameter].Value = MessageFile;
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.MessageParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.MessageParameter, MessageText);
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.MessageParameter].Value = MessageText;
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.ConnectivityMode] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.ConnectivityMode, ServiceBusHelper.ConnectivityMode.ToString());
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.ConnectivityMode].Value = ServiceBusHelper.ConnectivityMode.ToString();
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.Encoding] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.Encoding, ServiceBusHelper.EncodingType.ToString());
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.Encoding].Value = ServiceBusHelper.EncodingType.ToString();
            }
            if (configuration.AppSettings.Settings[ConfigurationParameters.SelectedEntitiesParameter] == null)
            {
                configuration.AppSettings.Settings.Add(ConfigurationParameters.SelectedEntitiesParameter, cboSelectedEntities.Text);
            }
            else
            {
                configuration.AppSettings.Settings[ConfigurationParameters.SelectedEntitiesParameter].Value = cboSelectedEntities.Text;
            }
            configuration.Save(ConfigurationSaveMode.Minimal);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var form = new TextForm(MessageTextTitle, txtMessageText.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtMessageText.Text = form.Content;
                }
            }
        }

        private void cboEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEncodingType.SelectedItem is EncodingType)
            {
                ServiceBusHelper.EncodingType = (EncodingType) cboEncodingType.SelectedItem;
            }
        }
        #endregion
    }
} 