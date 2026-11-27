using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ManualWorkerThinkTimeTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CancellationInterruptsThinkTimeAfterOneOperation(bool sending)
        {
            var entered = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var helper = new ServiceBusHelper((entry, _) =>
            {
                if (entry.StartsWith("Sleeping for", StringComparison.Ordinal)) entered.TrySetResult(true);
            }) { NamespaceUri = new Uri("sb://synthetic.example/") };
            using var cancellation = new CancellationTokenSource();
            using var template = ServiceBusReceivedMessageAdapter.ToBrokeredMessage(
                ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("sample"), messageId: "sample", sequenceNumber: 1));
            var sender = new Sender();
            var receiver = new Receiver();
            long next = -1;
            const int thinkTime = 30000;
            string summary = null;
            var worker = Task.Run(() => sending
                ? helper.SendMessages(sender, "queue", new[] { template }, () => ++next, 2, 0, false,
                    false, false, false, false, false, false, false, 1, true, thinkTime, BodyType.Stream,
                    null, null, cancellation, out summary)
                : helper.ReceiveMessages(receiver, ServiceBusReceiveMode.ReceiveAndDelete, false, 0, 1,
                    null, false, false, false, false, false, false, false, 1, true, thinkTime,
                    null, null, cancellation, out summary));

            try
            {
                var first = await Task.WhenAny(entered.Task, worker, Task.Delay(TimeSpan.FromSeconds(5)));
                if (first == worker) await worker;
                Assert.True(first == entered.Task, summary ?? "Worker did not enter think time.");
                cancellation.Cancel();
                Assert.Same(worker, await Task.WhenAny(worker, Task.Delay(TimeSpan.FromSeconds(5))));
                Assert.True(await worker, summary);
                Assert.Equal(sending ? 1 : 0, sender.Sends);
                Assert.Equal(sending ? 0 : 1, receiver.Reads);
            }
            finally { cancellation.Cancel(); }
        }

        private sealed class Sender : ServiceBusSender
        {
            public int Sends { get; private set; }
            public override Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken = default)
            {
                Sends++;
                return Task.CompletedTask;
            }
        }

        private sealed class Receiver : ServiceBusReceiver
        {
            public int Reads { get; private set; }
            public override Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan? maxWaitTime = null,
                CancellationToken cancellationToken = default)
            {
                Reads++;
                return Task.FromResult(ServiceBusModelFactory.ServiceBusReceivedMessage(body: new BinaryData("sample")));
            }
        }
    }
}
