using CommunityToolkit.Mvvm.ComponentModel;

namespace ServiceBusExplorer.Avalonia.ViewModels
{
    /// <summary>
    /// Base class for all ViewModels in the Avalonia host.
    /// Extends <see cref="ObservableObject"/> from CommunityToolkit.Mvvm so that
    /// source-generated [ObservableProperty] and [RelayCommand] attributes work.
    /// </summary>
    public abstract class ViewModelBase : ObservableObject
    {
    }
}

