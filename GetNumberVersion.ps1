# Verify it is a proper version
param(
    [Parameter(Mandatory=$false)]
	[string]
    $Version
)

Set-StrictMode -Version 3

if ($Version -match "^\d+\.\d+\.\d+$") {
	return $Version
}

if (-not ($Version -match "^\d+\.\d+\.\d+-.{1,12}$")) {
	throw "The version must start with three numbers separated by dots. This may be " + `
		"followed by a '-' and a word. That word may not be longer than 12 characters. For instance, 4.3.22-beta."
}

$parts = $Version -split "-"
return $parts[0]
