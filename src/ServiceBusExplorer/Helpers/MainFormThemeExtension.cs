// MainFormThemeExtension.cs — gerado pelo build-dark-theme.ps1
// Caminho: src/ServiceBusExplorer/Helpers/MainFormThemeExtension.cs
using System;
using System.Windows.Forms;
using ServiceBusExplorer.Forms;

namespace ServiceBusExplorer.Helpers
{
    internal static class MainFormThemeExtension
    {
        internal static void InitTheme(MainForm form, MenuStrip menuStrip)
        {
            ThemeManager.Apply(form);

            // Titlebar nativa escura (Windows 10 1903+)
            form.HandleCreated += (s, e) =>
            {
                ThemeManager.ApplyTitleBar(form.Handle);
                ThemeManager.ThemeChanged += (_, __) => ThemeManager.ApplyTitleBar(form.Handle);
            };

            // Adicionar submenu Theme > Dark / Light no menu View
            ToolStripMenuItem viewMenu = null;
            foreach (ToolStripItem item in menuStrip.Items)
            {
                if (item is ToolStripMenuItem mi &&
                    mi.Text.Replace("&", "").Equals("View", StringComparison.OrdinalIgnoreCase))
                {
                    viewMenu = mi;
                    break;
                }
            }
            if (viewMenu == null) return;

            var themeMenu = new ToolStripMenuItem("Theme");
            var darkItem  = new ToolStripMenuItem("Dark")  { CheckOnClick = false };
            var lightItem = new ToolStripMenuItem("Light") { CheckOnClick = false };

            Action updateChecks = () =>
            {
                darkItem.Checked  = ThemeManager.IsDark;
                lightItem.Checked = !ThemeManager.IsDark;
            };

            darkItem.Click  += (s, e) => { ThemeManager.SetTheme(Theme.Dark);  updateChecks(); };
            lightItem.Click += (s, e) => { ThemeManager.SetTheme(Theme.Light); updateChecks(); };

            themeMenu.DropDownItems.Add(darkItem);
            themeMenu.DropDownItems.Add(lightItem);
            viewMenu.DropDownItems.Add(new ToolStripSeparator());
            viewMenu.DropDownItems.Add(themeMenu);
            updateChecks();
        }
    }
}
