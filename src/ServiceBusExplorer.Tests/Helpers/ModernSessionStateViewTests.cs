using System;
using System.Collections.Generic;
using System.Text;
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
