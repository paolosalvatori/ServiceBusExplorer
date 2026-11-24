using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusExplorer.UIHelpers
{
    // Owned by the UI thread. Track the displayed topics independently of the filter's visible projection.
    internal sealed class EventGridSubscriptionLoads<TTopic> where TTopic : class
    {
        private readonly Dictionary<string, TopicEntry> topics = new Dictionary<string, TopicEntry>(StringComparer.OrdinalIgnoreCase);

        internal void Reset() => topics.Clear();

        internal void Track(string topicName, TTopic topic) => topics[topicName] = new TopicEntry(topic);

        internal TTopic Find(string topicName) => topics.TryGetValue(topicName, out var entry) ? entry.Topic : null;

        internal async Task LoadAsync<TResult>(string topicName, TTopic topic, Func<bool> canUpdateTree,
            Func<Func<bool>, Task<TResult>> readAsync, Action<TResult> apply, Action<Exception> reportFailure)
        {
            if (!topics.TryGetValue(topicName, out var entry) || !ReferenceEquals(entry.Topic, topic) || !canUpdateTree())
            {
                return;
            }

            var request = new object();
            entry.Request = request;

            try
            {
                var result = await readAsync(IsCurrent);
                if (IsCurrent())
                {
                    apply(result);
                }
            }
            catch (Exception ex)
            {
                if (IsCurrent())
                {
                    reportFailure(ex);
                }
            }

            bool IsCurrent() => canUpdateTree() && topics.TryGetValue(topicName, out var current)
                && ReferenceEquals(current, entry) && ReferenceEquals(entry.Request, request);
        }

        private sealed class TopicEntry
        {
            internal TopicEntry(TTopic topic) => Topic = topic;

            internal TTopic Topic { get; }
            internal object Request { get; set; }
        }
    }
}
