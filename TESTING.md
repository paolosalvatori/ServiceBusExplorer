# ServiceBusExplorer — Testing

## Acceptance Tests

| # | Test | Command/Action | Expected | Status |
|---|------|----------------|----------|--------|
| 1 | Build | `dotnet build src/ServiceBusExplorer.sln` | 0 errors | ✅ |
| 2 | Unit tests | `dotnet test src/ServiceBusExplorer.Tests` | All 42 pass | ✅ |
| 3 | TreeView filter | Type in filter box above tree | Nodes filter in real-time | ⬜ |
| 4 | Filter after refresh | F5 after filtering | Filter re-applied | ⬜ |
| 5 | Recursive match | Filter on subscription name | Parent topic visible | ⬜ |
| 6 | Filter clear | Clear filter text | All nodes restored | ⬜ |
| 7 | Debounce | Type rapidly | No UI stutter, filters after 250ms pause | ⬜ |

**Score: 2/7 (29%)**

Items 3-7 require manual testing with a live Azure Service Bus connection.

---

**Legend:** ✅ pass | ❌ fail | ⬜ not tested | 🔄 flaky
