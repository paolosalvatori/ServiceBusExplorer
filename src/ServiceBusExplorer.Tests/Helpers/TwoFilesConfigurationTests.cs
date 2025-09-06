using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Linq;

using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus;
using FluentAssertions;
using Xunit;


namespace ServiceBusExplorer.Tests.Helpers
{
    public enum Monster
    {
        Godzilla,
        KingKong,
        Zombie
    }

    public enum Crustacean
    {
        Shrimp,
        Crab,
        Lobster
    }

    public class TwoFilesConfigurationTests
    {
        #region Constants
        // Common values
        const string KeyDoesNotExistAnywhere = "nonExistingKey";
        const string KeyWithInvalidValue = "ContainsInvalidValue";

        // Bool keys
        const string KeyIsInitiallyTrueInAppConfig = "useAscii";
        const string KeyIsAddedAndSetToTrueBeforeSecondTest = "willExistOnlyInUserConfig";

        // String testing constants
        const string TestDirectoryName = "Service Bus Explorer Tests";
        const string KeySharkWhichWillBeOverridden = "shark";
        const string ValueForOverridingShark = "Great white";
        const string AnotherShark = "Black reef shark";
        const string SharkValueInAppConfig = "Blue shark";

        // Enum testing constants
        const string KeyConnectivityModeWhichWillBeOverriddenOrOverwritten = "connectivityMode";
        const string KeyCrustaceanIsAddedAndSetToCrabBeforeSecondTest = "crustacean";

        // Decimal testing constants
        const string KeyWhaleWeightWillBeAddedBeforeSecondRun = "whaleWeight";
        const string KeySharkWeightWhichWillBeOverridden = "sharkWeight";
        const decimal ValueSharkWeightInAppConfig = 44.12M;
        const decimal ValueSharkWeightInUserConfig = 64.0M;
        const decimal ValueWhaleWeight8039And30 = 8039.30M;

        // Int testing constants
        const string KeyWhaleLengthWhichWillBeAddedBeforeSecondRun = "whaleLength";
        const string KeySharkLengthWhichWillBeOverridden = "sharkLength";
        const decimal ValueSharkLengthInitial158 = 158;
        const decimal ValueSharkLengthSet172 = 172;
        const decimal ValueWhaleLengthSet634 = 634;

        // Hashtable constants
        const string SectionFreshWaterFishesWhichDoesNotExistInitially = "freshWaterFishes";
        const string SectionSaltWaterFishesWhichWillBeMerged = "saltWaterFishes";
        const string ValueAlaskaPollock = "Alaska pollock";
        const string ValueAlaskaPollockOldScientificName = "Theragra chalcogramma";
        const string ValueAlaskaPollockNewScientificName = "Gadus chalcogrammus";
        const string KeyPike = "Pike";
        const string ValuePikeScienticName = "Esox lucius";

        // MessagingNamespaces constants

        const string ServiceBusNamespaces = "serviceBusNamespaces"; // For accessing configuration files

        const string KeyNamespaceAdded1 = "treasureInUserFile";
        const string KeyNamespaceAdded2 = "anotherTreasureInUserFile";
        const string KeyNamespaceInBothFiles = "usedInUserFileAndAppFile";
        const string KeyNamespaceInAppFile1 = "treasureInAppFile";

        readonly int IndexNamespaceAdded1 = 0;
        readonly int IndexNamespaceAdded2 = 1;
        readonly int IndexFirstNamespaceInBothFiles = 2;
        readonly int IndexSecondNamespaceInBothFiles = 3;
        readonly int IndexNamespaceInAppFile1 = 4;

        // Indent size in config files
        const string indent = "  ";
        #endregion

        #region Private fields
        WriteToLogDelegate writeToLog;
        string logInMemory;

        readonly Dictionary<string, string> someSaltWaterFishes = new Dictionary<string, string>()
         {
             { "Atlantic chub mackerel", "Scomber colias" },
             { "Atlantic mackerel", "Scomber scombrus" },
             { "Alaska pollock", "Theragra chalcogramma" }
         };

        readonly Dictionary<string, string> someFreshWaterFishes = new Dictionary<string, string>()
         {
             { KeyPike, ValuePikeScienticName },
             { "Perch", "Perca flavescens" },
             { "Zander","Sander lucioperca" }
         };

        readonly List<KeyValuePair<string, string>> fakeConnectionStrings =
            new List<KeyValuePair<string, string>>()
        {
             new KeyValuePair<string, string>("treasureInUserFile",
                 "Endpoint=sb://fake.servicebus.windows.net/;SharedAccessKeyName=SomeKey;SharedAccessKey=18347="),

             new KeyValuePair<string, string>("anotherTreasureInUserFile",
                 "Endpoint=sb://fake.servicebus.windows.net/;SharedAccessKeyName=Root;SharedAccessKey=21452="),

             new KeyValuePair<string, string>("usedInUserFileAndAppFile",
                 "Endpoint=sb://fake.servicebus.windows.net/;SharedAccessKeyName=UserFile;SharedAccessKey=32345="),

             new KeyValuePair<string, string>("usedInUserFileAndAppFile",
                 "Endpoint=sb://fake.servicebus.windows.net/;SharedAccessKeyName=AppFile;SharedAccessKey=444445="),

             new KeyValuePair<string, string>("treasureInAppFile",
                 "Endpoint=sb://fake.servicebus.windows.net/;SharedAccessKeyName=Root;SharedAccessKey=54442=")
        };

