﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Globalization;
using Azure;
using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;
using Azure.ResourceManager.EventGrid;
using ServiceBusExplorer.Utilities.Helpers;

namespace EventGridExplorerLibrary
{
    public class EventGridLibrary
    {
        #region Private Constants
        private const string DefaultApiVersion = "2023-06-01-preview";
        private const string ExceptionFormat = "Exception: {0}";
        // Minimum wait time value for receive operation in Seconds
        private const int MinWaitTimeInSeconds = 10;
        // Maximum wait time value for receive operation in Seconds
        private const int MaxWaitTimeInSeconds = 120;
        #endregion

        #region Private Fields
        private EventGridControlPlaneClient eventGridControlPlaneClient;
        private Dictionary<string, EventGridClient> dataPlaneClients = new Dictionary<string, EventGridClient>();
        private int maxWaitTime;
        private readonly WriteToLogDelegate writeToLog = default;
        #endregion

        public EventGridLibrary(string subscriptionId, string apiVersion, int maxWaitTime, string customId, WriteToLogDelegate writeToLog)
        {
            string tenantId = customId == string.Empty ? null : customId;
            var apiVersionToUse = string.IsNullOrEmpty(apiVersion) ? DefaultApiVersion : apiVersion;
            eventGridControlPlaneClient = new EventGridControlPlaneClient(subscriptionId, tenantId);
            this.maxWaitTime = maxWaitTime;
            this.writeToLog = writeToLog;
        }

        public async Task<Response<EventGridNamespaceResource>> GetNamespacesAsync(string resourceGroupName, string namespaceName)
        {
            return await eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetAsync();
        }


        public async Task<AsyncPageable<NamespaceTopicResource>> GetTopicsAsync(string resourceGroupName, string namespaceName, string hostname)
        {
            NamespaceTopicCollection namespaceTopicCollection = eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetNamespaceTopics();
            AsyncPageable<NamespaceTopicResource> pages = namespaceTopicCollection.GetAllAsync();
            IAsyncEnumerator<NamespaceTopicResource> enumerator = pages.GetAsyncEnumerator();

            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    NamespaceTopicResource namespaceTopicResource = (await eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetNamespaceTopicAsync(enumerator.Current.Data.Name)).Value;
                    var key = (await namespaceTopicResource.GetSharedAccessKeysAsync()).Value;
                    dataPlaneClients[enumerator.Current.Data.Name] = new EventGridClient(new Uri(hostname), new AzureKeyCredential(key.Key1));
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
            }

           return pages;
        }

        public async Task<AsyncPageable<NamespaceTopicEventSubscriptionResource>> GetEventSubscriptionsAsync(string resourceGroupName, string namespaceName, string topicName)
        {
            NamespaceTopicResource namespaceTopicResource = (await eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetNamespaceTopicAsync(topicName)).Value;
            NamespaceTopicEventSubscriptionCollection namespaceTopicEventSubscriptionCollection = namespaceTopicResource.GetNamespaceTopicEventSubscriptions();
            AsyncPageable<NamespaceTopicEventSubscriptionResource> pages = namespaceTopicEventSubscriptionCollection.GetAllAsync();

            return pages;
        }

        public async Task CreateTopicAsync(string resourceGroupName, string namespaceName, string topicName)
        {
            await eventGridControlPlaneClient.CreateNamespaceTopicAsync(resourceGroupName, namespaceName, topicName);
        }

        public async Task DeleteTopicAsync(string resourceGroupName, string namespaceName, string topicName)
        {
            await eventGridControlPlaneClient.DeleteNamespaceTopicAsync(resourceGroupName, namespaceName, topicName);
        }

        public async Task CreateSubscriptionAsync(string resourceGroupName, string namespaceName, string topicName, string subscriptionName, string deliveryMode, List<Dictionary<string,string>> filters, List<string> eventTypes)
        {
            await eventGridControlPlaneClient.CreateNamespaceTopicEventSubscriptionAsync(resourceGroupName, namespaceName, topicName, subscriptionName, deliveryMode, filters, eventTypes);
        }

        public async Task DeleteSubscriptionAsync(string resourceGroupName, string namespaceName, string topicName, string subscriptionName)
        {
            await eventGridControlPlaneClient.DeleteNamespaceTopicEventSubscriptionAsync(resourceGroupName, namespaceName, topicName, subscriptionName);
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
                if (this.maxWaitTime < MinWaitTimeInSeconds)
                {
                     return await dataPlaneClients[topicName].ReceiveCloudEventsAsync(topicName, subscriptionName, maxEvents: maxEventNum, maxWaitTime: TimeSpan.FromSeconds(MinWaitTimeInSeconds));
                }
                else if (this.maxWaitTime > MaxWaitTimeInSeconds)
                {
                    return await dataPlaneClients[topicName].ReceiveCloudEventsAsync(topicName, subscriptionName, maxEvents: maxEventNum, maxWaitTime: TimeSpan.FromSeconds(MaxWaitTimeInSeconds));
                }
                else
                {
                    return await dataPlaneClients[topicName].ReceiveCloudEventsAsync(topicName, subscriptionName, maxEvents: maxEventNum, maxWaitTime: TimeSpan.FromSeconds(this.maxWaitTime));
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
                        AcknowledgeResult acknowledgeResult = await dataPlaneClients[topicName].AcknowledgeCloudEventsAsync(topicName, subscriptionName, new AcknowledgeOptions(lockTokens));
                        succeededLockTokens = acknowledgeResult.SucceededLockTokens;
                        failedLockTokens = acknowledgeResult.FailedLockTokens;
                        break;
                    case "Release":
                        ReleaseResult releaseResult = await dataPlaneClients[topicName].ReleaseCloudEventsAsync(topicName, subscriptionName, new ReleaseOptions(lockTokens));
                        succeededLockTokens = releaseResult.SucceededLockTokens;
                        failedLockTokens = releaseResult.FailedLockTokens;
                        break;
                    case "Reject":
                        RejectResult rejectResult = await dataPlaneClients[topicName].RejectCloudEventsAsync(topicName, subscriptionName, new RejectOptions(lockTokens));
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
                        writeToLog($"Error Code: {lockToken.Error.Code}");
                        writeToLog($"Error Description: {lockToken.Error.Message}");
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
