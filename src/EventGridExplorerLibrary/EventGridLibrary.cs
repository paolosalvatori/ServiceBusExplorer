using System;
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
        private Dictionary<string, (Uri Endpoint, AzureKeyCredential Credential)> topicCredentials = new Dictionary<string, (Uri Endpoint, AzureKeyCredential Credential)>();
        private Dictionary<string, EventGridSenderClient> senderClients = new Dictionary<string, EventGridSenderClient>();
        private Dictionary<(string TopicName, string SubscriptionName), EventGridReceiverClient> receiverClients = new Dictionary<(string TopicName, string SubscriptionName), EventGridReceiverClient>();
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
            var pages = namespaceTopicCollection.GetAllAsync();
            var enumerator = pages.GetAsyncEnumerator();

            try
            {
                var endpoint = new Uri(hostname);
                topicCredentials.Clear();
                senderClients.Clear();
                receiverClients.Clear();

                while (await enumerator.MoveNextAsync())
                {
                    var topicName = enumerator.Current.Data.Name;
                    NamespaceTopicResource namespaceTopicResource = (await eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetNamespaceTopicAsync(topicName)).Value;
                    var key = (await namespaceTopicResource.GetSharedAccessKeysAsync()).Value;
                    var credential = new AzureKeyCredential(key.Key1);
                    topicCredentials[topicName] = (endpoint, credential);
                    senderClients[topicName] = new EventGridSenderClient(endpoint, topicName, credential);
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
            var pages = namespaceTopicEventSubscriptionCollection.GetAllAsync();

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
            await senderClients[topicName].SendAsync(cloudEvent);
        }

        public async Task PublishEventsAsync(string topicName, string eventSource, string eventType, List<string> publishEvents)
        {
            var cloudEvents = new CloudEvent[publishEvents.Count];

            for (var i = 0; i < cloudEvents.Length; i++)
            {
                var eventModel = publishEvents[i];
                cloudEvents[i] = new CloudEvent(eventSource, eventType, eventModel);
            }

            await senderClients[topicName].SendAsync(cloudEvents);
        }

        public async Task<ReceiveResult> ReceiveEventsAsync(string topicName, string subscriptionName, int maxEventNum)
        {
            try
            {
                var receiverClient = GetReceiverClient(topicName, subscriptionName);
                if (this.maxWaitTime < MinWaitTimeInSeconds)
                {
                    return (await receiverClient.ReceiveAsync(maxEvents: maxEventNum, maxWaitTime: TimeSpan.FromSeconds(MinWaitTimeInSeconds))).Value;
                }
                else if (this.maxWaitTime > MaxWaitTimeInSeconds)
                {
                    return (await receiverClient.ReceiveAsync(maxEvents: maxEventNum, maxWaitTime: TimeSpan.FromSeconds(MaxWaitTimeInSeconds))).Value;
                }
                else
                {
                    return (await receiverClient.ReceiveAsync(maxEvents: maxEventNum, maxWaitTime: TimeSpan.FromSeconds(this.maxWaitTime))).Value;
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
                var receiverClient = GetReceiverClient(topicName, subscriptionName);
                IReadOnlyList<string> succeededLockTokens = null;
                IReadOnlyList<FailedLockToken> failedLockTokens = null;

                switch (action)
                {
                    case "Acknowledge":
                        AcknowledgeResult acknowledgeResult = await receiverClient.AcknowledgeAsync(lockTokens);
                        succeededLockTokens = acknowledgeResult.SucceededLockTokens;
                        failedLockTokens = acknowledgeResult.FailedLockTokens;
                        break;
                    case "Release":
                        ReleaseResult releaseResult = await receiverClient.ReleaseAsync(lockTokens);
                        succeededLockTokens = releaseResult.SucceededLockTokens;
                        failedLockTokens = releaseResult.FailedLockTokens;
                        break;
                    case "Reject":
                        RejectResult rejectResult = await receiverClient.RejectAsync(lockTokens);
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

        private EventGridReceiverClient GetReceiverClient(string topicName, string subscriptionName)
        {
            var clientKey = (topicName, subscriptionName);
            if (!receiverClients.TryGetValue(clientKey, out var receiverClient))
            {
                var topicCredential = topicCredentials[topicName];
                receiverClient = new EventGridReceiverClient(topicCredential.Endpoint, topicName, subscriptionName, topicCredential.Credential);
                receiverClients[clientKey] = receiverClient;
            }

            return receiverClient;
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