        readonly List<ConfigFileUse> configFileUses = new List<ConfigFileUse>()
         {
             ConfigFileUse.ApplicationConfig,
             ConfigFileUse.UserConfig,
             ConfigFileUse.BothConfig
         };
        #endregion

        #region The constructor
        public TwoFilesConfigurationTests()
        {
            // Initialize the delegate that handles logging
            writeToLog = WriteToLogInMemory;
        }
        #endregion

        #region Public methods   
        //TODO: Refactor Setup
        private void Setup()
        {
            // Reset application config file. It does not work well just making a copy
            // of the application config file since it may have been modified by a previous 
            // test run and the VS build system does not replace it with the app.config unless 
            // you do a Rebuild.
            ResetApplicationConfigFile();

            // Make sure the user config file does not exist
            DeleteUserConfigFile();

            // Empty the log buffer
            logInMemory = string.Empty;
        }


        [Fact]
        public void TestBoolValuesReadAndWrite()
        {
            foreach (var configFileUse in configFileUses)
            {
                // Cleanup before each run
                Setup();

                // Create the configuration object
                TwoFilesConfiguration configurationOpenedWithoutUserFile = TwoFilesConfiguration
                    .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values 
                TestReadingBoolValues(configurationOpenedWithoutUserFile, configHasBeenModified: false);

                // Set a value which will end up in the user file and already exists in the application config
                configurationOpenedWithoutUserFile.SetValue(KeyIsInitiallyTrueInAppConfig, false);

                // Set a value which will end up in the the appropriate file and does not exist in 
                // any file before
                configurationOpenedWithoutUserFile.SetValue(KeyIsAddedAndSetToTrueBeforeSecondTest, true);

                // Test reading config values again
                TestReadingBoolValues(configurationOpenedWithoutUserFile, configHasBeenModified: true);

                // Is this needed?
                configurationOpenedWithoutUserFile.Save();

                // Create the TwoFilesConfiguration object when a user file exists
                var configurationOpenedWithUserFile = TwoFilesConfiguration
                    .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values again 
                TestReadingBoolValues(configurationOpenedWithUserFile, configHasBeenModified: true);
            }
        }

        [Fact]
        public void TestEnumValuesReadAndWrite()
        {
            foreach (var configFileUse in configFileUses)
            {
                // Do the cleanup
                Setup();

                // Create the TwoFilesConfiguration object without a user file
                var configurationOpenedWithoutUserFile = TwoFilesConfiguration
                    .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values 
                TestReadingEnumValues(configurationOpenedWithoutUserFile, configHasBeenModified: false);

                // Set a value which depending upon configFileUse will end up either in in the user file 
                // or overwrite the existing value in the application config
                configurationOpenedWithoutUserFile.SetValue(KeyConnectivityModeWhichWillBeOverriddenOrOverwritten,
                    ConnectivityMode.Https);

                // Set a value which will end up in the user file and does not exist in the application config
                configurationOpenedWithoutUserFile.SetValue(KeyCrustaceanIsAddedAndSetToCrabBeforeSecondTest,
                    Crustacean.Crab);

                // Test reading config values again
                TestReadingEnumValues(configurationOpenedWithoutUserFile, configHasBeenModified: true);

                // Save the configuration to the user file
                configurationOpenedWithoutUserFile.Save();

                // Create the TwoFilesConfiguration object when a user file exists
                var configurationOpenedWithUserFile = TwoFilesConfiguration
                    .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values again 
                TestReadingEnumValues(configurationOpenedWithUserFile, configHasBeenModified: true);
            }
        }

        [Fact]
        public void TestDecimalValuesReadAndWrite()
        {
            foreach (var configFileUse in configFileUses)
            {
                // Do the cleanup
                Setup();

                // Create the TwoFilesConfiguration object without a user file
                var configurationOpenedWithoutUserFile = TwoFilesConfiguration
                .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values 
                TestReadingDecimalValues(configurationOpenedWithoutUserFile, configHasBeenModified: false);

                // Set a value which will end up in the user file and already exists in the application config
                configurationOpenedWithoutUserFile.SetValue(KeySharkWeightWhichWillBeOverridden,
                    ValueSharkWeightInUserConfig);

                // Set a value which will end up in the user file and does not exist in the application config
                configurationOpenedWithoutUserFile.SetValue(KeyWhaleWeightWillBeAddedBeforeSecondRun,
                    ValueWhaleWeight8039And30);

                // Test reading config values again
                TestReadingDecimalValues(configurationOpenedWithoutUserFile, configHasBeenModified: true);

                // Save the configuration to the user file
                configurationOpenedWithoutUserFile.Save();

                // Create the TwoFilesConfiguration object when a user file exists
                var configurationOpenedWithUserFile = TwoFilesConfiguration
                    .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values again 
                TestReadingDecimalValues(configurationOpenedWithUserFile, configHasBeenModified: true);
            }
        }

