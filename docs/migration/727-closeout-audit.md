# #727 closeout audit and acceptance recommendation

Audited after d70006534ccca289b35693a06a53ed5d842848d2. The accompanying change makes ServiceBusHelper2.CreateAdministrationClient retain V2021_05 for both SAS and Entra, matching ServiceBusHelper.Connect. This matters for SKU/entity probes after upgrading SDK 7.21.0; previously those secondary clients took its newer default.

## Exact remaining legacy paths

Paths below are relative to src. Method names are the durable source anchors.

| Call site | Classification and network consequence |
| --- | --- |
| Common/Helpers/ServiceBusHelper.cs: Connect's replacement preparation, NamespaceManager property | Creates a legacy NamespaceManager for non-emulator SAS/Entra profiles; getter simply returns it, not lazy creation. Construction and retry configuration alone do not enumerate/send. Manager is retained for the separate-service wrappers below. Emulator keeps it null. |
| ServiceBusHelper.cs: ConnectionStringWithoutEntityPath | Uses legacy ServiceBusConnectionStringBuilder locally to clear EntityPath for legacy namespace management/factory compatibility. Still runs during non-emulator SAS Connect, so ordinary use is not independent of the legacy assembly, even when it performs no legacy network operation. |
| ServiceBusHelper.cs: MessagingFactory getter, CreateMessagingFactory | Lazy legacy transport API remains public for compatibility. Connect no longer invokes it; no production caller of this getter/factory remains after listener resubmission migration. External callers could still invoke it. |
| ServiceBusHelper.cs: legacy SendMessages(MessageSender), ReceiveMessages(MessageReceiver), CustomMessageSessionAsyncHandler/Factory | Legacy-capable helpers remain; no audited application client creation feeds these overloads. Do not claim public/plugin consumers cannot call them. Remove only with compatibility agreement. |
| ServiceBusExplorer/Forms/MainForm.cs: namespace tree population at loadEventHubs/loadRelays | Optional selection-dependent network operations: NamespaceManager.GetEventHubAsync/GetEventHubsAsync and ServiceBusHelper.GetRelays. Even with a Service Bus namespace address, selecting these categories can issue legacy management probes against that address. Queue/topic-only selection avoids them. |
| Common/WindowsAzure/ServiceBusEventHub.cs: Get/Create/Update/Delete; ServiceBusConsumerGroup.cs: Get/GetAll/Create/Update/Delete; ServiceBusPartition.cs: Get/GetAll | Active legacy Event Hubs management, not Service Bus queue/topic operations. |
| ServiceBusExplorer/Controls/HandleEventHubControl.cs and HandleConsumerGroupControl.cs refresh; MainForm.cs event hub update | Active legacy Event Hubs management via NamespaceManager. |
| Common/WindowsAzure/ServiceBusRelay.cs: Get/GetAll/Create/Update/Delete; Controls/HandleRelayControl.cs delete | Active legacy Relay management, a different service. Keep functionality. |
| Common/WindowsAzure/ServiceBusNotificationHub.cs and MainForm.cs notification enumeration | Network calls use Microsoft.Azure.NotificationHubs, not legacy Service Bus messaging. Wrapper still checks a legacy manager and uses legacy URI helpers. |
| Common/Helpers/ServiceBusNamespace.cs and Forms/ConnectForm.cs | Profile parsing, legacy transport defaults/settings and ServiceBusEnvironment URI creation are local compatibility, not broker traffic. SAS parser splits at the first '=' and retains padded key values. |
| Common/WindowsAzure/ServiceBusQueue.cs and ServiceBusTopic.cs URI/dead-letter helpers | Legacy static URI/path helpers remain, while queue/topic CRUD and lookup use ServiceBusManagementClient/modern administration. |
| ServiceBusMessageAdapter, ServiceBusReceivedMessageAdapter, MessageSerializationHelper, IBrokeredMessageInspector/Generator and UI BrokeredMessage rows | Local body/property/plugin compatibility. Legacy assembly remains required. Replacing it is a separate serialized-body/plugin contract migration, not deleting imports. |
| TestQueueControl/TestTopicControl senderFactories lists and MessagingFactory tooltips | Unpopulated compatibility remnants; no active legacy transport creation. Modern receiver/client lifecycle tests remain relevant. |

## SAS and Entra obligations

Modern client construction: ServiceBusHelper.CreateServiceBusClient -> GetServiceBusHelper2 -> ServiceBusHelper2.CreateServiceBusClient. Data-plane normalization removes UI-only TransportType/AuthMode/TenantId and EntityPath while the original scope remains in ServiceBusNamespace.EntityPath and ServiceBusHelper2.ProfileEntityPath. This is routing normalization, not a permission upgrade. ServiceBusQueue.GetQueueUsingEntityPath and ServiceBusTopic.GetTopicUsingEntityPath request the selected entity; IsPremiumNamespace bypasses namespace SKU queries for entity-scoped SAS. Management requires appropriate claims; a send-only key must not be described as sufficient for full browsing/CRUD. Signed SAS audience restrictions, if supported by a profile path, remain enforced by the service; this audit does not establish support for every precomputed-SAS input form.

Entra ordinary Service Bus uses the modern credential bridge and SDK scope. Connect's Service Bus audience probe uses the modern administration client. It also constructs a legacy token provider locally for shared management. A recognized audience mismatch switches to the Event Hubs audience and explicitly obtains its token via the old-provider adapter. Local cancellation/token failures and explicit Manage/40301 denial must not switch audiences (existing tests). The retained generic 401 fallback can still classify an otherwise ambiguous response as Event Hubs; validate denied-role behavior live rather than asserting that fallback is unreachable. Scope constants remain https://servicebus.azure.net and https://eventhubs.azure.net; the credential factory normalizes to /.default.

