REM Build Mailer.NET
SET CONFIGURATION=%1
set PATH_SOURCE_SLN="%cd%\Mailer.NET.sln"
if [%1]==[] (
  SET CONFIGURATION=DebugNET40
)
MSBuild %PATH_SOURCE_SLN% /p:Configuration=%CONFIGURATION%

