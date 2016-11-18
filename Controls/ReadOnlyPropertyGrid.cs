#region Using Directives
using System;
using System.ComponentModel;
using System.Windows.Forms;
#endregion
namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer.Controls
{
    public class ReadOnlyPropertyGrid : PropertyGrid
    {
        private bool readOnly;

        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                SetObjectAsReadOnly();
            }
        }

        protected override void OnSelectedObjectsChanged(EventArgs e)
        {
            SetObjectAsReadOnly();
            base.OnSelectedObjectsChanged(e);
        }

        private void SetObjectAsReadOnly()
        {
            if (SelectedObject == null)
            {
                return;
            }
            TypeDescriptor.AddAttributes(SelectedObject, new Attribute[] { new ReadOnlyAttribute(readOnly) });
            Refresh();
        }
    }
}
