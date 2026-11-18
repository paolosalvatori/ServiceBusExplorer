using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServiceBusExplorer.Helpers
{
    public static class ServiceBusManagementFilter
    {
        private const RegexOptions Options = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
        private const string QuotedString = "'(?<value>(?:[^']|'')*)'";
        private const string Operator = @"(?<op>eq|ne|gt|ge|lt|le)";
        public delegate bool Predicate(string path, long messageCount, DateTime createdAt, DateTime updatedAt, DateTime accessedAt);

        public static Predicate Parse(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return (path, count, created, updated, accessed) => true;
            }

            var alternatives = Split(filter, "or").Select(part => Split(part, "and").Select(ParseClause).ToArray()).ToArray();
            return (path, count, created, updated, accessed) => alternatives.Any(clauses =>
                clauses.All(clause => clause(path, count, created, updated, accessed)));
        }

        private static string[] Split(string filter, string logicalOperator) =>
            Regex.Split(filter, @"\s+" + logicalOperator + @"\s+(?=(?:[^']*'[^']*')*[^']*$)", Options);

        private static Predicate ParseClause(string clause)
        {
            var prefix = Regex.Match(clause, @"^\s*startswith\(\s*(path|name)\s*,\s*" + QuotedString + @"\s*\)\s+eq\s+true\s*$", Options);
            if (prefix.Success)
            {
                var value = prefix.Groups["value"].Value.Replace("''", "'");
                return (path, count, created, updated, accessed) => path.StartsWith(value, StringComparison.OrdinalIgnoreCase);
            }

            var pathComparison = Regex.Match(clause, @"^\s*(path|name)\s+" + Operator + @"\s+" + QuotedString + @"\s*$", Options);
            if (pathComparison.Success)
            {
                var value = pathComparison.Groups["value"].Value.Replace("''", "'");
                var op = pathComparison.Groups["op"].Value.ToLowerInvariant();
                return (path, count, created, updated, accessed) => Compare(string.Compare(path, value, StringComparison.OrdinalIgnoreCase), op);
            }

            var countComparison = Regex.Match(clause, @"^\s*messagecount\s+" + Operator + @"\s+(?<value>\d+)\s*$", Options);
            if (countComparison.Success)
            {
                var value = long.Parse(countComparison.Groups["value"].Value, CultureInfo.InvariantCulture);
                var op = countComparison.Groups["op"].Value.ToLowerInvariant();
                return (path, count, created, updated, accessed) => Compare(count.CompareTo(value), op);
            }

            var timeComparison = Regex.Match(clause, @"^\s*(?<field>createdat|updatedat|accessedat)\s+" + Operator + @"\s+(datetime)?" + QuotedString + @"\s*$", Options);
            if (timeComparison.Success)
            {
                var value = DateTimeOffset.Parse(timeComparison.Groups["value"].Value, CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal).UtcDateTime;
                var field = timeComparison.Groups["field"].Value.ToLowerInvariant();
                var op = timeComparison.Groups["op"].Value.ToLowerInvariant();
                return (path, count, created, updated, accessed) => Compare(
                    (field == "createdat" ? created : field == "updatedat" ? updated : accessed).CompareTo(value), op);
            }

            throw new NotSupportedException("Unsupported Service Bus management filter clause: " + clause +
                ". Supported filters are Startswith(Path/Name, 'prefix') Eq true, Path/Name comparisons, MessageCount comparisons, and CreatedAt/UpdatedAt/AccessedAt comparisons, joined by And/Or.");
        }

        private static bool Compare(int comparison, string op)
        {
            switch (op)
            {
                case "eq": return comparison == 0;
                case "ne": return comparison != 0;
                case "gt": return comparison > 0;
                case "ge": return comparison >= 0;
                case "lt": return comparison < 0;
                case "le": return comparison <= 0;
                default: throw new ArgumentOutOfRangeException(nameof(op));
            }
        }
    }
}
