using FluentAssertions;
using System;
using System.Diagnostics;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class FileVersionShould
    {
        private string expectedVersion;

        public FileVersionShould()
        {
#if DEBUG
            expectedVersion = Environment.GetEnvironmentVariable("FILE_VERSION") ?? "0.0.0.0";
#else
            expectedVersion = Environment.GetEnvironmentVariable("FILE_VERSION") ??  throw new InvalidOperationException("file version not set by environment");
#endif
        }


        [Fact]
        public void FileShouldHaveSameVersionAsProvidedByBuild()
        {
            var fileVersion = FileVersionInfo.GetVersionInfo(typeof(Program).Assembly.Location);

            fileVersion.FileVersion.Should().Be(expectedVersion);
        }
    }
}