        [Fact]
        public void TestIntValuesReadAndWrite()
        {
            foreach (var configFileUse in configFileUses)
            {
                // Do the cleanup
                Setup();

                // Create the TwoFilesConfiguration object without a user file
                var configurationOpenedWithoutUserFile = TwoFilesConfiguration
                    .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading the app config values 
                TestReadingIntValues(configurationOpenedWithoutUserFile, configHasBeenModified: false);

                // Set an invalid value which will end up in the user file 
                configurationOpenedWithoutUserFile.SetValue(KeyWithInvalidValue, ValueAlaskaPollock);

                // Set a value which will end up in the user file and already exists in the 
                // application config
                configurationOpenedWithoutUserFile.SetValue(KeySharkLengthWhichWillBeOverridden,
                    ValueSharkLengthSet172);

                // Set a value which will end up in the user file and does not exist in the application config
                configurationOpenedWithoutUserFile.SetValue(KeyWhaleLengthWhichWillBeAddedBeforeSecondRun,
                    ValueWhaleLengthSet634);

                // Test reading config values again
                TestReadingIntValues(configurationOpenedWithoutUserFile, configHasBeenModified: true);

                // Save the configuration to the user file
                configurationOpenedWithoutUserFile.Save();

                // Create the TwoFilesConfiguration object when a user file exists
                var configurationOpenedWithUserFile = TwoFilesConfiguration
                    .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values again 
                TestReadingIntValues(configurationOpenedWithUserFile, configHasBeenModified: true);
            }
        }

        [Fact]
        public void TestStringValuesReadAndWrite()
        {
            foreach (var configFileUse in configFileUses)
            {
                // Do the cleanup
                Setup();

                // Create the TwoFilesConfiguration object without a user file
                var configurationOpenedWithoutUserFile = TwoFilesConfiguration
                .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values 
                TestReadingStringValues(configurationOpenedWithoutUserFile, configHasBeenModified: false);

                // Set a value which will end up in the user file and already exists in the application config
                configurationOpenedWithoutUserFile.SetValue(KeySharkWhichWillBeOverridden,
                    ValueForOverridingShark);

                // Set a value which will end up in the user file and does not exist in the application config
                configurationOpenedWithoutUserFile.SetValue(KeyIsAddedAndSetToTrueBeforeSecondTest, AnotherShark);

                // Test reading config values again
                TestReadingStringValues(configurationOpenedWithoutUserFile, configHasBeenModified: true);

                // Save the changes to the user file
                configurationOpenedWithoutUserFile.Save();

                // Create the TwoFilesConfiguration object when a user file exists
                var configurationOpenedWithUserFile = TwoFilesConfiguration
                    .Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values again 
                TestReadingStringValues(configurationOpenedWithUserFile, configHasBeenModified: true);
            }
        }

        [Fact]
        public void TestHashtableSectionReadAndWrite()
        {
            const string NonExistingSpecies = "NonexistingSpecies";

            foreach (var configFileUse in configFileUses)
            {
                // Do the cleanup
                Setup();

                // Create the TwoFilesConfiguration object without a user file
                var configurationOpenedWithoutUserFile = TwoFilesConfiguration.Create(GetUserSettingsFilePath()
                , configFileUse);

                // Test reading config values 
                TestReadingHashtableSection(configurationOpenedWithoutUserFile, configHasBeenModified: false);

                // Add a new entry to a section existing in the application config that should end up in the user config 
                configurationOpenedWithoutUserFile.AddEntryToDictionarySection(SectionSaltWaterFishesWhichWillBeMerged,
                    "Atlantic mackerel", "Scomber scombrus");

                // Add an existing entry to a section existing in the application config that 
                // should end up in the user config. 
                configurationOpenedWithoutUserFile.AddEntryToDictionarySection(SectionSaltWaterFishesWhichWillBeMerged,
                    "Alaska Pollock", ValueAlaskaPollockNewScientificName);

                foreach (var freshWaterFish in someFreshWaterFishes)
                {
                    configurationOpenedWithoutUserFile.AddEntryToDictionarySection
                        (SectionFreshWaterFishesWhichDoesNotExistInitially, freshWaterFish.Key,
                        freshWaterFish.Value);
                }

                // Persist the configuration
                configurationOpenedWithoutUserFile.Save();

                // Test reading config values again
                TestReadingHashtableSection(configurationOpenedWithoutUserFile, configHasBeenModified: true);

                // Add an  entry with a new value
                configurationOpenedWithoutUserFile.AddEntryToDictionarySection
                    (SectionFreshWaterFishesWhichDoesNotExistInitially, NonExistingSpecies,
                    "No name");

                // Delete the entry
                configurationOpenedWithoutUserFile.RemoveEntryFromDictionarySection
                    (SectionFreshWaterFishesWhichDoesNotExistInitially, NonExistingSpecies, writeToLog);

                // Test reading config values again
                TestReadingHashtableSection(configurationOpenedWithoutUserFile, configHasBeenModified: true);

                // Update an entry with a invalid value
                configurationOpenedWithoutUserFile.UpdateEntryInDictionarySection(
                    SectionFreshWaterFishesWhichDoesNotExistInitially,
                    KeyPike,
                    KeyPike,
                    "Wrong name",
                    writeToLog);

                // Update the previous entry with the correct value
                configurationOpenedWithoutUserFile.UpdateEntryInDictionarySection(
                    SectionFreshWaterFishesWhichDoesNotExistInitially,
                    KeyPike,
                    KeyPike,
                    ValuePikeScienticName,
                    writeToLog);

                // Test reading config values again
                TestReadingHashtableSection(configurationOpenedWithoutUserFile, configHasBeenModified: true);

                // Persist the configuration
                configurationOpenedWithoutUserFile.Save();

                // Create the TwoFilesConfiguration object when a user file exists
                var configurationOpenedWithUserFile = TwoFilesConfiguration.Create(GetUserSettingsFilePath(),
                    configFileUse);

                // Test reading config values again 
                TestReadingHashtableSection(configurationOpenedWithUserFile, configHasBeenModified: true);
            }
        }

