Push-Location $PSScriptRoot
Import-Module $PSScriptRoot\Build\CoreLeaf.Build.psd1 -Force

$artifactsPath = "$PSScriptRoot\artifacts"
$packagesPath = "$artifactsPath\packages"

$appPath = "Source\CoreLeaf"
$unitTestPath = "Source\CoreLeaf.Tests"


Install-DotNetCli -Version Latest

#clean up
if(Test-Path $artifactsPath)
{
    Remove-Item $artifactsPath -Force -Recurse
}

# Package restore
dotnet restore $appPath
dotnet restore $unitTestPath

# Build
dotnet build $appPath
dotnet build $unitTestPath

# Test
dotnet test $unitTestPath\CoreLeaf.Tests.csproj
