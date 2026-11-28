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
The default emulator uses administration port 5300 and AMQP TCP port 5672.
For a remapped instance, set `RuntimePort` to its published AMQP port, for
example `Endpoint=sb://localhost:15300/;RuntimePort=15672` in the raw profile.
With a separate `ManagementPort`, an explicit, distinct `Endpoint` port also
identifies the data listener. A profile pointing at the HTTP listener, including
port 5300, allows browsing and requires an explicit AMQP destination before
sending, purging or cancelling. A bare emulator host uses the standard data port.
Azure SAS data connections retain `EntityPath` as their signing scope. Explicit
Azure transport ports are applied to the physical connection.
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
messages are durable by service semantics. `ViaPartitionKey` maps to the modern
SDK's `TransactionPartitionKey`.

This increment moves queue/topic test senders, non-session message-browser
peek and receive operations, and non-session queue/subscription test receivers
to `ServiceBusSender` and `ServiceBusReceiver`. Message browsing still exposes
the established `BrokeredMessage` contract to inspectors and UI code through a
compatibility adapter. Application properties retain their original names and values.
The adapter maps representable broker headers, including state, to the corresponding
`BrokeredMessage` properties. Inspectors can read the original SDK message and its
receive-only fields with `ServiceBusReceivedMessageAdapter.GetReceivedMetadata(row)`.
That accessor returns null for a legacy-only row.

Dead-letter deletion uses SDK receiver settlement. Moving selected DLQ messages
uses a single SDK client with cross-entity transactions enabled and an ambient
transaction that sends the destination message and completes the source lock
atomically. An operation error does not trigger a second send; the source lock
is abandoned best-effort and failures are surfaced. Live Azure/emulator
acceptance is still needed to verify cross-entity transaction behavior for
each supported service profile.

Session-specific browsing and receiving now use `ServiceBusSessionReceiver`,
including test receivers and dead-letter receivers; selecting a session reads
its state through the modern receiver without changing it. Continuous
listeners use `ServiceBusProcessor` or the session-aware processor, with
asynchronous stop/cancellation and SDK settlement. Processor callbacks use
their cancellation tokens for explicit completion; expected cancellation on
stop is not logged as a processing failure. Manual session-receive loops stop
on terminal errors rather than retrying indefinitely. Received messages
continue through the metadata-preserving adapter for the existing inspector
and UI contracts.

The session list itself still uses the legacy read-only
`GetMessageSessions()` enumeration. Azure.Messaging.ServiceBus 7.20.1 has no
session-ID enumeration API; discovering IDs by repeatedly accepting sessions
would acquire locks and disturb other consumers, so this stage does not
approximate that operation. The list retains its existing `MessageSession`
property-grid model, while selected session state is read from the modern
receiver. This is a documented compatibility gap for a future decision.
Legacy callback adapter classes and the old message editor, inspectors, and
generators also remain as compatibility seams. Cleanup and package removal
remain out of scope. Event Hubs and Notification Hubs are unchanged.

The editor requires a fresh ID for a destructive replay when destination duplicate
 detection is enabled or cannot be verified. It validates the complete selection
 before taking locks. Modern DLQ moves keep a single transaction-enabled client for
 send and completion; a failed delivery is never counted as a settled source.

### Compatibility decisions for review

- **Rename:** the new SDK has no rename operation. Rename is disabled in the context and Actions menus on this path; direct API
  calls report an explicit unsupported error and leave the source untouched. It is not approximated
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
  resubmit and test sender paths convert to modern SDK messages at send time.
  Received messages expose SDK-only broker metadata under the documented
  `ServiceBusExplorer.SystemProperties.*` keys. Review whether a future public
  editable-message contract should replace these legacy extension types before
  stage 4 removes their package.
- **Dead-letter move atomicity:** moving from a dead-letter subqueue now depends
  on Service Bus cross-entity transaction support. The source is completed
  only in the same transaction as the destination send; clients enable
  cross-entity transactions explicitly. A lock-loss or transaction failure can
  still have an ambiguous service outcome, so the operation is not retried as a
  new send. Live acceptance against the emulator and supported Azure tiers is
  a prerequisite for claiming this path is service-verified.
- **Received metadata:** use the native accessor for the source sequence, lock,
  delivery and dead-letter details. Names beginning with
  `ServiceBusExplorer.SystemProperties.` are ordinary application properties.

### Validation

