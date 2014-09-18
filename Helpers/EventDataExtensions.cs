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
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class EventDataExtensions
    {
        private static readonly PropertyInfo bodyStreamPropertyInfo;

        static EventDataExtensions()
        {
            try
            {
                var type = typeof (EventData);
                bodyStreamPropertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                                             .FirstOrDefault(p => string.Compare("BodyStream", p.Name, StringComparison.OrdinalIgnoreCase) == 0);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        public static EventData Clone(this EventData eventData, Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            var clone = new EventData(stream);
            foreach (var pair in eventData.SystemProperties)
            {
                clone.SystemProperties.Add(pair);
            }
            foreach (var pair2 in eventData.Properties)
            {
                clone.Properties.Add(pair2);
            }
            return clone;
        }

        public static EventData Clone(this EventData eventData, string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            var clone = eventData.Clone();
            bodyStreamPropertyInfo.SetValue(clone, stream);
            return clone;
        }

        public static Stream GetBodyStream(this EventData eventData)
        {
            return bodyStreamPropertyInfo.GetValue(eventData) as Stream;
        }

        public static void SetBodyStream(this EventData eventData, Stream stream)
        {
            bodyStreamPropertyInfo.SetValue(eventData, stream);
        }
    }
}
