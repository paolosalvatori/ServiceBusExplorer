using System;

namespace ServiceBusExplorer.Common.Entities
{
    public sealed class EntraTenantIdItem : IEquatable<EntraTenantIdItem>
    {
        public string Value { get; set; }

        public string DisplayText => Value;

        public bool Equals(EntraTenantIdItem other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as EntraTenantIdItem);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Value ?? string.Empty);
        }
    }
}

