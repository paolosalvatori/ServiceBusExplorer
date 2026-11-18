using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
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
        private const string ExceptionFormat = "Exception: {0}";
        // Minimum wait time value for receive operation in Seconds
        private const int MinWaitTimeInSeconds = 10;
        // Maximum wait time value for receive operation in Seconds
        private const int MaxWaitTimeInSeconds = 120;
        #endregion

        #region Private Fields
        private readonly EventGridControlPlaneClient eventGridControlPlaneClient;
        private volatile NamespaceClients clients = new NamespaceClients(null);
        private readonly TimeSpan receiveWait;
        private readonly WriteToLogDelegate writeToLog;
        #endregion

        public EventGridLibrary(string subscriptionId, string apiVersion, int maxWaitTime, string customId, WriteToLogDelegate writeToLog)
            : this(new EventGridControlPlaneClient(subscriptionId, string.IsNullOrEmpty(customId) ? null : customId), maxWaitTime, writeToLog)
        {
        }

        internal EventGridLibrary(EventGridControlPlaneClient controlPlaneClient, int maxWaitTime, WriteToLogDelegate writeToLog)
        {
            eventGridControlPlaneClient = controlPlaneClient;
            receiveWait = TimeSpan.FromSeconds(Math.Min(Math.Max(maxWaitTime, MinWaitTimeInSeconds), MaxWaitTimeInSeconds));
            this.writeToLog = writeToLog;
        }

        public async Task<Response<EventGridNamespaceResource>> GetNamespacesAsync(string resourceGroupName, string namespaceName)
        {
            return await eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetAsync();
        }

        public async Task<IReadOnlyList<NamespaceTopicResource>> GetTopicsAsync(string resourceGroupName, string namespaceName, NamespaceTopicEndpoint endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint), "The Event Grid namespace endpoint is not loaded. Refresh the namespace and try again.");
            }

            var next = new NamespaceClients(endpoint.Address);
            var topics = new List<NamespaceTopicResource>();
            var collection = eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetNamespaceTopics();
            var enumerator = collection.GetAllAsync().GetAsyncEnumerator();
            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    var topic = enumerator.Current;
                    var keys = (await topic.GetSharedAccessKeysAsync()).Value;
                    var credential = new AzureKeyCredential(keys.Key1);
                    var name = topic.Data.Name;
                    next.Topics[name] = (credential, CreateSenderClient(endpoint.Address, name, credential));
                    topics.Add(topic);
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
            }

            clients = next;
            return topics;
        }

        public async Task<AsyncPageable<NamespaceTopicEventSubscriptionResource>> GetEventSubscriptionsAsync(string resourceGroupName, string namespaceName, string topicName)
        {
            var topic = (await eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetNamespaceTopicAsync(topicName)).Value;
            return GetEventSubscriptionsAsync(topic);
        }

        public AsyncPageable<NamespaceTopicEventSubscriptionResource> GetEventSubscriptionsAsync(NamespaceTopicResource topic)
        {
            return topic.GetNamespaceTopicEventSubscriptions().GetAllAsync();
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
            await clients.GetTopic(topicName).Sender.SendAsync(cloudEvent);
        }

        public async Task PublishEventsAsync(string topicName, string eventSource, string eventType, List<string> publishEvents)
        {
            var cloudEvents = new CloudEvent[publishEvents.Count];

            for (var i = 0; i < cloudEvents.Length; i++)
            {
                var eventModel = publishEvents[i];
                cloudEvents[i] = new CloudEvent(eventSource, eventType, eventModel);
            }

            await clients.GetTopic(topicName).Sender.SendAsync(cloudEvents);
        }

        public async Task<ReceiveResult> ReceiveEventsAsync(string topicName, string subscriptionName, int maxEventNum)
        {
            try
            {
                var receiverClient = GetReceiverClient(topicName, subscriptionName);
                return (await receiverClient.ReceiveAsync(maxEvents: maxEventNum, maxWaitTime: receiveWait)).Value;
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
                IReadOnlyList<string> succeededLockTokens;
                IReadOnlyList<FailedLockToken> failedLockTokens;

                switch (action)
                {
                    case "Acknowledge":
                        AcknowledgeResult acknowledgeResult = await GetReceiverClient(topicName, subscriptionName).AcknowledgeAsync(lockTokens);
                        succeededLockTokens = acknowledgeResult.SucceededLockTokens;
                        failedLockTokens = acknowledgeResult.FailedLockTokens;
                        break;
                    case "Release":
                        ReleaseResult releaseResult = await GetReceiverClient(topicName, subscriptionName).ReleaseAsync(lockTokens);
                        succeededLockTokens = releaseResult.SucceededLockTokens;
                        failedLockTokens = releaseResult.FailedLockTokens;
                        break;
                    case "Reject":
                        RejectResult rejectResult = await GetReceiverClient(topicName, subscriptionName).RejectAsync(lockTokens);
                        succeededLockTokens = rejectResult.SucceededLockTokens;
                        failedLockTokens = rejectResult.FailedLockTokens;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(action), action, "Use Acknowledge, Release, or Reject.");
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
            var current = clients;
            if (current.Endpoint == null)
            {
                throw new InvalidOperationException("The Event Grid namespace endpoint is not loaded. Refresh the namespace topics and try again.");
            }

            var topic = current.GetTopic(topicName);
            return current.Receivers.GetOrAdd((topicName, subscriptionName),
                key => CreateReceiverClient(current.Endpoint, key.Topic, key.Subscription, topic.Credential));
        }

        internal virtual EventGridSenderClient CreateSenderClient(Uri endpoint, string topicName, AzureKeyCredential credential)
        {
            return new EventGridSenderClient(endpoint, topicName, credential);
        }

        internal virtual EventGridReceiverClient CreateReceiverClient(Uri endpoint, string topicName, string subscriptionName, AzureKeyCredential credential)
        {
            return new EventGridReceiverClient(endpoint, topicName, subscriptionName, credential);
        }

        private sealed class NamespaceClients
        {
            public NamespaceClients(Uri endpoint)
            {
                Endpoint = endpoint;
            }

            public Uri Endpoint { get; }
            public Dictionary<string, (AzureKeyCredential Credential, EventGridSenderClient Sender)> Topics { get; } =
                new Dictionary<string, (AzureKeyCredential, EventGridSenderClient)>();
            public ConcurrentDictionary<(string Topic, string Subscription), EventGridReceiverClient> Receivers { get; } =
                new ConcurrentDictionary<(string, string), EventGridReceiverClient>();

            public (AzureKeyCredential Credential, EventGridSenderClient Sender) GetTopic(string name)
            {
                if (string.IsNullOrWhiteSpace(name) || !Topics.TryGetValue(name, out var topic))
                {
                    throw new InvalidOperationException($"The Event Grid topic '{name}' is not loaded. Refresh the namespace topics and try again.");
                }

                return topic;
            }
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
