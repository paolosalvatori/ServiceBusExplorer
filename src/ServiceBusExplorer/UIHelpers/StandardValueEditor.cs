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
using System.Windows.Forms.Design;
using ServiceBusExplorer.Controls;

#endregion

namespace ServiceBusExplorer.UIHelpers
{
    public class StandardValueEditor : UITypeEditor
    {
        #region Private Fields
        private StandardValueEditorUI standardValueEditorUI = new StandardValueEditorUI(); 
        #endregion

        #region Public Properties 
        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Public Methods
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (editorService == null)
            {
                return value;
            }
            standardValueEditorUI.SetData(context, editorService, value);
            editorService.DropDownControl(standardValueEditorUI);
            value = standardValueEditorUI.GetValue();
            return value;
        } 
        #endregion
    }
}
