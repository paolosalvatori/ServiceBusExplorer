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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Identity.Broker;
using Microsoft.Identity.Client;
using Microsoft.ServiceBus;

#endregion

namespace ServiceBusExplorer.Helpers
{
    /// <summary>
    /// Creates and caches Entra credentials for Service Bus connections.
    /// Shared by both the old (WindowsAzure.ServiceBus) and new (Azure.Messaging.ServiceBus) SDK paths.
    /// </summary>
    /// <remarks>
    /// This implementation targets public Azure only.
    /// The authority (login.microsoftonline.com) and audience (servicebus.azure.net / eventhubs.azure.net)
    /// are hard-coded for the public cloud. Sovereign clouds (Azure Government, Azure China, etc.)
    /// are not currently supported.
    /// </remarks>
    public static class EntraCredentialFactory
    {
        // Public Azure only — sovereign clouds would require a different audience and authority.
        const string DefaultTenantId = "organizations";
        public const string ServiceBusAudience = "https://servicebus.azure.net";
        public const string EventHubsAudience = "https://eventhubs.azure.net";
        static readonly Dictionary<IntPtr, BrokerWindow> brokerWindows = new Dictionary<IntPtr, BrokerWindow>();
        static readonly ConcurrentDictionary<string, InteractiveBrowserCredential> systemBrowserCredentials =
            new ConcurrentDictionary<string, InteractiveBrowserCredential>(StringComparer.OrdinalIgnoreCase);
        static readonly object credentialCacheLock = new object();
        static readonly ConcurrentDictionary<string, AzureActiveDirectoryTokenProvider.AuthenticationCallback> authenticationCallbacks =
            new ConcurrentDictionary<string, AzureActiveDirectoryTokenProvider.AuthenticationCallback>(StringComparer.OrdinalIgnoreCase);
        static readonly ConcurrentDictionary<string, TokenCredential> tokenCredentials =
            new ConcurrentDictionary<string, TokenCredential>(StringComparer.OrdinalIgnoreCase);
        static readonly ConcurrentDictionary<string, TokenProvider> tokenProviders =
            new ConcurrentDictionary<string, TokenProvider>(StringComparer.OrdinalIgnoreCase);
        // Serialize interactive requests for the same tenant, including different SDKs and audiences.
        static readonly ConcurrentDictionary<string, InteractiveLoginGate> interactiveLoginGates =
            new ConcurrentDictionary<string, InteractiveLoginGate>(StringComparer.OrdinalIgnoreCase);
        static IntPtr ownerWindowHandle = IntPtr.Zero;

        static volatile bool useSystemBrowserOnly;

        /// <summary>Use the system browser directly for subsequent Entra token requests.</summary>
        public static bool UseSystemBrowserOnly
        {
            get => useSystemBrowserOnly;
            set => useSystemBrowserOnly = value;
        }

        static string NormalizeTenantId(string tenantId)
        {
            return string.IsNullOrWhiteSpace(tenantId) ? DefaultTenantId : tenantId.Trim();
        }

        /// <summary>
        /// Selects the Explorer window that owns new broker requests.
        /// </summary>
        public static void SetOwnerWindowHandle(IntPtr handle)
        {
            lock (credentialCacheLock) ownerWindowHandle = handle;
        }

        /// <summary>Retires a destroyed HWND, including cached credentials for every tenant.</summary>
        public static void ReleaseOwnerWindowHandle(IntPtr destroyed, IntPtr fallback)
        {
            lock (credentialCacheLock)
            {
                if (brokerWindows.TryGetValue(destroyed, out var window))
                {
                    window.Retired = true;
                    brokerWindows.Remove(destroyed);
                }
                ownerWindowHandle = fallback;
            }
        }

        // Access the window registry only under credentialCacheLock.
        static BrokerWindow CurrentBrokerWindow()
        {
            if (!brokerWindows.TryGetValue(ownerWindowHandle, out var window))
            {
                window = new BrokerWindow(ownerWindowHandle);
                brokerWindows.Add(ownerWindowHandle, window);
            }
            return window;
        }

        sealed class BrokerWindow
        {
            public readonly IntPtr Handle;
            public readonly ConcurrentDictionary<string, InteractiveBrowserCredential> Credentials =
                new ConcurrentDictionary<string, InteractiveBrowserCredential>(StringComparer.OrdinalIgnoreCase);
            public bool Retired;

            public BrokerWindow(IntPtr handle) => Handle = handle;
        }

        /// <summary>
        /// Returns the Entra authority URL for the given tenant.
        /// Defaults to the "organizations" tenant when <paramref name="tenantId"/> is null or whitespace.
        /// </summary>
        /// <param name="tenantId">Entra tenant ID or domain, or null for the organizations endpoint.</param>
        public static string GetAuthority(string tenantId = null)
        {
            return $"https://login.microsoftonline.com/{NormalizeTenantId(tenantId)}";
        }

