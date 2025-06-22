// // Auto-added comment

// #region Using Directives

// using System;
// using System.Globalization;
// using ServiceBusExplorer.Utilities.Helpers;
// using FluentAssertions;
// using Xunit;

// #endregion

// namespace ServiceBusExplorer.Tests.Helpers
// {
//     public class ConversionHelperTests
//     {
//         [Fact]
//         public void MapStringTypeToCLRType_ValueIsGuidObject_ReturnsEqualGuidObject()
//         {
//             var guid = new Guid("2E9DB8C4-8803-4BD7-B860-8932CF13835E");
//             var convertedGuid = ConversionHelper.MapStringTypeToCLRType("Guid", guid);
//             convertedGuid.Should().BeEquivalentTo(guid);
//         }

//         [Fact]
//         public void MapStringTypeToCLRType_ValueIsGuidString_ReturnsEqualGuidObject()
//         {
//             var guidStr = "2E9DB8C4-8803-4BD7-B860-8932CF13835E";
//             var convertedGuid = ConversionHelper.MapStringTypeToCLRType("Guid", guidStr);
//             convertedGuid.ToString().Should().BeEquivalentTo(guidStr);
//         }

//         [Theory]
//         [InlineData("00:00:00")]
//         [InlineData("3:44:55")]
//         [InlineData("03:44:55")]
//         [InlineData("1:00:00:00.0000000")] // one day
//         public void MapStringTypeToCLRType_ValueIsTimeSpanString_ReturnsEqualTimespanObject(string timespanStr)
//         {
//             var convertedTimespan = ConversionHelper.MapStringTypeToCLRType("TimeSpan", timespanStr);
//             convertedTimespan.Should().BeEquivalentTo(TimeSpan.Parse(timespanStr, CultureInfo.InvariantCulture));
//         }

//         [Fact]
//         public void MapStringTypeToCLRType_ValueIsTimeSpan_ReturnsEqualTimespanObject()
//         {
//             var convertedTimespan = ConversionHelper.MapStringTypeToCLRType("TimeSpan", TimeSpan.Zero);
//             convertedTimespan.Should().Be(TimeSpan.Zero);
//         }
//     }
// }
