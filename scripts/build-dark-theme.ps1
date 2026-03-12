# build-dark-theme.ps1
# ─────────────────────────────────────────────────────────────────────────────
# Build do ServiceBusExplorer com Tema Escuro
#
# Requisitos:
#   - Git
#   - .NET SDK 8+ (dotnet CLI)  →  https://dotnet.microsoft.com/download
#   - .NET Framework 4.7.2 Developer Pack (para compilar o target net472)
#       https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472
#     (normalmente já vem junto com Visual Studio 2022)
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

Title "ServiceBusExplorer — Build com Tema Escuro"

# ── 1. Verificar pré-requisitos ───────────────────────────────────────────────
Title "Verificando pre-requisitos"

# Git
if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Fail "Git nao encontrado. Instale em https://git-scm.com"
}
Ok "Git: $(git --version)"

# dotnet CLI — obrigatorio para SDK-style projects
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Fail @"
.NET SDK nao encontrado!

Instale o .NET SDK 8 (ou superior) em:
  https://dotnet.microsoft.com/download

Apos instalar, feche e reabra o PowerShell.
"@
}
$dotnetVersion = dotnet --version
Ok ".NET SDK: $dotnetVersion"

# Verificar se o .NET Framework 4.7.2 targeting pack esta instalado
# (necessario para compilar o target net472)
$fx472 = "HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"
$fxRelease = 0
if (Test-Path $fx472) {
    $fxRelease = (Get-ItemProperty $fx472 -ErrorAction SilentlyContinue).Release
}
# Release 461808+ = .NET Framework 4.7.2
if ($fxRelease -lt 461808) {
    Warn ".NET Framework 4.7.2 pode nao estar instalado (release=$fxRelease)."
    Warn "Se o build falhar, instale o Developer Pack em:"
    Warn "https://dotnet.microsoft.com/en-us/download/dotnet-framework/net472"
} else {
    Ok ".NET Framework 4.7.2+ detectado (release=$fxRelease)"
}

# ── 2. Clonar / atualizar o repositório ──────────────────────────────────────
Title "Clonando repositorio"

$repoDir = "$PSScriptRoot\ServiceBusExplorer"
if (Test-Path $repoDir) {
    Info "Repositorio ja existe, atualizando para main..."
    Push-Location $repoDir
    git fetch origin
    git reset --hard origin/main
    Pop-Location
} else {
    Info "Clonando $RepoUrl ..."
    git clone $RepoUrl $repoDir
}
Ok "Repositorio pronto em: $repoDir"

# ── 3. Aplicar o tema escuro ──────────────────────────────────────────────────
Title "Aplicando tema escuro"

# Criar pasta Helpers se nao existir
$helpersDir = "$repoDir\src\ServiceBusExplorer\Helpers"
if (-not (Test-Path $helpersDir)) {
    New-Item -ItemType Directory -Path $helpersDir | Out-Null
    Info "Pasta Helpers\ criada"
}

# Copiar ThemeManager.cs
$themeSource = "$PSScriptRoot\ThemeManager.cs"
if (-not (Test-Path $themeSource)) {
    Fail "ThemeManager.cs nao encontrado em $PSScriptRoot`nColoque-o na mesma pasta que este script."
}
Copy-Item $themeSource "$helpersDir\ThemeManager.cs" -Force
Ok "ThemeManager.cs copiado para Helpers\"

# Localizar o .csproj principal
$csproj = Get-ChildItem "$repoDir\src" -Filter "ServiceBusExplorer.csproj" -Recurse |
    Where-Object { $_.FullName -notmatch "\\obj\\" } |
    Select-Object -First 1 -ExpandProperty FullName

if (-not $csproj) { Fail ".csproj nao encontrado em $repoDir\src" }
Info "Projeto: $csproj"

# Verificar se e SDK-style (contem <Project Sdk=)
$csprojContent = Get-Content $csproj -Raw
$isSdkStyle = $csprojContent -match '<Project\s+Sdk='

if ($isSdkStyle) {
    # SDK-style: arquivos .cs sao incluidos automaticamente por glob
    # Nao precisamos adicionar ao .csproj — so copiar o arquivo ja basta
    Ok "Projeto SDK-style: ThemeManager.cs sera incluido automaticamente"
} else {
    # Projeto legado: precisa adicionar entrada <Compile> manualmente
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
        Ok "ThemeManager.cs adicionado ao .csproj (legado)"
    } else {
        Ok "ThemeManager.cs ja estava no .csproj"
    }
}

# Localizar MainForm.cs
$mainFormPath = Get-ChildItem "$repoDir\src" -Filter "MainForm.cs" -Recurse |
    Where-Object { $_.FullName -notmatch "\\obj\\" } |
    Select-Object -First 1 -ExpandProperty FullName

