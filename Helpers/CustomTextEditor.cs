#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Drawing;
using System.Linq;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
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
