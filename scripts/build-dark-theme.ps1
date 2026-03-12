# build-dark-theme.ps1
# ─────────────────────────────────────────────────────────────────────────────
# Build ServiceBusExplorer with Dark Theme
#
# Requirements:
#   - Git
#   - .NET SDK 8+ (dotnet CLI)  →  https://dotnet.microsoft.com/download
#   - .NET Framework 4.7.2 Developer Pack (to compile the net472 target)
#       https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472
#     (usually comes with Visual Studio 2022)
# ─────────────────────────────────────────────────────────────────────────────
param(
    [string]$RepoUrl   = "https://github.com/paolosalvatori/ServiceBusExplorer.git",
    [string]$OutputDir = "$PSScriptRoot\output",
    [string]$Version   = "dark-theme"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Info  { param($m) Write-Host "  [INFO]  $m" -ForegroundColor Cyan }
function Ok    { param($m) Write-Host "  [ OK ]  $m" -ForegroundColor Green }
function Warn  { param($m) Write-Host "  [WARN]  $m" -ForegroundColor Yellow }
function Fail  { param($m) Write-Host "  [FAIL]  $m" -ForegroundColor Red; exit 1 }
function Title { param($m) Write-Host "`n━━━  $m  ━━━" -ForegroundColor Magenta }

Title "ServiceBusExplorer — Build with Dark Theme"

# ── 1. Check prerequisites ───────────────────────────────────────────────
Title "Checking prerequisites"

# Git
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Fail "Git not found. Install from https://git-scm.com"
}
Ok "Git: $(git --version)"

# dotnet CLI — required for SDK-style projects
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Fail @"
.NET SDK not found!

Install .NET SDK 8 (or higher) from:
  https://dotnet.microsoft.com/download

After installation, close and reopen PowerShell.
"@
}
$dotnetVersion = dotnet --version
Ok ".NET SDK: $dotnetVersion"

# Check if .NET Framework 4.7.2 targeting pack is installed
# (required to compile the net472 target)
$fx472 = "HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"
$fxRelease = 0
if (Test-Path $fx472) {
    $fxRelease = (Get-ItemProperty $fx472 -ErrorAction SilentlyContinue).Release
}
# Release 461808+ = .NET Framework 4.7.2
if ($fxRelease -lt 461808) {
    Warn ".NET Framework 4.7.2 may not be installed (release=$fxRelease)."
    Warn "If build fails, install the Developer Pack from:"
    Warn "https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472"
} else {
    Ok ".NET Framework 4.7.2+ detected (release=$fxRelease)"
}

# ── 2. Clone/update the repository ──────────────────────────────────────
Title "Cloning repository"

$repoDir = "$PSScriptRoot\ServiceBusExplorer"
if (Test-Path $repoDir) {
    Info "Repository already exists, updating to main..."
    Push-Location $repoDir
    git fetch origin
    git reset --hard origin/main
    Pop-Location
} else {
    Info "Cloning $RepoUrl ..."
    git clone $RepoUrl $repoDir
}
Ok "Repository ready at: $repoDir"

# ── 3. Apply the dark theme ──────────────────────────────────────────────────
Title "Applying dark theme"

# Create Helpers folder if it doesn't exist
$helpersDir = "$repoDir\src\ServiceBusExplorer\Helpers"
if (-not (Test-Path $helpersDir)) {
    New-Item -ItemType Directory -Path $helpersDir | Out-Null
    Info "Helpers\ folder created"
}

# Copy ThemeManager.cs
$themeSource = "$PSScriptRoot\ThemeManager.cs"
if (-not (Test-Path $themeSource)) {
    Fail "ThemeManager.cs not found in $PSScriptRoot`nPlace it in the same folder as this script."
}
Copy-Item $themeSource "$helpersDir\ThemeManager.cs" -Force
Ok "ThemeManager.cs copied to Helpers\"

