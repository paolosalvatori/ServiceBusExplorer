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

using ServiceBusExplorer.Utilities.Helpers;
using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

#endregion

namespace ServiceBusExplorer.Helpers
{
    // Used for determining which config file(s) to use
    [Flags]
    public enum ConfigFileUse
    {
        None = 0,
        ApplicationConfig = 1,
        UserConfig = 2,
        BothConfig = 4,
        AccessApplicationConfig = ApplicationConfig | BothConfig,
        AccessUserConfig = UserConfig | BothConfig
    }

    // This class is not thread safe since it relies on the Configuration class which is not thread 
    // safe when writing.
    public class TwoFilesConfiguration
    {
        #region Constants
        const string indent = "  ";
        #endregion

        #region Private static fields
        static bool userConfigPathHasBeenShown;
        #endregion

        #region Private instance fields
        string userConfigFilePath;
        Configuration applicationConfiguration;
        Configuration userConfiguration;
        #endregion

        #region Private static properties
        static ConfigFileUse cachedConfigFileUse { get; set; } = ConfigFileUse.None;
        #endregion

        #region Public Instance properties
        public ConfigFileUse ConfigFileUse { get; }
        public string UserConfigFilePath => userConfigFilePath;
        public string ApplicationFilePath => applicationConfiguration.FilePath;
        #endregion

        #region Private constructor
        TwoFilesConfiguration(ConfigFileUse configFileUse, Configuration applicationConfiguration,
            string userConfigFilePath, Configuration userConfiguration)
        {
            ConfigFileUse = configFileUse;
            this.applicationConfiguration = applicationConfiguration;
            this.userConfigFilePath = userConfigFilePath;
            this.userConfiguration = userConfiguration;
        }
        #endregion

        #region Public Static Create methods 
        // This is the normal way to create it
        public static TwoFilesConfiguration Create(ConfigFileUse configFileUse,
            WriteToLogDelegate writeToLog = null)
        {
            var userConfigFilePath = GetUserSettingsFilePath();

            return Create(userConfigFilePath, configFileUse, writeToLog);
        }


        /// <summary>
        /// This method is meant to only be called for unit testing, to avoid polluting 
        /// neither the application config file for the executable running the unit 
        /// tests nor the user config file.
        /// </summary>
        public static TwoFilesConfiguration Create(string userConfigFilePath,
            ConfigFileUse configFileUse, WriteToLogDelegate writeToLog = null)
        {
            var applicationConfiguration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return CreateConfiguration(applicationConfiguration, userConfigFilePath, configFileUse,
                writeToLog);
        }
        #endregion

        #region Public static methods

        public static void SaveConfigFileUse(ConfigFileUse configFileUse)
        {
            // If it is userconfig then we put it in the user config file.
            // If it is bothconfig then we also put it in the user config file.
            // But if it is applicationconfig then we remove the setting from the user file and 
            // set it in the application file.

            if (UseUserConfig(configFileUse))
            {
                var configuration = Create(ConfigFileUse.UserConfig);
                configuration.SetValue(ConfigurationParameters.ConfigurationConfigFileParameter, configFileUse);
            }
            else  // Put it in the application config file and remove it from the user config
            {
                var userConfiguration = Create(ConfigFileUse.ApplicationConfig);
                userConfiguration.SetValue(ConfigurationParameters.ConfigurationConfigFileParameter, configFileUse);

                var appConfiguration = Create(ConfigFileUse.UserConfig);
                appConfiguration.SetValue(ConfigurationParameters.ConfigurationConfigFileParameter, configFileUse);
            }
        }

        public static ConfigFileUse GetCurrentConfigFileUse()
        {
            return AquireConfigFileUseValue();
        }
        #endregion

