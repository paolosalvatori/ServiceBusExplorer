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

using System;
using FastColoredTextBoxNS;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Utilities.Helpers;


#endregion

namespace ServiceBusExplorer.UIHelpers
{
    public class LanguageDetector
    {
        #region Public Static Methods
        public static void SetFormattedMessage(ServiceBusHelper serviceBusHelper, BrokeredMessage message, FastColoredTextBox textBox)
        {
            if (serviceBusHelper == null)
            {
                throw new ArgumentNullException(nameof(serviceBusHelper), $"{nameof(serviceBusHelper)} parameter cannot be null");
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), $"{nameof(message)} parameter cannot be null");
            }

            if (textBox == null)
            {
                throw new ArgumentNullException(nameof(textBox), $"{nameof(textBox)} parameter cannot be null");
            }

            InternalSetFormattedMessage(serviceBusHelper.GetMessageText(message, 
                MainForm.SingletonMainForm.UseAscii, out _), textBox);
        }

        public static void SetFormattedMessage(ServiceBusHelper serviceBusHelper, EventData message, FastColoredTextBox textBox)
        {
            if (serviceBusHelper == null)
            {
                throw new ArgumentNullException(nameof(serviceBusHelper), $"{nameof(serviceBusHelper)} parameter cannot be null");
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), $"{nameof(message)} parameter cannot be null");
            }

            if (textBox == null)
            {
                throw new ArgumentNullException(nameof(textBox), $"{nameof(textBox)} parameter cannot be null");
            }

            InternalSetFormattedMessage(serviceBusHelper.GetMessageText(message, out _), textBox);
        }

        public static void SetFormattedMessage(ServiceBusHelper serviceBusHelper, string messageText, FastColoredTextBox textBox)
        {
            if (serviceBusHelper == null)
            {
                throw new ArgumentNullException(nameof(serviceBusHelper), $"{nameof(serviceBusHelper)} parameter cannot be null");
            }

            if (string.IsNullOrEmpty(messageText))
            {
                throw new ArgumentNullException(nameof(messageText), $"{nameof(messageText)} parameter cannot be null");
            }

            if (textBox == null)
            {
                throw new ArgumentNullException(nameof(textBox), $"{nameof(textBox)} parameter cannot be null");
            }

            InternalSetFormattedMessage(messageText, textBox);
        }

        public static void SetFormattedMessage(string language, FastColoredTextBox textBox)
        {
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentNullException(nameof(language), $"{nameof(language)} parameter cannot be null");
            }

            if (textBox == null)
            {
                throw new ArgumentNullException(nameof(textBox), $"{nameof(textBox)} parameter cannot be null");
            }

            textBox.ClearStylesBuffer();
            textBox.Range.ClearStyle(StyleIndex.All);

            switch (language)
            {
                case "JSON":
                    textBox.Language = Language.JSON;
                    break;
                case "XML":
                    textBox.Language = Language.HTML;
                    break;
                default:
                    textBox.Language = Language.Custom;
                    break;
            }
            textBox.OnTextChanged();
        }
        #endregion

        #region Private Static Methods
        private static void InternalSetFormattedMessage(string message, FastColoredTextBox textBox)
        {
            if (JsonSerializerHelper.IsJson(message))
            {
                textBox.Language = Language.JSON;
                textBox.Text = JsonSerializerHelper.Indent(message);
            }
            else if (XmlHelper.IsXml(message))
            {
                textBox.Language = Language.HTML;
                textBox.Text = XmlHelper.Indent(message);
            }
            else
            {
                textBox.Language = Language.Custom;
                textBox.Text = string.IsNullOrEmpty(message) ? "" : message;
            }
        }
        #endregion
    }
}