# Locate the main .csproj
$csproj = Get-ChildItem "$repoDir\src" -Filter "ServiceBusExplorer.csproj" -Recurse |
    Where-Object { $_.FullName -notmatch "\\obj\\" } |
    Select-Object -First 1 -ExpandProperty FullName

if (-not $csproj) { Fail ".csproj not found in $repoDir\src" }
Info "Project: $csproj"

# Check if it's SDK-style (contains <Project Sdk=)
$csprojContent = Get-Content $csproj -Raw
$isSdkStyle = $csprojContent -match '<Project\s+Sdk='

if ($isSdkStyle) {
    # SDK-style: .cs files are included automatically by glob
    # No need to add to .csproj — just copying the file is enough
    Ok "SDK-style project: ThemeManager.cs will be included automatically"
} else {
    # Legacy project: needs to add <Compile> entry manually
    if ($csprojContent -notmatch "ThemeManager\.cs") {
        $newEntry = '    <Compile Include="Helpers\ThemeManager.cs" />'
        $firstHelperMatch = [regex]::Match(
            $csprojContent,
            '<Compile Include="Helpers\\[^"]+\.cs"\s*/>'
        )
        if ($firstHelperMatch.Success) {
            $insertAt = $firstHelperMatch.Index + $firstHelperMatch.Length
            $csprojContent = $csprojContent.Insert($insertAt, "`r`n$newEntry")
        } else {
            $idx = $csprojContent.IndexOf("</ItemGroup>")
            if ($idx -ge 0) {
                $csprojContent = $csprojContent.Insert($idx, "$newEntry`r`n  ")
            }
        }
        Set-Content $csproj $csprojContent -Encoding UTF8
        Ok "ThemeManager.cs added to .csproj (legacy)"
    } else {
        Ok "ThemeManager.cs already in .csproj"
    }
}

# Locate MainForm.cs
$mainFormPath = Get-ChildItem "$repoDir\src" -Filter "MainForm.cs" -Recurse |
    Where-Object { $_.FullName -notmatch "\\obj\\" } |
    Select-Object -First 1 -ExpandProperty FullName

