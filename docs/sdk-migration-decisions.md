# SDK migration decisions for review

The Modernize SDKs milestone is being implemented in issue order: #472,
#687, then #727. The branches form a dependency chain in that order.

## Notification Hubs (#472)

The SDK upgrade adds FCM HTTP v1 service-account credentials and notifications.
Legacy API-key credentials remain available for reading and editing existing
hubs; they do not provide a supported modern Android delivery path. Review
whether to remove that compatibility option after existing users migrate.

The new SDK no longer exposes the old claim-based `AllowRule`. Saving a hub
with unsupported claim-based authorization reports an error instead of silently
discarding its rules. A separate compatibility implementation would be needed
if editing those rules must remain supported.

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

## Pull request dependencies

The task branches are stacked, but an upstream pull request cannot target a
branch that exists only in a contributor's fork. Until the preceding branch
is available upstream or its PR merges, an upstream PR targets `main` and
includes its ancestors. Merge in issue order and rebase later branches if
an earlier PR is squash-merged; do not merge a later PR independently.

The November 18 #909 dependency reconciliation incorporates #908's final Notification
Hubs content at `bf8c7aa0369e3930977cd1e5a17fdf30e29c30db`. The NH control, designer,
and three regression-test files match that head (only two trailing comment spaces are trimmed in the designer); the shared ServiceBusHelper
contains the same NH settings and connection normalization while retaining #909's
Event Hubs changes. Legacy and v1 credentials are preserved unless explicitly
replaced/removed, and both FCM template bodies write back through the same handler.
This is a new signed commit, without rewriting history or modifying/merging #908.
#909 therefore no longer depends on a future content reconciliation to obtain these
FCM fixes; the existing PR merge-order guidance still applies to the stacked history.

Partition readers share the listener's metadata consumer. Stop waits for readers
(and their final successful checkpoint flush) without closing that reusable client;
control disposal closes it once all readers finish. Checkpoint batches use the
configured maximum batch size or a five-second interval, including idle reads.
Only successfully processed events advance the pending position. The existing
checkpoint store remains in memory until application-close persistence.
