using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;

using FluentAssertions;

using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers.Theming;

using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [Collection("Theme UI")]
    public class OptionFormThemeTests
    {
        [Theory]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        [InlineData(ThemeMode.Light)]
        [InlineData(ThemeMode.Dark)]
        public void Constructor_UsesSuppliedThemeModeForInitialDropdownSelection(ThemeMode mode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Light);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.ThemeMode = mode;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    var comboBox = FindControl<ComboBox>(form, "cboTheme");
                    comboBox.SelectedIndex.Should().Be((int)mode);
                    comboBox.DropDownStyle.Should().Be(ComboBoxStyle.DropDownList);
                    comboBox.Items.Cast<string>().Should().Equal("Follow operating system theme", "Light", "Dark");
                    settings.ThemeMode.Should().Be(mode);
                    ThemeManager.IsDark.Should().BeFalse();
                }
            });
        }

        [Theory]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        [InlineData(ThemeMode.Light)]
        [InlineData(ThemeMode.Dark)]
        public void ThemeDropdown_ChangingSelection_DoesNotSwitchGlobalThemeBeforeSave(ThemeMode mode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Light);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.ThemeMode = ThemeMode.Light;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    var comboBox = FindControl<ComboBox>(form, "cboTheme");

                    comboBox.SelectedIndex = (int)mode;

                    settings.ThemeMode.Should().Be(mode);
                    ThemeManager.Mode.Should().Be(ThemeMode.Light);
                    ThemeManager.IsDark.Should().BeFalse();
                }
            });
        }

        [Theory]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        [InlineData(ThemeMode.Light)]
        public void Cancel_AfterUnsavedThemeChange_PreservesGlobalTheme(ThemeMode mode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Dark);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.ThemeMode = ThemeMode.Dark;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    form.Shown += (sender, args) =>
                    {
                        form.BeginInvoke(new Action(() =>
                        {
                            FindControl<ComboBox>(form, "cboTheme").SelectedIndex = (int)mode;
                            ThemeManager.IsDark.Should().Be(!SystemInformation.HighContrast);
                            GetButton(form, "btnCancel").PerformClick();
                        }));
                    };

                    form.ShowDialog().Should().Be(DialogResult.Cancel);
                    ThemeManager.Mode.Should().Be(ThemeMode.Dark);
                    ThemeManager.IsDark.Should().Be(!SystemInformation.HighContrast);
                }
            });
        }

        [Fact]
        public void Reset_SetsThemeDropdownBackToFollowOperatingSystem()
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Dark);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.ThemeMode = ThemeMode.Light;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    FindControl<ComboBox>(form, "cboTheme").SelectedIndex.Should().Be((int)ThemeMode.Light);

                    InvokePrivateMethod(form, "btnReset_Click", GetButton(form, "btnReset"), EventArgs.Empty);

                    FindControl<ComboBox>(form, "cboTheme").SelectedIndex.Should().Be((int)ThemeMode.FollowOperatingSystem);
                    settings.ThemeMode.Should().Be(ThemeMode.FollowOperatingSystem);
                    ThemeManager.Mode.Should().Be(ThemeMode.Dark);
                }
            });
        }

        [Theory]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        [InlineData(ThemeMode.Light)]
        [InlineData(ThemeMode.Dark)]
        public void GeneralTab_WhenShown_KeepsEveryOptionInsideTheVisiblePage(ThemeMode mode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(mode);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.ThemeMode = mode;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    form.Show();
                    form.PerformLayout();

                    var page = FindControl<TabPage>(form, "tabPageGeneral");
                    var controls = page.Controls.Cast<Control>().ToArray();
                    foreach (var control in controls)
                    {
                        page.ClientRectangle.Contains(control.Bounds).Should().BeTrue(
                            "{0} at {1} must fit inside the visible page {2}",
                            control.Name, control.Bounds, page.ClientRectangle);

                        foreach (var other in controls.Where(other => other != control))
                        {
                            control.Bounds.IntersectsWith(other.Bounds).Should().BeFalse(
                                "{0} must not overlap {1}", control.Name, other.Name);
                        }
                    }

                    var comboBox = FindControl<ComboBox>(form, "cboTheme");
                    var label = FindControl<Label>(form, "lblTheme");
                    comboBox.Left.Should().Be(GetCheckBox(form, "useAsciiCheckBox").Left);
                    label.Left.Should().Be(FindControl<Label>(form, "lblUseAscii").Left);
                    comboBox.Top.Should().BeGreaterThan(
                        GetCheckBox(form, "disableAccidentalDeletionPrevention").Bottom);
                    page.VerticalScroll.Visible.Should().BeFalse();

                    AssertContainersAndButtonsFit(form, page);
                }
            });
        }

        [Theory]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        [InlineData(ThemeMode.Light)]
        [InlineData(ThemeMode.Dark)]
        public void GeneralTab_WhenDialogHeightIsReduced_CanScrollToTheWholeThemeRow(ThemeMode mode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(mode);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.ThemeMode = mode;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    form.Show();
                    form.ClientSize = new Size(form.ClientSize.Width, form.ClientSize.Height - 80);
                    form.PerformLayout();

                    var page = FindControl<TabPage>(form, "tabPageGeneral");
                    var comboBox = FindControl<ComboBox>(form, "cboTheme");
                    var label = FindControl<Label>(form, "lblTheme");

                    page.AutoScroll.Should().BeTrue();
                    page.VerticalScroll.Visible.Should().BeTrue();
                    page.ScrollControlIntoView(comboBox);

                    page.ClientRectangle.Contains(comboBox.Bounds).Should().BeTrue();
                    page.ClientRectangle.Contains(label.Bounds).Should().BeTrue();
                    AssertContainersAndButtonsFit(form, page);
                }
            });
        }

        static void AssertContainersAndButtonsFit(OptionForm form, TabPage page)
        {
            var tabs = FindControl<TabControl>(form, "tabOptionsControl");
            var panel = FindControl<Panel>(form, "mainPanel");
            tabs.ClientRectangle.Contains(page.Bounds).Should().BeTrue();
            panel.ClientRectangle.Contains(tabs.Bounds).Should().BeTrue();
            form.ClientRectangle.Contains(panel.Bounds).Should().BeTrue();

            foreach (var name in new[] { "btnOk", "btnCancel", "btnSave", "btnReset" })
            {
                var button = GetButton(form, name);
                form.ClientRectangle.Contains(button.Bounds).Should().BeTrue();
                button.Top.Should().BeGreaterThan(panel.Bottom);
            }
        }

        static void RunOnSta(Action action)
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try
                {
                    ThemeManager.SetThemeMode(ThemeMode.Light);
                    action();
                }
                catch (Exception exception)
                {
                    failure = exception;
                }
                finally
                {
                    ThemeManager.SetThemeMode(ThemeMode.Light);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (failure != null)
            {
                ExceptionDispatchInfo.Capture(failure).Throw();
            }
        }

        static void InvokePrivateMethod(object instance, string methodName, params object[] arguments)
        {
            var method = instance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            method.Should().NotBeNull();
            method.Invoke(instance, arguments);
        }

        static T FindControl<T>(Control root, string name)
            where T : Control
        {
            return root.Controls.Find(name, true).Single().Should().BeOfType<T>().Which;
        }

        static CheckBox GetCheckBox(OptionForm form, string name)
        {
            return FindControl<CheckBox>(form, name);
        }

        static Button GetButton(OptionForm form, string name)
        {
            return FindControl<Button>(form, name);
        }
    }
}
