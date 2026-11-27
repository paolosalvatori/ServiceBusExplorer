# ServiceBusExplorer — Testing

## Acceptance Tests

| # | Test | Command/Action | Expected | Status |
|---|------|----------------|----------|--------|
| 1 | Build | `dotnet build src/ServiceBusExplorer.slnx` | 0 errors | ✅ |
| 2 | Unit tests | `dotnet test src/ServiceBusExplorer.Tests` | All 49 pass | ✅ |
| 3 | TreeView filter | Type in filter box above tree | Nodes filter in real-time | ⬜ |
| 4 | Filter after refresh | F5 after filtering | Filter re-applied | ⬜ |
| 5 | Recursive match | Filter on subscription name | Parent topic visible | ⬜ |
| 6 | Filter clear | Clear filter text | All nodes restored | ⬜ |
| 7 | Debounce | Type rapidly | No UI stutter, filters after 250ms pause | ⬜ |
| 8 | Dashboard tab visible | Connect to namespace | Dashboard tab is default, shows DataGridView | ⬜ |
| 9 | Dashboard data | Connect to namespace with queues/topics | Grid shows Name, Type, Active, DLQ, Scheduled, Total | ⬜ |
| 10 | Dashboard refresh | Click Refresh button | Data reloads, status shows timestamp | ⬜ |
| 11 | DLQ color coding | Have queue with DLQ > 0 | Row highlighted in light red | ⬜ |
| 12 | Auto-refresh | Enable checkbox, set 30s | Grid refreshes automatically | ⬜ |
| 13 | Tree click → Explorer | Click tree node after viewing Dashboard | Switches to Explorer tab | ⬜ |
| 14 | Subscriptions in Dashboard | Connect with topics that have subscriptions | Shows "TopicName / SubName" rows, not topic rows | ⬜ |
| 15 | Per-topic error isolation | Disconnect mid-load or have inaccessible topic | Other topics still load, error logged | ⬜ |
| 16 | Subscription filter | Filter on subscription name | Parent topic visible, non-matching subscriptions hidden | ⬜ |
| 17 | Topic name filter | Filter on topic name | Topic visible with all subscriptions intact | ⬜ |
| 18 | Dashboard single-click → tree sync | Click queue row in dashboard | TreeView selects matching queue, stays on Dashboard tab | ⬜ |
| 19 | Dashboard double-click → explorer | Double-click queue row in dashboard | TreeView selects queue, switches to Explorer tab | ⬜ |
| 20 | Copy row Ctrl+C | Select row, Ctrl+C | Clipboard contains TSV of all columns | ⬜ |
| 21 | Copy row context menu | Right-click → Copy row | Same TSV as Ctrl+C | ⬜ |
| 22 | Copy name context menu | Right-click → Copy name | Clipboard contains entity name only | ⬜ |
| 23 | Nested subscription filter | Filter on subscription name (3-level tree) | Container node preserved, matching sub visible | ⬜ |
| 24 | Dashboard refresh syncs all | Click Refresh in Dashboard | Log panel, TreeView, and Dashboard all refresh | ⬜ |
| 25 | Auto-refresh syncs all | Enable auto-refresh, wait interval | Same full sync as manual refresh | ⬜ |
| 26 | Single node refresh → dashboard | Right-click queue → Refresh, check dashboard | Dashboard row shows updated counts | ⬜ |
| 27 | Ctrl+F focuses filter | Press Ctrl+F anywhere | Filter box gets focus, text selected | ⬜ |
| 28 | Create entity → filter sync | Create queue while filter active | Filter snapshot invalidated, new entity visible | ⬜ |
| 29 | Delete entity → filter sync | Delete queue while filter active | Filter snapshot invalidated, entity removed | ⬜ |

