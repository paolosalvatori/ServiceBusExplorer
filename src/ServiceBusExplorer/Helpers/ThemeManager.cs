// ThemeManager.cs
// Path: src/ServiceBusExplorer/Helpers/ThemeManager.cs
//
// Manages Dark/Light theme with persistence in user's app.config.
// Usage:
//   ThemeManager.Apply(form)          — applies current theme to the form
//   ThemeManager.IsDark               — true if dark theme active
//   ThemeManager.CurrentTheme         — "Dark" or "Light"

using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceBusExplorer.Helpers
{
    public enum Theme
    {
        Light,
        Dark
    }

    public static class ThemeManager
    {
        // Key in app.config
        const string ConfigKey = "theme";

        // Current state
        static Theme CurrentTheme { get; set; } = Theme.Dark;

        public static bool IsDark => CurrentTheme == Theme.Dark;

        // Event fired when theme changes
        public static event EventHandler ThemeChanged;

        // Dark palette
        static class Dark
        {
            public static readonly Color Background = Color.FromArgb(33, 33, 33);
            public static readonly Color Surface = Color.FromArgb(42, 42, 42);
            public static readonly Color SurfaceLight = Color.FromArgb(51, 51, 51);
            public static readonly Color SurfaceLighter = Color.FromArgb(64, 64, 64);
            public static readonly Color Border = Color.FromArgb(72, 72, 72);
            public static readonly Color Foreground = Color.FromArgb(236, 236, 236);
            public static readonly Color ForegroundDim = Color.FromArgb(160, 160, 160);
            public static readonly Color Accent = Color.FromArgb(0, 122, 204);
            public static readonly Color AccentHover = Color.FromArgb(28, 151, 234);
            public static readonly Color AccentText = Color.White;
        }

        // Light palette (Windows system colors)
        static class Light
        {
            // Original Service Bus Explorer light palette
            public static readonly Color Background = Color.FromArgb(235, 241, 247);
            public static readonly Color Surface = Color.FromArgb(215, 228, 242);
            public static readonly Color SurfaceLight = Color.FromArgb(255, 255, 255);
            public static readonly Color SurfaceLighter = Color.FromArgb(194, 213, 229);
            public static readonly Color Border = Color.FromArgb(153, 180, 209);
            public static readonly Color Foreground = SystemColors.ControlText;
            public static readonly Color ForegroundDim = Color.FromArgb(26, 59, 105);
            public static readonly Color Accent = Color.FromArgb(153, 180, 209);
            public static readonly Color AccentHover = Color.FromArgb(194, 213, 229);
            public static readonly Color AccentText = Color.White;
            public static readonly Color HeaderStart = Color.FromArgb(215, 228, 242);
            public static readonly Color HeaderEnd = Color.FromArgb(153, 180, 209);
            public static readonly Color HeaderText = Color.White;
        }

        // Shortcuts to active palette
        public static Color Background => IsDark ? Dark.Background : Light.Background;
        public static Color Surface => IsDark ? Dark.Surface : Light.Surface;
        public static Color SurfaceLight => IsDark ? Dark.SurfaceLight : Light.SurfaceLight;
        public static Color SurfaceLighter => IsDark ? Dark.SurfaceLighter : Light.SurfaceLighter;
        public static Color Border => IsDark ? Dark.Border : Light.Border;
        public static Color Foreground => IsDark ? Dark.Foreground : Light.Foreground;
        public static Color ForegroundDim => IsDark ? Dark.ForegroundDim : Light.ForegroundDim;
        public static Color Accent => IsDark ? Dark.Accent : Light.Accent;
        public static Color AccentHover => IsDark ? Dark.AccentHover : Light.AccentHover;
        public static Color AccentText => IsDark ? Dark.AccentText : Light.AccentText;

        // Initialize: load saved preference
        static ThemeManager()
        {
            LoadFromConfig();
        }

        static void LoadFromConfig()
        {
            try
            {
                var val = ConfigurationManager.AppSettings[ConfigKey];
                if (!string.IsNullOrEmpty(val) &&
                    Enum.TryParse<Theme>(val, ignoreCase: true, out var saved))
                {
                    CurrentTheme = saved;
                }
            }
            catch
            {
                /* no config, uses Dark as default */
            }
        }

        static void SaveToConfig(Theme theme)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.PerUserRoamingAndLocal);
                config.AppSettings.Settings.Remove(ConfigKey);
                config.AppSettings.Settings.Add(ConfigKey, theme.ToString());
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch
            {
                /* silence persistence errors */
            }
        }

        // Public API

        /// <summary>Applies current theme to a Form and all its controls.</summary>
        /// <summary>Accepts Form, UserControl or any Control.</summary>
        public static void Apply(Control control)
        {
            ApplyToControl(control);
            ApplyRecursive(control);

            control.ControlAdded -= OnControlAdded;
            control.ControlAdded += OnControlAdded;

            // For Forms: reapply on Load to override any colors
            // that the Designer might have set after the constructor
            if (control is Form form)
            {
                form.Load -= OnFormLoad;
                form.Load += OnFormLoad;
            }
        }

        static void OnControlAdded(object sender, ControlEventArgs e)
        {
            ApplyRecursive(e.Control);
        }

        static void OnFormLoad(object sender, EventArgs e)
        {
            if (sender is Form form)
            {
                ApplyToControl(form);
                ApplyRecursive(form);
            }
        }

        /// <summary>Sets a specific theme.</summary>
        public static void SetTheme(Theme theme)
        {
            if (CurrentTheme == theme) return;
            CurrentTheme = theme;
            SaveToConfig(CurrentTheme);

            foreach (Form f in Application.OpenForms)
            {
                ApplyToControl(f);
                ApplyRecursive(f);
                f.Refresh();
            }

            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        // Recursive application

        // Their original light theme colors — any control with these colors is forced to dark
        static readonly Color[] LightThemeColors =
        {
            Color.FromArgb(215, 228, 242), // main light blue
            Color.FromArgb(153, 180, 209), // medium blue hover
            Color.FromArgb(235, 241, 247), // bluish off-white
            Color.FromArgb(194, 213, 229), // secondary light blue
        };

        static bool IsLightThemeColor(Color c)
        {
            foreach (var lc in LightThemeColors)
                if (Math.Abs(c.R - lc.R) < 15 && Math.Abs(c.G - lc.G) < 15 && Math.Abs(c.B - lc.B) < 15)
                    return true;
            return false;
        }

        static void ApplyRecursive(Control control)
        {
            ApplyToControl(control);

            // Force override of any residual light theme colors (dark mode)
            if (IsDark && IsLightThemeColor(control.BackColor))
                control.BackColor = Background;
            if (IsDark && IsLightThemeColor(control.ForeColor))
                control.ForeColor = Foreground;

            // Force override of white/Window ForeColor in light theme (invisible)
            if (!IsDark && (control.ForeColor == Color.White ||
                            control.ForeColor == SystemColors.Window))
                control.ForeColor = Foreground;

            // Force override of pure white or Window BackColor in dark
            if (IsDark && (control.BackColor == Color.White ||
                           control.BackColor == SystemColors.Window))
                control.BackColor = Surface;

            // logoPictureBox: white logo — Microsoft Azure blue background in both themes
            if (control.Name == "logoPictureBox")
            {
                control.BackColor = Color.FromArgb(0, 120, 212); // #0078D4 Microsoft Azure blue
                return;
            }

            // Grouper: custom control with its own color properties
            ApplyToGrouper(control);
            // HeaderPanel: custom gradient header
            ApplyToHeaderPanel(control);

            foreach (Control child in control.Controls)
                ApplyRecursive(child);

            if (control is ToolStrip ts) ApplyToToolStrip(ts);
            if (control is DataGridView dgv) ApplyToDataGridView(dgv);
            if (control is TabControl tc) ApplyToTabControl(tc);
        }

        static void ApplyToControl(Control control)
        {
            // AboutForm: has decorative BackgroundImage — preserve original appearance
            if (control.BackgroundImage != null && control is Form)
                return;

            // Labels with BackColor=Transparent inside a form with BackgroundImage
            // inherit the background — don't change to avoid breaking visual layout
            if (control.BackColor == Color.Transparent)
            {
                var parent = control.Parent;
                while (parent != null)
                {
                    if (parent.BackgroundImage != null) return;
                    parent = parent.Parent;
                }
            }

            control.BackColor = Background;
            control.ForeColor = Foreground;

            switch (control)
            {
                case RichTextBox rtb:
                    rtb.BackColor = SurfaceLight;
                    rtb.ForeColor = Foreground;
                    rtb.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case TextBox tb:
                    tb.BackColor = SurfaceLight;
                    tb.ForeColor = Foreground;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case ComboBox cb:
                    cb.BackColor = SurfaceLight;
                    cb.ForeColor = Foreground;
                    cb.FlatStyle = IsDark ? FlatStyle.Flat : FlatStyle.Standard;
                    break;

                case CheckBox chk:
                    chk.BackColor = Color.Transparent;
                    chk.ForeColor = Foreground;
                    break;

                case RadioButton rb:
                    rb.BackColor = Color.Transparent;
                    rb.ForeColor = Foreground;
                    break;

                case Button btn:
                    if (IsDark)
                    {
                        btn.BackColor = Accent;
                        btn.ForeColor = AccentText;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderColor = Accent;
                        btn.FlatAppearance.MouseOverBackColor = AccentHover;
                        btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 100, 180);
                    }
                    else
                    {
                        btn.BackColor = SystemColors.ButtonFace;
                        btn.ForeColor = SystemColors.ControlText;
                        btn.FlatStyle = FlatStyle.Standard;
                        btn.UseVisualStyleBackColor = true;
                    }

                    btn.Cursor = Cursors.Hand;
                    break;

                case ListBox lb:
                    lb.BackColor = SurfaceLight;
                    lb.ForeColor = Foreground;
                    break;

                case ListView lv:
                    lv.BackColor = SurfaceLight;
                    lv.ForeColor = Foreground;
                    lv.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case TreeView tv:
                    tv.BackColor = SurfaceLight;
                    tv.ForeColor = Foreground;
                    tv.LineColor = Border;
                    tv.BorderStyle = BorderStyle.FixedSingle;
                    break;

                // TabPage before Panel (inheritance)
                case TabPage tp:
                    tp.BackColor = Surface;
                    tp.ForeColor = Foreground;
                    break;

                case GroupBox gb:
                    gb.BackColor = Surface;
                    gb.ForeColor = ForegroundDim;
                    break;

                case Panel pnl:
                    pnl.BackColor = Surface;
                    break;

                case SplitContainer sc:
                    sc.BackColor = Border;
                    sc.Panel1.BackColor = Surface;
                    sc.Panel2.BackColor = Surface;
                    break;

                case NumericUpDown nud:
                    nud.BackColor = SurfaceLight;
                    nud.ForeColor = Foreground;
                    break;

                case DateTimePicker dtp:
                    dtp.BackColor = SurfaceLight;
                    dtp.ForeColor = Foreground;
                    dtp.CalendarMonthBackground = SurfaceLight;
                    dtp.CalendarForeColor = Foreground;
                    dtp.CalendarTitleBackColor = Accent;
                    dtp.CalendarTitleForeColor = AccentText;
                    break;

                // LinkLabel before Label (inheritance)
                case LinkLabel llbl:
                    llbl.BackColor = Color.Transparent;
                    llbl.ForeColor = Accent;
                    llbl.LinkColor = Accent;
                    llbl.ActiveLinkColor = AccentHover;
                    llbl.VisitedLinkColor = Accent;
                    llbl.LinkBehavior = LinkBehavior.HoverUnderline;
                    break;

                case Label lbl:
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = Foreground;
                    break;

                case ToolStripContainer tsc:
                    tsc.BackColor = Surface;
                    tsc.TopToolStripPanel.BackColor = Surface;
                    tsc.BottomToolStripPanel.BackColor = Surface;
                    tsc.LeftToolStripPanel.BackColor = Surface;
                    tsc.RightToolStripPanel.BackColor = Surface;
                    tsc.ContentPanel.BackColor = Background;
                    break;

                // ContextMenuStrip > MenuStrip > StatusStrip > ToolStrip (inheritance)
                case ContextMenuStrip cms:
                    cms.BackColor = SurfaceLight;
                    cms.ForeColor = Foreground;
                    cms.RenderMode = ToolStripRenderMode.Professional;
                    cms.Renderer = IsDark ? (ToolStripRenderer)new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    ApplyToMenuItems(cms.Items);
                    break;

                case MenuStrip ms:
                    ms.BackColor = Surface;
                    ms.ForeColor = Foreground;
                    ms.RenderMode = ToolStripRenderMode.Professional;
                    ms.Renderer = IsDark ? (ToolStripRenderer)new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    ApplyToMenuItems(ms.Items);
                    break;

                case StatusStrip ss:
                    ss.BackColor = Surface;
                    ss.ForeColor = ForegroundDim;
                    ss.SizingGrip = false;
                    ss.RenderMode = ToolStripRenderMode.Professional;
                    ss.Renderer = IsDark ? (ToolStripRenderer)new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    foreach (ToolStripItem item in ss.Items)
                    {
                        item.BackColor = Surface;
                        item.ForeColor = ForegroundDim;
                    }

                    break;

                case PropertyGrid pg:
                    pg.BackColor = Background;
                    pg.ViewBackColor = SurfaceLight;
                    pg.ViewForeColor = Foreground;
                    pg.LineColor = Border;
                    pg.CategoryForeColor = ForegroundDim;
                    pg.HelpBackColor = Surface;
                    pg.HelpForeColor = Foreground;
                    pg.CommandsBackColor = Surface;
                    pg.CommandsForeColor = Foreground;
                    break;
            }
        }

        static void ApplyToHeaderPanel(Control control)
        {
            var t = control.GetType();
            if (t.Name != "HeaderPanel") return;

            var bg = IsDark ? Surface : Light.HeaderStart;
            var bg2 = IsDark ? Background : Light.HeaderEnd;
            var fg = IsDark ? Foreground : Light.HeaderText;

            TrySetColor(t, control, "HeaderColor1", bg);
            TrySetColor(t, control, "HeaderColor2", bg2);

            control.ForeColor = fg;
            control.BackColor = IsDark ? bg2 : Light.SurfaceLight;
            control.Invalidate(true);
        }

        static void ApplyToGrouper(Control control)
        {
            // Grouper has BackgroundColor, BorderColor, CustomGroupBoxColor
            // We use reflection to avoid circular namespace dependency
            var t = control.GetType();
            if (t.Name != "Grouper") return;

            var bg = IsDark ? Background : Light_Background;
            var fg = IsDark ? Foreground : Light_Foreground;
            var bord = IsDark ? Border : Light_Border;

            TrySetColor(t, control, "BackgroundColor", bg);
            TrySetColor(t, control, "BackgroundGradientColor", bg);
            TrySetColor(t, control, "CustomGroupBoxColor", bg);
            TrySetColor(t, control, "BorderColor", bord);
            TrySetColor(t, control, "GroupTitleColor", fg);

            // Grouper title ForeColor
            control.ForeColor = fg;
            control.BackColor = bg;

            // Force repaint — Grouper uses custom OnPaint
            control.Invalidate(true);
        }

        static void TrySetColor(Type t, object obj, string propName, Color color)
        {
            try
            {
                var prop = t.GetProperty(propName);
                if (prop != null && prop.CanWrite)
                    prop.SetValue(obj, color);
            }
            catch
            {
                // ignored
            }
        }

        // Light colors for Grouper
        static Color Light_Background => Light.Background;
        static Color Light_Foreground => Light.Foreground;
        static Color Light_Border => Light.Border;

        static void ApplyToToolStrip(ToolStrip ts)
        {
            ts.BackColor = Surface;
            ts.ForeColor = Foreground;
            ts.RenderMode = ToolStripRenderMode.Professional;
            ts.Renderer = IsDark ? (ToolStripRenderer)new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
            foreach (ToolStripItem item in ts.Items)
            {
                item.BackColor = Surface;
                item.ForeColor = Foreground;
            }
        }

        static void ApplyToDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Background;
            dgv.GridColor = Border;
            dgv.BorderStyle = BorderStyle.None;
            dgv.DefaultCellStyle.BackColor = SurfaceLight;
            dgv.DefaultCellStyle.ForeColor = Foreground;
            dgv.DefaultCellStyle.SelectionBackColor = Accent;
            dgv.DefaultCellStyle.SelectionForeColor = AccentText;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Surface;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Foreground;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Surface;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ForegroundDim;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Surface;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.RowHeadersDefaultCellStyle.BackColor = Surface;
            dgv.RowHeadersDefaultCellStyle.ForeColor = ForegroundDim;
            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Accent;
            dgv.EnableHeadersVisualStyles = false;
        }

        static void ApplyToTabControl(TabControl tc)
        {
            tc.BackColor = Background;
        }

        static void ApplyToMenuItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.BackColor = SurfaceLight;
                item.ForeColor = Foreground;
                if (item is ToolStripMenuItem mi)
                    ApplyToMenuItems(mi.DropDownItems);
            }
        }

        // DWM: native dark title bar (Windows 10 1903+)
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr,
            ref int attrValue, int attrSize);

        const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public static void ApplyTitleBar(IntPtr handle)
        {
            try
            {
                var value = IsDark ? 1 : 0;
                DwmSetWindowAttribute(handle, DWMWA_USE_IMMERSIVE_DARK_MODE,
                    ref value, sizeof(int));
            }
            catch
            {
                /* Windows < 10 1903 */
            }
        }
    }

    // Dark renderer for ToolStrip / MenuStrip
    class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripRenderer() : base(new DarkColorTable())
        {
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var color = e.Item.Selected
                ? ThemeManager.SurfaceLighter
                : ThemeManager.SurfaceLight;
            using var brush = new SolidBrush(color);
            e.Graphics.FillRectangle(brush, e.Item.ContentRectangle);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using var brush = new SolidBrush(ThemeManager.Surface);
            e.Graphics.FillRectangle(brush, e.AffectedBounds);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                var color = e.Item.Pressed ? ThemeManager.Accent : ThemeManager.SurfaceLighter;
                using var brush = new SolidBrush(color);
                e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, e.Item.Size));
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            using var pen = new Pen(ThemeManager.Border);
            var r = e.Item.ContentRectangle;
            if (e.Vertical)
                e.Graphics.DrawLine(pen, r.Left + r.Width / 2, r.Top, r.Left + r.Width / 2, r.Bottom);
            else
                e.Graphics.DrawLine(pen, r.Left, r.Top + r.Height / 2, r.Right, r.Top + r.Height / 2);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Enabled ? ThemeManager.Foreground : ThemeManager.ForegroundDim;
            base.OnRenderItemText(e);
        }
    }

    class DarkColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder => ThemeManager.Border;
        public override Color MenuItemSelected => ThemeManager.SurfaceLighter;
        public override Color MenuItemSelectedGradientBegin => ThemeManager.SurfaceLighter;
        public override Color MenuItemSelectedGradientEnd => ThemeManager.SurfaceLighter;
        public override Color MenuItemPressedGradientBegin => ThemeManager.Accent;
        public override Color MenuItemPressedGradientEnd => ThemeManager.Accent;
        public override Color MenuStripGradientBegin => ThemeManager.Surface;
        public override Color MenuStripGradientEnd => ThemeManager.Surface;
        public override Color ToolStripDropDownBackground => ThemeManager.SurfaceLight;
        public override Color ImageMarginGradientBegin => ThemeManager.Surface;
        public override Color ImageMarginGradientMiddle => ThemeManager.Surface;
        public override Color ImageMarginGradientEnd => ThemeManager.Surface;
        public override Color ToolStripBorder => ThemeManager.Border;
        public override Color SeparatorLight => ThemeManager.Border;
        public override Color SeparatorDark => ThemeManager.SurfaceLight;
        public override Color ButtonSelectedBorder => ThemeManager.Accent;
        public override Color ButtonSelectedGradientBegin => ThemeManager.SurfaceLighter;
        public override Color ButtonSelectedGradientEnd => ThemeManager.SurfaceLighter;
        public override Color ButtonCheckedGradientBegin => ThemeManager.Accent;
        public override Color ButtonCheckedGradientEnd => ThemeManager.Accent;
        public override Color ToolStripGradientBegin => ThemeManager.Surface;
        public override Color ToolStripGradientMiddle => ThemeManager.Surface;
        public override Color ToolStripGradientEnd => ThemeManager.Surface;
        public override Color ToolStripContentPanelGradientBegin => ThemeManager.Background;
        public override Color ToolStripContentPanelGradientEnd => ThemeManager.Background;
        public override Color StatusStripGradientBegin => ThemeManager.Surface;
        public override Color StatusStripGradientEnd => ThemeManager.Surface;
        public override Color CheckBackground => ThemeManager.Accent;
        public override Color CheckPressedBackground => ThemeManager.AccentHover;
        public override Color CheckSelectedBackground => ThemeManager.Accent;
        public override Color OverflowButtonGradientBegin => ThemeManager.Surface;
        public override Color OverflowButtonGradientMiddle => ThemeManager.Surface;
        public override Color OverflowButtonGradientEnd => ThemeManager.Surface;
        public override Color GripLight => ThemeManager.Border;
        public override Color GripDark => ThemeManager.Surface;
    }
}
