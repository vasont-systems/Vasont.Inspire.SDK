REM Nuget Package created on project build
..\Tools\Nuget\NugetOrg\nuget setApiKey oy2d24huncsaefeqqg5altwxwedktltergm4ixg2vr34yq
..\Tools\Nuget\NugetOrg\nuget push ".\src\bin\release\*.nupkg" oy2d24huncsaefeqqg5altwxwedktltergm4ixg2vr34yq -Source https://www.nuget.org/api/v2/package
del ".\src\bin\release\*.nupkg"
pause