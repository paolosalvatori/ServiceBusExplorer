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
    /// This class represents the arguments for the events of the ServiceBusHelper 
    /// </summary>
    public class ServiceBusHelperEventArgs : EventArgs
    {
        #region Public Constructors
        public ServiceBusHelperEventArgs()
        {
            EntityInstance = null;
        }

        public ServiceBusHelperEventArgs(dynamic entityInstance, EntityType entityType)
        {
            EntityInstance = entityInstance;
            EntityType = entityType;
        }
        #endregion

        #region Public Constructors
        public dynamic EntityInstance { get; set; }
        public EntityType EntityType { get; set; }
        #endregion
    }
}
