#region Using Directives
using System;
using System.Collections.Generic; 
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    internal class CategorySorter : IComparer<CustomPropertyDescriptor>
    {
        #region IComparer<PropertyDescriptor> Members

        public int Compare( CustomPropertyDescriptor x, CustomPropertyDescriptor y )
        {
            x.TabAppendCount = 0;
            y.TabAppendCount = 0;
            switch (sortOrder)
            {
                case CustomSortOrder.AscendingById:
                    if (x.CategoryId > y.CategoryId)
                    {
                        return 1;
                    }
                    if (x.CategoryId < y.CategoryId)
                    {
                        return -1;
                    }
                    return 0;
                case CustomSortOrder.AscendingByName:
                    return (String.Compare(x.Category, y.Category, true));
                case CustomSortOrder.DescendingById:
                    if (x.CategoryId > y.CategoryId)
                    {
                        return -1;
                    }
                    if (x.CategoryId < y.CategoryId)
                    {
                        return 1;
                    }
                    return 0;
                case CustomSortOrder.DescendingByName:
                    return (String.Compare(y.Category, x.Category, true));
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