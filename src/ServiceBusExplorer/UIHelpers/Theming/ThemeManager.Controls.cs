using System;
using System.Drawing;
using System.Windows.Forms;
using ServiceBusExplorer.Controls;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    public static partial class ThemeManager
    {
        private static void CaptureControl(Control control, ThemeSnapshot state)
        {
            if (IsThemeExcludedControl(control))
                return;

            state.CaptureAmbientColors(control);
            switch (control)
            {
                case Form form:
                    state.Capture("BackgroundImage", () => form.BackgroundImage, value => form.BackgroundImage = value);
                    break;
                case Button button:
                    state.Capture("FlatStyle", () => button.FlatStyle, value => button.FlatStyle = value);
                    state.Capture("VisualStyle", () => button.UseVisualStyleBackColor, value => button.UseVisualStyleBackColor = value);
                    state.Capture("ButtonBorder", () => button.FlatAppearance.BorderColor, value => button.FlatAppearance.BorderColor = value);
                    state.Capture("ButtonHover", () => button.FlatAppearance.MouseOverBackColor, value => button.FlatAppearance.MouseOverBackColor = value);
                    state.Capture("ButtonPressed", () => button.FlatAppearance.MouseDownBackColor, value => button.FlatAppearance.MouseDownBackColor = value);
                    break;
                case TabControl tabs:
                    state.Capture("DrawMode", () => tabs.DrawMode, value => tabs.DrawMode = value);
                    state.Capture("Padding", () => tabs.Padding, value => tabs.Padding = value);
                    break;
                case TabPage page:
                    state.Capture("VisualStyle", () => page.UseVisualStyleBackColor, value => page.UseVisualStyleBackColor = value);
                    break;
                case ComboBox combo:
                    state.Capture("FlatStyle", () => combo.FlatStyle, value => combo.FlatStyle = value);
                    state.Capture("DrawMode", () => combo.DrawMode, value => combo.DrawMode = value);
                    break;
                case TreeView tree:
                    state.Capture("LineColor", () => tree.LineColor, value => tree.LineColor = value);
                    state.Capture("DrawMode", () => tree.DrawMode, value => tree.DrawMode = value);
                    break;
                case ListView list:
                    state.Capture("OwnerDraw", () => list.OwnerDraw, value => list.OwnerDraw = value);
                    break;
                case DataGridView grid:
                    CaptureGrid(grid, state);
                    break;
                case PropertyGrid grid:
                    state.Capture("ViewBack", () => grid.ViewBackColor, value => grid.ViewBackColor = value);
                    state.Capture("ViewFore", () => grid.ViewForeColor, value => grid.ViewForeColor = value);
                    state.Capture("Line", () => grid.LineColor, value => grid.LineColor = value);
                    state.Capture("CategoryFore", () => grid.CategoryForeColor, value => grid.CategoryForeColor = value);
                    state.Capture("CategorySplitter", () => grid.CategorySplitterColor, value => grid.CategorySplitterColor = value);
                    state.Capture("HelpBack", () => grid.HelpBackColor, value => grid.HelpBackColor = value);
                    state.Capture("HelpFore", () => grid.HelpForeColor, value => grid.HelpForeColor = value);
                    state.Capture("HelpBorder", () => grid.HelpBorderColor, value => grid.HelpBorderColor = value);
                    state.Capture("CommandsBack", () => grid.CommandsBackColor, value => grid.CommandsBackColor = value);
                    state.Capture("CommandsFore", () => grid.CommandsForeColor, value => grid.CommandsForeColor = value);
                    state.Capture("CommandsBorder", () => grid.CommandsBorderColor, value => grid.CommandsBorderColor = value);
                    state.Capture("DisabledFore", () => grid.DisabledItemForeColor, value => grid.DisabledItemForeColor = value);
                    state.Capture("SelectedBack", () => grid.SelectedItemWithFocusBackColor, value => grid.SelectedItemWithFocusBackColor = value);
                    state.Capture("SelectedFore", () => grid.SelectedItemWithFocusForeColor, value => grid.SelectedItemWithFocusForeColor = value);
                    break;
                case Grouper group:
                    state.Capture("Background", () => group.BackgroundColor, value => group.BackgroundColor = value);
                    state.Capture("Gradient", () => group.BackgroundGradientColor, value => group.BackgroundGradientColor = value);
                    state.Capture("Border", () => group.BorderColor, value => group.BorderColor = value);
                    state.Capture("Caption", () => group.CustomGroupBoxColor, value => group.CustomGroupBoxColor = value);
                    state.Capture("Shadow", () => group.ShadowColor, value => group.ShadowColor = value);
                    break;
                case HeaderPanel header:
                    state.Capture("HeaderBegin", () => header.HeaderColor1, value => header.HeaderColor1 = value);
                    state.Capture("HeaderEnd", () => header.HeaderColor2, value => header.HeaderColor2 = value);
                    break;
                case ToolStrip strip:
                    state.Capture("Renderer", () => strip.Renderer, value => strip.Renderer = value);
                    state.Capture("RenderMode", () => strip.RenderMode, value => strip.RenderMode = value);
                    CaptureItems(strip);
                    break;
            }
            CaptureSpecialControl(control, state);
        }

        private static void ApplyControl(Control control)
        {
            if (IsThemeExcludedControl(control))
                return;

            var palette = Palette;
            var input = control is TextBoxBase || control is ListBox || control is ListView ||
                control is TreeView || control is ComboBox || control is UpDownBase;
            control.BackColor = input ? palette.Surface : palette.Background;
            ApplyThemedForeColor(control);

            switch (control)
            {
                case Form form:
                    form.BackgroundImage = null;
                    break;
                case Button button:
                    button.FlatStyle = FlatStyle.Flat;
                    button.UseVisualStyleBackColor = false;
                    button.BackColor = palette.Raised;
                    button.FlatAppearance.BorderColor = palette.Border;
                    button.FlatAppearance.MouseOverBackColor = palette.Hover;
                    button.FlatAppearance.MouseDownBackColor = palette.Selection;
                    break;
                case LinkLabel link:
                    ApplyLink(link);
                    break;
                case TabControl tabs:
                    tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
                    tabs.Padding = new Point(Math.Max(tabs.Padding.X, 8), Math.Max(tabs.Padding.Y, 3));
                    break;
                case TabPage page:
                    page.UseVisualStyleBackColor = false;
                    break;
                case ComboBox combo:
                    combo.FlatStyle = FlatStyle.Flat;
                    if (combo.DrawMode == DrawMode.Normal && combo.DropDownStyle == ComboBoxStyle.DropDownList)
                        combo.DrawMode = DrawMode.OwnerDrawFixed;
                    break;
                case TreeView tree:
                    tree.LineColor = palette.Border;
                    tree.DrawMode = TreeViewDrawMode.OwnerDrawText;
                    break;
                case ListView list:
                    list.OwnerDraw = true;
                    break;
                case DataGridView grid:
                    ApplyGrid(grid);
                    break;
                case PropertyGrid grid:
                    grid.ViewBackColor = palette.Surface;
                    grid.ViewForeColor = palette.Text;
                    grid.LineColor = palette.Border;
                    grid.CategoryForeColor = palette.Text;
                    grid.CategorySplitterColor = palette.Border;
                    grid.HelpBackColor = palette.Background;
                    grid.HelpForeColor = palette.Text;
                    grid.HelpBorderColor = palette.Border;
                    grid.CommandsBackColor = palette.Background;
                    grid.CommandsForeColor = palette.Text;
                    grid.CommandsBorderColor = palette.Border;
                    grid.DisabledItemForeColor = palette.MutedText;
                    grid.SelectedItemWithFocusBackColor = palette.Selection;
                    grid.SelectedItemWithFocusForeColor = palette.SelectionText;
                    break;
                case Grouper group:
                    group.BackgroundColor = palette.Background;
                    group.BackgroundGradientColor = palette.Background;
                    group.BorderColor = palette.Border;
                    group.CustomGroupBoxColor = palette.Raised;
                    group.ShadowColor = palette.Background;
                    break;
                case HeaderPanel header:
                    header.HeaderColor1 = palette.Raised;
                    header.HeaderColor2 = palette.Raised;
                    break;
                case ToolStrip strip:
                    strip.Renderer = new DarkToolStripRenderer();
                    break;
            }
            ApplySpecialControl(control);
        }

        private static void CaptureGrid(DataGridView grid, ThemeSnapshot state)
        {
            state.Capture("Background", () => grid.BackgroundColor, value => grid.BackgroundColor = value);
            state.Capture("GridColor", () => grid.GridColor, value => grid.GridColor = value);
            state.Capture("HeadersVisualStyle", () => grid.EnableHeadersVisualStyles, value => grid.EnableHeadersVisualStyles = value);
            CaptureStyle(grid.DefaultCellStyle);
            CaptureStyle(grid.RowsDefaultCellStyle);
            CaptureStyle(grid.AlternatingRowsDefaultCellStyle);
            CaptureStyle(grid.ColumnHeadersDefaultCellStyle);
            CaptureStyle(grid.RowHeadersDefaultCellStyle);
            foreach (DataGridViewColumn column in grid.Columns)
                CaptureStyle(column.DefaultCellStyle);
        }

        private static void CaptureStyle(DataGridViewCellStyle style)
        {
            var state = Snapshot(style);
            state.Capture("Back", () => style.BackColor, value => style.BackColor = value);
            state.Capture("Fore", () => style.ForeColor, value => style.ForeColor = value);
            state.Capture("SelectionBack", () => style.SelectionBackColor, value => style.SelectionBackColor = value);
            state.Capture("SelectionFore", () => style.SelectionForeColor, value => style.SelectionForeColor = value);
        }

        private static void ApplyGrid(DataGridView grid)
        {
            grid.BackgroundColor = Palette.Surface;
            grid.GridColor = Palette.Border;
            grid.EnableHeadersVisualStyles = false;
            ApplyStyle(grid.DefaultCellStyle, Palette.Surface);
            ApplyStyle(grid.RowsDefaultCellStyle, Palette.Surface);
            ApplyStyle(grid.AlternatingRowsDefaultCellStyle, Palette.Background);
            ApplyStyle(grid.ColumnHeadersDefaultCellStyle, Palette.Raised);
            ApplyStyle(grid.RowHeadersDefaultCellStyle, Palette.Raised);
            foreach (DataGridViewColumn column in grid.Columns)
            {
                CaptureStyle(column.DefaultCellStyle);
                ApplyStyle(column.DefaultCellStyle, Palette.Surface);
            }
        }

        private static void ApplyStyle(DataGridViewCellStyle style, Color background)
        {
            Snapshot(style).Applied = true;
            style.BackColor = background;
            style.ForeColor = Palette.Text;
            style.SelectionBackColor = Palette.Selection;
            style.SelectionForeColor = Palette.SelectionText;
        }

        private static void RestoreExtraColors(Control control)
        {
            if (control is DataGridView grid)
            {
                foreach (var style in new[] { grid.DefaultCellStyle, grid.RowsDefaultCellStyle,
                    grid.AlternatingRowsDefaultCellStyle, grid.ColumnHeadersDefaultCellStyle, grid.RowHeadersDefaultCellStyle })
                    Snapshot(style).Restore();
                foreach (DataGridViewColumn column in grid.Columns)
                    Snapshot(column.DefaultCellStyle).Restore();
            }
            RestoreSpecialControl(control);
        }

        private static void ApplyThemedForeColor(Control control)
        {
            var expected = ExpectedThemedForeColor(control);
            if (control is ButtonBase button && !IsThemeExcludedControl(button))
            {
                UpdateButtonForeground(button, expected);
                return;
            }

            control.ForeColor = expected;
        }

        private static Color ExpectedThemedForeColor(Control control)
        {
            if (!control.Enabled)
                return Palette.MutedText;

            if (control is ButtonBase button && !IsThemeExcludedControl(button))
                return ExpectedButtonForeground(button);

            return ReadableText(controls.GetValue(control, CreateState).Snapshot.OriginalForeColor);
        }

        private static Color ExpectedButtonForeground(ButtonBase button)
        {
            var original = controls.GetValue(button, CreateState).Snapshot.OriginalForeColor;
            var source = IsSemanticButtonColor(button.ForeColor) ? button.ForeColor : original;
            return ReadableText(source);
        }

        private static void UpdateButtonForeground(ButtonBase button, Color expected)
        {
            if (button.ForeColor.ToArgb() == expected.ToArgb())
                return;

            var state = controls.GetValue(button, CreateState);
            if (state.UpdatingButtonForeground)
                return;

            state.UpdatingButtonForeground = true;
            try
            {
                button.ForeColor = expected;
            }
            finally
            {
                state.UpdatingButtonForeground = false;
            }
        }

        private static bool IsSemanticButtonColor(Color color)
        {
            if (color.IsEmpty ||
                color.ToArgb() == Palette.Text.ToArgb() ||
                color.ToArgb() == Palette.MutedText.ToArgb() ||
                color.ToArgb() == SystemColors.ControlText.ToArgb() ||
                color.ToArgb() == SystemColors.WindowText.ToArgb() ||
                color.ToArgb() == SystemColors.GrayText.ToArgb() ||
                color.ToArgb() == Color.White.ToArgb() ||
                color.ToArgb() == Color.Black.ToArgb())
            {
                return false;
            }

            return ReadableText(color).ToArgb() != Palette.Text.ToArgb();
        }

        private static void ApplyItems(ToolStrip strip)
        {
            foreach (ToolStripItem item in strip.Items)
            {
                var state = Snapshot(item);
                if (IsThemed)
                {
                    state.CaptureAmbientColors(item);
                    state.Applied = true;
                    item.ForeColor = Palette.Text;
                    item.BackColor = Palette.Raised;
                }
                else
                    state.Restore();
                if (item is ToolStripDropDownItem dropDown && dropDown.HasDropDownItems)
                    Register(dropDown.DropDown);
                if (item is ToolStripControlHost host)
                    Register(host.Control);
            }
        }

        private static void CaptureItems(ToolStrip strip)
        {
            foreach (ToolStripItem item in strip.Items)
            {
                Snapshot(item).CaptureAmbientColors(item);
                if (item is ToolStripDropDownItem dropDown && dropDown.HasDropDownItems)
                    PrepareTree(dropDown.DropDown);
            }
        }

        public static Color ReadableText(Color original)
        {
            if (!IsThemed)
                return original;
            if (SystemInformation.HighContrast)
                return Palette.Text;
            var saturation = original.GetSaturation();
            var hue = original.GetHue();
            if (saturation > 0.2f && hue >= 20f && hue <= 65f)
                return Palette.WarningText;
            if (original.R > original.G * 1.3 && original.R > original.B * 1.3)
                return Palette.ErrorText;
            if (original.G > original.R * 1.3 && original.G > original.B * 1.1)
                return Palette.SuccessText;
            if (original.B > original.R * 1.3)
                return Palette.Accent;
            return Palette.Text;
        }
    }
}
