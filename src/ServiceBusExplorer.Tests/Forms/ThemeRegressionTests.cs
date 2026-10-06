using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FastColoredTextBoxNS;
using TextStyle = FastColoredTextBoxNS.TextStyle;
using FluentAssertions;
using Microsoft.Win32;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Enums;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.UIHelpers.Theming;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [Collection("Theme UI")]
    public class ThemeRegressionTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void PreferenceRefresh_DispatchesHandlelessMenusToTheirOwningUiThread(bool registerComponents)
        {
            RunOnSta(() =>
            {
                using (var components = new Container())
                using (var form = new ThemedForm())
                using (var menu = new ContextMenuStrip(components))
                {
                    menu.Items.Add("Detached");
                    if (registerComponents)
                        ThemeManager.RegisterComponents(components);
                    else
                    {
                        form.ContextMenuStrip = menu;
                        ThemeManager.Register(form);
                    }
                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    menu.IsHandleCreated.Should().BeFalse();
                    menu.BackColor = Color.Orange;
                    var updates = new List<int>();
                    menu.BackColorChanged += (sender, args) => updates.Add(Thread.CurrentThread.ManagedThreadId);

                    RunOnWorker(() => typeof(ThemeManager).GetMethod("PreferencesChanged",
                        BindingFlags.Static | BindingFlags.NonPublic).Invoke(null,
                        new object[] { null, new UserPreferenceChangedEventArgs(UserPreferenceCategory.Color) }));

                    updates.Should().BeEmpty("the UI thread has not processed the queued update yet");
                    menu.BackColor.Should().Be(Color.Orange);
                    Application.DoEvents();
                    menu.BackColor.Should().Be(ThemeManager.Palette.Background);
                    updates.Should().ContainSingle().Which.Should().Be(Thread.CurrentThread.ManagedThreadId);
                    menu.IsHandleCreated.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void Apply_RejectsBackgroundCallsForRegisteredHandlelessControls()
        {
            RunOnSta(() =>
            {
                using (var panel = new Panel())
                {
                    ThemeManager.Register(panel);
                    panel.IsHandleCreated.Should().BeFalse();
                    RunOnWorker(() =>
                    {
                        Action apply = () => ThemeManager.Apply(panel);
                        apply.Should().Throw<InvalidOperationException>()
                            .WithMessage("Apply themes on the control's UI thread.");
                    });
                }
            });
        }

        [Fact]
        public void QueuedThemeRefresh_SkipsControlsDisposedBeforeDispatch()
        {
            RunOnSta(() =>
            {
                using (var menu = new ContextMenuStrip())
                {
                    ThemeManager.Register(menu);
                    RunOnWorker(() => ThemeManager.SetThemeMode(ThemeMode.Dark));
                    menu.Dispose();
                    Action dispatch = Application.DoEvents;
                    dispatch.Should().NotThrow();
                    menu.IsHandleCreated.Should().BeFalse();
                }
            });
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        public void ListDrawing_PreservesDisabledForegroundAndSelectionColors(bool disabled, bool selected)
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Dark);
                using (var list = new ListView { View = View.Details })
                using (var image = new Bitmap(180, 40))
                using (var expected = new Bitmap(180, 40))
                using (var graphics = Graphics.FromImage(image))
                using (var expectedGraphics = Graphics.FromImage(expected))
                {
                    var header = list.Columns.Add("Choice");
                    var item = list.Items.Add("Unavailable");
                    item.ForeColor = disabled ? SystemColors.GrayText : SystemColors.ControlText;
                    item.Selected = selected;
                    var bounds = new Rectangle(Point.Empty, image.Size);
                    var args = new DrawListViewSubItemEventArgs(graphics, bounds, item,
                        item.SubItems[0], 0, 0, header, ListViewItemStates.Default);

                    typeof(ThemeManager).GetMethod("DrawListSubItem", BindingFlags.Static | BindingFlags.NonPublic)
                        .Invoke(null, new object[] { list, args });

                    expectedGraphics.Clear(selected ? ThemeManager.Palette.Selection : ThemeManager.Palette.Surface);
                    TextRenderer.DrawText(expectedGraphics, item.Text, item.Font, Rectangle.Inflate(bounds, -4, 0),
                        selected ? ThemeManager.Palette.SelectionText :
                        disabled ? ThemeManager.Palette.MutedText : ThemeManager.Palette.Text,
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter |
                        TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                    for (var y = 0; y < image.Height; y++)
                        for (var x = 0; x < image.Width; x++)
                            image.GetPixel(x, y).Should().Be(expected.GetPixel(x, y));
                }
            });
        }

        [Fact]
        public void EditorStyles_RestoreSyntaxColorsAfterHighContrastBrushReplacementAndRepeatedApplications()
        {
            RunOnSta(() =>
            {
                using (var editor = new FastColoredTextBox { Language = Language.JSON, Text = "{\"key\": 42}" })
                {
                    var styles = new[] { (TextStyle)editor.SyntaxHighlighter.BlueStyle,
                        (TextStyle)editor.SyntaxHighlighter.GreenStyle, (TextStyle)editor.SyntaxHighlighter.RedStyle };
                    var originals = styles.Select(style => style.ForeBrush).ToArray();
                    ThemeManager.Register(editor);
                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    var dark = styles.Select(style => style.ForeBrush).ToArray();

                    // Reproduce the brush replacement performed by the high-contrast pass without changing OS settings.
                    foreach (var style in styles)
                        style.ForeBrush = SystemBrushes.WindowText;
                    for (var i = 0; i < 3; i++)
                    {
                        ThemeManager.ApplyEditorStyles(editor);
                        styles.Select(style => style.ForeBrush).Should().Equal(dark);
                    }
                    dark.Distinct().Should().HaveCountGreaterThan(1);
                    ThemeManager.SetThemeMode(ThemeMode.Light);
                    styles.Select(style => style.ForeBrush).Should().Equal(originals);
                }
            });
        }

        [Fact]
        public void TextFormButtons_ReapplyReadableForegroundAfterMouseLeaveAndEnableChanges()
        {
            RunOnSta(() =>
            {
                using (var form = new TextForm("Message", "payload"))
                {
                    form.Show();
                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    Application.DoEvents();

                    var button = FindControl<Button>(form, "btnOk");

                    RaiseControlEvent(button, "OnMouseEnter", EventArgs.Empty);
                    RaiseControlEvent(button, "OnMouseLeave", EventArgs.Empty);
                    button.ForeColor.Should().Be(ThemeManager.Palette.Text);

                    button.Enabled = false;
                    button.ForeColor.Should().Be(ThemeManager.Palette.MutedText);

                    button.Enabled = true;
                    button.ForeColor.Should().Be(ThemeManager.Palette.Text);

                    form.Close();
                }
            });
        }

        [Fact]
        public void ButtonBaseForegroundEnforcement_PreservesWarningSemanticColors()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm())
                using (var button = new Button
                {
                    ForeColor = Color.DarkOrange,
                    Text = "Warning"
                })
                {
                    button.MouseLeave += (sender, args) => button.ForeColor = SystemColors.ControlText;
                    form.Controls.Add(button);
                    form.Show();

                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    Application.DoEvents();

                    button.ForeColor.Should().Be(ThemeManager.Palette.WarningText);

                    RaiseControlEvent(button, "OnMouseLeave", EventArgs.Empty);
                    button.ForeColor.Should().Be(ThemeManager.Palette.WarningText);

                    button.Enabled = false;
                    button.ForeColor.Should().Be(ThemeManager.Palette.MutedText);

                    button.Enabled = true;
                    button.ForeColor.Should().Be(ThemeManager.Palette.WarningText);

                    form.Close();
                }
            });
        }

        [Fact]
        public void DetachedTreeNodeMenus_UseComponentRegistrationAndDynamicRightClickRegistration()
        {
            RunOnSta(() =>
            {
                using (var components = new Container())
                using (var form = new ThemedForm())
                using (var tree = new TreeView { Dock = DockStyle.Fill })
                using (var staticMenu = new ContextMenuStrip(components))
                using (var dynamicMenu = new ContextMenuStrip())
                {
                    staticMenu.Items.Add("Static");
                    dynamicMenu.Items.Add("Dynamic");

                    var staticRenderer = staticMenu.Renderer;
                    var staticRenderMode = staticMenu.RenderMode;
                    var dynamicRenderer = dynamicMenu.Renderer;
                    var dynamicRenderMode = dynamicMenu.RenderMode;

                    tree.Nodes.Add("Static").ContextMenuStrip = staticMenu;

                    form.Controls.Add(tree);
                    form.Show();

                    ThemeManager.RegisterComponents(components);
                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    Application.DoEvents();

                    staticMenu.Renderer.GetType().Name.Should().Be("DarkToolStripRenderer");
                    staticMenu.Items[0].ForeColor.Should().Be(ThemeManager.Palette.Text);

                    var dynamicNode = tree.Nodes.Add("Dynamic");
                    dynamicNode.ContextMenuStrip = dynamicMenu;
                    dynamicMenu.Renderer.Should().BeSameAs(dynamicRenderer);

                    RaiseTreeNodeMouseClick(tree, dynamicNode, MouseButtons.Right);
                    dynamicMenu.Renderer.GetType().Name.Should().Be("DarkToolStripRenderer");
                    dynamicMenu.Items[0].ForeColor.Should().Be(ThemeManager.Palette.Text);

                    ThemeManager.SetThemeMode(ThemeMode.Light);

                    staticMenu.Renderer.Should().BeSameAs(staticRenderer);
                    staticMenu.RenderMode.Should().Be(staticRenderMode);
                    dynamicMenu.Renderer.Should().BeSameAs(dynamicRenderer);
                    dynamicMenu.RenderMode.Should().Be(dynamicRenderMode);

                    form.Close();
                }
            });
        }

        [Fact]
        public void TestControlGraphLayouts_RethemeRecreatedChartTitlesAndRestoreLegends()
        {
            RunOnSta(() =>
            {
                VerifyGraphLayoutThemesRecreatedTitles(typeof(TestQueueControl), configure: control =>
                {
                    SetField(control, "senderEnabledCheckBox", new CheckBox { Checked = true });
                    SetField(control, "receiverEnabledCheckBox", new CheckBox { Checked = false });
                    SetField(control, "grouperReceiverStatistics", new Grouper { Width = 120 });
                });

                VerifyGraphLayoutThemesRecreatedTitles(typeof(TestTopicControl), configure: control =>
                {
                    SetField(control, "senderEnabledCheckBox", new CheckBox { Checked = true });
                    SetField(control, "receiverEnabledCheckBox", new CheckBox { Checked = false });
                    SetField(control, "grouperReceiverStatistics", new Grouper { Width = 120 });
                });

                VerifyGraphLayoutThemesRecreatedTitles(typeof(TestEventHubControl));
            });
        }

        [Fact]
        public void ColorPickerEditingControl_KeepsActualColorWhileDarkThemeIsActive()
        {
            RunOnSta(() =>
            {
                using (var form = new ThemedForm { Size = new Size(300, 160) })
                using (var grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AllowUserToAddRows = false
                })
                {
                    grid.Columns.Add(new DataGridViewColorPickerColumn { Name = "Color" });
                    grid.Rows.Add(Color.Red);
                    form.Controls.Add(grid);
                    form.Show();

                    ThemeManager.SetThemeMode(ThemeMode.Dark);
                    Application.DoEvents();

                    grid.CurrentCell = grid[0, 0];
                    grid.Focus();
                    grid.BeginEdit(true).Should().BeTrue();
                    Application.DoEvents();

                    var editingControl = grid.EditingControl;
                    editingControl.Should().NotBeNull();
                    editingControl.GetType().Name.Should().Be("ColorEditingControl");
                    editingControl.BackColor.Should().Be(Color.Red);
                    ((IDataGridViewEditingControl)editingControl).EditingControlFormattedValue.Should().Be(Color.Red);
                    grid.CurrentCell.Value.Should().Be(Color.Red);

                    form.Close();
                }
            });
        }

        [Fact]
        public void TreeNodeExplicitColor_KeepsHueForDrawingWithoutMutatingNodeMetadata()
        {
            RunOnSta(() =>
            {
                ThemeManager.SetThemeMode(ThemeMode.Dark);

                var original = Color.Purple;
                var node = new TreeNode("Colored") { ForeColor = original };
                var helper = typeof(ThemeManager).GetMethod("ReadableTreeNodeText", BindingFlags.Static | BindingFlags.NonPublic);

                helper.Should().NotBeNull();
                var adjusted = (Color)helper.Invoke(null, new object[] { node.ForeColor, ThemeManager.Palette.Surface });

                adjusted.Should().NotBe(ThemeManager.Palette.Text);
                Contrast(adjusted, ThemeManager.Palette.Surface).Should().BeGreaterThanOrEqualTo(4.5);
                node.ForeColor.Should().Be(original);
            });
        }

        private static void VerifyGraphLayoutThemesRecreatedTitles(Type controlType, Action<object> configure = null)
        {
            ThemeManager.SetThemeMode(ThemeMode.Dark);

            var control = FormatterServices.GetUninitializedObject(controlType);
            var chart = CreateChart();
            var tabPageGraph = new TabPage { Size = new Size(640, 360) };

            SetField(control, "chart", chart);
            SetField(control, "tabPageGraph", tabPageGraph);
            SetField(control, "grouperSenderStatistics", new Grouper { Width = 120 });
            configure?.Invoke(control);

            InvokePrivateMethod(control, "SetGraphLayout");

            chart.Titles.Should().ContainSingle();
            chart.Titles[0].ForeColor.Should().Be(ThemeManager.Palette.Text);
            chart.Legends[0].ForeColor.Should().Be(ThemeManager.Palette.Text);

            ThemeManager.SetThemeMode(ThemeMode.Light);
            ThemeManager.Apply(chart);

            chart.Titles[0].ForeColor.ToArgb().Should().NotBe(ThemeManager.Palette.Text.ToArgb());
            chart.Legends[0].ForeColor.Should().Be(Color.Black);
        }

        private static Chart CreateChart()
        {
            var chart = new Chart { Size = new Size(320, 180) };
            chart.ChartAreas.Add(new ChartArea("Default") { BackColor = Color.White });
            chart.Legends.Add(new Legend("Default") { ForeColor = Color.Black });
            return chart;
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

        private static void SetField(object instance, string fieldName, object value)
        {
            var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            field.Should().NotBeNull($"missing field {fieldName} on {instance.GetType().Name}");
            field.SetValue(instance, value);
        }

        private static void InvokePrivateMethod(object instance, string methodName)
        {
            var method = instance.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            method.Should().NotBeNull();
            method.Invoke(instance, null);
        }

        private static void RaiseControlEvent(Control control, string methodName, EventArgs eventArgs)
        {
            var method = typeof(Control).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            method.Should().NotBeNull();
            method.Invoke(control, new object[] { eventArgs });
        }

        private static void RaiseTreeNodeMouseClick(TreeView tree, TreeNode node, MouseButtons button)
        {
            var method = typeof(TreeView).GetMethod("OnNodeMouseClick", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Should().NotBeNull();
            method.Invoke(tree, new object[] { new TreeNodeMouseClickEventArgs(node, button, 1, 0, 0) });
        }

        private static T FindControl<T>(Control root, string name)
            where T : Control
        {
            return root.Controls.Find(name, true)[0].Should().BeOfType<T>().Which;
        }

        private static void RunOnSta(Action action)
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
                ExceptionDispatchInfo.Capture(failure).Throw();
        }

        private static void RunOnWorker(Action action)
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
            thread.Start();
            thread.Join();
            if (failure != null)
                ExceptionDispatchInfo.Capture(failure).Throw();
        }
    }
}
