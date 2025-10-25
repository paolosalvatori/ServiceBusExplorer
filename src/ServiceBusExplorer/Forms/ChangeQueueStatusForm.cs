using Microsoft.ServiceBus.Messaging;
using System;
using System.IO;
using System.Windows.Forms;

namespace ServiceBusExplorer.Forms
{
    public partial class ChangeQueueStatusForm : Form
    {
        public EntityStatus EntityStatus { get; private set; }

        public ChangeQueueStatusForm(EntityStatus entityCurrentStatus)
        {
            InitializeComponent();
            SetSelected(entityCurrentStatus);
        }

        private void SetSelected(EntityStatus entityStatus)
        {
            if (!cbStatus.Items.Contains(entityStatus.ToString()))
            {
                throw new InvalidDataException($"Unexpected value {entityStatus} passed");
            }
            EntityStatus = entityStatus;
            cbStatus.SelectedIndex = cbStatus.Items.IndexOf(entityStatus.ToString());
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            EntityStatus = (EntityStatus)Enum.Parse(typeof(EntityStatus), cbStatus.SelectedItem.ToString());
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
