using System;
using System.Threading.Tasks;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class RetryHelperTests
    {
        static void NoOpLog(string message, bool async = true) { }

        [Fact]
        public void RetryFunc_OperationCanceledException_DoesNotRetry()
        {
            int callCount = 0;

            Action act = () => RetryHelper.RetryFunc<bool>(() =>
            {
                callCount++;
                throw new OperationCanceledException("cancelled");
            }, NoOpLog);

            act.Should().Throw<OperationCanceledException>()
                .WithMessage("cancelled");
            callCount.Should().Be(1, "OperationCanceledException must not be retried");
        }

        [Fact]
        public void RetryAction_OperationCanceledException_DoesNotRetry()
        {
            int callCount = 0;

            Action act = () => RetryHelper.RetryAction(() =>
            {
                callCount++;
                throw new OperationCanceledException("cancelled");
            }, NoOpLog);

            act.Should().Throw<OperationCanceledException>()
                .WithMessage("cancelled");
            callCount.Should().Be(1, "OperationCanceledException must not be retried");
        }

        [Fact]
        public async Task RetryFuncAsync_OperationCanceledException_DoesNotRetry()
        {
            int callCount = 0;

            Func<Task> act = () => RetryHelper.RetryFuncAsync<bool>(async () =>
            {
                callCount++;
                await Task.CompletedTask;
                throw new OperationCanceledException("cancelled");
            }, NoOpLog);

            await act.Should().ThrowAsync<OperationCanceledException>()
                .WithMessage("cancelled");
            callCount.Should().Be(1, "OperationCanceledException must not be retried");
        }

        [Fact]
        public async Task RetryActionAsync_OperationCanceledException_DoesNotRetry()
        {
            int callCount = 0;

            Func<Task> act = () => RetryHelper.RetryActionAsync(async () =>
            {
                callCount++;
                await Task.CompletedTask;
                throw new OperationCanceledException("cancelled");
            }, NoOpLog);

            await act.Should().ThrowAsync<OperationCanceledException>()
                .WithMessage("cancelled");
            callCount.Should().Be(1, "OperationCanceledException must not be retried");
        }

        [Fact]
        public void RetryFunc_WrappedAuthFailure_DoesNotRetry()
        {
            // Simulates the AadCredentialFactory pattern: AuthenticationFailedException
            // is wrapped in OperationCanceledException to escape the retry loop.
            int callCount = 0;
            var innerException = new InvalidOperationException("authentication failed");

            Action act = () => RetryHelper.RetryFunc<bool>(() =>
            {
                callCount++;
                throw new OperationCanceledException("Interactive browser authentication failed or was cancelled.", innerException);
            }, NoOpLog);

            act.Should().Throw<OperationCanceledException>()
                .Which.InnerException.Should().BeSameAs(innerException);
            callCount.Should().Be(1, "wrapped auth failures must not be retried");
        }
    }
}
