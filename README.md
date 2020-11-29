The project is a Azure implementation of the [Newmoon](https://github.com/pingdong/newmoon) project. 

# newmoon.venues
Place management service

If you want to check the authentication service, check [newmoon.authentication](https://github.com/pingdong/newmoon.authentication)

[![Build Status](https://dev.azure.com/pingdong/Newmoon/_apis/build/status/pingdong.newmoon.venues?repoName=pingdong%2Fnewmoon.venues&branchName=master)](https://dev.azure.com/pingdong/Newmoon/_build/latest?definitionId=47&repoName=pingdong%2Fnewmoon.venues&branchName=master)

The place service requires a local.setting.json with the following configuration under \src\places

~~~~
{
  "IsEncrypted": false,

  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "EventBus_ConnectionString": "--The connection string of the Service Bus--",
  },

  "EventBus": {
    "ConnectionString": "--The connection string of the Service Bus--",
    "Topic": "--The topic name--"
  },

  "ConnectionStrings": {
    "Default": "--The connection string of the database--"
  }
}
~~~~
