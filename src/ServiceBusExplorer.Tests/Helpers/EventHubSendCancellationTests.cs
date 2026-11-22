using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventHubSendCancellationTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task TransientFailuresUseOneBoundedApplicationRetryBudget(bool batch)
        {
            var producer = new Producer { Send = token => throw new EventHubsException(true, "outage", EventHubsException.FailureReason.ServiceCommunicationProblem) };
            using (var cancellation = new CancellationTokenSource())
            {
                Func<Task> send = () => SendAsync(producer, cancellation, batch);
                await send.Should().ThrowAsync<Exception>().WithMessage("*outage*");
                producer.Calls.Should().Be(RetryHelper.RetryCount + 1);
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NonTransientNonSdkFailuresAreNotRetried(bool batch)
        {
            var producer = new Producer { Send = token => throw new InvalidOperationException("invalid send") };
            using (var cancellation = new CancellationTokenSource())
            {
                Func<Task> send = () => SendAsync(producer, cancellation, batch);
                await send.Should().ThrowAsync<Exception>().WithMessage("*invalid send*");
                producer.Calls.Should().Be(1);
            }
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public async Task ModernFailuresRespectTransienceForSingleAndBatchSends(bool batch, bool transient)
        {
            var producer = new Producer();
            producer.Send = token =>
            {
                if (producer.Calls == 1 || !transient)
                    throw new EventHubsException(transient, "simulated failure", EventHubsException.FailureReason.ResourceNotFound);
                return Task.CompletedTask;
            };
            using (var cancellation = new CancellationTokenSource())
            {
                Func<Task> send = () => SendAsync(producer, cancellation, batch, sameKey: true);
                if (transient) await send();
                else await send.Should().ThrowAsync<Exception>().WithMessage("*simulated failure*");
                producer.Calls.Should().Be(transient ? 2 : 1);
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CancellationInterruptsInFlightSdkSend(bool batch)
        {
            var started = new TaskCompletionSource<bool>();
            var producer = new Producer();
            producer.Send = token => { started.SetResult(true); return Task.Delay(Timeout.Infinite, token); };
            using (var cancellation = new CancellationTokenSource())
            {
                var send = SendAsync(producer, cancellation, batch);
                await started.Task;
                producer.Token.Should().Be(cancellation.Token);
                cancellation.Cancel();
                await CompletesPromptly(send);
                (await send).Should().Contain("Message Count=[0]");
                producer.Calls.Should().Be(1);
            }
        }

        [Fact]
        public async Task CancellationAfterAcknowledgmentPreventsLaterPartitionGroups()
        {
            using (var cancellation = new CancellationTokenSource())
            {
                var producer = new Producer { Send = token => { cancellation.Cancel(); return Task.CompletedTask; } };
                var summary = await SendAsync(producer, cancellation, true);
                summary.Should().Contain("Message Count=[1]");
                producer.Calls.Should().Be(1);
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CancellationDuringRetryBackoffDoesNotRetry(bool batch)
        {
            using (var cancellation = new CancellationTokenSource())
            {
                var producer = new Producer { Send = token => throw new TimeoutException("retry me") };
                var send = SendAsync(producer, cancellation, batch, log: message => cancellation.Cancel());
                await CompletesPromptly(send);
                producer.Calls.Should().Be(1);
                (await send).Should().Contain("Message Count=[0]");
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CancellationInterruptsLongThinkTime(bool batch)
        {
            using (var cancellation = new CancellationTokenSource())
            {
                var producer = new Producer();
                var send = SendAsync(producer, cancellation, batch, sameKey: true, think: true,
                    log: message => cancellation.Cancel());
                await CompletesPromptly(send);
                producer.Calls.Should().Be(1);
                (await send).Should().Contain(batch ? "Message Count=[2]" : "Message Count=[1]");
            }
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task AlreadyCanceledSenderDoesNotSend(bool batch)
        {
            using (var cancellation = new CancellationTokenSource())
            {
                cancellation.Cancel();
                var producer = new Producer();
                await SendAsync(producer, cancellation, batch);
                producer.Calls.Should().Be(0);
            }
        }

        private static async Task<string> SendAsync(Producer producer, CancellationTokenSource cancellation,
            bool batch, bool sameKey = false, bool think = false, Action<string> log = null)
        {
            var helper = new ServiceBusHelper((message, async) => log?.Invoke(message));
            using (var first = new EventDataEnvelope(new MemoryStream(new byte[] { 1 })) { PartitionKey = "first" })
            using (var second = new EventDataEnvelope(new MemoryStream(new byte[] { 2 })) { PartitionKey = sameKey ? "first" : "second" })
            {
                long next = 0;
                return await helper.SendEventData(producer, new[] { first, second }, () => next++, batch ? 2 : 1, 0,
                    updatePartitionKey: false, noPartitionKey: false, addMessageNumber: true,
                    logging: false, verbose: false, statistics: false, sendBatch: batch, batchSize: 2,
                    senderThinkTime: think, thinkTime: 60000, eventDataInspector: null, updateStatistics: null,
                    cancellationTokenSource: cancellation);
            }
        }

        private static async Task CompletesPromptly(Task task)
        {
            (await Task.WhenAny(task, Task.Delay(5000))).Should().BeSameAs(task);
            await task;
        }

        private sealed class Producer : EventHubProducerClient
        {
            public int Calls;
            public CancellationToken Token;
            public Func<CancellationToken, Task> Send = token => Task.CompletedTask;
            public override Task SendAsync(IEnumerable<EventData> events, SendEventOptions options, CancellationToken cancellationToken = default)
            {
                Calls++;
                Token = cancellationToken;
                return Send(cancellationToken);
            }
        }
    }
}
