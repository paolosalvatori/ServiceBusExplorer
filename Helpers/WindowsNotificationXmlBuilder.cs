#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives
using System.Globalization;
using System.Xml;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class WindowsNotificationXmlBuilder
    {
        #region Private Static Methods
        private static string Create(string format, params string[] args)
        {
            var document = new XmlDocument();
            if (args != null)
            {
                // ReSharper disable CoVariantArrayConversion
                document.LoadXml(string.Format(CultureInfo.InvariantCulture, format, args));
                // ReSharper restore CoVariantArrayConversion
            }
            return document.InnerXml;
        } 
        #endregion

        #region Public Static Methods
        public static string CreateBadgeXml(string badge)
        {
            return Create("<badge value=\"{0}\"></badge>", new[] { badge });
        }

        public static string CreateTileSquareBlockXml(string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileSquareBlock\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text></binding></visual></tile>", new[] { text1, text2 });
        }

        public static string CreateTileSquareImageXml(string imageSrc)
        {
            return Create("<tile><visual><binding template=\"TileSquareImage\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/></binding></visual></tile>", new[] { imageSrc, imageSrc });
        }

        public static string CreateTileSquareImageXml(string imageSrc, string imageAltText)
        {
            return Create("<tile><visual><binding template=\"TileSquareImage\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/></binding></visual></tile>", new[] { imageSrc, imageAltText });
        }

        public static string CreateTileSquarePeekImageAndText01Xml(string imageSrc, string text1, string text2, string text3, string text4)
        {
            return Create("<tile><visual><binding template=\"TileSquarePeekImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2, text3, text4 });
        }

        public static string CreateTileSquarePeekImageAndText01Xml(string imageSrc, string imageAltText, string text1, string text2, string text3, string text4)
        {
            return Create("<tile><visual><binding template=\"TileSquarePeekImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2, text3, text4 });
        }

        public static string CreateTileSquarePeekImageAndText02Xml(string imageSrc, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileSquarePeekImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2 });
        }

        public static string CreateTileSquarePeekImageAndText02Xml(string imageSrc, string imageAltText, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileSquarePeekImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2 });
        }

        public static string CreateTileSquarePeekImageAndText03Xml(string imageSrc, string text1, string text2, string text3, string text4)
        {
            return Create("<tile><visual><binding template=\"TileSquarePeekImageAndText03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2, text3, text4 });
        }

        public static string CreateTileSquarePeekImageAndText03Xml(string imageSrc, string imageAltText, string text1, string text2, string text3, string text4)
        {
            return Create("<tile><visual><binding template=\"TileSquarePeekImageAndText03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2, text3, text4 });
        }

        public static string CreateTileSquarePeekImageAndText04Xml(string imageSrc, string text1)
        {
            return Create("<tile><visual><binding template=\"TileSquarePeekImageAndText04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1 });
        }

        public static string CreateTileSquarePeekImageAndText04Xml(string imageSrc, string imageAltText, string text1)
        {
            return Create("<tile><visual><binding template=\"TileSquarePeekImageAndText04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1 });
        }

        public static string CreateTileSquareText01Xml(string text1, string text2, string text3, string text4)
        {
            return Create("<tile><visual><binding template=\"TileSquareText01\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text></binding></visual></tile>", new[] { text1, text2, text3, text4 });
        }

        public static string CreateTileSquareText02Xml(string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileSquareText02\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text></binding></visual></tile>", new[] { text1, text2 });
        }

        public static string CreateTileSquareText03Xml(string text1, string text2, string text3, string text4)
        {
            return Create("<tile><visual><binding template=\"TileSquareText03\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text></binding></visual></tile>", new[] { text1, text2, text3, text4 });
        }

        public static string CreateTileSquareText04Xml(string text1)
        {
            return Create("<tile><visual><binding template=\"TileSquareText04\"><text id=\"1\">{0}</text></binding></visual></tile>", new[] { text1 });
        }

        public static string CreateTileWideBlockAndText01Xml(string text1, string text2, string text3, string text4, string text5, string text6)
        {
            return Create("<tile><visual><binding template=\"TileWideBlockAndText01\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text><text id=\"6\">{5}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5, text6 });
        }

        public static string CreateTileWideBlockAndText02Xml(string text1, string text2, string text3)
        {
            return Create("<tile><visual><binding template=\"TileWideBlockAndText02\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text></binding></visual></tile>", new[] { text1, text2, text3 });
        }

        public static string CreateTileWideImageAndText01Xml(string imageSrc, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWideImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1 });
        }

        public static string CreateTileWideImageAndText01Xml(string imageSrc, string imageAltText, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWideImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1 });
        }

        public static string CreateTileWideImageAndText02Xml(string imageSrc, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWideImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2 });
        }

        public static string CreateTileWideImageAndText02Xml(string imageSrc, string imageAltText, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWideImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2 });
        }

        public static string CreateTileWideImageCollectionXml(string imageSrc1, string imageSrc2, string imageSrc3, string imageSrc4, string imageSrc5)
        {
            return Create("<tile><visual><binding template=\"TileWideImageCollection\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, imageSrc3, imageSrc3, imageSrc4, imageSrc4, imageSrc5, imageSrc5 });
        }

        public static string CreateTileWideImageCollectionXml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string imageSrc3, string imageAltText3, string imageSrc4, string imageAltText4, string imageSrc5, string imageAltText5)
        {
            return Create("<tile><visual><binding template=\"TileWideImageCollection\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, imageSrc3, imageAltText3, imageSrc4, imageAltText4, imageSrc5, imageAltText5 });
        }

        public static string CreateTileWideImageXml(string imageSrc)
        {
            return Create("<tile><visual><binding template=\"TileWideImage\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/></binding></visual></tile>", new[] { imageSrc, imageSrc });
        }

        public static string CreateTileWideImageXml(string imageSrc, string imageAltText)
        {
            return Create("<tile><visual><binding template=\"TileWideImage\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/></binding></visual></tile>", new[] { imageSrc, imageAltText });
        }

        public static string CreateTileWidePeekImage01Xml(string imageSrc, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2 });
        }

        public static string CreateTileWidePeekImage01Xml(string imageSrc, string imageAltText, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2 });
        }

        public static string CreateTileWidePeekImage02Xml(string imageSrc, string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text><text id=\"5\">{6}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWidePeekImage02Xml(string imageSrc, string imageAltText, string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text><text id=\"5\">{6}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWidePeekImage03Xml(string imageSrc, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1 });
        }

        public static string CreateTileWidePeekImage03Xml(string imageSrc, string imageAltText, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1 });
        }

        public static string CreateTileWidePeekImage04Xml(string imageSrc, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1 });
        }

        public static string CreateTileWidePeekImage04Xml(string imageSrc, string imageAltText, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1 });
        }

        public static string CreateTileWidePeekImage05Xml(string imageSrc1, string imageSrc2, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage05\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><text id=\"1\">{4}</text><text id=\"2\">{5}</text></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, text1, text2 });
        }

        public static string CreateTileWidePeekImage05Xml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage05\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><text id=\"1\">{4}</text><text id=\"2\">{5}</text></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, text1, text2 });
        }

        public static string CreateTileWidePeekImage06Xml(string imageSrc1, string imageSrc2, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage06\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><text id=\"1\">{4}</text></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, text1 });
        }

        public static string CreateTileWidePeekImage06Xml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImage06\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><text id=\"1\">{4}</text></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, text1 });
        }

        public static string CreateTileWidePeekImageAndText01Xml(string imageSrc, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1 });
        }

        public static string CreateTileWidePeekImageAndText01Xml(string imageSrc, string imageAltText, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1 });
        }

        public static string CreateTileWidePeekImageAndText02Xml(string imageSrc, string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text><text id=\"5\">{6}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWidePeekImageAndText02Xml(string imageSrc, string imageAltText, string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text><text id=\"5\">{6}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWidePeekImageCollection01Xml(string imageSrc1, string imageSrc2, string imageSrc3, string imageSrc4, string imageSrc5, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><text id=\"1\">{10}</text><text id=\"2\">{11}</text></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, imageSrc3, imageSrc3, imageSrc4, imageSrc4, imageSrc5, imageSrc5, text1, text2 });
        }

        public static string CreateTileWidePeekImageCollection01Xml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string imageSrc3, string imageAltText3, string imageSrc4, string imageAltText4, string imageSrc5, string imageAltText5, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><text id=\"1\">{10}</text><text id=\"2\">{11}</text></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, imageSrc3, imageAltText3, imageSrc4, imageAltText4, imageSrc5, imageAltText5, text1, text2 });
        }

        public static string CreateTileWidePeekImageCollection02Xml(string imageSrc1, string imageSrc2, string imageSrc3, string imageSrc4, string imageSrc5, string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><text id=\"1\">{10}</text><text id=\"2\">{11}</text><text id=\"3\">{12}</text><text id=\"4\">{13}</text><text id=\"5\">{14}</text></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, imageSrc3, imageSrc3, imageSrc4, imageSrc4, imageSrc5, imageSrc5, text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWidePeekImageCollection02Xml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string imageSrc3, string imageAltText3, string imageSrc4, string imageAltText4, string imageSrc5, string imageAltText5, string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><text id=\"1\">{10}</text><text id=\"2\">{11}</text><text id=\"3\">{12}</text><text id=\"4\">{13}</text><text id=\"5\">{14}</text></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, imageSrc3, imageAltText3, imageSrc4, imageAltText4, imageSrc5, imageAltText5, text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWidePeekImageCollection03Xml(string imageSrc1, string imageSrc2, string imageSrc3, string imageSrc4, string imageSrc5, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><text id=\"1\">{10}</text></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, imageSrc3, imageSrc3, imageSrc4, imageSrc4, imageSrc5, imageSrc5, text1 });
        }

        public static string CreateTileWidePeekImageCollection03Xml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string imageSrc3, string imageAltText3, string imageSrc4, string imageAltText4, string imageSrc5, string imageAltText5, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><text id=\"1\">{10}</text></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, imageSrc3, imageAltText3, imageSrc4, imageAltText4, imageSrc5, imageAltText5, text1 });
        }

        public static string CreateTileWidePeekImageCollection04Xml(string imageSrc1, string imageSrc2, string imageSrc3, string imageSrc4, string imageSrc5, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><text id=\"1\">{10}</text></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, imageSrc3, imageSrc3, imageSrc4, imageSrc4, imageSrc5, imageSrc5, text1 });
        }

        public static string CreateTileWidePeekImageCollection04Xml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string imageSrc3, string imageAltText3, string imageSrc4, string imageAltText4, string imageSrc5, string imageAltText5, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><text id=\"1\">{10}</text></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, imageSrc3, imageAltText3, imageSrc4, imageAltText4, imageSrc5, imageAltText5, text1 });
        }

        public static string CreateTileWidePeekImageCollection05Xml(string imageSrc1, string imageSrc2, string imageSrc3, string imageSrc4, string imageSrc5, string imageSrc6, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection05\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><image id=\"6\" src=\"{10}\" alt=\"{11}\"/><text id=\"1\">{12}</text><text id=\"2\">{13}</text></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, imageSrc3, imageSrc3, imageSrc4, imageSrc4, imageSrc5, imageSrc5, imageSrc6, imageSrc6, text1, text2 });
        }

        public static string CreateTileWidePeekImageCollection05Xml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string imageSrc3, string imageAltText3, string imageSrc4, string imageAltText4, string imageSrc5, string imageAltText5, string imageSrc6, string imageAltText6, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection05\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><image id=\"6\" src=\"{10}\" alt=\"{11}\"/><text id=\"1\">{12}</text><text id=\"2\">{13}</text></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, imageSrc3, imageAltText3, imageSrc4, imageAltText4, imageSrc5, imageAltText5, imageSrc6, imageAltText6, text1, text2 });
        }

        public static string CreateTileWidePeekImageCollection06Xml(string imageSrc1, string imageSrc2, string imageSrc3, string imageSrc4, string imageSrc5, string imageSrc6, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection06\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><image id=\"6\" src=\"{10}\" alt=\"{11}\"/><text id=\"1\">{12}</text></binding></visual></tile>", new[] { imageSrc1, imageSrc1, imageSrc2, imageSrc2, imageSrc3, imageSrc3, imageSrc4, imageSrc4, imageSrc5, imageSrc5, imageSrc6, imageSrc6, text1 });
        }

        public static string CreateTileWidePeekImageCollection06Xml(string imageSrc1, string imageAltText1, string imageSrc2, string imageAltText2, string imageSrc3, string imageAltText3, string imageSrc4, string imageAltText4, string imageSrc5, string imageAltText5, string imageSrc6, string imageAltText6, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWidePeekImageCollection06\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><image id=\"2\" src=\"{2}\" alt=\"{3}\"/><image id=\"3\" src=\"{4}\" alt=\"{5}\"/><image id=\"4\" src=\"{6}\" alt=\"{7}\"/><image id=\"5\" src=\"{8}\" alt=\"{9}\"/><image id=\"6\" src=\"{10}\" alt=\"{11}\"/><text id=\"1\">{12}</text></binding></visual></tile>", new[] { imageSrc1, imageAltText1, imageSrc2, imageAltText2, imageSrc3, imageAltText3, imageSrc4, imageAltText4, imageSrc5, imageAltText5, imageSrc6, imageAltText6, text1 });
        }

        public static string CreateTileWideSmallImageAndText01Xml(string imageSrc, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1 });
        }

        public static string CreateTileWideSmallImageAndText01Xml(string imageSrc, string imageAltText, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1 });
        }

        public static string CreateTileWideSmallImageAndText02Xml(string imageSrc, string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text><text id=\"5\">{6}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWideSmallImageAndText02Xml(string imageSrc, string imageAltText, string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text><text id=\"4\">{5}</text><text id=\"5\">{6}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWideSmallImageAndText03Xml(string imageSrc, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1 });
        }

        public static string CreateTileWideSmallImageAndText03Xml(string imageSrc, string imageAltText, string text1)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1 });
        }

        public static string CreateTileWideSmallImageAndText04Xml(string imageSrc, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2 });
        }

        public static string CreateTileWideSmallImageAndText04Xml(string imageSrc, string imageAltText, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2 });
        }

        public static string CreateTileWideSmallImageAndText05Xml(string imageSrc, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText05\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageSrc, text1, text2 });
        }

        public static string CreateTileWideSmallImageAndText05Xml(string imageSrc, string imageAltText, string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWideSmallImageAndText05\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></tile>", new[] { imageSrc, imageAltText, text1, text2 });
        }

        public static string CreateTileWideText01Xml(string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWideText01\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWideText02Xml(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9)
        {
            return Create("<tile><visual><binding template=\"TileWideText02\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text><text id=\"6\">{5}</text><text id=\"7\">{6}</text><text id=\"8\">{7}</text><text id=\"9\">{8}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5, text6, text7, text8, text9 });
        }

        public static string CreateTileWideText03Xml(string text1)
        {
            return Create("<tile><visual><binding template=\"TileWideText03\"><text id=\"1\">{0}</text></binding></visual></tile>", new[] { text1 });
        }

        public static string CreateTileWideText04Xml(string text1)
        {
            return Create("<tile><visual><binding template=\"TileWideText04\"><text id=\"1\">{0}</text></binding></visual></tile>", new[] { text1 });
        }

        public static string CreateTileWideText05Xml(string text1, string text2, string text3, string text4, string text5)
        {
            return Create("<tile><visual><binding template=\"TileWideText05\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5 });
        }

        public static string CreateTileWideText06Xml(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9, string text10)
        {
            return Create("<tile><visual><binding template=\"TileWideText06\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text><text id=\"6\">{5}</text><text id=\"7\">{6}</text><text id=\"8\">{7}</text><text id=\"9\">{8}</text><text id=\"10\">{9}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10 });
        }

        public static string CreateTileWideText07Xml(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9)
        {
            return Create("<tile><visual><binding template=\"TileWideText07\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text><text id=\"6\">{5}</text><text id=\"7\">{6}</text><text id=\"8\">{7}</text><text id=\"9\">{8}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5, text6, text7, text8, text9 });
        }

        public static string CreateTileWideText08Xml(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9, string text10)
        {
            return Create("<tile><visual><binding template=\"TileWideText08\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text><text id=\"6\">{5}</text><text id=\"7\">{6}</text><text id=\"8\">{7}</text><text id=\"9\">{8}</text><text id=\"10\">{9}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10 });
        }

        public static string CreateTileWideText09Xml(string text1, string text2)
        {
            return Create("<tile><visual><binding template=\"TileWideText09\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text></binding></visual></tile>", new[] { text1, text2 });
        }

        public static string CreateTileWideText10Xml(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9)
        {
            return Create("<tile><visual><binding template=\"TileWideText10\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text><text id=\"6\">{5}</text><text id=\"7\">{6}</text><text id=\"8\">{7}</text><text id=\"9\">{8}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5, text6, text7, text8, text9 });
        }

        public static string CreateTileWideText11Xml(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8, string text9, string text10)
        {
            return Create("<tile><visual><binding template=\"TileWideText11\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text><text id=\"4\">{3}</text><text id=\"5\">{4}</text><text id=\"6\">{5}</text><text id=\"7\">{6}</text><text id=\"8\">{7}</text><text id=\"9\">{8}</text><text id=\"10\">{9}</text></binding></visual></tile>", new[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10 });
        }

        public static string CreateToastImageAndText01Xml(string imageSrc, string text1)
        {
            return Create("<toast><visual><binding template=\"ToastImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></toast>", new[] { imageSrc, imageSrc, text1 });
        }

        public static string CreateToastImageAndText01Xml(string imageSrc, string imageAltText, string text1)
        {
            return Create("<toast><visual><binding template=\"ToastImageAndText01\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text></binding></visual></toast>", new[] { imageSrc, imageAltText, text1 });
        }

        public static string CreateToastImageAndText02Xml(string imageSrc, string text1, string text2)
        {
            return Create("<toast><visual><binding template=\"ToastImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></toast>", new[] { imageSrc, imageSrc, text1, text2 });
        }

        public static string CreateToastImageAndText02Xml(string imageSrc, string imageAltText, string text1, string text2)
        {
            return Create("<toast><visual><binding template=\"ToastImageAndText02\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></toast>", new[] { imageSrc, imageAltText, text1, text2 });
        }

        public static string CreateToastImageAndText03Xml(string imageSrc, string text1, string text2)
        {
            return Create("<toast><visual><binding template=\"ToastImageAndText03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></toast>", new[] { imageSrc, imageSrc, text1, text2 });
        }

        public static string CreateToastImageAndText03Xml(string imageSrc, string imageAltText, string text1, string text2)
        {
            return Create("<toast><visual><binding template=\"ToastImageAndText03\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text></binding></visual></toast>", new[] { imageSrc, imageAltText, text1, text2 });
        }

        public static string CreateToastImageAndText04Xml(string imageSrc, string text1, string text2, string text3)
        {
            return Create("<toast><visual><binding template=\"ToastImageAndText04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text></binding></visual></toast>", new[] { imageSrc, imageSrc, text1, text2, text3 });
        }

        public static string CreateToastImageAndText04Xml(string imageSrc, string imageAltText, string text1, string text2, string text3)
        {
            return Create("<toast><visual><binding template=\"ToastImageAndText04\"><image id=\"1\" src=\"{0}\" alt=\"{1}\"/><text id=\"1\">{2}</text><text id=\"2\">{3}</text><text id=\"3\">{4}</text></binding></visual></toast>", new[] { imageSrc, imageAltText, text1, text2, text3 });
        }

        public static string CreateToastText01Xml(string text1)
        {
            return Create("<toast><visual><binding template=\"ToastText01\"><text id=\"1\">{0}</text></binding></visual></toast>", new[] { text1 });
        }

        public static string CreateToastText02Xml(string text1, string text2)
        {
            return Create("<toast><visual><binding template=\"ToastText02\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text></binding></visual></toast>", new[] { text1, text2 });
        }

        public static string CreateToastText03Xml(string text1, string text2)
        {
            return Create("<toast><visual><binding template=\"ToastText03\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text></binding></visual></toast>", new[] { text1, text2 });
        }

        public static string CreateToastText04Xml(string text1, string text2, string text3)
        {
            return Create("<toast><visual><binding template=\"ToastText04\"><text id=\"1\">{0}</text><text id=\"2\">{1}</text><text id=\"3\">{2}</text></binding></visual></toast>", new[] { text1, text2, text3 });
        } 
        #endregion
    }
}
