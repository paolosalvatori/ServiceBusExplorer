using FluentAssertions;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Xunit;

namespace ServiceBusExplorer.Tests
{
    public class AssemblyVersionShould
    {
        [Fact]
        public void BeSameAsSetInProject()
        {
            var assemblyNames = Directory.GetFiles(".", "ServiceBus*.*")
                    .Where(x=>x.EndsWith(".dll") || x.EndsWith(".exe"));

            var projectFiles = Directory.GetFiles("../../../../", "*.csproj", SearchOption.AllDirectories);
            
            var xmlDocument = new XmlDocument();

            var expectedVersions = projectFiles.Select(x =>
            {
                xmlDocument.Load(x);
                var nodes = xmlDocument.SelectSingleNode("//AssemblyVersion");
                return nodes.InnerText;
            }).ToArray() ;

            var assemblyVersions = assemblyNames.Select(x=> 
                Assembly.LoadFrom(x).GetName().Version.ToString());

            assemblyVersions.Should().HaveCount(9).And.BeEquivalentTo(expectedVersions);
        }
    }
}
