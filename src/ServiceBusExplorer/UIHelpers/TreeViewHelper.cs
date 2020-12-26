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
using System.Collections;
using System.Windows.Forms;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    public class TreeViewHelper : IComparer
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string QueueEntities = "Queues";
        private const string TopicEntities = "Topics";
        private const string EventHubsEntities = "Event Hubs";
        private const string NotificationHubsEntities = "Notification Hubs";
        private const string RelayEntities = "Relays";
        private const string PartitionEntities = "Partitions";
        private const string ConsumerGroupEntities = "Consumer Groups";
        #endregion

        #region IComparer Methods
        public int Compare(object x, object y)
        {
            var leftNode = x as TreeNode;
            var rightNode = y as TreeNode;

            if (leftNode == null ||
                rightNode == null)
            {
                return 0;
            }

            if (leftNode.Name.StartsWith(QueueEntities))
            {
                return -1;
            }

            if (leftNode.Name.StartsWith(TopicEntities))
            {
                return rightNode.Name == QueueEntities ? 1 : -1;
            }

            if (leftNode.Name.StartsWith(EventHubsEntities))
            {
                return rightNode.Name == QueueEntities || 
                       rightNode.Name == TopicEntities ? 1 : -1;
            }

            if (leftNode.Name.StartsWith(NotificationHubsEntities))
            {
                return rightNode.Name == QueueEntities ||
                       rightNode.Name == TopicEntities ||
                       rightNode.Name == EventHubsEntities ? 1 : -1;
            }

            if (leftNode.Name.StartsWith(ConsumerGroupEntities))
            {
                return rightNode.Name == PartitionEntities ? 1 : -1;
            }

            if (leftNode.Name.StartsWith(PartitionEntities))
            {
                return -1;
            }

            if (leftNode.Name.StartsWith(RelayEntities))
            {
                return 1;
            }

            if (leftNode.ImageIndex == MainForm.UrlSegmentIconIndex &&
                rightNode.ImageIndex == MainForm.UrlSegmentIconIndex)
            {
                return String.CompareOrdinal(leftNode.Text, rightNode.Text);
            }

            if (leftNode.ImageIndex == MainForm.UrlSegmentIconIndex)
            {
                return -1;
            }

            if (rightNode.ImageIndex == MainForm.UrlSegmentIconIndex)
            {
                return 1;
            }

            return String.CompareOrdinal(leftNode.Text, rightNode.Text);
        } 
        #endregion
    }
}
