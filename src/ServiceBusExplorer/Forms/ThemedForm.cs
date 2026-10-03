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
            base.OnLoad(e);
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                ThemeManager.Register(this);
        }
    }
}
