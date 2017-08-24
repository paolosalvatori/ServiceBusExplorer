param(
    [Parameter(Mandatory=$true, HelpMessage="Location to output the result to")]
    [string] $OutputFilePath
)

# Get all files
$files = Get-ChildItem "$($env:APPVEYOR_BUILD_FOLDER)\src\ServiceBusExplorer\bin\Release" -File

# Create a string builder to append all checksums
$verificationBuilder = New-Object System.Text.StringBuilder

# Loop all files to hash and append to string builder
foreach ($file in $files) {
    $hashResult = Get-FileHash $file -Algorithm MD5
    $verificationBuilder.AppendLine("- $($file.Name) - $($hashResult.Hash)")

    Write-Host "Processed $($file.Name)"
}

# Store output in VERIFICATION file
New-Item -Path $OutputFilePath -Value $verificationBuilder.ToString() -Force

Write-Host "Wrote results to $($OutputFilePath)"