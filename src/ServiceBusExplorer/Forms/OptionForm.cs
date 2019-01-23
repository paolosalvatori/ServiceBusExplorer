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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using Microsoft.ServiceBus;

using Microsoft.Azure.ServiceBusExplorer.Enums;
using Microsoft.Azure.ServiceBusExplorer.Helpers;

#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Forms
{
    public partial class OptionForm : Form
    {
        #region Private Constants
        // Messages
        const string MessageTextTitle = "Message Text";

        // ConfigUseForUI constants
        const int ApplicationConfigFileIndex = 0;
        const int UserConfigFileIndex = 1;
        const int BothConfigFileIndex = 2;
        #endregion

        #region Private Fields
        int lastConfigFileIndex;

        // This List variable is tied to some of the constants
        static readonly List<string> ConfigUseForUI = new List<string>
        {
            "Application Configuration File",
            "User Configuration File",
            "Both (User file will override)"
        };
        #endregion

        #region Public Constructor
        public OptionForm(MainSettings mainSettings, ConfigFileUse configFileUse)
        {
            InitializeComponent();

            // Put data in the list controls
            cboConnectivityMode.DataSource = Enum.GetValues(typeof(ConnectivityMode));
            cboEncodingType.DataSource = Enum.GetValues(typeof(EncodingType));
            cboConfigFile.DataSource = ConfigUseForUI;

            foreach (var item in ConfigurationHelper.Entities)
            {
                cboSelectedEntities.Items.Add(item);
            }

            MainSettings = mainSettings;
            ConfigFileUse = configFileUse;
            cboConfigFile.SelectedIndex = GetIndexForConfigFileUseUIString(ConfigFileUse);

            ShowSettings(mainSettings);
        }
        #endregion

        #region Public Properties
        public MainSettings MainSettings { get; private set; } = new MainSettings();
        public ConfigFileUse ConfigFileUse { get; private set; }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            MainSettings.SelectedEntities = GetSelectedEntities();

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
            MainSettings.LogFontSize = logNumericUpDown.Value;
        }

        private void treeViewNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.TreeViewFontSize = treeViewNumericUpDown.Value;
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
            MainSettings.SetDefault();

            logNumericUpDown.Value = MainSettings.LogFontSize;
            treeViewNumericUpDown.Value = MainSettings.TreeViewFontSize;

            retryCountNumericUpDown.Value = MainSettings.RetryCount;
            retryTimeoutNumericUpDown.Value = MainSettings.RetryTimeout;

            receiveTimeoutNumericUpDown.Value = MainSettings.ReceiveTimeout;
            serverTimeoutNumericUpDown.Value = MainSettings.ServerTimeout;

            prefetchCountNumericUpDown.Value = MainSettings.PrefetchCount;
            topNumericUpDown.Value = MainSettings.TopCount;

            senderThinkTimeNumericUpDown.Value = MainSettings.SenderThinkTime;
            receiverThinkTimeNumericUpDown.Value = MainSettings.ReceiverThinkTime;

            monitorRefreshIntervalNumericUpDown.Value = MainSettings.MonitorRefreshInterval;
            cboConnectivityMode.SelectedItem = ConnectivityMode.AutoDetect;
            cboEncodingType.SelectedItem = EncodingType.ASCII;

            saveMessageToFileCheckBox.Checked = MainSettings.SaveMessageToFile;
            showMessageCountCheckBox.Checked = MainSettings.ShowMessageCount;
            savePropertiesToFileCheckBox.Checked = MainSettings.SavePropertiesToFile;
            saveCheckpointsToFileCheckBox.Checked = MainSettings.SaveCheckpointsToFile;
            useAsciiCheckBox.Checked = MainSettings.UseAscii;

            foreach (var item in ConfigurationHelper.Entities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }

            MainSettings.MessageBodyType = MainSettings.MessageBodyType; // .Stream.ToString();
        }

        private void retryCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.RetryCount = (int)retryCountNumericUpDown.Value;
        }

        private void retryTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.RetryTimeout = (int)retryTimeoutNumericUpDown.Value;
        }

        private void receiveTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.ReceiveTimeout = (int)receiveTimeoutNumericUpDown.Value;
        }

        private void sessionTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.ServerTimeout = (int)serverTimeoutNumericUpDown.Value;
        }

        private void prefetchCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.PrefetchCount = (int)prefetchCountNumericUpDown.Value;
        }

        private void topNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.TopCount = (int)topNumericUpDown.Value;
        }

        private void showMessageCountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainSettings.ShowMessageCount = showMessageCountCheckBox.Checked;
        }

        private void saveMessageToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainSettings.SaveMessageToFile = saveMessageToFileCheckBox.Checked;
        }

        private void savePropertiesToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainSettings.SavePropertiesToFile = savePropertiesToFileCheckBox.Checked;
        }

        private void saveCheckpointsToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainSettings.SaveCheckpointsToFile = saveCheckpointsToFileCheckBox.Checked;
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

        void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                                    cboConfigFile.Location.X - 1,
                                    cboConfigFile.Location.Y - 1,
                                    cboConfigFile.Size.Width + 1,
                                    cboConfigFile.Size.Height + 1);
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

        void senderThinkTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.SenderThinkTime = (int)senderThinkTimeNumericUpDown.Value;
        }

        void receiverThinkTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.ReceiverThinkTime = (int)receiverThinkTimeNumericUpDown.Value;
        }

        void useAscii_CheckedChanged(object sender, EventArgs e)
        {
            MainSettings.UseAscii = useAsciiCheckBox.Checked;
        }

        void monitorRefreshIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            MainSettings.MonitorRefreshInterval = (int)monitorRefreshIntervalNumericUpDown.Value;
        }

        void txtLabel_TextChanged(object sender, EventArgs e)
        {
            MainSettings.Label = txtLabel.Text;
        }

        void txtMessageFile_TextChanged(object sender, EventArgs e)
        {
            MainSettings.MessageFile = txtMessageFile.Text;
        }

        void txtMessageText_TextChanged(object sender, EventArgs e)
        {
            MainSettings.MessageText = txtMessageText.Text;
        }

        void cboConnectivityMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse<ConnectivityMode>(cboConnectivityMode.Text, true, out var connectivityMode))
            {
                MainSettings.ConnectivityMode = connectivityMode;
            }
        }

        void cboDefaultMessageBodyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainSettings.MessageBodyType = cboDefaultMessageBodyType.Text;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            // Get selected items
            MainSettings.SelectedEntities = GetSelectedEntities();

            SaveSettings(GetConfigFileUseFromUIIndex(cboConfigFile.SelectedIndex));
        }

        void btnOpen_Click(object sender, EventArgs e)
        {
            using (var form = new TextForm(MessageTextTitle, txtMessageText.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtMessageText.Text = form.Content;
                }
            }
        }

        void cboEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEncodingType.SelectedItem is EncodingType)
            {
                MainSettings.EncodingType = (EncodingType)cboEncodingType.SelectedItem;
            }
        }
        void btnOpenConfig_Click(object sender, EventArgs e)
        {
            var selected = GetConfigFileUseFromUIIndex(cboConfigFile.SelectedIndex);

            if (selected == ConfigFileUse.None)
            {
                MessageBox.Show("No file was selected in the list.", "No file opened",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Open the file(s) depending on what's selected. Create an instance of the 
            // TwoFilesConfiguration just to get the paths
            var configuration = TwoFilesConfiguration.Create(selected);

            if (cboConfigFile.SelectedIndex == ApplicationConfigFileIndex ||
                cboConfigFile.SelectedIndex == BothConfigFileIndex)
            {
                // Open the application config file
                Process.Start(configuration.ApplicationFilePath);
            }

            if (cboConfigFile.SelectedIndex == UserConfigFileIndex ||
                cboConfigFile.SelectedIndex == BothConfigFileIndex)
            {
                // Open the user config file. It might not exist though
                if (File.Exists(configuration.UserConfigFilePath))
                {
                    Process.Start(configuration.UserConfigFilePath);
                }
                else
                {
                    MessageBox.Show($"The file {configuration.UserConfigFilePath} does not exist. Click the Save"
                        + " button to create it.",
                        "File does exist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        void cboConfigFile_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Check if there is a difference compared to the configuration we are switching to
            var defaultProperties = new MainSettings();

            defaultProperties.SetDefault();
            var resultingProperties = ConfigurationHelper.GetMainProperties(
                GetConfigFileUseFromUIIndex(cboConfigFile.SelectedIndex), defaultProperties, null);

            if (MainSettings.Equals(resultingProperties))
            {
                lastConfigFileIndex = cboConfigFile.SelectedIndex;
                ConfigFileUse = GetConfigFileUseFromUIIndex(lastConfigFileIndex);
                return;
            }

            var answer = MessageBox.Show("One or more settings are different in the configuration file selected. " +
                    "Do you want to use the settings from "
                    + $"{ConfigUseForUI[cboConfigFile.SelectedIndex]}?",
                    "Use new config file settings", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (answer)
            {
                case DialogResult.Yes:
                    GetAndShowProperties(cboConfigFile.SelectedIndex);
                    break;

                case DialogResult.No:
                    break;

                case DialogResult.Cancel:
                    cboConfigFile.SelectedIndex = lastConfigFileIndex;
                    return;  // Don't do anything else

                default:
                    throw new InvalidDataException("Unexpected value returned from MessageBox.");
            }

            // We get if either Yes or No was selected
            lastConfigFileIndex = cboConfigFile.SelectedIndex;
            ConfigFileUse = GetConfigFileUseFromUIIndex(lastConfigFileIndex);
        }
        #endregion

        #region Private methods
        int GetIndexForConfigFileUseUIString(ConfigFileUse configFileUse)
        {
            switch (configFileUse)
            {
                case ConfigFileUse.None:
                case ConfigFileUse.ApplicationConfig:
                    return ApplicationConfigFileIndex;

                case ConfigFileUse.UserConfig:
                    return UserConfigFileIndex;

                case ConfigFileUse.BothConfig:
                    return BothConfigFileIndex;

                default:
                    throw new InvalidDataException("Unexpexted value passed to " +
                                                   nameof(GetIndexForConfigFileUseUIString));
            }
        }

        ConfigFileUse GetConfigFileUseFromUIIndex(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case ApplicationConfigFileIndex:
                    return ConfigFileUse.ApplicationConfig;

                case UserConfigFileIndex:
                    return ConfigFileUse.UserConfig;

                case BothConfigFileIndex:
                    return ConfigFileUse.BothConfig;

                default:
                    return ConfigFileUse.None;
            }
        }

        void SaveSettings(ConfigFileUse configFileUse)
        {
            var defaultSettings = MainSettings.GetDefault();
            var readSettings = ConfigurationHelper.GetMainProperties(configFileUse,
                defaultSettings, writeToLog: null);
            var configuration = TwoFilesConfiguration.Create(configFileUse);

            configuration.SetValue(ConfigurationParameters.ConfigurationConfigFileParameter, configFileUse);

            SaveSetting(configuration, readSettings, ConfigurationParameters.LogFontSize,
                MainSettings.LogFontSize);
            SaveSetting(configuration, readSettings, ConfigurationParameters.TreeViewFontSize,
                MainSettings.TreeViewFontSize);
            SaveSetting(configuration, readSettings, ConfigurationParameters.ShowMessageCountParameter,
                MainSettings.ShowMessageCount);
            SaveSetting(configuration, readSettings, ConfigurationParameters.SaveMessageToFileParameter,
                MainSettings.SaveMessageToFile);
            SaveSetting(configuration, readSettings, ConfigurationParameters.UseAsciiParameter,
                MainSettings.UseAscii);
            SaveSetting(configuration, readSettings,
                ConfigurationParameters.SavePropertiesToFileParameter,
                MainSettings.SavePropertiesToFile);
            SaveSetting(configuration, readSettings,
                ConfigurationParameters.SaveCheckpointsToFileParameter,
                MainSettings.SaveCheckpointsToFile);
            SaveSetting(configuration, readSettings, ConfigurationParameters.RetryCountParameter,
                MainSettings.RetryCount);
            SaveSetting(configuration, readSettings, ConfigurationParameters.RetryTimeoutParameter,
                MainSettings.RetryTimeout);
            SaveSetting(configuration, readSettings, ConfigurationParameters.TopParameter,
                MainSettings.TopCount);
            SaveSetting(configuration, readSettings, ConfigurationParameters.ReceiveTimeoutParameter,
                MainSettings.ReceiveTimeout);
            SaveSetting(configuration, readSettings, ConfigurationParameters.ServerTimeoutParameter,
                MainSettings.ServerTimeout);
            SaveSetting(configuration, readSettings, ConfigurationParameters.SenderThinkTimeParameter,
                MainSettings.SenderThinkTime);
            SaveSetting(configuration, readSettings, ConfigurationParameters.ReceiverThinkTimeParameter,
                MainSettings.ReceiverThinkTime);
            SaveSetting(configuration, readSettings,
                ConfigurationParameters.MonitorRefreshIntervalParameter,
                MainSettings.MonitorRefreshInterval);
            SaveSetting(configuration, readSettings, ConfigurationParameters.PrefetchCountParameter,
                MainSettings.PrefetchCount);
            SaveSetting(configuration, readSettings, ConfigurationParameters.LabelParameter,
                MainSettings.Label);

            SaveSetting(configuration, readSettings, ConfigurationParameters.MessageParameter,
                MainSettings.MessageText);
            SaveSetting(configuration, readSettings, ConfigurationParameters.FileParameter,
                MainSettings.MessageFile);

            SaveSetting(configuration, readSettings, ConfigurationParameters.ConnectivityMode,
                MainSettings.ConnectivityMode);
            SaveSetting(configuration, readSettings, ConfigurationParameters.Encoding,
                MainSettings.EncodingType);

            SaveListSetting(configuration, readSettings, ConfigurationParameters.SelectedEntitiesParameter,
                MainSettings.SelectedEntities);

            SaveSetting(configuration, readSettings, ConfigurationParameters.MessageBodyType,
                MainSettings.MessageBodyType);

            configuration.Save();
        }

        void SaveSetting<T>(TwoFilesConfiguration configuration, MainSettings savedSettings,
            string setting, T runningValue)
        {
            if (!savedSettings.GetValue(setting).Equals(runningValue))
            {
                configuration.SetValue(setting, runningValue);
            }
        }

        void SaveListSetting(TwoFilesConfiguration configuration, MainSettings savedSettings,
            string setting, List<string> runningList)
        {
            var savedList = savedSettings.GetValue(setting);

            if (savedList != null && !savedList.Equals(runningList))
            {
                var listAsString = string.Join(",", runningList);
                configuration.SetValue(setting, listAsString);
            }
        }

        void GetAndShowProperties(int configFileUIIndex)
        {
            var configFileUse = GetConfigFileUseFromUIIndex(configFileUIIndex);
            var defaultProperties = new MainSettings();

            defaultProperties.SetDefault();

            var readProperties = ConfigurationHelper.GetMainProperties(configFileUse, defaultProperties, writeToLog: null);

            ShowSettings(readProperties);
        }

        void ShowSettings(MainSettings mainSettings)
        {
            txtLabel.Text = mainSettings.Label;
            txtMessageFile.Text = mainSettings.MessageFile;
            txtMessageText.Text = mainSettings.MessageText;
            logNumericUpDown.Value = mainSettings.LogFontSize;
            treeViewNumericUpDown.Value = mainSettings.TreeViewFontSize;
            retryCountNumericUpDown.Value = mainSettings.RetryCount;
            retryTimeoutNumericUpDown.Value = mainSettings.RetryTimeout;
            receiveTimeoutNumericUpDown.Value = mainSettings.ReceiveTimeout;
            serverTimeoutNumericUpDown.Value = mainSettings.ServerTimeout;
            senderThinkTimeNumericUpDown.Value = mainSettings.SenderThinkTime;
            receiverThinkTimeNumericUpDown.Value = mainSettings.ReceiverThinkTime;
            monitorRefreshIntervalNumericUpDown.Value = mainSettings.MonitorRefreshInterval;
            prefetchCountNumericUpDown.Value = mainSettings.PrefetchCount;
            topNumericUpDown.Value = mainSettings.TopCount;
            showMessageCountCheckBox.Checked = mainSettings.ShowMessageCount;
            savePropertiesToFileCheckBox.Checked = mainSettings.SavePropertiesToFile;
            saveMessageToFileCheckBox.Checked = mainSettings.SaveMessageToFile;
            saveCheckpointsToFileCheckBox.Checked = mainSettings.SaveCheckpointsToFile;
            useAsciiCheckBox.Checked = mainSettings.UseAscii;

            cboConnectivityMode.SelectedItem = mainSettings.ConnectivityMode;
            cboEncodingType.SelectedItem = mainSettings.EncodingType;

            foreach (var item in mainSettings.SelectedEntities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }

            if (!Enum.TryParse<BodyType>(mainSettings.MessageBodyType, true, out var bodyType))
            {
                bodyType = BodyType.Stream;
            }

            cboDefaultMessageBodyType.SelectedIndex = (int)bodyType;
        }

        List<string> GetSelectedEntities()
        {
            return cboSelectedEntities.CheckBoxItems.
                Where(i => i.Checked).Select(i => i.Text).ToList();
        }
        #endregion
    }
}
