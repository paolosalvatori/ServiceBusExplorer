#region Using Directives

using System;
using System.Globalization;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Utilities.Helpers;
using NUnit.Framework;

#endregion

namespace ServiceBusExplorer.Tests.Helpers
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

        [Theory]
        [TestCase("00:00:00")]
        [TestCase("3:44:55")]
        [TestCase("03:44:55")]
        [TestCase("1:00:00:00.0000000")] // one day
        public void MapStringTypeToCLRType_ValueIsTimeSpanString_ReturnsEqualTimespanObject(string timespanStr)
        {
            var convertedTimespan = ConversionHelper.MapStringTypeToCLRType("TimeSpan", timespanStr);
            Assert.AreEqual(convertedTimespan, TimeSpan.Parse(timespanStr, CultureInfo.InvariantCulture));
        }

        [Test]
        public void MapStringTypeToCLRType_ValueIsTimeSpan_ReturnsEqualTimespanObject()
        {
            var convertedTimespan = ConversionHelper.MapStringTypeToCLRType("TimeSpan", TimeSpan.Zero);
            Assert.AreEqual(convertedTimespan, TimeSpan.Zero);
        }
    }
}
