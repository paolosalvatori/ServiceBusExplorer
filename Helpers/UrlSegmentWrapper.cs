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
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    /// <summary>
    /// This class contains the information of a Url Segment
    /// </summary>
    public class UrlSegmentWrapper
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the UrlSegmentWrapper class.
        /// </summary>
        /// <param name="type">The entity type.</param>
        /// <param name="uri">The url segment uri.</param>
        public UrlSegmentWrapper(EntityType type, Uri uri)
        {
            EntityType = type;
            Uri = uri;
        }
        #endregion

        #region Public Properties
        public EntityType EntityType { get; set; }
        public Uri Uri { get; set; }
        #endregion
    }
}