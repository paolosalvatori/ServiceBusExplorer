# SDK migration decisions for review

This document describes the Event Hubs data-plane SDK migration. The separate
Notification Hubs SDK migration is not included.

## Event Hubs (#687)

Sending, partition receiving, and runtime metadata queries use
`Azure.Messaging.EventHubs` 5.12.2. Custom Event Hubs generators and inspectors
must be rebuilt against `EventDataEnvelope` rather than the legacy SDK's
`EventData`.

Entity management still uses the existing SAS-capable `NamespaceManager`
and its description models. The modern Event Hubs data-plane package does
not replace those operations. Moving them to Azure Resource Manager requires
Entra authentication, Azure resource identifiers, and management-plane RBAC;
silently requiring those would break existing SAS-only connections.
Consequently, #687 does not remove every legacy Event Hubs SDK type.
Review this authentication and compatibility choice separately from the
data-plane upgrade.

Local partition checkpoint files retain their existing JSON format and legacy
lease model. This avoids invalidating saved offsets; the listener does not
introduce distributed ownership or Azure Blob checkpoint storage.

Partition readers share the listener's metadata consumer. Stop waits for readers
(and their final successful checkpoint flush) without closing that reusable client;
control disposal closes it once all readers finish. Checkpoint batches use the
configured maximum batch size or a five-second interval, including idle reads.
Only successfully processed events advance the pending position. The existing
checkpoint store remains in memory until application-close persistence.
