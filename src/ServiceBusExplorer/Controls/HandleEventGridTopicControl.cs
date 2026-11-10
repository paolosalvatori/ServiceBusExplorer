using Azure.ResourceManager.EventGrid;
using ServiceBusExplorer.UIHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceBusExplorer.Controls
{
    public partial class HandleEventGridTopicControl : UserControl
    {
        #region Private Constants
        //***************************
        // Property Labels
        //***************************
        private const string Id = "ID";
        private const string TopicName = "Topic Name";
        private const string TopicEndpoint = "Topic Endpoint";
        private const string InputSchema = "Input Schema";
        private const string EventRetentionInDays = "Event Retention (Days)";
        private const string ProvisioningState = "Provisioning State";
        private const string PublisherType = "Publisher Type";
        #endregion

        #region Private Fields
        private NamespaceTopicResource topic;
        #endregion

        public HandleEventGridTopicControl(NamespaceTopicResource topic, string hostname)
        {
            this.topic = topic;
            InitializeComponent();
            ConfigureReadUserInterface(hostname);
        }

        private void ConfigureReadUserInterface(string hostname)
        {
            // Initialize topic property list
            var propertyList = new List<string[]>();

            propertyList.AddRange(new[]
            {
                new[] {Id, topic.Id},
                new[] {TopicName, topic.Data.Name},
                new[] {TopicEndpoint, hostname + "/topics/" + topic.Data.Name},
                new[] {InputSchema, topic.Data.InputSchema.ToString()},
                new[] {EventRetentionInDays, topic.Data.EventRetentionInDays.ToString()},
                new[] {ProvisioningState, topic.Data.ProvisioningState.ToString()},
                new[] {PublisherType, topic.Data.PublisherType.ToString()},
            });

            foreach (var array in propertyList)
            {
                topicListView.Items.Add(new ListViewItem(array));
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
