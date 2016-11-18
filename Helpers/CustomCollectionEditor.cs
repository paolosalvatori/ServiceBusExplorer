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
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public class CustomCollectionEditor : UITypeEditor
    {
        #region Public Methods
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var service = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (service != null && value != null)
            {
                
                // ReSharper disable SuspiciousTypeConversion.Global
                var gridItem = provider as GridItem;
                // ReSharper restore SuspiciousTypeConversion.Global
                var propertyName = value.GetType().Name;
                if (gridItem != null)
                {
                    propertyName = gridItem.Label;
                }
                var label = propertyName;
                var oldJson = JsonSerializerHelper.Serialize(value);
                using (var form = new CollectionEditorForm(string.Format("Edit {0}", propertyName), label, value))
                {
                    if (service.ShowDialog(form) == DialogResult.OK)
                    {
                        if (string.Compare(oldJson,
                                           JsonSerializerHelper.Serialize(value),
                                           StringComparison.InvariantCulture) != 0)
                        {
                            value = GenericCopier<object>.DeepCopy(form.Value);
                        }
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
