using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using ServiceBusExplorer.Avalonia.ViewModels;

namespace ServiceBusExplorer.Avalonia.Views
{
    public partial class ConnectDialog : Window
    {
        public ConnectDialog()
        {
            InitializeComponent();
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            // Close the window automatically when the ViewModel signals success.
            if (DataContext is ConnectViewModel vm)
                vm.ConnectionEstablished += OnConnectionEstablished;
        }

        protected override void OnClosed(EventArgs e)
        {
            if (DataContext is ConnectViewModel vm)
                vm.ConnectionEstablished -= OnConnectionEstablished;

            base.OnClosed(e);
        }

        private void OnConnectionEstablished(object? sender, EventArgs e)
            => Dispatcher.UIThread.Post(Close);

        /// <summary>Click handler for the Cancel button (no ViewModel command needed).</summary>
        private void OnCancelClicked(object? sender, RoutedEventArgs e)
            => Close();
    }
}
