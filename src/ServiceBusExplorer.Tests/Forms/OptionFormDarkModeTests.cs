using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;

using FluentAssertions;

using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers.Theming;

using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [Collection("Theme UI")]
    public class OptionFormDarkModeTests
    {
        [Fact]
        public void Constructor_UsesSuppliedDarkModeForInitialCheckboxState()
        {
            RunOnSta(() =>
            {
                ThemeManager.SetDarkMode(false);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.DarkMode = true;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    GetCheckBox(form, "darkModeCheckBox").Checked.Should().BeTrue();
                    settings.DarkMode.Should().BeTrue();
                    ThemeManager.DarkMode.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void DarkModeCheckbox_ChangingSelection_DoesNotSwitchGlobalThemeBeforeSave()
        {
            RunOnSta(() =>
            {
                ThemeManager.SetDarkMode(false);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.DarkMode = false;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    var checkBox = GetCheckBox(form, "darkModeCheckBox");

                    checkBox.Checked = true;

                    settings.DarkMode.Should().BeTrue();
                    ThemeManager.DarkMode.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void Cancel_AfterUnsavedDarkModeChange_PreservesGlobalTheme()
        {
            RunOnSta(() =>
            {
                ThemeManager.SetDarkMode(true);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.DarkMode = true;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    form.Shown += (sender, args) =>
                    {
                        form.BeginInvoke(new Action(() =>
                        {
                            GetCheckBox(form, "darkModeCheckBox").Checked = false;
                            ThemeManager.DarkMode.Should().BeTrue();
                            GetButton(form, "btnCancel").PerformClick();
                        }));
                    };

                    form.ShowDialog().Should().Be(DialogResult.Cancel);
                    ThemeManager.DarkMode.Should().BeTrue();
                }
            });
        }

        [Fact]
        public void Reset_SetsDarkModeCheckboxBackToTrue()
        {
            RunOnSta(() =>
            {
                ThemeManager.SetDarkMode(true);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.DarkMode = false;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    GetCheckBox(form, "darkModeCheckBox").Checked.Should().BeFalse();

                    InvokePrivateMethod(form, "btnReset_Click", GetButton(form, "btnReset"), EventArgs.Empty);

                    GetCheckBox(form, "darkModeCheckBox").Checked.Should().BeTrue();
                    settings.DarkMode.Should().BeTrue();
                    ThemeManager.DarkMode.Should().BeTrue();
                }
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GeneralTab_WhenShown_KeepsEveryOptionInsideTheVisiblePage(bool darkMode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetDarkMode(darkMode);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.DarkMode = darkMode;

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

                    var checkBox = GetCheckBox(form, "darkModeCheckBox");
                    var label = FindControl<Label>(form, "lblDarkMode");
                    checkBox.Left.Should().Be(GetCheckBox(form, "useAsciiCheckBox").Left);
                    label.Left.Should().Be(FindControl<Label>(form, "lblUseAscii").Left);
                    checkBox.Top.Should().BeGreaterThan(
                        GetCheckBox(form, "disableAccidentalDeletionPrevention").Bottom);
                    page.VerticalScroll.Visible.Should().BeFalse();

                    AssertContainersAndButtonsFit(form, page);
                }
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GeneralTab_WhenDialogHeightIsReduced_CanScrollToTheWholeDarkModeRow(bool darkMode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetDarkMode(darkMode);

                var settings = new MainSettings();
                settings.SetDefault();
                settings.DarkMode = darkMode;

                using (var form = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                {
                    form.Show();
                    form.ClientSize = new Size(form.ClientSize.Width, form.ClientSize.Height - 80);
                    form.PerformLayout();

                    var page = FindControl<TabPage>(form, "tabPageGeneral");
                    var checkBox = GetCheckBox(form, "darkModeCheckBox");
                    var label = FindControl<Label>(form, "lblDarkMode");

                    page.AutoScroll.Should().BeTrue();
                    page.VerticalScroll.Visible.Should().BeTrue();
                    page.ScrollControlIntoView(checkBox);

                    page.ClientRectangle.Contains(checkBox.Bounds).Should().BeTrue();
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
                    ThemeManager.SetDarkMode(false);
                    action();
                }
                catch (Exception exception)
                {
                    failure = exception;
                }
                finally
                {
                    ThemeManager.SetDarkMode(false);
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
