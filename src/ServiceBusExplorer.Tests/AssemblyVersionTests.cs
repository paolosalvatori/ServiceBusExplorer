using FluentAssertions;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class AssemblyVersionTests
    {
        private string expectedVersion;
        private string expectedFileVersion;

        public AssemblyVersionTests()
        {
#if DEBUG
            expectedVersion = Environment.GetEnvironmentVariable("NUMBER_VERSION") ?? "0.0.0.0";
            expectedFileVersion = Environment.GetEnvironmentVariable("FILE_VERSION") ?? "0.0.0.0";
#else
            expectedVersion = Environment.GetEnvironmentVariable("NUMBER_VERSION") ??  throw new InvalidOperationException("Build number not set by environment");
            expectedFileVersion = Environment.GetEnvironmentVariable("FILE_VERSION") ??  throw new InvalidOperationException("file version not set by environment");
#endif
        }

        [Fact]
        public void AssemblyShouldHaveSameVersionAsProvidedByBuild()
        {
            typeof(Program).Assembly.GetName().Version.ToString().Should().Be(expectedVersion);
        }

        [Fact]
        public void FileShouldHaveSameVersionAsProvidedByBuild()
        {
            var fileVersion = FileVersionInfo.GetVersionInfo(typeof(Program).Assembly.Location);

            fileVersion.FileVersion.Should().Be(expectedFileVersion);
        }
    }
}
