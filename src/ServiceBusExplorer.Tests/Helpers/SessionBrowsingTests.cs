using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Xunit;
using ServiceBusExplorer.Helpers;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class SessionBrowsingTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("subscription")]
        public async Task BrowseRoutesWithoutAcceptingSessionsAndDisposesClient(string subscription)
        {
            var client = new Client();
            using var cancellation = new CancellationTokenSource();
            var result = await SessionBrowser.ListAsync(client, "entity", subscription, cancellation.Token);
            Assert.Equal(new[] { "one", "two" }, result.ConvertAll(s => s.SessionId));
            Assert.Equal("entity", client.Entity);
            Assert.Equal(subscription, client.Subscription);
            Assert.Equal(cancellation.Token, client.Token);
            Assert.True(client.Disposed);
        }

        [Fact]
        public async Task EnumerationFailurePropagatesAndDisposesClient()
        {
            var client = new Client { Fail = true };
            await Assert.ThrowsAsync<NotSupportedException>(() => SessionBrowser.ListAsync(client, "entity", null, CancellationToken.None));
            Assert.True(client.Disposed);
        }

        [Fact]
        public async Task CancellationDoesNotReturnPartialResults()
        {
            using var cancellation = new CancellationTokenSource();
            var client = new Client { Cancel = cancellation };
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => SessionBrowser.ListAsync(client, "entity", null, cancellation.Token));
            Assert.True(client.Disposed);
        }

        // Any accidental AcceptSession/AcceptNextSession call reaches the unconfigured
        // SDK mock and fails, rather than yielding a synthetic successful receiver.
        private sealed class Client : ServiceBusClient
        {
            public string Entity, Subscription;
            public CancellationToken Token;
            public bool Fail, Disposed;
            public CancellationTokenSource Cancel;
            public override IAsyncEnumerable<string> GetMessageSessionsAsync(string queueName, CancellationToken cancellationToken = default)
                => Browse(queueName, null, cancellationToken);
            public override IAsyncEnumerable<string> GetMessageSessionsAsync(string topicName, string subscriptionName, CancellationToken cancellationToken = default)
                => Browse(topicName, subscriptionName, cancellationToken);
            private async IAsyncEnumerable<string> Browse(string entity, string subscription, [EnumeratorCancellation] CancellationToken token)
            {
                Entity = entity; Subscription = subscription; Token = token;
                await Task.Yield();
                yield return "one";
                if (Fail) throw new NotSupportedException("Endpoint does not support session browsing.");
                Cancel?.Cancel();
                token.ThrowIfCancellationRequested();
                yield return "two";
            }
            public override ValueTask DisposeAsync() { Disposed = true; return default; }
        }
    }
}
