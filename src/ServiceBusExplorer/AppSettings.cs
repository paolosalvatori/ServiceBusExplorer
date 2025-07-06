
namespace ServiceBusExplorer
{
    public class AppSettings
    {
        public bool Debug { get; set; }
        public string SelectedEntities { get; set; }
        public string Encoding { get; set; }
        public bool ShowMessageCount { get; set; }
        public bool SaveMessageToFile { get; set; }
        public bool SavePropertiesToFile { get; set; }
        public bool SaveCheckpointsToFile { get; set; }
        public string Scheme { get; set; }
        public string Message { get; set; }
        public string File { get; set; }
        public string Label { get; set; }
        public int RetryCount { get; set; }
        public int RetryTimeout { get; set; }
        public int Top { get; set; }
        public int ReceiveTimeout { get; set; }
        public int ServerTimeout { get; set; }
        public int PrefetchCount { get; set; }
        public int SenderThinkTime { get; set; }
        public int ReceiverThinkTime { get; set; }
        public int MonitorRefreshInterval { get; set; }
        public bool UseAscii { get; set; }
        public string SubscriptionId { get; set; }
        public string CertificateThumbprint { get; set; }
        public string MessageDeferProvider { get; set; }
        public string Microsoft_ServiceBus_X509RevocationMode { get; set; }
        public string ClientSettingsProvider_ServiceUri { get; set; }
    }
}
