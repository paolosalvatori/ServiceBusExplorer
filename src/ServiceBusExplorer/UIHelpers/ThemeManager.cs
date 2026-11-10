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

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using ServiceBusExplorer.Controls;

namespace ServiceBusExplorer.UIHelpers
{
    public static class ThemeManager
    {
        #region CBT Hook for Automatic Form/Dialog Theming
        private const int WH_CBT = 5;
        private const int HCBT_ACTIVATE = 5;

        private static IntPtr hookHandle = IntPtr.Zero;
        private static HookProc hookProcDelegate;

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        static ThemeManager()
        {
            try
            {
                InstallHook();
            }
            catch
            {
                // Ignore
            }
        }

        public static void InstallHook()
        {
            if (hookHandle == IntPtr.Zero)
            {
                hookProcDelegate = CbtHookCallback;
                var threadId = GetCurrentThreadId();
                hookHandle = SetWindowsHookEx(WH_CBT, hookProcDelegate, IntPtr.Zero, threadId);
            }
        }

        private static IntPtr CbtHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                if (nCode == HCBT_ACTIVATE)
                {
                    var control = Control.FromHandle(wParam);
                    if (control is Form form)
                    {
                        ApplyTheme(form);
                        form.Shown -= Form_Shown;
                        form.Shown += Form_Shown;
                    }
                }
            }
            catch
            {
                // Ignore
            }