if (-not $mainFormPath) {
    Warn "MainForm.cs not found — manual patching required."
} else {
    $mfc = Get-Content $mainFormPath -Raw

    # Add using if necessary
    if ($mfc -notmatch "using ServiceBusExplorer\.Helpers") {
        # Insert before the first "using " line found
        $firstUsing = [regex]::Match($mfc, "using\s+[\w\.]+;")
        if ($firstUsing.Success) {
            $mfc = $mfc.Insert($firstUsing.Index, "using ServiceBusExplorer.Helpers;`r`n")
        }
        Ok "using ServiceBusExplorer.Helpers added"
    }

    # Inject ThemeManager.Apply(this) after InitializeComponent()
    if ($mfc -notmatch "ThemeManager\.Apply") {
        $p2 = '(InitializeComponent\(\);)'
        $r2 = '$1' + "`r`n            ThemeManager.Apply(this);"
        $mfc = $mfc -replace $p2, $r2
        Ok "ThemeManager.Apply(this) injected in constructor"
    } else {
        Ok "ThemeManager.Apply already in MainForm.cs"
    }

    # ── Create MainFormThemeExtension.cs (more reliable than injecting here-strings) ──
    $extensionPath = "$repoDir\src\ServiceBusExplorer\Helpers\MainFormThemeExtension.cs"
    if (-not (Test-Path $extensionPath)) {
        $viewMenuVar = "viewToolStripMenuItem"

        # Discover actual View menu name in Designer
        $designerPath = Get-ChildItem "$repoDir\src" -Filter "MainForm.Designer.cs" -Recurse |
            Where-Object { $_.FullName -notmatch "\\obj\\" } |
            Select-Object -First 1 -ExpandProperty FullName
        if ($designerPath) {
            $designer = Get-Content $designerPath -Raw
            $menuMatches = [regex]::Matches($designer, 'this\.(\w+)\.Text\s*=\s*"([^"]+)"')
            foreach ($m in $menuMatches) {
                if ($m.Groups[2].Value -match "^&?View$") {
                    $viewMenuVar = $m.Groups[1].Value
                    Info "View menu found: $viewMenuVar"
                    break
                }
            }
        }

        $ext = @"
// MainFormThemeExtension.cs — generated by build-dark-theme.ps1
// Path: src/ServiceBusExplorer/Helpers/MainFormThemeExtension.cs
using System;
using System.Windows.Forms;
using ServiceBusExplorer.Forms;

namespace ServiceBusExplorer.Helpers
{
    internal static class MainFormThemeExtension
    {
        internal static void InitTheme(MainForm form, MenuStrip menuStrip)
        {
            ThemeManager.Apply(form);

            // Native dark title bar (Windows 10 1903+)
            form.HandleCreated += (s, e) =>
            {
                ThemeManager.ApplyTitleBar(form.Handle);
                ThemeManager.ThemeChanged += (_, __) => ThemeManager.ApplyTitleBar(form.Handle);
            };

            // Add Theme > Dark / Light submenu to View menu
            ToolStripMenuItem viewMenu = null;
            foreach (ToolStripItem item in menuStrip.Items)
            {
                if (item is ToolStripMenuItem mi &&
                    mi.Text.Replace("&", "").Equals("View", StringComparison.OrdinalIgnoreCase))
                {
                    viewMenu = mi;
                    break;
                }
            }
            if (viewMenu == null) return;

            var themeMenu = new ToolStripMenuItem("Theme");
            var darkItem  = new ToolStripMenuItem("Dark")  { CheckOnClick = false };
            var lightItem = new ToolStripMenuItem("Light") { CheckOnClick = false };

            Action updateChecks = () =>
            {
                darkItem.Checked  = ThemeManager.IsDark;
                lightItem.Checked = !ThemeManager.IsDark;
            };

            darkItem.Click  += (s, e) => { ThemeManager.SetTheme(Theme.Dark);  updateChecks(); };
            lightItem.Click += (s, e) => { ThemeManager.SetTheme(Theme.Light); updateChecks(); };

            themeMenu.DropDownItems.Add(darkItem);
            themeMenu.DropDownItems.Add(lightItem);
            viewMenu.DropDownItems.Add(new ToolStripSeparator());
            viewMenu.DropDownItems.Add(themeMenu);
            updateChecks();
        }
    }
}
"@
        Set-Content $extensionPath $ext -Encoding UTF8
        Ok "MainFormThemeExtension.cs created"
    } else {
        Ok "MainFormThemeExtension.cs already exists"
    }

    # Inject call in MainForm constructor — a simple line
    if ($mfc -notmatch "InitTheme") {
        # Add using
        if ($mfc -notmatch "using ServiceBusExplorer\.Helpers") {
            $firstUsing = [regex]::Match($mfc, "using\s+[\w\.]+;")
            if ($firstUsing.Success) {
                $mfc = $mfc.Insert($firstUsing.Index, "using ServiceBusExplorer.Helpers;`r`n")
            }
        }
        # Inject after InitializeComponent()
        $p = "(InitializeComponent\(\);)"
        $r = "`$1`r`n            MainFormThemeExtension.InitTheme(this, mainMenuStrip);"
        $mfc = $mfc -replace $p, $r
        Ok "MainFormThemeExtension.InitTheme injected in constructor"
    } else {
        Ok "InitTheme already in MainForm.cs"
    }

    Set-Content $mainFormPath $mfc -Encoding UTF8
}

# ── 3b. Apply theme to all secondary Forms ───────────────────────────
Title "Applying theme to secondary Forms"

$allForms = Get-ChildItem "$repoDir\src" -Filter "*.cs" -Recurse |
    Where-Object { $_.FullName -notmatch "\\obj\\" -and
                   $_.Name -notmatch "Designer\.cs$" -and
                   $_.Name -ne "MainForm.cs" -and
                   $_.Name -ne "ThemeManager.cs" }

