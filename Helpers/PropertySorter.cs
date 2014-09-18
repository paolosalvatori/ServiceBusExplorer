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