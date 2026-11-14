using System;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusManagementFilterTests
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("Startswith(Path, 'ORD') Eq true", true)]
        [InlineData("Startswith(Name, 'ORD') Eq true And MessageCount Ge 42", true)]
        [InlineData("Path Eq 'other' Or MessageCount Eq 42", true)]
        [InlineData("Path Ne 'orders' And MessageCount Eq 42", false)]
        [InlineData("MessageCount Gt 42", false)]
        [InlineData("MessageCount Le 42", true)]
        [InlineData("MessageCount Lt 42", false)]
        [InlineData("CreatedAt Ge '2024-01-01T00:00:00Z'", true)]
        [InlineData("UpdatedAt Eq '2024-01-02T00:00:00Z'", true)]
        [InlineData("AccessedAt Lt '2025-01-01T00:00:00Z'", true)]
        public void UiFilterExpressions_AreEvaluated(string filter, bool expected)
        {
            var time = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc);
            ServiceBusManagementFilter.Parse(filter)("orders", 42, time, time, time).Should().Be(expected);
        }

        [Fact]
        public void QuotedLogicalOperatorsAndEscapedQuotes_AreNotSplit()
        {
            ServiceBusManagementFilter.Parse("Path Eq 'orders or Bob''s and Alice''s'")(
                "orders or Bob's and Alice's", 0, default, default, default).Should().BeTrue();
        }

        [Theory]
        [InlineData("Unsupported Eq true")]
        [InlineData("Path Eq 'orders' Or unsupported")]
        [InlineData("MessageCount Gt 0 garbage")]
        [InlineData("Path Eq 'unterminated")]
        public void UnsupportedClauses_AreRejectedBeforeEnumeration(string filter)
        {
            ((Action)(() => ServiceBusManagementFilter.Parse(filter))).Should().Throw<NotSupportedException>();
        }
    }
}
