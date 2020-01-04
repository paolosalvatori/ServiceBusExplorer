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

using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Helpers
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
