using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.ResourceManager;
using Azure.ResourceManager.EventGrid;
using EventGridExplorerLibrary;

namespace ServiceBusExplorer.Tests.Helpers
{
    internal sealed class EventGridArmTransport : HttpMessageHandler
    {
        private const string SubscriptionId = "00000000-0000-0000-0000-000000000000";
        internal const string TopicsPath = "/subscriptions/" + SubscriptionId +
            "/resourceGroups/group/providers/Microsoft.EventGrid/namespaces/store/topics";
        private readonly Queue<(HttpResponseMessage Response, Func<Task> BeforeResponse)> responses =
            new Queue<(HttpResponseMessage, Func<Task>)>();
        internal List<(HttpMethod Method, Uri Uri, string Body)> Requests { get; } = new List<(HttpMethod, Uri, string)>();

        internal EventGridControlPlaneClient CreateClient()
        {
            var options = new ArmClientOptions { Transport = new HttpClientTransport(new HttpClient(this)) };
            options.Retry.MaxRetries = 0;
            return new EventGridControlPlaneClient(SubscriptionId, new LocalTokenCredential(), options);
        }

        internal static object Topic(string name) =>
            new { id = TopicsPath + "/" + name, name, type = NamespaceTopicResource.ResourceType.ToString(), properties = new { } };

        internal void AddRefresh(params string[] names) => AddTopicPage(names);

        internal void AddTopicPage(string[] names, string nextLink = null) =>
            AddResponse(new { value = names.Select(Topic), nextLink });

        internal void AddKeys(string key) => AddResponse(new { key1 = key, key2 = key });

        internal void AddResponse(object body, HttpStatusCode status = HttpStatusCode.OK, Func<Task> beforeResponse = null)
        {
            var response = new HttpResponseMessage(status)
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };
            responses.Enqueue((response, beforeResponse));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var body = request.Content == null ? null : await request.Content.ReadAsStringAsync();
            Requests.Add((request.Method, request.RequestUri, body));
            if (responses.Count == 0)
            {
                throw new InvalidOperationException($"Unexpected ARM request: {request.Method} {request.RequestUri}.");
            }
            var next = responses.Dequeue();
            if (next.BeforeResponse != null)
            {
                await next.BeforeResponse();
            }
            return next.Response;
        }

        private sealed class LocalTokenCredential : TokenCredential
        {
            public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken) =>
                new AccessToken("test-token", DateTimeOffset.UtcNow.AddHours(1));

            public override ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken) =>
                new ValueTask<AccessToken>(GetToken(requestContext, cancellationToken));
        }
    }
}
