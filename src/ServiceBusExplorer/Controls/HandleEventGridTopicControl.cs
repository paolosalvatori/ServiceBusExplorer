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
            var startX = e.ColumnIndex == 0 ? -1 : e.Bounds.X;
            var endX = e.Bounds.X + e.Bounds.Width - 1;
            // Background
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(215, 228, 242)), startX, -1, e.Bounds.Width + 1,
                e.Bounds.Height + 1);
            // Left vertical line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, startX,
                e.Bounds.Y + e.Bounds.Height + 1);
            // TopCount horizontal line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, endX, -1);
            // Bottom horizontal line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), startX, e.Bounds.Height - 1, endX,
                e.Bounds.Height - 1);
            // Right vertical line
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), endX, -1, endX, e.Bounds.Height + 1);
            var roundedFontSize = (float)Math.Round(e.Font.SizeInPoints);
            var bounds = new RectangleF(e.Bounds.X + 4, (e.Bounds.Height - 8 - roundedFontSize) / 2, e.Bounds.Width,
                roundedFontSize + 6);
            e.Graphics.DrawString(e.Header.Text, e.Font, new SolidBrush(SystemColors.ControlText), bounds);
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
