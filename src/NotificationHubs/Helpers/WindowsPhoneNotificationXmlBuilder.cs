﻿#region Copyright
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
using System.Text;
using System.Xml;

#endregion

namespace ServiceBusExplorer.NotificationHubs.Helpers
{
    public static class WindowsPhoneNotificationXmlBuilder
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
        public static string CreateToast(string text1, string text2, string param)
        {
            return Create("<?xml version=\"1.0\" encoding=\"utf-8\"?><wp:Notification xmlns:wp=\"WPNotification\">" +
                          "<wp:Toast>" +
                          "<wp:Text1>{0}</wp:Text1>" +
                          "<wp:Text2>{1}</wp:Text2>" +
                          "<wp:Param>{2}</wp:Param>" +
                          "</wp:Toast>" +
                          "</wp:Notification>", 
                          new[] { text1, text2, param });
        }

        public static string CreateTile(string title = null,
                                        string count = null,
                                        string backTitle = null,
                                        string backContent = null,
                                        string wideBackContent = null,
                                        string smallBackgroundImage = null,
                                        string backgroundImage = null,
                                        string backBackgroundImage = null,
                                        string wideBackgroundImage = null,
                                        string wideBackBackgroundImage = null,
                                        bool titleClear = false,
                                        bool countClear = false,
                                        bool backTitleClear = false,
                                        bool backContentClear = false,
                                        bool wideBackContentClear = false,
                                        bool smallBackgroundImageClear = false,
                                        bool backgroundImageClear = false,
                                        bool backBackgroundImageClear = false,
                                        bool wideBackgroundImageClear = false,
                                        bool wideBackBackgroundImageClear = false,
                                        bool smallBackgroundImageIsRelative = true,
                                        bool backgroundImageIsRelative = true,
                                        bool backBackgroundImageIsRelative = true,
                                        bool wideBackgroundImageIsRelative = true,
                                        bool wideBackBackgroundImageIsRelative = true,
                                        bool smallBackgroundImageIsResource = true,
                                        bool backgroundImageIsResource = true,
                                        bool backBackgroundImageIsResource = true,
                                        bool wideBackgroundImageIsResource = true,
                                        bool wideBackBackgroundImageIsResource = true)
        {
            var builder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><wp:Notification xmlns:wp=\"WPNotification\" Version=\"2.0\"><wp:Tile Template=\"FlipTile\">");
            AppendTileElement(builder, titleClear, title, "Title");
            AppendTileElement(builder, backTitleClear, backTitle, "BackTitle");
            AppendTileElement(builder, backContentClear, backContent, "BackContent");
            AppendTileElement(builder, wideBackContentClear, wideBackContent, "WideBackContent");
            AppendTileElement(builder, countClear, count, "Count");
            AppendTileElement(builder, smallBackgroundImageClear, smallBackgroundImage, "SmallBackgroundImage", true, smallBackgroundImageIsRelative, smallBackgroundImageIsResource);
            AppendTileElement(builder, backgroundImageClear, backgroundImage, "BackgroundImage", true, backgroundImageIsRelative, backgroundImageIsResource);
            AppendTileElement(builder, backBackgroundImageClear, backBackgroundImage, "BackBackgroundImage", true, backBackgroundImageIsRelative, backBackgroundImageIsResource);
            AppendTileElement(builder, wideBackgroundImageClear, wideBackgroundImage, "WideBackgroundImage", true, wideBackgroundImageIsRelative, wideBackgroundImageIsResource);
            AppendTileElement(builder, wideBackBackgroundImageClear, wideBackBackgroundImage, "WideBackBackgroundImage", true, wideBackBackgroundImageIsRelative, wideBackBackgroundImageIsResource);
            builder.Append("</wp:Tile></wp:Notification>");

            // Form the XML template for the tile
            return builder.ToString();
        }


        public static string CreateRaw(string payload)
        {
            return payload;
        }
        #endregion

        #region Private Static Methods
        private static void AppendTileElement(StringBuilder builder, bool clear, string value, string element, bool isPicture = false, bool isRelative = true, bool isResource = true)
        {
            builder.Append(clear
                               ? string.Format("<wp:{0} Action=\"Clear\"/>", element)
                               : string.IsNullOrWhiteSpace(value)
                                     ? string.Empty
                                     : isPicture
                                         ? string.Format("<wp:{0} IsRelative=\"{1}\" IsResource=\"{2}\">{3}</wp:{0}>", element, isRelative, isResource, value)
                                         : string.Format("<wp:{0}>{1}</wp:{0}>", element, value));
        }
        #endregion
    }
}
