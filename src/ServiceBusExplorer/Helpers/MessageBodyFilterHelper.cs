#region Copyright

//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// Author: Fork contribution
//=======================================================================================

#endregion

#region Using Directives

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Utilities.Helpers;

#endregion

namespace ServiceBusExplorer.Helpers
{
    internal static class MessageBodyFilterHelper
    {
        /// <summary>
        /// Populates the body cache for all messages in the binding list that are not already cached.
        /// </summary>
        internal static void EnsureMessageBodyCache(
            SortableBindingList<BrokeredMessage> bindingList,
            IDictionary<BrokeredMessage, string> cache,
            Func<BrokeredMessage, string> getBodyFunc,
            WriteToLogDelegate writeToLog)
        {
            foreach (var msg in bindingList)
            {
                if (!cache.ContainsKey(msg))
                {
                    try
                    {
                        cache[msg] = getBodyFunc(msg);
                    }
                    catch (Exception ex)
                    {
                        writeToLog($"[BodyFilter] Failed to get body for message {msg.MessageId}: {ex.Message}");
                        cache[msg] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether a message matches the given body filter criteria.
        /// </summary>
        internal static bool MatchesBodyFilter(BrokeredMessage msg, IDictionary<BrokeredMessage, string> cache,
            string freeText, string jsonPath, string jsonValue, bool caseSensitive)
        {
            if (!cache.TryGetValue(msg, out var body))
                body = string.Empty;

            var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            if (!string.IsNullOrWhiteSpace(freeText))
            {
                if (string.IsNullOrEmpty(body) || body.IndexOf(freeText, comparison) < 0)
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(jsonPath))
            {
                var val = BodyFilterForm.GetJsonValueAtPath(body, jsonPath);
                if (val == null)
                    return false;

                if (!string.IsNullOrWhiteSpace(jsonValue))
                {
                    if (val.IndexOf(jsonValue, comparison) < 0)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns true if any body-level filter is active.
        /// </summary>
        internal static bool HasBodyFilter(string freeText, string jsonPath)
        {
            return !string.IsNullOrWhiteSpace(freeText) || !string.IsNullOrWhiteSpace(jsonPath);
        }

        /// <summary>
        /// Creates an inline search textbox with placeholder text.
        /// </summary>
        internal static TextBox AddInlineSearchBox(Control parent, PictureBox afterButton, int extraOffset, EventHandler textChangedHandler)
        {
            var startX = afterButton.Location.X + afterButton.Size.Width + extraOffset;

            var txt = new TextBox
            {
                Location = new Point(startX, 2),
                Size = new Size(180, 20),
                Font = new Font("Microsoft Sans Serif", 8.25F),
                ForeColor = SystemColors.GrayText,
                Text = "Search body...",
                Tag = true, // Tag tracks placeholder state: true = showing placeholder
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            txt.GotFocus += (s, e) =>
            {
                if (txt.Tag is true)
                {
                    txt.Text = string.Empty;
                    txt.ForeColor = SystemColors.WindowText;
                    txt.Tag = false;
                }
            };
            txt.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.Tag = true;
                    txt.ForeColor = SystemColors.GrayText;
                    txt.Text = "Search body...";
                }
            };
            txt.TextChanged += textChangedHandler;

            parent.Controls.Add(txt);
            return txt;
        }

        /// <summary>
        /// Creates a dead letter reason search textbox with placeholder text.
        /// Returns the TextBox and a ToolTip that must be disposed by the caller.
        /// </summary>
        internal static (TextBox textBox, ToolTip toolTip) AddDeadLetterReasonSearchBox(Control parent, TextBox afterTextBox)
        {
            var txt = new TextBox
            {
                Location = new Point(afterTextBox.Location.X + afterTextBox.Size.Width + 8, 2),
                Size = new Size(180, 20),
                Font = new Font("Microsoft Sans Serif", 8.25F),
                ForeColor = SystemColors.GrayText,
                Text = "Search DL reason...",
                Tag = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            txt.GotFocus += (s, e) =>
            {
                if (txt.Tag is true)
                {
                    txt.Text = string.Empty;
                    txt.ForeColor = SystemColors.WindowText;
                    txt.Tag = false;
                }
            };
            txt.LostFocus += (s, e) =>
            {
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.Tag = true;
                    txt.ForeColor = SystemColors.GrayText;
                    txt.Text = "Search DL reason...";
                }
            };

            var tooltip = new ToolTip();
            tooltip.SetToolTip(txt, "Filter by Dead Letter Reason (partial match)");

            parent.Controls.Add(txt);
            return (txt, tooltip);
        }
    }
}
