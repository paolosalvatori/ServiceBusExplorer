#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System.Drawing;
using System.Windows.Forms;
using ServiceBusExplorer.Helpers;
using System.Drawing.Text;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    using System;

    public static class TabControlHelper
    {
        public static void DrawTabControlTabs(TabControl tabControl, DrawItemEventArgs e, ImageList images)
        {
            if (ThemeManager.IsDark)
            {
                DrawDarkTabs(tabControl, e, images);
                return;
            }

            // Get the bounding end of tab strip rectangles.
            var tabstripEndRect = tabControl.GetTabRect(tabControl.TabPages.Count - 1);
            var tabstripEndRectF = new RectangleF(tabstripEndRect.X + tabstripEndRect.Width, tabstripEndRect.Y - 5,
            tabControl.Width - (tabstripEndRect.X + tabstripEndRect.Width), tabstripEndRect.Height + 5);
            var leftVerticalLineRect = new RectangleF(2, tabstripEndRect.Y + tabstripEndRect.Height + 2, 2, tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var rightVerticalLineRect = new RectangleF(tabControl.TabPages[tabControl.SelectedIndex].Width + 4, tabstripEndRect.Y + tabstripEndRect.Height + 2, 2, tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var bottomHorizontalLineRect = new RectangleF(2, tabstripEndRect.Y + tabstripEndRect.Height + tabControl.TabPages[tabControl.SelectedIndex].Height + 2, tabControl.TabPages[tabControl.SelectedIndex].Width + 4, 2);
            RectangleF leftVerticalBarNearFirstTab = new Rectangle(0, 0, 2, tabstripEndRect.Height + 2);

            // First, do the end of the tab strip.
            // If we have an image use it.
            if (tabControl.Parent.BackgroundImage != null)
            {
                var src = new RectangleF(tabstripEndRectF.X + tabControl.Left, tabstripEndRectF.Y + tabControl.Top, tabstripEndRectF.Width, tabstripEndRectF.Height);
                e.Graphics.DrawImage(tabControl.Parent.BackgroundImage, tabstripEndRectF, src, GraphicsUnit.Pixel);
            }
            // If we have no image, use the background color.
            else
            {
                using (Brush backBrush = new SolidBrush(tabControl.Parent.BackColor))
                {
                    e.Graphics.FillRectangle(backBrush, tabstripEndRectF);
                    e.Graphics.FillRectangle(backBrush, leftVerticalLineRect);
                    e.Graphics.FillRectangle(backBrush, rightVerticalLineRect);
                    e.Graphics.FillRectangle(backBrush, bottomHorizontalLineRect);
                    if (tabControl.SelectedIndex != 0)
                    {
                        e.Graphics.FillRectangle(backBrush, leftVerticalBarNearFirstTab);
                    }
                }
            }

            // Set up the page and the various pieces.
            var page = tabControl.TabPages[e.Index];
            using (var backBrush = new SolidBrush(page.BackColor))
            {
                using (var foreBrush = new SolidBrush(page.ForeColor))
                {
                    var tabName = page.Text;

                    // Set up the offset for an icon, the bounding rectangle and image size and then fill the background.
                    var iconOffset = 0;
                    Rectangle tabBackgroundRect;

                    if (e.Index == tabControl.SelectedIndex)
                    {
                        tabBackgroundRect = e.Bounds;
                        e.Graphics.FillRectangle(backBrush, tabBackgroundRect);
                    }
                    else
                    {
                        tabBackgroundRect = new Rectangle(e.Bounds.X, e.Bounds.Y - 2, e.Bounds.Width,
                                                          e.Bounds.Height + 4);
                        e.Graphics.FillRectangle(backBrush, tabBackgroundRect);
                        var rect = new Rectangle(e.Bounds.X - 2, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X - 1, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X + e.Bounds.Width, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                        rect = new Rectangle(e.Bounds.X + e.Bounds.Width + 1, e.Bounds.Y - 2, 1, 2);
                        e.Graphics.FillRectangle(backBrush, rect);
                    }

                    // If we have images, process them.
                    if (images != null)
                    {
                        // Get size and image.
                        var size = images.ImageSize;
                        Image icon = null;
                        if (page.ImageIndex > -1)
                            icon = images.Images[page.ImageIndex];
                        else if (page.ImageKey != "")
                            icon = images.Images[page.ImageKey];

                        // If there is an image, use it.
                        if (icon != null)
                        {
                            var startPoint =
                                new Point(tabBackgroundRect.X + 2 + ((tabBackgroundRect.Height - size.Height) / 2),
                                          tabBackgroundRect.Y + 2 + ((tabBackgroundRect.Height - size.Height) / 2));
                            e.Graphics.DrawImage(icon, new Rectangle(startPoint, size));
                            iconOffset = size.Width + 4;
                        }
                    }

                    // Draw out the label.
                    var labelRect = new Rectangle(tabBackgroundRect.X + iconOffset, tabBackgroundRect.Y + 5,
                                                  tabBackgroundRect.Width - iconOffset, tabBackgroundRect.Height - 3);
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center, HotkeyPrefix = HotkeyPrefix.Show })
                    {
                        e.Graphics.DrawString(tabName, new Font(e.Font.FontFamily, 8.25F, e.Font.Style), foreBrush, labelRect, sf);
                    }
                }
            }
        }

        private static void DrawDarkTabs(TabControl tabControl, DrawItemEventArgs e, ImageList images)
        {
            if (tabControl.TabPages.Count == 0 || tabControl.SelectedIndex < 0)
            {
                return;
            }

            var tabstripEndRect = tabControl.GetTabRect(tabControl.TabPages.Count - 1);
            var tabstripEndRectF = new RectangleF(tabstripEndRect.X + tabstripEndRect.Width, tabstripEndRect.Y - 5,
                tabControl.Width - (tabstripEndRect.X + tabstripEndRect.Width), tabstripEndRect.Height + 5);
            var leftVerticalLineRect = new RectangleF(2, tabstripEndRect.Y + tabstripEndRect.Height + 2, 2,
                tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var rightVerticalLineRect = new RectangleF(tabControl.TabPages[tabControl.SelectedIndex].Width + 4,
                tabstripEndRect.Y + tabstripEndRect.Height + 2, 2,
                tabControl.TabPages[tabControl.SelectedIndex].Height + 2);
            var bottomHorizontalLineRect = new RectangleF(2,
                tabstripEndRect.Y + tabstripEndRect.Height + tabControl.TabPages[tabControl.SelectedIndex].Height + 2,
                tabControl.TabPages[tabControl.SelectedIndex].Width + 4, 2);
            RectangleF leftVerticalBarNearFirstTab = new Rectangle(0, 0, 2, tabstripEndRect.Height + 2);

            var page = tabControl.TabPages[e.Index];
            var selected = e.Index == tabControl.SelectedIndex;
            var tabBounds = selected
                ? new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height + 1)
                : new Rectangle(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2);
            var pageBounds = tabControl.DisplayRectangle;
            var selectedTabBounds = tabControl.GetTabRect(tabControl.SelectedIndex);
            var tabBackColor = selected ? ThemeManager.SurfaceLighter : ThemeManager.Surface;
            var tabBorderColor = selected ? ThemeManager.ForegroundDim : ThemeManager.Border;
            var textColor = selected ? ThemeManager.Foreground : ThemeManager.ForegroundDim;

            var stripColor = tabControl.Parent != null ? tabControl.Parent.BackColor : ThemeManager.Background;
            using (var stripBrush = new SolidBrush(stripColor))
            {
                e.Graphics.FillRectangle(stripBrush, tabstripEndRectF);
                e.Graphics.FillRectangle(stripBrush, leftVerticalLineRect);
                e.Graphics.FillRectangle(stripBrush, rightVerticalLineRect);
                e.Graphics.FillRectangle(stripBrush, bottomHorizontalLineRect);
                if (tabControl.SelectedIndex != 0)
                {
                    e.Graphics.FillRectangle(stripBrush, leftVerticalBarNearFirstTab);
                }
            }

            using (var backBrush = new SolidBrush(tabBackColor))
            using (var borderPen = new Pen(tabBorderColor))
            using (var textBrush = new SolidBrush(textColor))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, HotkeyPrefix = HotkeyPrefix.Show })
            {
                e.Graphics.FillRectangle(backBrush, tabBounds);
                e.Graphics.DrawRectangle(borderPen, tabBounds);

                if (selected)
                {
                    using (var innerPen = new Pen(ThemeManager.SurfaceLight))
                    {
                        e.Graphics.DrawLine(innerPen, tabBounds.Left + 1, tabBounds.Top + 1, tabBounds.Right - 2, tabBounds.Top + 1);
                    }
                }

                e.Graphics.DrawString(page.Text, new Font(e.Font.FontFamily, 8.25F, e.Font.Style), textBrush, tabBounds, sf);
            }

            using (var contentPen = new Pen(ThemeManager.Border))
            {
                e.Graphics.DrawLine(contentPen, pageBounds.Left - 1, selectedTabBounds.Bottom, pageBounds.Left - 1, pageBounds.Bottom);
                e.Graphics.DrawLine(contentPen, pageBounds.Right, selectedTabBounds.Bottom, pageBounds.Right, pageBounds.Bottom);
                e.Graphics.DrawLine(contentPen, pageBounds.Left - 1, pageBounds.Bottom, pageBounds.Right, pageBounds.Bottom);
                e.Graphics.DrawLine(contentPen, 0, pageBounds.Top - 1, selectedTabBounds.Left - 2, pageBounds.Top - 1);
                e.Graphics.DrawLine(contentPen, selectedTabBounds.Right + 1, pageBounds.Top - 1, pageBounds.Right, pageBounds.Top - 1);
            }
        }

        static void DrawTitles(TabControl tabControl, DrawItemEventArgs e)
        {
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.HotkeyPrefix = HotkeyPrefix.Show;

            if (tabControl.SelectedIndex == e.Index)
            { 
                e.Graphics.FillRectangle(SystemBrushes.Control, e.Bounds);
            }

            e.Graphics.DrawString(tabControl.TabPages[e.Index].Text, tabControl.Font, 
                SystemBrushes.ControlText, e.Bounds, sf);
            sf.Dispose();
        }
    }
}
