#!/bin/bash
set -e

# Configuration
APP_NAME="ServiceBusExplorer"
BUNDLE_ID="com.servicebus.explorer"
VERSION="1.0.0"
PROJECT_DIR="$(cd "$(dirname "$0")" && pwd)"
PUBLISH_DIR="$PROJECT_DIR/bin/Release/net8.0/osx-arm64/publish"
APP_DIR="$PROJECT_DIR/bin/Release/$APP_NAME.app"

echo "🚀 Building Service Bus Explorer for macOS..."

# Clean previous builds
echo "🧹 Cleaning previous builds..."
rm -rf "$APP_DIR"
rm -rf "$PUBLISH_DIR"

# Publish the application
echo "📦 Publishing application..."
dotnet publish "$PROJECT_DIR/ServiceBusExplorer.Avalonia.csproj" \
    -c Release \
    -r osx-arm64 \
    --self-contained true \
    -p:PublishSingleFile=false \
    -p:PublishTrimmed=false

# Create .app bundle structure
echo "📁 Creating .app bundle structure..."
mkdir -p "$APP_DIR/Contents/MacOS"
mkdir -p "$APP_DIR/Contents/Resources"

# Copy the published files
echo "📋 Copying application files..."
cp -r "$PUBLISH_DIR"/* "$APP_DIR/Contents/MacOS/"

# Copy Info.plist
echo "📄 Copying Info.plist..."
cp "$PROJECT_DIR/Info.plist" "$APP_DIR/Contents/Info.plist"

# Convert icon from .ico to .icns if sips is available
echo "🎨 Converting icon..."
if [ -f "$PROJECT_DIR/Assets/app.ico" ]; then
    # Create a temporary directory for icon conversion
    TEMP_ICONSET="$PROJECT_DIR/bin/app.iconset"
    mkdir -p "$TEMP_ICONSET"
    
    # Extract the largest size from .ico and create different sizes
    sips -s format png "$PROJECT_DIR/Assets/app.ico" --out "$TEMP_ICONSET/icon_512x512.png" -Z 512 2>/dev/null || echo "⚠️  Warning: Could not convert icon"
    
    if [ -f "$TEMP_ICONSET/icon_512x512.png" ]; then
        # Create required icon sizes
        sips -z 16 16 "$TEMP_ICONSET/icon_512x512.png" --out "$TEMP_ICONSET/icon_16x16.png" 2>/dev/null
        sips -z 32 32 "$TEMP_ICONSET/icon_512x512.png" --out "$TEMP_ICONSET/icon_16x16@2x.png" 2>/dev/null
        sips -z 32 32 "$TEMP_ICONSET/icon_512x512.png" --out "$TEMP_ICONSET/icon_32x32.png" 2>/dev/null
        sips -z 64 64 "$TEMP_ICONSET/icon_512x512.png" --out "$TEMP_ICONSET/icon_32x32@2x.png" 2>/dev/null
        sips -z 128 128 "$TEMP_ICONSET/icon_512x512.png" --out "$TEMP_ICONSET/icon_128x128.png" 2>/dev/null
        sips -z 256 256 "$TEMP_ICONSET/icon_512x512.png" --out "$TEMP_ICONSET/icon_128x128@2x.png" 2>/dev/null
        sips -z 256 256 "$TEMP_ICONSET/icon_512x512.png" --out "$TEMP_ICONSET/icon_256x256.png" 2>/dev/null
        sips -z 512 512 "$TEMP_ICONSET/icon_512x512.png" --out "$TEMP_ICONSET/icon_256x256@2x.png" 2>/dev/null
        cp "$TEMP_ICONSET/icon_512x512.png" "$TEMP_ICONSET/icon_512x512@2x.png"
        
        # Create .icns file
        iconutil -c icns "$TEMP_ICONSET" -o "$APP_DIR/Contents/Resources/app.icns" 2>/dev/null && echo "✅ Icon converted successfully" || echo "⚠️  Warning: iconutil failed"
        
        # Clean up
        rm -rf "$TEMP_ICONSET"
    fi
else
    echo "⚠️  Warning: app.ico not found, skipping icon conversion"
fi

# Make the executable file executable
echo "🔧 Setting executable permissions..."
chmod +x "$APP_DIR/Contents/MacOS/ServiceBusExplorer.Avalonia"

# Create a PkgInfo file (optional but recommended)
echo "APPL????" > "$APP_DIR/Contents/PkgInfo"

echo ""
echo "✅ Build complete!"
echo "📍 Application created at: $APP_DIR"
echo ""
echo "To test the application:"
echo "  open \"$APP_DIR\""
echo ""
echo "To copy to Applications folder:"
echo "  cp -r \"$APP_DIR\" /Applications/"
echo ""
