param(
[string] $configuration = "Release"
    )

if(Test-Path "..\AutoUpdate.Executor\publish") 
{
    Remove-Item "..\AutoUpdate.Executor\publish" -Force -Recurse
}

if(Test-Path "..\ExecutorWin64.zip") 
{
    Remove-Item "..\ExecutorWin64.zip" -Force 
}

if(Test-Path "..\ExecutorWin86.zip") 
{
    Remove-Item "..\ExecutorWin86.zip" -Force 
}

dotnet publish "..\AutoUpdate.Executor\AutoUpdate.Executor.csproj" -c $configuration -r win-x64 -o "publish/ExecutorWin64" --no-restore
Compress-Archive "..\AutoUpdate.Executor\publish\ExecutorWin64" -DestinationPath "..\ExecutorWin64.zip"

dotnet publish "..\AutoUpdate.Executor\AutoUpdate.Executor.csproj" -c $configuration -r win-x86 -o "publish/ExecutorWin86" --no-restore
Compress-Archive "..\AutoUpdate.Executor\publish\ExecutorWin86" -DestinationPath "..\ExecutorWin86.zip"