param(
    [string]$RepositoryRoot = $PSScriptRoot
)

Set-StrictMode -Version 3
$ErrorActionPreference = 'Stop'

# The build target reports an unavailable baseline instead of guessing a version.
if (-not (Get-Command git -CommandType Application -ErrorAction SilentlyContinue) -or
    -not (Test-Path (Join-Path $RepositoryRoot '.git'))) {
    return
}

$isShallow = & git -C $RepositoryRoot rev-parse --is-shallow-repository
if ($LASTEXITCODE -ne 0) {
    throw 'Could not inspect repository history for the upstream release baseline.'
}
if ($isShallow -eq 'true') {
    return
}

$tags = & git -C $RepositoryRoot for-each-ref --merged=HEAD '--format=%(refname:strip=2)' refs/upstream-release-tags
if ($LASTEXITCODE -ne 0) {
    throw 'Could not read upstream release tags.'
}

$versions = foreach ($tag in $tags) {
    if ($tag -match '^v?((0|[1-9][0-9]*)\.(0|[1-9][0-9]*)\.(0|[1-9][0-9]*))$') {
        # Avoid [ref] casts, which are blocked in ConstrainedLanguage mode.
        $version = $Matches[1] -as [Version]
        if ($null -ne $version) {
            $version
        }
    }
}

$latest = $versions | Sort-Object -Descending | Select-Object -First 1
if ($null -ne $latest) {
    $latest.ToString(3)
}
