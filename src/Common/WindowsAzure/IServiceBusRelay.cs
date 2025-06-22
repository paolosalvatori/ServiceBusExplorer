// // Auto-added comment

// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Microsoft.ServiceBus.Messaging;

// namespace ServiceBusExplorer.WindowsAzure
// {
//     internal interface IServiceBusRelay : IServiceBusEntity
//     {
//         RelayDescription CreateRelay(RelayDescription description);

//         Task DeleteRelay(string path);

//         Task DeleteRelays(IEnumerable<string> relayServices);

//         RelayDescription GetRelay(string path);

//         IEnumerable<RelayDescription> GetRelays(int timeoutInSeconds);

//         Uri GetRelayUri(RelayDescription description);

//         RelayDescription UpdateRelay(RelayDescription description);
//     }
// }
