#region Using Directives
using System;
using System.Collections.Generic; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    internal class PropertySorter : IComparer<CustomPropertyDescriptor>
    {
        #region IComparer<PropertyDescriptor> Members

        public int Compare( CustomPropertyDescriptor x, CustomPropertyDescriptor y )
        {

            switch (sortOrder)
            {
                case CustomSortOrder.AscendingById:
                    if (x.PropertyId > y.PropertyId)
                    {
                        return 1;
                    }
                    else if (x.PropertyId < y.PropertyId)
                    {
                        return -1;
                    }
                    return 0;
                case CustomSortOrder.AscendingByName:
                    return (String.Compare(x.DisplayName, y.DisplayName, true));
                case CustomSortOrder.DescendingById:
                    if (x.PropertyId > y.PropertyId)
                    {
                        return -1;
                    }
                    else if (x.PropertyId < y.PropertyId)
                    {
                        return 1;
                    }
                    return 0;
                case CustomSortOrder.DescendingByName:
                    return (String.Compare(y.DisplayName, x.DisplayName, true));
            }
            return 0;
        }

        #endregion

        #region Private Fields
        private CustomSortOrder sortOrder = CustomSortOrder.AscendingByName; 
        #endregion

        #region Public Properties
        public CustomSortOrder SortOrder
        {
            get
            {
                return sortOrder;
            }
            set
            {
                sortOrder = value;
            }
        } 
        #endregion
    }
}