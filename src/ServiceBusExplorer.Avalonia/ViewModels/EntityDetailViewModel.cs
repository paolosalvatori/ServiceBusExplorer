using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
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
    /// Shows live details and message counts for the selected entity.
    /// Also exposes send / peek / receive / dead-letter operations.
    /// </summary>
    public partial class EntityDetailViewModel : ViewModelBase
    {
        private readonly INamespaceService  _namespace;
        private readonly IMessagingService  _messaging;

        // ── Selected entity ───────────────────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanPeekMessages))]
        [NotifyPropertyChangedFor(nameof(CanSendToEntity))]
        [NotifyPropertyChangedFor(nameof(IsQueueEntity))]
        [NotifyPropertyChangedFor(nameof(CanPurgeQueue))]
        [NotifyPropertyChangedFor(nameof(EntityTitle))]
        [NotifyPropertyChangedFor(nameof(HasEntity))]
        [NotifyCanExecuteChangedFor(nameof(TogglePeekOptionsPanelCommand))]
        [NotifyCanExecuteChangedFor(nameof(ToggleDlPeekOptionsPanelCommand))]
        [NotifyCanExecuteChangedFor(nameof(ExecutePeekCommand))]
        [NotifyCanExecuteChangedFor(nameof(ExecuteDlPeekCommand))]
        [NotifyCanExecuteChangedFor(nameof(SendMessageCommand))]
        [NotifyCanExecuteChangedFor(nameof(RefreshSelectedEntityCommand))]
        [NotifyCanExecuteChangedFor(nameof(SaveQueueSettingsCommand))]
        [NotifyCanExecuteChangedFor(nameof(OpenQueuePickerCommand))]
        [NotifyCanExecuteChangedFor(nameof(PurgeQueueCommand))]
        [NotifyCanExecuteChangedFor(nameof(PurgeDeadLetterQueueCommand))]
        private EntityInfo? _entity;

        public bool   HasEntity       => Entity != null;
        public bool   CanPeekMessages => Entity?.Kind is EntityKind.Queue or EntityKind.Subscription;
        public bool   CanSendToEntity => Entity?.Kind is EntityKind.Queue or EntityKind.Topic;
        public bool   IsQueueEntity   => Entity?.Kind == EntityKind.Queue;
        public bool   CanPurgeQueue   => IsQueueEntity && !IsBusy;
        public string EntityTitle     => Entity?.DisplayPath ?? "No entity selected";

        // ── Message list ──────────────────────────────────────────────────────────────────

        public ObservableCollection<ServiceBusMessageData> Messages { get; } = new();
        [ObservableProperty] private ServiceBusMessageData? _selectedMessage;
        [ObservableProperty] private string _messageBody = string.Empty;

        public string ShownMessagesLabel => $"Shown messages: {Messages.Count}";

        partial void OnSelectedMessageChanged(ServiceBusMessageData? value)
            => MessageBody = BuildMessageDetails(value);

        // ── Dead-letter message list ──────────────────────────────────────────────────────

        public ObservableCollection<ServiceBusMessageData> DeadLetterMessages { get; } = new();
        [ObservableProperty] private ServiceBusMessageData? _selectedDeadLetterMessage;
        [ObservableProperty] private string _deadLetterMessageBody = string.Empty;

        public string ShownDeadLetterMessagesLabel => $"Shown messages: {DeadLetterMessages.Count}";

        partial void OnSelectedDeadLetterMessageChanged(ServiceBusMessageData? value)
            => DeadLetterMessageBody = BuildMessageDetails(value);

        // ── Overview / output ─────────────────────────────────────────────────────────────

        public ObservableCollection<PropertyRow> OverviewProperties { get; } = new();
        public ObservableCollection<LogEntry>    LogEntries          { get; } = new();

        public bool HasOverviewProperties => OverviewProperties.Count > 0;

        // ── Status ────────────────────────────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(TogglePeekOptionsPanelCommand))]
        [NotifyCanExecuteChangedFor(nameof(ToggleDlPeekOptionsPanelCommand))]
        [NotifyCanExecuteChangedFor(nameof(ExecutePeekCommand))]
        [NotifyCanExecuteChangedFor(nameof(ExecuteDlPeekCommand))]
        [NotifyCanExecuteChangedFor(nameof(SendMessageCommand))]
        [NotifyCanExecuteChangedFor(nameof(RefreshSelectedEntityCommand))]
        [NotifyCanExecuteChangedFor(nameof(SaveQueueSettingsCommand))]
        [NotifyCanExecuteChangedFor(nameof(OpenQueuePickerCommand))]
        [NotifyCanExecuteChangedFor(nameof(PurgeQueueCommand))]
        [NotifyCanExecuteChangedFor(nameof(PurgeDeadLetterQueueCommand))]
        private bool _isBusy = false;

        [ObservableProperty] private string _status   = string.Empty;

        // ── Send panel ─────────────────────────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SendMessageCommand))]
        private string _sendBody = string.Empty;

        [ObservableProperty] private string _sendMessageId  = string.Empty;
        [ObservableProperty] private string _sendCorrelationId = string.Empty;
        [ObservableProperty] private string _sendMetadata   = string.Empty;
        [ObservableProperty] private int    _sendCount      = 1;
        [ObservableProperty] private bool   _showSendPanel  = false;

        // ── Tab selection  (0 = Overview | 1 = Settings | 2 = Messages | 3 = Dead-letter | 4 = Output) ──
        [ObservableProperty] private int _selectedTabIndex = 0;

        // ── Queue settings (editable) ──────────────────────────────────────────────────────

        [ObservableProperty] private string _queueForwardToInput = string.Empty;
        [ObservableProperty] private string _queueForwardDeadLetterToInput = string.Empty;

        public ObservableCollection<string> QueueSelectionOptions { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ApplySelectedQueueCommand))]
        private string? _selectedQueueOption;

        [ObservableProperty] private bool _isQueuePickerOpen;
        [ObservableProperty] private string _queuePickerTarget = "ForwardTo";

        public string QueuePickerTitle => QueuePickerTarget == "ForwardDeadLetteredMessagesTo"
            ? "Select destination queue for Forward dead-letter to"
            : "Select destination queue for Forward to";

        // ── Peek options (shared between Messages and Dead-letter tabs) ───────────────────

        [ObservableProperty] private bool _showPeekOptionsPanel   = false;
        [ObservableProperty] private bool _showDlPeekOptionsPanel = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPeekModeNonDestructive))]
        [NotifyPropertyChangedFor(nameof(IsPeekModeDestructive))]
        private PeekReceiveMode _peekReceiveMode = PeekReceiveMode.Peek;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCountAll))]
        [NotifyPropertyChangedFor(nameof(IsCountTop))]
        [NotifyPropertyChangedFor(nameof(IsCountLast))]
        [NotifyPropertyChangedFor(nameof(ShowPeekCountInput))]
        private PeekCountSelection _peekCountSelection = PeekCountSelection.TopX;

        [ObservableProperty] private decimal _peekCountValue = 50m;

        // NumericUpDown exposes decimal values; normalize once for fetch operations.
        private int PeekCount => Math.Max(1, decimal.ToInt32(decimal.Truncate(PeekCountValue)));

        // Radio-button helpers — receive mode
        public bool IsPeekModeNonDestructive
        {
            get => PeekReceiveMode == PeekReceiveMode.Peek;
            set { if (value) PeekReceiveMode = PeekReceiveMode.Peek; }
        }
        public bool IsPeekModeDestructive
        {
            get => PeekReceiveMode == PeekReceiveMode.PeekAndDelete;
            set { if (value) PeekReceiveMode = PeekReceiveMode.PeekAndDelete; }
        }

        // Radio-button helpers — count selection
        public bool IsCountAll
        {
            get => PeekCountSelection == PeekCountSelection.All;
            set { if (value) PeekCountSelection = PeekCountSelection.All; }
        }
        public bool IsCountTop
        {
            get => PeekCountSelection == PeekCountSelection.TopX;
            set { if (value) PeekCountSelection = PeekCountSelection.TopX; }
        }
        public bool IsCountLast
        {
            get => PeekCountSelection == PeekCountSelection.LastX;
            set { if (value) PeekCountSelection = PeekCountSelection.LastX; }
        }

        /// <summary>Show the numeric count input when Top or Last is selected.</summary>
        public bool ShowPeekCountInput => PeekCountSelection != PeekCountSelection.All;

        // ── Constructor ───────────────────────────────────────────────────────────────────

        public EntityDetailViewModel(INamespaceService ns, IMessagingService messaging)
        {
            _namespace = ns;
            _messaging = messaging;

            OverviewProperties.CollectionChanged += (_, __) => OnPropertyChanged(nameof(HasOverviewProperties));
            Messages.CollectionChanged += (_, __) => OnPropertyChanged(nameof(ShownMessagesLabel));
            DeadLetterMessages.CollectionChanged += (_, __) => OnPropertyChanged(nameof(ShownDeadLetterMessagesLabel));
        }

        // ── Load ─────────────────────────────────────────────────────────────────────────

        public async Task LoadEntityAsync(EntityInfo entity, CancellationToken ct = default)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                Entity      = entity;
                IsBusy        = true;
                Status        = "Refreshing…";
                ShowSendPanel = false;
                IsQueuePickerOpen = false;
                Messages.Clear();
                DeadLetterMessages.Clear();
                OverviewProperties.Clear();
                SelectedMessage           = null;
                SelectedDeadLetterMessage = null;
                MessageBody            = string.Empty;
                DeadLetterMessageBody  = string.Empty;
            });

            try
            {
                var refreshed = entity;
                List<PropertyRow> overview;
                QueueProperties? queueProps = null;

                switch (entity.Kind)
                {
                    case EntityKind.Queue:
                        refreshed = await _namespace.GetQueueAsync(entity.Name, ct).ConfigureAwait(false);
                        queueProps = await _namespace.GetQueuePropertiesAsync(entity.Name, ct).ConfigureAwait(false);
                        overview  = BuildQueueOverview(
                            refreshed,
                            queueProps);
                        break;

                    case EntityKind.Topic:
                        refreshed = await _namespace.GetTopicAsync(entity.Name, ct).ConfigureAwait(false);
                        overview  = BuildTopicOverview(
                            refreshed,
                            await _namespace.GetTopicPropertiesAsync(entity.Name, ct).ConfigureAwait(false));
                        break;

                    case EntityKind.Subscription:
                        overview = BuildSubscriptionOverview(
                            entity,
                            await _namespace.GetSubscriptionPropertiesAsync(entity.TopicPath!, entity.Name, ct).ConfigureAwait(false));
                        break;

                    default:
                        overview = BuildBasicOverview(entity);
                        break;
                }

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Entity = refreshed;
                    OverviewProperties.Clear();
                    foreach (var row in overview)
                        OverviewProperties.Add(row);

                    if (queueProps != null)
                    {
                        QueueForwardToInput = queueProps.ForwardTo ?? string.Empty;
                        QueueForwardDeadLetterToInput = queueProps.ForwardDeadLetteredMessagesTo ?? string.Empty;
                    }
                    else
                    {
                        QueueForwardToInput = string.Empty;
                        QueueForwardDeadLetterToInput = string.Empty;
                    }

                    Status = BuildStatusText(refreshed);
                });

                AppendLog(LogLevel.Success, $"Loaded {refreshed.DisplayPath}.");
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() => Status = $"Error: {ex.Message}");
                AppendLog(LogLevel.Error, $"Failed to load {entity.DisplayPath}: {ex.Message}");
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsBusy = false);
            }
        }

        // ── Commands ──────────────────────────────────────────────────────────────────────

        [RelayCommand(CanExecute = nameof(HasEntity))]
        private Task RefreshSelectedEntity()
            => Entity != null ? LoadEntityAsync(Entity) : Task.CompletedTask;

        [RelayCommand(CanExecute = nameof(IsQueueEntity))]
        private async Task SaveQueueSettings()
        {
            if (Entity?.Kind != EntityKind.Queue)
                return;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsBusy = true;
                Status = "Saving queue settings…";
            });

            try
            {
                var props = await _namespace.GetQueuePropertiesAsync(Entity.Name).ConfigureAwait(false);
                props.ForwardTo = NormalizeQueueName(QueueForwardToInput);
                props.ForwardDeadLetteredMessagesTo = NormalizeQueueName(QueueForwardDeadLetterToInput);

                var updated = await _namespace.UpdateQueueAsync(props).ConfigureAwait(false);
                var updatedProps = await _namespace.GetQueuePropertiesAsync(Entity.Name).ConfigureAwait(false);
                var overview = BuildQueueOverview(updated, updatedProps);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Entity = updated;
                    QueueForwardToInput = updatedProps.ForwardTo ?? string.Empty;
                    QueueForwardDeadLetterToInput = updatedProps.ForwardDeadLetteredMessagesTo ?? string.Empty;

                    OverviewProperties.Clear();
                    foreach (var row in overview)
                        OverviewProperties.Add(row);

                    Status = "Queue settings saved.";
                });

                AppendLog(LogLevel.Success, $"Updated forwarding settings for {updated.DisplayPath}.");
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() => Status = $"Save failed: {ex.Message}");
                AppendLog(LogLevel.Error, $"Failed to update queue settings for {Entity.DisplayPath}: {ex.Message}");
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsBusy = false);
            }
        }

        [RelayCommand(CanExecute = nameof(CanPurgeQueue))]
        private async Task PurgeQueue()
        {
            if (Entity?.Kind != EntityKind.Queue)
                return;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsBusy = true;
                Status = "Purging queue…";
            });

            try
            {
                await _messaging.PurgeQueueAsync(Entity.Name).ConfigureAwait(false);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Messages.Clear();
                    SelectedMessage = null;
                    MessageBody = string.Empty;
                    Status = "Queue purge completed.";
                });

                AppendLog(LogLevel.Warning, $"Purged queue {Entity.DisplayPath}.");
                await LoadEntityAsync(Entity).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() => Status = $"Purge failed: {ex.Message}");
                AppendLog(LogLevel.Error, $"Queue purge failed for {Entity.DisplayPath}: {ex.Message}");
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsBusy = false);
            }
        }

        [RelayCommand(CanExecute = nameof(CanPurgeQueue))]
        private async Task PurgeDeadLetterQueue()
        {
            if (Entity?.Kind != EntityKind.Queue)
                return;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsBusy = true;
                Status = "Purging dead-letter queue…";
            });

            try
            {
                await _messaging.PurgeDeadLetterQueueAsync(Entity.Name).ConfigureAwait(false);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    DeadLetterMessages.Clear();
                    SelectedDeadLetterMessage = null;
                    DeadLetterMessageBody = string.Empty;
                    Status = "Dead-letter queue purge completed.";
                });

                AppendLog(LogLevel.Warning, $"Purged dead-letter queue for {Entity.DisplayPath}.");
                await LoadEntityAsync(Entity).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() => Status = $"DLQ purge failed: {ex.Message}");
                AppendLog(LogLevel.Error, $"Dead-letter queue purge failed for {Entity.DisplayPath}: {ex.Message}");
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsBusy = false);
            }
        }

        [RelayCommand(CanExecute = nameof(IsQueueEntity))]
        private async Task OpenQueuePicker(string target)
        {
            if (Entity?.Kind != EntityKind.Queue)
                return;

            await EnsureQueueSelectionOptionsLoaded().ConfigureAwait(false);

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                QueuePickerTarget = target;
                OnPropertyChanged(nameof(QueuePickerTitle));
                SelectedQueueOption = target == "ForwardDeadLetteredMessagesTo"
                    ? NormalizeQueueName(QueueForwardDeadLetterToInput)
                    : NormalizeQueueName(QueueForwardToInput);
                IsQueuePickerOpen = true;
            });
        }

        [RelayCommand]
        private void CancelQueuePicker() => IsQueuePickerOpen = false;

        [RelayCommand(CanExecute = nameof(CanApplySelectedQueue))]
        private void ApplySelectedQueue()
        {
            if (string.IsNullOrWhiteSpace(SelectedQueueOption))
                return;

            if (QueuePickerTarget == "ForwardDeadLetteredMessagesTo")
                QueueForwardDeadLetterToInput = SelectedQueueOption;
            else
                QueueForwardToInput = SelectedQueueOption;

            IsQueuePickerOpen = false;
        }

        private bool CanApplySelectedQueue() => !string.IsNullOrWhiteSpace(SelectedQueueOption);

        private async Task EnsureQueueSelectionOptionsLoaded()
        {
            if (QueueSelectionOptions.Count > 0)
                return;

            var queues = await _namespace.GetQueuesAsync().ConfigureAwait(false);

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                QueueSelectionOptions.Clear();
                foreach (var q in queues)
                {
                    if (!string.Equals(q.Name, Entity?.Name, StringComparison.OrdinalIgnoreCase))
                        QueueSelectionOptions.Add(q.Name);
                }
            });
        }

        private static string? NormalizeQueueName(string? value)
            => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        // ── Peek options panels ───────────────────────────────────────────────────────────

        [RelayCommand(CanExecute = nameof(CanPeekMessages))]
        private void TogglePeekOptionsPanel()
        {
            ShowPeekOptionsPanel   = !ShowPeekOptionsPanel;
            ShowDlPeekOptionsPanel = false; // close the other
        }

        [RelayCommand(CanExecute = nameof(CanPeekMessages))]
        private void ToggleDlPeekOptionsPanel()
        {
            ShowDlPeekOptionsPanel = !ShowDlPeekOptionsPanel;
            ShowPeekOptionsPanel   = false;
        }

        // ── Execute peek ─────────────────────────────────────────────────────────────────

        [RelayCommand(CanExecute = nameof(CanPeekMessages))]
        private async Task ExecutePeek()
        {
            if (Entity == null) return;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsBusy = true;
                Status = PeekReceiveMode == PeekReceiveMode.PeekAndDelete
                    ? "Receiving (and deleting)…" : "Peeking…";
                Messages.Clear();
                SelectedMessage = null;
                MessageBody     = string.Empty;
                ShowPeekOptionsPanel = false;
            });

            try
            {
                var path  = GetPeekEntityPath(Entity);
                var msgs  = await FetchActive(path).ConfigureAwait(false);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    foreach (var m in msgs) Messages.Add(m);
                    SelectedMessage = null;   // user picks from list
                    Status = $"Fetched {msgs.Count} message(s).";
                });

                AppendLog(LogLevel.Info,
                    $"{ReceiveModeLabel()} {msgs.Count} message(s) from {Entity.DisplayPath}.");
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() => Status = $"Peek failed: {ex.Message}");
                AppendLog(LogLevel.Error, $"Peek failed for {Entity.DisplayPath}: {ex.Message}");
            }
            finally { await Dispatcher.UIThread.InvokeAsync(() => IsBusy = false); }
        }

        [RelayCommand(CanExecute = nameof(CanPeekMessages))]
        private async Task ExecuteDlPeek()
        {
            if (Entity == null) return;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsBusy = true;
                Status = PeekReceiveMode == PeekReceiveMode.PeekAndDelete
                    ? "Receiving dead-letter (and deleting)…" : "Peeking dead-letter…";
                DeadLetterMessages.Clear();
                SelectedDeadLetterMessage = null;
                DeadLetterMessageBody     = string.Empty;
                ShowDlPeekOptionsPanel    = false;
            });

            try
            {
                var path  = GetPeekEntityPath(Entity);
                var msgs  = await FetchDeadLetter(path).ConfigureAwait(false);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    foreach (var m in msgs) DeadLetterMessages.Add(m);
                    SelectedDeadLetterMessage = null;   // user picks from list
                    Status = $"Fetched {msgs.Count} dead-letter message(s).";
                });

                AppendLog(LogLevel.Warning,
                    $"{ReceiveModeLabel()} {msgs.Count} dead-letter message(s) from {Entity.DisplayPath}.");
            }
            catch (Exception ex)
            {
                var friendly = BuildDeadLetterPeekError(ex.Message);
                await Dispatcher.UIThread.InvokeAsync(() => Status = friendly);
                AppendLog(
                    ex.Message.IndexOf("auto-forwarding enabled", StringComparison.OrdinalIgnoreCase) >= 0
                        ? LogLevel.Warning
                        : LogLevel.Error,
                    $"Dead-letter peek failed for {Entity.DisplayPath}: {friendly}");
            }
            finally { await Dispatcher.UIThread.InvokeAsync(() => IsBusy = false); }
        }

        // ── Fetch helpers (respect options) ───────────────────────────────────────────────

        private async Task<IReadOnlyList<ServiceBusMessageData>> FetchActive(string path)
        {
            int count = ResolvedFetchCount();
            if (PeekReceiveMode == PeekReceiveMode.PeekAndDelete)
                return Slice(await _messaging.ReceiveMessagesAsync(path, count).ConfigureAwait(false));
            return Slice(await _messaging.PeekMessagesAsync(path, count).ConfigureAwait(false));
        }

        private async Task<IReadOnlyList<ServiceBusMessageData>> FetchDeadLetter(string path)
        {
            int count = ResolvedFetchCount();
            if (PeekReceiveMode == PeekReceiveMode.PeekAndDelete)
                return Slice(await _messaging.ReceiveDeadLetterMessagesAsync(path, count).ConfigureAwait(false));
            return Slice(await _messaging.PeekDeadLetterMessagesAsync(path, count).ConfigureAwait(false));
        }

        /// <summary>
        /// Converts the user's count settings into an actual fetch size.
        /// For LastX we over-fetch so we can take the tail client-side.
        /// </summary>
        private int ResolvedFetchCount()
        {
            if (PeekCountSelection == PeekCountSelection.All)  return 1000;
            if (PeekCountSelection == PeekCountSelection.LastX) return Math.Max(1000, PeekCount * 3);
            return Math.Max(1, PeekCount);
        }

        /// <summary>Applies LastX slicing after a fetch (All and TopX are no-ops here).</summary>
        private IReadOnlyList<ServiceBusMessageData> Slice(IReadOnlyList<ServiceBusMessageData> msgs)
        {
            if (PeekCountSelection == PeekCountSelection.LastX && msgs.Count > PeekCount)
            {
                var result = new List<ServiceBusMessageData>(PeekCount);
                for (int i = msgs.Count - PeekCount; i < msgs.Count; i++) result.Add(msgs[i]);
                return result;
            }
            return msgs;
        }

        private string ReceiveModeLabel() =>
            PeekReceiveMode == PeekReceiveMode.PeekAndDelete ? "Received & deleted" : "Peeked";

        [RelayCommand(CanExecute = nameof(CanSend))]
        private async Task SendMessage()
        {
            if (Entity == null || !CanSendToEntity) return;

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsBusy = true;
                Status = "Sending…";
            });

            try
            {
                var properties = ParseMetadata(SendMetadata);
                var correlationId = string.IsNullOrWhiteSpace(SendCorrelationId)
                    ? TakeMetadataString(properties, nameof(ServiceBusMessageData.CorrelationId))
                    : SendCorrelationId.Trim();
                var tasks = new List<Task>();
                for (int i = 0; i < Math.Max(1, SendCount); i++)
                {
                    tasks.Add(_messaging.SendMessageAsync(Entity.Name, new ServiceBusMessageData
                    {
                        Body      = SendBody,
                        MessageId = string.IsNullOrWhiteSpace(SendMessageId)
                            ? Guid.NewGuid().ToString()
                            : SendCount > 1 ? $"{SendMessageId}-{i + 1}" : SendMessageId,
                        CorrelationId = correlationId,
                        Properties = new Dictionary<string, object>(properties)
                    }));
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Status = $"Sent {tasks.Count} message(s) to {Entity.Name}.";
                    ShowSendPanel = false;
                });

                AppendLog(LogLevel.Success, $"Sent {tasks.Count} message(s) to {Entity.DisplayPath}.");
            }
            catch (Exception ex)
            {
                await Dispatcher.UIThread.InvokeAsync(() => Status = $"Send failed: {ex.Message}");
                AppendLog(LogLevel.Error, $"Send failed for {Entity.DisplayPath}: {ex.Message}");
            }
            finally
            {
                await Dispatcher.UIThread.InvokeAsync(() => IsBusy = false);
            }
        }

        private bool CanSend() => CanSendToEntity && !string.IsNullOrWhiteSpace(SendBody) && !IsBusy;

        private static string BuildMessageDetails(ServiceBusMessageData? message)
        {
            if (message == null)
                return string.Empty;

            var builder = new StringBuilder();
            builder.AppendLine("System Properties:");
            builder.AppendLine($"MessageId: {ValueOrNone(message.MessageId)}");
            builder.AppendLine($"CorrelationId: {ValueOrNone(message.CorrelationId)}");
            builder.AppendLine($"SessionId: {ValueOrNone(message.SessionId)}");
            builder.AppendLine($"Label: {ValueOrNone(message.Label)}");
            builder.AppendLine($"ContentType: {ValueOrNone(message.ContentType)}");
            builder.AppendLine($"SequenceNumber: {message.SequenceNumber}");
            builder.AppendLine($"DeliveryCount: {message.DeliveryCount}");
            builder.AppendLine($"EnqueuedTime: {ValueOrNone(message.EnqueuedTime)}");
            builder.AppendLine($"ExpiresAtUtc: {ValueOrNone(message.ExpiresAtUtc)}");
            if (!string.IsNullOrWhiteSpace(message.DeadLetterReason))
                builder.AppendLine($"DeadLetterReason: {message.DeadLetterReason}");

            builder.AppendLine();
            builder.AppendLine("Metadata:");
            if (message.Properties.Count == 0)
            {
                builder.AppendLine("(none)");
            }
            else
            {
                foreach (var property in message.Properties)
                    builder.AppendLine($"{property.Key}: {property.Value}");
            }

            builder.AppendLine();
            builder.AppendLine("Body:");
            builder.Append(message.Body ?? string.Empty);
            return builder.ToString();
        }

        private static string? TakeMetadataString(Dictionary<string, object> metadata, string key)
        {
            string? matchingKey = null;
            foreach (var property in metadata)
            {
                if (string.Equals(property.Key, key, StringComparison.OrdinalIgnoreCase))
                {
                    matchingKey = property.Key;
                    break;
                }
            }

            if (matchingKey == null)
                return null;

            var value = Convert.ToString(metadata[matchingKey]);
            metadata.Remove(matchingKey);
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private static string ValueOrNone(object? value)
            => value == null || string.IsNullOrWhiteSpace(Convert.ToString(value))
                ? "(none)"
                : Convert.ToString(value)!;

        private static Dictionary<string, object> ParseMetadata(string metadata)
        {
            var result = new Dictionary<string, object>();
            if (string.IsNullOrWhiteSpace(metadata))
                return result;

            var trimmed = metadata.Trim();
            if (trimmed.StartsWith("{", StringComparison.Ordinal))
            {
                using var document = JsonDocument.Parse(trimmed);
                if (document.RootElement.ValueKind != JsonValueKind.Object)
                    throw new FormatException("Metadata JSON must be an object.");

                foreach (var property in document.RootElement.EnumerateObject())
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                        continue;

                    result[property.Name] = ConvertMetadataValue(property.Value);
                }

                return result;
            }

            var lines = metadata.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var index = line.IndexOf('=');
                if (index <= 0)
                    throw new FormatException("Metadata lines must use key=value format, or provide a JSON object.");

                var key = line.Substring(0, index).Trim();
                if (key.Length == 0)
                    throw new FormatException("Metadata keys cannot be empty.");

                result[key] = line.Substring(index + 1).Trim();
            }

            return result;
        }

        private static object ConvertMetadataValue(JsonElement value)
        {
            switch (value.ValueKind)
            {
                case JsonValueKind.String:
                    return value.GetString() ?? string.Empty;
                case JsonValueKind.Number:
                    if (value.TryGetInt64(out var integer))
                        return integer;
                    if (value.TryGetDecimal(out var dec))
                        return dec;
                    return value.GetDouble();
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                default:
                    throw new FormatException("Metadata values must be strings, numbers, booleans, or null.");
            }
        }

        [RelayCommand]
        private void ToggleSendPanel()
        {
            ShowSendPanel = !ShowSendPanel;
            if (ShowSendPanel)
                SelectedTabIndex = 2; // Messages tab
        }

        [RelayCommand]
        private void ClearLog() => LogEntries.Clear();

        private static string GetPeekEntityPath(EntityInfo entity)
            => entity.Kind == EntityKind.Subscription
                ? $"{entity.TopicPath}/Subscriptions/{entity.Name}"
                : entity.Name;

        private static string BuildStatusText(EntityInfo entity)
            => entity.Kind switch
            {
                EntityKind.Topic => $"Scheduled: {entity.ScheduledMessageCount}",
                _ => $"Active: {entity.ActiveMessageCount}  DL: {entity.DeadLetterMessageCount}  Scheduled: {entity.ScheduledMessageCount}"
            };

        private static string BuildDeadLetterPeekError(string message)
        {
            if (message.IndexOf("auto-forwarding enabled", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "Dead-letter peek is not available for this entity because auto-forwarding is enabled. "
                     + "This is expected in Azure Service Bus for some forwarded entities.";
            }

            return $"Dead-letter peek failed: {message}";
        }

        private static List<PropertyRow> BuildQueueOverview(EntityInfo runtime, QueueProperties props)
            => new List<PropertyRow>
            {
                new("Name", props.Name),
                new("Status", props.Status.ToString()),
                new("Active messages", runtime.ActiveMessageCount.ToString()),
                new("Dead-letter messages", runtime.DeadLetterMessageCount.ToString()),
                new("Scheduled messages", runtime.ScheduledMessageCount.ToString()),
                new("Transfer messages", runtime.TransferMessageCount.ToString()),
                new("Transfer DLQ messages", runtime.TransferDeadLetterMessageCount.ToString()),
                new("Total messages", runtime.TotalMessageCount.ToString()),
                new("Max size (MB)", props.MaxSizeInMegabytes.ToString()),
                new("Max delivery count", props.MaxDeliveryCount.ToString()),
                new("Default TTL", FormatTimeSpan(props.DefaultMessageTimeToLive)),
                new("Lock duration", FormatTimeSpan(props.LockDuration)),
                new("Auto-delete on idle", FormatTimeSpan(props.AutoDeleteOnIdle)),
                new("Requires duplicate detection", FormatBool(props.RequiresDuplicateDetection)),
                new("Duplicate detection window", FormatTimeSpan(props.DuplicateDetectionHistoryTimeWindow)),
                new("Requires session", FormatBool(props.RequiresSession)),
                new("Dead-letter on expiration", FormatBool(props.EnableDeadLetteringOnMessageExpiration)),
                new("Enable batched operations", FormatBool(props.EnableBatchedOperations)),
                new("Forward to", FormatString(props.ForwardTo)),
                new("Forward dead-letter to", FormatString(props.ForwardDeadLetteredMessagesTo))
            };

        private static List<PropertyRow> BuildTopicOverview(EntityInfo runtime, TopicProperties props)
            => new List<PropertyRow>
            {
                new("Name", props.Name),
                new("Status", props.Status.ToString()),
                new("Scheduled messages", runtime.ScheduledMessageCount.ToString()),
                new("Max size (MB)", props.MaxSizeInMegabytes.ToString()),
                new("Default TTL", FormatTimeSpan(props.DefaultMessageTimeToLive)),
                new("Auto-delete on idle", FormatTimeSpan(props.AutoDeleteOnIdle)),
                new("Requires duplicate detection", FormatBool(props.RequiresDuplicateDetection)),
                new("Duplicate detection window", FormatTimeSpan(props.DuplicateDetectionHistoryTimeWindow)),
                new("Enable batched operations", FormatBool(props.EnableBatchedOperations)),
                new("Support ordering", FormatBool(props.SupportOrdering))
            };

        private static List<PropertyRow> BuildSubscriptionOverview(EntityInfo runtime, SubscriptionProperties props)
            => new List<PropertyRow>
            {
                new("Topic", FormatString(props.TopicPath)),
                new("Subscription", props.Name),
                new("Status", props.Status.ToString()),
                new("Active messages", runtime.ActiveMessageCount.ToString()),
                new("Dead-letter messages", runtime.DeadLetterMessageCount.ToString()),
                new("Transfer messages", runtime.TransferMessageCount.ToString()),
                new("Transfer DLQ messages", runtime.TransferDeadLetterMessageCount.ToString()),
                new("Total messages", runtime.TotalMessageCount.ToString()),
                new("Max delivery count", props.MaxDeliveryCount.ToString()),
                new("Default TTL", FormatTimeSpan(props.DefaultMessageTimeToLive)),
                new("Lock duration", FormatTimeSpan(props.LockDuration)),
                new("Auto-delete on idle", FormatTimeSpan(props.AutoDeleteOnIdle)),
                new("Requires session", FormatBool(props.RequiresSession)),
                new("Dead-letter on expiration", FormatBool(props.EnableDeadLetteringOnMessageExpiration)),
                new("Dead-letter on filter evaluation exception", FormatBool(props.EnableDeadLetteringOnFilterEvaluationException)),
                new("Enable batched operations", FormatBool(props.EnableBatchedOperations)),
                new("Forward to", FormatString(props.ForwardTo)),
                new("Forward dead-letter to", FormatString(props.ForwardDeadLetteredMessagesTo))
            };

        private static List<PropertyRow> BuildBasicOverview(EntityInfo entity)
            => new List<PropertyRow>
            {
                new("Name", entity.Name),
                new("Path", entity.DisplayPath),
                new("Kind", entity.Kind.ToString()),
                new("Total messages", entity.TotalMessageCount.ToString())
            };

        private static string FormatBool(bool value) => value ? "Yes" : "No";
        private static string FormatString(string? value) => string.IsNullOrWhiteSpace(value) ? "—" : value;
        private static string FormatTimeSpan(TimeSpan value) => value == TimeSpan.MaxValue ? "Never" : value.ToString();

        private void AppendLog(LogLevel level, string message)
        {
            void Add() => LogEntries.Insert(0, new LogEntry { Level = level, Message = message });

            if (Dispatcher.UIThread.CheckAccess())
                Add();
            else
                Dispatcher.UIThread.Post(Add);
        }
    }
}
