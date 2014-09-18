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

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    /// <summary>
    /// This class adds a Subscriptions collection to the TopicDescription class
    /// </summary>
    public class RuleWrapper
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the RuleWrapper class.
        /// </summary>
        public RuleWrapper()
        {
            RuleDescription = null;
            SubscriptionDescription = null;
        }

        /// <summary>
        /// Initializes a new instance of the RuleWrapper class.
        /// </summary>
        /// <param name="rule">A rule.</param>
        /// <param name="subscription">A subscription.</param>        
        public RuleWrapper(RuleDescription rule, SubscriptionDescription subscription)
        {
            RuleDescription = rule;
            SubscriptionDescription = subscription;
        }
        #endregion

        #region Public Properties
        public RuleDescription RuleDescription { get; set; }
        public SubscriptionDescription SubscriptionDescription { get; set; }
        #endregion
    }
}
