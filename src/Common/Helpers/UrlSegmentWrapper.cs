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
using ServiceBusExplorer.Enums;

#endregion

namespace ServiceBusExplorer.Helpers
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
