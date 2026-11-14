# Migrating to Azure.Messaging.ServiceBus (#727)

Issue: https://github.com/paolosalvatori/ServiceBusExplorer/issues/727

The migration is staged so every stage builds, passes tests and leaves a
runnable application. Stages are stacked in order; see
`sdk-migration-decisions.md` for the upstream PR targeting constraints.

| Stage | Scope |
|-------|-------|
| 1. Connection layer | Recognise emulator connection strings; document compatibility. |
| 2. Management plane | `NamespaceManager` and `*Description` move to `ServiceBusAdministrationClient` and `*Properties`. |
| 3. Data plane | `BrokeredMessage`, senders, receivers and sessions move to `ServiceBusSender`, `ServiceBusReceiver`, `ServiceBusSessionReceiver`, `ServiceBusProcessor`. |
| 4. Cleanup | Remove `WindowsAzure.ServiceBus` and `Microsoft.ServiceBus`. |

## Stage 1: connection layer

- `UseDevelopmentEmulator=true` is parsed into `ServiceBusNamespace.UseDevelopmentEmulator`.
- Emulator endpoints (`sb://localhost[:port]`, `sb://127.0.0.1`) are accepted as
  host names; no fully qualified Azure domain is required.
- Connecting to an emulator namespace fails fast with an explicit
  `NotSupportedException`, because the legacy SDK cannot reach the emulator.
  Emulator connectivity is delivered by stage 2 (management) and stage 3 (data).

## Decisions to review

- On-premises Service Bus Server (Windows auth, STS/runtime/management ports)
  has no equivalent in the new SDK. It is kept parseable until stage 4, then
  reported as unsupported.
- `ServiceBusEnvironment.SystemConnectivity` (connectivity mode) disappears;
  AMQP over TCP or WebSockets remains the transport choice.
- The emulator's management endpoint may require a separate administration
  connection (port 5300); stage 2 will confirm this against the emulator.