if (-not $mainFormPath) {
    Warn "MainForm.cs nao encontrado — patch manual necessario."
} else {
    $mfc = Get-Content $mainFormPath -Raw

    # Adicionar using se necessario
    if ($mfc -notmatch "using ServiceBusExplorer\.Helpers") {
        # Inserir antes da primeira linha "using " que encontrar
        $firstUsing = [regex]::Match($mfc, "using\s+[\w\.]+;")
        if ($firstUsing.Success) {
            $mfc = $mfc.Insert($firstUsing.Index, "using ServiceBusExplorer.Helpers;`r`n")
        }
        Ok "using ServiceBusExplorer.Helpers adicionado"
    }

    # Injetar ThemeManager.Apply(this) apos InitializeComponent()
    if ($mfc -notmatch "ThemeManager\.Apply") {
        $p2 = '(InitializeComponent\(\);)'
        $r2 = '$1' + "`r`n            ThemeManager.Apply(this);"
        $mfc = $mfc -replace $p2, $r2
        Ok "ThemeManager.Apply(this) injetado no construtor"
    } else {
        Ok "ThemeManager.Apply ja estava no MainForm.cs"
    }

    # ── Criar MainFormThemeExtension.cs (mais confiavel que injetar here-strings) ──
    $extensionPath = "$repoDir\src\ServiceBusExplorer\Helpers\MainFormThemeExtension.cs"
    if (-not (Test-Path $extensionPath)) {
        $viewMenuVar = "viewToolStripMenuItem"

        # Descobrir nome real do menu View no Designer
        $designerPath = Get-ChildItem "$repoDir\src" -Filter "MainForm.Designer.cs" -Recurse |
            Where-Object { $_.FullName -notmatch "\\obj\\" } |
            Select-Object -First 1 -ExpandProperty FullName
        if ($designerPath) {
            $designer = Get-Content $designerPath -Raw
            $menuMatches = [regex]::Matches($designer, 'this\.(\w+)\.Text\s*=\s*"([^"]+)"')
            foreach ($m in $menuMatches) {
                if ($m.Groups[2].Value -match "^&?View$") {
                    $viewMenuVar = $m.Groups[1].Value
                    Info "Menu View encontrado: $viewMenuVar"
                    break
                }
            }
        }

        $ext = @"
// MainFormThemeExtension.cs — gerado pelo build-dark-theme.ps1
// Caminho: src/ServiceBusExplorer/Helpers/MainFormThemeExtension.cs
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

            // Titlebar nativa escura (Windows 10 1903+)
            form.HandleCreated += (s, e) =>
            {
                ThemeManager.ApplyTitleBar(form.Handle);
                ThemeManager.ThemeChanged += (_, __) => ThemeManager.ApplyTitleBar(form.Handle);
            };

            // Adicionar submenu Theme > Dark / Light no menu View
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
        Ok "MainFormThemeExtension.cs criado"
    } else {
        Ok "MainFormThemeExtension.cs ja existe"
    }

    # Injetar chamada no construtor do MainForm — uma linha simples
    if ($mfc -notmatch "InitTheme") {
        # Adicionar using
        if ($mfc -notmatch "using ServiceBusExplorer\.Helpers") {
            $firstUsing = [regex]::Match($mfc, "using\s+[\w\.]+;")
            if ($firstUsing.Success) {
                $mfc = $mfc.Insert($firstUsing.Index, "using ServiceBusExplorer.Helpers;`r`n")
            }
        }
        # Injetar apos InitializeComponent()
        $p = "(InitializeComponent\(\);)"
        $r = "`$1`r`n            MainFormThemeExtension.InitTheme(this, mainMenuStrip);"
        $mfc = $mfc -replace $p, $r
        Ok "MainFormThemeExtension.InitTheme injetado no construtor"
    } else {
        Ok "InitTheme ja estava no MainForm.cs"
    }

    Set-Content $mainFormPath $mfc -Encoding UTF8
}

# ── 3b. Aplicar tema em todos os Forms secundarios ───────────────────────────
Title "Aplicando tema nos Forms secundarios"

$allForms = Get-ChildItem "$repoDir\src" -Filter "*.cs" -Recurse |
    Where-Object { $_.FullName -notmatch "\\obj\\" -and
                   $_.Name -notmatch "Designer\.cs$" -and
                   $_.Name -ne "MainForm.cs" -and
                   $_.Name -ne "ThemeManager.cs" }

$patchedCount = 0
foreach ($file in $allForms) {
    $src = Get-Content $file.FullName -Raw

    # Verificar se e um Form ou UserControl
    if ($src -notmatch ":\s*(Form|UserControl|ContainerControl)\b") { continue }
    # Pular se ja tem o tema
    if ($src -match "ThemeManager\.Apply") { continue }
    # Pular se nao tem InitializeComponent (nao e um Form com designer)
    if ($src -notmatch "InitializeComponent\(\)") { continue }

    # Adicionar using se necessario
    if ($src -notmatch "using ServiceBusExplorer\.Helpers") {
        $firstUsing = [regex]::Match($src, "using\s+[\w\.]+;")
        if ($firstUsing.Success) {
            $src = $src.Insert($firstUsing.Index, "using ServiceBusExplorer.Helpers;`r`n")
        }
    }

    # Injetar ThemeManager.Apply apos InitializeComponent()
    $p2 = "(InitializeComponent\(\);)"
    $r2 = "`$1`r`n            ThemeManager.Apply(this);"
    $src = $src -replace $p2, $r2

    Set-Content $file.FullName $src -Encoding UTF8
    $patchedCount++
}
Ok "$patchedCount forms secundarios com tema aplicado"

