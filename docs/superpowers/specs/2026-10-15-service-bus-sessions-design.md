# Service Bus sessions and processor migration

## Context

The `#727` data-plane migration already routes non-session queue and
subscription browsing/receiving through `ServiceBusReceiver`, and uses a
`BrokeredMessage` adapter to keep existing inspectors and UI controls working.
Session-oriented browsing, test receiving, session-state inspection, and the
continuous listener still use `WindowsAzure.ServiceBus`. The listener also
uses legacy message-handler callbacks for both session and non-session
entities.

This stage replaces those session paths and listener callbacks without
attempting to remove the legacy package or changing Event Hubs, Notification
Hubs, Relay, or XML import/export behavior.

## Goals

- Use `ServiceBusSessionReceiver` for session-specific browsing, receiving,
  and session-state operations.
- Use modern processor APIs for continuous queue/subscription listeners,
  including session-enabled entities.
- Preserve existing listener controls, statistics, logging, tracking, message
  inspectors, and visible message properties.
- Preserve receive mode, configured concurrency, auto-renewal, cancellation,
  and message settlement behavior as closely as the new SDK allows.
- Keep session state opaque and unchanged. Existing state-display behavior
  remains available; this stage does not rewrite or clear it.
- Ensure SDK-only received message metadata continues through
  `ServiceBusReceivedMessageAdapter` and is not silently discarded.
- Leave legacy editable message/inspector extension contracts in place as a
  compatibility boundary.

## Design

Use the `ServiceBusClient` factory already exposed by `ServiceBusHelper2`.
Create session receivers with the queue or topic/subscription overloads and
the requested receive mode. Replace listener message pumps with
`ServiceBusProcessor` for non-session entities and the SDK's session-aware
processor for session-required entities. The listener owns the client and
processor lifetime; starting and stopping the listener starts/stops processing
and disposes the associated SDK resources asynchronously.

Convert each received SDK message to the existing `BrokeredMessage` UI and
inspector representation with `ServiceBusReceivedMessageAdapter`. Keep the
native received message and processor/receiver settlement handle available
until settlement finishes: the compatibility object is for inspection and
display, not a substitute for the SDK's lock or settlement operations.
Processor callbacks honor the existing auto-complete configuration and use
explicit SDK settlement operations where the current listener does so.
Expected cancellation during stop is not reported as a processing error;
other callback and settlement failures remain visible through the existing
logging path.

Session-state bytes are not inferred from a message or reconstructed from
visible properties. Read them from the accepted session and retain the
existing text display convention. No migration path changes the state.

The manual-receive controls keep their current UI contracts: session
selection, count/timeout, peek versus consume, inspectors, and rendered
messages. Only the underlying Service Bus client and receiver operations
change. Existing UI code continues to use the compatibility model where
required.

## Alternatives considered

1. **SDK processors and session receivers (recommended):** use SDK-managed
   processor lifecycle/concurrency for continuous listeners and explicit
   receivers for user-triggered session operations. This follows the modern
   SDK model while preserving the current UI boundary.
2. **Hand-written receive loops for listeners:** repeatedly accept sessions
   and receive messages manually. This can avoid processor event callbacks but
   duplicates lifecycle, renewal, concurrency, retry, and cancellation logic.
3. **Retain the legacy session callback layer:** smallest immediate code
   change, but it leaves the primary session/listener migration incomplete and
   does not resolve the legacy dependency for those workflows.

## Compatibility and boundaries

- Preserve `IBrokeredMessageInspector` and existing message display/logging
  behavior through the adapter; do not expose SDK settlement objects to
  extension interfaces.
- Preserve session identifiers, session state bytes, application properties,
  sendable system properties, and SDK-only receive metadata.
- Do not make session state editable or add a new serialized state format.
- Preserve queue/subscription receive-mode semantics and cancellation on
  listener stop/close.
- Legacy extension types and unrelated message-editing APIs remain in scope
  only as compatibility seams. Removing `WindowsAzure.ServiceBus` is a later
  cleanup stage.
- No live Azure/emulator acceptance is implied; report that limitation
  separately from automated validation.

## Validation

Add focused regression coverage for session receiver configuration and
message/session-state adaptation where the current test architecture permits.
Run the focused tests, then the full Release suite with
`FILE_VERSION=1.0.0.1`, the Release solution build, and `git diff --check`.
Review listener start/stop and error paths to verify cancellation and async
disposal. The stage is complete only when the full solution still builds and
the full unit suite passes.

## Follow-up decisions

- A future stage must decide how and when the legacy `BrokeredMessage`
  inspector/generator contracts can be replaced before removing their SDK
  dependency.
- Live service acceptance is still needed for session locks/settlement and
  processor behavior against supported Azure profiles and the Service Bus
  emulator.
- This stage does not claim `#727` complete; package cleanup and any remaining
  legacy extension API migration stay open.
