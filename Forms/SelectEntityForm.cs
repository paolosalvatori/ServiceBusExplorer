#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
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
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Azure.NotificationHubs;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class SelectEntityForm : Form
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string QueueEntities = "Queues";
        private const string TopicEntities = "Topics";
        private const string EventHubEntities = "Event Hubs";
        private const string RelayEntities = "Relays";
        private const string NotificationHubEntities = "Notification Hubs";
        private const string SubscriptionPathFormat = "{0}/Subscriptions/{1}";
        private const string ConsumerGroupPathFormat = "{0}|{1}";
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string SubscriptionEntity = "Subscription";
        private const string EventHubEntity = "Event Hub";
        private const string ConsumerGroupEntity = "Consumer Group";
        private const string NotificationHubEntity = "Notification Hub";
        private const string RelayEntity = "Relay";
        #endregion

        #region Private Fields
        private TreeNode queueListNode;
        private TreeNode topicListNode;
        private TreeNode eventHubListNode;
        private TreeNode notificationHubListNode;
        private TreeNode relayListNode;
        private readonly bool includeSubscriptions;
        private readonly bool includeEventHubs;
        private readonly bool includeNotificationHubs;
        private readonly bool includeRelays;
        #endregion

        #region Public Constructor
        public SelectEntityForm(string dialogTitle, 
                                string groupTitle, 
                                string labelText, 
                                bool subscriptions = false, 
                                bool eventHubs = false, 
                                bool notificationHubs = false,
                                bool relays = false)
        {
            includeSubscriptions = subscriptions;
            includeEventHubs = eventHubs;
            includeNotificationHubs = notificationHubs;
            includeRelays = relays;
            InitializeComponent();
            if (MainForm.SingletonMainForm != null &&
                MainForm.SingletonMainForm.ServiceBusTreeView != null)
            {
                CloneNode(MainForm.SingletonMainForm.ServiceBusTreeView.Nodes[0], null);
            }
            if (serviceBusTreeView.Nodes.Count > 0 &&
                serviceBusTreeView.Nodes[0] != null)
            {
                serviceBusTreeView.Nodes[0].Expand();
            }
            if (queueListNode != null)
            {
                queueListNode.Expand();
            }
            if (topicListNode != null)
            {
                topicListNode.Expand();
            }
            if (eventHubListNode != null)
            {
                eventHubListNode.Expand();
            }
            if (notificationHubListNode != null)
            {
                notificationHubListNode.Expand();
            }
            if (relayListNode != null)
            {
                relayListNode.Expand();
            }
            Text = dialogTitle;
            grouperTreeView.GroupTitle = groupTitle;
            lblTargetQueueTopic.Text = labelText;
        }
        #endregion

        #region Event Handlers
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Path = null;
            Close();
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
        #endregion

        #region Public Properties
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public string Path { get; private set; }
        public string Type { get; private set; }
        #endregion

        #region Private Methods
        private void txtForwardTo_TextChanged(object sender, EventArgs e)
        {
            Path = txtEntity.Text;
        }

        private void TextForm_Activated(object sender, EventArgs e)
        {
            txtEntity.Focus();
        }

        private void CloneNode(TreeNode node, TreeNode parent)
        {
            if (node == null || 
                (!includeRelays && string.Compare(node.Text, RelayEntities, StringComparison.InvariantCultureIgnoreCase) == 0) ||
                (!includeNotificationHubs && string.Compare(node.Text, NotificationHubEntities, StringComparison.InvariantCultureIgnoreCase) == 0) || 
                (!includeEventHubs && string.Compare(node.Text, EventHubEntities, StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                return;
            }
            
            var newNode = parent == null ?
                              serviceBusTreeView.Nodes.Add(node.Text, node.Text, node.ImageIndex, node.StateImageIndex) :
                              parent.Nodes.Add(node.Text, node.Text, node.ImageIndex, node.SelectedImageIndex);
            if (node.Tag != null)
            {
                if (node.Tag is QueueDescription ||
                    node.Tag is TopicDescription ||
                    (includeSubscriptions && node.Tag is SubscriptionWrapper && ((SubscriptionWrapper)node.Tag).SubscriptionDescription != null) ||
                    (includeEventHubs && (node.Tag is EventHubDescription || node.Tag is ConsumerGroupDescription)) ||
                    (includeNotificationHubs && node.Tag is NotificationHubDescription) ||
                    (includeRelays && node.Tag is RelayDescription))
                {
                    newNode.Tag = node.Tag;
                }
            }
            if (newNode.Text == QueueEntities)
            {
                queueListNode = newNode;
            }
            if (newNode.Text == TopicEntities)
            {
                topicListNode = newNode;
            }
            if (includeEventHubs && string.Compare(newNode.Text, EventHubEntities, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                eventHubListNode = newNode;
            }
            if (includeNotificationHubs && string.Compare(newNode.Text, NotificationHubEntities, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                notificationHubListNode = newNode;
            }
            if (includeRelays && string.Compare(newNode.Text, RelayEntities, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                relayListNode = newNode;
            }
            if (node.Tag is TopicDescription && !includeSubscriptions)
            {
                return;
            }
            if (node.Tag is ConsumerGroupDescription ||
                node.Tag is NotificationHubDescription ||
                node.Tag is RelayDescription)
            {
                return;
            }
            var tag = node.Tag as SubscriptionWrapper;
            if (tag != null && tag.SubscriptionDescription != null)
            {
                return;
            }
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                CloneNode(node.Nodes[i], newNode);
            }
        }

        private void serviceBusTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var queueTag = e.Node.Tag as QueueDescription;
            if (queueTag != null)
            {
                txtEntity.Text = queueTag.Path;
                Type = QueueEntity;
                return;
            }
            var topicTag = e.Node.Tag as TopicDescription;
            if (topicTag != null)
            {
                txtEntity.Text = topicTag.Path;
                Type = TopicEntity;
                return;
            }
            var subscriptionTag = e.Node.Tag as SubscriptionWrapper;
            if (subscriptionTag != null)
            {
                txtEntity.Text = string.Format(SubscriptionPathFormat, 
                                               subscriptionTag.SubscriptionDescription.TopicPath, 
                                               subscriptionTag.SubscriptionDescription.Name);
                Type = SubscriptionEntity;
            }
            var eventHubTag = e.Node.Tag as EventHubDescription;
            if (eventHubTag != null)
            {
                txtEntity.Text = eventHubTag.Path;
                Type = EventHubEntity;
            }
            var consumerGroupTag = e.Node.Tag as ConsumerGroupDescription;
            if (consumerGroupTag != null)
            {
                txtEntity.Text = string.Format(ConsumerGroupPathFormat, 
                                               consumerGroupTag.EventHubPath,
                                               consumerGroupTag.Name);
                Type = ConsumerGroupEntity;
            }
            var notificationHubTag = e.Node.Tag as NotificationHubDescription;
            if (notificationHubTag != null)
            {
                txtEntity.Text = notificationHubTag.Path;
                Type = NotificationHubEntity;
            }
            var relayTag = e.Node.Tag as RelayDescription;
            if (relayTag != null)
            {
                txtEntity.Text = relayTag.Path;
                Type = RelayEntity;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEntity.Text = null;
        }
        #endregion
    }
}
