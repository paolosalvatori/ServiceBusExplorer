using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FastColoredTextBoxNS;
using TextStyle = FastColoredTextBoxNS.TextStyle;

namespace ServiceBusExplorer.UIHelpers.Theming
{
    public static partial class ThemeManager
    {
        private static void CaptureSpecialControl(Control control, ThemeSnapshot state)
        {
            if (control is ServiceBusExplorer.Controls.CustomTrackBar track)
            {
                state.Capture("Tick", () => track.TickColor, value => track.TickColor = value);
                state.Capture("Tracker", () => track.TrackerColor, value => track.TrackerColor = value);
                state.Capture("TrackBorder", () => track.BorderColor, value => track.BorderColor = value);
                state.Capture("TrackLine", () => track.TrackLineColor, value => track.TrackLineColor = value);
                state.Capture("TrackBrush", () => track.TrackLineBrushStyle, value => track.TrackLineBrushStyle = value);
            }
            if (control is LinkLabel link)
            {
                state.Capture("Link", () => link.LinkColor, value => link.LinkColor = value);
                state.Capture("ActiveLink", () => link.ActiveLinkColor, value => link.ActiveLinkColor = value);
                state.Capture("VisitedLink", () => link.VisitedLinkColor, value => link.VisitedLinkColor = value);
                state.Capture("DisabledLink", () => link.DisabledLinkColor, value => link.DisabledLinkColor = value);
            }
            if (control is FastColoredTextBox editor)
            {
                state.Capture("IndentBack", () => editor.IndentBackColor, value => editor.IndentBackColor = value);
                state.Capture("PaddingBack", () => editor.PaddingBackColor, value => editor.PaddingBackColor = value);
                state.Capture("LineNumber", () => editor.LineNumberColor, value => editor.LineNumberColor = value);
                state.Capture("ServiceLines", () => editor.ServiceLinesColor, value => editor.ServiceLinesColor = value);
                state.Capture("Caret", () => editor.CaretColor, value => editor.CaretColor = value);
                state.Capture("Selection", () => editor.SelectionColor, value => editor.SelectionColor = value);
                state.Capture("CurrentLine", () => editor.CurrentLineColor, value => editor.CurrentLineColor = value);
                state.Capture("Disabled", () => editor.DisabledColor, value => editor.DisabledColor = value);
                state.Capture("Folding", () => editor.FoldingIndicatorColor, value => editor.FoldingIndicatorColor = value);
                state.Capture("TextBorder", () => editor.TextAreaBorderColor, value => editor.TextAreaBorderColor = value);
            }
            if (control is Chart chart)
                CaptureChart(chart);
        }

        private static void ApplyLink(LinkLabel link)
        {
            link.LinkColor = Palette.Accent;
            link.ActiveLinkColor = Palette.SelectionText;
            link.VisitedLinkColor = Palette.Accent;
            link.DisabledLinkColor = Palette.MutedText;
        }

        private static void ApplySpecialControl(Control control)
        {
            if (control is ServiceBusExplorer.Controls.CustomTrackBar track)
            {
                track.TickColor = Palette.MutedText;
                track.TrackerColor = Palette.Accent;
                track.BorderColor = Palette.Border;
                track.TrackLineColor = Palette.Raised;
                track.TrackLineBrushStyle = ServiceBusExplorer.Controls.BrushStyle.Solid;
            }
            if (control is FastColoredTextBox editor)
            {
                editor.BackColor = Palette.Surface;
                editor.ForeColor = Palette.Text;
                editor.IndentBackColor = Palette.Background;
                editor.PaddingBackColor = Palette.Surface;
                editor.LineNumberColor = Palette.MutedText;
                editor.ServiceLinesColor = Palette.Border;
                editor.CaretColor = Palette.Text;
                editor.SelectionColor = Color.FromArgb(110, Palette.Accent);
                editor.CurrentLineColor = Palette.Raised;
                editor.DisabledColor = Palette.Background;
                editor.FoldingIndicatorColor = Palette.MutedText;
                editor.TextAreaBorderColor = Palette.Border;
                ApplyEditorStyles(editor);
            }
            if (control is Chart chart)
                ApplyChart(chart);
        }

        private static IEnumerable<TextStyle> EditorStyles(FastColoredTextBox editor)
        {
            var highlighter = editor.SyntaxHighlighter;
            var styles = new[] { highlighter.BlueBoldStyle, highlighter.BlueStyle, highlighter.BoldStyle,
                highlighter.BrownStyle, highlighter.GrayStyle, highlighter.GreenStyle, highlighter.MagentaStyle,
                highlighter.MaroonStyle, highlighter.RedStyle, highlighter.BlackStyle, highlighter.DarkCyanStyle,
                highlighter.MediumSeaGreenStyle, editor.DefaultStyle, editor.FoldedBlockStyle };
            foreach (var style in styles)
                if (style is TextStyle text)
                    yield return text;
            foreach (var style in editor.Styles)
                if (style is TextStyle text)
                    yield return text;
        }