With `FILE_VERSION=1.0.0.1`, the full Release solution build succeeds and the
Release unit suite passes (170 tests). The build retains the pre-existing
missing `MinimumRecommendedRules.ruleset` warning. Regression coverage includes
native properties/SAS/rule mapping, paged runtime queries, queue CRUD, rule
CRUD, default entity options, immutable-property guards, update responses,
legacy and current queue XML, filters, emulator management-only startup and
the outgoing HTTP administration URI. Message adapter tests cover editable
message conversion, `ViaPartitionKey` mapping, received SDK metadata, and
reserved-key collisions. No live Service Bus integration acceptance has been
performed for send, receive, settlement, or transactional moves.

## Decisions to review

- On-premises Service Bus Server (Windows auth, STS/runtime/management ports)
  remains parseable, but stage 2 reports its unsupported administration
  connection explicitly as described above.
- `ServiceBusEnvironment.SystemConnectivity` (connectivity mode) disappears;
  AMQP over TCP or WebSockets remains the transport choice.
- Live emulator acceptance still needs to verify administration capabilities
  and the separate AMQP endpoint during stage 3.

### Stage 3 readiness correction

The modern helper retains distinct administration and data endpoints. Emulator
resubmission uses AMQP TCP regardless of the WebSockets preference. Unedited
messages retain their sendable identity and routing fields. The editor preserves
AMQP Data and Value body types. The new-ID option changes MessageId only.
ViaPartitionKey maps to TransactionPartitionKey, but transactional send-via and
atomic dead-letter moves are not implemented by this resubmission stage.
The management-stage XML compatibility preflight remains in place.
### Management review follow-up (#911)

The unused Notification Hubs token-provider field and its assignments are removed;
this avoids dangling assignments when combined with #908 without including its SDK
upgrade in this branch. Re-run integrated CI after either migration lands.

Topic message counts in this migration are **subscription aggregates**, not unique
published messages: active, dead-letter and transfer counts sum the subscription
snapshots; scheduled count comes from the topic snapshot. A message copied to two
subscriptions can contribute twice. Runtime snapshots are not atomic. Listing uses
paged topic runtime reads, safe path filtering and at most four concurrent
subscription-runtime reads. It can still exhaust the configured timeout in a large
namespace. Laziness is deferred: skipping those reads currently supplies zero values
to counter filters, node-color thresholds and the selected-topic view. A follow-up
must represent unloaded counts explicitly and load them asynchronously without
changing those behaviors. This change does not claim that large-namespace latency
has been eliminated.

Live acceptance remains outstanding: **Validate management against live Azure and
the emulator**. Offline builds and synthetic tests do not replace that validation.

### Edited legacy message bodies (#912)

The resend editor preserves the original serialized String or ByteArray format
when editing a body detected as either legacy DataContract format. Unchanged bodies
retain their original bytes; Stream bodies edited as text remain UTF-8 streams.
The body-type selector does not silently reinterpret an existing message. This
preserves legacy GetBody<string>() / GetBody<byte[]>() consumers while retaining
IDs/routing unless Generate new MessageId is explicitly selected.

### Listener resubmission

The listener sends selected messages to the chosen queue or topic in a single
modern SDK batch. Replaying a received AMQP Data message preserves its original
body sections and application properties, excluding dead-letter and recovery
properties. For a supported WCF envelope addressed to another endpoint, the
listener rewrites the SOAP destination and preserves the payload, action and
other headers. The binary and text readers allow large strings and arrays
within the received body size and retain their depth and header quotas. If an
envelope cannot be fully read and re-encoded within the size limit, replay
keeps the original bytes. The receiver may therefore see the original SOAP
destination. Ordinary JSON, protobuf and compressed bodies are replayed verbatim.
The listener reports send failures and closes its sender and client.

### Destructive non-session read failures (#913)

ReceiveAndDelete rows are already removed from the broker when a batch arrives.
If inspection fails, queue/subscription and dead-letter views retain previously
consumed rows and every unmodified original in the failing batch, including rows
not yet inspected. The error is logged alongside the displayed partial result;
no retry or acknowledgment is attempted. Receiver/client cleanup failures also
retain the result. Callers without a reporting callback receive a
`PartialMessageReadException` carrying those rows and own their disposal. Peek
continues to fail without publishing a partial destructive result.

Native AMQP text and binary Values keep their wire type when edited. Other
Values can be copied when the modern codec reproduces their original section exactly. A body discarded by the legacy
receiver, including AMQP Sequence, stops replay before settlement. Legacy
DataContract String and ByteArray edits retain their serialized format. Ordinary
stream edits use UTF-8. Bulk and single replay exclude prior dead-letter and
NServiceBus recovery properties.
