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
        public static string XmlSerialize(object item)
        {
            if (item != null)
            {

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
            return null;
        }

        /// <summary>
        /// Deserialize an XML string into an object using the XmlSerializer.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <param name="type">The type of the serialized object.</param>
        /// <returns>The object deserialized.</returns>
        public static object XmlDeserialize(string item, Type type)
        {
            if (type != null && item != null && item.Length > 0)
            {
                using (var stringReader = new StringReader(item))
                {
                    var serializer = new XmlSerializer(type);
                    return serializer.Deserialize(stringReader);

                }
            }
            return null;
        }

        /// <summary>
        /// Deserialize an XML string into an object using the XmlSerializer.
        /// </summary>
        /// <param name="stream">The stream that must be deserialized.</param>
        /// <param name="type">The type of the serialized object.</param>
        /// <returns>The object deserialized.</returns>
        public static object XmlDeserialize(Stream stream, Type type)
        {
            if (stream != null && type != null)
            {
                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(stream);
            }
            return null;
        }
    }
}
