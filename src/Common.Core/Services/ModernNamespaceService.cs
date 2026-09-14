using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Messaging.ServiceBus.Administration;
using ServiceBusExplorer.Core.Abstractions;
using ServiceBusExplorer.Core.Models;

using SdkEntityStatus  = Azure.Messaging.ServiceBus.Administration.EntityStatus;
using CoreEntityStatus = ServiceBusExplorer.Core.Models.EntityStatus;
using CoreQueueProps   = ServiceBusExplorer.Core.Models.QueueProperties;
using CoreTopicProps   = ServiceBusExplorer.Core.Models.TopicProperties;
using CoreSubProps     = ServiceBusExplorer.Core.Models.SubscriptionProperties;
using NotificationHubDescription = Microsoft.Azure.NotificationHubs.NotificationHubDescription;
using NotificationHubNamespaceManager = Microsoft.Azure.NotificationHubs.NamespaceManager;

namespace ServiceBusExplorer.Core.Services
{
    public sealed class ModernNamespaceService : INamespaceService
    {
        private readonly ServiceBusAdministrationClient _admin;
        private readonly HashSet<string> _selectedEntities;
        private readonly NotificationHubNamespaceManager? _notificationHubNamespaceManager;
        private readonly string? _notificationHubEntityPath;

        public ModernNamespaceService(string connectionString, IEnumerable<string>? selectedEntities = null, string? entityPath = null)
        {
            _admin = new ServiceBusAdministrationClient(connectionString);
            _selectedEntities = CreateSelectedEntitySet(selectedEntities);
            _notificationHubEntityPath = NullIfWhiteSpace(entityPath);

            if (IsSelected(EntityTypeConstants.NotificationHubs))
            {
                var notificationHubConnectionString = RemoveConnectionStringKeys(connectionString, "TransportType", "EntityPath");
                _notificationHubNamespaceManager = NotificationHubNamespaceManager.CreateFromConnectionString(notificationHubConnectionString);
            }
        }

        public ModernNamespaceService(string fullyQualifiedNamespace, TokenCredential credential, IEnumerable<string>? selectedEntities = null)
        {
            _admin = new ServiceBusAdministrationClient(fullyQualifiedNamespace, credential);
            _selectedEntities = CreateSelectedEntitySet(selectedEntities);
        }

        public static ModernNamespaceService FromProfile(ConnectionProfile profile)
        {
            if (profile.IsAad)
            {
                if (string.IsNullOrWhiteSpace(profile.FullyQualifiedNamespace))
                    throw new ArgumentException("FullyQualifiedNamespace is required for AAD auth.");
                var credential = AadTokenCredentialFactory.Create(profile.TenantId);
                return new ModernNamespaceService(profile.FullyQualifiedNamespace!, credential, profile.SelectedEntities);
            }
            if (string.IsNullOrWhiteSpace(profile.ConnectionString))
                throw new ArgumentException("ConnectionString is required for SAS auth.");
            return new ModernNamespaceService(profile.ConnectionString!, profile.SelectedEntities, profile.EntityPath);
        }

        // Queues

        public async Task<IReadOnlyList<EntityInfo>> GetQueuesAsync(string? filter = null, CancellationToken ct = default)
        {
            var results = new List<EntityInfo>();
            await foreach (var p in _admin.GetQueuesRuntimePropertiesAsync(ct))
            {
                if (filter != null && !p.Name.StartsWith(filter, StringComparison.OrdinalIgnoreCase)) continue;
                results.Add(MapQueue(p));
            }
            return results;
        }

        public async Task<EntityInfo> GetQueueAsync(string name, CancellationToken ct = default)
            => MapQueue((await _admin.GetQueueRuntimePropertiesAsync(name, ct).ConfigureAwait(false)).Value);

        public async Task<EntityInfo> CreateQueueAsync(string name, CancellationToken ct = default)
        {
            await _admin.CreateQueueAsync(name, ct).ConfigureAwait(false);
            return await GetQueueAsync(name, ct).ConfigureAwait(false);
        }

        public async Task DeleteQueueAsync(string name, CancellationToken ct = default)
            => await _admin.DeleteQueueAsync(name, ct).ConfigureAwait(false);

        // Topics

        public async Task<IReadOnlyList<EntityInfo>> GetTopicsAsync(string? filter = null, CancellationToken ct = default)
        {
            var results = new List<EntityInfo>();
            await foreach (var p in _admin.GetTopicsRuntimePropertiesAsync(ct))
            {
                if (filter != null && !p.Name.StartsWith(filter, StringComparison.OrdinalIgnoreCase)) continue;
                results.Add(MapTopic(p));
            }
            return results;
        }

