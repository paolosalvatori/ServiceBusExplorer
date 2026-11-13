using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class VersionProviderTests
    {
        static readonly MethodInfo GetKnownReleaseVersionMethod =
            typeof(VersionProvider).GetMethod("GetKnownReleaseVersion", BindingFlags.NonPublic | BindingFlags.Static)
            ?? throw new InvalidOperationException("Unable to locate GetKnownReleaseVersion.");

        static readonly MethodInfo IsLatestVersionMethod =
            typeof(VersionProvider).GetMethod("IsLatestVersion", BindingFlags.NonPublic | BindingFlags.Static,
                null, new[] { typeof(Version), typeof(ReleaseInfo) }, null)
            ?? throw new InvalidOperationException("Unable to locate IsLatestVersion.");

        [Fact]
        public void GetKnownReleaseVersion_ReadsUpstreamMetadataInsteadOfTheForkVersion()
        {
            var metadata = new[]
            {
                new AssemblyMetadataAttribute("Unrelated", "99.0.0"),
                new AssemblyMetadataAttribute("UpstreamReleaseVersion", "6.3.1")
            };
            var messages = new List<string>();

            ReadVersion(metadata, (message, async) => messages.Add(message))
                .Should().Be(new Version(6, 3, 1));
            messages.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("not-a-version")]
        [InlineData("6.3")]
        [InlineData("6.3.1-preview")]
        [InlineData("6.3.1.0")]
        public void GetKnownReleaseVersion_InvalidMetadataLogsAndReturnsUnknown(string value)
        {
            var messages = new List<string>();
            var metadata = new[] { new AssemblyMetadataAttribute("UpstreamReleaseVersion", value) };

            ReadVersion(metadata, (message, async) => messages.Add(message)).Should().BeNull();
            messages.Should().ContainSingle().Which.Should().Contain("unavailable or invalid");
        }

        [Fact]
        public void GetKnownReleaseVersion_MissingMetadataLogsAndReturnsUnknown()
        {
            var messages = new List<string>();

            ReadVersion(Array.Empty<AssemblyMetadataAttribute>(), (message, async) => messages.Add(message))
                .Should().BeNull();
            messages.Should().ContainSingle().Which.Should().Contain("unavailable or invalid");
        }

        [Fact]
        public void GetKnownReleaseVersion_UsesMetadataEmbeddedInTheCommonAssembly()
        {
            var metadata = typeof(VersionProvider).Assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
                .SingleOrDefault(attribute => attribute.Key == "UpstreamReleaseVersion");
            var messages = new List<string>();

            var version = VersionProvider.GetKnownReleaseVersion((message, async) => messages.Add(message));

            if (metadata == null)
            {
                version.Should().BeNull();
                messages.Should().ContainSingle();
            }
            else
            {
                version.Should().Be(new Version(metadata.Value));
                messages.Should().BeEmpty();
            }
        }

        [Theory]
        [InlineData("6.3.1", "6.3.1", true)]
        [InlineData("6.3.1", "6.3.0", true)]
        [InlineData("6.3.1", "6.3.2", false)]
        [InlineData("6.3.1", "7.0.0", false)]
        [InlineData("6.3.1", "6.3.1.0", true)]
        [InlineData("6.3.1", "6.4", false)]
        public void IsLatestVersion_ComparesUpstreamBaselineWithLatestUpstreamRelease(string baseline, string latest, bool expected)
        {
            var releaseInfo = new ReleaseInfo(new Uri("https://example.com/release"), new Version(latest), "notes", null);

            IsLatest(new Version(baseline), releaseInfo).Should().Be(expected);
            releaseInfo.Version.Should().Be(new Version(latest));
        }

        [Fact]
        public void IsLatestVersion_UnknownBaselineSuppressesNotice()
        {
            IsLatest(null, new ReleaseInfo(null, new Version(99, 0, 0), string.Empty, null)).Should().BeTrue();
        }

        [Fact]
        public void IsLatestVersion_UnavailableLatestReleaseSuppressesNotice()
        {
            IsLatest(new Version(6, 3, 1), ReleaseInfo.Null).Should().BeTrue();
            IsLatest(new Version(6, 3, 1), null).Should().BeTrue();
        }

        static Version ReadVersion(IEnumerable<AssemblyMetadataAttribute> metadata, WriteToLogDelegate writeToLog)
        {
            return (Version)GetKnownReleaseVersionMethod.Invoke(null, new object[] { metadata, writeToLog });
        }

        static bool IsLatest(Version knownReleaseVersion, ReleaseInfo latestReleaseInfo)
        {
            return (bool)IsLatestVersionMethod.Invoke(null, new object[] { knownReleaseVersion, latestReleaseInfo });
        }
    }
}