On-premises Windows/STS Service Bus Server is explicitly unsupported by modern administration and rejected; do not relabel it as emulator or silently drop this limitation.

## What is verified, and what is not

Static call-site audit establishes that the queue/topic management and messaging paths use modern SDKs when only those entity categories are selected. It does NOT establish that all default/mixed-category SAS startup is free of legacy network probes. It also does not establish that the legacy assembly can be removed.

Existing native fixtures to run: ServiceBusManagementClientTests, ManagementEnumerationTests, ManagementRenameTests, ServiceBusNamespaceEmulatorTests, ServiceBusNamespaceEntraTests, ServiceBusHelperEntraTests, ConnectionPreparationTests, ServiceBusMessageAdapterTests, SessionReadinessTests, SessionListenerLifecycleTests, SessionBrowsingTests, ListenerResubmissionTests, AdministrationApiVersionTests. Adapter fixtures preserve local body shapes/metadata; they cannot establish WCF receiver interoperability or broker settlement.

Verification in this executor: Release application/net472-test cross-build passed with the existing missing ruleset warning; all five focused net9 tests passed (four session-browser cases plus the administration request test), using production source/project references.

AdministrationApiVersionTests uses a local TCP HTTP responder and the actual production helper/SDK to check the emitted api-version. This is a no-broker protocol regression check, not emulator acceptance. Linux full net472 execution remains unavailable without Mono. FENRIR's d700065 run does not include this additional commit.

## Live acceptance matrix (all live rows pending in this executor)

| Environment / identity | Entry points and acceptance | Owner / constraint |
| --- | --- | --- |
| Cloud namespace SAS, queue/topic categories only | Connect; CRUD/rename; queue/topic/subscription/rule enumerate; send/peek/receive/settle; listener resubmit both menu entries; no legacy Service Bus transport | Authorized disposable entities; capture API/transport evidence |
| Cloud entity-scoped SAS | Connect directly to queue/topic; no namespace SKU/list requirements; send/receive according to claims; denied operations reported without widening scope | Test Manage and restricted keys separately |
| Cloud Entra | Connect, browser dismissal, caller cancellation, denied role, queue/topic operations; no unintended Event Hubs scope fallback | Isak/authorized identity; no credentials embedded in fixtures |
| Emulator SAS, TCP | Connect to management 5300/data 5672; queue/topic/rule CRUD; send/peek/settlement/defer/DLQ/session state/processors; restart recovery | #911 live hold remains Isak's; no WebSockets/Entra claims |
| Cloud sessions | List active and state-only sessions while another receiver owns a lock; initial list does not acquire; explicit state selection; receive/process/cancel/drain | SDK 7.21 API; service behavior still needs evidence |
| Emulator sessions | Same list/state checks; explicitly record unsupported response if API absent; never emulate by accepting all sessions | Emulator capability not inferred from SDK availability |
| Body/compatibility | API binary/text and structured AMQP resubmission; WCF receiver; metadata, properties, exported files and external inspector/generator | Native fixtures plus integration where applicable |
| Mixed-service selection | Event Hubs/Relay/Notification Hubs branches remain functional and recognizable as separate SDK traffic | Regression scope, not proof of Service Bus-only traffic |

## Existing emulator hosting options: read-only findings

The selected cloud workspace currently has Docker 29.9.2, Linux x86_64, about 18.9 GB reported RAM and five CPUs. `docker info` succeeds; `docker ps` was empty. It is a concrete candidate for an ephemeral emulator plus SQL container. No image pull, container start, image build, new environment, workflow dispatch, resource creation or paid infrastructure action was performed. Disk capacity, image pull policy, license acceptance and actual emulator startup remain untested. No FENRIR installation is needed for a cloud-hosted protocol test, but connectivity from FENRIR to that host is not established; running a modern .NET harness inside this workspace is the straightforward candidate.

The existing .github/workflows/build-test.yml runs windows-2022 and dotnet builds/tests. It contains no Linux emulator service/job. No existing ready emulator Actions path was found in the checked-out workflows. A future CI proposal would be separate from this read-only audit; do not publish an unrelated workflow merely to transfer/test a candidate.

Official emulator documentation describes a Linux Docker service plus SQL dependency, management/health port 5300, TCP-only transport, no Entra and no persistence across restarts. It still warns that the released community Service Bus Explorer is incompatible, so candidate compatibility must be demonstrated rather than presumed:
- https://learn.microsoft.com/en-us/azure/service-bus-messaging/test-locally-with-service-bus-emulator
- https://learn.microsoft.com/en-us/azure/service-bus-messaging/overview-emulator

## Issue/PR recommendation

Keep stages 6–9 stacked on #914 with stage-only diffs, no closing keyword. Describe the completed scope as Azure Service Bus runtime migration, preserving explicit local legacy compatibility and separate-service management. Before closing #727, obtain maintainer agreement on runtime migration versus complete WindowsAzure.ServiceBus package removal, finish Windows/native and live acceptance, and integrate the base stack. If full removal is required, track the concrete remaining Event Hubs/Relay management and BrokeredMessage/plugin/serialization work rather than pretending it is covered by current commits. Preserve #911's explicit live-validation hold.
