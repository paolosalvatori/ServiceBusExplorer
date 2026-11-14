# Migrating to Azure.Messaging.ServiceBus (#727)

Issue: https://github.com/paolosalvatori/ServiceBusExplorer/issues/727

The migration is staged so every stage builds, passes tests and leaves a
runnable application. Stages are stacked in order; see
`sdk-migration-decisions.md` for the upstream PR targeting constraints.

| Stage | Scope |
|-------|-------|
| 1. Connection layer | Recognise emulator connection strings; document compatibility. |
| 2. Management plane | `NamespaceManager` and `*Description` move to `ServiceBusAdministrationClient` and `*Properties`. |
| 3. Data plane | `BrokeredMessage`, senders, receivers and sessions move to `ServiceBusSender`, `ServiceBusReceiver`, `ServiceBusSessionReceiver`, `ServiceBusProcessor`. |
| 4. Cleanup | Remove `WindowsAzure.ServiceBus` and `Microsoft.ServiceBus`. |

## Stage 1: connection layer

- `UseDevelopmentEmulator=true` is parsed into `ServiceBusNamespace.UseDevelopmentEmulator`.
- Emulator endpoints (`sb://localhost[:port]`, `sb://127.0.0.1`) are accepted as
  host names; no fully qualified Azure domain is required.
- Connecting to an emulator namespace fails fast with an explicit
  `NotSupportedException` **in the stage-1 commit**, because the legacy SDK
  cannot reach the emulator. Stage 2 below removes that connection blocker for
  management; stage 3 is still required for the legacy data-plane workflows.

## Stage 2: management plane

Queue, topic, subscription and rule management now uses
`ServiceBusAdministrationClient`: enumeration (including every page), reads,
creation, updates, deletion, status changes, forwarding, SAS authorization and
subscription rule creation/removal. Rules no longer open a legacy
`SubscriptionClient` or require a messaging factory. SAS and existing Entra
credentials remain supported; the existing Event Hubs audience fallback and
its separate management path are preserved.

`QueueInfo`, `TopicInfo`, `SubscriptionInfo` and `RuleInfo` are the UI/Common
management snapshots. They retain the native `*Properties`; entity snapshots
also retain the separate native runtime properties, rather than trying to set
read-only legacy counters. Queue/subscription totals, dead-letter/transfer
counts, sizes and timestamps come from the runtime API. Topic message counts
combine subscription runtime counters with the topic's scheduled count.
Reading a topic therefore also enumerates its subscription runtime properties.

For this staged change, snapshots expose an XML-compatible legacy edit buffer
through an explicit, type-safe mapper. Existing control/property names and XML
element names (`Queue`, `Path`, `TopicPath`, `AuthorizationRules`, `SqlFilter`,
etc.) remain unchanged. This compatibility seam is not a claim that the legacy
package can be removed yet. Unsupported settings fail explicitly before a
mutation; immutable settings cannot be changed through the edit buffer. Updates
start from current native properties and preserve native-only fields, including
`MaxMessageSizeInKilobytes`, which is also retained in export/import. Exports
no longer clamp Premium entity capacities to 1024 MB. Relative and absolute
forwarding addresses are both accepted by the export path.

### Emulator administration

Emulator connections now initialize management without constructing a legacy
messaging factory. The administration connection strips legacy transport and
entity-scope settings while retaining `UseDevelopmentEmulator=true`. For
`Endpoint=sb://localhost` or the standard data-plane port `:5672`, administration
uses HTTP port **5300**. A different explicit endpoint port is retained, allowing
an independently mapped administration port. The original saved connection
string is unchanged. A saved profile with a custom administration port is a
management profile; stage 3 must distinguish that port from the AMQP endpoint
when completing data-plane connectivity.

