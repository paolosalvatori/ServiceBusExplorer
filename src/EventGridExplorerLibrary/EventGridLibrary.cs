// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Globalization;
using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using Microsoft.Azure.Management.EventGrid;
using Microsoft.Azure.Management.EventGrid.Models;
using ServiceBusExplorer.Utilities.Helpers;

namespace EventGridExplorerLibrary
{
    public class EventGridLibrary
    {
        #region Private Constants
        private const string ArmEndpointUrl = "https://management.azure.com/";
        private const string ScopeUrl = "https://management.core.windows.net/.default";
        private const string AuthorityHostUri = "https://login.microsoftonline.com/";
        private readonly Dictionary<string, string> tenantIds = new Dictionary<string, string>
        {
            { "Public", "72f988bf-86f1-41af-91ab-2d7cd011db47" },
            { "Fairfax", "cab8a31a-1906-4287-a0d8-4eef66b95f6e" },
            { "Mooncake", "a55a4d5b-9241-49b1-b4ff-befa8db00269" }
        };
        private const string ExceptionFormat = "Exception: {0}";
        private const string TimeoutFormat = "Exception: Receiving events timed out after {0} ms";
        #endregion

        #region Private Fields
        private EventGridManagementClient controlPlaneClient;
        private Dictionary<string, EventGridClient> dataPlaneClients = new Dictionary<string, EventGridClient>();
        private int retryTimeout;
        private string tenantId;
        private readonly WriteToLogDelegate writeToLog = default;
        #endregion

        public EventGridLibrary(string subscriptionId, string apiVersion, int retryTimeout, string cloudTenant, string customId, WriteToLogDelegate writeToLog)
        {
            controlPlaneClient = new EventGridManagementClient(new Uri(ArmEndpointUrl), GetTokenCredential(), subscriptionId, apiVersion, retryTimeout);
            this.retryTimeout = retryTimeout;
            this.tenantId = customId == string.Empty ? tenantIds[cloudTenant] : customId;
            this.writeToLog = writeToLog;
        }

        public TokenCredentials GetTokenCredential()
        {
            string[] scope = { ScopeUrl };

            var credentialOption = new InteractiveBrowserCredentialOptions()
            {
                TenantId = tenantId,
                AuthorityHost = new Uri(AuthorityHostUri)
            };

            InteractiveBrowserCredential credential = new InteractiveBrowserCredential(credentialOption);
            TokenRequestContext tokenRequestContext = new TokenRequestContext(scope);
            var accessToken = credential.GetToken(tokenRequestContext);

            return new TokenCredentials(accessToken.Token);
        }

        public async Task<NamespaceModel> GetNamespacesAsync(string resourceGroupName, string namespaceName)
        {
            var eventGridNamespace = await controlPlaneClient.Namespaces.GetAsync(resourceGroupName, namespaceName);
            return eventGridNamespace;
        }

        public async Task<IPage<NamespaceTopic>> GetTopicsAsync(string resourceGroupName, string namespaceName, string hostname)
        {
            var topics = await controlPlaneClient.NamespaceTopics.ListByNamespaceAsync(resourceGroupName, namespaceName);

            foreach (var topic in topics)
            {
                var key = await controlPlaneClient.NamespaceTopics.ListSharedAccessKeysAsync(resourceGroupName, namespaceName, topic.Name);
                dataPlaneClients[topic.Name] = new EventGridClient(new Uri(hostname), new AzureKeyCredential(key.Key1));
            }

            return topics;
        }

        public async Task<IPage<Subscription>> GetEventSubscriptionsAsync(string resourceGroupName, string namespaceName, string topicName)
        {
            var subscriptions = await controlPlaneClient.NamespaceTopicEventSubscriptions.ListByNamespaceTopicAsync(resourceGroupName, namespaceName, topicName);
            return subscriptions;
        }

        public async Task CreateTopicAsync(string resourceGroupName, string namespaceName, string newTopicName)
        {
            NamespaceTopic namespaceTopic = new NamespaceTopic(name: newTopicName);
            await controlPlaneClient.NamespaceTopics.CreateOrUpdateAsync(resourceGroupName, namespaceName, newTopicName, namespaceTopic);
        }

        public async Task DeleteTopicAsync(string resourceGroupName, string namespaceName, string topicName)
        {
            await controlPlaneClient.NamespaceTopics.DeleteAsync(resourceGroupName, namespaceName, topicName);
        }