        public async Task<EntityInfo> GetTopicAsync(string name, CancellationToken ct = default)
            => MapTopic((await _admin.GetTopicRuntimePropertiesAsync(name, ct).ConfigureAwait(false)).Value);

        public async Task<EntityInfo> CreateTopicAsync(string name, CancellationToken ct = default)
        {
            await _admin.CreateTopicAsync(name, ct).ConfigureAwait(false);
            return await GetTopicAsync(name, ct).ConfigureAwait(false);
        }

        public async Task DeleteTopicAsync(string name, CancellationToken ct = default)
            => await _admin.DeleteTopicAsync(name, ct).ConfigureAwait(false);

        // Subscriptions

        public async Task<IReadOnlyList<EntityInfo>> GetSubscriptionsAsync(string topicPath, CancellationToken ct = default)
        {
            var results = new List<EntityInfo>();
            await foreach (var p in _admin.GetSubscriptionsRuntimePropertiesAsync(topicPath, ct))
                results.Add(MapSubscription(topicPath, p));
            return results;
        }

        // Notification Hubs

        public async Task<IReadOnlyList<EntityInfo>> GetNotificationHubsAsync(CancellationToken ct = default)
        {
            if (_notificationHubNamespaceManager == null)
                throw new NotSupportedException("Notification Hubs discovery currently requires a SAS connection string.");

            try
            {
                IEnumerable<NotificationHubDescription> hubs;
                if (!string.IsNullOrWhiteSpace(_notificationHubEntityPath))
                {
                    var hub = await _notificationHubNamespaceManager
                        .GetNotificationHubAsync(_notificationHubEntityPath!, ct)
                        .ConfigureAwait(false);
                    hubs = new[] { hub };
                }
                else
                {
                    hubs = await _notificationHubNamespaceManager
                        .GetNotificationHubsAsync(ct)
                        .ConfigureAwait(false);
                }

                return hubs
                    .Select(MapNotificationHub)
                    .OrderBy(h => h.Name, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Unable to load Notification Hubs. Use a Notification Hubs namespace or hub SAS connection string with Manage permission.",
                    ex);
            }
        }

        public async Task<EntityInfo> CreateSubscriptionAsync(string topicPath, string name, CancellationToken ct = default)
        {
            await _admin.CreateSubscriptionAsync(topicPath, name, ct).ConfigureAwait(false);
            var p = await _admin.GetSubscriptionRuntimePropertiesAsync(topicPath, name, ct).ConfigureAwait(false);
            return MapSubscription(topicPath, p.Value);
        }

        public async Task DeleteSubscriptionAsync(string topicPath, string name, CancellationToken ct = default)
            => await _admin.DeleteSubscriptionAsync(topicPath, name, ct).ConfigureAwait(false);

        // Full-property get

        public async Task<CoreQueueProps> GetQueuePropertiesAsync(string name, CancellationToken ct = default)
        {
            var p = (await _admin.GetQueueAsync(name, ct).ConfigureAwait(false)).Value;
            return new CoreQueueProps
            {
                Name                                   = p.Name,
                Status                                 = MapStatus(p.Status),
                MaxSizeInMegabytes                     = p.MaxSizeInMegabytes,
                MaxDeliveryCount                       = p.MaxDeliveryCount,
                DefaultMessageTimeToLive               = p.DefaultMessageTimeToLive,
                LockDuration                           = p.LockDuration,
                AutoDeleteOnIdle                       = p.AutoDeleteOnIdle,
                RequiresDuplicateDetection             = p.RequiresDuplicateDetection,
                DuplicateDetectionHistoryTimeWindow    = p.DuplicateDetectionHistoryTimeWindow,
                RequiresSession                        = p.RequiresSession,
                EnableDeadLetteringOnMessageExpiration = p.DeadLetteringOnMessageExpiration,
                EnableBatchedOperations                = p.EnableBatchedOperations,
                ForwardTo                              = p.ForwardTo,
                ForwardDeadLetteredMessagesTo          = p.ForwardDeadLetteredMessagesTo
            };
        }

