using System;
using Azure.Messaging.EventHubs;

namespace ServiceBusExplorer.Helpers
{
    public static class EventHubConnectionValidation
    {
        public static void ValidateIoTHubEndpoint(string connectionString, string eventHubName)
        {
            const string guidance = "Use the Event Hubs-compatible connection string and Event Hub-compatible name from IoT Hub > Built-in endpoints. IoT Hub HostName connection strings and messages/events are not supported here.";
            if (string.IsNullOrWhiteSpace(eventHubName) ||
                string.Equals(eventHubName.Trim(), "messages/events", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(guidance, nameof(eventHubName));
            }

            EventHubsConnectionStringProperties properties;
            try
            {
                properties = EventHubsConnectionStringProperties.Parse(connectionString);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
            {
                // Do not include the supplied string or parser exception: it contains credentials.
                throw new ArgumentException(guidance, nameof(connectionString));
            }
            if (properties.Endpoint == null ||
                !string.Equals(properties.Endpoint.Scheme, "sb", StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(properties.EventHubName) &&
                 !string.Equals(properties.EventHubName, eventHubName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException(guidance, nameof(connectionString));
            }
        }
    }
}
