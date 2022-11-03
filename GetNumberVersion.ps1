# Verify it is a proper version
param(
    [Parameter(Mandatory=$false)]
	[string]
    $Version = '0.0.0'
)

Set-StrictMode -Version 3

if ($Version -match "^\d+\.\d+\.\d+$") {
	return $Version
}

# RegEx from https://semver.org/
[string]$semanticRegEx = '^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$'

if (-not ($Version -match $semanticRegEx)) {
	throw "The version must follow semantic versioning, see https://semver.org/." + `
		" For instance 4.3.22-beta."
}

$parts = $Version -split {$_ -eq '-' -or $_ -eq '+'}

return $parts[0]