            return CallNextHookEx(hookHandle, nCode, wParam, lParam);
        }

        private static void Form_Shown(object sender, EventArgs e)
        {
            if (sender is Form form)
            {
                ApplyTheme(form);
            }
        }
        #endregion
        #region Constants
        public const string LightTheme = "Light";
        public const string DarkTheme = "Dark";

        // Dark theme palette
        public static readonly Color DarkFormBackground = Color.FromArgb(30, 30, 30);
        public static readonly Color DarkSurfaceBackground = Color.FromArgb(37, 37, 38);
        public static readonly Color DarkControlBackground = Color.FromArgb(45, 45, 48);
        public static readonly Color DarkInputBackground = Color.FromArgb(40, 40, 42);
        public static readonly Color DarkTextColor = Color.FromArgb(240, 240, 240);
        public static readonly Color DarkMutedText = Color.FromArgb(180, 180, 180);
        public static readonly Color DarkBorderColor = Color.FromArgb(63, 63, 70);
        public static readonly Color DarkAccentColor = Color.FromArgb(0, 122, 204);
        public static readonly Color DarkHeaderColor1 = Color.FromArgb(50, 50, 52);
        public static readonly Color DarkHeaderColor2 = Color.FromArgb(35, 35, 37);
        public static readonly Color DarkGridLine = Color.FromArgb(55, 55, 58);
        public static readonly Color DarkAlternateRow = Color.FromArgb(33, 33, 35);

        // Light theme palette
        public static readonly Color LightFormBackground = Color.FromArgb(215, 228, 242);
        public static readonly Color LightHeaderColor1 = Color.FromArgb(191, 205, 219);
        public static readonly Color LightHeaderColor2 = Color.FromArgb(153, 180, 209);
        #endregion

        #region Public Properties
        public static string CurrentTheme { get; private set; } = LightTheme;

        public static bool IsDarkTheme => string.Equals(CurrentTheme, DarkTheme, StringComparison.OrdinalIgnoreCase);

        public static event Action ThemeChanged;
        #endregion

        #region P/Invoke for Windows Immersive Dark Mode
        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        public static void SetWindowTheme(IntPtr handle, bool isDark)
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 10 && handle != IntPtr.Zero)
                {
                    int darkMode = isDark ? 1 : 0;
                    // Attribute 20 = DWMWA_USE_IMMERSIVE_DARK_MODE on Windows 10 (19041+) and Windows 11
                    // Attribute 19 = fallback for earlier Windows 10 builds
                    if (DwmSetWindowAttribute(handle, 20, ref darkMode, sizeof(int)) != 0)
                    {
                        DwmSetWindowAttribute(handle, 19, ref darkMode, sizeof(int));
                    }
                }
            }
            catch
            {
                // Silently ignore on operating systems that do not support dwmapi dark mode
            }
        }
        #endregion

        #region Theme Switching
        public static void SetTheme(string theme)
        {
            CurrentTheme = string.Equals(theme, DarkTheme, StringComparison.OrdinalIgnoreCase) ? DarkTheme : LightTheme;

            if (IsDarkTheme)
            {
                ToolStripManager.Renderer = new DarkToolStripRenderer();
            }
            else
            {
                ToolStripManager.Renderer = null;
            }

            try
            {
                foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
                {
                    ApplyTheme(form);
                }
            }
            catch
            {
                // Ignore
            }

            ThemeChanged?.Invoke();
        }
        #endregion

        #region Recursive Control Theming
        public static void ApplyTheme(Control root)
        {
            if (root == null)
            {
                return;
            }

            if (root is Form form)
            {
                SetWindowTheme(form.Handle, IsDarkTheme);
                form.BackColor = IsDarkTheme ? DarkFormBackground : LightFormBackground;
                form.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
            }

            ApplyControlTheme(root);

            foreach (Control child in root.Controls)
            {
                ApplyTheme(child);
            }
        }

        private static void ApplyControlTheme(Control c)
        {
            if (c is HeaderPanel headerPanel)
            {
                if (IsDarkTheme)
                {
                    headerPanel.HeaderColor1 = DarkHeaderColor1;
                    headerPanel.HeaderColor2 = DarkHeaderColor2;
                    headerPanel.ForeColor = DarkTextColor;
                    headerPanel.BackColor = DarkFormBackground;
                }
                else
                {
                    headerPanel.HeaderColor1 = LightHeaderColor1;
                    headerPanel.HeaderColor2 = LightHeaderColor2;
                    headerPanel.ForeColor = SystemColors.ControlText;
                    headerPanel.BackColor = LightFormBackground;
                }
                headerPanel.Invalidate();
                return;
            }

            if (c is Grouper grouper)
            {
                if (IsDarkTheme)
                {
                    grouper.BackgroundColor = DarkSurfaceBackground;
                    grouper.BackgroundGradientColor = DarkSurfaceBackground;
                    grouper.BorderColor = DarkBorderColor;
                    grouper.CustomGroupBoxColor = DarkControlBackground;
                    grouper.ForeColor = DarkTextColor;
                    grouper.ShadowColor = Color.FromArgb(20, 20, 20);
                }
                else
                {
                    grouper.BackgroundColor = Color.FromArgb(215, 228, 242);
                    grouper.BackgroundGradientColor = Color.White;
                    grouper.BorderColor = LightHeaderColor2;
                    grouper.CustomGroupBoxColor = Color.White;
                    grouper.ForeColor = SystemColors.ControlText;
                    grouper.ShadowColor = Color.DarkGray;
                }
                grouper.Invalidate();
                return;
            }

            if (c is FastColoredTextBox fctb)
            {
                if (IsDarkTheme)
                {
                    fctb.BackColor = DarkFormBackground;
                    fctb.ForeColor = DarkTextColor;
                    fctb.IndentBackColor = Color.FromArgb(40, 40, 42);
                    fctb.LineNumberColor = Color.FromArgb(120, 120, 120);
                    fctb.CaretColor = Color.White;
                    fctb.SelectionColor = Color.FromArgb(80, 0, 122, 204);
                }
                else
                {
                    fctb.BackColor = Color.White;
                    fctb.ForeColor = Color.Black;
                    fctb.IndentBackColor = Color.WhiteSmoke;
                    fctb.LineNumberColor = Color.Teal;
                    fctb.CaretColor = Color.Black;
                    fctb.SelectionColor = Color.FromArgb(60, 0, 0, 255);
                }
                return;
            }

            if (c is DataGridView dgv)
            {
                if (IsDarkTheme)
                {
                    dgv.BackgroundColor = DarkFormBackground;
                    dgv.GridColor = DarkGridLine;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.DefaultCellStyle.BackColor = DarkSurfaceBackground;
                    dgv.DefaultCellStyle.ForeColor = DarkTextColor;
                    dgv.DefaultCellStyle.SelectionBackColor = DarkAccentColor;
                    dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = DarkControlBackground;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = DarkTextColor;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = DarkControlBackground;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = DarkTextColor;
                    dgv.RowHeadersDefaultCellStyle.BackColor = DarkControlBackground;
                    dgv.RowHeadersDefaultCellStyle.ForeColor = DarkTextColor;
                    dgv.RowHeadersDefaultCellStyle.SelectionBackColor = DarkControlBackground;
                    dgv.RowHeadersDefaultCellStyle.SelectionForeColor = DarkTextColor;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = DarkAlternateRow;
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = DarkTextColor;
                }
                else
                {
                    dgv.BackgroundColor = LightFormBackground;
                    dgv.GridColor = SystemColors.ControlDark;
                    dgv.EnableHeadersVisualStyles = true;
                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                    dgv.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                    dgv.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = LightFormBackground;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = LightFormBackground;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.WindowText;
                    dgv.RowHeadersDefaultCellStyle.BackColor = LightFormBackground;
                    dgv.RowHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText;
                    dgv.RowHeadersDefaultCellStyle.SelectionBackColor = LightFormBackground;
                    dgv.RowHeadersDefaultCellStyle.SelectionForeColor = SystemColors.WindowText;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = SystemColors.ControlText;
                }
                return;
            }

            if (c is TabControl tc)
            {
                tc.BackColor = IsDarkTheme ? DarkControlBackground : LightFormBackground;
                tc.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                foreach (TabPage page in tc.TabPages)
                {
                    page.BackColor = IsDarkTheme ? DarkSurfaceBackground : LightFormBackground;
                    page.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                }
                return;
            }

            if (c is TreeView tv)
            {
                tv.BackColor = IsDarkTheme ? DarkFormBackground : SystemColors.Window;
                tv.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.WindowText;
                tv.LineColor = IsDarkTheme ? Color.FromArgb(90, 90, 90) : SystemColors.GrayText;
                return;
            }

            if (c is ListView lv)
            {
                lv.BackColor = IsDarkTheme ? DarkFormBackground : SystemColors.Window;
                lv.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.WindowText;
                return;
            }

            if (c is ListBox lb)
            {
                lb.BackColor = IsDarkTheme ? DarkFormBackground : SystemColors.Window;
                lb.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.WindowText;
                return;
            }

            if (c is TextBoxBase tb)
            {
                tb.BackColor = IsDarkTheme ? DarkInputBackground : SystemColors.Window;
                tb.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.WindowText;
                return;
            }

            if (c is NumericUpDown nud)
            {
                nud.BackColor = IsDarkTheme ? DarkInputBackground : SystemColors.Window;
                nud.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.WindowText;
                return;
            }

            if (c is ComboBox cb)
            {
                cb.BackColor = IsDarkTheme ? DarkInputBackground : SystemColors.Window;
                cb.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.WindowText;
                cb.FlatStyle = IsDarkTheme ? FlatStyle.Flat : FlatStyle.Standard;
                return;
            }

            if (c is Button btn)
            {
                if (IsDarkTheme)
                {
                    btn.BackColor = DarkControlBackground;
                    btn.ForeColor = DarkTextColor;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = DarkBorderColor;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(65, 65, 68);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(35, 35, 37);
                }
                else
                {
                    btn.BackColor = SystemColors.Control;
                    btn.ForeColor = SystemColors.ControlText;
                    btn.UseVisualStyleBackColor = true;
                    btn.FlatStyle = FlatStyle.Standard;
                }
                return;
            }

            if (c is LinkLabel ll)
            {
                ll.LinkColor = IsDarkTheme ? Color.FromArgb(86, 156, 214) : Color.Navy;
                ll.VisitedLinkColor = IsDarkTheme ? Color.FromArgb(150, 100, 200) : Color.Purple;
                return;
            }

            if (c is Label lbl)
            {
                lbl.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                return;
            }

            if (c is CheckBox chk)
            {
                chk.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                return;
            }

            if (c is RadioButton rb)
            {
                rb.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                return;
            }

            if (c is GroupBox gb)
            {
                gb.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                return;
            }

            if (c is TabPage tp)
            {
                tp.BackColor = IsDarkTheme ? DarkSurfaceBackground : LightFormBackground;
                tp.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                return;
            }

            if (c is PropertyGrid pg)
            {
                if (IsDarkTheme)
                {
                    pg.ViewBackColor = DarkSurfaceBackground;
                    pg.ViewForeColor = DarkTextColor;
                    pg.LineColor = DarkBorderColor;
                    pg.CategoryForeColor = DarkMutedText;
                    pg.CategorySplitterColor = DarkControlBackground;
                }
                else
                {
                    pg.ViewBackColor = SystemColors.Window;
                    pg.ViewForeColor = SystemColors.WindowText;
                    pg.LineColor = SystemColors.InactiveBorder;
                    pg.CategoryForeColor = SystemColors.ControlText;
                    pg.CategorySplitterColor = SystemColors.Control;
                }
                return;
            }

            if (c is MenuStrip ms)
            {
                ms.BackColor = IsDarkTheme ? DarkControlBackground : Color.Transparent;
                ms.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                ms.Renderer = IsDarkTheme ? new DarkToolStripRenderer() : null;
                ApplyToolStripItemsTheme(ms.Items);
                return;
            }

            if (c is StatusStrip ss)
            {
                ss.BackColor = IsDarkTheme ? DarkSurfaceBackground : LightFormBackground;
                ss.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                ss.Renderer = IsDarkTheme ? new DarkToolStripRenderer() : null;
                ApplyToolStripItemsTheme(ss.Items);
                return;
            }

            if (c is ToolStrip ts)
            {
                ts.BackColor = IsDarkTheme ? DarkControlBackground : SystemColors.Control;
                ts.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                ts.Renderer = IsDarkTheme ? new DarkToolStripRenderer() : null;
                ApplyToolStripItemsTheme(ts.Items);
                return;
            }

            if (c is SplitContainer sc)
            {
                sc.BackColor = IsDarkTheme ? DarkControlBackground : LightFormBackground;
                sc.Panel1.BackColor = IsDarkTheme ? DarkFormBackground : LightFormBackground;
                sc.Panel2.BackColor = IsDarkTheme ? DarkFormBackground : LightFormBackground;
                return;
            }

            if (c is UserControl uc && !(c is Grouper) && !(c is HeaderPanel))
            {
                uc.BackColor = IsDarkTheme ? DarkFormBackground : LightFormBackground;
                uc.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
                return;
            }

            if (c is Panel p && !(p is HeaderPanel))
            {
                p.BackColor = IsDarkTheme ? DarkFormBackground : LightFormBackground;
                p.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;
            }
        }

        public static void DrawControlBorder(Graphics g, Control c)
        {
            if (g == null || c == null)
            {
                return;
            }

            using (var pen = new Pen(IsDarkTheme ? DarkBorderColor : SystemColors.ActiveBorder, 1))
            {
                g.DrawRectangle(pen, c.Location.X - 1, c.Location.Y - 1, c.Size.Width + 1, c.Size.Height + 1);
            }
        }

        private static void ApplyToolStripItemsTheme(ToolStripItemCollection items)
        {
            if (items == null)
            {
                return;
            }

            foreach (ToolStripItem item in items)
            {
                item.ForeColor = IsDarkTheme ? DarkTextColor : SystemColors.ControlText;

                if (item is ToolStripDropDownItem dropDownItem && dropDownItem.HasDropDownItems)
                {
                    ApplyToolStripItemsTheme(dropDownItem.DropDownItems);
                }
            }
        }
        #endregion

        #region ListView Column Header Helper
        public static void DrawListViewColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            var startX = e.ColumnIndex == 0 ? -1 : e.Bounds.X;
            var endX = e.Bounds.X + e.Bounds.Width - 1;

            if (IsDarkTheme)
            {
                using (var backBrush = new SolidBrush(DarkControlBackground))
                {
                    e.Graphics.FillRectangle(backBrush, startX, -1, e.Bounds.Width + 1, e.Bounds.Height + 1);
                }
                using (var linePen = new Pen(DarkBorderColor))
                {
                    e.Graphics.DrawLine(linePen, startX, -1, startX, e.Bounds.Y + e.Bounds.Height + 1);
                    e.Graphics.DrawLine(linePen, startX, -1, endX, -1);
                    e.Graphics.DrawLine(linePen, startX, e.Bounds.Height - 1, endX, e.Bounds.Height - 1);
                    e.Graphics.DrawLine(linePen, endX, -1, endX, e.Bounds.Height + 1);
                }
                var roundedFontSize = (float)Math.Round(e.Font.SizeInPoints);
                var bounds = new RectangleF(e.Bounds.X + 4, (e.Bounds.Height - 8 - roundedFontSize) / 2, e.Bounds.Width, roundedFontSize + 6);
                using (var textBrush = new SolidBrush(DarkTextColor))
                {
                    e.Graphics.DrawString(e.Header.Text, e.Font, textBrush, bounds);
                }
            }
            else
            {
                using (var backBrush = new SolidBrush(LightFormBackground))
                {
                    e.Graphics.FillRectangle(backBrush, startX, -1, e.Bounds.Width + 1, e.Bounds.Height + 1);
                }
                e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, startX, e.Bounds.Y + e.Bounds.Height + 1);
                e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), startX, -1, endX, -1);
                e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), startX, e.Bounds.Height - 1, endX, e.Bounds.Height - 1);
                e.Graphics.DrawLine(new Pen(SystemColors.ControlDark), endX, -1, endX, e.Bounds.Height + 1);
                var roundedFontSize = (float)Math.Round(e.Font.SizeInPoints);
                var bounds = new RectangleF(e.Bounds.X + 4, (e.Bounds.Height - 8 - roundedFontSize) / 2, e.Bounds.Width, roundedFontSize + 6);
                e.Graphics.DrawString(e.Header.Text, e.Font, new SolidBrush(SystemColors.ControlText), bounds);
            }
        }
        #endregion
    }

    #region Custom Dark Professional Color Table and Renderer
    public class DarkColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Color.FromArgb(37, 37, 38);
        public override Color ImageMarginGradientBegin => Color.FromArgb(37, 37, 38);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(37, 37, 38);
        public override Color ImageMarginGradientEnd => Color.FromArgb(37, 37, 38);
        public override Color MenuBorder => Color.FromArgb(63, 63, 70);
        public override Color MenuItemBorder => Color.FromArgb(0, 122, 204);
        public override Color MenuItemSelected => Color.FromArgb(62, 62, 64);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(62, 62, 64);
        public override Color MenuItemSelectedGradientEnd => Color.FromArgb(62, 62, 64);
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(39, 39, 41);
        public override Color MenuItemPressedGradientMiddle => Color.FromArgb(39, 39, 41);
        public override Color MenuItemPressedGradientEnd => Color.FromArgb(39, 39, 41);
        public override Color MenuStripGradientBegin => Color.FromArgb(45, 45, 48);
        public override Color MenuStripGradientEnd => Color.FromArgb(45, 45, 48);
        public override Color StatusStripGradientBegin => Color.FromArgb(37, 37, 38);
        public override Color StatusStripGradientEnd => Color.FromArgb(37, 37, 38);
        public override Color ToolStripBorder => Color.FromArgb(63, 63, 70);
        public override Color SeparatorDark => Color.FromArgb(65, 65, 67);
        public override Color SeparatorLight => Color.FromArgb(30, 30, 30);
        public override Color ButtonSelectedHighlight => Color.FromArgb(62, 62, 64);
        public override Color ButtonSelectedGradientBegin => Color.FromArgb(62, 62, 64);
        public override Color ButtonSelectedGradientMiddle => Color.FromArgb(62, 62, 64);
        public override Color ButtonSelectedGradientEnd => Color.FromArgb(62, 62, 64);
        public override Color ButtonPressedHighlight => Color.FromArgb(39, 39, 41);
        public override Color ButtonPressedGradientBegin => Color.FromArgb(39, 39, 41);
        public override Color ButtonPressedGradientMiddle => Color.FromArgb(39, 39, 41);
        public override Color ButtonPressedGradientEnd => Color.FromArgb(39, 39, 41);
        public override Color CheckBackground => Color.FromArgb(55, 55, 58);
        public override Color CheckSelectedBackground => Color.FromArgb(70, 70, 74);
        public override Color CheckPressedBackground => Color.FromArgb(39, 39, 41);
        public override Color GripDark => Color.FromArgb(70, 70, 70);
        public override Color GripLight => Color.FromArgb(45, 45, 48);
    }

    public class DarkToolStripRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripRenderer() : base(new DarkColorTable())
        {
            RoundedEdges = false;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Enabled ? Color.FromArgb(240, 240, 240) : Color.FromArgb(120, 120, 120);
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = e.Item.Enabled ? Color.FromArgb(220, 220, 220) : Color.FromArgb(120, 120, 120);
            base.OnRenderArrow(e);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            var rect = new Rectangle(e.ImageRectangle.X - 2, e.ImageRectangle.Y - 2, e.ImageRectangle.Width + 4, e.ImageRectangle.Height + 4);
            using (var brush = new SolidBrush(Color.FromArgb(0, 122, 204)))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            using (var pen = new Pen(Color.White, 2))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawLines(pen, new[]
                {
                    new Point(rect.X + 3, rect.Y + rect.Height / 2),
                    new Point(rect.X + rect.Width / 2 - 1, rect.Y + rect.Height - 4),
                    new Point(rect.X + rect.Width - 3, rect.Y + 4)
                });
            }
        }
    }
    #endregion
}
