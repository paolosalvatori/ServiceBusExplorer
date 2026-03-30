#region Using Directives

using FastColoredTextBoxNS;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    internal static class CopyBodyButtonHelper
    {
        private const int ButtonWidth = 72;
        private const int ButtonHeight = 24;
        private const int ButtonTopMargin = 4;
        private const int ButtonRightMargin = 6;

        internal static Button AddCopyBodyButton(Controls.Grouper grouper, FastColoredTextBox textBox)
        {
            var btn = new Button
            {
                Name = "btnCopyBody_" + textBox.Name,
                Text = "Copy Body",
                Size = new Size(ButtonWidth, ButtonHeight),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(215, 228, 242),
                ForeColor = SystemColors.ControlText,
                Font = new Font("Microsoft Sans Serif", 8.25F),
                Enabled = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(153, 180, 209);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(153, 180, 209);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 180, 209);

            btn.Click += (s, e) =>
            {
                var text = textBox.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    Clipboard.SetText(text);
                }
            };

            textBox.TextChanged += (s, e) =>
            {
                btn.Enabled = !string.IsNullOrEmpty(textBox.Text);
            };

            grouper.Controls.Add(btn);
            btn.BringToFront();

            return btn;
        }

        internal static void LayoutTextBoxWithCopyButton(Controls.Grouper grouper, FastColoredTextBox textBox, Button copyButton)
        {
            var margin = textBox.Location.X;
            copyButton.Location = new Point(
                grouper.Size.Width - ButtonWidth - ButtonRightMargin,
                ButtonTopMargin);
            textBox.Size = new Size(
                grouper.Size.Width - margin * 2,
                grouper.Size.Height - textBox.Location.Y - margin);
        }
    }
}
