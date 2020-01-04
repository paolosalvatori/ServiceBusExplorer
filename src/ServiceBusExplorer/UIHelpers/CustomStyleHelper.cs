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
using ServiceBusExplorer.Controls;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    using System;

    /// <summary>
	/// Summary description for DrawCustomStyleHelper.
	/// </summary>
	public static class CustomStyleHelper
	{
	    /// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		/// <param name="drawRectF"></param>
		/// <param name="drawColor"></param>
		/// <param name="orientation"></param>
		public static void DrawAquaPill(Graphics g, RectangleF drawRectF, Color drawColor, Orientation orientation)
		{
	        var colorBlend = new ColorBlend();

			var color1 = CustomColorHelper.OpacityMix(Color.White, CustomColorHelper.SoftLightMix(drawColor, Color.Black, 100), 40);
			var color2 = CustomColorHelper.OpacityMix(Color.White, CustomColorHelper.SoftLightMix(drawColor, CustomColorHelper.CreateColorFromRGB(64, 64, 64), 100), 20);
            var color3 = CustomColorHelper.SoftLightMix(drawColor, CustomColorHelper.CreateColorFromRGB(128, 128, 128), 100);
            var color4 = CustomColorHelper.SoftLightMix(drawColor, CustomColorHelper.CreateColorFromRGB(192, 192, 192), 100);
            var color5 = CustomColorHelper.OverlayMix(CustomColorHelper.SoftLightMix(drawColor, Color.White, 100), Color.White, 75);
			
			//			
			colorBlend.Colors = new[]{color1, color2, color3, color4, color5};
			colorBlend.Positions = new[]{0, 0.25f, 0.5f, 0.75f, 1};
			var gradientBrush = orientation == Orientation.Horizontal ? new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top-1), new Point((int)drawRectF.Left, (int)drawRectF.Top + (int)drawRectF.Height+1), color1, color5) : new LinearGradientBrush(new Point((int)drawRectF.Left-1, (int)drawRectF.Top), new Point((int)drawRectF.Left + (int)drawRectF.Width+1, (int)drawRectF.Top), color1, color5);
			gradientBrush.InterpolationColors = colorBlend;
			FillPill(gradientBrush, drawRectF, g);

			//
			color2 = Color.White;
			colorBlend.Colors = new[]{color2, color3, color4, color5};
			colorBlend.Positions = new[]{0, 0.5f, 0.75f, 1};
			gradientBrush = orientation == Orientation.Horizontal ? new LinearGradientBrush(new Point((int)drawRectF.Left + 1, (int)drawRectF.Top), new Point((int)drawRectF.Left + 1, (int)drawRectF.Top + (int)drawRectF.Height - 1), color2, color5) : new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top + 1), new Point((int)drawRectF.Left + (int)drawRectF.Width - 1, (int)drawRectF.Top + 1), color2, color5);
			gradientBrush.InterpolationColors = colorBlend;
			FillPill(gradientBrush, RectangleF.Inflate(drawRectF,-3,-3), g);

		}

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="g"></param>
	    /// <param name="drawRectF"></param>
	    /// <param name="drawColor"></param>
	    /// <param name="orientation"></param>
	    /// <param name="brushStyle"></param>
	    public static void DrawAquaPillSingleLayer(Graphics g, RectangleF drawRectF, Color drawColor, Orientation orientation, BrushStyle brushStyle)
		{
		    if (brushStyle == BrushStyle.LinearGradient)
		    {
		        var colorBlend = new ColorBlend();
		        var color1 = drawColor;
                var color2 = ControlPaint.Light(color1);
                var color3 = ControlPaint.Light(color2);
                var color4 = ControlPaint.Light(color3);

		        colorBlend.Colors = new[] {color1, color2, color3, color4};
		        colorBlend.Positions = new[] {0, 0.25f, 0.65f, 1};

		        var gradientBrush = orientation == Orientation.Horizontal
		                                                ? new LinearGradientBrush(
		                                                      new Point((int) drawRectF.Left, (int) drawRectF.Top),
		                                                      new Point((int) drawRectF.Left,
		                                                                (int) drawRectF.Top + (int) drawRectF.Height), color1,
		                                                      color4)
		                                                : new LinearGradientBrush(
		                                                      new Point((int) drawRectF.Left, (int) drawRectF.Top),
		                                                      new Point((int) drawRectF.Left + (int) drawRectF.Width,
		                                                                (int) drawRectF.Top), color1, color4);
		        gradientBrush.InterpolationColors = colorBlend;

		        FillPill(gradientBrush, drawRectF, g);
		    }
		    else
		    {
                FillPill(new SolidBrush(drawColor), drawRectF, g);
		    }
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="b"></param>
		/// <param name="rect"></param>
		/// <param name="g"></param>
		private static void FillPill(Brush b, RectangleF rect, Graphics g)
		{
			if (rect.Width > rect.Height) 
			{
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.FillEllipse(b, new RectangleF(rect.Left, rect.Top, rect.Height, rect.Height));
				g.FillEllipse(b, new RectangleF(rect.Left + rect.Width - rect.Height, rect.Top, rect.Height, rect.Height));				

				var w = rect.Width - rect.Height;
				var l = rect.Left + ((rect.Height)/ 2);
				g.FillRectangle(b, new RectangleF(l, rect.Top, w, rect.Height));
				g.SmoothingMode = SmoothingMode.Default;
			} 
			else if (rect.Width < rect.Height) 
			{
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.FillEllipse(b, new RectangleF(rect.Left, rect.Top, rect.Width, rect.Width));
				g.FillEllipse(b, new RectangleF(rect.Left, rect.Top + rect.Height - rect.Width, rect.Width, rect.Width));				

				var t = rect.Top + (rect.Width/ 2);
				var h = rect.Height - rect.Width;
				g.FillRectangle (b, new RectangleF(rect.Left, t, rect.Width, h));				
				g.SmoothingMode = SmoothingMode.Default;
			} 
			else if (Math.Abs(rect.Width - rect.Height) < 0.1) // floats equality
			{
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.FillEllipse(b, rect);
				g.SmoothingMode = SmoothingMode.Default;
			}
		}

	}
}
