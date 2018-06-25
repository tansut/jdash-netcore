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

nuget.exe setApiKey [oy2nshkqr5wa4gkyghvacz2e3znoynbefdzd62oke4pjqy] -source https://www.nugettest.org
@echo on
for %%a in ("..\nupkgs\*symbols.nupkg") do (
	del %%a
)
for %%a in ("..\nupkgs\*.nupkg") do (
	nuget.exe push %%a oy2nshkqr5wa4gkyghvacz2e3znoynbefdzd62oke4pjqy -Source https://www.nugettest.org/api/v2/package
)
pause