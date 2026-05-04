#region Copyright

//=======================================================================================
// Microsoft Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
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
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

#endregion

// ReSharper disable once CheckNamespace

namespace ServiceBusExplorer.Forms
{
    public partial class ConnectForm : Form
    {
        #region Private Constants

        //***************************
        // Constants
        //***************************
        private const string SelectServiceBusNamespace = "Select a service bus namespace...";
        private const string EnterConnectionString = "Enter connection string...";
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string UriLabel = "URI or Server FQDN:";
        private const string ConnectionStringLabel = "Connection String:";
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string SubscriptionEntity = "Subscription";
        private const string ConnectionStringTransportType = "transporttype";
        private const string ConnectionStringRuntimePort = "runtimeport";
        private const string ConnectionStringTransportTypeFormat = ";TransportType={0}";
        private const string DefaultNetMessagingRuntimePort = "9354";
        private const string DefaultAmqpRuntimePort = "5671";
        private const string AuthenticationModeLabel = "Authentication:";
        private const string SharedAccessKeyNameLabel = "Shared Access Key Name:";
        private const string SharedAccessKeyLabel = "Shared Access Key:";
        private const string TenantIdLabel = "Tenant ID (optional):";
        private const string SharedAccessSignatureAuthMode = "Shared Access Signature (SAS)";
        private const string AzureActiveDirectoryAuthMode = "Azure Active Directory";
        private const string replacementText = "{replace}";
        private const string SelectedEntitiesTooltip = "Select which entity groups Service Bus Explorer loads for this namespace.";
        private const string AadSelectedEntitiesTooltip =
            "Azure Active Directory connections currently load queues and topics. Subscription nodes remain available under topics.";

        //***************************
        // Tooltips
        //***************************
        private const string ConnectionStringTooltip =
            "Microsoft Azure Service Bus\r\n"
            + "-----------------------------\r\n"
            + "Endpoint=sb://<servicebusnamespace>.servicebus.windows.net/;SharedAccessKeyName=<SAS policy name>;SharedAccessKey=<SAS policy key>\r\n"
            + "\r\n"
            + "Service Bus for Windows Server\r\n"
            + "---------------------------------\r\n"
            + "Endpoint=sb://<machinename>/<servicebusnamespace>;StsEndpoint=https://<machinename>:9355/<servicebusnamespace>;\r\n"
            + "RuntimePort=9354;ManagementPort=9355;WindowsUsername=<username>;WindowsDomain=<domain/machinename>;WindowsPassword=<password>";

        private const string UriTooltip = "Gets or sets the URI of the service bus namespace endpoint.";
        private const string TenantIdTooltip = "Gets or sets the Azure Active Directory tenant ID. Leave blank to use the organizations endpoint (work or school accounts only).";
        private const string AuthenticationModeTooltip = "Select how Service Bus Explorer authenticates to the namespace.";

        //***************************
        // Messages
        //***************************
        private const string ConnectionStringCannotBeNull = "The connection string cannot be null.";
        private const string InvalidEndpointMessage = "The format of the Service Bus endpoint is invalid.";

        #endregion

        #region Private Instance Fields

        private readonly ServiceBusHelper serviceBusHelper;
        private readonly ConfigFileUse configFileUse;
        private bool ignoreSelectedIndexChange;
        private bool ignoreAuthModeChange;

        #endregion

        #region Private Static Fields

        private static int connectionStringIndex = -1;
        private static string connectionString;

        #endregion

        #region Public Constructor

