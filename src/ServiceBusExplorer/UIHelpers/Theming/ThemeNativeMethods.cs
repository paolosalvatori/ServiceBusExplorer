using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    internal static class ThemeNativeMethods
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr window, int attribute, ref int value, int size);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr window);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr window, IntPtr deviceContext);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RedrawWindow(IntPtr window, IntPtr updateRectangle, IntPtr updateRegion, uint flags);

        public static void ApplyCaption(Form form, bool dark)
        {
            if (!form.IsHandleCreated || Environment.OSVersion.Version.Major < 6)
                return;

            var enabled = dark ? 1 : 0;
            var result = DwmSetWindowAttribute(form.Handle, 20, ref enabled, sizeof(int));
            // Older Windows releases do not support dark window captions.
            if (result != 0 && result != unchecked((int)0x80070057))
                Trace.WriteLine($"Dark window caption unavailable (HRESULT 0x{result:X8}).");
        }

        public static IDisposable TrackInputBorder(Control control)
        {
            if (HasInputBorder(control))
                return new InputBorderWindow(control);
            return null;
        }

        private static bool HasInputBorder(Control control) => control is TextBoxBase || control is UpDownBase ||
            control is ComboBox || control is ListView || control is DataGridView ||
            control is FastColoredTextBoxNS.FastColoredTextBox;

        public static void RefreshInputBorder(Control control)
        {
            if (control.IsHandleCreated && HasInputBorder(control) &&
                !RedrawWindow(control.Handle, IntPtr.Zero, IntPtr.Zero, 0x0405))
                Trace.WriteLine($"Unable to refresh input border (Win32 error {Marshal.GetLastWin32Error()}).");
        }

        private sealed class InputBorderWindow : NativeWindow, IDisposable
        {
            private readonly Control control;

            public InputBorderWindow(Control control)
            {
                this.control = control;
                control.HandleCreated += HandleCreated;
                control.HandleDestroyed += HandleDestroyed;
                if (control.IsHandleCreated)
                    AssignHandle(control.Handle);
            }

            private void HandleCreated(object sender, EventArgs e) => AssignHandle(control.Handle);
            private void HandleDestroyed(object sender, EventArgs e) => ReleaseHandle();

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (!ThemeManager.IsThemed || control.Width < 4 || control.Height < 4 ||
                    (control is TextBoxBase text && text.BorderStyle == BorderStyle.None) ||
                    (control is ListView list && list.BorderStyle == BorderStyle.None) ||
                    (control is DataGridView grid && grid.BorderStyle == BorderStyle.None) ||
                    (control is FastColoredTextBoxNS.FastColoredTextBox editor && editor.BorderStyle == BorderStyle.None))
                    return;

                if (m.Msg == 0x0317 && m.WParam != IntPtr.Zero)
                {
                    using (var graphics = Graphics.FromHdc(m.WParam))
                        PaintBorder(graphics);
                }
                else if (m.Msg == 0x000F || m.Msg == 0x0085)
                {
                    var context = GetWindowDC(Handle);
                    if (context == IntPtr.Zero)
                    {
                        Trace.WriteLine("Unable to obtain the input border device context.");
                        return;
                    }
                    try
                    {
                        using (var graphics = Graphics.FromHdc(context))
                            PaintBorder(graphics);
                    }
                    finally
                    {
                        ReleaseDC(Handle, context);
                    }
                }
            }

            private void PaintBorder(Graphics graphics)
            {
                using (var border = new Pen(ThemeManager.Palette.Border))
                {
                    graphics.DrawRectangle(border, 0, 0, control.Width - 1, control.Height - 1);
                    graphics.DrawRectangle(border, 1, 1, control.Width - 3, control.Height - 3);
                }
            }

            public void Dispose()
            {
                control.HandleCreated -= HandleCreated;
                control.HandleDestroyed -= HandleDestroyed;
                ReleaseHandle();
            }
        }

        internal sealed class TabWindow : NativeWindow, IDisposable
        {
            private readonly TabControl tabs;

            public TabWindow(TabControl tabs)
            {
                this.tabs = tabs;
                tabs.HandleCreated += HandleCreated;
                tabs.HandleDestroyed += HandleDestroyed;
                if (tabs.IsHandleCreated)
                    AssignHandle(tabs.Handle);
            }

            private void HandleCreated(object sender, EventArgs e) => AssignHandle(tabs.Handle);
            private void HandleDestroyed(object sender, EventArgs e) => ReleaseHandle();

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if ((m.Msg != 0x000F && m.Msg != 0x0317 && m.Msg != 0x0318) ||
                    !ThemeManager.IsThemed || tabs.TabCount == 0 ||
                    tabs.Alignment != TabAlignment.Top || tabs.Multiline)
                    return;

                using (var graphics = m.Msg == 0x000F ? Graphics.FromHwnd(Handle) : Graphics.FromHdc(m.WParam))
                using (var background = new SolidBrush(ThemeManager.Palette.Background))
                using (var border = new Pen(ThemeManager.Palette.Border))
                {
                    var first = tabs.GetTabRect(0);
                    var last = tabs.GetTabRect(tabs.TabCount - 1);
                    graphics.FillRectangle(background, 0, 0, tabs.ClientSize.Width, Math.Max(0, first.Top));
                    graphics.FillRectangle(background, 0, first.Top, Math.Max(0, first.Left), last.Bottom - first.Top);
                    graphics.FillRectangle(background, last.Right, 0,
                        Math.Max(0, tabs.ClientSize.Width - last.Right), last.Bottom);
                    var content = tabs.DisplayRectangle;
                    graphics.FillRectangle(background, 0, last.Bottom, tabs.ClientSize.Width,
                        Math.Max(0, content.Y - last.Bottom));
                    graphics.FillRectangle(background, 0, content.Y, content.X, content.Height);
                    graphics.FillRectangle(background, content.Right, content.Y,
                        Math.Max(0, tabs.ClientSize.Width - content.Right), content.Height);
                    graphics.FillRectangle(background, 0, content.Bottom, tabs.ClientSize.Width,
                        Math.Max(0, tabs.ClientSize.Height - content.Bottom));
                    graphics.DrawRectangle(border, content.X - 1, content.Y - 1,
                        content.Width + 1, content.Height + 1);
                }
            }

            public void Dispose()
            {
                tabs.HandleCreated -= HandleCreated;
                tabs.HandleDestroyed -= HandleDestroyed;
                ReleaseHandle();
            }
        }
    }
}
