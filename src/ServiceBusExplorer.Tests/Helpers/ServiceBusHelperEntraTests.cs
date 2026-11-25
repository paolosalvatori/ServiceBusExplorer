using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using FluentAssertions;
using Microsoft.Identity.Client;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
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

        static void CacheCredential(string fieldName, string tenant, InteractiveBrowserCredential credential)
        {
            var field = typeof(EntraCredentialFactory).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            var cache = (ConcurrentDictionary<string, InteractiveBrowserCredential>)field.GetValue(null);
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
            const string tenantId = "broker-fallback-tenant";
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

            var first = await AcquireWithFallback(tenantId, broker, fallback);
            var second = await AcquireWithFallback(tenantId, broker, fallback);

            first.Token.Should().Be("fallback-token");
            second.Token.Should().Be("broker-token");
            brokerCalls.Should().Be(2);
            fallbackCalls.Should().Be(1);
            EntraCredentialFactory.ClearCache();
        }

        [Fact]
        public async Task AcquireTokenWithFallback_WrappedMsalServiceBrokerError_UsesFallback()
        {
            const string tenantId = "broker-service-error-tenant";
            EntraCredentialFactory.ClearCache();
            var fallbackCalls = 0;

            var token = await AcquireWithFallback(
                tenantId,
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
            const string tenantId = "broker-success-tenant";
            EntraCredentialFactory.ClearCache();
            var fallbackCalls = 0;

            var token = await AcquireWithFallback(
                tenantId,
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
            const string tenantId = "broker-cancel-tenant";
            EntraCredentialFactory.ClearCache();
            var fallbackCalls = 0;

            Func<Task> act = () => AcquireWithFallback(
                tenantId,
                ct => throw new MsalClientException(MsalError.AuthenticationCanceledError, "Cancelled."),
                ct =>
                {
                    fallbackCalls++;
                    return new ValueTask<AccessToken>(new AccessToken("fallback-token", DateTimeOffset.UtcNow.AddMinutes(5)));
                });

            await act.Should().ThrowAsync<MsalClientException>();
            fallbackCalls.Should().Be(0);
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
                        typeof(ServiceBusExplorer.Forms.MainForm).GetMethod("RegisterEntraOwner", BindingFlags.Static | BindingFlags.NonPublic)
                            .Invoke(null, new object[] { form });
                        var handle = form.Handle;
                        var first = EntraCredentialFactory.CreateInteractiveBrowserCredential("window-test");
                        form.Recreate();
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
            Func<Task> action = () => AcquireWithFallback("denied", ct => throw new MsalServiceException(code, "Synthetic denial"), ct =>
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
                    typeof(EntraCredentialFactory), "AcquireTokenWithFallbackAsync", "cancelled",
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
            string tenantId,
            Func<CancellationToken, ValueTask<AccessToken>> broker,
            Func<CancellationToken, ValueTask<AccessToken>> fallback)
        {
            return InvokePrivateStatic<Task<AccessToken>>(
                typeof(EntraCredentialFactory),
                "AcquireTokenWithFallbackAsync",
                tenantId,
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