| 30 | Copy Body — enabled | Select message with body | "Copy Body" button enabled in Message Text grouper | ⬜ |
| 31 | Copy Body — click | Click "Copy Body" | Clipboard contains exact message body text | ⬜ |
| 32 | Copy Body — disabled | No message selected / empty body | "Copy Body" button disabled | ⬜ |
| 33 | Copy Body — deadletter | Select DLQ message, click "Copy Body" | Clipboard contains DLQ message body | ⬜ |
| 34 | Copy Body — subscription | Open subscription, select message, click "Copy Body" | Same behavior as queue | ⬜ |
| 35 | Dashboard click clears filter | Filter active ("queue"), click dashboard row | Filter box cleared, tree restored, clicked node selected | ⬜ |
| 36 | Dashboard click no filter | No filter, click dashboard row | Node selected, filter box unchanged | ⬜ |
| 37 | Dashboard right-click Refresh | Right-click queue row → Refresh | Row counts update, TreeView node also refreshes | ⬜ |
| 38 | Dashboard Refresh disabled | Right-click with no row selected | "Refresh" menu item disabled | ⬜ |
| 39 | Dashboard refresh stays on tab | Right-click Refresh on dashboard row | Counts update, stays on Dashboard tab | ⬜ |
| 40 | TreeView click → explorer | Click tree node | Switches to Explorer tab (unchanged behavior) | ⬜ |
| 41 | Dashboard hint label visible | Open Dashboard tab | "Single click: sync tree  |  Double click: open in Explorer" visible below grid | ⬜ |
| 42 | Hint label stays on resize | Resize form window | Label stays anchored below grid | ⬜ |
| 43 | Delete queue → dashboard sync | Delete queue "MyQueue" | Dashboard row "MyQueue" disappears immediately | ⬜ |
| 44 | Create queue → dashboard sync | Create queue "NewQueue" | Dashboard row "NewQueue" appears with 0,0,0,0 | ⬜ |
| 45 | Delete topic → dashboard sync | Delete topic "MyTopic" (with sub1, sub2) | Rows "MyTopic / sub1" and "MyTopic / sub2" disappear | ⬜ |
| 46 | Delete subscription → dashboard sync | Delete subscription "sub1" from "MyTopic" | Row "MyTopic / sub1" disappears; "MyTopic / sub2" remains | ⬜ |
| 47 | Create subscription → dashboard sync | Create subscription "NewSub" under "ExistingTopic" | Row "ExistingTopic / NewSub" appears | ⬜ |

**Score: 2/47 (4%)**

Items 3-17 require manual testing with a live Azure Service Bus connection.

---

**Legend:** ✅ pass | ❌ fail | ⬜ not tested | 🔄 flaky

## Event Hubs migration regressions (PR #909)

Automated coverage in `ServiceBusExplorer.Tests/Helpers`:

- `EventHubBatchRetryTests`: later partition-key group failures (transient and exhausted retries) never resend an acknowledged earlier group.
- `PartitionMetadataTests`: requests start concurrently, results retain partition order, and failures wait for outstanding requests before disposal.
- `EventHubConnectionValidationTests`: compatible IoT endpoint/name acceptance and rejection of HostName strings, messages/events, and mismatched EntityPath without exposing credentials.
- `PartitionInspectorTests`: thrown/null inspector results preserve original failure position and report the stop/replay policy.

Windows/live Azure checks still required:

1. Open a multi-partition listener; verify metadata loads and all partition choices appear.
2. In IoT Hub > Built-in endpoints, copy the Event Hubs-compatible connection string and name into the IoT listener dialog; verify receiving with the chosen consumer group. Verify an old HostName string or messages/events is rejected with guidance.
3. Enable checkpointing and use an inspector that throws on a known event (also test returning null). Verify the log identifies the original partition/sequence/offset, the listener stops, the button returns to Start, and the failed event is not checkpointed. Disable the faulty inspector and restart from the saved checkpoint to verify replay. Repeat while stopping/closing the listener to check cleanup races and absence of hangs.

### November 18 review follow-up

- `EventHubClientCleanupTests` covers concurrent producer disposal (including a synchronous failure) and shared-consumer lifetime until all partition readers have completed or failed.
- `EventHubCheckpointBatchTests` covers count/timed idle flush, stop flush of the last successful event, disable/re-enable behavior, and a failed checkpoint retry. Checkpoints update the existing in-memory store; file persistence remains at application close. The store is synchronized across partition readers.
- `EventHubBatchRetryTests` also verifies acknowledged group counts/logs on partial failure and excludes failed-attempt/backoff time from latency.
- `ServiceBusHelperEntraTests.EventHubFactoriesAcceptModernConnectionStrings` constructs producer/consumer clients from namespace/entity-scoped strings and strings with an existing transport option without legacy rewriting.

Additional Windows/live Azure checks: grow the sender count after a run, repeat after a sender fault, and close the sending tab during a slow send/close. Verify responsiveness and no retained producer connections. Start/stop a multi-partition listener repeatedly and verify only its shared consumer owns the connection; closing one reader must not close the consumer under other readers. Verify partial checkpoint batches flush on idle/stop and replay still starts before a failed inspector event.

