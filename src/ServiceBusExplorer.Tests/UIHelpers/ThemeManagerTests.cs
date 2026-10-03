using System;
using FluentAssertions;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests.UIHelpers
{
    public class ThemeManagerTests
    {
        [Theory]
        [InlineData(null, "Light")]
        [InlineData("", "Light")]
        [InlineData(" ", "Light")]
        [InlineData("Blue", "Light")]
        [InlineData("1", "Light")]
        [InlineData("dark", "Dark")]
        [InlineData("Dark", "Dark")]
        [InlineData("LIGHT", "Light")]
        public void ParseConfiguredMode_ReturnsExpectedMode(string configuredMode, string expectedMode)
        {
            ThemeManager.ParseConfiguredMode(configuredMode).Should().Be((ThemeMode)Enum.Parse(typeof(ThemeMode), expectedMode, true));
        }
    }
}
