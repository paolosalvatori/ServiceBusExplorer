# Unpublished #727 continuation after Astra integration

This candidate combines the original signed continuation through `5072c019bea932e502230ac9e8074f68275586d0` with published #914 `e4d641f2ce5ea091113dd488bebbd3e7ad156af7`. Both histories are preserved by a new signed merge; the published PR stack is unchanged. The stage6–8 documents and initial closeout audit describe their historical candidates; this document records the synchronized behavior and verification.

## Composition decisions

- Keep SDK 7.21.0 non-locking queue/subscription session enumeration. Initial list binding suppresses state reads and clears selection; it does not accept sessions to simulate listing.
- Explicit row selection uses a short-lived modern session receiver to read state. This can contend with an existing session lock, unlike the former legacy browsable handle. Timeout/errors are reported; there is no lock-taking enumeration fallback. Keep #914's SessionStateView current-selection tracking, immediate clearing, stale-success/error suppression, stream disposal and closed-view guard. Reject invalid session selectors and already-cancelled work before creating a client.
- Keep #911's configured administration client and scope handling. Fallback administration probes explicitly retain API 2021-05 after the SDK upgrade. Keep ManagementPort, RuntimePort, scoped SAS audiences, Entra dismissal/caller cancellation and management-only profile guards.
- Listener resubmission uses modern sending with all payloads prepared before creating the client. Reuse the reviewed replay adapter for existing messages rather than the new-composition String helper. Preserve native AMQP bodies and legacy serialized bytes; strip reserved recovery properties from outbound copies. WCF conversion remains explicit. This listener action copies messages and performs no source settlement. The separate editor/DLQ destination and settle-after-send guards are unchanged.
- Keep #914's strict session receiver selection, worker-drain/disposal ordering and corrected restart assertions. Connection preparation no longer initializes an unused legacy messaging factory. Legacy NamespaceManager creation, lazy compatibility APIs and separate-service management remain.

## Executed verification

Release application and net472 test-project cross-build passed, with zero errors and the existing missing MinimumRecommendedRules.ruleset warning.

Eight source-linked net9 tests passed using the production session browser, state view and portable helper:

| Requirement | Evidence |
| --- | --- |
| Queue/subscription listing route and cancellation token, no accept call, client disposal | BrowseRoutesWithoutAcceptingSessionsAndDisposesClient (two cases) |
| Failed enumeration does not return partial results and disposes client | EnumerationFailurePropagatesAndDisposesClient |
| Cancellation does not return partial rows | CancellationDoesNotReturnPartialResults |
| Selected-state result wins over stale success/error; deselection clears without reading | ModernStateSelectionKeepsCurrentResultAndClearsOnDeselection (two cases) |
| Current error is reported; closed view ignores late result | ModernStateFailureIsReportedAndClosedViewDiscardsResult |
| Secondary management probe retains 2021-05 | EntityProbeKeepsPreviousAdministrationApiWithoutABroker |

The existing disposable Linux emulator acceptance was rerun with synthetic data on emulator 2.0.1 (manifest digest `sha256:5a96d893b245031740f7d46e0fe5ff282d24b78c4b7d761dd57590f3f010a9b3`) and SQL Edge 2.0.0 (`sha256:52605ea3251cbc4a7e03eccd458e775ae8a626a3afb4019bec66520a81e78170`). Six checks passed: production helper management read/edit/refresh over 5300; localhost TCP send/peek; active/state-only session fixture creation for queue and subscription; and production SessionBrowser enumeration for each, followed by independent acceptance, re-enumeration while locked and message preservation. Only loopback host binds and the disposable Compose project were used. These checks do not exercise WinForms or Azure.

## Unverified coverage and closure prerequisites

The full/focused net472 run could not start because Mono is unavailable. Native Windows tests are required for this exact synchronized candidate; #914's 891-test success does not cover these new changes. Added compiled cases include ListenerReplayPreservesNativeBodyAndRemovesRecoveryProperties (String/Value/Sequence), ListenerReplayKeepsSerializedStringBytes, InvalidStateSelectionNeverCreatesAClient and CancelledStateSelectionNeverCreatesAClient. Existing listener selection/menu, error/disposal, configuration and session lifecycle fixtures must also run. No successful execution of these net472 cases is claimed.

Before landing: run native Windows suite and actual queue/subscription toolbar/tree session-list and row-selection flows, close/refresh during state reads, listener destination selection and WCF/plugin fixtures. Confirm explicit state-read lock contention semantics with maintainers. Perform authorized cloud SAS/entity-scoped SAS/Entra and settlement/duplicate-detection acceptance. Isak retains #911's live test responsibility; neither FENRIR nor Azure was used here.

Before genuinely closing #727: integrate/review the base stack and continuation, agree whether the issue means Azure Service Bus runtime migration or full WindowsAzure.ServiceBus package removal, and resolve remaining Event Hubs/Relay management and BrokeredMessage/plugin/serialized-body compatibility obligations. The package is still present. This candidate does not justify a closing keyword or claim full package removal/live acceptance.
