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
using System.Collections.Generic;
using System.Linq;

#endregion


namespace ServiceBusExplorer.Helpers
{
    public class UserPropertyWrapper
    {
        #region Private Fields
        private string name;
        private object val;
        #endregion

        #region Public Constructor
		public UserPropertyWrapper()
        {
        }

        public UserPropertyWrapper(KeyValuePair<string, object> property)
        {
            name = property.Key;
            val = property.Value;
        }
	    #endregion

        #region Public Properties
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value; 
            }
        }

        public object Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;
            }
        }
        #endregion
    }
}
