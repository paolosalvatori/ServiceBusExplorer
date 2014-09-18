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

#region Using References
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public partial class HeaderPanel : Panel
    {
        #region Private Fields
        private int headerHeight = 24;
        private string headerText = "header title";
        private Font headerFont = new Font("Arial", 10F, System.Drawing.FontStyle.Bold);
        private Color headerColor1 = SystemColors.InactiveCaption;
        private Color headerColor2 = SystemColors.ActiveCaption;
        private Color iconTransparentColor = Color.White;
        private Image icon = null;
        #endregion

        #region Public Properties
        [Browsable(true), Category("Custom")]
        public string HeaderText
        {
            get { return headerText; }
            set
            {
                headerText = value;
                Invalidate();
            }
        }

        [Browsable(true), Category("Custom")]
        public int HeaderHeight
        {
            get { return headerHeight; }
            set
            {
                headerHeight = value;
                Invalidate();
            }
        }

        [Browsable(true), Category("Custom")]
        public Font HeaderFont
        {
            get { return headerFont; }
            set
            {
                headerFont = value;
                Invalidate();
            }
        }

        [Browsable(true), Category("Custom")]
        public Color HeaderColor1
        {
            get { return headerColor1; }
            set
            {
                headerColor1 = value;
                Invalidate();
            }
        }

        [Browsable(true), Category("Custom")]
        public Color HeaderColor2
        {
            get { return headerColor2; }
            set
            {
                headerColor2 = value;
                Invalidate();
            }
        }

        [Browsable(true), Category("Custom")]
        public Image Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                Invalidate();
            }
        }

        [Browsable(true), Category("Custom")]
        public Color IconTransparentColor
        {
            get { return iconTransparentColor; }
            set
            {
                iconTransparentColor = value;
                Invalidate();
            }
        }
        #endregion

        #region Public Constructors
        public HeaderPanel()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            this.Padding = new Padding(5, headerHeight + 4, 5, 4);
        }
        #endregion

        #region Private Methods
        private void OutlookPanelEx_Paint(object sender, PaintEventArgs e)
        {
            if (headerHeight > 1)
            {
                // Draw border;
                DrawBorder(e.Graphics);

                // Draw heaeder
                DrawHeader(e.Graphics);

                // Draw text
                DrawText(e.Graphics);

                // Draw Icon
                DrawIcon(e.Graphics);
            }
        }

        private void DrawBorder(Graphics graphics)
        {
            using (var pen = new Pen(this.headerColor2))
            {
                graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        private void DrawHeader(Graphics graphics)
        {
            var headerRect = new Rectangle(1, 1, this.Width - 2, this.headerHeight);
            using (Brush brush = new LinearGradientBrush(headerRect, headerColor1, headerColor2, LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(brush, headerRect);
            }
        }

        private void DrawText(Graphics graphics)
        {
            if (string.IsNullOrWhiteSpace(this.headerText)) return;
            var size = graphics.MeasureString(this.headerText, this.headerFont);
            using (Brush brush = new SolidBrush(ForeColor))
            {
                float x;
                if (this.icon != null)
                {
                    x = this.icon.Width + 6;
                }
                else
                {
                    x = 4;
                }
                graphics.DrawString(this.headerText, this.headerFont, brush, x, (headerHeight - size.Height) / 2);
            }
        }

        private void DrawIcon(Graphics graphics)
        {
            if (icon != null)
            {
                var point = new Point(4, (headerHeight - icon.Height) / 2);
                var bitmap = new Bitmap(icon);
                bitmap.MakeTransparent(iconTransparentColor);
                graphics.DrawImage(bitmap, point);
            }
        }
        #endregion
    }
}