        public static void ApplyEditorStyles(FastColoredTextBox editor)
        {
            if (!IsThemed)
                return;
            foreach (var style in EditorStyles(editor))
            {
                var state = Snapshot(style);
                state.Capture("ForeBrush", () => style.ForeBrush, value => style.ForeBrush = value);
                state.Applied = true;
                controls.GetValue(editor, CreateState).EditorStyles.Add(style);
                if (SystemInformation.HighContrast)
                    style.ForeBrush = SystemBrushes.WindowText;
                else if (style.ForeBrush is SolidBrush brush)
                {
                    var color = brush.Color;
                    style.ForeBrush = color.B > color.R * 1.2 ? Brushes.LightSkyBlue :
                        color.G > color.R * 1.2 ? Brushes.PaleGreen :
                        color.R > color.G * 1.2 ? Brushes.LightSalmon :
                        color.R > 100 && color.B > 100 && color.G < 100 ? Brushes.Plum :
                        color.GetBrightness() < 0.6 ? Brushes.Gainsboro : style.ForeBrush;
                }
            }
            editor.Invalidate();
        }

        private static void CaptureChart(Chart chart)
        {
            foreach (var area in chart.ChartAreas)
            {
                var state = Snapshot(area);
                state.Capture("Back", () => area.BackColor, value => area.BackColor = value);
                state.Capture("SecondaryBack", () => area.BackSecondaryColor, value => area.BackSecondaryColor = value);
                state.Capture("Border", () => area.BorderColor, value => area.BorderColor = value);
                foreach (var axis in area.Axes)
                {
                    var axisState = Snapshot(axis);
                    axisState.Capture("Label", () => axis.LabelStyle.ForeColor, value => axis.LabelStyle.ForeColor = value);
                    axisState.Capture("Title", () => axis.TitleForeColor, value => axis.TitleForeColor = value);
                    axisState.Capture("Line", () => axis.LineColor, value => axis.LineColor = value);
                    axisState.Capture("MajorGrid", () => axis.MajorGrid.LineColor, value => axis.MajorGrid.LineColor = value);
                    axisState.Capture("MinorGrid", () => axis.MinorGrid.LineColor, value => axis.MinorGrid.LineColor = value);
                    axisState.Capture("MajorTick", () => axis.MajorTickMark.LineColor, value => axis.MajorTickMark.LineColor = value);
                    axisState.Capture("MinorTick", () => axis.MinorTickMark.LineColor, value => axis.MinorTickMark.LineColor = value);
                }
            }
            foreach (var legend in chart.Legends)
            {
                var state = Snapshot(legend);
                state.Capture("Back", () => legend.BackColor, value => legend.BackColor = value);
                state.Capture("Fore", () => legend.ForeColor, value => legend.ForeColor = value);
                state.Capture("Title", () => legend.TitleForeColor, value => legend.TitleForeColor = value);
            }
            foreach (var title in chart.Titles)
                Snapshot(title).Capture("Fore", () => title.ForeColor, value => title.ForeColor = value);
        }

        private static void ApplyChart(Chart chart)
        {
            CaptureChart(chart);
            foreach (var area in chart.ChartAreas)
            {
                Snapshot(area).Applied = true;
                area.BackColor = Palette.Surface;
                area.BackSecondaryColor = Palette.Surface;
                area.BorderColor = Palette.Border;
                foreach (var axis in area.Axes)
                {
                    Snapshot(axis).Applied = true;
                    axis.LabelStyle.ForeColor = Palette.Text;
                    axis.TitleForeColor = Palette.Text;
                    axis.LineColor = Palette.Border;
                    axis.MajorGrid.LineColor = Palette.Border;
                    axis.MinorGrid.LineColor = Palette.Border;
                    axis.MajorTickMark.LineColor = Palette.Border;
                    axis.MinorTickMark.LineColor = Palette.Border;
                }
            }
            foreach (var legend in chart.Legends)
            {
                Snapshot(legend).Applied = true;
                legend.BackColor = Palette.Background;
                legend.ForeColor = Palette.Text;
                legend.TitleForeColor = Palette.Text;
            }
            foreach (var title in chart.Titles)
            {
                Snapshot(title).Applied = true;
                title.ForeColor = Palette.Text;
            }
        }

        private static void RestoreSpecialControl(Control control)
        {
            if (control is FastColoredTextBox editor)
            {
                foreach (var style in controls.GetValue(editor, CreateState).EditorStyles)
                    Snapshot(style).Restore();
                controls.GetValue(editor, CreateState).EditorStyles.Clear();
                editor.Invalidate();
            }
            if (control is Chart chart)
            {
                foreach (var area in chart.ChartAreas)
                {
                    Snapshot(area).Restore();
                    foreach (var axis in area.Axes)
                        Snapshot(axis).Restore();
                }
                foreach (var legend in chart.Legends)
                    Snapshot(legend).Restore();
                foreach (var title in chart.Titles)
                    Snapshot(title).Restore();
            }
        }
    }
}
