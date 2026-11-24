using System;
using System.Threading;
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
        private const string DataPlaneRetryNotice =
            "Explorer did not issue an additional retry. The Azure SDK may retry transient failures.";
        // Minimum wait time value for receive operation in Seconds
        private const int MinWaitTimeInSeconds = 10;
        // Maximum wait time value for receive operation in Seconds
        private const int MaxWaitTimeInSeconds = 120;
        #endregion

        #region Private Fields
        private readonly EventGridControlPlaneClient eventGridControlPlaneClient;
        private readonly object topicRefreshLock = new object();
        private long topicRefreshVersion;
        private volatile NamespaceClients clients = new NamespaceClients(null);
        private readonly TimeSpan receiveWait;
        private readonly WriteToLogDelegate writeToLog;
        #endregion

        public EventGridLibrary(string subscriptionId, int maxWaitTime, string customId, WriteToLogDelegate writeToLog)
            : this(new EventGridControlPlaneClient(subscriptionId, string.IsNullOrEmpty(customId) ? null : customId), maxWaitTime, writeToLog)
        {
        }

        internal EventGridLibrary(EventGridControlPlaneClient controlPlaneClient, int maxWaitTime, WriteToLogDelegate writeToLog)
        {
            eventGridControlPlaneClient = controlPlaneClient;
            receiveWait = TimeSpan.FromSeconds(Math.Min(Math.Max(maxWaitTime, MinWaitTimeInSeconds), MaxWaitTimeInSeconds));
            this.writeToLog = writeToLog;
        }

        public async Task<Response<EventGridNamespaceResource>> GetNamespacesAsync(string resourceGroupName, string namespaceName,
            CancellationToken cancellationToken = default)
        {
            return await eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<NamespaceTopicResource>> GetTopicsAsync(string resourceGroupName, string namespaceName,
            NamespaceTopicEndpoint endpoint, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            long version;
            lock (topicRefreshLock)
            {
                version = ++topicRefreshVersion;
            }

            var next = new NamespaceClients(endpoint?.Address);
            var topics = new List<NamespaceTopicResource>();
            var collection = eventGridControlPlaneClient.GetNamespaceResource(resourceGroupName, namespaceName).GetNamespaceTopics();
            var enumerator = collection.GetAllAsync(cancellationToken: cancellationToken).GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    var topic = enumerator.Current;
                    topics.Add(topic);
                    next.Topics[topic.Data.Name] = new TopicState(topic);
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
            }

            lock (topicRefreshLock)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (version == topicRefreshVersion)
                {
                    clients = next;
                }
            }
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
            await WithTopicClientsAsync(topicName, topic => topic.Sender.Value.SendAsync(cloudEvent));
        }

        public async Task PublishEventsAsync(string topicName, string eventSource, string eventType, List<string> publishEvents)
        {
            var cloudEvents = new CloudEvent[publishEvents.Count];

            for (var i = 0; i < cloudEvents.Length; i++)
            {
                var eventModel = publishEvents[i];
                cloudEvents[i] = new CloudEvent(eventSource, eventType, eventModel);
            }

            await WithTopicClientsAsync(topicName, topic => topic.Sender.Value.SendAsync(cloudEvents));
        }

        public async Task<ReceiveResult> ReceiveEventsAsync(string topicName, string subscriptionName, int maxEventNum)
        {
            try
            {
                var response = await WithTopicClientsAsync(topicName, topic => topic.GetReceiver(subscriptionName)
                    .ReceiveAsync(maxEvents: maxEventNum, maxWaitTime: receiveWait));
                return response.Value;
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

                try
                {
                    switch (action)
                    {
                        case "Acknowledge":
                            AcknowledgeResult acknowledgeResult = await WithTopicClientsAsync(topicName, topic =>
                                topic.GetReceiver(subscriptionName).AcknowledgeAsync(lockTokens));
                            succeededLockTokens = acknowledgeResult.SucceededLockTokens;
                            failedLockTokens = acknowledgeResult.FailedLockTokens;
                            break;
                        case "Release":
                            ReleaseResult releaseResult = await WithTopicClientsAsync(topicName, topic =>
                                topic.GetReceiver(subscriptionName).ReleaseAsync(lockTokens));
                            succeededLockTokens = releaseResult.SucceededLockTokens;
                            failedLockTokens = releaseResult.FailedLockTokens;
                            break;
                        case "Reject":
                            RejectResult rejectResult = await WithTopicClientsAsync(topicName, topic =>
                                topic.GetReceiver(subscriptionName).RejectAsync(lockTokens));
                            succeededLockTokens = rejectResult.SucceededLockTokens;
                            failedLockTokens = rejectResult.FailedLockTokens;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(action), action, "Use Acknowledge, Release, or Reject.");
                    }
                }
                catch (EventGridDataPlaneException ex)
                {
                    writeToLog(ex.Message);
                    return false;
                }
                catch (RequestFailedException ex)
                {
                    writeToLog($"Event Grid could not {action.ToLowerInvariant()} events for topic '{topicName}' (HTTP {ex.Status}). " +
                        DataPlaneRetryNotice);
                    return false;
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

        private async Task<TResult> WithTopicClientsAsync<TResult>(string topicName, Func<TopicClients, Task<TResult>> operation)
        {
            var current = clients;
            if (current.Endpoint == null)
            {
                throw new EventGridDataPlaneException("The Event Grid namespace endpoint is not loaded. Refresh the namespace topics and try again.");
            }

            var topic = current.GetTopic(topicName);
            var initialization = topic.GetOrStart(() => LoadTopicClientsAsync(current.Endpoint, topic.Resource, topicName));
            TopicClients dataPlane;
            try
            {
                dataPlane = await initialization;
            }
            catch
            {
                topic.Invalidate(initialization);
                throw;
            }

            try
            {
                return await operation(dataPlane);
            }
            catch (RequestFailedException ex) when (ex.Status == 401)
            {
                // A concurrent retry may already have fetched a new key. Only discard ours.
                topic.Invalidate(initialization);
                throw new EventGridDataPlaneException(
                    $"Event Grid rejected authorization for topic '{topicName}'. If its access key rotated, retry the operation to fetch the current key. " +
                    "Also verify that local key authentication is enabled and the namespace is reachable. " +
                    DataPlaneRetryNotice, ex);
            }
            catch (RequestFailedException ex) when (ex.Status == 403)
            {
                throw new EventGridDataPlaneException(
                    $"Event Grid denied data-plane access to topic '{topicName}' (HTTP 403). Check local key authentication " +
                    "and the namespace network access policy. " + DataPlaneRetryNotice, ex);
            }
        }

        private async Task<TopicClients> LoadTopicClientsAsync(Uri endpoint, NamespaceTopicResource resource, string topicName)
        {
            string key;
            try
            {
                key = (await resource.GetSharedAccessKeysAsync()).Value?.Key1;
            }
            catch (RequestFailedException ex)
            {
                var advice = ex.Status == 429
                    ? "Wait for throttling to clear and retry."
                    : "For publish, pull or settlement, verify that your account can list topic keys and that local key authentication is enabled, then retry.";
                throw new EventGridDataPlaneException(
                    $"Could not read an access key for Event Grid topic '{topicName}' (HTTP {ex.Status}). Browsing is still available. " +
                    advice, ex);
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new EventGridDataPlaneException(
                    $"No access key was returned for Event Grid topic '{topicName}'. Browsing is still available. " +
                    "Verify that local key authentication is enabled and retry.");
            }

            var credential = new AzureKeyCredential(key);
            return new TopicClients(
                () => CreateSenderClient(endpoint, topicName, credential),
                subscription => CreateReceiverClient(endpoint, topicName, subscription, credential));
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
            public Dictionary<string, TopicState> Topics { get; } = new Dictionary<string, TopicState>();

            public TopicState GetTopic(string name)
            {
                if (string.IsNullOrWhiteSpace(name) || !Topics.TryGetValue(name, out var topic))
                {
                    throw new EventGridDataPlaneException($"The Event Grid topic '{name}' is not loaded. Refresh the namespace topics and try again.");
                }

                return topic;
            }
        }

        private sealed class TopicState
        {
            private readonly object initializationLock = new object();
            private Task<TopicClients> initialization;

            public TopicState(NamespaceTopicResource resource) => Resource = resource;

            public NamespaceTopicResource Resource { get; }

            public Task<TopicClients> GetOrStart(Func<Task<TopicClients>> load)
            {
                lock (initializationLock)
                {
                    return initialization ?? (initialization = load());
                }
            }

            public void Invalidate(Task<TopicClients> used)
            {
                lock (initializationLock)
                {
                    if (ReferenceEquals(initialization, used))
                    {
                        initialization = null;
                    }
                }
            }
        }

        private sealed class TopicClients
        {
            private readonly Func<string, EventGridReceiverClient> createReceiver;
            private readonly ConcurrentDictionary<string, Lazy<EventGridReceiverClient>> receivers =
                new ConcurrentDictionary<string, Lazy<EventGridReceiverClient>>();

            public TopicClients(Func<EventGridSenderClient> createSender, Func<string, EventGridReceiverClient> createReceiver)
            {
                Sender = new Lazy<EventGridSenderClient>(createSender);
                this.createReceiver = createReceiver;
            }

            public Lazy<EventGridSenderClient> Sender { get; }

            public EventGridReceiverClient GetReceiver(string subscription) =>
                receivers.GetOrAdd(subscription, name => new Lazy<EventGridReceiverClient>(() => createReceiver(name))).Value;
        }

        private sealed class EventGridDataPlaneException : InvalidOperationException
        {
            public EventGridDataPlaneException(string message, Exception innerException = null)
                : base(message, innerException)
            {
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