        public ConnectForm(ServiceBusHelper serviceBusHelper, ConfigFileUse configFileUse)
        {
            InitializeComponent();

            this.configFileUse = configFileUse;
            SetConfigFileUseLabelText(lblConfigFileUse);

            this.serviceBusHelper = serviceBusHelper;
            cboServiceBusNamespace.Items.Add(SelectServiceBusNamespace);
            cboServiceBusNamespace.Items.Add(EnterConnectionString);
            if (serviceBusHelper.ServiceBusNamespaces != null)
            {
                // ReSharper disable CoVariantArrayConversion
                cboServiceBusNamespace.Items.AddRange(serviceBusHelper.ServiceBusNamespaces.Keys.OrderBy(s => s).ToArray());
                // ReSharper restore CoVariantArrayConversion
            }

            ConnectivityMode = ServiceBusHelper.ConnectivityMode;
            cboConnectivityMode.DataSource = Enum.GetValues(typeof(ConnectivityMode));
            cboConnectivityMode.SelectedItem = ConnectivityMode;
            UseAmqpWebSockets = ServiceBusHelper.UseAmqpWebSockets;
            useAmqpWebSocketsCheckBox.Checked = UseAmqpWebSockets;

            cboAuthMode.Items.AddRange(new object[] { SharedAccessSignatureAuthMode, AzureActiveDirectoryAuthMode });
            cboAuthMode.SelectedIndex = 0;
            toolTip.SetToolTip(cboAuthMode, AuthenticationModeTooltip);
            txtNamespace.Visible = false;

            cboTransportType.DataSource = Enum.GetValues(typeof(TransportType));
            var settings = new MessagingFactorySettings();
            cboTransportType.SelectedItem = settings.TransportType;

            cboServiceBusNamespace.SelectedIndex = connectionStringIndex > 0 ? connectionStringIndex : 0;

            txtQueueFilterExpression.Text = FilterExpressionHelper.QueueFilterExpression;
            txtTopicFilterExpression.Text = FilterExpressionHelper.TopicFilterExpression;
            txtSubscriptionFilterExpression.Text = FilterExpressionHelper.SubscriptionFilterExpression;
            btnOk.Enabled = cboServiceBusNamespace.SelectedIndex > 1 ||
                            (cboServiceBusNamespace.Text == EnterConnectionString &&
                             !string.IsNullOrWhiteSpace(connectionString));

            foreach (var item in ConfigurationHelper.Entities)
            {
                cboSelectedEntities.Items.Add(item);
            }

            var selectedEntities = MainForm.SingletonMainForm?.SelectedEntities ?? new List<string>();
            foreach (var item in selectedEntities)
            {
                cboSelectedEntities.CheckBoxItems[item].Checked = true;
            }
            toolTip.SetToolTip(cboSelectedEntities, SelectedEntitiesTooltip);

            cboServiceBusNamespace_SelectedIndexChanged(cboServiceBusNamespace, EventArgs.Empty);
            validation_TextChanged(this, EventArgs.Empty);
        }

        void SetConfigFileUseLabelText(Label label)
        {
            var originalConfigFileUseInfo = label.Text;

            string replacement;

            switch (configFileUse)
            {
                case ConfigFileUse.ApplicationConfig:
                    replacement = "The application config file";
                    break;

                case ConfigFileUse.UserConfig:
                    replacement = "The user config file";
                    break;

                case ConfigFileUse.BothConfig:
                    replacement = "The application config file and the user config file";
                    break;

                default:
                    throw new InvalidOperationException($"Unexpected value, {configFileUse} in method " +
                        $"{nameof(SetConfigFileUseLabelText)}.");
            }

            var updatedConfigFileUseInfo = originalConfigFileUseInfo.Replace(replacementText, replacement);
            label.Text = updatedConfigFileUseInfo;
        }

        #endregion

        #region Public Properties

        public string Key { get; private set; }
        public string Uri { get; private set; }
        public string Namespace { get; private set; }
        public string ServicePath { get; set; }
        public string IssuerName { get; private set; }
        public string IssuerSecret { get; private set; }
        public string SharedAccessKeyName { get; private set; }
        public string SharedAccessKey { get; private set; }
        public string ConnectionString { get; private set; }
        public string EntityPath { get; private set; }
        public ConnectivityMode ConnectivityMode { get; private set; }
        public bool UseAmqpWebSockets { get; private set; }
        public TransportType TransportType { get; set; }

        /// <summary>
        /// When non-null, the caller should use this pre-built namespace instance
        /// (e.g. for AAD auth) instead of re-parsing ConnectionString.
        /// </summary>
        public ServiceBusNamespace ServiceBusNamespaceInstance { get; private set; }

        public List<string> SelectedEntities
        {
            get
            {
                if (SelectedAuthMode == ServiceBusAuthMode.AzureActiveDirectory)
                {
                    // Include Event Hub entities — the actual namespace type is detected
                    // at connect time, and MainForm will load only what's available.
                    return new List<string>
                    {
                        Constants.QueueEntities,
                        Constants.TopicEntities,
                        Constants.EventHubEntities
                    };
                }

                return cboSelectedEntities.CheckBoxItems.Where(i => i.Checked).Select(i => i.Text).ToList();
            }
        }

        #endregion

