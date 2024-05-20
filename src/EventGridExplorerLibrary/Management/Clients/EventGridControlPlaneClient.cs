// <copyright file="EventGridClient.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Azure.Management.EventGridV2
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

        private Dictionary<string, EventGridFilter> filterOperatorMap = new Dictionary<string, EventGridFilter> {
            {"Boolean equals", new BoolEqualsFilter()},
            {"String is in", new StringInFilter()},
            {"String is not in", new StringNotInFilter()},
            {"String contains", new StringContainsFilter()},
            {"String does not contain", new StringNotContainsFilter()},
            {"String begins with", new StringBeginsWithFilter()},
            {"String does not begin with", new StringNotBeginsWithFilter()},
            {"String ends with", new StringEndsWithFilter()},
            {"String does not end with", new StringNotEndsWithFilter()},
            {"Number is less than", new NumberLessThanFilter()},
            {"Number is greater than", new NumberGreaterThanFilter()},
            {"Number is less than or equal to", new NumberLessThanOrEqualsFilter()},
            {"Number is greater than or equal to", new NumberGreaterThanOrEqualsFilter()},
            {"Number is in", new NumberInFilter()},
            {"Number is not in", new NumberNotInFilter()},
            {"Number is in range", new NumberInRangeFilter()},
            {"Number is not in range", new NumberNotInRangeFilter()},
            {"Is null or undefined", new IsNullOrUndefinedFilter()},
            {"Is not null", new IsNotNullFilter()},
        };

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
            NamespaceTopicEventSubscriptionData namespaceTopicEventSubscriptionData = new NamespaceTopicEventSubscriptionData
            {
                DeliveryConfiguration = new DeliveryConfiguration
                {
                    DeliveryMode = deliveryMode
                },
                EventDeliverySchema = new DeliverySchema("CloudEventSchemaV1_0"),
                FiltersConfiguration = GetFiltersConfiguration(filters, eventTypes)

            };

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
            string[] scope = {  AadScope + "/.default" };

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

            foreach (Dictionary<string, string> i in filters)
            {
                EventGridFilter filter = filterOperatorMap[i["Operator"].ToString()];
                filter.Key = i["Key"].ToString();

                filtersConfiguration.Filters.Add(filter);
            }

            if (eventTypes.Count > 0)
            {
                foreach (string eventType in eventTypes)
                {
                    filtersConfiguration.IncludedEventTypes.Add(eventType);
                }
            }

            else
            {
                filtersConfiguration.IncludedEventTypes.Add("");
            }
    
            return filtersConfiguration;
        }
    }
}
