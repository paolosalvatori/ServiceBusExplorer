using System;

namespace ServiceBusExplorer.Core.Models
{
    public enum LogLevel { Info, Success, Warning, Error }

    /// <summary>A single log entry shown in the output pane.</summary>
    public sealed class LogEntry
    {
        public DateTime  Timestamp { get; } = DateTime.Now;
        public LogLevel  Level     { get; set; }
        public string    Message   { get; set; } = string.Empty;

        public string TimestampText => Timestamp.ToString("HH:mm:ss");

        public string LevelTag => Level switch
        {
            LogLevel.Success => "✓",
            LogLevel.Warning => "⚠",
            LogLevel.Error   => "✗",
            _                => "ℹ"
        };

        public string LevelColor => Level switch
        {
            LogLevel.Success => "#22863A",
            LogLevel.Warning => "#B08800",
            LogLevel.Error   => "#CC0000",
            _                => "Gray"
        };
    }
}

