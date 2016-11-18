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
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    /// <summary>
    /// This class adds a Subscriptions collection to the TopicDescription class
    /// </summary>
    public class SubscriptionWrapper
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the SubscriptionWrapper class.
        /// </summary>
        public SubscriptionWrapper()
        {
            SubscriptionDescription = null;
            TopicDescription = null;
            Filter = null;
        }

        /// <summary>
        /// Initializes a new instance of the SubscriptionWrapper class.
        /// </summary>
        /// <param name="subscription">A subscription.</param>
        /// <param name="topic">The topic the subscription belongs to.</param>
        public SubscriptionWrapper(SubscriptionDescription subscription, TopicDescription topic)
        {
            SubscriptionDescription = subscription;
            TopicDescription = topic;
        }

        /// <summary>
        /// Initializes a new instance of the SubscriptionWrapper class.
        /// </summary>
        /// <param name="subscription">A subscription.</param>
        /// <param name="topic">The topic the subscription belongs to.</param>
        /// <param name="filter">The OData filter.</param>
        public SubscriptionWrapper(SubscriptionDescription subscription, TopicDescription topic, string filter)
        {
            SubscriptionDescription = subscription;
            TopicDescription = topic;
            Filter = filter;
        }
        #endregion

        #region Public Properties
        public SubscriptionDescription SubscriptionDescription { get; set; }
        public TopicDescription TopicDescription { get; set; }
        public string Filter { get; set; }
        #endregion
    }
}
