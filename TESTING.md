# ServiceBusExplorer — Testing

## Acceptance Tests

| # | Test | Command/Action | Expected | Status |
|---|------|----------------|----------|--------|
| 1 | Build | `msbuild src/ServiceBusExplorer.sln /p:Configuration=Debug` | 0 errors | ⬜ |
| 2 | Unit tests | `dotnet test src/ServiceBusExplorer.Tests` | All pass | ⬜ |
| 3 | TreeView filter | Type in filter box above tree | Nodes filter in real-time | ⬜ |
| 4 | Filter after refresh | F5 after filtering | Filter re-applied | ⬜ |
| 5 | Recursive match | Filter on subscription name | Parent topic visible | ⬜ |

**Score: 0/5**

---

**Legend:** ✅ pass | ❌ fail | ⬜ not tested | 🔄 flaky
