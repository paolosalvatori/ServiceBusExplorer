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
