rm -rf nupkgs/*  

cd JDash.NetCore.Api
dotnet pack --output ../nupkgs
cd ../JDash.NetCore.Models
dotnet pack --output ../nupkgs  
cd ../JDash.NetCore.Provider.MsSQL
dotnet pack --output ../nupkgs 
cd ../JDash.NetCore.Provider.MySQL
dotnet pack --output ../nupkgs 
cd ..

nuget.exe setApiKey [oy2ae2qqggpop5vmezcq5llqkw2qq3wenoa5xp3twdegua] -source https://www.nuget.org
@echo on
for %%a in ("..\nupkgs\*symbols.nupkg") do (
	del %%a
)
for %%a in ("..\nupkgs\*.nupkg") do (
	nuget.exe push %%a oy2ae2qqggpop5vmezcq5llqkw2qq3wenoa5xp3twdegua -Source https://www.nuget.org/api/v2/package
)
pause