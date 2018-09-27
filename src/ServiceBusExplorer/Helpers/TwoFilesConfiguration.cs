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
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
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

        #region Private constructor
        private TwoFilesConfiguration(Configuration applicationConfiguration, string userConfigFilePath,
            Configuration userConfiguration)
        {
            this.applicationConfiguration = applicationConfiguration;
            this.userConfigFilePath = userConfigFilePath;
            this.userConfiguration = userConfiguration;
        }
        #endregion

        #region Static Create methods 
        public static TwoFilesConfiguration Create(WriteToLogDelegate writeToLog = null)
        {
            var localApplicationConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var userConfigFilePath = GetUserSettingsFilePath();

            return CreateConfiguration(localApplicationConfiguration, userConfigFilePath, writeToLog);
        }

        /// <summary>
        /// This method is meant to only be called for unit testing, to avoid polluting 
        /// neither the application config file for the executable running the unit 
        /// tests nor the user config file.
        /// </summary>
        public static TwoFilesConfiguration Create(string userConfigFilePath, WriteToLogDelegate writeToLog = null)
        {
            var applicationConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return CreateConfiguration(applicationConfiguration, userConfigFilePath, writeToLog);
        }
        #endregion

        #region Public methods
        public string GetStringValue(string AppSettingKey, string defaultValue = "")
        {
            string result = null;

            if (userConfiguration != null)
            {
                result = userConfiguration.AppSettings.Settings[AppSettingKey]?.Value;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = applicationConfiguration.AppSettings.Settings[AppSettingKey]?.Value;
            }

            if (string.IsNullOrEmpty(result))
            {
                result = defaultValue;
            }

            return result;
        }

        public bool GetBoolValue(string AppSettingKey, bool defaultValue,
            WriteToLogDelegate writeToLog)
        {
            if (userConfiguration != null)
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

            return defaultValue;
        }

        public T GetEnumValue<T>(string AppSettingKey, WriteToLogDelegate writeToLog, T defaultValue = default)
            where T : struct
        {
            if (userConfiguration != null)
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

            return defaultValue;
        }

        public float GetFloatValue(string AppSettingKey, WriteToLogDelegate writeToLog, float defaultValue = default)
        {
            if (userConfiguration != null)
            {
                string resultStringUser = userConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

                if (null != resultStringUser)
                {
                    if (float.TryParse(resultStringUser, NumberStyles.Float,
                        CultureInfo.InvariantCulture, out var result))
                    {
                        return result;
                    }

                    WriteParsingFailure(writeToLog, userConfigFilePath, AppSettingKey,
                        resultStringUser, typeof(float));
                }
            }

            string resultStringApp = applicationConfiguration.AppSettings.Settings[AppSettingKey]?.Value;

            if (!string.IsNullOrWhiteSpace(resultStringApp))
            {
                if (float.TryParse(resultStringApp, NumberStyles.Float,
                    CultureInfo.InvariantCulture, out var result))
                {
                    return result;
                }

                WriteParsingFailure(writeToLog, applicationConfiguration.FilePath, AppSettingKey,
                    resultStringApp, typeof(float));
            }

            return defaultValue;
        }

        public int GetIntValue(string AppSettingKey, WriteToLogDelegate writeToLog, int defaultValue = default)
        {
            if (userConfiguration != null)
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

            return defaultValue;
        }

        public Hashtable GetHashtableFromSection(string sectionName)
        {
            Hashtable sectionValues = null;

            var sectionInApp = applicationConfiguration?.Sections[sectionName];

            if (null != sectionInApp)
            {
                sectionValues = GetHashtableFromConfigurationSection(sectionInApp);
            }

            var sectionInUser = userConfiguration?.Sections[sectionName];

            if (null != sectionInUser)
            {
                var sectionValuesInUserConfig = GetHashtableFromConfigurationSection(sectionInUser);

                if (null == sectionValues)
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

            return sectionValues;
        }

        public void AddEntryToDictionarySection(string sectionName, string key, string value)
        {
            AquireUserConfiguration();

            ConfigurationSection section = AquireSection(sectionName);
            var xml = section.SectionInformation.GetRawXml();
            var element = XElement.Parse(xml);

            element.Add(new XElement("add",
                new XAttribute("key", key),
                new XAttribute("value", value)
                ));

            section.SectionInformation.SetRawXml(element.ToString());
            userConfiguration.Save();
        }

        public void UpdateEntryInDictionarySection(string sectionName, string key, string newKey, string newValue,
            WriteToLogDelegate writeToLog)
        {
            AquireUserConfiguration();

            ConfigurationSection section = AquireSection(sectionName);
            var xml = section.SectionInformation.GetRawXml();
            var xmlDocument = new XmlDocument();
            bool found = false;

            xmlDocument.LoadXml(xml);

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
                userConfiguration.Save();
            }
            else
            {
                writeToLog($"The key {key} was not found in the file {userConfigFilePath}. It needs to "
                    + $"be changed manually in the file {applicationConfiguration.FilePath}.");
            }
        }

        public void RemoveEntryFromDictionarySection(string sectionName, string key, WriteToLogDelegate writeToLog)
        {
            AquireUserConfiguration();

            ConfigurationSection section = AquireSection(sectionName);
            var xml = section.SectionInformation.GetRawXml();
            var xmlDocument = new XmlDocument();
            bool found = false;

            xmlDocument.LoadXml(xml);

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
                userConfiguration.Save();
            }
            else
            {
                writeToLog($"The key {key} was not found in the file {userConfigFilePath}. It needs to "
                    + $"be changed manually in the file {applicationConfiguration.FilePath}.");
            }
        }


        public void SetValue<T>(string AppSettingKey, T value)
        {
            AquireUserConfiguration();

            if (value is string)
            {
                SetValueInUserConfiguration(AppSettingKey, value as string);
            }
            else
            {
                var stringValue = Convert.ToString(value, CultureInfo.InvariantCulture);
                SetValueInUserConfiguration(AppSettingKey, stringValue);
            }
        }

        public void Save()
        {
            // We are only making changes to the user configuration
            userConfiguration.Save();
        }

        #endregion

        #region Private static methods
        static void WriteParsingFailure(WriteToLogDelegate writeToLogDelegate, string configFile,
            string appSettingsKey, string value, Type type)
        {
            if (null != writeToLogDelegate)
            {
                writeToLogDelegate($"The configuration file {configFile} has a setting, {appSettingsKey}" +
                    $" having the value {value}, which cannot be parsed to a(n) {type}. " +
                    "The setting is therefore ignored.");
            }
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

        static TwoFilesConfiguration CreateConfiguration(Configuration applicationConfiguration, string userConfigFilePath,
            WriteToLogDelegate writeToLog = null)
        {
            if (!userConfigPathHasBeenShown && writeToLog != null)
            {
                userConfigPathHasBeenShown = true;

                writeToLog($"The files {userConfigFilePath} and {applicationConfiguration.FilePath} are used" +
                    " for the configuration settings.");

                if (!File.Exists(userConfigFilePath))
                {
                    writeToLog($" The file {userConfigFilePath} does not currently exist though, but will be" +
                               " automatically created if any connection string or setting is changed.");
                }
            }

            if (File.Exists(userConfigFilePath))
            {
                Configuration userConfiguration = OpenConfiguration(userConfigFilePath);
                return new TwoFilesConfiguration(applicationConfiguration, userConfigFilePath,
                    userConfiguration);
            }

            return new TwoFilesConfiguration(applicationConfiguration, userConfigFilePath,
                null);
        }

        static Configuration OpenConfiguration(string userFilePath)
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
        #endregion

        #region Private instance methods
        void AquireUserConfiguration()
        {
            if (userConfiguration == null)
            {
                EnsureUserFileExists();
                userConfiguration = OpenConfiguration(userConfigFilePath);
            }
        }

        void CreateDictionarySectionInUserConfigFile(string sectionName)
        {
            AquireUserConfiguration();

            var section = userConfiguration.GetSection(sectionName);

            if (null != section)
            {
                return; // Section already exists
            }

            var document = XDocument.Load(userConfigFilePath);

            CreateSectionUsingRawXml(document, sectionName);

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = indent
            };

            using (var writer = XmlWriter.Create(userConfigFilePath, settings))
            {
                document.Save(writer);
            }

            // Refresh the configuration object
            userConfiguration = null;
            AquireUserConfiguration();
        }

        ConfigurationSection AquireSection(string sectionName)
        {
            AquireUserConfiguration();

            var section = userConfiguration.GetSection(sectionName);

            if (null == section)
            {
                // Create the section in the user file
                CreateDictionarySectionInUserConfigFile(sectionName);

                section = userConfiguration.GetSection(sectionName);
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

        void SetValueInUserConfiguration(string AppSettingKey, string stringValue)
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
        #endregion
    }
}
