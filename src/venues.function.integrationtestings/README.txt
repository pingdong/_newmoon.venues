1. Install Azure Functions Core Tools

npm i -g azure-functions-core-tools --unsafe-perm true

2. Create local.settings.json for local development

{
  "host": {
    "dotnetExePath": "%ProgramFiles%\\dotnet\\dotnet.exe",
    "funcHostExePath": "%APPDATA%\\npm\\node_modules\\azure-functions-core-tools\\bin\\func.dll",
    "funcAppExePath": "..\\..\\..\\..\\Places\\bin\\debug\\netcoreapp2.2",
    "port": 7071
  } 
}