        /// <summary>
        /// Creates an InteractiveBrowserCredential configured for the given tenant.
        /// Uses the Windows broker (WAM) by default or the system browser when selected in Options.
        /// The credential internally caches tokens via MSAL, so repeated calls with
        /// the same tenant reuse the browser sign-in session.
        /// </summary>
        public static InteractiveBrowserCredential CreateInteractiveBrowserCredential(string tenantId = null)
        {
            var normalizedTenantId = NormalizeTenantId(tenantId);
            if (UseSystemBrowserOnly) return CreateSystemBrowserCredential(normalizedTenantId);
            lock (credentialCacheLock) return CreateBrokerCredential(normalizedTenantId, CurrentBrokerWindow());
        }

        static InteractiveBrowserCredential CreateBrokerCredential(string normalizedTenantId, BrokerWindow owner)
        {
            return owner.Credentials.GetOrAdd(normalizedTenantId, _ =>
            {
                // Keep only an in-memory MSAL cache so ClearCache can discard it.
                var options = new InteractiveBrowserCredentialBrokerOptions(owner.Handle) { TenantId = normalizedTenantId };
                return new InteractiveBrowserCredential(options);
            });
        }

        /// <summary>
        /// Creates a non-broker InteractiveBrowserCredential for browser-only sign-in and broker fallback.
        /// </summary>
        static InteractiveBrowserCredential CreateSystemBrowserCredential(string normalizedTenantId)
        {
            return systemBrowserCredentials.GetOrAdd(normalizedTenantId, _ =>
            {
                var options = new InteractiveBrowserCredentialOptions
                {
                    TenantId = normalizedTenantId
                };

                return new InteractiveBrowserCredential(options);
            });
        }

        /// <summary>
        /// Creates an authentication callback for the old WindowsAzure.ServiceBus SDK
        /// that obtains tokens via interactive browser sign-in.
        /// Uses the Service Bus audience by default.
        /// </summary>
        /// <param name="tenantId">Entra tenant ID, or null for the organizations endpoint.</param>
        public static AzureActiveDirectoryTokenProvider.AuthenticationCallback CreateOldSdkAuthenticationCallback(string tenantId = null)
        {
            return CreateOldSdkAuthenticationCallback(tenantId, ServiceBusAudience);
        }

        /// <summary>
        /// Creates an authentication callback for the old WindowsAzure.ServiceBus SDK
        /// that obtains tokens via interactive browser sign-in, targeting the specified audience.
        /// </summary>
        /// <param name="tenantId">Entra tenant ID, or null for the organizations endpoint.</param>
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
                    // The legacy SDK supplies the Service Bus resource even for Event Hubs.
                    var token = await GetAccessTokenAsync(
                            normalizedTenantId,
                            new TokenRequestContext(GetScopes(normalizedAudience)),
                            CancellationToken.None)
                        .ConfigureAwait(false);
                    return token.Token;
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
        /// <param name="tenantId">Entra tenant ID, or null for the organizations endpoint.</param>
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
            return tokenCredentials.GetOrAdd(normalizedTenantId, _ => new CachedEntraTokenCredential(normalizedTenantId));
        }

        /// <summary>
        /// Clears the in-process credential caches, so the next connection re-runs the
        /// interactive sign-in. Any MSAL cache persisted on disk by an older version of this
        /// tool is not affected, and the browser session cookie is not cleared, so this is not
        /// a full Entra sign-out.
        /// </summary>
        public static void ClearCache()
        {
            lock (credentialCacheLock)
            {
                foreach (var window in brokerWindows.Values) window.Credentials.Clear();
            }
            systemBrowserCredentials.Clear();
            authenticationCallbacks.Clear();
            tokenCredentials.Clear();
            tokenProviders.Clear();
            // interactiveLoginGates is deliberately not cleared: the semaphores are mutual-exclusion
            // primitives, not cached state, and removing one that a sign-in still holds would let a
            // concurrent caller open a second browser prompt.
        }

        static string[] GetScopes(string resource)
        {
            var normalizedResource = string.IsNullOrWhiteSpace(resource)
                ? ServiceBusAudience
                : resource.Trim().TrimEnd('/');
            return new[] { $"{normalizedResource}/.default" };
        }

