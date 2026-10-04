namespace ServiceBusExplorer.Core.Models
{
    /// <summary>
    /// Portable transport type — subset of the old Microsoft.ServiceBus.Messaging.TransportType
    /// that is supported by the modern Azure.Messaging.ServiceBus SDK on all platforms.
    /// <para>
    /// <b>Compatibility mode note:</b> NetMessaging (legacy WCF transport) is intentionally
    /// excluded here. It remains available in the WinForms host via the legacy SDK adapter.
    /// </para>
    /// </summary>
    public enum ConnectionTransportType
    {
        /// <summary>AMQP over TCP (default, recommended).</summary>
        AmqpTcp = 0,

        /// <summary>AMQP over WebSockets port 443, useful behind strict firewalls.</summary>
        AmqpWebSockets = 1
    }
}

