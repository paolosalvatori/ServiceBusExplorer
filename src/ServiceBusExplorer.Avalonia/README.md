# Service Bus Explorer Avalonia for macOS

`ServiceBusExplorer.Avalonia` is the cross-platform desktop host for Service Bus Explorer. It is built with Avalonia 11 on .NET 8 and is the macOS-focused module for browsing Azure Service Bus namespaces, inspecting queues and topics, and running common message operations without the Windows-only WinForms host.

The existing WinForms application remains the primary Windows host while this Avalonia module is brought toward feature parity.

## Requirements

- macOS 10.15 or later
- .NET 8 SDK for development and local runs
- Azure Service Bus namespace access through either:
  - a SAS connection string with the required Manage/Send/Listen rights, or
  - Microsoft Entra ID interactive browser sign-in

The bundled macOS app is self-contained, so it does not require the .NET SDK on the target machine.

## Run from Source

From the repository `src` directory:

```bash
dotnet run --project ServiceBusExplorer.Avalonia
```

Or from this module directory:

```bash
dotnet run
```

The app starts at the connect screen. Use `File -> Connect...` or the connect button to open the namespace connection dialog.

## Build a macOS App Bundle

The module includes a helper script that publishes the application for Apple Silicon and creates a native `.app` bundle.

```bash
cd ServiceBusExplorer.Avalonia
./build-macos-app.sh
```

The app bundle is written to this path, relative to the repository `src` directory:

```text
ServiceBusExplorer.Avalonia/bin/Release/ServiceBusExplorer.app
```

From the repository `src` directory, run it locally with:

```bash
open ServiceBusExplorer.Avalonia/bin/Release/ServiceBusExplorer.app
```

From this module directory, the equivalent command is:

```bash
open bin/Release/ServiceBusExplorer.app
```

Install it into Applications with:

```bash
cp -r ServiceBusExplorer.Avalonia/bin/Release/ServiceBusExplorer.app /Applications/
```

The script currently targets `osx-arm64`. For Intel Macs, change the runtime in `build-macos-app.sh` from `osx-arm64` to `osx-x64`.

More bundle-specific notes are in [README-APP-BUNDLE.md](./README-APP-BUNDLE.md).

## Connect to a Namespace

The connect dialog supports:

- SAS connection strings
- Microsoft Entra ID interactive browser authentication
- saved connection profiles
- AMQP TCP and AMQP WebSockets transport selection
- entity type selection
- optional queue, topic, and subscription filter inputs

For Entra ID, enter the namespace endpoint as either:

```text
sb://<namespace>.servicebus.windows.net/
```

or:

```text
<namespace>.servicebus.windows.net
```

Tenant ID is optional. If it is left blank, the app uses the organizations endpoint for work or school accounts.

## Saved Connections

Avalonia stores saved connections as JSON in the user's application data directory. On macOS the default location is:

```text
~/Library/Application Support/ServiceBusExplorer/connections.json
```

Entra ID profiles store metadata only. SAS profiles currently store the connection string in the JSON file, so protect this file like any other local secret store.

## Supported Workflows

Current macOS workflows include:

- browse queues, topics, subscriptions, and Notification Hubs
- view queue, topic, and subscription runtime counts
- create and delete queues
- refresh the full entity tree or the queue branch
- inspect entity overview properties
- edit queue forwarding settings
- send messages to queues and topics
- peek active and dead-letter messages
- receive and delete active and dead-letter messages
- purge queue and dead-letter queue contents
- view message system properties, application properties, and decoded body text
- view operation output in the Output tab

Queues and topics with slash-separated names are displayed as folder-like tree paths.

## Current Limitations

- The app bundle script builds Apple Silicon by default.
- Windows/on-premises authentication is compatibility-mode only and is not supported on macOS.
- NetMessaging and WCF-based workflows remain Windows-only.
- Event Hubs and Relay are selectable in the connection dialog but are not fully implemented in the Avalonia macOS UI.
- Notification Hubs discovery currently requires a SAS connection string.
- Import/export, advanced inspectors, Event Grid, session-aware receive, scheduled/deferred message workflows, and full settings parity are still migration work.
- The app is not code-signed or notarized by the build script.

If macOS reports that the app is damaged after copying it from an untrusted location, clear quarantine attributes:

```bash
xattr -cr /Applications/ServiceBusExplorer.app
```

## Project Layout

```text
ServiceBusExplorer.Avalonia/
  App.axaml                  Avalonia application resources
  Program.cs                 Desktop application entry point
  Views/                     Main window and connect dialog AXAML
  ViewModels/                Shell, connect, entity tree, and detail view models
  Assets/                    Application icon assets
  Info.plist                 macOS bundle metadata
  build-macos-app.sh         macOS .app bundle build script
```

The Avalonia host depends on `Common.Core`, which contains cross-platform connection models, saved connection storage, and modern Azure SDK services.

## Development Notes

Build the module with:

```bash
dotnet build ServiceBusExplorer.Avalonia.csproj
```

Create a release publish manually with:

```bash
dotnet publish ServiceBusExplorer.Avalonia.csproj -c Release -r osx-arm64 --self-contained true
```

Keep macOS changes isolated to this module and portable service changes in `Common.Core` unless a feature explicitly needs shared behavior.
