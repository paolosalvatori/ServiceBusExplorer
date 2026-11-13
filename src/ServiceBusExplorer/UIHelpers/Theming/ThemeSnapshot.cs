using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    internal sealed class ThemeSnapshot
    {
        private readonly Dictionary<string, Action> restore = new Dictionary<string, Action>();
        public bool Applied { get; set; }
        public System.Drawing.Color OriginalForeColor { get; private set; }

        public void Capture<T>(string name, Func<T> get, Action<T> set)
        {
            if (restore.ContainsKey(name))
                return;

            var original = get();
            restore.Add(name, () => set(original));
        }

        public void CaptureAmbientColors(object control)
        {
            foreach (var name in new[] { "BackColor", "ForeColor" })
            {
                if (restore.ContainsKey(name))
                    continue;

                var property = TypeDescriptor.GetProperties(control)[name];
                var explicitValue = property.ShouldSerializeValue(control);
                var original = property.GetValue(control);
                if (name == "ForeColor")
                    OriginalForeColor = (System.Drawing.Color)original;
                restore.Add(name, () =>
                {
                    if (explicitValue)
                        property.SetValue(control, original);
                    else
                        property.ResetValue(control);
                });
            }
        }

        public void Restore()
        {
            if (!Applied)
                return;

            foreach (var action in restore.Values)
                action();

            Applied = false;
            restore.Clear();
        }
    }
}
