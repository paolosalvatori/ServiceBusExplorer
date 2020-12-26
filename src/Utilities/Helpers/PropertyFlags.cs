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

#endregion

namespace ServiceBusExplorer.Utilities.Helpers
{
    [Flags]
    public enum PropertyFlags
    {
        [StandardValue("None", "None of the flags should be applied to this property.")]
        None = 0,
        [StandardValue("Display name", "Display name should be retrieved from resource if possible for this property.")]
        LocalizeDisplayName = 1,
        [StandardValue("Category name", "Category name should be retrieved from resource if possible for this property.")]
        LocalizeCategoryName = 2,
        [StandardValue("Description", "Description string should be retrieved from resource if possible for this property.")]
        LocalizeDescription = 4,
        [StandardValue("Enumeration", "Enumerations' display strings should be retrieved from resource if possible  for this property if it is an enumeration type.")]
        LocalizeEnumerations = 8,
        [StandardValue("Exclusive", "Values can only be selected from a list and user are not allowed to type in the value for this property.")]
        ExclusiveStandardValues = 16,
        [StandardValue("Use resource for all string", "Use resource for all string for this property.")]
        LocalizeAllString = LocalizeDisplayName | LocalizeDescription | LocalizeCategoryName | LocalizeEnumerations,
        [StandardValue("Expandable", "Make property expandlabe if property type is IEnemerable")]
        ExpandIEnumerable = 32,
        [StandardValue("Supports standard values", "Property supports standard values.")]
        SupportStandardValues = 64,
        [StandardValue("All flags", "All of the flags should be applied to this property.")]
        All = LocalizeAllString | ExclusiveStandardValues | ExpandIEnumerable | SupportStandardValues,
        Default = LocalizeAllString | SupportStandardValues,
    }
}


