namespace ServiceBusExplorer.Core.Abstractions
{
    /// <summary>
    /// Delegate for writing diagnostic messages to the host's log surface.
    /// Matches the signature of <c>WriteToLogDelegate</c> in the legacy Common project
    /// so adapters can bridge the two without changing existing call sites.
    /// </summary>
    public delegate void CoreWriteToLogDelegate(string message, bool async = false);
}

