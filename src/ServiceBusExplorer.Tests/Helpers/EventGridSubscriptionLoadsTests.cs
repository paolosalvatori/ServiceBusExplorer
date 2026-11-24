using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class EventGridSubscriptionLoadsTests
    {
        private const string TopicName = "test-topic";
        private readonly EventGridSubscriptionLoads<object> loads = new EventGridSubscriptionLoads<object>();
        private readonly object topic = new object();
        private readonly List<string> applied = new List<string>();
        private readonly List<Exception> failures = new List<Exception>();

        public EventGridSubscriptionLoadsTests() => loads.Track(TopicName, topic);

        [Fact]
        public async Task NewerRefreshSupersedesOlderResultsForTheSameTopic()
        {
            var older = PendingRead();
            Func<bool> isOlderCurrent = null;
            var former = loads.LoadAsync(TopicName, topic, () => true, isCurrent =>
            {
                isOlderCurrent = isCurrent;
                return older.Task;
            }, applied.Add, failures.Add);
            Assert.True(isOlderCurrent());

            await LoadAsync(Task.FromResult("current"));
            Assert.False(isOlderCurrent());
            older.SetResult("stale");
            await former;

            Assert.Equal(new[] { "current" }, applied);
            Assert.Empty(failures);
        }

        [Fact]
        public async Task OlderFailureAfterNewerSuccessIsIgnored()
        {
            var older = PendingRead();
            var former = LoadAsync(older.Task);

            await LoadAsync(Task.FromResult("current"));
            older.SetException(new InvalidOperationException("older failure"));
            await former;

            Assert.Equal(new[] { "current" }, applied);
            Assert.Empty(failures);
        }

        [Fact]
        public async Task CurrentReadFailureLeavesPreviouslyAppliedSubscriptionsUnchanged()
        {
            await LoadAsync(Task.FromResult("confirmed"));
            var error = new InvalidOperationException("current failure");

            await LoadAsync(Task.FromException<string>(error));

            Assert.Equal(new[] { "confirmed" }, applied);
            Assert.Same(error, Assert.Single(failures));
        }

        [Fact]
        public async Task ReplacingDisplayedTreeRejectsOldReadAndFindsReplacementUnderSameName()
        {
            var pending = PendingRead();
            var oldRead = LoadAsync(pending.Task);
            loads.Reset();
            var replacement = new object();
            loads.Track(TopicName, replacement);
            await loads.LoadAsync(TopicName, replacement, () => true, _ => Task.FromResult("replacement"),
                applied.Add, failures.Add);
            pending.SetResult("old tree");
            await oldRead;

            Assert.Same(replacement, loads.Find(TopicName));
            Assert.Equal(new[] { "replacement" }, applied);
            Assert.Empty(failures);
        }

        [Fact]
        public async Task LosingActiveConnectionRejectsPendingResult()
        {
            var connected = true;
            var pending = PendingRead();
            var oldRead = loads.LoadAsync(TopicName, topic, () => connected, _ => pending.Task, applied.Add, failures.Add);

            connected = false;
            pending.SetResult("disconnected");
            await oldRead;

            Assert.Empty(applied);
            Assert.Empty(failures);
        }

        private Task LoadAsync(Task<string> read) =>
            loads.LoadAsync(TopicName, topic, () => true, _ => read, applied.Add, failures.Add);

        private static TaskCompletionSource<string> PendingRead() =>
            new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
    }
}
