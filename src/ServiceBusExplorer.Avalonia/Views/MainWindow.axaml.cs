using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ServiceBusExplorer.Avalonia.ViewModels;

namespace ServiceBusExplorer.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            // Wire the ViewModel's dialog-request event to the View.
            // This keeps all dialog-opening code out of the ViewModel.
            if (DataContext is ShellViewModel vm)
                vm.ConnectRequested += OnConnectRequested;
        }

        protected override void OnClosed(EventArgs e)
        {
            if (DataContext is ShellViewModel vm)
                vm.ConnectRequested -= OnConnectRequested;

            base.OnClosed(e);
        }

        private async void OnConnectRequested(object? sender, EventArgs e)
        {
            if (DataContext is not ShellViewModel vm) return;

            var connectVm = vm.CreateConnectViewModel();
            var dialog    = new ConnectDialog { DataContext = connectVm };

            // ShowDialog suspends here until the dialog closes.
            await dialog.ShowDialog(this);
        }

        /// <summary>
        /// Forwards TreeView selection to the EntityTreeViewModel so the
        /// detail panel updates without a direct view-to-viewmodel reference.
        /// </summary>
        private void OnTreeSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (DataContext is not ShellViewModel vm) return;
            if (vm.EntityTree == null) return;

            var selected = (sender as TreeView)?.SelectedItem as EntityTreeNodeViewModel;
            vm.EntityTree.OnNodeSelected(selected);
        }
    }
}
