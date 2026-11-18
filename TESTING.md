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

- Notification Hubs review fixes belong to #908 (verified at `6b521728`). #909's competing credential/template edits and test were removed; inherited #908 base content is unchanged. The branches still require reconciliation in dependency order before merging.
- `EventHubClientCleanupTests` covers concurrent producer disposal (including a synchronous failure) and shared-consumer lifetime until all partition readers have completed or failed.
- `EventHubCheckpointBatchTests` covers count/timed idle flush, stop flush of the last successful event, disable/re-enable behavior, and a failed checkpoint retry. Checkpoints update the existing in-memory store; file persistence remains at application close. The store is synchronized across partition readers.
- `EventHubBatchRetryTests` also verifies acknowledged group counts/logs on partial failure and excludes failed-attempt/backoff time from latency.
- `ServiceBusHelperEntraTests.EventHubFactoriesAcceptModernConnectionStrings` constructs producer/consumer clients from namespace/entity-scoped strings and strings with an existing transport option without legacy rewriting.

Additional Windows/live Azure checks: grow the sender count after a run, repeat after a sender fault, and close the sending tab during a slow send/close. Verify responsiveness and no retained producer connections. Start/stop a multi-partition listener repeatedly and verify only its shared consumer owns the connection; closing one reader must not close the consumer under other readers. Verify partial checkpoint batches flush on idle/stop and replay still starts before a failed inspector event.
