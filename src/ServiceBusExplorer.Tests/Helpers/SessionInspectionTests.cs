using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class SessionInspectionTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("subscription")]
        public async Task ContentionIsReturnedAndFreshInspectionReadsTheReleasedSession(string subscription)
        {
            var held = new Client { Held = true };
            var failure = await Assert.ThrowsAsync<ServiceBusException>(() => Inspect(held, subscription));
            Assert.Equal(ServiceBusFailureReason.SessionCannotBeLocked, failure.Reason);
            Assert.True(held.Closed);
            Assert.False(held.Receiver.Closed);

            var released = new Client();
            Assert.Equal(new byte[] { 4, 2 }, await Inspect(released, subscription));
            Assert.Equal("entity", released.Entity);
            Assert.Equal(subscription, released.Subscription);
            Assert.Equal("selected", released.Session);
            Assert.True(released.Closed);
            Assert.True(released.Receiver.Closed);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task InspectionDeadlineIncludesPendingAcceptAndResourceCleanup(bool blockCleanup)
        {
            var client = new Client();
            var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            if (blockCleanup) client.Receiver.CloseGate = gate.Task;
            else client.AcceptGate = gate.Task;
            try
            {
                var inspection = SessionBrowser.ReadStateAsync(client, "entity", null, "selected",
                    TimeSpan.FromMilliseconds(120), CancellationToken.None);
                Assert.Same(inspection, await Task.WhenAny(inspection, Task.Delay(TimeSpan.FromSeconds(5))));
                await Assert.ThrowsAsync<TimeoutException>(() => inspection);
                Assert.True(client.AcceptToken.IsCancellationRequested);
                Assert.False(client.Closed);
            }
            finally { gate.TrySetResult(true); }
            Assert.Same(client.Closing.Task, await Task.WhenAny(client.Closing.Task, Task.Delay(5000)));
            Assert.True(client.Receiver.Closed);
        }

        [Fact]
        public async Task CallerCancellationRemainsCancellationDuringStateInspection()
        {
            var gate = new TaskCompletionSource<bool>();
            var client = new Client { AcceptGate = gate.Task };
            using var caller = new CancellationTokenSource();
            var inspection = SessionBrowser.ReadStateAsync(client, "entity", null, "selected", TimeSpan.FromMinutes(1), caller.Token);
            caller.Cancel();
            try
            {
                await Assert.ThrowsAnyAsync<OperationCanceledException>(() => inspection);
                Assert.True(client.AcceptToken.IsCancellationRequested);
            }
            finally { gate.TrySetResult(true); }
        }

        private static Task<byte[]> Inspect(Client client, string subscription) =>
            SessionBrowser.ReadStateAsync(client, "entity", subscription, "selected", TimeSpan.FromSeconds(5), CancellationToken.None);

        private sealed class Client : ServiceBusClient
        {
            public readonly Receiver Receiver = new Receiver();
            public readonly TaskCompletionSource<bool> Closing = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            public bool Held, Closed;
            public Task AcceptGate = Task.CompletedTask;
            public CancellationToken AcceptToken;
            public string Entity, Subscription, Session;

            public override Task<ServiceBusSessionReceiver> AcceptSessionAsync(string queueName, string sessionId,
                ServiceBusSessionReceiverOptions options = null, CancellationToken cancellationToken = default) =>
                Accept(queueName, null, sessionId, cancellationToken);
            public override Task<ServiceBusSessionReceiver> AcceptSessionAsync(string topicName, string subscriptionName, string sessionId,
                ServiceBusSessionReceiverOptions options = null, CancellationToken cancellationToken = default) =>
                Accept(topicName, subscriptionName, sessionId, cancellationToken);

            private async Task<ServiceBusSessionReceiver> Accept(string entity, string subscription, string session, CancellationToken token)
            {
                Entity = entity;
                Subscription = subscription;
                Session = session;
                AcceptToken = token;
                await AcceptGate;
                if (Held) throw new ServiceBusException("held", ServiceBusFailureReason.SessionCannotBeLocked);
                return Receiver;
            }

            public override ValueTask DisposeAsync()
            {
                Closed = true;
                Closing.TrySetResult(true);
                return default;
            }
        }

        private sealed class Receiver : ServiceBusSessionReceiver
        {
            public bool Closed;
            public Task CloseGate = Task.CompletedTask;
            public override Task<BinaryData> GetSessionStateAsync(CancellationToken cancellationToken = default) =>
                Task.FromResult(new BinaryData(new byte[] { 4, 2 }));
            public override async ValueTask DisposeAsync()
            {
                await CloseGate;
                Closed = true;
            }
        }
    }
}
