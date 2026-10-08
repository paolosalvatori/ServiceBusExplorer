using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    public static partial class ThemeManager
    {
        static Color HostedBackground => IsThemed ? Palette.Background : SystemColors.Window;

        public static void SetHostedBackground(Control host)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));
            EnsureUiThread(host);
            controls.GetValue(host, CreateState).IsContentHost = true;
            host.BackColor = HostedBackground;
        }

        /// <summary>
        /// Replaces content on the host's UI thread. Register handleless hosts on that thread first.
        /// </summary>
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
            EnsureUiThread(host);
            if (!host.IsHandleCreated && !controls.TryGetValue(host, out _))
                throw new InvalidOperationException("Register handleless hosts on their UI thread before replacing content.");

            host.SuspendDrawing();
            T content = null;
            try
            {
                foreach (var child in host.Controls.OfType<UserControl>().ToArray())
                    child.Dispose();
                host.Controls.Clear();
                SetHostedBackground(host);

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
