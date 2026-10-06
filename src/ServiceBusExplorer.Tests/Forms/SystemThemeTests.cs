using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
using FluentAssertions;
using Microsoft.Win32;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.UIHelpers.Theming;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [Collection("Theme UI")]
    public class SystemThemeTests
    {
        [Fact]
        public void FollowOperatingSystem_ReadsWindowsApplicationThemePreference()
        {
            RunOnSta(() =>
            {
                var appsUseLightTheme = Registry.GetValue(
                    @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
                    "AppsUseLightTheme", 1);

                ThemeManager.SetThemeMode(ThemeMode.FollowOperatingSystem);

                ThemeManager.DarkMode.Should().Be(Equals(appsUseLightTheme, 0));
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void FollowOperatingSystem_AppliesCurrentApplicationTheme(bool systemDarkMode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SystemDarkModeProvider = () => systemDarkMode;

                ThemeManager.SetThemeMode(ThemeMode.FollowOperatingSystem);

                ThemeManager.Mode.Should().Be(ThemeMode.FollowOperatingSystem);
                ThemeManager.DarkMode.Should().Be(systemDarkMode);
                ThemeManager.IsDark.Should().Be(systemDarkMode && !SystemInformation.HighContrast);
            });
        }

        [Fact]
        public void PreferencesChanged_WhenFollowingOperatingSystem_UpdatesAllOpenWindowsInBothDirections()
        {
            RunOnSta(() =>
            {
                var systemDarkMode = false;
                ThemeManager.SystemDarkModeProvider = () => systemDarkMode;
                ThemeManager.SetThemeMode(ThemeMode.FollowOperatingSystem);

                using (var first = new ThemedForm { BackColor = Color.Beige })
                using (var second = new ThemedForm { BackColor = Color.LightBlue })
                {
                    first.Show();
                    second.Show();

                    systemDarkMode = true;
                    NotifyPreferencesChanged();

                    ThemeManager.DarkMode.Should().BeTrue();
                    first.BackColor.Should().Be(ThemeManager.Palette.Background);
                    second.BackColor.Should().Be(ThemeManager.Palette.Background);

                    systemDarkMode = false;
                    NotifyPreferencesChanged();

                    ThemeManager.Mode.Should().Be(ThemeMode.FollowOperatingSystem);
                    ThemeManager.DarkMode.Should().BeFalse();
                    if (!SystemInformation.HighContrast)
                    {
                        first.BackColor.Should().Be(Color.Beige);
                        second.BackColor.Should().Be(Color.LightBlue);
                    }
                }
            });
        }

        [Fact]
        public void PreferencesChanged_FromBackgroundThread_UpdatesWindowsOnTheirUiThread()
        {
            RunOnSta(() =>
            {
                var systemDarkMode = false;
                ThemeManager.SystemDarkModeProvider = () => systemDarkMode;
                ThemeManager.SetThemeMode(ThemeMode.FollowOperatingSystem);

                using (var form = new ThemedForm { BackColor = Color.Beige })
                {
                    form.Show();
                    systemDarkMode = true;
                    Exception failure = null;
                    var notificationThread = new Thread(() =>
                    {
                        try
                        {
                            NotifyPreferencesChanged();
                        }
                        catch (Exception exception)
                        {
                            failure = exception;
                        }
                    });
                    notificationThread.Start();
                    notificationThread.Join();
                    if (failure != null)
                        ExceptionDispatchInfo.Capture(failure).Throw();

                    Application.DoEvents();

                    ThemeManager.Mode.Should().Be(ThemeMode.FollowOperatingSystem);
                    ThemeManager.DarkMode.Should().BeTrue();
                    form.BackColor.Should().Be(ThemeManager.Palette.Background);
                }
            });
        }

        [Theory]
        [InlineData(ThemeMode.Light, false)]
        [InlineData(ThemeMode.Dark, true)]
        public void PreferencesChanged_WhenThemeIsExplicit_IgnoresOperatingSystemTheme(ThemeMode mode, bool darkMode)
        {
            RunOnSta(() =>
            {
                ThemeManager.SystemDarkModeProvider = () =>
                    throw new InvalidOperationException("An explicit theme must not read the OS preference.");
                ThemeManager.SetThemeMode(mode);

                NotifyPreferencesChanged();

                ThemeManager.Mode.Should().Be(mode);
                ThemeManager.DarkMode.Should().Be(darkMode);
            });
        }

        [Fact]
        public void SetThemeMode_WhenModeIsInvalid_RejectsItWithoutChangingTheCurrentTheme()
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Light);

                Action selectInvalidMode = () => ThemeManager.SetThemeMode((ThemeMode)99);

                selectInvalidMode.Should().Throw<ArgumentOutOfRangeException>();
                ThemeManager.Mode.Should().Be(ThemeMode.Light);
                ThemeManager.DarkMode.Should().BeFalse();
            });
        }

        static void NotifyPreferencesChanged()
        {
            var handler = typeof(ThemeManager).GetMethod("PreferencesChanged",
                BindingFlags.Static | BindingFlags.NonPublic);
            handler.Should().NotBeNull();
            handler.Invoke(null, new object[] { null, new UserPreferenceChangedEventArgs(UserPreferenceCategory.General) });
        }

        static void RunOnSta(Action action)
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                var originalProvider = ThemeManager.SystemDarkModeProvider;
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
                    ThemeManager.SystemDarkModeProvider = originalProvider;
                    ThemeManager.SetDarkMode(false);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (failure != null)
                ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
