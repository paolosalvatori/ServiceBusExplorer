using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FluentAssertions;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class MainSettingsThemeTests
    {
        [Fact]
        public void DefaultTheme_ShouldBeLight()
        {
            var settings = new MainSettings();
            settings.SetDefault();

            settings.Theme.Should().Be("Light");
        }

        [Fact]
        public void Equals_SameTheme_ReturnsTrue()
        {
            var first = new MainSettings();
            first.SetDefault();
            first.Theme = "Dark";

            var second = new MainSettings();
            second.SetDefault();
            second.Theme = "Dark";

            first.Equals(second).Should().BeTrue();
        }

        [Fact]
        public void Equals_DifferentTheme_ReturnsFalse()
        {
            var first = new MainSettings();
            first.SetDefault();
            first.Theme = "Light";

            var second = new MainSettings();
            second.SetDefault();
            second.Theme = "Dark";

            first.Equals(second).Should().BeFalse();
        }

        [Fact]
        public void GetValue_ThemeParameter_ReturnsConfiguredTheme()
        {
            var settings = new MainSettings();
            settings.SetDefault();
            settings.Theme = "Dark";

            var value = settings.GetValue(ConfigurationParameters.Theme);

            value.Should().Be("Dark");
        }

        [Fact]
        public void ThemeManager_SetTheme_UpdatesThemeState()
        {
            ThemeManager.SetTheme(ThemeManager.DarkTheme);
            ThemeManager.IsDarkTheme.Should().BeTrue();
            ThemeManager.CurrentTheme.Should().Be("Dark");

            ThemeManager.SetTheme(ThemeManager.LightTheme);
            ThemeManager.IsDarkTheme.Should().BeFalse();
            ThemeManager.CurrentTheme.Should().Be("Light");
        }

        [Fact]
        public void ThemeManager_ApplyTheme_Dark_AppliesToFormAndControls()
        {
            ThemeManager.SetTheme(ThemeManager.DarkTheme);

            using (var form = new Form())
            using (var grouper = new Grouper())
            using (var btn = new Button())
            using (var lv = new ListView())
            {
                form.Controls.Add(grouper);
                grouper.Controls.Add(btn);
                form.Controls.Add(lv);

                ThemeManager.ApplyTheme(form);

                form.BackColor.Should().Be(ThemeManager.DarkFormBackground);
                form.ForeColor.Should().Be(ThemeManager.DarkTextColor);

                grouper.BackgroundColor.Should().Be(ThemeManager.DarkSurfaceBackground);
                grouper.ForeColor.Should().Be(ThemeManager.DarkTextColor);

                btn.BackColor.Should().Be(ThemeManager.DarkControlBackground);
                btn.ForeColor.Should().Be(ThemeManager.DarkTextColor);

                lv.BackColor.Should().Be(ThemeManager.DarkFormBackground);
                lv.ForeColor.Should().Be(ThemeManager.DarkTextColor);
            }
        }

        [Fact]
        public void ThemeManager_ApplyTheme_Light_AppliesToFormAndControls()
        {
            ThemeManager.SetTheme(ThemeManager.LightTheme);

            using (var form = new Form())
            using (var grouper = new Grouper())
            using (var btn = new Button())
            {
                form.Controls.Add(grouper);
                grouper.Controls.Add(btn);

                ThemeManager.ApplyTheme(form);

                form.BackColor.Should().Be(ThemeManager.LightFormBackground);
                grouper.BackgroundColor.Should().Be(Color.FromArgb(215, 228, 242));
                btn.BackColor.Should().Be(SystemColors.Control);
            }
        }

        [Fact]
        public void ThemeManager_ApplyTheme_PreservesDashboardWarningPalette_Dark()
        {
            ThemeManager.SetTheme(ThemeManager.DarkTheme);

            using (var form = new Form())
            using (var dashboard = new DashboardControl())
            {
                form.Controls.Add(dashboard);

                ThemeManager.ApplyTheme(form);

                var warningPanel = dashboard.Controls.Cast<Control>().OfType<Panel>()
                    .FirstOrDefault(p => p.Controls.Count > 0 && p.Controls[0] is Label l && l.Text.Contains("Auto refreshing"));

                warningPanel.Should().NotBeNull();
                warningPanel.BackColor.Should().Be(Color.FromArgb(60, 50, 20));
                warningPanel.Controls[0].ForeColor.Should().Be(Color.FromArgb(255, 220, 100));
            }
        }

        [Fact]
        public void ThemeManager_ApplyTheme_PreservesDashboardWarningPalette_Light()
        {
            ThemeManager.SetTheme(ThemeManager.LightTheme);

            using (var form = new Form())
            using (var dashboard = new DashboardControl())
            {
                form.Controls.Add(dashboard);

                ThemeManager.ApplyTheme(form);

                var warningPanel = dashboard.Controls.Cast<Control>().OfType<Panel>()
                    .FirstOrDefault(p => p.Controls.Count > 0 && p.Controls[0] is Label l && l.Text.Contains("Auto refreshing"));

                warningPanel.Should().NotBeNull();
                warningPanel.BackColor.Should().Be(Color.FromArgb(255, 243, 205));
                warningPanel.Controls[0].ForeColor.Should().Be(Color.FromArgb(102, 77, 3));
            }
        }

        [Fact]
        public void ThemeManager_ApplyTheme_SetsAndRestoresDataGridViewRowsDefaultCellStyle()
        {
            using (var form = new Form())
            using (var dgv = new DataGridView())
            {
                dgv.Columns.Add("Col1", "Column 1");
                dgv.RowsDefaultCellStyle.BackColor = SystemColors.Window;
                dgv.RowsDefaultCellStyle.ForeColor = SystemColors.ControlText;

                var rowIndex = dgv.Rows.Add("TestValue");
                var row = dgv.Rows[rowIndex];
                row.DefaultCellStyle.BackColor = SystemColors.Window;
                row.DefaultCellStyle.ForeColor = SystemColors.ControlText;

                form.Controls.Add(dgv);

                // Switch to Dark
                ThemeManager.SetTheme(ThemeManager.DarkTheme);
                ThemeManager.ApplyTheme(form);

                dgv.RowsDefaultCellStyle.BackColor.Should().Be(ThemeManager.DarkSurfaceBackground);
                dgv.RowsDefaultCellStyle.ForeColor.Should().Be(ThemeManager.DarkTextColor);
                row.DefaultCellStyle.BackColor.Should().Be(ThemeManager.DarkSurfaceBackground);
                row.DefaultCellStyle.ForeColor.Should().Be(ThemeManager.DarkTextColor);

                // Switch back to Light
                ThemeManager.SetTheme(ThemeManager.LightTheme);
                ThemeManager.ApplyTheme(form);

                dgv.RowsDefaultCellStyle.BackColor.Should().Be(SystemColors.Window);
                dgv.RowsDefaultCellStyle.ForeColor.Should().Be(SystemColors.ControlText);
                row.DefaultCellStyle.BackColor.Should().Be(SystemColors.Window);
                row.DefaultCellStyle.ForeColor.Should().Be(SystemColors.ControlText);
            }
        }

        [Fact]
        public void ThemeManager_ApplyTheme_ExcludesColorEditingControl()
        {
            using (var form = new Form())
            using (var normalBtn = new Button())
            using (var colorPicker = new ColorEditingControl())
            {
                form.Controls.Add(normalBtn);
                form.Controls.Add(colorPicker);

                colorPicker.Value = Color.Goldenrod;

                // Test Dark theme
                ThemeManager.SetTheme(ThemeManager.DarkTheme);
                ThemeManager.ApplyTheme(form);

                normalBtn.BackColor.Should().Be(ThemeManager.DarkControlBackground);
                colorPicker.Value.Should().Be(Color.Goldenrod);
                colorPicker.BackColor.Should().Be(Color.Goldenrod);

                // Test Light theme
                ThemeManager.SetTheme(ThemeManager.LightTheme);
                ThemeManager.ApplyTheme(form);

                normalBtn.BackColor.Should().Be(SystemColors.Control);
                colorPicker.Value.Should().Be(Color.Goldenrod);
                colorPicker.BackColor.Should().Be(Color.Goldenrod);
            }
        }

        [Fact]
        public void TextForm_ButtonMouseLeave_RetainsDarkTextColorInDarkMode()
        {
            using (var form = new ServiceBusExplorer.Forms.TextForm("Title", "Label", "Content"))
            {
                var btnOk = form.Controls.Find("btnOk", true).FirstOrDefault() as Button;
                btnOk.Should().NotBeNull();

                // Dark mode
                ThemeManager.SetTheme(ThemeManager.DarkTheme);
                ThemeManager.ApplyTheme(form);

                // Simulate MouseLeave
                var mouseLeaveMethod = typeof(ServiceBusExplorer.Forms.TextForm)
                    .GetMethod("button_MouseLeave", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                mouseLeaveMethod.Should().NotBeNull();
                mouseLeaveMethod.Invoke(form, new object[] { btnOk, EventArgs.Empty });

                btnOk.ForeColor.Should().Be(ThemeManager.DarkTextColor);

                // Light mode
                ThemeManager.SetTheme(ThemeManager.LightTheme);
                ThemeManager.ApplyTheme(form);

                mouseLeaveMethod.Invoke(form, new object[] { btnOk, EventArgs.Empty });
                btnOk.ForeColor.Should().Be(SystemColors.ControlText);
            }
        }

        [Fact]
        public void ThemeManager_ApplyTheme_ExcludesColorEditingControl()
        {
            using (var form = new Form())
            using (var normalBtn = new Button())
            using (var colorPicker = new ColorEditingControl())
            {
                form.Controls.Add(normalBtn);
                form.Controls.Add(colorPicker);

                colorPicker.Value = Color.Goldenrod;

                // Test Dark theme
                ThemeManager.SetTheme(ThemeManager.DarkTheme);
                ThemeManager.ApplyTheme(form);

                normalBtn.BackColor.Should().Be(ThemeManager.DarkControlBackground);
                colorPicker.Value.Should().Be(Color.Goldenrod);
                colorPicker.BackColor.Should().Be(Color.Goldenrod);

                // Test Light theme
                ThemeManager.SetTheme(ThemeManager.LightTheme);
                ThemeManager.ApplyTheme(form);

                normalBtn.BackColor.Should().Be(SystemColors.Control);
                colorPicker.Value.Should().Be(Color.Goldenrod);
                colorPicker.BackColor.Should().Be(Color.Goldenrod);
            }
        }

        [Fact]
        public void TextForm_ButtonMouseLeave_RetainsDarkTextColorInDarkMode()
        {
            using (var form = new ServiceBusExplorer.Forms.TextForm("Title", "Label", "Content"))
            {
                var btnOk = form.Controls.Find("btnOk", true).FirstOrDefault() as Button;
                btnOk.Should().NotBeNull();

                // Dark mode
                ThemeManager.SetTheme(ThemeManager.DarkTheme);
                ThemeManager.ApplyTheme(form);

                // Simulate MouseLeave
                var mouseLeaveMethod = typeof(ServiceBusExplorer.Forms.TextForm)
                    .GetMethod("button_MouseLeave", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                mouseLeaveMethod.Should().NotBeNull();
                mouseLeaveMethod.Invoke(form, new object[] { btnOk, EventArgs.Empty });

                btnOk.ForeColor.Should().Be(ThemeManager.DarkTextColor);

                // Light mode
                ThemeManager.SetTheme(ThemeManager.LightTheme);
                ThemeManager.ApplyTheme(form);

                mouseLeaveMethod.Invoke(form, new object[] { btnOk, EventArgs.Empty });
                btnOk.ForeColor.Should().Be(SystemColors.ControlText);
            }
        }
    }
}

