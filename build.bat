dotnet test --logger "trx;LogFilePrefix=output" --collect "Code Coverage" 
dotnet build -c Release
dotnet tool update -g docfx
docfx docs/docfx.json
