using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Azure.Identity;
using Microsoft.Identity.Client;
using ServiceBusExplorer.Tests.Helpers;
using FluentAssertions;
using ServiceBusExplorer.Common.Helpers;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [Collection("Entra credential cache")]
    public class ConnectFormEntraTests
    {
        private const string ControlNameServiceBusNamespace = "cboServiceBusNamespace";
        private const string ControlNameAuthMode = "cboAuthMode";
        private const string ControlNameUri = "txtUri";
        private const string ControlNameTenantIds = "cboTenantIds";
        private const string ControlNameEntityPath = "txtEntityPath";
        private const string ControlNameTransportType = "cboTransportType";
        private const string ControlNameIssuerSecret = "txtIssuerSecret";
        private const string ControlNameSelectedEntities = "cboSelectedEntities";
        private const string ControlNameIssuerName = "txtIssuerName";

        private const string SavedEntraEntryName = "Saved Entra";

        private const string ConnectionStringPartEndpoint = "Endpoint=";
        private const string ConnectionStringPartAuthModeEntra = "AuthMode=AAD";
        private const string ConnectionStringPartTenantId = "TenantId=";
        private const string ConnectionStringPartEntityPath = "EntityPath=";

        [Fact]
        public void AuthenticationOptions_SaveAndReloadBrowserChoice()
        {
            // The real OK button saves application settings. Give this dialog its own config file.
            var setup = AppDomain.CurrentDomain.SetupInformation;
            var config = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".config");
            AppDomain domain = null;
            try
            {
                if (File.Exists(setup.ConfigurationFile)) File.Copy(setup.ConfigurationFile, config);
                setup.ApplicationBase = Path.GetDirectoryName(new Uri(typeof(ConnectFormEntraTests).Assembly.CodeBase).LocalPath);
                setup.ConfigurationFile = config;
                domain = AppDomain.CreateDomain("Entra settings", null, setup);
                domain.DoCallBack(CheckBrowserChoiceInDialog);
            }
            finally
            {
                if (domain != null) AppDomain.Unload(domain);
                File.Delete(config);
            }
        }

        static void CheckBrowserChoiceInDialog()
        {
            RunOnStaThread(() =>
            {
                var defaults = new MainSettings().GetDefault();
                foreach (var browserOnly in new[] { true, false })
                {
                    var settings = ConfigurationHelper.GetMainProperties(ConfigFileUse.ApplicationConfig, defaults, null);
                    using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                    {
                        form.Show();
                        FindControl<TabControl>(form, "tabOptionsControl").SelectedTab =
                            FindControl<TabPage>(form, "tabPageAuthentication");
                        var choice = FindControl<CheckBox>(form, "systemBrowserOnlyCheckBox");
                        choice.Visible.Should().BeTrue();
                        choice.Checked.Should().Be(!browserOnly);
                        choice.Checked = browserOnly;
                        FindControl<Button>(form, "btnOk").PerformClick();
                        form.DialogResult.Should().Be(DialogResult.OK);
                    }
                    ConfigurationHelper.GetMainProperties(ConfigFileUse.ApplicationConfig, defaults, null)
                        .EntraSystemBrowserOnly.Should().Be(browserOnly);
                }
            });
        }

        [Fact]
        public void SavedEmulatorRoundTripPreservesFlagEndpointAndCredentials()
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                const string value = "Endpoint=sb://127.0.0.1:5672;SharedAccessKeyName=name;SharedAccessKey=synthetic;UseDevelopmentEmulator=true;";
                var helper = new ServiceBusHelper((_, __) => { });
                helper.ServiceBusNamespaces["Saved emulator"] = ServiceBusNamespace.GetServiceBusNamespace("Saved emulator", value, (_, __) => { });
                using (var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null))
                {
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedItem = "Saved emulator";
                    GetTextBox(form, ControlNameUri).Text.Should().Be(value);
                    InvokePrivateMethod(form, "BuildCurrentConnectionString");
                    form.ConnectionString.Should().Be(value);
                    var restored = ServiceBusNamespace.GetServiceBusNamespace("Saved emulator", form.ConnectionString, (_, __) => { });
                    restored.UseDevelopmentEmulator.Should().BeTrue();
                    restored.Namespace.Should().Be("127.0.0.1");
                }
            });
        }

        [Fact]
        public void LoadingPaddedTransportDoesNotRewriteSavedProfile()
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                const string value = "Endpoint=sb://localhost:5672;SharedAccessKeyName=name;SharedAccessKey=synthetic==;UseDevelopmentEmulator=true; TransportType = Amqp ; RuntimePort =5671;";
                var helper = new ServiceBusHelper((_, __) => { });
                var profile = ServiceBusNamespace.GetServiceBusNamespace("Padded", value, (_, __) => { });
                helper.ServiceBusNamespaces["Padded"] = profile;
                using var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                GetComboBox(form, ControlNameServiceBusNamespace).SelectedItem = "Padded";
                GetComboBox(form, ControlNameTransportType).SelectedItem.Should().Be(TransportType.Amqp);
                GetTextBox(form, ControlNameUri).Text.Should().Be(value);
                profile.ConnectionString.Should().Be(value);
                GetComboBox(form, ControlNameTransportType).SelectedItem = TransportType.NetMessaging;
                GetTextBox(form, ControlNameUri).Text.Should().Contain(" TransportType =NetMessaging;");
                GetTextBox(form, ControlNameUri).Text.Should().Contain("SharedAccessKey=synthetic==;");
                GetTextBox(form, ControlNameUri).Text.Should().Contain(" RuntimePort =9354;");
                profile.ConnectionString.Should().Be(value);
            });
        }

        [Theory]
        [InlineData(TransportType.Amqp, TransportType.NetMessaging)]
        [InlineData(TransportType.NetMessaging, TransportType.Amqp)]
        public void DuplicatePaddedSettingsUseLastValueWithoutMutatingSavedText(TransportType first, TransportType last)
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                var value = "Endpoint=sb://localhost:5672;SharedAccessKeyName=name;SharedAccessKey=synthetic==;UseDevelopmentEmulator=true; TransportType =" + first +
                    "; TRANSPORTTYPE = " + last + " ;RuntimePort=1111; RuntimePort =5671;";
                var helper = new ServiceBusHelper((_, __) => { });
                var profile = ServiceBusNamespace.GetServiceBusNamespace("Duplicates", value, (_, __) => { });
                profile.TransportType.Should().Be(last);
                helper.ServiceBusNamespaces["Duplicates"] = profile;
                using var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                GetComboBox(form, ControlNameServiceBusNamespace).SelectedItem = "Duplicates";
                GetComboBox(form, ControlNameTransportType).SelectedItem.Should().Be(last);
                GetTextBox(form, ControlNameUri).Text.Should().Be(value);
                profile.ConnectionString.Should().Be(value);
                var parsed = InvokeStaticPrivateMethod<System.Collections.Generic.Dictionary<string, string>>(typeof(ConnectForm), "ParseConnectionStringParts", value);
                parsed["transporttype"].Should().Be(last.ToString());
                parsed["runtimeport"].Should().Be("5671");
                GetComboBox(form, ControlNameTransportType).SelectedItem = first;
                var edited = GetTextBox(form, ControlNameUri).Text;
                edited.Should().Contain(" TransportType =" + first + ";");
                edited.Should().Contain(" TRANSPORTTYPE =" + first + ";");
                var effective = ServiceBusNamespace.GetServiceBusNamespace("Edited", edited, (_, __) => { });
                effective.TransportType.Should().Be(first);
                var changed = InvokeStaticPrivateMethod<System.Collections.Generic.Dictionary<string, string>>(typeof(ConnectForm), "ParseConnectionStringParts", edited);
                changed["runtimeport"].Should().Be(first == TransportType.Amqp ? "5671" : "9354");
                edited.Should().Contain("SharedAccessKey=synthetic==;");
                profile.ConnectionString.Should().Be(value);
            });
        }

        [Fact]
        public void AcceptingDialogStagesFiltersWithoutChangingActiveFilters()
        {
            RunOnStaThread(() =>
            {
                var before = new[] { FilterExpressionHelper.QueueFilterExpression, FilterExpressionHelper.TopicFilterExpression, FilterExpressionHelper.SubscriptionFilterExpression };
                try
                {
                    FilterExpressionHelper.QueueFilterExpression = "old queue";
                    FilterExpressionHelper.TopicFilterExpression = "old topic";
                    FilterExpressionHelper.SubscriptionFilterExpression = "old subscription";
                    ResetManualConnectionState();
                    var helper = new ServiceBusHelper((_, __) => { });
                    helper.ServiceBusNamespaces["Synthetic"] = ServiceBusNamespace.GetServiceBusNamespace("Synthetic", "Endpoint=sb://localhost:5672;SharedAccessKeyName=name;SharedAccessKey=synthetic;UseDevelopmentEmulator=true", (_, __) => { });
                    using var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null);
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedItem = "Synthetic";
                    GetTextBox(form, "txtQueueFilterExpression").Text = "new queue";
                    GetTextBox(form, "txtTopicFilterExpression").Text = "new topic";
                    GetTextBox(form, "txtSubscriptionFilterExpression").Text = "new subscription";
                    typeof(ConnectForm).GetMethod("btnOk_Click", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(form, new object[] { null, EventArgs.Empty });
                    form.DialogResult.Should().Be(System.Windows.Forms.DialogResult.OK);
                    form.QueueFilterExpression.Should().Be("new queue");
                    form.TopicFilterExpression.Should().Be("new topic");
                    form.SubscriptionFilterExpression.Should().Be("new subscription");
                    FilterExpressionHelper.QueueFilterExpression.Should().Be("old queue");
                    FilterExpressionHelper.TopicFilterExpression.Should().Be("old topic");
                    FilterExpressionHelper.SubscriptionFilterExpression.Should().Be("old subscription");
                }
                finally
                {
                    FilterExpressionHelper.QueueFilterExpression = before[0];
                    FilterExpressionHelper.TopicFilterExpression = before[1];
                    FilterExpressionHelper.SubscriptionFilterExpression = before[2];
                }
            });
        }

        [Fact]
        public void TransportDropdownDoesNotRewriteOtherConnectionValues()
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                const string value = "Endpoint=sb://NetMessaging9354:5672;SharedAccessKeyName=name;SharedAccessKey=NetMessaging9354==;UseDevelopmentEmulator=true;TransportType=NetMessaging;RuntimePort=9354;";
                var helper = new ServiceBusHelper((_, __) => { });
                helper.ServiceBusNamespaces["Saved emulator"] = ServiceBusNamespace.GetServiceBusNamespace("Saved emulator", value, (_, __) => { });
                using (var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null))
                {
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedItem = "Saved emulator";
                    GetComboBox(form, ControlNameTransportType).SelectedItem = TransportType.Amqp;
                    var updated = GetTextBox(form, ControlNameUri).Text;
                    updated.Should().Contain("Endpoint=sb://NetMessaging9354:5672");
                    updated.Should().Contain("SharedAccessKey=NetMessaging9354==");
                    updated.Should().Contain("TransportType=Amqp");
                    updated.Should().Contain("RuntimePort=5671");
                }
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void AuthModeSwitch_PlaceholderToEntra_SelectsManualConnection(bool enterEndpointBeforeSwitch)
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig, null))
                {
                    var namespacePicker = GetComboBox(form, ControlNameServiceBusNamespace);
                    var authModePicker = GetComboBox(form, ControlNameAuthMode);
                    var endpoint = GetTextBox(form, ControlNameUri);
                    var okButton = FindControl<System.Windows.Forms.Button>(form, "btnOk");

                    namespacePicker.SelectedIndex.Should().Be(0);
                    if (enterEndpointBeforeSwitch)
                    {
                        endpoint.Text = "myns.servicebus.windows.net";
                    }

                    authModePicker.SelectedIndex = 1;

                    namespacePicker.SelectedIndex.Should().Be(1);
                    okButton.Enabled.Should().Be(enterEndpointBeforeSwitch);

                    if (!enterEndpointBeforeSwitch)
                    {
                        endpoint.Text = "myns.servicebus.windows.net";
                        okButton.Enabled.Should().BeTrue();
                    }
                }
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConnectButton_DismissedSignInPreservesActiveSettings(bool browserOnly)
        {
            RunOnStaThread(() =>
            {
                ResetManualConnectionState();
                const string tenant = "connect-dialog-test";
                var previousChoice = EntraCredentialFactory.UseSystemBrowserOnly;
                var previousMode = ServiceBusHelper.ConnectivityMode;
                var previousWebSockets = ServiceBusHelper.UseAmqpWebSockets;
                var previousFilter = FilterExpressionHelper.QueueFilterExpression;
                var cause = new MsalClientException(MsalError.AuthenticationCanceledError, "Sign-in closed");
                var credential = new ServiceBusHelperEntraTests.StubInteractiveCredential((context, token) =>
                    throw new AuthenticationFailedException("Synthetic cancellation", cause));
                var unexpected = new ServiceBusHelperEntraTests.StubInteractiveCredential((context, token) =>
                    throw new InvalidOperationException("Unexpected sign-in"));
                var helper = new ServiceBusHelper((message, asynchronous) => { });
                try
                {
                    EntraCredentialFactory.UseSystemBrowserOnly = browserOnly;
                    ServiceBusHelperEntraTests.CacheCredential("interactiveBrowserCredentials", tenant, browserOnly ? unexpected : credential);
                    ServiceBusHelperEntraTests.CacheCredential("systemBrowserCredentials", tenant, browserOnly ? credential : unexpected);
                    using (var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null))
                    {
                        form.Show();
                        GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                        GetComboBox(form, ControlNameAuthMode).SelectedIndex = 1;
                        GetTextBox(form, ControlNameUri).Text = "localhost";
                        GetComboBox(form, ControlNameTenantIds).Text = tenant;
                        GetTextBox(form, "txtQueueFilterExpression").Text = "submitted filter";
                        GetComboBox(form, "cboConnectivityMode").SelectedItem = previousMode == Microsoft.ServiceBus.ConnectivityMode.Http
                            ? Microsoft.ServiceBus.ConnectivityMode.Tcp : Microsoft.ServiceBus.ConnectivityMode.Http;
                        FindControl<CheckBox>(form, "useAmqpWebSocketsCheckBox").Checked = !previousWebSockets;

                        FindControl<Button>(form, "btnOk").PerformClick();
                        form.DialogResult.Should().Be(DialogResult.OK);
                        form.ServiceBusNamespaceInstance.TenantId.Should().Be(tenant);
                        new Uri(form.ServiceBusNamespaceInstance.Uri).IsLoopback.Should().BeTrue();
                        var failure = Record.Exception(() => form.TryConnectToNamespace(out _));
                        failure.Should().NotBeNull();
                        failure.GetBaseException().Should().BeSameAs(cause);
                        credential.Calls.Should().Be(1);
                        unexpected.Calls.Should().Be(0);
                        helper.NamespaceManager.Should().BeNull();
                        ServiceBusHelper.ConnectivityMode.Should().Be(previousMode);
                        ServiceBusHelper.UseAmqpWebSockets.Should().Be(previousWebSockets);
                        FilterExpressionHelper.QueueFilterExpression.Should().Be(previousFilter);
                    }
                }
                finally
                {
                    ServiceBusHelper.ConnectivityMode = previousMode;
                    ServiceBusHelper.UseAmqpWebSockets = previousWebSockets;
                    FilterExpressionHelper.QueueFilterExpression = previousFilter;
                    EntraCredentialFactory.UseSystemBrowserOnly = previousChoice;
                    EntraCredentialFactory.ClearCache();
                    ResetManualConnectionState();
                }
            });
        }

        [Fact]
        public void ConnectButton_ManualEntraMode_BuildsStructuredEntraEntry()
        {
            string connectionString = null;
            string endpoint = null;
            string tenantId = null;
            string entityPath = null;
            bool isEntra = false;
            bool issuerSecretVisible = true;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig, null))
                {
                    form.Show();
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                    GetComboBox(form, ControlNameAuthMode).SelectedIndex = 1;
                    GetTextBox(form, ControlNameUri).Text = "myns.servicebus.windows.net";
                    GetComboBox(form, ControlNameTenantIds).Text = "tenant-id";
                    GetTextBox(form, ControlNameEntityPath).Text = "queue-a";
                    GetComboBox(form, ControlNameTransportType).SelectedItem = TransportType.Amqp;

                    FindControl<Button>(form, "btnOk").PerformClick();
                    form.DialogResult.Should().Be(DialogResult.OK);

                    connectionString = form.ConnectionString;
                    endpoint = form.ServiceBusNamespaceInstance?.Uri;
                    tenantId = form.ServiceBusNamespaceInstance?.TenantId;
                    entityPath = form.ServiceBusNamespaceInstance?.EntityPath;
                    isEntra = form.ServiceBusNamespaceInstance?.IsEntra == true;
                    issuerSecretVisible = GetTextBox(form, ControlNameIssuerSecret).Visible;
                }
            });

            isEntra.Should().BeTrue();
            endpoint.Should().Be("sb://myns.servicebus.windows.net");
            tenantId.Should().Be("tenant-id");
            entityPath.Should().Be("queue-a");
            connectionString.Should().Contain(ConnectionStringPartEndpoint + "sb://myns.servicebus.windows.net");
            connectionString.Should().Contain(ConnectionStringPartAuthModeEntra);
            connectionString.Should().Contain(ConnectionStringPartTenantId + "tenant-id");
            connectionString.Should().Contain(ConnectionStringPartEntityPath + "queue-a");
            issuerSecretVisible.Should().BeFalse();
        }

        [Fact]
        public void BuildCurrentConnectionString_SavedEntraEntry_UsesEditedValues()
        {
            string connectionString = null;
            string endpoint = null;
            string tenantId = null;
            string entityPath = null;
            TransportType transportType = TransportType.NetMessaging;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                var helper = new ServiceBusHelper((message, asynchronous) => { });
                helper.ServiceBusNamespaces[SavedEntraEntryName] = new ServiceBusNamespace(
                    "sb://oldns.servicebus.windows.net/",
                    "oldns",
                    "tenant-old",
                    TransportType.NetMessaging,
                    "old-entity",
                    true);

                using (var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null))
                {
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedItem = SavedEntraEntryName;
                    GetTextBox(form, ControlNameUri).Text = "newns.servicebus.windows.net";
                    GetComboBox(form, ControlNameTenantIds).Text = "tenant-new";
                    GetTextBox(form, ControlNameEntityPath).Text = "queue-new";
                    GetComboBox(form, ControlNameTransportType).SelectedItem = TransportType.Amqp;

                    InvokePrivateMethod(form, "BuildCurrentConnectionString");

                    connectionString = form.ConnectionString;
                    endpoint = form.ServiceBusNamespaceInstance?.Uri;
                    tenantId = form.ServiceBusNamespaceInstance?.TenantId;
                    entityPath = form.ServiceBusNamespaceInstance?.EntityPath;
                    transportType = form.ServiceBusNamespaceInstance?.TransportType ?? TransportType.NetMessaging;
                }
            });

            endpoint.Should().Be("sb://newns.servicebus.windows.net");
            tenantId.Should().Be("tenant-new");
            entityPath.Should().Be("queue-new");
            transportType.Should().Be(TransportType.Amqp);
            connectionString.Should().Contain(ConnectionStringPartEndpoint + "sb://newns.servicebus.windows.net");
            connectionString.Should().Contain(ConnectionStringPartTenantId + "tenant-new");
            connectionString.Should().Contain(ConnectionStringPartEntityPath + "queue-new");
        }

        [Fact]
        public void AuthModeSwitch_SasToEntra_ForcesSelectedEntitiesToQueuesAndTopics()
        {
            string[] selectedEntities = null;
            bool selectedEntitiesEnabled = true;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig, null))
                {
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                    GetComboBox(form, ControlNameAuthMode).SelectedIndex = 1;
                    selectedEntities = form.SelectedEntities.ToArray();
                    selectedEntitiesEnabled = GetCheckBoxComboBox(form, ControlNameSelectedEntities).Enabled;
                }
            });

            selectedEntities.Should().Equal(Constants.QueueEntities, Constants.TopicEntities, Constants.EventHubEntities);
            selectedEntitiesEnabled.Should().BeFalse();
        }

        [Fact]
        public void AuthModeSwitch_EntraToSas_ReenablesSelectedEntitiesPicker()
        {
            bool selectedEntitiesEnabled = false;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig, null))
                {
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                    GetComboBox(form, ControlNameAuthMode).SelectedIndex = 1;
                    GetComboBox(form, ControlNameAuthMode).SelectedIndex = 0;
                    selectedEntitiesEnabled = GetCheckBoxComboBox(form, ControlNameSelectedEntities).Enabled;
                }
            });

            selectedEntitiesEnabled.Should().BeTrue();
        }

        [Fact]
        public void SavedEntraEntry_Load_ForcesSelectedEntitiesToQueuesAndTopics()
        {
            string[] selectedEntities = null;
            bool selectedEntitiesEnabled = true;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                var helper = new ServiceBusHelper((message, asynchronous) => { });
                helper.ServiceBusNamespaces[SavedEntraEntryName] = new ServiceBusNamespace(
                    "sb://oldns.servicebus.windows.net/",
                    "oldns",
                    "tenant-old",
                    TransportType.NetMessaging,
                    "old-entity",
                    true);

                using (var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, null))
                {
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedItem = SavedEntraEntryName;
                    selectedEntities = form.SelectedEntities.ToArray();
                    selectedEntitiesEnabled = GetCheckBoxComboBox(form, ControlNameSelectedEntities).Enabled;
                }
            });

            selectedEntities.Should().Equal(Constants.QueueEntities, Constants.TopicEntities, Constants.EventHubEntities);
            selectedEntitiesEnabled.Should().BeFalse();
        }

        static void RunOnStaThread(Action action)
        {
            Exception exception = null;
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (exception != null)
            {
                throw exception;
            }
        }

        static void ResetManualConnectionState()
        {
            SetPrivateStaticField(typeof(ConnectForm), "connectionString", null);
            SetPrivateStaticField(typeof(ConnectForm), "connectionStringIndex", -1);
        }

        static void InvokePrivateMethod(object instance, string methodName)
        {
            var method = instance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            method.Should().NotBeNull();
            method.Invoke(instance, null);
        }

        static T FindControl<T>(System.Windows.Forms.Control root, string name)
            where T : System.Windows.Forms.Control
        {
            return root.Controls.Find(name, true).Single().Should().BeOfType<T>().Which;
        }

        static System.Windows.Forms.ComboBox GetComboBox(ConnectForm form, string name)
        {
            return FindControl<System.Windows.Forms.ComboBox>(form, name);
        }

        static CheckBoxComboBox GetCheckBoxComboBox(ConnectForm form, string name)
        {
            var comboBox = FindControl<CheckBoxComboBox>(form, name);
            comboBox._CheckBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
            return comboBox;
        }

        static System.Windows.Forms.TextBox GetTextBox(ConnectForm form, string name)
        {
            return FindControl<System.Windows.Forms.TextBox>(form, name);
        }

        [Fact]
        public void AuthModeSwitch_SasToEntra_ExtractsEndpointAndEntityPath()
        {
            string uriText = null;
            string entityPathText = null;
            string issuerNameText = null;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig, null))
                {
                    // Start in manual SAS mode
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                    GetComboBox(form, ControlNameAuthMode).SelectedIndex = 0; // SAS

                    // Enter a full connection string with EntityPath
                    GetTextBox(form, ControlNameUri).Text =
                        "Endpoint=sb://myns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc123;EntityPath=myqueue";
                    GetTextBox(form, ControlNameIssuerName).Text = "RootManageSharedAccessKey";

                    // Switch to Entra
                    GetComboBox(form, ControlNameAuthMode).SelectedIndex = 1;

                    uriText = GetTextBox(form, ControlNameUri).Text;
                    entityPathText = GetTextBox(form, ControlNameEntityPath).Text;
                    issuerNameText = GetTextBox(form, ControlNameIssuerName).Text;
                }
            });

            uriText.Should().Be("sb://myns.servicebus.windows.net/");
            entityPathText.Should().Be("myqueue");
            issuerNameText.Should().BeEmpty("SAS key name should not persist as tenant ID");
        }

        [Fact]
        public void AuthModeSwitch_EntraToSas_HidesFields()
        {
            bool issuerNameVisible = true;
            bool issuerSecretVisible = true;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig, null))
                {
                    // Start in manual Entra mode
                    GetComboBox(form, ControlNameServiceBusNamespace).SelectedIndex = 1;
                    GetComboBox(form, ControlNameAuthMode).SelectedIndex = 1; // Entra

                    // Enter Entra fields
                    GetTextBox(form, ControlNameUri).Text = "myns.servicebus.windows.net";
                    GetComboBox(form, ControlNameTenantIds).Text = "my-tenant-id";

                    // Switch to SAS
                    GetComboBox(form, ControlNameAuthMode).SelectedIndex = 0;

                    issuerNameVisible = GetTextBox(form, ControlNameIssuerName).Visible;
                    issuerSecretVisible = GetTextBox(form, ControlNameIssuerSecret).Visible;
                }
            });

            issuerNameVisible.Should().BeFalse($"{ControlNameIssuerName} should not be visible");
            issuerSecretVisible.Should().BeFalse($"{ControlNameIssuerSecret} should not be visible");
        }

        [Fact]
        public void AuthModeSwitch_SasToEntra_PreservesExistingEntityPath()
        {
            string entityPathText = null;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig, null))
                {
                    GetComboBox(form, "cboServiceBusNamespace").SelectedIndex = 1;
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 0; // SAS

                    // EntityPath already set by user (entity path is only visible in structured mode)
                    // Switch to Entra first to get structured mode, set entity path, switch to SAS, then back to Entra
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 1; // Entra
                    GetTextBox(form, "txtEntityPath").Text = "user-set-queue";

                    // Enter a connection string with a different entity path
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 0; // SAS
                    GetTextBox(form, "txtUri").Text =
                        "Endpoint=sb://myns.servicebus.windows.net/;SharedAccessKeyName=key;SharedAccessKey=abc;EntityPath=other-queue";

                    // Switch to Entra: should NOT overwrite user-set entity path
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 1;

                    entityPathText = GetTextBox(form, "txtEntityPath").Text;
                }
            });

            // Entity path should keep what user set, not overwrite with connection string's value
            entityPathText.Should().Be("user-set-queue");
        }

        [Fact]
        public void ExtractEndpoint_ReturnsEndpointFromConnectionString()
        {
            var result = InvokeStaticPrivateMethod<string>(typeof(ConnectForm), "ExtractEndpoint",
                "Endpoint=sb://test.servicebus.windows.net/;SharedAccessKeyName=key;SharedAccessKey=abc");

            result.Should().Be("sb://test.servicebus.windows.net/");
        }

        [Fact]
        public void ExtractEntityPath_ReturnsEntityPath()
        {
            var result = InvokeStaticPrivateMethod<string>(typeof(ConnectForm), "ExtractEntityPath",
                "Endpoint=sb://test.servicebus.windows.net/;EntityPath=myqueue");

            result.Should().Be("myqueue");
        }

        [Fact]
        public void ExtractEntityPath_ReturnsNullWhenMissing()
        {
            var result = InvokeStaticPrivateMethod<string>(typeof(ConnectForm), "ExtractEntityPath",
                "Endpoint=sb://test.servicebus.windows.net/;SharedAccessKeyName=key");

            result.Should().BeNull();
        }

        static T InvokeStaticPrivateMethod<T>(Type type, string methodName, params object[] args)
        {
            var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            method.Should().NotBeNull();
            return (T)method.Invoke(null, args);
        }

        static void SetPrivateStaticField(Type type, string fieldName, object value)
        {
            var field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic);
            field.Should().NotBeNull();
            field.SetValue(null, value);
        }
    }
}
