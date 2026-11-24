using System;

namespace EventGridExplorerLibrary
{
    public sealed class NamespaceTopicEndpoint
    {
        public NamespaceTopicEndpoint(string hostname)
        {
            if (string.IsNullOrWhiteSpace(hostname))
            {
                throw new InvalidOperationException("The Event Grid namespace has no topics hostname. Ensure that namespace topics are enabled and refresh the namespace.");
            }
            if (Uri.CheckHostName(hostname) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("The Event Grid namespace returned an invalid topics hostname. Refresh the namespace and try again.", nameof(hostname));
            }

            Address = new UriBuilder(Uri.UriSchemeHttps, hostname).Uri;
        }

        public Uri Address { get; }

        public override string ToString() => Address.GetLeftPart(UriPartial.Authority);
    }
}
