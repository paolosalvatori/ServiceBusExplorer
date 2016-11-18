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
    public class InMemoryMessageDeferProvider : IMessageDeferProvider
    {
        #region Private Fields
        private readonly Queue<long> queue = new Queue<long>();
        #endregion

        #region Public Properties
        public int Count
        {
            get
            {
                lock (this)
                {
                    return queue.Count;
                }
            }
        }
        #endregion

        #region Public Methods
        public bool Dequeue(out long sequenceNumber)
        {
            sequenceNumber = -1;
            lock (this)
            {
                if (queue.Count > 0)
                {
                    sequenceNumber = queue.Dequeue();
                    return true;
                }
            }
            return false;
        }

        public void Enqueue(long sequenceNumber)
        {
            lock (this)
            {
                queue.Enqueue(sequenceNumber);
            }
        }
        #endregion
    }
}
