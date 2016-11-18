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
