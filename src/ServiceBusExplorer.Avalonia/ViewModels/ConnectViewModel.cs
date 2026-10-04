using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceBusExplorer.Core.Abstractions;
using ServiceBusExplorer.Core.Models;
using ServiceBusExplorer.Core.Services;

namespace ServiceBusExplorer.Avalonia.ViewModels
{
    /// <summary>
    /// ViewModel for the "Connect to Service Bus Namespace" dialog (ConnectDialog).
    ///
    /// Mirrors and replaces the logic in the WinForms ConnectForm:
    ///   • SAS and AAD auth modes
    ///   • Saved-namespace dropdown
    ///   • Transport type (AmqpTcp / AmqpWebSockets – NetMessaging stays in WinForms compat mode)
    ///   • Entity type checkboxes
    ///   • OData filter expressions
    ///   • Optional save-as name
    ///
    /// When <see cref="ConnectCommand"/> succeeds it invokes the constructor callback
    /// and raises <see cref="ConnectionEstablished"/> so the View can close itself.
    /// </summary>
    public partial class ConnectViewModel : ViewModelBase
    {
        private readonly ISavedConnectionsService _savedConnectionsService;
        private readonly Action<ConnectionProfile> _onConnect;

        // ── Auth mode ──────────────────────────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSasMode))]
        [NotifyPropertyChangedFor(nameof(IsAadMode))]
        [NotifyPropertyChangedFor(nameof(IsWindowsMode))]
        private ConnectionAuthMode _authMode = ConnectionAuthMode.Sas;

        /// <summary>Settable from the View's RadioButton binding.</summary>
        public bool IsSasMode
        {
            get => AuthMode == ConnectionAuthMode.Sas;
            set { if (value) AuthMode = ConnectionAuthMode.Sas; }
        }

        public bool IsAadMode
        {
            get => AuthMode == ConnectionAuthMode.AzureActiveDirectory;
            set { if (value) AuthMode = ConnectionAuthMode.AzureActiveDirectory; }
        }

        /// <summary>
        /// Windows auth is labelled "Compatibility Mode" in the Avalonia UI.
        /// It remains fully functional when the connection is handed to the legacy SDK adapter.
        /// </summary>
        public bool IsWindowsMode
        {
            get => AuthMode == ConnectionAuthMode.Windows;
            set { if (value) AuthMode = ConnectionAuthMode.Windows; }
        }

        // ── SAS fields ─────────────────────────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConnectCommand))]
        private string _connectionString = string.Empty;

        // ── AAD / common endpoint fields ───────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConnectCommand))]
        private string _endpoint = string.Empty;

        [ObservableProperty]
        private string _tenantId = string.Empty;

        // ── Transport ──────────────────────────────────────────────────────────────────────

        [ObservableProperty]
        private ConnectionTransportType _transportType = ConnectionTransportType.AmqpTcp;

        /// <summary>Available transport choices shown in the dropdown.</summary>
        public ObservableCollection<ConnectionTransportType> AvailableTransportTypes { get; } =
            new ObservableCollection<ConnectionTransportType>
            {
                ConnectionTransportType.AmqpTcp,
                ConnectionTransportType.AmqpWebSockets
            };

        // ── Entity selection ──────────────────────────────────────────────────────────────

