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
using System.Text;
using System.Xml.Serialization; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class XmlSerializerHelper
    {
        /// <summary>
        /// Serialize an object using the XmlSerializer.
        /// </summary>
        /// <param name="item">The object that must be serialized</param>
        /// <returns>A XML string.</returns>
        public static string Serialize(object item)
        {
            if (item == null)
            {
                return null;
            }
            using (var memoryStream = new MemoryStream())
            {
                using (var stringWriter = new StreamWriter(memoryStream, Encoding.ASCII))
                {
                    var serializer = new XmlSerializer(item.GetType());
                    serializer.Serialize(stringWriter, item);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Deserialize an XML string into an object using the XmlSerializer.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <param name="type">The type of the serialized object.</param>
        /// <returns>The object deserialized.</returns>
        public static object Deserialize(string item, Type type)
        {
            if (type == null || string.IsNullOrEmpty(item))
            {
                return null;
            }
            using (var stringReader = new StringReader(item))
            {
                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(stringReader);

            }
        }

        /// <summary>
        /// Deserialize an XML string into an object using the XmlSerializer.
        /// </summary>
        /// <param name="stream">The stream that must be deserialized.</param>
        /// <param name="type">The type of the serialized object.</param>
        /// <returns>The object deserialized.</returns>
        public static object Deserialize(Stream stream, Type type)
        {
            if (stream == null || type == null)
            {
                return null;
            }
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(stream);
        }
        
        /// <summary>
        /// Deserialize an XML string into an object using the XmlSerializer.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public static T Deserialize<T>(string item) where T : class
        {
            if (string.IsNullOrEmpty(item))
            {
                return null;
            }
            using (var stringReader = new StringReader(item))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stringReader) as T;

            }
        }

        /// <summary>
        /// Deserialize an XML string into an object using the XmlSerializer.
        /// </summary>
        /// <param name="stream">The stream that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public static T Deserialize<T>(Stream stream) where T : class
        {
            if (stream == null)
            {
                return null;
            }
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(stream) as T;
        }
    }
}
