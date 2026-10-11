using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Security;
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
    public class ThemeReviewTests
    {
        [Fact]
        public void HostedBackground_IsConsistentWhenEmptyOrPopulatedAcrossThemeSwitches()
        {
            RunOnSta(() =>
            {
                using var host = new Panel();
                host.BackColor = Color.LightBlue;
                ThemeManager.Register(host);
                MainForm.SetEmptyMainPanelBackground(host);
                host.BackColor.Should().Be(ThemeManager.IsThemed
                    ? ThemeManager.Palette.Background : SystemColors.Window);

                ThemeManager.ReplaceHostedContent(host, () => new UserControl(), null);
                host.BackColor.Should().Be(ThemeManager.IsThemed
                    ? ThemeManager.Palette.Background : SystemColors.Window);

                for (var cycle = 0; cycle < 2; cycle++)
                {
                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    host.BackColor.Should().Be(ThemeManager.Palette.Background);
                    host.Controls[0].Dispose();
                    host.Controls.Clear();
                    MainForm.SetEmptyMainPanelBackground(host);
                    ThemeManager.SetThemeMode(ThemeMode.Light);
                    host.BackColor.Should().Be(ThemeManager.IsThemed
                        ? ThemeManager.Palette.Background : SystemColors.Window);
                    ThemeManager.ReplaceHostedContent(host, () => new UserControl(), null);
                }
            });
        }

#if DEBUG
        [Fact]
        public void DebugVersionInformation_IsVisibleButCannotOpenAnUpgradePrompt()
        {
            RunOnSta(() =>
            {
                var form = (MainForm)FormatterServices.GetUninitializedObject(typeof(MainForm));
                using var link = new LinkLabel();
                typeof(MainForm).GetField("linkLabelNewVersionAvailable",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(form, link);

                typeof(MainForm).GetMethod("DisplayNewVersionInformation",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(form, null);

                link.Visible.Should().BeTrue();
                link.Text.Should().Be("Debug Version");
                link.Enabled.Should().BeFalse();
            });
        }
#endif

        [Theory]
        [InlineData(typeof(SecurityException))]
        [InlineData(typeof(UnauthorizedAccessException))]
        [InlineData(typeof(IOException))]
        public void SystemThemeRead_WhenRegistryIsUnavailable_UsesLightTheme(Type exceptionType)
        {
            RunOnSta(() =>
            {
                var exception = (Exception)Activator.CreateInstance(exceptionType);
                ReadSystemTheme(() => throw exception).Should().BeFalse();
            });
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(null, false)]
        public void SystemThemeRead_UsesTheWindowsApplicationPreference(object value, bool dark)
        {
            RunOnSta(() => ReadSystemTheme(() => value).Should().Be(dark));
        }

        [Fact]
        public void SystemThemeRead_DoesNotHideUnexpectedFailures()
        {
            RunOnSta(() =>
            {
                var expected = new InvalidOperationException("Unexpected preference reader failure.");
                Action read = () => ReadSystemTheme(() => throw expected);
                read.Should().Throw<TargetInvocationException>().Which.InnerException.Should().BeSameAs(expected);
            });
        }

        [Theory]
        [InlineData(UserPreferenceCategory.Desktop)]
        [InlineData(UserPreferenceCategory.Keyboard)]
        [InlineData(UserPreferenceCategory.Mouse)]
        [InlineData(UserPreferenceCategory.Locale)]
        [InlineData(UserPreferenceCategory.Power)]
        public void UnrelatedPreferenceChanges_DoNotReadTheThemeOrRepaintWindows(UserPreferenceCategory category)
        {
            RunOnSta(() =>
            {
                ThemeManager.SystemDarkThemeProvider = () => true;
                ThemeManager.SetThemeMode(ThemeMode.FollowOperatingSystem);
                using (var panel = new Panel())
                {
                    ThemeManager.Register(panel);
                    panel.BackColor = Color.Orange;
                    ThemeManager.SystemDarkThemeProvider = () =>
                        throw new InvalidOperationException("Unrelated preferences must not read the theme.");

                    NotifyPreferencesChanged(category);

                    panel.BackColor.Should().Be(Color.Orange);
                }
            });
        }

        [Theory]
        [InlineData(UserPreferenceCategory.Color)]
        [InlineData(UserPreferenceCategory.General)]
        [InlineData(UserPreferenceCategory.Accessibility)]
        [InlineData(UserPreferenceCategory.VisualStyle)]
        public void ThemePreferenceChanges_StillRepaintWindows(UserPreferenceCategory category)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Dark);
                using (var panel = new Panel())
                {
                    ThemeManager.Register(panel);
                    panel.BackColor = Color.Orange;

                    NotifyPreferencesChanged(category);

                    panel.BackColor.Should().Be(ThemeManager.Palette.Background);
                }
            });
        }

        [Fact]
        public void Apply_KeepsTheThemeLockedWhileChangingInheritedParentColors()
        {
            RunOnSta(() =>
            {
                using var host = new Panel();
                host.BackColor = Color.Beige;
                using var child = new Label();
                host.Controls.Add(child);
                ThemeManager.Register(host);
                host.BackColor = Color.Beige;
                var gate = typeof(ThemeManager).GetField("themeLock",
                    BindingFlags.Static | BindingFlags.NonPublic)
                    ?.GetValue(null);
                bool? workerAcquiredLock = null;
                host.BackColorChanged += (_, _) =>
                {
                    var worker = new Thread(() =>
                    {
                        var entered = Monitor.TryEnter(gate);
                        workerAcquiredLock = entered;
                        if (entered)
                            Monitor.Exit(gate);
                    });
                    worker.Start();
                    worker.Join(TimeSpan.FromSeconds(5)).Should().BeTrue();
                };

                ThemeManager.SetThemeMode(ThemeMode.Dark);

                workerAcquiredLock.Should().BeFalse();
                child.BackColor.Should().Be(ThemeManager.Palette.Background);
                ThemeManager.SetThemeMode(ThemeMode.Light);
                host.BackColor.Should().Be(ThemeManager.IsThemed
                    ? ThemeManager.Palette.Background : Color.Beige);
            });
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public void ReplaceHostedContent_RejectsForeignThreadsBeforeChangingTheHost(bool createHandle, bool staWorker)
        {
            RunOnSta(() =>
            {
                using var host = new Panel();
                using var original = new UserControl();
                host.Controls.Add(original);
                ThemeManager.Register(host);
                if (createHandle)
                    _ = host.Handle;
                var factoryCalled = false;

                RunOnWorker(() =>
                {
                    Action replace = () => ThemeManager.ReplaceHostedContent(host, () =>
                    {
                        factoryCalled = true;
                        return new UserControl();
                    }, null);
                    replace.Should().Throw<InvalidOperationException>()
                        .WithMessage("Apply themes on the control's UI thread.");
                }, staWorker);

                factoryCalled.Should().BeFalse();
                host.IsHandleCreated.Should().Be(createHandle);
                original.IsDisposed.Should().BeFalse();
                original.Parent.Should().BeSameAs(host);
            });
        }

        [Fact]
        public void ReplaceHostedContent_RequiresRegistrationForHandlelessHosts()
        {
            RunOnSta(() =>
            {
                using var host = new Panel();
                using var original = new UserControl();
                host.Controls.Add(original);
                Action replace = () => ThemeManager.ReplaceHostedContent(host, () => new UserControl(), null);

                replace.Should().Throw<InvalidOperationException>()
                    .WithMessage("Register handleless hosts on their UI thread before replacing content.");
                host.IsHandleCreated.Should().BeFalse();
                original.IsDisposed.Should().BeFalse();
                original.Parent.Should().BeSameAs(host);
            });
        }

        [Theory]
        [InlineData("DarkOrange", true)]
        [InlineData("Gold", true)]
        [InlineData("OrangeRed", false)]
        [InlineData("Red", false)]
        [InlineData("LightPink", false)]
        public void SemanticColors_AgreeBetweenTextAndGridBackgrounds(string colorName, bool warning)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Dark);
                var color = Color.FromName(colorName);
                using var grid = new DataGridView();
                var style = new DataGridViewCellStyle { BackColor = color, ForeColor = color };
                var args = new DataGridViewCellFormattingEventArgs(0, 0, "Status", typeof(string), style);
                typeof(ThemeManager).GetMethod("FormatCell",
                    BindingFlags.Static | BindingFlags.NonPublic)
                    ?.Invoke(null, [grid, args]);

                style.BackColor.Should().Be(warning
                    ? ThemeManager.Palette.WarningBackground : ThemeManager.Palette.ErrorBackground);
                style.ForeColor.Should().Be(ThemeManager.ReadableText(color));
                style.ForeColor.Should().Be(ThemeManager.IsHighContrast
                    ? ThemeManager.Palette.Text
                    : warning ? ThemeManager.Palette.WarningText : ThemeManager.Palette.ErrorText);
            });
        }

        [Fact]
        public void Buttons_UseNativeHighContrastRenderingAndRestoreAcrossThemeSwitches()
        {
            RunOnSta(() =>
            {
                using var button = new Button
                {
                    FlatStyle = FlatStyle.Popup,
                    UseVisualStyleBackColor = false,
                    BackColor = Color.Beige
                };
                button.FlatAppearance.MouseOverBackColor = Color.Cyan;
                button.FlatAppearance.MouseDownBackColor = Color.Magenta;
                ThemeManager.Register(button);

                for (var cycle = 0; cycle < 2; cycle++)
                {
                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    button.FlatStyle.Should().Be(FlatStyle.Flat);
                    button.FlatAppearance.MouseOverBackColor.Should().Be(ThemePalette.Dark.Hover);
                    button.FlatAppearance.MouseDownBackColor.Should().Be(ThemePalette.Dark.Selection);

                    ThemeManager.HighContrastProvider = () => true;
                    NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                    button.FlatStyle.Should().Be(FlatStyle.System);
                    button.UseVisualStyleBackColor.Should().BeTrue();
                    ThemeManager.IsDark.Should().BeFalse();

                    ThemeManager.SetThemeMode(ThemeMode.Light);
                    button.FlatStyle.Should().Be(FlatStyle.System);
                    ThemeManager.HighContrastProvider = () => false;
                    NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                    button.FlatStyle.Should().Be(FlatStyle.Popup);
                    button.UseVisualStyleBackColor.Should().BeFalse();
                    button.BackColor.Should().Be(Color.Beige);
                    button.FlatAppearance.MouseOverBackColor.Should().Be(Color.Cyan);
                    button.FlatAppearance.MouseDownBackColor.Should().Be(Color.Magenta);
                }
            });
        }

        [Fact]
        public void ActiveLinks_UseTheNormalHighContrastBackgroundColorPairAndRestore()
        {
            RunOnSta(() =>
            {
                using var link = new LinkLabel { ActiveLinkColor = Color.Yellow };
                ThemeManager.Register(link);
                ThemeManager.SetThemeMode(ThemeMode.Dark);
                link.ActiveLinkColor.Should().Be(ThemePalette.Dark.SelectionText);

                ThemeManager.HighContrastProvider = () => true;
                NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                link.BackColor.Should().Be(SystemColors.Control);
                link.ActiveLinkColor.Should().Be(SystemColors.ControlText);

                ThemeManager.HighContrastProvider = () => false;
                NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                link.ActiveLinkColor.Should().Be(ThemePalette.Dark.SelectionText);
                ThemeManager.SetThemeMode(ThemeMode.Light);
                link.ActiveLinkColor.Should().Be(Color.Yellow);
            });
        }

        [Theory]
        [InlineData(false, false, false)]
        [InlineData(false, false, true)]
        [InlineData(false, true, false)]
        [InlineData(false, true, true)]
        [InlineData(true, false, false)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        [InlineData(true, true, true)]
        public void ComboDrawing_DisplaysAmpersandsLiterally(bool highContrast, bool selected, bool editArea)
        {
            RunOnSta(() =>
            {
                ThemeManager.HighContrastProvider = () => highContrast;
                ThemeManager.SetThemeMode(ThemeMode.Dark);
                using var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
                combo.Items.Add("A&B && C");
                combo.SelectedIndex = 0;
                ThemeManager.Register(combo);
                combo.DrawMode.Should().Be(DrawMode.OwnerDrawFixed);
                using var image = new Bitmap(180, 40);
                using var expected = new Bitmap(180, 40);
                using var graphics = Graphics.FromImage(image);
                using var expectedGraphics = Graphics.FromImage(expected);
                var bounds = new Rectangle(Point.Empty, image.Size);
                var args = new DrawItemEventArgs(graphics, combo.Font, bounds, editArea ? -1 : 0,
                    selected ? DrawItemState.Selected : DrawItemState.None);

                typeof(ComboBox).GetMethod("OnDrawItem", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(combo, [args]);

                expectedGraphics.Clear(selected ? ThemeManager.Palette.Selection : ThemeManager.Palette.Surface);
                TextRenderer.DrawText(expectedGraphics, combo.GetItemText(combo.Items[0]), combo.Font,
                    Rectangle.Inflate(bounds, -2, 0),
                    selected ? ThemeManager.Palette.SelectionText : ThemeManager.Palette.Text,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                for (var y = 0; y < image.Height; y++)
                    for (var x = 0; x < image.Width; x++)
                        image.GetPixel(x, y).Should().Be(expected.GetPixel(x, y));
            });
        }

        static bool ReadSystemTheme(Func<object> readPreference)
        {
            var method = typeof(ThemeManager).GetMethod("ReadSystemDarkTheme",
                BindingFlags.Static | BindingFlags.NonPublic, null, [typeof(Func<object>)], null);
            method.Should().NotBeNull();
            return method != null && (bool)method.Invoke(null, [readPreference]);
        }

        static void NotifyPreferencesChanged(UserPreferenceCategory category)
        {
            typeof(ThemeManager).GetMethod("PreferencesChanged",
                BindingFlags.Static | BindingFlags.NonPublic)
                ?.Invoke(null,
                    [null, new UserPreferenceChangedEventArgs(category)]);
        }

        static void RunOnSta(Action action)
        {
            RunOnWorker(() =>
            {
                var originalProvider = ThemeManager.SystemDarkThemeProvider;
                var originalHighContrastProvider = ThemeManager.HighContrastProvider;
                try
                {
                    ThemeManager.HighContrastProvider = () => false;
                    ThemeManager.SetThemeMode(ThemeMode.Light);
                    action();
                }
                finally
                {
                    ThemeManager.SystemDarkThemeProvider = originalProvider;
                    ThemeManager.HighContrastProvider = originalHighContrastProvider;
                    ThemeManager.SetThemeMode(ThemeMode.Light);
                }
            }, true);
        }

        static void RunOnWorker(Action action, bool sta)
        {
            Exception failure = null;
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception exception)
                {
                    failure = exception;
                }
            });
            thread.SetApartmentState(sta ? ApartmentState.STA : ApartmentState.MTA);
            thread.Start();
            thread.Join();
            if (failure != null)
                ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
