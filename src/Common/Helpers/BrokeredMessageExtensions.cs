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

using Microsoft.Azure.ServiceBusExplorer.Helpers;
using Microsoft.ServiceBus.Messaging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

#endregion

namespace ServiceBusExplorer.Helpers
{
    public static class BrokeredMessageExtensions
    {
        private static readonly PropertyInfo BodyStreamPropertyInfo;

        static BrokeredMessageExtensions()
        {
            try
            {
                var type = typeof (BrokeredMessage);
                BodyStreamPropertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                                             .FirstOrDefault(p => string.Compare("BodyStream", p.Name, StringComparison.OrdinalIgnoreCase) == 0);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        public static BrokeredMessage Clone(this BrokeredMessage message, Stream stream, bool omitAllProperties)
        {
            if (stream != null && stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            var clone = message.Clone();

            if (omitAllProperties)
            {
                clone.Properties.Clear();
            }

            BodyStreamPropertyInfo.SetValue(clone, stream);

            return clone;
        }

        public static BrokeredMessage Clone(this BrokeredMessage message, string text, bool omitAllProperties)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            var clone = message.Clone();
            if (omitAllProperties)
            {
                clone.Properties.Clear();
            }
            BodyStreamPropertyInfo.SetValue(clone, stream);
            return clone;
        }

        public static BrokeredMessage CloneWithByteArrayBodyType(this BrokeredMessage originalMessage, string text, bool omitAllProperties)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var message = new BrokeredMessage(bytes);

            if (!omitAllProperties)
            {
                // Copy all custom properties
                var propertiesToCopy = originalMessage.Properties.Where(prop => !Constants.AlwaysOmittedProperties.Exists(
                    omitProp => prop.Key.Equals(omitProp, StringComparison.InvariantCultureIgnoreCase)));

                propertiesToCopy.ForEach(h => message.Properties[h.Key] = h.Value);
            }

            // Required standard properties
            message.CorrelationId = originalMessage.CorrelationId;
            message.ReplyTo = originalMessage.ReplyTo;
            message.TimeToLive = originalMessage.TimeToLive;
            message.ScheduledEnqueueTimeUtc = originalMessage.ScheduledEnqueueTimeUtc;
            message.ViaPartitionKey = originalMessage.ViaPartitionKey;
            message.ContentType = originalMessage.ContentType;
            message.Label = originalMessage.Label;

            return message;
        }

        public static Stream GetBodyStream(this BrokeredMessage message)
        {
            return BodyStreamPropertyInfo.GetValue(message) as Stream;
        }

        public static void SetBodyStream(this BrokeredMessage message, Stream stream)
        {
            BodyStreamPropertyInfo.SetValue(message, stream);
        }
    }
}
