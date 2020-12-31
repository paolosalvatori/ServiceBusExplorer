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

using Azure.Messaging.ServiceBus.Administration;

#endregion

namespace ServiceBusExplorer.ServiceBus.Helpers
{
    /// <summary>
    /// This class adds a Subscriptions collection to the TopicProperties class
    /// </summary>
    public class SubscriptionWrapper2
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the SubscriptionWrapper class.
        /// </summary>
        public SubscriptionWrapper2()
        {
            SubscriptionProperties = null;
            TopicProperties = null;
            Filter = null;
        }

        /// <summary>
        /// Initializes a new instance of the SubscriptionWrapper class.
        /// </summary>
        /// <param name="subscription">A subscription.</param>
        /// <param name="topic">The topic the subscription belongs to.</param>
        public SubscriptionWrapper2(SubscriptionProperties subscription, TopicProperties topic)
        {
            SubscriptionProperties = subscription;
            TopicProperties = topic;
        }

        /// <summary>
        /// Initializes a new instance of the SubscriptionWrapper class.
        /// </summary>
        /// <param name="subscription">A subscription.</param>
        /// <param name="topic">The topic the subscription belongs to.</param>
        /// <param name="filter">The OData filter.</param>
        public SubscriptionWrapper2(SubscriptionProperties subscription, TopicProperties topic, string filter)
        {
            SubscriptionProperties = subscription;
            TopicProperties = topic;
            Filter = filter;
        }
        #endregion

        #region Public Properties
        public SubscriptionProperties SubscriptionProperties { get; set; }
        public TopicProperties TopicProperties { get; set; }
        public string Filter { get; set; }
        #endregion
    }
}
