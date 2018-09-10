using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    public static class ConfigurationHelper
    {
        private static readonly string SERVICEBUS_SECTION_NAME = "serviceBusNamespaces";

        #region Public methods

        public static void UpdateServiceBusNamespace(string key, string newKey = null, string newValue = null)
        {
            var configuration = GetExeConfiguration();

            UpdateServiceBusElement(configuration, key, newKey, newValue);

            // Update config file for development
            configuration = GetDevelopmentConfiguration();
            if (configuration != null)
            { 
                UpdateServiceBusElement(configuration, key, newKey, newValue);
            }
        }

        public static void AddServiceBusNamespace(string key, string value)
        {
            var configuration = GetExeConfiguration();
            AddServiceBusElement(configuration, key, value);

            configuration = GetDevelopmentConfiguration();
            if (configuration != null)
            {
                AddServiceBusElement(configuration, key, value);
            }
        }

        public static void RemoveServiceBusNamespace(string key)
        {
            var configuration = GetExeConfiguration();
            RemoveServiceBusElement(configuration, key);

            configuration = GetDevelopmentConfiguration();
            if (configuration != null)
            {
                RemoveServiceBusElement(configuration, key);
            }
        }
        
        #endregion

        #region Private methods

        private static void AddServiceBusElement(Configuration configuration, string key, string value)
        {
            var configurationSection = configuration.Sections[SERVICEBUS_SECTION_NAME];
            configurationSection.SectionInformation.ForceSave = true;
            var xml = configurationSection.SectionInformation.GetRawXml();
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            var node = xmlDocument.CreateElement("add");
            node.SetAttribute("key", key);
            node.SetAttribute("value", value);
            xmlDocument.DocumentElement?.AppendChild(node);

            configurationSection.SectionInformation.SetRawXml(xmlDocument.OuterXml);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(SERVICEBUS_SECTION_NAME);
        }

        private static void RemoveServiceBusElement(Configuration configuration, string key)
        {
            var configurationSection = configuration.Sections[SERVICEBUS_SECTION_NAME];
            configurationSection.SectionInformation.ForceSave = true;
            var xml = configurationSection.SectionInformation.GetRawXml();
            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            foreach (XmlElement child in xmlDocument.DocumentElement.ChildNodes)
            {
                if (child.LocalName == "add" && child.GetAttribute("key") == key)
                {
                    child.ParentNode.RemoveChild(child);
                    break;
                }
            }
            configurationSection.SectionInformation.SetRawXml(xmlDocument.OuterXml);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(SERVICEBUS_SECTION_NAME);
        }

        private static void UpdateServiceBusElement(Configuration configuration, string key, string newKey, string newValue)
        {
            var configurationSection = configuration.Sections[SERVICEBUS_SECTION_NAME];
            configurationSection.SectionInformation.ForceSave = true;
            var xml = configurationSection.SectionInformation.GetRawXml();
            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            foreach (XmlElement child in xmlDocument.DocumentElement.ChildNodes)
            {
                if (child.LocalName == "add" && child.GetAttribute("key") == key)
                {
                    if (!string.IsNullOrEmpty(newKey)) child.SetAttribute("key", newKey);
                    if (!string.IsNullOrEmpty(newValue)) child.SetAttribute("value", newValue);
                    break;
                }
            }

            configurationSection.SectionInformation.SetRawXml(xmlDocument.OuterXml);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(SERVICEBUS_SECTION_NAME);
        }

        private static Configuration GetExeConfiguration()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var directory = Path.GetDirectoryName(configuration.FilePath);
            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentNullException("The directory of the configuration file cannot be null.");
            }
            return configuration;
        }

        private static Configuration GetDevelopmentConfiguration()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var directory = Path.GetDirectoryName(configuration.FilePath);
            var appConfig = Path.Combine(directory, "..\\..\\App.config");
            if (File.Exists(appConfig))
            {
                var exeConfigurationFileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = appConfig
                };
                configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
                return configuration;
            }
            return null;
        }

        #endregion  
    }
}
