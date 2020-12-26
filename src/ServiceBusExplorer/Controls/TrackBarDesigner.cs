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

using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

namespace ServiceBusExplorer.Controls
{
	/// <summary>
	/// The Designer for the <see cref="CustomTrackBar"/>.
	/// </summary>
	public class CustomTrackBarDesigner : ControlDesigner
	{
	    /// <summary>
		/// Returns the allowable design time selection rules.
		/// </summary>
		public override SelectionRules SelectionRules 
		{ 
			
			get 
			{ 
				var control = this.Control as CustomTrackBar;

				// Disallow vertical or horizontal sizing when AutoSize = True
				if(control != null && control.AutoSize)
					if(control.Orientation == Orientation.Horizontal)
						return (base.SelectionRules & ~SelectionRules.TopSizeable) & ~SelectionRules.BottomSizeable;
					else //control.Orientation == Orientation.Vertical
						return (base.SelectionRules & ~SelectionRules.LeftSizeable) & ~SelectionRules.RightSizeable;
			    return base.SelectionRules;
			}
		}



		//Overrides
		/// <summary>
		/// Remove Button and Control properties that are 
		/// not supported by the <see cref="CustomTrackBar"/>.
		/// </summary>
		protected override void PostFilterProperties(IDictionary Properties)
		{
			Properties.Remove("AllowDrop");
			Properties.Remove("BackgroundImage");
			Properties.Remove("ContextMenu");

			Properties.Remove("Text");
			Properties.Remove("TextAlign");
			Properties.Remove("RightToLeft");
		}


		//Overrides
		/// <summary>
		/// Remove Button and Control events that are 
		/// not supported by the <see cref="CustomTrackBar"/>.
		/// </summary>
		protected override void PostFilterEvents(IDictionary events)
		{
			//Actions
			events.Remove("Click");
			events.Remove("DoubleClick");

			//Appearence
			events.Remove("Paint");

			//Behavior
			events.Remove("ChangeUICues");
            events.Remove("ImeModeChanged");
			events.Remove("QueryAccessibilityHelp");
			events.Remove("StyleChanged");
			events.Remove("SystemColorsChanged");

			//Drag Drop
			events.Remove("DragDrop");
			events.Remove("DragEnter");
			events.Remove("DragLeave");
			events.Remove("DragOver");
			events.Remove("GiveFeedback");
			events.Remove("QueryContinueDrag");
			events.Remove("DragDrop");

			//Layout
			events.Remove("Layout");
			events.Remove("Move");
			events.Remove("Resize");

			//Property Changed
			events.Remove("BackColorChanged");
			events.Remove("BackgroundImageChanged");
			events.Remove("BindingContextChanged");
			events.Remove("CausesValidationChanged");
			events.Remove("CursorChanged");
			events.Remove("FontChanged");
			events.Remove("ForeColorChanged");
			events.Remove("RightToLeftChanged");
			events.Remove("SizeChanged");
			events.Remove("TextChanged");
			
			base.PostFilterEvents (events);
		}

	}
}