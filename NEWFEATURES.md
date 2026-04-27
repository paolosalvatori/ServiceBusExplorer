- TreeView search/filter box: real-time filtering of queues, topics and subscriptions (Ctrl+F to focus)
- Dashboard tab: message counts (Active, Dead Letter, Scheduled, Total) for all queues and subscriptions at a glance
- Dashboard auto-refresh: configurable interval (30s / 1min / 5min) with manual refresh button
- Dashboard navigation: single-click syncs TreeView selection; double-click opens entity in Explorer tab
- Dashboard right-click: refresh a single row without reloading the full namespace
- Copy message body to clipboard: "Copy Body" button in message preview pane
- Microsoft Entra ID / Azure AD interactive browser sign-in for Azure Service Bus namespaces
- Saved Entra connections persist metadata only (`Endpoint`, `AuthMode=AAD`, optional `TenantId`, `TransportType`, `EntityPath`)
- Connect dialog supports switching between SAS and Azure Active Directory authentication modes
- Dashboard rows stay in sync when queues or subscriptions are created or deleted

- HandlePartitionControl: IncomingBytesPerSecond OutgoingBytesPerSecond
- EventData.SerializedSizeInBytes is used to calculate KB/sec in the partitionlistenercontrol
- Added new properties (LastEnqueuedOffset and LastEnqueuedTimeUtc) to PartitionDescription control 
- Fixed visualization of event data properties in the partition listener control
- Greatly improved message tracking and clear of the partition listener control
- Relay Message saved to file
- Support for Persistent and Dynamic Relay Services
- Export Persistent Relay Services
- Event Hub Metrics
- All Metric
- Partition Consumer
- Added Metrics to consumer groups 
- New behavior: no chart is shown if a metric doesn't have any data
notification hubs metrics



