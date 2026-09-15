using System.Collections.Generic;
using FluentAssertions;
using ServiceBusExplorer.Common.Entities;
using ServiceBusExplorer.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class MainSettingsEntraTenantIdsTests
    {
        [Fact]
        public void IsEquivalentTo_SameTenantIdsWithDifferentInstances_ReturnsTrue()
        {
            var first = new MainSettings();
            first.SetDefault();
            first.EntraTenantIds = new List<EntraTenantIdItem>
            {
                new EntraTenantIdItem { Value = "Tenant-One" },
                new EntraTenantIdItem { Value = "Tenant-Two" }
            };

            var second = new MainSettings();
            second.SetDefault();
            second.EntraTenantIds = new List<EntraTenantIdItem>
            {
                new EntraTenantIdItem { Value = "tenant-one" },
                new EntraTenantIdItem { Value = "tenant-two" }
            };

            first.Equals(second).Should().BeTrue();
        }

        [Fact]
        public void IsEquivalentTo_DifferentTenantIds_ReturnsFalse()
        {
            var first = new MainSettings();
            first.SetDefault();
            first.EntraTenantIds = new List<EntraTenantIdItem>
            {
                new EntraTenantIdItem { Value = "tenant-a" }
            };

            var second = new MainSettings();
            second.SetDefault();
            second.EntraTenantIds = new List<EntraTenantIdItem>
            {
                new EntraTenantIdItem { Value = "tenant-b" }
            };

            first.Equals(second).Should().BeFalse();
        }
    }
}
