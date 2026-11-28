using System;
using System.Collections.Generic;
using System.Text;
using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ModernSessionStateViewTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task ModernStateSelectionKeepsCurrentResultAndClearsOnDeselection(bool oldReadFails)
        {
            string text = "previous";
            var errors = new List<Exception>();
            var view = new SessionStateView(value => text = value, errors.Add, () => true);
            var old = new TaskCompletionSource<byte[]>();
            var first = view.ReadModernAsync(new SessionInfo("old"), _ => old.Task);
            Assert.Equal(string.Empty, text);
            await view.ReadModernAsync(new SessionInfo("new"), id => Task.FromResult(Encoding.UTF8.GetBytes(id)));
            if (oldReadFails) old.SetException(new InvalidOperationException("stale failure"));
            else old.SetResult(Encoding.UTF8.GetBytes("stale success"));
            await first;
            Assert.Equal("new", text);
            Assert.Empty(errors);
            await view.ReadModernAsync(null, _ => throw new InvalidOperationException("unexpected read"));
            Assert.Equal(string.Empty, text);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task RevisitedSessionSharesOnlyTheOutstandingRead(bool readFails)
        {
            string text = null;
            var errors = new List<Exception>();
            var reads = new List<string>();
            var view = new SessionStateView(value => text = value, errors.Add, () => true);
            var first = new TaskCompletionSource<byte[]>();
            var other = new TaskCompletionSource<byte[]>();
            Task<byte[]> Read(string id)
            {
                reads.Add(id);
                return id == "A" ? first.Task : other.Task;
            }

            var original = view.ReadModernAsync(new SessionInfo("A"), Read);
            var different = view.ReadModernAsync(new SessionInfo("a"), Read);
            var revisited = view.ReadModernAsync(new SessionInfo("A"), Read);
            Assert.Equal(new[] { "A", "a" }, reads);

            var failure = new InvalidOperationException("state unavailable");
            if (readFails) first.SetException(failure);
            else first.SetResult(Encoding.UTF8.GetBytes("current"));
            await Task.WhenAll(original, revisited);
            other.SetResult(Encoding.UTF8.GetBytes("stale"));
            await different;

            Assert.Equal(readFails ? string.Empty : "current", text);
            if (readFails) Assert.Same(failure, Assert.Single(errors));
            else Assert.Empty(errors);
            await view.ReadModernAsync(new SessionInfo("A"), _ => Task.FromResult(Encoding.UTF8.GetBytes("fresh")));
            Assert.Equal("fresh", text);
        }

        [Theory]
        [InlineData(ServiceBusFailureReason.SessionCannotBeLocked, "locked by another receiver")]
        [InlineData(ServiceBusFailureReason.ServiceTimeout, "timed out")]
        public async Task BusyAndTimeoutAreNonfatalAndReselectingReadsFreshState(ServiceBusFailureReason reason, string status)
        {
            string text = null;
            var errors = new List<Exception>();
            var view = new SessionStateView(value => text = value, errors.Add, () => true);
            var selection = new SessionInfo("selected");
            await view.ReadModernAsync(selection, _ => Task.FromException<byte[]>(new ServiceBusException("broker", reason)));
            Assert.Contains(status, text);
            await view.ReadModernAsync(selection, _ => Task.FromException<byte[]>(new TimeoutException()));
            Assert.Contains("timed out", text);
            await view.ReadModernAsync(selection, _ => Task.FromResult(Encoding.UTF8.GetBytes("available")));
            Assert.Equal("available", text);
            Assert.Empty(errors);
        }

        [Fact]
        public async Task ModernStateFailureIsReportedAndClosedViewDiscardsResult()
        {
            string text = "previous";
            bool open = true;
            var errors = new List<Exception>();
            var view = new SessionStateView(value => text = value, errors.Add, () => open);
            var failure = new InvalidOperationException("current failure");
            await view.ReadModernAsync(new SessionInfo("selected"), _ => Task.FromException<byte[]>(failure));
            Assert.Same(failure, Assert.Single(errors));
            Assert.Equal(string.Empty, text);
            var pending = new TaskCompletionSource<byte[]>();
            var reading = view.ReadModernAsync(new SessionInfo("selected"), _ => pending.Task);
            open = false;
            pending.SetResult(Encoding.UTF8.GetBytes("closed"));
            await reading;
            Assert.Equal(string.Empty, text);
            Assert.Single(errors);
        }
    }
}
