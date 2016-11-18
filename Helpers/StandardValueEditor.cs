#region Using Directives
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
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