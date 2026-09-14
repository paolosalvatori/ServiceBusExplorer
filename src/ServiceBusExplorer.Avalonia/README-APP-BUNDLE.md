# macOS .app Bundle Instructions

## Building the Application

To create a native macOS .app bundle:

```bash
cd ServiceBusExplorer.Avalonia
./build-macos-app.sh
```

This script will:
1. Publish the application for macOS (Apple Silicon / arm64)
2. Create the proper .app bundle structure
3. Copy all necessary files and resources
4. Convert the icon to macOS format (.icns)
5. Set proper executable permissions

## Output Location

The application will be created at:
```
ServiceBusExplorer.Avalonia/bin/Release/ServiceBusExplorer.app
```

## Testing the Application

To test the app:
```bash
open ServiceBusExplorer.Avalonia/bin/Release/ServiceBusExplorer.app
```

## Installing to Applications

To install the app in your Applications folder:
```bash
cp -r ServiceBusExplorer.Avalonia/bin/Release/ServiceBusExplorer.app /Applications/
```

Then you can:
- Launch from Finder by double-clicking
- Find it in Spotlight search
- Add it to your Dock

## Architecture

This build is configured for:
- **Runtime:** osx-arm64 (Apple Silicon: M1/M2/M3)
- **Self-contained:** Yes (no .NET SDK required to run)
- **Framework:** .NET 8.0

### For Intel Macs

To build for Intel Macs, modify the build script to use `osx-x64` instead of `osx-arm64`:
```bash
-r osx-x64
```

## Files Included

- `Info.plist` - macOS application metadata
- `build-macos-app.sh` - Automated build script
- This README file

## Troubleshooting

### "App is damaged" warning
If macOS shows a warning that the app is damaged, run:
```bash
xattr -cr /Applications/ServiceBusExplorer.app
```

### Permission issues
Make sure the main executable has execute permissions:
```bash
chmod +x ServiceBusExplorer.app/Contents/MacOS/ServiceBusExplorer.Avalonia
```