$patchedCount = 0
foreach ($file in $allForms) {
    $src = Get-Content $file.FullName -Raw

    # Check if it's a Form or UserControl
    if ($src -notmatch ":\s*(Form|UserControl|ContainerControl)\b") { continue }
    # Skip if already has theme
    if ($src -match "ThemeManager\.Apply") { continue }
    # Skip if no InitializeComponent (not a Form with designer)
    if ($src -notmatch "InitializeComponent\(\)") { continue }

    # Add using if necessary
    if ($src -notmatch "using ServiceBusExplorer\.Helpers") {
        $firstUsing = [regex]::Match($src, "using\s+[\w\.]+;")
        if ($firstUsing.Success) {
            $src = $src.Insert($firstUsing.Index, "using ServiceBusExplorer.Helpers;`r`n")
        }
    }

    # Inject ThemeManager.Apply after InitializeComponent()
    $p2 = "(InitializeComponent\(\);)"
    $r2 = "`$1`r`n            ThemeManager.Apply(this);"
    $src = $src -replace $p2, $r2

    Set-Content $file.FullName $src -Encoding UTF8
    $patchedCount++
}
Ok "$patchedCount secondary forms with theme applied"

# ── 3c. Remove hardcoded colors in Designer.cs files ──────────────────────
Title "Removing hardcoded colors from Designer.cs"

$designers = Get-ChildItem "$repoDir\src" -Recurse |
    Where-Object { $_.Name -imatch "\.designer\.cs$" -and $_.FullName -notmatch "\\obj\\" }

$designerCount = 0
foreach ($file in $designers) {
    $src = Get-Content $file.FullName -Raw
    $original = $src

    # Skip AboutForm — it has decorative BackgroundImage, don't change BackColor
    if ($file.Name -eq "AboutForm.Designer.cs") { continue }

    # ── Replace hardcoded BackColor ──────────────────────────────────────
    # Covers: this.xxx.BackColor and this.BackColor (the form itself)
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.BackColor\s*=\s*)System\.Drawing\.Color\.FromArgb\([^;]+\);',
        '$1System.Drawing.Color.FromArgb(33, 33, 33);')

    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.BackColor\s*=\s*)System\.Drawing\.SystemColors\.\w+;',
        '$1System.Drawing.Color.FromArgb(33, 33, 33);')

    # ── Remove hardcoded ForeColor — let ThemeManager control at runtime ─
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.ForeColor\s*=\s*)System\.Drawing\.Color\.FromArgb\([^;]+\);',
        '$1System.Drawing.SystemColors.ControlText;')

    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.ForeColor\s*=\s*)System\.Drawing\.Color\.(White|Black|Gray|Silver|DarkGray|LightGray)\s*;',
        '$1System.Drawing.SystemColors.ControlText;')

    # SystemColors.Window as ForeColor becomes white in light theme — fix to ControlText
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.ForeColor\s*=\s*)System\.Drawing\.SystemColors\.Window;',
        '$1System.Drawing.SystemColors.ControlText;')

    # ── HeaderPanel: HeaderColor1, HeaderColor2 ─────────────────────────────────
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.HeaderColor1\s*=\s*)System\.Drawing\.Color\.[^;]+;',
        '$1System.Drawing.Color.FromArgb(42, 42, 42);')
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.HeaderColor2\s*=\s*)System\.Drawing\.Color\.[^;]+;',
        '$1System.Drawing.Color.FromArgb(33, 33, 33);')
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.HeaderForeColor\s*=\s*)System\.Drawing\.Color\.[^;]+;',
        '$1System.Drawing.Color.FromArgb(236, 236, 236);')

    # ── Grouper: custom color properties (BackgroundColor, BorderColor etc) ──
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.BackgroundColor\s*=\s*)System\.Drawing\.Color\.[^;]+;',
        '$1System.Drawing.Color.FromArgb(33, 33, 33);')
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.BackgroundGradientColor\s*=\s*)System\.Drawing\.Color\.[^;]+;',
        '$1System.Drawing.Color.FromArgb(33, 33, 33);')
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.CustomGroupBoxColor\s*=\s*)System\.Drawing\.Color\.[^;]+;',
        '$1System.Drawing.Color.FromArgb(42, 42, 42);')
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.BorderColor\s*=\s*)System\.Drawing\.Color\.[^;]+;',
        '$1System.Drawing.Color.FromArgb(72, 72, 72);')

    # ── FlatAppearance.MouseDownBackColor / MouseOverBackColor ───────────────
    $src = [regex]::Replace($src,
        '(this\.\w+\.FlatAppearance\.(MouseDownBackColor|MouseOverBackColor)\s*=\s*)System\.Drawing\.Color\.FromArgb\([^;]+\);',
        '$1System.Drawing.Color.FromArgb(28, 151, 234);')

    # ── FlatAppearance.BorderColor ───────────────────────────────────────────
    $src = [regex]::Replace($src,
        '(this\.\w+\.FlatAppearance\.BorderColor\s*=\s*)System\.Drawing\.Color\.FromArgb\([^;]+\);',
        '$1System.Drawing.Color.FromArgb(0, 122, 204);')

    # ── UseVisualStyleBackColor = false → true (let Apply control it) ────
    # Don't touch — Apply already overrides the color later

    if ($src -ne $original) {
        Set-Content $file.FullName $src -Encoding UTF8
        $designerCount++
    }
}
Ok "$designerCount Designer.cs files with colors fixed"

