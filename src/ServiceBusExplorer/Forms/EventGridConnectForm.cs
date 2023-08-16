using ServiceBusExplorer.Helpers;
using System;
using System.Collections.Generic;
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
        public List<string> SelectedEntities = new List<string> { Constants.TopicEntities, Constants.SubscriptionEntities };
        #endregion

        public EventGridConnectForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ResourceGroup = txtResourceGroupName.Text.Trim();
            NamespaceName = txtNamespaceName.Text.Trim();
            SubscriptionId = txtSubscriptionId.Text.Trim();
            ApiVersion = txtApiVersion.Text.Trim();

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