        public async Task<CoreTopicProps> GetTopicPropertiesAsync(string name, CancellationToken ct = default)
        {
            var p = (await _admin.GetTopicAsync(name, ct).ConfigureAwait(false)).Value;
            return new CoreTopicProps
            {
                Name                                = p.Name,
                Status                              = MapStatus(p.Status),
                MaxSizeInMegabytes                  = p.MaxSizeInMegabytes,
                DefaultMessageTimeToLive            = p.DefaultMessageTimeToLive,
                AutoDeleteOnIdle                    = p.AutoDeleteOnIdle,
                RequiresDuplicateDetection          = p.RequiresDuplicateDetection,
                DuplicateDetectionHistoryTimeWindow = p.DuplicateDetectionHistoryTimeWindow,
                EnableBatchedOperations             = p.EnableBatchedOperations,
                SupportOrdering                     = p.SupportOrdering
            };
        }

        public async Task<CoreSubProps> GetSubscriptionPropertiesAsync(string topicPath, string name, CancellationToken ct = default)
        {
            var p = (await _admin.GetSubscriptionAsync(topicPath, name, ct).ConfigureAwait(false)).Value;
            return new CoreSubProps
            {
                Name                                           = p.SubscriptionName,
                TopicPath                                      = topicPath,
                Status                                         = MapStatus(p.Status),
                MaxDeliveryCount                               = p.MaxDeliveryCount,
                DefaultMessageTimeToLive                       = p.DefaultMessageTimeToLive,
                LockDuration                                   = p.LockDuration,
                AutoDeleteOnIdle                               = p.AutoDeleteOnIdle,
                RequiresSession                                = p.RequiresSession,
                EnableDeadLetteringOnMessageExpiration         = p.DeadLetteringOnMessageExpiration,
                EnableDeadLetteringOnFilterEvaluationException = p.EnableDeadLetteringOnFilterEvaluationExceptions,
                EnableBatchedOperations                        = p.EnableBatchedOperations,
                ForwardTo                                      = p.ForwardTo,
                ForwardDeadLetteredMessagesTo                  = p.ForwardDeadLetteredMessagesTo
            };
        }

        // Full-property update

        public async Task<EntityInfo> UpdateQueueAsync(CoreQueueProps props, CancellationToken ct = default)
        {
            var p = (await _admin.GetQueueAsync(props.Name, ct).ConfigureAwait(false)).Value;
            p.MaxSizeInMegabytes                     = props.MaxSizeInMegabytes;
            p.MaxDeliveryCount                       = props.MaxDeliveryCount;
            p.DefaultMessageTimeToLive               = props.DefaultMessageTimeToLive;
            p.LockDuration                           = props.LockDuration;
            p.AutoDeleteOnIdle                       = props.AutoDeleteOnIdle;
            p.DeadLetteringOnMessageExpiration       = props.EnableDeadLetteringOnMessageExpiration;
            p.EnableBatchedOperations                = props.EnableBatchedOperations;
            p.ForwardTo                              = props.ForwardTo;
            p.ForwardDeadLetteredMessagesTo          = props.ForwardDeadLetteredMessagesTo;
            p.Status                                 = MapToSdk(props.Status);
            await _admin.UpdateQueueAsync(p, ct).ConfigureAwait(false);
            return await GetQueueAsync(props.Name, ct).ConfigureAwait(false);
        }

        public async Task<EntityInfo> UpdateTopicAsync(CoreTopicProps props, CancellationToken ct = default)
        {
            var p = (await _admin.GetTopicAsync(props.Name, ct).ConfigureAwait(false)).Value;
            p.MaxSizeInMegabytes       = props.MaxSizeInMegabytes;
            p.DefaultMessageTimeToLive = props.DefaultMessageTimeToLive;
            p.AutoDeleteOnIdle         = props.AutoDeleteOnIdle;
            p.EnableBatchedOperations  = props.EnableBatchedOperations;
            p.SupportOrdering          = props.SupportOrdering;
            p.Status                   = MapToSdk(props.Status);
            await _admin.UpdateTopicAsync(p, ct).ConfigureAwait(false);
            return await GetTopicAsync(props.Name, ct).ConfigureAwait(false);
        }

