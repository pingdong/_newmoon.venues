Venue Management Service

[![Build Status](https://dev.azure.com/pingdong/Newmoon/_apis/build/status/pingdong.newmoon.venues?repoName=pingdong%2Fnewmoon.venues&branchName=master)](https://dev.azure.com/pingdong/Newmoon/_build/latest?definitionId=47&repoName=pingdong%2Fnewmoon.venues&branchName=master)

This project provides a full automatic solution of Azure Function App, from test automation, infrastructure as code, environment management through Azure DevOps pipeline.

A local.settings file contains below code snippet need to create under \src\Venues.FunctionApp.
~~~~
{
  "IsEncrypted": false,

    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
    }
}
~~~~
