<#
.SYNOPSIS
    Bumps <Version> in the main ServiceBusExplorer.csproj.

.DESCRIPTION
    Finds the main .csproj (matching repo folder name), replaces <Version> with the
    supplied value. Never touches <AssemblyVersion>.

.PARAMETER Version
    New version string, e.g. 4.2.1

.EXAMPLE
    .\tools\version-bump.ps1 -Version 4.2.1
#>
param(
    [Parameter(Mandatory)]
    [string] $Version
)

Set-StrictMode -Version 2
$ErrorActionPreference = 'Stop'

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot '..')
$repoName = Split-Path $repoRoot -Leaf

$csproj = Get-ChildItem -Path $repoRoot -Recurse -Filter '*.csproj' |
    Where-Object { $_.Name -notmatch '\.Tests\.csproj$' -and $_.FullName -notmatch '\\(obj|bin)\\' } |
    Sort-Object { if ($_.BaseName -eq $repoName) { 0 } else { 1 } } |
    Select-Object -First 1

if (-not $csproj) {
    Write-Error "No .csproj found under $repoRoot"
}

Write-Host "Bumping version in: $($csproj.FullName | Resolve-Path -Relative)"

$content = Get-Content $csproj.FullName -Raw -Encoding UTF8

if ($content -match '<Version>[^<]*</Version>') {
    $content = $content -replace '<Version>[^<]*</Version>', "<Version>$Version</Version>"
    Write-Host "<Version> -> $Version"
} elseif ($content -match '<FileVersion>[^<]*</FileVersion>') {
    $content = $content -replace '<FileVersion>[^<]*</FileVersion>', "<FileVersion>$Version</FileVersion>"
    Write-Host "<FileVersion> -> $Version"
} else {
    $content = $content -replace '(<AssemblyVersion>[^<]*</AssemblyVersion>)', "`$1`n`t`t<Version>$Version</Version>"
    Write-Host "<Version> inserted: $Version"
}

Set-Content -Path $csproj.FullName -Value $content -Encoding UTF8 -NoNewline
Write-Host 'Done.'
