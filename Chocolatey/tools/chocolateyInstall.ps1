$name   = "AzureServiceBusExplorer"
$url    = "https://code.msdn.microsoft.com/windowsazure/Service-Bus-Explorer-f2abca5a/file/134519/2/Service%20Bus%20Explorer.zip"
$unzipLocation = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"

Install-ChocolateyZipPackage $name $url $unzipLocation
