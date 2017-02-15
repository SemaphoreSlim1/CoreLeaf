Push-Location $PSScriptRoot
Import-Module $PSScriptRoot\Build\CoreLeaf.Build.psd1 -Force

$artifactsPath = "$PSScriptRoot\artifacts"
$packagesPath = "$artifactsPath\packages"

$appDir = "Source\CoreLeaf"
$testDir = "Source\CoreLeaf.Tests"


Install-DotNetCli -Version Latest

#clean up
if(Test-Path $artifactsPath)
{
    Remove-Item $artifactsPath -Force -Recurse
}

# Package restore
Get-DotNetProjectDirectory -RootPath $PSScriptRoot | Restore-DependencyPackages

# Build
Get-DotNetProjectDirectory -RootPath $PSScriptRoot | Invoke-DotNetBuild

# Test
Get-DotNetProjectDirectory -RootPath $testDir  | Invoke-Test