        public async Task<EntityInfo> UpdateSubscriptionAsync(CoreSubProps props, CancellationToken ct = default)
        {
            var p = (await _admin.GetSubscriptionAsync(props.TopicPath, props.Name, ct).ConfigureAwait(false)).Value;
            p.MaxDeliveryCount                                = props.MaxDeliveryCount;
            p.DefaultMessageTimeToLive                        = props.DefaultMessageTimeToLive;
            p.LockDuration                                    = props.LockDuration;
            p.AutoDeleteOnIdle                                = props.AutoDeleteOnIdle;
            p.DeadLetteringOnMessageExpiration                = props.EnableDeadLetteringOnMessageExpiration;
            p.EnableDeadLetteringOnFilterEvaluationExceptions = props.EnableDeadLetteringOnFilterEvaluationException;
            p.EnableBatchedOperations                         = props.EnableBatchedOperations;
            p.ForwardTo                                       = props.ForwardTo;
            p.ForwardDeadLetteredMessagesTo                   = props.ForwardDeadLetteredMessagesTo;
            p.Status                                          = MapToSdk(props.Status);
            await _admin.UpdateSubscriptionAsync(p, ct).ConfigureAwait(false);
            var rp = await _admin.GetSubscriptionRuntimePropertiesAsync(props.TopicPath, props.Name, ct).ConfigureAwait(false);
            return MapSubscription(props.TopicPath, rp.Value);
        }

        // Create with full options

        public async Task<EntityInfo> CreateQueueWithOptionsAsync(CoreQueueProps props, CancellationToken ct = default)
        {
            await _admin.CreateQueueAsync(new CreateQueueOptions(props.Name)
            {
                MaxSizeInMegabytes                      = props.MaxSizeInMegabytes,
                MaxDeliveryCount                        = props.MaxDeliveryCount,
                DefaultMessageTimeToLive                = props.DefaultMessageTimeToLive,
                LockDuration                            = props.LockDuration,
                AutoDeleteOnIdle                        = props.AutoDeleteOnIdle,
                RequiresDuplicateDetection              = props.RequiresDuplicateDetection,
                DuplicateDetectionHistoryTimeWindow     = props.DuplicateDetectionHistoryTimeWindow,
                RequiresSession                         = props.RequiresSession,
                DeadLetteringOnMessageExpiration        = props.EnableDeadLetteringOnMessageExpiration,
                EnableBatchedOperations                 = props.EnableBatchedOperations,
                ForwardTo                               = props.ForwardTo,
                ForwardDeadLetteredMessagesTo           = props.ForwardDeadLetteredMessagesTo
            }, ct).ConfigureAwait(false);
            return await GetQueueAsync(props.Name, ct).ConfigureAwait(false);
        }

        public async Task<EntityInfo> CreateTopicWithOptionsAsync(CoreTopicProps props, CancellationToken ct = default)
        {
            await _admin.CreateTopicAsync(new CreateTopicOptions(props.Name)
            {
                MaxSizeInMegabytes                  = props.MaxSizeInMegabytes,
                DefaultMessageTimeToLive            = props.DefaultMessageTimeToLive,
                AutoDeleteOnIdle                    = props.AutoDeleteOnIdle,
                RequiresDuplicateDetection          = props.RequiresDuplicateDetection,
                DuplicateDetectionHistoryTimeWindow = props.DuplicateDetectionHistoryTimeWindow,
                EnableBatchedOperations             = props.EnableBatchedOperations,
                SupportOrdering                     = props.SupportOrdering
            }, ct).ConfigureAwait(false);
            return await GetTopicAsync(props.Name, ct).ConfigureAwait(false);
        }

        public async Task<EntityInfo> CreateSubscriptionWithOptionsAsync(CoreSubProps props, CancellationToken ct = default)
        {
            await _admin.CreateSubscriptionAsync(new CreateSubscriptionOptions(props.TopicPath, props.Name)
            {
                MaxDeliveryCount                                = props.MaxDeliveryCount,
                DefaultMessageTimeToLive                        = props.DefaultMessageTimeToLive,
                LockDuration                                    = props.LockDuration,
                AutoDeleteOnIdle                                = props.AutoDeleteOnIdle,
                RequiresSession                                 = props.RequiresSession,
                DeadLetteringOnMessageExpiration                = props.EnableDeadLetteringOnMessageExpiration,
                EnableDeadLetteringOnFilterEvaluationExceptions = props.EnableDeadLetteringOnFilterEvaluationException,
                EnableBatchedOperations                         = props.EnableBatchedOperations,
                ForwardTo                                       = props.ForwardTo,
                ForwardDeadLetteredMessagesTo                   = props.ForwardDeadLetteredMessagesTo
            }, ct).ConfigureAwait(false);
            var rp = await _admin.GetSubscriptionRuntimePropertiesAsync(props.TopicPath, props.Name, ct).ConfigureAwait(false);
            return MapSubscription(props.TopicPath, rp.Value);
        }

        // Full tree

