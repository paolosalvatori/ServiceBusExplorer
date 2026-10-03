using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ServiceBusExplorer.UIHelpers;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    public static partial class ThemeManager
    {
        public static T ReplaceHostedContent<T>(
            Control host,
            Func<T> createContent,
            Action<T> configureContent)
            where T : Control
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));
            if (createContent == null)
                throw new ArgumentNullException(nameof(createContent));

            host.SuspendDrawing();
            T content = null;
            try
            {
                foreach (var child in host.Controls.OfType<UserControl>().ToArray())
                    child.Dispose();
                host.Controls.Clear();
                host.BackColor = IsThemed ? Palette.Background : SystemColors.GradientInactiveCaption;

                content = createContent();
                if (content == null)
                    throw new InvalidOperationException("The hosted content factory returned null.");

                content.SuspendDrawing();
                host.Controls.Add(content);
                configureContent?.Invoke(content);
                Apply(content);
                return content;
            }
            finally
            {
                host.ResumeDrawing();
                if (content != null && !content.IsDisposed)
                    content.ResumeDrawing();
            }
        }
    }
}