### Copilot follow-up: retry, cancellation, and application close

- `EventHubSendCancellationTests` exercises single and grouped sends against SDK 5.12.2 exceptions: permanent failures stop after one attempt; transient failures can recover. It also covers cancellation during an SDK send, before later groups, retry backoff, long think time, and before starting (13 cases).
- `ListenerShutdownCoordinatorTests` verifies final checkpoint completion before persistence, joining repeated close requests, waiting for every listener even when one fails, retrying a failed close, and disposal unregistering while a flush is pending, and propagation of checkpoint-file write failures (5 cases).
- The existing two-argument retry API and its synchronization-context behavior remain intact. Event Hubs sends use the cancellation-aware overload.

Manual Windows/live Azure checks still required: close the app with partial checkpoint batches, repeat Close while readers stop, restart to verify saved offsets, and exercise checkpoint/close errors. Stop/Cancel a sender during a slow network send, retry delay, and long think time; verify no later partition-key group is sent. No manual UI/live Azure results are claimed by the automated tests.

### November 22 review: checkpoint identity, shutdown, and retry policy

- Normal close awaits listener drain and final checkpoints, then reports persistence failures and closes. Each persistence stage is independent so an earlier write failure does not suppress the checkpoint attempt. Windows shutdown/Task Manager close does not cancel the close or wait for UI continuations: it requests stop and makes a best-effort write of the completed checkpoint snapshot. An interrupted partial batch may replay after system shutdown; no unprocessed event is advanced. Normal close retains full final-flush ordering.
- Saved offsets always resume exclusively. The Offset Inc. checkbox is disabled in this listener because it offers checkpoint/time starts, not explicit offset entry.
- IoT checkpoints now use the SDK's fully qualified endpoint host plus the existing hub/group/partition keys. Rotating credentials does not change this identity. Old `Endpoint=...` namespace fields are sanitized to the matching endpoint host on load. Old `HostName=...` IoT identities become `legacy-iot:<host>` with credentials removed on the next successful save. There is no unambiguous local mapping from an IoT HostName to its Event Hubs-compatible endpoint, so those old leases are not automatically applied: select a known safe starting time (or replay from earliest) for the first run. Existing files/backups are not retroactively erased. No real credential/checkpoint file was read during development; regression inputs are synthetic.
- Pending checkpoints accepted while enabled flush once after disabling; newly disabled events never enter the pending batch.
- Producer clients disable SDK retries (`MaximumRetries = 0`); both send paths use one application retry budget, only for transient SDK errors and timeouts. Cancellation remains immediate between groups/attempts and interrupts backoff; acknowledged groups are not repeated.
- Added coverage for safe identity normalization, exclusive/empty checkpoint positions, bounded retry exhaustion, unexpected permanent errors, nonblocking system shutdown, and close-after-persistence-error behavior. Updated disable/flush regression expectations.

Not run locally: Windows GUI shutdown/read-only-directory workflows, IoT live endpoint migration/credential rotation, and live Azure outage/cancel checks. Linux Release build is available; net472 test execution requires Windows CI because Mono is absent.

### November 25 review: legacy checkpoint consolidation and failure logging

- `EventHubCheckpointPersistenceTests` uses only temporary synthetic files through `ReadCheckpoints`, `GetLease`, and `WriteCheckpoints`. Duplicate normalized Endpoint/HostName identities retain every partition and the highest sequence; equal sequences choose the ordinal serialized lease representation, independently of input order. Unmappable identities are persisted as `[]` after a successful load. Failed/unattempted reads cannot authorize an overwrite; a missing file still permits new checkpoints.
- `PartitionListenerFailureLogTests` runs a real WinForms message loop and listener reader/stop path with a synthetic failing consumer. It checks that the failure is synchronously visible before logging stops and that asynchronous reader shutdown completes without blocking the UI. Existing inspector replay and final-checkpoint batch tests remain applicable.
- No real checkpoint file, credential, or Azure service is used. Live inspector replay and interactive desktop evidence still require Windows/manual verification; the Linux executor cannot run the net472 WinForms application.

### Connection-stage readiness follow-up (#910)

