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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    public class DataGridViewDeleteButtonCell : DataGridViewButtonCell
    {
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            var fillRectangle = new Rectangle(cellBounds.X, cellBounds.Y, cellBounds.Width - 1, cellBounds.Height - 1);
            var stringRectangle = new Rectangle(cellBounds.X + 5, cellBounds.Y + 3, cellBounds.Width - 8, cellBounds.Height - 8);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(215, 228, 242)),
                                   fillRectangle);
            graphics.DrawLine(new Pen(Color.FromArgb(153, 180, 209), 1),
                              new Point(cellBounds.X + cellBounds.Width - 1, cellBounds.Y),
                              new Point(cellBounds.X + cellBounds.Width - 1, cellBounds.Y + cellBounds.Height - 1));
            graphics.DrawLine(new Pen(Color.FromArgb(153, 180, 209), 1),
                              new Point(cellBounds.X, cellBounds.Y + cellBounds.Height - 1),
                              new Point(cellBounds.X + cellBounds.Width - 1, cellBounds.Y + cellBounds.Height - 1));
            //var pen = new Pen(new LinearGradientBrush(new Rectangle(1, 1, 1, 1), 
            //                  Color.FromArgb(0, 0, 0), 
            //                  Color.FromArgb(200, 200, 200), 
            //                  LinearGradientMode.Horizontal))
            //{
            //    DashCap = DashCap.Round,
            //    Width = 1
            //};

            //graphics.DrawLine(pen,
            //                 new Point(cellBounds.X + 7, cellBounds.Y + 7),
            //                 new Point(cellBounds.X + cellBounds.Width - 7, cellBounds.Y + cellBounds.Height - 7));
            //graphics.DrawLine(pen,
            //                 new Point(cellBounds.X + cellBounds.Width - 7, cellBounds.Y + 7),
            //                 new Point(cellBounds.X + 7, cellBounds.Y + cellBounds.Height - 7));
            graphics.DrawString("X",
                                new Font("Comic Sans MS", 9.0f, FontStyle.Regular),
                                new SolidBrush(Color.FromArgb(0, 0, 0)),
                                stringRectangle);
        }
    }
}
