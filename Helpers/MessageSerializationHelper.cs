#region Copyright
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
#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class MessageSerializationHelper
    {
        #region Private Static Fields
        private static readonly Dictionary<string, Dictionary<string, PropertyInfo>> propertyCache = new Dictionary<string, Dictionary<string, PropertyInfo>>();
        #endregion

        #region Public Static Methods

        public static string Serialize(IEnumerable<object> entities, IEnumerable<string> bodies, bool doNotSerializeBody = false)
        {
            var entityEnumerable = entities as object[] ?? entities.ToArray();
            var bodyEnumerable = bodies as string[] ?? bodies.ToArray();
            if (entityEnumerable.Length == 0 || bodyEnumerable.Length == 0 ||
                entityEnumerable.Length != bodyEnumerable.Length)
            {
                return null;
            }
            var type = entityEnumerable[0].GetType();
            GetProperties(type);
            if (!propertyCache.ContainsKey(type.FullName))
            {
                return null;
            }
            var propertyDictionary = propertyCache[type.FullName];
            var entityList = new List<SortedDictionary<string, object>>(entityEnumerable.Length); 
            for (var i = 0; i < entityEnumerable.Length; i++)
            {
                var entityDictionary = new SortedDictionary<string, object>();
                if (!doNotSerializeBody && JsonSerializerHelper.IsJson(bodyEnumerable[i]))
                {
                    try
                    {
                        entityDictionary.Add("body", JObject.Parse(bodyEnumerable[i]));
                    }
                    catch (Exception)
                    {
                        try
                        {
                            entityDictionary.Add("body", JArray.Parse(bodyEnumerable[i]));
                        }
                        catch (Exception)
                        {
                            entityDictionary.Add("body", bodyEnumerable[i]);
                        }
                    }
                }
                else
                {
                    entityDictionary.Add("body", bodyEnumerable[i]);
                }
                foreach (var keyValuePair in propertyDictionary)
                {
                    var camelCase = string.Format("{0}{1}",
                                                  keyValuePair.Key.Substring(0, 1).ToLower(CultureInfo.InvariantCulture),
                                                  keyValuePair.Key.Substring(1, keyValuePair.Key.Length - 1));
                    entityDictionary.Add(camelCase, null);
                    try
                    {
                        var value = keyValuePair.Value.GetValue(entityEnumerable[i], null);
                        entityDictionary[camelCase] = value;
                    }
                    // ReSharper disable once EmptyGeneralCatchClause
                    catch (Exception)
                    {
                    }
                }
                entityList.Add(entityDictionary);
            }
            return JsonSerializerHelper.Serialize(entityList.ToArray(), Formatting.Indented);
        }

        public static string Serialize(object entity, string body, bool doNotSerializeBody = false)
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
                    entityDictionary[camelCase] = value;
                }
                    // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
            }
            return JsonSerializerHelper.Serialize(entityDictionary, Formatting.Indented);
        }
        #endregion

        #region Private Static Methods
        private static void GetProperties(Type type)
        {
            if (type == null)
            {
                return;
            }
            var fullName = type.FullName;
            if (string.IsNullOrWhiteSpace(fullName) ||
                propertyCache.ContainsKey(fullName))
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
        #endregion
    }
}