Saved emulator profiles use the raw connection editor so their development flag, endpoint/port, and credentials survive a save/connect round trip. Saving an emulator profile validates its parsed endpoint without passing development-only fields to the legacy SDK. Parsed field names/values are trimmed; invalid endpoint ports are rejected and emulator display names retain the complete host. At this connection-only stage, emulator Connect still rejects before changing any active helper state or creating clients.

Synthetic tests cover the saved ConnectForm selection/build path, padded fields, full IP/DNS display names, invalid ports, and unchanged helper fields after a rejected emulator Connect. Actual emulator connectivity and Windows save-to-configuration interaction remain unverified; no live service is used.

### Management-stage readiness follow-up (#911)

- Reconnect clears old namespace-scoped adapters before constructing the new set; emulator management cannot retain Relay/Event Hubs/Notification Hubs adapters from a previous cloud connection. Secondary/UI helpers receive a separate management connection string using the emulator HTTP management port. Their data-plane factory explicitly rejects emulator purge/send/receive at this management-only stage.
- Import preflights the complete XML before dispatch. Emulator documents containing Relay, EventHub, or NotificationHub entities are rejected as a unit. Rule import accepts legacy default/true/false filters, plain SQL filters/actions, and string correlation fields/properties. Typed/nested dictionary extensions, nonempty SQL parameters, unknown rule types and malformed shapes are rejected before any entity mutation; configure those rules explicitly instead. Legacy correlation values preserve embedded colons. Ordinary nested topic/subscription XML remains supported, including false boolean settings.
- Create/update returns the authoritative mutation response without a subsequent runtime-count query. RuntimeProperties is absent until an explicit refresh; placeholder counts are not a fresh broker measurement. This prevents runtime-query failures from causing retries of successful mutations.
- Synthetic tests cover reconnect adapter isolation, secondary administration construction and data-plane refusal, mixed/nested import preflight, ordinary nested import and all queue/topic/subscription create/update paths with runtime queries unavailable. Live emulator connectivity, interactive Windows probes/import dialogs and live Azure operations remain unverified.

Rule compatibility follow-up: baseline nested `$Default`, true, false, plain SQL/action and string correlation fixtures exercise the import entry point. Typed correlation and parameterized SQL fixtures verify whole-document rejection before an earlier queue can be created.

### Resubmission-stage readiness follow-up (#912)

- Secondary helpers keep separate administration (emulator HTTP management port) and data-plane (AMQP port 5672) connection strings. Emulator data-plane clients force AMQP TCP even when the global WebSockets preference is enabled; cloud transport selection remains configurable. The management-only data-plane guard remains in the preceding #911 stage, while #912 enables modern client construction for resubmission.
- The actual MessageForm preparation path preserves the original raw body when the displayed text is unchanged, including binary bytes that cannot round-trip through display decoding. Editing text replaces the body with UTF-8 bytes; WCF conversion remains its separate path. Stream/String/ByteArray display choices do not reserialize an unchanged body. Generating a new MessageId changes only that ID; session, partition, transaction partition, correlation and reply routing are retained.
- Windows regressions instantiate the real MessageForm and invoke the preparation method used by Submit, then check the modern outbound message. Tests cover binary bytes, Generate new MessageId off/on, routing, UTF-8 edits and source immutability. Adapter coverage includes ViaPartitionKey to TransactionPartitionKey. Preserving this field does not introduce transactional send-via or atomic moves.
- The corrected #911 XML preflight/legacy-rule compatibility tests are inherited unchanged. Live emulator/Azure delivery, interactive target selection and DLQ move behavior are not claimed; screenshots remain deferred.

### Receiver-stage readiness follow-up (#913)

Received broker sequence, lock and delivery metadata populate the legacy SDK's received-only properties rather than editable application properties. The pinned legacy SDK's internal setters are used by the adapter; regressions check these setters and the actual MessageForm preparation entry point. Original modern metadata remains separately associated with the adapted message. Application names that resemble system names retain their values and cannot override broker identity.

Receive All drains in bounded batches until an empty receive; Peek All retains the configured top limit. Non-session queue test receivers construct only modern clients, allowing emulator AMQP TCP without creating a legacy factory. Session test receivers retain their documented legacy boundary at this stage.

Moves from partitioned sources or transfer dead-letter queues are explicitly unsupported before client creation; no atomic cross-partition guarantee is claimed. Dead-letter failures report confirmed IDs and warn that unconfirmed outcomes need broker verification. Cleanup attempts every held lock and client independently. Synthetic receiver/sender tests cover a later failure after one successful operation and simultaneous abandonment/disposal errors.

