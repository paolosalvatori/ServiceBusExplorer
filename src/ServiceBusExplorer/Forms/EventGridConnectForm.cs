using ServiceBusExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ServiceBusExplorer.Forms
{
    public partial class EventGridConnectForm : Form
    {
        #region Public Fields
        public string ResourceGroup;
        public string NamespaceName;
        public string SubscriptionId;
        public string ApiVersion;
        public int RetryTimeout;
        public string CloudTenant;
        public string CustomId;
        public List<string> SelectedEntities = new List<string> { Constants.TopicEntities, Constants.SubscriptionEntities };
        #endregion

        public EventGridConnectForm()
        {
            InitializeComponent();
        }

        private void customCloud_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomId.Enabled = btnCustomCloud.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ResourceGroup = txtResourceGroupName.Text.Trim();
            NamespaceName = txtNamespaceName.Text.Trim();
            SubscriptionId = txtSubscriptionId.Text.Trim();
            ApiVersion = cboApiVersion.Text.Trim(); 

            int.TryParse(txtRetryTimeout.Text, out var retryTimoutInSeconds);
            RetryTimeout = retryTimoutInSeconds * 1000; // Convert to milliseconds
            CloudTenant = cloudGroupBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
            CustomId = txtCustomId.Text?.Trim();

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EventGridConnectForm_Load(object sender, EventArgs e)
        {
            cboApiVersion.SelectedIndex = 0;
        }
    }
}
