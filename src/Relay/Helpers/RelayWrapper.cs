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

#endregion

namespace ServiceBusExplorer.Relay.Helpers
{
    /// <summary>
    /// This class contains the information of a Relay
    /// </summary>
    public class RelayWrapper
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the RelayWrapper class.
        /// </summary>
        /// <param name="name">The service name.</param>
        /// <param name="uri">The service uri.</param>
        public RelayWrapper(string name, Uri uri)
        {
            Name = name;
            Uri = uri;
        }
        #endregion

        #region Public Properties
        public string Name { get; set; }
        public Uri Uri { get; set; }
        #endregion
    }
}
