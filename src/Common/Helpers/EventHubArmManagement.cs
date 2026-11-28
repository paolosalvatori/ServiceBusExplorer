using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.EventHubs;

namespace ServiceBusExplorer.Helpers
{
    public sealed class EventHubArmManagement
    {
        // Accept a single public-cloud ARM hub. Build all child requests from this handle.
        private static readonly Regex HubId = new Regex(
            @"\A/subscriptions/(?<sub>[0-9a-f-]{36})/resourceGroups/(?<rg>[\p{L}\p{Nd}_().-]{1,90})" +
            @"/providers/Microsoft\.EventHub/namespaces/(?<ns>[a-z][a-z0-9-]{4,48}[a-z0-9])" +
            @"/eventhubs/(?<hub>[a-z0-9](?:[a-z0-9._-]{0,254}[a-z0-9])?)\z",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        private static readonly Regex GroupName = new Regex(@"\A(?:\$Default|[a-z0-9](?:[a-z0-9._-]{0,48}[a-z0-9])?)\z",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private readonly EventHubResource hub;
        public string ResourceId => hub.Id.ToString();

        internal EventHubArmManagement(string resourceId, ServiceBusNamespace profile, string tenantId,
            Func<string, TokenCredential> getCredential, ArmClientOptions options = null)
        {
            if (profile != null && !profile.IsEntra)
                throw new InvalidOperationException("ARM management requires an Entra connection or a new Explorer window.");

            var match = HubId.Match(resourceId?.Trim() ?? string.Empty);
            if (!match.Success || !Guid.TryParseExact(match.Groups["sub"].Value, "D", out var subscription) ||
                subscription == Guid.Empty || match.Groups["rg"].Value.EndsWith(".", StringComparison.Ordinal))
                throw new ArgumentException("Enter the full Azure resource ID of one Event Hub, ending in /namespaces/{namespace}/eventhubs/{hub}.", nameof(resourceId));

            var ns = match.Groups["ns"].Value;
            var name = match.Groups["hub"].Value;
            if (profile != null)
            {
                if (!string.Equals(profile.FullyQualifiedNamespace, ns + ".servicebus.windows.net", StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrWhiteSpace(profile.EntityPath) && !string.Equals(name, profile.EntityPath, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException("Select an Event Hub within the connected Entra namespace and entity scope.", nameof(resourceId));
                if (!string.IsNullOrWhiteSpace(tenantId))
                    throw new ArgumentException("The connected profile supplies the ARM sign-in tenant.", nameof(tenantId));
                tenantId = profile.TenantId;
            }
            else if (!Guid.TryParseExact(tenantId?.Trim(), "D", out var tenant) || tenant == Guid.Empty)
            {
                throw new ArgumentException("Enter the Microsoft Entra tenant ID for the selected subscription.", nameof(tenantId));
            }

            var id = EventHubResource.CreateResourceIdentifier(subscription.ToString("D"), match.Groups["rg"].Value, ns, name);
            options = options ?? new ArmClientOptions();
            options.Environment = ArmEnvironment.AzurePublicCloud;
            hub = new ArmClient(getCredential(tenantId?.Trim()), id.SubscriptionId, options).GetEventHubResource(id);
        }

        public async Task<EventHubData> GetHubAsync(CancellationToken cancellationToken) =>
            (await hub.GetAsync(cancellationToken).ConfigureAwait(false)).Value.Data;

        public async Task<IReadOnlyList<EventHubsConsumerGroupData>> GetGroupsAsync(CancellationToken cancellationToken)
        {
            var groups = new List<EventHubsConsumerGroupData>();
            var pages = hub.GetEventHubsConsumerGroups().GetAllAsync(cancellationToken: cancellationToken);
            var collectionUri = new Uri("https://management.azure.com" + hub.Id + "/consumergroups");
            await foreach (var page in pages.AsPages().ConfigureAwait(false))
            {
                if (page.ContinuationToken != null &&
                    (!Uri.TryCreate(page.ContinuationToken, UriKind.Absolute, out var next) ||
                     next.UserInfo.Length != 0 || next.Fragment.Length != 0 ||
                     Uri.Compare(collectionUri, next, UriComponents.SchemeAndServer | UriComponents.Path,
                         UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) != 0))
                    throw new InvalidOperationException("ARM returned a continuation outside the selected consumer group collection.");

                foreach (var item in page.Values)
                    groups.Add(item.Data);
            }
            return groups;
        }

        public static void ValidateGroupMetadata(string name, string metadata)
        {
            if (name == null || !GroupName.IsMatch(name))
                throw new ArgumentException("Enter one valid consumer group name of up to 50 characters.", nameof(name));
            if (metadata == null || metadata.Length > 1024)
                throw new ArgumentException("Consumer group metadata must be at most 1024 characters. An empty value clears it.", nameof(metadata));
        }

        public async Task<EventHubsConsumerGroupData> SetGroupMetadataAsync(string name, string metadata, CancellationToken cancellationToken)
        {
            ValidateGroupMetadata(name, metadata);
            var data = new EventHubsConsumerGroupData { UserMetadata = metadata };
            var operation = await hub.GetEventHubsConsumerGroups()
                .CreateOrUpdateAsync(WaitUntil.Completed, name, data, cancellationToken).ConfigureAwait(false);
            return operation.Value.Data;
        }
    }
}
