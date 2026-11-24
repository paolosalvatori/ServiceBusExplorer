using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Controls
{
    public class NotificationHubRegistrationRequestTests
    {
        [Fact]
        public async Task PendingSelectionDoesNotStartPagingWithAnOldContinuationToken()
        {
            var view = new TestView();
            var selected = NewResponse();
            var selecting = view.Start(selected.Task, selection: Query.Selected);

            Assert.False(view.Requests.CanPage);
            var blocked = view.Start(Task.FromResult("old page"), continuation: "old token");
            await blocked;

            Assert.Equal((Query.Selected, null), Assert.Single(view.Loads));
            selected.SetResult("selected page");
            await selecting;

            Assert.Equal("selected page", Assert.Single(view.Applied));
            Assert.Equal(Query.Selected, view.Requests.CommittedQuery);
            Assert.True(view.Requests.CanPage);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task OldContinuationCannotOverwriteNewSelectionOrShowItsError(bool oldPageFails)
        {
            var view = new TestView();
            var oldPage = NewResponse();
            var paging = view.Start(oldPage.Task, continuation: "old token");
            await view.Start(Task.FromResult("selected page"), selection: Query.Selected);

            if (oldPageFails)
            {
                oldPage.SetException(new InvalidOperationException("old request failed"));
            }
            else
            {
                oldPage.SetResult("old page");
            }
            await paging;

            Assert.Equal("selected page", Assert.Single(view.Applied));
            Assert.Equal(Query.Selected, view.Requests.CommittedQuery);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task NewFirstPageRefreshFollowsPendingSelectionAndDiscardsEarlierResponse(bool newestFinishesFirst)
        {
            var view = new TestView();
            var beforeWrite = NewResponse();
            var afterWrite = NewResponse();
            var selecting = view.Start(beforeWrite.Task, selection: Query.Selected);
            var refreshing = view.Start(afterWrite.Task);

            Assert.Equal(new[] { (Query.Selected, (string)null), (Query.Selected, (string)null) }, view.Loads);
            Assert.Equal(Query.Initial, view.Requests.CommittedQuery);

            if (newestFinishesFirst)
            {
                afterWrite.SetResult("after creation");
                await refreshing;
            }
            beforeWrite.SetResult("before creation");
            await selecting;
            if (!newestFinishesFirst)
            {
                Assert.Empty(view.Applied);
                Assert.False(view.Requests.CanPage);
                afterWrite.SetResult("after creation");
                await refreshing;
            }

            Assert.Equal("after creation", Assert.Single(view.Applied));
            Assert.Equal(Query.Selected, view.Requests.CommittedQuery);
            Assert.True(view.Requests.CanPage);
        }

        [Fact]
        public async Task FailedLatestSelectionPreservesTheCommittedViewAndQuery()
        {
            var view = new TestView();
            await view.Start(Task.FromResult("initial page"));
            var failure = new InvalidOperationException("selected query failed");

            var error = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                view.Start(Task.FromException<string>(failure), selection: Query.Selected));

            Assert.Same(failure, error);
            Assert.Equal("initial page", Assert.Single(view.Applied));
            Assert.Equal(Query.Initial, view.Requests.CommittedQuery);
            Assert.True(view.Requests.CanPage);
            await view.Start(Task.FromResult("refreshed initial page"));

            Assert.Equal((Query.Initial, null), view.Loads[2]);
            Assert.Equal(new[] { "initial page", "refreshed initial page" }, view.Applied);
        }

        [Fact]
        public async Task FailedLatestRefreshDoesNotAllowSupersededFirstPageToCommit()
        {
            var view = new TestView();
            await view.Start(Task.FromResult("initial page"));
            var superseded = NewResponse();
            var selecting = view.Start(superseded.Task, selection: Query.Selected);
            var failure = new InvalidOperationException("refresh failed");

            var error = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                view.Start(Task.FromException<string>(failure)));
            superseded.SetResult("superseded page");
            await selecting;

            Assert.Same(failure, error);
            Assert.Equal((Query.Selected, null), view.Loads[2]);
            Assert.Equal("initial page", Assert.Single(view.Applied));
            Assert.Equal(Query.Initial, view.Requests.CommittedQuery);
            Assert.True(view.Requests.CanPage);
        }

        [Fact]
        public async Task LatestSelectionDoesNotCommitWhenPageProjectionFails()
        {
            var view = new TestView();
            await view.Start(Task.FromResult("initial page"));
            var failure = new InvalidOperationException("page projection failed");
            var result = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                view.Requests.RunAsync(null, Query.Selected,
                    (_, __) => Task.FromResult("invalid page"),
                    (_, __) => throw failure, () => true));

            Assert.Same(failure, result);
            Assert.Equal("initial page", Assert.Single(view.Applied));
            Assert.Equal(Query.Initial, view.Requests.CommittedQuery);
            Assert.True(view.Requests.CanPage);
        }

        private static TaskCompletionSource<string> NewResponse() =>
            new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

        private enum Query
        {
            Initial,
            Selected
        }

        private sealed class TestView
        {
            public RegistrationPageRequests<Query> Requests { get; } = new RegistrationPageRequests<Query>(Query.Initial);
            public List<(Query Selection, string Continuation)> Loads { get; } = new List<(Query, string)>();
            public List<string> Applied { get; } = new List<string>();

            public Task Start(Task<string> response, Query? selection = null, string continuation = null) =>
                Requests.RunAsync(continuation, selection, (query, token) =>
                {
                    Loads.Add((query, token));
                    return response;
                }, (_, page) => Applied.Add(page), () => true);
        }
    }
}
