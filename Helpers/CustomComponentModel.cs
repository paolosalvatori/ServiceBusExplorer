#region Using Directives
using System;
using System.Drawing.Design;
using System.Collections;
using System.ComponentModel; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
  internal class PropertyValueUIService : IPropertyValueUIService
  {
      #region Private Fields
      private PropertyValueUIHandler valueUIHandler;
      private EventHandler notifyHandler; 
      #endregion

      #region IPropertyValueUIService Members
      /// <summary>
      /// Adds or removes an event handler that will be invoked
      /// when the global list of PropertyValueUIItems is modified.
      /// </summary>
      event EventHandler IPropertyValueUIService.PropertyUIValueItemsChanged
      {
          add
          {
              lock (this)
                 notifyHandler += value;
          }
          remove
          {
              lock (this)
                 notifyHandler -= value;
          }
      }

      /// <summary>
      /// Tell the IPropertyValueUIService implementation that the global list of PropertyValueUIItems has been modified.
      /// </summary>
      void IPropertyValueUIService.NotifyPropertyValueUIItemsChanged()
      {
          if (notifyHandler != null)
          {
             notifyHandler(this, EventArgs.Empty);
          }
      }

      /// <summary>
      /// Adds a PropertyValueUIHandler to this service.  When GetPropertyUIValueItems is
      /// called, each handler added to this service will be called and given the opportunity
      /// to add an icon to the specified property.
      /// </summary>
      /// <param name="newHandler"></param>
      void IPropertyValueUIService.AddPropertyValueUIHandler(PropertyValueUIHandler newHandler)
      {
          if (newHandler == null)
          {
              throw new ArgumentNullException("newHandler");
          }
          lock (this)
             valueUIHandler = (PropertyValueUIHandler)Delegate.Combine(valueUIHandler, newHandler);
      }

      /// <summary>
      /// Removes a PropertyValueUIHandler to this service.  When GetPropertyUIValueItems is
      /// called, each handler added to this service will be called and given the opportunity
      /// to add an icon to the specified property.
      /// </summary>
      /// <param name="newHandler"></param>
      void IPropertyValueUIService.RemovePropertyValueUIHandler(PropertyValueUIHandler newHandler)
      {
          if (newHandler == null)
          {
              throw new ArgumentNullException("newHandler");
          }

         valueUIHandler = (PropertyValueUIHandler)Delegate.Remove(valueUIHandler, newHandler);
      }

      /// <summary>
      /// Gets all the PropertyValueUIItems that should be displayed on the given property.
      /// For each item returned, a glyph icon will be aded to the property.
      /// </summary>
      /// <param name="context"></param>
      /// <param name="propDesc"></param>
      /// <returns></returns>
      PropertyValueUIItem[] IPropertyValueUIService.GetPropertyUIValueItems(ITypeDescriptorContext context, PropertyDescriptor propDesc)
      {

          if (propDesc == null)
          {
              throw new ArgumentNullException("propDesc");
          }

          if (valueUIHandler == null)
          {
              return new PropertyValueUIItem[0];
          }


          lock (this)
          {
              var result = new ArrayList();
              valueUIHandler(context, propDesc, result);
              return (PropertyValueUIItem[])result.ToArray(typeof(PropertyValueUIItem));
          }

      }
      #endregion
  }
}
