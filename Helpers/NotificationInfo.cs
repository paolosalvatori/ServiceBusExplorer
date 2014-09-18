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
using System.ComponentModel;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class NotificationInfo
    {
        #region Public Constructors
        public NotificationInfo()
        {
            Name = null;
            Value = null;
        }

        public NotificationInfo(string key, string value)
        {
            Name = key;
            Value = value;
        }
        #endregion

        #region Static Constructor
        static NotificationInfo()
        {
            TemplateProperties = new List<NotificationInfo>();
            TemplateHeaders = new List<NotificationInfo>();
            MpnsHeaders = new List<NotificationInfo>();
            WnsHeaders = new List<NotificationInfo>();
            GcmHeaders = new List<NotificationInfo>();
            ApnsHeaders = new List<NotificationInfo>();
            TemplateTags = new List<TagInfo>();
            MpnsTags = new List<TagInfo>();
            WnsTags = new List<TagInfo>();
            GcmTags = new List<TagInfo>();
            ApnsTags = new List<TagInfo>();
        }
        #endregion

        #region Public Instance Properties
        public string Name { get; set; }
        public string Value { get; set; }
        #endregion

        #region Public Static Properties
        public static List<NotificationInfo> TemplateProperties { get; private set; }
        public static List<NotificationInfo> TemplateHeaders { get; private set; }
        public static List<NotificationInfo> MpnsHeaders { get; private set; }
        public static List<NotificationInfo> WnsHeaders { get; private set; }
        public static List<NotificationInfo> GcmHeaders { get; private set; }
        public static List<NotificationInfo> ApnsHeaders { get; private set; }
        public static List<TagInfo> TemplateTags { get; private set; }
        public static List<TagInfo> MpnsTags { get; private set; } 
        public static List<TagInfo> WnsTags { get; private set; }
        public static List<TagInfo> GcmTags { get; private set; } 
        public static List<TagInfo> ApnsTags { get; private set; } 
        #endregion
    }

    public class TagInfo
    {
        #region Public Constructors
        public TagInfo(){}

        public TagInfo(string tag)
        {
            Tag = tag;
        }
        #endregion

        #region Public Instance Properties
        public string Tag { get; set; }
        #endregion
    }
}