namespace ServiceBusExplorer.Helpers
{
    /// <summary>A session discovered without accepting it or acquiring its lock.</summary>
    public sealed class SessionInfo
    {
        public SessionInfo(string sessionId) => SessionId = sessionId;
        public string SessionId { get; }
    }
}
