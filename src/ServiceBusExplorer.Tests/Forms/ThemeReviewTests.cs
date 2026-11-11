using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers;
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
                var lightHeaderBackground = Color.FromArgb(215, 228, 242);
                using var host = new Panel { BackColor = lightHeaderBackground };
                var form = (MainForm)FormatterServices.GetUninitializedObject(typeof(MainForm));
                using var link = new LinkLabel { AutoSize = false, Size = new Size(160, 24) };
                using var logo = new PictureBox { Size = new Size(96, 12) };
                host.Controls.Add(link);
                host.Controls.Add(logo);
                ThemeManager.SetHeaderBrandLogo(logo);
                var linkClicked = false;
                link.LinkClicked += (_, _) => linkClicked = true;
                typeof(MainForm).GetField("linkLabelNewVersionAvailable",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(form, link);

                typeof(MainForm).GetMethod("DisplayNewVersionInformation",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.Invoke(form, null);
                ThemeManager.Register(host);

                link.Visible.Should().BeTrue();
                link.Text.Should().Be("Debug Version");
                link.Enabled.Should().BeTrue();
                link.TabStop.Should().BeFalse();
                link.LinkArea.Length.Should().Be(0);
                link.Links.Count.Should().Be(0);

                RaiseControlEvent(link, "OnClick", EventArgs.Empty);
                RaiseControlEvent(link, "OnMouseClick",
                    new MouseEventArgs(MouseButtons.Left, 1, 8, 8, 0));
                linkClicked.Should().BeFalse();

                AssertBrandAppearance(link, logo, ThemeManager.LightHeaderBrandColor, lightHeaderBackground);
                AssertRenderedTextContrast(link, lightHeaderBackground);

                ThemeManager.SetThemeMode(ThemeMode.Dark);
                AssertBrandAppearance(link, logo, Color.White, ThemePalette.Dark.Background);
                AssertRenderedTextContrast(link, ThemePalette.Dark.Background);

                ThemeManager.HighContrastProvider = () => true;
                NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                AssertBrandAppearance(link, logo, SystemColors.WindowText, SystemColors.Control);

                ThemeManager.HighContrastProvider = () => false;
                ThemeManager.SetThemeMode(ThemeMode.Light);
                AssertBrandAppearance(link, logo, ThemeManager.LightHeaderBrandColor, lightHeaderBackground);
            });
        }
