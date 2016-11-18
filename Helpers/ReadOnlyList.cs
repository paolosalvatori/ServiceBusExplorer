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
using System.Collections;
using System.Collections.Generic;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    internal class ReadOnlyList<T> : List<T>, ICollection<T>, IList
    {
        #region Public Constructors
        public ReadOnlyList() { }
        public ReadOnlyList(IEnumerable<T> collection) : base(collection) { }
        #endregion

        #region Public Properties
        bool ICollection<T>.IsReadOnly { get { return true; } }
        bool IList.IsReadOnly { get { return true; } }
        #endregion
    }
}
