#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class ConnectForm : Form
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string SelectServiceBusNamespace = "Select a service bus namespace...";
        private const string EnterConnectionString = "Enter connection string...";
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

        //***************************
        // Tooltips
        //***************************
        private const string ConnectionStringTooltip = "Windows Azure Service Bus\r\n-----------------------------\r\nEndpoint=sb://<servicebusnamespace>.servicebus.windows.net/;SharedSecretIssuer=<issuer>;SharedSecretValue=<secret>\r\n\r\nService Bus for Windows Server\r\n---------------------------------\r\nEndpoint=sb://<machinename>/<servicebusnamespace>;StsEndpoint=https://<machinename>:9355/<servicebusnamespace>;\r\nRuntimePort=9354;ManagementPort=9355;WindowsUsername=<username>;WindowsDomain=<domain/machinename>;WindowsPassword=<password>";
        private const string UriTooltip = "Gets or sets the Uri of the service bus namespace endpoint.";

        //***************************
        // Messages
        //***************************
        private const string ConnectionStringCannotBeNull = "The connection string cannot be null.";
        #endregion

        #region Private Instance Fields
        private readonly ServiceBusHelper serviceBusHelper;
        #endregion

        #region Private Static Fields
        private static int connectionStringIndex = -1;
        private static string connectionString;
        #endregion
        
        #region Public Constructor
        public ConnectForm(ServiceBusHelper serviceBusHelper)
        {
            InitializeComponent();
            this.serviceBusHelper = serviceBusHelper;
            cboServiceBusNamespace.Items.Add(SelectServiceBusNamespace);
            cboServiceBusNamespace.Items.Add(EnterConnectionString);
            if (serviceBusHelper.ServiceBusNamespaces != null)
            {
                // ReSharper disable CoVariantArrayConversion
                cboServiceBusNamespace.Items.AddRange(serviceBusHelper.ServiceBusNamespaces.Keys.OrderBy(s=>s).ToArray());
                // ReSharper restore CoVariantArrayConversion
            }

            ConnectivityMode = ServiceBusHelper.ConnectivityMode;
            cboConnectivityMode.DataSource = Enum.GetValues(typeof(ConnectivityMode));
            cboConnectivityMode.SelectedItem = ConnectivityMode;

            cboTransportType.DataSource = Enum.GetValues(typeof(TransportType));
            var settings = new MessagingFactorySettings();
            cboTransportType.SelectedItem = settings.TransportType;

            cboServiceBusNamespace.SelectedIndex = connectionStringIndex > 0 ? connectionStringIndex : 0;
            if (cboServiceBusNamespace.Text == EnterConnectionString)
            {
                txtUri.Text = connectionString;
            }

            txtQueueFilterExpression.Text = FilterExpressionHelper.QueueFilterExpression;
            txtTopicFilterExpression.Text = FilterExpressionHelper.TopicFilterExpression;
            txtSubscriptionFilterExpression.Text = FilterExpressionHelper.SubscriptionFilterExpression;
            btnOk.Enabled = cboServiceBusNamespace.SelectedIndex > 1 || (cboServiceBusNamespace.Text == EnterConnectionString && !string.IsNullOrWhiteSpace(connectionString));
        }
        #endregion

        #region Public Properties
        public string Key { get; private set; }
        public string Uri { get; private set; }
        public string Namespace { get; private set; }
        public string ServicePath { get; set; }
        public string IssuerName { get; private set; }
        public string IssuerSecret { get; private set; }
        public string ConnectionString { get; private set; }
        public ConnectivityMode ConnectivityMode { get; private set; }
        public TransportType TransportType { get; set; }
        #endregion

        #region Event Handlers
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var connectionStringType = cboServiceBusNamespace.SelectedIndex > 1 ?
                                      serviceBusHelper.ServiceBusNamespaces[cboServiceBusNamespace.Text].ConnectionStringType :
                                      ServiceBusNamespaceType.Custom;

            Key = cboServiceBusNamespace.SelectedIndex > 1 &&
                  serviceBusHelper.ServiceBusNamespaces.ContainsKey(cboServiceBusNamespace.Text) ?
                  cboServiceBusNamespace.Text :
                  null;

            var containsStsEndpoint = !string.IsNullOrWhiteSpace(Key) &&
                                      !string.IsNullOrWhiteSpace(serviceBusHelper.ServiceBusNamespaces[Key].StsEndpoint);

            if (cboServiceBusNamespace.Text == EnterConnectionString || connectionStringType == ServiceBusNamespaceType.OnPremises || containsStsEndpoint)
            {
                ConnectionString = txtUri.Text;
                if (string.IsNullOrWhiteSpace(ConnectionString))
                {
                    MainForm.StaticWriteToLog(ConnectionStringCannotBeNull);
                    return;
                }
            }
            else
            {
                Uri = txtUri.Text;
                Namespace = txtNamespace.Text;
                IssuerName = txtIssuerName.Text;
                IssuerSecret = txtIssuerSecret.Text;
                TransportType = (TransportType)cboTransportType.SelectedItem;
                ConnectionString = string.Format(ServiceBusNamespace.ConnectionStringFormat,
                                                 Uri,
                                                 IssuerName,
                                                 IssuerSecret,
                                                 TransportType);
            }
            DialogResult = DialogResult.OK;
            ConnectivityMode = (ConnectivityMode)cboConnectivityMode.SelectedItem;
            FilterExpressionHelper.QueueFilterExpression = txtQueueFilterExpression.Text;
            FilterExpressionHelper.TopicFilterExpression = txtTopicFilterExpression.Text;
            FilterExpressionHelper.SubscriptionFilterExpression = txtSubscriptionFilterExpression.Text;
            connectionStringIndex = cboServiceBusNamespace.SelectedIndex;
            if (cboServiceBusNamespace.Text == EnterConnectionString)
            {
                connectionString = txtUri.Text;
            }
        }

        private void validation_TextChanged(object sender, EventArgs e)
        {
            var connectionStringType = cboServiceBusNamespace.SelectedIndex > 1 ?
                                       serviceBusHelper.ServiceBusNamespaces[cboServiceBusNamespace.Text].ConnectionStringType :
                                       ServiceBusNamespaceType.Custom;

            Key = cboServiceBusNamespace.SelectedIndex > 1 &&
                  serviceBusHelper.ServiceBusNamespaces.ContainsKey(cboServiceBusNamespace.Text) ?
                  cboServiceBusNamespace.Text :
                  null;

            var containsStsEndpoint = !string.IsNullOrWhiteSpace(Key) &&
                                      !string.IsNullOrWhiteSpace(serviceBusHelper.ServiceBusNamespaces[Key].StsEndpoint);

            if (cboServiceBusNamespace.Text == EnterConnectionString || connectionStringType == ServiceBusNamespaceType.OnPremises || containsStsEndpoint)
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
                var parameters = cn.Split(';').Where(p => p.Contains('=')).ToDictionary(s => s.Substring(0, s.IndexOf('=')).ToLower(), s => s.Substring(s.IndexOf('=') + 1));
                if (!parameters.ContainsKey(ConnectionStringTransportType))
                {
                    cboTransportType.SelectedItem = TransportType.NetMessaging;
                    return;
                }
                TransportType transportType;
                if (Enum.TryParse(parameters[ConnectionStringTransportType], true, out transportType))
                {
                    cboTransportType.SelectedItem = transportType;
                }
            }
            else
            {
                btnOk.Enabled = (!string.IsNullOrWhiteSpace(txtUri.Text) ||
                                !string.IsNullOrWhiteSpace(txtNamespace.Text)) &&
                                !string.IsNullOrWhiteSpace(txtIssuerName.Text) &&
                                !string.IsNullOrWhiteSpace(txtIssuerSecret.Text);
            }
        }

        private void cboServiceBusNamespace_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionStringType = cboServiceBusNamespace.SelectedIndex > 1 ?
                                       serviceBusHelper.ServiceBusNamespaces[cboServiceBusNamespace.Text].ConnectionStringType :
                                       ServiceBusNamespaceType.Custom;

            Key = cboServiceBusNamespace.SelectedIndex > 1 && 
                  serviceBusHelper.ServiceBusNamespaces.ContainsKey(cboServiceBusNamespace.Text) ? 
                  cboServiceBusNamespace.Text : 
                  null;

            var containsStsEndpoint = !string.IsNullOrWhiteSpace(Key) &&
                                      !string.IsNullOrWhiteSpace(serviceBusHelper.ServiceBusNamespaces[Key].StsEndpoint);

            if (cboServiceBusNamespace.Text == EnterConnectionString || connectionStringType == ServiceBusNamespaceType.OnPremises || containsStsEndpoint)
            {
                lblUri.Text = ConnectionStringLabel;
                txtUri.Multiline = true;
                txtUri.Size = new Size(336, 168);
                txtUri.Text = string.Empty;
                toolTip.SetToolTip(txtUri, ConnectionStringTooltip);
            }
            else
            {
                lblUri.Text = UriLabel;
                txtUri.Multiline = false;
                txtUri.Size = new Size(336, 20);
                toolTip.SetToolTip(txtUri, UriTooltip);
            }
            if (cboServiceBusNamespace.SelectedIndex <= 1)
            {
                return;
            }
            var ns = serviceBusHelper.ServiceBusNamespaces[cboServiceBusNamespace.Text];
            if (ns == null)
            {
                return;
            }
            if (connectionStringType == ServiceBusNamespaceType.OnPremises || containsStsEndpoint)
            {
                txtUri.Text = ns.ConnectionString;
            }
            else
            {
                txtUri.Text = ns.Uri;
                txtNamespace.Text = ns.Namespace;
                txtIssuerName.Text = ns.IssuerName;
                txtIssuerSecret.Text = ns.IssuerSecret;
            }
            cboTransportType.SelectedItem = ns.TransportType;
        }

        private void cboTransportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionStringType = cboServiceBusNamespace.SelectedIndex > 1 ?
                                       serviceBusHelper.ServiceBusNamespaces[cboServiceBusNamespace.Text].ConnectionStringType :
                                       ServiceBusNamespaceType.Custom;
            if (cboServiceBusNamespace.Text == EnterConnectionString || connectionStringType == ServiceBusNamespaceType.OnPremises)
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
                var parameters = cn.Split(';').Where(p => p.Contains('=')).ToDictionary(s => s.Substring(0, s.IndexOf('=')).ToLower(), s => s.Substring(s.IndexOf('=') + 1));
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
                                       transportType == TransportType.Amqp ? 
                                       DefaultAmqpRuntimePort :
                                       DefaultNetMessagingRuntimePort);
                }
                txtUri.Text = value;
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

        private void grouperServiceBusNamespaceSettings_CustomPaint(PaintEventArgs e)
        {
            var pen = new Pen(SystemColors.ActiveBorder, 1);
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
        #endregion
    }
}
