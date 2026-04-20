#region Copyright

//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// Author: Fork contribution
//=======================================================================================

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer.Forms
{
    public partial class BodyFilterForm : Form
    {
        #region Private Fields

        private readonly List<string> jsonPaths;

        #endregion

        #region Public Properties

        public string PropertyFilterExpression { get; private set; }
        public string FreeTextFilter { get; private set; }
        public string SelectedJsonPath { get; private set; }
        public string JsonPathValue { get; private set; }
        public bool IsCaseSensitive { get; private set; }

        #endregion

        #region Constructor

        public BodyFilterForm(List<string> availableJsonPaths, string currentPropertyFilter, string currentFreeText, string currentJsonPath, string currentJsonValue, bool currentCaseSensitive)
        {
            jsonPaths = availableJsonPaths ?? new List<string>();
            InitializeForm();

            txtPropertyFilter.Text = currentPropertyFilter ?? string.Empty;
            txtFreeText.Text = currentFreeText ?? string.Empty;
            chkCaseSensitive.Checked = currentCaseSensitive;

            if (jsonPaths.Any())
            {
                cboJsonPath.Items.AddRange(jsonPaths.ToArray());
                if (!string.IsNullOrEmpty(currentJsonPath))
                {
                    cboJsonPath.SelectedItem = currentJsonPath;
                }
                txtJsonValue.Text = currentJsonValue ?? string.Empty;
            }
            else
            {
                grpJsonFilter.Enabled = false;
                lblNoJson.Visible = true;
            }
        }

        #endregion

        #region Private Fields (Controls)

        private TextBox txtPropertyFilter;
        private TextBox txtFreeText;
        private ComboBox cboJsonPath;
        private TextBox txtJsonValue;
        private CheckBox chkCaseSensitive;
        private GroupBox grpPropertyFilter;
        private GroupBox grpFreeText;
        private GroupBox grpJsonFilter;
        private Label lblNoJson;
        private Button btnOk;
        private Button btnCancel;
        private Button btnClear;

        #endregion

        #region Form Setup

        private void InitializeForm()
        {
            Text = "Message Filter";
            Size = new Size(520, 430);
            MinimumSize = new Size(520, 430);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            // Property filter group (SQL expression on message properties)
            grpPropertyFilter = new GroupBox
            {
                Text = "Property Filter (SQL expression on message properties)",
                Location = new Point(12, 12),
                Size = new Size(480, 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblPropertyFilter = new Label
            {
                Text = "Expression:",
                Location = new Point(10, 25),
                AutoSize = true
            };

            txtPropertyFilter = new TextBox
            {
                Location = new Point(90, 22),
                Size = new Size(375, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            grpPropertyFilter.Controls.AddRange(new Control[] { lblPropertyFilter, txtPropertyFilter });

            // Free text group
            grpFreeText = new GroupBox
            {
                Text = "Body Text Search (in message body)",
                Location = new Point(12, 80),
                Size = new Size(480, 80),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblFreeText = new Label
            {
                Text = "Search text:",
                Location = new Point(10, 28),
                AutoSize = true
            };

            txtFreeText = new TextBox
            {
                Location = new Point(90, 25),
                Size = new Size(375, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            chkCaseSensitive = new CheckBox
            {
                Text = "Case sensitive",
                Location = new Point(90, 52),
                AutoSize = true
            };

            grpFreeText.Controls.AddRange(new Control[] { lblFreeText, txtFreeText, chkCaseSensitive });

            // JSON filter group
            grpJsonFilter = new GroupBox
            {
                Text = "JSON Property Filter",
                Location = new Point(12, 168),
                Size = new Size(480, 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblPath = new Label
            {
                Text = "JSON path:",
                Location = new Point(10, 28),
                AutoSize = true
            };

            cboJsonPath = new ComboBox
            {
                Location = new Point(90, 25),
                Size = new Size(375, 21),
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblValue = new Label
            {
                Text = "Contains:",
                Location = new Point(10, 58),
                AutoSize = true
            };

            txtJsonValue = new TextBox
            {
                Location = new Point(90, 55),
                Size = new Size(375, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblHint = new Label
            {
                Text = "Select a JSON property path and enter a value to match. Both filters can be combined.",
                Location = new Point(10, 85),
                Size = new Size(460, 40),
                ForeColor = SystemColors.GrayText
            };

            lblNoJson = new Label
            {
                Text = "No JSON properties detected in current messages.",
                Location = new Point(10, 50),
                Size = new Size(460, 20),
                ForeColor = Color.OrangeRed,
                Visible = false
            };

            grpJsonFilter.Controls.AddRange(new Control[] { lblPath, cboJsonPath, lblValue, txtJsonValue, lblHint, lblNoJson });

            // Buttons
            btnOk = new Button
            {
                Text = "Apply",
                DialogResult = DialogResult.OK,
                Location = new Point(310, 345),
                Size = new Size(85, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnOk.Click += BtnOk_Click;

            btnClear = new Button
            {
                Text = "Clear All",
                Location = new Point(210, 345),
                Size = new Size(85, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnClear.Click += BtnClear_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(405, 345),
                Size = new Size(85, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            Controls.AddRange(new Control[] { grpPropertyFilter, grpFreeText, grpJsonFilter, btnOk, btnClear, btnCancel });
        }

        #endregion

        #region Event Handlers

        private void BtnOk_Click(object sender, EventArgs e)
        {
            PropertyFilterExpression = txtPropertyFilter.Text.Trim();
            FreeTextFilter = txtFreeText.Text.Trim();
            SelectedJsonPath = cboJsonPath.SelectedItem?.ToString() ?? cboJsonPath.Text.Trim();
            JsonPathValue = txtJsonValue.Text.Trim();
            IsCaseSensitive = chkCaseSensitive.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            PropertyFilterExpression = string.Empty;
            FreeTextFilter = string.Empty;
            SelectedJsonPath = string.Empty;
            JsonPathValue = string.Empty;
            IsCaseSensitive = false;
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        #region Static Helpers

        /// <summary>
        /// Extracts all unique JSON property paths from a collection of message body strings.
        /// Supports nested objects with dot notation (e.g. "order.customer.name").
        /// </summary>
        public static List<string> ExtractJsonPaths(IEnumerable<string> messageBodies)
        {
            var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var body in messageBodies)
            {
                if (string.IsNullOrWhiteSpace(body))
                    continue;

                var trimmed = body.TrimStart();
                if (!trimmed.StartsWith("{") && !trimmed.StartsWith("["))
                    continue;

                try
                {
                    ExtractPathsFromJson(trimmed, string.Empty, paths);
                }
                catch
                {
                    // Not valid JSON, skip
                }
            }

            var result = paths.ToList();
            result.Sort(StringComparer.OrdinalIgnoreCase);
            return result;
        }

        private static void ExtractPathsFromJson(string json, string prefix, HashSet<string> paths)
        {
            // Simple JSON parser for key extraction - handles objects and arrays
            json = json.Trim();

            if (json.StartsWith("["))
            {
                // For arrays, try to parse elements
                var items = ParseJsonArray(json);
                foreach (var item in items)
                {
                    ExtractPathsFromJson(item, prefix, paths);
                }
                return;
            }

            if (!json.StartsWith("{"))
                return;

            var kvPairs = ParseJsonObject(json);
            foreach (var kvp in kvPairs)
            {
                var fullPath = string.IsNullOrEmpty(prefix) ? kvp.Key : $"{prefix}.{kvp.Key}";
                paths.Add(fullPath);

                var val = kvp.Value.Trim();
                if (val.StartsWith("{") || val.StartsWith("["))
                {
                    ExtractPathsFromJson(val, fullPath, paths);
                }
            }
        }

        private static List<KeyValuePair<string, string>> ParseJsonObject(string json)
        {
            var result = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(json) || json.Length < 2)
                return result;

            // Remove outer braces
            var inner = json.Substring(1, json.Length - 2).Trim();
            if (string.IsNullOrEmpty(inner))
                return result;

            var i = 0;
            while (i < inner.Length)
            {
                // Skip whitespace
                while (i < inner.Length && char.IsWhiteSpace(inner[i])) i++;
                if (i >= inner.Length) break;

                // Skip comma
                if (inner[i] == ',') { i++; continue; }

                // Parse key (expect quoted string)
                if (inner[i] != '"') break;
                var key = ParseJsonString(inner, ref i);
                if (key == null) break;

                // Skip colon
                while (i < inner.Length && char.IsWhiteSpace(inner[i])) i++;
                if (i >= inner.Length || inner[i] != ':') break;
                i++; // skip ':'
                while (i < inner.Length && char.IsWhiteSpace(inner[i])) i++;

                // Parse value
                var value = ParseJsonValue(inner, ref i);
                if (value == null) break;

                result.Add(new KeyValuePair<string, string>(key, value));
            }

            return result;
        }

        private static List<string> ParseJsonArray(string json)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(json) || json.Length < 2) return result;

            var inner = json.Substring(1, json.Length - 2).Trim();
            if (string.IsNullOrEmpty(inner)) return result;

            var i = 0;
            while (i < inner.Length)
            {
                while (i < inner.Length && char.IsWhiteSpace(inner[i])) i++;
                if (i >= inner.Length) break;
                if (inner[i] == ',') { i++; continue; }

                var value = ParseJsonValue(inner, ref i);
                if (value != null) result.Add(value);
                else break;

                // Limit to first 10 items for path extraction
                if (result.Count >= 10) break;
            }
            return result;
        }

        private static string ParseJsonString(string json, ref int i)
        {
            if (i >= json.Length || json[i] != '"') return null;
            i++; // skip opening quote
            var start = i;
            while (i < json.Length)
            {
                if (json[i] == '\\') { i += 2; continue; }
                if (json[i] == '"') { var s = json.Substring(start, i - start); i++; return s; }
                i++;
            }
            return null;
        }

        private static string ParseJsonValue(string json, ref int i)
        {
            if (i >= json.Length) return null;

            if (json[i] == '"')
            {
                var start = i;
                var str = ParseJsonString(json, ref i);
                return str != null ? $"\"{str}\"" : null;
            }

            if (json[i] == '{' || json[i] == '[')
            {
                return ParseJsonBlock(json, ref i);
            }

            // Number, bool, null
            var vStart = i;
            while (i < json.Length && json[i] != ',' && json[i] != '}' && json[i] != ']' && !char.IsWhiteSpace(json[i]))
                i++;
            return json.Substring(vStart, i - vStart);
        }

        private static string ParseJsonBlock(string json, ref int i)
        {
            var open = json[i];
            var close = open == '{' ? '}' : ']';
            var depth = 1;
            var start = i;
            i++;
            var inString = false;
            while (i < json.Length && depth > 0)
            {
                if (json[i] == '\\' && inString) { i += 2; continue; }
                if (json[i] == '"') inString = !inString;
                if (!inString)
                {
                    if (json[i] == open) depth++;
                    if (json[i] == close) depth--;
                }
                i++;
            }
            return json.Substring(start, i - start);
        }

        /// <summary>
        /// Gets the value at a specific JSON path from a body string.
        /// </summary>
        public static string GetJsonValueAtPath(string body, string path)
        {
            if (string.IsNullOrWhiteSpace(body) || string.IsNullOrWhiteSpace(path))
                return null;

            var trimmed = body.TrimStart();
            if (!trimmed.StartsWith("{") && !trimmed.StartsWith("["))
                return null;

            try
            {
                var parts = path.Split('.');
                var current = trimmed;

                foreach (var part in parts)
                {
                    current = current.Trim();

                    if (current.StartsWith("["))
                    {
                        // Search in array items
                        var items = ParseJsonArray(current);
                        string found = null;
                        foreach (var item in items)
                        {
                            found = GetJsonValueAtPath(item, part);
                            if (found != null) break;
                        }
                        current = found;
                    }
                    else if (current.StartsWith("{"))
                    {
                        var kvPairs = ParseJsonObject(current);
                        var match = kvPairs.FirstOrDefault(kv =>
                            string.Equals(kv.Key, part, StringComparison.OrdinalIgnoreCase));
                        current = match.Value;
                    }
                    else
                    {
                        return null;
                    }

                    if (current == null) return null;
                }

                // Remove surrounding quotes if present
                if (current != null && current.StartsWith("\"") && current.EndsWith("\"") && current.Length >= 2)
                {
                    current = current.Substring(1, current.Length - 2);
                }

                return current;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
