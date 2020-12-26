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

using System.Collections.Generic;

#endregion

namespace ServiceBusExplorer.Utilities.Helpers
{
    /// <summary>
    /// A list which acts like the List class, but with more of a java
    /// influence. This allows you to set
    /// a looping variable to true, and creates
    /// a circular list. Also it utilizes the java iterator pattern of
    /// Next and HasNext.
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
