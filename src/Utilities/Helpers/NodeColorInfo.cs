namespace ServiceBusExplorer.Utilities.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class NodeColorInfo
    {
        public string Format()
        {
            return $"{Color.ToArgb()};{IsLeaf};{Text}";
        }

        public static NodeColorInfo Parse(string value)
        {
            try
            {
                var parts = value.Split(';');
                if (parts.Length == 3)
                {
                    return new NodeColorInfo
                    {
                        Color = Color.FromArgb(int.Parse(parts[0])),
                        IsLeaf = bool.Parse(parts[1]),
                        Text = parts[2]
                    };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<NodeColorInfo> ParseAll(string value)
        {
            return value.Split('¤').Select(Parse).Where(n => n != null).ToList();
        }

        public static string FormatAll(IEnumerable<NodeColorInfo> items)
        {
            return string.Join("¤", items.Select(i => i.Format()));
        }

        public string Text { get; set; }
        public bool IsLeaf { get; set; }
        public Color Color { get; set; }
    }
}
