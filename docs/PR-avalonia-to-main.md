# Add Avalonia-based cross-platform Service Bus Explorer host

## Summary

This PR introduces a new cross-platform desktop host for Service Bus Explorer based on Avalonia and adds a shared portable core layer for modern Azure Service Bus operations.

The branch adds:

- a new `Common.Core` project with portable abstractions, models, and modern Azure SDK-based implementations
- a new `ServiceBusExplorer.Avalonia` desktop application targeting .NET 8
- cross-platform connection, entity browsing, queue management, and message inspection workflows
- macOS app bundle support for local packaging
- minor cleanup in existing Event Grid and WinForms code paths

Diff size: **45 files changed, 4,912 insertions, 99 deletions**

## Why

The current WinForms host is Windows-centric and tightly coupled to legacy SDK assumptions. This PR starts a cross-platform path by separating Service Bus operations into a reusable core and adding a new Avalonia UI host that can run on Windows, macOS, and Linux.

## What Changed

### 1. Added a new portable core layer: `Common.Core`

New abstractions and models were introduced to decouple the new host from WinForms and from Windows-only dependencies.

#### New abstractions

- `IMessagingService`
- `INamespaceService`
- `ISavedConnectionsService`
- `CoreWriteToLogDelegate`

#### New portable models

- `ConnectionProfile`
- `ConnectionAuthMode`
- `ConnectionTransportType`
- `EntityInfo`
- `EntityProperties`
- `EntityTypeConstants`
- `LogEntry`
- `PropertyRow`

#### New service implementations

- `ModernMessagingService`
- `ModernNamespaceService`
- `JsonSavedConnectionsService`
- `ConnectionStringParser`
- `AadTokenCredentialFactory`

### 2. Added a new Avalonia desktop application: `ServiceBusExplorer.Avalonia`

A new .NET 8 Avalonia host was added and wired into the solution.

#### App structure

- `App.axaml` / `App.axaml.cs`
- `Program.cs`
- `MainWindow.axaml` / code-behind
- `ConnectDialog.axaml` / code-behind
- shell and feature view models

#### New view models

- `ShellViewModel`
- `ConnectViewModel`
- `EntityTreeViewModel`
- `EntityDetailViewModel`
- `PeekOptions`
- `ViewModelBase`

### 3. Implemented modern Service Bus operations on top of Azure SDKs

The new core uses:

- `Azure.Messaging.ServiceBus`
- `Azure.Messaging.ServiceBus.Administration`
- `Azure.Identity`

Supported operations include:

- connect using SAS or Azure AD / Entra ID
- enumerate queues, topics, and subscriptions
- load a hierarchical entity tree
- create and delete queues
- inspect queue, topic, and subscription properties
- update queue forwarding settings
- send messages
- peek messages
- receive-and-delete messages
- peek dead-letter messages
- receive-and-delete dead-letter messages
- purge queues
- purge dead-letter queues

### 4. Added saved connection support

The Avalonia host can load, save, and delete named connection profiles.

Implementation details:

- saved connections are persisted as JSON in the user application-data folder
- AAD metadata is stored without secrets
- SAS keys and passwords are currently stored in plain text in the profile file

### 5. Added Avalonia UI workflows

#### Connect dialog

The new connect dialog supports:

- SAS connection string input and parsing
- endpoint-based AAD connection setup
- saved connection selection
- transport selection
- entity-type selection fields
- filter fields
- optional "save connection as" behavior

#### Main shell

The new main window includes:

- connection/disconnection flow
- entity tree sidebar
- refresh actions
- queue create/delete actions
- entity detail tabs for overview, settings, messages, dead-letter, and output

#### Message experience

The message tabs support:

- peek vs receive-and-delete behavior
- top/all/last-N fetch behavior
- message body preview
- multi-message send
- dead-letter inspection

### 6. Added macOS packaging support

The branch adds:

- `Info.plist`
- `build-macos-app.sh`
- `README-APP-BUNDLE.md`
- application icon assets

This provides a local path to produce a macOS `.app` bundle for the Avalonia host.

### 7. Solution updates

The solution now includes:

- `Common.Core`
- `ServiceBusExplorer.Avalonia`

### 8. Minor updates outside the new host

Small changes were also made in existing code:

- Event Grid library cleanup / modernization
- minor WinForms control cleanup
- minor test cleanup in `RetryHelperTests`

## Notes / Current Limitations

This PR establishes the new cross-platform foundation, but a few areas remain incomplete or intentionally limited:

- the active implementation path is centered on **SAS** and **AAD** via the modern Azure SDKs
- the Avalonia UI exposes a Windows compatibility mode, but the new shell is not yet wired to a legacy Windows adapter path
- entity selection and filter fields are captured in connection profiles, but they are not yet enforced during entity-tree loading
- saved SAS secrets are currently stored in plain text; OS keychain integration is still future work

## Impact

This PR creates the baseline architecture for a cross-platform Service Bus Explorer without replacing the existing WinForms host. It introduces a reusable shared core and a new Avalonia application that can evolve independently while keeping the legacy application available.

## Validation

Validation performed for this PR description:

- branch diff review against `main`
- commit review for `main..avalonia`

Not validated in this pass:

- full build
- automated tests
- manual UI verification
- end-to-end Service Bus connectivity checks

## Commits

- `d279cf1` Avalonia added
- `7e5aabf` moved Send button
