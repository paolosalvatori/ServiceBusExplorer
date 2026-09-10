using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceBusExplorer.Core.Abstractions;
using ServiceBusExplorer.Core.Models;

namespace ServiceBusExplorer.Avalonia.ViewModels
{
    /// <summary>
    /// Represents a single node in the entity tree sidebar.
    /// Wraps <see cref="EntityTreeNode"/> for the Avalonia TreeView.
    /// </summary>
    public partial class EntityTreeNodeViewModel : ViewModelBase
    {
        [ObservableProperty] private bool _isExpanded;
        [ObservableProperty] private bool _isSelected;

        public string            Label    { get; }
        public EntityKind?       Kind     { get; }
        public EntityInfo?       Entity   { get; }
        public bool              IsGroup  { get; }

        public ObservableCollection<EntityTreeNodeViewModel> Children { get; } = new();

        public string Icon => Kind switch
        {
            EntityKind.Queue        => "📥",
            EntityKind.Topic        => "📤",
            EntityKind.Subscription => "🔔",
            EntityKind.EventHub     => "⚡",
            EntityKind.NotificationHub => "📣",
            EntityKind.Relay        => "🔁",
            _                       => "📁"
        };

        public string? MessageCountBadge =>
            Entity is { TotalMessageCount: > 0 } ? Entity.TotalMessageCount.ToString() : null;

        public string? DeadLetterBadge =>
            Entity is { DeadLetterMessageCount: > 0 }
                ? Entity.DeadLetterMessageCount.ToString()
                : null;

        /// <summary>
        /// Queue counters shown next to the queue name as:
        /// (active, dead-letter, transfer-DLQ)
        /// </summary>
        public string? QueueCounters =>
            Entity != null && Kind == EntityKind.Queue
                ? $"({Entity.ActiveMessageCount}, {Entity.DeadLetterMessageCount}, {Entity.TransferDeadLetterMessageCount})"
                : null;

        public EntityTreeNodeViewModel(EntityTreeNode model)
        {
            Label   = model.Label;
            Kind    = model.Kind;
            Entity  = model.Entity;
            IsGroup = model.IsGroup;

            foreach (var child in model.Children)
                Children.Add(new EntityTreeNodeViewModel(child));
        }
    }

    /// <summary>
    /// Manages the entity tree sidebar: loads entities from the namespace,
    /// supports refresh, and exposes the selected entity to the detail panel.
    /// </summary>
    public partial class EntityTreeViewModel : ViewModelBase
    {
        private readonly INamespaceService _namespaceService;
        private CancellationTokenSource   _cts = new CancellationTokenSource();
        private int                       _refreshInProgress;

        // ── Tree state ─────────────────────────────────────────────────────────────────────

