using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServiceBusExplorer.Core.Models;
#pragma warning disable CA2252 // preview features

namespace ServiceBusExplorer.Core.Abstractions
{
    /// <summary>
    /// Cross-platform Service Bus namespace operations backed by Azure.Messaging.ServiceBus.
    /// Implementations are independent of WindowsAzure.ServiceBus and run on macOS/Linux.
    /// </summary>
    public interface INamespaceService
    {
        // ── Discovery ──────────────────────────────────────────────────────────────────────

        Task<IReadOnlyList<EntityInfo>> GetQueuesAsync(string? filter = null, CancellationToken ct = default);
        Task<IReadOnlyList<EntityInfo>> GetTopicsAsync(string? filter = null, CancellationToken ct = default);
        Task<IReadOnlyList<EntityInfo>> GetSubscriptionsAsync(string topicPath, CancellationToken ct = default);
        Task<IReadOnlyList<EntityInfo>> GetNotificationHubsAsync(CancellationToken ct = default);

        /// <summary>Loads the selected namespace entity tree.</summary>
        Task<IReadOnlyList<EntityTreeNode>> GetEntityTreeAsync(CancellationToken ct = default);

        // ── Queue CRUD ─────────────────────────────────────────────────────────────────────

        Task<EntityInfo>  CreateQueueAsync(string name, CancellationToken ct = default);
        Task              DeleteQueueAsync(string name, CancellationToken ct = default);
        Task<EntityInfo>  GetQueueAsync(string name, CancellationToken ct = default);
        Task<QueueProperties> GetQueuePropertiesAsync(string name, CancellationToken ct = default);
        Task<EntityInfo> UpdateQueueAsync(QueueProperties props, CancellationToken ct = default);
        Task<EntityInfo> CreateQueueWithOptionsAsync(QueueProperties props, CancellationToken ct = default);

        // ── Topic CRUD ─────────────────────────────────────────────────────────────────────

        Task<EntityInfo>  CreateTopicAsync(string name, CancellationToken ct = default);
        Task              DeleteTopicAsync(string name, CancellationToken ct = default);
        Task<EntityInfo>  GetTopicAsync(string name, CancellationToken ct = default);
        Task<TopicProperties> GetTopicPropertiesAsync(string name, CancellationToken ct = default);
        Task<EntityInfo> UpdateTopicAsync(TopicProperties props, CancellationToken ct = default);
        Task<EntityInfo> CreateTopicWithOptionsAsync(TopicProperties props, CancellationToken ct = default);

        // ── Subscription CRUD ──────────────────────────────────────────────────────────────

        Task<EntityInfo>  CreateSubscriptionAsync(string topicPath, string name, CancellationToken ct = default);
        Task              DeleteSubscriptionAsync(string topicPath, string name, CancellationToken ct = default);
        Task<SubscriptionProperties> GetSubscriptionPropertiesAsync(string topicPath, string name, CancellationToken ct = default);
        Task<EntityInfo> UpdateSubscriptionAsync(SubscriptionProperties props, CancellationToken ct = default);
        Task<EntityInfo> CreateSubscriptionWithOptionsAsync(SubscriptionProperties props, CancellationToken ct = default);
    }
}