        #region Event Handlers

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            BuildCurrentConnectionString();
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                MainForm.StaticWriteToLog(ConnectionStringCannotBeNull);
                return;
            }
            DialogResult = DialogResult.OK;
            ConnectivityMode = (ConnectivityMode)cboConnectivityMode.SelectedItem;
            UseAmqpWebSockets = useAmqpWebSocketsCheckBox.Checked;
            FilterExpressionHelper.QueueFilterExpression = txtQueueFilterExpression.Text;
            FilterExpressionHelper.TopicFilterExpression = txtTopicFilterExpression.Text;
            FilterExpressionHelper.SubscriptionFilterExpression = txtSubscriptionFilterExpression.Text;
            connectionStringIndex = cboServiceBusNamespace.SelectedIndex;
            if (cboServiceBusNamespace.Text == EnterConnectionString)
            {
                connectionString = ConnectionString;
            }
        }

        private ServiceBusAuthMode SelectedAuthMode =>
            cboAuthMode.SelectedIndex == 1
                ? ServiceBusAuthMode.AzureActiveDirectory
                : ServiceBusAuthMode.Sas;

        private void SetSelectedAuthMode(ServiceBusAuthMode authMode)
        {
            ignoreAuthModeChange = true;
            cboAuthMode.SelectedIndex = authMode == ServiceBusAuthMode.AzureActiveDirectory ? 1 : 0;
            ignoreAuthModeChange = false;
        }

        private void GetSelectionState(out ServiceBusNamespaceType connectionStringType,
            out bool containsStsEndpoint,
            out ServiceBusNamespace selectedNamespace)
        {
            selectedNamespace = cboServiceBusNamespace.SelectedIndex > 1 &&
                                serviceBusHelper.ServiceBusNamespaces.ContainsKey(cboServiceBusNamespace.Text)
                ? serviceBusHelper.ServiceBusNamespaces[cboServiceBusNamespace.Text]
                : null;

            connectionStringType = selectedNamespace?.ConnectionStringType ?? ServiceBusNamespaceType.Custom;
            Key = selectedNamespace != null ? cboServiceBusNamespace.Text : null;
            containsStsEndpoint = !string.IsNullOrWhiteSpace(selectedNamespace?.StsEndpoint);
        }

        private bool UsesRawConnectionStringEditor(ServiceBusNamespaceType connectionStringType, bool containsStsEndpoint)
        {
            return connectionStringType == ServiceBusNamespaceType.OnPremises ||
                   containsStsEndpoint ||
                   (cboServiceBusNamespace.Text == EnterConnectionString &&
                    SelectedAuthMode == ServiceBusAuthMode.Sas);
        }

        private void UpdateConnectionSettingsUi(ServiceBusNamespaceType connectionStringType, bool containsStsEndpoint)
        {
            var usesRawConnectionStringEditor = UsesRawConnectionStringEditor(connectionStringType, containsStsEndpoint);
            var isAad = !usesRawConnectionStringEditor && SelectedAuthMode == ServiceBusAuthMode.AzureActiveDirectory;
            UpdateSelectedEntitiesUi(isAad);

            lblNamespace.Text = AuthenticationModeLabel;
            txtNamespace.Visible = false;
            cboAuthMode.Visible = true;
            cboAuthMode.Enabled = connectionStringType != ServiceBusNamespaceType.OnPremises && !containsStsEndpoint;

            lblUri.Text = usesRawConnectionStringEditor ? ConnectionStringLabel : UriLabel;
            txtUri.Multiline = usesRawConnectionStringEditor;
            txtUri.Size = usesRawConnectionStringEditor ? new Size(336, 44) : new Size(336, 20);
            toolTip.SetToolTip(txtUri, usesRawConnectionStringEditor ? ConnectionStringTooltip : UriTooltip);

            lblEntityPath.Visible = !usesRawConnectionStringEditor;
            txtEntityPath.Visible = !usesRawConnectionStringEditor;

            lblIssuerName.Visible = !usesRawConnectionStringEditor;
            txtIssuerName.Visible = !usesRawConnectionStringEditor;
            txtIssuerName.Enabled = !usesRawConnectionStringEditor;

            lblIssuerSecret.Visible = !usesRawConnectionStringEditor && !isAad;
            txtIssuerSecret.Visible = !usesRawConnectionStringEditor && !isAad;
            txtIssuerSecret.Enabled = !usesRawConnectionStringEditor && !isAad;

            if (usesRawConnectionStringEditor)
            {
                lblIssuerName.Text = SharedAccessKeyNameLabel;
                lblIssuerSecret.Text = SharedAccessKeyLabel;
                toolTip.SetToolTip(txtIssuerName, "Gets or sets the shared secret issuer name.");
                return;
            }

            if (isAad)
            {
                lblIssuerName.Text = TenantIdLabel;
                toolTip.SetToolTip(txtIssuerName, TenantIdTooltip);
                txtIssuerSecret.Text = string.Empty;
            }
            else
            {
                lblIssuerName.Text = SharedAccessKeyNameLabel;
                lblIssuerSecret.Text = SharedAccessKeyLabel;
                toolTip.SetToolTip(txtIssuerName, "Gets or sets the shared secret issuer name.");
            }
        }

        private void UpdateSelectedEntitiesUi(bool isAad)
        {
            toolTip.SetToolTip(cboSelectedEntities, isAad ? AadSelectedEntitiesTooltip : SelectedEntitiesTooltip);
            cboSelectedEntities.Enabled = !isAad;
            cboSelectedEntities._CheckBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();

            if (cboSelectedEntities.Items.Count > 0)
            {
                cboSelectedEntities.Items[0] = isAad
                    ? $"{Constants.QueueEntities}, {Constants.TopicEntities} ({Constants.SubscriptionEntities}), {Constants.EventHubEntities}"
                    : cboSelectedEntities.GetCSVText(true);
            }
        }

        private void ClearConnectionFields()
        {
            txtUri.Text = string.Empty;
            txtNamespace.Text = string.Empty;
            txtEntityPath.Text = string.Empty;
            txtIssuerName.Text = string.Empty;
            txtIssuerSecret.Text = string.Empty;
        }

        private static Dictionary<string, string> ParseConnectionStringParts(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new Dictionary<string, string>();
            }

            var trimmedValue = value.Trim();
            if (trimmedValue.IndexOf("=", StringComparison.Ordinal) < 0)
            {
                return new Dictionary<string, string>();
            }

            return trimmedValue
                .Split(';')
                .Where(p => p.Contains('='))
                .ToDictionary(s => s.Substring(0, s.IndexOf('=')).ToLowerInvariant(),
                    s => s.Substring(s.IndexOf('=') + 1),
                    StringComparer.OrdinalIgnoreCase);
        }

        private static string ExtractEndpoint(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            var trimmedValue = value.Trim();
            if (trimmedValue.IndexOf("Endpoint=", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return trimmedValue;
            }

            var parameters = ParseConnectionStringParts(trimmedValue);
            return parameters.TryGetValue("endpoint", out var endpoint)
                ? endpoint
                : trimmedValue;
        }

        private static string ExtractEntityPath(string value)
        {
            var parameters = ParseConnectionStringParts(value);
            return parameters.TryGetValue("entitypath", out var entityPath)
                ? entityPath
                : null;
        }

        private ServiceBusNamespace BuildAadNamespaceFromFields(string key)
        {
            var transportType = cboTransportType.SelectedItem is TransportType selectedTransportType
                ? selectedTransportType
                : new MessagingFactorySettings().TransportType;
            var aadConnectionString = ServiceBusNamespace.BuildAadConnectionString(
                ExtractEndpoint(txtUri.Text),
                string.IsNullOrWhiteSpace(txtIssuerName.Text) ? null : txtIssuerName.Text.Trim(),
                transportType,
                string.IsNullOrWhiteSpace(txtEntityPath.Text) ? null : txtEntityPath.Text.Trim());

            return ServiceBusNamespace.GetServiceBusNamespace(key, aadConnectionString, (message, asynchronous) => { });
        }

        private void PopulateManualConnectionFields(string savedConnectionString)
        {
            ClearConnectionFields();

            if (string.IsNullOrWhiteSpace(savedConnectionString))
            {
                SetSelectedAuthMode(ServiceBusAuthMode.Sas);
                GetSelectionState(out var emptyConnectionStringType, out var emptyContainsStsEndpoint, out _);
                UpdateConnectionSettingsUi(emptyConnectionStringType, emptyContainsStsEndpoint);
                return;
            }

            var savedNamespace = ServiceBusNamespace.GetServiceBusNamespace("Manual", savedConnectionString,
                (message, asynchronous) => { });

            if (savedNamespace?.IsAzureActiveDirectory == true)
            {
                SetSelectedAuthMode(ServiceBusAuthMode.AzureActiveDirectory);
                GetSelectionState(out var aadConnectionStringType, out var aadContainsStsEndpoint, out _);
                UpdateConnectionSettingsUi(aadConnectionStringType, aadContainsStsEndpoint);
                txtUri.Text = savedNamespace.Uri;
                txtIssuerName.Text = savedNamespace.TenantId;
                txtEntityPath.Text = savedNamespace.EntityPath;
                cboTransportType.SelectedItem = savedNamespace.TransportType;
                return;
            }

            SetSelectedAuthMode(ServiceBusAuthMode.Sas);
            GetSelectionState(out var rawConnectionStringType, out var rawContainsStsEndpoint, out _);
            UpdateConnectionSettingsUi(rawConnectionStringType, rawContainsStsEndpoint);
            txtUri.Text = savedConnectionString;
        }

        private void BuildCurrentConnectionString()
        {
            ServiceBusNamespaceInstance = null;
            GetSelectionState(out var connectionStringType, out var containsStsEndpoint, out _);

            txtUri.Text = txtUri.Text.Trim();

            if (!UsesRawConnectionStringEditor(connectionStringType, containsStsEndpoint) &&
                SelectedAuthMode == ServiceBusAuthMode.AzureActiveDirectory)
            {
                var aadNamespace = BuildAadNamespaceFromFields(Key ?? "Manual");
                TransportType = cboTransportType.SelectedItem is TransportType selectedTransportType
                    ? selectedTransportType
                    : new MessagingFactorySettings().TransportType;

                ConnectionString = ServiceBusNamespace.BuildAadConnectionString(
                    aadNamespace?.Uri ?? ExtractEndpoint(txtUri.Text),
                    string.IsNullOrWhiteSpace(txtIssuerName.Text) ? null : txtIssuerName.Text.Trim(),
                    TransportType,
                    string.IsNullOrWhiteSpace(txtEntityPath.Text) ? null : txtEntityPath.Text.Trim());

                ServiceBusNamespaceInstance = aadNamespace;
                Uri = aadNamespace?.Uri;
                Namespace = aadNamespace?.Namespace;
                EntityPath = aadNamespace?.EntityPath;
                TransportType = aadNamespace?.TransportType ?? TransportType;
                SharedAccessKeyName = null;
                SharedAccessKey = null;
                return;
            }

            if (UsesRawConnectionStringEditor(connectionStringType, containsStsEndpoint))
            {
                ConnectionString = txtUri.Text;
                Uri = null;
                Namespace = null;
                EntityPath = null;
                SharedAccessKeyName = null;
                SharedAccessKey = null;
                return;
            }

            Uri = ExtractEndpoint(txtUri.Text);
            TransportType = cboTransportType.SelectedItem is TransportType transportType
                ? transportType
                : new MessagingFactorySettings().TransportType;
            EntityPath = txtEntityPath.Text.Trim();
            SharedAccessKeyName = txtIssuerName.Text;
            SharedAccessKey = txtIssuerSecret.Text;

            if (string.IsNullOrEmpty(EntityPath))
            {
                ConnectionString = string.Format(ServiceBusNamespace.SasConnectionStringFormat,
                    Uri,
                    SharedAccessKeyName,
                    SharedAccessKey,
                    TransportType);
            }
            else
            {
                ConnectionString = string.Format(ServiceBusNamespace.SasConnectionStringEntityPathFormat,
                    Uri,
                    SharedAccessKeyName,
                    SharedAccessKey,
                    TransportType,
                    EntityPath);
            }

            var parsedNamespace = ServiceBusNamespace.GetServiceBusNamespace(Key ?? "Manual", ConnectionString,
                (message, asynchronous) => { });
            Namespace = parsedNamespace?.Namespace;
        }

        private void validation_TextChanged(object sender, EventArgs e)
        {
            GetSelectionState(out var connectionStringType, out var containsStsEndpoint, out _);

            if (cboServiceBusNamespace.SelectedIndex == 0)
            {
                btnOk.Enabled = false;
                return;
            }

            if (!UsesRawConnectionStringEditor(connectionStringType, containsStsEndpoint) &&
                SelectedAuthMode == ServiceBusAuthMode.AzureActiveDirectory)
            {
                btnOk.Enabled = !string.IsNullOrWhiteSpace(txtUri.Text) &&
                                BuildAadNamespaceFromFields(Key ?? "Manual") != null;
                return;
            }

            if (UsesRawConnectionStringEditor(connectionStringType, containsStsEndpoint))
            {
                btnOk.Enabled = !string.IsNullOrWhiteSpace(txtUri.Text);
                if (string.IsNullOrWhiteSpace(txtUri.Text))
                {
                    return;
                }

                var cn = txtUri.Text;
                if (cn[cn.Length - 1] == ';')
                {
                    cn = cn.Substring(0, cn.Length - 1);
                }
                var parameters =
                    cn.Split(';').Where(p => Enumerable.Contains(p, '='))
                        .ToDictionary(s => s.Substring(0, s.IndexOf('=')).ToLower(),
                            s => s.Substring(s.IndexOf('=') + 1));
                if (!parameters.ContainsKey(ConnectionStringTransportType))
                {
                    cboTransportType.SelectedItem = TransportType.NetMessaging;
                    return;
                }
                if (Enum.TryParse<TransportType>(parameters[ConnectionStringTransportType], true, out var transportType))
                {
                    cboTransportType.SelectedItem = transportType;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtUri.Text))
                {
                    try
                    {
                        BuildCurrentConnectionString();
                        var ns = ServiceBusNamespace.GetServiceBusNamespace(Key, ConnectionString, (message, async) => { });
                        txtNamespace.Text = ns.Namespace;
                    }
                    catch
                    {
                        // ignore
                    }
                }

                btnOk.Enabled = !string.IsNullOrWhiteSpace(txtUri.Text) &&
                                !string.IsNullOrWhiteSpace(txtIssuerName.Text) &&
                                !string.IsNullOrWhiteSpace(txtIssuerSecret.Text);
            }
        }

        private void cboServiceBusNamespace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreSelectedIndexChange)
            {
                return;
            }

            GetSelectionState(out var connectionStringType, out var containsStsEndpoint, out var selectedNamespace);

            btnRename.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = cboServiceBusNamespace.Text == EnterConnectionString;

            if (cboServiceBusNamespace.SelectedIndex == 0)
            {
                SetSelectedAuthMode(ServiceBusAuthMode.Sas);
                UpdateConnectionSettingsUi(connectionStringType, containsStsEndpoint);
                ClearConnectionFields();
                btnOk.Enabled = false;
                return;
            }

            if (cboServiceBusNamespace.Text == EnterConnectionString)
            {
                PopulateManualConnectionFields(connectionString);
                validation_TextChanged(sender, e);
                return;
            }

            if (selectedNamespace == null)
            {
                SetSelectedAuthMode(ServiceBusAuthMode.Sas);
                UpdateConnectionSettingsUi(connectionStringType, containsStsEndpoint);
                ClearConnectionFields();
                btnOk.Enabled = false;
                return;
            }

            btnSave.Visible = selectedNamespace.UserCreated;
            btnRename.Visible = selectedNamespace.UserCreated;
            btnDelete.Visible = selectedNamespace.UserCreated;

            SetSelectedAuthMode(selectedNamespace.IsAzureActiveDirectory
                ? ServiceBusAuthMode.AzureActiveDirectory
                : ServiceBusAuthMode.Sas);
            UpdateConnectionSettingsUi(connectionStringType, containsStsEndpoint);
            ClearConnectionFields();

            if (UsesRawConnectionStringEditor(connectionStringType, containsStsEndpoint))
            {
                txtUri.Text = selectedNamespace.ConnectionString;
            }
            else
            {
                txtUri.Text = selectedNamespace.Uri;
                txtEntityPath.Text = selectedNamespace.EntityPath;

                if (selectedNamespace.IsAzureActiveDirectory)
                {
                    txtIssuerName.Text = selectedNamespace.TenantId;
                }
                else
                {
                    txtIssuerName.Text = selectedNamespace.SharedAccessKeyName;
                    txtIssuerSecret.Text = selectedNamespace.SharedAccessKey;
                }
            }

            cboTransportType.SelectedItem = selectedNamespace.TransportType;
            validation_TextChanged(sender, e);
        }

        private void cboAuthMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreAuthModeChange)
            {
                return;
            }

            var switchingToAad = SelectedAuthMode == ServiceBusAuthMode.AzureActiveDirectory;
            var currentUri = txtUri.Text?.Trim() ?? string.Empty;

            if (switchingToAad)
            {
                // Switching to AAD: extract endpoint and entity path from any raw connection string
                var endpoint = ExtractEndpoint(currentUri);
                var entityPath = ExtractEntityPath(currentUri);

                txtIssuerName.Text = string.Empty;
                txtUri.Text = endpoint ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(entityPath) && string.IsNullOrWhiteSpace(txtEntityPath.Text))
                {
                    txtEntityPath.Text = entityPath;
                }
            }
            else
            {
                // Switching to SAS: clear fields since user needs a full connection string
                txtIssuerName.Text = string.Empty;
                txtUri.Text = string.Empty;
            }

            GetSelectionState(out var connectionStringType, out var containsStsEndpoint, out _);
            UpdateConnectionSettingsUi(connectionStringType, containsStsEndpoint);
            validation_TextChanged(sender, e);
        }

        private void cboTransportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSelectionState(out var connectionStringType, out var containsStsEndpoint, out _);

            if (UsesRawConnectionStringEditor(connectionStringType, containsStsEndpoint))
            {
                btnOk.Enabled = !string.IsNullOrWhiteSpace(txtUri.Text);
                if (string.IsNullOrEmpty(txtUri.Text))
                {
                    return;
                }
                var cn = txtUri.Text;
                if (cn[cn.Length - 1] == ';')
                {
                    cn = cn.Substring(0, cn.Length - 1);
                }
                var parameters =
                    cn.Split(';').Where(p => p.Contains('='))
                        .ToDictionary(s => s.Substring(0, s.IndexOf('=')).ToLower(),
                            s => s.Substring(s.IndexOf('=') + 1));
                var value = parameters.ContainsKey(ConnectionStringTransportType)
                    ? cn.Replace(parameters[ConnectionStringTransportType], cboTransportType.SelectedItem.ToString())
                    : cn + string.Format(ConnectionStringTransportTypeFormat, cboTransportType.SelectedItem);
                if (parameters.ContainsKey(ConnectionStringRuntimePort))
                {
                    if (!(cboTransportType.SelectedItem is TransportType))
                    {
                        return;
                    }
                    var transportType = (TransportType)cboTransportType.SelectedItem;
                    value = value.Replace(parameters[ConnectionStringRuntimePort],
                        transportType == TransportType.Amqp
                            ? DefaultAmqpRuntimePort
                            : DefaultNetMessagingRuntimePort);
                }
                txtUri.Text = value;
            }
            else
            {
                validation_TextChanged(sender, e);
            }
        }

        private void ConnectForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == '\r';
            if (e.Handled && cboServiceBusNamespace.SelectedIndex > 0)
            {
                btnOk_Click(sender, null);
            }
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

        private void clearButton_MouseLeave(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                control.ForeColor = Color.FromArgb(92, 125, 150);
            }
        }

        private void btnOpenQueueFilterForm_Click(object sender, EventArgs e)
        {
            using (var form = new FilterForm(QueueEntity, txtQueueFilterExpression.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtQueueFilterExpression.Text = form.FilterExpression;
                }
                txtQueueFilterExpression.Focus();
            }
        }

        private void btnOpenTopicFilterForm_Click(object sender, EventArgs e)
        {
            using (var form = new FilterForm(TopicEntity, txtTopicFilterExpression.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtTopicFilterExpression.Text = form.FilterExpression;
                }
                txtTopicFilterExpression.Focus();
            }
        }

        private void btnOpenSubscriptionFilterForm_Click(object sender, EventArgs e)
        {
            using (var form = new FilterForm(SubscriptionEntity, txtSubscriptionFilterExpression.Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtSubscriptionFilterExpression.Text = form.FilterExpression;
                }
                txtSubscriptionFilterExpression.Focus();
            }
        }

        private void btnClearQueueFilterExpression_Click(object sender, EventArgs e)
        {
            txtQueueFilterExpression.Text = null;
        }

        private void btnClearTopicFilterExpression_Click(object sender, EventArgs e)
        {
            txtTopicFilterExpression.Text = null;
        }

        private void btnClearSubscriptionFilterExpression_Click(object sender, EventArgs e)
        {
            txtSubscriptionFilterExpression.Text = null;
        }

        private void grouperServiceBusNamespaces_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                cboServiceBusNamespace.Location.X - 1,
                cboServiceBusNamespace.Location.Y - 1,
                cboServiceBusNamespace.Size.Width + 1,
                cboServiceBusNamespace.Size.Height + 1);
        }

        private void grouperFilters_CustomPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(SystemColors.ActiveBorder, 1),
                cboSelectedEntities.Location.X - 1,
                cboSelectedEntities.Location.Y - 1,
                cboSelectedEntities.Size.Width + 1,
                cboSelectedEntities.Size.Height + 1);
        }

        private void grouperServiceBusNamespaceSettings_CustomPaint(PaintEventArgs e)
        {
            var pen = new Pen(SystemColors.ActiveBorder, 1);
            e.Graphics.DrawRectangle(pen,
                cboAuthMode.Location.X - 1,
                cboAuthMode.Location.Y - 1,
                cboAuthMode.Size.Width + 1,
                cboAuthMode.Size.Height + 1);
            e.Graphics.DrawRectangle(pen,
                cboConnectivityMode.Location.X - 1,
                cboConnectivityMode.Location.Y - 1,
                cboConnectivityMode.Size.Width + 1,
                cboConnectivityMode.Size.Height + 1);
            e.Graphics.DrawRectangle(pen,
                cboTransportType.Location.X - 1,
                cboTransportType.Location.Y - 1,
                cboTransportType.Size.Width + 1,
                cboTransportType.Size.Height + 1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var key = cboServiceBusNamespace.Text;
                var isNewServiceBusNamespace = (key == EnterConnectionString);

                BuildCurrentConnectionString();

                if (string.IsNullOrWhiteSpace(ConnectionString))
                {
                    MainForm.StaticWriteToLog(ConnectionStringCannotBeNull);
                    return;
                }

                // For AAD entries, skip ServiceBusConnectionStringBuilder validation
                // since the AAD metadata string format is not a SAS connection string
                string host = null;
                if (SelectedAuthMode == ServiceBusAuthMode.AzureActiveDirectory &&
                    ServiceBusNamespaceInstance == null)
                {
                    MainForm.StaticWriteToLog(InvalidEndpointMessage);
                    return;
                }

                if (ServiceBusNamespaceInstance != null && ServiceBusNamespaceInstance.IsAzureActiveDirectory)
                {
                    host = ServiceBusNamespaceInstance.FullyQualifiedNamespace;
                }
                else
                {
                    ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder;
                    try
                    {
                        serviceBusConnectionStringBuilder = new ServiceBusConnectionStringBuilder(ConnectionString);
                    }
                    catch (Exception)
                    {
                        MainForm.StaticWriteToLog("The format of the connection string is invalid.");
                        return;
                    }

                    if (serviceBusConnectionStringBuilder.Endpoints == null ||
                        serviceBusConnectionStringBuilder.Endpoints.Count == 0)
                    {
                        MainForm.StaticWriteToLog("The connection string does not contain any endpoint.");
                        return;
                    }

                    host = serviceBusConnectionStringBuilder.Endpoints.ToArray()[0].Host;
                }

                var index = host.IndexOf(".", StringComparison.Ordinal);

                if (isNewServiceBusNamespace)
                {
                    key = index > 0 ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(host.Substring(0, index)) : "MyNamespace";

                    using (var parameterForm = new ParameterForm("Enter the key for the Service Bus namespace",
                        new List<string> { "Key" },
                        new List<string> { key },
                        new List<bool> { false }))
                    {
                        if (parameterForm.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        key = parameterForm.ParameterValues[0];
                    }
                }

                if (string.IsNullOrWhiteSpace(key))
                {
                    MainForm.StaticWriteToLog("The key of the Service Bus namespace cannot be null.");
                    return;
                }

                var value = ConnectionString;

                try
                {
                    if (isNewServiceBusNamespace)
                    {
                        ConfigurationHelper.AddServiceBusNamespace(configFileUse, key, value, MainForm.StaticWriteToLog);
                    }
                    else
                    {
                        ConfigurationHelper.UpdateServiceBusNamespace(configFileUse, key, key, value, MainForm.StaticWriteToLog);
                    }
                }
                catch (ArgumentNullException ex)
                {
                    MainForm.StaticWriteToLog(ex.Message);
                }

                serviceBusHelper.ServiceBusNamespaces[key] = ServiceBusNamespace.GetServiceBusNamespace(key, value, MainForm.StaticWriteToLog);

                cboServiceBusNamespace.Items.Clear();
                cboServiceBusNamespace.Items.Add(SelectServiceBusNamespace);
                cboServiceBusNamespace.Items.Add(EnterConnectionString);

                // ReSharper disable once CoVariantArrayConversion
                cboServiceBusNamespace.Items.AddRange(serviceBusHelper.ServiceBusNamespaces.Keys.OrderBy(s => s).ToArray());
                cboServiceBusNamespace.Text = key;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        void HandleException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }
            MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message))
            {
                MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var ns = serviceBusHelper.ServiceBusNamespaces[cboServiceBusNamespace.Text];
            var key = cboServiceBusNamespace.Text;

            using (var parameterForm = new ParameterForm("Enter the new key for the Service Bus namespace",
                   new List<string> { "Key" },
                   new List<string> { key },
                   new List<bool> { false }))
            {
                if (parameterForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var newKey = parameterForm.ParameterValues[0];
                if (newKey == key)
                {
                    MainForm.StaticWriteToLog("The new key of the Service Bus namespace was the same as before.");
                    return;
                }

                var existingKeys = serviceBusHelper.ServiceBusNamespaces.Keys;
                if (existingKeys.Contains(newKey))
                {
                    MainForm.StaticWriteToLog("A Service Bus namespace key must be unique");
                    return;
                }

                if (string.IsNullOrWhiteSpace(newKey))
                {
                    MainForm.StaticWriteToLog("The key of the Service Bus namespace cannot be null.");
                    return;
                }

                var itemIndex = cboServiceBusNamespace.SelectedIndex;
                ConfigurationHelper.UpdateServiceBusNamespace(configFileUse, key, newKey, newValue: null, MainForm.StaticWriteToLog);

                ignoreSelectedIndexChange = true;
                serviceBusHelper.ServiceBusNamespaces.Remove(key);
                serviceBusHelper.ServiceBusNamespaces[newKey] = ns;
                cboServiceBusNamespace.Items[itemIndex] = newKey.GetHashCode();
                cboServiceBusNamespace.Items[itemIndex] = newKey;
                cboServiceBusNamespace.Text = newKey;
                ignoreSelectedIndexChange = false;

                MainForm.StaticWriteToLog($"Renamed '{key}' to '{newKey}'");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var key = cboServiceBusNamespace.Text;
            using (var deleteForm = new DeleteForm($"Really delete Service Bus Namespace '{cboServiceBusNamespace.Text}'?"))
            {
                if (deleteForm.ShowDialog() == DialogResult.OK)
                {
                    ConfigurationHelper.RemoveServiceBusNamespace(configFileUse, key, MainForm.StaticWriteToLog);
                    cboServiceBusNamespace.Items.RemoveAt(cboServiceBusNamespace.SelectedIndex);
                    cboServiceBusNamespace.SelectedIndex = 0;

                    serviceBusHelper.ServiceBusNamespaces.Remove(key);
                }
            }
        }

        #endregion
    }
}
