@echo off
set scripts=%~dp0
set scripts=%scripts:~0,-1%

set solution=%1%.sln

dotnet restore %solution%