Release cross-build and exact-head Windows CI cover these regressions. Live Azure/emulator transactions, interactive bulk actions, partition routing and lock-expiry behavior are not verified; no live data was accessed and screenshots remain deferred.

### Session-stage readiness follow-up (#914)

- Processor callbacks propagate inspector, tracking and cancellation failures; successful return can no longer trigger automatic acknowledgment after failed work. Manual completion follows successful processing. Synthetic callback tests cover automatic/manual completion and cancellation.
- Stop, close and disposal share a single per-run shutdown task. Normal container/main close awaits processor callback drain before client disposal; logging stays available until drain completes. Tracking posts an awaited update to the UI instead of blocking it, and session counters reset between runs. A Windows message-loop regression runs a real listener callback while a fake processor is draining, checks overlapping shutdown identity, and verifies processor/client/log ordering. Windows system shutdown remains best-effort and does not cancel the OS shutdown.
- Queue/topic/subscription manual receive transactions only complete when the receive helper succeeds, commit is requested and cancellation is absent. Synthetic ambient transactions verify committed versus aborted outcomes.
- Entity-wide session peek uses an ordinary receiver and never acquires a session lock. Receive All reads bounded batches across successive sessions until accept timeout; a selected session remains scoped to that session. Session rows read state through the enumerated browsable session with its own SDK operation timeout. Row changes clear old state immediately, and late reads and errors cannot overwrite the latest selection.
- Legacy SDK session enumeration remains intentional; this change does not provide modern enumeration or emulator support for that legacy boundary. Live Azure/emulator settlement, transaction enlistment, lock renewal/expiry and session enumeration are unverified. Interactive row-change/state-fetch errors, normal application close with real processors, Windows OS shutdown, and repeated live start/stop remain manual checks. Automated UI tests use synthetic SDK clients; no live service or screenshots were used.

The final shutdown guard also waits for an in-progress SDK start before processor/client disposal. Manual queue/topic/subscription test controls track delegate work through EndInvoke/callback completion, cancel then await that work before closing shared clients and stopping logs, and defer disposal until that drain completes. Normal container/main close participates in the same tasks. Synthetic Windows message-loop tests cover startup cancellation and workers synchronously invoking the UI while their drain is awaited. The sender and receiver cancel their think-time waits during shutdown. In-flight SDK operations retain their own cancellation and timeout behavior.
### Independent review follow-up: connection stage (#910)

The raw transport dropdown updates only TransportType and RuntimePort assignments, preserving matching text in endpoints, entity paths and credentials. The Connect entry path publishes entity selection only on success and restores prior WebSockets/connectivity settings on false returns or exceptions. Synthetic tests exercise the real dropdown and the settings-publication method used by Connect. No live connection, credentials or configuration were used; adapter preparation/rollback is handled in the following management stage.

### Independent review follow-up: management stage (#911)

Compact legacy correlation XML preserves every adjacent string property, including embedded colons. Unsupported Filter/Action wrapper attributes or text and unsupported empty-action metadata are rejected during whole-document preflight before earlier entities can be created. Connection preparation uses a separate replacement helper; active profile/adapters publish only after successful preparation. Failed candidates close their provisional legacy messaging factory. A synthetic localhost profile whose unknown setting fails SDK construction verifies preservation after DNS succeeds, without broker I/O. Existing emulator/cloud isolation, management-only boundaries and legacy supported rule fixtures remain covered. Live reconnect/authentication, broker resource cleanup and interactive import remain manual checks.

### Independent review follow-up: resubmission stage (#912)

Secondary helpers retain original profile EntityPath metadata separately from normalized SDK connection strings. Queue and topic editor scope checks use this metadata, and IsPremiumNamespace itself returns the existing conservative non-premium default without constructing an administration client for entity-scoped SAS access. Audited callers: HandleQueueControl and HandleTopicControl are the only scope-check callers; direct ServiceBusHelper2 callers retain connection-string fallback. Synthetic helper/copy and throwing probe-double tests cover queue/topic scope preservation and no namespace SKU probe. Live entity-scoped SAS UI access remains a manual check; no namespace/network request is used by these tests.

### Stage 4 AMQP bodies and transaction progress

- `ServiceBusAmqpBodyTests` covers a mixed Data/Value/Sequence destructive receive batch,
  unchanged resend, detached nested AMQP values and both adapter paths on SDK 7.20.1.
