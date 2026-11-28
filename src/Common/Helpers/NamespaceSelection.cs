using System;
using System.Collections.Generic;
using System.Linq;
using ServiceBusExplorer.Common.Helpers;

namespace ServiceBusExplorer.Helpers
{
    public enum NamespaceService { ServiceBus, EventHubs, NotificationHubs, Relay, Custom }

    public sealed class NamespaceSelection
    {
        public NamespaceService Service { get; }
        public IReadOnlyList<string> Entities { get; }

        public NamespaceSelection(NamespaceService service, IEnumerable<string> customEntities = null)
        {
            Service = service;
            string[] groups;
            switch (service)
            {
                case NamespaceService.ServiceBus:
                    groups = new[] { Constants.QueueEntities, Constants.TopicEntities };
                    break;
                case NamespaceService.EventHubs:
                    groups = new[] { Constants.EventHubEntities };
                    break;
                case NamespaceService.NotificationHubs:
                    groups = new[] { Constants.NotificationHubEntities };
                    break;
                case NamespaceService.Relay:
                    groups = new[] { Constants.RelayEntities };
                    break;
                case NamespaceService.Custom:
                    groups = ConfigurationHelper.Entities.Intersect(customEntities ?? throw new ArgumentNullException(nameof(customEntities))).ToArray();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(service));
            }
            Entities = Array.AsReadOnly(groups);
        }

        internal string ToSetting() => Service == NamespaceService.Custom
            ? Service + "|" + string.Join(",", Entities)
            : Service.ToString();

        internal static NamespaceSelection FromSetting(string value)
        {
            if (value == null) return null;
            var parts = value.Split('|');
            if (!Enum.TryParse(parts[0], out NamespaceService service) || service.ToString() != parts[0] ||
                !Enum.IsDefined(typeof(NamespaceService), service)) return null;
            if (service != NamespaceService.Custom)
                return parts.Length == 1 ? new NamespaceSelection(service) : null;
            return parts.Length == 2 ? new NamespaceSelection(service, parts[1].Split(',')) : null;
        }
    }
}