        #region Public instance methods
        public string GetStringValue(string AppSettingKey, string defaultValue = "")
        {
            string result = null;

            if (userConfiguration != null && UseUserConfig())
            {
                result = userConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            if (UseApplicationConfig())
            {
                result = applicationConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                result = defaultValue;
            }

            return result;
        }

        public bool GetBoolValue(string AppSettingKey, bool defaultValue,
            WriteToLogDelegate writeToLog = null)
        {
            if (userConfiguration != null && UseUserConfig())
            {
                string resultStringUser = userConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (null != resultStringUser)
                {
                    if (bool.TryParse(resultStringUser, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, userConfigFilePath,
                        AppSettingKey, resultStringUser, typeof(bool));
                }
            }

            if (UseApplicationConfig())
            {
                string resultStringApp = applicationConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (!string.IsNullOrWhiteSpace(resultStringApp))
                {
                    if (bool.TryParse(resultStringApp, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, applicationConfiguration.FilePath, AppSettingKey,
                        resultStringApp, typeof(bool));
                }
            }

            return defaultValue;
        }

        public T GetEnumValue<T>(string AppSettingKey, T defaultValue = default,
            WriteToLogDelegate writeToLog = null)
            where T : struct
        {
            if (userConfiguration != null && UseUserConfig())
            {
                string resultStringUser = userConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (null != resultStringUser)
                {
                    if (Enum.TryParse<T>(resultStringUser, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, userConfigFilePath, AppSettingKey,
                        resultStringUser, typeof(T));
                }
            }

            if (UseApplicationConfig())
            {
                string resultStringApp = applicationConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (!string.IsNullOrWhiteSpace(resultStringApp))
                {
                    if (Enum.TryParse<T>(resultStringApp, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, applicationConfiguration.FilePath, AppSettingKey,
                        resultStringApp, typeof(T));
                }
            }

            return defaultValue;
        }

        public decimal GetDecimalValue(string AppSettingKey, decimal defaultValue = default,
            WriteToLogDelegate writeToLog = null)
        {
            if (userConfiguration != null && UseUserConfig())
            {
                string resultStringUser = userConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (null != resultStringUser)
                {
                    if (decimal.TryParse(resultStringUser, NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, userConfigFilePath, AppSettingKey,
                        resultStringUser, typeof(decimal));
                }
            }

            if (UseApplicationConfig())
            {
                string resultStringApp = applicationConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (!string.IsNullOrWhiteSpace(resultStringApp))
                {
                    if (decimal.TryParse(resultStringApp, NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, applicationConfiguration.FilePath, AppSettingKey,
                        resultStringApp, typeof(decimal));
                }
            }

            return defaultValue;
        }

        public int GetIntValue(string AppSettingKey, int defaultValue = default,
            WriteToLogDelegate writeToLog = null)
        {
            if (userConfiguration != null && UseUserConfig())
            {
                string resultStringUser = userConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (null != resultStringUser)
                {
                    if (int.TryParse(resultStringUser, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, userConfigFilePath, AppSettingKey, resultStringUser,
                        typeof(int));
                }
            }

            if (UseApplicationConfig())
            {
                string resultStringApp = applicationConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (!string.IsNullOrWhiteSpace(resultStringApp))
                {
                    if (int.TryParse(resultStringApp, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, applicationConfiguration.FilePath, AppSettingKey,
                        resultStringApp, typeof(int));
                }
            }

            return defaultValue;
        }

        public Hashtable GetHashtableFromSection(string sectionName)
        {
            Hashtable sectionValues = null;

            if (UseApplicationConfig())
            {
                var sectionInApp = applicationConfiguration?.Sections[sectionName];

                if (null != sectionInApp)
                {
                    sectionValues = GetHashtableFromConfigurationSection(sectionInApp);
                }
            }

            if (UseUserConfig())
            {
                var sectionInUser = userConfiguration?.Sections[sectionName];

                if (null != sectionInUser)
                {
                    var sectionValuesInUserConfig = GetHashtableFromConfigurationSection(sectionInUser);

                    // If sectionValues has not been set previously or configFileUse is set to read 
                    // just the user config then return what we got.
                    if (null == sectionValues || ConfigFileUse == ConfigFileUse.UserConfig)
                    {
                        sectionValues = sectionValuesInUserConfig;
                    }
                    else
                    {
                        // Merge the hash tables giving superiority to the values from user config
                        foreach (DictionaryEntry item in sectionValuesInUserConfig)
                        {
                            sectionValues[item.Key] = item.Value;
                        }
                    }
                }
            }

            return sectionValues;
        }

        public bool SettingExists(string AppSettingKey)
        {
            if (userConfiguration != null && UseUserConfig())
            {
                string resultStringUser = userConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (null != resultStringUser)
                {
                    return true;
                }

                // If we only should read the user config then we should quit searching here.
                if (ConfigFileUse == ConfigFileUse.UserConfig)
                {
                    return false;
                }
            }

            string resultStringApp = applicationConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

            return (null != resultStringApp);
        }

        public void AddEntryToDictionarySection(string sectionName, string key, string value)
        {
            AquireUserConfigurationIfNeeded();

            var section = AquireSectionInAppropriateConfigFile(sectionName);

            if (section == null)
            {
                var filePath = UseUserConfig() ? userConfigFilePath :
                    applicationConfiguration.FilePath;

                throw new InvalidDataException($"Unable to add an entry to the section {sectionName}. " +
                        $"The section in the configuration file {filePath} does not exist.");
            }

            var xml = section.SectionInformation.GetRawXml();
            var element = XElement.Parse(xml);

            element.Add(new XElement("add",
                new XAttribute("key", key),
                new XAttribute("value", value)
                ));

            section.SectionInformation.SetRawXml(element.ToString());


            if (UseUserConfig())
            {
                userConfiguration.Save();
            }
            else
            {
                applicationConfiguration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(sectionName);
            }
        }

        // It tries to update the section in both of the files
        public void UpdateEntryInDictionarySection(string sectionName, string key, string newKey,
            string newValue, WriteToLogDelegate writeToLog)
        {
            bool updatedUser = false;
            bool updatedApp = false;

            if (UseUserConfig())
            {
                AquireUserConfigurationIfNeeded();

                var sectionUser = AquireSectionInAppropriateConfigFile(sectionName);
                updatedUser = TryUpdateEntryInDictionarySectionUsingRawXml(sectionUser, key, newKey, newValue);

                if (updatedUser)
                {
                    userConfiguration.Save();
                }
            }

            // We will enter the scope if the entry was not deleted or if we are in Application config only
            // mode.
            if (!updatedUser && UseApplicationConfig())
            {
                // Update the application config
                var sectionApp = applicationConfiguration.GetSection(sectionName);
                updatedApp = TryUpdateEntryInDictionarySectionUsingRawXml(sectionApp, key, newKey, newValue);

                if (updatedApp)
                {
                    applicationConfiguration.Save(ConfigurationSaveMode.Modified, forceSaveAll: true);
                    ConfigurationManager.RefreshSection(sectionName);
                }
            }

            if (!updatedApp && !updatedApp)
            {
                HandleUpdateConfigurationFileError(updatedUser, updatedApp, writeToLog);
            }
        }

        public void RemoveEntryFromDictionarySection(string sectionName, string key,
            WriteToLogDelegate writeToLog)
        {
            bool removedUser = false;
            bool removedApp = false;

            if (UseUserConfig())
            {
                AquireUserConfigurationIfNeeded();

                ConfigurationSection sectionUser = AquireSectionInAppropriateConfigFile(sectionName);
                removedUser = TryRemoveEntryFromSectionUsingRawXml(sectionUser, key);

                if (removedUser)
                {
                    userConfiguration.Save();
                }
            }

            // We will enter the scope if the entry was not deleted or if we are in Application config only
            // mode.
            if (!removedUser && UseApplicationConfig())
            {
                // Update the application config
                var sectionApp = applicationConfiguration.GetSection(sectionName);
                removedApp = TryRemoveEntryFromSectionUsingRawXml(sectionApp, key);

                if (removedApp)
                {
                    applicationConfiguration.Save(ConfigurationSaveMode.Modified, forceSaveAll: true);
                    ConfigurationManager.RefreshSection(sectionName);
                }
            }

            if (!removedUser && !removedApp)
            {
                HandleUpdateConfigurationFileError(removedUser, removedApp, writeToLog);
            }
        }

        public void RemoveSection(string sectionName, WriteToLogDelegate writeToLog)
        {
            // First remove the section
            if (UseUserConfig())
            {
                var userSection = userConfiguration?.Sections[sectionName];

                if (userSection != null)
                {
                    userConfiguration.Sections.Remove(sectionName);
                    userConfiguration.Save(ConfigurationSaveMode.Modified);
                }
            }

            if (UseApplicationConfig())
            {
                var appSection = applicationConfiguration?.Sections[sectionName];

                if (appSection != null)
                {
                    applicationConfiguration.Sections.Remove(sectionName);
                    applicationConfiguration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(sectionName);
                }
            }

            // The remove it from the configSections section
            RemoveEntryFromDictionarySection("configSections", sectionName, writeToLog);
        }

        public void SetValue<T>(string AppSettingKey, T value)
        {
            AquireUserConfigurationIfNeeded();

            if (value is string)
            {
                SetValueInAppropriateConfigurations(AppSettingKey, value as string);
            }
            else
            {
                var stringValue = Convert.ToString(value, CultureInfo.InvariantCulture);
                SetValueInAppropriateConfigurations(AppSettingKey, stringValue);
            }
        }

        public void Save()
        {
            if (UseApplicationConfig())
            {
                applicationConfiguration.Save(ConfigurationSaveMode.Modified);
            }

            if (UseUserConfig())
            {
                userConfiguration?.Save();
            }
        }

        #endregion

        #region Private static methods
        static ConfigFileUse GetConfigFileUseValueIgnoringCache(string userConfigFilePath,
            WriteToLogDelegate writeToLog = null)
        {
            if (null != userConfigFilePath && File.Exists(userConfigFilePath))
            {
                Configuration userConfiguration = OpenUserConfiguration(userConfigFilePath);

                var resultStringUser =
                    userConfiguration?.AppSettings.Settings
                        [ConfigurationParameters.ConfigurationConfigFileParameter]
                        ?.Value;

                if (resultStringUser != null)
                {
                    if (Enum.TryParse<ConfigFileUse>(resultStringUser, out var resultUser))
                    {
                        return resultUser; // No need to check the application config
                    }

                    WriteParsingFailure(writeToLog, userConfigFilePath, 
                        ConfigurationParameters.ConfigurationConfigFileParameter,
                        resultStringUser, typeof(ConfigFileUse));
                }
            }

            // We get here if the user file did not have a valid value 
            string resultStringApp = ConfigurationManager.AppSettings[ConfigurationParameters.ConfigurationConfigFileParameter];

            if (!string.IsNullOrWhiteSpace(resultStringApp))
            {
                if (Enum.TryParse<ConfigFileUse>(resultStringApp, out var resultApp))
                {
                    return resultApp;
                }

                var appConfiguration =
                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                WriteParsingFailure(writeToLog, appConfiguration.FilePath,
                    ConfigurationParameters.ConfigurationConfigFileParameter, resultStringApp, typeof(ConfigFileUse));
            }

            return ConfigFileUse.ApplicationConfig;  // Default to Application for backwards compability
        }

        static ConfigFileUse AquireConfigFileUseValue()
        {
            if (cachedConfigFileUse == ConfigFileUse.None)
            {
                cachedConfigFileUse = GetConfigFileUseValueIgnoringCache(GetUserSettingsFilePath());
            }

            return cachedConfigFileUse;
        }

        static void WriteParsingFailure(WriteToLogDelegate writeToLogDelegate, string configFile,
            string appSettingsKey, string value, Type type)
        {
            writeToLogDelegate?.Invoke($"The configuration file {configFile} has a setting, {appSettingsKey}" +
                                       $" having the value {value}, which cannot be parsed to a(n) {type}. " +
                                       "The setting is therefore ignored.");
        }

        static Hashtable GetHashtableFromConfigurationSection(ConfigurationSection section)
        {
            var xml = section.SectionInformation.GetRawXml();
            Hashtable sectionValues = null;

            if (null != xml)
            {
                var doc = new XmlDocument();

                doc.Load(XmlReader.Create(new StringReader(xml)));
                sectionValues = (Hashtable)new DictionarySectionHandler()
                    .Create(null, null, section: doc.DocumentElement ?? throw new InvalidDataException("A configuration file is invalid"));
            }

            return sectionValues;
        }

        static TwoFilesConfiguration CreateConfiguration(Configuration applicationConfiguration,
            string userConfigFilePath, ConfigFileUse configFileUse, WriteToLogDelegate writeToLog = null)
        {
            if (!userConfigPathHasBeenShown && writeToLog != null)
            {
                userConfigPathHasBeenShown = true;

                string configFileUseInfo;

                switch(configFileUse)
                {
                    case ConfigFileUse.ApplicationConfig:
                        configFileUseInfo = $"The file {applicationConfiguration.FilePath} is used" +
                            " for the configuration settings.";
                        break;

                    case ConfigFileUse.UserConfig:
                        configFileUseInfo = $"The file {userConfigFilePath} is used" +
                            " for the configuration settings.";
                        break;

                    case ConfigFileUse.BothConfig:
                        configFileUseInfo = $"The files {userConfigFilePath} and {applicationConfiguration.FilePath} are used" +
                            " for the configuration settings.";
                        break;

                    default:
                        throw new InvalidOperationException("Unexptected value");
                }

                writeToLog(configFileUseInfo);

                if (UseUserConfig(configFileUse) && !File.Exists(userConfigFilePath))
                {
                    writeToLog($" The file {userConfigFilePath} does not currently exist though, but will be" +
                               " automatically created if any connection string or setting is changed.");
                }
            }

            if (File.Exists(userConfigFilePath))
            {
                Configuration userConfiguration = OpenUserConfiguration(userConfigFilePath);
                return new TwoFilesConfiguration(configFileUse, applicationConfiguration, userConfigFilePath,
                    userConfiguration);
            }

            return new TwoFilesConfiguration(configFileUse, applicationConfiguration, userConfigFilePath,
                null);
        }

        static Configuration OpenUserConfiguration(string userFilePath)
        {
            var exeConfigurationFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = userFilePath
            };

            return ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap,
                ConfigurationUserLevel.None);
        }

        static string GetUserSettingsFilePath()
        {
            // Environment.SpecialFolder.ApplicationData = The directory that serves as a common repository 
            // for application - specific data for the current roaming user.
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Service Bus Explorer", "UserSettings.config");
        }

        static void CreateSectionUsingRawXml(XDocument document, string sectionName)
        {
            var configElement = document.AquireElement("configuration", addFirst: true);
            var configSections = configElement.AquireElement("configSections", addFirst: true);

            // Create the section element
            var newSection = new XElement("section",
                    new XAttribute("name", sectionName),
                    new XAttribute("type", "System.Configuration.DictionarySectionHandler, System, " +
                        "Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));

            configSections.Add(newSection);
            configElement.AquireElement(sectionName);
        }

        // This updates only the specified section. The Configuration object needs to be saved.
        static bool TryUpdateEntryInDictionarySectionUsingRawXml(ConfigurationSection section,
            string key, string newKey, string newValue)
        {
            if (null == section)
            {
                return false;
            }

            var xml = section.SectionInformation.GetRawXml();
            var xmlDocument = new XmlDocument();
            bool found = false;

            xmlDocument.LoadXml(xml);

            if (null == xmlDocument.DocumentElement)
            {
                return false;
            }

            foreach (XmlElement child in xmlDocument.DocumentElement.ChildNodes)
            {
                if (child.LocalName == "add" && child.GetAttribute("key") == key)
                {
                    if (!string.IsNullOrEmpty(newKey)) child.SetAttribute("key", newKey);
                    if (!string.IsNullOrEmpty(newValue)) child.SetAttribute("value", newValue);
                    found = true;
                    break;
                }
            }

            if (found)
            {
                section.SectionInformation.SetRawXml(xmlDocument.OuterXml);
            }

            return found;
        }

        // The Configuration object needs to be saved after the removal of the entry.
        static bool TryRemoveEntryFromSectionUsingRawXml(ConfigurationSection section, string key)
        {
            if (null == section)
            {
                return false;
            }

            var xml = section.SectionInformation.GetRawXml();
            var xmlDocument = new XmlDocument();
            bool found = false;

            xmlDocument.LoadXml(xml);

            if (null == xmlDocument.DocumentElement)
            {
                return false;
            }

            foreach (XmlElement child in xmlDocument.DocumentElement.ChildNodes)
            {
                if (child.LocalName == "add" && child.GetAttribute("key") == key)
                {
                    found = true;
                    child.ParentNode.RemoveChild(child);
                    break;
                }
            }

            if (found)
            {
                section.SectionInformation.SetRawXml(xmlDocument.OuterXml);
            }

            return found;
        }

        static bool UseUserConfig(ConfigFileUse configFileUse)
        {
            return ((configFileUse & ConfigFileUse.AccessUserConfig) != ConfigFileUse.None);
        }

        #endregion

        #region Private instance methods
        bool UseUserConfig()
        {
            return UseUserConfig(ConfigFileUse);
        }

        bool UseApplicationConfig()
        {
            return ((ConfigFileUse & ConfigFileUse.AccessApplicationConfig) != ConfigFileUse.None);
        }

        void AquireUserConfigurationIfNeeded()
        {
            if (UseUserConfig())
            {
                if (userConfiguration == null)
                {
                    EnsureUserFileExists();
                    userConfiguration = OpenUserConfiguration(userConfigFilePath);
                }
            }
        }

        void CreateDictionarySectionInAppropriateConfigFile(string sectionName)
        {
            AquireUserConfigurationIfNeeded();

            ConfigurationSection section;
            string configFilePath;


            if (UseUserConfig())
            {
                section = userConfiguration.GetSection(sectionName);
                configFilePath = userConfigFilePath;
            }
            else
            {
                section = applicationConfiguration.GetSection(sectionName);
                configFilePath = applicationConfiguration.FilePath;
            }

            if (null != section)
            {
                return; // Section already exists
            }

            var document = XDocument.Load(configFilePath);

            CreateSectionUsingRawXml(document, sectionName);

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = indent
            };

            using (var writer = XmlWriter.Create(configFilePath, settings))
            {
                document.Save(writer);
            }

            // Refresh the configuration object
            if (UseUserConfig())
            {
                userConfiguration = null;
                AquireUserConfigurationIfNeeded();
            }
            else
            {
                // Reset the configuration
                ConfigurationManager.RefreshSection(sectionName);
                applicationConfiguration = ConfigurationManager
                    .OpenExeConfiguration(ConfigurationUserLevel.None);
            }
        }

        ConfigurationSection AquireSectionInAppropriateConfigFile(string sectionName)
        {
            AquireUserConfigurationIfNeeded();

            var singleConfiguration = UseUserConfig() ? userConfiguration : applicationConfiguration;

            var section = singleConfiguration.GetSection(sectionName);

            if (null == section)
            {
                // Create the section in the user file
                CreateDictionarySectionInAppropriateConfigFile(sectionName);

                // Get an updated configuration object
                if (UseUserConfig())
                {
                    singleConfiguration = userConfiguration =
                        OpenUserConfiguration(userConfigFilePath);
                }
                else
                {
                    ConfigurationManager.RefreshSection(sectionName);

                    singleConfiguration = applicationConfiguration = ConfigurationManager
                        .OpenExeConfiguration(ConfigurationUserLevel.None);
                }


                //configuration = UseUserConfig() ? userConfiguration : applicationConfiguration;
                section = singleConfiguration.GetSection(sectionName);

                if (null == section)
                {
                    var configFilePath = UseUserConfig() ? userConfigFilePath :
                        applicationConfiguration.FilePath;

                    throw new InvalidDataException($"Unable to create the section {sectionName} " +
                        $"in the configuration file {configFilePath}.");
                }
            }

            return section;
        }

        /// Creates the user configuration file if it does not exist. Another way 
        /// of creating the user file would be to use the SaveAs method of the 
        /// Configuration class. That would create a copy of the 
        void EnsureUserFileExists()
        {
            if (!File.Exists(userConfigFilePath))
            {
                // Make sure the directory exists
                var userConfigDirectory = Path.GetDirectoryName(userConfigFilePath);
                Directory.CreateDirectory(userConfigDirectory);

                // Create the config file 
                var document = new XDocument();

                document.Declaration = new XDeclaration("1.0", "utf-8", "yes");

                var rootElement = new XElement("configuration");

                rootElement.Add(new XElement("appSettings"));

                // Create the serviceBusNamespaces section
                CreateSectionUsingRawXml(document, "serviceBusNamespaces");

                // Save it
                document.Save(userConfigFilePath);
            }
        }

        void SetValueInAppropriateConfigurations(string AppSettingKey, string stringValue)
        {
            if (UseUserConfig())
            {
                if (userConfiguration.AppSettings.Settings[AppSettingKey] == null)
                {
                    userConfiguration.AppSettings.Settings.Add(AppSettingKey, stringValue);
                }
                else
                {
                    userConfiguration.AppSettings.Settings[AppSettingKey].Value = stringValue;
                }
            }
            else // Update the Application Configuration
            {
                if (applicationConfiguration.AppSettings.Settings[AppSettingKey] == null)
                {
                    applicationConfiguration.AppSettings.Settings.Add(AppSettingKey, stringValue);
                }
                else
                {
                    applicationConfiguration.AppSettings.Settings[AppSettingKey].Value = stringValue;
                }
            }

        }

        void HandleUpdateConfigurationFileError(bool removedUser, bool removedApp, WriteToLogDelegate writeToLog)
        {
            if (removedUser && removedApp)
            {
                return;
            }

            string message = string.Empty;

            switch (ConfigFileUse)
            {
                case ConfigFileUse.ApplicationConfig:
                    if (!removedApp)
                    {
                        message = $"the application configuration file {applicationConfiguration.FilePath}.";
                    }
                    break;

                case ConfigFileUse.UserConfig:
                    if (!removedUser)
                    {
                        message = $"the user configuration file {userConfigFilePath}.";
                    }
                    break;

                case ConfigFileUse.BothConfig:
                    if (!removedApp && !removedUser)
                    {
                        message = $"the application configuration file {applicationConfiguration.FilePath}" +
                            " and the user configuration file {userConfigFilePath}.";
                    }
                    break;

                default:
                    throw new InvalidDataException("Unexpected value for ConfigFileUse: " +
                        $"{ConfigFileUse}.");
            }

            writeToLog?.Invoke("Failed to update " + message);
        }
        #endregion
    }
}
