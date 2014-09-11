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