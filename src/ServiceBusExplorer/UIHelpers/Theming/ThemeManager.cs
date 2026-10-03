using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.Win32;
using ServiceBusExplorer.Controls;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    public static partial class ThemeManager
    {
        private static readonly ConditionalWeakTable<Control, ControlState> controls =
            new ConditionalWeakTable<Control, ControlState>();
        private static readonly ConditionalWeakTable<object, ThemeSnapshot> snapshots =
            new ConditionalWeakTable<object, ThemeSnapshot>();
        private static readonly List<WeakReference<Control>> roots = new List<WeakReference<Control>>();
        private static readonly object rootsLock = new object();
        private static bool initialized;

        public static bool DarkMode { get; private set; }
        public static bool IsDark => DarkMode && !SystemInformation.HighContrast;
        public static bool IsThemed => DarkMode || SystemInformation.HighContrast;
        public static ThemePalette Palette => SystemInformation.HighContrast ? ThemePalette.HighContrast : ThemePalette.Dark;

        public static void Initialize()
        {
            if (initialized)
                return;
            initialized = true;
            SystemEvents.UserPreferenceChanged += PreferencesChanged;
            Application.ApplicationExit += ApplicationExit;
        }

        public static void SetDarkMode(bool enabled)
        {
            DarkMode = enabled;
            RefreshWindows();
        }

        public static void Register(Control root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));
            if (IsThemeExcludedControl(root))
                return;
            lock (rootsLock)
            {
                roots.RemoveAll(reference => !reference.TryGetTarget(out var target) || target.IsDisposed);
                if (!roots.Any(reference => reference.TryGetTarget(out var target) && target == root))
                    roots.Add(new WeakReference<Control>(root));
            }
            Apply(root);
        }

        public static void RegisterComponents(IContainer components)
        {
            if (components == null)
                throw new ArgumentNullException(nameof(components));

            foreach (IComponent component in components.Components)
                if (component is Control control)
                    Register(control);
        }

        public static void Apply(Control root)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));
            if (root.IsDisposed || root.Disposing)
                return;
            if (IsThemeExcludedControl(root))
                return;
            if (root.InvokeRequired)
                throw new InvalidOperationException("Apply themes on the control's UI thread.");

            root.SuspendLayout();
            try
            {
                // Capture the whole tree before changing inherited parent colors.
                PrepareTree(root);
                ApplyTree(root);
            }
            finally
            {
                root.ResumeLayout(true);
            }
            root.Invalidate(true);
        }

        private static void PrepareTree(Control control)
        {
            if (IsThemeExcludedControl(control))
                return;

            var state = controls.GetValue(control, CreateState);
            if (IsThemed)
            {
                CaptureControl(control, state.Snapshot);
                if (control is TreeView tree)
                    RegisterTreeNodeMenus(tree);
            }
            foreach (Control child in control.Controls)
                PrepareTree(child);
        }

        private static void ApplyTree(Control control)
        {
            if (IsThemeExcludedControl(control))
                return;

            var state = controls.GetValue(control, CreateState);
            if (IsThemed)
            {
                state.Snapshot.Applied = true;
                ApplyControl(control);
            }
            else
            {
                state.Snapshot.Restore();
                RestoreExtraColors(control);
            }
            ThemeNativeMethods.RefreshInputBorder(control);
            if (control is Form form)
                ThemeNativeMethods.ApplyCaption(form, IsDark);
            foreach (Control child in control.Controls)
                ApplyTree(child);
            if (control.ContextMenuStrip != null)
                Register(control.ContextMenuStrip);
            if (control is ToolStrip strip)
                ApplyItems(strip);
            if (control is IThemeAware aware)
                aware.ApplyTheme();
        }

        private static ControlState CreateState(Control control)
        {
            control.ControlAdded += ControlAdded;
            control.ContextMenuStripChanged += ContextMenuChanged;
            control.HandleCreated += ControlHandleCreated;
            control.Disposed += ControlDisposed;
            var state = new ControlState
            {
                InputBorder = ThemeNativeMethods.TrackInputBorder(control)
            };
            AttachDrawing(control, state);
            return state;
        }

        private static void ControlAdded(object sender, ControlEventArgs e)
        {
            var host = ((Control)sender).FindForm();
            if (host != null && host.IsHandleCreated)
                QueueApply(host, e.Control);
            else
                Apply(e.Control);
        }

        private static void ContextMenuChanged(object sender, EventArgs e)
        {
            var menu = ((Control)sender).ContextMenuStrip;
            if (menu != null)
                Register(menu);
        }

        private static void ControlHandleCreated(object sender, EventArgs e)
        {
            var control = (Control)sender;
            if (control is Form form)
                ThemeNativeMethods.ApplyCaption(form, IsDark);
        }

        private static void ControlDisposed(object sender, EventArgs e)
        {
            var control = (Control)sender;
            if (controls.TryGetValue(control, out var state))
                state.Dispose();
            controls.Remove(control);
            lock (rootsLock)
                roots.RemoveAll(reference => !reference.TryGetTarget(out var target) || target == control);
        }

        private static void RefreshWindows()
        {
            Control[] live;
            lock (rootsLock)
            {
                roots.RemoveAll(reference => !reference.TryGetTarget(out var target) || target.IsDisposed);
                live = roots.Select(reference =>
                {
                    reference.TryGetTarget(out var target);
                    return target;
                }).Where(target => target != null).ToArray();
            }
            foreach (var root in live)
            {
                if (root.InvokeRequired)
                    QueueApply(root, root);
                else
                    Apply(root);
            }
        }

        private static void QueueApply(Control host, Control target)
        {
            if (host.IsDisposed || host.Disposing || !host.IsHandleCreated)
                return;
            try
            {
                host.BeginInvoke(new Action(() =>
                {
                    if (!target.IsDisposed && !target.Disposing)
                        Apply(target);
                }));
            }
            catch (InvalidOperationException exception) when (host.IsDisposed || host.Disposing || !host.IsHandleCreated)
            {
                Trace.WriteLine($"Theme update cancelled for a closing window: {exception.Message}");
            }
        }

        private static void PreferencesChanged(object sender, UserPreferenceChangedEventArgs e) => RefreshWindows();

        private static void ApplicationExit(object sender, EventArgs e)
        {
            SystemEvents.UserPreferenceChanged -= PreferencesChanged;
            Application.ApplicationExit -= ApplicationExit;
            initialized = false;
        }

        private sealed class ControlState : IDisposable
        {
            public ThemeSnapshot Snapshot { get; } = new ThemeSnapshot();
            public ThemeNativeMethods.TabWindow TabWindow { get; set; }
            public IDisposable InputBorder { get; set; }
            public HashSet<FastColoredTextBoxNS.TextStyle> EditorStyles { get; } = new HashSet<FastColoredTextBoxNS.TextStyle>();
            public bool UpdatingButtonForeground { get; set; }
            public void Dispose()
            {
                InputBorder?.Dispose();
                TabWindow?.Dispose();
            }
        }

        private static bool IsThemeExcludedControl(Control control) => control is ColorEditingControl;

        private static void RegisterTreeNodeMenus(TreeView treeView)
        {
            if (treeView == null)
                return;

            RegisterTreeNodeMenus(treeView.Nodes, new HashSet<ContextMenuStrip>());
        }

        private static void RegisterTreeNodeMenus(TreeNodeCollection nodes, ISet<ContextMenuStrip> registeredMenus)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.ContextMenuStrip != null && registeredMenus.Add(node.ContextMenuStrip))
                    Register(node.ContextMenuStrip);
                if (node.Nodes.Count > 0)
                    RegisterTreeNodeMenus(node.Nodes, registeredMenus);
            }
        }

        private static ThemeSnapshot Snapshot(object owner) => snapshots.GetValue(owner, _ => new ThemeSnapshot());
    }
}