- `MessageFormResubmissionTests.StructuredAmqpEditorPreservesOriginalUntilTextEdited`
  exercises the actual editor preparation with original/new IDs. Structured bodies have a
  preview; changing its text explicitly produces UTF-8 Data, as stated in the editor title.
- `ServiceBusTransactionProgressTests` uses fake senders/receivers and local transactions:
  a later batch failure retains acknowledged nontransactional progress; failed/cancelled
  transactions publish no committed progress. Receive-and-delete reports the already
  removed batch even when a later inspector fails. No broker is contacted.
- Live broker transaction enlistment/settlement, network failure ambiguity and interactive
  Azure/emulator workflows still require manual verification. Linux cross-build does not
  execute these .NET Framework/WinForms tests; Windows CI runs them.

### Stage 5 independent-review regressions

- Manual queue/topic/subscription workers use `ExecuteManualReceive` to distinguish failed
  processing from a successful requested rollback. The failed session inspector matrix covers
  transactions on/off and commit on/off; failure always ends that worker's session loop.
- The actual listener callback and checkbox are exercised with a per-run settlement snapshot.
  Checkbox changes during a run cannot suppress manual settlement or add duplicate settlement.
- Queue close tests run a real WinForms message pump and await the manager's final UI work,
  including failure and repeated close. Client disposal follows that work; cancellation before
  completion prevents a new final refresh from being queued.
- Synthetic SDK clients verify ordinary modern receivers for queue/subscription DLQs and
  cloud queue transfer DLQs, including cleanup. The emulator rejects transfer DLQ browsing
  before client creation. Subscription transfer DLQ browsing is explicitly unsupported in
  this view instead of incorrectly reading the ordinary DLQ. Partitioned browse entry points
  use the modern path; unsafe move guards remain. Legacy session enumeration remains unchanged.
- No live Azure/emulator operations or screenshots are part of these tests. Actual broker
  transaction/settlement behavior, live DLQ paging and interactive close under network outages
  still need manual validation; Linux builds do not execute .NET Framework WinForms tests.
### Connection profile and filter publication follow-up

Saved raw profile loading uses the same trimmed, case-insensitive keys as explicit transport
editing. Programmatic transport selection does not rewrite the raw profile. Regression tests
exercise the saved-profile dropdown with padded transport/runtime keys and unchanged credentials.
The dialog stages queue/topic/subscription filters; MainForm publishes them only after connection
success. Tests cover actual dialog acceptance plus successful, rejected and throwing attempts.
All fixtures are synthetic; no live connection or broker mutation is used.

### Atomic XML export and supported correlation values

The existing XML rule format represents string correlation values only. Export rejects typed
values, ambiguous property names and parameterized SQL rules instead of silently converting or
omitting them. A serialization failure aborts the whole in-memory export before destination
selection or writing. Completed exports replace files atomically. String correlation round-trip
tests cover adjacent properties, Unicode, leading spaces, colons in values and CR/LF; rejection
tests exercise the broker model through the real prepare/save boundary and preserve an existing
backup. All management clients are test doubles; no broker writes occur.

### Transactional receive and inspector clone follow-up

ReceiveAndDelete with a requested or ambient transaction is rejected before reads. All three
manual test controls validate the combination before startup; helpers enforce the same boundary.
Tests cover the actual queue validation entry and single/batch helper calls with zero fake reads.
Legacy single PeekLock statistics are published only after Complete/DeadLetter succeeds; the
settlement boundary tests cover success and failure without a broker.
Both receive and send inspector boundaries transfer retained AMQP body metadata to unchanged
clones. Value/Sequence tests cover both directions and deliberate edits, which remain Data edits.
Live legacy receiver settlement faults and interactive emulator/Azure workflows remain untested.

Session follow-up regressions exercise the modern session browse helper with synthetic accept, receive, receiver-disposal and client-disposal failures, preserving earlier consumed rows and reporting the failure. Pumped WinForms tests exercise the shared Start preparation and permanent Close paths in all three manual controls, plus EndInvoke/start-failure accounting even when error logging throws. Stop remains reversible. Live session broker behavior and manual visual checks remain unverified.
Queue/receiver closure tests exercise in-place inspector stream edits through receive and resend, source identity through edited inspector clones and the editor's Submit preparation, and confirmed destructive-receive accounting before single/batch inspector failure. The shared legacy boundary is exercised offline; actual legacy broker receive/settlement and live emulator operations are not run.
