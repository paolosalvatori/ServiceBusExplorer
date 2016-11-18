#region Copyright
//=======================================================================================
// Microsoft Business Platform Division Customer Advisory Team  
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

#region References
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus.Notifications;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    /// <summary>
    ///  This class is used to serialize and deserialize XML request.
    /// </summary>
    public static class ImportExportHelper
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string Namespace = @"http://schemas.microsoft.com/servicebusexplorer";
        private const string QueueDescriptionClass = "QueueDescription";
        private const string TopicDescriptionClass = "TopicDescription";
        private const string NotificationHubDescriptionClass = "NotificationHubDescription";
        private const string SubscriptionDescriptionClass = "SubscriptionDescription";
        private const string RuleDescriptionClass = "RuleDescription";
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string NotificationHubEntity = "NotificationHub";
        private const string SubscriptionEntity = "Subscription";
        private const string RuleEntity = "Rule";
        private const string FilterEntity = "Filter";
        private const string ActionEntity = "Action";
        private const string QueueEntityList = "Queues";
        private const string TopicEntityList = "Topics";
        private const string SubscriptionEntityList = "Subscriptions";
        private const string AuthorizationRuleList = "AuthorizationRules";
        private const string RuleEntityList = "Rules";
        private const string SqlFilterEntity = "SqlFilter";
        private const string CorrelationFilterEntity = "CorrelationFilter";
        private const string SqlRuleActionEntity = "SqlRuleAction";
        private const string EntityList = "Entities";
        private const string NotificationHubEntityList = "NotificationHubs";
        private const string ApnsCredentialEntity = "ApnsCredential";
        private const string WnsCredentialEntity = "WnsCredential";
        private const string NamespaceAttribute = "serviceBusNamespace";
        private const string Unknown = "Unknown";
        private const string ExtensionData = "ExtensionData";
        private const string EntityFormat = "{0}";
        private const string LambdaExpressionFilterFqdn = "Microsoft.ServiceBus.Messaging.Filters.LambdaExpressionFilter";
        private const string LambdaExpressionRuleActionFqdn = "Microsoft.ServiceBus.Messaging.Filters.LambdaExpressionRuleAction";
        private const string EmptyRuleActionFqdn = "Microsoft.ServiceBus.Messaging.EmptyRuleAction";
        private const string ExceptionFormat = "Exception: {0}";
        private const string InnerExceptionFormat = "InnerException: {0}";
        private const string StackTraceFormat = "StackTrace: {0}";
        private const string NodeNameFormat = "{{{0}}}{1}";
        private const string Path = "Path";
        private const string Name = "Name";
        private const string TopicPath = "TopicPath";
        private const string SqlExpression = "SqlExpression";
        private const string Correlationid = "Correlationid";
        private const string QueueExported = "The queue {0} has been successfully exported.";
        private const string TopicExported = "The topic {0} has been successfully exported.";
        private const string SubscriptionExported = "The subscription {0} of the topic {1} has been successfully exported.";
        private const string RuleExported = "The rule {0} has been successfully exported.";
        private const string NotificationHubExported = "The notification hub {0} has been successfully exported.";
        private const string MaxSizeInMegabytes = "MaxSizeInMegabytes";
        private const string ForwardTo = "ForwardTo";
        private const string Rights = "Rights";
        private const string Right = "Right";
        private const string KeyName = "KeyName";
        private const string PrimaryKey = "PrimaryKey";
        private const string SecondaryKey = "SecondaryKey";
        private const string IssuerName = "IssuerName";
        private const string ClaimType = "ClaimType";
        private const string ClaimValue = "ClaimValue";

        #endregion

        #region Private Static Fields
        private static readonly Dictionary<string, Dictionary<string, PropertyInfo>> propertyCache = new Dictionary<string, Dictionary<string, PropertyInfo>>();
        #endregion

        #region Static Constructor
        /// <summary>
        /// This is the static constructor of the Utility class.
        /// </summary>
        static ImportExportHelper()
        {
            GetProperties(typeof(QueueDescription), true, true);
            GetProperties(typeof(TopicDescription), true, true);
            GetProperties(typeof(NotificationHubDescription), true, true);
            GetProperties(typeof(ApnsCredential), true, true);
            GetProperties(typeof(WnsCredential), true, true);
            GetProperties(typeof(SubscriptionDescription), true, true);
            GetProperties(typeof(RuleDescription), true, true);
            GetProperties(typeof(SqlFilter), true, true);
            GetProperties(typeof(CorrelationFilter), true, true);
            GetProperties(typeof(SqlRuleAction), true, true);
            GetProperties(typeof(Filter), true, true);
            GetProperties(typeof(RuleAction), true, true);
            GetProperties(typeof(AllowRule), true, true);
            GetProperties(typeof(SharedAccessAuthorizationRule), true, true);
            var fullName = typeof(Filter).FullName;
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                propertyCache[LambdaExpressionFilterFqdn] = propertyCache[fullName];
            }
            fullName = typeof(RuleAction).FullName;
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                propertyCache[LambdaExpressionRuleActionFqdn] = propertyCache[fullName];
                propertyCache[EmptyRuleActionFqdn] = propertyCache[fullName];
            }
        }
        #endregion

        #region Public Static Methods

        /// <summary>
        /// Serializes a list of entities.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="entityList">The list of entities to serialize.</param>
        /// <returns>A XML string.</returns>
        public static string ReadAndSerialize(ServiceBusHelper serviceBusHelper, List<EntityDescription> entityList)
        {
            if (entityList != null &&
                entityList.Count > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var stringWriter = new StreamWriter(memoryStream, Encoding.ASCII))
                    {
                        using (var xmlWriter = XmlWriter.Create(stringWriter))
                        {
                            var queueList = entityList.Where(e => e is QueueDescription).Cast<QueueDescription>();
                            var topicList = entityList.Where(e => e is TopicDescription).Cast<TopicDescription>();
                            var notificationHubList = entityList.Where(e => e is NotificationHubDescription).Cast<NotificationHubDescription>();
                            xmlWriter.WriteStartElement(EntityList, Namespace);
                            xmlWriter.WriteAttributeString(NamespaceAttribute, string.IsNullOrWhiteSpace(serviceBusHelper.Namespace) ?
                                                                               Unknown :
                                                                               serviceBusHelper.Namespace);
                            var queueDescriptions = queueList as QueueDescription[] ?? queueList.ToArray();
                            if (queueDescriptions.Any())
                            {
                                xmlWriter.WriteStartElement(QueueEntityList);
                                foreach (var queue in queueDescriptions)
                                {
                                    try
                                    {
                                        SerializeEntity(serviceBusHelper, xmlWriter, queue);
                                    }
                                    catch (Exception ex)
                                    {
                                        HandleException(ex);
                                    }
                                }
                                xmlWriter.WriteEndElement();
                            }
                            var topicDescriptions = topicList as TopicDescription[] ?? topicList.ToArray();
                            if (topicDescriptions.Any())
                            {
                                xmlWriter.WriteStartElement(TopicEntityList);
                                foreach (var topic in topicDescriptions)
                                {
                                    try
                                    {
                                        SerializeEntity(serviceBusHelper, xmlWriter, topic);
                                    }
                                    catch (Exception ex)
                                    {
                                        HandleException(ex);
                                    }
                                }
                                xmlWriter.WriteEndElement();
                            }
                            var notificationHubDescriptions = notificationHubList as NotificationHubDescription[] ?? notificationHubList.ToArray();
                            if (notificationHubDescriptions.Any())
                            {
                                xmlWriter.WriteStartElement(NotificationHubEntityList);
                                foreach (var notificationHub in notificationHubDescriptions)
                                {
                                    try
                                    {
                                        SerializeEntity(serviceBusHelper, xmlWriter, notificationHub);
                                    }
                                    catch (Exception ex)
                                    {
                                        HandleException(ex);
                                    }
                                }
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            return null;
        }

        /// <summary>
        /// Deserialize the xml string into an object instance.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="xml">The string that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public static void DeserializeAndCreate(ServiceBusHelper serviceBusHelper, string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return;
            }
            using (var stringReader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    var root = XElement.Load(xmlReader);
                    CreateQueues(serviceBusHelper, root.Descendants(string.Format(NodeNameFormat, Namespace, QueueEntity)));
                    CreateTopics(serviceBusHelper, root.Descendants(string.Format(NodeNameFormat, Namespace, TopicEntity)));
                    CreateNotificationHubs(serviceBusHelper, root.Descendants(string.Format(NodeNameFormat, Namespace, NotificationHubEntity)));
                }
            }
        }
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Gets the collection of properties.
        /// </summary>
        /// <param name="type">The type of the object to get the properties for.</param>
        /// <param name="canRead">Gets a value indicating whether the property can be read.</param>
        /// <param name="canWrite">TGets a value indicating whether the property can be written.</param>
        /// <returns><see cref="PropertyDescriptorCollection"/> of bindable properties.</returns>
        private static void GetProperties(Type type, bool canRead, bool canWrite)
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
            var propertyDictionary = propertyArray.
                Where(p => p.CanRead == canRead && p.CanWrite == canWrite && p.Name != ExtensionData).
                ToDictionary(p => p.Name);
            propertyCache[fullName] = propertyDictionary;
        }

        /// <summary>
        /// Reads a property value from an Xml document and saves it to the propertyValue dictionary.
        /// </summary>
        /// <param name="propertyDictionary">The dictionary containing the definition of properties.</param>
        /// <param name="propertyValue">The dictionary containing the value of properties.</param>
        /// <param name="xmlReader">The XmlReader object used to read the property value.</param>
        private static void GetPropertyValue(Dictionary<string, PropertyInfo> propertyDictionary,
                                             Dictionary<string, object> propertyValue,
                                             XmlReader xmlReader)
        {
            if (propertyDictionary == null ||
                propertyValue == null ||
                xmlReader == null)
            {
                return;
            }
            xmlReader.Read();
            if (!propertyDictionary.ContainsKey(xmlReader.Name))
            {
                return;
            }
            var property = propertyDictionary[xmlReader.Name];
            var name = xmlReader.Name;
            if (property == null)
            {
                return;
            }
            if (property.PropertyType == typeof(int))
            {
                propertyValue[name] = xmlReader.ReadElementContentAsInt();
                return;
            }
            if (property.PropertyType == typeof(long))
            {
                propertyValue[name] = xmlReader.ReadElementContentAsLong();
                return;
            }
            if (property.PropertyType == typeof(float))
            {
                propertyValue[name] = xmlReader.ReadElementContentAsFloat();
                return;
            }
            if (property.PropertyType == typeof(double))
            {
                propertyValue[name] = xmlReader.ReadElementContentAsDouble();
                return;
            }
            if (property.PropertyType == typeof(decimal))
            {
                propertyValue[name] = xmlReader.ReadElementContentAsDecimal();
                return;
            }
            if (property.PropertyType == typeof(bool))
            {
                bool ok;
                if (bool.TryParse(xmlReader.ReadElementContentAsString().ToLower(), out ok))
                {
                    propertyValue[name] = ok;
                }
                return;
            }
            if (property.PropertyType == typeof(DateTime))
            {
                propertyValue[name] = xmlReader.ReadElementContentAsDateTime();
                return;
            }
            if (property.PropertyType == typeof(TimeSpan))
            {
                TimeSpan timeSpan;
                if (TimeSpan.TryParse(xmlReader.ReadElementContentAsString(), out timeSpan))
                {
                    propertyValue[name] = timeSpan;
                }
                return;
            }
            if (property.PropertyType == typeof(string))
            {
                propertyValue[name] = xmlReader.ReadElementContentAsString();
            }
        }

        /// <summary>
        /// Sets the value of the properties of an object passes as a parameter.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="propertyDictionary">The dictionary containing the definition of properties.</param>
        /// <param name="propertyValue">The dictionary containing the value of properties.</param>
        /// <param name="item">The object whose properties must be assigned a value.</param>
        private static void SetPropertyValue<T>(Dictionary<string, PropertyInfo> propertyDictionary,
                                                Dictionary<string, object> propertyValue,
                                                T item) where T : class
        {
            foreach (var property in propertyValue)
            {
                propertyDictionary[property.Key].SetValue(item, property.Value, null);
            }
        }

        /// <summary>
        /// Maps a class to the corresponding name of the Xml node.
        /// </summary>
        /// <param name="type">A Type object.</param>
        /// <returns>The name of the Xml node.</returns>
        private static string MapClassToEntity(Type type)
        {
            if (type == null ||
                string.IsNullOrWhiteSpace(type.Name))
            {
                return Unknown;
            }
            switch (type.Name)
            {
                case QueueDescriptionClass:
                    return QueueEntity;
                case TopicDescriptionClass:
                    return TopicEntity;
                case SubscriptionDescriptionClass:
                    return SubscriptionEntity;
                case RuleDescriptionClass:
                    return RuleEntity;
                case NotificationHubDescriptionClass:
                    return NotificationHubEntity;
                default:
                    return type.Name;
            }
        }

        /// <summary>
        /// Serializes an entity using the XmlSerializer.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="xmlWriter">The XmlWriter object to use.</param>
        /// <param name="entity">The entity to serialize.</param>
        /// <returns>A XML string.</returns>
        private static void SerializeEntity(ServiceBusHelper serviceBusHelper, XmlWriter xmlWriter, object entity)
        {
            if (xmlWriter == null ||
                entity == null)
            {
                return;
            }
            var type = entity.GetType();
            var typeName = type.Name;
            if (!propertyCache.ContainsKey(type.FullName))
            {
                return;
            }
            var propertyDictionary = propertyCache[type.FullName];
            xmlWriter.WriteStartElement(MapClassToEntity(type));
            foreach (var keyValuePair in propertyDictionary)
            {
                var value = keyValuePair.Value.GetValue(entity, null);
                if (string.Compare(keyValuePair.Key,
                                   MaxSizeInMegabytes,
                                   StringComparison.InvariantCultureIgnoreCase) == 0 &&
                    !(new List<long> { 1024, 2048, 3072, 4096, 5120 }).Contains((long)value))
                {
                    value = 1024;
                }
                if (string.Compare(keyValuePair.Key,
                                   ForwardTo,
                                   StringComparison.InvariantCultureIgnoreCase) == 0 &&
                    !string.IsNullOrWhiteSpace(value as string))
                {
                    value = (new Uri(value as string)).AbsolutePath.Substring(1);
                }
                if (string.Compare(keyValuePair.Key,
                                   Rights,
                                   StringComparison.InvariantCultureIgnoreCase) == 0 &&
                    (entity is AllowRule || entity is SharedAccessAuthorizationRule))
                {
                    var rule = entity as AuthorizationRule;
                    xmlWriter.WriteStartElement(Rights);
                    foreach (var right in rule.Rights)
                    {
                        xmlWriter.WriteElementString(Right, string.Format("{0}", right));
                    }
                    xmlWriter.WriteEndElement();
                    continue;
                }
                xmlWriter.WriteStartElement(keyValuePair.Value.Name);
                if (value is Filter || value is RuleAction || value is ApnsCredential || value is WnsCredential)
                {
                    SerializeEntity(serviceBusHelper, xmlWriter, value);
                }
                else
                {
                    xmlWriter.WriteString(string.Format(EntityFormat, value));
                }
                xmlWriter.WriteEndElement();
            }
            if (entity is QueueDescription)
            {
                var queue = entity as QueueDescription;
                if (queue.Authorization.Any())
                {
                    xmlWriter.WriteStartElement(AuthorizationRuleList);
                    foreach (var rule in queue.Authorization)
                    {
                        SerializeEntity(serviceBusHelper, xmlWriter, rule);
                    }
                    xmlWriter.WriteEndElement();
                }
            }
            if (entity is TopicDescription)
            {
                var topic = entity as TopicDescription;
                if (topic.Authorization.Any())
                {
                    xmlWriter.WriteStartElement(AuthorizationRuleList);
                    foreach (var rule in topic.Authorization)
                    {
                        SerializeEntity(serviceBusHelper, xmlWriter, rule);
                    }
                    xmlWriter.WriteEndElement();
                }
                var subscriptionList = serviceBusHelper.GetSubscriptions(topic.Path);
                var subscriptionDescriptions = subscriptionList as SubscriptionDescription[] ?? subscriptionList.ToArray();
                if (subscriptionDescriptions.Any())
                {
                    xmlWriter.WriteStartElement(SubscriptionEntityList);
                    foreach (var subscription in subscriptionDescriptions)
                    {
                        SerializeEntity(serviceBusHelper, xmlWriter, subscription);
                    }
                    xmlWriter.WriteEndElement();
                }
            }
            if (entity is SubscriptionDescription)
            {
                var subscription = entity as SubscriptionDescription;
                var ruleList = serviceBusHelper.GetRules(subscription.TopicPath, subscription.Name);
                var ruleDescriptions = ruleList as RuleDescription[] ?? ruleList.ToArray();
                if (ruleDescriptions.Any())
                {
                    xmlWriter.WriteStartElement(RuleEntityList);
                    foreach (var rule in ruleDescriptions)
                    {
                        SerializeEntity(serviceBusHelper, xmlWriter, rule);
                    }
                    xmlWriter.WriteEndElement();
                }
            }
            xmlWriter.WriteEndElement();
            switch (typeName)
            {
                case QueueDescriptionClass:
                    var queueDescription = entity as QueueDescription;
                    if (queueDescription != null)
                    {
                        MainForm.StaticWriteToLog(string.Format(QueueExported, queueDescription.Path));
                    }
                    break;
                case TopicDescriptionClass:
                    var topicDescription = entity as TopicDescription;
                    if (topicDescription != null)
                    {
                        MainForm.StaticWriteToLog(string.Format(TopicExported, topicDescription.Path));
                    }
                    break;
                case SubscriptionDescriptionClass:
                    var subscriptionDescription = entity as SubscriptionDescription;
                    if (subscriptionDescription != null)
                    {
                        MainForm.StaticWriteToLog(string.Format(SubscriptionExported,
                                                      subscriptionDescription.Name,
                                                      subscriptionDescription.TopicPath));
                    }
                    break;
                case RuleDescriptionClass:
                    var ruleDescription = entity as RuleDescription;
                    if (ruleDescription != null)
                    {
                        MainForm.StaticWriteToLog(string.Format(RuleExported,
                                                      ruleDescription.Name));
                    }
                    break;
                case NotificationHubDescriptionClass:
                    var notificationHubDescription = entity as NotificationHubDescription;
                    if (notificationHubDescription != null)
                    {
                        MainForm.StaticWriteToLog(string.Format(NotificationHubExported, notificationHubDescription.Path));
                    }
                    break;
            }
        }

        /// <summary>
        /// Creates the queues which xml definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="queues">The IEnumerable<XElement/> collection containing the xml definition of the queues to create.</param>
        private static void CreateQueues(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> queues)
        {
            try
            {
                if (serviceBusHelper == null ||
                    queues == null)
                {
                    return;
                }
                var fullName = typeof(QueueDescription).FullName;
                if (string.IsNullOrWhiteSpace(fullName) ||
                    !propertyCache.ContainsKey(fullName))
                {
                    return;
                }
                var propertyDictionary = propertyCache[fullName];
                var rulesName = string.Format(NodeNameFormat, Namespace, AuthorizationRuleList);
                foreach (var queue in queues)
                {
                    try
                    {
                        var propertyValue = new Dictionary<string, object>();
                        var properties = queue.Elements();
                        IEnumerable<XElement> authorizationRules = null;
                        foreach (var property in properties)
                        {
                            if (property.Name.ToString() == rulesName)
                            {
                                authorizationRules = property.Descendants();
                            }
                            else
                            {
                                var xmlReader = property.CreateReader();
                                GetPropertyValue(propertyDictionary,
                                                 propertyValue,
                                                 xmlReader);
                            }
                        }
                        if (!propertyValue.ContainsKey(Path))
                        {
                            continue;
                        }
                        var queueDescription = new QueueDescription(propertyValue[Path] as string);
                        SetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         queueDescription);
                        var rules = CreateAuthorizationRules(authorizationRules, serviceBusHelper.IsCloudNamespace);
                        if (rules != null &&
                            rules.Count > 0)
                        {
                            foreach (var authorizationRule in rules)
                            {
                                queueDescription.Authorization.Add(authorizationRule);
                            }
                        }
                        serviceBusHelper.CreateQueue(queueDescription);
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }

                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Creates the topics which xml definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="topics">The IEnumerable<XElement/> collection containing the xml definition of the topics to create.</param>
        private static void CreateTopics(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> topics)
        {
            try
            {
                if (serviceBusHelper == null ||
                    topics == null)
                {
                    return;
                }
                var fullName = typeof(TopicDescription).FullName;
                if (string.IsNullOrWhiteSpace(fullName) ||
                    !propertyCache.ContainsKey(fullName))
                {
                    return;
                }
                var propertyDictionary = propertyCache[fullName];
                var subscriptionName = string.Format(NodeNameFormat, Namespace, SubscriptionEntity);
                var subscriptionsName = string.Format(NodeNameFormat, Namespace, SubscriptionEntityList);
                var rulesName = string.Format(NodeNameFormat, Namespace, AuthorizationRuleList);
                foreach (var topic in topics)
                {
                    try
                    {
                        var propertyValue = new Dictionary<string, object>();
                        var properties = topic.Elements();
                        IEnumerable<XElement> authorizationRules = null;
                        IEnumerable<XElement> subscriptions = null;
                        foreach (var property in properties)
                        {
                            if (property.Name == rulesName)
                            {
                                authorizationRules = property.Descendants();
                            }
                            else
                            if (property.Name == subscriptionsName)
                            {
                                subscriptions = property.Descendants(subscriptionName);
                            }
                            else
                            {
                                var xmlReader = property.CreateReader();
                                GetPropertyValue(propertyDictionary,
                                                 propertyValue,
                                                 xmlReader);
                            }
                        }

                        if (!propertyValue.ContainsKey(Path))
                        {
                            continue;
                        }
                        var topicDescription = new TopicDescription(propertyValue[Path] as string);
                        SetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         topicDescription);
                        var rules = CreateAuthorizationRules(authorizationRules, serviceBusHelper.IsCloudNamespace);
                        if (rules != null &&
                            rules.Count > 0)
                        {
                            foreach (var authorizationRule in rules)
                            {
                                topicDescription.Authorization.Add(authorizationRule);
                            }
                        }
                        topicDescription = serviceBusHelper.CreateTopic(topicDescription);
                        CreateSubscriptions(serviceBusHelper, topicDescription, subscriptions);
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Creates the subscriptions which xml definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="topicDescription">A description of the topic to which to add the subscription.</param>
        /// <param name="subscriptions">The IEnumerable<XElement/> collection containing the xml definition of the subscriptions to create.</param>
        private static void CreateSubscriptions(ServiceBusHelper serviceBusHelper, TopicDescription topicDescription, IEnumerable<XElement> subscriptions)
        {
            try
            {
                if (serviceBusHelper == null ||
                    subscriptions == null)
                {
                    return;
                }
                var fullName = typeof(SubscriptionDescription).FullName;
                if (string.IsNullOrWhiteSpace(fullName) ||
                    !propertyCache.ContainsKey(fullName))
                {
                    return;
                }
                var propertyDictionary = propertyCache[fullName];
                var ruleName = string.Format(NodeNameFormat, Namespace, RuleEntity);
                var rulesName = string.Format(NodeNameFormat, Namespace, RuleEntityList);
                foreach (var subscription in subscriptions)
                {
                    var propertyValue = new Dictionary<string, object>();
                    var properties = subscription.Elements();
                    IEnumerable<XElement> rules = null;
                    foreach (var property in properties)
                    {
                        if (property.Name == rulesName)
                        {
                            rules = property.Descendants(ruleName);
                        }
                        else
                        {
                            var xmlReader = property.CreateReader();
                            GetPropertyValue(propertyDictionary,
                                             propertyValue,
                                             xmlReader);
                        }
                    }

                    if (propertyValue.ContainsKey(Name) &&
                        propertyValue.ContainsKey(TopicPath))
                    {
                        RuleDescription defaultRuleDescription = null;
                        IEnumerable<RuleDescription> nonDefaultRuleDescriptions = null;
                        var ruleDescriptions = CreateRules(rules);
                        if (ruleDescriptions != null)
                        {
                            defaultRuleDescription =
                                ruleDescriptions.FirstOrDefault(r => r.Name == RuleDescription.DefaultRuleName);
                            nonDefaultRuleDescriptions = ruleDescriptions.Where(r => r.Name != RuleDescription.DefaultRuleName);
                        }
                        var subscriptionDescription = new SubscriptionDescription(propertyValue[TopicPath] as string,
                                                                                  propertyValue[Name] as string);
                        SetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         subscriptionDescription);
                        string createdRuleName = null;
                        if (defaultRuleDescription != null)
                        {
                            createdRuleName = defaultRuleDescription.Name;
                            serviceBusHelper.CreateSubscription(topicDescription, subscriptionDescription, defaultRuleDescription);
                        }
                        else
                        {
                            if (ruleDescriptions != null && ruleDescriptions.Any())
                            {
                                var rule = ruleDescriptions.FirstOrDefault();
                                if (rule != null)
                                {
                                    createdRuleName = rule.Name;
                                    serviceBusHelper.CreateSubscription(topicDescription, subscriptionDescription, rule);
                                }
                            }
                            else
                            {
                                serviceBusHelper.CreateSubscription(topicDescription, subscriptionDescription);
                            }
                        }
                        if (nonDefaultRuleDescriptions != null)
                        {
                            foreach (var ruleDescription in nonDefaultRuleDescriptions)
                            {
                                if (string.Compare(ruleDescription.Name, 
                                                   createdRuleName, 
                                                   StringComparison.InvariantCultureIgnoreCase) != 0)
                                { 
                                    serviceBusHelper.AddRule(subscriptionDescription, ruleDescription); 
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Creates the authorization rules which xml definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="rules">The IEnumerable<XElement/> collection containing the xml definition of the authorization rules to create.</param>
        /// <param name="isCloudNamespace">True if it's cloud namespace, false otherwise.</param>
        private static List<AuthorizationRule> CreateAuthorizationRules(IEnumerable<XElement> rules, bool isCloudNamespace)
        {
            if (rules == null)
            {
                return null;
            }
            //var fullName = isCloudNamespace ?
            //               typeof(SharedAccessAuthorizationRule).FullName :
            //               typeof(AllowRule).FullName;

            var fullName = typeof(SharedAccessAuthorizationRule).FullName;

            if (string.IsNullOrWhiteSpace(fullName) ||
                !propertyCache.ContainsKey(fullName))
            {
                return null;
            }
            var propertyDictionary = propertyCache[fullName];
            var list = new List<AuthorizationRule>();
            var rightsName = string.Format(NodeNameFormat, Namespace, Rights);
            foreach (var rule in rules)
            {
                var propertyValue = new Dictionary<string, object>();
                var properties = rule.Elements();
                List<AccessRights> rights = null;
                foreach (var property in properties)
                {
                    if (property.Name.ToString() == rightsName)
                    {
                        rights = property.Elements().Select(e => (AccessRights)Enum.Parse(typeof(AccessRights), e.Value)).ToList();
                    }
                    else
                    {
                        var xmlReader = property.CreateReader();
                        GetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         xmlReader);
                    }
                }
                if (isCloudNamespace)
                {
                    if (propertyValue.Keys.Contains(KeyName) &&
                        propertyValue.Keys.Contains(PrimaryKey))
                    {
                        var keyName = propertyValue[KeyName] as string;
                        var primaryKey = propertyValue[PrimaryKey] as string;
                        if (!string.IsNullOrWhiteSpace(keyName) &&
                            !string.IsNullOrWhiteSpace(primaryKey) &&
                            rights != null &&
                            rights.Count > 0)
                        {
                            if (propertyValue.Keys.Contains(SecondaryKey) &&
                                !string.IsNullOrWhiteSpace(propertyValue[SecondaryKey] as string))
                            {
                                list.Add(new SharedAccessAuthorizationRule(keyName, 
                                                                           primaryKey,
                                                                           propertyValue[SecondaryKey] as string,
                                                                           rights));
                            }
                            else
                            {
                                list.Add(new SharedAccessAuthorizationRule(keyName, 
                                                                           primaryKey, 
                                                                           rights));
                                
                            }
                        }
                    }
                }
                else
                {
                    if (propertyValue.Keys.Contains(KeyName) &&
                        propertyValue.Keys.Contains(PrimaryKey))
                    {
                        var keyName = propertyValue[KeyName] as string;
                        var primaryKey = propertyValue[PrimaryKey] as string;
                        if (!string.IsNullOrWhiteSpace(keyName) &&
                            !string.IsNullOrWhiteSpace(primaryKey) &&
                            rights != null &&
                            rights.Count > 0)
                        {
                            if (propertyValue.Keys.Contains(SecondaryKey) &&
                                !string.IsNullOrWhiteSpace(propertyValue[SecondaryKey] as string))
                            {
                                list.Add(new SharedAccessAuthorizationRule(keyName,
                                                                           primaryKey,
                                                                           propertyValue[SecondaryKey] as string,
                                                                           rights));
                            }
                            else
                            {
                                list.Add(new SharedAccessAuthorizationRule(keyName,
                                                                           primaryKey,
                                                                           rights));

                            }
                        }
                    }
                    else if (propertyValue.Keys.Contains(IssuerName) &&
                        propertyValue.Keys.Contains(ClaimType) &&
                        propertyValue.Keys.Contains(ClaimValue))
                    {
                        var issuerName = propertyValue[IssuerName] as string;
                        var claimType = propertyValue[ClaimType] as string;
                        var claimValue = propertyValue[ClaimValue] as string;
                        if (!string.IsNullOrWhiteSpace(issuerName) &&
                            !string.IsNullOrWhiteSpace(claimType) &&
                            !string.IsNullOrWhiteSpace(claimValue) &&
                            rights != null &&
                            rights.Count > 0)
                        {
                            list.Add(new AllowRule(issuerName,
                                                   claimType,
                                                   claimValue,
                                                   rights));
                        }
                    }
                }

            }
            return list;
        }

        /// <summary>
        /// Creates the rules which xml definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="rules">The IEnumerable<XElement/> collection containing the xml definition of the rules to create.</param>
        private static List<RuleDescription> CreateRules(IEnumerable<XElement> rules)
        {
            if (rules == null)
            {
                return null;
            }
            var fullName = typeof(RuleDescription).FullName;
            if (string.IsNullOrWhiteSpace(fullName) ||
                !propertyCache.ContainsKey(fullName))
            {
                return null;
            }
            var propertyDictionary = propertyCache[fullName];
            var list = new List<RuleDescription>();
            var filterName = string.Format(NodeNameFormat, Namespace, FilterEntity);
            var actionName = string.Format(NodeNameFormat, Namespace, ActionEntity);
            foreach (var rule in rules)
            {
                Filter filter = null;
                RuleAction action = null;
                var propertyValue = new Dictionary<string, object>();
                var properties = rule.Elements();
                foreach (var property in properties)
                {
                    if (property.Name == filterName)
                    {
                        filter = CreateFilter(property.Elements().FirstOrDefault());
                    }
                    else
                    {
                        if (property.Name == actionName)
                        {
                            action = CreateAction(property.Elements().FirstOrDefault());
                        }
                        else
                        {
                            var xmlReader = property.CreateReader();
                            GetPropertyValue(propertyDictionary,
                                             propertyValue,
                                             xmlReader);
                        }
                    }
                }
                var ruleDescription = new RuleDescription();
                if (filter != null)
                {
                    ruleDescription.Filter = filter;
                }
                if (action != null)
                {
                    ruleDescription.Action = action;
                }
                if (propertyValue.ContainsKey(Name))
                {
                    ruleDescription.Name = propertyValue[Name] as string;
                }
                list.Add(ruleDescription);
            }
            return list;
        }

        /// <summary>
        /// Creates the filter which xml definition is contained in the element parameter.
        /// </summary>
        /// <param name="filter">The XElement containing the filter definition.</param>
        /// <returns>The filter.</returns>
        private static Filter CreateFilter(XElement filter)
        {
            if (filter == null)
            {
                return null;
            }
            string fullName = null;
            if (filter.Name == string.Format(NodeNameFormat, Namespace, SqlFilterEntity))
            {
                fullName = typeof(SqlFilter).FullName;
            }
            if (filter.Name == string.Format(NodeNameFormat, Namespace, CorrelationFilterEntity))
            {
                fullName = typeof(CorrelationFilter).FullName;
            }
            if (string.IsNullOrWhiteSpace(fullName) ||
                !propertyCache.ContainsKey(fullName))
            {
                return null;
            }
            var propertyDictionary = propertyCache[fullName];
            var propertyValue = new Dictionary<string, object>();
            var properties = filter.Elements();
            foreach (var property in properties)
            {
                var xmlReader = property.CreateReader();
                GetPropertyValue(propertyDictionary,
                                 propertyValue,
                                 xmlReader);
            }
            Filter ruleFilter = null;
            if (filter.Name == string.Format(NodeNameFormat, Namespace, SqlFilterEntity) &&
                propertyValue.ContainsKey(SqlExpression))
            {
                ruleFilter = new SqlFilter(propertyValue[SqlExpression] as string);
            }
            if (filter.Name == string.Format(NodeNameFormat, Namespace, CorrelationFilterEntity))
            {
                ruleFilter = new CorrelationFilter(propertyValue[Correlationid] as string);
            }
            if (ruleFilter != null)
            {
                SetPropertyValue(propertyDictionary,
                                 propertyValue,
                                 ruleFilter);
            }
            return ruleFilter;
        }

        /// <summary>
        /// Creates the action which xml definition is contained in the element parameter.
        /// </summary>
        /// <param name="action">The XElement containing the action definition.</param>
        /// <returns>The action.</returns>
        private static RuleAction CreateAction(XElement action)
        {
            if (action == null)
            {
                return null;
            }
            string fullName = null;
            if (action.Name == string.Format(NodeNameFormat, Namespace, SqlRuleActionEntity))
            {
                fullName = typeof(SqlRuleAction).FullName;
            }
            if (string.IsNullOrWhiteSpace(fullName) ||
                !propertyCache.ContainsKey(fullName))
            {
                return null;
            }
            var propertyDictionary = propertyCache[fullName];
            var propertyValue = new Dictionary<string, object>();
            var properties = action.Elements();
            foreach (var property in properties)
            {
                var xmlReader = property.CreateReader();
                GetPropertyValue(propertyDictionary,
                                 propertyValue,
                                 xmlReader);
            }
            RuleAction ruleAction = null;
            if (action.Name == string.Format(NodeNameFormat, Namespace, SqlRuleActionEntity) &&
                propertyValue.ContainsKey(SqlExpression))
            {
                ruleAction = new SqlRuleAction(propertyValue[SqlExpression] as string);
            }
            if (ruleAction != null)
            {
                SetPropertyValue(propertyDictionary,
                                 propertyValue,
                                 ruleAction);
            }
            return ruleAction;
        }

        /// <summary>
        /// Creates the apnsCredential which xml definition is contained in the element parameter.
        /// </summary>
        /// <param name="element">The XElement containing the apnsCredential definition.</param>
        /// <returns>The apnsCredential.</returns>
        private static ApnsCredential CreateApnsCredential(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            string fullName = null;
            if (element.Name == string.Format(NodeNameFormat, Namespace, ApnsCredentialEntity))
            {
                fullName = typeof(ApnsCredential).FullName;
            }
            if (string.IsNullOrWhiteSpace(fullName) ||
                !propertyCache.ContainsKey(fullName))
            {
                return null;
            }
            var propertyDictionary = propertyCache[fullName];
            var propertyValue = new Dictionary<string, object>();
            var properties = element.Elements();
            foreach (var property in properties)
            {
                var xmlReader = property.CreateReader();
                GetPropertyValue(propertyDictionary,
                                 propertyValue,
                                 xmlReader);
            }
            ApnsCredential apnsCredential = null;
            if (element.Name == string.Format(NodeNameFormat, Namespace, ApnsCredentialEntity))
            {
                apnsCredential = new ApnsCredential();
            }
            if (apnsCredential != null)
            {
                SetPropertyValue(propertyDictionary,
                                 propertyValue,
                                 apnsCredential);
            }
            return apnsCredential;
        }

        /// <summary>
        /// Creates the wnsCredential which xml definition is contained in the element parameter.
        /// </summary>
        /// <param name="element">The XElement containing the wnsCredential definition.</param>
        /// <returns>The wnsCredential.</returns>
        private static WnsCredential CreateWnsCredential(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            string fullName = null;
            if (element.Name == string.Format(NodeNameFormat, Namespace, WnsCredentialEntity))
            {
                fullName = typeof(WnsCredential).FullName;
            }
            if (string.IsNullOrWhiteSpace(fullName) ||
                !propertyCache.ContainsKey(fullName))
            {
                return null;
            }
            var propertyDictionary = propertyCache[fullName];
            var propertyValue = new Dictionary<string, object>();
            var properties = element.Elements();
            foreach (var property in properties)
            {
                var xmlReader = property.CreateReader();
                GetPropertyValue(propertyDictionary,
                                 propertyValue,
                                 xmlReader);
            }
            WnsCredential wnsCredential = null;
            if (element.Name == string.Format(NodeNameFormat, Namespace, WnsCredentialEntity))
            {
                wnsCredential = new WnsCredential();
            }
            if (wnsCredential != null)
            {
                SetPropertyValue(propertyDictionary,
                                 propertyValue,
                                 wnsCredential);
            }
            return wnsCredential;
        }

        /// <summary>
        /// Creates the notification hubs which xml definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="notificationHubs">The IEnumerable<XElement/> collection containing the xml definition of the notification hubs to create.</param>
        private static void CreateNotificationHubs(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> notificationHubs)
        {
            try
            {
                if (serviceBusHelper == null ||
                    notificationHubs == null)
                {
                    return;
                }
                var fullName = typeof(NotificationHubDescription).FullName;
                if (string.IsNullOrWhiteSpace(fullName) ||
                    !propertyCache.ContainsKey(fullName))
                {
                    return;
                }
                var propertyDictionary = propertyCache[fullName];
                var apnsCredentialName = string.Format(NodeNameFormat, Namespace, ApnsCredentialEntity);
                var wnsCredentialName = string.Format(NodeNameFormat, Namespace, WnsCredentialEntity);
                foreach (var notificationHub in notificationHubs)
                {
                    try
                    {
                        var propertyValue = new Dictionary<string, object>();
                        var properties = notificationHub.Elements();
                        foreach (var property in properties)
                        {
                            if (property.Name == apnsCredentialName)
                            {
                                var firstElement = property.Elements().ToArray().Length > 0
                                                       ? property.Elements().First()
                                                       : null;
                                if (firstElement != null)
                                {
                                    propertyValue[ApnsCredentialEntity] = CreateApnsCredential(firstElement);
                                }
                                continue;
                            }
                            if (property.Name == wnsCredentialName)
                            {
                                var firstElement = property.Elements().ToArray().Length > 0
                                                       ? property.Elements().First()
                                                       : null;
                                if (firstElement != null)
                                {
                                    propertyValue[WnsCredentialEntity] = CreateWnsCredential(firstElement);
                                }
                                continue;
                            }
                            var xmlReader = property.CreateReader();
                            GetPropertyValue(propertyDictionary,
                                             propertyValue,
                                             xmlReader);
                        }
                        if (!propertyValue.ContainsKey(Path))
                        {
                            continue;
                        }
                        var notificationHubDescription = new NotificationHubDescription(propertyValue[Path] as string);
                        SetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         notificationHubDescription);
                        serviceBusHelper.CreateNotificationHub(notificationHubDescription);
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Writes the specified message to the trace listener.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        private static void HandleException(Exception ex)
        {
            if (ex == null || string.IsNullOrWhiteSpace(ex.Message))
            {
                return;
            }
            MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
            MainForm.StaticWriteToLog(string.Format(CultureInfo.CurrentCulture, StackTraceFormat, ex.StackTrace));
        }
        #endregion
    }
}