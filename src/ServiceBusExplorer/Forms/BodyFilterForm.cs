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
using Newtonsoft.Json.Linq;

#endregion

namespace ServiceBusExplorer.Forms
{
    public class BodyFilterForm : Form
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
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;

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
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            PropertyFilterExpression = string.Empty;
            FreeTextFilter = string.Empty;
            SelectedJsonPath = string.Empty;
            JsonPathValue = string.Empty;
            IsCaseSensitive = false;
            DialogResult = DialogResult.OK;
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
                    var token = JToken.Parse(trimmed);
                    ExtractPathsFromToken(token, string.Empty, paths);
                }
                catch (Newtonsoft.Json.JsonException)
                {
                    // Not valid JSON, skip
                }
            }

            var result = paths.ToList();
            result.Sort(StringComparer.OrdinalIgnoreCase);
            return result;
        }

        private static void ExtractPathsFromToken(JToken token, string prefix, HashSet<string> paths, int depth = 0)
        {
            if (depth > 10) return;

            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (var property in ((JObject)token).Properties())
                    {
                        var fullPath = string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}.{property.Name}";
                        paths.Add(fullPath);
                        ExtractPathsFromToken(property.Value, fullPath, paths, depth + 1);
                    }
                    break;

                case JTokenType.Array:
                    var count = 0;
                    foreach (var item in (JArray)token)
                    {
                        ExtractPathsFromToken(item, prefix, paths, depth + 1);
                        if (++count >= 10) break;
                    }
                    break;
            }
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
                var token = JToken.Parse(trimmed);
                var parts = path.Split('.');

                foreach (var part in parts)
                {
                    if (token == null) return null;

                    switch (token.Type)
                    {
                        case JTokenType.Object:
                            token = ((JObject)token).GetValue(part, StringComparison.OrdinalIgnoreCase);
                            break;

                        case JTokenType.Array:
                            // Search array items for the property
                            JToken found = null;
                            foreach (var item in (JArray)token)
                            {
                                if (item is JObject obj)
                                {
                                    found = obj.GetValue(part, StringComparison.OrdinalIgnoreCase);
                                    if (found != null) break;
                                }
                            }
                            token = found;
                            break;

                        default:
                            return null;
                    }
                }

                if (token == null) return null;

                return token.Type == JTokenType.String
                    ? token.Value<string>()
                    : token.ToString();
            }
            catch (Newtonsoft.Json.JsonException)
            {
                return null;
            }
        }

        #endregion
    }
}
