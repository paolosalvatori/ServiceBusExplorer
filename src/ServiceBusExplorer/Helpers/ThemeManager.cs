// ThemeManager.cs
// Caminho: src/ServiceBusExplorer/Helpers/ThemeManager.cs
//
// Gerencia tema Dark/Light com persistência no app.config do usuário.
// Uso:
//   ThemeManager.Apply(form)          — aplica o tema atual ao form
//   ThemeManager.Toggle(form)         — alterna dark/light e reaaplica
//   ThemeManager.IsDark               — true se tema escuro ativo
//   ThemeManager.CurrentTheme         — "Dark" ou "Light"

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceBusExplorer.Helpers
{
    public enum Theme { Light, Dark }

    public static class ThemeManager
    {
        // ── Chave no app.config ───────────────────────────────────────────────
        private const string ConfigKey = "theme";

        // ── Estado atual ──────────────────────────────────────────────────────
        private static Theme _current = Theme.Dark;

        public static Theme CurrentTheme => _current;
        public static bool IsDark => _current == Theme.Dark;

        // ── Evento disparado ao trocar tema ───────────────────────────────────
        public static event EventHandler ThemeChanged;

        // ── Paleta Dark ───────────────────────────────────────────────────────
        private static class Dark
        {
            public static readonly Color Background     = Color.FromArgb(33,  33,  33);
            public static readonly Color Surface        = Color.FromArgb(42,  42,  42);
            public static readonly Color SurfaceLight   = Color.FromArgb(51,  51,  51);
            public static readonly Color SurfaceLighter = Color.FromArgb(64,  64,  64);
            public static readonly Color Border         = Color.FromArgb(72,  72,  72);
            public static readonly Color Foreground     = Color.FromArgb(236, 236, 236);
            public static readonly Color ForegroundDim  = Color.FromArgb(160, 160, 160);
            public static readonly Color Accent         = Color.FromArgb(0,   122, 204);
            public static readonly Color AccentHover    = Color.FromArgb(28,  151, 234);
            public static readonly Color AccentText     = Color.White;
        }

        // ── Paleta Light (cores do sistema Windows) ───────────────────────────
        private static class Light
        {
            public static readonly Color Background     = SystemColors.Control;
            public static readonly Color Surface        = SystemColors.Control;
            public static readonly Color SurfaceLight   = SystemColors.Window;
            public static readonly Color SurfaceLighter = SystemColors.ControlLight;
            public static readonly Color Border         = SystemColors.ControlDark;
            public static readonly Color Foreground     = SystemColors.ControlText;
            public static readonly Color ForegroundDim  = SystemColors.GrayText;
            public static readonly Color Accent         = SystemColors.Highlight;
            public static readonly Color AccentHover    = SystemColors.HotTrack;
            public static readonly Color AccentText     = SystemColors.HighlightText;
        }

        // ── Atalhos para paleta ativa ─────────────────────────────────────────
        public static Color Background     => IsDark ? Dark.Background     : Light.Background;
        public static Color Surface        => IsDark ? Dark.Surface        : Light.Surface;
        public static Color SurfaceLight   => IsDark ? Dark.SurfaceLight   : Light.SurfaceLight;
        public static Color SurfaceLighter => IsDark ? Dark.SurfaceLighter : Light.SurfaceLighter;
        public static Color Border         => IsDark ? Dark.Border         : Light.Border;
        public static Color Foreground     => IsDark ? Dark.Foreground     : Light.Foreground;
        public static Color ForegroundDim  => IsDark ? Dark.ForegroundDim  : Light.ForegroundDim;
        public static Color Accent         => IsDark ? Dark.Accent         : Light.Accent;
        public static Color AccentHover    => IsDark ? Dark.AccentHover    : Light.AccentHover;
        public static Color AccentText     => IsDark ? Dark.AccentText     : Light.AccentText;

        // ── Inicializar: carregar preferência salva ───────────────────────────
        static ThemeManager()
        {
            LoadFromConfig();
        }

        private static void LoadFromConfig()
        {
            try
            {
                var val = ConfigurationManager.AppSettings[ConfigKey];
                if (!string.IsNullOrEmpty(val) &&
                    Enum.TryParse<Theme>(val, ignoreCase: true, out var saved))
                {
                    _current = saved;
                }
            }
            catch { /* sem config, usa Dark como padrão */ }
        }

        private static void SaveToConfig(Theme theme)
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
            catch { /* silenciar erros de persistência */ }
        }

        // ── API pública ───────────────────────────────────────────────────────

        /// <summary>Aplica o tema atual a um Form e todos os seus controles.</summary>
        /// <summary>Aceita Form, UserControl ou qualquer Control.</summary>
        public static void Apply(Control control)
        {
            ApplyToControl(control);
            ApplyRecursive(control);
            control.ControlAdded += (s, e) => ApplyRecursive(e.Control);

            // Para Forms: reaplicar no Load para sobrescrever qualquer cor
            // que o Designer possa ter definido depois do construtor
            if (control is Form form)
            {
                form.Load += (s, e) =>
                {
                    ApplyToControl(form);
                    ApplyRecursive(form);
                };
            }
        }

        /// <summary>Alterna entre Dark e Light, reaaplica a todos os forms abertos e salva.</summary>
        public static void Toggle()
        {
            _current = IsDark ? Theme.Light : Theme.Dark;
            SaveToConfig(_current);

            foreach (Form f in Application.OpenForms)
            {
                ApplyToControl(f);
                ApplyRecursive(f);
                f.Refresh();
            }

            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>Define um tema específico.</summary>
        public static void SetTheme(Theme theme)
        {
            if (_current == theme) return;
            _current = theme;
            SaveToConfig(_current);

            foreach (Form f in Application.OpenForms)
            {
                ApplyToControl(f);
                ApplyRecursive(f);
                f.Refresh();
            }

            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        // ── Aplicação recursiva ───────────────────────────────────────────────

        // Cores do tema light original deles — qualquer controle com essas cores e forcado para dark
        private static readonly System.Drawing.Color[] LightThemeColors = {
            System.Drawing.Color.FromArgb(215, 228, 242),  // azul claro principal
            System.Drawing.Color.FromArgb(153, 180, 209),  // azul medio hover
            System.Drawing.Color.FromArgb(235, 241, 247),  // quase branco azulado
            System.Drawing.Color.FromArgb(194, 213, 229),  // azul claro secundario
        };

        private static bool IsLightThemeColor(System.Drawing.Color c)
        {
            foreach (var lc in LightThemeColors)
                if (Math.Abs(c.R - lc.R) < 15 && Math.Abs(c.G - lc.G) < 15 && Math.Abs(c.B - lc.B) < 15)
                    return true;
            return false;
        }

        private static void ApplyRecursive(Control control)
        {
            ApplyToControl(control);

            // Forcar override de qualquer cor residual do tema light (dark mode)
            if (IsDark && IsLightThemeColor(control.BackColor))
                control.BackColor = Background;
            if (IsDark && IsLightThemeColor(control.ForeColor))
                control.ForeColor = Foreground;

            // Forcar override de ForeColor branco/Window no tema claro (invisivel)
            if (!IsDark && (control.ForeColor == Color.White ||
                            control.ForeColor == SystemColors.Window))
                control.ForeColor = Foreground;

            // Forcar override de BackColor branco puro ou Window no dark
            if (IsDark && (control.BackColor == Color.White ||
                           control.BackColor == SystemColors.Window))
                control.BackColor = Surface;

            // logoPictureBox: logo branca — fundo azul Microsoft Azure em ambos os temas
            if (control.Name == "logoPictureBox")
            {
                control.BackColor = Color.FromArgb(0, 120, 212); // #0078D4 Microsoft Azure blue
                return;
            }

            // Grouper: controle customizado com propriedades de cor proprias
            ApplyToGrouper(control);
            // HeaderPanel: cabecalho gradiente customizado
            ApplyToHeaderPanel(control);

            foreach (Control child in control.Controls)
                ApplyRecursive(child);

            if (control is ToolStrip ts)   ApplyToToolStrip(ts);
            if (control is DataGridView dgv) ApplyToDataGridView(dgv);
            if (control is TabControl tc)  ApplyToTabControl(tc);
        }

        private static void ApplyToControl(Control control)
        {
            // AboutForm: tem BackgroundImage decorativa — preservar aparencia original
            if (control.BackgroundImage != null && control is Form)
                return;

            // Labels com BackColor=Transparent dentro de form com BackgroundImage
            // herdam o fundo — nao alterar para nao destruir o layout visual
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
                    tv.LineColor  = Border;
                    tv.BorderStyle = BorderStyle.FixedSingle;
                    break;

                // TabPage antes de Panel (herança)
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

                // LinkLabel antes de Label (herança)
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
                    tsc.TopToolStripPanel.BackColor    = Surface;
                    tsc.BottomToolStripPanel.BackColor = Surface;
                    tsc.LeftToolStripPanel.BackColor   = Surface;
                    tsc.RightToolStripPanel.BackColor  = Surface;
                    tsc.ContentPanel.BackColor         = Background;
                    break;

                // ContextMenuStrip > MenuStrip > StatusStrip > ToolStrip (herança)
                case ContextMenuStrip cms:
                    cms.BackColor  = SurfaceLight;
                    cms.ForeColor  = Foreground;
                    cms.RenderMode = ToolStripRenderMode.Professional;
                    cms.Renderer   = IsDark ? (ToolStripRenderer)new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    ApplyToMenuItems(cms.Items);
                    break;

                case MenuStrip ms:
                    ms.BackColor  = Surface;
                    ms.ForeColor  = Foreground;
                    ms.RenderMode = ToolStripRenderMode.Professional;
                    ms.Renderer   = IsDark ? (ToolStripRenderer)new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    ApplyToMenuItems(ms.Items);
                    break;

                case StatusStrip ss:
                    ss.BackColor  = Surface;
                    ss.ForeColor  = ForegroundDim;
                    ss.SizingGrip = false;
                    ss.RenderMode = ToolStripRenderMode.Professional;
                    ss.Renderer   = IsDark ? (ToolStripRenderer)new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
                    foreach (ToolStripItem item in ss.Items)
                    { item.BackColor = Surface; item.ForeColor = ForegroundDim; }
                    break;

                case PropertyGrid pg:
                    pg.BackColor         = Background;
                    pg.ViewBackColor     = SurfaceLight;
                    pg.ViewForeColor     = Foreground;
                    pg.LineColor         = Border;
                    pg.CategoryForeColor = ForegroundDim;
                    pg.HelpBackColor     = Surface;
                    pg.HelpForeColor     = Foreground;
                    pg.CommandsBackColor = Surface;
                    pg.CommandsForeColor = Foreground;
                    break;
            }
        }

        private static void ApplyToHeaderPanel(Control control)
        {
            var t = control.GetType();
            if (t.Name != "HeaderPanel") return;

            var bg  = IsDark ? Surface    : SystemColors.ControlLight;
            var bg2 = IsDark ? Background : SystemColors.Control;
            var fg  = IsDark ? Foreground : SystemColors.ControlText;

            TrySetColor(t, control, "HeaderColor1",    bg);
            TrySetColor(t, control, "HeaderColor2",    bg2);
            TrySetColor(t, control, "HeaderForeColor", fg);

            control.ForeColor = fg;
            control.BackColor = bg2;
            control.Invalidate(true);
        }

        private static void ApplyToGrouper(Control control)
        {
            // O Grouper tem BackgroundColor, BorderColor, CustomGroupBoxColor
            // Usamos reflection para nao criar dependencia circular de namespace
            var t = control.GetType();
            if (t.Name != "Grouper") return;

            var bg   = IsDark ? Background   : Light_Background;
            var fg   = IsDark ? Foreground   : Light_Foreground;
            var bord = IsDark ? Border        : Light_Border;

            TrySetColor(t, control, "BackgroundColor",         bg);
            TrySetColor(t, control, "BackgroundGradientColor",  bg);
            TrySetColor(t, control, "CustomGroupBoxColor",      bg);
            TrySetColor(t, control, "BorderColor",              bord);
            TrySetColor(t, control, "GroupTitleColor",          fg);

            // ForeColor do titulo do grouper
            control.ForeColor = fg;
            control.BackColor = bg;

            // Forcar repintura — o Grouper usa OnPaint customizado
            control.Invalidate(true);
        }

        private static void TrySetColor(Type t, object obj, string propName, Color color)
        {
            try
            {
                var prop = t.GetProperty(propName);
                if (prop != null && prop.CanWrite)
                    prop.SetValue(obj, color);
            }
            catch { }
        }

        // Cores Light para o Grouper (SystemColors)
        private static Color Light_Background => SystemColors.Control;
        private static Color Light_Foreground  => SystemColors.ControlText;
        private static Color Light_Border      => SystemColors.ControlDark;

        private static void ApplyToToolStrip(ToolStrip ts)
        {
            ts.BackColor  = Surface;
            ts.ForeColor  = Foreground;
            ts.RenderMode = ToolStripRenderMode.Professional;
            ts.Renderer   = IsDark ? (ToolStripRenderer)new DarkToolStripRenderer() : new ToolStripProfessionalRenderer();
            foreach (ToolStripItem item in ts.Items)
            { item.BackColor = Surface; item.ForeColor = Foreground; }
        }

        private static void ApplyToDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Background;
            dgv.GridColor       = Border;
            dgv.BorderStyle     = BorderStyle.None;
            dgv.DefaultCellStyle.BackColor          = SurfaceLight;
            dgv.DefaultCellStyle.ForeColor          = Foreground;
            dgv.DefaultCellStyle.SelectionBackColor = Accent;
            dgv.DefaultCellStyle.SelectionForeColor = AccentText;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Surface;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Foreground;
            dgv.ColumnHeadersDefaultCellStyle.BackColor   = Surface;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor   = ForegroundDim;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Surface;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.RowHeadersDefaultCellStyle.BackColor = Surface;
            dgv.RowHeadersDefaultCellStyle.ForeColor = ForegroundDim;
            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Accent;
            dgv.EnableHeadersVisualStyles = false;
        }

        private static void ApplyToTabControl(TabControl tc)
        {
            tc.BackColor = Background;
            if (IsDark)
            {
                // Dark: owner-draw para controlar cores das abas
                tc.Appearance = TabAppearance.FlatButtons;
                tc.DrawItem  -= TabControl_DrawItem;
                tc.DrawMode   = TabDrawMode.OwnerDrawFixed;
                tc.DrawItem  += TabControl_DrawItem;
            }
            else
            {
                // Light: voltar ao renderer nativo do Windows (sem distorcao)
                tc.DrawItem -= TabControl_DrawItem;
                tc.DrawMode  = TabDrawMode.Normal;
                tc.Appearance = TabAppearance.Normal;
            }
        }

        private static void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tc  = (TabControl)sender;
            var tab = tc.TabPages[e.Index];
            bool selected = tc.SelectedIndex == e.Index;
            var bg  = selected ? SurfaceLighter : Background;
            var fg  = selected ? Foreground     : ForegroundDim;

            // Preencher fundo da aba
            using (var bgBrush = new SolidBrush(bg))
                e.Graphics.FillRectangle(bgBrush, e.Bounds);

            // Borda inferior da aba selecionada: mesma cor do fundo da TabPage (sem linha visivel)
            // Borda das abas nao selecionadas: cor da borda do tema
            if (!selected)
            {
                using (var brdPen = new Pen(Border))
                {
                    var r = e.Bounds;
                    r.Inflate(-1, -1);
                    e.Graphics.DrawRectangle(brdPen, r);
                }
            }

            TextRenderer.DrawText(e.Graphics, tab.Text, e.Font, e.Bounds, fg,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private static void ApplyToMenuItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.BackColor = SurfaceLight;
                item.ForeColor = Foreground;
                if (item is ToolStripMenuItem mi)
                    ApplyToMenuItems(mi.DropDownItems);
            }
        }

        // ── DWM: titlebar nativa escura (Windows 10 1903+) ────────────────────
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr,
            ref int attrValue, int attrSize);
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public static void ApplyTitleBar(IntPtr handle)
        {
            try
            {
                int value = IsDark ? 1 : 0;
                DwmSetWindowAttribute(handle, DWMWA_USE_IMMERSIVE_DARK_MODE,
                    ref value, sizeof(int));
            }
            catch { /* Windows < 10 1903 */ }
        }
    }

    // ── Renderer escuro para ToolStrip / MenuStrip ────────────────────────────
    internal class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripRenderer() : base(new DarkColorTable()) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var color = e.Item.Selected
                ? ThemeManager.SurfaceLighter
                : ThemeManager.SurfaceLight;
            e.Graphics.FillRectangle(new SolidBrush(color), e.Item.ContentRectangle);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            => e.Graphics.FillRectangle(new SolidBrush(ThemeManager.Surface), e.AffectedBounds);

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                var color = e.Item.Pressed ? ThemeManager.Accent : ThemeManager.SurfaceLighter;
                e.Graphics.FillRectangle(new SolidBrush(color), new Rectangle(Point.Empty, e.Item.Size));
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            var pen = new Pen(ThemeManager.Border);
            var r   = e.Item.ContentRectangle;
            if (e.Vertical)
                e.Graphics.DrawLine(pen, r.Left + r.Width / 2, r.Top, r.Left + r.Width / 2, r.Bottom);
            else
                e.Graphics.DrawLine(pen, r.Left, r.Top + r.Height / 2, r.Right, r.Top + r.Height / 2);
        }
    }

    internal class DarkColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder                => ThemeManager.Border;
        public override Color MenuItemSelected              => ThemeManager.SurfaceLighter;
        public override Color MenuItemSelectedGradientBegin => ThemeManager.SurfaceLighter;
        public override Color MenuItemSelectedGradientEnd   => ThemeManager.SurfaceLighter;
        public override Color MenuItemPressedGradientBegin  => ThemeManager.Accent;
        public override Color MenuItemPressedGradientEnd    => ThemeManager.Accent;
        public override Color MenuStripGradientBegin        => ThemeManager.Surface;
        public override Color MenuStripGradientEnd          => ThemeManager.Surface;
        public override Color ToolStripDropDownBackground   => ThemeManager.SurfaceLight;
        public override Color ImageMarginGradientBegin      => ThemeManager.Surface;
        public override Color ImageMarginGradientMiddle     => ThemeManager.Surface;
        public override Color ImageMarginGradientEnd        => ThemeManager.Surface;
        public override Color ToolStripBorder               => ThemeManager.Border;
        public override Color SeparatorLight                => ThemeManager.Border;
        public override Color SeparatorDark                 => ThemeManager.SurfaceLight;
        public override Color ButtonSelectedBorder          => ThemeManager.Accent;
        public override Color ButtonSelectedGradientBegin   => ThemeManager.SurfaceLighter;
        public override Color ButtonSelectedGradientEnd     => ThemeManager.SurfaceLighter;
        public override Color ButtonCheckedGradientBegin    => ThemeManager.Accent;
        public override Color ButtonCheckedGradientEnd      => ThemeManager.Accent;
        public override Color ToolStripGradientBegin        => ThemeManager.Surface;
        public override Color ToolStripGradientMiddle       => ThemeManager.Surface;
        public override Color ToolStripGradientEnd          => ThemeManager.Surface;
        public override Color ToolStripContentPanelGradientBegin => ThemeManager.Background;
        public override Color ToolStripContentPanelGradientEnd   => ThemeManager.Background;
        public override Color StatusStripGradientBegin      => ThemeManager.Surface;
        public override Color StatusStripGradientEnd        => ThemeManager.Surface;
        public override Color CheckBackground               => ThemeManager.Accent;
        public override Color CheckPressedBackground        => ThemeManager.AccentHover;
        public override Color CheckSelectedBackground       => ThemeManager.Accent;
        public override Color OverflowButtonGradientBegin   => ThemeManager.Surface;
        public override Color OverflowButtonGradientMiddle  => ThemeManager.Surface;
        public override Color OverflowButtonGradientEnd     => ThemeManager.Surface;
        public override Color GripLight                     => ThemeManager.Border;
        public override Color GripDark                      => ThemeManager.Surface;
    }
}
