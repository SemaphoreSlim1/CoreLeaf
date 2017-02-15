<#
 .SYNOPSIS
  Runs the dotnet CLI install script from GitHub to install a project-local
  copy of the CLI.
#>
function Install-DotNetCli
{
  [CmdletBinding()]
  Param(
    [string]
    $Version = "Latest"
  )

  $callerPath = Split-Path $MyInvocation.PSCommandPath
  $installDir = Join-Path -Path $callerPath -ChildPath ".dotnet\cli"
  if (!(Test-Path $installDir))
  {
    New-Item -ItemType Directory -Path "$installDir" | Out-Null
  }

  # Download the dotnet CLI install script
  if (!(Test-Path .\dotnet\install.ps1))
  {
    Invoke-WebRequest "https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0/scripts/obtain/dotnet-install.ps1" -OutFile ".\.dotnet\dotnet-install.ps1"
  }

  # Run the dotnet CLI install
  & .\.dotnet\dotnet-install.ps1 -InstallDir "$installDir" -Version $Version

  # Add the dotnet folder path to the process.
  $env:PATH = "$installDir;$env:PATH"
}