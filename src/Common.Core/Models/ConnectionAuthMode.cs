namespace ServiceBusExplorer.Core.Models
{
    /// <summary>
    /// Portable authentication mode — no dependency on any Azure SDK or Windows APIs.
    /// This mirrors ServiceBusAuthMode from Common but without the legacy SDK imports.
    /// </summary>
    public enum ConnectionAuthMode
    {
        /// <summary>Shared Access Signature via a connection string or explicit key.</summary>
        Sas,

        /// <summary>Windows credentials (on-premises Service Bus for Windows Server only).</summary>
        Windows,

        /// <summary>Azure Active Directory / Microsoft Entra ID interactive browser sign-in.</summary>
        AzureActiveDirectory
    }
}