        [Fact]
        public void TestMessagingNamespacesReadAndWrite()
        {
            foreach (var configFileUse in configFileUses)
            {
                // Do the cleanup
                Setup();
                RemoveNamespaceSectionFromApplicationFile();

                // Create the TwoFilesConfiguration object without a user file
                var configuration = TwoFilesConfiguration.Create(GetUserSettingsFilePath(), configFileUse);

                // Test reading config values - both application config and user config are missing
                var namespaces = ServiceBusNamespace.GetMessagingNamespaces(configuration, writeToLog);
                namespaces.Should().BeEmpty();

                logInMemory.Should().Contain("Service bus accounts have not been properly configured");
                logInMemory = string.Empty;

                // Create two connection strings in the user file or the application file depending upon
                // configFileUse.
                SaveConnectionString(configuration, IndexNamespaceAdded1);
                SaveConnectionString(configuration, IndexNamespaceAdded2);
                logInMemory.Should().BeEmpty();

                namespaces = ServiceBusNamespace.GetMessagingNamespaces(configuration, writeToLog);
                namespaces.Should().HaveCount(2);

                logInMemory.Should().BeEmpty();


                fakeConnectionStrings[IndexNamespaceAdded1].Value.Should().Be(namespaces[KeyNamespaceAdded1].ConnectionString);
                fakeConnectionStrings[IndexNamespaceAdded2].Value.Should().Be(namespaces[KeyNamespaceAdded2].ConnectionString);


                // Add a connection to the application config file, but let the two connection 
                // strings added previously stay.
                SaveConnectionStringInApplicationFile(IndexSecondNamespaceInBothFiles);
                configuration = TwoFilesConfiguration.Create(GetUserSettingsFilePath(),
                    configFileUse);
                namespaces = ServiceBusNamespace.GetMessagingNamespaces(configuration, writeToLog);

                if (UseApplicationConfig(configFileUse))
                {
                    namespaces.Should().HaveCount(3);
                    fakeConnectionStrings[IndexSecondNamespaceInBothFiles].Value.Should().Be(namespaces[KeyNamespaceInBothFiles].ConnectionString);
                }
                else
                {
                    namespaces.Should().HaveCount(2);
                }

                fakeConnectionStrings[IndexNamespaceAdded1].Value.Should().Be(namespaces[KeyNamespaceAdded1].ConnectionString);
                fakeConnectionStrings[IndexNamespaceAdded2].Value.Should().Be(namespaces[KeyNamespaceAdded2].ConnectionString);


                // Add a connection string to the user file with the same index as an existing entry 
                // in the application file. Depending upon ConfigFileUse setting there are 
                // either three strings in the app config or one in the app and three in the user with
                // two having the same key.
                SaveConnectionString(configuration, IndexFirstNamespaceInBothFiles);

                configuration = TwoFilesConfiguration.Create(GetUserSettingsFilePath(), configFileUse);
                namespaces = ServiceBusNamespace.GetMessagingNamespaces(configuration, writeToLog);

                namespaces.Should().HaveCount(3);
                fakeConnectionStrings[IndexNamespaceAdded1].Value.Should().Be(namespaces[KeyNamespaceAdded1].ConnectionString);
                fakeConnectionStrings[IndexNamespaceAdded2].Value.Should().Be(namespaces[KeyNamespaceAdded2].ConnectionString);

                fakeConnectionStrings[IndexFirstNamespaceInBothFiles].Value.Should().Be(namespaces[KeyNamespaceInBothFiles].ConnectionString);

                // Add a connection string to the application file
                SaveConnectionStringInApplicationFile(IndexNamespaceInAppFile1);
                configuration = TwoFilesConfiguration.Create(GetUserSettingsFilePath(), configFileUse);
                namespaces = ServiceBusNamespace.GetMessagingNamespaces(configuration, writeToLog);

                // Depending upon ConfigFileUse setting there are
                // either four strings in the app config or two in the app and three in the user with
                // two having the same key.
                if (UseApplicationConfig(configFileUse))
                {
                    namespaces.Should().HaveCount(4);
                    fakeConnectionStrings[IndexNamespaceInAppFile1].Value.Should().Be(namespaces[KeyNamespaceInAppFile1].ConnectionString);
                }
                else
                {
                    namespaces.Should().HaveCount(3);
                }

                fakeConnectionStrings[IndexNamespaceAdded1].Value.Should().Be(namespaces[KeyNamespaceAdded1].ConnectionString);
                fakeConnectionStrings[IndexNamespaceAdded2].Value.Should().Be(namespaces[KeyNamespaceAdded2].ConnectionString);

                fakeConnectionStrings[IndexFirstNamespaceInBothFiles].Value.Should().Be(namespaces[KeyNamespaceInBothFiles].ConnectionString);


                // Delete the user file so reading will only be from the application file
                DeleteUserConfigFile();
                configuration = TwoFilesConfiguration.Create(GetUserSettingsFilePath(), configFileUse);
                namespaces = ServiceBusNamespace.GetMessagingNamespaces(configuration, writeToLog);

                if (UseApplicationConfig(configFileUse))
                {
                    logInMemory.Should().BeEmpty();
                }
                else
                {
                    logInMemory.Should().Contain("not been properly configured");
                }

                if (UseApplicationConfig(configFileUse))
                {
                    (configFileUse == ConfigFileUse.ApplicationConfig ? 4 : 2).Should().Be(namespaces.Count);

                    if (configFileUse == ConfigFileUse.ApplicationConfig)
                    {
                        fakeConnectionStrings[IndexNamespaceAdded1].Value.Should().Be(namespaces[KeyNamespaceAdded1].ConnectionString);
                        fakeConnectionStrings[IndexNamespaceAdded2].Value.Should().Be(namespaces[KeyNamespaceAdded2].ConnectionString);
                        fakeConnectionStrings[IndexFirstNamespaceInBothFiles].Value.Should().Be(namespaces[KeyNamespaceInBothFiles].ConnectionString);
                    }
                    else
                    {
                        fakeConnectionStrings[IndexSecondNamespaceInBothFiles].Value.Should().Be(namespaces[KeyNamespaceInBothFiles].ConnectionString);
                    }

                    fakeConnectionStrings[IndexNamespaceInAppFile1].Value.Should().Be(namespaces[KeyNamespaceInAppFile1].ConnectionString);
                }
                else
                {
                    namespaces.Should().BeEmpty();
                }
            }
        }

