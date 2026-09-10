using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ServiceBusExplorer.Avalonia.ViewModels;
using ServiceBusExplorer.Avalonia.Views;
using ServiceBusExplorer.Core.Services;

namespace ServiceBusExplorer.Avalonia
{
    public class App : Application
    {
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var savedConnectionsService = new JsonSavedConnectionsService();
                var shellVm = new ShellViewModel(savedConnectionsService);

                desktop.MainWindow = new MainWindow { DataContext = shellVm };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}