        [ObservableProperty] private bool _selectQueues    = true;
        [ObservableProperty] private bool _selectTopics    = true;
        [ObservableProperty] private bool _selectEventHubs = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotificationHubNameVisible))]
        private bool _selectNotificationHubs = false;

        [ObservableProperty] private bool _selectRelays = false;

        [ObservableProperty] private string _notificationHubName = string.Empty;

        public bool IsNotificationHubNameVisible => SelectNotificationHubs;

        // ── OData filters ─────────────────────────────────────────────────────────────────

        [ObservableProperty] private string _queueFilter        = string.Empty;
        [ObservableProperty] private string _topicFilter        = string.Empty;
        [ObservableProperty] private string _subscriptionFilter = string.Empty;

        // ── Saved connections ─────────────────────────────────────────────────────────────

        [ObservableProperty] private ObservableCollection<ConnectionProfile> _savedConnections = new();

        [ObservableProperty]
        private ConnectionProfile? _selectedConnection;

        partial void OnSelectedConnectionChanged(ConnectionProfile? value)
        {
            if (value == null) return;
            LoadFromProfile(value);
        }

        // ── Save-as ───────────────────────────────────────────────────────────────────────

        [ObservableProperty] private string _saveConnectionName = string.Empty;

        // ── Status ────────────────────────────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ConnectButtonLabel))]
        private string _statusMessage = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ConnectButtonLabel))]
        private bool   _isBusy        = false;

        public string ConnectButtonLabel => IsBusy ? "Connecting…" : "Connect";

        // ── Events ────────────────────────────────────────────────────────────────────────

        /// <summary>Raised after a successful Connect so the View can close the dialog.</summary>
        public event EventHandler? ConnectionEstablished;

        // ── Constructor ───────────────────────────────────────────────────────────────────

        public ConnectViewModel(ISavedConnectionsService savedConnectionsService,
                                Action<ConnectionProfile> onConnect)
        {
            _savedConnectionsService = savedConnectionsService;
            _onConnect               = onConnect;

            // Fire-and-forget initial load (errors are swallowed; list just stays empty)
            _ = LoadSavedConnectionsAsync();
        }

        // ── Commands ──────────────────────────────────────────────────────────────────────

        [RelayCommand(CanExecute = nameof(CanConnect))]
        private async Task Connect()
        {
            if (IsBusy)
                return;

            IsBusy        = true;
            StatusMessage = string.Empty;
            try
            {
                ConnectionProfile profile;
                try
                {
                    profile = BuildConnectionProfile();
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Invalid input: {ex.Message}";
                    return;
                }

                // Optionally persist the connection
                if (!string.IsNullOrWhiteSpace(SaveConnectionName))
                {
                    profile.Name        = SaveConnectionName.Trim();
                    profile.UserCreated = true;
                    await _savedConnectionsService.SaveAsync(profile);
                    await LoadSavedConnectionsAsync();
                }

                _onConnect(profile);
                ConnectionEstablished?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Connection failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanConnect()
        {
            return AuthMode switch
            {
                ConnectionAuthMode.Sas                  => !string.IsNullOrWhiteSpace(ConnectionString)
                                                           || !string.IsNullOrWhiteSpace(Endpoint),
                ConnectionAuthMode.AzureActiveDirectory => !string.IsNullOrWhiteSpace(Endpoint),
                ConnectionAuthMode.Windows              => !string.IsNullOrWhiteSpace(Endpoint),
                _                                       => false
            };
        }

        [RelayCommand]
        private async Task DeleteSelectedConnection()
        {
            if (SelectedConnection?.Name == null) return;
            await _savedConnectionsService.DeleteAsync(SelectedConnection.Name);
            await LoadSavedConnectionsAsync();
            SelectedConnection = null;
        }

        [RelayCommand]
        private void ParseConnectionStringInput()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString)) return;

            var profile = ConnectionStringParser.TryParse(ConnectionString);
            if (profile == null)
            {
                StatusMessage = "Could not parse the connection string. Please check the format.";
                return;
            }

            StatusMessage = string.Empty;
            LoadFromProfile(profile);
        }

        // ── Helpers ───────────────────────────────────────────────────────────────────────

        private async Task LoadSavedConnectionsAsync()
        {
            var all = await _savedConnectionsService.GetAllAsync().ConfigureAwait(false);
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                SavedConnections.Clear();
                foreach (var p in all)
                    SavedConnections.Add(p);
            });
        }

        /// <summary>Populates dialog fields from an existing saved profile.</summary>
        public void LoadFromProfile(ConnectionProfile profile)
        {
            AuthMode      = profile.AuthMode;
            Endpoint      = profile.Endpoint ?? string.Empty;
            TransportType = profile.TransportType;
            TenantId      = profile.TenantId ?? string.Empty;
            QueueFilter   = profile.QueueFilter ?? string.Empty;
            TopicFilter   = profile.TopicFilter ?? string.Empty;
            SubscriptionFilter = profile.SubscriptionFilter ?? string.Empty;
            NotificationHubName = profile.EntityPath ?? string.Empty;

            if (profile.AuthMode == ConnectionAuthMode.Sas)
                ConnectionString = profile.ConnectionString ?? string.Empty;

            var sel = profile.SelectedEntities;
            SelectQueues           = sel.Contains(EntityTypeConstants.Queues,           StringComparer.OrdinalIgnoreCase);
            SelectTopics           = sel.Contains(EntityTypeConstants.Topics,           StringComparer.OrdinalIgnoreCase);
            SelectEventHubs        = sel.Contains(EntityTypeConstants.EventHubs,        StringComparer.OrdinalIgnoreCase);
            SelectNotificationHubs = sel.Contains(EntityTypeConstants.NotificationHubs, StringComparer.OrdinalIgnoreCase);
            SelectRelays           = sel.Contains(EntityTypeConstants.Relays,           StringComparer.OrdinalIgnoreCase);
        }

        private ConnectionProfile BuildConnectionProfile()
        {
            var profile = new ConnectionProfile
            {
                AuthMode      = AuthMode,
                TransportType = TransportType,
                QueueFilter   = NullIfEmpty(QueueFilter),
                TopicFilter   = NullIfEmpty(TopicFilter),
                SubscriptionFilter = NullIfEmpty(SubscriptionFilter),
                SelectedEntities   = BuildSelectedEntities()
            };

            switch (AuthMode)
            {
                case ConnectionAuthMode.Sas:
                    if (!string.IsNullOrWhiteSpace(ConnectionString))
                    {
                        // Parse full connection string – extract endpoint for display
                        var parsed = ConnectionStringParser.TryParse(ConnectionString);
                        if (parsed == null)
                            throw new FormatException("Cannot parse the SAS connection string.");
                        profile.ConnectionString = ConnectionString.Trim();
                        profile.Endpoint         = parsed.Endpoint;
                        profile.SharedAccessKeyName = parsed.SharedAccessKeyName;
                        profile.SharedAccessKey     = parsed.SharedAccessKey;
                        profile.EntityPath          = parsed.EntityPath;
                    }
                    else
                    {
                        profile.Endpoint = NormaliseEndpoint(Endpoint);
                    }
                    break;

                case ConnectionAuthMode.AzureActiveDirectory:
                    profile.Endpoint  = NormaliseEndpoint(Endpoint);
                    profile.TenantId  = NullIfEmpty(TenantId);
                    break;

                case ConnectionAuthMode.Windows:
                    profile.Endpoint = NormaliseEndpoint(Endpoint);
                    break;
            }

            if (SelectNotificationHubs && !string.IsNullOrWhiteSpace(NotificationHubName))
                profile.EntityPath = NotificationHubName.Trim();

            return profile;
        }

        private System.Collections.Generic.List<string> BuildSelectedEntities()
        {
            var list = new System.Collections.Generic.List<string>();
            if (SelectQueues)           list.Add(EntityTypeConstants.Queues);
            if (SelectTopics)           list.Add(EntityTypeConstants.Topics);
            if (SelectEventHubs)        list.Add(EntityTypeConstants.EventHubs);
            if (SelectNotificationHubs) list.Add(EntityTypeConstants.NotificationHubs);
            if (SelectRelays)           list.Add(EntityTypeConstants.Relays);
            return list;
        }

        private static string? NullIfEmpty(string? s)
            => string.IsNullOrWhiteSpace(s) ? null : s!.Trim();

        private static string NormaliseEndpoint(string raw)
        {
            var s = raw.Trim();
            if (string.IsNullOrEmpty(s)) return s;
            if (!s.Contains("://")) s = "sb://" + s;
            if (!s.EndsWith("/"))   s = s + "/";
            return s;
        }
    }
}
