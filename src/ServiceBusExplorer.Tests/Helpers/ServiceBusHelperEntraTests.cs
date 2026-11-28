using System;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Messaging.ServiceBus.Administration;
using FluentAssertions;
using Microsoft.Identity.Client;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.ServiceBus.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    [CollectionDefinition("Entra credential cache", DisableParallelization = true)]
    public class EntraCredentialCacheCollection { }

    [Collection("Entra credential cache")]
    public class ServiceBusHelperEntraTests
    {
        [Fact]
        public void CopyConstructor_EntraState_PreservesEntraFieldsAndNewSdkBridge()
        {
            var source = new ServiceBusHelper((message, asynchronous) => { })
            {
                NamespaceUri = new Uri("sb://myns.servicebus.windows.net/")
            };
            var entraNamespace = new ServiceBusNamespace(
                "sb://myns.servicebus.windows.net/",
                "myns",
                "tenant-id",
                TransportType.Amqp);

            SetPrivateField(source, "serviceBusNamespaceInstance", entraNamespace);
            SetPrivateField(source, "entraTokenProvider", EntraCredentialFactory.CreateOldSdkTokenProvider("tenant-id"));

            var copy = new ServiceBusHelper((message, asynchronous) => { }, source);

            GetPrivateField<TokenProvider>(copy, "entraTokenProvider").Should().NotBeNull();
            GetPrivateField<ServiceBusNamespace>(copy, "serviceBusNamespaceInstance").Should().BeSameAs(entraNamespace);

            var helper2 = copy.GetServiceBusHelper2();
            helper2.IsEntra.Should().BeTrue();
            helper2.FullyQualifiedNamespace.Should().Be("myns.servicebus.windows.net");
            helper2.EntraTokenCredential.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateEventHubProducerClient_EntraNamespace_ReturnsClient()
        {
            var helper = new ServiceBusHelper((message, asynchronous) => { });
            var entraNamespace = new ServiceBusNamespace(
                "sb://myns.servicebus.windows.net/",
                "myns",
                "tenant-id",
                TransportType.Amqp);

            SetPrivateField(helper, "serviceBusNamespaceInstance", entraNamespace);

            var client = helper.CreateEventHubProducerClient("hub1");

            client.Should().NotBeNull();
            await client.DisposeAsync();
        }

        [Fact]
        public void ServiceBusHelper2_CreateClients_EntraConfiguration_ReturnsClients()
        {
            var helper2 = new ServiceBusHelper2((message, asynchronous) => { })
            {
                FullyQualifiedNamespace = "myns.servicebus.windows.net",
                EntraTokenCredential = new FakeTokenCredential(),
                TransportType = Azure.Messaging.ServiceBus.ServiceBusTransportType.AmqpTcp
            };

            helper2.IsEntra.Should().BeTrue();
            helper2.CreateServiceBusClient().Should().NotBeNull();
            helper2.CreateAdministrationClient().Should().NotBeNull();
        }

        [Fact]
        public void EntraCredentialFactory_SameTenant_ReusesCredentialCallbackAndProvider()
        {
            var credential1 = EntraCredentialFactory.CreateInteractiveBrowserCredential("tenant-id");
            var credential2 = EntraCredentialFactory.CreateInteractiveBrowserCredential("tenant-id");
            var callback1 = EntraCredentialFactory.CreateOldSdkAuthenticationCallback("tenant-id");
            var callback2 = EntraCredentialFactory.CreateOldSdkAuthenticationCallback("tenant-id");
            var provider1 = EntraCredentialFactory.CreateOldSdkTokenProvider("tenant-id");
            var provider2 = EntraCredentialFactory.CreateOldSdkTokenProvider("tenant-id");
            var tokenCredential1 = EntraCredentialFactory.CreateNewSdkTokenCredential("tenant-id");
            var tokenCredential2 = EntraCredentialFactory.CreateNewSdkTokenCredential("tenant-id");

            credential2.Should().BeSameAs(credential1);
            callback2.Should().BeSameAs(callback1);
            provider2.Should().BeSameAs(provider1);
            tokenCredential2.Should().BeSameAs(tokenCredential1);
            tokenCredential1.Should().NotBeNull();
        }

        [Fact]
        public void EntraCredentialFactory_ClearCache_ReturnsNewInstancesForSameTenant()
        {
            // Uses a tenant ID not referenced by any other test in this class: EntraCredentialFactory's
            // caches are static, and ClearCache() clears them for every tenant, not just this one.
            const string tenantId = "clear-cache-regression-tenant";

            var credential1 = EntraCredentialFactory.CreateInteractiveBrowserCredential(tenantId);
            var callback1 = EntraCredentialFactory.CreateOldSdkAuthenticationCallback(tenantId);
            var provider1 = EntraCredentialFactory.CreateOldSdkTokenProvider(tenantId);
            var tokenCredential1 = EntraCredentialFactory.CreateNewSdkTokenCredential(tenantId);

            EntraCredentialFactory.ClearCache();

            var credential2 = EntraCredentialFactory.CreateInteractiveBrowserCredential(tenantId);
            var callback2 = EntraCredentialFactory.CreateOldSdkAuthenticationCallback(tenantId);
            var provider2 = EntraCredentialFactory.CreateOldSdkTokenProvider(tenantId);
            var tokenCredential2 = EntraCredentialFactory.CreateNewSdkTokenCredential(tenantId);

            credential2.Should().NotBeSameAs(credential1, "ClearCache should force a fresh sign-in");
            callback2.Should().NotBeSameAs(callback1);
            provider2.Should().NotBeSameAs(provider1);
            tokenCredential2.Should().NotBeSameAs(tokenCredential1);
        }

        [Fact]
        public void EntraCredentialFactory_BlankTenant_ReusesSameCredentialAsOrganizations()
        {
            var fromNull = EntraCredentialFactory.CreateInteractiveBrowserCredential(null);
            var fromOrganizations = EntraCredentialFactory.CreateInteractiveBrowserCredential("organizations");

            fromOrganizations.Should().BeSameAs(fromNull);
        }

        [Fact]
        public void EntraCredentialFactory_GetScopes_UsesResourceValue()
        {
            var scopes = InvokePrivateStatic<string[]>(typeof(EntraCredentialFactory), "GetScopes", "https://servicebus.azure.net");

            scopes.Should().ContainSingle().Which.Should().Be("https://servicebus.azure.net/.default");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void EntraCredentialFactory_GetAuthority_BlankTenant_UsesOrganizationsEndpoint(string tenantId)
        {
            var authority = EntraCredentialFactory.GetAuthority(tenantId);

            authority.Should().Be("https://login.microsoftonline.com/organizations");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateEventHubProducerClient_NullOrEmptyPath_ThrowsArgumentExceptionWithParamName(string path)
        {
            var helper = new ServiceBusHelper((message, asynchronous) => { });

            var act = () => helper.CreateEventHubProducerClient(path);

            act.Should().Throw<ArgumentException>()
                .And.ParamName.Should().Be("path");
        }

        [Fact]
        public void EntraCredentialFactory_DifferentAudiences_ReturnDifferentProviders()
        {
            var sbProvider = EntraCredentialFactory.CreateOldSdkTokenProvider("tenant-id", EntraCredentialFactory.ServiceBusAudience);
            var ehProvider = EntraCredentialFactory.CreateOldSdkTokenProvider("tenant-id", EntraCredentialFactory.EventHubsAudience);

            ehProvider.Should().NotBeSameAs(sbProvider, "different audiences must produce different token providers");
        }

        [Fact]
        public void EntraCredentialFactory_SameAudience_ReusesCachedProvider()
        {
            var ehProvider1 = EntraCredentialFactory.CreateOldSdkTokenProvider("tenant-id", EntraCredentialFactory.EventHubsAudience);
            var ehProvider2 = EntraCredentialFactory.CreateOldSdkTokenProvider("tenant-id", EntraCredentialFactory.EventHubsAudience);

            ehProvider2.Should().BeSameAs(ehProvider1, "same tenant+audience should reuse cached provider");
        }

        [Fact]
        public void EntraCredentialFactory_DifferentAudiences_ReturnDifferentCallbacks()
        {
            var sbCallback = EntraCredentialFactory.CreateOldSdkAuthenticationCallback("tenant-id", EntraCredentialFactory.ServiceBusAudience);
            var ehCallback = EntraCredentialFactory.CreateOldSdkAuthenticationCallback("tenant-id", EntraCredentialFactory.EventHubsAudience);

            ehCallback.Should().NotBeSameAs(sbCallback, "different audiences must produce different callbacks");
        }

        [Fact]
        public void EntraCredentialFactory_EventHubsAudience_HasCorrectValue()
        {
            EntraCredentialFactory.EventHubsAudience.Should().Be("https://eventhubs.azure.net");
        }

        [Fact]
        public void EntraCredentialFactory_GetScopes_EventHubResource_UsesEventHubScope()
        {
            var scopes = InvokePrivateStatic<string[]>(typeof(EntraCredentialFactory), "GetScopes", "https://eventhubs.azure.net");

            scopes.Should().ContainSingle().Which.Should().Be("https://eventhubs.azure.net/.default");
        }

        [Fact]
        public void IsAudienceMismatch_InvalidAudienceSubstatus_ReturnsTrue()
        {
            var ex = new MessagingException("SubCode=40104. Token has invalid audience.");
            InvokePrivateStatic<bool>(typeof(ServiceBusHelper), "IsAudienceMismatchException", ex)
                .Should().BeTrue();
        }

        [Fact]
        public void IsAudienceMismatch_InvalidAudienceKeyword_ReturnsTrue()
        {
            var ex = new UnauthorizedAccessException("InvalidAudience: the token was issued for a different resource.");
            InvokePrivateStatic<bool>(typeof(ServiceBusHelper), "IsAudienceMismatchException", ex)
                .Should().BeTrue();
        }

        [Fact]
        public void IsAudienceMismatch_ManageClaimDenied_ReturnsFalse()
        {
            var ex = new UnauthorizedAccessException("40301: Manage claim is required for this operation.");
            InvokePrivateStatic<bool>(typeof(ServiceBusHelper), "IsAudienceMismatchException", ex)
                .Should().BeFalse();
        }

        [Fact]
        public void IsAudienceMismatch_Generic401_ReturnsTrueForBackwardCompat()
        {
            var ex = new MessagingException("The request was unauthorized. Status code: 401");
            InvokePrivateStatic<bool>(typeof(ServiceBusHelper), "IsAudienceMismatchException", ex)
                .Should().BeTrue();
        }

        [Fact]
        public void IsAudienceMismatch_UnrelatedMessagingException_ReturnsFalse()
        {
            var ex = new MessagingException("Entity not found. Status code: 404");
            InvokePrivateStatic<bool>(typeof(ServiceBusHelper), "IsAudienceMismatchException", ex)
                .Should().BeFalse();
        }

        [Theory]
        [InlineData(MsalError.CannotInvokeBroker)]
        [InlineData(MsalError.InvalidOwnerWindowType)]
        [InlineData(MsalError.PlatformNotSupported)]
        public void IsBrokerUnavailable_MsalBrokerErrorCodes_ReturnsTrue(string errorCode)
        {
            var ex = new MsalClientException(errorCode, "The broker could not be used.");
            InvokePrivateStatic<bool>(typeof(EntraCredentialFactory), "IsBrokerUnavailable", ex)
                .Should().BeTrue();
        }

        [Fact]
        public void IsBrokerUnavailable_MsalAuthenticationCanceled_ReturnsFalse()
        {
            var ex = new MsalClientException(MsalError.AuthenticationCanceledError, "The user cancelled sign-in.");
            InvokePrivateStatic<bool>(typeof(EntraCredentialFactory), "IsBrokerUnavailable", ex)
                .Should().BeFalse();
        }

        [Theory]
        [InlineData(typeof(DllNotFoundException))]
        [InlineData(typeof(EntryPointNotFoundException))]
        [InlineData(typeof(BadImageFormatException))]
        [InlineData(typeof(PlatformNotSupportedException))]
        public void IsBrokerUnavailable_NativeLoadFailureTypes_ReturnsTrue(Type exceptionType)
        {
            var ex = (Exception)Activator.CreateInstance(exceptionType);
            InvokePrivateStatic<bool>(typeof(EntraCredentialFactory), "IsBrokerUnavailable", ex)
                .Should().BeTrue();
        }

        [Fact]
        public void IsBrokerUnavailable_TypeInitializationException_ReturnsTrue()
        {
            var ex = new TypeInitializationException("SomeBrokerType", new DllNotFoundException("wamext.dll"));
            InvokePrivateStatic<bool>(typeof(EntraCredentialFactory), "IsBrokerUnavailable", ex)
                .Should().BeTrue();
        }

        [Fact]
        public void IsBrokerUnavailable_UnrelatedException_ReturnsFalse()
        {
            var ex = new InvalidOperationException("Something unrelated went wrong.");
            InvokePrivateStatic<bool>(typeof(EntraCredentialFactory), "IsBrokerUnavailable", ex)
                .Should().BeFalse();
        }

        [Fact]
        public void IsBrokerUnavailable_NestedInsideAuthenticationFailedException_ReturnsTrue()
        {
            var inner = new MsalClientException(MsalError.CannotInvokeBroker, "The broker could not be invoked.");
            var wrapped = new InvalidOperationException("Wrapper exception.", inner);
            InvokePrivateStatic<bool>(typeof(EntraCredentialFactory), "IsBrokerUnavailable", wrapped)
                .Should().BeTrue();
        }

        [Fact]
        public async Task CachedSdkEntryPointsHonorBrowserSelectionAndPreserveTokenRequests()
        {
            const string tenant = "browser-choice";
            var broker = new RecordingCredential("broker");
            var browser = new RecordingCredential("browser");
            var previousChoice = EntraCredentialFactory.UseSystemBrowserOnly;
            try
            {
                CacheCredential("interactiveBrowserCredentials", tenant, broker);
                CacheCredential("systemBrowserCredentials", tenant, browser);
                var modern = EntraCredentialFactory.CreateNewSdkTokenCredential(tenant);
                var legacy = EntraCredentialFactory.CreateOldSdkAuthenticationCallback(tenant, EntraCredentialFactory.EventHubsAudience);
                var request = new TokenRequestContext(new[] { EntraCredentialFactory.ServiceBusAudience + "/.default" },
                    "request-id", "claims challenge", tenant, isCaeEnabled: true);

                EntraCredentialFactory.UseSystemBrowserOnly = true;
                EntraCredentialFactory.CreateInteractiveBrowserCredential(tenant).Should().BeSameAs(browser);
                (await modern.GetTokenAsync(request, CancellationToken.None)).Token.Should().Be("browser");
                browser.LastRequest.Should().Be(request);
                (await legacy(null, null, null)).Should().Be("browser");
                browser.LastRequest.Scopes.Should().Equal(EntraCredentialFactory.EventHubsAudience + "/.default");
                broker.Calls.Should().Be(0);

                EntraCredentialFactory.UseSystemBrowserOnly = false;
                modern.GetToken(request, CancellationToken.None).Token.Should().Be("broker");
                broker.LastRequest.Should().Be(request);
                (await legacy(null, null, null)).Should().Be("broker");
                browser.Calls.Should().Be(2);
            }
            finally
            {
                EntraCredentialFactory.UseSystemBrowserOnly = previousChoice;
                EntraCredentialFactory.ClearCache();
            }
        }

        [Theory]
        [InlineData(false, MsalError.AuthenticationCanceledError, false)]
        [InlineData(true, MsalError.AuthenticationCanceledError, false)]
        [InlineData(false, "access_denied", false)]
        [InlineData(true, MsalError.UnknownBrokerError, false)]
        [InlineData(false, null, false)]
        [InlineData(true, "unexpected", false)]
        [InlineData(false, MsalError.AuthenticationCanceledError, true)]
        public void Connect_InteractiveFailureStopsAtFirstPrompt(bool browserOnly, string errorCode, bool eventHubs)
        {
            const string tenant = "cancel-connect";
            var helper = new ServiceBusHelper((m, a) => { });
            var previous = new ServiceBusNamespace();
            SetPrivateField(helper, "serviceBusNamespaceInstance", previous);
            SetPrivateField(helper, "connectionString", "previous connection");
            Exception cause = errorCode switch
            {
                null => new OperationCanceledException("Sign-in was canceled."),
                "unexpected" => new InvalidOperationException("Unexpected credential failure"),
                _ => new MsalClientException(errorCode, "Synthetic sign-in result")
            };
            var failureFromCredential = cause is MsalException
                ? new AuthenticationFailedException("Synthetic authentication failure", cause) : cause;
            var credential = new StubInteractiveCredential((_, ct) => throw failureFromCredential);
            var unexpected = new StubInteractiveCredential((_, ct) => throw new InvalidOperationException("Unexpected prompt"));
            var previousMode = EntraCredentialFactory.UseSystemBrowserOnly;
            var retries = RetryHelper.RetryCount;
            try
            {
                RetryHelper.RetryCount = 1;
                EntraCredentialFactory.UseSystemBrowserOnly = browserOnly;
                CacheCredential("interactiveBrowserCredentials", tenant, browserOnly ? unexpected : credential);
                CacheCredential("systemBrowserCredentials", tenant, browserOnly ? credential : unexpected);
                var target = new ServiceBusNamespace("sb://localhost/", "local", tenant, TransportType.Amqp);

                if (eventHubs)
                {
                    RejectServiceBusAudience(helper);
                }

                var failure = Record.Exception(() => helper.Connect(target));
                credential.Calls.Should().Be(1);
                unexpected.Calls.Should().Be(0);
                failure.Should().NotBeNull();
                failure.GetBaseException().Should().BeSameAs(cause);
                if (errorCode != null && errorCode != MsalError.AuthenticationCanceledError)
                {
                    for (var inner = failure; inner != null; inner = inner.InnerException)
                    {
                        inner.Should().NotBeAssignableTo<OperationCanceledException>();
                    }
                }
                GetPrivateField<ServiceBusNamespace>(helper, "serviceBusNamespaceInstance").Should().BeSameAs(previous);
                helper.ConnectionString.Should().Be("previous connection");
                helper.NamespaceManager.Should().BeNull();
                helper.IsEventHubNamespace.Should().BeFalse();
            }
            finally
            {
                RetryHelper.RetryCount = retries;
                EntraCredentialFactory.UseSystemBrowserOnly = previousMode;
                EntraCredentialFactory.ClearCache();
            }
        }

        [Fact]
        public async Task Connect_EventHubsAudienceReusesSuccessfulSignInForManagement()
        {
            const string tenant = "event-hubs-connect";
            var endpoint = new Uri("sb://localhost/");
            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                audience: EntraCredentialFactory.EventHubsAudience, expires: DateTime.UtcNow.AddHours(1)));
            var credential = new StubInteractiveCredential((_, ct) =>
                new ValueTask<AccessToken>(new AccessToken(token, DateTimeOffset.UtcNow.AddHours(1))));
            try
            {
                var helper = new ServiceBusHelper((_, __) => { });
                RejectServiceBusAudience(helper);
                CacheCredential("interactiveBrowserCredentials", tenant, credential);
                CacheCredential("systemBrowserCredentials", tenant, credential);
                var target = new ServiceBusNamespace(endpoint.AbsoluteUri, "local", tenant, TransportType.Amqp);
                helper.Connect(target).Should().BeTrue();
                helper.IsEventHubNamespace.Should().BeTrue();
                GetPrivateField<NamespaceManager>(helper, "namespaceManager").Should().BeNull();
                GetPrivateField<Lazy<NamespaceManager>>(helper, "legacyNamespaceManager").IsValueCreated.Should().BeFalse();
                var provider = helper.NamespaceManager.Settings.TokenProvider;
                var header = await provider.GetWebTokenAsync("https://localhost/hub", "Manage", false, TimeSpan.FromSeconds(2));
                header.Should().Contain(token);
                credential.Calls.Should().Be(1);
            }
            finally
            {
                EntraCredentialFactory.ClearCache();
            }
        }

        static void RejectServiceBusAudience(ServiceBusHelper helper)
        {
            // Simulate a service-side response at the migrated management probe boundary.
            Func<ServiceBusAdministrationClient, Task> reject = _ =>
                Task.FromException(new UnauthorizedAccessException("40104 InvalidAudience"));
            SetPrivateField(helper, "serviceBusAudienceProbe", reject);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task QueuedSdkRequestHonorsSignInOutcomeAcrossAudiences(bool browserOnly, bool callerCanceled)
        {
            const string tenant = "queued-connect";
            var pending = new TaskCompletionSource<AccessToken>(TaskCreationOptions.RunContinuationsAsynchronously);
            var credential = new StubInteractiveCredential((_, ct) => new ValueTask<AccessToken>(pending.Task));
            var other = new RecordingCredential("unexpected fallback");
            var recovery = new RecordingCredential("new token");
            var chosen = browserOnly ? "systemBrowserCredentials" : "interactiveBrowserCredentials";
            var previousMode = EntraCredentialFactory.UseSystemBrowserOnly;
            try
            {
                EntraCredentialFactory.UseSystemBrowserOnly = browserOnly;
                CacheCredential(chosen, tenant, credential);
                CacheCredential(browserOnly ? "interactiveBrowserCredentials" : "systemBrowserCredentials", tenant, other);
                var modern = EntraCredentialFactory.CreateNewSdkTokenCredential(tenant);
                var legacy = EntraCredentialFactory.CreateOldSdkAuthenticationCallback(tenant, EntraCredentialFactory.EventHubsAudience);
                var context = new TokenRequestContext(new[] { EntraCredentialFactory.ServiceBusAudience + "/.default" });
                using (var cancellation = new CancellationTokenSource())
                {
                    var first = modern.GetTokenAsync(context, cancellation.Token).AsTask();
                    var queued = legacy(null, null, null);
                    queued.IsCompleted.Should().BeFalse();
                    credential.Calls.Should().Be(1);
                    CacheCredential(chosen, tenant, recovery);
                    if (callerCanceled)
                    {
                        cancellation.Cancel();
                        pending.SetCanceled();
                        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => first);
                        (await queued).Should().Be("new token");
                        recovery.Calls.Should().Be(1);
                    }
                    else
                    {
                        var canceled = new AuthenticationFailedException("Sign-in closed",
                            new MsalClientException(MsalError.AuthenticationCanceledError, "User canceled"));
                        pending.SetException(canceled);
                        (await Record.ExceptionAsync(() => first)).Should().BeSameAs(canceled);
                        (await Record.ExceptionAsync(() => queued)).Should().BeSameAs(canceled);
                        recovery.Calls.Should().Be(0);
                        (await legacy(null, null, null)).Should().Be("new token");
                        recovery.Calls.Should().Be(1);
                    }
                    other.Calls.Should().Be(0);
                }
            }
            finally
            {
                pending.TrySetCanceled();
                EntraCredentialFactory.UseSystemBrowserOnly = previousMode;
                EntraCredentialFactory.ClearCache();
            }
        }

        internal sealed class StubInteractiveCredential : InteractiveBrowserCredential
        {
            readonly Func<TokenRequestContext, CancellationToken, ValueTask<AccessToken>> acquire;
            public int Calls { get; private set; }

            public StubInteractiveCredential(Func<TokenRequestContext, CancellationToken, ValueTask<AccessToken>> acquire)
            {
                this.acquire = acquire;
            }

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext context, CancellationToken cancellationToken)
            {
                Calls++;
                if (Calls > 1) throw new InvalidOperationException("A second interactive request started.");
                return acquire(context, cancellationToken);
            }
        }

        internal static void CacheCredential(string fieldName, string tenant, InteractiveBrowserCredential credential)
        {
            object value;
            if (fieldName == "interactiveBrowserCredentials")
            {
                var owner = InvokePrivateStatic<object>(typeof(EntraCredentialFactory), "CurrentBrokerWindow");
                value = owner.GetType().GetField("Credentials").GetValue(owner);
            }
            else
            {
                value = typeof(EntraCredentialFactory).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            }
            var cache = (ConcurrentDictionary<string, InteractiveBrowserCredential>)value;
            cache[tenant] = credential;
        }

        sealed class RecordingCredential : InteractiveBrowserCredential
        {
            readonly string token;
            public int Calls { get; private set; }
            public TokenRequestContext LastRequest { get; private set; }

            public RecordingCredential(string token) => this.token = token;

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                Calls++;
                LastRequest = requestContext;
                return new ValueTask<AccessToken>(new AccessToken(token, DateTimeOffset.UtcNow.AddMinutes(5)));
            }
        }

        [Fact]
        public async Task AcquireTokenWithFallback_BrokerUnavailable_UsesFallbackAndRetriesBrokerNextTime()
        {
            EntraCredentialFactory.ClearCache();
            var brokerCalls = 0;
            var fallbackCalls = 0;
            Func<CancellationToken, ValueTask<AccessToken>> broker = ct =>
            {
                brokerCalls++;
                if (brokerCalls == 1) throw new MsalClientException(MsalError.CannotInvokeBroker, "No broker.");
                return new ValueTask<AccessToken>(new AccessToken("broker-token", DateTimeOffset.UtcNow.AddMinutes(5)));
            };
            Func<CancellationToken, ValueTask<AccessToken>> fallback = ct =>
            {
                fallbackCalls++;
                return new ValueTask<AccessToken>(new AccessToken("fallback-token", DateTimeOffset.UtcNow.AddMinutes(5)));
            };

            var first = await AcquireWithFallback(broker, fallback);
            var second = await AcquireWithFallback(broker, fallback);

            first.Token.Should().Be("fallback-token");
            second.Token.Should().Be("broker-token");
            brokerCalls.Should().Be(2);
            fallbackCalls.Should().Be(1);
            EntraCredentialFactory.ClearCache();
        }

        [Fact]
        public async Task AcquireTokenWithFallback_WrappedMsalServiceBrokerError_UsesFallback()
        {
            EntraCredentialFactory.ClearCache();
            var fallbackCalls = 0;

            var token = await AcquireWithFallback(
                ct => throw new AuthenticationFailedException(
                    "Broker failed.",
                    new MsalServiceException(MsalError.CannotInvokeBroker, "Broker unavailable.")),
                ct =>
                {
                    fallbackCalls++;
                    return new ValueTask<AccessToken>(new AccessToken("fallback-token", DateTimeOffset.UtcNow.AddMinutes(5)));
                });

            token.Token.Should().Be("fallback-token");
            fallbackCalls.Should().Be(1);
            EntraCredentialFactory.ClearCache();
        }

        [Fact]
        public async Task AcquireTokenWithFallback_BrokerSucceeds_DoesNotUseFallback()
        {
            EntraCredentialFactory.ClearCache();
            var fallbackCalls = 0;

            var token = await AcquireWithFallback(
                ct => new ValueTask<AccessToken>(new AccessToken("broker-token", DateTimeOffset.UtcNow.AddMinutes(5))),
                ct =>
                {
                    fallbackCalls++;
                    return new ValueTask<AccessToken>(new AccessToken("fallback-token", DateTimeOffset.UtcNow.AddMinutes(5)));
                });

            token.Token.Should().Be("broker-token");
            fallbackCalls.Should().Be(0);
        }

        [Fact]
        public async Task AcquireTokenWithFallback_UserCancelled_PropagatesWithoutFallback()
        {
            EntraCredentialFactory.ClearCache();
            var fallbackCalls = 0;

            Func<Task> act = () => AcquireWithFallback(
                ct => throw new MsalClientException(MsalError.AuthenticationCanceledError, "Cancelled."),
                ct =>
                {
                    fallbackCalls++;
                    return new ValueTask<AccessToken>(new AccessToken("fallback-token", DateTimeOffset.UtcNow.AddMinutes(5)));
                });

            await act.Should().ThrowAsync<MsalClientException>();
            fallbackCalls.Should().Be(0);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task QueuedBrokerRequestUsesItsInitiatingWindowWhileItsHandleIsAlive(bool destroyWindow)
        {
            const string tenant = "queued-window-test";
            var main = new IntPtr(110);
            var tool = new IntPtr(120);
            var pending = new TaskCompletionSource<AccessToken>(TaskCreationOptions.RunContinuationsAsynchronously);
            var modern = EntraCredentialFactory.CreateNewSdkTokenCredential(tenant);
            var legacy = EntraCredentialFactory.CreateOldSdkAuthenticationCallback(tenant);
            var previousMode = EntraCredentialFactory.UseSystemBrowserOnly;
            try
            {
                EntraCredentialFactory.UseSystemBrowserOnly = false;
                EntraCredentialFactory.SetOwnerWindowHandle(main);
                CacheCredential("interactiveBrowserCredentials", tenant, new StubInteractiveCredential(
                    (context, token) => new ValueTask<AccessToken>(pending.Task)));
                var first = modern.GetTokenAsync(new TokenRequestContext(new[] { "scope" }), CancellationToken.None).AsTask();

                EntraCredentialFactory.SetOwnerWindowHandle(tool);
                CacheCredential("interactiveBrowserCredentials", tenant, new RecordingCredential("tool"));
                var queued = legacy(null, null, null);
                queued.IsCompleted.Should().BeFalse();
                EntraCredentialFactory.SetOwnerWindowHandle(main);
                CacheCredential("interactiveBrowserCredentials", tenant, new RecordingCredential("main"));
                if (destroyWindow)
                {
                    EntraCredentialFactory.ReleaseOwnerWindowHandle(tool, main);
                    // Windows can reuse the numeric HWND for a different window.
                    EntraCredentialFactory.SetOwnerWindowHandle(tool);
                    CacheCredential("interactiveBrowserCredentials", tenant, new RecordingCredential("reused"));
                    EntraCredentialFactory.SetOwnerWindowHandle(main);
                }

                pending.SetResult(new AccessToken("first", DateTimeOffset.UtcNow.AddMinutes(5)));
                (await first).Token.Should().Be("first");
                (await queued).Should().Be(destroyWindow ? "main" : "tool");
            }
            finally
            {
                pending.TrySetCanceled();
                EntraCredentialFactory.ReleaseOwnerWindowHandle(tool, main);
                EntraCredentialFactory.ReleaseOwnerWindowHandle(main, IntPtr.Zero);
                EntraCredentialFactory.UseSystemBrowserOnly = previousMode;
                EntraCredentialFactory.ClearCache();
            }
        }

        [Fact]
        public void QueueListenerWindowOwnsWorkerSignInAndRestoresMainOnClose()
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                const string tenant = "listener-window-test";
                var previousMode = EntraCredentialFactory.UseSystemBrowserOnly;
                var previousCrossThreadCheck = System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls;
                var endpoint = new Uri("sb://localhost/");
                var expectedHandle = IntPtr.Zero;
                var requestedHandle = IntPtr.Zero;
                var canceled = new StubInteractiveCredential((context, token) =>
                    throw new OperationCanceledException("Synthetic dismissal"));
                var modernCredential = EntraCredentialFactory.CreateNewSdkTokenCredential(tenant);
                var credential = new ObservingTokenCredential(modernCredential, () =>
                {
                    requestedHandle = GetStaticOwner();
                    if (requestedHandle != expectedHandle || requestedHandle == IntPtr.Zero)
                        throw new AuthenticationFailedException("Listener requested a token before it owned a window.");
                    CacheCredential("interactiveBrowserCredentials", tenant, canceled);
                });
                var administration = new ServiceBusAdministrationClient(endpoint.Host, credential);
                try
                {
                    EntraCredentialFactory.UseSystemBrowserOnly = false;
                    System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = true;
                    using (var main = new RecreatingOwnerForm())
                    {
                        EntraWindowOwners.Register(main, mainWindow: true);
                        main.Show();
                        var mainCredential = EntraCredentialFactory.CreateInteractiveBrowserCredential(tenant);
                        var helper = new ServiceBusHelper((message, asynchronous) => { }) { NamespaceUri = endpoint };
                        var manager = new ServiceBusManagementClient(administration, endpoint);
                        var queueType = typeof(ServiceBusHelper).Assembly.GetType("ServiceBusExplorer.WindowsAzure.ServiceBusQueue", true);
                        var ns = new ServiceBusNamespace(endpoint.AbsoluteUri, "local", tenant, TransportType.Amqp);
                        SetPrivateField(helper, "serviceBusQueue", Activator.CreateInstance(queueType, ns, manager));
                        using (var listener = new ContainerForm(helper, null, Enums.FormTypeEnum.Listener, new QueueDescription("test-queue")))
                        {
                            canceled.Calls.Should().Be(0);
                            listener.HandleCreated += (sender, args) => expectedHandle = listener.Handle;
                            try
                            {
                                // Create Queue Listener calls Show(). Its initial GetQueue goes through
                                // the real modern SDK and fails at our credential before any network I/O.
                                listener.Show();
                                System.Windows.Forms.Application.DoEvents();
                                expectedHandle.Should().NotBe(IntPtr.Zero);
                                requestedHandle.Should().Be(expectedHandle);
                                canceled.Calls.Should().Be(1);
                                listener.IsDisposed.Should().BeTrue();
                            }
                            finally
                            {
                                if (!listener.IsDisposed) listener.Close();
                            }
                        }
                        GetStaticOwner().Should().Be(main.Handle);
                        EntraCredentialFactory.CreateInteractiveBrowserCredential(tenant).Should().BeSameAs(mainCredential);
                    }
                    GetStaticOwner().Should().Be(IntPtr.Zero);
                }
                catch (Exception ex) { failure = ex; }
                finally
                {
                    System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = previousCrossThreadCheck;
                    EntraCredentialFactory.UseSystemBrowserOnly = previousMode;
                    EntraCredentialFactory.ClearCache();
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null) throw new Exception("Queue listener owner regression", failure);
        }

        [Fact]
        public void MainFormOwnerRegistrationRefreshesCredentialsAfterRealHandleRecreation()
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try
                {
                    using (var form = new RecreatingOwnerForm())
                    {
                        EntraWindowOwners.Register(form, mainWindow: true);
                        var handle = form.Handle;
                        var first = EntraCredentialFactory.CreateInteractiveBrowserCredential("window-test");
                        using (var tool = new RecreatingOwnerForm())
                        using (var other = new RecreatingOwnerForm())
                        {
                            EntraWindowOwners.Register(tool);
                            var toolHandle = tool.Handle;
                            tool.RaiseActivated();
                            GetStaticOwner().Should().Be(toolHandle);
                            var toolCredential = EntraCredentialFactory.CreateInteractiveBrowserCredential("window-test");
                            toolCredential.Should().NotBeSameAs(first);

                            // A sign-in dialog does not register, even if it takes focus.
                            var otherHandle = other.Handle;
                            other.RaiseActivated();
                            form.Recreate();
                            GetStaticOwner().Should().Be(toolHandle);
                            EntraCredentialFactory.CreateInteractiveBrowserCredential("window-test").Should().BeSameAs(toolCredential);
                        }
                        GetStaticOwner().Should().Be(form.Handle);
                        EntraCredentialFactory.CreateInteractiveBrowserCredential("window-test").Should().NotBeSameAs(first);
                    }
                    GetStaticOwner().Should().Be(IntPtr.Zero);
                }
                catch (Exception ex) { failure = ex; }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null) throw new Exception("Owner registration regression", failure);
        }

        static IntPtr GetStaticOwner() => (IntPtr)typeof(EntraCredentialFactory)
            .GetField("ownerWindowHandle", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

        sealed class RecreatingOwnerForm : System.Windows.Forms.Form
        {
            public void Recreate() => RecreateHandle();
            public void RaiseActivated() => OnActivated(EventArgs.Empty);
        }

        [Fact]
        public void OwnerHandleChangeInvalidatesEveryTenantButSameHandlePreservesCache()
        {
            EntraCredentialFactory.SetOwnerWindowHandle(new IntPtr(101));
            var first = EntraCredentialFactory.CreateInteractiveBrowserCredential("owner-one");
            var second = EntraCredentialFactory.CreateInteractiveBrowserCredential("owner-two");
            EntraCredentialFactory.SetOwnerWindowHandle(new IntPtr(101));
            EntraCredentialFactory.CreateInteractiveBrowserCredential("owner-one").Should().BeSameAs(first);
            EntraCredentialFactory.SetOwnerWindowHandle(IntPtr.Zero);
            EntraCredentialFactory.CreateInteractiveBrowserCredential("owner-one").Should().NotBeSameAs(first);
            EntraCredentialFactory.CreateInteractiveBrowserCredential("owner-two").Should().NotBeSameAs(second);
            var destroyed = EntraCredentialFactory.CreateInteractiveBrowserCredential("owner-one");
            EntraCredentialFactory.SetOwnerWindowHandle(new IntPtr(202));
            EntraCredentialFactory.CreateInteractiveBrowserCredential("owner-one").Should().NotBeSameAs(destroyed);
            EntraCredentialFactory.SetOwnerWindowHandle(IntPtr.Zero);
        }

        [Theory]
        [InlineData("access_denied")]
        [InlineData("invalid_grant")]
        [InlineData("interaction_required")]
        [InlineData(MsalError.UnknownBrokerError)]
        [InlineData(MsalError.AuthenticationCanceledError)]
        public async Task AuthenticationFailuresNeverFallBack(string code)
        {
            var fallbackCalls = 0;
            Func<Task> action = () => AcquireWithFallback(ct => throw new MsalServiceException(code, "Synthetic denial"), ct =>
            {
                fallbackCalls++;
                return new ValueTask<AccessToken>(new AccessToken());
            });
            await action.Should().ThrowAsync<MsalServiceException>();
            fallbackCalls.Should().Be(0);
        }

        [Fact]
        public async Task CancellationDuringBrokerFailureNeverOpensFallback()
        {
            using (var cancellation = new CancellationTokenSource())
            {
                var fallbackCalls = 0;
                Func<Task> action = () => InvokePrivateStatic<Task<AccessToken>>(
                    typeof(EntraCredentialFactory), "AcquireTokenWithFallbackAsync", new Action(() => { }),
                    new Func<CancellationToken, ValueTask<AccessToken>>(ct =>
                    {
                        cancellation.Cancel();
                        throw new MsalClientException(MsalError.CannotInvokeBroker, "Synthetic failure");
                    }),
                    new Func<CancellationToken, ValueTask<AccessToken>>(ct =>
                    {
                        fallbackCalls++;
                        return new ValueTask<AccessToken>(new AccessToken());
                    }), cancellation.Token);
                await action.Should().ThrowAsync<MsalClientException>();
                fallbackCalls.Should().Be(0);
            }
        }

        [Fact]
        public void WrappedDenialAndUnknownInitializationFailureAreNotInfrastructureFailures()
        {
            InvokePrivateStatic<bool>(typeof(EntraCredentialFactory), "IsBrokerUnavailable",
                new TypeInitializationException("Synthetic", new InvalidOperationException())).Should().BeFalse();
            InvokePrivateStatic<bool>(typeof(EntraCredentialFactory), "IsBrokerUnavailable",
                new MsalClientException("access_denied", "Synthetic", new DllNotFoundException())).Should().BeFalse();
        }

        static Task<AccessToken> AcquireWithFallback(
            Func<CancellationToken, ValueTask<AccessToken>> broker,
            Func<CancellationToken, ValueTask<AccessToken>> fallback)
        {
            return InvokePrivateStatic<Task<AccessToken>>(
                typeof(EntraCredentialFactory),
                "AcquireTokenWithFallbackAsync",
                new Action(() => { }),
                broker,
                fallback,
                CancellationToken.None);
        }

        [Theory]
        [InlineData("")]
        [InlineData(";EntityPath=hub1")]
        [InlineData(";TransportType=Amqp")]
        public async Task EventHubFactoriesAcceptModernConnectionStrings(string suffix)
        {
            var helper = new ServiceBusHelper((message, asynchronous) => { });
            var connection = "Endpoint=sb://myns.servicebus.windows.net/;SharedAccessKeyName=service;SharedAccessKey=c2VjcmV0" + suffix;
            SetPrivateField(helper, "connectionString", connection);
            var producer = helper.CreateEventHubProducerClient("hub1");
            var consumer = helper.CreateEventHubConsumerClient("hub1", "$Default", connection);
            try
            {
                producer.EventHubName.Should().Be("hub1");
                consumer.EventHubName.Should().Be("hub1");
                producer.FullyQualifiedNamespace.Should().Be("myns.servicebus.windows.net");
                consumer.FullyQualifiedNamespace.Should().Be(producer.FullyQualifiedNamespace);
            }
            finally
            {
                await producer.DisposeAsync();
                await consumer.DisposeAsync();
            }
        }

        static T GetPrivateField<T>(object instance, string fieldName)
        {
            var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            field.Should().NotBeNull();
            return (T)field.GetValue(instance);
        }

        static T InvokePrivateStatic<T>(Type type, string methodName, params object[] args)
        {
            var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            method.Should().NotBeNull();
            return (T)method.Invoke(null, args);
        }

        static void SetPrivateField(object instance, string fieldName, object value)
        {
            var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            field.Should().NotBeNull();
            field.SetValue(instance, value);
        }

        sealed class ObservingTokenCredential : TokenCredential
        {
            readonly TokenCredential inner;
            readonly Action beforeToken;

            public ObservingTokenCredential(TokenCredential inner, Action beforeToken)
            {
                this.inner = inner;
                this.beforeToken = beforeToken;
            }

            public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                beforeToken();
                return inner.GetToken(requestContext, cancellationToken);
            }

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                beforeToken();
                return inner.GetTokenAsync(requestContext, cancellationToken);
            }
        }

        sealed class FakeTokenCredential : TokenCredential
        {
            public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return new AccessToken("fake-token", DateTimeOffset.UtcNow.AddMinutes(5));
            }

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken)
            {
                return new ValueTask<AccessToken>(new AccessToken("fake-token", DateTimeOffset.UtcNow.AddMinutes(5)));
            }
        }
    }
}
