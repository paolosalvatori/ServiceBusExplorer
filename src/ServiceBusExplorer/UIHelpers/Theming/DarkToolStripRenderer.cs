using System.Drawing;
using System.Windows.Forms;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    internal sealed class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripRenderer() : base(new DarkColorTable())
        {
            RoundedEdges = false;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            var palette = ThemeManager.Palette;
            e.TextColor = !e.Item.Enabled ? palette.MutedText :
                e.Item.Selected ? palette.SelectionText : palette.Text;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = e.Item.Enabled ? ThemeManager.Palette.Text : ThemeManager.Palette.MutedText;
            base.OnRenderArrow(e);
        }

        private sealed class DarkColorTable : ProfessionalColorTable
        {
            public DarkColorTable() { UseSystemColors = false; }
            private ThemePalette Palette => ThemeManager.Palette;
            public override Color ToolStripGradientBegin => Palette.Raised;
            public override Color ToolStripGradientMiddle => Palette.Raised;
            public override Color ToolStripGradientEnd => Palette.Raised;
            public override Color MenuStripGradientBegin => Palette.Background;
            public override Color MenuStripGradientEnd => Palette.Background;
            public override Color StatusStripGradientBegin => Palette.Background;
            public override Color StatusStripGradientEnd => Palette.Background;
            public override Color ToolStripDropDownBackground => Palette.Raised;
            public override Color ImageMarginGradientBegin => Palette.Raised;
            public override Color ImageMarginGradientMiddle => Palette.Raised;
            public override Color ImageMarginGradientEnd => Palette.Raised;
            public override Color MenuItemSelected => Palette.Selection;
            public override Color MenuItemSelectedGradientBegin => Palette.Selection;
            public override Color MenuItemSelectedGradientEnd => Palette.Selection;
            public override Color MenuItemPressedGradientBegin => Palette.Selection;
            public override Color MenuItemPressedGradientMiddle => Palette.Selection;
            public override Color MenuItemPressedGradientEnd => Palette.Selection;
            public override Color MenuItemBorder => Palette.Border;
            public override Color MenuBorder => Palette.Border;
            public override Color ToolStripBorder => Palette.Border;
            public override Color SeparatorDark => Palette.Border;
            public override Color SeparatorLight => Palette.Raised;
            public override Color ButtonSelectedGradientBegin => Palette.Hover;
            public override Color ButtonSelectedGradientMiddle => Palette.Hover;
            public override Color ButtonSelectedGradientEnd => Palette.Hover;
            public override Color ButtonPressedGradientBegin => Palette.Selection;
            public override Color ButtonPressedGradientMiddle => Palette.Selection;
            public override Color ButtonPressedGradientEnd => Palette.Selection;
            public override Color ButtonCheckedGradientBegin => Palette.Selection;
            public override Color ButtonCheckedGradientMiddle => Palette.Selection;
            public override Color ButtonCheckedGradientEnd => Palette.Selection;
            public override Color CheckBackground => Palette.Selection;
            public override Color CheckSelectedBackground => Palette.Selection;
            public override Color CheckPressedBackground => Palette.Selection;
        }
    }
}
