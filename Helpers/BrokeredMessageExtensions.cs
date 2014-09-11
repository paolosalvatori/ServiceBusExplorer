#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
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
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class BrokeredMessageExtensions
    {
        private static readonly PropertyInfo bodyStreamPropertyInfo;

        static BrokeredMessageExtensions()
        {
            try
            {
                var type = typeof (BrokeredMessage);
                bodyStreamPropertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                                             .FirstOrDefault(p => string.Compare("BodyStream", p.Name, StringComparison.OrdinalIgnoreCase) == 0);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        public static BrokeredMessage Clone(this BrokeredMessage message, Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            var clone = message.Clone();
            bodyStreamPropertyInfo.SetValue(clone, stream);
            return clone;
        }

        public static BrokeredMessage Clone(this BrokeredMessage message, string text)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            var clone = message.Clone();
            bodyStreamPropertyInfo.SetValue(clone, stream);
            return clone;
        }

        public static Stream GetBodyStream(this BrokeredMessage message)
        {
            return bodyStreamPropertyInfo.GetValue(message) as Stream;
        }

        public static void SetBodyStream(this BrokeredMessage message, Stream stream)
        {
            bodyStreamPropertyInfo.SetValue(message, stream);
        }
    }
}
