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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ServiceBusExplorer.Forms;
using ServiceBusExplorer.Helpers;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    public class CustomTextEditor : UITypeEditor
    {
        #region Public Methods
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (service != null && context.Instance is CustomObject)
            {
                var customObject = context.Instance as CustomObject;
                
                var valueAsString = value as string;
                // ReSharper disable SuspiciousTypeConversion.Global
                var gridItem = provider as GridItem;
                // ReSharper restore SuspiciousTypeConversion.Global
                

                var propertyName = "Value";
                if (gridItem != null)
                {
                    propertyName = gridItem.Label;
                    if (customObject.Properties.All(p => p.Name != gridItem.Label && p.Type != typeof(string)))
                    {
                        return string.Empty;
                    }
                }
                using (var form = new TextForm(string.Format("Edit {0}", propertyName), propertyName, valueAsString, true))
                {
                    form.Size = new Size(600, 320);
                    if (service.ShowDialog(form) == DialogResult.OK)
                    {
                        value = form.Content;
                    }
                }
            }
            // ReSharper disable AssignNullToNotNullAttribute
            return value;
            // ReSharper restore AssignNullToNotNullAttribute
        } 
        #endregion
    }
}
