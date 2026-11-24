using System;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;
using FluentAssertions;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventHubClientCleanupTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task StartsAllProducerClosesAndWaitsForEveryClientEvenOnFailure(bool fails)
        {
            var first = new Producer { Fails = fails };
            var second = new Producer();
            var cleanup = EventHubClientCleanup.DisposeProducersAsync(new[] { first, second });
            first.Calls.Should().Be(1);
            second.Calls.Should().Be(1);
            cleanup.IsCompleted.Should().BeFalse();
            first.Closed.SetResult(true);
            cleanup.IsCompleted.Should().BeFalse();
            second.Closed.SetResult(true);
            if (fails)
            {
                Func<Task> wait = () => cleanup;
                await wait.Should().ThrowAsync<InvalidOperationException>();
            }
            else await cleanup;
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task SharedConsumerIsClosedOnlyAfterAllReadersFinish(bool fails)
        {
            var first = new TaskCompletionSource<bool>();
            var second = new TaskCompletionSource<bool>();
            var client = new Consumer();
            var cleanup = EventHubClientCleanup.DisposeConsumerAfterReadersAsync(client, new[] { first.Task, second.Task });
            if (fails) first.SetException(new InvalidOperationException("reader failure"));
            else first.SetResult(true);
            client.Calls.Should().Be(0);
            cleanup.IsCompleted.Should().BeFalse();
            second.SetResult(true);
            if (fails)
            {
                Func<Task> wait = () => cleanup;
                await wait.Should().ThrowAsync<InvalidOperationException>();
            }
            else await cleanup;
            client.Calls.Should().Be(1);
        }

        private sealed class Producer : EventHubProducerClient
        {
            public int Calls;
            public bool Fails;
            public TaskCompletionSource<bool> Closed = new TaskCompletionSource<bool>();
            public override ValueTask DisposeAsync()
            {
                Calls++;
                if (Fails) throw new InvalidOperationException("close failure");
                return new ValueTask(Closed.Task);
            }
        }

        private sealed class Consumer : EventHubConsumerClient
        {
            public int Calls;
            public override ValueTask DisposeAsync() { Calls++; return default; }
        }
    }
}
