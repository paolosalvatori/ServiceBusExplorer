using FluentAssertions;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class AssemblyVersionShould
    {
        [Fact]
        public void BeSameAsSetInProject()
        {
            var expectedVersion = "1.0.0.1";

            var assemblyNames = Directory.GetFiles(".", "ServiceBus*.*")
                    .Where(x=>x.EndsWith(".dll") || x.EndsWith(".exe"));

            var assemblyVersions = assemblyNames.Select(x=> 
                Assembly.LoadFrom(x).GetName().Version.ToString());

            assemblyVersions.Should().HaveCount(8).And.AllBeEquivalentTo(expectedVersion);
        }
    }
}
