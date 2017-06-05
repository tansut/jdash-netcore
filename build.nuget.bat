rmdir nupkgs /s /q  

cd JDash.NetCore.Api
dotnet pack --output ../nupkgs
cd ../JDash.NetCore.Models
dotnet pack --output ../nupkgs  
cd ../JDash.NetCore.Provider.MsSQL
dotnet pack --output ../nupkgs 
cd ../JDash.NetCore.Provider.MySQL
dotnet pack --output ../nupkgs 

cd ../util

@echo off
nuget.exe setApiKey [19aea128-8c07-467f-8b53-f50128c571b1] -source https://www.nuget.org
@echo on
for %%a in ("..\nupkgs\*symbols.nupkg") do (
	del %%a
)
for %%a in ("..\nupkgs\*.nupkg") do (
	nuget.exe push %%a 19aea128-8c07-467f-8b53-f50128c571b1 -Source https://www.nuget.org/api/v2/package
)
pause