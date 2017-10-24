@echo off
SET HOME=%cd%

cd "%HOME%"
call buildall.bat Release
cd "%HOME%\Mailer.NET"
"%HOME%\.nuget\NuGet.exe" Pack "%HOME%\Mailer.NET\Mailer.NET.csproj"

cd "%HOME%"