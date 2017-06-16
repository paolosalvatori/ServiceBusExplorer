#region Using Directives

using System;
using Microsoft.Azure.ServiceBusExplorer.Helpers;
using NUnit.Framework;

#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Tests.Helpers
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
