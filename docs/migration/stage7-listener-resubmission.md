# Stage 7: listener resubmission

Parent: stage 6 `4783215345ee03209765c898f246c4a6ededd88a`.

Both listener resubmit menu items retain destination selection and use the same modern send continuation. Selected rows are snapshotted, converted using the existing API/WCF conversion functions, then passed through ServiceBusMessageAdapter. Sender and client are disposed, including failures; original rows remain owned by the listener. Empty selections create no client. Successful count is logged only after the send finishes.

This preserves the existing single-batch behavior. Oversized batches fail without application-level splitting or retries. No change to WCF conversion, serializer format, message IDs, or external inspector interfaces is intended.

Release app/test cross-build passed. Added three helper tests for metadata/body preservation, row reuse, disposal order, failure propagation, and empty/cancelled work; added one Windows UI test through the actual destination-selected menu continuation for selected rows and delayed success logging. These net472 tests are compiled but not executed here: Mono is absent. FENRIR must run the full suite and specifically ListenerResubmissionTests and ResubmitMenuContinuationSendsSelectedRowsAndLogsOnlyAfterSuccess. UI test bypasses only the modal destination picker; manually verify each menu opens the picker and Cancel sends nothing.

No live broker/container is assumed by automated tests: clients/senders are mocked and the UI test injects a pending send. Live Azure/emulator send, Entra/SAS connectivity, and WCF receiver interoperability still require separate validation. No claims of exactly-once delivery are added; service timeouts retain their normal uncertain-delivery semantics.

Runtime audit after this change finds no application CreateMessageSender/CreateQueueClient/CreateSubscriptionClient/CreateMessageReceiver or AcceptMessageSession calls into the legacy factory. Legacy helper overloads remain as compatibility code. Connect still eagerly constructs an unused MessagingFactory; removing that initialization is the next narrowly scoped cleanup. NamespaceManager remains shared with Event Hubs/Relay/Notification Hubs and is not removed in this slice.
