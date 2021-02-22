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

#region References

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Azure.NotificationHubs;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Helpers
{
    /// <summary>
    ///  This class is used to serialize and deserialize XML requests.
    /// </summary>
    public class ImportExportHelper
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string Namespace = "http://schemas.microsoft.com/servicebusexplorer";
        private const string QueueDescriptionClass = "QueueDescription";
        private const string TopicDescriptionClass = "TopicDescription";
        private const string RelayDescriptionClass = "RelayDescription";
        private const string NotificationHubDescriptionClass = "NotificationHubDescription";
        private const string SubscriptionDescriptionClass = "SubscriptionDescription";
        private const string RuleDescriptionClass = "RuleDescription";
        private const string EventHubDescriptionClass = "EventHubDescription";
        private const string ConsumerGroupDescriptionClass = "ConsumerGroupDescription";
        private const string PartitionDescriptionClass = "PartitionDescription";
        private const string QueueEntity = "Queue";
        private const string TopicEntity = "Topic";
        private const string RelayEntity = "Relay";
        private const string NotificationHubEntity = "NotificationHub";
        private const string SubscriptionEntity = "Subscription";
        private const string RuleEntity = "Rule";
        private const string FilterEntity = "Filter";
        private const string ActionEntity = "Action";
        private const string EventHubEntity = "EventHub";
        private const string ConsumerGroupEntity = "ConsumerGroup";
        private const string PartitionEntity = "Partition";
        private const string QueueEntityList = "Queues";
        private const string TopicEntityList = "Topics";
        private const string RelayEntityList = "Relays";
        private const string SubscriptionEntityList = "Subscriptions";
        private const string AuthorizationRuleList = "AuthorizationRules";
        private const string RuleEntityList = "Rules";
        private const string SqlFilterEntity = "SqlFilter";
        private const string CorrelationFilterEntity = "CorrelationFilter";
        private const string SqlRuleActionEntity = "SqlRuleAction";
        private const string EntityList = "Entities";
        private const string NotificationHubEntityList = "NotificationHubs";
        private const string EventHubEntityList = "EventHubs";
        private const string ConsumerGroupEntityList = "ConsumerGroups";
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
        private const string EventHubPath = "EventHubPath";
        private const string SqlExpression = "SqlExpression";
        private const string CorrelationId = "CorrelationId";
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
        private const string DefaultConsumerGroupName = "$Default";
        private const string RelayType = "RelayType";
        #endregion

        #region Private Static Fields
        readonly Dictionary<string, Dictionary<string, PropertyInfo>> propertyCache = new Dictionary<string, Dictionary<string, PropertyInfo>>();
        readonly WriteToLogDelegate writeToLogDelegate;
        #endregion

        #region Constructor
        public ImportExportHelper(WriteToLogDelegate writeToLogDelegate)
        {
            this.writeToLogDelegate = writeToLogDelegate;
            GetProperties(typeof(QueueDescription), true, true);
            GetProperties(typeof(TopicDescription), true, true);
            GetProperties(typeof(RelayDescription), true, true);
            GetProperties(typeof(EventHubDescription), true, true);
            GetProperties(typeof(ConsumerGroupDescription), true, true);
            GetProperties(typeof(PartitionDescription), true, true);
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

        #region Public  Methods

        /// <summary>
        /// Serializes a list of entities.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="entityList">The list of entities to serialize.</param>
        /// <returns>An XML string.</returns>
        public async Task<string> ReadAndSerialize(ServiceBusHelper serviceBusHelper, List<IExtensibleDataObject> entityList)
        {
            if (entityList == null || entityList.Count <= 0)
            {
                return null;
            }
            
            using (var memoryStream = new MemoryStream())
            {
                using (var stringWriter = new StreamWriter(memoryStream, Encoding.ASCII))
                {
                    var xmlWriterSettings = new System.Xml.XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    using (var xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
                    {
                        var queueList = entityList.Where(e => e is QueueDescription).Cast<QueueDescription>();
                        var topicList = entityList.Where(e => e is TopicDescription).Cast<TopicDescription>();
                        var relayList = entityList.Where(e => e is RelayDescription).Cast<RelayDescription>();
                        var eventHubList = entityList.Where(e => e is EventHubDescription).Cast<EventHubDescription>();
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
                                    await SerializeEntity(serviceBusHelper, xmlWriter, queue);
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
                                    await SerializeEntity(serviceBusHelper, xmlWriter, topic);
                                }
                                catch (Exception ex)
                                {
                                    HandleException(ex);
                                }
                            }
                            xmlWriter.WriteEndElement();
                        }
                        var relayDescriptions = relayList as RelayDescription[] ?? relayList.ToArray();
                        if (relayDescriptions.Any())
                        {
                            xmlWriter.WriteStartElement(RelayEntityList);
                            foreach (var relay in relayDescriptions)
                            {
                                try
                                {
                                    await SerializeEntity(serviceBusHelper, xmlWriter, relay);
                                }
                                catch (Exception ex)
                                {
                                    HandleException(ex);
                                }
                            }
                            xmlWriter.WriteEndElement();
                        }
                        var eventHubDescriptions = eventHubList as EventHubDescription[] ?? eventHubList.ToArray();
                        if (eventHubDescriptions.Any())
                        {
                            xmlWriter.WriteStartElement(EventHubEntityList);
                            foreach (var eventHub in eventHubDescriptions)
                            {
                                try
                                {
                                    await SerializeEntity(serviceBusHelper, xmlWriter, eventHub);
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
                                    await SerializeEntity(serviceBusHelper, xmlWriter, notificationHub);
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

        /// <summary>
        /// Deserialize an XML string into an object instance.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="xml">The string that must be deserialized.</param>
        /// <returns>The object deserialized.</returns>
        public void DeserializeAndCreate(ServiceBusHelper serviceBusHelper, string xml)
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
                    CreateRelays(serviceBusHelper, root.Descendants(string.Format(NodeNameFormat, Namespace, RelayEntity)));
                    CreateEventHubs(serviceBusHelper, root.Descendants(string.Format(NodeNameFormat, Namespace, EventHubEntity)));
                    CreateNotificationHubs(serviceBusHelper, root.Descendants(string.Format(NodeNameFormat, Namespace, NotificationHubEntity)));
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the collection of properties.
        /// </summary>
        /// <param name="type">The type of the object to get the properties for.</param>
        /// <param name="canRead">A value indicating whether the property can be read.</param>
        /// <param name="canWrite">A value indicating whether the property can be written.</param>
        /// <returns><see cref="PropertyDescriptorCollection"/> of bindable properties.</returns>
        void GetProperties(Type type, bool canRead, bool canWrite)
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
                Where(p => p.Name == RelayType || (p.CanRead == canRead && p.CanWrite == canWrite && p.Name != ExtensionData && p.PropertyType != typeof(DateTime))).
                ToDictionary(p => p.Name);
            propertyCache[fullName] = propertyDictionary;
        }

        /// <summary>
        /// Reads a property value from an XML document and saves it to a propertyValue dictionary.
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
                if (bool.TryParse(xmlReader.ReadElementContentAsString().ToLower(), out var ok))
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
            if (property.PropertyType == typeof(RelayType))
            {
                propertyValue[name] = Enum.Parse(typeof(RelayType), xmlReader.ReadElementContentAsString(), true);
                return;
            }
            if (property.PropertyType == typeof(TimeSpan))
            {
                if (TimeSpan.TryParse(xmlReader.ReadElementContentAsString(), out var timeSpan))
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
        /// Sets the value of the properties of an object passed as a parameter.
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
        /// Maps a class to the corresponding name of the XML node.
        /// </summary>
        /// <param name="type">A Type object.</param>
        /// <returns>The name of the XML node.</returns>
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
                case RelayDescriptionClass:
                    return RelayEntity;
                case SubscriptionDescriptionClass:
                    return SubscriptionEntity;
                case RuleDescriptionClass:
                    return RuleEntity;
                case EventHubDescriptionClass:
                    return EventHubEntity;
                case ConsumerGroupDescriptionClass:
                    return ConsumerGroupEntity;
                case PartitionDescriptionClass:
                    return PartitionEntity;
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
        /// <returns>An XML string.</returns>
        async Task SerializeEntity(ServiceBusHelper serviceBusHelper, XmlWriter xmlWriter, object entity)
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
                    await SerializeEntity(serviceBusHelper, xmlWriter, value);
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
                        await SerializeEntity(serviceBusHelper, xmlWriter, rule);
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
                        await SerializeEntity(serviceBusHelper, xmlWriter, rule);
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
                        await SerializeEntity(serviceBusHelper, xmlWriter, subscription);
                    }
                    xmlWriter.WriteEndElement();
                }
            }
            if (entity is RelayDescription)
            {
                var relay = entity as RelayDescription;
                if (relay.Authorization.Any())
                {
                    xmlWriter.WriteStartElement(AuthorizationRuleList);
                    foreach (var rule in relay.Authorization)
                    {
                        await SerializeEntity(serviceBusHelper, xmlWriter, rule);
                    }
                    xmlWriter.WriteEndElement();
                }
            }
            if (entity is EventHubDescription)
            {
                var eventHub = entity as EventHubDescription;
                if (eventHub.Authorization.Any())
                {
                    xmlWriter.WriteStartElement(AuthorizationRuleList);
                    foreach (var rule in eventHub.Authorization)
                    {
                        await SerializeEntity(serviceBusHelper, xmlWriter, rule);
                    }
                    xmlWriter.WriteEndElement();
                }
                var consumerGroupList = serviceBusHelper.GetConsumerGroups(eventHub.Path);
                var consumerGroupDescriptions = consumerGroupList as ConsumerGroupDescription[] ?? consumerGroupList.ToArray();
                if (consumerGroupDescriptions.Any())
                {
                    xmlWriter.WriteStartElement(ConsumerGroupEntityList);
                    foreach (var consumerGroup in consumerGroupDescriptions)
                    {
                        await SerializeEntity(serviceBusHelper, xmlWriter, consumerGroup);
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
                        await SerializeEntity(serviceBusHelper, xmlWriter, rule);
                    }
                    xmlWriter.WriteEndElement();
                }
            }
            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Creates the queues whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="queues">The IEnumerable<XElement/> collection containing the XML definition of the queues to create.</param>
        void CreateQueues(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> queues)
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
        /// Creates the topics whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="topics">The IEnumerable<XElement/> collection containing the XML definition of the topics to create.</param>
        void CreateTopics(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> topics)
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
        /// Creates the relays whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="relays">The IEnumerable<XElement/> collection containing the XML definition of the relays to create.</param>
        void CreateRelays(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> relays)
        {
            try
            {
                if (serviceBusHelper == null ||
                    relays == null)
                {
                    return;
                }
                var fullName = typeof(RelayDescription).FullName;
                if (string.IsNullOrWhiteSpace(fullName) ||
                    !propertyCache.ContainsKey(fullName))
                {
                    return;
                }
                var propertyDictionary = propertyCache[fullName];
                var rulesName = string.Format(NodeNameFormat, Namespace, AuthorizationRuleList);
                foreach (var relay in relays)
                {
                    try
                    {
                        var propertyValue = new Dictionary<string, object>();
                        var properties = relay.Elements();
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
                        var relayDescription = new RelayDescription(propertyValue[Path] as string, (RelayType)propertyValue[RelayType]);
                        SetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         relayDescription);
                        var rules = CreateAuthorizationRules(authorizationRules, serviceBusHelper.IsCloudNamespace);
                        if (rules != null &&
                            rules.Count > 0)
                        {
                            foreach (var authorizationRule in rules)
                            {
                                relayDescription.Authorization.Add(authorizationRule);
                            }
                        }
                        serviceBusHelper.CreateRelay(relayDescription);
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
        /// Creates the event hubs whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="eventHubs">The IEnumerable<XElement/> collection containing the XML definition of the event hubs to create.</param>
        void CreateEventHubs(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> eventHubs)
        {
            try
            {
                if (serviceBusHelper == null ||
                    eventHubs == null)
                {
                    return;
                }
                var fullName = typeof(EventHubDescription).FullName;
                if (string.IsNullOrWhiteSpace(fullName) ||
                    !propertyCache.ContainsKey(fullName))
                {
                    return;
                }
                var propertyDictionary = propertyCache[fullName];
                var consumerGroupName = string.Format(NodeNameFormat, Namespace, ConsumerGroupEntity);
                var consumerGroupsName = string.Format(NodeNameFormat, Namespace, ConsumerGroupEntityList);
                var rulesName = string.Format(NodeNameFormat, Namespace, AuthorizationRuleList);
                foreach (var eventHub in eventHubs)
                {
                    try
                    {
                        var propertyValue = new Dictionary<string, object>();
                        var properties = eventHub.Elements();
                        IEnumerable<XElement> authorizationRules = null;
                        IEnumerable<XElement> consumerGroups = null;
                        foreach (var property in properties)
                        {
                            if (property.Name == rulesName)
                            {
                                authorizationRules = property.Descendants();
                            }
                            else
                                if (property.Name == consumerGroupsName)
                                {
                                    consumerGroups = property.Descendants(consumerGroupName);
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
                        var eventHubDescription = new EventHubDescription(propertyValue[Path] as string);
                        SetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         eventHubDescription);
                        var rules = CreateAuthorizationRules(authorizationRules, serviceBusHelper.IsCloudNamespace);
                        if (rules != null &&
                            rules.Count > 0)
                        {
                            foreach (var authorizationRule in rules)
                            {
                                eventHubDescription.Authorization.Add(authorizationRule);
                            }
                        }
                        serviceBusHelper.CreateEventHub(eventHubDescription);
                        CreateConsumerGroups(serviceBusHelper, consumerGroups);
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
        /// Creates the consumer groups whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="consumerGroups">The IEnumerable<XElement/> collection containing the XML definition of the consumer groups to create.</param>
        void CreateConsumerGroups(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> consumerGroups)
        {
            try
            {
                if (serviceBusHelper == null ||
                    consumerGroups == null)
                {
                    return;
                }
                var fullName = typeof(ConsumerGroupDescription).FullName;
                if (string.IsNullOrWhiteSpace(fullName) ||
                    !propertyCache.ContainsKey(fullName))
                {
                    return;
                }
                var propertyDictionary = propertyCache[fullName];
                foreach (var consumerGroup in consumerGroups)
                {
                    var propertyValue = new Dictionary<string, object>();
                    var properties = consumerGroup.Elements();
                    foreach (var property in properties)
                    {
                        var xmlReader = property.CreateReader();
                        GetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         xmlReader);
                    }

                    if (propertyValue.ContainsKey(Name) &&
                        propertyValue.ContainsKey(EventHubPath))
                    {
                        var eventHubName = propertyValue[EventHubPath] as string;
                        var name = propertyValue[Name] as string;
                        if (string.Compare(name, DefaultConsumerGroupName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            continue;
                        }
                        var consumerGroupDescription = new ConsumerGroupDescription(eventHubName, name);
                        SetPropertyValue(propertyDictionary,
                                         propertyValue,
                                         consumerGroupDescription);
                        serviceBusHelper.CreateConsumerGroup(consumerGroupDescription);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Creates the subscriptions whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="topicDescription">A description of the topic to which to add the subscriptions.</param>
        /// <param name="subscriptions">The IEnumerable<XElement/> collection containing the XML definition of the subscriptions to create.</param>
        private void CreateSubscriptions(ServiceBusHelper serviceBusHelper, TopicDescription topicDescription, IEnumerable<XElement> subscriptions)
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
        /// Creates the authorization rules whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="rules">The IEnumerable<XElement/> collection containing the XML definition of the authorization rules to create.</param>
        /// <param name="isCloudNamespace">True if it's a cloud namespace, false otherwise.</param>
        private List<AuthorizationRule> CreateAuthorizationRules(IEnumerable<XElement> rules, bool isCloudNamespace)
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
        /// Creates the rules whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="rules">The IEnumerable<XElement/> collection containing the XML definition of the rules to create.</param>
        private List<RuleDescription> CreateRules(IEnumerable<XElement> rules)
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
        /// Creates the filter whose XML definition is contained in the element parameter.
        /// </summary>
        /// <param name="filter">The XElement containing the filter definition.</param>
        /// <returns>The filter.</returns>
        private Filter CreateFilter(XElement filter)
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
                ruleFilter = new CorrelationFilter(propertyValue[CorrelationId] as string);
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
        /// Creates the action whose XML definition is contained in the element parameter.
        /// </summary>
        /// <param name="action">The XElement containing the action definition.</param>
        /// <returns>The action.</returns>
        private RuleAction CreateAction(XElement action)
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
        /// Creates the ApnsCredential whose XML definition is contained in the element parameter.
        /// </summary>
        /// <param name="element">The XElement containing the ApnsCredential definition.</param>
        /// <returns>The ApnsCredential.</returns>
        private ApnsCredential CreateApnsCredential(XElement element)
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
        /// Creates the WnsCredential whose XML definition is contained in the element parameter.
        /// </summary>
        /// <param name="element">The XElement containing the WnsCredential definition.</param>
        /// <returns>The WnsCredential.</returns>
        private WnsCredential CreateWnsCredential(XElement element)
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
        /// Creates the notification hubs whose XML definition is contained in the collection passed as a parameter.
        /// </summary>
        /// <param name="serviceBusHelper">A ServiceBusHelper object.</param>
        /// <param name="notificationHubs">The IEnumerable<XElement/> collection containing the XML definition of the notification hubs to create.</param>
        private void CreateNotificationHubs(ServiceBusHelper serviceBusHelper, IEnumerable<XElement> notificationHubs)
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
        private void HandleException(Exception ex)
        {
            if (ex == null || string.IsNullOrWhiteSpace(ex.Message))
            {
                return;
            }
            writeToLogDelegate(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
            {
                writeToLogDelegate(string.Format(CultureInfo.CurrentCulture, InnerExceptionFormat, ex.InnerException.Message));
            }
            writeToLogDelegate(string.Format(CultureInfo.CurrentCulture, StackTraceFormat, ex.StackTrace));
        }
        #endregion
    }
}
