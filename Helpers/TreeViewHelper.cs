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
using System.Collections;
using System.Windows.Forms;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class TreeViewHelper : IComparer
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string QueueEntities = "Queues";
        private const string TopicEntities = "Topics";
        private const string NotificationHubsEntities = "Notification Hubs";
        private const string RelayServiceEntities = "Relay Services";
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

            if (leftNode.Name.StartsWith(NotificationHubsEntities))
            {
                return rightNode.Name == QueueEntities || rightNode.Name == TopicEntities ? 1 : -1;
            }

            if (leftNode.Name.StartsWith(RelayServiceEntities))
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
