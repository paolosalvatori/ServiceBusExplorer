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
using System.Collections.Generic;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    /// <summary>
    /// A list which acts like the List class, but with more of a java
    /// influence. This allows you to set
    /// a looping variable to true, and creates
    /// a circular list. Also it utilizes the java iterator pattern of
    /// Next and HasNext.
    /// 
    /// Author: Wesley Tansey
    /// </summary>
    /// <typeparam name="T">The type of item that
    /// will be stored in the list</typeparam>
    public class CircularList<T> : List<T>
    {
        #region Private Fields
        private int index;
        #endregion

        #region Public Constructors
        public CircularList()
        {
        }

        public CircularList(int capacity)
            : base(capacity)
        {
        }

        public CircularList(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The next element in the list. The user is responsible for
        /// making sure that HasNext is true
        /// before getting the next element.
        /// </summary>
        public T Next
        {
            get
            {
                lock (this)
                {
                    if (index >= Count)
                    {
                        index = 0;
                    }
                    return this[index++];
                }
            }
        }
        #endregion
    }
}
