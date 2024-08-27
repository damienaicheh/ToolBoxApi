echo Build and package..

dotnet restore

dotnet build -c Release

echo Publishing the project..

dotnet publish -c Release -o out

cd out && zip -r app.zip .

mv app.zip ../app.zip