# ── 3c. Limpar cores hardcoded nos arquivos Designer.cs ──────────────────────
Title "Removendo cores hardcoded dos Designer.cs"

$designers = Get-ChildItem "$repoDir\src" -Recurse |
    Where-Object { $_.Name -imatch "\.designer\.cs$" -and $_.FullName -notmatch "\\obj\\" }

$designerCount = 0
foreach ($file in $designers) {
    $src = Get-Content $file.FullName -Raw
    $original = $src

    # Pular AboutForm — tem BackgroundImage decorativa, nao alterar BackColor
    if ($file.Name -eq "AboutForm.Designer.cs") { continue }

    # ── Substituir BackColor hardcoded ──────────────────────────────────────
    # Cobre: this.xxx.BackColor e this.BackColor (o form em si)
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.BackColor\s*=\s*)System\.Drawing\.Color\.FromArgb\([^;]+\);',
        '$1System.Drawing.Color.FromArgb(33, 33, 33);')

    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.BackColor\s*=\s*)System\.Drawing\.SystemColors\.\w+;',
        '$1System.Drawing.Color.FromArgb(33, 33, 33);')

    # ── Remover ForeColor hardcoded — deixar ThemeManager controlar em runtime ─
    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.ForeColor\s*=\s*)System\.Drawing\.Color\.FromArgb\([^;]+\);',
        '$1System.Drawing.SystemColors.ControlText;')

    $src = [regex]::Replace($src,
        '(this(?:\.\w+)?\.ForeColor\s*=\s*)System\.Drawing\.Color\.(White|Black|Gray|Silver|DarkGray|LightGray)\s*;',
        '$1System.Drawing.SystemColors.ControlText;')

    # SystemColors.Window como ForeColor fica branco no light — corrigir para ControlText
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

    # ── Grouper: propriedades de cor customizadas (BackgroundColor, BorderColor etc) ──
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

    # ── UseVisualStyleBackColor = false → true (deixar o Apply controlar) ────
    # Nao mexer — o Apply ja sobrescreve a cor depois

    if ($src -ne $original) {
        Set-Content $file.FullName $src -Encoding UTF8
        $designerCount++
    }
}
Ok "$designerCount arquivos Designer.cs com cores corrigidas"

# ── 4. Restore ────────────────────────────────────────────────────────────────
Title "Restaurando dependencias (dotnet restore)"

$sln = Get-ChildItem "$repoDir\src" -Filter "*.sln" | Select-Object -First 1 -ExpandProperty FullName
if (-not $sln) { Fail ".sln nao encontrado em $repoDir\src" }
Info "Solucao: $sln"

dotnet restore $sln
if ($LASTEXITCODE -ne 0) { Fail "Falha no dotnet restore" }
Ok "Dependencias restauradas"

# ── 5. Build ──────────────────────────────────────────────────────────────────
Title "Compilando (Release)"

# Passar a versao 6.1.3 explicitamente para evitar o aviso "new version available"
# O app compara o proprio AssemblyVersion com o ultimo release do GitHub
$buildVersion = "6.1.3"
dotnet build $sln --configuration Release --no-restore /nologo /p:Version=$buildVersion /p:AssemblyVersion="$buildVersion.0" /p:FileVersion="$buildVersion.0" /p:InformationalVersion="$buildVersion-dark-theme"
if ($LASTEXITCODE -ne 0) { Fail "Falha no build" }
Ok "Build concluido (v$buildVersion)"

# ── 6. Empacotar ──────────────────────────────────────────────────────────────
Title "Empacotando artefatos"

$exePath = Get-ChildItem "$repoDir\src" -Filter "ServiceBusExplorer.exe" -Recurse |
    Where-Object { $_.FullName -match "Release" -and $_.FullName -notmatch "\\obj\\" } |
    Select-Object -First 1 -ExpandProperty FullName

if (-not $exePath) { Fail "ServiceBusExplorer.exe nao encontrado no output do build" }
$buildOutput = Split-Path $exePath -Parent
Info "Output: $buildOutput"

if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

$zipPath = "$OutputDir\ServiceBusExplorer-$Version.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath -Force }

Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::CreateFromDirectory($buildOutput, $zipPath)
Ok "ZIP criado: $zipPath"

# ── 7. Resumo ─────────────────────────────────────────────────────────────────
Title "Pronto!"
Write-Host ""
Write-Host "  Executavel : $exePath" -ForegroundColor White
Write-Host "  ZIP        : $zipPath" -ForegroundColor White
Write-Host ""
Write-Host "  Executar:" -ForegroundColor Gray
Write-Host "  & '$exePath'" -ForegroundColor DarkGray
Write-Host ""