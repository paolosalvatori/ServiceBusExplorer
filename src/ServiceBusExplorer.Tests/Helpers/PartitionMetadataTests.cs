using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using FluentAssertions;
using ServiceBusExplorer.Controls;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class PartitionMetadataTests
    {
        [Fact]
        public async Task StartsAllRequestsBeforeWaitingAndPublishesInPartitionOrder()
        {
            var client = new MetadataClient();
            var fetch = PartitionListenerControl.FetchPartitionPropertiesAsync(client, new[] { "0", "1" });
            client.Requests.Keys.Should().Equal("0", "1");
            fetch.IsCompleted.Should().BeFalse();
            var second = EventHubsModelFactory.PartitionProperties("hub", "1", false, 0, 1, "1", DateTimeOffset.UtcNow);
            var first = EventHubsModelFactory.PartitionProperties("hub", "0", false, 0, 1, "1", DateTimeOffset.UtcNow);
            client.Requests["1"].SetResult(second);
            fetch.IsCompleted.Should().BeFalse("no partial result should be published");
            client.Requests["0"].SetResult(first);
            (await fetch).Should().Equal(first, second);
            client.Disposed.Should().BeFalse();
        }

        [Fact]
        public async Task FailedRequestWaitsForOtherRequestsBeforeDisposingClient()
        {
            var client = new MetadataClient();
            var fetch = PartitionListenerControl.FetchPartitionPropertiesAsync(client, new[] { "0", "1" });
            client.Requests["0"].SetException(new InvalidOperationException("metadata failure"));
            client.Disposed.Should().BeFalse();
            client.Requests["1"].SetResult(null);
            Func<Task> wait = () => fetch;
            await wait.Should().ThrowAsync<InvalidOperationException>().WithMessage("metadata failure");
            client.Disposed.Should().BeTrue();
        }

        private sealed class MetadataClient : EventHubConsumerClient
        {
            public Dictionary<string, TaskCompletionSource<PartitionProperties>> Requests { get; } =
                new Dictionary<string, TaskCompletionSource<PartitionProperties>>();
            public bool Disposed { get; private set; }

            public override Task<PartitionProperties> GetPartitionPropertiesAsync(string partitionId,
                CancellationToken cancellationToken = default)
            {
                var completion = new TaskCompletionSource<PartitionProperties>();
                Requests.Add(partitionId, completion);
                return completion.Task;
            }

            public override ValueTask DisposeAsync()
            {
                Disposed = true;
                return default;
            }
        }
    }
}