# ── 4. Restore ────────────────────────────────────────────────────────────────
Title "Restoring dependencies (dotnet restore)"

$sln = Get-ChildItem "$repoDir\src" -Filter "*.sln" | Select-Object -First 1 -ExpandProperty FullName
if (-not $sln) { Fail ".sln not found in $repoDir\src" }
Info "Solution: $sln"

dotnet restore $sln
if ($LASTEXITCODE -ne 0) { Fail "dotnet restore failed" }
Ok "Dependencies restored"

# ── 5. Build ──────────────────────────────────────────────────────────────────
Title "Compiling (Release)"

# Pass version 6.1.3 explicitly to avoid the "new version available" warning
# The app compares its own AssemblyVersion with the latest GitHub release
$buildVersion = "6.1.3"
dotnet build $sln --configuration Release --no-restore /nologo /p:Version=$buildVersion /p:AssemblyVersion="$buildVersion.0" /p:FileVersion="$buildVersion.0" /p:InformationalVersion="$buildVersion-dark-theme"
if ($LASTEXITCODE -ne 0) { Fail "Build failed" }
Ok "Build completed (v$buildVersion)"

# ── 6. Package ──────────────────────────────────────────────────────────────
Title "Packaging artifacts"

$exePath = Get-ChildItem "$repoDir\src" -Filter "ServiceBusExplorer.exe" -Recurse |
    Where-Object { $_.FullName -match "Release" -and $_.FullName -notmatch "\\obj\\" } |
    Select-Object -First 1 -ExpandProperty FullName

if (-not $exePath) { Fail "ServiceBusExplorer.exe not found in build output" }
$buildOutput = Split-Path $exePath -Parent
Info "Output: $buildOutput"

if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

$zipPath = "$OutputDir\ServiceBusExplorer-$Version.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }

Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::CreateFromDirectory($buildOutput, $zipPath)
Ok "ZIP created: $zipPath"

# ── 7. Summary ─────────────────────────────────────────────────────────────────
Title "Done!"
Write-Host ""
Write-Host "  Executable : $exePath" -ForegroundColor White
Write-Host "  ZIP        : $zipPath" -ForegroundColor White
Write-Host ""
Write-Host "  Run:" -ForegroundColor Gray
Write-Host "  & '$exePath'" -ForegroundColor DarkGray
Write-Host ""