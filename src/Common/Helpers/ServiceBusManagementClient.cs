using RuleDescription = ServiceBusExplorer.Helpers.RuleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBusExplorer.Helpers
{
    public sealed class ServiceBusManagementClient
    {
        public ServiceBusManagementClient(ServiceBusAdministrationClient client, Uri address)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public ServiceBusAdministrationClient Client { get; }
        public Uri Address { get; }

        private static T Wait<T>(Task<T> task) => task.ConfigureAwait(false).GetAwaiter().GetResult();

        private static List<T> ReadAll<T>(AsyncPageable<T> values) where T : notnull
        {
            var result = new List<T>();
            var pages = values.AsPages().GetAsyncEnumerator();
            try
            {
                while (pages.MoveNextAsync().AsTask().ConfigureAwait(false).GetAwaiter().GetResult())
                {
                    result.AddRange(pages.Current.Values);
                }
            }
            finally
            {
                pages.DisposeAsync().AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            return result;
        }

        public QueueInfo GetQueue(string path) => ReadQueue(path, CancellationToken.None);
        public TopicInfo GetTopic(string path) => ReadTopic(path, CancellationToken.None);
        public SubscriptionInfo GetSubscription(string topic, string name) =>
            new SubscriptionInfo(Wait(Client.GetSubscriptionAsync(topic, name)).Value, Wait(Client.GetSubscriptionRuntimePropertiesAsync(topic, name)).Value);

        private QueueInfo ReadQueue(string path, CancellationToken cancellationToken) =>
            new QueueInfo(Wait(Client.GetQueueAsync(path, cancellationToken)).Value, Wait(Client.GetQueueRuntimePropertiesAsync(path, cancellationToken)).Value);

        private TopicInfo ReadTopic(string path, CancellationToken cancellationToken)
        {
            var properties = Wait(Client.GetTopicAsync(path, cancellationToken)).Value;
            return ReadTopic(properties, Wait(Client.GetTopicRuntimePropertiesAsync(path, cancellationToken)).Value, cancellationToken);
        }

        private TopicInfo ReadTopic(TopicProperties properties, TopicRuntimeProperties runtime, CancellationToken cancellationToken)
        {
            var counts = new EntityMessageCounts { ScheduledMessageCount = runtime.ScheduledMessageCount };
            foreach (var subscription in ReadAll(Client.GetSubscriptionsRuntimePropertiesAsync(properties.Name, cancellationToken)))
            {
                counts.ActiveMessageCount += subscription.ActiveMessageCount;
                counts.DeadLetterMessageCount += subscription.DeadLetterMessageCount;
                counts.TransferMessageCount += subscription.TransferMessageCount;
                counts.TransferDeadLetterMessageCount += subscription.TransferDeadLetterMessageCount;
            }
            return new TopicInfo(properties, runtime, counts);
        }

        public IEnumerable<QueueInfo> GetQueues(string filter, int timeoutInSeconds)
        {
            var predicate = ServiceBusManagementFilter.Parse(filter);
            return WithTimeout(timeoutInSeconds, token =>
            {
                var runtimes = ReadAll(Client.GetQueuesRuntimePropertiesAsync(token)).ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase);
                return ReadAll(Client.GetQueuesAsync(token)).Select(properties =>
                    new QueueInfo(properties, runtimes.TryGetValue(properties.Name, out var runtime)
                        ? runtime : Wait(Client.GetQueueRuntimePropertiesAsync(properties.Name, token)).Value))
                    .Where(item => predicate(item.Path, item.MessageCount, item.CreatedAt, item.UpdatedAt, item.AccessedAt)).ToList();
            });
        }

        public IEnumerable<TopicInfo> GetTopics(string filter, int timeoutInSeconds)
        {
            var predicate = ServiceBusManagementFilter.Parse(filter);
            return WithTimeout(timeoutInSeconds, token => ReadAll(Client.GetTopicsAsync(token))
                .Select(properties => ReadTopic(properties, Wait(Client.GetTopicRuntimePropertiesAsync(properties.Name, token)).Value, token))
                .Where(item => predicate(item.Path,
                    item.MessageCountDetails.ActiveMessageCount + item.MessageCountDetails.DeadLetterMessageCount +
                    item.MessageCountDetails.ScheduledMessageCount + item.MessageCountDetails.TransferMessageCount +
                    item.MessageCountDetails.TransferDeadLetterMessageCount, item.CreatedAt, item.UpdatedAt, item.AccessedAt)).ToList());
        }

        public QueueInfo GetQueue(string path, int timeoutInSeconds) => WithTimeout(timeoutInSeconds, token => ReadQueue(path, token));
        public TopicInfo GetTopic(string path, int timeoutInSeconds) => WithTimeout(timeoutInSeconds, token => ReadTopic(path, token));

        private static T WithTimeout<T>(int timeoutInSeconds, Func<CancellationToken, T> action)
        {
            if (timeoutInSeconds <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeoutInSeconds));
            }
            using (var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutInSeconds)))
            {
                try { return action(cancellation.Token); }
                catch (OperationCanceledException ex) when (cancellation.IsCancellationRequested)
                {
                    throw new TimeoutException("Service Bus management enumeration timed out.", ex);
                }
            }
        }

        public IEnumerable<SubscriptionInfo> GetSubscriptions(string topic, string filter = null)
        {
            var predicate = ServiceBusManagementFilter.Parse(filter);
            return ReadAll(Client.GetSubscriptionsAsync(topic)).Select(properties =>
                new SubscriptionInfo(properties, Wait(Client.GetSubscriptionRuntimePropertiesAsync(topic, properties.SubscriptionName)).Value))
                .Where(item => predicate(item.Name, item.MessageCount, item.CreatedAt, item.UpdatedAt, item.AccessedAt)).ToList();
        }

        public QueueInfo CreateQueue(QueueInfo description)
        {
            var options = ServiceBusManagementMapper.ToCreateOptions(description.Description);
            options.MaxMessageSizeInKilobytes = description.MaxMessageSizeInKilobytes;
            var properties = Wait(Client.CreateQueueAsync(options)).Value;
            return new QueueInfo(properties, Wait(Client.GetQueueRuntimePropertiesAsync(properties.Name)).Value);
        }

        public TopicInfo CreateTopic(TopicInfo description)
        {
            var options = ServiceBusManagementMapper.ToCreateOptions(description.Description);
            options.MaxMessageSizeInKilobytes = description.MaxMessageSizeInKilobytes;
            var properties = Wait(Client.CreateTopicAsync(options)).Value;
            return ReadTopic(properties, Wait(Client.GetTopicRuntimePropertiesAsync(properties.Name)).Value, CancellationToken.None);
        }

        public SubscriptionInfo CreateSubscription(SubscriptionInfo description, RuleDescription rule = null)
        {
            var options = ServiceBusManagementMapper.ToCreateOptions(description.Description);
            var properties = rule == null ? Wait(Client.CreateSubscriptionAsync(options)).Value
                : Wait(Client.CreateSubscriptionAsync(options, ServiceBusManagementMapper.ToCreateOptions(rule.Description))).Value;
            return new SubscriptionInfo(properties, Wait(Client.GetSubscriptionRuntimePropertiesAsync(properties.TopicName, properties.SubscriptionName)).Value);
        }

        public QueueInfo UpdateQueue(QueueInfo description)
        {
            var properties = Wait(Client.GetQueueAsync(description.Path)).Value;
            properties.MaxMessageSizeInKilobytes = description.MaxMessageSizeInKilobytes ?? properties.MaxMessageSizeInKilobytes;
            Wait(Client.UpdateQueueAsync(ServiceBusManagementMapper.Apply(description.Description, properties)));
            return GetQueue(description.Path);
        }

        public TopicInfo UpdateTopic(TopicInfo description)
        {
            var properties = Wait(Client.GetTopicAsync(description.Path)).Value;
            properties.MaxMessageSizeInKilobytes = description.MaxMessageSizeInKilobytes ?? properties.MaxMessageSizeInKilobytes;
            Wait(Client.UpdateTopicAsync(ServiceBusManagementMapper.Apply(description.Description, properties)));
            return GetTopic(description.Path);
        }

        public SubscriptionInfo UpdateSubscription(SubscriptionInfo description)
        {
            var properties = Wait(Client.GetSubscriptionAsync(description.TopicPath, description.Name)).Value;
            Wait(Client.UpdateSubscriptionAsync(ServiceBusManagementMapper.Apply(description.Description, properties)));
            return GetSubscription(description.TopicPath, description.Name);
        }

        public async Task<QueueInfo> UpdateQueueAsync(QueueInfo description)
        {
            var properties = (await Client.GetQueueAsync(description.Path).ConfigureAwait(false)).Value;
            properties.MaxMessageSizeInKilobytes = description.MaxMessageSizeInKilobytes ?? properties.MaxMessageSizeInKilobytes;
            properties = (await Client.UpdateQueueAsync(ServiceBusManagementMapper.Apply(description.Description, properties)).ConfigureAwait(false)).Value;
            return new QueueInfo(properties, (await Client.GetQueueRuntimePropertiesAsync(description.Path).ConfigureAwait(false)).Value);
        }

        public async Task<TopicInfo> UpdateTopicAsync(TopicInfo description)
        {
            var properties = (await Client.GetTopicAsync(description.Path).ConfigureAwait(false)).Value;
            properties.MaxMessageSizeInKilobytes = description.MaxMessageSizeInKilobytes ?? properties.MaxMessageSizeInKilobytes;
            properties = (await Client.UpdateTopicAsync(ServiceBusManagementMapper.Apply(description.Description, properties)).ConfigureAwait(false)).Value;
            return ReadTopic(properties, (await Client.GetTopicRuntimePropertiesAsync(description.Path).ConfigureAwait(false)).Value, CancellationToken.None);
        }

        public async Task<SubscriptionInfo> UpdateSubscriptionAsync(SubscriptionInfo description)
        {
            var properties = (await Client.GetSubscriptionAsync(description.TopicPath, description.Name).ConfigureAwait(false)).Value;
            properties = (await Client.UpdateSubscriptionAsync(ServiceBusManagementMapper.Apply(description.Description, properties)).ConfigureAwait(false)).Value;
            return new SubscriptionInfo(properties, (await Client.GetSubscriptionRuntimePropertiesAsync(description.TopicPath, description.Name).ConfigureAwait(false)).Value);
        }

        public Task DeleteQueueAsync(string path) => Client.DeleteQueueAsync(path);
        public Task DeleteTopicAsync(string path) => Client.DeleteTopicAsync(path);
        public Task DeleteSubscriptionAsync(string topic, string name) => Client.DeleteSubscriptionAsync(topic, name);
        public IEnumerable<RuleDescription> GetRules(string topic, string subscription) =>
            ReadAll(Client.GetRulesAsync(topic, subscription)).Select(properties => new RuleInfo(properties)).ToList();
        public RuleDescription CreateRule(string topic, string subscription, RuleDescription rule) =>
            new RuleInfo(Wait(Client.CreateRuleAsync(topic, subscription, ServiceBusManagementMapper.ToCreateOptions(rule.Description))).Value);
        public void DeleteRule(string topic, string subscription, string rule) =>
            Client.DeleteRuleAsync(topic, subscription, rule).ConfigureAwait(false).GetAwaiter().GetResult();

        public QueueInfo RenameQueue(string path, string newPath) => throw new NotSupportedException(
            "The administration SDK does not support renaming queues. Create a new queue and explicitly transfer messages instead; the existing queue has not been changed.");
        public TopicInfo RenameTopic(string path, string newPath) => throw new NotSupportedException(
            "The administration SDK does not support renaming topics. Export/import to a new topic instead; the existing topic has not been changed.");
    }
}
