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

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class FilterExpressionHelper
    {
        #region Public Static Properties
        public static string QueueFilterExpression { get; set; }
        public static string TopicFilterExpression { get; set; }
        public static string SubscriptionFilterExpression { get; set; }
        #endregion
    }
}
