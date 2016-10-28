namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public enum CustomSortOrder
    {
        // no custom sorting
        None,
        // sort asscending using the property name or category name
        AscendingByName,
        // sort asscending using property id or categor id
        AscendingById,
        // sort descending using the property name or category name
        DescendingByName,
        // sort descending using property id or categor id
        DescendingById,
    }
}