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
using System.Windows.Forms;
using System.Drawing;
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
        private const string RelayServiceEntities = "Relay Services";
        private const string NotificationHubEntities = "Notification Hubs";
        private const string SubscriptionPathFormat = "{0}/Subscriptions/{1}";
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string SubscriptionEntity = "Subscription";
        #endregion

        #region Private Fields
        private TreeNode queueListNode;
        private TreeNode topicListNode;
        private readonly bool includeSubscriptions;
        #endregion

        #region Public Constructor
        public SelectEntityForm(string dialogTitle, string groupTitle, string labelText, bool subscriptions = false)
        {
            includeSubscriptions = subscriptions;
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
            if (node == null || node.Text == RelayServiceEntities || node.Text == NotificationHubEntities)
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
                    (includeSubscriptions && 
                     node.Tag is SubscriptionWrapper &&
                     ((SubscriptionWrapper)node.Tag).SubscriptionDescription != null))
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
            if (node.Tag is TopicDescription && !includeSubscriptions)
            {
                return;
            }
            if (node.Tag is SubscriptionWrapper && 
                ((SubscriptionWrapper)node.Tag).SubscriptionDescription != null)
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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEntity.Text = null;
        }
        #endregion
    }
}
