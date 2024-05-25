// <copyright file="EventGridClient.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace EventGridExplorerLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::Azure;
    using global::Azure.Core;
    using global::Azure.Identity;
    using global::Azure.Messaging.EventGrid.Namespaces;
    using global::Azure.ResourceManager;
    using global::Azure.ResourceManager.EventGrid;
    using global::Azure.ResourceManager.EventGrid.Models;

    /// <summary>
    /// Implementation of the event grid client interface.
    /// </summary>
    public class EventGridControlPlaneClient : IEventGridControlPlaneClient
    {
        private const string AadScope = "https://management.core.windows.net";
        private const string AuthorityHostUri = "https://login.microsoftonline.com/";
        private const string NamespaceTopicEventSubscriptionsApiVersion = "2023-12-15-preview";
        private const string NamespaceTopicEventSubscriptionsResourceType = "Microsoft.EventGrid/namespaces/topics/eventSubscriptions";
        private readonly string subscriptionId;
        private readonly string tenantId;
        public ArmClient armclient;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventGridClient"/> class.
        /// </summary>
        public EventGridControlPlaneClient(string subscriptionId, string tenantId)
        {
            this.subscriptionId = subscriptionId;
            this.tenantId = tenantId;
            this.armclient = new ArmClient(this.GetTokenCredential());

            // Setting this API version is needed to support name space topic event subscriptions with webhook as a destination.
            ArmClientOptions options = new ArmClientOptions();
            options.SetApiVersion(NamespaceTopicEventSubscriptionsResourceType, NamespaceTopicEventSubscriptionsApiVersion);
            
        }

        /// <inheritdoc/>
        public async Task<string> CreateNamespaceTopicAsync(string resourceGroupName, string namespaceName, string namespaceTopicName)
        {
            EventGridNamespaceResource namespaceResource = GetNamespaceResource(resourceGroupName, namespaceName);
            NamespaceTopicCollection collection = namespaceResource.GetNamespaceTopics();

            // check if exists
            if (await collection.ExistsAsync(namespaceTopicName))
            {
                return $"{namespaceTopicName} exists already";
            }

            NamespaceTopicData namespaceTopicData = new NamespaceTopicData
            {
                PublisherType = PublisherType.Custom,
                InputSchema = EventInputSchema.CloudEventSchemaV10,
            };

            ArmOperation<NamespaceTopicResource> azureOperation = await collection.CreateOrUpdateAsync(WaitUntil.Completed, namespaceTopicName, namespaceTopicData);
            NamespaceTopicResource result = azureOperation.Value;
            return result.Id;
        }

        /// <inheritdoc/>
        public async Task<string> DeleteNamespaceTopicAsync(string resourceGroupName, string namespaceName, string namespaceTopicName)
        {
            EventGridNamespaceResource namespaceResource = GetNamespaceResource(resourceGroupName, namespaceName);
            NamespaceTopicResource namespaceTopicResource = (await namespaceResource.GetNamespaceTopicAsync(namespaceTopicName)).Value;
            NamespaceTopicCollection collection = namespaceResource.GetNamespaceTopics();

            // check if exists
            if (!(await collection.ExistsAsync(namespaceTopicName)).Value)
            {
                return $"{namespaceTopicName} does not exist";
            }

            ArmOperation azureOperation = await namespaceTopicResource.DeleteAsync(WaitUntil.Completed);
            return string.Empty;
        }

        /// <inheritdoc/>
        public async Task<string> DeleteNamespaceTopicEventSubscriptionAsync(string resourceGroupName, string namespaceName, string namespaceTopicName, string subscriptionName)
        {
            EventGridNamespaceResource namespaceResource = GetNamespaceResource(resourceGroupName, namespaceName);
            NamespaceTopicResource namespaceTopicResource = (await namespaceResource.GetNamespaceTopicAsync(namespaceTopicName)).Value;
            NamespaceTopicEventSubscriptionResource namespaceTopicEventSubscriptionResource = (await namespaceTopicResource.GetNamespaceTopicEventSubscriptionAsync(subscriptionName)).Value;
            NamespaceTopicEventSubscriptionCollection collection = namespaceTopicResource.GetNamespaceTopicEventSubscriptions();

            // check if exists
            if (!(await collection.ExistsAsync(subscriptionName)).Value)
            {
                return $"{subscriptionName} for topic {namespaceTopicName} does not exist";
            }

            ArmOperation azureOperation = await namespaceTopicEventSubscriptionResource.DeleteAsync(WaitUntil.Completed);
            return string.Empty;
        }

        /// <inheritdoc/>
        public async Task<string> CreateNamespaceTopicEventSubscriptionAsync(string resourceGroupName, string namespaceName, string namespaceTopicName, string subscriptionName, string deliveryMode, List<Dictionary<string,string>> filters, List<string> eventTypes)
        {
            EventGridNamespaceResource namespaceResource = GetNamespaceResource(resourceGroupName, namespaceName);
            NamespaceTopicResource namespaceTopicResource = (await namespaceResource.GetNamespaceTopicAsync(namespaceTopicName)).Value;
            NamespaceTopicEventSubscriptionCollection collection = namespaceTopicResource.GetNamespaceTopicEventSubscriptions();
            NamespaceTopicEventSubscriptionData namespaceTopicEventSubscriptionData = new NamespaceTopicEventSubscriptionData();

             FiltersConfiguration filtersConfiguration = GetFiltersConfiguration(filters, eventTypes);
            if (filtersConfiguration.IncludedEventTypes.Count > 0 || filtersConfiguration.Filters.Count > 0)
            {
                namespaceTopicEventSubscriptionData.DeliveryConfiguration = new DeliveryConfiguration
                {
                    DeliveryMode = deliveryMode
                };
                namespaceTopicEventSubscriptionData.FiltersConfiguration = filtersConfiguration;
                namespaceTopicEventSubscriptionData.EventDeliverySchema = new DeliverySchema("CloudEventSchemaV1_0");
            }
            else
            {
                namespaceTopicEventSubscriptionData.DeliveryConfiguration = new DeliveryConfiguration
                {
                    DeliveryMode = deliveryMode
                };
                namespaceTopicEventSubscriptionData.EventDeliverySchema = new DeliverySchema("CloudEventSchemaV1_0");
            }

            // check if exists
            if ((await collection.ExistsAsync(subscriptionName)).Value)
            {
                return $"{subscriptionName} for topic {namespaceTopicName} already exists";
            }

            ArmOperation<NamespaceTopicEventSubscriptionResource> azureOperation = collection.CreateOrUpdate(WaitUntil.Completed, subscriptionName, namespaceTopicEventSubscriptionData);
            return azureOperation.Value.Id;
        }

        /// <inheritdoc/>
        public EventGridNamespaceResource GetNamespaceResource(string resourceGroupName, string namespaceName)
        {
            var namespaceArmId = CreateArmId(this.subscriptionId, resourceGroupName) + namespaceName;
            ArmClient client = GetArmClient();
            ResourceIdentifier namespaceIdentifier = new ResourceIdentifier(namespaceArmId);
            EventGridNamespaceResource namespaceResource = client.GetEventGridNamespaceResource(namespaceIdentifier);
            return namespaceResource;
        }

        private ArmClient GetArmClient()
        {
            // TODO: handle recreation of client if token expires
            if (armclient == null)
            {
                armclient = new ArmClient(GetTokenCredential());
            }

            return armclient;
        }

        private InteractiveBrowserCredential GetTokenCredential()
        {
            string[] scope = { AadScope + "/.default" };

            var credentialOption = new InteractiveBrowserCredentialOptions()
            {
                TenantId = tenantId,
                AuthorityHost = new Uri(AuthorityHostUri)
            };

            InteractiveBrowserCredential credential = new InteractiveBrowserCredential(credentialOption);
            return credential;
        }

        private string CreateArmId(string subscriptionId, string resourceGroupName)
        {
            return $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.EventGrid/namespaces/";
        }

        private FiltersConfiguration GetFiltersConfiguration(List<Dictionary<string, string>> filters, List<string> eventTypes)
        {
            FiltersConfiguration filtersConfiguration = new FiltersConfiguration();
            EventGridFilterValues eventGridFilterValues = new EventGridFilterValues();

            foreach (Dictionary<string, string> i in filters)
            {
                var operatorType = i["Operator"].ToString();
                var value = i["Value"].ToString();
                var key = i["Key"].ToString();
                if (operatorType.Equals("Boolean equals")) { var filter = new BoolEqualsFilter(); filter.Key = key; eventGridFilterValues.GetValueForBoolEqualsFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("String is in")) { var filter = new StringInFilter(); filter.Key = key; eventGridFilterValues.GetValueForStringInFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("String is not in")) { var filter = new StringNotInFilter(); filter.Key = key; eventGridFilterValues.GetValueForStringNotInFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("String contains")) { var filter = new StringContainsFilter(); filter.Key = key; eventGridFilterValues.GetValueForStringContainsFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("String does not contain")) { var filter = new StringNotContainsFilter(); filter.Key = key; eventGridFilterValues.GetValueForStringNotContainsFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("String begins with")) { var filter = new StringBeginsWithFilter(); filter.Key = key; eventGridFilterValues.GetValueForStringBeginsWithFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("String does not begin with")) { var filter = new StringNotBeginsWithFilter(); filter.Key = key; eventGridFilterValues.GetValueForStringNotBeginsWithFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("String ends with")) { var filter = new StringEndsWithFilter(); filter.Key = key; eventGridFilterValues.GetValueForStringEndsWithFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("String does not end with")) { var filter = new StringNotEndsWithFilter(); filter.Key = key; eventGridFilterValues.GetValueForStringNotEndsWithFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Number is less than")) { var filter = new NumberLessThanFilter(); filter.Key = key; eventGridFilterValues.GetValueForNumberLessThanFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Number is greater than")) { var filter = new NumberGreaterThanFilter(); filter.Key = key; eventGridFilterValues.GetValueForNumberGreaterThanFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Number is less than or equal to")) { var filter = new NumberLessThanOrEqualsFilter(); filter.Key = key; eventGridFilterValues.GetValueForNumberLessThanOrEqualsFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Number is greater than or equal to")) { var filter = new NumberGreaterThanOrEqualsFilter(); filter.Key = key; eventGridFilterValues.GetValueForNumberGreaterThanOrEqualsFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Number is in")) { var filter = new NumberInFilter(); filter.Key = key; eventGridFilterValues.GetValueForNumberInFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Number is not in")) { var filter = new NumberNotInFilter(); filter.Key = key; eventGridFilterValues.GetValueForNumberNotInFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Number is in range")) { var filter = new NumberInRangeFilter(); filter.Key = key; eventGridFilterValues.GetValueForNumberInRangeFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Number is not in range")) { var filter = new NumberNotInRangeFilter(); filter.Key = key; eventGridFilterValues.GetValueForNumberNotInRangeFilter(filter, value); filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Is null or undefined")) { var filter = new IsNullOrUndefinedFilter(); filter.Key = key; filtersConfiguration.Filters.Add(filter); };
                if (operatorType.Equals("Is not null")) { var filter = new IsNotNullFilter(); filter.Key = key; filtersConfiguration.Filters.Add(filter);};
            }

            if (eventTypes.Count > 0)
            {
                foreach (string eventType in eventTypes)
                {
                    filtersConfiguration.IncludedEventTypes.Add(eventType);
                }
            }
    
            return filtersConfiguration;
        }
    }
}
