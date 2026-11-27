# Stage 6 candidate: session discovery

Base: `6c8c021467848bc787d51ec91436a7b2cb30a640` (#914).

Queue and subscription Get Message Sessions (toolbar and tree context menu) now await the supported non-locking SDK API. Session IDs are application-owned rows, rather than legacy MessageSession instances. Initial grid population no longer implicitly acquires the first session to read its state; selecting a row still runs the existing explicit state inspection. Concurrent list clicks are ignored while a list is pending. Failed enumeration leaves the old rows intact and reports the exception; no fallback acquires all sessions.

Azure.Messaging.ServiceBus changes from 7.20.1 to 7.21.0. Administration requests explicitly retain V2021_05, the prior default, for both cloud and emulator; the new browsing operation uses AMQP. This preserves the requested API version, but is not proof of emulator compatibility.

Official references:
- https://learn.microsoft.com/en-us/azure/service-bus-messaging/message-sessions#list-message-sessions
- https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/servicebus/Azure.Messaging.ServiceBus/CHANGELOG.md
- https://learn.microsoft.com/en-us/dotnet/api/azure.messaging.servicebus.servicebusclient.getmessagesessionsasync

The unfiltered API discovers sessions with active messages or persisted state. It does not enumerate empty sessions with neither, and does not provide the legacy session object's lock metadata. The property grid therefore shows the identifier only; session state remains separately inspectable.

## Verification

Release cross-build of the application and net472 test project succeeds (existing missing MinimumRecommendedRules.ruleset warning). Four focused tests pass on net9 using source-linked production SessionBrowser.cs/SessionInfo.cs and the same SessionBrowsingTests.cs: queue/subscription routing, token forwarding, multiple yielded IDs, disposal, partial enumeration failure, and cancellation. No receiver is configured in the mock, so a regression to accepting sessions fails the test.

The full net472 test invocation aborts because Mono is absent. No WinForms execution or live Azure/emulator testing has occurred in this executor. These focused tests do not constitute UI/integration verification.

Windows entry-point checks required before landing:
1. Queue and subscription with multiple sessions: invoke both Get Message Sessions buttons and tree context menus. Verify rows, count, responsiveness, and repeated-click guard.
2. Leave another receiver holding a session; discovery must still succeed without acquiring/releasing its lock. Initial list population must not read session state. Explicitly select a row to inspect its state, including the first row.
3. Persist state on an otherwise empty session and verify it appears. Check an empty entity and service failures, including unsupported emulator response, without stale state/partial results.
4. Close the view during enumeration; verify no late binding to disposed controls. Check cloud SAS, Entra, and emulator management with preserved 2021-05 API.
5. Run the full Windows suite. #911's live validation remains Isak's handoff.

## Remaining production inventory by feature

This is a feature inventory, not a claim that namespace-import counts equal active legacy traffic.

| Feature | Remaining usage / action |
| --- | --- |
| Azure Service Bus listener resubmission | `Controls/ListenerControl.cs:ResubmitSelectedMessages` still calls MessagingFactory.CreateMessageSenderAsync and SendBatchAsync. Migrate next with body conversion/WCF compatibility and resource-lifetime tests through the resubmit menu. |
| Azure Service Bus connection setup | `ServiceBusHelper.Connect/CreateMessagingFactory`, legacy namespace manager/token provider and retry policy still initialize shared legacy state. Remove only after auditing consumers; Event Hubs/Relay/Notification Hubs share this state. |
| Azure Service Bus compatibility helpers | Legacy MessageSender/MessageReceiver overloads and CustomMessageSessionAsyncHandler classes remain. Call-site audit is needed before deleting unused helpers; modern send/receive overloads coexist. |
| Message UI, import/export, inspectors/generators | BrokeredMessage remains in MessageSerializationHelper, ServiceBusMessageAdapter, ServiceBusReceivedMessageAdapter and public IBrokeredMessageInspector/IBrokeredMessageGenerator contracts. This is local serialization/plugin compatibility, distinct from a legacy network client. A replacement needs fixtures for WCF/DataContract bodies, metadata, custom properties and external plugin contracts. |
| Entity metadata and URI helpers | WindowsAzure wrappers retain legacy enums/filter/authorization types and ServiceBusEnvironment/QueueClient path helpers. Convert independent local helpers after confirming persisted configuration/XML compatibility. |
| Event Hubs | Send/read data plane uses modern producer/consumer clients, but ServiceBusEventHub, consumer-group/partition management and legacy descriptions still depend on NamespaceManager. Decide whether migration scope includes ARM management and its authentication/permission model. |
| Notification Hubs | Network management/registrations use Microsoft.Azure.NotificationHubs. The wrapper still takes/checks a legacy NamespaceManager and uses shared URI helpers. This is shared plumbing, not Service Bus messaging. |
| Relay | ServiceBusRelay still uses legacy namespace management and description types; Relay is a separate service, not Azure.Messaging.ServiceBus. Do not delete its workflow to remove a package. |

WindowsAzure.ServiceBus 7.0.1 is still directly referenced by Common and the application (and tests). Package removal and issue closure are therefore not justified by this slice. Maintainer decisions are needed on compatibility guarantees for external BrokeredMessage plugins/WCF/serialized files and whether #727 includes Event Hubs/Relay management replacement. No workflow is silently removed here.
