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
using System.ComponentModel;
using System.Drawing.Design; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class PropertyValuePaintEditor : UITypeEditor
    {
        #region Public Properties
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            // let the property browser know we'd like
            // to do custom painting.
            if (context != null)
            {
                if (context.PropertyDescriptor != null)
                {
                    if (context.PropertyDescriptor is CustomPropertyDescriptor)
                    {
                        var cpd = context.PropertyDescriptor as CustomPropertyDescriptor;
                        return (cpd.ValueImage != null);
                    }
                }
            }
            return base.GetPaintValueSupported(context);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }

        public override void PaintValue(PaintValueEventArgs pe)
        {
            if (pe.Context != null)
            {
                if (pe.Context.PropertyDescriptor != null)
                {
                    if (pe.Context.PropertyDescriptor is CustomPropertyDescriptor)
                    {
                        var cpd = pe.Context.PropertyDescriptor as CustomPropertyDescriptor;

                        if (cpd.ValueImage != null)
                        {
                            pe.Graphics.DrawImage(cpd.ValueImage, pe.Bounds);
                            return;
                        }
                    }
                }
            }
            base.PaintValue(pe);
        } 
        #endregion
    }
}