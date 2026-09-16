namespace ServiceBusExplorer.Avalonia.ViewModels
{
    /// <summary>Controls whether peeking is non-destructive or receive-and-delete.</summary>
    public enum PeekReceiveMode
    {
        /// <summary>Peek Lock — messages stay in the queue (default).</summary>
        Peek,
        /// <summary>Receive and Delete — messages are consumed from the queue.</summary>
        PeekAndDelete
    }

    /// <summary>How many messages to retrieve.</summary>
    public enum PeekCountSelection
    {
        /// <summary>Retrieve all messages (up to a practical limit).</summary>
        All,
        /// <summary>Retrieve the first X messages (by sequence number).</summary>
        TopX,
        /// <summary>Retrieve the last X messages (client-side tail of a large fetch).</summary>
        LastX
    }
}

