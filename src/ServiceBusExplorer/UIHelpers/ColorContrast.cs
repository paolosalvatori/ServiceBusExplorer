using System;
using System.Drawing;

namespace ServiceBusExplorer.UIHelpers
{
    internal static class ColorContrast
    {
        public static double Ratio(Color first, Color second)
        {
            var a = RelativeLuminance(first);
            var b = RelativeLuminance(second);
            return (Math.Max(a, b) + 0.05) / (Math.Min(a, b) + 0.05);
        }

        private static double RelativeLuminance(Color color)
        {
            Func<byte, double> linear = channel =>
            {
                var value = channel / 255.0;
                return value <= 0.04045 ? value / 12.92 : Math.Pow((value + 0.055) / 1.055, 2.4);
            };

            return 0.2126 * linear(color.R) + 0.7152 * linear(color.G) + 0.0722 * linear(color.B);
        }
    }
}
