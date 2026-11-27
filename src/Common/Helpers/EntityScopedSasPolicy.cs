using System;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Messaging.ServiceBus.Administration;

namespace ServiceBusExplorer.Helpers
{
    internal sealed class EntityScopedSasPolicy : HttpPipelineSynchronousPolicy
    {
        private static readonly string[] AuthorizationHeaders =
        {
            "Authorization", "ServiceBusSupplementaryAuthorization", "ServiceBusDlqSupplementaryAuthorization"
        };

        private readonly Func<string> getSignature;

        private EntityScopedSasPolicy(Func<string> getSignature) => this.getSignature = getSignature;

        internal static ServiceBusAdministrationClient CreateClient(Uri endpoint, ServiceBusAdministrationClientOptions options, Func<string> getSignature)
        {
            var credential = new AzureSasCredential(getSignature());
            options.AddPolicy(new EntityScopedSasPolicy(getSignature), HttpPipelinePosition.PerRetry);
            return new ServiceBusAdministrationClient(endpoint.AbsoluteUri, credential, options);
        }

        public override void OnSendingRequest(HttpMessage message)
        {
            // The SDK copies the same precomputed SAS into the primary and any forwarding
            // authorization headers before entering the pipeline. Renew them for this attempt.
            var current = getSignature();
            foreach (var name in AuthorizationHeaders)
                if (message.Request.Headers.Contains(name)) message.Request.Headers.SetValue(name, current);
        }
    }
}
