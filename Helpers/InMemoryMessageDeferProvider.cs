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
