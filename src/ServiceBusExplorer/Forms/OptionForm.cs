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

        #region Private fields
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
            UseAscii = true;

            saveMessageToFileCheckBox.Checked = true;
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
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboDefaultMessageBodyType.Location.X - 1,
                                    cboDefaultMessageBodyType.Location.Y - 1,
                                    cboDefaultMessageBodyType.Size.Width + 1,
                                    cboDefaultMessageBodyType.Size.Height + 1);
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

        private void useAscii_CheckedChanged(object sender, EventArgs e)
        {
            UseAscii = useAsciiCheckBox.Checked;
        }

        private void monitorRefreshIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MonitorRefreshInterval = (int)monitorRefreshIntervalNumericUpDown.Value;
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
            if (Enum.TryParse<ConnectivityMode>(cboConnectivityMode.Text, true, out var connectivityMode))
            {
                ServiceBusHelper.ConnectivityMode = connectivityMode;
            }
        }

        private void cboDefaultMessageBodyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBodyType = cboDefaultMessageBodyType.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var configuration = TwoFilesConfiguration.Create();

            SaveDecimalSettingIfChanged(configuration, ConfigurationParameters.LogFontSize, LogFontSize,
                MainForm.lstLog.Font.Size);
            SaveDecimalSettingIfChanged(configuration, ConfigurationParameters.TreeViewFontSize,
                TreeViewFontSize, MainForm.tree);
            SaveBoolSettingIfChanged(configuration, ConfigurationParameters.ShowMessageCountParameter,
                ShowMessageCount);
            SaveBoolSettingIfChanged(configuration, ConfigurationParameters.SaveMessageToFileParameter,
                SaveMessageToFile);
            SaveBoolSettingIfChanged(configuration, ConfigurationParameters.UseAsciiParameter, UseAscii);
            SaveBoolSettingIfChanged(configuration, ConfigurationParameters.SavePropertiesToFileParameter,
                SavePropertiesToFile);
            SaveBoolSettingIfChanged(configuration, ConfigurationParameters.SaveCheckpointsToFileParameter,
                SaveCheckpointsToFile);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.RetryCountParameter, RetryCount);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.RetryTimeoutParameter, RetryTimeout);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.TopParameter, TopCount);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.ReceiveTimeoutParameter,
                ReceiveTimeout);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.ServerTimeoutParameter,
                ServerTimeout);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.SenderThinkTimeParameter,
                SenderThinkTime);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.ReceiverThinkTimeParameter,
                ReceiverThinkTime);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.MonitorRefreshIntervalParameter,
                MonitorRefreshInterval);
            SaveIntSettingIfChanged(configuration, ConfigurationParameters.PrefetchCountParameter, PrefetchCount);
            SaveStringSettingIfChanged(configuration, ConfigurationParameters.LabelParameter, Label);
            SaveMessageSettingsIfChanged(configuration, MessageText, MessageFile);
            SaveEnumSettingIfChanged(configuration, ConfigurationParameters.ConnectivityMode,
                ServiceBusHelper.ConnectivityMode);
            SaveEnumSettingIfChanged(configuration, ConfigurationParameters.Encoding,
                ServiceBusHelper.EncodingType);
            SaveSelectedEntitiesIfChanged(configuration, cboSelectedEntities.Text);
            SaveStringSettingIfChanged(configuration, ConfigurationParameters.MessageBodyType,
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
            if(!changedSettings.Contains(setting))
            {
                changedSettings.Add(setting);
            }
        }

        // Since the MainFrom reads the values as float from the configuration file this
        // method is doing that too. I'll create an Issue for making it decimal throughout.
        void SaveBoolSettingIfChanged(TwoFilesConfiguration configuration, string setting, bool runningValue)
        {
            if (runningValue != configuration.GetBoolValue(setting, runningValue) &&
                configuration.SettingExists(setting))
            {
                configuration.SetValue(setting, runningValue);
            }
        }

        void SaveDecimalSettingIfChanged(TwoFilesConfiguration configuration, string setting,
            decimal runningValue, decimal initialValue)
        {
            // Write the value if we get a different value when reading the configuration and
            // it has been set or it is different from the initial value.
            if (runningValue != configuration.GetDecimalValue(setting, default)
                && configuration.SettingExists(setting)
                || runningValue != initialValue)
            {

                configuration.SetValue(setting, runningValue);
            }
        }

        void SaveIntSettingIfChanged(TwoFilesConfiguration configuration, string setting,
            int runningValue)
        {
            if (runningValue != configuration.GetIntValue(setting, runningValue))
            {
                configuration.SetValue(setting, runningValue);
            }


        }


        void SaveMessageSettingsIfChanged(TwoFilesConfiguration configuration,
            string runningMessageText, string runningMessageFile)
        {
            MessageAndPropertiesHelper.GetMessageTextAndFile(configuration,
                out var configuredMessageText, out var configuredMessageFile);

            if (runningMessageText != configuredMessageText)
            {
                configuration.SetValue(ConfigurationParameters.MessageParameter, runningMessageText);
            }

            if ((runningMessageFile ?? string.Empty) != configuredMessageFile)
            {
                configuration.SetValue(ConfigurationParameters.FileParameter, runningMessageFile);
            }
        }

        void SaveSelectedEntitiesIfChanged(TwoFilesConfiguration configuration, string runningSelectedEntities)
        {
            var configuredSelectedEntities = ConfigurationHelper.GetSelectedEntities(configuration);

            // Check if the lists are different
            if (!(configuredSelectedEntities.All(runningEntitiesAsList.Contains) &&
                configuredSelectedEntities.Count == runningEntitiesAsList.Count))
            {
                configuration.SetValue(ConfigurationParameters.SelectedEntitiesParameter,
                    runningSelectedEntities);
            }
        }

        void SaveStringSettingIfChanged(TwoFilesConfiguration configuration, string setting,
            string runningValue)
        {
            if (runningValue != configuration.GetStringValue(setting, runningValue))
            {
                configuration.SetValue(setting, runningValue);
            }
        }

        void SaveEnumSettingIfChanged<T>(TwoFilesConfiguration configuration, string setting,
            T runningValue)
            where T : struct
        {
            if (!runningValue.Equals(configuration.GetEnumValue<T>(setting, runningValue)))
            {
                configuration.SetValue(setting, runningValue);
            }
        }

        #endregion
    }
}
