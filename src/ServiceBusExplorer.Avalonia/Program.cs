using Avalonia;

namespace ServiceBusExplorer.Avalonia
{
    internal sealed class Program
    {
        // Avalonia requires [STAThread] on Windows.
        // On macOS / Linux this attribute is ignored — safe to keep.
        [System.STAThread]
        public static void Main(string[] args) =>
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        /// <summary>
        /// Central configuration point for the Avalonia AppBuilder.
        /// Called by both the real entry point and Avalonia designer tooling.
        /// </summary>
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                         .UsePlatformDetect()
                         .WithInterFont()
                         .LogToTrace();
    }
}

