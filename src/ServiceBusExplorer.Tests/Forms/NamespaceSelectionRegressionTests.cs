using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Common.Helpers;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public partial class ConnectFormEntraTests
    {
        private const string PortProfile = LocalSas + ";ManagementPort=14443;RuntimePort=15671;EntityPath=orders;TransportType=Amqp";
        private const string EnvironmentKey = @"HKEY_CURRENT_USER\Environment connection string";

        [Fact]
        public void SavedSasPortsSurviveConnectAndSaveWithFieldEdits() =>
            WithProfileConfiguration(CheckSavedPorts, storedProfile: PortProfile);

        private static void CheckSavedPorts()
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                using var log = new BlockingCollection<string>();
                CreateProfileTestMainForm(log);
                var config = TwoFilesConfiguration.Create(ConfigFileUse.ApplicationConfig);
                var helper = new ServiceBusHelper((_, __) => { });
                helper.ServiceBusNamespaces = ServiceBusNamespace.GetMessagingNamespaces(config, (_, __) => { });
                using (var dialog = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null))
                {
                    dialog.Show();
                    dialog.SelectProfile(SavedProfileKey);
                    FindControl<RadioButton>(dialog, "radioServiceBus").PerformClick();
                    FindControl<Button>(dialog, "btnOk").PerformClick();
                    Assert.Equal(DialogResult.OK, dialog.DialogResult);
                    Assert.Equal(PortProfile, dialog.ConnectionString);
                }

                using (var dialog = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null))
                {
                    dialog.Show();
                    dialog.SelectProfile(SavedProfileKey);
                    FindControl<RadioButton>(dialog, "radioServiceBus").PerformClick();
                    GetTextBox(dialog, ControlNameIssuerName).Text = "rotated";
                    GetTextBox(dialog, ControlNameUri).Text = "sb://other.invalid/";
                    GetTextBox(dialog, ControlNameEntityPath).Clear();
                    GetComboBox(dialog, ControlNameTransportType).SelectedItem = TransportType.NetMessaging;
                    FindControl<Button>(dialog, "btnSave").PerformClick();
                }

                var reloaded = ServiceBusNamespace.GetMessagingNamespaces(
                    TwoFilesConfiguration.Create(ConfigFileUse.ApplicationConfig), (_, __) => { })[SavedProfileKey];
                Assert.Equal("rotated", reloaded.SharedAccessKeyName);
                Assert.Equal("sb://other.invalid/", reloaded.Uri);
                Assert.Equal(TransportType.NetMessaging, reloaded.TransportType);
                Assert.DoesNotContain("EntityPath=", reloaded.ConnectionString);
                Assert.Contains("other.invalid:14443", ServiceBusHelper.GetAdministrationConnectionString(reloaded));
                Assert.Contains("other.invalid:15671", ServiceBusHelper.GetDataPlaneConnectionString(reloaded));
                Assert.Equal(NamespaceService.ServiceBus, reloaded.Selection.Service);
            });
        }

        [Fact]
        public void PastingEmulatorKeepsExplicitCustomGroups()
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                using var dialog = new ConnectForm(new ServiceBusHelper((_, __) => { }), ConfigFileUse.ApplicationConfig, null);
                dialog.Show();
                GetComboBox(dialog, ControlNameServiceBusNamespace).SelectedIndex = 1;
                FindControl<RadioButton>(dialog, "radioCustom").PerformClick();
                foreach (var item in GetCheckBoxComboBox(dialog, ControlNameSelectedEntities).CheckBoxItems)
                    item.Checked = item.Text == Constants.QueueEntities;
                GetTextBox(dialog, ControlNameUri).Text = SavedEmulator;
                Assert.True(FindControl<RadioButton>(dialog, "radioCustom").Checked);
                Assert.Equal(Constants.QueueEntities, Assert.Single(dialog.SelectedEntities));
                FindControl<Button>(dialog, "btnOk").PerformClick();
                Assert.True(dialog.TryConnectToNamespace(out var profile));
                Assert.Equal(Constants.QueueEntities, Assert.Single(profile.GetDiscoveryEntities(false, true)));
            });
        }

        [Fact]
        public void ExternalPicksSurviveReloadWithoutCopyingCredentials() =>
            WithProfileConfiguration(CheckExternalSources, storedProfile: LocalSas, legacySetting: LocalSas);

        private static void CheckExternalSources()
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                const string env = "Endpoint=sb://localhost/;SharedAccessKeyName=env;SharedAccessKey=environmentOnly";
                using var log = new BlockingCollection<string>();
                var main = CreateProfileTestMainForm(log);
                using var menu = new ToolStripMenuItem();
                var helper = new ServiceBusHelper((_, __) => { });
                SetMainField(main, "serviceBusHelper", helper);
                SetMainField(main, "configFileUse", ConfigFileUse.ApplicationConfig);
                SetMainField(main, "savedConnectionsToolStripMenuItem", menu);
                main.LoadSavedConnections(env);

                void Save(string key, NamespaceService service)
                {
                    using var dialog = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                    dialog.Show();
                    dialog.SelectProfile(key);
                    FindControl<RadioButton>(dialog, "radio" + service).PerformClick();
                    FindControl<Button>(dialog, "btnSave").PerformClick();
                }
                Save(ConfigurationParameters.MicrosoftServiceBusConnectionString, NamespaceService.NotificationHubs);
                Save(EnvironmentKey, NamespaceService.Relay);
                main.LoadSavedConnections(env);
                Assert.Equal(NamespaceService.NotificationHubs, helper.ServiceBusNamespaces[ConfigurationParameters.MicrosoftServiceBusConnectionString].KnownSelection.Service);
                Assert.Equal(NamespaceService.Relay, helper.ServiceBusNamespaces[EnvironmentKey].KnownSelection.Service);
                Assert.Null(helper.ServiceBusNamespaces[SavedProfileKey].KnownSelection);
                Assert.Equal(env, helper.ServiceBusNamespaces[EnvironmentKey].ConnectionString);
                main.LoadSavedConnections(env.Replace("localhost", "other.invalid"));
                Assert.Null(helper.ServiceBusNamespaces[EnvironmentKey].KnownSelection);

                var xml = XDocument.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                Assert.DoesNotContain("environmentOnly", xml.ToString());
                Assert.Single(xml.Root.Element("serviceBusNamespaces").Elements("add"));
                var settings = xml.Root.Element("appSettings").Elements("add").ToList();
                Assert.Equal(LocalSas, (string)settings.Single(e => (string)e.Attribute("key") == ConfigurationParameters.MicrosoftServiceBusConnectionString).Attribute("value"));
                Assert.All(settings.Where(e => (string)e.Attribute("key") != ConfigurationParameters.MicrosoftServiceBusConnectionString),
                    e => Assert.Contains((string)e.Attribute("value"), new[] { "Relay", "NotificationHubs" }));

                // A user credential takes priority even when it points at the same endpoint as the application credential.
                var userFile = Path.GetTempFileName();
                try
                {
                    new XDocument(new XElement("configuration", new XElement("appSettings", new XElement("add",
                        new XAttribute("key", ConfigurationParameters.MicrosoftServiceBusConnectionString), new XAttribute("value", env))))).Save(userFile);
                    var both = TwoFilesConfiguration.Create(userFile, ConfigFileUse.BothConfig);
                    var overridden = ServiceBusNamespace.GetMessagingNamespaces(both, (_, __) => { })[ConfigurationParameters.MicrosoftServiceBusConnectionString];
                    Assert.Equal(env, overridden.ConnectionString);
                    Assert.Null(overridden.KnownSelection);
                    overridden.SaveExternalSelection(both, new NamespaceSelection(NamespaceService.EventHubs));
                    var userProfiles = ServiceBusNamespace.GetMessagingNamespaces(TwoFilesConfiguration.Create(userFile, ConfigFileUse.BothConfig), (_, __) => { });
                    Assert.Equal(NamespaceService.EventHubs, userProfiles[ConfigurationParameters.MicrosoftServiceBusConnectionString].KnownSelection.Service);
                    main.LoadSavedConnections(env);
                    Assert.Equal(NamespaceService.NotificationHubs, helper.ServiceBusNamespaces[ConfigurationParameters.MicrosoftServiceBusConnectionString].KnownSelection.Service);
                }
                finally { File.Delete(userFile); }
            });
        }

        [Fact]
        public void OriginalConfigurationMethodsRemainPublicWithTheirExactSignatures()
        {
            var s = typeof(string);
            var mode = typeof(ConfigFileUse);
            var log = typeof(WriteToLogDelegate);
            Assert.NotNull(typeof(ConfigurationHelper).GetMethod("AddServiceBusNamespace", new[] { mode, s, s, log }));
            Assert.NotNull(typeof(ConfigurationHelper).GetMethod("UpdateServiceBusNamespace", new[] { mode, s, s, s, log }));
            var config = typeof(TwoFilesConfiguration);
            Assert.NotNull(config.GetMethod("GetHashtableFromSection", new[] { s }));
            Assert.NotNull(config.GetMethod("AddEntryToDictionarySection", new[] { s, s, s }));
            Assert.NotNull(config.GetMethod("UpdateEntryInDictionarySection", new[] { s, s, s, s, log }));
            Assert.NotNull(config.GetMethod("RemoveEntryFromDictionarySection", new[] { s, s, log }));
        }
    }
}
