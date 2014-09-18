#region Copyright
//=======================================================================================
// [PLEASE DO NOT MODIFY THIS HEADER INFORMATION]---------------------
// Title: Grouper
// Description: A rounded groupbox with special painting features. 
// Date Created: December 17, 2005
// Author: Adam Smith
// Author Email: ibulwark@hotmail.com
// Websites: http://www.ebadgeman.com | http://www.codevendor.com
//  
// Version History:
// 1.0a - Beta Version - Release Date: December 17, 2005 
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
	/// <summary>A special custom rounding GroupBox with many painting features.</summary>
	[ToolboxBitmap(typeof(Grouper), "CodeVendor.Controls.Grouper.bmp")]
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
	public class Grouper : UserControl
	{
		#region Enumerations

		/// <summary>A special gradient enumeration.</summary>
		public enum GroupBoxGradientMode
		{
			/// <summary>Specifies no gradient mode.</summary>
			None = 4,

			/// <summary>Specifies a gradient from upper right to lower left.</summary>
			BackwardDiagonal = 3,

			/// <summary>Specifies a gradient from upper left to lower right.</summary>
			ForwardDiagonal = 2,

			/// <summary>Specifies a gradient from left to right.</summary>
			Horizontal = 0,

			/// <summary>Specifies a gradient from top to bottom.</summary>
			Vertical = 1
		}

		
		#endregion

        public event Action<PaintEventArgs> CustomPaint;

		#region Variables

		private Container components = null;
		private int V_RoundCorners = 10;
		private string V_GroupTitle = "The Grouper";
		private Color V_BorderColor = Color.Black;
		private float V_BorderThickness = 1;
		private bool V_ShadowControl = false;
		private Color V_BackgroundColor = Color.White;
		private Color V_BackgroundGradientColor = Color.White;
		private GroupBoxGradientMode V_BackgroundGradientMode = GroupBoxGradientMode.None;
		private Color V_ShadowColor = Color.DarkGray;
		private int V_ShadowThickness = 3;
		private Image V_GroupImage = null;
		private Color V_CustomGroupBoxColor = Color.White;
		private bool V_PaintGroupBox = false;
		private Color V_BackColor = Color.Transparent;

		#endregion

		#region Properties

		/// <summary>This feature will paint the background color of the control.</summary>
		[Category("Appearance"), Description("This feature will paint the background color of the control.")]
		public override Color BackColor{get{return V_BackColor;} set{V_BackColor=value; this.Refresh();}}

		/// <summary>This feature will paint the group title background to the specified color if PaintGroupBox is set to true.</summary>
		[Category("Appearance"), Description("This feature will paint the group title background to the specified color if PaintGroupBox is set to true.")]
		public Color CustomGroupBoxColor{get{return V_CustomGroupBoxColor;} set{V_CustomGroupBoxColor=value; this.Refresh();}}

		/// <summary>This feature will paint the group title background to the CustomGroupBoxColor.</summary>
		[Category("Appearance"), Description("This feature will paint the group title background to the CustomGroupBoxColor.")]
		public bool PaintGroupBox{get{return V_PaintGroupBox;} set{V_PaintGroupBox=value; this.Refresh();}}

		/// <summary>This feature can add a 16 x 16 image to the group title bar.</summary>
		[Category("Appearance"), Description("This feature can add a 16 x 16 image to the group title bar.")]
		public Image GroupImage{get{return V_GroupImage;} set{V_GroupImage=value; this.Refresh();}}

		/// <summary>This feature will change the control's shadow color.</summary>
		[Category("Appearance"), Description("This feature will change the control's shadow color.")]
		public Color ShadowColor{get{return V_ShadowColor;} set{V_ShadowColor=value; this.Refresh();}}

		/// <summary>This feature will change the size of the shadow border.</summary>
		[Category("Appearance"), Description("This feature will change the size of the shadow border.")]
		public int ShadowThickness
		{
			get{return V_ShadowThickness;} 
			set
			{
				if(value>10)
				{
					V_ShadowThickness=10;
				}
				else
				{
					if(value<1){V_ShadowThickness=1;}
					else{V_ShadowThickness=value; }
				}

				this.Refresh();
			}
		}

		
		/// <summary>This feature will change the group control color. This color can also be used in combination with BackgroundGradientColor for a gradient paint.</summary>
		[Category("Appearance"), Description("This feature will change the group control color. This color can also be used in combination with BackgroundGradientColor for a gradient paint.")]
		public Color BackgroundColor{get{return V_BackgroundColor;} set{V_BackgroundColor=value; this.Refresh();}}

		/// <summary>This feature can be used in combination with BackgroundColor to create a gradient background.</summary>
		[Category("Appearance"), Description("This feature can be used in combination with BackgroundColor to create a gradient background.")]
		public Color BackgroundGradientColor{get{return V_BackgroundGradientColor;} set{V_BackgroundGradientColor=value; this.Refresh();}}
		
		/// <summary>This feature turns on background gradient painting.</summary>
		[Category("Appearance"), Description("This feature turns on background gradient painting.")]
		public GroupBoxGradientMode BackgroundGradientMode{get{return V_BackgroundGradientMode;} set{V_BackgroundGradientMode=value; this.Refresh();}}

		/// <summary>This feature will round the corners of the control.</summary>
		[Category("Appearance"), Description("This feature will round the corners of the control.")]
		public int RoundCorners
		{
			get{return V_RoundCorners;} 
			set
			{
				if(value>25)
				{
					V_RoundCorners=25;
				}
				else
				{
					if(value<1){V_RoundCorners=1;}
					else{V_RoundCorners=value; }
				}
				
				this.Refresh();
			}
		}

		/// <summary>This feature will add a group title to the control.</summary>
		[Category("Appearance"), Description("This feature will add a group title to the control.")]
		public string GroupTitle{get{return V_GroupTitle;} set{V_GroupTitle=value; this.Refresh();}}

		/// <summary>This feature will allow you to change the color of the control's border.</summary>
		[Category("Appearance"), Description("This feature will allow you to change the color of the control's border.")]
		public Color BorderColor{get{return V_BorderColor;} set{V_BorderColor=value; this.Refresh();}}

		/// <summary>This feature will allow you to set the control's border size.</summary>
		[Category("Appearance"), Description("This feature will allow you to set the control's border size.")]
		public float BorderThickness
		{
			get{return V_BorderThickness;} 
			set
			{
				if(value>3)
				{
					V_BorderThickness=3;
				}
				else
				{
					if(value<1){V_BorderThickness=1;}
					else{V_BorderThickness=value;}
				}
				this.Refresh();
			}
		}

		/// <summary>This feature will allow you to turn on control shadowing.</summary>
		[Category("Appearance"), Description("This feature will allow you to turn on control shadowing.")]
		public bool ShadowControl{get{return V_ShadowControl;} set{V_ShadowControl=value; this.Refresh();}}

		#endregion

		#region Constructor
		
		/// <summary>This method will construct a new GroupBox control.</summary>
		public Grouper()
		{
			InitializeStyles();
			InitializeGroupBox();
		}

	
		#endregion

		#region DeConstructor

		/// <summary>This method will dispose of the GroupBox control.</summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing){if(components!=null){components.Dispose();}}
			base.Dispose(disposing);
		}


		#endregion

		#region Initialization

		/// <summary>This method will initialize the controls custom styles.</summary>
		private void InitializeStyles()
		{
			//Set the control styles----------------------------------
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			//--------------------------------------------------------
		}


		/// <summary>This method will initialize the GroupBox control.</summary>
		private void InitializeGroupBox()
		{
			components = new System.ComponentModel.Container();
			this.Resize+=new EventHandler(GroupBox_Resize);
			this.DockPadding.All = 20;
			this.Name = "GroupBox";
			this.Size = new Size(368, 288);
		}

	
		#endregion

		#region Protected Methods
		
		/// <summary>Overrides the OnPaint method to paint control.</summary>
		/// <param name="e">The paint event arguments.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			PaintBack(e.Graphics);
			PaintGroupText(e.Graphics);
            if (CustomPaint != null)
            {
                CustomPaint(e);
            }
		}

		#endregion

		#region Private Methods

		/// <summary>This method will paint the group title.</summary>
		/// <param name="g">The paint event graphics object.</param>
		private void PaintGroupText(Graphics g)
		{
			//Check if string has something-------------
			if(GroupTitle==string.Empty){return;}
			//------------------------------------------

			//Set Graphics smoothing mode to Anit-Alias-- 
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//-------------------------------------------

			//Declare Variables------------------
			SizeF StringSize = g.MeasureString(this.GroupTitle, this.Font);
			Size StringSize2 = StringSize.ToSize();
			if(this.GroupImage!=null){StringSize2.Width+=18;}
			int ArcWidth = this.RoundCorners;
			int ArcHeight = this.RoundCorners;
			int ArcX1 = 20;
			int ArcX2 = (StringSize2.Width+34) - (ArcWidth + 1);
			int ArcY1 = 0;
			int ArcY2 = 24 - (ArcHeight + 1);
			GraphicsPath path = new GraphicsPath();
			Brush BorderBrush = new SolidBrush(this.BorderColor);
			Pen BorderPen = new Pen(BorderBrush, this.BorderThickness);
			LinearGradientBrush BackgroundGradientBrush = null;
			Brush BackgroundBrush = (this.PaintGroupBox) ? new SolidBrush(this.CustomGroupBoxColor) : new SolidBrush(this.BackgroundColor);
			SolidBrush TextColorBrush = new SolidBrush(this.ForeColor);
			SolidBrush ShadowBrush = null;
			GraphicsPath ShadowPath  = null;
			//-----------------------------------

			//Check if shadow is needed----------
			if(ShadowControl)
			{
				ShadowBrush = new SolidBrush(this.ShadowColor);
				ShadowPath = new GraphicsPath();
				ShadowPath.AddArc(ArcX1+(this.ShadowThickness-1), ArcY1+(this.ShadowThickness-1), ArcWidth, ArcHeight, 180, GroupBoxConstants.SweepAngle); // TopCount Left
				ShadowPath.AddArc(ArcX2+(this.ShadowThickness-1), ArcY1+(this.ShadowThickness-1), ArcWidth, ArcHeight, 270, GroupBoxConstants.SweepAngle); //TopCount Right
				ShadowPath.AddArc(ArcX2+(this.ShadowThickness-1), ArcY2+(this.ShadowThickness-1), ArcWidth, ArcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
				ShadowPath.AddArc(ArcX1+(this.ShadowThickness-1), ArcY2+(this.ShadowThickness-1), ArcWidth, ArcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
				ShadowPath.CloseAllFigures();

				//Paint Rounded Rectangle------------
				g.FillPath(ShadowBrush, ShadowPath);
				//-----------------------------------
			}
			//-----------------------------------

			//Create Rounded Rectangle Path------
			path.AddArc(ArcX1, ArcY1, ArcWidth, ArcHeight, 180, GroupBoxConstants.SweepAngle); // TopCount Left
			path.AddArc(ArcX2, ArcY1, ArcWidth, ArcHeight, 270, GroupBoxConstants.SweepAngle); //TopCount Right
			path.AddArc(ArcX2, ArcY2, ArcWidth, ArcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
			path.AddArc(ArcX1, ArcY2, ArcWidth, ArcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
			path.CloseAllFigures(); 
			//-----------------------------------

			//Check if Gradient Mode is enabled--
			if(this.PaintGroupBox)
			{
				//Paint Rounded Rectangle------------
				g.FillPath(BackgroundBrush, path);
				//-----------------------------------
			}
			else
			{
				if(this.BackgroundGradientMode==GroupBoxGradientMode.None)
				{
					//Paint Rounded Rectangle------------
					g.FillPath(BackgroundBrush, path);
					//-----------------------------------
				}
				else
				{
					BackgroundGradientBrush = new LinearGradientBrush(new Rectangle(0,0,this.Width,this.Height), this.BackgroundColor, this.BackgroundGradientColor, (LinearGradientMode)this.BackgroundGradientMode);
				
					//Paint Rounded Rectangle------------
					g.FillPath(BackgroundGradientBrush, path);
					//-----------------------------------
				}
			}
			//-----------------------------------

			//Paint Borded-----------------------
			g.DrawPath(BorderPen, path);
			//-----------------------------------

			//Paint Text-------------------------
			int CustomStringWidth = (this.GroupImage!=null) ? 44 : 28;
			g.DrawString(this.GroupTitle, this.Font, TextColorBrush, CustomStringWidth, 5);
			//-----------------------------------

			//Draw GroupImage if there is one----
			if(this.GroupImage!=null)
			{
				g.DrawImage(this.GroupImage, 28,4, 16, 16);
			}
			//-----------------------------------

			//Destroy Graphic Objects------------
			if(path!=null){path.Dispose();}
			if(BorderBrush!=null){BorderBrush.Dispose();}
			if(BorderPen!=null){BorderPen.Dispose();}
			if(BackgroundGradientBrush!=null){BackgroundGradientBrush.Dispose();}
			if(BackgroundBrush!=null){BackgroundBrush.Dispose();}
			if(TextColorBrush!=null){TextColorBrush .Dispose();}
			if(ShadowBrush!=null){ShadowBrush.Dispose();}
			if(ShadowPath!=null){ShadowPath.Dispose();}
            
			//-----------------------------------
		}

		
		/// <summary>This method will paint the control.</summary>
		/// <param name="g">The paint event graphics object.</param>
		private void PaintBack(Graphics g)
		{
			//Set Graphics smoothing mode to Anit-Alias-- 
			g.SmoothingMode = SmoothingMode.AntiAlias;
			//-------------------------------------------

			//Declare Variables------------------
			int ArcWidth = this.RoundCorners * 2;
			int ArcHeight = this.RoundCorners * 2;
			int ArcX1 = 0;
			int ArcX2 = (this.ShadowControl) ? (this.Width - (ArcWidth + 1))-this.ShadowThickness : this.Width - (ArcWidth + 1);
			int ArcY1 = 10;
			int ArcY2 = (this.ShadowControl) ? (this.Height - (ArcHeight + 1))-this.ShadowThickness : this.Height - (ArcHeight + 1);
			GraphicsPath path = new GraphicsPath();
			Brush BorderBrush = new SolidBrush(this.BorderColor);
			Pen BorderPen = new Pen(BorderBrush, this.BorderThickness);
			LinearGradientBrush BackgroundGradientBrush = null;
			Brush BackgroundBrush = new SolidBrush(this.BackgroundColor);
			SolidBrush ShadowBrush = null;
			GraphicsPath ShadowPath  = null;
			//-----------------------------------

			//Check if shadow is needed----------
			if(this.ShadowControl)
			{
				ShadowBrush = new SolidBrush(this.ShadowColor);
				ShadowPath = new GraphicsPath();
				ShadowPath.AddArc(ArcX1+this.ShadowThickness, ArcY1+this.ShadowThickness, ArcWidth, ArcHeight, 180, GroupBoxConstants.SweepAngle); // TopCount Left
				ShadowPath.AddArc(ArcX2+this.ShadowThickness, ArcY1+this.ShadowThickness, ArcWidth, ArcHeight, 270, GroupBoxConstants.SweepAngle); //TopCount Right
				ShadowPath.AddArc(ArcX2+this.ShadowThickness, ArcY2+this.ShadowThickness, ArcWidth, ArcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
				ShadowPath.AddArc(ArcX1+this.ShadowThickness, ArcY2+this.ShadowThickness, ArcWidth, ArcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
				ShadowPath.CloseAllFigures();

				//Paint Rounded Rectangle------------
				g.FillPath(ShadowBrush, ShadowPath);
				//-----------------------------------
			}
			//-----------------------------------

			//Create Rounded Rectangle Path------
			path.AddArc(ArcX1, ArcY1, ArcWidth, ArcHeight, 180, GroupBoxConstants.SweepAngle); // TopCount Left
			path.AddArc(ArcX2, ArcY1, ArcWidth, ArcHeight, 270, GroupBoxConstants.SweepAngle); //TopCount Right
			path.AddArc(ArcX2, ArcY2, ArcWidth, ArcHeight, 360, GroupBoxConstants.SweepAngle); //Bottom Right
			path.AddArc(ArcX1, ArcY2, ArcWidth, ArcHeight, 90, GroupBoxConstants.SweepAngle); //Bottom Left
			path.CloseAllFigures(); 
			//-----------------------------------

			//Check if Gradient Mode is enabled--
			if(this.BackgroundGradientMode==GroupBoxGradientMode.None)
			{
				//Paint Rounded Rectangle------------
				g.FillPath(BackgroundBrush, path);
				//-----------------------------------
			}
			else
			{
				BackgroundGradientBrush = new LinearGradientBrush(new Rectangle(0,0,this.Width,this.Height), this.BackgroundColor, this.BackgroundGradientColor, (LinearGradientMode)this.BackgroundGradientMode);
				
				//Paint Rounded Rectangle------------
				g.FillPath(BackgroundGradientBrush, path);
				//-----------------------------------
			}
			//-----------------------------------

			//Paint Borded-----------------------
			g.DrawPath(BorderPen, path);
			//-----------------------------------

			//Destroy Graphic Objects------------
			if(path!=null){path.Dispose();}
			if(BorderBrush!=null){BorderBrush.Dispose();}
			if(BorderPen!=null){BorderPen.Dispose();}
			if(BackgroundGradientBrush!=null){BackgroundGradientBrush.Dispose();}
			if(BackgroundBrush!=null){BackgroundBrush.Dispose();}
			if(ShadowBrush!=null){ShadowBrush.Dispose();}
			if(ShadowPath!=null){ShadowPath.Dispose();}
			//-----------------------------------
		}

	
		/// <summary>This method fires when the GroupBox resize event occurs.</summary>
		/// <param name="sender">The object the sent the event.</param>
		/// <param name="e">The event arguments.</param>
		private void GroupBox_Resize(object sender, EventArgs e)
		{
			this.Refresh();
		}


		#endregion
	}

    public class GroupBoxConstants
    {
        #region Constants

        /// <summary>The sweep angle of the arc.</summary>
        public const int SweepAngle = 90;

        /// <summary>The minimum control height.</summary>
        public const int MinControlHeight = 32;

        /// <summary>The minimum control width.</summary>
        public const int MinControlWidth = 96;

        #endregion
    }
}