#endif

        [Fact]
        public void HeaderBrandLogoAndText_MatchAcrossLightDarkAndHighContrastThemes()
        {
            RunOnSta(() =>
            {
                var lightHeaderBackground = Color.FromArgb(215, 228, 242);
                ColorContrast.Ratio(ThemeManager.LightHeaderBrandColor, lightHeaderBackground)
                    .Should().BeGreaterThanOrEqualTo(4.5);
                ColorContrast.Ratio(Color.White, ThemePalette.Dark.Background)
                    .Should().BeGreaterThanOrEqualTo(4.5);

                using var form = new ThemedForm { BackColor = lightHeaderBackground };
                using var label = new LinkLabel { Text = "Header Status" };
                using var logo = new PictureBox();
                form.Controls.Add(label);
                form.Controls.Add(logo);
                ThemeManager.SetHeaderBrandLogo(logo);
                ThemeManager.SetHeaderBrandText(label);
                ThemeManager.Register(form);

                for (var cycle = 0; cycle < 2; cycle++)
                {
                    AssertBrandAppearance(label, logo, ThemeManager.LightHeaderBrandColor, lightHeaderBackground);

                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    AssertBrandAppearance(label, logo, Color.White, ThemePalette.Dark.Background);

                    ThemeManager.HighContrastProvider = () => true;
                    NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                    AssertBrandAppearance(label, logo, SystemColors.WindowText, SystemColors.Control);

                    ThemeManager.HighContrastProvider = () => false;
                    NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                    AssertBrandAppearance(label, logo, Color.White, ThemePalette.Dark.Background);

                    ThemeManager.SetThemeMode(ThemeMode.Light);
                }
            });
        }

        [Fact]
        public void HeaderBrandForms_UseAzureBlueLogoInLightModeAndWhiteInDarkMode()
        {
            RunOnSta(() =>
            {
                var helper = new ServiceBusHelper((_, _) => { });
                using var connectForm = new ConnectForm(helper, ConfigFileUse.ApplicationConfig, []);
                using var eventGridForm = new EventGridConnectForm();
                var connectLogo = connectForm.Controls.Find("logoPictureBox", true).Single()
                    .Should().BeOfType<PictureBox>().Which;
                var eventGridLogo = eventGridForm.Controls.Find("pbAzure", true).Single()
                    .Should().BeOfType<PictureBox>().Which;

                ThemeManager.Register(connectForm);
                ThemeManager.Register(eventGridForm);
                AssertLogoTint(connectLogo, ThemeManager.LightHeaderBrandColor);
                AssertLogoTint(eventGridLogo, ThemeManager.LightHeaderBrandColor);

                ThemeManager.SetThemeMode(ThemeMode.Dark);
                AssertLogoTint(connectLogo, Color.White);
                AssertLogoTint(eventGridLogo, Color.White);

                ThemeManager.SetThemeMode(ThemeMode.Light);
                AssertLogoTint(connectLogo, ThemeManager.LightHeaderBrandColor);
                AssertLogoTint(eventGridLogo, ThemeManager.LightHeaderBrandColor);
            });
        }

        [Fact]
        public void SetHeaderBrandHelpers_RejectNullAndForeignThreads()
        {
            RunOnSta(() =>
            {
                Action nullLogo = () => ThemeManager.SetHeaderBrandLogo(null);
                Action nullText = () => ThemeManager.SetHeaderBrandText(null);
                nullLogo.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("pictureBox");
                nullText.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("control");

                using var logo = new PictureBox();
                using var label = new Label();
                ThemeManager.Register(logo);
                ThemeManager.Register(label);

                RunOnWorker(() =>
                {
                    Action crossThreadLogo = () => ThemeManager.SetHeaderBrandLogo(logo);
                    Action crossThreadText = () => ThemeManager.SetHeaderBrandText(label);
                    crossThreadLogo.Should().Throw<InvalidOperationException>()
                        .WithMessage("Apply themes on the control's UI thread.");
                    crossThreadText.Should().Throw<InvalidOperationException>()
                        .WithMessage("Apply themes on the control's UI thread.");
                }, true);
            });
        }

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
        public void ThemePreferenceChanges_WithUnchangedPalette_DoNotReapplyWindows(UserPreferenceCategory category)
        {
            RunOnSta(() =>
            {
                foreach (var mode in new[] { ThemeMode.Dark, ThemeMode.Light, ThemeMode.FollowOperatingSystem })
                {
                    ThemeManager.SystemDarkThemeProvider = () => true;
                    ThemeManager.SetThemeMode(mode);
                    using var panel = new ThemeCountingPanel();
                    ThemeManager.Register(panel);
                    panel.Applications = 0;

                    NotifyPreferencesChanged(category);

                    panel.Applications.Should().Be(0, "the effective palette did not change in {0}", mode);
                }
            });
        }

        [Fact]
        public void RegisteredHost_IsRefreshedOnceWhenCoveredAndStillWorksWhenDetached()
        {
            RunOnSta(() =>
            {
                using var form = new ThemedForm();
                using var host = new ThemeCountingPanel();
                form.Controls.Add(host);
                ThemeManager.Register(host);
                host.IsHandleCreated.Should().BeFalse();
                ThemeManager.ReplaceHostedContent(host, () => new Button(), null);
                form.Show();

                host.Applications = 0;
                ThemeManager.SetThemeMode(ThemeMode.Dark);
                host.Applications.Should().Be(1);

                host.Applications = 0;
                RunOnWorker(() => ThemeManager.SetThemeMode(ThemeMode.Light), false);
                host.Applications.Should().Be(0);
                Application.DoEvents();
                host.Applications.Should().Be(1, "queued refreshes must also skip covered roots");

                RunOnWorker(() =>
                {
                    Action replace = () => ThemeManager.ReplaceHostedContent(host, () => new Button(), null);
                    replace.Should().Throw<InvalidOperationException>();
                }, true);

                form.Controls.Remove(host);
                form.Dispose();
                host.Applications = 0;
                ThemeManager.SetThemeMode(ThemeMode.Light);
                host.Applications.Should().Be(1);
                host.IsDisposed.Should().BeFalse();
                ThemeManager.ReplaceHostedContent(host, () => new Label(), null).Parent.Should().BeSameAs(host);
            });
        }

        [Fact]
        public void ThemedForm_LoadSeesThemedControlsAndLoadCreatedControlsRestoreInheritedColors()
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Dark);
                using var form = new ThemedForm { BackColor = Color.Beige, ForeColor = Color.Maroon };
                using var inherited = new Label();
                form.Controls.Add(inherited);
                using var late = new Panel();
                using var lateLabel = new Label { BackColor = Color.LightBlue };
                late.Controls.Add(lateLabel);
                Color loadBackground = Color.Empty;
                Color loadForeground = Color.Empty;
                form.Load += (_, _) =>
                {
                    loadBackground = inherited.BackColor;
                    loadForeground = inherited.ForeColor;
                    form.Controls.Add(late);
                };

                form.Show();

                loadBackground.Should().Be(ThemePalette.Dark.Background);
                loadForeground.Should().Be(ThemeManager.ReadableText(Color.Maroon));
                lateLabel.BackColor.Should().Be(ThemePalette.Dark.Background);
                ThemeManager.SetThemeMode(ThemeMode.Light);
                form.BackColor.Should().Be(Color.Beige);
                inherited.BackColor.Should().Be(Color.Beige);
                inherited.ForeColor.Should().Be(Color.Maroon);
                late.BackColor.Should().Be(Color.Beige);
                lateLabel.BackColor.Should().Be(Color.LightBlue);
                TypeDescriptor.GetProperties(inherited)["BackColor"].ShouldSerializeValue(inherited).Should().BeFalse();
                TypeDescriptor.GetProperties(late)["BackColor"].ShouldSerializeValue(late).Should().BeFalse();
                Application.DoEvents();
                lateLabel.BackColor.Should().Be(Color.LightBlue);
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DialogButtons_PreserveHoverPressedFeedbackAndRestoreLightAppearance(bool highContrast)
        {
            RunOnSta(() =>
            {
                using var form = new TextForm("Message", "payload");
                form.Show();
                var button = (Button)form.Controls.Find("btnOk", true)[0];
                var originalStyle = button.FlatStyle;
                var originalForeground = button.ForeColor;
                ThemeManager.HighContrastProvider = () => highContrast;
                ThemeManager.SetThemeMode(ThemeMode.Dark);

                RaiseControlEvent(button, "OnMouseEnter", EventArgs.Empty);
                if (highContrast)
                {
                    // Native high-contrast buttons own their hot/pressed colors, not hardcoded white text.
                    button.FlatStyle.Should().Be(FlatStyle.System);
                    button.UseVisualStyleBackColor.Should().BeTrue();
                    button.ForeColor.ToArgb().Should().Be(SystemColors.WindowText.ToArgb());
                }
                else
                {
                    button.ForeColor.Should().Be(ThemePalette.Dark.SelectionText);
                    button.FlatAppearance.MouseOverBackColor.Should().Be(ThemePalette.Dark.Hover);
                    button.FlatAppearance.MouseDownBackColor.Should().Be(ThemePalette.Dark.Selection);
                }
                RaiseControlEvent(button, "OnMouseDown", new MouseEventArgs(MouseButtons.Left, 1, 2, 2, 0));
                ThemeManager.Apply(button);
                button.ForeColor.ToArgb().Should().Be((highContrast ? SystemColors.WindowText : ThemePalette.Dark.SelectionText).ToArgb());
                RaiseControlEvent(button, "OnMouseUp", new MouseEventArgs(MouseButtons.Left, 1, 2, 2, 0));
                RaiseControlEvent(button, "OnMouseLeave", EventArgs.Empty);
                button.ForeColor.ToArgb().Should().Be(ThemeManager.Palette.Text.ToArgb());
                button.Enabled = false;
                button.ForeColor.Should().Be(ThemeManager.Palette.MutedText);
                button.Enabled = true;

                ThemeManager.HighContrastProvider = () => false;
                ThemeManager.SetThemeMode(ThemeMode.Light);
                button.FlatStyle.Should().Be(originalStyle);
                button.ForeColor.Should().Be(originalForeground);
                RaiseControlEvent(button, "OnMouseEnter", EventArgs.Empty);
                button.ForeColor.Should().Be(Color.White);
                RaiseControlEvent(button, "OnMouseLeave", EventArgs.Empty);
                button.ForeColor.Should().Be(SystemColors.ControlText);
            });
        }

        [Fact]
        public void ButtonRuntimeSemanticColor_ExplicitNeutralResetRestoresTheDefaultForeground()
        {
            RunOnSta(() =>
            {
                using var button = new Button();
                ThemeManager.Register(button);
                ThemeManager.SetThemeMode(ThemeMode.Dark);

                foreach (var disabled in new[] { false, true })
                {
                    button.ForeColor = Color.Red;
                    button.ForeColor.Should().Be(ThemePalette.Dark.ErrorText);
                    button.Enabled = !disabled;

                    button.ForeColor = SystemColors.ControlText;
                    button.Enabled = true;

                    button.ForeColor.Should().Be(ThemePalette.Dark.Text);
                    ThemeManager.Apply(button);
                    button.ForeColor.Should().Be(ThemePalette.Dark.Text);
                }

                ThemeManager.SetThemeMode(ThemeMode.Light);
                button.ForeColor.Should().Be(SystemColors.ControlText);
            });
        }

        [Fact]
        public void ButtonRuntimeSemanticColor_SurvivesHoverDisablingAndHighContrast()
        {
            RunOnSta(() =>
            {
                using var button = new Button { ForeColor = Color.DarkOrange };
                button.MouseEnter += (_, _) => button.ForeColor = Color.White;
                button.MouseLeave += (_, _) => button.ForeColor = SystemColors.ControlText;
                ThemeManager.Register(button);
                ThemeManager.SetThemeMode(ThemeMode.Dark);
                button.ForeColor = Color.Red;
                RaiseControlEvent(button, "OnMouseEnter", EventArgs.Empty);
                RaiseControlEvent(button, "OnMouseLeave", EventArgs.Empty);
                button.ForeColor.Should().Be(ThemePalette.Dark.ErrorText);
                button.Enabled = false;
                button.Enabled = true;
                button.ForeColor.Should().Be(ThemePalette.Dark.ErrorText);

                ThemeManager.HighContrastProvider = () => true;
                NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                button.ForeColor.Should().Be(SystemColors.WindowText);
                button.ForeColor = Color.Green;
                button.ForeColor.Should().Be(SystemColors.WindowText);
                ThemeManager.HighContrastProvider = () => false;
                NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                button.ForeColor.Should().Be(ThemePalette.Dark.SuccessText);
                ThemeManager.SetThemeMode(ThemeMode.Light);
                button.ForeColor.Should().Be(Color.DarkOrange);
            });
        }

        sealed class ThemeCountingPanel : Panel, IThemeAware
        {
            public int Applications { get; set; }
            public void ApplyTheme() => Applications++;
        }

        static void RaiseControlEvent(Control control, string method, EventArgs args) =>
            typeof(Control).GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(control, [args]);

        [Theory]
        [InlineData(true, 0, 0, 1, null)]
        [InlineData(false, 0, 0, 1, null)]
        [InlineData(true, unchecked((int)0x80070057), 0, 2, null)]
        [InlineData(false, unchecked((int)0x80070057), 0, 2, null)]
        [InlineData(true, unchecked((int)0x80070057), unchecked((int)0x80070057), 2, "80070057")]
        [InlineData(true, unchecked((int)0x80070057), unchecked((int)0x80004005), 2, "80004005")]
        [InlineData(true, unchecked((int)0x80004005), 0, 1, "80004005")]
        public void Caption_UsesLegacyAttributeOnlyWhenModernAttributeIsUnsupported(
            bool dark, int modernResult, int legacyResult, int expectedCalls, string diagnostic)
        {
            RunOnSta(() =>
            {
                using var form = new Form();
                var handle = form.Handle;
                var calls = new List<int>();
                using var output = new StringWriter();
                using var listener = new TextWriterTraceListener(output);
                Trace.Listeners.Add(listener);
                try
                {
                    ThemeNativeMethods.ApplyCaption(form, dark, (window, attribute, enabled) =>
                    {
                        window.Should().Be(handle);
                        enabled.Should().Be(dark ? 1 : 0);
                        calls.Add(attribute);
                        return attribute == 20 ? modernResult : legacyResult;
                    });
                    calls.Should().Equal(expectedCalls == 1 ? new[] { 20 } : new[] { 20, 19 });
                    if (diagnostic == null)
                        output.ToString().Should().BeEmpty();
                    else
                        output.ToString().Should().Contain("HRESULT 0x" + diagnostic);
                }
                finally
                {
                    Trace.Listeners.Remove(listener);
                }
            });
        }

        [Fact]
        public void Caption_DoesNotCreateAHandleOrHideUnexpectedFailures()
        {
            RunOnSta(() =>
            {
                using var form = new Form();
                var expected = new InvalidOperationException("Unexpected DWM failure");
                Action apply = () => ThemeNativeMethods.ApplyCaption(form, true, (_, _, _) => throw expected);
                apply.Should().NotThrow();
                form.IsHandleCreated.Should().BeFalse();
                _ = form.Handle;
                apply.Should().Throw<InvalidOperationException>().Which.Should().BeSameAs(expected);
            });
        }

        [Theory]
        [InlineData(ThemeMode.Light)]
        [InlineData(ThemeMode.Dark)]
        [InlineData(ThemeMode.FollowOperatingSystem)]
        public void Preferences_RefreshOnlyForEffectiveTransitionsIncludingHighContrastColors(ThemeMode mode)
        {
            RunOnSta(() =>
            {
                var systemDark = false;
                var highContrast = false;
                var systemColors = new[] { Color.White.ToArgb(), Color.Black.ToArgb() };
                ThemeManager.SystemDarkThemeProvider = () => systemDark;
                ThemeManager.HighContrastProvider = () => highContrast;
                ThemeManager.SystemColorsProvider = () => systemColors;
                ThemeManager.SetThemeMode(mode);
                using var panel = new ThemeCountingPanel { BackColor = Color.Beige };
                ThemeManager.Register(panel);
                panel.Applications = 0;

                systemDark = true;
                NotifyPreferencesChanged(UserPreferenceCategory.General);
                panel.Applications.Should().Be(mode == ThemeMode.FollowOperatingSystem ? 1 : 0);
                panel.BackColor.Should().Be(mode == ThemeMode.Light ? Color.Beige : ThemePalette.Dark.Background);

                highContrast = true;
                panel.Applications = 0;
                NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                panel.Applications.Should().Be(1);
                panel.BackColor.Should().Be(SystemColors.Control);
                panel.Applications = 0;
                NotifyPreferencesChanged(UserPreferenceCategory.General);
                panel.Applications.Should().Be(0);

                systemColors[0] = Color.Yellow.ToArgb();
                NotifyPreferencesChanged(UserPreferenceCategory.Color);
                panel.Applications.Should().Be(1, "an HC palette edit must refresh even while HC stays enabled");
                NotifyPreferencesChanged(UserPreferenceCategory.Color);
                panel.Applications.Should().Be(1, "identical palette values must not refresh twice");

                systemDark = false;
                NotifyPreferencesChanged(UserPreferenceCategory.General);
                panel.Applications.Should().Be(1, "HC still overrides the underlying dark/light preference");
                highContrast = false;
                panel.Applications = 0;
                NotifyPreferencesChanged(UserPreferenceCategory.Accessibility);
                panel.Applications.Should().Be(1);
                panel.BackColor.Should().Be(mode == ThemeMode.Dark ? ThemePalette.Dark.Background : Color.Beige);

                panel.Applications = 0;
                systemColors[0] = Color.White.ToArgb();
                NotifyPreferencesChanged(UserPreferenceCategory.Color);
                panel.Applications.Should().Be(mode == ThemeMode.Dark ? 0 : 1,
                    "light controls can use system colors, but the explicit dark palette is fixed");
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

        static void AssertBrandAppearance(Control text, PictureBox logo, Color expectedForeground, Color expectedBackground)
        {
            text.ForeColor.ToArgb().Should().Be(expectedForeground.ToArgb());
            text.BackColor.ToArgb().Should().Be(expectedBackground.ToArgb());
            logo.BackColor.ToArgb().Should().Be(expectedBackground.ToArgb());
            AssertLogoTint(logo, expectedForeground);
        }

        static void AssertLogoTint(PictureBox logo, Color expectedColor)
        {
            var bitmap = logo.BackgroundImage.Should().BeOfType<Bitmap>().Subject;
            var opaquePixels = new List<Color>();
            var hasTransparentPixels = false;
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    if (pixel.A == 0)
                        hasTransparentPixels = true;
                    else
                        opaquePixels.Add(pixel);
                }
            }

            hasTransparentPixels.Should().BeTrue();
            opaquePixels.Should().NotBeEmpty();
            opaquePixels.Should().OnlyContain(pixel =>
                pixel.R == expectedColor.R &&
                pixel.G == expectedColor.G &&
                pixel.B == expectedColor.B);
        }

        static void AssertRenderedTextContrast(Control control, Color background)
        {
            using var bitmap = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(bitmap, new Rectangle(Point.Empty, control.Size));
            var maxContrast = 1.0;
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    var contrast = ColorContrast.Ratio(bitmap.GetPixel(x, y), background);
                    if (contrast > maxContrast)
                        maxContrast = contrast;
                }
            }

            maxContrast.Should().BeGreaterThanOrEqualTo(4.5);
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
                var originalColorsProvider = ThemeManager.SystemColorsProvider;
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
                    ThemeManager.SystemColorsProvider = originalColorsProvider;
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
