using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KIS.Controls.Windows
{
    [ToolboxBitmap (@"HeaderPanel.offScreenBmp")]
	public partial class HeaderPanel : Panel
    {
        private const int DEFAULT_BORDER_WIDTH = 1;
        private const int DEFAULT_SHADOW_WIDTH = 5;

        // Window message constants
        private const int WM_NCCALCSIZE = 0x0083;
        private const int WM_NCPAINT = 0x0085;
        
        #region Member Variables

        private int mint_CaptionHeight;
        private bool mbln_CaptionVisible = true;
        private bool mbln_Antialias = true;
        private string mstr_CaptionText;
        private Color mclr_CaptionBeginColor;
        private Color mclr_CaptionEndColor;
        private LinearGradientMode menm_CaptionGradientMode;
        private CaptionPositions menm_CaptionPosition;

        private Icon mico_Icon = null;
        private bool mbln_IconVisible = false;

        private BorderStyles menm_BorderStyle;
        private Color mclr_BorderColor;

        private Color mclr_StartColor;
        private Color mclr_EndColor;
        private LinearGradientMode menm_GradientMode;

        #endregion

        #region Constructor

        public HeaderPanel() : base()
		{
			InitializeComponent();

            this.Font = new Font(SystemFonts.CaptionFont, 
                                SystemFonts.CaptionFont.Style);
            this.mstr_CaptionText = this.GetType().Name;
            this.mint_CaptionHeight = SystemInformation.CaptionHeight;
            this.mclr_CaptionBeginColor = Color.FromKnownColor(KnownColor.InactiveCaption);
            this.mclr_CaptionEndColor = Color.FromKnownColor(KnownColor.ActiveCaption);
            this.menm_CaptionGradientMode = LinearGradientMode.Vertical;
            this.menm_CaptionPosition = CaptionPositions.Top;
            
            this.menm_BorderStyle = BorderStyles.Shadow;
            base.BorderStyle = 0;
            this.mclr_BorderColor = Color.FromKnownColor(KnownColor.ActiveCaption);

            this.mclr_StartColor = Color.FromKnownColor(KnownColor.Window);
            this.mclr_EndColor = Color.FromKnownColor(KnownColor.Window);
            this.menm_GradientMode = LinearGradientMode.Vertical;

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        #endregion

        #region Properties

        [Description("Change the starting color of gradient background"), Category("Gradient"), Browsable(true)]
        public Color GradientStart
        {
            get
            {
                return mclr_StartColor;
            }
            set
            {
                mclr_StartColor = value;
                Invalidate();
            }
        }

        [Description("Change the end color of gradient background"), Category("Gradient"), Browsable(true)]
        public Color GradientEnd
        {
            get
            {
                return mclr_EndColor;
            }
            set
            {
                mclr_EndColor = value;
                Invalidate();
            }
        }

        [Description("Change the background gradient direction"), Category("Gradient"), Browsable(true)]
        public LinearGradientMode GradientDirection
        {
            get
            {
                return menm_GradientMode;
            }
            set
            {
                menm_GradientMode = value;
                Invalidate();
            }
        }

        [Description("Change style of the border"), Category("Border"), Browsable(true)]
        public new BorderStyles BorderStyle
        {
            get
            {
                return menm_BorderStyle;
            }
            set
            {
                menm_BorderStyle = value;
                PerformLayout();
                Invalidate();
            }
        }

        [Description("Change color of the border"), Category("Border"), Browsable(true)]
        public Color BorderColor
        {
            get
            {
                return mclr_BorderColor;
            }
            set
            {
                mclr_BorderColor = value;
                Invalidate();
            }
        }

        [Description("Enable/Disable antialiasing"), Category("Caption"), Browsable(true)]
        public bool TextAntialias
        {
            get
            {
                return mbln_Antialias;
            }
            set
            {
                mbln_Antialias = value;
                Invalidate();
            }
        }

        [Description("Change the caption text to be displayed"), Category("Caption"), Browsable(true)]
        public String CaptionText
        {
            get
            {
                return mstr_CaptionText;
            }
            set
            {
                mstr_CaptionText = value;
                Invalidate();
            }
        }

        [Description("Show or hide the caption"), Category("Caption"), Browsable(true)]
        public bool CaptionVisible
        {
            get
            {
                return mbln_CaptionVisible;
            }
            set
            {
                mbln_CaptionVisible = value;
                Invalidate();
            }
        }

        [Description("Change the caption height"), Category("Caption"), Browsable(true)]
        public int CaptionHeight
        {
            get
            {
                return mint_CaptionHeight;
            }
            set
            {
                mint_CaptionHeight = value;
                mbln_IconVisible = (mint_CaptionHeight >= 16);
                Invalidate();
            }
        }

        [Description("Change the caption gradient direction"), Category("Caption"), Browsable(true)]
        public LinearGradientMode CaptionGradientDirection
        {
            get
            {
                return menm_CaptionGradientMode;
            }
            set
            {
                menm_CaptionGradientMode = value;
                Invalidate();
            }
        }

        [Description("Change the caption gradient's start color"), Category("Caption"), Browsable(true)]
        public Color CaptionBeginColor
        {
            get
            {
                return mclr_CaptionBeginColor;
            }
            set
            {
                mclr_CaptionBeginColor = value;
                Invalidate();
            }
        }

        [Description("Change the caption gradient's end color"), Category("Caption"), Browsable(true)]
        public Color CaptionEndColor
        {
            get
            {
                return mclr_CaptionEndColor;
            }
            set
            {
                mclr_CaptionEndColor = value;
                Invalidate();
            }
        }

        [Description("Change the caption position"), Category("Caption"), Browsable(true)]
        public CaptionPositions CaptionPosition
        {
            get
            {
                return this.menm_CaptionPosition;
            }
            set
            {
                this.menm_CaptionPosition = value;
                Invalidate();
            }
        }

        [Description("Change the caption font"), Category("Caption"), Browsable(true)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                if (value.GetHeight() > mint_CaptionHeight)
                    mint_CaptionHeight = Convert.ToInt32(value.GetHeight() + 8.0f);
                Invalidate();
            }
        }

        [Description("Change the icon to display in the title"), Category("Icon"), Browsable(true)]
        public Icon PanelIcon
        {
            get
            {
                return mico_Icon;
            }
            set
            {
                mico_Icon = value;
                Invalidate();
            }
        }

        [Description("Enable/Disable the icon"), Category("Icon"), Browsable(true)]
        public bool PanelIconVisible
        {
            get
            {
                return mbln_IconVisible;
            }
            set
            {
                mbln_IconVisible = value && (mint_CaptionHeight >= 16);
                Invalidate();
            }
        }

        #endregion

        #region Overrided Methods

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCCALCSIZE:
                    WmNCCalcSize(ref m);
                    break;
                case WM_NCPAINT:
                    IntPtr hDC = NativeMethods.GetWindowDC(m.HWnd);
                    if (hDC != IntPtr.Zero)
                    {
                        using (Graphics canvas = Graphics.FromHdc(hDC))
                        {
                            PaintNonClientArea(canvas);
                        }
                        NativeMethods.ReleaseDC(m.HWnd, hDC);
                    }
                    m.Result = IntPtr.Zero;
                    break;
            }
            base.WndProc(ref m);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width == 0 || Height == 0) return;
            if (BackgroundImage != null)
                base.OnPaint(e);
            else
            {
                base.OnPaint(e);
                DrawBackground(e, menm_BorderStyle);
            }
        }
        #endregion

        #region Event Handlers
        private void HeaderPanel_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }
        #endregion

        #region Helper Routines

        private void WmNCCalcSize(ref Message m)
        {
            if (m.WParam == NativeMethods.FALSE)
            {
                NativeMethods.RECT ncRect = (NativeMethods.RECT)m.GetLParam(typeof(NativeMethods.RECT));
                Rectangle proposed = ncRect.Rect;
                RecalcNonClientArea(ref proposed);
                ncRect = NativeMethods.RECT.FromRectangle(proposed);
                Marshal.StructureToPtr(ncRect, m.LParam, false);
            }
            else if (m.WParam == NativeMethods.TRUE)
            {
                NativeMethods.NCCALCSIZE_PARAMS ncParams =
                    (NativeMethods.NCCALCSIZE_PARAMS)m.GetLParam(typeof(NativeMethods.NCCALCSIZE_PARAMS));
                Rectangle proposed = ncParams.rectProposed.Rect;
                RecalcNonClientArea(ref proposed);
                ncParams.rectProposed = NativeMethods.RECT.FromRectangle(proposed);
                Marshal.StructureToPtr(ncParams, m.LParam, false);
            }
            m.Result = IntPtr.Zero;
        }

        private void RecalcNonClientArea(ref Rectangle proposed)
        {
            int brdWidth = 0;
            int shadowWidth = 0;
            int captionHight = 0;

            brdWidth = ComputeBorderWidth();
            if (menm_BorderStyle == BorderStyles.Shadow)
                shadowWidth = DEFAULT_SHADOW_WIDTH;
            if (mbln_CaptionVisible)
                captionHight = mint_CaptionHeight;

            switch (menm_CaptionPosition)
            {
                case CaptionPositions.Top:
                    proposed = new Rectangle(proposed.Left + brdWidth, 
                                    proposed.Top + (brdWidth + captionHight),
                                    proposed.Width - (brdWidth + brdWidth + shadowWidth),
                                    proposed.Height - (brdWidth + brdWidth + shadowWidth + 
                                                        captionHight));
                    break;
                case CaptionPositions.Bottom:
                    proposed = new Rectangle(proposed.Left + brdWidth, 
                                    proposed.Top + brdWidth,
                                    proposed.Width - (brdWidth + brdWidth + shadowWidth),
                                    proposed.Height - (brdWidth + brdWidth + shadowWidth + 
                                                        captionHight));
                    break;
                case CaptionPositions.Left:
                    proposed = new Rectangle(proposed.Left + (brdWidth + captionHight), 
                                    proposed.Top + brdWidth,
                                    proposed.Width - (brdWidth + brdWidth + shadowWidth),
                                    proposed.Height - (brdWidth + brdWidth + shadowWidth));
                    break;
                case CaptionPositions.Right:
                    proposed = new Rectangle(proposed.Left + brdWidth, 
                                    proposed.Top + brdWidth,
                                    proposed.Width - (brdWidth + brdWidth + shadowWidth + 
                                                captionHight),
                                    proposed.Height - (brdWidth + brdWidth + shadowWidth));
                    break;
            }
        }

        private void PaintNonClientArea(Graphics canvas)
        {
            if (menm_BorderStyle == BorderStyles.Shadow)
                DrawShadow(canvas);

            if (menm_BorderStyle != BorderStyles.None)
                DrawBorder(canvas);

            if (mbln_CaptionVisible)
                DrawCaption(canvas);
        }

        private void DrawShadow(Graphics canvas)
        {
            Pen pen = null;
            Color[] clrArr = null;

            try
            {
                clrArr = new Color[] {Color.FromArgb(142, 142, 142), 
                                    Color.FromArgb(171, 171, 171),
                                    Color.FromArgb(212, 212, 212), 
                                    Color.FromArgb(220, 220, 220)};
                for (int cntr = 0; cntr < 4; cntr++)
                {
                    pen = new Pen(clrArr[cntr], 1.0f);
                    canvas.DrawLine(pen, Width - (5 - cntr), ((cntr + 1) * 2),
                                            Width - (5 - cntr), Height - (5 - cntr));
                    canvas.DrawLine(pen, ((cntr + 1) * 2), Height - (5 - cntr), 
                                            Width - (5 - cntr), Height - (5 - cntr));
                    pen.Dispose();
                    pen = null;
                }
            }
            finally
            {
                if (pen != null)
                {
                    pen.Dispose();
                    pen = null;
                }
                clrArr = null;
            }
        }

        private void DrawCaption(Graphics canvas)
        {
            int brdWidth = 0;
            int shadowWidth = 0;
            Bitmap offScreenBmp = null;
            Graphics offScreenDC = null;
            Rectangle drawRect;
            StringFormat format = null;
            LinearGradientBrush brsh = null;

            try
            {
                if (menm_BorderStyle == BorderStyles.None)
                    brdWidth = 0;
                else if (menm_BorderStyle == BorderStyles.Groove ||
                        menm_BorderStyle == BorderStyles.Ridge)
                    brdWidth = 2;
                else
                    brdWidth = DEFAULT_BORDER_WIDTH;

                if (menm_BorderStyle == BorderStyles.Shadow)
                    shadowWidth = DEFAULT_SHADOW_WIDTH;

                format = new StringFormat();
                format.FormatFlags = StringFormatFlags.NoWrap;
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Near;
                format.Trimming = StringTrimming.EllipsisCharacter;

                if (menm_CaptionPosition == CaptionPositions.Bottom || 
                    menm_CaptionPosition == CaptionPositions.Top)
                    offScreenBmp = new Bitmap(Bounds.Width - ((2 * brdWidth) + shadowWidth), 
                                                mint_CaptionHeight);
                else
                    offScreenBmp = new Bitmap(Bounds.Height - ((2 * brdWidth) + shadowWidth),
                                                mint_CaptionHeight);

                offScreenDC = Graphics.FromImage(offScreenBmp);
                if (mbln_Antialias)
                    offScreenDC.TextRenderingHint = TextRenderingHint.AntiAlias;

                brsh = new LinearGradientBrush(new Rectangle(0, 0, 
                                                    offScreenBmp.Width, 
                                                    offScreenBmp.Height), 
                                                mclr_CaptionBeginColor,
                                                mclr_CaptionEndColor, 
                                                menm_CaptionGradientMode);
                offScreenDC.FillRectangle(brsh, 0.0f, 0.0f, offScreenBmp.Width,
                                            offScreenBmp.Height);
                if (mico_Icon != null && mbln_IconVisible)
                {
                    offScreenDC.DrawIcon(mico_Icon, 
                                        new Rectangle(brdWidth + 2, 
                                                (mint_CaptionHeight - 16) / 2, 
                                                16, 16));
                    offScreenDC.DrawString(mstr_CaptionText, this.Font,
                                            new SolidBrush(this.ForeColor),
                                            new Rectangle(20, 0, 
                                                    offScreenBmp.Width - 20,
                                                    mint_CaptionHeight), format);
                }
                else
                {
                    offScreenDC.DrawString(mstr_CaptionText, this.Font,
                                            new SolidBrush(this.ForeColor),
                                            new Rectangle(2, 0,
                                                    offScreenBmp.Width,
                                                    mint_CaptionHeight), format);
                }

                drawRect = new Rectangle();
                switch (menm_CaptionPosition)
                {
                    case CaptionPositions.Top:
                        {
                            drawRect.X = brdWidth;
                            drawRect.Width = DisplayRectangle.Width;
                            drawRect.Y = brdWidth;
                            drawRect.Height = mint_CaptionHeight;
                            break;
                        }
                    case CaptionPositions.Bottom:
                        {
                            drawRect.X = brdWidth;
                            drawRect.Width = DisplayRectangle.Width;
                            drawRect.Y = Height - (shadowWidth + brdWidth + 
                                                    mint_CaptionHeight);
                            drawRect.Height = mint_CaptionHeight;
                            break;
                        }
                    case CaptionPositions.Left:
                        {
                            drawRect.X = brdWidth;
                            drawRect.Width = mint_CaptionHeight;
                            drawRect.Y = brdWidth;
                            drawRect.Height = Height - (shadowWidth + (2 * brdWidth));
                            offScreenBmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                        }
                    case CaptionPositions.Right:
                        {
                            drawRect.X = Width - (shadowWidth + brdWidth + mint_CaptionHeight);
                            drawRect.Width = mint_CaptionHeight;
                            drawRect.Y = brdWidth;
                            drawRect.Height = Height - (shadowWidth + (2 * brdWidth));
                            offScreenBmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        }
                }
                canvas.DrawImage(offScreenBmp, drawRect);
            }
            finally
            {
                if (brsh != null)
                {
                    brsh.Dispose();
                    brsh = null;
                }

                if (offScreenDC != null)
                {
                    offScreenDC.Dispose();
                    offScreenDC = null;
                }

                if (offScreenBmp != null)
                {
                    offScreenBmp.Dispose();
                    offScreenBmp = null;
                }

                if (format != null)
                {
                    format.Dispose();
                    format = null;
                }
            }
        }

        private void DrawBackground(PaintEventArgs peArgs, BorderStyles brdStyle)
        {
            LinearGradientBrush brsh = null;

            try
            {
                if (menm_BorderStyle == BorderStyles.Shadow)
                {
                    brsh = new LinearGradientBrush(new Rectangle(0, 0, Width - DEFAULT_SHADOW_WIDTH,
                                                    Height - DEFAULT_SHADOW_WIDTH), mclr_StartColor,
                                                    mclr_EndColor, menm_GradientMode);
                    peArgs.Graphics.FillRectangle(brsh, 0, 0,
                                                    Width - DEFAULT_SHADOW_WIDTH,
                                                    Height - DEFAULT_SHADOW_WIDTH);
                }
                else
                {
                    brsh = new LinearGradientBrush(new Rectangle(0, 0, Width,
                                                    Height), mclr_StartColor,
                                                    mclr_EndColor, menm_GradientMode);
                    peArgs.Graphics.FillRectangle(brsh, 0, 0, Width, Height);
                }
            }
            finally
            {
                if (brsh != null)
                {
                    brsh.Dispose();
                    brsh = null;
                }
            }
        }

        private void DrawBorder(Graphics canvas)
        {
            Pen penDark = null;
            Pen penLight = null;

            try
            {
                switch (menm_BorderStyle)
                {
                    case BorderStyles.Shadow:
                        penDark = new Pen(mclr_BorderColor);
                        canvas.DrawRectangle(penDark, 0, 0, Width - 6, 
                                                    Height - 6);
                        break;
                    case BorderStyles.Single:
                        penDark = new Pen(mclr_BorderColor);
                        canvas.DrawRectangle(penDark, 0, 0,
                                            Width - DEFAULT_BORDER_WIDTH,
                                            Height - DEFAULT_BORDER_WIDTH);
                        break;
                    case BorderStyles.Inset:
                        penDark = new Pen(SystemColors.ButtonShadow);
                        penLight = new Pen(SystemColors.ButtonHighlight);
                        canvas.DrawLine(penLight, 0, 0, 
                                                Width - DEFAULT_BORDER_WIDTH, 0);
                        canvas.DrawLine(penLight, 0, 0, 0,
                                            Height - DEFAULT_BORDER_WIDTH);
                        canvas.DrawLine(penDark, Width - DEFAULT_BORDER_WIDTH,
                                                0, Width - DEFAULT_BORDER_WIDTH,
                                                Height - DEFAULT_BORDER_WIDTH);
                        canvas.DrawLine(penDark, 0, Height - DEFAULT_BORDER_WIDTH,
                                                Width - DEFAULT_BORDER_WIDTH,
                                                Height - DEFAULT_BORDER_WIDTH);
                        break;
                    case BorderStyles.Outset:
                        penDark = new Pen(SystemColors.ButtonShadow);
                        penLight = new Pen(SystemColors.ButtonHighlight);
                        canvas.DrawLine(penDark, 0, 0,
                                                Width - DEFAULT_BORDER_WIDTH, 0);
                        canvas.DrawLine(penDark, 0, 0, 0,
                                            Height - DEFAULT_BORDER_WIDTH);
                        canvas.DrawLine(penLight, Width - DEFAULT_BORDER_WIDTH,
                                                0, Width - DEFAULT_BORDER_WIDTH,
                                                Height - DEFAULT_BORDER_WIDTH);
                        canvas.DrawLine(penLight, 0, Height - DEFAULT_BORDER_WIDTH,
                                                Width - DEFAULT_BORDER_WIDTH,
                                                Height - DEFAULT_BORDER_WIDTH);
                        break;
                    case BorderStyles.Groove:
                        penDark = new Pen(SystemColors.ButtonShadow);
                        penLight = new Pen(SystemColors.ButtonHighlight);
                        canvas.DrawRectangle(penLight,
                                new Rectangle(1, 1, Width - (2 * DEFAULT_BORDER_WIDTH),
                                                Height - (2 * DEFAULT_BORDER_WIDTH)));
                        canvas.DrawRectangle(penDark, 
                                new Rectangle(0, 0, Width - (2 * DEFAULT_BORDER_WIDTH),
                                                Height - (2 * DEFAULT_BORDER_WIDTH)));
                        break;
                    case BorderStyles.Ridge:

                        penDark = new Pen(SystemColors.ButtonShadow);
                        penLight = new Pen(SystemColors.ButtonHighlight);
                        canvas.DrawRectangle(penLight, 
                                new Rectangle(0, 0, Width - (2 * DEFAULT_BORDER_WIDTH),
                                                Height - (2 * DEFAULT_BORDER_WIDTH)));
                        canvas.DrawRectangle(penDark,
                                new Rectangle(1, 1, Width - (2 * DEFAULT_BORDER_WIDTH),
                                                Height - (2 * DEFAULT_BORDER_WIDTH)));
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                if (penDark != null)
                {
                    penDark.Dispose();
                    penDark = null;
                }
                if (penLight != null)
                {
                    penLight.Dispose();
                    penLight = null;
                }
            }
        }

        /// <summary>
        /// Helper function to compute the border width
        /// </summary>
        /// <returns>The border width</returns>
        private int ComputeBorderWidth()
        {
            int brdWidth = 0;

            switch (menm_BorderStyle)
            {
                case BorderStyles.Single:
                case BorderStyles.Shadow:
                case BorderStyles.Inset:
                case BorderStyles.Outset:
                    brdWidth = DEFAULT_BORDER_WIDTH;
                    break;
                case BorderStyles.Groove:
                case BorderStyles.Ridge:
                    brdWidth = (DEFAULT_BORDER_WIDTH + DEFAULT_BORDER_WIDTH);
                    break;
                default:
                    break;
            }
            return brdWidth;
        }

        #endregion
    }
}
