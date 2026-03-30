# ServiceBusExplorer — Testing

## Acceptance Tests

| # | Test | Command/Action | Expected | Status |
|---|------|----------------|----------|--------|
| 1 | Build | `dotnet build src/ServiceBusExplorer.sln` | 0 errors | ✅ |
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
| 18 | Dashboard row → TreeView | Click queue row in dashboard | TreeView selects matching queue, switches to Explorer tab | ⬜ |
| 19 | Dashboard sub row → TreeView | Click subscription row in dashboard | TreeView expands topic, selects subscription | ⬜ |
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

**Score: 2/38 (5%)**

Items 3-17 require manual testing with a live Azure Service Bus connection.

---

**Legend:** ✅ pass | ❌ fail | ⬜ not tested | 🔄 flaky
