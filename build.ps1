$appDir = "Source\CoreLeaf"
$testDir = "Source\CoreLeaf.Tests"

dotnet clean $appDir
dotnet restore $appDir
dotnet build $appDir

dotnet clean $testDir
dotnet restore $testDir
dotnet build $testDir
dotnet test $testDir\CoreLeaf.Tests.csproj
