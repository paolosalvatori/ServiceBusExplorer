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

#endregion

namespace Microsoft.Azure.ServiceBusExplorer.Helpers
{
    public class LanguageDetector
    {
        #region Public Static Methods
        public static void SetFormattedMessage(ServiceBusHelper serviceBusHelper, BrokeredMessage message, FastColoredTextBox textBox)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message", "message parameter cannot be null");
            }

            if (textBox == null)
            {
                throw new ArgumentNullException("textBox", "textBox parameter cannot be null");
            }

            var messageText = serviceBusHelper.GetMessageText(message, out _);

            if (JsonSerializerHelper.IsJson(messageText))
            {
                textBox.Language = Language.JSON;
                textBox.Text = JsonSerializerHelper.Indent(messageText);
            }
            else if (XmlHelper.IsXml(messageText))
            {
                textBox.Language = Language.HTML;
                textBox.Text = XmlHelper.Indent(messageText);
            }
            else
            {
                textBox.Text = messageText;
            }
        }

        public static void SetFormattedMessage(ServiceBusHelper serviceBusHelper, EventData message, FastColoredTextBox textBox)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message", "message parameter cannot be null");
            }

            if (textBox == null)
            {
                throw new ArgumentNullException("textBox", "textBox parameter cannot be null");
            }

            var messageText = serviceBusHelper.GetMessageText(message, out _);

            if (JsonSerializerHelper.IsJson(messageText))
            {
                textBox.Language = Language.JSON;
                textBox.Text = JsonSerializerHelper.Indent(messageText);
            }
            else if (XmlHelper.IsXml(messageText))
            {
                textBox.Language = Language.HTML;
                textBox.Text = XmlHelper.Indent(messageText);
            }
            else
            {
                textBox.Text = messageText;
            }
        }

        public static void SetFormattedMessage(ServiceBusHelper serviceBusHelper, string message, FastColoredTextBox textBox)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("message", "message parameter cannot be null");
            }

            if (textBox == null)
            {
                throw new ArgumentNullException("textBox", "textBox parameter cannot be null");
            }

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
                textBox.Text = message;
            }
        }
        #endregion
    }
}
