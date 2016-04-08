using System;
using Microsoft.WindowsAzure.CAT.ServiceBusExplorer;
using NUnit.Framework;

namespace ServiceBusExplorer.Tests
{
    [TestFixture]
    public class ConversionHelperTests
    {
        [Test]
        public void MapStringTypeToCLRType_ValueIsGuidObject_ReturnsEqualGuidObject()
        {
            var guid = new Guid("2E9DB8C4-8803-4BD7-B860-8932CF13835E");
            var convertedGuid = ConversionHelper.MapStringTypeToCLRType("Guid", guid);
            Assert.AreEqual(guid, convertedGuid);
        }

        [Test]
        public void MapStringTypeToCLRType_ValueIsGuidString_ReturnsEqualGuidObject()
        {
            var guidStr = "2E9DB8C4-8803-4BD7-B860-8932CF13835E";
            var convertedGuid = ConversionHelper.MapStringTypeToCLRType("Guid", guidStr);
            Assert.AreEqual(guidStr.ToLower(), convertedGuid.ToString());
        }
    }
}
