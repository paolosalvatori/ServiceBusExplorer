using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ListenerShutdownCoordinatorTests
    {
        [Fact]
        public void CheckpointFileWriteFailureIsReportedToTheClosingCaller()
        {
            var writeFile = typeof(EventProcessorCheckpointHelper).GetMethod("WriteFile", BindingFlags.NonPublic | BindingFlags.Static);
            // An existing directory cannot be overwritten as a checkpoint file.
            Action write = () => writeFile.Invoke(null, new object[] { Path.GetTempPath(), "checkpoint" });
            var error = write.Should().Throw<TargetInvocationException>().Which.InnerException;
            (error is IOException || error is UnauthorizedAccessException).Should().BeTrue();
        }

        [Fact]
        public async Task RepeatedCloseWaitsForEveryFinalCheckpointAndPersistsOnce()
        {
            var coordinator = new ListenerShutdownCoordinator();
            var first = new TaskCompletionSource<bool>();
            var second = new TaskCompletionSource<bool>();
            long savedOffset = 0;
            long persistedOffset = 0;
            int writes = 0;
            var batch = new EventHubCheckpointBatch(100, TimeSpan.FromHours(1), async lease =>
            { await first.Task; savedOffset = lease.SequenceNumber; });
            await batch.ProcessedAsync(new Lease { SequenceNumber = 42 }, true);
            coordinator.Register(() => batch.FlushAsync(true));
            coordinator.Register(() => second.Task);
            Action persist = () => { writes++; persistedOffset = savedOffset; };
            var close = coordinator.StopAndPersistAsync(persist);
            coordinator.StopAndPersistAsync(persist).Should().BeSameAs(close);
            coordinator.CanClose.Should().BeFalse();
            writes.Should().Be(0);
            first.SetResult(true);
            close.IsCompleted.Should().BeFalse();
            second.SetResult(true);
            await close;
            persistedOffset.Should().Be(42);
            coordinator.CanClose.Should().BeTrue();
            await coordinator.StopAndPersistAsync(persist);
            writes.Should().Be(1);
        }

        [Fact]
        public async Task FailedShutdownStillWaitsForOtherListenersAndAllowsCloseRetry()
        {
            var coordinator = new ListenerShutdownCoordinator();
            var second = new TaskCompletionSource<bool>();
            int attempts = 0, writes = 0, otherStops = 0;
            coordinator.Register(() => ++attempts == 1 ? throw new InvalidOperationException("flush failed") : Task.CompletedTask);
            coordinator.Register(() => { otherStops++; return second.Task; });
            var close = coordinator.StopAndPersistAsync(() => writes++);
            otherStops.Should().Be(1);
            close.IsCompleted.Should().BeFalse();
            second.SetResult(true);
            Func<Task> wait = () => close;
            await wait.Should().ThrowAsync<InvalidOperationException>();
            writes.Should().Be(0);
            coordinator.CanClose.Should().BeFalse();
            await coordinator.StopAndPersistAsync(() => writes++);
            attempts.Should().Be(2);
            writes.Should().Be(1);
        }

        [Fact]
        public async Task ListenerDisposalCanUnregisterWhileCloseAwaitsItsFlush()
        {
            var coordinator = new ListenerShutdownCoordinator();
            var pendingFlush = new TaskCompletionSource<bool>();
            Func<Task> shutdown = () => pendingFlush.Task;
            coordinator.Register(shutdown);
            bool persisted = false;
            var close = coordinator.StopAndPersistAsync(() => persisted = true);
            coordinator.Unregister(shutdown);
            persisted.Should().BeFalse();
            pendingFlush.SetResult(true);
            await close;
            persisted.Should().BeTrue();
        }

        [Fact]
        public async Task PersistenceFailureDoesNotAuthorizeCloseAndCanBeRetried()
        {
            var coordinator = new ListenerShutdownCoordinator();
            Func<Task> close = () => coordinator.StopAndPersistAsync(() => throw new InvalidOperationException("write failed"));
            await close.Should().ThrowAsync<InvalidOperationException>();
            coordinator.CanClose.Should().BeFalse();
            await coordinator.StopAndPersistAsync(() => { });
            coordinator.CanClose.Should().BeTrue();
        }
    }
}
