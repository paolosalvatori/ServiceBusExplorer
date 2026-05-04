#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.ServiceBus;

#endregion

namespace ServiceBusExplorer.Helpers
{
    /// <summary>
    /// Creates and caches Azure AD credentials for Service Bus connections.
    /// Shared by both the old (WindowsAzure.ServiceBus) and new (Azure.Messaging.ServiceBus) SDK paths.
    /// </summary>
    /// <remarks>
    /// This implementation targets public Azure only.
    /// The authority (login.microsoftonline.com) and audience (servicebus.azure.net / eventhubs.azure.net)
    /// are hard-coded for the public cloud. Sovereign clouds (Azure Government, Azure China, etc.)
    /// are not currently supported.
    /// </remarks>
    public static class AadCredentialFactory
    {
        // Public Azure only — sovereign clouds would require a different audience and authority.
        const string DefaultTenantId = "organizations";
        public const string ServiceBusAudience = "https://servicebus.azure.net";
        public const string EventHubsAudience = "https://eventhubs.azure.net";
        static readonly ConcurrentDictionary<string, InteractiveBrowserCredential> interactiveBrowserCredentials =
            new ConcurrentDictionary<string, InteractiveBrowserCredential>(StringComparer.OrdinalIgnoreCase);
        static readonly ConcurrentDictionary<string, AzureActiveDirectoryTokenProvider.AuthenticationCallback> authenticationCallbacks =
            new ConcurrentDictionary<string, AzureActiveDirectoryTokenProvider.AuthenticationCallback>(StringComparer.OrdinalIgnoreCase);
        static readonly ConcurrentDictionary<string, TokenCredential> tokenCredentials =
            new ConcurrentDictionary<string, TokenCredential>(StringComparer.OrdinalIgnoreCase);
        static readonly ConcurrentDictionary<string, TokenProvider> tokenProviders =
            new ConcurrentDictionary<string, TokenProvider>(StringComparer.OrdinalIgnoreCase);
        // Gate prevents multiple concurrent interactive browser prompts for the same tenant/scope
        // combination on first use. MSAL handles token freshness after the initial acquisition.
        static readonly ConcurrentDictionary<string, SemaphoreSlim> interactiveLoginGates =
            new ConcurrentDictionary<string, SemaphoreSlim>(StringComparer.OrdinalIgnoreCase);

        static string NormalizeTenantId(string tenantId)
        {
            return string.IsNullOrWhiteSpace(tenantId) ? DefaultTenantId : tenantId.Trim();
        }

        /// <summary>
        /// Returns the Azure AD authority URL for the given tenant.
        /// Defaults to the "organizations" tenant when <paramref name="tenantId"/> is null or whitespace.
        /// </summary>
        /// <param name="tenantId">Azure AD tenant ID or domain, or null for the organizations endpoint.</param>
        public static string GetAuthority(string tenantId = null)
        {
            return $"https://login.microsoftonline.com/{NormalizeTenantId(tenantId)}";
        }

        /// <summary>
        /// Creates an InteractiveBrowserCredential configured for the given tenant.
        /// The credential internally caches tokens via MSAL, so repeated calls with
        /// the same tenant reuse the browser sign-in session.
        /// </summary>
        public static InteractiveBrowserCredential CreateInteractiveBrowserCredential(string tenantId = null)
        {
            var normalizedTenantId = NormalizeTenantId(tenantId);
            return interactiveBrowserCredentials.GetOrAdd(normalizedTenantId, _ =>
            {
                var options = new InteractiveBrowserCredentialOptions
                {
                    TenantId = normalizedTenantId,
                    TokenCachePersistenceOptions = new TokenCachePersistenceOptions
                    {
                        Name = "ServiceBusExplorer"
                    }
                };

                return new InteractiveBrowserCredential(options);
            });
        }

        /// <summary>
        /// Creates an authentication callback for the old WindowsAzure.ServiceBus SDK
        /// that obtains tokens via interactive browser sign-in.
        /// Uses the Service Bus audience by default.
        /// </summary>
        /// <param name="tenantId">Azure AD tenant ID, or null for the organizations endpoint.</param>
        public static AzureActiveDirectoryTokenProvider.AuthenticationCallback CreateOldSdkAuthenticationCallback(string tenantId = null)
        {
            return CreateOldSdkAuthenticationCallback(tenantId, ServiceBusAudience);
        }

