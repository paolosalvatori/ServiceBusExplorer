param(
    [Parameter(Mandatory=$true, HelpMessage="Released version of ServiceBusExplorer")]
    [string] $Version,
    [Parameter(Mandatory=$true, HelpMessage="Path to the ServiceBusExplorer exe")]
    [string] $ExePath,
    [Parameter(Mandatory=$true, HelpMessage="Location of the VERIFICATION.txt template")]
    [string] $TemplateFilePath,
    [Parameter(Mandatory=$true, HelpMessage="Location to output the result to")]
    [string] $OutputFilePath
)

Write-Output "Creating VERIFICATION.txt based on template '$($TemplateFilePath)' for '$($ExePath)'"

# Get content of template
$rawTemplate = Get-Content $TemplateFilePath | Out-String
Write-Output "VERIFICATION.txt template: $($rawTemplate)"

# Hash ServiceBusExplorer.exe
$hashResult = Get-FileHash $ExePath -Algorithm MD5
Write-Output "Created hash '$($hashResult.Hash)'"

# Update template with checksum
$rawTemplate = $rawTemplate.Replace('%CHECKSUM%', $hashResult.Hash)
Write-Output "Replaced checksum in template with '$($hashResult.Hash)'"

# Updating template with version
$rawTemplate = $rawTemplate.Replace('%VERSION%', $Version)
Write-Output "Replaced version in template with '$($Version)'"

# Store output in VERIFICATION file
New-Item -Path $OutputFilePath -Value $rawTemplate -Force
Write-Host "Wrote results to $($OutputFilePath)"