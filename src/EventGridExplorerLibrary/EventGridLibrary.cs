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
using Microsoft.Azure.Management.EventGridV2;

namespace EventGridExplorerLibrary
{
    public class EventGridLibrary
    {
        #region Private Constants
        private const string DefaultApiVersion = "2023-06-01-preview";
        private const string ExceptionFormat = "Exception: {0}";
        private const string TimeoutFormat = "Exception: Receiving events timed out after {0} ms";
        #endregion

        #region Private Fields
        private EventGridControlPlaneClient eventGridControlPlaneClient;
        private Dictionary<string, EventGridClient> dataPlaneClients = new Dictionary<string, EventGridClient>();
        private int retryTimeout;
        private readonly WriteToLogDelegate writeToLog = default;
        #endregion

        public EventGridLibrary(string subscriptionId, string apiVersion, int retryTimeout, string cloudTenant, string customId, WriteToLogDelegate writeToLog)
        {
            string tenantId = customId == string.Empty ? null : customId;
            var apiVersionToUse = string.IsNullOrEmpty(apiVersion) ? DefaultApiVersion : apiVersion;
            eventGridControlPlaneClient = new EventGridControlPlaneClient(subscriptionId, retryTimeout, tenantId);
            this.retryTimeout = retryTimeout;
            this.writeToLog = writeToLog;
        }

        public async Task<Response<EventGridNamespaceResource>> GetNamespacesAsync(string resourceGroupName, string namespaceName)
        {
            var eventGridNamespace = await eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetAsync();
            return eventGridNamespace;
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

        public async Task CreateTopicAsync(string resourceGroupName, string namespaceName, string newTopicName)
        {
            await eventGridControlPlaneClient.CreateNamespaceTopicAsync(resourceGroupName, namespaceName, newTopicName);
        }

        public async Task DeleteTopicAsync(string resourceGroupName, string namespaceName, string topicName)
        {
            await eventGridControlPlaneClient.DeleteNamespaceTopicAsync(resourceGroupName, namespaceName, topicName);
        }

        public async Task CreateSubscriptionAsync(string resourceGroupName, string namespaceName, string topicName, string subscriptionName, string deliveryMode)
        {
            await eventGridControlPlaneClient.CreateNamespaceTopicEventSubscriptionAsync(resourceGroupName, namespaceName, topicName, subscriptionName, deliveryMode);
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
