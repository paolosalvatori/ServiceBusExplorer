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
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Azure.ServiceBusExplorer.Helpers;
using Microsoft.Azure.ServiceBusExplorer.Enums;
using Microsoft.ServiceBus;

#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Forms
{
    public partial class OptionForm : Form
    {
        #region Private Constants
        //***************************
        // Messages
        //***************************
        private const string MessageTextTitle = "Message Text";
        #endregion

        #region Private Fields
        bool initializing;
        List<string> changedSettings = new List<string>();
        #endregion

        #region Public Constructor
        public OptionForm(string label,
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
                          bool useAscii,
                          IEnumerable<string> selectedEntities,
                          string messageBodyType)
        {
            initializing = true;
            InitializeComponent();

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
            showMessageCountCheckBox.Checked = showMessageCount;
            savePropertiesToFileCheckBox.Checked = savePropertiesToFile;
            saveMessageToFileCheckBox.Checked = saveMessageToFile;
            saveCheckpointsToFileCheckBox.Checked = saveCheckpointsToFile;
            useAsciiCheckBox.Checked = useAscii;

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
            UseAscii = useAscii;

            foreach (var item in ConfigurationHelper.Entities)
            {
                cboSelectedEntities.Items.Add(item);
            }
            foreach (var item in selectedEntities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }
            if (!Enum.TryParse<BodyType>(messageBodyType, true, out var bodyType))
            {
                bodyType = BodyType.Stream;
            }
            cboDefaultMessageBodyType.SelectedIndex = (int)bodyType;
            initializing = false;
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
        public bool UseAscii { get; private set; }
        public bool SaveMessageToFile { get; private set; }
        public bool SavePropertiesToFile { get; private set; }
        public bool SaveCheckpointsToFile { get; private set; }
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
        public string MessageBodyType { get; private set; }

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
            AddSettingToChanged(ConfigurationParameters.LogFontSize);
            LogFontSize = logNumericUpDown.Value;
        }

        private void treeViewNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.TreeViewFontSize);
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
            UseAscii = true;

            saveMessageToFileCheckBox.Checked = true;
            showMessageCountCheckBox.Checked = true;
            savePropertiesToFileCheckBox.Checked = true;
            saveCheckpointsToFileCheckBox.Checked = true;
            useAsciiCheckBox.Checked = true;

            foreach (var item in ConfigurationHelper.Entities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }

            MessageBodyType = BodyType.Stream.ToString();
        }

        private void retryCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.RetryCountParameter);
            RetryCount = (int)retryCountNumericUpDown.Value;
        }

        private void retryTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.RetryTimeoutParameter);
            RetryTimeout = (int)retryTimeoutNumericUpDown.Value;
        }

        private void receiveTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.ReceiveTimeoutParameter);
            ReceiveTimeout = (int)receiveTimeoutNumericUpDown.Value;
        }
        private void sessionTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.ServerTimeoutParameter);
            ServerTimeout = (int)serverTimeoutNumericUpDown.Value;
        }

        private void prefetchCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.PrefetchCountParameter);
            PrefetchCount = (int)prefetchCountNumericUpDown.Value;
        }

        private void topNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.TopParameter);
            TopCount = (int)topNumericUpDown.Value;
        }

        private void showMessageCountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.ShowMessageCountParameter);
            ShowMessageCount = showMessageCountCheckBox.Checked;
        }

        private void saveMessageToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.SaveMessageToFileParameter);
            SaveMessageToFile = saveMessageToFileCheckBox.Checked;
        }

        private void savePropertiesToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.SavePropertiesToFileParameter);
            SavePropertiesToFile = savePropertiesToFileCheckBox.Checked;
        }

        private void saveCheckpointsToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.SaveCheckpointsToFileParameter);
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
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboDefaultMessageBodyType.Location.X - 1,
                                    cboDefaultMessageBodyType.Location.Y - 1,
                                    cboDefaultMessageBodyType.Size.Width + 1,
                                    cboDefaultMessageBodyType.Size.Height + 1);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(153, 180, 209), 1), 0, mainPanel.Size.Height - 1, mainPanel.Size.Width, mainPanel.Size.Height - 1);
        }

        private void senderThinkTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.SenderThinkTimeParameter);
            SenderThinkTime = (int)senderThinkTimeNumericUpDown.Value;
        }

        private void receiverThinkTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.ReceiverThinkTimeParameter);
            ReceiverThinkTime = (int)receiverThinkTimeNumericUpDown.Value;
        }

        private void useAscii_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.UseAsciiParameter);
            UseAscii = useAsciiCheckBox.Checked;
        }

        private void monitorRefreshIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.MonitorRefreshIntervalParameter);
            MonitorRefreshInterval = (int)monitorRefreshIntervalNumericUpDown.Value;
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.LabelParameter);
            Label = txtLabel.Text;
        }

        private void txtMessageFile_TextChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.FileParameter);
            MessageFile = txtMessageFile.Text;
        }

        private void txtMessageText_TextChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.MessageParameter);
            MessageText = txtMessageText.Text;
        }

        private void cboConnectivityMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse<ConnectivityMode>(cboConnectivityMode.Text, true, out var connectivityMode))
            {
                AddSettingToChanged(ConfigurationParameters.ConnectivityMode);
                ServiceBusHelper.ConnectivityMode = connectivityMode;
            }
        }

        private void cboDefaultMessageBodyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.MessageBodyType);
            MessageBodyType = cboDefaultMessageBodyType.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var configuration = TwoFilesConfiguration.Create();

            SaveSettingIfChanged(configuration, ConfigurationParameters.LogFontSize, LogFontSize);
            SaveSettingIfChanged(configuration, ConfigurationParameters.TreeViewFontSize,
                TreeViewFontSize);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ShowMessageCountParameter,
                ShowMessageCount);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SaveMessageToFileParameter,
                SaveMessageToFile);
            SaveSettingIfChanged(configuration, ConfigurationParameters.UseAsciiParameter, UseAscii);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SavePropertiesToFileParameter,
                SavePropertiesToFile);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SaveCheckpointsToFileParameter,
                SaveCheckpointsToFile);
            SaveSettingIfChanged(configuration, ConfigurationParameters.RetryCountParameter, RetryCount);
            SaveSettingIfChanged(configuration, ConfigurationParameters.RetryTimeoutParameter, RetryTimeout);
            SaveSettingIfChanged(configuration, ConfigurationParameters.TopParameter, TopCount);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ReceiveTimeoutParameter,
                ReceiveTimeout);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ServerTimeoutParameter,
                ServerTimeout);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SenderThinkTimeParameter,
                SenderThinkTime);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ReceiverThinkTimeParameter,
                ReceiverThinkTime);
            SaveSettingIfChanged(configuration, ConfigurationParameters.MonitorRefreshIntervalParameter,
                MonitorRefreshInterval);
            SaveSettingIfChanged(configuration, ConfigurationParameters.PrefetchCountParameter, PrefetchCount);
            SaveSettingIfChanged(configuration, ConfigurationParameters.LabelParameter, Label);
            SaveMessageSettingsIfChanged(configuration, MessageText, MessageFile);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ConnectivityMode,
                ServiceBusHelper.ConnectivityMode);
            SaveSettingIfChanged(configuration, ConfigurationParameters.Encoding,
                ServiceBusHelper.EncodingType);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SelectedEntitiesParameter, 
                cboSelectedEntities.Text);
            SaveSettingIfChanged(configuration, ConfigurationParameters.MessageBodyType,
                cboDefaultMessageBodyType.SelectedIndex.ToString());

            configuration.Save();
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
                ServiceBusHelper.EncodingType = (EncodingType)cboEncodingType.SelectedItem;
                AddSettingToChanged(ConfigurationParameters.SelectedEntitiesParameter);
            }
        }
        #endregion

        #region Private methods
        void AddSettingToChanged(string setting)
        {
            if(!initializing && !changedSettings.Contains(setting))
            {
                changedSettings.Add(setting);
            }
        }

        void SaveMessageSettingsIfChanged(TwoFilesConfiguration configuration,
            string runningMessageText, string runningMessageFile)
        {
            MessageAndPropertiesHelper.GetMessageTextAndFile(configuration,
                out var configuredMessageText, out var configuredMessageFile);

            if (changedSettings.Contains(ConfigurationParameters.MessageParameter))
            {
                configuration.SetValue(ConfigurationParameters.MessageParameter, runningMessageText);
            }

            if (changedSettings.Contains(ConfigurationParameters.FileParameter))
            {
                configuration.SetValue(ConfigurationParameters.FileParameter, runningMessageFile);
            }
        }

        void SaveSettingIfChanged<T>(TwoFilesConfiguration configuration, string setting,
            T runningValue)
        {
            if (changedSettings.Contains(setting))
            {
                configuration.SetValue(setting, runningValue);
            }
        }
        #endregion
    }
}
