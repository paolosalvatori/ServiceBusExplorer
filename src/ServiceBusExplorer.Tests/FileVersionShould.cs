using FluentAssertions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class FileVersionShould
    {
        private string expectedVersion;

        public FileVersionShould()
        {
#if DEBUG
            expectedVersion = Environment.GetEnvironmentVariable("FILE_VERSION") ?? "1.0.0.1";
#else
            expectedVersion = Environment.GetEnvironmentVariable("FILE_VERSION") ??  throw new InvalidOperationException("file version not set by environment");
#endif
        }


        [Fact]
        public void FileShouldHaveSameVersionAsProvidedByBuild()
        {
            var assemblyNames = Directory.GetFiles(".", "ServiceBus*.*")
                    .Where(x => x.EndsWith(".dll") || x.EndsWith(".exe"));

            var fileVersions = assemblyNames.Select(x => FileVersionInfo.GetVersionInfo(x).FileVersion);

            fileVersions.Should().HaveCount(8).And.AllBeEquivalentTo(expectedVersion);
        }
    }
}
