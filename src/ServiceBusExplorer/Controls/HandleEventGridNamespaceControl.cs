using Azure.ResourceManager.EventGrid;
using ServiceBusExplorer.UIHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceBusExplorer.Controls
{
    public partial class HandleEventGridNamespaceControl : UserControl
    {
        #region Private Constants
        //***************************
        // Property Labels
        //***************************
        private const string Id = "ID";
        private const string NamespaceName = "Name";
        private const string NamespaceLocation = "Location";
        private const string ProvisioningState = "Provisioning State";
        private const string PublicNetworkAccess = "Public Network Access";
        private const string MinimumTlsVersionAllowed = "Minimum TLS Version Allowed";
        #endregion

        #region Private Fields
        private EventGridNamespaceResource eventGridNamespace;
        #endregion

        public HandleEventGridNamespaceControl(EventGridNamespaceResource eventGridNamespace)
        {
            this.eventGridNamespace = eventGridNamespace;
            InitializeComponent();
            ConfigureReadUserInterface();
        }

        private void ConfigureReadUserInterface()
        {
            // Initialize namespace property list
            var propertyList = new List<string[]>();

            propertyList.AddRange(new[]
            {
                new[] {Id, eventGridNamespace.Id},
                new[] {NamespaceName, eventGridNamespace.Data.Name},
                new[] {NamespaceLocation, eventGridNamespace.Data.Location.ToString()},
                new[] {ProvisioningState, eventGridNamespace.Data.ProvisioningState.ToString()},
                new[] {PublicNetworkAccess, eventGridNamespace.Data.PublicNetworkAccess.ToString()},
                new[] {MinimumTlsVersionAllowed, eventGridNamespace.Data.MinimumTlsVersionAllowed.ToString()}
            });

            foreach (var array in propertyList)
            {
                namespaceListView.Items.Add(new ListViewItem(array));
            }
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            ThemeManager.DrawListViewColumnHeader(e);
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
        }

        private void listView_Resize(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null)
            {
                return;
            }
            try
            {
                listView.SuspendDrawing();
                listView.SuspendLayout();
                var width = listView.Width - listView.Columns[0].Width - 5;
                var scrollbars = ScrollBarHelper.GetVisibleScrollbars(listView);
                if (scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both)
                {
                    width -= 17;
                }
                listView.Columns[1].Width = width;
            }
            finally
            {
                listView.ResumeLayout();
                listView.ResumeDrawing();
            }
        }
    }
}