        #endregion

        #region Private instance methods

        // Since we are bypassing the Configuration class for this the existing Configuration object(s)
        // become invalid.
        void SaveConnectionStringInApplicationFile(int index)
        {
            EnsureSectionExistsInApplicationConfig(ServiceBusNamespaces);
            var appConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = appConfiguration.GetSection(ServiceBusNamespaces);

            var xml = section.SectionInformation.GetRawXml();
            var element = XElement.Parse(xml);

            element.Add(new XElement("add",
                new XAttribute("key", fakeConnectionStrings[index].Key),
                new XAttribute("value", fakeConnectionStrings[index].Value)
                ));

            section.SectionInformation.SetRawXml(element.ToString());
            appConfiguration.Save();
            ConfigurationManager.RefreshSection(ServiceBusNamespaces);
        }

        void EnsureSectionExistsInApplicationConfig(string sectionName)
        {
            var applicationConfiguration = ConfigurationManager.OpenExeConfiguration
                (ConfigurationUserLevel.None);
            var section = applicationConfiguration.GetSection(sectionName);

            if (null != section)
            {
                return; // Section already exists
            }

            var document = XDocument.Load(applicationConfiguration.FilePath);

            CreateSectionUsingRawXml(document, sectionName);

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = indent
            };

            using (var writer = XmlWriter.Create(applicationConfiguration.FilePath, settings))
            {
                document.Save(writer);
            }

            // Refresh the configuration object
            ConfigurationManager.RefreshSection(sectionName);
        }

        void RemoveNamespaceSectionFromApplicationFile()
        {
            var applicationConfiguration = ConfigurationManager
                .OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = applicationConfiguration.GetSection(ServiceBusNamespaces);

            if (null != section)
            {
                applicationConfiguration.Sections.Remove(ServiceBusNamespaces);
                section.SectionInformation.ForceSave = true;
                applicationConfiguration.Save(ConfigurationSaveMode.Full);
            }
        }

        void SaveConnectionString(TwoFilesConfiguration configuration, int index)
        {
            logInMemory.Should().BeEmpty();
            ServiceBusNamespace.SaveConnectionString(configuration, fakeConnectionStrings[index].Key, fakeConnectionStrings[index].Value, writeToLog);
            logInMemory.Should().BeEmpty();
        }

        void DeleteUserConfigFile()
        {
            DeleteFile(GetUserSettingsFilePath());
        }

        void TestReadingBoolValues(TwoFilesConfiguration configuration, bool configHasBeenModified)
        {
            const string keyOnlyInAppConfig = "savePropertiesToFile";

            // Get a value that do not exist in the application config file defaulting to true
            var nonExistingValueAsTrue = configuration.GetBoolValue(KeyDoesNotExistAnywhere, true, writeToLog);
            true.Should().Be(nonExistingValueAsTrue);

            // Get a value that do not exist in the application config file defaulting to false
            var nonExistingValueAsFalse = configuration.GetBoolValue(KeyDoesNotExistAnywhere, false, writeToLog);
            false.Should().Be(nonExistingValueAsFalse);

            // Get the value from the user file defaulting to true. If userFileShouldHaveUseAsciiKeyAsFalse
            // is false then we should read from the application config and that value should be true
            var useAsciiDefTrue = configuration.GetBoolValue(KeyIsInitiallyTrueInAppConfig, true, writeToLog);

            configHasBeenModified.Should().NotBe(useAsciiDefTrue);

            // Get a value from that initially only exist in the application file defaulting to false
            var useAsciiDefFalse = configuration.GetBoolValue(KeyIsInitiallyTrueInAppConfig, false, writeToLog);
            bool expectedUseAsciiDef;

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                expectedUseAsciiDef = !configHasBeenModified;
            }
            else
            {
                expectedUseAsciiDef = false;
            }

            expectedUseAsciiDef.Should().Be(useAsciiDefFalse);

