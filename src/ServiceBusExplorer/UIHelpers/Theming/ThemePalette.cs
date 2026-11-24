using System.Drawing;
using System.Windows.Forms;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    public sealed class ThemePalette
    {
        public static ThemePalette Dark { get; } = new ThemePalette(false);
        public static ThemePalette HighContrast { get; } = new ThemePalette(true);

        private readonly bool highContrast;

        private ThemePalette(bool highContrast)
        {
            this.highContrast = highContrast;
        }

        public Color Background => highContrast ? SystemColors.Control : Color.FromArgb(30, 32, 36);
        public Color Surface => highContrast ? SystemColors.Window : Color.FromArgb(37, 40, 45);
        public Color Raised => highContrast ? SystemColors.Control : Color.FromArgb(48, 52, 59);
        public Color Border => highContrast ? SystemColors.WindowText : Color.FromArgb(83, 91, 103);
        public Color Text => highContrast ? SystemColors.WindowText : Color.FromArgb(232, 235, 240);
        public Color MutedText => highContrast ? SystemColors.GrayText : Color.FromArgb(171, 179, 192);
        public Color Accent => highContrast ? SystemColors.Highlight : Color.FromArgb(112, 183, 255);
        public Color Selection => highContrast ? SystemColors.Highlight : Color.FromArgb(47, 79, 112);
        public Color SelectionText => highContrast ? SystemColors.HighlightText : Color.White;
        public Color Hover => highContrast ? SystemColors.Highlight : Color.FromArgb(60, 68, 80);
        public Color WarningBackground => highContrast ? SystemColors.Window : Color.FromArgb(70, 56, 30);
        public Color WarningText => highContrast ? SystemColors.WindowText : Color.FromArgb(255, 214, 128);
        public Color ErrorBackground => highContrast ? SystemColors.Window : Color.FromArgb(75, 39, 43);
        public Color ErrorText => highContrast ? SystemColors.WindowText : Color.FromArgb(255, 159, 166);
        public Color SuccessText => highContrast ? SystemColors.WindowText : Color.FromArgb(139, 213, 155);
    }
}
