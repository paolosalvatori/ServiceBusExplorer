using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.ResourceManager;
using TransportType = Microsoft.ServiceBus.Messaging.TransportType;
using Newtonsoft.Json.Linq;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventHubArmManagementTests
    {
        internal const string Tenant = "11111111-1111-1111-1111-111111111111";
        internal const string HubId = "/subscriptions/22222222-2222-2222-2222-222222222222/resourceGroups/rg.ops/providers/Microsoft.EventHub/namespaces/example-ns/eventhubs/orders";
        private const string Host = "example-ns.servicebus.windows.net";

        [Fact]
        public void OpeningFromTheExplorerHelperLeavesTheCurrentConnectionAlone()
        {
            var helper = new ServiceBusHelper((_, __) => { });
            Assert.Equal(HubId, helper.OpenArmEventHub(HubId, Tenant).ResourceId, ignoreCase: true);
            Assert.Null(helper.NamespaceUri);
            Assert.Null(helper.NamespaceManager);
            Assert.False(helper.IsEntra);
        }

        [Theory]
        [InlineData("")]
        [InlineData("/consumergroups/other")]
        [InlineData("?other=value")]
        [InlineData("%2Fother")]
        [InlineData("/..")]
        public void RequiresAnExactHubResourceIdBeforeCreatingCredentials(string suffix)
        {
            var input = suffix.Length == 0 ? HubId.Substring(0, HubId.LastIndexOf("/eventhubs", StringComparison.Ordinal)) : HubId + suffix;
            Assert.Throws<ArgumentException>(() => new EventHubArmManagement(input, null, Tenant,
                _ => throw new InvalidOperationException("unexpected credential access")));
        }

        [Theory]
        [InlineData("other-ns.servicebus.windows.net", "orders")]
        [InlineData(Host, "another-hub")]
        [InlineData(Host, "orders/consumergroups/workers")]
        [InlineData("example-ns.servicebus.chinacloudapi.cn", null)]
        public void ConnectedProfileCannotLeaveItsCloudNamespaceOrHub(string host, string scope)
        {
            var profile = new ServiceBusNamespace("sb://" + host + "/", "alias", Tenant, TransportType.Amqp, scope);
            Assert.Throws<ArgumentException>(() => new EventHubArmManagement(HubId, profile, null,
                _ => throw new InvalidOperationException("unexpected credential access")));
        }

        [Fact]
        public void RejectsSasTenantOverridesAndMissingStandaloneTenantBeforeSignIn()
        {
            TokenCredential Unexpected(string _) => throw new Exception("unexpected credential access");
            Assert.Throws<InvalidOperationException>(() => new EventHubArmManagement(HubId, new ServiceBusNamespace(), Tenant, Unexpected));
            var profile = new ServiceBusNamespace("sb://" + Host, "alias", Tenant, TransportType.Amqp);
            Assert.Throws<ArgumentException>(() => new EventHubArmManagement(HubId, profile, Guid.NewGuid().ToString(), Unexpected));
            Assert.Throws<ArgumentException>(() => new EventHubArmManagement(HubId, null, null, Unexpected));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task ReadsOnlyTheExactHubAndAllItsGroupPagesWithArmAuthorization(bool connected)
        {
            var profile = connected ? new ServiceBusNamespace("sb://" + Host + "/", "alias", Tenant, TransportType.Amqp, "ORDERS") : null;
            using var wire = new Wire();
            var result = wire.Open(profile);
            var hub = await result.GetHubAsync(default);
            var groups = await result.GetGroupsAsync(default);
            Assert.Equal("orders", hub.Name);
            Assert.Equal(2, hub.PartitionCount);
            Assert.Equal(new[] { "$Default", "workers" }, groups.Select(g => g.Name));
            Assert.Equal("original", groups[1].UserMetadata);
            Assert.Equal(new[] { "GET", "GET", "GET" }, wire.Requests.Select(x => x.Method));
            Assert.Equal(new[] { HubId, HubId + "/consumergroups", HubId + "/consumergroups" }.Select(s => s.ToLowerInvariant()),
                wire.Requests.Select(x => x.Uri.AbsolutePath.ToLowerInvariant()));
            Assert.All(wire.Requests, x => Assert.Equal("management.azure.com", x.Uri.Host));
            Assert.All(wire.Requests, x => Assert.Equal("Bearer", x.AuthScheme));
            Assert.Contains("api-version=2026-01-01", wire.Requests[0].Uri.Query);
            Assert.Contains("page=2", wire.Requests[2].Uri.Query);
            Assert.Equal(Tenant, wire.RequestedTenant);
            Assert.All(wire.Credential.Scopes, s => Assert.Equal("https://management.azure.com//.default", s));
            Assert.NotEmpty(wire.Credential.Scopes);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  one\r\ntwo Ω  ")]
        public async Task UpsertWritesOnlyTheChosenGroupsMetadataAndPreservesItsValue(string value)
        {
            using var wire = new Wire();
            var saved = await wire.Open().SetGroupMetadataAsync("workers", value, default);
            var request = Assert.Single(wire.Requests);
            Assert.Equal("PUT", request.Method);
            Assert.Equal(HubId + "/consumergroups/workers", request.Uri.AbsolutePath, ignoreCase: true);
            Assert.True(JToken.DeepEquals(new JObject { ["properties"] = new JObject { ["userMetadata"] = value } }, JObject.Parse(request.Body)), request.Body);
            Assert.Equal(value, saved.UserMetadata);
        }

        [Theory]
        [InlineData("other/group", "value")]
        [InlineData("../group", "value")]
        [InlineData("%2Fother", "value")]
        [InlineData("a", null)]
        public async Task InvalidGroupOrMetadataNeverReachesArm(string name, string value)
        {
            using var wire = new Wire();
            var management = wire.Open();
            await Assert.ThrowsAsync<ArgumentException>(() => management.SetGroupMetadataAsync(name, value, default));
            await Assert.ThrowsAsync<ArgumentException>(() => management.SetGroupMetadataAsync("a", new string('a', 1025), default));
            Assert.Empty(wire.Requests);
        }

        [Theory]
        [InlineData(401)]
        [InlineData(403)]
        [InlineData(409)]
        public async Task ArmDenialIsReturnedWithoutASecondOperation(int status)
        {
            using var wire = new Wire { Failure = (HttpStatusCode)status };
            var failure = await Assert.ThrowsAsync<RequestFailedException>(() => wire.Open().SetGroupMetadataAsync("workers", "new", default));
            Assert.Equal(status, failure.Status);
            Assert.Equal("PUT", Assert.Single(wire.Requests).Method);
        }

        [Theory]
        [InlineData("https://management.azure.com/subscriptions/other/consumergroups")]
        [InlineData("https://elsewhere.example/consumergroups")]
        public async Task DoesNotFollowAPageOutsideTheSelectedGroupCollection(string nextLink)
        {
            using var wire = new Wire { NextLink = nextLink };
            await Assert.ThrowsAsync<InvalidOperationException>(() => wire.Open().GetGroupsAsync(default));
            Assert.Single(wire.Requests);
        }

        [Fact]
        public async Task EquivalentEscapingOnTheSameCollectionLoadsItsSecondPage()
        {
            var id = HubId.Replace("rg.ops", "rg(ops)");
            using var wire = new Wire { NextLink = "https://management.azure.com" + id.Replace("(ops)", "%28ops%29") +
                "/consumergroups?api-version=2026-01-01&page=2" };
            var groups = await wire.Open(resourceId: id).GetGroupsAsync(default);
            Assert.Equal(new[] { "$Default", "workers" }, groups.Select(g => g.Name));
            Assert.Equal(2, wire.Requests.Count);
        }

        internal sealed class RecordingCredential : TokenCredential
        {
            public readonly List<string> Scopes = new List<string>();
            public override AccessToken GetToken(TokenRequestContext context, CancellationToken cancel)
            {
                Scopes.AddRange(context.Scopes);
                return new AccessToken("local-test", DateTimeOffset.UtcNow.AddHours(1));
            }
            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext context, CancellationToken cancel) =>
                new ValueTask<AccessToken>(GetToken(context, cancel));
        }

        internal sealed class Wire : HttpMessageHandler
        {
            public readonly List<(string Method, Uri Uri, string Body, string AuthScheme)> Requests = new List<(string, Uri, string, string)>();
            public readonly RecordingCredential Credential = new RecordingCredential();
            public HttpStatusCode? Failure;
            public bool OmitPutMetadata;
            public string NextLink;
            public TaskCompletionSource<bool> PendingPut;
            public CancellationToken PutToken;
            public string RequestedTenant;
            private readonly HttpClient http;

            public Wire() { http = new HttpClient(this, false); }
            public EventHubArmManagement Open(ServiceBusNamespace profile = null, string resourceId = HubId)
            {
                var options = new ArmClientOptions { Transport = new HttpClientTransport(http) };
                options.Retry.MaxRetries = 0;
                return new EventHubArmManagement(resourceId, profile, profile == null ? Tenant : null,
                    tenant => { RequestedTenant = tenant; return Credential; }, options);
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
            {
                var uri = request.RequestUri;
                var body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
                Requests.Add((request.Method.Method, uri, body, request.Headers.Authorization?.Scheme));
                JObject json;
                if (Failure.HasValue)
                    json = new JObject { ["error"] = new JObject { ["code"] = Failure == HttpStatusCode.Conflict ? "MissingSubscriptionRegistration" : "AuthorizationFailed", ["message"] = "Denied" } };
                else if (request.Method == HttpMethod.Put)
                {
                    if (PendingPut != null)
                    {
                        PutToken = token;
                        // The server may finish a write even after cancellation reaches the client.
                        await PendingPut.Task.ConfigureAwait(false);
                    }
                    json = Group(uri.Segments.Last(), (string)JObject.Parse(body)["properties"]["userMetadata"]);
                    if (OmitPutMetadata) ((JObject)json["properties"]).Property("userMetadata").Remove();
                }
                else if (uri.AbsolutePath.EndsWith("/consumergroups", StringComparison.OrdinalIgnoreCase))
                {
                    var more = !uri.Query.Contains("page=2");
                    json = new JObject { ["value"] = new JArray(more ? Group("$Default", null) : Group("workers", "original")) };
                    if (more) json["nextLink"] = NextLink ?? "https://management.azure.com" + HubId + "/consumergroups?api-version=2026-01-01&page=2";
                }
                else
                    json = new JObject { ["id"] = HubId, ["name"] = "orders", ["type"] = "Microsoft.EventHub/namespaces/eventhubs",
                        ["properties"] = new JObject { ["partitionCount"] = 2, ["status"] = "Active" } };
                return new HttpResponseMessage(Failure ?? HttpStatusCode.OK)
                    { Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json") };
            }

            private static JObject Group(string name, string metadata) => new JObject
            {
                ["id"] = HubId + "/consumergroups/" + name, ["name"] = name,
                ["type"] = "Microsoft.EventHub/namespaces/eventhubs/consumergroups",
                ["properties"] = new JObject { ["userMetadata"] = metadata }
            };

            protected override void Dispose(bool disposing)
            {
                if (disposing) http.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