            // Get a value that does not exist in the user file
            var savePropertiesToFile = configuration.GetBoolValue(keyOnlyInAppConfig, false, writeToLog);

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                savePropertiesToFile.Should().BeTrue();
            }
            else
            {
                savePropertiesToFile.Should().BeFalse();
            }

            // Get a value that will only exist in the user file
            var onlyInUserFile = configuration.GetBoolValue(KeyIsAddedAndSetToTrueBeforeSecondTest, false, writeToLog);
            onlyInUserFile.Should().Be(configHasBeenModified);
        }

        void TestReadingEnumValues(TwoFilesConfiguration configuration, bool configHasBeenModified)
        {
            const string keyOnlyInAppConfig = "monster";

            // Get a value that do not exist in any of the config files using default
            var nonExistingValueAsDefault = configuration.GetEnumValue(KeyDoesNotExistAnywhere, default(RelayType), writeToLog);

            RelayType.None.Should().Be(nonExistingValueAsDefault);

            // Get a value that do not exist in any of the config files defaulting to false
            var nonExistingValueAsNetEvent = configuration.GetEnumValue(KeyDoesNotExistAnywhere, RelayType.NetEvent, writeToLog);

            RelayType.NetEvent.Should().Be(nonExistingValueAsNetEvent);

            // If configHasBeenModified is false then we should read get the original value which should be
            // AutoDetect. Otherwise the value is Https.
            var connectivityMode = configuration.GetEnumValue<ConnectivityMode>(KeyConnectivityModeWhichWillBeOverriddenOrOverwritten, default, writeToLog);

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                (configHasBeenModified ? ConnectivityMode.Https : ConnectivityMode.AutoDetect).Should().Be(connectivityMode);
            }
            else  // Just user config file
            {
                // ConnectivityMode.Http is the default
                (configHasBeenModified ? ConnectivityMode.Https : ConnectivityMode.Http).Should().Be(connectivityMode);
            }

            // Get a value that do exist initially
            var monster = configuration.GetEnumValue(keyOnlyInAppConfig,
                default(Monster), writeToLog);

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                monster.Should().Be(Monster.KingKong);
            }
            else
            {
                monster.Should().Be(Monster.Godzilla);
            }

            // Get a value that will be added after the first test
            var crustacean = configuration.GetEnumValue(KeyCrustaceanIsAddedAndSetToCrabBeforeSecondTest, Crustacean.Shrimp, writeToLog);

            (configHasBeenModified ? Crustacean.Crab : Crustacean.Shrimp).Should().Be(crustacean);
        }

        void TestReadingIntValues(TwoFilesConfiguration configuration, bool configHasBeenModified)
        {
            const string keyOnlyInAppConfig = "morayEelLength";
            const int mediumNumber = 13500;

            // If the user file exists it should have an invalid value 
            if (configHasBeenModified)
            {
                configuration.SettingExists(KeyWithInvalidValue).Should().BeTrue();

                var shouldBeDefault = configuration.GetIntValue(KeyWithInvalidValue, mediumNumber, writeToLog);

                mediumNumber.Should().Be(shouldBeDefault);

                logInMemory.Should().Contain("which cannot be parsed to a(n) System.Int32");
                logInMemory = string.Empty;
            }

            // Get a value that do not exist in any config config file defaulting to empty
            configuration.SettingExists(KeyDoesNotExistAnywhere).Should().BeFalse();
            var nonExistingValueAsDefault = configuration.GetIntValue(KeyDoesNotExistAnywhere, default, writeToLog);

            nonExistingValueAsDefault.Should().Be(0);

            // Get a value that does not exist in the any config file defaulting to mediumNumber
            var nonExistingValueAsMediumNumber = configuration.GetIntValue(KeyDoesNotExistAnywhere, mediumNumber, writeToLog);

            mediumNumber.Should().Be(nonExistingValueAsMediumNumber);

            // Get the value from the user file defaulting to empty. If configHasBeenModified
            // is false then we should read from the application config and that value should be
            // ValueSharkLengthInAppConfig
            var sharkLength = configuration.GetIntValue(KeySharkLengthWhichWillBeOverridden,
                default, writeToLog);
            int expectedLength;

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                expectedLength = configHasBeenModified ? (int)ValueSharkLengthSet172 : (int)ValueSharkLengthInitial158;
            }
            else // Just user config
            {
                expectedLength = configHasBeenModified ? (int)ValueSharkLengthSet172 : default;
            }

            expectedLength.Should().Be(sharkLength);

            const int largeNumber = 3450242;
            // Get the value from the user file defaulting to a large number
            sharkLength = configuration.GetIntValue(KeySharkLengthWhichWillBeOverridden, largeNumber, writeToLog);

            if (configuration.ConfigFileUse == ConfigFileUse.UserConfig)
            {
                if (configHasBeenModified)
                {
                    expectedLength = (int)ValueSharkLengthSet172;
                }
                else
                {
                    expectedLength = largeNumber;
                }
            }

            expectedLength.Should().Be(sharkLength);

            // Get a value that only exists in the application config file
            const int valueMorayEelLength = 214;
            var morayEelLength = configuration.GetIntValue(keyOnlyInAppConfig, default, writeToLog);

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                valueMorayEelLength.Should().Be(morayEelLength);
            }
            else
            {
                morayEelLength.Should().Be(default);
            }

            // Get a value that will only exist in the user file or overridden in the application file
            var whaleLengthValue = configuration.GetIntValue(KeyWhaleLengthWhichWillBeAddedBeforeSecondRun,
                mediumNumber, writeToLog);

            expectedLength = configHasBeenModified ? (int)ValueWhaleLengthSet634 : mediumNumber;

            expectedLength.Should().Be(whaleLengthValue);
        }

        void TestReadingDecimalValues(TwoFilesConfiguration configuration, bool configHasBeenModified)
        {
            const string keyOnlyInAppConfig = "morayEelWeight";
            const decimal mediumNumber = 52.88M;

            // Get a value that do not exist in the application config file defaulting to empty
            var nonExistingValueAsDefault = configuration.GetDecimalValue(KeyDoesNotExistAnywhere, default, writeToLog);

            nonExistingValueAsDefault.Should().Be(0);

            // Get a value that do not exist in the application config file defaulting to mediumNumber
            var nonExistingValueAsMediumNumber = configuration.GetDecimalValue(KeyDoesNotExistAnywhere, mediumNumber, writeToLog);

            nonExistingValueAsMediumNumber.Should().Be(mediumNumber);

            // If configHasBeenModified is false then we should read from the application config 
            // and that value should be ValueSharkWeightInAppConfig.  Get the value from the user 
            // file defaulting to empty. 
            var sharkWeight = configuration.GetDecimalValue(KeySharkWeightWhichWillBeOverridden, default, writeToLog);

            decimal expectedWeight;

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                expectedWeight = configHasBeenModified ? ValueSharkWeightInUserConfig : ValueSharkWeightInAppConfig;
            }
            else
            {
                expectedWeight = configHasBeenModified ? ValueSharkWeightInUserConfig : default;
            }

            expectedWeight.Should().Be(sharkWeight);


            // Get the value from the user file defaulting to a large number
            const decimal largeNumber = 54789276579M;
            sharkWeight = configuration.GetDecimalValue(KeySharkWeightWhichWillBeOverridden,
                largeNumber, writeToLog);

            if (!configHasBeenModified && configuration.ConfigFileUse == ConfigFileUse.UserConfig)
            {
                expectedWeight = largeNumber;
            }

            expectedWeight.Should().Be(sharkWeight);

            // Get a value that do not exist in the user file
            const decimal morayEelWeightInAppConfig = 588M;

            var morayEelWeight = configuration.GetDecimalValue(keyOnlyInAppConfig, default, writeToLog);

            decimal expectedMorayEelWeight;
            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                expectedMorayEelWeight = morayEelWeightInAppConfig;
            }
            else
            {
                expectedMorayEelWeight = default;
            }

            expectedMorayEelWeight.Should().Be(morayEelWeight);

            // Get a value that does not exist in the application config initially
            var onlyInUserFile = configuration.GetDecimalValue(KeyWhaleWeightWillBeAddedBeforeSecondRun,
                mediumNumber, writeToLog);
            expectedWeight = configHasBeenModified ? ValueWhaleWeight8039And30 : mediumNumber;
            expectedWeight.Should().Be(onlyInUserFile);
        }


        void TestReadingStringValues(TwoFilesConfiguration configuration, bool configHasBeenModified)
        {
            const string keyOnlyInAppConfig = "whale";
            const string ExtinctShark = "Megalodon";

            // Get a value that do not exist in the application config file defaulting to empty
            var nonExistingValueAsTrue = configuration.GetStringValue(KeyDoesNotExistAnywhere, string.Empty);
            nonExistingValueAsTrue.Should().BeEmpty();

            // Get a value that do not exist in any config file defaulting to false
            var nonExistingValueAsFalse = configuration.GetStringValue(KeyDoesNotExistAnywhere, ExtinctShark);
            nonExistingValueAsFalse.Should().Be(ExtinctShark);

            // Get the value from the user file defaulting to empty. If configHasBeenModified
            // is false then we should read from the application config and that value should be
            // sharkValueInAppConfig
            var sharkSpecies = configuration.GetStringValue(KeySharkWhichWillBeOverridden);

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                (configHasBeenModified ? ValueForOverridingShark : SharkValueInAppConfig).Should().Be(sharkSpecies);
            }
            else // Just user config
            {
                (configHasBeenModified ? ValueForOverridingShark : string.Empty).Should().Be(sharkSpecies);
            }

            // Get the value from the user file defaulting to false
            const string valueWhiteTipReefShark = "White tip reef shark";

            sharkSpecies = configuration.GetStringValue(KeySharkWhichWillBeOverridden, valueWhiteTipReefShark);

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                (configHasBeenModified ? ValueForOverridingShark : SharkValueInAppConfig).Should().Be(sharkSpecies);
            }
            else
            {
                (configHasBeenModified ? ValueForOverridingShark : valueWhiteTipReefShark).Should().Be(sharkSpecies);
            }

            // Get a value that do not exist in the user file
            const string valueGrayWhale = "Gray whale";
            var whale = configuration.GetStringValue(keyOnlyInAppConfig);

            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                valueGrayWhale.Should().Be(whale);
            }
            else
            {
                whale.Should().BeEmpty();
            }

            // Get a value that will only exist in the user file
            var onlyInUserFile = configuration.GetStringValue(KeyIsAddedAndSetToTrueBeforeSecondTest, ExtinctShark);

            onlyInUserFile.Should().Be(configHasBeenModified ? AnotherShark : ExtinctShark);
        }

        void TestReadingHashtableSection(TwoFilesConfiguration configuration,
            bool configHasBeenModified)
        {
            // Get a value that do not exist in the application config file defaulting to empty
            var nonExistingSection = configuration.GetHashtableFromSection(KeyDoesNotExistAnywhere);
            nonExistingSection.Should().BeNull();

            var saltWaterFishes = configuration.GetHashtableFromSection(SectionSaltWaterFishesWhichWillBeMerged);

            // Get the Hashtable of the saltwater fishes. There are two of them in the app config. 
            // Two more are added before the second run.
            if (UseApplicationConfig(configuration.ConfigFileUse))
            {
                if (!configHasBeenModified)
                {
                    saltWaterFishes.Count.Should().Be(2);
                    saltWaterFishes["Alaska pollock"].Should().Be(ValueAlaskaPollockOldScientificName);
                }
                else
                {
                    saltWaterFishes.Count.Should().Be(3);
                    saltWaterFishes["Alaska pollock"].Should().Be(ValueAlaskaPollockNewScientificName);
                    saltWaterFishes["Atlantic mackerel"].Should().Be("Scomber scombrus");
                }

                saltWaterFishes["Atlantic chub mackerel"].Should().Be("Scomber colias");
            }
            else
            {
                if (!configHasBeenModified)
                {
                    saltWaterFishes.Should().BeNull();
                }
                else
                {
                    saltWaterFishes.Count.Should().Be(2);
                    saltWaterFishes["Atlantic mackerel"].Should().Be("Scomber scombrus");
                    saltWaterFishes["Alaska Pollock"].Should().Be(ValueAlaskaPollockNewScientificName);
                }
            }

            // Read a section that only exists in the user file
            var freshWaterFishes = configuration.GetHashtableFromSection
                (SectionFreshWaterFishesWhichDoesNotExistInitially);

            if (!configHasBeenModified)
            {
                if (ConfigFileUse.ApplicationConfig == configuration.ConfigFileUse)
                {
                    freshWaterFishes.Should().BeNull();
                }
                else
                {
                    freshWaterFishes.Should().BeNull();
                }
            }
            else
            {
                freshWaterFishes.Count.Should().Be(3);
                freshWaterFishes["Perch"].Should().Be("Perca flavescens");
                freshWaterFishes["Zander"].Should().Be("Sander lucioperca");
                freshWaterFishes["Pike"].Should().Be("Esox lucius");
            }
        }

        void WriteToLogInMemory(string message, bool async = true)
        {
            logInMemory += message;
        }

        void ResetApplicationConfigFile()
        {
            var configurationApplicationOnly = TwoFilesConfiguration.Create(null,
                ConfigFileUse.ApplicationConfig, WriteToLogInMemory);

            configurationApplicationOnly.SetValue(KeyIsInitiallyTrueInAppConfig, true);
            configurationApplicationOnly.SetValue(KeyConnectivityModeWhichWillBeOverriddenOrOverwritten,
                ConnectivityMode.AutoDetect);
            configurationApplicationOnly.SetValue(KeySharkWeightWhichWillBeOverridden,
                ValueSharkWeightInAppConfig);
            configurationApplicationOnly.SetValue(KeySharkLengthWhichWillBeOverridden,
                ValueSharkLengthInitial158);
            configurationApplicationOnly.SetValue(KeySharkWhichWillBeOverridden,
                SharkValueInAppConfig);

            // Remove the freshwater fish section entirely
            configurationApplicationOnly.RemoveSection(
                SectionFreshWaterFishesWhichDoesNotExistInitially, WriteToLogInMemory);

            // Clean up the saltwater fish section
            configurationApplicationOnly.RemoveEntryFromDictionarySection(SectionSaltWaterFishesWhichWillBeMerged,
                "Atlantic mackerel", WriteToLogInMemory);
            configurationApplicationOnly.RemoveEntryFromDictionarySection(SectionSaltWaterFishesWhichWillBeMerged,
                "Alaska Pollock", WriteToLogInMemory);

            configurationApplicationOnly.Save();


            var appConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = appConfiguration.GetSection("appSettings");

            var keysToRemove = new List<string>()
             {
                 KeyIsAddedAndSetToTrueBeforeSecondTest,
                 KeyCrustaceanIsAddedAndSetToCrabBeforeSecondTest,
                 KeyWhaleWeightWillBeAddedBeforeSecondRun,
                 KeyWhaleLengthWhichWillBeAddedBeforeSecondRun,
                 KeyWithInvalidValue
             };

            RemoveKeysUsingRawXml(section, keysToRemove);
            appConfiguration.Save(ConfigurationSaveMode.Modified, forceSaveAll: true);

            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("serviceBusNamespaces");
        }
        #endregion

        #region Private static methods
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
        static void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        static string GetUserSettingsFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                TestDirectoryName,
                "UserSettings.config");
        }

        static void RemoveKeysUsingRawXml(ConfigurationSection section, List<string> keysToRemove)
        {
            if (null == section)
            {
                return;
            }

            var xml = section.SectionInformation.GetRawXml();
            var xmlDocument = new XmlDocument();
            bool found = false;

            xmlDocument.LoadXml(xml);

            var childNodes = xmlDocument.DocumentElement.ChildNodes;

            for (int i = childNodes.Count - 1; i >= 0; --i)
            {
                var child = (XmlElement)childNodes[i];

                foreach (var key in keysToRemove)
                {

                    if (child.LocalName == "add" && child.GetAttribute("key") == key)
                    {
                        found = true;
                        child.ParentNode.RemoveChild(child);
                        break; // This will only break out of the inner loop
                    }
                }
            }

            if (found)
            {
                section.SectionInformation.SetRawXml(xmlDocument.OuterXml);
            }
        }

        static bool UseApplicationConfig(ConfigFileUse configFileUse)
        {
            return ((configFileUse & ConfigFileUse.AccessApplicationConfig) != ConfigFileUse.None);
        }
        #endregion
    }
}
