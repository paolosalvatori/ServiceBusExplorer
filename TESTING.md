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

### Independent review follow-up: connection stage (#910)

The raw transport dropdown updates only TransportType and RuntimePort assignments, preserving matching text in endpoints, entity paths and credentials. The Connect entry path publishes entity selection only on success and restores prior WebSockets/connectivity settings on false returns or exceptions. Synthetic tests exercise the real dropdown and the settings-publication method used by Connect. No live connection, credentials or configuration were used; adapter preparation/rollback is handled in the following management stage.
