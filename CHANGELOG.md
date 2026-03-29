# Changelog — ServiceBusExplorer (Tailormade fork)

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
