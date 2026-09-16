using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceBusExplorer.Core.Abstractions;
using ServiceBusExplorer.Core.Models;
using ServiceBusExplorer.Core.Services;

namespace ServiceBusExplorer.Avalonia.ViewModels
{
    /// <summary>
    /// ViewModel for the main application shell (MainWindow).
    /// Manages the overall connection state, entity tree and detail panel.
    /// </summary>
    public partial class ShellViewModel : ViewModelBase
    {
        private readonly ISavedConnectionsService _savedConnectionsService;

        // ── Observable state ──────────────────────────────────────────────────────────────

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsConnected))]
        [NotifyPropertyChangedFor(nameof(IsDisconnected))]
        [NotifyPropertyChangedFor(nameof(ConnectionStatusText))]
        [NotifyPropertyChangedFor(nameof(WindowTitle))]
        private ConnectionProfile? _currentConnection;

        [ObservableProperty] private EntityTreeViewModel?   _entityTree;
        [ObservableProperty] private EntityDetailViewModel? _entityDetail;

        // ── Computed properties ───────────────────────────────────────────────────────────

        public bool IsConnected    => CurrentConnection != null;
        public bool IsDisconnected => CurrentConnection == null;

        public string ConnectionStatusText => CurrentConnection != null
            ? $"Connected: {CurrentConnection.Namespace}  ({CurrentConnection.AuthMode})"
            : "Not connected";

        public string WindowTitle => CurrentConnection != null
            ? $"Service Bus Explorer — {CurrentConnection.Namespace}"
            : "Service Bus Explorer";

        // ── Events ─────────────────────────────────────────────────────────────────────────

        public event EventHandler? ConnectRequested;

        // ── Constructor ────────────────────────────────────────────────────────────────────

        public ShellViewModel(ISavedConnectionsService savedConnectionsService)
        {
            _savedConnectionsService = savedConnectionsService;
        }

        // ── Commands ───────────────────────────────────────────────────────────────────────

        [RelayCommand]
        private void OpenConnectDialog() => ConnectRequested?.Invoke(this, EventArgs.Empty);

        [RelayCommand]
        private void Disconnect()
        {
            CurrentConnection = null;
            EntityTree        = null;
            EntityDetail      = null;
        }

        [RelayCommand]
        private static void Exit()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime app)
                app.Shutdown();
        }

        // ── Factory ───────────────────────────────────────────────────────────────────────

        public ConnectViewModel CreateConnectViewModel()
            => new ConnectViewModel(_savedConnectionsService, OnConnectionEstablished);

        // ── Callbacks ──────────────────────────────────────────────────────────────────────

        private void OnConnectionEstablished(ConnectionProfile profile)
        {
            CurrentConnection = profile;

            // Spin up real service instances backed by the modern SDK
            var nsService      = ModernNamespaceService.FromProfile(profile);
            var messagingService = ModernMessagingService.FromProfile(profile);

            var tree   = new EntityTreeViewModel(nsService);
            var detail = new EntityDetailViewModel(nsService, messagingService);

            tree.EntitySelected += async entity =>
            {
                await detail.LoadEntityAsync(entity);
            };

            EntityTree   = tree;
            EntityDetail = detail;

            // Auto-load the entity tree
            _ = tree.RefreshAsync();
        }
    }
}

