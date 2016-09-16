@echo off
SET HOME=%cd%

cd "%HOME%"
call buildall.bat ReleaseNET40
cd "%HOME%"
call buildall.bat ReleaseNET45
cd "%HOME%\Mailer.NET"
"%HOME%\.nuget\NuGet.exe" Pack "%HOME%\Mailer.NET\Mailer.NET.csproj"

cd "%HOME%"