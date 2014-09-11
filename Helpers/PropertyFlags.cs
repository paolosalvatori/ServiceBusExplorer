#region Using Directives
using System;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
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


