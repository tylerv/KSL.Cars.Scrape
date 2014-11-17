param ([string]$source = 'F:\Users\Tyler\Documents\Visual Studio 2012\Projects\KSL.Cars.Scrape\KSL.Cars.App\bin\Release\KSL.Cars.App.exe', [string]$destination = 'F:\Voorheis.info Website\files\KSL.Cars.App.exe', [string]$updateXML = 'F:\Voorheis.info Website\files\update.xml', [string]$settingsFileToDelete = 'F:\Voorheis.info Website\files\KSL.Cars.App.exe.config')

[xml]$updateFile = Get-Content $updateXML

[string]$version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($source).FileVersion
[string]$date = Get-Date -format d
#[string]$comment = read-host 'Enter a description for this release: '

$updateFile.updates."KSL.Cars.App".version.'#text' = $version
$updateFile.updates."KSL.Cars.App".date.'#text' = $date
#$updateFile.updates."KSL.Cars.App".comment.'#text' = $comment

$updateFile.Save($updateXML)

Copy-Item $source $destination -force

If (Test-Path $settingsFileToDelete) {Remove-Item $settingsFileToDelete}

& 'C:\Program Files (x86)\2BrightSparks\SyncBackFree\SyncBackFree.exe' 'Upload Website'