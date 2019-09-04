# newmoon.places
Place management service

Develop Branch:<br />
[![Build Status](https://pingdong.visualstudio.com/Newmoon/_apis/build/status/places?branchName=develop)](https://pingdong.visualstudio.com/Newmoon/_build/latest?definitionId=29&branchName=develop)<br />
Master Branch:<br />
[![Build Status](https://pingdong.visualstudio.com/Newmoon/_apis/build/status/places?branchName=master)](https://pingdong.visualstudio.com/Newmoon/_build/latest?definitionId=29&branchName=master)<br />
<br />

Deployment to QA<br />
[![Deployment Status](https://pingdong.vsrm.visualstudio.com/_apis/public/Release/badge/e91eaf4f-be05-424d-b72e-fc1d8aab16fc/4/10)](https://pingdong.visualstudio.com/Newmoon/_release?definitionId=4)<br />
Deployment to Production<br />
[![Deployment Status](https://pingdong.vsrm.visualstudio.com/_apis/public/Release/badge/e91eaf4f-be05-424d-b72e-fc1d8aab16fc/4/11)](https://pingdong.visualstudio.com/Newmoon/_release?definitionId=4)<br />
<br />

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