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
using System.Drawing;
using System.Windows.Forms;

using Microsoft.Azure.ServiceBusExplorer.Enums;
using Microsoft.Azure.ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus;

#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Forms
{
    using System.Diagnostics;
    using System.IO;

    public partial class OptionForm : Form
    {
        #region Private Constants
        // Messages
        private const string MessageTextTitle = "Message Text";

        // ConfigUseForUI constants
        const int ApplicationConfigFileIndex = 0;
        const int UserConfigFileIndex = 1;
        const int BothConfigFileIndex = 2;
        #endregion

        #region Private Fields
        bool initializing;
        List<string> changedSettings = new List<string>();
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
        public OptionForm(MainProperties mainProperties)
        {
            initializing = true;
            InitializeComponent();
            MainProperties = mainProperties;

            //Label= mainProperties.Label;
            //MessageFile= MainProperties.messageFile;
            //MessageText= MainProperties.MessageText;
            //LogFontSize= MainProperties.LogFontSize;
            //TreeViewFontSize= MainProperties.treeViewFontSize;
            //RetryCount= MainProperties.RetryCount;
            //RetryTimeout= MainProperties.RetryTimeout;
            //ReceiveTimeout= MainProperties.ReceiveTimeout;
            //ServerTimeout= MainProperties.ServerTimeout;
            //SenderThinkTime= MainProperties.SenderThinkTime;
            //ReceiverThinkTime= MainProperties.ReceiverThinkTime;
            //MonitorRefreshInterval= MainProperties.MonitorRefreshInterval;
            //PrefetchCount= MainProperties.prefetchCount;
            //TopCount= MainProperties.top;

            txtLabel.Text = mainProperties.Label;
            txtMessageFile.Text = mainProperties.MessageFile;
            txtMessageText.Text = mainProperties.MessageText;
            logNumericUpDown.Value = mainProperties.LogFontSize;
            treeViewNumericUpDown.Value = mainProperties.TreeViewFontSize;
            retryCountNumericUpDown.Value = mainProperties.RetryCount;
            retryTimeoutNumericUpDown.Value = mainProperties.RetryTimeout;
            receiveTimeoutNumericUpDown.Value = mainProperties.ReceiveTimeout;
            serverTimeoutNumericUpDown.Value = mainProperties.ServerTimeout;
            senderThinkTimeNumericUpDown.Value = mainProperties.SenderThinkTime;
            receiverThinkTimeNumericUpDown.Value = mainProperties.ReceiverThinkTime;
            monitorRefreshIntervalNumericUpDown.Value = mainProperties.MonitorRefreshInterval;
            prefetchCountNumericUpDown.Value = mainProperties.PrefetchCount;
            topNumericUpDown.Value = mainProperties.TopCount;
            showMessageCountCheckBox.Checked = mainProperties.ShowMessageCount;
            savePropertiesToFileCheckBox.Checked = mainProperties.SavePropertiesToFile;
            saveMessageToFileCheckBox.Checked = mainProperties.SaveMessageToFile;
            saveCheckpointsToFileCheckBox.Checked = mainProperties.SaveCheckpointsToFile;
            useAsciiCheckBox.Checked = mainProperties.UseAscii;

            var connectivityMode = ServiceBusHelper.ConnectivityMode;  // TODO
            cboConnectivityMode.DataSource = Enum.GetValues(typeof(ConnectivityMode));
            cboConnectivityMode.SelectedItem = connectivityMode;

            var encodingType = ServiceBusHelper.EncodingType;  // TODO
            cboEncodingType.DataSource = Enum.GetValues(typeof(EncodingType));
            cboEncodingType.SelectedItem = encodingType;

            //ShowMessageCount= MainProperties.ShowMessageCount;
            //SaveMessageToFile= MainProperties.SaveMessageToFile;
            //SavePropertiesToFile= MainProperties.SavePropertiesToFile;
            //SaveCheckpointsToFile= MainProperties.SaveCheckpointsToFile;
            //UseAscii= MainProperties.useAscii;

            foreach (var item in ConfigurationHelper.Entities)
            {
                cboSelectedEntities.Items.Add(item);
            }

            foreach (var item in mainProperties.SelectedEntities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }

            if (!Enum.TryParse<BodyType>(mainProperties.MessageBodyType, true, out var bodyType))
            {
                bodyType = BodyType.Stream;
            }

            cboDefaultMessageBodyType.SelectedIndex = (int)bodyType;
            initializing = false;

            cboConfigFile.DataSource = ConfigUseForUI;
            cboConfigFile.SelectedIndex = GetIndexForConfigFileUseUIString(TwoFilesConfiguration.GetCurrentConfigFileUse());
        }

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
                        nameof(OptionForm.GetIndexForConfigFileUseUIString));
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


        #endregion

        #region Public Properties

        public MainProperties MainProperties { get; private set; }
        //public List<string> SelectedEntities
        //{
        //    get
        //    {
        //        return cboSelectedEntities.CheckBoxItems.Where(i => i.Checked).Select(i => i.Text).ToList();
        //    }
        //}
        //public string MessageBodyType { get; private set; }

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
            MainProperties.LogFontSize = logNumericUpDown.Value;
        }

        private void treeViewNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.TreeViewFontSize);
            MainProperties.TreeViewFontSize = treeViewNumericUpDown.Value;
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
            MainProperties.SetDefault();

            logNumericUpDown.Value = MainProperties.LogFontSize;
            treeViewNumericUpDown.Value = MainProperties.TreeViewFontSize;

            retryCountNumericUpDown.Value = MainProperties.RetryCount;
            retryTimeoutNumericUpDown.Value = MainProperties.RetryTimeout;

            receiveTimeoutNumericUpDown.Value = MainProperties.ReceiveTimeout;
            serverTimeoutNumericUpDown.Value = MainProperties.ServerTimeout;

            prefetchCountNumericUpDown.Value = MainProperties.PrefetchCount;
            topNumericUpDown.Value = MainProperties.TopCount;

            senderThinkTimeNumericUpDown.Value = MainProperties.SenderThinkTime;
            receiveTimeoutNumericUpDown.Value = MainProperties.ReceiverThinkTime;

            monitorRefreshIntervalNumericUpDown.Value = MainProperties.MonitorRefreshInterval;
            cboConnectivityMode.SelectedItem = ConnectivityMode.AutoDetect;
            cboEncodingType.SelectedItem = EncodingType.ASCII;

            saveMessageToFileCheckBox.Checked = MainProperties.SaveMessageToFile;
            showMessageCountCheckBox.Checked = MainProperties.ShowMessageCount;
            savePropertiesToFileCheckBox.Checked = MainProperties.SavePropertiesToFile;
            saveCheckpointsToFileCheckBox.Checked = MainProperties.SaveCheckpointsToFile;
            useAsciiCheckBox.Checked = MainProperties.UseAscii;

            foreach (var item in ConfigurationHelper.Entities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }

            MainProperties.MessageBodyType = MainProperties.MessageBodyType; // .Stream.ToString();
        }

        private void retryCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.RetryCountParameter);
            MainProperties.RetryCount = (int)retryCountNumericUpDown.Value;
        }

        private void retryTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.RetryTimeoutParameter);
            MainProperties.RetryTimeout = (int)retryTimeoutNumericUpDown.Value;
        }

        private void receiveTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.ReceiveTimeoutParameter);
            MainProperties.ReceiveTimeout = (int)receiveTimeoutNumericUpDown.Value;
        }
        private void sessionTimeoutNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.ServerTimeoutParameter);
            MainProperties.ServerTimeout = (int)serverTimeoutNumericUpDown.Value;
        }

        private void prefetchCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.PrefetchCountParameter);
            MainProperties.PrefetchCount = (int)prefetchCountNumericUpDown.Value;
        }

        private void topNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.TopParameter);
            MainProperties.TopCount = (int)topNumericUpDown.Value;
        }

        private void showMessageCountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.ShowMessageCountParameter);
            MainProperties.ShowMessageCount = showMessageCountCheckBox.Checked;
        }

        private void saveMessageToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.SaveMessageToFileParameter);
            MainProperties.SaveMessageToFile = saveMessageToFileCheckBox.Checked;
        }

        private void savePropertiesToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.SavePropertiesToFileParameter);
            MainProperties.SavePropertiesToFile = savePropertiesToFileCheckBox.Checked;
        }

        private void saveCheckpointsToFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.SaveCheckpointsToFileParameter);
            MainProperties.SaveCheckpointsToFile = saveCheckpointsToFileCheckBox.Checked;
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
            AddSettingToChanged(ConfigurationParameters.SenderThinkTimeParameter);
            MainProperties.SenderThinkTime = (int)senderThinkTimeNumericUpDown.Value;
        }

        void receiverThinkTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.ReceiverThinkTimeParameter);
            MainProperties.ReceiverThinkTime = (int)receiverThinkTimeNumericUpDown.Value;
        }

        void useAscii_CheckedChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.UseAsciiParameter);
            MainProperties.UseAscii = useAsciiCheckBox.Checked;
        }

        void monitorRefreshIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.MonitorRefreshIntervalParameter);
            MainProperties.MonitorRefreshInterval = (int)monitorRefreshIntervalNumericUpDown.Value;
        }

        void txtLabel_TextChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.LabelParameter);
            MainProperties.Label = txtLabel.Text;
        }

        void txtMessageFile_TextChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.FileParameter);
            MainProperties.MessageFile = txtMessageFile.Text;
        }

        void txtMessageText_TextChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.MessageParameter);
            MainProperties.MessageText = txtMessageText.Text;
        }

        void cboConnectivityMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Enum.TryParse<ConnectivityMode>(cboConnectivityMode.Text, true, out var connectivityMode))
            {
                AddSettingToChanged(ConfigurationParameters.ConnectivityMode);
                ServiceBusHelper.ConnectivityMode = connectivityMode;
            }
        }

        void cboDefaultMessageBodyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddSettingToChanged(ConfigurationParameters.MessageBodyType);
            MainProperties.MessageBodyType = cboDefaultMessageBodyType.Text;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            SaveChangedProperties();
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
                ServiceBusHelper.EncodingType = (EncodingType)cboEncodingType.SelectedItem;
                AddSettingToChanged(ConfigurationParameters.SelectedEntitiesParameter);
            }
        }
        #endregion

        #region Private methods
        void AddSettingToChanged(string setting)
        {
            if (!initializing && !changedSettings.Contains(setting))
            {
                changedSettings.Add(setting);
            }
        }

        void SaveMessageSettingsIfChanged(TwoFilesConfiguration configuration,
            string runningMessageText, string runningMessageFile)
        {
            MessageAndPropertiesHelper.GetMessageTextAndFile(configuration,
                out var configuredMessageText, out var configuredMessageFile);

            if (changedSettings.Contains(ConfigurationParameters.MessageParameter) &&
                configuredMessageText != runningMessageText)
            {
                configuration.SetValue(ConfigurationParameters.MessageParameter, runningMessageText);
            }

            if (changedSettings.Contains(ConfigurationParameters.FileParameter) &&
                configuredMessageFile != runningMessageFile)
            {
                configuration.SetValue(ConfigurationParameters.FileParameter, runningMessageFile);
            }
        }


        void SaveChangedProperties()
        {
            var configuration = TwoFilesConfiguration.Create();

            SaveSettingIfChanged(configuration, ConfigurationParameters.LogFontSize, MainProperties.LogFontSize);
            SaveSettingIfChanged(configuration, ConfigurationParameters.TreeViewFontSize,
                MainProperties.TreeViewFontSize);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ShowMessageCountParameter,
                MainProperties.ShowMessageCount);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SaveMessageToFileParameter,
                MainProperties.SaveMessageToFile);
            SaveSettingIfChanged(configuration, ConfigurationParameters.UseAsciiParameter, MainProperties.UseAscii);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SavePropertiesToFileParameter,
                MainProperties.SavePropertiesToFile);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SaveCheckpointsToFileParameter,
                MainProperties.SaveCheckpointsToFile);
            SaveSettingIfChanged(configuration, ConfigurationParameters.RetryCountParameter, MainProperties.RetryCount);
            SaveSettingIfChanged(configuration, ConfigurationParameters.RetryTimeoutParameter, MainProperties.RetryTimeout);
            SaveSettingIfChanged(configuration, ConfigurationParameters.TopParameter, MainProperties.TopCount);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ReceiveTimeoutParameter,
                MainProperties.ReceiveTimeout);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ServerTimeoutParameter,
                MainProperties.ServerTimeout);
            SaveSettingIfChanged(configuration, ConfigurationParameters.SenderThinkTimeParameter,
                MainProperties.SenderThinkTime);
            SaveSettingIfChanged(configuration, ConfigurationParameters.ReceiverThinkTimeParameter,
                MainProperties.ReceiverThinkTime);
            SaveSettingIfChanged(configuration, ConfigurationParameters.MonitorRefreshIntervalParameter,
                MainProperties.MonitorRefreshInterval);
            SaveSettingIfChanged(configuration, ConfigurationParameters.PrefetchCountParameter, MainProperties.PrefetchCount);
            SaveSettingIfChanged(configuration, ConfigurationParameters.LabelParameter, MainProperties.Label);
            SaveMessageSettingsIfChanged(configuration, MainProperties.MessageText, MainProperties.MessageFile);
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

        void SaveSettingIfChanged<T>(TwoFilesConfiguration configuration, string setting,
        T runningValue)
        {
            if (changedSettings.Contains(setting))
            {
                configuration.SetValue(setting, runningValue);
                changedSettings.Remove(setting);
            }
        }

        #endregion

        void cboConfigFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Read the values according to the selected configuration option and
            // and display them
            //cboConfigFile

            var configuration = TwoFilesConfiguration.Create();
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
            var configuration = TwoFilesConfiguration.Create(selected, writeToLog: null);

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
            }
        }

        void cboConfigFile_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (changedSettings.Count == 0)
            {
                return;
            }

            var defaultProperties = new MainProperties();

            defaultProperties.SetDefault();
            var resultingProperties = ConfigurationHelper.GetMainProperties(defaultProperties, null);

            if (!MainProperties.Equals(resultingProperties))
            {
                var answer = MessageBox.Show("One or more settings are different in the configuration file selected. " +
                    "Do you want to overwrite the settings "
                    + $"{ConfigUseForUI[lastConfigFileIndex]}?",
                    "Save changed settings", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            }

            switch (answer)
            {
                case DialogResult.Yes:
                    // Save all changed values
                    lastConfigFileIndex = cboConfigFile.SelectedIndex;
                    SaveChangedProperties();
                    ShowProperties(lastConfigFileIndex);
                    break;

                case DialogResult.No:
                    lastConfigFileIndex = cboConfigFile.SelectedIndex;
                    ShowProperties(lastConfigFileIndex);
                    break;

                case DialogResult.Cancel:
                    cboConfigFile.SelectedIndex = lastConfigFileIndex;
                    return;  // Don't do anything else

                default:
                    throw new InvalidDataException("Unexpected value returned from MessageBox.");
            }
        }

        void ShowProperties(int configFileUIIndex)
        {
            var configFileUse = GetConfigFileUseFromUIIndex(configFileUIIndex);
            var configuration = TwoFilesConfiguration.Create(configFileUse, writeToLog: null); // TODO get a variable here
            var defaultProperties = new MainProperties();

            defaultProperties.SetDefault();

            var readProperties = ConfigurationHelper.GetMainProperties(configFileUse, defaultProperties, writeToLog: null);

        }
    }
}