        static async Task<AccessToken> GetAccessTokenAsync(string tenantId, TokenRequestContext requestContext, CancellationToken cancellationToken)
        {
            BrokerWindow owner;
            lock (credentialCacheLock) owner = CurrentBrokerWindow();
            var gate = interactiveLoginGates.GetOrAdd(tenantId, _ => new InteractiveLoginGate());
            var attempt = gate.CurrentAttempt;
            await gate.Mutex.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                // Requests already waiting on a failed sign-in see that same failure. A new
                // request can try again, and successful calls still acquire their own scopes/claims.
                cancellationToken.ThrowIfCancellationRequested();
                attempt.Failure?.Throw();
                try
                {
                    return await AcquireForWindowAsync(owner, tenantId, requestContext, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
                {
                    // Keep the origin of the failure when the legacy SDK wraps it as unauthorized.
                    Exception failure = ex;
                    if (ex is OperationCanceledException canceled)
                    {
                        failure = new SignInCanceledException(canceled);
                    }
                    else if (!IsTokenAcquisitionFailure(ex))
                    {
                        failure = new AuthenticationFailedException("Entra token acquisition failed.", ex);
                    }
                    attempt.Failure = ExceptionDispatchInfo.Capture(failure);
                    gate.CurrentAttempt = new SignInAttempt();
                    if (failure != ex) throw failure;
                    throw;
                }
            }
            finally
            {
                gate.Mutex.Release();
            }
        }

        sealed class InteractiveLoginGate
        {
            public readonly SemaphoreSlim Mutex = new SemaphoreSlim(1, 1);
            public volatile SignInAttempt CurrentAttempt = new SignInAttempt();
        }

        sealed class SignInAttempt
        {
            public ExceptionDispatchInfo Failure;
        }

        sealed class SignInCanceledException : OperationCanceledException
        {
            public SignInCanceledException(OperationCanceledException cause)
                : base(cause.Message, cause, cause.CancellationToken) { }
        }

        /// <summary>Recognizes credential failures and cancellation inside the legacy SDK's management exceptions.</summary>
        public static bool IsTokenAcquisitionFailure(Exception exception)
        {
            for (var current = exception; current != null; current = current.InnerException)
            {
                if (current is AuthenticationFailedException || current is MsalException || current is SignInCanceledException)
                {
                    return true;
                }
            }
            return false;
        }

        static Task<AccessToken> AcquireForWindowAsync(BrokerWindow owner, string tenantId,
            TokenRequestContext context, CancellationToken cancellationToken)
        {
            InteractiveBrowserCredential broker = null;

            ValueTask<AccessToken> AcquireBroker(CancellationToken token)
            {
                lock (credentialCacheLock)
                {
                    // The request may have waited for another sign-in while its window closed.
                    if (owner.Retired || owner.Handle == IntPtr.Zero) owner = CurrentBrokerWindow();
                    broker = CreateBrokerCredential(tenantId, owner);
                }
                return broker.GetTokenAsync(context, token);
            }

            void InvalidateBroker()
            {
                lock (credentialCacheLock)
                {
                    if (owner.Credentials.TryGetValue(tenantId, out var cached) && cached == broker)
                        owner.Credentials.TryRemove(tenantId, out _);
                }
            }

            return AcquireTokenWithFallbackAsync(InvalidateBroker, AcquireBroker,
                token => CreateSystemBrowserCredential(tenantId).GetTokenAsync(context, token), cancellationToken);
        }

        static async Task<AccessToken> AcquireTokenWithFallbackAsync(
            Action invalidateBroker,
            Func<CancellationToken, ValueTask<AccessToken>> brokerAcquire,
            Func<CancellationToken, ValueTask<AccessToken>> browserAcquire,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (UseSystemBrowserOnly)
            {
                return await browserAcquire(cancellationToken).ConfigureAwait(false);
            }

            try
            {
                return await brokerAcquire(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex) when (!cancellationToken.IsCancellationRequested && IsBrokerUnavailable(ex))
            {
                // Only this acquisition falls back. A later request rebuilds and retries WAM.
                invalidateBroker();
                Trace.TraceWarning("Windows broker unavailable; using browser sign-in for this request. The next request will retry the broker.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            return await browserAcquire(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns true when the exception shows the Windows broker itself is unusable, as opposed to
        /// a normal sign-in failure (e.g. user cancelled), which must propagate.
        /// </summary>
        static bool IsBrokerUnavailable(Exception ex)
        {
            var unavailable = false;
            for (var current = ex; current != null; current = current.InnerException)
            {
                if (current is OperationCanceledException) return false;
                if (current is MsalException msalException)
                {
                    switch (msalException.ErrorCode)
                    {
                        case MsalError.CannotInvokeBroker:
                        case MsalError.InvalidOwnerWindowType:
                        case MsalError.PlatformNotSupported:
                            unavailable = true;
                            break;
                        default:
                            // Ambiguous broker errors, denials and Conditional Access failures must propagate.
                            return false;
                    }
                }
                else if (current is DllNotFoundException
                    || current is EntryPointNotFoundException
                    || current is BadImageFormatException
                    || current is PlatformNotSupportedException)
                {
                    unavailable = true;
                }
            }

            return unavailable;
        }

        sealed class CachedEntraTokenCredential : TokenCredential
        {
            readonly string tenantId;

            public CachedEntraTokenCredential(string tenantId)
            {
                this.tenantId = tenantId;
            }

            public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return GetAccessTokenAsync(tenantId, requestContext, cancellationToken).GetAwaiter().GetResult();
            }

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return new ValueTask<AccessToken>(GetAccessTokenAsync(tenantId, requestContext, cancellationToken));
            }
        }
    }
}
