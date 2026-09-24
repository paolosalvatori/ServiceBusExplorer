using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using FastColoredTextBoxNS;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Properties;

namespace ServiceBusExplorer.UIHelpers
{
    public enum ThemeMode
    {
        Light,
        Dark
    }

    /// <summary>
    /// Applies the dark theme to open forms. The selected mode is read once at startup;
    /// changing it requires an application restart, so there is no "restore light" path.
    /// </summary>
    public static class ThemeManager
    {
        private static readonly Color FormBackColor = Color.FromArgb(28, 30, 34);
        private static readonly Color ControlBackColor = Color.FromArgb(43, 46, 52);
        private static readonly Color InputBackColor = Color.FromArgb(23, 25, 29);
        private static readonly Color BorderColor = Color.FromArgb(93, 99, 108);
        private static readonly Color ForeColor = Color.FromArgb(242, 244, 247);
        private static readonly Color SecondaryForeColor = Color.FromArgb(198, 203, 211);
        private static readonly Color SelectionBackColor = Color.FromArgb(47, 111, 179);

        // Syntax colors for FastColoredTextBox (readable on InputBackColor).
        private static readonly Brush KeywordBrush = new SolidBrush(Color.FromArgb(102, 187, 255));
        private static readonly Brush StringBrush = new SolidBrush(Color.FromArgb(255, 179, 71));
        private static readonly Brush NumberBrush = new SolidBrush(Color.FromArgb(255, 204, 128));
        private static readonly Brush CommentBrush = new SolidBrush(Color.FromArgb(128, 138, 145));

        private static readonly HashSet<Form> AppliedForms = new HashSet<Form>();
        private static ThemeMode? cachedMode;
        private static bool initialized;
        private static bool isDarkEnabled;

        public static Color SurfaceColor => ControlBackColor;
        public static Color SurfaceBorderColor => BorderColor;
        public static Color InputColor => InputBackColor;
        public static Color TextColor => ForeColor;

        public static bool IsDarkEnabled => isDarkEnabled;

        /// <summary>
        /// Gets the configured mode. Setting it only persists the choice; it takes effect on the next start.
        /// </summary>
        public static ThemeMode CurrentMode
        {
            get => GetConfiguredMode();
            set
            {
                Settings.Default.ThemeMode = value.ToString();
                Settings.Default.Save();
            }
        }

        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }

            initialized = true;
            isDarkEnabled = GetConfiguredMode() == ThemeMode.Dark;

