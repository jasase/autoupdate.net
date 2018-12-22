param(
[string] $configuration = "Release"
    )

if(Test-Path ".\publish") 
{
    Remove-Item ".\publish" -Force -Recurse
}

if(Test-Path "..\ExecutorWin64.zip") 
{
    Remove-Item "..\ExecutorWin64.zip" -Force 
}

if(Test-Path "..\ExecutorWin86.zip") 
{
    Remove-Item "..\ExecutorWin86.zip" -Force 
}

dotnet publish ".\AutoUpdate.Executor.csproj" -c $configuration -r win-x64 -o "publish/ExecutorWin64" --no-build --no-restore
Compress-Archive ".\publish\ExecutorWin64" -DestinationPath "..\ExecutorWin64.zip"

dotnet publish ".\AutoUpdate.Executor.csproj" -c $configuration -r win-x86 -o "publish/ExecutorWin86" --no-build --no-restore
Compress-Archive ".\publish\ExecutorWin86" -DestinationPath "..\ExecutorWin86.zip"