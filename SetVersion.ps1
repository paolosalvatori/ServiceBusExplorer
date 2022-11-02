param(
	[Parameter(Mandatory)]
	[string]
	$FileName,

	[Parameter(Mandatory)]
	[string]
	$PropertyName,

	[Parameter(Mandatory, 
		ParameterSetName = 'Version')]
	[Version]
	$Version,

	[Parameter(Mandatory, 
		ParameterSetName = 'VersionString')]
	[string]
	$VersionString
)

Set-StrictMode -Version 2

$pattern = "^\[assembly: $PropertyName\(""(.*)""\)\]$"
$found = $false

if ($Version) {
	$VersionString = $Version
}

$content = (Get-Content $fileName -Encoding UTF8) | ForEach-Object {
	if ($_ -match $pattern) {
  	   	"[assembly: $PropertyName(""{0}"")]" -f $VersionString
		$found = $true
	} 
	else {
		$_
	}
} 

if (-not $found) {
	$content += "[assembly: $PropertyName(""{0}"")]" -f $VersionString
}

$content | Set-Content $fileName -Encoding UTF8