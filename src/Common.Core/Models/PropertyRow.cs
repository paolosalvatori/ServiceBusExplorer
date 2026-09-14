namespace ServiceBusExplorer.Core.Models
{
    /// <summary>A single property row displayed in the entity Overview tab.</summary>
    public sealed class PropertyRow
    {
        public PropertyRow(string key, string value) { Key = key; Value = value; }
        public string Key   { get; }
        public string Value { get; }
    }
}

