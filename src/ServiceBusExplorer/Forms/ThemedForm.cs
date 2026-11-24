using System;
using System.ComponentModel;
using System.Windows.Forms;
using ServiceBusExplorer.UIHelpers.Theming;

namespace ServiceBusExplorer.Forms
{
    public class ThemedForm : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            var runtime = !DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime;
            if (runtime)
                ThemeManager.Register(this);
            base.OnLoad(e);
            // Load handlers can populate controls or reset designer colors before the first paint.
            if (runtime)
                ThemeManager.Apply(this);
        }
    }
}
