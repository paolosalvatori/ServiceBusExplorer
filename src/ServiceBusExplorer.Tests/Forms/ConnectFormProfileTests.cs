using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public partial class ConnectFormEntraTests
    {
        private const string SavedProfileKey = "Synthetic profile";
        private const string LocalSas = "Endpoint=sb://localhost/;SharedAccessKeyName=name;SharedAccessKey=c3ludGhldGlj";
        private const string SavedEmulator = LocalSas + ";UseDevelopmentEmulator=true;";

        [Fact]
        public void StartupBuildsSavedMenuWhenEnvironmentProfileIsInvalid()
        {
            WithProfileConfiguration(CheckStartupMenu);
        }

        private static void CheckStartupMenu()
        {
            RunOnStaThread(() =>
            {
                var helper = new ServiceBusHelper((_, __) => { });
                using var log = new BlockingCollection<string>();
                using var menu = new ToolStripMenuItem();
                var main = CreateProfileTestMainForm(log);
                SetMainField(main, "configFileUse", ConfigFileUse.ApplicationConfig);
                SetMainField(main, "serviceBusHelper", helper);
                SetMainField(main, "savedConnectionsToolStripMenuItem", menu);

                main.LoadSavedConnections(SavedEmulator.Replace("localhost/", "localhost:65536/"));
                Assert.Equal(SavedProfileKey, Assert.Single(menu.DropDownItems.Cast<ToolStripMenuItem>()).Tag);
                Assert.Equal(SavedEmulator, Assert.Single(helper.ServiceBusNamespaces).Value.ConnectionString);
                Assert.Contains(log, message => message.Contains("endpoint or port is invalid"));

                main.LoadSavedConnections(SavedEmulator);
                const string environmentKey = @"HKEY_CURRENT_USER\Environment connection string";
                Assert.Equal(SavedEmulator, helper.ServiceBusNamespaces[environmentKey].ConnectionString);
                Assert.Contains(menu.DropDownItems.Cast<ToolStripMenuItem>(), item => Equals(item.Tag, environmentKey));
            });
        }

        [Theory]
        [InlineData("Endpoint=sb://localhost:0/;SharedAccessKeyName=name;SharedAccessKey=test;UseDevelopmentEmulator=true;")]
        [InlineData("Endpoint=sb://localhost/;SharedAccessKeyName= ;SharedAccessKey=test;UseDevelopmentEmulator=true;")]
        [InlineData("Endpoint=sb://localhost/;SharedAccessKeyName=name;SharedAccessKey= ;UseDevelopmentEmulator=true;")]
        [InlineData("Endpoint=sb://localhost/;SharedAccessKeyName=name;UseDevelopmentEmulator=true;")]
        [InlineData("Endpoint=sb://localhost/;SharedAccessKey=test;UseDevelopmentEmulator=true;")]
        public void SaveRejectsInvalidProfileWithoutReplacingFileOrCachedEntry(string invalid)
        {
            WithProfileConfiguration(CheckSaveButton, invalid);
        }

        private static void CheckSaveButton()
        {
            RunOnStaThread(() =>
            {
                var helper = new ServiceBusHelper((_, __) => { });
                using var log = new BlockingCollection<string>();
                CreateProfileTestMainForm(log);
                var previous = ServiceBusNamespace.GetServiceBusNamespace(SavedProfileKey, SavedEmulator, (_, __) => { });
                helper.ServiceBusNamespaces[SavedProfileKey] = previous;
                var configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                var originalFile = File.ReadAllBytes(configPath);
                using var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                form.Show();
                GetComboBox(form, ControlNameServiceBusNamespace).SelectedItem = SavedProfileKey;
                var editor = GetTextBox(form, ControlNameUri);
                var save = FindControl<Button>(form, "btnSave");
                editor.Text = (string)AppDomain.CurrentDomain.GetData("invalidProfile");
                save.PerformClick();
                Assert.Contains(log, message => message.Contains("invalid"));
                Assert.Same(previous, helper.ServiceBusNamespaces[SavedProfileKey]);
                Assert.Equal(originalFile, File.ReadAllBytes(configPath));

                // A subsequent valid save must keep the typed representation, including its final delimiter.
                const string corrected = LocalSas + "; TransportType = amqp ; ";
                editor.Text = corrected;
                save.PerformClick();
                var stored = XDocument.Load(configPath).Root.Element("serviceBusNamespaces").Elements("add")
                    .Single(entry => (string)entry.Attribute("key") == SavedProfileKey);
                Assert.Equal(corrected, (string)stored.Attribute("value"));
                Assert.Equal(corrected, helper.ServiceBusNamespaces[SavedProfileKey].ConnectionString);
                Assert.Equal(TransportType.Amqp, helper.ServiceBusNamespaces[SavedProfileKey].TransportType);
            });
        }

        [Theory]
        [InlineData("amqp", false)]
        [InlineData("NetMessaging;", true)]
        public void ConnectUsesSelectedLegacyTransportAndPreservesOriginalText(string originalTransport, bool selectAmqp)
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                var helper = new ServiceBusHelper((_, __) => { });
                using var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                form.Show();
                GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                var editor = GetTextBox(form, ControlNameUri);
                editor.Text = LocalSas + ";TransportType=" + originalTransport;
                if (selectAmqp)
                {
                    GetComboBox(form, ControlNameTransportType).SelectedItem = TransportType.NetMessaging;
                    GetComboBox(form, ControlNameTransportType).SelectedItem = TransportType.Amqp;
                }
                FindControl<RadioButton>(form, "radioServiceBus").Checked = true;
                string submitted = editor.Text;
                FindControl<Button>(form, "btnOk").PerformClick();
                Assert.True(form.TryConnectToNamespace(out var connected));
                Assert.Equal(TransportType.Amqp, helper.MessagingFactory.GetSettings().TransportType);
                Assert.True(helper.NamespaceManager.Address.IsLoopback);
                Assert.Equal(submitted, connected.ConnectionString);
                Assert.Equal(submitted, helper.ConnectionString);

                helper.MessagingFactory.Abort();
                ResetManualConnectionState();
            });
        }

        [Fact]
        public void RejectedSavedConnectionPreservesActiveClientsAndCredentials()
        {
            var helper = new ServiceBusHelper((_, __) => { });
            const string submitted = LocalSas + ";TransportType=amqp;";
            var active = ServiceBusNamespace.GetServiceBusNamespace("Active", submitted, (_, __) => { });
            Assert.True(helper.Connect(active));
            var manager = helper.NamespaceManager;
            var factory = helper.MessagingFactory;
            try
            {
                var invalid = ServiceBusNamespace.GetServiceBusNamespace("Rejected",
                    "Endpoint=sb://127.0.0.1/;SharedAccessKeyName=other;SharedAccessKey=other;TransportType=invalid", (_, __) => { });
                Assert.Throws<ArgumentException>(() => helper.Connect(invalid));
                Assert.Same(manager, helper.NamespaceManager);
                Assert.Same(factory, helper.MessagingFactory);
                Assert.Equal(TransportType.Amqp, factory.GetSettings().TransportType);
                Assert.Equal(submitted, helper.ConnectionString);
                Assert.Equal(active.SharedAccessKeyName, helper.SharedAccessKeyName);
                Assert.Equal(active.SharedAccessKey, helper.SharedAccessKey);
            }
            finally
            {
                factory.Abort();
            }
        }

        private static MainForm CreateProfileTestMainForm(BlockingCollection<string> log)
        {
            // Exercise the actual loading/menu and form logging paths without starting the version checker.
            var main = (MainForm)FormatterServices.GetUninitializedObject(typeof(MainForm));
            SetMainField(main, "logCollection", log);
            SetPrivateStaticField(typeof(MainForm), "mainSingletonMainForm", main);
            return main;
        }

        private static void SetMainField(MainForm main, string name, object value)
        {
            typeof(MainForm).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(main, value);
        }

        private static void WithProfileConfiguration(CrossAppDomainDelegate check, string invalid = null, string storedProfile = SavedEmulator, string legacySetting = null)
        {
            var setup = AppDomain.CurrentDomain.SetupInformation;
            var config = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".config");
            AppDomain domain = null;
            try
            {
                var root = new XElement("configuration",
                    new XElement("configSections", new XElement("section", new XAttribute("name", "serviceBusNamespaces"),
                        new XAttribute("type", typeof(System.Configuration.DictionarySectionHandler).AssemblyQualifiedName))),
                    new XElement("serviceBusNamespaces", new XElement("add", new XAttribute("key", SavedProfileKey),
                        new XAttribute("value", storedProfile))));
                if (legacySetting != null)
                    root.Add(new XElement("appSettings", new XElement("add",
                        new XAttribute("key", ConfigurationParameters.MicrosoftServiceBusConnectionString),
                        new XAttribute("value", legacySetting))));
                if (File.Exists(setup.ConfigurationFile))
                    root.Add(XDocument.Load(setup.ConfigurationFile).Root.Element("runtime"));
                new XDocument(root).Save(config);
                setup.ApplicationBase = Path.GetDirectoryName(new Uri(typeof(ConnectFormEntraTests).Assembly.CodeBase).LocalPath);
                setup.ConfigurationFile = config;
                domain = AppDomain.CreateDomain("Synthetic connection profiles", null, setup);
                domain.SetData("invalidProfile", invalid);
                domain.DoCallBack(check);
            }
            finally
            {
                if (domain != null) AppDomain.Unload(domain);
                File.Delete(config);
            }
        }
    }
}
