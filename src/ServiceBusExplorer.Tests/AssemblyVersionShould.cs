using FluentAssertions;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class AssemblyVersionShould
    {
        private string expectedVersion;

        public AssemblyVersionShould()
        {
#if DEBUG
            expectedVersion = Environment.GetEnvironmentVariable("NUMBER_VERSION") ?? "0.0.0";
#else
            expectedVersion = Environment.GetEnvironmentVariable("NUMBER_VERSION") ??  throw new InvalidOperationException("Build number not set by environment");
#endif
            var version = Version.Parse(expectedVersion);
            if (version.Revision == -1) expectedVersion += ".0";
        }

        [Fact]
        public void BeSameAsProvidedByBuild()
        { 
            typeof(Program).Assembly.GetName().Version.ToString().Should().Be(expectedVersion);
        }
    }
}
