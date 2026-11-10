using System.Drawing;
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
    }
}

