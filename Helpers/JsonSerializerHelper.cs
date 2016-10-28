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
using System.Web.Script.Serialization;  
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class JsonSerializerHelper
    {
        /// <summary>
        /// Serialize an object using the DataContractJsonSerializer.
        /// </summary>
        /// <param name="item">The object that must be serialized</param>
        /// <returns>A Json representation of the object passed as an argument.</returns>
        public static string Serialize(object item)
        {
            if (item == null)
            {
                throw new ArgumentException("The item argument cannot be null.");
            }

            var javaScriptSerializer = new JavaScriptSerializer();
            var json = javaScriptSerializer.Serialize(item);
            return json;
        }

        /// <summary>
        /// Deserialize a JSON string into an object using the JavaScriptSerializer.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <param name="type">The type of the serialized object.</param>
        /// <returns>The object deserialized.</returns>
        public static object Deserialize(string item, Type type)
        {
            if (item == null)
            {
                throw new ArgumentException("The item argument cannot be null.");
            }

            if (type == null)
            {
                throw new ArgumentException("The type argument cannot be null.");
            }

            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize(item, type);
        }

        /// <summary>
        /// Checks if the string is in JSON format.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <returns>Returns true if the object is in JSON format, false otherwise.</returns>
        public static bool IsJson(string item)
        {
            if (item == null)
            {
                throw new ArgumentException("The item argument cannot be null.");
            }
            try
            {
                var serializer = new JavaScriptSerializer();
                var obj = serializer.DeserializeObject(item);
                return obj != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deserialize an Json string into an object using the JavaScriptSerializer.
        /// </summary>
        /// <param name="stream">The stream that must be deserialized.</param>
        /// <param name="type">The type of the serialized object.</param>
        /// <returns>The object deserialized.</returns>
        public static object Deserialize(Stream stream, Type type)
        {
            if (stream == null)
            {
                throw new ArgumentException("The stream argument cannot be null.");
            }

            if (type == null)
            {
                throw new ArgumentException("The type argument cannot be null.");
            }           
                        
            string item;
            using (var reader = new StreamReader(stream))
            {
                item = reader.ReadToEnd();
            }
            return Deserialize(item, type);
        }
    }
}
