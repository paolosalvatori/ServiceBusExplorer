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