        public ObservableCollection<EntityTreeNodeViewModel> Nodes { get; } = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasSelectedEntity))]
        [NotifyPropertyChangedFor(nameof(CanDeleteSelectedQueue))]
        [NotifyCanExecuteChangedFor(nameof(DeleteSelectedQueueCommand))]
        private EntityTreeNodeViewModel? _selectedNode;

        public bool HasSelectedEntity => SelectedNode?.Entity != null;
        public bool CanDeleteSelectedQueue => SelectedNode?.Entity?.Kind == EntityKind.Queue && !IsLoading;

        [ObservableProperty] private bool _isCreateQueuePanelOpen;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateQueueCommand))]
        private string _newQueueName = string.Empty;

        // ── Loading state ──────────────────────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanDeleteSelectedQueue))]
        [NotifyCanExecuteChangedFor(nameof(CreateQueueCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteSelectedQueueCommand))]
        private bool   _isLoading  = false;
        [ObservableProperty] private string _statusText = string.Empty;
        [ObservableProperty] private string _errorText  = string.Empty;

        // ── Events ─────────────────────────────────────────────────────────────────────────

        public event Action<EntityInfo>? EntitySelected;

        // ── Constructor ───────────────────────────────────────────────────────────────────

        public EntityTreeViewModel(INamespaceService namespaceService)
        {
            _namespaceService = namespaceService;
        }

        // ── Commands ──────────────────────────────────────────────────────────────────────

        [RelayCommand]
        public async Task RefreshAsync()
        {
            if (Interlocked.Exchange(ref _refreshInProgress, 1) == 1)
                return;

            var nextCts     = new CancellationTokenSource();
            var previousCts = Interlocked.Exchange(ref _cts, nextCts);
            previousCts.Cancel();
            previousCts.Dispose();

            var ct = nextCts.Token;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsLoading  = true;
                ErrorText  = string.Empty;
                StatusText = "Loading entities…";
            });

            try
            {
                var tree = await _namespaceService.GetEntityTreeAsync(ct).ConfigureAwait(false);

                var queueRoot = tree.FirstOrDefault(n => n.Label == "Queues");
                var queueCount = queueRoot != null ? CountEntities(queueRoot, EntityKind.Queue) : 0;

                var topicRoot = tree.FirstOrDefault(n => n.Label == "Topics");
                var topicCount = topicRoot != null ? CountEntities(topicRoot, EntityKind.Topic) : 0;

                var notificationHubRoot = tree.FirstOrDefault(n => n.Label == "Notification Hubs");
                var notificationHubCount = notificationHubRoot != null ? CountEntities(notificationHubRoot, EntityKind.NotificationHub) : 0;
                var statusParts = new List<string>();
                if (queueRoot != null)
                    statusParts.Add($"{queueCount} queue(s)");
                if (topicRoot != null)
                    statusParts.Add($"{topicCount} topic(s)");
                if (notificationHubRoot != null)
                    statusParts.Add($"{notificationHubCount} notification hub(s)");

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Nodes.Clear();
                    foreach (var node in tree)
                    {
                        var vm = new EntityTreeNodeViewModel(node) { IsExpanded = true };
                        Nodes.Add(vm);
                    }

                    StatusText = statusParts.Count > 0 ? string.Join(", ", statusParts) : "No selected entity types loaded.";
                });
            }
            catch (OperationCanceledException)
            {
                await Dispatcher.UIThread.InvokeAsync(() => StatusText = "Refresh cancelled.");
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    ErrorText  = ex.Message;
                    StatusText = "Failed to load entities.";
                });
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsLoading = false);
                Interlocked.Exchange(ref _refreshInProgress, 0);
            }
        }

        [RelayCommand]
        public async Task RefreshQueuesAsync()
        {
            if (Interlocked.Exchange(ref _refreshInProgress, 1) == 1)
                return;

            var nextCts     = new CancellationTokenSource();
            var previousCts = Interlocked.Exchange(ref _cts, nextCts);
            previousCts.Cancel();
            previousCts.Dispose();

            var ct = nextCts.Token;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsLoading = true;
                ErrorText = string.Empty;
                StatusText = SelectedNode?.Entity?.Kind == EntityKind.Queue
                    ? "Refreshing selected queue…"
                    : "Refreshing queues…";
            });

            try
            {
                if (SelectedNode?.Entity?.Kind == EntityKind.Queue)
                    await RefreshSelectedQueueAsync(SelectedNode.Entity.Name, ct).ConfigureAwait(false);
                else
                    await RefreshAllQueuesAsync(ct).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                await Dispatcher.UIThread.InvokeAsync(() => StatusText = "Queue refresh cancelled.");
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    ErrorText = ex.Message;
                    StatusText = "Failed to refresh queues.";
                });
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsLoading = false);
                Interlocked.Exchange(ref _refreshInProgress, 0);
            }
        }

        [RelayCommand]
        private void ToggleCreateQueuePanel()
        {
            IsCreateQueuePanelOpen = !IsCreateQueuePanelOpen;
            if (IsCreateQueuePanelOpen)
            {
                ErrorText = string.Empty;
                NewQueueName = string.Empty;
            }
        }

        [RelayCommand]
        private void CancelCreateQueue()
        {
            IsCreateQueuePanelOpen = false;
            NewQueueName = string.Empty;
        }

        private bool CanCreateQueue() => !IsLoading && !string.IsNullOrWhiteSpace(NewQueueName);

        [RelayCommand(CanExecute = nameof(CanCreateQueue))]
        private async Task CreateQueue()
        {
            var queueName = NewQueueName?.Trim();
            if (string.IsNullOrWhiteSpace(queueName))
                return;

            IsLoading = true;
            ErrorText = string.Empty;
            StatusText = $"Creating queue '{queueName}'…";

            try
            {
                await _namespaceService.CreateQueueAsync(queueName).ConfigureAwait(false);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    IsCreateQueuePanelOpen = false;
                    NewQueueName = string.Empty;
                    StatusText = $"Queue '{queueName}' created.";
                });

                await RefreshQueuesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    ErrorText = ex.Message;
                    StatusText = "Create queue failed.";
                });
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsLoading = false);
            }
        }

        [RelayCommand(CanExecute = nameof(CanDeleteSelectedQueue))]
        private async Task DeleteSelectedQueue()
        {
            var queueName = SelectedNode?.Entity?.Kind == EntityKind.Queue ? SelectedNode.Entity.Name : null;
            if (string.IsNullOrWhiteSpace(queueName))
                return;

            IsLoading = true;
            ErrorText = string.Empty;
            StatusText = $"Deleting queue '{queueName}'…";

            try
            {
                await _namespaceService.DeleteQueueAsync(queueName).ConfigureAwait(false);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    SelectedNode = null;
                    StatusText = $"Queue '{queueName}' deleted.";
                });

                await RefreshQueuesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    ErrorText = ex.Message;
                    StatusText = "Delete queue failed.";
                });
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsLoading = false);
            }
        }

        public void OnNodeSelected(EntityTreeNodeViewModel? node)
        {
            SelectedNode = node;
            if (node?.Entity != null)
                EntitySelected?.Invoke(node.Entity);
        }

        private static int CountEntities(EntityTreeNode node, EntityKind kind)
        {
            var self = node.Entity?.Kind == kind ? 1 : 0;
            if (node.Children.Count == 0)
                return self;

            var children = 0;
            foreach (var child in node.Children)
                children += CountEntities(child, kind);

            return self + children;
        }

        private async Task RefreshSelectedQueueAsync(string queueName, CancellationToken ct)
        {
            var refreshed = await _namespaceService.GetQueueAsync(queueName, ct).ConfigureAwait(false);
            var replaced = false;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var newNode = new EntityTreeNodeViewModel(new EntityTreeNode
                {
                    Label = LeafLabel(refreshed.Name),
                    Kind = EntityKind.Queue,
                    Entity = refreshed
                });

                replaced = TryReplaceQueueNode(Nodes, refreshed.Name, newNode);
                if (!replaced)
                {
                    // If queue shape changed unexpectedly, rebuild queue branch.
                    StatusText = "Selected queue path changed, rebuilding queue list…";
                    return;
                }

                SelectedNode = newNode;
                EntitySelected?.Invoke(refreshed);
                UpdateStatusTextFromCurrentNodes();
                StatusText = $"Refreshed queue '{refreshed.Name}'.";
            });

            // Fallback rebuild when the target node was not found in current tree.
            if (!replaced)
                await RefreshAllQueuesAsync(ct).ConfigureAwait(false);
        }

        private async Task RefreshAllQueuesAsync(CancellationToken ct)
        {
            var queues = await _namespaceService.GetQueuesAsync(ct: ct).ConfigureAwait(false);
            var selectedQueueName = SelectedNode?.Entity?.Kind == EntityKind.Queue
                ? SelectedNode.Entity.Name
                : null;

            var queueRootModel = new EntityTreeNode { Label = "Queues", Kind = EntityKind.Queue };
            foreach (var q in queues.OrderBy(q => q.Name, StringComparer.OrdinalIgnoreCase))
                AddQueueNode(queueRootModel, q);
            SortChildrenFoldersFirst(queueRootModel);

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var queueRootVm = Nodes.FirstOrDefault(n => n.Label == "Queues");
                if (queueRootVm == null)
                {
                    queueRootVm = new EntityTreeNodeViewModel(new EntityTreeNode { Label = "Queues", Kind = EntityKind.Queue })
                    {
                        IsExpanded = true
                    };
                    Nodes.Insert(0, queueRootVm);
                }

                queueRootVm.Children.Clear();
                foreach (var child in queueRootModel.Children)
                    queueRootVm.Children.Add(new EntityTreeNodeViewModel(child));

                if (!string.IsNullOrWhiteSpace(selectedQueueName))
                {
                    var selected = FindQueueNode(queueRootVm.Children, selectedQueueName);
                    if (selected != null)
                        SelectedNode = selected;
                }

                UpdateStatusTextFromCurrentNodes();
                StatusText = "Queues refreshed.";
            });
        }

        private static bool TryReplaceQueueNode(
            IEnumerable<EntityTreeNodeViewModel> nodes,
            string queueName,
            EntityTreeNodeViewModel replacement)
        {
            foreach (var node in nodes)
            {
                var index = IndexOfQueue(node.Children, queueName);
                if (index >= 0)
                {
                    node.Children[index] = replacement;
                    return true;
                }

                if (TryReplaceQueueNode(node.Children, queueName, replacement))
                    return true;
            }

            return false;
        }

        private static int IndexOfQueue(ObservableCollection<EntityTreeNodeViewModel> nodes, string queueName)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                var entity = nodes[i].Entity;
                if (entity?.Kind == EntityKind.Queue
                    && entity.Name.Equals(queueName, StringComparison.OrdinalIgnoreCase))
                    return i;
            }

            return -1;
        }

        private static EntityTreeNodeViewModel? FindQueueNode(
            IEnumerable<EntityTreeNodeViewModel> nodes,
            string queueName)
        {
            foreach (var node in nodes)
            {
                if (node.Entity?.Kind == EntityKind.Queue
                    && node.Entity.Name.Equals(queueName, StringComparison.OrdinalIgnoreCase))
                    return node;

                var nested = FindQueueNode(node.Children, queueName);
                if (nested != null)
                    return nested;
            }

            return null;
        }

        private void UpdateStatusTextFromCurrentNodes()
        {
            var queueRoot = Nodes.FirstOrDefault(n => n.Label == "Queues");
            var topicRoot = Nodes.FirstOrDefault(n => n.Label == "Topics");

            var queueCount = queueRoot != null ? CountEntities(queueRoot, EntityKind.Queue) : 0;
            var topicCount = topicRoot != null ? CountEntities(topicRoot, EntityKind.Topic) : 0;

            StatusText = $"{queueCount} queue(s), {topicCount} topic(s)";
        }

        private static int CountEntities(EntityTreeNodeViewModel node, EntityKind kind)
        {
            var self = node.Entity?.Kind == kind ? 1 : 0;
            if (node.Children.Count == 0)
                return self;

            var children = 0;
            foreach (var child in node.Children)
                children += CountEntities(child, kind);

            return self + children;
        }

        private static string LeafLabel(string path)
        {
            var parts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length == 0 ? path : parts[parts.Length - 1];
        }

        private static void AddQueueNode(EntityTreeNode rootQueueNode, EntityInfo queue)
        {
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

            var segments = queue.Name.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
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
                    c.Entity == null && c.Label.Equals(segment, StringComparison.OrdinalIgnoreCase));

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
    }
}