`Azure.Messaging.ServiceBus` is upgraded from 7.19.0 to **7.20.1** because 7.19.0
discards the administration endpoint port and sends HTTP requests to port 80.
An SDK pipeline regression test captures the real outgoing URI without network
access and verifies `http://localhost:5300`. See the upstream
[7.20.1 client constructor](https://github.com/Azure/azure-sdk-for-net/blob/Azure.Messaging.ServiceBus_7.20.1/sdk/servicebus/Azure.Messaging.ServiceBus/src/Administration/ServiceBusAdministrationClient.cs)
and [emulator setup documentation](https://learn.microsoft.com/azure/service-bus-messaging/test-locally-with-service-bus-emulator).

The emulator does not expose Event Hubs, Notification Hubs or Relay nodes.
Management profiles may retain an administration endpoint on a custom port;
the data-plane connection builder independently selects AMQP port **5672** and
removes entity-scoped/legacy transport settings. The saved connection string is
not rewritten. This stage does **not** claim end-to-end emulator support or
completion of #727.
No live emulator or Azure namespace was available during validation; mocked
administration workflows and SDK request construction are covered, not live
service compatibility or emulator feature availability.

## Stage 3: data-plane migration (incremental)

The editable message dialog now sends resubmitted messages through
`ServiceBusSender`. A boundary adapter copies the body, sendable system
properties, and application properties from the existing editable
`BrokeredMessage` buffer into the immutable-SDK `ServiceBusMessage`. This keeps
message editing, inspectors, generators, and existing import/export formats
working while those extension and UI contracts are migrated separately.
`ForcePersistence` has no new-SDK equivalent and is not mapped; Service Bus
messages are durable by service semantics. `ViaPartitionKey` also has no
`ServiceBusMessage` equivalent and is not preserved by this send adapter.

This is an incremental stage, not completion of the data-plane migration.
Queue/topic test senders, ordinary and dead-letter receivers, session
operations, dead-letter settlement/moves, and listener pumps still use legacy
clients and remain unavailable for emulator data-plane profiles. In
particular, dead-letter move combines sending with settlement on the original
receiver; that path remains legacy rather than risk sending a duplicate or
losing the original message. The next increment must move those operations to
the SDK receiver/processor APIs and replace plugin/UI message contracts only
with an explicit compatibility plan. Event Hubs and Notification Hubs are
unchanged.

### Compatibility decisions for review

- **Rename:** the new SDK has no rename operation. Rename reports an explicit
  unsupported error and leaves the source untouched. It is not approximated
  with create/delete, which could lose messages, subscriptions or permissions.
  Review whether a separately approved transfer workflow is desirable.
- **Filters:** administration no longer accepts server-side OData filters.
  The existing UI grammar is evaluated locally: `Startswith(Path/Name, 'x')
  Eq true`, path/name comparisons, `MessageCount` comparisons and
  `CreatedAt`/`UpdatedAt`/`AccessedAt` comparisons, joined by `And`/`Or`
  (And has precedence). Quoted values and doubled single quotes are preserved.
  Arbitrary OData functions, parentheses and unsupported fields fail explicitly.
  Queue/topic enumeration timeout covers all pagination, runtime reads and
  filtering, with cancellation propagated to the SDK. Subscription enumeration
  retains the existing SDK timeout behavior.
- **Legacy flags:** Express, anonymous access, topic pre-publish filtering,
  client-affine subscriptions and ACS `AllowRule` have no implemented equivalent
  and are rejected if requested. Legacy queue `SupportOrdering` defaults to
  true but has no configurable queue property in the new SDK: it is accepted
  as a compatibility field, not sent as an SDK setting. Queue ordering follows
  Service Bus semantics; this field does not promise global FIFO or session
  guarantees. Topic `SupportOrdering` is a real native property and is mapped.
  Review removing the unsupported controls/compatibility fields in cleanup.
- **Rule timestamps:** the administration SDK does not expose rule creation
  timestamps. The control displays "Not available from the administration SDK"
  instead of a fabricated timestamp.
- **On-premises Service Bus Server:** modern administration cannot implement
  Windows/STS authentication. Connection now reports an explicit unsupported
  error; use the previous release for that environment. Restoring support
  requires a separately maintained legacy administration path.
- **Editable message compatibility:** the message editor and existing
  `IBrokeredMessageInspector`/`IBrokeredMessageGenerator` extension contracts
  continue using `BrokeredMessage` as an editable compatibility buffer. The
  resubmit operation converts to the modern SDK type at send time. Review
  whether a future public editable-message contract should replace these
  legacy extension types before stage 4 removes their package.

### Validation

With `FILE_VERSION=1.0.0.1`, the full Release solution build succeeds and the
Release unit suite passes (167 tests). The build retains the pre-existing
missing `MinimumRecommendedRules.ruleset` warning. Regression coverage includes
native properties/SAS/rule mapping, paged runtime queries, queue CRUD, rule
CRUD, default entity options, immutable-property guards, update responses,
legacy and current queue XML, filters, emulator management-only startup and
the outgoing HTTP administration URI. Stage-3 tests verify the editor message
adapter and the separate emulator administration/data-plane ports.

## Decisions to review

- On-premises Service Bus Server (Windows auth, STS/runtime/management ports)
  remains parseable, but stage 2 reports its unsupported administration
  connection explicitly as described above.
- `ServiceBusEnvironment.SystemConnectivity` (connectivity mode) disappears;
  AMQP over TCP or WebSockets remains the transport choice.
- Live emulator acceptance still needs to verify administration capabilities
  and the separate AMQP endpoint during stage 3.
