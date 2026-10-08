using System;
using System.Drawing;
using System.Windows.Forms;
using ServiceBusExplorer.Controls;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    public static partial class ThemeManager
    {
        private static void AttachDrawing(Control control, ControlState state)
        {
            if (!IsThemeExcludedControl(control))
            {
                control.EnabledChanged += EnabledChanged;
                if (control is ButtonBase button)
                    button.ForeColorChanged += ButtonForeColorChanged;
            }
            switch (control)
            {
                case TabControl tabs:
                    tabs.DrawItem += DrawTab;
                    state.TabWindow = new ThemeNativeMethods.TabWindow(tabs);
                    break;
                case ComboBox combo:
                    combo.DrawItem += DrawCombo;
                    if (combo is PopupComboBox popup)
                        popup.DropDown += PopupOpening;
                    break;
                case TreeView tree:
                    tree.DrawNode += DrawNode;
                    tree.NodeMouseClick += TreeNodeMouseClick;
                    break;
                case ListView list:
                    list.DrawColumnHeader += DrawColumnHeader;
                    list.DrawItem += DrawListItem;
                    list.DrawSubItem += DrawListSubItem;
                    break;
                case DataGridView grid:
                    grid.CellFormatting += FormatCell;
                    grid.EditingControlShowing += EditingControlShowing;
                    grid.ColumnAdded += ColumnAdded;
                    break;
                case ToolStrip strip:
                    strip.ItemAdded += ItemAdded;
                    if (strip is ToolStripDropDown menu)
                        menu.Opening += MenuOpening;
                    break;
            }
        }

        private static void EnabledChanged(object sender, EventArgs e)
        {
            if (IsThemed)
                ApplyThemedForeColor((Control)sender);
        }

        private static void DrawTab(object sender, DrawItemEventArgs e)
        {
            if (!IsThemed)
                return;
            var tabs = (TabControl)sender;
            if (e.Index < 0 || e.Index >= tabs.TabCount)
                return;
            var page = tabs.TabPages[e.Index];
            var selected = tabs.SelectedIndex == e.Index;
            using (var brush = new SolidBrush(selected ? Palette.Raised : Palette.Background))
                e.Graphics.FillRectangle(brush, e.Bounds);
            var text = Rectangle.Inflate(e.Bounds, -2, -2);
            if (tabs.ImageList != null)
            {
                Image image = null;
                if (page.ImageIndex >= 0 && page.ImageIndex < tabs.ImageList.Images.Count)
                    image = tabs.ImageList.Images[page.ImageIndex];
                else if (!string.IsNullOrEmpty(page.ImageKey) && tabs.ImageList.Images.ContainsKey(page.ImageKey))
                    image = tabs.ImageList.Images[page.ImageKey];
                if (image != null)
                {
                    e.Graphics.DrawImage(image, text.Left, text.Top + (text.Height - image.Height) / 2);
                    text.X += image.Width + 4;
                    text.Width -= image.Width + 4;
                }
            }
            TextRenderer.DrawText(e.Graphics, page.Text, tabs.Font, text, Palette.Text,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
            if (selected)
            {
                using (var accent = new Pen(Palette.Accent, 2))
                    e.Graphics.DrawLine(accent, e.Bounds.Left + 2, e.Bounds.Bottom - 2, e.Bounds.Right - 2, e.Bounds.Bottom - 2);
                if (tabs.Focused)
                    ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(e.Bounds, -3, -4), Palette.Text, Palette.Raised);
            }
        }

        private static void DrawCombo(object sender, DrawItemEventArgs e)
        {
            if (!IsThemed)
                return;
            var combo = (ComboBox)sender;
            var selected = (e.State & DrawItemState.Selected) != 0;
            using (var brush = new SolidBrush(selected ? Palette.Selection : Palette.Surface))
                e.Graphics.FillRectangle(brush, e.Bounds);
            var text = e.Index >= 0 && e.Index < combo.Items.Count ? combo.GetItemText(combo.Items[e.Index]) : combo.Text;
            TextRenderer.DrawText(e.Graphics, text, e.Font, Rectangle.Inflate(e.Bounds, -2, 0),
                !combo.Enabled ? Palette.MutedText : selected ? Palette.SelectionText : Palette.Text,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            e.DrawFocusRectangle();
        }

        private static void PopupOpening(object sender, EventArgs e)
        {
            var popup = (PopupComboBox)sender;
            if (popup.DropDownControl != null)
                Register(popup.DropDownControl);
        }

        private static void DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (!IsThemed)
            {
                e.DrawDefault = true;
                return;
            }
            var tree = (TreeView)sender;
            var selected = (e.State & TreeNodeStates.Selected) != 0;
            var background = selected ? Palette.Selection : Palette.Surface;
            var foreground = selected ? Palette.SelectionText :
                e.Node.ForeColor.IsEmpty ? Palette.Text : ReadableTreeNodeText(e.Node.ForeColor, background);
            using (var brush = new SolidBrush(background))
                e.Graphics.FillRectangle(brush, e.Bounds);
            TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.NodeFont ?? tree.Font, e.Bounds, foreground,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);
            if ((e.State & TreeNodeStates.Focused) != 0 && tree.Focused)
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, foreground, background);
        }

        private static Color ReadableTreeNodeText(Color original, Color background)
        {
            if (!IsThemed || original.IsEmpty)
                return original;
            if (SystemInformation.HighContrast || original.IsSystemColor)
                return ReadableText(original);
            return EnsureContrastPreservingHue(original, background);
        }

        private static Color EnsureContrastPreservingHue(Color original, Color background)
        {
            if (ContrastRatio(original, background) >= 4.5)
                return original;

            for (var step = 1; step <= 8; step++)
            {
                var candidate = BlendTowards(original, Color.White, step / 8d);
                if (ContrastRatio(candidate, background) >= 4.5)
                    return candidate;
            }

            return Color.White;
        }

        private static Color BlendTowards(Color from, Color to, double amount)
        {
            return Color.FromArgb(
                255,
                (int)Math.Round(from.R + ((to.R - from.R) * amount)),
                (int)Math.Round(from.G + ((to.G - from.G) * amount)),
                (int)Math.Round(from.B + ((to.B - from.B) * amount)));
        }

        private static double ContrastRatio(Color first, Color second)
        {
            var a = RelativeLuminance(first);
            var b = RelativeLuminance(second);
            return (Math.Max(a, b) + 0.05) / (Math.Min(a, b) + 0.05);
        }

        private static double RelativeLuminance(Color color)
        {
            Func<byte, double> linear = channel =>
            {
                var value = channel / 255.0;
                return value <= 0.04045 ? value / 12.92 : Math.Pow((value + 0.055) / 1.055, 2.4);
            };

            return 0.2126 * linear(color.R) + 0.7152 * linear(color.G) + 0.0722 * linear(color.B);
        }

        private static void DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (!IsThemed)
                return;
            e.DrawDefault = false;
            using (var background = new SolidBrush(Palette.Raised))
            using (var border = new Pen(Palette.Border))
            {
                e.Graphics.FillRectangle(background, e.Bounds);
                e.Graphics.DrawLine(border, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
                e.Graphics.DrawLine(border, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom);
            }
            var alignment = e.Header.TextAlign == HorizontalAlignment.Right ? TextFormatFlags.Right :
                e.Header.TextAlign == HorizontalAlignment.Center ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Left;
            TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, Rectangle.Inflate(e.Bounds, -4, 0),
                Palette.Text, alignment | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private static void DrawListItem(object sender, DrawListViewItemEventArgs e)
        {
            if (IsThemed)
                e.DrawDefault = ((ListView)sender).View != View.Details;
        }

        private static void DrawListSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (!IsThemed)
                return;
            e.DrawDefault = false;
            var selected = e.Item.Selected;
            using (var brush = new SolidBrush(selected ? Palette.Selection : Palette.Surface))
                e.Graphics.FillRectangle(brush, e.Bounds);
            var list = (ListView)sender;
            var bounds = Rectangle.Inflate(e.Bounds, -4, 0);
            if (e.ColumnIndex == 0 && list.CheckBoxes)
            {
                var box = new Rectangle(bounds.Left, bounds.Top + (bounds.Height - 13) / 2, 13, 13);
                using (var background = new SolidBrush(Palette.Surface))
                using (var border = new Pen(Palette.Border))
                using (var check = new Pen(Palette.Accent, 2))
                {
                    e.Graphics.FillRectangle(background, box);
                    e.Graphics.DrawRectangle(border, box);
                    if (e.Item.Checked)
                        e.Graphics.DrawLines(check, new[] { new Point(box.X + 2, box.Y + 6),
                            new Point(box.X + 5, box.Y + 9), new Point(box.X + 11, box.Y + 3) });
                }
                bounds.X += 18;
                bounds.Width -= 18;
            }
            if (e.ColumnIndex == 0 && list.SmallImageList != null)
            {
                Image image = null;
                if (e.Item.ImageIndex >= 0 && e.Item.ImageIndex < list.SmallImageList.Images.Count)
                    image = list.SmallImageList.Images[e.Item.ImageIndex];
                else if (!string.IsNullOrEmpty(e.Item.ImageKey) && list.SmallImageList.Images.ContainsKey(e.Item.ImageKey))
                    image = list.SmallImageList.Images[e.Item.ImageKey];
                if (image != null)
                {
                    e.Graphics.DrawImage(image, bounds.Left, bounds.Top + (bounds.Height - image.Height) / 2);
                    bounds.X += image.Width + 4;
                    bounds.Width -= image.Width + 4;
                }
            }
            var alignment = e.Header.TextAlign == HorizontalAlignment.Right ? TextFormatFlags.Right :
                e.Header.TextAlign == HorizontalAlignment.Center ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Left;
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font, bounds,
                selected ? Palette.SelectionText :
                e.SubItem.ForeColor == SystemColors.GrayText ? Palette.MutedText : ReadableText(e.SubItem.ForeColor),
                alignment | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        private static void FormatCell(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!IsThemed)
                return;
            var grid = (DataGridView)sender;
            var color = e.CellStyle.BackColor;
            var semantic = ClassifySemanticColor(color);
            if (semantic == SemanticColor.Error)
                e.CellStyle.BackColor = Palette.ErrorBackground;
            else if (semantic == SemanticColor.Warning)
                e.CellStyle.BackColor = Palette.WarningBackground;
            else if (color.GetBrightness() > 0.5)
                e.CellStyle.BackColor = e.RowIndex % 2 == 0 ? Palette.Surface : Palette.Background;
            e.CellStyle.ForeColor = ReadableText(e.CellStyle.ForeColor);
            e.CellStyle.SelectionBackColor = Palette.Selection;
            e.CellStyle.SelectionForeColor = Palette.SelectionText;
        }

        private static void EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!IsThemeExcludedControl(e.Control))
                Apply(e.Control);
        }

        private static void ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (IsThemed)
            {
                CaptureStyle(e.Column.DefaultCellStyle);
                ApplyStyle(e.Column.DefaultCellStyle, Palette.Surface);
            }
        }

        private static void ItemAdded(object sender, ToolStripItemEventArgs e) => ApplyItems((ToolStrip)sender);
        private static void MenuOpening(object sender, System.ComponentModel.CancelEventArgs e) => Apply((Control)sender);

        private static void ButtonForeColorChanged(object sender, EventArgs e)
        {
            if (!IsThemed || IsThemeExcludedControl((Control)sender))
                return;

            var button = (ButtonBase)sender;
            UpdateButtonForeground(button, ExpectedThemedForeColor(button));
        }

        private static void TreeNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Node?.ContextMenuStrip != null)
                Register(e.Node.ContextMenuStrip);
        }
    }
}
