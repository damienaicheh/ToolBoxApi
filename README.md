# Tool Box API

## Build the project

dotnet restore

dotnet build -c Release

## Publish the project

dotnet publish -c Release -o out

cd out && zip -r app.zip .

## Deploy
az login --use-device-code --tenantid <tenant-id>
 
az account show

az webapp deploy \
  --name  ${{ parameters.webAppName }} \
  --resource-group ${{ parameters.webAppResourceGroup }} \
  --src-path app.zip
  --type zip 
