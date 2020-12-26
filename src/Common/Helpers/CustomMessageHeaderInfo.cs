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

namespace ServiceBusExplorer.Helpers
{
    public class CustomMessageHeaderInfo
    {
        #region Public Constructors
        public CustomMessageHeaderInfo()
        {

            Name = null;
            Namespace = null;
            Value = null;
        }

        public CustomMessageHeaderInfo(string key, string type, string value)
        {
            Name = key;
            Namespace = type;
            Value = value;
        }
        #endregion

        #region Static Constructor
        static CustomMessageHeaderInfo()
        {
            Headers = new List<CustomMessageHeaderInfo>();
        }
        #endregion

        #region Public Instance Properties
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Value { get; set; }
        #endregion

        #region Public Static Properties
        public static List<CustomMessageHeaderInfo> Headers { get; set; }
        #endregion
    }
}