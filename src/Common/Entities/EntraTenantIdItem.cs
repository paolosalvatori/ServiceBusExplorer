using System;

namespace ServiceBusExplorer.Common.Entities
{
    public sealed class EntraTenantIdItem
    {
        public string Value { get; set; }

        public string DisplayText => Value;
    }
}