        /// <summary>
        /// Creates an authentication callback for the old WindowsAzure.ServiceBus SDK
        /// that obtains tokens via interactive browser sign-in, targeting the specified audience.
        /// </summary>
        /// <param name="tenantId">Azure AD tenant ID, or null for the organizations endpoint.</param>
        /// <param name="audience">The token audience (e.g. ServiceBusAudience or EventHubsAudience).</param>
        public static AzureActiveDirectoryTokenProvider.AuthenticationCallback CreateOldSdkAuthenticationCallback(string tenantId, string audience)
        {
            var normalizedTenantId = NormalizeTenantId(tenantId);
            var normalizedAudience = string.IsNullOrWhiteSpace(audience) ? ServiceBusAudience : audience.Trim();
            var cacheKey = $"{normalizedTenantId}|{normalizedAudience}";
            return authenticationCallbacks.GetOrAdd(cacheKey, _ =>
            {
                return async (authorityUrl, resource, state) =>
                {
                    try
                    {
                        // Use our normalizedAudience instead of the SDK-supplied resource.
                        // The old SDK always passes "https://servicebus.azure.net" as
                        // the resource, even for Event Hub clients, which is wrong for
                        // Event Hub namespaces that require the Event Hubs audience.
                        var token = await GetAccessTokenAsync(
                                normalizedTenantId,
                                GetScopes(normalizedAudience),
                                CancellationToken.None)
                            .ConfigureAwait(false);
                        return token.Token;
                    }
                    catch (AuthenticationFailedException ex)
                    {
                        // Surface as OperationCanceledException so the RetryHelper
                        // does not re-open a browser window up to 10 times.
                        throw new OperationCanceledException(
                            "Interactive browser authentication failed or was cancelled.", ex);
                    }
                };
            });
        }

        /// <summary>
        /// Creates a TokenProvider for the old WindowsAzure.ServiceBus SDK that obtains
        /// tokens from an InteractiveBrowserCredential.
        /// Uses the Service Bus audience by default.
        /// </summary>
        public static TokenProvider CreateOldSdkTokenProvider(string tenantId = null)
        {
            return CreateOldSdkTokenProvider(tenantId, ServiceBusAudience);
        }

        /// <summary>
        /// Creates a TokenProvider for the old WindowsAzure.ServiceBus SDK that obtains
        /// tokens from an InteractiveBrowserCredential, targeting the specified audience.
        /// </summary>
        /// <param name="tenantId">Azure AD tenant ID, or null for the organizations endpoint.</param>
        /// <param name="audience">The token audience (e.g. ServiceBusAudience or EventHubsAudience).</param>
        public static TokenProvider CreateOldSdkTokenProvider(string tenantId, string audience)
        {
            var normalizedTenantId = NormalizeTenantId(tenantId);
            var normalizedAudience = string.IsNullOrWhiteSpace(audience) ? ServiceBusAudience : audience.Trim();
            var cacheKey = $"{normalizedTenantId}|{normalizedAudience}";
            return tokenProviders.GetOrAdd(cacheKey, _ =>
                TokenProvider.CreateAzureActiveDirectoryTokenProvider(
                    CreateOldSdkAuthenticationCallback(normalizedTenantId, normalizedAudience),
                    new Uri(GetAuthority(normalizedTenantId)),
                    normalizedAudience,
                    null));
        }

        /// <summary>
        /// Returns a TokenCredential suitable for the new Azure.Messaging.ServiceBus SDK.
        /// </summary>
        public static TokenCredential CreateNewSdkTokenCredential(string tenantId = null)
        {
            var normalizedTenantId = NormalizeTenantId(tenantId);
            return tokenCredentials.GetOrAdd(normalizedTenantId, _ => new CachedAadTokenCredential(normalizedTenantId));
        }

        static string[] GetScopes(string resource)
        {
            var normalizedResource = string.IsNullOrWhiteSpace(resource)
                ? ServiceBusAudience
                : resource.Trim().TrimEnd('/');
            return new[] { $"{normalizedResource}/.default" };
        }

        static async Task<AccessToken> GetAccessTokenAsync(string tenantId, string[] scopes, CancellationToken cancellationToken)
        {
            var gateKey = $"{tenantId}|{string.Join(" ", scopes ?? Array.Empty<string>())}";
            var gate = interactiveLoginGates.GetOrAdd(gateKey, _ => new SemaphoreSlim(1, 1));
            await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var credential = CreateInteractiveBrowserCredential(tenantId);
                return await credential.GetTokenAsync(new TokenRequestContext(scopes), cancellationToken)
                    .ConfigureAwait(false);
            }
            finally
            {
                gate.Release();
            }
        }

        sealed class CachedAadTokenCredential : TokenCredential
        {
            readonly string tenantId;

            public CachedAadTokenCredential(string tenantId)
            {
                this.tenantId = tenantId;
            }

            public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return GetAccessTokenAsync(tenantId, requestContext.Scopes, cancellationToken).GetAwaiter().GetResult();
            }

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return new ValueTask<AccessToken>(GetAccessTokenAsync(tenantId, requestContext.Scopes, cancellationToken));
            }
        }
    }
}
