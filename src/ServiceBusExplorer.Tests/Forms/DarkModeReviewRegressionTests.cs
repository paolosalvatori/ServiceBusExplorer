using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FluentAssertions;
using ServiceBusExplorer.Controls;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.UIHelpers.Theming;
using Xunit;

namespace ServiceBusExplorer.Tests.Forms
{
    [Collection("Theme UI")]
    public class DarkModeReviewRegressionTests
    {
        [Fact]
        public void TextFormButtons_ReapplyReadableForegroundAfterMouseLeaveAndEnableChanges()
        {
            RunOnSta(() =>
            {
                using (var form = new TextForm("Message", "payload"))
                {
                    form.Show();
                    ThemeManager.SetDarkMode(true);
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

                    ThemeManager.SetDarkMode(true);
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
                    ThemeManager.SetDarkMode(true);
                    Application.DoEvents();

                    staticMenu.Renderer.GetType().Name.Should().Be("DarkToolStripRenderer");
                    staticMenu.Items[0].ForeColor.Should().Be(ThemeManager.Palette.Text);

                    var dynamicNode = tree.Nodes.Add("Dynamic");
                    dynamicNode.ContextMenuStrip = dynamicMenu;
                    dynamicMenu.Renderer.Should().BeSameAs(dynamicRenderer);

                    RaiseTreeNodeMouseClick(tree, dynamicNode, MouseButtons.Right);
                    dynamicMenu.Renderer.GetType().Name.Should().Be("DarkToolStripRenderer");
                    dynamicMenu.Items[0].ForeColor.Should().Be(ThemeManager.Palette.Text);

                    ThemeManager.SetDarkMode(false);

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

                    ThemeManager.SetDarkMode(true);
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
                ThemeManager.SetDarkMode(true);

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
            ThemeManager.SetDarkMode(true);

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

            ThemeManager.SetDarkMode(false);
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
