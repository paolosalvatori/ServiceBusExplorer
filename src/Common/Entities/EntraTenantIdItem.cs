using System;

namespace ServiceBusExplorer.Common.Entities
{
    public sealed class EntraTenantIdItem
    {
        public Guid Value { get; set; }

        public string DisplayText => Value.ToString();
    }
}