        public async Task CreateSubscriptionAsync(string resourceGroupName, string namespaceName, string topicName, string subscriptionName, string deliveryMode)
        {
            DeliveryConfiguration deliveryConfiguration = new DeliveryConfiguration(deliveryMode);
            Subscription subscription = new Subscription(name: subscriptionName, deliveryConfiguration: deliveryConfiguration);
            await controlPlaneClient.NamespaceTopicEventSubscriptions.CreateOrUpdateAsync(resourceGroupName, namespaceName, topicName, subscriptionName, subscription);
        }

        public async Task DeleteSubscriptionAsync(string resourceGroupName, string namespaceName, string topicName, string subscriptionName)
        {
            await controlPlaneClient.NamespaceTopicEventSubscriptions.DeleteAsync(resourceGroupName, namespaceName, topicName, subscriptionName);
        }

        public async Task PublishEventAsync(string topicName, string eventSource, string eventType, string eventDataJson)
        {
            var eventData = JsonSerializer.Deserialize<object>(eventDataJson);
            var cloudEvent = new CloudEvent(eventSource, eventType, eventData);
            await dataPlaneClients[topicName].PublishCloudEventAsync(topicName, cloudEvent);
        }

        public async Task PublishEventsAsync(string topicName, string eventSource, string eventType, List<string> publishEvents)
        {
            CloudEvent[] cloudEvents = new CloudEvent[publishEvents.Count];

            for (int i = 0; i < cloudEvents.Length; i++)
            {
                string eventModel = publishEvents[i];
                cloudEvents[i] = new CloudEvent(eventSource, eventType, eventModel);
            }

            await dataPlaneClients[topicName].PublishCloudEventsAsync(topicName, cloudEvents);
        }

        public async Task<ReceiveResult> ReceiveEventsAsync(string topicName, string subscriptionName, int maxEventNum)
        {
            try
            {
                Task<Response<ReceiveResult>> receiveTask = dataPlaneClients[topicName].ReceiveCloudEventsAsync(topicName, subscriptionName, maxEvents: maxEventNum);
                if (await Task.WhenAny(receiveTask, Task.Delay(retryTimeout)) == receiveTask)
                {
                    return receiveTask.Result;
                }
                else
                {
                    writeToLog(string.Format(CultureInfo.CurrentCulture, TimeoutFormat, retryTimeout));
                    return null;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }

        public async Task<bool> EventActionsAsync(string action, List<string> lockTokens, string topicName, string subscriptionName, WriteToLogDelegate writeToLog)
        {
            if (lockTokens.Count > 0)
            {
                IReadOnlyList<string> succeededLockTokens = null;
                IReadOnlyList<FailedLockToken> failedLockTokens = null;

                switch (action)
                {
                    case "Acknowledge":
                        AcknowledgeResult acknowledgeResult = await dataPlaneClients[topicName].AcknowledgeCloudEventsAsync(topicName, subscriptionName, lockTokens);
                        succeededLockTokens = acknowledgeResult.SucceededLockTokens;
                        failedLockTokens = acknowledgeResult.FailedLockTokens;
                        break;
                    case "Release":
                        ReleaseResult releaseResult = await dataPlaneClients[topicName].ReleaseCloudEventsAsync(topicName, subscriptionName, lockTokens);
                        succeededLockTokens = releaseResult.SucceededLockTokens;
                        failedLockTokens = releaseResult.FailedLockTokens;
                        break;
                    case "Reject":
                        RejectResult rejectResult = await dataPlaneClients[topicName].RejectCloudEventsAsync(topicName, subscriptionName, lockTokens);
                        succeededLockTokens = rejectResult.SucceededLockTokens;
                        failedLockTokens = rejectResult.FailedLockTokens;
                        break;
                }

                writeToLog($"Event Action: {action}");

                if (succeededLockTokens.Count > 0)
                {
                    writeToLog($"Success Count: {succeededLockTokens.Count}");
                    foreach (string lockToken in succeededLockTokens)
                    {
                        writeToLog($"Lock Token: {lockToken}");
                    }
                }

                if (failedLockTokens.Count > 0)
                {
                    writeToLog($"Failed Count: {failedLockTokens.Count}");
                    foreach (FailedLockToken lockToken in failedLockTokens)
                    {
                        writeToLog($"Lock Token: {lockToken.LockToken}");
                        writeToLog($"Error Code: {lockToken.ErrorCode}");
                        writeToLog($"Error Description: {lockToken.ErrorDescription}");
                    }
                }

                return failedLockTokens.Count == 0;
            }

            return false;
        }

        private void HandleException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message))
            {
                return;
            }

            writeToLog(string.Format(CultureInfo.CurrentCulture, ExceptionFormat, ex.Message));
        }
    }
}
