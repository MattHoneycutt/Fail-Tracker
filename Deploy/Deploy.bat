@echo off
powershell -NoProfile -ExecutionPolicy unrestricted -Command "& {.\deploy.ps1 -Properties @{TargetDir='c:\inetpub\wwwroot\FailTracker'}; exit $error.Count}"
pause