            if (isDarkEnabled)
            {
                Application.Idle += Application_Idle;
            }
        }

        public static void ApplyToOpenForms()
        {
            if (!isDarkEnabled)
            {
                return;
            }

            var applied = false;
            foreach (Form form in Application.OpenForms)
            {
                if (!AppliedForms.Add(form))
                {
                    continue;
                }

                form.Disposed -= Form_Disposed;
                form.Disposed += Form_Disposed;
                ApplyControl(form);
                applied = true;
            }

            if (applied)
            {
                RefreshOpenForms();
            }
        }

        public static void ReapplyToOpenForms()
        {
            AppliedForms.Clear();
            ApplyToOpenForms();
        }

        internal static ThemeMode ParseConfiguredMode(string configuredModeString)
        {
            // Accept only the explicit Dark name; any other value falls back to Light.
            return string.Equals(configuredModeString, nameof(ThemeMode.Dark), StringComparison.OrdinalIgnoreCase)
                ? ThemeMode.Dark
                : ThemeMode.Light;
        }

        private static ThemeMode GetConfiguredMode()
        {
            if (cachedMode.HasValue)
            {
                return cachedMode.Value;
            }

            try
            {
                cachedMode = ParseConfiguredMode(Settings.Default.ThemeMode);
            }
            catch (Exception)
            {
                // A corrupt user.config must not prevent the application from starting.
                cachedMode = ThemeMode.Light;
            }

            return cachedMode.Value;
        }

        private static void Application_Idle(object sender, EventArgs e)
        {
            ApplyToOpenForms();
        }

        private static void Form_Disposed(object sender, EventArgs e)
        {
            var form = (Form)sender;
            form.Disposed -= Form_Disposed;
            AppliedForms.Remove(form);
        }

        private static void RefreshOpenForms()
        {
            foreach (Form form in Application.OpenForms)
            {
                form.PerformLayout();
                form.Invalidate(true);
                form.Update();
            }
        }

        private static void Control_Added(object sender, ControlEventArgs e)
        {
            if (isDarkEnabled)
            {
                ApplyControl(e.Control);
            }
        }

        private static void Control_BackColorChanged(object sender, EventArgs e)
        {
            if (isDarkEnabled)
            {
                SetDarkColors((Control)sender);
            }
        }

        private static void Control_ForeColorChanged(object sender, EventArgs e)
        {
            if (isDarkEnabled)
            {
                SetDarkColors((Control)sender);
            }
        }

        private static void ApplyControl(Control control)
        {
            control.ControlAdded -= Control_Added;
            control.ControlAdded += Control_Added;
            control.BackColorChanged -= Control_BackColorChanged;
            control.BackColorChanged += Control_BackColorChanged;
            control.ForeColorChanged -= Control_ForeColorChanged;
            control.ForeColorChanged += Control_ForeColorChanged;

            SetDarkColors(control);

            if (control is FastColoredTextBox textBox)
            {
                ApplyFastColoredTextBox(textBox);
            }

            if (control is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = BorderColor;
                button.FlatAppearance.MouseOverBackColor = BorderColor;
                button.FlatAppearance.MouseDownBackColor = SelectionBackColor;
            }

            if (control is DataGridView dataGridView)
            {
                dataGridView.EnableHeadersVisualStyles = false;
                dataGridView.BackgroundColor = InputBackColor;
                dataGridView.GridColor = BorderColor;
                dataGridView.DefaultCellStyle.BackColor = InputBackColor;
                dataGridView.DefaultCellStyle.ForeColor = ForeColor;
                dataGridView.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
                dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = ControlBackColor;
                dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = ForeColor;
                dataGridView.RowHeadersDefaultCellStyle.BackColor = ControlBackColor;
                dataGridView.RowHeadersDefaultCellStyle.ForeColor = ForeColor;
                dataGridView.RowsDefaultCellStyle.BackColor = InputBackColor;
                dataGridView.RowsDefaultCellStyle.ForeColor = ForeColor;
                dataGridView.RowsDefaultCellStyle.SelectionBackColor = SelectionBackColor;
                dataGridView.RowsDefaultCellStyle.SelectionForeColor = Color.White;
                dataGridView.AlternatingRowsDefaultCellStyle.BackColor = ControlBackColor;
                dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = ForeColor;
                dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = SelectionBackColor;
                dataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            }

            if (control is Grouper grouper)
            {
                grouper.BackgroundColor = ControlBackColor;
                grouper.BackgroundGradientColor = ControlBackColor;
                grouper.BorderColor = BorderColor;
                grouper.CustomGroupBoxColor = ControlBackColor;
                grouper.ForeColor = ForeColor;
                grouper.BackColor = FormBackColor;
            }

            if (control is HeaderPanel headerPanel)
            {
                headerPanel.BackColor = InputBackColor;
                headerPanel.ForeColor = ForeColor;
                headerPanel.HeaderColor1 = ControlBackColor;
                headerPanel.HeaderColor2 = BorderColor;
            }

            if (control is PropertyGrid propertyGrid)
            {
                propertyGrid.ViewBackColor = ControlBackColor;
                propertyGrid.ViewForeColor = ForeColor;
                propertyGrid.HelpBackColor = ControlBackColor;
                propertyGrid.HelpForeColor = ForeColor;
                propertyGrid.LineColor = BorderColor;
                propertyGrid.CategoryForeColor = SecondaryForeColor;
            }

            if (control is ToolStrip toolStrip)
            {
                ApplyToolStrip(toolStrip);
            }

            if (control.ContextMenuStrip != null)
            {
                ApplyToolStrip(control.ContextMenuStrip);
            }

            foreach (Control child in control.Controls)
            {
                ApplyControl(child);
            }
        }

        private static void SetDarkColors(Control control)
        {
            var isInput = control is TextBoxBase
                          || control is ComboBox
                          || control is NumericUpDown
                          || control is ListControl
                          || control is FastColoredTextBox;
            var isLabel = control is Label || control is LinkLabel;

            var backColor = isInput
                ? InputBackColor
                : isLabel
                    ? FormBackColor
                    : ControlBackColor;
            var foreColor = isLabel ? SecondaryForeColor : ForeColor;

            if (control.BackColor != backColor)
            {
                control.BackColor = backColor;
            }

            if (control.ForeColor != foreColor)
            {
                control.ForeColor = foreColor;
            }
        }

        private static void ApplyToolStrip(ToolStrip toolStrip)
        {
            toolStrip.BackColor = ControlBackColor;
            toolStrip.ForeColor = ForeColor;

            foreach (ToolStripItem item in toolStrip.Items)
            {
                item.BackColor = ControlBackColor;
                item.ForeColor = ForeColor;

                // HasDropDownItems avoids creating an empty DropDown for every leaf item.
                if (item is ToolStripDropDownItem dropDownItem && dropDownItem.HasDropDownItems)
                {
                    ApplyToolStrip(dropDownItem.DropDown);
                }
            }
        }

        private static void ApplyFastColoredTextBox(FastColoredTextBox textBox)
        {
            // BackColor/ForeColor are handled in SetDarkColors.
            textBox.IndentBackColor = ControlBackColor;      // line-number gutter
            textBox.LineNumberColor = SecondaryForeColor;
            textBox.CaretColor = ForeColor;
            textBox.ServiceLinesColor = BorderColor;
            textBox.SelectionColor = Color.FromArgb(80, SelectionBackColor);

            var highlighter = textBox.SyntaxHighlighter;
            if (highlighter == null)
            {
                return;
            }

            // Several aliases can point to the same underlying style object (e.g. StringStyle and
            // BrownStyle), so each style is recolored once; the first (semantic) mapping wins.
            var recolored = new HashSet<Style>();
            Recolor(recolored, highlighter.KeywordStyle, KeywordBrush);
            Recolor(recolored, highlighter.StringStyle, StringBrush);
            Recolor(recolored, highlighter.NumberStyle, NumberBrush);
            Recolor(recolored, highlighter.CommentStyle, CommentBrush);

            // Concrete styles used directly by the built-in highlighters (e.g. JSON).
            Recolor(recolored, highlighter.BlueStyle, KeywordBrush);
            Recolor(recolored, highlighter.MaroonStyle, StringBrush);
            Recolor(recolored, highlighter.BrownStyle, StringBrush);
            Recolor(recolored, highlighter.MagentaStyle, NumberBrush);
            Recolor(recolored, highlighter.GreenStyle, CommentBrush);
        }

        private static void Recolor(ISet<Style> recolored, Style style, Brush brush)
        {
            if (style is TextStyle textStyle && recolored.Add(style))
            {
                textStyle.ForeBrush = brush;
            }
        }
    }
}
