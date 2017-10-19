@echo off
SET HOME=%cd%

cd "%HOME%"
call buildall.bat Release
cd "%HOME%\src\Mailer.NET"
"%HOME%\.nuget\NuGet.exe" Pack "%HOME%\src\Mailer.NET\Mailer.NET.csproj"
cd "%HOME%\src\Mailer.Net.Transport.Debug"
"%HOME%\.nuget\NuGet.exe" Pack "%HOME%\src\Mailer.Net.Transport.Debug\Mailer.Net.Transport.Debug.csproj"
cd "%HOME%\src\Mailer.Net.Transport.Mailgun"
"%HOME%\.nuget\NuGet.exe" Pack "%HOME%\src\Mailer.NET.Transport.Mailgun\Mailer.NET.Transport.Mailgun.csproj"

cd "%HOME%"