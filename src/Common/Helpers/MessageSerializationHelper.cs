//=======================================================================================
// Microsoft Business Platform Division Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using ServiceBusExplorer.Utilities.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServiceBusExplorer.Helpers
{
    public class MessageSerializationHelper
    {
        static readonly Dictionary<string, Dictionary<string, PropertyInfo>> propertyCache = new Dictionary<string, Dictionary<string, PropertyInfo>>();

        public static string Serialize(IEnumerable<object> entities, IEnumerable<string> bodies, bool doNotSerializeBody = false)
        {
            var entityArray = entities as object[] ?? entities.ToArray();
            var bodyArray = bodies as string[] ?? bodies.ToArray();
            if (entityArray.Length == 0 || bodyArray.Length == 0 || entityArray.Length != bodyArray.Length)
            {
                return null;
            }
            var type = entityArray[0].GetType();
            GetProperties(type);
            if (!propertyCache.ContainsKey(type.FullName))
            {
                return null;
            }

            var entityList = new List<SortedDictionary<string, object>>(entityArray.Length);

            for (var i = 0; i < entityArray.Length; i++)
            {
                var entityDictionary = PrepareMessageForSerialization(entityArray[i], bodyArray[i], doNotSerializeBody);

                entityList.Add(entityDictionary);
            }
            return JsonSerializerHelper.Serialize(entityList.ToArray(), Formatting.Indented);
        }

        public static string Serialize(object entity, string body, bool doNotSerializeBody = false)
        {
            var entityDictionary = PrepareMessageForSerialization(entity, body, doNotSerializeBody);

            return entityDictionary == null
                ? null
                : JsonSerializerHelper.Serialize(entityDictionary, Formatting.Indented);
        }

        /// <summary>
        /// Prepare a single message for serialization
        /// </summary>
        /// <param name="entity"><see cref="BrokeredMessageTemplate"/>></param>
        /// <param name="body">Message payload</param>
        /// <param name="doNotSerializeBody">Keep body message as a serialized string</param>
        /// <returns>null if cannot prepare the message or a serializable object</returns>
        static SortedDictionary<string, object> PrepareMessageForSerialization(object entity, string body, bool doNotSerializeBody)
        {
            if (entity == null)
            {
                return null;
            }

            var type = entity.GetType();
            GetProperties(type);
            if (!propertyCache.ContainsKey(type.FullName))
            {
                return null;
            }

            var propertyDictionary = propertyCache[type.FullName];
            var entityDictionary = new SortedDictionary<string, object>();
            if (!doNotSerializeBody && JsonSerializerHelper.IsJson(body))
            {
                try
                {
                    entityDictionary.Add("body", JObject.Parse(body));
                }
                catch (Exception)
                {
                    try
                    {
                        entityDictionary.Add("body", JArray.Parse(body));
                    }
                    catch (Exception)
                    {
                        entityDictionary.Add("body", body);
                    }
                }
            }
            else
            {
                entityDictionary.Add("body", body);
            }

            foreach (var keyValuePair in propertyDictionary)
            {
                var camelCase = string.Format("{0}{1}",
                    keyValuePair.Key.Substring(0, 1).ToLower(CultureInfo.InvariantCulture),
                    keyValuePair.Key.Substring(1, keyValuePair.Key.Length - 1));
                entityDictionary.Add(camelCase, null);
                try
                {
                    var value = keyValuePair.Value.GetValue(entity, null);

                    if (camelCase == "properties")
                    {
                        // TODO: do not hard-code everything to strings, discover the type and use it
                        entityDictionary[camelCase] = ((Dictionary<string, object>)value)
                            .Select(x =>
                            {
                                var typeName = x.Value.GetType().ToString().Replace("System.", "");

                                return new MessagePropertyInfo(x.Key, typeName, x.Value);
                            })
                            .ToArray();
                    }
                    else
                    {
                        entityDictionary[camelCase] = value;
                    }
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
            }

            return entityDictionary;
        }

        static void GetProperties(Type type)
        {
            if (type == null)
            {
                return;
            }
            var fullName = type.FullName;
            if (string.IsNullOrWhiteSpace(fullName) || propertyCache.ContainsKey(fullName))
            {
                return;
            }
            var propertyArray = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (propertyArray.Length <= 0)
            {
                return;
            }
            var propertyDictionary = propertyArray.Where(p => p.CanRead && p.Name != "ExtensionData").ToDictionary(p => p.Name);
            propertyCache[fullName] = propertyDictionary;
        }
    }
}
