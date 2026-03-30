# Changelog — ServiceBusExplorer (Tailormade fork)

## [1.4.0] - 2026-03-30

### Added
- Copy Body button in message preview pane (queues, deadletter, subscriptions)
- Dashboard right-click context menu: Refresh refreshes selected queue/subscription row
- Dashboard row click clears active search filter so target node is visible in TreeView
- version-bump.js tool supports nested .csproj repos (excludes Tests, obj, bin)

## [1.3.2] - 2026-03-29

### Changed
- Dashboard refresh button and auto-refresh now use ShowEntities flow (log panel, TreeView, and dashboard always in sync)
- Single node refresh (right-click queue/subscription) also updates corresponding dashboard row

### Added
- Ctrl+F keyboard shortcut focuses the TreeView filter box and selects text

### Fixed
- Filter snapshot invalidated on entity create/delete events (OnCreate/OnDelete handlers)

## [1.3.1] - 2026-03-29

### Changed
- Extracted TreeView filter logic from MainForm into TreeViewFilterHelper class (testability refactor)
- 7 new unit tests covering filter, restore, nested child, and case-insensitive scenarios

## [1.3.0] - 2026-03-29

### Added
- Dashboard row click navigates to corresponding TreeView node (queue or subscription)
- Copy dashboard row to clipboard: Ctrl+C for full row (TSV), right-click context menu for row or name
- TreeView filter now recursively searches inside topic subscription containers

### Fixed
- ContextMenuStrip disposal leak in DashboardControl
- Null guard on DashboardRowSelected prevents NRE when no connection active
- TreeView filter correctly finds subscriptions nested under "Subscriptions" container nodes

## [1.2.2] - 2026-03-29

### Added
- TreeView filter now searches inside topic subscriptions
- Subscription-level filtering: non-matching subscriptions hidden when topic is kept due to child match
- Child node snapshot/restore for clean filter clearing

## [1.2.1] - 2026-03-29

### Changed
- Dashboard shows subscriptions per topic instead of topics (topics always showed 0 counts)
- Subscription rows display "TopicName / SubscriptionName" with scheduled message counts

### Fixed
- Per-topic error handling: one failing topic no longer prevents loading remaining topics
- Scheduled message count now shown for subscriptions (was hardcoded to 0)

## [1.2.0] - 2026-03-29

### Added
- Dashboard tab with message counts per queue/topic (Active, Dead Letter, Scheduled, Total)
- Color coding: rows with Dead Letter > 0 highlighted in light red
- Auto-refresh with configurable interval (30s/60s/5min)
- Manual refresh button with last-refresh timestamp
- Tree node click auto-switches to Explorer tab

## [1.1.0] - 2026-03-29

### Added
- TreeView filter/search box for real-time filtering of queues, topics, and subscriptions
- 250ms debounce on filter input to avoid UI stutter
- Recursive matching: parent nodes stay visible when child nodes match
- Filter survives tree refresh (F5)
- Placeholder text "Filter queues/topics..." via Win32 EM_SETCUEBANNER

### Fixed
- Timer disposal: debounce timer now registered with components container for proper cleanup