        public async Task<IReadOnlyList<EntityTreeNode>> GetEntityTreeAsync(CancellationToken ct = default)
        {
            var root = new List<EntityTreeNode>();

            if (IsSelected(EntityTypeConstants.Queues))
            {
                var queues = await GetQueuesAsync(ct: ct).ConfigureAwait(false);
                var queueNode = new EntityTreeNode { Label = "Queues", Kind = EntityKind.Queue };
                foreach (var q in queues.OrderBy(q => q.Name, StringComparer.OrdinalIgnoreCase))
                    AddQueueNode(queueNode, q);
                SortChildrenFoldersFirst(queueNode);
                root.Add(queueNode);
            }

            if (IsSelected(EntityTypeConstants.Topics))
            {
                var topics = await GetTopicsAsync(ct: ct).ConfigureAwait(false);
                var topicsNode = new EntityTreeNode { Label = "Topics", Kind = EntityKind.Topic };
                foreach (var t in topics.OrderBy(t => t.Name, StringComparer.OrdinalIgnoreCase))
                {
                    var topicNode = new EntityTreeNode { Label = t.Name, Kind = EntityKind.Topic, Entity = t };
                    try
                    {
                        var subs = await GetSubscriptionsAsync(t.Name, ct).ConfigureAwait(false);
                        foreach (var s in subs.OrderBy(s => s.Name))
                            topicNode.Children.Add(new EntityTreeNode { Label = s.Name, Kind = EntityKind.Subscription, Entity = s });
                    }
                    catch (RequestFailedException) { }

                    AddTopicNode(topicsNode, topicNode);
                }
                SortChildrenFoldersFirst(topicsNode);
                root.Add(topicsNode);
            }

            if (IsSelected(EntityTypeConstants.NotificationHubs))
            {
                var notificationHubs = await GetNotificationHubsAsync(ct).ConfigureAwait(false);
                var notificationHubsNode = new EntityTreeNode { Label = "Notification Hubs", Kind = EntityKind.NotificationHub };
                foreach (var hub in notificationHubs)
                {
                    notificationHubsNode.Children.Add(new EntityTreeNode
                    {
                        Label = hub.Name,
                        Kind = EntityKind.NotificationHub,
                        Entity = hub
                    });
                }
                root.Add(notificationHubsNode);
            }

            return root;
        }

        private static void AddQueueNode(EntityTreeNode rootQueueNode, EntityInfo queue)
        {
            // Keep flat rendering for simple names and create folder hierarchy for path-like names.
            if (queue.Name.IndexOf('/') < 0)
            {
                rootQueueNode.Children.Add(new EntityTreeNode
                {
                    Label = queue.Name,
                    Kind = EntityKind.Queue,
                    Entity = queue
                });
                return;
            }

            var segments = queue.Name
                .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            if (segments.Length == 0)
            {
                rootQueueNode.Children.Add(new EntityTreeNode
                {
                    Label = queue.Name,
                    Kind = EntityKind.Queue,
                    Entity = queue
                });
                return;
            }

            var cursor = rootQueueNode;

            for (var i = 0; i < segments.Length - 1; i++)
            {
                var segment = segments[i];
                var folder = cursor.Children.FirstOrDefault(c =>
                    c.Entity == null
                    && c.Label.Equals(segment, StringComparison.OrdinalIgnoreCase));

                if (folder == null)
                {
                    folder = new EntityTreeNode { Label = segment };
                    cursor.Children.Add(folder);
                }

                cursor = folder;
            }

            cursor.Children.Add(new EntityTreeNode
            {
                Label = segments[segments.Length - 1],
                Kind = EntityKind.Queue,
                Entity = queue
            });
        }

        private static void AddTopicNode(EntityTreeNode rootTopicsNode, EntityTreeNode topicLeaf)
        {
            if (topicLeaf.Entity == null || topicLeaf.Entity.Kind != EntityKind.Topic)
            {
                rootTopicsNode.Children.Add(topicLeaf);
                return;
            }

            var topicName = topicLeaf.Entity.Name;
            if (topicName.IndexOf('/') < 0)
            {
                rootTopicsNode.Children.Add(topicLeaf);
                return;
            }

            var segments = topicName
                .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            if (segments.Length == 0)
            {
                rootTopicsNode.Children.Add(topicLeaf);
                return;
            }

            var cursor = rootTopicsNode;
            for (var i = 0; i < segments.Length - 1; i++)
            {
                var segment = segments[i];
                var folder = cursor.Children.FirstOrDefault(c =>
                    c.Entity == null
                    && c.Label.Equals(segment, StringComparison.OrdinalIgnoreCase));

                if (folder == null)
                {
                    folder = new EntityTreeNode { Label = segment };
                    cursor.Children.Add(folder);
                }

                cursor = folder;
            }

            topicLeaf.Label = segments[segments.Length - 1];
            cursor.Children.Add(topicLeaf);
        }

