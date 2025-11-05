@echo off
echo Starting Premium Calculator API...
set ASPNETCORE_ENVIRONMENT=Development

echo Building project...
dotnet build

echo Starting API on http://localhost:4112...
dotnet exec bin\Debug\net8.0\PremiumCalculator.API.dll --urls "http://localhost:4112"
pause

