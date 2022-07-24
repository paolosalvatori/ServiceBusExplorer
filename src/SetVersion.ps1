param(
    [Parameter(Mandatory)]
	[string]
    $FileName,

    [Parameter(Mandatory)]
    [string]
    $PropertyName,

    [Parameter(Mandatory)]
	[Version]
    $Version
)

Set-StrictMode -Version 2

$pattern = "^\[assembly: $PropertyName\(""(.*)""\)\]$"
$found = $false

$content = (Get-Content $fileName -Encoding UTF8) | ForEach-Object {
	if ($_ -match $pattern) 
	{
		"[assembly: $PropertyName(""{0}"")]" -f $Version
        $found = $true
	} 
	else 
	{
		$_
	}
} 

if (-not $found) {
  $content += "[assembly: $PropertyName(""{0}"")]" -f $Version
}

$content | Set-Content $fileName -Encoding UTF8