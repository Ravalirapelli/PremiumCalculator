# PowerShell script to run the Premium Calculator API
# This script helps avoid OneDrive sync issues by using dotnet exec instead of .exe

Write-Host "Starting Premium Calculator API..." -ForegroundColor Green

# Set environment
$env:ASPNETCORE_ENVIRONMENT = "Development"

# Build the project first
Write-Host "Building project..." -ForegroundColor Yellow
dotnet build

# Run using dotnet exec to bypass OneDrive .exe lock
Write-Host "Starting API on http://localhost:4112..." -ForegroundColor Green
dotnet exec bin\Debug\net8.0\PremiumCalculator.API.dll --urls "http://localhost:4112"

