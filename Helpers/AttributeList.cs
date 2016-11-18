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
using System.Collections.Generic;
using System.ComponentModel; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    internal class AttributeList : List<Attribute>
    {
        #region Public Conctructors
        public AttributeList()
        {

        }

        public AttributeList(AttributeCollection ac)
        {
            foreach (Attribute attr in ac)
            {
                Add(attr);
            }
        } 
        #endregion
    }
}