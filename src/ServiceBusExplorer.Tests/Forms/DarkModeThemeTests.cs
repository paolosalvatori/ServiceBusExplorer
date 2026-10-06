using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FastColoredTextBoxNS;
using TextStyle = FastColoredTextBoxNS.TextStyle;
using FluentAssertions;
using Microsoft.ServiceBus.Messaging;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;
using ServiceBusExplorer.UIHelpers.Theming;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [CollectionDefinition("Theme UI", DisableParallelization = true)]
    public class ThemeUiCollection { }

    [Collection("Theme UI")]
    public class DarkModeThemeTests
    {
        [Fact]
        public void Toggle_RestoresOriginalAndInheritedColorsAcrossRepeatedCycles()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm { BackColor = Color.Beige, ForeColor = Color.Navy })
                using (var panel = new Panel())
                using (var label = new Label { Text = "Inherited" })
                using (var button = new Button { BackColor = Color.LightBlue, FlatStyle = FlatStyle.System })
                {
                    panel.Controls.Add(label);
                    form.Controls.Add(panel);
                    form.Controls.Add(button);
                    var labelBack = label.BackColor;
                    var labelFore = label.ForeColor;
                    var visualStyle = button.UseVisualStyleBackColor;
                    ThemeManager.Register(form);
                    for (var i = 0; i < 3; i++)
                    {
                        ThemeManager.SetDarkMode(true);
                        form.BackColor.Should().Be(ThemeManager.Palette.Background);
                        button.FlatStyle.Should().Be(FlatStyle.Flat);
                        ThemeManager.SetDarkMode(false);
                        form.BackColor.Should().Be(Color.Beige);
                        label.BackColor.Should().Be(labelBack);
                        label.ForeColor.Should().Be(labelFore);
                        button.BackColor.Should().Be(Color.LightBlue);
                        button.FlatStyle.Should().Be(FlatStyle.System);
                        button.UseVisualStyleBackColor.Should().Be(visualStyle);
                    }
                    form.BackColor = Color.Pink;
                    label.BackColor.Should().Be(Color.Pink);
                }
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void VersionChecker_HidesBackgroundImageAndRestoresLightAppearance(bool initiallyDark)
        {
            RunOnSta(() =>
            {
                using (var form = new NewVersionAvailableForm())
                {
                    var backgroundImage = form.BackgroundImage;
                    var backColor = form.BackColor;
                    var originalColors = form.Controls.Cast<Control>().ToDictionary(
                        control => control, control => new { control.BackColor, control.ForeColor });
                    var link = form.Controls.OfType<LinkLabel>().Single();
                    var linkColor = link.LinkColor;
                    backgroundImage.Should().NotBeNull();
                    ThemeManager.SetDarkMode(initiallyDark);
                    ThemeManager.Register(form);

                    for (var i = 0; i < 3; i++)
                    {
                        ThemeManager.SetDarkMode(true);
                        form.BackgroundImage.Should().BeNull();
                        form.BackColor.Should().Be(ThemeManager.Palette.Background);
                        foreach (Control control in form.Controls)
                        {
                            control.BackColor.Should().Be(ThemeManager.Palette.Background);
                            Contrast(control.ForeColor, control.BackColor).Should().BeGreaterThanOrEqualTo(4.5);
                        }
                        Contrast(link.LinkColor, link.BackColor).Should().BeGreaterThanOrEqualTo(4.5);
                        using (var image = new Bitmap(form.Width, form.Height))
                        {
                            form.DrawToBitmap(image, new Rectangle(Point.Empty, form.Size));
                            image.GetPixel(form.Width - 24, form.Height - 24).ToArgb()
                                .Should().Be(ThemeManager.Palette.Background.ToArgb());
                        }

                        ThemeManager.SetDarkMode(false);
                        form.BackgroundImage.Should().BeSameAs(backgroundImage);
                        form.BackgroundImageLayout.Should().Be(ImageLayout.Stretch);
                        form.BackColor.Should().Be(backColor);
                        foreach (var original in originalColors)
                        {
                            original.Key.BackColor.Should().Be(original.Value.BackColor);
                            original.Key.ForeColor.Should().Be(original.Value.ForeColor);
                        }
                        link.LinkColor.Should().Be(linkColor);
                    }
                }
            });
        }

        [Fact]
        public void FormBackgroundImages_AreHiddenWithoutHidingPictureBoxImages()
        {
            RunOnSta(() =>
            {
                using (var image = new Bitmap(20, 20))
                using (var form = new ThemedForm { BackgroundImage = image })
                using (var picture = new PictureBox { BackgroundImage = image, Image = image })
                {
                    form.Controls.Add(picture);
                    ThemeManager.Register(form);
                    ThemeManager.SetDarkMode(true);
                    form.BackgroundImage.Should().BeNull();
                    picture.BackgroundImage.Should().BeSameAs(image);
                    picture.Image.Should().BeSameAs(image);
                    ThemeManager.SetDarkMode(false);
                    form.BackgroundImage.Should().BeSameAs(image);
                }
            });
        }

        [Fact]
        public void DynamicControls_UseThemeAndRestoreTheirOwnOriginalColors()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm())
                {
                    form.Show();
                    ThemeManager.SetDarkMode(true);
                    var panel = new Panel { BackColor = Color.LightBlue };
                    var text = new TextBox { BackColor = Color.White };
                    panel.Controls.Add(text);
                    form.Controls.Add(panel);
                    Application.DoEvents();
                    text.BackColor.Should().Be(ThemeManager.Palette.Surface);
                    panel.BackColor.Should().Be(ThemeManager.Palette.Background);
                    ThemeManager.SetDarkMode(false);
                    panel.BackColor.Should().Be(Color.LightBlue);
                    text.BackColor.Should().Be(Color.White);
                    form.Close();
                }
            });
        }

        [Fact]
        public void ReplaceHostedContent_AppliesDarkThemeAndKeepsContentVisibleBeforeDrawingResumes()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm { Size = new Size(400, 300) })
                using (var host = new Panel { Dock = DockStyle.Fill })
                {
                    form.Controls.Add(host);
                    form.Show();
                    ThemeManager.SetDarkMode(true);
                    var painted = false;
                    host.Paint += (sender, args) =>
                    {
                        painted = true;
                        host.BackColor.Should().Be(ThemeManager.Palette.Background);
                        var child = host.Controls.Cast<Control>().Single();
                        child.BackColor.Should().Be(ThemeManager.Palette.Background);
                        child.Bounds.Should().Be(new Rectangle(12, 18, 240, 160));
                    };

                    var content = ThemeManager.ReplaceHostedContent(
                        host,
                        () => new Panel { Size = new Size(240, 160), Visible = true },
                        control => control.Location = new Point(12, 18));

                    painted.Should().BeTrue();
                    host.BackColor.Should().Be(ThemeManager.Palette.Background);
                    content.BackColor.Should().Be(ThemeManager.Palette.Background);
                    content.Visible.Should().BeTrue();
                    content.Bounds.Should().Be(new Rectangle(12, 18, 240, 160));
                    host.Controls.Cast<Control>().Should().ContainSingle().Which.Should().BeSameAs(content);

                    form.Close();
                }
            });
        }

        [Fact]
        public void ReplaceHostedContent_QueueView_KeepsActionButtonsThemedAndVisibleAfterInitialization()
        {
            RunOnSta(() =>
            {
                var helper = new ServiceBusHelper((message, asynchronous) => { })
                {
                    NamespaceUri = new Uri("sb://localhost/")
                };
                typeof(ServiceBusHelper).GetProperty(nameof(ServiceBusHelper.ConnectionString),
                    BindingFlags.Public | BindingFlags.Instance).SetValue(helper,
                    "Endpoint=sb://localhost/;SharedAccessKeyName=test;SharedAccessKey=dGVzdA==;EntityPath=queue-a");
                var queue = new QueueDescription("queue-a");

                using (var form = new ThemedForm { Size = new Size(1200, 800) })
                using (var host = new Panel { Dock = DockStyle.Fill })
                {
                    form.Controls.Add(host);
                    form.Show();
                    ThemeManager.SetDarkMode(true);

                    var content = ThemeManager.ReplaceHostedContent(
                        host,
                        () => new HandleQueueControl((message, asynchronous) => { }, helper, queue, queue.Path, false),
                        control =>
                        {
                            control.Location = new Point(1, 25);
                            control.Size = new Size(host.Width - 4, host.Height - 26);
                        });

                    foreach (var processEvents in new[] { false, true })
                    {
                        if (processEvents)
                            Application.DoEvents();
                        content.Visible.Should().BeTrue();
                        content.BackColor.Should().Be(ThemeManager.Palette.Background);
                        foreach (var name in new[] { "btnRefresh", "btnChangeStatus", "btnMessages",
                            "btnDeadletter", "btnCancelUpdate", "btnCreateDelete" })
                        {
                            var button = content.Controls.Find(name, true).Single().Should().BeOfType<Button>().Which;
                            button.Visible.Should().BeTrue("{0} must remain visible after queue initialization", name);
                            button.Parent.ClientRectangle.Contains(button.Bounds).Should().BeTrue();
                            button.BackColor.Should().Be(ThemeManager.Palette.Raised);
                            button.ForeColor.Should().Be(ThemeManager.Palette.Text);
                        }
                    }
                    form.Close();
                }
            });
        }

        [Fact]
        public void ReplaceHostedContent_DisposesAllPreviousUserControlsButOnlyDetachesOtherControls()
        {
            RunOnSta(() =>
            {
                using (var host = new Panel())
                using (var first = new UserControl())
                using (var second = new UserControl())
                using (var other = new Panel())
                {
                    host.Controls.AddRange(new Control[] { first, second, other });

                    var content = ThemeManager.ReplaceHostedContent(host, () => new UserControl(), null);

                    first.IsDisposed.Should().BeTrue();
                    second.IsDisposed.Should().BeTrue();
                    other.IsDisposed.Should().BeFalse();
                    other.Parent.Should().BeNull();
                    host.Controls.Cast<Control>().Should().ContainSingle().Which.Should().BeSameAs(content);
                }
            });
        }

        [Fact]
        public void ReplaceHostedContent_InLightMode_PreservesSystemHostAndContentColors()
        {
            RunOnSta(() =>
            {
                using (var host = new Panel())
                {
                    var content = ThemeManager.ReplaceHostedContent(
                        host, () => new Panel { BackColor = Color.LightBlue }, null);

                    host.BackColor.Should().Be(ThemeManager.IsThemed
                        ? ThemeManager.Palette.Background
                        : SystemColors.GradientInactiveCaption);
                    content.BackColor.Should().Be(ThemeManager.IsThemed
                        ? ThemeManager.Palette.Background
                        : Color.LightBlue);
                }
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ReplaceHostedContent_WhenCallbackThrows_PropagatesExceptionAndResumesDrawing(bool configureThrows)
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm { Size = new Size(400, 300) })
                using (var host = new Panel { Dock = DockStyle.Fill })
                {
                    form.Controls.Add(host);
                    form.Show();
                    var expected = new InvalidOperationException("Hosted view initialization failed.");
                    Panel content = null;

                    Action replace = () => ThemeManager.ReplaceHostedContent(
                        host,
                        () =>
                        {
                            if (!configureThrows)
                                throw expected;
                            content = new Panel();
                            return content;
                        },
                        control => throw expected);

                    replace.Should().Throw<InvalidOperationException>().Which.Should().BeSameAs(expected);
                    host.Visible.Should().BeTrue();
                    if (configureThrows)
                    {
                        content.Visible.Should().BeTrue();
                        content.Parent.Should().BeSameAs(host);
                    }
                    form.Close();
                }
            });
        }

        [Fact]
        public void ReplaceHostedContent_WhenFactoryReturnsNull_ThrowsAndResumesHostDrawing()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm { Size = new Size(400, 300) })
                using (var host = new Panel { Dock = DockStyle.Fill })
                {
                    form.Controls.Add(host);
                    form.Show();

                    Action replace = () => ThemeManager.ReplaceHostedContent<Panel>(host, () => null, null);

                    replace.Should().Throw<InvalidOperationException>()
                        .WithMessage("The hosted content factory returned null.");
                    host.Visible.Should().BeTrue();
                    form.Close();
                }
            });
        }

        [Fact]
        public void ReplaceHostedContent_RejectsNullHostAndFactoryBeforeChangingContent()
        {
            RunOnSta(() =>
            {
                Action nullHost = () => ThemeManager.ReplaceHostedContent<Panel>(null, () => new Panel(), null);
                nullHost.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("host");

                using (var host = new Panel())
                using (var original = new UserControl())
                {
                    host.Controls.Add(original);
                    Action nullFactory = () => ThemeManager.ReplaceHostedContent<Panel>(host, null, null);

                    nullFactory.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("createContent");
                    original.IsDisposed.Should().BeFalse();
                    original.Parent.Should().BeSameAs(host);
                }
            });
        }

        [Fact]
        public void OpenWindows_AndLaterModalWindows_ReceiveTheme()
        {
            RunOnSta(() =>
            {
                using (var first = new ThemedForm())
                using (var second = new ThemedForm())
                {
                    first.Show();
                    second.Show();
                    ThemeManager.SetDarkMode(true);
                    first.BackColor.Should().Be(ThemeManager.Palette.Background);
                    second.BackColor.Should().Be(ThemeManager.Palette.Background);
                    using (var modal = new ThemedForm())
                    {
                        modal.Shown += (sender, args) =>
                        {
                            modal.BackColor.Should().Be(ThemeManager.Palette.Background);
                            modal.Close();
                        };
                        modal.ShowDialog(first);
                    }
                    first.Close();
                    second.Close();
                }
            });
        }

        [Fact]
        public void Grids_RestoreOriginalStylesWithoutChangingFontsOrFormatting()
        {
            RunOnSta(() =>
            {
                using (var grid = new DataGridView())
                {
                    grid.Columns.Add("Count", "Count");
                    grid.Columns[0].DefaultCellStyle.Format = "N0";
                    grid.DefaultCellStyle.BackColor = Color.Bisque;
                    grid.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
                    var defaultSelection = grid.DefaultCellStyle.SelectionBackColor;
                    var headersVisual = grid.EnableHeadersVisualStyles;
                    ThemeManager.Register(grid);
                    ThemeManager.SetDarkMode(true);
                    grid.DefaultCellStyle.BackColor.Should().Be(ThemeManager.Palette.Surface);
                    grid.ColumnHeadersDefaultCellStyle.ForeColor.Should().Be(ThemeManager.Palette.Text);
                    grid.EnableHeadersVisualStyles.Should().BeFalse();
                    grid.Columns.Add("New", "New");
                    grid.Columns[1].DefaultCellStyle.ForeColor.Should().Be(ThemeManager.Palette.Text);
                    ThemeManager.SetDarkMode(false);
                    grid.DefaultCellStyle.BackColor.Should().Be(Color.Bisque);
                    grid.AlternatingRowsDefaultCellStyle.BackColor.Should().Be(Color.Lavender);
                    grid.ColumnHeadersDefaultCellStyle.ForeColor.Should().Be(Color.Navy);
                    grid.DefaultCellStyle.SelectionBackColor.Should().Be(defaultSelection);
                    grid.EnableHeadersVisualStyles.Should().Be(headersVisual);
                    grid.Columns[0].DefaultCellStyle.Format.Should().Be("N0");
                    grid.Columns[1].DefaultCellStyle.ForeColor.Should().Be(Color.Empty);
                }
            });
        }

        [Fact]
        public void Menus_IncludeDetachedAndDynamicItems_AndRestoreRenderer()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm())
                using (var menu = new ContextMenuStrip())
                {
                    var item = new ToolStripMenuItem("Open");
                    item.DropDownItems.Add("Nested");
                    menu.Items.Add(item);
                    var renderer = menu.Renderer;
                    var renderMode = menu.RenderMode;
                    var originalFore = item.ForeColor;
                    form.ContextMenuStrip = menu;
                    ThemeManager.Register(form);
                    ThemeManager.SetDarkMode(true);
                    item.ForeColor.Should().Be(ThemeManager.Palette.Text);
                    item.DropDownItems[0].ForeColor.Should().Be(ThemeManager.Palette.Text);
                    menu.Items.Add("Added");
                    menu.Items[1].ForeColor.Should().Be(ThemeManager.Palette.Text);
                    ThemeManager.SetDarkMode(false);
                    menu.RenderMode.Should().Be(renderMode);
                    menu.Renderer.Should().BeSameAs(renderer);
                    item.ForeColor.Should().Be(originalFore);
                }
            });
        }

        [Fact]
        public void CustomControls_RestoreTheirPaletteProperties()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm())
                using (var group = new Grouper { BackgroundColor = Color.Azure, BorderColor = Color.Blue })
                using (var track = new CustomTrackBar { TickColor = Color.Black, TrackLineColor = Color.Cyan })
                using (var tabs = new TabControl())
                {
                    tabs.TabPages.Add("Messages");
                    form.Controls.Add(group);
                    form.Controls.Add(track);
                    form.Controls.Add(tabs);
                    ThemeManager.Register(form);
                    ThemeManager.SetDarkMode(true);
                    group.BackgroundColor.Should().Be(ThemeManager.Palette.Background);
                    track.TickColor.Should().Be(ThemeManager.Palette.MutedText);
                    tabs.DrawMode.Should().Be(TabDrawMode.OwnerDrawFixed);
                    ThemeManager.SetDarkMode(false);
                    group.BackgroundColor.Should().Be(Color.Azure);
                    group.BorderColor.Should().Be(Color.Blue);
                    track.TickColor.Should().Be(Color.Black);
                    track.TrackLineColor.Should().Be(Color.Cyan);
                    tabs.DrawMode.Should().Be(TabDrawMode.Normal);
                }
            });
        }

        [Fact]
        public void MessageEditor_UsesReadableSyntaxAndRestoresOriginalBrushes()
        {
            RunOnSta(() =>
            {
                using (var editor = new FastColoredTextBox { Language = Language.JSON, Text = "{\"key\": 42}" })
                {
                    var brush = ((TextStyle)editor.SyntaxHighlighter.BlueStyle).ForeBrush;
                    var indent = editor.IndentBackColor;
                    ThemeManager.Register(editor);
                    ThemeManager.SetDarkMode(true);
                    editor.BackColor.Should().Be(ThemeManager.Palette.Surface);
                    editor.CaretColor.Should().Be(ThemeManager.Palette.Text);
                    ((TextStyle)editor.SyntaxHighlighter.BlueStyle).ForeBrush.Should().NotBeSameAs(brush);
                    editor.Language = Language.HTML;
                    editor.Text = "<message>value</message>";
                    ThemeManager.ApplyEditorStyles(editor);
                    ThemeManager.SetDarkMode(false);
                    editor.IndentBackColor.Should().Be(indent);
                    ((TextStyle)editor.SyntaxHighlighter.BlueStyle).ForeBrush.Should().BeSameAs(brush);
                }
            });
        }

        [Fact]
        public void Dashboard_DeadLetterWarningsSurviveThemeSwitchAndRefresh()
        {
            RunOnSta(() =>
            {
                using (var dashboard = new DashboardControl())
                {
                    dashboard.AddRow("queue", "Queue");
                    dashboard.UpdateRow("queue", 1, 2, 3);
                    var grid = dashboard.Controls.OfType<DataGridView>().Single();
                    ThemeManager.Register(dashboard);
                    ThemeManager.SetDarkMode(true);
                    grid.Rows[0].DefaultCellStyle.BackColor.Should().Be(ThemeManager.Palette.ErrorBackground);
                    dashboard.UpdateRow("queue", 2, 3, 4);
                    grid.Rows[0].DefaultCellStyle.BackColor.Should().Be(ThemeManager.Palette.ErrorBackground);
                    ThemeManager.SetDarkMode(false);
                    grid.Rows[0].DefaultCellStyle.BackColor.Should().Be(Color.FromArgb(255, 235, 230));
                    dashboard.UpdateRow("queue", 2, 0, 4);
                    grid.Rows[0].DefaultCellStyle.BackColor.Should().Be(Color.Empty);
                }
            });
        }

        [Fact]
        public void DarkPalette_TextAndSemanticColorsMeetNormalTextContrast()
        {
            var palette = ThemePalette.Dark;
            foreach (var pair in new[] { (palette.Text, palette.Background), (palette.Text, palette.Surface),
                (palette.MutedText, palette.Background), (palette.SelectionText, palette.Selection),
                (palette.WarningText, palette.WarningBackground), (palette.ErrorText, palette.ErrorBackground),
                (palette.Accent, palette.Surface), (palette.SuccessText, palette.Surface) })
                Contrast(pair.Item1, pair.Item2).Should().BeGreaterThanOrEqualTo(4.5);
            ThemePalette.HighContrast.Text.Should().Be(SystemColors.WindowText);
            ThemePalette.HighContrast.Selection.Should().Be(SystemColors.Highlight);
        }

        [Fact]
        public void AllApplicationForms_UseSharedThemedBase()
        {
            var forms = typeof(MainForm).Assembly.GetTypes().Where(type => type.Namespace == typeof(MainForm).Namespace &&
                typeof(Form).IsAssignableFrom(type));
            forms.Should().OnlyContain(type => typeof(ThemedForm).IsAssignableFrom(type));
        }

        [Fact]
        public void Charts_ThemeAxesAndLegendsWithoutChangingSeriesColors()
        {
            RunOnSta(() =>
            {
                using (var chart = new Chart())
                {
                    chart.ChartAreas.Add(new ChartArea("Area") { BackColor = Color.White });
                    chart.Legends.Add(new Legend { ForeColor = Color.Black });
                    chart.Series.Add(new Series { Color = Color.Red });
                    ThemeManager.Register(chart);
                    ThemeManager.SetDarkMode(true);
                    chart.ChartAreas[0].BackColor.Should().Be(ThemeManager.Palette.Surface);
                    chart.ChartAreas[0].AxisX.LabelStyle.ForeColor.Should().Be(ThemeManager.Palette.Text);
                    chart.Legends[0].ForeColor.Should().Be(ThemeManager.Palette.Text);
                    chart.Series[0].Color.Should().Be(Color.Red);
                    ThemeManager.SetDarkMode(false);
                    chart.ChartAreas[0].BackColor.Should().Be(Color.White);
                    chart.Legends[0].ForeColor.Should().Be(Color.Black);
                }
            });
        }

        [Fact]
        public void TabHeaders_HaveRoomForLabelsAndPaintUnusedStripDark()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm { Size = new Size(700, 300) })
                {
                    var tabs = new TabControl { Dock = DockStyle.Fill, Padding = new Point(3, 3) };
                    tabs.TabPages.Add("Dashboard");
                    tabs.TabPages.Add("Explorer");
                    form.Controls.Add(tabs);
                    form.Show();
                    ThemeManager.SetDarkMode(true);
                    Application.DoEvents();
                    var measured = TextRenderer.MeasureText(tabs.TabPages[0].Text, tabs.Font);
                    tabs.GetTabRect(0).Width.Should().BeGreaterThanOrEqualTo(measured.Width + 4);
                    using (var image = new Bitmap(tabs.Width, tabs.Height))
                    {
                        tabs.DrawToBitmap(image, new Rectangle(Point.Empty, tabs.Size));
                        image.GetPixel(tabs.Width - 10, 10).ToArgb().Should().Be(ThemeManager.Palette.Background.ToArgb());
                    }
                    ThemeManager.SetDarkMode(false);
                    tabs.Padding.Should().Be(new Point(3, 3));
                    form.Close();
                }
            });
        }

        [Fact]
        public void RepresentativeWindows_CanPaintDarkAndRestoredLight()
        {
            RunOnSta(() =>
            {
                Application.EnableVisualStyles();
                var settings = new MainSettings();
                settings.SetDefault();
                using (var options = new OptionForm(settings, ConfigFileUse.ApplicationConfig))
                    PaintWindow(options, "options");
                using (var editor = new TextForm("Message", "{\"name\":\"hello\", \"active\":true, \"count\":42}"))
                    PaintWindow(editor, "editor");
                using (var form = new ThemedForm { Size = new Size(1000, 600) })
                {
                    var tabs = new TabControl { Dock = DockStyle.Fill };
                    var dashboard = new DashboardControl { Dock = DockStyle.Fill };
                    dashboard.AddRow("orders", "Queue");
                    dashboard.UpdateRow("orders", 18, 0, 3);
                    dashboard.AddRow("notifications / emails", "Subscription");
                    dashboard.UpdateRow("notifications / emails", 6, 4, 0);
                    var page = new TabPage("Dashboard");
                    page.Controls.Add(dashboard);
                    tabs.TabPages.Add(page);
                    tabs.TabPages.Add("Explorer");
                    var menu = new MenuStrip();
                    var file = new ToolStripMenuItem("File");
                    file.DropDownItems.Add("Connect");
                    menu.Items.Add(file);
                    menu.Items.Add("View");
                    form.Controls.Add(tabs);
                    form.Controls.Add(menu);
                    PaintWindow(form, "dashboard");
                }
            });
        }

        private static void PaintWindow(Form form, string name)
        {
            form.Show();
            Application.DoEvents();
            foreach (var dark in new[] { true, false })
            {
                ThemeManager.SetDarkMode(dark);
                Application.DoEvents();
                using (var image = new Bitmap(form.Width, form.Height))
                {
                    form.DrawToBitmap(image, new Rectangle(Point.Empty, form.Size));
                    var directory = Environment.GetEnvironmentVariable("SBE_THEME_SCREENSHOTS");
                    if (!string.IsNullOrEmpty(directory))
                        image.Save(Path.Combine(directory, name + (dark ? "-dark.png" : "-light.png")), ImageFormat.Png);
                }
            }
            form.Close();
        }

        private static double Contrast(Color first, Color second)
        {
            var a = Luminance(first);
            var b = Luminance(second);
            return (Math.Max(a, b) + 0.05) / (Math.Min(a, b) + 0.05);
        }

        private static double Luminance(Color color)
        {
            Func<byte, double> linear = channel =>
            {
                var value = channel / 255.0;
                return value <= 0.04045 ? value / 12.92 : Math.Pow((value + 0.055) / 1.055, 2.4);
            };
            return 0.2126 * linear(color.R) + 0.7152 * linear(color.G) + 0.0722 * linear(color.B);
        }

        private static void RunOnSta(Action action)
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
                ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
