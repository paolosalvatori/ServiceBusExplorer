using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using FluentAssertions;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    public class ConnectFormAadTests
    {
        [Fact]
        public void BuildCurrentConnectionString_ManualAadMode_BuildsStructuredAadEntry()
        {
            string connectionString = null;
            string endpoint = null;
            string tenantId = null;
            string entityPath = null;
            bool isAad = false;
            bool issuerSecretVisible = true;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig))
                {
                    GetComboBox(form, "cboServiceBusNamespace").SelectedIndex = 1;
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 1;
                    GetTextBox(form, "txtUri").Text = "myns.servicebus.windows.net";
                    GetTextBox(form, "txtIssuerName").Text = "tenant-id";
                    GetTextBox(form, "txtEntityPath").Text = "queue-a";
                    GetComboBox(form, "cboTransportType").SelectedItem = TransportType.Amqp;

                    InvokePrivateMethod(form, "BuildCurrentConnectionString");

                    connectionString = form.ConnectionString;
                    endpoint = form.ServiceBusNamespaceInstance?.Uri;
                    tenantId = form.ServiceBusNamespaceInstance?.TenantId;
                    entityPath = form.ServiceBusNamespaceInstance?.EntityPath;
                    isAad = form.ServiceBusNamespaceInstance?.IsAzureActiveDirectory == true;
                    issuerSecretVisible = GetTextBox(form, "txtIssuerSecret").Visible;
                }
            });

            isAad.Should().BeTrue();
            endpoint.Should().Be("sb://myns.servicebus.windows.net");
            tenantId.Should().Be("tenant-id");
            entityPath.Should().Be("queue-a");
            connectionString.Should().Contain("Endpoint=sb://myns.servicebus.windows.net");
            connectionString.Should().Contain("AuthMode=AAD");
            connectionString.Should().Contain("TenantId=tenant-id");
            connectionString.Should().Contain("EntityPath=queue-a");
            issuerSecretVisible.Should().BeFalse();
        }

        [Fact]
        public void BuildCurrentConnectionString_SavedAadEntry_UsesEditedValues()
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
                helper.ServiceBusNamespaces["Saved AAD"] = new ServiceBusNamespace(
                    "sb://oldns.servicebus.windows.net/",
                    "oldns",
                    "tenant-old",
                    TransportType.NetMessaging,
                    "old-entity",
                    true);

                using (var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig))
                {
                    GetComboBox(form, "cboServiceBusNamespace").SelectedItem = "Saved AAD";
                    GetTextBox(form, "txtUri").Text = "newns.servicebus.windows.net";
                    GetTextBox(form, "txtIssuerName").Text = "tenant-new";
                    GetTextBox(form, "txtEntityPath").Text = "queue-new";
                    GetComboBox(form, "cboTransportType").SelectedItem = TransportType.Amqp;

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
            connectionString.Should().Contain("Endpoint=sb://newns.servicebus.windows.net");
            connectionString.Should().Contain("TenantId=tenant-new");
            connectionString.Should().Contain("EntityPath=queue-new");
        }

        [Fact]
        public void AuthModeSwitch_SasToAad_ForcesSelectedEntitiesToQueuesAndTopics()
        {
            string[] selectedEntities = null;
            bool selectedEntitiesEnabled = true;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig))
                {
                    GetComboBox(form, "cboServiceBusNamespace").SelectedIndex = 1;
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 1;
                    selectedEntities = form.SelectedEntities.ToArray();
                    selectedEntitiesEnabled = GetCheckBoxComboBox(form, "cboSelectedEntities").Enabled;
                }
            });

            selectedEntities.Should().Equal(Constants.QueueEntities, Constants.TopicEntities);
            selectedEntitiesEnabled.Should().BeFalse();
        }

        [Fact]
        public void AuthModeSwitch_AadToSas_ReenablesSelectedEntitiesPicker()
        {
            bool selectedEntitiesEnabled = false;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig))
                {
                    GetComboBox(form, "cboServiceBusNamespace").SelectedIndex = 1;
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 1;
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 0;
                    selectedEntitiesEnabled = GetCheckBoxComboBox(form, "cboSelectedEntities").Enabled;
                }
            });

            selectedEntitiesEnabled.Should().BeTrue();
        }

        [Fact]
        public void SavedAadEntry_Load_ForcesSelectedEntitiesToQueuesAndTopics()
        {
            string[] selectedEntities = null;
            bool selectedEntitiesEnabled = true;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                var helper = new ServiceBusHelper((message, asynchronous) => { });
                helper.ServiceBusNamespaces["Saved AAD"] = new ServiceBusNamespace(
                    "sb://oldns.servicebus.windows.net/",
                    "oldns",
                    "tenant-old",
                    TransportType.NetMessaging,
                    "old-entity",
                    true);

                using (var form = new ConnectForm(helper, ConfigFileUse.ApplicationConfig))
                {
                    GetComboBox(form, "cboServiceBusNamespace").SelectedItem = "Saved AAD";
                    selectedEntities = form.SelectedEntities.ToArray();
                    selectedEntitiesEnabled = GetCheckBoxComboBox(form, "cboSelectedEntities").Enabled;
                }
            });

            selectedEntities.Should().Equal(Constants.QueueEntities, Constants.TopicEntities);
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
        public void AuthModeSwitch_SasToAad_ExtractsEndpointAndEntityPath()
        {
            string uriText = null;
            string entityPathText = null;
            string issuerNameText = null;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig))
                {
                    // Start in manual SAS mode
                    GetComboBox(form, "cboServiceBusNamespace").SelectedIndex = 1;
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 0; // SAS

                    // Enter a full connection string with EntityPath
                    GetTextBox(form, "txtUri").Text =
                        "Endpoint=sb://myns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=abc123;EntityPath=myqueue";
                    GetTextBox(form, "txtIssuerName").Text = "RootManageSharedAccessKey";

                    // Switch to AAD
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 1;

                    uriText = GetTextBox(form, "txtUri").Text;
                    entityPathText = GetTextBox(form, "txtEntityPath").Text;
                    issuerNameText = GetTextBox(form, "txtIssuerName").Text;
                }
            });

            uriText.Should().Be("sb://myns.servicebus.windows.net/");
            entityPathText.Should().Be("myqueue");
            issuerNameText.Should().BeEmpty("SAS key name should not persist as tenant ID");
        }

        [Fact]
        public void AuthModeSwitch_AadToSas_ClearsFields()
        {
            string uriText = null;
            string issuerNameText = null;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig))
                {
                    // Start in manual AAD mode
                    GetComboBox(form, "cboServiceBusNamespace").SelectedIndex = 1;
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 1; // AAD

                    // Enter AAD fields
                    GetTextBox(form, "txtUri").Text = "myns.servicebus.windows.net";
                    GetTextBox(form, "txtIssuerName").Text = "my-tenant-id";

                    // Switch to SAS
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 0;

                    uriText = GetTextBox(form, "txtUri").Text;
                    issuerNameText = GetTextBox(form, "txtIssuerName").Text;
                }
            });

            uriText.Should().BeEmpty("endpoint should not persist as connection string");
            issuerNameText.Should().BeEmpty("tenant ID should not persist as SAS key name");
        }

        [Fact]
        public void AuthModeSwitch_SasToAad_PreservesExistingEntityPath()
        {
            string entityPathText = null;

            RunOnStaThread(() =>
            {
                ResetManualConnectionState();

                using (var form = new ConnectForm(new ServiceBusHelper((message, asynchronous) => { }),
                           ConfigFileUse.ApplicationConfig))
                {
                    GetComboBox(form, "cboServiceBusNamespace").SelectedIndex = 1;
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 0; // SAS

                    // EntityPath already set by user (entity path is only visible in structured mode)
                    // Switch to AAD first to get structured mode, set entity path, switch to SAS, then back to AAD
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 1; // AAD
                    GetTextBox(form, "txtEntityPath").Text = "user-set-queue";

                    // Enter a connection string with a different entity path
                    GetComboBox(form, "cboAuthMode").SelectedIndex = 0; // SAS
                    GetTextBox(form, "txtUri").Text =
                        "Endpoint=sb://myns.servicebus.windows.net/;SharedAccessKeyName=key;SharedAccessKey=abc;EntityPath=other-queue";

                    // Switch to AAD: should NOT overwrite user-set entity path
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
