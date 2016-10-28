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
    /// This class contains the information of a Relay Service
    /// </summary>
    public class RelayServiceWrapper
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the RelayServiceWrapper class.
        /// </summary>
        /// <param name="name">The service name.</param>
        /// <param name="uri">The service uri.</param>
        public RelayServiceWrapper(string name, Uri uri)
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