# Event Hubs management boundary

This stage isolates WindowsAzure.ServiceBus management initialization; it does not
replace Event Hubs management or remove that package. Service Bus queue, topic,
subscription and rule adapters still use Azure.Messaging.ServiceBus. Connecting
only those features does not construct a legacy NamespaceManager or messaging
transport. The existing public NamespaceManager property initializes the legacy
manager on explicit use. The Event Hubs tree and editors now use feature adapters
for discovery, status changes, and reloads after an unsuccessful edit.

## Existing feature inventory

| Entry point | Operations and backend |
| --- | --- |
| MainForm.ShowEntities, HandleEventHubControl refresh | The hub adapter lists namespace profiles or reads one named hub for entity-scoped profiles. A failed scoped read never triggers a namespace list. |
| MainForm update and ServiceBusHelper Event Hub methods | NamespaceManager create, get, update, delete and bulk delete, with existing EventHubDescription mappings |
| HandleConsumerGroupControl and ServiceBusConsumerGroup | NamespaceManager get/list/create/update/delete groups, preserving hub path, group name, metadata and existing retry/error handling |
| ServiceBusPartition | Selecting or refreshing a partition reads its full metadata once, including when its last offset is empty. The consumer-group grid reads current hub IDs and full details for each partition. |
| Send and listener controls | The modern Event Hubs clients take the selected SAS connection string or tenant credential directly, without constructing the Service Bus queue/topic helper. |
| CLI startup and WinForms Connect | Shared ServiceBusHelper.Connect preparation/publication, then existing UI entity selection/loading; unchanged |

Relay management still uses the legacy manager on demand. Notification Hubs uses
its separate SDK manager; its existing URI wrapper now initializes lazily too.
No WCF Relay transport, public message inspector/generator interface, legacy
message serialization, or cold-connect/tree behavior changes are included.

## Authentication and lifecycle

SAS manager construction retains the same normalized namespace connection string,
key name/key and removal of EntityPath used before this stage. That does not
broaden an entity-scoped key's permissions. Modern scoped queue/topic management
and data credentials are unchanged.

Entra connection preparation still probes the Service Bus audience and falls back
only on the existing audience-mismatch classification. Event Hubs interactive
sign-in completes before publication; its provider retains the Event Hubs
audience. Dismissal/failure preserves the previous connection. Deferral introduces
no ARM credential, subscription ID, resource group or new sign-in flow.

Each successful connection captures its own endpoint, credentials and retry count.
Copies share the same lazy manager without forcing creation; a reconnect receives
a new lazy instance. Legacy wrappers use existing scheme, events and logging.
Retry backoff remains the legacy SDK default with the configured retry count.
Emulators expose no legacy manager and reject explicit legacy entity requests.

## Replacement decision matrix

| Option | Parity and decision |
| --- | --- |
| Azure.Messaging.EventHubs runtime properties | Supports SAS and Entra; hub properties, partition IDs and partition runtime metadata are available. Does not implement namespace-wide hub or consumer-group CRUD. Explorer also displays partition size and incoming/outgoing bytes per second. The modern partition model lacks all three, so the detail view and grid retain their full management descriptions. |
| Azure.ResourceManager.EventHubs | Management CRUD requires Azure resource identity and Entra/ARM authorization. Namespace SAS credentials alone cannot be substituted. Offer only as an explicitly chosen management mode, never a silent replacement. |
| Namespace REST / Atom adapter | Microsoft documents hub and group operations with SAS/Bearer headers. The classic documentation also carries a retirement warning. Confirm applicability/support for current namespaces before adopting it; test entity scopes, escaping, pagination, metadata/property ordering, updates, token audience, cancellation, error classification and retries. Consumer-group update semantics need explicit verification. |
| Retain isolated legacy management | Selected for this stage. Preserves existing working SAS and Entra behavior while keeping Service Bus-only connections independent of manager construction. Package removal remains incomplete. |

Primary references (reviewed 2026-11-28):

- [Modern runtime client API](https://learn.microsoft.com/dotnet/api/azure.messaging.eventhubs.consumer.eventhubconsumerclient)
- [Modern partition runtime properties](https://learn.microsoft.com/en-us/dotnet/api/azure.messaging.eventhubs.partitionproperties)
- [Fields in the partition management response](https://learn.microsoft.com/en-us/rest/api/eventhub/list-partition-runtime-metadata)
- [Current ARM consumer-group CRUD API](https://learn.microsoft.com/en-us/rest/api/eventhub/consumer-groups/create-or-update?view=rest-eventhub-2026-01-01)
- [Namespace REST common headers and lifecycle warning](https://learn.microsoft.com/en-us/rest/api/eventhub/event-hubs-management-rest)
- [Namespace REST create hub](https://learn.microsoft.com/en-us/rest/api/eventhub/create-event-hub)
- [Namespace REST create group](https://learn.microsoft.com/en-us/rest/api/eventhub/create-consumer-group)

## Validation handoff

`LegacyManagementIsolationTests` exercises the application's shared Connect entry
point with synthetic SAS and Entra profiles. Only the Entra network probe and the
SDK management network boundary are substituted. Cases cover queue/topic reads,
URI selection, deferred hub/group/partition access, copied helpers, reconnect and
emulator exclusion. Existing connection-publication and Event Hubs audience tests
also cover preservation of the lazy state and successful sign-in reuse.

Run on Windows with .NET Framework 4.7.2:

```powershell
dotnet build src/ServiceBusExplorer.Tests/ServiceBusExplorer.Tests.csproj -c Release
dotnet test src/ServiceBusExplorer.Tests/ServiceBusExplorer.Tests.csproj -c Release --no-build --filter "FullyQualifiedName~EventHubDiscoveryTests|FullyQualifiedName~EventHubPartitionNodeTests|FullyQualifiedName~LegacyManagementIsolationTests|FullyQualifiedName~ConnectionPreparationTests|FullyQualifiedName~ServiceBusHelperEntraTests"
```

These are synthetic checks, not live SAS/Entra acceptance. Before merging, run the
native Windows suite and exercise both authentication modes against authorized
Service Bus and Event Hubs namespaces, including hub/group CRUD, partition refresh,
entity-scoped access, audience rejection/cancellation, reconnect and Relay/NH smoke
checks. Existing reserved live testing is not performed by this change.