        private static void SortChildrenFoldersFirst(EntityTreeNode node)
        {
            if (node.Children.Count == 0)
                return;

            node.Children.Sort((a, b) =>
            {
                var aFolder = a.Entity == null;
                var bFolder = b.Entity == null;

                if (aFolder && !bFolder) return -1;
                if (!aFolder && bFolder) return 1;

                return string.Compare(a.Label, b.Label, StringComparison.OrdinalIgnoreCase);
            });

            foreach (var child in node.Children)
                SortChildrenFoldersFirst(child);
        }

        // Mappers

        private static EntityInfo MapQueue(QueueRuntimeProperties p) => new EntityInfo
        {
            Kind = EntityKind.Queue, Name = p.Name,
            ActiveMessageCount             = p.ActiveMessageCount,
            DeadLetterMessageCount         = p.DeadLetterMessageCount,
            ScheduledMessageCount          = p.ScheduledMessageCount,
            TransferMessageCount           = p.TransferMessageCount,
            TransferDeadLetterMessageCount = p.TransferDeadLetterMessageCount,
            TotalMessageCount              = p.TotalMessageCount
        };

        private static EntityInfo MapTopic(TopicRuntimeProperties p) => new EntityInfo
        {
            Kind = EntityKind.Topic, Name = p.Name,
            ScheduledMessageCount = p.ScheduledMessageCount
        };

        private static EntityInfo MapSubscription(string topicPath, SubscriptionRuntimeProperties p) => new EntityInfo
        {
            Kind = EntityKind.Subscription, Name = p.SubscriptionName, TopicPath = topicPath,
            ActiveMessageCount             = p.ActiveMessageCount,
            DeadLetterMessageCount         = p.DeadLetterMessageCount,
            TransferMessageCount           = p.TransferMessageCount,
            TransferDeadLetterMessageCount = p.TransferDeadLetterMessageCount,
            TotalMessageCount              = p.TotalMessageCount
        };

        private static EntityInfo MapNotificationHub(NotificationHubDescription p) => new EntityInfo
        {
            Kind = EntityKind.NotificationHub,
            Name = p.Path
        };

        private bool IsSelected(string entityType) => _selectedEntities.Contains(entityType);

        private static HashSet<string> CreateSelectedEntitySet(IEnumerable<string>? selectedEntities)
        {
            return new HashSet<string>(
                selectedEntities ?? new[] { EntityTypeConstants.Queues, EntityTypeConstants.Topics },
                StringComparer.OrdinalIgnoreCase);
        }

        private static string? NullIfWhiteSpace(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value!.Trim();
        }

        private static string RemoveConnectionStringKeys(string connectionString, params string[] keysToRemove)
        {
            var keys = new HashSet<string>(keysToRemove, StringComparer.OrdinalIgnoreCase);
            var parts = connectionString
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(part =>
                {
                    var separatorIndex = part.IndexOf('=');
                    if (separatorIndex <= 0)
                        return true;

                    var key = part.Substring(0, separatorIndex).Trim();
                    return !keys.Contains(key);
                });

            return string.Join(";", parts);
        }

        private static CoreEntityStatus MapStatus(SdkEntityStatus s)
        {
            if (s == SdkEntityStatus.Disabled)
                return CoreEntityStatus.Disabled;
            if (s == SdkEntityStatus.SendDisabled)
                return CoreEntityStatus.SendDisabled;
            if (s == SdkEntityStatus.ReceiveDisabled)
                return CoreEntityStatus.ReceiveDisabled;
            return CoreEntityStatus.Active;
        }

        private static SdkEntityStatus MapToSdk(CoreEntityStatus s)
        {
            switch (s)
            {
                case CoreEntityStatus.Disabled:
                    return SdkEntityStatus.Disabled;
                case CoreEntityStatus.SendDisabled:
                    return SdkEntityStatus.SendDisabled;
                case CoreEntityStatus.ReceiveDisabled:
                    return SdkEntityStatus.ReceiveDisabled;
                default:
                    return SdkEntityStatus.Active;
            }
        }
    }
}
