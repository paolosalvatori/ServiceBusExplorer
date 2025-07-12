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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace ServiceBusExplorer.Utilities.Helpers
{
    public static class JsonSerializerHelper
    {
        static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings { DateParseHandling = DateParseHandling.None };

        /// <summary>
        /// Indent the supplied json string
        /// </summary>
        /// <param name="json">Json to indent</param>
        /// <returns>Indented json</returns>
        public static string Indent(string json)
        {
            try
            {
                return JsonConvert
                    .SerializeObject(DeserializeObject<object>(json.Replace("$type", "%%$type%%"), serializerSettings), Formatting.Indented)
                    .Replace("%%$type%%", "$type");
            }
            catch (Exception)
            {
                return json;
            }
        }
        /// <summary>
        /// Serialize an object using the DataContractJsonSerializer.
        /// </summary>
        /// <param name="item">The object that must be serialized</param>
        /// <param name="formatting">Indicates out the output is formatted</param>
        /// <returns>A Json representation of the object passed as an argument.</returns>
        public static string Serialize(object item, Formatting formatting = default(Formatting))
        {
            if (item == null)
            {
                throw new ArgumentException("The item argument cannot be null.");
            }
            var json = JsonConvert.SerializeObject(item, formatting);
            return json;
        }

        /// <summary>
        /// Deserialize a JSON string into an object using the JavaScriptSerializer.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public static T Deserialize<T>(string item)
        {
            if (item == null)
            {
                throw new ArgumentException("The item argument cannot be null.");
            }
            return JsonConvert.DeserializeObject<T>(item);
        }

        /// <summary>
        /// Deserialize an Json string into an object using the JavaScriptSerializer.
        /// </summary>
        /// <param name="stream">The stream that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public static T Deserialize<T>(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentException("The stream argument cannot be null.");
            }

            string item;
            using (var reader = new StreamReader(stream))
            {
                item = reader.ReadToEnd();
            }
            return Deserialize<T>(item);
        }

        /// <remarks>
        /// Using this method instead of Json.Convert as Json.Convert can consume malformed JSON and display
        /// the fixed JSON. This method will allow SBE to catch an exception and then display the original
        /// text in the message.
        /// </remarks>
        /// <see href="https://github.com/paolosalvatori/ServiceBusExplorer/issues/425">
        /// Issue:25: Bad json (duplicate keys) are ignored silently
        /// </see>
        public static T DeserializeObject<T>(string json, JsonSerializerSettings settings = null)
        {
            var jsonSerializer = JsonSerializer.CreateDefault(settings);
            using (var stringReader = new StringReader(json))
            using (var jsonTextReader = new JsonTextReader(stringReader))
            {
                jsonTextReader.DateParseHandling = DateParseHandling.None;
                var loadSettings = new JsonLoadSettings
                {
                    DuplicatePropertyNameHandling = DuplicatePropertyNameHandling.Error
                };
                var jToken = JToken.ReadFrom(jsonTextReader, loadSettings);
                return jToken.ToObject<T>(jsonSerializer);
            }
        }
        /// <summary>
        /// Checks if the string is in JSON format.
        /// </summary>
        /// <param name="item">The string that must be deserialized.</param>
        /// <returns>Returns true if the object is in JSON format, false otherwise.</returns>
        public static bool IsJson(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return false;
            }
            try
            {
                var obj = JToken.Parse(item);
                return obj != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
